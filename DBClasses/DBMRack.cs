using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMRack
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMRack(MRack mRack)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRack";

            cmd.Parameters.AddWithValue("@RackNo", mRack.RackNo);

            cmd.Parameters.AddWithValue("@RackName", mRack.RackName);

            cmd.Parameters.AddWithValue("@RackCode", mRack.RackCode);

            cmd.Parameters.AddWithValue("@IsActive", mRack.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mRack.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mRack.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mRack.CompanyNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mRack.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMRack(MRack mRack)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMRack";

            cmd.Parameters.AddWithValue("@RackNo", mRack.RackNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mRack.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMRack()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MRack order by RackNo";
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

        public MRack ModifyMRackByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MRack where RackNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MRack MM = new MRack();
                while (dr.Read())
                {
                    MM.RackNo = Convert.ToInt32(dr["RackNo"]);
                    if (!Convert.IsDBNull(dr["RackName"])) MM.RackName = Convert.ToString(dr["RackName"]);
                    if (!Convert.IsDBNull(dr["RackCode"])) MM.RackCode = Convert.ToString(dr["RackCode"]);
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
            return new MRack();
        }

        public DataView GetMRackByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MRack where RackNo =" + ID;
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
                    sql = "Select RackNo,RackName AS 'Rack Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRack ";
                    break;
                case "RackName":
                    sql = "Select RackNo,RackName AS 'Rack Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRack where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
                    break;
                case "RackCode":
                    sql = "Select RackNo,RackName AS 'Rack Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRack where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
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


    public class MRack
    {
        private long mRackNo;
        private string mRackName;
        private string mRackCode;   
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private string Mmsg;

        public long RackNo
        {
            get { return mRackNo; }
            set { mRackNo = value; }
        }
        public string RackName
        {
            get { return mRackName; }
            set { mRackName = value; }
        }       
        public string RackCode
        {
            get { return mRackCode; }
            set { mRackCode = value; }
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
