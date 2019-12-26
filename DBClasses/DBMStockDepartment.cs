using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMStockDepartment
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMStockDepartment(MStockDepartment mstockdepartment)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockDepartment";

            cmd.Parameters.AddWithValue("@DepartmentNo", mstockdepartment.DepartmentNo);

            cmd.Parameters.AddWithValue("@DepartmentName", mstockdepartment.DepartmentName);

            cmd.Parameters.AddWithValue("@ControlGroup", mstockdepartment.ControlGroup);

            cmd.Parameters.AddWithValue("@IsActive", mstockdepartment.IsActive);

            cmd.Parameters.AddWithValue("@UserId", mstockdepartment.UserId);

            cmd.Parameters.AddWithValue("@UserDate", mstockdepartment.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockdepartment.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd,CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstockdepartment.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMStockDepartment(MStockDepartment mstockdepartment)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMStockDepartment";

            cmd.Parameters.AddWithValue("@DepartmentNo", mstockdepartment.DepartmentNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstockdepartment.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MStockDepartment ModifyMStockDepartmentByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MStockDepartment where DepartmentNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MStockDepartment MM = new MStockDepartment();
                while (dr.Read())
                {
                    MM.DepartmentNo = Convert.ToInt32(dr["DepartmentNo"]);
                    if (!Convert.IsDBNull(dr["DepartmentName"])) MM.DepartmentName = Convert.ToString(dr["DepartmentName"]);
                    if (!Convert.IsDBNull(dr["ControlGroup"])) MM.ControlGroup = Convert.ToInt64(dr["ControlGroup"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
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
            return new MStockDepartment();
        }

        public DataView GetAllMStockDepartment()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockDepartment order by DepartmentNo";
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

        public DataView GetMStockDepartmentByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockDepartment where DepartmentNo =" + ID;
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
                    sql = "Select DepartmentNo,DepartmentName AS 'Department Name' from MStockDepartment ";
                    break;
                case "DepartmentName":
                    sql = "Select DepartmentNo,DepartmentName AS 'Department Name' from MStockDepartment where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'order by DepartmentName";
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
    /// This Class use for MStockDepartment
    /// </summary>
    public class MStockDepartment
    {
        private long mDepartmentNo;
        private string mDepartmentName;
        private long mControlGroup;
        private bool mIsActive;
        private long mUserId;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for DepartmentNo
        /// </summary>
        public long DepartmentNo
        {
            get { return mDepartmentNo; }
            set { mDepartmentNo = value; }
        }
        /// <summary>
        /// This Properties use for DepartmentName
        /// </summary>
        public string DepartmentName
        {
            get { return mDepartmentName; }
            set { mDepartmentName = value; }
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
