using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string CompanyId { get; set; }

    public int AccountSubLedgerId { get; set; }

    public string LedgerNo { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public string CompanyName { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string BillingAddress { get; set; }

    public string Note { get; set; }

    public string Description { get; set; }

    public string PhotoUrl { get; set; }

    public string IdentityNumber { get; set; }

    public string IdentityPhotoUrl { get; set; }

    public int Age { get; set; }

    public string DateOfBirth { get; set; }

    public string CustomerType { get; set; }

    public bool Common { get; set; }

    public bool IsUser { get; set; }

    public string AdditionalPhoneNumber1 { get; set; }

    public string AdditionalPhoneNumber2 { get; set; }

    public string UserName { get; set; }

    public int Priority { get; set; }

    public string Reference { get; set; }

    public decimal OpeningBalance { get; set; }

    public int AreaId { get; set; }

    public bool IsImportant { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public bool? Active { get; set; }

    public int CustomerDiscountPriceGroupId { get; set; }

    public decimal DiscountQuota { get; set; }

    public decimal Point { get; set; }

    public string VatNo { get; set; }

    public string Password { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Zip { get; set; }
}
