using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.Models;
using PosWebsite.View_Models;

namespace Website.Api
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly AppDbContext _db;
		public ProductController(AppDbContext db)
		{
			_db = db;
		}
		[HttpPost("GetAllProduct")]
		public async Task<ActionResult<List<VmProduct>>> GetAllProduct(VmFilterData vm)
		{
			var bids = new List<string>();
			var cids = new List<string>();
			var firstBids = vm.BrandIds.TrimEnd(',').Split(',').ToList();
			foreach (var item in firstBids)
			{
				bids.Add(item.Split('-')[0]);
			}
			var firstCids = vm.CategoryIds.TrimEnd(',').Split(',').ToList();
			foreach (var item in firstCids)
			{
				cids.Add(item.Split('-')[0]);
			}
			var model = await (from _product in _db.Product.AsNoTracking().Where(x => (vm.BrandIds == "" || bids.Contains(x.ProductBrandId.ToString()))
														   && (vm.CategoryIds == "" || cids.Contains(x.CategoryId.ToString()))
														   && (vm.MinPrice == 0 || x.Price > vm.MinPrice)
														   && (vm.MaxPrice == 0 || x.Price < vm.MaxPrice)
														   && !x.Deleted)
							   join _inventory in _db.Inventory.Where(x => !x.Deleted) on _product.Id equals _inventory.ProductId
							   join category in _db.ProductCategory.Where(x => !x.Deleted) on _product.CategoryId equals category.Id into categories
							   from _category in categories.DefaultIfEmpty()
							   join brand in _db.ProductBrand.Where(x => !x.Deleted) on _product.ProductBrandId equals brand.Id into brands
							   from _brand in brands.DefaultIfEmpty()
							   select new VmProduct
							   {
								   Id = _product.Id,
								   Name = _product.Name,
								   Code = _product.Code,
								   ProductImageUrl = _product.ProductImageUrl ?? "/images/noimage.png",
								   Price = _product.Price,
								   IsFeatured = _product.IsFeatured,
								   CategoryName = _category.Name ?? "",
								   BrandName = _brand.Name ?? ""
							   }).Take(39).ToListAsync();
			return model;
		}
		[HttpGet("SearchProduct")]
		public async Task<ActionResult<List<VmProduct>>> SearchProduct(string key)
		{
			var products = await (from _product in _db.Product.AsNoTracking().Where(x => !x.Deleted && x.Name.ToLower().Contains(key.ToLower()))
								  join _inventory in _db.Inventory.Where(x => !x.Deleted) on _product.Id equals _inventory.ProductId
								  join category in _db.ProductCategory.Where(x => !x.Deleted) on _product.CategoryId equals category.Id into categories
								  from _category in categories.DefaultIfEmpty()
								  join brand in _db.ProductBrand.Where(x => !x.Deleted) on _product.ProductBrandId equals brand.Id into brands
								  from _brand in brands.DefaultIfEmpty()
								  select new VmProduct
								  {
									  Id = _product.Id,
									  Name = _product.Name,
									  ProductImageUrl = _product.ProductImageUrl ?? "/images/noimage.png",
									  Price = _product.Price,
									  CategoryId = _product.CategoryId,
									  IsFeatured = _product.IsFeatured,
									  CategoryName = _category.Name ?? "",
									  BrandName = _brand.Name ?? ""
								  }).ToListAsync();
			return products;
		}
	}
}
