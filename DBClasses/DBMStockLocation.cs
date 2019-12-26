using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMStockLocation
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMStockLocation(MStockLocation mstocklocation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockLocation";

            cmd.Parameters.AddWithValue("@StockLocationNo", mstocklocation.StockLocationNo);

            cmd.Parameters.AddWithValue("@StockLocationName", mstocklocation.StockLocationName);

            cmd.Parameters.AddWithValue("@GodownNo", mstocklocation.GodownNo);

            cmd.Parameters.AddWithValue("@IsActive", mstocklocation.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mstocklocation.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mstocklocation.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mstocklocation.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstocklocation.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMStockLocation(MStockLocation mstocklocation)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMStockLocation";

            cmd.Parameters.AddWithValue("@StockLocationNo", mstocklocation.StockLocationNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstocklocation.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMStockLocation()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockLocation order by StockLocationNo";
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

        public DataView GetMStockLocationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockLocation where StockLocationNo =" + ID;
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

        public MStockLocation ModifyMStockLocationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MStockLocation where StockLocationNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MStockLocation MM = new MStockLocation();
                while (dr.Read())
                {
                    MM.StockLocationNo = Convert.ToInt32(dr["StockLocationNo"]);
                    if (!Convert.IsDBNull(dr["StockLocationName"])) MM.StockLocationName = Convert.ToString(dr["StockLocationName"]);
                    if (!Convert.IsDBNull(dr["GodownNo"])) MM.GodownNo = Convert.ToInt64(dr["GodownNo"]);
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
            return new MStockLocation();
        }



        public DataView GetBySearch(string Column, string Value)
        {
            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select SL.StockLocationNo, SL.StockLocationName AS 'Name', G.godownname as 'Godown', Case when (SL.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MStockLocation SL,MGodown G Where G.godownno= SL.godownno order by SL.StockLocationName";  
                    break;

                case "StockLocationName":
                    sql = "Select SL.StockLocationNo, SL.StockLocationName AS 'Name', G.godownname as 'Godown', Case when (SL.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MStockLocation SL,MGodown G Where G.godownno= SL.godownno and SL." + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'  order by SL.StockLocationName";
                    break;

                case "GodownName":
                    sql = "Select SL.StockLocationNo, SL.StockLocationName AS 'Name', G.godownname as 'Godown', Case when (SL.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MStockLocation SL,MGodown G Where G.godownno= SL.godownno and G." + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%'  order by SL.StockLocationName";
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
    /// This Class use for MStockLocation
    /// </summary>
    public class MStockLocation
    {
        private long mStockLocationNo;
        private string mStockLocationName;
        private long mGodownNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for StockLocationNo
        /// </summary>
        public long StockLocationNo
        {
            get { return mStockLocationNo; }
            set { mStockLocationNo = value; }
        }
        /// <summary>
        /// This Properties use for StockLocationName
        /// </summary>
        public string StockLocationName
        {
            get { return mStockLocationName; }
            set { mStockLocationName = value; }
        }
        /// <summary>
        /// This Properties use for GodownNo
        /// </summary>
        public long GodownNo
        {
            get { return mGodownNo; }
            set { mGodownNo = value; }
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
