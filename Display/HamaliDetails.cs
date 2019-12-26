using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;using OMControls;


namespace Yadi.Display
{
    public partial class HamaliDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
       
        public HamaliDetails()
        {
            InitializeComponent();
        }
       
        private void HamaliDetails_Load(object sender, EventArgs e)
        {
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

        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                btnShow.Focus();
            } 
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string[] ReportSession;
            Form NewF = null;
            ReportSession = new string[4];

            ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");//DBGetVal.FirmNo.ToString();
            ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
            ReportSession[2] = VchType.Sales.ToString();
            ReportSession[3] = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1)).ToString();// +',' + Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges2)).ToString();
            
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                NewF = new Display.ReportViewSource(new Reports.RptGetHamaliDetails(), ReportSession);
            else
                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGetHamaliDetails.rpt", CommonFunctions.ReportPath), ReportSession);
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
    }
}
