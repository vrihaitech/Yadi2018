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
    public partial class GodownAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMGodown dbGodown = new DBMGodown();
        MGodown mGodown = new MGodown();
        string GodownNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long GodownNo, ID;
        long No = 0;

        public GodownAE()
        {
            InitializeComponent();
        }

        private void GodownAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                formatpicture();
                FillListAllMasters();
                // ObjFunction.FillCombo(cmbLocation, "Select LocationNo,LocationName From MLocation Where IsActive = 'True' ");
                //ObjFunction.FillCombo(cmbControlGroup, "Select GodownNo,GodownName From MGodown where LocationNo="+ObjFunction.GetComboValue(cmbLocation)+"");
                // ObjFunction.FillCombo(cmbControlGroup, "Select GodownNo,GodownName From MGodown Where IsActive = 'True' ");

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                GodownNm = "";
                dtSearch = ObjFunction.GetDataView("Select GodownNo From MGodown Where GodownNo <> 1 order by GodownNo").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    if (Godown.RequestGodownNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = Godown.RequestGodownNo;
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


            ObjFunction.FillList(lstcontrolgroup, "Select GodownNo,GodownName From MGodown Where IsActive = 'True' ORDER BY GodownName");

            ObjFunction.FillList(lstlocation, "SELECT LocationNo, LocationName FROM MLocation WHERE (IsActive='True') ORDER BY LocationName");
            
        }

        private void formatpicture()
        {

            pnlcontrolgroup.Top = txtcontrolgroup.Bottom;
            pnlcontrolgroup.Width = txtcontrolgroup.Width;
            pnlcontrolgroup.Height = 80;
            lstcontrolgroup.Top = pnlcontrolgroup.Top - 105;
            lstcontrolgroup.Height = pnlcontrolgroup.Height - 5;


            pnllocation.Top = txtlocation.Bottom;
            pnllocation.Width = txtlocation.Width;
            pnllocation.Height = 80;
            lstlocation.Top = pnllocation.Top - 60;
            lstlocation.Height = pnllocation.Height - 5;

           


        }
        private void FillControls()
        {
            try
            {
                EP.SetError(txtGodownName, "");
                //  EP.SetError(cmbLocation, "");
                //  EP.SetError(cmbControlGroup, "");


                mGodown = dbGodown.ModifyMGodownByID(ID);
                GodownNm = mGodown.GodownName;
                txtGodownName.Text = mGodown.GodownName;
                lstcontrolgroup.SelectedValue = mGodown.ControlGroup.ToString();
                txtcontrolgroup.Text = lstcontrolgroup.Text;
                lstlocation.SelectedValue = mGodown.LocationNo.ToString();
                txtlocation.Text = lstlocation.Text;

                chkActive.Checked = mGodown.IsActive;
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
                    dbGodown = new DBMGodown();
                    mGodown = new MGodown();

                    mGodown.GodownNo = ID;
                    mGodown.GodownName = txtGodownName.Text.Trim().ToUpper();
                    mGodown.LocationNo = Convert.ToInt64(lstlocation.SelectedValue);
                    mGodown.ControlGroup = Convert.ToInt64(lstcontrolgroup.SelectedValue);
                    mGodown.IsActive = chkActive.Checked;
                    mGodown.UserID = 1;
                    mGodown.UserDate = DBGetVal.ServerTime.Date;
                    mGodown.CompanyNo = DBGetVal.FirmNo; 

                    if (dbGodown.AddMGodown(mGodown) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Godown Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select GodownNo From MGodown Where GodownNo <> 1 order by GodownName").Table;
                            ID = ObjQry.ReturnLong("Select Max(GodownNo) FRom MGodown", CommonFunctions.ConStr);
                            SetNavigation();

                            FillControls(); 
                            btnNew.Focus();
                            
                        }
                        else
                        {
                            OMMessageBox.Show("Godown Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                            btnNew.Focus();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Godown not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Godown.RequestGodownNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtGodownName, "");
           // EP.SetError(cmbLocation, "");
           // EP.SetError(cmbControlGroup, "");
            if (txtGodownName.Text.Trim() == "")
            {
                EP.SetError(txtGodownName, "Enter Godown Name");
                EP.SetIconAlignment(txtGodownName, ErrorIconAlignment.MiddleRight);
                txtGodownName.Focus();
            }
            else if (txtlocation.Text.Trim() == "")
            {
                EP.SetError(txtlocation, "Enter location Name");
                EP.SetIconAlignment(txtlocation, ErrorIconAlignment.MiddleRight);
                txtlocation.Focus();
            }
           else if (txtcontrolgroup.Text.Trim() == "")
            {
                EP.SetError(txtcontrolgroup, "Enter controlgroup Name");
                EP.SetIconAlignment(txtcontrolgroup, ErrorIconAlignment.MiddleRight);
                txtcontrolgroup.Focus();
            }
           
          
            else if (GodownNm.ToUpper() != txtGodownName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MGodown where GodownName = '" + txtGodownName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtGodownName, "Duplicate Godown Name");
                    EP.SetIconAlignment(txtGodownName, ErrorIconAlignment.MiddleRight);
                    txtGodownName.Focus();

                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void GodownAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            GodownNm = "";
        }


        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(GodownNo),0)as GodownNo From MGodown ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GodownNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(GodownNo),0)as GodownNo From MGodown ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GodownNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(GodownNo),0)as GodownNo From MGodown where GodownNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GodownNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(GodownNo),0)as GodownNo From MGodown where  GodownNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["GodownNo"].ToString());
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
                if (BtnSave.Visible) BtnSave_Click(sender, e);
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
                dbGodown = new DBMGodown();
                mGodown = new MGodown();

                mGodown.GodownNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbGodown.DeleteMGodown(mGodown) == true)
                    {
                        OMMessageBox.Show("Godown Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                        //Form NewF = new Godown();
                        //this.Close();
                        //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else
                    {
                        OMMessageBox.Show("Godown not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Form NewF = new Godown();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                GodownNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                chkActive.Checked = true;
               // ObjFunction.FillCombo(cmbLocation, "Select LocationNo,LocationName From MLocation  Where IsActive = 'True' ");
                //ObjFunction.FillCombo(cmbControlGroup, "Select GodownNo,GodownName From MGodown  Where IsActive = 'True' ");

                txtGodownName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
            pnlcontrolgroup.Visible = false;
            pnllocation.Visible=false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtGodownName.Focus();
        }

        private void txtGodownName_Leave_1(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtGodownName, "");
                if (txtGodownName.Text.Trim() != "")
                {
                    if (GodownNm.ToUpper() != txtGodownName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MGodown where GodownName = '" + txtGodownName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtGodownName, "Duplicate Godown Name");
                            EP.SetIconAlignment(txtGodownName, ErrorIconAlignment.MiddleRight);
                            txtGodownName.Focus();

                        }
                       // else
                           // cmbLocation.Focus();
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtGodownName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtlocation.Focus();
            }

        }

        //private void cmbLocation_KeyDown(object sender, KeyEventArgs e)
        //{

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //      //  cmbControlGroup.Focus();
        //    }
        //}

        private void cmbControlGroup_KeyDown(object sender, KeyEventArgs e)
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

        private void txtlocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtlocation.Text == "")
                {
                    if (lstlocation.Items.Count > 0)
                    {
                        pnllocation.Visible = true;
                        lstlocation.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Location not found", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);

                        pnllocation.Visible = false;
                        txtcontrolgroup.Focus();

                    }
                    //lstlocation.SelectedIndex = 0;
                }
                else
                {
                    pnllocation.Visible = false;
                    txtcontrolgroup.Focus();
                }
            }
        }

        private void lstlocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtlocation.Text = lstlocation.Text;
                    pnllocation.Visible = false;
                    txtcontrolgroup.Focus();


                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtlocation.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtcontrolgroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtcontrolgroup.Text == "")
                {
                    if (lstcontrolgroup.Items.Count > 0)
                    {

                        pnlcontrolgroup.Visible = true;
                        lstcontrolgroup.Focus();
                    }

                    else
                    {
                        OMMessageBox.Show("Control Group not found", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        pnlcontrolgroup.Visible = false;
                        chkActive.Focus();
                    }
                }
                else
                {
                    pnlcontrolgroup.Visible = false;
                    chkActive.Focus();
                }
            }
        }

        private void lstcontolgroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                   txtcontrolgroup.Text = lstcontrolgroup.Text;
                    pnlcontrolgroup.Visible = false;
                    chkActive.Focus();


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

        private void lstlocation_Leave(object sender, EventArgs e)
        {
            pnllocation.Visible = false;
        }

        private void lstcontrolgroup_Leave(object sender, EventArgs e)
        {
            pnlcontrolgroup.Visible = false;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (pnllocation.Visible == true)
            {
                pnllocation.Visible = false;
                txtlocation.Focus();

            }
            else if (pnlcontrolgroup.Visible == true)
            {
                pnlcontrolgroup.Visible = false;
                txtcontrolgroup.Focus();
            }

        }

      

       
    }
}
