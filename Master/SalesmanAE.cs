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
    public partial class SalesmanAE : Form
    {
        OMCommonClass cc = new OMCommonClass();
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMSalesman dbSalesman = new DBMSalesman();
        MSalesman mSalesman = new MSalesman();
        string SalesmanNm; long SMNo;
        DataTable dtSearch = new DataTable();
        int cntRow;
        long ID;

        public SalesmanAE()
        {
            InitializeComponent();
        }

        private void SalesmanAE_Load(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            SalesmanNm = ""; SMNo = 0;
            dtSearch = ObjFunction.GetDataView("Select SalesmanNo From MSalesman order by SalesmanName").Table;
            if (dtSearch.Rows.Count > 0)
            {
                if (Salesman.RequestSalesmanNo == 0)
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                else
                    ID = Salesman.RequestSalesmanNo;
                FillControls();
                SetNavigation();
            }
            setDisplay(true);
            btnNew.Focus();
            KeyDownFormat(this.Controls);


        }

        private void FillControls()
        {
            EP.SetError(txtSalesmanName, "");
            EP.SetError(txtShortName, "");
            EP.SetError(txtAddress, "");
            EP.SetError(txtPinCode, "");
            //EP.SetError(txtMobileName, "");
            EP.SetError(txtRs, "");
            EP.SetError(txtDisc, "");

            mSalesman = dbSalesman.ModifyMSalesmanByID(ID);
            SalesmanNm = mSalesman.SalesmanName;
            txtSalesmanName.Text = mSalesman.SalesmanName;
            txtShortName.Text = mSalesman.SalesmanUserNo.ToString();
            SMNo = mSalesman.SalesmanUserNo;
            txtAddress.Text = mSalesman.Address;
            txtPinCode.Text = mSalesman.PinCode;
            txtPhone.Text = mSalesman.PhoneNo;
            txtMobileName.Text = mSalesman.Mobile;
            txtDisc.Text = mSalesman.Disc.ToString();
            txtRs.Text = mSalesman.Rupees.ToString(); ;
            chkActive.Checked = mSalesman.IsActive;
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";



        }

        public void SetValue()
        {
            if (Validations() == true)
            {
                dbSalesman = new DBMSalesman();
                mSalesman = new MSalesman();
                mSalesman.SalesmanNo = ID;

                mSalesman.SalesmanName = txtSalesmanName.Text.Trim().ToUpper();
                mSalesman.SalesmanUserNo = Convert.ToInt64(txtShortName.Text);
                mSalesman.Address = txtAddress.Text.Trim();
                mSalesman.PinCode = txtPinCode.Text.Trim();
                mSalesman.PhoneNo = txtPhone.Text.Trim();
                mSalesman.Mobile = txtMobileName.Text.Trim();
                if (txtDisc.Text == "")
                    txtDisc.Text = "0";
                mSalesman.Disc = Convert.ToDouble(txtDisc.Text.Trim());
                if (txtRs.Text == "")
                    txtRs.Text = "0";
                mSalesman.Rupees = Convert.ToDouble(txtRs.Text.Trim());
                mSalesman.IsActive = chkActive.Checked;
                mSalesman.UserID = DBGetVal.UserID;
                mSalesman.UserDate = DBGetVal.ServerTime.Date;

                if (dbSalesman.AddMSalesman(mSalesman) == true)
                {
                    MessageBox.Show("Salesman Added Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (ID == 0)
                    {
                        dtSearch = ObjFunction.GetDataView("Select SalesmanNo From MSalesman order by SalesmanName").Table;
                        ID = ObjQry.ReturnLong("Select Max(SalesmanNo) FRom MSalesman", CommonFunctions.ConStr);
                        SetNavigation();
                        FillControls();
                    }
                    else
                    {
                        FillControls();
                    }

                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                }
                else
                {
                    MessageBox.Show("Salesman not saved", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Salesman.RequestSalesmanNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtSalesmanName, ""); EP.SetError(txtShortName, "");
            EP.SetError(txtAddress, ""); EP.SetError(txtPinCode, "");
            //EP.SetError(txtMobileName, ""); 
            EP.SetError(txtDisc, "");
            EP.SetError(txtRs, "");
            if (txtRs.Text.Trim() == "")
                txtRs.Text = "0";
            if (txtDisc.Text.Trim() == "")
                txtDisc.Text = "0";

            //if (txtPinCode.Text.Trim() != "")
            //{
            //    flag = ObjFunction.CheckNumeric(txtPinCode.Text.Trim());
            //    if (flag == false)
            //    {
            //        EP.SetError(txtPinCode, "Enter Numeric Value");
            //        EP.SetIconAlignment(txtPinCode, ErrorIconAlignment.MiddleRight);
            //        txtPinCode.Focus();
            //    }
            //}
            if (ObjFunction.CheckValidAmount(txtRs.Text.Trim()) == false)
            {

                EP.SetError(txtRs, "Enter Valid Amount");
                EP.SetIconAlignment(txtRs, ErrorIconAlignment.MiddleRight);
                txtRs.Focus();




            }
            else if (ObjFunction.CheckValidAmount(txtDisc.Text.Trim()) == false)
            {

                EP.SetError(txtDisc, "Enter Valid Amount");
                EP.SetIconAlignment(txtDisc, ErrorIconAlignment.MiddleRight);
                txtDisc.Focus();


            }
            else if (txtSalesmanName.Text.Trim() == "")
            {
                EP.SetError(txtSalesmanName, "Enter Salesman Name");
                EP.SetIconAlignment(txtSalesmanName, ErrorIconAlignment.MiddleRight);
                txtSalesmanName.Focus();
            }

            else if (txtShortName.Text.Trim() == "")
            {
                EP.SetError(txtShortName, "Enter User No");
                EP.SetIconAlignment(txtShortName, ErrorIconAlignment.MiddleRight);
                txtShortName.Focus();
            }
            else if (ObjFunction.CheckNumeric(txtShortName.Text) == false)
            {
                EP.SetError(txtShortName, "Enter valid User No");
                EP.SetIconAlignment(txtShortName, ErrorIconAlignment.MiddleRight);
                txtShortName.Focus();
            }
            else if (txtAddress.Text.Trim() == "")
            {
                EP.SetError(txtAddress, "Enter Address");
                EP.SetIconAlignment(txtAddress, ErrorIconAlignment.MiddleRight);
                txtAddress.Focus();
            }
            else if (txtPinCode.Text.Trim() == "")
            {
                EP.SetError(txtPinCode, "Enter Pin Code");
                EP.SetIconAlignment(txtPinCode, ErrorIconAlignment.MiddleRight);
                txtPinCode.Focus();
            }
            else if ((Convert.ToDouble(txtDisc.Text.Trim()) > 0) && (Convert.ToDouble(txtRs.Text.Trim())) > 0)
            {
                MessageBox.Show("Enter Only One Commission Type", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRs.Focus();
            }
            //else if (txtMobileName.Text.Trim() == "")
            //{
            //    EP.SetError(txtMobileName, "Enter Mobile No");
            //    EP.SetIconAlignment(txtMobileName, ErrorIconAlignment.MiddleRight);
            //    txtMobileName.Focus();
            //}

            //else if (txtDisc.Text.Trim() == "")
            //{
            //    EP.SetError(txtDisc, "Enter Commision");
            //    EP.SetIconAlignment(txtDisc, ErrorIconAlignment.MiddleRight);
            //    txtDisc.Focus();
            //}


            else if (SalesmanNm != txtSalesmanName.Text)
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MSalesman where SalesmanName = '" + txtSalesmanName.Text + "' and SalesmanNo != " + ID + "", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtSalesmanName, "Duplicate Salesman Name");
                    EP.SetIconAlignment(txtSalesmanName, ErrorIconAlignment.MiddleRight);
                    txtSalesmanName.Focus();
                }
                else
                    flag = true;
            }


            //else if (txtMobile.Text.Trim() != "")
            //{
            //    flag = ObjFunction.CheckNumeric(txtMobile.Text.Trim());
            //    if (flag == false)
            //    {
            //        EP.SetError(txtMobile, "Enter Numeric Value");
            //        EP.SetIconAlignment(txtMobile, ErrorIconAlignment.MiddleRight);
            //        txtMobile.Focus();
            //    }
            //}

            else
                flag = true;

            if (flag == true)
            {
                flag = false;
                if (SMNo.ToString() != txtShortName.Text)
                {
                    if (ObjQry.ReturnInteger("Select Count(*) from MSalesman where SalesmanUserNo = " + txtShortName.Text + "", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtShortName, "Duplicate Salesman UserNo");
                        EP.SetIconAlignment(txtShortName, ErrorIconAlignment.MiddleRight);
                        txtShortName.Focus();
                    }
                    else
                        flag = true;
                }
                else
                    flag = true;
            }
            return flag;
        }

        private void SalesmanAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            SalesmanNm = "";
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
                    MessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cntRow = cntRow + 1;
                }
                else if (cntRow > dtSearch.Rows.Count - 1)
                {
                    MessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }

        }
        #endregion

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
            {
                chkActive.Text = "Yes";
            }
            else
            {
                chkActive.Text = "No";
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            dbSalesman = new DBMSalesman();
            mSalesman = new MSalesman();

            mSalesman.SalesmanNo = ID;
            if (MessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dbSalesman.DeleteMSalesman(mSalesman) == true)
                {
                    MessageBox.Show("Salesman Deleted Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FillControls();
                }
                else
                {
                    MessageBox.Show("Salesman not Deleted", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Salesman();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            SalesmanNm = "";
            ID = 0;
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtSalesmanName.Focus();
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
            txtSalesmanName.Focus();
        }

        private void txtDisc_Leave(object sender, EventArgs e)
        {
            if (txtDisc.Text == "")
            {
                txtDisc.Text = "0";
                chkActive.Focus();
            }
            if (ObjFunction.CheckValidAmount(txtDisc.Text.Trim()) == false)
            {

                EP.SetError(txtDisc, "Enter Valid Amount");
                EP.SetIconAlignment(txtDisc, ErrorIconAlignment.MiddleRight);
                txtDisc.Focus();

            }
            else if (Convert.ToDouble(txtDisc.Text) != 0)
            {
                txtRs.Text = "0";
                txtRs.Enabled = false;
                chkActive.Focus();
            }


        }

        private void txtSalesmanName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShortName.Focus();
            }
        }

        private void txtShortName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress.Focus();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPinCode.Focus();
            }
        }

        private void txtPinCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPhone.Focus();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMobileName.Focus();
            }
        }

        private void txtMobileName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisc.Focus();
            }
        }

        private void txtDisc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkActive.Focus();
            }
        }

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void txtRs_Leave(object sender, EventArgs e)
        {
            if ((txtRs.Text == "") || (txtRs.Text == "0"))
            {
                txtRs.Text = "0";
                txtDisc.Focus();
            }
            if (ObjFunction.CheckValidAmount(txtRs.Text.Trim()) == false)
            {

                EP.SetError(txtRs, "Enter Valid Amount");
                EP.SetIconAlignment(txtDisc, ErrorIconAlignment.MiddleRight);
                txtRs.Focus();

            }
            else if (Convert.ToDouble(txtRs.Text) != 0)
            {
                txtDisc.Text = "0";
                txtDisc.Enabled = false;
                chkActive.Focus();
            }

        }

    }
}
