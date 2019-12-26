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


namespace Yadi.Master
{
    public partial class EndDay : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMSettings dbMSettings = new DBMSettings();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        DateTime EndDate;//StartDate

        public EndDay()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpDate.Value.Date <= Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD)))
                {
                    if (OMMessageBox.Show("Are you sure you want to set this End Date?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OKCancel, OMMessageBoxIcon.Question) == DialogResult.OK)
                    {
                        EndDate = dtpDate.Value;
                        TransferData();
                        if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "").ToUpper() == System.Net.Dns.GetHostName().ToUpper())
                        {
                            lblStatus.Visible = true;
                            lblStatus.Text = "Please wait...Backup in process.";
                            Application.DoEvents();
                            DBAutoBackup dbAuto = new DBAutoBackup();
                            dbAuto.backup();
                            System.Threading.Thread.Sleep(800);
                            lblStatus.Visible = false;
                            Application.DoEvents();

                            dbAuto.ExportBackUp();
                        }

                        dbMSettings.AddAppSettings(AppSettings.O_EOD, EndDate.ToString("dd-MMM-yyyy"));
                        if (dbMSettings.ExecuteNonQueryStatements() == true)
                        {
                            string[] ReportSession;
                            Form NewF = null;

                            //ReportSession = new string[6];
                            //ReportSession[0] = VchType.Sales.ToString();
                            //ReportSession[1] = DBGetVal.FirmNo.ToString();
                            //ReportSession[2] = EndDate.ToString("dd-MMM-yyyy");
                            //ReportSession[3] = EndDate.ToString("dd-MMM-yyyy");
                            //ReportSession[4] = "1";
                            //ReportSession[5] = "0";

                            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            //    NewF = new Display.ReportViewSource(new Reports.RPTSalesSummary(), ReportSession);
                            //else
                            //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                            //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);


                            //NewF = null;

                            //ReportSession = new string[5];
                            //ReportSession[0] = DBGetVal.FirmNo.ToString();
                            //ReportSession[1] = EndDate.ToString("dd-MMM-yyyy");
                            //ReportSession[2] = EndDate.ToString("dd-MMM-yyyy");
                            //ReportSession[3] = "0";
                            //ReportSession[4] = "27";


                            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            //    NewF = new Display.ReportViewSource(new Reports.DayBook(), ReportSession);
                            //else
                            //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DayBook.rpt", CommonFunctions.ReportPath), ReportSession);
                            //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                            //ReportSession = new string[4];
                            //ReportSession[0] = VchType.Sales.ToString();
                            //ReportSession[1] = DBGetVal.FirmNo.ToString();
                            //ReportSession[2] = EndDate.ToString("dd-MMM-yyyy");
                            //ReportSession[3] = "";

                            // NewF = null;
                            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            //    NewF = new Display.ReportViewSource(new Reports.GetOutStanding(), ReportSession);
                            //else
                            //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutStanding.rpt", CommonFunctions.ReportPath), ReportSession);
                            //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                            ReportSession = new string[10];
                            ReportSession[0] = DBGetVal.FirmNo.ToString();
                            ReportSession[1] = EndDate.ToString("dd-MMM-yyyy");
                            ReportSession[2] = EndDate.ToString("dd-MMM-yyyy");
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


                            //Outstanding Report
                            ReportSession = new string[4];
                            ReportSession[0] = VchType.Sales.ToString();
                            ReportSession[1] = DBGetVal.FirmNo.ToString();
                            ReportSession[2] = EndDate.ToString("dd-MMM-yyyy");
                            ReportSession[3] = "";

                            NewF = null;
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.GetOutStanding(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutStanding.rpt", CommonFunctions.ReportPath), ReportSession);
                            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                            ObjFunction.SetAppSettings();

                            ObjTrans.ExecuteQuery("Exec StockUpdateAll", CommonFunctions.ConStr);
                            
                            
                        }
                    }
                }
                else
                    OMMessageBox.Show("End Date Must be Less then StartDate", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TransferData()
        {
            bool flag = true;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.AutoUploadData)) == true)
            {
                if (System.IO.File.Exists(Application.StartupPath + "\\RetailerAuto.exe") == true)
                {
                    foreach (System.Diagnostics.Process clsProcess in System.Diagnostics.Process.GetProcesses())
                    {
                        if (clsProcess.ProcessName.Contains("RetailerAuto.exe"))
                        {
                            flag = false;
                        }
                    }
                    if (flag == true)
                    {
                        try
                        {
                            System.Diagnostics.ProcessStartInfo pr = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + "\\RetailerAuto.exe", CommonFunctions.ConStr.Replace(" ", "") + " " + CommonFunctions.ConStrServer.Replace(" ", ""));
                            pr.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                            System.Diagnostics.Process newProc1 = null;
                            newProc1 = System.Diagnostics.Process.Start(pr);
                        }
                        catch (Exception e)
                        {
                            System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.StartupPath + "\\Log.txt", true);
                            sw.WriteLine(e.Message);
                            sw.Close();
                        }
                    }
                }
            }
           
        }

        private void EndDay_Load(object sender, EventArgs e)
        {
            try
            {
                dtpDate.MinDate = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD));
                lblStatus.Font = ObjFunction.GetFont(FontStyle.Bold, 9);
                lblStatus.ForeColor = Color.Blue;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
