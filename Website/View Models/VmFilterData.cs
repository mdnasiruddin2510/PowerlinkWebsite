namespace PosWebsite.View_Models
{
    public class VmFilterData
    {
        public string BrandIds { get; set; }
        public string CategoryIds { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int Limit {  get; set; } = 0;
        public int Offset { get; set; } = 0;
    }
}
