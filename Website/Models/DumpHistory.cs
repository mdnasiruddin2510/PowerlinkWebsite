using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class DumpHistory
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public long ItemId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public string CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }
}
