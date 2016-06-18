using CadastroClientes.Model;
using System.Windows;
using System.Linq;
using System;
using CadastroClientes.DAO;
using MySql.Data.MySqlClient;

namespace CadastroClientes
{
    /// <summary>
    /// Interaction logic for Cadastro.xaml
    /// </summary>
    public partial class Cadastro : Window
    {
        public Cliente ClienteAlterado { get; set; }

        public bool Alterado { get; set; }

        public Cadastro()
        {
            InitializeComponent();
        }

        public Cadastro(Cliente cliente)
        {
            InitializeComponent();
            this.ClienteAlterado = cliente;
            this.Alterado = true;

            txtCadastroNome.Text = ClienteAlterado.Nome;
            txtCadastroCpf.Text = ClienteAlterado.Cpf.ToString();

            txtCadastroCpf.IsEnabled = false;

            foreach (var endereco in this.ClienteAlterado.Enderecos)
            {
                listCadastroEndereco.Items.Add(endereco);
            }
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
                listCadastroEndereco.Items.Add(endereco);

            this.LimpaCamposEndereco();
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
            try
            {
                if (this.Alterado)
                {
                    this.ClienteAlterado.Nome = txtCadastroNome.Text;
                    this.ClienteAlterado.Cpf = Convert.ToInt32(txtCadastroCpf.Text);

                    this.ClienteAlterado.Enderecos.Clear();

                    foreach (var endereco in listCadastroEndereco.Items)
                        this.ClienteAlterado.Enderecos.Add(endereco as Endereco);

                    var editar = new ClienteDao();
                    editar.Editar(this.ClienteAlterado);

                    MessageBox.Show("Cliente editado com sucesso.", "Menssagem.");
                    this.Close();
                }
                else
                {
                    var novoCliente = new Cliente
                    {
                        Nome = txtCadastroNome.Text,
                        Cpf = Convert.ToInt32(txtCadastroCpf.Text),
                    };

                    foreach (var endereco in listCadastroEndereco.Items)
                        novoCliente.Enderecos.Add(endereco as Endereco);

                    var inserir = new ClienteDao();
                    inserir.Inserir(novoCliente);

                    MessageBox.Show("Cliente inserido com sucesso.", "Menssagem.");
                    this.Close();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro ao tentar inserir os dados.\r\n" + ex, "Erro");
            }
            
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
