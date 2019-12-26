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
    public partial class BillDetails : Form
    {
        
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VchCode;
        public int voucherno;
        
        public static string LedgName, RptTitle;
        public static int Type;
        public BillDetails()
        {
            InitializeComponent();
        }
        public BillDetails(long VchTypeCode)
        {
            InitializeComponent();
            if (VchTypeCode == VchType.Sales)
            {
                VchCode = VchType.Sales;
                this.Text = "Sales Bill Details";
            }
            else if (VchTypeCode == VchType.Purchase)
            {
                VchCode = VchType.Purchase;
                this.Text = "Purchase Bill Details";
            }


        }

        private void TaxDetails_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
        }

        private void DTPFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                DTToDate.Focus();
            } 
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string[] ReportSession;
            Form NewF = null;
            ReportSession = new string[3];
            ReportSession[0] = DTPFromDate.Text;
            ReportSession[1] = DTToDate.Text;
            ReportSession[2] = VchCode.ToString();
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                NewF = new Display.ReportViewSource(new Reports.RPTBillDetails(), ReportSession);
            else
                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTBillDetails.rpt", CommonFunctions.ReportPath), ReportSession);
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
    }
}
