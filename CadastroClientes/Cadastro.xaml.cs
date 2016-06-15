using CadastroClientes.Model;
using System.Windows;
using System.Linq;
using System;

namespace CadastroClientes
{
    /// <summary>
    /// Interaction logic for Cadastro.xaml
    /// </summary>
    public partial class Cadastro : Window
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void btnLimparCampos_Click(object sender, RoutedEventArgs e)
        {
           this.LimpaCamposEndereco();
        }

        private void LimpaCamposEndereco()
        {
            txtDescricao.Text = string.Empty;
            txtLogradouro.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtCidade.Text = string.Empty;
            txtUf.Text = string.Empty;
            txtCep.Text = string.Empty;
        }

        private void btnInserir_Click(object sender, RoutedEventArgs e)
        {
            var endereco = new Endereco
            {
                Descricao = txtDescricao.Text,
                Logradouro = txtDescricao.Text,
                Numero = txtNumero.Text,
                Complemento = txtComplemento.Text,
                Bairro = txtBairro.Text,
                Cidade = txtCidade.Text,
                Uf = txtUf.Text,
                Cep = txtCep.Text
            };

            if (listCadastroEndereco.Items.Contains(endereco))            
                lblErro.Content = "Já existe um endereço na lista com este nome";            
            else            
                listCadastroEndereco.Items.Add(endereco.ToString());            
        }

        private void listCadastroEndereco_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var endereco = listCadastroEndereco.SelectedItem as Endereco;

            txtDescricao.Text = endereco.Descricao;
            txtLogradouro.Text = endereco.Logradouro;
            txtNumero.Text = endereco.Numero;
            txtComplemento.Text = endereco.Complemento;
            txtBairro.Text = endereco.Bairro;
            txtCidade.Text = endereco.Cidade;
            txtUf.Text = endereco.Uf;
            txtCep.Text = endereco.Cep;
        }

        private void btnDeletarEndereco_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja deletar o endereço selecionado?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                listCadastroEndereco.Items.RemoveAt(listCadastroEndereco.SelectedIndex);
            }
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var novoCliente = new Cliente
            {
                Nome = txtCadastroNome.Text,
                Cpf = Convert.ToInt32(txtCadastroCpf.Text),
            };

            foreach (var endereco in listCadastroEndereco.Items)
                novoCliente.Enderecos.Add(endereco as Endereco);

#warning TERMINAR ESTE METODO DE ADICIONAR ENDERECO NO BANCO
        }
    }
}
