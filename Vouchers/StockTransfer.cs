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
    public partial class StockTransfer : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TStock tStock = new TStock();
        TStockGodown tStockGodown = new TStockGodown();
       DBMItemMaster dbMItemMaster = new DBMItemMaster();

        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtUOMTemp = new DataTable();
        DataTable dtVchMainDetails = new DataTable();
        DataTable dtCompRatio = new DataTable();
        //bool BarFlag = true;
        bool StopOnQty = false;
        Color clrColorRow = Color.FromArgb(255, 224, 192);
        int cntRow, rowQtyIndex;
        long VoucherUserNo = 0;
        bool Spaceflag = true;
        long ItemNameType = 0;/*bcdno,*/
        int iItemNameStartIndex = 3, ItemType = 0;
        string strUom, Param1Value = "", Param2Value = "";
        string[] strItemQuery, strItemQuery_last;
        //int defaultUOMRowNo = -1, LowerUOMRowNo = -1;
        DataTable dtStock;
        DataTable dt = new DataTable();
        string MsgName = "";

        public long RequestSalesNo, ID, VoucherType;

        public StockTransfer()
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

        private void StockTransfer_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dgBill.Enabled = false;
                VoucherType = VchType.InternalTransfer;
                dtStock = new DataTable();

                InitDelTable();

                ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)); //deepak
                initItemQuery();
                StopOnQty = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnQty));

                ObjFunction.FillCombo(cmbGodown, "SELECT GodownNo, GodownName FROM MGodown WHERE (IsActive = 'true') And GodownNo<>1 ");
                FillRateType();
                txtInvNo.Enabled = false;
                dtpBillDate.Enabled = false;
                InitControls();

                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    FillControls();
                    SetNavigation();
                }

                setDisplay(true);
                BtnNew.Focus();
                KeyDownFormat(this.Controls);
                BtnUpdate.Visible = false;
                //for (int i = 0; i < dgBill.Columns.Count; i++)
                //    dgBill.Columns[i].Visible = true;
                new GridSearch(dgItemList, 1);
                formatPics();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void formatPics()
        {
            pnlItemName.Width = 610;
            pnlItemName.Height = 235;
            pnlItemName.Top = 130;
            pnlItemName.Left = 42;

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


                dtpBillDate.Value = DBGetVal.ServerTime;

                while (dgBill.Rows.Count > 0)
                {
                    dgBill.Rows.RemoveAt(0);
                }

                dgBill.Rows.Add();
                txtInvNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr) + 1).ToString();

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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillControls()
        {
            try
            {
                BtnUpdate.Visible = false;
                btnDelete.Visible = false;
                tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(ID);
                VoucherUserNo = Convert.ToInt64(tVoucherEntry.VoucherUserNo);

                txtInvNo.Text = tVoucherEntry.VoucherUserNo.ToString();
                dtpBillDate.Value = tVoucherEntry.VoucherDate;
                txtRemark.Text = tVoucherEntry.Remark;
                //if (tVoucherEntry.IsCancel == true)
                //{
                //    BtnUpdate.Visible = !tVoucherEntry.IsCancel;
                //}
                //else
                //    BtnUpdate.Visible = true;
                FillGrid();
                //dt = ObjFunction.GetDataView("SELECT IsNull(SUM(TStock.Quantity),0) AS Quantity, IsNull(SUM(TStock.Amount),0) AS Amount FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                //    " TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + VoucherUserNo + ")").Table;
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
                //string sqlQuery = "SELECT     0 AS SrNo, (Select ItemName from dbo.MStockItems_V(null, Tstock.ItemNo,NULL,NULL,NULL,NULL,NULL)) AS ItemName , TStock.Quantity, MUOM.UOMName, MRateSetting.MRP, TStock.Rate, MRateSetting.PurRate, TStock.NetRate, " +
                //          " TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, TStock.NetAmount, " +
                //          " TStock.Amount, MStockBarcode.Barcode, TStock.PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, TVoucherEntry.PkVoucherNo, TStock.ItemNo, " +
                //          " TStock.FkUomNo AS UOMNo,  TStock.FkRateSettingNo AS PkRateSettingNo, TStock.FkItemTaxInfo AS PkItemTaxInfo, " +
                //          " MRateSetting.StockConversion AS StockFactor, TStock.BilledQuantity AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, tStock.SGSTPercentage, tStock.SGSTAmount, " +
                //          " TStockGodown.PKStockGodownNo AS FkStockGodownNo, TVoucherEntry.CompanyNo " +
                //          " FROM TStock INNER JOIN TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MStockItems ON TStock.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                //          " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                //          " INNER JOIN  MUOM ON MRateSetting.UOMNo = MUOM.UOMNo INNER JOIN TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo " +
                //          " WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + ID + ") and (TStock.TrnCode=1)";
                string sqlQuery = "SELECT DISTINCT 0 AS SrNo,(SELECT ItemName FROM dbo.MStockItems_V(NULL, TStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TStock.Quantity, " +
                          " MUOM.UOMName, MRateSetting.MRP, TStock.Rate, MRateSetting.PurRate, TStock.NetRate, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, TStock.NetAmount, TStock.Amount, MStockBarcode.Barcode, " +
                          " TStock.PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, TVoucherEntry.PkVoucherNo, TStock.ItemNo, TStock.FkUomNo AS UOMNo, TStock.FkRateSettingNo AS PkRateSettingNo, TStock.FkItemTaxInfo AS PkItemTaxInfo, MRateSetting.StockConversion AS StockFactor, " +
                          " TStock.BilledQuantity AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, tStock.SGSTPercentage, tStock.SGSTAmount, TStockGodown.PKStockGodownNo AS FkStockGodownNo, TVoucherEntry.CompanyNo, (select TStock_1.pkStockTrnNo from Tstock as TStock_1 where fkvoucherno=" + ID + " and trncode=2 and TStock.ItemNo=TStock_1.ItemNo) as PkStockTrnNoTo," +
                          " (select TStockGodown_1.pkstockgodownno from TStockGodown  AS TStockGodown_1 INNER JOIN Tstock as TStock_1 on TStock_1.PkStockTrnNo = TStockGodown_1.FKStockTrnNo where  TStock_1.fkvoucherno=" + ID + " and TStock_1.trncode=1 and TStock.ItemNo=TStock_1.ItemNo) as PkStockGodownNoTo,MRateSetting.MRP as TempMRP, " +
                          " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE (TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown) + ") And    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS TempQty " +
                          " FROM TStock INNER JOIN TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MStockItems ON TStock.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                          " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN " +
                          " MUOM ON MRateSetting.UOMNo = MUOM.UOMNo INNER JOIN " +
                          " TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo INNER JOIN TStock AS TStock_1 ON TStock.FKVoucherNo = TStock_1.FKVoucherNo INNER JOIN " +
                          " TStockGodown AS TStockGodown_1 ON TStock_1.PkStockTrnNo = TStockGodown_1.FKStockTrnNo " +
                          " WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + ID + ") and (TStock.TrnCode=2)";
                dt = ObjFunction.GetDataView(sqlQuery).Table;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBill.Rows.Add();
                    for (int i = 0; i < dgBill.Columns.Count; i++)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            
                            dgBill.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                            dgBill.Rows[j].Cells[ColIndex.MRP].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.MRP].ToString()).ToString("0.00");
                            dgBill.Rows[j].Cells[ColIndex.Rate].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.Rate].ToString()).ToString("0.00");
                            dgBill.Rows[j].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.PurRate].ToString()).ToString("0.00");
                            //dgBill.Rows[j].Cells[i].ReadOnly = true;
                        }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    cmbGodown.SelectedValue = ObjQry.ReturnString("Select GodownNo from TStockGodown where PKStockGodownNo=" + Convert.ToInt64(dt.Rows[0].ItemArray[29].ToString()) + "", CommonFunctions.ConStr);

                }


                dtStock = ObjFunction.GetDataView(" SELECT TStockGodown.PKStockGodownNo, TStockGodown.FKStockTrnNo,TStockGodown.GodownNo " +
                                                          " FROM TStock INNER JOIN TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                                                          " TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo where  TVoucherEntry.PkVoucherNo=" + ID + " and  TStock.TrnCode=1").Table;
                ObjFunction.FillCombo(cmbGodownTo, "SELECT GodownNo, GodownName FROM MGodown WHERE (IsActive = 'true') And GodownNo not in(1," + ObjFunction.GetComboValue(cmbGodown) + ")  ");
                cmbGodownTo.SelectedValue = dtStock.Rows[0].ItemArray[2].ToString();
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




        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool ValidationsMain()
        {

            bool flag = false;

            if (ObjFunction.GetComboValue(cmbGodown) <= 0)
            {
                OMMessageBox.Show("Please Select From Location", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                cmbGodown.Focus();
                flag = false;
            }
            else if (ObjFunction.GetComboValue(cmbGodownTo) <= 0)
            {
                OMMessageBox.Show("Please Select To Location", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                cmbGodownTo.Focus();
                flag = false;
            }
            else flag = true;

            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidationsMain() == false) return;
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                CalculateTotal();
                dbTVoucherEntry = new DBTVaucherEntry();
                DeleteValues();//Delete Old Values

                if (ID != 0)
                    dbTVoucherEntry.ReverseStock(ID);
                int VoucherSrNo = 1;
                //Voucher Header Entry 
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;
                tVoucherEntry.VoucherTypeCode = VoucherType;
                tVoucherEntry.VoucherUserNo = VoucherUserNo;
                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
                tVoucherEntry.Narration = "Stock Transfer";
                tVoucherEntry.OrderType = 1;
                tVoucherEntry.Reference = "";
                tVoucherEntry.ChequeNo = 0;
                tVoucherEntry.ClearingDate = dtpBillDate.Value;
                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                tVoucherEntry.BilledAmount = 0;
                tVoucherEntry.ChallanNo = "";
                tVoucherEntry.Remark = txtRemark.Text.Trim();
                tVoucherEntry.MacNo = DBGetVal.MacNo;
                tVoucherEntry.PayTypeNo = 0;
                tVoucherEntry.RateTypeNo = tVoucherEntry.RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType));
                tVoucherEntry.TaxTypeNo = 0;
                tVoucherEntry.TransporterCode = 0;
                tVoucherEntry.TransPayType = 0;
                tVoucherEntry.LRNo = "";
                tVoucherEntry.TransportMode = 0;
                tVoucherEntry.TransNoOfItems = 0;
                tVoucherEntry.UserID = DBGetVal.UserID;
                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

                // for form Location
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
                    tStock.TrnCode = 2;
                    tStock.Quantity = Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                    tStock.BilledQuantity = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString());// *Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString());
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
                    tStock.CompanyNo = DBGetVal.FirmNo;
                    tStock.UserID = DBGetVal.UserID;
                    tStock.UserDate = DBGetVal.ServerTime.Date;
                    //tStock.DisplayItemName = "";
                    dbTVoucherEntry.AddTStock(tStock);

                    tStockGodown = new TStockGodown();
                    tStockGodown.PKStockGodownNo = Convert.ToInt64(dgBill[ColIndex.FkStockGodownNo, i].Value.ToString());
                    tStockGodown.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                    tStockGodown.GodownNo = ObjFunction.GetComboValue(cmbGodown);
                    tStockGodown.Qty = Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                    tStockGodown.ActualQty = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString());// *Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString());
                    tStockGodown.CompanyNo = DBGetVal.FirmNo;
                    tStockGodown.UserID = DBGetVal.UserID;
                    tStockGodown.UserDate = DBGetVal.ServerTime;
                    dbTVoucherEntry.AddTStockGodown(tStockGodown);
                }

                // for To location          

                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    tStock = new TStock();
                    if (Convert.ToInt64(dgBill[ColIndex.PkStockTrnNoTo, i].Value) == 0)
                    {
                        tStock.PkStockTrnNo = 0;
                    }
                    else
                    {
                        tStock.PkStockTrnNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNoTo].Value);
                    }

                    tStock.GroupNo = GroupType.CapitalAccount;
                    tStock.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                    tStock.FkVoucherSrNo = VoucherSrNo;
                    tStock.TrnCode = 1;
                    tStock.Quantity = Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                    tStock.BilledQuantity = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString());// *Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString());
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
                    tStock.CompanyNo = DBGetVal.FirmNo;
                    tStock.UserID = DBGetVal.UserID;
                    tStock.UserDate = DBGetVal.ServerTime.Date;
                   // tStock.DisplayItemName = "";
                    dbTVoucherEntry.AddTStock(tStock);

                    tStockGodown = new TStockGodown();
                    tStockGodown.PKStockGodownNo = (dgBill[ColIndex.PkStockGodownNoTo, i].Value == null) ? 0 : Convert.ToInt64(dgBill[ColIndex.PkStockGodownNoTo, i].Value.ToString());
                    tStockGodown.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                    tStockGodown.GodownNo = ObjFunction.GetComboValue(cmbGodownTo);
                    tStockGodown.Qty = Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                    tStockGodown.ActualQty = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString());// *Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString());
                    tStockGodown.UserID = DBGetVal.UserID;
                    tStockGodown.CompanyNo = DBGetVal.FirmNo;
                    tStockGodown.UserDate = DBGetVal.ServerTime;
                    dbTVoucherEntry.AddTStockGodown(tStockGodown);
                }
                dbTVoucherEntry.EffectStock();
                long tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    string strVChNo = ObjQry.ReturnLong("Select VoucherUserNo From TVoucherEntry Where PKVoucherNo=" + tempID + "", CommonFunctions.ConStr).ToString();
                    if (ID == 0)
                        OMMessageBox.Show(MsgName + " No " + strVChNo + " Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    else
                        OMMessageBox.Show(MsgName + " No " + strVChNo + " Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                    //dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;
                    DataRow drSearch = dtSearch.NewRow();
                    drSearch[0] = tempID;
                    dtSearch.Rows.Add(drSearch);

                    ID = tempID;
                    SetNavigation();
                    setDisplay(true);
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    dgBill.Enabled = false;
                    btnNew_Click(BtnNew, e);

                }
                else
                {
                    OMMessageBox.Show(MsgName + " Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
                    cmbGodownTo.Enabled = false;
                }
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
                            //dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgBill.CurrentCell.Value;
                            //Desc_MoveNext(ItemNo[0], BarcodeNo[0]);

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
                                    dgBill.Rows[rowIndex].Cells[ColIndex.Rate].Value = Convert.ToDouble(dt.Rows[0].ItemArray[3].ToString());
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

                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo = " + ItemNo + " AND IsActive='true' ").Table;
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
                //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = ObjQry.ReturnString("Select ItemName from MStockItems_V(NULL,NULL) where ItemNo = " + ItemNo,CommonFunctions.ConStr);

                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();


                if (StopOnQty == true)
                {
                    if (dgBill[2, dgBill.CurrentCell.RowIndex].Value == null || dgBill[2, dgBill.CurrentCell.RowIndex].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                        dgBill.CurrentCell = dgBill[2, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }
                    else
                        Qty_MoveNext();
                }
                else
                {
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
                BeginInvoke(move2n, new object[] { rowQtyIndex, 3 });

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
                //dgBill.CurrentCell = dgBill[3, row];

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
                string str;//, str2;
                MovetoNext move2n = new MovetoNext(m2n);
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
                        str = "select pksrno,ASaleRate from GetItemRateAll(" + ItemNo + "," + BarcodeNo + "," + UOMNo + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }
                    else
                    {
                        // str = "select pksrno," + ObjFunction.GetComboValueString(cmbRateType) +
                        //   " from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,null,null)";
                        str = "select pksrno,ASaleRate from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }

                    ObjFunction.FillList(lstRate, str);

                    double StockQty = ObjQry.ReturnDouble(" SELECT ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0) FROM TStock Inner Join TStockGodown on TStock.PkStockTrnNo =TStockGodown.FkStockTrnNo " +
                               " WHERE TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown) + " And TStock.ItemNo=" + ItemNo + " And  FkRateSettingNo =" + lstRate.SelectedValue + " And FKVoucherNo Not In (SELECT PkVoucherNo FROM TVoucherEntry WHERE (TVoucherEntry.IsCancel = 'True')) ", CommonFunctions.ConStr);
                    if (StockQty > 0)
                    {
                        int IndexRow = 0;
                        if (ItemExist(Convert.ToInt64(ItemNo), Convert.ToInt64(lstRate.SelectedValue), out IndexRow) == true)
                        {
                        }
                        else
                        {
                            dgBill.Rows[RowIndex].Cells[ColIndex.TempQty].Value = StockQty;
                        }
                    }
                    else
                    {

                        // if (dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value == null || dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value == "")
                        dgBill.Rows.RemoveAt(dgBill.Rows.Count - 1);
                        dgBill.Rows.Add();
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        OMMessageBox.Show("Zero or Less Than Zero Stock Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, ColIndex.ItemName });
                        return;
                    }

                    if (lstRate.Items.Count == 1)
                    {
                        lstRate.SelectedIndex = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = lstRate.Text;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value = ((dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString() == "") ? "0" : dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].EditedFormattedValue);
                        DataTable dtRt = ObjFunction.GetDataView("Select MRP,PurRate From MRateSetting where PkSrNo=" + lstRate.SelectedValue + "").Table;
                        if (dtRt.Rows.Count > 0)
                        {
                            dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[0].ToString()).ToString("0.00");
                            dgBill.Rows[RowIndex].Cells[ColIndex.TempMRP].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[0].ToString()).ToString("0.00");
                            dgBill.Rows[RowIndex].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[1].ToString()).ToString("0.00");
                        }

                        //MovetoNext move2n = new MovetoNext(m2n);
                        //BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                        //dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                        //dgBill.Focus();
                        ////BindGrid(dgBill.CurrentRow.Index);
                        //if (BarFlag == true)
                        //{

                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Quantity });
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, RowIndex];
                        dgBill.Focus();
                        //}

                        Rate_MoveNext();

                    }
                    else if (lstRate.Items.Count > 1)
                    {
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Quantity });
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, RowIndex];
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
                    if (Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.TempQty].EditedFormattedValue) >= ((dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].EditedFormattedValue.ToString() == "") ? 0 : Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].EditedFormattedValue)))
                    {
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Quantity });
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, RowIndex];
                        dgBill.Focus();
                        //BindGrid(dgBill.CurrentRow.Index);


                        Rate_MoveNext();
                    }
                    else
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value = dgBill.Rows[RowIndex].Cells[ColIndex.TempQty].Value;
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Quantity });
                        OMMessageBox.Show("Stock For This Barcode Is insufficient", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, RowIndex];
                    }
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
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString() != "")
                {
                    if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                        if (PKStockTrnNo != 0)
                        {
                            long StockGodownNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FkStockGodownNo].Value);
                            long PkStockTrnNoTo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNoTo].Value);
                            long StockGodownNoTo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockGodownNoTo].Value);
                            if (PKStockTrnNo != 0)
                            {
                                DeleteDtls(1, PKStockTrnNo, StockGodownNo);
                                DeleteDtls(1, StockGodownNoTo, StockGodownNoTo);
                            }
                        }
                        if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                        {
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            dgBill.Rows.Add();
                        }
                        else
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);

                        CalculateTotal();

                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        //private void delete_row()
        //{
        //    //bool flag;
        //    try
        //    {
        //        if (OMMessageBox.Show("Are you sure want to Cancel this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
        //        {
        //            if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString().Trim() != "")
        //            {
        //                long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
        //                long StockGodownNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FkStockGodownNo].Value);
        //                long PkStockTrnNoTo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNoTo].Value);
        //                long StockGodownNoTo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockGodownNoTo].Value);
        //                if (PKStockTrnNo != 0)
        //                {
        //                    DeleteDtls(1, PKStockTrnNo, StockGodownNo);
        //                    DeleteDtls(1, PkStockTrnNoTo, StockGodownNoTo);
        //                    dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
        //                    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
        //                }

        //                else
        //                {
        //                    dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
        //                    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
        //                }
        //                CalculateTotal();
        //            }
        //            else
        //            {
        //                if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
        //                {
        //                    dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
        //                    dgBill.Rows.Add();
        //                }
        //                else
        //                    dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
        //                dtBillCollect.RemoveAt(dgBill.CurrentCell.RowIndex);
        //            }

        //            CalculateTotal();
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

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
                    if (dgBill.CurrentCell.Value == null)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ItemName });
                    }
                    if (cmbGodown.Enabled == true && dgBill.Rows.Count > 0 && dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() != "" && dgBill.Rows[0].Cells[ColIndex.ItemName].Value != null)
                    {
                        cmbGodown.Enabled = false;
                        cmbGodownTo.Enabled = false;
                    }
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
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.MRP)
                {
                    if (dgBill.CurrentCell.Value != null && dgBill.CurrentCell.Value.ToString() != "")
                    {
                        if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == false)
                        {
                            dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Enter Valid MRP";
                        }
                        else
                        {
                            dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                            if (e.RowIndex < dgBill.Rows.Count - 1)
                            {
                                MovetoNext move2n = new MovetoNext(m2n);
                                BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName });
                            }
                            else
                            {
                                if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1)
                                {
                                    dgBill.Rows.Add();
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName });
                                }
                                else
                                {
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ItemName });
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

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    delete_row();
                    if (dgBill.Rows.Count == 1 && (dgBill.Rows[0].Cells[ColIndex.ItemName].Value == null || dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() == ""))
                    {
                        cmbGodown.Enabled = true; cmbGodownTo.Enabled = true;
                    }
                    else if (dgBill.Rows.Count == 0)
                        cmbGodown.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        //e.SuppressKeyPress = true;
                        //dgBill.CurrentCell.Value = "";
                        //Desc_Start();
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
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
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
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
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
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                    {
                        e.SuppressKeyPress = true;
                        Rate_MoveNext();
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.MRP)
                    {
                        if (dgBill.CurrentCell.Value != null && dgBill.CurrentCell.Value.ToString() != "")
                        {
                            if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == false)
                            {
                                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MRP].ErrorText = "Enter Valid MRP";
                            }
                            else
                            {
                                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MRP].ErrorText = "";
                                if (dgBill.CurrentCell.RowIndex < dgBill.Rows.Count - 1)
                                {
                                    MovetoNext move2n = new MovetoNext(m2n);
                                    BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex + 1, ColIndex.ItemName });
                                }
                                else
                                {
                                    if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1)
                                    {
                                        dgBill.Rows.Add();
                                        MovetoNext move2n = new MovetoNext(m2n);
                                        BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex + 1, ColIndex.ItemName });
                                    }
                                    else
                                    {
                                        MovetoNext move2n = new MovetoNext(m2n);
                                        BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.ItemName });
                                    }
                                }
                            }


                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.SrNo)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.ItemName });
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
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    BtnSave.Focus();
                }

                //else if (e.KeyCode == Keys.F7)
                //{
                //    if (BtnSave.Visible == true)
                //    {
                //        //DisplayStockGodown();
                //    }
                //}
                //else if (e.KeyCode == Keys.F9)
                //{
                //    if (VoucherType == VchType.StockInward)
                //    {
                //        if (plnRateSetting.Visible == false)
                //        {
                //            plnRateSetting.Visible = true;
                //            DisplayColumns();
                //            InitRateSetting(Convert.ToInt64(dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.ItemNo].Value));
                //            gvRateSetting.Focus();
                //            gvRateSetting.CurrentCell = gvRateSetting[4, 0];
                //            FillUomCombos();
                //            FormatStockConversionControls();
                //        }
                //        else
                //        {
                //            plnRateSetting.Visible = false;
                //        }
                //    }
                //}

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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)e.Control;
                txt.KeyDown += new KeyEventHandler(txtSpace_KeyDown);
                if (dgBill.CurrentCell.ColumnIndex == 2)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
                }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.MRP)
                {
                    TextBox txt2 = (TextBox)e.Control;
                    txt2.TextChanged += new EventHandler(txtMRP_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void txtMRP_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMasked((TextBox)sender, 2);
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.MRP)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 8, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMasked((TextBox)sender, 2);
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
                DataTable dt = ObjFunction.GetDataView("SELECT r.FkBcdSrNo, r.MKTQty, r.StockConversion, " +
                   " t.PkSrNo,t.Percentage FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", NULL, " + GroupType.SalesAccount + ",null,NULL) As t " +
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

                    dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo].Value = Convert.ToInt64(dt.Rows[0][3].ToString());

                    if (dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.PkVoucherNo].Value = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.SGSTPercentage].Value = Convert.ToDouble(dt.Rows[0][4].ToString()); ;
                    if (dgBill.Rows[RowIndex].Cells[ColIndex.FkStockGodownNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.FkStockGodownNo].Value = "0";
                    if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1 && (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate || dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity))
                    {
                        dgBill.Rows.Add();
                    }

                    CalculateTotal();
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearch.Visible = true;
                txtSearch.Text = ""; txtSearch.Enabled = true;
                txtSearch.Focus();
                BtnNew.Enabled = false;
                BtnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            //Btndelete.Visible = flag;
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
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            try
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
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    //btnExit_Click(sender, e);
                }

                if (e.KeyCode == Keys.F9)
                {
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #endregion

        #region Delete code
        public void InitDelTable()
        {
            try
            {
                dtDelete.Columns.Add();
                dtDelete.Columns.Add();
                dtDelete.Columns.Add();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DeleteDtls(int Type, long PkNo, long StockGodownNo)
        {
            try
            {
                DataRow dr = null;
                dr = dtDelete.NewRow();
                dr[0] = Type;
                dr[1] = PkNo;
                dr[2] = StockGodownNo;
                dtDelete.Rows.Add(dr);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
                            tStockGodown.PKStockGodownNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[2]);
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
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                InitDelTable();
                txtInvNo.Enabled = false;
                InitControls();
                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    FillControls();
                    SetNavigation();
                }

                setDisplay(true);
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
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                dgBill.Enabled = false;
                cmbGodown.Enabled = true;
                cmbGodownTo.Enabled = true;
                // BtnUpdate.Visible = true;
                InitControls();

                txtInvNo.Text = ObjQry.ReturnLong("Select IsNull(Max(VoucherUserNo),0)+1 From TVoucherEntry Where VoucherTypeCode=" + VoucherType + " ", CommonFunctions.ConStr).ToString();
                // cmbGodown.Focus();
                dtpBillDate.Focus();

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
                cmbGodown.Enabled = false;
                cmbGodownTo.Enabled = false;
                dgBill.Focus();
                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
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

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                DisplayList(false);
                NavigationDisplay(5);
                BtnUpdate.Visible = false;
                btnDelete.Visible = false;
                dgBill.Enabled = false;
                BtnNew.Focus();
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
                    dbTVoucherEntry.CancelTVoucherEntry(tVoucherEntry);

                    OMMessageBox.Show("Record deleted successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);


                    dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;
                    ID = ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                    SetNavigation();

                    setDisplay(true);
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    dgBill.Enabled = false;
                    FillControls();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
                        // ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                        //    " 0 AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        //    " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                        //    " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        //    " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL,NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        //    " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        //    " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        //    " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        //    "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') AND mItemMaster.IsActive='true' " +
                        //    " ORDER BY mItemMaster.ItemName";
                        DataTable dtBarCodeItemNo = ObjFunction.GetDataView("Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "'").Table;
                        string ItemList = "";
                        for (int i = 0; i < dtBarCodeItemNo.Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                ItemList += " Union all ";
                            }
                            //ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            //    " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE (TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown) + ") And     (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                            //    " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,MRateSetting.PurRate " +
                            //    " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                            //    " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            //    " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            //    " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                            //    " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.IsActive='true' and mItemMaster.FkStockGroupTypeNo<>3 ";
                            ////" mItemMaster.ItemNo in (Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') AND mItemMaster.IsActive='true' ";

                            ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                                " IsNull(MSB.CurrentStock,0) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                                " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,MRateSetting.PurRate " +
                                " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                                " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                                " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                                " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                                " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo LEFT OUTER JOIN MStockItemBalance MSB ON MSB.ItemNo = mItemMaster.ItemNo AND MSB.MRP = MRateSetting.MRP AND MSB.GodownNo = " + ObjFunction.GetComboValue(cmbGodown) +
                                "  Where mItemMaster.IsActive='true' and mItemMaster.FkStockGroupTypeNo<>3 ";
                            //" mItemMaster.ItemNo in (Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') AND mItemMaster.IsActive='true' ";

                        }
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
                if (ObjFunction.GetComboValue(cmbGodown) != 0)
                    ItemList = ItemList.Replace("@GodownNo@", "" + ObjFunction.GetComboValue(cmbGodown) + "");
                    //ItemList = ItemList.Replace("TStockGodown.GodownNo=2", "TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbGodown));

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
                        " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE   (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                        " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL,NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        // " dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + ",NULL) AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                        // " dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + ",NULL) AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') AND mItemMaster.IsActive='true'  and mItemMaster.FkStockGroupTypeNo<>3" +
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
                            " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                            " FROM MStockItems INNER JOIN " +
                            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        //" dbo.GetItemTaxAll(NULL, NULL, 10," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Sale ON mItemMaster.ItemNo = MItemTaxInfo_Sale.ItemNo INNER JOIN " +
                        //" dbo.GetItemTaxAll(NULL, NULL, 11," + ObjFunction.GetComboValue(cmbTaxType) + ", NULL) AS MItemTaxInfo_Pur ON mItemMaster.ItemNo = MItemTaxInfo_Pur.ItemNo INNER JOIN " +
                            " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo Where mItemMaster.ItemNo <> 1 and mItemMaster.FkStockGroupTypeNo<>3 " +
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
            public static int MRP = 4;
            public static int Rate = 5;
            public static int PurRate = 6;
            public static int NetRate = 7;
            public static int DiscPercentage = 8;
            public static int DiscAmount = 9;
            public static int DiscRupees = 10;
            public static int DiscPercentage2 = 11;
            public static int DiscAmount2 = 12;
            public static int DiscRupees2 = 13;
            public static int NetAmt = 14;
            public static int Amount = 15;
            public static int Barcode = 16;
            public static int PkStockTrnNo = 17;
            public static int PkBarCodeNo = 18;
            public static int PkVoucherNo = 19;
            public static int ItemNo = 20;
            public static int UOMNo = 21;
            public static int PkRateSettingNo = 22;
            public static int PkItemTaxInfo = 23;
            public static int StockFactor = 24;
            public static int ActualQty = 25;
            public static int MKTQuantity = 26;
            public static int SGSTPercentage = 27;
            public static int SGSTAmount = 28;
            public static int FkStockGodownNo = 29;
            public static int StockCompanyNo = 30;
            public static int PkStockTrnNoTo = 31;
            public static int PkStockGodownNoTo = 32;
            public static int TempMRP = 33;
            public static int TempQty = 34;


        }
        #endregion


        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                    int rwindex = 0;
                    if (Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value) > 0)
                    {

                        if (ItemExist(i, Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value), out rwindex) == true)
                        {
                            if (Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value) > ((dgBill.Rows[rwindex].Cells[ColIndex.Quantity].EditedFormattedValue.ToString() == "") ? 0 : Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].EditedFormattedValue)))
                            {
                                pnlItemName.Visible = false;
                                dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                                dgBill.Rows[rwindex].Cells[ColIndex.TempQty].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");
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
                                OMMessageBox.Show("Stock For This Item Is insufficient", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                            }
                        }
                        else
                        {

                            dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                            dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[2].Value).ToString("0.00");//lstRate.Text;
                            dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);//lstRate.SelectedValue;
                            dgBill.CurrentRow.Cells[ColIndex.MRP].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.TempMRP].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.PurRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[13].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.UOM].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[3].Value;
                            dgBill.CurrentRow.Cells[ColIndex.TempQty].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].Value = ((dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString() == "") ? "0" : dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].EditedFormattedValue);
                            pnlItemName.Visible = false;
                            Desc_MoveNext(i, 0);
                        }
                        if (cmbGodown.Enabled == true && dgBill.Rows.Count > 0 && dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() != "" && dgBill.Rows[0].Cells[ColIndex.ItemName].Value != null)
                        {
                            cmbGodown.Enabled = false;
                            cmbGodownTo.Enabled = false;
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Zero or Less Than Zero Stock Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                    }
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

        public bool ItemExist(long ItNo, out int rowIndex)
        {
            rowIndex = -1;
            try
            {
                bool flag = false;
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

        #region Rate Type Realted Methods and Functions
        public void FillRateType()
        {
            try
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


        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    cmbGodown.Focus();
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

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                long tempNo;
                if (e.KeyCode == Keys.Enter)
                {
                    tempNo = ObjQry.ReturnLong("Select PKVoucherNo From TVoucherEntry Where VoucherUserNo=" + txtSearch.Text + " and VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        SetNavigation();
                        FillControls();

                        pnlSearch.Visible = false;
                        BtnNew.Enabled = true;
                        BtnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        DisplayMessage("Bill Not Found");
                        txtSearch.Focus();
                    }
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
            BtnNew.Enabled = true;
            BtnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void cmbGodown_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ObjFunction.FillCombo(cmbGodownTo, "SELECT GodownNo, GodownName FROM MGodown WHERE (IsActive = 'true') And GodownNo not in(1," + ObjFunction.GetComboValue(cmbGodown) + ") ");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbGodownTo_Leave(object sender, EventArgs e)
        {
            try
            {

                if (ObjFunction.GetComboValue(cmbGodown) == 0 || ObjFunction.GetComboValue(cmbGodownTo) == 0)
                    dgBill.Enabled = false;
                else
                    dgBill.Enabled = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbGodown_Leave(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbGodownTo, "SELECT GodownNo, GodownName FROM MGodown WHERE (IsActive = 'true') And GodownNo not in(1," + ObjFunction.GetComboValue(cmbGodown) + ") ");
                if (ObjFunction.GetComboValue(cmbGodown) == 0 || ObjFunction.GetComboValue(cmbGodownTo) == 0)
                    dgBill.Enabled = false;
                else
                    dgBill.Enabled = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

    }
}
