using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class StockIssue
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int BranchId { get; set; }

    public int LocationId { get; set; }

    public int EmployeeId { get; set; }

    public long ItemId { get; set; }

    public string Note { get; set; }

    public string CompanyId { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public bool Deleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }

    public decimal Quantity { get; set; }

    public int UnitId { get; set; }
}
