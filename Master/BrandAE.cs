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
    public partial class BrandAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMItemGroup dbMItemGroup = new DBMItemGroup();
        MItemGroup mItemGroup = new MItemGroup();

     
        string StockGroupNm;//, MsgName;
        DataTable dtSearch = new DataTable();
        DataTable dtGroup = new DataTable();
        bool IsLeave = false;
        //int cntRow, nw;
        long ID, DepartmentNo, CategoryNo;
        public long ShortID = 0;

        public BrandAE()
        {
            InitializeComponent();
        }

        public BrandAE(long DeptNo,long CatgNo)
        {
            InitializeComponent();
            DepartmentNo = DeptNo;
            CategoryNo = CatgNo;            
            
        }

        private void BrandAE_Load(object sender, EventArgs e)
        {
            try
            {
                StockGroupNm = "";
                ObjFunction.FillCombo(cmbManufacturerCompanyName, "Select MfgCompNo,MfgCompName From MManufacturerCompany Where IsActive = 'True' Order by MfgCompName");
                if (cmbManufacturerCompanyName.Items.Count <= 2)
                {
                    cmbManufacturerCompanyName.SelectedIndex = 1;   
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    lblLanguage.Visible = true; txtLanguage.Visible = true; btnLangDesc.Visible = true;
                    txtLanguage.Font = ObjFunction.GetLangFont();
                }
                else
                {
                    lblLanguage.Visible = false; txtLanguage.Visible = false; btnLangDesc.Visible = false;
                }
                dtGroup = ObjFunction.GetDataView("Select StockGroupName from MStockGroup Where ControlGroup=3 order by StockGroupName").Table;
                KeyDownFormat(this.Controls);
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
                    dbMItemGroup = new DBMItemGroup();
                    mItemGroup= new MItemGroup();
                   mItemGroup.ItemGroupNo  =  ID;
                    mItemGroup.ItemGroupName  = txtStockGroupName.Text.Trim().ToUpper();
                    mItemGroup.ControlSubGroup = CategoryNo;
                    mItemGroup.LanguageName = txtLanguage.Text;
                    mItemGroup.ControlGroup = 3;
                    mItemGroup.MfgCompNo = ObjFunction.GetComboValue(cmbManufacturerCompanyName);
                    mItemGroup.IsActive = true;
                    mItemGroup.UserId = DBGetVal.UserID;
                    mItemGroup.UserDate = DBGetVal.ServerTime.Date;
                    mItemGroup.CompanyNo = DBGetVal.FirmNo;
                    mItemGroup.Margin = 0;
                    mItemGroup.ActualStockGroupName = txtStockGroupName.Text.Trim().ToUpper();
                    if (dbMItemGroup.AddMItemGroup(mItemGroup) == true)
                    {
                        OMMessageBox.Show("Brand Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        dtSearch = ObjFunction.GetDataView("Select StockGroupNo From MStockGroup Where  ControlGroup=" + 3 + " ORDER BY StockGroupName").Table;
                        if (ID == 0)
                            ID = ObjQry.ReturnLong("Select Max(StockGroupNo) FRom MStockGroup", CommonFunctions.ConStr);
                        ShortID = ID;
                        DBGetFlag.Brand = true;
                        this.Close();
                    }
                    else
                    {
                        OMMessageBox.Show("Brand not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }    

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtStockGroupName, "");
            EP.SetError(cmbManufacturerCompanyName, "");
           
            if (txtStockGroupName.Text.Trim() == "")
            {
                EP.SetError(txtStockGroupName, "Enter Brand");
                EP.SetIconAlignment(txtStockGroupName, ErrorIconAlignment.MiddleRight);
                txtStockGroupName.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbManufacturerCompanyName) <= 0)
            {
                EP.SetError(cmbManufacturerCompanyName, "Select Company Name");
                EP.SetIconAlignment(cmbManufacturerCompanyName, ErrorIconAlignment.MiddleRight);
                cmbManufacturerCompanyName.Focus();
            }
            else if (StockGroupNm != txtStockGroupName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MStockGroup where StockGroupName = '" + txtStockGroupName.Text.Trim() + "' AND ControlGroup=" + 3 + "", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtStockGroupName, "Duplicate Brand");
                    EP.SetIconAlignment(txtStockGroupName, ErrorIconAlignment.MiddleRight);
                    txtStockGroupName.Focus();
                }
                else
                    flag = true;
            }
            
            else
                flag = true;

            return flag;
        }

        private void StockGroupAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            StockGroupNm = "";
        }

        private void txtStockGroupName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtStockGroupName, "");
                if (IsLeave == false)
                {
                    if (txtStockGroupName.Text.Trim() != "")
                    {
                        if (StockGroupNm != txtStockGroupName.Text.Trim())
                        {
                            ID = ObjQry.ReturnInteger("SELECT ISNULL(StockGroupNo,0) FROM MStockGroup WHERE  (StockGroupName = '" + txtStockGroupName.Text.Trim() + "'  and  ControlGroup=3 )", CommonFunctions.ConStr);
                            if (ID != 0)
                            {
                                mItemGroup= dbMItemGroup.ModifyMStockGroupByID(ID);
                                StockGroupNm = mItemGroup.ItemGroupName;
                                txtStockGroupName.Text = mItemGroup.ItemGroupName;
                                txtLanguage.Text = mItemGroup.LanguageName;
                                ObjFunction.FillCombo(cmbManufacturerCompanyName, "Select MfgCompNo,MfgCompName From MManufacturerCompany Where IsActive = 'True' OR MfgCompNo=" + mItemGroup.MfgCompNo + " Order by MfgCompName");
                                cmbManufacturerCompanyName.SelectedValue = ObjQry.ReturnLong("Select MfgCompNo From MManufacturerCompany Where MfgCompNo=" + mItemGroup.MfgCompNo, CommonFunctions.ConStr);

                                //FillControls();
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                                    txtLanguage.Focus();
                                else
                                    BtnSave.Focus();
                            }
                            else if (ObjQry.ReturnInteger("Select Count(*) from MStockGroup where StockGroupName = '" + txtStockGroupName.Text.Trim() + "' AND ControlGroup=" + 3 + "", CommonFunctions.ConStr) != 0)
                            {
                                EP.SetError(txtStockGroupName, "Duplicate ");// + MsgName
                                EP.SetIconAlignment(txtStockGroupName, ErrorIconAlignment.MiddleRight);
                                txtStockGroupName.Focus();
                            }
                            else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                            {
                                txtLanguage.Focus();
                                if (txtLanguage.Text.Trim().Length == 0)
                                {
                                    btnLangDesc_Click(btnLangDesc, null);
                                }
                            }
                            else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == false)
                            {
                                cmbManufacturerCompanyName.Focus();
                                //BtnSave.Focus();
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
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }

        }
        #endregion     

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            this.Close();
        }

        private void btnLangDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLanguage.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtStockGroupName.Text.Trim(), txtLanguage.Text, "", "");
                }
                else
                {
                    frmkb = new Utilities.KeyBoard(4, txtStockGroupName.Text.Trim(), txtLanguage.Text, "", "");
                }
                ObjFunction.OpenForm(frmkb);
                if (frmkb.DS == DialogResult.OK)
                {
                    txtLanguage.Text = frmkb.strLanguage.Trim(); txtStockGroupName.Focus();
                    frmkb.Close();
                }
                else
                {
                    frmkb.Close();
                    txtLanguage.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtStockGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    IsLeave = false;
                    lstBrandName.Visible = false;
                    txtStockGroupName_Leave(sender, e);
                }

                if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    if (lstBrandName.Visible)
                    {
                        IsLeave = true;
                        lstBrandName.Focus();
                    }
                    else
                        IsLeave = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
           
        }

        private void txtStockGroupName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] dr = null;
                if (txtStockGroupName.Text.Trim() != "")
                {
                    dr = dtGroup.Select("StockGroupname like '" + txtStockGroupName.Text.Trim().Replace("'", "''") + "%'");
                    if (dr.Length > 0)
                    {
                        lstBrandName.Visible = true;
                        lstBrandName.Items.Clear();
                        for (int i = 0; i < dr.Length; i++)
                        {
                            lstBrandName.Items.Add(dr[i].ItemArray[0].ToString());
                        }
                    }
                    else
                    {
                        lstBrandName.Visible = false;
                    }
                }
                else
                {
                    lstBrandName.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            
        }

        private void lstBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtStockGroupName.Text = lstBrandName.Text;
                    //txtStockGroupName.Focus();
                    long GrNo = ObjQry.ReturnLong("Select StockGroupNo from MStockGroup where StockGroupName = '" + txtStockGroupName.Text.Trim() + "' AND ControlGroup=3 ", CommonFunctions.ConStr);
                    ID = GrNo;
                    mItemGroup= dbMItemGroup.ModifyMStockGroupByID(ID);
                    StockGroupNm = mItemGroup.ItemGroupName;
                    txtStockGroupName.Text = mItemGroup.ItemGroupName;
                    txtLanguage.Text = mItemGroup.LanguageName;
                    ObjFunction.FillCombo(cmbManufacturerCompanyName, "Select MfgCompNo,MfgCompName From MManufacturerCompany Where IsActive = 'True' OR MfgCompNo=" + mItemGroup.MfgCompNo + " Order By MfgCompName");
                    cmbManufacturerCompanyName.SelectedValue = ObjQry.ReturnLong("Select MfgCompNo From MManufacturerCompany Where MfgCompNo=" + mItemGroup.MfgCompNo, CommonFunctions.ConStr);
                    BtnSave.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtStockGroupName.Focus();
                lstBrandName.Visible = false;
            }
        }

        private void lstBrandName_Leave(object sender, EventArgs e)
        {
            txtStockGroupName.Focus();
            lstBrandName.Visible = false;
        }

        private void btnNewManufacturer_Click(object sender, EventArgs e)
        {
            try
            {
                Form NewF = new Master.FirmAE(-1);
                ObjFunction.OpenForm(NewF);
                long MfgCompNo = ObjFunction.GetComboValue(cmbManufacturerCompanyName);
                if (((Master.FirmAE)NewF).ShortID != 0)
                {
                    ObjFunction.FillCombo(cmbManufacturerCompanyName, "Select MfgCompNo,MfgCompName From MManufacturerCompany Where IsActive = 'True' order by MfgCompName");
                    if (((Master.FirmAE)NewF).ShortID > 0)
                        cmbManufacturerCompanyName.SelectedValue = ((Master.FirmAE)NewF).ShortID;
                    else
                        cmbManufacturerCompanyName.SelectedValue = MfgCompNo;
                    cmbManufacturerCompanyName.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        
    }
}
