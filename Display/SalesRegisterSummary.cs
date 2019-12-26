#region
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Windows.Forms;
//using OM;
//using OMControls;

//namespace Yadi.Display
//{
//    public partial class SalesRegisterSummary : Form
//    {

//        CommonFunctions ObjFunction = new CommonFunctions();
//        Transaction.Transactions ObjTrans = new Transaction.Transactions();
//        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
//        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
//        DataSet dsVd = new DataSet();
//        public long CompNo, LedgNo, MNo, Type1, VoucherType;
//        public int voucherno;

//        public static string LedgName, RptTitle;
//        public static int Type;

//        public SalesRegisterSummary()
//        {
//            InitializeComponent();

//        }
//        public SalesRegisterSummary(long vchType)
//        {
//            InitializeComponent();
//            if (vchType == VchType.Sales)
//            {
//                VoucherType = VchType.Sales;
//                this.Text = "Sales Register Summary";
//                this.Name = "Sales Register Summary";
//            }
//            else if (vchType == VchType.Purchase)
//            {
//                VoucherType = VchType.Purchase;
//                this.Text = "Purchase Register Summary";
//                this.Name = "Purchase Register Summary";
//            }
//            else if (vchType == VchType.RejectionIn)
//            {
//                VoucherType = VchType.RejectionIn;
//                this.Text = "Sales Return Register Summary";
//                this.Name = "Sales Return Register Summary";
//            }
//            else if (vchType == VchType.RejectionOut)
//            {
//                VoucherType = VchType.RejectionOut;
//                this.Text = "Purchase Return Register Summary";
//                this.Name = "Purchase Return Register Summary";
//            }
//        }
//        private void SalesRegisterSummary_Load(object sender, EventArgs e)
//        {
//            CompNo = DBGetVal.FirmNo;
//            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
//            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
//            DTToDate.MinDate = DTPFromDate.Value;
//            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
//                BtnExport.Visible = true;
//            else
//                BtnExport.Visible = false;
//        }

//        private void btnPrint_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (rdSummary.Checked == true || rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true || rdDisc.Checked == true || rdDetailed.Checked==true)
//                {
//                    string[] ReportSession;
//                    ReportSession = new string[5];
//                    ReportSession[0] = VoucherType.ToString();
//                    ReportSession[1] = DBGetVal.FirmNo.ToString();
//                    ReportSession[2] = DTPFromDate.Value.ToString(Format.DDMMMYY) ;//Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
//                    ReportSession[3] = DTToDate.Value.ToString(Format.DDMMMYY);//Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");

//                    Form NewF = null;
//                    if (rdSummary.Checked == true)
//                    {

//                        ReportSession[4] = "1";
//                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
//                        else
//                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
//                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                    }

//                    else if (rdDaySummary.Checked == true)
//                    {

//                        ReportSession[4] = "1";
//                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDateWiseSummary(), ReportSession);
//                        else
//                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDateWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
//                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                    }
//                    else if (rdDetailed.Checked == true)
//                    {

//                        ReportSession[4] = "2";
//                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                            NewF = new Display.ReportViewSource(new Reports.GetBill(), ReportSession);
//                        else
//                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDtl.rpt", CommonFunctions.ReportPath), ReportSession);
//                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                    }
//                //    else if (rdMonthSummary.Checked == true)
//                //    {
//                //        ReportSession[4] = "3";
//                //        ReportSession[5] = "0";
//                //        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                //            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
//                //        else
//                //            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
//                //        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                //    }
//                //    else if (rdQuarterSummary.Checked == true)
//                //    {
//                //        ReportSession[4] = "4";
//                //        ReportSession[5] = "0";
//                //        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                //            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
//                //        else
//                //            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
//                //        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);


//                //    }
//                }
//                else
//                {
//                    OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
//                }
//            }
//            catch (Exception exc)
//            {
//                ObjFunction.ExceptionDisplay(exc.Message);
//            }
//        }

//        private void DTPFromDate_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (Convert.ToInt16(e.KeyChar) == 13)
//            {
//                DTToDate.Focus();
//            }
//        }

//        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
//        {
//            if (Convert.ToInt16(e.KeyChar) == 13)
//            {
//                rdSummary.Focus();
//            }
//        }

//        private void BtnExport_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                DataTable dt = new DataTable();
//                string str = "", ReportName = "", PartyName = "", VchTypeNo = "", VchTypeName = "";
//                double BillCount = 0, TotAmt = 0;//, TotFVAmt = 0, TotCrAmt = 0, TotCashAmt = 0, TotCCAmt = 0, TotChqAmt = 0,
//                int dtCount = 0, Cnt = 0;
//                DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VoucherType + ",0," + DBGetVal.FirmNo + "").Table;
//                if (rdSummary.Checked == true)
//                {
//                    DetailsExcel();

//                    //if (VoucherType == VchType.Sales) ReportName = "Sales Register Summary";
//                    //else if (VoucherType == VchType.Purchase) ReportName = "Purchase Register Summary";
//                    //else if (VoucherType == VchType.RejectionIn) ReportName = "Sales Return Register Summary";
//                    ////str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
//                    //str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
//                    //dt = ObjFunction.GetDataView(str).Table;

//                    //TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));

//                    //BillCount = Convert.ToDouble(dt.Compute("Max(VoucherNo)", ""));

//                    //dt.Columns.RemoveAt(0);
//                    //dt.Columns.RemoveAt(1);
//                    //dt.Columns.RemoveAt(1);

