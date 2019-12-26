using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBMBranch
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMBranch(MBranch mbranch)
        { 
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMBranch";

            cmd.Parameters.AddWithValue("@BranchNo", mbranch.BranchNo);

            cmd.Parameters.AddWithValue("@BranchName", mbranch.BranchName);

            cmd.Parameters.AddWithValue("@IsActive", mbranch.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mbranch.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mbranch.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mbranch.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mbranch.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMBranch(MBranch mbranch)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMBranch";

            cmd.Parameters.AddWithValue("@BranchNo", mbranch.BranchNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mbranch.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMBranch()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MBranch order by BranchNo";
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

        public DataView GetMBranchByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MBranch where BranchNo =" + ID;
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

        public MBranch ModifyMBranchByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MBranch where BranchNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MBranch MM = new MBranch();
                while (dr.Read())
                {
                    MM.BranchNo = Convert.ToInt32(dr["BranchNo"]);
                    if (!Convert.IsDBNull(dr["BranchName"])) MM.BranchName = Convert.ToString(dr["BranchName"]);
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
            return new MBranch();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select BranchNo,BranchName AS 'Branch Name',Case when (MBranch.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MBranch ";
                    break;
                case "BranchName":
                    sql = "Select BranchNo,BranchName AS 'Branch Name',Case when (MBranch.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MBranch where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'order by BranchName";
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

    }

    /// <summary>
    /// This Class use for MBranch
    /// </summary>
    public class MBranch
    {
        private long mBranchNo;
        private string mBranchName;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for BranchNo
        /// </summary>
        public long BranchNo
        {
            get { return mBranchNo; }
            set { mBranchNo = value; }
        }
        /// <summary>
        /// This Properties use for BranchName
        /// </summary>
        public string BranchName
        {
            get { return mBranchName; }
            set { mBranchName = value; }
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
