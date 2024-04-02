namespace PosWebsite.View_Models
{
    public class VmOrder
    {
        public string ProductId { get; set; }
        public int CustomerId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal PriceTotal { get; set; }
    }
}

