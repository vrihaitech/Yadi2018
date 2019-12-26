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

namespace Yadi.Master
{
    public partial class CategoryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMStockCategory dbMStockCategory = new DBMStockCategory();
        MStockCategory mStockCategory = new MStockCategory();
        string CategoryNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long CategoryNo, ID;

        public CategoryAE()
        {
            InitializeComponent();
        }

        private void FillControls()
        {
            try
            {
                EP.SetError(txtCategoryName, "");
                EP.SetError(cmbControlGroup, "");

                mStockCategory = dbMStockCategory.ModifyMStockCategoryByID(ID);
                txtCategoryName.Text = mStockCategory.CategoryName;
                cmbControlGroup.SelectedValue = mStockCategory.ControlGroup.ToString();
                cmbDepartmentName.SelectedValue = mStockCategory.DepartmentNo.ToString();
                CategoryNm = mStockCategory.CategoryName;
                chkActive.Checked = mStockCategory.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {
                    dbMStockCategory = new DBMStockCategory();
                    mStockCategory = new MStockCategory();
                    mStockCategory.CategoryNo = ID;
                    mStockCategory.CategoryName = txtCategoryName.Text.Trim().ToUpper();
                    mStockCategory.ControlGroup = ObjFunction.GetComboValue(cmbControlGroup);
                    mStockCategory.DepartmentNo = ObjFunction.GetComboValue(cmbDepartmentName);
                    mStockCategory.IsActive = chkActive.Checked;
                    mStockCategory.UserId = DBGetVal.UserID;
                    mStockCategory.UserDate = DBGetVal.ServerTime.Date;
                    mStockCategory.CompanyNo = DBGetVal.FirmNo;

                    if (dbMStockCategory.AddMStockCategory(mStockCategory) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Category Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select CategoryNo From MStockCategory order by CategoryName ").Table;
                            ID = ObjQry.ReturnLong("Select Max(CategoryNo) FRom MStockCategory", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Category Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Category not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Category.RequestCategoryNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtCategoryName, "");
            EP.SetError(cmbControlGroup, "");
            EP.SetError(cmbDepartmentName, "");
            if (txtCategoryName.Text.Trim() == "")
            {
                EP.SetError(txtCategoryName, "Enter Category Name");
                EP.SetIconAlignment(txtCategoryName, ErrorIconAlignment.MiddleRight);
                txtCategoryName.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbControlGroup) <= 0)
            {
                EP.SetError(cmbControlGroup, "Select Control Group");
                EP.SetIconAlignment(cmbControlGroup, ErrorIconAlignment.MiddleRight);
                cmbControlGroup.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbDepartmentName) <= 0)
            {
                EP.SetError(cmbDepartmentName, "Select Department");
                EP.SetIconAlignment(cmbDepartmentName, ErrorIconAlignment.MiddleRight);
                cmbDepartmentName.Focus();
            }
            else
                flag = true;
            return flag;
        }

        private void CityAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            CategoryNm = "";
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {
                long No = 0;
                if (type == 5)
                {
                    No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    ID = No;
                }
                else if (type == 1)
                {
                    No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                    cntRow = 0;
                    ID = No;
                }
                else if (type == 2)
                {
                    No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    cntRow = dtSearch.Rows.Count - 1;
                    ID = No;
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
                        ID = No;
                    }

                }
                FillControls();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID)
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
            btnDelete.Visible = flag;
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
                if (btnSave.Visible) BtnSave_Click(sender, e);
            }

        }
        #endregion

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbMStockCategory = new DBMStockCategory();
                mStockCategory = new MStockCategory();

                mStockCategory.CategoryNo = ID;
                if (ObjQry.ReturnLong("Select Count(*) from MStockGroup Where StockGroupNo=" + ID + "", CommonFunctions.ConStr) > 0)
                {
                    OMMessageBox.Show("Sorry You Can not delete this record..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (dbMStockCategory.DeleteMStockCategory(mStockCategory) == true)
                        {
                            OMMessageBox.Show("Category Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Category not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }

                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Category();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            txtCategoryName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                chkActive.Checked = true;
                txtCategoryName.Focus();
                cmbControlGroup.SelectedIndex = 0;
                cmbDepartmentName.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtCategoryName, "");
                if (txtCategoryName.Text.Trim() != "")
                {
                    if (CategoryNm != txtCategoryName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MCategory where CategoryName  = '" + txtCategoryName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtCategoryName, "Duplicate Category Name");
                            EP.SetIconAlignment(txtCategoryName, ErrorIconAlignment.MiddleRight);
                            txtCategoryName.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CategoryAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                ObjFunction.FillCombo(cmbControlGroup, "SELECT  CategoryNo, CategoryName FROM   MStockCategory WHERE  IsActive='true'  ORDER BY CategoryName");
                ObjFunction.FillCombo(cmbDepartmentName, "SELECT DepartmentNo, DepartmentName FROM MStockDepartment WHERE IsActive = 'True' ORDER BY DepartmentName");
                CategoryNm = "";
                dtSearch = ObjFunction.GetDataView("SELECT CategoryNo, CategoryName FROM MStockCategory order by CategoryName").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (Category.RequestCategoryNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = Category.RequestCategoryNo;
                    FillControls();
                    SetNavigation();
                }
                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
    }
}
