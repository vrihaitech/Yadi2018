using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMArea
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMArea(MArea marea)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMArea";

            cmd.Parameters.AddWithValue("@AreaNo", marea.AreaNo);

            cmd.Parameters.AddWithValue("@AreaName", marea.AreaName);

            cmd.Parameters.AddWithValue("@AreaLangName", marea.AreaLangName);

            cmd.Parameters.AddWithValue("@AreaShortCode", marea.AreaShortCode);

          //  cmd.Parameters.AddWithValue("@CountryNo", marea.CountryNo);

          // cmd.Parameters.AddWithValue("@StateNo", marea.StateNo);

            //cmd.Parameters.AddWithValue("@CityNo", marea.CityNo);

          //  cmd.Parameters.AddWithValue("@RegionNo", marea.RegionNo);

            cmd.Parameters.AddWithValue("@IsActive", marea.IsActive);

            cmd.Parameters.AddWithValue("@UserID", marea.UserID);

            cmd.Parameters.AddWithValue("@UserDate", marea.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo",marea.CompanyNo);

            //cmd.Parameters.AddWithValue("@ModifiedBy", marea.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                marea.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMArea(MArea marea)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMArea";

            cmd.Parameters.AddWithValue("@AreaNo", marea.AreaNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                marea.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMArea()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MArea order by AreaNo";
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

        public MArea ModifyMAreaByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MArea where AreaNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MArea MM = new MArea();
                while (dr.Read())
                {
                    MM.AreaNo = Convert.ToInt32(dr["AreaNo"]);
                    if (!Convert.IsDBNull(dr["AreaName"])) MM.AreaName = Convert.ToString(dr["AreaName"]);
                    if (!Convert.IsDBNull(dr["AreaLangName"])) MM.AreaLangName = Convert.ToString(dr["AreaLangName"]);
                    if (!Convert.IsDBNull(dr["AreaShortCode"])) MM.AreaShortCode = Convert.ToString(dr["AreaShortCode"]);
                   // if (!Convert.IsDBNull(dr["CountryNo"])) MM.CountryNo = Convert.ToInt64(dr["CountryNo"]);
                   // if (!Convert.IsDBNull(dr["StateNo"])) MM.StateNo = Convert.ToInt64(dr["StateNo"]);

                  //  if (!Convert.IsDBNull(dr["CityNo"])) MM.CityNo = Convert.ToInt64(dr["CityNo"]);
                  //  if (!Convert.IsDBNull(dr["RegionNo"])) MM.RegionNo = Convert.ToInt64(dr["RegionNo"]);
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
            return new MArea();
        }

        public DataView GetMAreaByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MArea where AreaNo =" + ID;
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
                    sql = "Select AreaNo,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MArea ";
                    break;
                case "AreaName":
                    sql = "Select AreaNo,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MArea where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
                    break;
                case "AreaShortCode":
                    sql = "Select AreaNo,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MArea where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'";
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
    /// This Class use for MArea
    /// </summary>
    public class MArea
    {
        private long mAreaNo;
        private string mAreaName;
        private string mAreaLangName;
        private string mAreaShortCode;
        private long mCountryNo;
        private long mStateNo;
        private long mRegionNo;
        private long mCityNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for AreaNo
        /// </summary>
        public long AreaNo
        {
            get { return mAreaNo; }
            set { mAreaNo = value; }
        }
        /// <summary>
        /// This Properties use for AreaName
        /// </summary>
        public string AreaName
        {
            get { return mAreaName; }
            set { mAreaName = value; }
        }
        /// <summary>
        /// This Properties use for AreaLangName
        /// </summary>
        public string AreaLangName
        {
            get { return mAreaLangName; }
            set { mAreaLangName = value; }
        }
        /// <summary>
        /// This Properties use for AreaShortCode
        /// </summary>
        public string AreaShortCode
        {
            get { return mAreaShortCode; }
            set { mAreaShortCode = value; }
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
        /// This Properties use for StateNo
        /// </summary>
        public long StateNo
        {
            get { return mStateNo; }
            set { mStateNo = value; }
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
        /// This Properties use for CityNo
        /// </summary>
        public long CityNo
        {
            get { return mCityNo; }
            set { mCityNo = value; }
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
