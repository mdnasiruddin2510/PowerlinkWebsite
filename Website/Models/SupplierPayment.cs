using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class SupplierPayment
{
    public int Id { get; set; }

    public string VoucherNo { get; set; }

    public DateTime PaymentDate { get; set; }

    public int LedgerId { get; set; }

    public decimal Amount { get; set; }

    public string Particulars { get; set; }

    public string Url { get; set; }

    public int BranchId { get; set; }

    public string CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public bool Deleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }
}
