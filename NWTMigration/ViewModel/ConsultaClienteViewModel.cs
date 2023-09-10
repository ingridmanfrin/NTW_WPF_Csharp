using Microsoft.EntityFrameworkCore;
using Microsoft.Xaml.Behaviors.Media;
using NWTMigration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NWTMigration.ViewModel
{
    public class ConsultaClienteViewModel
    {
        public ObservableCollection<Customer> Clientes { get; set; }
        public ConsultaClienteViewModel() //construtor vazio para o CREATE
        {

        }
        public void ObterClientes(string CustomerId) 
        {
            using (var context = new NorthwindContext())
            {
                List<Customer> clientes = context.Customers.Include(c => c.CustomerCustomerDemos).Where(c=> c.CustomerId == CustomerId).ToList();
                Clientes = new ObservableCollection<Customer>(clientes.ToList());
            }
        }
        public void ExcluirCliente(Customer customer) // (tste, vancn) nao consegue ser excluido devido a ter pedidos lançados
        {
            using (var context = new NorthwindContext())
            {
                Customer clientes = context.Customers .Where(c => c.CustomerId == customer.CustomerId).First();
                List<CustomerCustomerDemo> regioes = context.CustomerCustomerDemos .Where(r => r.CustomerId == customer.CustomerId).ToList();  //peguei do bc CustomerCustomerDemos
                context.CustomerCustomerDemos.RemoveRange(regioes);  //remove em CustomerCustomerDemos 
                context.Remove(clientes);
                context.SaveChanges();
                Clientes.Remove(customer);
            }
        }
    }
}
