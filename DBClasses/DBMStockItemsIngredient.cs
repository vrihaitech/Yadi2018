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
    class DBMStockItemsIngredient
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMStockItemsIngredient(MStockItemsIngredient mstockitemsingredient)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockItemsIngredient";

            cmd.Parameters.AddWithValue("@PkSrNo", mstockitemsingredient.PkSrNo);

            cmd.Parameters.AddWithValue("@IngredientValue", mstockitemsingredient.IngredientValue);

            cmd.Parameters.AddWithValue("@NutritionHeadValue", mstockitemsingredient.NutritionHeadValue);

            cmd.Parameters.AddWithValue("@ReceipeHead1", mstockitemsingredient.ReceipeHead1);

            cmd.Parameters.AddWithValue("@ReceipeHead2", mstockitemsingredient.ReceipeHead2);

            cmd.Parameters.AddWithValue("@ReceipeHead3", mstockitemsingredient.ReceipeHead3);

            cmd.Parameters.AddWithValue("@LangReceipeHead1", mstockitemsingredient.LangReceipeHead1);

            cmd.Parameters.AddWithValue("@LangReceipeHead2", mstockitemsingredient.LangReceipeHead2);

            cmd.Parameters.AddWithValue("@LangReceipeHead3", mstockitemsingredient.LangReceipeHead3);

            cmd.Parameters.AddWithValue("@ItemNo", mstockitemsingredient.ItemNo);

            cmd.Parameters.AddWithValue("@UserID", mstockitemsingredient.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mstockitemsingredient.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockitemsingredient.CompanyNo);


            SqlParameter outParameter = new SqlParameter();
            outParameter.ParameterName = "@ReturnID";
            outParameter.Direction = ParameterDirection.Output;
            outParameter.DbType = DbType.Int32;
            cmd.Parameters.Add(outParameter);

            commandcollection.Add(cmd);
            return true;
        }

        public bool AddMStockItemsNutrition(MStockItemsNutrition mstockitemsnutrition)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockItemsNutrition";

            cmd.Parameters.AddWithValue("@PkSrNo", mstockitemsnutrition.PkSrNo);

          //  cmd.Parameters.AddWithValue("@IngredientNo", mstockitemsnutrition.IngredientNo);

            cmd.Parameters.AddWithValue("@NutritionNo", mstockitemsnutrition.NutritionNo);

            cmd.Parameters.AddWithValue("@NutritionValue", mstockitemsnutrition.NutritionValue);

            cmd.Parameters.AddWithValue("@NutritionUOM", mstockitemsnutrition.NutritionUOM);

            cmd.Parameters.AddWithValue("@SequenceNo", mstockitemsnutrition.SequenceNo);

            cmd.Parameters.AddWithValue("@UserID", mstockitemsnutrition.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mstockitemsnutrition.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockitemsnutrition.CompanyNo);
                        
            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMStockItemsNutrition(MStockItemsNutrition mstockitemsnutrition)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMStockItemsNutrition";

            cmd.Parameters.AddWithValue("@PkSrNo", mstockitemsnutrition.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool DeleteMStockItemsReceipe(MStockItemsReceipe mstockitemsreceipe)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMStockItemsReceipe";

            cmd.Parameters.AddWithValue("@PkSrNo", mstockitemsreceipe.PkSrNo);

            commandcollection.Add(cmd);
            return true;
        }



        public bool AddMStockItemsReceipe(MStockItemsReceipe mstockitemsreceipe)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMStockItemsReceipe";

            cmd.Parameters.AddWithValue("@PkSrNo", mstockitemsreceipe.PkSrNo);

         //   cmd.Parameters.AddWithValue("@IngredientNo", mstockitemsreceipe.IngredientNo);

            cmd.Parameters.AddWithValue("@ReceipeDesc", mstockitemsreceipe.ReceipeDesc);

            cmd.Parameters.AddWithValue("@LangReceipeDesc", mstockitemsreceipe.LangReceipeDesc);

            cmd.Parameters.AddWithValue("@SequenceNo", mstockitemsreceipe.SequenceNo);

            cmd.Parameters.AddWithValue("@UserID", mstockitemsreceipe.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mstockitemsreceipe.UserDate);

            cmd.Parameters.AddWithValue("@CompanyNo", mstockitemsreceipe.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }
        
        public MStockItemsIngredient ModifyMStockItemsIngredientByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MStockItemsIngredient where PkSrNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MStockItemsIngredient MM = new MStockItemsIngredient();
                while (dr.Read())
                {
                    MM.PkSrNo = Convert.ToInt32(dr["PkSrNo"]);
                    if (!Convert.IsDBNull(dr["IngredientValue"])) MM.IngredientValue = Convert.ToString(dr["IngredientValue"]);
                    if (!Convert.IsDBNull(dr["NutritionHeadValue"])) MM.NutritionHeadValue = Convert.ToString(dr["NutritionHeadValue"]);
                    if (!Convert.IsDBNull(dr["ReceipeHead1"])) MM.ReceipeHead1 = Convert.ToString(dr["ReceipeHead1"]);
                    if (!Convert.IsDBNull(dr["ReceipeHead2"])) MM.ReceipeHead2 = Convert.ToString(dr["ReceipeHead2"]);
                    if (!Convert.IsDBNull(dr["ReceipeHead3"])) MM.ReceipeHead3 = Convert.ToString(dr["ReceipeHead3"]);
                    if (!Convert.IsDBNull(dr["LangReceipeHead1"])) MM.LangReceipeHead1 = Convert.ToString(dr["LangReceipeHead1"]);
                    if (!Convert.IsDBNull(dr["LangReceipeHead2"])) MM.LangReceipeHead2 = Convert.ToString(dr["LangReceipeHead2"]);
                    if (!Convert.IsDBNull(dr["LangReceipeHead3"])) MM.LangReceipeHead3 = Convert.ToString(dr["LangReceipeHead3"]);
                    if (!Convert.IsDBNull(dr["ItemNo"])) MM.ItemNo = Convert.ToInt64(dr["ItemNo"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["CompanyNo"])) MM.CompanyNo = Convert.ToInt64(dr["CompanyNo"]);
                    if (!Convert.IsDBNull(dr["StatusNo"])) MM.StatusNo = Convert.ToInt32(dr["StatusNo"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MStockItemsIngredient();
        }

        public DataView GetAllMStockItemsIngredient()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockItemsIngredient order by PkSrNo";
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

        public DataView GetMStockItemsIngredientByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MStockItemsIngredient where PkSrNo =" + ID;
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

        public long ExecuteNonQueryStatements()
        {

            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStr);
            cn.Open();

            SqlTransaction myTrans;
            myTrans = cn.BeginTransaction();
            int cntVchNo = -1;
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddMStockItemsIngredient")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddMStockItemsNutrition")
                        {
                            commandcollection[i].Parameters.AddWithValue("@IngredientNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddMStockItemsReceipe")
                        {
                            commandcollection[i].Parameters.AddWithValue("@IngredientNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
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
    }

    public class MStockItemsIngredient
    {
        private long mPkSrNo;
        private string mIngredientValue;
        private string mNutritionHeadValue;
        private string mReceipeHead1;
        private string mReceipeHead2;
        private string mReceipeHead3;
        private string mLangReceipeHead1;
        private string mLangReceipeHead2;
        private string mLangReceipeHead3;
        private long mItemNo;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public string IngredientValue
        {
            get { return mIngredientValue; }
            set { mIngredientValue = value; }
        }
        public string NutritionHeadValue
        {
            get { return mNutritionHeadValue; }
            set { mNutritionHeadValue = value; }
        }
        public string ReceipeHead1
        {
            get { return mReceipeHead1; }
            set { mReceipeHead1 = value; }
        }
        public string ReceipeHead2
        {
            get { return mReceipeHead2; }
            set { mReceipeHead2 = value; }
        }
        public string ReceipeHead3
        {
            get { return mReceipeHead3; }
            set { mReceipeHead3 = value; }
        }
        public string LangReceipeHead1
        {
            get { return mLangReceipeHead1; }
            set { mLangReceipeHead1 = value; }
        }
        public string LangReceipeHead2
        {
            get { return mLangReceipeHead2; }
            set { mLangReceipeHead2 = value; }
        }
        public string LangReceipeHead3
        {
            get { return mLangReceipeHead3; }
            set { mLangReceipeHead3 = value; }
        }
        public long ItemNo
        {
            get { return mItemNo; }
            set { mItemNo = value; }
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

    public class MStockItemsNutrition
    {
        private long mPkSrNo;
        private long mIngredientNo;
        private long mNutritionNo;
        private string mNutritionValue;
        private string mNutritionUOM;
        private long mSequenceNo;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long IngredientNo
        {
            get { return mIngredientNo; }
            set { mIngredientNo = value; }
        }
        public long NutritionNo
        {
            get { return mNutritionNo; }
            set { mNutritionNo = value; }
        }
        public string NutritionValue
        {
            get { return mNutritionValue; }
            set { mNutritionValue = value; }
        }
        public string NutritionUOM
        {
            get { return mNutritionUOM; }
            set { mNutritionUOM = value; }
        }
        public long SequenceNo
        {
            get { return mSequenceNo; }
            set { mSequenceNo = value; }
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

    public class MStockItemsReceipe
    {
        private long mPkSrNo;
        private long mIngredientNo;
        private string mReceipeDesc;
        private string mLangReceipeDesc;
        private long mSequenceNo;
        private long mUserID;
        private DateTime mUserDate;
        private long mCompanyNo;
        private int mStatusNo;
        private string Mmsg;

        public long PkSrNo
        {
            get { return mPkSrNo; }
            set { mPkSrNo = value; }
        }
        public long IngredientNo
        {
            get { return mIngredientNo; }
            set { mIngredientNo = value; }
        }
        public string ReceipeDesc
        {
            get { return mReceipeDesc; }
            set { mReceipeDesc = value; }
        }
        public string LangReceipeDesc
        {
            get { return mLangReceipeDesc; }
            set { mLangReceipeDesc = value; }
        }
        public long SequenceNo
        {
            get { return mSequenceNo; }
            set { mSequenceNo = value; }
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

}
