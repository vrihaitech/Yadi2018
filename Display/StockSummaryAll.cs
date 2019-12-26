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
    public partial class StockSummaryAll : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;

        public StockSummaryAll()
        {
            InitializeComponent();
        }

        private void StockSummuryAll_Load(object sender, EventArgs e)
        {
            try
            {
                DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                DTToDate.MinDate = DTPFromDate.Value;
                // pnlStatus.Visible = false;
                CompNo = 1;
                lblbarand.Visible = false;
                lblCategory.Visible = false;
                lblCompany.Visible = false;
                lblSubCategory.Visible = false;
                cmbbrand.Visible = false;
                cmbcategory.Visible = false;
                cmbCompany.Visible = false;
                cmbsubcategory.Visible = false;
                label4.Visible = false;
                cmbItem.Visible = false;
                ObjFunction.FillCombo(cmbCompany, "SELECT  MfgCompanyNo,MfgCompanyName  FROM  MMfgCompany where MfgCompanyNo=0");
                ObjFunction.FillCombo(cmbbrand, "SELECT  BrandCode,BrandName FROM  MBrand where BrandCode=0");
                ObjFunction.FillCombo(cmbcategory, "Select GroupNo,GroupName From MStockGroups where GroupNo=0");
                ObjFunction.FillCombo(cmbsubcategory, "SELECT CategoryNo,CategoryName FROM MStockCategory where CategoryNo=0");
                panel2.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            btnPrint.Visible = false;
            timer1.Enabled = true;
            //pnlStatus.Visible = true;
            tabControl1.Visible = false;
            tabControl1.SelectedTab = tabPage1;
            EP.SetError(cmbbrand, "");
            EP.SetError(cmbcategory, "");
            EP.SetError(cmbCompany, "");
            EP.SetError(cmbItem, "");
            EP.SetError(cmbsubcategory,"");

            try
            {
                if (txtBarCode.Text == "" && chkbarcode.Checked == false && Valid() == true && chkall.Checked == false)
                {
                    dsVd = ObjDset.FillDset("New", "Exec GetStockSummaryAll " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + cmbcategory.SelectedValue + "','" + cmbsubcategory.SelectedValue + "','" + cmbCompany.SelectedValue + "','" + cmbbrand.SelectedValue + "','" + cmbItem.SelectedValue + "'", CommonFunctions.ConStr);
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    tabControl1.Visible = true;
                    GetCount();
                }
                else if (chkall.Checked == true)
                {
                    dsVd = ObjDset.FillDset("New", "Exec GetStockSummaryAll " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + cmbcategory.SelectedValue + "','" + cmbsubcategory.SelectedValue + "','" + cmbCompany.SelectedValue + "','" + cmbbrand.SelectedValue + "',0", CommonFunctions.ConStr);
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    tabControl1.Visible = true;
                    GetCount();
                }

                else if (chkbarcode.Checked == true)
                {
                    EP.SetError(txtBarCode, "");
                    if (txtBarCode.Text == "")
                    {
                        EP.SetError(txtBarCode, "Enter Barcode");

                    }
                    else
                    {
                        txtBarCode.Text = txtBarCode.Text.Replace("\r", "").Replace("\n", "");
                        BItemNo = ObjQry.ReturnLong("Select MS.ItemNo from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems MS,MStockItemsBarCodeDetails MB Where MS.ItemNo =MB.ItemNo AND MB.BarCode='" + txtBarCode.Text + "' ", CommonFunctions.ConStr);
                        dsVd = ObjDset.FillDset("New", "Exec GetStockSummaryAll " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + cmbcategory.SelectedValue + "','" + cmbsubcategory.SelectedValue + "','" + cmbCompany.SelectedValue + "','" + cmbbrand.SelectedValue + "','" + BItemNo + "'", CommonFunctions.ConStr);
                        DataTable dt = dsVd.Tables[0];
                        DataRow dr = dt.NewRow();
                        dsVd.Tables[0].Rows.Add(dr);
                        DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                        tabControl1.Visible = true;
                        GetCount();
                    }
                    txtBarCode.Text = "";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

            //cmbItem.SelectedIndex = 0;
            //cmbCompany.SelectedIndex = 0;
            //cmbCompany.SelectedIndex = 0;
            //cmbcategory.SelectedIndex = 0;
            //cmbbrand.SelectedIndex = 0;
            //cmbsubcategory.SelectedIndex = 0;
            this.Cursor = Cursors.Default;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                if ((DataGridView1.CurrentRow.Cells[1].Value.ToString()) != "EHy$U")
                {
                    tabControl1.SelectedTab = tabPage2;
                    ItNo = Convert.ToInt64(DataGridView1.CurrentRow.Cells[1].Value);
                    ItNm = Convert.ToString(DataGridView1.CurrentRow.Cells[2].Value);
                    label7.Font = new Font("OM-DEV-0714", 14F, System.Drawing.FontStyle.Bold);
                    label7.Text = ItNm;
                    dsVd = ObjDset.FillDset("New", "Select * From GetItemClosingStockMonthly (" + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', " + ItNo + ", 0, 0 )", CommonFunctions.ConStr);

                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    DataGridView2.Columns[0].Visible = false;
                    if (DataGridView2.Rows.Count >= 1)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                }
                else
                {
                   // tabControl1.SelectedTab = tabPage1;
                }
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
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value = 0;


                    //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = 100;
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[5].Value);
                }
            }

            //===========Total At footer===========

            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = "Total";
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

                    //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[0].Value = 100;
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value);
                }
            }

            //===========Total At footer===========

            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont() ;
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value = "Total";
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > 0)
                {
                    tabControl1.SelectedTab = tabPage3;
                    MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                    label8.Text = "(" + Convert.ToString(DataGridView2.CurrentRow.Cells[1].Value) + ")";
                    lblDatewise.Font = ObjFunction.GetFont();
                    lblDatewise.Text = ItNm;
                    dsVd = ObjDset.FillDset("New", "Exec GetItemClosingStockByDate  " + MNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', " + ItNo + "", CommonFunctions.ConStr);
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;

                    if (GridViewDaily.Rows.Count >= 1)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                    GetCountVoucherDtls();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataGridView2.Columns[e.ColumnIndex].Name == "MonthName")
            {
                e.CellStyle.Font = new Font("Verdana", 10);
            }
        }

        private void GridViewDaily_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherTypeName")
            {
                e.CellStyle.Font = new Font("Verdana", 10);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (progressBar1.Value < 100)
            //{
            //    progressBar1.Value = progressBar1.Value + 10;
            //    if (progressBar1.Value == 0 || progressBar1.Value == 30 || progressBar1.Value == 60 || progressBar1.Value == 90)
            //        lblStatus.Text = "Processing .";
            //    else if (progressBar1.Value == 10 || progressBar1.Value == 40 || progressBar1.Value == 70 || progressBar1.Value == 100)
            //        lblStatus.Text = "Processing ..";
            //    else if (progressBar1.Value == 20 || progressBar1.Value == 50 || progressBar1.Value == 80)
            //        lblStatus.Text = "Processing ...";
            //}
            //else
            //{
            //    this.Cursor = Cursors.Default;
            //    progressBar1.Value = 0;
            //    lblStatus.Text = "";
            //    timer1.Enabled = false;
            //    tabControl1.Visible = true;
            //    tabControl1.SelectedTab = tabPage1;
            //    if (DataGridView1.Rows.Count > 1)
            //        btnPrint.Visible = true;
            //    else btnPrint.Visible = false;
            //    pnlStatus.Visible = false;
            //}
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
                chkall.Focus();
                    
            }
        }

        private void chkCmpNm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridNull();

                chkall.Checked = false;
                lblCompany.Visible = chkCmpNm.Checked;
                cmbCompany.Visible = chkCmpNm.Checked;
                if (chkCmpNm.Checked == true)
                    ObjFunction.FillCombo(cmbCompany, "SELECT  MfgCompanyNo,cast (MfgUesrNo As varchar)+ ' - ' +MfgCompanyName As MfgCompanyName FROM  MMfgCompany");
                else
                    cmbCompany.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void chkbrand_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridNull();
                chkall.Checked = false;
                lblbarand.Visible = chkbrand.Checked;
                cmbbrand.Visible = chkbrand.Checked;
                if (chkbrand.Checked == true)
                    ObjFunction.FillCombo(cmbbrand, "SELECT  BrandCode,cast(BrandUserNo As varchar)+ ' - ' +BrandName As BrandName FROM  MBrand ");
                else
                    cmbbrand.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void chkgroup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridNull();
                chkall.Checked = false;
                lblCategory.Visible = chkgroup.Checked;
                cmbcategory.Visible = chkgroup.Checked;
                if (chkgroup.Checked == true)
                    ObjFunction.FillCombo(cmbcategory, "Select GroupNo,cast(GroupUserNo As varchar)+ ' - ' +GroupName As GroupName From MStockGroups");
                else
                    cmbcategory.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void chKsubcategory_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkall.Checked = false;
                lblSubCategory.Visible = chKsubcategory.Checked;
                cmbsubcategory.Visible = chKsubcategory.Checked;
                if (chKsubcategory.Checked == true)
                    ObjFunction.FillCombo(cmbsubcategory, "SELECT CategoryNo,CategoryName FROM MStockCategory");
                else
                    cmbsubcategory.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                    ReportSession[1] = cmbcategory.SelectedValue.ToString();
                    ReportSession[2] = cmbsubcategory.SelectedValue.ToString();
                    ReportSession[3] = cmbCompany.SelectedValue.ToString();
                    ReportSession[4] = cmbbrand.SelectedValue.ToString();
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryAll(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryAll.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    ReportSession = new string[8];

                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = ItNo.ToString();
                    ReportSession[4] = "0";//Type
                    ReportSession[5] = "0";//No
                    ReportSession[6] = "Stock Summary All Details";
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
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
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

        private void chkall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkall.Checked == true)
            {
                GridNull();
                chkbrand.Checked = false;
                chkCmpNm.Checked = false;
                chkgroup.Checked = false;
                chKsubcategory.Checked = false;
                chkItem.Checked = false;

            }
            else
            {
                GridNull();
            }
        }

        private void cmbCompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbCompany, e, true);
        }

        private void cmbbrand_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbbrand, e, true);
        }

        private void cmbcategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbcategory, e, true);
        }

        private void cmbsubcategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbsubcategory, e, true);
        }

        public void GridNull()
        {
            DataGridView1.DataSource = null;
            DataGridView2.DataSource = null;
            GridViewDaily.DataSource = null;
            tabControl1.SelectedTab = tabPage1;
            btnPrint.Visible = false;
            EP.SetError(cmbCompany, "");
            EP.SetError(cmbbrand, "");
            EP.SetError(cmbcategory, "");
            EP.SetError(cmbsubcategory, "");
        }

        private void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkItem.Checked == true)
                {
                    label4.Visible = chkItem.Checked;
                    cmbItem.Visible = chkItem.Checked;
                    cmbItem.Enabled = true;
                    ObjFunction.FillCombo(cmbItem, "SELECT ItemNo,ItemName AS ItemName FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) order by ItemName");
                    panel2.Visible = false;
                    chkall.Checked = false;
                    chkbarcode.Checked = false;
                }
                else
                {
                    GridNull();
                    label4.Visible = false;
                    cmbItem.Visible = chkItem.Checked;
                    cmbItem.SelectedIndex = 0;
                    // cmbItem.SelectedIndex = 0;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void chkbarcode_CheckedChanged(object sender, EventArgs e)
        {

            if (chkbarcode.Checked == true)
            {
                GridNull();
                panel2.Visible = true;
                chkItem.Checked = false;
                //cmbItem.SelectedIndex = 0;
                cmbItem.Enabled = false;
             }
            else
            {
                GridNull();
                panel2.Visible = false;
                cmbItem.Enabled = true;
            }
        }

        public bool Valid()
        {
            bool flag=false;
            if (chkCmpNm.Checked == true && cmbCompany.SelectedIndex == 0)
            {
                EP.SetError(cmbCompany, "Select Mgf. Company");
                flag = false;
            }
            else if (chkgroup.Checked == true && cmbcategory.SelectedIndex == 0)
            {
                EP.SetError(cmbcategory, "Select Category");
                flag = false;
            }
            else if (chkbrand.Checked == true && cmbbrand.SelectedIndex == 0)
            {
                EP.SetError(cmbbrand, "Select brand");
                flag = false;
            }
            else if (chkItem.Checked == true && cmbItem.SelectedIndex == 0)
            {
                EP.SetError(cmbItem, "Select Item");
                flag = false;
            }
            else if (chKsubcategory.Checked == true && cmbsubcategory.SelectedIndex == 0)
            {
                EP.SetError(cmbsubcategory, "Select SubCategory");
                flag = false;
            }
            else if (chkall.Checked == false && chkCmpNm.Checked == false && chkgroup.Checked == false && chkbrand.Checked == false && chkItem.Checked == false && chKsubcategory.Checked == false)
            {
                OMMessageBox.Show("Select Atleast One Option");
                flag = false;
            }
            else
            {
                flag = true;
            }
            return flag;
          
        }

        private void cmbItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbItem, e, true);
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
