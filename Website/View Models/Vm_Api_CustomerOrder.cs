namespace PosWebsite.View_Models
{
    public class Vm_Api_CustomerOrder
    {
        public int CustomerId { get; set; }
        public decimal GndTotal { get; set; }
        public decimal PaidAmount { get; set; }
        public string SalesNote { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalOriginalAmount { get; set; }
        public decimal TotalProductDiscount { get; set; }
        public string OrderGroup { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal DeliveryCharge {  get; set; }
        public List<VmProduct> Products { get; set; }

    }
}
