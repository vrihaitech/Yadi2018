using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMCustomerSize
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMCustomerSize(MCustomerSize mcustomersize)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMCustomerSize";

            cmd.Parameters.AddWithValue("@CustSizeNo", mcustomersize.CustSizeNo);

            cmd.Parameters.AddWithValue("@CustSizeName", mcustomersize.CustSizeName);

            cmd.Parameters.AddWithValue("@CustSizeDesc", mcustomersize.CustSizeDesc);

            cmd.Parameters.AddWithValue("@CompanyNo", mcustomersize.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mcustomersize.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mcustomersize.UserDate);

            cmd.Parameters.AddWithValue("@IsActive", mcustomersize.IsActive);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcustomersize.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMCustomerSize(MCustomerSize mcustomersize)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMCustomerSize";

            cmd.Parameters.AddWithValue("@CustSizeNo", mcustomersize.CustSizeNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mcustomersize.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMCustomerSize()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCustomerSize order by CustSizeNo";
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

        public DataView GetMCustomerSizeByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MCustomerSize where CustSizeNo =" + ID;
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

        public MCustomerSize ModifyMCustomerSizeByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MCustomerSize where CustSizeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MCustomerSize MM = new MCustomerSize();
                while (dr.Read())
                {
                    MM.CustSizeNo = Convert.ToInt32(dr["CustSizeNo"]);
                    if (!Convert.IsDBNull(dr["CustSizeName"])) MM.CustSizeName = Convert.ToString(dr["CustSizeName"]);
                    if (!Convert.IsDBNull(dr["CustSizeDesc"])) MM.CustSizeDesc = Convert.ToString(dr["CustSizeDesc"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MCustomerSize();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select CustSizeNo,CustSizeName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCustomerSize order by CustSizeName ";
                    break;
                case "CustSizeName":
                    sql = "Select CustSizeNo,CustSizeName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCustomerSize where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by CustSizeName";
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

    public class MCustomerSize
    {
        private long mCustSizeNo;
        private string mCustSizeName;
        private string mCustSizeDesc;
        private long mStatusNo;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
        private bool mIsActive;
        private string Mmsg;

        public long CustSizeNo
        {
            get { return mCustSizeNo; }
            set { mCustSizeNo = value; }
        }
        public string CustSizeName
        {
            get { return mCustSizeName; }
            set { mCustSizeName = value; }
        }
        public string CustSizeDesc
        {
            get { return mCustSizeDesc; }
            set { mCustSizeDesc = value; }
        }
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
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
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

}
