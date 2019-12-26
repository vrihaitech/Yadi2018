using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{

    class DBMBarcodePrefix
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();


        public MBarcodePrefix ModifyBarcodePrefixByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MBarcodePrefix where PkPrefixBarcodeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MBarcodePrefix MM = new MBarcodePrefix();
                while (dr.Read())
                {
                    MM.PkPrefixBarcodeNo = Convert.ToInt32(dr["PkPrefixBarcodeNo"]);
                    if (!Convert.IsDBNull(dr["PrefixBarcode"])) MM.PrefixBarcode = Convert.ToString(dr["PrefixBarcode"]);
                    if (!Convert.IsDBNull(dr["MainGroupNo"])) MM.MainGroupNo = Convert.ToInt64(dr["MainGroupNo"]);
                    if (!Convert.IsDBNull(dr["BrandNo"])) MM.BrandNo = Convert.ToInt64(dr["BrandNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["DepartmentNo"])) MM.DepartmentNo = Convert.ToInt64(dr["DepartmentNo"]);
                    if (!Convert.IsDBNull(dr["CategoryNo"])) MM.CategoryNo = Convert.ToInt64(dr["CategoryNo"]);
                    if (!Convert.IsDBNull(dr["SalesTaxSettingNo"])) MM.SalesTaxSettingNo = Convert.ToInt64(dr["SalesTaxSettingNo"]);
                    if (!Convert.IsDBNull(dr["PurchaseTaxSettingNo"])) MM.PurchaseTaxSettingNo = Convert.ToInt64(dr["PurchaseTaxSettingNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MBarcodePrefix();
        }

        public DataView GetAllBarcodePrefix()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MBarcodePrefix order by PkPrefixBarcodeNo";
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

        public bool AddMBarcodePrefix(MBarcodePrefix mbarcodeprefix)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMBarcodePrefix";

            cmd.Parameters.AddWithValue("@PkPrefixBarcodeNo", mbarcodeprefix.PkPrefixBarcodeNo);
            cmd.Parameters.AddWithValue("@PrefixBarcode", mbarcodeprefix.PrefixBarcode);
            cmd.Parameters.AddWithValue("@MainGroupNo", mbarcodeprefix.MainGroupNo);
            cmd.Parameters.AddWithValue("@BrandNo", mbarcodeprefix.BrandNo);
            cmd.Parameters.AddWithValue("@CompanyNo", mbarcodeprefix.CompanyNo);
            cmd.Parameters.AddWithValue("@DepartmentNo", mbarcodeprefix.DepartmentNo);
            cmd.Parameters.AddWithValue("@CategoryNo", mbarcodeprefix.CategoryNo);
            cmd.Parameters.AddWithValue("@SalesTaxSettingNo", mbarcodeprefix.SalesTaxSettingNo);
            cmd.Parameters.AddWithValue("@PurchaseTaxSettingNo", mbarcodeprefix.PurchaseTaxSettingNo);
            cmd.Parameters.AddWithValue("@IsActive", mbarcodeprefix.IsActive);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBarcodePrefix(MBarcodePrefix mbarcodeprefix)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMBarcodePrefix";

            cmd.Parameters.AddWithValue("@PkPrefixBarcodeNo", mbarcodeprefix.PkPrefixBarcodeNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// This Class use for MBarcodePrefix
    /// </summary>
    public class MBarcodePrefix
    {
        private long mPkPrefixBarcodeNo;
        private string mPrefixBarcode;
        private long mMainGroupNo;
        private long mBrandNo;
        private long mCompanyNo;
        private long mDepartmentNo;
        private long mCategoryNo;
        private long mSalesTaxSettingNo;
        private long mPurchaseTaxSettingNo;
        private bool mIsActive;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkPrefixBarcodeNo
        /// </summary>
        public long PkPrefixBarcodeNo
        {
            get { return mPkPrefixBarcodeNo; }
            set { mPkPrefixBarcodeNo = value; }
        }
        /// <summary>
        /// This Properties use for PrefixBarcode
        /// </summary>
        public string PrefixBarcode
        {
            get { return mPrefixBarcode; }
            set { mPrefixBarcode = value; }
        }
        /// <summary>
        /// This Properties use for MainGroupNo
        /// </summary>
        public long MainGroupNo
        {
            get { return mMainGroupNo; }
            set { mMainGroupNo = value; }
        }
        /// <summary>
        /// This Properties use for BrandNo
        /// </summary>
        public long BrandNo
        {
            get { return mBrandNo; }
            set { mBrandNo = value; }
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
        /// This Properties use for DepartmentNo
        /// </summary>
        public long DepartmentNo
        {
            get { return mDepartmentNo; }
            set { mDepartmentNo = value; }
        }
        /// <summary>
        /// This Properties use for CategoryNo
        /// </summary>
        public long CategoryNo
        {
            get { return mCategoryNo; }
            set { mCategoryNo = value; }
        }
        /// <summary>
        /// This Properties use for SalesTaxSettingNo
        /// </summary>
        public long SalesTaxSettingNo
        {
            get { return mSalesTaxSettingNo; }
            set { mSalesTaxSettingNo = value; }
        }
        /// <summary>
        /// This Properties use for PurchaseTaxSettingNo
        /// </summary>
        public long PurchaseTaxSettingNo
        {
            get { return mPurchaseTaxSettingNo; }
            set { mPurchaseTaxSettingNo = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
