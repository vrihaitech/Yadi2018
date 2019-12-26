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
    public partial class TaxTypeAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();        

        DBMGroup dbGroup = new DBMGroup();
        MGroup mGroup = new MGroup();

        string TaxTypeNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long TaxTypeNo, ID;
        
        bool isDoProcess = false;

        public TaxTypeAE()
        {
            InitializeComponent();
        }

        private void TaxTypeAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                TaxTypeNm = "";
                ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + " and IsActive='true' ");
                dtSearch = ObjFunction.GetDataView("Select GroupNo From MGroup  where GroupNo <>20 And GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (TaxType.RequestTaxTypeNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = TaxType.RequestTaxTypeNo;
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
                EP.SetError(txtTaxTypeName, "");
                EP.SetError(cmbControlGroup, "");

                mGroup = dbGroup.ModifyMGroupByID(ID);
                TaxTypeNm = mGroup.GroupName;
                txtTaxTypeName.Text = mGroup.GroupName;
                chkActive.Checked = mGroup.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";
                ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + " and (IsActive='true' or GroupNo =" + mGroup.ControlGroup + ") ");
                cmbControlGroup.SelectedValue = mGroup.ControlGroup.ToString();
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

                    dbGroup = new DBMGroup();
                    mGroup = new MGroup();
                    mGroup.GroupNo = ID;
                    mGroup.GroupName = txtTaxTypeName.Text.Trim().ToUpper();
                    mGroup.ControlGroup = ObjFunction.GetComboValue(cmbControlGroup);
                    mGroup.IsActive = chkActive.Checked;
                    mGroup.UserID = DBGetVal.UserID;
                    mGroup.UserDate = DBGetVal.ServerTime.Date;
                    if (dbGroup.AddMGroup(mGroup) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("TaxType Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select GroupNo From MGroup  where GroupNo <>20 And GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "").Table;
                            ID = ObjQry.ReturnLong("Select Max(GroupNo) From MGroup Where  ControlGroup in(" + GroupType.DutiesAndTaxes + ") ", CommonFunctions.ConStr);
                            SetNavigation();
                            ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "");
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("TaxType Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "");
                            FillControls();
                        }
                        //ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "");
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("TaxType not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            TaxType.RequestTaxTypeNo = 0;
            this.Close();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtTaxTypeName, ""); 
            EP.SetError(cmbControlGroup, "");

            if (txtTaxTypeName.Text.Trim() == "")
            {
                EP.SetError(txtTaxTypeName, "Enter Name");
                EP.SetIconAlignment(txtTaxTypeName, ErrorIconAlignment.MiddleRight);
                txtTaxTypeName.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbControlGroup) <= 0)
            {
                EP.SetError(cmbControlGroup, "Select Control Group");
                EP.SetIconAlignment(cmbControlGroup, ErrorIconAlignment.MiddleRight);
                cmbControlGroup.Focus();
            }
            else if (TaxTypeNm.ToUpper() != txtTaxTypeName.Text.Trim().ToUpper())
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MGroup where GroupName = '" + txtTaxTypeName.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtTaxTypeName, "Duplicate Godown Name");
                    EP.SetIconAlignment(txtTaxTypeName, ErrorIconAlignment.MiddleRight);
                    txtTaxTypeName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;



            //if (ObjFunction.GetComboValue(cmbControlGroup) <= 0)
            //{

            //    EP.SetError(cmbControlGroup, "Select Control Group Name");
            //    EP.SetIconAlignment(cmbControlGroup, ErrorIconAlignment.MiddleRight);
            //    //if (flag) {
            //    flag = false; cmbControlGroup.Focus();
            //}         
            //if (txtTaxTypeName.Text.Trim() == "")
            //{

            //    EP.SetError(txtTaxTypeName, "Enter TaxType Name");
            //    EP.SetIconAlignment(txtTaxTypeName, ErrorIconAlignment.MiddleRight);
            //    txtTaxTypeName.Focus();
            //}
            //else if (TaxTypeNm != txtTaxTypeName.Text)
            //{
            //    if (ObjQry.ReturnInteger("Select Count(*) from MGroup From MGroup  where GroupNo <>20 And GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "", CommonFunctions.ConStr) != 0)                
            //    {
            //        EP.SetError(txtTaxTypeName, "Duplicate TaxType  Name");
            //        EP.SetIconAlignment(txtTaxTypeName, ErrorIconAlignment.MiddleRight);
            //        txtTaxTypeName.Focus();
            //    }
            //    else
            //        flag = true;
            //}
            //else
            //    flag = true;
            //return flag;
        }

        private void TaxTypeAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            TaxType.RequestTaxTypeNo = 0;
            TaxTypeNm = "";
        }

        private void txtTaxTypeName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtTaxTypeName, "");
                if (txtTaxTypeName.Text.Trim() != "")
                {
                    if (TaxTypeNm != txtTaxTypeName.Text.Trim().ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from From MGroup  where GroupNo <>20 And GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtTaxTypeName, "Duplicate TaxType  Name");
                            EP.SetIconAlignment(txtTaxTypeName, ErrorIconAlignment.MiddleRight);
                            txtTaxTypeName.Focus();
                        }
                        else
                        {
                            cmbControlGroup.Focus();
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
                TaxTypeNm = "";
                ID = 0;
                mGroup = new MGroup();
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + " and IsActive='true' ");
                chkActive.Checked = true;
                txtTaxTypeName.Focus();
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
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new TaxType();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbGroup = new DBMGroup();
                mGroup = new MGroup();

                mGroup.GroupNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbGroup.DeleteMGroup(mGroup) == true)
                    {
                        OMMessageBox.Show("TaxType Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + "");
                        FillControls();

                    }
                    else
                    {
                        OMMessageBox.Show("TaxType not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                btnNew.Focus();
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
            txtTaxTypeName.Focus();
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";

        }

        private void TaxTypeAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                    if (cmbControlGroup.Enabled == true)
                    {
                        long CId = ObjFunction.GetComboValue(cmbControlGroup);
                        ObjFunction.FillCombo(cmbControlGroup, "Select GroupNo,GroupName From MGroup where GroupNo in(" + GroupType.DutiesAndTaxes + ") OR   GroupNo  in (Select G.GroupNo From MGroup G Where G.ControlGroup  in(" + GroupType.DutiesAndTaxes + ")" + ObjFunction.GetGroupQuery(3, "G.ControlGroup", "" + GroupType.DutiesAndTaxes + "", "G.GroupNo", 3) + " and (IsActive='true' or GroupNo =" + mGroup.ControlGroup + ") ");
                        cmbControlGroup.SelectedValue = CId;
                    }
                    isDoProcess = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TaxTypeAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

        private void txtTaxTypeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                cmbControlGroup.Focus();
            }
        }

        private void cmbControlGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                chkActive.Focus();
            }
        }

        private void chkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                BtnSave.Focus();
            }
        }      
    }
}
