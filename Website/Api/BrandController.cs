using PosWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.View_Models;

namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        protected readonly AppDbContext _db;

        protected string companyId = "COM-2";
        public BrandController( AppDbContext db)
        { 
            _db = db;
        }
        [HttpGet("GetProductBrand")]
        public async Task<List<VmProductBrand>> GetProductBrand(string appId)
        {
            var result = new List<VmProductBrand>();
            result = await _db.ProductBrand.Where(x => !x.Deleted)
                                        .Select(s => new VmProductBrand
                                        {
                                            Id = s.Id,
                                            Name = s.Name,
                                        }).OrderBy(o => o.Name).ToListAsync();
            return result;
        }
        [HttpGet("GetBrandByCategory")]
        public async Task<List<VmProductBrand>> GetBrandByCategory(int id,string appId)
        {
            var result = new List<VmProductBrand>();
            var productList = await _db.Product.Where(x => x.CategoryId == id && x.ProductBrandId>0).ToListAsync();
            result = await (from P in _db.Product
                                join B in _db.ProductBrand on P.ProductBrandId equals B.Id
                                where !P.Deleted && P.CompanyId==companyId && P.ProductBrandId>0 && P.CategoryId==id
                                select new VmProductBrand
                                {
                                    Id = B.Id, 
                                    Name = B.Name
                                }).OrderBy(o=>o.Name).ToListAsync();
               
            
            return result;
          
        }
    }
}