//                    //dtCount = dt.Columns.Count; PartyName = "Party";
//                }
//                else if (rdDaySummary.Checked == true)
//                {
//                    if (VoucherType == VchType.Sales) ReportName = "Sales Register Daywise Summary";
//                    else if (VoucherType == VchType.Purchase) ReportName = "Purchase Register Daywise Summary";
//                    else if (VoucherType == VchType.RejectionIn) ReportName = "Sales Return Register Daywise Summary";
//                    else if (VoucherType == VchType.RejectionOut) ReportName = "Purchase Return Register Daywise Summary";
//                    //str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2,0";
//                    str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2,0";
//                    dt = ObjFunction.GetDataView(str).Table;
//                    //dt.Columns.RemoveAt(7);

//                    //TotCashAmt = Convert.ToDouble(dt.Compute("Sum(cash)", ""));
//                    //TotCCAmt = Convert.ToDouble(dt.Compute("Sum(CC)", ""));
//                    //TotCrAmt = Convert.ToDouble(dt.Compute("Sum(Credit)", ""));
//                    //TotChqAmt = Convert.ToDouble(dt.Compute("Sum(Cheque)", ""));
//                    //TotFVAmt = Convert.ToDouble(dt.Compute("Sum(FV)", ""));
//                    TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));
//                    //BillCount = Convert.ToDouble(dt.Compute("Sum(TotalBills)", ""));
//                    dt.Columns.RemoveAt(1);
//                    dt.Columns.RemoveAt(1);
//                    dt.Columns.RemoveAt(1);
//                    dt.Columns.RemoveAt(3);
//                    //dt.Columns.RemoveAt(2);
//                    //dt.Columns.RemoveAt(2);
//                    //dt.Columns.RemoveAt(2);
//                    //dt.Columns.RemoveAt(2);
//                    dtCount = dt.Columns.Count - 1; PartyName = "Party";
//                }
//                else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                {
//                    PartyName = "Perticulars";
//                    // str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,0";
//                    if (rdMonthSummary.Checked == true)
//                    {
//                        str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,0";

//                        if (VoucherType == VchType.Sales) ReportName = "Monthwise Sales Summary";
//                        else if (VoucherType == VchType.Purchase) ReportName = "Monthwise Purchase Summary";
//                        else if (VoucherType == VchType.RejectionIn) ReportName = "Monthwise Sales Return Summary";
//                        else if (VoucherType == VchType.RejectionOut) ReportName = "Monthwise Purchase Return Summary";
//                    }
//                    if (rdQuarterSummary.Checked == true)
//                    {
//                        str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,0";
//                        if (VoucherType == VchType.Sales) ReportName = "Quaterwise Sales Summary";
//                        else if (VoucherType == VchType.Purchase) ReportName = "Quaterwise Purchase Summary";
//                        else if (VoucherType == VchType.RejectionIn) ReportName = "Quaterwise Sales Return Summary";
//                        else if (VoucherType == VchType.RejectionOut) ReportName = "Quaterwise Purchase Return Summary";
//                    }
//                    dt = ObjFunction.GetDataView(str).Table;
//                    // dt.Columns.RemoveAt(7);
//                    //TotCashAmt = Convert.ToDouble(dt.Compute("Sum(cash)", ""));
//                    //TotCCAmt = Convert.ToDouble(dt.Compute("Sum(CC)", ""));
//                    //TotCrAmt = Convert.ToDouble(dt.Compute("Sum(Credit)", ""));
//                    //TotChqAmt = Convert.ToDouble(dt.Compute("Sum(Cheque)", ""));

//                    //TotFVAmt = Convert.ToDouble(dt.Compute("Sum(FV)", ""));
//                    TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));
//                    dt.Columns.RemoveAt(0);
//                    dt.Columns.RemoveAt(1);
//                    dt.Columns.RemoveAt(1);
//                    dt.Columns.RemoveAt(3);
//                    //dt.Columns.RemoveAt(2);
//                    //dt.Columns.RemoveAt(2);
//                    //dt.Columns.RemoveAt(2);
//                    //dt.Columns.RemoveAt(2);
//                    dtCount = dt.Columns.Count - 1;
//                }
//                //else if (rdQuarterSummary.Checked == true)
//                //{
//                //    if (VoucherType == VchType.Sales)  ReportName = "Quarterly Sale Register";
//                //    else ReportName = "Quarterly Purchase Register";
//                //    str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,0";
//                //    dt = ObjFunction.GetDataView(str).Table;
//                //    dtCount = dt.Columns.Count - 3; PartyName = "Quarter";
//                //}
//                else
//                {
//                    OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
//                }
//                if (str != "")
//                {
//                    //dt = ObjFunction.GetDataView(str).Table;


//                    int col = 1;
//                    CreateExcel excel = new CreateExcel();
//                    //Company Name Header
//                    excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
//                    col++;
//                    //Company Address Header
//                    excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
//                    col++;
//                    //Report Name And Dates
//                    excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
//                    col++;
//                    excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
//                    col++;
//                    excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
//                    col++;

//                    for (int i = 0; i < dt.Columns.Count - 1; i++)
//                    {
//                        switch (i)
//                        {
//                            case 0: if (rdSummary.Checked == true || rdDaySummary.Checked == true)
//                                {
//                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);

