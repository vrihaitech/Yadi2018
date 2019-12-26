using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMUOM
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMUOM(MUOM muom)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMUOM";

            cmd.Parameters.AddWithValue("@UOMNo", muom.UOMNo);

            cmd.Parameters.AddWithValue("@UOMName", muom.UOMName);

            cmd.Parameters.AddWithValue("@UOMShortCode", muom.UOMShortCode);

            cmd.Parameters.AddWithValue("@IsActive", muom.IsActive);

            cmd.Parameters.AddWithValue("@UserID", muom.UserID);

            cmd.Parameters.AddWithValue("@UserDate", muom.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);

            //cmd.Parameters.AddWithValue("@UQC", muom.UQC);

            cmd.Parameters.AddWithValue("@UOMType", muom.UOMType);

            //cmd.Parameters.AddWithValue("@ModifiedBy", muom.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                muom.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool AddMUOM1(MUOM muom)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMUOM1";

            cmd.Parameters.AddWithValue("@UOMNo", muom.UOMNo);

            cmd.Parameters.AddWithValue("@UOMName", muom.UOMName);

            cmd.Parameters.AddWithValue("@UOMShortCode", muom.UOMShortCode);

            cmd.Parameters.AddWithValue("@IsActive", muom.IsActive);

            cmd.Parameters.AddWithValue("@UserID", muom.UserID);

            cmd.Parameters.AddWithValue("@UserDate", muom.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);

            cmd.Parameters.AddWithValue("@GlobalCode", muom.GlobalCode);

            //cmd.Parameters.AddWithValue("@ModifiedBy", muom.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                muom.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMUOM(MUOM muom)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMUOM";

            cmd.Parameters.AddWithValue("@UOMNo", muom.UOMNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                muom.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMUOM()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MUOM order by UOMNo";
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

        public DataView GetMUOMByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MUOM where UOMNo =" + ID;
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

        public MUOM ModifyMUOMByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MUOM where UOMNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MUOM MM = new MUOM();
                while (dr.Read())
                {
                    MM.UOMNo = Convert.ToInt32(dr["UOMNo"]);
                    if (!Convert.IsDBNull(dr["UOMName"])) MM.UOMName = Convert.ToString(dr["UOMName"]);
                    if (!Convert.IsDBNull(dr["UOMShortCode"])) MM.UOMShortCode = Convert.ToString(dr["UOMShortCode"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.UQC = Convert.ToString(dr["UQC"]);
                    if (!Convert.IsDBNull(dr["UOMType"])) MM.UOMType = Convert.ToInt32(dr["UOMType"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MUOM();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name',Case When (IsActive ='true') Then 'True' Else 'False' END AS Status from MUOM  where uomno not in (1) Order By UOMName";
                    // sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name', Cast(IsActive As varchar(50)) AS Status from MUOM ";
                    break;
                case "UOMName":
                    sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name', Case When (IsActive ='true') Then 'True' Else 'False' END  AS Status from MUOM where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' and  uomno not in (1) Order By UOMName";
                    break;
                case "UOMShortCode":
                    sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name',Case When (IsActive ='true') Then 'True' Else 'False' END  AS Status from MUOM where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' and  uomno not in (1) Order By UOMName";
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
    /// This Class use for MUOM
    /// </summary>
    public class MUOM
    {
        private long mUOMNo;
        private string mUOMName;
        private string mUOMShortCode;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private long mCompanyNo;
        private long mGlobalCode;
        private string Mmsg;
        private string mUQC;
        private int MUOMType;

        /// <summary>
        /// This Properties use for UOMNo
        /// </summary>
        public long UOMNo
        {
            get { return mUOMNo; }
            set { mUOMNo = value; }
        }
        /// <summary>
        /// This Properties use for UOMName
        /// </summary>
        public string UOMName
        {
            get { return mUOMName; }
            set { mUOMName = value; }
        }
        /// <summary>
        /// This Properties use for UOMShortCode
        /// </summary>
        public string UOMShortCode
        {
            get { return mUOMShortCode; }
            set { mUOMShortCode = value; }
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
        /// This Properties use for CompanyNo
        /// </summary>
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        /// <summary>
        /// This Properties use for GlobalCode
        /// </summary>
        public long GlobalCode
        {
            get { return mGlobalCode; }
            set { mGlobalCode = value; }
        }
        /// <summary>
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }

        public string UQC
        {
            get { return mUQC; }
            set { mUQC = value; }
        }
       
        public int UOMType
        {
            get { return MUOMType; }
            set { MUOMType = value; }
        }
    }

}
