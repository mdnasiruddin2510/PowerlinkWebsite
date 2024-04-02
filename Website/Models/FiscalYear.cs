﻿using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class FiscalYear
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string CompanyId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }
}