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
    public partial class UomAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMUOM dbUOM = new DBMUOM();
        MUOM mUOM = new MUOM();

        DataTable dtSearch = new DataTable();
        int cntRow;
        long ID = 0;

        public UomAE()
        {
            InitializeComponent();
        }

        private void UomAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                formatpicture();
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("UomType");
                dt1.Columns.Add("valueid");
                DataRow dr = null;
                dr = dt1.NewRow();
                dr[0] = "Higher";
                dr[1] = "1";
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr[0] = "Lower";
                dr[1] = "0";
                dt1.Rows.Add(dr);
                lstUomType.DataSource = dt1.DefaultView;
                lstUomType.DisplayMember = dt1.Columns[0].ColumnName;
                lstUomType.ValueMember = dt1.Columns[1].ColumnName;
                //  lstUomType.Items.Add(new Item("Higher", "1"));
                // lstUomType.Items.Add(new Item("Lower", "0"));
                // lstUomType.SelectedIndex = 0;
                dtSearch = ObjFunction.GetDataView("Select UOMNo From MUOM  where uomno not in(1) order  by UOMName").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (UOM.RequestUOMNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = UOM.RequestUOMNo;
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

        public void formatpicture()
        {
            pnlUomType.Top = txtUomType.Bottom;
            pnlUomType.Width = txtUomType.Width;
            pnlUomType.Height = 45;
            lstUomType.Top = pnlUomType.Top - 88;
            lstUomType.Height = pnlUomType.Height - 5;
        }

        private void FillControls()
        {
            btnUpdate.Enabled = true;
            try
            {
                EP.SetError(txtUOMName, ""); EP.SetError(txtShortName, "");
                mUOM = dbUOM.ModifyMUOMByID(ID);

                txtUOMName.Text = mUOM.UOMName;
                txtShortName.Text = mUOM.UOMShortCode;
                lstUomType.SelectedValue = mUOM.UOMType;
                txtUomType.Text = lstUomType.Text;
                chkActive.Checked = mUOM.IsActive;
                if (txtUOMName.Text == "NA")
                {
                    //OMMessageBox.Show("UOM Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    btnUpdate.Enabled = false;
                }
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
                    dbUOM = new DBMUOM();
                    mUOM = new MUOM();
                    mUOM.UOMNo = ID;
                    mUOM.UOMName = txtUOMName.Text.Trim().ToUpper();
                    mUOM.UOMShortCode = txtShortName.Text.Trim();
                    mUOM.UOMType = Convert.ToInt32(lstUomType.SelectedValue);
                    mUOM.IsActive = chkActive.Checked;
                    mUOM.UserID = DBGetVal.UserID;
                    mUOM.UserDate = DBGetVal.ServerTime.Date;

                    if (dbUOM.AddMUOM(mUOM) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("UOM Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select UOMNo From MUOM where uomno not in(1) order by UOMName").Table;
                            ID = ObjQry.ReturnLong("Select Max(UOMNo) From MUOM", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }

                        else
                        {
                            OMMessageBox.Show("UOM Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("UOM not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            UOM.RequestUOMNo = 0;

            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtUOMName, ""); EP.SetError(txtShortName, "");
            EP.SetError(txtUomType, "");

            if (txtUOMName.Text.Trim() == "")
            {

                EP.SetError(txtUOMName, "Enter UOM Name");
                EP.SetIconAlignment(txtUOMName, ErrorIconAlignment.MiddleRight);
                txtUOMName.Focus();
            }
            else if (txtShortName.Text.Trim() == "")
            {

                EP.SetError(txtShortName, "Enter Short Name");
                EP.SetIconAlignment(txtShortName, ErrorIconAlignment.MiddleRight);
                txtShortName.Focus();
            }

            else if (ObjQry.ReturnInteger("Select Count(*) from MUOM where UOMName = '" + txtUOMName.Text.Trim().Replace("'", "''") + "' and UOMNo not in (" + ID + ")", CommonFunctions.ConStr) != 0)
            {
                EP.SetError(txtUOMName, "Duplicate UOM Name");
                EP.SetIconAlignment(txtUOMName, ErrorIconAlignment.MiddleRight);
                txtUOMName.Focus();
            }
            else
                flag = true;

            return flag;
        }

        private void UOMAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            UOM.RequestUOMNo = 0;

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
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtUOMName.Focus();

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
            txtUOMName.Focus();

            txtUomType.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            pnlUomType.Visible = false;
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new UOM();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbUOM = new DBMUOM();
                mUOM = new MUOM();

                mUOM.UOMNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbUOM.DeleteMUOM(mUOM) == true)
                    {
                        OMMessageBox.Show("UOM Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("UOM not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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

        private void txtuomtype_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {

                    if (txtUomType.Text == "")
                    {
                        pnlUomType.Visible = true;
                        lstUomType.Focus();
                    }
                    else
                    {
                        pnlUomType.Visible = false;
                        chkActive.Focus();
                    }

                }
                else if (e.KeyChar == Convert.ToChar(Keys.Delete))
                {
                    txtUomType.Text = "";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void lstUomType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {

                    txtUomType.Text = lstUomType.Text;
                    pnlUomType.Visible = false;
                    chkActive.Focus();


                }
                else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                {

                    txtUomType.Focus();
                    pnlUomType.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtshortName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //    txtUomType.Focus();
                if (txtUomType.Enabled == true)
                {
                    pnlUomType.Visible = true;
                    lstUomType.Focus();
                }

                else
                {
                    BtnSave.Focus();


                }
            }
        }

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void txtUOMName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    EP.SetError(txtUOMName, "");
                    if (txtUOMName.Text.Trim() != "")
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MUOM where (UOMName = '" + txtUOMName.Text.Trim().Replace("'", "''") + "' or MUOM.UOMShortCode='" + txtUOMName.Text.Trim().Replace("'", "''") + "') AND MUOM.UOMNo<>" + ID + "", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtUOMName, "Duplicate UOM Name");
                            EP.SetIconAlignment(txtUOMName, ErrorIconAlignment.MiddleRight);
                            txtUOMName.Focus();
                        }
                        else
                        {
                            txtShortName.Text = txtUOMName.Text;
                            txtShortName.Focus();
                        }

                    }
                    else
                    {
                        EP.SetError(txtUOMName, "Enter UOM Name.");
                        EP.SetIconAlignment(txtUOMName, ErrorIconAlignment.MiddleRight);
                        txtUOMName.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}
