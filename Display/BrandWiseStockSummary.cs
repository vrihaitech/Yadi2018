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
    public partial class BrandWiseStockSummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        //DBProgressBar PB;
        long MfgCompNo = 0;
        string strItemNo = "";
        //bool ShowFlage;

        public BrandWiseStockSummary()
        {
            InitializeComponent();
        }

        public BrandWiseStockSummary(long MfgCompNo)
        {
            InitializeComponent();
            this.MfgCompNo = MfgCompNo;
        }

        private void BrandWiseStockSummary_Load(object sender, EventArgs e)
        {
            try
            {
                lblFirmName.Text = "";
                if (MfgCompNo != 0)
                {
                    Form NewFrm = new Vouchers.FirmSelection();
                    ObjFunction.OpenForm(NewFrm);
                    MfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                    lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
                }
                label3.Font = new Font("Verdana", 12, FontStyle.Bold);
                DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                DTToDate.MinDate = DTPFromDate.Value;
                KeyDownFormat(this.Controls);
                new GridSearch(dgItem, 1, 2);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnItmShow_Click(object sender, EventArgs e)
        {
            BindItem();
        }

        public void BindItem()
        {
            try
            {
                dgDetails.Visible = false;
                while (dgDetails.Rows.Count > 0)
                    dgDetails.Rows.RemoveAt(0);

                while (dgItem.Rows.Count > 0)
                    dgItem.Rows.RemoveAt(0);
                chkSelectAll.Checked = false;

                string strWhere = "";
                if (rdActive.Checked == true)
                    strWhere = " AND mItemMaster.IsActive='true' ";
                else if (rdDeActive.Checked == true)
                    strWhere = " AND mItemMaster.IsActive='false' ";
                string sql ="";
                if (MfgCompNo == 0)
                    sql = "  SELECT  mItemMaster.ItemNo, mItemMaster.ItemName AS ItemName,'false' as 'chkSelect' " +
                            " FROM dbo.MStockItems_V(NULL, NULL, NULL, NULL, NULL, NULL, NULL) AS MStockItems  " +
                            " WHERE (MStockItems.FkStockGroupTypeNo = 2) " +
                            strWhere +
                            " ORDER BY ItemName ";
                else
                    sql = "  SELECT  mItemMaster.ItemNo, mItemMaster.ItemName AS ItemName,'false' as 'chkSelect' " +
                            " FROM dbo.MStockItems_V(NULL, NULL, NULL, NULL, NULL, NULL, NULL) AS MStockItems  " +
                            " WHERE (MStockItems.FkStockGroupTypeNo = 2) AND mItemMaster.MfgCompNo=" + MfgCompNo + " " +
                            strWhere +
                            " ORDER BY ItemName ";
                DataTable dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgItem.Rows.Add();
                    for (int j = 0; j < dgItem.Columns.Count; j++)
                        dgItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
                dgItem.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgItem.Rows.Count > 0)
                {
                    dgItem.Focus();
                    dgItem.CurrentCell = dgItem[2, 0];
                }
                pnlItem.Visible = true;
                btnPrint.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    chkSelectAll.Checked = !chkSelectAll.Checked;

                    for (int i = 0; i < dgItem.Rows.Count; i++)
                    {
                        dgItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
                    }
                    BtnShow.Focus();
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                strItemNo = "";
                btnPrint.Visible = false;

                for (int i = 0; i < dgItem.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgItem.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strItemNo == "")
                            strItemNo = dgItem.Rows[i].Cells[0].Value.ToString();
                        else
                            strItemNo = strItemNo + "," + dgItem.Rows[i].Cells[0].Value.ToString();
                    }
                }
                if (strItemNo != "")
                {
                    if (MfgCompNo == 0) ReportSession = new string[3];
                    else ReportSession = new string[4];
                    
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = strItemNo;

                    if (MfgCompNo == 0)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptBrandWiseDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptBrandWiseDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else
                    {
                        ReportSession[3] = lblFirmName.Text.Replace("Firm Name :", "");
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptBrandWiseDetailsFirm(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptBrandWiseDetailsFirm.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);





                    //pnlItem.Visible = false;
                    //PB = new DBProgressBar(this);
                    //PB.TimerStart();
                    //PB.Ctrl = dgDetails;
                    //ShowFlage = true;
                    //while (dgDetails.Rows.Count > 0)
                    //    dgDetails.Rows.RemoveAt(0);
                    //dsVd = ObjDset.FillDset("New", "Exec GetBrandWiseStockSummary '" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + strItemNo + "'", CommonFunctions.ConStr);
                    //DataTable dt = dsVd.Tables[0];
                    //DataRow dr = dt.NewRow();
                    //dsVd.Tables[0].Rows.Add(dr);

                    //for (int i = 0; i < dsVd.Tables[0].Rows.Count; i++)
                    //{
                    //    dgDetails.Rows.Add();
                    //    for (int j = 0; j < dgDetails.Columns.Count; j++)
                    //        dgDetails.Rows[i].Cells[j].Value = dsVd.Tables[0].Rows[i].ItemArray[j];
                    //}

                    //if (dgDetails.Rows.Count >= 1)
                    //{
                    //    btnPrint.Visible = true;
                    //    dgDetails.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //    dgDetails.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //    dgDetails.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //    dgDetails.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //}
                    //else btnPrint.Visible = false;
                    //GetCount();
                }
                else
                    OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void GetCount()
        {
            try
            {
                for (int i = 0; i < dgDetails.Rows.Count - 1; i = i + 1)
                {
                    if (dgDetails.Rows[i].Index != dgDetails.Rows.Count - 1)
                    {
                        if (Convert.IsDBNull(dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[4].Value) != false)
                            dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[4].Value = 0;


                        //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = 100;
                        dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[4].Value = Convert.ToDouble(dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[4].Value) + Convert.ToDouble(dgDetails.Rows[i].Cells[4].Value);
                    }
                }

                //===========Total At footer===========

                dgDetails.Rows[dgDetails.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font =ObjFunction.GetFont() ;
                dgDetails.Rows[dgDetails.Rows.Count - 1].Cells[0].Value = "Total";
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgItem.Rows.Count; i++)
                {
                    dgItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
                }
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
                ReportSession = new string[3];

                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = strItemNo;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewBrandStockSummary(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewBrandStockSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rdActive_CheckedChanged(object sender, EventArgs e)
        {
            while (dgItem.Rows.Count > 0)
                dgItem.Rows.RemoveAt(0);
        }

    }   
}
