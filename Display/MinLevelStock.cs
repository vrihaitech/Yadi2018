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
    public partial class MinLevelStock : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        
        public long CompNo;
        public long MfgCompNo = 0;
        public MinLevelStock()
        {
            InitializeComponent();
        }

        public MinLevelStock(long MfgCompNo)
        {
            InitializeComponent();
            this.MfgCompNo = MfgCompNo;
        }


        private void MinLevelStock_Load(object sender, EventArgs e)
        {
            lblFirmName.Text = "";
            if (MfgCompNo != 0)
            {
                Form NewFrm = new Vouchers.FirmSelection();
                ObjFunction.OpenForm(NewFrm);
                MfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
            }

            CompNo = DBGetVal.FirmNo;
            //DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTPToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            //DTPToDate.MinDate = DTPFromDate.Value;
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                int valActDeAct = 0;
                if (rdActive.Checked == true) valActDeAct = 0;
                else if (rdDeActive.Checked == true) valActDeAct = 1;
                else if (rdActiveDeActive.Checked == true) valActDeAct = -1;

                string[] ReportSession = new string[6];
                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");// Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = valActDeAct.ToString();
                ReportSession[4] = MfgCompNo.ToString();
                ReportSession[5] = lblFirmName.Text.Replace("Firm Name :", "");
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.MinLevelStock(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("MinLevelStock.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTPToDate.MinDate = DTPFromDate.Value;
        }

       
        }
    }

