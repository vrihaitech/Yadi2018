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
    class DBReportStatus
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();

        public bool AddReportStatus(ReportStatus mReportStatus)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AddReportStatus";

            cmd.Parameters.AddWithValue("@FrequencyNo", mReportStatus.FrequencyNo);

            cmd.Parameters.AddWithValue("@ReportType", mReportStatus.ReportType);

            cmd.Parameters.AddWithValue("@LastSentDateTime", mReportStatus.LastSentDateTime);

            if (ObjTrans.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == true)
            {
                return true;
            }
            else
            {
                mReportStatus.msg = ObjTrans.ErrorMessage;
                return false;
            }
        }

    }

 
    public class ReportStatus
    {
        private long mFrequencyNo;
        private long mReportType;
        private DateTime mLastSentDateTime;
        private string Mmsg;
        public long FrequencyNo
        {
            get { return mFrequencyNo; }
            set { mFrequencyNo = value; }
        }
     
        public long ReportType
        {
            get { return mReportType; }
            set { mReportType = value; }
        }
       
        public DateTime LastSentDateTime
        {
            get { return mLastSentDateTime; }
            set { mLastSentDateTime = value; }
        }
        public string msg
        {
            get { return Mmsg; }
            set { Mmsg = value; }
        }
    }


}
