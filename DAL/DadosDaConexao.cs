using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
     public class DadosDaConexao
    {
       public static string StringDeConexao
        {
            get
            {
                return "Data Source=locahost;Initial Catalog=Estoque;Persist Security Info=True;User ID=sa;Password=123";
            }
        }
    }
}
