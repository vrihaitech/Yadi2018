using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;
using OMControls;

namespace Yadi.Master
{
    public partial class CompanyAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        
        DBMCompany dbComp = new DBMCompany();
        MCompany mComp = new MCompany();
        
        public static string CompName;

        public CompanyAE()
        {
            InitializeComponent();
        }

        private void CompanyAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(CmbCity, "Select CityNo ,CityName  From MCity Where IsActive='True'");
                ObjFunction.FillCombo(cmbOState, "Select StateNo,StateName From MState Where IsActive ='True' order by StateName");

                DTPFinanYear.Format = DateTimePickerFormat.Custom;
                DTPFinanYear.CustomFormat = "dd-MMM-yyyy";

                DTPBookBeginFrom.Format = DateTimePickerFormat.Custom;
                DTPBookBeginFrom.CustomFormat = "dd-MMM-yyyy";

                DTPBookEndsOn.Format = DateTimePickerFormat.Custom;
                DTPBookEndsOn.CustomFormat = "dd-MMM-yyyy";
                //DTPBookEndsOn.Text = Convert.ToDateTime(DTPBookBeginFrom.Text).AddYears(1).ToString("dd-MMM-yyyy");
                //DTPBookEndsOn.Text = Convert.ToDateTime(DTPBookEndsOn.Text).AddDays(-1).ToString("dd-MMM-yyyy");

                if (Company.RequestCompNo != 0)
                {
                    CompName = "";
                    FillControls();
                }
                else
                {
                    Company.RequestCompNo = ObjQry.ReturnLong("Select CompanyNo FRom MCompany", CommonFunctions.ConStr);
                    FillControls();
                }
                pnl3.Visible = false;
                Label31.Visible = false;
                TxtCompLogo.Visible = false;
                //
                btnBrowse.Visible = false;
                //lblLBTNo.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_LBTSystem));
                //txtLBTNo.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_LBTSystem));
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
                MCompany MF = new MCompany();
                MF = dbComp.ModifyMCompanyByID(Convert.ToInt32(Company.RequestCompNo));
                TxtCompName.Text = MF.CompanyName;
                CompName = MF.CompanyName.ToUpper();
                TxtAddress.Text = MF.AddressCode;
                TxtPhone.Text = MF.PhoneNo;
                CmbCity.SelectedValue = Convert.ToString(MF.CityCode);
                //---GST---//
                long stateno = ObjQry.ReturnLong("select Stateno from mstate where statecode=" + MF.StateCode,CommonFunctions.ConStr );
                cmbOState.SelectedValue = Convert.ToString(stateno);

                txtPinCode.Text = MF.PinCode;
                DTPFinanYear.Text = MF.FinancialYear.ToString("dd-MMM-yyyy");
                DTPBookBeginFrom.Text = MF.BooksBeginFrom.ToString("dd-MMM-yyyy");
                DTPBookEndsOn.Text = MF.BooksEndOn.ToString("dd-MMM-yyyy");
                TxtConfig.Text = MF.Config;
                TxtCSTNo.Text = MF.CSTNo;
                TxtBSTNo.Text = MF.BSTNo;
                txtTinNo.Text = MF.TinNo;
                TxtIncomeTaxNo.Text = MF.IncomeTaxNo;
                TxtVATNo.Text = MF.VatNo;
                TxtCurrencySymb.Text = MF.CurrencySymbol;
                if (MF.SymbolSuffixed == 1) ChkSymbSuffix.Checked = true;
                else ChkSymbSuffix.Checked = false;

                if (MF.AmountInMillion == 1) ChkAmtMill.Checked = true;
                else ChkAmtMill.Checked = false;

                if (MF.SpaceBetweenAmountAndSymbol == 1) ChkSpaceAmtSymb.Checked = true;
                else ChkSpaceAmtSymb.Checked = false;

