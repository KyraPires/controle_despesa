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
    public partial class frmMembro : Form
    {
        private BindingSource bnMembro = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsMembro = new DataSet();
        private DataSet dsPapelmembro = new DataSet();


        public frmMembro()
        {
            InitializeComponent();
        }

        private void frmMembro_Load(object sender, EventArgs e)
        {
            try
            {
                Membro Mem = new Membro();
                dsMembro.Tables.Add(Mem.Listar());
                bnMembro.DataSource = dsMembro.Tables["MEMBRO"];
                dgvMembro.DataSource = bnMembro;
                bnvMembro.BindingSource = bnMembro;

                txtID.DataBindings.Add("TEXT", bnMembro, "id_membro");
                txtNome.DataBindings.Add("TEXT", bnMembro, "nome_membro");
                cbxFamiliar.DataBindings.Add("SelectedItem", bnMembro, "papel_membro");                                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddmembro_Click(object sender, EventArgs e)
        {
            if (tabctrlMembro.SelectedIndex == 0)
            {
                tabctrlMembro.SelectTab(1);
            }

            bnMembro.AddNew();
            txtID.Enabled = false;
            txtNome.Enabled = true;
            cbxFamiliar.Enabled = true;
            cbxFamiliar.SelectedIndex = 0;
            txtNome.Focus();
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnAddmembro.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;

            bInclusao = true; ;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (tabctrlMembro.SelectedIndex == 0)
            {
                tabctrlMembro.SelectTab(1);
            }


            if (MessageBox.Show("Confirma exclusão?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Membro RegFamiliar = new Membro();

                RegFamiliar.Nomemembro = txtNome.Text;
                RegFamiliar.Idmembro = Convert.ToInt32(txtID.Text);
                RegFamiliar.Papelmembro = cbxFamiliar.SelectedItem.ToString();


                if (RegFamiliar.Excluir() > 0)
                {
                    MessageBox.Show("Membro excluído com sucesso!");
                    Membro R = new Membro();
                    dsMembro.Tables.Clear();
                    dsMembro.Tables.Add(R.Listar());
                    bnMembro.DataSource = dsMembro.Tables["MEMBRO"];
                }
                else
                {
                    MessageBox.Show("Erro ao excluir membro!");
                }
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if ((txtNome.Text == "") && (cbxFamiliar.SelectedItem != null))
            {
                MessageBox.Show("Campo vazio!");
            }
            else
            {
                Membro RegMembro = new Membro();

                RegMembro.Idmembro = Convert.ToInt16(txtID.Text);
                RegMembro.Nomemembro = txtNome.Text;
                RegMembro.Papelmembro = cbxFamiliar.SelectedItem.ToString();

                if (bInclusao)
                {
                    if (RegMembro.Salvar() > 0)
                    {
                        MessageBox.Show("Membro adicionado com sucesso!");

                        btnSalvar.Enabled = false;
                        txtID.Enabled = false;
                        txtNome.Enabled = false;
                        cbxFamiliar.Enabled = false;
                        btnSalvar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAddmembro.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;

                        bInclusao = false;

                        dsMembro.Tables.Clear();
                        dsMembro.Tables.Add(RegMembro.Listar());
                        bnMembro.DataSource = dsMembro.Tables["MEMBRO"];
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar membro!");
                    }
                }
                else
                {
                    if (RegMembro.Alterar() > 0)
                    {
                        MessageBox.Show("Membro alterado com sucesso!");

                        dsMembro.Tables.Clear();
                        dsMembro.Tables.Add(RegMembro.Listar());
                        txtID.Enabled = false;
                        txtNome.Enabled = false;
                        cbxFamiliar.Enabled = false;
                        btnSalvar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAddmembro.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar membro!");
                    }

                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tabctrlMembro.SelectedIndex == 0)
            {
                tabctrlMembro.SelectTab(1);
            }

            txtNome.Enabled = true;
            cbxFamiliar.Enabled = true;
            txtNome.Focus();
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnAddmembro.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;
            bInclusao = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bnMembro.CancelEdit();

            btnSalvar.Enabled = false;
            txtNome.Enabled = false;
            cbxFamiliar.Enabled = false;
            btnEditar.Enabled = true;
            btnAddmembro.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
