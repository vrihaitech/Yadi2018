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
    class DBMScheme
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();

        public static string strerrormsg;

        public bool AddMScheme(MScheme mscheme)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMScheme";

            cmd.Parameters.AddWithValue("@SchemeNo", mscheme.SchemeNo);

            cmd.Parameters.AddWithValue("@SchemeTypeNo", mscheme.SchemeTypeNo);

            cmd.Parameters.AddWithValue("@SchemeName", mscheme.SchemeName);

            cmd.Parameters.AddWithValue("@SchemeUserNo", mscheme.SchemeUserNo);

            cmd.Parameters.AddWithValue("@SchemeDate", mscheme.SchemeDate);

            cmd.Parameters.AddWithValue("@SchemePeriodFrom", mscheme.SchemePeriodFrom);

            cmd.Parameters.AddWithValue("@SchemePeriodTo", mscheme.SchemePeriodTo);

            cmd.Parameters.AddWithValue("@SchemeRedPeriodFrom", mscheme.SchemeRedPeriodFrom);

            cmd.Parameters.AddWithValue("@SchemeRedPeriodTo", mscheme.SchemeRedPeriodTo);

            cmd.Parameters.AddWithValue("@SchemeWorth", mscheme.SchemeWorth);

            cmd.Parameters.AddWithValue("@CompanyNo", mscheme.CompanyNo);

            cmd.Parameters.AddWithValue("@IsActive", mscheme.IsActive);

            cmd.Parameters.AddWithValue("@DiscType", mscheme.DiscType);

            cmd.Parameters.AddWithValue("@UserID", mscheme.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mscheme.UserDate);

            cmd.Parameters.AddWithValue("@SponcorNo", mscheme.SponcorNo);

            cmd.Parameters.AddWithValue("@CampaignID", mscheme.CampaignID);

            cmd.Parameters.AddWithValue("@IsIWScheme", mscheme.IsIWScheme);

            cmd.Parameters.AddWithValue("@CustomerType", mscheme.CustomerType);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMSchemeFromDetails(MSchemeFromDetails mschemefromdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSchemeFromDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemefromdetails.PkSrNo);

            //   cmd.Parameters.AddWithValue("@SchemeDetailsNo", mschemefromdetails.SchemeDetailsNo);

            cmd.Parameters.AddWithValue("@BillAmount", mschemefromdetails.BillAmount);

            cmd.Parameters.AddWithValue("@ItemNo", mschemefromdetails.ItemNo);

            cmd.Parameters.AddWithValue("@Quantity", mschemefromdetails.Quantity);

            cmd.Parameters.AddWithValue("@Rate", mschemefromdetails.Rate);

            cmd.Parameters.AddWithValue("@MRP", mschemefromdetails.MRP);

            cmd.Parameters.AddWithValue("@Amount", mschemefromdetails.Amount);

            cmd.Parameters.AddWithValue("@UomNo", mschemefromdetails.UomNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", mschemefromdetails.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@DistPercentage", mschemefromdetails.DiscPercentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mschemefromdetails.CompanyNo);


            cmd.Parameters.AddWithValue("@UserID", mschemefromdetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mschemefromdetails.UserDate);



            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMSchemeToDetails(MSchemeToDetails mschemetodetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSchemeToDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemetodetails.PkSrNo);

            //   cmd.Parameters.AddWithValue("@SchemeDetailsNo", mschemetodetails.SchemeDetailsNo);

            cmd.Parameters.AddWithValue("@BillAmount", mschemetodetails.BillAmount);

            cmd.Parameters.AddWithValue("@ItemNo", mschemetodetails.ItemNo);

            cmd.Parameters.AddWithValue("@Quantity", mschemetodetails.Quantity);

            cmd.Parameters.AddWithValue("@Rate", mschemetodetails.Rate);

            cmd.Parameters.AddWithValue("@MRP", mschemetodetails.MRP);

            cmd.Parameters.AddWithValue("@Amount", mschemetodetails.Amount);

            cmd.Parameters.AddWithValue("@UomNo", mschemetodetails.UomNo);

            cmd.Parameters.AddWithValue("@FkRateSettingNo", mschemetodetails.FkRateSettingNo);

            cmd.Parameters.AddWithValue("@DiscPercentage", mschemetodetails.DiscPercentage);

            cmd.Parameters.AddWithValue("@CompanyNo", mschemetodetails.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mschemetodetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mschemetodetails.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMSchemeDetails(MSchemeDetails mschemedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSchemeDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemedetails.PkSrNo);

            //cmd.Parameters.AddWithValue("@SchemeNo", mschemedetails.SchemeNo);

            cmd.Parameters.AddWithValue("@DiscPercentage", mschemedetails.DiscPercentage);

            cmd.Parameters.AddWithValue("@DiscAmount", mschemedetails.DiscAmount);

            cmd.Parameters.AddWithValue("@CompanyNo", mschemedetails.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mschemedetails.UserID);

            cmd.Parameters.AddWithValue("@IsActive", mschemedetails.IsActive);

            cmd.Parameters.AddWithValue("@UserDate", mschemedetails.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMScheme(MScheme mScheme)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMScheme";

            cmd.Parameters.AddWithValue("@SchemeNo", mScheme.SchemeNo);

            cmd.Parameters.AddWithValue("@IsActive", mScheme.IsActive);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMSchemeDetails(MSchemeDetails mschemedetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMSchemeDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemedetails.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMSchemeToDetails(MSchemeToDetails mschemetodetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMSchemeToDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemetodetails.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMSchemeFromDetails(MSchemeFromDetails mschemefromdetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMSchemeFromDetails";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemefromdetails.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }



        public MScheme ModifyMSchemeByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MScheme where SchemeNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MScheme MM = new MScheme();
                while (dr.Read())
                {
                    MM.SchemeNo = Convert.ToInt32(dr["SchemeNo"]);
                    if (!Convert.IsDBNull(dr["SchemeTypeNo"])) MM.SchemeTypeNo = Convert.ToInt64(dr["SchemeTypeNo"]);
                    if (!Convert.IsDBNull(dr["SchemeName"])) MM.SchemeName = Convert.ToString(dr["SchemeName"]);
                    if (!Convert.IsDBNull(dr["SchemeUserNo"])) MM.SchemeUserNo = Convert.ToString(dr["SchemeUserNo"]);
                    if (!Convert.IsDBNull(dr["SchemeDate"])) MM.SchemeDate = Convert.ToDateTime(dr["SchemeDate"]);
                    if (!Convert.IsDBNull(dr["SchemePeriodFrom"])) MM.SchemePeriodFrom = Convert.ToDateTime(dr["SchemePeriodFrom"]);
                    if (!Convert.IsDBNull(dr["SchemePeriodTo"])) MM.SchemePeriodTo = Convert.ToDateTime(dr["SchemePeriodTo"]);
                    if (!Convert.IsDBNull(dr["SchemeRedPeriodFrom"])) MM.SchemeRedPeriodFrom = Convert.ToDateTime(dr["SchemeRedPeriodFrom"]);
                    if (!Convert.IsDBNull(dr["SchemeRedPeriodTo"])) MM.SchemeRedPeriodTo = Convert.ToDateTime(dr["SchemeRedPeriodTo"]);
                    if (!Convert.IsDBNull(dr["SchemeWorth"])) MM.SchemeWorth = Convert.ToString(dr["SchemeWorth"]);
                    if (!Convert.IsDBNull(dr["DiscType"])) MM.DiscType = Convert.ToInt64(dr["DiscType"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["SponcorNo"])) MM.SponcorNo = Convert.ToInt64(dr["SponcorNo"]);
                    if (!Convert.IsDBNull(dr["CampaignID"])) MM.CampaignID = Convert.ToString(dr["CampaignID"]);
                    if (!Convert.IsDBNull(dr["IsIWScheme"])) MM.IsIWScheme = Convert.ToInt32(dr["IsIWScheme"]);
                    if (!Convert.IsDBNull(dr["CustomerType"])) MM.CustomerType = Convert.ToInt32(dr["CustomerType"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MScheme();
        }

        public MSchemeFromDetails ModifyMSchemeFromDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MSchemeFromDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MSchemeFromDetails MM = new MSchemeFromDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["SchemeDetailsNo"])) MM.SchemeDetailsNo = Convert.ToInt64(dr["SchemeDetailsNo"]);
                    if (!Convert.IsDBNull(dr["BillAmount"])) MM.BillAmount = Convert.ToInt64(dr["BillAmount"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["Quantity"])) MM.Quantity = Convert.ToInt64(dr["Quantity"]);
                    if (!Convert.IsDBNull(dr["Rate"])) MM.Rate = Convert.ToInt64(dr["Rate"]);
                    if (!Convert.IsDBNull(dr["Amount"])) MM.Amount = Convert.ToInt64(dr["Amount"]);
                    if (!Convert.IsDBNull(dr["DistPercentage"])) MM.DiscPercentage = Convert.ToInt64(dr["DistPercentage"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MSchemeFromDetails();
        }

        public MSchemeDetails ModifyMSchemeDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MSchemeDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MSchemeDetails MM = new MSchemeDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["SchemeNo"])) MM.SchemeNo = Convert.ToInt64(dr["SchemeNo"]);
                    if (!Convert.IsDBNull(dr["DiscPercentage"])) MM.DiscPercentage = Convert.ToInt64(dr["DiscPercentage"]);
                    if (!Convert.IsDBNull(dr["DiscAmount"])) MM.DiscAmount = Convert.ToInt64(dr["DiscAmount"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MSchemeDetails();
        }

        public MSchemeToDetails ModifyMSchemeToDetailsByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MSchemeToDetails where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MSchemeToDetails MM = new MSchemeToDetails();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["SchemeDetailsNo"])) MM.SchemeDetailsNo = Convert.ToInt64(dr["SchemeDetailsNo"]);
                    if (!Convert.IsDBNull(dr["BillAmount"])) MM.BillAmount = Convert.ToInt64(dr["BillAmount"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["Quantity"])) MM.Quantity = Convert.ToInt64(dr["Quantity"]);
                    if (!Convert.IsDBNull(dr["Rate"])) MM.Rate = Convert.ToInt64(dr["Rate"]);
                    if (!Convert.IsDBNull(dr["Amount"])) MM.Amount = Convert.ToInt64(dr["Amount"]);
                    if (!Convert.IsDBNull(dr["DiscPercentage"])) MM.DiscPercentage = Convert.ToInt64(dr["DiscPercentage"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MSchemeToDetails();
        }


        public DataView GetAllMScheme()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MScheme order by SchemeNo";
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

        public DataView GetAllMSchemeFromDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSchemeFromDetails order by PkSrNo";
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

        public DataView GetAllMSchemeToDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSchemeToDetails order by PkSrNo";
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

        public DataView GetAllMSchemeDetails()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSchemeDetails order by PkSrNo";
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




        public DataView GetAllMScheme(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MScheme Where SchemeNo=" + ID;
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

        public DataView GetAllMSchemeFromDetails(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSchemeFromDetails Where PkSrNo=" + ID;
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

        public DataView GetAllMSchemeToDetails(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSchemeToDetails Where PkSrNo=" + ID;
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

        public DataView GetAllMSchemeDetails(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MSchemeDetails Where PkSrNo=" + ID;
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

        public DataView GetBySearch(string Column, string Value,long SchemeType)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "SELECT SchemeNo, SchemeUserNo As 'Scheme Number', SchemeName As 'Scheme Name', SchemeDate, SchemePeriodFrom As 'From', SchemePeriodTo As 'To',IsActive AS 'Status' FROM MScheme WHERE (SchemeTypeNo = " + SchemeType + ")";
                    break;
                case "SchemeName":
                    sql = "SELECT SchemeNo, SchemeUserNo As 'Scheme Number', SchemeName As 'Scheme Name', SchemeDate, SchemePeriodFrom As 'From' , SchemePeriodTo As 'To',IsActive AS 'Status' FROM MScheme WHERE (SchemeTypeNo = " + SchemeType + ") And  " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
                    break;
                case "SchemeUserNo":
                    sql = "SELECT SchemeNo, SchemeUserNo As 'Scheme Number', SchemeName As 'Scheme Name', SchemeDate, SchemePeriodFrom As 'From', SchemePeriodTo As 'To', IsActive AS 'Status' FROM MScheme WHERE (SchemeTypeNo = " + SchemeType + ") And  " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
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

        public bool AddMSchemeAssign(MSchemeAssign mschemeassign)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSchemeAssign";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemeassign.PkSrNo);

            cmd.Parameters.AddWithValue("@AssignDate", mschemeassign.AssignDate);

            //cmd.Parameters.AddWithValue("@SchemeNo", mschemeassign.SchemeNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mschemeassign.LedgerNo);

            cmd.Parameters.AddWithValue("@IsActive", mschemeassign.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mschemeassign.CompanyNo);

            cmd.Parameters.AddWithValue("@PromoCode", mschemeassign.PromoCode);

            cmd.Parameters.AddWithValue("@NoOfTimes", mschemeassign.NoOfTimes);

            cmd.Parameters.AddWithValue("@UserID", mschemeassign.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mschemeassign.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMSchemeAssign1(MSchemeAssign mschemeassign)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSchemeAssign1";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemeassign.PkSrNo);

            cmd.Parameters.AddWithValue("@AssignDate", mschemeassign.AssignDate);

            cmd.Parameters.AddWithValue("@SchemeNo", mschemeassign.SchemeNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mschemeassign.LedgerNo);

            cmd.Parameters.AddWithValue("@IsActive", mschemeassign.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mschemeassign.CompanyNo);

            cmd.Parameters.AddWithValue("@PromoCode", mschemeassign.PromoCode);

            cmd.Parameters.AddWithValue("@NoOfTimes", mschemeassign.NoOfTimes);

            cmd.Parameters.AddWithValue("@UserID", mschemeassign.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mschemeassign.UserDate);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMSchemeAssign(MSchemeAssign mschemeassign)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMSchemeAssign";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemeassign.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMSchemeAchievers(MSchemeAchievers mschemeachievers)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMSchemeAchievers";

            cmd.Parameters.AddWithValue("@PkSrNo", mschemeachievers.PkSrNo);

            cmd.Parameters.AddWithValue("@LedgerNo", mschemeachievers.LedgerNo);

            cmd.Parameters.AddWithValue("@SchemeNo", mschemeachievers.SchemeNo);

            cmd.Parameters.AddWithValue("@SchemeDetailsNo", mschemeachievers.SchemeDetailsNo);

            cmd.Parameters.AddWithValue("@SlabAmount", mschemeachievers.SlabAmount);

            cmd.Parameters.AddWithValue("@SlabDiscAmt", mschemeachievers.SlabDiscAmt);

            cmd.Parameters.AddWithValue("@SlabBalanceAmount", mschemeachievers.SlabBalanceAmount);

            cmd.Parameters.AddWithValue("@IsActive", mschemeachievers.IsActive);

            cmd.Parameters.AddWithValue("@CompanyNo", mschemeachievers.CompanyNo);

            cmd.Parameters.AddWithValue("@UserID", mschemeachievers.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mschemeachievers.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int64;
            cmd.Parameters.Add(outParameter);


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

            //cmd.Parameters.AddWithValue("@SchemeAchieverNo", mschemeachieverdetails.SchemeAchieverNo);

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

        public bool AddMSchemeAchieverDetails1(MSchemeAchieverDetails mschemeachieverdetails)
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

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mschemeachieverdetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int cntScheme = -1, cntSchemeDtls = 0, cntAssign = -1, cntAch = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddMScheme")
                        {
                            cntScheme = i;

                        }
                        if (commandcollection[i].CommandText == "AddMSchemeDetails")
                        {
                            cntSchemeDtls = i;
                            commandcollection[i].Parameters.AddWithValue("@SchemeNo", commandcollection[cntScheme].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMSchemeFromDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@SchemeDetailsNo", commandcollection[cntSchemeDtls].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@SchemeNo", commandcollection[cntScheme].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMSchemeToDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@SchemeDetailsNo", commandcollection[cntSchemeDtls].Parameters["@ReturnID"].Value);
                            commandcollection[i].Parameters.AddWithValue("@SchemeNo", commandcollection[cntScheme].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMSchemeAssign")
                        {
                            commandcollection[i].Parameters.AddWithValue("@SchemeNo", commandcollection[cntScheme].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMSchemeAssign1")
                        {
                            commandcollection[i].CommandText = "AddMSchemeAssign";
                            cntAssign = i;
                        }
                        if (commandcollection[i].CommandText == "AddMSchemeAchievers")
                        {
                            cntAch = i;
                        }
                        if (commandcollection[i].CommandText == "AddMSchemeAchieverDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@SchemeAchieverNo", commandcollection[cntAch].Parameters["@ReturnID"].Value);
                        }

                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                if (cntScheme == -1)
                {
                    if (cntAssign == -1)
                    {
                        if (cntAch == -1)
                            return 0;
                        else
                            return 1;
                    }
                    else return 1;
                }
                else
                    return Convert.ToInt64(commandcollection[cntScheme].Parameters["@ReturnID"].Value);
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

        public bool ExecuteNonQueryStatementsScheme()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            //int cntScheme = -1, cntSchemeDtls = 0, cntAssign = -1;//, cntAch = -1;
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


        public bool UpdateMScheme(long SchemeNo)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update MScheme set IsActive=1 where SchemeNo=@SchemeNo";

            cmd.Parameters.AddWithValue("@SchemeNo", SchemeNo);

            commandcollection.Add(cmd);
            return true;
        }

    }


    /// <summary>
    /// This Class use for MScheme
    /// </summary>
    public class MScheme
    {
        private long mSchemeNo;
        private long mSchemeTypeNo;
        private string mSchemeName;
        private string mSchemeUserNo;
        private DateTime mSchemeDate;
        private DateTime mSchemePeriodFrom;
        private DateTime mSchemePeriodTo;
        private DateTime mSchemeRedPeriodFrom;
        private DateTime mSchemeRedPeriodTo;
        private string mSchemeWorth;
        private long mCompanyNo;
        private long mStatusNo;
        private long mDiscType;
        private int mIsActive;
        private long mUserID;
        private DateTime mUserDate;
        private long mSponcorNo;
        private string mCampaignID;
        private int mIsIWScheme;
        private int mCustomerType;
        private string Mmsg;

        /// <summary>
        /// This Properties use for SchemeNo
        /// </summary>
        public long SchemeNo
        {
            get { return mSchemeNo; }
            set { mSchemeNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeTypeNo
        /// </summary>
        public long SchemeTypeNo
        {
            get { return mSchemeTypeNo; }
            set { mSchemeTypeNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeName
        /// </summary>
        public string SchemeName
        {
            get { return mSchemeName; }
            set { mSchemeName = value; }
        }
        /// <summary>
        /// This Properties use for SchemeUserNo
        /// </summary>
        public string SchemeUserNo
        {
            get { return mSchemeUserNo; }
            set { mSchemeUserNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeDate
        /// </summary>
        public DateTime SchemeDate
        {
            get { return mSchemeDate; }
            set { mSchemeDate = value; }
        }
        /// <summary>
        /// This Properties use for SchemePeriodFrom
        /// </summary>
        public DateTime SchemePeriodFrom
        {
            get { return mSchemePeriodFrom; }
            set { mSchemePeriodFrom = value; }
        }
        /// <summary>
        /// This Properties use for SchemePeriodTo
        /// </summary>
        public DateTime SchemePeriodTo
        {
            get { return mSchemePeriodTo; }
            set { mSchemePeriodTo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeRedPeriodFrom
        /// </summary>
        public DateTime SchemeRedPeriodFrom
        {
            get { return mSchemeRedPeriodFrom; }
            set { mSchemeRedPeriodFrom = value; }
        }
        /// <summary>
        /// This Properties use for SchemeRedPeriodTo
        /// </summary>
        public DateTime SchemeRedPeriodTo
        {
            get { return mSchemeRedPeriodTo; }
            set { mSchemeRedPeriodTo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeWorth
        /// </summary>
        public string SchemeWorth
        {
            get { return mSchemeWorth; }
            set { mSchemeWorth = value; }
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
        /// This Properties use for DiscType
        /// </summary>
        public long DiscType
        {
            get { return mDiscType; }
            set { mDiscType = value; }
        }
        /// <summary>
        /// This Properties use for IsActive
        /// </summary>
        public int IsActive
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
        /// This Properties use for SponcorNo
        /// </summary>
        public long SponcorNo
        {
            get { return mSponcorNo; }
            set { mSponcorNo = value; }
        }
        /// <summary>
        /// This Properties use for CampaignID
        /// </summary>
        public string CampaignID
        {
            get { return mCampaignID; }
            set { mCampaignID = value; }
        }
        /// <summary>
        /// This Properties use for IsIWScheme
        /// </summary>
        public int IsIWScheme
        {
            get { return mIsIWScheme; }
            set { mIsIWScheme = value; }
        }
        /// <summary>
        /// This Properties use for CustomerType
        /// </summary>
        public int CustomerType
        {
            get { return mCustomerType; }
            set { mCustomerType = value; }
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
    /// This Class use for MSchemeDetails
    /// </summary>
    public class MSchemeDetails
    {
        private long mPkSrNo;
        private long mSchemeNo;
        private double mDiscPercentage;
        private double mDiscAmount;
        private long mCompanyNo;
        private long mStatusNo;
        private bool mIsActive;
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
        /// This Properties use for SchemeNo
        /// </summary>
        public long SchemeNo
        {
            get { return mSchemeNo; }
            set { mSchemeNo = value; }
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
        /// This Properties use for msg
        /// </summary>
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    /// <summary>
    /// This Class use for MSchemeFromDetails
    /// </summary>
    public class MSchemeFromDetails
    {
        private long mPkSrNo;
        private long mSchemeDetailsNo;
        private long mSchemeNo;
        private double mBillAmount;
        private long mItemNo;
        private double mQuantity;
        private double mRate;
        private double mMRP;
        private double mAmount;
        private long mUomNo;
        private long mFkRateSettingNo;
        private double mDistPercentage;
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
        /// This Properties use for SchemeDetailsNo
        /// </summary>
        public long SchemeDetailsNo
        {
            get { return mSchemeDetailsNo; }
            set { mSchemeDetailsNo = value; }
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
        /// This Properties use for BillAmount
        /// </summary>
        public double BillAmount
        {
            get { return mBillAmount; }
            set { mBillAmount = value; }
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
        /// This Properties use for Rate
        /// </summary>
        public double Rate
        {
            get { return mRate; }
            set { mRate = value; }
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
        /// This Properties use for Amount
        /// </summary>
        public double Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        /// <summary>
        /// This Properties use for UomNo
        /// </summary>
        public long UomNo
        {
            get { return mUomNo; }
            set { mUomNo = value; }
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
        /// This Properties use for DistPercentage
        /// </summary>
        public double DiscPercentage
        {
            get { return mDistPercentage; }
            set { mDistPercentage = value; }
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
    /// This Class use for MSchemeToDetails
    /// </summary>
    public class MSchemeToDetails
    {
        private long mPkSrNo;
        private long mSchemeDetailsNo;
        private long mSchemeNo;
        private double mBillAmount;
        private long mItemNo;
        private double mQuantity;
        private double mRate;
        private double mMRP;
        private double mAmount;
        private long mUomNo;
        private long mFkRateSettingNo;
        private double mDiscPercentage;
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
        /// This Properties use for SchemeDetailsNo
        /// </summary>
        public long SchemeDetailsNo
        {
            get { return mSchemeDetailsNo; }
            set { mSchemeDetailsNo = value; }
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
        /// This Properties use for BillAmount
        /// </summary>
        public double BillAmount
        {
            get { return mBillAmount; }
            set { mBillAmount = value; }
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
        /// This Properties use for Rate
        /// </summary>
        public double Rate
        {
            get { return mRate; }
            set { mRate = value; }
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
        /// This Properties use for Amount
        /// </summary>
        public double Amount
        {
            get { return mAmount; }
            set { mAmount = value; }
        }
        /// <summary>
        /// This Properties use for UomNo
        /// </summary>
        public long UomNo
        {
            get { return mUomNo; }
            set { mUomNo = value; }
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
        /// This Properties use for DiscPercentage
        /// </summary>
        public double DiscPercentage
        {
            get { return mDiscPercentage; }
            set { mDiscPercentage = value; }
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
    /// This Class use for MSchemeAssign
    /// </summary>
    public class MSchemeAssign
    {
        private long mPkSrNo;
        private DateTime mAssignDate;
        private long mSchemeNo;
        private long mLedgerNo;
        private bool mIsActive;
        private long mStatusNo;
        private long mCompanyNo;
        private string mPromoCode;
        private int mNoOfTimes;
        private int mIsSchemeUsed;
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
        /// This Properties use for AssignDate
        /// </summary>
        public DateTime AssignDate
        {
            get { return mAssignDate; }
            set { mAssignDate = value; }
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
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
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
        /// This Properties use for PromoCode
        /// </summary>
        public string PromoCode
        {
            get { return mPromoCode; }
            set { mPromoCode = value; }
        }
        /// <summary>
        /// This Properties use for NoOfTimes
        /// </summary>
        public int NoOfTimes
        {
            get { return mNoOfTimes; }
            set { mNoOfTimes = value; }
        }
        /// <summary>
        /// This Properties use for IsSchemeUsed
        /// </summary>
        public int IsSchemeUsed
        {
            get { return mIsSchemeUsed; }
            set { mIsSchemeUsed = value; }
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
    /// This Class use for MSchemeAchievers
    /// </summary>
    public class MSchemeAchievers
    {
        private long mPkSrNo;
        private long mLedgerNo;
        private long mSchemeNo;
        private long mSchemeDetailsNo;
        private double mSlabAmount;
        private double mSlabDiscAmt;
        private double mSlabBalanceAmount;
        private bool mIsItemDiscStatus;
        private bool mIsActive;
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
        /// This Properties use for LedgerNo
        /// </summary>
        public long LedgerNo
        {
            get { return mLedgerNo; }
            set { mLedgerNo = value; }
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
        /// This Properties use for SlabAmount
        /// </summary>
        public double SlabAmount
        {
            get { return mSlabAmount; }
            set { mSlabAmount = value; }
        }
        /// <summary>
        /// This Properties use for SlabDiscAmt
        /// </summary>
        public double SlabDiscAmt
        {
            get { return mSlabDiscAmt; }
            set { mSlabDiscAmt = value; }
        }
        /// <summary>
        /// This Properties use for SlabBalanceAmount
        /// </summary>
        public double SlabBalanceAmount
        {
            get { return mSlabBalanceAmount; }
            set { mSlabBalanceAmount = value; }
        }
        /// <summary>
        /// This Properties use for IsItemDiscStatus
        /// </summary>
        public bool IsItemDiscStatus
        {
            get { return mIsItemDiscStatus; }
            set { mIsItemDiscStatus = value; }
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
    /// This Class use for MSchemeAchieverDetails
    /// </summary>
    public class MSchemeAchieverDetails
    {
        private long mPkSrNo;
        private long mSchemeAchSrNo;
        private DateTime mSchemeAchDate;
        private long mSchemeAchieverNo;
        private long mLedgerNo;
        private long mRefNo;
        private long mTypeOfRef;
        private double mAmount;
        private int mSignCode;
        private long mCompanyNo;
        private int mStatusNo;
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
        /// This Properties use for SchemeAchSrNo
        /// </summary>
        public long SchemeAchSrNo
        {
            get { return mSchemeAchSrNo; }
            set { mSchemeAchSrNo = value; }
        }
        /// <summary>
        /// This Properties use for SchemeAchDate
        /// </summary>
        public DateTime SchemeAchDate
        {
            get { return mSchemeAchDate; }
            set { mSchemeAchDate = value; }
        }
        /// <summary>
        /// This Properties use for SchemeAchieverNo
        /// </summary>
        public long SchemeAchieverNo
        {
            get { return mSchemeAchieverNo; }
            set { mSchemeAchieverNo = value; }
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
        public long TypeOfRef
        {
            get { return mTypeOfRef; }
            set { mTypeOfRef = value; }
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
        /// This Properties use for SignCode
        /// </summary>
        public int SignCode
        {
            get { return mSignCode; }
            set { mSignCode = value; }
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