//                                }
//                                if (rdQuarterSummary.Checked == true || rdMonthSummary.Checked == true)
//                                    excel.createHeaders(col, i + 1, PartyName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                                break;
//                            case 1: Cnt = 0; VchTypeName = "";VchTypeNo = "0";//if(rdSummary.Checked==true || rdDaySummary.Checked==true)
//                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

//                                break;
//                            case 2: Cnt = 0; VchTypeName = "";VchTypeNo = "0";//if (rdSummary.Checked == true || rdDaySummary.Checked==true)
//                                excel.createHeaders(col, i + 1, "Amount", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
//                            case 3: if (rdSummary.Checked == true)
//                                    excel.createHeaders(col, i + 1, "", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;

//                            default: if (rdSummary.Checked == true)
//                                    excel.createHeaders(col, i + 1 - 2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                                {
//                                    excel.createHeaders(col, i + 1 - 3, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 3), excel.ColName(col, i + 1 - 3), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                    Cnt = 0; VchTypeNo = "0";
//                                }
//                                break;
//                        }

//                        for (int j = 0; j < dt.Rows.Count; j++)
//                        {
//                            switch (i)
//                            {
//                                case 0: if (rdDaySummary.Checked == true)
//                                    {
//                                        if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
//                                        {
//                                            if (VchTypeName != "")
//                                            {
//                                                double amt=0;
//                                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
//                                                    amt = 0;
//                                                else
//                                                 amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
//                                                excel.createHeaders(j + col + 1 + Cnt, 1, "Total", excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                                excel.createHeaders(j + col + 1 + Cnt, 3, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + Cnt, 3), excel.ColName(j + col + 1 + Cnt, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                                Cnt++;
//                                            }
//                                            VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
//                                            if (VoucherType == VchType.Purchase)
//                                            {
//                                                if (VchTypeNo == "1") VchTypeName = "Purchase";
//                                                else if (VchTypeNo == "2") VchTypeName = "Purchase Return";

//                                            }
//                                            else if (VoucherType == VchType.Sales)
//                                            {
//                                                if (VchTypeNo == "1") VchTypeName = "Sales";
//                                                else if (VchTypeNo == "2") VchTypeName = "Sales Return";

//                                            }
//                                            else if (VoucherType == VchType.RejectionOut)
//                                            {
//                                                VchTypeName = "Purchase Return";
//                                            }
//                                            else if (VoucherType == VchType.RejectionIn)
//                                            {
//                                                VchTypeName = "Sales Return";
//                                            }


//                                            excel.createHeaders(j + col + 1 + Cnt, 1, VchTypeName, excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                                            Cnt++;

//                                        }
//                                        if (Convert.ToDateTime(dt.Rows[j].ItemArray[i].ToString()).ToString("dd-MMM-yyyy") != "01-Jan-1900")
//                                            excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
//                                        else
//                                            excel.addData(j + col + 1 + Cnt, i + 1, "", excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
//                                    }
//                                    else
//                                    {//if(rdSummary.Checked==true )
//                                        if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
//                                        {
//                                            if (VchTypeName != "")
//                                            {
//                                                double amt = 0;
//                                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
//                                                    amt = 0;
//                                                else
//                                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
//                                                excel.createHeaders(j + col + 1 + Cnt, 1, "Total", excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                                excel.createHeaders(j + col + 1 + Cnt, 3, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + Cnt, 3), excel.ColName(j + col + 1 + Cnt, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                                Cnt++;
//                                            }

//                                            VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
//                                            if (VoucherType == VchType.Purchase)
//                                            {
//                                                if (VchTypeNo == "1") VchTypeName = "Purchase";
//                                                else if (VchTypeNo == "2") VchTypeName = "Purchase Return";

//                                            }
//                                            else if (VoucherType == VchType.Sales)
//                                            {
//                                                if (VchTypeNo == "1") VchTypeName = "Sales";
//                                                else if (VchTypeNo == "2") VchTypeName = "Sales Return";

//                                            }
//                                            else if (VoucherType == VchType.RejectionOut)
//                                            {
//                                                VchTypeName = "Purchase Return";
//                                            }
//                                            else if (VoucherType == VchType.RejectionIn)
//                                            {
//                                                VchTypeName = "Sales Return";
//                                            }


//                                            excel.createHeaders(j + col + 1 + Cnt, 1, VchTypeName, excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                                            Cnt++;

//                                        }

//                                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
//                                    }
//                                    break;
//                                case 1: if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
//                                    {

//                                        VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
//                                        Cnt++;
//                                        if (rdDaySummary.Checked == true)
//                                        {
//                                            if (VchTypeNo != "1")
//                                                Cnt++;
//                                        }
//                                    }
//                                    if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                                    {
//                                        if (VchTypeNo != "1")
//                                            Cnt++;
//                                        excel.createHeaders(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                                    }
//                                    //else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                                    //    excel.createHeaders(j + col + 1, i + 1 - 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 1), excel.ColName(j + col + 1, i + 1 - 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                                    else
//                                    {

//                                        if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
//                                        {

//                                            VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
//                                            Cnt++;

//                                        }
//                                        excel.createHeaders(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                    }
//                                    if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                                        BillCount = BillCount + Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString());
//                                    break;
//                                case 2: if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
//                                    {

//                                        VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
//                                        Cnt++;
//                                        if (VchTypeNo != "1")
//                                            Cnt++;
//                                    }//if (rdSummary.Checked == true || rdDaySummary.Checked==true)
//                                    excel.createHeaders(j + col + 1 + Cnt, i + 1, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString())).ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
//                                case 3: if (rdSummary.Checked == true)
//                                    {
//                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

