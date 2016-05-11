using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//add
using Modelo;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DalUnidadeMedida
    {
        //
        private DalConexao conexao;

        //
        public DalUnidadeMedida(DalConexao cx)
        {
            this.conexao = cx;
        }

        //
        public void Incluir(ModeloUnidadeMedida modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.objetoConexao;
                cmd.CommandText = "insert into undmedida(umed_nome) values (@nome); select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@nome", modelo.UmedNome);
                conexao.Conectar();
                modelo.UmedCod = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconecta();
            }

        }

        //
        public void Alterar(ModeloUnidadeMedida modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.objetoConexao;
                cmd.CommandText = "update undmedida set umed_nome = @nome where umed_cod = @cod;";
                cmd.Parameters.AddWithValue("@nome", modelo.UmedNome);
                cmd.Parameters.AddWithValue("@cod", modelo.UmedCod);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconecta();
            }

        }

        //
        public void Excluir(int codigo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.objetoConexao;
                cmd.CommandText = "delete from undmedida where umed_cod = @codigo;";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
            finally
            {
                conexao.Desconecta();
            }

        }

        //
        public DataTable Localizar(String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from undmedida where umed_nome like '%" +
                valor + "%'", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }

        //
        public int VerificaUnidadeDeMedida(String valor) //0 - não existe || numero > 0 existe
        {
            int r = 0; //0 - não existe
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "select * from undmedida where umed_nome = @nome";
            cmd.Parameters.AddWithValue("@nome", valor);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                r = Convert.ToInt32(registro["umed_cod"]);
            }
            conexao.Desconecta();
            return r;
        }

        //
        public ModeloUnidadeMedida CarregaModeloUnidadeDeMedida(int codigo)
        {
            ModeloUnidadeMedida modelo = new ModeloUnidadeMedida();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "select * from undmedida where umed_cod = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.UmedCod = Convert.ToInt32(registro["umed_cod"]);
                modelo.UmedNome = Convert.ToString(registro["umed_nome"]);
            }
            conexao.Desconecta();
            return modelo;
        }


        public ModeloUnidadeMedida CarregaModeloUnidadeMedida(int codigo)
        {
            throw new NotImplementedException();
        }

        public int VerificaUnidadeMedida(string valor)
        {
            throw new NotImplementedException();
        }
    }
}
