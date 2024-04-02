using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class Inventory
{
    public long Id { get; set; }

    public int BranchId { get; set; }

    public int LocationId { get; set; }

    public string CompanyId { get; set; }

    public int ProductId { get; set; }

    public string VariantName { get; set; }

    public int UnitId { get; set; }

    public decimal Quantity { get; set; }

    public decimal FreeQuantityEligible { get; set; }

    public decimal FreeQuantity { get; set; }

    public string Barcode { get; set; }

    public string UniversalBarcode { get; set; }

    public DateTime? ExpireDate { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public decimal WholesalePrice { get; set; }

    public string VariantRefNumber { get; set; }

    public decimal MinStockQty { get; set; }

    public decimal MinWholesaleQty { get; set; }

    public string BatchNumber { get; set; }

    public string AdditionalField1 { get; set; }

    public string AdditionalField1Value { get; set; }

    public string AdditionalField2 { get; set; }

    public string AdditionalField2Value { get; set; }

    public string AdditionalField3 { get; set; }

    public string AdditionalField3Value { get; set; }

    public string AdditionalField4 { get; set; }

    public string AdditionalField4Value { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }

    public string ShelfNumber { get; set; }

    public string SerialNumber { get; set; }

    public int ShelfId { get; set; }

    public long FreeItemId { get; set; }
}
