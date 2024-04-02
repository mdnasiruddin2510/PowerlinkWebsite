using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class AccountingTransaction
{
    public long Id { get; set; }

    public DateTime TransactionDate { get; set; }

    public int AccountGroupId { get; set; }

    public int AccountLedgerId { get; set; }

    public int AccountSubLedgerId { get; set; }

    public string Particulars { get; set; }

    public string RefNumber { get; set; }

    public decimal Dr { get; set; }

    public decimal Cr { get; set; }

    public string Remarks { get; set; }

    public string Url { get; set; }

    public string HelperId { get; set; }

    public string HelperModule { get; set; }

    public string Note { get; set; }

    public int BranchId { get; set; }

    public string CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public bool Deleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }
}
