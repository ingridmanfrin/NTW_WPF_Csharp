using NWTMigration.ViewModel.Validacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWTMigration.Model
{
    public partial class CustomerCustomerDemo //tabela associativa entre Customer e CustomerDemographic
    {
        public virtual string CustomerTypeId { get; set; } = null!;
        public virtual string CustomerId { get; set; } = null!;
        public virtual Customer Customer { get; set; }

        public virtual CustomerDemographic CustomerDemographic { get; set; }
    }
}
