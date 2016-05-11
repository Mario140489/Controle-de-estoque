using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
      public class ModeloSubCategoria
    {
        public ModeloSubCategoria()
        {
            this.SCatCod = 0;
            this.SCatNome = "";
            this.CatCod = 0;
        }

        public ModeloSubCategoria(int SCatCod, string SCatNome, int CatCod)
        {

            this.SCatCod = scat_cod;
            this.SCatNome = scat_nome;
            this.CatCod = cat_cod;
        }
        private int scat_cod;

        public int SCatCod
        {
            get { return this.scat_cod; }
            set { this.scat_cod = value; }
        }

        private string scat_nome;

        public string SCatNome
        {
            get { return this.scat_nome; }
            set { this.scat_nome = value; }
        }

        private int cat_cod;

        public int CatCod
        {
            get { return this.cat_cod; }
            set { this.cat_cod = value; }
        }
    }
}
