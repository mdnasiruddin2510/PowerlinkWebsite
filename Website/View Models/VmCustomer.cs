namespace PosWebsite.View_Models
{
    public class VmCustomer
    {
        public int Id { get; set; }
        public string? CreatedDate { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Group { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Note { get; set; }
        public string? BillingAddress { get; set; }
        public decimal Balance { get; set; }
        public string? PhotoUrl { get; set; }
        public bool Common { get; set; }
        public bool Portal { get; set; }
        public string AppId { get; set; }
        public string? By { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        public string? city { get; set; }
        public string? postalCode { get; set; }
        public string? Msg { get; set; }
    }
}
