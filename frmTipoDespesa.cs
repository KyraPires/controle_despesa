using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PDESP
{
    public partial class frmTipoDespesa : Form
    {
        private BindingSource bnTipodespesa = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsTipodespesa = new DataSet();

        public frmTipoDespesa()
        {
            InitializeComponent();
        }

        private void frmTipoDespesa_Load(object sender, EventArgs e)
        {
            try
            {
                TipoDespesa Tipo = new TipoDespesa();
                dsTipodespesa.Tables.Add(Tipo.Listar());
                bnTipodespesa.DataSource = dsTipodespesa.Tables["TIPODESPESA"];
                dgvTipodespesa.DataSource = bnTipodespesa;
                bnvTipodespesa.BindingSource = bnTipodespesa;

                txtID.DataBindings.Add("TEXT", bnTipodespesa, "id_tipodespesa");
                txtTipodespesa.DataBindings.Add("TEXT", bnTipodespesa, "nome_tipodespesa");                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddtipodespesa_Click(object sender, EventArgs e)
        {
            if (tabctrlTipodespesa.SelectedIndex == 0)
            {
                tabctrlTipodespesa.SelectTab(1);
            }

            bnTipodespesa.AddNew();
            txtID.Enabled = false;
            txtTipodespesa.Enabled = true;
            txtTipodespesa.Focus();
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnAddtipodespesa.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;

            bInclusao = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tabctrlTipodespesa.SelectedIndex == 0)
            {
                tabctrlTipodespesa.SelectTab(1);
            }

            txtTipodespesa.Enabled = true;            
            txtTipodespesa.Focus();
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnAddtipodespesa.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
            bInclusao = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (txtTipodespesa.Text == "")
            {
                MessageBox.Show("Preencha o campo tipo de despesa");
            }
            else
            {
                TipoDespesa RegTipo = new TipoDespesa();

                RegTipo.Idtipodespesa = Convert.ToInt16(txtID.Text);
                RegTipo.Nometipodespesa = txtTipodespesa.Text;
                
                if (bInclusao)
                {
                    if (RegTipo.Salvar() > 0)
                    {
                        MessageBox.Show("Despesa adicionada com sucesso!");

                        btnSalvar.Enabled = false;
                        txtID.Enabled = false;
                        txtTipodespesa.Enabled = false;
                        btnSalvar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAddtipodespesa.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;

                        bInclusao = false;

                        dsTipodespesa.Tables.Clear();
                        dsTipodespesa.Tables.Add(RegTipo.Listar());
                        bnTipodespesa.DataSource = dsTipodespesa.Tables["TIPODESPESA"];
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar despesa!");
                    }
                }
                else
                {
                    if (RegTipo.Alterar() > 0)
                    {
                        MessageBox.Show("Despesa alterada com sucesso!");

                        dsTipodespesa.Tables.Clear();
                        dsTipodespesa.Tables.Add(RegTipo.Listar());
                        txtID.Enabled = false;
                        txtTipodespesa.Enabled = false;
                        btnSalvar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAddtipodespesa.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar despesa!");
                    }

                }
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (tabctrlTipodespesa.SelectedIndex == 0)
            {
                tabctrlTipodespesa.SelectTab(1);
            }


            if (MessageBox.Show("Confirma exclusão?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                TipoDespesa RegTipo = new TipoDespesa();

                RegTipo.Idtipodespesa = Convert.ToInt16(txtID.Text);
                RegTipo.Nometipodespesa = txtTipodespesa.Text;
                
                if (RegTipo.Excluir() > 0)
                {
                    MessageBox.Show("Despesa excluída com sucesso!");
                    TipoDespesa R = new TipoDespesa();
                    dsTipodespesa.Tables.Clear();
                    dsTipodespesa.Tables.Add(R.Listar());
                    bnTipodespesa.DataSource = dsTipodespesa.Tables["TIPODESPESA"];
                }
                else
                {
                    MessageBox.Show("Erro ao excluir despesa!");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bnTipodespesa.CancelEdit();

            btnSalvar.Enabled = false;
            txtTipodespesa.Enabled = false;
            btnEditar.Enabled = true;
            btnAddtipodespesa.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
