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
    public partial class CountryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMCountry dbCountry = new DBMCountry();
        MCountry mCountry = new MCountry();
        string CountryNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        bool FlagBilingual;
        public long CountryNo, ID;
        long No = 0;

        public CountryAE()
        {
            InitializeComponent();
        }
        private void CountryAE_Load(object sender, EventArgs e)
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

                txtCountryLangName.Font = ObjFunction.GetLangFont();
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                CountryNm = "";

                dtSearch = ObjFunction.GetDataView("Select CountryNo From MCountry order by CountryName").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (Country.RequestCountryNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = Country.RequestCountryNo;
                    FillControls();
                    SetNavigation();
                }
                txtCountryLangName.Font = ObjFunction.GetLangFont();
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

        public void SetFlag(bool flag)
        {
            txtCountryLangName.Visible = flag;
            btnLangLongDesc.Visible = flag;
            lblCountryLN.Visible = flag;
        }
        private void FillControls()
        {
            try
            {
                EP.SetError(txtCountryName, "");
                EP.SetError(txtshortName, "");
                EP.SetError(txtCountryLangName, "");

                mCountry = dbCountry.ModifyMCountryByID(ID);
                CountryNm = mCountry.CountryName;
                txtCountryName.Text = mCountry.CountryName;
                txtshortName.Text = mCountry.CountryShortCode;
                txtCountryLangName.Text = mCountry.CountryLangName.Trim();
                chkActive.Checked = mCountry.IsActive;
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
                    dbCountry = new DBMCountry();
                    mCountry = new MCountry();
                    mCountry.CountryNo = ID;

                    mCountry.CountryName = txtCountryName.Text.Trim().ToUpper();
                    mCountry.CountryShortCode = txtshortName.Text.Trim().ToUpper();
                    mCountry.CountryLangName = txtCountryLangName.Text.Trim();
                    mCountry.IsActive = chkActive.Checked;
                    mCountry.UserID = DBGetVal.UserID;
                    mCountry.UserDate = DBGetVal.ServerTime.Date;

                    if (dbCountry.AddMCountry(mCountry) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Country Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select CountryNo From MCountry order by CountryName").Table;
                            ID = ObjQry.ReturnLong("Select Max(CountryNo) From MCountry", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                            btnNew.Focus();
                        }
                        else
                        {
                            OMMessageBox.Show("Country Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                            btnNew.Focus();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Country not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Country.RequestCountryNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtCountryName, "");
            EP.SetError(txtshortName, "");

            if (txtCountryName.Text.Trim() == "")
            {

                EP.SetError(txtCountryName, "Enter Country Name");
                EP.SetIconAlignment(txtCountryName, ErrorIconAlignment.MiddleRight);
                txtCountryName.Focus();
            }
            //else if (txtshortName.Text.Trim() == "")
            //{

            //    EP.SetError(txtshortName, "Enter Short Name");
            //    EP.SetIconAlignment(txtshortName, ErrorIconAlignment.MiddleRight);
            //    txtshortName.Focus();
            //}

            else if (CountryNm != txtCountryName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MCountry where CountryName = '" + txtCountryName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtCountryName, "Duplicate Country Name");
                    EP.SetIconAlignment(txtCountryName, ErrorIconAlignment.MiddleRight);
                    txtCountryName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void CountryAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Country.RequestCountryNo = 0;
            CountryNm = "";
        }

        private void txtCountryName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtCountryName, "");
                if (txtCountryName.Text.Trim() != "")
                {
                    if (CountryNm != txtCountryName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MCountry where CountryName = '" + txtCountryName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtCountryName, "Duplicate Country Name");
                            EP.SetIconAlignment(txtCountryName, ErrorIconAlignment.MiddleRight);
                            txtCountryName.Focus();
                        }
                    }

                }
                if (ID == 0 && txtCountryName.Text != "")
                {
                    txtshortName.Text = txtCountryName.Text;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CountryNo),0)as CountryNo From MCountry ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CountryNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CountryNo),0)as CountryNo From MCountry ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CountryNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(CountryNo),0)as CountryNo From MCountry where CountryNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CountryNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(CountryNo),0)as CountryNo From MCountry where  CountryNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["CountryNo"].ToString());
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
        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                CountryNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                btnLangLongDesc.Enabled = true;
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtCountryName.Focus();
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
            txtCountryName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            btnLangLongDesc.Enabled = false;
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Country();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbCountry = new DBMCountry();
                mCountry = new MCountry();

                mCountry.CountryNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbCountry.DeleteMCountry(mCountry) == true)
                    {
                        OMMessageBox.Show("Country Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Country not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {

            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtCountryLangName.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtCountryName.Text.Trim(), txtCountryLangName.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtCountryLangName.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtCountryName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtCountryName.Text.Trim(), txtCountryLangName.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtCountryLangName.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtCountryLangName.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtshortName_Leave(object sender, EventArgs e)
        {
            if (FlagBilingual == true)
            {
                if (txtCountryName.Text.Trim() != "")
                {
                    if (txtCountryLangName.Text.Trim().Length == 0)
                    {
                        btnLangLongDesc_Click(btnLangLongDesc, null);
                    }
                }
            }
        }

        private void txtCountryName_KeyDown(object sender, KeyEventArgs e)
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
