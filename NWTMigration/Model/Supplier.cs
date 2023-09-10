﻿using NWTMigration.ViewModel.Validacoes;
using System;
using System.Collections.Generic;

namespace NWTMigration.Model;

public partial class Supplier: ValidacaoPropriedadeViewModel
{
    public virtual int SupplierId { get; set; }

    public virtual string CompanyName { get; set; } = null!;

    public virtual string? ContactName { get; set; }

    public virtual string? ContactTitle { get; set; }

    public virtual string? Address { get; set; }

    public virtual string? City { get; set; }

    public virtual string? Region { get; set; }

    public virtual string? PostalCode { get; set; }

    public virtual string? Country { get; set; }

    public virtual string? Phone { get; set; }

    public virtual string? Fax { get; set; }

    public virtual string? HomePage { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
