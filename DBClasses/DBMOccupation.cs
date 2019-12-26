using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using OM;
using OMControls;

namespace OM
{
    class DBMOccupation
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMOccupation(MOccupation moccupation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMOccupation";

            cmd.Parameters.AddWithValue("@OccupationNo", moccupation.OccupationNo);

            cmd.Parameters.AddWithValue("@OccupationName", moccupation.OccupationName);

            cmd.Parameters.AddWithValue("@ShortName", moccupation.ShortName);

            cmd.Parameters.AddWithValue("@IsActive", moccupation.IsActive);

            cmd.Parameters.AddWithValue("@UserID", moccupation.UserID);

            cmd.Parameters.AddWithValue("@UserDate", moccupation.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", moccupation.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                moccupation.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMOccupation(MOccupation moccupation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMOccupation";

            cmd.Parameters.AddWithValue("@OccupationNo", moccupation.OccupationNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                moccupation.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMOccupation()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MOccupation order by OccupationName";
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

        public MOccupation ModifyMOccupationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MOccupation where OccupationNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MOccupation MM = new MOccupation();
                while (dr.Read())
                {
                    MM.OccupationNo = Convert.ToInt32(dr["OccupationNo"]);
                    if (!Convert.IsDBNull(dr["OccupationName"])) MM.OccupationName = Convert.ToString(dr["OccupationName"]);
                    if (!Convert.IsDBNull(dr["ShortName"])) MM.ShortName = Convert.ToString(dr["ShortName"]);
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
            return new MOccupation();
        }

        public DataView GetMOccupationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MOccupation where OccupationNo =" + ID;
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
                    sql = "Select OccupationNo,ShortName ,OccupationName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MOccupation order by OccupationName ";
                    break;
                case "OccupationName":
                    sql = "Select OccupationNo,ShortName,OccupationName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MOccupation where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by OccupationName";
                    break;
                case "ShortName":
                    sql = "Select OccupationNo,ShortName AS 'Short Name',OccupationName AS 'Occupation Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MOccupation where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' Order By ShortName ";
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
    /// This Class use for MOccupation
    /// </summary>
    public class MOccupation
    {
        private long mOccupationNo;
        private string mOccupationName;
        private string mShortName;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for OccupationNo
        /// </summary>
        public long OccupationNo
        {
            get { return mOccupationNo; }
            set { mOccupationNo = value; }
        }
        /// <summary>
        /// This Properties use for OccupationName
        /// </summary>
        public string OccupationName
        {
            get { return mOccupationName; }
            set { mOccupationName = value; }
        }
        /// <summary>
        /// This Properties use for ShortName
        /// </summary>
        public string ShortName
        {
            get { return mShortName; }
            set { mShortName = value; }
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
