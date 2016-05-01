using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTE
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void metroTileItem1_Click(object sender, EventArgs e)
        {
            FrmCategoria cat = new FrmCategoria();
            cat.ShowDialog();
            cat.Dispose();

            /*FrmCategoria cat = null;
            cat = (FrmCategoria)this.MdiChildren.Where
                (x => x is FrmCategoria).FirstOrDefault();
            if (cat != null)
            {
                cat.BringToFront();
                metroTilePanel1.Visible = false;
                FechaMenu();
            }
            else
            {
                cat = new FrmCategoria();
                cat.MdiParent = this;
                cat.Show();
                metroTilePanel1.Visible = false;
                FechaMenu();
            }*/


        }

       

        private void Menu_Load(object sender, EventArgs e)
        {

        }

      
        public void FechaMenu()
        {
            metroTilePanel1.Visible = false;
            buttonX2.Visible = true;
        }
        public void AbreMenu()
        {
            metroTilePanel1.Visible = true;
            buttonX2.Visible = false;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            AbreMenu();
        }
    }
}
