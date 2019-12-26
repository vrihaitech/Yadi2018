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
    public partial class ItemGroupAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMItemGroup dbItemGroup = new DBMItemGroup();
        MItemGroup mItemGroup = new MItemGroup();
        DataTable dtGroup = new DataTable();
        string StockGroupNm, LangName;
        DataTable dtSearch = new DataTable();
        long ID;
        bool FlagBilingual;
        long No = 0;
        public long ShortID = 0;
        public ItemGroupAE()
        {
            InitializeComponent();
        }
        public ItemGroupAE(long shortid)
        {
            InitializeComponent();
            ShortID = shortid;
        }
        public ItemGroupAE(int GroupType)
        {
            InitializeComponent();
            ShortID = GroupType;

        }

        private void ItemGroupAE_Load(object sender, EventArgs e)
        {
            try
            {

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                StockGroupNm = "";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    FlagBilingual = true;
                    txtLanguage.Visible = true; btnLangDesc.Visible = true;
                    labellan.Visible = true;
                    txtLanguage.Font = ObjFunction.GetLangFont();
                }
                else
                {
                    FlagBilingual = false;
                    txtLanguage.Visible = false; btnLangDesc.Visible = false;
                    labellan.Visible = false;
                }
                if (ShortID ==3)
                {
                    dtSearch = ObjFunction.GetDataView(" Select max(isnull(ItemGroupNo,0)) as ItemGroupNo FROM MItemGroup where ControlGroup= 3 group by ItemGroupNo").Table;

                    if (ItemGroup.RequestGroupNo != 0)
                    {
                        StockGroupNm = "";
                        FillFields();
                        dtSearch = ObjFunction.GetDataView("Select ItemGroupNo From MItemGroup where ControlGroup= 3").Table;
                        //SetNavigation();
                        setDisplay(true);
                    }
                    else
                    {
                        setDisplay(false);
                    }
                    KeyDownFormat(this.Controls);
                    if (dtSearch.Rows.Count > 0)
                    {
                        if (ItemGroup.RequestGroupNo == 0)
                            //ID = Convert.ToInt64(dtSearch.Rows[0]["ItemGroupNo"].ToString());
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());

                        else
                            ID = ItemGroup.RequestGroupNo;
                        FillFields();
                        // SetNavigation();
                    }
                    setDisplay(true);
                    btnLangDesc.Visible = false;
                    btnNew.Focus();
                    KeyDownFormat(this.Controls);
                }
                else if (ShortID==0)
                {
                    NavigationDisplay(2);
                }
                else
                {
                    btnNew_Click(sender, new EventArgs());
                    // setDisable(false);
                    // BtnExit.Visible = true;
                    //txtCityName.Focus();
                    chkActive.Checked = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillFields()
        {
            try
            {
                EP.SetError(txtStockGroupName, "");

                mItemGroup = dbItemGroup.ModifyMStockGroupByID(ID);
                StockGroupNm = mItemGroup.ItemGroupName;
                LangName = mItemGroup.LanguageName;
                txtStockGroupName.Text = mItemGroup.ItemGroupName;
                txtLanguage.Text = mItemGroup.LanguageName;

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

        public void SaveFileds()
        {
            try
            {
                if (Validations() == true)
                {
                    dbItemGroup = new DBMItemGroup();
                    mItemGroup = new MItemGroup();
                    mItemGroup.ItemGroupNo = ID;

                    mItemGroup.ItemGroupName = txtStockGroupName.Text.Trim();
                    mItemGroup.LanguageName = txtLanguage.Text.Trim();
                    mItemGroup.IsActive = chkActive.Checked;
                    mItemGroup.UserId = DBGetVal.UserID;
                    mItemGroup.UserDate = DBGetVal.ServerTime.Date;
                    mItemGroup.CompanyNo = DBGetVal.FirmNo;
                    mItemGroup.ControlGroup = 3;
                    mItemGroup.ControlSubGroup = 8;
                    if (dbItemGroup.AddMItemGroup(mItemGroup) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Group Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select ItemGroupNo From MItemGroup order by ItemGroupName").Table;
                            ID = ObjQry.ReturnLong("Select Max(ItemGroupNo) From MItemGroup where ControlGroup= 3 ", CommonFunctions.ConStr);

                       
                            FillFields();
                        }
                        else
                        {
                            OMMessageBox.Show("Group Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillFields();
                        }
                        DBGetFlag.Brand = true;
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Group not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            EP.SetError(txtStockGroupName, "");
            if (txtStockGroupName.Text.Trim() == "")
            {

                OMMessageBox.Show("Enter Group Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                EP.SetIconAlignment(txtStockGroupName, ErrorIconAlignment.MiddleRight);
                txtStockGroupName.Focus();
            }

            else
                flag = true;
            return flag;
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
                txtStockGroupName.Focus();
                ShortID = ID;
                ID = 0;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                chkActive.Checked = true;
                txtStockGroupName.Focus();
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ID == 0)
            { //  NavigationDisplay(2);
                // NavigationDisplay(2);
                ID = ShortID;
                FillFields();
            }
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);

            btnNew.Focus();
         

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            btnNew.Focus();
            txtStockGroupName.Focus();
            if (FlagBilingual == true)
            {
                btnLangDesc.Visible = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new ItemGroup();
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

        private void BtnExit_Click(object sender, EventArgs e)
        {
            ItemDepartment.RequestDeptNo = 0;
            this.Close();
        }



        private void txtStockGroupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //txtItemLang.Text = ObjFunction.ChecklLangVal(txtItemName.Text);
                txtStockGroupName_Leave(sender, new EventArgs());
                txtLanguage.Focus();
            }
        }

        private void txtLanguage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtLanguage.Text.Trim() == "")
                {

                    btnLangDesc.Focus();
                }
                else
                {
                    BtnSave.Focus();
                }

            }
        }

        private void chkActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnSave.Focus();
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
            else if (e.KeyCode == Keys.Escape)
            {
                BtnExit_Click(sender, e);
            }
        }
        #endregion


        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 3").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup = 3 ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GroupNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(ItemGroupNo),0)as GroupNo From MItemGroup where ControlGroup= 3 and ItemGroupNo >" + ID).Table;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(ItemGroupNo),0)as GroupNo From MItemGroup where  ControlGroup= 3 and  ItemGroupNo <" + ID).Table;
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
        #endregion

        private void btnLangDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLanguage.Text.Trim().Length > 0)
                {
                    //string val = ObjFunction.ChecklLangVal(txtItemName.Text.Trim());
                    //if (val == "")
                    //{
                    frmkb = new Utilities.KeyBoard(1, txtStockGroupName.Text.Trim(), txtLanguage.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLanguage.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();

                    }
                    //}
                    //else
                    //    txtLangFullDesc.Text = val;
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtStockGroupName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtStockGroupName.Text.Trim(), txtLanguage.Text, "", "");
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
        private void txtStockGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkActive.Focus();
            }

        }

        private void txtStockGroupName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtStockGroupName, "");
                if (txtStockGroupName.Text.Trim() != "")
                {
                    //txtLangFullDesc.Text = "";
                    if (StockGroupNm != txtStockGroupName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MItemGroup where ItemGroupName = '" + txtStockGroupName.Text.Trim() + "' " + " AND ControlGroup = 3 AND ItemGroupNo not in (" + ID + ") ", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtStockGroupName, "Duplicate category Name");
                            EP.SetIconAlignment(txtStockGroupName, ErrorIconAlignment.MiddleRight);
                            txtStockGroupName.Focus();
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

                        txtStockGroupName.Text= txtStockGroupName.Text.ToUpper();
                    }
                    else
                    {
                        if (FlagBilingual == true)
                        {
                            //txtLangFullDesc.Focus();
                            if (txtStockGroupName.Text.Trim().Length == 0)
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
                    txtStockGroupName.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }








    }
}