//                                    } break;
//                                case 4: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                        excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
//                                    else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                                    {
//                                        if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
//                                        {
//                                            if (VchTypeNo != "1")
//                                                Cnt++;
//                                            VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
//                                            Cnt++;
//                                        }
//                                        excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                    }
//                                    break;
//                                default: if (rdSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                        excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
//                                    else if (rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true) //excel.createHeaders(j + col + 1, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 3), excel.ColName(j + col + 1, i + 1 - 3), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                    {
//                                        if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
//                                        {
//                                            if (VchTypeNo != "1")
//                                                Cnt++;
//                                            VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
//                                            Cnt++;
//                                        }
//                                        excel.addData(j + col + 1 + Cnt, i + 1 - 3, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1 - 3), excel.ColName(j + col + 1 + Cnt, i + 1 - 3), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

//                                    }
//                                    break;

//                            }

//                        }
//                    }
//                    col = col + dt.Rows.Count + Cnt;
//                    col++;

//                    if (rdSummary.Checked == true)
//                    {
//                        excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        excel.createHeaders(col, 3, Math.Abs(TotAmt).ToString("0.00"), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        col++;
//                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        col++;
//                        excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        excel.createHeaders(col, 3, "Amount", excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        col++;
//                        for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
//                        {
//                            excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                            excel.createHeaders(col, 3, Math.Abs(Convert.ToDouble(dtPayTypeDtls.Rows[i].ItemArray[4].ToString())).ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                            col++;
//                        }

//                        //excel.createHeaders(col, 1, "Cash", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 3, TotCashAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //excel.createHeaders(col, 1, "CC", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 3, TotCCAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //excel.createHeaders(col, 1, "Cheque", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 3, TotChqAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //excel.createHeaders(col, 1, "Credit", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 3, TotCrAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //if (VchType.Sales == VoucherType)
//                        //{
//                        //    excel.createHeaders(col, 1, "FV", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //    excel.createHeaders(col, 3, TotFVAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //    col++;
//                        //}
//                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);

//                        col++;
//                        //excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 3, BillCount.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                    }
//                    else// (rdDaySummary.Checked == true)
//                    {
//                        double amt = 0;
//                        if (VchTypeNo == "1")
//                        {
//                            if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
//                                amt = 0;
//                            else
//                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
//                        }
//                        else
//                        {

//                            if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2")) == true)
//                                amt = 0;
//                            else
//                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2"));
//                        }
//                        excel.createHeaders(col, 1, "Total", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        excel.createHeaders(col, 3,  Math.Abs(amt).ToString(), excel.ColName(col, 3), excel.ColName(col, 3), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        col++;
//                        excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        excel.createHeaders(col, 2, Math.Abs(TotAmt).ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        col++;
//                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        col++;
//                        excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        excel.createHeaders(col, 2, "Amount", excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        col++;
//                        for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
//                        {
//                            excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                            excel.createHeaders(col, 2, Math.Abs(Convert.ToDouble(dtPayTypeDtls.Rows[i].ItemArray[4].ToString())).ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                            col++;
//                        }
//                        //excel.createHeaders(col, 1, "Cash", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 2, TotCashAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //excel.createHeaders(col, 1, "CC", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 2, TotCCAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //excel.createHeaders(col, 1, "Cheque", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 2, TotChqAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //excel.createHeaders(col, 1, "Credit", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 2, TotCrAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                        //if (VchType.Sales == VoucherType)
//                        //{
//                        //    excel.createHeaders(col, 1, "FV", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //    excel.createHeaders(col, 2, TotFVAmt.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //    col++;
//                        //}
//                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        col++;
//                        //excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        //excel.createHeaders(col, 2, BillCount.ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        //col++;
//                    }
//                    excel.CompleteDoc("");

//                }
//            }
//            catch (Exception exc)
//            {
//                ObjFunction.ExceptionDisplay(exc.Message);
//            }

//        }

//        private void btnExit_Click(object sender, EventArgs e)
//        {
//            this.Close();
//        }

//        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
//        {
//            DTToDate.MinDate = DTPFromDate.Value;
//        }

//        public void DetailsExcel()
//        {
//            DataTable dt = new DataTable();
//            string str = "", ReportName = "", TempDate = "", VchTypeNo = "", VchTypeName = "";//PartyName = "",

//            double BillCount = 0, TotAmt = 0;//, TotFVAmt = 0, TotCrAmt = 0,TotCashAmt = 0, TotCCAmt = 0, TotChqAmt = 0,
//            int dtCount = 0, ExtraRows = 0;
//            DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VoucherType + ",0," + DBGetVal.FirmNo + "").Table;

//            if (VoucherType == VchType.Sales) ReportName = "Sales Register Summary";
//            else if (VoucherType == VchType.Purchase) ReportName = "Purchase Register Summary";
//            else if (VoucherType == VchType.RejectionIn) ReportName = "Sales Return Register Summary";
//            else if (VoucherType == VchType.RejectionOut) ReportName = "Purchase Return Register Summary";
//            //str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
//            str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
//            dt = ObjFunction.GetDataView(str).Table;
//            //dt.Columns.RemoveAt(7);
//            long SCount = Convert.ToInt64(dt.Compute("Count(VchTypeNo)", "VchTypeNo=1"));
//            long RCount = Convert.ToInt64(dt.Compute("Count(VchTypeNo)", "VchTypeNo=2"));
//            TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));

