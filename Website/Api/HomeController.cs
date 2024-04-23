using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.Models;
using PosWebsite.View_Models;
using Website.Models;
using Website.View_Models;

namespace Website.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetAllMenu")]
        public async Task<ActionResult<List<VmMenu>>> GetAllMenu()
        {
            var menu = await _db.Menu.OrderBy(x=> x.Order).Select(x => new VmMenu
            {
                Id = x.Id,
                Title = x.Title,
                Url = x.Url,
            }).ToListAsync();
            return menu;
        }
        [HttpGet("GetAllBanner")]
        public async Task<ActionResult<List<Banner>>> GetAllBanner()
        {
            var banner = await _db.Banner
                                  .Select(x => new Banner
                                  {
                                      Id = x.Id,
                                      Title = x.Title,
                                      ImageUrl = x.ImageUrl,
                                  }).ToListAsync();
            return banner;
        } 
        [HttpGet("GetAllBrandPartners")]
        public async Task<ActionResult<List<Partners>>> GetAllBrandPartners()
        {
            var partners = await _db.Partners
                                  .Select(x => new Partners
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      Logo = x.Logo,
                                      Description = x.Description,
                                      ProjectId = x.ProjectId
                                  }).ToListAsync();
            return partners;
        }
        [HttpGet("GetAllMajorClients")]
        public async Task<ActionResult<List<Clients>>> GetAllMajorClients()
        {
            var clients = await _db.Clients
                                  .Select(x => new Clients
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      Logo = x.Logo,
                                      Description = x.Description,
                                      ProjectId = x.ProjectId
                                  }).ToListAsync();
            return clients;
        }
        [HttpGet("GetContactInformation")]
        public async Task<ActionResult<Company>> GetContactInformation()
        {
            var companyInfo = await _db.Company
                                  .Select(x => new Company
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      Phone = x.Phone,
                                      Email = x.Email,
                                      Address = x.Address,
                                      LogoUrl = x.LogoUrl
                                  }).FirstOrDefaultAsync();
            return companyInfo;
        }
        [HttpGet("GetAboutUs")]
        public async Task<ActionResult<AboutUs>> GetAboutUs()
        {
            var aboutUs = await _db.AboutUs
                                  .Select(x => new AboutUs
                                  {
                                      Id = x.Id,
                                      WhoWeAre = x.WhoWeAre,
                                      Vision = x.Vision,
                                      Mission = x.Mission,
                                      Policy = x.Policy,
                                      Strength = x.Strength,
                                  }).FirstOrDefaultAsync();
            return aboutUs;
        }
        [HttpGet("GetSolutions")]
        public async Task<ActionResult<List<VmSolutions>>> GetSolutions()
        {
            var list = new List<VmSolutions>();
            
            var data = await _db.Solutions.ToListAsync();
            foreach (var item in data)
            {
                var model = new VmSolutions();
                var productList = new List<string>();
                model.Id = item.Id;
                model.Name = item.Name;
                var products = item.ProductIds.TrimEnd(',').Split(',').ToList();
                foreach (var i in products)
                {
                    productList.Add(i);
                }
                model.Products = productList;
                list.Add(model);
            }
            return list;
        }
		[HttpGet("GetProductById")]
		public async Task<ActionResult<VmProduct>> GetProductById(int id)
		{
			var model = await (from _product in _db.Product.Where(x => !x.Deleted && x.Id == id)
							   join _inventory in _db.Inventory.Where(x => !x.Deleted) on _product.Id equals _inventory.ProductId
							   select new VmProduct
							   {
								   Id = _product.Id,
								   Name = _product.Name,
								   Code = _product.Code,
								   Hscode = _product.Hscode,
								   ShortDescription = _product.ShortDescription,
								   FullDescription = _product.FullDescription,
								   SpecificationId = _product.SpecificationId,
								   ProductImageUrl = _product.ProductImageUrl ?? "/images/noimage.png",
								   Price = _product.Price,
								   CategoryId = _product.CategoryId,
								   SubCategoryId = _product.SubCategoryId,
								   ProductTypeId = _product.ProductTypeId,
								   ProductBrandId = _product.ProductBrandId,
								   GlobalBarcode = _product.GlobalBarcode,
								   Vat = _product.Vat,
								   Weight = _product.Weight,
								   WarrantyPeriod = _product.WarrantyPeriod,
								   WarrantyPeriodDuration = _product.WarrantyPeriodDuration,
								   WarrantyNote = _product.WarrantyNote,
								   WarrantyShownInInvoice = _product.WarrantyShownInInvoice,
								   NotifiedBeforeExpired = _product.NotifiedBeforeExpired,
								   CompanyId = _product.CompanyId,
								   AdditionalField1 = _product.AdditionalField1,
								   AdditionalField1Value = _product.AdditionalField1Value,
								   AdditionalField2 = _product.AdditionalField2,
								   AdditionalField2Value = _product.AdditionalField2Value,
								   AdditionalField3 = _product.AdditionalField3,
								   AdditionalField3Value = _product.AdditionalField3Value,
								   AdditionalField4 = _product.AdditionalField4,
								   AdditionalField4Value = _product.AdditionalField4Value,
								   Stock = _inventory.Quantity,
								   VariantName = _inventory.VariantName,
								   ExpireDate = _inventory.ExpireDate.Value.ToString("dd MM yy"),
								   Barcode = _inventory.Barcode,
							   }).FirstOrDefaultAsync();
			return model;
		}
	}
}
