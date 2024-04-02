namespace PosWebsite.View_Models
{
    public class VmProductCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public string PictureUrl { get; set; }

        public int DisplayOrder { get; set; }

        public bool ShowOnHomePage { get; set; }

        public string CompanyId { get; set; }
        public int CategoryId { get; set; }
        
    }
}
