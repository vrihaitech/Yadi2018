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
    public partial class StockUpdateUtilities : Form
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
        int cntRow, BillingMode, rowQtyIndex;
        bool Spaceflag = true;
        long ItemNameType = 0;/*bcdno,*/
        int iItemNameStartIndex = 3, ItemType = 0;
        string strUom, Param1Value = "", Param2Value = "";
        string[] strItemQuery, strItemQuery_last;
        DataTable dt = new DataTable();
        DataTablesCollection dtBillCollect = new DataTablesCollection();
        DateTime dtpBillDate = Convert.ToDateTime("1-1-1900");
        long VoucherUserNo;

        public long RequestSalesNo, ID, VoucherType;

        public StockUpdateUtilities()
        {
            InitializeComponent();
        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void StockInward_Load(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            dgBill.Enabled = false;
            VoucherType = 0;
            InitDelTable();
            ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)); //deepak
            initItemQuery();
            FillRateType();
            InitControls();
            dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;
            if (dtSearch.Rows.Count > 0)
            {
                ID = 0;
                FillControls();
                SetNavigation();
            }
            BtnNew.Focus();
            KeyDownFormat(this.Controls);
            new GridSearch(dgItemList, 1);
            formatPics();
          //  for (int i = 0; i < dgBill.Columns.Count; i++) dgBill.Columns[i].Visible = true;
        }


        private void formatPics()
        {
            pnlItemName.Width = 720;
            pnlItemName.Height = 235;
            pnlItemName.Top = 130;
            pnlItemName.Left = 88;

            pnlGroup1.Top = 140;
            pnlGroup1.Left = 200;
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

        public void InitControls()
        {
            VoucherUserNo = 0;
            while (dgBill.Rows.Count > 0)
            {
                dgBill.Rows.RemoveAt(0);
            }
            dgBill.Rows.Add();
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
            dgBill.CurrentCell = dgBill[1, 0];
        }

        private void FillControls()
        {
            tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(ID);
            VoucherUserNo = Convert.ToInt32(tVoucherEntry.VoucherUserNo);
            dtpBillDate = tVoucherEntry.VoucherDate;

            DataTable dt = ObjFunction.GetDataView("Select Case When Debit<>0 then Debit Else Credit End,LedgerNo,SrNo From TVoucherDetails Where FKVoucherNo=" + ID + " order by VoucherSrNo").Table;
            double subTot = ObjQry.ReturnDouble("Select sum(Debit) from TVoucherDetails  Where FKVoucherNo=" + ID + " ", CommonFunctions.ConStr);
            FillGrid();
            DataTable dtPartial = ObjFunction.GetDataView("Select Credit From TVoucherDetails Where FKVoucherNo=" + ID + " AND VoucherSrNo in (2,3) AND LedgerNo in(1,3) ").Table;
            if (dtPartial.Rows.Count == 2)
            {
            }

            long LastBillNo = ObjQry.ReturnLong("Select IsNull(Max(PkVoucherNo),0) From TVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND  PkVoucherNo<" + ID + "", CommonFunctions.ConStr);
            //dt = ObjFunction.GetDataView("SELECT IsNull(SUM(TStock.Quantity),0) AS Quantity, IsNull(SUM(TStock.Amount),0) AS Amount FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN "+
            //    " TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo WHERE (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.VoucherTypeCode = "+ VoucherType +") AND (TVoucherEntry.PkVoucherNo = "+ LastBillNo +")").Table;

            dt = ObjFunction.GetDataView("SELECT IsNull(SUM(TStock.Quantity),0) AS Quantity, IsNull(SUM(TStock.Amount),0) AS Amount FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                " TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + LastBillNo + ")").Table;
            dtVchMainDetails = ObjFunction.GetDataView("Select * From TVoucherDetails Where FKVoucherNo=" + ID + "").Table;
        }

        private void FillGrid()
        {
            dgBill.Rows.Clear();
            string sqlQuery = " SELECT 0 As SrNo,MStockItems.ItemName, TStock.Quantity,  MUOM.UOMName, MRateSetting.MRP, TStock.NetRate, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, " +
                             " TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, TStock.NetAmount, TStock.Amount, MStockBarcode.Barcode, TStock.PkStockTrnNo,MStockBarcode.PkStockBarcodeNo, TVoucherEntry.PkVoucherNo, TStock.ItemNo,  " +
                             " TStock.FkUomNo AS UOMNo, 0 AS TaxLedgerNo, 0 AS SalesLedgerNo,TStock.FkRateSettingNo AS PkRateSettingNo, TStock.FkItemTaxInfo AS PkItemTaxInfo, 0 AS StockFactor, TStock.BilledQuantity AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, " +
                             " tStock.SGSTPercentage, tStock.SGSTAmount, 0 AS SalesVchNo, 0 AS TaxVchNo, TVoucherEntry.CompanyNo, MRateSetting.ASaleRate AS SaleRate, MRateSetting.PurRate " +
                             " FROM TStock INNER JOIN TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MStockItems ON TStock.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                             " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
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

        public void CalculateTotal()
        {

            double subTotal = 0, totalDisc = 0, totalTax = 0;//totalChrg = 0, TotFinal = 0
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
                        if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value = 0;

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

        public void CalculateTotalCompanywise()
        {
            for (int i = 0; i < dgBill.Rows.Count; i++)
            {
                if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                {
                }
            }

            double subTotal = 0, totalDisc = 0, totalTax = 0;//totalChrg = 0 , TotFinal = 0
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
                        if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = 0;
                        if (dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value = 0;

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
                subTotal = Math.Round(subTotal, 00);
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
            bool flag = true;

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //double debit = 0;
            //long temp = 0;
            if (ValidationsMain() == false) return;
            if (dgBill.Rows.Count <= 1)
            {
                OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }
            dbTVoucherEntry = new DBTVaucherEntry();
            DeleteStockGodown();
            DeleteValues();//Delete Old Values
            int VoucherSrNo = 1;
            //Voucher Header Entry 
            tVoucherEntry = new TVoucherEntry();
            tVoucherEntry.PkVoucherNo = ID;
            tVoucherEntry.VoucherTypeCode = VoucherType;
            tVoucherEntry.VoucherUserNo = VoucherUserNo;
            tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.ToString("dd-MMM-yyyy"));
            tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
            tVoucherEntry.Narration = "Sales Bill";
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

            tVoucherEntry.TransporterCode = 0;
            tVoucherEntry.TransPayType = 0;
            tVoucherEntry.LRNo = "";
            tVoucherEntry.TransportMode = 0;
            tVoucherEntry.TransNoOfItems = 0;

            tVoucherEntry.UserID = DBGetVal.UserID;
            tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
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
                tStock.Quantity = Convert.ToInt64(dgBill[ColIndex.Quantity, i].Value.ToString());
                tStock.BilledQuantity = Convert.ToInt64(dgBill[ColIndex.ActualQty, i].Value.ToString());
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
              //  tStock.DisplayItemName = "";
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
                            tStockGodown.GodownNo = Convert.ToInt64(dt.Rows[row].ItemArray[0].ToString());
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
                string strVChNo = ObjQry.ReturnLong("Select VoucherUserNo From TVoucherEntry Where PKVoucherNo=" + tempID + "", CommonFunctions.ConStr).ToString();
                OMMessageBox.Show("Bill No " + strVChNo + " Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;
                ID = tempID;// ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                //string sql = "Exec SetTVoucherEntryCompany " + ID + "," + VoucherType + "";
                //ObjTrans.Execute(sql, CommonFunctions.ConStr);

                NavigationDisplay(5);
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dgBill.Enabled = false;
                BtnNew.Focus();

            }
            else
            {
                OMMessageBox.Show("Bill Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
        }

        private void setCompanyRatio()
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

        public bool Validations()
        {
            bool flag = true;
            return flag;
        }

        private void Control_Leave(object sender, EventArgs e)
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

        #region dgBill Methods and Events
        public delegate void MovetoNext(int RowIndex, int ColIndex);

        public void m2n(int RowIndex, int ColIndex)
        {
            dgBill.CurrentCell = dgBill.Rows[RowIndex].Cells[ColIndex];
        }

        private void Desc_Start()
        {
            if (dgBill.CurrentCell.Value == null || Convert.ToString(dgBill.CurrentCell.Value) == "")
            {
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
                        dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgBill.CurrentCell.Value;
                        Desc_MoveNext(ItemNo[0], BarcodeNo[0]);
                    }
                }
                //BindGrid();
                //CalculateTotal();
            }

            //from key_down
            //ItemType = 1;
            //FillItemList();
        }

        private void SearchBarcode(String strBarcode, out long[] ItemNo, out long[] BarcodeNo)
        {
            //string sql = "Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";
            string sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                " WHERE (MStockBarcode.Barcode = '" + strBarcode + "') AND (MStockItems.IsActive = 'true')";
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
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = ItemNo;
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;

            DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo = " + ItemNo).Table;
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
            //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = ObjQry.ReturnString("Select ItemName from MStockItems_V(NULL,NULL) where ItemNo = " + ItemNo,CommonFunctions.ConStr);

            if (ItemType == 2)
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();


            if (dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex].Value == null)
            {
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
            }
            Qty_MoveNext();


            //BindGrid();
            //CalculateTotal();
        }

        private void Qty_MoveNext()
        {
            rowQtyIndex = dgBill.CurrentCell.RowIndex;

            MovetoNext move2n = new MovetoNext(m2n);
            BeginInvoke(move2n, new object[] { rowQtyIndex, 3 });

            UOM_Start();

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

        private void UOM_Start()
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

        private void UOM_MoveNext()
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

        private void Rate_Start()
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
                    str = "select pksrno,ASaleRate from GetItemRateAll(" + ItemNo + "," + BarcodeNo + "," + UOMNo + ",null ,null,null)";
                }
                else
                {
                    // str = "select pksrno," + ObjFunction.GetComboValueString(cmbRateType) +
                    //   " from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,null,null)";
                    str = "select pksrno,ASaleRate from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,null,null)";
                }

                ObjFunction.FillList(lstRate, str);

                //if (lstRate.Items.Count == 1)
                //{
                    lstRate.SelectedIndex = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.SaleRate].Value = Convert.ToDouble(lstRate.Text).ToString("0.00");
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                    DataTable dtRt = ObjFunction.GetDataView("Select MRP,PurRate From MRateSetting where PkSrNo=" + lstRate.SelectedValue + "").Table;
                    if (dtRt.Rows.Count > 0)
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[0].ToString()).ToString("0.00");
                        dgBill.Rows[RowIndex].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[1].ToString()).ToString("0.00");
                    }

                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                    dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                    dgBill.Focus();
                    //BindGrid(dgBill.CurrentRow.Index);


                    BindGrid(dgBill.CurrentRow.Index);

                //}
                //else if (lstRate.Items.Count > 1)
                //{
                //    MovetoNext move2n = new MovetoNext(m2n);
                //    BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                //    dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                //    dgBill.Focus();

                //    CalculateTotal();
                //    pnlRate.Visible = true;
                //    lstRate.Focus();
                //}
                //else
                //{
                //    //error invalid Qty or UOM
                //}
            }
            else
            {
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                dgBill.Focus();
                //BindGrid(dgBill.CurrentRow.Index);




                BindGrid(dgBill.CurrentRow.Index);

            }
        }

        private void Rate_MoveNext()
        {
            if (dgBill.CurrentCell.Value != null)
            {
                if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == true)
                {
                    //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value) * Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[2].Value)) / Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MKTQuantity].Value);
                    //dgBill.CurrentCell.ReadOnly = true;
                    BindGrid(dgBill.CurrentCell.RowIndex);

                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1 });
                    dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                    dgBill.Focus();

                    //CalculateTotal();
                }
                else
                {
                    dgBill.CurrentCell.ErrorText = "Please Enter valid rate...";
                }
            }
        }

        private void Disc_MoveNext()
        {
            //Rate_MoveNext();
            CalculateTotal();
        }

        private void delete_row()
        {
            bool flag;
            if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null)
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

                dgBill.CurrentCell = dgBill[2, dgBill.Rows.Count - 1];
            }
        }

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Spaceflag == false) { Spaceflag = true; return; }
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
            {
               Desc_Start();
            }
            else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {

                if (dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "" && ObjFunction.CheckValidAmount(dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == true)
                {
                    Qty_MoveNext();
                }
                else
                {
                    dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Enter Valid Amount";
                }
            }
            else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
            {
                UOM_Start();
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

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                delete_row();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dgBill.Focus();
                if (dgBill.CurrentCell.ColumnIndex == 1)
                {
                    e.SuppressKeyPress = true;
                    dgBill.CurrentCell.Value = "";
                    Desc_Start();
                }
                else if (dgBill.CurrentCell.ColumnIndex == 2)
                {
                    e.SuppressKeyPress = true;
                    dgBill.CurrentCell.Value = "1";
                    Qty_MoveNext();
                }
                else if (dgBill.CurrentCell.ColumnIndex == 3)
                {
                    e.SuppressKeyPress = true;
                    if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value != null && dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value.ToString() != "")
                    {
                        UOM_Start();
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == 4)
                {
                    e.SuppressKeyPress = true;
                    Rate_MoveNext();
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
            else if (e.KeyCode == Keys.Escape)
            {
                BtnSave.Focus();
            }

        }

        private void dgBill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
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
                            dgBill.Focus();

                            dgBill.CurrentCell = dgBill[2, row];


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

        private void dgBill_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = (TextBox)e.Control;
            txt.KeyDown += new KeyEventHandler(txtSpace_KeyDown);
            //if (dgBill.CurrentCell.ColumnIndex == 2)
            //{
            //    TextBox txt = (TextBox)e.Control;
            //    txt.KeyDown += new KeyEventHandler(txtQuantity_KeyDown);
            //}
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

        private void BindGrid(int RowIndex)
        {
            long ItemNo, RateSettingNo, BarcodeNo;
            double StockConFactor;
            DataTable dtLedger = new DataTable();

            RateSettingNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value);
            ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);
            BarcodeNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value);
            DataTable dt = ObjFunction.GetDataView("SELECT r.FkBcdSrNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
               " t.PkSrNo,t.Percentage FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", NULL, " + GroupType.SalesAccount + ",null,NULL) As t " +
               " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;

            if (dt.Rows.Count > 0)
            {
                if (BarcodeNo == 0)
                {
                    BarcodeNo = Convert.ToInt64(dt.Rows[0][0].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;
                }

                dgBill.Rows[RowIndex].Cells[ColIndex.MKTQuantity].Value = Convert.ToInt64(dt.Rows[0][1].ToString());

                StockConFactor = Convert.ToDouble(dt.Rows[0][2].ToString());
                dgBill.Rows[RowIndex].Cells[ColIndex.StockFactor].Value = StockConFactor;

                dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo].Value = Convert.ToInt64(dt.Rows[0][3].ToString());
                dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo].Value = Convert.ToInt64(dt.Rows[0][4].ToString());
                dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo].Value = Convert.ToInt64(dt.Rows[0][5].ToString());
                if (dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value = 0;
                dgBill.Rows[RowIndex].Cells[ColIndex.PkVoucherNo].Value = 0;
                dgBill.Rows[RowIndex].Cells[ColIndex.SGSTPercentage].Value = Convert.ToDouble(dt.Rows[0][6].ToString()); ;

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
                if (BtnSave.Visible) btnSave_Click(sender, e);
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
                    //else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 2)
                    //{
                    //    dbTVoucherEntry.UpdateTStockBarCode(Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]));
                    //}
                    //else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 3)
                    //{
                    //    tRequire.PkRequireNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                    //    dbTVoucherEntry.DeleteTRequiredQuantity(tRequire);
                    //}
                    //else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 4)
                    //{
                    //    ObjTrans.Execute("Update TStockBarCode set IsSale='False' Where PkSrNo=(Select FKStockBarCode From TParkingBills Where PkSrNo=" + Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]) + ")", CommonFunctions.ConStr);

                    //    TParkingBills tParking = new TParkingBills();
                    //    tParking.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                    //    tParking.BillNo = 0;
                    //    dbTVoucherEntry.DeleteTParkingBills(tParking);
                    //}
                    //else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 5)
                    //{
                    //    tExchange.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                    //    dbTVoucherEntry.DeleteTExchangeDetails(tExchange);
                    //}
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            ID = 0;
            dtpBillDate = Convert.ToDateTime("1-1-1900");
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
                ID = ObjQry.ReturnLong("SELECT PkVoucherNo FROM TVoucherEntry WHERE (VoucherTypeCode = 0) AND (VoucherDate = '1/1/1900')", CommonFunctions.ConStr);
            dgBill.Focus();



        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            dgBill.Enabled = true;
            dgBill.Focus();
            dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            dgBill.Enabled = false;
            DisplayList(false);
            BtnNew.Focus();
        }

        public void DisplayList(bool flag)
        {
            pnlItemName.Visible = flag;
            pnlGroup1.Visible = flag;
            pnlGroup2.Visible = flag;
            pnlUOM.Visible = flag;
            pnlRate.Visible = flag;
            pnlStockGodown.Visible = flag;
        }

        private void btndelete_Click(object sender, EventArgs e)
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
            switch (iType)
            {
                case 1:
                    FillItemList(qNo);
                    break;
                case 2:
                    break;
                case 3:
                    string ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.SaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                        " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                        " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL,NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') " +
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
                    break;
            }
        }

        private void FillItemList(int qNo)
        {
            if (qNo == 0)
            {
                qNo = iItemNameStartIndex;
            }

            string ItemList = strItemQuery[qNo - 1];

            ItemList = ItemList.Replace("@cmbRateType@", "ASaleRate");// + ObjFunction.GetComboValueString(cmbRateType));

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

        private void FillItemList()
        {
            string ItemList = "";
            if (ItemType == 3)
            {
                ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.SaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                    " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                    " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                    " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                    " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL,NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                    " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                    " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                    // " dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + ",NULL) AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                    // " dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + ",NULL) AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                    " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                    "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') " +
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
                    //" dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                    //" dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo Where mItemMaster.ItemNo <> 1 " +
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
            //string ItemListStr = "";
            if (e.KeyChar == 13)
            {
                pnlGroup1.Visible = false;

                FillItemList(strItemQuery.Length);

                //if (ItemNameType == 2) 
                //{
                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + ObjFunction.GetComboValueString(cmbRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                //            " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                //            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                //            " FROM MStockItems_V(NULL,NULL) AS MStockItems INNER JOIN " +
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
            //string ItemListStr = "";
            if (e.KeyChar == 13)
            {
                pnlGroup2.Visible = false;

                FillItemList(strItemQuery.Length - 1);

                //if (ItemNameType == 3)
                //{
                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + ObjFunction.GetComboValueString(cmbRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
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

                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + ObjFunction.GetComboValueString(cmbRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
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
                //    ItemListStr = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting." + ObjFunction.GetComboValueString(cmbRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
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
            ObjFunction.FillList(lstUOM, "UomNo", "UomName");

            if (ItemType == 2)
                strUom = " Select * From GetUomList ('" + Convert.ToString(dgBill.Rows[RowIndex].Cells[ColIndex.Barcode].Value) + "',0," + Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";
            else
                strUom = " Select * From GetUomList (''," + Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value) + "," + Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";

            ObjFunction.FillList(lstUOM, strUom);

            if (lstUOM.Items.Count == 1)
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
                lstUOM.SelectedIndex = 0;

                UOM_MoveNext(); 
                //CalculateTotal();
                //pnlUOM.Visible = true;
                //lstUOM.Focus();
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
                CommonFunctions.ErrorMessge = e.Message;
            }
            return ValRatio;
        }
     
        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);

                dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value).ToString("0.00");//lstRate.Text;
                dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);//lstRate.SelectedValue;
                dgBill.CurrentRow.Cells[ColIndex.SaleRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[2].Value).ToString("0.00");
                dgBill.CurrentRow.Cells[ColIndex.PurRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[13].Value).ToString("0.00");
                pnlItemName.Visible = false;
                Desc_MoveNext(i, 0);
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

        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cmbPartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dgBill.Focus();
                e.SuppressKeyPress = true;
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
            if (e.KeyCode == Keys.Enter)
            {
                dgBill.Focus();
                dgBill.CurrentCell = dgBill[1, 0];
                e.SuppressKeyPress = true;
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
      
        #region Stock Godown related Methods
        int StockGodwnIndex;
        private void DisplayStockGodown()
        {
            StockGodwnIndex = dgBill.CurrentCell.RowIndex;
            if (pnlStockGodown.Visible == false)
            {
                if (dgBill.Rows[StockGodwnIndex].Cells[ColIndex.ItemNo].Value != null)
                {
                    pnlStockGodown.Visible = true;
                    //if (Convert.ToInt64(dgBill.Rows[StockGodwnIndex].Cells[ColIndex.PkStockTrnNo].Value) == 0)
                    //{
                    //    if (dtBillCollect.Count == StockGodwnIndex)
                    //    {
                    //        dtBillCollect.Add(ObjFunction.GetDataView("Exec GetStockGodownDetails 0,0").Table);
                    //        dgStockGodown.DataSource = dtBillCollect[dtBillCollect.Count - 1];
                    //        for (int row = 0; row < dgStockGodown.Rows.Count; row++)
                    //        {
                    //            if (Convert.ToInt64(dgStockGodown.Rows[row].Cells[0].Value) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation)))
                    //            {
                    //                dgStockGodown.Rows[row].Cells[2].Value = dgBill.Rows[StockGodwnIndex].Cells[ColIndex.Quantity].Value;
                    //                dgStockGodown.Rows[row].Cells[3].Value = dgBill.Rows[StockGodwnIndex].Cells[ColIndex.ActualQty].Value;
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        dgStockGodown.DataSource = dtBillCollect[dgBill.CurrentCell.RowIndex];
                    //    }
                    //}
                    //else
                    //{
                    //    dgStockGodown.DataSource = dtBillCollect[dgBill.CurrentCell.RowIndex];
                    //}
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

        public void CalculateGodownQty()
        {
            double Qty = 0;
            for (int i = 0; i < dgStockGodown.Rows.Count; i++)
            {
                Qty += Convert.ToDouble(dgStockGodown.Rows[i].Cells[2].Value);
            }
            txtStockGodwnQty.Text = Qty.ToString("0.00");
        }

        private void dgStockGodown_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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

        private void btnStkGodownOk_Click(object sender, EventArgs e)
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

        private void dgStockGodown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStkGodownOk.Focus();
            }
        }

        private void DeleteStockGodown()
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
        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            //DataTable dtTemp = ObjFunction.GetDataView("Select * From Temp1").Table;
            //DataTable dtTemp = ObjFunction.GetDataView("Select BarCode,MRP,OpeningStock FRom Temp2 T,MStockBarCode MS where MS.ItemNo=T.ItemCode AND (Select Count(*) From Temp2 T2 Where T2.ItemCode=T.ItemCode)=1 order by T.ItemCode").Table;
            DataTable dtTemp = ObjFunction.GetDataView("Select BarCode,MRP,OpeningStock From Temp").Table;
            int cnt = 0;
            for (int i = 0; i < dtTemp.Rows.Count; i++)
            {
                if (dtTemp.Rows[i].ItemArray[2] != null)
                {
                    if (dtTemp.Rows[i].ItemArray[2].ToString() != "")
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value = dtTemp.Rows[i].ItemArray[0].ToString();
                        dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.Quantity].Value = dtTemp.Rows[i].ItemArray[2].ToString();
                        dgBill_CellEndEdit(sender, new DataGridViewCellEventArgs(ColIndex.ItemName, dgBill.Rows.Count - 1));

                        //dgBill_CellEndEdit(sender, new DataGridViewCellEventArgs(ColIndex.Quantity, dgBill.Rows.Count - 1));
                        Application.DoEvents();
                        //System.Threading.Thread.Sleep(7000);
                        cnt++;
                    }
                }
            }
        }


    }
}
