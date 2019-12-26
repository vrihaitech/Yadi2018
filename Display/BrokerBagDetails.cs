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
    public partial class BrokerBagDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataTable dtParty = new DataTable();

        DBProgressBar PB;
        string strLedgerNo = "";

        public BrokerBagDetails()
        {
            InitializeComponent();
        }

        private void BrokerBagDetails_Load(object sender, EventArgs e)
        {
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
                if (pnlPartyDetails.Visible == true)
                {
                    chkPartySelectAll.Checked = !chkPartySelectAll.Checked;

                    for (int i = 0; i < gvParty.Rows.Count; i++)
                    {
                        gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
                    }
                    btnPrint.Focus();
                }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTToDate.Value < DTPFromDate.Value)
                {
                    OMMessageBox.Show("To Date cannot be less than From Date ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    DTPFromDate.Focus();
                    pnlPartyDetails.Visible = false;
                }
                else
                {
                    pnlPartyDetails.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = pnlPartyDetails;
                    BindGridParty();
                    strLedgerNo = "";
                    chkPartySelectAll.Checked = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridParty()
        {
            chkPartySelectAll.Checked = false;
            dtParty = new DataTable();
            string str = "SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName,'false' as chk " +
                         " FROM MLedger INNER JOIN " +
                         " TVoucherEntry ON MLedger.LedgerNo = TVoucherEntry.BrokerNo "+
                         " WHERE TVoucherEntry.VoucherTypeCode = 9 AND" +
                         "(TVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "')" +
                         " AND (TVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "')";

            dtParty = ObjFunction.GetDataView(str).Table;
            gvParty.Rows.Clear();
            for (int i = 0; i < dtParty.Rows.Count; i++)
            {
                gvParty.Rows.Add();
                for (int j = 0; j < gvParty.Columns.Count; j++)
                    gvParty.Rows[i].Cells[j].Value = dtParty.Rows[i].ItemArray[j];

            }
            gvParty.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (gvParty.Rows.Count > 0)
            {
                gvParty.Focus();
                gvParty.CurrentCell = gvParty[2, 0];
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                strLedgerNo = "";
                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvParty.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strLedgerNo == "")
                            strLedgerNo = gvParty.Rows[i].Cells[0].Value.ToString();
                        else
                            strLedgerNo = strLedgerNo + "," + gvParty.Rows[i].Cells[0].Value.ToString();
                    }
                }
                    if (strLedgerNo != "")
                {
                    string[] ReportSession;

                    ReportSession = new string[3];

                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString(Format.DDMMMYYYY);
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = strLedgerNo;

                    Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetBrokerBagDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBrokerBagDetails.rpt", CommonFunctions.ReportPath), ReportSession);

                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                    OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkPartySelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvParty.Rows.Count; i++)
            {
                gvParty.Rows[i].Cells[2].Value = chkPartySelectAll .Checked;
            }
        }

        private void gvParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnPrint.Focus();
            }
        }


    }
}
