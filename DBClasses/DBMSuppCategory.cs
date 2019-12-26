using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMSuppCategory
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMSuppCategory(MSuppCategory msuppcategory)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSuppCategory";

            cmd.Parameters.AddWithValue("@CategoryNo", msuppcategory.CategoryNo);

            cmd.Parameters.AddWithValue("@CategoryName", msuppcategory.CategoryName);

            cmd.Parameters.AddWithValue("@CategoryDesc", msuppcategory.CategoryDesc);

            cmd.Parameters.AddWithValue("@CompanyNo", msuppcategory.CompanyNo);

            cmd.Parameters.AddWithValue("@StatusNo", msuppcategory.StatusNo);

            cmd.Parameters.AddWithValue("@IsActive", msuppcategory.IsActive);

            cmd.Parameters.AddWithValue("@UserID", msuppcategory.UserID);

            cmd.Parameters.AddWithValue("@UserDate", msuppcategory.UserDate);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                msuppcategory.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMSuppCategory(MSuppCategory msuppcategory)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMSuppCategory";

            cmd.Parameters.AddWithValue("@CategoryNo", msuppcategory.CategoryNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                msuppcategory.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetMSuppCategoryByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSuppCategory where CategoryNo =" + ID;
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

        public DataView GetAllMSuppCategory()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSuppCategory order by CategoryNo";
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

        public MSuppCategory ModifyMSuppCategoryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MSuppCategory where CategoryNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MSuppCategory MM = new MSuppCategory();
                while (dr.Read())
                {
                    MM.CategoryNo = Convert.ToInt32(dr["CategoryNo"]);
                    if (!Convert.IsDBNull(dr["CategoryName"])) MM.CategoryName = Convert.ToString(dr["CategoryName"]);
                    if (!Convert.IsDBNull(dr["CategoryDesc"])) MM.CategoryDesc = Convert.ToString(dr["CategoryDesc"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MSuppCategory();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select CategoryNo ,CategoryName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MSuppCategory order by CategoryName ";
                    break;
                case "CategoryName":
                    sql = "Select CategoryNo,CategoryName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MSuppCategory where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by CategoryName";


                    break;

            }
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return ds.Tables[0].DefaultView;
        }

    }

    public class MSuppCategory
    {
        private long mCategoryNo;
        private string mCategoryName;
        private string mCategoryDesc;
        private long mCompanyNo;
        private long mStatusNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        public long CategoryNo
        {
            get { return mCategoryNo; }
            set { mCategoryNo = value; }
        }
        public string CategoryName
        {
            get { return mCategoryName; }
            set { mCategoryName = value; }
        }
        public string CategoryDesc
        {
            get { return mCategoryDesc; }
            set { mCategoryDesc = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
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
