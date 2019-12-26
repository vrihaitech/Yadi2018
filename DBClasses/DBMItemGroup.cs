using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMItemGroup
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMItemGroup(MItemGroup mitemgroup)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemGroup";

            cmd.Parameters.AddWithValue("@ItemGroupNo", mitemgroup.ItemGroupNo);

            cmd.Parameters.AddWithValue("@LangGroupName", mitemgroup.LanguageName);

            cmd.Parameters.AddWithValue("@ItemGroupName", mitemgroup.ItemGroupName);

            cmd.Parameters.AddWithValue("@ControlGroup", mitemgroup.ControlGroup);

            cmd.Parameters.AddWithValue("@ControlSubGroup", mitemgroup.ControlSubGroup);

            cmd.Parameters.AddWithValue("@IsActive", mitemgroup.IsActive);

            cmd.Parameters.AddWithValue("@UserId", mitemgroup.UserId);

            cmd.Parameters.AddWithValue("@UserDate", mitemgroup.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemgroup.CompanyNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mitemgroup.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }



        public bool DeleteMItemGroup(MItemGroup mitemgroup)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMItemGroup";

            cmd.Parameters.AddWithValue("@ItemGroupNo", mitemgroup.ItemGroupNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mitemgroup.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }
        
        public DataView GetAllMItemGroup()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockGroup order by StockGroupNo";
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
        
        public DataView GetMStockGroupByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockGroup where StockGroupNo =" + ID;
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

        public MItemGroup ModifyMStockGroupByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MItemGroup where ItemGroupNo =" + ID ;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MItemGroup MM = new MItemGroup();
                while (dr.Read())
                {
                    MM.ItemGroupNo = Convert.ToInt32(dr["ItemGroupNo"]);
                    if (!Convert.IsDBNull(dr["ItemGroupName"])) MM.ItemGroupName= Convert.ToString(dr["ItemGroupName"]);
                    if (!Convert.IsDBNull(dr["LangGroupName"])) MM.LanguageName = Convert.ToString(dr["LangGroupName"]);
                    if (!Convert.IsDBNull(dr["ControlGroup"])) MM.ControlGroup = Convert.ToInt64(dr["ControlGroup"]);
                    if (!Convert.IsDBNull(dr["ControlSubGroup"])) MM.ControlSubGroup = Convert.ToInt64(dr["ControlSubGroup"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserId"])) MM.UserId = Convert.ToInt64(dr["UserId"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                   
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MItemGroup();
        }

        public DataView GetBySearch(string Column, string Value)
        {
            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select ItemGroupNo,ItemGroupName AS 'ItemGroup Name',Case when (MItemGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MItemGroup where ControlGroup = 3";
                    break;
                case "ItemGroupName":
                    sql = "Select ItemGroupNo,ItemGroupName AS 'ItemGroup Name',Case when (MItemGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MItemGroup where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' and  ControlGroup = 3 order by ItemGroupName";
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

        public DataView GetBySearch(string Column, string Value, int Grno)
        {

            string sql = null;
            if (Grno != 3)
            {
                switch (Column)
                {
                    case "0":
                        sql = "Select StockGroupNo,StockGroupName AS 'StockGroup Name' ,'' As CompName,Case when (MStockGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MStockGroup Where StockGroupNo<>1 and ControlGroup=" + Grno + " order by StockGroupName";
                        break;
                    case "StockGroupName":
                        sql = "SELECT     StockGroupNo, StockGroupName AS 'StockGroup Name','' As CompName ,Case when (MStockGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' FROM         MStockGroup WHERE   (StockGroupNo <> 1) AND (StockGroupName LIKE '" + Value.Trim().Replace("'", "''") + "' + '%') and ControlGroup=" + Grno + " ORDER BY 'StockGroup Name'";

                        break;

                }
            }
            else
            {
                switch (Column)
                {
                    case "0":
                        sql = "Select StockGroupNo,StockGroupName AS 'StockGroup Name' ,MManufacturerCompany.MfgCompName As CompName,Case when (MStockGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MStockGroup INNER JOIN MManufacturerCompany ON MManufacturerCompany.MfgCompNo=MStockGroup.MfgCompNo Where StockGroupNo<>1 and ControlGroup=" + Grno + " order by StockGroupName";
                        break;
                    case "StockGroupName":
                        sql = "SELECT     StockGroupNo, StockGroupName AS 'StockGroup Name',MManufacturerCompany.MfgCompName As CompName ,Case when (MStockGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' FROM MStockGroup INNER JOIN MManufacturerCompany ON MManufacturerCompany.MfgCompNo=MStockGroup.MfgCompNo WHERE   (StockGroupNo <> 1) AND (StockGroupName LIKE '" + Value.Trim().Replace("'", "''") + "' + '%') and ControlGroup=" + Grno + " ORDER BY 'StockGroup Name'";

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

        public DataView GetBySearchCategory(string Column, string Value)
        {
            string sql = null;
            
            switch (Column)
            {
                case "0":
                    sql = "Select ItemGroupNo,ItemGroupName AS 'ItemGroup Name',Case when (MItemGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MItemGroup where ControlGroup=2  order by ItemGroupName";
                    break;
                case "ItemGroupName":
                    sql = "Select ItemGroupNo,ItemGroupName AS 'ItemGroup Name',Case when (MItemGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MItemGroup where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' and ControlGroup=2 order by ItemGroupName";
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
    /// This Class use for MStockGroup
    /// </summary>
    public class MItemGroup
    {
        private long mItemGroupNo;
        private string mItemGroupName;
        private string mLanguageName;
        private long mControlGroup;
        private long mControlSubGroup;
        private bool mIsActive;
        private long mUserId;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private long mGlobalCode;
        private long mMfgCompNo;
        private double mMargin;
        private bool mIsApplyToAll;
        private bool mIsLBTApply;
        private double mLBTVal;
        private string mActualStockGroupName;
        private string Mmsg;

        /// <summary>
        /// This Properties use for StockGroupNo
        /// </summary>
        public long ItemGroupNo
        {
            get { return mItemGroupNo; }
            set { mItemGroupNo = value; }
        }
        /// <summary>
        /// This Properties use for StockGroupName
        /// </summary>
        public string ItemGroupName
        {
            get { return mItemGroupName; }
            set { mItemGroupName = value; }
        }
        /// <summary>
        /// This Properties use for LanguageName
        /// </summary>
        public string LanguageName
        {
            get { return mLanguageName; }
            set { mLanguageName = value; }
        }
        /// <summary>
        /// This Properties use for ControlGroup
        /// </summary>
        public long ControlGroup
        {
            get { return mControlGroup; }
            set { mControlGroup = value; }
        }
        /// <summary>
        /// This Properties use for ControlSubGroup
        /// </summary>
        public long ControlSubGroup
        {
            get { return mControlSubGroup; }
            set { mControlSubGroup = value; }
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
        /// This Properties use for UserId
        /// </summary>
        public long UserId
        {
            get { return mUserId; }
            set { mUserId = value; }
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
        /// This Properties use for GlobalCode
        /// </summary>
        public long GlobalCode
        {
            get { return mGlobalCode; }
            set { mGlobalCode = value; }
        }
        /// <summary>
        /// This Properties use for MfgCompNo
        /// </summary>
        public long MfgCompNo
        {
            get { return mMfgCompNo; }
            set { mMfgCompNo = value; }
        }
        /// <summary>
        /// This Properties use for Margin
        /// </summary>
        public double Margin
        {
            get { return mMargin; }
            set { mMargin = value; }
        }
        /// <summary>
        /// This Properties use for IsApplyToAll
        /// </summary>
        public bool IsApplyToAll
        {
            get { return mIsApplyToAll; }
            set { mIsApplyToAll = value; }
        }
        public bool IsLBTApply
        {
            get { return mIsLBTApply; }
            set { mIsLBTApply = value; }
        }
        public double LBTVal
        {
            get { return mLBTVal; }
            set { mLBTVal = value; }
        }
        public string ActualStockGroupName
        {
            get { return mActualStockGroupName; }
            set { mActualStockGroupName = value; }
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
