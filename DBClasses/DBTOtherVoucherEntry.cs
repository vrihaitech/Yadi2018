using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using OMControls;


namespace OM
{
    class DBTOtherVoucherEntry
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;


        public bool AddTOtherVoucherEntry(TOtherVoucherEntry tothervoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTOtherVoucherEntry";

            cmd.Parameters.AddWithValue("@PkOtherVoucherNo", tothervoucherentry.PkOtherVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherTypeCode", tothervoucherentry.VoucherTypeCode);

            cmd.Parameters.AddWithValue("@VoucherUserNo", tothervoucherentry.VoucherUserNo);

            cmd.Parameters.AddWithValue("@VoucherDate", tothervoucherentry.VoucherDate);

            cmd.Parameters.AddWithValue("@VoucherTime", tothervoucherentry.VoucherTime);

            cmd.Parameters.AddWithValue("@Narration", tothervoucherentry.Narration);

            cmd.Parameters.AddWithValue("@Reference", tothervoucherentry.Reference);

            cmd.Parameters.AddWithValue("@ChequeNo", tothervoucherentry.ChequeNo);

            cmd.Parameters.AddWithValue("@ClearingDate", tothervoucherentry.ClearingDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tothervoucherentry.CompanyNo);

            cmd.Parameters.AddWithValue("@BilledAmount", tothervoucherentry.BilledAmount);

            cmd.Parameters.AddWithValue("@ChallanNo", tothervoucherentry.ChallanNo);

            cmd.Parameters.AddWithValue("@Remark", tothervoucherentry.Remark);

            cmd.Parameters.AddWithValue("@InwardLocationCode", tothervoucherentry.InwardLocationCode);

            cmd.Parameters.AddWithValue("@MacNo", tothervoucherentry.MacNo);

            cmd.Parameters.AddWithValue("@IsCancel", tothervoucherentry.IsCancel);

            cmd.Parameters.AddWithValue("@PayTypeNo", tothervoucherentry.PayTypeNo);

            cmd.Parameters.AddWithValue("@RateTypeNo", tothervoucherentry.RateTypeNo);

            cmd.Parameters.AddWithValue("@TaxTypeNo", tothervoucherentry.TaxTypeNo);

            cmd.Parameters.AddWithValue("@IsVoucherLock", tothervoucherentry.IsVoucherLock);

            cmd.Parameters.AddWithValue("@VoucherStatus", tothervoucherentry.VoucherStatus);

            cmd.Parameters.AddWithValue("@UserID", tothervoucherentry.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tothervoucherentry.UserDate);

            cmd.Parameters.AddWithValue("@OrderType", tothervoucherentry.OrderType);

            cmd.Parameters.AddWithValue("@ReturnAmount", tothervoucherentry.ReturnAmount);

            cmd.Parameters.AddWithValue("@Visibility", tothervoucherentry.Visibility);

            cmd.Parameters.AddWithValue("@DiscPercent", tothervoucherentry.DiscPercent);

            cmd.Parameters.AddWithValue("@DiscAmt", tothervoucherentry.DiscAmt);

            cmd.Parameters.AddWithValue("@MixMode", tothervoucherentry.MixMode);

            // cmd.Parameters.AddWithValue("@IsItemLevelDisc", tothervoucherentry.IsItemLevelDisc);

            //  cmd.Parameters.AddWithValue("@IsFooterLevelDisc", tothervoucherentry.IsFooterLevelDisc);

            //cmd.Parameters.AddWithValue("@SchemeDisc", tothervoucherentry.SchemeDisc);

            //cmd.Parameters.AddWithValue("@DistDisc", tothervoucherentry.DistDisc);

            //cmd.Parameters.AddWithValue("@CashDisc", tothervoucherentry.CashDisc);

            //cmd.Parameters.AddWithValue("@Charges", tothervoucherentry.Charges);

            //cmd.Parameters.AddWithValue("@SubTotal", tothervoucherentry.SubTotal);

            //cmd.Parameters.AddWithValue("@TotalTax", tothervoucherentry.TotalTax);

            //cmd.Parameters.AddWithValue("@RoundOff", tothervoucherentry.RoundOff);

            //cmd.Parameters.AddWithValue("@OtherTax", tothervoucherentry.OtherTax);

            cmd.Parameters.AddWithValue("@LedgerNo", tothervoucherentry.LedgerNo);

            //  cmd.Parameters.AddWithValue("@VersionNo", tothervoucherentry.VersionNo);

            //  cmd.Parameters.AddWithValue("@ServerOtherVoucherNo", tothervoucherentry.ServerOtherVoucherNo);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTOtherStock(TOtherStock totherstock)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTOtherStock";

            cmd.Parameters.AddWithValue("@PkOtherStockTrnNo", totherstock.PkOtherStockTrnNo);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", totherstock.FKVoucherNo);

            //cmd.Parameters.AddWithValue("@FkVoucherTrnNo", totherstock.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@FkVoucherSrNo", totherstock.FkVoucherSrNo);

            cmd.Parameters.AddWithValue("@GroupNo", totherstock.GroupNo);

            cmd.Parameters.AddWithValue("@ItemNo", totherstock.ItemNo);

            cmd.Parameters.AddWithValue("@TrnCode", totherstock.TrnCode);

            cmd.Parameters.AddWithValue("@Quantity", totherstock.Quantity);

            cmd.Parameters.AddWithValue("@BilledQuantity", totherstock.BilledQuantity);

            cmd.Parameters.AddWithValue("@Rate", totherstock.Rate);

            cmd.Parameters.AddWithValue("@Amount", totherstock.Amount);

            cmd.Parameters.AddWithValue("@NetRate", totherstock.NetRate);

            cmd.Parameters.AddWithValue("@NetAmount", totherstock.NetAmount);

            cmd.Parameters.AddWithValue("@SGSTPercentage", totherstock.SGSTPercentage);

            cmd.Parameters.AddWithValue("@SGSTAmount", totherstock.SGSTAmount);

            cmd.Parameters.AddWithValue("@DiscPercentage", totherstock.DiscPercentage);

            cmd.Parameters.AddWithValue("@DiscAmount", totherstock.DiscAmount);

            cmd.Parameters.AddWithValue("@DiscRupees", totherstock.DiscRupees);

            cmd.Parameters.AddWithValue("@DiscPercentage2", totherstock.DiscPercentage2);

            cmd.Parameters.AddWithValue("@DiscAmount2", totherstock.DiscAmount2);

            cmd.Parameters.AddWithValue("@DiscRupees2", totherstock.DiscRupees2);

            cmd.Parameters.AddWithValue("@FkUomNo", totherstock.FkUomNo);

            cmd.Parameters.AddWithValue("@FkStockBarCodeNo", totherstock.FkStockBarCodeNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", totherstock.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@FkItemTaxInfo", totherstock.FkItemTaxInfo);

            // cmd.Parameters.AddWithValue("@IsVoucherLock", totherstock.IsVoucherLock);

            cmd.Parameters.AddWithValue("@FreeQty", totherstock.FreeQty);

            cmd.Parameters.AddWithValue("@FreeUOMNo", totherstock.FreeUOMNo);

            cmd.Parameters.AddWithValue("@UserID", totherstock.UserID);

            cmd.Parameters.AddWithValue("@UserDate", totherstock.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", totherstock.CompanyNo);

            //cmd.Parameters.AddWithValue("@StatusNo", totherstock.StatusNo);

            //cmd.Parameters.AddWithValue("@LandedRate", totherstock.LandedRate);

            //cmd.Parameters.AddWithValue("@BalanceQty", totherstock.BalanceQty);

            //  cmd.Parameters.AddWithValue("@IsComplete", totherstock.IsComplete);
            cmd.Parameters.AddWithValue("@TRWeight", totherstock.TRWeight);
            cmd.Parameters.AddWithValue("@GRWeight", totherstock.GRWeight);
            cmd.Parameters.AddWithValue("@DisplayItemName", totherstock.DisplayItemName);
            cmd.Parameters.AddWithValue("@PackagingCharges", totherstock.PackagingCharges);
            cmd.Parameters.AddWithValue("@CGSTAmount", totherstock.CGSTAmount);
            cmd.Parameters.AddWithValue("@CGSTPercentage", totherstock.CGSTPercentage);
            cmd.Parameters.AddWithValue("@IGSTAmount", totherstock.IGSTAmount);
            cmd.Parameters.AddWithValue("@IGSTPercentage", totherstock.IGSTPercentage);
            cmd.Parameters.AddWithValue("@CessAmount", totherstock.CessAmount);
            cmd.Parameters.AddWithValue("@CessPercentage", totherstock.CessPercentage);
            cmd.Parameters.AddWithValue("@Remarks", totherstock.Remarks);
            cmd.Parameters.AddWithValue("@FkItemTaxInfo2", totherstock.FkItemTaxInfo2);
            cmd.Parameters.AddWithValue("@SalesMan", totherstock.SalesMan);
            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);
            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTOtherStock(TOtherStock totherstock)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTOtherStock";

            cmd.Parameters.AddWithValue("@PkOtherStockTrnNo", totherstock.PkOtherStockTrnNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                totherstock.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteTOtherVoucherEntry(TOtherVoucherEntry tothervoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTOtherVoucherEntry";

            cmd.Parameters.AddWithValue("@PkOtherVoucherNo", tothervoucherentry.PkOtherVoucherNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tothervoucherentry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteAllTOtherVoucherEntryByCollection(TOtherVoucherEntry tothervoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAllTOtherVoucherEntry";

            cmd.Parameters.AddWithValue("@PkOtherVoucherNo", tothervoucherentry.PkOtherVoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetTOtherStockByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TOtherStock where PkOtherStockTrnNo =" + ID;
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

        public DataView TOtherStock()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TOtherStock order by PkOtherStockTrnNo";
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

        public TOtherStock ModifyTOtherStockByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TOtherStock where PkOtherStockTrnNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TOtherStock MM = new TOtherStock();
                while (dr.Read())
                {
                    MM.PkOtherStockTrnNo = Convert.ToInt32(dr["PkOtherStockTrnNo"]);
                    if (!Convert.IsDBNull(dr["FKVoucherNo"])) MM.FKOtheVoucherNo = Convert.ToInt64(dr["FKOtheVoucherNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherTrnNo"])) MM.FkVoucherTrnNo = Convert.ToInt64(dr["FkVoucherTrnNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherSrNo"])) MM.FkVoucherSrNo = Convert.ToInt64(dr["FkVoucherSrNo"]);
                    if (!Convert.IsDBNull(dr["GroupNo"])) MM.GroupNo = Convert.ToInt64(dr["GroupNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["TrnCode"])) MM.TrnCode = Convert.ToInt64(dr["TrnCode"]);
                    if (!Convert.IsDBNull(dr["Quantity"])) MM.Quantity = Convert.ToDouble(dr["Quantity"]);
                    if (!Convert.IsDBNull(dr["BilledQuantity"])) MM.BilledQuantity = Convert.ToDouble(dr["BilledQuantity"]);
                    if (!Convert.IsDBNull(dr["Rate"])) MM.Rate = Convert.ToDouble(dr["Rate"]);
                    if (!Convert.IsDBNull(dr["Amount"])) MM.Amount = Convert.ToDouble(dr["Amount"]);
                    if (!Convert.IsDBNull(dr["NetRate"])) MM.NetRate = Convert.ToDouble(dr["NetRate"]);
                    if (!Convert.IsDBNull(dr["NetAmount"])) MM.NetAmount = Convert.ToDouble(dr["NetAmount"]);
                    if (!Convert.IsDBNull(dr["TaxPercentage"])) MM.SGSTPercentage = Convert.ToDouble(dr["TaxPercentage"]);
                    if (!Convert.IsDBNull(dr["TaxAmount"])) MM.SGSTAmount = Convert.ToDouble(dr["TaxAmount"]);
                    if (!Convert.IsDBNull(dr["DiscPercentage"])) MM.DiscPercentage = Convert.ToDouble(dr["DiscPercentage"]);
                    if (!Convert.IsDBNull(dr["DiscAmount"])) MM.DiscAmount = Convert.ToDouble(dr["DiscAmount"]);
                    if (!Convert.IsDBNull(dr["DiscRupees"])) MM.DiscRupees = Convert.ToDouble(dr["DiscRupees"]);
                    if (!Convert.IsDBNull(dr["DiscPercentage2"])) MM.DiscPercentage2 = Convert.ToDouble(dr["DiscPercentage2"]);
                    if (!Convert.IsDBNull(dr["DiscAmount2"])) MM.DiscAmount2 = Convert.ToDouble(dr["DiscAmount2"]);
                    if (!Convert.IsDBNull(dr["DiscRupees2"])) MM.DiscRupees2 = Convert.ToDouble(dr["DiscRupees2"]);
                    if (!Convert.IsDBNull(dr["FkUomNo"])) MM.FkUomNo = Convert.ToInt64(dr["FkUomNo"]);
                    if (!Convert.IsDBNull(dr["FkStockBarCodeNo"])) MM.FkStockBarCodeNo = Convert.ToInt64(dr["FkStockBarCodeNo"]);
                    if (!Convert.IsDBNull(dr["FkRateSettingNo"])) MM.FkRateSettingNo = Convert.ToInt64(dr["FkRateSettingNo"]);
                    if (!Convert.IsDBNull(dr["FkItemTaxInfo"])) MM.FkItemTaxInfo = Convert.ToInt64(dr["FkItemTaxInfo"]);
                    if (!Convert.IsDBNull(dr["IsVoucherLock"])) MM.IsVoucherLock = Convert.ToBoolean(dr["IsVoucherLock"]);
                    if (!Convert.IsDBNull(dr["FreeQty"])) MM.FreeQty = Convert.ToDouble(dr["FreeQty"]);
                    if (!Convert.IsDBNull(dr["FreeUOMNo"])) MM.FreeUOMNo = Convert.ToInt64(dr["FreeUOMNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt32(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["LandedRate"])) MM.LandedRate = Convert.ToDouble(dr["LandedRate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TOtherStock();
        }

        public DataView GetTOtherVoucherEntryByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TOtherVoucherEntry where PkOtherVoucherNo =" + ID;
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

        public DataView TOtherVoucherEntry()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TOtherVoucherEntry order by PkOtherVoucherNo";
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

        public bool AddMRateSetting3(MRateSetting3 mratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRateSetting3";

            cmd.Parameters.AddWithValue("@PkSrNo", mratesetting.PkSrNo);

            cmd.Parameters.AddWithValue("@FkBcdSrNo", mratesetting.FkBcdSrNo);

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

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public TOtherVoucherEntry ModifyTOtherVoucherEntryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TOtherVoucherEntry where PkOtherVoucherNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TOtherVoucherEntry MM = new TOtherVoucherEntry();
                while (dr.Read())
                {
                    MM.PkOtherVoucherNo = Convert.ToInt32(dr["PkOtherVoucherNo"]);
                    if (!Convert.IsDBNull(dr["VoucherTypeCode"])) MM.VoucherTypeCode = Convert.ToInt64(dr["VoucherTypeCode"]);
                    if (!Convert.IsDBNull(dr["VoucherUserNo"])) MM.VoucherUserNo = Convert.ToInt64(dr["VoucherUserNo"]);
                    if (!Convert.IsDBNull(dr["VoucherDate"])) MM.VoucherDate = Convert.ToDateTime(dr["VoucherDate"]);
                    if (!Convert.IsDBNull(dr["VoucherTime"])) MM.VoucherTime = Convert.ToDateTime(dr["VoucherTime"]);
                    if (!Convert.IsDBNull(dr["Narration"])) MM.Narration = Convert.ToString(dr["Narration"]);
                    if (!Convert.IsDBNull(dr["Reference"])) MM.Reference = Convert.ToString(dr["Reference"]);
                    if (!Convert.IsDBNull(dr["ChequeNo"])) MM.ChequeNo = Convert.ToInt64(dr["ChequeNo"]);
                    if (!Convert.IsDBNull(dr["ClearingDate"])) MM.ClearingDate = Convert.ToDateTime(dr["ClearingDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["BilledAmount"])) MM.BilledAmount = Convert.ToDouble(dr["BilledAmount"]);
                    if (!Convert.IsDBNull(dr["ChallanNo"])) MM.ChallanNo = Convert.ToString(dr["ChallanNo"]);
                    if (!Convert.IsDBNull(dr["Remark"])) MM.Remark = Convert.ToString(dr["Remark"]);
                    if (!Convert.IsDBNull(dr["InwardLocationCode"])) MM.InwardLocationCode = Convert.ToInt64(dr["InwardLocationCode"]);
                    if (!Convert.IsDBNull(dr["MacNo"])) MM.MacNo = Convert.ToInt64(dr["MacNo"]);
                    if (!Convert.IsDBNull(dr["IsCancel"])) MM.IsCancel = Convert.ToBoolean(dr["IsCancel"]);
                    if (!Convert.IsDBNull(dr["PayTypeNo"])) MM.PayTypeNo = Convert.ToInt64(dr["PayTypeNo"]);
                    if (!Convert.IsDBNull(dr["RateTypeNo"])) MM.RateTypeNo = Convert.ToInt64(dr["RateTypeNo"]);
                    if (!Convert.IsDBNull(dr["TaxTypeNo"])) MM.TaxTypeNo = Convert.ToInt64(dr["TaxTypeNo"]);
                    if (!Convert.IsDBNull(dr["IsVoucherLock"])) MM.IsVoucherLock = Convert.ToBoolean(dr["IsVoucherLock"]);
                    if (!Convert.IsDBNull(dr["VoucherStatus"])) MM.VoucherStatus = Convert.ToInt32(dr["VoucherStatus"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["OrderType"])) MM.OrderType = Convert.ToInt64(dr["OrderType"]);
                    if (!Convert.IsDBNull(dr["ReturnAmount"])) MM.ReturnAmount = Convert.ToDouble(dr["ReturnAmount"]);
                    if (!Convert.IsDBNull(dr["Visibility"])) MM.Visibility = Convert.ToDouble(dr["Visibility"]);
                    if (!Convert.IsDBNull(dr["DiscPercent"])) MM.DiscPercent = Convert.ToInt64(dr["DiscPercent"]);
                    if (!Convert.IsDBNull(dr["DiscAmt"])) MM.DiscAmt = Convert.ToInt64(dr["DiscAmt"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt32(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["MixMode"])) MM.MixMode = Convert.ToInt32(dr["MixMode"]);
                    if (!Convert.IsDBNull(dr["IsItemLevelDisc"])) MM.IsItemLevelDisc = Convert.ToBoolean(dr["IsItemLevelDisc"]);
                    if (!Convert.IsDBNull(dr["IsFooterLevelDisc"])) MM.IsFooterLevelDisc = Convert.ToBoolean(dr["IsFooterLevelDisc"]);
                    if (!Convert.IsDBNull(dr["Visibility"])) MM.Visibility = Convert.ToDouble(dr["Visibility"]);
                    //if (!Convert.IsDBNull(dr["SchemeDisc"])) MM.SchemeDisc = Convert.ToDouble(dr["SchemeDisc"]);
                    //if (!Convert.IsDBNull(dr["DistDisc"])) MM.DistDisc = Convert.ToDouble(dr["DistDisc"]);
                    //if (!Convert.IsDBNull(dr["CashDisc"])) MM.CashDisc = Convert.ToDouble(dr["CashDisc"]);
                    //if (!Convert.IsDBNull(dr["Charges"])) MM.Charges = Convert.ToDouble(dr["Charges"]);
                    //if (!Convert.IsDBNull(dr["SubTotal"])) MM.SubTotal = Convert.ToDouble(dr["SubTotal"]);
                    //if (!Convert.IsDBNull(dr["TotalTax"])) MM.TotalTax = Convert.ToDouble(dr["TotalTax"]);
                    //if (!Convert.IsDBNull(dr["RoundOff"])) MM.RoundOff = Convert.ToDouble(dr["RoundOff"]);
                    //if (!Convert.IsDBNull(dr["OtherTax"])) MM.OtherTax = Convert.ToDouble(dr["OtherTax"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TOtherVoucherEntry();
        }

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int cntVchNo = -1, cntRef = 0, cntStock = 0, cntRateSettingNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddTOtherVoucherEntry")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTOtherStock")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkOtherVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            if (cntRef != 0)
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            else
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                            cntStock = i;
                            if (cntRateSettingNo != -1)
                            {
                                commandcollection[i].Parameters["@FkRateSettingNo"].Value = commandcollection[cntRateSettingNo].Parameters["@ReturnID"].Value;

                                //commandcollection[i].CommandText.IndexOf("@FkRateSettingNo", Convert.ToInt32(commandcollection[cntRateSettingNo].Parameters["@ReturnID"].Value));

                                cntRateSettingNo = -1;
                            }
                        }


                        if (commandcollection[i].CommandText.IndexOf("Update") >= 0)
                        {
                            if (cntRef != 0)
                                if (commandcollection[i].CommandText.IndexOf("@pkSrNo") >= 0)
                                {
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                                }
                                else
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddMRateSetting3")
                        {
                            cntRateSettingNo = i;
                        }
                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();


                    }
                }

                myTrans.Commit();
                if (cntVchNo == -1)
                    return 0;
                else
                    return Convert.ToInt64(commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
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

        public bool ExecuteNonQueryStatements1()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

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

        public bool UpdateStatusOVoucherEntry(TOtherVoucherEntry tothervoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TOtherVoucherEntry set VoucherStatus=@VoucherStatus,StatusNo=2 where PkOtherVoucherNo=@PkOtherVoucherNo";

            cmd.Parameters.AddWithValue("@PkOtherVoucherNo", tothervoucherentry.PkOtherVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherStatus", tothervoucherentry.VoucherStatus);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tothervoucherentry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }
    }

    /// <summary>
    /// This Class use for TOtherStock
    /// </summary>
    //public class TOtherStock
    //{
    //    private long mPkOtherStockTrnNo;
    //    private long mFKVoucherNo;
    //    private long mFkVoucherTrnNo;
    //    private long mFkVoucherSrNo;
    //    private long mGroupNo;
    //    private long mItemNo;
    //    private long mTrnCode;
    //    private double mQuantity;
    //    private double mBilledQuantity;
    //    private double mRate;
    //    private double mAmount;
    //    private double mNetRate;
    //    private double mNetAmount;
    //    private double mSGSTPercentage;
    //    private double mSGSTAmount;
    //    private double mDiscPercentage;
    //    private double mDiscAmount;
    //    private double mDiscRupees;
    //    private double mDiscPercentage2;
    //    private double mDiscAmount2;
    //    private double mDiscRupees2;
    //    private long mFkUomNo;
    //    private long mFkStockBarCodeNo;
    //    private long mFkRateSettingNo;
    //    private long mFkItemTaxInfo;
    //    private bool mIsVoucherLock;
    //    private double mFreeQty;
    //    private long mFreeUOMNo;
    //    private long mUserID;
    //    private DateTime mUserDate;
    //    private string mModifiedBy;
    //    private long mCompanyNo;
    //    private int mStatusNo;
    //    private double mLandedRate;
    //    private double mBalanceQty;
    //    private bool mIsComplete;
    //    private string mDisplayItemName;
    //    private string Mmsg;

    //    /// <summary>
    //    /// This Properties use for PkOtherStockTrnNo
    //    /// </summary>
    //    public long PkOtherStockTrnNo
    //    {
    //        get { return mPkOtherStockTrnNo; }
    //        set { mPkOtherStockTrnNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FKVoucherNo
    //    /// </summary>
    //    public long FKVoucherNo
    //    {
    //        get { return mFKVoucherNo; }
    //        set { mFKVoucherNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FkVoucherTrnNo
    //    /// </summary>
    //    public long FkVoucherTrnNo
    //    {
    //        get { return mFkVoucherTrnNo; }
    //        set { mFkVoucherTrnNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FkVoucherSrNo
    //    /// </summary>
    //    public long FkVoucherSrNo
    //    {
    //        get { return mFkVoucherSrNo; }
    //        set { mFkVoucherSrNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for GroupNo
    //    /// </summary>
    //    public long GroupNo
    //    {
    //        get { return mGroupNo; }
    //        set { mGroupNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ItemNo
    //    /// </summary>
    //    public long ItemNo
    //    {
    //        get { return mItemNo; }
    //        set { mItemNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for TrnCode
    //    /// </summary>
    //    public long TrnCode
    //    {
    //        get { return mTrnCode; }
    //        set { mTrnCode = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Quantity
    //    /// </summary>
    //    public double Quantity
    //    {
    //        get { return mQuantity; }
    //        set { mQuantity = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for BilledQuantity
    //    /// </summary>
    //    public double BilledQuantity
    //    {
    //        get { return mBilledQuantity; }
    //        set { mBilledQuantity = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Rate
    //    /// </summary>
    //    public double Rate
    //    {
    //        get { return mRate; }
    //        set { mRate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Amount
    //    /// </summary>
    //    public double Amount
    //    {
    //        get { return mAmount; }
    //        set { mAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for NetRate
    //    /// </summary>
    //    public double NetRate
    //    {
    //        get { return mNetRate; }
    //        set { mNetRate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for NetAmount
    //    /// </summary>
    //    public double NetAmount
    //    {
    //        get { return mNetAmount; }
    //        set { mNetAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for TaxPercentage
    //    /// </summary>
    //    public double SGSTPercentage
    //    {
    //        get { return mSGSTPercentage; }
    //        set { mSGSTPercentage = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for TaxAmount
    //    /// </summary>
    //    public double SGSTAmount
    //    {
    //        get { return mSGSTAmount; }
    //        set { mSGSTAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscPercentage
    //    /// </summary>
    //    public double DiscPercentage
    //    {
    //        get { return mDiscPercentage; }
    //        set { mDiscPercentage = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscAmount
    //    /// </summary>
    //    public double DiscAmount
    //    {
    //        get { return mDiscAmount; }
    //        set { mDiscAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscRupees
    //    /// </summary>
    //    public double DiscRupees
    //    {
    //        get { return mDiscRupees; }
    //        set { mDiscRupees = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscPercentage2
    //    /// </summary>
    //    public double DiscPercentage2
    //    {
    //        get { return mDiscPercentage2; }
    //        set { mDiscPercentage2 = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscAmount2
    //    /// </summary>
    //    public double DiscAmount2
    //    {
    //        get { return mDiscAmount2; }
    //        set { mDiscAmount2 = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscRupees2
    //    /// </summary>
    //    public double DiscRupees2
    //    {
    //        get { return mDiscRupees2; }
    //        set { mDiscRupees2 = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FkUomNo
    //    /// </summary>
    //    public long FkUomNo
    //    {
    //        get { return mFkUomNo; }
    //        set { mFkUomNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FkStockBarCodeNo
    //    /// </summary>
    //    public long FkStockBarCodeNo
    //    {
    //        get { return mFkStockBarCodeNo; }
    //        set { mFkStockBarCodeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FkRateSettingNo
    //    /// </summary>
    //    public long FkRateSettingNo
    //    {
    //        get { return mFkRateSettingNo; }
    //        set { mFkRateSettingNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FkItemTaxInfo
    //    /// </summary>
    //    public long FkItemTaxInfo
    //    {
    //        get { return mFkItemTaxInfo; }
    //        set { mFkItemTaxInfo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsVoucherLock
    //    /// </summary>
    //    public bool IsVoucherLock
    //    {
    //        get { return mIsVoucherLock; }
    //        set { mIsVoucherLock = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FreeQty
    //    /// </summary>
    //    public double FreeQty
    //    {
    //        get { return mFreeQty; }
    //        set { mFreeQty = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for FreeUOMNo
    //    /// </summary>
    //    public long FreeUOMNo
    //    {
    //        get { return mFreeUOMNo; }
    //        set { mFreeUOMNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for UserID
    //    /// </summary>
    //    public long UserID
    //    {
    //        get { return mUserID; }
    //        set { mUserID = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for UserDate
    //    /// </summary>
    //    public DateTime UserDate
    //    {
    //        get { return mUserDate; }
    //        set { mUserDate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ModifiedBy
    //    /// </summary>
    //    public string ModifiedBy
    //    {
    //        get { return mModifiedBy; }
    //        set { mModifiedBy = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for CompanyNo
    //    /// </summary>
    //    public long CompanyNo
    //    {
    //        get { return mCompanyNo; }
    //        set { mCompanyNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for StatusNo
    //    /// </summary>
    //    public int StatusNo
    //    {
    //        get { return mStatusNo; }
    //        set { mStatusNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for LandedRate
    //    /// </summary>
    //    public double LandedRate
    //    {
    //        get { return mLandedRate; }
    //        set { mLandedRate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for BalanceQty
    //    /// </summary>
    //    public double BalanceQty
    //    {
    //        get { return mBalanceQty; }
    //        set { mBalanceQty = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsComplete
    //    /// </summary>
    //    public bool IsComplete
    //    {
    //        get { return mIsComplete; }
    //        set { mIsComplete = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for msg
    //    /// </summary>
    //    public string msg
    //    {
    //        get { return Mmsg; }
    //        set { Mmsg = value; }
    //    }

    //    public string DisplayItemName
    //    {
    //        get { return mDisplayItemName; }
    //        set { mDisplayItemName = value; }
    //    }
    //}
    //public class TOtherStock
    //{
    //    private long mPkVoucherNo;
    //    private long mVoucherTypeCode;
    //    private long mVoucherUserNo;
    //    private DateTime mVoucherDate;
    //    private DateTime mVoucherTime;
    //    private string mNarration;
    //    private string mReference;
    //    private long mChequeNo;
    //    private DateTime mClearingDate;
    //    private long mCompanyNo;
    //    private double mBilledAmount;
    //    private string mChallanNo;
    //    private string mRemark;
    //    private long mInwardLocationCode;
    //    private long mMacNo;
    //    private bool mIsCancel;
    //    private long mPayTypeNo;
    //    private long mRateTypeNo;
    //    private long mTaxTypeNo;
    //    private long mTaxInvoiceTypeNo = 1;
    //    private bool mIsVoucherLock;
    //    private int mVoucherStatus;
    //    private long mUserID;
    //    private DateTime mUserDate;
    //    private string mModifiedBy;
    //    private long mOrderType;
    //    private double mReturnAmount;
    //    private double mVisibility;
    //    private double mDiscPercent;
    //    private double mDiscAmt;
    //    private int mStatusNo;
    //    private int mMixMode;
    //    private bool mIsItemLevelDisc;
    //    private bool mIsFooterLevelDisc;
    //    private int mPrintCount;
    //    private long mBrokerNo;
    //    private long mSuppCategory;
    //    private DateTime mEffectiveDate;
    //    //private double mExcisePercentage;
    //    //private bool mIsExciseBill;
    //    private int mIsBillMulti;
    //    private long mTransporterCode;
    //    private long mTransPayType;
    //    private string mLRNo;
    //    private long mTransportMode;
    //    private double mTransNoOfItems;
    //    private double mChrgesTaxPerce;
    //    private bool mIsTaxFree;
    //    private long mStateCode;
    //    private long mLedgerNo;
    //    private double mTaxAmount;
    //    private long mPkRefNo;
    //    private string Mmsg;

    //    /// <summary>
    //    /// This Properties use for PkVoucherNo
    //    /// </summary>
    //    public long PkVoucherNo
    //    {
    //        get { return mPkVoucherNo; }
    //        set { mPkVoucherNo = value; }
    //    }
    //    public long PkRefNo
    //    {
    //        get { return mPkRefNo; }
    //        set { mPkRefNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherTypeCode
    //    /// </summary>
    //    public long VoucherTypeCode
    //    {
    //        get { return mVoucherTypeCode; }
    //        set { mVoucherTypeCode = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherUserNo
    //    /// </summary>
    //    public long
    //        VoucherUserNo
    //    {
    //        get { return mVoucherUserNo; }
    //        set { mVoucherUserNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherDate
    //    /// </summary>
    //    public DateTime VoucherDate
    //    {
    //        get { return mVoucherDate; }
    //        set { mVoucherDate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherTime
    //    /// </summary>
    //    public DateTime VoucherTime
    //    {
    //        get { return mVoucherTime; }
    //        set { mVoucherTime = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Narration
    //    /// </summary>
    //    public string Narration
    //    {
    //        get { return mNarration; }
    //        set { mNarration = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Reference
    //    /// </summary>
    //    public string Reference
    //    {
    //        get { return mReference; }
    //        set { mReference = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ChequeNo
    //    /// </summary>
    //    public long ChequeNo
    //    {
    //        get { return mChequeNo; }
    //        set { mChequeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ClearingDate
    //    /// </summary>
    //    public DateTime ClearingDate
    //    {
    //        get { return mClearingDate; }
    //        set { mClearingDate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for CompanyNo
    //    /// </summary>
    //    public long CompanyNo
    //    {
    //        get { return mCompanyNo; }
    //        set { mCompanyNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for BilledAmount
    //    /// </summary>
    //    public double BilledAmount
    //    {
    //        get { return mBilledAmount; }
    //        set { mBilledAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ChallanNo
    //    /// </summary>
    //    public string ChallanNo
    //    {
    //        get { return mChallanNo; }
    //        set { mChallanNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Remark
    //    /// </summary>
    //    public string Remark
    //    {
    //        get { return mRemark; }
    //        set { mRemark = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for InwardLocationCode
    //    /// </summary>
    //    public long InwardLocationCode
    //    {
    //        get { return mInwardLocationCode; }
    //        set { mInwardLocationCode = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for MacNo
    //    /// </summary>
    //    public long MacNo
    //    {
    //        get { return mMacNo; }
    //        set { mMacNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsCancel
    //    /// </summary>
    //    public bool IsCancel
    //    {
    //        get { return mIsCancel; }
    //        set { mIsCancel = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for PayTypeNo
    //    /// </summary>
    //    public long PayTypeNo
    //    {
    //        get { return mPayTypeNo; }
    //        set { mPayTypeNo = value; }
    //    }
    //    public long TaxTypeNo
    //    {
    //        get { return mTaxTypeNo; }
    //        set { mTaxTypeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for RateTypeNo
    //    /// </summary>
    //    public long RateTypeNo
    //    {
    //        get { return mRateTypeNo; }
    //        set { mRateTypeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for TaxTypeNo
    //    /// </summary>
    //    //public long TaxTypeNo
    //    //{
    //    //    get { return mTaxTypeNo; }
    //    //    set { mTaxTypeNo = value; }
    //    //}
    //    /// <summary>
    //    /// This Properties use for TaxInvoiceTypeNo
    //    /// </summary>
    //    public long TaxInvoiceTypeNo
    //    {
    //        get { return mTaxInvoiceTypeNo; }
    //        set { mTaxInvoiceTypeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsVoucherLock
    //    /// </summary>
    //    public bool IsVoucherLock
    //    {
    //        get { return mIsVoucherLock; }
    //        set { mIsVoucherLock = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherStatus
    //    /// </summary>
    //    public int VoucherStatus
    //    {
    //        get { return mVoucherStatus; }
    //        set { mVoucherStatus = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for UserID
    //    /// </summary>
    //    public long UserID
    //    {
    //        get { return mUserID; }
    //        set { mUserID = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for UserDate
    //    /// </summary>
    //    public DateTime UserDate
    //    {
    //        get { return mUserDate; }
    //        set { mUserDate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ModifiedBy
    //    /// </summary>
    //    public string ModifiedBy
    //    {
    //        get { return mModifiedBy; }
    //        set { mModifiedBy = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for OrderType
    //    /// </summary>
    //    public long OrderType
    //    {
    //        get { return mOrderType; }
    //        set { mOrderType = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ReturnAmount
    //    /// </summary>
    //    public double ReturnAmount
    //    {
    //        get { return mReturnAmount; }
    //        set { mReturnAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Visibility
    //    /// </summary>
    //    public double Visibility
    //    {
    //        get { return mVisibility; }
    //        set { mVisibility = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscPercent
    //    /// </summary>
    //    public double DiscPercent
    //    {
    //        get { return mDiscPercent; }
    //        set { mDiscPercent = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscAmt
    //    /// </summary>
    //    public double DiscAmt
    //    {
    //        get { return mDiscAmt; }
    //        set { mDiscAmt = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for StatusNo
    //    /// </summary>
    //    public int StatusNo
    //    {
    //        get { return mStatusNo; }
    //        set { mStatusNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for MixMode
    //    /// </summary>
    //    public int MixMode
    //    {
    //        get { return mMixMode; }
    //        set { mMixMode = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsItemLevelDisc
    //    /// </summary>
    //    public bool IsItemLevelDisc
    //    {
    //        get { return mIsItemLevelDisc; }
    //        set { mIsItemLevelDisc = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsFooterLevelDisc
    //    /// </summary>
    //    public bool IsFooterLevelDisc
    //    {
    //        get { return mIsFooterLevelDisc; }
    //        set { mIsFooterLevelDisc = value; }
    //    }
    //    public int PrintCount
    //    {
    //        get { return mPrintCount; }
    //        set { mPrintCount = value; }
    //    }
    //    public long BrokerNo
    //    {
    //        get { return mBrokerNo; }
    //        set { mBrokerNo = value; }
    //    }
    //    public long SuppCategory
    //    {
    //        get { return mSuppCategory; }
    //        set { mSuppCategory = value; }
    //    }
    //    public DateTime EffectiveDate
    //    {
    //        get { return mEffectiveDate; }
    //        set { mEffectiveDate = value; }
    //    }
    //    //public double ExcisePercentage
    //    //{
    //    //    get { return mExcisePercentage; }
    //    //    set { mExcisePercentage = value; }
    //    //}

    //    //public bool IsExciseBill
    //    //{
    //    //    get { return mIsExciseBill; }
    //    //    set { mIsExciseBill = value; }
    //    //}
    //    public int IsBillMulti
    //    {
    //        get { return mIsBillMulti; }
    //        set { mIsBillMulti = value; }
    //    }

    //    /// <summary>
    //    /// This Properties use for msg
    //    /// </summary>
    //    public string msg
    //    {
    //        get { return Mmsg; }
    //        set { Mmsg = value; }
    //    }

    //    public long TransporterCode
    //    {
    //        get { return mTransporterCode; }
    //        set { mTransporterCode = value; }
    //    }
    //    public long TransPayType
    //    {
    //        get { return mTransPayType; }
    //        set { mTransPayType = value; }
    //    }
    //    public string LRNo
    //    {
    //        get { return mLRNo; }
    //        set { mLRNo = value; }
    //    }
    //    public long TransportMode
    //    {
    //        get { return mTransportMode; }
    //        set { mTransportMode = value; }
    //    }
    //    public double TransNoOfItems
    //    {
    //        get { return mTransNoOfItems; }
    //        set { mTransNoOfItems = value; }
    //    }
    //    public double ChrgesTaxPerce
    //    {
    //        get { return mChrgesTaxPerce; }
    //        set { mChrgesTaxPerce = value; }
    //    }

    //    public bool IsTaxFree
    //    {
    //        get { return mIsTaxFree; }
    //        set { mIsTaxFree = value; }
    //    }
    //    public long StateCode
    //    {
    //        get { return mStateCode; }
    //        set { mStateCode = value; }
    //    }
    //    public long LedgerNo
    //    {
    //        get { return mLedgerNo; }
    //        set { mLedgerNo = value; }
    //    }
    //    public double TaxAmount
    //    {
    //        get { return mTaxAmount; }
    //        set { mTaxAmount = value; }
    //    }
    //}
    public class TOtherStock
    {
        private long mPkOtherStockTrnNo;
        private long mFKOtheVoucherNo;
        private long mFkVoucherTrnNo;
        private long mFkVoucherSrNo;
        private long mGroupNo;
        private long mItemNo;
        private long mTrnCode;
        private double mQuantity;
        private double mBilledQuantity;
        private double mRate;
        private double mAmount;
        private double mNetRate;
        private double mNetAmount;
        private double mSGSTPercentage;
        private double mSGSTAmount;
        private double mAddTaxPercentage;
        private double mAddTaxAmount;
        private double mDiscPercentage;
        private double mDiscAmount;
        private double mDiscRupees;
        private double mDiscPercentage2;
        private double mDiscAmount2;
        private double mDiscRupees2;
        private long mFkUomNo;
        private long mFkStockBarCodeNo;
        private long mFkRateSettingNo;
        private long mFkItemTaxInfo;
        private long mFKAddItemTaxSettingNo;
        private bool mIsVoucherLock;
        private double mFreeQty;
        private long mFreeUOMNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private int mStatusNo;
        private double mLandedRate;
        private long mFkGRNNo;
        private string mBatchNo;
        private long mNoOfBag;
        private double mCessValue;
        private double mPackagingCharges;

        private string mDisplayItemName;
        private string Mmsg;

        private double mCGSTPercentage;
        private double mCGSTAmount;
        private double mIGSTPercentage;
        private double mIGSTAmount;
        private long mFkItemTaxInfo2;
        private double mCessAmount;
        private double mCessPercentage;

        private double mTRWeight;
        private double mGRWeight;
        private string mRemarks;
        private double mFreight;
        private double mOtherCharges;
        public int mSalesMan;

        /// <summary>
        /// This Properties use for PkStockTrnNo
        /// </summary>
        public long PkOtherStockTrnNo
        {
            get { return mPkOtherStockTrnNo; }
            set { mPkOtherStockTrnNo = value; }
        }
        /// <summary>
        /// This Properties use for FKVoucherNo
        /// </summary>
        public long FKOtheVoucherNo
        {
            get { return mFKOtheVoucherNo; }
            set { mFKOtheVoucherNo = value; }
        }
        /// <summary>
        /// This Properties use for FkVoucherTrnNo
        /// </summary>
        public long FkVoucherTrnNo
        {
            get { return mFkVoucherTrnNo; }
            set { mFkVoucherTrnNo = value; }
        }
        /// <summary>
        /// This Properties use for FkVoucherSrNo
        /// </summary>
        public long FkVoucherSrNo
        {
            get { return mFkVoucherSrNo; }
            set { mFkVoucherSrNo = value; }
        }
        /// <summary>
        /// This Properties use for GroupNo
        /// </summary>
        public long GroupNo
        {
            get { return mGroupNo; }
            set { mGroupNo = value; }
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
        /// This Properties use for TrnCode
        /// </summary>
        public long TrnCode
        {
            get { return mTrnCode; }
            set { mTrnCode = value; }
        }
        /// <summary>
        /// This Properties use for Quantity
        /// </summary>
        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }
        /// <summary>
        /// This Properties use for BilledQuantity
        /// </summary>
        public double BilledQuantity
        {
            get { return mBilledQuantity; }
            set { mBilledQuantity = value; }
        }
        /// <summary>
        /// This Properties use for Rate
        /// </summary>
        public double Rate
        {
            get { return mRate; }
            set { mRate = value; }
        }
        /// <summary>
        /// This Properties use for Amount
        /// </summary>
        public double Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        /// <summary>
        /// This Properties use for NetRate
        /// </summary>
        public double NetRate
        {
            get { return mNetRate; }
            set { mNetRate = value; }
        }
        /// <summary>
        /// This Properties use for NetAmount
        /// </summary>
        public double NetAmount
        {
            get { return mNetAmount; }
            set { mNetAmount = value; }
        }
        /// <summary>
        /// This Properties use for TaxPercentage
        /// </summary>
        public double SGSTPercentage
        {
            get { return mSGSTPercentage; }
            set { mSGSTPercentage = value; }
        }
        /// <summary>
        /// This Properties use for TaxAmount
        /// </summary>
        public double SGSTAmount
        {
            get { return mSGSTAmount; }
            set { mSGSTAmount = value; }
        }
        /// <summary>
        /// This Properties use for DiscPercentage
        /// </summary>
        public double AddTaxPercentage
        {
            get { return mAddTaxPercentage; }
            set { mAddTaxPercentage = value; }
        }
        public double AddTaxAmount
        {
            get { return mAddTaxAmount; }
            set { mAddTaxAmount = value; }
        }
        public double DiscPercentage
        {
            get { return mDiscPercentage; }
            set { mDiscPercentage = value; }
        }
        /// <summary>
        /// This Properties use for DiscAmount
        /// </summary>
        public double DiscAmount
        {
            get { return mDiscAmount; }
            set { mDiscAmount = value; }
        }
        /// <summary>
        /// This Properties use for DiscRupees
        /// </summary>
        public double DiscRupees
        {
            get { return mDiscRupees; }
            set { mDiscRupees = value; }
        }
        /// <summary>
        /// This Properties use for DiscPercentage2
        /// </summary>
        public double DiscPercentage2
        {
            get { return mDiscPercentage2; }
            set { mDiscPercentage2 = value; }
        }
        /// <summary>
        /// This Properties use for DiscAmount2
        /// </summary>
        public double DiscAmount2
        {
            get { return mDiscAmount2; }
            set { mDiscAmount2 = value; }
        }
        /// <summary>
        /// This Properties use for DiscRupees2
        /// </summary>
        public double DiscRupees2
        {
            get { return mDiscRupees2; }
            set { mDiscRupees2 = value; }
        }
        /// <summary>
        /// This Properties use for FkUomNo
        /// </summary>
        public long FkUomNo
        {
            get { return mFkUomNo; }
            set { mFkUomNo = value; }
        }
        /// <summary>
        /// This Properties use for FkStockBarCodeNo
        /// </summary>
        public long FkStockBarCodeNo
        {
            get { return mFkStockBarCodeNo; }
            set { mFkStockBarCodeNo = value; }
        }
        /// <summary>
        /// This Properties use for FkRateSettingNo
        /// </summary>
        public long FkRateSettingNo
        {
            get { return mFkRateSettingNo; }
            set { mFkRateSettingNo = value; }
        }
        /// <summary>
        /// This Properties use for FkItemTaxInfo
        /// </summary>
        public long FkItemTaxInfo
        {
            get { return mFkItemTaxInfo; }
            set { mFkItemTaxInfo = value; }
        }
        public long FKAddItemTaxSettingNo
        {
            get { return mFKAddItemTaxSettingNo; }
            set { mFKAddItemTaxSettingNo = value; }
        }
        /// <summary>
        /// This Properties use for IsVoucherLock
        /// </summary>
        public bool IsVoucherLock
        {
            get { return mIsVoucherLock; }
            set { mIsVoucherLock = value; }
        }
        /// <summary>
        /// This Properties use for FreeQty
        /// </summary>
        public double FreeQty
        {
            get { return mFreeQty; }
            set { mFreeQty = value; }
        }
        /// <summary>
        /// This Properties use for FreeUOMNo
        /// </summary>
        public long FreeUOMNo
        {
            get { return mFreeUOMNo; }
            set { mFreeUOMNo = value; }
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
        /// This Properties use for LandedRate
        /// </summary>
        public double LandedRate
        {
            get { return mLandedRate; }
            set { mLandedRate = value; }
        }
        public long FkGRNNo
        {
            get { return mFkGRNNo; }
            set { mFkGRNNo = value; }
        }
        public long NoOfBag
        {
            get { return mNoOfBag; }
            set { mNoOfBag = value; }
        }
        public string BatchNo
        {
            get { return mBatchNo; }
            set { mBatchNo = value; }
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

        public string DisplayItemName
        {
            get { return mDisplayItemName; }
            set { mDisplayItemName = value; }
        }

        /// <summary>
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }


        public double CGSTPercentage
        {
            get { return mCGSTPercentage; }
            set { mCGSTPercentage = value; }
        }
        public double CGSTAmount
        {
            get { return mCGSTAmount; }
            set { mCGSTAmount = value; }
        }
        public double IGSTPercentage
        {
            get { return mIGSTPercentage; }
            set { mIGSTPercentage = value; }
        }
        public double IGSTAmount
        {
            get { return mIGSTAmount; }
            set { mIGSTAmount = value; }
        }
        public long FkItemTaxInfo2
        {
            get { return mFkItemTaxInfo2; }
            set { mFkItemTaxInfo2 = value; }
        }
        public double CessAmount
        {
            get { return mCessAmount; }
            set { mCessAmount = value; }
        }
        public double CessPercentage
        {
            get { return mCessPercentage; }
            set { mCessPercentage = value; }
        }


        public double TRWeight
        {
            get { return mTRWeight; }
            set { mTRWeight = value; }
        }
        public double GRWeight
        {
            get { return mGRWeight; }
            set { mGRWeight = value; }

        }
        public string Remarks
        {
            get { return mRemarks; }
            set { mRemarks = value; }
        }
        public double Freight
        {
            get { return mFreight; }
            set { mFreight = value; }
        }
        public double OtherCharges
        {
            get { return mOtherCharges; }
            set { mOtherCharges = value; }

        }
        public int SalesMan
        {
            get
            {
                return mSalesMan;
            }
            set { mSalesMan = value; }
        }
    }
    /// <summary>
    /// This Class use for TOtherVoucherEntry
    /// </summary>
    //public class TOtherVoucherEntry
    //{
    //    private long mPkOtherVoucherNo;
    //    private long mVoucherTypeCode;
    //    private long mVoucherUserNo;
    //    private DateTime mVoucherDate;
    //    private DateTime mVoucherTime;
    //    private string mNarration;
    //    private string mReference;
    //    private long mChequeNo;
    //    private DateTime mClearingDate;
    //    private long mCompanyNo;
    //    private double mBilledAmount;
    //    private string mChallanNo;
    //    private string mRemark;
    //    private long mInwardLocationCode;
    //    private long mMacNo;
    //    private bool mIsCancel;
    //    private long mPayTypeNo;
    //    private long mRateTypeNo;
    //    private long mTaxTypeNo;
    //    private bool mIsVoucherLock;
    //    private int mVoucherStatus;
    //    private long mUserID;
    //    private DateTime mUserDate;
    //    private string mModifiedBy;
    //    private long mOrderType;
    //    private double mReturnAmount;
    //    private double mVisibility;
    //    private double mDiscPercent;
    //    private double mDiscAmt;
    //    private int mStatusNo;
    //    private int mMixMode;
    //    private bool mIsItemLevelDisc;
    //    private bool mIsFooterLevelDisc;
    //    private double mSchemeDisc;
    //    private double mDistDisc;
    //    private double mCashDisc;
    //    private double mCharges;
    //    private double mSubTotal;
    //    private double mTotalTax;
    //    private double mRoundOff;
    //    private double mOtherTax;
    //    private long mLedgerNo;
    //    private bool mIsComplete;
    //    private long mVersionNo;
    //    //private long mServerOtherVoucherNo;
    //    private string Mmsg;

    //    /// <summary>
    //    /// This Properties use for PkOtherVoucherNo
    //    /// </summary>
    //    public long PkOtherVoucherNo
    //    {
    //        get { return mPkOtherVoucherNo; }
    //        set { mPkOtherVoucherNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherTypeCode
    //    /// </summary>
    //    public long VoucherTypeCode
    //    {
    //        get { return mVoucherTypeCode; }
    //        set { mVoucherTypeCode = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherUserNo
    //    /// </summary>
    //    public long VoucherUserNo
    //    {
    //        get { return mVoucherUserNo; }
    //        set { mVoucherUserNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherDate
    //    /// </summary>
    //    public DateTime VoucherDate
    //    {
    //        get { return mVoucherDate; }
    //        set { mVoucherDate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherTime
    //    /// </summary>
    //    public DateTime VoucherTime
    //    {
    //        get { return mVoucherTime; }
    //        set { mVoucherTime = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Narration
    //    /// </summary>
    //    public string Narration
    //    {
    //        get { return mNarration; }
    //        set { mNarration = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Reference
    //    /// </summary>
    //    public string Reference
    //    {
    //        get { return mReference; }
    //        set { mReference = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ChequeNo
    //    /// </summary>
    //    public long ChequeNo
    //    {
    //        get { return mChequeNo; }
    //        set { mChequeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ClearingDate
    //    /// </summary>
    //    public DateTime ClearingDate
    //    {
    //        get { return mClearingDate; }
    //        set { mClearingDate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for CompanyNo
    //    /// </summary>
    //    public long CompanyNo
    //    {
    //        get { return mCompanyNo; }
    //        set { mCompanyNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for BilledAmount
    //    /// </summary>
    //    public double BilledAmount
    //    {
    //        get { return mBilledAmount; }
    //        set { mBilledAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ChallanNo
    //    /// </summary>
    //    public string ChallanNo
    //    {
    //        get { return mChallanNo; }
    //        set { mChallanNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Remark
    //    /// </summary>
    //    public string Remark
    //    {
    //        get { return mRemark; }
    //        set { mRemark = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for InwardLocationCode
    //    /// </summary>
    //    public long InwardLocationCode
    //    {
    //        get { return mInwardLocationCode; }
    //        set { mInwardLocationCode = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for MacNo
    //    /// </summary>
    //    public long MacNo
    //    {
    //        get { return mMacNo; }
    //        set { mMacNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsCancel
    //    /// </summary>
    //    public bool IsCancel
    //    {
    //        get { return mIsCancel; }
    //        set { mIsCancel = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for PayTypeNo
    //    /// </summary>
    //    public long PayTypeNo
    //    {
    //        get { return mPayTypeNo; }
    //        set { mPayTypeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for RateTypeNo
    //    /// </summary>
    //    public long RateTypeNo
    //    {
    //        get { return mRateTypeNo; }
    //        set { mRateTypeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for TaxTypeNo
    //    /// </summary>
    //    public long TaxTypeNo
    //    {
    //        get { return mTaxTypeNo; }
    //        set { mTaxTypeNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsVoucherLock
    //    /// </summary>
    //    public bool IsVoucherLock
    //    {
    //        get { return mIsVoucherLock; }
    //        set { mIsVoucherLock = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for VoucherStatus
    //    /// </summary>
    //    public int VoucherStatus
    //    {
    //        get { return mVoucherStatus; }
    //        set { mVoucherStatus = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for UserID
    //    /// </summary>
    //    public long UserID
    //    {
    //        get { return mUserID; }
    //        set { mUserID = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for UserDate
    //    /// </summary>
    //    public DateTime UserDate
    //    {
    //        get { return mUserDate; }
    //        set { mUserDate = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ModifiedBy
    //    /// </summary>
    //    public string ModifiedBy
    //    {
    //        get { return mModifiedBy; }
    //        set { mModifiedBy = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for OrderType
    //    /// </summary>
    //    public long OrderType
    //    {
    //        get { return mOrderType; }
    //        set { mOrderType = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for ReturnAmount
    //    /// </summary>
    //    public double ReturnAmount
    //    {
    //        get { return mReturnAmount; }
    //        set { mReturnAmount = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Visibility
    //    /// </summary>
    //    public double Visibility
    //    {
    //        get { return mVisibility; }
    //        set { mVisibility = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscPercent
    //    /// </summary>
    //    public double DiscPercent
    //    {
    //        get { return mDiscPercent; }
    //        set { mDiscPercent = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DiscAmt
    //    /// </summary>
    //    public double DiscAmt
    //    {
    //        get { return mDiscAmt; }
    //        set { mDiscAmt = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for StatusNo
    //    /// </summary>
    //    public int StatusNo
    //    {
    //        get { return mStatusNo; }
    //        set { mStatusNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for MixMode
    //    /// </summary>
    //    public int MixMode
    //    {
    //        get { return mMixMode; }
    //        set { mMixMode = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsItemLevelDisc
    //    /// </summary>
    //    public bool IsItemLevelDisc
    //    {
    //        get { return mIsItemLevelDisc; }
    //        set { mIsItemLevelDisc = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsFooterLevelDisc
    //    /// </summary>
    //    public bool IsFooterLevelDisc
    //    {
    //        get { return mIsFooterLevelDisc; }
    //        set { mIsFooterLevelDisc = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for SchemeDisc
    //    /// </summary>
    //    public double SchemeDisc
    //    {
    //        get { return mSchemeDisc; }
    //        set { mSchemeDisc = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for DistDisc
    //    /// </summary>
    //    public double DistDisc
    //    {
    //        get { return mDistDisc; }
    //        set { mDistDisc = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for CashDisc
    //    /// </summary>
    //    public double CashDisc
    //    {
    //        get { return mCashDisc; }
    //        set { mCashDisc = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for Charges
    //    /// </summary>
    //    public double Charges
    //    {
    //        get { return mCharges; }
    //        set { mCharges = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for SubTotal
    //    /// </summary>
    //    public double SubTotal
    //    {
    //        get { return mSubTotal; }
    //        set { mSubTotal = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for TotalTax
    //    /// </summary>
    //    public double TotalTax
    //    {
    //        get { return mTotalTax; }
    //        set { mTotalTax = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for RoundOff
    //    /// </summary>
    //    public double RoundOff
    //    {
    //        get { return mRoundOff; }
    //        set { mRoundOff = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for OtherTax
    //    /// </summary>
    //    public double OtherTax
    //    {
    //        get { return mOtherTax; }
    //        set { mOtherTax = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for LedgerNo
    //    /// </summary>
    //    public long LedgerNo
    //    {
    //        get { return mLedgerNo; }
    //        set { mLedgerNo = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for IsComplete
    //    /// </summary>
    //    public bool IsComplete
    //    {
    //        get { return mIsComplete; }
    //        set { mIsComplete = value; }
    //    }
    //    /// <summary>
    //    /// This Properties use for msg
    //    /// </summary>
    //    public string msg
    //    {
    //        get { return Mmsg; }
    //        set { Mmsg = value; }
    //    }

    //    public long VersionNo
    //    {
    //        get { return mVersionNo; }
    //        set { mVersionNo = value; }
    //    }

    //    //public long ServerOtherVoucherNo
    //    //{
    //    //    get { return mServerOtherVoucherNo; }
    //    //    set { mServerOtherVoucherNo = value; }
    //    //}
    //}
    public class TOtherVoucherEntry
    {
        private long mPkOtherVoucherNo;
        private long mVoucherTypeCode;
        private long mVoucherUserNo;
        private DateTime mVoucherDate;
        private DateTime mVoucherTime;
        private string mNarration;
        private string mReference;
        private long mChequeNo;
        private DateTime mClearingDate;
        private long mCompanyNo;
        private double mBilledAmount;
        private string mChallanNo;
        private string mRemark;
        private long mInwardLocationCode;
        private long mMacNo;
        private bool mIsCancel;
        private long mPayTypeNo;
        private long mRateTypeNo;
        private long mTaxTypeNo;
        private long mTaxInvoiceTypeNo = 1;
        private bool mIsVoucherLock;
        private int mVoucherStatus;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mOrderType;
        private double mReturnAmount;
        private double mVisibility;
        private double mDiscPercent;
        private double mDiscAmt;
        private int mStatusNo;
        private int mMixMode;
        private bool mIsItemLevelDisc;
        private bool mIsFooterLevelDisc;
        private int mPrintCount;
        private long mBrokerNo;
        private long mSuppCategory;
        private DateTime mEffectiveDate;
        //private double mExcisePercentage;
        //private bool mIsExciseBill;
        private int mIsBillMulti;
        private long mTransporterCode;
        private long mTransPayType;
        private string mLRNo;
        private long mTransportMode;
        private double mTransNoOfItems;
        private double mChrgesTaxPerce;
        private bool mIsTaxFree;
        private long mStateCode;
        private long mLedgerNo;
        private double mTaxAmount;
        private long mPkRefNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkVoucherNo
        /// </summary>
        public long PkOtherVoucherNo
        {
            get { return mPkOtherVoucherNo; }
            set { mPkOtherVoucherNo = value; }
        }
        public long PkRefNo
        {
            get { return mPkRefNo; }
            set { mPkRefNo = value; }
        }
        /// <summary>
        /// This Properties use for VoucherTypeCode
        /// </summary>
        public long VoucherTypeCode
        {
            get { return mVoucherTypeCode; }
            set { mVoucherTypeCode = value; }
        }
        /// <summary>
        /// This Properties use for VoucherUserNo
        /// </summary>
        public long
            VoucherUserNo
        {
            get { return mVoucherUserNo; }
            set { mVoucherUserNo = value; }
        }
        /// <summary>
        /// This Properties use for VoucherDate
        /// </summary>
        public DateTime VoucherDate
        {
            get { return mVoucherDate; }
            set { mVoucherDate = value; }
        }
        /// <summary>
        /// This Properties use for VoucherTime
        /// </summary>
        public DateTime VoucherTime
        {
            get { return mVoucherTime; }
            set { mVoucherTime = value; }
        }
        /// <summary>
        /// This Properties use for Narration
        /// </summary>
        public string Narration
        {
            get { return mNarration; }
            set { mNarration = value; }
        }
        /// <summary>
        /// This Properties use for Reference
        /// </summary>
        public string Reference
        {
            get { return mReference; }
            set { mReference = value; }
        }
        /// <summary>
        /// This Properties use for ChequeNo
        /// </summary>
        public long ChequeNo
        {
            get { return mChequeNo; }
            set { mChequeNo = value; }
        }
        /// <summary>
        /// This Properties use for ClearingDate
        /// </summary>
        public DateTime ClearingDate
        {
            get { return mClearingDate; }
            set { mClearingDate = value; }
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
        /// This Properties use for BilledAmount
        /// </summary>
        public double BilledAmount
        {
            get { return mBilledAmount; }
            set { mBilledAmount = value; }
        }
        /// <summary>
        /// This Properties use for ChallanNo
        /// </summary>
        public string ChallanNo
        {
            get { return mChallanNo; }
            set { mChallanNo = value; }
        }
        /// <summary>
        /// This Properties use for Remark
        /// </summary>
        public string Remark
        {
            get { return mRemark; }
            set { mRemark = value; }
        }
        /// <summary>
        /// This Properties use for InwardLocationCode
        /// </summary>
        public long InwardLocationCode
        {
            get { return mInwardLocationCode; }
            set { mInwardLocationCode = value; }
        }
        /// <summary>
        /// This Properties use for MacNo
        /// </summary>
        public long MacNo
        {
            get { return mMacNo; }
            set { mMacNo = value; }
        }
        /// <summary>
        /// This Properties use for IsCancel
        /// </summary>
        public bool IsCancel
        {
            get { return mIsCancel; }
            set { mIsCancel = value; }
        }
        /// <summary>
        /// This Properties use for PayTypeNo
        /// </summary>
        public long PayTypeNo
        {
            get { return mPayTypeNo; }
            set { mPayTypeNo = value; }
        }
        public long TaxTypeNo
        {
            get { return mTaxTypeNo; }
            set { mTaxTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for RateTypeNo
        /// </summary>
        public long RateTypeNo
        {
            get { return mRateTypeNo; }
            set { mRateTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for TaxTypeNo
        /// </summary>
        //public long TaxTypeNo
        //{
        //    get { return mTaxTypeNo; }
        //    set { mTaxTypeNo = value; }
        //}
        /// <summary>
        /// This Properties use for TaxInvoiceTypeNo
        /// </summary>
        public long TaxInvoiceTypeNo
        {
            get { return mTaxInvoiceTypeNo; }
            set { mTaxInvoiceTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for IsVoucherLock
        /// </summary>
        public bool IsVoucherLock
        {
            get { return mIsVoucherLock; }
            set { mIsVoucherLock = value; }
        }
        /// <summary>
        /// This Properties use for VoucherStatus
        /// </summary>
        public int VoucherStatus
        {
            get { return mVoucherStatus; }
            set { mVoucherStatus = value; }
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
        /// This Properties use for OrderType
        /// </summary>
        public long OrderType
        {
            get { return mOrderType; }
            set { mOrderType = value; }
        }
        /// <summary>
        /// This Properties use for ReturnAmount
        /// </summary>
        public double ReturnAmount
        {
            get { return mReturnAmount; }
            set { mReturnAmount = value; }
        }
        /// <summary>
        /// This Properties use for Visibility
        /// </summary>
        public double Visibility
        {
            get { return mVisibility; }
            set { mVisibility = value; }
        }
        /// <summary>
        /// This Properties use for DiscPercent
        /// </summary>
        public double DiscPercent
        {
            get { return mDiscPercent; }
            set { mDiscPercent = value; }
        }
        /// <summary>
        /// This Properties use for DiscAmt
        /// </summary>
        public double DiscAmt
        {
            get { return mDiscAmt; }
            set { mDiscAmt = value; }
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
        /// This Properties use for MixMode
        /// </summary>
        public int MixMode
        {
            get { return mMixMode; }
            set { mMixMode = value; }
        }
        /// <summary>
        /// This Properties use for IsItemLevelDisc
        /// </summary>
        public bool IsItemLevelDisc
        {
            get { return mIsItemLevelDisc; }
            set { mIsItemLevelDisc = value; }
        }
        /// <summary>
        /// This Properties use for IsFooterLevelDisc
        /// </summary>
        public bool IsFooterLevelDisc
        {
            get { return mIsFooterLevelDisc; }
            set { mIsFooterLevelDisc = value; }
        }
        public int PrintCount
        {
            get { return mPrintCount; }
            set { mPrintCount = value; }
        }
        public long BrokerNo
        {
            get { return mBrokerNo; }
            set { mBrokerNo = value; }
        }
        public long SuppCategory
        {
            get { return mSuppCategory; }
            set { mSuppCategory = value; }
        }
        public DateTime EffectiveDate
        {
            get { return mEffectiveDate; }
            set { mEffectiveDate = value; }
        }
        //public double ExcisePercentage
        //{
        //    get { return mExcisePercentage; }
        //    set { mExcisePercentage = value; }
        //}

        //public bool IsExciseBill
        //{
        //    get { return mIsExciseBill; }
        //    set { mIsExciseBill = value; }
        //}
        public int IsBillMulti
        {
            get { return mIsBillMulti; }
            set { mIsBillMulti = value; }
        }

        /// <summary>
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }

        public long TransporterCode
        {
            get { return mTransporterCode; }
            set { mTransporterCode = value; }
        }
        public long TransPayType
        {
            get { return mTransPayType; }
            set { mTransPayType = value; }
        }
        public string LRNo
        {
            get { return mLRNo; }
            set { mLRNo = value; }
        }
        public long TransportMode
        {
            get { return mTransportMode; }
            set { mTransportMode = value; }
        }
        public double TransNoOfItems
        {
            get { return mTransNoOfItems; }
            set { mTransNoOfItems = value; }
        }
        public double ChrgesTaxPerce
        {
            get { return mChrgesTaxPerce; }
            set { mChrgesTaxPerce = value; }
        }

        public bool IsTaxFree
        {
            get { return mIsTaxFree; }
            set { mIsTaxFree = value; }
        }
        public long StateCode
        {
            get { return mStateCode; }
            set { mStateCode = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public double TaxAmount
        {
            get { return mTaxAmount; }
            set { mTaxAmount = value; }
        }
    }
}
