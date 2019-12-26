using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using OMControls;


namespace OM
{
    
    class DBTDocketPrinting
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTDocketPrinting(TDocketPrinting tdocketprinting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTDocketPrinting";

            cmd.Parameters.AddWithValue("@DocketPrintingNo", tdocketprinting.DocketPrintingNo);

            cmd.Parameters.AddWithValue("@DocketUserNo", tdocketprinting.DocketUserNo);

            cmd.Parameters.AddWithValue("@FromDate", tdocketprinting.FromDate);

            cmd.Parameters.AddWithValue("@ToDate", tdocketprinting.ToDate);

            cmd.Parameters.AddWithValue("@LedgerNo", tdocketprinting.LedgerNo);

            cmd.Parameters.AddWithValue("@UserID", tdocketprinting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tdocketprinting.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tdocketprinting.CompanyNo);
                     

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);

            return true;
        }

        public TDocketPrinting ModifyTDocketPrintingByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TDocketPrinting where DocketPrintingNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TDocketPrinting MM = new TDocketPrinting();
                while (dr.Read())
                {
                    MM.DocketPrintingNo = Convert.ToInt32(dr["DocketPrintingNo"]);
                    if (!Convert.IsDBNull(dr["DocketUserNo"])) MM.DocketUserNo = Convert.ToInt64(dr["DocketUserNo"]);
                    if (!Convert.IsDBNull(dr["FromDate"])) MM.FromDate = Convert.ToDateTime(dr["FromDate"]);
                    if (!Convert.IsDBNull(dr["ToDate"])) MM.ToDate = Convert.ToDateTime(dr["ToDate"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["Status"])) MM.Status = Convert.ToInt64(dr["Status"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TDocketPrinting();
        }
        
        public bool AddTDocketPrintingDetails(TDocketPrintingDetails tdocketprintingdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTDocketPrintingDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tdocketprintingdetails.PkSrNo);

           // cmd.Parameters.AddWithValue("@DocketPrintingNo", tdocketprintingdetails.DocketPrintingNo);

            cmd.Parameters.AddWithValue("@FkVoucherNo", tdocketprintingdetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@Remark", tdocketprintingdetails.Remark);

            cmd.Parameters.AddWithValue("@CompanyNo", tdocketprintingdetails.CompanyNo);

            commandcollection.Add(cmd);

            return true;
        }

        public bool DeleteTDocketPrintingDetails(TDocketPrintingDetails tdocketprintingdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTDocketPrintingDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tdocketprintingdetails.PkSrNo);

            commandcollection.Add(cmd);

            return true;
        }

        public bool DeleteTDocketPrinting(TDocketPrinting tdocketprinting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTDocketPrinting";

            cmd.Parameters.AddWithValue("@DocketPrintingNo", tdocketprinting.DocketPrintingNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int cntVchNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddTDocketPrinting")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTDocketPrintingDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@DocketPrintingNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                if (cntVchNo == -1)
                    return 0;
                else
                    return Convert.ToInt64(commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
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

    }
    public class TDocketPrinting
    {
        private long mDocketPrintingNo;
        private long mDocketUserNo;
        private DateTime mFromDate;
        private DateTime mToDate;
        private long mLedgerNo;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private long mStatus;
        private string Mmsg;

        public long DocketPrintingNo
        {
            get { return mDocketPrintingNo; }
            set { mDocketPrintingNo = value; }
        }
        public long DocketUserNo
        {
            get { return mDocketUserNo; }
            set { mDocketUserNo = value; }
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
        public long Status
        {
            get { return mStatus; }
            set { mStatus = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TDocketPrintingDetails
    {
        private long mPkSrNo;
        private long mDocketPrintingNo;
        private long mFkVoucherNo;
        private string mRemark;
        private long mCompanyNo;
        private long mStatus;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long DocketPrintingNo
        {
            get { return mDocketPrintingNo; }
            set { mDocketPrintingNo = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        public string Remark
        {
            get { return mRemark; }
            set { mRemark = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public long Status
        {
            get { return mStatus; }
            set { mStatus = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

}
