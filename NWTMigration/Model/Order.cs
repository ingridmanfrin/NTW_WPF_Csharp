using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NWTMigration.ViewModel.Validacoes;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NWTMigration.Model;

public partial class Order: ValidacaoPropriedadeViewModel
{
    private decimal ObterValorTotalDoPedido()
    {
        decimal somaValorTotal = 0;
        foreach (OrderDetail OrderDetail in OrderDetails)
        {
            somaValorTotal += OrderDetail.ObterValorTotalDoProduto();
        }
        return somaValorTotal + Freight.Value;
    }

    public decimal ValorTotalDoPedido => ObterValorTotalDoPedido(); 

    public virtual int OrderId { get; set; }   //transformei em virtual para permitir ser feito override na ViewModel. Motivo: para conseguir fazer o NotifyPropertyChanged

    public virtual string? CustomerId { get; set; }

    public virtual int? EmployeeId { get; set; }

    public virtual DateTime? OrderDate { get; set; }

    public virtual DateTime? RequiredDate { get; set; }

    public virtual DateTime? ShippedDate { get; set; }

    public virtual int? ShipVia { get; set; }

    public virtual decimal? Freight { get; set; }

    public virtual string? ShipName { get; set; }

    public virtual string? ShipAddress { get; set; }

    public virtual string? ShipCity { get; set; }

    public virtual string? ShipRegion { get; set; }

    public virtual string? ShipPostalCode { get; set; }

    public virtual string? ShipCountry { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Shipper? ShipViaNavigation { get; set; }
}
