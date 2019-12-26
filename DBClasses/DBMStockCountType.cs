using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
   
    class DBMStockCountType
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        string strerrormsg = "";

        public bool AddMStockCountType(MStockCountType mstockcounttype)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockCountType";

            cmd.Parameters.AddWithValue("@CountTypeNo", mstockcounttype.CountTypeNo);

//            cmd.Parameters.AddWithValue("@CountTypeName", mstockcounttype.CountTypeName);

            cmd.Parameters.AddWithValue("@DefaultValue", mstockcounttype.DefaultValue);

            cmd.Parameters.AddWithValue("@IsActive", mstockcounttype.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockcounttype.CompanyNo);

            cmd.Parameters.AddWithValue("@UserDate", mstockcounttype.UserDate);

            cmd.Parameters.AddWithValue("@UserID", mstockcounttype.UserID);


            commandcollection.Add(cmd);
            return true;
        }

        public MStockCountType ModifyMStockCountTypeByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MStockCountType where CountTypeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MStockCountType MM = new MStockCountType();
                while (dr.Read())
                {
                    MM.CountTypeNo = Convert.ToInt32(dr["CountTypeNo"]);
                    if (!Convert.IsDBNull(dr["CountTypeName"])) MM.CountTypeName = Convert.ToString(dr["CountTypeName"]);
                    if (!Convert.IsDBNull(dr["DefaultValue"])) MM.DefaultValue = Convert.ToDateTime(dr["DefaultValue"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MStockCountType();
        }

        public DataView GetAllMStockCountType()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockCountType order by CountTypeNo";
            SqlDataAdapter da = new SqlDataAdapter(sql, Con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                Con.Close();
            }
            return ds.Tables[(0)].DefaultView;
        }

        public DataView GetAllMStockCountType(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockCountType Where CountTypeNo=" + ID;
            SqlDataAdapter da = new SqlDataAdapter(sql, CommonFunctions.ConStr);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch
            {
                throw;
            }
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

    /// <summary>
    /// This Class use for MStockCountType
    /// </summary>
    public class MStockCountType
    {
        private long mCountTypeNo;
        private string mCountTypeName;
        private DateTime mDefaultValue;
        private bool mIsActive;
        private long mCompanyNo;
        private long mStatusNo;
        private DateTime mUserDate;
        private long mUserID;
        private string Mmsg;

        /// <summary>
        /// This Properties use for CountTypeNo
        /// </summary>
        public long CountTypeNo
        {
            get { return mCountTypeNo; }
            set { mCountTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for CountTypeName
        /// </summary>
        public string CountTypeName
        {
            get { return mCountTypeName; }
            set { mCountTypeName = value; }
        }
        /// <summary>
        /// This Properties use for DefaultValue
        /// </summary>
        public DateTime DefaultValue
        {
            get { return mDefaultValue; }
            set { mDefaultValue = value; }
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
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
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
