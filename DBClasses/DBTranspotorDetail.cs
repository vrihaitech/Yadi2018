using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;


namespace OM
{
    class DBTranspotorDetail
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();

        public bool AddTranspotorDetail(TranspotorDetail mTranspotorDetail)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTranspotorDetail";


            cmd.Parameters.AddWithValue("@PkTranspotorDetail", mTranspotorDetail.PkTranspotorDetail);
            //cmd.Parameters.AddWithValue("@FKEWayNo", mTranspotorDetail.FKEWayNo);
            //cmd.Parameters.AddWithValue("@FkVoucherNo", mTranspotorDetail.FkVoucherNo);
            cmd.Parameters.AddWithValue("@NoOfQty", mTranspotorDetail.NoOfQty);
            cmd.Parameters.AddWithValue("@BalancedQty", mTranspotorDetail.BalancedQty);
            cmd.Parameters.AddWithValue("@ReceivedQty", mTranspotorDetail.ReceivedQty);
            cmd.Parameters.AddWithValue("@msg", mTranspotorDetail.msg);

            //if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    mTranspotorDetail.msg = ObjTrans.ErrorMessage;
            //    return false;
            //}
            commandcollection.Add(cmd);
            return true;
        }



        //public bool AddMTranspotorDetail(MTranspotorDetail mTranspotorDetail)
        //{
        //    SqlCommand cmd;
        //    cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "AddMTranspotorDetail";

        //    cmd.Parameters.AddWithValue("@UOMNo", mTranspotorDetail.UOMNo);

        //    cmd.Parameters.AddWithValue("@UOMName", muom.UOMName);

        //    cmd.Parameters.AddWithValue("@UOMShortCode", muom.UOMShortCode);

        //    cmd.Parameters.AddWithValue("@IsActive", muom.IsActive);

        //    cmd.Parameters.AddWithValue("@UserID", muom.UserID);

        //    cmd.Parameters.AddWithValue("@UserDate", muom.UserDate);

        //    cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);

        //    cmd.Parameters.AddWithValue("@GlobalCode", muom.GlobalCode);

        //    //cmd.Parameters.AddWithValue("@ModifiedBy", muom.ModifiedBy);
        //    if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        muom.msg = ObjTrans.ErrorMessage;
        //        return false;
        //    }
        //}

        public bool DeleteTranspotorDetail(TranspotorDetail mTranspotorDetail)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTranspotorDetail";

            cmd.Parameters.AddWithValue("@PkTranspotorDetail", mTranspotorDetail.PkTranspotorDetail);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mTranspotorDetail.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTranspotorDetail()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TranspotorDetail order by PKTranspotorDetail";
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

        public DataView GetTranspotorDetailID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TranspotorDetail where PKTranspotorDetail =" + ID;
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

        public TranspotorDetail ModifyTranspotorDetailByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd; 
             //sql = "Select * from TranspotorDetail where FKTranspotorDetail =" + ID;
            sql = "Select * from TranspotorDetail where FkVoucherNo = " + ID ;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TranspotorDetail MM = new TranspotorDetail();
                while (dr.Read())
                {
                    MM.PkTranspotorDetail = Convert.ToInt32(dr["PkTranspotorDetail"]);
                    //if (!Convert.IsDBNull(dr["FKEWayNo"])) MM.FKEWayNo = Convert.ToInt32(dr["FKEWayNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt32(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["NoOfQty"])) MM.NoOfQty = Convert.ToDouble(dr["NoOfQty"]);
                    if (!Convert.IsDBNull(dr["BalancedQty"])) MM.BalancedQty = Convert.ToDouble(dr["BalancedQty"]);
                    if (!Convert.IsDBNull(dr["ReceivedQty"])) MM.ReceivedQty = Convert.ToDouble(dr["ReceivedQty"]);
                    if (!Convert.IsDBNull(dr["RemarkQty"])) MM.RemarkQty = Convert.ToString(dr["RemarkQty"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TranspotorDetail();
        }

        //public DataView GetBySearch(string Column, string Value)
        //{

        //    string sql = null;
        //    switch (Column)
        //    {
        //        case "0":
        //            sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name',Case When (IsActive ='true') Then 'True' Else 'False' END AS Status from MUOM  where uomno not in (1) Order By UOMName";
        //            // sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name', Cast(IsActive As varchar(50)) AS Status from MUOM ";
        //            break;
        //        case "UOMName":
        //            sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name', Case When (IsActive ='true') Then 'True' Else 'False' END  AS Status from MUOM where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' and  uomno not in (1) Order By UOMName";
        //            break;
        //        case "UOMShortCode":
        //            sql = "Select UOMNo,UOMShortCode AS 'Short Name',UOMName AS 'UOM Name',Case When (IsActive ='true') Then 'True' Else 'False' END  AS Status from MUOM where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' and  uomno not in (1) Order By UOMName";
        //            break;
        //    }
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
        //    }
        //    catch (SqlException e)
        //    {
        //        CommonFunctions.ErrorMessge = e.Message;
        //    }
        //    return ds.Tables[0].DefaultView;
        //}
    }
}

    /// <summary>
    /// This Class use for DBTranspotorDetail
    /// </summary>
//    public class TranspotorDetail
//    {
//        private long mPkTranspotorDetail;
//        private long mFkVoucherNo;
//        private long mFKEWayNo;
//        private double mNoOfQty;
//        private double mBalancedQty;
//        private double mReceivedQty;
//        private string mmsg;
//        public long PkTranspotorDetail
//        {
//            get { return mPkTranspotorDetail; }
//            set { mPkTranspotorDetail = value; }
//        }
//        public long FkVoucherNo
//        {
//            get { return mFkVoucherNo; }
//            set { mFkVoucherNo = value; }
//        }
//        public long FKEWayNo
//        {
//            get { return mFKEWayNo; }
//            set { mFKEWayNo = value; }
//        }
//        public double NoOfQty
//        {
//            get { return mNoOfQty; }
//            set { mNoOfQty = value; }
//        }
//        public double BalancedQty
//        {
//            get { return mBalancedQty; }
//            set { mBalancedQty = value; }
//        }
//        public double ReceivedQty
//        {
//            get { return mReceivedQty; }
//            set { mReceivedQty = value; }
//        }
//        public string msg
//        {
//            get { return mmsg; }
//            set { mmsg = value; }
//        }
//    }
//}
