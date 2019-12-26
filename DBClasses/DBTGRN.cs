using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;
using OMControls;

namespace OM
{
    class DBTGRN
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTGRN(TGRN tgrn)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTGRN";

            cmd.Parameters.AddWithValue("@GRNNo", tgrn.GRNNo);

            cmd.Parameters.AddWithValue("@GRNUserNo", tgrn.GRNUserNo);

            cmd.Parameters.AddWithValue("@GRNDate", tgrn.GRNDate);

            cmd.Parameters.AddWithValue("@GRNTime", tgrn.GRNTime);

            cmd.Parameters.AddWithValue("@LedgerNo", tgrn.LedgerNo);

            cmd.Parameters.AddWithValue("@RefNo", tgrn.RefNo);

            cmd.Parameters.AddWithValue("@GRNAmount", tgrn.GRNAmount);

            cmd.Parameters.AddWithValue("@CompanyNo", tgrn.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", tgrn.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tgrn.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTGRN(TGRN tgrn)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTGRN";

            cmd.Parameters.AddWithValue("@GRNNo", tgrn.GRNNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tgrn.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTGRN()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TGRN order by GRNNo";
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

        public DataView GetTGRNByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TGRN where GRNNo =" + ID;
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

        public TGRN ModifyTGRNByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TGRN where GRNNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TGRN MM = new TGRN();
                while (dr.Read())
                {
                    MM.GRNNo = Convert.ToInt32(dr["GRNNo"]);
                    if (!Convert.IsDBNull(dr["GRNUserNo"])) MM.GRNUserNo = Convert.ToInt64(dr["GRNUserNo"]);
                    if (!Convert.IsDBNull(dr["GRNDate"])) MM.GRNDate = Convert.ToDateTime(dr["GRNDate"]);
                    if (!Convert.IsDBNull(dr["GRNTime"])) MM.GRNTime = Convert.ToDateTime(dr["GRNTime"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["RefNo"])) MM.RefNo = Convert.ToString(dr["RefNo"]);
                    if (!Convert.IsDBNull(dr["GRNAmount"])) MM.GRNAmount = Convert.ToDouble(dr["GRNAmount"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["IsCancel"])) MM.IsCancel = Convert.ToBoolean(dr["IsCancel"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TGRN();
        }

        public bool AddTGRNDetails(TGRNDetails tgrndetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTGRNDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tgrndetails.PkSrNo);

            //cmd.Parameters.AddWithValue("@FkGRNNo", tgrndetails.FkGRNNo);

            cmd.Parameters.AddWithValue("@GRNSrNo", tgrndetails.GRNSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", tgrndetails.ItemNo);

            cmd.Parameters.AddWithValue("@Quantity", tgrndetails.Quantity);

            cmd.Parameters.AddWithValue("@BilledQuantity", tgrndetails.BilledQuantity);

            cmd.Parameters.AddWithValue("@Rate", tgrndetails.Rate);

            cmd.Parameters.AddWithValue("@Amount", tgrndetails.Amount);

            cmd.Parameters.AddWithValue("@NetRate", tgrndetails.NetRate);

            cmd.Parameters.AddWithValue("@NetAmount", tgrndetails.NetAmount);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", tgrndetails.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tgrndetails.CompanyNo);

            cmd.Parameters.AddWithValue("@GodownNo", tgrndetails.GodownNo);

            cmd.Parameters.AddWithValue("@UserID", tgrndetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tgrndetails.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTGRNDetails(TGRNDetails tgrndetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTGRNDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tgrndetails.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tgrndetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTGRNDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TGRNDetails order by PkSrNo";
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

        public DataView GetTGRNDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TGRNDetails where PkSrNo =" + ID;
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

        public TGRNDetails ModifyTGRNDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TGRNDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TGRNDetails MM = new TGRNDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["FkGRNNo"])) MM.FkGRNNo = Convert.ToInt64(dr["FkGRNNo"]);
                    if (!Convert.IsDBNull(dr["GRNSrNo"])) MM.GRNSrNo = Convert.ToInt64(dr["GRNSrNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["Quantity"])) MM.Quantity = Convert.ToDouble(dr["Quantity"]);
                    if (!Convert.IsDBNull(dr["BilledQuantity"])) MM.BilledQuantity = Convert.ToDouble(dr["BilledQuantity"]);
                    if (!Convert.IsDBNull(dr["Rate"])) MM.Rate = Convert.ToDouble(dr["Rate"]);
                    if (!Convert.IsDBNull(dr["Amount"])) MM.Amount = Convert.ToDouble(dr["Amount"]);
                    if (!Convert.IsDBNull(dr["NetRate"])) MM.NetRate = Convert.ToDouble(dr["NetRate"]);
                    if (!Convert.IsDBNull(dr["NetAmount"])) MM.NetAmount = Convert.ToDouble(dr["NetAmount"]);
                    if (!Convert.IsDBNull(dr["FkRateSettingNo"])) MM.FkRateSettingNo = Convert.ToInt64(dr["FkRateSettingNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["GodownNo"])) MM.GodownNo = Convert.ToInt64(dr["GodownNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TGRNDetails();
        }

        public long ExecuteNonQueryStatements()
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

                        if (commandcollection[i].CommandText == "AddTGRNDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkGRNNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                return Convert.ToInt64(commandcollection[0].Parameters["@ReturnID"].Value);
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
        }
            //____________
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
                 //____________________________________________________________________________________________________________________________________________________________________________________________________________
        }
    }

    public class TGRN
    {
        private long mGRNNo;
        private long mGRNUserNo;
        private DateTime mGRNDate;
        private DateTime mGRNTime;
        private long mLedgerNo;
        private string mRefNo;
        private double mGRNAmount;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private bool mIsCancel;
        private string Mmsg;

        public long GRNNo
        {
            get { return mGRNNo; }
            set { mGRNNo = value; }
        }
        public long GRNUserNo
        {
            get { return mGRNUserNo; }
            set { mGRNUserNo = value; }
        }
        public DateTime GRNDate
        {
            get { return mGRNDate; }
            set { mGRNDate = value; }
        }
        public DateTime GRNTime
        {
            get { return mGRNTime; }
            set { mGRNTime = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public string RefNo
        {
            get { return mRefNo; }
            set { mRefNo = value; }
        }
        public double GRNAmount
        {
            get { return mGRNAmount; }
            set { mGRNAmount = value; }
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
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
        }
        public bool IsCancel
        {
            get { return mIsCancel; }
            set { mIsCancel = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TGRNDetails
    {
        private long mPkSrNo;
        private long mFkGRNNo;
        private long mGRNSrNo;
        private long mItemNo;
        private double mQuantity;
        private double mBilledQuantity;
        private double mRate;
        private double mAmount;
        private double mNetRate;
        private double mNetAmount;
        private long mFkRateSettingNo;
        private long mCompanyNo;
        private long mGodownNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long FkGRNNo
        {
            get { return mFkGRNNo; }
            set { mFkGRNNo = value; }
        }
        public long GRNSrNo
        {
            get { return mGRNSrNo; }
            set { mGRNSrNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public double BilledQuantity
        {
            get { return mBilledQuantity; }
            set { mBilledQuantity = value; }
        }
        public double Rate
        {
            get { return mRate; }
            set { mRate = value; }
        }
        public double Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        public double NetRate
        {
            get { return mNetRate; }
            set { mNetRate = value; }
        }
        public double NetAmount
        {
            get { return mNetAmount; }
            set { mNetAmount = value; }
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
        public long GodownNo
        {
            get { return mGodownNo; }
            set { mGodownNo = value; }
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
