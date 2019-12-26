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

namespace Yadi.Vouchers
{
    public partial class EWayBillAE : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();

        string TransporterNm, mobNo;
        DataTable dtSearch = new DataTable();
        int cntRow;
        bool FlagBilingual;
        public long TransporterNo, ID = 0, LedgerUserNo = 0,No=0;

        public EWayBillAE()
        {
            InitializeComponent();
        }

        private void TransporterAE_Load(object sender, EventArgs e)
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
            TransporterNm = "";

            dtSearch = ObjFunction.GetDataView("Select LedgerNo,LedgerName From MLedger where GroupNo=" + GroupType.Transporter + " order by LedgerName").Table;

            if (dtSearch.Rows.Count > 0)
            {
                //if (Transporter.RequestTransporterNo != 0)
                //{
                //    ID = Transporter.RequestTransporterNo;
                //    TransporterNm = "";
                //}
                //else
                //{
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString()); ;
              //  }
                FillControls();
                SetNavigation();
            }

            txtLanguage.Font = ObjFunction.GetLangFont();
            setDisplay(true);
            btnLangDesc.Enabled = false;
            btnNew.Focus();
            KeyDownFormat(this.Controls);
        }
        public void SetFlag(bool flag)
        {
            txtLanguage.Visible = flag;
            btnLangDesc.Visible = flag;
            lblTransporterLN.Visible = flag;
        }
        private void FillControls()
        {
            EP.SetError(txtTransporterName, "");
            EP.SetError(txtContactNo, "");
            EP.SetError(txtLanguage, "");

            mLedger = dbLedger.ModifyMLedgerByID(ID);
            TransporterNm = mLedger.LedgerName;
            txtTransporterName.Text = mLedger.LedgerName;
            txtContactNo.Text = mLedger.ContactPerson;
            mobNo = mLedger.ContactPerson;
            txtLanguage.Text = mLedger.LedgerLangName;
            LedgerUserNo = Convert.ToInt32(mLedger.LedgerUserNo);
            chkActive.Checked = mLedger.IsActive;
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {
                    dbLedger = new DBMLedger();
                    mLedger = new MLedger();
                    mLedger.LedgerNo = ID;

                    mLedger.LedgerUserNo = LedgerUserNo.ToString();

                    mLedger.LedgerName = txtTransporterName.Text.Trim().ToUpper();
                    mLedger.LedgerLangName = txtLanguage.Text.Trim();
                    mLedger.GroupNo = GroupType.Transporter;
                    mLedger.InvFlag = false;
                    mLedger.MaintainBillByBill = false;
                    mLedger.ContactPerson = txtContactNo.Text.Trim();
                    mLedger.CompanyNo = 1;
                    mLedger.IsActive = chkActive.Checked;
                    mLedger.UserID = DBGetVal.UserID;
                    mLedger.UserDate = DBGetVal.ServerTime.Date;

                    dbLedger.AddMLedger(mLedger);

                    if (dbLedger.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Transporter Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select LedgerNo,LedgerName From MLedger where GroupNo=" + GroupType.Transporter + " order by LedgerName").Table;
                            ID = ObjQry.ReturnLong("Select Max(LedgerNo) From MLedger", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Transporter Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Transporter not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
           // Transporter.RequestTransporterNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
            // BtnExit.Focus();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            dbLedger = new DBMLedger();
            mLedger = new MLedger();

            mLedger.LedgerNo = ID;
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dbLedger.DeleteMLedger(mLedger) == true)
                {
                    OMMessageBox.Show("Transporter Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //Form NewF = new Transporter();
                    //this.Close();
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    FillControls();
                }
                else
                {
                    OMMessageBox.Show("Transporter not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }

            }
            btnNew.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           // Form NewF = new Transporter();
            this.Close();
          //  ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnLangDesc.Enabled = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtTransporterName.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //  nw = 0;
            ID = 0;
            TransporterNm = "";
            mobNo = "";
            ObjFunction.InitialiseControl(this.Controls);
            btnLangDesc.Enabled = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtTransporterName.Focus();
            chkActive.Checked = true;
            // txtContactNo.Text = "0";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dtSearch.Rows.Count > 0)
                NavigationDisplay(2);
            btnLangDesc.Enabled = false;
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " and  LedgerNo >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(LedgerNo),0)as LedgerNo From MLedger where GroupNo=" + GroupType.Transporter + " and LedgerNo <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["LedgerNo"].ToString());
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
            btndelete.Visible = flag;
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


        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion


        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtTransporterName, "");
            EP.SetError(txtContactNo, "");

            if (txtTransporterName.Text.Trim() == "")
            {

                EP.SetError(txtTransporterName, "Enter Ledger Name");
                EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                txtTransporterName.Focus();
                flag = false;
            }

            else if (TransporterNm != txtTransporterName.Text)
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MLedger where Ledgerno not in (" + ID + ") LedgerName = '" + txtTransporterName.Text + "'", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtTransporterName, "Duplicate Ledger Name");
                    EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                    txtTransporterName.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
        }


        private void TransporterAE_FormClosing(object sender, FormClosingEventArgs e)
        {
          //  Transporter.RequestTransporterNo = 0;
            TransporterNm = "";
        }

        private void txtTransporterName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtTransporterName, "");
                if (txtTransporterName.Text.Trim() != "")
                {
                    //txtLangFullDesc.Text = "";
                    if (TransporterNm != txtTransporterName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MLedger where Ledgerno not in (" + ID + ") LedgerName = '" + txtTransporterName.Text + "' AND GroupNo=" + GroupType.Transporter + " ", CommonFunctions.ConStr) != 0)

                       
                        {
                            EP.SetError(txtTransporterName, "Duplicate category Name");
                            EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                            txtTransporterName.Focus();
                        }
                        else if (FlagBilingual == true)
                        {

                            if (txtLanguage.Text.Trim().Length == 0)
                            {
                                btnLangDesc_Click(btnLangDesc, null);
                            }
                        }

                        else
                            txtContactNo.Focus();


                    }
                    else
                    {
                        if (FlagBilingual == true)
                        {
                            //txtLangFullDesc.Focus();
                            if (txtTransporterName.Text.Trim().Length == 0)
                            {
                                btnLangDesc_Click(btnLangDesc, null);
                            }
                        }
                        else
                            txtContactNo.Focus();
                    }
                }
                else
                {
                    txtTransporterName.Focus();
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

        private void txtTransporterName_KeyDown(object sender, KeyEventArgs e)
        {
            //  EP.SetError(txtTransporterName, "");
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtTransporterName.Text == "")
                {
                    //EP.SetError(txtTransporterName, "Enter the contact name");
                    //EP.SetIconAlignment(txtTransporterName, ErrorIconAlignment.MiddleRight);
                    txtTransporterName.Focus();
                }
                else
                {
                    //txtTransporterLN.Focus();
                    txtContactNo.Focus();
                }
            }
        }

        //private void txtContactNo_Leave(object sender, EventArgs e)
        //{
        //    EP.SetError(txtContactNo, "");
        //    if (txtContactNo.Text != "")
        //    {
        //        if (txtContactNo.Text.Trim().Length < 10 && txtContactNo.Text.Trim() != "0")
        //        {
        //            EP.SetError(txtContactNo, "Enter Valid Mobile No");
        //            EP.SetIconAlignment(txtContactNo, ErrorIconAlignment.MiddleRight);
        //            txtContactNo.Focus();
        //            flag = false;
        //        }
        //        else if (mobNo != txtContactNo.Text.Trim())
        //        {
        //            if (txtContactNo.Text.Trim() != "1111111111" && txtContactNo.Text.Trim() != "0")
        //            {
        //                if (ObjQry.ReturnInteger("Select Count(*) from MLedger where ContactPerson='" + txtContactNo.Text.Trim() + "' And LedgerNo!=" + ID + "", CommonFunctions.ConStr) != 0)
        //                {
        //                    EP.SetError(txtContactNo, "Duplicate contact No");
        //                    EP.SetIconAlignment(txtContactNo, ErrorIconAlignment.MiddleRight);
        //                    txtContactNo.Focus();
        //                    flag = false;
        //                }
        //                else
        //                    EP.SetError(txtContactNo, "");
        //            }
        //        }
        //    }

        //    else
        //        EP.SetError(txtContactNo, "");
        //}

        private void txtTransporterName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //txtItemLang.Text = ObjFunction.ChecklLangVal(txtItemName.Text);
                txtTransporterName_Leave(sender, new EventArgs());
               txtLanguage.Focus();
            }
        }

       
        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
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

        private void btnLangDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLanguage.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtTransporterName.Text.Trim(), txtLanguage.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLanguage.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtTransporterName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtTransporterName.Text.Trim(), txtLanguage.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLanguage.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLanguage.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

       
       
    }
}

