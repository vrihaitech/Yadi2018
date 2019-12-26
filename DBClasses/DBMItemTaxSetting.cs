using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMItemTaxSetting
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMItemTaxSetting(MItemTaxSetting mitemtaxsetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemTaxSetting";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxsetting.PkSrNo);

            cmd.Parameters.AddWithValue("@TaxSettingName", mitemtaxsetting.TaxSettingName);

            cmd.Parameters.AddWithValue("@TaxLedgerNo", mitemtaxsetting.TaxLedgerNo);

            cmd.Parameters.AddWithValue("@SalesLedgerNo", mitemtaxsetting.SalesLedgerNo);

            cmd.Parameters.AddWithValue("@CalculationMethod", mitemtaxsetting.CalculationMethod);

            cmd.Parameters.AddWithValue("@Percentage", mitemtaxsetting.Percentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemtaxsetting.CompanyNo);

            cmd.Parameters.AddWithValue("@IsActive", mitemtaxsetting.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mitemtaxsetting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mitemtaxsetting.UserDate);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mitemtaxsetting.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMItemTaxSetting(MItemTaxSetting mitemtaxsetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMItemTaxSetting";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxsetting.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mitemtaxsetting.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMItemTaxSetting()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemTaxSetting order by PkSrNo";
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

        public DataView GetMItemTaxSettingByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemTaxSetting where PkSrNo =" + ID;
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

        public MItemTaxSetting ModifyMItemTaxSettingByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MItemTaxSetting where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MItemTaxSetting MM = new MItemTaxSetting();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["TaxSettingName"])) MM.TaxSettingName = Convert.ToString(dr["TaxSettingName"]);
                    if (!Convert.IsDBNull(dr["TaxLedgerNo"])) MM.TaxLedgerNo = Convert.ToInt64(dr["TaxLedgerNo"]);
                    if (!Convert.IsDBNull(dr["SalesLedgerNo"])) MM.SalesLedgerNo = Convert.ToInt64(dr["SalesLedgerNo"]);
                    //if (!Convert.IsDBNull(dr["CalculationMethod"])) MM.CalculationMethod = Convert.ToString(dr["CalculationMethod"]);
                    if (!Convert.IsDBNull(dr["Percentage"])) MM.Percentage = Convert.ToDouble(dr["Percentage"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MItemTaxSetting();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select PkSrNo,TaxSettingName AS 'Tax Setting Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MItemTaxSetting order by TaxSettingName";
                    break;
                case "TaxSettingName":
                    sql = "Select PkSrNo,TaxSettingName AS 'Tax Setting Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MItemTaxSetting where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by TaxSettingName";
                    break;
                //case "StateShortCode":
                //    sql = "Select StateNo,StateShortCode AS 'Short Name',StateName AS 'State Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MState where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by StateName";
                //    break;
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

    }

    /// <summary>
    /// This Class use for MItemTaxSetting
    /// </summary>
    public class MItemTaxSetting
    {
        private long mPkSrNo;
        private string mTaxSettingName;
        private long mTaxLedgerNo;
        private long mSalesLedgerNo;
        private string mCalculationMethod;
        private double mPercentage;
        private long mCompanyNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
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
        /// This Properties use for TaxSettingName
        /// </summary>
        public string TaxSettingName
        {
            get { return mTaxSettingName; }
            set { mTaxSettingName = value; }
        }
        /// <summary>
        /// This Properties use for TaxLedgerNo
        /// </summary>
        public long TaxLedgerNo
        {
            get { return mTaxLedgerNo; }
            set { mTaxLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for SalesLedgerNo
        /// </summary>
        public long SalesLedgerNo
        {
            get { return mSalesLedgerNo; }
            set { mSalesLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for CalculationMethod
        /// </summary>
        public string CalculationMethod
        {
            get { return mCalculationMethod; }
            set { mCalculationMethod = value; }
        }
        /// <summary>
        /// This Properties use for Percentage
        /// </summary>
        public double Percentage
        {
            get { return mPercentage; }
            set { mPercentage = value; }
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
        /// This Properties use for IsActive
        /// </summary>
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
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
        /// This Properties use for ModifiedBy
        /// </summary>
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
        }
        /// <summary>
        /// This Properties use for StatusNo
        /// </summary>
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
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
}
