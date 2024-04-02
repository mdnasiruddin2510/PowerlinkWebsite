using System;
using System.Collections.Generic;
#nullable enable
namespace PosWebsite.Models;

public partial class ProductSetting
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public int BranchId { get; set; }

    public string? CompanyId { get; set; }
}
