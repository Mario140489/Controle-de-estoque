using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace DAL
{
     public class DalConexao
    {
        private String _strinConexao;
        private SqlConnection _conexao;

        public DalConexao (String dadosConexao)
        {
            this._conexao = new SqlConnection();
            this.StringConexao = dadosConexao;
            this._conexao.ConnectionString = dadosConexao;
        }

        public String StringConexao
        {
            get { return this._strinConexao; }
            set { this._strinConexao = value; }
        }

        public SqlConnection objetoConexao
        {
            get { return this._conexao; }
            set { this._conexao = value; }
        }
        public void Conectar()
        {
            this._conexao.Open();
        }
        public void Desconecta()
        {
            this._conexao.Close();
        }
    }
}
