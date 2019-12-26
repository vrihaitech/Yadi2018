using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OMControls;

namespace OM
{
    class DBMRegistration
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        Security secure = new Security();

        public static string strerrormsg;

        public bool AddMRegistration(MRegistration mregistration)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRegistration";

            cmd.Parameters.AddWithValue("@RegNo", mregistration.RegNo);

            cmd.Parameters.AddWithValue("@MacID", secure.psEncrypt(mregistration.MacID));

            cmd.Parameters.AddWithValue("@HostName", mregistration.HostName);

            cmd.Parameters.AddWithValue("@MachineIP", mregistration.MachineIP);

            cmd.Parameters.AddWithValue("@IsActive", mregistration.IsActive);

            cmd.Parameters.AddWithValue("@IsManual", mregistration.IsManual);

            cmd.Parameters.AddWithValue("@CompanyNo", mregistration.CompanyNo);
           
            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMRegistrationDetails(MRegistrationDetails mregistrationdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRegistrationDetails";

            cmd.Parameters.AddWithValue("@RegDtlsNo", mregistrationdetails.RegDtlsNo);

            //cmd.Parameters.AddWithValue("@RegNo", mregistrationdetails.RegNo);

            cmd.Parameters.AddWithValue("@RegDate", mregistrationdetails.RegDate);

            cmd.Parameters.AddWithValue("@RegTime", mregistrationdetails.RegTime);

            cmd.Parameters.AddWithValue("@CompanyNo", mregistrationdetails.CompanyNo);
            
            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMRegistration(MRegistration mregistration)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMRegistration";

            cmd.Parameters.AddWithValue("@RegNo", mregistration.RegNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mregistration.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMRegistrationDetails(MRegistrationDetails mregistrationdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMRegistrationDetails";

            cmd.Parameters.AddWithValue("@RegDtlsNo", mregistrationdetails.RegDtlsNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mregistrationdetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MRegistration ModifyMRegistrationByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MRegistration where RegNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MRegistration MM = new MRegistration();
                while (dr.Read())
                {
                    MM.RegNo = Convert.ToInt32(dr["RegNo"]);
                    if (!Convert.IsDBNull(dr["MacID"])) MM.MacID = secure.psDecrypt(Convert.ToString(dr["MacID"]));
                    if (!Convert.IsDBNull(dr["HostName"])) MM.HostName = Convert.ToString(dr["HostName"]);
                    if (!Convert.IsDBNull(dr["MachineIP"])) MM.MachineIP = Convert.ToString(dr["MachineIP"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["IsManual"])) MM.IsManual = Convert.ToBoolean(dr["IsManual"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MRegistration();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select Distinct RegNo,HostName AS 'Host Name',Case When (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRegistration order by HostName";
                    break;
                case "HostName":
                    sql = "Select Distinct RegNo,HostName AS 'Host Name',Case When (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRegistration where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by HostName";
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
                        if (commandcollection[i].CommandText == "AddMRegistrationDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@RegNo", commandcollection[0].Parameters["@ReturnID"].Value);
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

    /// <summary>
    /// This Class use for MRegistration
    /// </summary>
    public class MRegistration
    {
        private long mRegNo;
        private string mMacID;
        private string mHostName;
        private string mMachineIP;
        private bool mIsActive;
        private bool mIsManual;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for RegNo
        /// </summary>
        public long RegNo
        {
            get { return mRegNo; }
            set { mRegNo = value; }
        }
        /// <summary>
        /// This Properties use for MacID
        /// </summary>
        public string MacID
        {
            get { return mMacID; }
            set { mMacID = value; }
        }
        /// <summary>
        /// This Properties use for HostName
        /// </summary>
        public string HostName
        {
            get { return mHostName; }
            set { mHostName = value; }
        }
        /// <summary>
        /// This Properties use for MachineIP
        /// </summary>
        public string MachineIP
        {
            get { return mMachineIP; }
            set { mMachineIP = value; }
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
        /// This Properties use for IsManual
        /// </summary>
        public bool IsManual
        {
            get { return mIsManual; }
            set { mIsManual = value; }
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

    /// <summary>
    /// This Class use for MRegistrationDetails
    /// </summary>
    public class MRegistrationDetails
    {
        private long mRegDtlsNo;
        private long mRegNo;
        private DateTime mRegDate;
        private DateTime mRegTime;
        private int mStatusNo;
        private long mCompanyNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for RegDtlsNo
        /// </summary>
        public long RegDtlsNo
        {
            get { return mRegDtlsNo; }
            set { mRegDtlsNo = value; }
        }
        /// <summary>
        /// This Properties use for RegNo
        /// </summary>
        public long RegNo
        {
            get { return mRegNo; }
            set { mRegNo = value; }
        }
        /// <summary>
        /// This Properties use for RegDate
        /// </summary>
        public DateTime RegDate
        {
            get { return mRegDate; }
            set { mRegDate = value; }
        }
        /// <summary>
        /// This Properties use for RegTime
        /// </summary>
        public DateTime RegTime
        {
            get { return mRegTime; }
            set { mRegTime = value; }
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
        /// This Properties use for CompanyNo
        /// </summary>
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
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
