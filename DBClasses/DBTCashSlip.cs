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
    
    class DBTCashSlip
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTCashSlip(TCashSlip TCashSlip)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTCashSlip";

            cmd.Parameters.AddWithValue("@CashSlipNo", TCashSlip.CashSlipNo);

            cmd.Parameters.AddWithValue("@DocketUserNo", TCashSlip.DocketUserNo);

            cmd.Parameters.AddWithValue("@ToDate", TCashSlip.ToDate);

            cmd.Parameters.AddWithValue("@LedgerNo", TCashSlip.LedgerNo);

            cmd.Parameters.AddWithValue("@UserID", TCashSlip.UserID);

            cmd.Parameters.AddWithValue("@UserDate", TCashSlip.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", TCashSlip.CompanyNo);
                     

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);

            return true;
        }

        public TCashSlip ModifyTCashSlipByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TCashSlip where CashSlipNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TCashSlip MM = new TCashSlip();
                while (dr.Read())
                {
                    MM.CashSlipNo = Convert.ToInt32(dr["CashSlipNo"]);
                    if (!Convert.IsDBNull(dr["DocketUserNo"])) MM.DocketUserNo = Convert.ToInt64(dr["DocketUserNo"]);
                    if (!Convert.IsDBNull(dr["ToDate"])) MM.ToDate = Convert.ToDateTime(dr["ToDate"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TCashSlip();
        }
        
        public bool AddTCashSlipDetails(TCashSlipDetails TCashSlipdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTCashSlipDetails";

            cmd.Parameters.AddWithValue("@CashSlipDetailsNo", TCashSlipdetails.CashSlipDetailsNo);

            // cmd.Parameters.AddWithValue("@CashSlipNo", TCashSlipdetails.CashSlipNo);

            cmd.Parameters.AddWithValue("@Note", TCashSlipdetails.Note);

            cmd.Parameters.AddWithValue("@RSType", TCashSlipdetails.RSType);

            cmd.Parameters.AddWithValue("@Pieces", TCashSlipdetails.Pieces);

            cmd.Parameters.AddWithValue("@Amount", TCashSlipdetails.Amount);

            cmd.Parameters.AddWithValue("@IsActive", TCashSlipdetails.IsActive);

            cmd.Parameters.AddWithValue("@UserID", TCashSlipdetails.UserID);

          //  cmd.Parameters.AddWithValue("@UserDate", TCashSlipdetails.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", TCashSlipdetails.CompanyNo);

            commandcollection.Add(cmd);

            return true;
        }

        public bool DeleteTCashSlipDetails(TCashSlipDetails TCashSlipdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTCashSlipDetails";

            cmd.Parameters.AddWithValue("@CashSlipDetailsNo", TCashSlipdetails.CashSlipDetailsNo);

            commandcollection.Add(cmd);

            return true;
        }

        public bool DeleteTCashSlip(TCashSlip TCashSlip)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTCashSlip";

            cmd.Parameters.AddWithValue("@CashSlipNo", TCashSlip.CashSlipNo);

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
                        if (commandcollection[i].CommandText == "AddTCashSlip")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTCashSlipDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@CashSlipNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
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

    public class TCashSlip
    {
        private long mCashSlipNo;
        private long mDocketUserNo;
        private DateTime mToDate;
        private long mLedgerNo;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private string Mmsg;

        public long CashSlipNo
        {
            get { return mCashSlipNo; }
            set { mCashSlipNo = value; }
        }
        public long DocketUserNo
        {
            get { return mDocketUserNo; }
            set { mDocketUserNo = value; }
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
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TCashSlipDetails
    {
        private long mCashSlipDetailsNo;
        private long mCashSlipNo;
        private string mNote;
        private string mRSType;
        private long mPieces;
        private double mAmount;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private string Mmsg;

        public long CashSlipDetailsNo
        {
            get { return mCashSlipDetailsNo; }
            set { mCashSlipDetailsNo = value; }
        }
        public long CashSlipNo
        {
            get { return mCashSlipNo; }
            set { mCashSlipNo = value; }
        }
        public string Note
        {
            get { return mNote; }
            set { mNote = value; }
        }
        public string RSType
        {
            get { return mRSType; }
            set { mRSType = value; }
        }
        public long Pieces
        {
            get { return mPieces; }
            set { mPieces = value; }
        }
        public double Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
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
