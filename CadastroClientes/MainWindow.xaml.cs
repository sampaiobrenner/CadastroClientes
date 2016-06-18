using System.Collections.Generic;
using System.Linq;
using System.Windows;
using CadastroClientes.DAO;
using CadastroClientes.Model;

namespace CadastroClientes
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

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja deletar o cliente selecionado?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var cliente = listClientes.SelectedItem as Cliente;

                var deletar = new ClienteDao();
                deletar.Deletar(cliente);

                listClientes.Items.RemoveAt(listClientes.SelectedIndex);
            }
        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = listClientes.SelectedItem as Cliente;
            MessageBox.Show(string.Format("Consulta\r\n\r\nNome: {0}\r\nCPF: {1}", cliente.Nome, cliente.Cpf), "Consulta");
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            listClientes.Items.Clear();

            var listaPesquisados = new ClienteDao().BuscarClienteByNome(txtPesquisar.Text);

            if (listaPesquisados.Any())
            {
                foreach (var cliente in listaPesquisados)
                {
                    listClientes.Items.Add(cliente);
                }
            }
            else
            {
                MessageBox.Show("Nenhum cliente encontrado com os parâmetros pesquisados.", "Erro");
            }
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = listClientes.SelectedItem as Cliente;

            var cadastro = new Cadastro(cliente);
            cadastro.Show();
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var cadastro = new Cadastro();
            cadastro.Show();
        }
    }
}
