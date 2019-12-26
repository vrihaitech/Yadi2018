using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMCustomerType
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMCustomerType(MCustomerType mcustomertype)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMCustomerType";

            cmd.Parameters.AddWithValue("@CustomerTypeNo", mcustomertype.CustomerTypeNo);

            cmd.Parameters.AddWithValue("@CustomerTypeName", mcustomertype.CustomerTypeName);

            cmd.Parameters.AddWithValue("@CustomerTypeDesc", mcustomertype.CustomerTypeDesc);

            cmd.Parameters.AddWithValue("@CompanyNo", mcustomertype.CompanyNo);

            cmd.Parameters.AddWithValue("@IsActive", mcustomertype.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mcustomertype.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mcustomertype.UserDate);


            if (ObjTrans.ExecuteNonQuery(cmd,CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMCustomerType(MCustomerType mcustomertype)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMCustomerType";

            cmd.Parameters.AddWithValue("@CustomerTypeNo", mcustomertype.CustomerTypeNo);


            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MCustomerType ModifyMCustomerTypeByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MCustomerType where CustomerTypeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MCustomerType MM = new MCustomerType();
                while (dr.Read())
                {
                    MM.CustomerTypeNo = Convert.ToInt32(dr["CustomerTypeNo"]);
                    if (!Convert.IsDBNull(dr["CustomerTypeName"])) MM.CustomerTypeName = Convert.ToString(dr["CustomerTypeName"]);
                    if (!Convert.IsDBNull(dr["CustomerTypeDesc"])) MM.CustomerTypeDesc = Convert.ToString(dr["CustomerTypeDesc"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MCustomerType();
        }
        
        public DataView GetAllMCustomerType()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCustomerType order by CustomerTypeNo";
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                Con.Close();
            }
            return ds.Tables[(0)].DefaultView;
        }
        
        public DataView GetAllMCustomerType(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCustomerType Where CustomerTypeNo=" + ID;
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch
            {
                throw;
            }
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
                    sql = "Select CustomerTypeNo ,CustomerTypeName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCustomerType order by CustomerTypeName ";
                    break;
                case "CustomerTypeName":
                    sql = "Select CustomerTypeNo,CustomerTypeName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCustomerType where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by CustomerTypeName";
                  
                
                    break;

            }
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException e)
            {
                e.ToString();
            }
            return ds.Tables[0].DefaultView;
        }

    }

    public class MCustomerType
    {
        private long mCustomerTypeNo;
        private string mCustomerTypeName;
        private string mCustomerTypeDesc;
        private long mCompanyNo;
        private bool mIsActive;
        private long mStatusNo;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        public long CustomerTypeNo
        {
            get { return mCustomerTypeNo; }
            set { mCustomerTypeNo = value; }
        }
        public string CustomerTypeName
        {
            get { return mCustomerTypeName; }
            set { mCustomerTypeName = value; }
        }
        public string CustomerTypeDesc
        {
            get { return mCustomerTypeDesc; }
            set { mCustomerTypeDesc = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
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
