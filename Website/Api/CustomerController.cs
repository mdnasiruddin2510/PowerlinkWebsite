
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using PosWebsite.Models;
using PosWebsite.View_Models;


namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _db;
        protected string companyId = "COM-2";
        public CustomerController(AppDbContext db)
        {
            _db = db;
        }
        [Route("GetCustomerInfo")]
        [HttpGet]
        public async Task<ActionResult<VmUser>> GetCustomerInfo(string userName, string appId)
        {
            try
            {
                var data = await _db.Customer.FirstOrDefaultAsync(x => (x.Email.ToLower() == userName.ToLower() || x.UserName.ToLower() == userName.ToLower()) && x.CompanyId == companyId);
                var customer = new VmUser();
                if (data == null)
                {
                    customer.Name = "";
                    customer.Phone = "";
                    customer.Email = userName;
                    customer.Address = "";
                    customer.State = "";
                    customer.Zip = "";
                    customer.City = "";
                    customer.Username = userName;
                }
                else
                {
                    customer.Id = data.Id;
                    customer.Name = data.Name;
                    customer.Phone = data.Phone ?? "";
                    customer.Email = data.Email ?? "";
                    customer.Address = data.Address ?? "";
                    customer.State = data.State ?? "";
                    customer.Zip = data.Zip ?? "";
                    customer.City = data.City ?? "";
                    customer.Username = userName;
                }
                return customer;
            }
            catch(Exception ex)
            {

            }
            return new VmUser();
        }
        [HttpPost("UpdateCustomer")]
        public async Task<ActionResult> UpdateCustomer(VmCustomer vm)
        {
            string msg = "";
            if (vm.Id > 0)
            {
                var customer = await _db.Customer.FirstOrDefaultAsync(x => x.Id == vm.Id || x.UserName.ToLower() == vm.Username.ToLower());
                if (customer is not null)
                {
                    customer.Id = vm.Id;
                    customer.Name = vm.Name;
                    customer.Phone = vm.Phone ?? "";
                    customer.Email = vm.Email ?? "";
                    customer.Address = vm.Address ?? "";
                    customer.State = vm.State ?? "";
                    customer.Zip = vm.Zip ?? "";
                    customer.City = vm.City ?? "";
                    customer.UserName = vm.Username;
                    _db.Customer.Update(customer);
                    await _db.SaveChangesAsync();

                    var subLedger = await _db.AccountSubLedger.FirstOrDefaultAsync(x => x.Id == customer.AccountSubLedgerId);
                    if (subLedger is not null)
                    {
                        subLedger.Name = vm.Name;
                        subLedger.LedgerNo = vm.Phone;
                        _db.AccountSubLedger.Update(subLedger);
                        await _db.SaveChangesAsync();
                    }
                    msg = "Update Successfully!";
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(vm.Username) && !string.IsNullOrEmpty(vm.Phone) && !string.IsNullOrEmpty(vm.Email))
                {
                    var accountReceivable = await _db.AccountLedger.Where(x => x.Name == "Account Receivable").FirstOrDefaultAsync();
                    var ledger = new AccountSubLedger
                    {
                        CompanyId = companyId,
                        LedgerNo = vm.Phone,
                        Name = vm.Name,
                        AccountLedgerId = accountReceivable.Id,
                        AccountGroupId = accountReceivable.AccountGroupId,
                        UserDefined = true,
                        LedgerType = "Customer"
                    };
                    await _db.AccountSubLedger.AddAsync(ledger);
                    await _db.SaveChangesAsync();

                    var customer = new Customer
                    {
                        AccountSubLedgerId = ledger.Id,
                        Name = vm.Name,
                        Email = vm.Email,
                        Phone = vm.Phone,
                        PhotoUrl = "/images/noimage.png",
                        Address = vm.Address,
                        CreatedDate = DateTime.UtcNow,
                        UserName = vm.Username,
                        Common = false,
                        IsUser = true,
                        AreaId = 1,
                        Priority = 1,
                        IsImportant = false,
                        Active = false,
                        CompanyId = companyId,
                    };
                    await _db.AddAsync(customer);
                    await _db.SaveChangesAsync();
                    msg = "Add Successfully!";
                }
                else
                {
                    msg = "Missing required fields!";
                }
            }
            return Ok(msg);
        }
        [HttpPost("WishList")]
        public async Task<int> WishList(VmWishList vm)
        {
            int response = 0;
                var entity = await _db.WishList.FirstOrDefaultAsync(x => x.CustomerId == vm.CustomerId && x.ProductId == vm.ProductId);
                if (entity is null)
                {
                    var model = new WishList
                    {
                        ProductId = vm.ProductId,
                        CustomerId = vm.CustomerId,
                        CreatedDate = DateTime.UtcNow,
                    };
                    await _db.WishList.AddAsync(model);
                    await _db.SaveChangesAsync();
                    response = 1;
                }
                else
                {
                    _db.Remove(entity);
                    await _db.SaveChangesAsync();
                }
            return response;
        }
        [HttpGet("GetWishListByCustomerId")]
        public async Task<ActionResult<List<VmWishListProducts>>> GetWishListByCustomerId(int customerId)
        {
            var response = new List<VmWishListProducts>();
            var products = await (from W in _db.WishList
                                    join P in _db.Product on W.ProductId equals P.Id
                                    where P.CompanyId == companyId && !P.Deleted
                                    select new VmWishListProducts
                                    {
                                        Id = P.Id,
                                        Code = P.Code,
                                        Name = P.Name,
                                        Price = P.Price,
                                        ProductImageUrl = P.ProductImageUrl ?? "/images/noimage.png"
                                    }).ToListAsync();

            return products;
        }
        [HttpGet("RemoveWishList")]
        public async Task<int> RemoveWishList(int customerId, int productId)
        {
            int response = 0;
            var entity = await _db.WishList
                                  .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.ProductId == productId);
            if (entity is not null)
            {
                _db.WishList.Remove(entity);
                await _db.SaveChangesAsync();
                response = 1;
            }
            return response;
        }
        [HttpPost("CreateCustomer")]
        public async Task<ActionResult<int>> CreateCustomer(VmRegister vm)
        {
            var response = 0;
            var exist = await _db.Customer.FirstOrDefaultAsync(x => x.Email.ToLower() == vm.Email.ToLower() || x.UserName.ToLower() == vm.Username.ToLower() || x.Phone.ToLower() == vm.Phone.ToLower());
            if (exist is not null)
            {
                response = 2; // if response is 2 then show message "UserName or Email is already used
                return response;
            }
            if (!string.IsNullOrEmpty(vm.Username) && !string.IsNullOrEmpty(vm.Phone) && !string.IsNullOrEmpty(vm.Email))
            {
                var accountReceivable = await _db.AccountLedger.Where(x => x.Name == "Account Receivable").FirstOrDefaultAsync();
                var ledger = new AccountSubLedger
                {
                    CompanyId = companyId,
                    LedgerNo = vm.Phone,
                    Name = vm.Name,
                    AccountLedgerId = accountReceivable.Id,
                    AccountGroupId = accountReceivable.AccountGroupId,
                    UserDefined = true,
                    LedgerType = "Customer"
                };
                await _db.AccountSubLedger.AddAsync(ledger);
                await _db.SaveChangesAsync();

                var customer = new Customer
                {
                    AccountSubLedgerId = ledger.Id,
                    Name = vm.Name,
                    Email = vm.Email,
                    Phone = vm.Phone,
                    PhotoUrl = "/images/noimage.png",
                    Address = "",
                    CreatedDate = DateTime.UtcNow,
                    UserName = vm.Username,
                    Common = false,
                    IsUser = true,
                    AreaId = 1,
                    Priority = 1,
                    IsImportant = false,
                    Active = false,
                    CompanyId = companyId,
                };
                await _db.AddAsync(customer);
                await _db.SaveChangesAsync();
                response = 1;
            }
            else
            {
                response = -1;
            }
            return response;
        }
        [HttpPost("ManualLogin")]
        public async Task<int> ManualLogin(VmLogin vm)
        {
            var response = 0;
            var check = await _db.Customer.FirstOrDefaultAsync(x => (x.UserName.ToLower() == vm.UserName.ToLower() || x.Email.ToLower() == vm.UserName.ToLower()) && x.Password == vm.Password);
            if (check is not null)
            {
                response = 1;
            }
            else
            {
                response = 2;
            }
            return response;
        }


    }

}