//            BillCount = Convert.ToDouble(dt.Compute("Max(VoucherNo)", ""));

//            //dt.Columns.RemoveAt(0);
//            dt.Columns.RemoveAt(2);
//            dt.Columns.RemoveAt(2);

//            dtCount = dt.Columns.Count - 2; //PartyName = "Party";

//            if (str != "")
//            {



//                int col = 1;
//                CreateExcel excel = new CreateExcel();
//                //Company Name Header
//                excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
//                col++;
//                //Company Address Header
//                excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
//                col++;
//                //Report Name And Dates
//                excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
//                col++;
//                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
//                col++;
//                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
//                col++;

//                for (int i = 0; i < dt.Columns.Count - 1; i++)
//                {

//                    switch (i)
//                    {
//                        case 1: if (rdSummary.Checked == true)
//                                excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);

//                            break;
//                        case 2:
//                            excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

//                            break;
//                        case 3: excel.createHeaders(col, i, "Amount", excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
//                        case 4: if (rdSummary.Checked == true)
//                                excel.createHeaders(col, i, "", excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;


//                    }


//                }
//                for (int j = 0; j < dt.Rows.Count; j++)
//                {
//                    //if (i > 0)
//                    //{
//                    if (VchTypeNo != dt.Rows[j].ItemArray[5].ToString())
//                    {
//                        if (VchTypeNo == "1")
//                        {
//                            double amt = 0;
//                            if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + "")) == true)
//                            amt=0;
//                            else
//                            amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + ""));
//                            excel.createHeaders(j + col + 1 + ExtraRows, 1, "Day Total" , excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                            excel.createHeaders(j + col + 1 + ExtraRows, 4, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                            ExtraRows++;
//                        }
//                        VchTypeNo = dt.Rows[j].ItemArray[5].ToString();
//                        if (VoucherType == VchType.Purchase)
//                        {
//                            if (VchTypeNo == "1") VchTypeName = "Purchase";
//                            else if (VchTypeNo == "2") VchTypeName = "Purchase Return";

//                        }
//                        else if (VoucherType == VchType.Sales)
//                        {
//                            if (VchTypeNo == "1") VchTypeName = "Sales";
//                            else if (VchTypeNo == "2") VchTypeName = "Sales Return";

//                        }
//                        else if (VoucherType == VchType.RejectionOut)
//                        {
//                            VchTypeName = "Purchase Return";
//                        }
//                        else if (VoucherType == VchType.RejectionIn)
//                        {
//                            VchTypeName = "Sales Return";
//                        }
//                        if (VchTypeNo == "2")
//                        {
//                            if (SCount != 0)
//                            {
//                                excel.createHeaders(j + col + 1 + ExtraRows, 1, "No.Of Bills : " + SCount.ToString(), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                                double amt = 0;
//                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
//                                    amt = 0;
//                                else
//                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
//                                excel.createHeaders(j + col + 1 + ExtraRows, 3, "Total" , excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                excel.createHeaders(j + col + 1 + ExtraRows, 4, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                ExtraRows++;
//                            }
//                            excel.createHeaders(j + col + 1 + ExtraRows, 1, VchTypeName, excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        }
//                        else
//                            excel.createHeaders(j + col + 1 + ExtraRows, 1, VchTypeName, excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

//                        ExtraRows++;
//                    }
//                    if (TempDate != Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"))
//                    {
//                        if (rdSummary.Checked == true)
//                        {
//                            if (TempDate != "" && dt.Rows[j - 1].ItemArray[5].ToString()==VchTypeNo)
//                            {
//                                double amt = 0;
//                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + "")) == true)
//                                    amt = 0;
//                                else
//                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + ""));
//                                excel.createHeaders(j + col + 1 + ExtraRows, 1, "Day Total", excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                excel.createHeaders(j + col + 1 + ExtraRows, 4, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                                ExtraRows++;
//                            }
//                        }
//                        excel.createHeaders(j + col + 1 + ExtraRows, 1, Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 4), 4, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
//                        TempDate = Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy");
//                        ExtraRows++;
//                    }
//                    excel.addData(j + col + 1 + ExtraRows, 1, dt.Rows[j].ItemArray[1].ToString(), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), "", 0, CreateExcel.ExAlign.Left, false);
//                    excel.createHeaders(j + col + 1 + ExtraRows, 2, dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                    excel.createHeaders(j + col + 1 + ExtraRows, 3, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[3].ToString())).ToString(), excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                    excel.createHeaders(j + col + 1 + ExtraRows, 4, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

//                    //}

//                    //switch (i)
//                    //{
//                    //    case 1:
//                    //        if (TempDate != Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"))
//                    //        {
//                    //            excel.createHeaders(j + col + 1, i, Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                    //            TempDate = Convert.ToDateTime(dt.Rows[j].ItemArray[0]).ToString("dd-MMM-yyyy");
//                    //            ExtraRows++;
//                    //        }
//                    //        excel.addData(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), "", 0, CreateExcel.ExAlign.Left, false);
//                    //        break;
//                    //    case 2: if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                    //            excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                    //        break;                               
//                    //    case 3:
//                    //        excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1, i) + ExtraRows, 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right); break;
//                    //    case 4: if (rdSummary.Checked == true)
//                    //        {
//                    //            excel.createHeaders(j + col + 1 + ExtraRows, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i), excel.ColName(j + col + 1 + ExtraRows, i), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);

