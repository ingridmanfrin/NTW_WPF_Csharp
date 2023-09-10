using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using NWTMigration.Fornecedores;
using NWTMigration.Model;
using NWTMigration.ViewModel;
using System;
using System.Collections.Generic;
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

namespace NWTMigration.Fornecedor
{
    public partial class ConsultarFornecedor : Window
    {
        public ConsultaFornecedorViewModel ConsultaFornecedorViewModel { get; set; }

        public ConsultarFornecedor()
        {
            InitializeComponent();
            ConsultaFornecedorViewModel = new ConsultaFornecedorViewModel();
            DataContext = ConsultaFornecedorViewModel;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            var itemClicado = (Supplier)dgTabelaConsultaFonecedor.SelectedItem;
            var JanelaCadastrarFornecedor = new CadastroFornecedor(itemClicado.SupplierId);
            JanelaCadastrarFornecedor.ShowDialog();

            FiltrarFornecedores();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            string conteudoMessageBox = "Deseja Excluir esse Fornecedor?";
            string tituloMessageBox = "Excluir Fornecedor";

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            var itemClicado = (Supplier)dgTabelaConsultaFonecedor.SelectedItem;
            ConsultaFornecedorViewModel.ExcluirFornecedor(itemClicado);

            MessageBox.Show("Fornecedor Removido!");
        }

        private void btnFiltrar_Click(object sender, RoutedEventArgs e)
        {
            FiltrarFornecedores();
        }

        private void FiltrarFornecedores()
        {

            if (cbxFornecedor.SelectedItem != null)
            {
                ConsultaFornecedorViewModel.ObterFornecedor((cbxFornecedor.SelectedItem as Supplier).SupplierId);
                dgTabelaConsultaFonecedor.ItemsSource = ConsultaFornecedorViewModel.Fornecedor;
            }
            else
            {
                MessageBox.Show("Selecione um Fornecedor");
            }
        }
    }
}




