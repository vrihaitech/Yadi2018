using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using OMControls;

namespace OM
{
    class DBMRecipeMain
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMRecipeMain(MRecipeMain RecipeMain)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRecipeMain";

            cmd.Parameters.AddWithValue("@MRecipeID", RecipeMain.MRecipeID);

            cmd.Parameters.AddWithValue("@DocNo", RecipeMain.DocNo);

            cmd.Parameters.AddWithValue("@ESFlag", RecipeMain.ESFlag);

            cmd.Parameters.AddWithValue("@GroupNo ", RecipeMain.GroupNo);

            cmd.Parameters.AddWithValue("@FinishItemID", RecipeMain.FinishItemID);

            cmd.Parameters.AddWithValue("@PackingSize", RecipeMain.PackingSize);

            cmd.Parameters.AddWithValue("@RDate", RecipeMain.RDate);

            cmd.Parameters.AddWithValue("@Qty", RecipeMain.Qty);

            cmd.Parameters.AddWithValue("@ProdQty", RecipeMain.ProdQty);
            
            cmd.Parameters.AddWithValue("@UomNo", RecipeMain.UomNo);

            cmd.Parameters.AddWithValue("@RecipeType", RecipeMain.RecipeType);

            cmd.Parameters.AddWithValue("@FkRecipeID", RecipeMain.FkRecipeID); 

            cmd.Parameters.AddWithValue("@IsLock", RecipeMain.IsLock); 

            cmd.Parameters.AddWithValue("@IsActive", RecipeMain.IsActive);
            
            cmd.Parameters.AddWithValue("@UserID", RecipeMain.UserID);

            cmd.Parameters.AddWithValue("@UserDate", RecipeMain.UserDate);

            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMRecipeSub(MRecipeSub RecipeSub)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMRecipeSub";

            cmd.Parameters.AddWithValue("@SRecipeID", RecipeSub.SRecipeID);

            //cmd.Parameters.AddWithValue("@FKMRecipeID", RecipeSub.FKMRecipeID);

            cmd.Parameters.AddWithValue("@RawGroupNo", RecipeSub.RawGroupNo);

            cmd.Parameters.AddWithValue("@RawProductID ", RecipeSub.RawProductID);

            cmd.Parameters.AddWithValue("@Qty", RecipeSub.Qty);

            cmd.Parameters.AddWithValue("@UomNo", RecipeSub.UomNo);

            cmd.Parameters.AddWithValue("@ProductQty", RecipeSub.ProductQty);

            cmd.Parameters.AddWithValue("@Wastageper", RecipeSub.Wastageper);

            cmd.Parameters.AddWithValue("@WastagePerQty", RecipeSub.WastagePerQty);

            cmd.Parameters.AddWithValue("@FinalQty", RecipeSub.FinalQty);

            cmd.Parameters.AddWithValue("@IsActive", RecipeSub.IsActive);

