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
    class DBMPrinter
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMPrinter(MPrinter mprinter)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMPrinter";

            cmd.Parameters.AddWithValue("@PrinterNo", mprinter.PrinterNo);

            cmd.Parameters.AddWithValue("@PrinterName", mprinter.PrinterName);

            cmd.Parameters.AddWithValue("@MachineName", mprinter.MachineName);

            cmd.Parameters.AddWithValue("@MachineIP", mprinter.MachineIP);

            cmd.Parameters.AddWithValue("@IsDefault", mprinter.IsDefault);

            cmd.Parameters.AddWithValue("@IsActive", mprinter.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mprinter.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mprinter.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mprinter.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mprinter.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMPrinter(MPrinter mprinter)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMPrinter";

            cmd.Parameters.AddWithValue("@PrinterNo", mprinter.PrinterNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mprinter.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMPrinter()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPrinter order by PrinterNo";
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

        public DataView GetMPrinterByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MPrinter where PrinterNo =" + ID;
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

        public MPrinter ModifyMPrinterByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MPrinter where PrinterNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MPrinter MM = new MPrinter();
                while (dr.Read())
                {
                    MM.PrinterNo = Convert.ToInt32(dr["PrinterNo"]);
                    if (!Convert.IsDBNull(dr["PrinterName"])) MM.PrinterName = Convert.ToString(dr["PrinterName"]);
                    if (!Convert.IsDBNull(dr["MachineName"])) MM.MachineName = Convert.ToString(dr["MachineName"]);
                    if (!Convert.IsDBNull(dr["MachineIP"])) MM.MachineIP = Convert.ToString(dr["MachineIP"]);
                    if (!Convert.IsDBNull(dr["IsDefault"])) MM.IsDefault = Convert.ToBoolean(dr["IsDefault"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MPrinter();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select PrinterNo,PrinterName AS 'Printer Type' from MPrinter order by PrinterName";
                    break;
                case "PrinterName":
                    sql = "Select PrinterNo,PrinterName AS 'Printer Type' from MPrinter where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by PrinterName";
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
    }

    /// <summary>
    /// This Class use for MPrinter
    /// </summary>
    public class MPrinter
    {
        private long mPrinterNo;
        private string mPrinterName;
        private string mMachineName;
        private string mMachineIP;
        private bool mIsDefault;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PrinterNo
        /// </summary>
        public long PrinterNo
        {
            get { return mPrinterNo; }
            set { mPrinterNo = value; }
        }
        /// <summary>
        /// This Properties use for PrinterName
        /// </summary>
        public string PrinterName
        {
            get { return mPrinterName; }
            set { mPrinterName = value; }
        }
        /// <summary>
        /// This Properties use for MachineName
        /// </summary>
        public string MachineName
        {
            get { return mMachineName; }
            set { mMachineName = value; }
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
        /// This Properties use for IsDefault
        /// </summary>
        public bool IsDefault
        {
            get { return mIsDefault; }
            set { mIsDefault = value; }
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
        /// This Properties use for UserID
        /// </summary>
        public long UserID
        {
            get { return mUserID; }
            set { mUserID = value; }
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
        /// This Properties use for ModifiedBy
        /// </summary>
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
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
