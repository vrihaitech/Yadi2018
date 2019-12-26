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
    class DBTQuotation
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTQuotation(TQuotation tquotation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTQuotation";

            cmd.Parameters.AddWithValue("@QuotationNo", tquotation.QuotationNo);

            cmd.Parameters.AddWithValue("@QuotationUserNo", tquotation.QuotationUserNo);

            cmd.Parameters.AddWithValue("@QuotationDate", tquotation.QuotationDate);

            cmd.Parameters.AddWithValue("@FromDate", tquotation.FromDate);

            cmd.Parameters.AddWithValue("@ToDate", tquotation.ToDate);

            cmd.Parameters.AddWithValue("@LedgerNo", tquotation.LedgerNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tquotation.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", tquotation.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tquotation.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }
        public bool DeleteTQuotation(TQuotation tquotation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTQuotation";

            cmd.Parameters.AddWithValue("@QuotationNo", tquotation.QuotationNo);
            commandcollection.Add(cmd);
            return true;
        }
        

        public TQuotation ModifyTQuotationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TQuotation where QuotationNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TQuotation MM = new TQuotation();
                while (dr.Read())
                {
                    MM.QuotationNo = Convert.ToInt32(dr["QuotationNo"]);
                    if (!Convert.IsDBNull(dr["QuotationUserNo"])) MM.QuotationUserNo = Convert.ToInt64(dr["QuotationUserNo"]);
                    if (!Convert.IsDBNull(dr["QuotationDate"])) MM.QuotationDate = Convert.ToDateTime(dr["QuotationDate"]);
                    if (!Convert.IsDBNull(dr["FromDate"])) MM.FromDate = Convert.ToDateTime(dr["FromDate"]);
                    if (!Convert.IsDBNull(dr["ToDate"])) MM.ToDate = Convert.ToDateTime(dr["ToDate"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TQuotation();
        }

        public DataView GetAllTQuotation()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TQuotation order by QuotationNo";
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

        public DataView GetAllTQuotation(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TQuotation Where QuotationNo=" + ID;
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

        public bool AddTQuotationDetails(TQuotationDetails tquotationdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTQuotationDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tquotationdetails.PkSrNo);

            //  cmd.Parameters.AddWithValue("@FkQuotationNo", tquotationdetails.FkQuotationNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tquotationdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@ItemNo", tquotationdetails.ItemNo);

            cmd.Parameters.AddWithValue("@MRP", tquotationdetails.MRP);

            cmd.Parameters.AddWithValue("@Rate", tquotationdetails.Rate);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", tquotationdetails.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tquotationdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@StatusNo", tquotationdetails.StatusNo);

            cmd.Parameters.AddWithValue("@IsClose", tquotationdetails.IsClose);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTQuotationDetails1(TQuotationDetails tquotationdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTQuotationDetails1";

            cmd.Parameters.AddWithValue("@PkSrNo", tquotationdetails.PkSrNo);

            cmd.Parameters.AddWithValue("@FkQuotationNo", tquotationdetails.FkQuotationNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tquotationdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@ItemNo", tquotationdetails.ItemNo);

            cmd.Parameters.AddWithValue("@MRP", tquotationdetails.MRP);

            cmd.Parameters.AddWithValue("@Rate", tquotationdetails.Rate);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", tquotationdetails.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tquotationdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@StatusNo", tquotationdetails.StatusNo);

            cmd.Parameters.AddWithValue("@IsClose", tquotationdetails.IsClose);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTQuotationDetails(TQuotationDetails tquotationdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTQuotationDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tquotationdetails.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTQuotationDetails1(TQuotationDetails tquotationdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTQuotationDetails1";

            cmd.Parameters.AddWithValue("@PkSrNo", tquotationdetails.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public TQuotationDetails ModifyTQuotationDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TQuotationDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TQuotationDetails MM = new TQuotationDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["FkQuotationNo"])) MM.FkQuotationNo = Convert.ToInt64(dr["FkQuotationNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["MRP"])) MM.MRP = Convert.ToDouble(dr["MRP"]);
                    if (!Convert.IsDBNull(dr["Rate"])) MM.Rate = Convert.ToDouble(dr["Rate"]);
                    if (!Convert.IsDBNull(dr["FkRateSettingNo"])) MM.FkRateSettingNo = Convert.ToInt64(dr["FkRateSettingNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["IsClose"])) MM.IsClose = Convert.ToBoolean(dr["IsClose"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TQuotationDetails();
        }

        public DataView GetAllTQuotationDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TQuotationDetails order by PkSrNo";
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

        public DataView GetAllTQuotationDetails(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TQuotationDetails Where PkSrNo=" + ID;
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

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddTQuotationDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkQuotationNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }

                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                if (commandcollection[0].CommandText == "AddTQuotation")
                    return Convert.ToInt64(commandcollection[0].Parameters["@ReturnID"].Value);
                else
                    return Convert.ToInt64(1);
            }
            catch (Exception e)
            {
                myTrans.Rollback();

                if (e.GetBaseException().Message == "")
                {
                    strerrormsg = e.Message;
                }
                else
                {
                    strerrormsg = e.GetBaseException().Message;
                }
                return 0;
            }
            finally
            {
                cn.Close();
            }
            //________________________________________________________________________________________________________________________________________________________________________________________________________________________
        }

        public bool ExecuteNonQueryStatements1()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        

                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                myTrans.Rollback();

                if (e.GetBaseException().Message == "")
                {
                    strerrormsg = e.Message;
                }
                else
                {
                    strerrormsg = e.GetBaseException().Message;
                }
                return false;
            }
            finally
            {
                cn.Close();
            }
            //________________________________________________________________________________________________________________________________________________________________________________________________________________________
        }
    }
    public class TQuotation
    {
        private long mQuotationNo;
        private long mQuotationUserNo;
        private DateTime mQuotationDate;
        private DateTime mFromDate;
        private DateTime mToDate;
        private long mLedgerNo;
        private long mCompanyNo;
        private long mStatusNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;
        public long QuotationNo
        {
            get { return mQuotationNo; }
            set { mQuotationNo = value; }
        }
        public long QuotationUserNo
        {
            get { return mQuotationUserNo; }
            set { mQuotationUserNo = value; }
        }
        public DateTime QuotationDate
        {
            get { return mQuotationDate; }
            set { mQuotationDate = value; }
        }
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        public DateTime ToDate
        {
            get { return mToDate; }
            set { mToDate = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
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

    public class TQuotationDetails
    {
        private long mPkSrNo;
        private long mFkQuotationNo;
        private long mLedgerNo;
        private long mItemNo;
        private double mMRP;
        private double mRate;
        private long mFkRateSettingNo;
        private long mCompanyNo;
        private long mStatusNo;
        private bool mIsClose;
        private string Mmsg;
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long FkQuotationNo
        {
            get { return mFkQuotationNo; }
            set { mFkQuotationNo = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public double MRP
        {
            get { return mMRP; }
            set { mMRP = value; }
        }
        public double Rate
        {
            get { return mRate; }
            set { mRate = value; }
        }
        public long FkRateSettingNo
        {
            get { return mFkRateSettingNo; }
            set { mFkRateSettingNo = value; }
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
        public bool IsClose
        {
            get { return mIsClose; }
            set { mIsClose = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
