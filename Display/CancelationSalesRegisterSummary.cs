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
    public partial class CancelationSalesRegisterSummary : Form
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
        public CancelationSalesRegisterSummary()
        {
            InitializeComponent();
        }

        private void SalesRegisterSummary_Load(object sender, EventArgs e)
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
                        ReportSession[5] = "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    //else if (rdDaySummary.Checked == true)
                    //{
                    //    ReportSession[4] = "2";
                    //    ReportSession[5] = "1";
                    //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //        NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDateWiseSummary(), ReportSession);
                    //    else
                    //        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDateWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    //    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    //}
                    else if (rdMonthSummary.Checked == true)
                    {
                        ReportSession[4] = "3";
                        ReportSession[5] = "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdQuarterSummary.Checked == true)
                    {
                        ReportSession[4] = "4";
                        ReportSession[5] = "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
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
            try
            {
                DataTable dt = new DataTable();
                string str = "", ReportName = "", PartyName = "",VchTypeNo = "", VchTypeName="";
                double BillCount = 0, TotAmt = 0;//, TotFVAmt = 0,TotCashAmt = 0, TotCrAmt = 0, TotCCAmt = 0, TotChqAmt = 0
                int dtCount = 0, Cnt=0;
                long VoucherType=VchType.Sales;
                DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VchType.Sales + ",1," + DBGetVal.FirmNo + "").Table;
                if (rdSummary.Checked == true)
                {
                    DetailsExcel();
                    //ReportName = "Cancellation Sale Register Summary";
                    //str = "Exec GetSaleVouchEntryDayDtls " + VchType.Sales + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,1";
                    //dt = ObjFunction.GetDataView(str).Table;
                    //if (rdSummary.Checked == true)
                    //{
                    //    //TotCashAmt = Convert.ToDouble(dt.Compute("Sum(cash)", ""));
                    //    //TotCCAmt = Convert.ToDouble(dt.Compute("Sum(CC)", ""));
                    //    //TotCrAmt = Convert.ToDouble(dt.Compute("Sum(Credit)", ""));
                    //    //TotChqAmt = Convert.ToDouble(dt.Compute("Sum(Cheque)", ""));
                    //    //TotFVAmt = Convert.ToDouble(dt.Compute("Sum(FV)", ""));
                    //    TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));
                    //    BillCount = Convert.ToDouble(dt.Compute("Count(BillNo)", ""));
                    //}
                    //dt.Columns.RemoveAt(0);
                    //dt.Columns.RemoveAt(1);
                    //dt.Columns.RemoveAt(1);
                    ////dt.Columns.RemoveAt(0);
                    ////dt.Columns.RemoveAt(1);
                    ////dt.Columns.RemoveAt(1);
                    ////dt.Columns.RemoveAt(2);
                    ////dt.Columns.RemoveAt(2);
                    ////dt.Columns.RemoveAt(2);
                    ////dt.Columns.RemoveAt(2);
                    ////dt.Columns.RemoveAt(2);
                    //dtCount = dt.Columns.Count; PartyName = "Party";
                }
                else if (rdDaySummary.Checked == true)
                {
                    ReportName = "Cancellation Sale Register Daywise Summary";
                    str = "Exec GetSaleVouchEntryDayDtls " + VchType.Sales + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2,1";
                    dt = ObjFunction.GetDataView(str).Table;
                    //TotCashAmt = Convert.ToDouble(dt.Compute("Sum(cash)", ""));
                    //TotCCAmt = Convert.ToDouble(dt.Compute("Sum(CC)", ""));
                    //TotCrAmt = Convert.ToDouble(dt.Compute("Sum(Credit)", ""));
                    //TotChqAmt = Convert.ToDouble(dt.Compute("Sum(Cheque)", ""));
                    //TotFVAmt = Convert.ToDouble(dt.Compute("Sum(FV)", ""));
                    TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));
                    //BillCount = Convert.ToDouble(dt.Compute("Sum(TotalBills)", ""));
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(3);
                    //dt.Columns.RemoveAt(1);
                    //dt.Columns.RemoveAt(1);
                    //dt.Columns.RemoveAt(1);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    dtCount = dt.Columns.Count-1; PartyName = "Party";
                }
                else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                {
                    PartyName = "Particulars";
                    if (rdMonthSummary.Checked == true)
                    {
                        str = "Exec GetSaleVouchEntryDayDtls " + VchType.Sales + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,1";

                        ReportName = "Cancellation Monthwise Sales Summary";
                    }
                    if (rdQuarterSummary.Checked == true)
                    {
                        str = "Exec GetSaleVouchEntryDayDtls " + VchType.Sales + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,1";

                        ReportName = "Cancellation Quarterwise Sales Summary";
                    }
                    dt = ObjFunction.GetDataView(str).Table;
                    //TotCashAmt = Convert.ToDouble(dt.Compute("Sum(cash)", ""));
                    //TotCCAmt = Convert.ToDouble(dt.Compute("Sum(CC)", ""));
                    //TotCrAmt = Convert.ToDouble(dt.Compute("Sum(Credit)", ""));
                    //TotChqAmt = Convert.ToDouble(dt.Compute("Sum(Cheque)", ""));
                    //TotFVAmt = Convert.ToDouble(dt.Compute("Sum(FV)", ""));
                    TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));
                    dt.Columns.RemoveAt(0);
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(3);
                    //dt.Columns.RemoveAt(0);
                    //dt.Columns.RemoveAt(1);
                    //dt.Columns.RemoveAt(1);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    dtCount = dt.Columns.Count-1;
                }

                else
                {
                    OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                if (str != "")
                    if (str != "")
                    {
                        //dt = ObjFunction.GetDataView(str).Table;
                        try
                        {

                            int col = 1;
                            CreateExcel excel = new CreateExcel();
                            //Company Name Header
                            excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
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


                            for (int i = 0; i < dt.Columns.Count - 1; i++)
                            {
                                switch (i)
                                {
                                    case 0: if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                                        {
                                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);

                                        }
                                        if (rdQuarterSummary.Checked == true || rdMonthSummary.Checked == true)
                                            excel.createHeaders(col, i + 1, PartyName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                        break;
                                    case 1: Cnt = 0; VchTypeName = "";VchTypeNo = "";//if(rdSummary.Checked==true || rdDaySummary.Checked==true)
                                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                                        break;
                                    case 2: Cnt = 0; VchTypeName = "";VchTypeNo = "";//if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                                        excel.createHeaders(col, i + 1, "Amount", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                                    case 3: if (rdSummary.Checked == true)
                                            excel.createHeaders(col, i + 1, "", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;

                                    default: if (rdSummary.Checked == true)
                                            excel.createHeaders(col, i + 1 - 2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                        {
                                            excel.createHeaders(col, i + 1 - 3, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 3), excel.ColName(col, i + 1 - 3), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                            Cnt = 0; VchTypeNo = "0";
                                        }
                                        break;
                                }

                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    switch (i)
                                    {
                                        case 0: if (rdDaySummary.Checked == true)
                                            {
                                                if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                                {
                                                    if (VchTypeName != "")
                                                    {
                                                        double amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
                                                        excel.createHeaders(j + col + 1 + Cnt, 1, "Total", excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                        excel.createHeaders(j + col + 1 + Cnt, 3, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + Cnt, 3), excel.ColName(j + col + 1 + Cnt, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                        Cnt++;
                                                    }
                                                    VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                    if (VoucherType == VchType.Purchase)
                                                    {
                                                        if (VchTypeNo == "1") VchTypeName = "Purchase";
                                                        else if (VchTypeNo == "2") VchTypeName = "Purchase Return";

                                                    }
                                                    else if (VoucherType == VchType.Sales)
                                                    {
                                                        if (VchTypeNo == "1") VchTypeName = "Sales";
                                                        else if (VchTypeNo == "2") VchTypeName = "Sales Return";

                                                    }
                                                    else if (VoucherType == VchType.RejectionOut)
                                                    {
                                                        VchTypeName = "Purchase Return";
                                                    }
                                                    else if (VoucherType == VchType.RejectionIn)
                                                    {
                                                        VchTypeName = "Sales Return";
                                                    }


                                                    excel.createHeaders(j + col + 1 + Cnt, 1, VchTypeName, excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                                    Cnt++;

                                                }
                                                if (Convert.ToDateTime(dt.Rows[j].ItemArray[i].ToString()).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                                                    excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                                else
                                                    excel.addData(j + col + 1 + Cnt, i + 1, "", excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                            }
                                            else
                                            {//if(rdSummary.Checked==true )
                                                if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                                {
                                                    if (VchTypeName != "")
                                                    {
                                                        double amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
                                                        excel.createHeaders(j + col + 1 + Cnt, 1, "Total", excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                        excel.createHeaders(j + col + 1 + Cnt, 3, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + Cnt, 3), excel.ColName(j + col + 1 + Cnt, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                        Cnt++;
                                                    }

                                                    VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                    if (VoucherType == VchType.Purchase)
                                                    {
                                                        if (VchTypeNo == "1") VchTypeName = "Purchase";
                                                        else if (VchTypeNo == "2") VchTypeName = "Purchase Return";

                                                    }
                                                    else if (VoucherType == VchType.Sales)
                                                    {
                                                        if (VchTypeNo == "1") VchTypeName = "Sales";
                                                        else if (VchTypeNo == "2") VchTypeName = "Sales Return";

                                                    }
                                                    else if (VoucherType == VchType.RejectionOut)
                                                    {
                                                        VchTypeName = "Purchase Return";
                                                    }
                                                    else if (VoucherType == VchType.RejectionIn)
                                                    {
                                                        VchTypeName = "Sales Return";
                                                    }


                                                    excel.createHeaders(j + col + 1 + Cnt, 1, VchTypeName, excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                                    Cnt++;

                                                }

                                                excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                            }
                                            break;
                                        case 1: if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                            {

                                                VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                Cnt++;
                                                if (rdDaySummary.Checked == true)
                                                {
                                                    if (VchTypeNo != "1")
                                                        Cnt++;
                                                }
                                            }
                                            if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                            {
                                                if (VchTypeNo != "1")
                                                    Cnt++;
                                                excel.createHeaders(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                            }
                                            //else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                            //    excel.createHeaders(j + col + 1, i + 1 - 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 1), excel.ColName(j + col + 1, i + 1 - 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                            else
                                            {

                                                if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                                {

                                                    VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                    Cnt++;

                                                }
                                                excel.createHeaders(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                            }
                                            if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                                BillCount = BillCount + Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString());
                                            break;
                                        case 2: if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                            {

                                                VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                Cnt++;
                                                if (VchTypeNo != "1")
                                                    Cnt++;
                                            }//if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                                            excel.createHeaders(j + col + 1 + Cnt, i + 1, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString())).ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                                        case 3: if (rdSummary.Checked == true)
                                            {
                                                excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                                            } break;
                                        case 4: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                            else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                            {
                                                if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                                {
                                                    if (VchTypeNo != "1")
                                                        Cnt++;
                                                    VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                    Cnt++;
                                                }
                                                excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                            }
                                            break;
                                        default: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                            else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                            {
                                                if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                                {
                                                    if (VchTypeNo != "1")
                                                        Cnt++;
                                                    VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                    Cnt++;
                                                }
                                                excel.addData(j + col + 1 + Cnt, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1 - 3), excel.ColName(j + col + 1 + Cnt, i + 1 - 3), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                                            }
                                            break;

                                    }

                                }
                            }
                            col = col + dt.Rows.Count + Cnt;
                            col++;

                            if(rdSummary.Checked == false)
                            {
                                double amt = 0;
                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2")) == true)
                                    amt = 0;
                                else
                                amt=Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2"));
                                if (amt != 0)
                                {
                                    excel.createHeaders(col, 1, "Total", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    excel.createHeaders(col, 3, Math.Abs(amt).ToString(), excel.ColName(col, 3), excel.ColName(col, 3), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    col++;
                                }
                                else
                                {
                                    amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
                                    excel.createHeaders(col, 1, "Total", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    excel.createHeaders(col, 3, Math.Abs(amt).ToString(), excel.ColName(col, 3), excel.ColName(col, 3), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    col++;
                                }

                                excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                excel.createHeaders(col, 2, Math.Abs(TotAmt).ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                col++;
                                excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                col++;
                                excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                excel.createHeaders(col, 2, "Amount", excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                col++;
                                for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
                                {
                                    excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(col, 2, Math.Abs(Convert.ToDouble(dtPayTypeDtls.Rows[i].ItemArray[4].ToString())).ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    col++;
                                }
                                //excel.createHeaders(col, 1, "Cash", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                //excel.createHeaders(col, 2, TotCashAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                //col++;
                                //excel.createHeaders(col, 1, "CC", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                //excel.createHeaders(col, 2, TotCCAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                //col++;
                                //excel.createHeaders(col, 1, "Cheque", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                //excel.createHeaders(col, 2, TotChqAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                //col++;
                                //excel.createHeaders(col, 1, "Credit", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                //excel.createHeaders(col, 2, TotCrAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                //col++;
                                //if (VchType.Sales == VoucherType)
                                //{
                                //    excel.createHeaders(col, 1, "FV", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                //    excel.createHeaders(col, 2, TotFVAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                //    col++;
                                //}
                                excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                col++;
                                //excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                //excel.createHeaders(col, 2, BillCount.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                //col++;
                            }
                            //for (int i = 0; i < dt.Columns.Count-1; i++)
                            //{
                            //    switch (i)
                            //    {
                            //        case 0: if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                            //            {
                            //                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);

                            //            }
                            //            if (rdQuarterSummary.Checked == true || rdMonthSummary.Checked == true)
                            //                excel.createHeaders(col, i + 1, PartyName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //            break;
                            //        case 1: Cnt = 0; VchTypeNo = "0";//if(rdSummary.Checked==true || rdDaySummary.Checked==true)
                            //            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                            //            break;
                            //        case 2: Cnt = 0; VchTypeNo = "0";//if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                            //            excel.createHeaders(col, i + 1, "Amount", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                            //        case 3: if (rdSummary.Checked == true)
                            //                excel.createHeaders(col, i + 1, "", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;

                            //        default: if (rdSummary.Checked == true)
                            //                excel.createHeaders(col, i + 1 - 2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //            else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            //            {
                            //                excel.createHeaders(col, i + 1 - 3, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 3), excel.ColName(col, i + 1 - 3), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //                Cnt = 0; VchTypeNo = "0";
                            //            }
                            //            break;
                            //    }

                            //    for (int j = 0; j < dt.Rows.Count; j++)
                            //    {
                            //        switch (i)
                            //        {
                            //            case 0: if (rdDaySummary.Checked == true)
                            //                {
                            //                    if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                            //                    {
                            //                        VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                    
                            //                            if (VchTypeNo == "1") VchTypeName = "Sales";
                            //                            else if (VchTypeNo == "2") VchTypeName = "Sales Return";

                                                    

                            //                        excel.createHeaders(j + col + 1 + Cnt, 1, VchTypeName, excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //                        Cnt++;

                            //                    }
                            //                    if (Convert.ToDateTime(dt.Rows[j].ItemArray[i].ToString()).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            //                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                            //                    else
                            //                        excel.addData(j + col + 1 + Cnt, i + 1, "", excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            //                }
                            //                else
                            //                {//if(rdSummary.Checked==true )
                            //                    if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                            //                    {
                            //                        VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                                   
                            //                            if (VchTypeNo == "1") VchTypeName = "Sales";
                            //                            else if (VchTypeNo == "2") VchTypeName = "Sales Return";

                                                    

                            //                        excel.createHeaders(j + col + 1 + Cnt, 1, VchTypeName, excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //                        Cnt++;

                            //                    }
                            //                    excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            //                }
                            //                break;
                            //            case 1: if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                            //                {
                            //                    VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                            //                    Cnt++;
                            //                }
                            //                if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            //                    excel.createHeaders(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //                //else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            //                //    excel.createHeaders(j + col + 1, i + 1 - 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 1), excel.ColName(j + col + 1, i + 1 - 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //                else
                            //                {
                            //                    if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                            //                    {
                            //                        VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                            //                        Cnt++;
                            //                    }
                            //                    excel.createHeaders(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //                }
                            //                if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            //                    BillCount = BillCount + Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString());
                            //                break;
                            //            case 2: if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                            //                {
                            //                    VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                            //                    Cnt++;
                            //                }//if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                            //                excel.createHeaders(j + col + 1 + Cnt, i + 1, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString())).ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                            //            case 3: if (rdSummary.Checked == true)
                            //                {
                            //                    excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                            //                } break;
                            //            case 4: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //                    excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            //                else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            //                {
                            //                    if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                            //                    {
                            //                        VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                            //                        Cnt++;
                            //                    }
                            //                    excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //                }
                            //                break;
                            //            default: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //                    excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            //                else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //                {
                            //                    if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                            //                    {
                            //                        VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                            //                        Cnt++;
                            //                    }
                            //                    excel.addData(j + col + 1 + Cnt, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1 - 3), excel.ColName(j + col + 1 + Cnt, i + 1 - 3), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                            //                }
                            //                break;

                            //        }

                            //    }
                            //}
                            ////{
                            ////    switch (i)
                            ////    {
                            ////        case 0: if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                            ////                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            ////            if (rdQuarterSummary.Checked == true || rdMonthSummary.Checked == true)
                            ////                excel.createHeaders(col, i + 1, PartyName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            ////            break;
                            ////        case 1://if(rdSummary.Checked==true || rdDaySummary.Checked==true)
                            ////            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                            ////            break;
                            ////        case 2: //if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                            ////            excel.createHeaders(col, i + 1, "Amount", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                            ////        case 3: if (rdSummary.Checked == true)
                            ////                excel.createHeaders(col, i + 1, "", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;

                            ////        default: if (rdSummary.Checked == true)
                            ////                excel.createHeaders(col, i + 1 - 2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ////            else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            ////                excel.createHeaders(col, i + 1 - 3, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 3), excel.ColName(col, i + 1 - 3), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ////            break;
                            ////    }

                            ////    for (int j = 0; j < dt.Rows.Count; j++)
                            ////    {
                            ////        switch (i)
                            ////        {
                            ////            case 0: if (rdDaySummary.Checked == true)
                            ////                {
                            ////                    if (Convert.ToDateTime(dt.Rows[j].ItemArray[i].ToString()).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                            ////                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                            ////                    else
                            ////                        excel.addData(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            ////                }
                            ////                else //if(rdSummary.Checked==true )
                            ////                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            ////                break;
                            ////            case 1: if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            ////                    excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            ////                //else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            ////                //    excel.createHeaders(j + col + 1, i + 1 - 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 1), excel.ColName(j + col + 1, i + 1 - 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            ////                else
                            ////                    excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ////                if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                            ////                    BillCount = BillCount + Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString());
                            ////                break;
                            ////            case 2: //if (rdSummary.Checked == true || rdDaySummary.Checked==true)
                            ////                excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                            ////            case 3: if (rdSummary.Checked == true)
                            ////                {
                            ////                    excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                            ////                } break;
                            ////            case 4: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ////                    excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            ////                else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ////                break;
                            ////            default: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ////                    excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            ////                else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ////                    excel.addData(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            ////                break;

                            ////        }

                            ////    }
                            ////}
                            //col = col + dt.Rows.Count+Cnt;
                            //col++;

                            //if (rdSummary.Checked == true)
                            //{
                            //    excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    excel.createHeaders(col, 3, TotAmt.ToString("0.00"), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    col++;
                            //    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    col++;
                            //    excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    excel.createHeaders(col, 3, "Amount", excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    col++;
                            //    for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
                            //    {
                            //        excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //        excel.createHeaders(col, 3, dtPayTypeDtls.Rows[i].ItemArray[4].ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //        col++;
                            //    }
                            //    //excel.createHeaders(col, 1, "Cash", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 3, TotCashAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "CC", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 3, TotCCAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "Cheque", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 3, TotChqAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "Credit", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 3, TotCrAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "FV", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 3, TotFVAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    col++;
                            //    excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    excel.createHeaders(col, 3, BillCount.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    col++;
                            //}
                            //else// (rdDaySummary.Checked == true)
                            //{
                            //    excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    excel.createHeaders(col, 2, TotAmt.ToString("0.00"), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    col++;
                            //    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    col++;
                            //    excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    excel.createHeaders(col, 2, "Amount", excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    col++;
                            //    for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
                            //    {
                            //        excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //        excel.createHeaders(col, 2, dtPayTypeDtls.Rows[i].ItemArray[4].ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //        col++;
                            //    }
                            //    //excel.createHeaders(col, 1, "Cash", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 2, TotCashAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "CC", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 2, TotCCAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "Cheque", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 2, TotChqAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "Credit", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 2, TotCrAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    //excel.createHeaders(col, 1, "FV", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    //excel.createHeaders(col, 2, TotFVAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    //col++;
                            //    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    col++;
                            //    excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //    excel.createHeaders(col, 2, BillCount.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            //    col++;
                            //}
                            excel.CompleteDoc("");
                        }
                        catch (Exception ex)
                        {
                            CommonFunctions.ErrorMessge = ex.Message;
                        }
                    }
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

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        public void DetailsExcel()
        {
            DataTable dt = new DataTable();
            string str = "", ReportName = "", TempDate = "", VchTypeNo = "", VchTypeName = "";
            long VoucherType = VchType.Sales;
            double BillCount = 0, TotAmt = 0;//, TotFVAmt = 0,TotCashAmt = 0, TotCrAmt = 0, TotCCAmt = 0, TotChqAmt = 0,
            int dtCount = 0, ExtraRows = 0;
            //DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VoucherType + ",0," + DBGetVal.FirmNo + "").Table;
            DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VchType.Sales + ",1," + DBGetVal.FirmNo + "").Table;
            ReportName = "Cancellation Sale Register Summary";
            str = "Exec GetSaleVouchEntryDayDtls " + VchType.Sales + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,1";
            //dt = ObjFunction.GetDataView(str).Table;
            dt = ObjFunction.GetDataView(str).Table;
            //dt.Columns.RemoveAt(7);
            long SCount = Convert.ToInt64(dt.Compute("Count(VchTypeNo)", "VchTypeNo=1"));
            long RCount = Convert.ToInt64(dt.Compute("Count(VchTypeNo)", "VchTypeNo=2"));
            TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));

            BillCount = Convert.ToDouble(dt.Compute("Max(VoucherNo)", ""));

            //dt.Columns.RemoveAt(0);
            dt.Columns.RemoveAt(2);
            dt.Columns.RemoveAt(2);

            dtCount = dt.Columns.Count - 2; //PartyName = "Party";

            if (str != "")
            {



                int col = 1;
                CreateExcel excel = new CreateExcel();
                //Company Name Header
                excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
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

                for (int i = 0; i < dt.Columns.Count - 1; i++)
                {

                    switch (i)
                    {
                        case 1: if (rdSummary.Checked == true)
                                excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);

                            break;
                        case 2:
                            excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                            break;
                        case 3: excel.createHeaders(col, i, "Amount", excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                        case 4: if (rdSummary.Checked == true)
                                excel.createHeaders(col, i, "", excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;


                    }


                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    //if (i > 0)
                    //{
                    if (VchTypeNo != dt.Rows[j].ItemArray[5].ToString())
                    {
                        if (VchTypeNo == "1")
                        {
                            double amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + ""));
                            excel.createHeaders(j + col + 1 + ExtraRows, 1, "Day Total", excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                            excel.createHeaders(j + col + 1 + ExtraRows, 4, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                            ExtraRows++;
                        }
                        VchTypeNo = dt.Rows[j].ItemArray[5].ToString();
                        if (VoucherType == VchType.Purchase)
                        {
                            if (VchTypeNo == "1") VchTypeName = "Purchase";
                            else if (VchTypeNo == "2") VchTypeName = "Purchase Return";

                        }
                        else if (VoucherType == VchType.Sales)
                        {
                            if (VchTypeNo == "1") VchTypeName = "Sales";
                            else if (VchTypeNo == "2") VchTypeName = "Sales Return";

                        }
                        else if (VoucherType == VchType.RejectionOut)
                        {
                            VchTypeName = "Purchase Return";
                        }
                        else if (VoucherType == VchType.RejectionIn)
                        {
                            VchTypeName = "Sales Return";
                        }
                        if (VchTypeNo == "2")
                        {
                            if (SCount != 0)
                            {
                                excel.createHeaders(j + col + 1 + ExtraRows, 1, "No.Of Bills : " + SCount.ToString(), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                double amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
                                excel.createHeaders(j + col + 1 + ExtraRows, 3, "Total", excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                excel.createHeaders(j + col + 1 + ExtraRows, 4, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                ExtraRows++;
                            }
                            excel.createHeaders(j + col + 1 + ExtraRows, 1, VchTypeName, excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        }
                        else
                            excel.createHeaders(j + col + 1 + ExtraRows, 1, VchTypeName, excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                        ExtraRows++;
                    }
                    if (TempDate != Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"))
                    {
                        if (rdSummary.Checked == true)
                        {
                            if (TempDate != "" && dt.Rows[j - 1].ItemArray[5].ToString() == VchTypeNo)
                            {
                                double amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + ""));
                                excel.createHeaders(j + col + 1 + ExtraRows, 1, "Day Total", excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                excel.createHeaders(j + col + 1 + ExtraRows, 4, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                ExtraRows++;
                            }
                        }
                        excel.createHeaders(j + col + 1 + ExtraRows, 1, Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
                        TempDate = Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy");
                        ExtraRows++;
                    }
                    excel.addData(j + col + 1 + ExtraRows, 1, dt.Rows[j].ItemArray[1].ToString(), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), "", 0, CreateExcel.ExAlign.Left, false);
                    excel.createHeaders(j + col + 1 + ExtraRows, 2, dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    excel.createHeaders(j + col + 1 + ExtraRows, 3, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[3].ToString())).ToString(), excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    excel.createHeaders(j + col + 1 + ExtraRows, 4, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                    //}

                    //switch (i)
                    //{
                    //    case 1:
                    //        if (TempDate != Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"))
                    //        {
                    //            excel.createHeaders(j + col + 1, i, Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //            TempDate = Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy");
                    //            ExtraRows++;
                    //        }
                    //        excel.addData(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), "", 0, CreateExcel.ExAlign.Left, false);
                    //        break;
                    //    case 2: if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                    //            excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //        break;                               
                    //    case 3:
                    //        excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1, i) + ExtraRows, 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                    //    case 4: if (rdSummary.Checked == true)
                    //        {
                    //            excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                    //        } break;
                    //    case 5: if (rdSummary.Checked == true)
                    //            excel.addData(j + col + 1 + ExtraRows, i - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i + 1 - 2), excel.ColName(j + col + 1 + ExtraRows, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);

                    //        break;


                    //}

                }
                //}
                col = col + dt.Rows.Count + ExtraRows;
                col++;

                if (rdSummary.Checked == true)
                {
                    double amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[dt.Rows.Count - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[dt.Rows.Count - 1].ItemArray[5].ToString() + ""));
                    excel.createHeaders(col, 1, "Day Total", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    excel.createHeaders(col, 4, Math.Abs(amt).ToString(), excel.ColName(col, 4), excel.ColName(col, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    col++;
                    if (RCount == 0)
                    {
                        excel.createHeaders(col, 1, "No.Of Bills : " + SCount.ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        double amtt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
                        excel.createHeaders(col, 3, "Total", excel.ColName(col, 3), excel.ColName(col, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        excel.createHeaders(col, 4, Math.Abs(amtt).ToString(), excel.ColName(col, 4), excel.ColName(col, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        col++;
                    }
                    if (RCount != 0)
                    {
                        excel.createHeaders(col, 1, "No.Of Bills : " + RCount.ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        double amtt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2"));
                        excel.createHeaders(col, 3, "Total", excel.ColName(col, 3), excel.ColName(col, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        excel.createHeaders(col, 4, Math.Abs(amtt).ToString(), excel.ColName(col, 4), excel.ColName(col, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        col++;
                    }
                    excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    excel.createHeaders(col, 3, Math.Abs(TotAmt).ToString("0.00"), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    col++;
                    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    col++;
                    excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    excel.createHeaders(col, 3, "Amount", excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    col++;
                    for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
                    {
                        excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(col, 3, Math.Abs(Convert.ToDouble(dtPayTypeDtls.Rows[i].ItemArray[4].ToString())).ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        col++;
                    }


                    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);

                    col++;
                    //excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //excel.createHeaders(col, 3, BillCount.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //col++;
                }

                //for (int i = 0; i < dt.Columns.Count - 1; i++)
                //{

                //    switch (i)
                //    {
                //        case 1: if (rdSummary.Checked == true)
                //                excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);

                //            break;
                //        case 2:
                //            excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                //            break;
                //        case 3: excel.createHeaders(col, i, "Amount", excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                //        case 4: if (rdSummary.Checked == true)
                //                excel.createHeaders(col, i, "", excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;


                //    }


                //}
                //for (int j = 0; j < dt.Rows.Count; j++)
                //{
                //    //if (i > 0)
                //    //{
                //    if (VchTypeNo != dt.Rows[j].ItemArray[5].ToString())
                //    {
                //        VchTypeNo = dt.Rows[j].ItemArray[5].ToString();
                        
                //            if (VchTypeNo == "1") VchTypeName = "Sales";
                //            else if (VchTypeNo == "2") VchTypeName = "Sales Return";

                       

                //        excel.createHeaders(j + col + 1 + ExtraRows, 1, VchTypeName, excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                //        ExtraRows++;
                //    }
                //    if (TempDate != Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"))
                //    {
                //        excel.createHeaders(j + col + 1 + ExtraRows, 1, Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                //        TempDate = Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy");
                //        ExtraRows++;
                //    }
                //    excel.addData(j + col + 1 + ExtraRows, 1, dt.Rows[j].ItemArray[1].ToString(), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), "", 0, CreateExcel.ExAlign.Left, false);
                //    excel.createHeaders(j + col + 1 + ExtraRows, 2, dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                //    excel.createHeaders(j + col + 1 + ExtraRows, 3, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[3].ToString())).ToString(), excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                //    excel.createHeaders(j + col + 1 + ExtraRows, 4, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                //    //}

                //    //switch (i)
                //    //{
                //    //    case 1:
                //    //        if (TempDate != Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"))
                //    //        {
                //    //            excel.createHeaders(j + col + 1, i, Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                //    //            TempDate = Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy");
                //    //            ExtraRows++;
                //    //        }
                //    //        excel.addData(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), "", 0, CreateExcel.ExAlign.Left, false);
                //    //        break;
                //    //    case 2: if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                //    //            excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                //    //        break;                               
                //    //    case 3:
                //    //        excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1, i) + ExtraRows, 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
                //    //    case 4: if (rdSummary.Checked == true)
                //    //        {
                //    //            excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

                //    //        } break;
                //    //    case 5: if (rdSummary.Checked == true)
                //    //            excel.addData(j + col + 1 + ExtraRows, i - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i + 1 - 2), excel.ColName(j + col + 1 + ExtraRows, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);

                //    //        break;


                //    //}

                //}
                ////}
                //col = col + dt.Rows.Count + ExtraRows;
                //col++;

                //if (rdSummary.Checked == true)
                //{
                //    excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                //    excel.createHeaders(col, 3, TotAmt.ToString("0.00"), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                //    col++;
                //    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                //    col++;
                //    excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                //    excel.createHeaders(col, 3, "Amount", excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                //    col++;
                //    for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
                //    {
                //        excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                //        excel.createHeaders(col, 3, dtPayTypeDtls.Rows[i].ItemArray[4].ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                //        col++;
                //    }


                //    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);

                //    col++;
                //    excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                //    excel.createHeaders(col, 3, BillCount.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                //    col++;
                //}

                excel.CompleteDoc("");

            }

        }

        private void btnBigPrint_Click(object sender, EventArgs e)
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
                        ReportSession[5] = "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDaySummary.Checked == true)
                    {
                        ReportSession[4] = "2";
                        ReportSession[5] = "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDateWiseSummaryBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDateWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdMonthSummary.Checked == true)
                    {
                        ReportSession[4] = "3";
                        ReportSession[5] = "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummaryBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdQuarterSummary.Checked == true)
                    {
                        ReportSession[4] = "4";
                        ReportSession[5] = "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummaryBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
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
    }
}
