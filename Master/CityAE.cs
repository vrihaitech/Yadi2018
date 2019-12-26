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
    public partial class CityAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMCity dbCity = new DBMCity();
        MCity mCity = new MCity();
        string CityNm;
        bool FlagBilingual;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long CityNo, ID;
        long No = 0;
        bool isDoProcess = false;
        public long ShortID = 0;

        public CityAE()
        {
            InitializeComponent();
        }
        public CityAE(long shortid)
        
        {
            InitializeComponent();
            ShortID = shortid;
        }
        private void CityAE_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    FlagBilingual = true;
                    SetFlag(true);
                }
                else
                {
                    FlagBilingual = false;
                    SetFlag(false);
                }

               
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                formatpicture();
                if (ShortID == 0)
                {
                   

                    ObjFunction.FillList(lststate, "Select StateNo,StateName From MState where IsActive='true'  order by StateName");

                    CityNm = "";
                    dtSearch = ObjFunction.GetDataView("Select CityNo From MCity order by CityNo").Table;

                    if (dtSearch.Rows.Count > 0)
                    {
                        if (City.RequestCityNo == 0)
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                        else
                            ID = City.RequestCityNo;
                        FillControls();
                        SetNavigation();
                    }
                    txtCityLangName.Font = ObjFunction.GetLangFont();
                    setDisplay(true);
                    btnLangLongDesc.Enabled = false;
                    btnNew.Focus();
                    KeyDownFormat(this.Controls);
                }
                else
                {
                    btnNew_Click(sender, new EventArgs());
                    setDisable(false);
                  //  btnExit.Visible = true;
                    //txtCityName.Focus();
                    chkActive.Checked = true;
                }
                if (ShortID != 0)
                    txtCityName.Focus();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void setDisable(bool flag)
        {
            btnSave.Visible = !flag;
            btnCancel.Visible = !flag;
            btnUpdate.Visible = flag;
            btnSearch.Visible = flag;
            btnExit.Visible = flag;
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
        }
        public void SetFlag(bool flag)
        {
            txtCityLangName.Visible = flag;
            btnLangLongDesc.Visible = flag;
            lblCityLN.Visible = flag;
        }
        public void formatpicture()
        {
            pnlstate.Top = txtstate.Bottom;
            pnlstate.Width = txtstate.Width;
            pnlstate.Height = 105;
            lststate.Top = pnlstate.Top - 90;
            lststate.Height = pnlstate.Height - 5;
        }
        private void FillControls()
        {
            try
            {
                EP.SetError(txtCityName, "");
                EP.SetError(txtshortName, "");
                EP.SetError(txtCityLangName, "");
               // EP.SetError(cmbCountry, "");
                EP.SetError(txtstate, "");
               // EP.SetError(cmbRegion, "");
                mCity = dbCity.ModifyMCityByID(ID);
                CityNm = mCity.CityName;
                txtCityName.Text = mCity.CityName;
                txtshortName.Text = mCity.CityShortCode;
                txtCityLangName.Text = mCity.CityLangName.Trim();
                lststate.SelectedValue = mCity.StateNo.ToString();
                txtstate.Text = lststate.Text;
                chkActive.Checked = mCity.IsActive;
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
                    dbCity = new DBMCity();
                    mCity = new MCity();

                    mCity.CityNo = ID;
                    mCity.CityName = txtCityName.Text.Trim().ToUpper();
                    mCity.CityShortCode = txtshortName.Text.Trim().ToUpper();
                    mCity.CityLangName = txtCityLangName.Text.Trim();
                    mCity.IsActive = chkActive.Checked;
                
                    mCity.StateNo = ObjFunction.GetListValue(lststate);
                  
                    mCity.UserID = DBGetVal.UserID;
                    mCity.UserDate = DBGetVal.ServerTime.Date;
                    mCity.CompanyNo = DBGetVal.FirmNo;

                    if (dbCity.AddMCity(mCity) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("City Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select CityNo From MCity order by CityName").Table;
                            ID = ObjQry.ReturnLong("Select Max(CityNo) FRom MCity", CommonFunctions.ConStr);
                            if (ShortID == 0)
                            {
                                SetNavigation();
                                FillControls();
                            }
                            else
                            {
                                ShortID = ID;
                                this.Close();
                                txtCityName.Focus();
                            }
                           
                        }
                        else
                        {
                            OMMessageBox.Show("City Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("City not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            City.RequestCityNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtCityName, "");
            EP.SetError(txtshortName, "");
         
            EP.SetError(txtstate, "");
         
            if (txtCityName.Text.Trim() == "")
            {

                EP.SetError(txtCityName, "Enter City Name");
                EP.SetIconAlignment(txtCityName, ErrorIconAlignment.MiddleRight);
                txtCityName.Focus();
            }
            //else if (txtshortName.Text.Trim() == "")
            //{

            //    EP.SetError(txtshortName, "Enter Short Name");
            //    EP.SetIconAlignment(txtshortName, ErrorIconAlignment.MiddleRight);
            //    txtshortName.Focus();
            //}
           
            else if (txtstate.Text.Trim() == "")
            {
                EP.SetError(txtstate, "Select State");
                EP.SetIconAlignment(txtstate, ErrorIconAlignment.MiddleRight);
                txtstate.Focus();
            }
           
            else if (CityNm.ToUpper() != txtCityName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MCity where Cityno not in (" + ID + ") CityName = '" + txtCityName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtCityName, "Duplicate City Name");
                    EP.SetIconAlignment(txtCityName, ErrorIconAlignment.MiddleRight);
                    txtCityName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void CityAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            CityNm = "";
        }

        private void txtCityName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtCityName, "");
                if (txtCityName.Text.Trim() != "")
                {
                    if (CityNm != txtCityName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MCity where CityName = '" + txtCityName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtCityName, "Duplicate City Name");
                            EP.SetIconAlignment(txtCityName, ErrorIconAlignment.MiddleRight);
                            txtCityName.Focus();
                        }
                    }

                }

                if (ID == 0 && txtCityName.Text != "")
                {
                    txtshortName.Text = txtCityName.Text;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CityNo),0)as CityNo From MCity ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CityNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CityNo),0)as CityNo From MCity ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CityNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CityNo),0)as CityNo From MCity where CityNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CityNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CityNo),0)as CityNo From MCity where  CityNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CityNo"].ToString());
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
                dbCity = new DBMCity();
                mCity = new MCity();

                mCity.CityNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbCity.DeleteMCity(mCity) == true)
                    {
                        OMMessageBox.Show("City Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                       
                    }
                    else
                    {
                        OMMessageBox.Show("City not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Form NewF = new City();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnLangLongDesc.Enabled = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtCityName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            if (ShortID != 0)
            {

                this.Close();
               
            }
            btnLangLongDesc.Enabled = false;
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);

            pnlstate.Visible = false;
            btnNew.Focus();
           
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                CityNm = "";
                mCity = new MCity();
                ObjFunction.InitialiseControl(this.Controls);
                btnLangLongDesc.Enabled = true;
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
              //  ObjFunction.FillCombo(cmbCountry, "Select CountryNo,CountryName From MCountry where IsActive='true'");
              //  ObjFunction.FillCombo(cmbRegion, "select RegionNo,RegionName from MRegion where IsActive='true'");
                txtCityName.Focus();
                chkActive.Checked = true;
               // cmbCountry.SelectedIndex = 0;
                ObjFunction.FillList(lststate, "Select StateNo,StateName From MState Where IsActive = 'True' ORDER BY StateName");
               
                //cmbState.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

      

        private void CityAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                   
                    if (lststate.Enabled == true)
                    {
                        long tID = ObjFunction.GetListValue(lststate);
                        ObjFunction.FillList(lststate, "Select StateNo,StateName From MState Where IsActive = 'True' or StateNo=" + mCity.StateNo + " ORDER BY StateName");
                        lststate.SelectedValue = tID;
                    }
                   
                    isDoProcess = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CityAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

        private void txtshortName_Leave(object sender, EventArgs e)
        {
            if (FlagBilingual == true)
            {
                if (txtCityName.Text.Trim() != "")
                {
                    if (txtCityLangName.Text.Trim().Length == 0)
                    {
                        if (ShortID == 0)
                        {
                            btnLangLongDesc_Click(btnLangLongDesc, null);
                        }
                    }
                }
            }
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtCityLangName.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtCityName.Text.Trim(), txtCityLangName.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtCityLangName.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtCityName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtCityName.Text.Trim(), txtCityLangName.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtCityLangName.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtCityLangName.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtCityName_KeyDown(object sender, KeyEventArgs e)
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
                txtstate.Focus();
            }
        }

      
        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void txtstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtstate.Text == "")
                {
                    pnlstate.Visible = true;
                    lststate.Focus();
                    //lstdepartment.SelectedIndex = 0;
                }
                else
                {
                    pnlstate.Visible = false;
                    chkActive.Focus();
                }
            }
        }

        private void lststate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtstate.Text = lststate.Text;
                    pnlstate.Visible = false;
                    chkActive.Focus();



                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtstate.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (pnlstate.Visible == true)
            {
                pnlstate.Visible = false;
                txtstate.Focus();
            }
        }

   




    }
}
