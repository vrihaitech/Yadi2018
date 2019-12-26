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
    public partial class CustomerSizeAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMCustomerSize dbCustomerSize = new DBMCustomerSize();
        MCustomerSize mCustomerSize = new MCustomerSize();
        string CustomerSizeNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long CustomerSizeNo, ID;

        public CustomerSizeAE()
        {
            InitializeComponent();
        }
      
        private void CustomerSizeAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                CustomerSizeNm = "";
                dtSearch = ObjFunction.GetDataView("Select CustSizeNo From MCustomerSize order by CustSizeName").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (CustomerSize.RequestCustomerSizeNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = CustomerSize.RequestCustomerSizeNo;
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
                EP.SetError(txtCustomerSizeName, "");
                EP.SetError(txtDesc, "");
                mCustomerSize = dbCustomerSize.ModifyMCustomerSizeByID(ID);
                CustomerSizeNm = mCustomerSize.CustSizeName;
                txtCustomerSizeName.Text = mCustomerSize.CustSizeName;
                txtDesc.Text = mCustomerSize.CustSizeDesc;
                chkActive.Checked = mCustomerSize.IsActive;
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
                    dbCustomerSize = new DBMCustomerSize();
                    mCustomerSize = new MCustomerSize();
                    mCustomerSize.CompanyNo = ID;
                    mCustomerSize.CustSizeName = txtCustomerSizeName.Text.Trim().ToUpper();
                    mCustomerSize.CustSizeDesc = txtDesc.Text.Trim().ToUpper();
                    mCustomerSize.IsActive = chkActive.Checked;
                    mCustomerSize.UserID = DBGetVal.UserID;
                    mCustomerSize.UserDate = DBGetVal.ServerTime.Date;
                    mCustomerSize.CompanyNo = DBGetVal.FirmNo;

                    if (dbCustomerSize.AddMCustomerSize(mCustomerSize) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("CustomerSize Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select CustSizeNo From MCustomerSize order by CustSizeName").Table;
                            ID = ObjQry.ReturnLong("Select Max(CustSizeNo) From MCustomerSize", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("CustomerSize Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("CustomerSize not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            CustomerSize.RequestCustomerSizeNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtCustomerSizeName, ""); 
            EP.SetError(txtDesc, "");

            if (txtCustomerSizeName.Text.Trim() == "")
            {
                EP.SetError(txtCustomerSizeName, "Enter CustomerSize Name");
                EP.SetIconAlignment(txtCustomerSizeName, ErrorIconAlignment.MiddleRight);
                txtCustomerSizeName.Focus();
            }
            else if (txtDesc.Text.Trim() == "")
            {
                EP.SetError(txtDesc, "Enter Description");
                EP.SetIconAlignment(txtDesc, ErrorIconAlignment.MiddleRight);
                txtDesc.Focus();
            }
            else if (CustomerSizeNm != txtCustomerSizeName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MCustomerSize where CustSizeName = '" + txtCustomerSizeName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtCustomerSizeName, "Duplicate CustomerSize Name");
                    EP.SetIconAlignment(txtCustomerSizeName, ErrorIconAlignment.MiddleRight);
                    txtCustomerSizeName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void CustomerSizeAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            CustomerSize.RequestCustomerSizeNo = 0;
            CustomerSizeNm = "";
        }

        private void txtCustomerSizeName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtCustomerSizeName, "");
                if (txtCustomerSizeName.Text.Trim() != "")
                {
                    if (CustomerSizeNm != txtCustomerSizeName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MCustomerSize where CustSizeName = '" + txtCustomerSizeName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtCustomerSizeName, "Duplicate Size ");
                            EP.SetIconAlignment(txtCustomerSizeName, ErrorIconAlignment.MiddleRight);
                            txtCustomerSizeName.Focus();
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
                CustomerSizeNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtCustomerSizeName.Focus();
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
            txtCustomerSizeName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new CustomerSize();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbCustomerSize = new DBMCustomerSize();
                mCustomerSize = new MCustomerSize();

                mCustomerSize.CustSizeNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbCustomerSize.DeleteMCustomerSize(mCustomerSize) == true)
                    {
                        OMMessageBox.Show("CustomerSize Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("CustomerSize not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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

        private void txtCustomerSizeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDesc.GotFocus += delegate { txtDesc.Select(txtDesc.Text.Length, txtDesc.Text.Length); };
            }
        }
    }
}
