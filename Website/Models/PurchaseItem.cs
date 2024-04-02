using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class PurchaseItem
{
    public long Id { get; set; }

    public int PurchaseId { get; set; }

    public long InventoryId { get; set; }

    public int ProductId { get; set; }

    public string ItemCode { get; set; }

    public string ItemBarcode { get; set; }

    public string ItemName { get; set; }

    public int UnitId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal OriginalUnitPrice { get; set; }

    public decimal Quantity { get; set; }

    public decimal PurchaseRate { get; set; }

    public decimal TotalPurchase { get; set; }

    public DateTime? ReturnDate { get; set; }

    public decimal ReturnQuantity { get; set; }

    public bool IsReturned { get; set; }

    public string Comments { get; set; }

    public string Note { get; set; }
}
