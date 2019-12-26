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
    public partial class PurchaseOrderDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataTable dtParty = new DataTable();
        DBProgressBar PB;

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "", strLedgerNo = "";

        public PurchaseOrderDetails()
        {
            InitializeComponent();
        }

        private void StockSummary_Load(object sender, EventArgs e)
        {
             
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            KeyDownFormat(this.Controls);

            rbOpen.Checked = true;
            new GridSearch(gvParty, 1);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;

                ReportSession = new string[5];

                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = Convert.ToString(false);
                ReportSession[4] = strItemNo;
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNew.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                BtnPartyShow.Focus();

            }
        }

        private void BtnPartyShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbOpen.Checked == true)
                {
                    BindGridParty();
                }
                else if (rbClose.Checked == true)
                {
                    BindGridParty();
                }
                else
                {
                    BindGridParty();
                }

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
                    // BindGridParty();
                    //pnlPartyDetails.Visible = true;
                    strLedgerNo = "";
                    strItemNo = "";
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
            try
            {

                dtParty = new DataTable();


                //string str = "SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName,'false' as chk " +
                //             " FROM MLedger INNER JOIN " +
                //             " TOtherVoucherEntry ON TOtherVoucherEntry.LedgerNo = MLedger.LedgerNo " +
                //             " WHERE (TOtherVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "')" +
                //             " AND (TOtherVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "')";

                string str = " SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName,'false' as chk " +
                             " FROM MLedger INNER JOIN " +
                             " TOtherVoucherEntry ON TOtherVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                             " TOtherStock ON TOtherVoucherEntry.PkOtherVoucherNo = TOtherStock.FKVoucherNo " +
                             " WHERE (TOtherVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "') " +
                             " AND (TOtherVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "') ";



                if (rbOpen.Checked == true)
                {
                    str = str + " AND TOtherVoucherEntry.IsComplete = 'false' AND TOtherVoucherEntry.VoucherStatus != 4"; //  AND TOtherStock.IsComplete = 'false' ";
                }
                else if (rbClose.Checked == true)
                {
                    str = str + " AND TOtherVoucherEntry.IsComplete = 'true' AND TOtherVoucherEntry.VoucherStatus = 4"; //   AND TOtherStock.IsComplete = 'true' ";
                }
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
               // new GridSearch(gvParty, 1);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {

                chkPartySelectAll.Checked = !chkPartySelectAll.Checked;

                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
                }
                BtnShowItem.Focus();

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

        private void BtnShowItem_Click(object sender, EventArgs e)
        {
            //pnlSelectType.Visible = true;
            if (rbOpen.Checked == true)
            {
                CallReport(1);
                pnlSelectType.Visible = false;
            }
            else if (rbClose.Checked == true)
            {
                CallReport(2);
                pnlSelectType.Visible = false;
            }
            else if (rbBoth.Checked == true)
            {
                CallReport(3);
                pnlSelectType.Visible = false;
            }
        }

        private void chkPartySelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvParty.Rows.Count; i++)
            {
                gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
            }
        }

        public static class ColIndex
        {
            public static int Date = 3;
            public static int Time = 4;
            public static int ItemName = 2;
            public static int BillNo = 5;
            public static int Qty = 6;
            //public static int Rate = 7;
            //public static int Amt = 8;
            public static int GrpName = 1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            pnlPartyDetails.Visible = false;
            BtnPartyShow.Focus();
            chkPartySelectAll.Checked = false;
            strLedgerNo = "";
        }

        private void CallReport(int ch)
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

                ReportSession = new string[4];

                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = strLedgerNo;
                ReportSession[3] = ch.ToString();


                Form NewF = null;
                //if (rdDetails.Checked == true)
                //{
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RptPurchaseOrderDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptPurchaseOrderDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                //}
                //else
                //{
                //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //        NewF = new Display.ReportViewSource(new Reports.RptLedgerItemWisePurchaseSumm(), ReportSession);
                //    else
                //        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("PurchaseOtherSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                //}
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                //pnlItemDetails.Visible = true;

            }
            else
                OMMessageBox.Show("Select Atleast one PartyName ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            CallReport(1);
            pnlSelectType.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CallReport(2);
            pnlSelectType.Visible = false;
        }

        private void btnBoth_Click(object sender, EventArgs e)
        {
            CallReport(3);
            pnlSelectType.Visible = false;
        }


    }
}
