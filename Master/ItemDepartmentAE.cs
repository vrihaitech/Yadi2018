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
    public partial class ItemDepartmentAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        string DeptNm;
        DBMItemGroup dbItemGroup = new DBMItemGroup();
        MItemGroup mItemGroup = new MItemGroup();
        DataTable dtGroup = new DataTable();
        DataTable dtSearch = new DataTable();
        
        int cntRow;
        long No=0;
        public long DeptNo, ID;

        public ItemDepartmentAE()
        {
            InitializeComponent();
        }

        private void ItemDepartmentAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                KeyDownFormat(this.Controls);
                btnNew.Focus();
                DeptNm = "";
                dtSearch = ObjFunction.GetDataView("Select max(ItemGroupNo)as DeptNo From  MItemGroup where ControlGroup = 4").Table;
               
              
               
                if (dtSearch.Rows.Count > 0)
                {
                    if (ItemDepartment.RequestDeptNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[0]["DeptNo"].ToString());
                    else
                        ID = ItemDepartment.RequestDeptNo;
                    FillFields();
                    //SetNavigation();
                }
                setDisplay(true);
                //btnNew.Focus();

               
                
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

       

        public void SaveFileds()
        {
            try
            {
                if (Validations() == true)
                {
                    dbItemGroup = new DBMItemGroup();
                    mItemGroup = new MItemGroup();
                    mItemGroup.ItemGroupNo = ID;

                  
                    mItemGroup.ItemGroupName = txtDeptName.Text.Trim();
                    mItemGroup.LanguageName = "";
                    mItemGroup.IsActive = chkActive.Checked;
                    mItemGroup.UserId = DBGetVal.UserID;
                    mItemGroup.UserDate = DBGetVal.ServerTime.Date;
                    mItemGroup.CompanyNo = DBGetVal.FirmNo;
                    mItemGroup.ControlGroup = 4;
                    mItemGroup.ControlSubGroup = 0;
                    if (dbItemGroup.AddMItemGroup(mItemGroup) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Department Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select ItemGroupNo From MItemGroup order by ItemGroupName").Table;
                            ID = ObjQry.ReturnLong("Select Max(ItemGroupNo) From  MItemGroup where ControlGroup= 4 ", CommonFunctions.ConStr);
                            //ObjFunction.FillCombo(cmbControlGroup, "SELECT     DepartmentNo, DepartmentName FROM         MStockDepartment WHERE     (IsActive = 'True') ORDER BY DepartmentName");
                            //SetNavigation();
                            FillFields();
                        }
                        else
                        {
                            OMMessageBox.Show("Department Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillFields();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("DepartMent not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            ItemDepartment.RequestDeptNo = 0;            
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFileds();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtDeptName, ""); 
            if (txtDeptName.Text.Trim() == "")
            {

                OMMessageBox.Show("Enter Department Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                EP.SetIconAlignment(txtDeptName, ErrorIconAlignment.MiddleRight);
                txtDeptName.Focus();
            }            
            else if (DeptNm != txtDeptName.Text.Trim())
            {
               // if (ObjQry.ReturnInteger("Select Count(*) from MStockDepartment where DepartmentName = '" + txtDeptName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                if (ObjQry.ReturnInteger("Select Count(*) from MItemGroup where ItemGroupName = '" + txtDeptName.Text.Trim().Replace("'", "''") + "' and itemgroupno NOT IN(" + ID + ")", CommonFunctions.ConStr) != 0)
                {
                    OMMessageBox.Show("Duplicate Deparment Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    EP.SetIconAlignment(txtDeptName, ErrorIconAlignment.MiddleRight);
                    txtDeptName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void DepartmentAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ItemDepartment.RequestDeptNo = 0;
            DeptNm = "";
        }

       
        private void FillFields()
        {
            try
            {
                EP.SetError(txtDeptName, "");
                mItemGroup = dbItemGroup.ModifyMStockGroupByID(ID);
                DeptNm = mItemGroup.ItemGroupName;
                txtDeptName.Text = mItemGroup.ItemGroupName;
                chkActive.Checked = mItemGroup.IsActive;
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

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 4  ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 4   ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(ItemGroupNo),0)as GroupNo From MItemGroup where  ControlGroup= 4 and ItemGroupNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
                else if (type == 4)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 4 and ItemGroupNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            FillFields();
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
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }
            //else if (e.KeyCode == Keys.Escape)
            //{
            //    BtnExit_Click(sender, e);
            //}
        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                chkActive.Checked = true;
                txtDeptName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
          //  chkActive.Checked = true;
            txtDeptName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new ItemDepartment();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbItemGroup = new DBMItemGroup();
                mItemGroup = new MItemGroup();

                mItemGroup.ItemGroupNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbItemGroup.DeleteMItemGroup(mItemGroup) == true)
                    {
                        OMMessageBox.Show("Department Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillFields();
                    }
                    else
                    {
                        OMMessageBox.Show("Department not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        //private void chkActive_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkActive.Checked == true)
        //        chkActive.Text = "Open";
        //    else
        //        chkActive.Text = "Close";
        //}

        private void txtDeptName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkActive.Focus();
            }

        }

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

    }
}
