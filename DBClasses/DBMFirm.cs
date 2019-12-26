using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMFirm
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMFirm(MFirm Mfirm)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMFirm";
            cmd.Parameters.AddWithValue("@FirmNo", Mfirm.FirmNo);
            cmd.Parameters.AddWithValue("@FirmName", Mfirm.FirmName);
            cmd.Parameters.AddWithValue("@ShortName", Mfirm.ShortName);
            cmd.Parameters.AddWithValue("@Address", Mfirm.Address);
            cmd.Parameters.AddWithValue("@StateNo", Mfirm.StateNo);
            cmd.Parameters.AddWithValue("@StateCode", Mfirm.StateCode);
            cmd.Parameters.AddWithValue("@CityNo", Mfirm.CityNo);
            cmd.Parameters.AddWithValue("@Pincode", Mfirm.Pincode);
            cmd.Parameters.AddWithValue("@EmailID", Mfirm.EmailID);
            cmd.Parameters.AddWithValue("@PhoneNo1", Mfirm.PhoneNo1);
            cmd.Parameters.AddWithValue("@PhoneNo2", Mfirm.PhoneNo2);
            cmd.Parameters.AddWithValue("@MobileNo1", Mfirm.MobileNo1);
            cmd.Parameters.AddWithValue("@MobileNo2", Mfirm.MobileNo2);
            cmd.Parameters.AddWithValue("@TermAndCondition", Mfirm.TermAndCondition);
            cmd.Parameters.AddWithValue("@GSTNo", Mfirm.GSTNo);
            cmd.Parameters.AddWithValue("@FSSAINo", Mfirm.FSSAINo);
            cmd.Parameters.AddWithValue("@PANNo", Mfirm.PANNo);
            cmd.Parameters.AddWithValue("@AdharNo", Mfirm.AdharNo);
            cmd.Parameters.AddWithValue("@AnyotherNo1", Mfirm.AnyotherNo1);
            cmd.Parameters.AddWithValue("@AnyotherNo2", Mfirm.AnyotherNo2);
            cmd.Parameters.AddWithValue("@GSTDate", Mfirm.GSTDate);
            cmd.Parameters.AddWithValue("@FSSAIDate", Mfirm.FSSAIDate);
            cmd.Parameters.AddWithValue("@IsType", Mfirm.IsType);
            cmd.Parameters.AddWithValue("@IsActive", Mfirm.IsActive);
            cmd.Parameters.AddWithValue("@UserID", Mfirm.UserID);
            cmd.Parameters.AddWithValue("@UserDate", Mfirm.UserDate);
            cmd.Parameters.AddWithValue("@PrinterName", Mfirm.PrinterName);
            cmd.Parameters.AddWithValue("@CompanyType", Mfirm.CompanyType);
            //---GST--//
            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }



        public bool AddMFirmBank(MFirmBank mFirmBank)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMFirmBank";

            cmd.Parameters.AddWithValue("@PkSrNo", mFirmBank.PkSrNo);

            cmd.Parameters.AddWithValue("@BankNo", mFirmBank.BankNo);

            cmd.Parameters.AddWithValue("@UserID", mFirmBank.UserID);

            cmd.Parameters.AddWithValue("@IsActive", mFirmBank.IsActive);

            cmd.Parameters.AddWithValue("@UserDate", mFirmBank.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMFirmBank(MFirmBank mFirmBank)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMManufacturerBank";

            cmd.Parameters.AddWithValue("@PkSrNo", mFirmBank.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMFirm(MFirm mFirm)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMFirm";

            cmd.Parameters.AddWithValue("@FirmNo", mFirm.FirmNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mFirm.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MFirm ModifyMFirmByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MFirm where FirmNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MFirm MM = new MFirm();
                while (dr.Read())
                {
                    MM.FirmNo = Convert.ToInt32(dr["FirmNo"]);
                    if (!Convert.IsDBNull(dr["FirmName"])) MM.FirmName = Convert.ToString(dr["FirmName"]);
                    if (!Convert.IsDBNull(dr["ShortName"])) MM.ShortName = Convert.ToString(dr["ShortName"]);
                    if (!Convert.IsDBNull(dr["EmailID"])) MM.EmailID = Convert.ToString(dr["EmailID"]);
                    if (!Convert.IsDBNull(dr["PrinterName"])) MM.PrinterName = Convert.ToString(dr["PrinterName"]);
                    if (!Convert.IsDBNull(dr["Address"])) MM.Address = Convert.ToString(dr["Address"]);
                    if (!Convert.IsDBNull(dr["PhoneNo1"])) MM.PhoneNo1 = Convert.ToString(dr["PhoneNo1"]);
                    if (!Convert.IsDBNull(dr["PhoneNo2"])) MM.PhoneNo2 = Convert.ToString(dr["PhoneNo2"]);
                    if (!Convert.IsDBNull(dr["MobileNo1"])) MM.MobileNo1 = Convert.ToString(dr["MobileNo1"]);
                    if (!Convert.IsDBNull(dr["MobileNo2"])) MM.MobileNo2 = Convert.ToString(dr["MobileNo2"]);
                    if (!Convert.IsDBNull(dr["StateNo"])) MM.StateNo = Convert.ToInt64(dr["StateNo"]);
                    if (!Convert.IsDBNull(dr["CityNo"])) MM.CityNo = Convert.ToInt64(dr["CityNo"]);
                    if (!Convert.IsDBNull(dr["Pincode"])) MM.Pincode = Convert.ToString(dr["Pincode"]);
                    if (!Convert.IsDBNull(dr["TermAndCondition"])) MM.TermAndCondition = Convert.ToString(dr["TermAndCondition"]);
                    if (!Convert.IsDBNull(dr["GSTNO"])) MM.GSTNo = Convert.ToString(dr["GSTNO"]);
                    if (!Convert.IsDBNull(dr["GSTDate"])) MM.GSTDate = Convert.ToDateTime(dr["GSTDate"]);
                    if (!Convert.IsDBNull(dr["FSSAIDate"])) MM.FSSAIDate = Convert.ToDateTime(dr["FSSAIDate"]);
                    if (!Convert.IsDBNull(dr["FSSAINo"])) MM.FSSAINo = Convert.ToString(dr["FSSAINo"]);
                    if (!Convert.IsDBNull(dr["PANCardNo"])) MM.PANNo = Convert.ToString(dr["PANCardNo"]);
                    if (!Convert.IsDBNull(dr["AdharCardNo"])) MM.AdharNo = Convert.ToString(dr["AdharCardNo"]);
                    if (!Convert.IsDBNull(dr["AnyotherNo1"])) MM.AnyotherNo1 = Convert.ToString(dr["AnyotherNo1"]);
                    if (!Convert.IsDBNull(dr["AnyotherNo2"])) MM.AnyotherNo2 = Convert.ToString(dr["AnyotherNo2"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["IsType"])) MM.IsType = Convert.ToBoolean(dr["IsType"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["StateCode"])) MM.StateCode = Convert.ToInt32(dr["StateCode"]);
                    if (!Convert.IsDBNull(dr["CompanyType"])) MM.CompanyType = Convert.ToInt32(dr["CompanyType"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MFirm();
        }

        public MFirmBank ModifyMFirmBankByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MFirmBank where FirmNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MFirmBank MM = new MFirmBank();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                 
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MFirmBank();
        }




        public DataView GetAllMManufacturerCompany()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MManufacturerCompany order by MfgCompNo";
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch { throw; }
            finally
            {
                Con.Close();
            }
            return ds.Tables[(0)].DefaultView;
        }

        public DataView GetMManufacturerCompanyByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MManufacturerCompany where MfgCompNo =" + ID;
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch { throw; }
            finally
            {
                Con.Close();
            }
            return ds.Tables[(0)].DefaultView;
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select FirmNo,FirmName AS 'FirmName',ShortName AS 'ShortName',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MFirm order by FirmName ";
                    break;
                case "FirmName":
                    sql = "Select FirmNo,FirmName AS 'FirmName',ShortName AS 'ShortName',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MFirm where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by FirmName";
                    break;
                case "ShortName":
                    sql = "Select FirmNo,FirmName AS 'FirmName',ShortName AS 'ShortName',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MFirm where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by FirmName";
                    break;
            }
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException e)
            {
                CommonFunctions.ErrorMessge = e.Message;
            }
            return ds.Tables[0].DefaultView;
        }

        public DataView GetBySearchSupplier(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.SundryCreditors + " And LedgerNo Not In(" + Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC)) + ") And IsActive='True' order by LedgerName";
                    break;
                case "1":
                    sql = "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.SundryCreditors + " And LedgerNo Not In(" + Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC)) + ") And IsActive='True' And   LedgerName like '" + Value.Trim().Replace("'", "''") + "' + '%' order by LedgerName";
                    break;
            }
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException e)
            {
                CommonFunctions.ErrorMessge = e.Message;
            }
            return ds.Tables[0].DefaultView;
        }

        public long ExecuteNonQueryStatementsMFG()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            int cntVchNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddMFirm")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddMFirmBank")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FirmNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }

                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                if (cntVchNo == -1)
                    return 0;
                else
                    return Convert.ToInt64(commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
            }
            catch (Exception e)
            {
                myTrans.Rollback();

                if (e.GetBaseException().Message == "")
                {
                    strerrormsg = e.Message;
                }
                else
                {
                    strerrormsg = e.GetBaseException().Message;
                }
                return 0;
            }
            finally
            {
                cn.Close();
            }
            //________________________________________________________________________________________________________________________________________________________________________________________________________________________
        }

        public bool ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;

                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                myTrans.Rollback();

                if (e.GetBaseException().Message == "")
                {
                    strerrormsg = e.Message;
                }
                else
                {
                    strerrormsg = e.GetBaseException().Message;
                }
                return false;
            }
            finally
            {
                cn.Close();
            }
            //________________________________________________________________________________________________________________________________________________________________________________________________________________________
        }


    }

    /// <summary>
    /// This Class use for MFirm
    /// </summary>
    public class MFirm
    {
        private long   mFirmNo;
        private string mFirmName;
        private string mShortName;
        private string mAddress;
        private long   mStateNo;
        private long   mCityNo;
        private string mPincode;
        private string mEmailID;
        private string mPhoneNo1;
        private string mPhoneNo2;
        private string mMobileNo1;
        private string mMobileNo2;
        private bool   mIsActive;
        private bool   mIsType;
        private string mPrinterName;
        private int    mStatusNo;
        private string mGSTNo;
        private string mFSSAINo;
        private string mPANNo;
        private string mAdharNo;
        private DateTime mGSTDate;
        private DateTime mFSSAIDate;
        private string mAnyotherNo1;
        private string mAnyotherNo2;
        private long mCompanyType;
        private string mTermAndCondition;
        private long   mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;
        private long   mStateCode;


        public long CompanyType
        {
            get { return mCompanyType; }
            set { mCompanyType = value; }
        }


        public long FirmNo
        {
            get { return mFirmNo; }
            set { mFirmNo = value; }
        }

        public string FirmName
        {
            get { return mFirmName; }
            set { mFirmName = value; }
        }

        public string ShortName
        {
            get { return mShortName; }
            set { mShortName = value; }
        }
        public string EmailID
        {
            get { return mEmailID; }
            set { mEmailID = value; }
        }
        public string Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }

        public string PhoneNo1
        {
            get { return mPhoneNo1; }
            set { mPhoneNo1 = value; }
        }
        public string PhoneNo2
        {
            get { return mPhoneNo2; }
            set { mPhoneNo2 = value; }
        }

        public string MobileNo1
        {
            get { return mMobileNo1; }
            set { mMobileNo1 = value; }
        }
        public string MobileNo2
        {
            get { return mMobileNo2; }
            set { mMobileNo2 = value; }
        }
        public long StateNo
        {
            get { return mStateNo; }
            set { mStateNo = value; }
        }
        public long CityNo
        {
            get { return mCityNo; }
            set { mCityNo = value; }
        }

        public string Pincode
        {
            get { return mPincode; }
            set { mPincode = value; }
        }


        public string PrinterName
        {
            get { return mPrinterName; }
            set { mPrinterName = value; }
        }
   

        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }

        public bool IsType
        {
            get { return mIsType; }
            set { mIsType = value; }
        }
      
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }

        public string GSTNo
        {
            get { return mGSTNo; }
            set { mGSTNo = value; }
        }

        public string FSSAINo
        {
            get { return mFSSAINo; }
            set { mFSSAINo = value; }
        }

        public string PANNo
        {
            get { return mPANNo; }
            set { mPANNo = value; }
        }
        public string AdharNo
        {
            get { return mAdharNo; }
            set { mAdharNo = value; }
        }

        public string AnyotherNo1
        {
            get { return mAnyotherNo1; }
            set { mAnyotherNo1 = value; }
        }
        public string AnyotherNo2
        {
            get { return mAnyotherNo2; }
            set { mAnyotherNo2 = value; }
        }
        public string TermAndCondition
        {
            get { return mTermAndCondition; }
            set { mTermAndCondition = value; }
        }

   
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
    
        public DateTime UserDate
        {
            get { return mUserDate; }
            set { mUserDate = value; }
        }
      
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
        }
      
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
        public DateTime GSTDate
        {
            get { return mGSTDate; }
            set { mGSTDate = value; }
        }
        public DateTime FSSAIDate
        {
            get { return mFSSAIDate; }
            set { mFSSAIDate = value; }
        }
    }

    /// <summary>
    /// This Class use for MManufacturerDetails
    /// </summary>
    public class MFirmDetails
    {
        private long mPkSrNo;
        private long mLedgerNo;
        private long mManufacturerNo;
        private long mCompanyNo;
        private long mStatusNo;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkSrNo
        /// </summary>
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        /// <summary>
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for ManufacturerNo
        /// </summary>
        public long ManufacturerNo
        {
            get { return mManufacturerNo; }
            set { mManufacturerNo = value; }
        }
        /// <summary>
        /// This Properties use for CompanyNo
        /// </summary>
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        /// <summary>
        /// This Properties use for StatusNo
        /// </summary>
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        /// <summary>
        /// This Properties use for UserID
        /// </summary>
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        /// <summary>
        /// This Properties use for UserDate
        /// </summary>
        public DateTime UserDate
        {
            get { return mUserDate; }
            set { mUserDate = value; }
        }
        /// <summary>
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class MFirmBank
    {
        private long mPkSrNo;
        private long mFirmNo;
        private long mBankNo;
        private long mUserID;
        private DateTime mUserDate;
        private long mStatusNo;
        private string Mmsg;
        private bool mIsActive;

        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long FirmNo
        {
            get { return mFirmNo; }
            set { mFirmNo = value; }
        }
        public long BankNo
        {
            get { return mBankNo; }
            set { mBankNo = value; }
        }
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        public DateTime UserDate
        {
            get { return mUserDate; }
            set { mUserDate = value; }
        }
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
