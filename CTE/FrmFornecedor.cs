using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//add
using Modelo;
using BBL;
using DAL;

namespace CTE
{
    public partial class FrmFornecedor : Form
    {
        public FrmFornecedor()
        {
            InitializeComponent();
        }
        public int codigo = 0;
        public int operacao = 1;//operacao 1 = inserir, operacao 2 = alterar
        
        public void limpatela()
        {
            txtCod.Clear();
            txtNome.Clear();
            txtRazaoSocial.Clear();
            txtIE.Clear();
            txtCnpj.Clear();
            txtCep.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtTelefone.Clear();
            txtTelefone.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
            txtNumero.Clear();
            txtCidade.Clear();
            txtEstado.Clear();
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

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.altbt(1);
            dataGridViewX1.AutoGenerateColumns = false;     
   
        }

        //NOVO
        private void btnovo_Click(object sender, EventArgs e)
        {
            this.limpatela();
            this.operacao = 1;
            this.altbt(2);
            superTabControl1.SelectedTabIndex = 1;
            txtNome.Focus();
        }

        //CANCELAR
        private void btcancelar_Click(object sender, EventArgs e)
        {
            this.altbt(1);
            this.limpatela();
            superTabControl1.SelectedTabIndex = 1;
            this.altbt(1);
        }

        //SALVAR
        private void btsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloFornecedor modelo = new ModeloFornecedor();
                //campos para salvar
                modelo.ForNome = txtNome.Text;
                modelo.ForRSocial = txtRazaoSocial.Text;
                modelo.ForIe = txtIE.Text;
                modelo.ForCnpj = txtCnpj.Text;
                modelo.ForCep = txtCep.Text;
                modelo.ForEndereco = txtEndereco.Text;
                modelo.ForBairro = txtBairro.Text;
                modelo.ForFone = txtTelefone.Text;
                modelo.ForCelular = txtCelular.Text;
                modelo.ForEmail = txtEmail.Text;
                modelo.ForEndNumero = txtNumero.Text;
                modelo.ForCidade = txtCidade.Text; ;
                modelo.ForEstado = txtEstado.Text;

                DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                BLLFornecedor bll = new BLLFornecedor(cx);

                if (this.operacao == 1)
                {
                    MessageBox.Show("teste1");
                    bll.Incluir(modelo);
                    txtCod.Text = modelo.ForCod.ToString();
                  
                }
                else
                {
                    MessageBox.Show("teste2");
                    modelo.ForCod = Convert.ToInt32(txtCod.Text);
                    bll.Alterar(modelo);
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
                    BLLFornecedor bll = new BLLFornecedor(cx);
                    bll.Excluir(Convert.ToInt32(txtCod.Text));
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
            BLLFornecedor bll = new BLLFornecedor(cx);
            dataGridViewX1.DataSource = bll.Localizar(txtpesquisa.Text);
            this.altbt(3);
      
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            codigo = Convert.ToInt32(dataGridViewX1.Rows[e.RowIndex].Cells[0].Value);
                
               if (codigo != 0)
               {
                    DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                    BLLFornecedor bll = new BLLFornecedor(cx);
                    ModeloFornecedor modelo = bll.CarregaModeloFornecedor(codigo);
                    txtCod.Text = modelo.ForCod.ToString();
                    txtNome.Text = modelo.ForNome;
                    txtRazaoSocial.Text = modelo.ForRSocial;
                    txtIE.Text = modelo.ForIe;
                    txtCnpj.Text = modelo.ForCnpj;
                    txtCep.Text = modelo.ForCep;
                    txtEndereco.Text = modelo.ForEndereco;
                    txtBairro.Text = modelo.ForBairro;
                    txtTelefone.Text = modelo.ForFone;
                    txtCelular.Text = modelo.ForCelular;
                    txtEmail.Text = modelo.ForEmail;
                    txtNumero.Text = modelo.ForEndNumero;
                    txtCidade.Text = modelo.ForCidade;
                    txtEstado.Text = modelo.ForEstado;

                    superTabControl1.SelectedTabIndex = 1;
                    this.altbt(3);
                    this.operacao = 2;
               }else{
                   this.limpatela();
                   this.altbt(1);
               }                 

        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
