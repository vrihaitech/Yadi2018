using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using OMControls;

namespace OM
{
    class DBMUser
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        string strerrormsg;
        public static int count;
        private SqlCommand[] commandcollection;

        public bool AddMUser(MUser muser, int cnt)
        {
            commandcollection = new SqlCommand[cnt];
            count = 0;

            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMUser";

            cmd.Parameters.AddWithValue("@UserCode", muser.UserCode);

            cmd.Parameters.AddWithValue("@UsersUserCode", muser.UsersUserCode);

            cmd.Parameters.AddWithValue("@UserName", muser.UserName);

            cmd.Parameters.AddWithValue("@Password", muser.Password);

            cmd.Parameters.AddWithValue("@UserAddress", muser.UserAddress);

            cmd.Parameters.AddWithValue("@PhoneNo", muser.PhoneNo);

            cmd.Parameters.AddWithValue("@UserType", muser.UserType);

            cmd.Parameters.AddWithValue("@CityCode", muser.CityCode);

            cmd.Parameters.AddWithValue("@IsClose", muser.IsClose); 

            cmd.Parameters.AddWithValue("@FkAccYearNo", muser.FkAccYearNo);

            cmd.Parameters.AddWithValue("@FkCompanyNo", muser.FkCompanyNo);

            cmd.Parameters.AddWithValue("@FkLocationNo", muser.FkLocationNo);

            SqlParameter outparameter = new SqlParameter();
            outparameter.ParameterName = "@ReturnID";
            outparameter.Direction = ParameterDirection.Output;
            outparameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outparameter);

