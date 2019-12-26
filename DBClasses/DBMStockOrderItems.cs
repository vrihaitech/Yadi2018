using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMStockOrderItems
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMStockOrderItems(MStockOrderItems mstockorderitems)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockOrderItems";

            cmd.Parameters.AddWithValue("@StockOrderItemNo", mstockorderitems.StockOrderItemNo);

            cmd.Parameters.AddWithValue("@ItemNo", mstockorderitems.ItemNo);

            cmd.Parameters.AddWithValue("@IsUpload", mstockorderitems.IsUpload);

            cmd.Parameters.AddWithValue("@IsGeneral", mstockorderitems.IsGeneral);

            cmd.Parameters.AddWithValue("@MRP", mstockorderitems.MRP);

            cmd.Parameters.AddWithValue("@FKRateSettingNo", mstockorderitems.FKRateSettingNo);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockorderitems.CompanyNo);

            cmd.Parameters.AddWithValue("@StatusNo", mstockorderitems.StatusNo);

            cmd.Parameters.AddWithValue("@UserID", mstockorderitems.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mstockorderitems.UserDate);

            commandcollection.Add(cmd);
            return true;

        }

        public MStockOrderItems ModifyMStockOrderItemsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MStockOrderItems where StockOrderItemNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MStockOrderItems MM = new MStockOrderItems();
                while (dr.Read())
                {
                    MM.StockOrderItemNo = Convert.ToInt32(dr["StockOrderItemNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["IsUpload"])) MM.IsUpload = Convert.ToBoolean(dr["IsUpload"]);
                    if (!Convert.IsDBNull(dr["IsGeneral"])) MM.IsGeneral = Convert.ToBoolean(dr["IsGeneral"]);
                    if (!Convert.IsDBNull(dr["MRP"])) MM.MRP = Convert.ToDouble(dr["MRP"]);
                    if (!Convert.IsDBNull(dr["FKRateSettingNo"])) MM.FKRateSettingNo = Convert.ToInt64(dr["FKRateSettingNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MStockOrderItems();
        }

        public DataView GetAllMStockOrderItems()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockOrderItems order by StockOrderItemNo";
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

        public DataView GetMStockOrderItemsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockOrderItems where StockOrderItemNo =" + ID;
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

        public bool DeleteMStockOrderItems(MStockOrderItems mstockorderitems)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMStockOrderItems";

            cmd.Parameters.AddWithValue("@StockOrderItemNo", mstockorderitems.StockOrderItemNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mstockorderitems.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool ExecuteNonQueryStatements1()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
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

    public class MStockOrderItems
    {
        private long mStockOrderItemNo;
        private long mItemNo;
        private bool mIsUpload;
        private bool mIsGeneral;
        private double mMRP;
        private long mFKRateSettingNo;
        private long mCompanyNo;
        private long mStatusNo;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        public long StockOrderItemNo
        {
            get { return mStockOrderItemNo; }
            set { mStockOrderItemNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public bool IsUpload
        {
            get { return mIsUpload; }
            set { mIsUpload = value; }
        }
        public bool IsGeneral
        {
            get { return mIsGeneral; }
            set { mIsGeneral = value; }
        }
        public double MRP
        {
            get { return mMRP; }
            set { mMRP = value; }
        }
        public long FKRateSettingNo
        {
            get { return mFKRateSettingNo; }
            set { mFKRateSettingNo = value; }
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
