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
    public partial class CashSlip : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTCashSlip dbTCashSlip = new DBTCashSlip();
        TCashSlip tCashSlip = new TCashSlip();
        TCashSlipDetails tCashSlipDetails = new TCashSlipDetails();

        DataTable dtSearch = new DataTable();
        int cntRow;
        long No = 0;
        long ID = 0;

        public CashSlip()
        {
            InitializeComponent();
        }

        private void CashSlip_Load(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);

            ObjFunction.FillCombo(cmbDepostiteto, "Select LedgerNo,LedgerName From MLedger Where GroupNo=28 And IsActive='True'");
            dtSearch = ObjFunction.GetDataView("Select  max(CashSlipNo) From TCashSlip").Table;
            if (dtSearch.Rows.Count > 0)
            {
                ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString()); 
                FillControls();
                SetNavigation();
            }
            foreach (DataGridViewColumn col in dgCashDenomination.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            lblTotal.Font = ObjFunction.GetFont(FontStyle.Bold, 14);
            setDisplay(true);
            btnNew.Focus();
            KeyDownFormat(this.Controls);
        }

        public void FillControls()
        {

            tCashSlip = dbTCashSlip.ModifyTCashSlipByID(ID);
            DTToDate.Value = tCashSlip.ToDate;
            cmbDepostiteto.SelectedValue = tCashSlip.LedgerNo.ToString();
            txtDocketNo.Text = tCashSlip.DocketUserNo.ToString();
            lblChkHelp.Text = ObjQry.ReturnString("Select AccountNo From mLedgerDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbDepostiteto) + "", CommonFunctions.ConStr);
            lblChkHelp.Visible = false;
            FillData();
            dgCashDenomination.Enabled = false;
            btnPrint.Visible = true;
            btnNew.Focus();
        }

        public void InitalControls()
        {
            dgCashDenomination.Enabled = true;
            btnPrint.Visible = true;
        }

        #region Navigation Methods

        private void NavigationDisplay(int type)
        {
            try
            {
                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CashSlipNo),0)as CashSlipNo From TCashSlip ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashSlipNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CashSlipNo),0)as CashSlipNo From TCashSlip ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashSlipNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CashSlipNo),0)as CashSlipNo From TCashSlip where CashSlipNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashSlipNo"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
                else if (type == 4)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CashSlipNo),0) as CashSlipNo From TCashSlip where  CashSlipNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashSlipNo"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            if(ID!=0)
            FillControls();

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
            string sql = " SELECT   0 As SrNo, TCashSlipDetails.RSType, TCashSlipDetails.Note, TCashSlipDetails.Pieces, TCashSlipDetails.Amount,CashSlipDetailsNo " +
                         " FROM TCashSlip INNER JOIN TCashSlipDetails ON TCashSlip.CashSlipNo = TCashSlipDetails.CashSlipNo Where TCashSlip.CashSlipNo = " + ID + "";

            dgCashDenomination.Rows.Clear();
            DataTable dt = ObjFunction.GetDataView(sql).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgCashDenomination.Rows.Add();
                for (int j = 0; j < dgCashDenomination.Columns.Count; j++)
                {
                    dgCashDenomination.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
        }

        public void BindData()
        {

            string sql = " SELECT 0 AS SrNo, Case When (RSType=0) then 'NA' When (RSType=1) then 'Note' When (RSType=2) then 'Coin' End As RSType, " +
                       " Note as Notes,0 as PIECES,0 as Amount,0 as CashSlipDetailsNo From MCashDenomination Where IsActive = 'true' Order By RSType desc,SerialNo";

            dgCashDenomination.Rows.Clear();
            DataTable dt = ObjFunction.GetDataView(sql).Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgCashDenomination.Rows.Add();
                for (int j = 0; j < dgCashDenomination.Columns.Count; j++)
                {
                    dgCashDenomination.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
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
            else if (dgCashDenomination.Rows.Count < 0)
            {
                OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return false;
            }
            return flag;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (Validation() == true)
            {
                dbTCashSlip = new DBTCashSlip();
                tCashSlip = new TCashSlip();

                tCashSlip.CashSlipNo = ID;
                tCashSlip.ToDate = Convert.ToDateTime(DTToDate.Value);
                tCashSlip.DocketUserNo = Convert.ToInt64(txtDocketNo.Text);
                tCashSlip.LedgerNo = ObjFunction.GetComboValue(cmbDepostiteto);
                tCashSlip.UserID = DBGetVal.FirmNo;
                tCashSlip.UserDate = DBGetVal.ServerTime;
                tCashSlip.CompanyNo = DBGetVal.FirmNo;

                dbTCashSlip.AddTCashSlip(tCashSlip);

                for (int i = 0; i < dgCashDenomination.Rows.Count; i++)
                {
                    tCashSlipDetails = new TCashSlipDetails();
                    tCashSlipDetails.CashSlipDetailsNo = ((dgCashDenomination[ColIndex.CashSlipDetailsNo, i].EditedFormattedValue.ToString() == "") ? 0 : Convert.ToInt64(dgCashDenomination[ColIndex.CashSlipDetailsNo, i].EditedFormattedValue));
                    tCashSlipDetails.Note = (dgCashDenomination[ColIndex.Notes, i].Value.ToString());
                    tCashSlipDetails.RSType = (dgCashDenomination[ColIndex.RSType, i].Value.ToString());
                    tCashSlipDetails.Pieces = Convert.ToInt64(dgCashDenomination[ColIndex.Pieces, i].Value.ToString());
                    tCashSlipDetails.Amount = Convert.ToInt64(dgCashDenomination[ColIndex.Amount, i].Value.ToString());
                    tCashSlipDetails.IsActive = true;
                    tCashSlip.UserID = DBGetVal.FirmNo;
                    //tCashSlip.UserDate = System.DateTime.Now;// DBGetVal.ServerTime;
                    tCashSlipDetails.CompanyNo = DBGetVal.FirmNo;

                    dbTCashSlip.AddTCashSlipDetails(tCashSlipDetails);

                }
                long tempID = dbTCashSlip.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    if (ID == 0)
                    {
                        DisplayMessage("Cash Slip Add Successfully");
                        dtSearch = ObjFunction.GetDataView("Select CashSlipNo From TCashSlip").Table;
                        ID = ObjQry.ReturnLong("Select Max(CashSlipNo) From TCashSlip", CommonFunctions.ConStr);
                        SetNavigation();
                        FillControls();
                    }
                    else
                    {
                        DisplayMessage("Cash Slip Update Successfully");
                        FillControls();
                    }
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    btnNew.Focus();
                }
                else
                {
                    DisplayMessage("Cash Slip Not Save");
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
                lblChkHelp.Visible = false;
                txtDocketNo.Text = ObjQry.ReturnString("Select IsNull(Max(CashSlipNo),0)+1 From TCashSlip ", CommonFunctions.ConStr);
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
            dgCashDenomination.Enabled = true;
            btnPrint.Visible = true;
            DTToDate.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure want to delete this docket Print?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                dbTCashSlip = new DBTCashSlip();
                tCashSlip = new TCashSlip();

                tCashSlip.CashSlipNo = ID;
                if (dbTCashSlip.DeleteTCashSlip(tCashSlip))
                {
                    OMMessageBox.Show("CashSlip Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    btnNew_Click(sender, new EventArgs());
                }
                else
                {
                    OMMessageBox.Show("CashSlip Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int PrintType = 0;
                string[] ReportSession;
                ReportSession = new string[4];
                ReportSession[0] = ID.ToString();
                ReportSession[1] = cmbDepostiteto.Text;
                ReportSession[2] = txtDocketNo.Text;
                ReportSession[3] = DTToDate.Value.ToString();

                ////****//
                if (PrintType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.CashDenominationSlip");
                    else
                        childForm = ObjFunction.LoadReportObject("CashDenominationSlip.rpt", CommonFunctions.ReportPath);


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
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("CashDenominationSlip.rpt", CommonFunctions.ReportPath), ReportSession);


                    if (NewF != null)
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }


                //****//

                //Form NewF = null;
                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //    NewF = new Display.ReportViewSource(new Reports.RptDocketPrinting(), ReportSession);
                //else
                //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("CashDenominationSlip.rpt", CommonFunctions.ReportPath), ReportSession);
                //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
          
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                cmbDepostiteto.Focus();
            }

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
                    dgCashDenomination.Focus();
                    dgCashDenomination.CurrentCell = dgCashDenomination[ColIndex.Pieces, 0];
                }
            }
        }

        private void dgCashDenomination_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void dgCashDenomination_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgCashDenomination.Columns[ColIndex.Pieces].Index)
                {
                    //if (e.ColumnIndex != dgCashDenomination.Rows[ColumnIndex.Notes].Index && e.RowIndex != dgCashDenomination.Rows[RowsIndex.Coins].Index)
                    //{
                        if (ObjFunction.CheckNumeric(e.FormattedValue.ToString()) == false)
                        {
                            e.Cancel = true;
                            OMMessageBox.Show("Please Enter Only Numbers...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    //}
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int RSType = 1;
            public static int Notes = 2;
            public static int Pieces = 3;
            public static int Amount = 4;
            public static int CashSlipDetailsNo = 5;
        }
        #endregion

        public delegate void MovetoNext(int RowIndex, int ColIndex);
        public void m2n(int RowIndex, int ColIndex)
        {
            dgCashDenomination.CurrentCell = dgCashDenomination.Rows[RowIndex].Cells[ColIndex];
        }

        private void dgCashDenomination_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double tempAmount = 0.0;
                for (int i = 0; i < dgCashDenomination.RowCount; i++)
                {
                    if ((dgCashDenomination.Rows[i].Cells[ColIndex.Notes].Value != null) && (dgCashDenomination.Rows[i].Cells[ColIndex.Pieces].Value != null) && (dgCashDenomination.Rows[i].Cells[ColIndex.Amount].Value != null))
                    {
                        double tempDeno = Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.Notes].Value.ToString());
                        double tempPieces = Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.Pieces].Value.ToString());
                        dgCashDenomination.Rows[i].Cells[ColIndex.Amount].Value = tempDeno * Convert.ToDouble(tempPieces);
                        tempAmount = tempAmount + Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.Amount].FormattedValue.ToString());
                    }
                }

                //putting value to total textbox
                lblTotal.Text = tempAmount.ToString();
                //BtnSave.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

    }
}
