using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class InventoryHistory
{
    public long Id { get; set; }

    public long InventoryId { get; set; }

    public int ProductId { get; set; }

    public int BranchId { get; set; }

    public string CompanyId { get; set; }

    public string Particulars { get; set; }

    public int CustomerId { get; set; }

    public bool IsSupplier { get; set; }

    public int InvoiceId { get; set; }

    public string OperationType { get; set; }

    public string Module { get; set; }

    public decimal PreviousQuantity { get; set; }

    public decimal Quantity { get; set; }

    public decimal CurrentQuantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public int LocationId { get; set; }
}
