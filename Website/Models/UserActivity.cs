using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class UserActivity
{
    public long Id { get; set; }

    public DateTime DateTime { get; set; }

    public string UserId { get; set; }

    public string UserName { get; set; }

    public string Message { get; set; }

    public string Module { get; set; }
}
