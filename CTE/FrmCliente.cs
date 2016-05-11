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
using System.Windows.Forms;
using Ferramentas;

namespace CTE
{
    public partial class FrmCliente : Form
    {

        public int codigo = 0;
        public int operacao = 1;//operacao 1 = inserir, operacao 2 = alterar

        public void limpatela()
        {
            txtcod.Clear();
            txtnome.Clear();
            textBoxX1.Clear();
            txtBairro.Clear();
            txtCelular.Clear();
            txtCep.Clear();
            txtCidade.Clear();
            txtCPFCNPJ.Clear();
            txtEmail.Clear();
            txtEstado.Clear();
            txtFone.Clear();
            txtNumero.Clear();
            txtRGIE.Clear();
            txtRua.Clear();
            rbFisica.Checked = true;
            lbValorIncorreto.Visible = false;
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
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void radialMenu1_ItemClick(object sender, EventArgs e)
        {

        }
        public void btnovo_Click(object sender, EventArgs e)
        {
            this.limpatela();
            this.operacao = 1;
            superTabControl1.SelectedTabIndex = 1;
            this.altbt(2);
            txtnome.Focus();
         
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
                ModeloCliente modelo = new ModeloCliente();
                modelo.CliNome = txtnome.Text;
                modelo.CliRSocial = textBoxX1.Text;
                modelo.CliCpfCnpj = txtCPFCNPJ.Text;
                modelo.CliRgIe = txtRGIE.Text;
                modelo.CliCep = txtCep.Text;
                modelo.CliCidade = txtCidade.Text;
                modelo.CliEstado = txtEstado.Text;
                modelo.CliEndereco = txtRua.Text;
                modelo.CliEndNumero = txtNumero.Text;
                modelo.CliBairro = txtBairro.Text;
                modelo.CliEmail = txtEmail.Text;
                modelo.CliFone = txtFone.Text;
                modelo.CliCelular = txtCelular.Text;
                if (rbFisica.Checked == true)
                {
                    modelo.CliTipo = "Física"; // fisica
                    modelo.CliRSocial = "";
                }
                else
                {
                    modelo.CliTipo = "Jurídica"; // juridica
                }

                DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
                BLLCliente bll = new BLLCliente(cx);
                if (this.operacao == 1)
                {

                    bll.Incluir(modelo);
                    txtcod.Text = modelo.CliCod.ToString();
                    this.limpatela();
                    txtnome.Focus();
                    MessageBox.Show("Salvo com Sucesso", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    modelo.CliCod = Convert.ToInt32(txtcod.Text);
                    bll.Alterar(modelo);
                    this.limpatela();
                    superTabControl1.SelectedTabIndex = 0;
                    this.altbt(1);
                    dtvcli.DataSource = bll.Localizar(txtpesquisa.Text);
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
                    BLLCliente bll = new BLLCliente(cx);
                    bll.Excluir(Convert.ToInt32(txtcod.Text));
                    this.limpatela();
                    this.altbt(1);
                    superTabControl1.SelectedTabIndex = 0;
                    dtvcli.DataSource = bll.Localizar(txtpesquisa.Text);
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
            BLLCliente bll = new BLLCliente(cx);
            dtvcli.DataSource = bll.Localizar(txtpesquisa.Text);
           
            this.altbt(3);

        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                
                    codigo = Convert.ToInt32(dtvcli.Rows[e.RowIndex].Cells[1].Value);
                  
                    if (codigo != 0)
                    {
                        DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
                        BLLCliente bll = new BLLCliente(cx);
                        ModeloCliente modelo = bll.CarregaModeloCliente(codigo);
                        txtcod.Text = modelo.CliCod.ToString();

                        if (modelo.CliTipo == "Física")
                        {
                            rbFisica.Checked = true;
                        }
                        else
                        {
                            rbJuridica.Checked = true;
                        }

                        txtnome.Text = modelo.CliNome;
                        textBoxX1.Text = modelo.CliRSocial;
                        txtCPFCNPJ.Text = modelo.CliCpfCnpj;
                        txtRGIE.Text = modelo.CliRgIe;
                        txtCep.Text = modelo.CliCep;
                        txtEstado.Text = modelo.CliEstado;
                        txtCidade.Text = modelo.CliCidade;
                        txtRua.Text = modelo.CliEndereco;
                        txtNumero.Text = modelo.CliEndNumero;
                        txtBairro.Text = modelo.CliBairro;
                        txtEmail.Text = modelo.CliEmail;
                        txtFone.Text = modelo.CliFone;
                        txtCelular.Text = modelo.CliCelular;
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
            catch
            {

            }



        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    codigo = Convert.ToInt32(dtvcli.Rows[e.RowIndex].Cells[1].Value);
                    if (codigo != 0)
                    {
                        DalConexao cx = new DalConexao(DadosDaConexão.StringDeConexão);
                        BLLCliente bll = new BLLCliente(cx);
                        ModeloCliente modelo = bll.CarregaModeloCliente(codigo);
                        txtcod.Text = modelo.CliCod.ToString();

                        if (modelo.CliTipo == "Física")
                        {
                            rbFisica.Checked = true;
                        }
                        else
                        {
                            rbJuridica.Checked = true;
                        }
                        txtnome.Text = modelo.CliNome;
                        textBoxX1.Text = modelo.CliRSocial;
                        txtCPFCNPJ.Text = modelo.CliCpfCnpj;
                        txtRGIE.Text = modelo.CliRgIe;
                        txtCep.Text = modelo.CliCep;
                        txtEstado.Text = modelo.CliEstado;
                        txtCidade.Text = modelo.CliCidade;
                        txtRua.Text = modelo.CliEndereco;
                        txtNumero.Text = modelo.CliEndNumero;
                        txtBairro.Text = modelo.CliBairro;
                        txtEmail.Text = modelo.CliEmail;
                        txtFone.Text = modelo.CliFone;
                        txtCelular.Text = modelo.CliCelular;
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

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            rbFisica.Checked = true;
            textBoxX1.Visible = false;
            lbRSocial.Visible = false;
            this.altbt(1);
        }

        private void rbFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFisica.Checked == true)
            {
                lbRSocial.Visible = false;
                textBoxX1.Visible = false;
                txtnome.Visible = true;
                labelX3.Visible = true;
                lbCPFCNPJ.Text = "CPF";
                lbRGIE.Text = "RG";
            }
            else
            {
                lbRSocial.Visible = true;
                textBoxX1.Visible = true;
                lbCPFCNPJ.Text = "CNPJ";
                lbRGIE.Text = "IE";
                txtnome.Visible = false;
                labelX3.Visible = false;
            }
        }
        private void txtCep_Leave(object sender, EventArgs e)
        {
            if (Validacao.ValidaCep(txtCep.Text) == false)
            {
                MessageBox.Show("O CEP é inválido");
                txtBairro.Clear();
                txtEstado.Clear();
                txtCidade.Clear();
                txtRua.Clear();
            }
            else
            {
                if (BuscaEndereco.verificaCEP(txtCep.Text) == true)
                {
                    txtBairro.Text = BuscaEndereco.bairro;
                    txtEstado.Text = BuscaEndereco.estado;
                    txtCidade.Text = BuscaEndereco.cidade;
                    txtRua.Text = BuscaEndereco.endereco;
                }
            }

        }
        private void txtCPFCNPJ_Leave(object sender, EventArgs e)
        {
            lbValorIncorreto.Visible = false;
            if (rbFisica.Checked == true)
            {
                //cpf
                if (Validacao.IsCpf(txtCPFCNPJ.Text) == false)
                {
                    lbValorIncorreto.Visible = true;
                }
            }
            else
            {
                if (Validacao.IsCnpj(txtCPFCNPJ.Text) == false)
                {
                    lbValorIncorreto.Visible = true;
                }
            }
        }
        private void txtCPFCNPJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)8)
            {
                Campo edit = Campo.CPF;
                if (rbFisica.Checked == false) edit = Campo.CNPJ;
                Formatar(edit, txtCPFCNPJ);
            }
        }

