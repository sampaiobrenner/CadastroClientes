using System.Collections.Generic;

namespace CadastroClientes.Model
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Cpf { get; set; }
        public List<Endereco> Enderecos { get; set; }
    }
}
