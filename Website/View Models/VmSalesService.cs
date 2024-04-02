namespace PosWebsite.View_Models
{
    public class VmSalesService
    {
        public int Id { get; set; }

        public int SalesId { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal Amount { get; set; }

        public decimal Vat { get; set; }

        public decimal VatAmount { get; set; }
    }
}
