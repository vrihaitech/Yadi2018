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


namespace Yadi.Display
{
    public partial class StockLedgerWise : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public static long CompNo;

        public StockLedgerWise()
        {
            InitializeComponent();
        }

        private void StockLedgerWise_Load(object sender, EventArgs e)
        {
            try
            {
                CompNo = 1;
                cmbLedgerNm.DisplayMember = "LedgerName";
                cmbLedgerNm.ValueMember = "LedgerNo";
                cmbLedgerNm.DataSource = ObjDset.FillDset("New", "SELECT LedgerNo, LedgerName FROM MLedger", CommonFunctions.ConStr).Tables[0];

                DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                DTToDate.MinDate = DTPFromDate.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbLedgerNm.Text != "")
                {
                    dsVd = ObjDset.FillDset("New", "Exec GroupWiseClosingStock " + cmbLedgerNm.SelectedValue + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    bindingSource1.DataSource = dsVd.Tables[0];
                    DataGridView1.DataSource = bindingSource1;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void cmbLedgerNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbLedgerNm, e, true);
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