                TxtNarraHead.Text = MF.Narr_Head;
                TxtNarraFooter.Text = MF.Narr_Foot;
                TxtDeclr.Text = MF.Narr_Terms;
                TxtCompLogo.Text = MF.LogoPath;
                DTPFinanYear.Enabled = false;
                txtLBTNo.Text = MF.LBTNo;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Validations();
                if (TxtCompName.Text != "" && TxtAddress.Text != "" && CmbCity.Text != "")// && ValidDate() == true)
                {
                    if (lblDuplicate.Visible != true)
                    {
                        if (Company.RequestCompNo != 0)
                        {
                            mComp.CompanyNo = Company.RequestCompNo;
                        }
                        else
                        {
                            //CreateDatabase();
                            mComp.CompanyNo = ObjQry.ReturnLong("Select Isnull(Max(CompanyNo), 0) + 1 from MCompany", CommonFunctions.ConStr);
                        }
                        long StateCode = ObjQry.ReturnLong("Select StateCode from MState where Stateno=" + ObjFunction.GetComboValue(cmbOState), CommonFunctions.ConStr);
                     
                        mComp.RegistrationNo = 0;
                        mComp.CompanyUserCode = "";
                        mComp.CompanyName = (TxtCompName.Text == "") ? Convert.ToString("") : Convert.ToString(TxtCompName.Text);
                        mComp.AddressCode = (TxtAddress.Text == "") ? Convert.ToString("") : Convert.ToString(TxtAddress.Text);
                        mComp.PhoneNo = (TxtPhone.Text == "") ? Convert.ToString("") : Convert.ToString(TxtPhone.Text);
                        mComp.CityCode = (CmbCity.Text == "") ? 0 : Convert.ToInt64(CmbCity.SelectedValue);
                        mComp.PinCode = txtPinCode.Text;
                        //mComp.FinancialYear = (DTPFinanYear.Text == "") ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(DTPFinanYear.Text);
                        //mComp.BooksBeginFrom = (DTPBookBeginFrom.Text == "") ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(DTPBookBeginFrom.Text);
                        //mComp.BooksEndOn = (DTPBookEndsOn.Text == "") ? Convert.ToDateTime("01-01-1900") : Convert.ToDateTime(DTPBookEndsOn.Text);
                        mComp.Config = (TxtConfig.Text == "") ? Convert.ToString("") : Convert.ToString(TxtConfig.Text);
                        mComp.CSTNo = (TxtCSTNo.Text == "") ? Convert.ToString("") : Convert.ToString(TxtCSTNo.Text);
                        mComp.BSTNo = (TxtBSTNo.Text == "") ? Convert.ToString("") : Convert.ToString(TxtBSTNo.Text);
                        mComp.TinNo = (txtTinNo.Text == "") ? Convert.ToString("") : Convert.ToString(txtTinNo.Text);
                        mComp.IncomeTaxNo = (TxtIncomeTaxNo.Text == "") ? Convert.ToString("") : Convert.ToString(TxtIncomeTaxNo.Text);
                        mComp.VatNo = (TxtVATNo.Text == "") ? Convert.ToString("") : Convert.ToString(TxtVATNo.Text);
                        mComp.CurrencySymbol = (TxtCurrencySymb.Text == "") ? Convert.ToString("") : Convert.ToString(TxtCurrencySymb.Text);
                        mComp.MailingName = "";
                        mComp.MaintainCode = 0;
                        mComp.BaseCurrencySymbol = "";
                        mComp.FormalName = "";
                        mComp.NoOfDecimalPlaces = 0;
                        mComp.SymbolSuffixed = (ChkSymbSuffix.Checked == true) ? 1 : 0;
                        mComp.AmountInMillion = (ChkAmtMill.Checked == true) ? 1 : 0;
                        mComp.SpaceBetweenAmountAndSymbol = (ChkSpaceAmtSymb.Checked == true) ? 1 : 0;
                        mComp.UICulture = "";
                        mComp.FontName = "";
                        mComp.FontSize1 = 0;
                        mComp.FontSize2 = 0;
                        mComp.LogoPath = TxtCompLogo.Text;
                        mComp.Narr_Head = (TxtNarraHead.Text == "") ? Convert.ToString("") : Convert.ToString(TxtNarraHead.Text);
                        mComp.Narr_Foot = (TxtNarraFooter.Text == "") ? Convert.ToString("") : Convert.ToString(TxtNarraFooter.Text);
                        mComp.Narr_Terms = (TxtDeclr.Text == "") ? Convert.ToString("") : Convert.ToString(TxtDeclr.Text);
                        mComp.CompanyType = 0;
                        mComp.LBTNo = txtLBTNo.Text;
                        //--GST---//
                        mComp.StateCode = StateCode;

                        if (dbComp.AddMCompany(mComp) == true)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Company Added Successfully";
                            CompName = "";
                            CommonFunctions.CompanyName = TxtCompName.Text;
                            DBGetVal.FirmName = TxtCompName.Text;
                            DBGetVal.CompanyAddress = TxtAddress.Text;
                            DBGetVal.FromDate = Convert.ToDateTime(DTPBookBeginFrom.Value);
                            DBGetVal.ToDate = Convert.ToDateTime(DTPBookEndsOn.Value);
                            this.Close();
                            //Form childForm = new Master.Company();
                            //childForm.Text = "Company";
                            //ObjFunction.OpenForm(childForm, DBGetVal.MainForm);

                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Company not Saved";
                        }
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
            CompName = "";
            this.Close();
            //Form childForm = new Master.Company();
            //childForm.Text = "Company";
            //ObjFunction.OpenForm(childForm, DBGetVal.MainForm);
        }

