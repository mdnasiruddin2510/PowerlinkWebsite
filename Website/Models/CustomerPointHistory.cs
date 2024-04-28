using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class CustomerPointHistory
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int SalesId { get; set; }

    public decimal Rate { get; set; }

    public decimal Total { get; set; }

    public decimal Point { get; set; }

    public string CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }
}
