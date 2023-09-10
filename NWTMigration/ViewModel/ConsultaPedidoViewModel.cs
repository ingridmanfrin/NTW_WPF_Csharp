using Microsoft.EntityFrameworkCore;
using NWTMigration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NWTMigration.ViewModel
{
    public class ConsultaPedidoViewModel
    {
        public ObservableCollection<Order> Pedidos { get; set; }

        public List<Customer> Customers { get; set; }

        public void CarregarListas()
        {
            using (var context = new NorthwindContext())
            {
                Customers = context.Customers.ToList();
            }
        }

        public ConsultaPedidoViewModel()
        {
            CarregarListas();
        }

        public void ObterPedidosCliente(string CustomerId, DateTime? orderDate)
        {
            using (var context = new NorthwindContext())
            {
                var queryPedidos = context.Orders.Include(p => p.OrderDetails).ThenInclude(x => x.Product).Include(c => c.Employee).Include(s => s.ShipViaNavigation).Where(c => c.CustomerId == CustomerId); 

                if (orderDate != null)
                {
                    queryPedidos = queryPedidos.Where(p => p.OrderDate == orderDate.Value);
                }
                
                Pedidos = new ObservableCollection<Order>(queryPedidos.ToList());
            }
        }

        public void ExcluirPedido(Order order)
        {
            using (var context = new NorthwindContext())
            {
                Order pedidos = context.Orders.Where(o => o.OrderId == order.OrderId).First();

                List<OrderDetail> detalhesPedido = context.OrderDetails.Where(o => o.OrderId == order.OrderId).ToList();  //peguei do bcd o orderDetails 
                context.OrderDetails.RemoveRange(detalhesPedido);  //removi OrderDetails

                context.Remove(pedidos);
                context.SaveChanges();

                Pedidos.Remove(order);
            }
        }
    }


}
