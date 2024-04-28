using PosWebsite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PosWebsite.View_Models;

namespace PosWebsite.Api_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        protected readonly AppDbContext _db;

        protected string companyId = "COM-2";
        public ProductController( AppDbContext db)
        {
            _db = db;
        }
        [HttpPost("GetAllProduct")]
        public async Task<List<VmProduct>> GetAllProduct(VmFilterData vm)
        {
            var result = new List<VmProduct>();
            
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
                var query = await (from P in _db.Product
                                   join I in _db.Inventory on P.Id equals I.ProductId
                                   where !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId &&
                                   (vm.BrandIds == "" || bids.Contains(P.ProductBrandId.ToString()))   //CONFUSED ----
                                                               && (vm.CategoryIds == "" || cids.Contains(P.CategoryId.ToString()))  //CONFUSED ----
                                                               && (vm.MinPrice == 0 || P.Price > vm.MinPrice) //CONFUSED ----
                                                               && (vm.MaxPrice == 0 || P.Price < vm.MaxPrice) //CONFUSED ----HOW IT WORKS
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
                                       IsFeatured = P.IsFeatured,
                                   }).OrderBy(o => o.Code).ToListAsync();
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
            
            return result.DistinctBy(x => new { x.ProductId,x.Code,x.Price }).ToList();
        }


        [HttpGet("GetProductById")]
        public async Task<Vm_Api_ProductDetails> GetProductById(int id, string appId)
        {
            var result = new Vm_Api_ProductDetails();
            result = await (from P in _db.Product
                            join I in _db.Inventory on P.Id equals I.ProductId
                            where !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId
                            select new Vm_Api_ProductDetails
                            {
                                ProductId = P.Id,
                                ItemId = I.Id,
                                Name = P.Name,
                                Code = P.Code,
                                ProductImageUrl = P.ProductImageUrl ?? "/images/noimage.png",
                                Price = I.SalePrice
                            }).FirstOrDefaultAsync();
            return result;
        }
        [HttpPost("GetTopProduct")]
        public async Task<List<VmProduct>> GetTopProduct(VmFilterData vm)
        {
            var result = new List<VmProduct>();
            
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

            var topSale = await _db.ItemSalesHistory.OrderByDescending(x => x.Quantity).Select(s => s.ItemId).Distinct().Take(10).ToListAsync();
            foreach (var item in topSale)
            {
                result = await (from P in _db.Product
                                join I in _db.Inventory on P.Id equals I.ProductId
                                where !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId && I.Id == item &&
                                (vm.BrandIds == "" || bids.Contains(P.ProductBrandId.ToString()))   //CONFUSED ----
                                && (vm.CategoryIds == "" || cids.Contains(P.CategoryId.ToString()))  //CONFUSED ----
                                && (vm.MinPrice == 0 || P.Price > vm.MinPrice) //CONFUSED ----
                                && (vm.MaxPrice == 0 || P.Price < vm.MaxPrice) //CONFUSED ----HOW IT WORKS
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
                                }).OrderBy(o => o.Code).ToListAsync();
            }
            return result.DistinctBy(x => new { x.ProductId, x.Code, x.Price }).ToList();
        }
        [HttpGet("GetRelatedProductById")]
        public async Task<List<VmProduct>> GetRelatedProductById(int id, string appId)
        {
            var result = new List<VmProduct>();
            var product = await _db.Product.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
            var query = await (from P in _db.Product
                                join I in _db.Inventory on P.Id equals I.ProductId
                                where P.CategoryId == product.CategoryId
                                && !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId
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
            return result.DistinctBy(x => new { x.ProductId, x.Code, x.Price }).ToList();

        }
        [HttpGet("GetProductByCategoryId")]
        public async Task<VmProductWithSubCat> GetProductByCategoryId(int id, string appId)
        {
            var result = new VmProductWithSubCat();
            var subCategories = await _db.ProductCategory.Where(x => x.ParentId == id)
                                                    .Select(s => new VmProductCategory
                                                    {
                                                        Id = s.Id,
                                                        Name = s.Name,
                                                        PictureUrl = s.PictureUrl ?? "/images/noimage.png",
                                                        DisplayOrder = s.DisplayOrder,
                                                        CompanyId = companyId
                                                    }).ToListAsync();
            result.SubCategory.AddRange(subCategories);

            var query = await (from P in _db.Product
                                join I in _db.Inventory on P.Id equals I.ProductId
                                where P.CategoryId == id
                                && !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId
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
                result.Product.Add(new VmProduct
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
            result.Product = result.Product.DistinctBy(x => new { x.ProductId, x.Code, x.Price }).ToList();
            return result;
        }
        [HttpPost("GetWeeklyBestProduct")]
        public async Task<List<VmProduct>> GetWeeklyBestProduct(VmFilterData vm)
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
            var result = new List<VmProduct>();
            var tillDate = DateTime.UtcNow.AddDays(-7);
            var weeklySale = await _db.ItemSalesHistory.Where(x => x.CreatedDate > tillDate).OrderByDescending(x => x.Quantity).Select(s => s.ItemId).Distinct().Take(10).ToListAsync();
            foreach (var item in weeklySale)
            {
                result = await (from P in _db.Product
                                join I in _db.Inventory on P.Id equals I.ProductId
                                where !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId && I.Id == item &&
                                (vm.BrandIds == "" || bids.Contains(P.ProductBrandId.ToString()))   //CONFUSED ----
                                && (vm.CategoryIds == "" || cids.Contains(P.CategoryId.ToString()))  //CONFUSED ----
                                && (vm.MinPrice == 0 || P.Price > vm.MinPrice) //CONFUSED ----
                                && (vm.MaxPrice == 0 || P.Price < vm.MaxPrice) //CONFUSED ----HOW IT WORKS
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
                                }).OrderBy(o => o.Code).ToListAsync();
            }
            return result.DistinctBy(x => new { x.ProductId, x.Code, x.Price }).ToList();
        }

        [HttpGet("SearchProduct")]
        public async Task<List<VmProduct>> SearchProduct(string key,string appId)
        {
            var result = new List<VmProduct>();
            var query = await (from P in _db.Product
                                join I in _db.Inventory.Where(x => !x.Deleted) on P.Id equals I.ProductId
                                where !I.Deleted && !P.Deleted && I.CompanyId == companyId && P.CompanyId == companyId
                                select new VmProduct
                                {
                                    ProductId = P.Id,
                                    ItemId = I.Id,
                                    Barcode = I.Barcode,
                                    Name = P.Name + (I.VariantName??""),
                                    Code = P.Code,
                                    ProductImageUrl = P.ProductImageUrl ?? "/images/noimage.png",
                                    Price = I.SalePrice,
                                    CategoryId = P.CategoryId,
                                    IsFeatured = P.IsFeatured
                                }).Where(x=>x.Name.ToLower().Contains(key.ToLower())).ToListAsync();
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
            return result.DistinctBy(x => new { x.ProductId, x.Code, x.Price }).ToList();
        }
        
    }
}
