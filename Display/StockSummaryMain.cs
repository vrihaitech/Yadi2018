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
    public partial class StockSummaryMain : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "";

        public StockSummaryMain()
        {
            InitializeComponent();
        }


        private void StockSummary_Load(object sender, EventArgs e)
        {
            FillRateType();
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is CheckBox)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {



                string[] ReportSession;
                Form NewF = null;
                strItemNo = "";

                for (int i = 0; i < gvItem.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvItem.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strItemNo == "")
                            strItemNo = gvItem.Rows[i].Cells[0].Value.ToString();
                        else
                            strItemNo = strItemNo + "," + gvItem.Rows[i].Cells[0].Value.ToString();
                    }
                }

                if (strItemNo != "")
                {
                    if (rdQty.Checked == true)
                    {
                        ReportSession = new string[8];

                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = strItemNo;
                        ReportSession[3] = Convert.ToString(cmbRateType.SelectedIndex + 1);
                        ReportSession[4] = (chkIncludeTax.Checked == true) ? "1" : "0";
                        ReportSession[5] = (chkViewStock.Checked == true) ? "1" : "0";
                        ReportSession[6] = (rdAllColumns.Checked == true) ? "0" : "1";
                        ReportSession[7] = cmbRateType.Text;

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptStockSummaryQty(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptStockSummaryQty.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else if (rdAmount.Checked == true)
                    {
                        ReportSession = new string[8];

                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = strItemNo;
                        ReportSession[3] = Convert.ToString(cmbRateType.SelectedIndex + 1);
                        ReportSession[4] = (chkIncludeTax.Checked == true) ? "1" : "0";
                        ReportSession[5] = (chkViewStock.Checked == true) ? "1" : "0";
                        ReportSession[6] = (rdAllColumns.Checked == true) ? "0" : "1";
                        ReportSession[7] = cmbRateType.Text;

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptStockSummaryAmt(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptStockSummaryAmt.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else if (rdQtyAmt.Checked == true)
                    {
                        ReportSession = new string[8];

                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = strItemNo;
                        ReportSession[3] = Convert.ToString(cmbRateType.SelectedIndex + 1);
                        ReportSession[4] = (chkIncludeTax.Checked == true) ? "1" : "0";
                        ReportSession[5] = (chkViewStock.Checked == true) ? "1" : "0";
                        ReportSession[6] = (rdAllColumns.Checked == true) ? "0" : "1";
                        ReportSession[7] = cmbRateType.Text;

                        if (rdAllColumns.Checked == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RptStockSummaryQtyAmtAll(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptStockSummaryQtyAmtAll.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RptStockSummaryQtyAmtClosing(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptStockSummaryQtyAmtClosing.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                    }
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                    OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void FillRateType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RateType");
            dt.Columns.Add("RateTypeName");
            DataRow dr = null;


            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
                dr[0] = "ASaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
                dr[0] = "BSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_BillWithMRP)) == true)
            {
                dr = dt.NewRow();
                dr[1] = "MRP";
                dr[0] = "MRP";
                dt.Rows.Add(dr);
            }

            dr = dt.NewRow();
            dr[1] = "PurRate";
            dr[0] = "PurRate";
            dt.Rows.Add(dr);

            cmbRateType.DataSource = dt.DefaultView;
            cmbRateType.DisplayMember = dt.Columns[1].ColumnName;
            cmbRateType.ValueMember = dt.Columns[0].ColumnName;
            cmbRateType.SelectedIndex = 0;
        }

        public void BindGrid()
        {
            try
            {

                pnlPB.Visible = true;
                PBBar.Minimum = 1;
                PBBar.Value = 5;
                DataTable dt = new DataTable();
                string strWhere = "";
                if (rdActive.Checked == true)
                    strWhere = " Where mItemMaster.IsActive='true' ";
                else if (rdDeActive.Checked == true)
                    strWhere = " Where mItemMaster.IsActive='false' ";
                string str = "";
                if (DBGetVal.KachhaFirm == false)
                {
                    str = "SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN (ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                       " WHERE (MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                       " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo " +
                       strWhere + "and mItemMaster.ESFlag='false'" +
                       " ORDER BY ItemName";
                }
                else
                {
                    str = "SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN (ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                       " WHERE (MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                       " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo " +
                       strWhere +
                       " ORDER BY ItemName";
                }
                //string str = "SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN (ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                //                        " WHERE (MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                //                        " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo " +
                //                        strWhere +
                //                        " ORDER BY ItemName";
                dt = ObjFunction.GetDataView(str).Table;
                PBBar.Maximum = dt.Rows.Count + 5;


                gvItem.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvItem.Rows.Add();
                    Application.DoEvents();
                    PBBar.Value += 1;
                    for (int j = 0; j < gvItem.Columns.Count; j++)
                        gvItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                }
                gvItem.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvItem.Rows.Count > 0)
                {
                    gvItem.Focus();
                    gvItem.CurrentCell = gvItem[2, 0];
                }
                pnlPB.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;

                for (int i = 0; i < gvItem.Rows.Count; i++)
                {
                    gvItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
                }
                BtnShow.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;

                {
                    ReportSession = new string[5];

                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");


                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNew.rpt", CommonFunctions.ReportPath), ReportSession);

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
                BtnItmShow.Focus();

            }
        }

        public void GridNull()
        {

        }

        private void BtnItmShow_Click(object sender, EventArgs e)
        {
            BindGrid();

            if (gvItem.Rows.Count > 0)
            {
                gvItem.Focus();
                gvItem.CurrentCell = gvItem[2, 0];
            }
            chkSelectAll.Checked = false;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvItem.Rows.Count; i++)
            {
                gvItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnItmShow.Focus();
            }
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
