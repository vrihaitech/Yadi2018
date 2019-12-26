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
    public partial class StateAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMState dbState = new DBMState();
        MState mState = new MState();
        string StateNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        bool FlagBilingual;
        public long StateNo, ID;
        long No = 0;
        bool isDoProcess = false;

        public StateAE()
        {
            InitializeComponent();
        }
        private void StateAE_Load(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    FlagBilingual = true;
                    SetFlag(true);
                    txtStateLangName.Visible = true;
                    btnLangLongDesc.Visible = true;
                    lblStateLangName.Visible = true;
                }
                else
                {
                    FlagBilingual = false;
                    SetFlag(false);
                    txtStateLangName.Visible = false;
                    btnLangLongDesc.Visible = false;
                    lblStateLangName.Visible = false;
                }

                txtStateLangName.Font = ObjFunction.GetLangFont();
                linkLabel1.Visible = false;
                formatpicture();
                ObjFunction.FillList(lstcountry , "Select CountryNo,CountryName From MCountry Where IsActive ='True' Order By CountryName");
              
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                StateNm = "";
                dtSearch = ObjFunction.GetDataView("Select StateNo From MState order by StateName").Table;
               if (dtSearch.Rows.Count > 0)
                {
                    if (State.RequestStateNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = State.RequestStateNo;
                    FillControls();
                    SetNavigation();
                }
                setDisplay(true);
                btnLangLongDesc.Enabled = false;
                btnNew.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void formatpicture()
        {

            pnlcountry.Top = txtcountry.Bottom;
            pnlcountry.Width = txtcountry.Width;
            pnlcountry.Height = 90;
            lstcountry.Top = pnlcountry.Top - 90;
            lstcountry.Height = pnlcountry.Height - 5;

          }
        public void SetFlag(bool flag)
        {
            txtStateLangName.Visible = flag;
            btnLangLongDesc.Visible = flag;
            lblStateLangName.Visible = flag;
        }
        private void FillControls()
        {
            try
            {
                EP.SetError(txtStateName, "");
                EP.SetError(txtshortName, "");
                EP.SetError(txtStateLangName, "");
                EP.SetError(txtcountry, "");
              


                mState = dbState.ModifyMStateByID(ID);
                StateNm = mState.StateName.ToUpper();
                txtStateName.Text = mState.StateName;
                txtshortName.Text = mState.StateShortCode;
                txtStateLangName.Text = mState.StateLangName.Trim();
                                
                chkActive.Checked = mState.IsActive;
                txtStateCode.Text = mState.StateCode;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";
                
                lstcountry.SelectedValue = mState.CountryNo.ToString();
                txtcountry.Text = lstcountry.Text;

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
                    dbState = new DBMState();
                    mState = new MState();
                    mState.StateNo = ID;

                    mState.StateName = txtStateName.Text.Trim().ToUpper();
                    mState.StateShortCode = txtshortName.Text.Trim().ToUpper();
                    mState.StateLangName = txtStateLangName.Text.Trim();
                    mState.IsActive = chkActive.Checked;
                    mState.CountryNo = ObjFunction.GetListValue(lstcountry);
                   // mState.RegionNo = ObjFunction.GetComboValue(cmbRegion);
                    mState.UserID = DBGetVal.UserID;
                    mState.UserDate = DBGetVal.ServerTime.Date;
                    mState.CompanyNo = DBGetVal.FirmNo;
                    mState.StateCode = txtStateCode.Text.Trim();

                    if (dbState.AddMState(mState) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("State Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select StateNo From MState order by StateName").Table;
                            ID = ObjQry.ReturnLong("Select Max(StateNo) From MState", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("State Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("State not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            State.RequestStateNo = 0;
            this.Close();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtStateName, "");
            EP.SetError(txtshortName, "");
            EP.SetError(txtcountry, "");
          
            if (txtStateName.Text.Trim() == "")
            {

                EP.SetError(txtStateName, "Enter State Name");
                EP.SetIconAlignment(txtStateName, ErrorIconAlignment.MiddleRight);
                txtStateName.Focus();
            }
            //else if (txtshortName.Text.Trim() == "")
            //{

            //    EP.SetError(txtshortName, "Enter Short Name");
            //    EP.SetIconAlignment(txtshortName, ErrorIconAlignment.MiddleRight);
            //    txtshortName.Focus();
            //}
            else if (txtcountry.Text.Trim() == "")
            {
                EP.SetError(txtcountry, "Select Country Name");
                EP.SetIconAlignment(txtcountry, ErrorIconAlignment.MiddleRight);
                txtcountry.Focus();
            }
           
            else if (StateNm != txtStateName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MState where StateName = '" + txtStateName.Text.Trim().ToUpper().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtStateName, "Duplicate State Name");
                    EP.SetIconAlignment(txtStateName, ErrorIconAlignment.MiddleRight);
                    txtStateName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void StateAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            State.RequestStateNo = 0;
            StateNm = "";
        }

        private void txtStateName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtStateName, "");
                if (txtStateName.Text.Trim() != "")
                {
                    if (StateNm != txtStateName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MState where StateName = '" + txtStateName.Text.Trim().ToUpper().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtStateName, "Duplicate State Name");
                            EP.SetIconAlignment(txtStateName, ErrorIconAlignment.MiddleRight);
                            txtStateName.Focus();
                        }
                    }

                }
                if (ID == 0 && txtStateName.Text != "")
                {
                    txtshortName.Text = txtStateName.Text;
                }
                txtshortName.Focus();


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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(StateNo),0)as StateNo From MState ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["StateNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(StateNo),0)as StateNo From MState ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["StateNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(StateNo),0)as StateNo From MState where StateNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["StateNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(StateNo),0)as StateNo From MState where  StateNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["StateNo"].ToString());
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
                StateNm = "";
                mState = new MState();
                ObjFunction.InitialiseControl(this.Controls);
                btnLangLongDesc.Enabled = true;
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                ObjFunction.FillList(lstcountry, "Select CountryNo,CountryName From MCountry Where IsActive ='True' Order By CountryName");
               
                txtStateName.Focus();
                chkActive.Checked =true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            btnLangLongDesc.Enabled = false;
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            pnlcountry.Visible = false;
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new State();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbState = new DBMState();
                mState = new MState();

                mState.StateNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbState.DeleteMState(mState) == true)
                    {
                        OMMessageBox.Show("State Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("State not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnLangLongDesc.Enabled = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtStateName.Focus();
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void StateAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                    if (txtcountry.Enabled == true)
                    {
                        long tID = ObjFunction.GetListValue(lstcountry);
                        ObjFunction.FillList(lstcountry, "Select CountryNo,CountryName From MCountry Where (IsActive ='True' Or CountryNo = " + mState.CountryNo + ") Order By CountryName");
                        lstcountry.SelectedValue = tID;
                    }

                   
                    isDoProcess = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void StateAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

      

        private void txtshortName_Leave(object sender, EventArgs e)
        {
            if (FlagBilingual == true)
            {
                if (txtStateName.Text.Trim() != "")
                {
                    if (txtStateLangName.Text.Trim().Length == 0)
                    {
                        btnLangLongDesc_Click(btnLangLongDesc, null);
                        txtStateCode.Focus();
                    }
                }
            }
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtStateLangName.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtStateName.Text.Trim(), txtStateLangName.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtStateLangName.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtStateName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtStateName.Text.Trim(), txtStateLangName.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtStateLangName.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtStateLangName.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtStateName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtshortName.Focus();
            }
        }

        private void txtshortName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStateCode.Focus();
            }
        }

        private void txtStateCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcountry.Focus();
            }
        }

       

        private void cmbRegion_KeyDown(object sender, KeyEventArgs e)
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

        private void txtcountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtcountry.Text == "")
                {
                    pnlcountry.Visible = true;
                    lstcountry.Focus();
                    lstcountry.SelectedIndex = 0;
                }
                else
                {
                    pnlcountry.Visible = false;
                    chkActive.Focus();
                }
            }
        }

        private void lstcountry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtcountry.Text = lstcountry.Text;
                    pnlcountry.Visible = false;
                    chkActive.Focus();


                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtcountry.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (pnlcountry.Visible == true)
            {
                pnlcountry.Visible = false;
                txtcountry.Focus();
            }
        }
    }
}
