using System.Windows;

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
    }
}
