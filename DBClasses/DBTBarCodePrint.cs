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
    class DBTBarCodePrint
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTBarCodePrint(TBarCodePrint tbarcodeprint)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTBarCodePrint";

            cmd.Parameters.AddWithValue("@PkSrNo", tbarcodeprint.PkSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", tbarcodeprint.ItemNo);

            cmd.Parameters.AddWithValue("@Quantity", tbarcodeprint.Quantity);

            cmd.Parameters.AddWithValue("@FKRateSettingNo", tbarcodeprint.FKRateSettingNo);

            cmd.Parameters.AddWithValue("@MacNo", tbarcodeprint.MacNo);

            cmd.Parameters.AddWithValue("@UserID", tbarcodeprint.UserID);

            commandcollection.Add(cmd);
            return true;

        }

        public bool DeleteTBarCodePrint(TBarCodePrint tbarcodeprint)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTBarCodePrint";

            cmd.Parameters.AddWithValue("@MacNo", tbarcodeprint.MacNo);

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

        public DataView GetAllTBarCodePrint()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TBarCodePrint order by PkSrNo";
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

        public DataView GetTBarCodePrintByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TBarCodePrint where PkSrNo =" + ID;
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

        public TBarCodePrint ModifyTBarCodePrintByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TBarCodePrint where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TBarCodePrint MM = new TBarCodePrint();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["Quantity"])) MM.Quantity = Convert.ToInt64(dr["Quantity"]);
                    if (!Convert.IsDBNull(dr["FKRateSettingNo"])) MM.FKRateSettingNo = Convert.ToInt64(dr["FKRateSettingNo"]);
                    if (!Convert.IsDBNull(dr["MacNo"])) MM.MacNo = Convert.ToInt64(dr["MacNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TBarCodePrint();
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

        public bool AddTWeighingBarCodePrint(TWeighingBarCodePrint tweighingbarcodeprint)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTWeighingBarCodePrint";

            cmd.Parameters.AddWithValue("@PkSrNo", tweighingbarcodeprint.PkSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", tweighingbarcodeprint.ItemNo);

            cmd.Parameters.AddWithValue("@BarCode", tweighingbarcodeprint.BarCode);

            cmd.Parameters.AddWithValue("@ActualQuantity", tweighingbarcodeprint.ActualQuantity);

            cmd.Parameters.AddWithValue("@Quantity", tweighingbarcodeprint.Quantity);

            cmd.Parameters.AddWithValue("@FKRateSettingNo", tweighingbarcodeprint.FKRateSettingNo);

            cmd.Parameters.AddWithValue("@MacNo", tweighingbarcodeprint.MacNo);

            cmd.Parameters.AddWithValue("@UserID", tweighingbarcodeprint.UserID);

            commandcollection.Add(cmd);
            return true;

        }

        public bool DeleteTWeighingBarCodePrint(TWeighingBarCodePrint tweighingbarcodeprint)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTWeighingBarCodePrint";

            cmd.Parameters.AddWithValue("@MacNo", tweighingbarcodeprint.MacNo);

            cmd.Parameters.AddWithValue("@UserID", tweighingbarcodeprint.UserID);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tweighingbarcodeprint.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }
    }

    /// <summary>
    /// This Class use for TBarCodePrint
    /// </summary>
    public class TBarCodePrint
    {
        private long mPkSrNo;
        private long mItemNo;
        private double mQuantity;
        private long mFKRateSettingNo;
        private long mMacNo;
        private long mUserID;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkSrNo
        /// </summary>
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        /// <summary>
        /// This Properties use for ItemNo
        /// </summary>
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        /// <summary>
        /// This Properties use for Quantity
        /// </summary>
        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        /// <summary>
        /// This Properties use for FKRateSettingNo
        /// </summary>
        public long FKRateSettingNo
        {
            get { return mFKRateSettingNo; }
            set { mFKRateSettingNo = value; }
        }
        /// <summary>
        /// This Properties use for MacNo
        /// </summary>
        public long MacNo
        {
            get { return mMacNo; }
            set { mMacNo = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TWeighingBarCodePrint
    {
        private long mPkSrNo;
        private long mItemNo;
        private string mBarCode;
        private double mActualQuantity;
        private double mQuantity;
        private long mFKRateSettingNo;
        private long mMacNo;
        private long mUserID;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkSrNo
        /// </summary>
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        /// <summary>
        /// This Properties use for ItemNo
        /// </summary>
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        /// <summary>
        /// This Properties use for Barcode
        /// </summary>
        public string BarCode
        {
            get { return mBarCode; }
            set { mBarCode = value; }
        }
        /// <summary>
        /// This Properties use for Actual Quantity
        /// </summary>
        public double ActualQuantity
        {
            get { return mActualQuantity; }
            set { mActualQuantity = value; }
        }
        /// <summary>
        /// This Properties use for Quantity
        /// </summary>
        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        /// <summary>
        /// This Properties use for FKRateSettingNo
        /// </summary>
        public long FKRateSettingNo
        {
            get { return mFKRateSettingNo; }
            set { mFKRateSettingNo = value; }
        }
        /// <summary>
        /// This Properties use for MacNo
        /// </summary>
        public long MacNo
        {
            get { return mMacNo; }
            set { mMacNo = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
   
}
