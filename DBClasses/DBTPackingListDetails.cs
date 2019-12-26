using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{

    class DBTPackingListDetails
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;


        public bool AddTPackingListDetails(TPackingListDetails tpackinglistdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTPackingListDetails";

            cmd.Parameters.AddWithValue("@PackingListNo", tpackinglistdetails.PackingListNo);

            cmd.Parameters.AddWithValue("@FkVoucherNo", tpackinglistdetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@ItemNo", tpackinglistdetails.ItemNo);

            cmd.Parameters.AddWithValue("@Quantity", tpackinglistdetails.Quantity);

            cmd.Parameters.AddWithValue("@BagNo", tpackinglistdetails.BagNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tpackinglistdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@GroupNo", tpackinglistdetails.GroupNo);

            cmd.Parameters.AddWithValue("@FkStockTrnNo", tpackinglistdetails.FkStockTrnNo);

            cmd.Parameters.AddWithValue("@UserID", tpackinglistdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tpackinglistdetails.UserDate);

            commandcollection.Add(cmd);
            return true;
            
        }

        public bool SaveTransNoOfItems(long PkVoucherNo, double TransNoOfItems)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set TransNoOfItems=@TransNoOfItems,StatusNo=2 where PkVoucherNo=@PkVoucherNo";

            cmd.Parameters.AddWithValue("@PkVoucherNo", PkVoucherNo);

            cmd.Parameters.AddWithValue("@TransNoOfItems", TransNoOfItems);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                //tVoucherEntry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteTPackingListDetails(TPackingListDetails tpackinglistdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTPackingListDetails";

            cmd.Parameters.AddWithValue("@PackingListNo", tpackinglistdetails.PackingListNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
             
                return false;
            }
        }

        public bool DeleteTPackingListDetailsGroup(TPackingListDetails tpackinglistdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTPackingListDetailsGroup";

            cmd.Parameters.AddWithValue("@FkVoucherNo", tpackinglistdetails.FkVoucherNo);
            cmd.Parameters.AddWithValue("@GroupNo", tpackinglistdetails.GroupNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
              
                return false;
            }
        }
        
        public TPackingListDetails ModifyTPackingListDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TPackingListDetails where PackingListNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TPackingListDetails MM = new TPackingListDetails();
                while (dr.Read())
                {
                    MM.PackingListNo = Convert.ToInt32(dr["PackingListNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt64(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["Quantity"])) MM.Quantity = Convert.ToDouble(dr["Quantity"]);
                    if (!Convert.IsDBNull(dr["BagNo"])) MM.BagNo = Convert.ToString(dr["BagNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["GroupNo"])) MM.GroupNo = Convert.ToInt64(dr["GroupNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TPackingListDetails();
        }

        public DataView GetAllTPackingListDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TPackingListDetails order by PackingListNo";
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

        public DataView GetGetTPackingListDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TPackingListDetails where PackingListNo =" + ID;
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
    public class TPackingListDetails
    {
        private long mPackingListNo;
        private long mFkVoucherNo;
        private long mItemNo;
        private double mQuantity;
        private string mBagNo;
        private long mCompanyNo;
        private long mStatusNo;
        private long mGroupNo;
        private long mFkStockTrnNo;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        public long PackingListNo
        {
            get { return mPackingListNo; }
            set { mPackingListNo = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        public string BagNo
        {
            get { return mBagNo; }
            set { mBagNo = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public long GroupNo
        {
            get { return mGroupNo; }
            set { mGroupNo = value; }
        }
        public long FkStockTrnNo
        {
            get { return mFkStockTrnNo; }
            set { mFkStockTrnNo = value; }
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
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

}

