using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CTE
{
    public partial class Menu : DevComponents.DotNetBar.Metro.MetroForm
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void metroTileItem1_Click(object sender, EventArgs e)
        {
            FrmCategoria cat = new FrmCategoria();
            cat.ShowDialog();
        }

        private void metroTileItem2_Click(object sender, EventArgs e)
        {
            FrmSubCategoria scat = new FrmSubCategoria();
            scat.ShowDialog();
        }

        private void metroTileItem3_Click(object sender, EventArgs e)
        {
            FrmTipoPagamento tdp = new FrmTipoPagamento();
            tdp.ShowDialog();
        }

        private void metroTileItem4_Click(object sender, EventArgs e)
        {
            FrmMovCompras Mcp = new FrmMovCompras();
            Mcp.ShowDialog();
        }

        private void metroTileItem5_Click(object sender, EventArgs e)
        {
            FrmCliente fc = new FrmCliente();
            fc.ShowDialog();
            fc.Dispose();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}