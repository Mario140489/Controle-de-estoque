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
    public partial class FrmTipoPagamento : Form
    {
        public int codigo = 0;
        public int operacao = 1;//operacao 1 = inserir, operacao 2 = alterar

        public void limpatela()
        {
            txtcod.Clear();
            txtdesc.Clear();
        }

        public void altbt(int op)
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

        public FrmTipoPagamento()
        {
            InitializeComponent();
        }

        private void FrmTipoPagamento_Load(object sender, EventArgs e)
        {
            this.altbt(1);
            dataGridViewX1.AutoGenerateColumns = false;

        }
        public void btnovo_Click(object sender, EventArgs e)
        {
            this.limpatela();
            this.operacao = 1;
            superTabControl1.SelectedTabIndex = 1;
            this.altbt(2);
            txtdesc.Focus();
        }

        private void btcancelar_Click(object sender, EventArgs e)
        {

            this.limpatela();
            superTabControl1.SelectedTabIndex = 0;
            this.altbt(1);
        }

        private void btsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloTipoPagamento md = new ModeloTipoPagamento();
                md.TpaNome = txtdesc.Text;
                DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
                BLLTipoPagamento bll = new BLLTipoPagamento(cx);

                if (this.operacao == 1)
                {

                    bll.Incluir(md);
                    txtcod.Text = md.TpaCod.ToString();
                    md.TpaCod = Convert.ToInt32(txtcod.Text);
                    this.limpatela();
                    txtdesc.Focus();
                    MessageBox.Show("Salvo com Sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    md.TpaCod = Convert.ToInt32(txtcod.Text);
                    bll.Alterar(md);
                    this.limpatela();
                    superTabControl1.SelectedTabIndex = 0;
                    this.altbt(1);
                    dataGridViewX1.DataSource = bll.Localizar(txtpesquisa.Text);
                    MessageBox.Show("Salvo com Sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception erro)
            {
                MessageBox.Show(erro.Message);
            }

        }

        public void btexcluir_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Deseja realmente excluir essa categoria?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
                    BLLTipoPagamento bll = new BLLTipoPagamento(cx);
                    bll.Excluir(Convert.ToInt32(txtcod.Text));
                    this.limpatela();
                    this.altbt(1);
                    superTabControl1.SelectedTabIndex = 0;
                    dataGridViewX1.DataSource = bll.Localizar(txtpesquisa.Text);
                }

            }
            catch
            {
                MessageBox.Show("Deve estár selecionado o registro para pode excluilo", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

            DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
            BLLTipoPagamento bll = new BLLTipoPagamento(cx);
            dataGridViewX1.DataSource = bll.Localizar(txtpesquisa.Text);
            this.altbt(3);

        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {


            codigo = Convert.ToInt32(dataGridViewX1.Rows[e.RowIndex].Cells[0].Value);

            if (codigo != 0)
            {
                DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
                BLLTipoPagamento bll = new BLLTipoPagamento(cx);
                ModeloTipoPagamento modelo = bll.CarregaModeloTipoPagamento(codigo);
                txtcod.Text = modelo.TpaCod.ToString();
                txtdesc.Text = modelo.TpaNome;

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
            try
            {
                if (e.RowIndex != -1)
                {
                    codigo = Convert.ToInt32(dataGridViewX1.Rows[e.RowIndex].Cells[0].Value);
                    if (codigo != 0)
                    {
                        DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
                        BLLTipoPagamento bll = new BLLTipoPagamento(cx);
                        ModeloTipoPagamento modelo = bll.CarregaModeloTipoPagamento(codigo);
                        txtcod.Text = modelo.TpaCod.ToString();
                        txtdesc.Text = modelo.TpaNome;
                        this.altbt(3);
                        this.operacao = 2;
                    }
                    else
                    {

                        this.limpatela();
                        this.altbt(1);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
