using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBTChequeNo
    {
        
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTChequeNo(TChequeNo tchequeno)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTChequeNo";

            cmd.Parameters.AddWithValue("@PkChequeNo", tchequeno.PkChequeNo);

            cmd.Parameters.AddWithValue("@ChequeUserNo", tchequeno.ChequeUserNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tchequeno.LedgerNo);

            cmd.Parameters.AddWithValue("@FromChequeNo", tchequeno.FromChequeNo);

            cmd.Parameters.AddWithValue("@ToChequeNo", tchequeno.ToChequeNo);

            cmd.Parameters.AddWithValue("@Remark", tchequeno.Remark);

            cmd.Parameters.AddWithValue("@IsActive", tchequeno.IsActive);

            cmd.Parameters.AddWithValue("@UserId", tchequeno.UserId);

            cmd.Parameters.AddWithValue("@UserDate", tchequeno.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTChequeNo(TChequeNo tchequeno)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTChequeNo";

            cmd.Parameters.AddWithValue("@PkChequeNo", tchequeno.PkChequeNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tchequeno.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetTCouponByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TCoupon where CouponNo =" + ID;
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

        public DataView GetAllTCoupon()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TCoupon order by CouponNo";
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

        public TChequeNo ModifyTChequeNoByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TChequeNo where PkChequeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TChequeNo MM = new TChequeNo();
                while (dr.Read())
                {
                    MM.PkChequeNo = Convert.ToInt32(dr["PkChequeNo"]);
                    if (!Convert.IsDBNull(dr["ChequeUserNo"])) MM.ChequeUserNo = Convert.ToInt64(dr["ChequeUserNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["FromChequeNo"])) MM.FromChequeNo = Convert.ToInt64(dr["FromChequeNo"]);
                    if (!Convert.IsDBNull(dr["ToChequeNo"])) MM.ToChequeNo = Convert.ToInt64(dr["ToChequeNo"]);
                    if (!Convert.IsDBNull(dr["Remark"])) MM.Remark = Convert.ToString(dr["Remark"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserId"])) MM.UserId = Convert.ToInt64(dr["UserId"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TChequeNo();
        }

        public DataView GetBySearch(string Column, string Value)
        {
            Value = Value.Replace("'", "''");
            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = " SELECT DISTINCT TChequeNo.PkChequeNo, MLedger.LedgerName as 'Bank Name', TChequeNo.FromChequeNo as 'Cheque No From', "+
                          " (SELECT     COUNT(FkChequeNo) AS Expr1 "+
                          " FROM          TChequeNoDetails "+
                          "  WHERE      (FkChequeNo = TChequeNo.PkChequeNo) " +
                          "  GROUP BY FkChequeNo) AS 'No Of Cheques', "+ 
                          " TChequeNo.ToChequeNo as 'Cheque No To', CASE WHEN (TChequeNo.IsActive = 'True') "+
                          " THEN 'True' ELSE 'False' END AS 'Status' FROM TChequeNo INNER JOIN "+
                          " MLedger ON TChequeNo.LedgerNo = MLedger.LedgerNo INNER JOIN "+
                          " TChequeNoDetails ON TChequeNo.PkChequeNo = TChequeNoDetails.FkChequeNo "+
                          " ORDER BY MLedger.LedgerName ";
                    break;
            
                case "BankName":
                    sql = " SELECT DISTINCT TChequeNo.PkChequeNo, MLedger.LedgerName as 'Bank Name', TChequeNo.FromChequeNo, " +
                          " TChequeNo.ToChequeNo , "+
                          " (SELECT     COUNT(FkChequeNo) AS Expr1 "+
                          "  FROM          TChequeNoDetails "+
                          "  WHERE      (FkChequeNo = TChequeNo.PkChequeNo)"+
                          "  GROUP BY FkChequeNo) AS 'No Of Cheques', CASE WHEN (TChequeNo.IsActive = 'True') " +
                          " THEN 'True' ELSE 'False' END AS 'Status' FROM TChequeNo INNER JOIN " +
                          " MLedger ON TChequeNo.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                          " TChequeNoDetails ON TChequeNo.PkChequeNo = TChequeNoDetails.FkChequeNo " +
                          " WHERE (MLedger.LedgerName LIKE '" + Value.Trim() + "%') ORDER BY MLedger.LedgerName ";
                    break;

            }
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException )
            {

            }
            return ds.Tables[0].DefaultView;
        }


        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int cntRef = 0;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddTChequeNoDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkChequeNo", commandcollection[0].Parameters["@ReturnID"].Value);
                            cntRef = i;
                        }
                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                } 

                myTrans.Commit();
                return Convert.ToInt64(commandcollection[0].Parameters["@ReturnID"].Value);
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
                return 0;
            }
            finally
            {
                cn.Close();
            }
            //________________________________________________________________________________________________________________________________________________________________________________________________________________________
        }


        public bool AddTChequeNoDetails(TChequeNoDetails tchequenodetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTChequeNoDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tchequenodetails.PkSrNo);

            //cmd.Parameters.AddWithValue("@FkChequeNo", tchequenodetails.FkChequeNo);

            cmd.Parameters.AddWithValue("@ChequeDetailsUserNo", tchequenodetails.ChequeDetailsUserNo);

            cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tchequenodetails.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@IsActive", tchequenodetails.IsActive);

            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetTChequeNoDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TChequeNoDetails where PkSrNo =" + ID;
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

        public bool DeleteTChequeNoDetails(TChequeNoDetails tchequenodetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTChequeNoDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tchequenodetails.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tchequenodetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTChequeNoDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TChequeNoDetails order by PkSrNo";
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

        public TChequeNoDetails ModifyTChequeNoDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TChequeNoDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TChequeNoDetails MM = new TChequeNoDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["FkChequeNo"])) MM.FkChequeNo = Convert.ToInt64(dr["FkChequeNo"]);
                    if (!Convert.IsDBNull(dr["ChequeDetailsUserNo"])) MM.ChequeDetailsUserNo = Convert.ToString(dr["ChequeDetailsUserNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherTrnNo"])) MM.FkVoucherTrnNo = Convert.ToInt64(dr["FkVoucherTrnNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TChequeNoDetails();
        }

        

    }

    public class TChequeNo
    {
        private long mPkChequeNo;
        private long mChequeUserNo;
        private long mLedgerNo;
        private long mFromChequeNo;
        private long mToChequeNo;
        private string mRemark;
        private bool mIsActive;
        private long mUserId;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        public long PkChequeNo
        {
            get { return mPkChequeNo; }
            set { mPkChequeNo = value; }
        }
        public long ChequeUserNo
        {
            get { return mChequeUserNo; }
            set { mChequeUserNo = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public long FromChequeNo
        {
            get { return mFromChequeNo; }
            set { mFromChequeNo = value; }
        }
        public long ToChequeNo
        {
            get { return mToChequeNo; }
            set { mToChequeNo = value; }
        }
        public string Remark
        {
            get { return mRemark; }
            set { mRemark = value; }
        }
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }
        public long UserId
        {
            get { return mUserId; }
            set { mUserId = value; }
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

    public class TChequeNoDetails
    {
        private long mPkSrNo;
        private long mFkChequeNo;
        private string mChequeDetailsUserNo;
        private long mFkVoucherTrnNo;
        private bool mIsActive;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long FkChequeNo
        {
            get { return mFkChequeNo; }
            set { mFkChequeNo = value; }
        }
        public string ChequeDetailsUserNo
        {
            get { return mChequeDetailsUserNo; }
            set { mChequeDetailsUserNo = value; }
        }
        public long FkVoucherTrnNo
        {
            get { return mFkVoucherTrnNo; }
            set { mFkVoucherTrnNo = value; }
        }
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }


}
