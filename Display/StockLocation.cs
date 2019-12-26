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
    public partial class StockLocation : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dtLocation, dtItem;//dt
        string strTemp = "", strGodown = "", strSupp = "";
        //String sql = "";
        //int RType = 0;
        long MfgCompNo = 0;
        public StockLocation()
        {
            InitializeComponent();
        }
        public StockLocation(long MfgCompNo)
        {
            InitializeComponent();
            this.MfgCompNo = MfgCompNo;
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            lblFirmName.Text = "";
            if (MfgCompNo != 0)
            {
                Form NewFrm = new Vouchers.FirmSelection();
                ObjFunction.OpenForm(NewFrm);
                MfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgLocation.Rows.Count; i++)
            {
                dgLocation.Rows[i].Cells[1].Value = chkSelectAll.Checked;
            }
        }

        private void chkItemNameAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgItemName.Rows.Count; i++)
            {
                dgItemName.Rows[i].Cells[1].Value = chkSelectAllItem.Checked;
            }
        }

        private void btnLocationOk_Click(object sender, EventArgs e)
        {
            try
            {
                strGodown = "";
                pnlLocation.Visible = false;
                pnlProduct.Visible = true;
                while (dgItemName.Rows.Count > 0)
                    dgItemName.Rows.RemoveAt(0);
                int cntShop = 0, cntGd = 0;
                for (int k = 0; k < dgLocation.Rows.Count; k++)
                {

                    if (Convert.ToBoolean(dgLocation.Rows[k].Cells[1].FormattedValue) == true)
                    {

                        if (strGodown == "")
                        {
                            strGodown = "" + Convert.ToInt64(dgLocation.Rows[k].Cells[2].Value) + "";
                        }
                        else
                        {
                            strGodown = strGodown + "," + Convert.ToInt64(dgLocation.Rows[k].Cells[2].Value) + "";
                        }
                        if (Convert.ToInt64(dgLocation.Rows[k].Cells[2].Value) == 1) cntShop++;
                        if (Convert.ToInt64(dgLocation.Rows[k].Cells[2].Value) != 1) cntGd++;
                    }
                }
                if (strGodown == "")
                {
                    pnlLocation.Visible = true;
                    pnlProduct.Visible = false;
                    OMMessageBox.Show("Please Select Atleast One Location", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                }
                else
                {
                    string strWhere = "";
                    if (rdActive.Checked == true)
                        strWhere = " AND mItemMaster.IsActive='true' ";
                    else if (rdDeActive.Checked == true)
                        strWhere = " AND mItemMaster.IsActive='false' ";

                    strSupp = strTemp;
                    string sqlQuery="";
                    if (MfgCompNo == 0)
                        sqlQuery = "SELECT (SELECT     MItemGroup.StockGroupName + ' ' + CASE WHEN (ItemShortCode <> '') THEN ItemShortCode ELSE ItemName END AS Expr1 " +
                             " FROM          MStockItems AS MStockItems_1 INNER JOIN " +
                             " MStockGroup ON MStockItems_1.GroupNo = MItemGroup.StockGroupNo " +
                             " WHERE      (MStockItems_1.ItemNo = mItemMaster.ItemNo)) " +
                             " As ItemName,'false' AS chkCheckItem, mItemMaster.ItemNo FROM MStockItems " +
                             " where ItemNo in (Select Distinct ItemNo From TStockGodown Where GodownNo in (" + strGodown + ")) " +
                             strWhere +
                             " order by ItemName";
                    else
                        sqlQuery = "SELECT (SELECT     MItemGroup.StockGroupName + ' ' + CASE WHEN (ItemShortCode <> '') THEN ItemShortCode ELSE ItemName END AS Expr1 " +
                             " FROM          MStockItems AS MStockItems_1 INNER JOIN " +
                             " MStockGroup ON MStockItems_1.GroupNo = MItemGroup.StockGroupNo " +
                             " WHERE      (MStockItems_1.ItemNo = mItemMaster.ItemNo)) " +
                             " As ItemName,'false' AS chkCheckItem, mItemMaster.ItemNo FROM MStockItems  " +
                             " where mItemMaster.MfgCompNo=" + MfgCompNo + " AND ItemNo in (Select Distinct ItemNo From TStockGodown Where GodownNo in (" + strGodown + ")) " +
                             strWhere +
                             " order by ItemName";
                    //string sqlQuery = "SELECT DISTINCT MStockItems_V_1.ItemName, 'false' AS chkCheckItem, MStockItems_V_1.ItemNo " +
                    //                 "FROM dbo.MStockItems_V(NULL, NULL, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1 INNER JOIN " +
                    //                 "TStockGodown ON MStockItems_V_1.ItemNo = TStockGodown.ItemNo " +
                    //                 "Where TStockGodown.GodownNo In (" + strGodown + ")";

                    dtItem = ObjFunction.GetDataView(sqlQuery).Table;
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        dgItemName.Rows.Add();
                        for (int i = 0; i < dgItemName.Columns.Count; i++)
                        {

                            if (dtItem.Rows.Count > 0)
                                dgItemName.Rows[j].Cells[i].Value = dtItem.Rows[j].ItemArray[i].ToString();

                        }
                    }

                    chkSelectAllItem.Checked = false;
                    txtItemName.Text = "";
                    dgItemName.Focus();
                    if (dgItemName.Rows.Count != 0)
                        dgItemName.CurrentCell = dgItemName[1, dgItemName.CurrentRow.Index];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnSectionCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgLocation.Rows.Count; i++)
            {
                dgLocation.Rows[i].Cells[1].Value = false;
            }
            chkSelectAll.Checked = false;
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            try
            {
                strTemp = "";
                for (int i = 0; i < dgItemName.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgItemName.Rows[i].Cells[1].FormattedValue) == true)
                    {
                        if (strTemp == "")
                        {
                            strTemp = "" + Convert.ToInt64(dgItemName.Rows[i].Cells[2].Value) + "";
                        }
                        else
                        {
                            strTemp = strTemp + "," + Convert.ToInt64(dgItemName.Rows[i].Cells[2].Value) + "";
                        }
                    }
                }
                if (strTemp == "")
                {
                    OMMessageBox.Show("Please Select Atleast One Item", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    string[] ReportSession;
                    Form NewF = null;
                    if (rbLocationWise.Checked == true)
                    {
                        if (MfgCompNo == 0) ReportSession = new string[5];
                        else ReportSession = new string[6];

                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = txtFromDate.Text;
                        ReportSession[2] = txtToDate.Text;
                        ReportSession[3] = strTemp;
                        ReportSession[4] = strGodown;
                        if (MfgCompNo == 0)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewGodownStockSummary(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGodownStockSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        else
                        {
                            ReportSession[5] = lblFirmName.Text.Replace("Firm Name :", "");
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewGodownStockSummaryFirm(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGodownStockSummaryFirm.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rbProductWise.Checked == true)
                    {
                        if (MfgCompNo == 0) ReportSession = new string[5];
                        else ReportSession = new string[6];

                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = txtFromDate.Text;
                        ReportSession[2] = txtToDate.Text;
                        ReportSession[3] = strTemp;
                        ReportSession[4] = "";

                        if (MfgCompNo == 0)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewProductWiseGodownStockSummary(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewProductWiseGodownStockSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        else
                        {
                            ReportSession[5] = lblFirmName.Text.Replace("Firm Name :", "");
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewProductWiseGodownStockSummaryFirm(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewProductWiseGodownStockSummaryFirm.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

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

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            SearchGridValue(dgItemName, txtItemName.Text.Replace("'", "''"));
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                dgItemName.Focus();
            }
        }

        #region KeyDown Events
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
                if (pnlLocation.Visible == true)
                {
                    chkSelectAll.Checked = !chkSelectAll.Checked;
                    chkSelectAll_CheckedChanged(sender, e);
                    btnLocationOk.Focus();
                }
            }
        }
        #endregion

        public void SearchGridValue(DataGridView dg, string strSearch)
        {
            int i = 0, cnt = 0;
            try
            {
                if (strSearch != "")
                {
                    for (i = 0; i < dg.Rows.Count; i++)
                    {
                        dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        cnt = 0;
                        for (int j = 0; j < strSearch.Trim().ToUpper().Length; j++)
                        {
                            if (dg.Rows[i].Cells[0].Value != null)
                            {
                                if (strSearch.Trim().ToUpper()[j].ToString() == dg.Rows[i].Cells[0].Value.ToString().Trim().ToUpper()[j].ToString())
                                    cnt++;
                                else break;
                            }
                        }
                        if (cnt == strSearch.Trim().ToUpper().Length)
                        {
                            dg.CurrentCell = dg.Rows[i].Cells[1];
                            dg.Rows[i].DefaultCellStyle.BackColor = CommonFunctions.RowColor;
                        }
                        cnt = 0;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {


        }

        private void btnItemNameCancel_Click(object sender, EventArgs e)
        {
            pnlProduct.Visible = false;
            pnlLocation.Visible = true;
            if (dgLocation.Rows.Count > 0)
                dgLocation.CurrentCell = dgLocation[1, dgLocation.CurrentRow.Index];
            dgLocation.Focus();
        }

        private void ProductDetails()
        {
            try
            {
                if (rbProductWise.Checked == true)
                {
                    while (dgItemName.Rows.Count > 0)
                        dgItemName.Rows.RemoveAt(0);
                    string sqlQuery = "";
                    if (MfgCompNo == 0)
                        sqlQuery = "SELECT DISTINCT (SELECT     MItemGroup.StockGroupName + ' ' + CASE WHEN (ItemShortCode <> '') THEN ItemShortCode ELSE ItemName END AS Expr1 " +
                                            " FROM          MStockItems AS MStockItems_1 INNER JOIN " +
                                            " MStockGroup ON MStockItems_1.GroupNo = MItemGroup.StockGroupNo " +
                                            " WHERE      (MStockItems_1.ItemNo = mItemMaster.ItemNo)) As ItemName, " +
                                            " 'false' AS chkCheckItem, mItemMaster.ItemNo " +
                                            " FROM MStockItems " +
                                            " INNER JOIN TStockGodown ON mItemMaster.ItemNo = TStockGodown.ItemNo " +
                                            " Order By ItemName ";
                    else
                        sqlQuery = "SELECT DISTINCT (SELECT     MItemGroup.StockGroupName + ' ' + CASE WHEN (ItemShortCode <> '') THEN ItemShortCode ELSE ItemName END AS Expr1 " +
                                        " FROM          MStockItems AS MStockItems_1 INNER JOIN " +
                                        " MStockGroup ON MStockItems_1.GroupNo = MItemGroup.StockGroupNo " +
                                        " WHERE      (MStockItems_1.ItemNo = mItemMaster.ItemNo)) As ItemName, " +
                                        " 'false' AS chkCheckItem, mItemMaster.ItemNo " +
                                        " FROM MStockItems " +
                                        " INNER JOIN TStockGodown ON mItemMaster.ItemNo = TStockGodown.ItemNo INNER JOIN MStockGroup AS MS ON mItemMaster.GroupNo=MS.StockGroupNo " +
                                        " Where MS.MfgCompNo=" + MfgCompNo + " Order By ItemName ";

                    dtItem = ObjFunction.GetDataView(sqlQuery).Table;
                    for (int j = 0; j < dtItem.Rows.Count; j++)
                    {
                        dgItemName.Rows.Add();
                        for (int i = 0; i < dgItemName.Columns.Count; i++)
                        {

                            if (dtItem.Rows.Count > 0)
                                dgItemName.Rows[j].Cells[i].Value = dtItem.Rows[j].ItemArray[i].ToString();

                        }
                    }
                    chkSelectAllItem.Checked = false;
                    strGodown = "";
                    txtItemName.Text = "";
                    dgItemName.Focus();
                    dgItemName.CurrentCell = dgItemName[1, dgItemName.CurrentRow.Index];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                chkSelectAll.Checked = false;
                chkSelectAllItem.Checked = false;
                if (rbLocationWise.Checked == true)
                {
                    pnlProduct.Visible = false;
                    pnlLocation.Visible = true;
                    while (dgLocation.Rows.Count > 0)
                        dgLocation.Rows.RemoveAt(0);
                    string sqlQuery = "Select GodownName,'false' as chkCheck,GodownNo From MGodown Where GodownNo<>1  order By GodownName";
                    //txtFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                    //txtToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    dtLocation = ObjFunction.GetDataView(sqlQuery).Table;
                    for (int j = 0; j < dtLocation.Rows.Count; j++)
                    {
                        dgLocation.Rows.Add();
                        for (int i = 0; i < dgLocation.Columns.Count; i++)
                        {
                            if (dtLocation.Rows.Count > 0)
                                dgLocation.Rows[j].Cells[i].Value = dtLocation.Rows[j].ItemArray[i].ToString();

                        }
                    }
                    KeyDownFormat(this.Controls);
                    if (dgLocation.Rows.Count > 0)
                        dgLocation.CurrentCell = dgLocation[1, dgLocation.CurrentRow.Index];
                    dgLocation.Focus();
                }
                else if (rbProductWise.Visible == true)
                {
                    pnlLocation.Visible = false;
                    pnlProduct.Visible = true;
                    ProductDetails();
                    btnItemNameCancel.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            pnlLocation.Visible = false;
            pnlProduct.Visible = false;
        }

        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            txtToDate.MinDate = txtFromDate.Value;
        }
    }
}
