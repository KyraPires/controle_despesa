using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace PDESP
{
    public partial class frmPrincipal : Form
    {
        public static SqlConnection conexao;
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                conexao = new SqlConnection("Data Source=DESKTOP-52BADF3\\SERVER;Initial Catalog=PDesp;User ID=SISCOMP;Password=SISCOMP");
                conexao.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro de banco de dados" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Outros Erros: " + ex.Message);
            }
        }

        private void tipoMembroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoDespesa frmTipo = new frmTipoDespesa();
            frmTipo.MdiParent = this;
            frmTipo.WindowState = FormWindowState.Maximized;
            frmTipo.Show();
        }

        private void tipoDespesaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMembro frmMem = new frmMembro();
            frmMem.MdiParent = this;
            frmMem.WindowState = FormWindowState.Maximized;
            frmMem.Show();
        }

        private void dESPESAToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmDespesa frmDesp = new frmDespesa();
            frmDesp.MdiParent = this;
            frmDesp.WindowState = FormWindowState.Maximized;
            frmDesp.Show();
        }

        private void sOBREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSobre frmAbout = new frmSobre();
            frmAbout.Show();
        }

        private void sAIRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
