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
    public partial class UserWiseSalesRegister : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo, VchCode = 0;
        public string ItName, RptTitle, ItNm;
        //string strItemNo = "";
        //long RateTypeNo = 0;

        public UserWiseSalesRegister()
        {
            InitializeComponent();
        }
        public UserWiseSalesRegister(long vchno)
        {
            VchCode = vchno;
            InitializeComponent();
            if (VchCode == VchType.Sales)
                this.Text = "UserWiseSalesRegister";
            else if (VchCode == VchType.Purchase)
                this.Text = "UserWisePurchaseRegister";
        }

        private void UserWiseSalesDetails_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                btnExport.Visible = true;
            else
                btnExport.Visible = false;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hi");
            this.Cursor = Cursors.WaitCursor;
            Form NewF = null;
            try
            {
                if (rdDetails.Checked == true || rdSummary.Checked == true)
                {
                    string[] ReportSession;
                    ReportSession = new string[5];
                    ReportSession[0] = VchCode.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = "0";


                    if (rdDetails.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTUserWiseSalesRegisterDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTUserWiseSalesRegisterDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else if (rdSummary.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTUserWiseSalesRegisterSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTUserWiseSalesRegisterSummary.rpt", CommonFunctions.ReportPath), ReportSession);

                    }
                }
                else if (rdMonthSummary.Checked == true)
                {
                    string[] ReportSession;
                    ReportSession = new string[6];
                    ReportSession[0] = VchCode.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = "3";
                    ReportSession[5] = "0";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RptUserwiseMonthlySalesDtla(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptUserwiseMonthlySalesDtla.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                else if (rdQuarterSummary.Checked == true)
                {
                    string[] ReportSession;
                    ReportSession = new string[6];
                    ReportSession[0] = VchCode.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = "4";
                    ReportSession[5] = "0";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RptUserwiseMonthlySalesDtla(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptUserwiseMonthlySalesDtla.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
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
                BtnShow.Focus();
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string str = "", ReportName = "", VchTypeNo = ""; int dtCount = 0, Cnt = 0;//, VoucherType = 0, VchTypeName = ""
            if (rdDetails.Checked == true)
            {
                if (VchCode == VchType.Sales)
                    ReportName = "Userwise Sales Register Details";
                else if (VchCode == VchType.Purchase)
                    ReportName = "Userwise Purchase Register Details";
            }
            if (rdSummary.Checked == true || rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
            {
                try
                {
                    if (VchCode == VchType.Sales)
                        ReportName = "Userwise Sales Register Summary";
                    else if (VchCode == VchType.Purchase)
                        ReportName = "Userwise Purchase Register Summary";
                    if (rdSummary.Checked == true)
                    {
                        str = "Exec GetSaleUserWiseSummary " + VchCode + "," + DBGetVal.FirmNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',1";
                        dt = ObjFunction.GetDataView(str).Table;

                        double GTotal = 0, STotal = 0, RTotal = 0;
                        long BCount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i].ItemArray[3].ToString() == "1")
                                STotal = STotal + Convert.ToDouble(dt.Rows[i].ItemArray[2].ToString());
                            else
                                RTotal = RTotal + Math.Abs(Convert.ToDouble(dt.Rows[i].ItemArray[2].ToString()));
                            BCount = BCount + Convert.ToInt64(dt.Rows[i].ItemArray[1].ToString());
                        }
                        GTotal = STotal - RTotal;
                        DataRow dr = dt.NewRow();
                        dr[0] = "Grand Total";
                        dr[1] = BCount;
                        dr[2] = GTotal.ToString();
                        dr[3] = 3;
                        dr[4] = "Grand Total";
                        dt.Rows.Add(dr);

                    }
                    else if (rdMonthSummary.Checked == true)
                    {
                        if (VchCode == VchType.Sales)
                            ReportName = "Userwise Sales Monthwise Summary";
                        else if (VchCode == VchType.Purchase)
                            ReportName = "Userwise Purchase Monthwise Summary";

                        str = "Exec GetSaleUserWiseMonthlyDtls " + VchCode + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,0";
                        dt = ObjFunction.GetDataView(str).Table;
                        //dtCount = dt.Columns.Count - 2;
                    }
                    else if (rdQuarterSummary.Checked == true)
                    {
                        if (VchCode == VchType.Sales)
                            ReportName = "Userwise Sales Quarterwise Summary";
                        else if (VchCode == VchType.Purchase)
                            ReportName = "Userwise Purchase Quarterwise Summary";

                        str = "Exec GetSaleUserWiseMonthlyDtls " + VchCode + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,0";
                        dt = ObjFunction.GetDataView(str).Table;
                        //dtCount = dt.Columns.Count - 2;
                    }



                    //dt.Columns[1].ColumnName = "TotalBills";
                    //dt.Columns[2].ColumnName = "Cash";
                    //dt.Columns[3].ColumnName = "Credit";
                    //dt.Columns[4].ColumnName = "Cheque";
                    //dt.Columns[5].ColumnName = "CC";
                    //dt.Columns[6].ColumnName = "FV";
                    //dt.Columns[7].ColumnName = "Total";
                    if (rdSummary.Checked == true)
                        dtCount = dt.Columns.Count - 2;
                    else if (rdMonthSummary.Checked == true)
                        dtCount = dt.Columns.Count - 1;
                    else if (rdQuarterSummary.Checked == true)
                        dtCount = dt.Columns.Count - 1;
                    else
                        dtCount = dt.Columns.Count;
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
                    for (int i = 0; i < dtCount; i++)
                    {
                        Cnt = 0;

                        if (i == 0)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 30, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else
                        {
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                            VchTypeNo = "";
                        }
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (rdSummary.Checked == true)
                            {
                                if (i == 0)
                                {
                                    if (dt.Rows[j].ItemArray[3].ToString() != "3")
                                    {
                                        if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                        {
                                            double amt = 0;
                                            if (dt.Rows[j].ItemArray[3].ToString() == "2")
                                            {
                                                if (Convert.IsDBNull(dt.Compute("Sum(TotalAmt)", "VchTypeNo=1")) == true)
                                                    amt = 0;
                                                else
                                                    amt = Convert.ToDouble(dt.Compute("Sum(TotalAmt)", "VchTypeNo=1"));
                                                excel.createHeaders(j + col + 1 + Cnt, 1, "Total", excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                excel.createHeaders(j + col + 1 + Cnt, 3, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + Cnt, 3), excel.ColName(j + col + 1 + Cnt, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                                Cnt++;
                                            }
                                            VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                            excel.createHeaders(j + col + 1 + Cnt, 1, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                            Cnt++;
                                        }
                                    }
                                    //else
                                    //{
                                    //    double amt = 0;
                                    //    if (Convert.IsDBNull(dt.Compute("Sum(TotalAmt)", "VchTypeNo=2")) == true)
                                    //        amt = 0;
                                    //    else
                                    //        amt = Convert.ToDouble(dt.Compute("Sum(TotalAmt)", "VchTypeNo=2"));
                                    //    excel.createHeaders(j + col + 1 + Cnt, 1, "Total", excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    //    excel.createHeaders(j + col + 1 + Cnt, 3, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + Cnt, 3), excel.ColName(j + col + 1 + Cnt, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    //    Cnt++;
                                    //}
                                }
                                else
                                {
                                    if (dt.Rows[j].ItemArray[3].ToString() != "3")
                                    {
                                        if (VchTypeNo != dt.Rows[j].ItemArray[3].ToString())
                                        {
                                            if (dt.Rows[j].ItemArray[3].ToString() == "2")                                           
                                               
                                                Cnt++;
                                            
                                            VchTypeNo = dt.Rows[j].ItemArray[3].ToString();
                                            Cnt++;
                                        }
                                    }
                                }

                                if (i == 0)
                                {


                                    if (dt.Rows[j].ItemArray[3].ToString() == "3")
                                    {
                                        double amt = 0;
                                        if (Convert.IsDBNull(dt.Compute("Sum(TotalAmt)", "VchTypeNo=2")) == true)
                                            amt = 0;
                                        else
                                            amt = Convert.ToDouble(dt.Compute("Sum(TotalAmt)", "VchTypeNo=2"));
                                        excel.createHeaders(j + col + 1 + Cnt, 1, "Total", excel.ColName(j + col + 1 + Cnt, 1), excel.ColName(j + col + 1 + Cnt, 2), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 1 + Cnt, 3, Math.Abs(amt).ToString(), excel.ColName(j + col + 1 + Cnt, 3), excel.ColName(j + col + 1 + Cnt, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        Cnt++;
                                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, true);
                                    }
                                    else
                                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                }
                                else if (i == 1)
                                {
                                    if (dt.Rows[j].ItemArray[3].ToString() == "3")
                                    {
                                        Cnt++;
                                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    }
                                    else
                                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                }
                                else
                                {
                                    if (dt.Rows[j].ItemArray[3].ToString() == "3")
                                    {
                                        Cnt++;
                                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    }
                                    else if (VchTypeNo == "2")
                                        excel.addData(j + col + 1 + Cnt, i + 1, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString())).ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                    else
                                        excel.addData(j + col + 1 + Cnt, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1 + Cnt, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                }
                            }
                            else if (rdMonthSummary.Checked == true)
                            {
                                if (dt.Rows[j].ItemArray[3].ToString() != "0")
                                {
                                    if (i == 0) excel.createHeaders(j + col + 1, 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, 1), excel.ColName(j + col + 1 + Cnt, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                }
                                else
                                {
                                    bool flag = false;
                                    if (dt.Rows[j].ItemArray[0].ToString() == "Total" || dt.Rows[j].ItemArray[0].ToString() == "Grand Total")
                                        flag = true;

                                    if (i == 0)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, flag);
                                    else if (i == 1)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, flag);
                                    else
                                    {
                                        if (VchTypeNo == "2")
                                            excel.addData(j + col + 1, i + 1, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString())).ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, flag);
                                        else
                                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, flag);
                                    }
                                }
                            }
                            else
                            {
                                bool flag = false;
                                if (dt.Rows[j].ItemArray[0].ToString() == "Total" || dt.Rows[j].ItemArray[0].ToString() == "Grand Total")
                                    flag = true;

                                if (dt.Rows[j].ItemArray[3].ToString() != "0")
                                {
                                    if (i == 0)
                                        excel.createHeaders(j + col + 1, 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, 1), excel.ColName(j + col + 1, 3), 3, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                }
                                else
                                {
                                    if (i == 0)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, flag);
                                    else if (i == 1)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, flag);
                                    else
                                    {
                                        if (VchTypeNo == "2")
                                            excel.addData(j + col + 1, i + 1, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[i].ToString())).ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, flag);
                                        else
                                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1 + Cnt, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, flag);
                                    }
                                }
                            }
                        }
                    }
                    if (rdSummary.Checked == true)
                        col = col + dt.Rows.Count + Cnt;
                    else
                        col = col + dt.Rows.Count;

                    col++;
                    excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 3), 3, Color.White, true, 30, Color.Black, 12, CreateExcel.ExAlign.Left);
                    col++;
                    excel.createHeaders(col, 1, "Particulars", excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    excel.createHeaders(col, 2, "Amount", excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    col++;
                    DataTable dtPayTypeDtls = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VchCode + ",0," + DBGetVal.FirmNo + "").Table;
                    for (int i = 0; i < dtPayTypeDtls.Rows.Count; i++)
                    {
                        excel.createHeaders(col, 1, dtPayTypeDtls.Rows[i].ItemArray[1].ToString(), excel.ColName(col, 1), excel.ColName(col, 1), 1, Color.White, true, 30, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(col, 2, dtPayTypeDtls.Rows[i].ItemArray[4].ToString(), excel.ColName(col, 2), excel.ColName(col, 3), 2, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        col++;
                    }
                    excel.CompleteDoc("");
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }
            }
            else if (rdDetails.Checked == true)
            {
                try
                {
                    str = "Exec GetSaleUserWiseDateWiseSummary " + VchCode + "," + DBGetVal.FirmNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',1";
                    dt = ObjFunction.GetDataView(str).Table;
                    dt.Columns.RemoveAt(0);
                    dtCount = dt.Columns.Count;

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

                    for (int i = 0; i < dtCount; i++)
                    {
                        if (i == 0)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if (i == 1)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if (i == 2 || i == 3)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 0)
                            {
                                excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, true, 25, Color.Black, 12, CreateExcel.ExAlign.Left);
                            }
                            else if (i == 1)
                            {
                                if (Convert.ToDateTime(dt.Rows[j].ItemArray[i]).ToString("dd-MM-yyyy") == "01-01-1900")
                                    excel.createHeaders(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, true);
                            }
                            else
                            {
                                if (dt.Rows[j].ItemArray[i].ToString() == Format.NoFloating || dt.Rows[j].ItemArray[i].ToString() == Format.DoubleFloating)
                                    excel.createHeaders(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                else
                                    if (i == 2)
                                    {
                                        //excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        if (dt.Rows[j].ItemArray[i].ToString() == "Total")
                                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        else
                                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                    }
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                            }

                        }
                    }
                    excel.CompleteDoc("");
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            //else if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked==true)
            //{
            //    try
            //    {
            //        if (rdMonthSummary.Checked == true)
            //        {
            //            if (VchCode == VchType.Sales)
            //                ReportName = "Userwise Sales Monthwise Summary";
            //            else if (VchCode == VchType.Purchase)
            //                ReportName = "Userwise Purchase Monthwise Summary";

            //            str = "Exec GetSaleUserWiseMonthlyDtls " + VchCode + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3,0";
            //            dt = ObjFunction.GetDataView(str).Table;
            //            //dtCount = dt.Columns.Count - 2;
            //        }
            //        else if (rdQuarterSummary.Checked == true)
            //        {
            //            if (VchCode == VchType.Sales)
            //                ReportName = "Userwise Sales Quarterwise Summary";
            //            else if (VchCode == VchType.Purchase)
            //                ReportName = "Userwise Purchase Quarterwise Summary";

            //            str = "Exec GetSaleUserWiseMonthlyDtls " + VchCode + "," + DBGetVal.FirmNo.ToString() + ",'" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',4,0";
            //            dt = ObjFunction.GetDataView(str).Table;
            //            //dtCount = dt.Columns.Count - 2;
            //        }



            //        int col = 1;
            //        CreateExcel excel = new CreateExcel();
            //        //Company Name Header
            //        excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
            //        col++;
            //        //Company Address Header
            //        excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
            //        col++;
            //        //Report Name And Dates
            //        excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
            //        col++;
            //        excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
            //        col++;
            //        excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
            //        col++;

            //        for (int i = 0; i < dt.Columns.Count; i++)
            //        {
            //            switch (i)
            //            {
            //                case 0:
            //                    break;
            //                case 1: if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
            //                        excel.createHeaders(col, i + 1 - 1, dt.Columns[1].ColumnName, excel.ColName(col, i + 1 - 1), excel.ColName(col, i + 1 - 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
            //                    break;
            //                case 2: break;
            //                case 3: excel.createHeaders(col, i + 1 - 2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;
            //                default: if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
            //                        excel.createHeaders(col, i + 1 - 2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
            //                    break;
            //            }

            //            for (int j = 0; j < dt.Rows.Count; j++)
            //            {
            //                switch (i)
            //                {
            //                    case 0:
            //                        break;
            //                    case 1: if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
            //                            excel.createHeaders(j + col + 1, i + 1 - 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 1), excel.ColName(j + col + 1, i + 1 - 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
            //                        break;
            //                    case 2: break;
            //                    case 3: if (dt.Rows[j].ItemArray[dt.Columns.Count - 1].ToString() == "0.00") excel.createHeaders(j + col + 1, i + 1 - 2, "", excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
            //                        else excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;

            //                    case 4: if (dt.Rows[j].ItemArray[dt.Columns.Count - 1].ToString() == "0.00") excel.createHeaders(j + col + 1, i + 1 - 2, "", excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
            //                        else excel.createHeaders(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Right); break;
            //                    default: if (dt.Rows[j].ItemArray[dt.Columns.Count - 1].ToString() == "0.00") excel.addData(j + col + 1, i + 1 - 2, "", excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
            //                        else excel.addData(j + col + 1, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
            //                        break;

            //                }

            //            }
            //        }
            //        col++;
            //        excel.CompleteDoc("");
            //    }
            //    catch (Exception exc)
            //    {
            //        ObjFunction.ExceptionDisplay(exc.Message);
            //    }

            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void btnBigPrint_Click(object sender, EventArgs e)
        {
             this.Cursor = Cursors.WaitCursor;
            Form NewF = null;
            try
            {
                if (rdDetails.Checked == true || rdSummary.Checked == true)
                {
                    string[] ReportSession;
                    ReportSession = new string[3];
                   
                    //ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = VchCode.ToString();
                   // ReportSession[3] = DBGetVal.FirmNo.ToString();
                   // ReportSession[4] = "0";


                    if (rdDetails.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTUserWiseSalesRegisterDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTUserWiseSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else if (rdSummary.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTUserWiseSalesRegisterSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTUserWiseSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);

                    }
                }
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
        }
    }
}
