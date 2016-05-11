using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//add
using Modelo;
using System.Data.SqlClient;

namespace DAL
{
    public class DalUsuario
    {
        private DalConexao conexao;

        public DalUsuario(DalConexao cx)
        {
            this.conexao = cx;
        }

        public string LoginUsuario(ModeloUsuario Modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "	SELECT COUNT(idUsuario) FROM USUARIO WHERE NOME = @nome AND SENHA = @senha";
            cmd.Parameters.AddWithValue("@nome", Modelo.nome);
            cmd.Parameters.AddWithValue("@senha", Modelo.senha);
            conexao.Conectar();
            //strin retorno = cmd.ExecuteNonQuery();

            string retorno = cmd.ExecuteReader().ToString();
            conexao.Desconecta();
            return retorno;

            /*
            ModeloCategoria modelo = new ModeloCategoria();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "select * from  categoria where cat_cod = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();*/
        }

    }
}
