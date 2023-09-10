using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NWTMigration.Fornecedores;
using NWTMigration.Model;
using NWTMigration.View.Pedido;
using NWTMigration.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace NWTMigration
{
    public partial class ConsultaCliente : Window
    {
        public ConsultaClienteViewModel ConsultaClienteViewModel { get; set; }
        public ConsultaCliente() //Construtor
        {
            InitializeComponent();
            ConsultaClienteViewModel = new ConsultaClienteViewModel();
        }
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            var itemClicado = (Customer)dgTabelaConsultaCliente.SelectedItem;//Para pegar o a linha cliclada
            var JanelaCadastroCliente = new CadastroCliente(itemClicado.CustomerId);
            JanelaCadastroCliente.ShowDialog();
            FiltrarClientes();
        }
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
                string conteudoMessageBox = "Tem certeza de que deseja Excluir esse Cliente?";
                string tituloMessageBox = "Excluir Cliente";

                if (MessageBox.Show(conteudoMessageBox,
                       tituloMessageBox,
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
                var itemClicado = (Customer)dgTabelaConsultaCliente.SelectedItem;
                ConsultaClienteViewModel.ExcluirCliente(itemClicado);
                MessageBox.Show("Cliente Removido!");
        }
        private void btnFiltrarConsultaCliente_Click(object sender, RoutedEventArgs e)
        {
            FiltrarClientes();
        }
        private void FiltrarClientes()
        {
            ConsultaClienteViewModel.ObterClientes(txbConsultaCodigoCliente.Text);
            dgTabelaConsultaCliente.ItemsSource = ConsultaClienteViewModel.Clientes;
        }
    }
}
