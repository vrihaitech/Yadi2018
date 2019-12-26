using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMState
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();


        public bool AddMState(MState mstate)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMState";

            cmd.Parameters.AddWithValue("@StateNo", mstate.StateNo);

            cmd.Parameters.AddWithValue("@StateName", mstate.StateName);

            cmd.Parameters.AddWithValue("@StateShortCode", mstate.StateShortCode);

            cmd.Parameters.AddWithValue("@StateLangName", mstate.StateLangName);

            cmd.Parameters.AddWithValue("@CountryNo", mstate.CountryNo);

           

            cmd.Parameters.AddWithValue("@IsActive", mstate.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mstate.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mstate.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mstate.CompanyNo);
            cmd.Parameters.AddWithValue("@StateCode", mstate.StateCode); 

            //cmd.Parameters.AddWithValue("@ModifiedBy", mstate.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstate.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMState(MState mstate)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMState";

            cmd.Parameters.AddWithValue("@StateNo", mstate.StateNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstate.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }


        public DataView GetAllMState()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MState order by StateNo";
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
        public DataView GetMStateByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MState where StateNo =" + ID;
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

        public MState ModifyMStateByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MState where StateNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MState MM = new MState();
                while (dr.Read())
                {
                    MM.StateNo = Convert.ToInt32(dr["StateNo"]);
                    if (!Convert.IsDBNull(dr["StateName"])) MM.StateName = Convert.ToString(dr["StateName"]);
                    if (!Convert.IsDBNull(dr["StateShortCode"])) MM.StateShortCode = Convert.ToString(dr["StateShortCode"]);
                    if (!Convert.IsDBNull(dr["StateLangName"])) MM.StateLangName = Convert.ToString(dr["StateLangName"]);
                    if (!Convert.IsDBNull(dr["CountryNo"])) MM.CountryNo = Convert.ToInt64(dr["CountryNo"]);
                    
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StateCode"])) MM.StateCode = Convert.ToString(dr["StateCode"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MState();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select StateNo,StateShortCode AS 'Short Name',StateName AS 'State Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MState order by StateName";
                    break;
                case "StateName":
                    sql = "Select StateNo,StateShortCode AS 'Short Name',StateName AS 'State Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MState where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by StateName";
                    break;
                case "StateShortCode":
                    sql = "Select StateNo,StateShortCode AS 'Short Name',StateName AS 'State Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MState where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by StateName";
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
    /// This Class use for MState
    /// </summary>
    public class MState
    {
        private long mStateNo;
        private string mStateName;
        private string mStateShortCode;
        private string mStateLangName;
        private long mCountryNo;
        private long mRegionNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;
        private string mStateCode;

        /// <summary>
        /// This Properties use for StateNo
        /// </summary>
        public long StateNo
        {
            get { return mStateNo; }
            set { mStateNo = value; }
        }
        /// <summary>
        /// This Properties use for StateName
        /// </summary>
        public string StateName
        {
            get { return mStateName; }
            set { mStateName = value; }
        }
        /// <summary>
        /// This Properties use for StateShortCode
        /// </summary>
        public string StateShortCode
        {
            get { return mStateShortCode; }
            set { mStateShortCode = value; }
        }
         /// <summary>
        /// This Properties use for StateLangName
        /// </summary>
        public string StateLangName
        {
            get { return mStateLangName; }
            set { mStateLangName = value; }
        }
        /// <summary>
        /// This Properties use for CountryNo
        /// </summary>
        public long CountryNo
        {
            get { return mCountryNo; }
            set { mCountryNo = value; }
        }
        /// <summary>
        /// This Properties use for RegionNo
        /// </summary>
        public long RegionNo
        {
            get { return mRegionNo; }
            set { mRegionNo = value; }
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
        public string  StateCode
        {
            get { return mStateCode; }
            set { mStateCode = value; }
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
