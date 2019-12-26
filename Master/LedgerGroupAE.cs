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
    public partial class LedgerGroupAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMLedgerGroup dbLedgerGroup = new DBMLedgerGroup();
        MLedgerGroup mLedgerGroup = new MLedgerGroup();
        MLedgerGroupDetails mLedgerGroupDetails = new MLedgerGroupDetails();
        string AreaNm = "";
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long LedgerGroupNo, ID;
        bool FlagBilingual;
        long No = 0;
        bool isDoProcess = false;
        public long ShortID = 0;
        DataTable dt = new DataTable();

        public LedgerGroupAE()
        {
            InitializeComponent();
        }

        private void LedgerGroupAE_Load(object sender, EventArgs e)
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

                if (ShortID == 0)
                {
                    dtSearch = ObjFunction.GetDataView("Select LedgerGroupNo From MLedgerGroup where LedgerName IS NOT NULL order by LedgerGroupNo").Table;

                    if (dtSearch.Rows.Count > 0)
                    {
                        if (LedgerGroup.RequestLedgerGroupNo == 0)
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                        else
                            ID = LedgerGroup.RequestLedgerGroupNo;
                        FillControls();
                        SetNavigation();


                    }

                    txtLedgerLangName.Font = ObjFunction.GetLangFont();
                    setDisplay(true);
                    btnLangLongDesc.Enabled = false;
                    KeyDownFormat(this.Controls);
                }
                else
                {
                    setDisable(false);
                }

                btnNew.Focus();
                pnlGroupName.Visible = false;
                pnlGroupName.Top = txtGroupName.Bottom;
                pnlGroupName.Width = txtGroupName.Width;
                pnlGroupName.Height = 70;
                lstGroupName.Top = pnlGroupName.Top - 70;
                lstGroupName.Height = pnlGroupName.Height - 5;
                foreach (DataGridViewColumn col in GvParty.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetFlag(bool flag)
        {
            txtLedgerLangName.Visible = flag;
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
                EP.SetError(txtLedgerName, "");
                EP.SetError(txtLedgerLangName, "");
                EP.SetError(txtGroupName, "");

                mLedgerGroup = dbLedgerGroup.ModifyMLedgerGroupByID(ID);
                AreaNm = mLedgerGroup.LedgerName;
                txtLedgerName.Text = mLedgerGroup.LedgerName;
                txtLedgerLangName.Text = mLedgerGroup.LedgerLangName;
                FillList();
                lstGroupName.SelectedValue = mLedgerGroup.GroupNo;
                txtGroupName.Text = lstGroupName.Text;

                chkActive.Checked = mLedgerGroup.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";
                BindParyGrid();
                PnlParty.Visible = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillList()
        {
            ObjFunction.FillList(lstGroupName, "Select GroupNo,GroupName From MGroup where IsActive='true' and GroupNo in (22,26) order by GroupName");
        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {
                    dbLedgerGroup = new DBMLedgerGroup();
                    mLedgerGroup = new MLedgerGroup();
                    mLedgerGroup.LedgerGroupNo = ID;

                    mLedgerGroup.LedgerName = txtLedgerName.Text.Trim();
                    mLedgerGroup.LedgerLangName = txtLedgerLangName.Text.Trim();
                    mLedgerGroup.GroupNo = Convert.ToInt32(lstGroupName.SelectedValue);
                    mLedgerGroup.IsActive = chkActive.Checked;
                    mLedgerGroup.UserID = DBGetVal.UserID;
                    mLedgerGroup.UserDate = DBGetVal.ServerTime.Date;
                    mLedgerGroup.CompanyNo = DBGetVal.FirmNo;

                    dbLedgerGroup.AddMLedgerGroup(mLedgerGroup);

                    if (dbLedgerGroup.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Ledger Group Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select LedgerGroupNo From MLedgerGroup order by LedgerName").Table;
                            ID = ObjQry.ReturnLong("Select Max(LedgerGroupNo) From MLedgerGroup", CommonFunctions.ConStr);
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
                            OMMessageBox.Show("Ledger Group Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("Ledger Group Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            LedgerGroup.RequestLedgerGroupNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtLedgerName, "");
            EP.SetError(txtLedgerLangName, "");
            EP.SetError(txtGroupName, "");

            if (txtLedgerName.Text.Trim() == "")
            {

                EP.SetError(txtLedgerName, "Enter Ledger Group");
                EP.SetIconAlignment(txtLedgerName, ErrorIconAlignment.MiddleRight);
                txtLedgerName.Focus();
            }
            else if (AreaNm.ToUpper() != txtLedgerName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MLedgerGroup where LedgerGroupNo not in (" + ID + ") and LedgerName = '" + txtLedgerName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtLedgerName, "Duplicate Ledger Group");
                    EP.SetIconAlignment(txtLedgerName, ErrorIconAlignment.MiddleRight);
                    txtLedgerName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtLedgerName, "");
                if (txtLedgerName.Text.Trim() != "")
                {
                    if (AreaNm != txtLedgerName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MLedgerGroup where LedgerName = '" + txtLedgerName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtLedgerName, "Duplicate Ledger Group");
                            EP.SetIconAlignment(txtLedgerName, ErrorIconAlignment.MiddleRight);
                            txtLedgerName.Focus();
                        }
                    }
                }
                else
                {
                    txtGroupName.Focus();
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerGroupNo),0)as LedgerGroupNo From MLedgerGroup ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerGroupNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerGroupNo),0)as LedgerGroupNo From MLedgerGroup ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerGroupNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerGroupNo),0)as LedgerGroupNo From MLedgerGroup where LedgerGroupNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerGroupNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerGroupNo),0)as LedgerGroupNo From MLedgerGroup where  LedgerGroupNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerGroupNo"].ToString());
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
                mLedgerGroup = new MLedgerGroup();
                ObjFunction.InitialiseControl(this.Controls);
                btnLangLongDesc.Enabled = true;
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                chkActive.Checked = true;
                FillList();
                txtLedgerName.Focus();
                AreaNm = "";
                PnlParty.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new LedgerGroup();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbLedgerGroup = new DBMLedgerGroup();
                mLedgerGroup = new MLedgerGroup();

                mLedgerGroup.LedgerGroupNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbLedgerGroup.DeleteMLedgerGroup(mLedgerGroup) == true)
                    {
                        OMMessageBox.Show("Ledger Group Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Ledger Group Not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            pnlGroupName.Visible = false;
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
            txtLedgerName.Focus();
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void LedgerGroupAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                    isDoProcess = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void LedgerGroupAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLedgerLangName.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtLedgerName.Text.Trim(), txtLedgerLangName.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLedgerLangName.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtLedgerName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtLedgerName.Text.Trim(), txtLedgerLangName.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLedgerLangName.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLedgerLangName.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtLedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtGroupName.Focus();
            }
        }

        private void txtGroupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtGroupName.Text == "")
                {
                    pnlGroupName.Visible = true;
                    lstGroupName.Focus();
                    // lstGroupName.SelectedIndex = 0;
                }
                else
                {
                    pnlGroupName.Visible = false;
                    chkActive.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlGroupName.Visible = true;
                lstGroupName.Focus();
            }
        }

        private void lstGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtGroupName.Text = lstGroupName.Text;
                    pnlGroupName.Visible = false;
                    chkActive.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtGroupName.Focus();
                    txtGroupName.Text = lstGroupName.Text;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtGroupName_Leave(object sender, EventArgs e)
        {
            if (FlagBilingual == true)
            {
                if (txtLedgerName.Text.Trim() != "")
                {
                    if (txtLedgerLangName.Text.Trim().Length == 0)
                    {
                        if (ShortID == 0)
                        {
                            btnLangLongDesc_Click(btnLangLongDesc, null);
                        }
                    }
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

        private void txtLedgerLangName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void GvParty_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void BtnPartyAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string strLedgerNo = "";
                for (int i = 0; i < GvParty.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(GvParty.Rows[i].Cells[3].FormattedValue) == true)
                    {
                        if (strLedgerNo == "")
                            strLedgerNo = GvParty.Rows[i].Cells[0].Value.ToString();
                        else
                            strLedgerNo = strLedgerNo + "," + GvParty.Rows[i].Cells[0].Value.ToString();
                    }
                }
                if (strLedgerNo != "")
                {
                    long LedgerGrpDetailsNo = 0;

                    for (int i = 0; i < GvParty.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(GvParty.Rows[i].Cells[3].Value) == true)
                        {
                            dbLedgerGroup = new DBMLedgerGroup();
                            mLedgerGroupDetails = new MLedgerGroupDetails();

                            mLedgerGroupDetails.LedgerGrpDetailsNo = LedgerGrpDetailsNo;
                            mLedgerGroupDetails.LedgerGroupNo = ID;
                            mLedgerGroupDetails.LedgerNo = Convert.ToInt64(GvParty.Rows[i].Cells[2].Value);
                            mLedgerGroupDetails.IsActive = true;
                            mLedgerGroupDetails.UserID = DBGetVal.UserID;
                            mLedgerGroupDetails.UserDate = DBGetVal.ServerTime.Date;
                            mLedgerGroupDetails.CompanyNo = DBGetVal.FirmNo;
                            dbLedgerGroup.AddMLedgerGroupDetails(mLedgerGroupDetails);
                        }
                    }
                    if (dbLedgerGroup.ExecuteNonQueryStatements() == true)
                    {
                        if (LedgerGrpDetailsNo == 0)
                        {
                            OMMessageBox.Show("Party Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            BindParyGrid();
                            btnNew.Focus();
                        }
                        else
                        {
                            OMMessageBox.Show("Party Not Saved...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    OMMessageBox.Show("Select Atleast one Party ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void BindParyGrid()
        {
            string sqlQuery = "";

            sqlQuery = "SELECT 0 AS SrNo, MLedger.LedgerName, MLedger.LedgerNo, 'False' AS SelectParty FROM MLedger WHERE NOT EXISTS(SELECT     MLedgerGroupDetails.LedgerNo " +
                      " FROM  MLedgerGroupDetails INNER JOIN MLedgerGroup ON MLedgerGroupDetails.LedgerGroupNo = MLedgerGroup.LedgerGroupNo " +
                      " WHERE(MLedger.LedgerNo = MLedgerGroupDetails.LedgerNo) and MLedgerGroup.LedgerGroupNo = " + ID + ") and MLedger.GroupNo = " + Convert.ToInt64(lstGroupName.SelectedValue) + " and  MLedger.LedgerNo Not IN (24, 25) " +
                      " Union All " +
                      " SELECT 0 AS SrNo, MLedger.LedgerName, MLedger.LedgerNo, 'True' AS SelectParty FROM MLedger WHERE EXISTS (SELECT     MLedgerGroupDetails.LedgerNo " +
                      " FROM  MLedgerGroupDetails INNER JOIN MLedgerGroup ON MLedgerGroupDetails.LedgerGroupNo = MLedgerGroup.LedgerGroupNo " +
                      " WHERE(MLedger.LedgerNo = MLedgerGroupDetails.LedgerNo) and MLedgerGroup.LedgerGroupNo = " + ID + ") and MLedger.GroupNo = " + Convert.ToInt64(lstGroupName.SelectedValue) + "  and  MLedger.LedgerNo Not IN (24, 25) Order by LedgerName";

            GvParty.Rows.Clear();
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GvParty.Rows.Add();
                for (int j = 0; j < GvParty.Columns.Count; j++)
                {
                    GvParty.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
        }
    }
}
