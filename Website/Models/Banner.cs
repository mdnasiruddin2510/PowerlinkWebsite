namespace Website.Models
{
    public class Banner
    {
		public int Id { get; set; }
		public string Page { get; set; }
		public string Position { get; set; }
		public string Url { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool Deleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? DeletedDate { get; set; }
		public string DeletedBy { get; set; }

	}
}
