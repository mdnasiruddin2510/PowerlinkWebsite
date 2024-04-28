using PosWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.View_Models;
using System.ComponentModel.Design;

namespace Project623.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
       
        protected readonly AppDbContext _db;
        protected string companyId = "COM-2";
        public CategoryController( AppDbContext db)
        {
            
            _db = db;
        }
        [HttpGet("GetAllCategory")]
        public async Task<List<VmProductCategory>> GetAllCategory(string appId)
        {
            var response = new List<VmProductCategory>();
            
                var model = await _db.ProductCategory.Where(x => !x.Deleted && x.ParentId == 0)
                                                   .Select(s => new VmProductCategory
                                                   {
                                                       Id = s.Id,
                                                       Name = s.Name,
                                                       PictureUrl = s.PictureUrl ?? "/images/noimage.png",
                                                       DisplayOrder = s.DisplayOrder,
                                                   }).ToListAsync();

                return model.OrderBy(o => o.DisplayOrder).ToList();
            
        }
        [HttpGet("GetProductBySubCategory")]
        public async Task<List<VmProduct>> GetProductBySubCategory(int id, string appId)
        {
            var result = new List<VmProduct>();
            
                var query = await (from P in _db.Product
                                   join I in _db.Inventory on P.Id equals I.ProductId
                                   where !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId && P.SubCategoryId==id
                                   select new VmProduct
                                   {
                                       ProductId = P.Id,
                                       ItemId = I.Id,
                                       Barcode = I.Barcode,
                                       Name = P.Name + (I.VariantName ?? ""),
                                       Code = P.Code,
                                       ProductImageUrl = P.ProductImageUrl ?? "/images/noimage.png",
                                       Price = I.SalePrice,
                                       CategoryId = P.CategoryId,
                                       IsFeatured = P.IsFeatured
                                   }).ToListAsync();
                var products = query.GroupBy(g => new { g.Price, g.ProductId, g.Barcode }).ToList();
                foreach (var item in products)
                {
                    result.Add(new VmProduct
                    {
                        ProductId = item.FirstOrDefault().ProductId,
                        ItemId = item.FirstOrDefault().ItemId,
                        Barcode = item.FirstOrDefault().Barcode,
                        Name = item.FirstOrDefault().Name,
                        Code = item.FirstOrDefault().Code,
                        ProductImageUrl = item.FirstOrDefault().ProductImageUrl,
                        Price = item.FirstOrDefault().Price,
                        CategoryId = item.FirstOrDefault().CategoryId,
                        IsFeatured = item.FirstOrDefault().IsFeatured
                    });
                }
            return result;
        }
    }
}
