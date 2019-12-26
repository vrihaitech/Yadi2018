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
    public partial class MultipleBillPrint : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        long LedgerNo1, VoucherType, CompanyNo, MainMfgCompNo = 0;
        string PartyName = "", MainMfgCompName;
        public MultipleBillPrint()
        {
            InitializeComponent();
        }
        public MultipleBillPrint(long ledgerNo, long voucherType, long companyNo, string partyName)
        {
            InitializeComponent();
            LedgerNo1 = ledgerNo;
            VoucherType = voucherType;
            CompanyNo = companyNo;
            PartyName = partyName;
        }
        public MultipleBillPrint(long ledgerNo, long voucherType, long companyNo, string partyName, long MfgCompNo, string MfgCompName)
        {
            InitializeComponent();
            LedgerNo1 = ledgerNo;
            VoucherType = voucherType;
            CompanyNo = companyNo;
            PartyName = partyName;
            MainMfgCompNo = MfgCompNo;
            MainMfgCompName = MfgCompName;
        }

        private void MultipleBillPrint_Load(object sender, EventArgs e)
        {
            rbSummary.Checked = true;
            BindGrid();
            KeyDownFormat(this.Controls);
        }

        public void BindGrid()
        {
            DataTable dt;
            if (MainMfgCompNo == 0)
                dt = ObjFunction.GetDataView("Exec GetCollectionDetails " + LedgerNo1 + "," + VoucherType + "," + CompanyNo + "").Table;
            else
                dt = ObjFunction.GetDataView("Exec GetFirmCollectionDetails " + LedgerNo1 + "," + VoucherType + "," + CompanyNo + "," + MainMfgCompNo + "").Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GridView.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
            }
            if (GridView.Rows.Count > 0)
                GridView.CurrentCell = GridView[15, 0];
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (rbSummary.Checked == true)
            {
                string strBillNo = "";

                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(GridView.Rows[i].Cells[15].Value) == true)
                    {
                        if (strBillNo == "")
                            strBillNo = GridView.Rows[i].Cells[0].Value.ToString();
                        else
                            strBillNo += "," + GridView.Rows[i].Cells[0].Value.ToString();
                    }
                }
                if (strBillNo == "")
                    OMMessageBox.Show("Atleast one Bill required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                else
                {
                    string[] ReportSession;
                    Form NewF = null;
                    if (MainMfgCompNo == 0)
                        ReportSession = new string[6];
                    else
                    {
                        ReportSession = new string[8];
                        ReportSession[6] = MainMfgCompNo.ToString();
                        ReportSession[7] = MainMfgCompName;

                    }
                    ReportSession[2] = LedgerNo1.ToString();
                    ReportSession[3] = VoucherType.ToString();
                    ReportSession[4] = DBGetVal.FirmNo.ToString();
                    ReportSession[5] = strBillNo;
                    if (VoucherType == VchType.Sales)
                        ReportSession[0] = "Sales Outstanding Report";
                    else if (VoucherType == VchType.Purchase)
                        ReportSession[0] = "Purchase Outstanding Report";
                    ReportSession[1] = PartyName;

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    {
                        if (MainMfgCompNo == 0)
                            NewF = new Display.ReportViewSource(new Reports.GetCollectionDetailsByBilles(), ReportSession);
                        //else
                        //    NewF = new Display.ReportViewSource(new Reports.GetFirmCollectionDetailsByBilles(), ReportSession);
                    }
                    else
                    {
                        if (MainMfgCompNo == 0)
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetCollectionDetailsByBilles.rpt", CommonFunctions.ReportPath), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetFirmCollectionDetailsByBilles.rpt", CommonFunctions.ReportPath), ReportSession);
                    } ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }

            }
            else if (rbDetails.Checked == true)
            {
                string  strVchNo = "";

                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(GridView.Rows[i].Cells[15].Value) == true)
                    {
                        if (strVchNo == "")
                            strVchNo = GridView.Rows[i].Cells[14].Value.ToString();
                        else
                            strVchNo += "," + GridView.Rows[i].Cells[14].Value.ToString();
                    }
                }
                if (strVchNo == "")
                    OMMessageBox.Show("Atleast one Bill required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                else
                {
                    string[] ReportSession;
                    Form NewF = null;

                    if (MainMfgCompNo == 0)
                        ReportSession = new string[5];
                    else
                    {
                        ReportSession = new string[7];
                        ReportSession[5] = MainMfgCompNo.ToString();
                        ReportSession[6] = MainMfgCompName;
                    }
                    if (VoucherType == VchType.Sales)
                        ReportSession[0] = "Sales Bill Details";
                    else if (VoucherType == VchType.Purchase)
                        ReportSession[0] = "Purchase Inv Detailes";
                    ReportSession[1] = PartyName;
                    ReportSession[2] = "1";
                    ReportSession[3] = strVchNo;
                    ReportSession[4] = VoucherType.ToString();

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    {
                        if (MainMfgCompNo == 0)
                            NewF = new Display.ReportViewSource(new Reports.GetBillDetails(), ReportSession);
                        //else NewF = new Display.ReportViewSource(new Reports.GetFirmBillDetails(), ReportSession);
                    }
                    else
                    {
                        if (MainMfgCompNo == 0)
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetFirmBillDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    } ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }

            }
            else if (rbCustDetails.Checked == true)
            {
                if (GridView.Rows.Count > 0)
                {

                    string[] ReportSession;
                    if (MainMfgCompNo == 0)
                        ReportSession = new string[4];
                    else
                    {
                        ReportSession = new string[6];
                        ReportSession[4] = MainMfgCompNo.ToString();
                        ReportSession[5] = MainMfgCompName;
                    }
                    ReportSession[0] = VoucherType.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DateTime.Now.Date).ToString("dd-MMM-yyyy");
                    ReportSession[3] = LedgerNo1.ToString();

                    Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    {
                        if (MainMfgCompNo == 0)
                            NewF = new Display.ReportViewSource(new Reports.GetOutstandingDetails(), ReportSession);
                        //else
                         //   NewF = new Display.ReportViewSource(new Reports.GetFirmOutstandingDetails(), ReportSession);
                    }
                    else
                    {
                        if (MainMfgCompNo == 0)
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutstandingDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetFirmOutstandingDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
        }

        private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");

            }
        }
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                GridView.Rows[i].Cells[15].Value = chkSelectAll.Checked;
            }
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;
                chkSelectAll_CheckedChanged(sender, new EventArgs());
                btnPrint.Focus();
            }
            else if (e.KeyCode == Keys.F3)
            {
                rbSummary.Checked = true;
            }
            else if (e.KeyCode == Keys.F4)
            {
                rbDetails.Checked = true;
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

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustDetails.Checked == true)
            {
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    GridView.Rows[i].Cells[15].Value = true;
                    GridView.Rows[i].Cells[15].ReadOnly = true;
                }
                btnPrint.Focus();
            }
            else
            {
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    GridView.Rows[i].Cells[15].Value = false;
                    GridView.Rows[i].Cells[15].ReadOnly = false;
                }
                GridView.Focus();
            }
        }
    }
}
