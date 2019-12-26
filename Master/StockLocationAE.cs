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
    public partial class StockLocationAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMStockLocation dbStockLocation = new DBMStockLocation();
        MStockLocation mStockLocation = new MStockLocation();
        string StockLocationNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long ID;

        public StockLocationAE()
        {
            InitializeComponent();
        }

        private void StockLocationAE_Load(object sender, EventArgs e)
        {
            try
            {
                linkLabel1.Visible = false;
                ObjFunction.FillCombo(cmbGodown, "Select GodownNo,GodownName From MGodown");
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dtSearch = ObjFunction.GetDataView("Select StockLocationNo From MStockLocation order by StockLocationName").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    if (StockLocation.RequestStockLocationNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = StockLocation.RequestStockLocationNo;
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
                EP.SetError(txtStockLocationName, "");
                EP.SetError(cmbGodown, "");
                mStockLocation = dbStockLocation.ModifyMStockLocationByID(ID);
                StockLocationNm = mStockLocation.StockLocationName.ToUpper();
                txtStockLocationName.Text = mStockLocation.StockLocationName;
                cmbGodown.SelectedValue = mStockLocation.GodownNo.ToString();
                chkActive.Checked = mStockLocation.IsActive;

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
                    dbStockLocation = new DBMStockLocation();
                    mStockLocation = new MStockLocation();
                    mStockLocation.StockLocationNo = ID;
                    mStockLocation.StockLocationName = txtStockLocationName.Text.Trim();
                    mStockLocation.IsActive = chkActive.Checked;
                    mStockLocation.GodownNo = ObjFunction.GetComboValue(cmbGodown);
                    mStockLocation.UserID = DBGetVal.UserID;
                    mStockLocation.UserDate = DBGetVal.ServerTime.Date;
                    mStockLocation.CompanyNo = DBGetVal.FirmNo;

                    if (dbStockLocation.AddMStockLocation(mStockLocation) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Stock Location Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select StockLocationNo  From MStockLocation order by StockLocationName").Table;
                            ID = ObjQry.ReturnLong("Select Max(StockLocationNo) From MStockLocation", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Stock Location Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("Stock Location not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            StockLocation.RequestStockLocationNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtStockLocationName, ""); 

            if (txtStockLocationName.Text.Trim() == "")
            {
                EP.SetError(txtStockLocationName, "Enter Stock Location Name");
                EP.SetIconAlignment(txtStockLocationName, ErrorIconAlignment.MiddleRight);
                txtStockLocationName.Focus();
            }            
            else if (StockLocationNm != txtStockLocationName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MStockLocation where StockLocationName = '" + txtStockLocationName.Text.Trim().ToUpper().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtStockLocationName, "Duplicate Stock Location Name");
                    EP.SetIconAlignment(txtStockLocationName, ErrorIconAlignment.MiddleRight);
                    txtStockLocationName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void StockLocationAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            StockLocation.RequestStockLocationNo = 0;
            StockLocationNm = "";
        }

        private void txtStockLocationName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtStockLocationName, "");
                if (txtStockLocationName.Text.Trim() != "")
                {
                    if (StockLocationNm != txtStockLocationName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MStockLocation where StockLocationName = '" + txtStockLocationName.Text.Trim().ToUpper().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtStockLocationName, "Duplicate Stock Location Name");
                            EP.SetIconAlignment(txtStockLocationName, ErrorIconAlignment.MiddleRight);
                            txtStockLocationName.Focus();
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbStockLocation = new DBMStockLocation();
                mStockLocation = new MStockLocation();
                mStockLocation.StockLocationNo = ID;

                if (OMMessageBox.Show("Are you sure you want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbStockLocation.DeleteMStockLocation(mStockLocation) == true)
                    {
                        OMMessageBox.Show("Stock Location Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Stock Location not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            Form NewF = new StockLocation();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            chkActive.Checked = true;
            txtStockLocationName.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                StockLocationNm = "";
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                chkActive.Checked = true;
                txtStockLocationName.Focus();
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
