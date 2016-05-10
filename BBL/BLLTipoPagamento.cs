using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//add
using Modelo;
using DAL;
using System.Data;

namespace BBL
{
    public class BLLTipoPagamento
    {
        private DalConexao conexao;
        public BLLTipoPagamento(DalConexao cx)
        {
            this.conexao = cx;
        }
        public void Incluir(ModeloTipoPagamento modelo)
        {
            if (modelo.TpaNome.Trim().Length == 0)
            {
                throw new Exception("O tipo do pagamento é obrigatório");
            }

            DalTipoPagamento DALobj = new DalTipoPagamento(conexao);
            DALobj.Incluir(modelo);
        }
        public void Alterar(ModeloTipoPagamento modelo)
        {
            if (modelo.TpaCod <= 0)
            {
                throw new Exception("O código do tipo de pagamento é obrigatório");
            }
            if (modelo.TpaNome.Trim().Length == 0)
            {
                throw new Exception("O tipo do pagamento é obrigatório");
            }
            //modelo.CatNome = modelo.CatNome.ToUpper();

            DalTipoPagamento DALobj = new DalTipoPagamento(conexao);
            DALobj.Alterar(modelo);
        }
        public void Excluir(int codigo)
        {
            DalTipoPagamento DALobj = new DalTipoPagamento(conexao);
            DALobj.Excluir(codigo);
        }
        public DataTable Localizar(String valor)
        {
            DalTipoPagamento DALobj = new DalTipoPagamento(conexao);
            return DALobj.Localizar(valor);
        }
        public ModeloTipoPagamento CarregaModeloTipoPagamento(int codigo)
        {
            DalTipoPagamento DALobj = new DalTipoPagamento(conexao);
            return DALobj.CarregaModeloTipoPagamento(codigo);
        }
    }
}
