using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Modelo;
using System.Data;
using System.Data.SqlClient;

namespace BBL
{
   public class BBLSubCategoria
    {
        private DalConexao conexao;

        public BBLSubCategoria(DalConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloSubCategoria Modelo)
        {
            if ((Modelo.SCatNome.Trim().Length == 0) && (Modelo.CatCod<=0))
            {
                throw new Exception("Nome Descrição e Categoria  não pode estar vazio");
            }
            //Modelo.CatNome = Modelo.CatNome.ToUpper();//deixa letra toda maiuscula
            DalSubCategoria Dalobj = new DalSubCategoria(conexao);
            Dalobj.Incluir(Modelo);
        }
        public void Alterar(ModeloSubCategoria Modelo)
        {
            if ((Modelo.SCatCod <= 0) && (Modelo.SCatNome.Trim().Length == 0) && (Modelo.CatCod <=0))
            {
                throw new Exception("Necessario dados completos para alterar");
            }

            DalSubCategoria Dalobj = new DalSubCategoria(conexao);
            Dalobj.Alterar(Modelo);

        }
        public void Excluir(int codigo)
        {
            DalSubCategoria Dalobj = new DalSubCategoria(conexao);
            Dalobj.Excluir(codigo);
        }
        public DataTable localizar(String valor)
        {
            DalSubCategoria Dalobj = new DalSubCategoria(conexao);
            return Dalobj.localizar(valor);
        }
        public ModeloSubCategoria CarragaModelosubCategoria(int codigo)
        {
            DalSubCategoria Dalobj = new DalSubCategoria(conexao);
            return Dalobj.CarragaModelosubCategoria(codigo);
        }
    }
}
