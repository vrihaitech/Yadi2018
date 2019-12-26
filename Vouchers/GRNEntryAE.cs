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
    public partial class GRNEntryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTGRN dbTGrn = new DBTGRN();
        TGRN tGrn = new TGRN();
        TGRNDetails tGrnDetails = new TGRNDetails();
        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtUOMTemp = new DataTable();
        DataTable dtVchMainDetails = new DataTable();
        DataTable dtCompRatio = new DataTable();
        DataTable dtVchPrev = new DataTable();
        DataTable dtPayLedger = new DataTable();
        DataTable dtItemLevelDisc = new DataTable();
        DataTable dtVoucherEntry = new DataTable();
        DataTable dtStock = new DataTable();
        DataTable dtSaleLedger = new DataTable();
        DataTable dtTAxLedger = new DataTable();
        Color clrColorRow = Color.FromArgb(255, 224, 192);

        int cntRow, BillingMode, rowQtyIndex;

        bool Spaceflag = true, BillSizeFlag = false;
        public bool RewardDeleteFlag = false, RewardFlag = false, DiscFlag = false;
        long ItemNameType = 0, RateTypeNo, PartyNo, ParkBillNo;
        int iItemNameStartIndex = 3, ItemType = 0;
        string strUom, Param1Value = "", Param2Value = "";
        string[] strItemQuery, strItemQuery_last;
        bool isDoProcess = false;


        public static long NSalesCompNo = 1;

        DataTable dt = new DataTable();
        DataTablesCollection dtBillCollect = new DataTablesCollection();
        string cmbRateType = "", cmbTaxType = "";
        long TaxTypeNo;
        bool flagParking = false;
        bool StopOnQty = false, StopOnRate = false;
        public long RequestSalesNo, ID, VoucherType;




        public GRNEntryAE()
        {
            InitializeComponent();
        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            if (e.ColumnIndex == ColIndex.ItemName)
            {

                //else dgBill.Rows[e.RowIndex].ReadOnly = false;
            }
            if (e.ColumnIndex == ColIndex.ItemName)
            {
                if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "")
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "0")
                        dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                }


            }

            //dgBill.CurrentRow.Selected = true;
        }

        private void SalesAE_Load(object sender, EventArgs e)
        {
            try
            {
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                btnInsScheme.Visible = false;


                dgBill.Enabled = false;
                VoucherType = VchType.Sales;
                lblMessage.ForeColor = Color.Red;
                RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType));
                TaxTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_TaxType));
                cmbTaxType = ObjQry.ReturnString("Select GroupName from MGroup where GroupNo=" + TaxTypeNo + "", CommonFunctions.ConStr);
                cmbRateType = GetRateType(RateTypeNo);
                InitDelTable();

                ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_ItemNameType)); //deepak
                initItemQuery();



                ObjFunction.FillComb(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and IsActive='true' order by LedgerName");
                ObjFunction.FillComb(cmbGodown, "Select GodownNo,GodownName from MGodown Where GodownNo<>1 and IsActive='true'");
                ObjFunction.FillCombo(cmbPartyNameSearch, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ")  order by LedgerName");

                FillRateType();


                cmbPartyName.Enabled = false;//deepak
                txtInvNo.Enabled = false;
                dtpBillDate.Enabled = false;
                dtpBillTime.Enabled = false;
                dtpBillTime.Format = DateTimePickerFormat.Time;


                InitControls();
                StopOnQty = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnQty));// Convert.ToBoolean(dtSalesSetting.Rows[0].ItemArray[14].ToString());
                StopOnRate = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnRate)); //Convert.ToBoolean(dtSalesSetting.Rows[0].ItemArray[13].ToString());

                dtSearch = ObjFunction.GetDataView("Select GRNNo from TGRN " +
                    " order by GRNUserNo ").Table;//AND IsVoucherLock='false'

                if (dtSearch.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    FillControls();
                    SetNavigation();
                }

                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);

                DataTable dtSettings = ObjFunction.GetDataView("Select PKSettingNo From MSettings Where SettingTypeNo=4").Table;

                txtGrandTotal.Font = new Font("Verdana", 18, FontStyle.Bold);
                txtGrandTotal.ForeColor = Color.White;

                lblGrandTotal.Font = new Font("Verdana", 22, FontStyle.Bold);
                lblGrandTotal.ForeColor = Color.White;



                dgBill.ColumnHeadersDefaultCellStyle.Font = new Font("Verdana", 9, FontStyle.Bold);
                dgBill.RowHeadersDefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
                dgBill.RowTemplate.DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
                if (dgBill.Rows.Count > 0)
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                        dgBill.Rows[i].DefaultCellStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
                }

                new GridSearch(dgItemList, 1, 2);
                //new GridSearch(dgItemList,

                formatPics();

                dtpBillDate.CustomFormat = "dd-MMM-yy"; dtpBillDate.Width = 90;

                dgBill.BackgroundColor = Color.FromArgb(255, 255, 210);




                btnShortcut.BackColor = Color.FromArgb(255, 128, 0);




                txtSchemeDisc.Enabled = false;
                txtOtherDisc.Enabled = false;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public string GetRateType(long val)
        {
            string str = "";
            if (val == 1) str = "ASaleRate";
            else if (val == 2) str = "BSaleRate";
            else if (val == 3) str = "CSaleRate";
            else if (val == 4) str = "DSaleRate";
            else if (val == 5) str = "ESaleRate";
            else if (val == 6) str = "PurRate";
            return str;
        }

        private void formatPics()
        {
            try
            {
                pnlItemName.Width = 620;//560;
                pnlItemName.Height = 235;
                pnlItemName.Top = 90;
                pnlItemName.Left = 42;

                pnlGroup1.Top = 88;
                pnlGroup1.Left = 150;
                pnlGroup1.Width = 300;
                pnlGroup1.Height = 275;//205;// 220;

                pnlGroup2.Top = 88;
                pnlGroup2.Left = 100;
                pnlGroup2.Width = 300;
                pnlGroup2.Height = 220;

                pnlUOM.Top = 88;
                pnlUOM.Left = 372;
                pnlUOM.Width = 120;
                pnlUOM.Height = 80;

                pnlRate.Top = 88;
                pnlRate.Left = 430;
                pnlRate.Width = 120;
                pnlRate.Height = 80;


                btnInsScheme.Left = btnShortcut.Left;
                btnInsScheme.Top = btnShortcut.Bottom + 5;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void initItemQuery()
        {
            try
            {
                DataTable dtItemQuery = new DataTable();
                dtItemQuery = ObjFunction.GetDataView("SELECT * from MItemNameDisplayType WHERE ItemNameTypeNo = " + ItemNameType).Table;

                if (dtItemQuery.Rows.Count == 1)
                {
                    int qCount = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (dtItemQuery.Rows[0]["Query" + i] != null && dtItemQuery.Rows[0]["Query" + i].ToString().Trim().Length > 0)
                        {
                            qCount++;
                        }
                    }

                    strItemQuery = new string[qCount];
                    strItemQuery_last = new string[qCount];
                    for (int i = 0; i < strItemQuery_last.Length; i++)
                    {
                        strItemQuery_last[i] = "";
                    }
                    qCount = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (dtItemQuery.Rows[0]["Query" + i] != null && dtItemQuery.Rows[0]["Query" + i].ToString().Trim().Length > 0)
                        {
                            strItemQuery[qCount] = dtItemQuery.Rows[0]["Query" + i].ToString().Trim();
                            qCount++;
                        }
                    }

                    iItemNameStartIndex = Convert.ToInt32(dtItemQuery.Rows[0]["StartIndex"].ToString());
                    Param1Value = dtItemQuery.Rows[0]["Param1Value"].ToString();
                    Param2Value = dtItemQuery.Rows[0]["Param2Value"].ToString();
                }
                else
                {
                    OMMessageBox.Show("Please Select Valid Item Name display type in Sales Setting Form ...");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void InitControls()
        {
            try
            {
                flagParking = false;
                dtBillCollect = new DataTablesCollection();
                dtpBillDate.Value = DBGetVal.ServerTime;
                dtpBillTime.Value = DBGetVal.ServerTime;

                while (dgBill.Rows.Count > 0)
                {
                    dgBill.Rows.RemoveAt(0);
                }

                dgBill.Rows.Add();


                txtInvNo.Text = (ObjQry.ReturnLong("Select max(GRNUserNo) from TGRN ", CommonFunctions.ConStr) + 1).ToString();


                string sqlQuery = " SELECT 0 AS Sr, mItemMaster.ItemName, TStock.Quantity, MUOM.UOMName, TStock.Rate, TStock.Amount,MStockBarcode.Barcode, " +
                             " TStock.PkStockTrnNo, MStockBarcode.PkStockBarcodeNo,TVoucherEntry.PkVoucherNo, mItemMaster.ItemNo, MUOM.UOMNo " +
                             " FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                             " INNER JOIN TStock INNER JOIN MStockItems ON TStock.ItemNo = mItemMaster.ItemNo ON " +
                             " TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo INNER JOIN " +
                             " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN " +
                             " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo " +
                             " WHERE (MStockBarcode.Barcode = '') AND (TVoucherDetails.VoucherSrNo = 1) AND " +
                             " (TVoucherEntry.VoucherTypeCode IN (9, 21))";
                dt = ObjFunction.GetDataView(sqlQuery).Table;
                dtpBillDate.Focus();
                //txtRefNo.Focus();
                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDate)) == true) 
                //else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true) cmbPartyName.Focus();

                //else dgBill.Focus();
                //dgBill.Focus();
                lblMessage.Text = "";
                lblMessage.Visible = false;
                dgBill.CurrentCell = dgBill[1, 0];
                cmbPartyName.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC));

                txtSubTotal.Text = "0.00";
                txtGrandTotal.Text = "0.00"; txtTotalDisc.Text = "0.00"; txtTotalTax.Text = "0.00";
                txtSchemeDisc.Text = "0.00"; txtOtherDisc.Text = "0.00"; txtRoundOff.Text = "0.00";
                txtSchemeDisc.Enabled = false; txtOtherDisc.Enabled = false;
                cmbPartyName.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC));


                btnBillCancel.Visible = DBGetVal.IsAdmin;
                ParkBillNo = 0;

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
                tGrn = dbTGrn.ModifyTGRNByID(ID);

                txtGrandTotal.Text = tGrn.GRNAmount.ToString();
                ObjFunction.FillComb(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo in " +
                    "(" + GroupType.SundryCreditors + ") and IsActive='true' or LedgerNo=" + tGrn.LedgerNo + " order by LedgerName");

                cmbPartyName.SelectedValue = tGrn.LedgerNo.ToString();
                dtpBillDate.Value = tGrn.GRNDate;
                dtpBillTime.Value = tGrn.GRNTime;
                long GodownNo = ObjQry.ReturnLong("Select GodownNo from TGRNDetails  where FkGRNNo=" + ID + "", CommonFunctions.ConStr);
                cmbGodown.SelectedValue = GodownNo.ToString();
                txtInvNo.Text = tGrn.GRNUserNo.ToString();
                txtRefNo.Text = tGrn.RefNo.ToString();
                //cmbPaymentType.SelectedValue = ObjQry.ReturnLong("Select LedgerNo From TOtherVoucherDetails Where VoucherSrNo=2 AND FKVoucherNo=" + ID + "", CommonFunctions.ConStr).ToString();

                if (tGrn.IsCancel == true)
                {
                    lblMessage.Text = "GRN Cancel";
                    lblMessage.Visible = true;
                }
                else lblMessage.Visible = false;

                FillGrid();
                if (ObjQry.ReturnLong("Select Count(*) from TGRNInvoice where FkGRNNo=" + ID + "", CommonFunctions.ConStr) > 0)
                {
                    btnUpdate.Visible = false; btnBillCancel.Visible = false;
                    lblMessage.Visible = false;
                }
                else
                {
                    btnUpdate.Visible = true; btnBillCancel.Visible = true;
                }
                if (tGrn.IsCancel == true)
                {
                    btnUpdate.Visible = false; btnBillCancel.Visible = false;
                }
                else
                {
                    btnUpdate.Visible = true; btnBillCancel.Visible = true;
                }

                btnInsScheme.Enabled = false;
                txtSchemeDisc.Enabled = false;
                txtOtherDisc.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillGrid()
        {
            try
            {
                dgBill.Rows.Clear();
                string sqlQuery = "";
                //if (IsSuperMode() == true)

                sqlQuery = " SELECT   0 AS Sr,(SELECT ItemName FROM dbo.MStockItems_V(NULL, mItemMaster.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, " +
                       " TGRNDetails.Quantity, MUOM.UOMName, TGRNDetails.Rate, TGRNDetails.NetRate, TGRNDetails.NetAmount,  " +
                       " TGRNDetails.Amount, MStockBarcode.Barcode, TGRNDetails.PkSrNo AS DeliveryDtlsNo, MStockBarcode.PkStockBarcodeNo, " +
                       " TGRN.GRNNo, mItemMaster.ItemNo, MUOM.UOMNo, MRateSetting.PkSrNo, MRateSetting.StockConversion, TGRNDetails.BilledQuantity, " +
                       " MRateSetting.MKTQty, mItemMaster.CompanyNo FROM  MStockItems INNER JOIN " +
                       " TGRNDetails ON mItemMaster.ItemNo = TGRNDetails.ItemNo INNER JOIN " +
                       " TGRN ON TGRNDetails.FkGRNNo = TGRN.GRNNo INNER JOIN " +
                       " MRateSetting ON TGRNDetails.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN " +
                       " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                       " MUOM ON MRateSetting.UOMNo = MUOM.UOMNo where TGRN.GRNNo=" + ID + " order by TGRNDetails.PkSrNo";

                dt = ObjFunction.GetDataView(sqlQuery).Table;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBill.Rows.Add();
                    for (int i = 0; i < dgBill.Columns.Count; i++)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dgBill.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                            //dgBill.Rows[j].Cells[i].ReadOnly = true;
                        }
                    }
                }

                dgBill.Rows.Add();
                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                CalculateTotal();


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        public void CalculateTotal()
        {
            try
            {
                txtSubTotal.Text = "0.00";

                txtGrandTotal.Text = "0.00";
                txtTotalDisc.Text = "0.00";
                txtTotalTax.Text = "0.00";
                double subTotal = 0, totalDisc = 0, totalChrg = 0, totalTax = 0, TotFinal = 0;
                if (Validations() == true)
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                    {
                        if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                        {
                            if (dgBill.Rows[i].Cells[ColIndex.Quantity].Value == null) dgBill.Rows[i].Cells[ColIndex.Quantity].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value == null) dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.StockFactor].Value == null) dgBill.Rows[i].Cells[ColIndex.StockFactor].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.Rate].Value == null) dgBill.Rows[i].Cells[ColIndex.Rate].Value = 0;



                            double Amount = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value))).ToString("0.0000"));

                            //DiscAmt += Convert.ToDouble((((Amount - DiscAmt) * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.0000"));





                            double ttRate = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) == 0) ? 0 : Amount / Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value);


                            dgBill.Rows[i].Cells[ColIndex.Amount].Value = Amount.ToString("0.00");

                            dgBill.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = (ttRate * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.ActualQty].Value = ((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.StockFactor].Value)));

                            subTotal = subTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetAmt].Value);




                        }
                    }
                    subTotal = Convert.ToDouble(subTotal.ToString("0.00"));// Math.Round(subTotal, 00);
                    txtSubTotal.Text = (subTotal + totalDisc).ToString("0.00");
                    txtTotalDisc.Text = totalDisc.ToString("0.00");

                    double TotalAmt = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) + totalTax) - Convert.ToDouble(txtTotalDisc.Text)).ToString("0.00"));
                    //txtDiscRupees1.Text = Convert.ToDouble((TotalAmt * Convert.ToDouble(txtDiscount1.Text)) / 100).ToString("0.00");
                    if (txtSchemeDisc.Text.Trim() == "") txtSchemeDisc.Text = "0.00";
                    if (txtOtherDisc.Text.Trim() == "") txtOtherDisc.Text = "0.00";

                    TotalAmt -= Convert.ToDouble(txtSchemeDisc.Text) + Convert.ToDouble(txtOtherDisc.Text);


                    totalDisc = Convert.ToDouble(txtSchemeDisc.Text) + Convert.ToDouble(txtOtherDisc.Text);

                    txtTotalAnotherDisc.Text = totalDisc.ToString("0.00");
                    txtTotalChrgs.Text = totalChrg.ToString("0.00");
                    txtTotalTax.Text = totalTax.ToString("0.00");


                    totalTax = Convert.ToDouble(totalTax.ToString("0.00"));// Math.Round(totalTax, 00);
                    txtGrandTotal.Text = ((subTotal + totalTax + totalChrg) - totalDisc).ToString("0.00");
                    TotFinal = Math.Round(Convert.ToDouble(txtGrandTotal.Text), MidpointRounding.AwayFromZero);

                    txtRoundOff.Text = "0.00";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validations()
        {
            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void setCompanyRatio(double GrandTotal, double TotDisc, double TotRoundOff, double TotCharges)
        {
            //DataTable dtTemp = new DataTable();
            //bool TempFlag = false;
            //dtTemp.Columns.Add();
            //DataRow dr = dtTemp.NewRow();
            //dr[0] = Convert.ToInt64(dtStock.Rows[0].ItemArray[Swami.Vouchers.VoucherPostingAE.ColStock.StockCompanyNo].ToString());
            //dtTemp.Rows.Add(dr);
            //for (int k = 0; k < dtStock.Rows.Count; k++)
            //{
            //    for (int i = 0; i < dtTemp.Rows.Count; i++)
            //    {
            //        if (Convert.ToInt64(dtStock.Rows[k].ItemArray[Swami.Vouchers.VoucherPostingAE.ColStock.StockCompanyNo].ToString()) != Convert.ToInt64(dtTemp.Rows[i].ItemArray[0].ToString()))
            //        {
            //            TempFlag = true;
            //        }
            //        else
            //        {
            //            TempFlag = false;
            //            break;
            //        }
            //    }
            //    if (TempFlag == true)
            //    {
            //        dr = dtTemp.NewRow();
            //        dr[0] = Convert.ToInt64(dtStock.Rows[k].ItemArray[Swami.Vouchers.VoucherPostingAE.ColStock.StockCompanyNo].ToString());
            //        dtTemp.Rows.Add(dr);
            //    }
            //}

            //dtCompRatio = new DataTable();
            //dtCompRatio.Columns.Add();
            //dtCompRatio.Columns.Add();
            //double debit = 0;
            //for (int k = 0; k < dtTemp.Rows.Count; k++)
            //{
            //    for (int j = 0; j < dtStock.Rows.Count; j++)
            //    {
            //        if (Convert.ToInt64(dtStock.Rows[j].ItemArray[Swami.Vouchers.VoucherPostingAE.ColStock.StockCompanyNo].ToString()) == Convert.ToInt64(dtTemp.Rows[k].ItemArray[0].ToString()))
            //        {
            //            debit = debit + Convert.ToDouble(dtStock.Rows[j].ItemArray[Swami.Vouchers.VoucherPostingAE.ColStock.Amount].ToString());
            //        }
            //    }

            //    DataRow dr1 = dtCompRatio.NewRow();
            //    dr1[0] = Convert.ToInt64(dtTemp.Rows[k].ItemArray[0].ToString());
            //    dr1[1] = (debit * 10) / ((GrandTotal + TotDisc - TotRoundOff) - TotCharges);
            //    dtCompRatio.Rows.Add(dr1);
            //    debit = 0;
            //}


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (ObjQry.ReturnLong("Select Count(*) from TGRNInvoice where FkGRNNo=" + ID + "", CommonFunctions.ConStr) > 0)
                {
                    OMMessageBox.Show("Already this GRN generated invoice", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
            }
            SetValue();
        }

        public void SetValue()
        {
            if (ValidationsMain() == true)
            {
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                dbTGrn = new DBTGRN();

                tGrn = new TGRN();
                long GRNUserNo = 0;
                if (ID != 0)
                    GRNUserNo = ObjQry.ReturnLong("Select GrnUserNo from TGRN where GRNNo=" + ID + "", CommonFunctions.ConStr);


                tGrn.GRNNo = ID;
                tGrn.GRNUserNo = GRNUserNo;
                tGrn.GRNDate = Convert.ToDateTime(dtpBillDate.Text);
                tGrn.GRNTime = dtpBillTime.Value;
                tGrn.LedgerNo = ObjFunction.GetComboValue(cmbPartyName);
                tGrn.RefNo = txtRefNo.Text.Trim();
                tGrn.GRNAmount = Convert.ToDouble(txtGrandTotal.Text);
                tGrn.CompanyNo = DBGetVal.FirmNo;
                tGrn.UserDate = DBGetVal.ServerTime.Date;
                tGrn.UserID = DBGetVal.UserID;
                dbTGrn.AddTGRN(tGrn);

                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    tGrnDetails = new TGRNDetails();
                    tGrnDetails.PkSrNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value);
                    tGrnDetails.GRNSrNo = i + 1;
                    tGrnDetails.ItemNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value);
                    tGrnDetails.Quantity = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value);
                    tGrnDetails.BilledQuantity = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value);
                    tGrnDetails.Rate = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value);
                    tGrnDetails.Amount = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].Value);
                    tGrnDetails.NetRate = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetRate].Value);
                    tGrnDetails.NetAmount = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetAmt].Value);
                    tGrnDetails.FkRateSettingNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value);
                    tGrnDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.StockCompanyNo].Value);
                    tGrnDetails.GodownNo = ObjFunction.GetComboValue(cmbGodown);//Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.GodownNo].Value);
                    tGrnDetails.UserID = DBGetVal.UserID;
                    tGrnDetails.UserDate = DBGetVal.ServerTime.Date;
                    dbTGrn.AddTGRNDetails(tGrnDetails);
                }
                DeleteValues();
                long tempID = dbTGrn.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    if (ID == 0)
                    {
                        OMMessageBox.Show("GRN Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        dtSearch = ObjFunction.GetDataView("Select GRNNo from TGRN order by GRNUserNo ").Table;
                        ID = tempID;
                        PartyNo = 0;
                        SetNavigation();
                        btnNew_Click(null, null);
                    }
                    else
                    {
                        OMMessageBox.Show("GRN Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                        setDisplay(true);
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnSearch.Visible = true;
                        dgBill.Enabled = false;
                        btnNew.Focus();
                    }
                    //labelB.Visible = true;

                    //}
                    //else
                    //    MessageBox.Show("Bill Not Added Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OMMessageBox.Show("GRN Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }

        }

        private void setCompanyRatio()
        {
            try
            {
                DataTable dtTemp = new DataTable();
                bool TempFlag = false;
                dtTemp.Columns.Add();
                DataRow dr = dtTemp.NewRow();
                dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                dtTemp.Rows.Add(dr);
                for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                {
                    for (int i = 0; i < dtTemp.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTemp.Rows[i].ItemArray[0].ToString()))
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
                        dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                        dtTemp.Rows.Add(dr);
                    }
                }

                dtCompRatio = new DataTable();
                dtCompRatio.Columns.Add();
                dtCompRatio.Columns.Add();
                double debit = 0;
                for (int k = 0; k < dtTemp.Rows.Count; k++)
                {
                    for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                    {
                        if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTemp.Rows[k].ItemArray[0].ToString()))
                        {
                            debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.Amount].Value);
                        }
                    }

                    DataRow dr1 = dtCompRatio.NewRow();
                    dr1[0] = Convert.ToInt64(dtTemp.Rows[k].ItemArray[0].ToString());
                    dr1[1] = (debit * 10) / ((Convert.ToDouble(txtGrandTotal.Text) + Convert.ToDouble(txtTotalAnotherDisc.Text) - Convert.ToDouble(txtRoundOff.Text)) - Convert.ToDouble(txtTotalChrgs.Text));
                    dtCompRatio.Rows.Add(dr1);
                    debit = 0;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool ValidationsMain()
        {
            bool flag = false;
            try
            {
                if (ObjFunction.GetComboValue(cmbPartyName) <= 0)
                {
                    OMMessageBox.Show("Please Select Party Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    cmbPartyName.Focus();
                }
                else if (ObjFunction.GetComboValue(cmbGodown) <= 0)
                {
                    OMMessageBox.Show("Please Select Godown", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    cmbGodown.Focus();
                }
                else if (txtRefNo.Text.Trim() == "")
                {
                    OMMessageBox.Show("Enter RefNo", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    txtRefNo.Focus();
                }
                else flag = true;

                if (flag == true)
                {
                    long Count = ObjQry.ReturnLong("Select Count(*) from TGRN where RefNo='" + txtRefNo.Text.Trim() + "' and LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and GRNDate='" + dtpBillDate.Text + "' and GRNNo!=" + ID + "", CommonFunctions.ConStr);
                    if (Count > 0)
                    {
                        OMMessageBox.Show("This RefNo is already exist ....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        cmbPartyName.Focus();
                        flag = false;
                    }

                }

                if (flag == true)
                {
                    for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                    {
                        DataGridViewRow dr = dgBill.Rows[i];
                        if (dr.Cells[ColIndex.PkBarCodeNo].Value == null && dr.Cells[ColIndex.PkRateSettingNo].Value == null)
                        {
                            flag = false;
                            OMMessageBox.Show("Please Fill properly Sr No. " + (i + 1) + " of item..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            break;
                        }
                        else if (dgBill.Rows[i].Cells[ColIndex.PkBarCodeNo].Value.ToString() == "0")
                        {
                            flag = false;
                            OMMessageBox.Show("Please Fill properly Sr No. " + (i + 1) + " of item..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            break;
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

        private void Control_Leave(object sender, EventArgs e)
        {

        }

        #region dgBill Methods and Events
        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void Desc_Start()
        {
            try
            {
                if (dgBill.CurrentCell.Value == null || Convert.ToString(dgBill.CurrentCell.Value) == "")
                {
                    ItemType = 1;
                    FillItemList(0, ItemType); //FillItemList();
                }
                else
                {
                    ItemType = 2;
                    long[] BarcodeNo = null; long[] ItemNo = null;

                    //new code
                    switch (dgBill.CurrentCell.Value.ToString().Trim().ToUpper())
                    {
                        case "SV":
                            {
                                if (btnSave.Visible)
                                {
                                    //cmbPaymentType.SelectedValue = "2";

                                    btnSave_Click(btnSave, null);
                                    return;
                                }
                                break;
                            }
                        case "SVP":
                            {
                                if (btnSave.Visible)
                                {
                                    //cmbPaymentType.SelectedValue = "2";

                                    btnSave_Click(btnSave, null);
                                    return;
                                }
                                break;
                            }
                        case "CRP":
                            {
                                if (btnSave.Visible)
                                {
                                    dgBill.CurrentCell.Value = "";


                                    cmbPartyName.Focus();
                                    return;
                                }
                                break;
                            }
                        case "CHQ":
                            {
                                if (btnSave.Visible)
                                {
                                    dgBill.CurrentCell.Value = "";

                                    return;
                                }
                                break;
                            }
                        case "CRD":
                            {
                                if (btnSave.Visible)
                                {
                                    dgBill.CurrentCell.Value = "";

                                    return;
                                }
                                break;
                            }
                        case "B":
                            {
                                if (btnSave.Visible)
                                {
                                    if (ObjFunction.GetComboValue(cmbPartyName) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC)))// && (ObjFunction.GetComboValue(cmbPaymentType) == 3))
                                    {


                                        pnlPartyName.Visible = true;
                                        if (ID == 0)
                                            txtPartyName.Text = "";
                                        txtPartyName.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        DisplayMessage("B- Short cut key not working..");
                                        dgBill.CurrentCell.Value = ""; return;
                                    }
                                }
                                break;
                            }
                        case "N":
                            {
                                if (btnSave.Visible)
                                {
                                    btnSave_Click(btnSave, new EventArgs());
                                    return;
                                }
                                break;
                            }
                        default:
                            {
                                SearchBarcode(dgBill.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);
                                dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0;
                                break;
                            }

                    }

                    //old code
                    //SearchBarcode(dgBill.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);

                    if (ItemNo.Length == 0 || BarcodeNo.Length == 0)
                    {
                        dgBill.CurrentCell.Value = null;
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = "0";
                        //DisplayMessage("Barcode Not Found");
                        OMMessageBox.Show("Barcode Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.ItemName, dgBill });
                    }
                    else
                    {
                        if (ItemNo.Length > 1)
                        {
                            ItemType = 3;
                            FillItemList(0, ItemType);//FillItemList();
                        }
                        else
                        {
                            int rwindex = 0;
                            if (ItemExist(ItemNo[0], out rwindex) == true)
                            {
                                dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                                dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                                dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                                dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                                dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                            }
                            else
                            {
                                dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgBill.CurrentCell.Value;
                                Desc_MoveNext(ItemNo[0], BarcodeNo[0]);
                            }
                        }
                    }
                    //BindGrid();
                    //CalculateTotal();
                }

                //from key_down
                //ItemType = 1;
                //FillItemList();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SearchBarcode(String strBarcode, out long[] ItemNo, out long[] BarcodeNo)
        {
            string sql = "SELECT   Distinct  MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                    " where (MStockBarcode.Barcode = '" + strBarcode + "' or mItemMaster.ShortCode = '" + strBarcode + "') And mItemMaster.IsActive ='true'  AND (MRateSetting.IsActive='true') and (MStockItems.FkStockGroupTypeNo<>3) ";//and and mItemMaster.CompanyNo="+PurCompNo+"";//"Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";//AND mItemMaster.CompanyNo=" + DBGetVal.FirmNo + "

            DataTable dt = ObjFunction.GetDataView(sql).Table;
            BarcodeNo = new long[dt.Rows.Count];
            ItemNo = new long[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BarcodeNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                    ItemNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[1].ToString());
                }
            }
            else
            {
                //ItemNo[0] = 0;
                //BarcodeNo[0] = 0;
            }
        }

        private void Desc_MoveNext(long ItemNo, long BarcodeNo)
        {
            try
            {
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = ItemNo;
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;

                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL," + ItemNo + ",NULL,NULL,NULL,NULL,NULL) Where IsActive='true' ").Table;//where ItemNo = " + ItemNo
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
                //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = ObjQry.ReturnString("Select ItemName from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo = " + ItemNo,CommonFunctions.ConStr);

                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();

                if (StopOnQty == true)
                {
                    if (dgBill[2, dgBill.CurrentCell.RowIndex].Value == null)
                    {
                        dgBill.CurrentCell = dgBill[2, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }
                    else
                        Qty_MoveNext();
                }
                else
                {
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value == null)
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                    Qty_MoveNext();
                }

                //BindGrid();
                //CalculateTotal();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Qty_MoveNext()
        {
            try
            {
                rowQtyIndex = dgBill.CurrentCell.RowIndex;

                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { rowQtyIndex, 3, dgBill });

                UOM_Start();


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void UOM_Start()
        {
            try
            {
                int row = 0;
                if (dgBill.CurrentCell.RowIndex == 0)
                    row = dgBill.CurrentCell.RowIndex;
                else
                    row = dgBill.CurrentCell.RowIndex;
                dgBill.CurrentCell = dgBill[3, row];


                FillUOMList(row);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void UOM_MoveNext()
        {
            try
            {
                int Row = dgBill.CurrentCell.RowIndex;

                if (dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value != null &&
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value.ToString() != lstUOM.SelectedValue.ToString())
                {
                    dgBill.CurrentRow.Cells[ColIndex.Rate].Value = "0.00";//lstRate.Text;
                    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0;//lstRate.SelectedValue;
                }

                dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text;
                dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOM.SelectedValue);
                pnlUOM.Visible = false;

                Rate_Start();
                //CalculateTotal();//temp
                //CalculateGridValues(Row);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Rate_Start()
        {
            try
            {

                string str;
                //CalculateGridValues();
                int RowIndex = dgBill.CurrentCell.RowIndex;
                long ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);
                long BarcodeNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value);
                long UOMNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.UOMNo].Value);
                double Qty = Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value);

                if (dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value == null ||
                Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0)
                {
                    ObjFunction.FillList(lstRate, "pksrno", cmbRateType);
                    if (ItemType == 2)
                    {
                        str = "select pksrno," + cmbRateType +
                            " from GetItemRateAll(" + ItemNo + "," + BarcodeNo + "," + UOMNo + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }
                    else
                    {
                        str = "select pksrno," + cmbRateType +
                            " from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }
                    str = str.Replace("PurRate", "Isnull((Select Rate FRom Tstock Where PKStockTrnNo in (Select Max(PKStockTrnNo) FRom Tstock,TVoucherEntry Where TVoucherEntry.PKVoucherNo=TStock.FKVoucherNo AND TVoucherEntry.VoucherTypeCode=" + VchType.Purchase + " AND FKRateSettingNo in(PkSrNo))),0) AS PurRate");
                    ObjFunction.FillList(lstRate, str);

                    if (lstRate.Items.Count == 1)
                    {
                        lstRate.SelectedIndex = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = lstRate.Text;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                        dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                        dgBill.Focus();
                        //BindGrid(dgBill.CurrentRow.Index);

                        if (StopOnRate == false)
                        {
                            Rate_MoveNext();
                        }
                        else
                        {
                            BindGrid(dgBill.CurrentRow.Index);
                        }
                    }
                    else if (lstRate.Items.Count > 1)
                    {
                        if (flagParking == true)
                        {
                            lstRate.Text = dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value.ToString();
                            dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                            dgBill.Focus();
                            if (StopOnRate == false)
                                Rate_MoveNext();
                            else
                                BindGrid(dgBill.CurrentRow.Index);
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                            dgBill.Focus();
                            CalculateTotal();
                            pnlRate.Visible = true;
                            lstRate.Focus();
                        }
                    }
                    else
                    {
                        //error invalid Qty or UOM
                    }
                }
                else
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                    dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                    dgBill.Focus();
                    //BindGrid(dgBill.CurrentRow.Index);

                    if (StopOnRate == false)
                    {
                        Rate_MoveNext();
                    }
                    else
                    {
                        BindGrid(dgBill.CurrentRow.Index);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Rate_MoveNext()
        {
            try
            {
                if (dgBill.CurrentCell.Value != null)
                {
                    if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == true)
                    {
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value).ToString("0.00");
                        //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value) * Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[2].Value)) / Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MKTQuantity].Value);
                        //dgBill.CurrentCell.ReadOnly = true;
                        BindGrid(dgBill.CurrentCell.RowIndex);

                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                        dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                        dgBill.Focus();

                        if (ItemType == 1 && Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnGrid)) == false)
                        {
                            Desc_Start();
                        }

                        //CalculateTotal();
                    }
                    else
                    {
                        dgBill.CurrentCell.ErrorText = "Please Enter valid rate...";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Disc_MoveNext()
        {
            //Rate_MoveNext();
            CalculateTotal();
        }

        private void delete_row()
        {
            if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null)
            {
                if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                    if (PKStockTrnNo != 0)
                    {
                        DeleteDtls(1, PKStockTrnNo);
                    }

                    if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                    {
                        dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        dgBill.Rows.Add();
                    }
                    else
                        dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                    //dtBillCollect.RemoveAt(dgBill.CurrentCell.RowIndex);

                    CalculateTotal();

                    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                }
            }
        }

        private void delete_rowReward()
        {
            try
            {
                bool flag;
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null)
                {

                    long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                    if (PKStockTrnNo != 0)
                    {
                        DeleteDtls(1, PKStockTrnNo);
                        DataTable dtB = dtBillCollect[dgBill.CurrentCell.RowIndex];
                        for (int row = 0; row < dtB.Rows.Count; row++)
                        {
                            dtB.Rows[0][2] = "0";
                            dtB.AcceptChanges();
                        }

                        flag = false;
                        for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                        {
                            if (dgBill.CurrentCell.RowIndex != i)
                            {
                                if (Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkVoucherNo].Value) == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value))
                                { flag = true; break; }
                            }
                        }
                        if (flag == false)
                            DeleteDtls(2, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkVoucherNo].Value));





                        DataTable dt = dtBillCollect[dgBill.CurrentCell.RowIndex];
                        for (int row = 0; row < dt.Rows.Count; row++)
                        {
                            if (Convert.ToInt64(dt.Rows[row].ItemArray[4].ToString()) != 0 && Convert.ToDouble(dt.Rows[row].ItemArray[2].ToString()) == 0)
                            {
                                DeleteDtls(4, Convert.ToInt64(dt.Rows[row].ItemArray[4].ToString()));
                            }
                        }
                    }

                    if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                    {
                        dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        dgBill.Rows.Add();
                    }
                    else
                        dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                    dtBillCollect.RemoveAt(dgBill.CurrentCell.RowIndex);

                    CalculateTotal();

                    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Spaceflag == false) { Spaceflag = true; return; }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                {
                    if (dgBill.CurrentCell.Value != null && Convert.ToString(dgBill.CurrentCell.Value) != "")
                    {
                        Desc_Start();
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == true)
                        {
                            if (Convert.ToDouble(dgBill.CurrentCell.Value.ToString()) == 0) dgBill.CurrentCell.Value = "1";
                        }
                        else
                            dgBill.CurrentCell.Value = "1";
                    }
                    Qty_MoveNext();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
                {
                    UOM_Start();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                {
                    Rate_MoveNext();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {

                    delete_row();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.SrNo)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentCell.RowIndex];
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value == null)
                        {
                            dgBill.CurrentCell.Value = "";
                            Desc_Start();
                        }
                        else if (Convert.ToInt64(dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value) == 0)
                        {
                            dgBill.CurrentCell.Value = "";
                            Desc_Start();
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentCell.Value == null) dgBill.CurrentCell.Value = "1";
                        Qty_MoveNext();
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value != null && dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value.ToString() != "")
                        {
                            UOM_Start();
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                    {
                        e.SuppressKeyPress = true;
                        Rate_MoveNext();
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    btnSave.Focus();
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (dgBill.CurrentCell.Value != null)
                        dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                    else
                    {
                        if (dgBill.CurrentCell.RowIndex == 0)
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                        else
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex - 1];
                    }
                }
                else if (e.KeyCode == Keys.F7)
                {
                    if (btnSave.Visible == true)
                    {
                        //DisplayStockGodown();
                        if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value != null)
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                        else
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex - 1];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row, col;
                if (dgBill.CurrentCell != null)
                { col = dgBill.CurrentCell.ColumnIndex; row = dgBill.CurrentCell.RowIndex; }
                else { col = e.ColumnIndex; row = e.RowIndex; }
                if (dgBill.Rows.Count > 0)
                    dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                if (col == ColIndex.Quantity && row >= 0)
                {
                    dgBill.CurrentCell.ErrorText = "";
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (dgBill.CurrentCell.Value.ToString() != "" && dgBill.CurrentCell.Value.ToString() != "0")
                        {
                            if (ObjFunction.CheckNumeric(dgBill.CurrentCell.Value.ToString()) == true)
                            {
                                int rowIndex = dgBill.CurrentCell.RowIndex;
                                if (dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex].Value == null || Convert.ToString(dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex].Value) == "")
                                    dgBill[ColIndex.Amount, dgBill.CurrentCell.RowIndex].Value = "0.00";
                                else
                                    CalculateTotal();
                                dgBill.Focus();

                                dgBill.CurrentCell = dgBill[2, row];
                            }
                        }
                    }
                }
                else if (col == ColIndex.Rate && row >= 0)
                {
                    dgBill.CurrentCell.ErrorText = "";
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (dgBill.CurrentCell.Value.ToString() != "" && dgBill.CurrentCell.Value.ToString() != "0")
                        {
                            if (ObjFunction.CheckNumeric(dgBill.CurrentCell.Value.ToString()) == true)
                            {
                                dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                                dgBill.Rows[dgBill.CurrentCell.RowIndex].Selected = true;
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

        private void dgBill_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = (TextBox)e.Control;
            txt.KeyDown += new KeyEventHandler(txtSpace_KeyDown);
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
                //txt1.TextChanged -= new EventHandler(txtQuantity_TextChanged);
            }
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
            {
                TextBox txtrate = (TextBox)e.Control;
                txtrate.TextChanged += new EventHandler(txtRate_TextChanged);
            }

        }

        public void txtSpace_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                Spaceflag = false;
                if (dgBill.CurrentCell.RowIndex == 0)
                {
                    if (dgBill.CurrentCell.ColumnIndex != 0)
                        dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex - 1, dgBill.CurrentCell.RowIndex];
                }
                else
                {

                    if (dgBill.CurrentCell.ColumnIndex == 1)
                        dgBill.CurrentCell = dgBill[4, dgBill.CurrentCell.RowIndex - 1];
                    else if (dgBill.CurrentCell.ColumnIndex != 0)
                        dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex - 1, dgBill.CurrentCell.RowIndex];
                }
            }
            TextBox txt = (TextBox)sender;
            txt.KeyDown -= new KeyEventHandler(txtSpace_KeyDown);
        }

        public void txtRate_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMasked((TextBox)sender, 2);
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 5, OMFunctions.MaskedType.NotNegative);
            }
        }

        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMasked((TextBox)sender, 2);
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {
                //if (((TextBox)sender).Text.Length > 6) 
                //    ((TextBox)sender).Text = "0";
                ObjFunction.SetMasked((TextBox)sender, 2, 6, OMFunctions.MaskedType.NotNegative);
            }
        }


        private void BindGrid(int RowIndex)
        {
            try
            {
                long ItemNo, RateSettingNo, BarcodeNo;
                double StockConFactor;
                DataTable dtLedger = new DataTable();

                RateSettingNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value);
                ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);
                BarcodeNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value);

                if (DiscFlag == false)
                {
                    txtOtherDisc.Text = Format.DoubleFloating;// "0.00";
                    txtSchemeDisc.Text = Format.DoubleFloating;
                }
                else DiscFlag = false;

                DataTable dt = ObjFunction.GetDataView("SELECT r.FkBcdSrNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                    " t.PkSrNo,t.Percentage FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + "," + TaxTypeNo + ",NULL) As t " +
                    " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;


                if (dt.Rows.Count > 0)
                {
                    if (BarcodeNo == 0)
                    {
                        BarcodeNo = Convert.ToInt64(dt.Rows[0][0].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;
                        dgBill.Rows[RowIndex].Cells[ColIndex.Barcode].Value = ObjQry.ReturnString("Select BarCode From MStockBarCode Where PkStockBarcodeNo=" + BarcodeNo + "", CommonFunctions.ConStr);
                    }

                    dgBill.Rows[RowIndex].Cells[ColIndex.MKTQuantity].Value = Convert.ToInt64(dt.Rows[0][1].ToString());

                    StockConFactor = Convert.ToDouble(dt.Rows[0][2].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.StockFactor].Value = StockConFactor;


                    if (dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkVoucherNo].Value = 0;

                    //CheckItemLevelDiscountDetails(RowIndex);
                    //if (ItemExist(Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value), Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value),out tempRowIndex) == true)
                    //{
                    //    pnlItemName.Visible = false;
                    //    dgBill.Rows[tempRowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[tempRowIndex].Cells[ColIndex.Quantity].Value) + 1;
                    //    for (int i = 1; i < dgBill.Columns.Count; i++)
                    //    {
                    //        dgBill.Rows[dgBill.CurrentRow.Index].Cells[i].Value = "";
                    //    }
                    //    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentRow.Index];
                    //}

                    //else 
                    if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1 && dgBill.CurrentCell.ColumnIndex == 4)
                    {
                        dgBill.Rows.Add();
                    }

                    CalculateTotal();

                    DataTable dtStk;
                    DataRow dr = null;
                    if (dtBillCollect.Count == RowIndex)
                    {
                        dtStk = ObjFunction.GetDataView("Exec GetStockGodownDetails 0,0").Table;
                        for (int row = 0; row < dtStk.Rows.Count; row++)
                        {
                            if (Convert.ToInt64(dtStk.Rows[row].ItemArray[0].ToString()) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_OutwardLocation)))
                            {
                                dr = dtStk.Rows[row];
                                dr[2] = dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value;
                                dr[3] = dgBill.Rows[RowIndex].Cells[ColIndex.ActualQty].Value;
                            }
                        }
                        dtBillCollect.Add(dtStk);
                    }
                    else
                    {
                        if (dtBillCollect.Count > RowIndex)
                        {
                            dtStk = dtBillCollect[RowIndex];
                            for (int row = 0; row < dtStk.Rows.Count; row++)
                            {
                                if (Convert.ToInt64(dtStk.Rows[row].ItemArray[0].ToString()) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_OutwardLocation)))
                                {
                                    dr = dtStk.Rows[row];
                                    dr[2] = dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value;
                                    dr[3] = dgBill.Rows[RowIndex].Cells[ColIndex.ActualQty].Value;
                                }
                                else
                                {
                                    dr = dtStk.Rows[row];
                                    dr[2] = 0;
                                    dr[3] = 0;
                                }
                            }
                            dtBillCollect.RemoveAt(RowIndex);
                            dtBillCollect.Insert(RowIndex, dtStk);
                        }
                    }



                }
                else
                {

                    DisplayMessage("Items Tax Details Not Found.....");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Sales.RequestSalesNo = 0;
            //Form NewF = new Sales();
            //this.Close();
            //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            pnlSearch.Visible = true;
            txtSearch.Text = ""; txtSearch.Enabled = true;
            txtSearch.Focus();
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnBillCancel.Enabled = false;
            rbInvNo.Checked = true;
            rbType_CheckedChanged(rbInvNo, null);
            dgPartySearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

            //new code
            DataRow[] dr = dtSearch.Select("GRNNo=" + ID);
            if (dr.Length > 0)
            {
                cntRow = dtSearch.Rows.IndexOf(dr[0]);
            }
            else
            {
                cntRow = dtSearch.Rows.Count - 1;
            }

            //old code
            //for (int i = 0; i < dtSearch.Rows.Count; i++)
            //{
            //    if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID)
            //    {
            //        cntRow = i;
            //        break;
            //    }
            //}
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


            else if (e.KeyCode == Keys.F1)
            {
                e.SuppressKeyPress = true;
                if (pnlFooterInfo.Visible == false)
                {
                    pnlFooterInfo.Dock = DockStyle.Bottom;
                    //pnlFooterInfo.Height = 30;
                    pnlFooterInfo.BorderStyle = BorderStyle.None;
                    pnlFooterInfo.BringToFront();
                    pnlFooterInfo.Visible = true;
                }
                else
                {
                    pnlFooterInfo.Visible = false;
                }
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (dgBill.Focused == true)
                {
                    if (dgBill.Rows.Count > 0)
                    {
                        if (dgBill.CurrentCell.ColumnIndex == 2)
                        {
                            dgBill.CurrentCell.ReadOnly = false;
                            //AddRows = false;
                            //FlagRate = false;
                            //defaultQty = true;
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                //btnExit_Click(sender, e);
            }


            if (e.KeyCode == Keys.P && e.Control)
            {
                if (ID != 0)
                {
                    PrintBill();
                }
            }
            else if (e.KeyCode == Keys.F5)
            {
                if (ParkBillNo != 0)
                {
                    if (ObjTrans.Execute("Update TOtherParkingBill set IsCancel='true' where ParkingBillNo=" + ParkBillNo + "", CommonFunctions.ConStr) == true)
                        ParkBillNo = 0;
                }
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }

            }

            else if (e.KeyCode == Keys.O && e.Control)
            {
                if (DBGetVal.IsAdmin == true)
                {
                    if (btnNew.Visible == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_RateTypeAskPassword)) == true)
                        {
                            txtRateTypePassword.Enabled = true;
                            pnlRateType.Visible = true;
                            txtRateTypePassword.Text = "";
                            txtRateTypePassword.Focus();
                        }
                    }
                }
            }

            else if (e.KeyCode == Keys.Y && e.Control)
            {
                //if (btnSave.Visible == true)
                //    ApplyNoDiscount();
            }
        }
        #endregion

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
                        tGrnDetails = new TGRNDetails();
                        tGrnDetails.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTGrn.DeleteTGRNDetails(tGrnDetails);
                    }
                }
                dtDelete.Rows.Clear();
            }
        }


        #endregion

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (BillingMode == 0)
                {
                    BillingMode = 1;
                    //VoucherType = VchType.TempSales;
                    lblStatus.ForeColor = Color.Green;
                }
                else
                {
                    BillingMode = 0;
                    //VoucherType = VchType.Sales;
                    lblStatus.ForeColor = Color.Red;
                }

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);


                InitDelTable();
                txtInvNo.Enabled = false;
                InitControls();
                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TOtherVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND IsVoucherLock='false' Order by VoucherDate, VoucherUserNo").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    FillControls();
                    SetNavigation();
                }

                setDisplay(true);
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                btnInsScheme.Enabled = true;
                btnInsScheme.Visible = false;
                cmbGodown.SelectedIndex = 0;
                btnSearch.Visible = false;
                //btnPrint.Visible = false;

                //btnCCSave.Visible = true;

                dgBill.Enabled = true;
                ObjFunction.FillComb(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and (IsActive='true') order by LedgerName");
                InitControls();
                if (cmbPartyName.Focused == true) cmbPartyName.DroppedDown = true;
                btnBillCancel.Visible = false;




                PartyNo = 0;
                ParkBillNo = 0;

                txtSchemeDisc.Enabled = false;
                txtSchemeDisc.Text = "0.00";

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjQry.ReturnLong("Select Count(*) from TGRNInvoice where FkGRNNo=" + ID + "", CommonFunctions.ConStr) > 0)
                {
                    btnUpdate.Visible = false; btnBillCancel.Visible = false;
                    OMMessageBox.Show("Already this GRN generated invoice", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtInvNo.Enabled = false;
                btnInsScheme.Enabled = true;

                btnSearch.Visible = false;
                //  btnPrint.Visible = false;
                dgBill.Enabled = true;
                dgBill.Focus();
                btnBillCancel.Visible = false;

                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                if (ID != 0)
                {
                    //cmbPaymentType.Enabled = false;
                    //dgPayType.Enabled = false;
                }
                txtSchemeDisc.Enabled = false;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (ObjQry.ReturnLong("Select Count(*) from TGRNInvoice where FkGRNNo=" + ID + "", CommonFunctions.ConStr) > 0)
            {
                btnUpdate.Visible = false; btnBillCancel.Visible = false;
                OMMessageBox.Show("Already this GRN generated invoice", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }
            ObjFunction.LockButtons(true, this.Controls);
            btnBillCancel.Visible = true;

            btnSearch.Visible = true;
            //btnPrint.Visible = true;

            NavigationDisplay(5);
            //if (ParkBillNo != 0)
            //  ObjTrans.Execute("Update TOtherParkingBill set IsCancel='true' where ParkingBillNo=" + ParkBillNo + "", CommonFunctions.ConStr);
            ObjFunction.LockControls(false, this.Controls);
            btnInsScheme.Enabled = false;

            DisplayList(false);
            dgBill.Enabled = false;
            btnNew.Focus();
            ParkBillNo = 0;

        }

        public void DisplayList(bool flag)
        {
            pnlItemName.Visible = flag;
            pnlGroup1.Visible = flag;
            pnlGroup2.Visible = flag;
            pnlUOM.Visible = flag;
            pnlRate.Visible = flag;
        }

        private void btnBillCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (OMMessageBox.Show("Are you sure you want to cancel this GRN Entry ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tGrn = new TGRN();
                    tGrn.GRNNo = ID;
                    if (dbTGrn.DeleteTGRN(tGrn) == true)
                    {
                        OMMessageBox.Show("GRN Details deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("GRN Details deleted Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                long tempNo;
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    tempNo = ObjQry.ReturnLong("Select GRNNo From TGRN Where GRNUserNo=" + txtSearch.Text + " ", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        SetNavigation();
                        FillControls();

                        pnlSearch.Visible = false;
                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnBillCancel.Enabled = true;
                        SearchVisible(false);
                    }
                    else
                    {
                        pnlSearch.Visible = false;
                        DisplayMessage("GRN Not Found");
                        Application.DoEvents();
                        pnlSearch.Visible = true;
                        cmbPartyNameSearch.SelectedIndex = 0;
                        rbInvNo.Focus();
                        txtSearch.Focus();
                        txtSearch.Text = "";
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlSearch.Visible = false;
                    pnlPartySearch.Visible = false;
                    btnCancelSearch_Click(sender, new EventArgs());
                    btnNew.Focus();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    e.SuppressKeyPress = true;
                    rbPartyName.Checked = true;
                    rbType_CheckedChanged(rbPartyName, null);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnBillCancel.Enabled = true;
        }

        private void ChkQty_CheckedChanged(object sender, EventArgs e)
        {
            //StopOnQty = ChkQty.Checked;
        }

        private void ChkRate_CheckedChanged(object sender, EventArgs e)
        {
            //StopOnRate = ChkRate.Checked;
        }

        #region Without Barcode Methods

        private void FillItemList(int qNo, int iType)
        {
            try
            {
                switch (iType)
                {
                    case 1:
                        FillItemList(qNo);
                        break;
                    case 2:
                        break;
                    case 3:
                        //string  ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName,MStockItems.ItemNameLang, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                        //         " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        //         " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                        //         " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        //         " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        //         " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        //         " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        //         " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        //         "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') And mItemMaster.IsActive='true'" +
                        //         " ORDER BY mItemMaster.ItemName";
                        DataTable dtBarCodeItemNo = ObjFunction.GetDataView("Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "'").Table;
                        string ItemList = "";
                        for (int i = 0; i < dtBarCodeItemNo.Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                ItemList += " Union all ";
                            }

                            ItemList += " SELECT mItemMaster.ItemNo, mItemMaster.ItemName,MStockItems.ItemNameLang, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                                " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TOtherStock  INNER JOIN TStockGodown ON TOtherStock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TOtherStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TOtherStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TOtherStock.PkStockTrnNo FROM TOtherVoucherEntry INNER JOIN  TOtherStock ON TOtherVoucherEntry.PkVoucherNo = TOtherStock.FKVoucherNo WHERE (TOtherVoucherEntry.IsCancel = 'True') AND (TOtherStock.ItemNo = mItemMaster.ItemNo))) AS Stock , '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                                " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,0 As PurRate " +
                                " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                                " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                                " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                                " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                                " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where  " +
                                " mItemMaster.IsActive='true'";
                        }
                        ItemList += " ORDER BY mItemMaster.ItemName";

                        DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                        if (dtItemList.Rows.Count > 0)
                        {
                            dgItemList.DataSource = dtItemList.DefaultView;
                            pnlItemName.Visible = true;
                            dgItemList.CurrentCell = dgItemList[1, 0];
                            dgItemList.Focus();
                        }
                        else
                        {
                            //DisplayMessage("Items Not Found......");
                            OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                        }
                        break;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillItemList(int qNo)
        {
            try
            {
                if (qNo == 0)
                {
                    qNo = iItemNameStartIndex;
                }

                string ItemList = strItemQuery[qNo - 1];

                ItemList = ItemList.Replace("@cmbRateType@", "" + cmbRateType);

                switch (qNo)
                {
                    case 1:
                        break;
                    case 2:
                        switch (strItemQuery.Length)
                        {
                            case 2:
                                ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value));
                                ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL"));
                                break;
                            case 3:
                                ItemList = ItemList.Replace("@Param2@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : Param2Value));
                                ItemList = ItemList.Replace("@Param2NULL@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : "NULL"));
                                break;
                        }
                        break;
                    case 3:
                        ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value));
                        ItemList = ItemList.Replace("@Param2@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : Param2Value));
                        ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL"));
                        ItemList = ItemList.Replace("@Param2NULL@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : "NULL"));
                        break;
                }
                ItemList = ItemList.Replace("@CompNo@", "MStockItems.CompanyNo");
                ItemList = ItemList.Replace("MRateSetting.PurRate AS SaleRate", "Isnull((Select Rate FRom Tstock Where PKStockTrnNo in (Select Max(PKStockTrnNo) FRom Tstock,TVoucherEntry Where TVoucherEntry.PKVoucherNo=TStock.FKVoucherNo AND TVoucherEntry.VoucherTypeCode=" + VchType.Purchase + " AND FKRateSettingNo in(MRateSetting.PkSrNo))),0) AS SaleRate");
                ItemList = ItemList.Replace("@GodownNo@", "" + ObjFunction.GetComboValue(cmbGodown) + "");
                switch (strItemQuery.Length - qNo)
                {
                    case 0:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            //ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MStockItems.LangShortDesc<>'') then mItemMaster.LangShortDesc else mItemMaster.LangFullDesc end AS ItemNameLang,");
                            DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                            strItemQuery_last[qNo - 1] = ItemList;
                            if (dtItemList.Rows.Count > 0)
                            {
                                dgItemList.DataSource = dtItemList.DefaultView;
                                pnlItemName.Visible = true;
                                dgItemList.CurrentCell = dgItemList[1, 0];
                                dgItemList.Focus();
                            }
                            else
                            {
                                //DisplayMessage("Items Not Found......");
                                OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            pnlItemName.Visible = true;
                            dgItemList.CurrentCell = dgItemList[1, 0];
                            dgItemList.Focus();
                        }
                        break;
                    case 1:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                            //    ObjFunction.FillList(lstGroup1Lang, ItemList.Replace("StockGroupName from", "LanguageName from"));
                            ////ObjFunction.FillList(lstGroup1Lang, ItemList);

                            ObjFunction.FillList(lstGroup1, ItemList);
                            strItemQuery_last[qNo - 1] = ItemList;
                        }
                        pnlGroup1.Visible = true;
                        lstGroup1.Focus();
                        break;
                    case 2:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ObjFunction.FillList(lstGroup2, ItemList);
                            strItemQuery_last[qNo - 1] = ItemList;
                        }
                        pnlGroup2.Visible = true;
                        lstGroup2.Focus();
                        break;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillItemList()
        {
            try
            {
                string ItemList = "";
                if (ItemType == 3)
                {
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                        " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TOtherStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TOtherStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TOtherStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TOtherStock.PkStockTrnNo FROM TOtherVoucherEntry INNER JOIN  TOtherStock ON TOtherVoucherEntry.PkVoucherNo = TOtherStock.FKVoucherNo WHERE (TOtherVoucherEntry.IsCancel = 'True') AND (TOtherStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,0 As PurRate " +
                        " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        // " dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + ",NULL) AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                        // " dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + ",NULL) AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') And mItemMaster.IsActive='true'" +
                        " ORDER BY mItemMaster.ItemName";
                    DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                    if (dtItemList.Rows.Count > 0)
                    {
                        dgItemList.DataSource = dtItemList.DefaultView;
                        pnlItemName.Visible = true;
                        dgItemList.CurrentCell = dgItemList[1, 0];
                        dgItemList.Focus();
                    }
                    else
                    {
                        //DisplayMessage("Items Not Found......");
                        OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                    }
                }
                else if (ItemNameType == 1)
                {
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TOtherStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TOtherStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TOtherStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TOtherStock.PkStockTrnNo FROM TOtherVoucherEntry INNER JOIN  TOtherStock ON TOtherVoucherEntry.PkVoucherNo = TOtherStock.FKVoucherNo WHERE (TOtherVoucherEntry.IsCancel = 'True') AND (TOtherStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                            " FROM MStockItems INNER JOIN " +
                            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        //" dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                        //" dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                            " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo Where mItemMaster.ItemNo <> 1 And mItemMaster.IsActive='true'" +
                            " ORDER BY mItemMaster.ItemName";
                    DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                    if (dtItemList.Rows.Count > 0)
                    {
                        dgItemList.DataSource = dtItemList.DefaultView;
                        pnlItemName.Visible = true;
                        dgItemList.CurrentCell = dgItemList[1, 0];
                        dgItemList.Focus();
                    }
                    else
                    {
                        //DisplayMessage("Items Not Found......");
                        OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                    }

                }
                else if (ItemNameType == 2)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup1, ItemList);
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                    //lstGroup1.Location = new Point(dgBill.Left + dgBill.CurrentCell.ContentBounds.Left, dgBill.Top + dgBill.CurrentRow.Height + dgBill.CurrentCell.ContentBounds.Top + dgBill.ColumnHeadersHeight);
                }
                else if (ItemNameType == 3)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo1 from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup2, ItemList);
                    pnlGroup2.Visible = true;
                    lstGroup2.Focus();
                }
                else if (ItemNameType == 4)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup1, ItemList);
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                }
                dgBill.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstUOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                UOM_MoveNext();
            }
            else if (e.KeyChar == ' ')
            {
                dgBill.Focus();
                pnlUOM.Visible = false;
            }
        }

        private void lstGroup1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pnlGroup1.Visible = false;

                FillItemList(strItemQuery.Length);

                //if (ItemNameType == 2) 
                //{
                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                //            " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                //            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                //            " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                //            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                //            " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                //            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                //            //" dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + "," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                //            //" dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + "," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                //            " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo <> 1 " +
                //            " AND mItemMaster.GroupNo = " + Convert.ToInt64(lstGroup1.SelectedValue) + " " +
                //            " ORDER BY mItemMaster.ItemName";
                //    DataTable dtItemList = ObjFunction.GetDataView(ItemListStr).Table;
                //    if (dtItemList.Rows.Count > 0)
                //    {
                //        dgItemList.DataSource = dtItemList.DefaultView;
                //        pnlItemName.Visible = true;
                //        dgItemList.CurrentCell = dgItemList[1, 0];
                //        dgItemList.Focus();
                //    }
                //    else
                //    {
                //        DisplayMessage("Items Not Found......");
                //    }

                //}
                //else if (ItemNameType == 4)
                //{
                //    ItemListStr = "Select StockGroupNo,StockGroupName from MStockGroup " + 
                //         " where StockGroupNo in (select Distinct GroupNo1 from MStockItems " + 
                //         " where GroupNo=" + Convert.ToInt64(lstGroup1.SelectedValue) + ")";
                //    ObjFunction.FillList(lstGroup2, ItemListStr);
                //    pnlGroup1.Visible = false;
                //    pnlGroup2.Visible = true;
                //    lstGroup2.Focus();
                //}

            }
            else if (e.KeyChar == ' ')
            {
                dgBill.Focus();
                pnlGroup1.Visible = false;
            }
        }

        private void lstGroup2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                pnlGroup2.Visible = false;

                FillItemList(strItemQuery.Length - 1);

                //if (ItemNameType == 3)
                //{
                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                //        " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage 
                //        " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                //        " FROM MStockItems_V(NULL,NULL) as MStockItems INNER JOIN " + 
                //        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                //        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                //        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                //        //" dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                //        //" dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                //        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo <> 1 " +
                //        " AND mItemMaster.GroupNo1 = " + Convert.ToInt64(lstGroup2.SelectedValue) + " " +
                //        " ORDER BY mItemMaster.ItemName";                                                                                                             
                //    DataTable dtItemList = ObjFunction.GetDataView(ItemListStr).Table;
                //    if (dtItemList.Rows.Count > 0)
                //    {
                //        dgItemList.DataSource = dtItemList.DefaultView;
                //        pnlItemName.Visible = true;
                //        dgItemList.CurrentCell = dgItemList[1, 0];
                //        dgItemList.Focus();
                //    }
                //    else
                //    {
                //        DisplayMessage("Items Not Found......");
                //    }
                //}
                //else if (ItemNameType == 4)
                //{
                //    pnlGroup2.Visible = false;

                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                //            " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                //            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                //            " FROM MStockItems_V(NULL,NULL) AS MStockItems INNER JOIN " +
                //            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                //            " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                //            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                //            //" dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + "," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                //            //" dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + "," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                //            " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo <> 1 " +
                //            " AND mItemMaster.GroupNo1=" + Convert.ToInt64(lstGroup2.SelectedValue) +
                //            " AND mItemMaster.GroupNo=" + Convert.ToInt64(lstGroup1.SelectedValue) + " " +
                //            " ORDER BY mItemMaster.ItemName";
                //    DataTable dtItemList = ObjFunction.GetDataView(ItemListStr).Table;
                //    if (dtItemList.Rows.Count > 0)
                //    {
                //        dgItemList.DataSource = dtItemList.DefaultView;
                //        pnlItemName.Visible = true;
                //        dgItemList.CurrentCell = dgItemList[1, 0];
                //        dgItemList.Focus();
                //    }
                //    else
                //    {
                //        DisplayMessage("Items Not Found......");
                //    }

                //}
                //else if(ItemNameType==5)
                //{
                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + cmbRateType + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                //        " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                //        " mItemMaster.CompanyNo, s As RateSettingNo " +
                //        " FROM MStockItems INNER JOIN " +
                //        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                //        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                //        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                //        //" dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + "," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                //        //" dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + "," + Convert.ToInt64(lstGroup1.SelectedValue) + ") AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                //        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo <> 1 " +
                //        " AND mItemMaster.GroupNo1=" + Convert.ToInt64(lstGroup2.SelectedValue) +
                //        " AND mItemMaster.GroupNo=" + Convert.ToInt64(lstGroup1.SelectedValue) + " " +
                //        " ORDER BY mItemMaster.ItemName";

                //    DataTable dtItemList = ObjFunction.GetDataView(ItemListStr).Table;
                //    if (dtItemList.Rows.Count > 0)
                //    {
                //        dgItemList.DataSource = dtItemList.DefaultView;
                //        pnlItemName.Visible = true;
                //        dgItemList.CurrentCell = dgItemList[1, 0];
                //        dgItemList.Focus();
                //    }
                //    else
                //    {
                //        DisplayMessage("Items Not Found......");
                //    }
                //}
            }
            else if (e.KeyChar == ' ')
            {
                dgBill.Focus();
                pnlGroup2.Visible = false;
            }
        }

        private void lstRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                dgBill.CurrentRow.Cells[ColIndex.Rate].Value = lstRate.Text;
                dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;
                Rate_MoveNext();
                pnlRate.Visible = false;
            }
            else if (e.KeyChar == ' ')
            {
                dgBill.Focus();
                pnlRate.Visible = false;
            }
        }

        private void FillUOMList(int RowIndex)
        {
            try
            {
                ObjFunction.FillList(lstUOM, "UomNo", "UomName");

                if (ItemType == 2)
                    strUom = " Select * From GetUomList ('" + Convert.ToString(dgBill.Rows[RowIndex].Cells[ColIndex.Barcode].Value) + "',0," + Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";
                else
                    strUom = " Select * From GetUomList (''," + Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value) + "," + Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";

                ObjFunction.FillList(lstUOM, strUom);

                if (lstUOM.Items.Count == 0)
                {
                    dgBill.Focus();
                }
                else if (lstUOM.Items.Count == 1)
                {
                    //dgBill.Rows[RowIndex].Cells[3].Value = lstUOM.Text;
                    //dgBill.Rows[RowIndex].Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOM.SelectedValue);
                    lstUOM.SelectedIndex = 0;
                    //lstUOM_KeyPress(lstUOM, new KeyPressEventArgs((char)13));
                    //CalculateGridValues(RowIndex);
                    UOM_MoveNext();
                }
                else
                {
                    ////lstUOM.SelectedIndex = 0;
                    //lstUOM.SelectedValue = dgBill.Rows[RowIndex].Cells[ColIndex.UOMNo].Value;
                    ////lstUOM_KeyPress(lstUOM, new KeyPressEventArgs((char)13));
                    ////CalculateGridValues(RowIndex);
                    //UOM_MoveNext();
                    if (flagParking == true)
                    {
                        lstUOM.SelectedValue = dgBill.Rows[RowIndex].Cells[ColIndex.UOMNo].Value;
                        UOM_MoveNext();
                    }
                    else
                    {
                        CalculateTotal();
                        pnlUOM.Visible = true;
                        lstUOM.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        private void lst_VisibleChanged(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.Control)sender).Visible == true)
                dgBill.Enabled = false;
            else
            {
                dgBill.Enabled = true;
                dgBill.Focus();
            }
            if (((System.Windows.Forms.Control)sender).Name == "pnlItemName")
            {


            }
        }

        private void dgBill_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgBill.CurrentCell != null)
            {
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    dgBill.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                dgBill.Rows[dgBill.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clrColorRow;
                dgBill.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int Quantity = 2;
            public static int UOM = 3;
            public static int Rate = 4;
            public static int NetRate = 5;
            public static int NetAmt = 6;
            public static int Amount = 7;
            public static int Barcode = 8;
            public static int PkStockTrnNo = 9;
            public static int PkBarCodeNo = 10;
            public static int PkVoucherNo = 11;
            public static int ItemNo = 12;
            public static int UOMNo = 13;
            public static int PkRateSettingNo = 14;
            public static int StockFactor = 15;
            public static int ActualQty = 16;
            public static int MKTQuantity = 17;
            public static int StockCompanyNo = 18;

        }
        #endregion

        public long GetVoucherPK(string expression)
        {
            long strVal = 0;
            try
            {
                if (dtVchMainDetails.Rows.Count > 0)
                {
                    DataRow[] result = dtVchMainDetails.Select(expression);
                    strVal = Convert.ToInt64(result[0].ItemArray[0].ToString());
                }
            }
            catch (Exception e)
            {
                strVal = 0;
                e.ToString();
            }
            return strVal;
        }

        public double GetRatio(long CompNo)
        {
            double ValRatio = 0;
            try
            {
                for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                {
                    if (Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) == CompNo)
                    {
                        ValRatio = Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString());
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                ValRatio = 0;
                e.ToString();
            }
            return ValRatio;
        }

        #region Datagrid ItemList Methods

        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                int rwindex = 0;
                if (ItemExist(i, out rwindex) == true)
                {
                    pnlItemName.Visible = false;
                    dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                    dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                    dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                    dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                }
                else
                {
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                    dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[2].Value).ToString("0.00");//lstRate.Text;
                    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);//lstRate.SelectedValue;

                    pnlItemName.Visible = false;
                    Desc_MoveNext(i, 0);
                }
                //long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);

                //dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                //dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[2].Value).ToString("0.00");//lstRate.Text;
                //dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);//lstRate.SelectedValue;

                //pnlItemName.Visible = false;
                //Desc_MoveNext(i, 0);
            }
            else if (e.KeyCode == Keys.Space)
            {
                pnlItemName.Visible = false;
                if (strItemQuery.Length > 1)
                {
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                }
                else
                {
                    dgBill.Focus();
                }
            }

        }

        private void dgItemList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgItemList.CurrentCell != null)
            {
                for (int i = 0; i < dgItemList.Rows.Count; i++)
                {
                    dgItemList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                dgItemList.Rows[dgItemList.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clrColorRow;
                dgItemList.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
            }
        }
        #endregion

        #region Rate Type Realted Methods and Functions
        public void FillRateType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RateType");
            dt.Columns.Add("RateTypeName");
            DataRow dr = null;

            //dr = dt.NewRow();
            //dr[0] = "----Select----";
            //dr[1] = "0";
            //dt.Rows.Add(dr);

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
                dr[0] = "ASaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
                dr[0] = "BSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.CRateLabel);
                dr[0] = "CSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.DRateLabel);
                dr[0] = "DSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.ERateLabel);
                dr[0] = "ESaleRate";
                dt.Rows.Add(dr);
            }


            //SetRateType(Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType).ToString()));
        }




        #endregion

        public void PrintBill()
        {
            string[] ReportSession;

            ReportSession = new string[2];
            ReportSession[0] = ID.ToString();
            ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();

            CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                childForm = ObjFunction.GetReportObject("Reports.GetDC");
            else
                childForm = ObjFunction.LoadReportObject("GetDC.rpt", CommonFunctions.ReportPath);

            if (childForm != null)
            {
                DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                if (objRpt.PrintReport() == true)
                {
                    DisplayMessage("Delivery Challan Print Successfully!!!");
                }
                else
                {
                    DisplayMessage("Delivery Challan not Print !!!");
                }
            }
            else
            {
                DisplayMessage("Delivery Challan Report not exist !!!");
            }
        }

        public void PrintBillFirm(long pkvoucher)
        {
            //try
            //{
            //    double Amt = 0;
            //    DataTable dtCBill = ObjFunction.GetDataView("select FKVoucherNo From TOtherVoucherPosting where FkOtherVoucherNo=" + Convert.ToString(ID)).Table;
            //    for (int i = 0; i < dtCBill.Rows.Count; i++)
            //    {
            //        Amt = ObjQry.ReturnDouble("Select sum(Case When TStock.Rate <>0 Then (((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount) Else MRateSetting.MRP*TStock.Quantity END) FROM TStock INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
            //                            " Where TStock.FkVoucherNo=" + Convert.ToInt64(dtCBill.Rows[i].ItemArray[0].ToString()) + " AND (Case When TStock.Rate <>0 Then ((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount Else MRateSetting.MRP*TStock.Quantity END)>0 ", CommonFunctions.ConStr);
            //       // Amt += Convert.ToDouble(txtDiscRupees1.Text);
            //        string[] ReportSession;

            //        ReportSession = new string[16];
            //        ReportSession[0] = dtCBill.Rows[i].ItemArray[0].ToString();
            //        ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();
            //        ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
            //        ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
            //        ReportSession[4] = Amt.ToString("0.00");
            //        ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
            //        ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : cmbPaymentType.Text);
            //        ReportSession[7] = (txtTotalAnotherDisc.Text == "") ? "0" : txtTotalAnotherDisc.Text;
            //        ReportSession[8] = (txtTotalChrgs.Text == "") ? "0" : txtTotalChrgs.Text;
            //        ReportSession[9] = (rbEnglish.Checked == true) ? "1" : "2";
            //        ReportSession[10] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSavingBill)).ToString();
            //        ReportSession[11] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)).ToString();
            //        if (btnMixMode.Visible == true)
            //        {
            //            ReportSession[13] = "0";
            //        }
            //        else
            //        {
            //            ReportSession[13] = (MixModeVal == 1) ? "1" : "0";
            //        }

            //        ReportSession[14] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
            //        ReportSession[15] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff)) == true) ? "1" : "2";
            //        //ReportSession[13] = (btnMixMode.Visible == true && MixModeVal == 1) ? "0" : "1";

            //        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
            //        {
            //            //double OpBal = ObjQry.ReturnDouble("Select OpeningBalance From MLedger Where LedgerNo =" + ObjFunction.GetComboValue(cmbPartyName) + "", CommonFunctions.ConStr);
            //            double OpBal = ObjQry.ReturnDouble("Select case when signCode=1 then isnull(OpeningBalance,0) else isNull(OpeningBalance,0)*-1 end  From MLedger Where LedgerNo =" + ObjFunction.GetComboValue(cmbPartyName) + "", CommonFunctions.ConStr);
            //            //double AvlBal = OpBal + ObjQry.ReturnDouble("Select isNull(Sum(Amount),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=3 AND FKVoucherTrnNo not in(Select PKVoucherTrnNo From TVoucherDetails Where FKVoucherNo=" + ID + ") ", CommonFunctions.ConStr);
            //            double AvlBal = OpBal + ObjQry.ReturnDouble("Select isNull(Sum(Amount),0) FROM         TVoucherDetails INNER JOIN " +
            //                                                         " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
            //                                                       " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo where TVoucherRefDetails.LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef=3 AND FKVoucherTrnNo not in(Select PKVoucherTrnNo From TVoucherDetails Where FKVoucherNo=" + ID + ") and  TVoucherEntry.IsCancel = 'false' AND VouchertypeCode=15", CommonFunctions.ConStr);
            //            double RecBal = ObjQry.ReturnDouble("Select isNull(Sum(Amount),0) from TVoucherRefDetails where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and TypeOfRef in(2,5)", CommonFunctions.ConStr);
            //            RecBal += ObjQry.ReturnDouble("SELECT isnull(SUM(TVoucherDetails.Credit),0.00) FROM TVoucherDetails INNER JOIN   TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo WHERE (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartyName) + ")  AND VoucherTypeCode=12", CommonFunctions.ConStr);
            //            double TotalDues = 0;
            //            long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPaymentType) + "", CommonFunctions.ConStr);
            //            //if (ObjFunction.GetComboValue(cmbPaymentType) == 3)
            //            if (ControlUnder == 3)
            //            {
            //                if (BillFlag == false)
            //                    TotalDues = Convert.ToDouble((AvlBal - RecBal) + Convert.ToDouble(txtGrandTotal.Text));
            //                else
            //                    TotalDues = Convert.ToDouble((AvlBal - RecBal));
            //                if (TotalDues > 0)
            //                {
            //                    ReportSession[12] = "Total Dues:" + TotalDues.ToString("0.00");
            //                }
            //                else if (TotalDues <= 0)
            //                    ReportSession[12] = "Total Dues: 0";
            //            }
            //            else
            //            {
            //                TotalDues = AvlBal - RecBal;
            //                if (TotalDues > 0)
            //                    ReportSession[12] = "Total Dues:" + Convert.ToDouble(AvlBal - RecBal).ToString("0.00");
            //                else
            //                    ReportSession[12] = "Total Dues: 0";
            //            }
            //        }
            //        else
            //            ReportSession[12] = "0";

            //        CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
            //        childForm = null;
            //        if (rbEnglish.Checked == true)
            //        {
            //            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
            //            {
            //                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
            //                    childForm = ObjFunction.GetReportObject("Reports.GetBillMRP");
            //                else
            //                    childForm = ObjFunction.GetReportObject("Reports.GetBill");
            //            }
            //            else
            //            {
            //                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
            //                    childForm = ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath);
            //                else
            //                    childForm = ObjFunction.LoadReportObject("GetBill.rpt", CommonFunctions.ReportPath);
            //            }
            //        }
            //        else if (rbMarathi.Checked == true)
            //        {
            //            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
            //            {
            //                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
            //                    childForm = ObjFunction.GetReportObject("Reports.GetBillMarMRP");
            //                else
            //                    childForm = ObjFunction.GetReportObject("Reports.GetBillMar");
            //            }
            //            else
            //            {
            //                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
            //                    childForm = ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath);
            //                else
            //                    childForm = ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath);
            //            }
            //        }
            //        if (childForm != null)
            //        {
            //            DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
            //            if (objRpt.PrintReport() == true)
            //            {
            //                DisplayMessage("Bill Print Successfully!!!");
            //            }
            //            else
            //            {
            //                DisplayMessage("Bill not Print !!!");
            //            }
            //        }
            //        else
            //        {
            //            DisplayMessage("Bill Report not exist !!!");
            //        }
            //    }
            //}
            //catch (Exception exc)
            //{
            //    ObjFunction.ExceptionDisplay(exc.Message);
            //}
        }

        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true) cmbPartyName.Focus();

                //else dgBill.Focus();
                // cmbPartyName.Focus();
                txtRefNo.Focus();

                e.SuppressKeyPress = true;
            }
        }

        private void cmbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (ObjFunction.GetComboValue(cmbPartyName) == 0)
                    {
                        DisplayMessage("Select Party Name");
                        cmbPartyName.Focus();
                        //cmbPartyName.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC));
                    }
                    else
                    {
                        long Count = ObjQry.ReturnLong("Select Count(*) from TGRN where RefNo='" + txtRefNo.Text.Trim() + "' and LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " and GRNDate='" + dtpBillDate.Text + "' and GRNNo!=" + ID + "", CommonFunctions.ConStr);
                        if (Count > 0)
                        {
                            OMMessageBox.Show("This RefNo is already exist ....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            cmbPartyName.Focus();
                        }
                        else
                            cmbGodown.Focus();
                    }
                    e.SuppressKeyPress = true;
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbPartyName_Leave(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                pnlGroup1.Visible = false;
                FillItemList(1);
            }
        }



        private void cmbRateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RateTypeNo = cmbRateType.SelectedIndex + 1;
            ChangeBillRate();
        }

        private void ChangeBillRate()
        {
            try
            {
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value != null)
                    {
                        dgBill.Rows[i].Cells[ColIndex.Rate].Value = ObjQry.ReturnDouble("Select " + cmbRateType + " From MRateSetting Where PkSrNo=" + dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value + "", CommonFunctions.ConStr);
                    }
                }
                CalculateTotal();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region Rate Type Password related Methods

        private void txtRateTypePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnRateTypeOK_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pnlRateType.Visible = false;
                btnNew.Focus();
            }
        }

        private void btnRateTypeOK_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                string[,] arr = new string[5, 2];
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)) == true)
                {
                    arr[0, 0] = ObjFunction.GetAppSettings(AppSettings.ARatePassword);
                    arr[0, 1] = "ASaleRate";
                }

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)) == true)
                {
                    arr[1, 0] = ObjFunction.GetAppSettings(AppSettings.BRatePassword);
                    arr[1, 1] = "BSaleRate";
                }

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive)) == true)
                {
                    arr[2, 0] = ObjFunction.GetAppSettings(AppSettings.CRatePassword);
                    arr[2, 1] = "CSaleRate";
                }

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive)) == true)
                {
                    arr[3, 0] = ObjFunction.GetAppSettings(AppSettings.DRatePassword);
                    arr[3, 1] = "DSaleRate";
                }

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive)) == true)
                {
                    arr[4, 0] = ObjFunction.GetAppSettings(AppSettings.ERatePassword);
                    arr[4, 1] = "ESaleRate";
                }

                for (int i = 0; i < 5; i++)
                {
                    if (arr[i, 0] != null)
                    {
                        if (txtRateTypePassword.Text == arr[i, 0].ToString())
                        {
                            // cmbRateType.SelectedValue = arr[i, 1].ToString();
                            //cmbRateType_SelectedIndexChanged(sender, new EventArgs());
                            RateTypeNo = i + 1;
                            DBMSettings dbMSettings = new DBMSettings();
                            if (arr[i, 1].ToString() == "ASaleRate")
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateDBEffect)) == true)
                                    dbMSettings.AddAppSettings(AppSettings.P_RateType, "1");
                            }
                            else if (arr[i, 1].ToString() == "BSaleRate")
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateDBEffect)) == true)
                                    dbMSettings.AddAppSettings(AppSettings.P_RateType, "2");
                            }
                            else if (arr[i, 1].ToString() == "CSaleRate")
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateDBEffect)) == true)
                                    dbMSettings.AddAppSettings(AppSettings.P_RateType, "3");
                            }
                            else if (arr[i, 1].ToString() == "DSaleRate")
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateDBEffect)) == true)
                                    dbMSettings.AddAppSettings(AppSettings.P_RateType, "4");
                            }
                            else if (arr[i, 1].ToString() == "ESaleRate")
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateDBEffect)) == true)
                                    dbMSettings.AddAppSettings(AppSettings.P_RateType, "5");
                            }
                            dbMSettings.ExecuteNonQueryStatements();
                            ObjFunction.SetAppSettings();
                            FillControls();
                            flag = true;
                            break;
                        }
                    }
                }
                if (flag == false)
                {
                    OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    txtRateTypePassword.Focus();
                }
                else
                {
                    pnlRateType.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnRateTypeCancel_Click(object sender, EventArgs e)
        {
            txtRateTypePassword.Text = "";
            pnlRateType.Visible = false;
        }

        #endregion

        public bool IsSuperMode()
        {
            bool flag = false;
            long RTNo = RateTypeNo;// Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType));
            if (RTNo == 1)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateSuperMode));
            }
            else if (RTNo == 2)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateSuperMode));
            }
            else if (RTNo == 3)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateSuperMode));
            }
            else if (RTNo == 4)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateSuperMode));
            }
            else if (RTNo == 5)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateSuperMode));
            }
            return true;
        }

        #region Party Name Methods
        private void btnPartyOK_Click(object sender, EventArgs e)
        {
            pnlPartyName.Visible = false;
            btnSave_Click(btnSave, new EventArgs());
        }

        private void btnPartyCancel_Click(object sender, EventArgs e)
        {
            pnlPartyName.Visible = false;
            btnSave.Focus();
        }
        #endregion

        private void lblPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                PrintBill();
            }
        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            lblGrandTotal.Text = txtGrandTotal.Text;
        }

        private void dgBill_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.Quantity)
            {

            }             
        }

        private void btnShortcut_Click(object sender, EventArgs e)
        {
            if (pnlFooterInfo.Visible == false)
            {
                pnlFooterInfo.Dock = DockStyle.Bottom;
                //pnlFooterInfo.Height = 30;
                pnlFooterInfo.BorderStyle = BorderStyle.None;
                pnlFooterInfo.BringToFront();
                pnlFooterInfo.Visible = true;
            }
            else
            {
                pnlFooterInfo.Visible = false;
            }
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            if (BillSizeFlag == false)
            {
                dgBill.Height = dgBill.Height - (pnlTotalAmt.Height + 10);
                pnlTotalAmt.Location = new Point(dgBill.Height + 10, pnlTotalAmt.Location.Y);
                pnlTotalAmt.Location = new Point(dgBill.Width - pnlTotalAmt.Width + 10, dgBill.Location.Y + dgBill.Height + 10);
                pnlTotalAmt.Visible = true;
                BillSizeFlag = true;
            }
            else
            {
                BillSizeFlag = false;
                pnlTotalAmt.Visible = false;
                dgBill.Height = dgBill.Height + (pnlTotalAmt.Height + 10);
            }
        }

        private void btnTodaysSales_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;

                ReportSession = new string[5];
                ReportSession[0] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                ReportSession[1] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                ReportSession[2] = VchType.Sales.ToString();
                ReportSession[3] = "0";
                ReportSession[4] = DBGetVal.FirmNo.ToString();
                if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RPTSalesSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.RPTSalesSummary");
                    else
                        childForm = ObjFunction.LoadReportObject("RPTSalesSummary.rpt", CommonFunctions.ReportPath);

                    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    objRpt.PrintReport();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSalesRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;

                ReportSession = new string[6];
                ReportSession[0] = VchType.Sales.ToString();
                ReportSession[1] = DBGetVal.FirmNo.ToString();
                ReportSession[2] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                ReportSession[3] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                ReportSession[4] = "1";
                ReportSession[5] = "0";

                if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.RPTSalesRegister");
                    else
                        childForm = ObjFunction.LoadReportObject("RPTSalesRegister.rpt", CommonFunctions.ReportPath);

                    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    objRpt.PrintReport();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                PartyNo = ObjFunction.GetComboValue(cmbPartyName);
                Form NewF = new Master.CustomerAE(-1);
                ObjFunction.OpenForm(NewF);

                if (((Master.CustomerAE)NewF).ShortID != 0)
                {
                    ObjFunction.FillCombo(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ")  order by LedgerName", "New Entry");
                    if (((Master.CustomerAE)NewF).ShortID > 0 && btnSave.Visible == true)
                        cmbPartyName.SelectedValue = ((Master.CustomerAE)NewF).ShortID;
                    else
                        cmbPartyName.SelectedValue = PartyNo;
                    cmbPartyName.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool ItemExist(long ItNo, out int rowIndex)
        {
            rowIndex = -1;
            bool flag = false;
            try
            {
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;
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

        public bool ItemExist(long ItNo, long RateSettingNo, out int rowIndex)
        {
            rowIndex = -1;
            bool flag = false;
            try
            {
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) && RateSettingNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;
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



        private void cmbPartyName_SelectedValueChanged(object sender, EventArgs e)
        {

        }






        private void lstGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            //  lstGroup1Lang.SelectedIndex = lstGroup1.SelectedIndex;

        }

        private void lstGroup1Lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            //{
            //    if (lstGroup1.Items.Count > 0)
            //        lstGroup1.SelectedIndex = lstGroup1Lang.SelectedIndex;
            //}
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            rbType(true);

        }

        public void rbType(bool IsSetFocus)
        {
            try
            {
                rbInvNo.Enabled = true;
                rbPartyName.Enabled = true;
                if (rbInvNo.Checked == true)
                {
                    if (IsSetFocus)
                    {
                        lblLable.Visible = true;
                        lblLable.Text = "GRN No :";
                        txtSearch.Width = 162;
                        txtSearch.Location = new System.Drawing.Point(90, 39);
                        txtSearch.Visible = true;
                        cmbPartyNameSearch.Visible = false;
                        btnPartyName.Visible = false;
                        txtSearch.Focus();
                    }
                }
                else if (rbPartyName.Checked == true)
                {
                    if (IsSetFocus)
                    {
                        btnPartyName.Enabled = true;
                        cmbPartyNameSearch.Enabled = true;
                        btnPartyName.Visible = true;
                        lblLable.Visible = true;
                        lblLable.Text = "Party Name :";
                        cmbPartyNameSearch.Width = 250;
                        cmbPartyNameSearch.Location = new System.Drawing.Point(90, 39);
                        btnPartyName.Location = new System.Drawing.Point((90 + 250 + 5), 39);
                        cmbPartyNameSearch.Visible = true;
                        txtSearch.Visible = false;
                        cmbPartyNameSearch.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbPartyNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (pnlPartySearch.Visible == true)
                    {
                        pnlPartySearch.Visible = false;
                        pnlSearch.Visible = true;
                    }
                    else
                    {

                        pnlPartySearch.Visible = true;
                        int x = dgBill.GetCellDisplayRectangle(0, 0, true).X + 200;//(Screen.PrimaryScreen.WorkingArea.Width) / 2;
                        int y = dgBill.GetCellDisplayRectangle(0, 0, true).Y + 100;
                        //pnlPartySearch.SetBounds(x, y, dgPartySearch.Width + 10, dgPartySearch.Height + 10);
                        pnlPartySearch.Location = new Point(x, y);
                        string str = "SELECT    0 as [#], TGRN.GRNUserNo AS [GRN #], TGRN.GRNDate AS 'Date', TGRN.GRNAmount AS 'Amount'," +
                                     "TGRN.GRNNo FROM TGRN  WHERE (CompanyNo = " + DBGetVal.FirmNo + ")" +
                                     "And TGRN.LedgerNo=" + ObjFunction.GetComboValue(cmbPartyNameSearch) + " " +
                                     "Order By  TGRN.GRNUserNo desc,TGRN.GRNDate desc ";
                        dgPartySearch.DataSource = ObjFunction.GetDataView(str).Table.DefaultView;
                        dgPartySearch.Columns[0].Width = 50;
                        dgPartySearch.Columns[1].Width = 150;
                        dgPartySearch.Columns[2].Width = 80;
                        dgPartySearch.Columns[3].Width = 110;
                        dgPartySearch.Columns[3].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                        dgPartySearch.Columns[3].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                        dgPartySearch.Columns[4].Visible = false;
                        if (dgPartySearch.RowCount > 0)
                        {
                            pnlPartySearch.Focus();
                            SearchVisible(false);
                            pnlSearch.Visible = false;
                            e.SuppressKeyPress = true;
                            dgPartySearch.Focus();
                            dgPartySearch.CurrentCell = dgPartySearch[0, dgPartySearch.CurrentRow.Index];
                            lblSearchName.Text = "Party Name: " + cmbPartyNameSearch.Text;
                            lblSearchName.Font = new Font("Verdana", 11, FontStyle.Bold);

                            //MovetoNext move2n = new MovetoNext(m2n);
                            //BeginInvoke(move2n, new object[] { dgPartySearch.CurrentRow.Index, 0, dgPartySearch });
                        }
                        else
                        {
                            pnlPartySearch.Visible = false;
                            pnlSearch.Visible = false;
                            DisplayMessage("Bill Not Found");
                            pnlSearch.Visible = true;
                            rbPartyName.Focus();
                            rbType_CheckedChanged(rbPartyName, null);
                        }
                        txtSearch.Text = "";
                        cmbPartyNameSearch.SelectedIndex = 0;

                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlSearch.Visible = false;
                    pnlPartySearch.Visible = false;
                    btnNew.Focus();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    rbInvNo.Checked = true;
                    rbType_CheckedChanged(rbInvNo, null);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPartySearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long tempNo;
                    e.SuppressKeyPress = true;
                    tempNo = ObjQry.ReturnLong("Select GRNNo From TGRN Where GRNNo=" + Convert.ToInt64(dgPartySearch.Rows[dgPartySearch.CurrentRow.Index].Cells[4].Value) + " and CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        SetNavigation();
                        FillControls();
                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnBillCancel.Enabled = true;
                        pnlPartySearch.Visible = false;
                        btnNew.Focus();
                        SearchVisible(false);
                    }
                    else
                    {
                        txtSearch.Text = "";
                        cmbPartyName.SelectedIndex = 0;
                        DisplayMessage("Bill Not Found");
                        txtSearch.Focus();
                        //SearchVisible(false);
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlPartySearch.Visible = false;
                    pnlSearch.Visible = true;
                    txtSearch.Focus();
                    rbType_CheckedChanged(sender, new EventArgs());
                }
                txtSearch.Text = "";
                cmbPartyNameSearch.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SearchVisible(bool flag)
        {
            txtSearch.Visible = flag;
            cmbPartyNameSearch.Visible = flag;
            btnPartyName.Visible = flag;
            lblLable.Visible = flag;
        }

        private void dgPartySearch_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;

            }
            else if (e.ColumnIndex == 2)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
            }
        }

        private void btnPartyName_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Master.AdvancedSearch Adsch = new Swami.Master.AdvancedSearch(GroupType.SundryDebtors);
            //    ObjFunction.OpenForm(Adsch);
            //    if (Adsch.LedgerNo != 0)
            //    {
            //        cmbPartyNameSearch.SelectedValue = Adsch.LedgerNo;
            //        cmbPartyNameSearch.Focus();
            //        Adsch.Close();
            //    }
            //}
            //catch (Exception exc)
            //{
            //    ObjFunction.ExceptionDisplay(exc.Message);
            //}
        }

        private void SalesAE_Activated(object sender, EventArgs e)
        {
            try
            {
                if (isDoProcess)
                {
                    long Pid = ObjFunction.GetComboValue(cmbPartyName);
                    ObjFunction.FillComb(cmbPartyName, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and (IsActive='true' or LedgerNo = " + Pid + ") order by LedgerName");
                    cmbPartyName.SelectedValue = Pid;

                    if (ObjQry.ReturnLong("Select Count(*) from TGRNInvoice where FkGRNNo=" + ID + "", CommonFunctions.ConStr) > 0)
                    {
                        btnUpdate.Visible = false; btnBillCancel.Visible = false;
                    }

                }
                isDoProcess = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                isDoProcess = false;
            }
        }

        private void SalesAE_Deactivate(object sender, EventArgs e)
        {
            isDoProcess = true;
        }

        private double CalculateTotal_Billed()
        {
            try
            {
                double TotalFinal = 0;

                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {

                    TotalFinal += Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].FormattedValue.ToString());

                }
                return TotalFinal;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return 0;
            }

        }

        private void dtpBillDate_Leave(object sender, EventArgs e)
        {

        }

        private void dgBill_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    PrintBill();
                }
            }
        }

        private void btnFirmWPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    bool islock = ObjQry.ReturnBoolean("select IsVoucherLock from TOtherVoucherEntry where  pkvoucherno =" + ID + "", CommonFunctions.ConStr);
                    if (!islock)
                    {
                        if (OMMessageBox.Show("You want to do Voucher Posting of this Challan ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {

                            PrintBillFirm(ID);

                        }
                    }
                    else
                    {
                        PrintBillFirm(ID);
                    }

                }

            }
        }

        public void GetLocationWisePrint()
        {
            DataTable dtData = new DataTable();

            string str;
            string[] ReportSession;
            string sql = "SELECT TOtherStock.ItemNo, TOtherStock.BilledQuantity, TOtherStock.FkUomNo,IsNull((Select GodownNo From MGodownSetting Where QuantitySlabFrom<TOtherStock.BilledQuantity And QuantitySlabTo>=TOtherStock.BilledQuantity And ItemNo=TOtherStock.ItemNo And UOMNO=TOtherStock.FkUomNo)," + Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_OutwardLocation)) + ") As Location " +
                         " FROM TOtherStock INNER JOIN MUOM ON TOtherStock.FkUomNo = MUOM.UOMNo " +
                         " WHERE (TOtherStock.FkVoucherNo = " + ID + ") " +
                         " GROUP BY TOtherStock.ItemNo, TOtherStock.BilledQuantity,TOtherStock.FkUomNo ";

            dtData = ObjFunction.GetDataView(sql).Table;
            DataTable dtRows = dtData.DefaultView.ToTable(true, "Location");
            for (int i = 0; i < dtRows.Rows.Count; i++)
            {
                DataRow[] dtDetails = dtData.Select("Location=" + dtRows.Rows[i][0].ToString());
                str = "";
                foreach (DataRow dtrow in dtDetails)
                {
                    if (str == "")
                        str = dtrow[0].ToString();
                    else
                        str += "," + dtrow[0].ToString();
                }
                if (str != "")
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    ReportSession = new string[4];
                    ReportSession[0] = ID.ToString();
                    ReportSession[1] = str;
                    ReportSession[2] = dtRows.Rows[i][0].ToString();
                    ReportSession[3] = txtInvNo.Text;
                    childForm = ObjFunction.GetReportObject("Reports.RptLocationWisePrint");
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            DisplayMessage("Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("Report not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Report not exist !!!");
                    }
                }
            }
        }

        private void btnLocationWPrint_Click(object sender, EventArgs e)
        {
            GetLocationWisePrint();
        }

        private void cmbGodown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgBill.Focus();
            }
        }

        private void dtpBillTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbPartyName.Focus();
            }
        }

        private void txtRefNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtRefNo_Leave(sender, e);
            }
        }

        private void txtRefNo_Leave(object sender, EventArgs e)
        {
            if (txtRefNo.Text.Trim() != "")
            {
                //dtpBillDate.Focus();
                cmbPartyName.Focus();


            }
            else
            {
                OMMessageBox.Show("Enter Ref No.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtRefNo.Focus();
            }

        } 
    }
      
}