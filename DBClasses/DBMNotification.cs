using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using OMControls;

namespace OM
{
    class DBMNotification
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public CommandCollection commandcollection = new CommandCollection();
        public static string strerrormsg;

        public bool AddMNotification(MNotification mnotification)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddMNotification";

            cmd.Parameters.AddWithValue("@NotifyNo", mnotification.NotifyNo);

            cmd.Parameters.AddWithValue("@NotifyType", mnotification.NotifyType);

            cmd.Parameters.AddWithValue("@NotifyMessage", mnotification.NotifyMessage);

            cmd.Parameters.AddWithValue("@NotifyDate", mnotification.NotifyDate);

            cmd.Parameters.AddWithValue("@NotifyStatus", mnotification.NotifyStatus);

            cmd.Parameters.AddWithValue("@NotifyFileName", mnotification.NotifyFileName);

            cmd.Parameters.AddWithValue("@CompanyNo", mnotification.CompanyNo);

            commandcollection.Add(cmd);
            return true;
        }

        public bool UpdateNotification(MNotification mnotification)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Update MNotification set NotifyStatus=@NotifyStatus ,StatusNo=2 where NotifyNo=@NotifyNo";

            cmd.Parameters.AddWithValue("@NotifyNo", mnotification.NotifyNo);

            cmd.Parameters.AddWithValue("@NotifyStatus", mnotification.NotifyStatus);

            commandcollection.Add(cmd);
            return true;
        }

        public bool ExecuteNonQueryStatements()
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
    }

    /// <summary>
    /// This Class use for MNotification
    /// </summary>
    public class MNotification
    {
        private long mNotifyNo;
        private long mNotifyType;
        private string mNotifyMessage;
        private DateTime mNotifyDate;
        private int mNotifyStatus;
        private string mNotifyFileName;
        private int mStatusNo;
        private long mCompanyNo;
        private string Mmsg;

        /// <summary>
        /// This Properties use for NotifyNo
        /// </summary>
        public long NotifyNo
        {
            get { return mNotifyNo; }
            set { mNotifyNo = value; }
        }
        /// <summary>
        /// This Properties use for NotifyType
        /// </summary>
        public long NotifyType
        {
            get { return mNotifyType; }
            set { mNotifyType = value; }
        }
        /// <summary>
        /// This Properties use for NotifyMessage
        /// </summary>
        public string NotifyMessage
        {
            get { return mNotifyMessage; }
            set { mNotifyMessage = value; }
        }
        /// <summary>
        /// This Properties use for NotifyDate
        /// </summary>
        public DateTime NotifyDate
        {
            get { return mNotifyDate; }
            set { mNotifyDate = value; }
        }
        /// <summary>
        /// This Properties use for NotifyStatus
        /// </summary>
        public int NotifyStatus
        {
            get { return mNotifyStatus; }
            set { mNotifyStatus = value; }
        }
        /// <summary>
        /// This Properties use for NotifyFileName
        /// </summary>
        public string NotifyFileName
        {
            get { return mNotifyFileName; }
            set { mNotifyFileName = value; }
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

}
