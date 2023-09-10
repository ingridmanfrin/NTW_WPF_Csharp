using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel;
using NWTMigration.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows;

namespace NWTMigration.ViewModel
{
    public class CadastroFornecedorViewModel : Model.Supplier, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged(string nomeDaPropriedade)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(nomeDaPropriedade));
        }

        //dados de persistencia
        public override int SupplierId { get => base.SupplierId; set { base.SupplierId = value; this.NotifyPropertyChanged("SupplierId"); } }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(40, ErrorMessage = "Nome da Empresa não pode ultrapassar 40 caracteres")]
        public override string? CompanyName { get => base.CompanyName; set { base.CompanyName = value; this.NotifyPropertyChanged("CompanyName"); } }
        [MaxLength(30, ErrorMessage = "Nome do Responsável não pode ultrapassar 30 caracteres")]
        public override string? ContactName { get => base.ContactName; set { base.ContactName = value; this.NotifyPropertyChanged("ContactName"); } }
        [MaxLength(30, ErrorMessage = "Cargo não pode ultrapassar 30 caracteres")]
        public override string? ContactTitle { get => base.ContactTitle; set { base.ContactTitle = value; this.NotifyPropertyChanged("ContactTitle"); } }
        [MaxLength(60, ErrorMessage = "AVENIDA/RUA/N° não pode ultrapassar 60 caracteres")]
        public override string? Address { get => base.Address; set { base.Address = value; this.NotifyPropertyChanged("Address"); } }
        [MaxLength(15, ErrorMessage = "Cidade não pode ultrapassar 15 caracteres")]
        public override string? City { get => base.City; set { base.City = value; this.NotifyPropertyChanged("City"); } }
        [MaxLength(15, ErrorMessage = "UF não pode ultrapassar 15 caracteres")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Apenas letras!")]
        public override string? Region { get => base.Region; set { base.Region = value; this.NotifyPropertyChanged("Region"); } }
        [MaxLength(10, ErrorMessage = "CEP não pode ultrapassar 10 caracteres")]
        public override string? PostalCode { get => base.PostalCode; set { base.PostalCode = value; this.NotifyPropertyChanged("PostalCode"); } }
        [MaxLength(15, ErrorMessage = "País não pode ultrapassar 15 caracteres")]
        [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Inválido")]
        public override string? Country { get => base.Country; set { base.Country = value; this.NotifyPropertyChanged("Country"); } }
        [MaxLength(24, ErrorMessage = "Telefone não pode ultrapassar 24 caracteres")]
        [RegularExpression(@"^[0-9()\s-]+$", ErrorMessage = "Permitido apenas Numeros!")]
        public override string? Phone { get => base.Phone; set { base.Phone = value; this.NotifyPropertyChanged("Phone"); } }
        [MaxLength(24, ErrorMessage = "Fax não pode ultrapassar 24 caracteres")]
        [RegularExpression(@"^[0-9()\s-]+$", ErrorMessage = "Permitido apenas Numeros!")]
        public override string? Fax { get => base.Fax; set { base.Fax = value; this.NotifyPropertyChanged("Fax"); } }
        public override string? HomePage { get => base.HomePage; set { base.HomePage = value; this.NotifyPropertyChanged("HomePage"); } }

        public CadastroFornecedorViewModel()
        {

        }

        public CadastroFornecedorViewModel(int supplierId)
        {
            using (var context = new NorthwindContext())
            {
                var order = context.Suppliers.Where(c => c.SupplierId == supplierId).First();

                this.SupplierId = order.SupplierId;
                this.CompanyName = order.CompanyName;
                this.ContactName = order.ContactName;
                this.ContactTitle = order.ContactTitle;
                this.Address = order.Address;
                this.City = order.City;
                this.Region = order.Region;
                this.PostalCode = order.PostalCode;
                this.Country = order.Country;
                this.Phone = order.Phone;
                this.Fax = order.Fax;
                this.HomePage = order.HomePage;
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
                if (SupplierId != 0)
                {
                    context.Suppliers.Update(this);
                }
                else
                {
                    context.Suppliers.Add(this);
                }
                context.SaveChanges();
            }
            return true;
        }

    }
}
