using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Drawing.Printing;
using System.IO;

namespace Yadi.Display
{
    public partial class ReportViewSource : Form
    {
        public static string TitleName, ReportName;
        CommonFunctions ObjFunction = new CommonFunctions();
        public string[] ReportSession;
        ReportDocument report = new ReportDocument();
        ConnectionInfo LIF = new ConnectionInfo();
        TableLogOnInfo logoninfo = new TableLogOnInfo();
        public ParameterField paramField = new ParameterField();
        public ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
        public ParameterValues paramValues = new ParameterValues();
        public bool ReportType = false;
        public string newFullPath = "";
        public string rptName = "";
        public ReportViewSource(object rpt, string[] ReportSession)
        {
            InitializeComponent();
            report = (ReportDocument)rpt;
            this.ReportSession = ReportSession;
        }
        public ReportViewSource(object rpt)
        {
            InitializeComponent();
            report = (ReportDocument)rpt;
        }

        public ReportViewSource(object rpt, string[] ReportSession,bool reportType,string RptName)
        {
            InitializeComponent();
            report = (ReportDocument)rpt;
            this.ReportSession = ReportSession;
            ReportType = reportType;
            rptName = RptName;
        }

        private void ReportViewSource_Load(object sender, EventArgs e)
        {
            TitleName = "";

            int i, cnt = 0;
            if (report.FileName == "")
            {
                for (i = report.ToString().Length - 1; i >= 0; i--)
                {
                    if (Convert.ToChar(report.ToString()[i]) == 46) break;
                    cnt++;
                }
                ReportName = report.ToString().Substring(report.ToString().Length - cnt, cnt);
            }
            else
            {
                for (i = report.FileName.Length - 1; i >= 0; i--)
                {
                    if (Convert.ToChar(report.FileName[i]) == '\\') break;
                    cnt++;
                }
                ReportName = report.FileName.Substring(report.FileName.Length - cnt, cnt).Replace(".rpt", "");
            }

             
            LIF.ServerName = CommonFunctions.ServerName;

            LIF.IntegratedSecurity = true;
            LIF.DatabaseName = CommonFunctions.DatabaseName; ;
            LIF.UserID = "Logicall";
            LIF.Password = "Logicall";
            //LIF.UserID = "Trend2";
            //LIF.Password = "estofa@123";
            LIF.IntegratedSecurity = false;

            foreach (CrystalDecisions.CrystalReports.Engine.Table table in report.Database.Tables)
            {
                logoninfo = table.LogOnInfo;
                logoninfo.ConnectionInfo = LIF;
                table.ApplyLogOnInfo(logoninfo);
            }


            foreach (Section sec in report.ReportDefinition.Sections)
            {
                ReportObjects myReportObjects = sec.ReportObjects;
                foreach (ReportObject myReportObject in myReportObjects)
                {
                    if (myReportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject mySubreportObject = (SubreportObject)myReportObject;
                        ReportDocument subReportDocument = mySubreportObject.OpenSubreport(mySubreportObject.SubreportName);
                        foreach (Table tab in subReportDocument.Database.Tables)
                        {
                            // Get the TableLogOnInfo object.
                            logoninfo = tab.LogOnInfo;
                            logoninfo.ConnectionInfo = LIF;
                            tab.ApplyLogOnInfo(logoninfo);
                        }
                    }
                }
            }

            ReportObject reportObject = report.ReportDefinition.ReportObjects["txtRegName"];
            if (reportObject != null)
            {
                TextObject textObject = (TextObject)reportObject;
                textObject.Text = DBGetVal.RegCompName;
            }

            //To set parameters for report
            GetParaMeters();

            System.Drawing.Printing.PrinterSettings objPrint = new System.Drawing.Printing.PrinterSettings();

            DataTable dtPrinter = ObjFunction.GetDataView("SELECT MPrinter.PrinterName, MReportPrinterSettings.PageSizeName FROM MReportPrinterSettings INNER JOIN MPrinter ON MReportPrinterSettings.PrinterNo = MPrinter.PrinterNo " +
                   " WHERE     (MReportPrinterSettings.ReportClassName = '" + ReportName + "')").Table;
            string PrinterName = "";

            if (dtPrinter.Rows.Count > 0)
            {
                int RawKind = 0;
                PrinterName = dtPrinter.Rows[0].ItemArray[0].ToString();
                string PageSize = dtPrinter.Rows[0].ItemArray[1].ToString();
                objPrint.PrinterName = PrinterName;
                for (int a11 = 0; a11 < objPrint.PaperSizes.Count; a11++)
                {
                    if (objPrint.PaperSizes[a11].PaperName == PageSize)
                    {
                        RawKind = (int)objPrint.PaperSizes[a11].RawKind;
                        report.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)objPrint.PaperSizes[a11].RawKind;
                        break;
                    }
                    else
                        RawKind = 0;
                }
            }
            if (ReportType == false)//======for pdf export condition added 08-01-2019
            {
                if (PrinterName.Length > 0)
                    report.PrintOptions.PrinterName = PrinterName;
               // report.PrintOptions.PrinterName = "HP LaserJet 1020";


                else
                    report.PrintOptions.PrinterName = objPrint.PrinterName;
            }
            else
            {
                string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\PendingReport\\";
                if (Directory.Exists(appPath) == false)
                {
                    Directory.CreateDirectory(appPath);
                }
                string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\PendingReport\\" + rptName + ".pdf";// "\\Print\\SalesBill.pdf";
                int count = 1;
                string fileNameOnly = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                string DirectoryName = Path.GetDirectoryName(path);
                newFullPath = path;

                while (File.Exists(newFullPath))
                {
                    string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                    newFullPath = Path.Combine(DirectoryName, tempFileName + extension);
                }
                report.ExportToDisk(ExportFormatType.PortableDocFormat, newFullPath);
            }
            //if (PrinterName.Length > 0)
            //    report.PrintOptions.PrinterName = PrinterName;
            //else
            //    report.PrintOptions.PrinterName = objPrint.PrinterName;

