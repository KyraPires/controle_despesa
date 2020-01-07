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
    public partial class frmDespesa : Form
    {
        private BindingSource bnDespesa = new BindingSource();
        private bool bInclusao = false;
        private DataSet dsDespesa = new DataSet();
        private DataSet dsTipodespesa = new DataSet();
        private DataSet dsMembro = new DataSet();
       
        public frmDespesa()
        {
            InitializeComponent();
        }
        private void frmDespesa_Load(object sender, EventArgs e)
        {
            try
            {
                Despesa Desp = new Despesa();
                dsDespesa.Tables.Add(Desp.Listar());
                bnDespesa.DataSource = dsDespesa.Tables["DESPESA"];
                dgvDespesa.DataSource = bnDespesa;
                bnvDespesa.BindingSource = bnDespesa;

                txtID.DataBindings.Add("TEXT", bnDespesa, "id_despesa");
                mskbxValor.DataBindings.Add("TEXT", bnDespesa, "valor_despesa");
                mskbxData.DataBindings.Add("TEXT", bnDespesa, "Data_despesa");
                rchtxtObservacao.DataBindings.Add("TEXT", bnDespesa, "obs_despesa");

                TipoDespesa Tipo = new TipoDespesa();
                dsTipodespesa.Tables.Add(Tipo.Listar());
                cbxDespesa.DataSource = dsTipodespesa.Tables["TIPODESPESA"];              
                cbxDespesa.DisplayMember = "NOME_TIPODESPESA";
                cbxDespesa.ValueMember = "ID_TIPODESPESA";   
                cbxDespesa.DataBindings.Add("SelectedValue", bnDespesa, "TIPODESPESA_ID_TIPODESPESA");

                Membro Mem = new Membro();
                dsMembro.Tables.Add(Mem.Listar());
                cbxMembro.DataSource = dsMembro.Tables["MEMBRO"];
                cbxMembro.DisplayMember = "NOME_MEMBRO"; 
                cbxMembro.ValueMember = "ID_MEMBRO"; 
                cbxMembro.DataBindings.Add("SelectedValue", bnDespesa, "MEMBRO_ID_MEMBRO");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdddespesa_Click(object sender, EventArgs e)
        {
            if (tabctrlDespesa.SelectedIndex == 0)
            {
                tabctrlDespesa.SelectTab(1);
            }

            bnDespesa.AddNew();
            cbxMembro.Enabled = true;
            cbxMembro.SelectedIndex = 0;
            cbxDespesa.Enabled = true;
            cbxDespesa.SelectedIndex = 0;
            mskbxValor.Enabled = true;
            mskbxData.Enabled = true;
            rchtxtObservacao.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnAdddespesa.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;

            bInclusao = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (tabctrlDespesa.SelectedIndex == 0)
            {
                tabctrlDespesa.SelectTab(1);
            }

            cbxMembro.Enabled = true;            
            cbxDespesa.Enabled = true;
            mskbxValor.Enabled = true;
            mskbxData.Enabled = true;
            rchtxtObservacao.Enabled = true;
            btnSalvar.Enabled = true;
            btnEditar.Enabled = false;
            btnAdddespesa.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = true;

            bInclusao = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            bool verificar = (cbxMembro.SelectedItem != null) && (cbxDespesa.SelectedItem != null) &&
                (mskbxValor.MaskCompleted == true) && (mskbxData.MaskCompleted == true);

            if (verificar == false)
            {
                MessageBox.Show("Preencha todos os campos!");
            }
            else
            {
                Despesa RegDesp = new Despesa();

                RegDesp.Membroidmembro = Convert.ToInt32(cbxMembro.SelectedValue);
                RegDesp.Despesaiddespesa = Convert.ToInt32(cbxDespesa.SelectedValue);
                RegDesp.Valordespesa = Convert.ToDouble(mskbxValor.Text);
                RegDesp.Datadespesa = Convert.ToDateTime(mskbxData.Text);                
                RegDesp.Obsdespesa = rchtxtObservacao.Text;
                RegDesp.Iddespesa = Convert.ToInt32(txtID.Text);
               
                if (bInclusao)
                {
                    if (RegDesp.Salvar() > 0)
                    {
                        MessageBox.Show("Despesa adicionado com sucesso!");

                        cbxMembro.Enabled = false;
                        cbxDespesa.Enabled = false;
                        mskbxValor.Enabled = false;
                        mskbxData.Enabled = false;
                        rchtxtObservacao.Enabled = false;
                        btnSalvar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAdddespesa.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;                        
                        
                        bInclusao = false;
                        
                        dsDespesa.Tables.Clear();
                        dsDespesa.Tables.Add(RegDesp.Listar());
                        bnDespesa.DataSource = dsDespesa.Tables["DESPESA"];
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Despesa!");
                    }
                }
                else
                {
                    if (RegDesp.Alterar() > 0)
                    {
                        MessageBox.Show("Despesa alterado com sucesso!");

                        dsDespesa.Tables.Clear();
                        dsDespesa.Tables.Add(RegDesp.Listar());
                        cbxMembro.Enabled = false;
                        cbxDespesa.Enabled = false;
                        rchtxtObservacao.Enabled = false;
                        btnSalvar.Enabled = false;
                        btnEditar.Enabled = true;
                        btnAdddespesa.Enabled = true;
                        btnExcluir.Enabled = true;
                        btnCancelar.Enabled = false;                        
                    }
                    else
                    {
                        MessageBox.Show("Erro ao gravar Despesa!");
                    }
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (tabctrlDespesa.SelectedIndex == 0)
            {
                tabctrlDespesa.SelectTab(1);
            }

            if (MessageBox.Show("Confirma exclusão?", "Yes or No", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Despesa RegDesp = new Despesa();

                RegDesp.Membroidmembro = Convert.ToInt32(cbxMembro.SelectedValue);
                RegDesp.Despesaiddespesa = Convert.ToInt32(cbxDespesa.SelectedValue);
                RegDesp.Valordespesa = Convert.ToDouble(mskbxValor.Text);
                RegDesp.Datadespesa = Convert.ToDateTime(mskbxData.Text);
                RegDesp.Obsdespesa = rchtxtObservacao.Text;
                RegDesp.Iddespesa = Convert.ToInt32(txtID.Text);

                if (RegDesp.Excluir() > 0)
                {
                    MessageBox.Show("Despesa excluída com sucesso!");
                    Despesa R = new Despesa();
                    dsDespesa.Tables.Clear();
                    dsDespesa.Tables.Add(R.Listar());
                    bnDespesa.DataSource = dsDespesa.Tables["DESPESA"];
                }
                else
                {
                    MessageBox.Show("Erro ao excluir despesa!");
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            bnDespesa.CancelEdit();

            cbxMembro.Enabled = false;
            cbxDespesa.Enabled = false;
            rchtxtObservacao.Enabled = false;
            btnSalvar.Enabled = false;
            btnEditar.Enabled = true;
            btnAdddespesa.Enabled = true;
            btnExcluir.Enabled = true;            
        }
        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
