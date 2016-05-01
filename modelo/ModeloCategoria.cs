using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ModeloCategoria
    {
        private int cat_cod;

        public int CatCod
        {
            get { return this.cat_cod; }
            set { this.cat_cod = value; }
        }

        private string cat_nome;

        public string CatNome
        {
            get { return this.cat_nome; }
            set { this.cat_nome = value; }
        }
    }

    
}
