﻿using System;
using System.Collections.Generic;

namespace PosWebsite.Models;

public partial class Branch
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public bool Default { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }

    public string Web { get; set; }

    public string CompanyId { get; set; }

    public bool Deleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string DeletedBy { get; set; }

    public string ManagerName { get; set; }

    public string ManagerPhone { get; set; }

    public bool? Active { get; set; }
}
