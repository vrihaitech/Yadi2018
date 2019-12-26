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
    public partial class ItemCategoryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMItemGroup dbMItemGroup = new DBMItemGroup();
        MItemGroup mItemGroup = new MItemGroup();
        DataTable dtGroup = new DataTable();
        string StockGroupNm;
        public string MsgName = "";
        DataTable dtSearch = new DataTable();
        int GrType;
        long ID,  No=0;
        bool FlagBilingual;

        public ItemCategoryAE()
        {
            InitializeComponent();
        }

        public ItemCategoryAE(int GroupType)
        {
            InitializeComponent();

            if (GroupType == 2)
            {
                GrType = GroupType;
                this.Text = "Category Entry";
                this.Name = "Category Entry";
                this.MsgName = "Category";
            }
            //else if (GroupType == 3)
            //{
            //    GrType = GroupType;
            //    this.Text = "Brand Entry";
            //    this.Name = "Brand Enttry";
            //    this.MsgName = "Brand ";
            //}
            //else if (GroupType == 4)
            //{
            //    GrType = GroupType;
            //    this.Text = "Department Entry";
            //    this.Name = "Department";
            //    this.MsgName = "Department Name";
            //}
        }

        private void ItemCategoryAE_Load(object sender, EventArgs e)
        {

           
            try
            {

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                btnNew.Focus();
                formatpicture();
                //ObjFunction.FillList(lstdepartment, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 ORDER BY ItemGroupName");
                //   lstGroup1.SelectedIndex = 1;     
               // txtdeptName.Text = lstdepartment.Text.ToString();

               // StockGroupNm = "";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    FlagBilingual = true;
                    txtLanguage.Visible = true; //btnLangDesc.Visible = true;
                     label4.Visible = true;

                    txtLanguage.Font = ObjFunction.GetLangFont();
                    btnNew.Focus();
                }
                else
                {
                    FlagBilingual =false;
                    txtLanguage.Visible = false; //btnLangDesc.Visible = false;
                     label4.Visible = false;
                }

                dtSearch = ObjFunction.GetDataView("Select max(ItemGroupNo)as categoryNo From  MItemGroup where ControlGroup = 2 ").Table;


               // KeyDownFormat(this.Controls);
                if (dtSearch.Rows.Count > 0)
                {
                    if (ItemCategory.RequestGroupNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[0]["categoryNo"].ToString());
                    else
                        ID = ItemCategory.RequestGroupNo;
                    FillFields();
                    //SetNavigation();
                }
                setDisplay(true);
               
                
                
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
        public void formatpicture()
        {
            //pnldepart.Top = txtdeptName.Bottom;
            //pnldepart.Width = txtdeptName.Width;
            //pnldepart.Height = 85;
            //lstdepartment.Top = pnldepart.Top - 28;
            //lstdepartment.Height = pnldepart.Height - 5;
        }
        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
               
                ID = 0;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtCategoryName.Focus();
                chkActive.Checked = true;
                if (FlagBilingual == true)
                {
                    btnLangDesc.Visible = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFileds();

        }
        public void SaveFileds()
        {
            try
            {

                if (Validations() == true)
                {
                    dbMItemGroup = new DBMItemGroup();
                    mItemGroup = new MItemGroup();
                    mItemGroup.ItemGroupNo = ID;

                    mItemGroup.ItemGroupName = txtCategoryName.Text.Trim();
                    mItemGroup.LanguageName = txtLanguage.Text.Trim();
                    mItemGroup.IsActive = chkActive.Checked;
                    mItemGroup.UserId = DBGetVal.UserID;
                    mItemGroup.UserDate = DBGetVal.ServerTime.Date;
                    mItemGroup.CompanyNo = DBGetVal.FirmNo;
                    mItemGroup.ControlGroup = 2;
                    //mItemGroup.ControlSubGroup = Convert.ToInt64(lstdepartment.SelectedValue);
                   // lstdepartment.SelectedValue = mItemGroup.ControlSubGroup;
                
                    if (dbMItemGroup.AddMItemGroup(mItemGroup) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Category Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select ItemGroupNo From MItemGroup order by ItemGroupName").Table;
                            ID = ObjQry.ReturnLong("Select Max(ItemGroupNo) From MItemGroup where ControlGroup = 2", CommonFunctions.ConStr);
                            //ObjFunction.FillCombo(cmbControlGroup, "SELECT     DepartmentNo, DepartmentName FROM  MStockDepartment WHERE (IsActive = 'True') ORDER BY DepartmentName");
                            //SetNavigation();
                            FillFields();
                        }
                        else
                        {
                            OMMessageBox.Show("Category Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillFields();
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
        public bool Validations()
        {
            bool flag = false;
           // EP.SetError(txtdeptName, "");
            EP.SetError(txtCategoryName, "");
            //if (txtdeptName.Text.Trim() == "")
            //{

            //    OMMessageBox.Show("Enter dept Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            //    EP.SetIconAlignment(txtdeptName, ErrorIconAlignment.MiddleRight);
            //    txtdeptName.Focus();
            //}
             if (txtCategoryName.Text.Trim() == "")
            {

                OMMessageBox.Show("Enter category Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                EP.SetIconAlignment(txtCategoryName, ErrorIconAlignment.MiddleRight);
                txtCategoryName.Focus();
            }
            else if (StockGroupNm == txtCategoryName.Text)//if (txtCategoryName.Text.Trim() != "")
            {
               // if (ObjQry.ReturnInteger("Select Count(*) from MItemGroup where ItemGroupName = '" + txtCategoryName.Text.Trim().Replace("'", "''") + "' and ControlGroup = 2 AND  ControlSUBGroup  IN (" + lstdepartment.SelectedValue + ") ", CommonFunctions.ConStr) != 0)

                    if (ObjQry.ReturnInteger("Select Count(*) from MItemGroup where ItemGroupNO!=" + ID + " and ItemGroupName = '" + txtCategoryName.Text.Trim().Replace("'", "''") + "' and ControlGroup = 2  ", CommonFunctions.ConStr) != 0)
                {
                    OMMessageBox.Show("Duplicate category Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    EP.SetIconAlignment(txtCategoryName, ErrorIconAlignment.MiddleRight);
                    txtCategoryName.Focus();
                }

                else
                   flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void FillFields()
        {
            try
            {
                //EP.SetError(txtdeptName, "");
                EP.SetError(txtCategoryName, "");
                mItemGroup = dbMItemGroup.ModifyMStockGroupByID(ID);
                StockGroupNm = mItemGroup.ItemGroupName;
               // LangName = mItemGroup.LanguageName;
                txtCategoryName.Text = mItemGroup.ItemGroupName;
                txtLanguage.Text = mItemGroup.LanguageName;
               // lstdepartment.SelectedValue = mItemGroup.ControlSubGroup.ToString();
                //txtdeptName.Text = lstdepartment.Text;

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

        private void txtdeptName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //if (txtdeptName.Text == "")
                //{
                //    pnldepart.Visible = true;
                //    lstdepartment.Focus();
                //    lstdepartment.SelectedIndex = 0;
                //}
                //else
                //{
                //    pnldepart.Visible = false;
                //    txtCategoryName.Focus();
                //}
            }
           
           
        }

        private void lstdepartment_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{

                //    e.SuppressKeyPress = true;
                //    txtdeptName.Text = lstdepartment.Text;
                //    pnldepart.Visible = false;
                //    txtCategoryName.Focus();
                   


                //}
                //else if (e.KeyCode == Keys.Escape)
                //{
                //    e.SuppressKeyPress = true;
                //    txtCategoryName.Focus();

                //}

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
          //  ItemDepartment.RequestDeptNo = 0;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
           // pnldepart.Visible = false;
            btnNew.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            txtCategoryName.Focus();
            if (FlagBilingual == true)
            {
                btnLangDesc.Visible = true;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new ItemCategory();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbMItemGroup = new DBMItemGroup();
                mItemGroup = new MItemGroup();

                mItemGroup.ItemGroupNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbMItemGroup.DeleteMItemGroup(mItemGroup) == true)
                    {
                        OMMessageBox.Show("Category Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillFields();
                    }
                    else
                    {
                        OMMessageBox.Show("Category not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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

        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 2  ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 2  ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(ItemGroupNo),0)as GroupNo From MItemGroup where  ControlGroup= 2 and ItemGroupNo >" + ID).Table;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 2 and ItemGroupNo <" + ID).Table;
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

        //private void txtCategoryName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        chkActive.Focus();
        //    }

        //}

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (FlagBilingual == true)
                {
                    txtLanguage.Focus();
                }
                else

                BtnSave.Focus();
            }
        }

        private void txtCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtCategoryName, "");
                if (txtCategoryName.Text.Trim() != "")
                {
                    //txtLangFullDesc.Text = "";
                    if (StockGroupNm != txtCategoryName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MItemGroup where ItemGroupName = '" + txtCategoryName.Text.Trim().Replace("'", "''") + "' and ControlGroup = 2  ", CommonFunctions.ConStr) != 0)
                        {
                            OMMessageBox.Show("Duplicate category Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            EP.SetIconAlignment(txtCategoryName, ErrorIconAlignment.MiddleRight);
                            txtCategoryName.Focus();
                        }
                        else if (FlagBilingual == true)
                        {

                            if (txtLanguage.Text.Trim().Length == 0)
                            {
                                btnLangDesc_Click(btnLangDesc, null);
                            }
                        }

                        else
                            BtnSave.Focus();


                    }
                    else
                    {
                        if (FlagBilingual == true)
                        {
                            //txtLangFullDesc.Focus();
                            if (txtCategoryName.Text.Trim().Length == 0)
                            {
                                btnLangDesc_Click(btnLangDesc, null);
                            }
                        }
                        else
                            BtnSave.Focus();
                    }
                }
                else
                {
                    txtCategoryName.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnLangDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLanguage.Text.Trim().Length > 0)
                {
                   
                    frmkb = new Utilities.KeyBoard(1, txtCategoryName.Text.Trim(), txtLanguage.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLanguage.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                        
                    }
                   // txtLanguage.Text = val;
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtCategoryName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtCategoryName.Text.Trim(), txtLanguage.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLanguage.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                            
                        }
                    }
                    else
                        txtLanguage.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtCategoryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //txtItemLang.Text = ObjFunction.ChecklLangVal(txtItemName.Text);
                txtCategoryName_Leave(sender, new EventArgs());
                txtLanguage.Focus();
            }

        }

        private void txtLanguage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void pnlMain_Click(object sender, EventArgs e)
        {
            if (pnldepart.Visible == true)
            {
                pnldepart.Visible = false;
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (pnldepart.Visible == true)
            {
                pnldepart.Visible = false;
            }
        }
    }
}
