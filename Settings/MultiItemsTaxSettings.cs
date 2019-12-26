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
    public partial class MultiItemsTaxSettings : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dt = new DataTable();
        DataView dv = new DataView();
        DataTable dt1 = new DataTable();
        DataView dv1 = new DataView();

        public MultiItemsTaxSettings()
        {
            InitializeComponent();
        }

        private void MultiItemsTaxSettings_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbGroupNo1, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 ORDER BY StockGroupName");
            ObjFunction.FillComb(cmbGroupNo2, "GroupNo", "StockGroupName");
            ObjFunction.FillCombo(cmbDepartmentName, "SELECT  DepartmentNo, DepartmentName FROM  MStockDepartment Where DepartmentNo Not In(1) ORDER BY DepartmentName");
            ObjFunction.FillCombo(cmbCategoryName, "SELECT  CategoryNo, CategoryName FROM MStockCategory Where CategoryNo Not In(1) ORDER BY CategoryName");
            dgvItemTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));
            dgvTaxItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom))));
            dgvItemTax.ColumnHeadersDefaultCellStyle.Font = GetFont();
            dgvItemTax.RowTemplate.DefaultCellStyle.Font = GetFont();
            dgvTaxItem.ColumnHeadersDefaultCellStyle.Font = GetFont();
            dgvItemTax.RowTemplate.DefaultCellStyle.Font = GetFont();
            rdBoth.Checked = true;
            KeyDownFormat(this.Controls);
            BindGrid();
            pnlrb.Visible = false;
        }

        private Font GetFont()
        {
            return new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
        }

        private void BindGrid()
        {
            string sql = "";
            sql = "SELECT  MItemTaxSetting.PkSrNo, MItemTaxSetting.TaxSettingName AS ItemName, MGroup.GroupName AS TaxType, MLedger_1.LedgerName AS SalesAccount, " +
                 "MLedger.LedgerName AS TaxAccount, MTaxCalculationMethod.CalculationMethod AS CalculationMethodName, MItemTaxSetting.Percentage, " +
                 "MItemTaxSetting.TaxLedgerNo, MItemTaxSetting.SalesLedgerNo, MItemTaxSetting.CalculationMethod AS CalculationMethodNo, " +
                 "MGroup_1.GroupName AS TransType, MLedger_1.GroupNo AS TransTypeNo " +
                 "FROM  MTaxCalculationMethod INNER JOIN " +
                 "MItemTaxSetting ON MTaxCalculationMethod.CalculationMethodNo = MItemTaxSetting.CalculationMethod INNER JOIN " +
                 "MCompany ON MItemTaxSetting.CompanyNo = MCompany.CompanyNo INNER JOIN " +
                 "MGroup INNER JOIN " +
                 "MLedger ON MGroup.GroupNo = MLedger.GroupNo ON MItemTaxSetting.TaxLedgerNo = MLedger.LedgerNo INNER JOIN " +
                 "MLedger AS MLedger_1 INNER JOIN " +
                 "MGroup AS MGroup_1 ON MLedger_1.GroupNo = MGroup_1.GroupNo ON MItemTaxSetting.SalesLedgerNo = MLedger_1.LedgerNo " +
                 "Where MItemTaxSetting.IsActive='True' " +
                 "ORDER BY ItemName";
            dt = ObjFunction.GetDataView(sql).Table;
            dgvItemTax.DataSource = dt.DefaultView;
            if (dt.Rows.Count > 0)
            {
                dgvItemTax.Focus();
                dgvItemTax.CurrentCell = dgvItemTax[0, 0];
            }
            //string sql = "";
            //sql ="SELECT  t.PkSrNo, MItemTaxSetting.TaxSettingName As ItemName, t.TaxType, t.SalesAccount, t.TaxAccount, t.CalculationMethodName, t.Percentage, i.CompanyName, t.FromDate, "+
            //     "i.Group1Name, i.Group2Name, i.DepartmentName, i.CategoryName, t.TaxLedgerNo, t.SalesLedgerNo, t.CalculationMethodNo, t.TransType, t.TransTypeNo, "+
            //     "t.FKTaxSettingNo "+
            //     "FROM  GetItemInfo AS i INNER JOIN "+
            //     "GetTaxInfo AS t ON i.ItemNo = t.ItemNo INNER JOIN "+
            //     "MItemTaxSetting ON t.FKTaxSettingNo = MItemTaxSetting.PkSrNo "+
            //     "ORDER BY MItemTaxSetting.TaxSettingName, t.FromDate"; 

            //    //" select PkSrNo, MStockBarcode.Barcode, ItemName, TaxType,SalesAccount, TaxAccount, t.CalculationMethodName, Percentage, CompanyName, FromDate, " +
            //    //  " Group1Name,Group2Name,DepartmentName, CategoryName, i.ItemNo, TaxLedgerNo,SalesLedgerNo,t.CalculationMethodNo, " +
            //    //  " t.TransType,t.TransTypeNo,t.FKTaxSettingNo from getiteminfo as i Inner JOIN gettaxinfo as t ON i.ItemNo = t.ItemNo   INNER JOIN " +
            //    //  " MStockBarcode ON i.ItemNo = MStockBarcode.ItemNo " +
            //    //  " WHERE ItemName LIKE '%" + txtSelectItem.Text + "%' "+
            //    //  " And MStockBarcode.Barcode LIKE '%"+txtBarcodeSearch.Text+"%' "+
            //    //  " order By MStockBarcode.Barcode,ItemName asc,FromDate desc ";
            //dt = ObjFunction.GetDataView(sql).Table;
            //dgvItemTax.DataSource = dt.DefaultView;
            //if (dt.Rows.Count > 0)
            //{
            //    dgvItemTax.Focus();
            //    dgvItemTax.CurrentCell = dgvItemTax[0, 0];
            //}
        }

        private void BindGrid1(int type)
        {
            dt1.Clear();
            string sql = "";
            for (int i = 0; i < dgvItemTax.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvItemTax[0, i].Value) == true)
                {
                    sql = " SELECT     ISNULL(t.PkSrNo, 0) AS PkSrNo,i.Barcode, i.ItemName, MGroup.GroupName AS TaxType, MLedger_1.LedgerName AS SalesAccount,  " +
                      " MLedger.LedgerName AS TaxAccount, MTaxCalculationMethod.CalculationMethod AS CalculationMethodName, t.Percentage, i.CompanyName, t.FromDate, i.Group1Name, " +
                      " i.Group2Name, i.DepartmentName, i.CategoryName, i.ItemNo, t.TaxLedgerNo, t.SalesLedgerNo, t.CalculationMethod AS CalculationMethodNo " +
                      " FROM (SELECT     PkSrNo, ItemNo, TaxLedgerNo, SalesLedgerNo, FromDate, CalculationMethod, Percentage, CompanyNo FROM dbo.GetItemTaxAll(NULL, NULL, " + dgvItemTax[13, i].Value + ",NULL,NULL) ) AS t INNER JOIN " +
                      " MLedger ON t.TaxLedgerNo = MLedger.LedgerNo INNER JOIN MLedger AS MLedger_1 ON t.SalesLedgerNo = MLedger_1.LedgerNo INNER JOIN MTaxCalculationMethod ON t.CalculationMethod = MTaxCalculationMethod.CalculationMethodNo INNER JOIN " +
                      " MGroup ON MLedger.GroupNo = MGroup.GroupNo RIGHT OUTER JOIN GetItemInfo AS i ON t.ItemNo = i.ItemNo " +
                      "Where (i.ItemNo <> 1)";

                    //" SELECT     ISNULL(t.PkSrNo, 0) AS PkSrNo,i.Barcode, i.ItemName, MGroup.GroupName AS TaxType, MLedger_1.LedgerName AS SalesAccount,  " +
                    //" MLedger.LedgerName AS TaxAccount, MTaxCalculationMethod.CalculationMethod AS CalculationMethodName, t.Percentage, i.CompanyName, t.FromDate, i.Group1Name, " +
                    //" i.Group2Name, i.DepartmentName, i.CategoryName, i.ItemNo, t.TaxLedgerNo, t.SalesLedgerNo, t.CalculationMethod AS CalculationMethodNo " +
                    //" FROM (SELECT     PkSrNo, ItemNo, TaxLedgerNo, SalesLedgerNo, FromDate, CalculationMethod, Percentage, CompanyNo FROM dbo.GetItemTaxAll(NULL, NULL, " + dgvItemTax[21, i].Value + ",NULL,NULL) ) AS t INNER JOIN " +
                    //" MLedger ON t.TaxLedgerNo = MLedger.LedgerNo INNER JOIN MLedger AS MLedger_1 ON t.SalesLedgerNo = MLedger_1.LedgerNo INNER JOIN MTaxCalculationMethod ON t.CalculationMethod = MTaxCalculationMethod.CalculationMethodNo INNER JOIN " +
                    //" MGroup ON MLedger.GroupNo = MGroup.GroupNo RIGHT OUTER JOIN GetItemInfo AS i ON t.ItemNo = i.ItemNo " +
                    //"Where (i.ItemNo <> 1) And i.ItemNo Not In(" + dgvItemTax[16, i].Value + ") ";
                    break;
                }
            }

            string StrWhere = "";
            if (ObjFunction.GetComboValue(cmbGroupNo1) > 0)
            {
                StrWhere += " And Group2Name ='" + cmbGroupNo1.Text + "' ";
            }
            if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
            {
                StrWhere += " And Group1Name ='" + cmbGroupNo2.Text + "' ";
            }
            if (ObjFunction.GetComboValue(cmbCategoryName) > 0)
            {
                StrWhere += " And CategoryName ='" + cmbCategoryName.Text + "' ";
            }
            if (ObjFunction.GetComboValue(cmbDepartmentName) > 0)
            {
                StrWhere += " And DepartmentName ='" + cmbDepartmentName.Text + "' ";
            }
            if (txtProductName.Text != "")
            {
                StrWhere += " And ItemName LIKE '" + txtProductName.Text + "%' ";
            }
            if (txtBarcode.Text != "")
            {
                StrWhere += " And Barcode LIKE '" + txtBarcode.Text + "%' ";
            }
            if (type == 1)
            {
                StrWhere += " And MGroup.GroupName is not null ";//Assigned Data
            }
            else if (type == 2)
            {
                StrWhere += " And MGroup.GroupName is null ";//Not Assigned Data
            }
            sql += StrWhere + " order by i.Barcode,i.ItemName";
            dt1 = ObjFunction.GetDataView(sql).Table;
            dgvTaxItem.DataSource = dt1.DefaultView;

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
                //EP.SetError(cmbGroupNo1, "");
                //EP.SetError(cmbGroupNo2, "");
                //EP.SetError(cmbCategoryName, "");
                //EP.SetError(cmbDepartmentName, "");
                //if (ObjFunction.GetComboValue(cmbGroupNo1) <= 0)
                //{
                //    EP.SetError(cmbGroupNo1, "Select GroupNo1");
                //    EP.SetIconAlignment(cmbGroupNo1, ErrorIconAlignment.MiddleRight);
                //    cmbGroupNo1.Focus();
                //}
                //else
                //{
                //    if ((int)e.KeyChar == 13)
                //    {
                //        BindGrid1(0);
                //    }
                //}
            }
        }

        private void cmbGroupNo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Validation();
                //EP.SetError(cmbGroupNo1, "");
                //EP.SetError(cmbGroupNo2, "");
                //EP.SetError(cmbCategoryName, "");
                //EP.SetError(cmbDepartmentName, "");
                //if (ObjFunction.GetComboValue(cmbGroupNo2) <= 0)
                //{
                //    EP.SetError(cmbGroupNo2, "Select GroupNo2");
                //    EP.SetIconAlignment(cmbGroupNo2, ErrorIconAlignment.MiddleRight);
                //    cmbGroupNo2.Focus();
                //}
                //else
                //{
                //    if ((int)e.KeyChar == 13)
                //    {
                //        BindGrid1(0);
                //    }
                //}
            }
        }

        private void cmbCategoryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Validation();
                //EP.SetError(cmbGroupNo1, "");
                //EP.SetError(cmbGroupNo2, "");
                //EP.SetError(cmbCategoryName, "");
                //EP.SetError(cmbDepartmentName, "");
                //if (ObjFunction.GetComboValue(cmbCategoryName) <= 0)
                //{
                //    EP.SetError(cmbCategoryName, "Select GroupNo2");
                //    EP.SetIconAlignment(cmbCategoryName, ErrorIconAlignment.MiddleRight);
                //    cmbCategoryName.Focus();
                //}
                //else
                //{
                //    if ((int)e.KeyChar == 13)
                //    {
                //        BindGrid1(0);
                //    }
                //}
            }
        }

        private void cmbDepartmentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Validation();

                //EP.SetError(cmbGroupNo1, "");
                //EP.SetError(cmbGroupNo2, "");
                //EP.SetError(cmbCategoryName, "");
                //EP.SetError(cmbDepartmentName, "");
                //if (ObjFunction.GetComboValue(cmbDepartmentName) <= 0)
                //{
                //    EP.SetError(cmbDepartmentName, "Select GroupNo2");
                //    EP.SetIconAlignment(cmbDepartmentName, ErrorIconAlignment.MiddleRight);
                //    cmbDepartmentName.Focus();
                //}
                //else
                //{
                //    if ((int)e.KeyChar == 13)
                //    {
                //        BindGrid1(0);
                //    }
                //}
            }
        }

        private void dgvTaxItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {
            //EP.SetError(cmbGroupNo1, "");
            //EP.SetError(cmbGroupNo2, "");
            //EP.SetError(cmbCategoryName, "");
            //EP.SetError(cmbDepartmentName, "");
            //EP.SetError(txtProductName, "");
            if (txtProductName.Text != "")
            {
                BindGrid1(0);
                //EP.SetError(cmbDepartmentName, "Select GroupNo2");
                //EP.SetIconAlignment(cmbDepartmentName, ErrorIconAlignment.MiddleRight);
                //cmbDepartmentName.Focus();
            }
            //else
            //{
            //    BindGrid1(0);
            //}
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
            dt.Clear();
            dt1.Clear();
            EP.SetError(cmbGroupNo1, "");
            EP.SetError(cmbGroupNo2, "");
            EP.SetError(cmbCategoryName, "");
            EP.SetError(cmbDepartmentName, "");
            chkSelectAll.Checked = false;
            txtProductName.Text = "";
            txtBarcode.Text = "";
            cmbGroupNo1.SelectedIndex = 0;
            cmbGroupNo2.SelectedIndex = 0;
            cmbCategoryName.SelectedIndex = 0;
            cmbDepartmentName.SelectedIndex = 0;
            cmbGroupNo1.Enabled = false;
            cmbGroupNo2.Enabled = false;
            txtProductName.Enabled = false;
            txtBarcode.Enabled = false;
            cmbDepartmentName.Enabled = false;
            cmbCategoryName.Enabled = false;
            rdBoth.Checked = true;
            BindGrid();
            dgvItemTax.DataSource = dt.DefaultView;
            dgvTaxItem.DataSource = dt1.DefaultView;
            pnlrb.Visible = false;
        }

        private void btnSetTax_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public void SetValue()
        {
            bool flag = false;
            DataTable dt = new DataTable();
            DBMDutiesTaxesInfo dbMDutiesTaxesInfo = new DBMDutiesTaxesInfo();
            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

            for (int j = 0; j < dgvItemTax.Rows.Count; j++)
            {
                if (Convert.ToBoolean(dgvItemTax[0, j].Value) == true)
                {
                    dbMDutiesTaxesInfo = new DBMDutiesTaxesInfo();
                    mItemTaxInfo = new MItemTaxInfo();
                    for (int i = 0; i < dgvTaxItem.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgvTaxItem[0, i].Value) == true)
                        {
                            flag = true;
                            mItemTaxInfo.PkSrNo = 0;

                            mItemTaxInfo.ItemNo = Convert.ToInt64(dgvTaxItem[16, i].Value);
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(dgvItemTax[9, j].Value);//Convert.ToInt64(dgvItemTax[17, j].Value)
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(dgvItemTax[10, j].Value);
                            mItemTaxInfo.FromDate = DBGetVal.ServerTime;
                            mItemTaxInfo.Percentage = Convert.ToDouble(dgvItemTax[8, j].Value);
                            mItemTaxInfo.CalculationMethod = dgvItemTax[11, j].Value.ToString();
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt32(dgvItemTax[2, j].Value.ToString());
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            dbMDutiesTaxesInfo.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                    }
                    if (dbMDutiesTaxesInfo.ExecuteNonQueryStatements() == true && flag == true)
                    {
                        OMMessageBox.Show("Tax Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        rdBoth.Checked = true;
                        BindGrid1(0);

                    }
                    else
                    {
                        if (flag == false)
                            OMMessageBox.Show("Select Atleast One Checkbox", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        else
                            OMMessageBox.Show("Tax not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
        }

        //private void dgvItemTax_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    //bool Flag = false;
        //    //for (int i = 0; i < dgvItemTax.Rows.Count; i++)
        //    //{
        //    //    if (e.RowIndex == i)
        //    //    {
        //    //        dgvItemTax.Rows[i].Cells[0].Value = true;
        //    //        cmbGroupNo1.Enabled = true;
        //    //        cmbGroupNo2.Enabled = true;
        //    //        cmbCategoryName.Enabled = true;
        //    //        cmbDepartmentName.Enabled = true;
        //    //        txtProductName.Enabled = true;
        //    //        txtBarcode.Enabled = true;
        //    //        cmbGroupNo1.SelectedIndex = 0;
        //    //        cmbGroupNo2.SelectedIndex = 0;
        //    //        cmbCategoryName.SelectedIndex = 0;
        //    //        cmbDepartmentName.SelectedIndex = 0;
        //    //        txtProductName.Text = "";
        //    //        txtBarcode.Text = "";
        //    //        cmbGroupNo1.Focus();
        //    //        //txtSelectItem.Enabled = false;
        //    //        //txtBarcodeSearch.Enabled = false;
        //    //        //btnSearch.Enabled = false;
        //    //        if (dgvTaxItem.Rows.Count > 0)
        //    //        {
        //    //            dt1.Clear();
        //    //            dgvTaxItem.DataSource = dt1.DefaultView;
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        dgvItemTax.Rows[i].Cells[0].Value = false;

        //    //    }
        //    //    if (Convert.ToBoolean(dgvItemTax.Rows[i].Cells[0].EditedFormattedValue.ToString()) == true)
        //    //    {
        //    //        Flag = true;
        //    //        break;
        //    //    }

        //    //    rdBoth.Checked = true;
        //    //}
        //    //if (Flag == true) pnlrb.Visible = true; else pnlrb.Visible = false;
        //}

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
            if (ObjFunction.GetComboValue(cmbGroupNo1) > 0)
            {
                flag = true;
            }
            else if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
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
                EP.SetError(cmbGroupNo1, "");
                EP.SetError(cmbGroupNo2, "");
                EP.SetError(cmbCategoryName, "");
                EP.SetError(cmbDepartmentName, "");
                EP.SetError(txtProductName, "");
                EP.SetError(txtBarcode, "");
                //e.SuppressKeyPress = true;
                if (txtBarcode.Text != "")
                {
                    BindGrid1(0);
                    dgvTaxItem.CurrentCell = dgvTaxItem[0, 0];
                }
            }
        }

        //private void txtBarcodeSearch_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        //btnSearch.Focus();
        //    }
        //}

        private void cmbGroupNo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbGroupNo1.SelectedIndex > 0)
                {
                    ObjFunction.FillCombo(cmbGroupNo2, "SELECT DISTINCT StockGroupNo AS GroupNo, StockGroupName FROM  MStockGroup WHERE  (StockGroupNo IN (SELECT  MT.GroupNo FROM MStockItems MT WHERE (MT.GroupNo1 =" + ObjFunction.GetComboValue(cmbGroupNo1) + "))) ORDER BY StockGroupName");
                }
            }
        }

        private void dgvTaxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnSetTax.Focus();
            }
        }

        private void dgvItemTax_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            bool Flag = false;
            for (int i = 0; i < dgvItemTax.Rows.Count; i++)
            {
                if (e.RowIndex == i)
                {
                    dgvItemTax.Rows[i].Cells[0].Value = true;
                    cmbGroupNo1.Enabled = true;
                    cmbGroupNo2.Enabled = true;
                    cmbCategoryName.Enabled = true;
                    cmbDepartmentName.Enabled = true;
                    txtProductName.Enabled = true;
                    txtBarcode.Enabled = true;
                    cmbGroupNo1.SelectedIndex = 0;
                    cmbGroupNo2.SelectedIndex = 0;
                    cmbCategoryName.SelectedIndex = 0;
                    cmbDepartmentName.SelectedIndex = 0;
                    txtProductName.Text = "";
                    txtBarcode.Text = "";
                    cmbGroupNo1.Focus();
                    //txtSelectItem.Enabled = false;
                    //txtBarcodeSearch.Enabled = false;
                    //btnSearch.Enabled = false;
                    if (dgvTaxItem.Rows.Count > 0)
                    {
                        dt1.Clear();
                        dgvTaxItem.DataSource = dt1.DefaultView;
                    }
                }
                else
                {
                    dgvItemTax.Rows[i].Cells[0].Value = false;

                }
                if (Convert.ToBoolean(dgvItemTax.Rows[i].Cells[0].EditedFormattedValue.ToString()) == true)
                {
                    Flag = true;
                }

                rdBoth.Checked = true;
            }
            if (Flag == true)
                pnlrb.Visible = true;
            else
            {
                cmbGroupNo1.Enabled = false;
                cmbGroupNo2.Enabled = false;
                cmbCategoryName.Enabled = false;
                cmbDepartmentName.Enabled = false;
                txtProductName.Enabled = false;
                txtBarcode.Enabled = false;
                cmbGroupNo1.SelectedIndex = 0;
                cmbGroupNo2.SelectedIndex = 0;
                cmbCategoryName.SelectedIndex = 0;
                cmbDepartmentName.SelectedIndex = 0;
                pnlrb.Visible = false;
            }

        }

    }
}
