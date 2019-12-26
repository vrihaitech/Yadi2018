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
    class DBMFooterDiscount
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public CommandCollection commandcollection = new CommandCollection();

        DataTable dtId = new DataTable();

        public static string strerrormsg;

        public bool AddMFooterDiscount(MFooterDiscount mfooterdiscount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMFooterDiscount";

            cmd.Parameters.AddWithValue("@PkSrNo", mfooterdiscount.PkSrNo);

            cmd.Parameters.AddWithValue("@FooterDiscUserNo", mfooterdiscount.FooterDiscUserNo);

            cmd.Parameters.AddWithValue("@FooterDiscDate", mfooterdiscount.FooterDiscDate);

            cmd.Parameters.AddWithValue("@PeriodFrom", mfooterdiscount.PeriodFrom);

            cmd.Parameters.AddWithValue("@PeriodTo", mfooterdiscount.PeriodTo);

            cmd.Parameters.AddWithValue("@IsActive", mfooterdiscount.IsActive);

            cmd.Parameters.AddWithValue("@DiscType", mfooterdiscount.DiscType);

            cmd.Parameters.AddWithValue("@CompanyNo", mfooterdiscount.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mfooterdiscount.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mfooterdiscount.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMFooterDiscount(MFooterDiscount mfooterdiscount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMFooterDiscount";

            cmd.Parameters.AddWithValue("@PkSrNo", mfooterdiscount.PkSrNo);
            cmd.Parameters.AddWithValue("@IsActive", mfooterdiscount.IsActive);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mfooterdiscount.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MFooterDiscount ModifyMFooterDiscountByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MFooterDiscount where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MFooterDiscount MM = new MFooterDiscount();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["FooterDiscUserNo"])) MM.FooterDiscUserNo = Convert.ToInt64(dr["FooterDiscUserNo"]);
                    if (!Convert.IsDBNull(dr["FooterDiscDate"])) MM.FooterDiscDate = Convert.ToDateTime(dr["FooterDiscDate"]);
                    if (!Convert.IsDBNull(dr["PeriodFrom"])) MM.PeriodFrom = Convert.ToDateTime(dr["PeriodFrom"]);
                    if (!Convert.IsDBNull(dr["PeriodTo"])) MM.PeriodTo = Convert.ToDateTime(dr["PeriodTo"]);
                    if (!Convert.IsDBNull(dr["DiscType"])) MM.DiscType = Convert.ToInt64(dr["DiscType"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToInt32(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MFooterDiscount();
        }

        public DataView GetAllMFooterDiscount()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MFooterDiscount order by PkSrNo";
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

        public DataView GetMFooterDiscountByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MFooterDiscount where PkSrNo =" + ID;
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

        public bool AddMFooterDiscountDetails(MFooterDiscountDetails mfooterdiscountdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMFooterDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mfooterdiscountdetails.PkSrNo);

            //  cmd.Parameters.AddWithValue("@FooterDiscNo", mfooterdiscountdetails.FooterDiscNo);

            cmd.Parameters.AddWithValue("@Amount", mfooterdiscountdetails.Amount);

            cmd.Parameters.AddWithValue("@DiscPercentage", mfooterdiscountdetails.DiscPercentage);

            cmd.Parameters.AddWithValue("@DiscAmount", mfooterdiscountdetails.DiscAmount);

            cmd.Parameters.AddWithValue("@CompanyNo", mfooterdiscountdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mfooterdiscountdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mfooterdiscountdetails.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMFooterDiscountDetails(MFooterDiscountDetails mfooterdiscountDetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMFooterDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mfooterdiscountDetails.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mfooterdiscountDetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MFooterDiscountDetails ModifyMFooterDiscountDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MFooterDiscountDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MFooterDiscountDetails MM = new MFooterDiscountDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["FooterDiscNo"])) MM.FooterDiscNo = Convert.ToInt64(dr["FooterDiscNo"]);
                    if (!Convert.IsDBNull(dr["Amount"])) MM.Amount = Convert.ToDouble(dr["Amount"]);
                    if (!Convert.IsDBNull(dr["DiscPercentage"])) MM.DiscPercentage = Convert.ToDouble(dr["DiscPercentage"]);
                    if (!Convert.IsDBNull(dr["DiscAmount"])) MM.DiscAmount = Convert.ToDouble(dr["DiscAmount"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MFooterDiscountDetails();
        }

        public DataView GetAllMFooterDiscountDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MFooterDiscountDetails order by PkSrNo";
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

        public DataView GetMFooterDiscountDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MFooterDiscountDetails where PkSrNo =" + ID;
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

        public bool AddMFooterDiscountPayTypeDetails(MFooterDiscountPayTypeDetails mfooterdiscountpaytypedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMFooterDiscountPayTypeDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mfooterdiscountpaytypedetails.PkSrNo);

        //    cmd.Parameters.AddWithValue("@FooterDiscNo", mfooterdiscountpaytypedetails.FooterDiscNo);

            cmd.Parameters.AddWithValue("@PayTypeNo", mfooterdiscountpaytypedetails.PayTypeNo);

            cmd.Parameters.AddWithValue("@IsActive", mfooterdiscountpaytypedetails.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mfooterdiscountpaytypedetails.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMFooterDiscountPayTypeDetails(MFooterDiscountPayTypeDetails mfooterdiscountpaytypedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMFooterDiscountPayTypeDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mfooterdiscountpaytypedetails.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mfooterdiscountpaytypedetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MFooterDiscountPayTypeDetails ModifyMFooterDiscountPayTypeDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MFooterDiscountPayTypeDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MFooterDiscountPayTypeDetails MM = new MFooterDiscountPayTypeDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["FooterDiscNo"])) MM.FooterDiscNo = Convert.ToInt64(dr["FooterDiscNo"]);
                    if (!Convert.IsDBNull(dr["PayTypeNo"])) MM.PayTypeNo = Convert.ToInt64(dr["PayTypeNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MFooterDiscountPayTypeDetails();
        }

        public DataView GetAllMFooterDiscountPayTypeDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MFooterDiscountPayTypeDetails order by PkSrNo";
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

        public DataView GetMFooterDiscountPayTypeDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MFooterDiscountPayTypeDetails where PkSrNo =" + ID;
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


        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select PkSrNo,FooterDiscUserNo As DocNo,FooterDiscDate As Date,PeriodFrom,PeriodTo,Case When DiscType=1 Then 'Percent' Else 'Rupees' End As Type,Case When IsActive=0 then 'Draft' Else Case When IsActive=1 Then 'Active' Else 'Closed' End End AS Status from MFooterDiscount ";
                    break;
                case "FooterDiscUserNo":
                    sql = "Select PkSrNo,FooterDiscUserNo As DocNo,FooterDiscDate As Date,PeriodFrom,PeriodTo,Case When DiscType=1 Then 'Percent' Else 'Rupees' End As Type,Case When IsActive=0 then 'Draft' Else Case When IsActive=1 Then 'Active' Else 'Closed' End End AS Status from MFooterDiscount where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
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

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            //long ItemNoTemp = 0, BarcodeNoTemp = 0;
            //int cntref = 0;

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

                        if (commandcollection[i].CommandText == "AddMFooterDiscountDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FooterDiscNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMFooterDiscountPayTypeDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FooterDiscNo", commandcollection[0].Parameters["@ReturnID"].Value);
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
            //________________________________________________________________________________________________________________________________________________________________________________________________________________________
        }
    }

    /// <summary>
    /// This Class use for MFooterDiscount
    /// </summary>
    public class MFooterDiscount
    {
        private long mPkSrNo;
        private long mFooterDiscUserNo;
        private DateTime mFooterDiscDate;
        private DateTime mPeriodFrom;
        private DateTime mPeriodTo;
        private long mDiscType;
        private long mStatusNo;
        private int mIsActive;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
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
        /// This Properties use for FooterDiscUserNo
        /// </summary>
        public long FooterDiscUserNo
        {
            get { return mFooterDiscUserNo; }
            set { mFooterDiscUserNo = value; }
        }
        /// <summary>
        /// This Properties use for FooterDiscDate
        /// </summary>
        public DateTime FooterDiscDate
        {
            get { return mFooterDiscDate; }
            set { mFooterDiscDate = value; }
        }
        /// <summary>
        /// This Properties use for PeriodFrom
        /// </summary>
        public DateTime PeriodFrom
        {
            get { return mPeriodFrom; }
            set { mPeriodFrom = value; }
        }
        /// <summary>
        /// This Properties use for PeriodTo
        /// </summary>
        public DateTime PeriodTo
        {
            get { return mPeriodTo; }
            set { mPeriodTo = value; }
        }
        /// <summary>
        /// This Properties use for DiscType
        /// </summary>
        public long DiscType
        {
            get { return mDiscType; }
            set { mDiscType = value; }
        }
        /// <summary>
        /// This Properties use for StatusNo
        /// </summary>
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        /// <summary>
        /// This Properties use for IsActive
        /// </summary>
        public int IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    /// <summary>
    /// This Class use for MFooterDiscountDetails
    /// </summary>
    public class MFooterDiscountDetails
    {
        private long mPkSrNo;
        private long mFooterDiscNo;
        private double mAmount;
        private double mDiscPercentage;
        private double mDiscAmount;
        private long mStatusNo;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
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
        /// This Properties use for FooterDiscNo
        /// </summary>
        public long FooterDiscNo
        {
            get { return mFooterDiscNo; }
            set { mFooterDiscNo = value; }
        }
        /// <summary>
        /// This Properties use for Amount
        /// </summary>
        public double Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        /// <summary>
        /// This Properties use for DiscPercentage
        /// </summary>
        public double DiscPercentage
        {
            get { return mDiscPercentage; }
            set { mDiscPercentage = value; }
        }
        /// <summary>
        /// This Properties use for DiscAmount
        /// </summary>
        public double DiscAmount
        {
            get { return mDiscAmount; }
            set { mDiscAmount = value; }
        }
        /// <summary>
        /// This Properties use for StatusNo
        /// </summary>
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    /// <summary>
    /// This Class use for MFooterDiscountPayTypeDetails
    /// </summary>
    public class MFooterDiscountPayTypeDetails
    {
        private long mPkSrNo;
        private long mFooterDiscNo;
        private long mPayTypeNo;
        private bool mIsActive;
        private long mStatusNo;
        private long mCompanyNo;
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
        /// This Properties use for FooterDiscNo
        /// </summary>
        public long FooterDiscNo
        {
            get { return mFooterDiscNo; }
            set { mFooterDiscNo = value; }
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
        /// This Properties use for IsActive
        /// </summary>
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }
        /// <summary>
        /// This Properties use for StatusNo
        /// </summary>
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }


}

