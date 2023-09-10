using NWTMigration.Model;
using NWTMigration.ViewModel;
using System;
using System.Collections.Generic;
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

namespace NWTMigration.Fornecedores
{
    public partial class CadastroFornecedor : Window
    {
        public CadastroFornecedorViewModel fornecedor { get; set; }

        public CadastroFornecedor(int idFornecedor)
        {
            InitializeComponent();
            fornecedor = new CadastroFornecedorViewModel(idFornecedor);
            DataContext = fornecedor;
        }

        public CadastroFornecedor()
        {
            InitializeComponent();
            fornecedor = new CadastroFornecedorViewModel();
            DataContext = fornecedor;
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string conteudoMessageBox = "Deseja Salvar este Fornecedor?";
            string tituloMessageBox = "Salvar Fornecedor";

            if (fornecedor.SupplierId != 0)
            {
                conteudoMessageBox = "Deseja Atualizar este Fornecedor?";
                tituloMessageBox = "Atualizar Fornecedor";
            }

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var salvar = fornecedor.Salvar();

                if (salvar == true)
                {
                    MessageBox.Show("Fornecedor Salvo com Sucesso!");
                    this.Close();
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            string conteudoMessageBox = "Deseja Cancelar o Cadastro deste Fornecedor?";
            string tituloMessageBox = "Cancelar Cadastro";

            if (fornecedor.SupplierId != 0)
            {
                conteudoMessageBox = "Deseja Cancelar a Atualização dos dados deste Fornecedor?";
                tituloMessageBox = "Atualizar Cadastro";
            }

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

    }
}
