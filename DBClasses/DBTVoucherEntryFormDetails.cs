using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBTVoucherEntryFormDetails
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddTVoucherEntryFormDetails(TVoucherEntryFormDetails tvoucherentryformdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherEntryFormDetails";

            cmd.Parameters.AddWithValue("@PkVoucherFormNo", tvoucherentryformdetails.PkVoucherFormNo);

            cmd.Parameters.AddWithValue("@FkVoucherNo", tvoucherentryformdetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherFormDate", tvoucherentryformdetails.VoucherFormDate);

            cmd.Parameters.AddWithValue("@FormNo", tvoucherentryformdetails.FormNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherentryformdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", tvoucherentryformdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tvoucherentryformdetails.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherEntryFormDetails(TVoucherEntryFormDetails tvoucherentryformdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherEntryFormDetails";

            cmd.Parameters.AddWithValue("@PkVoucherFormNo", tvoucherentryformdetails.PkVoucherFormNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tvoucherentryformdetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public TVoucherEntryFormDetails ModifyTVoucherEntryFormDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TVoucherEntryFormDetails where PkVoucherFormNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TVoucherEntryFormDetails MM = new TVoucherEntryFormDetails();
                while (dr.Read())
                {
                    MM.PkVoucherFormNo = Convert.ToInt32(dr["PkVoucherFormNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt64(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["VoucherFormDate"])) MM.VoucherFormDate = Convert.ToDateTime(dr["VoucherFormDate"]);
                    if (!Convert.IsDBNull(dr["FormNo"])) MM.FormNo = Convert.ToString(dr["FormNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TVoucherEntryFormDetails();
        }

        public DataView GetAllTVoucherEntryFormDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherEntryFormDetails order by PkVoucherFormNo";
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

        public DataView GetTVoucherEntryFormDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherEntryFormDetails where PkVoucherFormNo =" + ID;
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

     


    public class TVoucherEntryFormDetails
    {
        private long mPkVoucherFormNo;
        private long mFkVoucherNo;
        private DateTime mVoucherFormDate;
        private string mFormNo;
        private long mStatusNo;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        public long PkVoucherFormNo
        {
            get { return mPkVoucherFormNo; }
            set { mPkVoucherFormNo = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        public DateTime VoucherFormDate
        {
            get { return mVoucherFormDate; }
            set { mVoucherFormDate = value; }
        }
        public string FormNo
        {
            get { return mFormNo; }
            set { mFormNo = value; }
        }
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
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
