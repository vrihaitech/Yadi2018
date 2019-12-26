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
    class DBMRank
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;
        public bool AddMRacInv(MRank mRank)
        {

           
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRacInv";

            cmd.Parameters.AddWithValue("@RacInvNo", mRank.RacInvNo);

            //cmd.Parameters.AddWithValue("@CityName", mcity.CityName);

            cmd.Parameters.AddWithValue("@ItemNo", mRank.ItemNo);

           // cmd.Parameters.AddWithValue("@Uom", mRank.UOM);
           // cmd.Parameters.AddWithValue("@Quantity", mRank.Quantity);
    
            //cmd.Parameters.AddWithValue("@CountryNo", mcity.CountryNo);

            //cmd.Parameters.AddWithValue("@StateNo", mcity.StateNo);

            //cmd.Parameters.AddWithValue("@RegionNo", mcity.RegionNo);

            cmd.Parameters.AddWithValue("@IsActive", mRank.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mRank.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mRank.UserDate);


            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
            
            // cmd.Parameters.AddWithValue("@CompanyNo", mcity.CompanyNo);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mcity.ModifiedBy);
            //if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    mRank.msg = ObjTrans.ErrorMessage;
            //    return false;
            //}
        }




        public bool AddMRacDeatils(MRacDetils mRacDetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRacDetails";

            cmd.Parameters.AddWithValue("@RacDetailsNo", mRacDetails.RacDetailsNo);

            cmd.Parameters.AddWithValue("@ItemNo", mRacDetails.ItemNo);

         //   cmd.Parameters.AddWithValue("@RacInvNo", mRacDetails.RacInvNo);


            cmd.Parameters.AddWithValue("@RacDocNo", mRacDetails.RacDocNo);
            cmd.Parameters.AddWithValue("@Quantity", mRacDetails.Quantity);
             cmd.Parameters.AddWithValue("@UomNo", mRacDetails.UOMNo);

            cmd.Parameters.AddWithValue("@GodOwnNo", mRacDetails.GodOwnNo);

            //cmd.Parameters.AddWithValue("@StateNo", mcity.StateNo);

            //cmd.Parameters.AddWithValue("@RegionNo", mcity.RegionNo);

            cmd.Parameters.AddWithValue("@IsActive", mRacDetails.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mRacDetails.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mRacDetails.UserDate);

            // cmd.Parameters.AddWithValue("@CompanyNo", mcity.CompanyNo);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mcity.ModifiedBy);


            //if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    mRacDetails.msg = ObjTrans.ErrorMessage;
            //    return false;
            //}
            //SqlParameter outParameter = new SqlParameter();
            //outParameter.ParameterName = "@ReturnID";
            //outParameter.Direction = ParameterDirection.Output;
            //outParameter.DbType = DbType.Int32;
            //cmd.Parameters.Add(outParameter);
            commandcollection.Add(cmd);
            return true;
        }

        public bool DeletemRank(MRank mRank)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMCity";

            cmd.Parameters.AddWithValue("@RankNo", mRank.RacInvNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mRank.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMRank()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from Mrank order by RankNo";
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

        public DataView GetMRankByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MRank where RankNo =" + ID;
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

        public MRacDetils ModifyMRackByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from mRacdetails where RacDetailsNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MRacDetils MM = new MRacDetils();
                while (dr.Read())
                {
                    MM.RacInvNo = Convert.ToInt32(dr["RacDetailsNo"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt32(dr["ItemNo"]);

                    if (!Convert.IsDBNull(dr["Quantity"])) MM.Quantity = Convert.ToInt32(dr["Quantity"]);
                    if (!Convert.IsDBNull(dr["UOMNo"])) MM.UOMNo = Convert.ToInt32(dr["UOMNo"]);
                    //if (!Convert.IsDBNull(dr["CityShortCode"])) MM.CityShortCode = Convert.ToString(dr["CityShortCode"]);
                   // if (!Convert.IsDBNull(dr["CityLangName"])) MM.CityLangName = Convert.ToString(dr["CityLangName"]);
                   // if (!Convert.IsDBNull(dr["CountryNo"])) MM.CountryNo = Convert.ToInt64(dr["CountryNo"]);
                 //   if (!Convert.IsDBNull(dr["StateNo"])) MM.StateNo = Convert.ToInt64(dr["StateNo"]);
                   // if (!Convert.IsDBNull(dr["RegionNo"])) MM.RegionNo = Convert.ToInt64(dr["RegionNo"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    //if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                  //  if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);

                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MRacDetils();
        }


        public bool ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();
            
            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;


            int cntVchNo = -1, cntRef = 0, cntRateSettingNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddMRacInv")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddMRacDetails")
                        {
                            cntVchNo = i;
                            //commandcollection[i].Parameters.AddWithValue("@RacInvNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            //  if (cntRef != 0)
                            //    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            //else
                            //    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                            //cntStock = i;
                            //if (cntRateSettingNo != -1)
                            //{
                            //    commandcollection[i].Parameters["@FkRateSettingNo"].Value = commandcollection[cntRateSettingNo].Parameters["@ReturnID"].Value;

                            //    //commandcollection[i].CommandText.IndexOf("@FkRateSettingNo", Convert.ToInt32(commandcollection[cntRateSettingNo].Parameters["@ReturnID"].Value));

                            //    cntRateSettingNo = -1;
                            //}
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
                    return false;
                else
                    return true;
                    //return Convert.(commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
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

        public bool DeleteMRaC(MRacDetils mRacDetails)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMRac";

            cmd.Parameters.AddWithValue("@RacDocNo", mRacDetails.RacDocNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mRacDetails.msg = ObjTrans.ErrorMessage;
                return false;
            }
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
                commandcollection.Clear();
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

        //public DataView GetBySearch(string Column, string Value)
        //{

        //    string sql = null;
        //    switch (Column)
        //    {
        //        case "0":
        //            sql = "Select RankNo,CityShortCode AS 'Short Name',CityName AS 'City Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCity order by CityName ";
        //            break;
        //        case "CityName":
        //            sql = "Select CityNo,CityShortCode AS 'Short Name',CityName AS 'City Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCity where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by CityName";
        //            break;
        //        case "CityShortCode":
        //            sql = "Select CityNo,CityShortCode AS 'Short Name',CityName AS 'City Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MCity where " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' order by CityName";
        //            break;
        //    }
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
        //    }
        //    catch (SqlException e)
        //    {
        //        CommonFunctions.ErrorMessge = e.Message;
        //    }
        //    return ds.Tables[0].DefaultView;
        //}
    }

    /// <summary>
    /// This Class use for MCity
    /// </summary
    /// 

    public class MRacDetils
    {
        private long mRacDetailsNo; 
        private int mRacInvNo;
        //  private string mCityName;
        private int mUomNo;
        private int mGodOwnNo;
        private int mRacDocNo;
        //private string mCityLangName;
        //private long mCountryNo;
        //private long mStateNo;
        //private long mRegionNo;
        private int mItemNo;
        private bool mIsActive;
        private long mUserID;
        private double mQuantity;
       // private int muom;
        private DateTime mUserDate;
        private string mModifiedBy;
        //////private long mCompanyNo;
        //////private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for CityNo
        /// </summary>
        public long RacDetailsNo
        {
            get { return mRacDetailsNo; }
            set { mRacDetailsNo = value; }
        }

        
             public int RacDocNo
        {
            get { return mRacDocNo; }
            set { mRacDocNo = value; }
        }
        public int GodOwnNo
        {
            get { return mGodOwnNo; }
            set { mGodOwnNo = value; }
        }

        public int RacInvNo
        {
            get { return mRacInvNo; }
            set { mRacInvNo = value; }
        }

        public int UOMNo
        {
            get { return mUomNo; }
            set { mUomNo = value; }
        }

        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }

        /// <summary>
        /// This Properties use for CityName
        /// </summary>
        //public string CityName
        //{
        //    get { return mCityName; }
        //    set { mCityName = value; }
        //}
        /// <summary>
        /// This Properties use for CityShortCode
        /// </summary>
        public int ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }

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




        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }

    }

    public class MRank

    {
        private long mRacInvNo;
      //  private string mCityName;
        private int mItemNo;
        //private string mCityLangName;
        //private long mCountryNo;
        //private long mStateNo;
        //private long mRegionNo;
        private bool mIsActive;
        private long mUserID;
        private double mQuantity;
        private int muom;
        private DateTime mUserDate;
        private string mModifiedBy;
        //////private long mCompanyNo;
        //////private int mStatusNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for CityNo
        /// </summary>
        public long RacInvNo
        {
            get { return mRacInvNo; }
            set { mRacInvNo = value; }
        }

        public int UOM
        {
            get { return muom; }
            set { muom = value; }
        }
        /// <summary>
        /// This Properties use for CityName
        /// </summary>
        //public string CityName
        //{
        //    get { return mCityName; }
        //    set { mCityName = value; }
        //}
        /// <summary>
        /// This Properties use for CityShortCode
        /// </summary>
        public int ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
        }

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

        public double Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }    



        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
   
    }


}
