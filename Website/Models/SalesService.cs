using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public class SalesService
{
    public int Id { get; set; }

    public int SalesId { get; set; }

    public string Name { get; set; }

    public decimal Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Amount { get; set; }

    public decimal Vat { get; set; }

    public decimal VatAmount { get; set; }
}
