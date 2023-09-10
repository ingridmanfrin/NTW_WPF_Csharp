using NWTMigration.ViewModel;
using NWTMigration.ViewModel.Validacoes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NWTMigration.Model;

public partial class Customer : ValidacaoPropriedadeViewModel
{
    
    public virtual string CustomerId { get; set; } = null!;

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

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    //public virtual ICollection<CustomerDemographic> CustomerTypes { get; set; } = new List<CustomerDemographic>();   //removido Customer não tem acesso direto ao CustomerDemographic
    public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; } = new List<CustomerCustomerDemo>();  //adicionado para refazer corretamento o mapeamento do EF, CustomerCustomerDemo que está relacionado diretamente com o Customer
}
