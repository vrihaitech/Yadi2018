using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using OMControls;

namespace OM
{
    class DBMDutiesTaxesInfo
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;
       
        public bool AddMDutiesTaxesInfo(MDutiesTaxesInfo mdutiestaxesinfo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMDutiesTaxesInfo";

            cmd.Parameters.AddWithValue("@PkSrNo", mdutiestaxesinfo.PkSrNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mdutiestaxesinfo.LedgerNo);

            cmd.Parameters.AddWithValue("@TaxOnLedgerNo", mdutiestaxesinfo.TaxOnLedgerNo);

            cmd.Parameters.AddWithValue("@FromDate", mdutiestaxesinfo.FromDate);

            cmd.Parameters.AddWithValue("@CalculationMethod", mdutiestaxesinfo.CalculationMethod);

            cmd.Parameters.AddWithValue("@Percentage", mdutiestaxesinfo.Percentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mdutiestaxesinfo.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mdutiestaxesinfo.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mdutiestaxesinfo.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mdutiestaxesinfo.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mdutiestaxesinfo.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMDutiesTaxesInfo(MDutiesTaxesInfo mdutiestaxesinfo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMDutiesTaxesInfo";

            cmd.Parameters.AddWithValue("@PkSrNo", mdutiestaxesinfo.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mdutiestaxesinfo.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMDutiesTaxesInfo()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MDutiesTaxesInfo order by PkSrNo";
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

        public DataView GetMDutiesTaxesInfoByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MDutiesTaxesInfo where PkSrNo =" + ID;
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

        public MDutiesTaxesInfo ModifyMDutiesTaxesInfoByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MDutiesTaxesInfo where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MDutiesTaxesInfo MM = new MDutiesTaxesInfo();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["TaxOnLedgerNo"])) MM.TaxOnLedgerNo = Convert.ToInt64(dr["TaxOnLedgerNo"]);
                    if (!Convert.IsDBNull(dr["FromDate"])) MM.FromDate = Convert.ToDateTime(dr["FromDate"]);
                    if (!Convert.IsDBNull(dr["CalculationMethod"])) MM.CalculationMethod = Convert.ToString(dr["CalculationMethod"]);
                    if (!Convert.IsDBNull(dr["Percentage"])) MM.Percentage = Convert.ToInt64(dr["Percentage"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MDutiesTaxesInfo();
        }



        public bool AddMItemTaxInfo(MItemTaxInfo mitemtaxinfo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemTaxInfo";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxinfo.PkSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", mitemtaxinfo.ItemNo);

            cmd.Parameters.AddWithValue("@TaxLedgerNo", mitemtaxinfo.TaxLedgerNo);

            cmd.Parameters.AddWithValue("@SalesLedgerNo", mitemtaxinfo.SalesLedgerNo);

            cmd.Parameters.AddWithValue("@FromDate", mitemtaxinfo.FromDate);

            cmd.Parameters.AddWithValue("@CalculationMethod", mitemtaxinfo.CalculationMethod);

            cmd.Parameters.AddWithValue("@Percentage", mitemtaxinfo.Percentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemtaxinfo.CompanyNo);

            cmd.Parameters.AddWithValue("@FKTaxSettingNo", mitemtaxinfo.FKTaxSettingNo);

            cmd.Parameters.AddWithValue("@UserID", mitemtaxinfo.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mitemtaxinfo.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mitemtaxinfo.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mitemtaxinfo.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }


        public bool AddMItemTaxInfo1(MItemTaxInfo mitemtaxinfo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemTaxInfo";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxinfo.PkSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", mitemtaxinfo.ItemNo);

            cmd.Parameters.AddWithValue("@TaxLedgerNo", mitemtaxinfo.TaxLedgerNo);

            cmd.Parameters.AddWithValue("@SalesLedgerNo", mitemtaxinfo.SalesLedgerNo);

            cmd.Parameters.AddWithValue("@FromDate", mitemtaxinfo.FromDate);

            cmd.Parameters.AddWithValue("@CalculationMethod", mitemtaxinfo.CalculationMethod);

            cmd.Parameters.AddWithValue("@Percentage", mitemtaxinfo.Percentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemtaxinfo.CompanyNo);

            cmd.Parameters.AddWithValue("@FKTaxSettingNo", mitemtaxinfo.FKTaxSettingNo);

            cmd.Parameters.AddWithValue("@UserID", mitemtaxinfo.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mitemtaxinfo.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mitemtaxinfo.ModifiedBy);
            commandcollection.Add(cmd);

            return true;
        }

        public bool DeleteMItemTaxInfo(MItemTaxInfo mitemtaxinfo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMItemTaxInfo";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxinfo.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mitemtaxinfo.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMItemTaxInfo()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemTaxInfo order by PkSrNo";
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

        public DataView GetMItemTaxInfoByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemTaxInfo where PkSrNo =" + ID;
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

        public MItemTaxInfo ModifyMItemTaxInfoByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MItemTaxInfo where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MItemTaxInfo MM = new MItemTaxInfo();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.TaxLedgerNo = Convert.ToInt64(dr["TaxLedgerNo"]);
                    if (!Convert.IsDBNull(dr["SalesTaxLedgerNo"])) MM.SalesLedgerNo = Convert.ToInt64(dr["SalesLedgerNo"]);
                    if (!Convert.IsDBNull(dr["FromDate"])) MM.FromDate = Convert.ToDateTime(dr["FromDate"]);
                    if (!Convert.IsDBNull(dr["CalculationMethod"])) MM.CalculationMethod = Convert.ToString(dr["CalculationMethod"]);
                    if (!Convert.IsDBNull(dr["Percentage"])) MM.Percentage = Convert.ToDouble(dr["Percentage"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MItemTaxInfo();
        }



        public bool ExecuteNonQueryStatements()
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
                        if (commandcollection[i].CommandText == "AddMItemTaxInfo1")
                        {
                            commandcollection[i].CommandText = "AddMItemTaxInfo1";
                        }
                        else  if (commandcollection[i] != null)
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

    /// <summary>
    /// This Class use for MDutiesTaxesInfo
    /// </summary>
    public class MDutiesTaxesInfo
    {
        private long mPkSrNo;
        private long mLedgerNo;
        private long mTaxOnLedgerNo;
        private DateTime mFromDate;
        private string mCalculationMethod;
        private double mPercentage;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkSrNo
        /// </summary>
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
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
        /// This Properties use for TaxOnLedgerNo
        /// </summary>
        public long TaxOnLedgerNo
        {
            get { return mTaxOnLedgerNo; }
            set { mTaxOnLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for FromDate
        /// </summary>
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        /// <summary>
        /// This Properties use for CalculationMethod
        /// </summary>
        public string CalculationMethod
        {
            get { return mCalculationMethod; }
            set { mCalculationMethod = value; }
        }
        /// <summary>
        /// This Properties use for Percentage
        /// </summary>
        public double Percentage
        {
            get { return mPercentage; }
            set { mPercentage = value; }
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
        /// This Properties use for UserID
        /// </summary>
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
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

    /// <summary>
    /// This Class use for MItemTaxInfo
    /// </summary>
    public class MItemTaxInfo1
    {
        private long mPkSrNo;
        private long mItemNo;
        private long mTaxLedgerNo;
        private long mSalesLedgerNo;
        private DateTime mFromDate;
        private string mCalculationMethod;
        private double mPercentage;
        private long mCompanyNo;
        private long mFKTaxSettingNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkSrNo
        /// </summary>
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        /// <summary>
        /// This Properties use for ItemNo
        /// </summary>
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        /// <summary>
        /// This Properties use for TaxLedgerNo
        /// </summary>
        public long TaxLedgerNo
        {
            get { return mTaxLedgerNo; }
            set { mTaxLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for SalesLedgerNo
        /// </summary>
        public long SalesLedgerNo
        {
            get { return mSalesLedgerNo; }
            set { mSalesLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for FromDate
        /// </summary>
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        /// <summary>
        /// This Properties use for CalculationMethod
        /// </summary>
        public string CalculationMethod
        {
            get { return mCalculationMethod; }
            set { mCalculationMethod = value; }
        }
        /// <summary>
        /// This Properties use for Percentage
        /// </summary>
        public double Percentage
        {
            get { return mPercentage; }
            set { mPercentage = value; }
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
        /// This Properties use for FKTaxSettingNo
        /// </summary>
        public long FKTaxSettingNo
        {
            get { return mFKTaxSettingNo; }
            set { mFKTaxSettingNo = value; }
        }
        /// <summary>
        /// This Properties use for UserID
        /// </summary>
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
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
