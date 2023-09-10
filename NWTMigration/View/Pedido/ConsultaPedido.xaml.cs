using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NWTMigration.Fornecedores;
using NWTMigration.Model;
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

//excluir e criar está funcinando pois o ObservableCollection já funciona nestes dois, mas não funciona no atualizar, neste caso precisa implementar o NotifyPropretyChange

namespace NWTMigration.View.Pedido
{
    public partial class ConsultarPedido : Window
    {
        private ConsultaPedidoViewModel ConsultaPedidoViewModel { get; set; }

        public ConsultarPedido()
        {
            InitializeComponent();
            ConsultaPedidoViewModel = new ConsultaPedidoViewModel();
            DataContext = ConsultaPedidoViewModel;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            var itemClicado = (Order)dgTabelaConsultaPedido.SelectedItem;
            var JanelaLancamentoPedido = new LancamentoPedido(itemClicado.OrderId);
            JanelaLancamentoPedido.ShowDialog();

            FiltrarPedidos();
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            string conteudoMessageBox = "Deseja Excluir esse Pedido?";
            string tituloMessageBox = "Excluir Pedido";

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }

            var itemClicado = (Order)dgTabelaConsultaPedido.SelectedItem;

            ConsultaPedidoViewModel.ExcluirPedido(itemClicado);

            MessageBox.Show("Pedido Removido!");

        }

        private void btnFiltrarConsultaPedido_Click(object sender, RoutedEventArgs e)
        {
            FiltrarPedidos();
        }

        private void FiltrarPedidos()
        {
            if (cbxCliente.SelectedItem != null)
            {
                ConsultaPedidoViewModel.ObterPedidosCliente((cbxCliente.SelectedItem as Customer).CustomerId, datePickerDataPedido.SelectedDate);
                dgTabelaConsultaPedido.ItemsSource = ConsultaPedidoViewModel.Pedidos;
            }
            else
            {
                MessageBox.Show("Selecione um Cliente");
            }
        }

    }
}


