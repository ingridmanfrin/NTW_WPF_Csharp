using NWTMigration.Fornecedor;
using NWTMigration.Fornecedores;
using NWTMigration.View.Pedido;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NWTMigration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MenuCadastrarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            var WindowCadastrarFornecedor = new CadastroFornecedor();
            WindowCadastrarFornecedor.ShowDialog();
        }

        private void  MenuConsultarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            var WindowConsultarFornecedor = new ConsultarFornecedor();
            WindowConsultarFornecedor.ShowDialog();
        }
        

        private void windowCadastrarCliente_Click(object sender, RoutedEventArgs e)
        {
            var windowCadastrarCliente = new CadastroCliente();
            windowCadastrarCliente.ShowDialog();

        }

        private void windowConsultarCliente_Click(object sender, RoutedEventArgs e)
        {
            var windowConsultarCliente = new ConsultaCliente();
            windowConsultarCliente.ShowDialog();

        }

        private void MenuLancamentoPedido_Click(object sender, RoutedEventArgs e)
        {
            var windowLancamentoPedido = new LancamentoPedido();
            windowLancamentoPedido.ShowDialog();

        }
        private void MenuConsultarPedido_Click(object sender, RoutedEventArgs e)
        {
            var windowConsultarPedido = new ConsultarPedido();
            windowConsultarPedido.ShowDialog();

        }

    }
}
