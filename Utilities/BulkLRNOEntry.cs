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
    public partial class BulkLRNOEntry : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVaucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        public BulkLRNOEntry()
        {
            InitializeComponent();
        }

        private void BulkLRNOEntry_Load(object sender, EventArgs e)
        {
            //ObjFunction.FillCombo(cmbCompanyName, "Select MFGCompNo,MfgCompName from MManufacturerCompany  Where IsActive='True' order By MfgCompName", "All Company");
            ObjFunction.FillCombo(cmbCompanyName, "SELECT FirmNo, FirmName FROM MFirm ORDER BY FirmName");
            ObjFunction.FillList(lstTransName, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.Transporter + " Order By LedgerName");
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            cmbCompanyName.SelectedIndex = 1;
            KeyDownFormat(this.Controls);
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
            if (e.KeyCode == Keys.F1)
            {
                rbAll.Checked = true;
                BindGrid();
            }
            else if (e.KeyCode == Keys.F2)
            {
                rbEmpty.Checked = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                rbFeeded.Checked = true;
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
                Sql = " SELECT  0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo,MLedger.LedgerName as 'PartyName',TVoucherEntry.BilledAmount,  MLedger_1.LedgerName, TVoucherEntry.LRNo, TVoucherEntry.TransporterCode, TVoucherEntry.PkVoucherNo,0 as IsChange " +
                    " FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN " +
                    " MLedger AS MLedger_1 ON TVoucherEntry.TransporterCode = MLedger_1.LedgerNo " +
                    " WHERE     (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.VoucherTypeCode = " + VchType.Sales + ") " +
                    " And TVoucherEntry.VoucherDate>='" + DTPFromDate.Text + "' and TVoucherEntry.VoucherDate<='" + DTToDate.Text + "'";

            }
            else
            {
                if (ObjFunction.GetComboValue(cmbCompanyName) > 0)
                {
                    Sql = " SELECT  0 AS SrNo, TVoucherEntry.VoucherDate,   TVoucherEntryCompany.VoucherUserNo,MLedger.LedgerName as 'PartyName',TVoucherEntry.BilledAmount,  MLedger_1.LedgerName, TVoucherEntry.LRNo, TVoucherEntry.TransporterCode, TVoucherEntry.PkVoucherNo,0 as IsChange " +
                       " FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  INNER JOIN TVoucherEntryCompany ON TVoucherEntry.PkVoucherNo = TVoucherEntryCompany.FkVoucherNo LEFT OUTER JOIN " +
                       " MLedger AS MLedger_1 ON TVoucherEntry.TransporterCode = MLedger_1.LedgerNo " +
                       " WHERE     (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.VoucherTypeCode = " + VchType.Sales + ") " +
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
                else if (rbEmpty.Checked == true)
                {
                    Sql = Sql + "And IsNull(TVoucherEntry.LRNo,'')=''  ORDER by  TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo";
                }
                else if (rbFeeded.Checked == true)
                {
                    Sql = Sql + "And IsNull(TVoucherEntry.LRNo,'')<>'' ORDER by  TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo";
                }

                DataTable dtDetails = ObjFunction.GetDataView(Sql).Table;
                for (int i = 0; i < dtDetails.Rows.Count; i++)
                {
                    dgBill.Rows.Add();
                    for (int j = 0; j < dgBill.ColumnCount; j++)
                    {
                        dgBill.Rows[i].Cells[j].Value = dtDetails.Rows[i].ItemArray[j].ToString();
                    }
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
            public static int LRNo = 6;
            public static int TransNo = 7;
            public static int FkVoucherNo = 8;
            public static int IsChange = 9;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstTransName_VisibleChanged(object sender, EventArgs e)
        {
            if (lstTransName.Visible == true)
            {
                dgBill.Enabled = false;
            }
            else
            {
                dgBill.Enabled = true;

            }
        }

        private void lstTransName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int RowIndex = dgBill.CurrentRow.Index;
                e.SuppressKeyPress = true;
                if (lstTransName.SelectedValue != dgBill.Rows[RowIndex].Cells[ColIndex.TransNo].Value)
                {
                    dgBill.Rows[RowIndex].Cells[ColIndex.TransNo].Value = lstTransName.SelectedValue;
                    dgBill.Rows[RowIndex].Cells[ColIndex.TransName].Value = lstTransName.Text;
                    dgBill.Rows[RowIndex].Cells[ColIndex.TransName].Style.BackColor = Color.LightBlue;
                    dgBill.Rows[RowIndex].Cells[ColIndex.IsChange].Value = "1";
                }
                lstTransName.Visible = false;
                dgBill.CurrentCell = dgBill[ColIndex.LRNo, RowIndex];
                dgBill.Focus();
            }
            else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
            {
                int RowIndex = dgBill.CurrentRow.Index;
                e.SuppressKeyPress = true;
                lstTransName.Visible = false;
                dgBill.CurrentCell = dgBill[ColIndex.TransName, RowIndex];
                dgBill.Focus();
            }
        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int RowIndex = dgBill.CurrentRow.Index;
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.TransName)
                {
                    e.SuppressKeyPress = true;
                    lstTransName.Visible = !lstTransName.Visible;
                    if (lstTransName.Visible == true) lstTransName.Focus(); else dgBill.Focus();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.LRNo)
                {
                    e.SuppressKeyPress = true;
                    if (dgBill.Rows.Count - 1 > RowIndex)
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.TransName, RowIndex + 1];
                        dgBill.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                lstTransName.Visible = false;
                btnApplyChange.Focus();
            }
        }

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.LRNo)
            {
                int RowIndex = e.RowIndex;
                dgBill.Rows[RowIndex].Cells[ColIndex.LRNo].Style.BackColor = Color.LightBlue;
                dgBill.Rows[RowIndex].Cells[ColIndex.IsChange].Value = "1";
                if (dgBill.Rows.Count - 1 > e.RowIndex)
                {
                    dgBill.CurrentCell = dgBill[ColIndex.TransName, e.RowIndex + 1];
                    dgBill.Focus();
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

        private void btnApplyChange_Click(object sender, EventArgs e)
        {
            dbTVaucherEntry = new DBTVaucherEntry();
            bool flag = false;
            for (int i = 0; i < dgBill.Rows.Count; i++)
            {
                if (Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.IsChange].EditedFormattedValue.ToString()) == 1)
                {
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = Convert.ToInt64(dgBill[ColIndex.FkVoucherNo, i].Value);
                    tVoucherEntry.TransporterCode = Convert.ToInt64((dgBill[ColIndex.TransNo, i].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgBill[ColIndex.TransNo, i].Value);
                    tVoucherEntry.LRNo = dgBill[ColIndex.LRNo, i].EditedFormattedValue.ToString().Trim();
                    dbTVaucherEntry.UpdateTransporterDetails(tVoucherEntry);
                    flag = true;
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
            lstTransName.Visible = false;
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
        }
    }
}
