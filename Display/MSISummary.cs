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
    public partial class MSISummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VoucherType;
        public int voucherno;
        
        public static string LedgName, RptTitle;
        public static int Type;

        public MSISummary()
        {
            InitializeComponent();
        }
        public MSISummary(long vchType)
        {
            InitializeComponent();
            
        }
        private void MSISummary_Load(object sender, EventArgs e)
        {
            DTPFromDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Form NewF = null;
                string[] ReportSession;
                ReportSession = new string[6];
                ReportSession[0] = DTPFromDate.Text;
                ReportSession[1] = VchType.Sales.ToString();
                ReportSession[2] = DBGetVal.FirmNo.ToString();
                ReportSession[3] = VchType.Purchase.ToString();
                ReportSession[4] = VchType.SalesReceipt.ToString();
                ReportSession[5] = "5";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.MSISummaryDetail(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("MSISummaryDetail.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
      
    }
}
