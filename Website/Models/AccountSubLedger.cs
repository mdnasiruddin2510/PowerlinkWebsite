using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class AccountSubLedger
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string LedgerNo { get; set; }

    public int AccountGroupId { get; set; }

    public int AccountLedgerId { get; set; }

    public string LedgerType { get; set; }

    public string CompanyId { get; set; }

    public bool UserDefined { get; set; } = false;
}
