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
    public partial class DailySalePurchaseChart : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long VtNo;
        public string LblHeader;
        DataTable dtChart;

        public DailySalePurchaseChart(int VtNo)
        {
            InitializeComponent();
            this.VtNo = VtNo;
            if (VtNo == VchType.Sales)
            {
                this.Text = "Daily Sales Chart";
            }
            else
            {
                this.Text = "Daily Purchase Chart";
            }

        }

        private void DailySalePurchaseChart_Load(object sender, EventArgs e)
        {
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                    BtnExport.Visible = true;
                else
                    BtnExport.Visible = false;
                this.Cursor = Cursors.WaitCursor;
                dsVd = ObjDset.FillDset("New", "Exec GetDailySalesPurchaseChart " + VtNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);

                dtChart = dsVd.Tables[0];
                DataRow dr = dtChart.NewRow();
                dsVd.Tables[0].Rows.Add(dr);

                DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                for (int i = 1; i < 4; i++)
                {
                    DataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                GetCount();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession = new string[4];
                Form NewF = null;
                ReportSession[0] = LblHeader;
                ReportSession[1] = VtNo.ToString();
                ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.DailySalePurchaseChartRPT(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DailySalePurchaseChartRPT.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void GetCount()
        {
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView1.Rows[i].Index != DataGridView1.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = 0;

                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = 0;

                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value = 0;

                    //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = 100;
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[1].Value)).ToString(Format.DoubleFloating);
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value)).ToString(Format.DoubleFloating);
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[3].Value)).ToString(Format.DoubleFloating);
                }
            }

            //===========Total At footer===========

            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font =ObjFunction.GetFont();
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = "EHy$U";
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "VoucherDate")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 130;
            }

            if (e.ColumnIndex == 1)//(this.DataGridView1.Columns[e.ColumnIndex].Name == "TotalPurchase" || this.DataGridView1.Columns[e.ColumnIndex].Name == "TotalSale")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 150;
            }

            if (e.ColumnIndex == 2)// (this.DataGridView1.Columns[e.ColumnIndex].Name == "CreditPurchase" || this.DataGridView1.Columns[e.ColumnIndex].Name == "CreditSale")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 125;
            }

            if (e.ColumnIndex == 3)// (this.DataGridView1.Columns[e.ColumnIndex].Name == "CashSale" || this.DataGridView1.Columns[e.ColumnIndex].Name == "CashPurchase")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 125;
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
                BtnShow.Focus();
            } 
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); string ReportName = "";
                dt = ObjFunction.TransferData(dtChart);
                dt.Columns[0].ColumnName = "Date";
                int dtCount = dt.Columns.Count;
                if (VtNo == VchType.Sales)
                    ReportName = "Daily Sales Chart";
                else
                    ReportName = "Daily Purchase Chart";
              
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
                        else
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 0)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "dd-MMM-yyyy", 0, CreateExcel.ExAlign.Left, false);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
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
    }
}
