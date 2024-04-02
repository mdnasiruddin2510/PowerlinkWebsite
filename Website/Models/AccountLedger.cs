using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class AccountLedger
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int AccountGroupId { get; set; }

    public string CompanyId { get; set; }

    public bool UserDefined { get; set; }
}
