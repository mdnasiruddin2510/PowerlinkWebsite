using PosWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using PosWebsite.View_Models;

namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        protected readonly AppDbContext _db;
        protected string companyId = "COM-2";
        public CustomerOrderController( AppDbContext db)
        {
            _db = db;
        }
        public string GenerateInvoiceNumber(AppDbContext _db, string companyId)
        {
            string _number = "";
            string prefixValue = _db.SalesSettings.FirstOrDefault(x => x.CompanyId == companyId && x.Name == "Invoice Prefix").Value;
            string dateTimeValue = _db.SalesSettings.FirstOrDefault(x => x.CompanyId == companyId && x.Name == "DateTime in Invoice Prefix").Value;
            string prefix = "";
            if (!string.IsNullOrEmpty(prefixValue))
            {
                prefix = prefixValue + "-";
            }
            string dateTime = "";
            if (!string.IsNullOrEmpty(dateTimeValue))
            {
                switch (dateTimeValue)
                {
                    case "yyyy/MM/dd":
                        dateTime = DateTime.Now.ToString("yyyy/MM/dd");
                        break;
                    case "dd/MM/yyyy":
                        dateTime = DateTime.Now.ToString("dd/MM/yyyy");
                        break;
                    case "MM/dd/yyyy":
                        dateTime = DateTime.Now.ToString("MM/dd/yyyy");
                        break;
                    case "yy/MM/dd":
                        dateTime = DateTime.Now.ToString("yy/MM/dd");
                        break;
                    case "dd/MM/yy":
                        dateTime = DateTime.Now.ToString("dd/MM/yy");
                        break;
                    case "MM/dd/yy":
                        dateTime = DateTime.Now.ToString("MM/dd/yy");
                        break;
                    default:
                        dateTime = "";
                        break;
                }
                dateTime = dateTime + "-";
            }
            int countRow = _db.Sales.Count(x => x.CompanyId == companyId) + generateInvoiceNumberRetry;
            generateInvoiceNumberRetry++;
            return _number = dateTime + prefix + countRow.ToString("00000000");
        }
        int generateInvoiceNumberRetry = 1;
        [HttpPost("PlaceOrder")]
        public async Task<ActionResult<string>> PlaceOrder(Vm_Api_CustomerOrder vm)
        {
            var msg = "";
            
                if (vm.CustomerId > 0)
                {
                    int salesId = 0;
                    var customer = await _db.Customer.FirstOrDefaultAsync(x => x.Id == vm.CustomerId);
                    var branch = await _db.Branch.FirstOrDefaultAsync();
                    decimal previousDue = 0;
                    decimal totalPreviousDue = 0;
                    decimal dueAmount = 0;
                    decimal totalPurchase = 0;
                    var subLedger = await _db.AccountSubLedger.FirstOrDefaultAsync(x => x.Id == customer.AccountSubLedgerId);
                    bool due = false;
                    if (vm.GndTotal > vm.PaidAmount)
                    {
                        due = true;
                    }
                    string invoiceNumber = GenerateInvoiceNumber(_db, companyId);
                    var sale = new Sales
                    {
                        AdvanceAmount = vm.PaidAmount,
                        CompanyId = companyId,
                        BranchId = branch.Id,
                        CreatedBy = "API",
                        CreatedDate = DateTime.UtcNow,
                        InvoiceDate = DateTime.UtcNow,
                        InvoiceNumber = invoiceNumber,
                        SalesNote = vm.SalesNote,
                        TotalDiscountAmount = vm.DiscountAmount,
                        TotalDiscountPercentage = vm.DiscountPercentage,
                        TotalPaidAmount = vm.PaidAmount,
                        SubTotal = vm.SubTotal,
                        TotalSales = vm.GndTotal,
                        PaymentNote = "",
                        TotalOriginalAmount = vm.TotalOriginalAmount,
                        TotalProductDiscount = vm.TotalProductDiscount,
                        SalesGroup = vm.OrderGroup,
                        ReferenceNumber = "",
                        Deleted = false,
                        DeliveryAddress = vm.DeliveryAddress,
                        DeliveryCharge = vm.DeliveryCharge,
                        IsIncludedDeliveryChargeWithTotal = true,
                        TotalProductVat = 0,
                        ReturnAmount = 0,
                        TotalItems = vm.Products.Count,
                        AccountSubLedgerId = subLedger.Id,
                        IsIncludedVat = false,
                        CustomerId = customer.Id,
                        CustomerCode = customer.Code,
                        Name = customer.Name,
                        CompanyName = customer.CompanyName ?? "",
                        Phone = customer.Phone,
                        Address = customer.Address,
                        Status="Pending"
                    };
                    
                    if (due)
                    {
                        decimal changes = vm.GndTotal - vm.PaidAmount;
                        sale.DueAmount = changes;
                        sale.Dues = due;
                        totalPreviousDue += changes;
                        dueAmount = changes;
                    }

                    var salesItem = new List<SalesItem>();
                    if (vm.Products.Count > 0)
                    {
                        foreach (var item in vm.Products)
                        {
                            var iteminfo = await (from I in _db.Inventory
                                                  join P in _db.Product on I.ProductId equals P.Id
                                                  where I.Id == item.ItemId
                                                  select new
                                                  {
                                                      ProductId = I.ProductId,
                                                      PurchasePrice = I.PurchasePrice,
                                                      Barcode = I.Barcode,
                                                      Code = P.Code,
                                                      UnitId = I.UnitId,
                                                      Warranty = (P.WarrantyNote == null) ? "" : P.WarrantyNote + " For " + P.WarrantyPeriod + " " + P.WarrantyPeriodDuration.ToString()
                                                  }).FirstOrDefaultAsync();
                            var _item = new SalesItem
                            {
                                InventoryId = item.ItemId,
                                ProductId = iteminfo.ProductId,
                                Quantity = item.Qty,
                                TotalPurchase = item.Qty * iteminfo.PurchasePrice,
                                TotalSales = item.Amount,
                                UnitPrice = item.Price,
                                UnitId = iteminfo.UnitId,
                                ItemName = item.Name,
                                OriginalUnitPrice = item.OriginalUnitPrice,
                                Discount = item.Discount,
                                PurchaseRate = iteminfo.PurchasePrice,
                                ItemBarcode = iteminfo.Barcode,
                                ItemCode = iteminfo.Code,
                                Comments = item.Comments,
                                Vat = 0,
                                VatAmount = 0,
                                Warranty = iteminfo.Warranty,
                                FreeItemId = item.FreeItemId,
                                FreeQty = item.FreeQty
                            };
                            totalPurchase += _item.TotalPurchase;
                            salesItem.Add(_item);
                        }
                    }

                    var trans = _db.Database.BeginTransaction();

                    try
                    {
                        sale.TotalPurchase = totalPurchase;
                        sale.PreviousDue = previousDue;
                        sale.TotalPreviousDue = totalPreviousDue;
                        _db.Sales.Add(sale);
                        _db.SaveChanges();
                        salesId = sale.Id;
                        if (vm.Products.Count > 0)
                        {
                            foreach (var item in salesItem)
                            {
                                item.SalesId = salesId;
                                _db.SalesItem.Add(item);
                                _db.SaveChanges();
                                var data = await _db.Inventory.FirstOrDefaultAsync(x => x.Id == item.InventoryId);
                                var unit = await _db.ProductUnit.FirstOrDefaultAsync(x => x.Id == item.UnitId);

                                var itemHistory = new ItemSalesHistory
                                {
                                    Date = DateTime.UtcNow,
                                    Price = item.UnitPrice,
                                    Quantity = item.Quantity,
                                    SubLedgerId = subLedger.Id,
                                    SalesId = salesId,
                                    ItemId = item.InventoryId,
                                    ProductId = item.ProductId,
                                    Amount = item.TotalSales,
                                    CompanyId = companyId,
                                    CreatedBy = "API",
                                    CreatedDate = DateTime.UtcNow,
                                    Deleted = false,
                                    DeletedDate = DateTime.UtcNow,
                                    DeletedBy = "Shadow User"
                                };
                                _db.ItemSalesHistory.Add(itemHistory);
                                _db.SaveChanges();

                            }
                        }

                        if (vm.Products.Count > 0)
                        {
                            foreach (var item in salesItem.Where(x => x.FreeItemId > 0))
                            {
                                var data = await _db.Inventory.FirstOrDefaultAsync(x => x.Id == item.FreeItemId);
                                var unit = await _db.ProductUnit.FirstOrDefaultAsync(x => x.Id == item.UnitId);
 
                                var itemHistory = new ItemSalesHistory
                                {
                                    Date = DateTime.UtcNow,
                                    Price = data.SalePrice,
                                    Quantity = item.FreeQty,
                                    SubLedgerId = subLedger.Id,
                                    SalesId = salesId,
                                    ItemId = item.FreeItemId,
                                    ProductId = data.ProductId,
                                    Amount = 0,
                                    CompanyId = companyId,
                                    CreatedBy = "API",
                                    CreatedDate = DateTime.UtcNow,
                                    Deleted = false,
                                    DeletedDate = DateTime.UtcNow,
                                    DeletedBy = "Shadow User"
                                };
                                _db.ItemSalesHistory.Add(itemHistory);
                                _db.SaveChanges();

                            }
                        }

                    if (sale.CustomerId > 0)
                    {
                        var rate = await _db.SalesSettings.FirstOrDefaultAsync(x => x.Name == "Customer Point Rate" & x.CompanyId == companyId && x.BranchId == branch.Id);
                        if (rate != null)
                        {
                            decimal pointRate = Convert.ToDecimal(rate.Value);
                            var customerPoint = new CustomerPointHistory
                            {
                                CompanyId = companyId,
                                CreatedBy = "API",
                                CreatedDate = DateTime.UtcNow,
                                CustomerId = sale.CustomerId,
                                Point = sale.TotalSales * pointRate,
                                Rate = pointRate,
                                SalesId = salesId,
                                Total = sale.TotalSales,
                            };
                            _db.CustomerPointHistory.Add(customerPoint);
                            _db.SaveChanges();
                            if (customer != null)
                            {
                                var updateCustomer = customer;
                                customer.Point += customerPoint.Point;
                                _db.Entry(customer).CurrentValues.SetValues(updateCustomer);
                                _db.SaveChanges();
                            }
                        }
                    }

                    var cashLedger = _db.AccountSubLedger.FirstOrDefault(x => x.LedgerType == "Payment" && x.CompanyId == companyId && !x.UserDefined);
                        if (subLedger.Id > 0)
                        {
                            var customerCRTransaction = new AccountingTransaction
                            {
                                AccountGroupId = subLedger.AccountGroupId,
                                AccountLedgerId = subLedger.AccountLedgerId,
                                AccountSubLedgerId = subLedger.Id,
                                CompanyId = companyId,
                                BranchId = branch.Id,
                                TransactionDate = DateTime.UtcNow,
                                Dr = vm.GndTotal,
                                Cr = 0,
                                Particulars = "Total bill for customer",
                                RefNumber = "Invoice Number:" + invoiceNumber,
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = "API",
                                HelperId = salesId.ToString(),
                                HelperModule = "Sales",
                                Url = "/sales/invoice/" + salesId,

                            };
                            _db.AccountingTransaction.Add(customerCRTransaction);
                            _db.SaveChanges();


                            if (vm.PaidAmount > 0)
                            {
                                var advanceAdjAmountTransaction = new AccountingTransaction
                                {
                                    AccountGroupId = subLedger.AccountGroupId,
                                    AccountLedgerId = subLedger.AccountLedgerId,
                                    AccountSubLedgerId = subLedger.Id,
                                    CompanyId = companyId,
                                    BranchId = branch.Id,
                                    TransactionDate = DateTime.UtcNow,
                                    Dr = vm.PaidAmount,
                                    Cr = 0,
                                    Particulars = "Adjust with advance amount",
                                    RefNumber = "Invoice Number:" + invoiceNumber,
                                    CreatedDate = DateTime.UtcNow,
                                    CreatedBy = "API",
                                    HelperId = salesId.ToString(),
                                    HelperModule = "Sales",
                                    Url = "/sales/invoice/" + salesId,
                                };
                                _db.AccountingTransaction.Add(advanceAdjAmountTransaction);
                                _db.SaveChanges();

                                var advancePayment = new SalesPayment
                                {
                                    Amount = vm.PaidAmount,
                                    PaymentMethod = "Adjust with advance",
                                    PaymentDate = DateTime.UtcNow,
                                    SalesId = salesId
                                };
                                _db.SalesPayment.Add(advancePayment);
                                _db.SaveChanges();
                            }


                        }

                        var salesLedger = _db.AccountLedger.FirstOrDefault(x => x.Name == "Sales" && !x.UserDefined && x.CompanyId == companyId);
                        if (salesLedger is not null)
                        {
                            var salesCRTransaction = new AccountingTransaction
                            {
                                AccountGroupId = salesLedger.AccountGroupId,
                                AccountLedgerId = salesLedger.Id,
                                AccountSubLedgerId = 0,
                                CompanyId = companyId,
                                BranchId = branch.Id,
                                TransactionDate = DateTime.UtcNow,
                                Cr = vm.GndTotal,
                                Dr = 0,
                                Particulars = "Total bill of sales",
                                RefNumber = "Invoice Number:" + invoiceNumber,
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = "API",
                                HelperId = salesId.ToString(),
                                HelperModule = "Sales",
                                Url = "/sales/invoice/" + salesId,
                            };
                            _db.AccountingTransaction.Add(salesCRTransaction);
                            _db.SaveChanges();
                        }
                        msg = "Order placed successfully!";
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        msg = "An error occured!";
                    }

                }
                else
                {
                    msg = "Please send valid customer information!";
                }
            return msg;
        }
    }
}
