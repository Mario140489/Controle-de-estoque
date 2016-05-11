using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//add
using DAL;
using Modelo;
using System.Data;
using System.Data.SqlClient;

namespace BBL
{
    public class BLLUsuario
    {

        private DalConexao conexao;

        public BLLUsuario(DalConexao cx)
        {
            this.conexao = cx;
        }

        //se retornar 1 é que tem um usuario cadastro com o usuario e senha que foram fornecidos
        public string LoginUsuario(ModeloUsuario usuario)
        {
            DalUsuario Dalobj = new DalUsuario(conexao);
            string retorno = Dalobj.LoginUsuario(usuario);
            return retorno;
        }

    }
}
