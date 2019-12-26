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
    public partial class TransporterAE : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        MLedgerDetails mLedgerDetails = new MLedgerDetails();
        string TransporterNm, mobNo;
        DataTable dtSearch = new DataTable();
        int cntRow;
        bool FlagBilingual;
        public long TransporterNo, ID = 0, LedgerUserNo = 0, No = 0;
        long TempStateNo = 0;

        public TransporterAE()
        {
            InitializeComponent();
        }

        private void TransporterAE_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                FlagBilingual = true;
                SetFlag(true);
            }
            else
            {
                FlagBilingual = false;
                SetFlag(false);
            }
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            formatpicture();
            TransporterNm = "";
            FillListAll();
            dtSearch = ObjFunction.GetDataView("Select LedgerNo,LedgerName From MLedger where GroupNo=" + GroupType.Transporter + " and IsActive='true' order by LedgerName").Table;
            // dtSearch = ObjFunction.GetDataView("Select TransporterNo,TransporterName From MTransporter where IsActive='true' order by TransporterName").Table;

            if (dtSearch.Rows.Count > 0)
            {
                if (Transporter.RequestTransporterNo != 0)
                {
                    ID = Transporter.RequestTransporterNo;
                    TransporterNm = "";
                }
                else
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString()); ;
                }
                FillControls();
                SetNavigation();
            }

            txtLanguage.Font = ObjFunction.GetLangFont();
            txtContactPersonLang.Font = ObjFunction.GetLangFont();
            txtAddressLang.Font = ObjFunction.GetLangFont();
            setDisplay(true);
            btnLangDesc.Enabled = false;
            btnNew.Focus();
            KeyDownFormat(this.Controls);
        }
        public void FillListAll()
        {
            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' order by CityName");

            ObjFunction.FillList(lstStateName, "Select StateNo,StateName From MState where IsActive='true'  order by StateName");


        }
        private void formatpicture()
        {
            pnlStateName.Top = txtStateName.Bottom;
            pnlStateName.Width = txtStateName.Width;
            pnlStateName.Height = 90;
            lstStateName.Top = pnlStateName.Top - 195;
            lstStateName.Height = pnlStateName.Height - 5;


            pnlCityName.Top = txtCityName.Bottom;
            pnlCityName.Width = txtCityName.Width;
            pnlCityName.Height = 90;
            lstCityName.Top = pnlCityName.Top - 195;
            lstCityName.Height = pnlStateName.Height - 5;



        }



        public void SetFlag(bool flag)
        {
            pnllang.Visible = flag;
            // txtLanguage.Visible = flag;
            //btnLangDesc.Visible = flag;
            // lblTransporterLN.Visible = flag;
        }
        private void FillControls()
        {
            EP.SetError(txtTransporterName, "");
            // EP.SetError(txtContactNo, "");
            EP.SetError(txtLanguage, "");
            txtLanguage.Font = ObjFunction.GetLangFont();
            txtContactPersonLang.Font = ObjFunction.GetLangFont();
            txtAddressLang.Font = ObjFunction.GetLangFont();
            mLedger = dbLedger.ModifyMLedgerByID(ID);
            mLedgerDetails = dbLedger.ModifyMLedgerDetailsByID(ID);
            TransporterNm = mLedger.LedgerName;
            txtTransporterName.Text = mLedger.LedgerName;
            txtContactPer.Text = mLedger.ContactPerson;
            mobNo = mLedger.ContactPerson;
            txtLanguage.Text = mLedger.LedgerLangName;
            LedgerUserNo = Convert.ToInt32(mLedger.LedgerUserNo);
            chkActive.Checked = mLedger.IsActive;
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";

            txtOAddress.Text = mLedgerDetails.Address;


            lstStateName.SelectedValue = mLedgerDetails.StateNo.ToString();
            txtStateName.Text = lstStateName.Text;



            lstCityName.SelectedValue = mLedgerDetails.CityNo.ToString();
            txtCityName.Text = lstCityName.Text;

            txtOPinCode.Text = mLedgerDetails.PinCode;
            txtOPhone1.Text = mLedgerDetails.PhNo1;
            // txtPhoneNo2.Text = mLedgerDetails.PhNo2;
            txtLanguage.Text = String.IsNullOrEmpty(mLedger.LedgerLangName) ? "" : mLedger.LedgerLangName.ToString();
            txtAddressLang.Text = String.IsNullOrEmpty(mLedgerDetails.AddressLang) ? "" : mLedgerDetails.AddressLang.ToString();
            txtContactPersonLang.Text = String.IsNullOrEmpty(mLedger.ContactPersonLang) ? "" : mLedger.ContactPersonLang.ToString();
            txtMobileNo1.Text = mLedgerDetails.MobileNo1;
            mobNo = mLedgerDetails.MobileNo1;
            // mobNo2 = mLedgerDetails.MobileNo2;
            // txtMobile2.Text = mLedgerDetails.MobileNo2;
            //txtEmailID.Text = mLedgerDetails.EmailID;
            //  txtFSSAINo.Text = (mLedgerDetails.FSSAI == null) ? "" : mLedgerDetails.FSSAI.ToString();
            // txtPANNo.Text = mLedgerDetails.PANNo;
            // txtAdharNo.Text = mLedgerDetails.AdharNo;
            // txtCreditLimit.Text = mLedgerDetails.CreditLimit.ToString("0.00");
            // txtDiscPer.Text = mLedgerDetails.DiscPer.ToString();
            // txtDiscRs.Text = mLedgerDetails.DiscRs.ToString();
            // lstTransporter.SelectedValue = mLedger.TransporterNo;

            txtGSTNo.Text = mLedgerDetails.GSTNo;
            txtAnyotherno1.Text = mLedgerDetails.AnyotherNo1;
            txtAnyotherno2.Text = mLedgerDetails.AnyotherNo2;

            txtGSTDate.MinDate = Convert.ToDateTime("01-01-1900");
            // tFSSAIDate.MinDate = Convert.ToDateTime("01-01-1900");
            txtGSTDate.Value = (mLedgerDetails.GSTDate);
            //tFSSAIDate.Value = (mLedgerDetails.FSSAIDate);


        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {
                    dbLedger = new DBMLedger();
                    mLedger = new MLedger();
                    bool flag = false;
                    long StateCode = ObjQry.ReturnLong("Select StateCode from MState where (stateno = '" + ObjFunction.GetListValue(lstStateName) + "')", CommonFunctions.ConStr);

                    mLedger.LedgerNo = ID;

                    mLedger.LedgerUserNo = LedgerUserNo.ToString();

                    mLedger.LedgerName = txtTransporterName.Text.Trim().ToUpper();
                    mLedger.LedgerLangName = txtLanguage.Text.Trim();
                    mLedger.GroupNo = GroupType.Transporter;
                    mLedger.InvFlag = false;
                    mLedger.MaintainBillByBill = false;
                    mLedger.ContactPerson = txtContactPer.Text.Trim();
                    mLedger.CompanyNo = DBGetVal.FirmNo;
                    mLedger.IsActive = chkActive.Checked;
                    mLedger.UserID = DBGetVal.UserID;
                    mLedger.UserDate = DBGetVal.ServerTime.Date;
                    mLedger.ContactPersonLang = txtContactPersonLang.Text.Trim().ToUpper();
                    flag = dbLedger.AddMLedger(mLedger);

                    if (flag == true)
                    {
                        mLedgerDetails = new MLedgerDetails();

                        mLedgerDetails.LedgerDetailsNo = ObjQry.ReturnLong("Select LedgerDetailsNo From MLedgerDetails Where LedgerNo=" + mLedger.LedgerNo + "", CommonFunctions.ConStr);
                        mLedgerDetails.LedgerNo = ID;
                        mLedgerDetails.Address = txtOAddress.Text.Trim();
                        if (txtStateName.Text != "")
                        {
                            mLedgerDetails.StateNo = Convert.ToInt32(lstStateName.SelectedValue);
                        }
                        else
                        { mLedgerDetails.StateNo = 0; }
                        if (txtCityName.Text != "")
                        {
                            mLedgerDetails.CityNo = Convert.ToInt32(lstCityName.SelectedValue);
                        }


                        mLedgerDetails.AreaNo = 0;

                        mLedgerDetails.PinCode = txtOPinCode.Text.Trim();
                        mLedgerDetails.PhNo1 = txtOPhone1.Text.Trim();
                        mLedgerDetails.PhNo2 = "";
                        mLedgerDetails.MobileNo1 = txtMobileNo1.Text.Trim();
                        mLedgerDetails.MobileNo2 = "";
                        mLedgerDetails.EmailID = "";
                        mLedgerDetails.CustomerType = 0;//Convert.ToInt32(lstCustType.SelectedValue);
                        mLedgerDetails.CreditLimit = 0.00;//(txtCreditLimit.Text.Trim() == "") ? 0 : Convert.ToDouble(txtCreditLimit.Text.Trim());
                        mLedgerDetails.PANNo = "";//Convert.ToString(txtPANNo.Text);
                        mLedgerDetails.FSSAI = "";// Convert.ToString(txtFSSAINo.Text);
                        mLedgerDetails.AdharNo = "";//Convert.ToString(txtAdharNo.Text);
                        mLedgerDetails.UserID = DBGetVal.UserID;
                        mLedgerDetails.UserDate = DBGetVal.ServerTime.Date;
                        mLedgerDetails.AddressLang = txtAddressLang.Text.Trim();
                        mLedgerDetails.RateTypeNo = 0; //Convert.ToInt32(lstRateType.SelectedValue);
                        mLedgerDetails.DiscPer = 0.00;//Convert.ToDouble(txtDiscPer.Text);
                                                      //  mLedgerDetails.DiscRs = Convert.ToDouble(txtDiscRs.Text);
                        mLedgerDetails.AnyotherNo1 = Convert.ToString(txtAnyotherno1.Text);
                        mLedgerDetails.AnyotherNo2 = Convert.ToString(txtAnyotherno2.Text);
                        mLedgerDetails.GSTNo = Convert.ToString(txtGSTNo.Text);
                        mLedgerDetails.GSTDate = Convert.ToDateTime(txtGSTDate.Text);
                        mLedgerDetails.FSSAIDate = Convert.ToDateTime(txtGSTDate.Value);
                        mLedgerDetails.AccountNo = "0";
                        mLedgerDetails.ReportName = ".";
                        mLedgerDetails.Distance = 0;
                        dbLedger.AddMLedgerDetails(mLedgerDetails);
                    }


                    if (dbLedger.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Transporter Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select LedgerNo,LedgerName From MLedger where GroupNo=" + GroupType.Transporter + " order by LedgerName").Table;
                            ID = ObjQry.ReturnLong("Select Max(LedgerNo) From MLedger", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Transporter Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Transporter not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Transporter.RequestTransporterNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
            // BtnExit.Focus();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            dbLedger = new DBMLedger();
            mLedger = new MLedger();

            mLedger.LedgerNo = ID;
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dbLedger.DeleteMLedger(mLedger) == true)
                {
                    OMMessageBox.Show("Transporter Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //Form NewF = new Transporter();
                    //this.Close();
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    FillControls();
                }
                else
                {
                    OMMessageBox.Show("Transporter not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }

            }
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Transporter();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnLangDesc.Enabled = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtTransporterName.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //  nw = 0;
            ID = 0;
            TransporterNm = "";
            mobNo = "";
            ObjFunction.InitialiseControl(this.Controls);
            btnLangDesc.Enabled = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtTransporterName.Focus();
            chkActive.Checked = true;
            // txtContactNo.Text = "0";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dtSearch.Rows.Count > 0)
                NavigationDisplay(2);
            btnLangDesc.Enabled = false;
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " and  LedgerNo >" + ID).Table;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " and LedgerNo <" + ID).Table;
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
            btndelete.Visible = flag;
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


        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void txtStateName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtStateName.Text == "")
                {
                    if (lstStateName.Items.Count > 0)
                    {
                        pnlStateName.Visible = true;
                        lstStateName.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("State is not Available ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        pnlStateName.Visible = false;
                        txtCityName.Focus();
                    }

                }
                else
                {
                    //txtCityName.Text = "";
                    pnlStateName.Visible = false;
                    ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' and stateno=" + lstStateName.SelectedValue + " order by CityName");
                    txtCityName.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtStateName.Text = "";
            }
            else
            {
                TempStateNo = Convert.ToInt32(lstStateName.SelectedValue);
                pnlStateName.Visible = true;
                lstStateName.Focus();
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
                    txtCityName.Text = "";
                    ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' and stateno=" + lstStateName.SelectedValue + " order by CityName");
                    txtCityName.Focus();
                    pnlStateName.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if ((e.KeyCode == Keys.Escape) || (e.KeyCode == Keys.Space))
            {
                e.SuppressKeyPress = true;
                txtStateName.Focus();
                pnlStateName.Visible = false;

                lstStateName.SelectedValue = TempStateNo;
                txtStateName.Text = lstStateName.Text;
                TempStateNo = 0;

            }
        }

        private void txtCityName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                if (txtCityName.Text == "")
                {

                    if (lstCityName.Items.Count > 0)
                    {
                        pnlCityName.Visible = true;
                        lstCityName.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("City is not Available in Selected state", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        pnlCityName.Visible = false;
                        txtGSTNo.Focus();
                    }

                }
                else
                {
                    pnlCityName.Visible = false;

                    txtGSTNo.Focus();

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

                    txtGSTNo.Focus();
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
        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtTransporterName, "");
            // EP.SetError(txtContactNo, "");

            if (txtTransporterName.Text.Trim() == "")
            {

                EP.SetError(txtTransporterName, "Enter Ledger Name");
                EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                txtTransporterName.Focus();
                flag = false;
            }

            else if (TransporterNm != txtTransporterName.Text)
            {
                // if (ObjQry.ReturnInteger("Select Count(*) from MLedger where Ledgerno not in (" + ID + ") LedgerName = '" + txtTransporterName.Text + "' AND GroupNo =" + GroupType.Transporter + " , CommonFunctions.ConStr) != 0)
                if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtTransporterName.Text.Trim().Replace("'", "''") + "' and GroupNo = " + GroupType.Transporter + " AND Ledgerno NOT IN(" + ID + ") ", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtTransporterName, "Duplicate Ledger Name");
                    EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                    txtTransporterName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }


        private void TransporterAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Transporter.RequestTransporterNo = 0;
            TransporterNm = "";
        }

        private void txtTransporterName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtTransporterName, "");
                if (txtTransporterName.Text.Trim() != "")
                {
                    //txtLangFullDesc.Text = "";
                    if (TransporterNm != txtTransporterName.Text.Trim())
                    {
                        //  if (ObjQry.ReturnInteger("Select Count(*) from MLedger where Ledgerno not in (" + ID + ") LedgerName = '" + txtTransporterName.Text + "' AND GroupNo=" + GroupType.Transporter + " ", CommonFunctions.ConStr) != 0)
                        if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtTransporterName.Text.Trim().Replace("'", "''") + "' and GroupNo = " + GroupType.Transporter + " AND Ledgerno NOT IN(" + ID + ") ", CommonFunctions.ConStr) != 0)


                        {
                            EP.SetError(txtTransporterName, "Duplicate transport Name");
                            EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                            txtTransporterName.Focus();
                        }
                        else if (FlagBilingual == true)
                        {

                            if (txtLanguage.Text.Trim().Length == 0)
                            {
                                btnLangDesc_Click(btnLangDesc, null);
                            }
                        }

                        else
                            txtContactPer.Focus();


                    }
                    else
                    {
                        if (FlagBilingual == true)
                        {
                            //txtLangFullDesc.Focus();
                            if (txtTransporterName.Text.Trim().Length == 0)
                            {
                                btnLangDesc_Click(btnLangDesc, null);
                            }
                        }
                        else
                            txtContactPer.Focus();
                    }
                }
                else
                {
                    txtTransporterName.Focus();
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

        private void txtTransporterName_KeyDown(object sender, KeyEventArgs e)
        {
            //  EP.SetError(txtTransporterName, "");
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtTransporterName.Text == "")
                {
                    //EP.SetError(txtTransporterName, "Enter the contact name");
                    //EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                    txtTransporterName.Focus();
                }
                else
                {
                    //txtTransporterLN.Focus();
                    txtContactPer.Focus();
                }
            }
        }

        //private void txtContactNo_Leave(object sender, EventArgs e)
        //{
        //    EP.SetError(txtContactNo, "");
        //    if (txtContactNo.Text != "")
        //    {
        //        if (txtContactNo.Text.Trim().Length < 10 && txtContactNo.Text.Trim() != "0")
        //        {
        //            EP.SetError(txtContactNo, "Enter Valid Mobile No");
        //            EP.SetIconAlignment(txtContactNo, ErrorIconAlignment.MiddleRight);
        //            txtContactNo.Focus();
        //            flag = false;
        //        }
        //        else if (mobNo != txtContactNo.Text.Trim())
        //        {
        //            if (txtContactNo.Text.Trim() != "1111111111" && txtContactNo.Text.Trim() != "0")
        //            {
        //                if (ObjQry.ReturnInteger("Select Count(*) from MLedger where ContactPerson='" + txtContactNo.Text.Trim() + "' And LedgerNo!=" + ID + "", CommonFunctions.ConStr) != 0)
        //                {
        //                    EP.SetError(txtContactNo, "Duplicate contact No");
        //                    EP.SetIconAlignment(txtContactNo, ErrorIconAlignment.MiddleRight);
        //                    txtContactNo.Focus();
        //                    flag = false;
        //                }
        //                else
        //                    EP.SetError(txtContactNo, "");
        //            }
        //        }
        //    }

        //    else
        //        EP.SetError(txtContactNo, "");
        //}

        private void txtTransporterName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //txtItemLang.Text = ObjFunction.ChecklLangVal(txtItemName.Text);
                txtTransporterName_Leave(sender, new EventArgs());
                txtLanguage.Focus();
            }
        }


        //private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        chkActive.Focus();

        //    }
        //}

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void btnLangDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLanguage.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtTransporterName.Text.Trim(), txtLanguage.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLanguage.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtTransporterName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtTransporterName.Text.Trim(), txtLanguage.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLanguage.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLanguage.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtContactPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //txtItemLang.Text = ObjFunction.ChecklLangVal(txtItemName.Text);
                txtContactPer_Leave(sender, new EventArgs());
                txtContactPersonLang.Focus();
            }
        }

        private void txtContactPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtContactPer.Text == "")
                {

                    txtContactPer.Focus();
                }
                else
                {

                    txtMobileNo1.Focus();
                }
            }
        }

        private void txtContactPer_Leave(object sender, EventArgs e)
        {
            try
            {

                if (txtContactPer.Text.Trim() != "")
                {
                    //txtLangFullDesc.Text = "";


                    if (FlagBilingual == true)
                    {

                        if (txtContactPersonLang.Text.Trim().Length == 0)
                        {
                            btnContactPersonLang_Click(btnContactPersonLang, null);
                        }
                        else if (txtContactPer.Text.Trim().Length == 0)
                        {
                            btnContactPersonLang_Click(btnContactPersonLang, null);
                        }
                        else

                            txtMobileNo1.Focus();
                    }

                }
                else
                {
                    txtContactPer.Focus();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtGSTNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {

                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtGSTNo.Text.Trim() != "")
                    {
                        string statecode = ObjQry.ReturnString("Select Statecode from Mstate where stateno=" + lstStateName.SelectedValue, CommonFunctions.ConStr);
                        if ((statecode != (txtGSTNo.Text.Substring(0, 2))) || (txtGSTNo.Text.Length != 15))
                        {

                            statecode = (txtGSTNo.Text.Substring(0, 2)); //Convert.ToInt32(txtGSTNo.Text.Substring(0, 2));
                            OMMessageBox.Show("Enter GST No", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            EP.SetIconAlignment(txtGSTNo, ErrorIconAlignment.MiddleRight);
                            txtGSTNo.Focus();

                        }

                        else
                        {
                            txtGSTNo.Text = txtGSTNo.Text.ToUpper();
                            txtGSTDate.Focus();
                        }
                    }
                    else
                    {

                        txtAnyotherno1.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnContactPersonLang_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtContactPersonLang.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtContactPer.Text.Trim(), txtContactPersonLang.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtContactPersonLang.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtContactPer.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtContactPer.Text.Trim(), txtContactPersonLang.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtContactPersonLang.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtContactPersonLang.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtMobileNo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOPhone1.Focus();

            }

        }

        private void txtOPhone1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


                txtOAddress.Focus();

            }
        }

        private void txtOAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtOAddress.Text == "")
                {
                    //EP.SetError(txtTransporterName, "Enter the contact name");
                    //EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                    txtOAddress.Focus();
                }
                else
                {
                    //txtTransporterLN.Focus();
                    txtOPinCode.Focus();
                }
            }
        }

        private void txtOAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                txtOAddress_Leave(sender, new EventArgs());
                //txtAddressLang.Focus();
            }
        }

        private void txtOAddress_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtTransporterName, "");
                if (txtOAddress.Text.Trim() != "")
                {

                    if (FlagBilingual == true)
                    {

                        if (txtAddressLang.Text.Trim().Length == 0)
                        {
                            btnAddressLang_Click(btnAddressLang, null);
                        }
                        else if (txtOAddress.Text.Trim().Length == 0)
                        {
                            btnAddressLang_Click(btnAddressLang, null);
                        }
                        else

                            txtOPinCode.Focus();
                    }



                }
                else
                {
                    txtOPinCode.Focus();
                }


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnAddressLang_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtAddressLang.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtOAddress.Text.Trim(), txtAddressLang.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtAddressLang.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtOAddress.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtOAddress.Text.Trim(), txtAddressLang.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtAddressLang.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtAddressLang.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtOPinCode_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                txtStateName.Focus();

            }
        }

        private void txtGSTDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtAnyotherno1.Focus();

            }
        }

        private void txtAnyotherno1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtAnyotherno2.Focus();

            }
        }

        private void txtAnyotherno2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkActive.Focus();

            }
        }

        private void txtMobileNo1_TextChanged(object sender, EventArgs e)
        {

            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtOPhone1_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }






    }
}

