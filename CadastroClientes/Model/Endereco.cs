namespace CadastroClientes.Model
{
    public class Endereco
    {
        public Cliente Cliente { get; set; }
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }

        public override string ToString()
        {
            return this.Descricao;
        }
    }
}
