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
    public partial class StartDayReports : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        
        public StartDayReports()
        {
            InitializeComponent();
        }

        private void MSIDailyBusiness_Load(object sender, EventArgs e)
        {
            DTStartDate.Text = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD)).ToString(Format.DDMMMYYYY);
            DTEndDate.Text = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_EOD)).ToString(Format.DDMMMYYYY);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDayReports_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                ReportSession = new string[10];
                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = DTEndDate.Value.ToString("dd-MMM-yyyy");
                ReportSession[2] = DTEndDate.Value.ToString("dd-MMM-yyyy");
                ReportSession[3] = "0";
                ReportSession[4] = "27";
                ReportSession[5] = VchType.Sales.ToString();
                ReportSession[6] = DBGetVal.FirmNo.ToString();
                ReportSession[7] = "";
                ReportSession[8] = "1";
                ReportSession[9] = "0";

                NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.RptEndDay(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptEndDay.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnStocksummary_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                ReportSession = new string[3];
                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = DTStartDate.Value.ToString("dd-MMM-yyyy");
                ReportSession[2] = DTStartDate.Value.ToString("dd-MMM-yyyy");

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewDailyStockSummary(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewDailyStockSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnOutstanding_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession = new string[4];
                ReportSession[0] = VchType.Sales.ToString();
                ReportSession[1] = DBGetVal.FirmNo.ToString();
                ReportSession[2] = DTEndDate.Value.ToString("dd-MMM-yyyy");
                ReportSession[3] = "";

                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.GetOutStanding(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutStanding.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


    }
}
