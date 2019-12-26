using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMLedgerGroup
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMLedgerGroup(MLedgerGroup MLedgerGroup)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLedgerGroup";

            cmd.Parameters.AddWithValue("@LedgerGroupNo", MLedgerGroup.LedgerGroupNo);

            cmd.Parameters.AddWithValue("@LedgerName", MLedgerGroup.LedgerName);

            cmd.Parameters.AddWithValue("@LedgerLangName", MLedgerGroup.LedgerLangName);

            cmd.Parameters.AddWithValue("@GroupNo", MLedgerGroup.GroupNo);

            cmd.Parameters.AddWithValue("@IsActive", MLedgerGroup.IsActive);

            cmd.Parameters.AddWithValue("@UserID", MLedgerGroup.UserID);

            cmd.Parameters.AddWithValue("@UserDate", MLedgerGroup.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", MLedgerGroup.CompanyNo);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);
            commandcollection.Add(cmd);
            return true;
         
        }

        public bool DeleteMLedgerGroup(MLedgerGroup MLedgerGroup)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMLedgerGroup";

            cmd.Parameters.AddWithValue("@LedgerGroupNo", MLedgerGroup.LedgerGroupNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                MLedgerGroup.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMLedgerGroup()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLedgerGroup order by LedgerGroupNo";
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

        public MLedgerGroup ModifyMLedgerGroupByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MLedgerGroup where LedgerGroupNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MLedgerGroup MM = new MLedgerGroup();
                while (dr.Read())
                {
                    MM.LedgerGroupNo = Convert.ToInt32(dr["LedgerGroupNo"]);
                    if (!Convert.IsDBNull(dr["LedgerName"])) MM.LedgerName = Convert.ToString(dr["LedgerName"]);
                    if (!Convert.IsDBNull(dr["LedgerLangName"])) MM.LedgerLangName = Convert.ToString(dr["LedgerLangName"]);
                    if (!Convert.IsDBNull(dr["GroupNo"])) MM.GroupNo = Convert.ToInt64(dr["GroupNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MLedgerGroup();
        }

        public DataView GetMLedgerGroupByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLedgerGroup where LedgerGroupNo =" + ID;
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
                    sql = "Select LedgerGroupNo,LedgerName AS 'Ledger Group Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedgerGroup ";
                    break;
                case "LedgerName":
                    sql = "Select LedgerGroupNo,LedgerName AS 'Ledger Group Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedgerGroup where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
                    break;
                case "GroupNo":
                    sql = "Select LedgerGroupNo,LedgerName AS 'Ledger Group Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedgerGroup where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
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

        public bool AddMLedgerGroupDetails(MLedgerGroupDetails MLedgerGroupDetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLedgerGroupDetails";

            cmd.Parameters.AddWithValue("@LedgerGrpDetailsNo", MLedgerGroupDetails.LedgerGrpDetailsNo);

            cmd.Parameters.AddWithValue("@LedgerGroupNo", MLedgerGroupDetails.LedgerGroupNo);

            cmd.Parameters.AddWithValue("@LedgerNo", MLedgerGroupDetails.LedgerNo);
        
            cmd.Parameters.AddWithValue("@IsActive", MLedgerGroupDetails.IsActive);

            cmd.Parameters.AddWithValue("@UserID", MLedgerGroupDetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", MLedgerGroupDetails.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", MLedgerGroupDetails.CompanyNo);

            //commandcollection.Add(cmd);
            //return true;

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                MLedgerGroupDetails.msg = ObjTrans.ErrorMessage;
                return false;
            }

        }

        public bool ExecuteNonQueryStatements()
        {
            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            int cntref = 0;
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
                        if (commandcollection[i].CommandText == "AddMLedgerGroup")
                        {
                            // commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);
                            cntref = i;
                        }
                        else if (commandcollection[i].CommandText == "AddMLedgerGroupDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@LedgerGroupNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
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

    public class MLedgerGroup
    {
        private long MLedgerGroupNo;
        private string MLedgerName;
        private string MLedgerLangName;
        private long MGroupNo;   
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private string Mmsg;

        public long LedgerGroupNo
        {
            get { return MLedgerGroupNo; }
            set { MLedgerGroupNo = value; }
        }

        public string LedgerName
        {
            get { return MLedgerName; }
            set { MLedgerName = value; }
        }
        public string LedgerLangName
        {
            get { return MLedgerLangName; }
            set { MLedgerLangName = value; }
        }
        public long GroupNo
        {
            get { return MGroupNo; }
            set { MGroupNo = value; }
        }
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
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
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class MLedgerGroupDetails
    {
        private long MLedgerGrpDetailsNo;
        private long MLedgerGroupNo;
        private long MLedgerNo;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private string Mmsg;

        public long LedgerGrpDetailsNo
        {
            get { return MLedgerGrpDetailsNo; }
            set { MLedgerGrpDetailsNo = value; }
        }
        public long LedgerGroupNo
        {
            get { return MLedgerGroupNo; }
            set { MLedgerGroupNo = value; }
        }
        public long LedgerNo
        {
            get { return MLedgerNo; }
            set { MLedgerNo = value; }
        }
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
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
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
