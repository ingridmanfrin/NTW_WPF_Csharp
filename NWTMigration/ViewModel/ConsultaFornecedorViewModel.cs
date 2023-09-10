using NWTMigration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWTMigration.ViewModel
{
    public class ConsultaFornecedorViewModel
    {
        public ObservableCollection<Supplier> Fornecedor { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public void CarregarLista()
        {
            using (var context = new NorthwindContext())
            {
                Suppliers = context.Suppliers.ToList();
            }
        }

        public ConsultaFornecedorViewModel() //construtor vazio para o CREAT
        {
            CarregarLista();
        }

        public void ObterFornecedor(int supplierId)
        {
            using (var context = new NorthwindContext())
            {
                List<Supplier> fornecedor = context.Suppliers.Where(f => f.SupplierId == supplierId).ToList();

                Fornecedor = new ObservableCollection<Supplier>(fornecedor.ToList());
            }
        }

        public void ExcluirFornecedor(Supplier supplier)
        {
            using (var context = new NorthwindContext())
            {
                Supplier fornecedor = context.Suppliers.Where(o => o.SupplierId == supplier.SupplierId).First();

                context.Remove(fornecedor);
                context.SaveChanges();

                Fornecedor.Remove(supplier);
            }
        }
    }
}
