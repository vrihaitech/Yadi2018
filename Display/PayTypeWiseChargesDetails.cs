using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;
using OMControls;

namespace Yadi.Display
{
    public partial class PayTypeWiseChargesDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public long VoucherType;

        public PayTypeWiseChargesDetails()
        {
            InitializeComponent();
        }
        public PayTypeWiseChargesDetails(long vchType)
        {
            InitializeComponent();
            if (vchType == VchType.Sales)
                VoucherType = VchType.Sales;
        }



        private void PayTypeWiseChargesDetails_Load(object sender, EventArgs e)
        {
            //CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string[] ReportSession;
            ReportSession = new string[6];
            ReportSession[0] = VoucherType.ToString();
            ReportSession[1] = DBGetVal.FirmNo.ToString();
            ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
            ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
            Form NewF = null;

            if (rdSummary.Checked == true)
            {
                ReportSession[4] = "1";
                ReportSession[5] = "0";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.RPTPayTypeChargesDetails(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTPayTypeChargesDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else
            {
                ReportSession[4] = "2";
                ReportSession[5] = "0";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.RPTPayTypeChargesSummary(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTPayTypeChargesSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
