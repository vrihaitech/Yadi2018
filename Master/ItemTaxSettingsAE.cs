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
    public partial class ItemTaxSettingsAE : Form
    {  
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMItemTaxSetting dbItemTaxSetting = new DBMItemTaxSetting();
        MItemTaxSetting mItemTaxSetting = new MItemTaxSetting();
        string TaxSettingNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long TaxSettingNo, ID;

        public ItemTaxSettingsAE()
        {
            InitializeComponent();
        }

        private void ItemTaxSettingsAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                ObjFunction.FillCombo(cmbSalePurLedger, " SELECT LedgerNo, LedgerName FROM MLedger WHERE GroupNo in (" + GroupType.SalesAccount + "," + GroupType.PurchaseAccount + ") AND IsActive = 'True' ORDER BY LedgerName");
                ObjFunction.FillCombo(cmbTaxType, "SELECT GroupNo, GroupName FROM MGroup WHERE (ControlGroup = " + GroupType.DutiesAndTaxes + " ) AND IsActive = 'True' ORDER BY GroupName");
                ObjFunction.FillCombo(cmbTaxLedger, "SELECT LedgerNo, LedgerName From MLedger where LedgerNo=0");
                ObjFunction.FillCombo(cmbCompanyName, "SELECT FirmNo, FirmName FROM MFirm ORDER BY FirmName");
                TaxSettingNm = "";
                dtSearch = ObjFunction.GetDataView("Select PkSrNo From MItemTaxSetting order by TaxSettingName").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    if (ItemTaxSettings.RequestItemTaxSettingNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = ItemTaxSettings.RequestItemTaxSettingNo;
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

        private void FillControls()
        {
            try
            {
                EP.SetError(txtTaxSettingName, "");
                EP.SetError(txtPercentage, "");
                EP.SetError(cmbSalePurLedger, "");
                EP.SetError(cmbTaxType, "");
                EP.SetError(cmbTaxLedger, "");
                EP.SetError(cmbCompanyName, "");

                mItemTaxSetting = dbItemTaxSetting.ModifyMItemTaxSettingByID(ID);
                TaxSettingNm = mItemTaxSetting.TaxSettingName;
                txtTaxSettingName.Text = mItemTaxSetting.TaxSettingName;
                txtPercentage.Text = mItemTaxSetting.Percentage.ToString(Format.DoubleFloating);
                cmbSalePurLedger.SelectedValue = mItemTaxSetting.SalesLedgerNo.ToString();
                cmbTaxType.SelectedValue = ObjQry.ReturnLong("Select GroupNo From MLedger Where LedgerNo=" + mItemTaxSetting.TaxLedgerNo + "", CommonFunctions.ConStr);
                ObjFunction.FillCombo(cmbTaxLedger, "SELECT LedgerNo, LedgerName FROM MLedger WHERE GroupNo = " + ObjFunction.GetComboValue(cmbTaxType) + " AND IsActive='True' ORDER BY LedgerName");
                cmbTaxLedger.SelectedValue = mItemTaxSetting.TaxLedgerNo.ToString();
                cmbCompanyName.SelectedValue = mItemTaxSetting.CompanyNo.ToString();

                chkActive.Checked = mItemTaxSetting.IsActive;
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
                    dbItemTaxSetting = new DBMItemTaxSetting();
                    mItemTaxSetting = new MItemTaxSetting();

                    mItemTaxSetting.PkSrNo = ID;
                    mItemTaxSetting.TaxSettingName = txtTaxSettingName.Text.Trim().ToUpper();
                    mItemTaxSetting.TaxLedgerNo = ObjFunction.GetComboValue(cmbTaxLedger);
                    mItemTaxSetting.SalesLedgerNo = ObjFunction.GetComboValue(cmbSalePurLedger);
                    mItemTaxSetting.Percentage = Convert.ToDouble(txtPercentage.Text);
                    mItemTaxSetting.IsActive = chkActive.Checked;
                    mItemTaxSetting.CalculationMethod = "2";
                    mItemTaxSetting.CompanyNo = ObjFunction.GetComboValue(cmbCompanyName);
                    mItemTaxSetting.UserID = DBGetVal.UserID;
                    mItemTaxSetting.UserDate = DBGetVal.ServerTime.Date;

                    if (dbItemTaxSetting.AddMItemTaxSetting(mItemTaxSetting) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Item Tax Settings Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select PkSrNo From MItemTaxSetting order by TaxSettingName").Table;
                            ID = ObjQry.ReturnLong("Select Max(PkSrNo) FRom MItemTaxSetting", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Item Tax Settings Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("Item Tax Settings not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            ItemTaxSettings.RequestItemTaxSettingNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtTaxSettingName, ""); 
            EP.SetError(txtPercentage, "");
            EP.SetError(cmbSalePurLedger, "");
            EP.SetError(cmbTaxType, "");
            EP.SetError(cmbCompanyName, "");

            if (txtTaxSettingName.Text.Trim() == "")
            {
                EP.SetError(txtTaxSettingName, "Enter Tax Settings Name");
                EP.SetIconAlignment(txtTaxSettingName, ErrorIconAlignment.MiddleRight);
                txtTaxSettingName.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbSalePurLedger) <= 0)
            {
                EP.SetError(cmbSalePurLedger, "Select Sales/Purchase Tax Ledger");
                EP.SetIconAlignment(cmbSalePurLedger, ErrorIconAlignment.MiddleRight);
                cmbSalePurLedger.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbTaxType) <= 0)
            {
                EP.SetError(cmbTaxType, "Select Tax Type");
                EP.SetIconAlignment(cmbTaxType, ErrorIconAlignment.MiddleRight);
                cmbTaxType.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbTaxLedger) <= 0)
            {
                EP.SetError(cmbTaxLedger, "Select Tax Ledger");
                EP.SetIconAlignment(cmbTaxLedger, ErrorIconAlignment.MiddleRight);
                cmbTaxLedger.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbCompanyName) <= 0)
            {
                EP.SetError(cmbCompanyName, "Select CompanyName");
                EP.SetIconAlignment(cmbCompanyName, ErrorIconAlignment.MiddleRight);
                cmbCompanyName.Focus();
            }
            else if (txtPercentage.Text == "")
            {
                EP.SetError(txtPercentage, "Enter Percentage");
                EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                txtPercentage.Focus();
            }
            else if (ObjFunction.CheckValidAmount( txtPercentage.Text) == false)
            {
                EP.SetError(txtPercentage, "Enter Percentage in numeric field");
                EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                txtPercentage.Focus();
            }
            else if (ObjFunction.CheckValidAmount(txtPercentage.Text) != false && ObjQry.ReturnLong("SELECT  ISNULL(MItemTaxSetting.PkSrNo,0) FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo INNER JOIN   MItemTaxSetting ON MLedger.LedgerNo = MItemTaxSetting.TaxLedgerNo " +
                                          " WHERE  (MItemTaxSetting.SalesLedgerNo = " + ObjFunction.GetComboValue(cmbSalePurLedger) + ") AND (MItemTaxSetting.Percentage = " + Convert.ToDouble(txtPercentage.Text.Trim()) + ") AND (MGroup.GroupNo =  " + ObjFunction.GetComboValue(cmbTaxType) + ") AND (MItemTaxSetting.PkSrNo <> " + ID + ") AND (MItemTaxSetting.TaxLedgerNo = " + ObjFunction.GetComboValue(cmbTaxLedger) + ")", CommonFunctions.ConStr) != 0)
            {
                EP.SetError(txtPercentage, "Percentage Already Exists");
                EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                txtPercentage.Focus();
            }
            else if (TaxSettingNm != txtTaxSettingName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MItemTaxSetting where TaxSettingName = '" + txtTaxSettingName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtTaxSettingName, "Duplicate Tax Setting Name");
                    EP.SetIconAlignment(txtTaxSettingName, ErrorIconAlignment.MiddleRight);
                    txtTaxSettingName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void ItemTaxSettingsAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            TaxSettingNm = "";
        }

        private void txtTaxSettingName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtTaxSettingName, "");
                if (txtTaxSettingName.Text.Trim() != "")
                {
                    if (TaxSettingNm != txtTaxSettingName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MItemTaxSetting where TaxSettingName = '" + txtTaxSettingName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtTaxSettingName, "Duplicate Tax Setting Name");
                            EP.SetIconAlignment(txtTaxSettingName, ErrorIconAlignment.MiddleRight);
                            txtTaxSettingName.Focus();
                        }
                    }

                }
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
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


            FillControls();

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
                dbItemTaxSetting = new DBMItemTaxSetting();
                mItemTaxSetting = new MItemTaxSetting();

                mItemTaxSetting.PkSrNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbItemTaxSetting.DeleteMItemTaxSetting(mItemTaxSetting) == true)
                    {
                        OMMessageBox.Show("Item Tax Setting Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                        //Form NewF = new City();
                        //this.Close();
                        //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else
                    {
                        OMMessageBox.Show("Item Tax Setting not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

       
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new ItemTaxSettings();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtTaxSettingName.Focus();
                string sql = "SELECT Distinct MItemTaxInfo.FKTaxSettingNo " +
                           "FROM  MItemTaxInfo INNER JOIN " +
                           "MItemTaxSetting ON MItemTaxInfo.FKTaxSettingNo = MItemTaxSetting.PkSrNo " +
                           "WHERE (MItemTaxSetting.PkSrNo =" + ID + ")";
                long i = ObjQry.ReturnLong(sql, CommonFunctions.ConStr);
                if (i != 0)
                {
                    cmbSalePurLedger.Enabled = false;
                    cmbTaxLedger.Enabled = false;
                    txtPercentage.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            EP.SetError(txtPercentage, "");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                TaxSettingNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtTaxSettingName.Focus();
                cmbSalePurLedger.SelectedIndex = 0;
                cmbTaxType.SelectedIndex = 0;
                cmbTaxLedger.SelectedIndex = 0;
                EP.SetError(txtPercentage, "");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbTaxType_Leave(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbTaxLedger, "SELECT LedgerNo, LedgerName FROM MLedger WHERE GroupNo = " + ObjFunction.GetComboValue(cmbTaxType) + " AND IsActive='True' ORDER BY LedgerName");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbTaxType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbTaxType_Leave(sender, new EventArgs());
            }
        }

        private void txtPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                //EP.SetError(txtPercentage, "");
                //if (ObjFunction.CheckValidAmount(txtPercentage.Text) != false)
                //{

                //    //if (ObjQry.ReturnLong("SELECT  PkSrNo FROM  MItemTaxSetting  WHERE (SalesLedgerNo = " + ObjFunction.GetComboValue(cmbSalePurLedger) + ") AND (Percentage = " + Convert.ToDouble(txtPercentage.Text.Trim()) + ")", CommonFunctions.ConStr) != 0)
                //    if (ObjQry.ReturnLong("SELECT  MItemTaxSetting.PkSrNo FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo INNER JOIN   MItemTaxSetting ON MLedger.LedgerNo = MItemTaxSetting.TaxLedgerNo "+
                //                          " WHERE  (MItemTaxSetting.SalesLedgerNo = " + ObjFunction.GetComboValue(cmbSalePurLedger) + ") AND (MItemTaxSetting.Percentage = " + Convert.ToDouble(txtPercentage.Text.Trim()) + ") AND (MGroup.GroupNo =  " + ObjFunction.GetComboValue(cmbTaxType) + ")",CommonFunctions.ConStr)!=0)
                //    {
                //        EP.SetError(txtPercentage, "Percentage Already Exists");
                //        EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                //        txtPercentage.Focus();
                //    }
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

    }
}
