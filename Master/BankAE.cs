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
    public partial class BankAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();



        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        MLedgerDetails mLedgerDetails = new MLedgerDetails();
        string BankNm, mobNo;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long BankNo, ID = 0, LedgerUserNo = 0, No=0;

        public BankAE()
        {
            InitializeComponent();
        }
        private void BankAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                BankNm = "";
                dtSearch = ObjFunction.GetDataView("Select LedgerNo,LedgerName From MLedger where GroupNo=" + GroupType.BankAccounts + " order by LedgerName").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    if (Bank.RequestBankNo != 0)
                    {
                        ID = Bank.RequestBankNo;
                        BankNm = "";
                    }
                    else
                    {
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString()); ;
                    }
                    FillControls();
                    SetNavigation();
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
                EP.SetError(txtBankName, "");
                EP.SetError(txtContactNo, "");

                mLedger = dbLedger.ModifyMLedgerByID(ID);
                BankNm = mLedger.LedgerName;
                txtBankName.Text = mLedger.LedgerName;

                txtContactNo.Text = mLedger.ContactPerson;
                mobNo = mLedger.ContactPerson;
                LedgerUserNo = Convert.ToInt32(mLedger.LedgerUserNo);
                chkActive.Checked = mLedger.IsActive;

                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";

                mLedgerDetails = dbLedger.ModifyMLedgerDetailsByID(ID);

                txtAccountNo.Text = mLedgerDetails.AccountNo;
                txtReportName.Text = mLedgerDetails.ReportName;
                txtAddress.Text = mLedgerDetails.Address.Trim();
                txtifsccode.Text = mLedgerDetails.GSTNo;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {

                    dbLedger = new DBMLedger();
                    mLedger = new MLedger();
                    mLedger.LedgerNo = ID;
                    mLedger.LedgerUserNo = LedgerUserNo.ToString();
                    mLedger.LedgerName = txtBankName.Text.Trim();
                    mLedger.GroupNo = GroupType.BankAccounts;


                    mLedger.InvFlag = false;
                    mLedger.MaintainBillByBill = false;

                    mLedger.ContactPerson = txtContactNo.Text.Trim();
                    mLedger.CompanyNo = DBGetVal.FirmNo;
                    mLedger.LedgerStatus = (ID == 0) ? 1 : 2;
                    mLedger.LedgerLangName = "";
                    mLedger.IsActive = chkActive.Checked;
                    mLedger.UserID = DBGetVal.UserID;
                    mLedger.UserDate = DBGetVal.ServerTime.Date;

                    if (dbLedger.AddMLedger(mLedger))
                    {
                        mLedgerDetails.LedgerDetailsNo = ObjQry.ReturnLong("Select LedgerDetailsNo From MLedgerDetails Where LedgerNo=" + mLedger.LedgerNo + "", CommonFunctions.ConStr);
                        mLedgerDetails.Address = txtAddress.Text.Trim();
                        mLedgerDetails.StateNo = 0;
                        mLedgerDetails.CityNo = 0;
                        mLedgerDetails.PinCode = "";
                        mLedgerDetails.PhNo1 = "";
                        mLedgerDetails.PhNo2 = "";
                        mLedgerDetails.MobileNo1 = "";
                        mLedgerDetails.MobileNo2 = "";
                        mLedgerDetails.EmailID = "";
                        mLedgerDetails.DOB = Convert.ToDateTime("01/01/1900");
                        mLedgerDetails.QualificationNo = 0;
                        mLedgerDetails.OccupationNo = 0;
                        mLedgerDetails.CustomerType = 0;
                        mLedgerDetails.CreditLimit = 0;
                        mLedgerDetails.PANNo = "";
                        mLedgerDetails.VATNo = "";
                        mLedgerDetails.CSTNo = "";
                        mLedgerDetails.AccountNo = txtAccountNo.Text.Trim();
                        mLedgerDetails.ReportName = txtReportName.Text.Trim();
                        mLedgerDetails.AdharNo = "";
                        mLedgerDetails.UserID = DBGetVal.UserID;
                        mLedgerDetails.UserDate = DBGetVal.ServerTime.Date;
                        mLedgerDetails.IsLBTApply = false;
                        mLedgerDetails.LBTNo = "";
                        mLedgerDetails.AnyotherNo1 = "";
                        mLedgerDetails.AnyotherNo2 = "";
                        mLedgerDetails.GSTNo = txtifsccode.Text.Trim();
                        mLedgerDetails.FSSAIDate = DBGetVal.ServerTime.Date;
                        mLedgerDetails.GSTDate = DBGetVal.ServerTime.Date;
                        dbLedger.AddMLedgerDetails(mLedgerDetails);

                    }



                    if (dbLedger.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Bank Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select LedgerNo,LedgerName From MLedger where GroupNo=" + GroupType.BankAccounts + " order by LedgerName").Table;
                            ID = ObjQry.ReturnLong("Select Max(LedgerNo) From MLedger", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Bank Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();

                    }
                    else
                    {
                        OMMessageBox.Show("Bank not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            EP.SetError(txtBankName, "");
            //  EP.SetError(txtContactNo, "");
            EP.SetError(txtAccountNo, "");
            if (txtBankName.Text.Trim() == "")
            {

                EP.SetError(txtBankName, "Enter Ledger Name");
                EP.SetIconAlignment(txtBankName, ErrorIconAlignment.MiddleRight);
                txtBankName.Focus();
            }
            else if (txtAccountNo.Text.Trim() == "")
            {

                EP.SetError(txtAccountNo, "Enter account No");
                EP.SetIconAlignment(txtAccountNo, ErrorIconAlignment.MiddleRight);
                txtAccountNo.Focus();
                flag = false;

            }
            else if (BankNm != txtBankName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtBankName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtBankName, "Duplicate Ledger Name");
                    EP.SetIconAlignment(txtBankName, ErrorIconAlignment.MiddleRight);
                    txtBankName.Focus();
                }
                else
                    flag = true;
            }

            else if (txtContactNo.Text != "")
            {
                if (txtContactNo.Text.Trim().Length < 10)
                {
                    EP.SetError(txtContactNo, "Enter Valid Mobile No");
                    EP.SetIconAlignment(txtContactNo, ErrorIconAlignment.MiddleRight);
                    txtContactNo.Focus();
                    flag = false;
                }
                else if (mobNo != txtContactNo.Text.Trim())
                {
                    if (txtContactNo.Text.Trim() != "1111111111" && txtContactNo.Text.Trim() != "0")
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MLedger where ContactPerson='" + txtContactNo.Text.Trim() + "' And LedgerNo!=" + ID + "", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtContactNo, "Duplicate contact No");
                            EP.SetIconAlignment(txtContactNo, ErrorIconAlignment.MiddleRight);
                            txtContactNo.Focus();
                            flag = false;
                        }
                        else
                            // EP.SetError(txtContactNo, "");
                            flag = true;
                    }


                }
                flag = true;
            }
            else
                flag = true;

            return flag;
        }

        private void BankAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bank.RequestBankNo = 0;
            BankNm = "";
        }

        private void txtBankName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtBankName, "");
                if (txtBankName.Text.Trim() != "")
                {
                    if (BankNm != txtBankName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtBankName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtBankName, "Duplicate Ledger Name");
                            EP.SetIconAlignment(txtBankName, ErrorIconAlignment.MiddleRight);
                            txtBankName.Focus();
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.BankAccounts + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.BankAccounts + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.BankAccounts + " and  LedgerNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.BankAccounts + " and LedgerNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
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
                BankNm = "";
                txtBankName.Focus();
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
            txtBankName.Focus();
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
            Form NewF = new Bank();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbLedger = new DBMLedger();
                mLedger = new MLedger();

                mLedger.LedgerNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbLedger.DeleteMLedger(mLedger) == true)
                    {
                        OMMessageBox.Show("Ledger Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();

                    }
                    else
                    {
                        OMMessageBox.Show("Ledger not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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

        private void txtContactNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtContactNo, -1, 15);
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                txtContactNo.Focus();
            }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                txtAccountNo.Focus();
            }
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                txtifsccode.Focus();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                txtReportName.Focus();
            }
        }

        private void txtReportName_KeyDown(object sender, KeyEventArgs e)
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

        private void txtifsccode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                txtAddress.Focus();
            }
        }

        


    }
}
