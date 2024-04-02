using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class Purchase
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int CustomerId { get; set; }

    public int AccountSubLedgerId { get; set; }

    public string SupplierCode { get; set; }

    public string Name { get; set; }

    public string CompanyName { get; set; }

    public string Phone { get; set; }

    public string SupplierAddress { get; set; }

    public DateTime PurchaseDate { get; set; }

    public string PurchaseNumber { get; set; }

    public decimal TotalPurchase { get; set; }

    public decimal TotalOriginalAmount { get; set; }

    public decimal CarryingCost { get; set; }

    public bool IsIncludedCarryingCostWithTotal { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TotalDiscountPercentage { get; set; }

    public decimal TotalDiscountAmount { get; set; }

    public decimal DueAmount { get; set; }

    public decimal TotalPaidAmount { get; set; }

    public decimal ChangeAmount { get; set; }

    public string PaymentNote { get; set; }

    public string PurchaseNote { get; set; }

    public decimal VatAmount { get; set; }

    public decimal VatPercentage { get; set; }

    public bool Dues { get; set; }

    public decimal PreviousDue { get; set; }

    public decimal TotalPreviousDue { get; set; }

    public decimal ReturnAmount { get; set; }

    public string ReferenceNumber { get; set; }

    public string PurchaseGroup { get; set; }

    public decimal TotalItems { get; set; }

    public int BranchId { get; set; }

    public string CompanyId { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }
}
