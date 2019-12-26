using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBMPayTypeLedger
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMPayTypeLedger(MPayTypeLedger mpaytypeledger)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMPayTypeLedger";

            cmd.Parameters.AddWithValue("@PKPayTypeLedgerNo", mpaytypeledger.PKPayTypeLedgerNo);

            cmd.Parameters.AddWithValue("@PayTypeNo", mpaytypeledger.PayTypeNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mpaytypeledger.LedgerNo);

            cmd.Parameters.AddWithValue("@CompanyNo", mpaytypeledger.CompanyNo);

            cmd.Parameters.AddWithValue("@ChargesPerce", mpaytypeledger.ChargesPerce);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mpaytypeledger.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMPayTypeLedger(MPayTypeLedger mpaytypeledger)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMPayTypeLedger";

            cmd.Parameters.AddWithValue("@PKPayTypeLedgerNo", mpaytypeledger.PKPayTypeLedgerNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mpaytypeledger.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMPayTypeLedger()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPayTypeLedger order by PKPayTypeLedgerNo";
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

        public DataView GetMPayTypeLedgerByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPayTypeLedger where PKPayTypeLedgerNo =" + ID;
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

        public MPayTypeLedger ModifyMPayTypeByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MPayTypeLedger where PKPayTypeLedgerNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MPayTypeLedger MM = new MPayTypeLedger();
                while (dr.Read())
                {
                    MM.PKPayTypeLedgerNo = Convert.ToInt32(dr["PKPayTypeLedgerNo"]);
                    if (!Convert.IsDBNull(dr["PayTypeNo"])) MM.PayTypeNo = Convert.ToInt32(dr["PayTypeNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt32(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MPayTypeLedger();
        }
    }

    /// <summary>
    /// This Class use for MPayTypeLedger
    /// </summary>
    public class MPayTypeLedger
    {
        private long mPkPayTypeLedgerNo;
        private long mPayTypeNo;
        private long mLedgerNo;
        private long mCompanyNo;
        private double mChargesPerce;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkPayTypeLedgerNo
        /// </summary>
        public long PKPayTypeLedgerNo
        {
            get { return mPkPayTypeLedgerNo; }
            set { mPkPayTypeLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for PayTypeNo
        /// </summary>
        public long PayTypeNo
        {
            get { return mPayTypeNo; }
            set { mPayTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
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
        /// This Properties use for Charges Percentage
        /// </summary>
        public double ChargesPerce
        {
            get { return mChargesPerce; }
            set { mChargesPerce = value; }
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
