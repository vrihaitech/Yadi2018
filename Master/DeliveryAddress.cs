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
    public partial class DeliveryAddress : Form
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
        long  ID, LID = 0;
        public long ShortID = 0;
        long StateCode = 0;

        public DeliveryAddress()
        {
            InitializeComponent();
        }
        long StateNo = 0,CustType=0;
        string GSTNO = "";
        DateTime GSTDate;

        public DeliveryAddress(long shortid)
        {
            InitializeComponent();
            ID = shortid;
        }
        public DeliveryAddress(long shortid,long stateno,long custtype,string gstno,DateTime date)
        {
            InitializeComponent();
            ID = shortid;
            StateNo = stateno;
            CustType = custtype;
            GSTNO = gstno;
            GSTDate = date;
           // FillListAll();
           // FillFields();
        }
        private void DeliveryAddress_Load(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                   
                }
                else
                {
                   
                }
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                lbladdarea.Enabled = false;
                lbladdcity.Enabled = false;
                formatpicture();
                FillListAll();
                DisplayCustomerCount();
                txtGSTDate.CustomFormat = "dd-MMM-yy";

                //  dtSearch = ObjFunction.GetDataView(" Select max(isnull(LedgerDetailsNo,0)) as LedgerDetailsNo FROM MLedgerDetails Where DeliveryLedgerNo=" + ID + "").Table;
                dtSearch = ObjFunction.GetDataView(" Select max(isnull(LedgerDetailsNo,0)) as LedgerDetailsNo FROM MLedgerDetails Where DeliveryLedgerNo=" + ID + " " +
                  "  union all Select max(isnull(LedgerDetailsNo, 0)) as LedgerDetailsNo FROM MLedgerDetails Where LedgerNo = " + ID + " " +
                    " order by LedgerDetailsNo desc                    ").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (Customer.RequestCustomerNo == 0)
                        LID = Convert.ToInt64(dtSearch.Rows[0]["LedgerDetailsNo"].ToString());
                    else
                        LID = Customer.RequestCustomerNo;
                    FillFields();

                }
                setDisplay(true);

                btnNew.Focus();
                KeyDownFormat(this.Controls);

                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillListAll()
        {
            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' order by CityName");
            ObjFunction.FillList(lstAreaName, "Select AreaNo,AreaName From MArea where IsActive='true' order by AreaName");
            ObjFunction.FillList(lstStateName, "Select StateNo,StateName From MState where IsActive='true' and stateno="+ StateNo +" order by StateName");


        }

        private void formatpicture()
        {
            pnlStateName.Top = txtStateName.Bottom;
            pnlStateName.Width = txtStateName.Width;
            pnlStateName.Height = 90;
           // lstStateName.Top = pnlStateName.Top +5;
           // lstStateName.Height = pnlStateName.Height - 5;


            pnlCityName.Top = txtCityName.Bottom+5;
            pnlCityName.Width = txtCityName.Width;
            pnlCityName.Height = 90;
           // lstCityName.Top = pnlCityName.Top + 5;
           // lstCityName.Height = pnlStateName.Height - 5;

            pnlAreaName.Top = txtAreaName.Bottom;
            pnlAreaName.Width = txtAreaName.Width;
            pnlAreaName.Height = 90;
           // lstAreaName.Top = pnlAreaName.Top +5;
           // lstAreaName.Height = pnlAreaName.Height - 5;

            pnlCustType.Top = txtCustType.Bottom;
            pnlCustType.Width = txtCustType.Width;
            pnlCustType.Height = 80;
          //  lstCustType.Top = pnlCustType.Top +5;
          //  lstCustType.Height = pnlCustType.Height - 5;

        }


        public void setDisplay(bool flag)
        {
            btnDelete.Visible = flag;
        }

        public void setDisable(bool flag)
        {
            BtnSave.Visible = !flag;
            btnCancel.Visible = !flag;

            btnUpdate.Visible = flag;
            //  btnSearch.Visible = flag;
            BtnExit.Visible = flag;
         
            btnDelete.Visible = flag;
        }
        private void DisplayCustomerCount()
        {
            lblTotalCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  MLedger WHERE  (GroupNo = " + GroupType.SundryDebtors + ")", CommonFunctions.ConStr).ToString();
            lblActiveCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  MLedger WHERE  (GroupNo = " + GroupType.SundryDebtors + ") AND (IsActive = 'true')", CommonFunctions.ConStr).ToString();
            lblDeActiveCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  MLedger WHERE  (GroupNo = " + GroupType.SundryDebtors + ") AND (IsActive = 'false')", CommonFunctions.ConStr).ToString();
            label9.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblTotalCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            label10.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblActiveCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            label11.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblDeActiveCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
        }

        public bool Validations()
        {
            bool flag = false;
            // EP.SetError(txtLedgerName, "");
            EP.SetError(txtMobileNo1, "");
            // EP.SetError(txtMobile2, "");
          //  EP.SetError(txtContactPer, "");
            // EP.SetError(txtCreditLimit, "");
            EP.SetError(txtStateName, "");
            //EP.SetError(txtEmailID, "");

            if (txtMobileNo1.Text.Trim() == "")
            {
                txtMobileNo1.Text = "1111111111";
                //EP.SetError(txtMobileNo1, "Enter Mobile Number");
                //EP.SetIconAlignment(txtMobileNo1, ErrorIconAlignment.MiddleRight);
                //txtMobileNo1.Focus();

            }
            else if (txtStateName.Text.Trim() == "")
            {
                EP.SetError(txtStateName, "Please Select State Name");
                EP.SetIconAlignment(txtStateName, ErrorIconAlignment.MiddleRight);
                txtStateName.Focus();
            }
            //else if (txtContactPer.Text.Trim() == "")
            //{
            //    EP.SetError(txtContactPer, "Enter Name");
            //    EP.SetIconAlignment(txtContactPer, ErrorIconAlignment.MiddleRight);
            //    txtContactPer.Focus();
            //}

            else
                flag = true;



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
                    //else if (mobNo != txtMobileNo1.Text.Trim())
                    //{
                    //    if (txtMobileNo1.Text.Trim() != "1111111111")
                    //    {
                    //        //if (ObjQry.ReturnInteger("Select Count(*) from MLedgerDetails where MobileNo1='" + txtMobileNo1.Text.Trim() + "'", CommonFunctions.ConStr) != 0)
                    //        //{
                    //        //    EP.SetError(txtMobileNo1, "Duplicate Mobile No");
                    //        //    EP.SetIconAlignment(txtMobileNo1, ErrorIconAlignment.MiddleRight);
                    //        //    txtMobileNo1.Focus();
                    //        //    flag = false;
                    //        //}
                    //        //else
                    //            flag = true;
                    //    }
                    //}

                }
            }


            return flag;
        }

        private void FillFields()
        {
            try
            {
                FillGrid();
                //  EP.SetError(txtLedgerName, "");
                EP.SetError(txtMobileNo1, "");
                // EP.SetError(txtMobile2, "");
              //  EP.SetError(txtContactPer, "");
                //  EP.SetError(txtCreditLimit, "");
                //EP.SetError(txtEmailID, "");
                EP.SetError(txtStateName, "");
                // mLedger = dbLedger.ModifyMLedgerByID(ID);
                mLedgerDetails = dbLedger.ModifyMLedgerDetailsByID2(LID);
                LedgerNm = mLedger.LedgerName;
                //  txtLedgerName.Text = mLedger.LedgerName;
                //    txtContactPer.Text = mLedger.ContactPerson;
                //   lstTransporter.SelectedValue = mLedger.TransporterNo.ToString();
                //  txtTransporter.Text = lstTransporter.Text;

                // chkActive.Checked = mLedgerDetails.IsActive;
                btnDelete.Visible = false;
                chkActive.Checked = true;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";

                //Ledger Details
                txtOAddress.Text = mLedgerDetails.Address;


                lstStateName.SelectedValue = StateNo;
                txtStateName.Text = lstStateName.Text;



                lstCityName.SelectedValue = mLedgerDetails.CityNo.ToString();
                txtCityName.Text = lstCityName.Text;


                lstAreaName.SelectedValue = mLedgerDetails.AreaNo.ToString();
                txtAreaName.Text = lstAreaName.Text;

                // lstRateType.SelectedValue = mLedgerDetails.RateTypeNo.ToString();
                // txtRateType.Text = lstRateType.Text;

                lstCustType.SelectedValue = mLedgerDetails.CustomerType.ToString();
                txtCustType.Text = lstCustType.Text;


                txtOPinCode.Text = mLedgerDetails.PinCode;
                txtOPhone1.Text = mLedgerDetails.PhNo1;
                //    txtPhoneNo2.Text = mLedgerDetails.PhNo2;
                //txtLangName.Text = String.IsNullOrEmpty(mLedger.LedgerLangName) ? "" : mLedger.LedgerLangName.ToString();
                //txtAddressLang.Text = String.IsNullOrEmpty(mLedgerDetails.AddressLang) ? "" : mLedgerDetails.AddressLang.ToString();
                //txtContactPersonLang.Text = String.IsNullOrEmpty(mLedger.ContactPersonLang) ? "" : mLedger.ContactPersonLang.ToString();
                txtMobileNo1.Text = mLedgerDetails.MobileNo1;
                mobNo = mLedgerDetails.MobileNo1;
                mobNo2 = mLedgerDetails.MobileNo2;
                // txtMobile2.Text = mLedgerDetails.MobileNo2;
                txtEmailID.Text = mLedgerDetails.EmailID;
                //txtFSSAINo.Text = (mLedgerDetails.FSSAI == null) ? "" : mLedgerDetails.FSSAI.ToString();
                //txtPANNo.Text = mLedgerDetails.PANNo;
                //txtAdharNo.Text = mLedgerDetails.AdharNo;
                //txtCreditLimit.Text = mLedgerDetails.CreditLimit.ToString("0.00");
                //txtDiscPer.Text = mLedgerDetails.DiscPer.ToString();
                //txtDiscRs.Text = mLedgerDetails.DiscRs.ToString();
                //lstTransporter.SelectedValue = mLedger.TransporterNo;

                txtGSTNo.Text = GSTNO;
                //txtAnyotherno1.Text = mLedgerDetails.AnyotherNo1;
                // txtAnyotherno2.Text = mLedgerDetails.AnyotherNo2;

                txtGSTDate.MinDate = Convert.ToDateTime("01-01-1900");
                //  tFSSAIDate.MinDate = Convert.ToDateTime("01-01-1900");
                txtGSTDate.Value = (GSTDate);
                // tFSSAIDate.Value = (mLedgerDetails.FSSAIDate);

                //chkPartyWiseRate.Checked = mLedger.IsPartyWiseRate;
                //if (chkPartyWiseRate.Checked == true)
                //    chkPartyWiseRate.Text = "Yes";
                //else
                //    chkPartyWiseRate.Text = "No";

                //chkQuotationRate.Checked = mLedger.QuotationRate;
                //if (chkQuotationRate.Checked == true)
                //    chkQuotationRate.Text = "Yes";
                //else
                //    chkQuotationRate.Text = "No";
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
                LID = 0;
                //lblCrDr.Text = "";
               // ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                lbladdarea.Enabled = true;
                lbladdcity.Enabled = true;
               // FillListAll();
                chkActive.Checked = true;
                txtAreaName.Text = "";
                txtMobileNo1.Text = "";
                txtOAddress.Text = "";
                txtStateName.Enabled = false;
                txtGSTDate.Enabled = false;
                txtGSTNo.Enabled = false;
                txtMobileNo1.Focus();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void FillGrid()
        {
            try
            {
                string sql = "";
                sql = "SELECT     MLedgerDetails.LedgerDetailsNo, MLedgerDetails.LedgerNo, MLedgerDetails.Address, MArea.AreaNo, MArea.AreaName, MCity.CityNo, " +
 " MCity.CityName,MLedgerDetails.PinCode, MLedgerDetails.PhNo1, MLedgerDetails.MobileNo1 " +
  " FROM MLedgerDetails left JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo left JOIN  MCity ON MLedgerDetails.CityNo = MCity.CityNo WHERE(LedgerNo = " + ID + ") " +
" union all SELECT MLedgerDetails.LedgerDetailsNo, MLedgerDetails.LedgerNo, " +
 " MLedgerDetails.Address, MArea.AreaNo, MArea.AreaName, MCity.CityNo, MCity.CityName,MLedgerDetails.PinCode, MLedgerDetails.PhNo1, MLedgerDetails.MobileNo1 " +
 " FROM MLedgerDetails left JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo left JOIN  MCity ON MLedgerDetails.CityNo = MCity.CityNo WHERE(DeliveryLedgerNo = "+ ID +")"; 
                DataView dv = new DataView();
                // Item itm = (Item)CmbSearch.SelectedItem;
                DataSet ds = new DataSet();
              
                    ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
               
               
                   // CommonFunctions.ErrorMessge = e.Message;
             
               /// return ds.Tables[0].DefaultView;
               dv= ds.Tables[0].DefaultView;
                // dv = dbLedger.GetBySearch(itm.Value, TxtSearch.Text);

                dataGridView1.DataSource = dv;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                // DataGridView1.Columns[1].Width = 550;
                // DataGridView1.Columns[2].Width = 100;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFields();
        }

        public void SaveFields()
        {
            try
            {
                if (Validations() == true)
                {

                    //---GST---//
                    StateCode = ObjQry.ReturnLong("Select StateCode from MState where (stateno = '" + ObjFunction.GetListValue(lstStateName) + "')", CommonFunctions.ConStr);

                    dbLedger = new DBMLedger();
           
                    mLedgerDetails = new MLedgerDetails();

                    mLedgerDetails.LedgerDetailsNo = LID;// ObjQry.ReturnLong("Select LedgerDetailsNo From MLedgerDetails Where LedgerNo=" + ID + "", CommonFunctions.ConStr);
                    mLedgerDetails.LedgerNo = ID;
                    mLedgerDetails.Address = txtOAddress.Text.Trim();

                    mLedgerDetails.StateNo = StateNo;
                 
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
                    //   mLedgerDetails.PhNo2 = txtPhoneNo2.Text.Trim();
                    mLedgerDetails.MobileNo1 = txtMobileNo1.Text.Trim();
                    //  mLedgerDetails.MobileNo2 = txtMobile2.Text.Trim();
                    mLedgerDetails.EmailID = txtEmailID.Text.Trim();
                    mLedgerDetails.CustomerType = Convert.ToInt32(lstCustType.SelectedValue);
                    mLedgerDetails.CreditLimit = 0;// (txtCreditLimit.Text.Trim() == "") ? 0 : Convert.ToDouble(txtCreditLimit.Text.Trim());
                    //   mLedgerDetails.PANNo = Convert.ToString(txtPANNo.Text);
                    //    mLedgerDetails.FSSAI = Convert.ToString(txtFSSAINo.Text);
                    //     mLedgerDetails.AdharNo = Convert.ToString(txtAdharNo.Text);
                    mLedgerDetails.UserID = DBGetVal.UserID;
                    mLedgerDetails.UserDate = DBGetVal.ServerTime.Date;
                    //     mLedgerDetails.AddressLang = txtAddressLang.Text.Trim();
                    //      mLedgerDetails.RateTypeNo = Convert.ToInt32(lstRateType.SelectedValue);
                    //    mLedgerDetails.DiscPer = Convert.ToDouble(txtDiscPer.Text);
                    //    mLedgerDetails.DiscRs = Convert.ToDouble(txtDiscRs.Text);
                    //    mLedgerDetails.AnyotherNo1 = Convert.ToString(txtAnyotherno1.Text);
                    //    mLedgerDetails.AnyotherNo2 = Convert.ToString(txtAnyotherno2.Text);
                    mLedgerDetails.GSTNo = GSTNO;
                    mLedgerDetails.GSTDate = Convert.ToDateTime(GSTDate);// Convert.ToDateTime(txtGSTDate.Text);
                    mLedgerDetails.FSSAIDate = GSTDate;// Convert.ToDateTime(tFSSAIDate.Value);
                    //mLedgerDetails.AccountNo = "0";
                   // mLedgerDetails.ReportName = ".";
                    mLedgerDetails.CompanyNo = DBGetVal.FirmNo;
                    dbLedger.AddMLedgerDetails2(mLedgerDetails);
                    // }

                    if (dbLedger.ExecuteNonQueryStatements() == true)
                    {
                        if (LID == 0)
                        {
                            OMMessageBox.Show("Customer Address Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                           // dtSearch = ObjFunction.GetDataView("Select max(LedgerDetailsNo) from MLedgerDetails Where Ledgerno=" + ID + "  ").Table;
                           // dtLedger = ObjFunction.GetDataView("Select LedgerName from MLedger Where GroupNo=" + GroupType.SundryDebtors + " order by LedgerName").Table;
                            LID = ObjQry.ReturnLong("Select max(LedgerDetailsNo) from MLedgerDetails Where Ledgerno=" + ID + "  ", CommonFunctions.ConStr);
                          

                                FillFields();
                         
                        }
                        else
                        {
                            OMMessageBox.Show("Customer address Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillFields();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        // btnContactPersonLang.Enabled = false;
                        // btnAddressLang.Enabled = false;
                        // btnLangLongDesc.Enabled = false;
                        btnNew.Focus();
                    }

                    else
                        OMMessageBox.Show("Customer Address Not Added ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

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

            Form NewF = new Customer();
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


        private void txtContactPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //if (e.KeyChar == Convert.ToChar(Keys.Enter))
                //{
                //    if (txtContactPer.Text.Trim() == "")
                //    {

                //        OMMessageBox.Show("Enter Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                //        EP.SetIconAlignment(txtContactPer, ErrorIconAlignment.MiddleRight);
                //        txtContactPer.Focus();
                //    }
                //    if (txtContactPer.Text.Trim() != "")
                //    {
                      
                //        txtMobileNo1.Focus();
               
                //        txtContactPer.Text = txtContactPer.Text.Trim().ToUpper();
                //    }
                //}
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



        private void txtOPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ObjFunction.SetMasked(txtOPinCode, 0, 6, OMFunctions.MaskedType.NotNegative);
                txtCityName.Focus();
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
                        BtnSave.Focus();
                    }

                }
                else
                {
                    pnlAreaName.Visible = false;
                    BtnSave.Focus();

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
                    BtnSave.Focus();
                    //txtCreditLimit.Focus();
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

                    chkActive.Focus();
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

                private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
          //  NavigationDisplay(2);
            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true'  order by CityName");

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            lbladdarea.Enabled = false;
            lbladdcity.Enabled = false;
            pnlStateName.Visible = false;
            pnlCityName.Visible = false;
            pnlAreaName.Visible = false;
            pnlCustType.Visible = false;
            LID = (dataGridView1.Rows[0].Cells[0].Value == null) ? 0 : Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);
            FillFields();
            btnNew.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            lbladdarea.Enabled = true;
            lbladdcity.Enabled = true; 
        }
   
        private void txtValid_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            LID = (dataGridView1.CurrentRow.Cells[0].Value == null) ? 0 : Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

           // LID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
            FillFields();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                //ObjFunction.SetGridStatus(e);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
            {
                // pnlDeliveryAddress.Visible = false;
                // IsDeliveryAddress = true;
                //  LedgerDetailsNo = (dgDeliveryAddress.CurrentRow.Cells[0].Value == null) ? 0 : Convert.ToInt32(dgDeliveryAddress.CurrentRow.Cells[0].Value);

                //,//
                dataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex));
            }
        }

        private void txtOAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtOAddress.Text != "")
                {
                    txtOPinCode.Focus();
                }
                else
                { txtOAddress.Focus(); }
            }
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
          
            if (e.KeyCode == Keys.F2)
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
        }
        #endregion

        
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

        private void lstCityName_Leave(object sender, EventArgs e)
        {
            pnlCityName.Visible = false;
            txtCityName.Focus();
        }

        private void lstCustType_Leave(object sender, EventArgs e)
        {
            pnlCustType.Visible = false;
            txtCustType.Focus();
        }

        private void lstAreaName_Leave(object sender, EventArgs e)
        {
            pnlAreaName.Visible = false;
            txtAreaName.Focus();
        }

        private void lstStateName_Leave(object sender, EventArgs e)
        {
            pnlStateName.Visible = false;
            txtStateName.Focus();
        }

        private void lbladdcity_Click(object sender, EventArgs e)
        {

            Master.CityAE NewF = new Yadi.Master.CityAE(-1);
            ObjFunction.OpenForm(NewF);
            //FillListAll();
            ObjFunction.FillList(lstCityName, "Select CityNo,CityName From MCity where IsActive='true' and stateno=" + lstStateName.SelectedValue + " order by CityName");

            txtCityName.Focus();
        }

        private void lbladdarea_Click(object sender, EventArgs e)
        {
            Master.AreaAE NewF = new Yadi.Master.AreaAE(-1);
            ObjFunction.OpenForm(NewF);
            // FillListAll();
            ObjFunction.FillList(lstAreaName, "Select AreaNo,AreaName From MArea where IsActive='true' order by AreaName");

            txtAreaName.Focus();

        }







    }
}
