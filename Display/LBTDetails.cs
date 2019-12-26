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
    public partial class LBTDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();       
        long VchCode;

        public LBTDetails()
        {
            InitializeComponent();
        }

        public LBTDetails(long VchCode)
        {
            InitializeComponent();
            this.VchCode = VchCode;
            if (VchCode == VchType.Sales)
            {
                this.Text = "LBT Details(Sales)";
            }
            else if (VchCode == VchType.Purchase)
            {
                this.Text = "LBT Details(Purchase)";
            }
        }

        private void GRNDetails_Load(object sender, EventArgs e)
        {
            txtFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            txtToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            txtToDate.MinDate = txtFromDate.Value;
        }

        #region KeyDown Events
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
        #endregion

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                
                if (rbSummary.Checked == true)
                {

                    ReportSession = new string[4];
                    ReportSession[0] = txtFromDate.Text;
                    ReportSession[1] = txtToDate.Text;
                    ReportSession[2] = VchCode.ToString();
                    if (VchCode == VchType.Sales)
                        ReportSession[3] = "LBT Summary(Sales)";
                    else
                        ReportSession[3] = "LBT Summary(Purchase)";

                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //    NewF = new Display.ReportViewSource(new Reports.GetLBTSummary(), ReportSession);
                    //else
                    //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLBTSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rbDetails.Checked == true)
                {
                    ReportSession = new string[4];
                    ReportSession[0] = txtFromDate.Text;
                    ReportSession[1] = txtToDate.Text;
                    ReportSession[2] = VchCode.ToString();
                    if (VchCode == VchType.Sales)
                        ReportSession[3] = "LBT Details(Sales)";
                    else
                        ReportSession[3] = "LBT Details(Purchase)";

                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //    NewF = new Display.ReportViewSource(new Reports.GetLBTDetails(), ReportSession);
                    //else
                    //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLBTDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            txtToDate.MinDate = txtFromDate.Value;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
