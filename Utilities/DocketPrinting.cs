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
namespace Yadi.Utilities
{
    public partial class DocketPrinting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTDocketPrinting dbTDocketPrinting = new DBTDocketPrinting();
        TDocketPrinting tDocketPrinting = new TDocketPrinting();
        TDocketPrintingDetails tDocketPrintingDetails = new TDocketPrintingDetails();

        DataTable dtSearch = new DataTable();
        int cntRow;

        long ID = 0;

        public DocketPrinting()
        {
            InitializeComponent();
        }

        private void DocketPrinting_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbDepostiteto, "Select LedgerNo,LedgerName From MLedger Where GroupNo=28 And IsActive='True'");
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);

            dtSearch = ObjFunction.GetDataView("Select DocketPrintingNo From TDocketPrinting order by FromDate").Table;

            if (dtSearch.Rows.Count > 0)
            {
                ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString()); ;
                FillControls();
                SetNavigation();
            }

            setDisplay(true);
            btnNew.Focus();
            KeyDownFormat(this.Controls);
        }

        public void FillControls()
        {

            tDocketPrinting = dbTDocketPrinting.ModifyTDocketPrintingByID(ID);
            DTPFromDate.Value = tDocketPrinting.FromDate;
            DTToDate.Value = tDocketPrinting.ToDate;
            cmbDepostiteto.SelectedValue = tDocketPrinting.LedgerNo.ToString();
            txtDocketNo.Text = tDocketPrinting.DocketUserNo.ToString();
            lblChkHelp.Text = ObjQry.ReturnString("Select AccountNo From mLedgerDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbDepostiteto) + "", CommonFunctions.ConStr);
            lblChkHelp.Visible = true;
            FillData();
            dgDetails.Enabled = false;
            btnReamainig.Visible = false;
            btnPrint.Visible = true;
            btnNew.Focus();
        }

        public void InitalControls()
        {

            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);
            btnReamainig.Visible = false;
            dgDetails.Enabled = true;
            btnPrint.Visible = false;

        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {
                long No = 0;
                if (type == 5)
                {
                    No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    ID = No;
                }

                else if (type == 1)
                {
                    No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                    cntRow = 0;
                    ID = No;
                }
                else if (type == 2)
                {
                    No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    cntRow = dtSearch.Rows.Count - 1;
                    ID = No;
                }
                else
                {
                    if (type == 3)
                    {
                        cntRow = cntRow + 1;
                    }
                    else if (type == 4)
                    {
                        cntRow = cntRow - 1;
                    }

                    if (cntRow < 0)
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        cntRow = cntRow + 1;
                    }
                    else if (cntRow > dtSearch.Rows.Count - 1)
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        cntRow = cntRow - 1;
                    }
                    else
                    {
                        No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                        ID = No;
                    }

                }


                FillControls();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID)
                {
                    cntRow = i;
                    break;
                }
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }

        #endregion

        #region KeyDown Events
        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && e.Control)
            {
                if (btnPrev.Enabled) btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (btnFirst.Enabled) btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                if (btnNext.Enabled) btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (btnLast.Enabled) btnLast_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }

        }
        #endregion

        public void FillData()
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);

            string sql = " SELECT     0 AS SrNo, MLedger.LedgerName, TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate , MBranch.BranchName, TVoucherEntry.BilledAmount,TDocketPrintingDetails.Remark, 'True' AS chkSelect, TVoucherEntry.PkVoucherNo, TDocketPrintingDetails.PkSrNo " +
                       " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN TVoucherEntry INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo ON  " +
                       " TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo INNER JOIN TDocketPrintingDetails ON TVoucherEntry.PkVoucherNo = TDocketPrintingDetails.FkVoucherNo " +
                       " WHERE (TVoucherEntry.CompanyNo = 1) AND (TVoucherEntry.PayTypeNo = 4) AND (TVoucherDetails.SrNo = 501) And  TDocketPrintingDetails.DocketPrintingNo=" + ID + " ";

            DataTable dt = ObjFunction.GetDataView(sql).Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgDetails.Rows.Add();
                for (int j = 0; j < dgDetails.ColumnCount; j++)
                {
                    dgDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }

        }

        public void BindData()
        {
            if (ID == 0)
            {
                while (dgDetails.Rows.Count > 0)
                    dgDetails.Rows.RemoveAt(0);

                string sql = "SELECT 0 AS SrNo, MLedger.LedgerName, TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate, MBranch.BranchName, TVoucherEntry.BilledAmount,'' AS Remark, 'false' AS chkSelect, TVoucherEntry.PkVoucherNo,0 as DocketPrintingNo " +
                    " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN TVoucherEntry INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo ON  " +
                    " TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo " +
                    " WHERE  (TVoucherEntry.CompanyNo = 1) AND (TVoucherEntry.PayTypeNo = 4) AND (TVoucherDetails.SrNo = 501) And (TVoucherEntry.VoucherDate <= '" + DTToDate.Text + "') AND (TVoucherEntry.PkVoucherNo NOT IN (SELECT FkVoucherNo FROM TDocketPrintingDetails)) ";

                DataTable dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgDetails.Rows.Add();
                    for (int j = 0; j < dgDetails.ColumnCount; j++)
                    {
                        dgDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                    }
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BindData();
                cmbDepostiteto.Focus();
            }

        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DTToDate.MinDate = DTPFromDate.Value;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int LedgerName = 1;
            public static int ChequeNo = 2;
            public static int ChequeDate = 3;
            public static int BranchName = 4;
            public static int BilledAmount = 5;
            public static int Remark = 6;
            public static int chkSelect = 7;
            public static int PkVoucherNo = 8;
            public static int PkSrNo = 9;
        }

        public bool Validation()
        {
            bool flag = true;

            if (ObjFunction.GetComboValue(cmbDepostiteto) <= 0)
            {
                EP.SetError(cmbDepostiteto, "Select Depostite to Bank");
                EP.SetIconAlignment(cmbDepostiteto, ErrorIconAlignment.MiddleRight);
                cmbDepostiteto.Focus();
                return false;
            }
            else if (dgDetails.Rows.Count < 0)
            {
                OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return false;
            }
            else
            {
                bool flag1 = false;
                for (int i = 0; i < dgDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgDetails.Rows[i].Cells[ColIndex.chkSelect].EditedFormattedValue))
                    {
                        flag1 = true;
                        break;
                    }
                }
                flag = flag1;
            }
            return flag;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            dbTDocketPrinting = new DBTDocketPrinting();
            if (Validation())
            {

                tDocketPrinting = new TDocketPrinting();

                tDocketPrinting.DocketPrintingNo = ID;
                tDocketPrinting.FromDate = DTPFromDate.Value.Date;
                tDocketPrinting.ToDate = DTToDate.Value.Date;
                tDocketPrinting.DocketUserNo = Convert.ToInt64(txtDocketNo.Text);
                tDocketPrinting.LedgerNo = ObjFunction.GetComboValue(cmbDepostiteto);
                tDocketPrinting.UserID = DBGetVal.FirmNo;
                tDocketPrinting.UserDate = DBGetVal.ServerTime;
                tDocketPrinting.CompanyNo = DBGetVal.FirmNo;

                dbTDocketPrinting.AddTDocketPrinting(tDocketPrinting);

                for (int i = 0; i < dgDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgDetails.Rows[i].Cells[ColIndex.chkSelect].EditedFormattedValue))
                    {
                        tDocketPrintingDetails = new TDocketPrintingDetails();

                        tDocketPrintingDetails.PkSrNo = ((dgDetails[ColIndex.PkSrNo, i].EditedFormattedValue.ToString() == "") ? 0 : Convert.ToInt64(dgDetails[ColIndex.PkSrNo, i].EditedFormattedValue));
                        tDocketPrintingDetails.FkVoucherNo = Convert.ToInt64(dgDetails[ColIndex.PkVoucherNo, i].EditedFormattedValue);
                        tDocketPrintingDetails.Remark = dgDetails[ColIndex.Remark, i].EditedFormattedValue.ToString();
                        tDocketPrintingDetails.CompanyNo = DBGetVal.FirmNo;

                        dbTDocketPrinting.AddTDocketPrintingDetails(tDocketPrintingDetails);

                    }
                    else if (Convert.ToBoolean(dgDetails.Rows[i].Cells[ColIndex.chkSelect].EditedFormattedValue) == false && Convert.ToInt64(dgDetails[ColIndex.PkSrNo, i].Value) != 0)
                    {
                        tDocketPrintingDetails = new TDocketPrintingDetails();
                        tDocketPrintingDetails.PkSrNo = Convert.ToInt64(dgDetails[ColIndex.PkSrNo, i].Value);
                        dbTDocketPrinting.DeleteTDocketPrintingDetails(tDocketPrintingDetails);
                    }
                }
                long tempID = dbTDocketPrinting.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    if (ID == 0)
                    {
                        DisplayMessage("DocketPrinting Add Successfully");
                        DataRow drSearch = dtSearch.NewRow();
                        drSearch[0] = tempID;
                        dtSearch.Rows.Add(drSearch);
                        ID = tempID;
                        SetNavigation();
                        FillControls();
                    }
                    else
                    {
                        DisplayMessage("DocketPrinting Update Successfully");
                        FillControls();
                    }
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    btnNew.Focus();
                }
                else
                {
                    DisplayMessage("DocketPrinting Not Save");
                }
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void cmbDepostiteto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ObjFunction.GetComboValue(cmbDepostiteto) <= 0)
                {
                    cmbDepostiteto.Focus();
                    e.SuppressKeyPress = true;
                    lblChkHelp.Visible = false;
                }
                else
                {
                    BindData();
                    if (dgDetails.Rows.Count > 0)
                    {
                        e.SuppressKeyPress = true;
                        lblChkHelp.Text = ObjQry.ReturnString("Select AccountNo From mLedgerDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbDepostiteto) + "", CommonFunctions.ConStr);
                        lblChkHelp.Visible = true;
                        dgDetails.CurrentCell = dgDetails[ColIndex.Remark, 0];
                        dgDetails.Focus();
                       
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                InitalControls();
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                cmbDepostiteto.SelectedIndex = 0;
                lblChkHelp.Visible = true;
                txtDocketNo.Text = ObjQry.ReturnString(" Select IsNull(Max(DocketPrintingNo),0)+1 From TDocketPrinting ", CommonFunctions.ConStr);
                DTToDate.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            dgDetails.Enabled = true;
            btnReamainig.Visible = true;
            btnPrint.Visible = false;
            DTToDate.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            NavigationDisplay(5);
            btnReamainig.Visible = false;
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);

        }

        private void btnReamainig_Click(object sender, EventArgs e)
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);

            string sql = " SELECT     0 AS SrNo, MLedger.LedgerName, TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate , MBranch.BranchName, TVoucherEntry.BilledAmount,TDocketPrintingDetails.Remark, 'True' AS chkSelect, TVoucherEntry.PkVoucherNo, TDocketPrintingDetails.PkSrNo " +
                       " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN TVoucherEntry INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo ON  " +
                       " TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo INNER JOIN TDocketPrintingDetails ON TVoucherEntry.PkVoucherNo = TDocketPrintingDetails.FkVoucherNo " +
                       " WHERE (TVoucherEntry.CompanyNo = 1) AND (TVoucherEntry.PayTypeNo = 4) AND (TVoucherDetails.SrNo = 501) And  TDocketPrintingDetails.DocketPrintingNo=" + ID + " " +
                       " union " +
                       "SELECT 0 AS SrNo, MLedger.LedgerName, TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate, MBranch.BranchName, TVoucherEntry.BilledAmount,'' AS Remark, 'false' AS chkSelect, TVoucherEntry.PkVoucherNo,0 as DocketPrintingNo " +
                " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN TVoucherEntry INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo INNER JOIN TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo ON  " +
                " TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo " +
                " WHERE  (TVoucherEntry.CompanyNo = 1) AND (TVoucherEntry.PayTypeNo = 4) AND (TVoucherDetails.SrNo = 501) And  (TVoucherEntry.VoucherDate >='" + DTPFromDate.Text + "') And (TVoucherEntry.VoucherDate <= '" + DTToDate.Text + "') AND (TVoucherEntry.PkVoucherNo NOT IN (SELECT FkVoucherNo FROM TDocketPrintingDetails)) " +
                " order by  PkSrNo desc ,MLedger.LedgerName ";



            DataTable dt = ObjFunction.GetDataView(sql).Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgDetails.Rows.Add();
                for (int j = 0; j < dgDetails.ColumnCount; j++)
                {
                    dgDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }

            dgDetails.CurrentCell = dgDetails[ColIndex.Remark, 0];
            dgDetails.Focus();
        }

        private void dgDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
            else if (e.ColumnIndex == ColIndex.ChequeDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString(Format.DDMMMYYYY);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure want to delete this docket Print?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                dbTDocketPrinting = new DBTDocketPrinting();
                tDocketPrinting = new TDocketPrinting();
                tDocketPrinting.DocketPrintingNo = ID;
                if (dbTDocketPrinting.DeleteTDocketPrinting(tDocketPrinting))
                {
                    OMMessageBox.Show("docket Print Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    btnNew_Click(sender, new EventArgs());
                }
                else
                {
                    OMMessageBox.Show("docket Print Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
        }

        private void dgDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dgDetails.CurrentCell != null && dgDetails.CurrentCell.ColumnIndex == ColIndex.Remark)
                {
                    if (dgDetails[ColIndex.Remark, dgDetails.CurrentRow.Index].EditedFormattedValue.ToString().Trim() != "")
                    {
                        dgDetails.CurrentCell = dgDetails[ColIndex.Remark, ((dgDetails.RowCount - 1 > dgDetails.CurrentRow.Index) ? dgDetails.CurrentRow.Index + 1 : dgDetails.RowCount - 1)];
                        dgDetails.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnSave.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int PrintType = 0;
                string[] ReportSession;
                ReportSession = new string[1];
                ReportSession[0] = ID.ToString();
                if (PrintType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.RptDocketPrinting");
                    else
                        childForm = ObjFunction.LoadReportObject("RptDocketPrinting.rpt", CommonFunctions.ReportPath);


                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            DisplayMessage("Bill Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("Bill not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
                else
                {
                    Form NewF = null;
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptDocketPrinting.rpt", CommonFunctions.ReportPath), ReportSession);


                    if (NewF != null)
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }


                //Form NewF = null;
                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //    NewF = new Display.ReportViewSource(new Reports.RptDocketPrinting(), ReportSession);
                //else
                //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptDocketPrinting.rpt", CommonFunctions.ReportPath), ReportSession);
                //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}
