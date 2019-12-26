using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMBillDiscount
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();

        public static string strerrormsg;

        public bool AddMItemDiscount(MItemDiscount mitemdiscount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemDiscount";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemdiscount.PkSrNo);

            cmd.Parameters.AddWithValue("@DiscUserNo", mitemdiscount.DiscUserNo);

            cmd.Parameters.AddWithValue("@MfgCompanyNo", mitemdiscount.MfgCompanyNo);

            cmd.Parameters.AddWithValue("@DiscDate", mitemdiscount.DiscDate);

            cmd.Parameters.AddWithValue("@PeriodFrom", mitemdiscount.PeriodFrom);

            cmd.Parameters.AddWithValue("@PeriodTo", mitemdiscount.PeriodTo);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemdiscount.CompanyNo);

            cmd.Parameters.AddWithValue("@IsActive", mitemdiscount.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mitemdiscount.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mitemdiscount.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;

        }

        public bool DeleteMItemDiscount(MItemDiscount mitemdiscount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMItemDiscount";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemdiscount.PkSrNo);
            cmd.Parameters.AddWithValue("@IsActive", mitemdiscount.IsActive);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mitemdiscount.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MItemDiscount ModifyMItemDiscountByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MItemDiscount where CompanyNo="+DBGetVal.FirmNo+" And PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MItemDiscount MM = new MItemDiscount();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["DiscUserNo"])) MM.DiscUserNo = Convert.ToInt64(dr["DiscUserNo"]);
                    if (!Convert.IsDBNull(dr["MfgCompanyNo"])) MM.MfgCompanyNo = Convert.ToInt64(dr["MfgCompanyNo"]);
                    if (!Convert.IsDBNull(dr["DiscDate"])) MM.DiscDate = Convert.ToDateTime(dr["DiscDate"]);
                    if (!Convert.IsDBNull(dr["PeriodFrom"])) MM.PeriodFrom = Convert.ToDateTime(dr["PeriodFrom"]);
                    if (!Convert.IsDBNull(dr["PeriodTo"])) MM.PeriodTo = Convert.ToDateTime(dr["PeriodTo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToInt32(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MItemDiscount();
        }

        public DataView GetAllMItemDiscount()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemDiscount Where CompanyNo=" + DBGetVal.FirmNo + "  order by PkSrNo";
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

        public DataView GetMItemDiscountByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemDiscount where CompanyNo=" + DBGetVal.FirmNo + " And  PkSrNo =" + ID;
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


        public bool AddMItemBrandDiscount(MItemBrandDiscount mitembranddiscount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemBrandDiscount";

            cmd.Parameters.AddWithValue("@PkSrNo", mitembranddiscount.PkSrNo);

            //   cmd.Parameters.AddWithValue("@ItemDiscNo", mitembranddiscount.ItemDiscNo);

            cmd.Parameters.AddWithValue("@StockGroupNo", mitembranddiscount.StockGroupNo);

            cmd.Parameters.AddWithValue("@DiscPercentage", mitembranddiscount.DiscPercentage);

            cmd.Parameters.AddWithValue("@IsActive", mitembranddiscount.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mitembranddiscount.CompanyNo);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMItemBrandDiscount(MItemBrandDiscount mitembranddiscount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMItemBrandDiscount";

            cmd.Parameters.AddWithValue("@PkSrNo", mitembranddiscount.PkSrNo);


            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MItemBrandDiscount ModifyMItemBrandDiscountByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MItemBrandDiscount where CompanyNo=" + DBGetVal.FirmNo + " And PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MItemBrandDiscount MM = new MItemBrandDiscount();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["ItemDiscNo"])) MM.ItemDiscNo = Convert.ToInt64(dr["ItemDiscNo"]);
                    if (!Convert.IsDBNull(dr["StockGroupNo"])) MM.StockGroupNo = Convert.ToInt64(dr["StockGroupNo"]);
                    if (!Convert.IsDBNull(dr["DiscPercentage"])) MM.DiscPercentage = Convert.ToInt64(dr["DiscPercentage"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MItemBrandDiscount();
        }

        public DataView GetAllMItemBrandDiscount()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemBrandDiscount Where CompanyNo=" + DBGetVal.FirmNo + "  order by PkSrNo";
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

        public DataView GetMItemBrandDiscountByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemBrandDiscount where CompanyNo=" + DBGetVal.FirmNo + " And PkSrNo =" + ID;
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


        public bool AddMItemDiscountDetails(MItemDiscountDetails mitemdiscountdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemdiscountdetails.PkSrNo);

       //     cmd.Parameters.AddWithValue("@ItemBrandDiscNo", mitemdiscountdetails.ItemBrandDiscNo);

            cmd.Parameters.AddWithValue("@ItemNo", mitemdiscountdetails.ItemNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", mitemdiscountdetails.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@DiscPercentage", mitemdiscountdetails.DiscPercentage);

            cmd.Parameters.AddWithValue("@MRP", mitemdiscountdetails.MRP);

            cmd.Parameters.AddWithValue("@IsActive", mitemdiscountdetails.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemdiscountdetails.CompanyNo);


            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMItemDiscountDetails(MItemDiscountDetails mitemdiscountdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMItemDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemdiscountdetails.PkSrNo);


            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MItemDiscountDetails ModifyMItemDiscountDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MItemDiscountDetails where CompanyNo=" + DBGetVal.FirmNo + " And PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MItemDiscountDetails MM = new MItemDiscountDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["ItemBrandDiscNo"])) MM.ItemBrandDiscNo = Convert.ToInt64(dr["ItemBrandDiscNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["FkRateSettingNo"])) MM.FkRateSettingNo = Convert.ToInt64(dr["FkRateSettingNo"]);
                    if (!Convert.IsDBNull(dr["DiscPercentage"])) MM.DiscPercentage = Convert.ToDouble(dr["DiscPercentage"]);
                    if (!Convert.IsDBNull(dr["MRP"])) MM.MRP = Convert.ToDouble(dr["MRP"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MItemDiscountDetails();
        }

        public DataView GetAllMItemDiscountDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemDiscountDetails Where CompanyNo=" + DBGetVal.FirmNo + "  order by PkSrNo";
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

        public DataView GetMItemDiscountDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MItemDiscountDetails where  CompanyNo=" + DBGetVal.FirmNo + " And PkSrNo =" + ID;
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

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            int ItemBrandNo = 0;//, BarcodeNoTemp = 0;
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

                        if (commandcollection[i].CommandText == "AddMItemBrandDiscount")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemDiscNo", commandcollection[0].Parameters["@ReturnID"].Value);
                            ItemBrandNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddMItemDiscountDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemBrandDiscNo", commandcollection[ItemBrandNo].Parameters["@ReturnID"].Value);
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

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "SELECT  MItemDiscount.PkSrNo, MItemDiscount.DiscUserNo, MManufacturerCompany.MfgCompName, MItemDiscount.DiscDate, MItemDiscount.PeriodFrom, MItemDiscount.PeriodTo,  " +
                        " CASE WHEN MItemDiscount.IsActive = 0 THEN 'Draft' ELSE CASE WHEN MItemDiscount.IsActive = 1 THEN 'Active' ELSE 'Closed' END END AS Status FROM  MItemDiscount INNER JOIN " +
                        " MManufacturerCompany ON MItemDiscount.MfgCompanyNo = MManufacturerCompany.MfgCompNo ";
                    break;
                case "DiscUserNo":
                    sql = "SELECT  MItemDiscount.PkSrNo, MItemDiscount.DiscUserNo, MManufacturerCompany.MfgCompName, MItemDiscount.DiscDate, MItemDiscount.PeriodFrom, MItemDiscount.PeriodTo,  " +
                        " CASE WHEN MItemDiscount.IsActive = 0 THEN 'Draft' ELSE CASE WHEN MItemDiscount.IsActive = 1 THEN 'Active' ELSE 'Closed' END END AS Status FROM  MItemDiscount INNER JOIN " +
                        " MManufacturerCompany ON MItemDiscount.MfgCompanyNo = MManufacturerCompany.MfgCompNo  where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' ";
                    break;
                case "MfgCompName":
                    sql = "SELECT  MItemDiscount.PkSrNo, MItemDiscount.DiscUserNo, MManufacturerCompany.MfgCompName, MItemDiscount.DiscDate, MItemDiscount.PeriodFrom, MItemDiscount.PeriodTo,  " +
                        " CASE WHEN MItemDiscount.IsActive = 0 THEN 'Draft' ELSE CASE WHEN MItemDiscount.IsActive = 1 THEN 'Active' ELSE 'Closed' END END AS Status FROM  MItemDiscount INNER JOIN " +
                        " MManufacturerCompany ON MItemDiscount.MfgCompanyNo = MManufacturerCompany.MfgCompNo where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' ";
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

    /// <summary>
    /// This Class use for MItemDiscount
    /// </summary>
    public class MItemDiscount
    {
        private long mPkSrNo;
        private long mDiscUserNo;
        private long mMfgCompanyNo;
        private DateTime mDiscDate;
        private DateTime mPeriodFrom;
        private DateTime mPeriodTo;
        private long mStatusNo;
        private long mCompanyNo;
        private int mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
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
        /// This Properties use for DiscUserNo
        /// </summary>
        public long DiscUserNo
        {
            get { return mDiscUserNo; }
            set { mDiscUserNo = value; }
        }
        /// <summary>
        /// This Properties use for MfgCompanyNo
        /// </summary>
        public long MfgCompanyNo
        {
            get { return mMfgCompanyNo; }
            set { mMfgCompanyNo = value; }
        }
        /// <summary>
        /// This Properties use for DiscDate
        /// </summary>
        public DateTime DiscDate
        {
            get { return mDiscDate; }
            set { mDiscDate = value; }
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
        /// This Properties use for IsActive
        /// </summary>
        public int IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    /// <summary>
    /// This Class use for MItemBrandDiscount
    /// </summary>
    public class MItemBrandDiscount
    {
        private long mPkSrNo;
        private long mItemDiscNo;
        private long mStockGroupNo;
        private double mDiscPercentage;
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
        /// This Properties use for ItemDiscNo
        /// </summary>
        public long ItemDiscNo
        {
            get { return mItemDiscNo; }
            set { mItemDiscNo = value; }
        }
        /// <summary>
        /// This Properties use for StockGroupNo
        /// </summary>
        public long StockGroupNo
        {
            get { return mStockGroupNo; }
            set { mStockGroupNo = value; }
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

    /// <summary>
    /// This Class use for MItemDiscountDetails
    /// </summary>
    public class MItemDiscountDetails
    {
        private long mPkSrNo;
        private long mItemBrandDiscNo;
        private long mItemNo;
        private long mFkRateSettingNo;
        private double mDiscPercentage;
        private double mMRP;
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
        /// This Properties use for ItemBrandDiscNo
        /// </summary>
        public long ItemBrandDiscNo
        {
            get { return mItemBrandDiscNo; }
            set { mItemBrandDiscNo = value; }
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
        /// This Properties use for FkRateSettingNo
        /// </summary>
        public long FkRateSettingNo
        {
            get { return mFkRateSettingNo; }
            set { mFkRateSettingNo = value; }
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
        /// This Properties use for MRP
        /// </summary>
        public double MRP
        {
            get { return mMRP; }
            set { mMRP = value; }
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