            //To set report to ReportViewer
            crystalReportViewer1.ReportSource = report;


            string strPrinters = null; int cntRow = 0, row = 0;
            foreach (string strItem in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                row = row + 1;
                CmbPrinterName.Items.Add(strPrinters + strItem);
                if (strItem == objPrint.PrinterName)
                    cntRow = row - 1;
            }
            if (PrinterName == "")
                CmbPrinterName.SelectedText = objPrint.PrinterName;
            else CmbPrinterName.SelectedText = PrinterName;
            CmbPrinterName.SelectedIndex = cntRow;
            BtnPrint.Focus();
        }

        private void GetParaMeters()
        {
            int ParaCount = 0;
            paramField = new ParameterField();
            paramDiscreteValue = new ParameterDiscreteValue();
            paramValues = new ParameterValues();

            if (ReportName != "BarCodePrint" && ReportName != "BarCodePrintBig" && ReportName != "BarCodePrintBig50x50" && ReportName != "BarCodePrintMedium50x25" && ReportName != "BarCodePrintSmall34x22" && ReportName != "SeasonalBarCodePrint" && ReportName != "WeighingBarCodePrint" && ReportName != "BarCodePrintLarge40x35" && ReportName != "BarCodePrintExtraLarge24x35")
            {
                paramField = report.ParameterFields[ParaCount];
                paramDiscreteValue.Value = DBGetVal.FirmName;
                paramValues.Add(paramDiscreteValue);
                paramField.CurrentValues = paramValues;
                report.DataDefinition.ParameterFields[ParaCount].ApplyCurrentValues(paramValues);
                ParaCount++;
            }

            
            if (ReportName != "RptGetBill" && ReportName != "BarCodePrint" && ReportName != "BarCodePrintBig" && ReportName != "GetDCPrint"  && ReportName != "GetBillAPMCBig" && ReportName != "DCGetBill-a4" && ReportName != "GetBill-a4" && ReportName != "GetRackWiseBill-a4" && ReportName != "GetBillOrder-A5" && ReportName != "GetMultiBill" && ReportName != "GetBillMar"
                 && ReportName != "BarCodePrintBig50x50" && ReportName != "BarCodePrintMedium50x25" && ReportName != "BarCodePrintSmall34x22" && ReportName != "BarCodePrintLarge40x35" && ReportName != "BarCodePrintExtraLarge24x35" &&
                    ReportName != "SeasonalBarCodePrint" && ReportName != "WeighingBarCodePrint"
                  && !ReportName.StartsWith("GetDeliveryChallan") && !ReportName.StartsWith("GetBill") && !ReportName.StartsWith("GetDC") && !ReportName.StartsWith("GetBigBill")
               && 
               ReportName!= "StockOutward" && !ReportName.StartsWith("GetReturnBill") && ReportName != "GetQuotation" && ReportName != "GetQuotationBig")
            {
                paramField = new ParameterField();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramValues = new ParameterValues();

                paramField = report.ParameterFields[ParaCount];
                paramDiscreteValue.Value = DBGetVal.FromDate;
                paramValues.Add(paramDiscreteValue);
                paramField.CurrentValues = paramValues;
                report.DataDefinition.ParameterFields[ParaCount].ApplyCurrentValues(paramValues);
                ParaCount++;

                paramField = new ParameterField();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramValues = new ParameterValues();

                paramField = report.ParameterFields[ParaCount];
                paramDiscreteValue.Value = DBGetVal.ToDate;
                paramValues.Add(paramDiscreteValue);
                paramField.CurrentValues = paramValues;
                report.DataDefinition.ParameterFields[ParaCount].ApplyCurrentValues(paramValues);
                ParaCount++;
            }
            for (int i = 0; i < ReportSession.Length; i++)
            {
                paramField = new ParameterField();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramValues = new ParameterValues();

                paramField = report.ParameterFields[ParaCount];
                paramDiscreteValue.Value = ReportSession[i];
                paramValues.Add(paramDiscreteValue);
                paramField.CurrentValues = paramValues;
                report.DataDefinition.ParameterFields[ParaCount].ApplyCurrentValues(paramValues);

                ParaCount += 1;
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            report.PrintOptions.PrinterName = CmbPrinterName.SelectedItem.ToString();

            report.PrintToPrinter(1, true, 0, 0);
            btnExit.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkToolBar_CheckedChanged(object sender, EventArgs e)
        {
            crystalReportViewer1.DisplayToolbar = chkToolBar.Checked;
        }

        private void chkStatusBar_CheckedChanged(object sender, EventArgs e)
        {
            crystalReportViewer1.DisplayStatusBar = chkStatusBar.Checked;
        }

        private void ReportViewSource_FormClosing(object sender, FormClosingEventArgs e)
        {
            report.Close();
        }
    }
}

