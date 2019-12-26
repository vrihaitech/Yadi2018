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
    public partial class SupplierAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        MLedgerDetails mLedgerDetails = new MLedgerDetails();
        string LedgerNm, mobNo, mobNo2;
        DataTable dtSearch = new DataTable();
        DataTable dtLedger = new DataTable();
        long LedgerUserNo, ID;
        bool FlagBilingual;
        public long ShortID = 0;
        long No = 0;

        public SupplierAE()
        {
            InitializeComponent();
        }

        public SupplierAE(long shortid)
        {
            InitializeComponent();
            ShortID = shortid;
        }

        private void SupplierAE_Load(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    FlagBilingual = true;
                    SetLangFlag(true);
                }
                else
                {
                    FlagBilingual = false;
                    SetLangFlag(false);
                }

                txtLangName.Font = ObjFunction.GetLangFont();
                txtAddressLang.Font = ObjFunction.GetLangFont();
                txtContactPersonLang.Font = ObjFunction.GetLangFont();
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                lbladdarea.Enabled = false;
                lbladdcity.Enabled = false;
                formatpicture();
                FillListAll();

                DisplayCustomerCount();
                txtGSTDate.CustomFormat = "dd-MMM-yy";
                tFSSAIDate.CustomFormat = "dd-MMM-yy";

                if (ShortID == 0)
                {
                    dtSearch = dtSearch = ObjFunction.GetDataView(" Select max(isnull(LedgerNo,0)) as LedgerNo ,ledgername FROM MLedger Where GroupNo=" + GroupType.SundryCreditors + " and ledgerno not in (25) group by LedgerNo,ledgername order by ledgername").Table;

                    KeyDownFormat(this.Controls);
                    if (dtSearch.Rows.Count > 0)
                        if (dtSearch.Rows.Count > 0)
                        {
                            if (Supplier.RequestSupplierNo == 0)
                                // ID = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                                ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                            else
                                ID = Supplier.RequestSupplierNo;
                            FillFields();

                        }
                    setDisplay(true);

                    btnNew.Focus();
                    KeyDownFormat(this.Controls);
                    btnContactPersonLang.Enabled = false;
                    btnAddressLang.Enabled = false;
                    btnLangLongDesc.Enabled = false;
                }
                else
                {
                    btnNew_Click(sender, new EventArgs());
                    setDisable(false);
                    //BtnExit.Visible = true;
                    //txtCityName.Focus();
                    BtnExit.Visible = true;
                    chkActive.Checked = true;
                }
                lblCustCode.Font = new Font("Times Of Roman", 15, FontStyle.Bold);
                lblCustCode.ForeColor = Color.White;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void formatpicture()
        {
            pnlStateName.Top = txtStateName.Bottom;
            pnlStateName.Width = txtStateName.Width;
            pnlStateName.Height = 90;
            lstStateName.Top = pnlStateName.Top - 190;
            lstStateName.Height = pnlStateName.Height - 5;


            pnlCityName.Top = txtCityName.Bottom;
            pnlCityName.Width = txtCityName.Width;
            pnlCityName.Height = 90;
            lstCityName.Top = pnlCityName.Top - 190;
            lstCityName.Height = pnlStateName.Height - 5;

            pnlAreaName.Top = txtAreaName.Bottom;
            pnlAreaName.Width = txtAreaName.Width;
            pnlAreaName.Height = 90;
            lstAreaName.Top = pnlAreaName.Top - 190;
            lstAreaName.Height = pnlAreaName.Height - 5;

            pnlCustType.Top = txtCustType.Bottom;
            pnlCustType.Width = txtCustType.Width;
            pnlCustType.Height = 90;
            lstCustType.Top = pnlCustType.Top - 240;
            lstCustType.Height = pnlCustType.Height - 5;

        }

        public void FillListAll()
        {
            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' order by CityName");
            ObjFunction.FillList(lstAreaName, "Select AreaNo,AreaName From MArea where IsActive='true' order by AreaName");
            ObjFunction.FillList(lstStateName, "Select StateNo,StateName From MState where IsActive='true' order by StateName");
            ObjFunction.FillList(lstCustType, "Select LedgerTypeNo,ShortName From MLedgerType where IsActive='true' and LedgerTypeNo not in (4) order by LedgerTypeName");
            ObjFunction.FillList(lstTransporter, "Select TransporterNo,TransporterName From MTransporter where IsActive='true' order by TransporterName");
            ObjFunction.FillList(lstRateType, "select RateTypeNo,ActualName from MRateType where IsActive='true' order by ActualName");

        }

        public void SetLangFlag(bool flag)
        {
            txtLangName.Visible = flag;
            btnLangLongDesc.Visible = flag;
            txtAddressLang.Visible = flag;
            btnAddressLang.Visible = flag;
            btnContactPersonLang.Visible = flag;
            txtContactPersonLang.Visible = flag;


        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
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

        private void DisplayCustomerCount()
        {
            lblTotalCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  MLedger WHERE  (GroupNo = " + GroupType.SundryCreditors + ")", CommonFunctions.ConStr).ToString();
            lblActiveCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  MLedger WHERE  (GroupNo = " + GroupType.SundryCreditors + ") AND (IsActive = 'true')", CommonFunctions.ConStr).ToString();
            lblDeActiv.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  MLedger WHERE  (GroupNo = " + GroupType.SundryCreditors + ") AND (IsActive = 'false')", CommonFunctions.ConStr).ToString();
            label9.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblTotalCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            label10.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblActiveCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            label11.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblDeActiv.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtLedgerName, "");
            EP.SetError(txtMobileNo1, "");
            EP.SetError(txtMobile2, "");
            EP.SetError(txtContactPer, "");
            EP.SetError(txtCreditLimit, "");
            //EP.SetError(txtEmailID, "");
            EP.SetError(txtStateName, "");
            if (txtMobileNo1.Text.Trim() == "")
            {
                txtMobileNo1.Text = "1111111111";
                //EP.SetError(txtMobileNo1, "Enter Mobile Number");
                //EP.SetIconAlignment(txtMobileNo1, ErrorIconAlignment.MiddleRight);
                //txtMobileNo1.Focus();

            }
            if (txtMobile2.Visible == true)
            {
                if (txtMobile2.Text.Trim() == "")
                {
                    txtMobile2.Text = "1111111111";
                    //EP.SetError(txtMobileNo1, "Enter Mobile Number");
                    //EP.SetIconAlignment(txtMobileNo1, ErrorIconAlignment.MiddleRight);
                    //txtMobileNo2.Focus();
                }
            }
            if (txtLedgerName.Text.Trim() == "")
            {
                EP.SetError(txtLedgerName, "Enter Customer Name");
                EP.SetIconAlignment(txtLedgerName, ErrorIconAlignment.MiddleRight);
                txtLedgerName.Focus();
            }
            else if (txtStateName.Text.Trim() == "")
            {
                EP.SetError(txtStateName, "Please Select State Name");
                EP.SetIconAlignment(txtStateName, ErrorIconAlignment.MiddleRight);
                txtStateName.Focus();
            }
            else if (txtContactPer.Text.Trim() == "")
            {
                EP.SetError(txtContactPer, "Enter Name");
                EP.SetIconAlignment(txtContactPer, ErrorIconAlignment.MiddleRight);
                txtContactPer.Focus();
            }
            else if (ObjFunction.CheckValidAmount(txtCreditLimit.Text.Trim()) == false)
            {
                EP.SetError(txtCreditLimit, "Enter Valid Credit Amount");
                EP.SetIconAlignment(txtCreditLimit, ErrorIconAlignment.MiddleRight);
                txtCreditLimit.Focus();
            }

            else
                flag = true;

            if (flag == true)
            {
                if (LedgerNm.ToUpper() != txtLedgerName.Text.Trim().ToUpper())
                {
                    if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtLedgerName.Text.Trim().ToUpper().Replace("'", "''") + "' And GroupNo=" + GroupType.SundryCreditors + "", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtLedgerName, "Duplicate Ledger Name");
                        EP.SetIconAlignment(txtLedgerName, ErrorIconAlignment.MiddleRight);
                        txtLedgerName.Focus();
                        flag = false;
                    }
                    else
                        flag = true;
                }
            }


            if (flag == true)
            {
                if (txtMobileNo1.Text != "")
                {
                    if (txtMobileNo1.Text.Trim().Length < 10)
                    {
                        EP.SetError(txtMobileNo1, "Enter Valid Mobile No");
                        EP.SetIconAlignment(txtMobileNo1, ErrorIconAlignment.MiddleRight);
                        txtMobileNo1.Focus();
                        flag = false;
                    }
                    else if (mobNo != txtMobileNo1.Text.Trim())
                    {
                        if (txtMobileNo1.Text.Trim() != "1111111111")
                        {
                            if (ObjQry.ReturnInteger("Select Count(*) from MLedgerDetails where MobileNo1='" + txtMobileNo1.Text.Trim() + "'", CommonFunctions.ConStr) != 0)
                            {
                                EP.SetError(txtMobileNo1, "Duplicate Mobile No");
                                EP.SetIconAlignment(txtMobileNo1, ErrorIconAlignment.MiddleRight);
                                txtMobileNo1.Focus();
                                flag = false;
                            }
                            else
                                flag = true;
                        }
                    }
                }
            }
            if (flag == true)
            {
                if (txtMobile2.Visible == true)
                {
                    if (mobNo2 != txtMobile2.Text.Trim())
                    {
                        if (txtMobile2.Text.Trim() != "1111111111")
                        {
                            if (ObjQry.ReturnInteger("Select Count(*) from MLedgerDetails where MobileNo2='" + txtMobile2.Text.Trim() + "'", CommonFunctions.ConStr) != 0)
                            {
                                EP.SetError(txtMobile2, "Duplicate Mobile No");
                                EP.SetIconAlignment(txtMobile2, ErrorIconAlignment.MiddleRight);
                                txtMobile2.Focus();
                                flag = false;
                            }
                            else
                                flag = true;
                        }
                    }
                    if (flag == true)
                    {
                        if (txtMobile2.Text.Trim().Length < 10)
                        {
                            EP.SetError(txtMobile2, "Enter Valid Mobile No");
                            EP.SetIconAlignment(txtMobile2, ErrorIconAlignment.MiddleRight);
                            txtMobileNo1.Focus();
                            flag = false;
                        }
                        else
                            flag = true;
                    }
                }
                else
                    flag = true;
            }
            //if (flag == true)
            //{
            //    if (txtEmailID.Text.Trim() != "")
            //    {
            //        if (ObjFunction.CheckValidEmailID(txtEmailID.Text.Trim()) == false)
            //        {
            //            EP.SetError(txtEmailID, "Enter Valid Email ID");
            //            EP.SetIconAlignment(txtEmailID, ErrorIconAlignment.MiddleRight);
            //            txtEmailID.Focus();
            //            flag = false;
            //        }
            //        else
            //        {
            //            flag = true;
            //        }
            //    }
            //}


            return flag;
        }

        private void FillFields()
        {
            try
            {
                EP.SetError(txtLedgerName, "");
                EP.SetError(txtMobileNo1, "");
                EP.SetError(txtMobile2, "");
                EP.SetError(txtContactPer, "");
                EP.SetError(txtCreditLimit, "");
                // EP.SetError(txtEmailID, "");
                EP.SetError(txtStateName, "");

                mLedger = dbLedger.ModifyMLedgerByID(ID);
                mLedgerDetails = dbLedger.ModifyMLedgerDetailsByID(ID);
                LedgerNm = mLedger.LedgerName;
                txtLedgerName.Text = mLedger.LedgerName;
                LedgerUserNo = 0;
                txtContactPer.Text = mLedger.ContactPerson;
                lstTransporter.SelectedValue = mLedger.TransporterNo.ToString();
                txtTransporter.Text = lstTransporter.Text;
                lblCustCode.Text = "CustCode : " + mLedger.LedgerNo;
                chkActive.Checked = mLedger.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";

                //Ledger Details
                txtOAddress.Text = mLedgerDetails.Address;


                lstStateName.SelectedValue = mLedgerDetails.StateNo.ToString();
                txtStateName.Text = lstStateName.Text;



                lstCityName.SelectedValue = mLedgerDetails.CityNo.ToString();
                txtCityName.Text = lstCityName.Text;


                lstAreaName.SelectedValue = mLedgerDetails.AreaNo.ToString();
                txtAreaName.Text = lstAreaName.Text;

                lstRateType.SelectedValue = mLedgerDetails.RateTypeNo.ToString();
                txtRateType.Text = lstRateType.Text;

                lstCustType.SelectedValue = mLedgerDetails.CustomerType.ToString();
                txtCustType.Text = lstCustType.Text;


                txtOPinCode.Text = mLedgerDetails.PinCode;
                txtOPhone1.Text = mLedgerDetails.PhNo1;
                txtPhoneNo2.Text = mLedgerDetails.PhNo2;
                txtLangName.Text = String.IsNullOrEmpty(mLedger.LedgerLangName) ? "" : mLedger.LedgerLangName.ToString();
                txtAddressLang.Text = String.IsNullOrEmpty(mLedgerDetails.AddressLang) ? "" : mLedgerDetails.AddressLang.ToString();
                txtContactPersonLang.Text = String.IsNullOrEmpty(mLedger.ContactPersonLang) ? "" : mLedger.ContactPersonLang.ToString();
                txtMobileNo1.Text = mLedgerDetails.MobileNo1;
                mobNo = mLedgerDetails.MobileNo1;
                mobNo2 = mLedgerDetails.MobileNo2;
                txtMobile2.Text = mLedgerDetails.MobileNo2;
                txtEmailID.Text = mLedgerDetails.EmailID;
                txtFSSAINo.Text = (mLedgerDetails.FSSAI == null) ? "" : mLedgerDetails.FSSAI.ToString();
                txtPANNo.Text = mLedgerDetails.PANNo;
                txtAdharNo.Text = mLedgerDetails.AdharNo;
                txtCreditLimit.Text = mLedgerDetails.CreditLimit.ToString("0.00");
                txtCreditDays.Text = mLedgerDetails.CreditDays.ToString();
                txtDiscPer.Text = mLedgerDetails.DiscPer.ToString();
                txtDiscRs.Text = mLedgerDetails.DiscRs.ToString();
                lstTransporter.SelectedValue = mLedger.TransporterNo;

                txtGSTNo.Text = mLedgerDetails.GSTNo;
                txtAnyotherno1.Text = mLedgerDetails.AnyotherNo1;
                txtAnyotherno2.Text = mLedgerDetails.AnyotherNo2;

                txtGSTDate.MinDate = Convert.ToDateTime("01-01-1900");
                tFSSAIDate.MinDate = Convert.ToDateTime("01-01-1900");
                txtGSTDate.Value = (mLedgerDetails.GSTDate);
                tFSSAIDate.Value = (mLedgerDetails.FSSAIDate);

                chkPartyWiseRate.Checked = mLedger.IsPartyWiseRate;
                if (chkPartyWiseRate.Checked == true)
                    chkPartyWiseRate.Text = "Yes";
                else
                    chkPartyWiseRate.Text = "No";

                chkQuotationRate.Checked = mLedger.QuotationRate;
                if (chkQuotationRate.Checked == true)
                    chkQuotationRate.Text = "Yes";
                else
                    chkQuotationRate.Text = "No";

                lbladdarea.Enabled = false;
                lbladdcity.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                LedgerNm = "";
                mobNo = "";
                ID = 0;
                //lblCrDr.Text = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                lbladdarea.Enabled = true;
                lbladdcity.Enabled = true;

                FillListAll();
                chkActive.Checked = true;
                txtLedgerName.Focus();
                pnlAdditional1.Visible = false;
                pnlAdditional2.Visible = false;
                txtCreditLimit.Text = "0.00";
                txtCreditDays.Text = "0";
                txtDiscRs.Text = "0.00";
                txtDiscPer.Text = "0.00";
                btnContactPersonLang.Enabled = true;
                btnAddressLang.Enabled = true;
                btnLangLongDesc.Enabled = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            pnlAdditional1.Visible = false;
            pnlAdditional2.Visible = false;

            SaveFields();
        }

        public void SaveFields()
        {
            try
            {
                if (Validations() == true)
                {

                    //---GST---//
                    //string StateCode = ObjQry.ReturnString("SELECT  StateCode FROM  Mstate  WHERE  (stateno = '" + ObjFunction.GetComboValue(cmbOState) + "')", CommonFunctions.ConStr).ToString();
                    bool flag = false;
                    long StateCode = ObjQry.ReturnLong("Select StateCode from MState where (stateno = '" + ObjFunction.GetListValue(lstStateName) + "')", CommonFunctions.ConStr);
                    dbLedger = new DBMLedger();
                    mLedger = new MLedger();
                    mLedger.LedgerNo = ID;
                    mLedger.LedgerUserNo = LedgerUserNo.ToString();
                    mLedger.LedgerName = txtLedgerName.Text.Trim().ToUpper();
                    mLedger.LedgerLangName = txtLangName.Text.Trim();
                    mLedger.ContactPersonLang = txtContactPersonLang.Text.Trim();
                    mLedger.ContactPerson = txtContactPer.Text.Trim();
                    mLedger.TransporterNo = Convert.ToInt32(lstTransporter.SelectedValue);
                    mLedger.StateCode = StateCode;
                    mLedger.IsActive = chkActive.Checked;
                    mLedger.IsPartyWiseRate = chkPartyWiseRate.Checked;
                    mLedger.QuotationRate = chkQuotationRate.Checked;
                    mLedger.GroupNo = GroupType.SundryCreditors;
                    mLedger.CompanyNo = DBGetVal.FirmNo;
                    mLedger.UserID = DBGetVal.UserID;
                    mLedger.UserDate = DBGetVal.ServerTime.Date;
                    mLedger.LedgerStatus = (ID == 0) ? 1 : 2;
                    mLedger.IsEnroll = false;
                    mLedger.IsSendSMS = false;
                    mLedger.MaintainBillByBill = false;
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
                        else { mLedgerDetails.CityNo = 0; }
                        if (txtAreaName.Text != "")
                        {
                            mLedgerDetails.AreaNo = Convert.ToInt32(lstAreaName.SelectedValue);
                        }
                        else
                        {
                            mLedgerDetails.AreaNo = 0;
                        }
                        mLedgerDetails.PinCode = txtOPinCode.Text.Trim();
                        mLedgerDetails.PhNo1 = txtOPhone1.Text.Trim();
                        mLedgerDetails.PhNo2 = txtPhoneNo2.Text.Trim();
                        mLedgerDetails.MobileNo1 = txtMobileNo1.Text.Trim();
                        mLedgerDetails.MobileNo2 = txtMobile2.Text.Trim();
                        mLedgerDetails.EmailID = txtEmailID.Text.Trim();
                        mLedgerDetails.CustomerType = Convert.ToInt32(lstCustType.SelectedValue);
                        mLedgerDetails.CreditLimit = (txtCreditLimit.Text.Trim() == "") ? 0 : Convert.ToDouble(txtCreditLimit.Text.Trim());
                        mLedgerDetails.CreditDays = (txtCreditDays.Text.Trim() == "") ? 0 : Convert.ToInt64(txtCreditDays.Text.Trim());
                        mLedgerDetails.PANNo = Convert.ToString(txtPANNo.Text);
                        mLedgerDetails.FSSAI = Convert.ToString(txtFSSAINo.Text);
                        mLedgerDetails.AdharNo = Convert.ToString(txtAdharNo.Text);
                        mLedgerDetails.UserID = DBGetVal.UserID;
                        mLedgerDetails.UserDate = DBGetVal.ServerTime.Date;
                        mLedgerDetails.AddressLang = txtAddressLang.Text.Trim();
                        mLedgerDetails.RateTypeNo = Convert.ToInt32(lstRateType.SelectedValue);
                        mLedgerDetails.DiscPer = Convert.ToDouble(txtDiscPer.Text);
                        mLedgerDetails.DiscRs = Convert.ToDouble(txtDiscRs.Text);
                        mLedgerDetails.AnyotherNo1 = Convert.ToString(txtAnyotherno1.Text);
                        mLedgerDetails.AnyotherNo2 = Convert.ToString(txtAnyotherno2.Text);
                        mLedgerDetails.GSTNo = Convert.ToString(txtGSTNo.Text);
                        mLedgerDetails.GSTDate = Convert.ToDateTime(txtGSTDate.Text);

                        mLedgerDetails.FSSAIDate = Convert.ToDateTime(tFSSAIDate.Value);
                        mLedgerDetails.AccountNo = "0";
                        mLedgerDetails.ReportName = ".";
                        dbLedger.AddMLedgerDetails(mLedgerDetails);
                    }

                    if (dbLedger.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Supplier Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select LedgerNo from MLedger Where GroupNo=" + GroupType.SundryDebtors + " order by LedgerName ").Table;
                            dtLedger = ObjFunction.GetDataView("Select LedgerName from MLedger Where GroupNo=" + GroupType.SundryDebtors + " order by LedgerName").Table;
                            ID = ObjQry.ReturnLong("Select Max(LedgerNo) FRom MLedger", CommonFunctions.ConStr);
                            if (ShortID == 0)
                            {

                                FillFields();
                                ObjFunction.LockButtons(true, this.Controls);
                                ObjFunction.LockControls(false, this.Controls);
                                btnContactPersonLang.Enabled = false;
                                btnAddressLang.Enabled = false;
                                btnLangLongDesc.Enabled = false;
                                btnNew.Focus();
                            }
                            else
                            {
                                ShortID = ID;
                                this.Close();
                            }
                        }
                        else
                        {
                            OMMessageBox.Show("Supplier Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillFields();
                            ObjFunction.LockButtons(true, this.Controls);
                            ObjFunction.LockControls(false, this.Controls);
                            btnContactPersonLang.Enabled = false;
                            btnAddressLang.Enabled = false;
                            btnLangLongDesc.Enabled = false;
                            btnNew.Focus();
                        }
                    }

                    else
                        OMMessageBox.Show("Supplier Not Added ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                    DisplayCustomerCount();

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Supplier();
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

                if (ObjQry.ReturnLong("Select Count(*) from TVoucherEntry Inner Join TvoucherDetails on TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo where TVoucherEntry.VoucherDate>='" + DBGetVal.FromDate + "' and TVoucherEntry.VoucherDate<='" + DBGetVal.ToDate + "' and TVoucherDetails.LedgerNo=" + ID + "", CommonFunctions.ConStr) > 0)
                {
                    OMMessageBox.Show("Sorry You can not delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {

                    if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (dbLedger.DeleteMLedger(mLedger) == true)
                        {
                            OMMessageBox.Show("Customer Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillFields();
                        }
                        else
                        {
                            OMMessageBox.Show("Customer not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }
                }
                DisplayCustomerCount();
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Customer.RequestCustomerNo = 0;
            this.Close();
        }

        private void txtLedgerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtLedgerName.Text.Trim() == "")
                    {

                        OMMessageBox.Show("Enter Supplier Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        EP.SetIconAlignment(txtLedgerName, ErrorIconAlignment.MiddleRight);
                        txtLedgerName.Focus();
                    }
                    else if (txtLedgerName.Text.Trim() != "")
                    {

                        if (ObjQry.ReturnInteger("Select Count(*) from MLedger where LedgerName = '" + txtLedgerName.Text.Trim().ToUpper().Replace("'", "''") + "' And GroupNo=" + GroupType.SundryCreditors + " and LedgerNo not in (" + ID + ")", CommonFunctions.ConStr) != 0)
                        {
                            OMMessageBox.Show("Duplicate Supplier Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            EP.SetIconAlignment(txtLedgerName, ErrorIconAlignment.MiddleRight);
                            txtLedgerName.Focus();
                        }
                        else
                        {
                            if (FlagBilingual == true)
                            {
                                Utilities.KeyBoard frmkb;
                                {
                                    string val = ObjFunction.ChecklLangVal(txtLedgerName.Text.Trim());
                                    if (val == "")
                                    {
                                        frmkb = new Utilities.KeyBoard(4, txtLedgerName.Text.Trim(), txtLangName.Text, "", "");
                                        ObjFunction.OpenForm(frmkb);
                                        if (frmkb.DS == DialogResult.OK)
                                        {
                                            txtLangName.Text = frmkb.strLanguage.Trim();
                                            frmkb.Close();
                                        }
                                    }
                                    else
                                        txtLangName.Text = val;
                                }


                                txtLedgerName.Text = txtLedgerName.Text.Trim().ToUpper();
                                txtLedgerName_Leave(sender, new EventArgs());

                            }
                            else
                            {
                                txtLedgerName.Text = txtLedgerName.Text.Trim().ToUpper();
                                txtLedgerName_Leave(sender, new EventArgs());
                                txtContactPer.Focus();
                            }

                        }


                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {

                if (FlagBilingual == true)
                {
                    if (txtLedgerName.Text.Trim() != "")
                    {
                        if (txtLangName.Text.Trim().Length == 0)
                        {
                            btnLangLongDesc_Click(btnLangLongDesc, null);
                        }
                        txtContactPer.Text = txtLedgerName.Text;
                    }
                }
                if (ID == 0 && txtLedgerName.Text != "")
                {
                    txtContactPer.Text = txtLedgerName.Text;
                }
                txtContactPer.Focus();


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtLangName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtLangName.Text.Trim() == "")
                {

                    btnLangLongDesc.Focus();
                }
                else
                {
                    txtContactPer.Focus();
                }

            }
        }

        private void txtContactPer_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtContactPer.Text.Trim() == "")
                    {

                        OMMessageBox.Show("Enter Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        EP.SetIconAlignment(txtContactPer, ErrorIconAlignment.MiddleRight);
                        txtContactPer.Focus();
                    }
                    if (txtContactPer.Text.Trim() != "")
                    {
                        if (FlagBilingual == true)
                        {
                            Utilities.KeyBoard frmkb;
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
                                txtMobileNo1.Focus();
                            }
                        }
                        else
                        {
                            txtMobileNo1.Focus();
                        }
                        txtContactPer.Text = txtContactPer.Text.Trim().ToUpper();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtMobileNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtOPhone1.Focus();
            }
        }

        private void txtOPhone1_KeyPress(object sender, KeyPressEventArgs e)
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
                txtOAddress.GotFocus += delegate { txtOAddress.Select(txtOAddress.Text.Length, txtOAddress.Text.Length); };
                txtOAddress.Focus();
            }
        }

        private void txtOAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (FlagBilingual == true)
                    {
                        Utilities.KeyBoard frmkb; if (txtOAddress.Text != "")
                        {
                            string val = ObjFunction.ChecklLangVal(txtOAddress.Text.Trim());
                            if (val == "")
                            {
                                frmkb = new Utilities.KeyBoard(4, txtOAddress.Text.Trim(), txtAddressLang.Text, txtOAddress.Text, "");
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
                    txtOAddress.Text = txtOAddress.Text.Trim().ToUpper();
                    txtOPinCode.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtAddressLang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAddressLang.Text.Trim() == "")
                {

                    btnAddressLang.Focus();
                }
                else
                {
                    txtOPinCode.Focus();
                }

            }
        }

        private void txtOPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ObjFunction.SetMasked(txtOPinCode, 0, 6, OMFunctions.MaskedType.NotNegative);
                txtStateName.Focus();
            }
        }

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
                    if (lstCityName.Items.Count > 0)
                    {
                        pnlCityName.Visible = true;
                        lstCityName.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("City is not Available in Selected state", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        pnlCityName.Visible = false;
                        txtAreaName.Focus();
                    }

                }
                else
                {
                    pnlCityName.Visible = false;

                    txtAreaName.Focus();

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

                    txtAreaName.Focus();
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

        private void txtAreaName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAreaName.Text == "")
                {

                    if (lstAreaName.Items.Count > 0)
                    {
                        pnlAreaName.Visible = true;
                        lstAreaName.Focus();

                    }
                    else
                    {
                        OMMessageBox.Show("Area is not Available in Selected City", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        pnlAreaName.Visible = false;
                        txtCreditLimit.Focus();
                    }

                }
                else
                {
                    pnlAreaName.Visible = false;
                    txtCreditLimit.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtAreaName.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstAreaName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtAreaName.Text = lstAreaName.Text;

                    txtCreditLimit.Focus();
                    pnlAreaName.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtAreaName.Focus();
                pnlAreaName.Visible = false;

            }
        }

        private void txtCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCreditLimit.Text.Trim() == "")
                {

                    txtCreditLimit.Focus();
                }
                else
                {
                    txtCreditDays.Focus();
                }
            }
        }

        private void txtCreditDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCreditDays.Text.Trim() == "")
                {
                    txtCreditDays.Focus();
                }
                else
                {
                    txtGSTNo.Focus();
                }
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

                        if (txtGSTNo.Text.Length == 15)
                        {
                            long statecode = ObjQry.ReturnLong("Select Statecode from Mstate where stateno=" + lstStateName.SelectedValue, CommonFunctions.ConStr);

                            if ((statecode != Convert.ToInt32(txtGSTNo.Text.Substring(0, 2))))
                            {
                                OMMessageBox.Show("Enter GST No", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                EP.SetIconAlignment(txtGSTNo, ErrorIconAlignment.MiddleRight);
                                txtGSTNo.Focus();
                            }
                            else
                            {
                                txtGSTNo.Text = txtGSTNo.Text.Trim().ToUpper();
                                txtGSTDate.Focus();
                                EP.SetError(txtGSTNo, "");
                            }
                        }
                        else
                        {
                            OMMessageBox.Show("Enter Correct GSTNo", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            EP.SetIconAlignment(txtGSTNo, ErrorIconAlignment.MiddleRight);
                            txtGSTNo.Focus();
                        }
                    }
                    else
                    {
                        txtCustType.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void txtGSTDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCustType.Focus();
            }
        }
        private void txtDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCustType.Focus();
            }
        }
        private void txtCustType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCustType.Text == "")
                {
                    if (lstCustType.Items.Count > 0)
                    {
                        pnlCustType.Visible = true;
                        lstCustType.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Customer type not Available ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        pnlCustType.Visible = false;
                        chkActive.Focus();
                    }

                }
                else
                {
                    pnlCustType.Visible = false;
                    chkActive.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtCustType.Text = "";
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
                    txtCustType.Text = lstCustType.Text;
                    //chkActive.Focus();
                    if ((ObjFunction.GetListValue(lstCustType) == 2) || (ObjFunction.GetListValue(lstCustType) == 4))
                    {
                        if (txtGSTNo.Text != "")
                        {
                            if ((txtGSTNo.MaxLength < 15) || (txtGSTNo.MaxLength > 15))
                            {
                                EP.SetError(txtGSTNo, "Enter Correct 15 Digit GSTNo");
                                txtGSTNo.Focus();
                            }
                            else
                            {
                                txtGSTDate.Focus();
                            }
                        }
                        else
                        {
                            txtGSTDate.Focus();
                        }
                    }
                    else if (ObjFunction.GetListValue(lstCustType) == 3)
                    {
                        if (txtGSTNo.Text == "")
                        {
                            EP.SetError(txtGSTNo, "Enter 15 Digit GSTNo");
                            txtGSTNo.MaxLength = 15;
                            txtGSTNo.Focus();
                        }
                        else
                        {
                            EP.SetError(txtGSTNo, "");
                            chkActive.Focus();
                        }
                    }
                    pnlCustType.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtCustType.Focus();
                pnlCustType.Visible = false;

            }
        }

        private void chkActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnSave.Focus();
            }
        }

        private void btnAdditional1_Click(object sender, EventArgs e)
        {
            pnlAdditional1.Visible = !pnlAdditional1.Visible;
            pnlAdditional2.Visible = false;
            pnlAdditional1.Location = new Point(157, 100);
            if (pnlAdditional1.Visible == true)
                txtPhoneNo2.Focus();
        }

        private void btnAdditional2_Click(object sender, EventArgs e)
        {
            pnlAdditional2.Location = new Point(157, 100);
            pnlAdditional2.Visible = !pnlAdditional2.Visible;
            pnlAdditional1.Visible = false;
            if (pnlAdditional2.Visible == true)
                txtAnyotherno1.Focus();
        }

        private void txtPhoneNo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtMobile2.Focus();
            }
        }

        private void txtMobile2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtFSSAINo.Focus();
            }
        }

        private void txtFSSAINo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                tFSSAIDate.Focus();
            }
        }

        private void tFSSAIDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtPANNo.Focus();
            }
        }
        private void txtFSSAIDate_KeyPress(object sender, KeyPressEventArgs e)
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
                btnAdd1Ok.Focus();
            }
        }


        private void txtAnyotherno1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtAnyotherno2.Focus();
            }
        }

        private void txtAnyotherno2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnAdd2Ok.Focus();
            }
        }
        private void txtRateType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtRateType.Text == "")
                {
                    pnlRateType.Visible = true;
                    lstRateType.Focus();
                }
                else
                {
                    pnlRateType.Visible = false;
                    txtTransporter.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtRateType.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void txtTransporter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtTransporter.Text == "")
                {
                    pnlTransporter.Visible = true;
                    lstTransporter.Focus();
                }
                else
                {
                    pnlTransporter.Visible = false;
                    txtDiscPer.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtTransporter.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstRateType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtRateType.Text = lstRateType.Text;

                    txtRateType.Focus();
                    pnlRateType.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtRateType.Focus();
                pnlRateType.Visible = false;

            }
        }

        private void lstTransporter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtTransporter.Text = lstTransporter.Text;

                    txtTransporter.Focus();
                    pnlTransporter.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtTransporter.Focus();
                pnlTransporter.Visible = false;

            }
        }

        private void txtDiscPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtDiscRs.Focus();
            }
        }

        private void txtDiscRs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                chkPartyWiseRate.Focus();
            }
        }

        private void chkPartyWiseRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                chkQuotationRate.Focus();
            }
        }

        private void chkQuotationRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtAnyotherno1.Focus();
            }
        }

        private void btnAdd1Ok_Click(object sender, EventArgs e)
        {
            pnlAdditional1.Visible = false;
            BtnSave.Focus();
        }
        private void btnAdd1Cancel_Click(object sender, EventArgs e)
        {
            pnlAdditional1.Visible = false;
            BtnSave.Focus();
        }
        private void btnAdd2Ok_Click(object sender, EventArgs e)
        {
            pnlAdditional2.Visible = false;
            BtnSave.Focus();
        }

        private void btnAdd2Cancel_Click(object sender, EventArgs e)
        {
            pnlAdditional2.Visible = false;
            BtnSave.Focus();
        }



        private void txtMobile2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtMobile2.Visible == true)
                {
                    if (txtMobile2.Text != "")
                    {
                        if (txtMobile2.Text.Trim().Length < 10)
                        {
                            EP.SetError(txtMobile2, "Enter Valid Mobile No");
                            EP.SetIconAlignment(txtMobile2, ErrorIconAlignment.MiddleRight);
                            txtMobile2.Focus();
                        }
                        else if (mobNo2 != txtMobile2.Text.Trim())
                        {
                            if (txtMobile2.Text.Trim() != "1111111111")
                            {
                                if (ObjQry.ReturnInteger("Select Count(*) from MLedgerDetails where MobileNo1='" + txtMobile2.Text.Trim() + "'", CommonFunctions.ConStr) != 0)
                                {
                                    EP.SetError(txtMobile2, "Duplicate Mobile No");
                                    EP.SetIconAlignment(txtMobile2, ErrorIconAlignment.MiddleRight);
                                    txtMobile2.Focus();
                                }
                                else
                                    EP.SetError(txtMobile2, "");
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLangName.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtLedgerName.Text.Trim(), txtLangName.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLangName.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtLedgerName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtLedgerName.Text.Trim(), txtLangName.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLangName.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLangName.Text = val;
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

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void chkPartyWiseRate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPartyWiseRate.Checked == true)
                chkPartyWiseRate.Text = "Yes";
            else
                chkPartyWiseRate.Text = "No";
        }

        private void chkQuotationRate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuotationRate.Checked == true)
                chkQuotationRate.Text = "Yes";
            else
                chkQuotationRate.Text = "No";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (ShortID != 0)
            //{

            //    this.Close();

            //}
            if (ID != 0)
            { FillFields(); }
            else
            {
                NavigationDisplay(2);
            }
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);

            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true'  order by CityName");
            lbladdarea.Enabled = false;
            lbladdcity.Enabled = false;

            pnlStateName.Visible = false;
            pnlCityName.Visible = false;
            pnlAreaName.Visible = false;
            pnlCustType.Visible = false;
            pnlAdditional1.Visible = false;
            pnlAdditional2.Visible = false;

            btnContactPersonLang.Enabled = false;
            btnAddressLang.Enabled = false;
            btnLangLongDesc.Enabled = false;
            btnNew.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            lbladdarea.Enabled = true;
            lbladdcity.Enabled = true;
            chkActive.Checked = true;

            txtLedgerName.Focus();
            btnContactPersonLang.Enabled = true;
            btnAddressLang.Enabled = true;
            btnLangLongDesc.Enabled = true;
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
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (BtnSave.Visible) lbladdcity_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (BtnSave.Visible) lbladdarea_Click(sender, e);
            }
            //else if (e.KeyCode == Keys.Escape)
            //{
            //    BtnExit_Click(sender, e);
            //}
        }
        #endregion


        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo,Ledgername From MLedger Where GroupNo=" + GroupType.SundryCreditors + " and ledgerno not in (25) group by LedgerNo,ledgername order by ledgername asc ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo ,Ledgername From MLedger Where GroupNo=" + GroupType.SundryCreditors + " and ledgerno not in (25) group by LedgerNo,ledgername order by ledgername desc ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo ,Ledgername From MLedger where GroupNo=22 and LedgerNo >" + ID + " and ledgerno not in (25) group by LedgerNo,ledgername order by ledgername ").Table;
                    if ((dtSearch != null) && (dtSearch.Rows.Count != 0))
                    {
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
                    else
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
                else if (type == 4)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo ,Ledgername From MLedger where GroupNo=22 and LedgerNo <" + ID + " and ledgerno not in (25) group by LedgerNo,ledgername order by ledgername ").Table;
                    if ((dtSearch != null) && (dtSearch.Rows.Count != 0))
                    {
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

        private void txtContactPersonLang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtContactPersonLang.Text.Trim() == "")
                {

                    btnContactPersonLang.Focus();
                }
                else
                {
                    txtMobileNo1.Focus();
                }

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

        private void pnlMain_Click(object sender, EventArgs e)
        {
            if (pnlCityName.Visible == true)
            {
                pnlCityName.Visible = false;
                txtCityName.Focus();
            }
            else if (pnlAreaName.Visible == true)
            {
                pnlAreaName.Visible = false;
                txtAreaName.Focus();
            }
            else if (pnlStateName.Visible == true)
            {
                pnlStateName.Visible = false;
                txtStateName.Focus();
            }
            else if (pnlCustType.Visible == true)
            {
                pnlCustType.Visible = false;
                txtCustType.Focus();
            }
        }

        private void lstCustType_Leave(object sender, EventArgs e)
        {
            pnlCustType.Visible = false;
        }

        private void lstAreaName_Leave(object sender, EventArgs e)
        {
            pnlAreaName.Visible = false;
        }

        private void lstCityName_Leave(object sender, EventArgs e)
        {
            pnlCityName.Visible = false;
        }

        private void lstStateName_Leave(object sender, EventArgs e)
        {
            pnlStateName.Visible = false;
        }

        private void lbladdcity_Click(object sender, EventArgs e)
        {

            Master.CityAE NewF = new Yadi.Master.CityAE(-1);
            ObjFunction.OpenForm(NewF);
            // FillListAll();
            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' and stateno=" + lstStateName.SelectedValue + " order by CityName");

            txtCityName.Focus();
        }

        private void lbladdarea_Click(object sender, EventArgs e)
        {

            Master.AreaAE NewF = new Yadi.Master.AreaAE(-1);
            ObjFunction.OpenForm(NewF);
            //  FillListAll();
            ObjFunction.FillList(lstAreaName, "Select AreaNo,AreaName From MArea where IsActive='true' order by AreaName");

            txtAreaName.Focus();
        }





    }
}
