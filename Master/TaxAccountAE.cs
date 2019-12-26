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
    public partial class TaxAccountAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        string LedgerNm;
        DataTable dtSearch = new DataTable();
        int cntRow, LedgerUserNo;//RequestLedgerNo, Groupno
        long ID;
        bool isDoProcess = false;

        public TaxAccountAE()
        {
            InitializeComponent();
        }

        private void TaxAccountAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbGroupName, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") or GroupNo in (Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + ")) and IsActive='true'");

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                dtSearch = ObjFunction.GetDataView("Select LedgerNo From MLedger Where LedgerNo >15 AND GroupNo in(" + GroupType.DutiesAndTaxes + ") or GroupNo in(Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + ")) order by LedgerName").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (TaxAccount.RequestLedgerNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = TaxAccount.RequestLedgerNo;
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
                EP.SetError(txtAccName, ""); EP.SetError(cmbGroupName, "");

                mLedger = dbLedger.ModifyMLedgerByID(ID);
                LedgerNm = mLedger.LedgerName.ToUpper();
                txtAccName.Text = mLedger.LedgerName;
                ObjFunction.FillCombo(cmbGroupName, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") or GroupNo in (Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + ")) and (IsActive='true' or GroupNo=" + mLedger.GroupNo + ")");
                cmbGroupName.SelectedValue = mLedger.GroupNo.ToString();
                chkActive.Checked = mLedger.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";

                LedgerUserNo = Convert.ToInt32(mLedger.LedgerUserNo);
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

                    mLedger.LedgerName = txtAccName.Text.Trim().ToUpper();
                    mLedger.GroupNo = ObjFunction.GetComboValue(cmbGroupName);
                    mLedger.ContactPerson = "";
                    mLedger.InvFlag = false;
                    mLedger.MaintainBillByBill = false;
                    mLedger.IsActive = chkActive.Checked;
                    mLedger.CompanyNo = DBGetVal.FirmNo;
                    mLedger.LedgerStatus = (ID == 0) ? 1 : 2;
                    mLedger.LedgerLangName = "";
                    mLedger.UserID = DBGetVal.UserID;
                    mLedger.UserDate = DBGetVal.ServerTime.Date;
                    dbLedger.AddMLedger(mLedger);

                    if (dbLedger.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Tax Accounts Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select LedgerNo From MLedger  Where LedgerNo >15 AND GroupNo in(" + GroupType.DutiesAndTaxes + ") or GroupNo in(Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + "))order by LedgerName").Table;
                            ID = ObjQry.ReturnLong("Select Max(LedgerNo) FRom MLedger", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Tax Accounts Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Accounts not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtAccName, ""); EP.SetError(cmbGroupName, "");

            if (txtAccName.Text.Trim() == "")
            {
                EP.SetError(txtAccName, "Enter Account Name");
                EP.SetIconAlignment(txtAccName, ErrorIconAlignment.MiddleRight);
                txtAccName.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbGroupName) <= 0)
            {
                EP.SetError(cmbGroupName, "Select Group Name");
                EP.SetIconAlignment(cmbGroupName, ErrorIconAlignment.MiddleRight);
                cmbGroupName.Focus();
            }
            else if (LedgerNm != txtAccName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MLedger INNER JOIN  MGroup ON MLedger.GroupNo = MGroup.GroupNo " +
                          "where (MLedger.LedgerNo >15) AND (MLedger.GroupNo in(" + GroupType.DutiesAndTaxes + ") or MLedger.GroupNo in(Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + "))) and MLedger.LedgerName = '" + txtAccName.Text.Trim().Replace("'", "''") + "' AND LedgerNo <> " + ID + "", CommonFunctions.ConStr) != 0)
                //if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtAccName.Text.Trim() + "' AND LedgerNo <> " + ID, CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtAccName, "Duplicate Tax Account Name");
                    EP.SetIconAlignment(txtAccName, ErrorIconAlignment.MiddleRight);
                    txtAccName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void TaxAccountAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            LedgerNm = "";
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
            btnDelete.Visible = flag;
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
                if (btnSave.Visible) BtnSave_Click(sender, e);
            }

        }
        #endregion

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
                        OMMessageBox.Show("Tax Accounts Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Tax Accounts not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new TaxAccount();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                LedgerNm = "";
                mLedger = new MLedger();
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                ObjFunction.FillCombo(cmbGroupName, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") or GroupNo in (Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + ")) and IsActive='true'");
                cmbGroupName.SelectedIndex = 0;
                chkActive.Checked = true;
                txtAccName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtAccName.Focus();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {

        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void txtAccName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtAccName, "");
                if (txtAccName.Text.Trim() != "")
                {
                    if (LedgerNm != txtAccName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MLedger INNER JOIN  MGroup ON MLedger.GroupNo = MGroup.GroupNo " +
                              "where (MLedger.LedgerNo >15) AND (MLedger.GroupNo in(" + GroupType.DutiesAndTaxes + ") or MLedger.GroupNo in(Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + "))) and MLedger.LedgerName = '" + txtAccName.Text.Trim().Replace("'", "''") + "' AND LedgerNo <> " + ID + "", CommonFunctions.ConStr) != 0)
                        //if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtAccName.Text + "' AND LedgerNo <> " + ID, CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtAccName, "Duplicate Tax Account Name");
                            EP.SetIconAlignment(txtAccName, ErrorIconAlignment.MiddleRight);
                            txtAccName.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TaxAccountAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                    if (cmbGroupName.Enabled == true)
                    {
                        long CId = ObjFunction.GetComboValue(cmbGroupName);
                        ObjFunction.FillCombo(cmbGroupName, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") or GroupNo in (Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + ")) and (IsActive='true' or GroupNo=" + mLedger.GroupNo + ")");
                        cmbGroupName.SelectedValue = CId;
                    }
                    isDoProcess = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TaxAccountAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

    }
}
