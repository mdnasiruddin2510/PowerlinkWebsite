using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class SupplierPrice
{
    public int Id { get; set; }

    public long ItemId { get; set; }

    public int ProductId { get; set; }

    public int Supplier { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }

    public string Reference { get; set; }

    public int PurchaseId { get; set; }

    public string CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }
}
