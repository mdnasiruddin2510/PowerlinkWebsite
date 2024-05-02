using System.ComponentModel;

namespace PosWebsite.View_Models
{
    public class VmProduct
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        public int ProductId { get; set; }
        public long ItemId { get; set; }

        public string Hscode { get; set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int SpecificationId { get; set; }

        public string ProductImageUrl { get; set; }

        public decimal Price { get; set; }
        public decimal Qty { get; set; }
        public decimal Amount { get; set; }
        public decimal OriginalUnitPrice { get; set; }
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public long FreeItemId { get; set; }
        public decimal FreeQty { get; set; }

        public int CategoryId { get; set; }

        public int SubCategoryId { get; set; }

        public int ProductTypeId { get; set; }

        public int ProductBrandId { get; set; }

        public string GlobalBarcode { get; set; }

        public decimal Vat { get; set; }

        public string Weight { get; set; }

        public int WarrantyPeriod { get; set; }

        public int WarrantyPeriodDuration { get; set; }

        public string WarrantyNote { get; set; }

        public bool WarrantyShownInInvoice { get; set; }

        public int NotifiedBeforeExpired { get; set; }

        public string CompanyId { get; set; }

        public string AdditionalField1 { get; set; }

        public string AdditionalField1Value { get; set; }

        public string AdditionalField2 { get; set; }

        public string AdditionalField2Value { get; set; }

        public string AdditionalField3 { get; set; }

        public string AdditionalField3Value { get; set; }

        public string AdditionalField4 { get; set; }

        public string AdditionalField4Value { get; set; }

        public decimal Stock {  get; set; }
        public string VariantName { get; set; }
        public string ExpireDate { get; set; }
        public string Barcode { get; set; }
        public bool IsFeatured { get; set; }
        public string Brand { get; set; }
        public string category { get; set; }
        public int UnitId { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
