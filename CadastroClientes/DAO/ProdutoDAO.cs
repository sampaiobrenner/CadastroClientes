//using System;
//using System.Collections.Generic;
//using System.Data;
//using MySql.Data.MySqlClient;

//namespace CadastroClientes.DAO
//{
//    class ProdutoDAO: BaseDao
//    {
//        public List<Produto> LocalizarPorDescricao(string descricao)
//        {
//            List<Produto> retorno = new List<Produto>();
//            Produto p;

//            string sql = "SELECT * FROM produto WHERE descricao LIKE @DESCRICAO";

//            MySqlCommand cmd = new MySqlCommand(sql);

//            cmd.Parameters.Add(new MySqlParameter("DESCRICAO", descricao + "%"));

//            DataTable dt = RetornarDataTable(cmd);

//            foreach(DataRow dr in dt.Rows)
//            {
//                p = new Produto();

//                p.Codigo = Convert.ToInt32(dr["codigo"].ToString());
//                p.Descricao = dr["descricao"].ToString();
//                p.Preco = Convert.ToDouble(dr["preco"]);

//                retorno.Add(p);
//            }

//            return retorno;
//        }

//        public void Excluir(Produto produto)
//        {
//            string sql = "DELETE FROM produto WHERE codigo=@CODIGO";

//            MySqlCommand cmd = new MySqlCommand(sql);

//            cmd.Parameters.Add(new MySqlParameter("@CODIGO", produto.Codigo));

//            ExecutarComando(cmd);
//        }

//        public void Inserir(Produto produto)
//        {
//            string sql = "INSERT INTO produto (codigo, descricao, preco)" + 
//                " VALUES (@CODIGO, @DESCRICAO, @PRECO)";

//            ExecutarComando(RetornarComando(sql, produto));
//        }

//        public void Alterar(Produto produto)
//        {
//            string sql = "UPDATE produto SET descricao=@DESCRICAO, preco=@PRECO" +
//                " WHERE codigo=@CODIGO";

//            ExecutarComando(RetornarComando(sql, produto));
//        }

//        private MySqlCommand RetornarComando(string sql, Produto produto)
//        {
//            MySqlCommand cmd = new MySqlCommand(sql);

//            cmd.Parameters.Add(new MySqlParameter("@CODIGO", produto.Codigo));
//            cmd.Parameters.Add(new MySqlParameter("@DESCRICAO", produto.Descricao));
//            cmd.Parameters.Add(new MySqlParameter("@PRECO", produto.Preco));

//            return cmd;
//        }
//    }
//}
