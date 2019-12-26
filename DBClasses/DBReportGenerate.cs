using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OM;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using Yadi.Vouchers;

namespace OM
{
    class DBReportGenerate
    {
        public static string TitleName, ReportName;
        CommonFunctions ObjFunction = new CommonFunctions();
        public string[] ReportSession;
        public ReportDocument report = new ReportDocument();
        ConnectionInfo LIF = new ConnectionInfo();
        TableLogOnInfo logoninfo = new TableLogOnInfo();
        public static ParameterField paramField = new ParameterField();
        public static ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
        public static ParameterValues paramValues = new ParameterValues();
        public string OwnPrinterName = "";
        public int PrintCount = 1;
        public bool ReportType = false;
        public bool isAutoMail = false;
        public string newFullPath = "";
        public string rptName = "";
        public DBReportGenerate(object rpt, string[] ReportSession)
        {
            report = (ReportDocument)rpt;
            this.ReportSession = ReportSession;
            PrintCount = 1;
        }
        public DBReportGenerate(object rpt, string[] ReportSession, bool reportType)
        {
            report = (ReportDocument)rpt;
            this.ReportSession = ReportSession;
            PrintCount = 1;
            ReportType = reportType;

        }
        public DBReportGenerate(object rpt, string[] ReportSession, bool reportType, string RptName, bool IsAutoMail)
        {
            report = (ReportDocument)rpt;
            this.ReportSession = ReportSession;
            PrintCount = 1;
            ReportType = reportType;
            rptName = RptName;
            isAutoMail = IsAutoMail;
        }

        public bool PrintReport()
        {
            try
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
                LIF.DatabaseName = CommonFunctions.DatabaseName;
                LIF.UserID = "Logicall";
                LIF.Password = "Logicall";
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
                if (OwnPrinterName == "")
                {
                    System.Drawing.Printing.PrinterSettings objPrint = new System.Drawing.Printing.PrinterSettings();
                    SetPrinterSize();
                    //report.PrintOptions.PrinterName = objPrint.PrinterName;
                }
                else
                {
                    report.PrintOptions.PrinterName = OwnPrinterName;
                }

                if (PrintCount <= 0) PrintCount = 1;
                if (ReportType == false)//======for pdf export condition added 08-01-2019
                {
                    report.PrintToPrinter(PrintCount, true, 1, 1);
                    report.PrintToPrinter(PrintCount, true, 2, 2);
                    report.PrintToPrinter(PrintCount, true, 3, 3);
                    report.PrintToPrinter(PrintCount, true, 4, 0);
                    report.Close();
                }
                return true;
            }
            catch (Exception e)
            {
                report.Close();
                CommonFunctions.ErrorMessge = e.Message;
                return false;
            }
        }

        private void GetParaMeters()
        {
            int ParaCount = 0;
            paramField = new ParameterField();
            paramDiscreteValue = new ParameterDiscreteValue();
            paramValues = new ParameterValues();

            if (ReportName != "RPTVoucherPrint" && ReportName != "RPTVoucherDetailsPrint" && ReportName != "BarCodePrint" && ReportName != "BarCodePrintBig" && ReportName != "BarCodePrintBig50x50" && ReportName != "BarCodePrintMedium50x25" && ReportName != "BarCodePrintSmall34x22" && ReportName != "SeasonalBarCodePrint" && ReportName != "WeighingBarCodePrint")
            {
                paramField = report.ParameterFields[ParaCount];
                paramDiscreteValue.Value = DBGetVal.FirmName;
                paramValues.Add(paramDiscreteValue);
                paramField.CurrentValues = paramValues;
                report.DataDefinition.ParameterFields[ParaCount].ApplyCurrentValues(paramValues);
                ParaCount++;
            }

            if (ReportName != "GetBill" && ReportName != "GetBillAPMCMar" && ReportName != "GetBillAPMC" && ReportName != "DCGetBill-A4" && ReportName != "GetDCPrint" && ReportName != "GetDCAPMC" && ReportName != "GetBillFirmPackList"
                && ReportName != "GetBillMar" && ReportName != "RPTVoucherPrint" && ReportName != "RPTVoucherDetailsPrint" && ReportName != "BarCodePrint" && ReportName != "GetDCBoxPrintMar" && ReportName != "GetDCKgPrintMar"
                && ReportName != "BarCodePrintBig" && ReportName != "BarCodePrintBig50x50" && ReportName != "BarCodePrintMedium50x25"
                && ReportName != "BarCodePrintSmall34x22" && ReportName != "GetBigBill" && ReportName != "GetBillMRP" && ReportName != "GetBillMarMRP" && ReportName != "GetBill-a4" && ReportName != "GetBillMarPakka-A5"
                && ReportName.IndexOf("GetDeliveryChallanFirm") == -1 && ReportName.IndexOf("GetBillFirm") == -1 && ReportName.IndexOf("GetBigBillFirm") == -1
                   && ReportName.IndexOf("GetBillFirmTrans") == -1 && ReportName.IndexOf("GetBigBillFirmTrans") == -1
                    && ReportName.IndexOf("GetBillFirmMar") == -1 && ReportName.IndexOf("GetBigBillFirmMar") == -1
                   && ReportName.IndexOf("GetBillFirmTransMar") == -1 && ReportName.IndexOf("GetBigBillFirmTransMar") == -1
                && ReportName != "RptLocationWisePrint"  
                && ReportName.IndexOf("GetReturnBill") == -1 && ReportName != "GetQuotation" && ReportName != "GetQuotationBig")
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
            try
            {
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
            catch (Exception e)
            {
                report.Close();
                CommonFunctions.ErrorMessge = e.Message;

            }
            }

        private void SetPrinterSize()
        {
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
                else
                    report.PrintOptions.PrinterName = objPrint.PrinterName;
            }
            else
            {
                if (isAutoMail == false)
                {
                    string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Print\\";
                    if (Directory.Exists(appPath) == false)
                    {
                        Directory.CreateDirectory(appPath);
                    }
                    string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Print\\" + "BillNo_" + SalesBarcodeAE.EmailRptName + ".pdf";// "\\Print\\SalesBill.pdf";
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
                else
                {
                    string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\PendingReport\\";
                    if (Directory.Exists(appPath) == false)
                    {
                        Directory.CreateDirectory(appPath);
                    }
                    string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\PendingReport\\" + rptName + ".pdf";// "\\Print\\SalesBill.pdf";
                    //int count = 1;
                    string fileNameOnly = Path.GetFileNameWithoutExtension(path);
                    string extension = Path.GetExtension(path);
                    string DirectoryName = Path.GetDirectoryName(path);
                    newFullPath = path;

                    while (File.Exists(newFullPath))
                    {
                        FileStream fileStream = new FileStream(newFullPath, FileMode.Open, FileAccess.Read);
                       
                        fileStream.Close();
                        File.Delete(newFullPath);
                        //string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
                        //newFullPath = Path.Combine(DirectoryName, tempFileName + extension);
                    }
                    report.ExportToDisk(ExportFormatType.PortableDocFormat, newFullPath);
                }
            }
        }
    }
}
