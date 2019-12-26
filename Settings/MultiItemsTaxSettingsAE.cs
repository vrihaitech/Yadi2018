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
    public partial class MultiItemsTaxSettingsAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataTable dt1 = new DataTable();
        DataView dv1 = new DataView();

        public MultiItemsTaxSettingsAE()
        {
            InitializeComponent();
        }

        long SalesTaxType = 32;

        public MultiItemsTaxSettingsAE(long iSalesTaxType)
        {
            InitializeComponent();
            SalesTaxType = iSalesTaxType;
        }

        private void MultiItemsTaxSettings_Load(object sender, EventArgs e)
        {
            lblWait.Font = new Font("Verdana", 14, FontStyle.Bold);
            lblWait.ForeColor = Color.Green;
            //ObjFunction.FillCombo(cmbGroupNo1, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 ORDER BY StockGroupName");
            ObjFunction.FillCombo(cmbGroupNo2, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
            //ObjFunction.FillComb(cmbGroupNo2, "GroupNo", "StockGroupName");
            ObjFunction.FillCombo(cmbDepartmentName, "SELECT DISTINCT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4 order by StockGroupName");
            ObjFunction.FillCombo(cmbCategoryName, "SELECT DISTINCT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 order By StockGroupName");
            ObjFunction.FillCombo(cmbVatSales, "SELECT DISTINCT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)) as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                    " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = " + SalesTaxType + ") And MItemTaxSetting.IsActive='True'  Order by  Percentage ");
            ObjFunction.FillCombo(cmbVatPurchase, "SELECT DISTINCT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)) as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                   "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                   " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = " + SalesTaxType + ") And MItemTaxSetting.IsActive='True' Order by  Percentage ");
            dgvTaxItem.ColumnHeadersDefaultCellStyle.Font = GetFont();
            dgvTaxItem.Columns[ColIndex.SrNo].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTaxItem.Columns[ColIndex.SVat].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvTaxItem.Columns[ColIndex.PVat].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            rdBoth.Checked = true;           
            new GridSearch(dgvTaxItem, ColIndex.ItemName, 2);
            KeyDownFormat(this.Controls);           
            pnlrb.Visible = false;
        }

        private Font GetFont()
        {
            return new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
        }
     

        private void BindGrid1(int type)
        {
            dt1.Clear();
            string sql = "";

            sql = " SELECT DISTINCT 0 as SrNo,MStockBarcode.Barcode, MItemGroup.StockGroupName + ' ' + mItemMaster.ItemName AS ItemName, " +
                          " ISNULL(MItemTaxInfo_S.Percentage, NULL) AS SVat, " +
                          " ISNULL(MItemTaxInfo_P.Percentage, NULL) AS PVat, " +
                          " ISNULL (MItemTaxInfo_S.PkSrNo, 0) AS SalesLedgerNo, " +
                          " ISNULL (MItemTaxInfo_P.PkSrNo, 0) AS PurLedgerNo, " +
                          " mItemMaster.ItemNo " +
                          " FROM " +
                          " MStockItems INNER JOIN  " +
                          " MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo INNER JOIN " +
                          " MStockBarcode ON MStockBarcode.ItemNo = mItemMaster.ItemNo LEFT OUTER JOIN " +
                          " ( SELECT MItemTaxInfo_S.Percentage, MItemTaxInfo_S.PkSrNo, MItemTaxInfo_S.ItemNo FROM " +
                          " MItemTaxInfo AS MItemTaxInfo_S INNER JOIN " +
                          " MLedger AS MLedger_S ON MItemTaxInfo_S.SalesLedgerNo = MLedger_S.LedgerNo AND MLedger_S.GroupNo = 10 INNER JOIN " +
                          " MLedger AS MLedger_ST ON MItemTaxInfo_S.TaxLedgerNo = MLedger_ST.LedgerNo AND MLedger_ST.GroupNo = " + SalesTaxType + " " +
                          " ) MItemTaxInfo_S ON MItemTaxInfo_S.ItemNo = mItemMaster.ItemNo LEFT OUTER JOIN " +
                          " ( SELECT MItemTaxInfo_P.Percentage, MItemTaxInfo_P.PkSrNo, MItemTaxInfo_P.ItemNo FROM " +
                          " MItemTaxInfo AS MItemTaxInfo_P INNER JOIN " +
                          " MLedger AS MLedger_P ON MItemTaxInfo_P.SalesLedgerNo = MLedger_P.LedgerNo AND MLedger_P.GroupNo = 11 INNER JOIN " +
                          " MLedger AS MLedger_PT ON MItemTaxInfo_P.TaxLedgerNo = MLedger_PT.LedgerNo AND MLedger_PT.GroupNo = " + SalesTaxType + " " +
                          " ) MItemTaxInfo_P ON MItemTaxInfo_P.ItemNo = mItemMaster.ItemNo " +
                          " WHERE     (MStockItems.ItemNo <> 1) ";
            
                    //sql = " SELECT DISTINCT 0 as SrNo,GetItemInfo.Barcode, GetItemInfo.ItemName, ISNULL "+
                    //      " ((SELECT MItemTaxInfo_3.Percentage  FROM MItemTaxInfo AS MItemTaxInfo_3 INNER JOIN "+
                    //      " MLedger ON MItemTaxInfo_3.SalesLedgerNo = MLedger.LedgerNo INNER JOIN "+
                    //      " MLedger AS MLedger_1 ON MItemTaxInfo_3.TaxLedgerNo = MLedger_1.LedgerNo "+
            //      " WHERE  (MLedger.GroupNo = 10) AND (MLedger_1.GroupNo = " + SalesTaxType + ") AND (MItemTaxInfo_3.ItemNo = mItemMaster.ItemNo)), 0) AS SVat, ISNULL "+
                    //      " ((SELECT MItemTaxInfo.Percentage FROM MItemTaxInfo AS MItemTaxInfo INNER JOIN "+
                    //      " MLedger AS MLedger_2 ON MItemTaxInfo.SalesLedgerNo = MLedger_2.LedgerNo INNER JOIN "+
                    //      " MLedger AS MLedger_1 ON MItemTaxInfo.TaxLedgerNo = MLedger_1.LedgerNo "+
            //      " WHERE     (MLedger_2.GroupNo = 11) AND (MLedger_1.GroupNo = " + SalesTaxType + ") AND (MItemTaxInfo.ItemNo = mItemMaster.ItemNo)), 0) AS PVat, ISNULL "+
                    //      " ((SELECT MItemTaxInfo_2.PkSrNo FROM MItemTaxInfo AS MItemTaxInfo_2 INNER JOIN "+
                    //      " MLedger AS MLedger_3 ON MItemTaxInfo_2.SalesLedgerNo = MLedger_3.LedgerNo INNER JOIN "+
                    //      " MLedger AS MLedger_1 ON MItemTaxInfo_2.TaxLedgerNo = MLedger_1.LedgerNo "+
            //      " WHERE     (MLedger_3.GroupNo = 10) AND (MLedger_1.GroupNo = " + SalesTaxType + ") AND (MItemTaxInfo_2.ItemNo = mItemMaster.ItemNo)), 0) AS SalesLedgerNo, "+
                    //      " ISNULL((SELECT     MItemTaxInfo.PkSrNo FROM MItemTaxInfo AS MItemTaxInfo INNER JOIN " +
                    //      " MLedger AS MLedger_2 ON MItemTaxInfo.SalesLedgerNo = MLedger_2.LedgerNo INNER JOIN MLedger AS MLedger_1 ON MItemTaxInfo.TaxLedgerNo = MLedger_1.LedgerNo "+
            //      " WHERE     (MLedger_2.GroupNo = 11) AND (MLedger_1.GroupNo = " + SalesTaxType + ") AND (MItemTaxInfo.ItemNo = mItemMaster.ItemNo)), 0) AS PurLedgerNo, "+
                    //      " mItemMaster.ItemNo FROM MItemTaxInfo AS MItemTaxInfo_1 INNER JOIN MStockItems ON MItemTaxInfo_1.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                    //      " MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo INNER JOIN GetItemInfo ON mItemMaster.ItemNo = GetItemInfo.ItemNo "+
                    //      " WHERE     (MStockItems.ItemNo <> 1)";

                    //" SELECT     ISNULL(t.PkSrNo, 0) AS PkSrNo,i.Barcode, i.ItemName, MGroup.GroupName AS TaxType, MLedger_1.LedgerName AS SalesAccount,  " +
                    //" MLedger.LedgerName AS TaxAccount, MTaxCalculationMethod.CalculationMethod AS CalculationMethodName, t.Percentage, i.CompanyName, t.FromDate, i.Group1Name, " +
                    //" i.Group2Name, i.DepartmentName, i.CategoryName, i.ItemNo, t.TaxLedgerNo, t.SalesLedgerNo, t.CalculationMethod AS CalculationMethodNo " +
                    //" FROM (SELECT     PkSrNo, ItemNo, TaxLedgerNo, SalesLedgerNo, FromDate, CalculationMethod, Percentage, CompanyNo FROM dbo.GetItemTaxAll(NULL, NULL, " + dgvItemTax[21, i].Value + ",NULL,NULL) ) AS t INNER JOIN " +
                    //" MLedger ON t.TaxLedgerNo = MLedger.LedgerNo INNER JOIN MLedger AS MLedger_1 ON t.SalesLedgerNo = MLedger_1.LedgerNo INNER JOIN MTaxCalculationMethod ON t.CalculationMethod = MTaxCalculationMethod.CalculationMethodNo INNER JOIN " +
                    //" MGroup ON MLedger.GroupNo = MGroup.GroupNo RIGHT OUTER JOIN GetItemInfo AS i ON t.ItemNo = i.ItemNo " +
                    //"Where (i.ItemNo <> 1) And i.ItemNo Not In(" + dgvItemTax[16, i].Value + ") ";
                                 

            string StrWhere = "";
            //if (ObjFunction.GetComboValue(cmbGroupNo1) > 0)
            //{
            //    StrWhere += " And Group2Name ='" + cmbGroupNo1.Text + "' ";
            //}
            if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
            {
                StrWhere += " And mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " ";
            }
            if (ObjFunction.GetComboValue(cmbCategoryName) > 0)
            {
                StrWhere += " And mItemMaster.GroupNo1 =" + ObjFunction.GetComboValue(cmbCategoryName) + " ";
            }
            if (ObjFunction.GetComboValue(cmbDepartmentName) > 0)
            {
                StrWhere += " And mItemMaster.FKStockDeptNo =" + ObjFunction.GetComboValue(cmbDepartmentName) + " ";
            }
            if (txtBarcode.Text.Trim() != "")
            {
                StrWhere += " And MStockBarcode.Barcode = '" + txtBarcode.Text.Trim() + "' ";
            }
            //if (type == 1)
            //{
            //    StrWhere += " And MGroup.GroupName is not null ";//Assigned Data
            //}
            //else if (type == 2)
            //{
            //    StrWhere += " And MGroup.GroupName is null ";//Not Assigned Data
            //}
            sql += StrWhere + " order by ItemName,Barcode";
            try
            {
                if (StrWhere != "")
                {
                    //DisplayMessageForWait(true);
                    dt1 = ObjFunction.GetDataView(sql).Table;
                }
                dgvTaxItem.DataSource = dt1.DefaultView;
                if (dt1.Rows.Count > 0)
                    pnlTAx.Visible = true;
                else
                    pnlTAx.Visible = false;
            }
            catch (Exception e)
            {
                CommonFunctions.ErrorMessge = e.Message;
            }
            finally
            {
                //DisplayMessageForWait(false);
                this.Cursor = Cursors.Default;
            }
        }

        private void dgvItemTax_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }

        }

        private void cmbGroupNo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Validation();
                
            }
        }

        private void dgvTaxItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }       

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvTaxItem.Rows.Count; i++)
            {
                dgvTaxItem.Rows[i].Cells[0].Value = chkSelectAll.Checked;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
            cmbCategoryName.Enabled = true;
            cmbDepartmentName.Enabled = true;
        }

        public void ClearFields()
        {
            dt.Clear();
            dt1.Clear();

            EP.SetError(cmbGroupNo2, "");
            EP.SetError(cmbCategoryName, "");
            EP.SetError(cmbDepartmentName, "");
            chkSelectAll.Checked = false;           
            txtBarcode.Text = "";
            ObjFunction.FillCombo(cmbGroupNo2, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
            ObjFunction.FillCombo(cmbDepartmentName, "SELECT Distinct StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4 order by StockGroupName");
            ObjFunction.FillCombo(cmbCategoryName, "SELECT Distinct StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 order By StockGroupName");
            cmbGroupNo2.SelectedIndex = 0;
            cmbCategoryName.SelectedIndex = 0;
            cmbDepartmentName.SelectedIndex = 0;
            cmbVatPurchase.SelectedIndex = 0;
            cmbVatSales.SelectedIndex = 0;

            rdBoth.Checked = true;
            dgvTaxItem.DataSource = dt1.DefaultView;
            pnlrb.Visible = false;
            pnlTAx.Visible = false;
            cmbGroupNo2.Focus();
        }

        private void btnSetTax_Click(object sender, EventArgs e)
        {
            if (Validations() == true)
            {
                SetValue();
            }
        }

        public bool Validations()
        {
            bool flag = true;
            EP.SetError(cmbVatPurchase, "");
            EP.SetError(cmbVatSales, "");
            if (ObjFunction.GetComboValue(cmbVatSales) <= 0 && ObjFunction.GetComboValue(cmbVatPurchase) <= 0)
            {
                DisplayMessage("Please Select atleast one Tax....");
                flag = false;
            }          

            return flag;
        }

        public void SetValue()
        {
            bool flag = false,SFlag=false;
            DataTable dt = new DataTable();
            DBMDutiesTaxesInfo dbMDutiesTaxesInfo = new DBMDutiesTaxesInfo();
            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();
            Application.DoEvents();
            //DisplayMessageForWait(true);
            this.Cursor = Cursors.WaitCursor;
            try
            {
                for (int i = 0; i < dgvTaxItem.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgvTaxItem[0, i].Value) == true)
                    {
                        if (ObjFunction.GetComboValue(cmbVatSales) > 0)
                        {
                            // DataTable dtItemTaxSales = ObjFunction.GetDataView("Select TaxLedgerNo,SalesLedgerNo,CalculationMethod From dbo.GetItemTaxAll(" + Convert.ToInt64(dgvTaxItem[ColIndex.ItemNo, i].Value) + ", NULL, " + GroupType.SalesAccount + ",NULL,NULL)").Table;
                            DataTable dtItemTaxSales = ObjFunction.GetDataView("Select TaxLedgerNo,SalesLedgerNo,CalculationMethod,Percentage From MItemTaxSetting Where PkSrNo in (" + ObjFunction.GetComboValue(cmbVatSales) + ")").Table;
                            if (dtItemTaxSales.Rows.Count > 0)
                            {
                                dbMDutiesTaxesInfo = new DBMDutiesTaxesInfo();
                                mItemTaxInfo = new MItemTaxInfo();
                                flag = true;
                                mItemTaxInfo.PkSrNo = Convert.ToInt64(dgvTaxItem[ColIndex.SaleLedgPk, i].Value);
                                mItemTaxInfo.ItemNo = Convert.ToInt64(dgvTaxItem[ColIndex.ItemNo, i].Value);
                                mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(dtItemTaxSales.Rows[0].ItemArray[0].ToString());//Convert.ToInt64(dgvItemTax[17, j].Value)
                                mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(dtItemTaxSales.Rows[0].ItemArray[1].ToString());
                                mItemTaxInfo.FromDate = DBGetVal.ServerTime;
                                mItemTaxInfo.Percentage = Convert.ToDouble(dtItemTaxSales.Rows[0].ItemArray[3].ToString());
                                mItemTaxInfo.CalculationMethod = dtItemTaxSales.Rows[0].ItemArray[2].ToString();
                                mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                                mItemTaxInfo.FKTaxSettingNo = ObjFunction.GetComboValue(cmbVatSales);
                                mItemTaxInfo.UserID = DBGetVal.UserID;
                                mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                                dbMDutiesTaxesInfo.AddMItemTaxInfo1(mItemTaxInfo);
                            }
                        }

                        if (ObjFunction.GetComboValue(cmbVatPurchase) > 0)
                        {
                            //DataTable dtItemTaxPur = ObjFunction.GetDataView("Select TaxLedgerNo,SalesLedgerNo,CalculationMethod From dbo.GetItemTaxAll(" + Convert.ToInt64(dgvTaxItem[ColIndex.ItemNo, i].Value) + ", NULL, " + GroupType.PurchaseAccount + ",NULL,NULL)").Table;
                            DataTable dtItemTaxPur = ObjFunction.GetDataView("Select TaxLedgerNo,SalesLedgerNo,CalculationMethod,Percentage From MItemTaxSetting Where PkSrNo in (" + ObjFunction.GetComboValue(cmbVatPurchase) + ")").Table;
                            if (dtItemTaxPur.Rows.Count > 0)
                            {
                                mItemTaxInfo = new MItemTaxInfo();
                                flag = true;
                                mItemTaxInfo.PkSrNo = Convert.ToInt64(dgvTaxItem[ColIndex.PurLedgPk, i].Value);
                                mItemTaxInfo.ItemNo = Convert.ToInt64(dgvTaxItem[ColIndex.ItemNo, i].Value);
                                mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(dtItemTaxPur.Rows[0].ItemArray[0].ToString());//Convert.ToInt64(dgvItemTax[17, j].Value)
                                mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(dtItemTaxPur.Rows[0].ItemArray[1].ToString());
                                mItemTaxInfo.FromDate = DBGetVal.ServerTime;
                                mItemTaxInfo.Percentage = Convert.ToDouble(dtItemTaxPur.Rows[0].ItemArray[3].ToString());
                                mItemTaxInfo.CalculationMethod = dtItemTaxPur.Rows[0].ItemArray[2].ToString();
                                mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                                mItemTaxInfo.FKTaxSettingNo = ObjFunction.GetComboValue(cmbVatPurchase);
                                mItemTaxInfo.UserID = DBGetVal.UserID;
                                mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                                dbMDutiesTaxesInfo.AddMItemTaxInfo1(mItemTaxInfo);


                            }
                        }
                        if (dbMDutiesTaxesInfo.ExecuteNonQueryStatements() == true && flag == true)
                        {
                            SFlag = true;
                        }
                    }
                }
                if (SFlag == true)
                {
                    OMMessageBox.Show("Tax Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    rdBoth.Checked = true;
                    ClearFields();
                    cmbCategoryName.Enabled = true;
                    cmbDepartmentName.Enabled = true;
                }
                else
                {
                    if (flag == false)
                        OMMessageBox.Show("Select Atleast One Checkbox", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    else
                        OMMessageBox.Show("Tax not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            finally
            {
                //DisplayMessageForWait(false);
                this.Cursor = Cursors.Default;
            }
        }

        public static class ColIndex
        {
            public static int Select = 0;
            public static int SrNo = 1;
            public static int Barcode = 2;
            public static int ItemName = 3;
            public static int SVat = 4;
            public static int PVat = 5;
            public static int SaleLedgPk = 6;
            public static int PurLedgPk = 7;
            public static int ItemNo = 8;

        }
      
        private void txtSelectItem_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void rd_Click(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            if (rd.Checked == true)
            {
                if (rd.Name == "rdBoth")
                {
                    BindGrid1(0);
                }
                else if (rd.Name == "rdAssigned")
                {
                    BindGrid1(1);
                }
                else if (rd.Name == "rdNotAssigned")
                {
                    BindGrid1(2);
                }
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
                chkSelectAll.Checked = !chkSelectAll.Checked;
            }
        }
        #endregion

        public void Validation()
        {
            bool flag = false;
             if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
            {
                flag = true;
            }
            else if (ObjFunction.GetComboValue(cmbCategoryName) > 0)
            {
                flag = true;
            }
            else if (ObjFunction.GetComboValue(cmbDepartmentName) > 0)
            {
                flag = true;
            }

            if (flag == false)
            {
                DisplayMessage("Please Select atleast one group....");
                while (dgvTaxItem.Rows.Count > 0)
                    dgvTaxItem.Rows.RemoveAt(0);
            }
            else
            {
                BindGrid1(0);
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
              
                EP.SetError(cmbGroupNo2, "");
                EP.SetError(cmbCategoryName, "");
                EP.SetError(cmbDepartmentName, "");               
                EP.SetError(txtBarcode, "");
                e.SuppressKeyPress = true;
                if (txtBarcode.Text.Trim() != "")
                {
                    BindGrid1(0);
                    if (dgvTaxItem.Rows.Count > 0)
                    {
                        dgvTaxItem.CurrentCell = dgvTaxItem[0, 0];
                        dgvTaxItem.Focus();
                    }
                }
                else
                {
                    if (cmbDepartmentName.Enabled == true)
                        cmbDepartmentName.Focus();
                    else
                    {
                        if (dgvTaxItem.Rows.Count > 0)
                        {
                            dgvTaxItem.CurrentCell = dgvTaxItem[0, 0];
                            dgvTaxItem.Focus();
                        }
                    }
                }
            }
        }

        private void dgvTaxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                cmbVatSales.Focus();
            }
        }

        private void cmbVatSales_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbVatSales) > 0)
                    cmbVatPurchase.Focus();
                else
                    cmbVatSales.Focus();
            }
        }

        private void cmbVatPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbVatPurchase) > 0)
                    btnSetTax.Focus();
                else
                    cmbVatPurchase.Focus();
            }
        }

        private void cmbDepartmentName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                //if (cmbDepartmentName.SelectedIndex != 0)
                //    ObjFunction.FillCombo(cmbCategoryName, "SELECT DISTINCT StockGroupNo, StockGroupName FROM   MStockGroup WHERE   (ControlGroup = 2) AND (ControlSubGroup = " + ObjFunction.GetComboValue(cmbDepartmentName) + ") AND (IsActive = 'true')  ORDER BY StockGroupName");
                //else
                //    ObjFunction.FillCombo(cmbCategoryName, "SELECT DISTINCT StockGroupNo, StockGroupName FROM   MStockGroup WHERE   (ControlGroup = 2) AND  (IsActive = 'true')  ORDER BY StockGroupName");
                cmbCategoryName.Focus();
                //Validation();
            }
        }

        private void cmbGroupNo2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
                {
                    DisplayAll(false);
                    dgvTaxItem.Focus();
                    //ObjFunction.FillCombo(cmbDepartmentName, " SELECT Distinct MItemGroup.StockGroupNo, MItemGroup.StockGroupName " +
                    //                                        " FROM MStockItems INNER JOIN MStockGroup ON mItemMaster.FKStockDeptNo = MItemGroup.StockGroupNo " +
                    //                                        " where ControlGroup=4 and mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " and MItemGroup.IsActive = 'True' order by StockGroupName");
                    //ObjFunction.FillCombo(cmbCategoryName, "SELECT  Distinct MItemGroup.StockGroupNo, MItemGroup.StockGroupName " +
                    //                                        " FROM  MStockItems INNER JOIN MStockGroup ON mItemMaster.GroupNo1 = MItemGroup.StockGroupNo " +
                    //                                        " where ControlGroup=2 and mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " and MItemGroup.IsActive = 'True' order by StockGroupName");
                }
                else
                {
                    DisplayAll(true);
                    ObjFunction.FillCombo(cmbDepartmentName, "SELECT Distinct StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4 order by StockGroupName");
                    ObjFunction.FillCombo(cmbCategoryName, "SELECT Distinct StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 order By StockGroupName");
                    txtBarcode.Focus();
                }
                //BindGrid1(0);
                
            }
        }

        public void DisplayAll(bool flag)
        {
            cmbCategoryName.Enabled = flag;
            cmbDepartmentName.Enabled = flag;
            cmbCategoryName.SelectedIndex = 0;
            cmbDepartmentName.SelectedIndex = 0;
            txtBarcode.Text = "";
        }

        private void cmbCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;               
                //Validation();
                //if (dgvTaxItem.Rows.Count > 0)
                //{
                //    dgvTaxItem.CurrentCell = dgvTaxItem[0, 0];
                dgvTaxItem.Focus();
                //}
            }
        }

        private void cmbGroupNo2_Leave(object sender, EventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
            {
                DisplayAll(false);
                BindGrid1(0);
            }
            else
            {
                DisplayAll(true);
                ObjFunction.FillCombo(cmbDepartmentName, "SELECT Distinct StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4 order by StockGroupName");
                ObjFunction.FillCombo(cmbCategoryName, "SELECT Distinct StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 order By StockGroupName");
            }
        }

        private void cmbDepartmentName_Leave(object sender, EventArgs e)
        {
            if (cmbDepartmentName.SelectedIndex != 0)
            {
                ObjFunction.FillCombo(cmbCategoryName, "SELECT DISTINCT StockGroupNo, StockGroupName FROM   MStockGroup WHERE   (ControlGroup = 2) AND (ControlSubGroup = " + ObjFunction.GetComboValue(cmbDepartmentName) + ") AND (IsActive = 'true')  ORDER BY StockGroupName");
                BindGrid1(0);
                dgvTaxItem.Focus();
            }
            else
                ObjFunction.FillCombo(cmbCategoryName, "SELECT DISTINCT StockGroupNo, StockGroupName FROM   MStockGroup WHERE   (ControlGroup = 2) AND  (IsActive = 'true')  ORDER BY StockGroupName");
        }

        private void cmbCategoryName_Leave(object sender, EventArgs e)
        {
            Validation();
            //if (dgvTaxItem.Rows.Count > 0)
            //{
            //    dgvTaxItem.CurrentCell = dgvTaxItem[0, 0];
            //    dgvTaxItem.Focus();
            //}
        }

        public void DisplayMessageForWait(bool flag)
        {
            try
            {
                lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                lblWait.Text = "Processing ...";
                Application.DoEvents();
                lblWait.Visible = flag;
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        
    }
}
