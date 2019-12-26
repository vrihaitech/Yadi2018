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
    public partial class OtherBankAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMOtherBank dbOtherBank = new DBMOtherBank();
        MOtherBank mOtherBank = new MOtherBank();
        string OtherBankNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long OtherBankNo, ID;
        public long ShortID = 0, No=0;

        public OtherBankAE()
        {
            InitializeComponent();
        }
        public OtherBankAE(long shortid)
        {
            InitializeComponent();
            ShortID = shortid;
        }
        private void OtherBankAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                OtherBankNm = "";
                if (ShortID == 0)
                {
                    dtSearch = ObjFunction.GetDataView("Select BankNo From MOtherBank order by BankName").Table;
                    if (dtSearch.Rows.Count > 0)
                    {
                        if (OtherBank.RequestBankNo == 0)
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                        else
                            ID = OtherBank.RequestBankNo;
                        FillControls();
                        SetNavigation();
                    }
                    setDisplay(true);
                    btnNew.Focus();
                }
                else
                {
                    btnNew_Click(sender, new EventArgs());
                    setDisable(false);
                    txtBabkName.Focus();
                    chkActive.Checked = true;
                }
                KeyDownFormat(this.Controls);
                if (ShortID != 0)
                    txtBabkName.Focus();
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
                EP.SetError(txtBabkName, "");

                mOtherBank = dbOtherBank.ModifyMBankByID(ID);
                OtherBankNm = mOtherBank.BankName;
                txtBabkName.Text = mOtherBank.BankName;
                chkActive.Checked = mOtherBank.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";
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
                    dbOtherBank = new DBMOtherBank();
                    mOtherBank = new MOtherBank();
                    mOtherBank.BankNo = ID;

                    mOtherBank.BankName = txtBabkName.Text.Trim();
                    mOtherBank.IsActive = chkActive.Checked;
                    mOtherBank.UserID = DBGetVal.UserID;
                    mOtherBank.UserDate = DBGetVal.ServerTime.Date;
                    mOtherBank.CompanyNo = DBGetVal.FirmNo;

                    if (dbOtherBank.AddMBank(mOtherBank) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Bank Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select BankNo From MOtherBank order by BankName").Table;
                            ID = ObjQry.ReturnLong("Select Max(BankNo) From MOtherBank", CommonFunctions.ConStr);
                            if (ShortID == 0)
                            {
                                SetNavigation();
                                FillControls();
                            }
                            else
                            {
                                ShortID = ID;
                                this.Close();
                            }
                        }
                        else
                        {
                            OMMessageBox.Show("Bank Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                            ShortID = 0;
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
            OtherBank.RequestBankNo = 0;
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ShortID = ID;
                ID = 0;
                OtherBankNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtBabkName.Focus();
                chkActive.Checked = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtBabkName, "");
           
            if (txtBabkName.Text.Trim() == "")
            {
                EP.SetError(txtBabkName, "Enter Bank Name");
                EP.SetIconAlignment(txtBabkName, ErrorIconAlignment.MiddleRight);
                txtBabkName.Focus();
            }
            else if (OtherBankNm != txtBabkName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MOtherBank where BankName = '" + txtBabkName.Text.Trim().Replace("'", "''") + "' and BankNo not in (" + ID + ")", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtBabkName, "Duplicate Bank Name");
                    EP.SetIconAlignment(txtBabkName, ErrorIconAlignment.MiddleRight);
                    txtBabkName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void OtherBankAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            OtherBank.RequestBankNo = 0;
            OtherBankNm = "";
        }

        //private void txtBranchName_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        EP.SetError(txtBabkName, "");
        //        if (txtBabkName.Text.Trim() != "")
        //        {
        //            if (OtherBankNm != txtBabkName.Text.Trim())
        //            {
        //                if (ObjQry.ReturnInteger("Select Count(*) from mBank where BankName = '" + txtBabkName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
        //                {
        //                    EP.SetError(txtBabkName, "Duplicate Bank Name");
        //                    EP.SetIconAlignment(txtBabkName, ErrorIconAlignment.MiddleRight);
        //                    txtBabkName.Focus();
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(BankNo),0)as BankNo From MOtherBank ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["BankNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(BankNo),0)as BankNo From MOtherBank ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["BankNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(BankNo),0)as BankNo From MOtherBank where BankNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["BankNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(BankNo),0)as BankNo From MOtherBank where  BankNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["BankNo"].ToString());
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
        public void setDisable(bool flag)
        {
            BtnSave.Visible = !flag;
            btnCancel.Visible = !flag;
            btnUpdate.Visible = flag;
            btnSearch.Visible = flag;
            BtnExit.Visible = flag;
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ShortID != 0)
            {
                ID = ShortID;
                NavigationDisplay(5);
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                btnNew.Focus();
            }
           // else this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new OtherBank();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbOtherBank = new DBMOtherBank();
                mOtherBank = new MOtherBank();

                mOtherBank.BankNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbOtherBank.DeleteMBank(mOtherBank) == true)
                    {
                        OMMessageBox.Show("Bank Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Bank not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ShortID = ID;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtBabkName.Focus();
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

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void txtBabkName_KeyDown(object sender, KeyEventArgs e)
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
