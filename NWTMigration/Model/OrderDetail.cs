using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NWTMigration.Model;

public partial class OrderDetail
{
    public decimal ObterValorTotalDoProduto()
    {
        return (UnitPrice * Quantity) * (1 - decimal.Parse(Discount.ToString()));
    }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public short Quantity { get; set; }

    public float Discount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
