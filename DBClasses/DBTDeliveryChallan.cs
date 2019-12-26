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
    public class DBTDeliveryChallan
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        public CommandCollection commandcollection = new CommandCollection();
        public static DataTable dtCompRatio = new DataTable();



        DataTable dtId = new DataTable();
        DataTable dtZeroTax = new DataTable();

        //SaveBill 

        DataTable dtHeader = new DataTable();
        DataTable dtDetails = new DataTable();
        public static int HeaderLen = 21, DataLen = 23, StockDataLen = 5;
        public static string strerrormsg;

        public bool AddTDeliveryChallan(TDeliveryChallan tdeliverychallan)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTDeliveryChallan";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tdeliverychallan.PkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherTypeCode", tdeliverychallan.VoucherTypeCode);

            cmd.Parameters.AddWithValue("@VoucherUserNo", tdeliverychallan.VoucherUserNo);

            cmd.Parameters.AddWithValue("@VoucherDate", tdeliverychallan.VoucherDate);

            cmd.Parameters.AddWithValue("@VoucherTime", tdeliverychallan.VoucherTime);

            cmd.Parameters.AddWithValue("@Narration", tdeliverychallan.Narration);

            cmd.Parameters.AddWithValue("@Reference", tdeliverychallan.Reference);

            cmd.Parameters.AddWithValue("@ChequeNo", tdeliverychallan.ChequeNo);

            cmd.Parameters.AddWithValue("@ClearingDate", tdeliverychallan.ClearingDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tdeliverychallan.CompanyNo);

            cmd.Parameters.AddWithValue("@BilledAmount", tdeliverychallan.BilledAmount);

            cmd.Parameters.AddWithValue("@ChallanNo", tdeliverychallan.ChallanNo);

            cmd.Parameters.AddWithValue("@Remark", tdeliverychallan.Remark);

            cmd.Parameters.AddWithValue("@InwardLocationCode", tdeliverychallan.InwardLocationCode);

            cmd.Parameters.AddWithValue("@MacNo", tdeliverychallan.MacNo);

            cmd.Parameters.AddWithValue("@PayTypeNo", tdeliverychallan.PayTypeNo);

            cmd.Parameters.AddWithValue("@RateTypeNo", tdeliverychallan.RateTypeNo);

            cmd.Parameters.AddWithValue("@TaxTypeNo", tdeliverychallan.TaxTypeNo);

            cmd.Parameters.AddWithValue("@OrderType", tdeliverychallan.OrderType);

            cmd.Parameters.AddWithValue("@ReturnAmount", tdeliverychallan.ReturnAmount);

            cmd.Parameters.AddWithValue("@Visibility", tdeliverychallan.Visibility);

            cmd.Parameters.AddWithValue("@DiscPercent", tdeliverychallan.DiscPercent);

            cmd.Parameters.AddWithValue("@DiscAmt", tdeliverychallan.DiscAmt);

            cmd.Parameters.AddWithValue("@MixMode", tdeliverychallan.MixMode);

            cmd.Parameters.AddWithValue("@IsItemLevelDisc", tdeliverychallan.IsItemLevelDisc);

            cmd.Parameters.AddWithValue("@IsFooterLevelDisc", tdeliverychallan.IsFooterLevelDisc);

            cmd.Parameters.AddWithValue("@UserID", tdeliverychallan.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tdeliverychallan.UserDate);

            cmd.Parameters.AddWithValue("@BrokerNo", tdeliverychallan.BrokerNo);

            cmd.Parameters.AddWithValue("@SuppCategory", tdeliverychallan.SuppCategory);

            cmd.Parameters.AddWithValue("@EffectiveDate", (tdeliverychallan.EffectiveDate == Convert.ToDateTime("01-Jan-0001")) ? Convert.ToDateTime("01-Jan-1900") : tdeliverychallan.EffectiveDate);

            cmd.Parameters.AddWithValue("@ExcisePercentage", tdeliverychallan.ExcisePercentage);

            cmd.Parameters.AddWithValue("@IsExciseBill", tdeliverychallan.IsExciseBill);

            cmd.Parameters.AddWithValue("@IsBillMulti", tdeliverychallan.IsBillMulti);

            cmd.Parameters.AddWithValue("@TransporterCode", tdeliverychallan.TransporterCode);

            cmd.Parameters.AddWithValue("@TransPayType", tdeliverychallan.TransPayType);

            cmd.Parameters.AddWithValue("@LRNo", tdeliverychallan.LRNo);

            cmd.Parameters.AddWithValue("@TransportMode", tdeliverychallan.TransportMode);

            cmd.Parameters.AddWithValue("@TransNoOfItems", tdeliverychallan.TransNoOfItems);

            cmd.Parameters.AddWithValue("@ChrgesTaxPerce", tdeliverychallan.ChrgesTaxPerce);

            cmd.Parameters.AddWithValue("@MfgCompNo", tdeliverychallan.MfgCompNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tdeliverychallan.LedgerNo);
            cmd.Parameters.AddWithValue("@ChargesAmt1", tdeliverychallan.ChargesAmt1);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTDeliveryChallan(TDeliveryChallan tdeliverychallan)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherEntry";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tdeliverychallan.PkVoucherNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTDeliveryChallan1(TDeliveryChallan tdeliverychallan)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherEntry1";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tdeliverychallan.PkVoucherNo);
            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTDeliveryChallan()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherEntry order by PkVoucherNo";
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

        public DataView GetTDeliveryChallanByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherEntry where PkVoucherNo =" + ID;
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

        public TDeliveryChallan ModifyTDeliveryChallanByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TDeliveryChallan where PkVoucherNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TDeliveryChallan MM = new TDeliveryChallan();
                while (dr.Read())
                {
                    MM.PkVoucherNo = Convert.ToInt32(dr["PkVoucherNo"]);
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
                    if (!Convert.IsDBNull(dr["PayTypeNo"])) MM.PayTypeNo = Convert.ToInt64(dr["PayTypeNo"]);
                    if (!Convert.IsDBNull(dr["RateTypeNo"])) MM.RateTypeNo = Convert.ToInt64(dr["RateTypeNo"]);
                    if (!Convert.IsDBNull(dr["TaxTypeNo"])) MM.TaxTypeNo = Convert.ToInt64(dr["TaxTypeNo"]);
                    if (!Convert.IsDBNull(dr["IsVoucherLock"])) MM.IsVoucherLock = Convert.ToBoolean(dr["IsVoucherLock"]);
                    if (!Convert.IsDBNull(dr["IsCancel"])) MM.IsCancel = Convert.ToBoolean(dr["IsCancel"]);
                    if (!Convert.IsDBNull(dr["OrderType"])) MM.OrderType = Convert.ToInt64(dr["OrderType"]);
                    if (!Convert.IsDBNull(dr["ReturnAmount"])) MM.ReturnAmount = Convert.ToDouble(dr["ReturnAmount"]);
                    if (!Convert.IsDBNull(dr["Visibility"])) MM.Visibility = Convert.ToDouble(dr["Visibility"]);
                    if (!Convert.IsDBNull(dr["DiscPercent"])) MM.DiscPercent = Convert.ToDouble(dr["DiscPercent"]);
                    if (!Convert.IsDBNull(dr["DiscAmt"])) MM.DiscAmt = Convert.ToDouble(dr["DiscAmt"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["MixMode"])) MM.MixMode = Convert.ToInt32(dr["MixMode"]);
                    if (!Convert.IsDBNull(dr["IsItemLevelDisc"])) MM.IsItemLevelDisc = Convert.ToBoolean(dr["IsItemLevelDisc"]);
                    if (!Convert.IsDBNull(dr["IsFooterLevelDisc"])) MM.IsFooterLevelDisc = Convert.ToBoolean(dr["IsFooterLevelDisc"]);
                    if (!Convert.IsDBNull(dr["PrintCount"])) MM.PrintCount = Convert.ToInt32(dr["PrintCount"]);
                    if (!Convert.IsDBNull(dr["BrokerNo"])) MM.BrokerNo = Convert.ToInt32(dr["BrokerNo"]);
                    if (!Convert.IsDBNull(dr["SuppCategory"])) MM.SuppCategory = Convert.ToInt32(dr["SuppCategory"]);
                    if (!Convert.IsDBNull(dr["EffectiveDate"])) MM.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]);
                    if (!Convert.IsDBNull(dr["ExcisePercentage"])) MM.ExcisePercentage = Convert.ToDouble(dr["ExcisePercentage"]);
                    if (!Convert.IsDBNull(dr["IsExciseBill"])) MM.IsExciseBill = Convert.ToBoolean(dr["IsExciseBill"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["TransporterCode"])) MM.TransporterCode = Convert.ToInt64(dr["TransporterCode"]);
                    if (!Convert.IsDBNull(dr["TransPayType"])) MM.TransPayType = Convert.ToInt64(dr["TransPayType"]);
                    if (!Convert.IsDBNull(dr["LRNo"])) MM.LRNo = Convert.ToString(dr["LRNo"]);
                    if (!Convert.IsDBNull(dr["TransportMode"])) MM.TransportMode = Convert.ToInt64(dr["TransportMode"]);
                    if (!Convert.IsDBNull(dr["TransNoOfItems"])) MM.TransNoOfItems = Convert.ToDouble(dr["TransNoOfItems"]);
                    if (!Convert.IsDBNull(dr["ChrgesTaxPerce"])) MM.ChrgesTaxPerce = Convert.ToDouble(dr["ChrgesTaxPerce"]);
                    if (!Convert.IsDBNull(dr["FKVoucherNo"])) MM.FKVoucherNo = Convert.ToInt64(dr["FKVoucherNo"]);
                    
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TDeliveryChallan();
        }

        public DataView GetBySearch(string Column, string Value, long VchCode)
        {

            string sql = " SELECT TVoucherEntry.PkVoucherNo,TVoucherEntry.VoucherUserNo As 'V.No', MLedger.LedgerName, Sum(Case When (TVoucherDetails.Debit>0) then  TVoucherDetails.Debit else TVoucherDetails.Credit " +
                         " END) AS 'Amount' FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo CROSS JOIN TVoucherEntry Where SrNo=" + Others.Party + "" +
                         " And TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo and TVoucherEntry.VoucherTypeCode=" + VchCode + " GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherUserNo, MLedger.LedgerName order by TVoucherEntry.PkVoucherNo";
            switch (Column)
            {
                case "0":
                    sql = " SELECT TVoucherEntry.PkVoucherNo,TVoucherEntry.VoucherUserNo 'V.No', MLedger.LedgerName, Sum(Case When (TVoucherDetails.Debit>0) then  TVoucherDetails.Debit else TVoucherDetails.Credit " +
                         " END) AS 'Amount' FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo CROSS JOIN TVoucherEntry Where SrNo=" + Others.Party + "" +
                         " And TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo and TVoucherEntry.VoucherTypeCode=" + VchCode + " GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherUserNo, MLedger.LedgerName order by TVoucherEntry.PkVoucherNo";
                    break;
                case "LedgerName":
                    sql = " SELECT TVoucherEntry.PkVoucherNo,TVoucherEntry.VoucherUserNo 'V.No', MLedger.LedgerName, Sum(Case When (TVoucherDetails.Debit>0) then  TVoucherDetails.Debit else TVoucherDetails.Credit " +
                         " END) AS 'Amount' FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo CROSS JOIN TVoucherEntry Where SrNo=" + Others.Party + "" +
                         " And TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo and TVoucherEntry.VoucherTypeCode=" + VchCode + " and " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherUserNo, MLedger.LedgerName order by TVoucherEntry.PkVoucherNo";
                    break;
                case "VoucherDate":
                    sql = " SELECT TVoucherEntry.PkVoucherNo,TVoucherEntry.VoucherUserNo 'V.No', MLedger.LedgerName, Sum(Case When (TVoucherDetails.Debit>0) then  TVoucherDetails.Debit else TVoucherDetails.Credit " +
                         " END) AS 'Amount' FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo CROSS JOIN TVoucherEntry Where SrNo=" + Others.Party + "" +
                         " And TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo and TVoucherEntry.VoucherTypeCode=" + VchCode + " and " + Column + " = '" + Value.Trim().Replace("'", "''") + "' + '' GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherUserNo, MLedger.LedgerName order by TVoucherEntry.PkVoucherNo";
                    break;

            }
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException e)
            {
                strerrormsg = e.Message;
            }
            return ds.Tables[0].DefaultView;
        }

        public DataView GetBySearchVoucher(string Column, string Value, long VchCode)
        {

            string sql = " SELECT TVoucherEntry.PkVoucherNo,TVoucherEntry.VoucherUserNo, MLedger.LedgerName, Sum(Case When (TVoucherDetails.Debit>0) then  TVoucherDetails.Debit else TVoucherDetails.Credit " +
                         " END) AS 'Amount' FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo CROSS JOIN TVoucherEntry Where SrNo=" + Others.Party + "" +
                         " And TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo and TVoucherEntry.VoucherTypeCode=" + VchCode + " and TVoucherDetails.VoucherSrNo=1 and TVoucherEntry.IsVoucherLock='false' GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherUserNo, MLedger.LedgerName order by TVoucherEntry.PkVoucherNo";
            switch (Column)
            {
                case "0":
                    sql = " SELECT TVoucherEntry.PkVoucherNo,TVoucherEntry.VoucherUserNo, MLedger.LedgerName, Sum(Case When (TVoucherDetails.Debit>0) then  TVoucherDetails.Debit else TVoucherDetails.Credit " +
                         " END) AS 'Amount' FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo CROSS JOIN TVoucherEntry Where SrNo=" + Others.Party + "" +
                         " And TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo and TVoucherEntry.IsVoucherLock='false' and TVoucherEntry.VoucherTypeCode=" + VchCode + " and TVoucherDetails.VoucherSrNo=1 GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherUserNo, MLedger.LedgerName order by TVoucherEntry.PkVoucherNo";
                    break;
                case "LedgerName":
                    sql = " SELECT TVoucherEntry.PkVoucherNo,TVoucherEntry.VoucherUserNo, MLedger.LedgerName, Sum(Case When (TVoucherDetails.Debit>0) then  TVoucherDetails.Debit else TVoucherDetails.Credit " +
                         " END) AS 'Amount' FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo CROSS JOIN TVoucherEntry Where SrNo=" + Others.Party + "" +
                         " And TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo and TVoucherEntry.IsVoucherLock='false' and TVoucherDetails.VoucherSrNo=1 and TVoucherEntry.VoucherTypeCode=" + VchCode + " and " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' GROUP BY TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherUserNo, MLedger.LedgerName order by TVoucherEntry.PkVoucherNo";
                    break;

            }
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException e)
            {
                strerrormsg = e.Message;
            }
            return ds.Tables[0].DefaultView;
        }

        public bool UpdateTDeliveryChallan(TDeliveryChallan tdeliverychallan)
        {
            string strQuery = "Update TVoucherEntry set OrderType=" + tdeliverychallan.OrderType + " where PkVoucherNo=" + tdeliverychallan.PkVoucherNo + "";

            if (ObjTrans.Execute(strQuery, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tdeliverychallan.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool AddTDeliveryChallanStock(TDeliveryChallanStock tdeliverychallanstock)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTDeliveryChallanStock";

            cmd.Parameters.AddWithValue("@PkStockTrnNo", tdeliverychallanstock.PkStockTrnNo);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", tstock.FKVoucherNo);

            //cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tstock.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@FkVoucherSrNo", tdeliverychallanstock.FkVoucherSrNo);

            cmd.Parameters.AddWithValue("@GroupNo", tdeliverychallanstock.GroupNo);

            cmd.Parameters.AddWithValue("@ItemNo", tdeliverychallanstock.ItemNo);

            cmd.Parameters.AddWithValue("@TrnCode", tdeliverychallanstock.TrnCode);

            cmd.Parameters.AddWithValue("@Quantity", tdeliverychallanstock.Quantity);

            cmd.Parameters.AddWithValue("@BilledQuantity", tdeliverychallanstock.BilledQuantity);

            cmd.Parameters.AddWithValue("@Rate", tdeliverychallanstock.Rate);

            cmd.Parameters.AddWithValue("@Amount", tdeliverychallanstock.Amount);

            cmd.Parameters.AddWithValue("@NetRate", tdeliverychallanstock.NetRate);

            cmd.Parameters.AddWithValue("@NetAmount", tdeliverychallanstock.NetAmount);

            cmd.Parameters.AddWithValue("@SGSTPercentage", tdeliverychallanstock.SGSTPercentage);

            cmd.Parameters.AddWithValue("@SGSTAmount", tdeliverychallanstock.SGSTAmount);

            cmd.Parameters.AddWithValue("@DiscPercentage", tdeliverychallanstock.DiscPercentage);

            cmd.Parameters.AddWithValue("@DiscAmount", tdeliverychallanstock.DiscAmount);

            cmd.Parameters.AddWithValue("@DiscRupees", tdeliverychallanstock.DiscRupees);

            cmd.Parameters.AddWithValue("@DiscPercentage2", tdeliverychallanstock.DiscPercentage2);

            cmd.Parameters.AddWithValue("@DiscAmount2", tdeliverychallanstock.DiscAmount2);

            cmd.Parameters.AddWithValue("@DiscRupees2", tdeliverychallanstock.DiscRupees2);

            cmd.Parameters.AddWithValue("@FkUomNo", tdeliverychallanstock.FkUomNo);

            cmd.Parameters.AddWithValue("@FkStockBarCodeNo", tdeliverychallanstock.FkStockBarCodeNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", tdeliverychallanstock.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@FkItemTaxInfo", tdeliverychallanstock.FkItemTaxInfo);

            cmd.Parameters.AddWithValue("@FreeQty", tdeliverychallanstock.FreeQty);

            cmd.Parameters.AddWithValue("@FreeUOMNo", tdeliverychallanstock.FreeUOMNo);

            cmd.Parameters.AddWithValue("@UserID", tdeliverychallanstock.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tdeliverychallanstock.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tdeliverychallanstock.CompanyNo);

            cmd.Parameters.AddWithValue("@LandedRate", tdeliverychallanstock.LandedRate);

            cmd.Parameters.AddWithValue("@FkGRNNo", tdeliverychallanstock.FkGRNNo);

            cmd.Parameters.AddWithValue("@BatchNo", (tdeliverychallanstock.BatchNo == null) ? "" : tdeliverychallanstock.BatchNo);

            cmd.Parameters.AddWithValue("@NoOfBag", tdeliverychallanstock.NoOfBag);

            cmd.Parameters.AddWithValue("@CessValue", tdeliverychallanstock.CessValue);

            cmd.Parameters.AddWithValue("@PackagingCharges", tdeliverychallanstock.PackagingCharges);

            cmd.Parameters.AddWithValue("@LBTPerce", tdeliverychallanstock.LBTPerce);

            cmd.Parameters.AddWithValue("@LBTApplicableAmount", tdeliverychallanstock.LBTApplicableAmount);

            cmd.Parameters.AddWithValue("@LBTAmount", tdeliverychallanstock.LBTAmount);

            cmd.Parameters.AddWithValue("@DisplayItemName", tdeliverychallanstock.DisplayItemName);

            cmd.Parameters.AddWithValue("@MfgCompNo", tdeliverychallanstock.MfgCompNo);

            cmd.Parameters.AddWithValue("@TaxPercentageAdnl", tdeliverychallanstock.TaxPercentageAdnl);

            cmd.Parameters.AddWithValue("@TaxAmountAdnl", tdeliverychallanstock.TaxAmountAdnl);

            cmd.Parameters.AddWithValue("@FkItemTaxSettingNoAdnl", tdeliverychallanstock.FkItemTaxSettingNoAdnl);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTDeliveryChallanStock(TDeliveryChallanStock tdeliverychallanstock)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTDeliveryChallanStock";

            cmd.Parameters.AddWithValue("@PkStockTrnNo", tdeliverychallanstock.PkStockTrnNo);

            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTDeliveryChallanStock()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TStock order by PkStockTrnNo";
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

        public DataView GetTDeliveryChallanStockByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TStock where PkStockTrnNo =" + ID;
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

        public TDeliveryChallanStock ModifyTDeliveryChallanStockByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TStock where PkStockTrnNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TDeliveryChallanStock MM = new TDeliveryChallanStock();
                while (dr.Read())
                {
                    MM.PkStockTrnNo = Convert.ToInt32(dr["PkStockTrnNo"]);
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
                    if (!Convert.IsDBNull(dr["TaxPercentage"]))MM.SGSTPercentage = Convert.ToDouble(dr["TaxPercentage"]);
                    if (!Convert.IsDBNull(dr["TaxAmount"]))MM.SGSTAmount = Convert.ToDouble(dr["TaxAmount"]);
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
                    if (!Convert.IsDBNull(dr["FreeQty"])) MM.FreeQty = Convert.ToDouble(dr["FreeQty"]);
                    if (!Convert.IsDBNull(dr["FreeUOMNo"])) MM.FreeUOMNo = Convert.ToInt64(dr["FreeUOMNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["LandedRate"])) MM.LandedRate = Convert.ToDouble(dr["LandedRate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["MfgCompNo"])) MM.MfgCompNo = Convert.ToInt64(dr["MfgCompNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TDeliveryChallanStock();
        }

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            int cntVchNo = -1, cntRef = 0, cntStock = 0, cntRateSettingNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddTDeliveryChallan")
                        {
                            cntVchNo = i;
                        }

                        if (commandcollection[i].CommandText == "AddTDeliveryChallanStock")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
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
                                else if (commandcollection[i].CommandText.IndexOf("@FkVoucherNo") >= 0)
                                {
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                                }
                                else
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddMRateSetting3")
                        {
                            cntRateSettingNo = i;
                        }
                        if (commandcollection[i].CommandText == "StockEffectDC")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
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

        public bool UpdateDeliveryChallan(double BilledAmount, long PkVoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TDeliveryChallan set BilledAmount=@BilledAmount where PKVoucherNo=@PkVoucherNo";

            cmd.Parameters.AddWithValue("@BilledAmount", BilledAmount);
            cmd.Parameters.AddWithValue("@PkVoucherNo", PkVoucherNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateQuery(string strQuery)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strQuery;

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteAllDeliveryChallan(TDeliveryChallan tdeliverychallan)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "DeleteAllVoucherEntry";
            cmd.CommandText = "DeleteAllDeliveryChallan";

            cmd.Parameters.AddWithValue("@VoucherNo", tdeliverychallan.PkVoucherNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tdeliverychallan.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool CancelTDeliveryChallan(TDeliveryChallan tdeliverychallan)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CancelTVoucherEntry";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tdeliverychallan.PkVoucherNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tdeliverychallan.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteReceiptDetails(DateTime FromDate, long PayType)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteReceiptEntry";

            cmd.Parameters.AddWithValue("@FromDate", FromDate);
            cmd.Parameters.AddWithValue("@PayType", PayType);


            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteAllDeliveryChallan(long PKvoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAllVoucherEntry";

            cmd.Parameters.AddWithValue("@VoucherNo", PKvoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteAllDeliveryChallanNew(long PKvoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAllVoucherEntryNew";

            cmd.Parameters.AddWithValue("@VoucherNo", PKvoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool ExecuteNonQueryStatementsCheque()
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

        public void AddRows(long SchDetailsNo, long RewardNo, long RewardDetailsNo)
        {
            DataRow dr = dtId.NewRow();
            dr[0] = SchDetailsNo;
            dr[1] = RewardNo;
            dr[2] = RewardDetailsNo;
            dtId.Rows.Add(dr);
        }

        public int ReturnCommPos(long SchemeDtlsNo)
        {
            for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
            {
                if ((this.commandcollection[i] != null))
                {
                    if (commandcollection[i].CommandText == "AddTRewardDetails")
                    {
                        if (commandcollection[i].Parameters["@SchemeDetailsNo"].Value.ToString() == SchemeDtlsNo.ToString())
                        {
                            return i;
                        }
                    }
                }
            }
            return -1;
        }

        public bool UpdateSchemeAchievers(long pkSrNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update MSchemeAchievers set IsItemDiscStatus='true',StatusNo=2 where PkSrNo=@pkSrNo";

            cmd.Parameters.AddWithValue("@PkSrNo", pkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool EffectStockDC()
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "StockEffectDC";

            commandcollection.Add(cmd);
            return true;
        }

        public bool ReverseStockDC(long ID)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "StockReverseDC";
            cmd.Parameters.AddWithValue("@FkVoucherNo", ID);
            commandcollection.Add(cmd);
            return true;
        }

        private int GetCommandIndex(string ProcName, long MfgCompNo)
        {
            int RwIndex = -1;
            bool flagIndex = true;
            for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
            {
                if ((this.commandcollection[i] != null))
                {
                    if (commandcollection[i].CommandText == ProcName)
                    {
                        for (int paracol = 0; paracol < commandcollection[i].Parameters.Count; paracol++)
                        {
                            if (commandcollection[i].Parameters["@MfgCompNo"].Value.ToString() == MfgCompNo.ToString())
                            {
                                RwIndex = i;
                                flagIndex = false;
                                break;
                            }
                        }
                    }
                    if (flagIndex == false)
                        break;
                }
            }
            return RwIndex;
        }

        //public bool AddTDeleteVouchers(TDeleteVouchers tdeletevouchers)
        //{
        //    SqlCommand cmd;
        //    cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "AddTDeleteVouchers";

        //    cmd.Parameters.AddWithValue("@PkSrNo", tdeletevouchers.PkSrNo);

        //    cmd.Parameters.AddWithValue("@FKVoucherNo", tdeletevouchers.FKVoucherNo);

        //    cmd.Parameters.AddWithValue("@VoucherTypeCode", tdeletevouchers.VoucherTypeCode);

        //    cmd.Parameters.AddWithValue("@CompanyNo", tdeletevouchers.CompanyNo);

        //    commandcollection.Add(cmd);
        //    return true;

        //}

        //public bool DeleteTDeleteVouchers(TDeleteVouchers tdeletevouchers)
        //{
        //    SqlCommand cmd;
        //    cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "DeleteTDeleteVouchers";

        //    cmd.Parameters.AddWithValue("@PkSrNo", tdeletevouchers.PkSrNo);
        //    if (ObjTrans.ExecuteNonQuery(cmd, ConStr) == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        tdeletevouchers.msg = ObjTrans.ErrorMessage;
        //        return false;
        //    }
        //}

        #region Col Index Values
        private static class HeaderColIndex
        {
            public static int VoucherDate = 0;
            public static int VoucherTime = 1;
            public static int LedgerNo = 2;
            public static int DeviceUserNo = 3;
            public static int ChequeNo = 4;
            public static int ChequeDate = 5;
            public static int BilledAmount = 6;
            public static int PayType = 7;
            public static int RateType = 8;
            public static int VoucherStatus = 9;
            public static int DiscPercent = 10;
            public static int DiscAmount = 11;
            public static int Charges1Amt = 12;
            public static int Charges2Amt = 13;
            public static int IsItemLevelDisc = 14;
            public static int IsFooterLevelDisc = 15;
            public static int UserID = 16;
            public static int CompanyNo = 17;
            public static int FkYadiVoucherNo = 18;
            public static int YadiServerNo = 19;
            public static int ServerOtherVoucherNo = 20;
        }

        private static class DataColIndex
        {
            public static int ItemNo = 0;
            public static int Quantity = 1;
            public static int Rate = 2;
            public static int Amount = 3;
            public static int NetRate = 4;
            public static int NetAmount = 5;
            public static int TaxPerce = 6;
            public static int SGSTAmount = 7;
            public static int Disc1Amount = 8;
            public static int Disc2Amount = 9;
            public static int Disc3Amount = 10;
            public static int StatusNo = 11;
            public static int UOM = 12;
            public static int ServerItemNo = 13;
            public static int YadiSrNo = 14;
            public static int MFGCompNo = 15;
            public static int RateSettingNo = 16;
            public static int FkItemTaxSettingNo = 17;
            public static int DsicPer = 18;
            public static int FkStockBarcodeNo = 19;
            public static int StockConversion = 20;
            public static int SaleTaxNo = 21;
            public static int LedgerTaxNo = 22;

        }

        private static class StockDataColIndex
        {
            public static int ItemNo = 0;
            public static int Quantity = 1;
            public static int DamQty = 2;
            public static int Rate = 3;
            public static int BarCode = 4;
        }
        #endregion

        public bool UpdateTransporterDetails(TDeliveryChallan tVoucherEntry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set TransporterCode=@TransporterCode,LRNo=@LRNo,StatusNo=2 where PkVoucherNo=@PkVoucherNo";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tVoucherEntry.PkVoucherNo);

            cmd.Parameters.AddWithValue("@TransporterCode", tVoucherEntry.TransporterCode);

            cmd.Parameters.AddWithValue("@LRNo", tVoucherEntry.LRNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateTransporterStatus(TDeliveryChallan tdeliverychallan)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set TransPayType=@TransPayType ,StatusNo=2 where PkVoucherNo=@PkVoucherNo";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tdeliverychallan.PkVoucherNo);

            cmd.Parameters.AddWithValue("@TransPayType", tdeliverychallan.TransPayType);
            commandcollection.Add(cmd);
            return true;
        }

        public bool SaveTransporterDetails(TDeliveryChallan tdeliverychallan)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set TransporterCode=@TransporterCode,TransNoOfItems=@TransNoOfItems,TransportMode=@TransportMode,LRNo=@LRNo,TransPayType=@TransPayType,StatusNo=2 where PkVoucherNo=@PkVoucherNo";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tdeliverychallan.PkVoucherNo);

            cmd.Parameters.AddWithValue("@TransporterCode", tdeliverychallan.TransporterCode);

            cmd.Parameters.AddWithValue("@LRNo", tdeliverychallan.LRNo);

            cmd.Parameters.AddWithValue("@TransportMode", tdeliverychallan.TransportMode);

            cmd.Parameters.AddWithValue("@TransNoOfItems", tdeliverychallan.TransNoOfItems);

            cmd.Parameters.AddWithValue("@TransPayType", tdeliverychallan.TransPayType);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tdeliverychallan.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool UpdateVoucherRefDetails(long VoucherNo, double DiscAmt, int typeCrDr)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set BilledAmount=BilledAmount-@DiscAmt Where PKVoucherNo=@PkVoucherNo;" +
                " Update TVoucherDetails set " + ((typeCrDr == 1) ? "Debit=Debit-" : "Credit=Credit-") + "@DiscAmt Where FKVoucherNo=@PkVoucherNo AND SrNo=" + Others.Party + "; " +
                " Update TVoucherDetails set " + ((typeCrDr == 1) ? "Credit=Credit-" : "Debit=Debit-") + "@DiscAmt Where FKVoucherNo=@PkVoucherNo AND SrNo=" + Others.Discount1 + "; ";

            cmd.Parameters.AddWithValue("@PkVoucherNo", VoucherNo);

            cmd.Parameters.AddWithValue("@DiscAmt", DiscAmt);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateVoucherRefDetails_Interest(long VoucherNo, double interestAmt, int typeCrDr)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set BilledAmount=BilledAmount-@InterestAmt Where PKVoucherNo=@PkVoucherNo;" +
                " Update TVoucherDetails set " + ((typeCrDr == 1) ? "Debit=Debit-" : "Credit=Credit-") + "@InterestAmt Where FKVoucherNo=@PkVoucherNo AND SrNo=" + Others.Party + "; " +
                " Update TVoucherDetails set " + ((typeCrDr == 1) ? "Credit=Credit-" : "Debit=Debit-") + "@InterestAmt Where FKVoucherNo=@PkVoucherNo AND SrNo=" + 0 + "; ";

            cmd.Parameters.AddWithValue("@PkVoucherNo", VoucherNo);

            cmd.Parameters.AddWithValue("@InterestAmt", interestAmt);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTOtherStockDetails(TOtherStockDetails totherstockdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTOtherStockDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", totherstockdetails.PkSrNo);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", totherstockdetails.FKVoucherNo);

            //cmd.Parameters.AddWithValue("@FKStockTrnNo", totherstockdetails.FKStockTrnNo);

            cmd.Parameters.AddWithValue("@FKOtherVoucherNo", totherstockdetails.FKOtherVoucherNo);

            cmd.Parameters.AddWithValue("@FKOtherStockTrnNo", totherstockdetails.FKOtherStockTrnNo);

            cmd.Parameters.AddWithValue("@ItemNo", totherstockdetails.ItemNo);

            cmd.Parameters.AddWithValue("@Quantity", totherstockdetails.Quantity);

            cmd.Parameters.AddWithValue("@UserID", totherstockdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", totherstockdetails.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", totherstockdetails.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

    }

    /// <summary>
    /// This Class use for TVoucherEntry
    /// </summary>
    public class TDeliveryChallan
    {
        private long mPkVoucherNo;
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
        private double mExcisePercentage;
        private bool mIsExciseBill;
        private int mIsBillMulti;
        private long mTransporterCode;
        private long mTransPayType;
        private string mLRNo;
        private long mTransportMode;
        private double mTransNoOfItems;
        private double mChrgesTaxPerce;
        private long mMfgCompNo;
        private long mLedgerNo;
        private string Mmsg;
        private double mChargesAmt1;
        private long mFKVoucherNo;

        /// <summary>
        /// This Properties use for PkVoucherNo
        /// </summary>
        public long PkVoucherNo
        {
            get { return mPkVoucherNo; }
            set { mPkVoucherNo = value; }
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
        public long VoucherUserNo
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
        public long TaxTypeNo
        {
            get { return mTaxTypeNo; }
            set { mTaxTypeNo = value; }
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
        public double ExcisePercentage
        {
            get { return mExcisePercentage; }
            set { mExcisePercentage = value; }
        }
        public bool IsExciseBill
        {
            get { return mIsExciseBill; }
            set { mIsExciseBill = value; }
        }
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
        public long MfgCompNo
        {
            get { return mMfgCompNo; }
            set { mMfgCompNo = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public double ChargesAmt1
        {
            get { return mChargesAmt1; }
            set { mChargesAmt1 = value; }
        }
        public long FKVoucherNo
        {
            get { return mFKVoucherNo; }
            set { mFKVoucherNo = value; }
        }
    }

    /// <summary>
    /// This Class use for TStock
    /// </summary>
    public class TDeliveryChallanStock
    {
        private long mPkStockTrnNo;
        private long mFKVoucherNo;
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
        private double mLBTPerce;
        private double mLBTApplicableAmount;
        private double mLBTAmount;
        private long mMfgCompNo;
        private string mDisplayItemName;
        private double mTaxPercentageAdnl;
        private double mTaxAmountAdnl;
        private long mFkItemTaxSettingNoAdnl;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkStockTrnNo
        /// </summary>
        public long PkStockTrnNo
        {
            get { return mPkStockTrnNo; }
            set { mPkStockTrnNo = value; }
        }
        /// <summary>
        /// This Properties use for FKVoucherNo
        /// </summary>
        public long FKVoucherNo
        {
            get { return mFKVoucherNo; }
            set { mFKVoucherNo = value; }
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
        public double LBTPerce
        {
            get { return mLBTPerce; }
            set { mLBTPerce = value; }
        }
        public double LBTApplicableAmount
        {
            get { return mLBTApplicableAmount; }
            set { mLBTApplicableAmount = value; }
        }
        public double LBTAmount
        {
            get { return mLBTAmount; }
            set { mLBTAmount = value; }
        }

        public long MfgCompNo
        {
            get { return mMfgCompNo; }
            set { mMfgCompNo = value; }
        }

        public string DisplayItemName
        {
            get { return mDisplayItemName; }
            set { mDisplayItemName = value; }
        }
        public double TaxPercentageAdnl
        {
            get { return mTaxPercentageAdnl; }
            set { mTaxPercentageAdnl = value; }
        }
        /// <summary>
        /// This Properties use for TaxAmount
        /// </summary>
        public double TaxAmountAdnl
        {
            get { return mTaxAmountAdnl; }
            set { mTaxAmountAdnl = value; }
        }
        public long FkItemTaxSettingNoAdnl
        {
            get { return mFkItemTaxSettingNoAdnl; }
            set { mFkItemTaxSettingNoAdnl = value; }
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
