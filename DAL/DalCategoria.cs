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
    public class DalCategoria
    {
        private DalConexao conexao;

        public DalCategoria(DalConexao cx)
        {
            this.conexao = cx;
        }
     
        public void Incluir(ModeloCategoria Modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "insert into categoria(cat_nome) values (@nome); select @@IDENTITY;";
            cmd.Parameters.AddWithValue("@nome", Modelo.CatNome);
            conexao.Conectar();
            Modelo.CatCod = Convert.ToInt32(cmd.ExecuteScalar());
            conexao.Desconecta();
        }
        public void Alterar(ModeloCategoria Modelo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "update  categoria set cat_nome =  @nome where cat_cod = @codigo;";
            cmd.Parameters.AddWithValue("@nome", Modelo.CatNome);
            cmd.Parameters.AddWithValue("@codigo", Modelo.CatCod);
            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconecta();
        }
        public void Excluir(int codigo)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "delete from  categoria where cat_cod = @codigo;";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();
            cmd.ExecuteNonQuery();
            conexao.Desconecta();
        }
        public DataTable localizar(String valor)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from categoria where cat_nome like '%" + valor + "%'", conexao.StringConexao);
            da.Fill(tabela);
            return tabela;
        }
       public ModeloCategoria CarragaModeloCategoria (int codigo)
        {
            ModeloCategoria modelo = new ModeloCategoria();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conexao.objetoConexao;
            cmd.CommandText = "select * from  categoria where cat_cod = @codigo";
            cmd.Parameters.AddWithValue("@codigo", codigo);
            conexao.Conectar();
            SqlDataReader registro = cmd.ExecuteReader();
            if (registro.HasRows)
            {
                registro.Read();
                modelo.CatCod = Convert.ToInt32(registro["cat_cod"]);
                modelo.CatNome = Convert.ToString(registro["cat_nome"]);
            }
            conexao.Desconecta();
            return modelo;

        }
        public DataTable localizarsubcat (int codigo)
        {
            DataTable tabelasubcat = new DataTable();
            SqlDataAdapter dasub = new SqlDataAdapter("select scat.scat_nome from categoria c inner join subcategoria scat on (c.cat_cod = scat.cat_cod) where c.cat_cod =" + codigo , conexao.StringConexao);
            dasub.Fill(tabelasubcat);
            return tabelasubcat;
            
        }
    }
}
