using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class Employee
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public int AccountSubLedgerId { get; set; }

    public string LedgerNo { get; set; }

    public string Name { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string EmployeeType { get; set; }

    public string CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public bool Active { get; set; }
}
