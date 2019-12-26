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
    class DBMGodown
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();


        public bool AddMGodown(MGodown mgodown)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMGodown";

            cmd.Parameters.AddWithValue("@GodownNo", mgodown.GodownNo);

            cmd.Parameters.AddWithValue("@GodownName", mgodown.GodownName);

            cmd.Parameters.AddWithValue("@ControlGroup", mgodown.ControlGroup);

            cmd.Parameters.AddWithValue("@LocationNo", mgodown.LocationNo);

            cmd.Parameters.AddWithValue("@IsActive", mgodown.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mgodown.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mgodown.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mgodown.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mgodown.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMGodown(MGodown mgodown)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMGodown";

            cmd.Parameters.AddWithValue("@GodownNo", mgodown.GodownNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mgodown.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMGodown()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MGodown order by GodownNo";
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

        public DataView GetMGodownByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MGodown where GodownNo =" + ID;
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

        public MGodown ModifyMGodownByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MGodown where GodownNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MGodown MM = new MGodown();
                while (dr.Read())
                {
                    MM.GodownNo = Convert.ToInt32(dr["GodownNo"]);
                    if (!Convert.IsDBNull(dr["GodownName"])) MM.GodownName = Convert.ToString(dr["GodownName"]);
                    if (!Convert.IsDBNull(dr["ControlGroup"])) MM.ControlGroup = Convert.ToInt64(dr["ControlGroup"]);
                    if (!Convert.IsDBNull(dr["LocationNo"])) MM.LocationNo = Convert.ToInt64(dr["LocationNo"]);
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
            return new MGodown();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select GodownNo,GodownName AS 'Godown Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MGodown Where GodownNo <> 1 order by GodownName";
                    break;
                case "GodownName":
                    sql = "Select GodownNo,GodownName AS 'Godown Name', Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MGodown where (GodownNo <> 1) And " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by GodownName ";
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
    /// This Class use for MGodown
    /// </summary>
    public class MGodown
    {
        private long mGodownNo;
        private string mGodownName;
        private long mControlGroup;
        private long mLocationNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for GodownNo
        /// </summary>
        public long GodownNo
        {
            get { return mGodownNo; }
            set { mGodownNo = value; }
        }
        /// <summary>
        /// This Properties use for GodownName
        /// </summary>
        public string GodownName
        {
            get { return mGodownName; }
            set { mGodownName = value; }
        }
        /// <summary>
        /// This Properties use for ControlGroup
        /// </summary>
        public long ControlGroup
        {
            get { return mControlGroup; }
            set { mControlGroup = value; }
        }
        /// <summary>
        /// This Properties use for LocationNo
        /// </summary>
        public long LocationNo
        {
            get { return mLocationNo; }
            set { mLocationNo = value; }
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
