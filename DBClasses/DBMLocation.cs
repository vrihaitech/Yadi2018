using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMLocation
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMLocation(MLocation mlocation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLocation";

            cmd.Parameters.AddWithValue("@LocationNo", mlocation.LocationNo);

            cmd.Parameters.AddWithValue("@LocationName", mlocation.LocationName);

            cmd.Parameters.AddWithValue("@LocationShortCode", mlocation.LocationShortCode);

            cmd.Parameters.AddWithValue("@CountryNo", mlocation.CountryNo);

            cmd.Parameters.AddWithValue("@StateNo", mlocation.StateNo);

            cmd.Parameters.AddWithValue("@CityNo", mlocation.CityNo);

            cmd.Parameters.AddWithValue("@RegionNo", mlocation.RegionNo);

            cmd.Parameters.AddWithValue("@ControlGroup", mlocation.ControlGroup);

            cmd.Parameters.AddWithValue("@IsActive", mlocation.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mlocation.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mlocation.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mlocation.CompanyNo);

           // cmd.Parameters.AddWithValue("@ModifiedBy", mlocation.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mlocation.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMLocation(MLocation mlocation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMLocation";

            cmd.Parameters.AddWithValue("@LocationNo", mlocation.LocationNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mlocation.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMLocation()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLocation order by LocationNo";
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

        public MLocation ModifyMLocationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MLocation where LocationNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MLocation MM = new MLocation();
                while (dr.Read())
                {
                    MM.LocationNo = Convert.ToInt32(dr["LocationNo"]);
                    if (!Convert.IsDBNull(dr["LocationName"])) MM.LocationName = Convert.ToString(dr["LocationName"]);
                    if (!Convert.IsDBNull(dr["LocationShortCode"])) MM.LocationShortCode = Convert.ToString(dr["LocationShortCode"]);
                    if (!Convert.IsDBNull(dr["CountryNo"])) MM.CountryNo = Convert.ToInt64(dr["CountryNo"]);
                    if (!Convert.IsDBNull(dr["StateNo"])) MM.StateNo = Convert.ToInt64(dr["StateNo"]);
                    if (!Convert.IsDBNull(dr["CityNo"])) MM.CityNo = Convert.ToInt64(dr["CityNo"]);
                    if (!Convert.IsDBNull(dr["RegionNo"])) MM.RegionNo = Convert.ToInt64(dr["RegionNo"]);
                    if (!Convert.IsDBNull(dr["ControlGroup"])) MM.ControlGroup = Convert.ToInt64(dr["ControlGroup"]);
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
            return new MLocation();
        }

        public DataView GetMLocationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLocation where LocationNo =" + ID;
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
                    sql = "Select LocationNo,LocationShortCode AS 'Short Name',LocationName AS 'Location Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLocation Where (LocationNo <> 1)  ";
                    break;
                case "LocationName":
                    sql = "Select LocationNo,LocationShortCode AS 'Short Name',LocationName AS 'Location Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLocation where (LocationNo <> 1) And " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by LocationName";
                    break;
                case "LocationShortCode":
                    sql = "Select LocationNo,LocationShortCode AS 'Short Name',LocationName AS 'Location Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLocation where (LocationNo <> 1) And " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by LocationName";
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
    /// This Class use for MLocation
    /// </summary>
    public class MLocation
    {
        private long mLocationNo;
        private string mLocationName;
        private string mLocationShortCode;
        private long mCountryNo;
        private long mStateNo;
        private long mCityNo;
        private long mRegionNo;
        private long mControlGroup;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for LocationNo
        /// </summary>
        public long LocationNo
        {
            get { return mLocationNo; }
            set { mLocationNo = value; }
        }
        /// <summary>
        /// This Properties use for LocationName
        /// </summary>
        public string LocationName
        {
            get { return mLocationName; }
            set { mLocationName = value; }
        }
        /// <summary>
        /// This Properties use for LocationShortCode
        /// </summary>
        public string LocationShortCode
        {
            get { return mLocationShortCode; }
            set { mLocationShortCode = value; }
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
        /// This Properties use for CityNo
        /// </summary>
        public long CityNo
        {
            get { return mCityNo; }
            set { mCityNo = value; }
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
        /// This Properties use for ControlGroup
        /// </summary>
        public long ControlGroup
        {
            get { return mControlGroup; }
            set { mControlGroup = value; }
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
