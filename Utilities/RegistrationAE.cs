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

namespace Yadi.Utilities
{
    public partial class RegistrationAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMRegistration dbReg = new DBMRegistration();
        MRegistration mReg = new MRegistration();
        MRegistrationDetails mRegDet = new MRegistrationDetails();
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long ID;

        public RegistrationAE()
        {
            InitializeComponent();
        }

        private void RegistrationAE_Load(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            dtSearch = ObjFunction.GetDataView("Select RegNo From MRegistration order by HostName").Table;

            if (dtSearch.Rows.Count > 0)
            {
                if (Registration.RequestRegNo == 0)
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                else
                    ID = Registration.RequestRegNo;
                FillControls();
                SetNavigation();
            }
            setDisplay(true);
            btnNew.Focus();
            KeyDownFormat(this.Controls);
          }

        private void FillControls()
        {
            EP.SetError(txtMacID, "");
            mReg = dbReg.ModifyMRegistrationByID(ID);
            txtMacID.Text = mReg.MacID;
            txtHostName.Text = mReg.HostName;
            txtMachineIP.Text = mReg.MachineIP;
            chkActive.Checked = mReg.IsActive;

            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";

            if (mReg.IsManual == true)
            {
                lblIsManual.Text = "Manual";
                btnUpdate.Visible = true;
            }
            else
            {
                lblIsManual.Text = "Auto Entry";
                btnUpdate.Visible = false;
            }
         }

        public void SetValue()
        {
            if (Validations() == true)
            {
                dbReg = new DBMRegistration();
                mReg = new MRegistration();
                mRegDet = new MRegistrationDetails();

                mReg.RegNo = ID;
                mReg.MacID = txtMacID.Text;
                mReg.MachineIP = txtMachineIP.Text;
                mReg.HostName = txtHostName.Text;
                mReg.IsActive = chkActive.Checked;
                mReg.IsManual = true;
                dbReg.AddMRegistration(mReg);

                mRegDet.RegDtlsNo = ObjQry.ReturnLong("Select RegDtlsNo From MRegistrationDetails Where RegNo=" + ID + "", CommonFunctions.ConStr);
                mRegDet.RegDate = Convert.ToDateTime(DTRegDate.Text);
                mRegDet.RegTime = DBGetVal.ServerTime;
                dbReg.AddMRegistrationDetails(mRegDet);

                if (dbReg.ExecuteNonQueryStatements() == true)
                {
                    if (ID == 0)
                    {
                        OMMessageBox.Show("Registration Details Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        dtSearch = ObjFunction.GetDataView("Select RegNo From MRegistration order by HostName").Table;
                        ID = ObjQry.ReturnLong("Select Max(RegNo) From MRegistration", CommonFunctions.ConStr);
                        SetNavigation();
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Registration Details Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }

                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                }
                else
                {
                    OMMessageBox.Show("Registration Details not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Registration.RequestRegNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtMacID, ""); 

            if (txtMacID.Text.Trim() == "")
            {
                EP.SetError(txtMacID, "Enter ID");
                EP.SetIconAlignment(txtMacID, ErrorIconAlignment.MiddleRight);
                txtMacID.Focus();
            }
            if (txtHostName.Text.Trim() == "")
            {
                EP.SetError(txtHostName, "Enter Host Name");
                EP.SetIconAlignment(txtHostName, ErrorIconAlignment.MiddleRight);
                txtHostName.Focus();
            }    
            else
                flag = true;
            return flag;
        }

        private void RegistrationAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Registration.RequestRegNo = 0;
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            long No = 0;
            if (type == 5)
            {
                No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                ID = No;
            }
            if (type == 1)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Registration();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            txtMacID.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {  
            ID = 0;
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            txtMacID.Focus();            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            NavigationDisplay(5);
            ObjFunction.LockControls(false, this.Controls);
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }
    }
}
