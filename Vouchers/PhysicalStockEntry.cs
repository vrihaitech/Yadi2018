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
    public partial class PhysicalStockEntry : Form
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

        DataTablesCollection dtBillCollect = new DataTablesCollection();

        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtUOMTemp = new DataTable();
        Color clrColorRow = Color.FromArgb(255, 224, 192);
        int cntRow, rowQtyIndex;
        long VoucherUserNo = 0;
        bool Spaceflag = true;
        long ItemNameType = 0;
        int iItemNameStartIndex = 3, ItemType = 0;
        string strUom, Param1Value = "", Param2Value = "";
        string[] strItemQuery, strItemQuery_last;
        //int defaultUOMRowNo = -1, LowerUOMRowNo = -1;
        DataTable dtInventory = new DataTable();

        long ScheduleType;

        DataTable dt = new DataTable();
        string MsgName = "";

        public long RequestSalesNo, ID, VoucherType;

        public PhysicalStockEntry()
        {
            InitializeComponent();
            VoucherType = VchType.PhysicalStock;
            MsgName = "Physical Stock";
            ScheduleType = 1;
            lblSearch.Text = "Physical Stock No.";
        }

        public PhysicalStockEntry(DataTable dt)
        {
            InitializeComponent();
            dtInventory = new DataTable();
            VoucherType = VchType.PhysicalStock;
            MsgName = "Physical Stock";
            lblSearch.Text = "Physical Stock No.";
            ScheduleType = 2;
            dtInventory = dt;

        }

        //public void FillSchedulType()
        //{

        //    DataTable dt = ObjFunction.GetDataView("Exec GetInventoryCountSchedule '" + dtpBillDate.Text + "'").Table;

        //    btnNew_Click(BtnNew, new EventArgs());
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {

        //        dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.ItemName].Value = dt.Rows[i].ItemArray[1].ToString();
        //        dgBill_CellEndEdit(dgBill, new DataGridViewCellEventArgs(ColIndex.ItemName, dgBill.CurrentRow.Index));
        //        System.Threading.Thread.Sleep(100);

        //    }

        //}

        private void FillSchedulType()
        {
            
           
            btnNew_Click(BtnNew, new EventArgs());
            string str = "";
            for (int i = 0; i < dtInventory.Rows.Count; i++)
            {
                if (str == "")
                    str += dtInventory.Rows[i].ItemArray[0].ToString();
                else
                    str += "," + dtInventory.Rows[i].ItemArray[0].ToString();
            }
            if (str != "")
            {
                dgBill.Rows.Clear();
                string sqlQuery = "SELECT 0 AS SrNo,(SELECT ItemName FROM dbo.MStockItems_V(NULL, mItemMaster.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, 1 AS Quantity, " +
                                 " IsNull( (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock     WHERE      (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))),0) AS Stock, " +
                                 " 0 AS ActualStock, MUOM.UOMName, MRateSetting.MRP, MRateSetting.ASaleRate AS Rate, MRateSetting.PurRate, 0 AS NetRate, 0 AS DiscPercentage, 0 AS DiscAmount, " +
                                 " 0 AS DiscRupees, 0 AS DiscPercentage2, 0 AS DiscAmount2, 0 AS DiscRupees2, 0 AS NetAmount, 0 AS Amount, MStockBarcode.Barcode, 0 AS PkStockTrnNo, " +
                                 " MStockBarcode.PkStockBarcodeNo, 0 AS PkVoucherNo, mItemMaster.ItemNo, mItemMaster.UOMPrimary AS UOMNo, MRateSetting.PkSrNo AS PkRateSettingNo, " +
                                 " 0 AS PkItemTaxInfo, MRateSetting.StockConversion AS StockFactor, 0 AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, 0 AS TaxPercentage, 0 AS TaxAmount, " +
                                 " 0 AS FkStockGodownNo, 0 AS CompanyNo " +
                                 " FROM         MStockBarcode INNER JOIN " +
                                 " MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo INNER JOIN MUOM INNER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS   MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                                 " Where  mItemMaster.ItemNo in(" + str + ") ";
                dt = ObjFunction.GetDataView(sqlQuery).Table;


                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBill.Rows.Add();
                    for (int i = 0; i < dgBill.Columns.Count - 3; i++)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dgBill.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                            dgBill.Rows[j].Cells[ColIndex.MRP].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.MRP].ToString()).ToString("0.00");
                            dgBill.Rows[j].Cells[ColIndex.Rate].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.Rate].ToString()).ToString("0.00");
                            dgBill.Rows[j].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndex.PurRate].ToString()).ToString("0.00");
                        }
                    }
                }
                BtnUpdate.Visible = false;
                dgBill.Columns[ColIndex.ActualStock].Visible = true;
                dtBillCollect = new DataTablesCollection();
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    dtBillCollect.Add(ObjFunction.GetDataView("Exec GetStockGodownDetails " + dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString() + "," + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value + "").Table);
                }
                dgBill.Rows.Add();
                CalculateTotal();
            }
            dtpBillDate.Enabled = false;
            BtnCancel.Visible = false;
            btnDelete.Visible = false;
            BtnSearch.Visible = false;
           
            BtnExit.Location = new Point(BtnCancel.Location.X, BtnCancel.Location.Y);
            dgBill.CurrentCell = dgBill[ColIndex.ActualStock, 0];
            dgBill.Focus();
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
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dgBill.Enabled = false;


                InitDelTable();

                ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)); //deepak
                initItemQuery();

                txtInvNo.Enabled = false;
                dtpBillDate.Enabled = false;
                InitControls();

                dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VoucherType + "").Table;

                //dgBill.Columns[ColIndex.Stock].Visible = false;
                dgBill.Columns[ColIndex.Amount].Visible = false;
                //dgBill.Columns[ColIndex.ActualStock].Visible = false;
                dgBill.Columns[ColIndex.MRP].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                new GridSearch(dgItemList, 1);
                formatPics();

                if (dtSearch.Rows.Count > 0)
                {
                    //if (Sales.RequestSalesNo == 0)
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    //else
                    //    ID = Sales.RequestSalesNo;
                    if (ScheduleType == 2)
                    {
                        FillSchedulType();
                        setDisplay(false);
                    }
                    else
                    {
                        FillControls();
                        setDisplay(true);
                    }
                    SetNavigation();
                }

               
                BtnNew.Focus();
                KeyDownFormat(this.Controls);


                //for (int i = 0; i < dgBill.Columns.Count; i++)
                //    dgBill.Columns[i].Visible = true;

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

                string sqlQuery = " SELECT 0 AS Sr, mItemMaster.ItemName, TStock.Quantity,0 as Stock,0 as ActualStock, MUOM.UOMName, TStock.Rate, TStock.Amount,MStockBarcode.Barcode, " +
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
                tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(ID);
                VoucherUserNo = Convert.ToInt64(tVoucherEntry.VoucherUserNo);

                txtInvNo.Text = tVoucherEntry.VoucherUserNo.ToString();
                dtpBillDate.Value = tVoucherEntry.VoucherDate;

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
                string sqlQuery = " SELECT    SrNo, ItemName, (CASE WHEN trncode = 1 THEN - Quantity ELSE Quantity END) AS Quantity, Stock, (CASE WHEN trncode = 1 THEN (Stock - Quantity) "+
                                  " ELSE Stock + Quantity END) AS ActualStock, UOMName, MRP, Rate, PurRate, NetRate, DiscPercentage, DiscAmount, DiscRupees, DiscPercentage2, DiscAmount2, "+
                                  " DiscRupees2, NetAmount, Amount, Barcode, PkStockTrnNo, PkStockBarcodeNo, PkVoucherNo, ItemNo, UOMNo, PkRateSettingNo, PkItemTaxInfo, StockFactor, "+
                                  " ActualQty, MKTQuantity, TaxPercentage, TaxAmount, FkStockGodownNo, CompanyNo "+
                                  " FROM   (SELECT     0 AS SrNo, "+
                                  " (SELECT     ItemName "+
                                  " FROM   dbo.MStockItems_V(NULL, TStock_1.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TStock_1.Quantity, "+
                                  " (SELECT     ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) "+
                                  " + ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0) AS Expr1 "+
                                  " FROM  TStock "+
                                  " WHERE  (FkRateSettingNo IN "+
                                  " (SELECT     PkSrNo "+
                                  " FROM    MRateSetting AS MRateSetting_1 "+
                                  " WHERE   (ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (ItemNo = mItemMaster.ItemNo) AND "+
                                  " (PkStockTrnNo NOT IN "+
                                  " (SELECT   TStock_1.PkStockTrnNo "+
                                  " FROM     TVoucherEntry INNER JOIN "+
                                  " TStock AS TStock_2 ON TVoucherEntry.PkVoucherNo = TStock_2.FKVoucherNo "+
                                  " WHERE      (TVoucherEntry.IsCancel = 'True') AND (TStock_2.ItemNo = mItemMaster.ItemNo)))) AS Stock, 0 AS ActualStock, MUOM.UOMName, "+
                                  " MRateSetting.MRP, TStock_1.Rate, MRateSetting.PurRate, TStock_1.NetRate, TStock_1.DiscPercentage, TStock_1.DiscAmount, TStock_1.DiscRupees, "+
                                  " TStock_1.DiscPercentage2, TStock_1.DiscAmount2, TStock_1.DiscRupees2, TStock_1.NetAmount, TStock_1.Amount, MStockBarcode.Barcode, "+
                                  " TStock_1.PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, TVoucherEntry_1.PkVoucherNo, TStock_1.ItemNo, TStock_1.FkUomNo AS UOMNo, "+
                                  " TStock_1.FkRateSettingNo AS PkRateSettingNo, TStock_1.FkItemTaxInfo AS PkItemTaxInfo, MRateSetting.StockConversion AS StockFactor, "+ 
                                  " TStock_1.BilledQuantity AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, TStock_1.TaxPercentage, TStock_1.TaxAmount, 0 AS FkStockGodownNo, "+ 
                                  " TVoucherEntry_1.CompanyNo, TStock_1.TrnCode "+
                                  " FROM   TStock AS TStock_1 INNER JOIN "+
                                  " TVoucherEntry AS TVoucherEntry_1 ON TStock_1.FKVoucherNo = TVoucherEntry_1.PkVoucherNo INNER JOIN "+
                                  " MStockItems ON TStock_1.ItemNo = mItemMaster.ItemNo INNER JOIN "+
                                  " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN "+
                                  " MRateSetting ON TStock_1.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN "+
                                  " MUOM ON MRateSetting.UOMNo = MUOM.UOMNo "+
                                  " WHERE      (TVoucherEntry_1.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry_1.PkVoucherNo = " + ID + ")) AS Tab1";
                //    " SELECT  0 AS SrNo,(SELECT   ItemName   FROM      dbo.MStockItems_V(NULL, TStock_1.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TStock_1.Quantity, " +
                //                 " (SELECT  ISNULL((CASE WHEN trncode = 1 THEN  (TStock_1.quantity + " +
                //                 " (SELECT  SUM(ISNULL((CASE WHEN TS .trncode = 1 THEN TS .quantity ELSE - TS .quantity END), 0)) AS Expr1 " +
                //                 " FROM     TStock AS TS " +
                //                 " WHERE    (FKVoucherNo < TVoucherEntry.PkVoucherNo) AND (ItemNo = TStock_1.ItemNo))) " +
                //                 " ELSE - (TStock_1.quantity + " +
                //                 " (SELECT  SUM(ISNULL((CASE WHEN TS .trncode = 1 THEN TS .quantity ELSE - TS .quantity END), 0)) AS Expr1 " +
                //                 " FROM     TStock AS TS " +
                //                 " WHERE    (FKVoucherNo < TVoucherEntry.PkVoucherNo) AND (ItemNo = TStock_1.ItemNo))) END), 0) AS Expr1) AS Stock, " +
                //                 " ISNULL((SELECT  SUM(ISNULL((CASE WHEN TS .trncode = 1 THEN TS .quantity ELSE - TS .quantity END), 0)) AS Expr1 " +
                //                 " FROM     TStock AS TS " +
                //                 " WHERE    (FKVoucherNo < TVoucherEntry.PkVoucherNo) AND (ItemNo = TStock_1.ItemNo)),0) AS ActualStock, MUOM.UOMName, MRateSetting.MRP, TStock_1.Rate, " +
                //                 " MRateSetting.PurRate, TStock_1.NetRate, TStock_1.DiscPercentage, TStock_1.DiscAmount, TStock_1.DiscRupees, TStock_1.DiscPercentage2, TStock_1.DiscAmount2, " +
                //                 " TStock_1.DiscRupees2, TStock_1.NetAmount, TStock_1.Amount, MStockBarcode.Barcode, TStock_1.PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, " +
                //                 " TVoucherEntry.PkVoucherNo, TStock_1.ItemNo, TStock_1.FkUomNo AS UOMNo, TStock_1.FkRateSettingNo AS PkRateSettingNo, " +
                //                 " TStock_1.FkItemTaxInfo AS PkItemTaxInfo, MRateSetting.StockConversion AS StockFactor, TStock_1.BilledQuantity AS ActualQty, " +
                //                 " MRateSetting.MKTQty AS MKTQuantity, TStock_1.TaxPercentage, TStock_1.TaxAmount, 0 AS FkStockGodownNo, TVoucherEntry.CompanyNo " +
                //                 " FROM         TStock AS TStock_1 INNER JOIN " +
                //                 " TVoucherEntry ON TStock_1.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                //                 " MStockItems ON TStock_1.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                //                 " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN " +
                //                 " MRateSetting ON TStock_1.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN " +
                //                 " MUOM ON MRateSetting.UOMNo = MUOM.UOMNo " +
                //                 " WHERE     (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + ID + ")";
                //"SELECT     0 AS SrNo, (Select ItemName from dbo.MStockItems_V(null, Tstock.ItemNo,NULL,NULL,NULL,NULL,NULL)) AS ItemName , TStock.Quantity, (SELECT ISNull((CASE WHEN trncode = 1 THEN TStock.quantity ELSE - TStock.quantity END),0)) as Stock,0 as ActualStock ,MUOM.UOMName, MRateSetting.MRP, TStock.Rate, MRateSetting.PurRate, TStock.NetRate, " +
                //          " TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, TStock.NetAmount, " +
                //          " TStock.Amount, MStockBarcode.Barcode, TStock.PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, TVoucherEntry.PkVoucherNo, TStock.ItemNo, " +
                //          " TStock.FkUomNo AS UOMNo,  TStock.FkRateSettingNo AS PkRateSettingNo, TStock.FkItemTaxInfo AS PkItemTaxInfo, " +
                //          " MRateSetting.StockConversion AS StockFactor, TStock.BilledQuantity AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, tStock.SGSTPercentage, tStock.SGSTAmount, " +
                //          " 0 AS FkStockGodownNo, TVoucherEntry.CompanyNo " +
                //          " FROM TStock INNER JOIN TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MStockItems ON TStock.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                //          " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                //          " INNER JOIN  MUOM ON MRateSetting.UOMNo = MUOM.UOMNo " +
                //          " WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + ID + ") ";
                dt = ObjFunction.GetDataView(sqlQuery).Table;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBill.Rows.Add();
                    for (int i = 0; i < dgBill.Columns.Count - 3; i++)
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
                //dgBill.Columns[ColIndex.Stock].Visible = true;
                //dgBill.Columns[ColIndex.ActualStock].Visible = true;
                BtnUpdate.Visible = false;

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
                double subTotal = 0, totalDisc = 0, totalTax = 0; //, totalChrg = 0, TotFinal = 0;
                double subStock = 0, subDiff = 0, SubActul = 0;
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

                            double Amount = Math.Abs(Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value))).ToString("0.00")));
                            double DiscAmt = Convert.ToDouble(((Amount * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value)) / 100).ToString("0.00"));
                            DiscAmt += Convert.ToDouble((((Amount - DiscAmt) * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.00"));
                            DiscAmt += Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value);

                            double tAmount = Amount - DiscAmt;
                            double TaxPerce = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value);
                            double TaxAmt = Convert.ToDouble(((tAmount * TaxPerce) / (100 + TaxPerce)).ToString("0.00"));
                            totalTax += TaxAmt;
                            double ttRate = (tAmount - TaxAmt) / Math.Abs(Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value));

                            double StockAmt = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Stock].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MRP].Value)))).ToString("0.00"));
                            double DiffAmt = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MRP].Value)))).ToString("0.00"));
                            double ActualAmt = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.ActualStock].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MRP].Value)))).ToString("0.00"));


                            dgBill.Rows[i].Cells[ColIndex.StockAmt].Value = StockAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiffAmt].Value = DiffAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.ActualAmt].Value = ActualAmt.ToString("0.00");


                            dgBill.Rows[i].Cells[ColIndex.Amount].Value = tAmount.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value = DiscAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value = TaxAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = Math.Abs((ttRate * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value))).ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.ActualQty].Value = (Math.Abs(Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.StockFactor].Value)));

                            subTotal = subTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetAmt].Value);
                            totalDisc = totalDisc + DiscAmt;// Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value);
                            subStock = subStock + StockAmt;
                            subDiff = subDiff + DiffAmt;
                            SubActul = SubActul + ActualAmt;
                        }
                    }
                    subTotal = Convert.ToDouble(subTotal.ToString("0.00"));// Math.Round(subTotal, 00);

                }
                txtStockAmt.Text = subStock.ToString("0.00");
                txtDiffAmt.Text = subDiff.ToString("0.00");
                txtActualStockAmt.Text = SubActul.ToString("0.00");

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

        public bool Validation()
        {

            bool flag = true;

            for (int i = 0; i < dgBill.Rows.Count - 1; i++)
            {
                dgBill.Rows[i].Cells[ColIndex.ActualStock].ErrorText = "";
                if (dgBill.Rows[i].Cells[ColIndex.ActualStock].FormattedValue.ToString() == "" || dgBill.Rows[i].Cells[ColIndex.ActualStock].FormattedValue.ToString() == "0")
                {
                    flag = false;
                    dgBill.Rows[i].Cells[ColIndex.ActualStock].ErrorText = "Enter Valid Qty";
                    dgBill.CurrentCell = dgBill.Rows[i].Cells[ColIndex.ActualStock];
                    dgBill.Focus();
                    break;
                }
            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //double debit = 0;
            //long temp = 0;
            try
            {
                int cnt = 0;
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                if (Validation() == true)
                {
                    CalculateTotal();
                    dbTVoucherEntry = new DBTVaucherEntry();
                    //DeleteStockGodown();
                    DeleteValues();//Delete Old Values
                    int VoucherSrNo = 1;
                    //Voucher Header Entry 
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = ID;
                    tVoucherEntry.VoucherTypeCode = VoucherType;
                    tVoucherEntry.VoucherUserNo = VoucherUserNo;
                    tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                    tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
                    tVoucherEntry.Narration = "Physical Stock Entry";
                    tVoucherEntry.RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType));
                    tVoucherEntry.Reference = "";
                    tVoucherEntry.ChequeNo = 0;
                    tVoucherEntry.ClearingDate = dtpBillDate.Value;
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = 0;
                    tVoucherEntry.ChallanNo = "";
                    tVoucherEntry.Remark = "";
                    tVoucherEntry.MacNo = DBGetVal.MacNo;
                    tVoucherEntry.PayTypeNo = 0;
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
                        if (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) != 0)
                        {
                            cnt++;
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
                            if (Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()) > 0)
                                tStock.TrnCode = 2;
                            else
                                tStock.TrnCode = 1;

                            tStock.Quantity = Math.Abs(Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()));
                            tStock.BilledQuantity = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString()) * Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString());
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
                    }
                    if (cnt > 0)
                    {
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
                            ID = tempID;// ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                            //string sql = "Exec SetTVoucherEntryCompany " + ID + "," + VoucherType + "";
                            //ObjTrans.Execute(sql, CommonFunctions.ConStr);
                            SetNavigation();
                            setDisplay(true);
                            ObjFunction.LockButtons(true, this.Controls);
                            ObjFunction.LockControls(false, this.Controls);
                            dgBill.Enabled = false;
                            //btnNew_Click(BtnNew, e);
                            FillControls();
                            BtnNew.Focus();
                            BtnUpdate.Visible = false;
                        }
                        else
                        {
                            OMMessageBox.Show(MsgName + " Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        OMMessageBox.Show(MsgName + " Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
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
            dgBill.CurrentCell = dgBill.Rows[RowIndex].Cells[ColIndex];
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
                    long[] BarcodeNo; long[] ItemNo;
                    SearchBarcode(dgBill.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);
                    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0;

                    if (ItemNo.Length == 0 || BarcodeNo.Length == 0)
                    {
                        dgBill.CurrentCell.Value = null;
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = "0";
                        DisplayMessage("Barcode Not Found");
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                        //OMMessageBox.Show("Barcode Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                        //MovetoNext move2n = new MovetoNext(m2n);
                        //BeginInvoke(move2n, new object[] { dgBill.CurrentRow.Index, ColIndex.ItemName, dgBill });
                        //dgBill.Focus();
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
                                //dgBill.Rows[rwindex].Cells[ColIndex.ActualStock].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.ActualStock].Value) + 1;
                                dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                                dgBill.CurrentCell = dgBill[ColIndex.ActualStock, rwindex];
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

            //string sql = "Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";
            string sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                " WHERE ((MStockBarcode.Barcode = '" + strBarcode + "') or (MStockItems.ShortCode = '" + strBarcode + "')) AND (MStockItems.IsActive = 'true') AND (MRateSetting.IsActive='true')";
            dt = ObjFunction.GetDataView(sql).Table;
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
                //sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                //" WHERE (MStockItems.ShortCode = '" + strBarcode + "') AND (MStockItems.IsActive = 'true')";
                //dt = ObjFunction.GetDataView(sql).Table;
                //BarcodeNo = new long[dt.Rows.Count];
                //ItemNo = new long[dt.Rows.Count];
                //if (dt.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        BarcodeNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                //        ItemNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[1].ToString());
                //        dgBill.CurrentCell.Value = dt.Rows[i].ItemArray[2].ToString();
                //    }

                //}

            }
        }



        private void Desc_MoveNext(long ItemNo, long BarcodeNo)
        {
            try
            {
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = ItemNo;
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;

                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo = " + ItemNo).Table;
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
                //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = ObjQry.ReturnString("Select ItemName from MStockItems_V(NULL,NULL) where ItemNo = " + ItemNo,CommonFunctions.ConStr);

                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();

                if (BarcodeNo != 0)
                    dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.Stock].Value = ObjQry.ReturnDouble("Select  IsNull( (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock     WHERE      (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (ItemNo =" + ItemNo + "))) AND (ItemNo = " + ItemNo + ") And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = " + ItemNo + "))),0) AS Stock", CommonFunctions.ConStr);



                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                // Qty_MoveNext();
                ActualStock_Start();


                //BindGrid();
                //CalculateTotal();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void ActualStock_Start()
        {
            try
            {
                rowQtyIndex = dgBill.CurrentCell.RowIndex;


                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { rowQtyIndex, ColIndex.ActualStock });

                //UOM_Start();
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

        private void ActualStock_MoveNext()
        {
            try
            {
                rowQtyIndex = dgBill.CurrentCell.RowIndex;
                //dgBill.Rows[rowQtyIndex].Cells[ColIndex.Quantity].Value = Math.Abs(Convert.ToDouble(dgBill.Rows[rowQtyIndex].Cells[ColIndex.Stock].Value)) - Convert.ToDouble(dgBill.Rows[rowQtyIndex].Cells[ColIndex.ActualStock].Value);
                dgBill.Rows[rowQtyIndex].Cells[ColIndex.Quantity].Value = (Convert.ToDouble(dgBill.Rows[rowQtyIndex].Cells[ColIndex.Stock].Value)) - Convert.ToDouble(dgBill.Rows[rowQtyIndex].Cells[ColIndex.ActualStock].Value);
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
                dgBill.CurrentCell = dgBill[ColIndex.ActualStock, row];

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
                string str; //, str2;
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

                    if (lstRate.Items.Count == 1)
                    {
                        lstRate.SelectedIndex = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = lstRate.Text;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;
                        DataTable dtRt = ObjFunction.GetDataView("Select MRP,PurRate From MRateSetting where PkSrNo=" + lstRate.SelectedValue + "").Table;
                        if (dtRt.Rows.Count > 0)
                        {
                            dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[0].ToString()).ToString("0.00");
                            dgBill.Rows[RowIndex].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dtRt.Rows[0].ItemArray[1].ToString()).ToString("0.00");
                        }

                        //MovetoNext move2n = new MovetoNext(m2n);
                        //BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                        dgBill.CurrentCell = dgBill[ColIndex.ActualStock, RowIndex];
                        dgBill.Focus();
                        //BindGrid(dgBill.CurrentRow.Index);


                        Rate_MoveNext();

                    }
                    else if (lstRate.Items.Count > 1)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                        dgBill.CurrentCell = dgBill[ColIndex.ActualStock, RowIndex];
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
                    //MovetoNext move2n = new MovetoNext(m2n);
                    //BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate });
                    //dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                    dgBill.Focus();
                    //BindGrid(dgBill.CurrentRow.Index);


                    Rate_MoveNext();

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
            try
            {
                CalculateTotal();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void delete_row()
        {
            //bool flag;
            try
            {
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value != null)
                {

                    if (OMMessageBox.Show("Are you sure want to Cancel this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null)
                        {
                            long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                            long StockGodownNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FkStockGodownNo].Value);
                            if (PKStockTrnNo != 0)
                            {
                                DeleteDtls(1, PKStockTrnNo, StockGodownNo);
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                                dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                            }

                            else
                            {
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                                dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                            }
                            CalculateTotal();
                            dtBillCollect.RemoveAt(dgBill.CurrentCell.RowIndex);
                        }
                        if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                        {
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            dgBill.Rows.Add();
                        }
                        else
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        //else
                        //{
                        //    dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        //    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        //}

                        CalculateTotal();
                    }
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
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
                {

                    if (dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "" && ObjFunction.CheckValidAmount(dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == true)
                    {
                        ActualStock_MoveNext();
                    }
                    else
                    {
                        dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Enter Valid Actual Stocks";
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
                    e.SuppressKeyPress = true;
                    dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell.Value = "";
                        Desc_Start();
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentCell.Value == null) dgBill.CurrentCell.Value = "1";
                        Qty_MoveNext();
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
                    {

                        if (dgBill.CurrentRow.Cells[ColIndex.ActualStock].FormattedValue.ToString() != "" && ObjFunction.CheckValidAmount(dgBill.CurrentRow.Cells[ColIndex.ActualStock].FormattedValue.ToString()) == true)
                        {
                            ActualStock_MoveNext();
                        }
                        else
                        {
                            dgBill.CurrentRow.Cells[ColIndex.ActualStock].ErrorText = "Enter Valid Actual Stocks";
                        }
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
                if (col == ColIndex.ActualStock && row >= 0)
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
                                {
                                    dgBill[ColIndex.Amount, dgBill.CurrentCell.RowIndex].Value = "0.00";
                                    dgBill[ColIndex.StockAmt, dgBill.CurrentCell.RowIndex].Value = "0.00";
                                    dgBill[ColIndex.DiffAmt, dgBill.CurrentCell.RowIndex].Value = "0.00";
                                    dgBill[ColIndex.ActualAmt, dgBill.CurrentCell.RowIndex].Value = "0.00";
                                }
                                else
                                    CalculateTotal();
                                dgBill.Focus();

                                dgBill.CurrentCell = dgBill[ColIndex.ActualStock, row];


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
                //if (dgBill.CurrentCell.ColumnIndex == 2)
                //{
                //    TextBox txt = (TextBox)e.Control;
                //    txt.KeyDown += new KeyEventHandler(txtQuantity_KeyDown);
                //}
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
                    //txt1.TextChanged -= new EventHandler(txtQuantity_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
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
                    }

                    dgBill.Rows[RowIndex].Cells[ColIndex.MKTQuantity].Value = Convert.ToInt64(dt.Rows[0][1].ToString());

                    StockConFactor = Convert.ToDouble(dt.Rows[0][2].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.StockFactor].Value = StockConFactor;

                    dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo].Value = Convert.ToInt64(dt.Rows[0][3].ToString());

                    if (dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.PkVoucherNo].Value = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.SGSTPercentage].Value = Convert.ToDouble(dt.Rows[0][4].ToString()); ;
                    if (dgBill.Rows[RowIndex].Cells[ColIndex.FkStockGodownNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.FkStockGodownNo].Value = "0";
                    if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1 && dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
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
                        dtBillCollect.RemoveAt(RowIndex);
                        dtBillCollect.Insert(RowIndex, dtStk);
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = true;
            txtSearch.Text = ""; txtSearch.Enabled = true;
            txtSearch.Focus();
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
            dtDelete.Columns.Add();
            dtDelete.Columns.Add();
            dtDelete.Columns.Add();
        }

        public void DeleteDtls(int Type, long PkNo, long StockGodownNo)
        {
            DataRow dr = null;
            dr = dtDelete.NewRow();
            dr[0] = Type;
            dr[1] = PkNo;
            dr[2] = StockGodownNo;
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
                        tStockGodown.PKStockGodownNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[2]);
                        dbTVoucherEntry.DeleteTStockGodown(tStockGodown);
                    }
                }
                dtDelete.Rows.Clear();
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                dtBillCollect = new DataTablesCollection();
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                dgBill.Enabled = true;
                InitControls();

                dgBill.Columns[ColIndex.Stock].Visible = true;
                dgBill.Columns[ColIndex.ActualStock].Visible = true;
                txtInvNo.Text = ObjQry.ReturnLong("Select IsNull(Max(VoucherUserNo),0)+1 From TVoucherEntry Where VoucherTypeCode=" + VoucherType + " ", CommonFunctions.ConStr).ToString();
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
                dgBill.Columns[ColIndex.Stock].Visible = true;
                dgBill.Columns[ColIndex.ActualStock].Visible = true;
                dgBill.Enabled = true;
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
                NavigationDisplay(5);

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dgBill.Enabled = false;
                DisplayList(false);
                BtnNew.Focus();
                BtnUpdate.Visible = false;
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

                    setDisplay(true);
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
                        //string  ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName,MStockItems.ItemNameLang, MRateSetting." + ObjFunction.GetComboValueString(cmbRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
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

                            ItemList += " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                                "IsNull( (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock     WHERE      (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))),0) AS Stock , '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
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
                            dgItemList.CurrentCell = dgItemList[2, 0];
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
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
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
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
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
            public static int Stock = 3;
            public static int ActualStock = 4;
            public static int UOM = 5;
            public static int MRP = 6;
            public static int Rate = 7;
            public static int PurRate = 8;
            public static int NetRate = 9;
            public static int DiscPercentage = 10;
            public static int DiscAmount = 11;
            public static int DiscRupees = 12;
            public static int DiscPercentage2 = 13;
            public static int DiscAmount2 = 14;
            public static int DiscRupees2 = 15;
            public static int NetAmt = 16;
            public static int Amount = 17;
            public static int Barcode = 18;
            public static int PkStockTrnNo = 19;
            public static int PkBarCodeNo = 20;
            public static int PkVoucherNo = 21;
            public static int ItemNo = 22;
            public static int UOMNo = 23;
            public static int PkRateSettingNo = 24;
            public static int PkItemTaxInfo = 25;
            public static int StockFactor = 26;
            public static int ActualQty = 27;
            public static int MKTQuantity = 28;
            public static int SGSTPercentage = 29;
            public static int SGSTAmount = 30;
            public static int FkStockGodownNo = 31;
            public static int StockCompanyNo = 32;
            public static int StockAmt = 33;
            public static int DiffAmt = 34;
            public static int ActualAmt = 35;
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
                    if (ItemExist(i, Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value), out rwindex) == true)
                    {
                        //pnlItemName.Visible = false;
                        //dgBill.Focus();
                        //dgBill.CurrentCell = dgBill[ColIndex.ActualStock, rwindex];
                        //dgBill.Rows[rwindex].Cells[ColIndex.ActualStock].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.ActualStock].Value) + 1;
                        //if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                        //dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                        //dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                        //dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));

                        pnlItemName.Visible = false;
                        if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                        e.SuppressKeyPress = true;
                        dgBill.Focus();
                        dgBill.CurrentCell = dgBill[ColIndex.ActualStock, rwindex];


                    }
                    else
                    {
                        dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                        dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[2].Value).ToString("0.00");//lstRate.Text;
                        dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);//lstRate.SelectedValue;
                        dgBill.CurrentRow.Cells[ColIndex.MRP].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value).ToString("0.00");
                        dgBill.CurrentRow.Cells[ColIndex.PurRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[13].Value).ToString("0.00");
                        dgBill.CurrentRow.Cells[ColIndex.Stock].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");
                        pnlItemName.Visible = false;
                        Desc_MoveNext(i, 0);
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



        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    dgBill.Focus();
                    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
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

        public bool ItemExist(long ItNo, long RateSettingNo, out int rowIndex)
        {
            rowIndex = -1;
            try
            {
               
                bool flag = false;
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
