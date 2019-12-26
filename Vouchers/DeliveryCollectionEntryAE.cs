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
    /// <summary>
    /// This Class use for Collection Entry
    /// </summary>
    public partial class DeliveryCollectionEntryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        TVoucherChqCreditDetails tVchChqCredit = new TVoucherChqCreditDetails();
        TVoucherRefDetails tVchRefDtls = new TVoucherRefDetails();
        TVoucherEntryCompany tVoucherEntryComp = new TVoucherEntryCompany();
        TVoucherDetailsCompany tVoucherDetailsComp = new TVoucherDetailsCompany();

        DataTable dtSearch = new DataTable();
        DataTable dtPayTypeLedger = new DataTable();
        int rowCount;
        long VoucherType = 0, grpNo;
        DataTable dtCompRatio = new DataTable(), dtBill;

        public DeliveryCollectionEntryAE()
        {
            InitializeComponent();
            this.VoucherType = OM.VchType.Sales;
        }
     
        public DeliveryCollectionEntryAE(long VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
            if (VoucherType == VchType.Sales)
            {
                this.Text = "Home Delv. Collection Entry";
                grpNo = GroupType.SundryDebtors;
            }

        }

        private void SalesVoucherAE_Load(object sender, EventArgs e)
        {
            try
            {

                KeyDownFormat(this.Controls);
                dtpVoucherDate.Value = DBGetVal.ServerTime;
                string sql = " SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName " +
                           " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                           " WHERE (TVoucherEntry.OrderType = 2) AND (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.VoucherTypeCode = 15) And MLedger.GroupNo =" + grpNo + " order by MLedger.LedgerName ";

                //ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo =" + grpNo + " order by LedgerName", "All Party");
                ObjFunction.FillCombo(cmbPartyName, sql, "All Party");

                dtPayTypeLedger = ObjFunction.GetDataView("SELECT     MPayTypeLedger.PayTypeNo, MPayTypeLedger.LedgerNo, MPayType.PayTypeName " +
                                                           " FROM MPayTypeLedger INNER JOIN MPayType ON MPayTypeLedger.PayTypeNo = MPayType.PKPayTypeNo " +
                                                            "  where MPayTypeLedger.CompanyNo=" + DBGetVal.FirmNo + " and MPayType.PKPayTypeNo not in (1,3) order by PayTypeName").Table;


                if (VoucherType == VchType.Sales)
                    ObjFunction.FillCombo(cmbBank, "Select BankNo,BankName From MOtherBank where IsActive='true' order by BankName");
                else
                    ObjFunction.FillCombo(cmbBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + " And IsActive='true' order by LedgerName");

                ObjFunction.FillCombo(cmbBranch, "Select BranchNo,BranchName From MBranch  Where isActive='true' order by BranchName");


                if (VoucherType == VchType.Sales)
                {
                    ObjFunction.FillCombo(cmbCrBank, "Select BankNo,BankName From MOtherBank where IsActive='true' order by BankName");
                    ObjFunction.FillList(lstPayType, "Select PKPayTypeNo,PayTypeName from MPayType where ControlUnder not in(1,3)");
                    //ObjFunction.FillList(lstPayType, "Select PKPayTypeNo,PayTypeName from MPayType where PayTypeNo not in(1,3)");
                    //ObjFunction.FillComb(cmbPayType, "Select PKPayTypeNo,PayTypeName from MPayType where PKPayTypeNo not in(1,3)");
                }
                else
                {
                    ObjFunction.FillCombo(cmbCrBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + "  And IsActive='true' order by LedgerName");
                    ObjFunction.FillList(lstPayType, "Select PKPayTypeNo,PayTypeName from MPayType where PKPayTypeNo in(2,4,5)");
                }

                ObjFunction.FillCombo(cmbCrBranch, "Select BranchNo,BranchName From MBranch Where isActive='true' order by BranchName");

                cmbPartyName.Focus();
                //lblNetBal.Font = new Font("Verdana", 11, FontStyle.Bold);

                label5.Visible = false;


                if (VoucherType == VchType.Sales)
                {
                    label12.Visible = true;
                    cmbBranch.Visible = true;
                    label2.Visible = true;
                    cmbCrBranch.Visible = true;
                }
                else
                {
                    label12.Visible = false;
                    cmbBranch.Visible = false;
                    cmbBank.Width = 300;
                    if (cmbBranch.Items.Count > 1)
                        cmbBranch.SelectedIndex = 1;

                    label2.Visible = false;
                    cmbCrBranch.Visible = false;
                    cmbCrBank.Width = 200;
                    if (cmbCrBranch.Items.Count > 1)
                        cmbCrBranch.SelectedIndex = 1;

                }
                rbCheck_Changed();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BindGrid()
        {
            try
            {
                if (cmbPartyName.SelectedIndex != 0)
                {
                    GridView.Rows.Clear();
                    dgBillSelection.Rows.Clear();

                    double NetBal = 0;
                    DataTable dt = ObjFunction.GetDataView("Exec GetDeliveryCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VoucherType + "," + DBGetVal.FirmNo + "").Table;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridView.Rows.Add();
                        dgBillSelection.Rows.Add();

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                            //dgBillSelection.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                        }
                        
                        NetBal = NetBal + Convert.ToDouble(dt.Rows[i].ItemArray[ColIndex.NetBal].ToString());
                    }
                    if (ObjFunction.GetComboValue(cmbPartyName) != -1)
                        GridView.Columns[ColIndex.LedgerName].Visible = false;
                    else
                        GridView.Columns[ColIndex.LedgerName].Visible = true;

                    GridView.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //dgBillSelection.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgBillSelection.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgBillSelection.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgBillSelection.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgBillSelection.Columns[ColIndex.Chk].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    lblNetB.Text = NetBal.ToString(); ;
                    
                    //(
                    //          Convert.ToDouble(dtDetails.Rows[0].ItemArray[1].ToString()) -
                    //          Convert.ToDouble(dtDetails.Rows[0].ItemArray[0].ToString())).ToString("0.00");
                    if (Convert.ToDouble(lblNetB.Text) > 0)
                        lblPayStatus.Text = "To Pay";
                     if (Convert.ToDouble(lblNetB.Text) < 0)
                        lblPayStatus.Text = "To Receive";
                    else
                        lblPayStatus.Text = "";

                }
                else
                    DataClrscr();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private bool Validations()
        {
            bool flag = false;
            try
            {
                if (GridView.Rows.Count < 0)
                {
                    OMMessageBox.Show("Sorry No Data to Save......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < GridView.Rows.Count; i++)
                    {
                        if (GridView.Rows[i].Cells[5].ErrorText == "")
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            OMMessageBox.Show("Please Enter Valid Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            GridView.CurrentCell = GridView.Rows[i].Cells[5];
                            GridView.Focus();
                            return false;
                        }
                    }

                }

                flag = true;

                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool ValidationsDGBill()
        {
            bool flag = false;
            try
            {
                if (dgBillSelection.Rows.Count < 0)
                {
                    OMMessageBox.Show("Sorry No Data to Save......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < dgBillSelection.Rows.Count; i++)
                    {
                        if (dgBillSelection.Rows[i].Cells[ColIndex.Amount].ErrorText == "")
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            OMMessageBox.Show("Please Enter Valid Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dgBillSelection.CurrentCell = dgBillSelection.Rows[i].Cells[ColIndex.Amount];
                            dgBillSelection.Focus();
                            return false;
                        }
                    }

                }

                flag = true;

                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void setValues()
        {
            try
            {
                if (cmbPartyName.SelectedIndex != 0)
                {
                    bool flag = false, flagAdjs = false;
                   
                    if (Validations() == true)
                    {

                        for (int j = 0; j < GridView.RowCount; j++)
                        {
                            if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value.ToString()) != 0)
                            {
                                setCompanyRatio(Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value.ToString()));
                                long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value.ToString()) + "", CommonFunctions.ConStr);
                                dbTVoucherEntry = new DBTVaucherEntry();
                                tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = 0;
                                if (VoucherType == VchType.Sales)
                                    tVoucherEntry.VoucherTypeCode = VchType.SalesReceipt;
                                else
                                    tVoucherEntry.VoucherTypeCode = VchType.PurchasePayment;
                                tVoucherEntry.VoucherUserNo = 0;
                                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                tVoucherEntry.VoucherTime = Convert.ToDateTime("01-Jan-1900");
                                tVoucherEntry.Reference = "";

                                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                tVoucherEntry.BilledAmount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVoucherEntry.ChallanNo = "";
                                tVoucherEntry.Remark = "";
                                if (ControlUnder == 4)
                                    tVoucherEntry.ChequeNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.ChqNo].Value);
                                else
                                    tVoucherEntry.ChequeNo = 0;
                                tVoucherEntry.ClearingDate = Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                tVoucherEntry.Narration = "";
                                tVoucherEntry.UserID = DBGetVal.UserID;
                                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                tVoucherEntry.OrderType = 1;
                                tVoucherEntry.PayTypeNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                tVoucherEntry.TransporterCode = 0;
                                tVoucherEntry.TransPayType = 0;
                                tVoucherEntry.LRNo = "";
                                tVoucherEntry.TransportMode = 0;
                                tVoucherEntry.TransNoOfItems = 0;

                                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); SetVoucherCompany(tVoucherEntry);

                                if (VoucherType == VchType.Sales)
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 2;
                                    tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.MainLedgerNo].Value);
                                    tVoucherDetails.Debit = 0;
                                    tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                                }
                                else if (VoucherType == VchType.Purchase)
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 1;
                                    tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.MainLedgerNo].Value);
                                    tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                                }

                                tVchRefDtls = new TVoucherRefDetails();
                                tVchRefDtls.PkRefTrnNo = 0;
                                tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                tVchRefDtls.TypeOfRef = 2;
                                tVchRefDtls.RefNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value);
                                tVchRefDtls.DueDays = 0;
                                tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                tVchRefDtls.Amount = tVoucherEntry.BilledAmount;
                                if (VoucherType == VchType.Sales)
                                    tVchRefDtls.SignCode = 2;
                                else
                                    tVchRefDtls.SignCode = 1;

                                tVchRefDtls.UserID = DBGetVal.UserID;
                                tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                                if (ControlUnder == 4)//GridView.Rows[j].Cells[ColIndex.PayType].Value.ToString() == "Cheque"
                                {
                                    tVchChqCredit.PkSrNo = 0;
                                    tVchChqCredit.ChequeNo = Convert.ToString(GridView.Rows[j].Cells[ColIndex.ChqNo].Value);
                                    tVchChqCredit.ChequeDate = Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                    tVchChqCredit.BankNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BankNo].Value);
                                    tVchChqCredit.BranchNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BranchNo].Value);
                                    tVchChqCredit.CreditCardNo = "";
                                    tVchChqCredit.Amount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVchChqCredit.PostFkVoucherNo = 0;
                                    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                }
                                else if (ControlUnder == 5)//GridView.Rows[j].Cells[ColIndex.PayType].Value.ToString() == "Credit Card"
                                {
                                    tVchChqCredit.PkSrNo = 0;
                                    tVchChqCredit.ChequeNo = "";
                                    tVchChqCredit.ChequeDate = Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                    tVchChqCredit.BankNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BankNo].Value);
                                    tVchChqCredit.BranchNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BranchNo].Value);
                                    tVchChqCredit.CreditCardNo = Convert.ToString(GridView.Rows[j].Cells[ColIndex.ChqNo].Value);
                                    tVchChqCredit.Amount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVchChqCredit.PostFkVoucherNo = 0;
                                    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                }

                                if (VoucherType == VchType.Sales)
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 1;
                                    tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                    tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.Credit = 0;
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                                }
                                else if (VoucherType == VchType.Purchase)
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 2;
                                    tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value); //0;
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                                }

                                if (dbTVoucherEntry.ExecuteNonQueryStatements() != 0)
                                {
                                    flag = true;
                                    flagAdjs = true;
                                }
                                else
                                {
                                    flag = false;
                                    flagAdjs = true;
                                    break;
                                }

                            }

                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Please Enter Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                    if (flag == true && flagAdjs == true)
                    {
                        OMMessageBox.Show("Voucher Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();
                        rbCheck_Changed();
                    }
                    else if (flagAdjs == true)
                        OMMessageBox.Show("Voucher Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                }
                else
                {
                    OMMessageBox.Show("Please Select Party Name ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void setValues1()
        {
            try
            {
                bool flag = false;
                if (Validations() == true)
                {
                    for (int j = 0; j < GridView.RowCount; j++)
                    {
                        if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value.ToString()) != 0)
                        {
                            dbTVoucherEntry = new DBTVaucherEntry();
                            tVoucherEntry = new TVoucherEntry();
                            tVoucherEntry.PkVoucherNo = 0;
                            if (VoucherType == VchType.Sales)
                                tVoucherEntry.VoucherTypeCode = VchType.SalesReceipt;
                            else
                                tVoucherEntry.VoucherTypeCode = VchType.PurchasePayment;
                            tVoucherEntry.VoucherUserNo = 0;
                            tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                            tVoucherEntry.VoucherTime = Convert.ToDateTime("01-Jan-1900");
                            tVoucherEntry.Reference = "";
                            tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                            tVoucherEntry.BilledAmount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                            tVoucherEntry.ChallanNo = "";
                            tVoucherEntry.Remark = "";
                            if (GridView.Rows[j].Cells[ColIndex.PayType].Value.ToString() == "Cheque")
                                tVoucherEntry.ChequeNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.ChqNo].Value);
                            else
                                tVoucherEntry.ChequeNo = 0;
                            tVoucherEntry.ClearingDate = Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                            tVoucherEntry.Narration = "";
                            tVoucherEntry.UserID = DBGetVal.UserID;
                            tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                            tVoucherEntry.OrderType = 1;
                            tVoucherEntry.PayTypeNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                            tVoucherEntry.TransporterCode = 0;
                            tVoucherEntry.TransPayType = 0;
                            tVoucherEntry.LRNo = "";
                            tVoucherEntry.TransportMode = 0;
                            tVoucherEntry.TransNoOfItems = 0;

                            dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); SetVoucherCompany(tVoucherEntry);

                            if (VoucherType == VchType.Sales)
                            {

                                tVoucherDetails = new TVoucherDetails();
                                tVoucherDetails.PkVoucherTrnNo = 0;
                                tVoucherDetails.VoucherSrNo = 1;
                                tVoucherDetails.SignCode = 2;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.MainLedgerNo].Value);
                                tVoucherDetails.Debit = 0;
                                tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVoucherDetails.SrNo = Others.Party;
                                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                tVoucherDetails.Narration = "";
                                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                            }
                            else if (VoucherType == VchType.Purchase)
                            {
                                tVoucherDetails = new TVoucherDetails();
                                tVoucherDetails.PkVoucherTrnNo = 0;
                                tVoucherDetails.VoucherSrNo = 1;
                                tVoucherDetails.SignCode = 1;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.MainLedgerNo].Value);
                                tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVoucherDetails.SrNo = Others.Party;
                                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                tVoucherDetails.Narration = "";
                                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                            }

                            tVchRefDtls = new TVoucherRefDetails();
                            tVchRefDtls.PkRefTrnNo = 0;
                            tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                            tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                            tVchRefDtls.TypeOfRef = 2;
                            tVchRefDtls.RefNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value);
                            tVchRefDtls.DueDays = 0;
                            tVchRefDtls.DueDate = DBGetVal.ServerTime;
                            tVchRefDtls.Amount = tVoucherEntry.BilledAmount;
                            tVchRefDtls.SignCode = 1;
                            tVchRefDtls.UserID = DBGetVal.UserID;
                            tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                            tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                            dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                            if (GridView.Rows[j].Cells[ColIndex.PayType].Value.ToString() == "Cheque")
                            {

                                tVchChqCredit.PkSrNo = 0;
                                tVchChqCredit.ChequeNo = Convert.ToString(GridView.Rows[j].Cells[ColIndex.ChqNo].Value);
                                tVchChqCredit.ChequeDate = Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                tVchChqCredit.BankNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BankNo].Value);
                                tVchChqCredit.BranchNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BranchNo].Value);
                                tVchChqCredit.CreditCardNo = "";
                                tVchChqCredit.Amount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVchChqCredit.PostFkVoucherNo = 0;
                                tVchChqCredit.PostFkVoucherTrnNo = 0;
                                tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                            }
                            else if (GridView.Rows[j].Cells[ColIndex.PayType].Value.ToString() == "Credit Card")
                            {
                                tVchChqCredit.PkSrNo = 0;
                                tVchChqCredit.ChequeNo = "";
                                tVchChqCredit.ChequeDate = Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                tVchChqCredit.BankNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BankNo].Value);
                                tVchChqCredit.BranchNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BranchNo].Value);
                                tVchChqCredit.CreditCardNo = Convert.ToString(GridView.Rows[j].Cells[ColIndex.ChqNo].Value);
                                tVchChqCredit.Amount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVchChqCredit.PostFkVoucherNo = 0;
                                tVchChqCredit.PostFkVoucherTrnNo = 0;
                                tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                            }

                            if (VoucherType == VchType.Sales)
                            {
                                tVoucherDetails = new TVoucherDetails();
                                tVoucherDetails.PkVoucherTrnNo = 0;
                                tVoucherDetails.VoucherSrNo = 2;
                                tVoucherDetails.SignCode = 1;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.MainLedgerNo].Value);
                                tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVoucherDetails.Credit = 0;
                                tVoucherDetails.SrNo = 0;
                                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                tVoucherDetails.Narration = "";
                                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                            }
                            else if (VoucherType == VchType.Purchase)
                            {
                                tVoucherDetails = new TVoucherDetails();
                                tVoucherDetails.PkVoucherTrnNo = 0;
                                tVoucherDetails.VoucherSrNo = 2;
                                tVoucherDetails.SignCode = 2;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.MainLedgerNo].Value);
                                tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value); //0;
                                tVoucherDetails.SrNo = 0;
                                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                tVoucherDetails.Narration = "";
                                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                            }

                            if (dbTVoucherEntry.ExecuteNonQueryStatements() != 0)
                            {
                                flag = true;
                            }
                            else
                            {
                                flag = false;
                                break;
                            }

                        }

                    }

                    if (flag == true)
                    {
                        OMMessageBox.Show("Voucher Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();


                    }
                    else
                        OMMessageBox.Show("Voucher Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            setValues();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region KeyDown Events
        private void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
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
            if (e.Alt && e.KeyCode == Keys.F2)
            {
                btnAdvanceSearch_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                BtnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.P && e.Control)
            {
                PrintOutstanding();
            }
        }
        #endregion

        private void dtpVoucherDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                cmbPartyName.Focus();
            }
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (GridView.Rows.Count != 0)
                    {
                        if (GridView.CurrentCell.ColumnIndex == ColIndex.PayType)
                        {
                            pnlPaytype.Visible = true;
                            lstPayType.Focus();
                        }
                        else if (GridView.CurrentCell.ColumnIndex == ColIndex.Amount)
                        {
                            if (GridView.CurrentCell.Value != null)
                            {
                                if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                                    GridView.CurrentCell.ErrorText = "Please Enter Valid Amount";
                                else
                                    GridView.CurrentCell = GridView[ColIndex.PayType, GridView.CurrentCell.RowIndex];
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    if (pnlRecord.Visible == true)
                    {
                        pnlRecord.Visible = false;
                        GridView.CurrentCell = GridView[ColIndex.TotRec, GridView.CurrentCell.RowIndex];
                    }
                    else
                        BtnSave.Focus();
                }
                else if (e.KeyCode == Keys.F4)
                {
                    e.SuppressKeyPress = true;
                    BindRecords();
                    dgDataRecord.Focus();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private static class ColIndex
        {
            public static int BillNo = 0;
            public static int BillDate = 1;
            public static int LedgerName = 2;
            public static int BillAmt = 3;
            public static int TotRec = 4;
            public static int NetBal = 5;
            public static int Amount = 6;
            public static int PayType = 7;
            public static int LedgerNo = 8;
            public static int ChqNo = 9;
            public static int ChqDate = 10;
            public static int BankNo = 11;
            public static int BranchNo = 12;
            public static int PayTypeNo = 13;
            public static int RefNo = 14;
            public static int MainLedgerNo = 15;
            public static int Chk = 16;
        }

        private void cmbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                processPartyNameChange();
            }
        }

        private void processPartyNameChange()
        {
            BindGrid();
            if (GridView.Rows.Count > 0)
            {
                GridView.Focus();
                GridView.CurrentCell = GridView[ColIndex.Amount, 0];
                label5.Visible = true;
            }
            rbCheck_Changed();
        }

        private void lstPayType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Panel p = new Panel();
                    if (pnlBillselection.Visible == true)
                        p = pnlBillselection;
                    else
                        p = pnlBillwise;

                    e.SuppressKeyPress = true;
                    txtChqNo.Text = "";
                    txtCrCardNo.Text = "";
                    cmbBank.SelectedIndex = 0;
                    cmbBranch.SelectedIndex = 0;
                    cmbCrBank.SelectedIndex = 0;
                    cmbCrBranch.SelectedIndex = 0;

                    // GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                    //manali
                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + lstPayType.SelectedValue + "", CommonFunctions.ConStr);
                    if (ControlUnder == 1 || ControlUnder == 4 || ControlUnder == 5)
                    {
                        if (ControlUnder == 4 || ControlUnder == 5)
                        {
                            if (ControlUnder == 4)
                            {
                                int y = pnlMain.Location.Y + p.Location.Y + GridView.Location.Y;
                                y = y + ((p.Height - GridView.Height) / 2);
                                int x = (GridView.Width - pnlchq.Width) / 2;
                                pnlchq.Location = new Point(GridView.Location.X + x, y + 10);

                                pnlPaytype.Visible = false;
                                pnlchq.Visible = true;
                                BtnSave.Enabled = false;
                                txtChqNo.Focus();
                            }
                            else if (ControlUnder == 5)
                            {
                                int y = pnlMain.Location.Y + p.Location.Y + GridView.Location.Y;
                                y = y + ((p.Height - GridView.Height) / 2);
                                int x = (GridView.Width - pnlCredit.Width) / 2;
                                pnlCredit.Location = new Point(GridView.Location.X + x, y + 10);
                                BtnSave.Enabled = false;
                                pnlPaytype.Visible = false;
                                pnlCredit.Visible = true;
                                txtCrCardNo.Focus();
                            }
                        }
                        else
                        {
                            GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                            GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dtPayTypeLedger.Rows[0].ItemArray[1].ToString();
                            GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;
                            pnlPaytype.Visible = false;
                            BtnSave.Enabled = true;
                            GridView.Focus();
                            if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                                GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                        }

                    }
                    else
                    {
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dtPayTypeLedger.Rows[0].ItemArray[1].ToString();
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;
                        pnlPaytype.Visible = false;
                        BtnSave.Enabled = true;
                        GridView.Focus();
                        if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                            GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                    }

                    //manaliend
                    //if (lstPayType.SelectedValue.ToString() == "4")
                    //{
                    //    int y = pnlMain.Location.Y + p.Location.Y + GridView.Location.Y;
                    //    y = y + ((p.Height - GridView.Height) / 2);
                    //    int x = (GridView.Width - pnlchq.Width) / 2;
                    //    pnlchq.Location = new Point(GridView.Location.X + x, y + 10);

                    //    pnlPaytype.Visible = false;
                    //    pnlchq.Visible = true;
                    //    BtnSave.Enabled = false;
                    //    txtChqNo.Focus();
                    //}
                    //else if (lstPayType.SelectedValue.ToString() == "5")
                    //{
                    //    int y = pnlMain.Location.Y + p.Location.Y + GridView.Location.Y;
                    //    y = y + ((p.Height - GridView.Height) / 2);
                    //    int x = (GridView.Width - pnlCredit.Width) / 2;
                    //    pnlCredit.Location = new Point(GridView.Location.X + x, y + 10);
                    //    BtnSave.Enabled = false;
                    //    pnlPaytype.Visible = false;
                    //    pnlCredit.Visible = true;
                    //    txtCrCardNo.Focus();
                    //}
                    //else
                    //{
                    //    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                    //    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dtPayTypeLedger.Rows[0].ItemArray[1].ToString();
                    //    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;
                    //    pnlPaytype.Visible = false;
                    //    BtnSave.Enabled = true;
                    //    GridView.Focus();
                    //    if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                    //        GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                    //}
                }
                else if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
                {
                    pnlPaytype.Visible = false;
                    GridView.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnchqOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChqValidations() == true)
                {
                    BtnSave.Enabled = true;

                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtChqNo.Text;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = dtpChqDate.Value;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                    if (VoucherType == VchType.Sales)
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dtPayTypeLedger.Rows[1].ItemArray[1].ToString();
                    else
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbBank);

                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;

                    pnlchq.Visible = false;
                    GridView.Focus();
                    if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                        GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnchqCancel_Click(object sender, EventArgs e)
        {
            pnlchq.Visible = false;
            BtnSave.Enabled = true;
            txtChqNo.Text = "";
            cmbBank.SelectedIndex = 0;
            cmbBranch.SelectedIndex = 0;
            EP.SetError(txtChqNo, "");

            GridView.Focus();
            if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
            else
                GridView.CurrentCell = GridView[ColIndex.Amount, GridView.Rows.Count - 1];

        }

        private bool ChqValidations()
        {
            bool tempflag = false;
            try
            {
                EP.SetError(txtChqNo, "");
                EP.SetError(cmbBank, "");
                EP.SetError(cmbBranch, "");

                if (txtChqNo.Text.Trim() == "")
                {
                    EP.SetError(txtChqNo, "Enter Cheque No");
                    EP.SetIconAlignment(txtChqNo, ErrorIconAlignment.MiddleRight);
                    txtChqNo.Focus();
                }
                else if (Convert.ToInt64(cmbBank.SelectedValue) == 0)
                {
                    EP.SetError(cmbBank, "Select Bank");
                    EP.SetIconAlignment(cmbBank, ErrorIconAlignment.MiddleRight);
                    cmbBank.Focus();
                }
                else if (Convert.ToInt64(cmbBranch.SelectedValue) == 0)
                {
                    if (VoucherType == VchType.Sales)
                    {
                        EP.SetError(cmbBranch, "Select Branch");
                        EP.SetIconAlignment(cmbBranch, ErrorIconAlignment.MiddleRight);
                        cmbBranch.Focus();
                    }
                    else if (VoucherType == VchType.Purchase)
                        tempflag = true;
                }
                else
                    tempflag = true;
                return tempflag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.BillDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");

            }
        }

        private void GridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowCount = GridView.CurrentCell.RowIndex;
                GridView.CurrentCell.ErrorText = "";

                if (GridView.CurrentCell.ColumnIndex == ColIndex.Amount)
                {
                    if (GridView.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                            GridView.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else if (Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.NetBal].Value) < Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.Amount].Value))
                            GridView.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount, ColIndex.PayType, GridView });
                            //GridView.CurrentCell = GridView[ColIndex.PayType, rowCount];
                        }

                    }
                    else
                    {
                        GridView.CurrentCell.Value = "0.00";
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { rowCount, ColIndex.PayType, GridView });
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        private void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbPartyName.SelectedValue = "0";
            DataClrscr();
            cmbPartyName.Focus();
        }

        private void btnCrOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (CreditValidations() == true)
                {
                    BtnSave.Enabled = true;

                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtCrCardNo.Text;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = Convert.ToDateTime("01-01-1900");
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbCrBank.SelectedValue;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbCrBranch.SelectedValue;

                    if (VoucherType == VchType.Sales)
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dtPayTypeLedger.Rows[2].ItemArray[1].ToString();
                    else
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCrBank);

                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                    pnlCredit.Visible = false;
                    GridView.Focus();
                    if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                        GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCrCancel_Click(object sender, EventArgs e)
        {
            pnlCredit.Visible = false;
            txtCrCardNo.Text = "";
            cmbCrBank.SelectedIndex = 0;
            cmbCrBranch.SelectedIndex = 0;
            EP.SetError(txtCrCardNo, "");
            //GridView.Focus();

            GridView.Focus();
            if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
            else
                GridView.CurrentCell = GridView[ColIndex.Amount, GridView.Rows.Count - 1];

        }

        private bool CreditValidations()
        {
            bool tempflag = false;
            try
            {
                EP.SetError(txtCrCardNo, "");
                EP.SetError(cmbCrBank, "");
                EP.SetError(cmbCrBranch, "");

                if (txtCrCardNo.Text.Trim() == "")
                {
                    EP.SetError(txtCrCardNo, "Enter CreditCardNo");
                    EP.SetIconAlignment(txtCrCardNo, ErrorIconAlignment.MiddleRight);
                    txtCrCardNo.Focus();
                }
                else if (Convert.ToInt64(cmbCrBank.SelectedValue) == 0)
                {
                    EP.SetError(cmbCrBank, "Select Bank");
                    EP.SetIconAlignment(cmbCrBank, ErrorIconAlignment.MiddleRight);
                    cmbCrBank.Focus();
                }
                else if (Convert.ToInt64(cmbCrBranch.SelectedValue) == 0)
                {
                    if (VoucherType == VchType.Sales)
                    {
                        EP.SetError(cmbCrBranch, "Select Branch");
                        EP.SetIconAlignment(cmbCrBranch, ErrorIconAlignment.MiddleRight);
                        cmbCrBranch.Focus();
                    }
                    else if (VoucherType == VchType.Purchase)
                        tempflag = true;
                }
                else
                    tempflag = true;
                return tempflag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void BindRecords()
        {
            try
            {
                while (dgDataRecord.Rows.Count > 0)
                    dgDataRecord.Rows.RemoveAt(0);
                pnlRecord.Visible = false;
                DataTable dt = new DataTable();
                DataGridView gv = null;
                gv = GridView;
                if (gv.Rows.Count > 0)
                {
                    if (VoucherType == VchType.Sales)
                    {
                        //DataTable dt = ObjFunction.GetDataView("Exec GetCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VoucherType + "," + DBGetVal.FirmNo + "").Table;
                        dt = ObjFunction.GetDataView("SELECT TVoucherEntry.VoucherDate, TVoucherRefDetails.Amount,  Case When(TVoucherEntry.VoucherTypeCode<>" + VchType.RejectionIn + ")Then MPayType.PayTypeName Else 'Against Sales Return' End AS PayTypeName , TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate, " +
                              " TVoucherChqCreditDetails.CreditCardNo,TVoucherEntry.PKVoucherNo,TVoucherRefDetails.PkRefTrnNo,TVoucherChqCreditDetails.PkSrNo " +
                              " FROM         TVoucherEntry INNER JOIN " +
                              " TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                              " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo LEFT OUTER JOIN " +
                              " TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo INNER JOIN " +
                              " MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherDetails.LedgerNo = " + gv.CurrentRow.Cells[ColIndex.MainLedgerNo].Value + ") AND (TVoucherEntry.VoucherTypeCode in( " + VchType.SalesReceipt + "," + VchType.RejectionIn + "))" +
                        "AND (TVoucherRefDetails.RefNo = " + gv.CurrentRow.Cells[ColIndex.RefNo].Value + ") AND " +
                        "(TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")  " +//ORDER BY TVoucherEntry.VoucherDate
                        " UNION " +
                        " SELECT TVoucherRefDetails.UserDate, TVoucherRefDetails.Amount,  'Against Op Balance' , '', '',  '',0,0,0 " +
                        " FROM TVoucherRefDetails   WHERE (TVoucherRefDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") AND TVoucherRefDetails.TypeOfRef=5 AND (TVoucherRefDetails.RefNo = " + gv.CurrentRow.Cells[ColIndex.RefNo].Value + ") " +
                        " AND (TVoucherRefDetails.CompanyNo = " + DBGetVal.FirmNo + ")  ORDER BY TVoucherRefDetails.PkRefTrnNo").Table;
                    }
                    else if (VoucherType == VchType.Purchase)
                    {
                        dt = ObjFunction.GetDataView("SELECT TVoucherEntry.VoucherDate, TVoucherRefDetails.Amount,  Case When(TVoucherEntry.VoucherTypeCode<>" + VchType.RejectionOut + ")Then MPayType.PayTypeName Else 'Against Purchase Return' End AS PayTypeName , TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate, " +
                              " TVoucherChqCreditDetails.CreditCardNo,TVoucherEntry.PKVoucherNo,TVoucherRefDetails.PkRefTrnNo,TVoucherChqCreditDetails.PkSrNo " +
                              " FROM         TVoucherEntry INNER JOIN " +
                              " TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                              " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo LEFT OUTER JOIN " +
                              " TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo INNER JOIN " +
                              " MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherDetails.LedgerNo = " + gv.CurrentRow.Cells[ColIndex.MainLedgerNo].Value + ") AND (TVoucherEntry.VoucherTypeCode in (" + VchType.PurchasePayment + "," + VchType.RejectionOut + "))" +
                        "AND (TVoucherRefDetails.RefNo = " + gv.CurrentRow.Cells[ColIndex.RefNo].Value + ") AND " +
                        "(TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") " +//ORDER BY TVoucherEntry.VoucherDate
                        " UNION " +
                        " SELECT TVoucherRefDetails.UserDate, TVoucherRefDetails.Amount,  'Against Op Balance' , '', '',  '',0,0,0 " +
                        " FROM TVoucherRefDetails   WHERE (TVoucherRefDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") AND TVoucherRefDetails.TypeOfRef=5 AND (TVoucherRefDetails.RefNo = " + gv.CurrentRow.Cells[ColIndex.RefNo].Value + ") " +
                        " AND (TVoucherRefDetails.CompanyNo = " + DBGetVal.FirmNo + ")  ORDER BY TVoucherRefDetails.PkRefTrnNo").Table;
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    if (VoucherType == VchType.Sales)
                        DisplayMessage("No Collection Entry Found");
                    else
                        DisplayMessage("No Payments Entry Found");
                }
                else
                {

                    pnlRecord.Visible = true;
                    dgDataRecord.DataSource = dt.DefaultView;
                    pnlRecord.Location = new System.Drawing.Point(10, 110);
                    dgDataRecord.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDataRecord.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDataRecord.Columns[6].Visible = false;
                    dgDataRecord.Columns[7].Visible = false;
                    dgDataRecord.Columns[8].Visible = false;
                    dgDataRecord.Columns[0].Width = 94;
                    dgDataRecord.Columns[1].Width = 67;
                    dgDataRecord.Columns[2].Width = 103;
                    dgDataRecord.Columns[3].Width = 79;
                    dgDataRecord.Columns[4].Width = 92;
                    dgDataRecord.Columns[5].Width = 100;

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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

        private void DataRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 4)
            {
                if (Convert.IsDBNull(e.Value) == false)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                    if (e.Value.ToString() == "01-Jan-1900")
                        e.Value = "";
                }
            }
        }

        private void DataRecord_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    if (pnlRecord.Visible == true)
                    {
                        pnlRecord.Visible = false;
                        if (pnlBillwise.Visible == true)
                        {
                            GridView.CurrentCell = GridView[ColIndex.TotRec, GridView.CurrentCell.RowIndex];
                            GridView.Focus();
                        }
                        else if (pnlBillselection.Visible == true)
                        {
                            dgBillSelection.CurrentCell = dgBillSelection[ColIndex.TotRec, dgBillSelection.CurrentCell.RowIndex];
                            dgBillSelection.Focus();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (dgDataRecord.CurrentRow.Index < 0)
                        return;
                    if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbTVoucherEntry = new DBTVaucherEntry();
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = Convert.ToInt64(dgDataRecord.CurrentRow.Cells[6].Value);
                        dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);
                        OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                        if (pnlRecord.Visible == true)
                        {
                            pnlRecord.Visible = false;
                            if (pnlBillwise.Visible == true)
                            {
                                GridView.CurrentCell = GridView[ColIndex.TotRec, GridView.CurrentCell.RowIndex];
                                GridView.Focus();
                            }
                            else if (pnlBillselection.Visible == true)
                            {
                                dgBillSelection.CurrentCell = dgBillSelection[ColIndex.TotRec, dgBillSelection.CurrentCell.RowIndex];
                                dgBillSelection.Focus();
                            }
                        }
                        cmbPartyName_Leave(sender, new EventArgs());
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (GridView.CurrentCell.ColumnIndex == ColIndex.Amount)
            {
                TextBox txtAmt = (TextBox)e.Control;
                txtAmt.TextChanged += new EventHandler(txtAmt_TextChanged);
            }
        }

        private void txtAmt_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMasked((TextBox)sender, 2);
            if (GridView.CurrentCell.ColumnIndex == ColIndex.Amount)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void btnAdvanceSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Master.AdvancedSearch Adsch = new Yadi.Master.AdvancedSearch(grpNo);
                ObjFunction.OpenForm(Adsch);
                if (Adsch.LedgerNo != 0)
                {
                    cmbPartyName.SelectedValue = Adsch.LedgerNo;
                    cmbPartyName.Focus();
                    Adsch.Close();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rbCheck_Changed()
        {
            try
            {
                lblPrint.Visible = false;

                label5.Visible = true;
                pnlBillwise.Visible = true;
                pnlBillselection.Visible = false;
                pnlBillwise.Location = new System.Drawing.Point(panel1.Location.X, 110);
                BtnSave.Enabled = true;
                lblPrint.Visible = true;
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    GridView.Rows[i].Cells[ColIndex.Amount].Value = "0.00";
                }
                //if (GridView.Rows.Count > 0)
                //    GridView.CurrentCell = GridView.Rows[0].Cells[5];
                //GridView.Focus();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void dgBillSelection_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    e.SuppressKeyPress = true;
                    BindRecords();
                    dgDataRecord.Focus();
                }

                else if (e.KeyCode == Keys.Space)
                {
                    if (dgBillSelection.CurrentCell == dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk])
                    {
                        if (Convert.ToBoolean(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].FormattedValue) == true)
                        {
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = false;
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = "0.00";
                        }
                        else
                        {
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = true;
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.NetBal].Value;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBillSelection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.BillDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
            }
        }

        private bool isProcessingPartyNameChange = false;
        private void cmbPartyName_Leave(object sender, EventArgs e)
        {
            if (isProcessingPartyNameChange == false)
            {
                isProcessingPartyNameChange = true;
                if (cmbPartyName.SelectedIndex != 0)
                {
                    processPartyNameChange();
                }
                else DataClrscr();
                isProcessingPartyNameChange = false;
            }
        }

        private void dgBillSelection_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txtAmt = (TextBox)e.Control;
            txtAmt.TextChanged += new EventHandler(txtAmt_TextChanged);
        }

        private void txtChqNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtChqNo);
        }

        private void cmbPartyName_MouseClick(object sender, MouseEventArgs e)
        {
            // if (ObjFunction.GetComboValue(cmbPartyName)>0)
            //   cmbPartyName_KeyDown(cmbPartyName, new KeyEventArgs(Keys.Enter));
        }

        private void txtCrCardNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtCrCardNo);
        }

        private void cmbBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (VoucherType == VchType.Purchase)
                {
                    btnchqOk.Focus();
                }
                else
                    cmbBranch.Focus();
            }
        }

        private void cmbCrBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (VoucherType == VchType.Purchase)
                {
                    btnCrOk.Focus();
                }
                else
                    cmbCrBranch.Focus();
            }
        }

        private void dgBillSelection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                rowCount = dgBillSelection.CurrentCell.RowIndex;
                dgBillSelection.CurrentCell.ErrorText = "";

                if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.Amount)
                {
                    if (dgBillSelection.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(dgBillSelection.CurrentCell.EditedFormattedValue.ToString()) == false)
                            dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.NetBal].Value) < Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value))
                            dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value) == 0)
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = false;
                        else
                        {
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = true;
                            MovetoNext move2n = new MovetoNext(m2n);
                            if (dgBillSelection.Rows.Count - 1 == rowCount)
                                BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, dgBillSelection });
                            else if (dgBillSelection.Rows.Count - 1 > rowCount)
                                BeginInvoke(move2n, new object[] { rowCount + 1, ColIndex.Amount, dgBillSelection });
                            //GridView.CurrentCell = GridView[ColIndex.PayType, rowCount];
                        }
                    }
                    //else if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.Chk)
                    //{

                    //    if (Convert.ToBoolean(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].EditedFormattedValue) == true)
                    //    {
                    //        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = false;
                    //        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = "0.00";
                    //    }
                    //    else
                    //    {
                    //        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = true;
                    //        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.NetBal].Value;
                    //    }
                    //}
                    else
                    {
                        dgBillSelection.CurrentCell.Value = "0.00";
                        dgBillSelection.CurrentRow.Cells[ColIndex.Chk].Value = false;
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, dgBillSelection });

                    }
                }
                else if (e.ColumnIndex == ColIndex.Chk)
                {
                    if (Convert.ToBoolean(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].FormattedValue) == true)
                    {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = false;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = "0.00";
                    }
                    else
                    {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = true;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.NetBal].Value;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataClrscr()
        {
            cmbPartyName.SelectedValue = "0";
            lblNetBal.Text = "";
            lblAmount.Text = "";

            GridView.Rows.Clear();
            dgBillSelection.Rows.Clear();
            pnlchq.Visible = false;
            pnlCredit.Visible = false;
            pnlPaytype.Visible = false;
            pnlRecord.Visible = false;
            BtnSave.Enabled = false;
            label5.Visible = false;


        }

        private void PrintOutstanding()
        {
            try
            {
                //string[] ReportSession;
                //Form NewF = null;

                //ReportSession = new string[5];
                //ReportSession[0] = ObjFunction.GetComboValue(cmbPartyName).ToString();
                //ReportSession[1] = VoucherType.ToString();
                //ReportSession[2] = DBGetVal.FirmNo.ToString();
                //if (VoucherType == VchType.Sales)
                //    ReportSession[3] = "Sales Outstanding Report";
                //else if (VoucherType == VchType.Purchase)
                //    ReportSession[3] = "Purchase Outstanding Report";
                //ReportSession[4] = cmbPartyName.Text;

                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //    NewF = new Display.ReportViewSource(new Reports.GetCollectionDetails(), ReportSession);
                //else
                //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetCollectionDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);




                Display.MultipleBillPrint Newf = new Yadi.Display.MultipleBillPrint(Convert.ToInt64(GridView.CurrentRow.Cells[ColIndex.MainLedgerNo].Value), VoucherType, DBGetVal.FirmNo, GridView.CurrentRow.Cells[ColIndex.LedgerName].Value.ToString());
                ObjFunction.OpenForm(Newf);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public static class CColIndex
        {
            public static int ItemNo = 0;
            public static int PkStockTrnNo = 1;
            public static int MfgCompNo = 2;
            public static int CompanyNo = 3;
            public static int Amount = 4;
            public static int SGSTAmount = 5;
        }

        private void setCompanyRatio(long RefNo)
        {
            try
            {

                long PkVoucherNo = ObjQry.ReturnLong("Select FkVoucherNo From TVoucherDetails Where PkVoucherTrnNo=(Select FkVoucherTrnno From TVoucherRefDetails Where RefNo=" + RefNo + " And TypeOfRef=3)", CommonFunctions.ConStr);

                dtBill = ObjFunction.GetDataView("SELECT  mItemMaster.ItemNo, TStock.PkStockTrnNo, mItemMaster.MfgCompNo, TStock.CompanyNo, TStock.Amount, tStock.SGSTAmount FROM  MStockItems INNER JOIN TStock ON mItemMaster.ItemNo = TStock.ItemNo WHERE     (TStock.FKVoucherNo = " + PkVoucherNo + ")").Table;
                if (dtBill.Rows.Count > 0)
                {
                    DataTable dt = ObjFunction.GetDataView("Select Case When Debit<>0 then Debit Else Credit End,LedgerNo,SrNo From TVoucherDetails Where FKVoucherNo=" + PkVoucherNo + " order by VoucherSrNo").Table;
                    DataTable dtTemp = new DataTable();
                    bool TempFlag = false;
                    dtTemp.Columns.Add();
                    dtTemp.Columns.Add();
                    DataRow dr = dtTemp.NewRow();
                    dr[0] = Convert.ToInt64(dtBill.Rows[0][CColIndex.CompanyNo].ToString());
                    dr[1] = Convert.ToInt64(dtBill.Rows[0][CColIndex.MfgCompNo].ToString());
                    dtTemp.Rows.Add(dr);
                    for (int k = 1; k < dtBill.Rows.Count; k++)
                    {
                        for (int i = 0; i < dtTemp.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dtBill.Rows[k][CColIndex.MfgCompNo].ToString()) != Convert.ToInt64(dtTemp.Rows[i].ItemArray[1].ToString()))
                            {
                                TempFlag = true;
                            }
                            else
                            {
                                TempFlag = false;
                                break;
                            }
                        }
                        if (TempFlag == true)
                        {
                            dr = dtTemp.NewRow();
                            dr[0] = Convert.ToInt64(dtBill.Rows[k][CColIndex.CompanyNo].ToString());
                            dr[1] = Convert.ToInt64(dtBill.Rows[k][CColIndex.MfgCompNo].ToString());
                            dtTemp.Rows.Add(dr);
                        }
                    }
                    double GrandTotal = 0, TotalAnotherDisc = 0, RoundOff = 0, TotalChrgs = 0, TotalTax = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[2].ToString() == Others.Party.ToString())
                        {
                            GrandTotal = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString());
                        }
                        if (dt.Rows[i].ItemArray[2].ToString() == Others.RoundOff.ToString())
                        {
                            RoundOff = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString());
                        }
                        else if (dt.Rows[i].ItemArray[2].ToString() == Others.Discount3.ToString())
                        {
                            TotalAnotherDisc = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString());
                        }
                        else if (dt.Rows[i].ItemArray[2].ToString() == Others.Charges1.ToString())
                        {
                            TotalChrgs = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString());
                        }
                    }
                    TotalTax = Convert.ToDouble(dtBill.Compute("Sum(TaxAmount)", ""));


                    dtCompRatio = new DataTable();
                    dtCompRatio.Columns.Add();
                    dtCompRatio.Columns.Add();
                    dtCompRatio.Columns.Add();
                    double debit = 0;
                    for (int k = 0; k < dtTemp.Rows.Count; k++)
                    {
                        for (int j = 0; j < dtBill.Rows.Count; j++)
                        {
                            if (Convert.ToInt64(dtBill.Rows[j][CColIndex.MfgCompNo].ToString()) == Convert.ToInt64(dtTemp.Rows[k].ItemArray[1].ToString()))
                            {
                                debit = debit + Convert.ToDouble(dtBill.Rows[j][CColIndex.Amount].ToString());
                            }
                        }

                        DataRow dr1 = dtCompRatio.NewRow();
                        dr1[0] = Convert.ToInt64(dtTemp.Rows[k].ItemArray[0].ToString());
                        dr1[1] = Convert.ToInt64(dtTemp.Rows[k].ItemArray[1].ToString());
                        if (debit > 0)
                            dr1[2] = (debit * 10) / ((GrandTotal + TotalAnotherDisc - RoundOff - TotalChrgs - TotalTax));
                        else dr1[2] = 1;
                        dtCompRatio.Rows.Add(dr1);
                        debit = 0;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SetVoucherCompany(TVoucherEntry tVouch)
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

        private void SetVoucherDetailsCompany(TVoucherDetails tVouch)
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
}


