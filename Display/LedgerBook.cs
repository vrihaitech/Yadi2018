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
    public partial class LedgerBook : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DBProgressBar PB;
        public long CompNo, LedgNo, MNo, Type1, voucherno;
        public string LedgName, RptTitle;
        long vouchertypecode;
        string Str = "";
        int SelectedCount = 0;
        DataTable dtTab1 = new DataTable();
        DataTable dtTab2 = new DataTable();
        DataTable dtTab3 = new DataTable();
        bool ExcelFlag = false;

        private class ReportType
        {
            public const int Summary = 1;
            public const int Monthwise = 2;
            public const int Detailed = 3;
            public const int Billwise = 4;
            public const int MultiBillPrint = 5;
        }

        public LedgerBook()
        {
            InitializeComponent();
        }

        private void LedgerBook_Load(object sender, EventArgs e)
        {
            try
            {
                CompNo = DBGetVal.FirmNo;
                label7.Text = "";
                plnLedger.Visible = true;
                tabLedgerBook.Visible = false;
                //tabLedgerBook.Visible = true;
                KeyDownFormat(this.Controls);
                rbMultibillPrint.Visible = false;
                rbSummary.Visible = false;
                rbMonthwise.Visible = false;
                rbDetailed.Visible = false;
                DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                DTToDate.MinDate = DTPFromDate.Value;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void BindGridLedger()
        {
            long Optypecode=0 ;
            try
            {
                while (dgLedger.Rows.Count > 0)
                {
                    dgLedger.Rows.RemoveAt(0);
                }
                chkSelectAll.Checked = false;
                DataTable dt = new DataTable();
                plnLedger.Visible = true;
                tabLedgerBook.Visible = false;
                string sql = "";
                if (rbCustomer.Checked == true)
                {
                    if (DBGetVal.KachhaFirm == false)
                    {

                        vouchertypecode = 15;
                        Optypecode=36;
                    }
                    else
                    {
                        vouchertypecode = 115;
                        Optypecode = 136;
                    }
                    //(TVoucherEntry.VoucherDate >='" + DTPFromDate.Text + "') AND
                    //sql = "SELECT distinct 0 AS SrNo, MLedger.LedgerNo, MLedger.LedgerName, 'False' AS IsActive " +
                    //          " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                    //          " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                    //          " WHERE  (TVoucherEntry.VoucherDate <= '" + DTToDate.Text + "') and (MLedger.GroupNo=" + GroupType.SundryDebtors + ") and (TVoucherEntry.IsCancel='false')" +
                    //          " and (TVoucherEntry.vouchertypecode=" + vouchertypecode + " or TVoucherEntry.vouchertypecode=" + Optypecode + " ) ORDER BY MLedger.LedgerName";
                    sql = "SELECT distinct 0 AS SrNo, MLedger.LedgerNo, MLedger.LedgerName, 'False' AS IsActive " +
                              " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                              " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                              " WHERE   (MLedger.GroupNo=" + GroupType.SundryDebtors + ")  ORDER BY MLedger.LedgerName";

                }

                if (rbSupplier.Checked == true)
                {
                    if (DBGetVal.KachhaFirm == false)
                    {

                        vouchertypecode = 9;
                        Optypecode = 36;
                    }
                    else
                    {
                        vouchertypecode = 109;
                        Optypecode = 136;
                    }
                    //(TVoucherEntry.VoucherDate >='" + DTPFromDate.Text + "') AND
                    sql = "SELECT distinct 0 AS SrNo, MLedger.LedgerNo, MLedger.LedgerName, 'False' AS IsActive " +
                              " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                              " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                              " WHERE  (TVoucherEntry.VoucherDate <= '" + DTToDate.Text + "') and (MLedger.GroupNo=" + GroupType.SundryCreditors + ") and (TVoucherEntry.IsCancel='false')" +
                              " and (TVoucherEntry.vouchertypecode=" + vouchertypecode + " or TVoucherEntry.vouchertypecode=" + Optypecode + " ) ORDER BY MLedger.LedgerName";
                }
                if (rbOther.Checked == true)
                {//(TVoucherEntry.VoucherDate >='" + DTPFromDate.Text + "') AND
                    sql = "SELECT distinct 0 AS SrNo, MLedger.LedgerNo, MLedger.LedgerName, 'False' AS IsActive " +
                              " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                              " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                              " WHERE  (TVoucherEntry.VoucherDate <= '" + DTToDate.Text + "') and (MLedger.GroupNo not in(" + GroupType.SundryDebtors + "," + GroupType.SundryCreditors + ")) and (TVoucherEntry.IsCancel='false')" +
                              " ORDER BY MLedger.LedgerName";
                }
                //--umesh 16-11-2018
                //if (RdMultibillPrint.Checked == true)//--umesh 16-11-2018
                //{
                //    sql = "SELECT distinct 0 AS SrNo, MLedger.LedgerNo, MLedger.LedgerName, 'False' AS IsActive " +
                //                 " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                //                 " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                //                 " WHERE (TVoucherEntry.VoucherDate >='" + DTPFromDate.Text + "') AND (TVoucherEntry.VoucherDate <= '" + DTToDate.Text + "') and (MLedger.GroupNo=" + GroupType.SundryDebtors + ") and (TVoucherEntry.IsCancel='false')" +
                //                 " ORDER BY MLedger.LedgerName";
                //}



                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == true)
                {
                    sql = sql.Replace(", MLedger.LedgerName", ", MLedger.LedgerName +'-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName");
                    sql = sql.Replace("TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo", "TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo=MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo  ");
                    sql = sql.Replace("ORDER BY MLedger.LedgerName", "ORDER BY MLedger.LedgerName +'-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') ");
                }
                dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgLedger.Rows.Add();
                    for (int j = 0; j < dgLedger.ColumnCount; j++)
                    {
                        dgLedger.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                        dgLedger.Rows[i].Cells[0].Value = i + 1;
                    }
                }
                if (dgLedger.Rows.Count > 0)
                {
                    dgLedger.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgLedger.Focus();
                    dgLedger.CurrentCell = dgLedger[3, 0];
                }
                new GridSearch(dgLedger, 2);


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {

            chkSelectAll.Checked = false;
            this.Cursor = Cursors.WaitCursor;
            tabLedgerBook.SelectedTab = tabPage1;
            BindGridLedger();
            plnLedger.Visible = true;
            this.Cursor = Cursors.Default;
            rbBillwise.Checked = true;

        }

        public void GetCount()
        {
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i = i + 1)
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
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value = Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[j].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[j].Value)).ToString(Format.DoubleFloating);

                    }
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
                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value = 0;

                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value = 0;

                    //DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[0].Value = 100;
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value = Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[4].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[4].Value)).ToString(Format.DoubleFloating);
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[5].Value)).ToString(Format.DoubleFloating);
                }
            }
            //===========Total At footer===========

            DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new Font("OM-DEV-0714", 15, FontStyle.Bold);
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
                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = 0;

                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = 0;
                    if (Convert.IsDBNull(GridViewDaily.Rows[i].Cells[5].Value) != false)
                    { GridViewDaily.Rows[i].Cells[5].Value = 0; }
                    if (Convert.IsDBNull(GridViewDaily.Rows[i].Cells[6].Value) != false)
                    { GridViewDaily.Rows[i].Cells[6].Value = 0; }
                    //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[0].Value = 100;
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value)).ToString(Format.DoubleFloating);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value)).ToString(Format.DoubleFloating);
                }
            }

            //===========Total At footer===========

            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();//new Font("OM-DEV-0714", 15, FontStyle.Bold);
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[3].Value = "Total";//"EHy$U";
            lblTotal.Visible = true;
            lblTotal.Text = "Balance :  " + (Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) - Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value)).ToString("0.00");
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                if ((DataGridView1.CurrentRow.Cells[1].Value.ToString()) != "Total") //"EHy$U"
                {
                    tabLedgerBook.SelectedTab = tabPage2;
                    LedgNo = Convert.ToInt64(DataGridView1.CurrentRow.Cells[0].Value);
                    LedgName = Convert.ToString(DataGridView1.CurrentRow.Cells[1].Value);
                    //label7.Font = new Font("OM-DEV-0714", 14F, System.Drawing.FontStyle.Bold);
                    // label7.Text = LedgName;
                    dsVd = ObjDset.FillDset("New", "Select MonthID,MonthName As 'Particulars',OpDrAmt AS 'Op. Amt(Dr)',OpCrAmt AS 'Op. Amt(Cr)', DebitAmt As 'Debit Amount',CreditAmt As 'Credit Amount',ClDrAmt AS 'Cl. Amt(Dr)',ClCrAmt AS 'Cl. Amt(Cr)',CrDr From GetLedgerBalanceByMonthly(" + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "')", CommonFunctions.ConStr);
                    dtTab2 = dsVd.Tables[0];
                    DataRow dr = dtTab2.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    for (int i = 2; i < 8; i++)
                    {
                        DataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    new GridSearch(DataGridView2, 1);
                    DataGridView2.Columns[0].Visible = false;
                    //DataGridView2.Columns[5].Width = 50;
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
                    DataGridView2.Rows[0].DefaultCellStyle.BackColor = System.Drawing.Color.Bisque;
                    DataGridView2.Rows[DataGridView2.Rows.Count - 2].DefaultCellStyle.BackColor = System.Drawing.Color.Bisque;
                    //DataGridView2.CurrentCell = DataGridView2[1, 1];
                }
                else
                {
                    // tabLedgerBook.SelectedTab = tabPage1;
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
                if (e.RowIndex != -1 && e.RowIndex != cnt)
                {
                    this.Cursor = Cursors.WaitCursor;
                    tabLedgerBook.SelectedTab = tabPage3;
                    MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                    label7.Text = "(" + Convert.ToString(DataGridView2.CurrentRow.Cells[1].Value) + ")";
                    lblDatewise.Font = ObjFunction.GetFont();//new Font("OM-DEV-0714", 14F, System.Drawing.FontStyle.Bold);
                    lblDatewise.Text = LedgName;
                    //string str = "Exec GetLedgerWiseVoucherDetails_E " + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + MNo + "";
                    dsVd = ObjDset.FillDset("New", "Exec GetLedgerWiseVoucherDetails_E " + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + MNo + "", CommonFunctions.ConStr);

                    dtTab3 = dsVd.Tables[0];
                    DataRow dr = dtTab3.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;
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
                    this.Cursor = Cursors.Default;
                    GridViewDaily.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[2].HeaderText = "VNo";
                    GridViewDaily.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[0].Visible = false;
                    GridViewDaily.Columns[2].Width = 80;
                    GridViewDaily.Columns[1].Width = 80;
                    GridViewDaily.Columns[3].Width = 280;
                    GridViewDaily.Columns[4].Width = 150;
                    GridViewDaily.Columns[5].Width = 100;
                    GridViewDaily.Columns[6].Width = 100;
                    GetCountVchDtl();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {



            //if (this.DataGridView2.Columns[e.ColumnIndex].Name == "Particulars")
            //{
            //    e.CellStyle.Font = new Font("Verdana", 10);
            //}

        }

        private void GridViewDaily_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "Voucher Type")
            //{
            //    e.CellStyle.Font = new Font("Verdana", 10);
            //}
            //if (e.ColumnIndex == 0)
            //{
            //    GridViewDaily.Columns[0].Visible = false;

            //}
        }

        private void tabLedgerBook_TabChanged(object sender, EventArgs e)
        {
            if (tabLedgerBook.SelectedIndex == 0)
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
            else if (tabLedgerBook.SelectedIndex == 1)
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
            else if (tabLedgerBook.SelectedIndex == 2)
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
                //string[] ReportSession;
                //Form NewF = null;
                if (tabLedgerBook.SelectedIndex == 0)
                {
                    //string str = GetStrLedger();
                    //ReportSession = new string[4];
                    //ReportSession[0] = DBGetVal.FirmNo.ToString();
                    //ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    //ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    //ReportSession[3] = str;
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //    NewF = new Display.ReportViewSource(new Reports.ViewLedgerBook(), ReportSession);
                    //else
                    //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewLedgerBook.rpt", CommonFunctions.ReportPath), ReportSession);
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    printReport(1);
                }
                else if (tabLedgerBook.SelectedIndex == 1)
                {
                    //ReportSession = new string[6];
                    //ReportSession[0] = "Ledger Book Details";
                    //ReportSession[1] = LedgName;
                    //ReportSession[2] = LedgNo.ToString();
                    //ReportSession[3] = DBGetVal.FirmNo.ToString();
                    //ReportSession[4] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    //ReportSession[5] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //    NewF = new Display.ReportViewSource(new Reports.ViewLedgerDetails(), ReportSession);
                    //else
                    //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewLedgerDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    printReport(2);

                }
                else if (tabLedgerBook.SelectedIndex == 2)
                {
                    //ReportSession = new string[6];
                    //ReportSession[0] = LedgNo.ToString();
                    //ReportSession[1] = DBGetVal.FirmNo.ToString();

                    //ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    //ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    //ReportSession[4] = MNo.ToString();
                    //ReportSession[5] = LedgName;
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //    NewF = new Display.ReportViewSource(new Reports.ViewLedgWiseVchDtls(), ReportSession);
                    //else
                    //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewLedgWiseVchDtls.rpt", CommonFunctions.ReportPath), ReportSession);
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    printReport(3);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void printReport(int rType)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                string str = "";
                switch (rType)
                {
                    case ReportType.Summary:
                        str = GetStrLedger();
                        ReportSession = new string[4];
                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = str;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewLedgerBook(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewLedgerBook.rpt", CommonFunctions.ReportPath), ReportSession);

                        break;
                    case ReportType.Monthwise:
                        ReportSession = new string[6];
                        ReportSession[0] = "Ledger Book Details";
                        ReportSession[1] = LedgName;
                        ReportSession[2] = LedgNo.ToString();
                        ReportSession[3] = DBGetVal.FirmNo.ToString();
                        ReportSession[4] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[5] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewLedgerDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewLedgerDetails.rpt", CommonFunctions.ReportPath), ReportSession);

                        break;
                    case ReportType.Detailed:
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

                        break;
                    case ReportType.Billwise:
                        str = GetStrLedger();
                        ReportSession = new string[5];

                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        if (DBGetVal.KachhaFirm == false)//--umesh 16-11-2018
                        {
                            ReportSession[2] = (rbCustomer.Checked == true) ? VchType.Sales.ToString() : VchType.Purchase.ToString();
                        }
                        else
                        {
                            ReportSession[2] = (rbCustomer.Checked == true) ? VchType.DSales.ToString() : VchType.DPurchase.ToString();
                        }

                        ReportSession[3] = DBGetVal.FirmNo.ToString();
                        ReportSession[4] = str;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptLederBookBillwiseNew(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptLederBookBillwiseNew.rpt", CommonFunctions.ReportPath), ReportSession);
                        break;

                    case ReportType.MultiBillPrint:
                        str = GetStrLedger();
                        ReportSession = new string[5];

                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        // ReportSession[2] = (RdMultibillPrint.Checked == true) ? VchType.Sales.ToString() : VchType.Purchase.ToString();
                        if (DBGetVal.KachhaFirm == false)//--umesh 16-11-2018
                        {
                            ReportSession[2] = (rbCustomer.Checked == true) ? VchType.Sales.ToString() : VchType.Purchase.ToString();
                        }
                        else
                        {
                            ReportSession[2] = (rbCustomer.Checked == true) ? VchType.DSales.ToString() : VchType.DPurchase.ToString();
                        }
                        ReportSession[3] = DBGetVal.FirmNo.ToString();
                        ReportSession[4] = str;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.GetMultiBill(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetMultiBill.rpt", CommonFunctions.ReportPath), ReportSession);
                        break;

                }

                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
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

        private void GridViewDaily_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (Convert.IsDBNull(GridViewDaily.CurrentRow.Cells[0].Value) == false)
            //{
            //    voucherno = Convert.ToInt32(GridViewDaily.CurrentRow.Cells[0].Value);

            //    Form newf = new VoucherViewAE(0, voucherno);
            //    ObjFunction.OpenForm(newf);
            //}
        }

        private void rbAllLedger_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GridViewDaily_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Convert.IsDBNull(GridViewDaily.CurrentRow.Cells[0].Value) == false)
                {
                    voucherno = Convert.ToInt32(GridViewDaily.CurrentRow.Cells[0].Value);

                    Form newf = new VoucherViewAE(0, voucherno);
                    ObjFunction.OpenForm(newf);
                }
            }
            if (e.KeyCode == Keys.P && e.Control)
            {
                if (Convert.IsDBNull(GridViewDaily.CurrentRow.Cells[4].Value) == false)
                {
                    if (GridViewDaily.CurrentRow.Cells[4].Value.ToString() == "Sales Receipt")
                        PrintBill(GridViewDaily.CurrentRow.Cells[0].Value.ToString(), VchType.Sales);
                    else if (GridViewDaily.CurrentRow.Cells[4].Value.ToString() == "Purchase Payment")
                        PrintBill(GridViewDaily.CurrentRow.Cells[0].Value.ToString(), VchType.Purchase);
                }
            }
        }

        public void PrintBill(string PkVoucherNo, long VoucherType)
        {
            string[] ReportSession;
            DataTable dtPrint = ObjFunction.GetDataView("SELECT VoucherDate, BilledAmount, Remark,IsNull((Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=501 And FkVoucherNo=PkVoucherNo),0) as PartyAmount ,IsNull( (Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=502 And FkVoucherNo=PkVoucherNo),0) as DiscAmt  FROM TVoucherEntry Where PkVoucherNo In(" + PkVoucherNo + ") ").Table;
            for (int i = 0; i < dtPrint.Rows.Count; i++)
            {

                ReportSession = new string[6];
                ReportSession[0] = ((VoucherType == 15) ? "Receipt Voucher" : "Payment Voucher");
                ReportSession[1] = dtPrint.Rows[i].ItemArray[0].ToString();
                ReportSession[2] = (Convert.ToDouble(dtPrint.Rows[i].ItemArray[3].ToString()) - Convert.ToDouble(dtPrint.Rows[i].ItemArray[4].ToString())).ToString();
                ReportSession[3] = NumberToWordsIndian.getWords(ReportSession[2].ToString());
                ReportSession[4] = PkVoucherNo;
                ReportSession[5] = DBGetVal.CompanyAddress;

                DialogResult ds = OMMessageBox.Show("Want to print this Voucher?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button3, "Preview");
                if (ds == DialogResult.Yes)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    childForm = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.GetCollectionPrint");
                    else
                        childForm = ObjFunction.LoadReportObject("GetCollectionPrint.rpt", CommonFunctions.ReportPath);
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            OMMessageBox.Show("" + ((VoucherType == 15) ? "Receipt" : "Payment") + " Print Successfully!!!", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                        else
                        {
                            OMMessageBox.Show("" + ((VoucherType == 15) ? "Receipt" : "Payment") + " not Print !!!", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Bill Report not exist !!!", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                else if (ds == DialogResult.Cancel)
                {
                    Form NewF = null;

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetCollectionPrint(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetCollectionPrint.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
        }

        private void btnCancelt2_Click(object sender, EventArgs e)
        {
            tabLedgerBook.SelectedTab = tabPage2;
        }

        private void btnCancelt1_Click(object sender, EventArgs e)
        {
            tabLedgerBook.SelectedTab = tabPage1;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgLedger.Rows.Count; i++)
                dgLedger.Rows[i].Cells[3].Value = chkSelectAll.Checked;
        }

        private void btnSLedger_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetStrLedger() != "")
                {
                    if (rbSummary.Checked)
                    {
                        plnLedger.Visible = false;
                        tabLedgerBook.Visible = false;
                        PB = new DBProgressBar(this);
                        PB.TimerStart();
                        PB.Ctrl = tabLedgerBook;
                        tabLedgerBook.SelectedIndex = 0;
                        dsVd = ObjDset.FillDset("New", "Exec GetAllLedgerBalance " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + Str + "'", CommonFunctions.ConStr);
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
                        DataGridView1.Focus();
                    }
                    else if (rbBillwise.Checked)//Please add new report here
                    {
                        string[] ReportSession;
                        Form NewF = null;
                        string str = "";
                        str = GetStrLedger();
                        ReportSession = new string[5];

                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        if (DBGetVal.KachhaFirm == false)//--umesh 16-11-2018
                        {
                            ReportSession[2] = (rbCustomer.Checked == true) ? VchType.Sales.ToString() : VchType.Purchase.ToString();
                        }
                        else
                        {
                            ReportSession[2] = (rbCustomer.Checked == true) ? VchType.DSales.ToString() : VchType.DPurchase.ToString();
                        }
                        // ReportSession[2] = (rbCustomer.Checked == true) ? VchType.Sales.ToString() : VchType.Purchase.ToString();
                        ReportSession[3] = DBGetVal.FirmNo.ToString();
                        ReportSession[4] = str;
                        //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        //    NewF = new Display.ReportViewSource(new Reports.RptLederBookBillSummary(), ReportSession);
                        //else
                        //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptLederBookBillSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                           NewF = new Display.ReportViewSource(new Reports.RptLederBookDetail(), ReportSession);
                        //NewF = new Display.ReportViewSource(new Reports.RptLederBookBillSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptLederBookDetail.rpt", CommonFunctions.ReportPath), ReportSession);

                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                    }
                    else if (rbDetailed.Checked)
                    {
                        printReport(4);
                    }

                    else if (rbMultibillPrint.Checked)
                    {
                        printReport(5);
                    }
                    else if (rbMonthwise.Checked)
                    {
                        if (SelectedCount > 1)
                        {
                            OMMessageBox.Show("Please select Single Party name Only !!!");
                            return;
                        }

                        for (int i = 0; i < dgLedger.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dgLedger.Rows[i].Cells[3].Value) == true)
                            {
                                LedgNo = Convert.ToInt64(dgLedger.Rows[i].Cells[1].Value.ToString());
                                LedgName = dgLedger.Rows[i].Cells[2].Value.ToString();
                                MNo = 0;
                                break;
                            }
                        }

                        if (rbMonthwise.Checked)
                            printReport(2);
                        //else if (rbDetailed.Checked)
                        //    printReport(3);
                    }
                    else
                    {
                        OMMessageBox.Show("Select Report Type", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                else
                {
                    OMMessageBox.Show("Select Ledger Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    plnLedger.Visible = true;
                    tabLedgerBook.Visible = false;
                    if (dgLedger.Rows.Count > 0)
                        dgLedger.CurrentCell = dgLedger[3, dgLedger.CurrentRow.Index];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public string GetStrLedger()
        {
            Str = "";
            SelectedCount = 0;
            for (int i = 0; i < dgLedger.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgLedger.Rows[i].Cells[3].Value) == true)
                {
                    SelectedCount++;

                    if (Str == "")
                        Str += dgLedger.Rows[i].Cells[1].Value.ToString();
                    else
                        Str += "," + dgLedger.Rows[i].Cells[1].Value.ToString();
                }
            }
            return Str;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            chkSelectAll.Checked = false;
            BindGridLedger();
        }

        private void dgLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnSLedger.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkSelectAll.Checked = false;
                    chkSelectAll_CheckedChanged(sender, (EventArgs)e);
                }
                else if (chkSelectAll.Checked == false)
                {
                    chkSelectAll.Checked = true;
                    chkSelectAll_CheckedChanged(sender, (EventArgs)e);
                }
            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyDownFormat(ctrl.Controls);

            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                //if (chkSelectAll.Checked == true)
                //    chkSelectAll.Checked = false;
                //else
                //    chkSelectAll.Checked = true;
                //for (int i = 0; i < dgLedger.Rows.Count; i++)
                //{
                //    dgLedger.Rows[i].Cells[3].Value = chkSelectAll.Checked;
                //}
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (plnLedger.Visible == true)
                    btnSLedger_Click(btnSLedger, new EventArgs());
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabLedgerBook.SelectedIndex == 0)
                {
                    ExportBook();
                }
                else if (tabLedgerBook.SelectedIndex == 1)
                {
                    ExportMonthDtls();
                }
                else if (tabLedgerBook.SelectedIndex == 2)
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
            string str = GetStrLedger();
            //dt = ObjFunction.GetDataView("Exec GetAllLedgerBalance " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + Str + "'").Table;
            dt = ObjFunction.TransferData(dtTab1);
            DataRow dr = dt.NewRow(); //double total = 0;
            int dtCount = dt.Columns.Count;// int temp = 0;
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
                excel.createHeaders(col, 1, "Ledger Book", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
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
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else if (i != 1)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
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
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
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
                    //    excel.createHeaders(dt.Rows.Count + col + 1, i + 1, "Total", excel.ColName(dt.Rows.Count + col + 1, i + 1), excel.ColName(dt.Rows.Count+col+1, i + 1 + 1), 2, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //else if (i != 1)
                    //{
                    //    excel.createHeaders(dt.Rows.Count + col + 1, i + 1, total.ToString(), excel.ColName(dt.Rows.Count + col + 1, i + 1), excel.ColName(dt.Rows.Count+col+1, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
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
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    for (int j = 0; j < dt.Rows.Count - 1; j++)
                    {
                        if (i == 0)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 7)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                        else
                        {
                            if (Convert.IsDBNull(dt.Rows[j].ItemArray[i].ToString()) != false)
                                dt.Rows[j].ItemArray[i] = 0;
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
            //dt=ObjFunction.GetDataView("Exec GetLedgerWiseVoucherDetails_E " + LedgNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + MNo + "").Table;
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
                        excel.createHeaders(col, i + 1, "SrNo", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Center);
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
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYY, 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 2)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                        else if (i == 5 || i == 6)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        else if (i == 3 || i == 4)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
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

        private void tabLedgerBook_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbCustomer_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            DataGridView2.DataSource = null;
            GridViewDaily.DataSource = null;
            //  rbBillwise.Checked = false; rbBillwise.Visible = true;
            BindGridLedger();
        }

        private void rbSupplier_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            DataGridView2.DataSource = null;
            GridViewDaily.DataSource = null;
            // rbBillwise.Checked = false; rbBillwise.Visible = true;
            BindGridLedger();
        }

        private void DTToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnShow.Focus();
            }
        }

        private void rbOther_CheckedChanged(object sender, EventArgs e)
        {
            DataGridView1.DataSource = null;
            DataGridView2.DataSource = null;
            GridViewDaily.DataSource = null;
            rbBillwise.Checked = false; rbBillwise.Visible = false;
            BindGridLedger();
        }

        private void RdMultibillPrint_CheckedChanged(object sender, EventArgs e)
        {

            //DataGridView1.DataSource = null;
            //DataGridView2.DataSource = null;
            //GridViewDaily.DataSource = null;
            //// rbBillwise.Checked = false; rbBillwise.Visible = true;
            //BindGridLedger();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabLedgerBook.SelectedTab = tabPage2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabLedgerBook.SelectedTab = tabPage1;
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

    }
}

