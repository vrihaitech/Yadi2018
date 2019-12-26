using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;
using OMControls;

namespace Yadi.Master
{
    public partial class SmsTemplate : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMBarcodeTemplate dbMBarcodeTemplate = new DBMBarcodeTemplate();
        MBarcodeTemplate mBarcodeTemplate = new MBarcodeTemplate();
        long pkSrNo = 0;
        public static string DefaultScript = "";

        public SmsTemplate()
        {
            InitializeComponent();
        }

        private void BarcodeTemplateAE_Load(object sender, EventArgs e)
        {
            try
            {
                FillControl();
                DataTable dtHelp = new DataTable();
                dtHelp.Columns.Add("Name");
                dtHelp.Columns.Add("Value");

                AddData(dtHelp, "CustomerName", "VarCustomerName");
                AddData(dtHelp, "Bill No", "VarBillNo");
                AddData(dtHelp, "Bill Date", "VarBillDate");
                AddData(dtHelp, "Bill Amount", "VarBillAmount");

                dgHelp.DataSource = dtHelp.DefaultView;
                this.dgHelp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void AddData(DataTable dt, string value, string DispName)
        {
            DataRow dr = dt.NewRow();
            dr[0] = value;
            dr[1] = DispName;
            dt.Rows.Add(dr);
        }

        public void FillControl()
        {
            try
            {
                string script = ObjQry.ReturnString("select ScriptData from MSmsTemplate", CommonFunctions.ConStr);
                pkSrNo = ObjQry.ReturnLong("select PkSrNo from MSmsTemplate", CommonFunctions.ConStr);

                txtScript.Text = script;
                DefaultScript = mBarcodeTemplate.DefaultScript;
                BtnSave.Enabled = true;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void Clrscr()
        {
            try
            {

                txtScript.Text = "";
                DefaultScript = "";

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clrscr();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtScript.Text != "")
                {
                    dbMBarcodeTemplate = new DBMBarcodeTemplate();
                    mBarcodeTemplate = new MBarcodeTemplate();
                    mBarcodeTemplate.ScriptData = txtScript.Text;
                    mBarcodeTemplate.UserID = DBGetVal.UserID;
                    mBarcodeTemplate.UserDate = DBGetVal.ServerTime;
                    mBarcodeTemplate.CompanyNo = DBGetVal.FirmNo;
                    mBarcodeTemplate.PkSrNo = pkSrNo;

                    if (dbMBarcodeTemplate.AddMSmsTemplate(mBarcodeTemplate) == true)
                    {
                        OMMessageBox.Show("Sms Template Updated Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        Clrscr();
                        FillControl();
                    }
                    else
                    {
                        OMMessageBox.Show("Sms Template Updated not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                else
                {
                    txtScript.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void chkReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkReadOnly.Checked == true)
                {
                    chkReadOnly.Text = "Edit On";
                    txtScript.ReadOnly = false;
                }
                else
                {
                    chkReadOnly.Text = "Read Only";
                    txtScript.ReadOnly = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgHelp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string text = Convert.ToString(dgHelp.Rows[dgHelp.CurrentCell.RowIndex].Cells[1].Value);
            String smstext = "<" + text + ">";
            txtScript.Text += smstext;
        }
    }
}
