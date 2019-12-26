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
    public partial class DailyItemTransaction : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DBProgressBar PB;
        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;

        public DailyItemTransaction()
        {
            InitializeComponent();
        }

        private void DailyItemTransaction_Load(object sender, EventArgs e)
        {
            try
            {
                CompNo = 1;
                ObjFunction.FillCombo(cmbItemName, "SELECT ItemNo, ItemName AS ItemName FROM MStockItems_V(null,null,null,null,null,null,null) order by ItemName");
                label8.Text = "";
                DTPOnDate.Format = DateTimePickerFormat.Custom;
                DTPOnDate.CustomFormat = "dd-MMM-yyyy";
                if (Convert.ToDateTime(DBGetVal.ServerTime.ToString("dd-MMM-yyyy")) > DBGetVal.ToDate)
                    DTPOnDate.Text = DBGetVal.ToDate.ToString("dd-MMM-yyyy");
                else
                    DTPOnDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");

                rbItemWise.Checked = true;
                rbItemWise_CheckedChanged(sender, e);
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
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value = 0;
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[4].Value);
                }
            }

            //===========Total At footer===========

            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();//new Font("OM-DEV-0714", 15, FontStyle.Bold);
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = "Total";//"EHy$U";
        }

        public void GetCountVoucherDtls()
        {
            for (int i = 0; i < GridViewDaily.Rows.Count - 1; i = i + 1)
            {
                if (GridViewDaily.Rows[i].Index != GridViewDaily.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = 0;

                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = 0;
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value);
                }
            }

            //===========Total At footer===========

            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();//new Font("OM-DEV-0714", 15, FontStyle.Bold);
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value = "Total";
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBarCode.Text != "")
                {
                    btnPrint.Visible = false;
                    tabControl1.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = tabControl1;
                    if (DataGridView1.Rows.Count > 1)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;
                    tabControl1.SelectedTab = tabPage1;

                    txtBarCode.Text = txtBarCode.Text.Replace("\r", "").Replace("\n", "");
                    BItemNo = ObjQry.ReturnLong("Select MS.ItemNo from MStockItems_V(null,null,null,null,null,null,null) as MStockItems MS,MStockItemsBarCodeDetails MB Where MS.ItemNo =MB.ItemNo AND MB.BarCode='" + txtBarCode.Text + "' ", CommonFunctions.ConStr);

                    dsVd = ObjDset.FillDset("New", "Exec GetStockAllItem " + CompNo + ",'" + DBGetVal.FromDate.ToString("dd-MMM-yyyy") + "','" + DTPOnDate.Text + "', " + BItemNo + "", CommonFunctions.ConStr);

                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    GetCount();
                }
                else if (rbItemWise.Checked == true || rbAllItem.Checked == true)
                {

                    btnPrint.Visible = false;

                    if (rbItemWise.Checked == true)
                    {
                        EP.SetError(cmbItemName, "");
                        if (cmbItemName.SelectedIndex != 0)
                        {

                            tabControl1.Visible = false;
                            PB = new DBProgressBar(this);
                            PB.TimerStart();
                            PB.Ctrl = tabControl1;
                            tabControl1.SelectedTab = tabPage1;
                            dsVd = ObjDset.FillDset("New", "Exec GetStockAllItem " + CompNo + ",'" + DBGetVal.FromDate.ToString("dd-MMM-yyyy") + "','" + DTPOnDate.Text + "', " + cmbItemName.SelectedValue + "", CommonFunctions.ConStr);
                            DataTable dt = dsVd.Tables[0];
                            DataRow dr = dt.NewRow();
                            dsVd.Tables[0].Rows.Add(dr);
                            DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                            GetCount();
                        }
                        else
                        {
                            EP.SetError(cmbItemName, "Please Select Item Name");
                            EP.SetIconAlignment(cmbItemName, ErrorIconAlignment.MiddleRight);
                        }
                    }
                    else if (rbAllItem.Checked == true)
                    {
                        tabControl1.Visible = false;
                        PB = new DBProgressBar(this);
                        PB.TimerStart();
                        PB.Ctrl = tabControl1;
                        tabControl1.SelectedTab = tabPage1;
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItem " + CompNo + ",'" + DBGetVal.FromDate.ToString("dd-MMM-yyyy") + "','" + DTPOnDate.Text + "', 0", CommonFunctions.ConStr);
                        DataTable dt = dsVd.Tables[0];
                        DataRow dr = dt.NewRow();
                        dsVd.Tables[0].Rows.Add(dr);
                        DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                        GetCount();
                    }
                    DataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (DataGridView1.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;
                }
                else
                {
                    OMMessageBox.Show("Select Type to Display Records", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rbItemWise_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridNull();
                if (rbItemWise.Checked == true)
                {
                    panel1.Visible = true;
                    checkBox1.Visible = true;
                    cmbItemName.Focus();
                    label6.Text = "Item wise Stock Summary";
                }
                else
                {
                    panel1.Visible = false;
                    panel2.Visible = false;
                    checkBox1.Visible = false;
                    label6.Text = "All Item Stock Summary";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Selected == false)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ItNo = Convert.ToInt64(DataGridView1.CurrentRow.Cells[0].Value);
                    ItNm = Convert.ToString(DataGridView1.CurrentRow.Cells[1].Value);
                    label7.Font = ObjFunction.GetFont();
                    label7.Text = ItNm;
                    dsVd = ObjDset.FillDset("New", "Select * From GetItemClosingStockMonthly (" + CompNo + ",'" + DBGetVal.FromDate.ToString("dd-MMM-yyyy") + "','" + DTPOnDate.Text + "', " + ItNo + ", 0, 0 )", CommonFunctions.ConStr);

                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    for (int i = 2; i < 9; i++)
                    {
                        DataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    DataGridView2.Columns[0].Visible = false;
                    tabControl1.SelectedTab = tabPage2;
                    if (DataGridView2.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                    this.Cursor = Cursors.Default;
                }
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
                if (e.RowIndex > 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                    label8.Text = "(" + Convert.ToString(DataGridView2.CurrentRow.Cells[1].Value) + ")";
                    lblDatewise.Font = ObjFunction.GetFont();
                    lblDatewise.Text = ItNm;
                    dsVd = ObjDset.FillDset("New", "Exec GetItemClosingStockByDate  " + MNo + "," + CompNo + ",'" + DBGetVal.FromDate.ToString("dd-MMM-yyyy") + "','" + DTPOnDate.Text + "', " + ItNo + "", CommonFunctions.ConStr);
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;
                    tabControl1.SelectedTab = tabPage3;

                    GetCountVoucherDtls();
                    if (GridViewDaily.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (DataGridView1.Rows.Count > 0)
                    btnPrint.Visible = true;
                else
                    btnPrint.Visible = false;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (DataGridView2.Rows.Count > 0)
                    btnPrint.Visible = true;
                else
                    btnPrint.Visible = false;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if (GridViewDaily.Rows.Count > 0)
                    btnPrint.Visible = true;
                else
                    btnPrint.Visible = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                if (tabControl1.SelectedIndex == 0)
                {
                    ReportSession = new string[5];
                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = DBGetVal.FromDate.ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTPOnDate.Text).ToString("dd-MMM-yyyy");

                    if (rbAllItem.Checked == true)
                    {
                        ReportSession[3] = "0";
                        ReportSession[4] = "";
                    }
                    else if (rbItemWise.Checked == true)
                    {
                        if (txtBarCode.Text == "")
                        {
                            ReportSession[3] = cmbItemName.SelectedValue.ToString();
                            ReportSession[4] = cmbItemName.Text;
                        }
                        else
                        {
                            ReportSession[3] = BItemNo.ToString();
                            ReportSession[4] = DataGridView1.Rows[0].Cells[1].Value.ToString();
                        }
                    }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewDailyStock(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewDailyStock.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    ReportSession = new string[8];
                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = DBGetVal.FromDate.ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTPOnDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = ItNo.ToString();
                    ReportSession[4] = "0";//Type
                    ReportSession[5] = "0";//No
                    ReportSession[6] = "Daily Item Transaction";
                    ReportSession[7] = ItNm;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummDtlsByMonthly(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummDtlsByMonthly.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    ReportSession = new string[6];
                    ReportSession[0] = MNo.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = DBGetVal.FromDate.ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTPOnDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = ItNo.ToString();
                    ReportSession[5] = ItNm;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewGetItemClosingStockByDate(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGetItemClosingStockByDate.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbItemName, e, true);
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                BtnShow.Focus();
            }
        }

        private void DTPOnDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                if (panel1.Visible == true)
                    cmbItemName.Focus();
                else
                    BtnShow.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            GridNull();
            cmbItemName.SelectedIndex = 0;
            if (checkBox1.Checked == true)
            {
                panel2.Visible = true;
                cmbItemName.Enabled = false;
                txtBarCode.Focus();
            }
            else
            {
                panel2.Visible = false;
                txtBarCode.Text = "";
                cmbItemName.Enabled = true;
                cmbItemName.Focus();
            }
        }

        public void GridNull()
        {
            DataGridView1.DataSource = null;
            DataGridView2.DataSource = null;
            GridViewDaily.DataSource = null;
            tabControl1.SelectedTab = tabPage1;
            btnPrint.Visible = false;
            EP.SetError(cmbItemName, "");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
