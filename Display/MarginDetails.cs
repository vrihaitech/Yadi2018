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
    public partial class MarginDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataTable dtParty = new DataTable();
        DBProgressBar PB;

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "", strLedgerNo = "";

        public MarginDetails()
        {
            InitializeComponent();
        }

        private void StockSummary_Load(object sender, EventArgs e)
        {
             
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            FillGrid();
            KeyDownFormat(this.Controls);

            new GridSearch(gvItem, 1);
        }

        private void FillGrid()
        {
            ObjFunction.FillCombo(cmbDepart, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.FkStockDeptNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 4) ORDER BY MItemGroup.StockGroupName");
            ObjFunction.FillCombo(cmbCategory, "Select StockGroupNo, StockGroupName from MStockGroup where ControlGroup = 2 and IsActive='True' order by MItemGroup.StockGroupName"); //, "SELECT DISTINCT StockGroupNo,StockGroupName FROM MStockGroup WHERE (MItemGroup.IsActive = 'True') And ControlGroup = 2 order by StockGroupName");
            ObjFunction.FillCombo(cmbBrandName, "Select StockGroupNo, StockGroupName from MStockGroup where ControlGroup = 3 and IsActive='True' order by MItemGroup.StockGroupName");    //, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
            ObjFunction.FillCombo(cmbManu, "Select MfgCompNo, MfgCompName from MManufacturerCompany where IsActive='True' order by MfgCompName");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;

                ReportSession = new string[5];

                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = Convert.ToString(false);
                ReportSession[4] = strItemNo;
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("MarginDetail.rpt", CommonFunctions.ReportPath), ReportSession);
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

        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                btnShow.Focus();

            }
        }

        private void BtnPartyShow_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridItem(1);

                if (DTToDate.Value < DTPFromDate.Value)
                {
                    OMMessageBox.Show("To Date cannot be less than From Date ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    DTPFromDate.Focus();
                    pnlPartyDetails.Visible = false;
                }
                else
                {
                    pnlPartyDetails.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = pnlPartyDetails;
                    // BindGridParty();
                    //pnlPartyDetails.Visible = true;
                    strLedgerNo = "";
                    strItemNo = "";
                    chkPartySelectAll.Checked = false;
                }
                //if (ObjFunction.GetComboValue(cmbManu) > 0)
                //{
                //    cmbDepart.SelectedValue = 0;
                //    cmbCategory.SelectedValue = 0;
                //    cmbBrandName.SelectedValue = 0;
                //    cmbManu.SelectedValue = 0;
                //}
                //else if (ObjFunction.GetComboValue(cmbDepart) > 0 || ObjFunction.GetComboValue(cmbCategory) > 0 || ObjFunction.GetComboValue(cmbBrandName) > 0)
                //    cmbManu.SelectedValue = 0;
                // FillGrid();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridItem(int ch)
        {
            try
            {

                if (ObjFunction.GetComboValue(cmbBrandName) <= 0 && ObjFunction.GetComboValue(cmbDepart) <= 0 && ObjFunction.GetComboValue(cmbCategory) <= 0 && ObjFunction.GetComboValue(cmbManu) <= 0 && ch == 1)
                {
                    OMMessageBox.Show("Select atleast one object");
                }
                else
                {
                    dtParty = new DataTable();
                    string str = "";
                    if (ch == 2)
                        str = "SELECT 0 AS SrNo, MItemGroup.StockGroupName + ' '+ mItemMaster.ItemName AS ItemName,MStockItems.ItemNo,'false' as chck from MStockItems Inner join MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo where  mItemMaster.IsActive='True'";
                    else
                    {

                        if (ObjFunction.GetComboValue(cmbManu) > 0)
                        {
                            str = "SELECT 0 AS SrNo, MItemGroup.StockGroupName + ' '+ mItemMaster.ItemName AS ItemName,MStockItems.ItemNo,'false' as chck from MStockItems Inner join MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo where mItemMaster.MfgCompNo=" + cmbManu.SelectedValue + " and mItemMaster.IsActive='True'";
                        }
                        else
                        {
                            //str = "SELECT 0 AS SrNo, MItemGroup.StockGroupName + ' '+ mItemMaster.ItemName AS ItemName,MStockItems.ItemNo,'false' as chck from MStockItems Inner join MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo where mItemMaster.GroupNo=" + cmbBrandName.SelectedValue + " and mItemMaster.IsActive='True'";
                            str = "SELECT 0 AS SrNo, MItemGroup.StockGroupName + ' '+ mItemMaster.ItemName AS ItemName,MStockItems.ItemNo,'false' as chck from MStockItems Inner join MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo where mItemMaster.IsActive='True' ";
                            if (ObjFunction.GetComboValue(cmbBrandName) > 0)
                            {
                                str += "and mItemMaster.GroupNo=" + cmbBrandName.SelectedValue + "";
                            }
                            if (ObjFunction.GetComboValue(cmbDepart) > 0)
                            {
                                str += "and mItemMaster.FkStockDeptNo=" + ObjFunction.GetComboValue(cmbDepart) + "";
                            }
                            if (ObjFunction.GetComboValue(cmbCategory) > 0)
                            {
                                str += "and mItemMaster.GroupNo1=" + ObjFunction.GetComboValue(cmbCategory) + "";
                            }
                            str += "order by MItemGroup.StockGroupName + ' '+ mItemMaster.ItemName";
                        }
                    }



                    dtParty = ObjFunction.GetDataView(str).Table;
                    gvItem.Rows.Clear();
                    for (int i = 0; i < dtParty.Rows.Count; i++)
                    {
                        gvItem.Rows.Add();
                        gvItem.Rows[i].Cells[0].Value = i + 1;
                        for (int j = 1; j < gvItem.Columns.Count; j++)
                            gvItem.Rows[i].Cells[j].Value = dtParty.Rows[i].ItemArray[j];

                    }
                    gvItem.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (gvItem.Rows.Count > 0)
                    {
                        gvItem.Focus();
                        gvItem.CurrentCell = gvItem[1, 0];
                    }
                    //gvItem.Visible = true;

                    // new GridSearch(gvParty, 1);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {

                chkPartySelectAll.Checked = !chkPartySelectAll.Checked;

                for (int i = 0; i < gvItem.Rows.Count; i++)
                {
                    gvItem.Rows[i].Cells[3].Value = chkPartySelectAll.Checked;
                }
                BtnShowItem.Focus();

            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else
                    KeyDownFormat(ctrl.Controls);
            }
        }
        #endregion

        private void BtnShowItem_Click(object sender, EventArgs e)
        {
            //pnlSelectType.Visible = true;
                CallReport();
                pnlSelectType.Visible = false;
                FillGrid();
        }

        private void chkPartySelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvItem.Rows.Count; i++)
            {
                gvItem.Rows[i].Cells[3].Value = chkPartySelectAll.Checked;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            pnlPartyDetails.Visible = false;
            btnShow.Focus();
            chkPartySelectAll.Checked = false;
            gvItem.Rows.Clear();
            strLedgerNo = "";
        }

        private void CallReport()
        {

            strLedgerNo = "";
            for (int i = 0; i < gvItem.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvItem.Rows[i].Cells[3].FormattedValue) == true)
                {
                    if (strLedgerNo == "")
                        strLedgerNo = gvItem.Rows[i].Cells[2].Value.ToString();
                    else
                        strLedgerNo = strLedgerNo + "," + gvItem.Rows[i].Cells[2].Value.ToString();
                }
            }
            if (strLedgerNo != "")
            {
                string[] ReportSession;

                ReportSession = new string[4];

                ReportSession[0] = 1.ToString();
                ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = strLedgerNo;


                Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.MarginDetail(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("MarginDetail.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                //pnlItemDetails.Visible = true;

            }
            else
                OMMessageBox.Show("Select Atleast one PartyName ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        }

        private void cmbDepart_KeyDown(object sender, KeyEventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbManu) > 0)
                cmbManu.SelectedIndex = 0;
            
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbDepart) != 0)
                    ObjFunction.FillCombo(cmbCategory, "Select StockGroupNo, StockGroupName from MStockGroup where ControlGroup = 2 and ControlSubGroup = " + ObjFunction.GetComboValue(cmbDepart) + "");
                else
                    ObjFunction.FillCombo(cmbCategory, "Select StockGroupNo, StockGroupName from MStockGroup where ControlGroup = 2 and IsActive='True'");
                cmbCategory.Focus();
            }
        }

        private void cmbDepart_Leave(object sender, EventArgs e)
        {
            cmbDepart_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbManu) > 0)
                cmbManu.SelectedIndex = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (ObjFunction.GetComboValue(cmbCategory) != 0)
                {
                    ObjFunction.FillCombo(cmbBrandName, "Select MItemGroup.StockGroupNo, MItemGroup.StockGroupName from MStockGroup where MItemGroup.StockGroupNo in(Select GroupNo From MStockItems where GroupNo1= " +  ObjFunction.GetComboValue(cmbCategory) +  ") and MItemGroup.IsActive='True' "); 
                }
                else
                    ObjFunction.FillCombo(cmbBrandName, "Select StockGroupNo, StockGroupName from MStockGroup where ControlGroup = 3 and IsActive='True'");
                
                e.SuppressKeyPress = true;
                cmbBrandName.Focus();
            }
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            cmbCategory_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        private void cmbBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbManu) > 0)
                cmbManu.SelectedIndex = 0;

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnShow.Focus();
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridItem(2);

                cmbDepart.SelectedIndex = 0;
                cmbCategory.SelectedIndex = 0;
                cmbBrandName.SelectedIndex = 0;
                cmbManu.SelectedIndex = 0;

                if (DTToDate.Value < DTPFromDate.Value)
                {
                    OMMessageBox.Show("To Date cannot be less than From Date ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    DTPFromDate.Focus();
                    pnlPartyDetails.Visible = false;
                }
                else
                {
                    pnlPartyDetails.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = pnlPartyDetails;
                    // BindGridParty();
                    //pnlPartyDetails.Visible = true;
                    strLedgerNo = "";
                    strItemNo = "";
                    chkPartySelectAll.Checked = false;
                }
                FillGrid();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbManu_KeyDown(object sender, KeyEventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbDepart) > 0 || ObjFunction.GetComboValue(cmbCategory) > 0 || ObjFunction.GetComboValue(cmbBrandName) > 0)
            {
                cmbDepart.SelectedIndex = 0;
                cmbCategory.SelectedIndex = 0;
                cmbBrandName.SelectedIndex = 0;
            }
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnShow.Focus();
            }
        }

        private void DTToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbDepart.Focus();
            }
        }

    }
}