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
    public partial class CustomerTypeAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMCustomerType dbCustomerType = new DBMCustomerType();
        MCustomerType mCustomerType = new MCustomerType();
        string CustomerTypeNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long CustomerTypeNo, ID;

        public CustomerTypeAE()
        {
            InitializeComponent();
        }
      
        private void CustomerTypeAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                CustomerTypeNm = "";
                dtSearch = ObjFunction.GetDataView("Select CustomerTypeNo From MCustomerType order by CustomerTypeName").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (CustomerType.RequestCustomerTypeNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = CustomerType.RequestCustomerTypeNo;
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
                EP.SetError(txtCustomerTypeName, "");
                EP.SetError(txtDesc, "");
                mCustomerType = dbCustomerType.ModifyMCustomerTypeByID(ID);
                CustomerTypeNm = mCustomerType.CustomerTypeName;
                txtCustomerTypeName.Text = mCustomerType.CustomerTypeName;
                txtDesc.Text = mCustomerType.CustomerTypeDesc;
                chkActive.Checked = mCustomerType.IsActive;
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
                    dbCustomerType = new DBMCustomerType();
                    mCustomerType = new MCustomerType();
                    mCustomerType.CustomerTypeNo = ID;
                    mCustomerType.CustomerTypeName = txtCustomerTypeName.Text.Trim().ToUpper();
                    mCustomerType.CustomerTypeDesc = txtDesc.Text.Trim().ToUpper();
                    mCustomerType.IsActive = chkActive.Checked;
                    mCustomerType.UserID = DBGetVal.UserID;
                    mCustomerType.UserDate = DBGetVal.ServerTime.Date;
                    mCustomerType.CompanyNo = DBGetVal.FirmNo;
                    

                    if (dbCustomerType.AddMCustomerType(mCustomerType) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("CustomerType Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select CustomerTypeNo From MCustomerType order by CustomerTypeName").Table;
                            ID = ObjQry.ReturnLong("Select Max(CustomerTypeNo) From MCustomerType", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("CustomerType Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("CustomerType not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            CustomerType.RequestCustomerTypeNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtCustomerTypeName, ""); 
            EP.SetError(txtDesc, "");

            if (txtCustomerTypeName.Text.Trim() == "")
            {
                EP.SetError(txtCustomerTypeName, "Enter CustomerType Name");
                EP.SetIconAlignment(txtCustomerTypeName, ErrorIconAlignment.MiddleRight);
                txtCustomerTypeName.Focus();
            }
            else if (txtDesc.Text.Trim() == "")
            {
                EP.SetError(txtDesc, "Enter Description");
                EP.SetIconAlignment(txtDesc, ErrorIconAlignment.MiddleRight);
                txtDesc.Focus();
            }
            else if (CustomerTypeNm != txtCustomerTypeName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MCustomerType where CustomerTypeName = '" + txtCustomerTypeName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtCustomerTypeName, "Duplicate CustomerType Name");
                    EP.SetIconAlignment(txtCustomerTypeName, ErrorIconAlignment.MiddleRight);
                    txtCustomerTypeName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void CustomerTypeAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            CustomerType.RequestCustomerTypeNo = 0;
            CustomerTypeNm = "";
        }

        private void txtCustomerTypeName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtCustomerTypeName, "");
                if (txtCustomerTypeName.Text.Trim() != "")
                {
                    if (CustomerTypeNm != txtCustomerTypeName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MCustomerType where CustomerTypeName = '" + txtCustomerTypeName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtCustomerTypeName, "Duplicate CustomerType Name");
                            EP.SetIconAlignment(txtCustomerTypeName, ErrorIconAlignment.MiddleRight);
                            txtCustomerTypeName.Focus();
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                CustomerTypeNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtCustomerTypeName.Focus();
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
            txtCustomerTypeName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new CustomerType();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbCustomerType = new DBMCustomerType();
                mCustomerType = new MCustomerType();

                mCustomerType.CustomerTypeNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbCustomerType.DeleteMCustomerType(mCustomerType) == true)
                    {
                        OMMessageBox.Show("CustomerType Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("CustomerType not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
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

        private void txtCustomerTypeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDesc.GotFocus += delegate { txtDesc.Select(txtDesc.Text.Length, txtDesc.Text.Length); };
            }
        }
    }
}
