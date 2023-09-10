using Microsoft.IdentityModel.Tokens;
using NWTMigration.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using MahApps.Metro.Actions;
using NWTMigration.ViewModel.Validacoes;

//trocar o nome da classe clienteViewModel para CadastroClienteViewModel
// qual a necessidade da sobreposição (override)
namespace NWTMigration.ViewModel
{

    public class CadastroClienteViewModel : Model.Customer, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; //"propriedade evento manipulador"
        public void NotifyPropertyChanged(string nomeDaPropriedade) //avisar que teve uma alteração na propriedade. Por exemplo A string "OrderId" que é umas das (string nomeDaPropriedade);
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(nomeDaPropriedade));
        }

        [Required(ErrorMessage ="Campo obrigatório")]
        [MaxLength(5, ErrorMessage = "Não pode ultrapassar 5 caracter")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Números e caracteres especiais não são permitidos no Código do Cliente.")]
        public override string?  CustomerId  { get => base.CustomerId; set { base.CustomerId = value; this.NotifyPropertyChanged("CustomerId"); } }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(40, ErrorMessage = "Excede o tamanho permitido")]
        public override string? CompanyName { get => base.CompanyName; set { base.CompanyName = value; this.NotifyPropertyChanged("CompanyName"); } }
        [MaxLength(30, ErrorMessage = "Excede o tamanho permitido")]
        public override string? ContactName { get => base.ContactName; set { base.ContactName = value; this.NotifyPropertyChanged("ContactName"); } }
        [MaxLength(30, ErrorMessage = "Excede o tamanho permitido")]
        public override string? ContactTitle { get => base.ContactTitle; set { base.ContactTitle = value; this.NotifyPropertyChanged("ContactTitle"); } }
        [MaxLength(60, ErrorMessage = "Excede o tamanho permitido")]
        public override string? Address { get => base.Address; set { base.Address = value; this.NotifyPropertyChanged("Address"); } }
        [MaxLength(15, ErrorMessage = "Excede o tamanho permitido")]
        public override string? City { get => base.City; set { base.City = value; this.NotifyPropertyChanged("City"); } }
        [MaxLength(15, ErrorMessage = "Excede o tamanho permitido")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Apenas letras!")]

        public override string? Region { get => base.Region; set { base.Region = value; this.NotifyPropertyChanged("Region"); } }
        [MaxLength(10, ErrorMessage = "Excede o tamanho permitido")]
        public override string? PostalCode { get => base.PostalCode; set { base.PostalCode = value; this.NotifyPropertyChanged("PostalCode"); } }
        [MaxLength(15, ErrorMessage = "Excede o tamanho permitido")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = " Nome de País Inválido")]
        public override string? Country { get => base.Country; set { base.Country = value; this.NotifyPropertyChanged("Country "); } }
        [MaxLength(24, ErrorMessage = "Excede o tamanho permitido")]
        [RegularExpression(@"^[0-9()\s-]+$", ErrorMessage = "Permitido apenas Numeros!")]
        public override string? Phone { get => base.Phone; set { base.Phone = value; this.NotifyPropertyChanged("Phone "); } }
        [MaxLength(24, ErrorMessage = "Excede o tamanho permitido")]
        [RegularExpression(@"^[0-9()\s-]+$", ErrorMessage = "Permitido apenas Numeros!")]
        public override string? Fax { get => base.Fax; set { base.Fax = value; this.NotifyPropertyChanged("Fax "); } }
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public override ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get => RegiaoAtuacao; set { RegiaoAtuacao = new ObservableCollection<CustomerCustomerDemo>(value); this.NotifyPropertyChanged("RegiaoAtuacao"); } }
        [ListaNaoPodeSerVazia(ErrorMessage = "Adicione ao menos uma Região.")]  
        public ObservableCollection<CustomerCustomerDemo> RegiaoAtuacao { get; set; }

        public CadastroClienteViewModel() //construtor vazio para o CREATE
        {
            ClienteNovo = true;
            RegiaoAtuacao = new ObservableCollection<CustomerCustomerDemo>();
        }
        public CadastroClienteViewModel(string customerId) //não serve para o CREATE, serve para READ UPDATE E DELITE 
        {
            using (var context = new NorthwindContext())
            {
                ClienteNovo = false;
                var customer = context.Customers.Include(c => c.CustomerCustomerDemos).ThenInclude(c => c.CustomerDemographic).Where(c => c.CustomerId == customerId).First();

                this.CustomerId = customer.CustomerId;  //inicialização das propriedades de Customer
                this.CompanyName = customer.CompanyName;
                this.ContactName = customer.ContactName;
                this.ContactTitle = customer.ContactTitle;
                this.Address = customer.Address;
                this.City = customer.City;
                this.Region = customer.Region;
                this.PostalCode = customer.PostalCode;
                this.Country = customer.Country;
                this.Phone = customer.Phone;
                this.Fax = customer.Fax;

                this.Orders = customer.Orders;
                this.CustomerCustomerDemos = customer.CustomerCustomerDemos;
            }
        }
        public bool ClienteNovo { get; set; }
        internal bool Salvar()
        {
            if (ValidarCampos() == false)
            {
                return false;
            }
            using (var context = new NorthwindContext())
            {
                if (ClienteNovo == false)
                {
                    context.Customers.Update(this);
                    List<CustomerCustomerDemo> customerCustomerDemoExcluir = context.CustomerCustomerDemos.Where(c => c.CustomerId == CustomerId).ToList();
                    context.CustomerCustomerDemos.RemoveRange(customerCustomerDemoExcluir);

                    context.SaveChanges();  
                    foreach (var regiaoAtuacao in RegiaoAtuacao)   
                    {
                        regiaoAtuacao.CustomerId = this.CustomerId;
                        regiaoAtuacao.CustomerDemographic = null;
                        regiaoAtuacao.Customer = null;
                    }
                    context.CustomerCustomerDemos.AddRange(RegiaoAtuacao);
                }
                else //create
                {
                    context.Customers.Add(this);
                    context.SaveChanges();    
                    foreach (var regiaoAtuacao in RegiaoAtuacao)
                    {
                        regiaoAtuacao.CustomerId = this.CustomerId;
                        regiaoAtuacao.CustomerDemographic = null;
                    }
                    context.CustomerCustomerDemos.AddRange(RegiaoAtuacao);
                }
                context.SaveChanges();
            }
            return true;
        }
    }
    //public bool Validar()
    //{
    //    if (CustomerId.IsNullOrEmpty())
    //    {
    //        MessageBox.Show("Preencher código do cliente");
    //        return false;
    //    }
    //    return true;
    //}
}

