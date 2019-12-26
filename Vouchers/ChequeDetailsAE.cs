using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;using OMControls;

namespace Yadi.Utilities
{
    public partial class ChequeDetailsAE : Form
    {
        
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTChequeNo dbTChequeNo = new DBTChequeNo();
        TChequeNo tChequeNo = new TChequeNo();
        TChequeNoDetails tChequeNoDetails = new TChequeNoDetails();
       
        DataTable dtSearch = new DataTable();
        DataTable dt = new DataTable();
        DataTable dtCheck = new DataTable();
        int cntRow;
        public long ChequeNo, ID;

        #region init
        public ChequeDetailsAE()
        {
            InitializeComponent();
        }

        private void CouponDetailsAE_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbBankAccount, "Select LedgerNo,LedgerName from MLedger Where GroupNo = " + GroupType.BankAccounts + " and IsActive='true' order by LedgerName");

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);

            dtSearch = ObjFunction.GetDataView("Select PkChequeNo from TChequeNo order by PkChequeNo").Table;
            if (dtSearch.Rows.Count > 0)
            {
                if (ChequeDetails.RequestChequeNo == 0)
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                else
                    ID = ChequeDetails.RequestChequeNo;
                FillControls();
                SetNavigation();

                dgChequeDetails.Enabled = false;
            }

            setDisplay(true);
            btnNew.Focus();
            KeyDownFormat(this.Controls);

            dgChequeDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left));


        }

        #endregion

        #region Fill 
            private void FillControls()
            {
                tChequeNo = dbTChequeNo.ModifyTChequeNoByID(ID);

                lblChequeBookNo.Text = Convert.ToString(ObjQry.ReturnLong("Select ChequeUserNo from TChequeNo Where PkChequeNo = " + ID, CommonFunctions.ConStr));

                cmbBankAccount.SelectedValue = ObjQry.ReturnDouble("Select LedgerNo from TChequeNo Where PkChequeNo=" + ID, CommonFunctions.ConStr);

                txtFrom.Text = Convert.ToString(tChequeNo.FromChequeNo);

                txtTo.Text = Convert.ToString(tChequeNo.ToChequeNo);

                calculate();

                txtNarration.Text = tChequeNo.Remark;

                chkActive.Checked = tChequeNo.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";

                FillGrid();

            }

            private void FillGrid()
        {
            if (this.dgChequeDetails.DataSource != null)
            {
                this.dgChequeDetails.DataSource = null;
            }
            else
            {
                this.dgChequeDetails.Rows.Clear();
            }
             string sqlQuery = "SELECT TChequeNoDetails.PkSrNo, TChequeNoDetails.ChequeDetailsUserNo As 'Cheque No', TChequeNoDetails.IsActive AS 'Chk'"+
                              " FROM TChequeNo INNER JOIN "+
                              " TChequeNoDetails ON TChequeNo.PkChequeNo = TChequeNoDetails.FkChequeNo"+
                              " Where TChequeNoDetails.FkChequeNo = " + ID;

            dt = ObjFunction.GetDataView(sqlQuery).Table;
            dgChequeDetails.DataSource = dt.DefaultView;

            dgChequeDetails.Columns[0].Visible = false;
            dgChequeDetails.Columns[1].Width = 100;
            dgChequeDetails.Columns[2].Width = 50;
            if (dgChequeDetails.Rows.Count > 0)
            {
                //calculate();
            }
        }
        #endregion 

        #region Navigation Methods

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
                    MessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cntRow = cntRow + 1;
                }
                else if (cntRow > dtSearch.Rows.Count - 1)
                {
                    MessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region Button Click

        private void btnNew_Click(object sender, EventArgs e)
        {
            ID = 0;
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons (false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            long chqno = ObjQry.ReturnLong("Select (Max(ChequeUserNo)+1) as 'ChequeBookNo' from TChequeNo", CommonFunctions.ConStr);
            if (chqno == 0)
            {
                lblChequeBookNo.Text = "1";
            }
            else
            {
                lblChequeBookNo.Text = Convert.ToString(chqno);
            }
            

            lblTotalNoCheques.Text = "";
            if (this.dgChequeDetails.DataSource != null)
            {
                this.dgChequeDetails.DataSource = null;
            }
            else
            {
                this.dgChequeDetails.Rows.Clear();
            }
            cmbBankAccount.Focus();
            dgChequeDetails.Enabled = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
            dgChequeDetails.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            dgChequeDetails.Enabled = false;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            setView(false);

            txtNarration.Enabled = true;
            chkActive.Enabled = true;

            bool chkflag = true;
            dtCheck = ObjFunction.GetDataView("Select FkVoucherTrnNo from TChequeNoDetails  Where FkChequeNo = " + ID).Table;
            for (int j = 0; j < dtCheck.Rows.Count; j++)
            {
                foreach (DataRow row in dtCheck.Rows)
                {
                    if (Convert.ToInt32(row["FkVoucherTrnNo"].ToString()) > 0)
                    {
                        chkflag = false;
                        break;
                    }
                }
            }

            cmbBankAccount.Enabled = chkflag;

                for (int i = 0; i < dgChequeDetails.Rows.Count; i++)
                {
                    dgChequeDetails.Columns["Cheque No"].ReadOnly = true;
                }
            dgChequeDetails.Enabled = true;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new ChequeDetails();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete the record ?", CommonFunctions.ErrorTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dbTChequeNo = new DBTChequeNo();
                tChequeNo = new TChequeNo();
                tChequeNo.PkChequeNo = ID;
                dbTChequeNo.DeleteTChequeNo(tChequeNo);
                
                for (int i = 0; i < dgChequeDetails.Rows.Count - 1; i++)
                {
                    //dbTVoucherEntry.UpdateTStockBarCode(Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString()));
                }

                MessageBox.Show("Record deleted successfully.....", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);


                dtSearch = ObjFunction.GetDataView("Select PkChequeNo from TChequeNo Where PkChequeNo=" + ID).Table;
                ID = ObjQry.ReturnLong("Select Max(PkChequeNo) FRom TChequeNo Where PkChequeNo=" + ID, CommonFunctions.ConStr);
                SetNavigation();
                FillControls();

                setDisplay(true);
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dgChequeDetails.Enabled = false;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            ChequeDetails.RequestChequeNo = 0;
            this.Close();
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
                btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                btnLast_Click(sender, e);
            }
        }

        private void dgCouponDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                BtnSave.Focus();
        }
        #endregion

        #region Validations and Calculations
        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtNarration, "");
            EP.SetError(txtTo, "");
            EP.SetError(txtFrom, "");
            EP.SetError(cmbBankAccount, "");
            
           if (ObjFunction.GetComboValue(cmbBankAccount) <= 0)
            {
                EP.SetError(cmbBankAccount, "Select Bank Account");
                EP.SetIconAlignment(cmbBankAccount, ErrorIconAlignment.MiddleRight);
                cmbBankAccount.Focus();
            }
           else if (Convert.ToInt64(txtFrom.Text) > Convert.ToInt64(txtTo.Text))
           {
               EP.SetError(txtFrom, "Starting Cheque No should be Greater than End Cheque No");
               EP.SetIconAlignment(txtFrom, ErrorIconAlignment.MiddleRight);
               txtFrom.Focus();
           }
           else
                flag = true;
            return flag;
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;

            if (dtSearch.Rows.Count == 0)
            {
                btnFirst.Visible = false;
                btnPrev.Visible = false;
                btnNext.Visible = false;
                btnLast.Visible = false;
            }
        }

        public void setView(bool flag)
        {
            cmbBankAccount.Enabled = flag;
            txtFrom.Enabled = flag;
            txtTo.Enabled = flag;
            txtNarration.Enabled = flag;
            chkActive.Enabled = flag;

        }

        public void calculate()
        {
            lblTotalNoCheques.Text = Convert.ToString((Convert.ToInt64(txtTo.Text) - Convert.ToInt64(txtFrom.Text)) + 1);
        }

       #endregion

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";


            if (dgChequeDetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgChequeDetails.Rows.Count; i++)
                {
                    dgChequeDetails.Rows[i].Cells["Chk"].Value = chkActive.Checked;
                }
            }
        }

        private void SetValue()
        {
            if (Validations())
            {
                dbTChequeNo = new DBTChequeNo();
                tChequeNo = new TChequeNo();
                tChequeNoDetails = new TChequeNoDetails();


                tChequeNo.PkChequeNo = ID;

                tChequeNo.ChequeUserNo =Convert.ToInt64(lblChequeBookNo.Text);

                tChequeNo.LedgerNo = ObjFunction.GetComboValue(cmbBankAccount);
                tChequeNo.FromChequeNo = Convert.ToInt64(txtFrom.Text);
                tChequeNo.ToChequeNo = Convert.ToInt64(txtTo.Text);
                if (txtNarration.Text == "")
                {
                    tChequeNo.Remark = ".";
                }
                else
                {
                    tChequeNo.Remark = txtNarration.Text;
                }
                
                tChequeNo.IsActive = chkActive.Checked;
                tChequeNo.UserId = DBGetVal.UserID;
                tChequeNo.UserDate = DBGetVal.ServerTime;
                dbTChequeNo.AddTChequeNo(tChequeNo);
                int count = 0;
                if (ID == 0)
                {
                    for (int i = 0; i < Convert.ToInt64(lblTotalNoCheques.Text); i++)
                    {
                        tChequeNoDetails = new TChequeNoDetails();
                        tChequeNoDetails.PkSrNo = 0;
                        tChequeNoDetails.ChequeDetailsUserNo = Convert.ToString(Convert.ToInt64(txtFrom.Text) + count);
                        tChequeNoDetails.FkVoucherTrnNo = 0;
                        tChequeNoDetails.IsActive = true;
                        dbTChequeNo.AddTChequeNoDetails(tChequeNoDetails);
                        count =count+ 1;
                    }
                }
                else
                {
                    for (int i = 0; i < dgChequeDetails.Rows.Count; i++)
                    {

                        tChequeNoDetails = dbTChequeNo.ModifyTChequeNoDetailsByID(Convert.ToInt64(dgChequeDetails.Rows[i].Cells["PkSrno"].Value));
                        tChequeNoDetails.IsActive = Convert.ToBoolean(dgChequeDetails.Rows[i].Cells["Chk"].Value);
                        dbTChequeNo.AddTChequeNoDetails(tChequeNoDetails);
                    }
                }

                long tempID = dbTChequeNo.ExecuteNonQueryStatements();

                if (tempID != 0)
                {
                    MessageBox.Show("Cheque(s) Added Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dtSearch = ObjFunction.GetDataView("Select PkchequeNo from TChequeNo").Table;
                    ID = tempID;

                    SetNavigation();
                    FillControls();
                    setDisplay(true);
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    btnNew.Focus();
                }
                else
                    MessageBox.Show("Cheque(s) Not Added Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTo_Leave(txtNarration, new EventArgs());
            }
        }

        private void txtTo_Leave(object sender, EventArgs e)
        {
            EP.SetError(txtTo, "");
            if (txtTo.Text == "")
            {
                EP.SetError(txtTo, "Please Enter Cheque Number");
                EP.SetIconAlignment(txtTo, ErrorIconAlignment.MiddleRight);
                txtTo.Focus();
            }
            else if (ObjFunction.CheckNumeric(txtTo.Text) == false)
            {
                EP.SetError(txtTo, "Please Enter numeric value");
                EP.SetIconAlignment(txtTo, ErrorIconAlignment.MiddleRight);
                txtTo.Focus();
            }
            else if (txtFrom.Text != "" && Convert.ToInt64(txtTo.Text) < Convert.ToInt64(txtFrom.Text))
            {
                    EP.SetError(txtTo, "Ending Cheque No must be Less than Starting Cheque No");
                    EP.SetIconAlignment(txtTo, ErrorIconAlignment.MiddleRight);
                    txtTo.Focus();
            }
            else if (txtFrom.Text !="" && txtTo.Text != "")
            {
                calculate();
            }
            else if (txtFrom.Text == "")
            {
                EP.SetError(txtFrom, "Please Enter Starting Cheque No");
            }
        }

        private void txtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFrom_Leave(txtNarration, new EventArgs());
            }
        }

        private void txtFrom_Leave(object sender, EventArgs e)
        {
            EP.SetError(txtFrom, "");
            if (txtFrom.Text == "")
            {
                EP.SetError(txtFrom, "Please Enter Cheque Number");
                EP.SetIconAlignment(txtFrom, ErrorIconAlignment.MiddleRight);
                txtFrom.Focus();
            }
            else if (ObjFunction.CheckNumeric(txtFrom.Text) == false)
            {
                EP.SetError(txtFrom, "Please Enter numeric value");
                EP.SetIconAlignment(txtFrom, ErrorIconAlignment.MiddleRight);
                txtFrom.Focus();
            }
            else if (txtTo.Text != "" && Convert.ToInt64(txtFrom.Text) > Convert.ToInt64(txtTo.Text))
            {
                   EP.SetError(txtFrom, "Starting Cheque No must be Less than End Cheque No");
                   EP.SetIconAlignment(txtFrom, ErrorIconAlignment.MiddleRight);
                   txtFrom.Focus();
            }

        }
    }
}




