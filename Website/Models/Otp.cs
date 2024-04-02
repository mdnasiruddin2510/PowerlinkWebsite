namespace PosWebsite.Models
{
    public partial class Otp
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int Code { get; set; }
        public bool Used { get; set; }
        public int SentCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UsedDate { get; set; }
        public string CompanyId { get; set; }
    }
}
