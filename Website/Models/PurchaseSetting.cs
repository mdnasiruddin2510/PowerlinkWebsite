using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class PurchaseSetting
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

    public int BranchId { get; set; }

    public string CompanyId { get; set; }
}
