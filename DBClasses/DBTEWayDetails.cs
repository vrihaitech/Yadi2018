using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBTEWayDetails
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddTEWayDetails(TEWayDetails tewaydetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTEWayDetails";

            cmd.Parameters.AddWithValue("@PKEWayNo", tewaydetails.PKEWayNo);

            cmd.Parameters.AddWithValue("@FkVoucherNo", tewaydetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@EWayNo", tewaydetails.EWayNo);

            cmd.Parameters.AddWithValue("@VoucherUserNo", tewaydetails.VoucherUserNo);

            cmd.Parameters.AddWithValue("@EWayDate", tewaydetails.EWayDate);

            cmd.Parameters.AddWithValue("@ModeNo", tewaydetails.ModeNo);

            cmd.Parameters.AddWithValue("@Distance", tewaydetails.Distance);

            cmd.Parameters.AddWithValue("@TransportNo", tewaydetails.TransportNo);

            cmd.Parameters.AddWithValue("@VehicleNo", tewaydetails.VehicleNo);

            cmd.Parameters.AddWithValue("@LRNo", tewaydetails.LRNo);

            cmd.Parameters.AddWithValue("@LRDate", tewaydetails.LRDate);

            cmd.Parameters.AddWithValue("@LedgerNo", tewaydetails.LedgerNo);

            cmd.Parameters.AddWithValue("@LedgerName", tewaydetails.LedgerName);

            cmd.Parameters.AddWithValue("@Address", tewaydetails.Address);

            cmd.Parameters.AddWithValue("@CityNo", tewaydetails.CityNo);

            cmd.Parameters.AddWithValue("@CityName", tewaydetails.CityName);

            cmd.Parameters.AddWithValue("@PinCode", tewaydetails.PinCode);

            cmd.Parameters.AddWithValue("@StateCode", tewaydetails.StateCode);

            cmd.Parameters.AddWithValue("@StateName", tewaydetails.StateName);

            cmd.Parameters.AddWithValue("@UserID", tewaydetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tewaydetails.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", tewaydetails.ModifiedBy);

            cmd.Parameters.AddWithValue("@StatusNo", tewaydetails.StatusNo);

            cmd.Parameters.AddWithValue("@IsActive", tewaydetails.IsActive);


            cmd.Parameters.AddWithValue("@TranspotorNm", tewaydetails.TranspotorNm);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tewaydetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteTEWayDetails(TEWayDetails tewaydetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTEWayDetails";

            cmd.Parameters.AddWithValue("@PKEWayNo", tewaydetails.PKEWayNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tewaydetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        //public DataView GetAllMArea()
        //{
        //    SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
        //    string sql = "Select * from MArea order by AreaNo";
        //    SqlDataAdapter da = new SqlDataAdapter(sql, Con);
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        da.Fill(ds);
        //    }
        //    catch { throw; }
        //    finally
        //    {
        //        Con.Close();
        //    }
        //    return ds.Tables[(0)].DefaultView;
        //}

        public TEWayDetails ModifyTEWayDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TEWayDetails where FkVoucherNo = " + ID ;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TEWayDetails MM = new TEWayDetails();
                while (dr.Read())
                {
                    MM.PKEWayNo = Convert.ToInt64(dr["PKEWayNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt32(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["EWayNo"])) MM.EWayNo = Convert.ToString(dr["EWayNo"]);
                    if (!Convert.IsDBNull(dr["VoucherUserNo"])) MM.VoucherUserNo = Convert.ToInt64(dr["VoucherUserNo"]);
                    if (!Convert.IsDBNull(dr["EWayDate"])) MM.EWayDate = Convert.ToDateTime(dr["EWayDate"]);
                    if (!Convert.IsDBNull(dr["ModeNo"])) MM.ModeNo = Convert.ToInt64(dr["ModeNo"]);
                    if (!Convert.IsDBNull(dr["Distance"])) MM.Distance = Convert.ToDouble(dr["Distance"]);
                    if (!Convert.IsDBNull(dr["TransportNo"])) MM.TransportNo = Convert.ToInt64(dr["TransportNo"]);
                    if (!Convert.IsDBNull(dr["VehicleNo"])) MM.VehicleNo = Convert.ToString(dr["VehicleNo"]);
                    if (!Convert.IsDBNull(dr["LRNo"])) MM.LRNo = Convert.ToString(dr["LRNo"]);
                    if (!Convert.IsDBNull(dr["LRDate"])) MM.LRDate = Convert.ToDateTime(dr["LRDate"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt32(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["LedgerName"])) MM.LedgerName = Convert.ToString(dr["LedgerName"]);
                    if (!Convert.IsDBNull(dr["Address"])) MM.Address = Convert.ToString(dr["Address"]);
                    if (!Convert.IsDBNull(dr["CityNo"])) MM.CityNo = Convert.ToInt64(dr["CityNo"]);
                    if (!Convert.IsDBNull(dr["CityName"])) MM.CityName = Convert.ToString(dr["CityName"]);
                    if (!Convert.IsDBNull(dr["PinCode"])) MM.PinCode = Convert.ToInt64(dr["PinCode"]);
                    if (!Convert.IsDBNull(dr["StateCode"])) MM.StateCode = Convert.ToInt64(dr["StateCode"]);
                    if (!Convert.IsDBNull(dr["StateName"])) MM.StateName = Convert.ToString(dr["StateName"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt32(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.Address = Convert.ToString(dr["IsActive"]);

                    if (!Convert.IsDBNull(dr["TranspotorNm"])) MM.TranspotorNm = Convert.ToString(dr["TranspotorNm"]);

                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TEWayDetails();
        }

        public DataView GetTEWayDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLedger where LedgerNo =" + ID;
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
                    sql = "Select AreaNo,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MArea ";
                    break;
                case "AreaName":
                    sql = "Select AreaNo,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MArea where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
                    break;
                case "AreaShortCode":
                    sql = "Select AreaNo,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MArea where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
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


    public class TEWayDetails
    {
        private long mPKEWayNo;
        private long mFkVoucherNo;
        private string mEWayNo;
        private long mVoucherUserNo;
        private DateTime mEWayDate;
        private long mModeNo;
        private double mDistance;
        private long mTransportNo;
        private string mVehicleNo;
        private string  mLRNo;
        private DateTime mLRDate;
        private long mLedgerNo;
        private string mLedgerName;
        private string mAddress;
        private long mCityNo;
        private string mCityName;
        private long mPinCode;
        private long mStateCode;
        private string mStateName;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private bool mIsActive;
        private string Mmsg;
        private string  MTranspotorNm;

        public string TranspotorNm
        {
            get { return MTranspotorNm; }
            set { MTranspotorNm = value; }
        }
        public long PKEWayNo
        {
            get { return mPKEWayNo; }
            set { mPKEWayNo = value; }
        }
        
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }

        public string EWayNo
        {
            get { return mEWayNo; }
            set { mEWayNo = value; }
        }

        public long VoucherUserNo
        {
            get { return mVoucherUserNo; }
            set { mVoucherUserNo = value; }
        }

        public DateTime EWayDate
        {
            get { return mEWayDate; }
            set { mEWayDate = value; }
        }

        public long ModeNo
        {
            get { return mModeNo; }
            set { mModeNo = value; }
        }

        public double Distance
        {
            get { return mDistance; }
            set { mDistance = value; }
        }

        public long TransportNo
        {
            get { return mTransportNo; }
            set { mTransportNo = value; }
        }

        public string VehicleNo
        {
            get { return mVehicleNo; }
            set { mVehicleNo = value; }
        }

        public string LRNo
        {
            get { return mLRNo; }
            set { mLRNo = value; }
        }

        public DateTime LRDate
        {
            get { return mLRDate; }
            set { mLRDate = value; }
        }

        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }

        public string LedgerName
        {
            get { return mLedgerName; }
            set { mLedgerName = value; }
        }


        public string Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }

        public long CityNo
        {
            get { return mCityNo; }
            set { mCityNo = value; }
        }

        public string CityName
        {
            get { return mCityName; }
            set { mCityName = value; }
        }

        public long PinCode
        {
            get { return mPinCode; }
            set { mPinCode = value; }
        }

        public long StateCode
        {
            get { return mStateCode; }
            set { mStateCode = value; }
        }

        public string StateName
        {
            get { return mStateName; }
            set { mStateName = value; }
        }

        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }

        public DateTime UserDate
        {
            get { return mUserDate; }
            set { mUserDate = value; }
        }

        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
        }

        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }

        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }

        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
       
    }

}
