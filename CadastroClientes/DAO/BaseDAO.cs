using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace CadastroClientes.DAO
{
    public class BaseDao
    {
        protected static MySqlConnection Conexao;

        static BaseDao()
        {
            if (Conexao == null)
            {
                Conexao = new MySqlConnection(RetornarStringConexao());
            }
        }

        //public static void InstanciarConexao()
        //{
        //    if (conexao == null)
        //    {
        //        conexao = new MySqlConnection(RetornarStringConexao());
        //    }
        //}

        private static string RetornarStringConexao()
        {
            //Essas informações deveriam ser obtidas em um arquivo de configuração
            string server = "localhost";
            string database = "cadastroclientes";
            string uid = "root";
            string password = "root";

            string connectionString = string.Format(
                "SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", 
                server, 
                database,
                uid, 
                password);

            return connectionString;
        }

        protected void ExecutarComando(MySqlCommand cmd)
        {
            try
            {
                Conexao.Open();
                cmd.Connection = Conexao;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Apenas para lhe facilitar, se der erro vai parar aqui e você 
                //conseguirá obter informações sobre o erro nas strings abaixo.
                string mensagemErro = ex.Message;
                string sql = cmd.CommandText;

                throw ex;
            }
            finally
            {
                Conexao.Close();
            }
        }

        protected void ExecutarComandos(List<MySqlCommand> comandos)
        {
            try
            {
                Conexao.Open();

                MySqlTransaction transacao = Conexao.BeginTransaction();

                MySqlCommand cmdAtual = null;

                try
                {
                    foreach(MySqlCommand cmd in comandos)
                    {
                        cmdAtual = cmd;

                        cmd.Connection = Conexao;
                        cmd.Transaction = transacao;
                        cmd.ExecuteNonQuery();
                    }

                    transacao.Commit();
                }
                catch (Exception ex)
                {
                    //Apenas para lhe facilitar, se der erro vai parar aqui e você 
                    //conseguirá obter informações sobre o erro nas strings abaixo.
                    string mensagemErro = ex.Message;
                    string sql = cmdAtual.CommandText;

                    transacao.Rollback();

                    throw ex;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexao.Close();
            }
        }

        protected DataTable RetornarDataTable(MySqlCommand cmd)
        {
            try
            {
                Conexao.Open();

                cmd.Connection = Conexao;

                MySqlCommand cmdAtual = null;

                try
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                    DataTable tabela = new DataTable();

                    da.Fill(tabela);

                    return tabela;
                }
                catch (Exception ex)
                {
                    //Apenas para lhe facilitar, se der erro vai parar aqui e você 
                    //conseguirá obter informações sobre o erro nas strings abaixo.
                    string mensagemErro = ex.Message;
                    string sql = cmdAtual.CommandText;

                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                Conexao.Close();
            }
        }
    }
}
