using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMCashDenomination
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMCashDenomination(MCashDenomination MCashDenomination)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMCashDenomination";

            cmd.Parameters.AddWithValue("@CashDenominationNo", MCashDenomination.CashDenominationNo);

            cmd.Parameters.AddWithValue("@Note", MCashDenomination.Note);
            
            cmd.Parameters.AddWithValue("@RSType", MCashDenomination.RSType);

            cmd.Parameters.AddWithValue("@SerialNo", MCashDenomination.SerialNo);
           
            cmd.Parameters.AddWithValue("@IsActive", MCashDenomination.IsActive);

            cmd.Parameters.AddWithValue("@UserID", MCashDenomination.UserID);

            cmd.Parameters.AddWithValue("@UserDate", MCashDenomination.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo",MCashDenomination.CompanyNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                MCashDenomination.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMCashDenomination(MCashDenomination MCashDenomination)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMCashDenomination";

            cmd.Parameters.AddWithValue("@CashDenominationNo", MCashDenomination.CashDenominationNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                MCashDenomination.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMCashDenomination()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCashDenomination order by CashDenominationNo";
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

        public MCashDenomination ModifyMCashDenominationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MCashDenomination where CashDenominationNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MCashDenomination MM = new MCashDenomination();
                while (dr.Read())
                {
                    MM.CashDenominationNo = Convert.ToInt32(dr["CashDenominationNo"]);
                    if (!Convert.IsDBNull(dr["Note"])) MM.Note = Convert.ToString(dr["Note"]);
                    if (!Convert.IsDBNull(dr["SerialNo"])) MM.SerialNo = Convert.ToInt64(dr["SerialNo"]);
                    if (!Convert.IsDBNull(dr["RSType"])) MM.RSType = Convert.ToInt64(dr["RSType"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MCashDenomination();
        }

        public DataView GetMCashDenominationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCashDenomination where CashDenominationNo =" + ID;
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
                    sql = "Select CashDenominationNo,Note AS 'Note',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCashDenomination ";
                    break;
                case "Note":
                    sql = "Select CashDenominationNo,Note AS 'Note',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCashDenomination where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
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


    public class MCashDenomination
    {
        private long mCashDenominationNo;
        private string mNote;
        private long mRSType;
        private long mSerialNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private string Mmsg;
  
        public long CashDenominationNo
        {
            get { return mCashDenominationNo; }
            set { mCashDenominationNo = value; }
        }
        public string Note
        {
            get { return mNote; }
            set { mNote = value; }
        }
        public long RSType
        {
            get { return mRSType; }
            set { mRSType = value; }
        }
        public long SerialNo
        {
            get { return mSerialNo; }
            set { mSerialNo = value; }
        }
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
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
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

}