        private void CompanyAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            CompName = ""; Company.RequestCompNo = 0;
        }

        public void Validations()
        {
               
            if (TxtCompName.Text != "")
            {
                int Cnt = ObjQry.ReturnInteger("Select Count(*) from MCompany where CompanyName = N'" + TxtCompName.Text.Replace("'","''") + "'", CommonFunctions.ConStr);
                if (Cnt != 0)
                {
                    if (CompName != TxtCompName.Text.ToUpper())
                    {
                        TxtCompName.Focus();
                        lblDuplicate.Visible = true;
                    }
                    else
                        lblDuplicate.Visible = false;
                }
                else
                {
                    lblDuplicate.Visible = false;
                }
            }

            
            if (TxtCompName.Text == "") lblCompName.Visible = true;
            else lblCompName.Visible = false;

            if (TxtAddress.Text == "") lblAddress.Visible = true;
            else lblAddress.Visible = false;

            if (ObjFunction.GetComboValue(cmbOState) <= 0)
            {
                EP.SetError(cmbOState, "Select State");
                EP.SetIconAlignment(cmbOState, ErrorIconAlignment.MiddleRight);
                cmbOState.Focus();
            }
           

         if (CmbCity.Text == "") lblCity.Visible = true;
            else lblCity.Visible = false;

        }
        

        private void DTPFinanYear_ValueChanged(object sender, EventArgs e)
        {
            //DTPBookBeginFrom.Text = DTPFinanYear.Text;
            //DTPBookEndsOn.Text = Convert.ToDateTime(DTPBookBeginFrom.Text).AddYears(1).AddDays(-1).ToString("dd-MMM-yyyy");
            //DTPBookEndsOn.Text = Convert.ToDateTime(DTPBookEndsOn.Text).AddDays(-1).ToString("dd-MMM-yyyy");
        }

        public void CreateDatabase()
        {
            string str1, str2, dbname = "", strInitdbName, strConnect = "";
            SqlConnection MyConn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlConnection MyConn2 = new SqlConnection();
            try
            {

                strInitdbName = "Account180001";
                strConnect = "server=" + CommonFunctions.ServerName + " ;Integrated Security=True;Database=Master";
                MyConn = new SqlConnection(strConnect);
                MyConn.Open();
                //=====================get maximum company number from company database =====================

                long MaxCompNo = 0;
                str2 = "select Isnull(max(right(name,len(name)-" + strInitdbName.Length + ")),0)+ 1 from sysdatabases where left(name," + strInitdbName.Length + ")='" + strInitdbName + "'";
                MaxCompNo = ObjQry.ReturnLong(str2, strConnect);

                dbname = strInitdbName + MaxCompNo;
                //set the Server Path.
                string strpath1 = "";// Server.MapPath("data");
                strpath1 = "C:\\Databases\\" + dbname;
                //CREATE DATABASE
                str1 = "CREATE DATABASE " + dbname + " ON  ( NAME = N'" + dbname + "_dat',FILENAME = N'" + strpath1 + "_dat.mdf' ) LOG ON	( NAME = N'" + dbname + "_log', FILENAME = N'" + strpath1 + "_log.ldf');";


                cmd = new SqlCommand(str1, MyConn);
                cmd.ExecuteNonQuery();


                //Save Data to Company Address
                MyConn.Close();


            
                strConnect = "server=" + CommonFunctions.ServerName + " ;Integrated Security=True;Database=" + dbname + "";
                MyConn2 = new SqlConnection(strConnect);

                System.IO.StreamReader sr = new System.IO.StreamReader("C:\\Databases\\" + strInitdbName + ".sql"); ;
                string sql = sr.ReadToEnd();
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("GO", System.Text.RegularExpressions.RegexOptions.IgnoreCase & System.Text.RegularExpressions.RegexOptions.Multiline);

                string[] lines = regex.Split(sql);
                MyConn2.Open();

                cmd = new SqlCommand();
                //cmd.ExecuteNonQuery();

                cmd.Connection = MyConn2;
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        cmd.CommandText = line;
                        cmd.CommandType = CommandType.Text;
                    }
                    cmd.ExecuteNonQuery();
                }


                //CommonFunctions.ConStr = "Data Source=OM1\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=OMBanking";
                //
                CommonFunctions ObjFunction = new CommonFunctions();
                CommonFunctions.ConStr = "Data Source="+ CommonFunctions.ServerName +";Integrated Security=SSPI;Initial Catalog=" + dbname + "";
                string ConStr="";
                MyConn = new SqlConnection(ConStr);
                MyConn.Open();
                string NewdbName = dbname, PrevdbName = strInitdbName + Convert.ToString(Convert.ToInt32(dbname.Substring(strInitdbName.Length, 1)) - 1);
                //For Group
                string strQuery = "Delete From " + NewdbName + ".dbo.MGroup  " +
                "insert " + NewdbName + ".dbo.MGroup  Select * from " + PrevdbName + ".dbo.MGroup Where ControlGroup=0 ";//Where ControlGroup=0
                //for VoucherType
                strQuery = strQuery + "Delete From " + NewdbName + ".dbo.MVoucherType  " +
                "insert " + NewdbName + ".dbo.MVoucherType  Select * from " + PrevdbName + ".dbo.MVoucherType ";
                //// For Units
                //strQuery = strQuery + "Delete From " + NewdbName + ".dbo.MUnits  " +
                //"insert " + NewdbName + ".dbo.MUnits  Select * from " + PrevdbName + ".dbo.MUnits ";
                //For Sign
                strQuery = strQuery + "Delete From " + NewdbName + ".dbo.MSign  " +
                "insert " + NewdbName + ".dbo.MSign  Select * from " + PrevdbName + ".dbo.MSign ";
                // For City
                strQuery = strQuery + "Delete From " + NewdbName + ".dbo.MCity  " +
                "insert " + NewdbName + ".dbo.MCity  Select * from " + PrevdbName + ".dbo.MCity ";
                // For State
                strQuery = strQuery + "Delete From " + NewdbName + ".dbo.MState  " +
                "insert " + NewdbName + ".dbo.MState  Select * from " + PrevdbName + ".dbo.MState ";
                // For Country
                strQuery = strQuery + "Delete From " + NewdbName + ".dbo.MCountry  " +
                "insert " + NewdbName + ".dbo.MCountry  Select * from " + PrevdbName + ".dbo.MCountry ";
                // For Stock Groups
                strQuery = strQuery + "Delete From " + NewdbName + ".dbo.MStockGroups  " +
                "insert " + NewdbName + ".dbo.MStockGroups  Select * from " + PrevdbName + ".dbo.MStockGroups ";
                //// For Stock Items
                //strQuery = strQuery + "Delete From " + @NewdbName + ".dbo.MStockItems " +
                //" set Identity_Insert " + @NewdbName + ".dbo.MStockItems ON " +
                //" insert " + @NewdbName + ".dbo.MStockItems (ItemNo,CompanyNo,ItemUserNo,ItemName,GroupNo,CategoryNo,UnitCode,VatPercent,BatchFlag,ExpiryDateFlag,RateOfDuty,MfgCompanyNo,MinLevel,MaxLevel,ReorderLevel,ReorderQty,RetailPrice,WholeSalePrice,OpBalGodown,OpBalBatch,OpBalQty,TrnCode,OpBalRate,OpBalAmount,ModifiedBy ) " +
                //" Select * from " + @PrevdbName + ".dbo.MStockItems " +
                //" set Identity_Insert " + @NewdbName + ".dbo.MStockItems OFF ";
                //For Default Ledgers
                strQuery = strQuery + "Delete From " + @NewdbName + ".dbo.MLedger " +
                " set Identity_Insert " + @NewdbName + ".dbo.MLedger ON " +
                " insert " + @NewdbName + ".dbo.MLedger (LedgerNo,CompanyNo,LedgerUserNo,LedgerName,GroupNo,InvFlag,OpeningBalance,SignCode,AddressCode,ModifiedBy ) " +
                " Select * from " + @PrevdbName + ".dbo.MLedger Where LedgerNo in(1,2) " +
                " set Identity_Insert " + @NewdbName + ".dbo.MLedger OFF ";

                strQuery = strQuery + " insert into OMBanking.dbo.MAccBankingLedger values (1,20,25,0,'" + Convert.ToDateTime(DTPBookBeginFrom.Value) + "')";
                //cmd = new SqlCommand("Exec SetAllDefaultRecords " + dbname + "," + strInitdbName + Convert.ToString(Convert.ToInt32(dbname.Substring(strInitdbName.Length, 1)) - 1) + "", MyConn);
                cmd = new SqlCommand(strQuery, MyConn);
                cmd.ExecuteNonQuery();

                //CommonFunctions.ConStr = "Data Source=OM1\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog="+ dbname +"";
                //

                MyConn2.Close();
                MyConn = null;
            }
            catch (Exception e)
            {
                MyConn2.Close();
                CommonFunctions.ErrorMessge = e.Message;

                cmd = new SqlCommand("Drop DataBase " + dbname, MyConn);
                cmd.ExecuteNonQuery();
                MyConn.Close();
                MyConn2.Close();
            }


        }

        public bool ValidDate()
        {
            DateTime FrDt, ToDt; bool flag = true;
            string ConStrDate = "Data Source="+ CommonFunctions.ServerName +";Integrated Security=SSPI;Initial Catalog=Master";
            DataSet dsDate = new DataSet();
            dsDate = ObjDset.FillDset("New", "Select name From Sysdatabases Where name like 'Account18%'", ConStrDate);
            if (dsDate.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDate.Tables[0].Rows.Count; i++)
                {
                    FrDt = ObjQry.ReturnDate("Select BooksBeginFrom From " + dsDate.Tables[0].Rows[i].ItemArray[0].ToString() + ".dbo.MCompany", ConStrDate);
                    ToDt = ObjQry.ReturnDate("Select BooksEndOn From " + dsDate.Tables[0].Rows[i].ItemArray[0].ToString() + ".dbo.MCompany", ConStrDate);
                    if (FrDt >= Convert.ToDateTime(DTPBookBeginFrom.Value) && FrDt <= Convert.ToDateTime(DTPBookEndsOn.Value))
                    {
                        flag = false; break;
                    }
                    if (ToDt >= Convert.ToDateTime(DTPBookBeginFrom.Value) && ToDt <= Convert.ToDateTime(DTPBookEndsOn.Value))
                    {
                        flag = false; break;
                    }
                }
            }
            if (Company.RequestCompNo != 0) flag = true;
            if (flag == false) OMMessageBox.Show("Company already Exist between this date range,so select another financial Year", "Error", OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            return flag;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (OF.ShowDialog() == DialogResult.OK)
            {
                TxtCompLogo.Text = OF.FileName;
            }
            else
                TxtCompLogo.Text = "";
        }

        private void CmbCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ObjFunction.AutoComplete(ref CmbCity, e, true);
        }

        private void txtPinCode_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtPinCode, 0, 6, OMFunctions.MaskedType.NotNegative);
        }

        private void TxtPhone_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(TxtPhone);
        }

        private void TxtCompName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtAddress.Text = TxtAddress.Text.Trim();
                TxtAddress.GotFocus += delegate { TxtAddress.Select(TxtAddress.Text.Length, TxtAddress.Text.Length); };
            }
        }

        private void TxtNarraHead_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtNarraFooter.Text = TxtNarraFooter.Text.Trim();
                TxtNarraFooter.GotFocus += delegate { TxtNarraFooter.Select(TxtNarraFooter.Text.Length, TxtNarraFooter.Text.Length); };
            }
        }

        private void TxtNarraFooter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtDeclr.Text = TxtDeclr.Text.Trim();
                TxtDeclr.GotFocus += delegate { TxtDeclr.Select(TxtDeclr.Text.Length, TxtDeclr.Text.Length); };
            }
        }

        private void cmbOState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    ObjFunction.FillCombo(CmbCity, "Select CityNo,CityName From MCity where StateNo = " + ObjFunction.GetComboValue(cmbOState) + " AND  (IsActive='True' ) order by CityName");
                    CmbCity.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }
            }
        }

        private void cmbOState_Leave(object sender, EventArgs e)
        {
            cmbOState_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }        
    }
}
