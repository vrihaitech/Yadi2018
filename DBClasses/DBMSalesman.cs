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
    class DBMSalesman
    {
        OMCommonClass cc = new OMCommonClass();
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMSalesman(MSalesman msalesman)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSalesman";

            cmd.Parameters.AddWithValue("@SalesmanNo", msalesman.SalesmanNo);

            cmd.Parameters.AddWithValue("@SalesmanName", msalesman.SalesmanName);

            cmd.Parameters.AddWithValue("@SalesmanUserNo", msalesman.SalesmanUserNo);

            cmd.Parameters.AddWithValue("@Address", msalesman.Address);

            cmd.Parameters.AddWithValue("@PinCode", msalesman.PinCode);

            cmd.Parameters.AddWithValue("@PhoneNo", msalesman.PhoneNo);

            cmd.Parameters.AddWithValue("@Mobile", msalesman.Mobile);

            cmd.Parameters.AddWithValue("@Disc", msalesman.Disc);

            cmd.Parameters.AddWithValue("@Rupees", msalesman.Rupees);

            cmd.Parameters.AddWithValue("@IsActive", msalesman.IsActive);

            cmd.Parameters.AddWithValue("@UserID", msalesman.UserID);

            cmd.Parameters.AddWithValue("@UserDate", msalesman.UserDate);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                msalesman.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }



        public bool DeleteMSalesman(MSalesman msalesman)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMSalesman";

            cmd.Parameters.AddWithValue("@SalesmanNo", msalesman.SalesmanNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                msalesman.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMSalesman()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSalesman order by SalesmanNo";
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

        public DataView GetMSalesmanByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSalesman where SalesmanNo =" + ID;
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

        public MSalesman ModifyMSalesmanByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MSalesman where SalesmanNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MSalesman MM = new MSalesman();
                while (dr.Read())
                {
                    MM.SalesmanNo = Convert.ToInt32(dr["SalesmanNo"]);
                    if (!Convert.IsDBNull(dr["SalesmanName"])) MM.SalesmanName = Convert.ToString(dr["SalesmanName"]);
                    if (!Convert.IsDBNull(dr["SalesmanUserNo"])) MM.SalesmanUserNo = Convert.ToInt64(dr["SalesmanUserNo"]);
                    if (!Convert.IsDBNull(dr["Address"])) MM.Address = Convert.ToString(dr["Address"]);
                    if (!Convert.IsDBNull(dr["PinCode"])) MM.PinCode = Convert.ToString(dr["PinCode"]);
                    if (!Convert.IsDBNull(dr["PhoneNo"])) MM.PhoneNo = Convert.ToString(dr["PhoneNo"]);
                    if (!Convert.IsDBNull(dr["Mobile"])) MM.Mobile = Convert.ToString(dr["Mobile"]);
                    if (!Convert.IsDBNull(dr["Disc"])) MM.Disc = Convert.ToDouble(dr["Disc"]);
                    if (!Convert.IsDBNull(dr["Rupees"])) MM.Rupees = Convert.ToDouble(dr["Rupees"]);
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
            return new MSalesman();
        }



        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select SalesmanNo,SalesmanUserNo AS 'Short Name',SalesmanName from MSalesman order by SalesmanNo";
                    break;
                case "SalesmanName":
                    sql = "Select SalesmanNo,SalesmanUserNo AS 'Short Name',SalesmanName from MSalesman where " + Column + " like '" + Value.Replace("'", "''").Trim() + "' + '%' order by SalesmanNo";
                    break;
                case "SalesmanUserNo":
                    sql = "Select SalesmanNo,SalesmanUserNo AS 'Short Name',SalesmanName from MSalesman where " + Column + " like '" + Value.Replace("'", "''").Trim() + "' + '%' order by SalesmanNo";
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
    public class MSalesman
    {
        private long mSalesmanNo;
        private string mSalesmanName;
        private long mSalesmanUserNo;
        private string mAddress;
        private string mPinCode;
        private string mPhoneNo;
        private string mMobile;
        private double mDisc;
        private double mRupees;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        public long SalesmanNo
        {
            get { return mSalesmanNo; }
            set { mSalesmanNo = value; }
        }
        public string SalesmanName
        {
            get { return mSalesmanName; }
            set { mSalesmanName = value; }
        }
        public long SalesmanUserNo
        {
            get { return mSalesmanUserNo; }
            set { mSalesmanUserNo = value; }
        }
        public string Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        public string PinCode
        {
            get { return mPinCode; }
            set { mPinCode = value; }
        }
        public string PhoneNo
        {
            get { return mPhoneNo; }
            set { mPhoneNo = value; }
        }
        public string Mobile
        {
            get { return mMobile; }
            set { mMobile = value; }
        }
        public double Disc
        {
            get { return mDisc; }
            set { mDisc = value; }
        }
        public double Rupees
        {
            get { return mRupees; }
            set { mRupees = value; }
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
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }


}
