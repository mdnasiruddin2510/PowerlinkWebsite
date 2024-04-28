namespace PosWebsite.View_Models
{
    public class VmAddItemApi
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
        public int CategoryId { get; set; }
        public string Barcode { get; set; }
        public int BrandId { get; set; }
        public int ProductTypeId { get; set; }
    }
}