//                    //        } break;
//                    //    case 5: if (rdSummary.Checked == true)
//                    //            excel.addData(j + col + 1 + ExtraRows, i - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + ExtraRows, i + 1 - 2), excel.ColName(j + col + 1 + ExtraRows, i + 1 - 2), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);

//                    //        break;


//                    //}

//                }
//                //}
//                col = col + dt.Rows.Count + ExtraRows;
//                col++;

//                if (rdSummary.Checked == true)
//                {
//                    double amt = 0;
//                    if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[dt.Rows.Count - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[dt.Rows.Count - 1].ItemArray[5].ToString() + "")) == true)
//                        amt = 0;
//                    else
//                    amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[dt.Rows.Count - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[dt.Rows.Count - 1].ItemArray[5].ToString() + ""));
//                    excel.createHeaders(col, 1, "Day Total", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                    excel.createHeaders(col, 4, Math.Abs(amt).ToString(), excel.ColName(col, 4), excel.ColName(col, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                    col++;
//                    if (RCount != 0)
//                    {
//                        excel.createHeaders(col, 1, "No.Of Bills : " + RCount.ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        double amtt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2"));
//                        excel.createHeaders(col, 3, "Total", excel.ColName(col, 3), excel.ColName(col, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        excel.createHeaders(col, 4, Math.Abs(amtt).ToString(), excel.ColName(col, 4), excel.ColName(col, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        col++;
//                    }
//                    excel.createHeaders(col, 1, "TotalAmount", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                    excel.createHeaders(col, 3, Math.Abs(TotAmt).ToString("0.00"), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                    col++;
//                    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                    col++;
//                    excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                    excel.createHeaders(col, 3, "Amount", excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                    col++;
//                    for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
//                    {
//                        excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                        excel.createHeaders(col, 3, Math.Abs(Convert.ToDouble(dtPayTypeDtls.Rows[i].ItemArray[4].ToString())).ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                        col++;
//                    }


//                    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);

//                    col++;
//                    //excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
//                    //excel.createHeaders(col, 3, BillCount.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
//                    //col++;
//                }

//                excel.CompleteDoc("");

//            }

//        }

//        private void btnBigPrint_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (rdSummary.Checked == true || rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
//                {
//                    string[] ReportSession;
//                    ReportSession = new string[6];
//                    ReportSession[0] = VoucherType.ToString();
//                    ReportSession[1] = DBGetVal.FirmNo.ToString();
//                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
//                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
//                    Form NewF = null;
//                    if (rdSummary.Checked == true)
//                    {
//                        ReportSession[4] = "1";
//                        ReportSession[5] = "0";
//                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterBig(), ReportSession);
//                        else
//                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterBig.rpt", CommonFunctions.ReportPath), ReportSession);
//                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                    }
//                    else if (rdDaySummary.Checked == true)
//                    {
//                        ReportSession[4] = "2";
//                        ReportSession[5] = "0";
//                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDateWiseSummaryBig(), ReportSession);
//                        else
//                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDateWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
//                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                    }
//                    else if (rdMonthSummary.Checked == true)
//                    {
//                        ReportSession[4] = "3";
//                        ReportSession[5] = "0";
//                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummaryBig(), ReportSession);
//                        else
//                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
//                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                    }
//                    else if (rdQuarterSummary.Checked == true)
//                    {
//                        ReportSession[4] = "4";
//                        ReportSession[5] = "0";
//                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
//                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummaryBig(), ReportSession);
//                        else
//                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
//                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
//                    }
//                }
//                else
//                {
//                    OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
//                }
//            }
//            catch (Exception exc)
//            {
//                ObjFunction.ExceptionDisplay(exc.Message);
//            }
//        }






