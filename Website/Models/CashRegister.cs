using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class CashRegister
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int BranchId { get; set; }

    public int LocationId { get; set; }

    public decimal OpeningBalance { get; set; }

    public decimal ClosingBalance { get; set; }

    public int EmployeeId { get; set; }

    public string Username { get; set; }

    public string CreatedBy { get; set; }

    public bool Closed { get; set; }
}
