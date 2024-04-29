using PosWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PosWebsite.View_Models;
using Website.Helper;

namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ////  DEPRICATED API---------------DELETE THIS LATER
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ItemController(AppDbContext db)
        {
            _db = db;
        }
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(VmAddItemApi vm)
        {
            string msg = "";
            string conn = _db.Database.GetConnectionString();
            var location = _db.InventoryLocation.FirstOrDefault();
            var unit = _db.ProductUnit.FirstOrDefault();
            var branch = _db.Branch.FirstOrDefault();
            var trans = await _db.Database.BeginTransactionAsync();

            try
            {
                var check = await _db.Product.FirstOrDefaultAsync(x => x.GlobalBarcode == vm.Barcode && !x.Deleted);
                if(check == null)
                {
                    var product = new Product
                    {
                        Name = vm.Name,
                        Code = await GenerateProductCode(),
                        Price = vm.Price,
                        CategoryId = vm.CategoryId,
                        SubCategoryId = 0,
                        Vat = vm.Vat,
                        GlobalBarcode = vm.Barcode,
                        CompanyId = "COM-2",
                        CreatedBy = "App",
                        CreatedDate = DateTime.UtcNow,
                        Deleted = false,
                        ProductBrandId = vm.BrandId,
                        ProductTypeId = vm.ProductTypeId
                        
                    };
                    await _db.Product.AddAsync(product);
                    await _db.SaveChangesAsync();


                    var inventory = new Inventory
                    {
                        BranchId = branch.Id,
                        LocationId = location.Id,
                        Barcode = generateBarcodeReturnString(),
                        UnitId = unit.Id,
                        PurchasePrice = 0,
                        Quantity = 0,
                        SalePrice = vm.Price,
                        VariantName = "",
                        WholesalePrice = 0,
                        CompanyId = "COM-2",
                        CreatedBy = "App",
                        CreatedDate = DateTime.UtcNow,
                        Deleted = false,
                        MinStockQty = 0,
                        MinWholesaleQty = 0,
                        VariantRefNumber = "",
                        ProductId = product.Id,
                        UniversalBarcode = vm.Barcode
                    };
                    await _db.Inventory.AddAsync(inventory);
                    await _db.SaveChangesAsync();

                    var inventoryHist = new InventoryHistory
                    {
                        BranchId = branch.Id,
                        LocationId= location.Id,
                        CompanyId = "COM-2",
                        CreatedBy = "App",
                        CreatedDate = DateTime.UtcNow,
                        CurrentQuantity = 0,
                        OperationType = "IN",
                        Particulars = "Insert as openning stock",
                        InventoryId = inventory.Id,
                        PreviousQuantity = 0,
                        Quantity = 0,
                        CustomerId = 0,
                        InvoiceId = 0,
                        ProductId = product.Id,
                    };
                    await _db.InventoryHistory.AddAsync(inventoryHist);
                    await _db.SaveChangesAsync();

                    await trans.CommitAsync();
                    msg = "save";
                }
                else
                {
                    msg = "Already exist!";
                }
                
            }
            catch (Exception e)
            {
                await trans.RollbackAsync();
            }

            return Ok(msg);
        }
        private string generateBarcodeReturnString()
        {
            var number = AppFunction.Generate_4_digitRandomNo().ToString();
            var isExits = _db.Inventory.Any(x => x.Barcode.ToLower() == number.ToLower() && x.CompanyId == "COM-2" && !x.Deleted);
            var result = "";
            if (isExits)
            {
                number = number + DateTime.Now.ToString("ss");
                generateBarcodeReturnString();
            }
            else
            {
                result = number;
            }
            return result;
        }

        int tempNumber = 0;
        private async Task<string> GenerateProductCode()
        {
            int number = _db.Product.Where(x => x.CompanyId == "COM-2").Count() + 1;
            if (tempNumber > 0)
            {
                number = tempNumber + 1;
            }
            string prefix = _db.ProductSetting.FirstOrDefault(x => x.Name == "Product Code Prefix" && x.CompanyId == "COM-2" ).Value;
            var code = prefix + number.ToString("000000");
            code = number.ToString("000000");
            var isExits = await _db.Product.AnyAsync(x => x.Code.ToLower() == code.ToLower() && !x.Deleted);
            var result = "";
            if (isExits)
            {
                if (tempNumber == 0) { tempNumber = number; }
                ++tempNumber;
                await GenerateProductCode();
            }
            else
            {
                result = code;
                tempNumber = 0;
            }
            return result;
        }
        [HttpGet("GetCategory")]
        public async Task<List<SelectListItem>> GetCategory()
        {
            var categories =await _db.ProductCategory.Where(x=>!x.Deleted).Select(s=>new SelectListItem {Text=s.Name,Value=s.Id.ToString()}).ToListAsync();
            return categories;
        }
        [HttpGet("GetProductType")]
        public async Task<List<SelectListItem>> GetProductType()
        {
            var types = await _db.ProductType.Where(x => !x.Deleted).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToListAsync();
            return types;
        }
        [HttpGet("GetBrand")]
        public async Task<List<SelectListItem>> GetBrand()
        {
            var brands = await _db.ProductBrand.Where(x => !x.Deleted).Select(s => new SelectListItem { Text = s.Name, Value = s.Id.ToString() }).ToListAsync();
            return brands;
        }
        [HttpGet("Checkbarcode")]
        public async Task<string> Checkbarcode(string code)
        {
            string msg = "";
            var check = await _db.Product.FirstOrDefaultAsync(x => x.GlobalBarcode == code && !x.Deleted);
            if (check != null)
            {
                msg = "Already Exists!";
            }
            return msg;
        }
    }
}