            commandcollection.Add(cmd);
            return true;
        }

        public MRecipeMain ModifyMRecipeMainByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MRecipeMain where MRecipeID =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MRecipeMain MM = new MRecipeMain();
                while (dr.Read())
                {
                    MM.MRecipeID = Convert.ToInt32(dr["MRecipeID"]);
                    if (!Convert.IsDBNull(dr["DocNo"])) MM.DocNo = Convert.ToInt64(dr["DocNo"]);
                    if (!Convert.IsDBNull(dr["ESFlag"])) MM.ESFlag = Convert.ToBoolean(dr["ESFlag"]);
                    if (!Convert.IsDBNull(dr["GroupNo"])) MM.GroupNo = Convert.ToInt64(dr["GroupNo"]);
                    if (!Convert.IsDBNull(dr["FinishItemID"])) MM.FinishItemID = Convert.ToInt64(dr["FinishItemID"]);
                    if (!Convert.IsDBNull(dr["PackingSize"])) MM.PackingSize = Convert.ToInt64(dr["PackingSize"]);
                    if (!Convert.IsDBNull(dr["RDate"])) MM.RDate = Convert.ToDateTime(dr["RDate"]);
                    if (!Convert.IsDBNull(dr["Qty"])) MM.Qty = Convert.ToDouble(dr["Qty"]);
                    if (!Convert.IsDBNull(dr["ProdQty"])) MM.ProdQty = Convert.ToDouble(dr["ProdQty"]); 
                    if (!Convert.IsDBNull(dr["UomNo"])) MM.UomNo = Convert.ToInt64(dr["UomNo"]);
                    if (!Convert.IsDBNull(dr["RecipeType"])) MM.RecipeType = Convert.ToInt64(dr["RecipeType"]);
                    if (!Convert.IsDBNull(dr["FkRecipeID"])) MM.FkRecipeID = Convert.ToInt64(dr["FkRecipeID"]);
                    if (!Convert.IsDBNull(dr["IsLock"])) MM.IsLock = Convert.ToBoolean(dr["IsLock"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);                 
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MRecipeMain();
        }

        public bool DeleteMRecipeSub(MRecipeSub RecipeSub)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMRecipeSub";

            cmd.Parameters.AddWithValue("@SRecipeID", RecipeSub.SRecipeID);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                RecipeSub.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }
        public bool UpdateMRecipeSub(MRecipeSub RecipeSub)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpdateMRecipeSub";

            cmd.Parameters.AddWithValue("@SRecipeID", RecipeSub.SRecipeID);
            commandcollection.Add(cmd);
            return true;
            //if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            //{
            //    return true;
            //}
            //else
            //{
            //    RecipeSub.msg = ObjTrans.ErrorMessage;
            //    return false;
            //}
        }

        public DataView GetAllMRecipeSub()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MRecipeSub order by SRecipeID";
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

        public MRecipeSub ModifyMRecipeSubByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MRecipeSub where SRecipeID =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MRecipeSub MM = new MRecipeSub();
                while (dr.Read())
                {
                    MM.SRecipeID = Convert.ToInt32(dr["SRecipeID"]);
                    if (!Convert.IsDBNull(dr["FKMRecipeID"])) MM.FKMRecipeID = Convert.ToInt64(dr["FKMRecipeID"]);
                    if (!Convert.IsDBNull(dr["RawGroupNo"])) MM.RawGroupNo = Convert.ToInt64(dr["RawGroupNo"]);
                    if (!Convert.IsDBNull(dr["RawProductID"])) MM.RawProductID = Convert.ToInt64(dr["RawProductID"]);
                    if (!Convert.IsDBNull(dr["Qty"])) MM.Qty = Convert.ToDecimal(dr["Qty"]);
                    if (!Convert.IsDBNull(dr["UomNo"])) MM.UomNo = Convert.ToInt64(dr["UomNo"]);
                    if (!Convert.IsDBNull(dr["WastagePerQty"])) MM.WastagePerQty = Convert.ToDecimal(dr["WastagePerQty"]);
                    if (!Convert.IsDBNull(dr["Wastageper"])) MM.Wastageper = Convert.ToDecimal(dr["Wastageper"]);
                    if (!Convert.IsDBNull(dr["FinalQty"])) MM.FinalQty = Convert.ToDecimal(dr["FinalQty"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["ProductQty"])) MM.ProductQty = Convert.ToDouble(dr["ProductQty"]); 
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MRecipeSub();
        }

        public DataView GetMRecipeSubByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MRecipeSub where SRecipeID =" + ID;
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

            string sql = null;
            switch (Column)
            {
                case "0":
                    // " SELECT MItemMaster.ItemNo, MItemGroup.ItemGroupName + '-' + MItemMaster.ItemShortName  AS 'ShortName' , MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName  AS 'ItemName',Case when (MItemMaster.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                    //      " FROM MItemMaster INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo Where ItemNo<>1 order by MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName ";
                    //sql = "Select SRecipeID,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRecipeSub ";
                    sql = "SELECT MRecipeMain.MRecipeID, MItemGroup.ItemGroupName as 'GroupName',MItemMaster.ItemName as 'ItemName',Case when (MRecipeMain.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                           "FROM MRecipeMain INNER JOIN MItemGroup ON MRecipeMain.GroupNo = MItemGroup.ItemGroupNo INNER JOIN MItemMaster ON MRecipeMain.FinishItemID = MItemMaster.ItemNo";
          
                    break;
                case "GroupName":
                    //sql = "Select SRecipeID,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRecipeSub where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
                    // sql = "  SELECT MItemMaster.ItemNo, MItemGroup.ItemGroupName + '-' + MItemMaster.ItemShortName  AS 'ShortName' , MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName  AS 'ItemName',Case when (MItemMaster.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                    // " FROM MItemMaster INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo Where ItemNo<>1 AND MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName";
                    sql = "SELECT MRecipeMain.MRecipeID, MItemGroup.ItemGroupName as 'GroupName',MItemMaster.ItemName as 'ItemName',Case when (MRecipeMain.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                           "FROM MRecipeMain INNER JOIN MItemGroup ON MRecipeMain.GroupNo = MItemGroup.ItemGroupNo INNER JOIN MItemMaster ON MRecipeMain.FinishItemID = MItemMaster.ItemNo where MItemGroup.ItemGroupName like '%" + Value.Trim().Replace("'", "''") + "' + '%' ";

                    break;
                case "ItemName":
                    // sql = "Select SRecipeID,AreaShortCode AS 'Short Name',AreaName AS 'Area Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MRecipeSub where " + Column + " like '" + Value.Trim().Replace("'", "''") + "' + '%'";
                    sql = "SELECT  MRecipeMain.MRecipeID, MItemGroup.ItemGroupName as 'GroupName',MItemMaster.ItemName as 'ItemName',Case when (MRecipeMain.IsActive = 'True') Then 'True' Else 'False' End As 'Status' " +
                            "FROM MRecipeMain INNER JOIN MItemGroup ON MRecipeMain.GroupNo = MItemGroup.ItemGroupNo INNER JOIN MItemMaster ON MRecipeMain.FinishItemID = MItemMaster.ItemNo where MItemMaster.ItemName like '%" + Value.Trim().Replace("'", "''") + "' + '%' ";

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
            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            //cmd.Transaction = myTrans;
            int cntEntry = 0;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddMRecipeMain")
                        {
                            cntEntry = i;
                        }
                        if (commandcollection[i].CommandText == "AddMRecipeSub")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FKMRecipeID", commandcollection[cntEntry].Parameters["@ReturnID"].Value);
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
                commandcollection.Clear();
                cn.Close();
            }
            //________________________________________________________________________________________________________________________________________________________________________________________________________________________
        }
    }

    public class MRecipeSub
    {
        private long mSRecipeID;
        private long mFKMRecipeID;
        private long mRawGroupNo;
        private long mRawProductID;
        private decimal mQty;
        private long mUomNo;
        private double mProductQty;
        private decimal mWastageper;
        private decimal mWastagePerQty;
        private decimal mFinalQty;
        private bool mIsActive;
        private string Mmsg;

        public long SRecipeID
        {
            get { return mSRecipeID; }
            set { mSRecipeID = value; }
        }
        public long FKMRecipeID
        {
            get { return mFKMRecipeID; }
            set { mFKMRecipeID = value; }
        }
        public long RawGroupNo
        {
            get { return mRawGroupNo; }
            set { mRawGroupNo = value; }
        }
        public long RawProductID
        {
            get { return mRawProductID; }
            set { mRawProductID = value; }
        }
        public decimal Qty
        {
            get { return mQty; }
            set { mQty = value; }
        }
        public long UomNo
        {
            get { return mUomNo; }
            set { mUomNo = value; }
        }
        public double ProductQty
        {
            get { return mProductQty; }
            set { mProductQty = value; }
        }
        public decimal Wastageper
        {
            get { return mWastageper; }
            set { mWastageper = value; }
        }
        public decimal WastagePerQty
        {
            get { return mWastagePerQty; }
            set { mWastagePerQty = value; }
        }
        public decimal FinalQty
        {
            get { return mFinalQty; }
            set { mFinalQty = value; }
        }
        public bool IsActive
        {
            get { return mIsActive; }
            set { mIsActive = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }

    public class MRecipeMain
    {
        private long mMRecipeID;
        private long mDocNo;
        private bool mESFlag;
        private long mGroupNo;
        private long mFinishItemID;
        private decimal mPackingSize;
        private DateTime mRDate;
        private bool mIsActive;
        private long mUserID;
        private string Mmsg;
        private DateTime mUserDate;
        private double mQty;
        private long mUomNo;
        private long mRecipeType;
        private double mProdQty;
        private long mFkRecipeID;
        private bool mIsLock;

        
        public long MRecipeID
        {
            get { return mMRecipeID; }
            set { mMRecipeID = value; }
        }
        public long DocNo
        {
            get { return mDocNo; }
            set { mDocNo = value; }
        }
        public bool ESFlag
        {
            get { return mESFlag; }
            set { mESFlag = value; }
        }
        public long GroupNo
        {
            get { return mGroupNo; }
            set { mGroupNo = value; }
        }
        public long FinishItemID
        {
            get { return mFinishItemID; }
            set { mFinishItemID = value; }
        }
        public decimal PackingSize
        {
            get { return mPackingSize; }
            set { mPackingSize = value; }
        }
        public DateTime RDate
        {
            get { return mRDate; }
            set { mRDate = value; }
        }
        public long RecipeType
        {
            get { return mRecipeType; }
            set { mRecipeType = value; }
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
        public double Qty
        {
            get { return mQty; }
            set { mQty = value; }
        }
        public double ProdQty
        {
            get { return mProdQty; }
            set { mProdQty = value; }
        }
        public long UomNo
        {
            get { return mUomNo; }
            set { mUomNo = value; }
        }
        public long FkRecipeID
        {
            get { return mFkRecipeID; }
            set { mFkRecipeID = value; }
        }
        public bool IsLock
        {
            get { return mIsLock; }
            set { mIsLock = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }
}
