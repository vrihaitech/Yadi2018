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
    class DBMGroup
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddMGroup(MGroup mgroup)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMGroup";

            cmd.Parameters.AddWithValue("@GroupNo", mgroup.GroupNo);

            cmd.Parameters.AddWithValue("@GroupName", mgroup.GroupName);

            cmd.Parameters.AddWithValue("@ControlGroup", mgroup.ControlGroup);

            cmd.Parameters.AddWithValue("@SignCode", mgroup.SignCode);

            cmd.Parameters.AddWithValue("@IsActive", mgroup.IsActive);

            cmd.Parameters.AddWithValue("@UserID", mgroup.UserID);

            cmd.Parameters.AddWithValue("@UserDate", mgroup.UserDate);

            //cmd.Parameters.AddWithValue("@ModifiedBy", mgroup.ModifiedBy);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mgroup.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public bool DeleteMGroup(MGroup mgroup)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeleteMGroup";

            cmd.Parameters.AddWithValue("@GroupNo", mgroup.GroupNo);
            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mgroup.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

        public DataView GetAllMGroup()
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MGroup order by GroupNo";
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

        public DataView GetMGroupByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql = "Select * from MGroup where GroupNo =" + ID;
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

        public MGroup ModifyMGroupByID(long ID)
        {
            SqlConnection Con = new SqlConnection(CommonFunctions.ConStr);
            string sql;
            SqlCommand cmd;
            sql = "Select * from MGroup where GroupNo =" + ID;
            cmd = new SqlCommand(sql, Con);
            cmd.Connection = Con;
            Con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr;
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.HasRows)
            {
                MGroup MM = new MGroup();
                while (dr.Read())
                {
                    MM.GroupNo = Convert.ToInt32(dr["GroupNo"]);
                    if (!Convert.IsDBNull(dr["GroupName"])) MM.GroupName = Convert.ToString(dr["GroupName"]);
                    if (!Convert.IsDBNull(dr["ControlGroup"])) MM.ControlGroup = Convert.ToInt64(dr["ControlGroup"]);
                    if (!Convert.IsDBNull(dr["SignCode"])) MM.SignCode = Convert.ToInt64(dr["SignCode"]);
                    if (!Convert.IsDBNull(dr["IsActive"])) MM.IsActive = Convert.ToBoolean(dr["IsActive"]);
                    if (!Convert.IsDBNull(dr["UserID"])) MM.UserID = Convert.ToInt64(dr["UserID"]);
                    if (!Convert.IsDBNull(dr["UserDate"])) MM.UserDate = Convert.ToDateTime(dr["UserDate"]);
                    if (!Convert.IsDBNull(dr["ModifiedBy"])) MM.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);
                }
                dr.Close();
                return MM;
            }
            else
                dr.Close();
            return new MGroup();
        }

        public DataView GetBySearch(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select MGroup.GroupNo,MGroup.GroupName AS 'Group Name',MGroup_1.GroupName AS 'Under Group', Case when (MGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MGroup "+
                          "INNER JOIN   MGroup AS MGroup_1 ON MGroup.ControlGroup = MGroup_1.GroupNo " +
                          "Where (MGroup.GroupNo >29) order by MGroup.GroupName";
                    break;
                case "GroupName":
                    sql = "Select MGroup.GroupNo,MGroup.GroupName AS 'Group Name',MGroup_1.GroupName AS 'Under Group', Case when (MGroup.IsActive = 'True') Then 'True' Else 'False' End As 'Status' from MGroup "+
                          "INNER JOIN   MGroup AS MGroup_1 ON MGroup.ControlGroup = MGroup_1.GroupNo "+
                          "where (MGroup.GroupNo >29) AND (MGroup." + Column + ") like '" + Value.Trim().Replace("'","''") + "' + '%' order by MGroup.GroupName";
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

        public DataView GetBySearchTax(string Column, string Value)
        {

            string sql = null;
            switch (Column)
            {
                case "0":
                    sql = "Select GroupNo,GroupName AS 'Group Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup",""+GroupType.DutiesAndTaxes+"","G.GroupNo",3) + " And GroupNo<>" + GroupType.DutiesAndTaxes + " order by GroupName";
                    break;
                case "GroupName":
                    sql = "Select GroupNo,GroupName AS 'Group Name',Case when (IsActive = 'True') Then 'True' Else 'False' End As 'Status' From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "  AND " + Column + " like '" + Value.Trim().Replace("'","''") + "' + '%' And GroupNo<>" + GroupType.DutiesAndTaxes + " order by GroupName";
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
    /// This Class use for MGroup
    /// </summary>
    public class MGroup
    {
        private long mGroupNo;
        private string mGroupName;
        private long mControlGroup;
        private long mSignCode;
        private bool mIsActive;
        private int mStatusNo;
        private long mUserID;
        private DateTime mUserDate;
        private string mModifiedBy;
        private string Mmsg;

        /// <summary>
        /// This Properties use for GroupNo
        /// </summary>
        public long GroupNo
        {
            get { return mGroupNo; }
            set { mGroupNo = value; }
        }
        /// <summary>
        /// This Properties use for GroupName
        /// </summary>
        public string GroupName
        {
            get { return mGroupName; }
            set { mGroupName = value; }
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
        /// This Properties use for SignCode
        /// </summary>
        public long SignCode
        {
            get { return mSignCode; }
            set { mSignCode = value; }
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
        /// This Properties use for ModifiedBy
        /// </summary>
        public string ModifiedBy
        {
            get { return mModifiedBy; }
            set { mModifiedBy = value; }
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
