using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class SalesPayment
{
    public int Id { get; set; }

    public int SalesId { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string Note { get; set; }
}
