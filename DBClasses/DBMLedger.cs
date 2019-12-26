using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Data;
using OMControls;

namespace OM
{
    class DBMLedger
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMLedger(MLedger mledger)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLedger1";

            cmd.Parameters.AddWithValue("@LedgerNo", mledger.LedgerNo);

            cmd.Parameters.AddWithValue("@LedgerUserNo", mledger.LedgerUserNo);

            cmd.Parameters.AddWithValue("@LedgerName", mledger.LedgerName);

            cmd.Parameters.AddWithValue("@GroupNo", mledger.GroupNo);

            cmd.Parameters.AddWithValue("@OpeningBalance", 0);

            cmd.Parameters.AddWithValue("@SignCode", 0);

            cmd.Parameters.AddWithValue("@MaintainBillByBill", mledger.MaintainBillByBill);

            cmd.Parameters.AddWithValue("@IsActive", mledger.IsActive);

            cmd.Parameters.AddWithValue("@ContactPerson", mledger.ContactPerson);

            cmd.Parameters.AddWithValue("@CompanyNo", mledger.CompanyNo);

            cmd.Parameters.AddWithValue("@LedgerStatus", mledger.LedgerStatus);

            cmd.Parameters.AddWithValue("@IsEnroll", mledger.IsEnroll);

            cmd.Parameters.AddWithValue("@IsSendSMS", mledger.IsSendSMS);

            cmd.Parameters.AddWithValue("@UserID", mledger.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mledger.UserDate);

            cmd.Parameters.AddWithValue("@TransporterNo", mledger.TransporterNo);

            cmd.Parameters.AddWithValue("@StateCode", mledger.StateCode);

            cmd.Parameters.AddWithValue("@LedgerLangName", (mledger.LedgerLangName == null) ? "" : mledger.LedgerLangName);

            cmd.Parameters.AddWithValue("@IsPartyWiseRate", mledger.IsPartyWiseRate);

            cmd.Parameters.AddWithValue("@QuotationRate ", mledger.QuotationRate);

            cmd.Parameters.AddWithValue("@ContactPersonLang", (mledger.ContactPersonLang == null) ? "" : mledger.ContactPersonLang);

