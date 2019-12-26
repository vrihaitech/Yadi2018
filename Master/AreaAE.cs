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
    public partial class AreaAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();


        DBMArea dbArea = new DBMArea();
        MArea mArea = new MArea();
        string AreaNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long AreaNo, ID;
        bool FlagBilingual;
        long No = 0;
        bool isDoProcess = false;
        public long ShortID = 0;

        public AreaAE()
        {
            InitializeComponent();
        }
        public AreaAE(long shortid)
        
        {
            InitializeComponent();
            ShortID = shortid;
        }

        private void AreaAE_Load(object sender, EventArgs e)
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
                AreaNm = "";
                if (ShortID == 0)
                {
                    dtSearch = ObjFunction.GetDataView("Select AreaNo From MArea where AreaName IS NOT NULL order by AreaNo").Table;

                    if (dtSearch.Rows.Count > 0)
                    {
                        if (Area.RequestAreaNo == 0)
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                        else
                            ID = Area.RequestAreaNo;
                        FillControls();
                        SetNavigation();
                    }

                    txtAreaLangName.Font = ObjFunction.GetLangFont();
                    setDisplay(true);
                    btnLangLongDesc.Enabled = false;
                    btnNew.Focus();
                    KeyDownFormat(this.Controls);
                }
                else
                {
                    btnNew_Click(sender, new EventArgs());
                    setDisable(false);

                    txtAreaName.Focus();
                    chkActive.Checked = true;
                }
                if (ShortID != 0)
                    txtAreaName.Focus();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetFlag(bool flag)
        {
            txtAreaLangName.Visible = flag;
            btnLangLongDesc.Visible = flag;
            lblCityLN.Visible = flag;
        }
        public void setDisable(bool flag)
        {
            BtnSave.Visible = !flag;
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

        private void FillControls()
        {
            try
            {
                EP.SetError(txtAreaName, "");
                EP.SetError(txtAreaLangName, "");
                EP.SetError(txtshortName, "");
               

                mArea = dbArea.ModifyMAreaByID(ID);
                AreaNm = mArea.AreaName;
                txtAreaName.Text = mArea.AreaName;
                txtAreaLangName.Text = mArea.AreaLangName;
                txtshortName.Text = mArea.AreaShortCode;
                chkActive.Checked = mArea.IsActive;
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
                    dbArea = new DBMArea();
                    mArea = new MArea();
                    mArea.AreaNo = ID;

                    mArea.AreaName = txtAreaName.Text.Trim();
                    mArea.AreaLangName = txtAreaLangName.Text.Trim();
                    mArea.AreaShortCode = txtshortName.Text.Trim();
                    mArea.IsActive = chkActive.Checked;
                    mArea.UserID = DBGetVal.UserID;
                    mArea.UserDate = DBGetVal.ServerTime.Date;
                    mArea.CompanyNo = DBGetVal.FirmNo;

                    if (dbArea.AddMArea(mArea) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Area Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select AreaNo From MArea order by AreaName").Table;
                            ID = ObjQry.ReturnLong("Select Max(AreaNo) From MArea", CommonFunctions.ConStr);
                            if (ShortID == 0)
                            {
                                SetNavigation();
                                FillControls();
                            }
                            else
                            {
                                ShortID = ID;
                                this.Close();
                            }
                            
                        }
                        else
                        {
                            OMMessageBox.Show("Area Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("Area not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Area.RequestAreaNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtAreaName, "");
            EP.SetError(txtAreaLangName, "");
            EP.SetError(txtshortName, "");


            if (txtAreaName.Text.Trim() == "")
            {

                EP.SetError(txtAreaName, "Enter Area Name");
                EP.SetIconAlignment(txtAreaName, ErrorIconAlignment.MiddleRight);
                txtAreaName.Focus();
            }

            //else if (txtshortName.Text.Trim() == "")
            //{

            //    EP.SetError(txtshortName, "Enter Short Name");
            //    EP.SetIconAlignment(txtshortName, ErrorIconAlignment.MiddleRight);
            //    txtshortName.Focus();
            //}

            else if (AreaNm.ToUpper() != txtAreaName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MArea where Areano not in (" + ID + ") AreaName = '" + txtAreaName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtAreaName, "Duplicate Area Name");
                    EP.SetIconAlignment(txtAreaName, ErrorIconAlignment.MiddleRight);
                    txtAreaName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        

        private void txtAreaName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtAreaName, "");
                if (txtAreaName.Text.Trim() != "")
                {
                    if (AreaNm != txtAreaName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MArea where AreaName = '" + txtAreaName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtAreaName, "Duplicate Area Name");
                            EP.SetIconAlignment(txtAreaName, ErrorIconAlignment.MiddleRight);
                            txtAreaName.Focus();
                        }
                    }

                }
                if (ID == 0 && txtAreaName.Text != "")
                {
                    txtshortName.Text = txtAreaName.Text;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(AreaNo),0)as AreaNo From MArea ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["AreaNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(AreaNo),0)as AreaNo From MArea ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["AreaNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(AreaNo),0)as AreaNo From MArea where AreaNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["AreaNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(AreaNo),0)as AreaNo From MArea where  AreaNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["AreaNo"].ToString());
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
                mArea = new MArea();
                ObjFunction.InitialiseControl(this.Controls);
                btnLangLongDesc.Enabled = true;
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);

                chkActive.Checked = true;
                txtAreaName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Area();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbArea = new DBMArea();
                mArea = new MArea();

                mArea.AreaNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbArea.DeleteMArea(mArea) == true)
                    {
                        OMMessageBox.Show("Area Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();

                    }
                    else
                    {
                        OMMessageBox.Show("Area not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
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
            btnNew.Focus();
            if (ShortID != 0)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnLangLongDesc.Enabled = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtAreaName.Focus();
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }



        private void AreaAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                    //if (cmbCountry.Enabled == true)
                    //{
                    //    long tID = ObjFunction.GetComboValue(cmbCountry);
                    //    ObjFunction.FillCombo(cmbCountry, "Select CountryNo,CountryName From MCountry Where IsActive='true' or CountryNo=" + mArea.CountryNo + "");
                    //    cmbRegion.SelectedValue = tID;
                    //}
                    isDoProcess = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void AreaAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtAreaLangName.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtAreaName.Text.Trim(), txtAreaLangName.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtAreaLangName.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtAreaName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtAreaName.Text.Trim(), txtAreaLangName.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtAreaLangName.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtAreaLangName.Text = val;
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
                if (txtAreaName.Text.Trim() != "")
                {
                    if (txtAreaLangName.Text.Trim().Length == 0)
                    {
                        if (ShortID == 0)
                        {
                            btnLangLongDesc_Click(btnLangLongDesc, null);
                        }
                    }
                }
            }
        }

        private void txtAreaName_KeyDown(object sender, KeyEventArgs e)
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

        private void txtAreaLangName_KeyDown(object sender, KeyEventArgs e)
        {

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
