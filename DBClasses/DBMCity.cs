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
    class DBMCity
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMCity(MCity mcity)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMCity";

            cmd.Parameters.AddWithValue("@CityNo", mcity.CityNo);

            cmd.Parameters.AddWithValue("@CityName", mcity.CityName);

            cmd.Parameters.AddWithValue("@CityShortCode", mcity.CityShortCode);

            cmd.Parameters.AddWithValue("@CityLangName", mcity.CityLangName);

            //cmd.Parameters.AddWithValue("@CountryNo", mcity.CountryNo);

            cmd.Parameters.AddWithValue("@StateNo", mcity.StateNo);

           // cmd.Parameters.AddWithValue("@RegionNo", mcity.RegionNo);

            cmd.Parameters.AddWithValue("@IsActive", mcity.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mcity.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mcity.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mcity.CompanyNo);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mcity.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcity.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMCity(MCity mcity)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMCity";

            cmd.Parameters.AddWithValue("@CityNo", mcity.CityNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcity.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMCity()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCity order by CityNo";
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

        public DataView GetMCityByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCity where CityNo =" + ID;
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

        public MCity ModifyMCityByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MCity where CityNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MCity MM = new MCity();
                while (dr.Read())
                {
                    MM.CityNo = Convert.ToInt32(dr["CityNo"]);
                    if (!Convert.IsDBNull(dr["CityName"])) MM.CityName = Convert.ToString(dr["CityName"]);
                    if (!Convert.IsDBNull(dr["CityShortCode"])) MM.CityShortCode = Convert.ToString(dr["CityShortCode"]);
                    if (!Convert.IsDBNull(dr["CityLangName"])) MM.CityLangName = Convert.ToString(dr["CityLangName"]);
                    //if (!Convert.IsDBNull(dr["CountryNo"])) MM.CountryNo = Convert.ToInt64(dr["CountryNo"]);
                    if (!Convert.IsDBNull(dr["StateNo"])) MM.StateNo = Convert.ToInt64(dr["StateNo"]);
                    //if (!Convert.IsDBNull(dr["RegionNo"])) MM.RegionNo = Convert.ToInt64(dr["RegionNo"]);
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
            return new MCity();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select CityNo,CityShortCode AS 'Short Name',CityName AS 'City Name',Case when (IsActive = 'True') Then 'True' Else 'false' End As 'Status' from MCity order by CityName ";
                    break;
                case "CityName":
                    sql = "Select CityNo,CityShortCode AS 'Short Name',CityName AS 'City Name',Case when (IsActive = 'True') Then 'True' Else 'false' End As 'Status' from MCity where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by CityName";
                    break;
                case "CityShortCode":
                    sql = "Select CityNo,CityShortCode AS 'Short Name',CityName AS 'City Name',Case when (IsActive = 'True') Then 'True' Else 'false' End As 'Status' from MCity where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by CityName";
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
    /// This Class use for MCity
    /// </summary>
    public class MCity
    {
        private long mCityNo;
        private string mCityName;
        private string mCityShortCode;
        private string mCityLangName;
        private long mCountryNo;
        private long mStateNo;
        private long mRegionNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for CityNo
        /// </summary>
        public long CityNo
        {
            get { return mCityNo; }
            set { mCityNo = value; }
        }
        /// <summary>
        /// This Properties use for CityName
        /// </summary>
        public string CityName
        {
            get { return mCityName; }
            set { mCityName = value; }
        }
        /// <summary>
        /// This Properties use for CityShortCode
        /// </summary>
        public string CityShortCode
        {
            get { return mCityShortCode; }
            set { mCityShortCode = value; }
        }
        /// <summary>
        /// This Properties use for CityLangName
        /// </summary>
        public string CityLangName
        {
            get { return mCityLangName; }
            set { mCityLangName = value; }
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
