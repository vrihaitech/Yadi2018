using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;


namespace OM
{
    class DBMBarcodeTemplate
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        //public static string strerrormsg;

        public bool AddMBarcodeTemplate(MBarcodeTemplate mbarcodetemplate)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMBarcodeTemplate";

            cmd.Parameters.AddWithValue("@PkSrNo", mbarcodetemplate.PkSrNo);

            cmd.Parameters.AddWithValue("@ScriptData", mbarcodetemplate.ScriptData);

            cmd.Parameters.AddWithValue("@PrinterName", mbarcodetemplate.PrinterName);

            cmd.Parameters.AddWithValue("@CompanyNo", mbarcodetemplate.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mbarcodetemplate.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mbarcodetemplate.UserDate);
            

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mbarcodetemplate.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool AddMSmsTemplate(MBarcodeTemplate mbarcodetemplate)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddSmsTemplate";

            cmd.Parameters.AddWithValue("@PkSrNo", mbarcodetemplate.PkSrNo);

            cmd.Parameters.AddWithValue("@ScriptData", mbarcodetemplate.ScriptData);
      
            cmd.Parameters.AddWithValue("@CompanyNo", mbarcodetemplate.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mbarcodetemplate.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mbarcodetemplate.UserDate);


            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mbarcodetemplate.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MBarcodeTemplate ModifyMBarcodeTemplateByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MBarcodeTemplate where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MBarcodeTemplate MM = new MBarcodeTemplate();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["TemplateName"])) MM.TemplateName = Convert.ToString(dr["TemplateName"]);
                    if (!Convert.IsDBNull(dr["Size"])) MM.Size = Convert.ToString(dr["Size"]);
                    if (!Convert.IsDBNull(dr["DefaultScript"])) MM.DefaultScript = Convert.ToString(dr["DefaultScript"]);
                    if (!Convert.IsDBNull(dr["ScriptData"])) MM.ScriptData = Convert.ToString(dr["ScriptData"]);
                    if (!Convert.IsDBNull(dr["PrinterName"])) MM.PrinterName = Convert.ToString(dr["PrinterName"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MBarcodeTemplate();
        }

        public DataView GetAllMBarcodeTemplate()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MBarcodeTemplate order by PkSrNo";
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

        public DataView GetMBarcodeTemplateByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MBarcodeTemplate where PkSrNo =" + ID;
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

    /// <summary>
    /// This Class use for MBarcodeTemplate
    /// </summary>
    public class MBarcodeTemplate
    {
        private long mPkSrNo;
        private string mTemplateName;
        private string mSize;
        private int mNoOfColumn;
        private string mDefaultScript;
        private string mScriptData;
        private string mPrinterName;
        private long mCompanyNo;
        private long mStatusNo;
        private long mUserID;
        private DateTime mUserDate;
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
        /// This Properties use for TemplateName
        /// </summary>
        public string TemplateName
        {
            get { return mTemplateName; }
            set { mTemplateName = value; }
        }
        /// <summary>
        /// This Properties use for Size
        /// </summary>
        public string Size
        {
            get { return mSize; }
            set { mSize = value; }
        }
        /// <summary>
        /// This Properties use for NoOfColumn
        /// </summary>
        public int NoOfColumn
        {
            get { return mNoOfColumn; }
            set { mNoOfColumn = value; }
        }
        /// <summary>
        /// This Properties use for DefaultScript
        /// </summary>
        public string DefaultScript
        {
            get { return mDefaultScript; }
            set { mDefaultScript = value; }
        }
        /// <summary>
        /// This Properties use for ScriptData
        /// </summary>
        public string ScriptData
        {
            get { return mScriptData; }
            set { mScriptData = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
