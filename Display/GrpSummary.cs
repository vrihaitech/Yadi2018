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
    public partial class GrpSummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DBProgressBar PB;
        public long CompNo, LedgNo, MNo, Type1;
        public string LedgName, RptTitle;
        public int voucherno;
        DataTable dtTab1 = new DataTable();
        DataTable dtTab2 = new DataTable();
        DataTable dtTab3 = new DataTable();
        bool ExcelFlag = false;
        public GrpSummary()
        {
            InitializeComponent();
        }

        private void GrpSummary_Load(object sender, EventArgs e)
        {
            try
            {
                rbGrpwise.Checked = true;

                CompNo = DBGetVal.FirmNo;
                cmbGroupName.DisplayMember = "GroupName";
                cmbGroupName.ValueMember = "GroupNo";
                cmbGroupName.DataSource = ObjDset.FillDset("New", "SELECT GroupNo, GroupName FROM MGroup", CommonFunctions.ConStr).Tables[0];
                rbGrpwise_CheckedChanged(sender, e);
                DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                DTToDate.MinDate = DTPFromDate.Value;
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
                tabGrpSumm.SelectedTab = tabPage1;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                    ExcelFlag = true;
                else
                    ExcelFlag = false;
                if (rbGrpwise.Checked == true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    dsVd = ObjDset.FillDset("New", "Exec GetGroupwiseLedgerBal " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',0," + cmbGroupName.SelectedValue + "", CommonFunctions.ConStr);

                    dtTab1 = dsVd.Tables[0];
                    DataRow dr = dtTab1.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    DataGridView1.Columns[0].Visible = false;
                    DataGridView1.Columns[1].Width = 200;
                    DataGridView1.Columns[8].Width = 50;
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
                else if (rbAllGroup.Checked == true)
                {


                    btnPrint.Visible = false;
                    btnExport.Visible = false;
                    tabGrpSumm.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = tabGrpSumm;
                    tabGrpSumm.SelectedTab = tabPage1;


                    dsVd = ObjDset.FillDset("New", "Exec GetGroupwiseLedgerBal " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',1," + 0 + "", CommonFunctions.ConStr);

                    dtTab1 = dsVd.Tables[0];
                    DataRow dr = dtTab1.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    DataGridView1.Columns[0].Visible = false;
                    DataGridView1.Columns[1].Width = 200;
                    DataGridView1.Columns[8].Width = 50;
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
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rbGrpwise_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGrpwise.Checked == true)
            {
                panel1.Visible = true;
                cmbGroupName.Focus();
            }
            else
                panel1.Visible = false;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
               
                if ((DataGridView1.CurrentRow.Cells[1].Value.ToString()) != "Total")//"EHy$U")
                {
                    tabGrpSumm.SelectedTab = tabPage2;
                    LedgNo = Convert.ToInt64(DataGridView1.CurrentRow.Cells[0].Value);
                    LedgName = Convert.ToString(DataGridView1.CurrentRow.Cells[1].Value);
                    lblMonthDtls.Font = ObjFunction.GetFont();//new Font("OM-DEV-0714", 16F, System.Drawing.FontStyle.Bold);
                    lblMonthDtls.Text = LedgName;
                    dsVd = ObjDset.FillDset("New", "Select MonthID,MonthName As 'Particulars',OpDrAmt AS 'Op. Amt(Dr)',OpCrAmt AS 'Op. Amt(Cr)', DebitAmt As 'Debit Amount',CreditAmt As 'Credit Amount',ClDrAmt AS 'Cl. Amt(Dr)',ClCrAmt AS 'Cl. Amt(Cr)',CrDr From GetLedgerBalanceByMonthly (" + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "')", CommonFunctions.ConStr);

                    dtTab2 = dsVd.Tables[0];
                    DataRow dr = dtTab2.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    for (int i = 2; i < 8; i++)
                    {
                        DataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    DataGridView2.Columns[0].Visible = false;
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
                else
                {
                    //tabGrpSumm.SelectedTab = tabPage1;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int cnt;
                cnt = DataGridView2.Rows.Count - 1;
                if (DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[1].Selected == false)
                {
                    if (e.RowIndex != cnt)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        tabGrpSumm.SelectedTab = tabPage3;
                        MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                        label6.Text = "(" + Convert.ToString(DataGridView2.CurrentRow.Cells[1].Value) + ")";
                        lblSummDtls.Font = ObjFunction.GetFont();
                        lblSummDtls.Text = LedgName;
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
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void GridViewDaily_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "CrDr")
                e.CellStyle.Font = ObjFunction.GetFont();
        }

        private void DataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i <= DataGridView1.Rows.Count - 1; i++)
            {
                if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[0].Value) != true)
                {
                    if (Convert.ToInt64(DataGridView1.Rows[i].Cells[0].Value) == 0)
                    {
                        DataGridView1.Rows[i].DefaultCellStyle.Font = ObjFunction.GetFont();
                        DataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Pink;
                    }
                    else if (Convert.ToInt64(DataGridView1.Rows[i].Cells[0].Value) < 0)
                    {
                        DataGridView1.Rows[i].DefaultCellStyle.Font = ObjFunction.GetFont();
                        DataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }
                }
            }
        }

        public void GetCount()
        {
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView1.Rows[i].Index != DataGridView1.Rows.Count - 1)
                {
                    for (int j = 2; j < 8; j++)
                    {
                        if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value) != false)
                            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value = 0;
                        if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[j].Value) != false)
                            DataGridView1.Rows[i].Cells[j].Value = 0;
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value =Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[j].Value)).ToString(Format.DoubleFloating);

                    }
                    //if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) != false)
                    //    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = 0;

                    //if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value) != false)
                    //    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value = 0;

                    //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = 100;
                    //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value);
                    //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[3].Value);
                }
            }

            //===========Total At footer===========

            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = "Total";
        }

        public void GetCountMonth()
        {
            for (int i = 0; i < DataGridView2.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView2.Rows[i].Index != DataGridView2.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value = 0;

                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value = 0;
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value =Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[2].Value)).ToString(Format.DoubleFloating);
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value =Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[3].Value)).ToString(Format.DoubleFloating);
                }
            }
            //===========Total At footer===========

            DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
            DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[1].Value = "Total";
        }

        public void GetCountVchDtl()
        {
            for (int i = 0; i < GridViewDaily.Rows.Count - 1; i = i + 1)
            {
                if (GridViewDaily.Rows[i].Index != GridViewDaily.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = 0;

                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = 0;
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value =Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value)).ToString(Format.DoubleFloating);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value =Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value)).ToString(Format.DoubleFloating);
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

        private void tabLedgerBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabGrpSumm.SelectedIndex == 0)
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
            else if (tabGrpSumm.SelectedIndex == 1)
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
            else if (tabGrpSumm.SelectedIndex == 2)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                if (tabGrpSumm.SelectedIndex == 0)
                {
                    ReportSession = new string[5];
                    if (rbGrpwise.Checked == true)
                    {

                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = "0";
                        ReportSession[4] = cmbGroupName.SelectedValue.ToString();
                    }
                    else if (rbAllGroup.Checked == true)
                    {

                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = "1";
                        ReportSession[4] = "0";
                    }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewGroupSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGroupSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (tabGrpSumm.SelectedIndex == 1)
                {
                    ReportSession = new string[6];
                    ReportSession[0] = LedgNo.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = "Group Summary Details";
                    ReportSession[5] = LedgName;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewGrpSummDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGrpSummDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (tabGrpSumm.SelectedIndex == 2)
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
                if (panel1.Visible == true)
                    cmbGroupName.Focus();
                else
                    BtnShow.Focus();
            }
        }

        private void cmbGroupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbGroupName, e, true);
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                BtnShow.Focus();
            }
        }

        private void GridViewDaily_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            voucherno = Convert.ToInt32(GridViewDaily.CurrentRow.Cells[0].Value);

            Form newf = new VoucherViewAE(0, voucherno);
            ObjFunction.OpenForm(newf);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabGrpSumm.SelectedIndex == 0)
                {
                    ExportBook();
                }
                else if (tabGrpSumm.SelectedIndex == 1)
                {
                    ExportMonthDtls();
                }
                else if (tabGrpSumm.SelectedIndex == 2)
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

            //if (rbGrpwise.Checked == true)
            //{
            //    dt = ObjFunction.GetDataView("Exec GetGroupwiseLedgerBal " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',0," + cmbGroupName.SelectedValue + "").Table;
            //}
            //else if (rbAllGroup.Checked == true)
            //{
            //    dt = ObjFunction.GetDataView("Exec GetGroupwiseLedgerBal " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "',1," + 0 + "").Table;
            //}
            dt = ObjFunction.TransferData(dtTab1);
            //DataRow dr = dt.NewRow();
            int dtCount = dt.Columns.Count; double total = 0;//int temp = 0; 
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
                excel.createHeaders(col, 1, "Group Summary", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        excel.createHeaders(col, i + 1, dt.Columns[i + 1].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1 + 1), 2, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    }
                    else if (i == 8)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else if (i != 1)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 25, Color.Black, 12, CreateExcel.ExAlign.Right);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (j != dt.Rows.Count - 1)
                        {
                            if (i == 0)
                            {
                                if (rbAllGroup.Checked == true)
                                {
                                    if (dt.Rows[j].ItemArray[0].ToString() == "0")
                                    {
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i + 1].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1 + 1), 2, Color.Turquoise, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    }
                                    else if (Convert.ToInt64(dt.Rows[j].ItemArray[0].ToString()) < 0)
                                    {
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i + 1].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1 + 1), 2, Color.Yellow, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    }
                                    else
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i + 1].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1 + 1), 2, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                }
                                else
                                {
                                    excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i + 1].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1 + 1), 2, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                }
                            }

                            //excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i+1].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, "", false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else if (i == 8)
                            {
                                if (rbAllGroup.Checked == true)
                                {
                                    if (dt.Rows[j].ItemArray[0].ToString() == "0")
                                    {
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.Turquoise, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                                    }
                                    else if (Convert.ToInt64(dt.Rows[j].ItemArray[0].ToString()) < 0)
                                    {
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.Yellow, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                                    }
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                }
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);

                            }
                            else if (i != 1)
                            {
                                if (rbAllGroup.Checked == true)
                                {
                                    if (dt.Rows[j].ItemArray[0].ToString() == "0")
                                    {
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1,Color.Turquoise, false, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    }
                                    else if (Convert.ToInt64(dt.Rows[j].ItemArray[0].ToString()) < 0)
                                    {
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.Yellow, false, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    }
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                                }
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                total = total + Convert.ToDouble(dt.Rows[j].ItemArray[i]);
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
                excel.createHeaders(col, 1, "Ledger Book Details", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 7)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                    for (int j = 0; j < dt.Rows.Count - 1; j++)
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
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
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
                        else if (i == 2 )
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                        else if(i == 5 || i == 6)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        else if (i == 3 || i == 4)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                    }

                }
                col++;
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
