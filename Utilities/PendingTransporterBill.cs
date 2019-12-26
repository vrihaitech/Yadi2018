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
    public partial class PendingTransporterBill : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVaucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        public PendingTransporterBill()
        {
            InitializeComponent();
        }

        private void BulkLRNOEntry_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbCompanyName, "Select MFGCompNo,MfgCompName from MManufacturerCompany  Where IsActive='True' order By MfgCompName", "All Company");
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            cmbCompanyName.SelectedIndex = 1;
            KeyDownFormat(this.Controls);
            if (rbPending.Checked == true)
                btnPrint.Visible = true;
            else
                btnPrint.Visible = false;
        }

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
            if (e.KeyCode == Keys.F3)
            {
                rbAll.Checked = true;
            }
            else if (e.KeyCode == Keys.F1)
            {
                rbPending.Checked = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                rbFullfill.Checked = true;
            }

        }
        #endregion

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        public void BindGrid()
        {
            while (dgBill.Rows.Count > 0)
                dgBill.Rows.RemoveAt(0);

            string Sql = "";
            if (ObjFunction.GetComboValue(cmbCompanyName) == -1)
            {
                Sql = " SELECT  0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo,MLedger.LedgerName as 'PartyName',TVoucherEntry.BilledAmount,  MLedger_1.LedgerName,MTransporterPayType.TransPayTypeName AS Status,Case When(TransPayType=5) Then 'false' Else 'true' End As chk, TVoucherEntry.PkVoucherNo,0 As IsChange " +
                    " FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN " +
                    " MLedger AS MLedger_1 ON TVoucherEntry.TransporterCode = MLedger_1.LedgerNo " +
                    " INNER JOIN MTransporterPayType ON MTransporterPayType.TransPayTypeNo =TVoucherEntry.TransPayType " +
                    " WHERE     (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.VoucherTypeCode = " + VchType.Purchase + ") " +
                    " And TVoucherEntry.VoucherDate>='" + DTPFromDate.Text + "' and TVoucherEntry.VoucherDate<='" + DTToDate.Text + "'";

            }
            else
            {
                if (ObjFunction.GetComboValue(cmbCompanyName) > 0)
                {
                    Sql = " SELECT  0 AS SrNo, TVoucherEntry.VoucherDate,   TVoucherEntryCompany.VoucherUserNo,MLedger.LedgerName as 'PartyName',TVoucherEntry.BilledAmount,  MLedger_1.LedgerName, MTransporterPayType.TransPayTypeName AS Status, Case When(TransPayType=5) Then 'false' Else 'true' End As chk, TVoucherEntry.PkVoucherNo,0 As IsChange " +
                       " FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  INNER JOIN TVoucherEntryCompany ON TVoucherEntry.PkVoucherNo = TVoucherEntryCompany.FkVoucherNo LEFT OUTER JOIN " +
                       " MLedger AS MLedger_1 ON TVoucherEntry.TransporterCode = MLedger_1.LedgerNo " +
                       " INNER JOIN MTransporterPayType ON MTransporterPayType.TransPayTypeNo =TVoucherEntry.TransPayType " +
                       " WHERE     (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.VoucherTypeCode = " + VchType.Purchase + ") " +
                       " And  TVoucherEntryCompany.MfgCompNo=" + ObjFunction.GetComboValue(cmbCompanyName) + "" +
                       " And TVoucherEntry.VoucherDate>='" + DTPFromDate.Text + "' and TVoucherEntry.VoucherDate<='" + DTToDate.Text + "' and TVoucherEntry.IsBillMulti=0 ";
                }
            }

            if (Sql != "")
            {
                if (rbAll.Checked == true)
                {
                    Sql = Sql + " ORDER by  TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo";
                }
                else if (rbPending.Checked == true)
                {
                    Sql = Sql + "And IsNull(TVoucherEntry.TransPayType,0)=5  ORDER by  TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo";
                }
                else if (rbFullfill.Checked == true)
                {
                    Sql = Sql + "And IsNull(TVoucherEntry.TransPayType,0)=4 ORDER by  TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo";
                }

                DataTable dtDetails = ObjFunction.GetDataView(Sql).Table;
                for (int i = 0; i < dtDetails.Rows.Count; i++)
                {
                    dgBill.Rows.Add();
                    for (int j = 0; j < dgBill.ColumnCount; j++)
                    {
                        dgBill.Rows[i].Cells[j].Value = dtDetails.Rows[i].ItemArray[j].ToString();
                    }
                    if (Convert.ToBoolean(dtDetails.Rows[i].ItemArray[ColIndex.chk].ToString()) == true)
                        dgBill.Rows[i].Cells[ColIndex.chk].ReadOnly = true;
                }

                if (dgBill.Rows.Count > 0)
                {
                    dgBill.CurrentCell = dgBill[ColIndex.TransName, 0];
                    dgBill.Focus();
                }

            }
        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int VoucherDate = 1;
            public static int BillNo = 2;
            public static int Party = 3;
            public static int Amount = 4;
            public static int TransName = 5;
            public static int Status = 6;
            public static int chk = 7;
            public static int FkVoucherNo = 8;
            public static int IsChange = 9;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int RowIndex = dgBill.CurrentRow.Index;
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.chk)
                {
                    e.SuppressKeyPress = true;
                    if (dgBill.Rows.Count - 1 > RowIndex)
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.chk, RowIndex + 1];
                        dgBill.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnApplyChange.Focus();
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

        private void btnApplyChange_Click(object sender, EventArgs e)
        {
            dbTVaucherEntry = new DBTVaucherEntry();
            bool flag = false;
            for (int i = 0; i < dgBill.Rows.Count; i++)
            {
                if (Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.IsChange].EditedFormattedValue.ToString()) == 1)
                {
                    if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.chk].EditedFormattedValue.ToString()) == true)
                    {
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = Convert.ToInt64(dgBill[ColIndex.FkVoucherNo, i].Value);
                        tVoucherEntry.TransPayType = 4;
                        dbTVaucherEntry.UpdateTransporterStatus(tVoucherEntry);
                        flag = true;
                    }
                }
            }

            if (flag)
            {
                if (dbTVaucherEntry.ExecuteNonQueryStatementsCheque())
                {
                    DisplayMessage("Transporter Details Update Successfully...");
                    BindGrid();
                }
                else
                {
                    DisplayMessage("Transporter Details Not Update Successfully!!");
                }
            }

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbCompanyName.SelectedIndex = 1;
            rbAll.Checked = true;
            while (dgBill.Rows.Count > 0)
                dgBill.Rows.RemoveAt(0);
            cmbCompanyName.Focus();
        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.VoucherDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }
            else if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
            if (rbPending.Checked == true)
                btnPrint.Visible = true;
            else
                btnPrint.Visible = false;
        }

        private void dgBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.chk)
            {
                int RowIndex = e.RowIndex;
                if (dgBill.Rows[RowIndex].Cells[ColIndex.chk].ReadOnly == false)
                {
                    dgBill.Rows[RowIndex].Cells[ColIndex.IsChange].Value = "1";
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.Rows.Count > 0)
                {
                    string[] ReportSession;
                    ReportSession = new string[5];
                    if (ObjFunction.GetComboValue(cmbCompanyName) == -1)
                    {
                        ReportSession[0] = DBGetVal.FirmName;
                    }
                    else
                    {
                        ReportSession[0] = cmbCompanyName.Text.ToString();
                    }

                    ReportSession[1] = ObjFunction.GetComboValue(cmbCompanyName).ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = VchType.Purchase.ToString();
                    Form NewF;

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetPendingTransporterBill(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetPendingTransporterBill.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        
        }
    }
}
