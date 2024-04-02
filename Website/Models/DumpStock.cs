using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class DumpStock
{
    public int Id { get; set; }

    public int BranchId { get; set; }

    public string CompanyId { get; set; }

    public long InventoryId { get; set; }

    public int ProductId { get; set; }

    public int UnitId { get; set; }

    public decimal Quantity { get; set; }

    public string RackNumber { get; set; }

    public decimal Price { get; set; }

    public DateTime? ExpireDate { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }

    public string Barcode { get; set; }

    public string BatchNumber { get; set; }

    public int LocationId { get; set; }

    public string SerialNumber { get; set; }

    public string ShelfNumber { get; set; }
}
