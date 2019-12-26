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
    public partial class OccupationAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMOccupation dbOccupation = new DBMOccupation();
        MOccupation mOccupation = new MOccupation();
        string OccupationNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long OccupationNo, ID;
        public long ShortID = 0;
 
        public OccupationAE()
        {
            InitializeComponent();
        }

        public OccupationAE(long shortid)
        {
            InitializeComponent();
            ShortID = shortid;
        }

        private void OccupationAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                OccupationNm = "";
                dtSearch = ObjFunction.GetDataView("Select OccupationNo From MOccupation order by OccupationName").Table;
                if (ShortID == 0)
                {
                    if (dtSearch.Rows.Count > 0)
                    {
                        if (Occupation.RequestOccupationNo == 0)
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                        else
                            ID = Occupation.RequestOccupationNo;
                        FillControls();
                        SetNavigation();
                    }
                    setDisplay(true);
                    btnNew.Focus();
                }
                else
                {
                    btnNew_Click(sender, new EventArgs());
                    setDisable(false);
                }

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
                EP.SetError(txtOccupationName, "");
                EP.SetError(txtShortName, "");
                mOccupation = dbOccupation.ModifyMOccupationByID(ID);
                OccupationNm = mOccupation.OccupationName;
                txtOccupationName.Text = mOccupation.OccupationName;
                txtShortName.Text = mOccupation.ShortName;
                chkActive.Checked = mOccupation.IsActive;
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

        public void setDisable(bool flag)
        {
            BtnSave.Visible = !flag;
            btnCancel.Visible = !flag;
            btnUpdate.Visible = flag;
            btnSearch.Visible = flag;
            BtnExit.Visible = flag;
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {
                    dbOccupation = new DBMOccupation();
                    mOccupation = new MOccupation();
                    mOccupation.OccupationNo = ID;
                    mOccupation.OccupationName = txtOccupationName.Text.Trim().ToUpper();
                    mOccupation.ShortName = txtShortName.Text.Trim().ToUpper();
                    mOccupation.IsActive = chkActive.Checked;
                    mOccupation.UserID = DBGetVal.UserID;
                    mOccupation.UserDate = DBGetVal.ServerTime.Date;
                    mOccupation.CompanyNo = DBGetVal.FirmNo;

                    if (dbOccupation.AddMOccupation(mOccupation) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Occupation Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select OccupationNo From MOccupation order by OccupationName").Table;
                            ID = ObjQry.ReturnLong("Select Max(OccupationNo) From MOccupation", CommonFunctions.ConStr);
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
                            OMMessageBox.Show("Occupation Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Occupation not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Occupation.RequestOccupationNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtOccupationName, ""); 
            EP.SetError(txtShortName, "");

            if (txtOccupationName.Text.Trim() == "")
            {
                EP.SetError(txtOccupationName, "Enter Occupation Name");
                EP.SetIconAlignment(txtOccupationName, ErrorIconAlignment.MiddleRight);
                txtOccupationName.Focus();
            }
            else if (txtShortName.Text.Trim() == "")
            {
                EP.SetError(txtShortName, "Enter Short Name");
                EP.SetIconAlignment(txtShortName, ErrorIconAlignment.MiddleRight);
                txtShortName.Focus();
            }
            else if (OccupationNm != txtOccupationName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MOccupation where OccupationName = '" + txtOccupationName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtOccupationName, "Duplicate Occupation Name");
                    EP.SetIconAlignment(txtOccupationName, ErrorIconAlignment.MiddleRight);
                    txtOccupationName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void OccupationAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            Occupation.RequestOccupationNo = 0;
            OccupationNm = "";
        }

        private void txtOccupationName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtOccupationName, "");
                if (txtOccupationName.Text.Trim() != "")
                {
                    if (OccupationNm != txtOccupationName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MOccupation where OccupationName = '" + txtOccupationName.Text.Trim().Replace("'", "''").Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtOccupationName, "Duplicate Occupation Name");
                            EP.SetIconAlignment(txtOccupationName, ErrorIconAlignment.MiddleRight);
                            txtOccupationName.Focus();
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
                OccupationNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtOccupationName.Focus();
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
            txtOccupationName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ShortID == 0)
            {
                NavigationDisplay(5);
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
            }
            else this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Occupation();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbOccupation = new DBMOccupation();
                mOccupation = new MOccupation();
                mOccupation.OccupationNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbOccupation.DeleteMOccupation(mOccupation) == true)
                    {
                        OMMessageBox.Show("Occupation Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Occupation not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
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
    }
}