            cmd.Parameters.AddWithValue("@IsSendEmail", mledger.IsSendEmail);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);


            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMLedgerDetails(MLedgerDetails mledgerdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLedgerDetails";

            cmd.Parameters.AddWithValue("@LedgerDetailsNo", mledgerdetails.LedgerDetailsNo);

            cmd.Parameters.AddWithValue("@CreditLimit", mledgerdetails.CreditLimit);

            cmd.Parameters.AddWithValue("@Address", mledgerdetails.Address);

            cmd.Parameters.AddWithValue("@StateNo", mledgerdetails.StateNo);

            cmd.Parameters.AddWithValue("@CityNo", mledgerdetails.CityNo);

            cmd.Parameters.AddWithValue("@PinCode", mledgerdetails.PinCode);

            cmd.Parameters.AddWithValue("@PhNo1", mledgerdetails.PhNo1);

            cmd.Parameters.AddWithValue("@PhNo2", mledgerdetails.PhNo2);

            cmd.Parameters.AddWithValue("@MobileNo1", mledgerdetails.MobileNo1);

            cmd.Parameters.AddWithValue("@MobileNo2", mledgerdetails.MobileNo2);

            cmd.Parameters.AddWithValue("@EmailID", mledgerdetails.EmailID);

            cmd.Parameters.AddWithValue("@CustomerType", mledgerdetails.CustomerType);

            cmd.Parameters.AddWithValue("@PANNo", mledgerdetails.PANNo);

            cmd.Parameters.AddWithValue("@AccountNo", mledgerdetails.AccountNo);

            cmd.Parameters.AddWithValue("@ReportName", mledgerdetails.ReportName);

            cmd.Parameters.AddWithValue("@UserID", mledgerdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mledgerdetails.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);

            cmd.Parameters.AddWithValue("@FSSAI", (mledgerdetails.FSSAI == null) ? "" : mledgerdetails.FSSAI);

            cmd.Parameters.AddWithValue("@AreaNo", mledgerdetails.AreaNo);

            cmd.Parameters.AddWithValue("@AddressLang", (mledgerdetails.AddressLang == null) ? "" : mledgerdetails.AddressLang);

            cmd.Parameters.AddWithValue("@RateTypeNo", mledgerdetails.RateTypeNo);

            cmd.Parameters.AddWithValue("@DiscPer", mledgerdetails.DiscPer);

            cmd.Parameters.AddWithValue("@DiscRs", mledgerdetails.DiscRs);

            cmd.Parameters.AddWithValue("@AdharNo", mledgerdetails.AdharNo);

            cmd.Parameters.AddWithValue("@AnyotherNo1", mledgerdetails.AnyotherNo1);

            cmd.Parameters.AddWithValue("@AnyotherNo2", mledgerdetails.AnyotherNo2);

            cmd.Parameters.AddWithValue("@GSTNo", mledgerdetails.GSTNo);

            cmd.Parameters.AddWithValue("@FSSAIDate", mledgerdetails.FSSAIDate);

            cmd.Parameters.AddWithValue("@GSTDate", mledgerdetails.GSTDate);

            cmd.Parameters.AddWithValue("@Distance", mledgerdetails.Distance);

            cmd.Parameters.AddWithValue("@CreditDays", mledgerdetails.CreditDays);


            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMLedgerDetails2(MLedgerDetails mledgerdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLedgerDetails2";

            cmd.Parameters.AddWithValue("@LedgerDetailsNo", mledgerdetails.LedgerDetailsNo);
            cmd.Parameters.AddWithValue("@LedgerNo", mledgerdetails.LedgerNo);
            cmd.Parameters.AddWithValue("@CreditLimit", mledgerdetails.CreditLimit);

            cmd.Parameters.AddWithValue("@Address", mledgerdetails.Address);

            cmd.Parameters.AddWithValue("@StateNo", mledgerdetails.StateNo);

            cmd.Parameters.AddWithValue("@CityNo", mledgerdetails.CityNo);

            cmd.Parameters.AddWithValue("@PinCode", mledgerdetails.PinCode);

            cmd.Parameters.AddWithValue("@PhNo1", mledgerdetails.PhNo1);

            //  cmd.Parameters.AddWithValue("@PhNo2", mledgerdetails.PhNo2);

            cmd.Parameters.AddWithValue("@MobileNo1", mledgerdetails.MobileNo1);

            // cmd.Parameters.AddWithValue("@MobileNo2", mledgerdetails.MobileNo2);

            cmd.Parameters.AddWithValue("@EmailID", mledgerdetails.EmailID);

            cmd.Parameters.AddWithValue("@CustomerType", mledgerdetails.CustomerType);

            // cmd.Parameters.AddWithValue("@PANNo", mledgerdetails.PANNo);

            cmd.Parameters.AddWithValue("@UserID", mledgerdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mledgerdetails.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);

            //   cmd.Parameters.AddWithValue("@FSSAI", (mledgerdetails.FSSAI == null) ? "" : mledgerdetails.FSSAI);

            cmd.Parameters.AddWithValue("@AreaNo", mledgerdetails.AreaNo);

            //     cmd.Parameters.AddWithValue("@AddressLang", (mledgerdetails.AddressLang == null) ? "" : mledgerdetails.AddressLang);

            // cmd.Parameters.AddWithValue("@AdharNo", mledgerdetails.AdharNo);

            //cmd.Parameters.AddWithValue("@RateTypeNo", mledgerdetails.RateTypeNo);

            //    cmd.Parameters.AddWithValue("@DiscPer", mledgerdetails.DiscPer);
            ///
            // cmd.Parameters.AddWithValue("@DiscRs", mledgerdetails.DiscRs);

            //cmd.Parameters.AddWithValue("@AnyotherNo1", mledgerdetails.AnyotherNo1);
            //cmd.Parameters.AddWithValue("@AccountNo", mledgerdetails.AccountNo);
            //cmd.Parameters.AddWithValue("@ReportName", mledgerdetails.ReportName);
            //cmd.Parameters.AddWithValue("@AnyotherNo2", mledgerdetails.AnyotherNo2);
            cmd.Parameters.AddWithValue("@GSTNo", mledgerdetails.GSTNo);
            cmd.Parameters.AddWithValue("@FSSAIDate", mledgerdetails.FSSAIDate);
            cmd.Parameters.AddWithValue("@GSTDate", mledgerdetails.GSTDate);
            cmd.Parameters.AddWithValue("@Distance", mledgerdetails.Distance);



            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMLedger(MLedger mledger)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMLedger";

            cmd.Parameters.AddWithValue("@LedgerNo", mledger.LedgerNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mledger.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMLedger(int GroupNo)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLedger where GroupNo=" + GroupNo + " order by LedgerNo";
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

        public DataView GetMLedgerByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLedger where LedgerNo =" + ID;
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

        public MLedger ModifyMLedgerByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MLedger where LedgerNo =" + ID + " ";
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MLedger MM = new MLedger();
                while (dr.Read())
                {
                    MM.LedgerNo = Convert.ToInt32(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["LedgerUserNo"])) MM.LedgerUserNo = Convert.ToString(dr["LedgerUserNo"]);
                    if (!Convert.IsDBNull(dr["LedgerName"])) MM.LedgerName = Convert.ToString(dr["LedgerName"]);
                    if (!Convert.IsDBNull(dr["GroupNo"])) MM.GroupNo = Convert.ToInt64(dr["GroupNo"]);
                    if (!Convert.IsDBNull(dr["StateCode"])) MM.StateCode = Convert.ToInt64(dr["StateCode"]);
                    if (!Convert.IsDBNull(dr["MaintainBillByBill"])) MM.MaintainBillByBill = Convert.ToBoolean(dr["MaintainBillByBill"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["ContactPerson"])) MM.ContactPerson = Convert.ToString(dr["ContactPerson"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["IsEnroll"])) MM.IsEnroll = Convert.ToBoolean(dr["IsEnroll"]);
                    if (!Convert.IsDBNull(dr["IsSendSMS"])) MM.IsSendSMS = Convert.ToBoolean(dr["IsSendSMS"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["LedgerLangName"])) MM.LedgerLangName = Convert.ToString(dr["LedgerLangName"]);
                    if (!Convert.IsDBNull(dr["TransporterNo"])) MM.TransporterNo = Convert.ToInt64(dr["TransporterNo"]);
                    if (!Convert.IsDBNull(dr["IsPartyWiseRate"])) MM.IsPartyWiseRate = Convert.ToBoolean(dr["IsPartyWiseRate"]);
                    if (!Convert.IsDBNull(dr["QuotationRate"])) MM.QuotationRate = Convert.ToBoolean(dr["QuotationRate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["ContactPersonLang"])) MM.ContactPersonLang = Convert.ToString(dr["ContactPersonLang"]);
                    if (!Convert.IsDBNull(dr["IsSendEmail"])) MM.IsSendEmail = Convert.ToBoolean(dr["IsSendEmail"]); 
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MLedger();
        }

        public DataView GetBySearch(string Column, string Value, long GroupNo)
        {
            Value = Value.Replace("'", "''");
            string sql = null;
            if (GroupNo == GroupType.Transporter)
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                {
                    switch (Column)
                    {
                        case "0":
                            sql = "Select LedgerNo,LedgerName AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND GroupNo=" + GroupNo + " order by LedgerName";
                            break;
                        case "LedgerName":
                            sql = "Select LedgerNo,LedgerName AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND  GroupNo=" + GroupNo + " and " + Column + " like '%" + Value.Trim() + "%' order by LedgerName";
                            break;
                        case "ContactPerson":
                            sql = "Select LedgerNo,LedgerName AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND  GroupNo=" + GroupNo + " and " + Column + " like '%" + Value.Trim() + "%' order by LedgerName";
                            break;
                    }
                }
                else
                {
                    switch (Column)
                    {
                        case "0":
                            sql = "Select MLedger.LedgerNo,LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '')  AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo where MLedger.LedgerNo >15 AND GroupNo=" + GroupNo + " order by LedgerName";
                            break;
                        case "LedgerName":
                            sql = "Select MLedger.LedgerNo,LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo where MLedger.LedgerNo >15 AND  GroupNo=" + GroupNo + " and " + Column + " like '%" + Value.Trim() + "%' order by LedgerName";
                            break;
                        case "ContactPerson":
                            sql = "Select MLedger.LedgerNo,LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo where MLedger.LedgerNo >15 AND  GroupNo=" + GroupNo + " and " + Column + " like '%" + Value.Trim() + "%' order by LedgerName";
                            break;
                    }
                }
            }
            else
            {
                switch (Column)
                {
                    case "0":
                        sql = "Select LedgerNo,LedgerName AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND GroupNo=" + GroupNo + " order by LedgerName";
                        break;
                    case "LedgerName":
                        sql = "Select LedgerNo,LedgerName AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND  GroupNo=" + GroupNo + " and " + Column + " like '%" + Value.Trim() + "%' order by LedgerName";
                        break;
                    case "ContactPerson":
                        sql = "Select LedgerNo,LedgerName AS 'Name',ContactPerson AS " + (GroupNo == GroupType.BankAccounts ? "'Contact Number'" : "'Contact Person'") + ",Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND  GroupNo=" + GroupNo + " and " + Column + " like '%" + Value.Trim() + "%' order by LedgerName";
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

        public DataView GetBySearchAcc(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select LedgerNo,LedgerName AS 'Ledger Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND GroupNo not in(" + GroupType.SundryCreditors + "," + GroupType.SundryDebtors + "," + GroupType.DutiesAndTaxes + "," + GroupType.BankAccounts + "," + GroupType.Primary + ") order by LedgerName";
                    break;
                case "LedgerName":
                    sql = "Select LedgerNo,LedgerName AS 'Ledger Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger where LedgerNo >15 AND GroupNo not in(" + GroupType.SundryCreditors + "," + GroupType.SundryDebtors + "," + GroupType.DutiesAndTaxes + "," + GroupType.BankAccounts + "," + GroupType.Primary + ") and " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%' order by LedgerName";
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

        public DataView GetBySearchTaxAcc(string Column, string Value)
        {
            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select LedgerNo,LedgerName AS 'Ledger Name',MGroup.GroupName As 'ControlGroup',Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger " +
                          "INNER JOIN  MGroup ON MLedger.GroupNo = MGroup.GroupNo " +
                          "where (MLedger.LedgerNo >15) AND (MLedger.GroupNo in(" + GroupType.DutiesAndTaxes + ")) or MLedger.GroupNo in(Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + ")) order by LedgerName";
                    break;
                case "LedgerName":
                    sql = "Select LedgerNo,LedgerName AS 'Ledger Name', MGroup.GroupName As 'ControlGroup',Case when (MLedger.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MLedger " +
                          "INNER JOIN  MGroup ON MLedger.GroupNo = MGroup.GroupNo " +
                          "where (MLedger.LedgerNo >15) AND (MLedger.GroupNo in(" + GroupType.DutiesAndTaxes + ") or MLedger.GroupNo in(Select G.GroupNo From MGroup G Where G.ControlGroup in(" + GroupType.DutiesAndTaxes + "))) and " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'  order by LedgerName";
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

        public bool DeleteMLedgerDetails(MLedgerDetails mledgerdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMLedgerDetails";

            cmd.Parameters.AddWithValue("@LedgerDetailsNo", mledgerdetails.LedgerDetailsNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mledgerdetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMAddress()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLedgerDetails order by LedgerDetailsNo";
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

        public DataView GetMAddressByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MLedgerDetails where LedgerDetailsNo =" + ID;
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

        public MLedgerDetails ModifyMLedgerDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MLedgerDetails where LedgerNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MLedgerDetails MM = new MLedgerDetails();
                while (dr.Read())
                {
                    MM.LedgerDetailsNo = Convert.ToInt32(dr["LedgerDetailsNo"]);
                    //if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["Address"])) MM.Address = Convert.ToString(dr["Address"]);
                    if (!Convert.IsDBNull(dr["AddressLang"])) MM.AddressLang = Convert.ToString(dr["AddressLang"]);
                    if (!Convert.IsDBNull(dr["StateNo"])) MM.StateNo = Convert.ToInt64(dr["StateNo"]);
                    if (!Convert.IsDBNull(dr["CityNo"])) MM.CityNo = Convert.ToInt64(dr["CityNo"]);
                    if (!Convert.IsDBNull(dr["AreaNo"])) MM.AreaNo = Convert.ToInt64(dr["AreaNo"]);
                    if (!Convert.IsDBNull(dr["PinCode"])) MM.PinCode = Convert.ToString(dr["PinCode"]);
                    if (!Convert.IsDBNull(dr["PhNo1"])) MM.PhNo1 = Convert.ToString(dr["PhNo1"]);
                    if (!Convert.IsDBNull(dr["PhNo2"])) MM.PhNo2 = Convert.ToString(dr["PhNo2"]);
                    if (!Convert.IsDBNull(dr["MobileNo1"])) MM.MobileNo1 = Convert.ToString(dr["MobileNo1"]);
                    if (!Convert.IsDBNull(dr["MobileNo2"])) MM.MobileNo2 = Convert.ToString(dr["MobileNo2"]);
                    if (!Convert.IsDBNull(dr["EmailID"])) MM.EmailID = Convert.ToString(dr["EmailID"]);
                    if (!Convert.IsDBNull(dr["CustomerType"])) MM.CustomerType = Convert.ToInt64(dr["CustomerType"]);
                    if (!Convert.IsDBNull(dr["RateTypeNo"])) MM.RateTypeNo = Convert.ToInt64(dr["RateTypeNo"]);
                    if (!Convert.IsDBNull(dr["CreditLimit"])) MM.CreditLimit = Convert.ToDouble(dr["CreditLimit"]);
                    if (!Convert.IsDBNull(dr["CreditDays"])) MM.CreditDays = Convert.ToInt64(dr["CreditDays"]);
                    if (!Convert.IsDBNull(dr["PANNo"])) MM.PANNo = Convert.ToString(dr["PANNo"]);
                    if (!Convert.IsDBNull(dr["AdharCardNo"])) MM.AdharNo = Convert.ToString(dr["AdharCardNo"]);
                    if (!Convert.IsDBNull(dr["RateTypeNo"])) MM.RateTypeNo = Convert.ToInt64(dr["RateTypeNo"]);
                    if (!Convert.IsDBNull(dr["DiscPer"])) MM.DiscPer = Convert.ToDouble(dr["DiscPer"]);
                    if (!Convert.IsDBNull(dr["DiscRs"])) MM.DiscRs = Convert.ToDouble(dr["DiscRs"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["FSSAI"])) MM.FSSAI = Convert.ToString(dr["FSSAI"]);
                    if (!Convert.IsDBNull(dr["AnyotherNo1"])) MM.AnyotherNo1 = Convert.ToString(dr["AnyotherNo1"]);
                    if (!Convert.IsDBNull(dr["AnyotherNo2"])) MM.AnyotherNo2 = Convert.ToString(dr["AnyotherNo2"]);
                    if (!Convert.IsDBNull(dr["GSTNo"])) MM.GSTNo = Convert.ToString(dr["GSTNo"]);
                    if (!Convert.IsDBNull(dr["AccountNo"])) MM.AccountNo = Convert.ToString(dr["AccountNo"]);
                    if (!Convert.IsDBNull(dr["ReportName"])) MM.ReportName = Convert.ToString(dr["ReportName"]);

                    if (!Convert.IsDBNull(dr["FSSAIDate"])) MM.FSSAIDate = Convert.ToDateTime(dr["FSSAIDate"]);
                    if (!Convert.IsDBNull(dr["GSTDate"])) MM.GSTDate = Convert.ToDateTime(dr["GSTDate"]);
                    if (!Convert.IsDBNull(dr["Distance"])) MM.Distance = Convert.ToDouble(dr["Distance"]);


                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MLedgerDetails();
        }

        public MLedgerDetails ModifyMLedgerDetailsByID2(long LID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MLedgerDetails where LedgerDetailsNo =" + LID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MLedgerDetails MM = new MLedgerDetails();
                while (dr.Read())
                {
                    MM.LedgerDetailsNo = Convert.ToInt32(dr["LedgerDetailsNo"]);
                    //if (!Convert.IsDBNull(dr["LedgerNo"])) MM.LedgerNo = Convert.ToInt64(dr["LedgerNo"]);
                    if (!Convert.IsDBNull(dr["Address"])) MM.Address = Convert.ToString(dr["Address"]);
                    if (!Convert.IsDBNull(dr["AddressLang"])) MM.AddressLang = Convert.ToString(dr["AddressLang"]);
                    if (!Convert.IsDBNull(dr["StateNo"])) MM.StateNo = Convert.ToInt64(dr["StateNo"]);
                    if (!Convert.IsDBNull(dr["CityNo"])) MM.CityNo = Convert.ToInt64(dr["CityNo"]);
                    if (!Convert.IsDBNull(dr["AreaNo"])) MM.AreaNo = Convert.ToInt64(dr["AreaNo"]);
                    if (!Convert.IsDBNull(dr["PinCode"])) MM.PinCode = Convert.ToString(dr["PinCode"]);
                    if (!Convert.IsDBNull(dr["PhNo1"])) MM.PhNo1 = Convert.ToString(dr["PhNo1"]);
                    if (!Convert.IsDBNull(dr["PhNo2"])) MM.PhNo2 = Convert.ToString(dr["PhNo2"]);
                    if (!Convert.IsDBNull(dr["MobileNo1"])) MM.MobileNo1 = Convert.ToString(dr["MobileNo1"]);
                    if (!Convert.IsDBNull(dr["MobileNo2"])) MM.MobileNo2 = Convert.ToString(dr["MobileNo2"]);
                    if (!Convert.IsDBNull(dr["EmailID"])) MM.EmailID = Convert.ToString(dr["EmailID"]);
                    if (!Convert.IsDBNull(dr["CustomerType"])) MM.CustomerType = Convert.ToInt64(dr["CustomerType"]);
                    if (!Convert.IsDBNull(dr["RateTypeNo"])) MM.RateTypeNo = Convert.ToInt64(dr["RateTypeNo"]);
                    if (!Convert.IsDBNull(dr["CreditLimit"])) MM.CreditLimit = Convert.ToDouble(dr["CreditLimit"]);
                    if (!Convert.IsDBNull(dr["PANNo"])) MM.PANNo = Convert.ToString(dr["PANNo"]);
                    if (!Convert.IsDBNull(dr["AdharCardNo"])) MM.AdharNo = Convert.ToString(dr["AdharCardNo"]);
                    if (!Convert.IsDBNull(dr["RateTypeNo"])) MM.RateTypeNo = Convert.ToInt64(dr["RateTypeNo"]);
                    if (!Convert.IsDBNull(dr["DiscPer"])) MM.DiscPer = Convert.ToDouble(dr["DiscPer"]);
                    if (!Convert.IsDBNull(dr["DiscRs"])) MM.DiscRs = Convert.ToDouble(dr["DiscRs"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                    if (!Convert.IsDBNull(dr["FSSAI"])) MM.FSSAI = Convert.ToString(dr["FSSAI"]);
                    if (!Convert.IsDBNull(dr["AnyotherNo1"])) MM.AnyotherNo1 = Convert.ToString(dr["AnyotherNo1"]);
                    if (!Convert.IsDBNull(dr["AnyotherNo2"])) MM.AnyotherNo2 = Convert.ToString(dr["AnyotherNo2"]);
                    if (!Convert.IsDBNull(dr["GSTNo"])) MM.GSTNo = Convert.ToString(dr["GSTNo"]);
                    if (!Convert.IsDBNull(dr["AccountNo"])) MM.AccountNo = Convert.ToString(dr["AccountNo"]);
                    if (!Convert.IsDBNull(dr["ReportName"])) MM.ReportName = Convert.ToString(dr["ReportName"]);

                    if (!Convert.IsDBNull(dr["FSSAIDate"])) MM.FSSAIDate = Convert.ToDateTime(dr["FSSAIDate"]);
                    if (!Convert.IsDBNull(dr["GSTDate"])) MM.GSTDate = Convert.ToDateTime(dr["GSTDate"]);
                    if (!Convert.IsDBNull(dr["Distance"])) MM.Distance = Convert.ToDouble(dr["Distance"]);


                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MLedgerDetails();
        }
       
        //public bool UpdateLedgerBalance(MLedger mledger)
        //{
        //    SqlCommand cmd;
        //    cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = "Update MLedger set OpeningBalance=@OpeningBalance,SignCode=@SignCode,StatusNo=2 where LedgerNo=@LedgerNo";

        //    cmd.Parameters.AddWithValue("@LedgerNo", mledger.LedgerNo);

        //    commandcollection.Add(cmd);
        //    return true;
        //}

        public bool ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int TaxLedgNo = 0, SaleLedgNo = 0;

            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddMLedger1")
                        {
                            if (i == 0)
                                TaxLedgNo = i;
                            else
                                SaleLedgNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddMLedgerDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@LedgerNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMLedgerDetails2")
                        {
                            //  commandcollection[i].Parameters.AddWithValue("@LedgerNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMLedgerDistDetails")
                        {
                            // commandcollection[i].Parameters.AddWithValue("@LedgerNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMItemTaxSetting")
                        {
                            commandcollection[i].Parameters.AddWithValue("@TaxLedgerNo", commandcollection[TaxLedgNo].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@SalesLedgerNo", commandcollection[SaleLedgNo].Parameters["@ReturnID"].Value);
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

        public bool AddMLedgerDistDetails(MLedgerDistDetails mledgerdistdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMLedgerDistDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mledgerdistdetails.PkSrNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mledgerdistdetails.LedgerNo);

            cmd.Parameters.AddWithValue("@DistAgentName", mledgerdistdetails.DistAgentName);

            cmd.Parameters.AddWithValue("@MobileNo", mledgerdistdetails.MobileNo);

            cmd.Parameters.AddWithValue("@Remark", mledgerdistdetails.Remark);

            cmd.Parameters.AddWithValue("@CompanyNo", DBGetVal.FirmNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool ExecuteNonQueryStatementsServer()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStrServer);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            //int cntLedg = 0;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        //if (commandcollection[i].CommandText == "AddMLedger1")
                        //{
                        //    cntLedg = i;
                        //}
                        //if (commandcollection[i].CommandText == "AddMLedgerDetails")
                        //{
                        //    commandcollection[i].Parameters.AddWithValue("@LedgerNo", commandcollection[cntLedg].Parameters["@ReturnID"].Value);
                        //}
                        //if (commandcollection[i].CommandText == "AddMLedgerDistDetails")
                        //{
                        //   // commandcollection[i].Parameters.AddWithValue("@LedgerNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        //}
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

        public bool AddMItemTaxSetting(MItemTaxSetting mitemtaxsetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMItemTaxSetting";

            cmd.Parameters.AddWithValue("@PkSrNo", mitemtaxsetting.PkSrNo);

            cmd.Parameters.AddWithValue("@TaxSettingName", mitemtaxsetting.TaxSettingName);

            //cmd.Parameters.AddWithValue("@TaxLedgerNo", mitemtaxsetting.TaxLedgerNo);

            //cmd.Parameters.AddWithValue("@SalesLedgerNo", mitemtaxsetting.SalesLedgerNo);

            //cmd.Parameters.AddWithValue("@CalculationMethod", mitemtaxsetting.CalculationMethod);

            cmd.Parameters.AddWithValue("@Percentage", mitemtaxsetting.Percentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mitemtaxsetting.CompanyNo);

            cmd.Parameters.AddWithValue("@IsActive", mitemtaxsetting.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mitemtaxsetting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mitemtaxsetting.UserDate);

            commandcollection.Add(cmd);
            return true;
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

            cmd.Parameters.AddWithValue("@MfgCompNo", mledgerratesetting.CompNo);

            cmd.Parameters.AddWithValue("@UserID", mledgerratesetting.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mledgerratesetting.UserDate);
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

        public bool UpdateLedgerRateSetting(MLedgerRateSetting mledgerratesetting)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update MLedgerRateSetting set Rate=@Rate,DiscPercentage=@DiscPercentage where PkSrNo=@PkSrNo";

            cmd.Parameters.AddWithValue("@PkSrNo", mledgerratesetting.PkSrNo);

            cmd.Parameters.AddWithValue("@Rate", mledgerratesetting.Rate);

            cmd.Parameters.AddWithValue("@DiscPercentage", mledgerratesetting.DiscPercentage);

            commandcollection.Add(cmd);
            return true;
        }

    }

    public class MLedger
    {
        private long mLedgerNo;
        private string mLedgerUserNo;
        private string mLedgerName;
        private long mGroupNo;
        //private double mOpeningBalance;
        //private long mSignCode;
        private bool mInvFlag;
        private bool mMaintainBillByBill;
        private bool mIsActive;
        private string mContactPerson;
        private long mCompanyNo;
        private int mLedgerStatus;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private int mStatusNo;
        private bool mIsEnroll;
        private bool mIsSendSMS;
        private string mLedgerLangName;
        private long mTransporterNo;
        private bool mIsHistoryMaintain;
        private long mStateCode;
        private bool mIsPartyWiseRate;
        private bool mQuotationRate;
        private string Mmsg;
        private string mContactPersonLang;
        private bool mIsSendEmail;

        public bool QuotationRate
        {
            get { return mQuotationRate; }
            set { mQuotationRate = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }

        public string LedgerUserNo
        {
            get { return mLedgerUserNo; }
            set { mLedgerUserNo = value; }
        }

        public string LedgerName
        {
            get { return mLedgerName; }
            set { mLedgerName = value; }
        }

        public long GroupNo
        {
            get { return mGroupNo; }
            set { mGroupNo = value; }
        }

        public bool InvFlag
        {
            get { return mInvFlag; }
            set { mInvFlag = value; }
        }

        public bool MaintainBillByBill
        {
            get { return mMaintainBillByBill; }
            set { mMaintainBillByBill = value; }
        }

        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }

        public string ContactPerson
        {
            get { return mContactPerson; }
            set { mContactPerson = value; }
        }

        public long CompanyNo
        {
            get { return mCompanyNo; }
            set { mCompanyNo = value; }
        }

        public int LedgerStatus
        {
            get { return mLedgerStatus; }
            set { mLedgerStatus = value; }
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

        public int StatusNo
        {
            get { return mStatusNo; }
            set { mStatusNo = value; }
        }

        public bool IsEnroll
        {
            get { return mIsEnroll; }
            set { mIsEnroll = value; }
        }

        public bool IsSendSMS
        {
            get { return mIsSendSMS; }
            set { mIsSendSMS = value; }
        }
       
        public string LedgerLangName
        {
            get { return mLedgerLangName; }
            set { mLedgerLangName = value; }
        }

        public string ContactPersonLang
        {
            get { return mContactPersonLang; }
            set { mContactPersonLang = value; }
        }

        public long TransporterNo
        {
            get { return mTransporterNo; }
            set { mTransporterNo = value; }
        }

        public bool IsHistoryMaintain
        {
            get { return mIsHistoryMaintain; }
            set { mIsHistoryMaintain = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
        //--GST--//
        public long StateCode
        {
            get { return mStateCode; }
            set { mStateCode = value; }
        }
        public bool IsPartyWiseRate
        {
            get { return mIsPartyWiseRate; }
            set { mIsPartyWiseRate = value; }
        }
        public bool IsSendEmail
        {
            get { return mIsSendEmail; }
            set { mIsSendEmail = value; }
        }
    }

    /// <summary>
    /// This Class use for MLedgerDetails
    /// </summary>
    public class MLedgerDetails
    {
        private long mLedgerDetailsNo;
        private long mLedgerNo;
        private string mAddress;
        private long mStateNo;
        private long mCityNo;
        private string mPinCode;
        private string mPhNo1;
        private string mPhNo2;
        private string mMobileNo1;
        private string mMobileNo2;
        private string mEmailID;
        private DateTime mDOB;
        private long mQualificationNo;
        private long mOccupationNo;
        private long mCustomerType;
        private double mCreditLimit;
        private long mCreditDays;
        private long mBillPrintType;
        private string mPANNo;
        private string mVATNo;
        private string mCSTNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private long mGender;
        private string mFSSAI;
        private string mServiceTaxNo;
        private long mAreaNo;
        private long mCustSizeNo;
        private int mStatusNo;
        private long mCompanyNo;
        private DateTime mConsentDate;
        private bool mIsLBTApply;
        private string mLBTNo;
        private string mAccountNo;
        private string mReportName;
        private string mAddressLang;
        private double mDiscPer;
        private double mDiscRs;
        private long mRateTypeNo;
        private string mAdharNo;
        private string mAnyotherNo1;
        private string mAnyotherNo2;
        private string mGSTNo;
        private string Mmsg;
        private DateTime mFSSAIDate;
        private DateTime mGSTDate;
        private double mDistance;

        public DateTime FSSAIDate
        {
            get { return mFSSAIDate; }
            set { mFSSAIDate = value; }
        }
        public DateTime GSTDate
        {
            get { return mGSTDate; }
            set { mGSTDate = value; }
        }

        public string GSTNo
        {
            get { return mGSTNo; }
            set { mGSTNo = value; }
        }

        public string AnyotherNo1
        {
            get { return mAnyotherNo1; }
            set { mAnyotherNo1 = value; }
        }
        public string AnyotherNo2
        {
            get { return mAnyotherNo2; }
            set { mAnyotherNo2 = value; }
        }


        public string AdharNo
        {
            get { return mAdharNo; }
            set { mAdharNo = value; }
        }
        public long LedgerDetailsNo
        {
            get { return mLedgerDetailsNo; }
            set { mLedgerDetailsNo = value; }
        }

        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for Address
        /// </summary>
        public string Address
        {
            get { return mAddress; }
            set { mAddress = value; }
        }
        /// <summary>
        /// This Properties use for StateNo
        /// </summary>
        public long StateNo
        {
            get { return mStateNo; }
            set { mStateNo = value; }
        }
        /// <summary>
        /// This Properties use for CityNo
        /// </summary>
        public long CityNo
        {
            get { return mCityNo; }
            set { mCityNo = value; }
        }
        /// <summary>
        /// This Properties use for PinCode
        /// </summary>
        public string PinCode
        {
            get { return mPinCode; }
            set { mPinCode = value; }
        }
        /// <summary>
        /// This Properties use for PhNo1
        /// </summary>
        public string PhNo1
        {
            get { return mPhNo1; }
            set { mPhNo1 = value; }
        }
        /// <summary>
        /// This Properties use for PhNo2
        /// </summary>
        public string PhNo2
        {
            get { return mPhNo2; }
            set { mPhNo2 = value; }
        }
        /// <summary>
        /// This Properties use for MobileNo1
        /// </summary>
        public string MobileNo1
        {
            get { return mMobileNo1; }
            set { mMobileNo1 = value; }
        }
        /// <summary>
        /// This Properties use for MobileNo2
        /// </summary>
        public string MobileNo2
        {
            get { return mMobileNo2; }
            set { mMobileNo2 = value; }
        }
        /// <summary>
        /// This Properties use for EmailID
        /// </summary>
        public string EmailID
        {
            get { return mEmailID; }
            set { mEmailID = value; }
        }
        /// <summary>
        /// This Properties use for DOB
        /// </summary>
        public DateTime DOB
        {
            get { return mDOB; }
            set { mDOB = value; }
        }
        /// <summary>
        /// This Properties use for QualificationNo
        /// </summary>
        public long QualificationNo
        {
            get { return mQualificationNo; }
            set { mQualificationNo = value; }
        }
        /// <summary>
        /// This Properties use for OccupationNo
        /// </summary>
        public long OccupationNo
        {
            get { return mOccupationNo; }
            set { mOccupationNo = value; }
        }
        /// <summary>
        /// This Properties use for CustomerType
        /// </summary>
        public long CustomerType
        {
            get { return mCustomerType; }
            set { mCustomerType = value; }
        }
        /// <summary>
        /// This Properties use for CreditLimit
        /// </summary>
        public long BillPrintType
        {
            get { return mBillPrintType; }
            set { mBillPrintType = value; }
        }
        public double CreditLimit
        {
            get { return mCreditLimit; }
            set { mCreditLimit = value; }
        }

        public long CreditDays
        {
            get { return mCreditDays; }
            set { mCreditDays = value; }
        }

        /// <summary>
        /// This Properties use for PANNo
        /// </summary>
        public string PANNo
        {
            get { return mPANNo; }
            set { mPANNo = value; }
        }
        /// <summary>
        /// This Properties use for VATNo
        /// </summary>
        public string VATNo
        {
            get { return mVATNo; }
            set { mVATNo = value; }
        }
        /// <summary>
        /// This Properties use for CSTNo
        /// </summary>
        public string CSTNo
        {
            get { return mCSTNo; }
            set { mCSTNo = value; }
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
        /// This Properties use for Gender
        /// </summary>
        public long Gender
        {
            get { return mGender; }
            set { mGender = value; }
        }
        public string FSSAI
        {
            get { return mFSSAI; }
            set { mFSSAI = value; }
        }
        public string ServiceTaxNo
        {
            get { return mServiceTaxNo; }
            set { mServiceTaxNo = value; }
        }
        public long AreaNo
        {
            get { return mAreaNo; }
            set { mAreaNo = value; }
        }
        public long CustSizeNo
        {
            get { return mCustSizeNo; }
            set { mCustSizeNo = value; }
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
        /// This Properties use for ConsentDate
        /// </summary>
        public DateTime ConsentDate
        {
            get { return mConsentDate; }
            set { mConsentDate = value; }
        }
        public bool IsLBTApply
        {
            get { return mIsLBTApply; }
            set { mIsLBTApply = value; }
        }
        public string LBTNo
        {
            get { return mLBTNo; }
            set { mLBTNo = value; }
        }
        public string AccountNo
        {
            get { return mAccountNo; }
            set { mAccountNo = value; }
        }
        public string ReportName
        {
            get { return mReportName; }
            set { mReportName = value; }
        }
        public double Distance
        {
            get { return mDistance; }
            set { mDistance = value; }
        }

        /// <summary>
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
        public string AddressLang
        {
            get { return mAddressLang; }
            set { mAddressLang = value; }
        }
        public long RateTypeNo
        {
            get { return mRateTypeNo; }
            set { mRateTypeNo = value; }
        }
        public double DiscPer
        {
            get { return mDiscPer; }
            set { mDiscPer = value; }
        }
        public double DiscRs
        {
            get { return mDiscRs; }
            set { mDiscRs = value; }
        }

    }


    /// <summary>
    /// This Class use for MLedgerDistDetails
    /// </summary>
    public class MLedgerDistDetails
    {
        private long mPkSrNo;
        private long mLedgerNo;
        private string mDistAgentName;
        private string mMobileNo;
        private string mRemark;
        private int mStatusNo;
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
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        /// <summary>
        /// This Properties use for DistAgentName
        /// </summary>
        public string DistAgentName
        {
            get { return mDistAgentName; }
            set { mDistAgentName = value; }
        }
        /// <summary>
        /// This Properties use for MobileNo
        /// </summary>
        public string MobileNo
        {
            get { return mMobileNo; }
            set { mMobileNo = value; }
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

    public class MLedgerRateSetting
    {
        private long mPkSrNo;
        private long mLedgerNo;
        private long mItemNo;
        private long mUOMNo;
        private double mRate;
        private double mMRP;
        private double mDiscPercentage;
        private double mDiscAmount;
        private long mCompNo;
        private long mUserID;
        private DateTime mUserDate;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }
        public long UOMNo
        {
            get { return mUOMNo; }
            set { mUOMNo = value; }
        }
        public double Rate
        {
            get { return mRate; }
            set { mRate = value; }
        }
        public double MRP
        {
            get { return mMRP; }
            set { mMRP = value; }
        }
        public double DiscPercentage
        {
            get { return mDiscPercentage; }
            set { mDiscPercentage = value; }
        }
        public double DiscAmount
        {
            get { return mDiscAmount; }
            set { mDiscAmount = value; }
        }
        public long CompNo
        {
            get { return mCompNo; }
            set { mCompNo = value; }
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


}
