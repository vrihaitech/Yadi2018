using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBMCompany
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMCompany(MCompany mcompany)
        {

            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMCompany";

            cmd.Parameters.AddWithValue("@CompanyNo", mcompany.CompanyNo);

            cmd.Parameters.AddWithValue("@RegistrationNo", mcompany.RegistrationNo);

            cmd.Parameters.AddWithValue("@CompanyUserCode", mcompany.CompanyUserCode);

            cmd.Parameters.AddWithValue("@CompanyName", mcompany.CompanyName);

            cmd.Parameters.AddWithValue("@AddressCode", mcompany.AddressCode);

            cmd.Parameters.AddWithValue("@PhoneNo", mcompany.PhoneNo);

            cmd.Parameters.AddWithValue("@CityCode", mcompany.CityCode);

            cmd.Parameters.AddWithValue("@PinCode", mcompany.PinCode);

            //cmd.Parameters.AddWithValue("@FinancialYear", mcompany.FinancialYear);

            //cmd.Parameters.AddWithValue("@BooksBeginFrom", mcompany.BooksBeginFrom);

            //cmd.Parameters.AddWithValue("@BooksEndOn", mcompany.BooksEndOn);

            cmd.Parameters.AddWithValue("@Config", mcompany.Config);

            cmd.Parameters.AddWithValue("@CSTNo", mcompany.CSTNo);

            cmd.Parameters.AddWithValue("@BSTNo", mcompany.BSTNo);

            cmd.Parameters.AddWithValue("@TinNo", mcompany.TinNo);

            cmd.Parameters.AddWithValue("@IncomeTaxNo", mcompany.IncomeTaxNo);

            cmd.Parameters.AddWithValue("@VatNo", mcompany.VatNo);

            cmd.Parameters.AddWithValue("@CurrencySymbol", mcompany.CurrencySymbol);

            cmd.Parameters.AddWithValue("@MailingName", mcompany.MailingName);

            cmd.Parameters.AddWithValue("@MaintainCode", mcompany.MaintainCode);

            cmd.Parameters.AddWithValue("@BaseCurrencySymbol", mcompany.BaseCurrencySymbol);

            cmd.Parameters.AddWithValue("@FormalName", mcompany.FormalName);

            cmd.Parameters.AddWithValue("@NoOfDecimalPlaces", mcompany.NoOfDecimalPlaces);

            cmd.Parameters.AddWithValue("@SymbolSuffixed", mcompany.SymbolSuffixed);

            cmd.Parameters.AddWithValue("@AmountInMillion", mcompany.AmountInMillion);

            cmd.Parameters.AddWithValue("@SpaceBetweenAmountAndSymbol", mcompany.SpaceBetweenAmountAndSymbol);

            cmd.Parameters.AddWithValue("@UICulture", mcompany.UICulture);

            cmd.Parameters.AddWithValue("@FontName", mcompany.FontName);

            cmd.Parameters.AddWithValue("@FontSize1", mcompany.FontSize1);

            cmd.Parameters.AddWithValue("@FontSize2", mcompany.FontSize2);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mcompany.ModifiedBy);

            cmd.Parameters.AddWithValue("@LogoPath", mcompany.LogoPath);

            cmd.Parameters.AddWithValue("@Narr_Head", mcompany.Narr_Head);

            cmd.Parameters.AddWithValue("@Narr_Foot", mcompany.Narr_Foot);

            cmd.Parameters.AddWithValue("@Narr_Terms", mcompany.Narr_Terms);

            cmd.Parameters.AddWithValue("@CompanyType", mcompany.CompanyType);

            cmd.Parameters.AddWithValue("@LBTNo", mcompany.LBTNo);


            //---GST--//
            cmd.Parameters.AddWithValue("@StateCode", mcompany.StateCode);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcompany.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMCompany(MCompany mcompany)
        {

            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMCompany";

            cmd.Parameters.AddWithValue("@CompanyNo", mcompany.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcompany.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MCompany ModifyMCompanyByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = null;
            SqlCommand cmd = null;
            sql = "Select * from MCompany where CompanyNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = null;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MCompany MM = new MCompany();
                while (dr.Read())
                {
                    MM.CompanyNo = Convert.ToInt32(dr["CompanyNo"]);
                    if (!(Convert.IsDBNull(dr["RegistrationNo"])))
                    {
                        MM.RegistrationNo = Convert.ToInt64(dr["RegistrationNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["CompanyUserCode"])))
                    {
                        MM.CompanyUserCode = Convert.ToString(dr["CompanyUserCode"]);
                    }
                    if (!(Convert.IsDBNull(dr["CompanyName"])))
                    {
                        MM.CompanyName = Convert.ToString(dr["CompanyName"]);
                    }
                    if (!(Convert.IsDBNull(dr["AddressCode"])))
                    {
                        MM.AddressCode = Convert.ToString(dr["AddressCode"]);
                    }
                    if (!(Convert.IsDBNull(dr["PhoneNo"])))
                    {
                        MM.PhoneNo = Convert.ToString(dr["PhoneNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["CityCode"])))
                    {
                        MM.CityCode = Convert.ToInt64(dr["CityCode"]);
                    }
                    if (!(Convert.IsDBNull(dr["PinCode"])))
                    {
                        MM.PinCode = Convert.ToString(dr["PinCode"]);
                    }
                    if (!(Convert.IsDBNull(dr["FinancialYear"])))
                    {
                        MM.FinancialYear = Convert.ToDateTime(dr["FinancialYear"]);
                    }
                    if (!(Convert.IsDBNull(dr["BooksBeginFrom"])))
                    {
                        MM.BooksBeginFrom = Convert.ToDateTime(dr["BooksBeginFrom"]);
                    }
                    if (!(Convert.IsDBNull(dr["BooksEndOn"])))
                    {
                        MM.BooksEndOn = Convert.ToDateTime(dr["BooksEndOn"]);
                    }
                    if (!(Convert.IsDBNull(dr["Config"])))
                    {
                        MM.Config = Convert.ToString(dr["Config"]);
                    }
                    if (!(Convert.IsDBNull(dr["CSTNo"])))
                    {
                        MM.CSTNo = Convert.ToString(dr["CSTNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["BSTNo"])))
                    {
                        MM.BSTNo = Convert.ToString(dr["BSTNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["TinNo"])))
                    {
                        MM.TinNo = Convert.ToString(dr["TinNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["IncomeTaxNo"])))
                    {
                        MM.IncomeTaxNo = Convert.ToString(dr["IncomeTaxNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["VatNo"])))
                    {
                        MM.VatNo = Convert.ToString(dr["VatNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["CurrencySymbol"])))
                    {
                        MM.CurrencySymbol = Convert.ToString(dr["CurrencySymbol"]);
                    }
                    if (!(Convert.IsDBNull(dr["MailingName"])))
                    {
                        MM.MailingName = Convert.ToString(dr["MailingName"]);
                    }
                    if (!(Convert.IsDBNull(dr["MaintainCode"])))
                    {
                        MM.MaintainCode = Convert.ToInt64(dr["MaintainCode"]);
                    }
                    if (!(Convert.IsDBNull(dr["BaseCurrencySymbol"])))
                    {
                        MM.BaseCurrencySymbol = Convert.ToString(dr["BaseCurrencySymbol"]);
                    }
                    if (!(Convert.IsDBNull(dr["FormalName"])))
                    {
                        MM.FormalName = Convert.ToString(dr["FormalName"]);
                    }
                    if (!(Convert.IsDBNull(dr["NoOfDecimalPlaces"])))
                    {
                        MM.NoOfDecimalPlaces = Convert.ToInt32(dr["NoOfDecimalPlaces"]);
                    }
                    if (!(Convert.IsDBNull(dr["SymbolSuffixed"])))
                    {
                        MM.SymbolSuffixed = Convert.ToInt32(dr["SymbolSuffixed"]);
                    }
                    if (!(Convert.IsDBNull(dr["AmountInMillion"])))
                    {
                        MM.AmountInMillion = Convert.ToInt32(dr["AmountInMillion"]);
                    }
                    if (!(Convert.IsDBNull(dr["SpaceBetweenAmountAndSymbol"])))
                    {
                        MM.SpaceBetweenAmountAndSymbol = Convert.ToInt32(dr["SpaceBetweenAmountAndSymbol"]);
                    }
                    if (!(Convert.IsDBNull(dr["UICulture"])))
                    {
                        MM.UICulture = Convert.ToString(dr["UICulture"]);
                    }
                    if (!(Convert.IsDBNull(dr["FontName"])))
                    {
                        MM.FontName = Convert.ToString(dr["FontName"]);
                    }
                    if (!(Convert.IsDBNull(dr["FontSize1"])))
                    {
                        MM.FontSize1 = Convert.ToInt32(dr["FontSize1"]);
                    }
                    if (!(Convert.IsDBNull(dr["FontSize2"])))
                    {
                        MM.FontSize2 = Convert.ToInt32(dr["FontSize2"]);
                    }
                    if (!(Convert.IsDBNull(dr["ModifiedBy"])))
                    {
                        MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    }
                    if (!(Convert.IsDBNull(dr["LogoPath"])))
                    {
                        MM.LogoPath = Convert.ToString(dr["LogoPath"]);
                    }
                    if (!(Convert.IsDBNull(dr["Narr_Head"])))
                    {
                        MM.Narr_Head = Convert.ToString(dr["Narr_Head"]);
                    }
                    if (!(Convert.IsDBNull(dr["Narr_Foot"])))
                    {
                        MM.Narr_Foot = Convert.ToString(dr["Narr_Foot"]);
                    }
                    if (!(Convert.IsDBNull(dr["Narr_Terms"])))
                    {
                        MM.Narr_Terms = Convert.ToString(dr["Narr_Terms"]);
                    }
                    if (!(Convert.IsDBNull(dr["CompanyType"])))
                    {
                        MM.CompanyType = Convert.ToInt32(dr["CompanyType"]);
                    }
                    if (!(Convert.IsDBNull(dr["LBTNo"])))
                    {
                        MM.LBTNo = Convert.ToString(dr["LBTNo"]);
                    }
                    if (!(Convert.IsDBNull(dr["StateCode"])))
                    {
                        MM.StateCode = Convert.ToInt32(dr["StateCode"]);
                    }
                }
                dr.Close();
                return MM;
            }
            else
            {
                dr.Close();
            }
            return new MCompany();
        }
    }

    /// <summary>
    /// This Class use for MCompany
    /// </summary>
    public class MCompany
    {
        private long mCompanyNo;
        private long mRegistrationNo;
        private string mCompanyUserCode;
        private string mCompanyName;
        private string mAddressCode;
        private string mPhoneNo;
        private long mCityCode;
        private string mPinCode;
        private DateTime mFinancialYear;
        private DateTime mBooksBeginFrom;
        private DateTime mBooksEndOn;
        private string mConfig;
        private string mCSTNo;
        private string mBSTNo;
        private string mTinNo;
        private string mIncomeTaxNo;
        private string mVatNo;
        private string mCurrencySymbol;
        private string mMailingName;
        private long mMaintainCode;
        private string mBaseCurrencySymbol;
        private string mFormalName;
        private int mNoOfDecimalPlaces;
        private int mSymbolSuffixed;
        private int mAmountInMillion;
        private int mSpaceBetweenAmountAndSymbol;
        private string mUICulture;
        private string mFontName;
        private int mFontSize1;
        private int mFontSize2;
        private string mModifiedBy;
        private string mLogoPath;
        private string mNarr_Head;
        private string mNarr_Foot;
        private string mNarr_Terms;
        private int mCompanyType;
        private int mStatusNo;
        private string mLBTNo;
        private string Mmsg;
        private long mStateCode;

        /// <summary>
        /// This Properties use for CompanyNo
        /// </summary>
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        /// <summary>
        /// This Properties use for RegistrationNo
        /// </summary>
        public long RegistrationNo
        {
            get { return mRegistrationNo; }
            set { mRegistrationNo = value; }
        }
        /// <summary>
        /// This Properties use for CompanyUserCode
        /// </summary>
        public string CompanyUserCode
        {
            get { return mCompanyUserCode; }
            set { mCompanyUserCode = value; }
        }
        /// <summary>
        /// This Properties use for CompanyName
        /// </summary>
        public string CompanyName
        {
            get { return mCompanyName; }
            set { mCompanyName = value; }
        }
        /// <summary>
        /// This Properties use for AddressCode
        /// </summary>
        public string AddressCode
        {
            get { return mAddressCode; }
            set { mAddressCode = value; }
        }
        /// <summary>
        /// This Properties use for PhoneNo
        /// </summary>
        public string PhoneNo
        {
            get { return mPhoneNo; }
            set { mPhoneNo = value; }
        }
        /// <summary>
        /// This Properties use for CityCode
        /// </summary>
        public long CityCode
        {
            get { return mCityCode; }
            set { mCityCode = value; }
        }
        /// <summary>
        /// This Properties use for PinCode
        /// </summary>
        public string PinCode
        {
            get { return mPinCode; }
            set { mPinCode = value; }
        }
        /// <summary>
        /// This Properties use for FinancialYear
        /// </summary>
        public DateTime FinancialYear
        {
            get { return mFinancialYear; }
            set { mFinancialYear = value; }
        }
        /// <summary>
        /// This Properties use for BooksBeginFrom
        /// </summary>
        public DateTime BooksBeginFrom
        {
            get { return mBooksBeginFrom; }
            set { mBooksBeginFrom = value; }
        }
        /// <summary>
        /// This Properties use for BooksEndOn
        /// </summary>
        public DateTime BooksEndOn
        {
            get { return mBooksEndOn; }
            set { mBooksEndOn = value; }
        }
        /// <summary>
        /// This Properties use for Config
        /// </summary>
        public string Config
        {
            get { return mConfig; }
            set { mConfig = value; }
        }
        /// <summary>
        /// This Properties use for CSTNo
        /// </summary>
        public string CSTNo
        {
            get { return mCSTNo; }
            set { mCSTNo = value; }
        }
        /// <summary>
        /// This Properties use for BSTNo
        /// </summary>
        public string BSTNo
        {
            get { return mBSTNo; }
            set { mBSTNo = value; }
        }
        /// <summary>
        /// This Properties use for TinNo
        /// </summary>
        public string TinNo
        {
            get { return mTinNo; }
            set { mTinNo = value; }
        }
        /// <summary>
        /// This Properties use for IncomeTaxNo
        /// </summary>
        public string IncomeTaxNo
        {
            get { return mIncomeTaxNo; }
            set { mIncomeTaxNo = value; }
        }
        /// <summary>
        /// This Properties use for VatNo
        /// </summary>
        public string VatNo
        {
            get { return mVatNo; }
            set { mVatNo = value; }
        }
        /// <summary>
        /// This Properties use for CurrencySymbol
        /// </summary>
        public string CurrencySymbol
        {
            get { return mCurrencySymbol; }
            set { mCurrencySymbol = value; }
        }
        /// <summary>
        /// This Properties use for MailingName
        /// </summary>
        public string MailingName
        {
            get { return mMailingName; }
            set { mMailingName = value; }
        }
        /// <summary>
        /// This Properties use for MaintainCode
        /// </summary>
        public long MaintainCode
        {
            get { return mMaintainCode; }
            set { mMaintainCode = value; }
        }
        /// <summary>
        /// This Properties use for BaseCurrencySymbol
        /// </summary>
        public string BaseCurrencySymbol
        {
            get { return mBaseCurrencySymbol; }
            set { mBaseCurrencySymbol = value; }
        }
        /// <summary>
        /// This Properties use for FormalName
        /// </summary>
        public string FormalName
        {
            get { return mFormalName; }
            set { mFormalName = value; }
        }
        /// <summary>
        /// This Properties use for NoOfDecimalPlaces
        /// </summary>
        public int NoOfDecimalPlaces
        {
            get { return mNoOfDecimalPlaces; }
            set { mNoOfDecimalPlaces = value; }
        }
        /// <summary>
        /// This Properties use for SymbolSuffixed
        /// </summary>
        public int SymbolSuffixed
        {
            get { return mSymbolSuffixed; }
            set { mSymbolSuffixed = value; }
        }
        /// <summary>
        /// This Properties use for AmountInMillion
        /// </summary>
        public int AmountInMillion
        {
            get { return mAmountInMillion; }
            set { mAmountInMillion = value; }
        }
        /// <summary>
        /// This Properties use for SpaceBetweenAmountAndSymbol
        /// </summary>
        public int SpaceBetweenAmountAndSymbol
        {
            get { return mSpaceBetweenAmountAndSymbol; }
            set { mSpaceBetweenAmountAndSymbol = value; }
        }
        /// <summary>
        /// This Properties use for UICulture
        /// </summary>
        public string UICulture
        {
            get { return mUICulture; }
            set { mUICulture = value; }
        }
        /// <summary>
        /// This Properties use for FontName
        /// </summary>
        public string FontName
        {
            get { return mFontName; }
            set { mFontName = value; }
        }
        /// <summary>
        /// This Properties use for FontSize1
        /// </summary>
        public int FontSize1
        {
            get { return mFontSize1; }
            set { mFontSize1 = value; }
        }
        /// <summary>
        /// This Properties use for FontSize2
        /// </summary>
        public int FontSize2
        {
            get { return mFontSize2; }
            set { mFontSize2 = value; }
        }
        /// <summary>
        /// This Properties use for ModifiedBy
        /// </summary>
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
        }
        /// <summary>
        /// This Properties use for LogoPath
        /// </summary>
        public string LogoPath
        {
            get { return mLogoPath; }
            set { mLogoPath = value; }
        }
        /// <summary>
        /// This Properties use for Narr_Head
        /// </summary>
        public string Narr_Head
        {
            get { return mNarr_Head; }
            set { mNarr_Head = value; }
        }
        /// <summary>
        /// This Properties use for Narr_Foot
        /// </summary>
        public string Narr_Foot
        {
            get { return mNarr_Foot; }
            set { mNarr_Foot = value; }
        }
        /// <summary>
        /// This Properties use for Narr_Terms
        /// </summary>
        public string Narr_Terms
        {
            get { return mNarr_Terms; }
            set { mNarr_Terms = value; }
        }
        /// <summary>
        /// This Properties use for CompanyType
        /// </summary>
        public int CompanyType
        {
            get { return mCompanyType; }
            set { mCompanyType = value; }
        }
        /// <summary>
        /// This Properties use for StatusNo
        /// </summary>
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public string LBTNo
        {
            get { return mLBTNo; }
            set { mLBTNo = value; }
        }
        /// <summary>
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
        //--GST--//
        public long StateCode
        {
            get { return mStateCode; }
            set { mStateCode = value; }
        }
    }

}
