using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
     public class DadosDaConexão
    {
       public static string StringDeConexão
        {
            get
            {
                return "Data Source=.\\SQLEXPRESS;Initial Catalog=Estoque;Persist Security Info=True;User ID=sa;Password=123";
            }
        }
    }
}
