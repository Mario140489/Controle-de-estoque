using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//add
using DAL;
using Modelo;
using System.Data;

namespace BBL
{
    public class BLLUnidadeMedida
    {
        private DalConexao conexao;
        public BLLUnidadeMedida(DalConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloUnidadeMedida modelo)
        {
            if (modelo.UmedNome.Trim().Length == 0)
            {
                throw new Exception("O nome da unidade de medida é obrigatório");
            }

            DalUnidadeMedida DALobj = new DalUnidadeMedida(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloUnidadeMedida modelo)
        {
            if (modelo.UmedCod <= 0)
            {
                throw new Exception("O código da unidade de medida é obrigatório");
            }
            if (modelo.UmedNome.Trim().Length == 0)
            {
                throw new Exception("O nome da unidade de medida é obrigatório");
            }

            DalUnidadeMedida Dalobj = new DalUnidadeMedida(conexao);
            Dalobj.Alterar(modelo);
        }
        public void Excluir(int codigo)
        {
            DalUnidadeMedida Dalobj = new DalUnidadeMedida(conexao);
            Dalobj.Excluir(codigo);
        }
        public DataTable Localizar(String valor)
        {
            DalUnidadeMedida Dalobj = new DalUnidadeMedida(conexao);
            return Dalobj.Localizar(valor);
        }
        public int VerificaUnidadeMedida(String valor) //0 - não existe || numero > 0 existe
        {
            DalUnidadeMedida Dalobj = new DalUnidadeMedida(conexao);
            return Dalobj.VerificaUnidadeMedida(valor);
        }
        public ModeloUnidadeMedida CarregaModeloUnidadeMedida(int codigo)
        {
            DalUnidadeMedida Dalobj = new DalUnidadeMedida(conexao);
            return Dalobj.CarregaModeloUnidadeMedida(codigo);
        }
    }
}
