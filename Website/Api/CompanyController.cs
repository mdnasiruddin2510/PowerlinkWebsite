using PosWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.View_Models;

namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        protected string companyId = "COM-2";
        protected readonly AppDbContext _db;
        public CompanyController( AppDbContext db)
        {
            _db = db;
        }
        [HttpGet("GetCompanyLogo")]
        public async Task<string> GetCompanyLogo(string appId)
        {
            var logo = "/images/noimage.png";
            
                var company = await _db.Company.FirstOrDefaultAsync(x => x.Id == companyId);
                if (company?.LogoUrl is not null)
                {
                    logo = company.LogoUrl;
                }
            return logo;
        }
    }
}
