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
    public partial class FirmAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMFirm dbMFirm = new DBMFirm();
        MFirm mFirm = new MFirm();
        MFirmBank mFirmBank = new MFirmBank();
        string ManufacturerCompanyNm;
        DataTable dtSearch = new DataTable();
        DataTable dtMFG = new DataTable();
        public long ManufacturerCompanyNo, ID;
        long No = 0;
        public long ShortID = 0;
        Boolean enterflag = false;

        public FirmAE()
        {
            InitializeComponent();
        }

        public FirmAE(long shortid)
        {
            InitializeComponent();
            ShortID = shortid;
        }

        

        private void MFirmAE_Load_1(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                lbladdcity.Enabled = false;
                FillListAll();
                FormatPicture();
                //txtGSTDate.CustomFormat = "dd-MMM-yy";
                //txtFSSAIDate.CustomFormat = "dd-MMM-yy";
                dtSearch = ObjFunction.GetDataView("Select max(isnull(FirmNo,0)) as FirmNo FROM MFirm group by FirmNo").Table;

               
                KeyDownFormat(this.Controls);
                if (dtSearch.Rows.Count > 0)
                {
                    if (Firm.RequestMFirmNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[0]["FirmNo"].ToString());
                    else
                        ID = Firm.RequestMFirmNo;
                    FillFields();

                }
                setDisplay(true);

                btnNew.Enabled = false;
                KeyDownFormat(this.Controls);


            }

            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void FillListAll()
        {
            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' order by CityName");
            ObjFunction.FillList(lstStateName, "Select StateNo,StateName From MState where IsActive='true' order by StateName");
            //ObjFunction.FillList(lstStateName, "Select StateNo,StateName From MState where IsActive='true' and Stateno in (select StateNo From MCity Where IsActive='true') order by StateName");
            ObjFunction.FillList(lstCompType, "Select LedgerTypeNo,ShortName From MLedgerType where IsActive='true' order by LedgerTypeName");
           
        }

        private void FormatPicture()    // code by swati --- to create a listview run time
        {

            pnlPrinter.Top = txtPrinter.Bottom;
            pnlPrinter.Width = txtPrinter.Width;
            pnlPrinter.Height = 50;
            lstPrinter.Top = pnlPrinter.Top - 80;
            lstPrinter.Height = pnlPrinter.Height - 5;

            pnlStateName.Top = txtStateName.Bottom;
            pnlStateName.Width = txtStateName.Width;
            pnlStateName.Height = 100;
            lstStateName.Top = pnlStateName.Top - 165;
            lstStateName.Height = pnlStateName.Height - 5;

            pnlCityName.Top = txtCityName.Bottom;
            pnlCityName.Width = txtCityName.Width;
            pnlCityName.Height = 100;
            lstCityName.Top = pnlCityName.Top - 165;
            lstCityName.Height = pnlCityName.Height - 5;

            pnlCompType.Top = txtCompType.Bottom;
            pnlCompType.Width = txtCompType.Width;
            pnlCompType.Height = 100;
            lstCompType.Top = pnlCompType.Top - 298;
            lstCompType.Height = pnlCompType.Height - 5;

         
        }

        public void BindMBank()
        {
            while (dgBank.Rows.Count > 0)
                dgBank.Rows.RemoveAt(0);

            string sql = " SELECT (CASE WHEN (MFirmBank.IsActive = 0) THEN 'false' ELSE 'True' END) AS Chk, MLedger.LedgerName, MLedger.LedgerNo,ISNULL(MFirmBank.PkSrNo, 0) AS PkSrNo" +
                       " FROM MFirmBank RIGHT OUTER JOIN MLedger ON MFirmBank.BankNo = MLedger.LedgerNo AND MFirmBank.FirmNo = " + ID + " " +
                       " WHERE     (MLedger.GroupNo = " + GroupType.BankAccounts + ") AND (MLedger.IsActive = 'True') ";
            DataTable dt = ObjFunction.GetDataView(sql).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgBank.Rows.Add();
                for (int j = 0; j < dgBank.Columns.Count; j++)
                {
                    dgBank.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
        }
        private void FillFields()
        {
            try
            {
                EP.SetError(txtManufacturerCompanyName, "");
                mFirm = dbMFirm.ModifyMFirmByID(ID);
                txtManufacturerCompanyName.Text = mFirm.FirmName;
                txtShortName.Text = mFirm.ShortName;
                txtMfgCompAdd.Text = mFirm.Address;
                txtEmailID.Text = mFirm.EmailID;
                txtPhoneNo.Text = mFirm.PhoneNo1;
                txtPhoneNo2.Text = mFirm.PhoneNo2;
                txtMobileNo1.Text = mFirm.MobileNo1;
                txtMobileNo2.Text = mFirm.MobileNo2;
                txtPincode.Text = mFirm.Pincode;
                txtTermAndCondition.Text = mFirm.TermAndCondition;
                txtGSTNo.Text = mFirm.GSTNo;
                txtFSSAI.Text = mFirm.FSSAINo;
                txtPANNo.Text = mFirm.PANNo;
                txtAdharNo.Text = mFirm.AdharNo;
                txtAnyotherNo1.Text = mFirm.AnyotherNo1;
                txtAnyOtherNo2.Text = mFirm.AnyotherNo2;

                lstStateName.SelectedValue = mFirm.StateNo.ToString();
                txtStateName.Text = lstStateName.Text;

                lstCityName.SelectedValue = mFirm.CityNo.ToString();
                txtCityName.Text = lstCityName.Text;
                lstCompType.SelectedValue = mFirm.CompanyType;
                txtCompType.Text = lstCompType.Text;

                txtPrinter.Text = mFirm.PrinterName.ToString();



                txtGSTDate.MinDate = Convert.ToDateTime("01-01-1900");
                txtFSSAIDate.MinDate = Convert.ToDateTime("01-01-1900");
                txtGSTDate.Value = (mFirm.GSTDate);
                txtFSSAIDate.Value = (mFirm.FSSAIDate);

                chkActive.Checked = mFirm.IsActive;
               
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";
                chkIsAPMCBill.Checked = mFirm.IsType;
                if (chkIsAPMCBill.Checked == true)
                    chkIsAPMCBill.Text = "Yes";
                else
                    chkIsAPMCBill.Text = "No";
                //if (mFirm.IsType == true)
                //    rbPakka.Checked = true;
                //else
                //    rbKacches.Checked = true;


                lbladdcity.Enabled = false;
               FillPrinter(mFirm.PrinterName);
               BindMBank();
                dgBank.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void setDisable(bool flag)
        {
            btnSave.Visible = !flag;
            btnCancel.Visible = !flag;
            btnUpdate.Visible = flag;
            btnSearch.Visible = flag;
            btnExit.Visible = flag;
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtManufacturerCompanyName, "");

            try
            {
                if (txtManufacturerCompanyName.Text.Trim() == "")
                {
                    EP.SetError(txtManufacturerCompanyName, "Enter  Company Name");
                    EP.SetIconAlignment(txtManufacturerCompanyName, ErrorIconAlignment.MiddleRight);
                    txtManufacturerCompanyName.Focus();
                }
                else if (ManufacturerCompanyNm != txtManufacturerCompanyName.Text.Trim().ToUpper())
                {
                    if (ObjQry.ReturnInteger("Select Count(*) from MCompany where CompanyName = '" + txtManufacturerCompanyName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtManufacturerCompanyName, "Duplicate  Company  Name");
                        EP.SetIconAlignment(txtManufacturerCompanyName, ErrorIconAlignment.MiddleRight);
                        txtManufacturerCompanyName.Focus();
                    }
                    else
                        flag = true;
                }
                else
                    flag = true;
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return flag;
            }

        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                ManufacturerCompanyNm = "";
                lbladdcity.Enabled = true;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtManufacturerCompanyName.Focus();
                chkActive.Checked = true;
           
                FillPrinter("");
                BindMBank();
                dgBank.Enabled = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void SaveFields()
        {
            try
            {
                if (Validations() == true)
                {
                    dbMFirm = new DBMFirm();
                    mFirm = new MFirm();

                    mFirm.FirmNo = ID;
                    mFirm.FirmName = txtManufacturerCompanyName.Text.Trim().ToUpper();
                    mFirm.ShortName = txtShortName.Text.Trim().ToUpper();
                    mFirm.Address = txtMfgCompAdd.Text.Trim();
                    mFirm.EmailID = txtEmailID.Text;
                    mFirm.PrinterName = Convert.ToString(lstPrinter.Text);
                    mFirm.PhoneNo1 = txtPhoneNo.Text;
                    mFirm.PhoneNo2 = txtPhoneNo2.Text;
                    mFirm.MobileNo1 = txtMobileNo1.Text;
                    mFirm.MobileNo2 = txtMobileNo2.Text;
                    mFirm.StateNo = Convert.ToInt64(lstStateName.SelectedValue);
                    mFirm.StateCode = Convert.ToInt64(lstStateName.SelectedValue);
                    mFirm.CityNo = Convert.ToInt64(lstCityName.SelectedValue);
                    mFirm.Pincode = txtPincode.Text;
                    mFirm.TermAndCondition = txtTermAndCondition.Text;
                    mFirm.GSTNo = txtGSTNo.Text;
                    mFirm.FSSAINo = txtFSSAI.Text;
                    mFirm.PANNo = txtPANNo.Text;
                    mFirm.AdharNo = txtAdharNo.Text;
                    mFirm.AnyotherNo1 = txtAnyotherNo1.Text;
                    mFirm.AnyotherNo2 = txtAnyOtherNo2.Text;
                    mFirm.GSTDate = Convert.ToDateTime(txtGSTDate.Value);
                    mFirm.FSSAIDate = Convert.ToDateTime(txtFSSAIDate.Value);
                    mFirm.IsActive = chkActive.Checked;
                    mFirm.IsType = chkIsAPMCBill.Checked;
                    mFirm.CompanyType = Convert.ToInt64(lstCompType.SelectedValue);
                    mFirm.UserID = DBGetVal.UserID;
                    mFirm.UserDate = DBGetVal.ServerTime.Date;


                    dbMFirm.AddMFirm(mFirm);

                    for (int i = 0; i < dgBank.Rows.Count; i++)
                    {
                        //if (Convert.ToBoolean(dgBank.Rows[i].Cells[0].EditedFormattedValue) == true && Convert.ToInt64(dgBank.Rows[i].Cells[3].Value) == 0)
                        //{
                            mFirmBank = new MFirmBank();
                            mFirmBank.PkSrNo = Convert.ToInt64(dgBank.Rows[i].Cells[3].Value);
                            mFirmBank.BankNo = Convert.ToInt64(dgBank.Rows[i].Cells[2].Value);
                            mFirmBank.UserID = DBGetVal.UserID;
                            mFirmBank.UserDate = DBGetVal.ServerTime;
                            mFirmBank.IsActive = Convert.ToBoolean(dgBank.Rows[i].Cells[0].Value);
                            dbMFirm.AddMFirmBank(mFirmBank);
                       // }
                        //else if(Convert.ToBoolean(dgBank.Rows[i].Cells[0].EditedFormattedValue) == false && Convert.ToInt64(dgBank.Rows[i].Cells[3].Value) != 0)
                        //{
                        //    mFirmBank = new MFirmBank();
                        //    mFirmBank.PkSrNo = Convert.ToInt64(dgBank.Rows[i].Cells[3].Value);
                        //    dbMFirm.DeleteMFirmBank(mFirmBank);
                        //}
                    }
                    long TempId = dbMFirm.ExecuteNonQueryStatementsMFG();
                    if (TempId != 0)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Company Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select FirmNo From MFirm order by FirmNo").Table;
                            ID = TempId;
                            if (ShortID == 0)
                            {
                                // SetNavigation();
                                FillFields();
                            }
                            else
                            {
                                ShortID = ID;
                                this.Close();
                            }
                        }
                        else
                        {
                            OMMessageBox.Show("Company Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillFields();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("Company not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            dgBank.Enabled = true;
            lbladdcity.Enabled = true;
            txtManufacturerCompanyName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            pnlAdditional.Visible = false;
            pnlStateName.Visible = false;
            pnlCityName.Visible = false;
            pnlPrinter.Visible = false;
            pnlCompType.Visible = false;
            lbladdcity.Enabled = false;
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Firm();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbMFirm = new DBMFirm();
                mFirm = new MFirm();

                mFirm.FirmNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbMFirm.DeleteMFirm(mFirm) == true)
                    {
                        OMMessageBox.Show("Company Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillFields();
                    }
                    else
                    {
                        OMMessageBox.Show("Company not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }

                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Firm.RequestMFirmNo = 0;
            this.Close();
        }

        private void txtManufacturerCompanyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtManufacturerCompanyName.Text.Trim() == "")
                {

                    OMMessageBox.Show("Enter Company Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    EP.SetIconAlignment(txtManufacturerCompanyName, ErrorIconAlignment.MiddleRight);
                    txtManufacturerCompanyName.Focus();
                }
                else if (txtManufacturerCompanyName.Text.Trim() != "")
                {
                   
                        if (ObjQry.ReturnInteger("Select Count(*) from MCompany where CompanyName = '" + txtManufacturerCompanyName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            OMMessageBox.Show("Duplicate Company Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            EP.SetIconAlignment(txtManufacturerCompanyName, ErrorIconAlignment.MiddleRight);
                            txtManufacturerCompanyName.Focus();
                        }
                   
                    else
                    {
                        txtShortName.Focus();
                    }
                }


            }
        }

        private void txtShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtEmailID.Focus();

            }
        }

        private void txtEmailID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtPhoneNo.Focus();

            }
        }

        private void txtPhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtPrinter.Focus();

            }
        }

        private void txtPrinter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPrinter.Text == "")
                {
                    pnlPrinter.Visible = true;
                    lstPrinter.Focus();
                }
                else
                {
                    pnlPrinter.Visible = false;

                    txtMfgCompAdd.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtPrinter.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstPrinter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtPrinter.Text = lstPrinter.Text;

                    txtPrinter.Focus();
                    pnlPrinter.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtPrinter.Focus();
                pnlPrinter.Visible = false;

            }
        }

        private void txtMfgCompAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (enterflag == true)
                {
                    txtStateName.Focus();
                }
                else
                {
                    enterflag = true ;
                }
            }
            else
            {
                enterflag = false;
            }
        }

        private void txtStateName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtStateName.Text == "")
                {
                    pnlStateName.Visible = true;
                    lstStateName.Focus();
                }
                else
                {
                    pnlStateName.Visible = false;

                    txtCityName.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtStateName.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstStateName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtStateName.Text = lstStateName.Text;

                    txtStateName.Focus();
                    pnlStateName.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtStateName.Focus();
                pnlStateName.Visible = false;

            }
        }

        private void txtCityName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCityName.Text == "")
                {
                    pnlCityName.Visible = true;
                    lstCityName.Focus();
                }
                else
                {
                    pnlCityName.Visible = false;

                    txtPincode.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtCityName.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }

        }

        private void lstCityName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtCityName.Text = lstCityName.Text;

                    txtCityName.Focus();
                    pnlCityName.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtCityName.Focus();
                pnlCityName.Visible = false;

            }
        }

        private void txtPincode_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtTermAndCondition.Focus();

            }
        }
        private void txtTermAndCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (enterflag == true)
                {
                    txtGSTNo.Focus();
                }
                else
                {
                    enterflag = true;
                }
            }
            else
            {
                enterflag = false;
            }            
        }

        private void txtGSTNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtFSSAI.Focus();

            }
        }

        private void txtFSSAI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtCompType.Focus();

            }
        }

        private void txtCustomerType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCompType.Text == "")
                {
                    pnlCompType.Visible = true;
                    lstCompType.Focus();
                }
                else
                {
                    pnlCompType.Visible = false;

                    chkIsAPMCBill.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtCompType.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }

        }

        private void lstCustType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtCompType.Text = lstCompType.Text;

                    txtCompType.Focus();
                    pnlCompType.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtCompType.Focus();
                pnlCompType.Visible = false;

            }
        }

        private void btnAdditional_Click(object sender, EventArgs e)
        {
            pnlAdditional.Visible = !pnlAdditional.Visible;

            pnlAdditional.Location = new Point(30, 100);
            if (pnlAdditional.Visible == true)
                txtPhoneNo2.Focus();
        }

        private void txtPhoneNo2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtMobileNo1.Focus();

            }
        }

        private void txtMobileNo1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtMobileNo2.Focus();

            }
        }

        private void txtMobileNo2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtPANNo.Focus();

            }
        }

        private void txtPANNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtAdharNo.Focus();

            }
        }

        private void txtAdharNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtGSTDate.Focus();

            }
        }

        private void txtGSTDate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtFSSAIDate.Focus();

            }
        }

        private void txtFSSAIDate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtAnyotherNo1.Focus();

            }
        }

        private void txtAnyotherNo1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtAnyOtherNo2.Focus();

            }
        }

        private void txtAnyOtherNo2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                btnAddOk.Focus();

            }
        }
        private void btnAddOk_Click(object sender, EventArgs e)
        {
            pnlAdditional.Visible = false;
            btnSave.Focus();
        }

        private void btnAddCancel_Click(object sender, EventArgs e)
        {
            pnlAdditional.Visible = false;
            btnSave.Focus();
        }


        public void FillPrinter(string PrinterName)
        {
            try
            {
                bool flag = false;
                string strPrinters = null;
                int i = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID"); dt.Columns.Add("Desc");

                foreach (string strItem in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = (strPrinters + strItem);
                    dr[0] = i.ToString(); i++;
                    dt.Rows.Add(dr);
                    if (PrinterName != null)
                    {
                        if (PrinterName.ToString().ToUpper().Trim() == (strPrinters + strItem).ToString().ToUpper().Trim())
                            flag = true;
                    }
                }
                lstPrinter.DisplayMember = dt.Columns[1].ColumnName;
                lstPrinter.ValueMember = dt.Columns[0].ColumnName;
                lstPrinter.DataSource = dt;

                if (flag == true)
                {
                    lstPrinter.Text = PrinterName;
                }
                else
                {
                    System.Drawing.Printing.PrinterSettings objPrint = new System.Drawing.Printing.PrinterSettings();
                    lstPrinter.Text = objPrint.PrinterName;
                }
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

        private void chkActive_TextChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }
        private void txtValid_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
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
                if (btnSave.Visible) btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnExit_Click(sender, e);
            }
        }
        #endregion


        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(FirmNo),0)as FirmNo From MFirm").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["FirmNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(FirmNo),0)as FirmNo From MFirm").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["FirmNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(FirmNo),0)as FirmNo From MFirm where FirmNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["FirmNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(FirmNo),0)as FirmNo From MFirm where  FirmNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["FirmNo"].ToString());
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
            FillFields();
        }
        #endregion

        private void rbPakka_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                chkActive.Focus();
                // rbKacches.Focus();

            }
        }

        private void rbKacches_KeyDown(object sender, KeyEventArgs e)
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

                chkSelectAll.Focus();

            }
        }

        private void chkSelectAll_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void chkIsAPMCBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkActive.Focus();
            }
        }

        private void lbladdcity_Click(object sender, EventArgs e)
        {
            Master.CityAE NewF = new Yadi.Master.CityAE(-1);
            ObjFunction.OpenForm(NewF);
            FillListAll();
            txtCityName.Focus();
        }

    
      
    }
}

       