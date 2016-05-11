using BBL;
using DAL;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CTE
{
    
    public partial class FrmCategoria : Form
    {
        public int codigo = 0;
        public int operacao = 1;//operacao 1 = inserir, operacao 2 = alterar
        
        public void limpatela()
        {
            txtcod.Clear();
            txtdesc.Clear();
        }

        public void altbt( int op)
        {
            btnovo.Enabled = false;
            btsalvar.Enabled = false;
            btcancelar.Enabled = false;
            btexcluir.Enabled = false;
            switch (op)
                {
                case 1:
            
                btnovo.Enabled = true;

                    break;
                case 2:
            
                btcancelar.Enabled = true;
                btsalvar.Enabled = true;
                    break;
                case 3:
                
                    btexcluir.Enabled = true;
                    btcancelar.Enabled = true;
                    btsalvar.Enabled = true;
                    break;
            }
            
        }

    
        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.altbt(1);
            dataGridViewX1.AutoGenerateColumns = false;
           
           
           
        }

        private void btnovo_Click(object sender, EventArgs e)
        {
            this.limpatela();
            this.operacao = 1;
            this.altbt(2);
            superTabControl1.SelectedTabIndex = 1;
            txtdesc.Focus();
        }

        private void btcancelar_Click(object sender, EventArgs e)
        {
            this.altbt(1);
            this.limpatela();
            superTabControl1.SelectedTabIndex = 1;
            this.altbt(1);
        }

        private void btsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloCategoria md = new ModeloCategoria();
                md.CatNome = txtdesc.Text;
                DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                BBLCategoria bll = new BBLCategoria(cx);

                if (this.operacao == 1)
                {
                    MessageBox.Show("teste1");
                    bll.Incluir(md);
                    txtcod.Text = md.CatCod.ToString();
                  
                }
                else
                {
                    MessageBox.Show("teste2");
                    md.CatCod = Convert.ToInt32(txtcod.Text);
                    bll.Alterar(md);
                    this.limpatela();

                }

            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }

        private void btexcluir_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (MessageBox.Show("Deseja realmente excluir essa categoria?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                  
                    DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                    BBLCategoria bll = new BBLCategoria(cx);
                    bll.Excluir(Convert.ToInt32(txtcod.Text));
                    this.limpatela();
                    this.altbt(1);
                }

            }
            catch
            {
                MessageBox.Show("impossivel excluir");
                this.altbt(1);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            
            DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
            BBLCategoria bll = new BBLCategoria(cx);
            dataGridViewX1.DataSource = bll.localizar(txtpesquisa.Text);
            this.altbt(3);
      
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
         
                codigo = Convert.ToInt32(dataGridViewX1.Rows[e.RowIndex].Cells[0].Value);
                
               if (codigo != 0)
                     {
                DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                         BBLCategoria bll = new BBLCategoria(cx);
                         ModeloCategoria modelo = bll.CarragaModeloCategoria(codigo);
                         txtcod.Text = modelo.CatCod.ToString();
                         txtdesc.Text = modelo.CatNome;
                         superTabControl1.SelectedTabIndex = 1;
                this.altbt(3);
                this.operacao = 2;
            }
                     else
            {
               
                this.limpatela();
                         this.altbt(1);
                     }
                     
            

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
