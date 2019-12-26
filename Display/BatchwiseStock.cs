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
    public partial class BatchwiseStock : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dtLocation, dtItem;
        string strTemp = "", strGodown = "", strSupp = "";

        public BatchwiseStock()
        {
            InitializeComponent();
        }

        private void BatchwiseStock_Load(object sender, EventArgs e)
        {
            txtFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
           txtToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
           txtToDate.MinDate = txtFromDate.Value;
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
                chkSelectAllItem.Checked = false;
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

                    strSupp = strTemp;
                    //string sqlQuery = "SELECT (Select Distinct MS.ItemName From MStockItems_V(NULL,MStockItems.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MS) ItemName,'false' AS chkCheckItem, mItemMaster.ItemNo FROM MStockItems " +
                    //               " where ItemNo in (Select Distinct ItemNo From TStockGodown Where GodownNo in (" + strGodown + ")) order by ItemName";
                    string sqlQuery = " SELECT  distinct  TStock.BatchNo ,'false' as chkCheckItem ,0 as ItemNo " +
                                    " FROM MGodown INNER JOIN TStockGodown ON MGodown.GodownNo = TStockGodown.GodownNo INNER JOIN " +
                                    " TStock ON TStockGodown.FKStockTrnNo = TStock.PkStockTrnNo  INNER JOIN "+
                                    " TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo " +
                                    " where   TStock.BatchNo<>'' and TStockGodown.GodownNo in(" + strGodown + ")  "+
                                    " and TVoucherEntry.VoucherDate>='"+Convert.ToDateTime(txtFromDate.Text)+"' and TVoucherEntry.VoucherDate<='"+Convert.ToDateTime(txtToDate.Text)+"'";
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
                string strTemp = "";
                for (int i = 0; i < dgItemName.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgItemName.Rows[i].Cells[1].FormattedValue) == true)
                    {
                        if (strTemp == "")
                        {
                            //strTemp ="'''" + dgItemName.Rows[i].Cells[0].Value + "'";
                            strTemp = "'" + dgItemName.Rows[i].Cells[0].Value + "'";
                        }
                        else
                        {
                            //strTemp = strTemp + "',''" + dgItemName.Rows[i].Cells[0].Value + "'";
                            strTemp = strTemp + ",'" + dgItemName.Rows[i].Cells[0].Value + "'";
                        }
                    }
                }
                //if (strTemp != "") strTemp = strTemp + "''";
                if (strTemp == "")
                {
                    OMMessageBox.Show("Please Select Atleast One BatchNo", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    string[] ReportSession;
                    Form NewF = null;
                    if (rbSummary.Checked == true)
                    {

                        ReportSession = new string[5];
                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = txtFromDate.Text;
                        ReportSession[2] = txtToDate.Text;
                        ReportSession[3] = strTemp;
                        ReportSession[4] = strGodown;

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.GetBatchNowiseStockSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBatchNowiseStockSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rbDetails.Checked == true)
                    {
                        ReportSession = new string[5];
                        ReportSession[0] = DBGetVal.FirmNo.ToString();
                        ReportSession[1] = txtFromDate.Text;
                        ReportSession[2] = txtToDate.Text;
                        ReportSession[3] = strTemp;
                        ReportSession[4] = strGodown;

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.GetBatchNowiseDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBatchNowiseDetails.rpt", CommonFunctions.ReportPath), ReportSession);
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
                if (pnlProduct.Visible == true)
                {
                    chkSelectAllItem.Checked = !chkSelectAllItem.Checked;
                    chkItemNameAll_CheckedChanged(sender, e);
                    btnShowReport.Focus();
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
            chkSelectAll.Checked = false;
            pnlProduct.Visible = false;
            pnlLocation.Visible = true;
            if (dgLocation.Rows.Count > 0)
                dgLocation.CurrentCell = dgLocation[1, dgLocation.CurrentRow.Index];
            dgLocation.Focus();
        }

     
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                //if (rbSummary.Checked == true)
                //{
                chkSelectAll.Checked = false;
                    pnlProduct.Visible = false;
                    pnlLocation.Visible = true;
                    while (dgLocation.Rows.Count > 0)
                        dgLocation.Rows.RemoveAt(0);
                 //   string sqlQuery = "Select GodownName,'false' as chkCheck,GodownNo From MGodown Where GodownNo<>1  order By GodownName";
                    string sqlQuery = " SELECT DISTINCT MGodown.GodownName, 'false' AS chkCheck, MGodown.GodownNo "+
                                      " FROM  MGodown INNER JOIN TStockGodown ON MGodown.GodownNo = TStockGodown.GodownNo INNER JOIN "+
                                      " TStock ON TStockGodown.FKStockTrnNo = TStock.PkStockTrnNo INNER JOIN "+
                                      " TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo "+
                                      " WHERE     (TStock.BatchNo <> '')  and TVoucherEntry.VoucherDate>='"+Convert.ToDateTime(txtFromDate.Text)+"' and TVoucherEntry.VoucherDate<='"+Convert.ToDateTime(txtToDate.Text)+"'"+
                                      " ORDER BY MGodown.GodownName ";
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
                //}
                //else if (rbDetails.Visible == true)
                //{
                //    pnlLocation.Visible = false;
                //    pnlProduct.Visible = true;
                //    ProductDetails();
                //    btnItemNameCancel.Visible = false;
                //}
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
