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
    public class DBTVaucherEntry
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        public CommandCollection commandcollection = new CommandCollection();
        public static DataTable dtCompRatio = new DataTable();

        TVoucherEntryCompany tVoucherEntryComp = new TVoucherEntryCompany();
        TVoucherDetailsCompany tVoucherDetailsComp = new TVoucherDetailsCompany();
        TVoucherEntry tvoucherenetry = new TVoucherEntry();

        DataTable dtId = new DataTable();
        DataTable dtZeroTax = new DataTable();

        //SaveBill 

        DataTable dtHeader = new DataTable();
        DataTable dtDetails = new DataTable();
        public static int HeaderLen = 21, DataLen = 23, StockDataLen = 5;
        long CompNo, PartyLedgerNo = 0;
        long VType;
        double SubTotalAmt = 0, TotalTaxAmt = 0, TotalDiscAmt = 0;

        public static string strerrormsg;

        public bool AddTVoucherEntry(TVoucherEntry tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherEntry";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tvoucherentry.PkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherTypeCode", tvoucherentry.VoucherTypeCode);

            cmd.Parameters.AddWithValue("@VoucherUserNo", tvoucherentry.VoucherUserNo);

            cmd.Parameters.AddWithValue("@VoucherDate", tvoucherentry.VoucherDate);

            cmd.Parameters.AddWithValue("@VoucherTime", tvoucherentry.VoucherTime);

            cmd.Parameters.AddWithValue("@Narration", tvoucherentry.Narration);

            cmd.Parameters.AddWithValue("@Reference", tvoucherentry.Reference);

            cmd.Parameters.AddWithValue("@ChequeNo", tvoucherentry.ChequeNo);

            cmd.Parameters.AddWithValue("@ClearingDate", tvoucherentry.ClearingDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherentry.CompanyNo);

            cmd.Parameters.AddWithValue("@BilledAmount", tvoucherentry.BilledAmount);

            cmd.Parameters.AddWithValue("@ChallanNo", tvoucherentry.ChallanNo);

            cmd.Parameters.AddWithValue("@Remark", tvoucherentry.Remark);

            cmd.Parameters.AddWithValue("@InwardLocationCode", tvoucherentry.InwardLocationCode);

            cmd.Parameters.AddWithValue("@MacNo", tvoucherentry.MacNo);

            cmd.Parameters.AddWithValue("@PayTypeNo", tvoucherentry.PayTypeNo);

            cmd.Parameters.AddWithValue("@RateTypeNo", tvoucherentry.RateTypeNo);

            cmd.Parameters.AddWithValue("@TaxTypeNo", tvoucherentry.TaxTypeNo);

            cmd.Parameters.AddWithValue("@TaxInvoiceTypeNo", tvoucherentry.TaxInvoiceTypeNo);

            cmd.Parameters.AddWithValue("@OrderType", tvoucherentry.OrderType);

            cmd.Parameters.AddWithValue("@ReturnAmount", tvoucherentry.ReturnAmount);

            cmd.Parameters.AddWithValue("@Visibility", tvoucherentry.Visibility);

            cmd.Parameters.AddWithValue("@DiscPercent", tvoucherentry.DiscPercent);

            cmd.Parameters.AddWithValue("@DiscAmt", tvoucherentry.DiscAmt);

            cmd.Parameters.AddWithValue("@MixMode", tvoucherentry.MixMode);

            cmd.Parameters.AddWithValue("@IsItemLevelDisc", tvoucherentry.IsItemLevelDisc);

            cmd.Parameters.AddWithValue("@IsFooterLevelDisc", tvoucherentry.IsFooterLevelDisc);

            cmd.Parameters.AddWithValue("@UserID", tvoucherentry.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tvoucherentry.UserDate);

            cmd.Parameters.AddWithValue("@BrokerNo", tvoucherentry.BrokerNo);

            cmd.Parameters.AddWithValue("@SuppCategory", tvoucherentry.SuppCategory);

            cmd.Parameters.AddWithValue("@EffectiveDate", (tvoucherentry.EffectiveDate == Convert.ToDateTime("01-Jan-0001")) ? Convert.ToDateTime("01-Jan-1900") : tvoucherentry.EffectiveDate);

            //cmd.Parameters.AddWithValue("@ExcisePercentage", tvoucherentry.ExcisePercentage);

            //cmd.Parameters.AddWithValue("@IsExciseBill", tvoucherentry.IsExciseBill);

            cmd.Parameters.AddWithValue("@IsBillMulti", tvoucherentry.IsBillMulti);

            cmd.Parameters.AddWithValue("@TransporterCode", tvoucherentry.TransporterCode);

            cmd.Parameters.AddWithValue("@TransPayType", tvoucherentry.TransPayType);

            cmd.Parameters.AddWithValue("@LRNo", tvoucherentry.LRNo);

            cmd.Parameters.AddWithValue("@TransportMode", tvoucherentry.TransportMode);

            cmd.Parameters.AddWithValue("@TransNoOfItems", tvoucherentry.TransNoOfItems);

            cmd.Parameters.AddWithValue("@ChrgesTaxPerce", tvoucherentry.ChrgesTaxPerce);

            cmd.Parameters.AddWithValue("@StateCode", tvoucherentry.StateCode);

            cmd.Parameters.AddWithValue("@TaxAmount", tvoucherentry.TaxAmount);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherentry.LedgerNo);
            cmd.Parameters.AddWithValue("@PkRefNo", tvoucherenetry.PkRefNo);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTVoucherEntryES(TVoucherEntry tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherEntryES";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tvoucherentry.PkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherTypeCode", tvoucherentry.VoucherTypeCode);

            cmd.Parameters.AddWithValue("@VoucherUserNo", tvoucherentry.VoucherUserNo);

            cmd.Parameters.AddWithValue("@VoucherDate", tvoucherentry.VoucherDate);

            cmd.Parameters.AddWithValue("@VoucherTime", tvoucherentry.VoucherTime);

            cmd.Parameters.AddWithValue("@Narration", tvoucherentry.Narration);

            cmd.Parameters.AddWithValue("@Reference", tvoucherentry.Reference);

            cmd.Parameters.AddWithValue("@ChequeNo", tvoucherentry.ChequeNo);

            cmd.Parameters.AddWithValue("@ClearingDate", tvoucherentry.ClearingDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherentry.CompanyNo);

            cmd.Parameters.AddWithValue("@BilledAmount", tvoucherentry.BilledAmount);

            cmd.Parameters.AddWithValue("@ChallanNo", tvoucherentry.ChallanNo);

            cmd.Parameters.AddWithValue("@Remark", tvoucherentry.Remark);

            cmd.Parameters.AddWithValue("@InwardLocationCode", tvoucherentry.InwardLocationCode);

            cmd.Parameters.AddWithValue("@MacNo", tvoucherentry.MacNo);

            cmd.Parameters.AddWithValue("@PayTypeNo", tvoucherentry.PayTypeNo);

            cmd.Parameters.AddWithValue("@RateTypeNo", tvoucherentry.RateTypeNo);

            cmd.Parameters.AddWithValue("@TaxTypeNo", tvoucherentry.TaxTypeNo);

            cmd.Parameters.AddWithValue("@TaxInvoiceTypeNo", tvoucherentry.TaxInvoiceTypeNo);

            cmd.Parameters.AddWithValue("@OrderType", tvoucherentry.OrderType);

            cmd.Parameters.AddWithValue("@ReturnAmount", tvoucherentry.ReturnAmount);

            cmd.Parameters.AddWithValue("@Visibility", tvoucherentry.Visibility);

            cmd.Parameters.AddWithValue("@DiscPercent", tvoucherentry.DiscPercent);

            cmd.Parameters.AddWithValue("@DiscAmt", tvoucherentry.DiscAmt);

            cmd.Parameters.AddWithValue("@MixMode", tvoucherentry.MixMode);

            cmd.Parameters.AddWithValue("@IsItemLevelDisc", tvoucherentry.IsItemLevelDisc);

            cmd.Parameters.AddWithValue("@IsFooterLevelDisc", tvoucherentry.IsFooterLevelDisc);

            cmd.Parameters.AddWithValue("@UserID", tvoucherentry.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tvoucherentry.UserDate);

            cmd.Parameters.AddWithValue("@BrokerNo", tvoucherentry.BrokerNo);

            cmd.Parameters.AddWithValue("@SuppCategory", tvoucherentry.SuppCategory);

            cmd.Parameters.AddWithValue("@EffectiveDate", (tvoucherentry.EffectiveDate == Convert.ToDateTime("01-Jan-0001")) ? Convert.ToDateTime("01-Jan-1900") : tvoucherentry.EffectiveDate);

            //cmd.Parameters.AddWithValue("@ExcisePercentage", tvoucherentry.ExcisePercentage);

            //cmd.Parameters.AddWithValue("@IsExciseBill", tvoucherentry.IsExciseBill);

            cmd.Parameters.AddWithValue("@IsBillMulti", tvoucherentry.IsBillMulti);

            cmd.Parameters.AddWithValue("@TransporterCode", tvoucherentry.TransporterCode);

            cmd.Parameters.AddWithValue("@TransPayType", tvoucherentry.TransPayType);

            cmd.Parameters.AddWithValue("@LRNo", tvoucherentry.LRNo);

            cmd.Parameters.AddWithValue("@TransportMode", tvoucherentry.TransportMode);

            cmd.Parameters.AddWithValue("@TransNoOfItems", tvoucherentry.TransNoOfItems);

            cmd.Parameters.AddWithValue("@ChrgesTaxPerce", tvoucherentry.ChrgesTaxPerce);

            cmd.Parameters.AddWithValue("@StateCode", tvoucherentry.StateCode);

            cmd.Parameters.AddWithValue("@TaxAmount", tvoucherentry.TaxAmount);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherentry.LedgerNo);
            //cmd.Parameters.AddWithValue("@PkRefNo", tvoucherenetry.PkRefNo);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherEntry(TVoucherEntry tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherEntry";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tvoucherentry.PkVoucherNo);
            commandcollection.Add(cmd);
            return true;
        }

        public string FillGridSales(long StateType, long Firmtype, out string str)
        {
            str = "";
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetFillGridSales";

            cmd.Parameters.AddWithValue("@StateType", StateType);
            cmd.Parameters.AddWithValue("@Firmtype", Firmtype);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.String;
            cmd.Parameters.Add(outParameter);
            // commandcollection.Add(cmd);


            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return str = (string)cmd.Parameters["@ReturnID"].Value;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteTVoucherEntry1(TVoucherEntry tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherEntry1";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tvoucherentry.PkVoucherNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherEntryCompany1(TVoucherEntryCompany tvoucherentrycompany)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherEntryCompany1";

            cmd.Parameters.AddWithValue("@FkVoucherNo", tvoucherentrycompany.FkVoucherNo);
            cmd.Parameters.AddWithValue("@MfgCompNo", tvoucherentrycompany.MfgCompNo);
            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTVoucherEntry()
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

        public DataView GetTVoucherEntryByID(long ID)
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

        public TVoucherEntry ModifyTVoucherEntryByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TVoucherEntry where PkVoucherNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TVoucherEntry MM = new TVoucherEntry();
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
                    if (!Convert.IsDBNull(dr["TaxInvoiceTypeNo"])) MM.TaxInvoiceTypeNo = Convert.ToInt64(dr["TaxInvoiceTypeNo"]);
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
                    //if (!Convert.IsDBNull(dr["ExcisePercentage"])) MM.ExcisePercentage = Convert.ToDouble(dr["ExcisePercentage"]);
                    //if (!Convert.IsDBNull(dr["IsExciseBill"])) MM.IsExciseBill = Convert.ToBoolean(dr["IsExciseBill"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["TransporterCode"])) MM.TransporterCode = Convert.ToInt64(dr["TransporterCode"]);
                    if (!Convert.IsDBNull(dr["TransPayType"])) MM.TransPayType = Convert.ToInt64(dr["TransPayType"]);
                    if (!Convert.IsDBNull(dr["LRNo"])) MM.LRNo = Convert.ToString(dr["LRNo"]);
                    if (!Convert.IsDBNull(dr["TransportMode"])) MM.TransportMode = Convert.ToInt64(dr["TransportMode"]);
                    if (!Convert.IsDBNull(dr["TransNoOfItems"])) MM.TransNoOfItems = Convert.ToDouble(dr["TransNoOfItems"]);
                    if (!Convert.IsDBNull(dr["ChrgesTaxPerce"])) MM.ChrgesTaxPerce = Convert.ToDouble(dr["ChrgesTaxPerce"]);
                    if (!Convert.IsDBNull(dr["IsTaxFree"])) MM.IsTaxFree = Convert.ToBoolean(dr["IsTaxFree"]);
                    if (!Convert.IsDBNull(dr["PkRefNo"])) MM.PkRefNo = Convert.ToInt64(dr["PkRefNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["StateCode"])) MM.StateCode = Convert.ToInt64(dr["StateCode"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TVoucherEntry();
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

        public bool UpdateTVoucherEntry(TVoucherEntry tvoucherentry)
        {
            string strQuery = "Update TVoucherEntry set OrderType=" + tvoucherentry.OrderType + " where PkVoucherNo=" + tvoucherentry.PkVoucherNo + "";

            if (ObjTrans.Execute(strQuery, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tvoucherentry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool AddTVoucherDetails(TVoucherDetails tvoucherdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherDetails";

            cmd.Parameters.AddWithValue("@PkVoucherTrnNo", tvoucherdetails.PkVoucherTrnNo);

            //cmd.Parameters.AddWithValue("@FkVoucherNo", tvoucherdetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherSrNo", tvoucherdetails.VoucherSrNo);

            cmd.Parameters.AddWithValue("@SignCode", tvoucherdetails.SignCode);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@Debit", tvoucherdetails.Debit);

            cmd.Parameters.AddWithValue("@Credit", tvoucherdetails.Credit);

            cmd.Parameters.AddWithValue("@SrNo", tvoucherdetails.SrNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@Narration", tvoucherdetails.Narration);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }
        public bool AddTDeliveryAddress(TDeliveryAddress tdeliveryaddress)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTDeliveryAddress";

            cmd.Parameters.AddWithValue("@PkSrNo", tdeliveryaddress.PkSrNo);

            //cmd.Parameters.AddWithValue("@FkVoucherNo", tdeliveryaddress.FkVoucherNo);

            cmd.Parameters.AddWithValue("@Ledgerno", tdeliveryaddress.Ledgerno);

            cmd.Parameters.AddWithValue("@LedgerDetailsNo", tdeliveryaddress.LedgerDetailsNo);

            cmd.Parameters.AddWithValue("@UserId", tdeliveryaddress.UserId);

            cmd.Parameters.AddWithValue("@UserDate", tdeliveryaddress.UserDate);

            cmd.Parameters.AddWithValue("@IsActive", tdeliveryaddress.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", tdeliveryaddress.CompanyNo);


            // SqlParameter outParameter = new SqlParameter();
            //// outParameter.ParameterName = "@ReturnID";
            // outParameter.Direction = ParameterDirection.Output;
            // outParameter.DbType = DbType.Int32;
            // cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }
        public bool AddTVoucherDetailsES(TVoucherDetails tvoucherdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherDetailsES";

            cmd.Parameters.AddWithValue("@PkVoucherTrnNo", tvoucherdetails.PkVoucherTrnNo);

            //cmd.Parameters.AddWithValue("@FkVoucherNo", tvoucherdetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherSrNo", tvoucherdetails.VoucherSrNo);

            cmd.Parameters.AddWithValue("@SignCode", tvoucherdetails.SignCode);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@Debit", tvoucherdetails.Debit);

            cmd.Parameters.AddWithValue("@Credit", tvoucherdetails.Credit);

            cmd.Parameters.AddWithValue("@SrNo", tvoucherdetails.SrNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@Narration", tvoucherdetails.Narration);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }
        public bool DeleteTVoucherDetails(TVoucherDetails tvoucherdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherDetails";

            cmd.Parameters.AddWithValue("@PkVoucherTrnNo", tvoucherdetails.PkVoucherTrnNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherDetailsCompany(TVoucherDetails tvoucherdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherDetailsCompany";

            cmd.Parameters.AddWithValue("@FkVoucherNo", tvoucherdetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherdetails.CompanyNo);
            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTVoucherDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherDetails order by PkVoucherTrnNo";
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

        public DataView GetTVoucherDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select TVoucherDetails.SignCode,TVoucherDetails.LedgerNo,TVoucherDetails.Debit,TVoucherDetails.Credit,TVoucherDetails.PkVoucherTrnNo,TVoucherDetails.CompanyNo,MCompany.CompanyName from TVoucherDetails ,MCompany Where TVoucherDetails.CompanyNo=MCompany.CompanyNo AND FkVoucherNo =" + ID;
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

        public DataView GetTVoucherDetailsVoucherByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = " SELECT   0 as SrNo,  MSign.ShortName, MLedger.LedgerName, TVoucherDetails.Debit, TVoucherDetails.Credit, TVoucherDetails.PkVoucherTrnNo,  " +
                         "  MSign.SignCode, MLedger.LedgerNo, TChequeNoDetails.PkSrNo,TVoucherDetails.Narration , TVoucherChqCreditDetails.ChequeNo, " +
                         " TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.BankNo, TVoucherChqCreditDetails.BranchNo, " +
                         " TVoucherChqCreditDetails.PkSrNo FROM  MSign INNER JOIN " +
                         " TVoucherDetails ON MSign.SignCode = TVoucherDetails.SignCode INNER JOIN " +
                         " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                         " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN " +
                         " TChequeNoDetails ON TVoucherDetails.PkVoucherTrnNo = TChequeNoDetails.FkVoucherTrnNo LEFT OUTER JOIN " +
                         " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo " +
                         " where TVoucherEntry.IsVoucherLock='false' and TVoucherDetails.FkVoucherNo =" + ID;
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


        public TVoucherDetails ModifyTVoucherDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TVoucherDetails where PkVoucherTrnNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TVoucherDetails MM = new TVoucherDetails();
                while (dr.Read())
                {
                    MM.PkVoucherTrnNo = Convert.ToInt32(dr["PkVoucherTrnNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt64(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["VoucherSrNo"])) MM.VoucherSrNo = Convert.ToInt64(dr["VoucherSrNo"]);
                    if (!Convert.IsDBNull(dr["SignCode"])) MM.SignCode = Convert.ToInt64(dr["SignCode"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["Debit"])) MM.Debit = Convert.ToDouble(dr["Debit"]);
                    if (!Convert.IsDBNull(dr["Credit"])) MM.Credit = Convert.ToDouble(dr["Credit"]);
                    if (!Convert.IsDBNull(dr["SrNo"])) MM.SrNo = Convert.ToInt64(dr["SrNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["Narration"])) MM.Narration = Convert.ToString(dr["Narration"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TVoucherDetails();
        }


        public bool AddTStock(TStock tstock)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTStock";

            cmd.Parameters.AddWithValue("@PkStockTrnNo", tstock.PkStockTrnNo);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", tstock.FKVoucherNo);

            //cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tstock.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@FkVoucherSrNo", tstock.FkVoucherSrNo);

            cmd.Parameters.AddWithValue("@GroupNo", tstock.GroupNo);

            cmd.Parameters.AddWithValue("@ItemNo", tstock.ItemNo);

            cmd.Parameters.AddWithValue("@TrnCode", tstock.TrnCode);

            cmd.Parameters.AddWithValue("@Quantity", tstock.Quantity);

            cmd.Parameters.AddWithValue("@BilledQuantity", tstock.BilledQuantity);

            cmd.Parameters.AddWithValue("@Rate", tstock.Rate);

            cmd.Parameters.AddWithValue("@Amount", tstock.Amount);

            cmd.Parameters.AddWithValue("@NetRate", tstock.NetRate);

            cmd.Parameters.AddWithValue("@NetAmount", tstock.NetAmount);

            cmd.Parameters.AddWithValue("@SGSTPercentage", tstock.SGSTPercentage);

            cmd.Parameters.AddWithValue("@SGSTAmount", tstock.SGSTAmount);

            cmd.Parameters.AddWithValue("@AddTaxPercentage", tstock.AddTaxPercentage);

            cmd.Parameters.AddWithValue("@AddTaxAmount", tstock.AddTaxAmount);

            cmd.Parameters.AddWithValue("@DiscPercentage", tstock.DiscPercentage);

            cmd.Parameters.AddWithValue("@DiscAmount", tstock.DiscAmount);

            cmd.Parameters.AddWithValue("@DiscRupees", tstock.DiscRupees);

            cmd.Parameters.AddWithValue("@DiscPercentage2", tstock.DiscPercentage2);

            cmd.Parameters.AddWithValue("@DiscAmount2", tstock.DiscAmount2);

            cmd.Parameters.AddWithValue("@DiscRupees2", tstock.DiscRupees2);

            cmd.Parameters.AddWithValue("@FkUomNo", tstock.FkUomNo);

            cmd.Parameters.AddWithValue("@FkStockBarCodeNo", tstock.FkStockBarCodeNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", tstock.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@FkItemTaxInfo", tstock.FkItemTaxInfo);

            cmd.Parameters.AddWithValue("@FKAddItemTaxSettingNo", tstock.FKAddItemTaxSettingNo);

            cmd.Parameters.AddWithValue("@FreeQty", tstock.FreeQty);

            cmd.Parameters.AddWithValue("@FreeUOMNo", tstock.FreeUOMNo);

            cmd.Parameters.AddWithValue("@UserID", tstock.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tstock.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tstock.CompanyNo);

            cmd.Parameters.AddWithValue("@LandedRate", tstock.LandedRate);

            cmd.Parameters.AddWithValue("@FkGRNNo", tstock.FkGRNNo);

            cmd.Parameters.AddWithValue("@BatchNo", (tstock.BatchNo == null) ? "" : tstock.BatchNo);

            cmd.Parameters.AddWithValue("@NoOfBag", tstock.NoOfBag);

            cmd.Parameters.AddWithValue("@CessValue", tstock.CessValue);

            //
            cmd.Parameters.AddWithValue("@PackagingCharges", tstock.PackagingCharges);

            //cmd.Parameters.AddWithValue("@LBTPerce", tstock.LBTPerce);

            //cmd.Parameters.AddWithValue("@LBTApplicableAmount", tstock.LBTApplicableAmount);

            //cmd.Parameters.AddWithValue("@LBTAmount", tstock.LBTAmount);

            cmd.Parameters.AddWithValue("@DisplayItemName", tstock.DisplayItemName);

            //cmd.Parameters.AddWithValue("@MfgCompNo", tstock.MfgCompNo);

            cmd.Parameters.AddWithValue("@CGSTAmount", tstock.CGSTAmount);
            cmd.Parameters.AddWithValue("@CGSTPercentage", tstock.CGSTPercentage);
            cmd.Parameters.AddWithValue("@IGSTAmount", tstock.IGSTAmount);
            cmd.Parameters.AddWithValue("@IGSTPercentage", tstock.IGSTPercentage);
            cmd.Parameters.AddWithValue("@CessAmount", tstock.CessAmount);
            cmd.Parameters.AddWithValue("@CessPercentage", tstock.CessPercentage);

            cmd.Parameters.AddWithValue("@FkItemTaxInfo2", tstock.FkItemTaxInfo2);
            cmd.Parameters.AddWithValue("@TRWeight", tstock.TRWeight);
            cmd.Parameters.AddWithValue("@GRWeight", tstock.GRWeight);
            cmd.Parameters.AddWithValue("@Remarks", tstock.Remarks);
            cmd.Parameters.AddWithValue("@Freight", tstock.Freight);
            cmd.Parameters.AddWithValue("@OtherCharges", tstock.OtherCharges);
            cmd.Parameters.AddWithValue("@SalesMan", tstock.SalesMan);
            cmd.Parameters.AddWithValue("@IType", tstock.IType);
            cmd.Parameters.AddWithValue("@ContainerCharges", tstock.ContainerCharges);
            cmd.Parameters.AddWithValue("@ContainerChargesAmt", tstock.ContainerChargesAmt);
            cmd.Parameters.AddWithValue("@PackagingChargesAmt", tstock.PackagingChargesAmt);
            cmd.Parameters.AddWithValue("@Hamali", tstock.Hamali);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTStockES(TStock tstock)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTStockES";

            cmd.Parameters.AddWithValue("@PkStockTrnNo", tstock.PkStockTrnNo);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", tstock.FKVoucherNo);

            //cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tstock.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@FkVoucherSrNo", tstock.FkVoucherSrNo);

            cmd.Parameters.AddWithValue("@GroupNo", tstock.GroupNo);

            cmd.Parameters.AddWithValue("@ItemNo", tstock.ItemNo);

            cmd.Parameters.AddWithValue("@TrnCode", tstock.TrnCode);

            cmd.Parameters.AddWithValue("@Quantity", tstock.Quantity);

            cmd.Parameters.AddWithValue("@BilledQuantity", tstock.BilledQuantity);

            cmd.Parameters.AddWithValue("@Rate", tstock.Rate);

            cmd.Parameters.AddWithValue("@Amount", tstock.Amount);

            cmd.Parameters.AddWithValue("@NetRate", tstock.NetRate);

            cmd.Parameters.AddWithValue("@NetAmount", tstock.NetAmount);

            cmd.Parameters.AddWithValue("@SGSTPercentage", tstock.SGSTPercentage);

            cmd.Parameters.AddWithValue("@SGSTAmount", tstock.SGSTAmount);

            cmd.Parameters.AddWithValue("@AddTaxPercentage", tstock.AddTaxPercentage);

            cmd.Parameters.AddWithValue("@AddTaxAmount", tstock.AddTaxAmount);

            cmd.Parameters.AddWithValue("@DiscPercentage", tstock.DiscPercentage);

            cmd.Parameters.AddWithValue("@DiscAmount", tstock.DiscAmount);

            cmd.Parameters.AddWithValue("@DiscRupees", tstock.DiscRupees);

            cmd.Parameters.AddWithValue("@DiscPercentage2", tstock.DiscPercentage2);

            cmd.Parameters.AddWithValue("@DiscAmount2", tstock.DiscAmount2);

            cmd.Parameters.AddWithValue("@DiscRupees2", tstock.DiscRupees2);

            cmd.Parameters.AddWithValue("@FkUomNo", tstock.FkUomNo);

            cmd.Parameters.AddWithValue("@FkStockBarCodeNo", tstock.FkStockBarCodeNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", tstock.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@FkItemTaxInfo", tstock.FkItemTaxInfo);

            cmd.Parameters.AddWithValue("@FKAddItemTaxSettingNo", tstock.FKAddItemTaxSettingNo);

            cmd.Parameters.AddWithValue("@FreeQty", tstock.FreeQty);

            cmd.Parameters.AddWithValue("@FreeUOMNo", tstock.FreeUOMNo);

            cmd.Parameters.AddWithValue("@UserID", tstock.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tstock.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tstock.CompanyNo);

            cmd.Parameters.AddWithValue("@LandedRate", tstock.LandedRate);

            cmd.Parameters.AddWithValue("@FkGRNNo", tstock.FkGRNNo);

            cmd.Parameters.AddWithValue("@BatchNo", (tstock.BatchNo == null) ? "" : tstock.BatchNo);

            cmd.Parameters.AddWithValue("@NoOfBag", tstock.NoOfBag);

            cmd.Parameters.AddWithValue("@CessValue", tstock.CessValue);

            cmd.Parameters.AddWithValue("@PackagingCharges", tstock.PackagingCharges);

            //cmd.Parameters.AddWithValue("@LBTPerce", tstock.LBTPerce);

            //cmd.Parameters.AddWithValue("@LBTApplicableAmount", tstock.LBTApplicableAmount);

            //cmd.Parameters.AddWithValue("@LBTAmount", tstock.LBTAmount);

            cmd.Parameters.AddWithValue("@DisplayItemName", tstock.DisplayItemName);

            //  cmd.Parameters.AddWithValue("@MfgCompNo", tstock.MfgCompNo);

            cmd.Parameters.AddWithValue("@CGSTAmount", tstock.CGSTAmount);
            cmd.Parameters.AddWithValue("@CGSTPercentage", tstock.CGSTPercentage);
            cmd.Parameters.AddWithValue("@IGSTAmount", tstock.IGSTAmount);
            cmd.Parameters.AddWithValue("@IGSTPercentage", tstock.IGSTPercentage);
            cmd.Parameters.AddWithValue("@CessAmount", tstock.CessAmount);
            cmd.Parameters.AddWithValue("@CessPercentage", tstock.CessPercentage);

            cmd.Parameters.AddWithValue("@FkItemTaxInfo2", tstock.FkItemTaxInfo2);
            cmd.Parameters.AddWithValue("@TRWeight", tstock.TRWeight);
            cmd.Parameters.AddWithValue("@GRWeight", tstock.GRWeight);
            cmd.Parameters.AddWithValue("@Remarks", tstock.Remarks);
            cmd.Parameters.AddWithValue("@Freight", tstock.Freight);
            cmd.Parameters.AddWithValue("@OtherCharges", tstock.OtherCharges);
            cmd.Parameters.AddWithValue("@SalesMan", tstock.SalesMan);
            cmd.Parameters.AddWithValue("@IType", tstock.IType);
            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTStock(TStock tstock)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTStock";

            cmd.Parameters.AddWithValue("@PkStockTrnNo", tstock.PkStockTrnNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTReward_All(long FkVoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTReward_All";

            cmd.Parameters.AddWithValue("@FkVoucherNo", FkVoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTStock()
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

        public DataView GetTStockByID(long ID)
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

        public TStock ModifyTStockByID(long ID)
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
                TStock MM = new TStock();
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
                    if (!Convert.IsDBNull(dr["FreeQty"])) MM.FreeQty = Convert.ToDouble(dr["FreeQty"]);
                    if (!Convert.IsDBNull(dr["FreeUOMNo"])) MM.FreeUOMNo = Convert.ToInt64(dr["FreeUOMNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["LandedRate"])) MM.LandedRate = Convert.ToDouble(dr["LandedRate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    // if (!Convert.IsDBNull(dr["MfgCompNo"])) MM.MfgCompNo = Convert.ToInt64(dr["MfgCompNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TStock();
        }


        public bool AddTStockGodown(TStockGodown tstockgodown)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTStockGodown";

            cmd.Parameters.AddWithValue("@PKStockGodownNo", tstockgodown.PKStockGodownNo);

            //cmd.Parameters.AddWithValue("@FKStockTrnNo", tstockgodown.FKStockTrnNo);

            cmd.Parameters.AddWithValue("@ItemNo", tstockgodown.ItemNo);

            cmd.Parameters.AddWithValue("@GodownNo", tstockgodown.GodownNo);

            cmd.Parameters.AddWithValue("@Qty", tstockgodown.Qty);

            cmd.Parameters.AddWithValue("@ActualQty", tstockgodown.ActualQty);

            cmd.Parameters.AddWithValue("@UserID", tstockgodown.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tstockgodown.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tstockgodown.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTStockGodown(TStockGodown tstockgodown)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTStockGodown";

            cmd.Parameters.AddWithValue("@PKStockGodownNo", tstockgodown.PKStockGodownNo);
            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTStockGodown()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TStockGodown order by PKStockGodownNo";
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

        public DataView GetTStockGodownByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TStockGodown where PKStockGodownNo =" + ID;
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

        public TStockGodown ModifyTStockGodownByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TStockGodown where PKStockGodownNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TStockGodown MM = new TStockGodown();
                while (dr.Read())
                {
                    MM.PKStockGodownNo = Convert.ToInt32(dr["PKStockGodownNo"]);
                    if (!Convert.IsDBNull(dr["FKStockTrnNo"])) MM.FKStockTrnNo = Convert.ToInt64(dr["FKStockTrnNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["GodownNo"])) MM.GodownNo = Convert.ToInt64(dr["GodownNo"]);
                    if (!Convert.IsDBNull(dr["Qty"])) MM.Qty = Convert.ToDouble(dr["Qty"]);
                    if (!Convert.IsDBNull(dr["ActualQty"])) MM.ActualQty = Convert.ToDouble(dr["ActualQty"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TStockGodown();
        }


        public bool AddTVoucherRefDetails(TVoucherRefDetails tvoucherrefdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherRefDetails";

            cmd.Parameters.AddWithValue("@PkRefTrnNo", tvoucherrefdetails.PkRefTrnNo);

            // cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tvoucherrefdetails.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@FkVoucherSrNo", tvoucherrefdetails.FkVoucherSrNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherrefdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@RefNo", tvoucherrefdetails.RefNo);

            cmd.Parameters.AddWithValue("@TypeOfRef", tvoucherrefdetails.TypeOfRef);

            cmd.Parameters.AddWithValue("@DueDays", tvoucherrefdetails.DueDays);

            cmd.Parameters.AddWithValue("@DueDate", tvoucherrefdetails.DueDate);

            cmd.Parameters.AddWithValue("@Amount", tvoucherrefdetails.Amount);

            cmd.Parameters.AddWithValue("@DiscAmt", tvoucherrefdetails.DiscAmt);

            cmd.Parameters.AddWithValue("@SignCode", tvoucherrefdetails.SignCode);

            cmd.Parameters.AddWithValue("@UserID", tvoucherrefdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tvoucherrefdetails.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherrefdetails.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTVoucherRefDetails1(TVoucherRefDetails tvoucherrefdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherRefDetails1";

            cmd.Parameters.AddWithValue("@PkRefTrnNo", tvoucherrefdetails.PkRefTrnNo);

            cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tvoucherrefdetails.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@FkVoucherSrNo", tvoucherrefdetails.FkVoucherSrNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherrefdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@RefNo", tvoucherrefdetails.RefNo);

            cmd.Parameters.AddWithValue("@TypeOfRef", tvoucherrefdetails.TypeOfRef);

            cmd.Parameters.AddWithValue("@DueDays", tvoucherrefdetails.DueDays);

            cmd.Parameters.AddWithValue("@DueDate", tvoucherrefdetails.DueDate);

            cmd.Parameters.AddWithValue("@Amount", tvoucherrefdetails.Amount);

            cmd.Parameters.AddWithValue("@DiscAmt", tvoucherrefdetails.DiscAmt);

            cmd.Parameters.AddWithValue("@SignCode", tvoucherrefdetails.SignCode);

            cmd.Parameters.AddWithValue("@UserID", tvoucherrefdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tvoucherrefdetails.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherrefdetails.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTEWayDetails1(TEWayDetails tewaydetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTEWayDetails";

            cmd.Parameters.AddWithValue("@PKEWayNo", tewaydetails.PKEWayNo);

            //  cmd.Parameters.AddWithValue("@FkVoucherNo", tewaydetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@EWayNo", tewaydetails.EWayNo);

            cmd.Parameters.AddWithValue("@VoucherUserNo", tewaydetails.VoucherUserNo);

            cmd.Parameters.AddWithValue("@EWayDate", tewaydetails.EWayDate);


            cmd.Parameters.AddWithValue("@ModeNo", tewaydetails.ModeNo);

            cmd.Parameters.AddWithValue("@Distance", tewaydetails.Distance);

            cmd.Parameters.AddWithValue("@TransportNo", tewaydetails.TransportNo);

            cmd.Parameters.AddWithValue("@VehicleNo", tewaydetails.VehicleNo);

            cmd.Parameters.AddWithValue("@LRNo", tewaydetails.LRNo);


            cmd.Parameters.AddWithValue("@LRDate", tewaydetails.LRDate);

            cmd.Parameters.AddWithValue("@LedgerNo", tewaydetails.LedgerNo);

            cmd.Parameters.AddWithValue("@LedgerName", tewaydetails.LedgerName);

            cmd.Parameters.AddWithValue("@Address", tewaydetails.Address);

            cmd.Parameters.AddWithValue("@CityNo", tewaydetails.CityNo);


            cmd.Parameters.AddWithValue("@CityName", tewaydetails.CityName);

            cmd.Parameters.AddWithValue("@PinCode", tewaydetails.PinCode);

            cmd.Parameters.AddWithValue("@StateCode", tewaydetails.StateCode);

            cmd.Parameters.AddWithValue("@StateName", tewaydetails.StateName);

            cmd.Parameters.AddWithValue("@UserID", tewaydetails.UserID);


            cmd.Parameters.AddWithValue("@UserDate", tewaydetails.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", tewaydetails.ModifiedBy);

            cmd.Parameters.AddWithValue("@StatusNo", tewaydetails.StatusNo);

            cmd.Parameters.AddWithValue("@IsActive", tewaydetails.IsActive);

            cmd.Parameters.AddWithValue("@TranspotorNm", tewaydetails.TranspotorNm);


            cmd.Parameters.AddWithValue("@msg", tewaydetails.msg);



            commandcollection.Add(cmd);
            return true;
        }


        public bool DeleteTVoucherRefDetails(TVoucherRefDetails tvoucherrefdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherRefDetails";

            cmd.Parameters.AddWithValue("@PkRefTrnNo", tvoucherrefdetails.PkRefTrnNo);
            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTVoucherRefDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherRefDetails order by PkRefTrnNo";
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

        public DataView GetTVoucherRefDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "SELECT     MTypeOfRef.TypeOfRef, TVoucherRefDetails.RefNo, TVoucherRefDetails.Amount,0 AftAmt, MTypeOfRef.RefTypeCode, TVoucherRefDetails.PkRefTrnNo FROM TVoucherRefDetails INNER JOIN  " +
                " MTypeOfRef ON TVoucherRefDetails.TypeOfRef = MTypeOfRef.RefTypeCode where TVoucherRefDetails.FKVoucherTrnNo =" + ID;
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

        public TVoucherRefDetails ModifyTVoucherRefDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TVoucherRefDetails where PkRefTrnNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TVoucherRefDetails MM = new TVoucherRefDetails();
                while (dr.Read())
                {
                    MM.PkRefTrnNo = Convert.ToInt32(dr["PkRefTrnNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherTrnNo"])) MM.FkVoucherTrnNo = Convert.ToInt64(dr["FkVoucherTrnNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherSrNo"])) MM.FkVoucherSrNo = Convert.ToInt64(dr["FkVoucherSrNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["RefNo"])) MM.RefNo = Convert.ToInt64(dr["RefNo"]);
                    if (!Convert.IsDBNull(dr["TypeOfRef"])) MM.TypeOfRef = Convert.ToInt32(dr["TypeOfRef"]);
                    if (!Convert.IsDBNull(dr["DueDays"])) MM.DueDays = Convert.ToInt64(dr["DueDays"]);
                    if (!Convert.IsDBNull(dr["DueDate"])) MM.DueDate = Convert.ToDateTime(dr["DueDate"]);
                    if (!Convert.IsDBNull(dr["Amount"])) MM.Amount = Convert.ToDouble(dr["Amount"]);
                    if (!Convert.IsDBNull(dr["SignCode"])) MM.SignCode = Convert.ToInt64(dr["SignCode"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["Modifiedby"])) MM.Modifiedby = Convert.ToString(dr["Modifiedby"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TVoucherRefDetails();
        }

        public bool AddTVoucherPayTypeDetails(TVoucherPayTypeDetails tvoucherpaytypedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherPayTypeDetails";

            cmd.Parameters.AddWithValue("@PKVoucherPayTypeNo", tvoucherpaytypedetails.PKVoucherPayTypeNo);

            cmd.Parameters.AddWithValue("@FKSalesVoucherNo", tvoucherpaytypedetails.FKSalesVoucherNo);

            //cmd.Parameters.AddWithValue("@FKReceiptVoucherNo", tvoucherpaytypedetails.FKReceiptVoucherNo);

            //cmd.Parameters.AddWithValue("@FKVoucherTrnNo", tvoucherpaytypedetails.FKVoucherTrnNo);

            cmd.Parameters.AddWithValue("@FKPayTypeNo", tvoucherpaytypedetails.FKPayTypeNo);

            cmd.Parameters.AddWithValue("@Amount", tvoucherpaytypedetails.Amount);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherpaytypedetails.CompanyNo);

            cmd.Parameters.AddWithValue("@ChargesPerce", tvoucherpaytypedetails.ChargesPerce);

            cmd.Parameters.AddWithValue("@ChargesAmount", tvoucherpaytypedetails.ChargesAmount);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherPayTypeDetails(TVoucherPayTypeDetails tvoucherpaytypedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherPayTypeDetails";

            cmd.Parameters.AddWithValue("@PKVoucherPayTypeNo", tvoucherpaytypedetails.PKVoucherPayTypeNo);

            commandcollection.Add(cmd);
            return true;
        }

        public DataView GetAllTVoucherPayTypeDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherPayTypeDetails order by PKVoucherPayTypeNo";
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

        public DataView GetTVoucherPayTypeDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherPayTypeDetails where PKVoucherPayTypeNo =" + ID;
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

        public TVoucherPayTypeDetails ModifyTVoucherPayTypeDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TVoucherPayTypeDetails where PKVoucherPayTypeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TVoucherPayTypeDetails MM = new TVoucherPayTypeDetails();
                while (dr.Read())
                {
                    MM.PKVoucherPayTypeNo = Convert.ToInt32(dr["PKVoucherPayTypeNo"]);
                    if (!Convert.IsDBNull(dr["FKSalesVoucherNo"])) MM.FKSalesVoucherNo = Convert.ToInt64(dr["FKSalesVoucherNo"]);
                    if (!Convert.IsDBNull(dr["FKReceiptVoucherNo"])) MM.FKReceiptVoucherNo = Convert.ToInt64(dr["FKReceiptVoucherNo"]);
                    if (!Convert.IsDBNull(dr["FKVoucherTrnNo"])) MM.FKVoucherTrnNo = Convert.ToInt64(dr["FKVoucherTrnNo"]);
                    if (!Convert.IsDBNull(dr["FKPayTypeNo"])) MM.FKPayTypeNo = Convert.ToInt64(dr["FKPayTypeNo"]);
                    if (!Convert.IsDBNull(dr["Amount"])) MM.Amount = Convert.ToDouble(dr["Amount"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TVoucherPayTypeDetails();
        }

        public bool UpdateChequeDetails(long FkChequeNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TChequeNoDetails set FkVoucherTrnNo=@FkVoucherTrnNo where PkSrNo=@FkChequeNo";

            cmd.Parameters.AddWithValue("@FkChequeNo", FkChequeNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateChequeCreditDetails(long pkSrNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TVoucherChqCreditDetails set PostFkVoucherNo=@FkVoucherNo , PostFkVoucherTrnNo=@FkVoucherTrnNo, IsPost='true' where PkSrNo=@pkSrNo";

            cmd.Parameters.AddWithValue("@PkSrNo", pkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateVoucherRefDetails(long PkRefTrnNo, long RefNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TVoucherRefDetails set TypeOfRef=1, RefNo=@RefNo where PkRefTrnNo =@PkRefTrnNo ";

            cmd.Parameters.AddWithValue("@PkRefTrnNo", PkRefTrnNo);

            cmd.Parameters.AddWithValue("@RefNo", RefNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteVoucherPayTypeDetails(long RecVoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete From TVoucherPayTypeDetails where FKReceiptVoucherNo =@RecVoucherNo";

            cmd.Parameters.AddWithValue("@RecVoucherNo", RecVoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteChequeDetails(long FkChequeNo)
        {

            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TChequeNoDetails set FkVoucherTrnNo=0 where PkSrNo=@FkChequeNo";
            cmd.Parameters.AddWithValue("@FkChequeNo", FkChequeNo);
            commandcollection.Add(cmd);
            return true;

        }


        public bool AddTVoucherChequeDetails(TVoucherChequeDetails tvoucherchequedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherChequeDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tvoucherchequedetails.PkSrNo);

            cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tvoucherchequedetails.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@ClearingDate", tvoucherchequedetails.ClearingDate);

            cmd.Parameters.AddWithValue("@UserID", tvoucherchequedetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tvoucherchequedetails.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherChequeDetails(TVoucherChequeDetails tvoucherchequedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherChequeDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tvoucherchequedetails.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tvoucherchequedetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllTVoucherChequeDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherChequeDetails order by PkSrNo";
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

        public DataView GetTVoucherChequeDetailsByID(int ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TVoucherChequeDetails where PkSrNo =" + ID;
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

        public TVoucherChequeDetails ModifyTVoucherChequeDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TVoucherChequeDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TVoucherChequeDetails MM = new TVoucherChequeDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherTrnNo"])) MM.FkVoucherTrnNo = Convert.ToInt64(dr["FkVoucherTrnNo"]);
                    if (!Convert.IsDBNull(dr["ClearingDate"])) MM.ClearingDate = Convert.ToDateTime(dr["ClearingDate"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TVoucherChequeDetails();
        }

        public bool AddTVoucherChqCreditDetails(TVoucherChqCreditDetails tvoucherchqcreditdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherChqCreditDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tvoucherchqcreditdetails.PkSrNo);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", tvoucherchqcreditdetails.FKVoucherNo);

            //cmd.Parameters.AddWithValue("@FkVoucherTrnNo", tvoucherchqcreditdetails.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@ChequeNo", tvoucherchqcreditdetails.ChequeNo);

            cmd.Parameters.AddWithValue("@ChequeDate", tvoucherchqcreditdetails.ChequeDate);

            cmd.Parameters.AddWithValue("@BankNo", tvoucherchqcreditdetails.BankNo);

            cmd.Parameters.AddWithValue("@BranchNo", tvoucherchqcreditdetails.BranchNo);

            cmd.Parameters.AddWithValue("@CreditCardNo", tvoucherchqcreditdetails.CreditCardNo);

            cmd.Parameters.AddWithValue("@Amount", tvoucherchqcreditdetails.Amount);

            cmd.Parameters.AddWithValue("@IsPost", tvoucherchqcreditdetails.IsPost);

            cmd.Parameters.AddWithValue("@PostFkVoucherNo", tvoucherchqcreditdetails.PostFkVoucherNo);

            cmd.Parameters.AddWithValue("@PostFkVoucherTrnNo", tvoucherchqcreditdetails.PostFkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherchqcreditdetails.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherChqCreditDetails(TVoucherChqCreditDetails tvoucherchqcreditdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherChqCreditDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tvoucherchqcreditdetails.PkSrNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTParkingBill(TParkingBill tparkingbill)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTParkingBill";

            cmd.Parameters.AddWithValue("@ParkingBillNo", tparkingbill.ParkingBillNo);

            cmd.Parameters.AddWithValue("@BillNo", tparkingbill.BillNo);

            cmd.Parameters.AddWithValue("@BillDate", tparkingbill.BillDate);

            cmd.Parameters.AddWithValue("@BillTime", tparkingbill.BillTime);

            cmd.Parameters.AddWithValue("@PersonName", tparkingbill.PersonName);

            cmd.Parameters.AddWithValue("@LedgerNo", tparkingbill.LedgerNo);

            cmd.Parameters.AddWithValue("@IsBill", tparkingbill.IsBill);

            cmd.Parameters.AddWithValue("@CompanyNo", tparkingbill.CompanyNo);

            cmd.Parameters.AddWithValue("@IsCancel", tparkingbill.IsCancel);

            cmd.Parameters.AddWithValue("@UserID", tparkingbill.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tparkingbill.UserDate);

            cmd.Parameters.AddWithValue("@Discount", tparkingbill.Discount);

            cmd.Parameters.AddWithValue("@Charges", tparkingbill.Charges);

            cmd.Parameters.AddWithValue("@Charges2", tparkingbill.Charges2);

            cmd.Parameters.AddWithValue("@Remark", tparkingbill.Remark);

            cmd.Parameters.AddWithValue("@RateTypeNo", tparkingbill.RateTypeNo);

            cmd.Parameters.AddWithValue("@TaxTypeNo", tparkingbill.TaxTypeNo);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTParkingBillDetails(TParkingBillDetails tparkingbilldetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTParkingBillDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tparkingbilldetails.PkSrNo);

            //cmd.Parameters.AddWithValue("@ParkingBillNo", tparkingbilldetails.ParkingBillNo);

            cmd.Parameters.AddWithValue("@Barcode", tparkingbilldetails.Barcode);

            cmd.Parameters.AddWithValue("@Qty", tparkingbilldetails.Qty);

            cmd.Parameters.AddWithValue("@Rate", tparkingbilldetails.Rate);

            cmd.Parameters.AddWithValue("@ItemDisc", tparkingbilldetails.ItemDisc);

            cmd.Parameters.AddWithValue("@UOMNo", tparkingbilldetails.UOMNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", tparkingbilldetails.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@ItemNo", tparkingbilldetails.ItemNo);

            cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTParkingBill(TParkingBill tparkingbill)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTParkingBill";

            cmd.Parameters.AddWithValue("@ParkingBillNo", tparkingbill.ParkingBillNo);

            cmd.Parameters.AddWithValue("@FKVoucherNo", tparkingbill.FKVoucherNo);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tparkingbill.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteTParkingBillDetails(TPackingListDetails tPackingListDetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTPackingListDetailsItemNo";

            cmd.Parameters.AddWithValue("@FkVoucherNo", tPackingListDetails.FkVoucherNo);

            cmd.Parameters.AddWithValue("@ItemNo", tPackingListDetails.ItemNo);

            commandcollection.Add(cmd);
            return true;
        }


        public bool DeleteTParkingBillDetails(TParkingBillDetails tparkingbilldetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTParkingBillDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tparkingbilldetails.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tparkingbilldetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            bool transfer = false;
            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int cntVchNo = -1, cntVchNoES = -1, cntTrsD = 0, cntRef = 0, cntRefES = 0, cntStock = 0, cntStockES = 0, cntRateSettingNo = -1, cntRateSettingNoES = -1, RewardNo = -1, RewardDtlsNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;

                        if (commandcollection[i].CommandText == "AddTVoucherEntry")
                        {

                            cntVchNo = i;

                        }

                        if (commandcollection[i].CommandText == "AddTEWayDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddTranspotorDetail")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);

                        }

                        if (commandcollection[i].CommandText == "AddTVoucherEntryES")
                        {
                            if (DBGetVal.KachhaFirm == true)
                            {//======for pakka bill referance
                                commandcollection[i].Parameters.AddWithValue("@PkRefNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                                transfer = true;
                            }
                            cntVchNoES = i;
                        }
                        if (commandcollection[i].CommandText == "AddTVoucherDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            cntRef = i;
                        }
                        if (commandcollection[i].CommandText == "AddTDeliveryAddress")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddTVoucherDetailsES")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNoES].Parameters["@ReturnID"].Value);
                            cntRefES = i;
                        }
                        if (commandcollection[i].CommandText == "AddTVoucherRefDetails")
                        {
                            // if(cntRef!=0)
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            //else
                            //  commandcollection[i].Parameters["@FkVoucherTrnNo"].Value = commandcollection[cntRateSettingNo].Parameters["@ReturnID"].Value;
                        }
                        if (commandcollection[i].CommandText == "AddTStock")
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
                        if (commandcollection[i].CommandText == "AddTStockES")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNoES].Parameters["@ReturnID"].Value);
                            if (cntRefES != 0)
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRefES].Parameters["@ReturnID"].Value);
                            else
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                            cntStockES = i;
                            if (cntRateSettingNoES != -1)
                            {
                                commandcollection[i].Parameters["@FkRateSettingNo"].Value = commandcollection[cntRateSettingNoES].Parameters["@ReturnID"].Value;

                                //commandcollection[i].CommandText.IndexOf("@FkRateSettingNo", Convert.ToInt32(commandcollection[cntRateSettingNo].Parameters["@ReturnID"].Value));

                                cntRateSettingNoES = -1;
                            }
                        }
                        if (commandcollection[i].CommandText == "AddTStockGodown")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FKStockTrnNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTVoucherPayTypeDetails")
                        {
                            if (cntVchNo != -1)
                            {
                                commandcollection[i].Parameters.AddWithValue("@FKReceiptVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                                if (cntRef != 0)
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            }
                            else
                            {
                                commandcollection[i].Parameters.AddWithValue("@FKReceiptVoucherNo", 0);
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                            }
                        }
                        if (commandcollection[i].CommandText == "TMRateStock")
                        {


                        }
                        if (commandcollection[i].CommandText.IndexOf("Update TDeliveryChallan") >= 0)
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }
                        else if (commandcollection[i].CommandText.IndexOf("Update") >= 0)
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
                        if (commandcollection[i].CommandText == "AddTVoucherChqCreditDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            if (cntRef != 0)
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            else
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                        }
                        if (commandcollection[i].CommandText == "AddTParkingBill")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTParkingBillDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ParkingBillNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMRateSetting3")
                        {
                            cntRateSettingNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTVoucherRefDetails1")
                        {

                        }
                        if (commandcollection[i].CommandText == "AddTReward")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            RewardNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTRewardDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@RewardNo", commandcollection[RewardNo].Parameters["@ReturnID"].Value);
                            RewardDtlsNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTRewardFrom")
                        {
                            RewardDtlsNo = ReturnCommPos(Convert.ToInt64(commandcollection[RewardDtlsNo].Parameters["@SchemeDetailsNo"].Value));

                            commandcollection[i].Parameters.AddWithValue("@RewardNo", commandcollection[RewardNo].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@RewardDetailsNo", commandcollection[RewardDtlsNo].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@FkStockNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTRewardTo")
                        {
                            RewardDtlsNo = ReturnCommPos(Convert.ToInt64(commandcollection[RewardDtlsNo].Parameters["@SchemeDetailsNo"].Value));
                            commandcollection[i].Parameters.AddWithValue("@RewardNo", commandcollection[RewardNo].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@RewardDetailsNo", commandcollection[RewardDtlsNo].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@FkStockNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTFooterDiscountDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTItemLevelDiscountDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FKStockTrnNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTOtherStockDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@FKStockTrnNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "StockEffect")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTGRNInvoice")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }
                        //if (commandcollection[i].CommandText == "AddTVoucherEntryCompany")
                        //{
                        //    cntVchCompNo = i;
                        //    commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        //}
                        //if (commandcollection[i].CommandText == "AddTVoucherDetailsCompany")
                        //{
                        //    cntVchCompNo = GetCommandIndex("AddTVoucherEntryCompany", Convert.ToInt64(commandcollection[i].Parameters["@MfgCompNo"].Value.ToString()));
                        //    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                        //    commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchCompNo].Parameters["@ReturnID"].Value);
                        //    cntVchDtlsCompNo = i;
                        //}
                        if (commandcollection[i].CommandText == "AddTChequePrinting")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText.IndexOf("UpdateChequeDetails") > -1)
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTVoucherJournalEntry")
                        {
                            if (cntVchNo != -1)
                            {
                                commandcollection[i].Parameters.AddWithValue("@FKReceiptVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            }
                            else
                            {
                                commandcollection[i].Parameters.AddWithValue("@FKreceiptVoucherNo", 0);
                            }
                        }

                        //if (commandcollection[i].CommandText == "AddTRewardFrom")
                        //{
                        //    DataRow[] dr = dtId.Select("SchemeDetailsNo=" + commandcollection[i].Parameters["@SchemeDetailsNo"].Value);
                        //    commandcollection[i].Parameters.AddWithValue("@RewardNo",Convert.ToInt64(dr[0]["RewardNo"].ToString()));
                        //    commandcollection[i].Parameters.AddWithValue("@RewardDetailsNo", Convert.ToInt64(dr[0]["RewardDetailsNo"].ToString()));
                        //    commandcollection[i].Parameters.AddWithValue("@FkStockNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        //}
                        //if (commandcollection[i].CommandText == "AddTRewardTo")
                        //{
                        //    DataRow[] dr = dtId.Select("SchemeDetailsNo=" + commandcollection[i].Parameters["@SchemeDetailsNo"].Value);
                        //    commandcollection[i].Parameters.AddWithValue("@RewardNo", Convert.ToInt64(dr[0]["RewardNo"].ToString()));
                        //    commandcollection[i].Parameters.AddWithValue("@RewardDetailsNo", Convert.ToInt64(dr[0]["RewardDetailsNo"].ToString()));
                        //    commandcollection[i].Parameters.AddWithValue("@FkStockNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        //}

                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();

                        //if (commandcollection[i].CommandText == "AddTRewardDetails")
                        //{
                        //    AddRows(Convert.ToInt64(commandcollection[i].Parameters["@SchemeDetailsNo"].Value), Convert.ToInt64(commandcollection[i].Parameters["@RewardNo"].Value),Convert.ToInt64(commandcollection[i].Parameters["@ReturnID"].Value));
                        //}
                    }
                }

                myTrans.Commit();
                if (cntVchNo == -1)
                    return 0;
                else
                {
                    if ((DBGetVal.KachhaFirm == true) && (transfer == true))
                    {
                        ObjTrans.ExecuteQuery("Update TVoucherEntry set PkRefno= " + Convert.ToInt64(commandcollection[cntVchNoES].Parameters["@ReturnID"].Value) + " where pkvoucherno= " + Convert.ToInt64(commandcollection[cntVchNo].Parameters["@ReturnID"].Value), CommonFunctions.ConStr);
                        ObjTrans.ExecuteQuery("Update TVoucherEntry set PkRefno= " + Convert.ToInt64(commandcollection[cntVchNo].Parameters["@ReturnID"].Value) + " where pkvoucherno= " + Convert.ToInt64(commandcollection[cntVchNoES].Parameters["@ReturnID"].Value), CommonFunctions.ConStr);

                    }
                    return Convert.ToInt64(commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                }

                //commandcollection.Clear();
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

        public bool UpdateVoucherDetails(double Debit, double Credit, long PkVoucherTrnNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TVoucherDetails set Debit=@Debit , Credit=@Credit where PKVoucherTrnNo=@PkVoucherTrnNo";

            cmd.Parameters.AddWithValue("@Debit", Debit);
            cmd.Parameters.AddWithValue("@Credit", Credit);
            cmd.Parameters.AddWithValue("@PkVoucherTrnNo", PkVoucherTrnNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateVoucherDetailsCompany(double Debit, double Credit, long PkVoucherCompTrnNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TVoucherDetailsCompany set Debit=@Debit , Credit=@Credit where PKVoucherCompTrnNo=@PkVoucherCompTrnNo";

            cmd.Parameters.AddWithValue("@Debit", Debit);
            cmd.Parameters.AddWithValue("@Credit", Credit);
            cmd.Parameters.AddWithValue("@PkVoucherCompTrnNo", PkVoucherCompTrnNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateVoucherEntry(double BilledAmount, long PkVoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TVoucherEntry set BilledAmount=@BilledAmount where PKVoucherNo=@PkVoucherNo";

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

        public bool DeleteAllVoucherEntry(TVoucherEntry tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAllVoucherEntry";

            cmd.Parameters.AddWithValue("@VoucherNo", tvoucherentry.PkVoucherNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tvoucherentry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool CancelTVoucherEntry(TVoucherEntry tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CancelTVoucherEntry";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tvoucherentry.PkVoucherNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tvoucherentry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }
        public bool CancelTVoucherEntrynew(TVoucherEntry tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CancelTVoucherEntry";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tvoucherentry.PkVoucherNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool CancelPostTVoucherEntry(long tvoucherentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CancelTVoucherEntry";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tvoucherentry);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                //tvoucherentry.msg = ObjTrans.ErrorMessage;
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

        public bool DeleteAllVoucherEntry(long PKvoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAllVoucherEntry";

            cmd.Parameters.AddWithValue("@VoucherNo", PKvoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteAllVoucherEntryNew(long PKvoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteAllVoucherEntryNew";
            //DeleteWithoutVoucherEntry

            cmd.Parameters.AddWithValue("@VoucherNo", PKvoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteWithoutVoucherEntry(long PKvoucherNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteWithoutVoucherEntry";
            //DeleteWithoutVoucherEntry

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

        public bool AddMRateSetting3(MRateSetting3 mratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRateSetting3";

            cmd.Parameters.AddWithValue("@PkSrNo", mratesetting.PkSrNo);

            //   cmd.Parameters.AddWithValue("@FkBcdSrNo", mratesetting.FkBcdSrNo);

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

        public bool UpdateMRateSetting(MRateSetting mratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update MRateSetting set PurRate=@PurRate Where PkSrNo=@PkSrNo";

            cmd.Parameters.AddWithValue("@PkSrNo", mratesetting.PkSrNo);

            cmd.Parameters.AddWithValue("@PurRate", mratesetting.PurRate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateMRateSetting1(MRateSetting mratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update MRateSetting set ASaleRate=@ASaleRate,BSaleRate=@BSaleRate Where PkSrNo=@PkSrNo";

            cmd.Parameters.AddWithValue("@PkSrNo", mratesetting.PkSrNo);

            cmd.Parameters.AddWithValue("@ASaleRate", mratesetting.ASaleRate);

            cmd.Parameters.AddWithValue("@BSaleRate", mratesetting.BSaleRate);

            commandcollection.Add(cmd);
            return true;
        }
        public bool UpdateMRateSettingStock(MRateSetting mratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "Update MRateSetting set Stock=@Stock  Where PkSrNo=@PkSrNo,and UOMNo=@UOMNo,and ItemNo=@ItemNo and MRP=@MRP";
            cmd.CommandText = "TMRateStock";
            cmd.Parameters.AddWithValue("@ItemNo", mratesetting.ItemNo);
            cmd.Parameters.AddWithValue("@PkSrNo", mratesetting.PkSrNo);
            cmd.Parameters.AddWithValue("@MRP", mratesetting.MRP);
            cmd.Parameters.AddWithValue("@UOMNo", mratesetting.UOMNo);
            cmd.Parameters.AddWithValue("@ESFlag", mratesetting.ESFlag);
            cmd.Parameters.AddWithValue("@ASaleRate", mratesetting.ASaleRate);// INPUT value as quantity not rate 
            cmd.Parameters.AddWithValue("@StockConversion", mratesetting.StockConversion);

            commandcollection.Add(cmd);
            return true;
        }


        public bool AddTReward(TReward treward)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTReward";

            cmd.Parameters.AddWithValue("@RewardNo", treward.RewardNo);

            //cmd.Parameters.AddWithValue("@FkVoucherNo", treward.FkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherUserNo", treward.VoucherUserNo);

            cmd.Parameters.AddWithValue("@TotalBillAmount", treward.TotalBillAmount);

            cmd.Parameters.AddWithValue("@LedgerNo", treward.LedgerNo);

            cmd.Parameters.AddWithValue("@ToalDiscAmount", treward.ToalDiscAmount);

            cmd.Parameters.AddWithValue("@FkVoucherTrnNo", treward.FkVoucherTrnNo);

            cmd.Parameters.AddWithValue("@RedemptionStatusNo", treward.RedemptionStatusNo);

            cmd.Parameters.AddWithValue("@CompanyNo", treward.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", treward.UserID);

            cmd.Parameters.AddWithValue("@UserDate", treward.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            dtId = ObjFunction.GetDataView("SELECT SchemeDetailsNo, RewardNo, PkSrNo AS 'RewardDetailsNo' FROM TRewardDetails WHERE (PkSrNo = 0)").Table;
            return true;
        }

        public void AddRows(long SchDetailsNo, long RewardNo, long RewardDetailsNo)
        {
            DataRow dr = dtId.NewRow();
            dr[0] = SchDetailsNo;
            dr[1] = RewardNo;
            dr[2] = RewardDetailsNo;
            dtId.Rows.Add(dr);
        }

        public bool DeleteTReward(TReward treward)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTReward";

            cmd.Parameters.AddWithValue("@RewardNo", treward.RewardNo);
            commandcollection.Add(cmd);
            return true;
        }

        public TReward ModifyTRewardByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TReward where RewardNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TReward MM = new TReward();
                while (dr.Read())
                {
                    MM.RewardNo = Convert.ToInt32(dr["RewardNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt64(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["VoucherUserNo"])) MM.VoucherUserNo = Convert.ToInt64(dr["VoucherUserNo"]);
                    if (!Convert.IsDBNull(dr["TotalBillAmount"])) MM.TotalBillAmount = Convert.ToInt64(dr["TotalBillAmount"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["ToalDiscAmount"])) MM.ToalDiscAmount = Convert.ToInt64(dr["ToalDiscAmount"]);
                    if (!Convert.IsDBNull(dr["FkVoucherTrnNo"])) MM.FkVoucherTrnNo = Convert.ToInt64(dr["FkVoucherTrnNo"]);
                    if (!Convert.IsDBNull(dr["RedemptionStatusNo"])) MM.RedemptionStatusNo = Convert.ToInt64(dr["RedemptionStatusNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TReward();
        }

        public DataView GetAllTReward()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TReward order by RewardNo";
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

        public DataView GetAllTReward(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TReward Where RewardNo=" + ID;
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


        public bool AddTRewardDetails(TRewardDetails trewarddetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTRewardDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", trewarddetails.PkSrNo);

            // cmd.Parameters.AddWithValue("@RewardNo", trewarddetails.RewardNo);

            cmd.Parameters.AddWithValue("@SchemeNo", trewarddetails.SchemeNo);

            cmd.Parameters.AddWithValue("@SchemeDetailsNo", trewarddetails.SchemeDetailsNo);

            cmd.Parameters.AddWithValue("@SchemeType", trewarddetails.SchemeType);

            cmd.Parameters.AddWithValue("@DiscPer", trewarddetails.DiscPer);

            cmd.Parameters.AddWithValue("@DiscAmount", trewarddetails.DiscAmount);

            cmd.Parameters.AddWithValue("@SchemeAmount", trewarddetails.SchemeAmount);

            cmd.Parameters.AddWithValue("@SchemeAcheiverNo", trewarddetails.SchemeAcheiverNo);

            cmd.Parameters.AddWithValue("@CompanyNo", trewarddetails.CompanyNo);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTRewardDetails(TRewardDetails trewarddetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTRewardDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", trewarddetails.PkSrNo);
            commandcollection.Add(cmd);
            return true;
        }

        public TRewardDetails ModifyTRewardDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TRewardDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TRewardDetails MM = new TRewardDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["RewardNo"])) MM.RewardNo = Convert.ToInt64(dr["RewardNo"]);
                    if (!Convert.IsDBNull(dr["SchemeNo"])) MM.SchemeNo = Convert.ToInt64(dr["SchemeNo"]);
                    if (!Convert.IsDBNull(dr["SchemeDetailsNo"])) MM.SchemeDetailsNo = Convert.ToInt64(dr["SchemeDetailsNo"]);
                    if (!Convert.IsDBNull(dr["SchemeType"])) MM.SchemeType = Convert.ToInt64(dr["SchemeType"]);
                    if (!Convert.IsDBNull(dr["DiscPer"])) MM.DiscPer = Convert.ToInt64(dr["DiscPer"]);
                    if (!Convert.IsDBNull(dr["DiscAmount"])) MM.DiscAmount = Convert.ToInt64(dr["DiscAmount"]);
                    if (!Convert.IsDBNull(dr["SchemeAmount"])) MM.SchemeAmount = Convert.ToInt64(dr["SchemeAmount"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TRewardDetails();
        }

        public DataView GetAllTRewardDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TRewardDetails order by PkSrNo";
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

        public DataView GetAllTRewardDetails(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TRewardDetails Where PkSrNo=" + ID;
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


        public bool AddTRewardFrom(TRewardFrom trewardfrom)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTRewardFrom";

            cmd.Parameters.AddWithValue("@PkSrNo", trewardfrom.PkSrNo);

            // cmd.Parameters.AddWithValue("@RewardNo", trewardfrom.RewardNo);

            // cmd.Parameters.AddWithValue("@RewardDetailsNo", trewardfrom.RewardDetailsNo);

            cmd.Parameters.AddWithValue("@SchemeDetailsNo", trewardfrom.SchemeDetailsNo);

            cmd.Parameters.AddWithValue("@SchemeFromNo", trewardfrom.SchemeFromNo);

            //  cmd.Parameters.AddWithValue("@FkStockNo", trewardfrom.FkStockNo);

            cmd.Parameters.AddWithValue("@CompanyNo", trewardfrom.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTRewardFrom(TRewardFrom trewardfrom)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTRewardFrom";

            cmd.Parameters.AddWithValue("@PkSrNo", trewardfrom.PkSrNo);
            commandcollection.Add(cmd);
            return true;
        }

        public TRewardFrom ModifyTRewardFromByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TRewardFrom where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TRewardFrom MM = new TRewardFrom();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["RewardNo"])) MM.RewardNo = Convert.ToInt64(dr["RewardNo"]);
                    if (!Convert.IsDBNull(dr["RewardDetailsNo"])) MM.RewardDetailsNo = Convert.ToInt64(dr["RewardDetailsNo"]);
                    if (!Convert.IsDBNull(dr["SchemeFromNo"])) MM.SchemeFromNo = Convert.ToInt64(dr["SchemeFromNo"]);
                    if (!Convert.IsDBNull(dr["FkStockNo"])) MM.FkStockNo = Convert.ToInt64(dr["FkStockNo"]);

                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TRewardFrom();
        }

        public DataView GetAllTRewardFrom()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TRewardFrom order by PkSrNo";
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

        public DataView GetAllTRewardFrom(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TRewardFrom Where PkSrNo=" + ID;
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

        public bool AddTRewardTo(TRewardTo trewardto)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTRewardTo";

            cmd.Parameters.AddWithValue("@PkSrNo", trewardto.PkSrNo);

            // cmd.Parameters.AddWithValue("@RewardNo", trewardto.RewardNo);

            //   cmd.Parameters.AddWithValue("@RewardDetailsNo", trewardto.RewardDetailsNo);

            cmd.Parameters.AddWithValue("@SchemeDetailsNo", trewardto.SchemeDetailsNo);

            cmd.Parameters.AddWithValue("@SchemeToNo", trewardto.SchemeToNo);

            //  cmd.Parameters.AddWithValue("@FkStockNo", trewardto.FkStockNo);

            cmd.Parameters.AddWithValue("@CompanyNo", trewardto.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTRewardTo(TRewardTo trewardto)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTRewardTo";

            cmd.Parameters.AddWithValue("@PkSrNo", trewardto.PkSrNo);
            commandcollection.Add(cmd);
            return true;
        }

        public TRewardTo ModifyTRewardToByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TRewardTo where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TRewardTo MM = new TRewardTo();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["RewardNo"])) MM.RewardNo = Convert.ToInt64(dr["RewardNo"]);
                    if (!Convert.IsDBNull(dr["RewardDetailsNo"])) MM.RewardDetailsNo = Convert.ToInt64(dr["RewardDetailsNo"]);
                    if (!Convert.IsDBNull(dr["SchemeToNo"])) MM.SchemeToNo = Convert.ToInt64(dr["SchemeToNo"]);
                    if (!Convert.IsDBNull(dr["FkStockNo"])) MM.FkStockNo = Convert.ToInt64(dr["FkStockNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TRewardTo();
        }

        public DataView GetAllTRewardTo()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TRewardTo order by PkSrNo";
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

        public DataView GetAllTRewardTo(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from TRewardTo Where PkSrNo=" + ID;
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

        public bool AddTFooterDiscountDetails(TFooterDiscountDetails tfooterdiscountdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTFooterDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tfooterdiscountdetails.PkSrNo);

            cmd.Parameters.AddWithValue("@FooterDiscNo", tfooterdiscountdetails.FooterDiscNo);

            cmd.Parameters.AddWithValue("@FooterDiscDetailsNo", tfooterdiscountdetails.FooterDiscDetailsNo);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", tfooterdiscountdetails.FKVoucherNo);

            cmd.Parameters.AddWithValue("@DiscAmount", tfooterdiscountdetails.DiscAmount);

            cmd.Parameters.AddWithValue("@CompanyNo", tfooterdiscountdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", tfooterdiscountdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tfooterdiscountdetails.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTFooterDiscountDetails(TFooterDiscountDetails tfooterdiscountdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTFooterDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", tfooterdiscountdetails.PkSrNo);
            commandcollection.Add(cmd);
            return true;
        }

        public bool AddTItemLevelDiscountDetails(TItemLevelDiscountDetails titemleveldiscountdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTItemLevelDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", titemleveldiscountdetails.PkSrNo);

            cmd.Parameters.AddWithValue("@ItemDiscNo", titemleveldiscountdetails.ItemDiscNo);

            cmd.Parameters.AddWithValue("@ItemBrandDiscNo", titemleveldiscountdetails.ItemBrandDiscNo);

            cmd.Parameters.AddWithValue("@ItemNo", titemleveldiscountdetails.ItemNo);

            cmd.Parameters.AddWithValue("@DiscAmount", titemleveldiscountdetails.DiscAmount);

            //cmd.Parameters.AddWithValue("@FKStockTrnNo", titemleveldiscountdetails.FKStockTrnNo);

            cmd.Parameters.AddWithValue("@CompanyNo", titemleveldiscountdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", titemleveldiscountdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", titemleveldiscountdetails.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTItemLevelDiscountDetails(TItemLevelDiscountDetails titemleveldiscountdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTItemLevelDiscountDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", titemleveldiscountdetails.PkSrNo);
            commandcollection.Add(cmd);
            return true;
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

        public bool AddMSchemeAchieverDetails(MSchemeAchieverDetails mschemeachieverdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSchemeAchieverDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemeachieverdetails.PkSrNo);

            cmd.Parameters.AddWithValue("@SchemeAchSrNo", mschemeachieverdetails.SchemeAchSrNo);

            cmd.Parameters.AddWithValue("@SchemeAchDate", mschemeachieverdetails.SchemeAchDate);

            cmd.Parameters.AddWithValue("@SchemeAchieverNo", mschemeachieverdetails.SchemeAchieverNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mschemeachieverdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@RefNo", mschemeachieverdetails.RefNo);

            cmd.Parameters.AddWithValue("@TypeOfRef", mschemeachieverdetails.TypeOfRef);

            cmd.Parameters.AddWithValue("@Amount", mschemeachieverdetails.Amount);

            cmd.Parameters.AddWithValue("@SignCode", mschemeachieverdetails.SignCode);

            cmd.Parameters.AddWithValue("@CompanyNo", mschemeachieverdetails.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mschemeachieverdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mschemeachieverdetails.UserDate);

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

        public bool DeleteTOtherStockDetails(TOtherStockDetails totherstockdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTOtherStockDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", totherstockdetails.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool EffectStock()
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "StockEffect";

            commandcollection.Add(cmd);
            return true;
        }

        public bool ReverseStock(long ID)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "StockReverse";
            cmd.Parameters.AddWithValue("@FkVoucherNo", ID);
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

        public bool AddTGRNInvoice(TGRNInvoice tgrninvoice)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTGRNInvoice";

            cmd.Parameters.AddWithValue("@PkSrNo", tgrninvoice.PkSrNo);

            cmd.Parameters.AddWithValue("@FkGRNNo", tgrninvoice.FkGRNNo);

            // cmd.Parameters.AddWithValue("@FkVoucherNo", tgrninvoice.FkVoucherNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tgrninvoice.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", tgrninvoice.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tgrninvoice.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        #region Voucher Company wise
        public bool AddTVoucherEntryCompany(TVoucherEntryCompany tvoucherentrycompany)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherEntryCompany";

            cmd.Parameters.AddWithValue("@PKVoucherCompanyNo", tvoucherentrycompany.PKVoucherCompanyNo);

            // cmd.Parameters.AddWithValue("@FkVoucherNo", tvoucherentrycompany.FkVoucherNo);

            cmd.Parameters.AddWithValue("@VoucherTypeCode", tvoucherentrycompany.VoucherTypeCode);

            cmd.Parameters.AddWithValue("@VoucherUserNo", tvoucherentrycompany.VoucherUserNo);

            cmd.Parameters.AddWithValue("@BilledAmount", tvoucherentrycompany.BilledAmount);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherentrycompany.CompanyNo);

            cmd.Parameters.AddWithValue("@MfgCompNo", tvoucherentrycompany.MfgCompNo);

            cmd.Parameters.AddWithValue("@PayTypeNo", tvoucherentrycompany.PayTypeNo);

            cmd.Parameters.AddWithValue("@TaxTypeNo", tvoucherentrycompany.TaxTypeNo);



            cmd.Parameters.AddWithValue("@TaxInvoiceTypeNo", tvoucherentrycompany.TaxInvoiceTypeNo);

            cmd.Parameters.AddWithValue("@UserID", tvoucherentrycompany.UserID);

            cmd.Parameters.AddWithValue("@UserDate", tvoucherentrycompany.UserDate);
            //   cmd.Parameters.AddWithValue("@voucherdate", tvoucherenetry.VoucherDate);


            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
            //if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    tvoucherentrycompany.msg = ObjTrans.ErrorMessage;
            //    return false;
            //}
        }

        public bool DeleteTVoucherEntryCompany(TVoucherEntryCompany tvoucherentrycompany)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherEntryCompany";

            cmd.Parameters.AddWithValue("@PKVoucherCompanyNo", tvoucherentrycompany.PKVoucherCompanyNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tvoucherentrycompany.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public TVoucherEntryCompany ModifyTVoucherEntryCompanyByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TVoucherEntryCompany where PKVoucherCompanyNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TVoucherEntryCompany MM = new TVoucherEntryCompany();
                while (dr.Read())
                {
                    MM.PKVoucherCompanyNo = Convert.ToInt32(dr["PKVoucherCompanyNo"]);
                    if (!Convert.IsDBNull(dr["FkVoucherNo"])) MM.FkVoucherNo = Convert.ToInt64(dr["FkVoucherNo"]);
                    if (!Convert.IsDBNull(dr["VoucherTypeCode"])) MM.VoucherTypeCode = Convert.ToInt64(dr["VoucherTypeCode"]);
                    if (!Convert.IsDBNull(dr["VoucherUserNo"])) MM.VoucherUserNo = Convert.ToInt64(dr["VoucherUserNo"]);
                    if (!Convert.IsDBNull(dr["BilledAmount"])) MM.BilledAmount = Convert.ToInt64(dr["BilledAmount"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["MfgCompNo"])) MM.MfgCompNo = Convert.ToInt64(dr["MfgCompNo"]);
                    if (!Convert.IsDBNull(dr["PayTypeNo"])) MM.PayTypeNo = Convert.ToInt64(dr["PayTypeNo"]);
                    if (!Convert.IsDBNull(dr["TaxInvoiceTypeNo"])) MM.TaxInvoiceTypeNo = Convert.ToInt64(dr["TaxInvoiceTypeNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TVoucherEntryCompany();
        }

        public bool AddTVoucherDetailsCompany(TVoucherDetailsCompany tvoucherdetailscompany)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherDetailsCompany";

            cmd.Parameters.AddWithValue("@PkVoucherCompTrnNo", tvoucherdetailscompany.PkVoucherCompTrnNo);

            //cmd.Parameters.AddWithValue("@FkVoucherNo", tvoucherdetailscompany.FkVoucherNo);

            //cmd.Parameters.AddWithValue("@FKVoucherTrnNo", tvoucherdetailscompany.FKVoucherTrnNo);

            cmd.Parameters.AddWithValue("@VoucherSrNo", tvoucherdetailscompany.VoucherSrNo);

            cmd.Parameters.AddWithValue("@SignCode", tvoucherdetailscompany.SignCode);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherdetailscompany.LedgerNo);

            cmd.Parameters.AddWithValue("@Debit", tvoucherdetailscompany.Debit);

            cmd.Parameters.AddWithValue("@Credit", tvoucherdetailscompany.Credit);

            cmd.Parameters.AddWithValue("@SrNo", tvoucherdetailscompany.SrNo);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherdetailscompany.CompanyNo);

            cmd.Parameters.AddWithValue("@Narration", tvoucherdetailscompany.Narration);

            cmd.Parameters.AddWithValue("@MfgCompNo", tvoucherdetailscompany.MfgCompNo);

            commandcollection.Add(cmd);
            return true;
            //if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    tvoucherdetailscompany.msg = ObjTrans.ErrorMessage;
            //    return false;
            //}
        }

        public bool DeleteTVoucherDetailsCompany(TVoucherDetailsCompany tvoucherdetailscompany)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherDetailsCompany";

            cmd.Parameters.AddWithValue("@PkVoucherCompTrnNo", tvoucherdetailscompany.PkVoucherCompTrnNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tvoucherdetailscompany.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        #endregion


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

        public bool AddTChequePrinting(TChequePrinting tchequeprinting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTChequePrinting";

            cmd.Parameters.AddWithValue("@PkSrNo", tchequeprinting.PkSrNo);

            cmd.Parameters.AddWithValue("@PrintingUserNo", tchequeprinting.PrintingUserNo);

            cmd.Parameters.AddWithValue("@PrintingDate", tchequeprinting.PrintingDate);

            cmd.Parameters.AddWithValue("@IsPurchase", tchequeprinting.IsPurchase);

            //cmd.Parameters.AddWithValue("@FKVoucherNo", tchequeprinting.FKVoucherNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tchequeprinting.LedgerNo);

            cmd.Parameters.AddWithValue("@ByLedgerNo", tchequeprinting.ByLedgerNo);

            cmd.Parameters.AddWithValue("@ChequeDate", tchequeprinting.ChequeDate);

            cmd.Parameters.AddWithValue("@ChequeNo", tchequeprinting.ChequeNo);

            cmd.Parameters.AddWithValue("@FKChequeNo", tchequeprinting.FKChequeNo);

            cmd.Parameters.AddWithValue("@Remark1", tchequeprinting.Remark1);

            cmd.Parameters.AddWithValue("@Remark2", tchequeprinting.Remark2);

            cmd.Parameters.AddWithValue("@Remark3", tchequeprinting.Remark3);

            cmd.Parameters.AddWithValue("@ChqStatusNo", tchequeprinting.ChqStatusNo);

            cmd.Parameters.AddWithValue("@BankDate", tchequeprinting.BankDate);

            cmd.Parameters.AddWithValue("@CompanyNo", tchequeprinting.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTChequePrinting(TChequePrinting tchequeprinting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTChequePrinting";

            cmd.Parameters.AddWithValue("@PkSrNo", tchequeprinting.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tchequeprinting.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public TChequePrinting ModifyTChequePrintingByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from TChequePrinting where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                TChequePrinting MM = new TChequePrinting();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["PrintingUserNo"])) MM.PrintingUserNo = Convert.ToInt64(dr["PrintingUserNo"]);
                    if (!Convert.IsDBNull(dr["PrintingDate"])) MM.PrintingDate = Convert.ToDateTime(dr["PrintingDate"]);
                    if (!Convert.IsDBNull(dr["IsPurchase"])) MM.IsPurchase = Convert.ToBoolean(dr["IsPurchase"]);
                    if (!Convert.IsDBNull(dr["FKVoucherNo"])) MM.FKVoucherNo = Convert.ToInt64(dr["FKVoucherNo"]);
                    if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["ByLedgerNo"])) MM.ByLedgerNo = Convert.ToInt64(dr["ByLedgerNo"]);
                    if (!Convert.IsDBNull(dr["ChequeDate"])) MM.ChequeDate = Convert.ToDateTime(dr["ChequeDate"]);
                    if (!Convert.IsDBNull(dr["ChequeNo"])) MM.ChequeNo = Convert.ToString(dr["ChequeNo"]);
                    if (!Convert.IsDBNull(dr["FKChequeNo"])) MM.FKChequeNo = Convert.ToInt64(dr["FKChequeNo"]);
                    if (!Convert.IsDBNull(dr["Remark1"])) MM.Remark1 = Convert.ToString(dr["Remark1"]);
                    if (!Convert.IsDBNull(dr["Remark2"])) MM.Remark2 = Convert.ToString(dr["Remark2"]);
                    if (!Convert.IsDBNull(dr["Remark3"])) MM.Remark3 = Convert.ToString(dr["Remark3"]);
                    if (!Convert.IsDBNull(dr["ChqStatusNo"])) MM.ChqStatusNo = Convert.ToInt64(dr["ChqStatusNo"]);
                    if (!Convert.IsDBNull(dr["BankDate"])) MM.BankDate = Convert.ToDateTime(dr["BankDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt64(dr["StatusNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new TChequePrinting();
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


        #region SaveBills
        #region Update and Delete Querys

        public bool UpdateBillAmount(long PKvoucherNo, double BilledAmount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TVoucherEntry Set BilledAmount=@BilledAmount Where PkVoucherNo=@PkVoucherNo";


            cmd.Parameters.AddWithValue("@BilledAmount", BilledAmount);
            cmd.Parameters.AddWithValue("@PkVoucherNo", PKvoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateBillAmountCompanySingle(long FkVoucherNo, double BilledAmount)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TVoucherEntryCompany Set BilledAmount=@BilledAmount Where FkVoucherNo=@FkVoucherNo";


            cmd.Parameters.AddWithValue("@BilledAmount", BilledAmount);
            cmd.Parameters.AddWithValue("@FkVoucherNo", FkVoucherNo);

            commandcollection.Add(cmd);
            return true;
        }

        public void DeleteSalesBillWithReceipt(long SalesID, long CompNo, DateTime BillDate, long PartyLedgerNo, int PayTypeNo)
        {
            if (PayTypeNo == 1 || PayTypeNo == 3)
            {
                DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
                String strQuery = "";

                #region Check Sales Bill's Collection details and take action on it
                strQuery = "SELECT     TVE_B.PkVoucherNo AS PkVoucherNoSales, TVE_B.VoucherTypeCode, TVE_B.VoucherUserNo, TVE_BF.VoucherUserNo AS VoucherUserNoFirmwise, " +
                          " TVE_B.BilledAmount AS BilledAmountSales, TVD_B.Debit, TVD_B.Credit, TVRD_B.Amount, TVRD_C.Amount AS TotAdjustedAmount, TVRD_C.Amount - TVRD_C.DiscAmt AS MainAmount, " +
                          " TVRD_C.DiscAmt, LINK_B_C_J.JVAmount As InterestAmount, TVE_C.BilledAmount AS TotalCollAmount, TVE_C.PkVoucherNo AS PkVoucherNoCollection, TVE_C.VoucherTypeCode AS VoucherTypeCodeCollection, " +
                          " LINK_B_C_J.FKVoucherNo AS FKSalesVoucherNo, LINK_B_C_J.FKReceiptVoucherNo, LINK_B_C_J.FKJournalVoucherNo, TVRD_B.PkRefTrnNo As RefPkSales, TVRD_C.PkRefTrnNo As RefPkColl " +
                          " FROM         TVoucherEntry AS TVE_B INNER JOIN " +
                          " TVoucherEntryCompany AS TVE_BF ON TVE_B.PkVoucherNo = TVE_BF.FkVoucherNo INNER JOIN " +
                          " TVoucherDetails AS TVD_B ON TVE_B.PkVoucherNo = TVD_B.FkVoucherNo INNER JOIN " +
                          " TVoucherRefDetails AS TVRD_B ON TVD_B.PkVoucherTrnNo = TVRD_B.FkVoucherTrnNo INNER JOIN " +
                          " TVoucherRefDetails AS TVRD_C ON TVRD_B.RefNo = TVRD_C.RefNo AND TVRD_B.PkRefTrnNo <> TVRD_C.PkRefTrnNo INNER JOIN " +
                          " TVoucherDetails AS TVD_C ON TVRD_C.FkVoucherTrnNo = TVD_C.PkVoucherTrnNo INNER JOIN " +
                          " TVoucherEntry AS TVE_C ON TVD_C.FkVoucherNo = TVE_C.PkVoucherNo LEFT OUTER JOIN " +
                          " TVoucherJournalEntry AS LINK_B_C_J ON TVE_C.PkVoucherNo = LINK_B_C_J.FKReceiptVoucherNo AND TVE_B.PkVoucherNo = LINK_B_C_J.FKVoucherNo " +
                          " WHERE     (TVE_B.VoucherTypeCode = " + VchType.Sales + ") AND (TVE_B.PkVoucherNo in (" + SalesID + ")) " +
                          " ORDER BY PkVoucherNoSales, TVRD_C.PkRefTrnNo";

                DataTable dtCollectionDetails = ObjFunction.GetDataView(strQuery).Table;

                for (int iColl = 0; iColl < dtCollectionDetails.Rows.Count; iColl++)
                {
                    long CollectionID = Convert.ToInt64(dtCollectionDetails.Rows[iColl]["PkVoucherNoCollection"].ToString());
                    int CollectionVoucherTypeCode = Convert.ToInt32(dtCollectionDetails.Rows[iColl]["VoucherTypeCodeCollection"].ToString());

                    #region Fetch Collection's Other Adjustment Details and take adjustment details
                    strQuery = "SELECT     TVE_B.PkVoucherNo AS PkVoucherNoSales, TVE_B.VoucherTypeCode, TVE_B.VoucherUserNo, TVE_BF.VoucherUserNo AS VoucherUserNoFirmwise, " +
                          " TVE_B.BilledAmount AS BilledAmountSales, TVD_B.Debit, TVD_B.Credit, TVRD_B.Amount, TVRD_C.Amount AS TotAdjustedAmount, TVRD_C.Amount - TVRD_C.DiscAmt AS MainAmount, " +
                          " TVRD_C.DiscAmt, LINK_B_C_J.JVAmount As InterestAmount, TVE_C.BilledAmount AS TotalCollAmount, TVE_C.PkVoucherNo AS PkVoucherNoCollection, TVE_C.VoucherTypeCode AS VoucherTypeCodeCollection, " +
                          " LINK_B_C_J.FKVoucherNo AS FKSalesVoucherNo, LINK_B_C_J.FKReceiptVoucherNo, LINK_B_C_J.FKJournalVoucherNo, TVRD_B.PkRefTrnNo As RefPkSales, TVRD_C.PkRefTrnNo As RefPkColl " +
                          " FROM         TVoucherEntry AS TVE_B INNER JOIN " +
                          " TVoucherEntryCompany AS TVE_BF ON TVE_B.PkVoucherNo = TVE_BF.FkVoucherNo INNER JOIN " +
                          " TVoucherDetails AS TVD_B ON TVE_B.PkVoucherNo = TVD_B.FkVoucherNo INNER JOIN " +
                          " TVoucherRefDetails AS TVRD_B ON TVD_B.PkVoucherTrnNo = TVRD_B.FkVoucherTrnNo INNER JOIN " +
                          " TVoucherRefDetails AS TVRD_C ON TVRD_B.RefNo = TVRD_C.RefNo AND TVRD_B.PkRefTrnNo <> TVRD_C.PkRefTrnNo INNER JOIN " +
                          " TVoucherDetails AS TVD_C ON TVRD_C.FkVoucherTrnNo = TVD_C.PkVoucherTrnNo INNER JOIN " +
                          " TVoucherEntry AS TVE_C ON TVD_C.FkVoucherNo = TVE_C.PkVoucherNo LEFT OUTER JOIN " +
                          " TVoucherJournalEntry AS LINK_B_C_J ON TVE_C.PkVoucherNo = LINK_B_C_J.FKReceiptVoucherNo AND TVE_B.PkVoucherNo = LINK_B_C_J.FKVoucherNo " +
                          " WHERE     (TVE_B.VoucherTypeCode = " + VchType.Sales + ") AND (TVE_B.PkVoucherNo not in (" + SalesID + ")) " +
                          " AND (TVE_C.PkVoucherNo in (" + CollectionID + ")) " +
                          " ORDER BY PkVoucherNoSales, TVRD_C.PkRefTrnNo";

                    DataTable dtCollectionAdjustmentDetails = ObjFunction.GetDataView(strQuery).Table;

                    if (dtCollectionAdjustmentDetails.Rows.Count == 0)
                    {
                        #region Other Adjustments doesn't exist, So delete Collection/SalesReturn & JV
                        #region Delete JV
                        if (dtCollectionDetails.Rows[iColl]["FKJournalVoucherNo"] != DBNull.Value)
                        {
                            dbTVoucherEntry.DeleteAllVoucherEntryNew(Convert.ToInt64(dtCollectionDetails.Rows[iColl]["FKJournalVoucherNo"].ToString()));
                        }
                        #endregion
                        #region Delete Collection / SalesReturn
                        dbTVoucherEntry.DeleteAllVoucherEntryNew(CollectionID);
                        #endregion
                        #endregion
                    }
                    else
                    {
                        #region Other Adjustments found, So reduce collection/salesreturn amount and delete JV
                        if (CollectionVoucherTypeCode == VchType.SalesReceipt)
                        {
                            #region Delete JV's Agst Ref in Collection And JV's Voucher
                            if (dtCollectionDetails.Rows[iColl]["FKJournalVoucherNo"] != DBNull.Value)
                            {
                                strQuery = "SELECT TVRD_C.PkRefTrnNo " +
                                        " FROM         TVoucherDetails AS TVD_B INNER JOIN " +
                                        " TVoucherRefDetails AS TVRD_B ON TVD_B.PkVoucherTrnNo = TVRD_B.FkVoucherTrnNo INNER JOIN " +
                                        " TVoucherRefDetails AS TVRD_C ON TVRD_B.RefNo = TVRD_C.RefNo AND TVRD_B.PkRefTrnNo <> TVRD_C.PkRefTrnNo " +
                                        " WHERE     (TVD_B.FkVoucherNo in (" + dtCollectionDetails.Rows[iColl]["FKJournalVoucherNo"] + ")) ";

                                TVoucherRefDetails tvref = new TVoucherRefDetails();
                                tvref.PkRefTrnNo = ObjQry.ReturnLong(strQuery, CommonFunctions.ConStr);
                                dbTVoucherEntry.DeleteTVoucherRefDetails(tvref);

                                strQuery = "SELECT PkSrNo From TVoucherJournalEntry " +
                                    " WHERE FKJournalVoucherNo = " + dtCollectionDetails.Rows[iColl]["FKJournalVoucherNo"] + " ";

                                TVoucherJournalEntry tvje = new TVoucherJournalEntry();
                                tvje.PKSrNo = ObjQry.ReturnLong(strQuery, CommonFunctions.ConStr);
                                dbTVoucherEntry.DeleteTVoucherJournalEntry(tvje);

                                dbTVoucherEntry.DeleteAllVoucherEntryNew(Convert.ToInt64(dtCollectionDetails.Rows[iColl]["FKJournalVoucherNo"].ToString()));
                            }
                            #endregion

                            #region Delete Bill's Agst ref in Collection And Reduce Collection Amount

                            #region Delete Bill's Agst ref in Collection

                            strQuery = "SELECT TVRD_C.PkRefTrnNo " +
                                        " FROM         TVoucherDetails AS TVD_B INNER JOIN " +
                                        " TVoucherRefDetails AS TVRD_B ON TVD_B.PkVoucherTrnNo = TVRD_B.FkVoucherTrnNo INNER JOIN " +
                                        " TVoucherRefDetails AS TVRD_C ON TVRD_B.RefNo = TVRD_C.RefNo AND TVRD_B.PkRefTrnNo <> TVRD_C.PkRefTrnNo INNER JOIN " +
                                        " TVoucherDetails AS TVD_C ON TVRD_C.FkVoucherTrnNo = TVD_C.PkVoucherTrnNo " +
                                        " WHERE     (TVD_B.FkVoucherNo in (" + SalesID + ")) AND (TVD_C.FkVoucherNo in (" + CollectionID + ")) ";

                            TVoucherRefDetails tvrefCol = new TVoucherRefDetails();
                            tvrefCol.PkRefTrnNo = ObjQry.ReturnLong(strQuery, CommonFunctions.ConStr);

                            dbTVoucherEntry.DeleteTVoucherRefDetails(tvrefCol);

                            #endregion

                            #region Reduce TVoucherDetails
                            double MainAmount = 0, DiscountAmount = 0, InterestAmount = 0;
                            MainAmount = Convert.ToDouble(dtCollectionDetails.Rows[iColl]["MainAmount"].ToString());
                            DiscountAmount = Convert.ToDouble(dtCollectionDetails.Rows[iColl]["DiscAmt"].ToString());
                            if (dtCollectionDetails.Rows[iColl]["InterestAmount"] != DBNull.Value)
                                InterestAmount = Convert.ToDouble(dtCollectionDetails.Rows[iColl]["InterestAmount"].ToString());
                            DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit,SrNo FROM TVoucherDetails " +
                                            " WHERE (FkVoucherNo = " + CollectionID + ") order by VoucherSrNo ").Table;
                            for (int i = 0; i < dtReceipt.Rows.Count; i++)
                            {
                                int iSrNo = Convert.ToInt32(dtReceipt.Rows[i].ItemArray[4].ToString());
                                double debitAmount = Convert.ToDouble(dtReceipt.Rows[i].ItemArray[2].ToString());
                                double creditAmount = Convert.ToDouble(dtReceipt.Rows[i].ItemArray[3].ToString());

                                if (iSrNo == Others.Party)
                                {
                                    creditAmount = creditAmount - MainAmount - DiscountAmount - InterestAmount;
                                    dbTVoucherEntry.UpdateVoucherDetails(0, creditAmount, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                                    dbTVoucherEntry.UpdateBillAmount(CollectionID, creditAmount);
                                }
                                else if (iSrNo == Others.Discount1)
                                {
                                    debitAmount = debitAmount - DiscountAmount;
                                    dbTVoucherEntry.UpdateVoucherDetails(debitAmount, 0, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                                }
                                else if (iSrNo == 0)
                                {
                                    debitAmount = debitAmount - MainAmount - InterestAmount;
                                    dbTVoucherEntry.UpdateVoucherDetails(debitAmount, 0, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                                }
                            }
                            #endregion

                            #region Reduce TVoucherDetailsCompany
                            dtReceipt = ObjFunction.GetDataView("Select PKVoucherCompTrnNo,TVoucherDetailsCompany.LedgerNo,TVoucherDetailsCompany.Debit, " +
                                " TVoucherDetailsCompany.Credit, TVoucherDetailsCompany.SrNo " +
                                " From TVoucherDetails INNER JOIN TVoucherDetailsCompany ON TVoucherDetails.PkVoucherTrnNo=TVoucherDetailsCompany.FKVoucherTrnNo " +
                                " Where TVoucherDetails.FKVoucherNo=" + CollectionID + " AND " +
                                " TVoucherDetails.CompanyNo=" + DBGetVal.FirmNo //+ " AND TVoucherDetailsCompany.MfgCompNo=" + MainMfgCompNo + ""
                                ).Table;
                            for (int i = 0; i < dtReceipt.Rows.Count; i++)
                            {
                                int iSrNo = Convert.ToInt32(dtReceipt.Rows[i].ItemArray[4].ToString());
                                double debitAmount = Convert.ToDouble(dtReceipt.Rows[i].ItemArray[2].ToString());
                                double creditAmount = Convert.ToDouble(dtReceipt.Rows[i].ItemArray[3].ToString());

                                if (iSrNo == Others.Party)
                                {
                                    creditAmount = creditAmount - MainAmount - DiscountAmount - InterestAmount;
                                    dbTVoucherEntry.UpdateVoucherDetailsCompany(0, creditAmount, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                                    dbTVoucherEntry.UpdateBillAmountCompanySingle(CollectionID, creditAmount);
                                }
                                else if (iSrNo == Others.Discount1)
                                {
                                    debitAmount = debitAmount - DiscountAmount;
                                    dbTVoucherEntry.UpdateVoucherDetailsCompany(debitAmount, 0, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                                }
                                else if (iSrNo == 0)
                                {
                                    debitAmount = debitAmount - MainAmount - InterestAmount;
                                    dbTVoucherEntry.UpdateVoucherDetailsCompany(debitAmount, 0, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                                }
                            }
                            #endregion

                            #endregion
                        }
                        else
                        {
                            OMMessageBox.Show("Sales Return Adjusted against multiple Bills found !!!" + Environment.NewLine + Environment.NewLine +
                                "Can't Delete Bill and its collection details ...");
                            //TODO Code here???
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                #region Delete Sales Bill
                dbTVoucherEntry.DeleteAllVoucherEntryNew(SalesID);
                #endregion

                dbTVoucherEntry.ExecuteNonQueryStatements();

            }
            else
            {
                #region Delete Bill and Update Auto Receipt
                DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();

                #region Delete Sales Bill
                dbTVoucherEntry.DeleteAllVoucherEntryNew(SalesID);
                dbTVoucherEntry.ExecuteNonQueryStatements();
                #endregion

                #region Update Auto Receipt

                long ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                            " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherEntry.VoucherDate ='" + Convert.ToDateTime(BillDate).ToString("dd-MMM-yyyy") + "') AND (TVoucherDetails.LedgerNo = " + PartyLedgerNo + ") AND " +
                            " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=" + PayTypeNo + ") AND TVoucherEntry.CompanyNo=" + CompNo + "", CommonFunctions.ConStr);
                if (ReceiptID != 0)
                {
                    dbTVoucherEntry = new DBTVaucherEntry();

                    double Amt = ObjQry.ReturnDouble("SELECT IsNull(SUM(TVoucherDetails.Debit),0) FROM TVoucherEntry INNER JOIN " +
                            " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                            " WHERE (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.PayTypeNo=" + PayTypeNo + ") AND " +
                            " (TVoucherDetails.LedgerNo = " + PartyLedgerNo + ") AND " +
                            " (TVoucherEntry.CompanyNo = " + CompNo + ") AND (TVoucherEntry.VoucherDate = '" + BillDate + "') AND " +
                            " (TVoucherEntry.VoucherTypeCode = " + VchType.Sales + ") ", CommonFunctions.ConStr);

                    if (Amt == 0)
                        dbTVoucherEntry.DeleteAllVoucherEntryNew(ReceiptID);
                    else
                    {
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit,SrNo FROM TVoucherDetails " +
                          " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        for (int i = 0; i < dtReceipt.Rows.Count; i++)
                        {
                            if (i == 0)
                                dbTVoucherEntry.UpdateVoucherDetails(0, Amt, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                            else
                                dbTVoucherEntry.UpdateVoucherDetails(Amt, 0, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                        }
                        dbTVoucherEntry.UpdateBillAmount(ReceiptID, Amt);
                    }

                    dbTVoucherEntry.ExecuteNonQueryStatements();
                }
                #endregion

                #endregion
            }
        }

        public bool UpdateReceiptDetails(long SalesID, long CompNo, DateTime BillDate, long PartyLedgerNo, long VoucherType)
        {
            long ReceiptType = 0;
            bool flag = false;

            DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
            flag = dbTVoucherEntry.DeleteAllVoucherEntry(SalesID);
            dbTVoucherEntry.ExecuteNonQueryStatements();
            dbTVoucherEntry = new DBTVaucherEntry();

            if (VoucherType == VchType.Sales)
                ReceiptType = VchType.Receipt;



            long ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                        " WHERE (TVoucherEntry.VoucherTypeCode = " + ReceiptType + ") AND (TVoucherEntry.VoucherDate ='" + Convert.ToDateTime(BillDate).ToString("dd-MMM-yyyy") + "') AND (TVoucherDetails.LedgerNo = " + PartyLedgerNo + ") AND " +
                        " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=2) AND TVoucherEntry.CompanyNo=" + CompNo + "", CommonFunctions.ConStr);
            if (ReceiptID != 0)
            {
                double Amt = ObjQry.ReturnDouble("SELECT IsNull(SUM(TVoucherDetails.Debit),0) FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                                              " WHERE (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.PayTypeNo=2) AND (TVoucherDetails.LedgerNo = " + PartyLedgerNo + ") AND (TVoucherEntry.CompanyNo = " + CompNo + ") AND (TVoucherEntry.VoucherDate = '" + BillDate + "') AND  (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") ", CommonFunctions.ConStr);

                if (Amt == 0)
                    dbTVoucherEntry.DeleteAllVoucherEntry(ReceiptID);
                else
                {
                    DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit,SrNo FROM TVoucherDetails " +
                      " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                    for (int i = 0; i < dtReceipt.Rows.Count; i++)
                    {
                        if (i == 0)
                            dbTVoucherEntry.UpdateVoucherDetails(0, Amt, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                        else
                            dbTVoucherEntry.UpdateVoucherDetails(Amt, 0, Convert.ToInt64(dtReceipt.Rows[i].ItemArray[0].ToString()));
                    }
                    dbTVoucherEntry.UpdateBillAmount(ReceiptID, Amt);
                }
                dbTVoucherEntry.ExecuteNonQueryStatements();
            }
            return flag;
        }

        #endregion
        public long SaveBill(string val, long VchCode)
        {
            string[] str = new string[1];
            str[0] = "\n";
            string[] strLine = val.Split(str, StringSplitOptions.None);
            long strReturn = -1;
            string[] strData;

            dtHeader = new DataTable();
            dtDetails = new DataTable();
            string[] strW = new string[1];
            strW[0] = ",";
            VType = VchCode;

            if (strLine.Length >= 2)
            {
                strData = strLine[0].Split(strW, StringSplitOptions.None);
                if (strData.Length == HeaderLen)
                {
                    for (int i = 0; i < strData.Length; i++)
                    {
                        dtHeader.Columns.Add();
                    }
                    DataRow dr = dtHeader.NewRow();
                    for (int i = 0; i < strData.Length; i++)
                    {
                        dr[i] = strData[i].ToString();
                    }
                    dtHeader.Rows.Add(dr);

                    for (int row = 1; row < strLine.Length; row++)
                    {
                        strData = strLine[row].Split(strW, StringSplitOptions.None);
                        if (strData.Length == DataLen - 7)
                        {
                            if (row == 1)
                            {
                                for (int i = 0; i < DataLen; i++)
                                {
                                    dtDetails.Columns.Add();
                                }
                            }

                            dr = dtDetails.NewRow();
                            for (int i = 0; i < strData.Length; i++)
                            {
                                dr[i] = strData[i].ToString();
                            }
                            dtDetails.Rows.Add(dr);
                        }
                        else
                        {
                            strReturn = 0;
                            break;
                        }
                    }



                }
                else
                    strReturn = 0;
            }
            if (strReturn == -1)
            {
                //if (ObjQry.ReturnLong("Select PkVoucherNo from TVoucherEntry Where Reference='" + dtHeader.Rows[0].ItemArray[HeaderColIndex.Reference].ToString() + "'") == 0)
                //{
                long BillNo = 0;
                BillNo = SaveVouchers(VType);
                //else
                //  BillNo = SaveStockInward(VType);
                if (BillNo != 0)
                    strReturn = BillNo;
                else
                    strReturn = 0;
                //}
                //else
                //    strReturn = "Fail";
            }

            return strReturn;
        }

        public void CalculateDetails()
        {
            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {

                DataTable dtOther = ObjFunction.GetDataView("SELECT r.FkBcdSrNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                    " t.PkSrNo,t.Percentage,r.PkSrNo FROM GetItemRateAll(" + Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.ItemNo].ToString()) + ",NULL," + Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.UOM].ToString()) + ",NULL,'" + Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString()).Date + "',NULL) As r,GetItemTaxAll(" + Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.ItemNo].ToString()) + ", NULL, " + GroupType.SalesAccount + "," + ObjFunction.GetAppSettings(AppSettings.S_TaxType) + ",NULL) As t " +
                    " WHERE r.ItemNo = t.ItemNo").Table;

                double DiscRs = 0, DiscPer = 0, TaxPercentage = 0, taxAmount = 0, NetRate = 0, NetAmt = 0;
                double Amount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Amount].ToString());
                SubTotalAmt = SubTotalAmt + Amount;
                DiscRs = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc1Amount].ToString()) + Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc2Amount].ToString()) + Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc3Amount].ToString());
                DiscPer = Math.Round((DiscRs * 100) / Amount, 2);

                if (ObjQry.ReturnBoolean("Select IsMfgType From MManufacturerCompany Where MfgCompNo=" + dtDetails.Rows[i][DataColIndex.MFGCompNo].ToString() + "", CommonFunctions.ConStr) == true)
                {
                    TaxPercentage = Convert.ToDouble(dtOther.Rows[0].ItemArray[6].ToString());
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)))
                    {
                        taxAmount = Convert.ToDouble((((Amount) * TaxPercentage) / (100 + TaxPercentage)).ToString("0.00"));
                        NetRate = Convert.ToDouble((((Amount) - taxAmount) / Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                        NetAmt = Convert.ToDouble((NetRate * Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                    }
                    else
                    {
                        taxAmount = Convert.ToDouble((((Amount - DiscRs) * TaxPercentage / 100)).ToString("0.00"));
                        NetRate = Convert.ToDouble((((Amount - DiscRs)) / Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                        NetAmt = Convert.ToDouble((NetRate * Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                    }

                    TotalTaxAmt = TotalTaxAmt + taxAmount;
                    TotalDiscAmt = TotalDiscAmt + DiscRs;

                    dtDetails.Rows[i][DataColIndex.LedgerTaxNo] = dtOther.Rows[0].ItemArray[3].ToString();
                    dtDetails.Rows[i][DataColIndex.SaleTaxNo] = dtOther.Rows[0].ItemArray[4].ToString();
                    dtDetails.Rows[i][DataColIndex.FkItemTaxSettingNo] = dtOther.Rows[0].ItemArray[5].ToString();
                }
                else
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)))
                    {
                        NetRate = Convert.ToDouble((Amount / Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                        NetAmt = Convert.ToDouble((NetRate * Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                    }
                    else
                    {
                        NetRate = Convert.ToDouble(((Amount - DiscRs) / Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                        NetAmt = Convert.ToDouble((NetRate * Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
                    }

                    dtDetails.Rows[i][DataColIndex.LedgerTaxNo] = Convert.ToInt64(dtZeroTax.Rows[0][0].ToString());
                    dtDetails.Rows[i][DataColIndex.SaleTaxNo] = Convert.ToInt64(dtZeroTax.Rows[0][1].ToString());
                    dtDetails.Rows[i][DataColIndex.TaxPerce] = TaxPercentage;
                    dtDetails.Rows[i][DataColIndex.FkItemTaxSettingNo] = Convert.ToInt64(dtZeroTax.Rows[0][3].ToString());
                }

                dtDetails.Rows[i][DataColIndex.SGSTAmount] = taxAmount;
                dtDetails.Rows[i][DataColIndex.TaxPerce] = TaxPercentage;
                dtDetails.Rows[i][DataColIndex.NetRate] = NetRate;
                dtDetails.Rows[i][DataColIndex.NetAmount] = NetAmt;
                dtDetails.Rows[i][DataColIndex.Disc1Amount] = DiscRs;
                dtDetails.Rows[i][DataColIndex.DsicPer] = DiscPer;
                dtDetails.Rows[i][DataColIndex.RateSettingNo] = dtOther.Rows[0].ItemArray[7].ToString();
                dtDetails.Rows[i][DataColIndex.FkStockBarcodeNo] = dtOther.Rows[0].ItemArray[0].ToString();
                dtDetails.Rows[i][DataColIndex.StockConversion] = dtOther.Rows[0].ItemArray[2].ToString();
            }
        }

        //private long SaveVouchers(long vchCode)
        //{

        //    long ID = 0;
        //    CompNo = DBGetVal.FirmNo;
        //    TVoucherEntry tVoucher = new TVoucherEntry();


        //    DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        //    TVoucherEntry tVoucherEntry = new TVoucherEntry();
        //    TVoucherDetails tVoucherDetails = new TVoucherDetails();
        //    TVoucherPayTypeDetails tVchPayTypeDetails = new TVoucherPayTypeDetails();
        //    TVoucherRefDetails tVchRefDtls = new TVoucherRefDetails();
        //    TVoucherChqCreditDetails tVchChqCredit = new TVoucherChqCreditDetails();
        //    TStock tStock = new TStock();
        //    TStockGodown tStockGodown = new TStockGodown();
        //    DataTable dtItemTax = new DataTable();
        //    dtItemTax.Columns.Add("");
        //    dtItemTax.Columns.Add("");
        //    dtItemTax.Columns.Add("");
        //    dtItemTax.Columns.Add("");

        //    long VoucherSrNo = 1;
        //    tVoucherEntry = new TVoucherEntry();
        //    tVoucherEntry.PkVoucherNo = 0;
        //    tVoucherEntry.VoucherTypeCode = VchType.Sales;
        //    tVoucherEntry.VoucherUserNo = 0;
        //    tVoucherEntry.VoucherDate = Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString());
        //    tVoucherEntry.VoucherTime = Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherTime].ToString());
        //    tVoucherEntry.Narration = "Sales Bill";
        //    tVoucherEntry.Reference = dtHeader.Rows[0].ItemArray[HeaderColIndex.YadiServerNo].ToString();
        //    tVoucherEntry.ChequeNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.ChequeNo].ToString());
        //    tVoucherEntry.ClearingDate = Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.ChequeDate].ToString());
        //    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
        //    tVoucherEntry.BilledAmount = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.BilledAmount].ToString());
        //    tVoucherEntry.ChallanNo = "";
        //    tVoucherEntry.Remark = "";
        //    tVoucherEntry.MacNo = 0;
        //    tVoucherEntry.PayTypeNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.PayType].ToString());
        //    tVoucherEntry.RateTypeNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.RateType].ToString());
        //    tVoucherEntry.TaxTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_TaxType));
        //    tVoucherEntry.OrderType = 1;
        //    tVoucherEntry.DiscPercent = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscPercent].ToString());
        //    tVoucherEntry.DiscAmt = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString());
        //    tVoucherEntry.MixMode = 0;
        //    tVoucherEntry.IsItemLevelDisc = Convert.ToBoolean(dtHeader.Rows[0].ItemArray[HeaderColIndex.IsItemLevelDisc].ToString());
        //    tVoucherEntry.IsFooterLevelDisc = Convert.ToBoolean(dtHeader.Rows[0].ItemArray[HeaderColIndex.IsFooterLevelDisc].ToString());
        //    tVoucherEntry.UserID = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.UserID].ToString());
        //    tVoucherEntry.UserDate = tVoucherEntry.VoucherDate;
        //    tVoucherEntry.LRNo = "";

        //    //tVoucherEntry.YadiVoucherNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.FkYadiVoucherNo].ToString());
        //    dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

        //    tVoucherDetails = new TVoucherDetails();
        //    tVoucherDetails.PkVoucherTrnNo = 0;
        //    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
        //    tVoucherDetails.SignCode = 1;

        //    tVoucherDetails.LedgerNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.LedgerNo].ToString());
        //    tVoucherDetails.Debit = tVoucherEntry.BilledAmount;
        //    tVoucherDetails.Credit = 0;
        //    tVoucherDetails.Narration = "";
        //    tVoucherDetails.CompanyNo = CompNo;
        //    tVoucherDetails.SrNo = Others.Party;

        //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

        //    if (tVoucherEntry.PayTypeNo == 3)//For Credit Bill
        //    {
        //        tVchRefDtls = new TVoucherRefDetails();
        //        tVchRefDtls.PkRefTrnNo = ObjQry.ReturnLong("Select PKRefTrnNo From TVoucherRefDetails Where FKVoucherTrnNo=" + tVoucherDetails.PkVoucherTrnNo + " ANd CompanyNo=" + CompNo + " ", CommonFunctions.ConStr);
        //        tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
        //        tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
        //        tVchRefDtls.TypeOfRef = 3;
        //        tVchRefDtls.RefNo = 0;
        //        tVchRefDtls.DueDays = 0;
        //        tVchRefDtls.DueDate = DateTime.Now.Date;
        //        tVchRefDtls.Amount = tVoucherEntry.BilledAmount;
        //        tVchRefDtls.SignCode = 1;
        //        tVchRefDtls.UserID = DBGetVal.UserID;
        //        tVchRefDtls.UserDate = DateTime.Now.Date;
        //        tVchRefDtls.CompanyNo = CompNo;
        //        dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);
        //    }
        //    double SubTotalAmt = 0, TotalTaxAmt = 0, TotalDiscAmt = 0;

        //    for (int i = 0; i < dtDetails.Rows.Count; i++)
        //    {

        //        tStock = new TStock();
        //        tStock.PkStockTrnNo = 0;
        //        tStock.ItemNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.ItemNo].ToString());
        //        tStock.FkUomNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.UOM].ToString());
        //        DataTable dtOther = ObjFunction.GetDataView("SELECT r.FkBcdSrNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
        //            " t.PkSrNo,t.Percentage,r.PkSrNo FROM GetItemRateAll(" + tStock.ItemNo + ",NULL," + tStock.FkUomNo + ",NULL,'" + tVoucherEntry.VoucherDate.Date + "',NULL) As r,GetItemTaxAll(" + tStock.ItemNo + ", NULL, " + GroupType.SalesAccount + "," + ObjFunction.GetAppSettings(AppSettings.S_TaxType) + ",NULL) As t " +
        //            " WHERE r.ItemNo = t.ItemNo").Table;

        //        double DiscRs = 0, DiscAmt = 0, DiscPer = 0;
        //        double TaxPercentage = Convert.ToDouble(dtOther.Rows[0].ItemArray[6].ToString());
        //        double Amount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Amount].ToString());
        //        SubTotalAmt = SubTotalAmt + Amount;
        //        DiscRs = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc1Amount].ToString()) + Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc2Amount].ToString()) + Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc3Amount].ToString());
        //        DiscPer = Math.Round((DiscRs * 100) / Amount, 2);
        //        double taxAmount = 0, NetRate = 0, NetAmt = 0;
        //        if (true)
        //        {
        //            taxAmount = Convert.ToDouble((((Amount) * TaxPercentage) / (100 + TaxPercentage)).ToString("0.00"));
        //            NetRate = Convert.ToDouble((((Amount) - taxAmount) / Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
        //            NetAmt = Convert.ToDouble((NetRate * Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
        //        }
        //        else
        //        {
        //            taxAmount = Convert.ToDouble((((Amount - DiscRs) * TaxPercentage / 100)).ToString("0.00"));
        //            NetRate = Convert.ToDouble((((Amount - DiscRs)) / Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
        //            NetAmt = Convert.ToDouble((NetRate * Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString())).ToString("0.00"));
        //        }

        //        dtDetails.Rows[i][DataColIndex.SGSTAmount] = taxAmount;

        //        TotalTaxAmt = TotalTaxAmt + taxAmount;
        //        TotalDiscAmt = TotalDiscAmt + DiscRs;

        //        tStock.GroupNo = 0;
        //        tStock.FkVoucherSrNo = VoucherSrNo;
        //        tStock.TrnCode = 2;
        //        tStock.Quantity = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString());
        //        tStock.BilledQuantity = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString()) * Convert.ToDouble(dtOther.Rows[0].ItemArray[2].ToString());
        //        tStock.Rate = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Rate].ToString());
        //        tStock.Amount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Amount].ToString());
        //        tStock.SGSTPercentage = TaxPercentage;
        //        tStock.SGSTAmount = taxAmount;
        //        tStock.DiscPercentage = DiscPer;
        //        tStock.DiscAmount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc1Amount].ToString());
        //        tStock.DiscRupees = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc2Amount].ToString());
        //        tStock.DiscPercentage2 = 0;
        //        tStock.DiscAmount2 = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc3Amount].ToString());
        //        tStock.DiscRupees2 = 0;
        //        tStock.NetRate = NetRate;
        //        tStock.NetAmount = NetAmt;
        //        tStock.FkStockBarCodeNo = Convert.ToInt64(dtOther.Rows[0].ItemArray[0].ToString());// ObjQry.ReturnLong("Select PKStockBarCodeNo From MStockBarCode Where BarCode='" + dtDetails.Rows[i].ItemArray[DataColIndex.BarCode].ToString() + "'");

        //        tStock.FkRateSettingNo = Convert.ToInt64(dtOther.Rows[0].ItemArray[7].ToString());
        //        tStock.FkItemTaxInfo = Convert.ToInt64(dtOther.Rows[0].ItemArray[5].ToString());
        //        tStock.FreeQty = 0;
        //        tStock.FreeUOMNo = 1;
        //        tStock.UserID = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.UserID].ToString());
        //        tStock.UserDate = DateTime.Now.Date;
        //        tStock.CompanyNo = CompNo;
        //        tStock.DisplayItemName = "";
        //        tStock.LandedRate = 0;
        //        tStock.MfgCompNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.MFGCompNo].ToString());
        //        dbTVoucherEntry.AddTStock(tStock);

        //        TOtherStockDetails tOtherStkDtls = new TOtherStockDetails();
        //        tOtherStkDtls.PkSrNo = 0;
        //        tOtherStkDtls.FKOtherVoucherNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.YadiServerNo].ToString());
        //        tOtherStkDtls.FKOtherStockTrnNo = Convert.ToInt64(dtDetails.Rows[0].ItemArray[DataColIndex.YadiSrNo].ToString());
        //        tOtherStkDtls.ItemNo = tStock.ItemNo;
        //        tOtherStkDtls.Quantity = tStock.Quantity;
        //        tOtherStkDtls.UserID = DBGetVal.UserID;
        //        tOtherStkDtls.UserDate = DBGetVal.ServerTime.Date;
        //        tOtherStkDtls.CompanyNo = DBGetVal.FirmNo;
        //        dbTVoucherEntry.AddTOtherStockDetails(tOtherStkDtls);


        //        tStockGodown = new TStockGodown();
        //        tStockGodown.PKStockGodownNo = 0;
        //        tStockGodown.ItemNo = tStock.ItemNo;
        //        tStockGodown.GodownNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation));
        //        tStockGodown.Qty = tStock.Quantity;
        //        tStockGodown.ActualQty = tStock.BilledQuantity;
        //        tStockGodown.UserID = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.UserID].ToString());
        //        tStockGodown.UserDate = DateTime.Now.Date;

        //        dbTVoucherEntry.AddTStockGodown(tStockGodown);

        //        DataRow dr1 = dtItemTax.NewRow();
        //        dr1[0] = dtOther.Rows[0].ItemArray[3].ToString();
        //        dr1[1] = dtOther.Rows[0].ItemArray[4].ToString();
        //        dr1[2] = tStock.NetAmount.ToString();
        //        dr1[3] = tStock.SGSTAmount.ToString();
        //        dtItemTax.Rows.Add(dr1);

        //    }

        //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == false)
        //    {
        //        setCompanyRatio((SubTotalAmt), TotalTaxAmt, TotalDiscAmt);
        //        tVoucherEntry.BilledAmount = tVoucherEntry.BilledAmount + Math.Round(TotalTaxAmt, 2);
        //    }
        //    else
        //        setCompanyRatio((SubTotalAmt - TotalTaxAmt), TotalTaxAmt, TotalDiscAmt);
        //    SetVoucherCompany(tVoucherEntry, dbTVoucherEntry);
        //    SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);

        //    double debit = 0;
        //    //Item Sales Ledger Details

        //    int ledgerNo = 0; long cnt = VoucherSrNo - 1, cntLedg = -1, cntTaxLedg = -1;
        //    //for (int k = 0; k < dtSaleLedger.Rows.Count; k++)
        //    //{
        //    cntLedg = -1;
        //    for (int j = 0; j < dtDetails.Rows.Count; j++)
        //    {
        //        if (cntLedg == -1) cntLedg = j;
        //        debit = debit + Convert.ToDouble(dtItemTax.Rows[j].ItemArray[2].ToString());
        //        ledgerNo = j;
        //    }
        //    if (debit > 0)
        //    {
        //        tVoucherDetails = new TVoucherDetails();
        //        tVoucherDetails.PkVoucherTrnNo = 0;
        //        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
        //        tVoucherDetails.SignCode = 2;
        //        tVoucherDetails.LedgerNo = Convert.ToInt64(dtItemTax.Rows[ledgerNo].ItemArray[1].ToString());
        //        tVoucherDetails.Debit = 0;
        //        tVoucherDetails.CompanyNo = CompNo;
        //        tVoucherDetails.Credit = debit;
        //        tVoucherDetails.Narration = "";
        //        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
        //        debit = 0;
        //    }

        //    //Item Tax Details
        //    cnt = VoucherSrNo - 1;
        //    debit = 0;
        //    ledgerNo = 0;
        //    cntTaxLedg = -1;
        //    for (int j = 0; j < dtDetails.Rows.Count; j++)
        //    {
        //        if (cntTaxLedg == -1) cntTaxLedg = j;
        //        debit = debit + Convert.ToDouble(dtItemTax.Rows[j].ItemArray[3].ToString());
        //        ledgerNo = j;
        //    }
        //    if (debit > 0)
        //    {
        //        tVoucherDetails = new TVoucherDetails();
        //        tVoucherDetails.PkVoucherTrnNo = 0;
        //        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
        //        tVoucherDetails.SignCode = 2;
        //        tVoucherDetails.LedgerNo = Convert.ToInt64(dtItemTax.Rows[ledgerNo].ItemArray[0].ToString());
        //        tVoucherDetails.Debit = 0;
        //        tVoucherDetails.CompanyNo = CompNo;
        //        tVoucherDetails.Credit = debit;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
        //        tVoucherDetails.Narration = "";
        //        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
        //        debit = 0;
        //    }

        //    //For Discount Ledger 1 %
        //    if (Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString()) != 0)
        //    {
        //        tVoucherDetails = new TVoucherDetails();
        //        tVoucherDetails.PkVoucherTrnNo = 0;
        //        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
        //        tVoucherDetails.SignCode = 1;
        //        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
        //        tVoucherDetails.Debit = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString());
        //        tVoucherDetails.Credit = 0;
        //        tVoucherDetails.Narration = "";
        //        tVoucherDetails.SrNo = Others.Discount1;
        //        tVoucherDetails.CompanyNo = CompNo;
        //        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
        //    }

        //    //=========Debit Entrys=========================
        //    //For Charges Rupees 1
        //    if (Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges1Amt].ToString()) != 0)
        //    {
        //        tVoucherDetails = new TVoucherDetails();
        //        tVoucherDetails.PkVoucherTrnNo = 0;
        //        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
        //        tVoucherDetails.SignCode = 2;
        //        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1));
        //        tVoucherDetails.Debit = 0;
        //        tVoucherDetails.Credit = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges1Amt].ToString());
        //        tVoucherDetails.Narration = "";
        //        tVoucherDetails.SrNo = Others.Charges1;
        //        tVoucherDetails.CompanyNo = CompNo;
        //        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
        //    }

        //    ////For Charges Rupees 2
        //    //if (Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges2Amt].ToString()) != 0)
        //    //{
        //    //    tVoucherDetails = new TVoucherDetails();
        //    //    tVoucherDetails.PkVoucherTrnNo = 0;
        //    //    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
        //    //    tVoucherDetails.SignCode = 2;
        //    //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1));
        //    //    tVoucherDetails.Debit = 0;
        //    //    tVoucherDetails.Credit = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges2Amt].ToString());
        //    //    tVoucherDetails.Narration = "";
        //    //    tVoucherDetails.SrNo = Others.Charges2;
        //    //    tVoucherDetails.CompanyNo = CompNo;
        //    //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
        //    //}

        //    //For Round Off Acc Ledger
        //    double TotAmt = Math.Round(tVoucherEntry.BilledAmount, MidpointRounding.AwayFromZero);
        //    double RoundOff = TotAmt - tVoucherEntry.BilledAmount;
        //    if (RoundOff != 0)
        //    {
        //        tVoucherDetails = new TVoucherDetails();
        //        tVoucherDetails.PkVoucherTrnNo = 0;
        //        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
        //        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc));
        //        if (RoundOff >= 0)
        //        {
        //            tVoucherDetails.SignCode = 2;
        //            tVoucherDetails.Debit = 0;
        //            tVoucherDetails.Credit = RoundOff;
        //        }
        //        else
        //        {
        //            tVoucherDetails.SignCode = 1;
        //            tVoucherDetails.Debit = Math.Abs(RoundOff);
        //            tVoucherDetails.Credit = 0;
        //        }
        //        tVoucherDetails.Narration = "";
        //        tVoucherDetails.SrNo = Others.RoundOff;
        //        tVoucherDetails.CompanyNo = CompNo;
        //        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
        //    }

        //    long tempid = dbTVoucherEntry.ExecuteNonQueryStatements();

        //    if (tVoucherEntry.PayTypeNo == 2)
        //    {
        //        SaveReceipt(tempid);
        //    }
        //    else
        //    {
        //        dbTVoucherEntry.UpdateReceiptDetails(ID, CompNo, Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString()), Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.LedgerNo].ToString()), VchType.Sales);
        //    }
        //    return tempid;
        //}

        private long SaveVouchers(long vchCode)
        {
            dtZeroTax = ObjFunction.GetDataView("Select TaxLedgerNo, SalesLedgerNo, Percentage, IsNull(pksrno,0) AS PkSrNo From MItemTaxSetting  Where SalesLedgerNo in(Select LedgerNo  From MLedger Where GroupNo=10) AND Percentage = 0").Table;
            long ID = 0;
            SubTotalAmt = 0; TotalTaxAmt = 0; TotalDiscAmt = 0;
            CompNo = DBGetVal.FirmNo;
            TVoucherEntry tVoucher = new TVoucherEntry();


            DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
            TVoucherEntry tVoucherEntry = new TVoucherEntry();
            TVoucherDetails tVoucherDetails = new TVoucherDetails();
            TVoucherPayTypeDetails tVchPayTypeDetails = new TVoucherPayTypeDetails();
            TVoucherRefDetails tVchRefDtls = new TVoucherRefDetails();
            TVoucherChqCreditDetails tVchChqCredit = new TVoucherChqCreditDetails();
            TStock tStock = new TStock();
            TStockGodown tStockGodown = new TStockGodown();
            DataTable dtItemTax = new DataTable();
            dtItemTax.Columns.Add("");
            dtItemTax.Columns.Add("");
            dtItemTax.Columns.Add("");
            dtItemTax.Columns.Add("");

            CalculateDetails();

            int VoucherSrNo = 1;

            tVoucherEntry = new TVoucherEntry();
            tVoucherEntry.PkVoucherNo = 0;
            tVoucherEntry.VoucherTypeCode = VchType.Sales;
            tVoucherEntry.VoucherUserNo = 0;
            tVoucherEntry.VoucherDate = Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString());
            tVoucherEntry.VoucherTime = Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherTime].ToString());
            tVoucherEntry.Narration = "Sales Bill";
            tVoucherEntry.Reference = dtHeader.Rows[0].ItemArray[HeaderColIndex.ServerOtherVoucherNo].ToString();
            tVoucherEntry.ChequeNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.ChequeNo].ToString());
            tVoucherEntry.ClearingDate = Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.ChequeDate].ToString());
            tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == false)
                tVoucherEntry.BilledAmount = SubTotalAmt + Math.Round(TotalTaxAmt) + Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges1Amt].ToString()) - Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString());
            else
                tVoucherEntry.BilledAmount = SubTotalAmt + Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges1Amt].ToString()) - Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString()); ;

            tVoucherEntry.ChallanNo = "";
            tVoucherEntry.Remark = "";
            tVoucherEntry.MacNo = 0;
            tVoucherEntry.PayTypeNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.PayType].ToString());
            tVoucherEntry.RateTypeNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.RateType].ToString());
            tVoucherEntry.TaxTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_TaxType));
            tVoucherEntry.OrderType = 1;
            tVoucherEntry.DiscPercent = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscPercent].ToString());
            tVoucherEntry.DiscAmt = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString());
            tVoucherEntry.MixMode = 0;
            tVoucherEntry.IsItemLevelDisc = Convert.ToBoolean(dtHeader.Rows[0].ItemArray[HeaderColIndex.IsItemLevelDisc].ToString());
            tVoucherEntry.IsFooterLevelDisc = Convert.ToBoolean(dtHeader.Rows[0].ItemArray[HeaderColIndex.IsFooterLevelDisc].ToString());
            tVoucherEntry.UserID = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.UserID].ToString());
            tVoucherEntry.UserDate = tVoucherEntry.VoucherDate;
            tVoucherEntry.LRNo = "";

            //tVoucherEntry.YadiVoucherNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.FkYadiVoucherNo].ToString());
            dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

            tVoucherDetails = new TVoucherDetails();
            tVoucherDetails.PkVoucherTrnNo = 0;
            tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
            tVoucherDetails.SignCode = 1;

            tVoucherDetails.LedgerNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.LedgerNo].ToString());
            tVoucherDetails.Debit = tVoucherEntry.BilledAmount;
            tVoucherDetails.Credit = 0;
            tVoucherDetails.Narration = "";
            tVoucherDetails.CompanyNo = CompNo;
            tVoucherDetails.SrNo = Others.Party;

            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

            if (tVoucherEntry.PayTypeNo == 3)//For Credit Bill
            {
                tVchRefDtls = new TVoucherRefDetails();
                tVchRefDtls.PkRefTrnNo = ObjQry.ReturnLong("Select PKRefTrnNo From TVoucherRefDetails Where FKVoucherTrnNo=" + tVoucherDetails.PkVoucherTrnNo + " ANd CompanyNo=" + CompNo + " ", CommonFunctions.ConStr);
                tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                tVchRefDtls.TypeOfRef = 3;
                tVchRefDtls.RefNo = 0;
                tVchRefDtls.DueDays = 0;
                tVchRefDtls.DueDate = DateTime.Now.Date;
                tVchRefDtls.Amount = tVoucherEntry.BilledAmount;
                tVchRefDtls.SignCode = 1;
                tVchRefDtls.UserID = DBGetVal.UserID;
                tVchRefDtls.UserDate = DateTime.Now.Date;
                tVchRefDtls.CompanyNo = CompNo;
                dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);
            }


            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {

                tStock = new TStock();
                tStock.PkStockTrnNo = 0;
                tStock.ItemNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.ItemNo].ToString());
                tStock.FkUomNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.UOM].ToString());

                tStock.GroupNo = 0;
                tStock.FkVoucherSrNo = VoucherSrNo;
                tStock.TrnCode = 2;
                tStock.Quantity = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString());
                tStock.BilledQuantity = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Quantity].ToString()) * Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.StockConversion].ToString());
                tStock.Rate = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Rate].ToString());
                tStock.Amount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Amount].ToString());
                tStock.SGSTPercentage = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.TaxPerce].ToString());
                tStock.SGSTAmount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.SGSTAmount].ToString());
                tStock.DiscPercentage = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.DsicPer].ToString());
                tStock.DiscAmount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc1Amount].ToString());
                tStock.DiscRupees = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc2Amount].ToString());
                tStock.DiscPercentage2 = 0;
                tStock.DiscAmount2 = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.Disc3Amount].ToString());
                tStock.DiscRupees2 = 0;
                tStock.NetRate = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.NetRate].ToString());
                tStock.NetAmount = Convert.ToDouble(dtDetails.Rows[i].ItemArray[DataColIndex.NetAmount].ToString());
                tStock.FkStockBarCodeNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.FkStockBarcodeNo].ToString());

                tStock.FkRateSettingNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.RateSettingNo].ToString());
                tStock.FkItemTaxInfo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.FkItemTaxSettingNo].ToString());
                tStock.FreeQty = 0;
                tStock.FreeUOMNo = 1;
                tStock.UserID = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.UserID].ToString());
                tStock.UserDate = DateTime.Now.Date;
                tStock.CompanyNo = CompNo;
                tStock.DisplayItemName = "";
                tStock.LandedRate = 0;
                // tStock.MfgCompNo = Convert.ToInt64(dtDetails.Rows[i].ItemArray[DataColIndex.MFGCompNo].ToString());
                dbTVoucherEntry.AddTStock(tStock);

                TOtherStockDetails tOtherStkDtls = new TOtherStockDetails();
                tOtherStkDtls.PkSrNo = 0;
                tOtherStkDtls.FKOtherVoucherNo = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.YadiServerNo].ToString());
                tOtherStkDtls.FKOtherStockTrnNo = Convert.ToInt64(dtDetails.Rows[0].ItemArray[DataColIndex.YadiSrNo].ToString());
                tOtherStkDtls.ItemNo = tStock.ItemNo;
                tOtherStkDtls.Quantity = tStock.Quantity;
                tOtherStkDtls.UserID = DBGetVal.UserID;
                tOtherStkDtls.UserDate = DBGetVal.ServerTime.Date;
                tOtherStkDtls.CompanyNo = DBGetVal.FirmNo;
                dbTVoucherEntry.AddTOtherStockDetails(tOtherStkDtls);


                tStockGodown = new TStockGodown();
                tStockGodown.PKStockGodownNo = 0;
                tStockGodown.ItemNo = tStock.ItemNo;
                tStockGodown.GodownNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation));
                tStockGodown.Qty = tStock.Quantity;
                tStockGodown.ActualQty = tStock.BilledQuantity;
                tStockGodown.UserID = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.UserID].ToString());
                tStockGodown.UserDate = DateTime.Now.Date;

                dbTVoucherEntry.AddTStockGodown(tStockGodown);

                DataRow dr1 = dtItemTax.NewRow();
                dr1[0] = dtDetails.Rows[i].ItemArray[DataColIndex.LedgerTaxNo].ToString();
                dr1[1] = dtDetails.Rows[i].ItemArray[DataColIndex.SaleTaxNo].ToString();
                dr1[2] = tStock.NetAmount.ToString();
                dr1[3] = tStock.SGSTAmount.ToString();
                dtItemTax.Rows.Add(dr1);

            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == false)
            {
                setCompanyRatio(SubTotalAmt, TotalTaxAmt, TotalDiscAmt);
            }
            else
                setCompanyRatio((SubTotalAmt - TotalTaxAmt), TotalTaxAmt, TotalDiscAmt);
            SetVoucherCompany(tVoucherEntry, dbTVoucherEntry);
            SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);

            double debit = 0;




            //Item Sales Ledger Details
            DataTable dtSaleLedger = new DataTable();
            bool TempFlag = false;
            dtSaleLedger.Columns.Add();
            dtSaleLedger.Columns.Add();
            DataRow dr = dtSaleLedger.NewRow();
            dr[0] = Convert.ToInt64(dtDetails.Rows[0].ItemArray[DataColIndex.SaleTaxNo].ToString());
            dr[1] = 1;
            dtSaleLedger.Rows.Add(dr);
            for (int k = 1; k < dtDetails.Rows.Count; k++)
            {
                for (int i = 0; i < dtSaleLedger.Rows.Count; i++)
                {
                    if (Convert.ToInt64(dtDetails.Rows[k].ItemArray[DataColIndex.SaleTaxNo].ToString()) != Convert.ToInt64(dtSaleLedger.Rows[i].ItemArray[0].ToString()))
                    {
                        TempFlag = true;
                    }
                    else
                    {
                        TempFlag = false;
                        break;
                    }
                }
                if (TempFlag == true)
                {
                    dr = dtSaleLedger.NewRow();
                    dr[0] = Convert.ToInt64(dtDetails.Rows[k].ItemArray[DataColIndex.SaleTaxNo].ToString());
                    dr[1] = 1;
                    dtSaleLedger.Rows.Add(dr);
                }
            }

            int ledgerNo = 0; int cnt = VoucherSrNo - 1, cntLedg = -1, cntTaxLedg = -1;
            for (int k = 0; k < dtSaleLedger.Rows.Count; k++)
            {
                cntLedg = -1;
                for (int j = 0; j < dtDetails.Rows.Count; j++)
                {
                    if (Convert.ToInt64(dtDetails.Rows[j].ItemArray[DataColIndex.SaleTaxNo].ToString()) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[0].ToString()))
                    {
                        if (cntLedg == -1) cntLedg = j;
                        debit = debit + Convert.ToDouble(dtDetails.Rows[j].ItemArray[DataColIndex.NetAmount].ToString());
                        ledgerNo = j;
                    }

                }
                if (debit > 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(dtDetails.Rows[ledgerNo].ItemArray[DataColIndex.SaleTaxNo].ToString());
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.CompanyNo = 1;
                    tVoucherDetails.Credit = debit;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                    tVoucherDetails.Narration = "";
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
                    debit = 0;
                }
            }



            //Item Tax Details
            DataTable dtTAxLedger = new DataTable();
            TempFlag = false;
            dtTAxLedger.Columns.Add();
            dtTAxLedger.Columns.Add();
            dr = dtTAxLedger.NewRow();
            dr[0] = Convert.ToInt64(dtDetails.Rows[0].ItemArray[DataColIndex.LedgerTaxNo].ToString());
            dr[1] = 1;
            dtTAxLedger.Rows.Add(dr);
            for (int k = 1; k < dtDetails.Rows.Count; k++)
            {
                TempFlag = false;
                for (int i = 0; i < dtTAxLedger.Rows.Count; i++)
                {
                    if (Convert.ToInt64(dtDetails.Rows[k].ItemArray[DataColIndex.LedgerTaxNo].ToString()) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()))
                    {
                        TempFlag = true;
                    }
                    else
                    {
                        TempFlag = false;
                        break;
                    }
                }
                if (TempFlag == true)
                {
                    dr = dtTAxLedger.NewRow();
                    dr[0] = Convert.ToInt64(dtDetails.Rows[k].ItemArray[DataColIndex.LedgerTaxNo].ToString());
                    dr[1] = 1;
                    dtTAxLedger.Rows.Add(dr);
                }
            }

            cnt = VoucherSrNo - 1;
            debit = 0;
            ledgerNo = 0;
            for (int k = 0; k < dtTAxLedger.Rows.Count; k++)
            {
                cntTaxLedg = -1;
                for (int j = 0; j < dtDetails.Rows.Count; j++)
                {
                    if (Convert.ToInt64(dtDetails.Rows[j].ItemArray[DataColIndex.LedgerTaxNo].ToString()) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()))
                    {
                        if (cntTaxLedg == -1) cntTaxLedg = j;
                        debit = debit + Convert.ToDouble(dtDetails.Rows[j].ItemArray[DataColIndex.SGSTAmount].ToString());
                        ledgerNo = j;
                    }
                }
                if (debit > 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(dtDetails.Rows[ledgerNo].ItemArray[DataColIndex.LedgerTaxNo].ToString());
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.CompanyNo = 1;
                    tVoucherDetails.Credit = debit;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                    tVoucherDetails.Narration = "";
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
                    debit = 0;
                }
            }

            //For Discount Ledger 1 %
            if (Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString()) != 0)
            {
                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                tVoucherDetails.SignCode = 1;
                tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                tVoucherDetails.Debit = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.DiscAmount].ToString());
                tVoucherDetails.Credit = 0;
                tVoucherDetails.Narration = "";
                tVoucherDetails.SrNo = Others.Discount1;
                tVoucherDetails.CompanyNo = CompNo;
                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
            }

            //=========Debit Entrys=========================
            //For Charges Rupees 1
            if (Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges1Amt].ToString()) != 0)
            {
                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                tVoucherDetails.SignCode = 2;
                tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1));
                tVoucherDetails.Debit = 0;
                tVoucherDetails.Credit = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges1Amt].ToString());
                tVoucherDetails.Narration = "";
                tVoucherDetails.SrNo = Others.Charges1;
                tVoucherDetails.CompanyNo = CompNo;
                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
            }

            ////For Charges Rupees 2
            //if (Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges2Amt].ToString()) != 0)
            //{
            //    tVoucherDetails = new TVoucherDetails();
            //    tVoucherDetails.PkVoucherTrnNo = 0;
            //    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
            //    tVoucherDetails.SignCode = 2;
            //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1));
            //    tVoucherDetails.Debit = 0;
            //    tVoucherDetails.Credit = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.Charges2Amt].ToString());
            //    tVoucherDetails.Narration = "";
            //    tVoucherDetails.SrNo = Others.Charges2;
            //    tVoucherDetails.CompanyNo = CompNo;
            //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
            //}

            //For Round Off Acc Ledger
            double TotAmt = Math.Round(tVoucherEntry.BilledAmount, MidpointRounding.AwayFromZero);

            double RoundOff = 0;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == false)
                RoundOff = TotAmt - (SubTotalAmt + TotalTaxAmt + TotalDiscAmt);
            else
                RoundOff = TotAmt - (SubTotalAmt);

            RoundOff = Math.Round(RoundOff, 2);
            if (RoundOff != 0)
            {
                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc));
                if (RoundOff >= 0)
                {
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = RoundOff;
                }
                else
                {
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.Debit = Math.Abs(RoundOff);
                    tVoucherDetails.Credit = 0;
                }
                tVoucherDetails.Narration = "";
                tVoucherDetails.SrNo = Others.RoundOff;
                tVoucherDetails.CompanyNo = CompNo;
                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
            }
            dbTVoucherEntry.EffectStock();
            long tempid = dbTVoucherEntry.ExecuteNonQueryStatements();

            if (tVoucherEntry.PayTypeNo == 2)
            {
                SaveReceipt(tempid);
            }
            else
            {
                dbTVoucherEntry.UpdateReceiptDetails(ID, CompNo, Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString()), Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.LedgerNo].ToString()), VchType.Sales);
            }
            return tempid;
        }

        private void setCompanyRatio(double SubTotal, double TotalTaxAmt, double TotalDisc)
        {
            try
            {
                DataTable dtTemp = new DataTable();
                bool TempFlag = false;
                dtTemp.Columns.Add();
                dtTemp.Columns.Add();
                DataRow dr = dtTemp.NewRow();
                dr[0] = DBGetVal.FirmNo;
                dr[1] = Convert.ToInt64(dtDetails.Rows[0][DataColIndex.MFGCompNo].ToString());
                dtTemp.Rows.Add(dr);
                for (int k = 0; k < dtDetails.Rows.Count; k++)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        //if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTemp.Rows[i].ItemArray[0].ToString()) &&
                        if (Convert.ToInt64(dtDetails.Rows[k][DataColIndex.MFGCompNo].ToString()) != Convert.ToInt64(dtTemp.Rows[i].ItemArray[1].ToString()))
                        {
                            TempFlag = true;
                        }
                        else
                        {
                            TempFlag = false;
                            break;
                        }
                    }
                    if (TempFlag == true)
                    {
                        dr = dtTemp.NewRow();
                        dr[0] = DBGetVal.FirmNo;
                        dr[1] = Convert.ToInt64(dtDetails.Rows[k][DataColIndex.MFGCompNo].ToString());
                        dtTemp.Rows.Add(dr);
                    }
                }

                dtCompRatio = new DataTable();
                dtCompRatio.Columns.Add();
                dtCompRatio.Columns.Add();
                dtCompRatio.Columns.Add();
                double debit = 0;
                for (int k = 0; k < dtTemp.Rows.Count; k++)
                {
                    for (int j = 0; j < dtDetails.Rows.Count; j++)
                    {
                        //if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTemp.Rows[k].ItemArray[0].ToString()) &&
                        if (Convert.ToInt64(dtDetails.Rows[j][DataColIndex.MFGCompNo].ToString()) == Convert.ToInt64(dtTemp.Rows[k].ItemArray[1].ToString()))
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == false)
                                debit = debit + Convert.ToDouble(dtDetails.Rows[j][DataColIndex.Amount].ToString()) + Convert.ToDouble(dtDetails.Rows[j][DataColIndex.SGSTAmount].ToString());
                            else
                                debit = debit + Convert.ToDouble(dtDetails.Rows[j][DataColIndex.Amount].ToString());

                        }
                    }

                    DataRow dr1 = dtCompRatio.NewRow();
                    dr1[0] = Convert.ToInt64(dtTemp.Rows[k].ItemArray[0].ToString());
                    dr1[1] = Convert.ToInt64(dtTemp.Rows[k].ItemArray[1].ToString());
                    if (debit > 0)
                        dr1[2] = (debit * 10) / ((Convert.ToDouble(SubTotal) + Convert.ToDouble(TotalTaxAmt)) - Convert.ToDouble(TotalDisc)); //((Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtTotalAnotherDisc.Text) - Convert.ToDouble(txtRoundOff.Text) - Convert.ToDouble(txtTotalChrgs.Text)) + Convert.ToDouble(txtTotalTax.Text));
                    else dr1[2] = 1;
                    dtCompRatio.Rows.Add(dr1);
                    debit = 0;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private bool SaveReceipt(long SalesID)
        {
            DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
            TVoucherEntry tVoucherEntry = new TVoucherEntry();
            TVoucherDetails tVoucherDetails = new TVoucherDetails();
            TVoucherPayTypeDetails tVchPayTypeDetails = new TVoucherPayTypeDetails();
            DataTable dtPayType = new DataTable();
            int cntPayType = 1;
            long tempid = -1, ReceiptID = 0, VoucherUserNo = 0; double totamt = 0, amt = 0;
            long LedgNo = 0;

            ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                           " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherEntry.VoucherDate ='" + Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString()).ToString("dd-MMM-yyyy") + "') AND (TVoucherDetails.LedgerNo = " + PartyLedgerNo + ") AND " +
                           " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=2) AND TVoucherEntry.CompanyNo=" + CompNo + "", CommonFunctions.ConStr);



            DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
            VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);

            if (dtReceipt.Rows.Count > 0)
                amt = Convert.ToDouble(dtReceipt.Rows[0].ItemArray[3]);
            int VoucherSrNo = 1;
            dbTVoucherEntry = new DBTVaucherEntry();
            //Voucher Header Entry
            tVoucherEntry = new TVoucherEntry();
            tVoucherEntry.PkVoucherNo = ReceiptID;
            tVoucherEntry.VoucherTypeCode = VchType.Receipt;
            tVoucherEntry.VoucherUserNo = VoucherUserNo;
            tVoucherEntry.VoucherDate = Convert.ToDateTime(Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString()).ToString("dd-MMM-yyyy")); Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy"));// Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherDate].ToString());
            tVoucherEntry.VoucherTime = DateTime.Now;// Convert.ToDateTime(dtHeader.Rows[0].ItemArray[HeaderColIndex.VoucherTime].ToString());
            tVoucherEntry.Narration = "Receipt Bill";
            tVoucherEntry.Reference = "";
            tVoucherEntry.ChequeNo = 0;
            tVoucherEntry.ClearingDate = Convert.ToDateTime("01-Jan-1900");
            tVoucherEntry.CompanyNo = CompNo;// Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.CompanyNo].ToString());
            tVoucherEntry.BilledAmount = amt + Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.BilledAmount].ToString());
            tVoucherEntry.ChallanNo = "";
            tVoucherEntry.Remark = "";
            tVoucherEntry.MacNo = 0;
            tVoucherEntry.PayTypeNo = 2;
            tVoucherEntry.RateTypeNo = 0;
            tVoucherEntry.TaxTypeNo = 0;
            tVoucherEntry.UserID = Convert.ToInt64(dtHeader.Rows[0].ItemArray[HeaderColIndex.UserID].ToString()); ;
            tVoucherEntry.UserDate = DateTime.Now.Date;
            dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); SetVoucherCompany(tVoucherEntry, dbTVoucherEntry);

            DataTable dtVoucherDetails = new DataTable();
            totamt = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.BilledAmount].ToString());
            if (ReceiptID != 0)
            {
                dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where FkVoucherNo=" + ReceiptID + " order by VoucherSrNo").Table;
                dtPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo From TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=2 order by PKVoucherPayTypeNo").Table;
            }

            for (int i = 0; i < dtCompRatio.Rows.Count; i++)
            {
                dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where LedgerNo=" + PartyLedgerNo + " AND  FkVoucherNo=" + ReceiptID + " AND CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " order by VoucherSrNo").Table;

                //For Party Ledger
                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo;
                tVoucherDetails.SignCode = 1;
                tVoucherDetails.LedgerNo = PartyLedgerNo;// Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC));
                tVoucherDetails.Debit = 0;

                double Newval = 0;
                for (int k = 0; k < dtDetails.Rows.Count; k++)
                {
                    //if (Convert.ToInt64(dtDetails.Rows[k].ItemArray[DataColIndex.StockCompanyNo].ToString()) == Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()))
                    //{

                    Newval = Newval + Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString());

                    //}
                }
                tVoucherDetails.Credit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString()) : 0) + totamt;

                VoucherSrNo += 1;
                tVoucherDetails.Narration = "";
                tVoucherDetails.SrNo = Others.Party;
                tVoucherDetails.CompanyNo = Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);
            }



            for (int i = 0; i < dtCompRatio.Rows.Count; i++)
            {
                LedgNo = ObjQry.ReturnLong("Select LedgerNo from MPayTypeLedger where PayTypeNo=2 ", CommonFunctions.ConStr);//AND CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + "
                dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where LedgerNo=" + LedgNo + " AND  FkVoucherNo=" + ReceiptID + " AND CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " order by VoucherSrNo").Table;
                //For PayType Details
                tVoucherDetails = new TVoucherDetails();
                //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo;
                tVoucherDetails.SignCode = 2;
                tVoucherDetails.LedgerNo = LedgNo;
                double Newval = 0;
                for (int k = 0; k < dtDetails.Rows.Count; k++)
                {
                    //if (Convert.ToInt64(dtDetails.Rows[k].ItemArray[DataColIndex.StockCompanyNo].ToString()) == Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()))
                    //{

                    Newval = Newval + Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString());

                    //}
                }


                tVoucherDetails.Debit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) : 0) + Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.BilledAmount].ToString());


                tVoucherDetails.Credit = 0;
                VoucherSrNo += 1;
                tVoucherDetails.Narration = "";
                tVoucherDetails.CompanyNo = Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);


                tVchPayTypeDetails = new TVoucherPayTypeDetails();
                tVchPayTypeDetails.PKVoucherPayTypeNo = (dtPayType.Rows.Count > cntPayType - 1) ? Convert.ToInt64(dtPayType.Rows[cntPayType - 1].ItemArray[0].ToString()) : 0; cntPayType += 1;
                tVchPayTypeDetails.FKSalesVoucherNo = SalesID;
                tVchPayTypeDetails.FKPayTypeNo = 2;
                tVchPayTypeDetails.Amount = Convert.ToDouble(dtHeader.Rows[0].ItemArray[HeaderColIndex.BilledAmount]);
                tVchPayTypeDetails.CompanyNo = CompNo;
                dbTVoucherEntry.AddTVoucherPayTypeDetails(tVchPayTypeDetails); SetVoucherDetailsCompany(tVoucherDetails, dbTVoucherEntry);


            }

            //Delete VoucherDetails
            DataTable dtDelPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo,TVoucherPayTypeDetails.CompanyNo,FKReceiptVoucherNo,Amount From TVoucherPayTypeDetails,TVoucherDetails Where TVoucherDetails.PkVoucherTrnNo=TVoucherPayTypeDetails.FKVoucherTrnNo AND FKSalesVoucherNo=" + SalesID + " AND TVoucherPayTypeDetails.CompanyNo=" + CompNo + "  order by PKVoucherPayTypeNo").Table;
            for (int k = 0; k < dtDelPayType.Rows.Count; k++)
            {
                if (dtPayType.Rows.Count > 0)
                {
                    if (dtPayType.Select("PKVoucherPayTypeNo=" + dtDelPayType.Rows[k].ItemArray[0].ToString())[0][0].ToString() != dtDelPayType.Rows[k].ItemArray[0].ToString())
                    {
                        tVchPayTypeDetails = new TVoucherPayTypeDetails();
                        tVchPayTypeDetails.PKVoucherPayTypeNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[0].ToString());
                        dbTVoucherEntry.DeleteTVoucherPayTypeDetails(tVchPayTypeDetails);
                    }
                }
                else
                {
                    tVchPayTypeDetails = new TVoucherPayTypeDetails();
                    tVchPayTypeDetails.PKVoucherPayTypeNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[0].ToString());
                    dbTVoucherEntry.DeleteTVoucherPayTypeDetails(tVchPayTypeDetails);
                }
                DataTable dtUpdateVoucher = ObjFunction.GetDataView("Select PKVoucherTrnNo,Debit,Credit From TVoucherDetails Where FKVoucherNo=" + dtDelPayType.Rows[k].ItemArray[2].ToString() + " AND CompanyNo=" + dtDelPayType.Rows[k].ItemArray[1].ToString() + "").Table;
                totamt = 0;
                bool alllowdel = false;
                for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                {
                    double DrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[1].ToString());
                    double CrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[2].ToString());
                    if (DrAmt > 0) DrAmt = DrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                    if (CrAmt > 0) CrAmt = CrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                    dbTVoucherEntry.UpdateVoucherDetails(DrAmt, CrAmt, Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString()));
                    totamt = totamt + DrAmt + CrAmt;
                    alllowdel = true;
                }
                if (totamt == 0 && alllowdel == true)
                {
                    for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                    {
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString());
                        dbTVoucherEntry.DeleteTVoucherDetails(tVoucherDetails);

                        if (m == dtUpdateVoucher.Rows.Count - 1)
                        {
                            if (ObjQry.ReturnLong("Select Count(*) From TVoucherDetails Where FKVoucherNo=" + Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[2].ToString()) + " AND CompanyNo=" + CompNo + "", CommonFunctions.ConStr) >= dtUpdateVoucher.Rows.Count)
                            {
                                tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[2].ToString());
                                dbTVoucherEntry.DeleteTVoucherEntry1(tVoucherEntry);
                            }
                        }
                    }
                }
            }

            tempid = dbTVoucherEntry.ExecuteNonQueryStatements();



            if (tempid != 0)
                return true;
            else
                return false;
        }

        private void SetVoucherCompany(TVoucherEntry tVouch, DBTVaucherEntry dbTVoucherEntry)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)))
            {
                for (int j = 0; j < dtCompRatio.Rows.Count; j++)
                {
                    tVoucherEntryComp = new TVoucherEntryCompany();
                    tVoucherEntryComp.PKVoucherCompanyNo = ObjQry.ReturnLong("Select PKVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + tVouch.PkVoucherNo + " AND MfgCompNo=" + dtCompRatio.Rows[j].ItemArray[1].ToString() + "", CommonFunctions.ConStr);
                    tVoucherEntryComp.VoucherTypeCode = tVouch.VoucherTypeCode;
                    tVoucherEntryComp.VoucherUserNo = 0;
                    tVoucherEntryComp.BilledAmount = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.BilledAmount) / 10);
                    tVoucherEntryComp.CompanyNo = tVouch.CompanyNo;
                    tVoucherEntryComp.MfgCompNo = Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                    tVoucherEntryComp.PayTypeNo = tVouch.PayTypeNo;
                    tVoucherEntryComp.TaxTypeNo = tVouch.TaxTypeNo;

                    tVoucherEntryComp.UserID = tVouch.UserID;
                    tVoucherEntryComp.UserDate = tVouch.UserDate;
                    dbTVoucherEntry.AddTVoucherEntryCompany(tVoucherEntryComp);
                }
            }
        }

        private void SetVoucherDetailsCompany(TVoucherDetails tVouch, DBTVaucherEntry dbTVoucherEntry)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)))
            {
                for (int j = 0; j < dtCompRatio.Rows.Count; j++)
                {
                    tVoucherDetailsComp = new TVoucherDetailsCompany();
                    tVoucherDetailsComp.PkVoucherCompTrnNo = ObjQry.ReturnLong("Select PkVoucherCompTrnNo From TVoucherDetailsCompany Where FKVoucherTrnNo=" + tVouch.PkVoucherTrnNo + " AND MfgCompNo=" + dtCompRatio.Rows[j].ItemArray[1].ToString() + "", CommonFunctions.ConStr);
                    tVoucherDetailsComp.VoucherSrNo = tVouch.VoucherSrNo;
                    tVoucherDetailsComp.SignCode = tVouch.SignCode;
                    tVoucherDetailsComp.LedgerNo = tVouch.LedgerNo;
                    if (tVouch.Debit != 0)
                        tVoucherDetailsComp.Debit = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Debit) / 10);
                    else tVoucherDetailsComp.Debit = 0;
                    if (tVouch.Credit != 0)
                        tVoucherDetailsComp.Credit = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Credit) / 10);
                    else tVoucherDetailsComp.Credit = 0;

                    tVoucherDetailsComp.SrNo = tVouch.SrNo;
                    tVoucherDetailsComp.CompanyNo = tVouch.CompanyNo;
                    tVoucherDetailsComp.Narration = tVouch.Narration;
                    tVoucherDetailsComp.MfgCompNo = Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                    dbTVoucherEntry.AddTVoucherDetailsCompany(tVoucherDetailsComp);
                }
            }
        }
        #endregion

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

        public bool UpdateTransporterDetails(TVoucherEntry tVoucherEntry)
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

        public bool UpdateTransporterStatus(TVoucherEntry tVoucherEntry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set TransPayType=@TransPayType ,StatusNo=2 where PkVoucherNo=@PkVoucherNo";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tVoucherEntry.PkVoucherNo);

            cmd.Parameters.AddWithValue("@TransPayType", tVoucherEntry.TransPayType);

            commandcollection.Add(cmd);
            return true;
        }

        public bool SaveTransporterDetails(TVoucherEntry tVoucherEntry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TvoucherEntry set TransporterCode=@TransporterCode,TransNoOfItems=@TransNoOfItems,TransportMode=@TransportMode,LRNo=@LRNo,TransPayType=@TransPayType,StatusNo=2 where PkVoucherNo=@PkVoucherNo";

            cmd.Parameters.AddWithValue("@PkVoucherNo", tVoucherEntry.PkVoucherNo);

            cmd.Parameters.AddWithValue("@TransporterCode", tVoucherEntry.TransporterCode);

            cmd.Parameters.AddWithValue("@LRNo", tVoucherEntry.LRNo);

            cmd.Parameters.AddWithValue("@TransportMode", tVoucherEntry.TransportMode);

            cmd.Parameters.AddWithValue("@TransNoOfItems", tVoucherEntry.TransNoOfItems);

            cmd.Parameters.AddWithValue("@TransPayType", tVoucherEntry.TransPayType);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                tVoucherEntry.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool AddMLedgerRateSetting(MLedgerRateSetting mledgerratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLedgerRateSetting";

            cmd.Parameters.AddWithValue("@PkSrNo", mledgerratesetting.PkSrNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mledgerratesetting.LedgerNo);

            cmd.Parameters.AddWithValue("@ItemNo", mledgerratesetting.ItemNo);

            cmd.Parameters.AddWithValue("@UOMNo", mledgerratesetting.UOMNo);

            cmd.Parameters.AddWithValue("@Rate", mledgerratesetting.Rate);

            cmd.Parameters.AddWithValue("@MRP", mledgerratesetting.MRP);

            cmd.Parameters.AddWithValue("@DiscPercentage", mledgerratesetting.DiscPercentage);

            cmd.Parameters.AddWithValue("@DiscAmount", mledgerratesetting.DiscAmount);

            cmd.Parameters.AddWithValue("@CompNo", mledgerratesetting.CompNo);

            cmd.Parameters.AddWithValue("@UserID", mledgerratesetting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mledgerratesetting.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMLedgerRateSetting(MLedgerRateSetting mledgerratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMLedgerRateSetting";

            cmd.Parameters.AddWithValue("@PkSrNo", mledgerratesetting.PkSrNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mledgerratesetting.msg = ObjTrans.ErrorMessage;
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


        public bool AddTranspotorDetail(TranspotorDetail mTranspotorDetail)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTranspotorDetail";


            cmd.Parameters.AddWithValue("@PkTranspotorDetail", mTranspotorDetail.PkTranspotorDetail);
            //cmd.Parameters.AddWithValue("@FKEWayNo", mTranspotorDetail.FKEWayNo);
            // cmd.Parameters.AddWithValue("@FkVoucherNo", mTranspotorDetail.FkVoucherNo);
            cmd.Parameters.AddWithValue("@NoOfQty", mTranspotorDetail.NoOfQty);
            cmd.Parameters.AddWithValue("@BalancedQty", mTranspotorDetail.BalancedQty);
            cmd.Parameters.AddWithValue("@ReceivedQty", mTranspotorDetail.ReceivedQty);
            cmd.Parameters.AddWithValue("@msg", mTranspotorDetail.msg);
            cmd.Parameters.AddWithValue("@RemarkQty", mTranspotorDetail.RemarkQty);
            cmd.Parameters.AddWithValue("@UpatedDate", mTranspotorDetail.UpatedDate);
            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);
            //if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    mTranspotorDetail.msg = ObjTrans.ErrorMessage;
            //    return false;
            //}
            commandcollection.Add(cmd);
            return true;
        }
        public bool AddTranspotorDetailNew(TranspotorDetail mTranspotorDetail)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTranspotorDetail";


            cmd.Parameters.AddWithValue("@PkTranspotorDetail", mTranspotorDetail.PkTranspotorDetail);
            //cmd.Parameters.AddWithValue("@FKEWayNo", mTranspotorDetail.FKEWayNo);
            cmd.Parameters.AddWithValue("@FkVoucherNo", mTranspotorDetail.FkVoucherNo);
            cmd.Parameters.AddWithValue("@NoOfQty", mTranspotorDetail.NoOfQty);
            cmd.Parameters.AddWithValue("@BalancedQty", mTranspotorDetail.BalancedQty);
            cmd.Parameters.AddWithValue("@ReceivedQty", mTranspotorDetail.ReceivedQty);
            cmd.Parameters.AddWithValue("@msg", mTranspotorDetail.msg);
            cmd.Parameters.AddWithValue("@UpatedDate", mTranspotorDetail.UpatedDate);
            cmd.Parameters.AddWithValue("@RemarkQty", mTranspotorDetail.RemarkQty);
            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            //commandcollection.Add(cmd);
            //return true;

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mTranspotorDetail.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool AddTVoucherJournalEntry(TVoucherJournalEntry tvoucherjournalentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddTVoucherJournalEntry";

            cmd.Parameters.AddWithValue("@PKSrNo", tvoucherjournalentry.PKSrNo);

            cmd.Parameters.AddWithValue("@FKVoucherNo", tvoucherjournalentry.FKVoucherNo);

            //cmd.Parameters.AddWithValue("@FKRecieptVoucherNo", tvoucherjournalentry.FKJournalVoucherNo);

            cmd.Parameters.AddWithValue("@FKJournalVoucherNo", tvoucherjournalentry.FKJournalVoucherNo);

            cmd.Parameters.AddWithValue("@LedgerNo", tvoucherjournalentry.LedgerNo);

            cmd.Parameters.AddWithValue("@Amount", tvoucherjournalentry.Amount);

            cmd.Parameters.AddWithValue("@JVAmount", tvoucherjournalentry.JVAmount);

            cmd.Parameters.AddWithValue("@CompanyNo", tvoucherjournalentry.CompanyNo);

            cmd.Parameters.AddWithValue("@StatusNo", tvoucherjournalentry.StatusNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteTVoucherJournalEntry(TVoucherJournalEntry tvoucherjournalentry)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteTVoucherJournalEntry";

            cmd.Parameters.AddWithValue("@PKSrNo", tvoucherjournalentry.PKSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateDeliveryChallanDetails(string DCNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update TDeliveryChallan set FKVoucherNo=@FKVoucherNo Where PKVoucherNo in(" + DCNo + ")";

            //cmd.Parameters.AddWithValue("@DCNo", DCNo);

            commandcollection.Add(cmd);
            return true;
        }
    }

    /// <summary>
    /// This Class use for TVoucherEntry
    /// </summary>
    public class TVoucherEntry
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
        public long PkVoucherNo
        {
            get { return mPkVoucherNo; }
            set { mPkVoucherNo = value; }
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
    /// <summary>
    /// This Class use for TVoucherDetails
    /// </summary>
    public class TVoucherDetails
    {
        private long mPkVoucherTrnNo;
        private long mFkVoucherNo;
        private long mVoucherSrNo;
        private long mSignCode;
        private long mLedgerNo;
        private double mDebit;
        private double mCredit;
        private long mSrNo;
        private long mCompanyNo;
        private string mNarration;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkVoucherTrnNo
        /// </summary>
        public long PkVoucherTrnNo
        {
            get { return mPkVoucherTrnNo; }
            set { mPkVoucherTrnNo = value; }
        }
        /// <summary>
        /// This Properties use for FkVoucherNo
        /// </summary>
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        /// <summary>
        /// This Properties use for VoucherSrNo
        /// </summary>
        public long VoucherSrNo
        {
            get { return mVoucherSrNo; }
            set { mVoucherSrNo = value; }
        }
        /// <summary>
        /// This Properties use for SignCode
        /// </summary>
        public long SignCode
        {
            get { return mSignCode; }
            set { mSignCode = value; }
        }
        /// <summary>
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for Debit
        /// </summary>
        public double Debit
        {
            get { return mDebit; }
            set { mDebit = value; }
        }
        /// <summary>
        /// This Properties use for Credit
        /// </summary>
        public double Credit
        {
            get { return mCredit; }
            set { mCredit = value; }
        }
        /// <summary>
        /// This Properties use for SrNo
        /// </summary>
        public long SrNo
        {
            get { return mSrNo; }
            set { mSrNo = value; }
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
        /// This Properties use for Narration
        /// </summary>
        public string Narration
        {
            get { return mNarration; }
            set { mNarration = value; }
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
    /// This Class use for TStock
    /// </summary>
    public class TStock
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
        public bool mIType;
        private double mContainerCharges;
        private double mContainerChargesAmt;
        private double mPackagingChargesAmt;
        private double mHamali;

        public double Hamali
        {
            get { return mHamali; }
            set { mHamali = value; }
        }
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
        public double ContainerCharges
        {
            get { return mContainerCharges; }
            set { mContainerCharges = value; }
        }
        public double ContainerChargesAmt
        {
            get { return mContainerChargesAmt; }
            set { mContainerChargesAmt = value; }
        }
        public double PackagingChargesAmt
        {
            get { return mPackagingChargesAmt; }
            set { mPackagingChargesAmt = value; }
        }
        //public double LBTPerce
        //{
        //    get { return mLBTPerce; }
        //    set { mLBTPerce = value; }
        //}
        //public double LBTApplicableAmount
        //{
        //    get { return mLBTApplicableAmount; }
        //    set { mLBTApplicableAmount = value; }
        //}
        //public double LBTAmount
        //{
        //    get { return mLBTAmount; }
        //    set { mLBTAmount = value; }
        //}

        //public long MfgCompNo
        //{
        //    get { return mMfgCompNo; }
        //    set { mMfgCompNo = value; }
        //}

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
        public bool IType
        {
            get
            {
                return mIType;
            }
            set { mIType = value; }
        }
    }

    /// <summary>
    /// This Class use for TStockGodown
    /// </summary>
    public class TStockGodown
    {
        private long mPKStockGodownNo;
        private long mFKStockTrnNo;
        private long mItemNo;
        private long mGodownNo;
        private double mQty;
        private double mActualQty;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mCompanyNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PKStockGodownNo
        /// </summary>
        public long PKStockGodownNo
        {
            get { return mPKStockGodownNo; }
            set { mPKStockGodownNo = value; }
        }
        /// <summary>
        /// This Properties use for FKStockTrnNo
        /// </summary>
        public long FKStockTrnNo
        {
            get { return mFKStockTrnNo; }
            set { mFKStockTrnNo = value; }
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
        /// This Properties use for GodownNo
        /// </summary>
        public long GodownNo
        {
            get { return mGodownNo; }
            set { mGodownNo = value; }
        }
        /// <summary>
        /// This Properties use for Qty
        /// </summary>
        public double Qty
        {
            get { return mQty; }
            set { mQty = value; }
        }
        /// <summary>
        /// This Properties use for ActualQty
        /// </summary>
        public double ActualQty
        {
            get { return mActualQty; }
            set { mActualQty = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    /// <summary>
    /// This Class use for TVoucherRefDetails
    /// </summary>
    public class TVoucherRefDetails
    {
        private long mPkRefTrnNo;
        private long mFkVoucherTrnNo;
        private long mFkVoucherSrNo;
        private long mLedgerNo;
        private long mRefNo;
        private int mTypeOfRef;
        private long mDueDays;
        private DateTime mDueDate;
        private double mAmount;
        private double mDiscAmt;
        private long mSignCode;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedby;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PkRefTrnNo
        /// </summary>
        public long PkRefTrnNo
        {
            get { return mPkRefTrnNo; }
            set { mPkRefTrnNo = value; }
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
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for RefNo
        /// </summary>
        public long RefNo
        {
            get { return mRefNo; }
            set { mRefNo = value; }
        }
        /// <summary>
        /// This Properties use for TypeOfRef
        /// </summary>
        public int TypeOfRef
        {
            get { return mTypeOfRef; }
            set { mTypeOfRef = value; }
        }
        /// <summary>
        /// This Properties use for DueDays
        /// </summary>
        public long DueDays
        {
            get { return mDueDays; }
            set { mDueDays = value; }
        }
        /// <summary>
        /// This Properties use for DueDate
        /// </summary>
        public DateTime DueDate
        {
            get { return mDueDate; }
            set { mDueDate = value; }
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
        /// This Properties use for DiscAmt
        /// </summary>
        public double DiscAmt
        {
            get { return mDiscAmt; }
            set { mDiscAmt = value; }
        }
        /// <summary>
        /// This Properties use for SignCode
        /// </summary>
        public long SignCode
        {
            get { return mSignCode; }
            set { mSignCode = value; }
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
        /// This Properties use for Modifiedby
        /// </summary>
        public string Modifiedby
        {
            get { return mModifiedby; }
            set { mModifiedby = value; }
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
    /// This Class use for TVoucherPayTypeDetails
    /// </summary>

    public class TVoucherPayTypeDetails
    {
        private long mPKVoucherPayTypeNo;
        private long mFKSalesVoucherNo;
        private long mFKReceiptVoucherNo;
        private long mFKVoucherTrnNo;
        private long mFKPayTypeNo;
        private double mAmount;
        private long mCompanyNo;
        private int mStatusNo;
        private double mChargesPerce;
        private double mChargesAmount;
        private string Mmsg;

        /// <summary>
        /// This Properties use for PKVoucherPayTypeNo
        /// </summary>
        public long PKVoucherPayTypeNo
        {
            get { return mPKVoucherPayTypeNo; }
            set { mPKVoucherPayTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for FKSalesVoucherNo
        /// </summary>
        public long FKSalesVoucherNo
        {
            get { return mFKSalesVoucherNo; }
            set { mFKSalesVoucherNo = value; }
        }
        /// <summary>
        /// This Properties use for FKReceiptVoucherNo
        /// </summary>
        public long FKReceiptVoucherNo
        {
            get { return mFKReceiptVoucherNo; }
            set { mFKReceiptVoucherNo = value; }
        }
        /// <summary>
        /// This Properties use for FKVoucherTrnNo
        /// </summary>
        public long FKVoucherTrnNo
        {
            get { return mFKVoucherTrnNo; }
            set { mFKVoucherTrnNo = value; }
        }
        /// <summary>
        /// This Properties use for FKPayTypeNo
        /// </summary>
        public long FKPayTypeNo
        {
            get { return mFKPayTypeNo; }
            set { mFKPayTypeNo = value; }
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
        public double ChargesPerce
        {
            get { return mChargesPerce; }
            set { mChargesPerce = value; }
        }
        public double ChargesAmount
        {
            get { return mChargesAmount; }
            set { mChargesAmount = value; }
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
    /// This Class use for TVoucherChequeDetails
    /// </summary>

    public class TVoucherChequeDetails
    {
        private long mPkSrNo;
        private long mFkVoucherTrnNo;
        private DateTime mClearingDate;
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
        /// This Properties use for FkVoucherTrnNo
        /// </summary>
        public long FkVoucherTrnNo
        {
            get { return mFkVoucherTrnNo; }
            set { mFkVoucherTrnNo = value; }
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

    /// <summary>
    /// This Class use for TVoucherChqCreditDetails
    /// </summary>

    public class TVoucherChqCreditDetails
    {
        private long mPkSrNo;
        private long mFKVoucherNo;
        private long mFkVoucherTrnNo;
        private string mChequeNo;
        private DateTime mChequeDate;
        private long mBankNo;
        private long mBranchNo;
        private string mCreditCardNo;
        private double mAmount;
        private bool mIsPost;
        private long mPostFkVoucherNo;
        private long mPostFkVoucherTrnNo;
        private long mCompanyNo;
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
        /// This Properties use for ChequeNo
        /// </summary>
        public string ChequeNo
        {
            get { return mChequeNo; }
            set { mChequeNo = value; }
        }
        /// <summary>
        /// This Properties use for ChequeDate
        /// </summary>
        public DateTime ChequeDate
        {
            get { return mChequeDate; }
            set { mChequeDate = value; }
        }
        /// <summary>
        /// This Properties use for BankNo
        /// </summary>
        public long BankNo
        {
            get { return mBankNo; }
            set { mBankNo = value; }
        }
        /// <summary>
        /// This Properties use for BranchNo
        /// </summary>
        public long BranchNo
        {
            get { return mBranchNo; }
            set { mBranchNo = value; }
        }
        /// <summary>
        /// This Properties use for CreditCardNo
        /// </summary>
        public string CreditCardNo
        {
            get { return mCreditCardNo; }
            set { mCreditCardNo = value; }
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
        /// This Properties use for IsPost
        /// </summary>
        public bool IsPost
        {
            get { return mIsPost; }
            set { mIsPost = value; }
        }
        /// <summary>
        /// This Properties use for PostFkVoucherNo
        /// </summary>
        public long PostFkVoucherNo
        {
            get { return mPostFkVoucherNo; }
            set { mPostFkVoucherNo = value; }
        }
        /// <summary>
        /// This Properties use for PostFkVoucherTrnNo
        /// </summary>
        public long PostFkVoucherTrnNo
        {
            get { return mPostFkVoucherTrnNo; }
            set { mPostFkVoucherTrnNo = value; }
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

    public class TDeliveryAddress
    {
        private long mPkSrNo;
        private long mFkVoucherno;
        private long mLedgerno;
        private long mLedgerDetailsNo;
        private int mUserId;
        private DateTime mUserDate;
        private string mModifiedBy;
        private double mIsActive;
        private int mStatusNo;
        private string Mmsg;
        private long mCompanyNo;


        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }

        public long FkVoucherno
        {
            get { return mFkVoucherno; }
            set { mFkVoucherno = value; }
        }

        public long Ledgerno
        {
            get { return mLedgerno; }
            set { mLedgerno = value; }
        }

        public long LedgerDetailsNo
        {
            get { return mLedgerDetailsNo; }
            set { mLedgerDetailsNo = value; }
        }

        public int UserId
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

        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }

        public double IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }

        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
    }
    /// <summary>
    /// This Class use for TParkingBill
    /// </summary>

    public class TParkingBill
    {
        private long mParkingBillNo;
        private long mBillNo;
        private DateTime mBillDate;
        private DateTime mBillTime;
        private string mPersonName;
        private long mLedgerNo;
        private bool mIsBill;
        private long mFKVoucherNo;
        private long mCompanyNo;
        private bool mIsCancel;
        private long mUserID;
        private DateTime mUserDate;
        private int mStatusNo;
        private double mDiscount;
        private double mCharges;
        private double mCharges2;
        private string mRemark;
        private long mRateTypeNo;
        private long mTaxTypeNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for ParkingBillNo
        /// </summary>
        public long ParkingBillNo
        {
            get { return mParkingBillNo; }
            set { mParkingBillNo = value; }
        }
        /// <summary>
        /// This Properties use for BillNo
        /// </summary>
        public long BillNo
        {
            get { return mBillNo; }
            set { mBillNo = value; }
        }
        /// <summary>
        /// This Properties use for BillDate
        /// </summary>
        public DateTime BillDate
        {
            get { return mBillDate; }
            set { mBillDate = value; }
        }
        /// <summary>
        /// This Properties use for BillTime
        /// </summary>
        public DateTime BillTime
        {
            get { return mBillTime; }
            set { mBillTime = value; }
        }
        /// <summary>
        /// This Properties use for PersonName
        /// </summary>
        public string PersonName
        {
            get { return mPersonName; }
            set { mPersonName = value; }
        }
        /// <summary>
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for IsBill
        /// </summary>
        public bool IsBill
        {
            get { return mIsBill; }
            set { mIsBill = value; }
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
        /// This Properties use for CompanyNo
        /// </summary>
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
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
        /// This Properties use for StatusNo
        /// </summary>
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        /// <summary>
        /// This Properties use for Discount
        /// </summary>
        public double Discount
        {
            get { return mDiscount; }
            set { mDiscount = value; }
        }
        /// <summary>
        /// This Properties use for Charges
        /// </summary>
        public double Charges
        {
            get { return mCharges; }
            set { mCharges = value; }
        }
        /// <summary>
        /// This Properties use for Charges2
        /// </summary>
        public double Charges2
        {
            get { return mCharges2; }
            set { mCharges2 = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    /// <summary>
    /// This Class use for TParkingBillDetails
    /// </summary>

    public class TParkingBillDetails
    {
        private long mPkSrNo;
        private long mParkingBillNo;
        private string mBarcode;
        private double mQty;
        private double mRate;
        private double mItemDisc;
        private long mUOMNo;
        private int mStatusNo;
        private long mCompanyNo;
        private long mFkRateSettingNo;
        private long mItemNo;
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
        /// This Properties use for ParkingBillNo
        /// </summary>
        public long ParkingBillNo
        {
            get { return mParkingBillNo; }
            set { mParkingBillNo = value; }
        }
        /// <summary>
        /// This Properties use for Barcode
        /// </summary>
        public string Barcode
        {
            get { return mBarcode; }
            set { mBarcode = value; }
        }
        /// <summary>
        /// This Properties use for Qty
        /// </summary>
        public double Qty
        {
            get { return mQty; }
            set { mQty = value; }
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
        /// This Properties use for ItemDisc
        /// </summary>
        public double ItemDisc
        {
            get { return mItemDisc; }
            set { mItemDisc = value; }
        }
        /// <summary>
        /// This Properties use for UOMNo
        /// </summary>
        public long UOMNo
        {
            get { return mUOMNo; }
            set { mUOMNo = value; }
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
        /// This Properties use for FkRateSettingNo
        /// </summary>
        public long FkRateSettingNo
        {
            get { return mFkRateSettingNo; }
            set { mFkRateSettingNo = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    /// <summary>
    /// This Class use for MRateSetting
    /// </summary>

    public class MRateSetting3
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

        /// <summary>
        /// This Properties use for PkSrNo
        /// </summary>
        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        /// <summary>
        /// This Properties use for FkBcdSrNo
        /// </summary>
        public long FkBcdSrNo
        {
            get { return mFkBcdSrNo; }
            set { mFkBcdSrNo = value; }
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
        /// This Properties use for FromDate
        /// </summary>
        public DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        /// <summary>
        /// This Properties use for PurRate
        /// </summary>
        public double PurRate
        {
            get { return mPurRate; }
            set { mPurRate = value; }
        }
        /// <summary>
        /// This Properties use for MRP
        /// </summary>
        public double MRP
        {
            get { return mMRP; }
            set { mMRP = value; }
        }
        /// <summary>
        /// This Properties use for UOMNo
        /// </summary>
        public long UOMNo
        {
            get { return mUOMNo; }
            set { mUOMNo = value; }
        }
        /// <summary>
        /// This Properties use for ASaleRate
        /// </summary>
        public double ASaleRate
        {
            get { return mASaleRate; }
            set { mASaleRate = value; }
        }
        /// <summary>
        /// This Properties use for BSaleRate
        /// </summary>
        public double BSaleRate
        {
            get { return mBSaleRate; }
            set { mBSaleRate = value; }
        }
        /// <summary>
        /// This Properties use for CSaleRate
        /// </summary>
        public double CSaleRate
        {
            get { return mCSaleRate; }
            set { mCSaleRate = value; }
        }
        /// <summary>
        /// This Properties use for DSaleRate
        /// </summary>
        public double DSaleRate
        {
            get { return mDSaleRate; }
            set { mDSaleRate = value; }
        }
        /// <summary>
        /// This Properties use for ESaleRate
        /// </summary>
        public double ESaleRate
        {
            get { return mESaleRate; }
            set { mESaleRate = value; }
        }
        /// <summary>
        /// This Properties use for StockConversion
        /// </summary>
        public double StockConversion
        {
            get { return mStockConversion; }
            set { mStockConversion = value; }
        }
        /// <summary>
        /// This Properties use for PerOfRateVariation
        /// </summary>
        public double PerOfRateVariation
        {
            get { return mPerOfRateVariation; }
            set { mPerOfRateVariation = value; }
        }
        /// <summary>
        /// This Properties use for MKTQty
        /// </summary>
        public long MKTQty
        {
            get { return mMKTQty; }
            set { mMKTQty = value; }
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
    /// This Class use for TReward
    /// </summary>

    public class TReward
    {
        private long mRewardNo;
        private long mFkVoucherNo;
        private long mVoucherUserNo;
        private double mTotalBillAmount;
        private long mLedgerNo;
        private double mToalDiscAmount;
        private long mFkVoucherTrnNo;
        private long mRedemptionStatusNo;
        private long mStatusNo;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        /// <summary>
        /// This Properties use for RewardNo
        /// </summary>
        public long RewardNo
        {
            get { return mRewardNo; }
            set { mRewardNo = value; }
        }
        /// <summary>
        /// This Properties use for FkVoucherNo
        /// </summary>
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
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
        /// This Properties use for TotalBillAmount
        /// </summary>
        public double TotalBillAmount
        {
            get { return mTotalBillAmount; }
            set { mTotalBillAmount = value; }
        }
        /// <summary>
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for ToalDiscAmount
        /// </summary>
        public double ToalDiscAmount
        {
            get { return mToalDiscAmount; }
            set { mToalDiscAmount = value; }
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
        /// This Properties use for RedemptionStatusNo
        /// </summary>
        public long RedemptionStatusNo
        {
            get { return mRedemptionStatusNo; }
            set { mRedemptionStatusNo = value; }
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
        /// This Properties use for CompanyNo
        /// </summary>
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
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

    /// <summary>
    /// This Class use for TRewardDetails
    /// </summary>

    public class TRewardDetails
    {
        private long mPkSrNo;
        private long mRewardNo;
        private long mSchemeNo;
        private long mSchemeDetailsNo;
        private long mSchemeType;
        private double mDiscPer;
        private double mDiscAmount;
        private double mSchemeAmount;
        private long mSchemeAcheiverNo;
        private long mStatusNo;
        private long mCompanyNo;
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
        /// This Properties use for RewardNo
        /// </summary>
        public long RewardNo
        {
            get { return mRewardNo; }
            set { mRewardNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeNo
        /// </summary>
        public long SchemeNo
        {
            get { return mSchemeNo; }
            set { mSchemeNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeDetailsNo
        /// </summary>
        public long SchemeDetailsNo
        {
            get { return mSchemeDetailsNo; }
            set { mSchemeDetailsNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeType
        /// </summary>
        public long SchemeType
        {
            get { return mSchemeType; }
            set { mSchemeType = value; }
        }
        /// <summary>
        /// This Properties use for DiscPer
        /// </summary>
        public double DiscPer
        {
            get { return mDiscPer; }
            set { mDiscPer = value; }
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
        /// This Properties use for SchemeAmount
        /// </summary>
        public double SchemeAmount
        {
            get { return mSchemeAmount; }
            set { mSchemeAmount = value; }
        }
        /// <summary>
        /// This Properties use for SchemeAcheiverNo
        /// </summary>
        public long SchemeAcheiverNo
        {
            get { return mSchemeAcheiverNo; }
            set { mSchemeAcheiverNo = value; }
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

    /// <summary>
    /// This Class use for TRewardFrom
    /// </summary>

    public class TRewardFrom
    {
        private long mPkSrNo;
        private long mRewardNo;
        private long mRewardDetailsNo;
        private long mSchemeDetailsNo;
        private long mSchemeFromNo;
        private long mFkStockNo;
        private long mStatusNo;
        private long mCompanyNo;
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
        /// This Properties use for RewardNo
        /// </summary>
        public long RewardNo
        {
            get { return mRewardNo; }
            set { mRewardNo = value; }
        }
        /// <summary>
        /// This Properties use for RewardDetailsNo
        /// </summary>
        public long RewardDetailsNo
        {
            get { return mRewardDetailsNo; }
            set { mRewardDetailsNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeDetailsNo
        /// </summary>
        public long SchemeDetailsNo
        {
            get { return mSchemeDetailsNo; }
            set { mSchemeDetailsNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeFromNo
        /// </summary>
        public long SchemeFromNo
        {
            get { return mSchemeFromNo; }
            set { mSchemeFromNo = value; }
        }
        /// <summary>
        /// This Properties use for FkStockNo
        /// </summary>
        public long FkStockNo
        {
            get { return mFkStockNo; }
            set { mFkStockNo = value; }
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

    /// <summary>
    /// This Class use for TRewardTo
    /// </summary>

    public class TRewardTo
    {
        private long mPkSrNo;
        private long mRewardNo;
        private long mRewardDetailsNo;
        private long mSchemeDetailsNo;
        private long mSchemeToNo;
        private long mFkStockNo;
        private long mStatusNo;
        private long mCompanyNo;
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
        /// This Properties use for RewardNo
        /// </summary>
        public long RewardNo
        {
            get { return mRewardNo; }
            set { mRewardNo = value; }
        }
        /// <summary>
        /// This Properties use for RewardDetailsNo
        /// </summary>
        public long RewardDetailsNo
        {
            get { return mRewardDetailsNo; }
            set { mRewardDetailsNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeDetailsNo
        /// </summary>
        public long SchemeDetailsNo
        {
            get { return mSchemeDetailsNo; }
            set { mSchemeDetailsNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeToNo
        /// </summary>
        public long SchemeToNo
        {
            get { return mSchemeToNo; }
            set { mSchemeToNo = value; }
        }
        /// <summary>
        /// This Properties use for FkStockNo
        /// </summary>
        public long FkStockNo
        {
            get { return mFkStockNo; }
            set { mFkStockNo = value; }
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

    /// <summary>
    /// This Class use for TFooterDiscountDetails
    /// </summary>

    public class TFooterDiscountDetails
    {
        private long mPkSrNo;
        private long mFooterDiscNo;
        private long mFooterDiscDetailsNo;
        private long mFKVoucherNo;
        private double mDiscAmount;
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
        /// This Properties use for FooterDiscNo
        /// </summary>
        public long FooterDiscNo
        {
            get { return mFooterDiscNo; }
            set { mFooterDiscNo = value; }
        }
        /// <summary>
        /// This Properties use for FooterDiscDetailsNo
        /// </summary>
        public long FooterDiscDetailsNo
        {
            get { return mFooterDiscDetailsNo; }
            set { mFooterDiscDetailsNo = value; }
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
        /// This Properties use for DiscAmount
        /// </summary>
        public double DiscAmount
        {
            get { return mDiscAmount; }
            set { mDiscAmount = value; }
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

    /// <summary>
    /// This Class use for TItemLevelDiscountDetails
    /// </summary>

    public class TItemLevelDiscountDetails
    {
        private long mPkSrNo;
        private long mItemDiscNo;
        private long mItemBrandDiscNo;
        private long mItemNo;
        private double mDiscAmount;
        private long mFKStockTrnNo;
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
        /// This Properties use for ItemDiscNo
        /// </summary>
        public long ItemDiscNo
        {
            get { return mItemDiscNo; }
            set { mItemDiscNo = value; }
        }
        /// <summary>
        /// This Properties use for ItemBrandDiscNo
        /// </summary>
        public long ItemBrandDiscNo
        {
            get { return mItemBrandDiscNo; }
            set { mItemBrandDiscNo = value; }
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
        /// This Properties use for DiscAmount
        /// </summary>
        public double DiscAmount
        {
            get { return mDiscAmount; }
            set { mDiscAmount = value; }
        }
        /// <summary>
        /// This Properties use for FKStockTrnNo
        /// </summary>
        public long FKStockTrnNo
        {
            get { return mFKStockTrnNo; }
            set { mFKStockTrnNo = value; }
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

    /// <summary>
    /// This Class use for TOtherStockDetails
    /// </summary>

    public class TOtherStockDetails
    {
        private long mPkSrNo;
        private long mFKVoucherNo;
        private long mFKStockTrnNo;
        private long mFKOtherVoucherNo;
        private long mFKOtherStockTrnNo;
        private long mItemNo;
        private double mQuantity;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
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
        /// This Properties use for FKVoucherNo
        /// </summary>
        public long FKVoucherNo
        {
            get { return mFKVoucherNo; }
            set { mFKVoucherNo = value; }
        }
        /// <summary>
        /// This Properties use for FKStockTrnNo
        /// </summary>
        public long FKStockTrnNo
        {
            get { return mFKStockTrnNo; }
            set { mFKStockTrnNo = value; }
        }
        /// <summary>
        /// This Properties use for FKOtherVoucherNo
        /// </summary>
        public long FKOtherVoucherNo
        {
            get { return mFKOtherVoucherNo; }
            set { mFKOtherVoucherNo = value; }
        }
        /// <summary>
        /// This Properties use for FKOtherStockTrnNo
        /// </summary>
        public long FKOtherStockTrnNo
        {
            get { return mFKOtherStockTrnNo; }
            set { mFKOtherStockTrnNo = value; }
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
        /// This Properties use for Quantity
        /// </summary>
        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
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

    public class TGRNInvoice
    {
        private long mPkSrNo;
        private long mFkGRNNo;
        private long mFkVoucherNo;
        private long mCompanyNo;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long FkGRNNo
        {
            get { return mFkGRNNo; }
            set { mFkGRNNo = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
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
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TVoucherEntryCompany
    {
        private long mPKVoucherCompanyNo;
        private long mFkVoucherNo;
        private long mVoucherTypeCode;
        private long mVoucherUserNo;
        private double mBilledAmount;
        private long mStatusNo;
        private long mCompanyNo;
        private long mMfgCompNo;
        private long mTaxTypeNo;

        private long mPayTypeNo;
        private long mTaxInvoiceTypeNo = 1;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        public long PKVoucherCompanyNo
        {
            get { return mPKVoucherCompanyNo; }
            set { mPKVoucherCompanyNo = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        public long VoucherTypeCode
        {
            get { return mVoucherTypeCode; }
            set { mVoucherTypeCode = value; }
        }
        public long VoucherUserNo
        {
            get { return mVoucherUserNo; }
            set { mVoucherUserNo = value; }
        }
        public double BilledAmount
        {
            get { return mBilledAmount; }
            set { mBilledAmount = value; }
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
        public long MfgCompNo
        {
            get { return mMfgCompNo; }
            set { mMfgCompNo = value; }
        }

        public long TaxTypeNo
        {
            get { return mTaxTypeNo; }
            set { mTaxTypeNo = value; }
        }
        public long PayTypeNo
        {
            get { return mPayTypeNo; }
            set { mPayTypeNo = value; }
        }
        public long TaxInvoiceTypeNo
        {
            get { return mTaxInvoiceTypeNo; }
            set { mTaxInvoiceTypeNo = value; }
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

    public class TVoucherDetailsCompany
    {
        private long mPkVoucherCompTrnNo;
        private long mFkVoucherNo;
        private long mFKVoucherTrnNo;
        private long mVoucherSrNo;
        private long mSignCode;
        private long mLedgerNo;
        private double mDebit;
        private double mCredit;
        private long mSrNo;
        private long mCompanyNo;
        private string mNarration;
        private int mStatusNo;
        private long mMfgCompNo;
        private string Mmsg;

        public long PkVoucherCompTrnNo
        {
            get { return mPkVoucherCompTrnNo; }
            set { mPkVoucherCompTrnNo = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        public long FKVoucherTrnNo
        {
            get { return mFKVoucherTrnNo; }
            set { mFKVoucherTrnNo = value; }
        }
        public long VoucherSrNo
        {
            get { return mVoucherSrNo; }
            set { mVoucherSrNo = value; }
        }
        public long SignCode
        {
            get { return mSignCode; }
            set { mSignCode = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public double Debit
        {
            get { return mDebit; }
            set { mDebit = value; }
        }
        public double Credit
        {
            get { return mCredit; }
            set { mCredit = value; }
        }
        public long SrNo
        {
            get { return mSrNo; }
            set { mSrNo = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public string Narration
        {
            get { return mNarration; }
            set { mNarration = value; }
        }
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public long MfgCompNo
        {
            get { return mMfgCompNo; }
            set { mMfgCompNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TChequePrinting
    {
        private long mPkSrNo;
        private long mPrintingUserNo;
        private DateTime mPrintingDate;
        private bool mIsPurchase;
        private long mFKVoucherNo;
        private long mLedgerNo;
        private long mByLedgerNo;
        private DateTime mChequeDate;
        private string mChequeNo;
        private long mFKChequeNo;
        private string mRemark1;
        private string mRemark2;
        private string mRemark3;
        private long mChqStatusNo;
        private DateTime mBankDate;
        private long mCompanyNo;
        private long mStatusNo;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long PrintingUserNo
        {
            get { return mPrintingUserNo; }
            set { mPrintingUserNo = value; }
        }
        public DateTime PrintingDate
        {
            get { return mPrintingDate; }
            set { mPrintingDate = value; }
        }
        public bool IsPurchase
        {
            get { return mIsPurchase; }
            set { mIsPurchase = value; }
        }
        public long FKVoucherNo
        {
            get { return mFKVoucherNo; }
            set { mFKVoucherNo = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public long ByLedgerNo
        {
            get { return mByLedgerNo; }
            set { mByLedgerNo = value; }
        }
        public DateTime ChequeDate
        {
            get { return mChequeDate; }
            set { mChequeDate = value; }
        }
        public string ChequeNo
        {
            get { return mChequeNo; }
            set { mChequeNo = value; }
        }
        public long FKChequeNo
        {
            get { return mFKChequeNo; }
            set { mFKChequeNo = value; }
        }
        public string Remark1
        {
            get { return mRemark1; }
            set { mRemark1 = value; }
        }
        public string Remark2
        {
            get { return mRemark2; }
            set { mRemark2 = value; }
        }
        public string Remark3
        {
            get { return mRemark3; }
            set { mRemark3 = value; }
        }
        public long ChqStatusNo
        {
            get { return mChqStatusNo; }
            set { mChqStatusNo = value; }
        }
        public DateTime BankDate
        {
            get { return mBankDate; }
            set { mBankDate = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public long StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TDeleteVouchers
    {
        private long mPkSrNo;
        private long mFKVoucherNo;
        private long mVoucherTypeCode;
        private long mCompanyNo;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long FKVoucherNo
        {
            get { return mFKVoucherNo; }
            set { mFKVoucherNo = value; }
        }
        public long VoucherTypeCode
        {
            get { return mVoucherTypeCode; }
            set { mVoucherTypeCode = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TVoucherJournalEntry
    {
        private long mPKSrNo;
        private long mFKVoucherNo;
        private long mFKReceiptVoucherNo;
        private long mFKJournalVoucherNo;
        private long mLedgerNo;
        private double mAmount;
        private double mJVAmount;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        public long PKSrNo
        {
            get { return mPKSrNo; }
            set { mPKSrNo = value; }
        }
        public long FKVoucherNo
        {
            get { return mFKVoucherNo; }
            set { mFKVoucherNo = value; }
        }
        public long FKReceiptVoucherNo
        {
            get { return mFKReceiptVoucherNo; }
            set { mFKReceiptVoucherNo = value; }
        }
        public long FKJournalVoucherNo
        {
            get { return mFKJournalVoucherNo; }
            set { mFKJournalVoucherNo = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public double Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        public double JVAmount
        {
            get { return mJVAmount; }
            set { mJVAmount = value; }
        }
        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }
        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class TranspotorDetail
    {
        private long mPkTranspotorDetail;
        private long mFkVoucherNo;
        private long mFKEWayNo;
        private double mNoOfQty;
        private double mBalancedQty;
        private double mReceivedQty;
        private string mmsg;
        private DateTime mUpatedDate;
        private string mRemarkQty;
        //private long ReturnID;

        public long PkTranspotorDetail
        {
            get { return mPkTranspotorDetail; }
            set { mPkTranspotorDetail = value; }
        }
        public long FkVoucherNo
        {
            get { return mFkVoucherNo; }
            set { mFkVoucherNo = value; }
        }
        public long FKEWayNo
        {
            get { return mFKEWayNo; }
            set { mFKEWayNo = value; }
        }
        public DateTime UpatedDate
        {
            get { return mUpatedDate; }
            set { mUpatedDate = value; }
        }
        public double NoOfQty
        {
            get { return mNoOfQty; }
            set { mNoOfQty = value; }
        }
        public double BalancedQty
        {
            get { return mBalancedQty; }
            set { mBalancedQty = value; }
        }
        public double ReceivedQty
        {
            get { return mReceivedQty; }
            set { mReceivedQty = value; }
        }
        public string msg
        {
            get { return mmsg; }
            set { mmsg = value; }
        }
        public string RemarkQty
        {
            get { return mRemarkQty; }
            set { mRemarkQty = value; }
        }
    }

}
