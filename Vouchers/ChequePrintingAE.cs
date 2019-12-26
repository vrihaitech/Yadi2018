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

    public partial class ChequePrintingAE : Form
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
        TChequePrinting tCheque = new TChequePrinting();

        DataTable dtSearch = new DataTable();
        DataTable dtPayTypeLedger = new DataTable();
        int rowCount, cntRow, amtflag = 0;
        long VoucherType = 0, grpNo;
        double OpBalPendingAmt = 0, TotalDisc=0.0;
        long ID = 0, tempID = 0;

        DataTable dtCompRatio = new DataTable(), dtBill;

        public ChequePrintingAE()
        {
            InitializeComponent();
            this.VoucherType = OM.VchType.Sales;
        }

        public ChequePrintingAE(long VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
            if (VoucherType == VchType.Purchase)
            {
                this.Text = "Cheque Printing";
                grpNo = GroupType.SundryCreditors;
                txtDisc1.Text = "0.00";
                txtDisc2.Text = "0.00";
            }
        }

        private void SalesVoucherAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                dtpVoucherDate.Value = DBGetVal.ServerTime;
                ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo =" + grpNo + " order by LedgerName");
                dtPayTypeLedger = ObjFunction.GetDataView("SELECT     MPayTypeLedger.PayTypeNo, MPayTypeLedger.LedgerNo, MPayType.PayTypeName " +
                                                           " FROM MPayTypeLedger INNER JOIN MPayType ON MPayTypeLedger.PayTypeNo = MPayType.PKPayTypeNo " +
                                                            "  where MPayTypeLedger.CompanyNo=" + DBGetVal.FirmNo + " and MPayType.PKPayTypeNo not in (1,3) order by PayTypeName").Table;


                if (VoucherType == VchType.Sales)
                    ObjFunction.FillCombo(cmbBank, "Select BankNo,BankName From MOtherBank where IsActive='true' order by BankName");
                else
                    ObjFunction.FillCombo(cmbBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + " And IsActive='true' order by LedgerName");


                cmbPartyName.Focus();
                //lblNetBal.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblCp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lbldp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblDrBalp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblNetBp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblCrNetp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblNp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblopp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblOpBalp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblCancel.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblCancel.ForeColor = Color.Red;
                label5.Visible = false;

                rbCheck_Changed();

                dtSearch = ObjFunction.GetDataView("Select PkSrNo From TChequePrinting order by PrintingDate").Table;

                if (dtSearch.Rows.Count > 0 && ID == 0)
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    FillControls();
                    SetNavigation();
                    setDisplay(true);
                }

                btnNew.Focus();
                KeyDownFormat(this.Controls);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillControls()
        {
            tCheque = dbTVoucherEntry.ModifyTChequePrintingByID(ID);
            txtNo.Text = tCheque.PrintingUserNo.ToString();
            dtpVoucherDate.Value = tCheque.PrintingDate;
            if (tCheque.IsPurchase == true)
                rdPurchase.Checked = true;
            else
                rdOther.Checked = true;
            rdPurchase_CheckedChanged(rdPurchase, new EventArgs());

            cmbPartyName.SelectedValue = tCheque.LedgerNo.ToString();
            cmbBank.SelectedValue = tCheque.ByLedgerNo.ToString();
            ObjFunction.FillCombo(cmbChqNo, "SELECT TChequeNoDetails.PkSrNo, TChequeNoDetails.ChequeDetailsUserNo FROM TChequeNo INNER JOIN " +
               " TChequeNoDetails ON TChequeNo.PkChequeNo = TChequeNoDetails.FkChequeNo WHERE (TChequeNoDetails.IsActive = 'true') " +
               " AND (TChequeNoDetails.FkVoucherTrnNo = 0) AND (TChequeNo.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") OR TChequeNoDetails.PkSrNo=" + tCheque.FKChequeNo + "  ", "ECS");

            cmbChqNo.SelectedValue = tCheque.FKChequeNo.ToString();
            dtpChqDate.Value = tCheque.ChequeDate;
            txtAmount.Text = ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where PkVoucherNo=" + tCheque.FKVoucherNo + "", CommonFunctions.ConStr).ToString("0.00");
            txtRemark1.Text = tCheque.Remark1;
            txtRemark2.Text = tCheque.Remark2;
            txtRemark3.Text = tCheque.Remark3;
            cmbChequeName.Text = tCheque.Remark3;
            txtStatus.Text = "Issued";
            dtpBankDate.Value = tCheque.BankDate;
            lblCancel.Visible = ObjQry.ReturnBoolean("Select IsCancel From TVoucherEntry Where PKVoucherNo=" + tCheque.FKVoucherNo + "", CommonFunctions.ConStr);
            FillTodaysCheque();
        }

        public void InitControls()
        {
            try
            {
                GridView.Rows.Clear();
                pnlDetailsPur.Visible = false;
                ID = 0;
                lblDrBalp.Text = "0.00";
                lblCrNetp.Text = "0.00";
                lblNetBp.Text = "0.00";
                lblOpBalp.Text = "0.00";
                lblOpStatusPur.Text = "To Pay";

                if (Convert.ToDouble(lblNetBp.Text) < 0)
                    lblPayStatusPur.Text = "To Receive";
                else if (Convert.ToDouble(lblNetBp.Text) > 0)
                    lblPayStatusPur.Text = "To Pay";
                else
                    lblPayStatusPur.Text = "";
                lblNetBp.Text = "0.00";

                txtAmount.Text = "0.00";
                txtDisc1.Text = "0.00";
                txtDisc2.Text = "0.00";
                amtflag = 0;
                txtNo.Text = (ObjQry.ReturnLong("Select IsNull(Max(PkSrno),0) From TChequePrinting ", CommonFunctions.ConStr) + 1).ToString();

                GridView.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridView.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridView.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridView.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                rdPurchase.Checked = true;
                txtAmount.Enabled = false;
                DataClrscr();
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

                    double NetBal = 0;

                    pnlDetailsPur.Visible = true;
                    DataTable dt = ObjFunction.GetDataView("Exec GetCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VoucherType + "," + DBGetVal.FirmNo + "").Table;

                    for (int i =0; i < dt.Rows.Count; i++)
                    {
                        GridView.Rows.Add();
                        //==========umesh  for (int j = 0; j < dt.Columns.Count-1 ; j++)
                        for (int j = 0; j < dt.Columns.Count - 1; j++)
                        { 
                            GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                        }
                        NetBal = NetBal + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                    }
                    double OpBalance = ObjQry.ReturnDouble("Select OpAmt from GetOpeningLedgerBalanceOnly(" + ObjFunction.GetComboValue(cmbPartyName) + "," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                    DataTable dtDetails = ObjFunction.GetDataView("SELECT isnull(SUM(TVoucherDetails.Debit),0.00) AS 'Dr Bal', " +
                        " isnull( SUM(TVoucherDetails.Credit),0.00) AS 'Cr Bal' " +
                        " FROM TVoucherDetails INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                        " INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                        " WHERE (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") " +
                        " AND (TVoucherEntry.IsCancel = 0) AND (TVoucherEntry.VoucherTypeCode <> " + VchType.OpeningBalance + ") " +
                        " ").Table;

                    lblDrBalp.Text = "0.00";
                    lblCrNetp.Text = "0.00";
                    lblNetBp.Text = "0.00";

                    lblOpBalp.Text = Math.Abs(OpBalance).ToString("0.00");
                    if (Convert.ToDouble(OpBalance) > 0)
                        lblOpStatusPur.Text = "To Receive";
                    else if (Convert.ToDouble(OpBalance) < 0)
                        lblOpStatusPur.Text = "To Pay";
                    else
                        lblOpStatusPur.Text = "";


                    if (dtDetails.Rows.Count > 0)
                    {
                        lblAmount.Text = NetBal.ToString("0.00");

                        //lblOpBal.Text = dtDetails.Rows[0].ItemArray[2].ToString();
                        lblDrBalp.Text = dtDetails.Rows[0].ItemArray[0].ToString();
                        lblCrNetp.Text = dtDetails.Rows[0].ItemArray[1].ToString();

                        lblNetBp.Text = (OpBalance +
                            Convert.ToDouble(dtDetails.Rows[0].ItemArray[0].ToString()) -
                            Convert.ToDouble(dtDetails.Rows[0].ItemArray[1].ToString())).ToString("0.00");
                        if (Convert.ToDouble(lblNetBp.Text) < 0)
                            lblPayStatusPur.Text = "To Pay";
                        else if (Convert.ToDouble(lblNetBp.Text) > 0)
                            lblPayStatusPur.Text = "To Receive";
                        else
                            lblPayStatusPur.Text = "";
                        lblNetBp.Text = Math.Abs(Convert.ToDouble(lblNetBp.Text)).ToString("0.00");

                    }
                    else
                    {
                        lblNetBp.Text = Math.Abs(OpBalance).ToString("0.00");
                        lblPayStatusPur.Text = lblOpStatusPur.Text;
                    }

                    double OpBal = 0;
                    OpBal = OpBalance * -1;
                    double TotBal = 0;
                    //In below query we used reverse logic of signcode as this is adjsted entry's values
                    TotBal = OpBal + ObjQry.ReturnDouble("Select isNull(Sum(case when (signcode=1) then isNull(Amount,0) else isNull(Amount,0)*-1 end),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=5 AND REFNO <> 0", CommonFunctions.ConStr);
                    //In below query we used forward logic of signcode as this is direct entry's values
                    TotBal = TotBal + ObjQry.ReturnDouble("Select isNull(Sum(case when (signcode=2) then isNull(Amount,0) else isNull(Amount,0)*-1 end),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=5 AND REFNO = 0", CommonFunctions.ConStr);

                    OpBalPendingAmt = Math.Abs(TotBal);

                    GridView.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                }
                else
                    DataClrscr();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void FillPrintChequeName()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("ID");
            DataRow dr = null;
            dr = dt.NewRow();
            dr[1] = cmbPartyName.Text;
            dr[0] = 0;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[1] = "YourSelf For RTGS/NEFT";
            dr[0] = 1;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[1] = "Self";
            dr[0] = 2;
            dt.Rows.Add(dr);
            cmbChequeName.DataSource = dt.DefaultView;
            cmbChequeName.DisplayMember = dt.Columns[1].ColumnName;
            cmbChequeName.ValueMember = dt.Columns[0].ColumnName;
            //txtRemark3.Text = cmbChequeName.Text;

        }
        private bool Validations()
        {
            bool flag = true;
            try
            {
                if (Convert.ToDouble(txtAmount.Text) == 0)
                {
                    flag = false;
                    OMMessageBox.Show("Enter valid amount.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else if (ObjFunction.GetComboValue(cmbChqNo) == 0)
                {
                    flag = false;
                    OMMessageBox.Show("Select Cheque No.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else if (ObjFunction.GetComboValue(cmbBank) == 0)
                {
                    flag = false;
                    OMMessageBox.Show("Select Bank Name.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }

                if (flag == true)
                {
                    if (GridView.Rows.Count < 0)
                    {
                        OMMessageBox.Show("Sorry No Data to Save......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        flag = false;
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
                                break;
                            }
                        }

                    }
                }

                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool ValidationChqNCreditCard()
        {
            bool flag = true;

            if (ObjFunction.GetComboValue(cmbChqNo) == 0)
            {
                flag = false;
                OMMessageBox.Show("Select Cheque No.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
            else if (ObjFunction.GetComboValue(cmbBank) == 0)
            {
                flag = false;
                OMMessageBox.Show("Select Bank Name.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
            else
                flag = true;


            return flag;
        }

        private void setValues()
        {
            try
            {
                if (cmbPartyName.SelectedIndex != 0)
                {
                    bool flag = false;
                    if (Validations() == true)
                    {
                        dbTVoucherEntry = new DBTVaucherEntry();
                        #region  Voucher Entry
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = ObjQry.ReturnLong("Select FKVoucherNo FRom TChequePrinting Where PkSrNo=" + ID + "", CommonFunctions.ConStr);
                        if (VoucherType == VchType.Sales)
                            tVoucherEntry.VoucherTypeCode = VchType.SalesReceipt;
                        else
                            tVoucherEntry.VoucherTypeCode = VchType.PurchasePayment;
                        tVoucherEntry.VoucherUserNo = 0;
                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                        tVoucherEntry.VoucherTime = Convert.ToDateTime(dtpVoucherDate.Value);
                        tVoucherEntry.Reference = "";

                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                        tVoucherEntry.BilledAmount = Convert.ToDouble(txtAmount.Text);// + TotalDisc;
                        tVoucherEntry.ChallanNo = "";
                        if (amtflag==1)
                        {
                            tVoucherEntry.Remark = "On Account";
                        }
                        else
                        {
                            tVoucherEntry.Remark = "Payment";
                        }

                        tVoucherEntry.ChequeNo = 0;
                        //  tVoucherEntry.ChequeNo = Convert.ToInt64(cmbChqNo.Text);
                        tVoucherEntry.ClearingDate = Convert.ToDateTime(dtpVoucherDate.Text);
                        tVoucherEntry.Narration = "Cheque Printing";
                        tVoucherEntry.UserID = DBGetVal.UserID;
                        tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                        tVoucherEntry.OrderType = 1;
                        tVoucherEntry.PayTypeNo = 4;//for Cheque
                        tVoucherEntry.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);

                        tVoucherEntry.TransporterCode = 0;
                        tVoucherEntry.TransPayType = 0;
                        tVoucherEntry.LRNo = "";
                        tVoucherEntry.TransportMode = 0;
                        tVoucherEntry.TransNoOfItems = 0;

                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);

                        #endregion

                        #region VoucherDetails Party
                        DataTable dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + tVoucherEntry.PkVoucherNo + " order by VoucherSrNo").Table;
                        int VoucherSrNo = 1;
                        if (VoucherType == VchType.Sales)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            tVoucherDetails.VoucherSrNo = 1; VoucherSrNo = VoucherSrNo + 1;
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                            tVoucherDetails.Debit = 0;
                            tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text); ;
                            tVoucherDetails.SrNo = Others.Party;
                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            tVoucherDetails.Narration = "";
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);
                            if (ObjFunction.GetComboValue(cmbChqNo) > 0)
                                dbTVoucherEntry.UpdateChequeDetails(ObjFunction.GetComboValue(cmbChqNo));
                        }
                        else if (VoucherType == VchType.Purchase)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            tVoucherDetails.VoucherSrNo = 1; VoucherSrNo = VoucherSrNo + 1;
                            tVoucherDetails.SignCode = 1;
                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                            tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text);// + TotalDisc; ;
                            tVoucherDetails.Credit = 0;
                            tVoucherDetails.SrNo = Others.Party;
                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            tVoucherDetails.Narration = "";
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                            if (ObjFunction.GetComboValue(cmbChqNo) > 0)
                                dbTVoucherEntry.UpdateChequeDetails(ObjFunction.GetComboValue(cmbChqNo));

                            //if (TotalDisc != 0)
                            //{
                            //    tVoucherDetails = new TVoucherDetails();
                            //    tVoucherDetails.PkVoucherTrnNo = 0;
                            //    tVoucherDetails.VoucherSrNo = 3;
                            //    tVoucherDetails.SignCode = 2;
                            //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                            //    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                            //    tVoucherDetails.Credit = TotalDisc; //0;
                            //    tVoucherDetails.SrNo = Others.Discount1;
                            //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            //    tVoucherDetails.Narration = "";
                            //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry

                            //}
                        }
                        #endregion

                        #region Voucher Ref DEtails
                        int cnt = 0;
                        if (ID == 0 && rdPurchase.Checked == true)
                        {
                            if (amtflag == 0)
                            {
                                for (int j = 0; j < GridView.RowCount; j++)
                                {

                                    if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value.ToString()) != 0)
                                    {
                                        cnt++;
                                        // setCompanyRatio(Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value.ToString()));
                                        long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value.ToString()) + "", CommonFunctions.ConStr);

                                        tVchRefDtls = new TVoucherRefDetails();
                                        tVchRefDtls.PkRefTrnNo = 0;
                                        tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                        tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                        tVchRefDtls.TypeOfRef = 2;
                                        tVchRefDtls.RefNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value);
                                        tVchRefDtls.DueDays = 0;
                                        tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                        tVchRefDtls.Amount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                        tVchRefDtls.DiscAmt = 0;// TotalDisc;
                                        if (VoucherType == VchType.Sales)
                                            tVchRefDtls.SignCode = 2;
                                        else
                                            tVchRefDtls.SignCode = 1;

                                        tVchRefDtls.UserID = DBGetVal.UserID;
                                        tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                        tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                        dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);
                                    }

                                }
                            } // amtflag 
                        }

                        else cnt = 1;

                        #endregion

                        #region Cheque Details
                        tVchChqCredit.PkSrNo = ObjQry.ReturnLong("Select PkSRNo From TVoucherChqCreditDetails Where FKVoucherNo=" + tVoucherEntry.PkVoucherNo + "", CommonFunctions.ConStr);
                        tVchChqCredit.ChequeNo = cmbChqNo.Text;
                        tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                        tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                        tVchChqCredit.BranchNo = 0;
                        tVchChqCredit.CreditCardNo = "";
                        tVchChqCredit.Amount = Convert.ToDouble(txtAmount.Text);
                        tVchChqCredit.PostFkVoucherNo = 0;
                        tVchChqCredit.PostFkVoucherTrnNo = 0;
                        tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);

                        #endregion

                        #region Bank Voucher Details
                        if (VoucherType == VchType.Sales)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            tVoucherDetails.VoucherSrNo = 2; VoucherSrNo = VoucherSrNo + 1;
                            tVoucherDetails.SignCode = 1;
                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbBank);
                            tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text);
                            tVoucherDetails.Credit = 0;
                            tVoucherDetails.SrNo = 0;
                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            tVoucherDetails.Narration = "";
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                        }
                        else if (VoucherType == VchType.Purchase)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            tVoucherDetails.VoucherSrNo = 2; VoucherSrNo = VoucherSrNo + 1;
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbBank);
                            tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                            tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text);
                            tVoucherDetails.SrNo = 0;
                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            tVoucherDetails.Narration = "";
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                        }

                        #endregion
 
                        #region Cheque Printings
                        tCheque = new TChequePrinting();
                        tCheque.PkSrNo = ID;
                        tCheque.PrintingUserNo = Convert.ToInt64(txtNo.Text);
                        tCheque.PrintingDate = Convert.ToDateTime(dtpVoucherDate.Text);
                        tCheque.IsPurchase = rdPurchase.Checked;
                        tCheque.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                        tCheque.ByLedgerNo = ObjFunction.GetComboValue(cmbBank);
                        tCheque.ChequeNo = cmbChqNo.Text;
                        tCheque.FKChequeNo = ObjFunction.GetComboValue(cmbChqNo);
                        tCheque.ChequeDate = Convert.ToDateTime(dtpChqDate.Text);
                        tCheque.Remark1 = txtRemark1.Text;
                        tCheque.Remark2 = txtRemark2.Text;
                        //tCheque.Remark3 = cmbChequeName.Text;
                        tCheque.Remark3 = txtRemark3.Text;
                        tCheque.ChqStatusNo = 1;
                        tCheque.BankDate = Convert.ToDateTime(dtpBankDate.Text);
                        tCheque.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTChequePrinting(tCheque);
                        #endregion

                        if (cnt > 0 || amtflag== 1)
                        {
                            if (dbTVoucherEntry.ExecuteNonQueryStatements() != 0)
                                flag = true;
                            else
                                flag = false;
                        }
                        else
                           if (amtflag==0)
                        {

                            flag = false;
                        }
                        else
                        {
                            flag = true;

                        }
                        if (rdPurchase.Checked == true)
                        {if (amtflag==0)
                            setValuesDisc(); }
                    }   // validations 
                    else
                    {
                        OMMessageBox.Show("Please Enter Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                    if (flag == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Voucher Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            tempID = ObjQry.ReturnLong("Select Max(PkSRno) FRom TChequePrinting ", CommonFunctions.ConStr);
                            DataRow drSearch = dtSearch.NewRow();
                            drSearch[0] = tempID;
                            dtSearch.Rows.Add(drSearch);
                            ID = tempID;

                            if (OMMessageBox.Show("Are you sure want to print cheque details ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                                PrintCheque();
                        }
                        else
                            OMMessageBox.Show("Voucher Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        setDisplay(true);
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        GridView.DataSource = null;
                        FillControls();
                        btnNew.Focus();
                    }
                    else
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
        private void setValuesDisc()
        {
            try
            {
                if (cmbPartyName.SelectedIndex != 0)
                {
                    bool flag = false;
                    if (TotalDisc > 0)
                    {
                        dbTVoucherEntry = new DBTVaucherEntry();
                        #region  Voucher Entry
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = ObjQry.ReturnLong("Select FKVoucherNo FRom TChequePrinting Where PkSrNo=" + ID + "", CommonFunctions.ConStr);
                        if (VoucherType == VchType.Sales)
                            tVoucherEntry.VoucherTypeCode = VchType.SalesReceipt;
                        else
                            tVoucherEntry.VoucherTypeCode = VchType.PurchasePayment;
                        tVoucherEntry.VoucherUserNo = 0;
                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                        tVoucherEntry.VoucherTime = Convert.ToDateTime(dtpVoucherDate.Value);
                        tVoucherEntry.Reference = "";

                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                        tVoucherEntry.BilledAmount = TotalDisc;// Convert.ToDouble(txtAmount.Text) + TotalDisc;
                        tVoucherEntry.ChallanNo = "";
                        tVoucherEntry.Remark = "";

                        tVoucherEntry.ChequeNo = 0;
                        //  tVoucherEntry.ChequeNo = Convert.ToInt64(cmbChqNo.Text);
                        tVoucherEntry.ClearingDate = Convert.ToDateTime(dtpVoucherDate.Text);
                        tVoucherEntry.Narration = "Discount Cheque Printing";
                        tVoucherEntry.UserID = DBGetVal.UserID;
                        tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                        tVoucherEntry.OrderType = 1;
                        tVoucherEntry.PayTypeNo = 4;//for Cheque
                        tVoucherEntry.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);

                        tVoucherEntry.TransporterCode = 0;
                        tVoucherEntry.TransPayType = 0;
                        tVoucherEntry.LRNo = "";
                        tVoucherEntry.TransportMode = 0;
                        tVoucherEntry.TransNoOfItems = 0;

                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);

                        #endregion

                        #region VoucherDetails Party
                        DataTable dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + tVoucherEntry.PkVoucherNo + " order by VoucherSrNo").Table;
                        int VoucherSrNo = 1;
                        if (VoucherType == VchType.Sales)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            tVoucherDetails.VoucherSrNo = 1; VoucherSrNo = VoucherSrNo + 1;
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                            tVoucherDetails.Debit = 0;
                            tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text); ;
                            tVoucherDetails.SrNo = Others.Party;
                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            tVoucherDetails.Narration = "";
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);
                            if (ObjFunction.GetComboValue(cmbChqNo) > 0)
                                dbTVoucherEntry.UpdateChequeDetails(ObjFunction.GetComboValue(cmbChqNo));
                        }
                        else if (VoucherType == VchType.Purchase)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            tVoucherDetails.VoucherSrNo = 1; VoucherSrNo = VoucherSrNo + 1;
                            tVoucherDetails.SignCode = 1;
                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                            tVoucherDetails.Debit = TotalDisc;// Convert.ToDouble(txtAmount.Text) + TotalDisc; ;
                            tVoucherDetails.Credit = 0;
                            tVoucherDetails.SrNo = Others.Party;
                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            tVoucherDetails.Narration = "";
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                            //if (ObjFunction.GetComboValue(cmbChqNo) > 0)
                            //    dbTVoucherEntry.UpdateChequeDetails(ObjFunction.GetComboValue(cmbChqNo));

                            if (TotalDisc != 0)
                            {
                                tVoucherDetails = new TVoucherDetails();
                                tVoucherDetails.PkVoucherTrnNo = 0;
                                tVoucherDetails.VoucherSrNo = 3;
                                tVoucherDetails.SignCode = 2;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                                tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                tVoucherDetails.Credit = TotalDisc; //0;
                                tVoucherDetails.SrNo = Others.Discount1;
                                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                tVoucherDetails.Narration = "";
                                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry

                            }
                        }
                        #endregion

                        #region Voucher Ref DEtails
                        int cnt = 0;
                        if (ID == 0 && rdPurchase.Checked == true)
                        {
                            for (int j = 0; j < GridView.RowCount; j++)
                            {

                                if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Disc1].Value.ToString()) != 0)
                                {
                                    cnt++;
                                    // setCompanyRatio(Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value.ToString()));
                                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value.ToString()) + "", CommonFunctions.ConStr);

                                    tVchRefDtls = new TVoucherRefDetails();
                                    tVchRefDtls.PkRefTrnNo = 0;
                                    tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                    tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                    tVchRefDtls.TypeOfRef = 2;
                                    tVchRefDtls.RefNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value);
                                    tVchRefDtls.DueDays = 0;
                                    tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                    tVchRefDtls.Amount = 0;// convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVchRefDtls.DiscAmt = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Disc1].Value);//TotalDisc;
                                    if (VoucherType == VchType.Sales)
                                        tVchRefDtls.SignCode = 2;
                                    else
                                        tVchRefDtls.SignCode = 1;

                                    tVchRefDtls.UserID = DBGetVal.UserID;
                                    tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                    tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);
                                }

                            }
                        }
                        else cnt = 1;

                        #endregion

                        #region Cheque Details
                        //tVchChqCredit.PkSrNo = ObjQry.ReturnLong("Select PkSRNo From TVoucherChqCreditDetails Where FKVoucherNo=" + tVoucherEntry.PkVoucherNo + "", CommonFunctions.ConStr);
                        //tVchChqCredit.ChequeNo = cmbChqNo.Text;
                        //tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                        //tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                        //tVchChqCredit.BranchNo = 0;
                        //tVchChqCredit.CreditCardNo = "";
                        //tVchChqCredit.Amount = Convert.ToDouble(txtAmount.Text);
                        //tVchChqCredit.PostFkVoucherNo = 0;
                        //tVchChqCredit.PostFkVoucherTrnNo = 0;
                        //tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                        //dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);

                        #endregion

                        #region Bank Voucher Details
                        //if (VoucherType == VchType.Sales)
                        //{
                        //    tVoucherDetails = new TVoucherDetails();
                        //    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        //    tVoucherDetails.VoucherSrNo = 2; VoucherSrNo = VoucherSrNo + 1;
                        //    tVoucherDetails.SignCode = 1;
                        //    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbBank);
                        //    tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text);
                        //    tVoucherDetails.Credit = 0;
                        //    tVoucherDetails.SrNo = 0;
                        //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        //    tVoucherDetails.Narration = "";
                        //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                        //}
                        //else if (VoucherType == VchType.Purchase)
                        //{
                        //    tVoucherDetails = new TVoucherDetails();
                        //    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        //    tVoucherDetails.VoucherSrNo = 2; VoucherSrNo = VoucherSrNo + 1;
                        //    tVoucherDetails.SignCode = 2;
                        //    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbBank);
                        //    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                        //    tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text);
                        //    tVoucherDetails.SrNo = 0;
                        //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        //    tVoucherDetails.Narration = "";
                        //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                        //}

                        #endregion

                        #region Cheque Printings
                        //tCheque = new TChequePrinting();
                        //tCheque.PkSrNo = ID;
                        //tCheque.PrintingUserNo = Convert.ToInt64(txtNo.Text);
                        //tCheque.PrintingDate = Convert.ToDateTime(dtpVoucherDate.Text);
                        //tCheque.IsPurchase = rdPurchase.Checked;
                        //tCheque.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                        //tCheque.ByLedgerNo = ObjFunction.GetComboValue(cmbBank);
                        //tCheque.ChequeNo = cmbChqNo.Text;
                        //tCheque.FKChequeNo = ObjFunction.GetComboValue(cmbChqNo);
                        //tCheque.ChequeDate = Convert.ToDateTime(dtpChqDate.Text);
                        //tCheque.Remark1 = txtRemark1.Text;
                        //tCheque.Remark2 = txtRemark2.Text;
                        //tCheque.Remark3 = cmbChequeName.Text;
                        //tCheque.ChqStatusNo = 1;
                        //tCheque.BankDate = Convert.ToDateTime(dtpBankDate.Text);
                        //tCheque.CompanyNo = DBGetVal.FirmNo;
                        //dbTVoucherEntry.AddTChequePrinting(tCheque);
                        #endregion

                        if (cnt > 0)
                        {
                            if (dbTVoucherEntry.ExecuteNonQueryStatements() != 0)
                                flag = true;
                            else
                                flag = false;
                        }
                        else
                            flag = false;

                    }
                    //else
                    //{
                    //    OMMessageBox.Show("Please Enter Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //}

                    if (flag == true)
                    {
                        if (ID == 0)
                        {
                            //OMMessageBox.Show("Voucher Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            tempID = ObjQry.ReturnLong("Select Max(PkSRno) FRom TChequePrinting ", CommonFunctions.ConStr);
                            DataRow drSearch = dtSearch.NewRow();
                            drSearch[0] = tempID;
                            dtSearch.Rows.Add(drSearch);
                            ID = tempID;

                            //if (OMMessageBox.Show("Are you sure want to print cheque details ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            //    PrintCheque();
                        }
                        //else
                            //OMMessageBox.Show("Voucher Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //setDisplay(true);
                        //ObjFunction.LockButtons(true, this.Controls);
                        //ObjFunction.LockControls(false, this.Controls);
                        //GridView.DataSource = null;
                        //FillControls();
                        //btnNew.Focus();
                    }
                    else
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
                        if (GridView.CurrentCell.ColumnIndex == ColIndex.Amount)
                        {
                            if (GridView.CurrentCell.Value != null)
                            {
                                if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                                    GridView.CurrentCell.ErrorText = "Please Enter Valid Amount";
                                else
                                    GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex];
                            }
                        }
                        if (GridView.CurrentCell.ColumnIndex == ColIndex.Disc1)
                        {
                            if (GridView.CurrentCell.Value != null)
                            {
                                if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                                    GridView.CurrentCell.ErrorText = "Please Enter Valid discount";
                                else
                                    GridView.CurrentCell = GridView[ColIndex.Disc1, GridView.CurrentCell.RowIndex];
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    string str = "Invoice No:", str2 = "";
                    for (int i = 0; i < GridView.Rows.Count; i++)
                    {
                        if (Convert.ToDouble(GridView.Rows[i].Cells[ColIndex.Amount].Value) > 0.00)
                        {
                            str2 = Convert.ToString(GridView.Rows[i].Cells[ColIndex.BillNo].Value);
                            str2 = str2 + ',';
                        }
                        str = str + ' ' + str2;
                        str2 = "";
                    }

                    txtRemark1.Text = str;
                    cmbBank.Focus();
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
            public static int BillAmt = 2;
            public static int TotRec = 3;
            public static int NetBal = 4;
            public static int PayType = 6;
            public static int Amount = 5;
            public static int LedgerNo = 7;
            public static int ChqNo = 8;
            public static int ChqDate = 9;
            public static int BankNo = 10;
            public static int BranchNo = 11;
            public static int PayTypeNo = 12;
            public static int RefNo = 13;
           // public static int Chk = 14;
            public static int Disc1 = 14;
            public static int Disc2 = 15;
        }

        private void cmbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ID == 0)
                {
                    if (rdPurchase.Checked == true)
                        processPartyNameChange();
                    else cmbBank.Focus();
                }
                else cmbBank.Focus();
            }
        }

        private void processPartyNameChange()
        {
            txtRemark3.Text = cmbPartyName.Text;
            if (rdPurchase.Checked == true)
            {
                BindGrid();
                if (GridView.Rows.Count > 0)
                {
                    GridView.Focus();
                    GridView.CurrentCell = GridView[ColIndex.Amount, 0];
                    label5.Visible = true;
                    amtflag = 0;
                    //FillPrintChequeName();
                }
                else
                {
//                  cmbPartyName.Focus();
                    txtAmount.Enabled = true;
                    amtflag = 1;
                    cmbBank.Focus();

                }


                rbCheck_Changed();
            }
        }

        private bool ChqValidations()
        {
            bool tempflag = false;
            try
            {
                EP.SetError(cmbChqNo, "");
                EP.SetError(cmbBank, "");

                if (Convert.ToInt64(cmbChqNo.SelectedValue) == 0)
                {
                    EP.SetError(cmbChqNo, "Select Cheque No");
                    EP.SetIconAlignment(cmbChqNo, ErrorIconAlignment.MiddleRight);
                    cmbChqNo.Focus();
                }
                else if (Convert.ToInt64(cmbBank.SelectedValue) == 0)
                {
                    EP.SetError(cmbBank, "Select Bank");
                    EP.SetIconAlignment(cmbBank, ErrorIconAlignment.MiddleRight);
                    cmbBank.Focus();
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
                            CalculateAmount();
                            //txtAmount.Text = (Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(GridView.CurrentCell.Value)).ToString(Format.DoubleFloating);
                            if (rowCount + 1 < GridView.Rows.Count)
                            {
                                MovetoNext move2n = new MovetoNext(m2n);
                                BeginInvoke(move2n, new object[] { rowCount , ColIndex.Disc1, GridView });
                            }
                            else
                            {
                                MovetoNext move2n = new MovetoNext(m2n);
                                BeginInvoke(move2n, new object[] { rowCount, ColIndex.Disc1, GridView });
                            }
                            //GridView.CurrentCell = GridView[ColIndex.PayType, rowCount];
                        }

                    }
                    else
                    {
                        GridView.CurrentCell.Value = "0.00";
                        if (rowCount + 1 < GridView.Rows.Count)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount + 1, ColIndex.Amount, GridView });
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, GridView });
                        }
                    }
                }
                else if (GridView.CurrentCell.ColumnIndex == ColIndex.Disc1)
                {
                    if (GridView.CurrentCell.Value != null)
                    {
                        CalculateAmount();

                        //--
                        if (rowCount + 1 < GridView.Rows.Count)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount+1, ColIndex.Amount, GridView });
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, GridView });
                        }

                        //--
                    }
                }
                else if (GridView.CurrentCell.ColumnIndex == ColIndex.Disc2)
                {
                    if (GridView.CurrentCell.Value != null)
                    {
                        CalculateAmount();
                    }
                }
                // CalculateAmount();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CalculateAmount()
        {
            try
            {
                double TotalAmt = 0;
                TotalDisc = 0.00;
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    TotalAmt += Convert.ToDouble(GridView.Rows[i].Cells[ColIndex.Amount].Value);
                    TotalDisc += Convert.ToDouble(GridView.Rows[i].Cells[ColIndex.Disc1].Value) + Convert.ToDouble(GridView.Rows[i].Cells[ColIndex.Disc2].Value);
                }
                txtAmount.Text = TotalAmt.ToString(Format.DoubleFloating);
                txtDisc1.Text = TotalDisc.ToString(Format.DoubleFloating);
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
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                InitControls();
                NavigationDisplay(5);
                ObjFunction.LockControls(false, this.Controls);
                BtnSave.Enabled = true;
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                        " WHERE (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") AND (TVoucherEntry.VoucherTypeCode in( " + VchType.SalesReceipt + "," + VchType.RejectionIn + "))" +
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
                        " WHERE (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") AND (TVoucherEntry.VoucherTypeCode in (" + VchType.PurchasePayment + "," + VchType.RejectionOut + "))" +
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
                    pnlRecord.Location = new System.Drawing.Point(470, 20);
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
                    // cmbPartyName.SelectedValue = Adsch.LedgerNo;
                    FillTodaysCheque(Adsch.LedgerNo);
                    // cmbPartyName.Focus();
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

                BtnSave.Enabled = true;
                lblPrint.Visible = true;
                txtAmount.Text = "0.00";
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    GridView.Rows[i].Cells[5].Value = "0.00";
                }
                if (GridView.Rows.Count > 0)
                {
                    GridView.CurrentCell = GridView.Rows[0].Cells[5];
                    GridView.Focus();
                }
