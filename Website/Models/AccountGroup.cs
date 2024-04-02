using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class AccountGroup
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int AccountType { get; set; }

    public string CompanyId { get; set; }
}
