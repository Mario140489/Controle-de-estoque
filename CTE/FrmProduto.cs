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
using BBL;
using DAL;
using Modelo;

namespace CTE
{
    public partial class FrmProduto : Form
    {
        public FrmProduto()
        {
            InitializeComponent();
        }

        public int codigo = 0;
        public int operacao = 1;//operacao 1 = inserir, operacao 2 = alterar

        public void limpatela()
        {
            txtcod.Clear();
            txtNome.Clear();
            txtdesc.Clear();
            txtValorPago.Clear();
            txtValorVenda.Clear();
            txtQuantidade.Clear();
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

        //LOAD
        private void FrmProduto_Load(object sender, EventArgs e)
        {
            this.altbt(1);
            dataGridViewX1.AutoGenerateColumns = false;

            //combo da categoria
            DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
            BBLCategoria bll = new BBLCategoria(cx);
            cbCategoria.DataSource = bll.localizar("");
            cbCategoria.DisplayMember = "cat_nome";
            cbCategoria.ValueMember = "cat_cod";


            try
            {
                //combo da subcategoria
                BBLSubCategoria sbll = new BBLSubCategoria(cx);
                cbSubcategoria.DataSource = sbll.LocalizarPorCategoria((int)cbCategoria.SelectedValue);
                cbSubcategoria.DisplayMember = "scat_nome";
                cbSubcategoria.ValueMember = "scat_cod";
            }
            catch
            {
                //MessageBox.Show("Cadastre uma categoria");
            }
            //combo und medida
            BLLUnidadeMedida ubll = new BLLUnidadeMedida(cx);
            cbUnidadeMedida.DataSource = ubll.Localizar("");
            cbUnidadeMedida.DisplayMember = "umed_nome";
            cbUnidadeMedida.ValueMember = "umed_cod";
        }


        //BTN NOVO
        private void btnovo_Click(object sender, EventArgs e)
        {
            this.limpatela();
            this.operacao = 1;
            this.altbt(2);
            superTabControl1.SelectedTabIndex = 1;
            txtNome.Focus();
        }

        //BTN CANCELAR
        private void btcancelar_Click(object sender, EventArgs e)
        {
            /*
            this.limpatela();
            superTabControl1.SelectedTabIndex = 0;
            this.altbt(1);*/
            this.Close();
        }

        //BTN SALVAR
        private void btsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ModeloProduto modelo = new ModeloProduto();
                modelo.ProNome = txtNome.Text;
                modelo.ProDescricao = txtdesc.Text;
                modelo.ProValorPago = Convert.ToDouble(txtValorPago.Text);
                modelo.ProValorVenda = Convert.ToDouble(txtValorVenda.Text);
                modelo.ProQtde = Convert.ToInt32(txtQuantidade.Text);
                modelo.CatCod = Convert.ToInt32(cbCategoria.SelectedValue);
                modelo.ScatCod = Convert.ToInt32(cbSubcategoria.SelectedValue);
                modelo.UmedCod = Convert.ToInt32(cbUnidadeMedida.SelectedValue);
                DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                BLLProduto bll = new BLLProduto(cx);

                if (this.operacao == 1)
                {
                    //salvar
                    bll.Incluir(modelo);
                    txtcod.Text = modelo.ProCod.ToString();
                    this.limpatela();
                    txtdesc.Focus();
                    MessageBox.Show("Salvo com Sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    //alterar
                    modelo.ProCod = Convert.ToInt32(txtcod.Text);
                    bll.Alterar(modelo);
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

        //BTN EXCLUIR
        public void btexcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja realmente excluir essa categoria?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                    BLLProduto bll = new BLLProduto(cx);
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
                this.altbt(1);
            }
        }

        //PESQUISA
        private void buttonX1_Click(object sender, EventArgs e)
        {
            DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
            BLLProduto bll = new BLLProduto(cx);
            dataGridViewX1.DataSource = bll.Localizar(txtpesquisa.Text);
            this.altbt(3);
        }

        //
        public void acessadatagrid(object sender, DataGridViewCellEventArgs e)
        {
            codigo = Convert.ToInt32(dataGridViewX1.Rows[e.RowIndex].Cells[0].Value);

            if (codigo != 0)
            {
                DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                BLLProduto bll = new BLLProduto(cx);
                ModeloProduto modelo = bll.CarregaModeloProduto(codigo);
                txtcod.Text = modelo.ProCod.ToString();
                txtNome.Text = modelo.ProNome;
                txtdesc.Text = modelo.ProDescricao;
                txtValorPago.Text = modelo.ProValorPago.ToString();
                txtValorVenda.Text = modelo.ProValorVenda.ToString();
                txtQuantidade.Text = modelo.ProQtde.ToString();
                cbCategoria.Text = modelo.CatCod.ToString();
                cbSubcategoria.Text = modelo.ScatCod.ToString();
                cbUnidadeMedida.Text = modelo.UmedCod.ToString();

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

        //
        public void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            codigo = Convert.ToInt32(dataGridViewX1.Rows[e.RowIndex].Cells[0].Value);

            if (codigo != 0)
            {
                DalConexao cx = new DalConexao(DadosDaConexao.StringDeConexao);
                BLLProduto bll = new BLLProduto(cx);
                ModeloProduto modelo = bll.CarregaModeloProduto(codigo);
                txtcod.Text = modelo.CatCod.ToString();
                txtdesc.Text = modelo.ProNome;
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

        private void txtpesquisa_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
