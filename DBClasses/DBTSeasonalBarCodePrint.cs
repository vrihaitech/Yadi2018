using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;
using OMControls;

namespace OM
{
    class DBTSeasonalBarCodePrint
    {
        
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTSeasonalBarCodePrint(TSeasonalBarCodePrint tbarcodeprint)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTSeasonalBarCodePrint";

            cmd.Parameters.AddWithValue("@PkSrNo", tbarcodeprint.PkSrNo); 

            cmd.Parameters.AddWithValue("@BillNo", tbarcodeprint.BillNo);

            cmd.Parameters.AddWithValue("@ItemName", tbarcodeprint.ItemName);

            cmd.Parameters.AddWithValue("@MRP", tbarcodeprint.MRP);

            cmd.Parameters.AddWithValue("@Qty", tbarcodeprint.Qty);

            cmd.Parameters.AddWithValue("@Barcode",tbarcodeprint.Barcode);

            cmd.Parameters.AddWithValue("@UserID", tbarcodeprint.UserID);

            commandcollection.Add(cmd);
            return true;

        }

        public bool DeleteTSeasonalBarCodePrint(TSeasonalBarCodePrint tbarcodeprint)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTSeasonalBarCodePrint";

            cmd.Parameters.AddWithValue("@UserID", tbarcodeprint.UserID);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tbarcodeprint.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTSeasonalBarCodePrint()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TSeasonalBarCodePrint order by PkSrNo";
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

        public DataView GetTSeasonalBarCodePrintByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TSeasonalBarCodePrint where PkSrNo =" + ID;
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

        public TSeasonalBarCodePrint ModifyTSeasonalBarCodePrintByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TSeasonalBarCodePrint where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TSeasonalBarCodePrint MM = new TSeasonalBarCodePrint();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]); 
                    if (!Convert.IsDBNull(dr["BillNo"])) MM.BillNo = Convert.ToInt64(dr["BillNo"]);
                    if (!Convert.IsDBNull(dr["ItemName"])) MM.ItemName = Convert.ToString(dr["ItemName"]);
                    if (!Convert.IsDBNull(dr["MRP"])) MM.MRP = Convert.ToDouble(dr["MRP"]);
                    if (!Convert.IsDBNull(dr["Qty"])) MM.MRP = Convert.ToDouble(dr["Qty"]);
                    if (!Convert.IsDBNull(dr["Barcode"])) MM.Barcode = Convert.ToString(dr["Barcode"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TSeasonalBarCodePrint();
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

    public class TSeasonalBarCodePrint
    {
        private long mPkSrNo;
        private long mBillNo;
        private string mItemName;
        private double mMRP;
        private double mQty;
        private string mBarcode;
        private long mUserID;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
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
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

   
}
