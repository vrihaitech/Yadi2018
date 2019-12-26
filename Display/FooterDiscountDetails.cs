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
    public partial class FooterDiscountDetails : Form
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

        public FooterDiscountDetails()
        {
            InitializeComponent();
        }

        private void FooterDiscountDetails_Load(object sender, EventArgs e)
        {
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Form NewF = null;
                string[] ReportSession;
                ReportSession = new string[4];
                ReportSession[0] = DTPFromDate.Text;
                ReportSession[1] = DTToDate.Text;
                ReportSession[2] = DBGetVal.FirmNo.ToString();
                if (rdDetails.Checked == true)
                    ReportSession[3] = "1";
                else
                    ReportSession[3] = "2";

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                {
                    if (rdDetails.Checked == true)
                        NewF = new Display.ReportViewSource(new Reports.RptFooterDiscountDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(new Reports.RptFooterDiscountDateWiseDetails(), ReportSession);
                }
                else
                {
                    if (rdDetails.Checked == true)
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptFooterDiscountDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptFooterDiscountDateWiseDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int dtCount = 0;
                DataTable dt=new DataTable();
                if (rdDetails.Checked == true)
                    dt = ObjFunction.GetDataView("Exec GetFooterDiscountDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + DBGetVal.FirmNo + ",1").Table;
                else
                    dt = ObjFunction.GetDataView("Exec GetFooterDiscountDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + DBGetVal.FirmNo + ",2").Table;
                DataRow dr=dt.NewRow();
                dr[1] = "Total";
                dr[3] = dt.Compute("Sum(Amount)", "");
                dr[6] = dt.Compute("Sum(DiscAmount)", "");
                dt.Rows.Add(dr);
                
                dt.Columns.RemoveAt(0);
                if (rdDateWise.Checked == true)
                {
                    dt.Columns.RemoveAt(3);
                    dt.Columns[0].SetOrdinal(1);
                }
                dtCount = dt.Columns.Count;
                //if (str != "")
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
                    if (rdDetails.Checked == true)
                        excel.createHeaders(col, 1, "Footer Discount Details", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else
                        excel.createHeaders(col, 1, "Footer Discount DateWise Details", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;
                    excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i == 0 && rdDetails.Checked == true)
                            excel.createHeaders(col, i + 1, "Bill No.", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else if (i == 0 && rdDateWise.Checked == true)
                            excel.createHeaders(col, i + 1, "Date", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if (i == 1 && rdDetails.Checked == true)
                            excel.createHeaders(col, i + 1, "Date", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if (i == 1 && rdDateWise.Checked == true)
                            excel.createHeaders(col, i + 1, "Total Bills", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows.Count > 1)
                            {
                                if (i == 0 && rdDetails.Checked == true)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                                else if (i == 0 && rdDateWise.Checked == true)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                else if (i == 1 && rdDetails.Checked == true)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                else if (i == 1 && rdDateWise.Checked == true)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                                else if (dt.Rows.Count - 1 == j)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, true);
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            }
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, true);
                        }
                    }
                    col++;
                    excel.CompleteDoc("");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

    }
}
