using Microsoft.IdentityModel.Tokens;
using NWTMigration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using MaterialDesignThemes.Wpf;
using NWTMigration.ViewModel.Validacoes;

namespace NWTMigration.ViewModel
{
    public class LancamentoPedidoViewModel : Model.Order, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; //"propriedade evento manipulador"

        public void NotifyPropertyChanged(string nomeDaPropriedade) //avisar que teve uma alteração na propriedade. Por exemplo A string "OrderId" que é umas das (string nomeDaPropriedade);
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(nomeDaPropriedade));
        }

        public override int OrderId { get => base.OrderId; set { base.OrderId = value; this.NotifyPropertyChanged("OrderId"); } }//String "OrderId"//Override na ViewModel. Motivo: para conseguir fazer o NotifyPropertyChanged
        [Required(ErrorMessage = "Este campo é obrigatório")] // não está funcionando a mensagem em vermelho!
        public override string? CustomerId { get => base.CustomerId; set { base.CustomerId = value; this.NotifyPropertyChanged("CustomerId"); } }  //dados de persistencia
        public override int? EmployeeId { get => base.EmployeeId; set { base.EmployeeId = value; this.NotifyPropertyChanged("EmployeeId"); } }
        public override DateTime? OrderDate { get => base.OrderDate; set { base.OrderDate = value; this.NotifyPropertyChanged("OrderDate"); } }
        public override DateTime? RequiredDate { get => base.RequiredDate; set { base.RequiredDate = value; this.NotifyPropertyChanged("RequiredDate"); } }
        public override DateTime? ShippedDate { get => base.ShippedDate; set { base.ShippedDate = value; this.NotifyPropertyChanged("ShippedDate"); } }
        public override int? ShipVia { get => base.ShipVia; set { base.ShipVia = value; this.NotifyPropertyChanged("ShipVia"); } }
        public override decimal? Freight { get => base.Freight; set { base.Freight = value; this.NotifyPropertyChanged("Freight"); } }
        [MaxLength(40, ErrorMessage = "Destinatário não pode ultrapassar 40 caracteres")]
        public override string? ShipName { get => base.ShipName; set { base.ShipName = value; this.NotifyPropertyChanged("ShipName"); } }
        [MaxLength(60, ErrorMessage = "AVENIDA/RUA/N° não pode ultrapassar 60 caracteres")]
        public override string? ShipAddress { get => base.ShipAddress; set { base.ShipAddress = value; this.NotifyPropertyChanged("ShipAddress"); } }
        [MaxLength(15, ErrorMessage = "Cidade não pode ultrapassar 15 caracteres")]
        public override string? ShipCity { get => base.ShipCity; set { base.ShipCity = value; this.NotifyPropertyChanged("ShipCity"); } }
        [MaxLength(15, ErrorMessage = "UF não pode ultrapassar 15 caracteres")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Esse campo permite apenas letras!")]
        public override string? ShipRegion { get => base.ShipRegion; set { base.ShipRegion = value; this.NotifyPropertyChanged("ShipRegion"); } }
        [MaxLength(10, ErrorMessage = "CEP não pode ultrapassar 10 caracteres")]
        public override string? ShipPostalCode { get => base.ShipPostalCode; set { base.ShipPostalCode = value; this.NotifyPropertyChanged("ShipPostalCode"); } }
        [MaxLength(15, ErrorMessage = "País não pode ultrapassar 15 caracteres")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Esse campo permite apenas letras!")]
        public override string? ShipCountry { get => base.ShipCountry; set { base.ShipCountry = value; this.NotifyPropertyChanged("ShipCountry"); } }
        public override ICollection<OrderDetail> OrderDetails { get => DetalhePedido; set { DetalhePedido = new ObservableCollection<OrderDetail>(value); this.NotifyPropertyChanged("OrderDetails"); } }
        [ListaNaoPodeSerVazia(ErrorMessage = "Adicione ao menos um Produto.")]
        //[DisplayName("Teste")]
        public ObservableCollection<OrderDetail> DetalhePedido { get; set; }

        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Shipper> Shippers { get; set; }


        public void CarregarListas()
        {
            using (var context = new NorthwindContext())
            {
                Products = context.Products.ToList();
                Customers = context.Customers.ToList();
                Employees = context.Employees.ToList();
                Shippers = context.Shippers.ToList();
            }
        }

        public LancamentoPedidoViewModel() //construtor vazio para o CREATE
        {
            CarregarListas();

            DetalhePedido = new ObservableCollection<OrderDetail>();
        }

        public LancamentoPedidoViewModel(int orderId) //não serve para o CREATE, serve para READ UPDATE E DELITE 
        {
            CarregarListas();

            using (var context = new NorthwindContext())
            {
                var order = context.Orders.Include(p => p.OrderDetails).ThenInclude(x => x.Product).Where(c => c.OrderId == orderId).First();

                this.OrderId = order.OrderId;         //inicialização das propriedades de Order //factoring
                this.CustomerId = order.CustomerId;
                this.EmployeeId = order.EmployeeId;
                this.OrderDate = order.OrderDate;
                this.RequiredDate = order.RequiredDate;
                this.ShippedDate = order.ShippedDate;
                this.ShipVia = order.ShipVia;
                this.Freight = order.Freight;
                this.ShipName = order.ShipName;
                this.ShipAddress = order.ShipAddress;
                this.ShipCity = order.ShipCity;
                this.ShipRegion = order.ShipRegion;
                this.ShipPostalCode = order.ShipPostalCode;
                this.ShipCountry = order.ShipCountry;

                this.Customer = order.Customer;
                this.Employee = order.Employee;

                this.OrderDetails = order.OrderDetails;
            }
        }

        internal bool Salvar()
        {
            if (ValidarCampos() == false)
            {
                return false;
            }
            using (var context = new NorthwindContext())
            {
                if (OrderId != 0) //update
                {
                    context.Orders.Update(this);
                    List<OrderDetail> detalhesPedido = context.OrderDetails.Where(o => o.OrderId == OrderId).ToList();  //peguei do bc o orderDetails 
                    context.OrderDetails.RemoveRange(detalhesPedido);   //aqui remove a lista de detalhes de pedido no banco (assim poderei criar uma nova lista no meu UPDATE. Forma mais simples de resolver o UPDATE)

                    context.SaveChanges();  //fiz o savechanges pois antes de disso não cria um id, logo orderDetails ficaria nulo
                    foreach (var orderDetail in OrderDetails)    //aqui adicionei de novo o detalhes de pedidos, independetemente de ser um criate ou um update
                    {
                        orderDetail.OrderId = this.OrderId;
                        orderDetail.Product = null;
                        orderDetail.Order = null;
                    }
                    context.OrderDetails.AddRange(this.OrderDetails);
                }
                else //create
                {
                    context.Orders.Add(this); //adicionei Order na memoria EF
                    context.SaveChanges();    //salvei de fato essa Order da linha acima, no banco de dados. O ID só é criado quando se é salvo no banco de dados
                    foreach (var orderDetail in OrderDetails)
                    {
                        orderDetail.OrderId = this.OrderId;
                        orderDetail.Product = null;
                    }

                    context.OrderDetails.AddRange(this.OrderDetails);
                }
                context.SaveChanges();
            }
            return true;
        }


    }

}

    

