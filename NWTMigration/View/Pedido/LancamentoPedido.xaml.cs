using ControlzEx.Standard;
using Microsoft.IdentityModel.Tokens;
using NWTMigration.Model;
using NWTMigration.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace NWTMigration.View.Pedido
{
    public partial class LancamentoPedido : Window
    {
        public LancamentoPedidoViewModel pedido { get; set; }

        public LancamentoPedido(int idPedido)
        {
            InitializeComponent();
            pedido = new LancamentoPedidoViewModel(idPedido);
            DataContext = pedido;
        }

        public LancamentoPedido()
        {
            InitializeComponent();

            pedido = new LancamentoPedidoViewModel();
            DataContext = pedido;
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e) //botão de dentro da datagrid de Produtos
        {
            string conteudoMessageBox = "Deseja Excluir esse Produto?";
            string tituloMessageBox = "Excluir Produto";

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            var itemClicado = (OrderDetail)dgTabelaLancamentoProduto.SelectedItem;

            pedido.OrderDetails.Remove(itemClicado);
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string conteudoMessageBox = "Deseja Salvar este Lançamento de Pedido?";
            string tituloMessageBox = "Salvar Lançamento de Pedido";

            if (pedido.OrderId != 0)
            {
                conteudoMessageBox = "Deseja Salvar a Atualização dos Dados de Pedidos?";
                tituloMessageBox = "Atualizar Lançamento de Pedido";
            }

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var salvar = pedido.Salvar();

                if (salvar == true)
                {
                    MessageBox.Show("Pedido Salvo com Sucesso!");
                    this.Close();
                }
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            string conteudoMessageBox = "Deseja Cancelar este Lançamento de Pedido?";
            string tituloMessageBox = "Cancelar Lançamento de Pedido";

            if (pedido.OrderId != 0)
            {
                conteudoMessageBox = "Deseja Cancelar a Atualização dos Dados de Pedidos?";
                tituloMessageBox = "Atualizar Lançamento de Pedido";
            }

            if (MessageBox.Show(conteudoMessageBox,
                   tituloMessageBox,
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void btnAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (cbxNomeProduto.SelectedItem != null && txtQuantidadeProduto.Text != null && txtDescontoProduto.Text != null)
            {
                var item = new OrderDetail();
                item.Product = (cbxNomeProduto.SelectedItem as Product);
                item.ProductId = (cbxNomeProduto.SelectedItem as Product).ProductId;
                item.UnitPrice = item.Product.UnitPrice.Value;

                bool produtoJaSelecionado = pedido.OrderDetails.Any(p => p.ProductId == item.ProductId);

                var deuCertoQ = short.TryParse(s: txtQuantidadeProduto.Text, out short quantidadeProduto);
                item.Quantity = quantidadeProduto;
                var deuCertoD = float.TryParse(s: txtDescontoProduto.Text, out float descontoProduto);
                item.Discount = descontoProduto;

                var textoMessageBox = "";
                
                if (produtoJaSelecionado)
                {
                    textoMessageBox += ("Esse Produto já foi selecionado");
                }
                if (deuCertoQ != true || item.Quantity <= 0)
                {
                    textoMessageBox += ("\nQuantidade do produto deve ser maior que 0");
                }
                if (!(deuCertoD == true && item.Discount >= 0 && item.Discount <= 1))
                {
                    textoMessageBox += ("\nDesconto do produto deve ser um valor entre 0 e 1");
                }
                if (textoMessageBox.IsNullOrEmpty()) //se não houver mensagem de erro é pq não tem erro
                {
                    pedido.DetalhePedido.Add(item);
                }
                else
                {
                    MessageBox.Show(textoMessageBox);
                }
            }
            else
            {
                MessageBox.Show("Preencha os campos de Produto antes de clicar no botão Adicionar Produto");
            }
        }
    }
}
