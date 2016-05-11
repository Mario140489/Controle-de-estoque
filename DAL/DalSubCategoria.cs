using Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DalSubCategoria
    {
        private DalConexao conexao;

        public DalSubCategoria(DalConexao cx)
        {
            this.conexao = cx;
        }

        public void Incluir(ModeloSubCategoria Modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.objetoConexao;
                cmd.CommandText = "insert into subcategoria(cat_cod, scat_nome) values (@catcod, @nome); select @@IDENTITY;";
                cmd.Parameters.AddWithValue("@catcod", Modelo.CatCod);
                cmd.Parameters.AddWithValue("@nome", Modelo.SCatNome);
                conexao.Conectar();
                Modelo.SCatCod = Convert.ToInt32(cmd.ExecuteScalar());
                conexao.Desconecta();
            }
            catch (Exception Erro)
            {
                throw new Exception(Erro.Message);
            }
            finally
            {
                conexao.Desconecta();
            }
        }
        public void Alterar(ModeloSubCategoria Modelo)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.objetoConexao;
                cmd.CommandText = "update  subcategoria set scat_nome = @nome, cat_cod = @catcod where scat_cod = @codigo;";
                cmd.Parameters.AddWithValue("@nome", Modelo.SCatNome);
                cmd.Parameters.AddWithValue("@catcod", Modelo.CatCod);
                cmd.Parameters.AddWithValue("@codigo", Modelo.SCatCod);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception Erro)
            {
                throw new Exception(Erro.Message);
            }
            finally
            {
                conexao.Desconecta();
            }
        }
        public void Excluir(int codigo)
        {
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conexao.objetoConexao;
                cmd.CommandText = "delete from  subcategoria where scat_cod = @codigo;";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                conexao.Conectar();
                cmd.ExecuteNonQuery();
                conexao.Desconecta();
            }
            catch (Exception Erro)
            {
                throw new Exception(Erro.Message);
            }
            finally
            {
                conexao.Desconecta();
            }
        }
        public DataTable localizar(String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select Scat.scat_cod, scat.scat_nome, cat.cat_nome from subcategoria scat inner join categoria cat on(scat.cat_cod = cat.cat_cod) where scat_nome like '%" + valor + "%'", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
        public ModeloSubCategoria CarragaModelosubCategoria(int codigo)
        {
            ModeloSubCategoria modelo = new ModeloSubCategoria();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "select * from  subcategoria where scat_cod = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.SCatCod = Convert.ToInt32(registro["scat_cod"]);
                modelo.SCatNome = Convert.ToString(registro["scat_nome"]);
                modelo.CatCod = Convert.ToInt32(registro["cat_cod"]);
            }
            conexao.Desconecta();
            return modelo;

        }
    }
}
