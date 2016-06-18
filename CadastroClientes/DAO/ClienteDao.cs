using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadastroClientes.Model;
using MySql.Data.MySqlClient;

namespace CadastroClientes.DAO
{
    public class ClienteDao : BaseDao
    {
        private MySqlCommand ComandosCliente(string sql, Cliente cliente)
        {
            var cmd = new MySqlCommand(sql);

            cmd.Parameters.Add(new MySqlParameter("@NOME", cliente.Nome));
            cmd.Parameters.Add(new MySqlParameter("@CPF", cliente.Cpf));

            return cmd;
        }

        private MySqlCommand ComandosEndereco(string sql, Endereco endereco, string cpfCliente)
        {
            var cmd = new MySqlCommand(sql);

            cmd.Parameters.AddWithValue("@DESCRICAO", endereco.Descricao);
            cmd.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
            cmd.Parameters.AddWithValue("@NUMERO", endereco.Numero);
            cmd.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
            cmd.Parameters.AddWithValue("@BAIRRO", endereco.Bairro);
            cmd.Parameters.AddWithValue("@CIDADE", endereco.Cidade);
            cmd.Parameters.AddWithValue("@UF", endereco.Uf);
            cmd.Parameters.AddWithValue("@CEP", endereco.Cep);
            cmd.Parameters.AddWithValue("@CPFCLIENTE", cpfCliente);

            return cmd;
        }

        private MySqlCommand ComandosParamCpf(string sql, string cpf)
        {
            var cmd = new MySqlCommand(sql);

            cmd.Parameters.AddWithValue("@CPF", cpf);

            return cmd;
        }

        public void Inserir(Cliente cliente)
        {
            var sql = "INSERT INTO cliente (nome, cpf) VALUES (@NOME, @CPF)";

            ExecutarComando(ComandosCliente(sql, cliente));

            var commandEndereco = new List<MySqlCommand>();

            foreach (var endereco in cliente.Enderecos)
            {
                sql =
                    "INSERT INTO endereco (descricao, logradouro, numero, complemento, bairro, cidade, uf, cep, cpfcliente)" +
                    " VALUES (@DESCRICAO, @LOGRADOURO, @NUMERO, @COMPLEMENTO, @BAIRRO, @CIDADE, @UF, @CEP, @CPFCLIENTE)";

                commandEndereco.Add(ComandosEndereco(sql, endereco, cliente.Cpf.ToString()));
            }

            ExecutarComandos(commandEndereco);
        }

        public void Deletar(Cliente cliente)
        {
#warning VERIFICAR QUERY
            var sql = "DELETE FROM cliente as c JOIN endereco as e ON c.cpf = e.cpfcliente WHERE c.cpf = @CPF OR e.cpfcliente = @CPF";

            ExecutarComando(this.ComandosParamCpf(sql, cliente.Cpf.ToString()));
        }

        public void Editar(Cliente cliente)
        {
            var sql = "UPDATE cliente SET nome = @NOME WHERE cpf=@CPF";
            var cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("@NOME", cliente.Nome);
            cmd.Parameters.AddWithValue("@CPF", cliente.Cpf);

            var commandEndereco = new List<MySqlCommand>();

            commandEndereco.Add(cmd);
            foreach (var endereco in cliente.Enderecos)
	        {
                sql = "UPDATE endereco SET descricao = @DESCRICAO, logradouro = @LOGRADOURO, numero = @NUMERO, "+
                    "complemento = @COMPLEMENTO, bairro = @BAIRRO, cidade = @CIDADE, uf = @UF, "+
                    "cep = CEP WHERE cpfcliente = @CPFCLIENTE";
                commandEndereco.Add(ComandosEndereco(sql, endereco, cliente.Cpf.ToString()));
            }

            ExecutarComandos(commandEndereco);
        }

        public List<Cliente> BuscarClienteByNome(string nome)
        {
            var listCliente = new List<Cliente>();

            var sql = "SELECT * FROM cliente WHERE nome = @NOME";
            var cmd = new MySqlCommand(sql);
            cmd.Parameters.AddWithValue("@NOME", nome);
            var dTable = this.RetornarDataTable(cmd);
            foreach (DataRow row in dTable.Rows)
            {
                var clienteEncontrado = new Cliente
                {
                    Codigo = Convert.ToInt32(row["codigo"].ToString()),
                    Cpf = Convert.ToInt32(row["cpf"].ToString()),
                    Nome = row["nome"].ToString()
                };

                var sqlEndereco = "SELECT * FROM endereco WHERE cpfcliente = @CPF";

                var dTableEndereco = this.RetornarDataTable(this.ComandosParamCpf(sqlEndereco, clienteEncontrado.Cpf.ToString()));

                foreach (DataRow rowEndereco in dTableEndereco.Rows)
                {
                    var endereco = new Endereco
                    {
                        Bairro = rowEndereco["bairro"].ToString(),
                        Cep = rowEndereco["cep"].ToString(),
                        Cidade = rowEndereco["cidade"].ToString(),
                        Codigo = Convert.ToInt32(rowEndereco["codigo"].ToString()),
                        Complemento = rowEndereco["complemento"].ToString(),
                        Descricao = rowEndereco["descricao"].ToString(),
                        Logradouro = rowEndereco["logradouro"].ToString(),
                        Numero = rowEndereco["numero"].ToString(),
                        Uf = rowEndereco["uf"].ToString(),
                    };

                    clienteEncontrado.Enderecos.Add(endereco);
                }

                listCliente.Add(clienteEncontrado);
            }

            return listCliente;
        }
    }
}