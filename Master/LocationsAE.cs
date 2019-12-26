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
    public partial class LocationsAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMLocation dbLocation = new DBMLocation();
        MLocation mLocation = new MLocation();
        string LocationNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        bool isDoProcess = false;
        public long LocationNo, ID;
        long No = 0;

        public LocationsAE()
        {
            InitializeComponent();
        }
        private void LocationAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                formatpicture();
                FillListAllMasters();
                lbladdcity.Enabled = false;
                LocationNm = "";
                dtSearch = ObjFunction.GetDataView("Select LocationNo From MLocation Where IsActive='true' order by LocationName").Table;

                if (Locations.RequestLocNo != 0)
                {
                    LocationNm = "";
                    FillControls();
                    dtSearch = ObjFunction.GetDataView("Select LocationNo From MLocation").Table;
                    SetNavigation();
                    setDisplay(true);
                }
                else
                {
                    setDisplay(false);
                }
                //KeyDownFormat(this.Controls);
                if (dtSearch.Rows.Count > 0)
                {
                    if (Locations.RequestLocNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = Locations.RequestLocNo;
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

        private void FillListAllMasters()
        {


            ObjFunction.FillList(lstcountry, "Select CountryNo,CountryName From MCountry Where IsActive = 'True' ORDER BY CountryName");

            ObjFunction.FillList(lstcontrolgroup, "SELECT LocationNo, LocationName FROM MLocation WHERE (IsActive='True') ORDER BY LocationName");
            ObjFunction.FillList(lststate, "SELECT StateNo, StateName FROM MState Where IsActive = 'True' ORDER BY StateName");

            ObjFunction.FillList(lstcity, "SELECT CityNo,CityName from MCity WHERE IsActive = 'True'  ORDER BY CityName");

        }

        private void formatpicture()
        {

            pnlcontrolgroup.Top = txtcontrolgroup.Bottom;
            pnlcontrolgroup.Width = txtcontrolgroup.Width;
            pnlcontrolgroup.Height = 90;
            lstcontrolgroup.Top = pnlcontrolgroup.Top - 85;
            lstcontrolgroup.Height = pnlcontrolgroup.Height - 5;

            pnlcountry.Top = txtcountry.Bottom;
            pnlcountry.Width = txtcountry.Width;
            pnlcountry.Height = 50;
            lstcountry.Height = 48;
            lstcountry.Top = pnlcountry.Top - 115;
            lstcountry.Height = pnlcountry.Height - 5;


            pnlstate.Top = txtstate.Bottom;
            pnlstate.Width = txtstate.Width;
            pnlstate.Height = 90;
            lststate.Height = 85;
            lststate.Top = pnlstate.Top - 140;
            lststate.Height = pnlstate.Height - 5;

            pnlcity.Top = txtcity.Bottom;
            pnlcity.Width = txtcity.Width;
            pnlcity.Height = 60;
            lstcity.Height = 57;
            lstcity.Top = pnlcity.Top - 175;
            lstcity.Height = pnlcity.Height - 5;



        }

        private void FillControls()
        {
            try
            {
                EP.SetError(txtLocationName, "");
                EP.SetError(txtshortName, "");
                EP.SetError(txtcountry, "");
                EP.SetError(txtstate, "");
                EP.SetError(txtcity, "");
                EP.SetError(txtcontrolgroup, "");

                lbladdcity.Enabled = false;
                mLocation = dbLocation.ModifyMLocationByID(ID);
                LocationNm = mLocation.LocationName;
                txtLocationName.Text = mLocation.LocationName;
                txtshortName.Text = mLocation.LocationShortCode;
                lstcontrolgroup.SelectedValue = mLocation.ControlGroup.ToString();
                txtcontrolgroup.Text = lstcontrolgroup.Text;
                lstcountry.SelectedValue = mLocation.CountryNo.ToString();
                txtcountry.Text = lstcountry.Text;
                lststate.SelectedValue = mLocation.StateNo.ToString();
                txtstate.Text = lststate.Text;
                lstcity.SelectedValue = mLocation.CityNo.ToString();
                txtcity.Text = lstcity.Text;

                chkActive.Checked = mLocation.IsActive;

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
                    dbLocation = new DBMLocation();
                    mLocation = new MLocation();
                    mLocation.LocationNo = ID;

                    mLocation.LocationName = txtLocationName.Text.Trim();
                    mLocation.LocationShortCode = txtshortName.Text.Trim();
                    mLocation.IsActive = chkActive.Checked;

                    mLocation.CountryNo = ObjFunction.GetListValue(lstcountry);
                    mLocation.StateNo = ObjFunction.GetListValue(lststate);
                    mLocation.CityNo = ObjFunction.GetListValue(lstcity);
                    mLocation.ControlGroup = ObjFunction.GetListValue(lstcontrolgroup);
                    mLocation.UserID = 1;
                    mLocation.UserDate = DBGetVal.ServerTime.Date;
                    mLocation.CompanyNo = DBGetVal.FirmNo;

                    if (dbLocation.AddMLocation(mLocation) == true)
                    {
                        OMMessageBox.Show("Location Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        if (ID == 0)
                        {
                            dtSearch = ObjFunction.GetDataView("Select LocationNo From MLocation Where (LocationNo <> 1) order by LocationName").Table;
                            ID = ObjQry.ReturnLong("Select Max(LocationNo) From MLocation", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Location not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Locations.RequestLocNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtLocationName, "");
            EP.SetError(txtshortName, "");
            EP.SetError(txtcontrolgroup, "");
            EP.SetError(txtstate, "");
            EP.SetError(txtcity, "");
            EP.SetError(txtcountry, "");
            //  EP.SetError(cmbRegion, "");

            if (txtLocationName.Text.Trim() == "")
            {

                EP.SetError(txtLocationName, "Enter Location Name");
                EP.SetIconAlignment(txtLocationName, ErrorIconAlignment.MiddleRight);
                txtLocationName.Focus();
            }
            //else if (txtshortName.Text.Trim() == "")
            //{

            //    EP.SetError(txtshortName, "Enter Short Name");
            //    EP.SetIconAlignment(txtshortName, ErrorIconAlignment.MiddleRight);
            //    txtshortName.Focus();
            //}
            else if (txtcontrolgroup.Text.Trim() == "")
            {
                EP.SetError(txtcontrolgroup, "Select Control Group");
                EP.SetIconAlignment(txtcontrolgroup, ErrorIconAlignment.MiddleRight);
                txtcontrolgroup.Focus();
            }

            else if (txtcountry.Text.Trim() == "")
            {
                EP.SetError(txtcountry, "Enter Country Name");
                EP.SetIconAlignment(txtcountry, ErrorIconAlignment.MiddleRight);
                txtcountry.Focus();
            }
            else if (txtstate.Text.Trim() == "")
            {
                EP.SetError(txtstate, "Enter State Name");
                EP.SetIconAlignment(txtstate, ErrorIconAlignment.MiddleRight);
                txtstate.Focus();
            }
            else if (txtcity.Text.Trim() == "")
            {
                EP.SetError(txtcity, "Enter City Name");
                EP.SetIconAlignment(txtcity, ErrorIconAlignment.MiddleRight);
                txtcity.Focus();
            }
            else if (LocationNm != txtLocationName.Text.Trim())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MLocation where LocationName = '" + txtLocationName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtLocationName, "Duplicate Location Name");
                    EP.SetIconAlignment(txtLocationName, ErrorIconAlignment.MiddleRight);
                    txtLocationName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void LocationAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Locations.RequestLocNo = 0;
            LocationNm = "";
        }

        private void txtLocationName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtLocationName, "");
                if (txtLocationName.Text.Trim() != "")
                {
                    if (LocationNm != txtLocationName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MLocation where LocationName = '" + txtLocationName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtLocationName, "Duplicate Location Name");
                            EP.SetIconAlignment(txtLocationName, ErrorIconAlignment.MiddleRight);
                            txtLocationName.Focus();
                        }
                    }
                }
                if (ID == 0 && txtLocationName.Text != "")
                {
                    txtshortName.Text = txtLocationName.Text;
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

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LocationNo),0)as LocationNo From MLocation ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LocationNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LocationNo),0)as LocationNo From MLocation ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LocationNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LocationNo),0)as LocationNo From MLocation where LocationNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LocationNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LocationNo),0)as LocationNo From MLocation where  LocationNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LocationNo"].ToString());
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
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                FillListAllMasters();
                lbladdcity.Enabled = true;
                txtLocationName.Focus();
                LocationNm = "";
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
            txtLocationName.Focus();
            lbladdcity.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            pnlstate.Visible = false;
            pnlcity.Visible = false;
            pnlcountry.Visible = false;
            pnlcontrolgroup.Visible = false;
            btnNew.Focus();
            lbladdcity.Enabled = false;
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Locations();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbLocation = new DBMLocation();
                mLocation = new MLocation();

                mLocation.LocationNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbLocation.DeleteMLocation(mLocation) == true)
                    {
                        OMMessageBox.Show("Location Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                        //Form NewF = new Godown();
                        //this.Close();
                        //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else
                    {
                        OMMessageBox.Show("Location not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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


        private void LocationsAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                   

                    if (lstcontrolgroup.Enabled == true)
                    {
                        long tID = ObjFunction.GetListValue(lstcontrolgroup);
                        ObjFunction.FillList(lstcontrolgroup, "SELECT LocationNo, LocationName FROM MLocation WHERE (IsActive='True' OR LocationNo=" + mLocation.ControlGroup + ") ORDER BY LocationName");
                        lstcontrolgroup.SelectedValue = tID;
                    }
                    if (lstcountry.Enabled == true)
                    {
                        long tID = ObjFunction.GetListValue(lstcountry);
                        ObjFunction.FillList(lstcountry, "Select CountryNo,CountryName From MCountry Where (IsActive = 'True' OR CountryNo=" + mLocation.CountryNo + ") ORDER BY CountryName");
                        lstcountry.SelectedValue = tID;
                    }
                    if (lststate.Enabled == true)
                    {
                        long tID = ObjFunction.GetListValue(lststate);
                        ObjFunction.FillList(lststate, "Select StateNo,StateName From MState Where CountryNo = " + ObjFunction.GetListValue(lstcountry) + " And (IsActive = 'True' OR StateNo=" + mLocation.StateNo + ") ORDER BY StateName");
                        lststate.SelectedValue = tID;
                    }
                    if (lstcity.Enabled == true)
                    {
                        long tID = ObjFunction.GetListValue(lstcity);

                        ObjFunction.FillList(lstcity, "Select CityNo, CityName From MCity Where StateNo = " + ObjFunction.GetListValue(lststate) + " And (IsActive = 'True' OR CityNo =" + mLocation.CityNo + ") ORDER BY CityName");
                        lstcity.SelectedValue = tID;
                    }
                    isDoProcess = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void LocationsAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

        private void txtcontrolgroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtcontrolgroup.Text == "")
                {
                    pnlcontrolgroup.Visible = true;
                    lstcontrolgroup.Focus();
                    lstcontrolgroup.SelectedIndex = 0;
                }
                else
                {
                    pnlcontrolgroup.Visible = false;
                    txtcountry.Focus();
                }
            }
        }

        private void lstcontrolgroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtcontrolgroup.Text = lstcontrolgroup.Text;
                    pnlcontrolgroup.Visible = false;
                    txtcountry.Focus();


                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtcontrolgroup.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                    txtstate.Focus();
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
                    txtstate.Focus();


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

        private void txtstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtstate.Text == "")
                {
                    pnlstate.Visible = true;
                    lststate.Focus();
                    lststate.SelectedIndex = 0;
                }
                else
                {
                    pnlstate.Visible = false;
                    txtcity.Focus();
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
                    ObjFunction.FillList(lstcity, "Select CityNo,CityName From MCity where IsActive='true' and stateno=" + lststate.SelectedValue + " order by CityName");
                   
                    pnlstate.Visible = false;
                    txtcity.Focus();


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

        private void txtcity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtcity.Text == "")
                {
                    if (lstcity.Items.Count > 0)
                    {
                        pnlcity.Visible = true;
                        lstcity.Focus();
                        lstcity.SelectedIndex = 0;
                    }
                    else
                    {
                        OMMessageBox.Show("City is not Available in Selected state", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        pnlcity.Visible = false;
                        chkActive.Focus();
                    }
                    
                   
                }
                else
                {
                    pnlcity.Visible = false;
                    chkActive.Focus();
                }
            }
        }

        private void lstcity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtcity.Text = lstcity.Text;
                    pnlcity.Visible = false;
                    chkActive.Focus();


                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtcity.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void txtLocationName_KeyDown(object sender, KeyEventArgs e)
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
                txtcontrolgroup.Focus();
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (pnlcontrolgroup.Visible == true)
            {
                pnlcontrolgroup.Visible = false;
                txtcontrolgroup.Focus();
            }
            else if (pnlcountry.Visible == true)
            {
                pnlcountry.Visible = false;
                txtcountry.Focus();
            }
            else if (pnlstate.Visible == true)
            {
                pnlstate.Visible = false;
                txtstate.Focus();
            }
            else if (pnlcity.Visible == true)
            {
                pnlcity.Visible = false;
                txtcity.Focus();
            }

        }

        private void lbladdcity_Click(object sender, EventArgs e)
        {
            Master.CityAE NewF = new Yadi.Master.CityAE(-1);
            ObjFunction.OpenForm(NewF);
            FillListAllMasters();
            txtcity.Focus();
        }

    }
}
