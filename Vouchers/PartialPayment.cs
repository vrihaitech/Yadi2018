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
    public partial class PartialPayment : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        TVoucherRefDetails tVchRefDtls = new TVoucherRefDetails();
        TVoucherPayTypeDetails tVchPayTypeDetails = new TVoucherPayTypeDetails();
        TVoucherChqCreditDetails tVchChqCredit = new TVoucherChqCreditDetails();
        TStock tStock = new TStock();
        TStockGodown tStockGodown = new TStockGodown();
        TVoucherEntryCompany tVoucherEntryComp = new TVoucherEntryCompany();
        TVoucherDetailsCompany tVoucherDetailsComp = new TVoucherDetailsCompany();
        public DialogResult DS = DialogResult.Cancel;
        DataTable dtDelete = new DataTable();
        long ID=0,PayType=1,PartyNo=0;
        DataTable dtPayLedger = new DataTable();
        double GrandTotal = 0;
        DataTable dtChqDtls = new DataTable();
        DataTable dtCrDtls = new DataTable();
        long TempPayTypeNo = 0,TPayTypeNo=0;

        public DataTable dtCompRatio = new DataTable();
        public long MfgCompNo = 0;

        public PartialPayment()
        {
            InitializeComponent();
        }

        public PartialPayment(long id,double Amt,long paytype,long partyno)
        {
            InitializeComponent();
            ID = id;
            GrandTotal = Amt;
            PayType = paytype;
            PartyNo = partyno;
        }

        private void PartialPayment_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillList(lstBank, "Select BankNo,BankName From MBank where IsActive='true' order by BankName");
                ObjFunction.FillList(lstBranch, "Select BranchNo,BranchName From MBranch where IsActive='true' order by BranchName");
                InitDelTable();
                BindGridPayType(ID);
                InitCheDtls();
                label2.ForeColor = Color.Green;
                lblRecAmt.ForeColor = Color.Green;
                label4.ForeColor = Color.Orange;
                lblPayTypeChrgAmt.ForeColor = Color.Orange;
                label3.ForeColor = Color.Red;
                lblPayTypeBal.ForeColor = Color.Red;
                dgPayType.CurrentCell = dgPayType[2, 1];
                dgPayType.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void InitCheDtls()
        {
            for (int i = 0; i < 10; i++)
            {
                dtChqDtls.Columns.Add();
            }
            for (int i = 0; i < 9; i++)
            {
                dtCrDtls.Columns.Add();
            }

        }

        public void BindGridPayType(long ID)
        {
            try
            {
                DataTable dtPayType = new DataTable();
               // dtPayLedger = ObjFunction.GetDataView("Select * From MPayTypeLedger where PayTypeNo in(2,4,5)").Table;
                dtPayLedger = ObjFunction.GetDataView("Select * From MPayTypeLedger where PayTypeNo in(Select PkPayTypeNo from MPayType where ControlUnder in(2,4,5))").Table;
                string sqlQuery = "";
                if (ID == 0)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPayTypeChargesCalculation)) == true)
                        sqlQuery = "SELECT PayTypeName, PKPayTypeNo, Cast(0.00 as varchar) AS Amount, 0 AS LedgerNo, 0 AS PKVoucherPayTypeNo,ControlUnder,(Select ChargesPerce FRom MPayTypeLedger Where PayTypeNo=PKPayTypeNo) As ChrgPerce,0.00 As ChrgAmt FROM MPayType   ORDER BY PKPayTypeNo";//Where ControlUnder in(2,4,5)
                    else
                        sqlQuery = "SELECT PayTypeName, PKPayTypeNo, Cast(0.00 as varchar) AS Amount, 0 AS LedgerNo, 0 AS PKVoucherPayTypeNo,ControlUnder,0.00 As ChrgPerce,0.00 As ChrgAmt FROM MPayType   ORDER BY PKPayTypeNo";
                }
                else
                    sqlQuery = "SELECT PayTypeName, PKPayTypeNo,Cast( IsNull((SELECT SUM(Amount) FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND ControlUnder in(2,4,5) AND (FKPayTypeNo = PKPayTypeNo)),0) AS varchar) AS Amount, 0 AS LedgerNo, 0 AS PKVoucherPayTypeNo ,ControlUnder,IsNull((SELECT ChargesPerce FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) As ChrgPerce,IsNull((SELECT ChargesAmount FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) As ChrgAmt FROM MPayType  ORDER BY PKPayTypeNo";
                dtPayType = ObjFunction.GetDataView(sqlQuery).Table;
                while (dgPayType.Columns.Count > 0)
                    dgPayType.Columns.RemoveAt(0);
                dgPayType.DataSource = dtPayType.DefaultView;
                for (int i = 0; i < dgPayType.Columns.Count; i++)
                    dgPayType.Columns[i].Visible = false;
                dgPayType.Columns[0].Visible = true;
                dgPayType.Columns[2].Visible = true;
                dgPayType.Rows[0].Visible = false;
                dgPayType.Rows[2].Visible = false;
                dgPayType.Columns[0].Width = 120;
                dgPayType.Columns[2].Width = 100;
                dgPayType.Columns[0].ReadOnly = true;
                dgPayType.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPayType.Rows[0].Visible = false;
                dgPayType.Columns[6].Visible = true;
                dgPayType.Columns[7].Visible = true;
                dgPayType.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPayType.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPayType.Columns[6].Width = 40;
                dgPayType.Columns[7].Width = 75;
                if (ID != 0)
                {
                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + PayType + "", CommonFunctions.ConStr);
                    //manali
                    if (ControlUnder == 4 || ControlUnder == 5)
                    {
                        for (int i = 0; i < dgPayType.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dgPayType.Rows[i].Cells[1].Value.ToString()) == ControlUnder)
                            {
                                if(ControlUnder == 4)
                                    dgPayType.Rows[i].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND ChequeNo <>''", CommonFunctions.ConStr).ToString("0.00");
                                else if (ControlUnder == 5)
                                    dgPayType.Rows[i].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND CreditCardNo <>''", CommonFunctions.ConStr).ToString("0.00");
                            }
                        }
                    }

                    //if (PayType == 1 || PayType == 4 || PayType == 5)
                    //{
                    //    dgPayType.Rows[3].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND ChequeNo <>''", CommonFunctions.ConStr).ToString("0.00");
                    //    dgPayType.Rows[4].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND CreditCardNo <>''", CommonFunctions.ConStr).ToString("0.00");
                    //}

                    double RefAmt = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherRefDetails Where FKVucherTrnNo in (Select PKVoucherTrnNo From TVoucherDetails Where FkVoucherNo=" + ID + ")", CommonFunctions.ConStr);
                    if (RefAmt > 0)
                    {
                        dgPayType.Rows[2].Cells[2].Value = RefAmt;
                    }
                }
                CaluculatePayType();
                pnlPartial.Visible = true;
                dgPayType.CurrentCell = dgPayType[2, 1];
                dgPayType.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CaluculatePayType()
        {
            try
            {
                double TotAmt = 0, ChrgAmt = 0;
                for (int i = 0; i < dgPayType.Rows.Count; i++)
                {
                    if (dgPayType.Rows[i].Cells[2].Value == null) dgPayType.Rows[i].Cells[2].Value = "0";
                    TotAmt += Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value);
                    if (dgPayType.Rows[i].Cells[7].Value == null) dgPayType.Rows[i].Cells[7].Value = "0";
                    ChrgAmt += Convert.ToDouble(dgPayType.Rows[i].Cells[7].Value);
                }
                txtTotalAmt.Text = TotAmt.ToString("0.00");
                lblPayTypeChrgAmt.Text = ChrgAmt.ToString("0.00");
                if (GrandTotal != 0)
                {
                    lblBillAmt.Text = GrandTotal.ToString("0.00");
                    lblRecAmt.Text = TotAmt.ToString("0.00");
                    lblPayTypeBal.Text = (GrandTotal - TotAmt + ChrgAmt).ToString("0.00");
                }
                else
                {
                    lblBillAmt.Text = GrandTotal.ToString("0.00");
                    lblRecAmt.Text = "0.00";
                    lblPayTypeBal.Text = "0.00";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(lblPayTypeBal.Text) > 0)
                {
                    if (PartyNo == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))
                    {
                        OMMessageBox.Show("Bill Amount does not match...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        if (OMMessageBox.Show(" Amount due is Rs. " + lblPayTypeBal.Text + "\n against the total bill amount of Rs. " + lblBillAmt.Text + " \n Do you want to adjust in Credit ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.No)
                            return;
                    }
                }
                bool flag = true;//, tempFlag = false;
                if (Convert.ToDouble(txtTotalAmt.Text) <= 0)
                {
                    OMMessageBox.Show("Please fill details. Zero Amount not allowed..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                for (int i = 0; i < dgPayType.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value) > 0)
                    {
                        if (dgPayType.Rows[i].Cells[5].Value.ToString() == "4")
                        {
                            if (dgPayChqDetails.Rows[0].Cells[0].Value == null || dgPayChqDetails.Rows[0].Cells[1].FormattedValue.ToString() == "" || dgPayChqDetails.Rows[0].Cells[2].FormattedValue.ToString() == "" || dgPayChqDetails.Rows[0].Cells[3].FormattedValue.ToString() == "")
                            {
                                OMMessageBox.Show("Please Fill Cheque Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                flag = false;
                            }
                            else flag = true;
                        }
                    }
                    if (flag == true)
                    {
                        if (Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value) > 0)
                        {
                            if (dgPayType.Rows[i].Cells[5].Value.ToString() == "5")
                            {
                                if (dgPayCreditCardDetails.Rows[0].Cells[0].FormattedValue.ToString() == "" || dgPayCreditCardDetails.Rows[0].Cells[1].FormattedValue.ToString() == "" || dgPayCreditCardDetails.Rows[0].Cells[2].FormattedValue.ToString() == "")
                                {
                                    OMMessageBox.Show("Please Fill Credit Card Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                    flag = false;
                                }
                                else flag = true;
                            }
                        }
                    }
                }
                if (flag == true)
                {
                    if (Convert.ToDouble(lblPayTypeBal.Text) < 0)
                    {
                        OMMessageBox.Show("TOTAL AMOUNT EXCEEDS BILL AMOUNT.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        flag = false;
                    }
                    else
                    {
                        //btnSave.Enabled = true;
                        //btnSave.Focus();
                        flag = true;
                        //pnlPartial.Visible = false;
                    }
                }
                if (flag == true)
                {           
                    
                    DS = DialogResult.OK;
                    this.Close();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool SaveData(long Pk)
        {

            bool tempFlag = false;
            ID = Pk;
            for (int i = 0; i < dgPayType.Rows.Count; i++)
            {
                if (Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value) != 0)
                {
                    dbTVoucherEntry = new DBTVaucherEntry();
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = 0;
                    tVoucherEntry.VoucherTypeCode = VchType.SalesReceipt;
                    tVoucherEntry.VoucherUserNo = 0;
                    tVoucherEntry.VoucherDate = DBGetVal.ServerTime.Date;
                    tVoucherEntry.VoucherTime = Convert.ToDateTime("01-Jan-1900");
                    tVoucherEntry.Reference = "";
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value);
                    tVoucherEntry.ChallanNo = "";
                    tVoucherEntry.Remark = "";
                    tVoucherEntry.ChequeNo = 0;
                    tVoucherEntry.ClearingDate = Convert.ToDateTime("01-Jan-1900");
                    tVoucherEntry.Narration = "";
                    tVoucherEntry.UserID = DBGetVal.UserID;
                    tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                    tVoucherEntry.OrderType = 1;
                    tVoucherEntry.PayTypeNo = Convert.ToInt64(dgPayType.Rows[i].Cells[1].Value);
                    tVoucherEntry.TransporterCode = 0;
                    tVoucherEntry.TransPayType = 0;
                    tVoucherEntry.LRNo = "";
                    tVoucherEntry.TransportMode = 0;
                    tVoucherEntry.TransNoOfItems = 0;
                    dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); SetVoucherCompany(tVoucherEntry);

                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = 0;
                    tVoucherDetails.VoucherSrNo = 1;
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = PartyNo;
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value);
                    tVoucherDetails.SrNo = Others.Party;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.Narration = "";
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);

                    long RefNo = ObjQry.ReturnLong(" SELECT TVoucherRefDetails.RefNo FROM TVoucherEntry INNER JOIN " +
                                                   " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                                                " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo where TVoucherEntry.PkVoucherNo=" + ID + "", CommonFunctions.ConStr);
                    tVchRefDtls = new TVoucherRefDetails();
                    tVchRefDtls.PkRefTrnNo = 0;
                    tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                    tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                    tVchRefDtls.TypeOfRef = 2;
                    tVchRefDtls.RefNo = RefNo;
                    tVchRefDtls.DueDays = 0;
                    tVchRefDtls.DueDate = DBGetVal.ServerTime;
                    tVchRefDtls.Amount = tVoucherEntry.BilledAmount;
                    tVchRefDtls.SignCode = 2;
                    tVchRefDtls.UserID = DBGetVal.UserID;
                    tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                    tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                    if (Convert.ToInt64(dgPayType.Rows[i].Cells[5].Value) == 4)
                    {
                        DataRow[] dr = dtChqDtls.Select("Column9=" + Convert.ToInt64(dgPayType.Rows[i].Cells[1].Value) + "");
                        for (int row = 0; row < dr.Length; row++)
                        {
                            if (dr[row][0].ToString() != null && dr[row][0].ToString() != "")
                            {
                                tVchChqCredit.PkSrNo = 0;//Convert.ToInt64(dgPayChqDetails.Rows[row].Cells[5].Value);
                                tVchChqCredit.ChequeNo = dr[row][0].ToString();//dgPayChqDetails.Rows[row].Cells[0].Value.ToString();
                                tVchChqCredit.ChequeDate = (dr[row][1] == null) ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(dr[row][1].ToString());
                                tVchChqCredit.BankNo = Convert.ToInt64(dr[row][6].ToString());
                                tVchChqCredit.BranchNo = Convert.ToInt64(dr[row][7].ToString());
                                tVchChqCredit.CreditCardNo = "";
                                tVchChqCredit.Amount = Convert.ToDouble(dr[row][4].ToString());
                                tVchChqCredit.PostFkVoucherNo = 0;
                                tVchChqCredit.PostFkVoucherTrnNo = 0;
                                tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                            }
                        }
                        //for (int row = 0; row < dgPayChqDetails.Rows.Count; row++)
                        //{
                        //    if (dgPayChqDetails.Rows[row].Cells[0].Value != null && dgPayChqDetails.Rows[row].Cells[0].Value.ToString() != "")
                        //    {
                        //        tVchChqCredit.PkSrNo = Convert.ToInt64(dgPayChqDetails.Rows[row].Cells[5].Value);
                        //        tVchChqCredit.ChequeNo = dgPayChqDetails.Rows[row].Cells[0].Value.ToString();
                        //        tVchChqCredit.ChequeDate = (dgPayChqDetails.Rows[row].Cells[1].Value == null) ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(dgPayChqDetails.Rows[row].Cells[1].Value);
                        //        tVchChqCredit.BankNo = Convert.ToInt64(dgPayChqDetails.Rows[row].Cells[6].Value);
                        //        tVchChqCredit.BranchNo = Convert.ToInt64(dgPayChqDetails.Rows[row].Cells[7].Value);
                        //        tVchChqCredit.CreditCardNo = "";
                        //        tVchChqCredit.Amount = Convert.ToDouble(dgPayChqDetails.Rows[row].Cells[4].Value);
                        //        tVchChqCredit.PostFkVoucherNo = 0;
                        //        tVchChqCredit.PostFkVoucherTrnNo = 0;
                        //        tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                        //        dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                        //    }
                        //}
                    }

                    if (Convert.ToInt64(dgPayType.Rows[i].Cells[5].Value) == 5)
                    {
                        DataRow[] dr = dtCrDtls.Select("Column8=" + Convert.ToInt64(dgPayType.Rows[i].Cells[1].Value) + "");
                        for (int row = 0; row < dr.Length; row++)
                        {
                            if (dr[row][0].ToString() != null && dr[row][0].ToString() != "")
                            {
                                tVchChqCredit.PkSrNo = 0;//Convert.ToInt64(dgPayCreditCardDetails.Rows[row].Cells[4].Value);
                                tVchChqCredit.CreditCardNo = dr[row][0].ToString();//dgPayCreditCardDetails.Rows[row].Cells[0].Value.ToString();
                                tVchChqCredit.ChequeDate = Convert.ToDateTime("01-Jan-1900");
                                tVchChqCredit.BankNo = Convert.ToInt64(dr[row][5].ToString());
                                tVchChqCredit.BranchNo = Convert.ToInt64(dr[row][6].ToString());
                                tVchChqCredit.ChequeNo = "";
                                tVchChqCredit.Amount = Convert.ToDouble(dr[row][3].ToString());
                                tVchChqCredit.PostFkVoucherNo = 0;
                                tVchChqCredit.PostFkVoucherTrnNo = 0;
                                tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                            }
                        }
                        //for (int row = 0; row < dgPayCreditCardDetails.Rows.Count; row++)
                        //{
                        //    if (dgPayCreditCardDetails.Rows[row].Cells[0].Value != null && dgPayCreditCardDetails.Rows[row].Cells[0].Value.ToString() != "")
                        //    {
                        //        tVchChqCredit.PkSrNo = Convert.ToInt64(dgPayCreditCardDetails.Rows[row].Cells[4].Value);
                        //        tVchChqCredit.CreditCardNo = dgPayCreditCardDetails.Rows[row].Cells[0].Value.ToString();
                        //        tVchChqCredit.ChequeDate = Convert.ToDateTime("01-Jan-1900");
                        //        tVchChqCredit.BankNo = Convert.ToInt64(dgPayCreditCardDetails.Rows[row].Cells[5].Value);
                        //        tVchChqCredit.BranchNo = Convert.ToInt64(dgPayCreditCardDetails.Rows[row].Cells[6].Value);
                        //        tVchChqCredit.ChequeNo = "";
                        //        tVchChqCredit.Amount = Convert.ToDouble(dgPayCreditCardDetails.Rows[row].Cells[3].Value);
                        //        tVchChqCredit.PostFkVoucherNo = 0;
                        //        tVchChqCredit.PostFkVoucherTrnNo = 0;
                        //        tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                        //        dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                        //    }
                        //}
                    }
                    long LedgNo = ObjQry.ReturnLong(" SELECT MPayTypeLedger.LedgerNo FROM MPayType INNER JOIN " +
                                                    " MPayTypeLedger ON MPayType.PKPayTypeNo = MPayTypeLedger.PayTypeNo where MPayType.PKPayTypeNo =" + Convert.ToInt64(dgPayType.Rows[i].Cells[1].Value) + "", CommonFunctions.ConStr);

                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = 0;
                    tVoucherDetails.VoucherSrNo = 2;
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = LedgNo;
                    tVoucherDetails.Debit = Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.SrNo = 0;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.Narration = "";
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);

                    tVchPayTypeDetails = new TVoucherPayTypeDetails();
                    tVchPayTypeDetails.PKVoucherPayTypeNo = 0;
                    tVchPayTypeDetails.FKSalesVoucherNo = ID;
                    tVchPayTypeDetails.FKPayTypeNo = Convert.ToInt64(dgPayType.Rows[i].Cells[1].Value);
                    tVchPayTypeDetails.Amount = Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value);
                    tVchPayTypeDetails.ChargesPerce = Convert.ToDouble(dgPayType.Rows[i].Cells[6].Value);
                    tVchPayTypeDetails.ChargesAmount = Convert.ToDouble(dgPayType.Rows[i].Cells[7].Value);
                    tVchPayTypeDetails.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTVoucherPayTypeDetails(tVchPayTypeDetails);

                    long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                    if (tempId != 0)
                    {
                        tempFlag = true;
                    }
                    else
                        tempFlag = false;

                    //if (tempFlag == true)
                    //{
                    //    OMMessageBox.Show("Data Saved Successfuly", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                    //}
                    //else
                    //{
                    //    OMMessageBox.Show("Data Not Saved Successfuly", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //}
                }
            }
            return tempFlag;
        }

        private void SetVoucherCompany(TVoucherEntry tVouch)
        {
            if (MfgCompNo == 0)
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)))
                {
                    for (int j = 0; j < dtCompRatio.Rows.Count; j++)
                    {
                        tVoucherEntryComp = new TVoucherEntryCompany();
                        tVoucherEntryComp.PKVoucherCompanyNo = ObjQry.ReturnLong("Select PKVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + tVouch.PkVoucherNo + " AND MfgCompNo=" + dtCompRatio.Rows[j].ItemArray[1].ToString() + "", CommonFunctions.ConStr);
                        tVoucherEntryComp.VoucherTypeCode = tVouch.VoucherTypeCode;
                        tVoucherEntryComp.VoucherUserNo = 0;
                        tVoucherEntryComp.BilledAmount = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.BilledAmount) / 10);
                        tVoucherEntryComp.CompanyNo = tVouch.CompanyNo;
                        tVoucherEntryComp.MfgCompNo = Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                        tVoucherEntryComp.PayTypeNo = tVouch.PayTypeNo;
                        tVoucherEntryComp.UserID = tVouch.UserID;
                        tVoucherEntryComp.UserDate = tVouch.UserDate;
                        dbTVoucherEntry.AddTVoucherEntryCompany(tVoucherEntryComp);
                    }
                }
            }
            else
            {
                for (int j = 0; j < dtCompRatio.Rows.Count; j++)
                {
                    tVoucherEntryComp = new TVoucherEntryCompany();
                    tVoucherEntryComp.PKVoucherCompanyNo = ObjQry.ReturnLong("Select PKVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + tVouch.PkVoucherNo + " AND MfgCompNo=" + dtCompRatio.Rows[j].ItemArray[1].ToString() + "", CommonFunctions.ConStr);
                    tVoucherEntryComp.VoucherTypeCode = tVouch.VoucherTypeCode;
                    tVoucherEntryComp.VoucherUserNo = 0;
                    tVoucherEntryComp.BilledAmount = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.BilledAmount) / 10);
                    tVoucherEntryComp.CompanyNo = tVouch.CompanyNo;
                    tVoucherEntryComp.MfgCompNo = Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                    tVoucherEntryComp.PayTypeNo = tVouch.PayTypeNo;
                    tVoucherEntryComp.UserID = tVouch.UserID;
                    tVoucherEntryComp.UserDate = tVouch.UserDate;
                    dbTVoucherEntry.AddTVoucherEntryCompany(tVoucherEntryComp);
                }
            }
        }

        private void SetVoucherDetailsCompany(TVoucherDetails tVouch)
        {
            if (MfgCompNo == 0)
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)))
                {
                    for (int j = 0; j < dtCompRatio.Rows.Count; j++)
                    {
                        tVoucherDetailsComp = new TVoucherDetailsCompany();
                        tVoucherDetailsComp.PkVoucherCompTrnNo = ObjQry.ReturnLong("Select PkVoucherCompTrnNo From TVoucherDetailsCompany Where FKVoucherTrnNo=" + tVouch.PkVoucherTrnNo + " AND MfgCompNo=" + dtCompRatio.Rows[j].ItemArray[1].ToString() + "", CommonFunctions.ConStr);
                        tVoucherDetailsComp.VoucherSrNo = tVouch.VoucherSrNo;
                        tVoucherDetailsComp.SignCode = tVouch.SignCode;
                        tVoucherDetailsComp.LedgerNo = tVouch.LedgerNo;
                        if (tVouch.Debit != 0)
                            tVoucherDetailsComp.Debit = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Debit) / 10);
                        else tVoucherDetailsComp.Debit = 0;
                        if (tVouch.Credit != 0)
                            tVoucherDetailsComp.Credit = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Credit) / 10);
                        else tVoucherDetailsComp.Credit = 0;

                        tVoucherDetailsComp.SrNo = tVouch.SrNo;
                        tVoucherDetailsComp.CompanyNo = tVouch.CompanyNo;
                        tVoucherDetailsComp.Narration = tVouch.Narration;
                        tVoucherDetailsComp.MfgCompNo = Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsCompany(tVoucherDetailsComp);
                    }
                }
            }
            else
            {
                for (int j = 0; j < dtCompRatio.Rows.Count; j++)
                {
                    tVoucherDetailsComp = new TVoucherDetailsCompany();
                    tVoucherDetailsComp.PkVoucherCompTrnNo = ObjQry.ReturnLong("Select PkVoucherCompTrnNo From TVoucherDetailsCompany Where FKVoucherTrnNo=" + tVouch.PkVoucherTrnNo + " AND MfgCompNo=" + dtCompRatio.Rows[j].ItemArray[1].ToString() + "", CommonFunctions.ConStr);
                    tVoucherDetailsComp.VoucherSrNo = tVouch.VoucherSrNo;
                    tVoucherDetailsComp.SignCode = tVouch.SignCode;
                    tVoucherDetailsComp.LedgerNo = tVouch.LedgerNo;
                    if (tVouch.Debit != 0)
                        tVoucherDetailsComp.Debit = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Debit) / 10);
                    else tVoucherDetailsComp.Debit = 0;
                    if (tVouch.Credit != 0)
                        tVoucherDetailsComp.Credit = Convert.ToDouble((Convert.ToDouble(dtCompRatio.Rows[j].ItemArray[2].ToString()) * tVouch.Credit) / 10);
                    else tVoucherDetailsComp.Credit = 0;

                    tVoucherDetailsComp.SrNo = tVouch.SrNo;
                    tVoucherDetailsComp.CompanyNo = tVouch.CompanyNo;
                    tVoucherDetailsComp.Narration = tVouch.Narration;
                    tVoucherDetailsComp.MfgCompNo = Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[1].ToString());
                    dbTVoucherEntry.AddTVoucherDetailsCompany(tVoucherDetailsComp);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtTotalAmt.Text) == 0)
            {
                DS = DialogResult.Cancel;
                //if (PartyNo == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))
                //{
                //    OMMessageBox.Show("Bill Amount does not match...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                //    return;
                //}
                    this.Close();
            }
            else
            {
                OMMessageBox.Show("Amount Should Be Zero", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }
        }

        private void dgPayType_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    btnOk.Enabled = true;
                    if (dgPayType.CurrentCell.Value == null) dgPayType.CurrentCell.Value = "0";
                    if (ObjFunction.CheckValidAmount(dgPayType.CurrentCell.Value.ToString()) == false)
                    {
                        dgPayType.CurrentCell.Value = "0.00";
                        OMMessageBox.Show("Please Enter valid amount..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgPayType.CurrentCell.RowIndex, 2, dgPayType });
                    }
                    else
                    {
                        dgPayType.CurrentCell.ErrorText = "";
                        dgPayType.CurrentCell.Value = Convert.ToDouble(dgPayType.CurrentCell.Value).ToString("0.00");
                        dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[7].Value = Convert.ToDouble((Convert.ToDouble(dgPayType.CurrentCell.Value.ToString()) * Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[6].Value)) / 100).ToString("0.00");
                        CaluculatePayType();

                        if (dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[5].Value.ToString()=="4")//dgPayType.CurrentCell.RowIndex == 3
                        {
                            if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) > 0)
                                btnOk.Enabled = false;
                            else
                                btnOk.Enabled = true;
                            if (TempPayTypeNo != Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value))
                            {
                                dgPayChqDetails.Rows.Clear();
                                dgPayChqDetails.Rows.Add();
                            }
                            if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) <= 0 )
                            {
                                for (int i = 0; i < dtChqDtls.Rows.Count; i++)
                                {
                                    if (Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value) == Convert.ToInt64(dtChqDtls.Rows[i].ItemArray[8].ToString()))
                                        dtChqDtls.Rows.RemoveAt(i);

                                }
                                    dgPayChqDetails.Rows.Clear();
                                    dgPayChqDetails.Rows.Add();
                            }
                           

                            else
                            {
                                dgPayChqDetails.Rows.Clear();
                                dgPayChqDetails.Rows.Add();
                                for (int i = 0; i < dtChqDtls.Rows.Count; i++)
                                {
                                    if (dtChqDtls.Rows[i].ItemArray[8].ToString() == dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value.ToString())
                                    {
                                        for (int j = 0; j < dgPayChqDetails.Columns.Count; j++)
                                        {
                                            dgPayChqDetails.Rows[0].Cells[j].Value = dtChqDtls.Rows[i].ItemArray[j];
                                        }
                                    }
                                }

                                    TempPayTypeNo = Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value);
                                    pnlPartial.Size = new Size(866, 346);


                                //pnlPartial.Location = new Point(20, 123);
                                dgPayChqDetails.Location = new Point(392, 3);
                                dgPayChqDetails.Visible = true;
                                dgPayChqDetails.BringToFront();
                                dgPayChqDetails.Focus();
                                dgPayCreditCardDetails.Visible = false;

                                if (dgPayChqDetails.Rows.Count == 0)
                                {
                                    //dgPayType.CurrentCell = dgPayType[2, dgPayType.CurrentCell.RowIndex];
                                    //dgPayChqDetails.Rows.Add();
                                    //dgPayChqDetails.CurrentCell = dgPayChqDetails[0, 0];
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { dgPayType.CurrentCell.RowIndex, 2, dgPayType });
                                    dgPayChqDetails.Rows.Add();
                                    //move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { 0, 0, dgPayChqDetails });
                                }
                                else
                                {
                                    dgPayChqDetails.Rows[0].Cells[4].Value = dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value;
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { dgPayType.CurrentCell.RowIndex, 2, dgPayType });
                                }
                            }
                        }
                        if (dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[5].Value.ToString() == "5")
                        {
                            if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) > 0)
                                btnOk.Enabled = false;
                            else
                                btnOk.Enabled = true;
                            if (TPayTypeNo != Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value))
                            {
                                dgPayCreditCardDetails.Rows.Clear();
                                dgPayCreditCardDetails.Rows.Add();
                            }
                            if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) <= 0)
                            {
                                for (int i = 0; i < dtCrDtls.Rows.Count; i++)
                                {
                                    if (Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value) == Convert.ToInt64(dtCrDtls.Rows[i].ItemArray[8].ToString()))
                                        dtCrDtls.Rows.RemoveAt(i);

                                }
                                dgPayCreditCardDetails.Rows.Clear();
                                dgPayCreditCardDetails.Rows.Add();
                            }
                            else
                            {
                                dgPayCreditCardDetails.Rows.Clear();
                                dgPayCreditCardDetails.Rows.Add();
                                for (int i = 0; i < dtCrDtls.Rows.Count; i++)
                                {
                                    if (dtCrDtls.Rows[i].ItemArray[7].ToString() == dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value.ToString())
                                    {
                                        for (int j = 0; j < dgPayCreditCardDetails.Columns.Count; j++)
                                        {
                                            dgPayCreditCardDetails.Rows[0].Cells[j].Value = dtCrDtls.Rows[i].ItemArray[j];
                                        }
                                    }
                                }

                                TPayTypeNo = Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value);

                                pnlPartial.Size = new Size(866, 346);

                                //pnlPartial.Location = new Point(20, 123);
                                dgPayCreditCardDetails.Location = new Point(392, 3);
                                dgPayCreditCardDetails.Visible = true;
                                dgPayCreditCardDetails.Focus();
                                dgPayCreditCardDetails.BringToFront();
                                dgPayChqDetails.Visible = false;
                                dgPayType.CurrentCell = dgPayType[2, dgPayType.CurrentCell.RowIndex];
                                if (dgPayCreditCardDetails.Rows.Count == 0)
                                {
                                    //dgPayCreditCardDetails.Rows.Add();
                                    //dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[0, 0];
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { dgPayType.CurrentCell.RowIndex, 2, dgPayType });
                                    dgPayCreditCardDetails.Rows.Add();
                                    //move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { 0, 0, dgPayCreditCardDetails });
                                }
                                else
                                {
                                    dgPayCreditCardDetails.Rows[0].Cells[3].Value = dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value;
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { dgPayType.CurrentCell.RowIndex, 2, dgPayType });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayType_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgPayType.CurrentCell.ColumnIndex == 2)
            {
                TextBox txt = (TextBox)e.Control;
                txt.KeyDown += new KeyEventHandler(txtAmt_KeyDown);
                txt.TextChanged+=new EventHandler(txtAmt_TextChanged);
            }
        }

        public void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (ObjFunction.CheckValidAmount(dgPayType.CurrentCell.Value.ToString()) == false)
                {
                    OMMessageBox.Show("Please Enter valid amount..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    dgPayType.CurrentCell = dgPayType[2, dgPayType.CurrentCell.RowIndex];
                }
            }
        }

        public void txtAmt_TextChanged(object sender, EventArgs e)
        {
            if (dgPayType.CurrentCell.ColumnIndex == 2)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void dgPayType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Escape)
                //{
                //    btnOk_Click(sender, new EventArgs());
                //}
                 if (e.KeyCode == Keys.D && e.Control)
                {
                    if (dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[5].Value.ToString() == "4")
                    {
                        if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) > 0)
                        {
                            TempPayTypeNo = Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value);
                            pnlPartial.Size = new Size(866, 346);

                           // pnlPartial.Location = new Point(12, 12);
                            dgPayChqDetails.Location = new Point(392, 3);
                            dgPayChqDetails.Visible = true;
                            dgPayChqDetails.BringToFront();
                            dgPayChqDetails.Focus();
                            dgPayCreditCardDetails.Visible = false;
                            if (dgPayChqDetails.Rows.Count == 0)
                            {
                                dgPayChqDetails.Rows.Add();
                                dgPayChqDetails.CurrentCell = dgPayChqDetails[0, 0];
                            }
                        }
                    }
                    else if (dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[5].Value.ToString() == "5")
                    {
                        if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) > 0)
                        {
                            TPayTypeNo = Convert.ToInt64(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[1].Value);
                            pnlPartial.Size = new Size(866, 346);

                          //  pnlPartial.Location = new Point(20, 123);
                            dgPayCreditCardDetails.Location = new Point(392, 3);
                            dgPayCreditCardDetails.Visible = true;
                            dgPayCreditCardDetails.Focus();
                            dgPayCreditCardDetails.BringToFront();
                            dgPayChqDetails.Visible = false;
                            if (dgPayCreditCardDetails.Rows.Count == 0)
                            {
                                dgPayCreditCardDetails.Rows.Add();
                                dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[0, 0];
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayChqDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[1].Value == null)
                    {
                        //if (e.RowIndex == 0) dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value = dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value;
                        dtpChqDate.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 72 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                        dtpChqDate.Visible = true;
                        dtpChqDate.BringToFront();
                        dtpChqDate.Focus();
                    }
                    else
                    {
                        dgPayChqDetails.Focus();
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgPayChqDetails.CurrentCell.RowIndex, 1, dgPayChqDetails });
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value != null)
                    {
                        dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "";
                        if (ObjFunction.CheckValidAmount(dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value.ToString()) == false)
                        {
                            dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "Please Enter Valid Amount";
                        }
                        else
                        {
                            double TotAmt = 0;
                            dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "";
                            if (e.RowIndex == dgPayChqDetails.Rows.Count - 1 && dgPayChqDetails.Rows[e.RowIndex].Cells[1].Value != null)
                            {
                                for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                                {
                                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value != null)
                                    {
                                        TotAmt = TotAmt + Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                                    }
                                }
                                txtTotalAmt.Text = TotAmt.ToString();
                                dgPayChqDetails.Rows.Add();
                                dgPayChqDetails.Focus();
                                dgPayChqDetails.CurrentCell = dgPayChqDetails[0, dgPayChqDetails.Rows.Count - 1];
                            }
                            else
                            {
                                for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                                {
                                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value != null)
                                    {
                                        TotAmt = TotAmt + Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                                    }
                                }
                                txtTotalAmt.Text = TotAmt.ToString();
                                //btnOk.Focus();
                            }
                        }
                    }
                    else
                    {
                        dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "Please Enter  Amount";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayChqDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPayChqDetails.CurrentCell.ColumnIndex == 2)
            {
                lblNMsg.Text = "(Ctrl+N) Add New Bank";
            }
            else if (dgPayChqDetails.CurrentCell.ColumnIndex == 3)
            {
                lblNMsg.Text = "(Ctrl+N) Add New Branch";
            }
            else
            {
                lblNMsg.Text = "";
            }
        }

        private void dgPayCreditCardDetails_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex == 2)
            {
                Rectangle cellRect = dgPayChqDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                toolTip1.Show("Ctrl + N - Bank Name",
                              this,
                              dgPayChqDetails.Location.X + cellRect.X + cellRect.Size.Width,
                              dgPayChqDetails.Location.Y + cellRect.Y + cellRect.Size.Height,
                              1200);
                toolTip1.BackColor = CommonFunctions.RowColor;
            }
            else if (e.RowIndex == 3)
            {
                Rectangle cellRect = dgPayChqDetails.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                toolTip1.Show("Ctrl + N - Branch Name",
                              this,
                              dgPayChqDetails.Location.X + cellRect.X + cellRect.Size.Width,
                              dgPayChqDetails.Location.Y + cellRect.Y + cellRect.Size.Height,
                              1200);
                toolTip1.BackColor = CommonFunctions.RowColor;
            }
        }

        private void dgPayChqDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgPayChqDetails.CurrentCell.ColumnIndex == 1)
                    {
                        dtpChqDate.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 72 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                        dtpChqDate.Visible = true;
                        dtpChqDate.BringToFront();
                        dtpChqDate.Focus();
                    }
                    else if (dgPayChqDetails.CurrentCell.ColumnIndex == 2)
                    {
                        pnlBank.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 150 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                        pnlBank.Visible = true;
                        pnlBank.BringToFront();
                        lstBank.Focus();
                    }
                    else if (dgPayChqDetails.CurrentCell.ColumnIndex == 3)
                    {
                        pnlBranch.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 235 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                        pnlBranch.Visible = true;
                        pnlBranch.BringToFront();
                        lstBranch.Focus();
                    }
                    else if (dgPayChqDetails.CurrentCell.ColumnIndex == 4)
                    {
                        double TotAmt = 0;
                        for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                        {
                            if (dgPayChqDetails.Rows[0].Cells[4].Value != null)
                            {
                                TotAmt = TotAmt + Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                            }
                        }
                        txtTotalAmt.Text = TotAmt.ToString();
                       // btnOk.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    //double Amt = 0;
                    if (dgPayChqDetails.Rows[0].Cells[0].Value != null)
                    {
                        if (dgPayChqDetails.Rows[0].Cells[1].FormattedValue.ToString() == "" || dgPayChqDetails.Rows[0].Cells[2].FormattedValue.ToString() == "" || dgPayChqDetails.Rows[0].Cells[3].FormattedValue.ToString() == "")
                        {
                            OMMessageBox.Show("Please Fill Cheque Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            return;
                        }

                        FillChequeDetails();
                        btnOk.Enabled = true;


                            //for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                            //{
                            //    Amt += (dgPayChqDetails.Rows[i].Cells[4].Value == null) ? 0 : Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                            //}

                            //if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) != Amt)
                            //{
                            //    OMMessageBox.Show("Please enter Cheque amount and Cheque Details amount are not same...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            //    dgPayChqDetails.Focus();
                            //}
                            //else
                            //{
                            if (PayType == 1)
                            {
                                pnlPartial.Size = new Size(387, 346);

                                // pnlPartial.Location = new Point(200, 123);
                                dgPayType.CurrentCell = dgPayType[2, dgPayType.CurrentCell.RowIndex];
                                dgPayType.Focus();
                            }
                            else
                            {
                                dgPayChqDetails.Visible = false;
                                pnlPartial.Size = new Size(387, 346);

                                //pnlPartial.Location = new Point(200, 123);
                                dgPayType.CurrentCell = dgPayType[2, dgPayType.CurrentCell.RowIndex];
                                dgPayType.Focus();
                                //btnSave.Enabled = true;
                                //btnSave.Focus();
                                //pnlPartial.Visible = false;
                            }
                            //btnOk.Focus();
                        //}
                    }
                    else
                    {
                        if (PayType == 1)
                        {
                            pnlPartial.Size = new Size(387, 346);

                            //pnlPartial.Location = new Point(200, 123);
                            dgPayType.CurrentCell = dgPayType[2, 3];
                            dgPayType.Focus();
                        }
                        else
                        {
                            //btnSave.Enabled = true;
                            //btnSave.Focus();
                            //pnlPartial.Visible = false;
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //if (dgPayChqDetails.CurrentCell.RowIndex != dgPayChqDetails.Rows.Count - 1)
                    //{
                    if (Convert.ToInt64(dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[5].Value) != 0)
                    {
                        DeleteDtls(5, Convert.ToInt64(dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[5].Value));
                    }
                    dgPayChqDetails.Rows.RemoveAt(dgPayChqDetails.CurrentCell.RowIndex);
                    if (dgPayChqDetails.Rows.Count == 0)
                        dgPayChqDetails.Rows.Add();
                    dgPayChqDetails.CurrentCell = dgPayChqDetails[0, dgPayChqDetails.Rows.Count - 1];
                    // }
                }
                else if (e.KeyCode == Keys.N && e.Control)
                {
                    if (dgPayChqDetails.CurrentCell.ColumnIndex == 2)
                    {
                        Master.OtherBankAE NewF = new Yadi.Master.OtherBankAE(-1);
                        ObjFunction.OpenForm(NewF);
                        if (NewF.ShortID < 0)
                        {
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[2, dgPayChqDetails.CurrentCell.RowIndex];
                        }
                        else if (NewF.ShortID != 0)
                        {
                            ObjFunction.FillList(lstBank, "Select BankNo,BankName From MBank order by BankName");
                            lstBank.SelectedValue = ObjQry.ReturnLong("Select Max(BankNo) From MBank", CommonFunctions.ConStr);
                            dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value = lstBank.Text;
                            dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[6].Value = lstBank.SelectedValue;
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[3, dgPayChqDetails.CurrentCell.RowIndex];
                        }
                    }
                    else if (dgPayChqDetails.CurrentCell.ColumnIndex == 3)
                    {
                        Master.BranchAE NewF = new Yadi.Master.BranchAE(-1);
                        ObjFunction.OpenForm(NewF);
                        if (NewF.ShortID < 0)
                        {
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[2, dgPayChqDetails.CurrentCell.RowIndex];
                        }
                        else if (NewF.ShortID != 0)
                        {
                            ObjFunction.FillList(lstBranch, "Select BranchNo,BranchName From MBranch order by BranchName");
                            lstBranch.SelectedValue = ObjQry.ReturnLong("Select Max(BranchNo) From MBranch ", CommonFunctions.ConStr);
                            dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value = lstBranch.Text;
                            dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[7].Value = lstBranch.SelectedValue;
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[4, dgPayChqDetails.CurrentCell.RowIndex];

                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillChequeDetails()
        {
            DataRow dr;
            int chqRow=-1;
            for (int i = 0; i < dtChqDtls.Rows.Count; i++)
            {
                if (dtChqDtls.Rows[i].ItemArray[8].ToString() == TempPayTypeNo.ToString())
                {
                    chqRow = i; break;  
                }
            }
            if (chqRow != -1)
                dtChqDtls.Rows.RemoveAt(chqRow);

            for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
            {
                if (dgPayChqDetails.Rows[i].Cells[0].Value != null && dgPayChqDetails.Rows[i].Cells[0].Value.ToString() != "")
                {
                    dr = dtChqDtls.NewRow();
                    dr[0] = dgPayChqDetails.Rows[i].Cells[0].Value;
                    dr[1] = dgPayChqDetails.Rows[i].Cells[1].Value;
                    dr[2] = dgPayChqDetails.Rows[i].Cells[2].Value;
                    dr[3] = dgPayChqDetails.Rows[i].Cells[3].Value;
                    dr[4] = dgPayChqDetails.Rows[i].Cells[4].Value;
                    dr[5] = dgPayChqDetails.Rows[i].Cells[5].Value;
                    dr[6] = dgPayChqDetails.Rows[i].Cells[6].Value;
                    dr[7] = dgPayChqDetails.Rows[i].Cells[7].Value;
                    dr[8] = TempPayTypeNo;
                    dr[9] = i;
                    dtChqDtls.Rows.Add(dr);
                }
            }
        }

        public void FillCrCardDetails()
        {
            DataRow dr;
            int crdRow = -1;
            for (int i = 0; i < dtCrDtls.Rows.Count; i++)
            {
                if (dtCrDtls.Rows[i].ItemArray[7].ToString() == TPayTypeNo.ToString())
                {
                    crdRow = i; break;
                }
            }
            if (crdRow != -1)
                dtCrDtls.Rows.RemoveAt(crdRow);

            for (int i = 0; i < dgPayCreditCardDetails.Rows.Count; i++)
            {
                if (dgPayCreditCardDetails.Rows[i].Cells[0].Value != null && dgPayCreditCardDetails.Rows[i].Cells[0].Value.ToString() != "")
                {
                    dr = dtCrDtls.NewRow();
                    dr[0] = dgPayCreditCardDetails.Rows[i].Cells[0].Value;
                    dr[1] = dgPayCreditCardDetails.Rows[i].Cells[1].Value;
                    dr[2] = dgPayCreditCardDetails.Rows[i].Cells[2].Value;
                    dr[3] = dgPayCreditCardDetails.Rows[i].Cells[3].Value;
                    dr[4] = dgPayCreditCardDetails.Rows[i].Cells[4].Value;
                    dr[5] = dgPayCreditCardDetails.Rows[i].Cells[5].Value;
                    dr[6] = dgPayCreditCardDetails.Rows[i].Cells[6].Value;
                    dr[7] = TPayTypeNo;
                    dr[8] = i;
                    dtCrDtls.Rows.Add(dr);
                }
            }
        }

        private void dgPayCreditCardDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 1)
                    {
                        pnlBank.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 72 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                        pnlBank.Visible = true;
                        pnlBank.BringToFront();
                        lstBank.Focus();
                    }
                    else if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 2)
                    {
                        pnlBranch.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 200 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                        pnlBranch.Visible = true;
                        pnlBranch.BringToFront();
                        lstBranch.Focus();
                    }
                    else if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 3)
                    {
                        double TotAmt = 0;
                        for (int i = 0; i < dgPayCreditCardDetails.Rows.Count; i++)
                        {
                            if (dgPayCreditCardDetails.Rows[i].Cells[3].Value != null)
                            {
                                TotAmt = TotAmt + Convert.ToDouble(dgPayCreditCardDetails.Rows[i].Cells[3].Value);
                            }
                        }
                        txtTotalAmt.Text = TotAmt.ToString();
                        btnOk.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    //double Amt = 0;
                    if (dgPayCreditCardDetails.Rows[0].Cells[0].Value != null)
                    {
                        if (dgPayCreditCardDetails.Rows[0].Cells[0].FormattedValue.ToString() == "" || dgPayCreditCardDetails.Rows[0].Cells[1].FormattedValue.ToString() == "" || dgPayCreditCardDetails.Rows[0].Cells[2].FormattedValue.ToString() == "")
                        {
                            OMMessageBox.Show("Please Fill Credit Card Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            return;
                        }
                        FillCrCardDetails();
                        btnOk.Enabled = true;

                        //for (int i = 0; i < dgPayCreditCardDetails.Rows.Count; i++)
                        //{
                        //    Amt += (dgPayCreditCardDetails.Rows[i].Cells[3].Value == null) ? 0 : Convert.ToDouble(dgPayCreditCardDetails.Rows[i].Cells[3].Value);
                        //}
                        //if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) != Amt)
                        //{
                        //    OMMessageBox.Show("Please enter CrediCard amount and CreditCard Details amount are not same...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        //    dgPayCreditCardDetails.Focus();
                        //}
                        //else
                        //{
                            if (PayType == 1)
                            {
                                pnlPartial.Size = new Size(387, 346);

                                //pnlPartial.Location = new Point(200, 123);
                                dgPayType.CurrentCell = dgPayType[2, 4];
                                dgPayType.Focus();
                            }
                            else
                            {
                                dgPayCreditCardDetails.Visible = false;
                                pnlPartial.Size = new Size(387, 346);

                                dgPayType.CurrentCell = dgPayType[2, 4];
                                dgPayType.Focus();
                                //btnSave.Enabled = true;
                                //btnSave.Focus();
                                //pnlPartial.Visible = false;
                            }
                            //btnOk.Focus();
                        //}
                    }
                    else
                    {
                        if (PayType == 1)
                        {
                            pnlPartial.Size = new Size(387, 346);

                            // pnlPartial.Location = new Point(200, 123);
                            dgPayType.CurrentCell = dgPayType[2, 4];
                            dgPayType.Focus();
                        }
                        else
                        {
                            //btnSave.Enabled = true;
                            //btnSave.Focus();
                            // pnlPartial.Visible = false;
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //if (dgPayCreditCardDetails.CurrentCell.RowIndex != dgPayCreditCardDetails.Rows.Count - 1)
                    //{
                    if (Convert.ToInt64(dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[4].Value) != 0)
                    {
                        DeleteDtls(5, Convert.ToInt64(dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[4].Value));
                    }
                    dgPayCreditCardDetails.Rows.RemoveAt(dgPayCreditCardDetails.CurrentCell.RowIndex);
                    if (dgPayCreditCardDetails.Rows.Count == 0)
                        dgPayCreditCardDetails.Rows.Add();
                    dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[0, dgPayCreditCardDetails.Rows.Count - 1];
                    //}
                }
                else if (e.KeyCode == Keys.N && e.Control)
                {
                    if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 1)
                    {
                        Master.OtherBankAE NewF = new Yadi.Master.OtherBankAE(-1);
                        ObjFunction.OpenForm(NewF);
                        if (NewF.ShortID < 0)
                        {
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[2, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        }
                        else if (NewF.ShortID != 0)
                        {
                            ObjFunction.FillList(lstBank, "Select BankNo,BankName From MBank order by BankName");
                            lstBank.SelectedValue = ObjQry.ReturnLong("Select Max(BankNo) From MBank", CommonFunctions.ConStr);
                            dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[1].Value = lstBank.Text;
                            dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[5].Value = lstBank.SelectedValue;
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        }
                    }
                    else if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 2)
                    {
                        Master.BranchAE NewF = new Yadi.Master.BranchAE(-1);
                        ObjFunction.OpenForm(NewF);
                        if (NewF.ShortID < 0)
                        {
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        }
                        else if (NewF.ShortID != 0)
                        {
                            ObjFunction.FillList(lstBranch, "Select BranchNo,BranchName From MBranch order by BranchName");
                            lstBranch.SelectedValue = ObjQry.ReturnLong("Select Max(BranchNo) From MBranch ", CommonFunctions.ConStr);
                            dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[2].Value = lstBranch.Text;
                            dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[6].Value = lstBranch.SelectedValue;
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayCreditCardDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (dgPayCreditCardDetails.Rows[e.RowIndex].Cells[1].Value == null)
                    {
                        //if (e.RowIndex == 0) dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].Value = dgPayType.Rows[4].Cells[2].Value;
                        pnlBank.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 72 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                        pnlBank.Visible = true;
                        pnlBank.BringToFront();
                        lblNMsg.Text = "(Ctrl+N) Add New Bank";
                        lstBank.Focus();
                    }
                    else
                    {
                        dgPayCreditCardDetails.Focus();
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgPayCreditCardDetails.CurrentCell.RowIndex, 1, dgPayCreditCardDetails });
                    }
                }
                if (e.ColumnIndex == 3)
                {
                    if (dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "";
                        if (ObjFunction.CheckValidAmount(dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].Value.ToString()) == false)
                        {
                            dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "Please Enter Valid Amount";
                        }
                        else
                        {
                            double TotAmt = 0;
                            dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "";
                            if (e.RowIndex == dgPayCreditCardDetails.Rows.Count - 1 && dgPayCreditCardDetails.Rows[e.RowIndex].Cells[1].Value != null)
                            {
                                dgPayCreditCardDetails.Rows.Add();
                                dgPayCreditCardDetails.Focus();
                                dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[0, dgPayCreditCardDetails.Rows.Count - 1];
                            }

                            for (int i = 0; i < dgPayCreditCardDetails.Rows.Count; i++)
                            {
                                if (dgPayCreditCardDetails.Rows[i].Cells[3].Value != null)
                                {
                                    TotAmt = TotAmt + Convert.ToDouble(dgPayCreditCardDetails.Rows[i].Cells[3].Value);
                                }
                            }
                            txtTotalAmt.Text = TotAmt.ToString();
                        }
                    }
                    else
                    {
                        dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "Please Enter  Amount";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayCreditCardDetails_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 1)
            {
                lblNMsg.Text = "(Ctrl+N) Add New Bank";
            }
            else if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 2)
            {
                lblNMsg.Text = "(Ctrl+N) Add New Branch";
            }
            else
            {
                lblNMsg.Text = "";
            }
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        #region Delete code
        public void InitDelTable()
        {
            dtDelete.Columns.Add();
            dtDelete.Columns.Add();
        }

        public void DeleteDtls(int Type, long PkNo)
        {
            DataRow dr = null;
            dr = dtDelete.NewRow();
            dr[0] = Type;
            dr[1] = PkNo;
            dtDelete.Rows.Add(dr);
        }

        public void DeleteValues()
        {
            if (dtDelete != null)
            {
                for (int i = 0; i < dtDelete.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
                    {
                        tStock.PkStockTrnNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTStock(tStock);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 2)
                    {
                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTVoucherDetails(tVoucherDetails);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 3)
                    {
                        tVoucherDetails.CompanyNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        tVoucherDetails.FkVoucherNo = ID;
                        dbTVoucherEntry.DeleteTVoucherDetailsCompany(tVoucherDetails);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 4)
                    {
                        tStockGodown.PKStockGodownNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTStockGodown(tStockGodown);
                    }
                   
                }
                dtDelete.Rows.Clear();
            }
        }


        #endregion

        private void dtpChqDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[1].Value = dtpChqDate.SelectionStart.ToString("dd-MMM-yy");
                    dtpChqDate.Visible = false;
                    if (dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value == null)
                    {
                        pnlBank.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 150 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                        pnlBank.Visible = true;
                        pnlBank.BringToFront();
                        lblNMsg.Text = "(Ctrl+N) Add New Bank";
                        lstBank.Focus();
                    }
                    else
                    {
                        dgPayChqDetails.Focus();
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[2, dgPayChqDetails.CurrentCell.RowIndex];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgPayChqDetails.Visible == true)
                    {
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value = lstBank.Text;
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[6].Value = lstBank.SelectedValue;
                        pnlBank.Visible = false;
                        if (dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value == null)
                        {
                            pnlBranch.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 235 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                            pnlBranch.Visible = true;
                            pnlBranch.BringToFront();
                            lblNMsg.Text = "(Ctrl+N) Add New Branch";
                            lstBranch.Focus();
                        }
                        else
                        {
                            dgPayChqDetails.Focus();
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[3, dgPayChqDetails.CurrentCell.RowIndex];
                        }
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[1].Value = lstBank.Text;
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[5].Value = lstBank.SelectedValue;
                        pnlBank.Visible = false;
                        if (dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[2].Value == null)
                        {
                            pnlBranch.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 235 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 40);
                            pnlBranch.Visible = true;
                            pnlBranch.BringToFront();
                            lblNMsg.Text = "(Ctrl+N) Add New Branch";
                            lstBranch.Focus();
                        }
                        else
                        {
                            dgPayCreditCardDetails.Focus();
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        }
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (dgPayChqDetails.Visible == true)
                    {
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value = "";
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[6].Value = 0;
                        pnlBank.Visible = false;
                        if (dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value == null)
                        {
                            dgPayChqDetails.Focus();
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[2, dgPayChqDetails.CurrentCell.RowIndex];
                            //pnlBranch.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 200 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                            //pnlBranch.Visible = true;
                            //pnlBranch.BringToFront();
                            //lstBranch.Focus();
                        }
                        else
                        {
                            dgPayChqDetails.Focus();
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[3, dgPayChqDetails.CurrentCell.RowIndex];
                        }
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[1].Value = "";
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[5].Value = 0;
                        pnlBank.Visible = false;
                        if (dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[2].Value == null)
                        {
                            pnlBranch.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 200 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                            pnlBranch.Visible = true;
                            pnlBranch.BringToFront();
                            lblNMsg.Text = "(Ctrl+N) Add New Branch";
                            lstBranch.Focus();
                        }
                        else
                        {
                            dgPayCreditCardDetails.Focus();
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstBranch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgPayChqDetails.Visible == true)
                    {
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value = lstBranch.Text;
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[7].Value = lstBranch.SelectedValue;
                        pnlBranch.Visible = false;
                        dgPayChqDetails.Focus();
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[4, dgPayChqDetails.CurrentCell.RowIndex];
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[2].Value = lstBranch.Text;
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[6].Value = lstBranch.SelectedValue;
                        pnlBranch.Visible = false;
                        dgPayCreditCardDetails.Focus();
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (dgPayChqDetails.Visible == true)
                    {
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value = "";
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[7].Value = 0;
                        pnlBranch.Visible = false;
                        dgPayChqDetails.Focus();
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[4, dgPayChqDetails.CurrentCell.RowIndex];
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[3].Value = "";
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[6].Value = 0;
                        pnlBranch.Visible = false;
                        dgPayCreditCardDetails.Focus();
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayChqDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgPayChqDetails.CurrentCell.ColumnIndex == 4)
            {
                TextBox txt = (TextBox)e.Control;
                txt.TextChanged += new EventHandler(txtChqAmt_TextChanged);
            }
        }

        public void txtChqAmt_TextChanged(object sender, EventArgs e)
        {
            if (dgPayChqDetails.CurrentCell.ColumnIndex == 4)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void dgPayCreditCardDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 3)
            {
                TextBox txt = (TextBox)e.Control;
                txt.TextChanged+=new EventHandler(txtCCAmt_TextChanged);
            }
        }

        public void txtCCAmt_TextChanged(object sender, EventArgs e)
        {
            if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 3)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }

        public void DeleteDtls()
        {
            for (int i = 0; i < dgPayType.Rows.Count; i++)
            {
                if (dgPayType.Rows[i].Cells[5].Value.ToString() == "4" && Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value.ToString())==0)
                {
                    for (int j = 0; j < dtChqDtls.Rows.Count; j++)
                    {
                        if (dgPayType.Rows[i].Cells[1].Value.ToString() == dtChqDtls.Rows[j].ItemArray[8].ToString())
                        {
                            dtChqDtls.Rows.RemoveAt(j);
                        }
                    }

                }
            }
        }

        

    }
}
