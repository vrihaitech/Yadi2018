using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBTSeasonalItems
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTSeasonalItems(TSeasonalItems tseasonalitems)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTSeasonalItems";

            cmd.Parameters.AddWithValue("@PKSrNo", tseasonalitems.PKSrNo);

            cmd.Parameters.AddWithValue("@BillNo", tseasonalitems.BillNo);

            cmd.Parameters.AddWithValue("@ItemName", tseasonalitems.ItemName);

            cmd.Parameters.AddWithValue("@MRP", tseasonalitems.MRP);

            cmd.Parameters.AddWithValue("@Qty", tseasonalitems.Qty);

            cmd.Parameters.AddWithValue("@Barcode", tseasonalitems.Barcode);

            cmd.Parameters.AddWithValue("@IsPrint", tseasonalitems.IsPrint);

            cmd.Parameters.AddWithValue("@UserID", tseasonalitems.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tseasonalitems.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTSeasonalItems(TSeasonalItems tseasonalitems)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTSeasonalItems";

            cmd.Parameters.AddWithValue("@PKSrNo", tseasonalitems.PKSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tseasonalitems.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTSeasonalItems()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TSeasonalItems order by PKSrNo";
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

        public DataView GetTSeasonalItemsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TSeasonalItems where PKSrNo =" + ID;
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

        public TSeasonalItems ModifyTSeasonalItemsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TSeasonalItems where PKSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TSeasonalItems MM = new TSeasonalItems();
                while (dr.Read())
                {
                    MM.PKSrNo = Convert.ToInt32(dr["PKSrNo"]);
                    if (!Convert.IsDBNull(dr["BillNo"])) MM.BillNo = Convert.ToInt64(dr["BillNo"]);
                    if (!Convert.IsDBNull(dr["ItemName"])) MM.ItemName = Convert.ToString(dr["ItemName"]);
                    if (!Convert.IsDBNull(dr["MRP"])) MM.MRP = Convert.ToDouble(dr["MRP"]);
                    if (!Convert.IsDBNull(dr["Qty"])) MM.Qty = Convert.ToDouble(dr["Qty"]);
                    if (!Convert.IsDBNull(dr["Barcode"])) MM.Barcode = Convert.ToString(dr["Barcode"]);
                    if (!Convert.IsDBNull(dr["IsPrint"])) MM.IsPrint = Convert.ToBoolean(dr["IsPrint"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifyBy"])) MM.ModifyBy = Convert.ToString(dr["ModifyBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TSeasonalItems();
        }

        public bool UpdateSeasonalItems(long PkSrNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TSeasonalItems set Isprint='true' where PkSrNo=@PkSrNo";

            cmd.Parameters.AddWithValue("@PkSrNo", PkSrNo);

            commandcollection.Add(cmd);
            return true;
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
    
    public class TSeasonalItems
    {
        private long mPKSrNo;
        private long mBillNo;
        private string mItemName;
        private double mMRP;
        private double mQty;
        private string mBarcode;
        private bool mIsPrint;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifyBy;
        private string Mmsg;

        public long PKSrNo
        {
            get { return mPKSrNo; }
            set { mPKSrNo = value; }
        }
        public long BillNo
        {
            get { return mBillNo; }
            set { mBillNo = value; }
        }
        public string ItemName
        {
            get { return mItemName; }
            set { mItemName = value; }
        }
        public double MRP
        {
            get { return mMRP; }
            set { mMRP = value; }
        }
        public double Qty
        {
            get { return mQty; }
            set { mQty = value; }
        }
        public string Barcode
        {
            get { return mBarcode; }
            set { mBarcode = value; }
        }
        public bool IsPrint
        {
            get { return mIsPrint; }
            set { mIsPrint = value; }
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
        public string ModifyBy
        {
            get { return mModifyBy; }
            set { mModifyBy = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
