using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMStockCategory
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMStockCategory(MStockCategory mstockcategory)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockCategory";

            cmd.Parameters.AddWithValue("@CategoryNo", mstockcategory.CategoryNo);

            cmd.Parameters.AddWithValue("@CategoryName", mstockcategory.CategoryName);

            cmd.Parameters.AddWithValue("@ControlGroup", mstockcategory.ControlGroup);

            cmd.Parameters.AddWithValue("@DepartmentNo", mstockcategory.DepartmentNo);

            cmd.Parameters.AddWithValue("@IsActive", mstockcategory.IsActive);

            cmd.Parameters.AddWithValue("@UserId", mstockcategory.UserId);

            cmd.Parameters.AddWithValue("@UserDate", mstockcategory.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockcategory.CompanyNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstockcategory.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMStockCategory(MStockCategory mstockcategory)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMStockCategory";

            cmd.Parameters.AddWithValue("@CategoryNo", mstockcategory.CategoryNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstockcategory.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMStockCategory()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockCategory order by CategoryNo";
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

        public DataView GetMStockCategoryByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockCategory where CategoryNo =" + ID;
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

        public MStockCategory ModifyMStockCategoryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MStockCategory where CategoryNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MStockCategory MM = new MStockCategory();
                while (dr.Read())
                {
                    MM.CategoryNo = Convert.ToInt32(dr["CategoryNo"]);
                    if (!Convert.IsDBNull(dr["CategoryName"])) MM.CategoryName = Convert.ToString(dr["CategoryName"]);
                    if (!Convert.IsDBNull(dr["ControlGroup"])) MM.ControlGroup = Convert.ToInt64(dr["ControlGroup"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["DepartmentNo"])) MM.DepartmentNo = Convert.ToInt64(dr["DepartmentNo"]);
                    if (!Convert.IsDBNull(dr["UserId"])) MM.UserId = Convert.ToInt64(dr["UserId"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MStockCategory();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select CategoryNo,CategoryName AS 'Category Name' from MStockCategory order by CategoryName ";
                    break;
                case "CategoryName":
                    sql = "Select CategoryNo,CategoryName  AS 'Category Name' from MStockCategory where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by CategoryName";
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
    /// This Class use for MStockCategory
    /// </summary>
    public class MStockCategory
    {
        private long mCategoryNo;
        private string mCategoryName;
        private long mControlGroup;
        private bool mIsActive;
        private long mDepartmentNo;
        private long mUserId;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for CategoryNo
        /// </summary>
        public long CategoryNo
        {
            get { return mCategoryNo; }
            set { mCategoryNo = value; }
        }
        /// <summary>
        /// This Properties use for CategoryName
        /// </summary>
        public string CategoryName
        {
            get { return mCategoryName; }
            set { mCategoryName = value; }
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
        /// This Properties use for DepartmentNo
        /// </summary>
        public long DepartmentNo
        {
            get { return mDepartmentNo; }
            set { mDepartmentNo = value; }
        }
        /// <summary>
        /// This Properties use for UserId
        /// </summary>
        public long UserId
        {
            get { return mUserId; }
            set { mUserId = value; }
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
