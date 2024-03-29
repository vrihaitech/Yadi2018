﻿using System;
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
    public partial class BranchAE : Form
    {
       
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMBranch dbBranch = new DBMBranch();
        MBranch mBranch = new MBranch();
        string BranchNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long BranchNo, ID;
        public long ShortID = 0;
        
        public BranchAE()
        {
            InitializeComponent();
        }
        public BranchAE(long shortid)
        {
            InitializeComponent();
            ShortID = shortid;
        }
        private void BranchAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                BranchNm = "";
                if (ShortID == 0)
                {
                    dtSearch = ObjFunction.GetDataView("Select BranchNo From MBranch order by BranchName").Table;

                    if (dtSearch.Rows.Count > 0)
                    {
                        if (Branch.RequestBranchNo == 0)
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                        else
                            ID = Branch.RequestBranchNo;
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
                    chkActive.Checked = true;
                }
                KeyDownFormat(this.Controls);
                if (ShortID != 0)
                {
                    txtBranchName.Focus();
                }
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
                EP.SetError(txtBranchName, "");

                mBranch = dbBranch.ModifyMBranchByID(ID);
                BranchNm = mBranch.BranchName;
                txtBranchName.Text = mBranch.BranchName;
                chkActive.Checked = mBranch.IsActive;
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
                    dbBranch = new DBMBranch();
                    mBranch = new MBranch();
                    mBranch.BranchNo = ID;

                    mBranch.BranchName = txtBranchName.Text.Trim();
                    mBranch.IsActive = chkActive.Checked;
                    mBranch.UserID = DBGetVal.UserID;
                    mBranch.UserDate = DBGetVal.ServerTime.Date;
                    mBranch.CompanyNo = DBGetVal.FirmNo;

                    if (dbBranch.AddMBranch(mBranch) == true)
                    {
                        if (ID == 0)
                        {
                            //MessageBox.Show("Branch Added Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OMMessageBox.Show("Branch Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select BranchNo From MBranch order by BranchName").Table;
                            ID = ObjQry.ReturnLong("Select Max(BranchNo) From MBranch", CommonFunctions.ConStr);

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
                            //MessageBox.Show("Branch Updated Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            OMMessageBox.Show("Branch Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        //MessageBox.Show("Branch not saved", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        OMMessageBox.Show("Branch Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
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
            Branch.RequestBranchNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtBranchName, "");
           
            if (txtBranchName.Text.Trim() == "")
            {

                EP.SetError(txtBranchName, "Enter Branch Name");
                EP.SetIconAlignment(txtBranchName, ErrorIconAlignment.MiddleRight);
                txtBranchName.Focus();
            }
            else if (BranchNm != txtBranchName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MBranch where BranchName = '" + txtBranchName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtBranchName, "Duplicate Branch Name");
                    EP.SetIconAlignment(txtBranchName, ErrorIconAlignment.MiddleRight);
                    txtBranchName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void BranchAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Branch.RequestBranchNo = 0;
            BranchNm = "";
        }

        private void txtBranchName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtBranchName, "");
                if (txtBranchName.Text.Trim() != "")
                {
                    if (BranchNm != txtBranchName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MBranch where BranchName = '" + txtBranchName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtBranchName, "Duplicate Branch Name");
                            EP.SetIconAlignment(txtBranchName, ErrorIconAlignment.MiddleRight);
                            txtBranchName.Focus();
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
                long No = 0;
                if (type == 5)
                {
                    No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    ID = No;
                }
                if (type == 1)
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                BranchNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtBranchName.Focus();
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
            txtBranchName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ShortID == 0)
                {
                    NavigationDisplay(5);
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    btnNew.Focus();
                }
                else this.Close();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Branch();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbBranch = new DBMBranch();
                mBranch = new MBranch();

                mBranch.BranchNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbBranch.DeleteMBranch(mBranch) == true)
                    {
                        //MessageBox.Show("Branch Deleted Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        OMMessageBox.Show("Branch Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        //MessageBox.Show("Branch not Deleted", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        OMMessageBox.Show("Branch not Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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

        private void txtBranchName_KeyDown(object sender, KeyEventArgs e)
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

    

