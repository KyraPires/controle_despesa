using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace PDESP
{
    class Despesa
    {
        private int iddespesa;
        private int membroidmembro;
        private int despesaiddespesa;
        private String obsdespesa;
        private Double valordespesa;
        private DateTime datadespesa;

        public int Iddespesa
        {
            get
            {
                return iddespesa;
            }
            set
            {
                iddespesa = value;
            }
        }
        public int Membroidmembro
        {
            get
            {
                return membroidmembro;
            }
            set
            {
                membroidmembro = value;
            }
        }
        public int Despesaiddespesa
        {
            get
            {
                return despesaiddespesa;
            }
            set
            {
                despesaiddespesa = value;
            }
        }
        public String Obsdespesa
        {
            get
            {
                return obsdespesa;
            }
            set
            {
                obsdespesa = value;
            }
        }

        public Double Valordespesa
        {
            get
            {
                return valordespesa;
            }
            set
            {
                valordespesa = value;
            }
        }

        public DateTime Datadespesa
        {
            get
            {
                return datadespesa;
            }
            set
            {
                datadespesa = value;
            }
        }
        public DataTable Listar()
        {
            SqlDataAdapter daDespesa;

            DataTable dtDespesa = new DataTable();
            try
            {
                daDespesa = new SqlDataAdapter("SELECT * FROM DESPESA", frmPrincipal.conexao);
                daDespesa.Fill(dtDespesa);
                daDespesa.FillSchema(dtDespesa, SchemaType.Source);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtDespesa;
        }
        public int Salvar()
        {
            int retorno = 0;
            try
            {
                SqlCommand mycommand;
                int nReg;

                mycommand = new SqlCommand("INSERT INTO DESPESA VALUES (@TIPODESPESA_ID_TIPODESPESA, @MEMBRO_ID_MEMBRO, @data_despesa, @valor_despesa, @obs_despesa)", frmPrincipal.conexao);                
                mycommand.Parameters.Add(new SqlParameter("@TIPODESPESA_ID_TIPODESPESA", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@MEMBRO_ID_MEMBRO", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@valor_despesa", SqlDbType.Float));
                mycommand.Parameters.Add(new SqlParameter("@data_despesa", SqlDbType.Date));
                mycommand.Parameters.Add(new SqlParameter("@obs_despesa", SqlDbType.VarChar));
                mycommand.Parameters["@TIPODESPESA_ID_TIPODESPESA"].Value = despesaiddespesa;
                mycommand.Parameters["@MEMBRO_ID_MEMBRO"].Value = membroidmembro;
                mycommand.Parameters["@valor_despesa"].Value = valordespesa;
                mycommand.Parameters["@data_despesa"].Value = datadespesa;
                mycommand.Parameters["@obs_despesa"].Value = obsdespesa;

                nReg = mycommand.ExecuteNonQuery();
                if (nReg > 0)
                {
                    retorno = nReg;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int Alterar()
        {
            int retorno = 0;

            try
            {
                SqlCommand mycommand;
                int nReg = 0;
                mycommand = new SqlCommand("UPDATE DESPESA SET TIPODESPESA_ID_TIPODESPESA = @TIPODESPESA_ID_TIPODESPESA, MEMBRO_ID_MEMBRO = @MEMBRO_ID_MEMBRO, valor_despesa = @valor_despesa, data_despesa = @data_despesa, obs_despesa = @obs_despesa  WHERE id_despesa = @id_despesa", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@id_despesa", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@TIPODESPESA_ID_TIPODESPESA", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@MEMBRO_ID_MEMBRO", SqlDbType.Int));
                mycommand.Parameters.Add(new SqlParameter("@valor_despesa", SqlDbType.Float));
                mycommand.Parameters.Add(new SqlParameter("@data_despesa", SqlDbType.Date));
                mycommand.Parameters.Add(new SqlParameter("@obs_despesa", SqlDbType.VarChar));
                mycommand.Parameters["@id_despesa"].Value = iddespesa;
                mycommand.Parameters["@TIPODESPESA_ID_TIPODESPESA"].Value = despesaiddespesa;
                mycommand.Parameters["@MEMBRO_ID_MEMBRO"].Value = membroidmembro;
                mycommand.Parameters["@valor_despesa"].Value = valordespesa;
                mycommand.Parameters["@data_despesa"].Value = datadespesa;
                mycommand.Parameters["@obs_despesa"].Value = obsdespesa;

                nReg = mycommand.ExecuteNonQuery();
                if (nReg > 0)
                {
                    retorno = nReg;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int Excluir()
        {
            int nReg = 0;

            try
            {
                SqlCommand mycommand;

                mycommand = new SqlCommand("DELETE FROM DESPESA WHERE id_despesa = @id_despesa", frmPrincipal.conexao);
                mycommand.Parameters.Add(new SqlParameter("@id_despesa", SqlDbType.Int));
                mycommand.Parameters["@id_despesa"].Value = iddespesa;

                nReg = mycommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nReg;
        }
    }
}

