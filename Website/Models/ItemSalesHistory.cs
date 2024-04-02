using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class ItemSalesHistory
{
    public long Id { get; set; }

    public DateTime Date { get; set; }

    public int SalesId { get; set; }

    public int SubLedgerId { get; set; }

    public long ItemId { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public string CompanyId { get; set; }

    public decimal Amount { get; set; }

    public int ProductId { get; set; }

    public bool? Deleted { get; set; }

    public string DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }
}
