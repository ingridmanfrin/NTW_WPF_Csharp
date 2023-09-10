using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NWTMigration.Model;
using NWTMigration.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;


namespace NWTMigration
{
    public partial class CadastroCliente : Window
    {
        public string itemClicado;//conferir se esta sendo usado
        private string CodigoCliente { get; set; } // conferir se esse parametro ainda esta sendo usado!!! *********************
        //public CadastroClienteViewModel itemClicado { get; set; }
        public List<CustomerDemographic> ListaRegiao { get; set; }
        public CadastroClienteViewModel cliente { get; set; }
        

        public CadastroCliente(string idCliente)
        {
            InitializeComponent();

            cbxRegiaoAtuacao.ItemsSource = ListaRegioes();
            cliente = new CadastroClienteViewModel(idCliente);
            dgTabelaRegiaoAtuacao.ItemsSource = cliente.RegiaoAtuacao;
            DataContext = cliente; ;
        }
        public CadastroCliente()
        {
            InitializeComponent();
            cbxRegiaoAtuacao.ItemsSource = ListaRegioes();
            cliente = new CadastroClienteViewModel();
            dgTabelaRegiaoAtuacao.ItemsSource = cliente.RegiaoAtuacao;
            DataContext = cliente;
        }

        private List<CustomerDemographic> ListaRegioes() //não serve para o CREATE, serve para READ UPDATE E DELITE 
        {
            var regiao = new List<CustomerDemographic>();
            using (var context = new NorthwindContext())
            {
                regiao = context.CustomerDemographics.ToList();
            }
            return regiao;
        }
        private void btnExcluir_Click(object sender, RoutedEventArgs e) //botão de dentro da datagrid de Regioes
        {
            string conteudoMessageBox = "Deseja Excluir essa Região?";
            string tituloMessageBox = "Excluir Região";

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            var itemClicado = (CustomerCustomerDemo)dgTabelaRegiaoAtuacao.SelectedItem;
            cliente.CustomerCustomerDemos.Remove(itemClicado);
        }
        private void btnSalvar_Click(object sender, RoutedEventArgs e)//FALTA INSERIR MSG DE CONCLUIDO
        {
            string conteudoMessageBox = "Deseja salvar este cadastro?";
            string tituloMessageBox = "Salvar cadastro do cliente";

            if (cliente.ClienteNovo == false)
            {
                 conteudoMessageBox = "Deseja salvar a atualização dos dados desse cliente?";
                tituloMessageBox = "Atualizar cadastro do cliente";
            }
            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var salvar = cliente.Salvar();

                if (salvar == true)
                {
                    this.Close();
                }
            }
        }
        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            string conteudoMessageBox = "Deseja cancelar este cadastro ?";//VERIFICAR A INFORMAÇÃO DA MENSAGEM.ESTA SENDO REPETIDA
            string tituloMessageBox = "Cancelar cadastro";

            if (cliente.ClienteNovo == false)
            {
                conteudoMessageBox = "Deseja realmente cancelar a atualização dos dados do cliente?";
                tituloMessageBox = "Atualizar cadastro";
            }
            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void btnAddRegiao_Click(object sender, RoutedEventArgs e)
        {

            var regiaoSelecionada = cbxRegiaoAtuacao.SelectedItem as CustomerDemographic;

            if (regiaoSelecionada != null)
            {
                // Verifica se a regiao já foi selecionada
                bool regiaoJaSelecionada = cliente.RegiaoAtuacao.Any(r => r.CustomerTypeId == regiaoSelecionada.CustomerTypeId);

                if (!regiaoJaSelecionada)
                {
                    var itemAdicionar = new CustomerCustomerDemo();

                    itemAdicionar.CustomerDemographic = regiaoSelecionada;
                    itemAdicionar.CustomerTypeId = regiaoSelecionada.CustomerTypeId;

                    cliente.RegiaoAtuacao.Add(itemAdicionar);
                }
                else
                {
                    MessageBox.Show("Essa região já existe.", "", MessageBoxButton.OK);
                }
            }
        }
    }
}

