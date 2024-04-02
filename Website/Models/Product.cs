using System;
using System.Collections.Generic;
#nullable enable
namespace PosWebsite.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Hscode { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    public int SpecificationId { get; set; }

    public string? ProductImageUrl { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int SubCategoryId { get; set; }

    public int ProductTypeId { get; set; }

    public int ProductBrandId { get; set; }

    public string? GlobalBarcode { get; set; }

    public decimal Vat { get; set; }

    public string? Weight { get; set; }

    public int WarrantyPeriod { get; set; }

    public int WarrantyPeriodDuration { get; set; }

    public string? WarrantyNote { get; set; }

    public bool WarrantyShownInInvoice { get; set; }

    public int NotifiedBeforeExpired { get; set; }

    public string? CompanyId { get; set; }

    public string? AdditionalField1 { get; set; }

    public string? AdditionalField1Value { get; set; }

    public string? AdditionalField2 { get; set; }

    public string? AdditionalField2Value { get; set; }

    public string? AdditionalField3 { get; set; }

    public string? AdditionalField3Value { get; set; }

    public string? AdditionalField4 { get; set; }

    public string? AdditionalField4Value { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }
    public bool IsFeatured { get; set; }
}
