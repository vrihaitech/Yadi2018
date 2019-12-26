using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMItemMaster
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMItemMaster(MItemMaster mItemMaster)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemMaster";

            cmd.Parameters.AddWithValue("@ItemNo", mItemMaster.ItemNo);
            cmd.Parameters.AddWithValue("@ItemName", mItemMaster.ItemName);
            cmd.Parameters.AddWithValue("@ItemShortName", mItemMaster.ItemShortName);
            cmd.Parameters.AddWithValue("@Barcode", mItemMaster.Barcode);
            cmd.Parameters.AddWithValue("@ShortCode", mItemMaster.ShortCode);
            cmd.Parameters.AddWithValue("@GroupNo", mItemMaster.GroupNo);
            cmd.Parameters.AddWithValue("@UOML", mItemMaster.UOML);
            cmd.Parameters.AddWithValue("@UOMH", mItemMaster.UOMH);
            cmd.Parameters.AddWithValue("@UOMDefault", mItemMaster.UOMDefault);
            cmd.Parameters.AddWithValue("@FkDepartmentNo", mItemMaster.FkDepartmentNo);
            cmd.Parameters.AddWithValue("@FkCategoryNo", mItemMaster.FkCategoryNo);
            cmd.Parameters.AddWithValue("@MinLevel", mItemMaster.MinLevel);
            cmd.Parameters.AddWithValue("@MaxLevel", mItemMaster.MaxLevel);
            cmd.Parameters.AddWithValue("@LangFullDesc", mItemMaster.LangFullDesc);
            cmd.Parameters.AddWithValue("@LangShortDesc", mItemMaster.LangShortDesc);
            cmd.Parameters.AddWithValue("@CompanyNo", mItemMaster.CompanyNo);
            cmd.Parameters.AddWithValue("@IsActive", mItemMaster.IsActive);
            cmd.Parameters.AddWithValue("@UserId", mItemMaster.UserId);
            cmd.Parameters.AddWithValue("@UserDate", mItemMaster.UserDate);
            cmd.Parameters.AddWithValue("@ControlUnder", mItemMaster.ControlUnder);
            cmd.Parameters.AddWithValue("@FactorVal", mItemMaster.FactorVal);
            cmd.Parameters.AddWithValue("@Margin", mItemMaster.Margin);
            cmd.Parameters.AddWithValue("@CessValue", mItemMaster.CessValue);
            cmd.Parameters.AddWithValue("@PackagingCharges", mItemMaster.PackagingCharges);
            cmd.Parameters.AddWithValue("@Dhekhrek", mItemMaster.Dhekhrek);
            cmd.Parameters.AddWithValue("@OtherCharges", mItemMaster.OtherCharges);
            cmd.Parameters.AddWithValue("@HigherVariation", mItemMaster.HigherVariation);
            cmd.Parameters.AddWithValue("@LowerVariation", mItemMaster.LowerVariation);
            //---GST------//
            cmd.Parameters.AddWithValue("@HSNCode", mItemMaster.HSNCode);
            cmd.Parameters.AddWithValue("@FKStockGroupTypeNo", mItemMaster.FKStockGroupTypeNo);
            cmd.Parameters.AddWithValue("@ItemType", mItemMaster.ItemType);
            cmd.Parameters.AddWithValue("@ESFlag", mItemMaster.ESFlag);
            cmd.Parameters.AddWithValue("@ContainerCharges", mItemMaster.ContainerCharges);
            cmd.Parameters.AddWithValue("@GSTSlab", mItemMaster.GSTSlab);
            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);

            return true;
        }

        public bool DeleteMItemMaster(MItemMaster mItemMaster)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMItemMaster";

            cmd.Parameters.AddWithValue("@ItemNo", mItemMaster.ItemNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mItemMaster.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool AddMRateSetting2(MRateSetting mratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRateSetting2";

            cmd.Parameters.AddWithValue("@PkSrNo", mratesetting.PkSrNo);

            //cmd.Parameters.AddWithValue("@FkBcdSrNo", mratesetting.FkBcdSrNo);

            // cmd.Parameters.AddWithValue("@ItemNo", mratesetting.ItemNo);

            cmd.Parameters.AddWithValue("@FromDate", mratesetting.FromDate);

            cmd.Parameters.AddWithValue("@PurRate", mratesetting.PurRate);

            cmd.Parameters.AddWithValue("@MRP", mratesetting.MRP);

            cmd.Parameters.AddWithValue("@UOMNo", mratesetting.UOMNo);

            cmd.Parameters.AddWithValue("@ASaleRate", mratesetting.ASaleRate);

            cmd.Parameters.AddWithValue("@BSaleRate", mratesetting.BSaleRate);

            cmd.Parameters.AddWithValue("@CSaleRate", mratesetting.CSaleRate);

            cmd.Parameters.AddWithValue("@DSaleRate", mratesetting.DSaleRate);

            cmd.Parameters.AddWithValue("@ESaleRate", mratesetting.ESaleRate);

            cmd.Parameters.AddWithValue("@StockConversion", mratesetting.StockConversion);

            //  cmd.Parameters.AddWithValue("@PerOfRateVariation", mratesetting.PerOfRateVariation);

            cmd.Parameters.AddWithValue("@MKTQty", mratesetting.MKTQty);

            cmd.Parameters.AddWithValue("@IsActive", mratesetting.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mratesetting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mratesetting.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mratesetting.CompanyNo);

            cmd.Parameters.AddWithValue("@Weight1", mratesetting.Weight1);                                         

            cmd.Parameters.AddWithValue("@Weight2", mratesetting.Weight2);

            cmd.Parameters.AddWithValue("@LPPerc", mratesetting.LPPerc);

            cmd.Parameters.AddWithValue("@SPPerc", mratesetting.SPPerc);

            cmd.Parameters.AddWithValue("@Hamali", mratesetting.Hamali);

            commandcollection.Add(cmd);

            return true;
        }

        public bool AddMItemTaxInfo1(MItemTaxInfo mitemtaxinfo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemTaxInfo1";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxinfo.PkSrNo);

            // cmd.Parameters.AddWithValue("@ItemNo", mitemtaxinfo.ItemNo);

            cmd.Parameters.AddWithValue("@TaxLedgerNo", mitemtaxinfo.TaxLedgerNo);

            cmd.Parameters.AddWithValue("@SalesLedgerNo", mitemtaxinfo.SalesLedgerNo);

            cmd.Parameters.AddWithValue("@FromDate", mitemtaxinfo.FromDate);

            cmd.Parameters.AddWithValue("@CalculationMethod", mitemtaxinfo.CalculationMethod);

            cmd.Parameters.AddWithValue("@Percentage", mitemtaxinfo.Percentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemtaxinfo.CompanyNo);

            cmd.Parameters.AddWithValue("@FKTaxSettingNo", mitemtaxinfo.FKTaxSettingNo);

            cmd.Parameters.AddWithValue("@UserID", mitemtaxinfo.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mitemtaxinfo.UserDate);

            commandcollection.Add(cmd);

            return true;


        }

        public bool AddMItemTaxInfo2(MItemTaxInfo mitemtaxinfo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemTaxInfo2";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxinfo.PkSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", mitemtaxinfo.ItemNo);

            cmd.Parameters.AddWithValue("@TaxLedgerNo", mitemtaxinfo.TaxLedgerNo);

            cmd.Parameters.AddWithValue("@SalesLedgerNo", mitemtaxinfo.SalesLedgerNo);

            cmd.Parameters.AddWithValue("@FromDate", mitemtaxinfo.FromDate);

            cmd.Parameters.AddWithValue("@CalculationMethod", mitemtaxinfo.CalculationMethod);

            cmd.Parameters.AddWithValue("@Percentage", mitemtaxinfo.Percentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemtaxinfo.CompanyNo);

            cmd.Parameters.AddWithValue("@FKTaxSettingNo", mitemtaxinfo.FKTaxSettingNo);

            cmd.Parameters.AddWithValue("@UserID", mitemtaxinfo.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mitemtaxinfo.UserDate);

            commandcollection.Add(cmd);

            return true;


        }

        public MItemMaster ModifyMItemMasterByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MItemMaster where ItemNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MItemMaster MM = new MItemMaster();
                while (dr.Read())
                {
                    MM.ItemNo = Convert.ToInt32(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["ItemName"])) MM.ItemName = Convert.ToString(dr["ItemName"]);
                    if (!Convert.IsDBNull(dr["ItemShortName"])) MM.ItemShortName = Convert.ToString(dr["ItemShortName"]);
                    if (!Convert.IsDBNull(dr["Barcode"])) MM.Barcode = Convert.ToString(dr["Barcode"]);
                    if (!Convert.IsDBNull(dr["ShortCode"])) MM.ShortCode = Convert.ToString(dr["ShortCode"]);
                    if (!Convert.IsDBNull(dr["GroupNo"])) MM.GroupNo = Convert.ToInt64(dr["GroupNo"]);
                    if (!Convert.IsDBNull(dr["UOMH"])) MM.UOMH = Convert.ToInt64(dr["UOMH"]);
                    if (!Convert.IsDBNull(dr["UOML"])) MM.UOML = Convert.ToInt64(dr["UOML"]);
                    if (!Convert.IsDBNull(dr["UOMDefault"])) MM.UOMDefault = Convert.ToInt64(dr["UOMDefault"]);
                    if (!Convert.IsDBNull(dr["FkDepartmentNo"])) MM.FkDepartmentNo = Convert.ToInt64(dr["FkDepartmentNo"]);
                    if (!Convert.IsDBNull(dr["FkCategoryNo"])) MM.FkCategoryNo = Convert.ToInt64(dr["FkCategoryNo"]);
                    if (!Convert.IsDBNull(dr["MinLevel"])) MM.MinLevel = Convert.ToInt64(dr["MinLevel"]);
                    if (!Convert.IsDBNull(dr["MaxLevel"])) MM.MaxLevel = Convert.ToInt64(dr["MaxLevel"]);
                    if (!Convert.IsDBNull(dr["ReOrderLevelQty"])) MM.ReOrderLevelQty = Convert.ToInt64(dr["ReOrderLevelQty"]);
                    if (!Convert.IsDBNull(dr["LangFullDesc"])) MM.LangFullDesc = Convert.ToString(dr["LangFullDesc"]);
                    if (!Convert.IsDBNull(dr["LangShortDesc"])) MM.LangShortDesc = Convert.ToString(dr["LangShortDesc"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserId"])) MM.UserId = Convert.ToInt64(dr["UserId"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt32(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["ControlUnder"])) MM.ControlUnder = Convert.ToInt64(dr["ControlUnder"]);
                    if (!Convert.IsDBNull(dr["FactorVal"])) MM.FactorVal = Convert.ToInt64(dr["FactorVal"]);
                    if (!Convert.IsDBNull(dr["Margin"])) MM.Margin = Convert.ToInt64(dr["Margin"]);
                    if (!Convert.IsDBNull(dr["CessValue"])) MM.CessValue = Convert.ToDouble(dr["CessValue"]);
                    if (!Convert.IsDBNull(dr["PackagingCharges"])) MM.PackagingCharges = Convert.ToDouble(dr["PackagingCharges"]);
                    if (!Convert.IsDBNull(dr["Dhekhrek"])) MM.Dhekhrek = Convert.ToDouble(dr["Dhekhrek"]);
                    if (!Convert.IsDBNull(dr["OtherCharges"])) MM.OtherCharges = Convert.ToDouble(dr["OtherCharges"]);
                    if (!Convert.IsDBNull(dr["HigherVariation"])) MM.HigherVariation = Convert.ToInt64(dr["HigherVariation"]);
                    if (!Convert.IsDBNull(dr["LowerVariation"])) MM.LowerVariation = Convert.ToInt64(dr["LowerVariation"]);
                    if (!Convert.IsDBNull(dr["HSNCode"])) MM.HSNCode = Convert.ToString(dr["HSNCode"]);
                    if (!Convert.IsDBNull(dr["ItemType"])) MM.ItemType = Convert.ToInt32(dr["ItemType"]);
                    if (!Convert.IsDBNull(dr["ESFlag"])) MM.ESFlag = Convert.ToBoolean(dr["ESFlag"]);
                    if (!Convert.IsDBNull(dr["ContainerCharges"])) MM.ContainerCharges = Convert.ToDouble(dr["ContainerCharges"]);
                    if (!Convert.IsDBNull(dr["GSTSlab"])) MM.GSTSlab = Convert.ToBoolean(dr["GSTSlab"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MItemMaster();
        }

        public bool AddMRateSetting1(MRateSetting mratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRateSetting";

            cmd.Parameters.AddWithValue("@PkSrNo", mratesetting.PkSrNo);

            //  cmd.Parameters.AddWithValue("@FkBcdSrNo", mratesetting.FkBcdSrNo);

            cmd.Parameters.AddWithValue("@ItemNo", mratesetting.ItemNo);

            cmd.Parameters.AddWithValue("@FromDate", mratesetting.FromDate);

            cmd.Parameters.AddWithValue("@PurRate", mratesetting.PurRate);

            cmd.Parameters.AddWithValue("@MRP", mratesetting.MRP);

            cmd.Parameters.AddWithValue("@UOMNo", mratesetting.UOMNo);

            cmd.Parameters.AddWithValue("@ASaleRate", mratesetting.ASaleRate);

            cmd.Parameters.AddWithValue("@BSaleRate", mratesetting.BSaleRate);

            cmd.Parameters.AddWithValue("@CSaleRate", mratesetting.CSaleRate);

            cmd.Parameters.AddWithValue("@DSaleRate", mratesetting.DSaleRate);

            cmd.Parameters.AddWithValue("@ESaleRate", mratesetting.ESaleRate);

            cmd.Parameters.AddWithValue("@StockConversion", mratesetting.StockConversion);

            cmd.Parameters.AddWithValue("@PerOfRateVariation", mratesetting.PerOfRateVariation);

            cmd.Parameters.AddWithValue("@MKTQty", mratesetting.MKTQty);

            cmd.Parameters.AddWithValue("@IsActive", mratesetting.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mratesetting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mratesetting.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mratesetting.CompanyNo);

            //  cmd.Parameters.AddWithValue("@CompanyNo", mratesetting.AreaNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mratesetting.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool UpdateMRateSetting(MRateSetting mRateSetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update MRateSetting set IsActive=@IsActive,StatusNo=2 where PkSrNo=@PksrNo; Update MStockItems set StatusNo=2 Where ItemNo=@ItemNo";

            cmd.Parameters.AddWithValue("@PksrNo", mRateSetting.PkSrNo);

            cmd.Parameters.AddWithValue("@IsActive", mRateSetting.IsActive);

            cmd.Parameters.AddWithValue("@ItemNo", mRateSetting.ItemNo);

            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetBySearch(string Column, string Value)
        {

         
            string sql = null;
            switch (Column)
            {
                case "0":

                    sql = " SELECT MItemMaster.ItemNo, MItemGroup.ItemGroupName + '-' + MItemMaster.ItemShortName  AS 'ShortName' , MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName  AS 'ItemName',Case when (MItemMaster.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                         " FROM MItemMaster INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo Where ItemNo<>1 order by MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName ";
                    break;
                case "ItemName":
                    sql = "  SELECT MItemMaster.ItemNo, MItemGroup.ItemGroupName + '-' + MItemMaster.ItemShortName  AS 'ShortName' , MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName  AS 'ItemName',Case when (MItemMaster.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                         " FROM MItemMaster INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo Where ItemNo<>1 AND MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName";

                    break;
                case "ItemShortName":
                    sql = "  SELECT MItemMaster.ItemNo, MItemGroup.ItemGroupName + '-' + MItemMaster.ItemShortName  AS 'ShortName' , MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName  AS 'ItemName',Case when (MItemMaster.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                         " FROM MItemMaster INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo Where ItemNo<>1 AND MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName";

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
            //long ItemNoTemp = 0, BarcodeNoTemp = 0;
            int cntref = 0;

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
                        if (commandcollection[i].CommandText == "AddMItemMaster")
                        {
                            // commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);
                            cntref = i;
                        }
                        else if (commandcollection[i].CommandText == "AddMStockBarcode1")
                        {
                            commandcollection[i].CommandText = "AddMStockBarcode";
                        }


                        if (commandcollection[i].CommandText == "AddMRateSetting")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkBcdSrNo", commandcollection[0].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddMRateSetting2")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);

                            //  commandcollection[i].Parameters.AddWithValue("@FkBcdSrNo", commandcollection[cntref].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMItemTaxInfo1")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddMStockItemManufacturer")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);
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

        public long ExecuteNonQueryStatements1()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            //long ItemNoTemp = 0, BarcodeNoTemp = 0;
            int cntref = 0;

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
                        if (commandcollection[i].CommandText == "AddMItemMaster")
                        {
                            // commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);
                            cntref = i;
                        }
                        else if (commandcollection[i].CommandText == "AddMStockBarcode1")
                        {
                            commandcollection[i].CommandText = "AddMStockBarcode";
                        }


                        if (commandcollection[i].CommandText == "AddMRateSetting")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkBcdSrNo", commandcollection[0].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddMRateSetting2")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);

                            //  commandcollection[i].Parameters.AddWithValue("@FkBcdSrNo", commandcollection[cntref].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMItemTaxInfo1")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddMStockItemManufacturer")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ItemNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();

                return Convert.ToInt32(commandcollection[0].Parameters["@ReturnID"].Value);
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

        public bool UpdateMItemMaster(long ItemNo, bool ESFlag)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update mItemMaster set ESFlag =@ESFlag,StatusNo=2 where ItemNo =@itemNo";

            cmd.Parameters.AddWithValue("@itemNo", ItemNo);
            cmd.Parameters.AddWithValue("@ESFlag", ESFlag);

            commandcollection.Add(cmd);
            return true;

        }

        public bool UpdateMItemMasterIsActive(MItemMaster mItemMaster)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update mItemMaster set mItemMaster.IsActive=@IsActive,StatusNo=2 where mItemMaster.ItemNo=@ItemNo";

            cmd.Parameters.AddWithValue("@ItemNo", mItemMaster.ItemNo);

            cmd.Parameters.AddWithValue("@IsActive", mItemMaster.IsActive);

            commandcollection.Add(cmd);
            return true;
        }

    }

    public class MItemMaster
    {
        private long mItemNo;
        private string mItemName;
        private string mItemShortName;
        private string mBarcode;
        private string mShortCode;
        private long mGroupNo;
        private long mUOMH;
        private long mUOML;
        private long mUOMDefault;
        private long mFkDepartmentNo;
        private long mFkCategoryNo;
        private long mMinLevel;
        private long mMaxLevel;
        private long mReOrderLevelQty;
        private string mLangFullDesc;
        private string mLangShortDesc;
        private long mCompanyNo;
        private bool mIsActive;
        private long mUserId;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private long mControlUnder;
        private long mFactorVal;
        private long mMargin;
        private double mCessValue;
        private double mPackagingCharges;
        private double mDhekhrek;
        private double mOtherCharges;
        private long mHigherVariation;
        private long mLowerVariation;
        private string mHSNCode;
        private bool mESFlag;
        private long mFKStockGroupTypeNo;
        private string Mmsg;
        private int mItemType;
        private double mContainerCharges;
        private bool mGSTSlab;

        public int ItemType
        {
            get { return mItemType; }
            set { mItemType = value; }
        }

        public long FKStockGroupTypeNo
        {
            get { return mFKStockGroupTypeNo; }
            set { mFKStockGroupTypeNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public bool ESFlag
        {
            get { return mESFlag; }
            set { mESFlag = value; }
        }
        public string ItemName
        {
            get { return mItemName; }
            set { mItemName = value; }
        }
        public string ItemShortName
        {
            get { return mItemShortName; }
            set { mItemShortName = value; }
        }
        public string Barcode
        {
            get { return mBarcode; }
            set { mBarcode = value; }
        }
        public string ShortCode
        {
            get { return mShortCode; }
            set { mShortCode = value; }
        }
        public long GroupNo
        {
            get { return mGroupNo; }
            set { mGroupNo = value; }
        }
        public long UOMH
        {
            get { return mUOMH; }
            set { mUOMH = value; }
        }
        public long UOML
        {
            get { return mUOML; }
            set { mUOML = value; }
        }
        public long UOMDefault
        {
            get { return mUOMDefault; }
            set { mUOMDefault = value; }
        }
        public long FkDepartmentNo
        {
            get { return mFkDepartmentNo; }
            set { mFkDepartmentNo = value; }
        }
        public long FkCategoryNo
        {
            get { return mFkCategoryNo; }
            set { mFkCategoryNo = value; }
        }
        public long MinLevel
        {
            get { return mMinLevel; }
            set { mMinLevel = value; }
        }
        public long MaxLevel
        {
            get { return mMaxLevel; }
            set { mMaxLevel = value; }
        }
        public long ReOrderLevelQty
        {
            get { return mReOrderLevelQty; }
            set { mReOrderLevelQty = value; }
        }
        public string LangFullDesc
        {
            get { return mLangFullDesc; }
            set { mLangFullDesc = value; }
        }
        public string LangShortDesc
        {
            get { return mLangShortDesc; }
            set { mLangShortDesc = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
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
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public long ControlUnder
        {
            get { return mControlUnder; }
            set { mControlUnder = value; }
        }
        public long FactorVal
        {
            get { return mFactorVal; }
            set { mFactorVal = value; }
        }
        public long Margin
        {
            get { return mMargin; }
            set { mMargin = value; }
        }
        public double CessValue
        {
            get { return mCessValue; }
            set { mCessValue = value; }
        }
        public double PackagingCharges
        {
            get { return mPackagingCharges; }
            set { mPackagingCharges = value; }
        }
        public double Dhekhrek
        {
            get { return mDhekhrek; }
            set { mDhekhrek = value; }
        }
        public double OtherCharges
        {
            get { return mOtherCharges; }
            set { mOtherCharges = value; }
        }
        public long HigherVariation
        {
            get { return mHigherVariation; }
            set { mHigherVariation = value; }
        }
        public long LowerVariation
        {
            get { return mLowerVariation; }
            set { mLowerVariation = value; }
        }
        public string HSNCode
        {
            get { return mHSNCode; }
            set { mHSNCode = value; }
        }
        public double ContainerCharges
        {
            get { return mContainerCharges; }
            set { mContainerCharges = value; }
        }
        public bool GSTSlab
        {
            get { return mGSTSlab; }
            set { mGSTSlab = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class MRateSetting
    {
        private long mPkSrNo;
        private long mFkBcdSrNo;
        private long mItemNo;
        private DateTime mFromDate;
        private double mPurRate;
        private double mMRP;
        private long mUOMNo;
        private double mASaleRate;
        private double mBSaleRate;
        private double mCSaleRate;
        private double mDSaleRate;
        private double mESaleRate;
        private double mStockConversion;
        private double mPerOfRateVariation;
        private long mMKTQty;
        private bool mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;
        private long mAreaNo;
        private bool mESFlag;
        private double mWeight1;
        private double mWeight2;
        private double mLPPerc;
        private double mSPPerc;
        private double mHamali;

        public double Hamali
        {
            get { return mHamali; }
            set { mHamali = value; }
        }
        public bool ESFlag
        {
            get { return mESFlag; }
            set { mESFlag = value; }
        }
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long FkBcdSrNo
        {
            get { return mFkBcdSrNo; }
            set { mFkBcdSrNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        public double PurRate
        {
            get { return mPurRate; }
            set { mPurRate = value; }
        }
        public double MRP
        {
            get { return mMRP; }
            set { mMRP = value; }
        }
        public long UOMNo
        {
            get { return mUOMNo; }
            set { mUOMNo = value; }
        }
        public double ASaleRate
        {
            get { return mASaleRate; }
            set { mASaleRate = value; }
        }
        public double BSaleRate
        {
            get { return mBSaleRate; }
            set { mBSaleRate = value; }
        }
        public double CSaleRate
        {
            get { return mCSaleRate; }
            set { mCSaleRate = value; }
        }
        public double DSaleRate
        {
            get { return mDSaleRate; }
            set { mDSaleRate = value; }
        }
        public double ESaleRate
        {
            get { return mESaleRate; }
            set { mESaleRate = value; }
        }
        public double StockConversion
        {
            get { return mStockConversion; }
            set { mStockConversion = value; }
        }
        public double PerOfRateVariation
        {
            get { return mPerOfRateVariation; }
            set { mPerOfRateVariation = value; }
        }
        public long MKTQty
        {
            get { return mMKTQty; }
            set { mMKTQty = value; }
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
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public long AreaNo
        {
            get { return mAreaNo; }
            set { mAreaNo = value; }
        }
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public double Weight1
        {
            get { return mWeight1; }
            set { mWeight1 = value; }
        }
        public double Weight2
        {
            get { return mWeight2; }
            set { mWeight2 = value; }
        }
        public double LPPerc
        {
            get { return mLPPerc; }
            set { mLPPerc = value; }
        }
        public double SPPerc
        {
            get { return mSPPerc; }
            set { mSPPerc = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class MItemTaxInfo
    {
        private long mPkSrNo;
        private long mItemNo;
        private long mTaxLedgerNo;
        private long mSalesLedgerNo;
        private DateTime mFromDate;
        private string mCalculationMethod;
        private double mPercentage;
        private long mCompanyNo;
        private long mFKTaxSettingNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
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
        /// This Properties use for ItemNo
        /// </summary>
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        /// <summary>
        /// This Properties use for TaxLedgerNo
        /// </summary>
        public long TaxLedgerNo
        {
            get { return mTaxLedgerNo; }
            set { mTaxLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for SalesLedgerNo
        /// </summary>
        public long SalesLedgerNo
        {
            get { return mSalesLedgerNo; }
            set { mSalesLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for FromDate
        /// </summary>
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        /// <summary>
        /// This Properties use for CalculationMethod
        /// </summary>
        public string CalculationMethod
        {
            get { return mCalculationMethod; }
            set { mCalculationMethod = value; }
        }
        /// <summary>
        /// This Properties use for Percentage
        /// </summary>
        public double Percentage
        {
            get { return mPercentage; }
            set { mPercentage = value; }
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
        /// This Properties use for FKTaxSettingNo
        /// </summary>
        public long FKTaxSettingNo
        {
            get { return mFKTaxSettingNo; }
            set { mFKTaxSettingNo = value; }
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
