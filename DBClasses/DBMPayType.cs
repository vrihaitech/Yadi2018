using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBMPayType
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMPayType(MPayType mpaytype)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMPayType";

            cmd.Parameters.AddWithValue("@PKPayTypeNo", mpaytype.PKPayTypeNo);

            cmd.Parameters.AddWithValue("@PayTypeName", mpaytype.PayTypeName);

            cmd.Parameters.AddWithValue("@ShortName", mpaytype.ShortName);

            cmd.Parameters.AddWithValue("@IsActive", mpaytype.IsActive);

            cmd.Parameters.AddWithValue("@ControlUnder", mpaytype.ControlUnder);

            cmd.Parameters.AddWithValue("@CompanyNo", mpaytype.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mpaytype.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mpaytype.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMPayTypeLedger(MPayTypeLedger mpaytypeledger)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMPayTypeLedger";

            cmd.Parameters.AddWithValue("@PKPayTypeLedgerNo", mpaytypeledger.PKPayTypeLedgerNo);

           // cmd.Parameters.AddWithValue("@PayTypeNo", mpaytypeledger.PayTypeNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mpaytypeledger.LedgerNo);

            cmd.Parameters.AddWithValue("@CompanyNo", mpaytypeledger.CompanyNo);

            cmd.Parameters.AddWithValue("@ChargesPerce", mpaytypeledger.ChargesPerce);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMPayType(MPayType mpaytype)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMPayType";

            cmd.Parameters.AddWithValue("@PKPayTypeNo", mpaytype.PKPayTypeNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mpaytype.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMPayType()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPayType order by PKPayTypeNo";
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

        public DataView GetMPayTypeByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPayType where PKPayTypeNo =" + ID;
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

        public MPayType ModifyMPayTypeByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MPayType where PKPayTypeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MPayType MM = new MPayType();
                while (dr.Read())
                {
                    MM.PKPayTypeNo = Convert.ToInt32(dr["PKPayTypeNo"]);
                    if (!Convert.IsDBNull(dr["PayTypeName"])) MM.PayTypeName = Convert.ToString(dr["PayTypeName"]);
                    if (!Convert.IsDBNull(dr["ShortName"])) MM.ShortName = Convert.ToString(dr["ShortName"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["ControlUnder"])) MM.ControlUnder = Convert.ToInt32(dr["ControlUnder"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt32(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MPayType();
        }

        public bool AddMPayTypeDetails(MPayTypeDetails mpaytypedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMPayTypeDetails";

            cmd.Parameters.AddWithValue("@PKPayTypeDetailsNo", mpaytypedetails.PKPayTypeDetailsNo);

            cmd.Parameters.AddWithValue("@FKPayTypeNo", mpaytypedetails.FKPayTypeNo);

            cmd.Parameters.AddWithValue("@ColLabel", mpaytypedetails.ColLabel);

            cmd.Parameters.AddWithValue("@ColName", mpaytypedetails.ColName);

            cmd.Parameters.AddWithValue("@IsComplsory", mpaytypedetails.IsComplsory);

            cmd.Parameters.AddWithValue("@IsActive", mpaytypedetails.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mpaytypedetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mpaytypedetails.UserDate);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mpaytypedetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMPayTypeDetails(MPayTypeDetails mpaytypedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMPayTypeDetails";

            cmd.Parameters.AddWithValue("@PKPayTypeDetailsNo", mpaytypedetails.PKPayTypeDetailsNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mpaytypedetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMPayTypeDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPayTypeDetails order by PKPayTypeDetailsNo";
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

        public DataView GetMPayTypeDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPayTypeDetails where PKPayTypeDetailsNo =" + ID;
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

        public MPayTypeDetails ModifyMPayTypeDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MPayTypeDetails where PKPayTypeDetailsNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MPayTypeDetails MM = new MPayTypeDetails();
                while (dr.Read())
                {
                    MM.PKPayTypeDetailsNo = Convert.ToInt32(dr["PKPayTypeDetailsNo"]);
                    if (!Convert.IsDBNull(dr["FKPayTypeNo"])) MM.FKPayTypeNo = Convert.ToInt64(dr["FKPayTypeNo"]);
                    if (!Convert.IsDBNull(dr["ColLabel"])) MM.ColLabel = Convert.ToString(dr["ColLabel"]);
                    if (!Convert.IsDBNull(dr["ColName"])) MM.ColName = Convert.ToString(dr["ColName"]);
                    if (!Convert.IsDBNull(dr["IsComplsory"])) MM.IsComplsory = Convert.ToBoolean(dr["IsComplsory"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MPayTypeDetails();
        }

        public bool ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int PayNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddMPayType")
                        {
                            PayNo = i;
                        }

                        if (commandcollection[i].CommandText == "AddMPayTypeLedger")
                        {
                            commandcollection[i].Parameters.AddWithValue("@PayTypeNo", commandcollection[PayNo].Parameters["@ReturnID"].Value);
                        }
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

    /// <summary>
    /// This Class use for MPayType
    /// </summary>
    public class MPayType
    {
        private long mPKPayTypeNo;
        private string mPayTypeName;
        private string mShortName;
        private string mDisplayName;
        private bool mIsActive;
        private long mControlUnder;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PKPayTypeNo
        /// </summary>
        public long PKPayTypeNo
        {
            get { return mPKPayTypeNo; }
            set { mPKPayTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for PayTypeName
        /// </summary>
        public string PayTypeName
        {
            get { return mPayTypeName; }
            set { mPayTypeName = value; }
        }
        /// <summary>
        /// This Properties use for ShortName
        /// </summary>
        public string ShortName
        {
            get { return mShortName; }
            set { mShortName = value; }
        }
        /// <summary>
        /// This Properties use for DisplayName
        /// </summary>
        public string DisplayName
        {
            get { return mDisplayName; }
            set { mDisplayName = value; }
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
        /// This Properties use for ControlUnder
        /// </summary>
        public long ControlUnder
        {
            get { return mControlUnder; }
            set { mControlUnder = value; }
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
        /// This Properties use for CompanyNo
        /// </summary>
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
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
    /// This Class use for MPayTypeDetails
    /// </summary>
    public class MPayTypeDetails
    {
        private long mPKPayTypeDetailsNo;
        private long mFKPayTypeNo;
        private string mColLabel;
        private string mColName;
        private bool mIsComplsory;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PKPayTypeDetailsNo
        /// </summary>
        public long PKPayTypeDetailsNo
        {
            get { return mPKPayTypeDetailsNo; }
            set { mPKPayTypeDetailsNo = value; }
        }
        /// <summary>
        /// This Properties use for FKPayTypeNo
        /// </summary>
        public long FKPayTypeNo
        {
            get { return mFKPayTypeNo; }
            set { mFKPayTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for ColLabel
        /// </summary>
        public string ColLabel
        {
            get { return mColLabel; }
            set { mColLabel = value; }
        }
        /// <summary>
        /// This Properties use for ColName
        /// </summary>
        public string ColName
        {
            get { return mColName; }
            set { mColName = value; }
        }
        /// <summary>
        /// This Properties use for IsComplsory
        /// </summary>
        public bool IsComplsory
        {
            get { return mIsComplsory; }
            set { mIsComplsory = value; }
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
