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
    public partial class ExpensesEntryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbtVaucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long ID, Vtype = 0, CompanyNo;
        long grpno = 0;

        public ExpensesEntryAE()
        {
            InitializeComponent();
        }

        public ExpensesEntryAE(long VouType)
        {
            InitializeComponent();
            if (VouType == VchType.ExpensesEntry)
            {
                Vtype = VchType.ExpensesEntry;
                this.Text = "Expenses Entry";
                this.Name = "Expenses Entry";
                lblType.Text = "Expenses Type :";
            }
            else if (VouType == VchType.CashReceipt)
            {
                Vtype = VchType.CashReceipt;
                this.Text = "Cash Receipt Entry";
                this.Name = "Cash Receipt Entry";
                lblType.Text = "Party :";
                lblType2.Text = "Receipt Type :";
            }
            else if (VouType == VchType.CashPayment)
            {
                Vtype = VchType.CashPayment;
                this.Text = "Cash Payment Entry";
                this.Name = "Cash Payment Entry";
                lblType.Text = "Party :";
            }
            
        }

        private void ExpensesEntry_Load(object sender, EventArgs e)
        {
          
            CompanyNo = DBGetVal.FirmNo;

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            dtpDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            dtpChqDate.Text = "";
            if (Vtype == VchType.ExpensesEntry)
            {
                ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MLedger.GroupNo IN (13,15,4,5,24 )) AND (MLedger.IsActive = 'true') AND LedgerNo>15 order by MLedger.LedgerName");
                ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.CashInhand + ", " + GroupType.BankAccounts + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo=1 order by MLedger.LedgerName");
            }
            else if (Vtype == VchType.CashReceipt)
            {
                ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo not IN (" + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 order by MLedger.LedgerName");
                ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.CashInhand + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo=1 order by MLedger.LedgerName");
            }
            else if (Vtype == VchType.CashPayment)
            {
                ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo not IN (" + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 order by MLedger.LedgerName");
                ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.CashInhand + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo=1 order by MLedger.LedgerName");
            }
            
            setDisplay(false);
            InitControls();
            dtSearch = ObjFunction.GetDataView("SELECT  PkVoucherNo FROM  TVoucherEntry  WHERE  (VoucherTypeCode = " + Vtype + ") and CompanyNo="+CompanyNo+"").Table;
            if (dtSearch.Rows.Count > 0)
            {
                if (ExpensesEntry.RequestJVNo == 0)
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                else
                    ID = ExpensesEntry.RequestJVNo;
                FillControls();
                SetNavigation();
            }
            btnNew.Focus();
            lblAmountWord.Font = ObjFunction.GetFont(FontStyle.Bold, 10);
            lblAmountWord.ForeColor = Color.SteelBlue;
            KeyDownFormat(this.Controls);
        }

        public void InitControls()
        {
            dtpDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            txtVoucherNo.Text = ObjQry.ReturnLong("Select ISNULL(Max(VoucherUserNo),0) + 1 From TVoucherEntry  where vouchertypecode=" + Vtype +" and CompanyNo="+CompanyNo+"", CommonFunctions.ConStr).ToString();
            cmbExpType.SelectedIndex = 0;
            cmbPayType.SelectedIndex = 0;
            txtChqNo.Text = "0";
            txtAmount.Text = "";
            txtNarration.Text = "";
            dtpDate.Focus();
        }

        private void FillControls()
        {
            EP.SetError(txtAmount, "");
            EP.SetError(txtNarration, "");
            EP.SetError(cmbExpType, "");
            EP.SetError(cmbPayType, "");            
            EP.SetError(txtChqNo, "");

            tVoucherEntry = dbtVaucherEntry.ModifyTVoucherEntryByID(ID);
            txtVoucherNo.Text = tVoucherEntry.VoucherUserNo.ToString();
            dtpDate.Text = Convert.ToDateTime(tVoucherEntry.VoucherDate).ToString("dd-MMM-yyyy");
            txtAmount.Text = tVoucherEntry.BilledAmount.ToString();
            lblAmountWord.Text = NumberToWordsIndian.getWords(txtAmount.Text);
            lblAmountWord.Visible = true;
            txtNarration.Text = tVoucherEntry.Narration;
            txtChqNo.Text = tVoucherEntry.ChequeNo.ToString();
            dtpChqDate.Text = (tVoucherEntry.ClearingDate == Convert.ToDateTime("01/01/1900")) ? dtpChqDate.Text = "" : tVoucherEntry.ClearingDate.ToString("dd-MMM-yyyy");

            //long ExLedNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherDetails  Where FKVoucherNo=" + ID + " AND SignCode=" + (Vtype == VchType.ExpensesEntry ? 1 : 2).ToString(), CommonFunctions.ConStr);
            //long LedNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherDetails  Where FKVoucherNo=" + ID + " AND SignCode=" + (Vtype == VchType.ExpensesEntry ? 2 : 1).ToString(), CommonFunctions.ConStr);

            long ExLedNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherDetails  Where FKVoucherNo=" + ID + " AND VoucherSrNo=1", CommonFunctions.ConStr);
            long LedNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherDetails  Where FKVoucherNo=" + ID + " AND VoucherSrNo=2", CommonFunctions.ConStr);
            if (Vtype == VchType.ExpensesEntry)
            {
                ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MLedger.GroupNo IN (13,15,4,5,24 )) AND (MLedger.IsActive = 'true') AND LedgerNo>15 OR LedgerNo=" + ExLedNo + " order by MLedger.LedgerName");
                ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.CashInhand + ", " + GroupType.BankAccounts + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo in (1,"+ LedNo +") order by MLedger.LedgerName");
            }
            else if (Vtype == VchType.CashReceipt)
            {
                ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 OR LedgerNo=" + ExLedNo + " order by MLedger.LedgerName");
                ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.CashInhand + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo in (1,"+ LedNo +")  order by MLedger.LedgerName");
            }
            else if (Vtype == VchType.CashPayment)
            {
               // ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 OR LedgerNo=" + ExLedNo + " order by MLedger.LedgerName");
                ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 OR LedgerNo IN (" + ExLedNo + " ) OR MLedger.GroupNo=  " + GroupType.BankAccounts + " order by MLedger.LedgerName");
              
                
                ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.CashInhand + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo in (1," + LedNo + ")  order by MLedger.LedgerName");
            }
            cmbExpType.SelectedValue = ExLedNo;
            cmbPayType.SelectedValue = LedNo;

            grpno = ObjQry.ReturnLong("SELECT  GroupNo FROM  MLedger WHERE  (LedgerNo = " + LedNo + ")", CommonFunctions.ConStr);
            if (grpno != GroupType.BankAccounts)
            {
                setDisplay(false);
            }
            else
            {
                setDisplay(true);
            }
            bool flagCancelled = ObjQry.ReturnBoolean("select IsCancel from TVoucherEntry where pkvoucherno = " + ID + "", CommonFunctions.ConStr);
            if (flagCancelled == true)
            {
                btnUpdate.Visible = false;
                btnDelete.Enabled = false;
            }
            else
            {
                btnUpdate.Visible = true;
                btnDelete.Enabled = true;
            }
        }

        public void SetValue()
        {
            if (Validations() == true)
            {
                dbtVaucherEntry = new DBTVaucherEntry();
                int VoucherSrNo = 1;
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;
                tVoucherEntry.VoucherTypeCode = Vtype;// == VchType.ExpensesEntry ? VchType.ExpensesEntry : VchType.CashReceipt;
                tVoucherEntry.VoucherUserNo = Convert.ToInt64(txtVoucherNo.Text);
                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpDate.Text);
                tVoucherEntry.VoucherTime = dtpDate.Value;
                tVoucherEntry.Narration = txtNarration.Text.Trim();
                tVoucherEntry.Reference = "";
                tVoucherEntry.ChequeNo = grpno != GroupType.BankAccounts ? 0 : Convert.ToInt64(txtChqNo.Text);
                tVoucherEntry.ClearingDate = tVoucherEntry.ChequeNo == 0 ? Convert.ToDateTime("1/1/1900") : Convert.ToDateTime(dtpChqDate.Text);
                tVoucherEntry.CompanyNo = CompanyNo;
                tVoucherEntry.BilledAmount = Convert.ToDouble(txtAmount.Text.Trim());
                tVoucherEntry.ChallanNo = "";
                tVoucherEntry.Remark = "";
                tVoucherEntry.MacNo = DBGetVal.MacNo;
                tVoucherEntry.PayTypeNo = 0;
                tVoucherEntry.RateTypeNo = 0;
                tVoucherEntry.TaxTypeNo = 0;
                tVoucherEntry.OrderType = 0;
                tVoucherEntry.DiscPercent = 0;
                tVoucherEntry.DiscAmt = 0;
                tVoucherEntry.MixMode = 0;
                tVoucherEntry.IsItemLevelDisc = false;
                tVoucherEntry.IsFooterLevelDisc = false;

                tVoucherEntry.TransporterCode = 0;
                tVoucherEntry.TransPayType = 0;
                tVoucherEntry.LRNo = "";
                tVoucherEntry.TransportMode = 0;
                tVoucherEntry.TransNoOfItems = 0;

                tVoucherEntry.UserID = DBGetVal.UserID;
                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                
                dbtVaucherEntry.AddTVoucherEntry(tVoucherEntry);

                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = ObjQry.ReturnLong("Select PkVoucherTrnNo from TVoucherDetails where SignCode=1 and FkVoucherNo="+ID, CommonFunctions.ConStr);
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbExpType);
                if (Vtype == VchType.ExpensesEntry)
                {
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text.Trim());
                    tVoucherDetails.Credit = 0;
                }
                else if (Vtype == VchType.CashReceipt)
                {
                    tVoucherDetails.SignCode = 2;
                    //tVoucherDetails.Debit = 0;

                    tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text.Trim());
                    tVoucherDetails.Credit = 0;
                    
                }
                else if (Vtype == VchType.CashPayment)
                {
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.Debit = 0;
                    
                    //tVoucherDetails.Credit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text.Trim());
                }
                //tVoucherDetails.SignCode = Vtype == VchType.ExpensesEntry ? 1 : 2;
                //tVoucherDetails.Debit = Vtype == VchType.ExpensesEntry ? Convert.ToDouble(txtAmount.Text.Trim()) : 0;
                //tVoucherDetails.Credit = Vtype == VchType.ExpensesEntry ? 0 : Convert.ToDouble(txtAmount.Text.Trim());
                tVoucherDetails.Narration = "";
                tVoucherDetails.CompanyNo = CompanyNo;
                tVoucherDetails.SrNo = Others.Party;
                dbtVaucherEntry.AddTVoucherDetails(tVoucherDetails);

                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = ObjQry.ReturnLong("Select PkVoucherTrnNo from TVoucherDetails where SignCode=2 and FkVoucherNo="+ID, CommonFunctions.ConStr);
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPayType);
                if (Vtype == VchType.ExpensesEntry)
                {
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text.Trim());
                }
                else if (Vtype == VchType.CashReceipt)
                {
                    tVoucherDetails.SignCode = 1;
                    
                  //  tVoucherDetails.Credit = 0;
                    //
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text.Trim());
                }
                else if (Vtype == VchType.CashPayment)
                {
                    tVoucherDetails.SignCode = 2;
                    //tVoucherDetails.Debit = 0;
                    tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text.Trim());
                    tVoucherDetails.Credit = 0;
                }
                //tVoucherDetails.SignCode = Vtype == VchType.ExpensesEntry ? 2 : 1;
                
                //tVoucherDetails.Debit = Vtype == VchType.ExpensesEntry ? 0 : Convert.ToDouble(txtAmount.Text.Trim());
                //tVoucherDetails.Credit = Vtype == VchType.ExpensesEntry ? Convert.ToDouble(txtAmount.Text.Trim()) : 0;
                tVoucherDetails.Narration = "";
                tVoucherDetails.CompanyNo = CompanyNo;
                tVoucherDetails.SrNo = 0;
                dbtVaucherEntry.AddTVoucherDetails(tVoucherDetails);
                long tempID = dbtVaucherEntry.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    if (ID == 0)
                    {
                        if (Vtype == VchType.ExpensesEntry)
                            OMMessageBox.Show("Expenses Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        else if (Vtype == VchType.CashReceipt)
                            OMMessageBox.Show("Cash Receipt Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        else if (Vtype == VchType.CashPayment)
                            OMMessageBox.Show("Cash Payment Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        dtSearch = ObjFunction.GetDataView("SELECT  PkVoucherNo FROM  TVoucherEntry  WHERE  (VoucherTypeCode = " + Vtype + ") and CompanyNo="+CompanyNo+"").Table;
                        ID = ObjQry.ReturnLong("Select Max(PkVoucherNo) From TVoucherEntry where (VoucherTypeCode = " + Vtype + ") and CompanyNo="+CompanyNo+"", CommonFunctions.ConStr);
                        SetNavigation();
                        FillControls();
                        
                    }
                    else
                    {
                        if (Vtype == VchType.ExpensesEntry)
                            OMMessageBox.Show("Expenses Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        else if (Vtype == VchType.CashReceipt)
                            OMMessageBox.Show("Cash Receipt Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        else if (Vtype == VchType.CashPayment)
                            OMMessageBox.Show("Cash Payment Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }

                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                }
                else
                {
                    if (Vtype == VchType.ExpensesEntry)
                        OMMessageBox.Show("Expenses not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    else if (Vtype == VchType.CashReceipt)
                        OMMessageBox.Show("Cash Receipt not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    else if (Vtype == VchType.CashPayment)
                        OMMessageBox.Show("Cash Payment not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Vouchers.ExpensesEntry.RequestJVNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(cmbExpType, "");
            EP.SetError(cmbPayType, "");
            EP.SetError(txtAmount, "");
            EP.SetError(txtChqNo, "");
      
            if (ObjFunction.GetComboValue(cmbExpType) <= 0)
            {
                EP.SetError(cmbExpType, "Select Type");
                EP.SetIconAlignment(cmbExpType, ErrorIconAlignment.MiddleRight);
                cmbExpType.Focus();
            }
            else if (ObjFunction.GetComboValue(cmbPayType) <= 0)
            {
                EP.SetError(cmbPayType, "Select Payment Type");
                EP.SetIconAlignment(cmbPayType, ErrorIconAlignment.MiddleRight);
                cmbPayType.Focus();
            }
            else if (txtAmount.Text.Trim()=="")
            {
                EP.SetError(txtAmount, "Enter Amount");
                EP.SetIconAlignment(txtAmount, ErrorIconAlignment.MiddleRight);
                txtAmount.Focus();
            }
            else if (txtChqNo.Text.Trim() == "")
            {
                EP.SetError(txtChqNo, "Enter Cheque Number");
                EP.SetIconAlignment(txtChqNo, ErrorIconAlignment.MiddleRight);
                txtChqNo.Focus();
            }
            else
                flag = true;
            return flag;
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            long No = 0;
            if (type == 5)
            {
                if (dtSearch.Rows.Count > 0)
                {
                    No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    ID = No;
                }
            }
            else if (type == 1)
            {
                if (dtSearch.Rows.Count > 0)
                {
                    No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                    cntRow = 0;
                    ID = No;
                }
            }
            else if (type == 2)
            {
                if (dtSearch.Rows.Count > 0)
                {
                    No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    cntRow = dtSearch.Rows.Count - 1;
                    ID = No;
                }
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
            if (ID > 0)
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
            label7.Visible = flag;
            label8.Visible = flag;
            txtChqNo.Visible = flag;
            dtpChqDate.Visible = flag;
            lblStar4.Visible = flag;
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
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            ID = 0;
            lblAmountWord.Visible = false;
            setDisplay(false);
            InitControls();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            dtpDate.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            NavigationDisplay(5);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = null;
            if (Vtype == VchType.ExpensesEntry)
                NewF = new ExpensesEntry(VchType.ExpensesEntry, CompanyNo);
            else if (Vtype == VchType.CashReceipt)
                NewF = new ExpensesEntry(VchType.CashReceipt, CompanyNo);
            else if (Vtype == VchType.CashPayment)
                NewF = new ExpensesEntry(VchType.CashPayment, CompanyNo);
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure you want to cancel this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                dbtVaucherEntry = new DBTVaucherEntry();
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;
                dbtVaucherEntry.CancelTVoucherEntry(tVoucherEntry);
                if (Vtype == VchType.ExpensesEntry)
                    OMMessageBox.Show("Expenses Cancelled Successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                else if (Vtype == VchType.CashReceipt)
                    OMMessageBox.Show("Cash Receipt Cancelled Successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                else if (Vtype == VchType.CashPayment)
                    OMMessageBox.Show("Cash Payment Cancelled Successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + Vtype + " and CompanyNo="+CompanyNo+"").Table;
                ID = ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + Vtype + " and CompanyNo="+CompanyNo+"", CommonFunctions.ConStr);
                SetNavigation();
                
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                if (ID > 0)
                    FillControls();
            }
        }

        private void cmbPayType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                long LedNo = ObjFunction.GetComboValue(cmbPayType);
                grpno = ObjQry.ReturnLong("SELECT  GroupNo FROM  MLedger WHERE  (LedgerNo = " + LedNo + ")", CommonFunctions.ConStr);
                if (grpno != GroupType.BankAccounts)
                {
                    setDisplay(false);
                }
                else
                {
                    setDisplay(true);
                }
            }
        }

        private void txtNarration_Leave(object sender, EventArgs e)
        {
            if (txtChqNo.Visible == false)
                BtnSave.Focus();
        }

        private void txtNarration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtNarration_Leave(sender, e);
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNarration.GotFocus += delegate { txtNarration.Select(txtNarration.Text.Length, txtNarration.Text.Length); };
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            if (ObjFunction.CheckValidAmount(txtAmount.Text) == false)
            {
                EP.SetError(txtAmount, "Enter Valid Amount");
                EP.SetIconAlignment(txtAmount, ErrorIconAlignment.MiddleRight);
                txtAmount.Focus();
                lblAmountWord.Visible = false;
            }
            else
            {
                EP.SetError(txtAmount, "");
                lblAmountWord.Text = NumberToWordsIndian.getWords(txtAmount.Text);
                lblAmountWord.Visible = true;
            }
        }

        private void ExpensesEntryAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtAmount, 2, 10,OMFunctions.MaskedType.NotNegative);                       
        }

        private void txtChqNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtChqNo, -1, 10);
        }

        private void ExpensesEntryAE_Shown(object sender, EventArgs e)
        {
            if (CompanyNo == 0)
                this.Close();
        }
    }
}
