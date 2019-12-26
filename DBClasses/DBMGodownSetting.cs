using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBMGodownSetting
    {
       
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public CommandCollection commandcollection = new CommandCollection();       

        public static string strerrormsg;

        public bool AddMGodownSetting(MGodownSetting mgodownsetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMGodownSetting";

            cmd.Parameters.AddWithValue("@PkGodownSettingNo", mgodownsetting.PkGodownSettingNo);

            cmd.Parameters.AddWithValue("@FkBcdSrNo", mgodownsetting.FkBcdSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", mgodownsetting.ItemNo);

            cmd.Parameters.AddWithValue("@UOMNo", mgodownsetting.UOMNo);

            cmd.Parameters.AddWithValue("@QuantitySlabFrom", mgodownsetting.QuantitySlabFrom);

            cmd.Parameters.AddWithValue("@QuantitySlabTo", mgodownsetting.QuantitySlabTo);

            cmd.Parameters.AddWithValue("@GodownNo", mgodownsetting.GodownNo);

            cmd.Parameters.AddWithValue("@UserID", mgodownsetting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mgodownsetting.UserDate); 

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

        public bool DeleteMGodownSetting(MGodownSetting mgodownsetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMGodownSetting";

            cmd.Parameters.AddWithValue("@PkGodownSettingNo", mgodownsetting.PkGodownSettingNo);
            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllMGodownSetting()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MGodownSetting order by PkGodownSettingNo";
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

        public MGodownSetting ModifyMGodownSettingByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MGodownSetting where PkGodownSettingNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MGodownSetting MM = new MGodownSetting();
                while (dr.Read())
                {
                    MM.PkGodownSettingNo = Convert.ToInt32(dr["PkGodownSettingNo"]);
                    if (!Convert.IsDBNull(dr["FkBcdSrNo"])) MM.FkBcdSrNo = Convert.ToInt64(dr["FkBcdSrNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["UOMNo"])) MM.UOMNo = Convert.ToInt64(dr["UOMNo"]);
                    if (!Convert.IsDBNull(dr["QuantitySlabFrom"])) MM.QuantitySlabFrom = Convert.ToDouble(dr["QuantitySlabFrom"]);
                    if (!Convert.IsDBNull(dr["QuantitySlabTo"])) MM.QuantitySlabTo = Convert.ToDouble(dr["QuantitySlabTo"]);
                    if (!Convert.IsDBNull(dr["GodownNo"])) MM.GodownNo = Convert.ToInt64(dr["GodownNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MGodownSetting();
        }

        public DataView GetMGodownSettingByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MGodownSetting where PkGodownSettingNo =" + ID;
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

    }
    public class MGodownSetting
    {
        private long mPkGodownSettingNo;
        private long mFkBcdSrNo;
        private long mItemNo;
        private long mUOMNo;
        private double mQuantitySlabFrom;
        private double mQuantitySlabTo;
        private long mGodownNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        public long PkGodownSettingNo
        {
            get { return mPkGodownSettingNo; }
            set { mPkGodownSettingNo = value; }
        }
        public long FkBcdSrNo
        {
            get { return mFkBcdSrNo; }
            set { mFkBcdSrNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public long UOMNo
        {
            get { return mUOMNo; }
            set { mUOMNo = value; }
        }
        public double QuantitySlabFrom
        {
            get { return mQuantitySlabFrom; }
            set { mQuantitySlabFrom = value; }
        }
        public double QuantitySlabTo
        {
            get { return mQuantitySlabTo; }
            set { mQuantitySlabTo = value; }
        }
        public long GodownNo
        {
            get { return mGodownNo; }
            set { mGodownNo = value; }
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
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

}
