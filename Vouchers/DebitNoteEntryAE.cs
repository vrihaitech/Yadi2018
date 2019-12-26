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
    public partial class DebitNoteEntryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbtVaucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        TVoucherChqCreditDetails tVchChqCredit = new TVoucherChqCreditDetails();
        TVoucherRefDetails tVchRefDtls = new TVoucherRefDetails();
        TVoucherEntryCompany tVoucherEntryComp = new TVoucherEntryCompany();
        TVoucherDetailsCompany tVoucherDetailsComp = new TVoucherDetailsCompany();
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long ID, VType = 0, CompanyNo;
        long grpno = 0;
        long MainMfgCompNo = 0;
        public string StrRBillNo = "";
        public DebitNoteEntryAE()
        {
            InitializeComponent();
            VType = VchType.DebitNote;
            this.Text = "Debit Note";
            this.Name = "Debit Note";
        }

        private void ExpensesEntry_Load(object sender, EventArgs e)
        {
            Form NewFrm = new Vouchers.FirmSelection();
            ObjFunction.OpenForm(NewFrm);

            MainMfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
            lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;         
            CompanyNo = DBGetVal.FirmNo;
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            dtpDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            dtpChqDate.Text = "";

            ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 order by MLedger.LedgerName");
            ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.DirectExpenses + "," + GroupType.InDirectExpenses + "," + GroupType.BankAccounts + "," + GroupType.PurchaseAccount + "," + GroupType.AdminExpenses + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo=1 order by MLedger.LedgerName");


            setDisplay(false);
            InitControls();
            dtSearch = ObjFunction.GetDataView("SELECT  PkVoucherNo FROM  TVoucherEntry  WHERE  (VoucherTypeCode = " + VType + ") and CompanyNo=" + CompanyNo + "").Table;
            if (dtSearch.Rows.Count > 0)
            {
                if (DebitNoteEntry.RequestDebitNo == 0)
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                else
                    ID = DebitNoteEntry.RequestDebitNo;
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
            txtVoucherNo.Text = ObjQry.ReturnLong("Select ISNULL(Max(VoucherUserNo),0) + 1 From TVoucherEntry  where vouchertypecode=" + VType + " and CompanyNo=" + CompanyNo + "", CommonFunctions.ConStr).ToString();
            cmbExpType.SelectedIndex = 0;
            cmbPayType.SelectedIndex = 0;
            txtChqNo.Text = "0";
            txtAmount.Text = "";
            txtNarration.Text = "";
            lblPKVoucherNo.Text = "0";
            txtMainBillNo.Text = "";
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

            long ExLedNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherDetails  Where FKVoucherNo=" + ID + " AND VoucherSrNo=1", CommonFunctions.ConStr);
            long LedNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherDetails  Where FKVoucherNo=" + ID + " AND VoucherSrNo=2", CommonFunctions.ConStr);

            ObjFunction.FillCombo(cmbExpType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 OR LedgerNo=" + ExLedNo + " order by MLedger.LedgerName");
            ObjFunction.FillCombo(cmbPayType, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (" + GroupType.DirectExpenses + "," + GroupType.InDirectExpenses + "," + GroupType.BankAccounts + "," + GroupType.PurchaseAccount + "," + GroupType.AdminExpenses + ")) AND (MLedger.IsActive = 'true') AND LedgerNo>15 or LedgerNo in (1," + LedNo + ")  order by MLedger.LedgerName");

            cmbExpType.SelectedValue = ExLedNo;
            cmbPayType.SelectedValue = LedNo;

            grpno = ObjQry.ReturnLong("SELECT  GroupNo FROM  MLedger WHERE  (LedgerNo = " + LedNo + ")", CommonFunctions.ConStr);
            if (grpno != GroupType.BankAccounts)
            {
                setDisplay(false);
                lblPKVoucherNo.Text = "0";
                txtMainBillNo.Text = "";
            }
            else
            {
                setDisplay(true);

                DataTable dt = ObjFunction.GetDataView(" SELECT TVoucherEntry.PkVoucherNo, TVoucherEntry.Reference, TVoucherEntry.VoucherTypeCode " +
                    " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                    " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo " +
                    " WHERE  (TVoucherRefDetails.TypeOfRef = 3)  AND  TVoucherRefDetails.RefNo in   " +
                    "(SELECT TVoucherRefDetails.RefNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                        " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo WHERE " +
                        " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PkVoucherNo = " + ID + "))").Table;
                if (dt.Rows.Count > 0)
                {
                    lblPKVoucherNo.Text = dt.Rows[0].ItemArray[0].ToString();
                    string StrVoucherName = "";
                    int VchCode = Convert.ToInt32(dt.Rows[0].ItemArray[2].ToString());
                    if (VchCode == VchType.Purchase)
                    {
                        StrVoucherName = "Purcchase Bill Raw Material";
                    }
                   
                    
                    txtMainBillNo.Text = "Inv No:" + dt.Rows[0].ItemArray[1].ToString() + " (" + StrVoucherName + ")";
                }
                else
                {
                    lblPKVoucherNo.Text = "0";
                    txtMainBillNo.Text = "";
                }
            }

            long PKVoucherNo = ObjQry.ReturnLong("Select TD.FKVoucherNo From TVoucherDetails TD INNER JOIN TVoucherRefDetails TR ON TD.PKVoucherTrnNo=TR.FKVoucherTrnNo " +
                    " Where TR.TypeOfRef=3  AND TR.RefNo in(Select RefNo From TVoucherDetails INNER JOIN TVoucherRefDetails ON TVoucherDetails.PKVoucherTrnNo=TVoucherRefDetails.FKVoucherTrnNo " +
                    " Where TVOucherDetails.FKVoucherNo=" + ID + ")", CommonFunctions.ConStr);
            if (PKVoucherNo > 0)
            {
                lblPKVoucherNo.Text = PKVoucherNo.ToString();
                DataTable dtDetails = ObjFunction.GetDataView("Select VoucherUserNO,VoucherTypeName From TVoucherEntry INNER JOIN MVoucherType ON TVoucherEntry.VoucherTypeCode=MVoucherType.VoucherTypeCode " +
                    " Where TVoucherEntry.PKVoucherNo=" + PKVoucherNo + "").Table;
                if (dtDetails.Rows.Count > 0)
                {
                    txtMainBillNo.Text = "Bill No:" + dtDetails.Rows[0].ItemArray[0].ToString() + " (" + dtDetails.Rows[0].ItemArray[1].ToString() + ")";
                }
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


                //if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                //{
                //    btnUpdate.Visible = false;
                //}
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
                tVoucherEntry.VoucherTypeCode = VType;// == VchType.DebitNoteEntry ? VchType.DebitNoteEntry : VchType.CashReceipt;
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
                SetVoucherCompany(tVoucherEntry);

                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = ObjQry.ReturnLong("Select PkVoucherTrnNo from TVoucherDetails where SignCode=1 and FkVoucherNo=" + ID, CommonFunctions.ConStr);
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbExpType);
                tVoucherDetails.SignCode = 1;
                tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text.Trim());
                tVoucherDetails.Credit = 0;
                tVoucherDetails.Narration = "";
                tVoucherDetails.CompanyNo = CompanyNo;
                tVoucherDetails.SrNo = Others.Party;
                dbtVaucherEntry.AddTVoucherDetails(tVoucherDetails);
                SetVoucherDetailsCompany(tVoucherDetails);

                if (grpno == GroupType.BankAccounts)
                {
                    tVchChqCredit.PkSrNo = ObjQry.ReturnLong("Select PkSrNo From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "", CommonFunctions.ConStr);
                    tVchChqCredit.ChequeNo = txtChqNo.Text;
                    tVchChqCredit.ChequeDate = tVoucherEntry.ChequeNo == 0 ? Convert.ToDateTime("1/1/1900") : Convert.ToDateTime(dtpChqDate.Text);
                    tVchChqCredit.BankNo = 0;
                    tVchChqCredit.BranchNo = 0;
                    tVchChqCredit.CreditCardNo = "";
                    tVchChqCredit.Amount = Convert.ToDouble(txtAmount.Text.Trim());
                    tVchChqCredit.PostFkVoucherNo = 0;
                    tVchChqCredit.PostFkVoucherTrnNo = 0;
                    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                    dbtVaucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                }

                if (Convert.ToInt64(lblPKVoucherNo.Text) != 0)
                {
                    tVchRefDtls = new TVoucherRefDetails();
                    tVchRefDtls.PkRefTrnNo = ObjQry.ReturnLong("SELECT TVoucherRefDetails.PkRefTrnNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                        " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo WHERE (TVoucherEntry.PkVoucherNo = " + ID + ")", CommonFunctions.ConStr);
                    tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                    tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                    tVchRefDtls.TypeOfRef = 2;
                    tVchRefDtls.RefNo = ObjQry.ReturnLong("SELECT TVoucherRefDetails.RefNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                        " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo WHERE " +
                        " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherRefDetails.TypeOfRef = 3) AND (TVoucherEntry.PkVoucherNo = " + lblPKVoucherNo.Text + ")", CommonFunctions.ConStr);
                    tVchRefDtls.DueDays = 0;
                    tVchRefDtls.DueDate = DBGetVal.ServerTime;
                    tVchRefDtls.Amount = tVoucherEntry.BilledAmount;
                    tVchRefDtls.DiscAmt = 0;
                    tVchRefDtls.SignCode = 1;
                    tVchRefDtls.UserID = DBGetVal.UserID;
                    tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                    tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                    dbtVaucherEntry.AddTVoucherRefDetails(tVchRefDtls);
                }

                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = ObjQry.ReturnLong("Select PkVoucherTrnNo from TVoucherDetails where SignCode=2 and FkVoucherNo=" + ID, CommonFunctions.ConStr);
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1;
                tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPayType);
                tVoucherDetails.SignCode = 2;
                tVoucherDetails.Debit = 0;
                tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text.Trim());
                tVoucherDetails.Narration = "";
                tVoucherDetails.CompanyNo = CompanyNo;
                tVoucherDetails.SrNo = 0;
                dbtVaucherEntry.AddTVoucherDetails(tVoucherDetails);
                SetVoucherDetailsCompany(tVoucherDetails);
                long tempID = dbtVaucherEntry.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    if (ID == 0)
                    {

                        OMMessageBox.Show("Debit Note Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                        dtSearch = ObjFunction.GetDataView("SELECT  PkVoucherNo FROM  TVoucherEntry  WHERE  (VoucherTypeCode = " + VType + ") and CompanyNo=" + CompanyNo + "").Table;
                        ID = ObjQry.ReturnLong("Select Max(PkVoucherNo) From TVoucherEntry where (VoucherTypeCode = " + VType + ") and CompanyNo=" + CompanyNo + "", CommonFunctions.ConStr);
                        SetNavigation();
                        FillControls();

                    }
                    else
                    {
                        OMMessageBox.Show("Debit Note Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }

                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                }
                else
                {
                    OMMessageBox.Show("Debit Note not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Vouchers.DebitNoteEntry.RequestDebitNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }
        private void SetVoucherCompany(TVoucherEntry tVouch)
        {
           
                tVoucherEntryComp = new TVoucherEntryCompany();
                tVoucherEntryComp.PKVoucherCompanyNo = ObjQry.ReturnLong("Select PKVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + tVouch.PkVoucherNo + " AND MfgCompNo=" + MainMfgCompNo + "", CommonFunctions.ConStr);
                tVoucherEntryComp.VoucherTypeCode = tVouch.VoucherTypeCode;
                tVoucherEntryComp.VoucherUserNo = 0;
                tVoucherEntryComp.BilledAmount = tVouch.BilledAmount;//Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.BilledAmount) / 10);
                tVoucherEntryComp.CompanyNo = tVouch.CompanyNo;
                tVoucherEntryComp.MfgCompNo = MainMfgCompNo; //Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                tVoucherEntryComp.PayTypeNo = tVouch.PayTypeNo;
                tVoucherEntryComp.UserID = tVouch.UserID;
                tVoucherEntryComp.UserDate = tVouch.UserDate;
                dbtVaucherEntry.AddTVoucherEntryCompany(tVoucherEntryComp);
            
        }

        private void SetVoucherDetailsCompany(TVoucherDetails tVouch)
        {
           
                tVoucherDetailsComp = new TVoucherDetailsCompany();
                tVoucherDetailsComp.PkVoucherCompTrnNo = ObjQry.ReturnLong("Select PkVoucherCompTrnNo From TVoucherDetailsCompany Where FKVoucherTrnNo=" + tVouch.PkVoucherTrnNo + " AND MfgCompNo=" + MainMfgCompNo + "", CommonFunctions.ConStr);
                tVoucherDetailsComp.VoucherSrNo = tVouch.VoucherSrNo;
                tVoucherDetailsComp.SignCode = tVouch.SignCode;
                tVoucherDetailsComp.LedgerNo = tVouch.LedgerNo;
                if (tVouch.Debit != 0)
                    tVoucherDetailsComp.Debit = tVouch.Debit;//Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Debit) / 10);
                else tVoucherDetailsComp.Debit = 0;
                if (tVouch.Credit != 0)
                    tVoucherDetailsComp.Credit = tVouch.Credit;//Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Credit) / 10);
                else tVoucherDetailsComp.Credit = 0;

                tVoucherDetailsComp.SrNo = tVouch.SrNo;
                tVoucherDetailsComp.CompanyNo = tVouch.CompanyNo;
                tVoucherDetailsComp.Narration = tVouch.Narration;
                tVoucherDetailsComp.MfgCompNo = MainMfgCompNo;//Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                dbtVaucherEntry.AddTVoucherDetailsCompany(tVoucherDetailsComp);
            
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
            else if (txtAmount.Text.Trim() == "")
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
            //if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
            //{
            //    btnUpdate.Visible = false;
            //    OMMessageBox.Show("Already this bill is amount has adjusted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            //    return;
            //}
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
            NewF = new DebitNoteEntry();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure you want to cancel this Debit Note ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                dbtVaucherEntry = new DBTVaucherEntry();
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;
                dbtVaucherEntry.CancelTVoucherEntry(tVoucherEntry);

                OMMessageBox.Show("Debit Note Cancelled Successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VType + " and CompanyNo=" + CompanyNo + "").Table;
                ID = ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + VType + " and CompanyNo=" + CompanyNo + "", CommonFunctions.ConStr);
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
            ObjFunction.SetMasked(txtAmount, 2, 10, OMFunctions.MaskedType.NotNegative);
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

        private void btnSelectBill_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbExpType) > 0)
                {
                    if (BtnSave.Visible == false) return;
                    Form NewF = new Vouchers.PurchaseBillSelection(ObjFunction.GetComboValue(cmbExpType));
                    ObjFunction.OpenForm(NewF);

                    if (((PurchaseBillSelection)NewF).DS == DialogResult.OK)
                    {
                        lblPKVoucherNo.Text = Convert.ToString(((PurchaseBillSelection)NewF).PKVoucherNo); //((PurchaseBillSelection)NewF).PKVoucherNo).ToString();
                        txtMainBillNo.Text = "Inv No:" + ((PurchaseBillSelection)NewF).VoucherUserNo + " (" + ((PurchaseBillSelection)NewF).StrVoucherName + ")";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DialogResult ds = OMMessageBox.Show("Are you sure you want to Print Debit Note ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
            if (ds == DialogResult.Yes)
            {
                PrintBill(ID.ToString(), 0);
            }
            else if (ds == DialogResult.Cancel)
            {
                PrintBill(ID.ToString(), 1);
            }
        }

        public void PrintBill(string PkVoucherNo, int PType)
        {
            string[] ReportSession;
            DataTable dtPrint = ObjFunction.GetDataView("SELECT VoucherDate, TVoucherEntryCompany.BilledAmount, Remark,IsNull((Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=501 And FkVoucherNo=PkVoucherNo),0) as PartyAmount ,IsNull( (Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=502 And FkVoucherNo=PkVoucherNo),0) as DiscAmt,BankName,TVoucherChqCreditDetails.ChequeNo ,TVoucherEntryCompany.voucheruserno" +
                               " FROM TVoucherEntry INNER JOIN TVoucherEntryCompany on TVoucherEntryCompany.FkVoucherNo=TVoucherEntry.PkVoucherNo  " +
                               " Left Outer Join TVoucherChqCreditDetails on TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FkVoucherNo " +
                               " Left Join MOtherBank on MOtherBank.bankNo = TVoucherChqCreditDetails.bankNo Where PkVoucherNo In(" + PkVoucherNo + ") ").Table;
 
            for (int i = 0; i < dtPrint.Rows.Count; i++)
            {
                //ReportSession = new string[6];
                //ReportSession[0] = "Debit Note";
                //ReportSession[1] = dtPrint.Rows[i].ItemArray[0].ToString();
                //ReportSession[2] = (Convert.ToDouble(dtPrint.Rows[i].ItemArray[3].ToString()) - Convert.ToDouble(dtPrint.Rows[i].ItemArray[4].ToString())).ToString();
                //ReportSession[3] = NumberToWordsIndian.getWords(ReportSession[2].ToString());
                //ReportSession[4] = PkVoucherNo;
                //ReportSession[5] = DBGetVal.CompanyAddress;
              //  string a ="12";
                ReportSession = new string[11];
                ReportSession[0] = lblFirmName.Text.Replace("Firm Name :", "");
                ReportSession[1] = ((VType == 15) ? "Credit Note " : "Debit Note");
                ReportSession[2] = dtPrint.Rows[i].ItemArray[0].ToString();
                ReportSession[3] = (Convert.ToDouble(dtPrint.Rows[i].ItemArray[3].ToString()) - Convert.ToDouble(dtPrint.Rows[i].ItemArray[4].ToString())).ToString();
                ReportSession[4] = NumberToWordsIndian.getWords(ReportSession[3].ToString());
                ReportSession[5] = dtPrint.Rows[i].ItemArray[2].ToString();
                ReportSession[6] = cmbExpType.Text;
                ReportSession[7] = dtPrint.Rows[i].ItemArray[7].ToString();
                ReportSession[8] = dtPrint.Rows[i].ItemArray[4].ToString();
                ReportSession[9] = dtPrint.Rows[i].ItemArray[5].ToString();
                ReportSession[10] = dtPrint.Rows[i].ItemArray[6].ToString();


                if (PType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    childForm = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.GetCollectionPrint");
                    else
                        childForm = ObjFunction.LoadReportObject("GetCollectionPrint.rpt", CommonFunctions.ReportPath);
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            DisplayMessage("Debit note Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("Debit Note not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
                else
                {
                    Form NewF = null;

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetCollectionPrint(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetCollectionPrint.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
        }


        private void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }
    }
}
