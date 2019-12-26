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
    public partial class StockSummaryHSNCodeWise : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "";

        public StockSummaryHSNCodeWise()
        {
            InitializeComponent();
        }

        private void StockSummaryHSNCodeWise_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            KeyDownFormat(this.Controls);
            rbMonthlySummary.Visible = false;
            new GridSearch(gvItem, 1, 2);
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

        public void BindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                string strWhere = "";
                if (rdActive.Checked == true)
                    strWhere = " Where mItemMaster.IsActive='true' ";
                else if (rdDeActive.Checked == true)
                    strWhere = " Where mItemMaster.IsActive='false' ";

                string str = "";
                if (DBGetVal.KachhaFirm == false)
                {
                    str = " select DISTINCT itemno,ItemName,chk from (  SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN(ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                      " WHERE(MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                      " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo  INNER JOIN  " +
                      " TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo " + strWhere + " and mItemMaster.ESFlag='false'" +
                    //  " AND TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") +
                    " AND TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' " +
                   " union all SELECT distinct MRecipeSub.RawProductID as itemno, MItemGroup.ItemGroupName + ' ' + ItemName as ItemName,'false' AS chk " +
                  " FROM MRecipeSub INNER JOIN   MRecipeMain ON MRecipeSub.FKMRecipeID = MRecipeMain.MRecipeID INNER JOIN     MItemMaster ON MRecipeSub.RawProductID = MItemMaster.ItemNo " +
                   " INNER JOIN    MItemGroup ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo " + strWhere + " and mItemMaster.ESFlag = 'false'" +
                  //    " AND MRecipeMain.RDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") +
                    " AND MRecipeMain.RDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' )tabl ORDER BY ItemName ";
                }
                else
                {
                    str = " select DISTINCT itemno,ItemName,chk from (  SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN(ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                      " WHERE(MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                      " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                       " TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo " + strWhere + " " +
                    //  " AND TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") +
                    " AND TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "'  "  +
                    " union all SELECT distinct MRecipeSub.RawProductID as itemno, MItemGroup.ItemGroupName + ' ' + ItemName as ItemName,'false' AS chk " +
                   " FROM MRecipeSub INNER JOIN   MRecipeMain ON MRecipeSub.FKMRecipeID = MRecipeMain.MRecipeID INNER JOIN     MItemMaster ON MRecipeSub.RawProductID = MItemMaster.ItemNo " +
                   " INNER JOIN    MItemGroup ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo " + strWhere + " " +
                  //" AND MRecipeMain.RDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") +
                    " AND MRecipeMain.RDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' )tabl ORDER BY ItemName ";
                }
                dt = ObjFunction.GetDataView(str).Table;
                gvItem.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvItem.Rows.Add();
                    Application.DoEvents();
                    for (int j = 0; j < gvItem.Columns.Count; j++)
                        gvItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
                gvItem.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvItem.Rows.Count > 0)
                {
                    gvItem.Focus();
                    gvItem.CurrentCell = gvItem[2, 0];
                }
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
                btnPrint.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
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
                    if (rbHSNCodeWise.Checked == true)
                    {
                        ReportSession = new string[5];
                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = strItemNo;
                        if (DBGetVal.KachhaFirm == false)
                        {
                            ReportSession[4] = "0";
                        }
                        else
                        { ReportSession[4] = "1"; }
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("StockReportHSNCodeWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rbItemWise.Checked == true)
                    {
                        ReportSession = new string[5];
                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = strItemNo;
                        if (DBGetVal.KachhaFirm == false)
                        {
                            ReportSession[4] = "0";//sales
                        }
                        else
                        { ReportSession[4] = "1";//estimate 
                        }
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("StockReportItemWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                    }
                    else if (rbMonthlySummary.Checked == true)
                    {
                        ReportSession = new string[5];
                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = strItemNo;
                        if (DBGetVal.KachhaFirm == false)
                        {
                            ReportSession[4] = "0";
                        }
                        else
                        { ReportSession[4] = "1"; }
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewStockSummDtlsByMonthlyRType(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("StockReportMonthWiseSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                    OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
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

        private void BtnItmShow_Click(object sender, EventArgs e)
        {
            BindGrid();
            panel1.Visible = true;
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

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
