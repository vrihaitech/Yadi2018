using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMRackDetails
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMRackDetails(MRackDetails MRackDetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRackDetails";

            cmd.Parameters.AddWithValue("@RackDetailsNo", MRackDetails.RackDetailsNo);

            cmd.Parameters.AddWithValue("@UOMLNo", MRackDetails.UOMLNo);

            cmd.Parameters.AddWithValue("@FkRackNo", MRackDetails.FkRackNo);

            cmd.Parameters.AddWithValue("@ItemNo", MRackDetails.ItemNo); 

            cmd.Parameters.AddWithValue("@ToQty", MRackDetails.ToQty);
            
            cmd.Parameters.AddWithValue("@IsActive", MRackDetails.IsActive);

            cmd.Parameters.AddWithValue("@UserID", MRackDetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", MRackDetails.UserDate);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                MRackDetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMRackDetails(MRackDetails MRackDetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMRackDetails";

            cmd.Parameters.AddWithValue("@RackDetailsNo", MRackDetails.RackDetailsNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                MRackDetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMRackDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MRackDetails order by RackDetailsNo";
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

        public MRackDetails ModifyMRackDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MRackDetails where RackDetailsNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MRackDetails MM = new MRackDetails();
                while (dr.Read())
                {
                    MM.RackDetailsNo = Convert.ToInt32(dr["RackDetailsNo"]);
                    if (!Convert.IsDBNull(dr["UOMLNo"])) MM.UOMLNo = Convert.ToInt64(dr["UOMLNo"]);
                    if (!Convert.IsDBNull(dr["FkRackNo"])) MM.FkRackNo = Convert.ToInt64(dr["FkRackNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);                
                    if (!Convert.IsDBNull(dr["ToQty"])) MM.ToQty = Convert.ToDouble(dr["ToQty"]); 
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MRackDetails();
        }

        public DataView GetMRackDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MRackDetails where RackDetailsNo =" + ID;
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
                    sql = "Select RackDetailsNo,UOMLNo AS 'Rack Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRackDetails ";
                    break;
                case "UOMLNo":
                    sql = "Select RackDetailsNo,UOMLNo AS 'Rack Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRackDetails where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
                    break;
                case "FkRackNo":
                    sql = "Select RackDetailsNo,UOMLNo AS 'Rack Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRackDetails where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
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


    public class MRackDetails
    {
        private long mRackDetailsNo;
        private long mUOMLNo;
        private long mFkRackNo;
        private long mItemNo;
        private double mToQty;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        public long RackDetailsNo
        {
            get { return mRackDetailsNo; }
            set { mRackDetailsNo = value; }
        }
        public long UOMLNo
        {
            get { return mUOMLNo; }
            set { mUOMLNo = value; }
        }       
        public long FkRackNo
        {
            get { return mFkRackNo; }
            set { mFkRackNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public double ToQty
        {
            get { return mToQty; }
            set { mToQty = value; }
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
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

}
