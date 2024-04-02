using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public class Sales
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int SupplierId { get; set; }

    public int AccountSubLedgerId { get; set; }

    public string CustomerCode { get; set; }

    public string Name { get; set; }

    public string CompanyName { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public DateTime InvoiceDate { get; set; }

    public string InvoiceNumber { get; set; }

    public decimal TotalPurchase { get; set; }

    public decimal TotalProductDiscount { get; set; }

    public decimal TotalProductVat { get; set; }

    public bool IsIncludedVat { get; set; }

    public decimal SubTotal { get; set; }

    public decimal TotalOriginalAmount { get; set; }

    public decimal TotalDiscountPercentage { get; set; }

    public decimal TotalDiscountAmount { get; set; }

    public decimal DeliveryCharge { get; set; }

    public string DeliveryAddress { get; set; }

    public bool IsIncludedDeliveryChargeWithTotal { get; set; }

    public decimal TotalSales { get; set; }

    public decimal ChangeAmount { get; set; }

    public decimal DueAmount { get; set; }

    public decimal AdvanceAmount { get; set; }

    public decimal TotalPaidAmount { get; set; }

    public decimal TotalItems { get; set; }

    public string PaymentNote { get; set; }

    public string SalesNote { get; set; }

    public bool Dues { get; set; }

    public decimal PreviousDue { get; set; }

    public decimal TotalPreviousDue { get; set; }

    public decimal ReturnAmount { get; set; }

    public string SalesGroup { get; set; }

    public string ReferenceNumber { get; set; }

    public string AdditionalField1 { get; set; }

    public string AdditionalField1Value { get; set; }

    public string AdditionalField2 { get; set; }

    public string AdditionalField2Value { get; set; }

    public string AdditionalField3 { get; set; }

    public string AdditionalField3Value { get; set; }

    public string AdditionalField4 { get; set; }

    public string AdditionalField4Value { get; set; }

    public int BranchId { get; set; }

    public string CompanyId { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }

    public string VatNo { get; set; }

    public string Status { get; set; }
}
