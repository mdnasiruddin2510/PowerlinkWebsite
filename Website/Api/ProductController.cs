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
								   IsFeatured = _product.IsFeatured
							   }).Skip(vm.Offset).Take(vm.Limit).ToListAsync();
			return model;
		}
	}
}
