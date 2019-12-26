using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMOtherBank
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMBank(MOtherBank mbank)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMOtherBank";

            cmd.Parameters.AddWithValue("@BankNo", mbank.BankNo);

            cmd.Parameters.AddWithValue("@BankName", mbank.BankName);

            cmd.Parameters.AddWithValue("@IsActive", mbank.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mbank.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mbank.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mbank.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mbank.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMBank(MOtherBank mbank)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMOtherBank";

            cmd.Parameters.AddWithValue("@BankNo", mbank.BankNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mbank.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMBank()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MOtherBank order by BankNo";
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

        public DataView GetMBankByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MOtherBank where BankNo =" + ID;
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


        public MOtherBank ModifyMBankByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MOtherBank where BankNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MOtherBank MM = new MOtherBank();
                while (dr.Read())
                {
                    MM.BankNo = Convert.ToInt32(dr["BankNo"]);
                    if (!Convert.IsDBNull(dr["BankName"])) MM.BankName = Convert.ToString(dr["BankName"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MOtherBank();
        }
        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    //sql = "Select BranchNo,BranchShortCode AS 'Short Name',BranchName AS 'Branch Name' from MBranch ";
                    sql = "Select BankNo,BankName AS 'Bank Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MOtherBank ";
                    break;
                case "BankName":
                    //sql = "Select BranchNo,BranchShortCode AS 'Short Name',BranchName AS 'Branch Name' from MBranch where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by BranchName";
                    sql = "Select BankNo,BankName AS 'Bank Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MOtherBank where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
                    break;
                //case "BranchShortCode":
                //    sql = "Select BranchNo,BranchShortCode AS 'Short Name',BranchName AS 'Branch Name' from MBranch where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by BranchName";
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
    /// This Class use for MOtherBank
    /// </summary>
    public class MOtherBank
    {
        private long mBankNo;
        private string mBankName;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for BankNo
        /// </summary>
        public long BankNo
        {
            get { return mBankNo; }
            set { mBankNo = value; }
        }
        /// <summary>
        /// This Properties use for BankName
        /// </summary>
        public string BankName
        {
            get { return mBankName; }
            set { mBankName = value; }
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
