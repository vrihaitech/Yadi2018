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

namespace Yadi.Utilities
{
    public partial class OpeningStockAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        TVoucherPayTypeDetails tVchPayTypeDetails = new TVoucherPayTypeDetails();
        TStock tStock = new TStock();
        TStockGodown tStockGodown = new TStockGodown();
        DBMItemMaster dbMItemMaster = new DBMItemMaster();

        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtUOMTemp = new DataTable();
        DataTable dtVchMainDetails = new DataTable();
        DataTable dtCompRatio = new DataTable();
        Color clrColorRow = Color.FromArgb(255, 224, 192);
        int cntRow, BillingMode, rowQtyIndex;//, tempindex;
        //long LastBillNo = 0;
        bool Spaceflag = true;
        long ItemNameType = 0;/*bcdno,*/
        int iItemNameStartIndex = 3, ItemType = 0;
        string strUom, Param1Value = "", Param2Value = "";
        string[] strItemQuery, strItemQuery_last;
        int defaultUOMRowNo = -1, LowerUOMRowNo = -1;
        //DataTable dtStock, dtRate;
        DataTable dt = new DataTable();
        DataTablesCollection dtBillCollect = new DataTablesCollection();
        DateTime dtpBillDate = Convert.ToDateTime("1-1-1900");//Convert.ToDateTime("1-1-1900");
        long /*cnt = 0, */VoucherUserNo;

        bool BarFlag = true;

        public long RequestSalesNo, ID, VoucherType;

