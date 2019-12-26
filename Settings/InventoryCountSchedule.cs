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
    public partial class InventoryCountSchedule : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMStockCountSchedule dbMStockCountSchedule = new DBMStockCountSchedule();
        MStockCountSchedule mStockCountSchedule = new MStockCountSchedule();
        DataTable dt = new DataTable();

        public InventoryCountSchedule()
        {
            InitializeComponent();
        }

        private void InventoryCountSchedule_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbGroupNo2, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE ControlGroup=3 AND IsActive = 'True' ORDER BY ItemGroupName");
                ObjFunction.FillCombo(cmbDepartmentName, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 order by ItemGroupName");
                ObjFunction.FillCombo(cmbCategoryName, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 order By ItemGroupName");
                ObjFunction.FillCombo(cmbStockCountType, "SELECT CountTypeNo, CountTypeName FROM MStockCountType WHERE (IsActive = 'True') ");
                btnSave.Enabled = false;
                cmbGroupNo2.Focus();
                KeyDownFormat(this.Controls);
                gvDetails.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                gvDetails.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BindGrid()
        {
            try
            {
                while (gvDetails.Rows.Count > 0)
                    gvDetails.Rows.RemoveAt(0);

                pnlType.Visible = false;
                btnSave.Enabled = false;

                if (ObjFunction.GetComboValue(cmbGroupNo2) != 0 || ObjFunction.GetComboValue(cmbCategoryName) != 0 || txtBarcode.Text != "" || ObjFunction.GetComboValue(cmbDepartmentName) != 0)
                {
                    string sql = " SELECT 0 AS srNo, MItemGroup.StockGroupName AS DepartmentName, MStockGroup_1.StockGroupName AS CategoryName, mItemMaster.ItemName, MUOM.UOMName, " +
                               " MStockCountType.CountTypeName, NULL AS Value, MStockCountSchedule.CountScheduleDate,MStockCountType.CountTypeNo, MItemGroup.StockGroupNo AS DepartmentNo,MStockGroup_1.StockGroupNo AS CategoryNo, mItemMaster.ItemNo, ISNULL(MStockCountSchedule.PkSrNo, 0) AS PkSrNo,'false' AS chk " +
                               ",'false' As ChkSelect " +
                               " FROM MStockCountType INNER JOIN MStockCountSchedule ON MStockCountType.CountTypeNo = MStockCountSchedule.CountTypeNo RIGHT OUTER JOIN MStockItems INNER JOIN " +
                               " MStockGroup ON mItemMaster.FKStockDeptNo = MItemGroup.StockGroupNo INNER JOIN MStockGroup AS MStockGroup_1 ON mItemMaster.GroupNo1 = MStockGroup_1.StockGroupNo INNER JOIN MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN " +
                               " MUOM ON mItemMaster.UOMPrimary = MUOM.UOMNo ON MStockCountSchedule.ItemNo = mItemMaster.ItemNo " +
                               " Where (MStockItems.ItemNo <> 1) And  (MStockItems.FkStockGroupTypeNo <> 3) And mItemMaster.IsActive='" + rbActive.Checked + "' ";
                    string strWhere = "";

                    if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
                    {
                        strWhere += " And  mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " ";
                    }

                    if (ObjFunction.GetComboValue(cmbCategoryName) > 0)
                    {
                        strWhere += " And mItemMaster.GroupNo1 =" + ObjFunction.GetComboValue(cmbCategoryName) + " ";
                    }
                    if (ObjFunction.GetComboValue(cmbDepartmentName) > 0)
                    {
                        strWhere += " And mItemMaster.FKStockDeptNo =" + ObjFunction.GetComboValue(cmbDepartmentName) + " ";
                    }
                    if (txtBarcode.Text.Trim() != "")
                    {
                        strWhere += " And  MStockBarcode.Barcode ='" + txtBarcode.Text.Trim() + "' ";
                    }

                    sql += strWhere + " order by mItemMaster.ItemName";
                    try
                    {
                        dt = new DataTable();
                        dt = ObjFunction.GetDataView(sql).Table;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            gvDetails.Rows.Add();
                            for (int c = 0; c < dt.Columns.Count; c++)
                            {
                                gvDetails[c, i].Value = dt.Rows[i][c];
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        CommonFunctions.ErrorMessge = e.Message;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    EP.SetError(cmbGroupNo2, "");
                    EP.SetError(cmbCategoryName, "");
                    EP.SetError(cmbDepartmentName, "");
                    EP.SetError(txtBarcode, "");
                    e.SuppressKeyPress = true;
                    if (txtBarcode.Text != "")
                    {
                        cmbCategoryName.SelectedIndex = 0;
                        cmbDepartmentName.SelectedIndex = 0;
                        cmbGroupNo2.SelectedIndex = 0;
                        if (gvDetails.Rows.Count > 0)
                        {
                            gvDetails.CurrentCell = gvDetails[ColIndex.chk, 0];
                            gvDetails.Focus();
                        }
                        BindGrid();
                    }
                    else
                        cmbDepartmentName.Focus();
                  
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
            try
            {
                if (e.KeyCode == Keys.F5)
                {
                    rbActive.Checked = true;
                }
                else if (e.KeyCode == Keys.F6)
                {
                    rbDeActive.Checked = true;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    chkSelectAll.Checked = !chkSelectAll.Checked;

                    for (int i = 0; i < gvDetails.Rows.Count; i++)
                    {
                        gvDetails.Rows[i].Cells[ColIndex.chk].Value = chkSelectAll.Checked;
                        if (Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chk].EditedFormattedValue) == true && Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chkSelect].EditedFormattedValue) == true)
                        {
                            if (Convert.ToInt64(gvDetails.Rows[i].Cells[ColIndex.PkSrNo].EditedFormattedValue) == 0)
                            {
                                gvDetails.Rows[i].Cells[ColIndex.CountType].Value = "";
                                gvDetails.Rows[i].Cells[ColIndex.CountTypeNo].Value = "";
                                gvDetails.Rows[i].Cells[ColIndex.chkSelect].Value = false;
                                gvDetails.Rows[i].Cells[ColIndex.Value].Value = "";
                                gvDetails.Rows[i].Cells[ColIndex.CountSchedule].Value = "";
                            }
                        }
                    }
                    if (pnlType.Visible == false && chkSelectAll.Checked == true && gvDetails.Rows.Count > 0)
                    {
                        pnlType.Visible = true;
                        cmbDefault.Visible = false;
                        DtpDefaultValue.Visible = false;
                        cmbStockCountType.SelectedIndex = 0;
                        btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                    }
                    else if (pnlType.Visible == true && chkSelectAll.Checked == false)
                    {
                        pnlType.Visible = false;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #endregion

        private void cmbDepartmentName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    BindGrid();
                    cmbCategoryName.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    Validation();
                    if (gvDetails.Rows.Count > 0)
                    {
                        gvDetails.CurrentCell = gvDetails[ColIndex.chk, 0];
                        gvDetails.Focus();
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

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
                while (gvDetails.Rows.Count > 0)
                    gvDetails.Rows.RemoveAt(0);
            }
            else
            {
                BindGrid();
            }
        }

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(700);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbGroupNo2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtBarcode.Text = "";
                    e.SuppressKeyPress = true;
                    if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
                    {
                        ObjFunction.FillCombo(cmbDepartmentName, " SELECT Distinct MItemGroup.StockGroupNo, MItemGroup.StockGroupName " +
                                                                " FROM MStockItems INNER JOIN MStockGroup ON mItemMaster.FKStockDeptNo = MItemGroup.StockGroupNo " +
                                                                " where ControlGroup=4 and mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " and MItemGroup.IsActive = 'True' order by StockGroupName");
                        ObjFunction.FillCombo(cmbCategoryName, "SELECT  Distinct MItemGroup.StockGroupNo, MItemGroup.StockGroupName " +
                                                                " FROM  MStockItems INNER JOIN MStockGroup ON mItemMaster.GroupNo1 = MItemGroup.StockGroupNo " +
                                                                " where ControlGroup=2 and mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " and MItemGroup.IsActive = 'True' order by StockGroupName");
                    }
                    else
                    {
                        ObjFunction.FillCombo(cmbDepartmentName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=4 order by StockGroupName");
                        ObjFunction.FillCombo(cmbCategoryName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 order By StockGroupName");
                    }
                    BindGrid();
                    txtBarcode.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbStockCountType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    long CountType = ObjFunction.GetComboValue(cmbStockCountType);
                    if (CountType == 0)
                    {
                        cmbDefault.Visible = false;
                        DtpDefaultValue.Visible = false;
                        btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                        cmbDefault.Focus();
                    }
                    else if (CountType == StockCountType.Daily || CountType == StockCountType.NA)
                    {
                        cmbDefault.Visible = false;
                        DtpDefaultValue.Visible = false;
                        btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                        btnApply.Focus();
                    }
                    else if (CountType == StockCountType.Weekly)
                    {
                        cmbDefault.Visible = true;
                        DtpDefaultValue.Visible = false;
                        ObjFunction.FillWeek(cmbDefault);
                        cmbDefault.SelectedValue = dbMStockCountSchedule.GetWeek(ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).DayOfWeek.ToString().Trim());
                        cmbDefault.Focus();
                        btnApply.Location = new Point(476, 9);
                    }
                    else if (CountType == StockCountType.Monthly)
                    {
                        cmbDefault.Visible = true;
                        DtpDefaultValue.Visible = false;
                        ObjFunction.FillDays(cmbDefault);
                        cmbDefault.SelectedValue = Convert.ToInt64(ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).ToString("dd").ToUpper().Trim());
                        cmbDefault.Focus();
                        btnApply.Location = new Point(476, 9);
                    }
                    else if (CountType == StockCountType.Yearly)
                    {
                        cmbDefault.Visible = false;
                        DtpDefaultValue.Visible = true;
                        DtpDefaultValue.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                        DtpDefaultValue.CustomFormat = "dd-MMM";
                        DtpDefaultValue.Text = ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).ToString("dd-MMM-yyyy");
                        DtpDefaultValue.Focus();
                        btnApply.Location = new Point(476, 9);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbStockCountType_Leave(object sender, EventArgs e)
        {
            try
            {
                long CountType = ObjFunction.GetComboValue(cmbStockCountType);

                if (CountType == 0)
                {
                    cmbDefault.Visible = false;
                    DtpDefaultValue.Visible = false;
                    btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                    cmbDefault.Focus();
                }
                else if (CountType == StockCountType.Daily || CountType == StockCountType.NA)
                {
                    cmbDefault.Visible = false;
                    DtpDefaultValue.Visible = false;
                    btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                    btnApply.Focus();
                }
                else if (CountType == StockCountType.Weekly)
                {
                    cmbDefault.Visible = true;
                    DtpDefaultValue.Visible = false;
                    ObjFunction.FillWeek(cmbDefault);
                    cmbDefault.SelectedValue = dbMStockCountSchedule.GetWeek(ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).DayOfWeek.ToString().Trim());
                    cmbDefault.Focus();
                    btnApply.Location = new Point(476, 9);
                }
                else if (CountType == StockCountType.Monthly)
                {
                    cmbDefault.Visible = true;
                    DtpDefaultValue.Visible = false;
                    ObjFunction.FillDays(cmbDefault);
                    cmbDefault.SelectedValue = Convert.ToInt64(ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).ToString("dd").ToUpper().Trim());
                    cmbDefault.Focus();
                    btnApply.Location = new Point(476, 9);
                }
                else if (CountType == StockCountType.Yearly)
                {
                    cmbDefault.Visible = false;
                    DtpDefaultValue.Visible = true;
                    DtpDefaultValue.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                    DtpDefaultValue.CustomFormat = "dd-MMM";
                    DtpDefaultValue.Text = ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).ToString("dd-MMM-yyyy");
                    DtpDefaultValue.Focus();
                    btnApply.Location = new Point(476, 9);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int DepartmentName = 1;
            public static int CategoryName = 2;
            public static int ItemName = 3;
            public static int UOMName = 4;
            public static int CountType = 5;
            public static int Value = 6;
            public static int CountSchedule = 7;
            public static int CountTypeNo = 8;
            public static int DepartmentNo = 9;
            public static int CategoryNo = 10;
            public static int ItemNo = 11;
            public static int PkSrNo = 12;
            public static int chk = 13;
            public static int chkSelect = 14;
        }

        private void gvDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //bool flag = false;
            try
            {
                //for (int i = 0; i < gvDetails.Rows.Count; i++)
                //{
                //    if (Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chk].FormattedValue) == true)
                //    {

                //        if (pnlType.Visible == false)
                //        {
                //            cmbDefault.Visible = false;
                //            DtpDefaultValue.Visible = false;
                //            cmbStockCountType.SelectedIndex = 0;
                //            btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                //        }
                //        flag = true;
                //        break;
                //    }

                //}

                //if (Convert.ToBoolean(gvDetails.Rows[e.RowIndex].Cells[ColIndex.chk].FormattedValue) == true && Convert.ToBoolean(gvDetails.Rows[e.RowIndex].Cells[ColIndex.chkSelect].EditedFormattedValue) == true)
                //{
                //    if (Convert.ToInt64(gvDetails.Rows[e.RowIndex].Cells[ColIndex.PkSrNo].EditedFormattedValue) == 0)
                //    {
                //        gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountType].Value = "";
                //        gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].Value = "";
                //        gvDetails.Rows[e.RowIndex].Cells[ColIndex.chkSelect].Value = false;
                //        gvDetails.Rows[e.RowIndex].Cells[ColIndex.Value].Value = "";
                //        gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountSchedule].Value = "";
                //    }

                //}

                //pnlType.Visible = flag;

                gvDetails.Rows[gvDetails.CurrentRow.Index].Cells[ColIndex.chk].Value = !Convert.ToBoolean(gvDetails.Rows[gvDetails.CurrentRow.Index].Cells[ColIndex.chk].FormattedValue);
                gvDetails.Rows[gvDetails.CurrentRow.Index].Cells[ColIndex.chkSelect].Value = !Convert.ToBoolean(gvDetails.Rows[gvDetails.CurrentRow.Index].Cells[ColIndex.chkSelect].FormattedValue);
                gvDetails_CellContentClick(sender, e);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            bool flag = false;
            try
            {
                if (ObjFunction.GetComboValue(cmbStockCountType) != 0)
                {
                    for (int i = 0; i < gvDetails.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chk].FormattedValue) == true)
                        {
                            flag = true;
                            btnSave.Enabled = true;
                            long StockCount = ObjFunction.GetComboValue(cmbStockCountType);
                            gvDetails.Rows[i].Cells[ColIndex.CountType].Value = cmbStockCountType.Text;
                            gvDetails.Rows[i].Cells[ColIndex.CountTypeNo].Value = StockCount;
                            gvDetails.Rows[i].Cells[ColIndex.chkSelect].Value = true;
                            gvDetails.Rows[i].Cells[ColIndex.chk].Value = false;
                            if (StockCount == StockCountType.Daily || StockCount == StockCountType.NA)
                            {
                                gvDetails.Rows[i].Cells[ColIndex.Value].Value = "";
                                gvDetails.Rows[i].Cells[ColIndex.CountSchedule].Value = "";
                            }
                            else if (StockCount == StockCountType.Weekly || StockCount == StockCountType.Monthly)
                            {
                                gvDetails.Rows[i].Cells[ColIndex.Value].Value = cmbDefault.Text;
                                gvDetails.Rows[i].Cells[ColIndex.CountSchedule].Value = dbMStockCountSchedule.SetStockCountType(StockCount, ObjFunction.GetComboValue(cmbDefault).ToString());
                            }
                            else
                            {
                                gvDetails.Rows[i].Cells[ColIndex.Value].Value = cmbDefault.Text;
                                gvDetails.Rows[i].Cells[ColIndex.CountSchedule].Value = dbMStockCountSchedule.SetStockCountType(StockCount, DtpDefaultValue.Value.ToString("dd-MMM"));
                            }
                        }
                    }
                    if (flag == false)
                    {
                        btnSave.Enabled = false;
                        OMMessageBox.Show("Select Atleast One Checkbox", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        gvDetails.CurrentCell = gvDetails[ColIndex.chk, 0];
                    }
                    else
                        btnSave.Focus();
                }
                else
                {
                    cmbStockCountType.Focus();
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

        public void ClearFields()
        {
            while (gvDetails.Rows.Count > 0)
                gvDetails.Rows.RemoveAt(0);
            cmbStockCountType.SelectedIndex = 0;
            txtBarcode.Text = "";
            cmbDepartmentName.SelectedIndex = 0;
            cmbCategoryName.SelectedIndex = 0;
            cmbGroupNo2.SelectedIndex = 0;
            gvDetails.Enabled = true;
            btnSave.Enabled = false;
            pnlType.Visible = false;
            chkSelectAll.Checked = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            try
            {
                ClearFields();
                cmbGroupNo2.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                dbMStockCountSchedule = new DBMStockCountSchedule();
                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chkSelect].EditedFormattedValue) == true
                        && gvDetails.Rows[i].Cells[ColIndex.CountTypeNo].FormattedValue.ToString().Trim() != "")
                    {

                        mStockCountSchedule = new MStockCountSchedule();

                        mStockCountSchedule.PkSrNo = Convert.ToInt64(gvDetails.Rows[i].Cells[ColIndex.PkSrNo].Value);
                        mStockCountSchedule.ItemNo = Convert.ToInt64(gvDetails.Rows[i].Cells[ColIndex.ItemNo].Value);
                        mStockCountSchedule.CountTypeNo = Convert.ToInt64(gvDetails.Rows[i].Cells[ColIndex.CountTypeNo].Value);
                        mStockCountSchedule.CountScheduleDate = (gvDetails.Rows[i].Cells[ColIndex.CountSchedule].FormattedValue.ToString() == "") ? DBGetVal.ServerTime : Convert.ToDateTime(gvDetails.Rows[i].Cells[ColIndex.CountSchedule].Value);
                        mStockCountSchedule.CompanyNo = DBGetVal.FirmNo;
                        mStockCountSchedule.IsActive = true;
                        mStockCountSchedule.UserID = DBGetVal.UserID;
                        mStockCountSchedule.UserDate = DBGetVal.ServerTime;
                        dbMStockCountSchedule.AddMStockCountSchedule(mStockCountSchedule);
                        flag = true;
                        gvDetails.Rows[i].Cells[ColIndex.chk].Value = false;
                        gvDetails.Rows[i].Cells[ColIndex.chkSelect].Value = false;
                    }
                }
                if (flag == true)
                {
                    if (dbMStockCountSchedule.ExecuteNonQueryStatements() == true)
                    {
                        OMMessageBox.Show("Inventory Count Schedule Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        ClearFields();
                        cmbGroupNo2.Focus();
                    }
                }
                else
                {
                    OMMessageBox.Show("Select Atleast One Checkbox", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void gvDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColIndex.SrNo)
                {
                    e.Value = e.RowIndex + 1;
                }
                if (e.ColumnIndex == ColIndex.Value)
                {
                    if (gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].FormattedValue.ToString() != "")
                    {
                        if (Convert.ToInt64(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].Value) == StockCountType.Daily || Convert.ToInt64(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].Value) == StockCountType.NA)
                        {

                        }
                        else if (Convert.ToInt64(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].Value) == StockCountType.Weekly)
                        {
                            e.Value = Convert.ToDateTime(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountSchedule].Value).DayOfWeek.ToString().Trim();
                        }
                        else if (Convert.ToInt64(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].Value) == StockCountType.Monthly)
                        {
                            e.Value = Convert.ToDateTime(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountSchedule].Value).ToString("dd");
                        }
                        else if (Convert.ToInt64(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].Value) == StockCountType.Yearly)
                        {
                            e.Value = Convert.ToDateTime(gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountSchedule].Value).ToString("dd-MMM");
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbDefault_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnApply.Focus();
            }
        }

        private void DtpDefaultValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnApply.Focus();
            }
        }

        private void gvDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    if (pnlType.Visible == true)
                    {
                        cmbStockCountType.Focus();
                    }
                    else
                    {
                        gvDetails.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    gvDetails.Focus();
                }
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
                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {
                    gvDetails.Rows[i].Cells[ColIndex.chk].Value = chkSelectAll.Checked;
                    if (Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chk].EditedFormattedValue) == true && Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chkSelect].EditedFormattedValue) == true)
                    {
                        if (Convert.ToInt64(gvDetails.Rows[i].Cells[ColIndex.PkSrNo].EditedFormattedValue) == 0)
                        {
                            gvDetails.Rows[i].Cells[ColIndex.CountType].Value = "";
                            gvDetails.Rows[i].Cells[ColIndex.CountTypeNo].Value = "";
                            gvDetails.Rows[i].Cells[ColIndex.chkSelect].Value = false;
                            gvDetails.Rows[i].Cells[ColIndex.Value].Value = "";
                            gvDetails.Rows[i].Cells[ColIndex.CountSchedule].Value = "";
                        }
                    }
                }
                if (pnlType.Visible == false && chkSelectAll.Checked == true && gvDetails.Rows.Count > 0)
                {
                    pnlType.Visible = true;
                    cmbDefault.Visible = false;
                    DtpDefaultValue.Visible = false;
                    cmbStockCountType.SelectedIndex = 0;
                    btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                }
                else if (pnlType.Visible == true && chkSelectAll.Checked == false)
                {
                    pnlType.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbStockCountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                long CountType = ObjFunction.GetComboValue(cmbStockCountType);
                if (CountType == 0)
                {
                    cmbDefault.Visible = false;
                    DtpDefaultValue.Visible = false;
                    btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                    //cmbDefault.Focus();
                }
                else if (CountType == StockCountType.Daily || CountType == StockCountType.NA)
                {
                    cmbDefault.Visible = false;
                    DtpDefaultValue.Visible = false;
                    btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                    //btnApply.Focus();
                }
                else if (CountType == StockCountType.Weekly)
                {
                    cmbDefault.Visible = true;
                    DtpDefaultValue.Visible = false;
                    ObjFunction.FillWeek(cmbDefault);
                    cmbDefault.SelectedValue = dbMStockCountSchedule.GetWeek(ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).DayOfWeek.ToString().Trim());
                    //cmbDefault.Focus();
                    btnApply.Location = new Point(476, 9);
                }
                else if (CountType == StockCountType.Monthly)
                {
                    cmbDefault.Visible = true;
                    DtpDefaultValue.Visible = false;
                    ObjFunction.FillDays(cmbDefault);
                    cmbDefault.SelectedValue = Convert.ToInt64(ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).ToString("dd").ToUpper().Trim());
                    //cmbDefault.Focus();
                    btnApply.Location = new Point(476, 9);
                }
                else if (CountType == StockCountType.Yearly)
                {
                    cmbDefault.Visible = false;
                    DtpDefaultValue.Visible = true;
                    DtpDefaultValue.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                    DtpDefaultValue.CustomFormat = "dd-MMM";
                    DtpDefaultValue.Text = ObjQry.ReturnDate("Select DefaultValue From MStockCountType Where CountTypeNo=" + CountType + " ", CommonFunctions.ConStr).ToString("dd-MMM-yyyy");
                    //DtpDefaultValue.Focus();
                    btnApply.Location = new Point(476, 9);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void cmbGroupNo2_Leave(object sender, EventArgs e)
        {
            try
            {
                txtBarcode.Text = "";
                if (ObjFunction.GetComboValue(cmbGroupNo2) > 0)
                {
                    ObjFunction.FillCombo(cmbDepartmentName, " SELECT Distinct MItemGroup.StockGroupNo, MItemGroup.StockGroupName " +
                                                            " FROM MStockItems INNER JOIN MStockGroup ON mItemMaster.FKStockDeptNo = MItemGroup.StockGroupNo " +
                                                            " where ControlGroup=4 and mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " and MItemGroup.IsActive = 'True' order by StockGroupName");
                    ObjFunction.FillCombo(cmbCategoryName, "SELECT  Distinct MItemGroup.StockGroupNo, MItemGroup.StockGroupName " +
                                                            " FROM  MStockItems INNER JOIN MStockGroup ON mItemMaster.GroupNo1 = MItemGroup.StockGroupNo " +
                                                            " where ControlGroup=2 and mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbGroupNo2) + " and MItemGroup.IsActive = 'True' order by StockGroupName");
                }
                else
                {
                    ObjFunction.FillCombo(cmbDepartmentName, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 order by ItemGroupName");
                    ObjFunction.FillCombo(cmbCategoryName, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 order By ItemGroupName");
                }
                BindGrid();
              //  txtBarcode.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbDepartmentName_Leave(object sender, EventArgs e)
        {
            try
            {
                BindGrid();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void cmbCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                Validation();
                if (gvDetails.Rows.Count > 0)
                {
                    gvDetails.CurrentCell = gvDetails[ColIndex.chk, 0];
                    gvDetails.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void gvDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            bool flag = false;
            try
            {
                for (int i = 0; i < gvDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvDetails.Rows[i].Cells[ColIndex.chk].EditedFormattedValue) == true)
                    {

                        if (pnlType.Visible == false)
                        {
                            cmbDefault.Visible = false;
                            DtpDefaultValue.Visible = false;
                            cmbStockCountType.SelectedIndex = 0;
                            btnApply.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                        }
                        flag = true;
                        break;
                    }

                }

                if (Convert.ToBoolean(gvDetails.Rows[e.RowIndex].Cells[ColIndex.chk].EditedFormattedValue) == true && Convert.ToBoolean(gvDetails.Rows[e.RowIndex].Cells[ColIndex.chkSelect].EditedFormattedValue) == true)
                {
                    if (Convert.ToInt64(gvDetails.Rows[e.RowIndex].Cells[ColIndex.PkSrNo].EditedFormattedValue) == 0)
                    {
                        gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountType].Value = "";
                        gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountTypeNo].Value = "";
                        gvDetails.Rows[e.RowIndex].Cells[ColIndex.chkSelect].Value = false;
                        gvDetails.Rows[e.RowIndex].Cells[ColIndex.Value].Value = "";
                        gvDetails.Rows[e.RowIndex].Cells[ColIndex.CountSchedule].Value = "";
                    }

                }

                pnlType.Visible = flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


    }
}
