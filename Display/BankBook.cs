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
    public partial class BankBook : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public  long CompNo, LedgNo, MNo, Type1;
        public  string LedgName, RptTitle;
        public int voucherno;
        DataTable dtTab1 = new DataTable();
        DataTable dtTab2 = new DataTable();
        DataTable dtTab3 = new DataTable();
        bool ExcelFlag = false;
        public BankBook()
        {
            InitializeComponent();
        }

        private void BankBook_Load(object sender, EventArgs e)
        {
            
            CompNo = 1;
            LedgName = ""; RptTitle = ""; LedgNo = 0; MNo = 0; Type1 = 0;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
               
                if ((DataGridView1.CurrentRow.Cells[1].Value.ToString()) != "Total")//"EHy$U")
                {
                    TabBankBook.SelectedTab = tabPage2;
                    LedgNo = Convert.ToInt64(DataGridView1.CurrentRow.Cells[0].Value);
                    LedgName = Convert.ToString(DataGridView1.CurrentRow.Cells[1].Value);
                    lblMonthly.Font = ObjFunction.GetFont();
                    lblMonthly.Text = LedgName;
                    dsVd = ObjDset.FillDset("New", "Select MonthID,MonthName As 'Particulars',OpDrAmt AS 'Op. Amt(Dr)',OpCrAmt AS 'Op. Amt(Cr)', DebitAmt As 'Debit Amount',CreditAmt As 'Credit Amount',ClDrAmt AS 'Cl. Amt(Dr)',ClCrAmt AS 'Cl. Amt(Cr)',CrDr From GetLedgerBalanceByMonthly(" + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "')", CommonFunctions.ConStr);

                    dtTab2 = dsVd.Tables[0];
                    DataRow dr = dtTab2.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    DataGridView2.Columns[0].Visible = false;
                    for (int i = 2; i < 8; i++)
                    {
                        DataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    if (DataGridView2.Rows.Count > 0)
                    {
                        btnPrint.Visible = true;
                        btnExport.Visible = ExcelFlag;
                    }
                    else
                    {
                        btnPrint.Visible = false;
                        btnExport.Visible = false;
                    }
                    GetCountMonth();
                }
                else
                {
                  //  TabBankBook.SelectedTab = tabPage1;
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int cnt;
                cnt = DataGridView2.Rows.Count - 1;
                if (e.RowIndex != cnt)
                {
                    this.Cursor = Cursors.WaitCursor;
                    TabBankBook.SelectedTab = tabPage3;
                    MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                    lblM.Text = "(" + Convert.ToString(DataGridView2.CurrentRow.Cells[1].Value) + ")";
                    lblDatewise.Font = ObjFunction.GetFont();
                    lblDatewise.Text = LedgName;
                    //dsVd = ObjDset.FillDset("New", "Exec GetLedgerWiseVoucherDetails " + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + MNo + "", CommonFunctions.ConStr);
                    dsVd = ObjDset.FillDset("New", "Exec GetLedgerWiseVoucherDetails_E " + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + MNo + "", CommonFunctions.ConStr);

                    dtTab3 = dsVd.Tables[0];
                    DataRow dr = dtTab3.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;
                    GridViewDaily.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[2].HeaderText = "VNo";
                    GridViewDaily.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[0].Visible = false;
                    GridViewDaily.Columns[2].Width = 40;
                    GridViewDaily.Columns[1].Width = 80;
                    GridViewDaily.Columns[3].Width = 300;
                    GridViewDaily.Columns[4].Width = 150;
                    GridViewDaily.Columns[5].Width = 100;
                    GridViewDaily.Columns[6].Width = 100;

                    if (GridViewDaily.Rows.Count > 0)
                    {
                        btnPrint.Visible = true;
                        btnExport.Visible = ExcelFlag;
                    }
                    else
                    {
                        btnPrint.Visible = false;
                        btnExport.Visible = false;
                    }

                    GetCountVchDtl();
                    this.Cursor = Cursors.Default;
                }
                GridViewDaily.Columns[0].Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }        

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                TabBankBook.SelectedTab = tabPage1;

                dsVd = ObjDset.FillDset("New", "Exec GetGroupwiseLedgerBal2 " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',0,28", CommonFunctions.ConStr);
                dtTab1 = dsVd.Tables[0];
                DataRow dr = dtTab1.NewRow();
                dsVd.Tables[0].Rows.Add(dr);
                DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                DataGridView1.Columns[0].Visible = false;
                DataGridView1.Columns[1].Width = 200;
                DataGridView1.Columns[8].Width = 50;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                    ExcelFlag = true;
                else
                    ExcelFlag = false;
                for (int i = 2; i < 8; i++)
                {
                    DataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }
                if (DataGridView1.Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExport.Visible = ExcelFlag;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnExport.Visible = false;
                }
                GetCount();
                this.Cursor = Cursors.Default;
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
                    if (DataGridView1.Rows[i].Index != DataGridView1.Rows.Count - 1)
                    {
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = 100;
                        for (int j = 2; j < 8; j++)
                        {
                            if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value) != false)
                                DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value = 0;
                            if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[j].Value) != false)
                                DataGridView1.Rows[i].Cells[j].Value = 0;
                            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value =Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[j].Value)).ToString(Format.DoubleFloating);

                        }
                    }
                }
            }

            //===========Total At footer===========

            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont(); //new Font("OM-DEV-0714", 15, FontStyle.Bold);
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = "Total";//"EHy$U";
        }

        public void GetCountMonth()
        {
            for (int i = 0; i < DataGridView2.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView2.Rows[i].Index != DataGridView2.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value = 0;

                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value = 0;

                    //DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[0].Value = 100;
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value = Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[4].Value)).ToString(Format.DoubleFloating);
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[5].Value)).ToString(Format.DoubleFloating);
                }
            }
            ////===========Total At footer===========

            DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont(); //new Font("OM-DEV-0714", 15, FontStyle.Bold);
            DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[1].Value = "Total";
        }

        public void GetCountVchDtl()
        {
            for (int i = 0; i < GridViewDaily.Rows.Count - 1; i = i + 1)
            {
                if (i == 0)
                {
                    if (GridViewDaily.Rows[i].Cells[1].Value.ToString() == "01-01-1900")
                    {
                        GridViewDaily.Rows[i].Cells[1].Value = "";
                        //GridViewDaily.Rows[i].Cells[2].Value = "";
                        GridViewDaily.Rows[i].Cells[4].Value = "";
                    }
                }
                if (GridViewDaily.Rows[i].Index != GridViewDaily.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = 0;

                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = 0;
                    if (Convert.IsDBNull(GridViewDaily.Rows[i].Cells[6].Value) != false)
                    { GridViewDaily.Rows[i].Cells[4].Value = 0; }
                    if (Convert.IsDBNull(GridViewDaily.Rows[i].Cells[5].Value) != false)
                    { GridViewDaily.Rows[i].Cells[5].Value = 0; }
                    //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[0].Value = 100;
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value)).ToString(Format.DoubleFloating);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value)).ToString(Format.DoubleFloating);
                }
            }

            //===========Total At footer===========
            if (GridViewDaily.Rows.Count > 1)
            {
                GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
                GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[3].Value = "Total";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                if (TabBankBook.SelectedIndex == 0)
                {
                    ReportSession = new string[6];
                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = "0";
                    ReportSession[4] = "28";
                    ReportSession[5] = "Bank Book";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewBook(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewBook.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else if (TabBankBook.SelectedIndex == 1)
                {
                    ReportSession = new string[6];
                    ReportSession[2] = LedgNo.ToString();
                    ReportSession[2] = DBGetVal.FirmNo.ToString();

                    ReportSession[4] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[5] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[0] = "Bank Book Details";
                    ReportSession[1] = LedgName;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewBookDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewBookDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (TabBankBook.SelectedIndex == 2)
                {
                    ReportSession = new string[6];
                    ReportSession[0] = LedgNo.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();

                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = MNo.ToString();
                    ReportSession[5] = LedgName;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewLedgWiseVchDtls(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewLedgWiseVchDtls.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TabBankBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabBankBook.SelectedIndex == 0)
            {
                if (DataGridView1.Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExport.Visible = ExcelFlag;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnExport.Visible = false;
                }
            }
            else if (TabBankBook.SelectedIndex == 1)
            {
                if (DataGridView2.Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExport.Visible = ExcelFlag;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnExport.Visible = false;
                }
            }
            else if (TabBankBook.SelectedIndex == 2)
            {
                if (GridViewDaily.Rows.Count > 0)
                {
                    btnPrint.Visible = true;
                    btnExport.Visible = ExcelFlag;
                }
                else
                {
                    btnPrint.Visible = false;
                    btnExport.Visible = false;
                }
            }
            else
            {
                btnPrint.Visible = false;
                btnExport.Visible = false;
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

        private void GridViewDaily_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //voucherno = Convert.ToInt32(GridViewDaily.CurrentRow.Cells[0].Value);

            //Form newf = new VoucherViewAE(0, voucherno);
            //ObjFunction.OpenForm(newf);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (TabBankBook.SelectedIndex == 0)
                {
                    ExportBook();
                }
                else if (TabBankBook.SelectedIndex == 1)
                {
                    ExportMonthDtls();
                }
                else if (TabBankBook.SelectedIndex == 2)
                {
                    ExportDtls();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void ExportBook()
        {
            DataTable dt = new DataTable();
            //dt = ObjFunction.GetDataView("Exec GetGroupwiseLedgerBal2 " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',0,28").Table;
            dt = ObjFunction.TransferData(dtTab1);
            
            int dtCount = dt.Columns.Count;
            try
            {
                int col = 1; //double total = 0;
                CreateExcel excel = new CreateExcel();
                //Company Name Header
                excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount,Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                col++;
                //Company Address Header
                excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                col++;
                //Report Name And Dates
                excel.createHeaders(col, 1, "Bank Book", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount),Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        excel.createHeaders(col, i + 1, dt.Columns[i + 1].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1 + 1), 2, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 8)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else if (i != 1)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (j != dt.Rows.Count - 1)
                        {
                            if (i == 0)
                                excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i + 1].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1 + 1), 2, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            //excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i+1].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, "", false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else if (i == 8)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                            else if (i != 1)
                            {
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1),Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                //total = total + Convert.ToDouble(dt.Rows[j].ItemArray[i]);
                            }
                        }
                        else
                        {
                            if (i == 0)
                                excel.createHeaders(j + col + 1, i + 1, "Total", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1 + 1), 2, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else if (i != 1)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                        }
                    }

                    //if (i == 0)
                    //    excel.createHeaders(dt.Rows.Count + col + 1, i + 1, "Total", excel.ColName(dt.Rows.Count + col + 1, i + 1), excel.ColName(dt.Rows.Count + col + 1, i + 1 + 1), 2, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //else if (i != 1)
                    //{
                    //    excel.createHeaders(dt.Rows.Count + col + 1, i + 1, total.ToString(), excel.ColName(dt.Rows.Count + col + 1, i + 1), excel.ColName(dt.Rows.Count + col + 1, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //    total = 0;
                    //}

                }
                col++;
                excel.CompleteDoc("");
            }
            catch (Exception ex)
            {
                CommonFunctions.ErrorMessge = ex.Message;
            }
        }

        public void ExportMonthDtls()
        {
            DataTable dt = new DataTable();
            LedgNo = Convert.ToInt64(DataGridView1.CurrentRow.Cells[0].Value);
            //dt = ObjFunction.GetDataView("Select MonthName As 'Month',OpDrAmt AS 'Op. Amt(Dr)',OpCrAmt AS 'Op. Amt(Cr)', DebitAmt As 'Debit Amount',CreditAmt As 'Credit Amount',ClDrAmt AS 'Cl. Amt(Dr)',ClCrAmt AS 'Cl. Amt(Cr)',CrDr From GetLedgerBalanceByMonthly(" + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "')").Table;
            dt = ObjFunction.TransferData(dtTab2);
            dt.Columns.RemoveAt(0);
            int dtCount = dt.Columns.Count;
            try
            {
                int col = 1; double total = 0;
                CreateExcel excel = new CreateExcel();
                //Company Name Header
                excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                col++;
                //Company Address Header
                excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                col++;
                //Report Name And Dates
                excel.createHeaders(col, 1, "Bank Book Details", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 8)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                    for (int j = 0; j < dt.Rows.Count-1; j++)
                    {
                        if (i == 0)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 7)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                        else
                        {
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            total = total + Convert.ToDouble(dt.Rows[j].ItemArray[i]);
                        }
                    }
                    if (i == 0)
                        excel.createHeaders(dt.Rows.Count + col, i + 1, "Total", excel.ColName(dt.Rows.Count + col, i + 1), excel.ColName(dt.Rows.Count + col, i + 1), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i != 7)
                    {
                        excel.addData(dt.Rows.Count + col, i + 1, total.ToString(), excel.ColName(dt.Rows.Count + col, i + 1), excel.ColName(dt.Rows.Count + col, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                        total = 0;
                    }

                }
                col++;
                excel.CompleteDoc("");
            }
            catch (Exception ex)
            {
                CommonFunctions.ErrorMessge = ex.Message;
            }

        }

        public void ExportDtls()
        {
            DataTable dt = new DataTable();
            MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
            //dt = ObjFunction.GetDataView("Exec GetLedgerWiseVoucherDetails_E " + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + MNo + "").Table;
            dt = ObjFunction.TransferData(dtTab3);
            int dtCount = dt.Columns.Count;
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
                excel.createHeaders(col, 1, "Voucher Details", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        excel.createHeaders(col, i + 1, "SrNo", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else if (i == 1)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 2 || i == 5 || i == 6)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                    else if (i == 3 || i == 4)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (i == 0)
                            excel.addData(j + col + 1, i + 1, (j + 1).ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                        else if (i == 1)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 2)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                        else if(i == 5 || i == 6)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        else if (i == 3 || i == 4)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                    }

                }
                col++;
                excel.CompleteDoc("");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnCancelt2_Click(object sender, EventArgs e)
        {
            TabBankBook.SelectedTab = tabPage2;
        }

        private void btnCancelt1_Click(object sender, EventArgs e)
        {
            TabBankBook.SelectedTab = tabPage1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DTToDate.MinDate = DTPFromDate.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}