            commandcollection[count] = cmd;
            count = count + 1;
            return true;

            
        }

        public bool AddMUserMenuMaster(MUserMenuMaster musermenumaster)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMUserMenuMaster";

            cmd.Parameters.AddWithValue("@PKSrNo", musermenumaster.PKSrNo);

         //   cmd.Parameters.AddWithValue("@FKUserId", musermenumaster.FKUserId);

            cmd.Parameters.AddWithValue("@Master", musermenumaster.Master);

            cmd.Parameters.AddWithValue("@Sales", musermenumaster.Sales);

            cmd.Parameters.AddWithValue("@Purchase", musermenumaster.Purchase);

            cmd.Parameters.AddWithValue("@Accounts", musermenumaster.Accounts);

            cmd.Parameters.AddWithValue("@Reports", musermenumaster.Reports);

            cmd.Parameters.AddWithValue("@Settings", musermenumaster.Settings);

            cmd.Parameters.AddWithValue("@Utilities", musermenumaster.Utilities);

            cmd.Parameters.AddWithValue("@Hidden", musermenumaster.Hidden);

            cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);
        
            commandcollection[count] = cmd;
            count = count + 1;
            return true;
      
        }
        
        public bool DeleteMUser(MUser muser)
        {

            SqlCommand cmd = null;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMUser";

            cmd.Parameters.AddWithValue("@UserCode", muser.UserCode);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                muser.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMUser()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MUser order by UserCode";
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

        public DataView GetMUserByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MUser where UserCode =" + ID;
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

        public MUser ModifyMUserByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MUser where UserCode =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MUser MM = new MUser();
                while (dr.Read())
                {
                    MM.UserCode = Convert.ToInt32(dr["UserCode"]);
                    if (!Convert.IsDBNull(dr["UsersUserCode"])) MM.UsersUserCode = Convert.ToString(dr["UsersUserCode"]);
                    if (!Convert.IsDBNull(dr["UserName"])) MM.UserName = Convert.ToString(dr["UserName"]);
                    if (!Convert.IsDBNull(dr["Password"])) MM.Password = Convert.ToString(dr["Password"]);
                    if (!Convert.IsDBNull(dr["UserAddress"])) MM.UserAddress = Convert.ToString(dr["UserAddress"]);
                    if (!Convert.IsDBNull(dr["PhoneNo"])) MM.PhoneNo = Convert.ToString(dr["PhoneNo"]);
                    if (!Convert.IsDBNull(dr["UserType"])) MM.UserType = Convert.ToInt32(dr["UserType"]);
                    if (!Convert.IsDBNull(dr["CityCode"])) MM.CityCode = Convert.ToInt64(dr["CityCode"]);
                    if (!Convert.IsDBNull(dr["IsClose"])) MM.IsClose = Convert.ToInt32(dr["IsClose"]);
                    if (!Convert.IsDBNull(dr["FkAccYearNo"])) MM.FkAccYearNo = Convert.ToInt64(dr["FkAccYearNo"]);
                    if (!Convert.IsDBNull(dr["FkCompanyNo"])) MM.FkCompanyNo = Convert.ToInt64(dr["FkCompanyNo"]);
                    if (!Convert.IsDBNull(dr["FkLocationNo"])) MM.FkLocationNo = Convert.ToInt64(dr["FkLocationNo"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MUser();
        }

        public DataView GetNodesByNodeID(long NodeID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select SrNo,MenuID,MenuName,ShortCutKey From MMenuMaster where IsAllow=1 AND ControlMenu=" + NodeID + " order by MenuID";
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

        public bool ExecuteNonQueryStatements()
        {
            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();

            try
            {
                for (int i = 0; i < this.commandcollection.Length; i++)
                {
                    if (this.commandcollection[i] != null)
                    {
                        ((SqlCommand)(this.commandcollection[i])).Connection = cn;
                        ((SqlCommand)(this.commandcollection[i])).Transaction = myTrans;
                        if (((SqlCommand)(this.commandcollection[i])).CommandText == "AddMUserMenuMaster")
                        {
                            ((SqlCommand)(this.commandcollection[i])).Parameters.AddWithValue("@FKUserId", ((SqlCommand)(this.commandcollection[0])).Parameters["@ReturnID"].Value);
                        }
                        ((SqlCommand)(this.commandcollection[i])).ExecuteNonQuery();
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
            { cn.Close(); }
        }
    }

    /// <summary>
    /// This Class use for MUser
    /// </summary>
    public class MUser
    {
        private long mUserCode;
        private string mUsersUserCode;
        private string mUserName;
        private string mPassword;
        private string mUserAddress;
        private string mPhoneNo;
        private int mUserType;
        private long mCityCode;
        private int mIsClose;
        private long mFkAccYearNo;
        private long mFkCompanyNo;
        private long mFkLocationNo;
        private string mModifiedBy;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for UserCode
        /// </summary>
        public long UserCode
        {
            get { return mUserCode; }
            set { mUserCode = value; }
        }
        /// <summary>
        /// This Properties use for UsersUserCode
        /// </summary>
        public string UsersUserCode
        {
            get { return mUsersUserCode; }
            set { mUsersUserCode = value; }
        }
        /// <summary>
        /// This Properties use for UserName
        /// </summary>
        public string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }
        /// <summary>
        /// This Properties use for Password
        /// </summary>
        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }
        /// <summary>
        /// This Properties use for UserAddress
        /// </summary>
        public string UserAddress
        {
            get { return mUserAddress; }
            set { mUserAddress = value; }
        }
        /// <summary>
        /// This Properties use for PhoneNo
        /// </summary>
        public string PhoneNo
        {
            get { return mPhoneNo; }
            set { mPhoneNo = value; }
        }
        /// <summary>
        /// This Properties use for UserType
        /// </summary>
        public int UserType
        {
            get { return mUserType; }
            set { mUserType = value; }
        }
        /// <summary>
        /// This Properties use for CityCode
        /// </summary>
        public long CityCode
        {
            get { return mCityCode; }
            set { mCityCode = value; }
        }
        /// <summary>
        /// This Properties use for IsClose
        /// </summary>
        public int IsClose
        {
            get { return mIsClose; }
            set { mIsClose = value; }
        }
        /// <summary>
        /// This Properties use for FkAccYearNo
        /// </summary>
        public long FkAccYearNo
        {
            get { return mFkAccYearNo; }
            set { mFkAccYearNo = value; }
        }
        /// <summary>
        /// This Properties use for FkCompanyNo
        /// </summary>
        public long FkCompanyNo
        {
            get { return mFkCompanyNo; }
            set { mFkCompanyNo = value; }
        }
        /// <summary>
        /// This Properties use for FkLocationNo
        /// </summary>
        public long FkLocationNo
        {
            get { return mFkLocationNo; }
            set { mFkLocationNo = value; }
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
    /// This Class use for MUserMenuMaster
    /// </summary>
    public class MUserMenuMaster
    {
        private long mPKSrNo;
        private long mFKUserId;
        private string mMaster;
        private string mSales;
        private string mPurchase;
        private string mAccounts;
        private string mReports;
        private string mSettings;
        private string mUtilities;
        private string mHidden;
        private int mStatusNo;
        private long mCompanyNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PKSrNo
        /// </summary>
        public long PKSrNo
        {
            get { return mPKSrNo; }
            set { mPKSrNo = value; }
        }
        /// <summary>
        /// This Properties use for FKUserId
        /// </summary>
        public long FKUserId
        {
            get { return mFKUserId; }
            set { mFKUserId = value; }
        }
        /// <summary>
        /// This Properties use for Master
        /// </summary>
        public string Master
        {
            get { return mMaster; }
            set { mMaster = value; }
        }
        /// <summary>
        /// This Properties use for Sales
        /// </summary>
        public string Sales
        {
            get { return mSales; }
            set { mSales = value; }
        }
        /// <summary>
        /// This Properties use for Purchase
        /// </summary>
        public string Purchase
        {
            get { return mPurchase; }
            set { mPurchase = value; }
        }
        /// <summary>
        /// This Properties use for Accounts
        /// </summary>
        public string Accounts
        {
            get { return mAccounts; }
            set { mAccounts = value; }
        }
        /// <summary>
        /// This Properties use for Reports
        /// </summary>
        public string Reports
        {
            get { return mReports; }
            set { mReports = value; }
        }
        /// <summary>
        /// This Properties use for Settings
        /// </summary>
        public string Settings
        {
            get { return mSettings; }
            set { mSettings = value; }
        }
        /// <summary>
        /// This Properties use for Utilities
        /// </summary>
        public string Utilities
        {
            get { return mUtilities; }
            set { mUtilities = value; }
        }
        /// <summary>
        /// This Properties use for Hidden
        /// </summary>
        public string Hidden
        {
            get { return mHidden; }
            set { mHidden = value; }
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