//    }
//}
#endregion
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
    public partial class SalesRegisterSummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VoucherType;
        public int voucherno;

        public static string LedgName, RptTitle;
        public static int Type;

        public SalesRegisterSummary()
        {
            InitializeComponent();
        }
        public SalesRegisterSummary(long vchType)
        {
            InitializeComponent();

            if (DBGetVal.KachhaFirm == false)
            {
               

                if (vchType == VchType.Sales)
                {
                    VoucherType = VchType.Sales;
                    this.Text = "Sales Register Summary";
                    this.Name = "Sales Register Summary";
                }
                else if (vchType == VchType.Purchase)
                {
                    VoucherType = VchType.Purchase;
                    this.Text = "Purchase Register Summary";
                    this.Name = "Purchase Register Summary";
                }
                else if (vchType == VchType.RejectionIn)
                {
                    VoucherType = VchType.RejectionIn;
                    this.Text = "Sales Return Register Summary";
                    this.Name = "Sales Return Register Summary";
                }
                else if (vchType == VchType.RejectionOut)
                {
                    VoucherType = VchType.RejectionOut;
                    this.Text = "Purchase Return Register Summary";
                    this.Name = "Purchase Return Register Summary";
                }
            }
            else {

                if (vchType == VchType.Sales)
                {
                    VoucherType = VchType.DSales;
                    this.Text = "Estimate Sales  Register Summary";
                    this.Name = "Estimate Sales Register Summary";
                }
                else if (vchType == VchType.Purchase)
                {
                    VoucherType = VchType.DPurchase;
                    this.Text = "Estimate Purchase Register Summary";
                    this.Name = "Estimate Purchase Register Summary";
                }
                else if (vchType == VchType.RejectionIn)
                {
                    VoucherType = VchType.DRejectionIn;
                    this.Text = "Estimate Sales Return Register Summary";
                    this.Name = "Estimate Sales Return Register Summary";
                }
                else if (vchType == VchType.RejectionOut)
                {
                    VoucherType = VchType.DRejectionOut;
                    this.Text = "Estimate Purchase Return Register Summary";
                    this.Name = "Estimate Purchase Return Register Summary";
                }

            }
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
                if (rdSummary.Checked == true || rdDetailed.Checked == true || rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true || rdDisc.Checked == true)
                {
                 //   VoucherType = 15;
                    string[] ReportSession;
                    ReportSession = new string[6];
                    ReportSession[0] = VoucherType.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    Form NewF = null;
                    if (rdSummary.Checked == true)
                    {
                        ReportSession[4] = "1";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
                        else
                            try
                            {
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                            }
                            catch (Exception exc)
                            {
                                ObjFunction.ExceptionDisplay(exc.Message);
                            }
                    }
                    else if (rdDisc.Checked == true)
                    {
                        ReportSession[4] = "1";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDisc.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDaySummary.Checked == true)
                    {
                        ReportSession[4] = "2";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDateWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDateWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdMonthSummary.Checked == true)
                    {
                        ReportSession[4] = "3";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdQuarterSummary.Checked == true)
                    {
                        ReportSession[4] = "4";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDetailed.Checked == true)
                    {
                        OMMessageBox.Show("report not avilable");
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
                string str = "", ReportName = "", PartyName = "", VchTypeNo = "", VchTypeName = "";
                double BillCount = 0, TotAmt = 0;//, TotFVAmt = 0, TotCrAmt = 0, TotCashAmt = 0, TotCCAmt = 0, TotChqAmt = 0,
                int dtCount = 0, Cnt = 0;
                DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VoucherType + ",0," + DBGetVal.FirmNo + "").Table;
                if (rdSummary.Checked == true)
                {
                    DetailsExcel();

                    //if (VoucherType == VchType.Sales) ReportName = "Sales Register Summary";
                    //else if (VoucherType == VchType.Purchase) ReportName = "Purchase Register Summary";
                    //else if (VoucherType == VchType.RejectionIn) ReportName = "Sales Return Register Summary";
                    ////str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.firmno.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
                    //str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.firmno.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
                    //dt = ObjFunction.GetDataView(str).Table;

                    //TotAmt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", ""));

                    //BillCount = Convert.ToDouble(dt.Compute("Max(VoucherNo)", ""));

                    //dt.Columns.RemoveAt(0);
                    //dt.Columns.RemoveAt(1);
                    //dt.Columns.RemoveAt(1);

                    //dtCount = dt.Columns.Count; PartyName = "Party";
                }
                else if (rdDaySummary.Checked == true)
                {
                    if (VoucherType == VchType.Sales) ReportName = "Sales Register Daywise Summary";
                    else if (VoucherType == VchType.Purchase) ReportName = "Purchase Register Daywise Summary";
                    else if (VoucherType == VchType.RejectionIn) ReportName = "Sales Return Register Daywise Summary";
                    else if (VoucherType == VchType.RejectionOut) ReportName = "Purchase Return Register Daywise Summary";
                    //str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.firmno.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2,0";
                    str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2,0";
                    dt = ObjFunction.GetDataView(str).Table;
                    //dt.Columns.RemoveAt(7);

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
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    dtCount = dt.Columns.Count - 1; PartyName = "Party";
                }
                else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                {
                    PartyName = "Perticulars";
                    // str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.firmno.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,0";
                    if (rdMonthSummary.Checked == true)
                    {
                        str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,0";

                        if (VoucherType == VchType.Sales) ReportName = "Monthwise Sales Summary";
                        else if (VoucherType == VchType.Purchase) ReportName = "Monthwise Purchase Summary";
                        else if (VoucherType == VchType.RejectionIn) ReportName = "Monthwise Sales Return Summary";
                        else if (VoucherType == VchType.RejectionOut) ReportName = "Monthwise Purchase Return Summary";
                    }
                    if (rdQuarterSummary.Checked == true)
                    {
                        str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,0";
                        if (VoucherType == VchType.Sales) ReportName = "Quaterwise Sales Summary";
                        else if (VoucherType == VchType.Purchase) ReportName = "Quaterwise Purchase Summary";
                        else if (VoucherType == VchType.RejectionIn) ReportName = "Quaterwise Sales Return Summary";
                        else if (VoucherType == VchType.RejectionOut) ReportName = "Quaterwise Purchase Return Summary";
                    }
                    dt = ObjFunction.GetDataView(str).Table;
                    // dt.Columns.RemoveAt(7);
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
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    //dt.Columns.RemoveAt(2);
                    dtCount = dt.Columns.Count - 1;
                }
                //else if (rdQuarterSummary.Checked == true)
                //{
                //    if (VoucherType == VchType.Sales)  ReportName = "Quarterly Sale Register";
                //    else ReportName = "Quarterly Purchase Register";
                //    str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.firmno.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,0";
                //    dt = ObjFunction.GetDataView(str).Table;
                //    dtCount = dt.Columns.Count - 3; PartyName = "Quarter";
                //}
                else
                {
                    OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                if (str != "")
                {
                    //dt = ObjFunction.GetDataView(str).Table;


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
                            case 1: Cnt = 0; VchTypeName = ""; VchTypeNo = "0";//if(rdSummary.Checked==true || rdDaySummary.Checked==true)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                                break;
                            case 2: Cnt = 0; VchTypeName = ""; VchTypeNo = "0";//if (rdSummary.Checked == true || rdDaySummary.Checked==true)
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
                                                double amt = 0;
                                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
                                                    amt = 0;
                                                else
                                                    amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
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
                                                double amt = 0;
                                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
                                                    amt = 0;
                                                else
                                                    amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
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

                    if (rdSummary.Checked == true)
                    {
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

                        //excel.createHeaders(col, 1, "Cash", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        //excel.createHeaders(col, 3, TotCashAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //col++;
                        //excel.createHeaders(col, 1, "CC", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        //excel.createHeaders(col, 3, TotCCAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //col++;
                        //excel.createHeaders(col, 1, "Cheque", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        //excel.createHeaders(col, 3, TotChqAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //col++;
                        //excel.createHeaders(col, 1, "Credit", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        //excel.createHeaders(col, 3, TotCrAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //col++;
                        //if (VchType.Sales == VoucherType)
                        //{
                        //    excel.createHeaders(col, 1, "FV", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        //    excel.createHeaders(col, 3, TotFVAmt.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //    col++;
                        //}
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 4), 4, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);

                        col++;
                        //excel.createHeaders(col, 1, "No.Of Bills", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        //excel.createHeaders(col, 3, BillCount.ToString(), excel.ColName(col, 3), excel.ColName(col, 4), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //col++;
                    }
                    else// (rdDaySummary.Checked == true)
                    {
                        double amt = 0;
                        if (VchTypeNo == "1")
                        {
                            if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
                                amt = 0;
                            else
                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
                        }
                        else
                        {

                            if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2")) == true)
                                amt = 0;
                            else
                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=2"));
                        }
                        excel.createHeaders(col, 1, "Total", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        excel.createHeaders(col, 3, Math.Abs(amt).ToString(), excel.ColName(col, 3), excel.ColName(col, 3), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        col++;
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
                    excel.CompleteDoc("");

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
            string str = "", ReportName = "", TempDate = "", VchTypeNo = "", VchTypeName = "";//PartyName = "",

            double BillCount = 0, TotAmt = 0;//, TotFVAmt = 0, TotCrAmt = 0,TotCashAmt = 0, TotCCAmt = 0, TotChqAmt = 0,
            int dtCount = 0, ExtraRows = 0;
            DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VoucherType + ",0," + DBGetVal.FirmNo + "").Table;

            if (VoucherType == VchType.Sales) ReportName = "Sales Register Summary";
            else if (VoucherType == VchType.Purchase) ReportName = "Purchase Register Summary";
            else if (VoucherType == VchType.RejectionIn) ReportName = "Sales Return Register Summary";
            else if (VoucherType == VchType.RejectionOut) ReportName = "Purchase Return Register Summary";
            //str = "Exec GetSaleVouchEntryDtlsByDate " + VoucherType + "," + DBGetVal.firmno.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
            str = "Exec GetSaleVouchEntryDayDtls " + VoucherType + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1,0";
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
                            double amt = 0;
                            if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + "")) == true)
                                amt = 0;
                            else
                                amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + ""));
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
                                double amt = 0;
                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1")) == true)
                                    amt = 0;
                                else
                                    amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "VchTypeNo=1"));
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
                                double amt = 0;
                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + "")) == true)
                                    amt = 0;
                                else
                                    amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[j - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[j - 1].ItemArray[5].ToString() + ""));
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
                    double amt = 0;
                    if (Convert.IsDBNull(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[dt.Rows.Count - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[dt.Rows.Count - 1].ItemArray[5].ToString() + "")) == true)
                        amt = 0;
                    else
                        amt = Convert.ToDouble(dt.Compute("Sum(TotalAmount)", "Date='" + dt.Rows[dt.Rows.Count - 1].ItemArray[0].ToString() + "'  and VchTypeNo=" + dt.Rows[dt.Rows.Count - 1].ItemArray[5].ToString() + ""));
                    excel.createHeaders(col, 1, "Day Total", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    excel.createHeaders(col, 4, Math.Abs(amt).ToString(), excel.ColName(col, 4), excel.ColName(col, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    col++;
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

                excel.CompleteDoc("");

            }

        }

        private void btnBigPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdSummary.Checked == true || rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true || rdDetailed.Checked == true)
                {
                    string[] ReportSession;
                    ReportSession = new string[6];
                    ReportSession[0] = VoucherType.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    Form NewF = null;
                    if (rdSummary.Checked == true)
                    {
                        ReportSession[4] = "1";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDaySummary.Checked == true)
                    {
                        ReportSession[4] = "2";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDateWiseSummaryBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDateWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdMonthSummary.Checked == true)
                    {
                        ReportSession[4] = "3";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummaryBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdQuarterSummary.Checked == true)
                    {
                        ReportSession[4] = "4";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummaryBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterMonthWiseSummaryBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDetailed.Checked == true)
                    {
                        string[] ReportSession1;
                        ReportSession1 = new string[5];
                        ReportSession1[0] = VoucherType.ToString();
                        ReportSession1[1] = DBGetVal.FirmNo.ToString();
                        ReportSession1[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession1[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession1[4] = "4";
                        // ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDetail(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegisterDetail.rpt", CommonFunctions.ReportPath), ReportSession1);
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

        private void rdSummary_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}

