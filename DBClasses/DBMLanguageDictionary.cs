using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMLanguageDictionary
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMLanguageDictionary(MLanguageDictionary mlanguagedictionary)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLanguageDictionary";

            cmd.Parameters.AddWithValue("@PkSrNo", mlanguagedictionary.PkSrNo);

            cmd.Parameters.AddWithValue("@EnglishVal", mlanguagedictionary.EnglishVal);

            cmd.Parameters.AddWithValue("@MarathiVal", mlanguagedictionary.MarathiVal);

            cmd.Parameters.AddWithValue("@HindiVal", mlanguagedictionary.HindiVal);

            cmd.Parameters.AddWithValue("@KarnatakaVal", mlanguagedictionary.KarnatakaVal);

            cmd.Parameters.AddWithValue("@CompanyNo", mlanguagedictionary.CompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mlanguagedictionary.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMLanguageDictionary(MLanguageDictionary mlanguagedictionary)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMLanguageDictionary";

            cmd.Parameters.AddWithValue("@PkSrNo", mlanguagedictionary.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mlanguagedictionary.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public MLanguageDictionary ModifyMLanguageDictionaryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MLanguageDictionary where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MLanguageDictionary MM = new MLanguageDictionary();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["EnglishVal"])) MM.EnglishVal = Convert.ToString(dr["EnglishVal"]);
                    if (!Convert.IsDBNull(dr["MarathiVal"])) MM.MarathiVal = Convert.ToString(dr["MarathiVal"]);
                    if (!Convert.IsDBNull(dr["HindiVal"])) MM.HindiVal = Convert.ToString(dr["HindiVal"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MLanguageDictionary();
        }

        public DataView GetAllMLanguageDictionary()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLanguageDictionary order by PkSrNo";
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

        public DataView GetMLanguageDictionaryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLanguageDictionary where PkSrNo =" + ID;
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
            int LangVal = Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.O_Language));
            string sql = null;
            if (LangVal == 1)
            {
                return null;
            }
            if (LangVal == 2)
            {
                switch (Column)
                {
                    case "0":
                        sql = "Select PkSrNo, EnglishVal AS 'English Val', MarathiVal AS 'Marathi Val' From MLanguageDictionary";
                        break;
                    case "Lang":
                        sql = "Select PkSrNo, EnglishVal AS 'English Val', MarathiVal AS 'Marathi Val' From MLanguageDictionary Where EnglishVal like '" + Value.Replace("'", "''") + "%'";
                        break;
                }
            }
            else if (LangVal == 3)
            {
                switch (Column)
                {
                    case "0":
                        sql = "Select PkSrNo, EnglishVal AS 'English Val',  HindiVal AS 'Hindi Val' From MLanguageDictionary";
                        break;
                    case "Lang":
                        sql = "Select PkSrNo, EnglishVal AS 'English Val',  HindiVal AS 'Hindi Val' From MLanguageDictionary Where EnglishVal like '" + Value.Replace("'", "''") + "%'";
                        break;
                }
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
    /// This Class use for MLanguageDictionary
    /// </summary>
    public class MLanguageDictionary
    {
        private long mPkSrNo;
        private string mEnglishVal;
        private string mMarathiVal;
        private string mHindiVal;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;
        private string mKarnatakaVal;

        /// <summary>
        /// This Properties use for PkSrNo
        /// </summary>
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        /// <summary>
        /// This Properties use for EnglishVal
        /// </summary>
        public string EnglishVal
        {
            get { return mEnglishVal; }
            set { mEnglishVal = value; }
        }
        public string KarnatakaVal
        {
            get { return mKarnatakaVal; }
            set { mKarnatakaVal = value; }

        }
        /// <summary>
        /// This Properties use for MarathiVal
        /// </summary>
        public string MarathiVal
        {
            get { return mMarathiVal; }
            set { mMarathiVal = value; }
        }
        /// <summary>
        /// This Properties use for HindiVal
        /// </summary>
        public string HindiVal
        {
            get { return mHindiVal; }
            set { mHindiVal = value; }
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
