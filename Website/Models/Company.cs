using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class Company
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Slogan { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string Web { get; set; }

    public string LogoUrl { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public string TimeZoneId { get; set; }

    public string Currency { get; set; }

    public string Plan { get; set; }
}