        private void txtCep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)8)
            {
                Campo edit = Campo.CEP;
                Formatar(edit, txtCep);
            }
        }
        public void Formatar(Campo Valor, TextBox txtTexto)
        {
            switch (Valor)
            {
                case Campo.CPF:
                    txtTexto.MaxLength = 14;
                    if (txtTexto.Text.Length == 3)
                    {
                        txtTexto.Text = txtTexto.Text + ".";
                        txtTexto.SelectionStart = txtTexto.Text.Length + 1;
                    }
                    else if (txtTexto.Text.Length == 7)
                    {
                        txtTexto.Text = txtTexto.Text + ".";
                        txtTexto.SelectionStart = txtTexto.Text.Length + 1;
                    }
                    else if (txtTexto.Text.Length == 11)
                    {
                        txtTexto.Text = txtTexto.Text + "-";
                        txtTexto.SelectionStart = txtTexto.Text.Length + 1;
                    }
                    break;

                case Campo.CNPJ:
                    txtTexto.MaxLength = 18;
                    if (txtTexto.Text.Length == 2 || txtTexto.Text.Length == 6)
                    {
                        txtTexto.Text = txtTexto.Text + ".";
                        txtTexto.SelectionStart = txtTexto.Text.Length + 1;
                    }
                    else if (txtTexto.Text.Length == 10)
                    {
                        txtTexto.Text = txtTexto.Text + "/";
                        txtTexto.SelectionStart = txtTexto.Text.Length + 1;
                    }
                    else if (txtTexto.Text.Length == 15)
                    {
                        txtTexto.Text = txtTexto.Text + "-";
                        txtTexto.SelectionStart = txtTexto.Text.Length + 1;
                    }
                    break;
                case Campo.CEP:
                    txtTexto.MaxLength = 9;
                    if (txtTexto.Text.Length == 5)
                    {
                        txtTexto.Text = txtTexto.Text + "-";
                        txtTexto.SelectionStart = txtTexto.Text.Length + 1;
                    }
                    break;
            }
        }
        public enum Campo
        {
            CPF = 1,
            CNPJ = 2,
            CEP = 3,
        }


       
    }
    
}