        public OpeningStockAE()
        {
            InitializeComponent();
        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = (e.RowIndex + 1).ToString();
                }
                if (e.ColumnIndex == 2)
                {
                    if (e.Value != null && e.Value.ToString() != "")
                    {
                        e.Value = Convert.ToDouble(e.Value).ToString("0.00");
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (e.Value != null && e.Value.ToString() != "")
                    {
                        e.Value = Convert.ToDouble(e.Value).ToString("0.00");
                    }
                }
                if (e.ColumnIndex == 32)
                {
                    if (e.Value != null && e.Value.ToString() != "")
                    {
                        e.Value = Convert.ToDouble(e.Value).ToString("0.00");
                    }
                }
                if (e.ColumnIndex == 33)
                {
                    if (e.Value != null && e.Value.ToString() != "")
                    {
                        e.Value = Convert.ToDouble(e.Value).ToString("0.00");
                    }
                }
                if (e.ColumnIndex == ColIndex.ItemName)
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "")
                    {
                        if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "0")
                            dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    }
                    else
                        dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void OpeningStockAE_Load(object sender, EventArgs e)
        {
            try
            {
                dtpBillDate = ObjQry.ReturnDate("select FinancialYear from MFirm   ", CommonFunctions.ConStr);
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                ObjFunction.FillCombo(cmbGodown, "SELECT GodownNo, GodownName FROM MGodown WHERE (IsActive = 'true') and GodownNo=2");
                dgBill.Enabled = false;
                if (DBGetVal.KachhaFirm == false)
                {
                    VoucherType = VchType.OpeningBalance;
                }
                else
                {
                    VoucherType = VchType.DOpeningBalance;
                }
                InitDelTable();
                ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)); //deepak
                initItemQuery();
                FillRateType();
                InitControls();

                cmbGodown.SelectedIndex = 1;
                dtpBillDate = Convert.ToDateTime("1-1-1900");
                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    ID = Convert.ToInt32(dtSearch.Rows[0].ItemArray[0]);
                    FillControls();
                    // SetNavigation();
                    BtnUpdate.Visible = true;
                    BtnUpdate.Focus();
                    BtnNew.Visible = false;
                }
                else
                {

                    BtnUpdate.Visible = false;
                    BtnNew.Focus();
                }
                KeyDownFormat(this.Controls);
                new GridSearch(dgItemList, 1);
                formatPics();
                dgBill.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBill.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBill.Columns[32].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgBill.Columns[33].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                //for (int i = 0; i < dgBill.Columns.Count; i++) dgBill.Columns[i].Visible = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void formatPics()
        {
            pnlItemName.Width = 720;
            pnlItemName.Height = 235;
            pnlItemName.Top = 80;
            pnlItemName.Left = 88;

            pnlGroup1.Top = 80;
            pnlGroup1.Left = 100;
            pnlGroup1.Width = 300;
            pnlGroup1.Height = 220;

            pnlGroup2.Top = 140;
            pnlGroup2.Left = 100;
            pnlGroup2.Width = 300;
            pnlGroup2.Height = 220;

            pnlUOM.Top = 140;
            pnlUOM.Left = 372;
            pnlUOM.Width = 120;
            pnlUOM.Height = 80;

            pnlRate.Top = 140;
            pnlRate.Left = 430;
            pnlRate.Width = 120;
            pnlRate.Height = 80;

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
                VoucherUserNo = 0;
                while (dgBill.Rows.Count > 0)
                {
                    dgBill.Rows.RemoveAt(0);
                }
                dgBill.Rows.Add();
                string sqlQuery = " SELECT 0 AS Sr, mItemMaster.ItemName, TStock.Quantity, MUOM.UOMName, TStock.Rate, TStock.Amount, " +
                                 " TStock.PkStockTrnNo, MItemMaster.Barcode,TVoucherEntry.PkVoucherNo, mItemMaster.ItemNo, MUOM.UOMNo " +
                                 " FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                                 " INNER JOIN TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo ON " +
                                 " TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo INNER JOIN " +
                                 //  " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN " +
                                 " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo " +
                                 " WHERE (MItemMaster.Barcode = '') AND (TVoucherDetails.VoucherSrNo = 1) AND " +
                                 " (TVoucherEntry.VoucherTypeCode IN (9, 21))";
                dt = ObjFunction.GetDataView(sqlQuery).Table;
                dgBill.CurrentCell = dgBill[1, 0];
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
                tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(ID);
                VoucherUserNo = Convert.ToInt32(tVoucherEntry.VoucherUserNo);
                dtpBillDate = tVoucherEntry.VoucherDate;
                cmbGodown.SelectedIndex = 1;
                DataTable dt = ObjFunction.GetDataView("Select Case When Debit<>0 then Debit Else Credit End,LedgerNo,SrNo From TVoucherDetails Where FKVoucherNo=" + ID + " order by VoucherSrNo").Table;
                double subTot = ObjQry.ReturnDouble("Select sum(Debit) from TVoucherDetails  Where FKVoucherNo=" + ID + " ", CommonFunctions.ConStr);
                FillGrid();
                DataTable dtPartial = ObjFunction.GetDataView("Select Credit From TVoucherDetails Where FKVoucherNo=" + ID + " AND VoucherSrNo in (2,3) AND LedgerNo in(1,3) ").Table;
                if (dtPartial.Rows.Count == 2)
                {
                }

                long LastBillNo = ObjQry.ReturnLong("Select IsNull(Max(PkVoucherNo),0) From TVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND  PkVoucherNo<" + ID + "", CommonFunctions.ConStr);

                dt = ObjFunction.GetDataView("SELECT IsNull(SUM(TStock.Quantity),0) AS Quantity, IsNull(SUM(TStock.Amount),0) AS Amount FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                    " TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + LastBillNo + ")").Table;
                dtVchMainDetails = ObjFunction.GetDataView("Select * From TVoucherDetails Where FKVoucherNo=" + ID + "").Table;
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
                string sqlQuery = " SELECT 0 As SrNo,(SELECT case when esflag='False' then ItemName+'*' else ItemName end as ItemName FROM dbo.MStockItems_V(NULL, TStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TStock.Quantity,  MUOM.UOMName, MRateSetting.MRP, TStock.NetRate, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, " +
                                 " TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, TStock.NetAmount, TStock.Amount, Mitemmaster.Barcode, TStock.PkStockTrnNo,  mItemMaster.ItemNo as PkStockBarcodeNo, TVoucherEntry.PkVoucherNo, TStock.ItemNo,  " +
                                 " TStock.FkUomNo AS UOMNo, 0 AS TaxLedgerNo, 0 AS SalesLedgerNo,TStock.FkRateSettingNo AS PkRateSettingNo, TStock.FkItemTaxInfo AS PkItemTaxInfo, MRateSetting.StockConversion AS StockFactor, TStock.BilledQuantity AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, " +
                                 " tStock.SGSTPercentage, tStock.SGSTAmount, 0 AS SalesVchNo, 0 AS TaxVchNo, TVoucherEntry.CompanyNo, MRateSetting.ASaleRate AS SaleRate, MRateSetting.PurRate " +
                                 " FROM TStock INNER JOIN TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN mItemMaster ON TStock.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                                 "  MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                                 " INNER JOIN  MUOM ON MRateSetting.UOMNo = MUOM.UOMNo " +
                                 " WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + ID + ") ";
                dt = ObjFunction.GetDataView(sqlQuery).Table;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBill.Rows.Add();
                    for (int i = 0; i < dgBill.Columns.Count; i++)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dgBill.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                            dgBill.Rows[j].Cells[ColIndex.Rate].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.Rate].ToString()).ToString("0.00");
                            dgBill.Rows[j].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.PurRate].ToString()).ToString("0.00");
                            dgBill.Rows[j].Cells[ColIndex.SaleRate].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.SaleRate].ToString()).ToString("0.00");
                        }
                    }
                }
                dtBillCollect = new DataTablesCollection();
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    dtBillCollect.Add(ObjFunction.GetDataView("Exec GetStockGodownDetails " + dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString() + "," + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value + "").Table);
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
                double subTotal = 0, totalDisc = 0, totalTax = 0;//, TotFinal = 0;, totalChrg = 0
                if (Validations() == true)
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                    {
                        if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                        {
                            if (dgBill.Rows[i].Cells[ColIndex.Quantity].Value == null || dgBill.Rows[i].Cells[ColIndex.Quantity].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.Quantity].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value == null || dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.StockFactor].Value == null || dgBill.Rows[i].Cells[ColIndex.StockFactor].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.StockFactor].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.Rate].Value == null || dgBill.Rows[i].Cells[ColIndex.Rate].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.Rate].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value == null || dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value == null || dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value == null || dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value == null || dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value == null || dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value == null || dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value.ToString() == "") dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value = 0;


                            double Amount = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value))).ToString("0.00"));
                            double DiscAmt = Convert.ToDouble(((Amount * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value)) / 100).ToString("0.00"));
                            DiscAmt += Convert.ToDouble((((Amount - DiscAmt) * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.00"));
                            DiscAmt += Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value);

                            double tAmount = Amount - DiscAmt;
                            double TaxPerce = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value);
                            double TaxAmt = Convert.ToDouble(((tAmount * TaxPerce) / (100 + TaxPerce)).ToString("0.00"));
                            totalTax += TaxAmt;
                            double ttRate = (tAmount - TaxAmt) / Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value);


                            dgBill.Rows[i].Cells[ColIndex.Amount].Value = tAmount.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value = DiscAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value = TaxAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = (ttRate * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.ActualQty].Value = ((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.StockFactor].Value)));

                            subTotal = subTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetAmt].Value);
                            totalDisc = totalDisc + DiscAmt;// Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value);
                        }
                    }
                    subTotal = Convert.ToDouble(subTotal.ToString("0.00"));// Math.Round(subTotal, 00);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CalculateTotalCompanywise()
        {
            try
            {
                //for (int i = 0; i < dgBill.Rows.Count; i++)
                //{
                //    if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                //    {
                //    }
                //}

                //double subTotal = 0, totalDisc = 0, totalTax = 0;//, TotFinal = 0;,totalChrg = 0
                //if (Validations() == true)
                //{
                //    for (int i = 0; i < dgBill.Rows.Count; i++)
                //    {
                //        if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                //        {
                //            if (dgBill.Rows[i].Cells[ColIndex.Quantity].Value == null) dgBill.Rows[i].Cells[ColIndex.Quantity].Value = 1;
                //            if (dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value == null) dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value = 1;
                //            if (dgBill.Rows[i].Cells[ColIndex.StockFactor].Value == null) dgBill.Rows[i].Cells[ColIndex.StockFactor].Value = 1;
                //            if (dgBill.Rows[i].Cells[ColIndex.Rate].Value == null) dgBill.Rows[i].Cells[ColIndex.Rate].Value = 0;
                //            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value = 0;
                //            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value = 0;
                //            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value = 0;
                //            if (dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = 0;
                //            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value = 0;

                //            double Amount = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value))).ToString("0.00"));
                //            double DiscAmt = Convert.ToDouble(((Amount * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value)) / 100).ToString("0.00"));
                //            DiscAmt += Convert.ToDouble((((Amount - DiscAmt) * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.00"));
                //            DiscAmt += Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value);

                //            double tAmount = Amount - DiscAmt;
                //            double TaxPerce = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value);
                //            double TaxAmt = Convert.ToDouble(((tAmount * TaxPerce) / (100 + TaxPerce)).ToString("0.00"));
                //            totalTax += TaxAmt;
                //            double ttRate = (tAmount - TaxAmt) / Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value);


                //            dgBill.Rows[i].Cells[ColIndex.Amount].Value = tAmount.ToString("0.00");
                //            dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value = DiscAmt.ToString("0.00");
                //            dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value = TaxAmt.ToString("0.00");
                //            dgBill.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                //            dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = (ttRate * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString("0.00");
                //            dgBill.Rows[i].Cells[ColIndex.ActualQty].Value = ((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.StockFactor].Value)));

                //            subTotal = subTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetAmt].Value);
                //            totalDisc = totalDisc + DiscAmt;// Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value);

                //        }
                //    }
                //    subTotal = Math.Round(subTotal, 00);
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtDiscount1_Leave(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void txtChrg1_Leave(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool ValidationsMain()
        {
            try
            {
                bool flag = false;

                if (ObjFunction.GetComboValue(cmbGodown) <= 0)
                {
                    OMMessageBox.Show("Please Select Godown Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    cmbGodown.Focus();
                }

                else flag = true;

                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                pnlGroup1.Visible = false;
                pnlGroup2.Visible = false;
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.ItemName].Value == null || dgBill.Rows[i].Cells[ColIndex.ItemName].Value.ToString() == "")
                        dgBill.Rows.RemoveAt(i);
                }

                //double debit = 0;
                //long temp = 0;
                if (ValidationsMain() == false) return;
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                CalculateTotal();
                dbTVoucherEntry = new DBTVaucherEntry();
                DeleteStockGodown();
                DeleteValues();//Delete Old Values
                int VoucherSrNo = 1;
                //Voucher Header Entry 
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;
                tVoucherEntry.VoucherTypeCode = VoucherType;
                tVoucherEntry.VoucherUserNo = VoucherUserNo;

                int yearno = dtpBillDate.Year;
                int mno = dtpBillDate.Month;
                if (mno < 4)
                {
                    yearno = yearno - 1;
                }

                string str = "31-Mar-" + yearno;
                //tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.ToString("dd-MMM-yyyy"));
                tVoucherEntry.VoucherDate = Convert.ToDateTime(str);
                tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
                tVoucherEntry.Narration = "Opening Stock";
                tVoucherEntry.Reference = "";
                tVoucherEntry.ChequeNo = 0;
                tVoucherEntry.ClearingDate = Convert.ToDateTime(dtpBillDate.ToString("dd-MMM-yyyy"));
                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                tVoucherEntry.BilledAmount = 0;
                tVoucherEntry.ChallanNo = "";
                tVoucherEntry.Remark = "";
                tVoucherEntry.MacNo = DBGetVal.MacNo;
                tVoucherEntry.PayTypeNo = 0;
                tVoucherEntry.RateTypeNo = tVoucherEntry.RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType));
                tVoucherEntry.TaxTypeNo = 0;
                tVoucherEntry.UserID = DBGetVal.UserID;
                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                tVoucherEntry.TransporterCode = 0;
                tVoucherEntry.TransPayType = 0;
                tVoucherEntry.LRNo = "";
                tVoucherEntry.TransportMode = 0;
                tVoucherEntry.TransNoOfItems = 0;
                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {

                    tStock = new TStock();
                    if (Convert.ToInt64(dgBill[ColIndex.PkStockTrnNo, i].Value) == 0)
                    {
                        tStock.PkStockTrnNo = 0;
                    }
                    else
                    {
                        tStock.PkStockTrnNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value);
                    }

                    tStock.GroupNo = GroupType.CapitalAccount;
                    tStock.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                    tStock.FkVoucherSrNo = VoucherSrNo;
                    tStock.TrnCode = 1;
                    tStock.Quantity = Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                    tStock.BilledQuantity = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString());
                    tStock.Rate = Convert.ToDouble(dgBill[ColIndex.Rate, i].Value.ToString());
                    tStock.Amount = Convert.ToDouble(dgBill[ColIndex.Amount, i].Value.ToString());
                    tStock.SGSTPercentage = Convert.ToDouble(dgBill[ColIndex.SGSTPercentage, i].Value.ToString());
                    tStock.SGSTAmount = Convert.ToDouble(dgBill[ColIndex.SGSTAmount, i].Value.ToString());
                    tStock.DiscPercentage = Convert.ToDouble(dgBill[ColIndex.DiscPercentage, i].Value.ToString());
                    tStock.DiscAmount = Convert.ToDouble(dgBill[ColIndex.DiscAmount, i].Value.ToString());
                    tStock.DiscRupees = Convert.ToDouble(dgBill[ColIndex.DiscRupees, i].Value.ToString());
                    tStock.DiscPercentage2 = Convert.ToDouble(dgBill[ColIndex.DiscPercentage2, i].Value.ToString());
                    tStock.DiscAmount2 = Convert.ToDouble(dgBill[ColIndex.DiscAmount2, i].Value.ToString());
                    tStock.DiscRupees2 = Convert.ToDouble(dgBill[ColIndex.DiscRupees2, i].Value.ToString());
                    tStock.NetRate = Convert.ToDouble(dgBill[ColIndex.NetRate, i].Value.ToString());
                    tStock.NetAmount = Convert.ToDouble(dgBill[ColIndex.NetAmt, i].Value.ToString());
                    tStock.FkStockBarCodeNo = Convert.ToInt64(dgBill[ColIndex.PkBarCodeNo, i].Value.ToString());
                    tStock.FkUomNo = Convert.ToInt64(dgBill[ColIndex.UOMNo, i].Value.ToString());
                    tStock.FkRateSettingNo = Convert.ToInt64(dgBill[ColIndex.PkRateSettingNo, i].Value.ToString());
                    tStock.FkItemTaxInfo = Convert.ToInt64(dgBill[ColIndex.PkItemTaxInfo, i].Value.ToString());
                    tStock.UserID = DBGetVal.UserID;
                    tStock.UserDate = DBGetVal.ServerTime.Date;
                    tStock.CompanyNo = Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString());
                    tStock.LandedRate = 0;
                    tStock.DisplayItemName = Convert.ToString(dgBill[ColIndex.ItemName, i].Value.ToString());
                    // tStock.TRWeight = 0;
                    //tStock.GRWeight = 0;
                    tStock.Remarks = "";
                    // tStock.Freight = 0;
                    // tStock.OtherCharges = 0;
                    // tStock.SalesMan = 0;
                    //tStock.PackagingCharges = 0;
                    // tStock.IType =0;
                    dbTVoucherEntry.AddTStock(tStock);

                    DataTable dt = dtBillCollect[i];
                    if (dt.Rows.Count > 0)
                    {
                        for (int row = 0; row < dt.Rows.Count; row++)
                        {
                            if (Convert.ToDouble(dt.Rows[row].ItemArray[2].ToString()) > 0)
                            {
                                tStockGodown = new TStockGodown();
                                tStockGodown.PKStockGodownNo = Convert.ToInt64(dt.Rows[row].ItemArray[4].ToString());
                                tStockGodown.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                                //tStockGodown.GodownNo = Convert.ToInt64(dt.Rows[row].ItemArray[0].ToString());
                                tStockGodown.GodownNo = ObjFunction.GetComboValue(cmbGodown);
                                tStockGodown.Qty = Convert.ToDouble(dt.Rows[row].ItemArray[2].ToString());
                                tStockGodown.ActualQty = Convert.ToDouble(dt.Rows[row].ItemArray[3].ToString());
                                tStockGodown.UserID = DBGetVal.UserID;
                                tStockGodown.UserDate = DBGetVal.ServerTime.Date;
                                tStockGodown.CompanyNo = Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString());
                                dbTVoucherEntry.AddTStockGodown(tStockGodown);
                            }
                        }
                    }
                }
                long tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    ObjTrans.Execute("exec StockUpdateAll", CommonFunctions.ConStr);
                    string strVChNo = ObjQry.ReturnLong("Select VoucherUserNo From TVoucherEntry Where PKVoucherNo=" + tempID + "", CommonFunctions.ConStr).ToString();
                    OMMessageBox.Show("Bill No " + strVChNo + " Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;
                    ID = tempID;
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    dgBill.Enabled = false;
                    cmbGodown.SelectedIndex = 0;
                    FillControls();

                    BtnUpdate.Visible = true;
                    BtnNew.Visible = false;
                    BtnUpdate.Focus();

                }
                else
                {
                    OMMessageBox.Show("Bill Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                    dtCompRatio.Rows.Add(dr1);
                    debit = 0;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validations()
        {
            bool flag = true;
            return flag;
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            try
            {
                //double TotalAmt = 0;
                if (((TextBox)sender).Name == "txtDiscount1")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        //txtDiscRupees1.Text = Convert.ToDouble((TotalAmt * Convert.ToDouble(txtDiscount1.Text)) / 100).ToString("0.00");
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscRupees1")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscount2")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        //TotalAmt -=  Convert.ToDouble(txtDiscRupees1.Text);
                        //txtDiscRupees2.Text = Convert.ToDouble((TotalAmt * Convert.ToDouble(txtDiscount2.Text)) / 100).ToString("0.00");
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscRupees2")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscount3")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        //TotalAmt -=  (Convert.ToDouble(txtDiscRupees1.Text) + Convert.ToDouble(txtDiscRupees2.Text));
                        //txtDiscRupees3.Text = Convert.ToDouble((TotalAmt * Convert.ToDouble(txtDiscount3.Text)) / 100).ToString("0.00");
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscRupees3")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscount4")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        //TotalAmt -=  (Convert.ToDouble(txtDiscRupees1.Text) + Convert.ToDouble(txtDiscRupees2.Text) + Convert.ToDouble(txtDiscRupees3.Text));
                        // txtDiscRupees4.Text = Convert.ToDouble((TotalAmt * Convert.ToDouble(txtDiscount4.Text)) / 100).ToString("0.00");
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscRupees4")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtChrgRupees1")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Charges Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else CalculateTotal();
                }
                else if (((TextBox)sender).Name == "txtChrgRupees2")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Charges Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else CalculateTotal();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region dgBill Methods and Events
        public delegate void MovetoNext(int RowIndex, int ColIndex);

        public void m2n(int RowIndex, int ColIndex)
        {
            try
            {
                dgBill.CurrentCell = dgBill.Rows[RowIndex].Cells[ColIndex];
            }
            catch (Exception)
            {
            }
        }

        private void Desc_Start()
        {
            try
            {

                if (dgBill.Rows.Count > 1)
                {
                    cmbGodown.Enabled = false;
                }
                if (dgBill.CurrentCell.Value == null || Convert.ToString(dgBill.CurrentCell.Value) == "")
                {
                    BarFlag = true;
                    ItemType = 1;
                    FillItemList(0, ItemType); //FillItemList();
                }
                else
                {
                    ItemType = 2;
                    long[] BarcodeNo; long[] ItemNo;
                    SearchBarcode(dgBill.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);

                    if (ItemNo.Length == 0 || BarcodeNo.Length == 0)
                    {
                        dgBill.CurrentCell.Value = null;
                        for (int i = 1; i < dgBill.Columns.Count; i++)
                            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[i].Value = null;
                        DisplayMessage("Barcode Not Found");
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
                            int rowIndex = dgBill.CurrentCell.RowIndex;
                            int tempRowindex = dgBill.CurrentCell.RowIndex;
                            dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgBill.CurrentCell.Value;
                            Desc_MoveNext(ItemNo[0], BarcodeNo[0]);
                            UOM_Start();

                            if (dgBill.Rows.Count - 1 > rowIndex)
                            {
                                if (dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value == null || dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value.ToString() == "")
                                    dgBill.Rows.RemoveAt(dgBill.Rows.Count - 1);

                            }
                            if (ItemExist(Convert.ToInt64(dgBill.Rows[rowIndex].Cells[ColIndex.ItemNo].Value), Convert.ToInt64(dgBill.Rows[rowIndex].Cells[ColIndex.PkRateSettingNo].Value), out rowIndex) == true)
                            {
                                if (tempRowindex != rowIndex)
                                {
                                    pnlItemName.Visible = false;
                                    dgBill.Rows[rowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Quantity].Value) + 1;
                                    //dgBill.Rows[rowIndex].Cells[ColIndex.ItemName].Value = "";
                                    for (int i = 1; i < dgBill.Columns.Count; i++)
                                        dgBill.CurrentRow.Cells[i].Value = null;
                                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, rowIndex];
                                    dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rowIndex));
                                    dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                                }


                            }
                            else
                            {
                                rowIndex = dgBill.CurrentCell.RowIndex;
                                DataTable dt = ObjFunction.GetDataView(" Select TStock.FkUomNo,TStock.Rate,TStock.FkRateSettingNo,MRateSetting.ASaleRate as SaleRate,MRateSetting.PurRate,TStock.PkStockTrnNo,TStock.Quantity from MRateSetting INNER JOIN " +
                                                               "   TStock ON MRateSetting.PkSrNo = TStock.FkRateSettingNo  INNER JOIN  TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo " +
                                                               " Where TStock.FkVoucherNo=" + ID + " and TStock.ItemNo=" + Convert.ToInt64(dgBill.Rows[rowIndex].Cells[ColIndex.ItemNo].Value) + " AND MRateSetting.PkSrNo=" + Convert.ToInt64(dgBill.Rows[rowIndex].Cells[ColIndex.PkRateSettingNo].Value) + "" +
                                                               " and TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown) + "").Table;
                                if (dt.Rows.Count > 0)
                                {
                                    dgBill.Rows[rowIndex].Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dt.Rows[0].ItemArray[0].ToString());
                                    dgBill.Rows[rowIndex].Cells[ColIndex.Rate].Value = Convert.ToDouble(dt.Rows[0].ItemArray[1].ToString());//lstRate.Text;
                                    dgBill.Rows[rowIndex].Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dt.Rows[0].ItemArray[2].ToString());//lstRate.SelectedValue;
                                    dgBill.Rows[rowIndex].Cells[ColIndex.SaleRate].Value = Convert.ToDouble(dt.Rows[0].ItemArray[3].ToString());
                                    dgBill.Rows[rowIndex].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dt.Rows[0].ItemArray[4].ToString());
                                    dgBill.Rows[rowIndex].Cells[ColIndex.PkStockTrnNo].Value = Convert.ToInt64(dt.Rows[0].ItemArray[5].ToString());
                                    dgBill.Rows[rowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dt.Rows[0].ItemArray[6].ToString());
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { rowIndex, ColIndex.Quantity });
                                    //Desc_MoveNext(Convert.ToInt64(dgBill.Rows[rowIndex].Cells[ColIndex.ItemNo].Value), Convert.ToInt64(dgBill.Rows[rowIndex].Cells[ColIndex.PkBarCodeNo].Value));
                                }
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
            BarFlag = false;
            //string sql = "Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";
            string sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                " WHERE ((MStockBarcode.Barcode = '" + strBarcode + "') or (MStockItems.ShortCode='" + strBarcode + "')) AND (MStockItems.IsActive = 'true') and (MStockItems.FkStockGroupTypeNo<>3) AND (MRateSetting.IsActive='true')";
            DataTable dt = ObjFunction.GetDataView(sql).Table;
            BarcodeNo = new long[dt.Rows.Count];
            ItemNo = new long[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BarcodeNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                    ItemNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[1].ToString());
                    dgBill.CurrentCell.Value = dt.Rows[i].ItemArray[2].ToString();
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

                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo = " + ItemNo + " AND IsActive='true'").Table;
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
                //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = ObjQry.ReturnString("Select ItemName from MStockItems_V(NULL,NULL) where ItemNo = " + ItemNo,CommonFunctions.ConStr);

                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();


                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value == null || dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value.ToString() == "")
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.Quantity });
                //Qty_MoveNext();


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

                //MovetoNext move2n = new MovetoNext(m2n);
                //BeginInvoke(move2n, new object[] { rowQtyIndex, 3 });

                UOM_Start();
                Rate_MoveNext();
                dgBill.CurrentCell = dgBill[3, rowQtyIndex];
                dgBill.Focus();
                //CalculateTotal();//temp

                // dgBill.Rows[rowQtyIndex].Cells[2].ReadOnly = true;

                //found in the dgBill_keydown
                //if (dgBill.CurrentCell.ColumnIndex == 2)
                //{
                //    if (dgBill.Rows.Count > 1)
                //    {
                //        row = (dgBill.CurrentCell.RowIndex == 0) ? 0 : dgBill.CurrentCell.RowIndex;
                //        if (Convert.ToString(dgBill.Rows[row].Cells[2].Value) != "")
                //        {
                //            dgBill.CurrentCell = dgBill[3, row];
                //            dgBill.CurrentCell.ReadOnly = false;
                //        }
                //    }
                //}
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

                //dgBill.CurrentCell.ReadOnly = false;
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
                    ObjFunction.FillList(lstRate, "pksrno", "ASaleRate");
                    if (ItemType == 2)
                    {
                        str = "select pksrno,ASaleRate from GetItemRateAll(" + ItemNo + "," + UOMNo + ",null ,null,null)";
                    }
                    else
                    {
                        // str = "select pksrno," + ObjFunction.GetComboValueString(cmbRateType) +
                        //   " from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,null,null)";
                        str = "select pksrno,ASaleRate from GetItemRateAll(" + ItemNo + " ," + UOMNo + ",null ,null,null)";
                    }

                    ObjFunction.FillList(lstRate, str);

                    if (lstRate.Items.Count == 1)
                    {
                        lstRate.SelectedIndex = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.SaleRate].Value = Convert.ToDouble(lstRate.Text).ToString("0.00");
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                        DataTable dtRt = ObjFunction.GetDataView("Select MRP,PurRate From MRateSetting where PkSrNo=" + lstRate.SelectedValue + "").Table;
                        if (dtRt.Rows.Count > 0)
                        {
                            dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[0].ToString()).ToString("0.00");
                            dgBill.Rows[RowIndex].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[1].ToString()).ToString("0.00");
                        }
                        if (BarFlag == true)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                            dgBill.Focus();
                        }
                        //BindGrid(dgBill.CurrentRow.Index);


                        BindGrid(dgBill.CurrentRow.Index);

                    }
                    else if (lstRate.Items.Count > 1)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                        dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                        dgBill.Focus();

                        CalculateTotal();
                        pnlRate.Visible = true;
                        lstRate.Focus();
                    }
                    else
                    {
                        //error invalid Qty or UOM
                    }
                }
                else
                {

                    BindGrid(dgBill.CurrentRow.Index);
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                    dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                    dgBill.Focus();
                    //BindGrid(dgBill.CurrentRow.Index);





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
                    if (ObjFunction.CheckValidAmount(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value.ToString()) == true)
                    {
                        //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value) * Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[2].Value)) / Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MKTQuantity].Value);
                        //dgBill.CurrentCell.ReadOnly = true;
                        BindGrid(dgBill.CurrentCell.RowIndex);
                        if (lstUOM.Items.Count == 1)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1 });
                            dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                        }
                        dgBill.Focus();

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
            try
            {
                bool flag;
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString().Trim() != "")
                {
                    long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                    if (PKStockTrnNo != 0)
                    {
                        DeleteDtls(1, PKStockTrnNo);

                        ////For Sales LedgerNo
                        //flag = false;
                        //for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                        //{
                        //    if (dgBill.CurrentCell.RowIndex != i)
                        //    {
                        //        if (Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.StockCompanyNo].Value))
                        //        { flag = true; break; }
                        //    }
                        //}
                        //if (flag == false)
                        //    DeleteDtls(3, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value));

                        //For Party LedgerNo
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

                        //For Sales LedgerNo
                        flag = false;
                        for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                        {
                            if (dgBill.CurrentCell.RowIndex != i)
                            {
                                if (Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SalesVchNo].Value) == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.SalesVchNo].Value))
                                { flag = true; break; }
                            }
                        }
                        if (flag == false)
                            DeleteDtls(2, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SalesVchNo].Value));

                        //FOr TaxLedgerNo
                        flag = false;
                        for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                        {
                            if (dgBill.CurrentCell.RowIndex != i)
                            {
                                if (Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TaxVchNo].Value) == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.TaxVchNo].Value))
                                { flag = true; break; }
                            }
                        }
                        if (flag == false)
                            DeleteDtls(2, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TaxVchNo].Value));

                    }

                    if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                    {
                        dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        dgBill.Rows.Add();
                    }
                    else
                        dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);

                    CalculateTotal();
                    if (dgBill.Rows.Count > 0)
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

                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = 0;
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value = 0;

                    Desc_Start();
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName].Value == null || dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName].Value.ToString() == "")
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ItemName });
                    }
                    //if (cmbGodown.Enabled == true && dgBill.Rows.Count > 0 && dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() != "" && dgBill.Rows[0].Cells[ColIndex.ItemName].Value != null)
                    //{
                    //    cmbGodown.Enabled = false;
                    //}
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {

                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName].EditedFormattedValue.ToString().Trim() != "" && dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName].EditedFormattedValue.ToString().Trim() != "0")
                    {
                        if (dgBill.CurrentCell.EditedFormattedValue.ToString().Trim() == "") dgBill.CurrentCell.Value = "1";
                        if (Convert.ToDouble(dgBill.CurrentCell.Value.ToString()) == 0) dgBill.CurrentCell.Value = "1";
                        Qty_MoveNext();
                    }
                    else
                    {
                        dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                        dgBill.CurrentCell = dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName];
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                    {
                        UOM_Start();
                    }
                    else
                    {
                        dgBill.CurrentCell = dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName];
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                {
                    Rate_MoveNext();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    Disc_MoveNext();
                    if (dgBill.Columns[ColIndex.DiscAmount].Visible == true)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscAmount });
                    }
                    else
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.Barcode });
                    }
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
                    e.SuppressKeyPress = true;
                    delete_row();
                    if (dgBill.Rows.Count == 1 && (dgBill.Rows[0].Cells[ColIndex.ItemName].Value == null || dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() == ""))
                        cmbGodown.Enabled = true;
                    else if (dgBill.Rows.Count == 0)
                        cmbGodown.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                   // dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == 0)
                    {
                        dgBill.CurrentCell = dgBill[1, dgBill.CurrentCell.RowIndex];
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == 1)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value == null || dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value.ToString() == "")
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
                    else if (dgBill.CurrentCell.ColumnIndex == 2)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {
                            if (dgBill.CurrentCell.EditedFormattedValue.ToString().Trim() == "")
                                dgBill.CurrentCell.Value = "1";
                            Qty_MoveNext();
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == 3)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {
                            UOM_Start();
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.ItemName];
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == 4)
                    {
                        e.SuppressKeyPress = true;
                        Rate_MoveNext();
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1 });
                        dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                        dgBill.Focus();
                    }
                    else
                    {
                        BtnSave.Focus();
                    }
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
                    if (BtnSave.Visible == true)
                    {
                        DisplayStockGodown();
                    }

                }
                else if (e.KeyCode == Keys.F9)
                {
                    if (plnRateSetting.Visible == false)
                    {
                        plnRateSetting.Visible = true;
                        DisplayColumns();
                        InitRateSetting(Convert.ToInt64(dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.ItemNo].Value));
                        gvRateSetting.Focus();
                        gvRateSetting.CurrentCell = gvRateSetting[4, 0];
                        FillUomCombos();
                        FormatStockConversionControls();
                    }
                    else
                    {
                        plnRateSetting.Visible = false;
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    BtnSave.Focus();
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
                    //if (dgBill.Rows.Count == dgBill.CurrentCell.RowIndex + 1) AddRows = true;
                    //    if (flagParking == true) return;
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
                                dgBill.CurrentCell = dgBill[2, row];
                                dgBill.Focus();

                               


                            }

                        }
                    }
                }
                else if (col == ColIndex.Rate && row >= 0)
                {

                    //    if (flagParking == true) return;
                    dgBill.CurrentCell.ErrorText = "";
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (dgBill.CurrentCell.Value.ToString() != "" && dgBill.CurrentCell.Value.ToString() != "0")
                        {
                            if (ObjFunction.CheckNumeric(dgBill.CurrentCell.Value.ToString()) == true)
                            {

                                //dgBill[5, dgBill.CurrentCell.RowIndex].Value = Convert.ToDouble(dgBill[4, dgBill.CurrentCell.RowIndex].Value) * Convert.ToDouble(dgBill[2, dgBill.CurrentCell.RowIndex].Value);
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
            if (dgBill.CurrentCell.ColumnIndex == 2)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
            }
        }
        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (dgBill.CurrentCell.ColumnIndex == 2)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 10, OMFunctions.MaskedType.NotNegative);
            }
        }

        public void txtSpace_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                DataTable dt = ObjFunction.GetDataView("SELECT  r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                   " t.PkSrNo,t.Percentage FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", NULL, " + GroupType.SalesAccount + ",null,NULL) As t " +
                   " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;

                if (dt.Rows.Count > 0)
                {
                    //if (BarcodeNo == 0)
                    //{
                    //    BarcodeNo = Convert.ToInt64(dt.Rows[0][0].ToString());
                    //    dgBill.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;
                    //}

                    dgBill.Rows[RowIndex].Cells[ColIndex.MKTQuantity].Value = Convert.ToInt64(dt.Rows[0][0].ToString());

                    StockConFactor = Convert.ToDouble(dt.Rows[0][1].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.StockFactor].Value = StockConFactor;

                    dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo].Value = Convert.ToInt64(dt.Rows[0][2].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo].Value = Convert.ToInt64(dt.Rows[0][3].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo].Value = Convert.ToInt64(dt.Rows[0][4].ToString());
                    if (dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkVoucherNo].Value = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.SGSTPercentage].Value = Convert.ToDouble(dt.Rows[0][5].ToString()); ;

                    if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1 && dgBill.CurrentCell.ColumnIndex == 3)
                    {

                        dgBill.Rows.Add();
                    }

                    CalculateTotal();
                    DataTable dtStk;
                    DataRow dr = null;
                    if (dtBillCollect.Count == RowIndex)
                    {
                        dtStk = ObjFunction.GetDataView("Exec GetStockGodownDetails " + dgBill.Rows[0].Cells[ColIndex.PkStockTrnNo].Value.ToString() + "," + dgBill.Rows[0].Cells[ColIndex.ItemNo].Value.ToString() + "").Table;
                        for (int row = 0; row < dtStk.Rows.Count; row++)
                        {
                            if (Convert.ToInt64(dtStk.Rows[row].ItemArray[0].ToString()) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation)))
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
                        dtStk = dtBillCollect[RowIndex];
                        for (int row = 0; row < dtStk.Rows.Count; row++)
                        {
                            if (Convert.ToInt64(dtStk.Rows[row].ItemArray[0].ToString()) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation)))
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
                        dtBillCollect.Add(dtStk);
                    }

                }
                else
                {
                    for (int i = 1; i < dgBill.Columns.Count; i++)
                    {
                        dgBill.Rows[RowIndex].Cells[i].Value = null;
                    }
                    DisplayMessage("Items Tax Details Not Found.....");
                    dgBill.Focus();
                    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];

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
            BtnNew.Enabled = false;
            BtnUpdate.Enabled = false;
            btnDelete.Enabled = false;
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
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID)
                {
                    cntRow = i;
                    break;
                }
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
            if (e.KeyCode == Keys.F2)
            {
                // if (BtnSave.Visible) btnSave_Click(sender, e);
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
                PrintBill();
            }
            if (e.KeyCode == Keys.F9)
            {
            }

            //else if (e.KeyCode == Keys.F5)
            //{
            //    if (ID == 0)
            //        ParkingSave();
            //}
            //else if (e.KeyCode == Keys.F6)
            //{
            //    if (ID == 0)
            //        ShowParkingBill();
            //}
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
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #endregion

        #region Parial Payment Methods

        private void Partial_Leave(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox)sender).Text == "")
                {
                    OMMessageBox.Show("Please Enter amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    ((TextBox)sender).Focus();
                }
                else if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                {
                    OMMessageBox.Show("Please Enter valid amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    ((TextBox)sender).Focus();
                }
                else
                {
                    //double cash, credit;
                    //cash = (txtCash.Text == "") ? 0 : Convert.ToDouble(txtCash.Text); txtCash.Text = cash.ToString("0.00");
                    //credit = (txtCredit.Text == "") ? 0 : Convert.ToDouble(txtCredit.Text); txtCredit.Text = credit.ToString("0.00");
                    //txtBalance.Text = (Convert.ToDouble(txtGrandTotal.Text) - (cash + credit)).ToString("0.00");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void Partial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Partial_Leave(sender, e);
            }
        }
        #endregion

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(700);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void lblStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (BillingMode == 0)
                {
                    BillingMode = 1;
                    VoucherType = 0;
                }
                else
                {
                    BillingMode = 0;
                    VoucherType = 0;
                }

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                InitDelTable();
                InitControls();
                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    FillControls();
                    SetNavigation();
                }

                BtnNew.Focus();
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
                dtpBillDate = System.DateTime.Now;
                dtBillCollect = new DataTablesCollection();

                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                while (dgStockGodown.Rows.Count > 0)
                {
                    dgStockGodown.Rows.RemoveAt(0);
                }
                dgBill.Enabled = true;
                InitControls();
                if (ID == 0)
                {
                    ID = ObjQry.ReturnLong("SELECT PkVoucherNo FROM TVoucherEntry WHERE (VoucherTypeCode = 0) AND (VoucherDate = '1/1/1900')", CommonFunctions.ConStr);
                    if (ID != 0)
                    {
                        tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(ID);
                        VoucherUserNo = tVoucherEntry.VoucherUserNo;
                        dtpBillDate = tVoucherEntry.VoucherDate;
                    }
                }
                cmbGodown.Enabled = true;
                cmbGodown.Focus();
                //dgBill.Focus();
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
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                dgBill.Enabled = true;
                cmbGodown.Focus();
                //dgBill.Focus();
                //dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                //NavigationDisplay(5);
                while (dgBill.Rows.Count > 0) dgBill.Rows.RemoveAt(0);
                dgBill.Rows.Add();
                NavigationDisplay(5);
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                DisplayList(false);
                dgBill.Enabled = false;
                if (ID != 0)
                {
                    BtnUpdate.Visible = true;

                    BtnNew.Visible = false;
                    BtnUpdate.Focus();
                }
                else
                {
                    BtnUpdate.Visible = false;
                    BtnNew.Visible = true;
                    BtnNew.Focus();


                }
                cmbGodown.SelectedIndex = 1;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayList(bool flag)
        {
            pnlItemName.Visible = flag;
            pnlGroup1.Visible = flag;
            pnlGroup2.Visible = flag;
            pnlUOM.Visible = flag;
            pnlRate.Visible = flag;
            pnlStockGodown.Visible = flag;
            plnRateSetting.Visible = flag;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (OMMessageBox.Show("Are you sure you want to delete the record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {

                    dbTVoucherEntry = new DBTVaucherEntry();
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = ID;
                    dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);

                    for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                    {
                        //dbTVoucherEntry.UpdateTStockBarCode(Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString()));
                    }

                    OMMessageBox.Show("Record deleted successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);


                    dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;
                    ID = ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                    SetNavigation();
                    FillControls();

                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    dgBill.Enabled = false;
                }
            }

            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            BtnNew.Enabled = true;
            BtnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void txtChrg2_Leave(object sender, EventArgs e)
        {
            CalculateTotal();
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
                        DataTable dtBarCodeItemNo = ObjFunction.GetDataView("Select ItemNo from MItemMaster where Barcode = '" + dgBill.CurrentCell.Value + "'").Table;
                        string ItemList = "";
                        for (int i = 0; i < dtBarCodeItemNo.Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                ItemList += " Union all ";
                            }

                            ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                                " IsNull(MSB.CurrentStock,0) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                                " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,MRateSetting.PurRate " +
                                " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                                " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                                " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                                " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                                " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo LEFT OUTER JOIN MStockItemBalance MSB ON MSB.ItemNo = mItemMaster.ItemNo AND MSB.MRP = MRateSetting.MRP AND MSB.GodownNo = " + ObjFunction.GetComboValue(cmbGodown) +
                                "  Where mItemMaster.IsActive='true' and mItemMaster.FkStockGroupTypeNo<>3 ";

                        }
                        ItemList += " ORDER BY mItemMaster.ItemName";
                        DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                        if (dtItemList.Rows.Count > 0)
                        {
                            dgItemList.DataSource = dtItemList.DefaultView;
                            pnlItemName.Visible = true;
                            dgItemList.CurrentCell = dgItemList[2, 0];
                            dgItemList.Focus();
                        }
                        else
                        {
                            DisplayMessage("Items Not Found......");
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

                ItemList = ItemList.Replace("@cmbRateType@", "ASaleRate");// + ObjFunction.GetComboValueString(cmbRateType));
                ItemList = ItemList.Replace("ORDER BY ItemName", " and mItemMaster.FkStockGroupTypeNo<>3 ORDER BY ItemName");
                ItemList = ItemList.Replace("where FromDate<='@FromDate'", " ");
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
                ItemList = ItemList.Replace("@CompNo@", DBGetVal.FirmNo.ToString());
                if (ObjFunction.GetComboValue(cmbGodown) != 0)
                    //ItemList = ItemList.Replace("TStockGodown.GodownNo=2", "TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown));
                    ItemList = ItemList.Replace("@GodownNo@", "" + ObjFunction.GetComboValue(cmbGodown));
                switch (strItemQuery.Length - qNo)
                {
                    case 0:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
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
                                DisplayMessage("Items Not Found......");
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
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.SaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                       " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE   TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown) + " and (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                        " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL,NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +

                        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') AND mItemMaster.IsActive='true' and mItemMaster.FkStockGroupTypeNo<>3" +
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
                        DisplayMessage("Items Not Found......");
                    }
                }
                else if (ItemNameType == 1)
                {
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.SaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                            " FROM MStockItems INNER JOIN " +
                            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +

                            " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo Where mItemMaster.ItemNo <> 1 and mItemMaster.FkStockGroupTypeNo<>3" +
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
                        DisplayMessage("Items Not Found......");
                    }

                }
                else if (ItemNameType == 2)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup1, ItemList);
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
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
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //string ItemListStr = "";
                if (e.KeyChar == 13)
                {
                    pnlGroup1.Visible = false;

                    FillItemList(strItemQuery.Length);

                }
                else if (e.KeyChar == ' ')
                {
                    dgBill.Focus();
                    pnlGroup1.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //string ItemListStr = "";
                if (e.KeyChar == 13)
                {
                    pnlGroup2.Visible = false;

                    FillItemList(strItemQuery.Length - 1);

                }
                else if (e.KeyChar == ' ')
                {
                    dgBill.Focus();
                    pnlGroup2.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    int rowIndex = 0;
                    dgBill.CurrentRow.Cells[ColIndex.Rate].Value = lstRate.Text;
                    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;
                    if (ItemExist(Convert.ToInt64(dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value), Convert.ToInt64(dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value), out rowIndex) == true)
                    {
                        pnlItemName.Visible = false;
                        dgBill.Rows[rowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rowIndex].Cells[ColIndex.Quantity].Value) + 1;
                        //dgBill.Rows[rowIndex].Cells[ColIndex.ItemName].Value = "";
                        for (int i = 1; i < dgBill.Columns.Count; i++)
                            dgBill.CurrentRow.Cells[i].Value = null;
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, rowIndex];
                        dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rowIndex));
                        dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));


                    }
                    Rate_MoveNext();
                    pnlRate.Visible = false;
                }
                else if (e.KeyChar == ' ')
                {
                    dgBill.Focus();
                    pnlRate.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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

                if (lstUOM.Items.Count == 1)
                {
                    //dgBill.Rows.Add();
                    lstUOM.SelectedIndex = 0;
                    UOM_MoveNext();
                }
                else
                {
                    CalculateTotal();
                    pnlUOM.Visible = true;
                    lstUOM.Focus();
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
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CurrentCellChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
            public static int DiscPercentage = 6;
            public static int DiscAmount = 7;
            public static int DiscRupees = 8;
            public static int DiscPercentage2 = 9;
            public static int DiscAmount2 = 10;
            public static int DiscRupees2 = 11;
            public static int NetAmt = 12;
            public static int Amount = 13;
            public static int Barcode = 14;
            public static int PkStockTrnNo = 15;
            public static int PkBarCodeNo = 16;
            public static int PkVoucherNo = 17;
            public static int ItemNo = 18;
            public static int UOMNo = 19;
            public static int TaxLedgerNo = 20;
            public static int SalesLedgerNo = 21;
            public static int PkRateSettingNo = 22;
            public static int PkItemTaxInfo = 23;
            public static int StockFactor = 24;
            public static int ActualQty = 25;
            public static int MKTQuantity = 26;
            public static int SGSTPercentage = 27;
            public static int SGSTAmount = 28;
            public static int SalesVchNo = 29;
            public static int TaxVchNo = 30;
            public static int StockCompanyNo = 31;
            public static int SaleRate = 32;
            public static int PurRate = 33;
        }
        #endregion

        public long GetVoucherPK(string expression)
        {
            try
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
                    CommonFunctions.ErrorMessge = e.Message;
                }
                return strVal;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return 0;
            }
        }

        public double GetRatio(long CompNo)
        {
            try
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
                    CommonFunctions.ErrorMessge = e.Message;
                }
                return ValRatio;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return 0;
            }
        }

        #region Receipt Grid Methods


        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int rwindex = 0;
                    long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                    long RateSettingNo = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);


                    DataTable dt = ObjFunction.GetDataView(" Select TStock.FkUomNo,TStock.Rate,TStock.FkRateSettingNo,MRateSetting.ASaleRate as SaleRate,MRateSetting.PurRate,isnull(TStock.PkStockTrnNo,0) AS PkStockTrnNo,TStock.Quantity from MRateSetting INNER JOIN " +
                                                           "   TStock ON MRateSetting.PkSrNo = TStock.FkRateSettingNo  INNER JOIN  TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo " +
                                                           " Where TStock.FkVoucherNo=" + ID + " and TStock.ItemNo=" + i + " AND MRateSetting.PkSrNo=" + RateSettingNo + " and TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown) + "").Table;
                    if (dt.Rows.Count > 0)
                    {
                        if (ItemExist(i, RateSettingNo, out rwindex) == true)
                        {
                            pnlItemName.Visible = false;
                            dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                            // dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                            if (rwindex != dgBill.CurrentRow.Index)
                            {
                                for (int j = 0; j < dgBill.Columns.Count; j++)
                                    dgBill.CurrentRow.Cells[j].Value = "";
                            }
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                            dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                            dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                        }
                        else
                        {
                            dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dt.Rows[0].ItemArray[0].ToString());
                            dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dt.Rows[0].ItemArray[1].ToString());//lstRate.Text;
                            dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dt.Rows[0].ItemArray[2].ToString());//lstRate.SelectedValue;
                            dgBill.CurrentRow.Cells[ColIndex.SaleRate].Value = Convert.ToDouble(dt.Rows[0].ItemArray[3].ToString());
                            dgBill.CurrentRow.Cells[ColIndex.PurRate].Value = Convert.ToDouble(dt.Rows[0].ItemArray[4].ToString());
                            dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].Value = Convert.ToInt64(dt.Rows[0].ItemArray[5].ToString());
                            dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = Convert.ToDouble(dt.Rows[0].ItemArray[6].ToString());
                            dgBill.CurrentRow.Cells[ColIndex.UOM].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[3].Value;
                            dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[10].Value;
                            Desc_MoveNext(i, 0);
                        }
                    }
                    else
                    {
                        if (ItemExist(i, RateSettingNo, out rwindex) == true)
                        {
                            pnlItemName.Visible = false;
                            dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                            //dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                            if (rwindex != dgBill.CurrentRow.Index)
                            {
                                for (int j = 0; j < dgBill.Columns.Count; j++)
                                    dgBill.CurrentRow.Cells[j].Value = "";
                            }
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                            dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                            dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                        }
                        else
                        {
                            // dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value =i;
                            dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                            dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value).ToString("0.00");//lstRate.Text;
                            dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);//lstRate.SelectedValue;
                            dgBill.CurrentRow.Cells[ColIndex.SaleRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[2].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.PurRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[13].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.UOM].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[3].Value;
                            dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[10].Value;
                            dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].Value = ((dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString() == "") ? "0" : dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].EditedFormattedValue);
                            Desc_MoveNext(i, 0);
                        }
                    }
                    if (cmbGodown.Enabled == true && dgBill.Rows.Count > 0 && dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() != "" && dgBill.Rows[0].Cells[ColIndex.ItemName].Value != null)
                    {
                        cmbGodown.Enabled = false;
                    }
                    pnlItemName.Visible = false;
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
                else if (e.KeyCode == Keys.F6)
                {

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        #region Rate Type Realted Methods and Functions
        public void FillRateType()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("RateType");
                dt.Columns.Add("RateTypeName");
                DataRow dr = null;

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
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public int GetRateType()
        {
            string str = "";// ObjFunction.GetComboValueString(cmbRateType);
            int val = 0;
            if (str == "ASaleRate") val = 1;
            else if (str == "BSaleRate") val = 2;
            else if (str == "CSaleRate") val = 3;
            else if (str == "DSaleRate") val = 4;
            else if (str == "ESaleRate") val = 5;
            return val;
        }


        #endregion

        public void PrintBill()
        {
            try
            {
                string[] ReportSession;

                ReportSession = new string[2];
                ReportSession[0] = ID.ToString();
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();
                CrystalDecisions.CrystalReports.Engine.ReportClass childForm = ObjFunction.GetReportObject("Reports.GetBill");
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgBill.Focus();
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbRateType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true;
            }
        }

        private void cmbTaxType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgBill.Focus();
                    dgBill.CurrentCell = dgBill[1, 0];
                    e.SuppressKeyPress = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F9)
                {
                    pnlGroup1.Visible = false;
                    FillItemList(1);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region Transporter related Methods
        private void dgSaleHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
            }
        }

        private void dgPurHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
            }
        }


        #endregion

        #region RateSettingPln

        #region ColumnIndex

        public static class RateColIndex
        {
            public static int Sr = 0;
            public static int Date = 1;
            public static int BarCode = 2;
            public static int MRP = 3;
            public static int UOM = 4;
            public static int ASaleRate = 5;
            public static int BSaleRate = 6;
            public static int CSaleRate = 7;
            public static int DSaleRate = 8;
            public static int ESaleRate = 9;
            public static int MKTQty = 10;
            public static int PurRate = 11;
            public static int RateVariation = 12;
            public static int Chk = 13;
            public static int StockConversion = 14;
            public static int PkSrNo = 15;
            public static int BarCodeNo = 16;
            public static int ItemNo = 17;
        }
        #endregion

        public void DisplayColumns()
        {
            gvRateSetting.Columns[RateColIndex.ASaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive));
            gvRateSetting.Columns[RateColIndex.BSaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive));
            gvRateSetting.Columns[RateColIndex.CSaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive));
            gvRateSetting.Columns[RateColIndex.DSaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive));
            gvRateSetting.Columns[RateColIndex.ESaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive));
        }

        public void InitRateSetting(long ID)
        {
            try
            {
                DataGridViewComboBoxColumn cmbGridUom = gvRateSetting.Columns[RateColIndex.UOM] as DataGridViewComboBoxColumn;
                if (cmbGridUom != null)
                {
                    ObjFunction.FillCombo(cmbGridUom, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' ORDER BY UOMName");
                }

                DataTable dt = new DataTable();
                string str = "SELECT 0 AS srNo, MRateSetting.FromDate, MStockBarcode.Barcode, MRateSetting.MRP, MUOM.UOMNo, MRateSetting.ASaleRate, MRateSetting.BSaleRate, " +
                            " MRateSetting.CSaleRate, MRateSetting.DSaleRate, MRateSetting.ESaleRate, MRateSetting.MKTQty, MRateSetting.PurRate, MRateSetting.PerOfRateVariation, " +
                            " 'false' AS Chk, MRateSetting.StockConversion, MRateSetting.PkSrNo, MRateSetting.FkBcdSrNo,MRateSetting.ItemNo " +
                            " FROM  MStockBarcode INNER JOIN " +
                            " MUOM INNER JOIN " +
                            " dbo.GetItemRateAll(" + ID + ",null,null,null,null,NULL) as MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo INNER JOIN " +
                            " MStockItems ON MRateSetting.ItemNo = mItemMaster.ItemNo ON MStockBarcode.PkStockBarcodeNo = MRateSetting.FkBcdSrNo ";
                dt = ObjFunction.GetDataView(str).Table;
                gvRateSetting.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvRateSetting.Rows.Add();
                    for (int j = 0; j < gvRateSetting.Columns.Count; j++)
                    {
                        gvRateSetting.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                    gvRateSetting.Rows[i].Cells[RateColIndex.Chk].Value = "False";
                }
                for (int i = gvRateSetting.Rows.Count; i < 3; i++)
                {
                    gvRateSetting.Rows.Add();
                    gvRateSetting.Rows[i].Cells[RateColIndex.Sr].Value = "0";
                    gvRateSetting.Rows[i].Cells[RateColIndex.Date].Value = "0";

                    gvRateSetting.Rows[i].Cells[RateColIndex.PurRate].Value = "0";

                    for (int irate = 5; irate < 10; irate++)
                    {
                        gvRateSetting.Rows[i].Cells[irate].Value = "0";
                    }

                    gvRateSetting.Rows[i].Cells[RateColIndex.StockConversion].Value = "0";
                    gvRateSetting.Rows[i].Cells[RateColIndex.PkSrNo].Value = "0";
                    gvRateSetting.Rows[i].Cells[RateColIndex.BarCodeNo].Value = "0";
                    gvRateSetting.Rows[i].Cells[RateColIndex.MRP].Value = "0";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void gvRateSetting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvRateSetting.Rows.Count > 0 && e.ColumnIndex != RateColIndex.Chk)
                {
                    gvRateSetting.Rows[e.RowIndex].Cells[RateColIndex.Chk].Value = true;
                    for (int i = 0; i < gvRateSetting.Columns.Count - 1; i++)
                    {
                        if (gvRateSetting.Rows[e.RowIndex].Cells[i].Value == null || gvRateSetting.Rows[e.RowIndex].Cells[i].Value.ToString().Length == 0)
                        {
                            gvRateSetting.Rows[e.RowIndex].Cells[RateColIndex.Chk].Value = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void gvRateSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Value = (e.RowIndex + 1).ToString();
        }

        private void gvRateSetting_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F9)
                {
                    //FillUomCombos();\

                    plnRateSetting.Visible = false;
                    dgBill.Focus();
                    dgBill.CurrentCell = dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.ItemName];
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    cmbDefaultUOM.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnRateSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flage = false;
                for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[RateColIndex.Chk].FormattedValue) == true)
                    {
                        MRateSetting mRateSetting = new MRateSetting();
                        mRateSetting.PkSrNo = 0;//(gvRateSetting.Rows[i].Cells[13].Value == null) ? 0 : Convert.ToInt64(gvRateSetting.Rows[i].Cells[13].Value);
                        mRateSetting.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.ItemNo].Value);
                        mRateSetting.FkBcdSrNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.BarCodeNo].Value);
                        mRateSetting.FromDate = dtpBillDate;
                        mRateSetting.PurRate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.PurRate].Value);
                        mRateSetting.UOMNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.UOM].Value);
                        mRateSetting.ASaleRate = (gvRateSetting.Rows[i].Cells[RateColIndex.ASaleRate].Value == null) ? 0 : Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.ASaleRate].Value);
                        mRateSetting.BSaleRate = (gvRateSetting.Rows[i].Cells[RateColIndex.BSaleRate].Value == null) ? 0 : Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.BSaleRate].Value);
                        mRateSetting.CSaleRate = (gvRateSetting.Rows[i].Cells[RateColIndex.CSaleRate].Value == null) ? 0 : Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.CSaleRate].Value);
                        mRateSetting.DSaleRate = (gvRateSetting.Rows[i].Cells[RateColIndex.DSaleRate].Value == null) ? 0 : Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.DSaleRate].Value);
                        mRateSetting.ESaleRate = (gvRateSetting.Rows[i].Cells[RateColIndex.ESaleRate].Value == null) ? 0 : Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.ESaleRate].Value);
                        mRateSetting.StockConversion = Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.StockConversion].Value);
                        mRateSetting.PerOfRateVariation = Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.RateVariation].Value);
                        mRateSetting.MKTQty = Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.MKTQty].Value);
                        mRateSetting.MRP = (gvRateSetting.Rows[i].Cells[RateColIndex.MRP].Value == null) ? 0 : Convert.ToDouble(txtMRP.Text);
                        mRateSetting.IsActive = true;
                        mRateSetting.UserID = DBGetVal.UserID;
                        mRateSetting.UserDate = DBGetVal.ServerTime.Date;

                        if (dbMItemMaster.AddMRateSetting1(mRateSetting) == true)
                            flage = true;
                        else
                            flage = false;
                    }
                }

                if (flage == true)
                {
                    OMMessageBox.Show(" Rate Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    LatestRate(Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value));
                    FillItemList(2);
                    plnRateSetting.Visible = false;
                }
                else
                {
                    OMMessageBox.Show(" Rate not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void LatestRate(long ItemNo)
        {
            //DataTable dtl = new DataTable();
            //string sql = "";

        }

        private void cmbLowerUOM_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    FormatStockConversionControls();
                    if (txtStockCon.Visible == false)
                    {
                        txtMRP.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    if (txtMRP.Text.Trim() != "")
                    {
                        for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                        {
                            gvRateSetting.Rows[i].Cells[RateColIndex.MRP].Value = txtMRP.Text;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtMRP_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtMRP, "");
                if (txtMRP.Text.Trim() == "")
                {
                    EP.SetError(txtMRP, "Enter MRP");
                    EP.SetIconAlignment(txtMRP, ErrorIconAlignment.MiddleRight);
                    txtMRP.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtPurRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    //if (cmbLowerUOM.Items.Count == 2)
                    //{
                    //    gvRateSetting.Rows[0].Cells[11].Value = 1;
                    //    double amt = Convert.ToDouble(txtPurRate.Text);// / (Convert.ToDouble(gvRateSetting.Rows[0].Cells[10].Value) * Convert.ToDouble(gvRateSetting.Rows[0].Cells[11].Value));
                    //    gvRateSetting.Rows[0].Cells[4].Value = amt.ToString();
                    //}
                    //if (cmbLowerUOM.Items.Count > 2)
                    //{
                    for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                    {

                        if (i == defaultUOMRowNo)
                        {
                            //gvRateSetting.Rows[i].Cells[11].Value = txtStockCon.Text;
                            double amt = Convert.ToDouble(txtPurRate.Text);// / (Convert.ToDouble(gvRateSetting.Rows[i].Cells[10].Value) * Convert.ToDouble(gvRateSetting.Rows[i].Cells[11].Value));
                            gvRateSetting.Rows[i].Cells[RateColIndex.PurRate].Value = amt.ToString();
                        }
                        else //if (gvRateSetting.Rows[i].Cells[3].FormattedValue.ToString() == lblDefaultOther.Text)
                        {
                            //gvRateSetting.Rows[i].Cells[11].Value = txtStockConOther.Text;
                            double amt = Convert.ToDouble(txtPurRate.Text) * ((Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.MKTQty].Value) * Convert.ToDouble(gvRateSetting.Rows[i].Cells[RateColIndex.StockConversion].Value)) / (Convert.ToDouble(gvRateSetting.Rows[defaultUOMRowNo].Cells[RateColIndex.MKTQty].Value) * Convert.ToDouble(gvRateSetting.Rows[defaultUOMRowNo].Cells[RateColIndex.StockConversion].Value)));
                            gvRateSetting.Rows[i].Cells[RateColIndex.PurRate].Value = amt.ToString();
                        }
                        //else
                        //{
                        //    gvRateSetting.Rows[i].Cells[11].Value = 1;
                        //    double amt = Convert.ToDouble(txtPurRate.Text) / (Convert.ToDouble(gvRateSetting.Rows[i].Cells[10].Value) * Convert.ToDouble(gvRateSetting.Rows[i].Cells[11].Value));
                        //    gvRateSetting.Rows[i].Cells[4].Value = amt.ToString();
                        //}
                    }
                    //}
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtStockCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    int ii = 0;
                    for (int j = 0; j < gvRateSetting.Rows.Count; j++)
                    {
                        if (ii == 0 && j != LowerUOMRowNo)
                        {
                            gvRateSetting.Rows[j].Cells[RateColIndex.StockConversion].Value = txtStockCon.Text;
                            ii = 1;
                        }

                    }
                    if (txtStockConOther.Visible == false)
                    {
                        txtMRP.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtStockConOther_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    int ii = 0;
                    for (int j = 0; j < gvRateSetting.Rows.Count; j++)
                    {
                        if (ii == 0 && j != LowerUOMRowNo)
                        {
                            gvRateSetting.Rows[j].Cells[RateColIndex.StockConversion].Value = txtStockCon.Text;
                            ii = 1;
                        }
                        else if (ii == 1 && j != LowerUOMRowNo)
                        {
                            gvRateSetting.Rows[j].Cells[RateColIndex.StockConversion].Value = txtStockConOther.Text;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void FormatStockConversionControls()
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbLowerUOM) != 0)
                {
                    lblLower.Text = cmbLowerUOM.Text;
                    lblLowerOther.Text = cmbLowerUOM.Text;
                    defaultUOMRowNo = -1;
                    LowerUOMRowNo = -1;

                    for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.UOM].Value) == ObjFunction.GetComboValue(cmbDefaultUOM))
                        {
                            defaultUOMRowNo = i;
                            lblPur.Text = "/ " + gvRateSetting.Rows[i].Cells[RateColIndex.MKTQty].Value + " " + cmbDefaultUOM.Text;
                            if (gvRateSetting.Rows[i].Cells[RateColIndex.PurRate].Value != null)
                            {
                                txtPurRate.Text = gvRateSetting.Rows[i].Cells[RateColIndex.PurRate].Value.ToString();
                            }
                            else
                            {
                                txtPurRate.Text = "0.00";
                            }
                        }

                        if (Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.UOM].Value) == ObjFunction.GetComboValue(cmbLowerUOM))
                        {
                            gvRateSetting.Rows[i].Cells[RateColIndex.StockConversion].Value = 1;
                            LowerUOMRowNo = i;
                            //if (gvRateSetting.Rows[i].Cells[11].Value != null)
                            //    txtStockCon.Text = gvRateSetting.Rows[i].Cells[11].Value.ToString();
                        }
                        //else
                        //{
                        //    if (gvRateSetting.Rows[i].Cells[4].Value != null)
                        //        txtPurRate.Text = gvRateSetting.Rows[i].Cells[4].Value.ToString();
                        //}
                    }

                    if (Convert.ToBoolean(gvRateSetting.Rows[2].Cells[RateColIndex.Chk].FormattedValue) == true)
                    {
                        lbl1.Visible = true;
                        lblDefaultOther.Visible = true;
                        lblLowerOther.Visible = true;
                        txtStockConOther.Visible = true;
                    }

                    if (Convert.ToBoolean(gvRateSetting.Rows[1].Cells[RateColIndex.Chk].FormattedValue) == true)
                    {
                        lblDefault.Visible = true;
                        lblLower.Visible = true;
                        txtStockCon.Visible = true;
                    }

                    int ii = 0;
                    for (int j = 0; j < gvRateSetting.Rows.Count; j++)
                    {
                        if (ii == 0 && j != LowerUOMRowNo)
                        {
                            lblDefault.Text = gvRateSetting.Rows[j].Cells[RateColIndex.UOM].FormattedValue.ToString();
                            if (gvRateSetting.Rows[j].Cells[RateColIndex.StockConversion].Value != null)
                                txtStockCon.Text = gvRateSetting.Rows[j].Cells[RateColIndex.StockConversion].Value.ToString();
                            ii = 1;
                        }
                        else if (ii == 1 && j != LowerUOMRowNo)
                        {
                            lblDefaultOther.Text = gvRateSetting.Rows[j].Cells[RateColIndex.UOM].FormattedValue.ToString();
                            if (gvRateSetting.Rows[j].Cells[RateColIndex.StockConversion].Value != null)
                                txtStockConOther.Text = gvRateSetting.Rows[j].Cells[RateColIndex.StockConversion].Value.ToString();
                        }
                        //else
                        //{
                        //    if (gvRateSetting.Rows[j].Cells[4].Value != null)
                        //        txtPurRate.Text = gvRateSetting.Rows[j].Cells[4].Value.ToString();
                        //}
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillUomCombos()
        {
            try
            {
                string str = "0";
                for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                {
                    if (gvRateSetting.Rows[i].Cells[RateColIndex.UOM].Value != null && Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.UOM].Value) != 0)
                    {
                        str = str + "," + Convert.ToInt64(gvRateSetting.Rows[i].Cells[RateColIndex.UOM].Value).ToString();
                    }
                }

                ObjFunction.FillCombo(cmbLowerUOM, "Select UOMNo,UOMName from MUOM where UOMNO in(" + str + ") AND IsActive = 'True' Order By UOMName");
                ObjFunction.FillCombo(cmbDefaultUOM, "Select UOMNo,UOMName from MUOM where UOMNO in(" + str + ") AND IsActive = 'True' Order By UOMName");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbLowerUOM_Leave(object sender, EventArgs e)
        {
            try
            {

                FormatStockConversionControls();

                if (txtStockCon.Visible == false)
                {
                    txtMRP.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnRCancel_Click(object sender, EventArgs e)
        {
            try
            {
                plnRateSetting.Visible = false;
                FillItemList(2);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }

        #endregion

        #region Stock Godown related Methods
        int StockGodwnIndex;
        private void DisplayStockGodown()
        {
            try
            {
                StockGodwnIndex = dgBill.CurrentCell.RowIndex;
                if (pnlStockGodown.Visible == false)
                {
                    if (dgBill.Rows[StockGodwnIndex].Cells[ColIndex.ItemNo].Value != null)
                    {
                        pnlStockGodown.Visible = true;

                        dgStockGodown.DataSource = dtBillCollect[dgBill.CurrentCell.RowIndex];
                        if (dgStockGodown.Rows.Count > 0)
                        {
                            CalculateGodownQty();
                            dgStockGodown.Columns[0].Visible = false;
                            dgStockGodown.Columns[1].ReadOnly = true;
                            dgStockGodown.Columns[1].Width = 145;
                            dgStockGodown.Columns[2].Width = 68;
                            dgStockGodown.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgStockGodown.Columns[3].Width = 78;
                            dgStockGodown.Columns[3].ReadOnly = true;
                            dgStockGodown.Columns[4].Visible = false;


                            dgStockGodown.CurrentCell = dgStockGodown[2, 0];
                            dgStockGodown.Focus();
                        }
                    }
                    else
                        DisplayMessage("Please fill item details...");
                }
                else
                {
                    pnlStockGodown.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CalculateGodownQty()
        {
            try
            {
                double Qty = 0;
                for (int i = 0; i < dgStockGodown.Rows.Count; i++)
                {
                    Qty += Convert.ToDouble(dgStockGodown.Rows[i].Cells[2].Value);
                }
                txtStockGodwnQty.Text = Qty.ToString("0.00");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgStockGodown_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    if (dgStockGodown.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(dgStockGodown.CurrentCell.Value.ToString()) == true)
                        {
                            CalculateGodownQty();
                            dgStockGodown.Rows[e.RowIndex].Cells[3].Value = Convert.ToDouble(dgStockGodown.Rows[e.RowIndex].Cells[2].Value) * Convert.ToDouble(dgBill.Rows[StockGodwnIndex].Cells[ColIndex.StockFactor].Value);
                            dgStockGodown.CurrentCell.ErrorText = "";
                        }
                        else
                            dgStockGodown.CurrentCell.ErrorText = "Enter Valid Amount";
                    }
                    else
                    {
                        dgStockGodown.CurrentCell.Value = 0;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnStkGodownOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtStockGodwnQty.Text) <= Convert.ToDouble(dgBill.Rows[StockGodwnIndex].Cells[ColIndex.Quantity].Value))
                {

                    DataTable dt = dtBillCollect[StockGodwnIndex].Clone();
                    for (int i = 0; i < dgStockGodown.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int col = 0; col < dgStockGodown.Columns.Count; col++)
                            dr[col] = dgStockGodown.Rows[i].Cells[col].Value;
                        dt.Rows.Add(dr);
                    }
                    dtBillCollect.RemoveAt(StockGodwnIndex);
                    dtBillCollect.Insert(StockGodwnIndex, dt);
                    pnlStockGodown.Visible = false;
                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, StockGodwnIndex];
                    dgBill.Focus();
                }
                else
                {
                    OMMessageBox.Show("Item Bill Quantity and Stock Godown Quantity doesn't match.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    btnStkGodownOk.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void dgStockGodown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStkGodownOk.Focus();
            }
        }

        private void DeleteStockGodown()
        {
            try
            {
                DataTable dt;
                for (int i = 0; i < dtBillCollect.Count; i++)
                {
                    dt = dtBillCollect[i];
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        if (Convert.ToInt64(dt.Rows[row].ItemArray[4].ToString()) != 0 && Convert.ToDouble(dt.Rows[row].ItemArray[2].ToString()) == 0)
                        {
                            DeleteDtls(4, Convert.ToInt64(dt.Rows[row].ItemArray[4].ToString()));
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #endregion

        public bool ItemExist(long ItNo, long FKRateSettingNo, out int rowIndex)
        {
            rowIndex = -1;
            try
            {
                bool flag = false;
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) && FKRateSettingNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value))
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

        private void cmbGodown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbGodown) > 0)
                {
                    dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                    dgBill.Focus();
                }
                else
                    cmbGodown.Focus();
            }
        }

        private void cmbGodown_Leave(object sender, EventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbGodown) <= 0)
                {
                    cmbGodown.SelectedValue = "2";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);

            }
        }

    }
}
