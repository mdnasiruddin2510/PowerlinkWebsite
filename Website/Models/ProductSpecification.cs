using System;
using System.Collections.Generic;
#nullable enable
namespace PosWebsite.Models;

public partial class ProductSpecification
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int GroupId { get; set; }

    public int HeadId { get; set; }

    public string? Value { get; set; }

    public string? CompanyId { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }
}
