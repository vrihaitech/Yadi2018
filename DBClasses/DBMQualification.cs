using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using OM;
using OMControls;

namespace OM
{
    class DBMQualification
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMQualification(MQualification mqualification)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMQualification";

            cmd.Parameters.AddWithValue("@QualificationNo", mqualification.QualificationNo);

            cmd.Parameters.AddWithValue("@QualificationName", mqualification.QualificationName);

            cmd.Parameters.AddWithValue("@ShortName", mqualification.ShortName);

            cmd.Parameters.AddWithValue("@IsActive", mqualification.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mqualification.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mqualification.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mqualification.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mqualification.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMQualification(MQualification mqualification)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMQualification";

            cmd.Parameters.AddWithValue("@QualificationNo", mqualification.QualificationNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mqualification.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMQualification()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MQualification order by QualificationName";
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

        public MQualification ModifyMQualificationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MQualification where QualificationNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MQualification MM = new MQualification();
                while (dr.Read())
                {
                    MM.QualificationNo = Convert.ToInt32(dr["QualificationNo"]);
                    if (!Convert.IsDBNull(dr["QualificationName"])) MM.QualificationName = Convert.ToString(dr["QualificationName"]);
                    if (!Convert.IsDBNull(dr["ShortName"])) MM.ShortName = Convert.ToString(dr["ShortName"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MQualification();
        }

        public DataView GetMQualificationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MQualification where QualificationNo =" + ID;
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
                    sql = "Select QualificationNo,ShortName ,QualificationName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MQualification order by QualificationName ";
                    break;
                case "QualificationName":
                    sql = "Select QualificationNo,ShortName,QualificationName,Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MQualification where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by QualificationName";
                    break;
                case "ShortName":
                    sql = "Select QualificationNo,ShortName AS 'Short Name',QualificationName AS 'Qualification Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MQualification where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' Order By ShortName ";
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
    /// This Class use for MQualification
    /// </summary>
    public class MQualification
    {
        private long mQualificationNo;
        private string mQualificationName;
        private string mShortName;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for QualificationNo
        /// </summary>
        public long QualificationNo
        {
            get { return mQualificationNo; }
            set { mQualificationNo = value; }
        }
        /// <summary>
        /// This Properties use for QualificationName
        /// </summary>
        public string QualificationName
        {
            get { return mQualificationName; }
            set { mQualificationName = value; }
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

}
