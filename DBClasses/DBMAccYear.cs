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
    class DBMAccYear
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMAccYear(MAccYear maccyear)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMAccYear";

            cmd.Parameters.AddWithValue("@AccYearNo", maccyear.AccYearNo);

            cmd.Parameters.AddWithValue("@AccYearName", maccyear.AccYearName);

            cmd.Parameters.AddWithValue("@FinancialYear", maccyear.FinancialYear);

            cmd.Parameters.AddWithValue("@BooksBeginFrom", maccyear.BooksBeginFrom);

            cmd.Parameters.AddWithValue("@BooksEndOn", maccyear.BooksEndOn);

            cmd.Parameters.AddWithValue("@AssesmentYearName", maccyear.AssesmentYearName);

            cmd.Parameters.AddWithValue("@DatabaseName", maccyear.DatabaseName);

            cmd.Parameters.AddWithValue("@IsLocked", maccyear.IsLocked);

            cmd.Parameters.AddWithValue("@IsActive", maccyear.IsActive);

            cmd.Parameters.AddWithValue("@UserID", maccyear.UserID);

            cmd.Parameters.AddWithValue("@UserDate", maccyear.UserDate);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                maccyear.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMAccYear(MAccYear maccyear)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMAccYear";

            cmd.Parameters.AddWithValue("@AccYearNo", maccyear.AccYearNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                maccyear.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMAccYear()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MAccYear order by AccYearNo";
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

        public DataView GetMAccYearByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MAccYear where AccYearNo =" + ID;
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

        public MAccYear ModifyMAccYearByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MAccYear where AccYearNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MAccYear MM = new MAccYear();
                while (dr.Read())
                {
                    MM.AccYearNo = Convert.ToInt32(dr["AccYearNo"]);
                    if (!Convert.IsDBNull(dr["AccYearName"])) MM.AccYearName = Convert.ToString(dr["AccYearName"]);
                    if (!Convert.IsDBNull(dr["FinancialYear"])) MM.FinancialYear = Convert.ToString(dr["FinancialYear"]);
                    if (!Convert.IsDBNull(dr["BooksBeginFrom"])) MM.BooksBeginFrom = Convert.ToDateTime(dr["BooksBeginFrom"]);
                    if (!Convert.IsDBNull(dr["BooksEndOn"])) MM.BooksEndOn = Convert.ToDateTime(dr["BooksEndOn"]);
                    if (!Convert.IsDBNull(dr["AssesmentYearName"])) MM.AssesmentYearName = Convert.ToString(dr["AssesmentYearName"]);
                    if (!Convert.IsDBNull(dr["DatabaseName"])) MM.DatabaseName = Convert.ToString(dr["DatabaseName"]);
                    if (!Convert.IsDBNull(dr["IsLocked"])) MM.IsLocked = Convert.ToBoolean(dr["IsLocked"]);
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
            return new MAccYear();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select AccYearNo,AccYearName AS 'AccYear Name' from MAccYear order by AccYearName ";
                    break;
                case "AccYearName":
                    sql = "Select AccYearNo,AccYearName AS 'AccYear Name' from MAccYear where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by AccYearName";
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
    /// This Class use for MAccYear
    /// </summary>
    public class MAccYear
    {
        private long mAccYearNo;
        private string mAccYearName;
        private string mFinancialYear;
        private DateTime mBooksBeginFrom;
        private DateTime mBooksEndOn;
        private string mAssesmentYearName;
        private string mDatabaseName;
        private bool mIsLocked;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        /// <summary>
        /// This Properties use for AccYearNo
        /// </summary>
        public long AccYearNo
        {
            get { return mAccYearNo; }
            set { mAccYearNo = value; }
        }
        /// <summary>
        /// This Properties use for AccYearName
        /// </summary>
        public string AccYearName
        {
            get { return mAccYearName; }
            set { mAccYearName = value; }
        }
        /// <summary>
        /// This Properties use for FinancialYear
        /// </summary>
        public string FinancialYear
        {
            get { return mFinancialYear; }
            set { mFinancialYear = value; }
        }
        /// <summary>
        /// This Properties use for BooksBeginFrom
        /// </summary>
        public DateTime BooksBeginFrom
        {
            get { return mBooksBeginFrom; }
            set { mBooksBeginFrom = value; }
        }
        /// <summary>
        /// This Properties use for BooksEndOn
        /// </summary>
        public DateTime BooksEndOn
        {
            get { return mBooksEndOn; }
            set { mBooksEndOn = value; }
        }
        /// <summary>
        /// This Properties use for AssesmentYearName
        /// </summary>
        public string AssesmentYearName
        {
            get { return mAssesmentYearName; }
            set { mAssesmentYearName = value; }
        }
        /// <summary>
        /// This Properties use for DatabaseName
        /// </summary>
        public string DatabaseName
        {
            get { return mDatabaseName; }
            set { mDatabaseName = value; }
        }
        /// <summary>
        /// This Properties use for IsLocked
        /// </summary>
        public bool IsLocked
        {
            get { return mIsLocked; }
            set { mIsLocked = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

}
