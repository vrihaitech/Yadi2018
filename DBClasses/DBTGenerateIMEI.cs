using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBTGenerateIMEI
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddTGenerateIMEI(TGenerateIMEI TGenerateIMEI)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTGenerateIMEI";

            cmd.Parameters.AddWithValue("@PkGenerateIMEIID", TGenerateIMEI.PkGenerateIMEIID);

            cmd.Parameters.AddWithValue("@IMEINo", TGenerateIMEI.IMEINo);

            cmd.Parameters.AddWithValue("@FkVoucherNo", TGenerateIMEI.FkVoucherNo);

            cmd.Parameters.AddWithValue("@FkStockTrnNo", TGenerateIMEI.FkStockTrnNo);

            cmd.Parameters.AddWithValue("@ItemNo", TGenerateIMEI.ItemNo);

            cmd.Parameters.AddWithValue("@IsSales", TGenerateIMEI.IsSales);

            cmd.Parameters.AddWithValue("@SalesStockTrnNo", TGenerateIMEI.SalesStockTrnNo);

            cmd.Parameters.AddWithValue("@SalesFkVoucherNo", TGenerateIMEI.SalesFkVoucherNo);

            cmd.Parameters.AddWithValue("@IsActive", TGenerateIMEI.IsActive);

            cmd.Parameters.AddWithValue("@UserID", TGenerateIMEI.UserID);

            cmd.Parameters.AddWithValue("@UserDate", TGenerateIMEI.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", TGenerateIMEI.CompanyNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                TGenerateIMEI.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteTGenerateIMEI(TGenerateIMEI TGenerateIMEI)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTGenerateIMEI";

            cmd.Parameters.AddWithValue("@PkGenerateIMEIID", TGenerateIMEI.PkGenerateIMEIID);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                TGenerateIMEI.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTGenerateIMEI()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TGenerateIMEI order by PkGenerateIMEIID";
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

        public DataView GetTGenerateIMEIByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TGenerateIMEI where PkGenerateIMEIID =" + ID;
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

        public TGenerateIMEI ModifyTGenerateIMEIByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TGenerateIMEI where PkGenerateIMEIID =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TGenerateIMEI MM = new TGenerateIMEI();
                while (dr.Read())
                {
                    MM.PkGenerateIMEIID = Convert.ToInt32(dr["PkGenerateIMEIID"]);
                    if (!Convert.IsDBNull(dr["IMEINo"])) MM.IMEINo = Convert.ToString(dr["IMEINo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt64(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["FkStockTrnNo"])) MM.FkStockTrnNo = Convert.ToInt64(dr["FkStockTrnNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["IsSales"])) MM.IsSales = Convert.ToBoolean(dr["IsSales"]);
                    if (!Convert.IsDBNull(dr["SalesStockTrnNo"])) MM.SalesStockTrnNo = Convert.ToInt64(dr["SalesStockTrnNo"]);
                    if (!Convert.IsDBNull(dr["SalesFkVoucherNo"])) MM.SalesFkVoucherNo = Convert.ToInt64(dr["SalesFkVoucherNo"]);

                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TGenerateIMEI();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select PkGenerateIMEIID,IMEINo AS 'Branch Name',Case when (TGenerateIMEI.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from TGenerateIMEI ";
                    break;
                case "IMEINo":
                    sql = "Select PkGenerateIMEIID,IMEINo AS 'Branch Name',Case when (TGenerateIMEI.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from TGenerateIMEI where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'order by IMEINo";
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


    public class TGenerateIMEI
    {
        private long mPkGenerateIMEIID;
        private string mIMEINo;
        private long mFkVoucherNo;
        private long mFkStockTrnNo;
        private long mItemNo;
        private bool mIsSales;
        private long mSalesStockTrnNo;
        private long mSalesFkVoucherNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private string Mmsg;

        public long PkGenerateIMEIID
        {
            get { return mPkGenerateIMEIID; }
            set { mPkGenerateIMEIID = value; }
        }
        public string IMEINo
        {
            get { return mIMEINo; }
            set { mIMEINo = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        public long FkStockTrnNo
        {
            get { return mFkStockTrnNo; }
            set { mFkStockTrnNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public bool IsSales
        {
            get { return mIsSales; }
            set { mIsSales = value; }
        }
        public long SalesStockTrnNo
        {
            get { return mSalesStockTrnNo; }
            set { mSalesStockTrnNo = value; }
        }
        public long SalesFkVoucherNo
        {
            get { return mSalesFkVoucherNo; }
            set { mSalesFkVoucherNo = value; }
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
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
