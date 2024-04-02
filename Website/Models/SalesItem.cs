using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class SalesItem
{
    public long Id { get; set; }

    public int SalesId { get; set; }

    public long InventoryId { get; set; }

    public int ProductId { get; set; }

    public string ItemCode { get; set; }

    public string ItemBarcode { get; set; }

    public string ItemName { get; set; }

    public int UnitId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal OriginalUnitPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal Vat { get; set; }

    public decimal VatAmount { get; set; }

    public decimal Quantity { get; set; }

    public decimal PurchaseRate { get; set; }

    public decimal TotalPurchase { get; set; }

    public decimal TotalSales { get; set; }

    public DateTime? ReturnDate { get; set; }

    public decimal ReturnQuantity { get; set; }

    public bool IsReturned { get; set; }

    public string Comments { get; set; }

    public string Note { get; set; }

    public string Warranty { get; set; }

    public long FreeItemId { get; set; }

    public decimal FreeQty { get; set; }

    public string ReturnedBy { get; set; }
}