//                FillPrintChequeName();
                // else { cmbPartyName.Focus(); }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void rbBillwise_CheckedChanged(object sender, EventArgs e)
        {
            rbCheck_Changed();
        }

        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataGridView gv = null;
                gv = GridView;
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (ObjFunction.CheckValidAmount(txtAmount.Text.Replace("-", "")) == true)
                    {
                        txtRemark1.Focus();
                    }
                    else
                        MessageBox.Show("Enter Valid Amount", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                    if (ID == 0)
                        processPartyNameChange();
                }
                else
                {
                    DataClrscr();
                    cmbPartyName.Focus();
                }
                isProcessingPartyNameChange = false;
            }
        }

        private void dgBillSelection_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txtAmt = (TextBox)e.Control;
            txtAmt.TextChanged += new EventHandler(txtAmt_TextChanged);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtAmount, 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void rbAdvance_CheckedChanged(object sender, EventArgs e)
        {
            rbCheck_Changed();
        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.BillDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
            }
        }

        private void DataClrscr()
        {
            cmbPartyName.SelectedValue = "0";
            lblNetBal.Text = "";
            lblAmount.Text = "";
            lblOpBalp.Text = "0.00";
            lblNetBp.Text = "0.00";
            lblDrBalp.Text = "0.00";
            lblCrNetp.Text = "0.00";

            GridView.Rows.Clear();
            pnlRecord.Visible = false;
            pnlDetailsPur.Visible = false;
            //BtnSave.Enabled = false;
            label5.Visible = false;

        }

        private void PrintOutstanding()
        {
            try
            {
                Display.MultipleBillPrint Newf = new Yadi.Display.MultipleBillPrint(ObjFunction.GetComboValue(cmbPartyName), VoucherType, DBGetVal.FirmNo, cmbPartyName.Text);
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            ID = 0;
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            InitControls();
            FillTodaysCheque();
            dtpVoucherDate.Focus();
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
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

                if (ID > 0)
                    FillControls();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SetNavigation()
        {
            cntRow = 0;
            DataRow[] dr = dtSearch.Select("PkSrNo=" + ID);
            if (dr.Length > 0)
            {
                cntRow = dtSearch.Rows.IndexOf(dr[0]);
            }
            else
            {
                cntRow = dtSearch.Rows.Count - 1;
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            //btnDelete.Visible = flag;
            //GridRange.Height = 25;
            if (dtSearch.Rows.Count == 0)
            {
                btnFirst.Visible = false;
                btnPrev.Visible = false;
                btnNext.Visible = false;
                btnLast.Visible = false;
            }
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);

            cmbPartyName.Focus();
        }

        private void cmbBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbBank_Leave(sender, new EventArgs());
            }
        }

        private void cmbBank_Leave(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbChqNo, "SELECT TChequeNoDetails.PkSrNo, TChequeNoDetails.ChequeDetailsUserNo FROM TChequeNo INNER JOIN " +
           " TChequeNoDetails ON TChequeNo.PkChequeNo = TChequeNoDetails.FkChequeNo WHERE (TChequeNoDetails.IsActive = 'true') " +
           " AND (TChequeNoDetails.FkVoucherTrnNo = 0) AND (TChequeNo.LedgerNo = " + ObjFunction.GetComboValue(cmbBank) + ") ", "ECS");
            string strReportName = ObjQry.ReturnString("Select ReportName From  MLedgerDetails Where LedgerNo =" + ObjFunction.GetComboValue(cmbBank) + "", CommonFunctions.ConStr);
            if (strReportName == "")
            {
                DisplayMessage("Please Add Report Name Of Selecated Bank.." + cmbBank.Text);
                // cmbBank.Focus();
                return;
            }
            if (cmbBank.SelectedIndex == 0)
            { cmbBank.Focus(); }
            else
                cmbChqNo.Focus();
        }

        private void PrintCheque()
        {
            int PrintType = 0;
            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrint)) == true)
            //{
            //    PrintType = 0;
            //}
            //else { PrintType = 1; }


            string strReportName = ObjQry.ReturnString("Select ReportName From  MLedgerDetails Where LedgerNo =" + ObjFunction.GetComboValue(cmbBank) + "", CommonFunctions.ConStr);


            string[] ReportSession;

            ReportSession = new string[2];
            ReportSession[0] = ID.ToString();
            ReportSession[1] = NumberToWordsIndian.getWords(txtAmount.Text);

            if (PrintType == 0)
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    childForm = ObjFunction.GetReportObject("Reports." + strReportName);
                else
                    childForm = ObjFunction.LoadReportObject(strReportName + ".rpt", CommonFunctions.ReportPath);


                if (childForm != null)
                {
                    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    if (objRpt.PrintReport() == true)
                    {
                        DisplayMessage("Bill Print Successfully!!!");
                    }
                    else
                    {
                        DisplayMessage("Bill not Print !!!");
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
                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject(strReportName + ".rpt", CommonFunctions.ReportPath), ReportSession);


                if (NewF != null)
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
                PrintCheque();
        }

        private void dgTodaysChq_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            else if (e.ColumnIndex == 5)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }
        }

        public void FillTodaysCheque()
        {
            DataTable dtChq = ObjFunction.GetDataView("SELECT TChequePrinting.PkSrNo,0 AS SrNo, MLedger.LedgerName, TChequePrinting.ChequeNo, TVoucherEntry.BilledAmount AS Amount, TChequePrinting.ChequeDate, MLedger_1.LedgerName AS BankName " +
                " FROM TChequePrinting INNER JOIN TVoucherEntry ON TChequePrinting.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MLedger ON TChequePrinting.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                " MLedger AS MLedger_1 ON TChequePrinting.ByLedgerNo = MLedger_1.LedgerNo WHERE (TChequePrinting.PrintingDate = '" + dtpVoucherDate.Text + "') " +
                " ORDER BY TVoucherEntry.VoucherDate DESC").Table;
            dgTodaysChq.DataSource = dtChq;
        }

        public void FillTodaysCheque(long ledgerno)
        {
            DataTable dtChq = ObjFunction.GetDataView("SELECT TChequePrinting.PkSrNo,0 AS SrNo, MLedger.LedgerName, TChequePrinting.ChequeNo, TVoucherEntry.BilledAmount AS Amount, TChequePrinting.ChequeDate, MLedger_1.LedgerName AS BankName " +
                " FROM TChequePrinting INNER JOIN TVoucherEntry ON TChequePrinting.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MLedger ON TChequePrinting.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                " MLedger AS MLedger_1 ON TChequePrinting.ByLedgerNo = MLedger_1.LedgerNo WHERE (TChequePrinting.LedgerNo = " + ledgerno + ") " +
                " ORDER BY TVoucherEntry.VoucherDate DESC").Table;
            dgTodaysChq.DataSource = dtChq;
        }
        private void dtpVoucherDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                FillTodaysCheque();
                rdPurchase.Focus();
            }
        }

        private void rdPurchase_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPurchase.Checked == true)
            {   if (BtnSave.Visible== true )

               txtAmount.Enabled = false;
                  ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo =" + grpNo + " order by LedgerName");
            }
            else
            {
                if (BtnSave.Visible == true)               txtAmount.Enabled = true;
//                  ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo  in(" + GroupType.SundryDebtors + "," + GroupType.SundryCreditors + ") order by LedgerName");
                ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo not in(" + GroupType.SundryDebtors + "," + GroupType.SundryCreditors + ") order by LedgerName");

            }


        }

        private void rdPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbPartyName.Focus();
            }
        }

        private void dtpChqDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtAmount.Enabled == true)
                    txtAmount.Focus();
                else
                    //cmbChequeName.Focus();
                    txtRemark3.Focus();
            }
        }

        private void btnChqCancel_Click(object sender, EventArgs e)
        {
            if (ID == 0) return;
            if (OMMessageBox.Show("Are you sure you want to cancel this cheque ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {

                dbTVoucherEntry = new DBTVaucherEntry();
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ObjQry.ReturnLong("Select FKVoucherNo From TChequePrinting Where PkSrNo=" + ID + "", CommonFunctions.ConStr);
                dbTVoucherEntry.CancelTVoucherEntry(tVoucherEntry);


                OMMessageBox.Show("Cheque cancelled successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                lblCancel.Visible = true;


            }
        }

        private void cmbChqNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                if (cmbChqNo.SelectedIndex == 0)
                { cmbChqNo.Focus(); }
                else

                    dtpChqDate.Focus();
            }
        }

        private void txtRemark1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemark2.Focus();
            }
        }

        private void txtRemark3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                txtRemark1.Focus();
            }

        }
        

        private void txtRemark2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStatus.Focus();
            }
        }

        private void txtRemark3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtStatus.Focus();
            }
        }

        private void txtStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpBankDate.Focus();
            }

        }

        private void dtpBankDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }

        }

        private void cmbChequeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                
                txtRemark1.Focus();
            }
        }





    }
}


