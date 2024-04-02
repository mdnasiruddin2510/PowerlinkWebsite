﻿using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class PurchasePayment
{
    public int Id { get; set; }

    public int PurchaseId { get; set; }

    public string PaymentMethod { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string Note { get; set; }
}