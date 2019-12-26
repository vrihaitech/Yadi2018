
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
    public partial class APMCRegisterSummary : Form
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

        public APMCRegisterSummary()
        {
            InitializeComponent();
        }
        public APMCRegisterSummary(long vchType)
        {
            InitializeComponent();
            if (vchType == VchType.Sales)
            {
                VoucherType = VchType.Sales;
                this.Text = "APMC Register Summary";
                this.Name = "APMC Register Summary";
            }
            else if (vchType == VchType.Purchase)
            {
                VoucherType = VchType.Purchase;
                this.Text = "APMC Register Summary";
                this.Name = "APMC Register Summary";
            }
            else if (vchType == VchType.RejectionIn)
            {
                VoucherType = VchType.RejectionIn;
                this.Text = "APMC Return Register Summary";
                this.Name = "APMC Return Register Summary";
            }
            else if (vchType == VchType.RejectionOut)
            {
                VoucherType = VchType.RejectionOut;
                this.Text = "APMC Return Register Summary";
                this.Name = "APMC Return Register Summary";
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
                if (rdSummary.Checked == true || rdDetailed.Checked == true || rdDaySummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true )
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
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTAPMCSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                
                    else if (rdDaySummary.Checked == true)
                    {
                        ReportSession[4] = "2";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterDateWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTAPMCSalesRegisterDateWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdMonthSummary.Checked == true)
                    {
                        ReportSession[4] = "3";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTAPMCSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdQuarterSummary.Checked == true)
                    {
                        ReportSession[4] = "4";
                        ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTSalesRegisterMonthWiseSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTAPMCSalesRegisterMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDetailed.Checked == true)
                    {
                        
                            ReportSession[4] = "1";
                            ReportSession[5] = "0";
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTAPMCSalesRegisterDetails.rpt", CommonFunctions.ReportPath), ReportSession);
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
           
        }

        private void rdSummary_CheckedChanged(object sender, EventArgs e)
        {

        }


    }
}

