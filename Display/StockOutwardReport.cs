
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
    public partial class StockOutwardReport : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
       
        public long CompNo, LedgNo, MNo, Type1, voucherno,ID;
        public string LedgName, RptTitle;

        public StockOutwardReport()
        {
            InitializeComponent();
        }

        private void LedgerBook_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            KeyDownFormat(this.Controls);
            rbSummary.Checked = true;
        }
             
        private void btnPrint_Click(object sender, EventArgs e)
        {
           PrintBill(0);
        }

        public void PrintBill(int PrintType)
        {
            try
            {
                if (rbSummary.Checked)
                {
                    string[] ReportSession;
                    Form NewF = null;

                    ReportSession = new string[2];
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
              
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.StockOutwardSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("StockOutwardSummary.rpt", CommonFunctions.ReportPath), ReportSession);

                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else if (rbDetailed.Checked)
                {
                    string[] ReportSession;
                    Form NewF = null;

                    ReportSession = new string[2];
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                  
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.StockOutwardSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("StockOutwardDetails.rpt", CommonFunctions.ReportPath), ReportSession);

                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
     
        private void DTPFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                DTToDate.Focus();
            }
        }
                  
        private void btnBack_Click(object sender, EventArgs e)
        {
          
        }
      
        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyDownFormat(ctrl.Controls);

            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
               
            }
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Form NewF = new Vouchers.StockOutwardAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
      
        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

    }
}

