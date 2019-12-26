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
    public partial class CollectionEntryAE : Form
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
        int rowCount, AdvRowIndex = 0;//VoucherNo
        //double drTotal, crTotal;
        long VoucherType = 0, VoucherTypeCode = 0, grpNo, LedgNo = 0;
        double AdvAdjAmt = 0, TolAdvadjAmt = 0, Amt = 0;
        double OpBalPendingAmt = 0, OpBalAdjustedAmt = 0;
        bool Advflag = false;
        public string StrLedgerQuery;
        public string StrRBillNo = "";

        DataTable dtCompRatio = new DataTable();

        /// <summary>
        /// This is Class of Constructor
        /// </summary>
        public CollectionEntryAE()
        {
            InitializeComponent();
            this.VoucherType = OM.VchType.Sales;
        }
        /// <summary>
        /// This is Class of parameterised Constructor
        /// </summary>
        public CollectionEntryAE(long VoucherType)
        {
            InitializeComponent();
          //  rbBillSelection.Visible = false;
            this.VoucherType = VoucherType;
            if (VoucherType == 15)
            {
                if (DBGetVal.KachhaFirm == false)
                {
                    VoucherType = VchType.Sales;
                    VoucherTypeCode = VchType.SalesReceipt;
                }
                else
                {
                    VoucherType = VchType.DSales;
                    VoucherTypeCode = VchType.DSalesReceipt;
                }

                this.Text = "Sales Collection Entry";
                grpNo = GroupType.SundryDebtors;
                rbAdjSalesReturn.Text = "Adjust Sales Return";
                lblTargetBank.Text = "Received In Bank A/c : ";

            }
            else if (VoucherType == 9)
            {
                if (DBGetVal.KachhaFirm == false)
                {
                    VoucherType = VchType.Purchase;
                    VoucherTypeCode = VchType.PurchasePayment;
                }
                else
                {
                    VoucherType = VchType.DPurchase;
                    VoucherTypeCode = VchType.DPurchasePayment;
                }
                this.Text = "Purchase Payment Entry";
                grpNo = GroupType.SundryCreditors;
                rbAdjSalesReturn.Text = "Adj. Purchase Return";
                lblTargetBank.Text = "Given From Bank A/c : ";

            }
        }

        private void SalesVoucherAE_Load(object sender, EventArgs e)
        {
            try
            {
                KeyDownFormat(this.Controls);
                dtpVoucherDate.Value = DBGetVal.ServerTime;

                if (VoucherType == 15)
                {
                    if (DBGetVal.KachhaFirm == false)
                    {
                        VoucherType = VchType.Sales;
                        VoucherTypeCode = VchType.SalesReceipt;
                    }
                    else
                    {
                        VoucherType = VchType.DSales;
                        VoucherTypeCode = VchType.DSalesReceipt;
                    }
                }
                else if (VoucherType == 9)
                {
                    if (DBGetVal.KachhaFirm == false)
                    {
                        VoucherType = VchType.Purchase;
                        VoucherTypeCode = VchType.PurchasePayment;
                    }
                    else
                    {
                        VoucherType = VchType.DPurchase;
                        VoucherTypeCode = VchType.DPurchasePayment;
                    }

                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                    ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo =" + grpNo + " order by LedgerName");
                else
                    ObjFunction.FillCombo(cmbPartyName, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + grpNo + ")) AND (MLedger.IsActive = 'true') ORDER BY LedgerName ");

                dtPayTypeLedger = ObjFunction.GetDataView("SELECT     MPayTypeLedger.PayTypeNo, MPayTypeLedger.LedgerNo, MPayType.PayTypeName " +
                                                           " FROM MPayTypeLedger INNER JOIN MPayType ON MPayTypeLedger.PayTypeNo = MPayType.PKPayTypeNo " +
                                                            "  where MPayTypeLedger.CompanyNo=" + DBGetVal.FirmNo + " and MPayType.PKPayTypeNo not in (1,3) order by PayTypeName").Table;


                ObjFunction.FillCombo(cmbBranch, "Select BranchNo,BranchName From MBranch  Where isActive='true' order by BranchName");

                ObjFunction.FillCombo(cmbCompanyBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + " And IsActive='true' order by LedgerName");
                ObjFunction.FillCombo(cmbCrCompanyBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + " And IsActive='true' order by LedgerName");

                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                {
                    ObjFunction.FillCombo(cmbBank, "Select BankNo,BankName From MOtherBank where IsActive='true' order by BankName");
                    ObjFunction.FillCombo(cmbCrBank, "Select BankNo,BankName From MOtherBank where IsActive='true' order by BankName");
                    ObjFunction.FillList(lstPayType, "Select PKPayTypeNo,PayTypeName from MPayType where ControlUnder not in(1,3)");
                    ObjFunction.FillComb(cmbPayType, "Select PKPayTypeNo,PayTypeName from MPayType where ControlUnder not in(1,3)");
                }
                else
                {
                    ObjFunction.FillCombo(cmbBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + " And IsActive='true' order by LedgerName");
                    ObjFunction.FillCombo(cmbCrBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + "  And IsActive='true' order by LedgerName");
                    ObjFunction.FillList(lstPayType, "Select PKPayTypeNo,PayTypeName from MPayType where PKPayTypeNo not in(1,3)");//in(2,4,5)");
                    ObjFunction.FillComb(cmbPayType, "Select PKPayTypeNo,PayTypeName from MPayType where PKPayTypeNo not in(1,3)");//in(2,4,5)");
                }

                ObjFunction.FillCombo(cmbCrBranch, "Select BranchNo,BranchName From MBranch Where isActive='true' order by BranchName");

                cmbPartyName.Focus();
                //lblNetBal.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblC.Font = new Font("Verdana", 11, FontStyle.Bold);
                lbld.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblDrBal.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblNetB.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblCrNet.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblN.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblop.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblOpBal.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblCp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lbldp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblDrBalp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblNetBp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblCrNetp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblNp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblopp.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblOpBalp.Font = new Font("Verdana", 11, FontStyle.Bold);
                label15.Font = new Font("Verdana", 11, FontStyle.Bold);
                lblTotRecAmt.Font = new Font("Verdana", 8, FontStyle.Regular);

                label5.Visible = false;


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

                    double NetBal = 0, TotRec = 0;
                    if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                        pnlDetails.Visible = true;
                    else
                        pnlDetailsPur.Visible = true;
                 //   MessageBox.Show (ObjFunction.GetComboValue(cmbPartyName) + "," + VoucherType + "," + DBGetVal.FirmNo + "");
                    //

                   // OMMessageBox.Show("Sorry No Data to Save......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                    DataTable dt = ObjFunction.GetDataView("Exec GetCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VoucherType + "," + DBGetVal.FirmNo + "").Table;




                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridView.Rows.Add();
                        dgBillSelection.Rows.Add();

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                            dgBillSelection.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                        }
                        NetBal = NetBal + Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                        TotRec = TotRec + Convert.ToDouble(dt.Rows[i].ItemArray[ColIndex.TotRec].ToString());
                        GridView.Rows[i].Cells[ColIndex.DiscAmt].Value = "0.00";
                        dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value = "0.00";
                    }
                    double OpBalance = 0.00;
                    string str = "";



                    //if (DBGetVal.KachhaFirm == false)
                    //{
                    // str = "136,115,111,109,107,130,131,113";
                    // OpBalance = ObjQry.ReturnDouble("Select OpAmt * -1 from GetOpeningLedgerBalanceOnly(" + ObjFunction.GetComboValue(cmbPartyName) + "," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                    // }
                    //else
                    //{
                    // str = "36,15,11,9,7,30,31,13";
                    //OpBalance = ObjQry.ReturnDouble("Select OpAmt * -1 from GetOpeningLedgerBalanceOnlyES(" + ObjFunction.GetComboValue(cmbPartyName) + "," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                    //}
                    if (DBGetVal.KachhaFirm == false)
                    {
                        //                        str = "136,115,111,109,107,130,131,113";
                        str = "36, 136 ,115,111,109,107,130,131,113";

                        OpBalance = ObjQry.ReturnDouble("Select OpAmt * -1 from GetOpeningLedgerBalanceOnly(" + ObjFunction.GetComboValue(cmbPartyName) + "," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                    }
                    else
                    {
                        //                        str = "36,15,11,9,7,30,31,13";
                        str = "36, 136,15,11,9,7,30,31,13";

                        OpBalance = ObjQry.ReturnDouble("Select OpAmt * -1 from GetOpeningLedgerBalanceOnlyES(" + ObjFunction.GetComboValue(cmbPartyName) + "," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);

                    }
                    //umesh===21-11-2018
                    DataTable dtDetails = ObjFunction.GetDataView("SELECT " +
                        " isnull(SUM(TVoucherDetails.Debit),0.00) AS 'Dr Bal', " +
                        " isnull( SUM(TVoucherDetails.Credit),0.00) AS 'Cr Bal' " +
                        " FROM TVoucherDetails " +
                        " INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                        " WHERE (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") " +
                        " AND (TVoucherEntry.IsCancel = 0) AND (TVoucherEntry.VoucherTypeCode not in( " + str + ")) ").Table;//AND (TVoucherEntry.VoucherTypeCode = " + VoucherType  + ")





                    lblDrBal.Text = "0.00";
                    lblDrBalp.Text = "0.00";
                    lblCrNet.Text = "0.00";
                    lblCrNetp.Text = "0.00";
                    lblOpBal.Text = "0.00";
                    lblNetB.Text = "0.00";
                    lblNetBp.Text = "0.00";

                    lblOpBal.Text = Math.Abs(OpBalance).ToString("0.00");
                    if (Convert.ToDouble(OpBalance) < 0)
                        lblOpStatus.Text = "To Receive";
                    else if (Convert.ToDouble(OpBalance) > 0)
                        lblOpStatus.Text = "To Pay";
                    else
                        lblOpStatus.Text = "";

                    lblOpBalp.Text = Math.Abs(OpBalance).ToString("0.00");
                    if (Convert.ToDouble(OpBalance) < 0)
                        lblOpStatusPur.Text = "To Receive";
                    else if (Convert.ToDouble(OpBalance) > 0)
                        lblOpStatusPur.Text = "To Pay";
                    else
                        lblOpStatusPur.Text = "";


                    if (dtDetails.Rows.Count > 0)
                    {
                        lblAmount.Text = NetBal.ToString("0.00");
                        lblDrBal.Text = dtDetails.Rows[0].ItemArray[0].ToString();
                        lblDrBalp.Text = dtDetails.Rows[0].ItemArray[0].ToString();
                        lblCrNet.Text = dtDetails.Rows[0].ItemArray[1].ToString();
                        lblCrNetp.Text = dtDetails.Rows[0].ItemArray[1].ToString();
                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                        {
                            lblNetB.Text = (OpBalance +
                                Convert.ToDouble(dtDetails.Rows[0].ItemArray[1].ToString()) -
                                Convert.ToDouble(dtDetails.Rows[0].ItemArray[0].ToString())).ToString("0.00");
                            if (Convert.ToDouble(lblNetB.Text) > 0)
                                lblPayStatus.Text = "To Pay";
                            else if (Convert.ToDouble(lblNetB.Text) < 0)
                                lblPayStatus.Text = "To Receive";
                            else
                                lblPayStatus.Text = "";
                            lblNetB.Text = Math.Abs(Convert.ToDouble(lblNetB.Text)).ToString("0.00");

                        }
                        else
                        {
                            lblNetBp.Text = (OpBalance +
                                Convert.ToDouble(dtDetails.Rows[0].ItemArray[1].ToString()) -
                                Convert.ToDouble(dtDetails.Rows[0].ItemArray[0].ToString())).ToString("0.00");
                            if (Convert.ToDouble(lblNetBp.Text) > 0)
                                lblPayStatusPur.Text = "To Pay";
                            else if (Convert.ToDouble(lblNetBp.Text) < 0)
                                lblPayStatusPur.Text = "To Receive";
                            else
                                lblPayStatusPur.Text = "";
                            lblNetBp.Text = Math.Abs(Convert.ToDouble(lblNetBp.Text)).ToString("0.00");
                        }

                    }
                    else
                    {
                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                        {
                            lblNetB.Text = Math.Abs(OpBalance).ToString("0.00");
                            lblPayStatus.Text = lblOpStatus.Text;
                        }
                        else
                        {
                            lblNetBp.Text = Math.Abs(OpBalance).ToString("0.00");
                            lblPayStatusPur.Text = lblOpStatusPur.Text;
                        }
                    }

                    dgAdvance.Rows.Clear();
                    DataTable dtAdv = new DataTable();
                    if (VoucherType == VchType.Sales)
                    {
                        if (rbAgainstAdv.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.SalesReceipt + "," + DBGetVal.FirmNo + ",1").Table;
                        else if (rbAdjSalesReturn.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.RejectionIn + "," + DBGetVal.FirmNo + ",3").Table;

                    }
                    else if (VoucherType == VchType.Purchase)
                    {
                        if (rbAgainstAdv.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.PurchasePayment + "," + DBGetVal.FirmNo + ",1").Table;
                        else if (rbAdjSalesReturn.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.RejectionOut + "," + DBGetVal.FirmNo + ",3").Table;
                    }
                    else if (VoucherType == VchType.DSales)
                    {
                        if (rbAgainstAdv.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DSalesReceipt + "," + DBGetVal.FirmNo + ",1").Table;
                        else if (rbAdjSalesReturn.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DRejectionIn + "," + DBGetVal.FirmNo + ",3").Table;

                    }
                    else if (VoucherType == VchType.DPurchase)
                    {
                        if (rbAgainstAdv.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DPurchasePayment + "," + DBGetVal.FirmNo + ",1").Table;
                        else if (rbAdjSalesReturn.Checked == true)
                            dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DRejectionOut + "," + DBGetVal.FirmNo + ",3").Table;
                    }

                    for (int i = 0; i < dtAdv.Rows.Count; i++)
                    {
                        dgAdvance.Rows.Add();
                        for (int j = 0; j < dgAdvance.Columns.Count - 1; j++)
                        {
                            dgAdvance.Rows[i].Cells[j].Value = dtAdv.Rows[i].ItemArray[j];
                        }
                    }

                    double OpBal = 0;
                    OpBal = OpBalance;
                    double TotBal = 0;
                    //In below query we used reverse logic of signcode as this is adjsted entry's values
                    TotBal = OpBal + ObjQry.ReturnDouble("Select isNull(Sum(case when (signcode=1) then isNull(Amount,0) else isNull(Amount,0)*-1 end),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=5 AND REFNO <> 0 " +
                        " AND FKVOUCHERTRNNO IN (SELECT PKVOUCHERTRNNO FROM TVOUCHERDETAILS WHERE FKVOUCHERNO IN (SELECT PKVOUCHERNO FROM TVOUCHERENTRY WHERE TVoucherEntry.VoucherTypeCode NOT  in( " + str + "))) ", CommonFunctions.ConStr);
                    //In below query we used forward logic of signcode as this is direct entry's values
                    TotBal = TotBal + ObjQry.ReturnDouble("Select isNull(Sum(case when (signcode=2) then isNull(Amount,0) else isNull(Amount,0)*-1 end),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=5 AND REFNO = 0 " +
                       " AND FKVOUCHERTRNNO IN (SELECT PKVOUCHERTRNNO FROM TVOUCHERDETAILS WHERE FKVOUCHERNO IN (SELECT PKVOUCHERNO FROM TVOUCHERENTRY WHERE TVoucherEntry.VoucherTypeCode NOT in( " + str + "))) ", CommonFunctions.ConStr);


                    OpBalPendingAmt = Math.Abs(TotBal);
                    OpBalAdjustedAmt = 0;
                    if (OpBal <= 0 && TotBal >= 0)
                    {
                        rbAdjustment.Enabled = false;
                    }
                    else if (OpBal >= 0 && TotBal <= 0)
                    {
                        rbAdjustment.Enabled = false;
                    }
                    else
                    {
                        rbAdjustment.Enabled = true;
                    }

                    BindGridAdv();

                    GridView.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    dgBillSelection.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBillSelection.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBillSelection.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBillSelection.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBillSelection.Columns[ColIndex.Chk].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgBill.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBill.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBill.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBill.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgBill.Columns[ColIndex.Chk].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    dgAdvance.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgAdvance.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgAdvance.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgAdvance.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
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
                        if (GridView.Rows[i].Cells[ColIndex.Amount].ErrorText == "" &&
                            GridView.Rows[i].Cells[ColIndex.DiscAmt].ErrorText == "")
                        {
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            OMMessageBox.Show("Please Enter Valid Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            GridView.CurrentCell = GridView.Rows[i].Cells[ColIndex.Amount];
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

        private bool ValidationChqNCreditCard()
        {
            bool flag = true;
            long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
            if (ControlUnder == 4)//cmbPayType.Text == "Cheque"
            {
                if (txtChqNo.Text == "")
                {
                    flag = false;
                    OMMessageBox.Show("Enter Cheque No.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else if ((ObjFunction.GetComboValue(cmbBank) == 0) && (ObjFunction.GetComboValue(cmbPayType) == 4) && ((VoucherType != VchType.Purchase) || (VoucherType != VchType.Purchase)))
                {
                    flag = false;
                    OMMessageBox.Show("Select Bank Name.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else if (ObjFunction.GetComboValue(cmbCompanyBank) == 0)
                {
                    flag = false;
                    OMMessageBox.Show("Select Bank Account.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else if ((ObjFunction.GetComboValue(cmbBranch) == 0) && (ObjFunction.GetComboValue(cmbPayType) == 4) && ((VoucherType != VchType.Purchase) || (VoucherType != VchType.Purchase)))
                {
                    flag = false;
                    OMMessageBox.Show("Select Branch Name.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                    flag = true;
            }
            else if (ControlUnder == 5)//cmbPayType.Text == "Credit Card"
            {
                if (txtCrCardNo.Text == "")
                {
                    flag = false;
                    OMMessageBox.Show("Enter Credit Card No.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }

                else if (ObjFunction.GetComboValue(cmbCrCompanyBank) == 0)
                {
                    flag = false;
                    OMMessageBox.Show("Select Bank Account.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }

                else
                    flag = true;
            }
            return flag;
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
                        if (Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.NetBal].Value) < (Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value)))
                            dgBillSelection.Rows[i].Cells[ColIndex.Amount].ErrorText = "Please enter valid amount";
                        else
                        {
                            dgBillSelection.Rows[i].Cells[ColIndex.Amount].ErrorText = "";
                            dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].ErrorText = "";
                        }
                        if (dgBillSelection.Rows[i].Cells[ColIndex.Amount].ErrorText == "" &&
                            dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].ErrorText == "")
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

        private bool ValidationdgBillAdv()
        {
            bool flag = false, flagAdv = false; ;
            try
            {
                if (dgBill.Rows.Count <= 0)
                {
                    OMMessageBox.Show("Sorry No Data to Save......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                        {
                            flagAdv = true;
                            break;
                        }

                    }

                    if (flagAdv == true)
                        flag = true;
                    else
                        flag = false;

                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool ValidationdgAdjustOpening()
        {
            bool flag = false, flagAdv = false; ;
            try
            {
                if (dgAdjustOpening.Rows.Count <= 0)
                {
                    OMMessageBox.Show("Sorry No Data to Save......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    for (int i = 0; i < dgAdjustOpening.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgAdjustOpening.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                        {
                            flagAdv = true;
                            break;
                        }
                    }

                    if (flagAdv == true)
                        flag = true;
                    else
                        flag = false;
                }
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
                    string StrBillNo = "";
                    StrRBillNo = "";
                    DataGridView dg = null;
                    double TotAmt = 0;
                    if (Validations() == true)
                    {
                        #region rbBillwise
                        if (rbBillwise.Checked == true)
                        {
                            for (int j = 0; j < GridView.RowCount; j++)
                            {
                                if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value.ToString()) != 0 )
                                {
                                    //setCompanyRatio(Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value.ToString()));
                                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value.ToString()) + "", CommonFunctions.ConStr);
                                    dbTVoucherEntry = new DBTVaucherEntry();
                                    tVoucherEntry = new TVoucherEntry();
                                    tVoucherEntry.PkVoucherNo = 0;
                                    tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                    tVoucherEntry.VoucherUserNo = 0;
                                    tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                    tVoucherEntry.VoucherTime = DBGetVal.ServerTime;// Convert.ToDateTime("01-Jan-1900");
                                    tVoucherEntry.Reference = "";

                                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherEntry.BilledAmount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);// + Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                    tVoucherEntry.ChallanNo = "";
                                    tVoucherEntry.Remark = Convert.ToString(GridView.Rows[j].Cells[ColIndex.Remark].Value);
                                    tVoucherEntry.ChequeNo = 0;
                                    tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;// Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                    if (VoucherTypeCode == 130 || VoucherTypeCode == 30)
                                        tVoucherEntry.Narration = "Collection";
                                    else if(VoucherTypeCode == 31 || VoucherTypeCode == 131)

                                    {
                                        tVoucherEntry.Narration = "Payment";

                                    }
                                    else {
                                        tVoucherEntry.Narration = "";

                                    }


                                    tVoucherEntry.UserID = DBGetVal.UserID;
                                    tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                    tVoucherEntry.OrderType = 1;
                                    tVoucherEntry.PayTypeNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                    tVoucherEntry.TransporterCode = 0;
                                    tVoucherEntry.TransPayType = 0;
                                    tVoucherEntry.LRNo = "";
                                    tVoucherEntry.TransportMode = 0;
                                    tVoucherEntry.TransNoOfItems = 0;
                                    tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                    dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);// SetVoucherCompany(tVoucherEntry);for firmwise

                                    if (StrRBillNo == "")
                                        StrRBillNo = "(" + GridView.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVoucherEntry.BilledAmount + "," + Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";
                                    else
                                        StrRBillNo = StrRBillNo + Environment.NewLine + "(" + GridView.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVoucherEntry.BilledAmount + "," + Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";


                                    if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                    {
                                        tVoucherDetails = new TVoucherDetails();
                                        tVoucherDetails.PkVoucherTrnNo = 0;
                                        tVoucherDetails.VoucherSrNo = 1;
                                        tVoucherDetails.SignCode = 2;
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                        tVoucherDetails.Debit = 0;
                                        tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);// + Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                        tVoucherDetails.SrNo = Others.Party;
                                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        tVoucherDetails.Narration = "Collection";
                                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails); for firmwise

                                        // if discount > 0 then 
                                        //if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value) > 0)
//                                        {
//                                           tVoucherDetails = new TVoucherDetails();
//                                            tVoucherDetails.PkVoucherTrnNo = 0;
//                                            tVoucherDetails.VoucherSrNo = 1;
//                                           tVoucherDetails.SignCode = 2;
//                                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
//                                            tVoucherDetails.Debit = 0;
//                                            tVoucherDetails.Credit =  Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
//                                            tVoucherDetails.SrNo = Others.Party;
//                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
//                                            tVoucherDetails.Narration = "";
//                                           dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails); for firmwise
//                                        }
                                        //

                                    }
                                    else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                    {
                                        tVoucherDetails = new TVoucherDetails();
                                        tVoucherDetails.PkVoucherTrnNo = 0;
                                        tVoucherDetails.VoucherSrNo = 1;
                                        tVoucherDetails.SignCode = 1;
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                        tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);// + Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                        tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                        tVoucherDetails.SrNo = Others.Party;
                                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        tVoucherDetails.Narration = "Payment";
                                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                                    }

                                    tVchRefDtls = new TVoucherRefDetails();
                                    tVchRefDtls.PkRefTrnNo = 0;
                                    tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                    tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                    tVchRefDtls.TypeOfRef = 2;
                                    tVchRefDtls.RefNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value);
                                    tVchRefDtls.DueDays = 0;
                                    tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                    tVchRefDtls.Amount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value); //tVoucherEntry.BilledAmount;
                                    tVchRefDtls.DiscAmt = 0; Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                    if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                        tVchRefDtls.SignCode = 2;
                                    else
                                        tVchRefDtls.SignCode = 1;

                                    tVchRefDtls.UserID = DBGetVal.UserID;
                                    tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                    tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                                    if ((ControlUnder == 4) || (ControlUnder == 5))//GridView.Rows[j].Cells[ColIndex.PayType].Value.ToString() == "Cheque"
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

                                    if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                    {  if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value) != 0)
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
                                            tVoucherDetails.Narration = "Collection";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);
                                        }
                                        //if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value) != 0)
                                        //{
                                        //    tVoucherDetails = new TVoucherDetails();
                                        //    tVoucherDetails.PkVoucherTrnNo = 0;
                                        //    tVoucherDetails.VoucherSrNo = 3;
                                        //    tVoucherDetails.SignCode = 1;
                                        //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));// Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                        //    tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                        //    tVoucherDetails.Credit = 0;
                                        //    tVoucherDetails.SrNo = Others.Discount1;
                                        //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        //    tVoucherDetails.Narration = "";
                                        //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                                        //}
                                    }
                                    else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                    {
                                        if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value) != 0)
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
                                            tVoucherDetails.Narration = "Payment";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails); for firmwise entry
                                        }
                                        //if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value) != 0)
                                        //{
                                        //    tVoucherDetails = new TVoucherDetails();
                                        //    tVoucherDetails.PkVoucherTrnNo = 0;
                                        //    tVoucherDetails.VoucherSrNo = 3;
                                        //    tVoucherDetails.SignCode = 2;
                                        //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                                        //    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                        //    tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value); //0;
                                        //    tVoucherDetails.SrNo = Others.Discount1;
                                        //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        //    tVoucherDetails.Narration = "";
                                        //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                        //}
                                    }
                                    long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                    if (tempId != 0)
                                    {
                                        if (StrBillNo == "")
                                            StrBillNo = tempId.ToString();
                                        else
                                            StrBillNo = StrBillNo + "," + tempId.ToString();


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
                        #endregion
                        #region rbBillSelection
                        else if (rbBillSelection.Checked == true)
                        {

                            {
                                flagAdjs = true;
                                if (ValidationsDGBill() == true)
                                {
                                    bool flagetotal = true;
                                    int cnt = 0;
                                    double TotDiscAmt = 0;
                                    dg = dgBillSelection;
                                    for (int i = 0; i < dgBillSelection.Rows.Count; i++)
                                    {
                                        // setCompanyRatio(Convert.ToInt64(dgBillSelection.Rows[i].Cells[ColIndex.RefNo].Value.ToString()));
                                       if ((Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.Amount].Value.ToString()) != 0) || (Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value.ToString()) != 0))// && (Convert.ToBoolean(dgBillSelection.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true))
                                        {
                                            TotAmt = TotAmt + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.Amount].Value.ToString());
                                            TotDiscAmt = TotDiscAmt + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value.ToString());
                                        }
                                    }
                                    if (TotAmt <= 0 && TotDiscAmt<=0)
                                    {
                                        flagetotal = false;
                                        OMMessageBox.Show("Please fill atleast one bill.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                        txtAmount.Focus();
                                        return;
                                    }
                                    if (txtAmount.Text.Trim() != "")
                                    {
                                        if (TotAmt != Convert.ToDouble(txtAmount.Text))
                                        {
                                            flagetotal = false;
                                            OMMessageBox.Show("Amount does not match please check amount.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                            txtAmount.Focus();
                                            return;
                                        }
                                    }
                                    //TotAmt += TotDiscAmt;
                                    if (flagetotal == true)
                                    {
                                        flagetotal = ValidationChqNCreditCard();
                                        if (flagetotal == false)
                                        {
                                            cmbPayType_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                                            return;
                                        }
                                    }
                                    if (flagetotal == true)
                                    {

                                        if (TotAmt>0)
                                        {
                                        long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
                                        dbTVoucherEntry = new DBTVaucherEntry();
                                        tVoucherEntry = new TVoucherEntry();
                                        tVoucherEntry.PkVoucherNo = 0;
                                        tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                        tVoucherEntry.VoucherUserNo = 0;
                                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                        tVoucherEntry.VoucherTime = DBGetVal.ServerTime.Date; //Convert.ToDateTime("01-Jan-1900");
                                        tVoucherEntry.Reference = "";

                                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                        tVoucherEntry.BilledAmount = Convert.ToDouble(txtAmount.Text);//+ TotDiscAmt;
                                        tVoucherEntry.ChallanNo = "";
                                        tVoucherEntry.Remark = txtMainRemark.Text;
                                        //if (ControlUnder == 4)//cmbPayType.Text == "Cheque"
                                        //    tVoucherEntry.ChequeNo = Convert.ToInt64(txtChqNo.Text.Trim());
                                        //else
                                        tVoucherEntry.ChequeNo = 0;
                                        tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;// Convert.ToDateTime("01-01-1900");//Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);

                                        if (VoucherTypeCode == 130 || VoucherTypeCode == 30)
                                            tVoucherEntry.Narration = "Collection";
                                        else if (VoucherTypeCode == 31 || VoucherTypeCode == 131)
                                        { tVoucherEntry.Narration = "Payment"; }
                                        else { tVoucherEntry.Narration = ""; }
                                        tVoucherEntry.UserID = DBGetVal.UserID;
                                        tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                        tVoucherEntry.OrderType = 1;
                                        tVoucherEntry.PayTypeNo = ObjFunction.GetComboValue(cmbPayType);//Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                        tVoucherEntry.TransporterCode = 0;
                                        tVoucherEntry.TransPayType = 0;
                                        tVoucherEntry.LRNo = "";
                                        tVoucherEntry.TransportMode = 0;
                                        tVoucherEntry.TransNoOfItems = 0;
                                        tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); SetVoucherCompany(tVoucherEntry);

                                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 1;
                                            tVoucherDetails.SignCode = 2;
                                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                            tVoucherDetails.Debit = 0;
                                            tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text);// + TotDiscAmt;
                                            tVoucherDetails.SrNo = Others.Party;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "Collection";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                        }
                                        else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 1;
                                            tVoucherDetails.SignCode = 1;
                                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                            tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text);// + TotDiscAmt;
                                            tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                            tVoucherDetails.SrNo = Others.Party;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "Payment";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                        }


                                        for (int j = 0; j < dg.RowCount; j++)
                                        {
                                            if (Convert.ToDouble(dg.Rows[j].Cells[ColIndex.Amount].Value.ToString()) != 0)
                                            {
                                                cnt++;
                                                tVchRefDtls = new TVoucherRefDetails();
                                                tVchRefDtls.PkRefTrnNo = 0;
                                                tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                                tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                                tVchRefDtls.TypeOfRef = 2;
                                                tVchRefDtls.RefNo = Convert.ToInt64(dg.Rows[j].Cells[ColIndex.RefNo].Value);
                                                tVchRefDtls.DueDays = 0;
                                                tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                                tVchRefDtls.Amount = Convert.ToDouble(dg.Rows[j].Cells[ColIndex.Amount].Value);// + Convert.ToDouble(dg.Rows[j].Cells[ColIndex.DiscAmt1].Value);
                                                tVchRefDtls.DiscAmt = 0;// Convert.ToDouble(dg.Rows[j].Cells[ColIndex.DiscAmt1].Value);
                                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                                    tVchRefDtls.SignCode = 2;
                                                else
                                                    tVchRefDtls.SignCode = 1;

                                                tVchRefDtls.UserID = DBGetVal.UserID;
                                                tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                                tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                                dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                                                if (StrRBillNo == "")
                                                    StrRBillNo = "(" + dg.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVchRefDtls.Amount + "," + Convert.ToDateTime(dg.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";
                                                else
                                                    StrRBillNo = StrRBillNo + Environment.NewLine + "(" + dg.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVchRefDtls.Amount + "," + Convert.ToDateTime(dg.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";

                                            }
                                        }
                                        if (ControlUnder == 4)//cmbPayType.Text == "Cheque"
                                        {
                                            tVchChqCredit.PkSrNo = 0;
                                            tVchChqCredit.ChequeNo = Convert.ToString(txtChqNo.Text.Trim());
                                            tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                                            tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                                            tVchChqCredit.BranchNo = ObjFunction.GetComboValue(cmbBranch);
                                            tVchChqCredit.CreditCardNo = "";
                                            tVchChqCredit.Amount = Convert.ToDouble(txtAmount.Text);
                                            tVchChqCredit.PostFkVoucherNo = 0;
                                            tVchChqCredit.PostFkVoucherTrnNo = 0;
                                            tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                            dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                        }
                                        else if (ControlUnder == 5)//cmbPayType.Text == "Credit Card"
                                        {
                                            tVchChqCredit.PkSrNo = 0;
                                            tVchChqCredit.ChequeNo = Convert.ToString(txtCrCardNo.Text.Trim());
                                            tVchChqCredit.ChequeDate = Convert.ToDateTime("01-01-1900");
                                            tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbCrBank);
                                            tVchChqCredit.BranchNo = 0;// ObjFunction.GetComboValue(cmbCrBranch);
                                            tVchChqCredit.BankNo = 0;
                                            tVchChqCredit.BranchNo = 0;
                                            tVchChqCredit.CreditCardNo = Convert.ToString(txtCrCardNo.Text.Trim());
                                            tVchChqCredit.Amount = Convert.ToDouble(txtAmount.Text);
                                            tVchChqCredit.PostFkVoucherNo = 0;
                                            tVchChqCredit.PostFkVoucherTrnNo = 0;
                                            tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                            dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                        }
                                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 2;
                                            tVoucherDetails.SignCode = 1;
                                            tVoucherDetails.LedgerNo = LedgNo;
                                            tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text);
                                            tVoucherDetails.Credit = 0;
                                            tVoucherDetails.SrNo = 0;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "Collection";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry

                                            //if (TotDiscAmt != 0)
                                            //{
                                            //    tVoucherDetails = new TVoucherDetails();
                                            //    tVoucherDetails.PkVoucherTrnNo = 0;
                                            //    tVoucherDetails.VoucherSrNo = 3;
                                            //    tVoucherDetails.SignCode = 1;
                                            //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));// Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                            //    tVoucherDetails.Debit = TotDiscAmt;
                                            //    tVoucherDetails.Credit = 0;
                                            //    tVoucherDetails.SrNo = Others.Discount1;
                                            //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            //    tVoucherDetails.Narration = "";
                                            //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                            //}
                                        }
                                        else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 2;
                                            tVoucherDetails.SignCode = 2;
                                            tVoucherDetails.LedgerNo = LedgNo;
                                            tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                            tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text);
                                            tVoucherDetails.SrNo = 0;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "Payment";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry

                                            //if (TotDiscAmt != 0)
                                            //{
                                            //    tVoucherDetails = new TVoucherDetails();
                                            //    tVoucherDetails.PkVoucherTrnNo = 0;
                                            //    tVoucherDetails.VoucherSrNo = 3;
                                            //    tVoucherDetails.SignCode = 2;
                                            //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                                            //    tVoucherDetails.Debit = 0;
                                            //    tVoucherDetails.Credit = TotDiscAmt;
                                            //    tVoucherDetails.SrNo = Others.Discount1;
                                            //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            //    tVoucherDetails.Narration = "";
                                            //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                            //}
                                        }
                                        if (cnt > 0)
                                        {
                                            long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                            if (tempId != 0)
                                            {
                                                if (StrBillNo == "")
                                                    StrBillNo = tempId.ToString();
                                                else
                                                    StrBillNo = StrBillNo + "," + tempId.ToString();

                                                flag = true;
                                                flagAdjs = true;
                                                txtAmount.Text = "";
                                            }
                                            else
                                            {
                                                flag = false;
                                                flagAdjs = true;

                                            }
                                        }
                                        else
                                        {
                                            OMMessageBox.Show("Select Atleast One Bill.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                            return;
                                        }
                                    } // if txtamount >0 
                                    } // if flagetotal = true 
                                }
                            }

                        }
                        #endregion
                        #region rbAdjustment
                        else if (rbAdjustment.Checked == true)
                        {
                            if (ValidationChqNCreditCard() == false)
                            {
                                cmbPayType_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                                return;
                            }

                            if (txtAdjustment.Visible && txtAdjustment.Text != "")
                            {
                                long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
                                //int cnt = 0;

                                dbTVoucherEntry = new DBTVaucherEntry();
                                tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = 0;
                                tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                tVoucherEntry.VoucherUserNo = 0;
                                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                tVoucherEntry.VoucherTime = DBGetVal.ServerTime.Date; // Convert.ToDateTime("01-Jan-1900");
                                tVoucherEntry.Reference = "";

                                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                tVoucherEntry.BilledAmount = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                tVoucherEntry.ChallanNo = "";
                                tVoucherEntry.Remark = "";
                                tVoucherEntry.ChequeNo = 0;
                                tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;// Convert.ToDateTime("01-01-1900");//Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                tVoucherEntry.Narration = "";
                                tVoucherEntry.UserID = DBGetVal.UserID;
                                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                tVoucherEntry.OrderType = 1;
                                tVoucherEntry.PayTypeNo = ObjFunction.GetComboValue(cmbPayType);//Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                tVoucherEntry.TransporterCode = 0;
                                tVoucherEntry.TransPayType = 0;
                                tVoucherEntry.LRNo = "";
                                tVoucherEntry.TransportMode = 0;
                                tVoucherEntry.TransNoOfItems = 0;
                                tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);for firmwise entry


                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 2;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = 0;
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 1;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }

                                tVchRefDtls = new TVoucherRefDetails();
                                tVchRefDtls.PkRefTrnNo = 0;
                                tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                tVchRefDtls.TypeOfRef = 5;
                                tVchRefDtls.RefNo = 0;
                                tVchRefDtls.DueDays = 0;
                                tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                tVchRefDtls.Amount = Convert.ToDouble(txtAdjustment.Text);
                                tVchRefDtls.DiscAmt = 0;
                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                    tVchRefDtls.SignCode = 2;
                                else
                                    tVchRefDtls.SignCode = 1;

                                tVchRefDtls.UserID = DBGetVal.UserID;
                                tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                                if ((ControlUnder == 4) || (ControlUnder == 5))//cmbPayType.Text == "Cheque"
                                {
                                    tVchChqCredit.PkSrNo = 0;
                                    tVchChqCredit.ChequeNo = Convert.ToString(txtChqNo.Text.Trim());
                                    tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                                    tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                                    tVchChqCredit.BranchNo = ObjFunction.GetComboValue(cmbBranch);
                                    tVchChqCredit.CreditCardNo = "";
                                    tVchChqCredit.Amount = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVchChqCredit.PostFkVoucherNo = 0;
                                    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                }
                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 1;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else
                                        tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? ObjFunction.GetComboValue(cmbCrCompanyBank) : ObjFunction.GetComboValue(cmbCompanyBank));

                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 2;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else if (ControlUnder == 5)
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbCrCompanyBank);
                                    else if (ControlUnder == 4)
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbCompanyBank);
                                    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                                }
                                long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                if (tempId != 0)
                                {
                                    if (StrBillNo == "")
                                        StrBillNo = tempId.ToString();
                                    else
                                        StrBillNo = StrBillNo + "," + tempId.ToString();

                                    flag = true;
                                    flagAdjs = true;
                                }
                                else
                                {
                                    flag = false;
                                    flagAdjs = true;

                                }
                            }
                            else if (dgAdjustOpening.Visible)
                            {
                                long pkBill = 0;
                                //double Amount = 0;

                                if (ValidationdgAdjustOpening() == true)
                                {
                                    dbTVoucherEntry = new DBTVaucherEntry();
                                    for (int i = 0; i < dgAdjustOpening.Rows.Count; i++)
                                    {
                                        if (Convert.ToBoolean(dgAdjustOpening.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                                        {
                                            pkBill = Convert.ToInt64(dgAdjustOpening.Rows[i].Cells[ColIndex.PayTypeNo].Value);
                                            DataTable dtRefBill = ObjFunction.GetDataView("SELECT FkVoucherTrnNo, FkVoucherSrNo, RefNo, CompanyNo FROM TVoucherRefDetails where PkRefTrnNo=" + pkBill + "").Table;

                                            #region adjust bill's reference
                                            tVchRefDtls = new TVoucherRefDetails();
                                            tVchRefDtls.PkRefTrnNo = 0;
                                            tVchRefDtls.FkVoucherSrNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[1].ToString());
                                            tVchRefDtls.FkVoucherTrnNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[0].ToString());
                                            tVchRefDtls.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                            tVchRefDtls.TypeOfRef = 5;//tVchRefDtls.TypeOfRef = 2;
                                            tVchRefDtls.RefNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[2].ToString());
                                            tVchRefDtls.DueDays = 0;
                                            tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                            tVchRefDtls.Amount = Convert.ToDouble(dgAdjustOpening.Rows[i].Cells[ColIndex.Amount].Value);
                                            if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                                tVchRefDtls.SignCode = 2;
                                            else
                                                tVchRefDtls.SignCode = 1;
                                            tVchRefDtls.UserID = DBGetVal.UserID;
                                            tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                            tVchRefDtls.CompanyNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[3].ToString());
                                            dbTVoucherEntry.AddTVoucherRefDetails1(tVchRefDtls);
                                            #endregion


                                        }
                                    }



                                    if (dbTVoucherEntry.ExecuteNonQueryStatements() != 0)
                                    {
                                        flag = true;
                                        flagAdjs = true;
                                    }
                                    else
                                    {
                                        flag = true;
                                        flagAdjs = true;

                                    }
                                }
                            }
                        }
                        #endregion
                        #region rbAdvance
                        else if (rbAdvance.Checked == true)
                        {
                            if (txtAdvAmt.Text != "")
                            {
                                if (ValidationChqNCreditCard() == false)
                                {
                                    cmbPayType_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                                    return;
                                }
                                long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
                                //int cnt = 0;

                                dbTVoucherEntry = new DBTVaucherEntry();
                                tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = 0;
                                tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                tVoucherEntry.VoucherUserNo = 0;
                                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                tVoucherEntry.VoucherTime = DBGetVal.ServerTime.Date; //Convert.ToDateTime("01-Jan-1900");
                                tVoucherEntry.Reference = "";

                                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                tVoucherEntry.BilledAmount = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                tVoucherEntry.ChallanNo = "";
                                tVoucherEntry.Remark = txtRemark.Text.Trim();
                                tVoucherEntry.ChequeNo = 0;
                                tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date; //Convert.ToDateTime("01-01-1900");//Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                tVoucherEntry.Narration = "";
                                tVoucherEntry.UserID = DBGetVal.UserID;
                                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                tVoucherEntry.OrderType = 1;
                                tVoucherEntry.PayTypeNo = ObjFunction.GetComboValue(cmbPayType);//Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                tVoucherEntry.TransporterCode = 0;
                                tVoucherEntry.TransPayType = 0;
                                tVoucherEntry.LRNo = "";
                                tVoucherEntry.TransportMode = 0;
                                tVoucherEntry.TransNoOfItems = 0;
                                tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);for firmwise entry

                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 2;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = 0;
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 1;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }


                                if ((ControlUnder == 4) || (ControlUnder == 5))//cmbPayType.Text == "Cheque"
                                {
                                    tVchChqCredit.PkSrNo = 0;
                                    tVchChqCredit.ChequeNo = Convert.ToString(txtChqNo.Text.Trim());
                                    tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                                    tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                                    tVchChqCredit.BranchNo = ObjFunction.GetComboValue(cmbBranch);
                                    tVchChqCredit.CreditCardNo = "";
                                    tVchChqCredit.Amount = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVchChqCredit.PostFkVoucherNo = 0;
                                    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                }
                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 1;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else
                                        tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? ObjFunction.GetComboValue(cmbCrCompanyBank) : ObjFunction.GetComboValue(cmbCompanyBank));

                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 2;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else
                                        tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? 0 : ObjFunction.GetComboValue(cmbBank));
                                    // tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? ObjFunction.GetComboValue(cmbCrBank) : ObjFunction.GetComboValue(cmbBank));
                                    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                if (tempId != 0)
                                {
                                    if (StrBillNo == "")
                                        StrBillNo = tempId.ToString();
                                    else
                                        StrBillNo = StrBillNo + "," + tempId.ToString();

                                    flag = true;
                                    flagAdjs = true;
                                }
                                else
                                {
                                    flag = false;
                                    flagAdjs = true;

                                }
                            }
                        }
                        #endregion
                        #region rbAgainstAdv && rbAdjSalesReturn
                        else if (rbAgainstAdv.Checked == true || rbAdjSalesReturn.Checked == true)
                        {
                            long pkBill = 0;
                            //double Amount = 0;

                            if (ValidationdgBillAdv() == true)
                            {


                                dbTVoucherEntry = new DBTVaucherEntry();
                                for (int i = 0; i < dgBill.Rows.Count; i++)
                                {
                                    if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                                    {
                                        pkBill = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PayTypeNo].Value);

                                        #region adjust bill's reference
                                        tVchRefDtls = new TVoucherRefDetails();
                                        tVchRefDtls.PkRefTrnNo = 0;
                                        tVchRefDtls.FkVoucherSrNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.BVoucherSrNo].Value.ToString());// Convert.ToInt64(dtRefAdvance.Rows[0].ItemArray[1].ToString());
                                        tVchRefDtls.FkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.BFKVoucherNo].Value.ToString());// Convert.ToInt64(dtRefAdvance.Rows[0].ItemArray[0].ToString());
                                        tVchRefDtls.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                        tVchRefDtls.TypeOfRef = 2;
                                        tVchRefDtls.RefNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.RefNo].Value);// Convert.ToInt64(dtRefBill.Rows[0].ItemArray[2].ToString());
                                        tVchRefDtls.DueDays = 0;
                                        tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                        tVchRefDtls.Amount = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].Value);
                                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                            tVchRefDtls.SignCode = 2;
                                        else
                                            tVchRefDtls.SignCode = 1;
                                        tVchRefDtls.UserID = DBGetVal.UserID;
                                        tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                        tVchRefDtls.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtRefBill.Rows[0].ItemArray[3].ToString());
                                        dbTVoucherEntry.AddTVoucherRefDetails1(tVchRefDtls);
                                        #endregion

                                        #region adjust Advance's reference

                                        #endregion
                                    }
                                }

                                long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                if (tempId != 0)
                                {
                                    if (StrBillNo == "")
                                        StrBillNo = tempId.ToString();
                                    else
                                        StrBillNo = StrBillNo + "," + tempId.ToString();

                                    flag = true;
                                    flagAdjs = true;
                                }
                                else
                                {
                                    flag = true;
                                    flagAdjs = true;
                                }
                            }
                        }
                        #endregion
                        if (rbBillSelection.Checked == true || rbBillwise.Checked == true)
                        { setValuesDisc(); }
                    }
                    else
                    {
                        OMMessageBox.Show("Please Enter Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                    if (flag == true && flagAdjs == true)
                    {
                        OMMessageBox.Show("Voucher Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        if (rbBillwise.Checked == true || rbBillSelection.Checked == true || rbAdvance.Checked == true)
                        {
                            if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                            {
                                DialogResult ds = OMMessageBox.Show("Are you sure you want to Print " + ((VoucherType == 15) ? "Receipt" : "Payment") + " ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                                if (ds == DialogResult.Yes)
                                {
                                    PrintBill(StrBillNo, 0);
                                }
                                else if (ds == DialogResult.Cancel)
                                {
                                    PrintBill(StrBillNo, 1);
                                }
                            }
                            else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsPaymentPrint)))
                                {
                                    DialogResult ds = OMMessageBox.Show("Are you sure you want to Print " + ((VoucherType == 9) ? "Receipt" : "Payment") + " ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                                    if (ds == DialogResult.Yes)
                                    {
                                        PrintBill(StrBillNo, 0);
                                    }
                                    else if (ds == DialogResult.Cancel)
                                    {
                                        PrintBill(StrBillNo, 1);
                                    }
                                }
                            }
                        }

                        BindGrid();
                        if (rbAdjustment.Checked == true)
                        {
                            rbBillwise.Checked = true;
                            rbCheck_Changed();
                        }
                        else if (rbAdvance.Checked == true)
                        {
                            rbBillwise.Checked = true;
                            rbCheck_Changed();
                        }
                        else if (rbAgainstAdv.Checked == true || rbAdjSalesReturn.Checked == true)
                        {
                            rbBillwise.Checked = true;
                            rbCheck_Changed();
                            BindGridAdv();
                        }
                        cmbPartyName.Focus();
                        txtDiscPer.Text = "0.00";
                    }
                    else if (flagAdjs == false)
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
                    bool flag = false, flagAdjs = false;
                    string StrBillNo = "";
                    StrRBillNo = "";
                    DataGridView dg = null;
                    double TotAmt = 0;
                    if (Validations() == true)
                    {
                        #region rbBillwise
                        if (rbBillwise.Checked == true)
                        {
                            for (int j = 0; j < GridView.RowCount; j++)
                            {
                                if ( Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value.ToString()) != 0)
                                {
                                    //setCompanyRatio(Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value.ToString()));
                                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value.ToString()) + "", CommonFunctions.ConStr);
                                    dbTVoucherEntry = new DBTVaucherEntry();
                                    tVoucherEntry = new TVoucherEntry();
                                    tVoucherEntry.PkVoucherNo = 0;
                                    tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                    tVoucherEntry.VoucherUserNo = 0;
                                    tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                    tVoucherEntry.VoucherTime = DBGetVal.ServerTime;// Convert.ToDateTime("01-Jan-1900");
                                    tVoucherEntry.Reference = "";

                                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherEntry.BilledAmount =  Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                    tVoucherEntry.ChallanNo = "";
                                    tVoucherEntry.Remark = Convert.ToString(GridView.Rows[j].Cells[ColIndex.Remark].Value);
                                    tVoucherEntry.ChequeNo = 0;
                                    tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;// Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                    if (VoucherTypeCode == 130 || VoucherTypeCode == 30)
                                        tVoucherEntry.Narration = "Discount";
                                    else if (VoucherTypeCode == 31 || VoucherTypeCode == 131)

                                    {tVoucherEntry.Narration = "Discount";   }
                                    else {tVoucherEntry.Narration = "";                                    }
                                    tVoucherEntry.UserID = DBGetVal.UserID;
                                    tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                    tVoucherEntry.OrderType = 1;
                                    tVoucherEntry.PayTypeNo = 2; Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                    tVoucherEntry.TransporterCode = 0;
                                    tVoucherEntry.TransPayType = 0;
                                    tVoucherEntry.LRNo = "";
                                    tVoucherEntry.TransportMode = 0;
                                    tVoucherEntry.TransNoOfItems = 0;
                                    tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                    dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);// SetVoucherCompany(tVoucherEntry);for firmwise

                                    if (StrRBillNo == "")
                                        StrRBillNo = "(" + GridView.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVoucherEntry.BilledAmount + "," + Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";
                                    else
                                        StrRBillNo = StrRBillNo + Environment.NewLine + "(" + GridView.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVoucherEntry.BilledAmount + "," + Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";


                                    if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                    {
                                        tVoucherDetails = new TVoucherDetails();
                                        tVoucherDetails.PkVoucherTrnNo = 0;
                                        tVoucherDetails.VoucherSrNo = 1;
                                        tVoucherDetails.SignCode = 2;
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                        tVoucherDetails.Debit = 0;
                                        tVoucherDetails.Credit =  Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                        tVoucherDetails.SrNo = Others.Party;
                                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        tVoucherDetails.Narration = "";
                                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails); for firmwise

                                        // if discount > 0 then 
                                        //if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value) > 0)
                                        //                                        {
                                        //                                           tVoucherDetails = new TVoucherDetails();
                                        //                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                        //                                            tVoucherDetails.VoucherSrNo = 1;
                                        //                                           tVoucherDetails.SignCode = 2;
                                        //                                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                        //                                            tVoucherDetails.Debit = 0;
                                        //                                            tVoucherDetails.Credit =  Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                        //                                            tVoucherDetails.SrNo = Others.Party;
                                        //                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        //                                            tVoucherDetails.Narration = "";
                                        //                                           dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails); for firmwise
                                        //                                        }
                                        //

                                    }
                                    else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                    {
                                        tVoucherDetails = new TVoucherDetails();
                                        tVoucherDetails.PkVoucherTrnNo = 0;
                                        tVoucherDetails.VoucherSrNo = 1;
                                        tVoucherDetails.SignCode = 1;
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                        tVoucherDetails.Debit =  Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                        tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                        tVoucherDetails.SrNo = Others.Party;
                                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        tVoucherDetails.Narration = "Discount";
                                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                                    }

                                    tVchRefDtls = new TVoucherRefDetails();
                                    tVchRefDtls.PkRefTrnNo = 0;
                                    tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                    tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                    tVchRefDtls.TypeOfRef = 2;
                                    tVchRefDtls.RefNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.RefNo].Value);
                                    tVchRefDtls.DueDays = 0;
                                    tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                    tVchRefDtls.Amount = 0;// Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value); //tVoucherEntry.BilledAmount;
                                    tVchRefDtls.DiscAmt =  Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                    if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                        tVchRefDtls.SignCode = 2;
                                    else
                                        tVchRefDtls.SignCode = 1;

                                    tVchRefDtls.UserID = DBGetVal.UserID;
                                    tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                    tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                                    //if ((ControlUnder == 4) || (ControlUnder == 5))//GridView.Rows[j].Cells[ColIndex.PayType].Value.ToString() == "Cheque"
                                    //{
                                    //    tVchChqCredit.PkSrNo = 0;
                                    //    tVchChqCredit.ChequeNo = Convert.ToString(GridView.Rows[j].Cells[ColIndex.ChqNo].Value);
                                    //    tVchChqCredit.ChequeDate = Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                    //    tVchChqCredit.BankNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BankNo].Value);
                                    //    tVchChqCredit.BranchNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.BranchNo].Value);
                                    //    tVchChqCredit.CreditCardNo = "";
                                    //    tVchChqCredit.Amount = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    //    tVchChqCredit.PostFkVoucherNo = 0;
                                    //    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                    //    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                    //    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                    //}

                                    if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                    {
                                        //if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value) != 0)
                                        //{
                                        //    tVoucherDetails = new TVoucherDetails();
                                        //    tVoucherDetails.PkVoucherTrnNo = 0;
                                        //    tVoucherDetails.VoucherSrNo = 2;
                                        //    tVoucherDetails.SignCode = 1;
                                        //    tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                        //    tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                        //    tVoucherDetails.Credit = 0;
                                        //    tVoucherDetails.SrNo = 0;
                                        //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        //    tVoucherDetails.Narration = "";
                                        //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);
                                        //}
                                        if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value) != 0)
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 3;
                                            tVoucherDetails.SignCode = 1;
                                            tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));// Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                            tVoucherDetails.Debit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value);
                                            tVoucherDetails.Credit = 0;
                                            tVoucherDetails.SrNo = Others.Discount1;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "Discount";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);
                                        }
                                    }
                                    else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                    {
                                        //if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value) != 0)
                                        //{
                                        //    tVoucherDetails = new TVoucherDetails();
                                        //    tVoucherDetails.PkVoucherTrnNo = 0;
                                        //    tVoucherDetails.VoucherSrNo = 2;
                                        //    tVoucherDetails.SignCode = 2;
                                        //    tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                        //    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                        //    tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value); //0;
                                        //    tVoucherDetails.SrNo = 0;
                                        //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                        //    tVoucherDetails.Narration = "";
                                        //    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails); for firmwise entry
                                        //}
                                        if (Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value) != 0)
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 3;
                                            tVoucherDetails.SignCode = 2;
                                            tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                                            tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                            tVoucherDetails.Credit = Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.DiscAmt].Value); //0;
                                            tVoucherDetails.SrNo = Others.Discount1;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "Discount";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                        }
                                    }
                                    long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                    if (tempId != 0)
                                    {
                                        if (StrBillNo == "")
                                            StrBillNo = tempId.ToString();
                                        else
                                            StrBillNo = StrBillNo + "," + tempId.ToString();


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
                        #endregion
                        #region rbBillSelection
                        else if (rbBillSelection.Checked == true)
                        {

                            {
                                flagAdjs = true;
                                if (ValidationsDGBill() == true)
                                {
                                    bool flagetotal = false;
                                    int cnt = 0;
                                    double TotDiscAmt = 0;
                                    dg = dgBillSelection;
                                    for (int i = 0; i < dgBillSelection.Rows.Count; i++)
                                    {
                                        // setCompanyRatio(Convert.ToInt64(dgBillSelection.Rows[i].Cells[ColIndex.RefNo].Value.ToString()));
                                        if ((Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value.ToString()) != 0))// && (Convert.ToBoolean(dgBillSelection.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true))
                                        {
                                            TotAmt = TotAmt + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.Amount].Value.ToString());
                                            TotDiscAmt = TotDiscAmt + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value.ToString());
                                        }
                                    }
                                     if (TotDiscAmt>0 )
                                    flagetotal = true;
                                    //if (TotAmt <= 0)
                                    //{
                                    //    flagetotal = false;
                                    //    OMMessageBox.Show("Please fill atleast one bill.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                    //    txtAmount.Focus();
                                    //    return;
                                    //}
                                    //if (txtAmount.Text.Trim() != "")
                                    //{
                                    //    if (TotAmt != Convert.ToDouble(txtAmount.Text))
                                    //    {
                                    //        flagetotal = false;
                                    //        OMMessageBox.Show("Amount does not match please check amount.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                    //        txtAmount.Focus();
                                    //        return;
                                    //    }
                                    //}
                                    //TotAmt += TotDiscAmt;
                                    //if (flagetotal == true)
                                    //{
                                    //    flagetotal = ValidationChqNCreditCard();
                                    //    if (flagetotal == false)
                                    //    {
                                    //        cmbPayType_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                                    //        return;
                                    //    }
                                    //}
                                    if (flagetotal == true)
                                    {
                                        long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
                                        dbTVoucherEntry = new DBTVaucherEntry();
                                        tVoucherEntry = new TVoucherEntry();
                                        tVoucherEntry.PkVoucherNo = 0;
                                        tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                        tVoucherEntry.VoucherUserNo = 0;
                                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                        tVoucherEntry.VoucherTime = DBGetVal.ServerTime.Date; //Convert.ToDateTime("01-Jan-1900");
                                        tVoucherEntry.Reference = "";

                                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                        tVoucherEntry.BilledAmount = TotDiscAmt;// Convert.ToDouble(txtAmount.Text) + TotDiscAmt;
                                        tVoucherEntry.ChallanNo = "";
                                        tVoucherEntry.Remark = txtMainRemark.Text;
                                        //if (ControlUnder == 4)//cmbPayType.Text == "Cheque"
                                        //    tVoucherEntry.ChequeNo = Convert.ToInt64(txtChqNo.Text.Trim());
                                        //else
                                        tVoucherEntry.ChequeNo = 0;
                                        tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;// Convert.ToDateTime("01-01-1900");//Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                        tVoucherEntry.Narration = "Discount";
                                        tVoucherEntry.UserID = DBGetVal.UserID;
                                        tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                        tVoucherEntry.OrderType = 1;
                                        tVoucherEntry.PayTypeNo = 2;// ObjFunction.GetComboValue(cmbPayType);//Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                        tVoucherEntry.TransporterCode = 0;
                                        tVoucherEntry.TransPayType = 0;
                                        tVoucherEntry.LRNo = "";
                                        tVoucherEntry.TransportMode = 0;
                                        tVoucherEntry.TransNoOfItems = 0;
                                        tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); SetVoucherCompany(tVoucherEntry);

                                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 1;
                                            tVoucherDetails.SignCode = 2;
                                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                            tVoucherDetails.Debit = 0;
                                            tVoucherDetails.Credit = TotDiscAmt;// convert.ToDouble(txtAmount.Text) + TotDiscAmt;
                                            tVoucherDetails.SrNo = Others.Party;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "Discount";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                        }
                                        else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                        {
                                            tVoucherDetails = new TVoucherDetails();
                                            tVoucherDetails.PkVoucherTrnNo = 0;
                                            tVoucherDetails.VoucherSrNo = 1;
                                            tVoucherDetails.SignCode = 1;
                                            tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                            tVoucherDetails.Debit = TotDiscAmt;  //Convert.ToDouble(txtAmount.Text) + 
                                            tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                            tVoucherDetails.SrNo = Others.Party;
                                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherDetails.Narration = "";
                                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                        }


                                        for (int j = 0; j < dg.RowCount; j++)
                                        {
                                            if (Convert.ToDouble(dg.Rows[j].Cells[ColIndex.DiscAmt1].Value.ToString()) != 0)
                                            {
                                                cnt++;
                                                tVchRefDtls = new TVoucherRefDetails();
                                                tVchRefDtls.PkRefTrnNo = 0;
                                                tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                                tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                                tVchRefDtls.TypeOfRef = 2;
                                                tVchRefDtls.RefNo = Convert.ToInt64(dg.Rows[j].Cells[ColIndex.RefNo].Value);
                                                tVchRefDtls.DueDays = 0;
                                                tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                                tVchRefDtls.Amount = 0;// Convert.ToDouble(dg.Rows[j].Cells[ColIndex.DiscAmt1].Value);// + Convert.ToDouble(dg.Rows[j].Cells[ColIndex.DiscA mt1].Value);
                                                tVchRefDtls.DiscAmt = Convert.ToDouble(dg.Rows[j].Cells[ColIndex.DiscAmt1].Value);// Convert.ToDouble(dg.Rows[j].Cells[ColIndex.DiscAmt1].Value);
                                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                                    tVchRefDtls.SignCode = 2;
                                                else
                                                    tVchRefDtls.SignCode = 1;

                                                tVchRefDtls.UserID = DBGetVal.UserID;
                                                tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                                tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                                dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                                                if (StrRBillNo == "")
                                                    StrRBillNo = "(" + dg.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVchRefDtls.Amount + "," + Convert.ToDateTime(dg.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";
                                                else
                                                    StrRBillNo = StrRBillNo + Environment.NewLine + "(" + dg.Rows[j].Cells[ColIndex.BillNo].Value.ToString() + "," + tVchRefDtls.Amount + "," + Convert.ToDateTime(dg.Rows[j].Cells[ColIndex.BillDate].Value.ToString()).ToString(Format.DDMMMYY) + ")";

                                            }
                                        }
                                        //if (ControlUnder == 4)//cmbPayType.Text == "Cheque"
                                        //{
                                        //    tVchChqCredit.PkSrNo = 0;
                                        //    tVchChqCredit.ChequeNo = Convert.ToString(txtChqNo.Text.Trim());
                                        //    tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                                        //    tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                                        //    tVchChqCredit.BranchNo = ObjFunction.GetComboValue(cmbBranch);
                                        //    tVchChqCredit.CreditCardNo = "";
                                        //    tVchChqCredit.Amount = Convert.ToDouble(txtAmount.Text);
                                        //    tVchChqCredit.PostFkVoucherNo = 0;
                                        //    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                        //    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                        //    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                        //}
                                        //else if (ControlUnder == 5)//cmbPayType.Text == "Credit Card"
                                        //{
                                        //    tVchChqCredit.PkSrNo = 0;
                                        //    tVchChqCredit.ChequeNo = Convert.ToString(txtCrCardNo.Text.Trim());
                                        //    tVchChqCredit.ChequeDate = Convert.ToDateTime("01-01-1900");
                                        //    tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbCrBank);
                                        //    tVchChqCredit.BranchNo = 0;// ObjFunction.GetComboValue(cmbCrBranch);
                                        //    tVchChqCredit.BankNo = 0;
                                        //    tVchChqCredit.BranchNo = 0;
                                        //    tVchChqCredit.CreditCardNo = Convert.ToString(txtCrCardNo.Text.Trim());
                                        //    tVchChqCredit.Amount = Convert.ToDouble(txtAmount.Text);
                                        //    tVchChqCredit.PostFkVoucherNo = 0;
                                        //    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                        //    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                        //    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                        //}
                                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                        {
                                            //tVoucherDetails = new TVoucherDetails();
                                            //tVoucherDetails.PkVoucherTrnNo = 0;
                                            //tVoucherDetails.VoucherSrNo = 2;
                                            //tVoucherDetails.SignCode = 1;
                                            //tVoucherDetails.LedgerNo = LedgNo;
                                            //tVoucherDetails.Debit = Convert.ToDouble(txtAmount.Text);
                                            //tVoucherDetails.Credit = 0;
                                            //tVoucherDetails.SrNo = 0;
                                            //tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            //tVoucherDetails.Narration = "";
                                            //dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry

                                            if (TotDiscAmt != 0)
                                            {
                                                tVoucherDetails = new TVoucherDetails();
                                                tVoucherDetails.PkVoucherTrnNo = 0;
                                                tVoucherDetails.VoucherSrNo = 3;
                                                tVoucherDetails.SignCode = 1;
                                                tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));// Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.LedgerNo].Value);
                                                tVoucherDetails.Debit = TotDiscAmt;
                                                tVoucherDetails.Credit = 0;
                                                tVoucherDetails.SrNo = Others.Discount1;
                                                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                                tVoucherDetails.Narration = "Discount";
                                                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                            }
                                        }
                                        else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                        {
                                            //tVoucherDetails = new TVoucherDetails();
                                            //tVoucherDetails.PkVoucherTrnNo = 0;
                                            //tVoucherDetails.VoucherSrNo = 2;
                                            //tVoucherDetails.SignCode = 2;
                                            //tVoucherDetails.LedgerNo = LedgNo;
                                            //tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                            //tVoucherDetails.Credit = Convert.ToDouble(txtAmount.Text);
                                            //tVoucherDetails.SrNo = 0;
                                            //tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                            //tVoucherDetails.Narration = "";
                                            //dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry

                                            if (TotDiscAmt != 0)
                                            {
                                                tVoucherDetails = new TVoucherDetails();
                                                tVoucherDetails.PkVoucherTrnNo = 0;
                                                tVoucherDetails.VoucherSrNo = 3;
                                                tVoucherDetails.SignCode = 2;
                                                tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                                                tVoucherDetails.Debit = 0;
                                                tVoucherDetails.Credit = TotDiscAmt;
                                                tVoucherDetails.SrNo = Others.Discount1;
                                                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                                tVoucherDetails.Narration = "Discount";
                                                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                            }
                                        }
                                        if (cnt > 0)
                                        {
                                            long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                            if (tempId != 0)
                                            {
                                                if (StrBillNo == "")
                                                    StrBillNo = tempId.ToString();
                                                else
                                                    StrBillNo = StrBillNo + "," + tempId.ToString();

                                                flag = true;
                                                flagAdjs = true;
                                                txtAmount.Text = "";
                                            }
                                            else
                                            {
                                                flag = false;
                                                flagAdjs = true;

                                            }
                                        }
                                        else
                                        {
                                            OMMessageBox.Show("Select Atleast One Bill.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                            return;
                                        }
                                    }
                                }
                            }

                        }
                        #endregion
                        #region rbAdjustment
                        else if (rbAdjustment.Checked == true)
                        {
                            if (ValidationChqNCreditCard() == false)
                            {
                                cmbPayType_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                                return;
                            }

                            if (txtAdjustment.Visible && txtAdjustment.Text != "")
                            {
                                long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
                                //int cnt = 0;

                                dbTVoucherEntry = new DBTVaucherEntry();
                                tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = 0;
                                tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                tVoucherEntry.VoucherUserNo = 0;
                                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                tVoucherEntry.VoucherTime = DBGetVal.ServerTime.Date; // Convert.ToDateTime("01-Jan-1900");
                                tVoucherEntry.Reference = "";

                                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                tVoucherEntry.BilledAmount = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                tVoucherEntry.ChallanNo = "";
                                tVoucherEntry.Remark = "";
                                tVoucherEntry.ChequeNo = 0;
                                tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;// Convert.ToDateTime("01-01-1900");//Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                tVoucherEntry.Narration = "";
                                tVoucherEntry.UserID = DBGetVal.UserID;
                                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                tVoucherEntry.OrderType = 1;
                                tVoucherEntry.PayTypeNo = ObjFunction.GetComboValue(cmbPayType);//Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                tVoucherEntry.TransporterCode = 0;
                                tVoucherEntry.TransPayType = 0;
                                tVoucherEntry.LRNo = "";
                                tVoucherEntry.TransportMode = 0;
                                tVoucherEntry.TransNoOfItems = 0;
                                tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);for firmwise entry


                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 2;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = 0;
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 1;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }

                                tVchRefDtls = new TVoucherRefDetails();
                                tVchRefDtls.PkRefTrnNo = 0;
                                tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                                tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                                tVchRefDtls.TypeOfRef = 5;
                                tVchRefDtls.RefNo = 0;
                                tVchRefDtls.DueDays = 0;
                                tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                tVchRefDtls.Amount = Convert.ToDouble(txtAdjustment.Text);
                                tVchRefDtls.DiscAmt = 0;
                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                    tVchRefDtls.SignCode = 2;
                                else
                                    tVchRefDtls.SignCode = 1;

                                tVchRefDtls.UserID = DBGetVal.UserID;
                                tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                tVchRefDtls.CompanyNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);

                                if ((ControlUnder == 4) || (ControlUnder == 5))//cmbPayType.Text == "Cheque"
                                {
                                    tVchChqCredit.PkSrNo = 0;
                                    tVchChqCredit.ChequeNo = Convert.ToString(txtChqNo.Text.Trim());
                                    tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                                    tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                                    tVchChqCredit.BranchNo = ObjFunction.GetComboValue(cmbBranch);
                                    tVchChqCredit.CreditCardNo = "";
                                    tVchChqCredit.Amount = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVchChqCredit.PostFkVoucherNo = 0;
                                    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                }
                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 1;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else
                                        tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? ObjFunction.GetComboValue(cmbCrCompanyBank) : ObjFunction.GetComboValue(cmbCompanyBank));

                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);// SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 2;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else if (ControlUnder == 5)
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbCrCompanyBank);
                                    else if (ControlUnder == 4)
                                        tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbCompanyBank);
                                    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdjustment.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                                }
                                long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                if (tempId != 0)
                                {
                                    if (StrBillNo == "")
                                        StrBillNo = tempId.ToString();
                                    else
                                        StrBillNo = StrBillNo + "," + tempId.ToString();

                                    flag = true;
                                    flagAdjs = true;
                                }
                                else
                                {
                                    flag = false;
                                    flagAdjs = true;

                                }
                            }
                            else if (dgAdjustOpening.Visible)
                            {
                                long pkBill = 0;
                                //double Amount = 0;

                                if (ValidationdgAdjustOpening() == true)
                                {
                                    dbTVoucherEntry = new DBTVaucherEntry();
                                    for (int i = 0; i < dgAdjustOpening.Rows.Count; i++)
                                    {
                                        if (Convert.ToBoolean(dgAdjustOpening.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                                        {
                                            pkBill = Convert.ToInt64(dgAdjustOpening.Rows[i].Cells[ColIndex.PayTypeNo].Value);
                                            DataTable dtRefBill = ObjFunction.GetDataView("SELECT FkVoucherTrnNo, FkVoucherSrNo, RefNo, CompanyNo FROM TVoucherRefDetails where PkRefTrnNo=" + pkBill + "").Table;

                                            #region adjust bill's reference
                                            tVchRefDtls = new TVoucherRefDetails();
                                            tVchRefDtls.PkRefTrnNo = 0;
                                            tVchRefDtls.FkVoucherSrNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[1].ToString());
                                            tVchRefDtls.FkVoucherTrnNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[0].ToString());
                                            tVchRefDtls.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                            tVchRefDtls.TypeOfRef = 5;//tVchRefDtls.TypeOfRef = 2;
                                            tVchRefDtls.RefNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[2].ToString());
                                            tVchRefDtls.DueDays = 0;
                                            tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                            tVchRefDtls.Amount = Convert.ToDouble(dgAdjustOpening.Rows[i].Cells[ColIndex.Amount].Value);
                                            if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                                tVchRefDtls.SignCode = 2;
                                            else
                                                tVchRefDtls.SignCode = 1;
                                            tVchRefDtls.UserID = DBGetVal.UserID;
                                            tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                            tVchRefDtls.CompanyNo = Convert.ToInt64(dtRefBill.Rows[0].ItemArray[3].ToString());
                                            dbTVoucherEntry.AddTVoucherRefDetails1(tVchRefDtls);
                                            #endregion


                                        }
                                    }



                                    if (dbTVoucherEntry.ExecuteNonQueryStatements() != 0)
                                    {
                                        flag = true;
                                        flagAdjs = true;
                                    }
                                    else
                                    {
                                        flag = true;
                                        flagAdjs = true;

                                    }
                                }
                            }
                        }
                        #endregion
                        #region rbAdvance
                        else if (rbAdvance.Checked == true)
                        {
                            if (txtAdvAmt.Text != "")
                            {
                                if (ValidationChqNCreditCard() == false)
                                {
                                    cmbPayType_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                                    return;
                                }
                                long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
                                //int cnt = 0;

                                dbTVoucherEntry = new DBTVaucherEntry();
                                tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = 0;
                                tVoucherEntry.VoucherTypeCode = VoucherTypeCode;
                                tVoucherEntry.VoucherUserNo = 0;
                                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpVoucherDate.Text);
                                tVoucherEntry.VoucherTime = DBGetVal.ServerTime.Date; //Convert.ToDateTime("01-Jan-1900");
                                tVoucherEntry.Reference = "";

                                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                tVoucherEntry.BilledAmount = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                tVoucherEntry.ChallanNo = "";
                                tVoucherEntry.Remark = txtRemark.Text.Trim();
                                tVoucherEntry.ChequeNo = 0;
                                tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date; //Convert.ToDateTime("01-01-1900");//Convert.ToDateTime(GridView.Rows[j].Cells[ColIndex.ChqDate].Value);
                                tVoucherEntry.Narration = "";
                                tVoucherEntry.UserID = DBGetVal.UserID;
                                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                tVoucherEntry.OrderType = 1;
                                tVoucherEntry.PayTypeNo = ObjFunction.GetComboValue(cmbPayType);//Convert.ToInt64(GridView.Rows[j].Cells[ColIndex.PayTypeNo].Value);

                                tVoucherEntry.TransporterCode = 0;
                                tVoucherEntry.TransPayType = 0;
                                tVoucherEntry.LRNo = "";
                                tVoucherEntry.TransportMode = 0;
                                tVoucherEntry.TransNoOfItems = 0;
                                tVoucherEntry.LedgerNo = Convert.ToInt64(cmbPartyName.SelectedValue);
                                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);for firmwise entry

                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 2;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = 0;
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 1;
                                    tVoucherDetails.SignCode = 1;
                                    tVoucherDetails.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.SrNo = Others.Party;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }


                                if ((ControlUnder == 4) || (ControlUnder == 5))//cmbPayType.Text == "Cheque"
                                {
                                    tVchChqCredit.PkSrNo = 0;
                                    tVchChqCredit.ChequeNo = Convert.ToString(txtChqNo.Text.Trim());
                                    tVchChqCredit.ChequeDate = Convert.ToDateTime(dtpChqDate.Text.Trim());
                                    tVchChqCredit.BankNo = ObjFunction.GetComboValue(cmbBank);
                                    tVchChqCredit.BranchNo = ObjFunction.GetComboValue(cmbBranch);
                                    tVchChqCredit.CreditCardNo = "";
                                    tVchChqCredit.Amount = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVchChqCredit.PostFkVoucherNo = 0;
                                    tVchChqCredit.PostFkVoucherTrnNo = 0;
                                    tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                                    dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                                }
                                if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 1;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else
                                        tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? ObjFunction.GetComboValue(cmbCrCompanyBank) : ObjFunction.GetComboValue(cmbCompanyBank));

                                    tVoucherDetails.Debit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.Credit = 0;
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                                {
                                    tVoucherDetails = new TVoucherDetails();
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                    tVoucherDetails.VoucherSrNo = 2;
                                    tVoucherDetails.SignCode = 2;

                                    if (ControlUnder == 2)//cmbPayType.Text == "Cash"
                                        tVoucherDetails.LedgerNo = ObjQry.ReturnLong("Select LedgerNo From MPayTypeLedger Where PayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                                    else
                                        tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? 0 : ObjFunction.GetComboValue(cmbBank));
                                    // tVoucherDetails.LedgerNo = ((ControlUnder == 5) ? ObjFunction.GetComboValue(cmbCrBank) : ObjFunction.GetComboValue(cmbBank));
                                    tVoucherDetails.Debit = 0;//Convert.ToDouble(GridView.Rows[j].Cells[ColIndex.Amount].Value);
                                    tVoucherDetails.Credit = Convert.ToDouble(txtAdvAmt.Text.Replace("-", ""));
                                    tVoucherDetails.SrNo = 0;
                                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                                    tVoucherDetails.Narration = "";
                                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); //SetVoucherDetailsCompany(tVoucherDetails);for firmwise entry
                                }
                                long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                if (tempId != 0)
                                {
                                    if (StrBillNo == "")
                                        StrBillNo = tempId.ToString();
                                    else
                                        StrBillNo = StrBillNo + "," + tempId.ToString();

                                    flag = true;
                                    flagAdjs = true;
                                }
                                else
                                {
                                    flag = false;
                                    flagAdjs = true;

                                }
                            }
                        }
                        #endregion
                        #region rbAgainstAdv && rbAdjSalesReturn
                        else if (rbAgainstAdv.Checked == true || rbAdjSalesReturn.Checked == true)
                        {
                            long pkBill = 0;
                            //double Amount = 0;

                            if (ValidationdgBillAdv() == true)
                            {


                                dbTVoucherEntry = new DBTVaucherEntry();
                                for (int i = 0; i < dgBill.Rows.Count; i++)
                                {
                                    if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                                    {
                                        pkBill = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PayTypeNo].Value);

                                        #region adjust bill's reference
                                        tVchRefDtls = new TVoucherRefDetails();
                                        tVchRefDtls.PkRefTrnNo = 0;
                                        tVchRefDtls.FkVoucherSrNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.BVoucherSrNo].Value.ToString());// Convert.ToInt64(dtRefAdvance.Rows[0].ItemArray[1].ToString());
                                        tVchRefDtls.FkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.BFKVoucherNo].Value.ToString());// Convert.ToInt64(dtRefAdvance.Rows[0].ItemArray[0].ToString());
                                        tVchRefDtls.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                                        tVchRefDtls.TypeOfRef = 2;
                                        tVchRefDtls.RefNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.RefNo].Value);// Convert.ToInt64(dtRefBill.Rows[0].ItemArray[2].ToString());
                                        tVchRefDtls.DueDays = 0;
                                        tVchRefDtls.DueDate = DBGetVal.ServerTime;
                                        tVchRefDtls.Amount = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].Value);
                                        if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                                            tVchRefDtls.SignCode = 2;
                                        else
                                            tVchRefDtls.SignCode = 1;
                                        tVchRefDtls.UserID = DBGetVal.UserID;
                                        tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                                        tVchRefDtls.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtRefBill.Rows[0].ItemArray[3].ToString());
                                        dbTVoucherEntry.AddTVoucherRefDetails1(tVchRefDtls);
                                        #endregion

                                        #region adjust Advance's reference

                                        #endregion
                                    }
                                }

                                long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                                if (tempId != 0)
                                {
                                    if (StrBillNo == "")
                                        StrBillNo = tempId.ToString();
                                    else
                                        StrBillNo = StrBillNo + "," + tempId.ToString();

                                    flag = true;
                                    flagAdjs = true;
                                }
                                else
                                {
                                    flag = true;
                                    flagAdjs = true;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        OMMessageBox.Show("Please Enter Amount......... ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                    if (flag == true && flagAdjs == true)
                    {
                        OMMessageBox.Show("Voucher Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        if (rbBillwise.Checked == true || rbBillSelection.Checked == true || rbAdvance.Checked == true)
                        {
                            if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                            {
                                DialogResult ds = OMMessageBox.Show("Are you sure you want to Print " + ((VoucherType == 15) ? "Receipt" : "Payment") + " ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                                if (ds == DialogResult.Yes)
                                {
                                    PrintBill(StrBillNo, 0);
                                }
                                else if (ds == DialogResult.Cancel)
                                {
                                    PrintBill(StrBillNo, 1);
                                }
                            }
                            else if ((VoucherType == VchType.Purchase) || (VoucherType == VchType.DPurchase))
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsPaymentPrint)))
                                {
                                    DialogResult ds = OMMessageBox.Show("Are you sure you want to Print " + ((VoucherType == 9) ? "Receipt" : "Payment") + " ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                                    if (ds == DialogResult.Yes)
                                    {
                                        PrintBill(StrBillNo, 0);
                                    }
                                    else if (ds == DialogResult.Cancel)
                                    {
                                        PrintBill(StrBillNo, 1);
                                    }
                                }
                            }
                        }

                        BindGrid();
                        if (rbAdjustment.Checked == true)
                        {
                            rbBillwise.Checked = true;
                            rbCheck_Changed();
                        }
                        else if (rbAdvance.Checked == true)
                        {
                            rbBillwise.Checked = true;
                            rbCheck_Changed();
                        }
                        else if (rbAgainstAdv.Checked == true || rbAdjSalesReturn.Checked == true)
                        {
                            rbBillwise.Checked = true;
                            rbCheck_Changed();
                            BindGridAdv();
                        }
                        cmbPartyName.Focus();
                        txtDiscPer.Text = "0.00";
                    }
                    else if (flagAdjs == false)
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

        public void PrintBill(string PkVoucherNo, int PType)
        {
            string[] ReportSession;
            DataTable dtPrint = ObjFunction.GetDataView("SELECT VoucherDate, BilledAmount, Remark,IsNull((Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=501 And FkVoucherNo=PkVoucherNo),0) as PartyAmount ,IsNull( (Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=502 And FkVoucherNo=PkVoucherNo),0) as DiscAmt  , vouchertime FROM TVoucherEntry Where PkVoucherNo In(" + PkVoucherNo + ") ").Table;
            for (int i = 0; i < dtPrint.Rows.Count; i++)
            {

                ReportSession = new string[12];
                ReportSession[0] = DBGetVal.FirmName;
                if ((VoucherType == 15) || (VoucherType == 115))
                {
                    ReportSession[1] = "Receipt";
                }
                else
                { ReportSession[1] = "Payment"; }
                ReportSession[2] = dtPrint.Rows[i].ItemArray[0].ToString();
                ReportSession[3] = (Convert.ToDouble(dtPrint.Rows[i].ItemArray[3].ToString()) - Convert.ToDouble(dtPrint.Rows[i].ItemArray[4].ToString())).ToString();
                ReportSession[4] = NumberToWordsIndian.getWords(ReportSession[3].ToString());
                ReportSession[5] = dtPrint.Rows[i].ItemArray[2].ToString();
                ReportSession[6] = cmbPartyName.Text;
                ReportSession[7] = StrRBillNo;
                ReportSession[8] = dtPrint.Rows[i].ItemArray[4].ToString();
                if (Convert.ToInt32(cmbCompanyBank.SelectedValue) != 0)
                {
                    ReportSession[9] = cmbCompanyBank.Text.ToUpper();
                }
                else
                {
                    ReportSession[9] = "";

                }
                ReportSession[10] = txtChqNo.Text.ToUpper();
                ReportSession[11] = dtPrint.Rows[i].ItemArray[5].ToString();

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
                            if ((VoucherType == 15) || (VoucherType == 115))
                            {
                                DisplayMessage("Receipt Print Successfully!!!");
                            }
                            else
                            {
                                DisplayMessage(" Payment Print Successfully!!!");
                            }
                        }
                        else
                        {
                            if ((VoucherType == 15) || (VoucherType == 115))
                            {
                                DisplayMessage("Receipt not Print !!!");
                            }
                            else
                            {
                                DisplayMessage("Payment not Print !!!");

                            }
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //
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
                if (rbBillwise.Checked == true || rbBillSelection.Checked == true)
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
                                {
                                    GridView.CurrentCell = GridView[ColIndex.PayType, GridView.CurrentCell.RowIndex];
                                    CalculateRecAmount();
                                }
                            }
                        }
                        else if (GridView.CurrentCell.ColumnIndex == ColIndex.DiscAmt)
                        {
                            if (GridView.CurrentCell.Value != null)
                            {
                                if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                                    GridView.CurrentCell.ErrorText = "Please Enter Valid Disc Amt";
                                else
                                    GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex];
                            }
                        }
                        else if (GridView.CurrentCell.ColumnIndex == ColIndex.Remark)
                        {
                            if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                            {
                                GridView.Focus();
                                GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                            }
                            else { BtnSave.Focus(); }


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
            public static int Chk = 14;
            public static int DiscAmt = 14;
            public static int Remark = 15;
            public static int DiscAmt1 = 15;
            public static int BFKVoucherNo = 15;
            public static int BVoucherSrNo = 16;
        }

        private void cmbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChqNo.Text = "";
                txtCrCardNo.Text = "";
                cmbBank.SelectedIndex = 0;
                cmbBranch.SelectedIndex = 0;
                if (VoucherType == 15)
                {
                    if (DBGetVal.KachhaFirm == false)
                    {
                        VoucherType = VchType.Sales;
                    }
                    else
                    {
                        VoucherType = VchType.DSales;
                    }
                }
                else if (VoucherType == 9)
                {
                    if (DBGetVal.KachhaFirm == false)
                    {
                        VoucherType = VchType.Purchase;
                    }
                    else
                    {
                        VoucherType = VchType.DPurchase;
                    }

                }
                e.SuppressKeyPress = true;
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
                    else if (pnlAdjustment.Visible == true)
                        p = pnlAdjustment;
                    else
                        p = pnlBillwise;

                    e.SuppressKeyPress = true;

                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + lstPayType.SelectedValue + "", CommonFunctions.ConStr);
                    if (ControlUnder == 1 || ControlUnder == 4 || ControlUnder == 5)
                    {
                        if (ControlUnder == 4 || ControlUnder == 5)
                        {
                            int y = pnlMain.Location.Y + p.Location.Y + GridView.Location.Y;
                            y = y + ((p.Height - GridView.Height) / 2);
                            pnlPaytype.Visible = false;
                            BtnSave.Enabled = false;

                            if (ControlUnder == 4)
                            {
                                if ((VoucherType == 9) || (VoucherType == 109))
                                {
                                    //label4.Visible = false;
                                    //label2.Visible = false;
                                    //cmbCrBank.Visible = false;
                                    //cmbCrBranch.Visible = false;
                                    label11.Visible = false;
                                    label12.Visible = false;
                                    cmbBank.Visible = false;
                                    cmbBranch.Visible = false;
                                }
                                //if (Convert.ToInt32(lstPayType.SelectedValue) == 4)
                                {
                                    int x = (GridView.Width - pnlchq.Width) / 2;
                                    pnlchq.Location = new Point(GridView.Location.X + x, y + 10);
                                    pnlchq.Visible = true;
                                    txtChqNo.Focus();
                                }


                            }
                            else if (ControlUnder == 5)
                            {
                                if ((VoucherType == 9) || (VoucherType == 109))
                                {
                                    label4.Visible = false;
                                    label2.Visible = false;
                                    cmbCrBank.Visible = false;
                                    cmbCrBranch.Visible = false;

                                }
                                int x = (GridView.Width - pnlCredit.Width) / 2;
                                pnlCredit.Location = new Point(GridView.Location.X + x, y + 10);
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
                            GridView.CurrentCell = GridView[ColIndex.Remark, GridView.CurrentCell.RowIndex];
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
                        GridView.CurrentCell = GridView[ColIndex.Remark, GridView.CurrentCell.RowIndex];
                    }


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
                    if (rbBillwise.Checked == true)
                    {
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtChqNo.Text;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = dtpChqDate.Value;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;
                        // if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCompanyBank);

                        pnlchq.Visible = false;

                        GridView.Focus();
                        GridView.CurrentCell = GridView[ColIndex.Remark, GridView.CurrentCell.RowIndex];
                    }
                    else if (rbBillSelection.Checked == true)
                    {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtChqNo.Text;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = dtpChqDate.Value;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = cmbPayType.Text;
                        //  if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                        // {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCompanyBank);//dtPayTypeLedger.Rows[1].ItemArray[1].ToString();
                        LedgNo = ObjFunction.GetComboValue(cmbCompanyBank);//Convert.ToInt64(dtPayTypeLedger.Rows[1].ItemArray[1].ToString());

                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = ObjFunction.GetComboValue(cmbPayType);
                        pnlchq.Visible = false;
                        dgBillSelection.Focus();
                        GridView.Focus();
                        GridView.CurrentCell = GridView[ColIndex.Remark, GridView.CurrentCell.RowIndex];
                    }
                    else if (rbAdjustment.Checked == true)
                    {
                        BtnSave.Focus();
                        pnlchq.Visible = false;
                    }
                    else if (rbAdvance.Checked == true)
                    {
                        BtnSave.Focus();
                        pnlchq.Visible = false;
                    }
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
            if (rbBillwise.Checked == true)
            {
                GridView.Focus();
                if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                    GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                else
                    GridView.CurrentCell = GridView[ColIndex.Amount, GridView.Rows.Count - 1];
            }
            else
            {
                cmbPayType.Focus();
            }
        }

        private bool ChqValidations()
        {
            bool tempflag = false;
            try
            {
                if (Convert.ToInt32(lstPayType.SelectedValue) == 3)
                {
                    EP.SetError(txtChqNo, "");
                    EP.SetError(cmbBank, "");
                    EP.SetError(cmbCompanyBank, "");
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
                    else if (VoucherType == VchType.Sales && Convert.ToInt64(cmbCompanyBank.SelectedValue) == 0)
                    {
                        EP.SetError(cmbCompanyBank, "Select Bank Accont");
                        EP.SetIconAlignment(cmbCompanyBank, ErrorIconAlignment.MiddleRight);
                        cmbCompanyBank.Focus();
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
                }
                else
                {
                    EP.SetError(cmbCompanyBank, "");
                    if (Convert.ToInt64(cmbCompanyBank.SelectedValue) == 0)
                    {
                        EP.SetError(cmbCompanyBank, "Select Bank Accont");
                        EP.SetIconAlignment(cmbCompanyBank, ErrorIconAlignment.MiddleRight);
                        cmbCompanyBank.Focus();
                    }
                    else
                        tempflag = true;
                }
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

                if (GridView.CurrentCell.ColumnIndex == ColIndex.DiscAmt)
                {
                    if (GridView.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                            GridView.CurrentCell.ErrorText = "Please Enter Valid Disc Amount";
                        else if (Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.NetBal].Value) < (Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.Amount].Value) + Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.DiscAmt].Value)))
                            GridView.CurrentCell.ErrorText = "Please Enter Valid Disc Amount";
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, GridView });
                        }
                    }

                    else
                    {
                        GridView.CurrentCell.Value = "0.00";
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, GridView });
                    }
                }
                if (GridView.CurrentCell.ColumnIndex == ColIndex.Amount)
                {
                    if (GridView.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                            GridView.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else if (Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.NetBal].Value) < (Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.Amount].Value) + Convert.ToDouble(GridView.Rows[rowCount].Cells[ColIndex.DiscAmt].Value)))
                            GridView.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount, ColIndex.PayType, GridView });
                            //GridView.CurrentCell = GridView[ColIndex.PayType, rowCount];
                            CalculateRecAmount();
                        }

                    }
                    else
                    {
                        GridView.CurrentCell.Value = "0.00";
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { rowCount, ColIndex.PayType, GridView });
                    }
                }
                if (GridView.CurrentCell.ColumnIndex == ColIndex.Remark)
                {
                    if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                    {
                        GridView.Focus();
                        GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                    }
                    else { BtnSave.Focus(); }
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
                    if (rbBillwise.Checked == true)
                    {
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtCrCardNo.Text;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = Convert.ToDateTime("01-01-1900");
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = 0;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = 0;
                        //if (VoucherType == VchType.Sales)
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCrCompanyBank);//
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                        pnlCredit.Visible = false;

                        GridView.Focus();
                        GridView.CurrentCell = GridView[ColIndex.Remark, GridView.CurrentCell.RowIndex];

                    }
                    else if (rbBillSelection.Checked == true)
                    {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtCrCardNo.Text;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = Convert.ToDateTime("01-01-1900");
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = 0;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = 0;

                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCrCompanyBank);//dtPayTypeLedger.Rows[2].ItemArray[1].ToString();
                        LedgNo = ObjFunction.GetComboValue(cmbCrCompanyBank);//Convert.ToInt64(dtPayTypeLedger.Rows[2].ItemArray[1].ToString());
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = ObjFunction.GetComboValue(cmbPayType);
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = cmbPayType.Text;
                        pnlCredit.Visible = false;
                        dgBillSelection.Focus();

                        GridView.Focus();
                        GridView.CurrentCell = GridView[ColIndex.Remark, GridView.CurrentCell.RowIndex];
                    }
                    else if (rbAdjustment.Checked == true)
                    {
                        pnlCredit.Visible = false;
                        BtnSave.Focus();
                    }
                    else if (rbAdvance.Checked == true)
                    {
                        pnlCredit.Visible = false;
                        BtnSave.Focus();
                    }

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
            if (rbBillwise.Checked == true)
            {
                GridView.Focus();
                if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                    GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                else
                    GridView.CurrentCell = GridView[ColIndex.Amount, GridView.Rows.Count - 1];
            }
            else
            {
                cmbPayType.Focus();
            }
        }

        private bool CreditValidations()
        {
            bool tempflag = false;
            try
            {
                EP.SetError(txtCrCardNo, "");
                //EP.SetError(cmbCrBank, "");
                EP.SetError(cmbCrCompanyBank, "");
                // EP.SetError(cmbCrBranch, "");

                if (txtCrCardNo.Text.Trim() == "")
                {
                    EP.SetError(txtCrCardNo, "Enter CreditCardNo");
                    EP.SetIconAlignment(txtCrCardNo, ErrorIconAlignment.MiddleRight);
                    txtCrCardNo.Focus();
                }

                else if (VoucherType == VchType.Sales && Convert.ToInt64(cmbCrCompanyBank.SelectedValue) == 0)
                {
                    EP.SetError(cmbCrCompanyBank, "Select Bank Account");
                    EP.SetIconAlignment(cmbCrCompanyBank, ErrorIconAlignment.MiddleRight);
                    cmbCrCompanyBank.Focus();
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
                if (rbBillSelection.Checked == true)
                    gv = dgBillSelection;
                else if (rbBillwise.Checked == true)
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
            else if (GridView.CurrentCell.ColumnIndex == ColIndex.DiscAmt)
            {
                TextBox txtDisc = (TextBox)e.Control;
                txtDisc.TextChanged += new EventHandler(txtDisc_TextChanged);
            }
        }

        private void txtDisc_TextChanged(object sender, EventArgs e)
        {
            if (GridView.CurrentCell.ColumnIndex == ColIndex.DiscAmt)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 7, OMFunctions.MaskedType.NotNegative);
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
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                    ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo =" + grpNo + " order by LedgerName");

                else
                    ObjFunction.FillCombo(cmbPartyName, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + grpNo + ")) AND (MLedger.IsActive = 'true') ORDER BY LedgerName ");

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
                lblTotRecAmt.Visible = false;
                lblTotRecAmt.Text = "Total Rec. Amt: 0.00";
                txtAmount.Visible = true;
                label14.Visible = true;
                lblPrint.Visible = false;
                if (rbBillwise.Checked == true)
                {
                    label7.Visible = false;
                    txtDiscPer.Visible = false;
                    cmbPayType.Enabled = true;
                    label5.Visible = true;
                    pnlBillwise.Visible = true;
                    txtAmount.Visible = false;
                    label14.Visible = false;
                    pnlBillselection.Visible = false;
                    pnlAdjustment.Visible = false;
                    PnlAgainstAdv.Visible = false;
                    //pnlBillwise.Location = new System.Drawing.Point(panel1.Location.X, 110);
                    BtnSave.Enabled = true;
                    pnlPayT.Visible = false;
                    lblPrint.Visible = true;
                    txtAmount.Text = "";
                    for (int i = 0; i < GridView.Rows.Count; i++)
                    {
                        GridView.Rows[i].Cells[5].Value = "0.00";
                        GridView.Rows[i].Cells[ColIndex.DiscAmt].Value = "0.00";
                    }
                    cmbPayType.SelectedIndex = 0;
                    if (GridView.Rows.Count > 0)
                        GridView.CurrentCell = GridView.Rows[0].Cells[ColIndex.Amount];
                    GridView.Columns[ColIndex.DiscAmt].DisplayIndex = ColIndex.Amount;
                    GridView.Focus();
                    lblTotRecAmt.Visible = true;
                }
                else if (rbBillSelection.Checked == true)
                {
                    label7.Visible = true;
                    txtDiscPer.Visible = true;
                    cmbPayType.Enabled = true;
                    label5.Visible = true;
                    pnlBillselection.Visible = true;
                    pnlBillwise.Visible = false;
                    pnlAdjustment.Visible = false;
                    PnlAgainstAdv.Visible = false;
                    dgBillSelection.Dock = DockStyle.Fill;
                    pnlBillselection.Location = pnlBillwise.Location;
                    pnlBillselection.Size = pnlBillwise.Size;
                    BtnSave.Enabled = false;
                    pnlPayT.Visible = true;
                    lblPrint.Visible = true;
                    txtDiscPer.Text = "0";
                    txtAmount.Text = "0";
                    ObjFunction.FillComb(cmbPayType, "Select PKPayTypeNo,PayTypeName from MPayType where ControlUnder Not in(1,3)");

                    txtAmount.Text = "";
                    for (int i = 0; i < dgBillSelection.Rows.Count; i++)
                    {
                        dgBillSelection.Rows[i].Cells[ColIndex.Amount].Value = "0.00";
                        dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value = "0.00";
                        dgBillSelection.Rows[i].Cells[ColIndex.Chk].Value = false;
                    }
                    cmbPayType.SelectedIndex = 0;
                    if (dgBillSelection.Rows.Count > 0)
                        cmbPayType_KeyDown(cmbPayType, new KeyEventArgs(Keys.Enter));
                    if (dgBillSelection.Rows.Count > 0)
                        dgBillSelection.CurrentCell = dgBillSelection.Rows[0].Cells[ColIndex.Amount];
                    dgBillSelection.Columns[ColIndex.DiscAmt1].DisplayIndex = ColIndex.Amount;
                    dgBillSelection.Focus();

                    lblMainRemark.Visible = true; txtMainRemark.Visible = true;
                    //lblTotRecAmt.Visible = true;
                }
                else if (rbAdjustment.Checked == true)
                {
                    label7.Visible = false;
                    txtDiscPer.Visible = false;
                    cmbPayType.Enabled = false;
                    label5.Visible = false;
                    cmbPayType.SelectedIndex = 0;
                    pnlBillselection.Visible = false;
                    PnlAgainstAdv.Visible = false;
                    pnlBillwise.Visible = false;
                    lblAdvBal.Visible = false;
                    lblAdvlbl.Visible = false;
                    lblAdvOpBal.Visible = false;
                    txtAdvAmt.Visible = false;
                    lblOpeningBal.Visible = true;
                    lblBalance.Visible = true;
                    pnlAdjustment.Visible = true;

                    pnlAdjustment.Location = pnlBillwise.Location;
                    pnlAdjustment.Size = pnlBillwise.Size;

                    dgAdjustOpening.Visible = false;
                    txtAmount.Visible = false;
                    label14.Visible = false;
                    BtnSave.Enabled = false;
                    txtAdjustment.Text = "";
                    pnlPayT.Visible = true;
                    pnlchq.Visible = false;
                    pnlCredit.Visible = false;
                    txtRemark.Visible = false;
                    lblRemark.Visible = false;
                    ObjFunction.FillComb(cmbPayType, "Select PKPayTypeNo,PayTypeName from MPayType where ControlUnder not in(1,3)");

                    double OpBal = 0.00;
                    string str = "";
                    if (DBGetVal.KachhaFirm == false)
                    {
                        str = "136,36,115,111,109,107,130,131";
                        OpBal = ObjQry.ReturnDouble("Select OpAmt * -1 from GetOpeningLedgerBalanceOnly(" + ObjFunction.GetComboValue(cmbPartyName) + "," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                    }
                    else
                    {
                        str = "36,136,15,11,9,7,30,31";
                        OpBal = ObjQry.ReturnDouble("Select OpAmt * -1 from GetOpeningLedgerBalanceOnlyES(" + ObjFunction.GetComboValue(cmbPartyName) + "," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                    }
                    double TotBal = 0;
                    //In below query we used reverse logic of signcode as this is adjsted entry's values
                    TotBal = OpBal + ObjQry.ReturnDouble("Select isNull(Sum(case when (signcode=1) then isNull(Amount,0) else isNull(Amount,0)*-1 end),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=5 AND REFNO <> 0 " + //, CommonFunctions.ConStr);
                    " AND FKVOUCHERTRNNO IN (SELECT PKVOUCHERTRNNO FROM TVOUCHERDETAILS WHERE FKVOUCHERNO IN (SELECT PKVOUCHERNO FROM TVOUCHERENTRY WHERE TVoucherEntry.VoucherTypeCode NOT  in( " + str + "))) ", CommonFunctions.ConStr);

                    //In below query we used forward logic of signcode as this is direct entry's values
                  //  TotBal = TotBal + ObjQry.ReturnDouble("Select isNull(Sum(case when (signcode=2) then isNull(Amount,0) else isNull(Amount,0)*-1 end),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=5 AND REFNO = 0", CommonFunctions.ConStr);


                    //In below query we used forward logic of signcode as this is direct entry's values
                    TotBal = TotBal + ObjQry.ReturnDouble("Select isNull(Sum(case when (signcode=2) then isNull(Amount,0) else isNull(Amount,0)*-1 end),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=5 AND REFNO = 0 " +
                       " AND FKVOUCHERTRNNO IN (SELECT PKVOUCHERTRNNO FROM TVOUCHERDETAILS WHERE FKVOUCHERNO IN (SELECT PKVOUCHERNO FROM TVOUCHERENTRY WHERE TVoucherEntry.VoucherTypeCode NOT in( " + str + "))) ", CommonFunctions.ConStr);

                    OpBalPendingAmt = Math.Abs(TotBal);
                    OpBalAdjustedAmt = 0;

                    if (OpBal < 0 && (VoucherType == VchType.Sales || VoucherType == VchType.DSales))
                    {
                        lblOpeningBal.Text = "Opening Balance  :    " + Math.Abs(OpBal).ToString("0.00") + "   To Receive";
                        lblAdjlbl.Visible = true;
                        txtAdjustment.Visible = true;
                        dgAdjustOpening.Visible = false;
                        BtnSave.Enabled = false;
                    }
                    else if (OpBal > 0 && (VoucherType == VchType.Sales || VoucherType == VchType.DSales))
                    {
                        lblOpeningBal.Text = "Opening Balance  :    " + Math.Abs(OpBal).ToString("0.00") + "   To Pay";
                        lblAdjlbl.Visible = false;
                        txtAdjustment.Visible = false;
                        dgAdjustOpening.Visible = true;
                        BtnSave.Enabled = true;
                    }
                    else if (OpBal > 0 && (VoucherType == VchType.Purchase || VoucherType == VchType.DPurchase))
                    {
                        lblOpeningBal.Text = "Opening Balance  :    " + Math.Abs(OpBal).ToString("0.00") + "   To Pay";
                        lblAdjlbl.Visible = true;
                        txtAdjustment.Visible = true;
                        dgAdjustOpening.Visible = false;
                        BtnSave.Enabled = false;
                    }
                    else if (OpBal < 0 && (VoucherType == VchType.Purchase || VoucherType == VchType.DPurchase))
                    {
                        lblOpeningBal.Text = "Opening Balance  :    " + Math.Abs(OpBal).ToString("0.00") + "   To Receive";

                        lblAdjlbl.Visible = false;
                        txtAdjustment.Visible = false;
                        dgAdjustOpening.Visible = true;
                        BtnSave.Enabled = true;
                    }
                    else
                        lblOpeningBal.Text = "Opening Balance  :    " + Math.Abs(OpBal).ToString("0.00");
                    lblBalance.Text = "Balance :    " + Math.Abs(TotBal).ToString("0.00");

                    txtAdjustment.Focus();
                    txtAdjustment.Focus();
                }
                else if (rbAdvance.Checked == true)
                {
                    label7.Visible = false;
                    txtDiscPer.Visible = false;
                    cmbPayType.Enabled = false;
                    label5.Visible = false;
                    cmbPayType.SelectedIndex = 0;
                    PnlAgainstAdv.Visible = false;
                    pnlBillselection.Visible = false;
                    pnlBillwise.Visible = false;
                    pnlAdjustment.Visible = true;

                    pnlAdjustment.Location = pnlBillwise.Location;
                    pnlAdjustment.Size = pnlBillwise.Size;

                    dgAdjustOpening.Visible = false;
                    lblAdvBal.Visible = true;
                    lblAdvlbl.Visible = true;
                    txtRemark.Visible = true;
                    txtRemark.Text = "";
                    lblRemark.Visible = true;
                    lblAdvOpBal.Visible = true;
                    txtAdvAmt.Visible = true;
                    lblOpeningBal.Visible = false;
                    lblBalance.Visible = false;
                    lblAdjlbl.Visible = false;
                    txtAdjustment.Visible = false;
                    txtAmount.Visible = false;
                    label14.Visible = false;
                    pnlAdjustment.Location = new System.Drawing.Point(12, 110);
                    BtnSave.Enabled = false;
                    txtAdvAmt.Text = "";
                    pnlPayT.Visible = true;
                    pnlchq.Visible = false;
                    pnlCredit.Visible = false;
                    ObjFunction.FillComb(cmbPayType, "Select PKPayTypeNo,PayTypeName from MPayType where ControlUnder not in(1,3)");

                    //===umesh======
                    string str = "";
                    if (DBGetVal.KachhaFirm == false)
                    {
                        str = "30,31";
                    }
                    else {
                        str = "130,131";
                    }

                    double totRecAdv = ObjQry.ReturnDouble("Select Sum(NetBal) From(SELECT SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) - ISNULL((SELECT SUM(Amount) FROM TVoucherRefDetails AS TVoucherRefDetails_2 WHERE (FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)), 0) AS NetBal, TVoucherDetails.PkVoucherTrnNo " +
                        " FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                        " WHERE     (TVoucherEntry.VoucherTypeCode in("+ str  +")) AND (TVoucherDetails.SrNo = " + Others.Party + ") AND (MLedger.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ") AND  (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND (TVoucherEntry.IsCancel = 'false') " +
                        " GROUP BY TVoucherDetails.PkVoucherTrnNo HAVING (SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) - ISNULL ((SELECT     SUM(Amount) FROM TVoucherRefDetails AS TVoucherRefDetails_2 WHERE (FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)), 0) > 0) )TempTable", CommonFunctions.ConStr);



                    lblAdvOpBal.Text = "";// "Total Advance         : " + OpBal.ToString("0.00");
                    lblAdvBal.Text = "Balance :    " + (totRecAdv).ToString("0.00");//OpBal -
                    txtAdvAmt.Focus();
                    lblMainRemark.Visible = false; txtMainRemark.Visible = false;
                }
                else if (rbAgainstAdv.Checked == true || rbAdjSalesReturn.Checked == true)
                {
                    label7.Visible = false;
                    txtDiscPer.Visible = false;
                    cmbPayType.Enabled = true;
                    label5.Visible = false;
                    pnlBillwise.Visible = false;
                    txtAmount.Visible = false;
                    label14.Visible = false;
                    pnlBillselection.Visible = false;
                    pnlAdjustment.Visible = false;
                    PnlAgainstAdv.Visible = true;

                    dgAdvance.Dock = DockStyle.Fill;
                    dgBill.Dock = DockStyle.Fill;

                    PnlAgainstAdv.Location = pnlBillwise.Location;
                    PnlAgainstAdv.Size = pnlBillwise.Size;

                    BtnSave.Enabled = true;
                    pnlPayT.Visible = false;
                    dgBill.Visible = false;
                    txtAmount.Text = "";


                    if (dgAdvance.Rows.Count > 0)
                        dgAdvance.CurrentCell = dgAdvance[8, 0];
                    //dgAdvance.CurrentCell = dgAdvance.Rows[0].Cells[ColIndex.Amount];
                    dgAdvance.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void cmbPayType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnSave.Enabled = true;
                    e.SuppressKeyPress = true;
                    Panel p = new Panel();
                    if (pnlBillselection.Visible == true)
                        p = pnlBillselection;
                    else if (pnlAdjustment.Visible == true)
                        p = pnlAdjustment;
                    else
                        p = pnlBillwise;
                    txtChqNo.Text = "";
                    txtCrCardNo.Text = "";
                    cmbBank.SelectedIndex = 0;
                    cmbBranch.SelectedIndex = 0;
                    pnlCredit.Visible = false;
                    pnlchq.Visible = false;

                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPayType) + "", CommonFunctions.ConStr);
                    if (ControlUnder == 1 || ControlUnder == 4 || ControlUnder == 5)
                    {
                        if (ControlUnder == 4 || ControlUnder == 5)
                        {
                            if (ControlUnder == 4)
                            {
                                if (rbAdjustment.Checked == true || rbAdvance.Checked == true)
                                {
                                    pnlchq.Location = new Point(pnlAdjustment.Location.X + 10, pnlAdjustment.Location.Y + txtAdjustment.Location.Y + 40);
                                    pnlchq.Visible = true;
                                    pnlchq.BringToFront();
                                    txtChqNo.Focus();
                                }
                                else
                                {
                                    int y = pnlMain.Location.Y + p.Location.Y + dgBillSelection.Location.Y;
                                    y = y + ((p.Height - dgBillSelection.Height) / 2);
                                    int x = (dgBillSelection.Width - pnlchq.Width) / 2;
                                    pnlchq.Location = new Point(dgBillSelection.Location.X + x, y + 10);
                                    pnlchq.Visible = true;
                                    pnlchq.BringToFront();
                                    txtChqNo.Focus();
                                }
                            }
                            else if (ControlUnder == 5)
                            {
                                if (rbAdvance.Checked == true || rbAdjustment.Checked == true)
                                {
                                    pnlCredit.Location = new Point(pnlAdjustment.Location.X + 10, pnlAdjustment.Location.Y + txtAdjustment.Location.Y + 40);
                                    pnlCredit.Visible = true;
                                    pnlCredit.BringToFront();
                                    txtCrCardNo.Focus();
                                }
                                else
                                {
                                    int y = pnlMain.Location.Y + p.Location.Y + dgBillSelection.Location.Y;
                                    y = y + ((p.Height - dgBillSelection.Height) / 2);
                                    int x = (dgBillSelection.Width - pnlCredit.Width) / 2;
                                    pnlCredit.Location = new Point(dgBillSelection.Location.X + x, y + 10);
                                    pnlCredit.Visible = true;
                                    pnlCredit.BringToFront();
                                    txtCrCardNo.Focus();
                                }
                            }
                        }
                        {
                            if (rbBillSelection.Checked == true)
                            {
                                dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = cmbPayType.Text;
                                dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dtPayTypeLedger.Rows[0].ItemArray[1].ToString();
                                LedgNo = Convert.ToInt64(dtPayTypeLedger.Rows[0].ItemArray[1].ToString());
                                dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = ObjFunction.GetComboValue(cmbPayType);

                            }
                            //BtnSave.Focus();
                        }
                    }
                    else
                    {
                        if (rbBillSelection.Checked == true)
                        {
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = cmbPayType.Text;
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dtPayTypeLedger.Rows[0].ItemArray[1].ToString();
                            LedgNo = Convert.ToInt64(dtPayTypeLedger.Rows[0].ItemArray[1].ToString());
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = ObjFunction.GetComboValue(cmbPayType);
                            //pnlPayT.Visible = false;

                            if (dgBillSelection.CurrentCell.RowIndex < dgBillSelection.Rows.Count - 1)
                            {
                                dgBillSelection.Focus();
                                dgBillSelection.CurrentCell = dgBillSelection[ColIndex.Amount, dgBillSelection.CurrentCell.RowIndex + 1];
                            }
                            else
                            {
                                BtnSave.Focus();
                            }
                        }
                        if (txtMainRemark.Visible == true)
                            txtMainRemark.Focus();
                        else BtnSave.Focus();
                    }


                }
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

        private void rbBillSelection_CheckedChanged(object sender, EventArgs e)
        {
            rbCheck_Changed();
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
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    cmbPayType.Focus();
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
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = (Convert.ToDouble(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.NetBal].Value) - Convert.ToDouble(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.DiscAmt1].Value)).ToString("0.00");
                        }
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.DiscAmt)
                    {
                        if (dgBillSelection.CurrentCell.Value != null)
                        {
                            if (ObjFunction.CheckValidAmount(dgBillSelection.CurrentCell.Value.ToString()) == false)
                                dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Disc Amt";
                            else
                                dgBillSelection.CurrentCell = GridView[ColIndex.Amount, dgBillSelection.CurrentCell.RowIndex];
                        }
                    }
                    else if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.Remark)
                    {
                        if (dgBillSelection.CurrentCell.RowIndex < dgBillSelection.Rows.Count - 1)
                        {
                            dgBillSelection.Focus();
                            dgBillSelection.CurrentCell = GridView[ColIndex.Amount, dgBillSelection.CurrentCell.RowIndex + 1];
                        }
                        else { BtnSave.Focus(); }


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

        private void txtAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataGridView gv = null;
                if (rbBillwise.Checked == true) gv = GridView;
                else if (rbBillSelection.Checked == true) gv = dgBillSelection;
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (ObjFunction.CheckValidAmount(txtAmount.Text.Replace("-", "")) == true)
                    {
                        if (Convert.ToDouble(txtAmount.Text.Trim()) > Convert.ToDouble(lblAmount.Text))
                        {
                            if (OMMessageBox.Show("Enter Amount less than Total Amount \nExceed Amount consider as advance amount. want to continue, click on Yes or No", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Information) == DialogResult.No)
                            {
                                txtAmount.Focus();
                                return;
                            }
                        }
                        //if (Convert.ToDouble(txtAmount.Text.Trim()) <= Convert.ToDouble(lblAmount.Text))
                        //{
                        txtDiscPer.Text = Convert.ToDouble((txtDiscPer.Text == "") ? "0" : txtDiscPer.Text).ToString();
                        double Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        for (int i = 0; i < gv.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value) <= Amount)
                            {
                                // gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = "0";
                                gv.Rows[i].Cells[ColIndex.Amount].Value = Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value) - Math.Round(Convert.ToDouble(gv.Rows[i].Cells[ColIndex.DiscAmt1].Value), 0);
                                gv.Rows[i].Cells[ColIndex.Chk].Value = "True";
                                Amount = Amount - (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value));
                            }
                            else if (Amount != 0)
                            {
                                if (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value) <= Amount)
                                {
                                    // gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = "0";
                                    gv.Rows[i].Cells[ColIndex.Amount].Value = Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value) - Math.Round(Convert.ToDouble(gv.Rows[i].Cells[ColIndex.DiscAmt1].Value), 0);
                                    Amount = Amount - Convert.ToDouble(gv.Rows[i].Cells[ColIndex.Amount].Value);
                                    gv.Rows[i].Cells[ColIndex.Chk].Value = "True";
                                }
                                else
                                {
                                    // gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = "0";
                                    gv.Rows[i].Cells[ColIndex.Amount].Value = Amount.ToString("0.00");
                                    gv.Rows[i].Cells[ColIndex.Chk].Value = "True";
                                    Amount = 0;
                                }
                            }
                            else
                            {
                                gv.Rows[i].Cells[ColIndex.Amount].Value = "0.00";
                                gv.Rows[i].Cells[ColIndex.Chk].Value = false;
                            }
                        }
                        gv.CurrentCell = gv[ColIndex.Amount, 0];
                        gv.Focus();

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

        private void rbAdjustment_CheckedChanged(object sender, EventArgs e)
        {
            rbCheck_Changed();
        }

        private void txtAdjustment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    cmbPayType.Enabled = false;
                    if (txtAdjustment.Text != "")
                    {
                        if (Convert.ToDouble(txtAdjustment.Text.Replace("-", "")) < 0)
                        {
                            OMMessageBox.Show("Enter Valid Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            txtAdjustment.Focus();
                        }
                        else if (Convert.ToDouble(txtAdjustment.Text) == 0)
                        {
                            OMMessageBox.Show("Enter Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            txtAdjustment.Focus();
                        }
                        else if (Convert.ToDouble(lblBalance.Text.Replace("Balance :    ", "").Replace("-", "")) < Convert.ToDouble(txtAdjustment.Text.Replace("-", "")))
                        {
                            OMMessageBox.Show("Enter Amount less than Balance Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            txtAdjustment.Focus();
                        }
                        else
                        {
                            cmbPayType.Enabled = true;
                            cmbPayType.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtAdjustment_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 10, OMFunctions.MaskedType.NotNegative);

        }

        private void cmbPartyName_SelectionChangeCommitted(object sender, EventArgs e)
        {

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

            if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.DiscAmt1)
            {
                TextBox txtDisc = (TextBox)e.Control;
                txtDisc.TextChanged += new EventHandler(txtDisc1_TextChanged);
            }
        }

        private void txtDisc1_TextChanged(object sender, EventArgs e)
        {
            if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.DiscAmt1)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 7, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtAmount, 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void txtChqNo_TextChanged(object sender, EventArgs e)
        {
           // ObjFunction.SetMaskedNumeric(txtChqNo);
        }


        private void txtCrCardNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void rbAdvance_CheckedChanged(object sender, EventArgs e)
        {
            rbCheck_Changed();
        }

        private void txtAdvAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbPayType.Enabled = false;
                if (txtAdvAmt.Text != "")
                {
                    if (Convert.ToDouble(txtAdvAmt.Text.Replace("-", "")) < 0)
                    {
                        OMMessageBox.Show("Enter Valid Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        txtAdvAmt.Focus();
                    }
                    else if (Convert.ToDouble(txtAdvAmt.Text) == 0)
                    {
                        OMMessageBox.Show("Enter Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        txtAdvAmt.Focus();
                    }

                    else
                    {
                        txtRemark.Focus();
                    }
                }
            }
        }

        private void txtAdvAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 10, OMFunctions.MaskedType.NotNegative);
        }

        private void rbAgainstAdv_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rbCheck_Changed();
                label15.Visible = true;
                dgAdvance.Rows.Clear();
                DataTable dtAdv = new DataTable();
                if (VoucherType == VchType.Sales)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.SalesReceipt + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.RejectionIn + "," + DBGetVal.FirmNo + ",3").Table;

                }
                else if (VoucherType == VchType.Purchase)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.PurchasePayment + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.RejectionOut + "," + DBGetVal.FirmNo + ",3").Table;
                }
                else if (VoucherType == VchType.DSales)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DSalesReceipt + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DRejectionIn + "," + DBGetVal.FirmNo + ",3").Table;

                }
                else if (VoucherType == VchType.DPurchase)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DPurchasePayment + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DRejectionOut + "," + DBGetVal.FirmNo + ",3").Table;
                }
                for (int i = 0; i < dtAdv.Rows.Count; i++)
                {
                    dgAdvance.Rows.Add();
                    for (int j = 0; j < dgAdvance.Columns.Count - 1; j++)
                    {
                        dgAdvance.Rows[i].Cells[j].Value = dtAdv.Rows[i].ItemArray[j];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgAdvance_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Value = e.RowIndex + 1;
            if (e.ColumnIndex == 1)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }
        }

        private void dgAdvance_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
        }

        private void Chk_CheckedChanged(object sender, EventArgs e)
        {
            if (dgAdvance.CurrentCell.ColumnIndex == 8)
            {
                if (Convert.ToBoolean(dgAdvance.Rows[dgAdvance.CurrentCell.RowIndex].Cells[8].FormattedValue) == true)
                {
                    for (int i = 0; i < dgAdvance.Rows.Count; i++)
                    {
                        if (i != dgAdvance.CurrentCell.ColumnIndex)
                            dgAdvance.Rows[i].Cells[8].Value = false;
                    }
                }

            }
        }

        private void dgAdvance_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.BillDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
            }
        }

        private void dgAdvance_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void BindGridAdv()
        {
            try
            {
                dgBill.Rows.Clear();
                DataTable dt = ObjFunction.GetDataView("Exec GetAdvanceCollectionBillDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VoucherType + "," + DBGetVal.FirmNo + "").Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgBill.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgBill.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                    dgBill.Rows[i].Cells[ColIndex.Chk].Value = false;
                }

                dgBill.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBill.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBill.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBill.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBill.Columns[ColIndex.Chk].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                #region For OpeningBalance Adjustment
                dgAdjustOpening.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgAdjustOpening.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgAdjustOpening.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                    dgAdjustOpening.Rows[i].Cells[ColIndex.Chk].Value = false;
                }

                dgAdjustOpening.Columns[ColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgAdjustOpening.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgAdjustOpening.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgAdjustOpening.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgAdjustOpening.Columns[ColIndex.Chk].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgAdjustOpening.Columns[ColIndex.Chk].ReadOnly = false;
                #endregion
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rbAdjSalesReturn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rbCheck_Changed();
                label15.Visible = true;


                dgAdvance.Rows.Clear();
                DataTable dtAdv = new DataTable();
                if (VoucherType == VchType.Sales)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.SalesReceipt + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.RejectionIn + "," + DBGetVal.FirmNo + ",3").Table;

                    label15.Text = "Select the Bill to be Adjust Sales Return";
                }
                else if (VoucherType == VchType.DSales)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DSalesReceipt + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DRejectionIn + "," + DBGetVal.FirmNo + ",3").Table;

                    label15.Text = "Select the Bill to be Adjust Estimate Sales Return";
                }
                else if (VoucherType == VchType.Purchase)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.PurchasePayment + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.RejectionOut + "," + DBGetVal.FirmNo + ",3").Table;
                    label15.Text = "Select the Bill to be Adjust Purchase Return";
                }
                else if (VoucherType == VchType.DPurchase)
                {
                    if (rbAgainstAdv.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DPurchasePayment + "," + DBGetVal.FirmNo + ",1").Table;
                    else if (rbAdjSalesReturn.Checked == true)
                        dtAdv = ObjFunction.GetDataView("Exec GetAdvanceCollectionDetails " + ObjFunction.GetComboValue(cmbPartyName) + "," + VchType.DRejectionOut + "," + DBGetVal.FirmNo + ",3").Table;
                    label15.Text = "Select the Bill to be Adjust Estimate Purchase Return";
                }

                for (int i = 0; i < dtAdv.Rows.Count; i++)
                {
                    dgAdvance.Rows.Add();
                    for (int j = 0; j < dgAdvance.Columns.Count - 1; j++)
                    {
                        dgAdvance.Rows[i].Cells[j].Value = dtAdv.Rows[i].ItemArray[j];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgAdjustOpening_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    double AdjustAmt = 0;
                    int rowIndex = dgAdjustOpening.CurrentRow.Index;
                    if (dgAdjustOpening.CurrentCell.ColumnIndex == ColIndex.Chk)
                    {
                        if (Convert.ToBoolean(dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].FormattedValue) == false)
                        {
                            dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].Value = true;
                            if (OpBalAdjustedAmt < OpBalPendingAmt)
                            {
                                AdjustAmt = Convert.ToDouble(dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.NetBal].Value);

                                if (AdjustAmt <= (OpBalPendingAmt - OpBalAdjustedAmt))
                                {
                                    dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value = AdjustAmt.ToString("0.00");
                                    OpBalAdjustedAmt = OpBalAdjustedAmt + AdjustAmt;
                                }
                                else if ((OpBalPendingAmt - OpBalAdjustedAmt) > 0)
                                {
                                    dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value = (OpBalPendingAmt - OpBalAdjustedAmt).ToString("0.00");
                                    OpBalAdjustedAmt = OpBalAdjustedAmt + (OpBalPendingAmt - OpBalAdjustedAmt);
                                }
                            }
                            else
                                dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                        }
                        else
                        {
                            dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                            OpBalAdjustedAmt = OpBalAdjustedAmt - Convert.ToDouble(dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                            dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value = "0.00";
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgAdjustOpening_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double AdjustAmt = 0;
                if (e.ColumnIndex == ColIndex.Chk)
                {
                    int rowIndex = e.RowIndex;
                    if (Convert.ToBoolean(dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].FormattedValue) == false)
                    {
                        dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].Value = true;
                        if (OpBalAdjustedAmt < OpBalPendingAmt)
                        {
                            AdjustAmt = Convert.ToDouble(dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.NetBal].Value);

                            if (AdjustAmt <= (OpBalPendingAmt - OpBalAdjustedAmt))
                            {
                                dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value = AdjustAmt.ToString("0.00");
                                OpBalAdjustedAmt = OpBalAdjustedAmt + AdjustAmt;
                            }
                            else if ((OpBalPendingAmt - OpBalAdjustedAmt) > 0)
                            {
                                dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value = (OpBalPendingAmt - OpBalAdjustedAmt).ToString("0.00");
                                OpBalAdjustedAmt = OpBalAdjustedAmt + (OpBalPendingAmt - OpBalAdjustedAmt);
                            }
                        }
                        else
                            dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                    }
                    else
                    {
                        dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                        OpBalAdjustedAmt = OpBalAdjustedAmt - Convert.ToDouble(dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                        dgAdjustOpening.Rows[rowIndex].Cells[ColIndex.Amount].Value = "0.00";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgAdjustOpening_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.BillDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
            }
        }

        private void dgAdjustOpening_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.Chk)
            {
                int rowIndex = e.RowIndex;
            }
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



        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbPayType.Enabled = true;
                cmbPayType.Focus();
            }
        }

        private void dgAdvance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8)
                {
                    int rowIndex = e.RowIndex;
                    if (Convert.ToBoolean(dgAdvance.Rows[rowIndex].Cells[8].Value) == false)
                    {
                        dgAdvance.Rows[rowIndex].Cells[8].Value = true;
                        for (int i = 0; i < dgAdvance.Rows.Count; i++)
                        {
                            if (i != e.RowIndex)
                                dgAdvance.Rows[i].Cells[8].Value = false;
                        }
                        TolAdvadjAmt = Convert.ToDouble(dgAdvance.Rows[rowIndex].Cells[5].Value);
                        AdvRowIndex = rowIndex;
                        AdvAdjAmt = 0;
                        Amt = 0;
                        Advflag = false;
                        BindGridAdv();
                        dgBill.Visible = true;
                    }
                    else
                    {
                        dgAdvance.Rows[rowIndex].Cells[8].Value = false;
                        TolAdvadjAmt = 0;
                        AdvRowIndex = 0;
                        AdvAdjAmt = 0;
                        Amt = 0;
                        Advflag = false;
                        dgBill.Visible = false;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColIndex.Chk)
                {
                    int rowIndex = e.RowIndex;
                    if (Convert.ToBoolean(dgBill.Rows[rowIndex].Cells[ColIndex.Chk].EditedFormattedValue) == false)
                    {
                        dgBill.Rows[rowIndex].Cells[ColIndex.Chk].Value = true;
                        if (AdvAdjAmt <= 0 && (Amt != TolAdvadjAmt))
                            AdvAdjAmt = Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.NetBal].Value);
                        if (AdvAdjAmt <= TolAdvadjAmt && Advflag == false && (Amt != TolAdvadjAmt))
                        {
                            if (AdvAdjAmt <= Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.NetBal].Value))
                            {
                                dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = AdvAdjAmt.ToString("0.00");
                                Amt = Amt + Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                            }
                            else
                            {
                                dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = dgBill.Rows[rowIndex].Cells[ColIndex.NetBal].Value;
                                Amt = Amt + Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                            }
                            AdvAdjAmt = TolAdvadjAmt - Amt;
                        }
                        else if (AdvAdjAmt > TolAdvadjAmt)
                        {
                            dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = TolAdvadjAmt.ToString("0.00");
                            AdvAdjAmt = TolAdvadjAmt;
                            Advflag = true;
                        }
                        else
                            dgBill.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                        dgBill.Rows[rowIndex].Cells[ColIndex.BFKVoucherNo].Value = dgAdvance.Rows[AdvRowIndex].Cells[6].Value.ToString();
                        dgBill.Rows[rowIndex].Cells[ColIndex.BVoucherSrNo].Value = dgAdvance.Rows[AdvRowIndex].Cells[7].Value.ToString();
                    }
                    else
                    {
                        dgBill.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                        AdvAdjAmt = AdvAdjAmt - Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                        Amt = Amt - Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                        dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = "0.00";
                        dgBill.Rows[rowIndex].Cells[ColIndex.BFKVoucherNo].Value = "0";
                        dgBill.Rows[rowIndex].Cells[ColIndex.BVoucherSrNo].Value = "0";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void CalculateTotal()
        {
            double totamt = 0.0;

            for (int i = 0; i < dgBillSelection.Rows.Count; i++)
            {

                if (Convert.ToBoolean(dgBillSelection.Rows[i].Cells[ColIndex.Chk].Value) == true)
                {
                    totamt = totamt + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.DiscAmt1].Value);


                }
                txtAmount.Text = totamt.ToString("0.00");
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
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.NetBal].Value) < (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.DiscAmt1].Value)))
                            dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value) == 0)
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = false;
                        else
                        {
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = true;
                            MovetoNext move2n = new MovetoNext(m2n);
                            if (dgBillSelection.Rows.Count - 1 == rowCount)
                                BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, dgBillSelection });
                            //else if (dgBillSelection.Rows.Count - 1 > rowCount)
                            //    BeginInvoke(move2n, new object[] { rowCount + 1, ColIndex.Amount, dgBillSelection });
                            //GridView.CurrentCell = GridView[ColIndex.PayType, rowCount];
                        }
                    }
                    else
                    {
                        dgBillSelection.CurrentCell.Value = "0.00";
                        dgBillSelection.CurrentRow.Cells[ColIndex.Chk].Value = false;
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, dgBillSelection });

                    }
                }
                else if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.DiscAmt1)
                {
                    if (dgBillSelection.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(dgBillSelection.CurrentCell.EditedFormattedValue.ToString()) == false)
                            dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Disc Amount";
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.NetBal].Value) < (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.DiscAmt1].Value)))
                            dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Disc Amount";
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value) == 0)
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = false;
                    }
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
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.DiscAmt1].Value = "0.00";
                    }
                    else
                    {
                        double discrs = (Convert.ToDouble(txtDiscPer.Text) * (Convert.ToDouble(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.NetBal].Value)) / 100);
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.DiscAmt1].Value = discrs.ToString("0.00");
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Chk].Value = true;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = Convert.ToDouble(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.NetBal].Value) - Convert.ToDouble(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.DiscAmt1].Value);// -Convert.ToDouble(dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.DiscAmt1].Value);
                    }
                    CalculateTotal();
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
            lblOpBal.Text = "0.00";
            lblNetB.Text = "0.00";
            lblDrBal.Text = "0.00";
            lblCrNet.Text = "0.00";
            lblOpBalp.Text = "0.00";
            lblNetBp.Text = "0.00";
            lblDrBalp.Text = "0.00";
            lblCrNetp.Text = "0.00";

            GridView.Rows.Clear();
            dgBillSelection.Rows.Clear();
            dgBill.Rows.Clear();
            dgAdvance.Rows.Clear();
            pnlchq.Visible = false;
            pnlCredit.Visible = false;
            pnlPaytype.Visible = false;
            pnlRecord.Visible = false;
            pnlDetails.Visible = false;
            pnlDetailsPur.Visible = false;
            BtnSave.Enabled = false;
            label5.Visible = false;
            txtAmount.Text = "";
            txtAdjustment.Text = "";
            txtAdvAmt.Text = "";

            lblOpeningBal.Text = "Opening Balance:    0.00";
            lblBalance.Text = "Balance:    0.00";
            lblAdvOpBal.Text = "Total Advance:    0.00";
            lblAdvBal.Text = "Balance:    0.00";

        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnSave.Focus();
            }
            else if (e.KeyCode == Keys.Space)
            {
                try
                {
                    int rowIndex = dgBill.CurrentCell.RowIndex;
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.Chk)
                    {
                        if (Convert.ToBoolean(dgBill.Rows[rowIndex].Cells[ColIndex.Chk].EditedFormattedValue) == false)
                        {
                            dgBill.Rows[rowIndex].Cells[ColIndex.Chk].Value = true;
                            if (AdvAdjAmt <= 0 && (Amt != TolAdvadjAmt))
                                AdvAdjAmt = Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.NetBal].Value);
                            if (AdvAdjAmt <= TolAdvadjAmt && Advflag == false && (Amt != TolAdvadjAmt))
                            {
                                if (AdvAdjAmt <= Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.NetBal].Value))
                                {
                                    dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = AdvAdjAmt.ToString("0.00");
                                    Amt = Amt + Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                                }
                                else
                                {
                                    dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = dgBill.Rows[rowIndex].Cells[ColIndex.NetBal].Value;
                                    Amt = Amt + Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                                }
                                AdvAdjAmt = TolAdvadjAmt - Amt;
                            }
                            else if (AdvAdjAmt > TolAdvadjAmt)
                            {
                                dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = TolAdvadjAmt.ToString("0.00");
                                AdvAdjAmt = TolAdvadjAmt;
                                Advflag = true;
                            }
                            else
                                dgBill.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                        }
                        else
                        {
                            dgBill.Rows[rowIndex].Cells[ColIndex.Chk].Value = false;
                            AdvAdjAmt = AdvAdjAmt - Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                            Amt = Amt - Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value);
                            dgBill.Rows[rowIndex].Cells[ColIndex.Amount].Value = "0.00";
                        }
                    }
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }
            }
        }

        private void dgAdvance_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgAdvance.CurrentCell == null)
                    return;
                int rowIndex = dgAdvance.CurrentCell.RowIndex;
                if (e.KeyCode == Keys.Space && dgAdvance.CurrentCell.ColumnIndex == 8)
                {
                    if (Convert.ToBoolean(dgAdvance.Rows[rowIndex].Cells[8].Value) == false)
                    {
                        dgAdvance.Rows[rowIndex].Cells[8].Value = true;
                        for (int i = 0; i < dgAdvance.Rows.Count; i++)
                        {
                            if (i != rowIndex)
                                dgAdvance.Rows[i].Cells[8].Value = false;
                        }
                        TolAdvadjAmt = Convert.ToDouble(dgAdvance.Rows[rowIndex].Cells[5].Value);
                        AdvRowIndex = rowIndex;
                        AdvAdjAmt = 0;
                        Amt = 0;
                        Advflag = false;
                        BindGridAdv();
                        dgBill.Visible = true;
                    }
                    else
                    {
                        dgAdvance.Rows[rowIndex].Cells[8].Value = false;
                        TolAdvadjAmt = 0;
                        AdvRowIndex = 0;
                        AdvAdjAmt = 0;
                        Amt = 0;
                        Advflag = false;
                        dgBill.Visible = false;
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (Convert.ToDouble(dgAdvance.Rows[rowIndex].Cells[4].Value) == 0)
                    {
                        if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            dbTVoucherEntry = new DBTVaucherEntry();
                            tVoucherEntry = new TVoucherEntry();
                            tVoucherEntry.PkVoucherNo = ObjQry.ReturnLong("Select FKVoucherNo From TVoucherDetails Where PKVoucherTrnNo in(Select FKVoucherTrnNo From  TVoucherRefDetails Where PKRefTrnNo=" + Convert.ToInt64(dgAdvance.Rows[rowIndex].Cells[7].Value) + ") ", CommonFunctions.ConStr);
                            dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);
                            OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                            rbAgainstAdv_CheckedChanged(rbAgainstAdv, new EventArgs());
                        }

                    }
                    else DisplayMessage("This advance amount already adjusted to another bill. Not allowed delete bill.");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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

                dtCompRatio = ObjFunction.GetDataView("SELECT     TVEC.CompanyNo, TVEC.MfgCompNo, TVEC.BilledAmount * 10 / TVE.BilledAmount " +
                            " FROM         TVoucherEntry AS TVE INNER JOIN " +
                            " TVoucherEntryCompany AS TVEC ON TVE.PkVoucherNo = TVEC.FkVoucherNo " +
                            " WHERE     (TVE.PkVoucherNo = " + PkVoucherNo + ")").Table;
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

        private void dgBillSelection_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.NetBal].Value) < (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.DiscAmt1].Value)))
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
                                BeginInvoke(move2n, new object[] { rowCount + 1, ColIndex.Amount, dgBillSelection });   //GridView.CurrentCell = GridView[ColIndex.PayType, rowCount];
                            //GridView.CurrentCell = GridView[ColIndex.PayType, rowCount];

                        }
                        double TotalAmt = 0;
                        for (int i = 0; i < dgBillSelection.RowCount; i++)
                        {
                            TotalAmt = TotalAmt + Convert.ToDouble(dgBillSelection.Rows[i].Cells[ColIndex.Amount].Value);
                        }
                        txtAmount.Text = TotalAmt.ToString("0.00");
                    }
                    else
                    {
                        dgBillSelection.CurrentCell.Value = "0.00";
                        dgBillSelection.CurrentRow.Cells[ColIndex.Chk].Value = false;
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, dgBillSelection });

                    }
                }
                else if (dgBillSelection.CurrentCell.ColumnIndex == ColIndex.DiscAmt1)
                {
                    if (dgBillSelection.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(dgBillSelection.CurrentCell.Value.ToString()) == false)
                            dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Disc Amount";
                        else if (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.NetBal].Value) < (Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBillSelection.Rows[rowCount].Cells[ColIndex.DiscAmt1].Value)))
                        {
                            dgBillSelection.CurrentCell.ErrorText = "Please Enter Valid Disc Amount";
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, dgBillSelection });
                        }
                    }

                    else
                    {
                        dgBillSelection.CurrentCell.Value = "0.00";
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { rowCount, ColIndex.Amount, dgBillSelection });
                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtMainRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnSave.Focus();
            }
        }

        private void txtCrCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCrCompanyBank.Focus();
            }
        }

        private void cmbCrBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbCrCompanyBank.Focus();
            }
        }

        private void cmbCrCompanyBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCrOk.Focus();
            }
        }

        private void btnCrOk_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (CreditValidations() == true)
                {
                    BtnSave.Enabled = true;
                    if (rbBillwise.Checked == true)
                    {
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtCrCardNo.Text;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = Convert.ToDateTime("01-01-1900");
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbCrBank.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbCrBranch.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCrCompanyBank);//
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                        pnlCredit.Visible = false;
                        GridView.Focus();
                        if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                        {
                            GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                        }
                        else
                        {
                            GridView.CurrentCell = GridView[ColIndex.Remark, GridView.CurrentCell.RowIndex];

                        }
                    }
                    else if (rbBillSelection.Checked == true)
                    {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtCrCardNo.Text;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = Convert.ToDateTime("01-01-1900");
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbCrBank.SelectedValue;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbCrBranch.SelectedValue;
                        // if (VoucherType == VchType.Sales)
                        {
                            dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCrCompanyBank);//dtPayTypeLedger.Rows[2].ItemArray[1].ToString();
                            LedgNo = ObjFunction.GetComboValue(cmbCrCompanyBank);//Convert.ToInt64(dtPayTypeLedger.Rows[2].ItemArray[1].ToString());
                        }
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = ObjFunction.GetComboValue(cmbPayType);
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = cmbPayType.Text;
                        pnlCredit.Visible = false;
                        dgBillSelection.Focus();
                        if (dgBillSelection.CurrentCell.RowIndex < dgBillSelection.Rows.Count - 1)
                            dgBillSelection.CurrentCell = dgBillSelection[ColIndex.Amount, dgBillSelection.CurrentCell.RowIndex + 1];
                        BtnSave.Focus();
                    }
                    else if (rbAdjustment.Checked == true)
                    {
                        pnlCredit.Visible = false;
                        BtnSave.Focus();
                    }
                    else if (rbAdvance.Checked == true)
                    {
                        pnlCredit.Visible = false;
                        BtnSave.Focus();
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void txtDiscPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataGridView gv = null;
                if (rbBillwise.Checked == true) gv = GridView;
                else if (rbBillSelection.Checked == true) gv = dgBillSelection;
                if (e.KeyChar == Convert.ToChar(Keys.Enter))

                {

                    if (ObjFunction.CheckValidAmount(txtDiscPer.Text.Replace("-", "")) == true)
                    {
                        if (Convert.ToDouble(txtDiscPer.Text.Trim()) > Convert.ToDouble(lblAmount.Text))
                        {
                            if (OMMessageBox.Show("Enter Amount less than Total Amount \nExceed Amount consider as advance amount. want to continue, click on Yes or No", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Information) == DialogResult.No)
                            {
                                txtDiscPer.Focus();
                                return;
                            }
                        }
                        //if (Convert.ToDouble(txtAmount.Text.Trim()) <= Convert.ToDouble(lblAmount.Text))
                        //{
                        txtAmount.Text = Convert.ToDouble((txtAmount.Text == "") ? "0" : txtAmount.Text).ToString();
                        double Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        for (int i = 0; i < gv.Rows.Count; i++)
                        {
                            if (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value) <= Amount)
                            {
                                double discrs = (Convert.ToDouble(txtDiscPer.Text) * (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value)) / 100);
                                gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = Math.Round((discrs), 0).ToString("0.00");
                                //  gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = "0";
                                //  gv.Rows[i].Cells[ColIndex.Amount].Value = gv.Rows[i].Cells[ColIndex.NetBal].Value;
                                gv.Rows[i].Cells[ColIndex.Chk].Value = "True";
                                Amount = Amount - Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value);
                            }
                            else if (Amount != 0)
                            {
                                if (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value) <= Amount)
                                {
                                    double discrs = (Convert.ToDouble(txtDiscPer.Text) * (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value)) / 100);
                                    gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = Math.Round((discrs), 0).ToString("0.00");
                                    gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = "0";
                                    // gv.Rows[i].Cells[ColIndex.Amount].Value = Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value);
                                    Amount = Amount - Convert.ToDouble(gv.Rows[i].Cells[ColIndex.Amount].Value);
                                    gv.Rows[i].Cells[ColIndex.Chk].Value = "True";
                                }
                                else
                                {
                                    double discrs = (Convert.ToDouble(txtDiscPer.Text) * (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value)) / 100);
                                    gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = Math.Round((discrs), 0).ToString("0.00");
                                    //  gv.Rows[i].Cells[ColIndex.Amount].Value ="0.00";
                                    gv.Rows[i].Cells[ColIndex.Chk].Value = "True";
                                    Amount = 0;
                                }
                            }
                            else
                            {
                                double discrs = (Convert.ToDouble(txtDiscPer.Text) * (Convert.ToDouble(gv.Rows[i].Cells[ColIndex.NetBal].Value)) / 100);
                                gv.Rows[i].Cells[ColIndex.DiscAmt1].Value = Math.Round((discrs), 0).ToString("0.00");
                                //  gv.Rows[i].Cells[ColIndex.Amount].Value ="0.00";
                                gv.Rows[i].Cells[ColIndex.Chk].Value = "True";
                            }
                        }
                        gv.CurrentCell = gv[ColIndex.Amount, 0];
                        gv.Focus();

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

        private void txtDiscPer_TextChanged(object sender, EventArgs e)
        {

            ObjFunction.SetMasked(txtAmount, 2, 9, OMFunctions.MaskedType.NotNegative);
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


        private void CalculateRecAmount()
        {
            double amt = 0;
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                amt += Convert.ToDouble(GridView.Rows[i].Cells[ColIndex.Amount].Value);
            }
            lblTotRecAmt.Text = "Total Rec. Amt:" + amt.ToString(Format.DoubleFloating);
        }

        private void txtChqNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                //e.SuppressKeyPress = true;
                dtpChqDate.Focus();
            }
        }

        private void dtpChqDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbBank.Visible == true)
                {
                    //e.SuppressKeyPress = true;
                    cmbBank.Focus();
                }
                else { cmbCompanyBank.Focus(); }
            }
        }

        private void cmbBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (VoucherType == VchType.Purchase)
                {
                    btnchqOk.Focus();
                }
                else
                    cmbCompanyBank.Focus();
            }
        }

        private void cmbCompanyBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                btnchqOk.Focus();


            }

        }
        private void btnchqOk_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (ChqValidations() == true)
                {
                    BtnSave.Enabled = true;
                    if (rbBillwise.Checked == true)
                    {
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtChqNo.Text;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = dtpChqDate.Value;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = lstPayType.Text;
                        // if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCompanyBank);

                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = lstPayType.SelectedValue;

                        pnlchq.Visible = false;
                        GridView.Focus();
                        if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                            GridView.CurrentCell = GridView[ColIndex.Amount, GridView.CurrentCell.RowIndex + 1];
                    }
                    else if (rbBillSelection.Checked == true)
                    {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqNo].Value = txtChqNo.Text;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.ChqDate].Value = dtpChqDate.Value;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayType].Value = cmbPayType.Text;
                        // if ((VoucherType == VchType.Sales) || (VoucherType == VchType.DSales))
                        //  {
                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = ObjFunction.GetComboValue(cmbCompanyBank);//dtPayTypeLedger.Rows[1].ItemArray[1].ToString();
                        LedgNo = ObjFunction.GetComboValue(cmbCompanyBank);

                        dgBillSelection.Rows[dgBillSelection.CurrentCell.RowIndex].Cells[ColIndex.PayTypeNo].Value = ObjFunction.GetComboValue(cmbPayType);
                        pnlchq.Visible = false;
                        dgBillSelection.Focus();
                        if (dgBillSelection.CurrentCell.RowIndex < dgBillSelection.Rows.Count - 1)
                            dgBillSelection.CurrentCell = dgBillSelection[ColIndex.Amount, dgBillSelection.CurrentCell.RowIndex + 1];
                        BtnSave.Focus();
                    }
                    else if (rbAdjustment.Checked == true)
                    {
                        BtnSave.Focus();
                        pnlchq.Visible = false;
                    }
                    else if (rbAdvance.Checked == true)
                    {
                        BtnSave.Focus();
                        pnlchq.Visible = false;
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


