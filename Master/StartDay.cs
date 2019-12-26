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
    public partial class StartDay : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMSettings dbMSettings = new DBMSettings();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        DateTime StartDate, EndDate;

        public StartDay()
        {
            InitializeComponent();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                //string[] ReportSession;
                //Form NewF = null;
                //if (OMMessageBox.Show("Are you sure you want to set this Start Date?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                //{
                StartDate = dtpDate.Value;
                EndDate = dtpDate.Value.AddDays(-1);
                dbMSettings.AddAppSettings(AppSettings.O_SOD, StartDate.ToString("dd-MMM-yyyy"));
                if (ObjFunction.GetAppSettings(AppSettings.O_EOD) != EndDate.ToString("dd-MMM-yyyy"))
                {
                    dbMSettings.AddAppSettings(AppSettings.O_EOD, EndDate.ToString("dd-MMM-yyyy"));
                    TransferData();

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

                    //ReportSession = new string[3];
                    //ReportSession[0] = VchType.Sales.ToString();
                    //ReportSession[1] = DBGetVal.FirmNo.ToString();
                    //ReportSession[2] = EndDate.ToString("dd-MMM-yyyy");

                    //NewF = null;
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //    NewF = new Display.ReportViewSource(new Reports.GetOutStanding(), ReportSession);
                    //else
                    //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutStanding.rpt", CommonFunctions.ReportPath), ReportSession);
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    #region New Requirement
                    ///New Requirement
                    ////ReportSession = new string[10];
                    ////ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ////ReportSession[1] = EndDate.ToString("dd-MMM-yyyy");
                    ////ReportSession[2] = EndDate.ToString("dd-MMM-yyyy");
                    ////ReportSession[3] = "0";
                    ////ReportSession[4] = "27";
                    ////ReportSession[5] = VchType.Sales.ToString();
                    ////ReportSession[6] = DBGetVal.FirmNo.ToString();
                    ////ReportSession[7] = "";
                    ////ReportSession[8] = "1";
                    ////ReportSession[9] = "0";

                    ////NewF = null;
                    ////if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    ////    NewF = new Display.ReportViewSource(new Reports.RptEndDay(), ReportSession);
                    ////else
                    ////    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptEndDay.rpt", CommonFunctions.ReportPath), ReportSession);
                    ////ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    #endregion
                }
                if (dbMSettings.ExecuteNonQueryStatements() == true)
                {
                    //CheckMFGCompany();
                    #region New Requirement
                    ///New Requirement
                    ////ReportSession = new string[3];

                    ////ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ////ReportSession[1] = StartDate.ToString("dd-MMM-yyyy");
                    ////ReportSession[2] = StartDate.ToString("dd-MMM-yyyy");

                    ////if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    ////    NewF = new Display.ReportViewSource(new Reports.ViewDailyStockSummary(), ReportSession);
                    ////else
                    ////    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewDailyStockSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    ////ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    #endregion
                }
                ObjFunction.SetAppSettings();

                #region New Requirement
                //DataTable dt = ObjFunction.GetDataView("Exec GetInventoryCountSchedule '" + EndDate.ToString("dd-MMM-yyyy") + "'").Table;
                //if (dt.Rows.Count == 0)
                //{
                //    this.Close();
                //}
                //else
                //{
                //    Form frmChild = new Vouchers.PhysicalStockEntryAE(dt, 1);
                //    ObjFunction.OpenForm(frmChild);
                //}
                #endregion

                DataTable dtScheme = ObjFunction.GetDataView(" SELECT 0 as SrNo,MSchemeType.SchemeTypeName, MScheme.SchemeName, MScheme.SchemePeriodFrom, MScheme.SchemePeriodTo, MScheme.SchemeNo,'false' as IsActive " +
                                          " FROM  MScheme INNER JOIN MSchemeType ON MScheme.SchemeTypeNo = MSchemeType.SchemeTypeNo " +
                                          " Where MScheme.IsActive=0 and  MScheme.SchemeDate='" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + "' and " +
                                          "  MScheme.SchemePeriodFrom >='" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + "' and  MScheme.SchemePeriodTo<='" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + "'").Table;
                if (dtScheme.Rows.Count > 0)
                {
                    Form frmChild = new Utilities.SchemeActivation();
                    ObjFunction.OpenForm(frmChild);
                }
                //Zip Database Backup
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_SODUploadBackup)) == true)
                {
                    DBAutoBackup dbAuto = new DBAutoBackup();
                    dbAuto.ExportBackUp();
                    this.Close();
                }
                ObjTrans.ExecuteQuery("Exec StockUpdateAll", CommonFunctions.ConStr);
                //}
                //else
                //    this.Close();

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

        private void StartDay_Load(object sender, EventArgs e)
        {
            dtpDate.MinDate = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD).ToString()).Date;
            dtpDate.MaxDate = DBGetVal.ServerTime.Date;
            dtpTodaysDate.Value = DBGetVal.ServerTime.Date;
            dtpLastTransDate.Value = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD).ToString()).Date;
            BtnOk.Focus();
            KeyDownFormat(this.Controls);
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        //public void CheckMFGCompany()
        //{
        //    DataTable MLedger = ObjFunction.GetDataView(" SELECT DISTINCT TVoucherDetails.LedgerNo FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo Where (TVoucherEntry.VoucherTypeCode = " + VchType.Purchase + ") AND (TVoucherEntry.VoucherDate ='" + DBGetVal.ServerTime.Date.AddDays(-1) + "' ) AND (TVoucherDetails.VoucherSrNo = 1) And (TVoucherDetails.LedgerNo Not In(" + Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC)) + ")) ").Table;
        //    for (int L = 0; L < MLedger.Rows.Count; L++)
        //    {
        //        bool flag = false;
        //        DBMManufacturerCompany dbMManufacturerCompany = new DBMManufacturerCompany();
        //        MManufacturerDetails mManufactuerDetails = new MManufacturerDetails();

        //        string sql = "SELECT DISTINCT MManufacturerCompany.MfgCompNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN " +
        //                   " MStockItems ON TStock.ItemNo = mItemMaster.ItemNo INNER JOIN MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo INNER JOIN MManufacturerCompany ON mItemMaster.MfgCompNo = MManufacturerCompany.MfgCompNo " +
        //                   " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Purchase + ") AND (TVoucherEntry.VoucherDate ='" + DBGetVal.ServerTime.Date.AddDays(-1) + "' ) AND (TVoucherDetails.VoucherSrNo = 1) AND  (TVoucherDetails.LedgerNo = " + Convert.ToInt64(MLedger.Rows[L].ItemArray[0].ToString()) + ") " +
        //                   " And MManufacturerCompany.MfgCompNo Not In(Select ManufacturerNo From MManufacturerDetails  Where LedgerNo = " + Convert.ToInt64(MLedger.Rows[L].ItemArray[0].ToString()) + ") ";


        //        DataTable dt = new DataTable();
        //        dt = ObjFunction.GetDataView(sql).Table;

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            mManufactuerDetails = new MManufacturerDetails();
        //            mManufactuerDetails.LedgerNo = Convert.ToInt64(MLedger.Rows[L].ItemArray[0].ToString());
        //            mManufactuerDetails.PkSrNo = 0;
        //            mManufactuerDetails.ManufacturerNo = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
        //            mManufactuerDetails.UserID = DBGetVal.UserID;
        //            mManufactuerDetails.CompanyNo = DBGetVal.FirmNo;
        //            mManufactuerDetails.UserID = DBGetVal.UserID;
        //            mManufactuerDetails.UserDate = DBGetVal.ServerTime.Date;
        //            dbMManufacturerCompany.AddMManufacturerDetails(mManufactuerDetails);
        //            flag = true;
        //        }
        //        if (flag == true)
        //        {
        //            dbMManufacturerCompany.ExecuteNonQueryStatements();
        //        }
        //    }
        //}

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
            if (e.KeyCode == Keys.Escape)
            {
                btnNo_Click(sender, new EventArgs());
            }
            
        }
        #endregion     
    }
}
