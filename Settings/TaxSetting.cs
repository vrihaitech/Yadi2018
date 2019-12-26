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

namespace Yadi.Settings
{
    public partial class TaxSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMDutiesTaxesInfo dbDutiesTaxesInfo = new DBMDutiesTaxesInfo();
        DBMDutiesTaxesInfo dbItemTaxesInfo = new DBMDutiesTaxesInfo();
        MDutiesTaxesInfo mDutiesTaxesInfo = new MDutiesTaxesInfo();
        MItemTaxInfo mItemTaxesInfo = new MItemTaxInfo();
        //string DutiesTaxesInfoNm;
        DataTable dtSearch = new DataTable();
        int cntRow, Groupno;
        public static long RequestTaxSettingsNo;
        public static long RequestItemTaxSettingsNo;
        DataTable dt = new DataTable();
        //int count = 0;
        public TaxSetting()
        {
            InitializeComponent();
        }

        private void TaxSettingsAE_Load(object sender, EventArgs e)
        {
            bindGrid();
            setpdatagridview(DGPanel);
           
            txtSaleTaxSettingNo.Visible = false;
            label1.Visible = false;

            Groupno = (int)GroupType.DutiesAndTaxes;


            rdTotalSale.Checked = true;
            SetPanels(pnlTotalSale);

            if (TaxSetting.RequestTaxSettingsNo != 0)
            {
                //DutiesTaxesInfoNm = "";
                FillControls();
                dtSearch = ObjFunction.GetDataView("Select LedgerNo From MDutiesTaxesInfo").Table;
                SetNavigation();
                setDisplay(true);
            }
            else
            {
                setDisplay(false);
            }
            KeyDownFormat(this.Controls);
        }

        public void  setpdatagridview(Panel pnl)
        {         
            pnl.Location = new Point(400,5);
            DGPanel.Width = 450;
            DGPanel.Height = 292;
        }

        private void FillControls()
        {
            if (rdTotalSale.Checked == true)
            {


                mDutiesTaxesInfo = dbDutiesTaxesInfo.ModifyMDutiesTaxesInfoByID(TaxSetting.RequestTaxSettingsNo);

                cmbSaleTax.SelectedValue = mDutiesTaxesInfo.LedgerNo.ToString();
                cmbSaleCalcMethod.SelectedValue = mDutiesTaxesInfo.CalculationMethod.ToString();
                dtpSaleFrmDate.Value = mDutiesTaxesInfo.FromDate;
                txtTotSalePercentage.Text = mDutiesTaxesInfo.Percentage.ToString();
            }

            if (rdItemRate.Checked == true)
            {
                bindGrid();
                setpdatagridview(DGPanel);
                mItemTaxesInfo = dbItemTaxesInfo.ModifyMItemTaxInfoByID(TaxSetting.RequestTaxSettingsNo);

                cmbItemTax.SelectedValue = mItemTaxesInfo.TaxLedgerNo.ToString();
                cmbItemCalcMethod.SelectedValue = mItemTaxesInfo.CalculationMethod.ToString();
                cmbItemName.SelectedValue = mItemTaxesInfo.ItemNo.ToString();
                dtpItemFrmDate.Value = mItemTaxesInfo.FromDate;
                txtItemPercentage.Text = mItemTaxesInfo.Percentage.ToString();
            }

            if (rdOtherTaxes.Checked == true)
            {
                bindGrid();
                setpdatagridview(DGPanel);
                mDutiesTaxesInfo = dbDutiesTaxesInfo.ModifyMDutiesTaxesInfoByID(TaxSetting.RequestTaxSettingsNo);

                cmbOtherTax.SelectedValue = mDutiesTaxesInfo.LedgerNo.ToString();
                cmbLedgerTax.SelectedValue = mDutiesTaxesInfo.TaxOnLedgerNo.ToString();
                cmbOtherTaxCalcMethod.SelectedValue = mDutiesTaxesInfo.CalculationMethod.ToString();
                dtpOtherTaxFrmDate.Value = mDutiesTaxesInfo.FromDate;
                txtOtherTaxPercentage.Text = mDutiesTaxesInfo.Percentage.ToString();

            }

            if (rdGrandTotal.Checked == true)
            {
                bindGrid();
                setpdatagridview(DGPanel);
                mDutiesTaxesInfo = dbDutiesTaxesInfo.ModifyMDutiesTaxesInfoByID(TaxSetting.RequestTaxSettingsNo);

                cmbGrandTotTax.SelectedValue = mDutiesTaxesInfo.LedgerNo.ToString();
                cmbGrandTotCalcMethod.SelectedValue = mDutiesTaxesInfo.CalculationMethod.ToString();
                dtpGrandTotFrmDate.Value = mDutiesTaxesInfo.FromDate;
                txtGrandTotPercentage.Text = mDutiesTaxesInfo.Percentage.ToString();
            }
        }

