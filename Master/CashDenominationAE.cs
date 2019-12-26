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
    public partial class CashDenominationAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMCashDenomination dbMCashDenomination = new DBMCashDenomination();
        MCashDenomination mCashDenomination = new MCashDenomination();
        string NoteNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long BankNo, ID = 0, LedgerUserNo = 0, No = 0;
        DataTable dt = new DataTable();

        public CashDenominationAE()
        {
            InitializeComponent();
        }

        private void CashDenominationAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                NoteNm = "";

                cmbRSType.Items.Add("NA");
                cmbRSType.Items.Add("Note");
                cmbRSType.Items.Add("Coin");

                dtSearch = ObjFunction.GetDataView("Select CashDenominationNo From MCashDenomination order by CashDenominationNo").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    if (CashDenomination.RequestCashDenominationNo != 0)
                    {
                        ID = CashDenomination.RequestCashDenominationNo;
                        NoteNm = "";
                    }
                    else
                    {
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    }
                    FillControls();
                    SetNavigation();
                }
                foreach (DataGridViewColumn col in GVRS.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillControls()
        {
            try
            {
                EP.SetError(txtNote, "");
                EP.SetError(txtSerialNo, "");

                mCashDenomination = dbMCashDenomination.ModifyMCashDenominationByID(ID);
                NoteNm = mCashDenomination.Note;
                txtNote.Text = mCashDenomination.Note;
                txtSerialNo.Text = mCashDenomination.SerialNo.ToString();
                cmbRSType.SelectedIndex = Convert.ToInt32(mCashDenomination.RSType);
                cmbRSType.SelectedItem = cmbRSType.SelectedIndex.ToString();
                chkActive.Checked = mCashDenomination.IsActive;

                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";

                BindGrid();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGrid()
        {
            string sql = " SELECT 0 AS SrNo, Case When (RSType=0) then 'NA' When (RSType=1) then 'Note' When (RSType=2) then 'Coin' End As RSType, " +
                         " Note,SerialNo From MCashDenomination Where IsActive = 'true' Order By RSType desc,SerialNo ";

            GVRS.Rows.Clear();
            dt = ObjFunction.GetDataView(sql).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GVRS.Rows.Add();
                for (int j = 0; j < GVRS.ColumnCount; j++)
                {
                    GVRS.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {

                    dbMCashDenomination = new DBMCashDenomination();
                    mCashDenomination = new MCashDenomination();
                    mCashDenomination.CashDenominationNo = ID;
                    mCashDenomination.Note = txtNote.Text.Trim();
                    mCashDenomination.RSType = Convert.ToInt64(cmbRSType.SelectedIndex);
                    mCashDenomination.SerialNo = Convert.ToInt64(txtSerialNo.Text);
                    mCashDenomination.IsActive = chkActive.Checked;
                    mCashDenomination.UserID = DBGetVal.UserID;
                    mCashDenomination.UserDate = DBGetVal.ServerTime.Date;
                    mCashDenomination.CompanyNo = DBGetVal.FirmNo;

                    if (dbMCashDenomination.AddMCashDenomination(mCashDenomination) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Cash Denomination Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select CashDenominationNo From MCashDenomination order by Note").Table;
                            ID = ObjQry.ReturnLong("Select Max(CashDenominationNo) From MCashDenomination", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Cash Denomination Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();

                    }
                    else
                    {
                        OMMessageBox.Show("Cash Denomination Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Bank.RequestBankNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtNote, "");
            EP.SetError(txtSerialNo, "");

            if (txtNote.Text.Trim() == "")
            {
                EP.SetError(txtNote, "Enter Note");
                EP.SetIconAlignment(txtNote, ErrorIconAlignment.MiddleRight);
                txtNote.Focus();
            }
            else if (txtSerialNo.Text.Trim() == "")
            {
                EP.SetError(txtSerialNo, "Enter Serial No");
                EP.SetIconAlignment(txtSerialNo, ErrorIconAlignment.MiddleRight);
                chkActive.Focus();
                flag = false;

            }
            else if (NoteNm != txtNote.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MCashDenomination where RSType = " + Convert.ToInt64(cmbRSType.SelectedValue) + " and Note = '" + txtNote.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtNote, "Duplicate Note");
                    EP.SetIconAlignment(txtNote, ErrorIconAlignment.MiddleRight);
                    txtNote.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;

            return flag;
        }

        private void CashDenominationAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            CashDenomination.RequestCashDenominationNo = 0;
            NoteNm = "";
        }

        private void txtNote_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtNote, "");
                if (txtNote.Text.Trim() != "")
                {
                    if (NoteNm != txtNote.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MCashDenomination where RSType = " + Convert.ToInt64(cmbRSType.SelectedValue) + " and Note = '" + txtNote.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtNote, "Duplicate Note");
                            EP.SetIconAlignment(txtNote, ErrorIconAlignment.MiddleRight);
                            txtNote.Focus();
                        }
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CashDenominationNo),0)as CashDenominationNo From MCashDenomination").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashDenominationNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CashDenominationNo),0)as CashDenominationNo From MCashDenomination").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashDenominationNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CashDenominationNo),0)as CashDenominationNo From MCashDenomination where  CashDenominationNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashDenominationNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CashDenominationNo),0)as CashDenominationNo From MCashDenomination where CashDenominationNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CashDenominationNo"].ToString());
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
            //else if (e.KeyCode == Keys.Escape)
            //{
            //    BtnExit_Click(sender, e);
            //}
        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {

            try
            {
                ID = 0;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                NoteNm = "";
                txtNote.Focus();
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
            txtNote.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CashDenomination NewF = new CashDenomination();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbMCashDenomination = new DBMCashDenomination();
                mCashDenomination = new MCashDenomination();

                mCashDenomination.CashDenominationNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbMCashDenomination.DeleteMCashDenomination(mCashDenomination) == true)
                    {
                        OMMessageBox.Show("Record Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();

                    }
                    else
                    {
                        OMMessageBox.Show("Record Not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtSerialNo, -1, 15);
        }

        private void txtNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNote.Text == "")
                {
                    txtNote.Focus();
                }
                else
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    cmbRSType.Focus();
                    SendKeys.Send("F4");
                }

            }
        }

        private void GVRS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void cmbRSType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbRSType.Text.Trim() == "")
                {
                    cmbRSType.Focus();
                    SendKeys.Send("F4");
                    cmbRSType.SelectedValue = "0";
                }
                else
                {
                    e.SuppressKeyPress = true;
                    txtSerialNo.Focus();
                }
            }
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                chkActive.Focus();
            }
        }

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                BtnSave.Focus();
            }
        }

    }
}
