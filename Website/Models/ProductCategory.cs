using System;
using System.Collections.Generic;
# nullable enable
namespace PosWebsite.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int ParentId { get; set; }

    public string? PictureUrl { get; set; }

    public int DisplayOrder { get; set; }

    public bool ShowOnHomePage { get; set; }

    public string? CompanyId { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }
}
