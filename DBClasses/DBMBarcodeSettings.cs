using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMBarcodeSettings
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;


        public bool AddMBarcodeSettings(MBarcodeSettings mbarcodesettings)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMBarcodeSettings";

            cmd.Parameters.AddWithValue("@PkSrNo", mbarcodesettings.PkSrNo);

            cmd.Parameters.AddWithValue("@SettingNo", mbarcodesettings.SettingNo);

            cmd.Parameters.AddWithValue("@BarcodeTemplateNo", mbarcodesettings.BarcodeTemplateNo);

            cmd.Parameters.AddWithValue("@SettingValue", mbarcodesettings.SettingValue);

            cmd.Parameters.AddWithValue("@IsActive", mbarcodesettings.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mbarcodesettings.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMBarcodeSettings(MBarcodeSettings mbarcodesettings)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMBarcodeSettings";

            cmd.Parameters.AddWithValue("@PkSrNo", mbarcodesettings.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mbarcodesettings.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetGetAllMBarcodeSettingsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MBarcodeSettings where PkSrNo =" + ID;
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

        public DataView MBarcodeSettings()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MBarcodeSettings order by PkSrNo";
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

        public MBarcodeSettings ModifyMBarcodeSettingsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MBarcodeSettings where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MBarcodeSettings MM = new MBarcodeSettings();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["SettingNo"])) MM.SettingNo = Convert.ToInt64(dr["SettingNo"]);
                    if (!Convert.IsDBNull(dr["BarcodeTemplateNo"])) MM.BarcodeTemplateNo = Convert.ToInt64(dr["BarcodeTemplateNo"]);
                    if (!Convert.IsDBNull(dr["SettingValue"])) MM.SettingValue = Convert.ToString(dr["SettingValue"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MBarcodeSettings();
        }

        public bool ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            //long ItemNoTemp = 0;//, BarcodeNoTemp = 0;
            //int cntref = 0;

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

    /// <summary>
    /// This Class use for MBarcodeSettings
    /// </summary>
    public class MBarcodeSettings
    {
        private long mPkSrNo;
        private long mSettingNo;
        private long mBarcodeTemplateNo;
        private string mSettingValue;
        private bool mIsActive;
        private long mCompanyNo;
        private int mStatusNo;
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
        /// This Properties use for SettingNo
        /// </summary>
        public long SettingNo
        {
            get { return mSettingNo; }
            set { mSettingNo = value; }
        }
        /// <summary>
        /// This Properties use for BarcodeTemplateNo
        /// </summary>
        public long BarcodeTemplateNo
        {
            get { return mBarcodeTemplateNo; }
            set { mBarcodeTemplateNo = value; }
        }
        /// <summary>
        /// This Properties use for SettingValue
        /// </summary>
        public string SettingValue
        {
            get { return mSettingValue; }
            set { mSettingValue = value; }
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