        public void SetValue()
        {
            if (Validations() == true)
            {
                dbDutiesTaxesInfo = new DBMDutiesTaxesInfo();
                dbItemTaxesInfo = new DBMDutiesTaxesInfo();
                mDutiesTaxesInfo = new MDutiesTaxesInfo();
                mItemTaxesInfo = new MItemTaxInfo();
                mDutiesTaxesInfo.LedgerNo = TaxSetting.RequestTaxSettingsNo;
                mItemTaxesInfo.TaxLedgerNo = TaxSetting.RequestTaxSettingsNo;

                if (rdTotalSale.Checked == true)
                {
                    mDutiesTaxesInfo.LedgerNo = ObjFunction.GetComboValue(cmbSaleTax);
                    mDutiesTaxesInfo.Percentage = Convert.ToInt64(txtTotSalePercentage.Text.Trim());
                    mDutiesTaxesInfo.FromDate = dtpSaleFrmDate.Value;

                    mDutiesTaxesInfo.CalculationMethod = ObjFunction.GetComboValue(cmbSaleCalcMethod).ToString();
                    mDutiesTaxesInfo.CompanyNo = DBGetVal.FirmNo;
                    mDutiesTaxesInfo.UserID = DBGetVal.UserID;
                    mDutiesTaxesInfo.UserDate = DBGetVal.ServerTime.Date;

                    if (dbDutiesTaxesInfo.AddMDutiesTaxesInfo(mDutiesTaxesInfo) == true)
                    {
                        OMMessageBox.Show("Tax Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //this.Close();
                        bindGrid();
                        setpdatagridview(DGPanel);
                        
                    }
                    else
                    {
                        OMMessageBox.Show("Tax not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }

                if (rdItemRate.Checked == true)
                {
                    mItemTaxesInfo.TaxLedgerNo = ObjFunction.GetComboValue(cmbItemTax);
                    mItemTaxesInfo.SalesLedgerNo = ObjFunction.GetComboValue(cmbSalesAccount);
                    mItemTaxesInfo.CalculationMethod = ObjFunction.GetComboValue(cmbItemCalcMethod).ToString();
                    mItemTaxesInfo.ItemNo = ObjFunction.GetComboValue(cmbItemName);
                    mItemTaxesInfo.FromDate = dtpItemFrmDate.Value;
                    mItemTaxesInfo.Percentage = Convert.ToInt64(txtItemPercentage.Text.Trim());
                    mItemTaxesInfo.CompanyNo = DBGetVal.FirmNo;
                    mItemTaxesInfo.UserID = DBGetVal.UserID;
                    mItemTaxesInfo.UserDate = DBGetVal.ServerTime.Date;

                    if (dbItemTaxesInfo.AddMItemTaxInfo(mItemTaxesInfo) == true)
                    {
                        OMMessageBox.Show("Tax Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        bindGrid();
                        setpdatagridview(DGPanel);
                    }
                    else
                    {
                        OMMessageBox.Show("Tax not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }

                if (rdOtherTaxes.Checked == true)
                {
                    mDutiesTaxesInfo.LedgerNo = ObjFunction.GetComboValue(cmbOtherTax);
                    mDutiesTaxesInfo.TaxOnLedgerNo = ObjFunction.GetComboValue(cmbLedgerTax);
                    mDutiesTaxesInfo.Percentage = Convert.ToInt64(txtOtherTaxPercentage.Text.Trim());
                    mDutiesTaxesInfo.FromDate = dtpOtherTaxFrmDate.Value;

                    mDutiesTaxesInfo.CalculationMethod = ObjFunction.GetComboValue(cmbOtherTaxCalcMethod).ToString();
                    mDutiesTaxesInfo.CompanyNo = DBGetVal.FirmNo;
                    mDutiesTaxesInfo.UserID = DBGetVal.UserID;
                    mDutiesTaxesInfo.UserDate = DBGetVal.ServerTime.Date;

                    if (dbDutiesTaxesInfo.AddMDutiesTaxesInfo(mDutiesTaxesInfo) == true)
                    {
                        OMMessageBox.Show("Tax Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        bindGrid();
                        setpdatagridview(DGPanel);
                    }
                    else
                    {
                        OMMessageBox.Show("Tax not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }

                if (rdGrandTotal.Checked == true)
                {
                    mDutiesTaxesInfo.LedgerNo = ObjFunction.GetComboValue(cmbGrandTotTax);
                    mDutiesTaxesInfo.Percentage = Convert.ToInt64(txtGrandTotPercentage.Text.Trim());
                    mDutiesTaxesInfo.FromDate = dtpGrandTotFrmDate.Value;

                    mDutiesTaxesInfo.CalculationMethod = ObjFunction.GetComboValue(cmbGrandTotCalcMethod).ToString();
                    mDutiesTaxesInfo.CompanyNo = DBGetVal.FirmNo;
                    mDutiesTaxesInfo.UserID = DBGetVal.UserID;
                    mDutiesTaxesInfo.UserDate = DBGetVal.ServerTime.Date;

                    if (dbDutiesTaxesInfo.AddMDutiesTaxesInfo(mDutiesTaxesInfo) == true)
                    {
                        OMMessageBox.Show("Tax Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        bindGrid();
                        setpdatagridview(DGPanel);
                    }
                    else
                    {
                        OMMessageBox.Show("Tax not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                

                
            }
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtTotSalePercentage, "");

            if (rdTotalSale.Checked == true)
            {
                if (txtTotSalePercentage.Text.Trim() == "")
                {

                    EP.SetError(txtTotSalePercentage, "Enter Percentage");
                    EP.SetIconAlignment(txtTotSalePercentage, ErrorIconAlignment.MiddleRight);
                    txtTotSalePercentage.Focus();
                }
                else
                    flag = true;
            }

            if (rdItemRate.Checked == true)
            {
                if (txtItemPercentage.Text.Trim() == "")
                {

                    EP.SetError(txtItemPercentage, "Enter Percentage");
                    EP.SetIconAlignment(txtItemPercentage, ErrorIconAlignment.MiddleRight);
                    txtItemPercentage.Focus();
                }
                else
                    flag = true;
            }

            if (rdOtherTaxes.Checked == true)
            {
                if (txtOtherTaxPercentage.Text.Trim() == "")
                {

                    EP.SetError(txtOtherTaxPercentage, "Enter Percentage");
                    EP.SetIconAlignment(txtOtherTaxPercentage, ErrorIconAlignment.MiddleRight);
                    txtOtherTaxPercentage.Focus();
                }
                else
                    flag = true;
            }

            if (rdGrandTotal.Checked == true)
            {
                if (txtGrandTotPercentage.Text.Trim() == "")
                {

                    EP.SetError(txtGrandTotPercentage, "Enter Percentage");
                    EP.SetIconAlignment(txtGrandTotPercentage, ErrorIconAlignment.MiddleRight);
                    txtGrandTotPercentage.Focus();
                }
                else
                    flag = true;
            }

            return flag;
        }

        private void rdTotalSale_CheckedChanged(object sender, EventArgs e)
        {
            rdSelectedChanged();
            bindGrid();
            setpdatagridview(DGPanel);
        }

        private void rdItemRate_CheckedChanged(object sender, EventArgs e)
        {
            rdSelectedChanged();
            bindGrid();
            setpdatagridview(DGPanel);
        }

        private void rdOtherTaxes_CheckedChanged(object sender, EventArgs e)
        {
            rdSelectedChanged();
        }

        private void rdGrandTotal_CheckedChanged(object sender, EventArgs e)
        {
            rdSelectedChanged();
        }

        public void SetPanels(Panel pnl)
        {
            pnlGrandTotal.Visible = false;
            pnlTotalSale.Visible = false;
            pnlItemRate.Visible = false;
            pnlOtherTaxes.Visible = false;
            pnl.Visible = true;
            pnl.Location = new Point(14, 68);
            pnlMain.Width = 373;
            pnlMain.Height = 292;
        }

        public void rdSelectedChanged()
        {
            if (rdTotalSale.Checked == true)
            {
                SetPanels(pnlTotalSale);
                ObjFunction.FillCombo(cmbSaleTax, "Select LedgerNo,LedgerName From MLedger where (GroupNo = " + GroupType.DutiesAndTaxes + " or GroupNo in (Select ControlGroup From MGroup Where GroupNo = " + GroupType.DutiesAndTaxes + ")) and LedgerNo not in (Select LedgerNo From MDutiesTaxesInfo Where CalculationMethod = 1)");
                ObjFunction.FillComb(cmbSaleCalcMethod, "Select CalculationMethodNo,CalculationMethod from MTaxCalculationMethod where CalculationMethodNo = 1");
                cmbSaleTax.Focus();
                EP.SetError(txtTotSalePercentage, "");
            }
            else if (rdItemRate.Checked == true)
            {
                SetPanels(pnlItemRate);
              //  ObjFunction.FillCombo(cmbItemTax, "Select LedgerNo,LedgerName From MLedger where (GroupNo = 20 or GroupNo in (Select ControlGroup From MGroup Where GroupNo = 20)) and LedgerNo not in (Select LedgerNo From MItemTaxInfo Where CalculationMethod = 2)");
                ObjFunction.FillCombo(cmbSalesAccount, "SELECT LedgerNo, LedgerName FROM MLedger WHERE GroupNo in("+GroupType.SalesAccount+","+GroupType.PurchaseAccount+")");
                ObjFunction.FillCombo(cmbTextType, "SELECT GroupNo, GroupName FROM MGroup  WHERE (ControlGroup = " + GroupType.DutiesAndTaxes + ")");
                                
                ObjFunction.FillComb(cmbItemCalcMethod, "Select CalculationMethodNo,CalculationMethod from MTaxCalculationMethod where CalculationMethodNo = 2");
                ObjFunction.FillCombo(cmbItemName, "Select ItemNo,ItemName From MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo<>1 AND IsActive='true' order by ItemName");
                cmbTextType.Focus();
                EP.SetError(txtTotSalePercentage, "");
            }
            else if (rdOtherTaxes.Checked == true)
            {
                SetPanels(pnlOtherTaxes);
                ObjFunction.FillCombo(cmbOtherTax, "Select LedgerNo,LedgerName From MLedger where (GroupNo = " + GroupType.DutiesAndTaxes + " or GroupNo in (Select ControlGroup From MGroup Where GroupNo = " + GroupType.DutiesAndTaxes + ")) and LedgerNo not in (Select LedgerNo From MDutiesTaxesInfo " +
                                                   " Where CalculationMethod = 3) and LedgerNo not in (Select TaxOnLedgerNo From MDutiesTaxesInfo Where CalculationMethod = 3) and LedgerNo not in (Select LedgerNo From MDutiesTaxesInfo Where CalculationMethod = 1)");
                ObjFunction.FillCombo(cmbLedgerTax, "Select LedgerNo,LedgerName From MLedger where LedgerNo in (Select LedgerNo From MDutiesTaxesInfo Where CalculationMethod = 1)");
                ObjFunction.FillComb(cmbOtherTaxCalcMethod, "Select CalculationMethodNo,CalculationMethod from MTaxCalculationMethod where CalculationMethodNo = 3");
                cmbOtherTax.Focus();
                EP.SetError(txtTotSalePercentage, "");
            }
            else if (rdGrandTotal.Checked == true)
            {
                SetPanels(pnlGrandTotal);
                ObjFunction.FillCombo(cmbGrandTotTax, "Select LedgerNo,LedgerName From MLedger where (GroupNo = " + GroupType.DutiesAndTaxes + " or GroupNo in (Select ControlGroup From MGroup Where GroupNo = " + GroupType.DutiesAndTaxes + ")) and LedgerNo not in (Select LedgerNo From MDutiesTaxesInfo )");
                ObjFunction.FillComb(cmbGrandTotCalcMethod, "Select CalculationMethodNo,CalculationMethod from MTaxCalculationMethod where CalculationMethodNo = 4");
                              
                cmbGrandTotTax.Focus();
                EP.SetError(txtTotSalePercentage, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        private void TaxSettingsAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            TaxSetting.RequestTaxSettingsNo = 0;
            
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            long No = 0;

            if (type == 1)
            {
                No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                cntRow = 0;
                TaxSetting.RequestTaxSettingsNo = No;
            }
            else if (type == 2)
            {
                No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                cntRow = dtSearch.Rows.Count - 1;
                TaxSetting.RequestTaxSettingsNo = No;
            }
            else
            {
                if (type == 3)
                {
                    cntRow = cntRow + 1;
                }
                else if (type == 4)
                {
                    cntRow = cntRow - 1;
                }

                if (cntRow < 0)
                {
                    OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    cntRow = cntRow + 1;
                }
                else if (cntRow > dtSearch.Rows.Count - 1)
                {
                    OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    cntRow = cntRow - 1;
                }
                else
                {
                    No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    TaxSetting.RequestTaxSettingsNo = No;
                }

            }


            FillControls();

        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == TaxSetting.RequestTaxSettingsNo)
                {
                    cntRow = i;
                    break;
                }
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }

        

        #endregion

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
            if (e.KeyCode == Keys.Left && e.Control)
            {
                if (btnPrev.Enabled) btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (btnFirst.Enabled) btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                if (btnNext.Enabled) btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (btnLast.Enabled) btnLast_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (btnSave.Visible) btnSave_Click(sender, e);
            }
            //else if (e.KeyCode == Keys.Escape)
            //{
            //    btnExit_Click(sender, e);
            //}
        }
        #endregion

        private void btndelete_Click(object sender, EventArgs e)
        {
            dbDutiesTaxesInfo = new DBMDutiesTaxesInfo();
            mDutiesTaxesInfo = new MDutiesTaxesInfo();

            mDutiesTaxesInfo.LedgerNo = TaxSetting.RequestTaxSettingsNo;
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dbDutiesTaxesInfo.DeleteMDutiesTaxesInfo(mDutiesTaxesInfo) == true)
                {
                    OMMessageBox.Show("Tax Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    Form NewF = new TaxSetting();
                    this.Close();
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                {
                    OMMessageBox.Show("Tax not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            TaxSetting.RequestTaxSettingsNo = 0;
            //Form NewF = new TaxSettingsAE();
            this.Close();
            //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void txtTotSalePercentage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {

                btnSave.Focus();
            }
           

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        public void bindGrid()
        {
            if (rdTotalSale.Checked == true)
            {
                string sql;
                sql = "Select 0 As Sr, MLedger.LedgerName AS SelectTax,MTaxCalculationMethod.CalculationMethod , MDutiesTaxesInfo.FromDate AS FromDate, MDutiesTaxesInfo.Percentage AS Percentage " +
                    " FROM         MDutiesTaxesInfo INNER JOIN " +
                    " MLedger ON MDutiesTaxesInfo.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                    " MTaxCalculationMethod ON MDutiesTaxesInfo.CalculationMethod = MTaxCalculationMethod.CalculationMethodNo " +
                    " WHERE     (MDutiesTaxesInfo.CalculationMethod = 1) ";
                dt = ObjFunction.GetDataView(sql).Table;
                
                dgv.DataSource = dt;
            }
            if (rdItemRate.Checked == true)
            {
                string sql;
                
                sql = "SELECT     0 AS SR, MLedger.LedgerName AS SelectTax, MTaxCalculationMethod.CalculationMethod, mItemMaster.ItemName, MItemTaxInfo.FromDate, " +
                    " MItemTaxInfo.Percentage " +
                    " FROM  MLedger INNER JOIN " +
                    " MItemTaxInfo ON MLedger.LedgerNo = MItemTaxInfo.TaxLedgerNo INNER JOIN " +
                    " MTaxCalculationMethod ON MItemTaxInfo.CalculationMethod = MTaxCalculationMethod.CalculationMethodNo INNER JOIN " +
                    " MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON MItemTaxInfo.ItemNo = mItemMaster.ItemNo " +
                    " WHERE     (MItemTaxInfo.CalculationMethod = 2) AND (MStockItems.IsActive='true') ";
                dt = ObjFunction.GetDataView(sql).Table;

                dgv.DataSource = dt;
            }
            if (rdOtherTaxes.Checked == true)
            {
                string sql;
                sql = " SELECT (0+1) As SR, MLedger.LedgerName AS SelectTax,MTaxCalculationMethod.CalculationMethod , MDutiesTaxesInfo.FromDate AS FromDate, MDutiesTaxesInfo.Percentage AS Percentage " +
                    " FROM         MDutiesTaxesInfo INNER JOIN " +
                    " MLedger ON MDutiesTaxesInfo.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                    " MTaxCalculationMethod ON MDutiesTaxesInfo.CalculationMethod = MTaxCalculationMethod.CalculationMethodNo " +
                    " WHERE     (MDutiesTaxesInfo.CalculationMethod = 3) ";
                dt = ObjFunction.GetDataView(sql).Table;
                dgv.DataSource = dt;
            }

            if (rdGrandTotal.Checked == true)
            { 
            }

         
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                
                e.Value = (e.RowIndex + 1).ToString();
            }
            DataGridViewColumn column = dgv.Columns[0];
            column.Width = 40;
            DataGridViewColumn column1 = dgv.Columns[1];
            column1.Width = 120;
            DataGridViewColumn column2 = dgv.Columns[2];
            column2.Width = 150;
            DataGridViewColumn column3 = dgv.Columns[3];
            column3.Width = 80;
            DataGridViewColumn column4 = dgv.Columns[4];
            column4.Width = 50;
        }

        private void dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
           
        }

        private void cmbTextType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbItemTax, "SELECT LedgerNo, LedgerName FROM MLedger WHERE    GroupNo = '" + ObjFunction.GetComboValue(cmbTextType) + "'");
         
        }

       

        
    }
}
