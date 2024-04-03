using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.Models;
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
    }
}
