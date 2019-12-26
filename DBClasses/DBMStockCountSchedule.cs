using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMStockCountSchedule
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();

        string strerrormsg;

        public bool AddMStockCountSchedule(MStockCountSchedule mstockcountschedule)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockCountSchedule";

            cmd.Parameters.AddWithValue("@PkSrNo", mstockcountschedule.PkSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", mstockcountschedule.ItemNo);

            cmd.Parameters.AddWithValue("@CountTypeNo", mstockcountschedule.CountTypeNo);

            cmd.Parameters.AddWithValue("@CountScheduleDate", mstockcountschedule.CountScheduleDate);

            cmd.Parameters.AddWithValue("@IsActive", mstockcountschedule.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockcountschedule.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mstockcountschedule.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mstockcountschedule.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public MStockCountSchedule ModifyMStockCountScheduleByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MStockCountSchedule where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MStockCountSchedule MM = new MStockCountSchedule();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["CountTypeNo"])) MM.CountTypeNo = Convert.ToInt64(dr["CountTypeNo"]);
                    if (!Convert.IsDBNull(dr["CountScheduleDate"])) MM.CountScheduleDate = Convert.ToDateTime(dr["CountScheduleDate"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MStockCountSchedule();
        }

        public bool DeleteMStockCountSchedule(MStockCountSchedule mstockcountschedule)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMStockCountSchedule";

            cmd.Parameters.AddWithValue("@PkSrNo", mstockcountschedule.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstockcountschedule.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMStockCountSchedule()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockCountSchedule order by PkSrNo";
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

        public DataView GetMStockCountScheduleByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockCountSchedule where PkSrNo =" + ID;
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

        public int GetWeek(string MMM)
        {
            int Type = 0;
            switch (MMM)
            {
                case "Sunday":
                    Type = 0;
                    break;
                case "Monday":
                    Type = 1;
                    break;
                case "Tuesday":
                    Type = 2;
                    break;
                case "Wednesday":
                    Type = 3;
                    break;
                case "Thursday":
                    Type = 4;
                    break;
                case "Friday":
                    Type = 5;
                    break;
                case "Saturday":
                    Type = 6;
                    break;
            }
            return Type;
        }

        public DateTime SetStockCountType(long CountTypeNo, string value)
        {
            if (StockCountType.Weekly == CountTypeNo)
            {
                DateTime dt = DBGetVal.ServerTime;
                for (int i = 1; i <= 7; i++)
                {
                    if (((int)Convert.ToDateTime(i + "-" + "Jan-" + DBGetVal.ServerTime.Year.ToString()).DayOfWeek) == Convert.ToInt16(value))
                    {
                        dt = Convert.ToDateTime(i + "-" + "Jan" + "-" + DBGetVal.ServerTime.Year.ToString());
                        break;
                    }
                }
                return dt;
            }
            else if (StockCountType.Monthly == CountTypeNo)
            {
                return Convert.ToDateTime(value + "-" + "Jan" + "-" + DBGetVal.ServerTime.Year.ToString());
            }
            else if (StockCountType.Yearly == CountTypeNo)
            {
                return Convert.ToDateTime(value + "-" + DBGetVal.ServerTime.Year.ToString());
            }
            else
            {
                return DBGetVal.ServerTime;
            }
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
    /// This Class use for MStockCountSchedule
    /// </summary>
    public class MStockCountSchedule
    {
        private long mPkSrNo;
        private long mItemNo;
        private long mCountTypeNo;
        private DateTime mCountScheduleDate;
        private bool mIsActive;
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
        /// This Properties use for ItemNo
        /// </summary>
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        /// <summary>
        /// This Properties use for CountTypeNo
        /// </summary>
        public long CountTypeNo
        {
            get { return mCountTypeNo; }
            set { mCountTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for CountScheduleDate
        /// </summary>
        public DateTime CountScheduleDate
        {
            get { return mCountScheduleDate; }
            set { mCountScheduleDate = value; }
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
}
