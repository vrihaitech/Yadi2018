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
    public partial class PurchaseAPMCReport : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VoucherType;
        public int voucherno;

        public static string LedgName, RptTitle;
        public static int Type;

        public PurchaseAPMCReport()
        {
            InitializeComponent();
        }

        private void PurchaseAPMCReport_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            KeyDownFormat(this.Controls);
        }


        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;

                for (int i = 0; i < dgBroker.Rows.Count; i++)
                {
                    dgBroker.Rows[i].Cells[0].Value = chkSelectAll.Checked;
                }
                btnPrint.Focus();
            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else
                    KeyDownFormat(ctrl.Controls);
            }
        }
        #endregion


        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rdAll.Checked == true)
            {
                string[] ReportSession = new string[5];
                ReportSession[0] = DTPFromDate.Text;
                ReportSession[1] = DTToDate.Text;
                ReportSession[2] = "1";
                ReportSession[3] = "";
                ReportSession[4] = DBGetVal.FirmNo.ToString();

                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.GetAllAPMCReport(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetAllAPMCReport.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

            }
            else if (rdBrokerWise.Checked == true)
            {
                string str = "";

                for (int i = 0; i < dgBroker.RowCount; i++)
                {
                    if (Convert.ToBoolean(dgBroker.Rows[i].Cells[0].EditedFormattedValue) == true)
                    {
                        if (str == "")
                            str = dgBroker.Rows[i].Cells[1].EditedFormattedValue.ToString();
                        else
                            str += "," + dgBroker.Rows[i].Cells[1].EditedFormattedValue.ToString();
                    }
                }

                if (str != "")
                {

                    string[] ReportSession = new string[5];
                    ReportSession[0] = DTPFromDate.Text;
                    ReportSession[1] = DTToDate.Text;
                    ReportSession[2] = "2";
                    ReportSession[3] = str;
                    ReportSession[4] = DBGetVal.FirmNo.ToString();

                    Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetBrokerWiseAPMCReport(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBrokerWiseAPMCReport.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                {
                    OMMessageBox.Show("Select Atleast one Broker ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
        }

        private void DTPFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                DTToDate.Focus();
            }
        }

        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                rdAll.Focus();
            }
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }


        public void BindBroker()
        {
            string sql = " SELECT DISTINCT TVoucherEntry.BrokerNo, MLedger.LedgerName FROM TVoucherEntry INNER JOIN MLedger ON TVoucherEntry.BrokerNo = MLedger.LedgerNo " +
                         " WHERE  (TVoucherEntry.VoucherTypeCode = 9) AND (TVoucherEntry.BrokerNo <> 0) " +
                         "  AND (TVoucherEntry.VoucherDate >= '" + DTPFromDate.Text + "') AND (TVoucherEntry.VoucherDate <= '" + DTToDate.Text + "')  " +
                         " Order By  MLedger.LedgerName";

            dgBroker.DataSource = ObjFunction.GetDataView(sql).Table;
            if (dgBroker.RowCount > 0)
            {
                btnPrint.Enabled = true;
                dgBroker.CurrentCell = dgBroker.Rows[0].Cells[2];
                dgBroker.Focus();
            }
            else
                btnPrint.Enabled = false;
            
        }

        private void rdType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAll.Checked == true)
            {
                pnlBroker.Visible = false;
            }
            else if (rdBrokerWise.Checked == true)
            {
                pnlBroker.Visible = true;
                BindBroker();
            }

        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgBroker.Rows.Count; i++)
            {
                dgBroker.Rows[i].Cells[0].Value = chkSelectAll.Checked;
            }
        }

    }
}
