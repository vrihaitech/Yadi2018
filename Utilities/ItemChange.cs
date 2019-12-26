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

namespace Yadi.Utilities
{
    public partial class ItemChange : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMItemMaster dbMItemMaster = new DBMItemMaster();
        //MRateSetting mRateSettig = new MRateSetting();
        MItemMaster mItemMaster = new MItemMaster();
        //   MItemMaster mItemMaster = new MItemMaster();
        public ItemChange()
        {
            InitializeComponent();
        }

        private void ItemRateChange_Load(object sender, EventArgs e)
        {
            try
            {
                btnApplyChanges.Enabled = false;
                ObjFunction.FillCombo(cmbDepartment, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4  ORDER BY StockGroupName");
                ObjFunction.FillCombo(cmbCategory, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2  ORDER BY StockGroupName");
                ObjFunction.FillCombo(cmbMainGroup, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 ORDER BY StockGroupName");
                // ObjFunction.FillComb(cmbBrandName, "GroupNo", "StockGroupName");
                //ObjFunction.FillCombo(cmbBrandName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
                ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
                ObjFunction.FillComb(cmbItemName, "ItemNo", "ItemName");
                //DataGridViewComboBoxColumn UOM = gvRateSetting.Columns[5] as DataGridViewComboBoxColumn;
                //ObjFunction.FillComb(UOM, "SELECT UOMNo, UOMName FROM MUOM ORDER BY UOMName");
                cmbMainGroup.Focus();
                txtTotProducts.Text = "0";
                btnCancel.Visible = true;
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
        //    try
        //    {
                //if (Validations() == true)
                //{
                //    if (OMMessageBox.Show("Are you sure want to save this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                //    {
                //         dbMItemMaster = new DBMItemMaster();
                //        for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                //        {
                //            if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                //            {
                //                mStockItems  = new MItemMaster();
                //                mItemMaster.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);
                //                mItemMaster.GroupNo1 = ObjFunction.GetComboValue(cmbCategory) == 0 ? Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.CategoryNo].Value) : Convert.ToInt64(ObjFunction.GetComboValue(cmbCategory));
                //                mItemMaster.IsActive = Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsActive].FormattedValue);
                //                mItemMaster.FKStockDeptNo = ObjFunction.GetComboValue(cmbDepartment) == 0 ? Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.DepartmentNo].Value) : Convert.ToInt64(ObjFunction.GetComboValue(cmbDepartment));
                //                dbMStockItems.UpdateStockItemsDeptCate(mStockItems);
                //                if (mStockItems.IsActive == true)//&& ID != 0
                //                {
                //                    dbMItemMaster.UpdateGroupNo(true, ObjFunction.GetComboValue(cmbCategory) == 0 ? Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.CategoryNo].Value) : Convert.ToInt64(ObjFunction.GetComboValue(cmbCategory)));
                //                    dbMItemMaster.UpdateGroupNo(true, ObjFunction.GetComboValue(cmbBrandName));
                //                    dbMItemMaster.UpdateGroupNo(true, ObjFunction.GetComboValue(cmbDepartment) == 0 ? Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.DepartmentNo].Value) : Convert.ToInt64(ObjFunction.GetComboValue(cmbDepartment)));
                //                }
                //            }
                //        }
                //        if (dbMStockItems.ExecuteNonQueryStatements() == true)
                //        {
                //            DisplayMessage("Item Changed Save Successfully...");
                //            BindGrid();
                //            btnApplyChanges.Enabled = false;
                //            ObjFunction.FillCombo(cmbDepartment, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4  ORDER BY StockGroupName");
                //            ObjFunction.FillCombo(cmbCategory, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2  ORDER BY StockGroupName");
                //            cmbDepartment.SelectedIndex = 0;
                //            cmbCategory.SelectedIndex = 0;
                //            pnlChange.Visible = false;
                //            cmbBrandName.Focus();
                //        }
                //   
            //}
            //    }
            //}
            //catch (Exception exc)
            //{
            //    ObjFunction.ExceptionDisplay(exc.Message);
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ChkSelect.Checked = false;
                while (gvRateSetting.Rows.Count > 0)
                {
                    gvRateSetting.Rows.RemoveAt(0);
                }
                if (cmbItemName.SelectedIndex >= 0)
                    cmbItemName.SelectedIndex = 0;
                if (cmbMainGroup.SelectedIndex >= 0)
                    cmbMainGroup.SelectedIndex = 0;
                if (cmbBrandName.SelectedIndex >= 0)
                    cmbBrandName.SelectedIndex = 0;

                CalculateTotProducts();
                //cmbMainGroup.Focus();
                cmbBrandName.Focus();
                ObjFunction.FillCombo(cmbDepartment, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4  ORDER BY StockGroupName");
                ObjFunction.FillCombo(cmbCategory, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2  ORDER BY StockGroupName");
                cmbDepartment.SelectedIndex = 0;
                cmbCategory.SelectedIndex = 0;
                btnApplyChanges.Enabled = false;
                pnlChange.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindGrid()
        {
            try
            {
                string sql = "";
                sql = "SELECT   0 AS srNo, MItemGroup.StockGroupName AS DepartmentName, MStockGroup_1.StockGroupName AS CategoryName, mItemMaster.ItemName, mItemMaster.ItemNo, " +
                      " MItemGroup.StockGroupNo AS DepartmentNo, MStockGroup_1.StockGroupNo AS CategoryNo, mItemMaster.IsActive, 'false' AS chk,MStockItems.IsActive As HidChk " +
                      " FROM   MStockItems INNER JOIN " +
                      " MStockGroup ON mItemMaster.FKStockDeptNo = MItemGroup.StockGroupNo INNER JOIN " +
                      " MStockGroup AS MStockGroup_1 ON mItemMaster.GroupNo1 = MStockGroup_1.StockGroupNo INNER JOIN " +
                      " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo " +
                      " Where ";
                string strWhere = "";
                //if (ObjFunction.GetComboValue(cmbMainGroup) != 0)
                //{
                //    strWhere = " mItemMaster.GroupNo1 = " + ObjFunction.GetComboValue(cmbMainGroup) + "";
                //}
                if (ObjFunction.GetComboValue(cmbBrandName) != 0)
                {
                    strWhere = " (MStockItems.GroupNo1 = " + ObjFunction.GetComboValue(cmbMainGroup) + " or mItemMaster.GroupNo = " + ObjFunction.GetComboValue(cmbBrandName) + ")";
                }
                //if (ObjFunction.GetComboValue(cmbItemName) != 0)
                //{
                //    strWhere += "AND mItemMaster.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + "";
                //}
                //if (txtBarcode.Text != "")
                //{
                //    if(ObjFunction.GetComboValue(cmbBrandName) != 0)
                //        strWhere += " And MStockBarcode.Barcode ='"+txtBarcode.Text.Trim()+"'";
                //    else
                //        strWhere += " MStockBarcode.Barcode ='" + txtBarcode.Text.Trim() + "'";
                //}
                sql += strWhere + " order by mItemMaster.ItemName";
                DataTable dt = new DataTable();
                if (strWhere != "")
                    dt = ObjFunction.GetDataView(sql).Table;
                gvRateSetting.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvRateSetting.Rows.Add();
                        for (int j = 0; j < gvRateSetting.Columns.Count; j++)
                        {
                            if (j == ColIndex.IsActive)
                                gvRateSetting.Rows[i].Cells[ColIndex.IsActive].Value = false;

                            if (j == ColIndex.HidChk)
                                gvRateSetting.Rows[i].Cells[ColIndex.HidChk].Value = false;

                            gvRateSetting.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                        gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = "False";
                    }
                    if (gvRateSetting.Rows.Count > 0)
                    {
                        gvRateSetting.CurrentCell = gvRateSetting[ColIndex.Chk, 0];
                        gvRateSetting.Focus();
                    }
                }
                else
                {
                    //DisplayMessage("Records not found...!!");
                    while (gvRateSetting.Rows.Count > 0)
                    {
                        gvRateSetting.Rows.RemoveAt(0);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbMainGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbMainGroup.SelectedIndex > 0)
                {
                    BindGrid();
                    CalculateTotProducts();
                    //ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT mItemMaster.GroupNo, MItemGroup.StockGroupName FROM  MStockItems INNER JOIN  MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo WHERE (MStockItems.GroupNo IN (SELECT  GroupNo FROM MStockItems AS MStockItems  WHERE (GroupNo1 =" + ObjFunction.GetComboValue(cmbMainGroup) + "))) ORDER BY MItemGroup.StockGroupName");
                    //ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT StockGroupNo AS GroupNo, StockGroupName FROM  MStockGroup WHERE  (StockGroupNo IN (SELECT  MT.GroupNo FROM MStockItems MT WHERE (MT.GroupNo1 =" + ObjFunction.GetComboValue(cmbMainGroup) + "))) ORDER BY StockGroupName");
                    ObjFunction.FillCombo(cmbBrandName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
                }
            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int Sr = 0;
            public static int DepartmentName = 1;
            public static int CategoryName = 2;
            public static int ItemName = 3;
            public static int ItemNo = 4;
            public static int DepartmentNo = 5;
            public static int CategoryNo = 6;
            public static int IsActive = 7;
            public static int Chk = 8;
            public static int HidChk = 9;
        }
        #endregion

        private void cmbBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cmbBrandName.SelectedIndex >= 0)
                    {
                        BindGrid();
                        CalculateTotProducts();
                        ObjFunction.FillCombo(cmbItemName, "SELECT ItemNo,ItemName From MStockItems WHERE GroupNo=" + ObjFunction.GetComboValue(cmbBrandName) + " or GroupNo1='" + ObjFunction.GetComboValue(cmbMainGroup) + "' ORDER BY ItemName");
                        if (gvRateSetting.Rows.Count > 0)
                        {
                            e.SuppressKeyPress = true;
                            gvRateSetting.Focus();
                            gvRateSetting.CurrentCell = gvRateSetting[8, 0];
                        }
                    }
                    btnApplyChanges.Enabled = false;
                    pnlChange.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CalculateTotProducts()
        {
            txtTotProducts.Text = Convert.ToString(gvRateSetting.Rows.Count);
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void cmbMainGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT mItemMaster.GroupNo, MItemGroup.StockGroupName FROM  MStockItems INNER JOIN  MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo WHERE (MStockItems.GroupNo IN (SELECT  GroupNo FROM MStockItems AS MStockItems  WHERE (GroupNo1 =" + ObjFunction.GetComboValue(cmbMainGroup) + "))) ORDER BY MItemGroup.StockGroupName");
            ObjFunction.FillCombo(cmbBrandName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
        }

        private void gvRateSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void gvRateSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnOK.Focus();
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
                ChkSelect.Checked = !ChkSelect.Checked;
                ChkSelect_CheckedChanged(sender, e);
            }
        }
        #endregion

        private void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRateSetting.Rows.Count; i++)
            {
                gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = ChkSelect.Checked;
            }
        }

        public bool Validations()
        {
            try
            {
                bool flag = false;
                //bool fl = false;
                if (ObjFunction.GetComboValue(cmbDepartment) != 0 || ObjFunction.GetComboValue(cmbCategory) != 0)
                {
                    flag = true;
                }
                else if (ObjFunction.GetComboValue(cmbDepartment) == 0 || ObjFunction.GetComboValue(cmbCategory) == 0)
                {
                    DisplayMessage("Please Select Department or Category");
                    if (pnlChange.Visible == true)
                        cmbDepartment.Focus();
                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(1200);
            lblMsg.Visible = false;
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void gvRateSetting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (gvRateSetting.Rows.Count > 0 && e.ColumnIndex != ColIndex.Chk)//&& e.ColumnIndex!=ColIndex.IsActive)
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].Value = true;
                        //if(e.ColumnIndex!=ColIndex.IsActive)
                        //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = true;
                        for (int i = 0; i < gvRateSetting.Columns.Count - 1; i++)
                        {
                            if (gvRateSetting.Rows[e.RowIndex].Cells[i].Value == null || gvRateSetting.Rows[e.RowIndex].Cells[i].Value.ToString().Length == 0 || gvRateSetting.Rows[e.RowIndex].Cells[i].ErrorText != "")
                            {
                                gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].Value = false;
                                //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                long iselected = ObjFunction.GetComboValue(cmbDepartment);
                if (iselected != 0)
                    ObjFunction.FillCombo(cmbCategory, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 AND ControlSubGroup=" + ObjFunction.GetComboValue(cmbDepartment) + " ORDER BY StockGroupName");
                e.SuppressKeyPress = true;
                cmbCategory.Focus();
                cmbCategory.SelectedIndex = 0;
            }
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            bool flag = false;

            for (int i = 0; i < gvRateSetting.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                {
                    flag = true;
                    break;
                }
            }
            if (flag == true)
            {
                pnlChange.Visible = true;
                btnApplyChanges.Enabled = true;
                cmbDepartment.Focus();
            }
            else
            {
                DisplayMessage("Please Select Atleast One Item");
                if (gvRateSetting.Rows.Count > 0)
                {
                    gvRateSetting.Focus();
                    gvRateSetting.CurrentCell = gvRateSetting[8, 0];
                }
            }
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnApplyChanges.Focus();
            }
        }

    }
}
