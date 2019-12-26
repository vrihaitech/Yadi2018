using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMCountry
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMCountry(MCountry mcountry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMCountry";

            cmd.Parameters.AddWithValue("@CountryNo", mcountry.CountryNo);

            cmd.Parameters.AddWithValue("@CountryName", mcountry.CountryName);

            cmd.Parameters.AddWithValue("@CountryShortCode", mcountry.CountryShortCode);
            cmd.Parameters.AddWithValue("@CountryLangName", mcountry.CountryLangName);
            cmd.Parameters.AddWithValue("@IsActive", mcountry.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mcountry.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mcountry.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mcountry.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcountry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMCountry(MCountry mcountry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMCountry";

            cmd.Parameters.AddWithValue("@CountryNo", mcountry.CountryNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcountry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMCountry()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCountry order by CountryNo";
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

        public DataView GetMCountryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCountry where CountryNo =" + ID;
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
        public MCountry ModifyMCountryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MCountry where CountryNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MCountry MM = new MCountry();
                while (dr.Read())
                {
                    MM.CountryNo = Convert.ToInt32(dr["CountryNo"]);
                    if (!Convert.IsDBNull(dr["CountryName"])) MM.CountryName = Convert.ToString(dr["CountryName"]);
                    if (!Convert.IsDBNull(dr["CountryShortCode"])) MM.CountryShortCode = Convert.ToString(dr["CountryShortCode"]);
                    if (!Convert.IsDBNull(dr["CountryLangName"])) MM.CountryLangName = Convert.ToString(dr["CountryLangName"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MCountry();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select CountryNo,CountryShortCode AS 'Short Name',CountryName AS 'Country Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCountry Order By CountryName ";
                    break;
                case "CountryName":
                    sql = "Select CountryNo,CountryShortCode AS 'Short Name',CountryName AS 'Country Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCountry where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' Order By CountryName";
                    break;
                case "CountryShortCode":
                    sql = "Select CountryNo,CountryShortCode AS 'Short Name',CountryName AS 'Country Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCountry where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' Order By CountryShortCode";
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
    /// This Class use for MCountry
    /// </summary>
    public class MCountry
    {
        private long mCountryNo;
        private string mCountryName;
        private string mCountryShortCode;
        private string mCountryLangName;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for CountryNo
        /// </summary>
        public long CountryNo
        {
            get { return mCountryNo; }
            set { mCountryNo = value; }
        }
        /// <summary>
        /// This Properties use for CountryName
        /// </summary>
        public string CountryName
        {
            get { return mCountryName; }
            set { mCountryName = value; }
        }
        /// <summary>
        /// This Properties use for CountryShortCode
        /// </summary>
        public string CountryShortCode
        {
            get { return mCountryShortCode; }
            set { mCountryShortCode = value; }
        }
        /// <summary>
        /// This Properties use for CountryLangName
        /// </summary>
        public string CountryLangName
        {
            get { return mCountryLangName; }
            set { mCountryLangName = value; }
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
