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
    public partial class ParkingWiseSalesRegisterSummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1;
        public int voucherno;
        
        public static string LedgName, RptTitle;
        public static int Type;
        public ParkingWiseSalesRegisterSummary()
        {
            InitializeComponent();
        }

        private void ParkingWiseSalesRegisterSummary_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                BtnExport.Visible = true;
            else
                BtnExport.Visible = false;
        }

        

        

       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdSummary.Checked == true || rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                {
                    string[] ReportSession;
                    ReportSession = new string[6];
                    ReportSession[0] = VchType.Sales.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    Form NewF = null;
                    if (rdSummary.Checked == true)
                    {
                        ReportSession[4] = "1";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTParkingSalesRegister(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTParkingSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDaySummary.Checked == true)
                    {
                        ReportSession[4] = "2";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTParkingSalesRegisterDateWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTParkingSalesRegisterDateWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdMonthSummary.Checked == true)
                    {
                        ReportSession[4] = "3";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTParkingSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTParkingSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdQuarterSummary.Checked == true)
                    {
                        ReportSession[4] = "4";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTParkingSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTParkingSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                {
                    OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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

        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                rdSummary.Focus();
            } 
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string str = "",ReportName="",PartyName="";
            double BillCount = 0, TotAmt = 0;
            int dtCount = 0;
            if (rdSummary.Checked == true)
            {
                ReportName = "Parkingwise Sale Register Summary";
                str = "Exec GetParkingWiseSaleVouchEntryDtlsByDate " + VchType.Sales.ToString() + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
                dt = ObjFunction.GetDataView(str).Table;
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(1);
                dtCount = dt.Columns.Count; PartyName = "Party";
                TotAmt = Convert.ToDouble(dt.Compute("Sum(Total)", ""));

                BillCount = Convert.ToDouble(dt.Compute("Count(BillNo)", ""));
            }
            else if (rdDaySummary.Checked == true)
            {
                ReportName = "Parkingwise Sale Register Daywise Summary";
                str = "Exec GetParkingWiseSaleVouchEntryDtlsByDate " + VchType.Sales.ToString() + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2,0";
                dt = ObjFunction.GetDataView(str).Table;
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(1);
               
                TotAmt = Convert.ToDouble(dt.Compute("Sum(Total)", ""));
                dtCount = dt.Columns.Count; PartyName = "Party";
            }
            else if (rdMonthSummary.Checked == true)
            {
                ReportName = "Parkingwise Monthwise Sale Register Summary";
                str = "Exec GetParkingWiseSaleVouchEntryDtlsByDate " + VchType.Sales.ToString() + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,0";
                dt = ObjFunction.GetDataView(str).Table;
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(1);
               
                TotAmt = Convert.ToDouble(dt.Compute("Sum(Total)", ""));
                dtCount = dt.Columns.Count ; PartyName = "Month";
            }
            else if (rdQuarterSummary.Checked == true)
            {
                ReportName = "Parkingwise Quarterwise Sale Register Summary";
                str = "Exec GetParkingWiseSaleVouchEntryDtlsByDate   " + VchType.Sales.ToString() + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,0";
                dt = ObjFunction.GetDataView(str).Table;
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(1);
                dt.Columns.RemoveAt(1);
               
                TotAmt = Convert.ToDouble(dt.Compute("Sum(Total)", ""));
                dtCount = dt.Columns.Count; PartyName = "Quarter";
            }
            else
            {
                OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            if (str != "")
            {
                //dt = ObjFunction.GetDataView(str).Table;
                try
                {

                    int col = 1;
                    CreateExcel excel = new CreateExcel();
                    //Company Name Header
                    excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                    col++;
                    //Company Address Header
                    excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                    col++;
                    //Report Name And Dates
                    excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;
                    excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        switch (i)
                        {
                            case 0: if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                if (rdQuarterSummary.Checked == true || rdMonthSummary.Checked == true)
                                    excel.createHeaders(col, i + 1, PartyName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                break;
                            case 1://if(rdSummary.Checked==true || rdDaySummary.Checked==true)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                                break;
                            case 2: //if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                                excel.createHeaders(col, i + 1, "Amount", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                            case 3: if (rdSummary.Checked == true)
                                    excel.createHeaders(col, i + 1, "", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;

                            default: if (rdSummary.Checked == true)
                                    excel.createHeaders(col, i + 1 - 2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                    excel.createHeaders(col, i + 1 - 3, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 3), excel.ColName(col, i + 1 - 3), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                break;
                        }

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            switch (i)
                            {
                                case 0: if (rdDaySummary.Checked == true)
                                    {
                                        if (Convert.ToDateTime(dt.Rows[j].ItemArray[i].ToString()).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                        else
                                            excel.addData(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                    }
                                    else //if(rdSummary.Checked==true )
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                    break;
                                case 1: if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    //else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                    //    excel.createHeaders(j + col + 1, i + 1 - 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 1), excel.ColName(j + col + 1, i + 1 - 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    else
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    //if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                        //BillCount = BillCount + Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString());
                                    break;
                                case 2: //if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                                    excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                                case 3: if (rdSummary.Checked == true)
                                    {
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                                    } break;
                                case 4: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                    else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    break;
                                default: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                    else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.addData(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                    break;

                            }

                        }
                    }
                    //for (int i = 0; i < dt.Columns.Count; i++)
                    //{
                    //    switch (i)
                    //    {
                    //        case 0: if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                    //                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);                                    
                    //                break;
                    //        case 1:if(rdSummary.Checked==true)
                    //                excel.createHeaders(col, i + 1, PartyName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //                else if(rdMonthSummary.Checked==true || rdQuarterSummary.Checked==true)
                    //                    excel.createHeaders(col, i + 1 - 1, PartyName, excel.ColName(col, i + 1 - 1), excel.ColName(col, i + 1 - 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //            break;
                    //        case 2: break;
                    //        case 3: break;                           
                    //        default: if(rdSummary.Checked==true)
                    //                    excel.createHeaders(col, i + 1-2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1-2), excel.ColName(col, i + 1-2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //                 else if(rdDaySummary.Checked==true || rdMonthSummary.Checked==true || rdQuarterSummary.Checked==true)
                    //                    excel.createHeaders(col, i + 1 - 3, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 3), excel.ColName(col, i + 1 - 3), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);                            
                    //                 break;
                    //    }
                        
                    //    for (int j = 0; j < dt.Rows.Count; j++)
                    //    {
                    //        switch (i)
                    //        {
                    //            case 0: if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                    //                       excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);                                    
                    //                    break;
                    //            case 1: if (rdSummary.Checked == true)
                    //                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //                    else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                    //                        excel.createHeaders(j + col + 1, i + 1 - 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 1), excel.ColName(j + col + 1, i + 1 - 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //                break;
                    //            case 2: break;                                    
                    //            case 3: break;
                                
                    //            case 4:
                    //            case 5:
                    //                if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //                    excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                    //                else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //                break;
                               
                    //            default: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //                                                    excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false); 
                    //                else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //                excel.addData(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false); 
                    //                     break;

                    //        }

                    //    }
                    //}
                    col = col + dt.Rows.Count;
                    col++;

                    if (rdSummary.Checked == true)
                    {
                        excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(col, 3, TotAmt.ToString("0.00"), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        col++;
                       

                       
                    }
                    else// (rdDaySummary.Checked == true)
                    {
                        excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(col, 2, TotAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        col++;
                        
                    }
                    excel.CompleteDoc("");
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
