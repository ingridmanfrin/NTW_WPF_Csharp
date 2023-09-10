using System;
using System.Collections.Generic;

namespace NWTMigration.Model;

public partial class CustomerDemographic
{
    public virtual string CustomerTypeId { get; set; } = null!;

    public virtual string? CustomerDesc { get; set; }

    public virtual ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; } = new List<CustomerCustomerDemo>(); //CustomerCustomerDemo tabela associativa

}
