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
    public partial class PhysicalStockEntryAE : Form
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
        long ID = 0, VoucherUserNo = 0;
        DataTable dtSearch = new DataTable();
        DataTable dtMain;//, dtFill;
        int cntRow, Rtype = 0;
        int iItemNameStartIndex = 3, ItemType = 0, rowQtyIndex;
        //bool BarFlag = true;
        string[] strItemQuery, strItemQuery_last;
        long ItemNameType = 0;
        string Param1Value = "", Param2Value = "", str = "";//strUom
        long VchNo = 0;
        public PhysicalStockEntryAE()
        {
            InitializeComponent();
        }

        public PhysicalStockEntryAE(DataTable dt, int Rtype)
        {
            InitializeComponent();
            this.Rtype = Rtype;
            dtMain = dt;
        }

        private void PhysicalStockEntryAE_Load(object sender, EventArgs e)
        {
            try
            {
                if (DBGetVal.KachhaFirm == false)
                {
                    VchNo = VchType.PhysicalStock;
                }
                else
                {
                    VchNo = VchType.DPhysicalStock;

                }
                    //ObjTrans.Execute("exec StockUpdateAll", CommonFunctions.ConStr);
                    ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType));
                initItemQuery();
                ObjFunction.FillCombo(cmbGodown, "SELECT GodownNo, GodownName FROM MGodown WHERE (IsActive = 'true') And GodownNo=2 ");
                cmbGodown.SelectedValue = "0";
                //dtSearch = ObjFunction.GetDataView("Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + VchType.PhysicalStock + "").Table;
                dtSearch = ObjFunction.GetDataView("SELECT DISTINCT TVoucherEntry.PkVoucherNo, ISNULL(TStockGodown.GodownNo, 0) AS GodownNo "+
                                                   " FROM  TStockGodown INNER JOIN "+
                                                   " TStock ON TStockGodown.FKStockTrnNo = TStock.PkStockTrnNo RIGHT OUTER JOIN "+
                                                   " TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo "+
                                                   " WHERE   (TVoucherEntry.VoucherTypeCode = " + VchNo + ") AND TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(dtpBillDate.Text).ToString(Format.DDMMMYYYY) + "' " +
                                                   " ORDER BY TVoucherEntry.PkVoucherNo").Table;

                dtpBillDate.Value = DBGetVal.ServerTime.Date;

                if (ObjQry.ReturnLong("Select isNull(PkVoucherNo,0) from TVoucherEntry where VoucherTypeCode=" + VchNo + " and VoucherDate='" + Convert.ToDateTime(dtpBillDate.Text) + "'", CommonFunctions.ConStr) == 0)
                {
                    ID = EntryTvoucherEntry();
                    DataRow dr;
                    dr = dtSearch.NewRow();
                    dr[0] = ID;//ObjQry.ReturnLong("Select isNull(PkVoucherNo,0) from TVoucherEntry where VoucherTypeCode=" + VchType.PhysicalStock + " and VoucherDate='" + Convert.ToDateTime(dtpBillDate.Text) + "'", CommonFunctions.ConStr);
                    dr[1] = 0;
                    dtSearch.Rows.Add(dr);
                }
                else
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                
                SetNavigation();
                if (Rtype != 0)
                {
                    btnLast.Visible = false;
                    btnPrev.Visible = false;
                    btnNext.Visible = false;
                    btnFirst.Visible = false;
                    cmbGodown.Enabled = false;
                    if (cmbGodown.Items.Count > 1)
                    {
                        cmbGodown.SelectedIndex = 1;
                        FillGrid();
                    }
                }
                else
                    setDisplay(false);
                new GridSearch(dgItemList, 1);
                formatPics();
                cmbGodown.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private long EntryTvoucherEntry()
        {
            try
            {
                dbTVoucherEntry = new DBTVaucherEntry();
                //DeleteStockGodown();

                //int VoucherSrNo = 1;
                //Voucher Header Entry 
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;
                tVoucherEntry.VoucherTypeCode = VchNo; //VchType.PhysicalStock;
                tVoucherEntry.VoucherUserNo = VoucherUserNo;
                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
                tVoucherEntry.Narration = "Physical Stock Entry";
                tVoucherEntry.RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType));
                tVoucherEntry.Reference = "";
                tVoucherEntry.ChequeNo = 0;
                tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;
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
                long tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                return tempID;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return 0;
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

        private void FillGrid()
        {

            try
            {
                DataTable dtFill = new DataTable();
         
                str = "";
 
                {
                    string sqlQuery = "";
                    dgBill.Rows.Clear();
                    str = str.Equals("") ? "0" : str;
                    string vch = "";
                    if (DBGetVal.KachhaFirm == false)
                    {
                        vch = "where vouchertypecode not in (108,112,113,115,109,136)";
                    }
                    else {
                        vch = "where vouchertypecode not in (8,12,13,15,9,36)";

                    }
                    sqlQuery = "Delete from MStockItemBalanceTemp where UserID = " + DBGetVal.UserID + "";
                    ObjTrans.Execute(sqlQuery, CommonFunctions.ConStr);

                    sqlQuery =" INSERT INTO MStockItemBalanceTemp (ItemNo, MRP,GodownNo,UserID,PrevStock,CurrentStock) "+
                                " Select TStock.ItemNo, MRateSetting.MRP,2 as GodownNo, "+DBGetVal.UserID+" as UserID, "+
                                " SUM(CASE WHEN TStock.FKVoucherNo < " + ID + " THEN (CASE WHEN TStock.TrnCode = 1 THEN (TStock.BilledQuantity) ELSE (TStock.BilledQuantity)*-1 END) else 0 end) as PrevStock, " +
                                " SUM(CASE WHEN TStock.TrnCode = 1 THEN (TStock.BilledQuantity) ELSE (TStock.BilledQuantity)*-1 END) as CurrentStock "+
                                " from TStock INNER JOIN "+
                                " TVoucherEntry ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo AND TVoucherEntry.IsCancel = 'False'  INNER JOIN "+
                                " MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo left JOIN "+
                                " TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo "+
                                "  " + vch + " group by TStock.ItemNo, MRateSetting.MRP, TStockGodown.GodownNo";
                    ObjTrans.Execute(sqlQuery, CommonFunctions.ConStr);

                    sqlQuery = " SELECT DISTINCT 0 AS SrNo,MItemGroup.ItemGroupName+' '+mItemMaster.ItemName as ItemName, " +
                           
                               " Tab1.MRP, MUOM.UOMName, " +

                               " MSB.PrevStock AS Stock, " +
                               //
                               " MSB.CurrentStock AS StockNow, " + 
                               //
                               " 0 AS ActualStock, 0 AS Quantity, " +
                               " Tab1.ASaleRate AS Rate, Tab1.PurRate, 0 AS NetRate, 0 AS DiscPercentage, 0 AS DiscAmount, 0 AS DiscRupees, 0 AS DiscPercentage2, 0 AS DiscAmount2, " +
                               " 0 AS DiscRupees2, 0 AS NetAmount, 0 AS Amount, mItemMaster.Barcode, 0 AS PkStockTrnNo, mItemMaster.itemno as PkStockBarcodeNo, 0 AS PkVoucherNo, " +
                               " Tab1.ItemNo, mItemMaster.UOML AS UOMNo, Tab1.PkSrNo AS PkRateSettingNo, 0 AS PkItemTaxInfo, Tab1.StockConversion AS StockFactor, " +
                               " 0 AS ActualQty, Tab1.MKTQty AS MKTQuantity, 0 AS TaxPercentage, 0 AS TaxAmount, 0 AS FkStockGodownNo, mItemMaster.CompanyNo, 1 AS Type " +
                               //From
                               " FROM  " +
                               //Tab1
                               " (SELECT DISTINCT " +
                               " TStock_1.ItemNo, GetItemRateAll_1.MRP, GetItemRateAll_1.PkSrNo, GetItemRateAll_1.UOMNo, GetItemRateAll_1.ASaleRate, GetItemRateAll_1.PurRate, " +
                               " GetItemRateAll_1.StockConversion, GetItemRateAll_1.MKTQty " +
                               " FROM    TStock AS TStock_1 INNER JOIN " +
                               " MRateSetting AS GetItemRateAll_1 ON TStock_1.FkRateSettingNo = GetItemRateAll_1.PkSrNo INNER JOIN " +
                               " TStockGodown AS TStockGodown_3 ON TStock_1.PkStockTrnNo = TStockGodown_3.FKStockTrnNo " +
                               " WHERE      (TStock_1.FKVoucherNo = " + ID + ") AND (TStockGodown_3.GodownNo = " + ObjFunction.GetComboValue(cmbGodown) + ") " +
                               " UNION " +
                               " SELECT DISTINCT " +
                               " MStockCountSchedule.ItemNo, GetItemRateAll_1.MRP, GetItemRateAll_1.PkSrNo, GetItemRateAll_1.UOMNo, GetItemRateAll_1.ASaleRate, " +
                               " GetItemRateAll_1.PurRate, GetItemRateAll_1.StockConversion, GetItemRateAll_1.MKTQty " +
                               " FROM         MStockCountSchedule INNER JOIN " +
                               " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL) AS GetItemRateAll_1 ON MStockCountSchedule.ItemNo = GetItemRateAll_1.ItemNo " +
                               " WHERE     (MStockCountSchedule.ItemNo IN (" + str + "))) " +
                               " AS Tab1 INNER JOIN MUOM " + 
                               //tab1
                               " ON MUOM.UOMNo = Tab1.UOMNo INNER JOIN "+
                              // " mItemMaster INNER JOIN " +
                               //  " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo 
                               " mItemMaster ON Tab1.ItemNo = mItemMaster.ItemNo " +
                               " Inner Join MItemGroup ON  MItemGroup.ItemGRoupNo=mItemMaster.Groupno " +
                               " INNER JOIN MStockItemBalanceTemp MSB ON MSB.ItemNO = Tab1.ItemNo AND MSB.MRP = Tab1.MRP " + 
                               " AND MSB.GodownNo = " + ObjFunction.GetComboValue(cmbGodown) + " AND MSB.UserID = " + DBGetVal.UserID + " " +
                               " ORDER BY ItemName";
       
                    dgBill.Rows.Clear();
                    dtFill = ObjFunction.GetDataView(sqlQuery).Table;


                    for (int j = 0; j < dtFill.Rows.Count; j++)
                    {
                        dgBill.Rows.Add();
                        for (int i = 0; i < dgBill.Columns.Count - 3; i++)
                        {
                            if (dtFill.Rows.Count > 0)
                            {
                                dgBill.Rows[j].Cells[i].Value = dtFill.Rows[j].ItemArray[i].ToString();
                                dgBill.Rows[j].Cells[ColIndex.MRP].Value = Convert.ToDouble(dtFill.Rows[j].ItemArray[ColIndex.MRP].ToString()).ToString("0.00");
                                dgBill.Rows[j].Cells[ColIndex.Rate].Value = Convert.ToDouble(dtFill.Rows[j].ItemArray[ColIndex.Rate].ToString()).ToString("0.00");
                                dgBill.Rows[j].Cells[ColIndex.PurRate].Value = Convert.ToDouble(dtFill.Rows[j].ItemArray[ColIndex.PurRate].ToString()).ToString("0.00");
                                if (i == ColIndex.Quantity)
                                {
                                    if (Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.Quantity].Value) != 0)
                                        dgBill.Rows[j].Cells[ColIndex.ActualStock].Value = Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.Stock].Value) - Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.Quantity].Value);
                                    //if (Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.ActualStock].Value) == 0)
                                    //    dgBill.Rows[j].Cells[ColIndex.Quantity].Value = "0.00";

                                }
                            }
                        }
                        dgBill.Rows[j].Cells[ColIndex.ItemName].ReadOnly = true;
                    }

                    dgBill.Columns[ColIndex.ActualStock].Visible = true;

                    dgBill.Rows.Add();
                    dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].ReadOnly = false;
                    dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ActualStock].ReadOnly = true;
                    if (dgBill.Rows.Count > 1)
                        dgBill.CurrentCell = dgBill[ColIndex.ActualStock, 0];
                    else
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, 0];
                    dgBill.Focus();

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
            public static int MRP = 2;
            public static int UOM = 3;
            public static int Stock = 4;
            public static int StockNow = 5;
            public static int ActualStock = 6;
            public static int Quantity = 7;
            public static int Rate = 8;
            public static int PurRate = 9;
            public static int NetRate = 10;
            public static int DiscPercentage = 11;
            public static int DiscAmount = 12;
            public static int DiscRupees = 13;
            public static int DiscPercentage2 = 14;
            public static int DiscAmount2 = 15;
            public static int DiscRupees2 = 16;
            public static int NetAmt = 17;
            public static int Amount = 18;
            public static int Barcode = 19;
            public static int PkStockTrnNo = 20;
            public static int PkBarCodeNo = 21;
            public static int PkVoucherNo = 22;
            public static int ItemNo = 23;
            public static int UOMNo = 24;
            public static int PkRateSettingNo = 25;
            public static int PkItemTaxInfo = 26;
            public static int StockFactor = 27;
            public static int ActualQty = 28;
            public static int MKTQuantity = 29;
            public static int SGSTPercentage = 30;
            public static int SGSTAmount = 31;
            public static int FkStockGodownNo = 32;
            public static int StockCompanyNo = 33;
            public static int Type = 34;
            public static int StockAmt = 35;
            public static int DiffAmt = 36;
            public static int ActualAmt = 37;
            
        }
        #endregion

        public bool Validation()
        {
            try
            {
                bool flag = true;
                if (ObjFunction.GetComboValue(cmbGodown) > 0)
                {
                    for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                    {
                        dgBill.Rows[i].Cells[ColIndex.ActualStock].ErrorText = "";
                        if (dgBill.Rows[i].Cells[ColIndex.ActualStock].FormattedValue.ToString() == "")
                        {
                            flag = false;
                            dgBill.Rows[i].Cells[ColIndex.ActualStock].ErrorText = "Enter Valid Qty";
                            dgBill.CurrentCell = dgBill.Rows[i].Cells[ColIndex.ActualStock];
                            dgBill.Focus();
                            break;
                        }
                    }
                }
                else
                    flag = false;
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
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
                    if (ID != 0)
                    {
                        VoucherUserNo = ObjQry.ReturnLong("Select VoucherUserNo From TVoucherEntry Where PKVoucherNo=" + ID + "", CommonFunctions.ConStr);
                    }
                    else
                        VoucherUserNo = 0;
                    dbTVoucherEntry = new DBTVaucherEntry();
                    //DeleteStockGodown();

                    int VoucherSrNo = 1;
                    //Voucher Header Entry 
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = ID;
                    if (DBGetVal.KachhaFirm == true)
                    { tVoucherEntry.VoucherTypeCode = VchType.DPhysicalStock; ; }
                    else
                    {
                        tVoucherEntry.VoucherTypeCode = VchType.PhysicalStock; ;
                    }
                    //tVoucherEntry.VoucherTypeCode = VchType.PhysicalStock; ;
                    tVoucherEntry.VoucherUserNo = VoucherUserNo;
                    tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                    tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
                    tVoucherEntry.Narration = "Physical Stock Entry";
                    tVoucherEntry.RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType));
                    tVoucherEntry.Reference = "";
                    tVoucherEntry.ChequeNo = 0;
                    tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = 0;
                    tVoucherEntry.ChallanNo = "";
                    tVoucherEntry.Remark = "";
                    tVoucherEntry.MacNo = DBGetVal.MacNo;
                    tVoucherEntry.PayTypeNo = 0;
                    tVoucherEntry.TaxTypeNo = 0;
                    tVoucherEntry.IsCancel = false;
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
                            tStock.BilledQuantity = Math.Abs(Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString())) * Math.Abs(Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString()));
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
                            tStock.FreeQty = 0; 
                            tStock.FreeUOMNo = 1;
                            //tStock.DisplayItemName = "";
                            tStock.CompanyNo = Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString());
                            tStock.DisplayItemName = Convert.ToString(dgBill[ColIndex.ItemName, i].Value.ToString());
                            //tStock.TRWeight = 0;
                            //tStock.GRWeight = 0;
                            tStock.Remarks = "";
                            //tStock.Freight = 0;
                            //tStock.OtherCharges = 0;
                            //tStock.SalesMan = 0;
                            //tStock.PackagingCharges = 0;
                            if (DBGetVal.KachhaFirm == true)
                            { tStock.IType = true; }
                            else {
                                tStock.IType = false ;
                            }
                            dbTVoucherEntry.AddTStock(tStock);

                            //if (Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()) > 0)
                            //{
                            tStockGodown = new TStockGodown();
                            tStockGodown.PKStockGodownNo = Convert.ToInt64(dgBill[ColIndex.FkStockGodownNo, i].Value.ToString());
                            tStockGodown.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                            tStockGodown.GodownNo = ObjFunction.GetComboValue(cmbGodown);
                            tStockGodown.Qty = Math.Abs(Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()));
                            tStockGodown.ActualQty = Math.Abs(Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString())) * Math.Abs(Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString()));//Math.Abs(Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()));
                            tStockGodown.UserID = DBGetVal.UserID;
                            tStockGodown.UserDate = DBGetVal.ServerTime.Date;
                            tStockGodown.CompanyNo = Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString());
                            dbTVoucherEntry.AddTStockGodown(tStockGodown);
                            //}

                        }
                    }
                    if (cnt > 0)
                    {
                        long tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                        if (tempID != 0)
                        {
                            ObjTrans.Execute("exec StockUpdateAll", CommonFunctions.ConStr);
                            string strVChNo = ObjQry.ReturnLong("Select VoucherUserNo From TVoucherEntry Where PKVoucherNo=" + tempID + "", CommonFunctions.ConStr).ToString();
                            //if (ID == 0)
                            OMMessageBox.Show("Physical Stock Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                           ID = tempID;
                          dtSearch = ObjFunction.GetDataView("SELECT DISTINCT TVoucherEntry.PkVoucherNo, TStockGodown.GodownNo " +
                                                   " FROM  TVoucherEntry INNER JOIN " +
                                                   " TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN " +
                                                   " TStockGodown ON TStock.PkStockTrnNo = TStockGodown.FKStockTrnNo " +
                                                   " WHERE  (TVoucherEntry.VoucherTypeCode =" + VchNo  + ") " +
                                                   " ORDER BY TVoucherEntry.PkVoucherNo").Table;
                            FillGrid();
                            SetNavigation();
                            if (Rtype == 0)
                            {
                                setDisplay(false);
                            }



                        }
                        else
                        {
                            OMMessageBox.Show("Physical Stock Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Enter Atleast One Items Physical Stock ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbGodown) > 0)
                {
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = 0;
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value = 0;

                        Desc_Start();
             
                    }
                    else if (e.ColumnIndex == ColIndex.ActualStock)
                    {
                        if (dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                        {
                            dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
                            //dgBill.Rows[e.RowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.StockNow].Value) - Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.ActualStock].Value);
                        }
                        else
                        {
                            if (Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.ActualStock].Value) == 0)
                            {
                                if (OMMessageBox.Show("Are you sure you want to set Zero", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    dgBill.Rows[e.RowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.StockNow].Value) - Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.ActualStock].Value);
                                }
                                else
                                {
                                    dgBill.Rows[e.RowIndex].Cells[ColIndex.ActualStock].Value = 0;
                                    dgBill.Rows[e.RowIndex].Cells[ColIndex.Quantity].Value = 0;
                                }
                            }
                            else
                            {
                                if (Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.ActualStock].Value) != 0)
                                    dgBill.Rows[e.RowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.StockNow].Value) - Convert.ToDouble(dgBill.Rows[e.RowIndex].Cells[ColIndex.ActualStock].Value);
                                else
                                    dgBill.Rows[e.RowIndex].Cells[ColIndex.Quantity].Value = 0;
                            }
                        }
                        if (e.RowIndex < dgBill.Rows.Count - 2)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { e.RowIndex + 1, 6 });
                            //dgBill.CurrentCell = dgBill[ColIndex.ActualStock, dgBill.Rows.Count - 1];
                        }
                        else if (e.RowIndex == dgBill.Rows.Count - 2)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName });
                            //dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        }
                    }
                }
                else
                {
                    dgBill.CurrentCell.Value = "";
                    OMMessageBox.Show("Please Select Godown Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    cmbGodown.Focus();
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
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
                {
                    ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = (e.RowIndex + 1).ToString();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpBillDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                ID = ObjQry.ReturnLong("Select isNull(PkVoucherNo,0) from TVoucherEntry where VoucherTypeCode=" + VchNo + " and VoucherDate='" + Convert.ToDateTime(dtpBillDate.Text) + "'", CommonFunctions.ConStr);
                //FillGrid();
                if (dtpBillDate.Text == DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy"))
                    DisplayALL(true);
                else
                    DisplayALL(false);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DisplayALL(bool flag)
        {
            BtnSave.Enabled = flag;
            btnCancel.Enabled = flag;
            btnPrint.Enabled = flag;
            cmbGodown.Enabled = flag;
        }

        public void FillControls()
        {
            try
            {
                if (ID != 0)
                {
                    tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(ID);
                    dtpBillDate.Value = tVoucherEntry.VoucherDate;
                }
                else
                {
                    dtpBillDate.Value = DBGetVal.ServerTime.Date;
                }
                FillGrid();
                if (dtpBillDate.Text == DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy"))
                    DisplayALL(true);
                else
                    DisplayALL(false);
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

                DataRow[] dr = dtSearch.Select("GodownNo=" + dtSearch.Rows[cntRow].ItemArray[1].ToString());
                long godown = Convert.ToInt64(dr[0].ItemArray[1]);
                cmbGodown.SelectedValue = godown;
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
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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

        private void delete_row()
        {
            try
            {
                //bool flag;
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Type].EditedFormattedValue.ToString().Trim() == "" )
                {
                    if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                        {
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            dgBill.Rows.Add();
                        }
                        else
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        //CalculateTotal();
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
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
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    BtnSave.Focus();
                }
                if (e.KeyCode == Keys.Delete)
                {
                    delete_row();
                    if (dgBill.Rows.Count == 1 && (dgBill.Rows[0].Cells[ColIndex.ItemName].Value == null || dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() == ""))
                        cmbGodown.Enabled = true;
                    else if (dgBill.Rows.Count == 0)
                        cmbGodown.Focus();

                }
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    dgBill.Focus();
                    if (ObjFunction.GetComboValue(cmbGodown) > 0)
                    {
                        if (dgBill.CurrentRow.Index == dgBill.Rows.Count - 1)
                        {
                            if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                            {
                                e.SuppressKeyPress = true;

                                if (dgBill.CurrentRow.Cells[ColIndex.ItemName].Value == null || dgBill.CurrentRow.Cells[ColIndex.ItemName].Value.ToString() == "")
                                {
                                    dgBill.CurrentCell.Value = "";
                                    Desc_Start();
                                }
                               
                            }
                        }
                        else if (dgBill.CurrentCell.ColumnIndex == ColIndex.ActualStock)
                        {
                            e.SuppressKeyPress = true;
                            if (dgBill.CurrentRow.Cells[ColIndex.ActualStock].Value == null || dgBill.CurrentRow.Cells[ColIndex.ActualStock].Value.ToString() == "")
                            {
                                dgBill.CurrentRow.Cells[ColIndex.ActualStock].Value = "0";
                                dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = "0";
                            }
                            if (dgBill.CurrentRow.Index < dgBill.Rows.Count - 2)
                            {
                                MovetoNext move2n = new MovetoNext(m2n);
                                BeginInvoke(move2n, new object[] { dgBill.CurrentRow.Index + 1, 6 });
                            }
                            else if (dgBill.CurrentRow.Index == dgBill.Rows.Count - 2)
                            {
                                MovetoNext move2n = new MovetoNext(m2n);
                                BeginInvoke(move2n, new object[] { dgBill.CurrentRow.Index + 1, ColIndex.ItemName });
                            }
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Please Select Godown Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        cmbGodown.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbGodown_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (ObjFunction.GetComboValue(cmbGodown) > 0)
                    {
                        dgBill.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Please Select Location", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        cmbGodown.Focus();
                    }
                }
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
                if (ObjFunction.GetComboValue(cmbGodown) > 0)
                {
                    SelectGodown();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SelectGodown()
        {
            try
            {
                ObjTrans.ExecuteQuery("exec stockupdateall ", CommonFunctions.ConStr);
                FillGrid();
                cmbGodown.Enabled = false;
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
                cmbGodown.Enabled = true;
                cmbGodown.SelectedValue = "0";
                while (dgBill.Rows.Count > 0)
                    dgBill.Rows.RemoveAt(0);
                pnlGroup1.Visible = false;
                pnlItemName.Visible = false;
                pnlRate.Visible = false;
                pnlUOM.Visible = false;
                pnlGroup2.Visible = false;
                cmbGodown.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void formatPics()
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                            int rowIndex = dgBill.CurrentCell.RowIndex;
                            int tempRowindex = dgBill.CurrentCell.RowIndex;
                            dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgBill.CurrentCell.Value;
                       
                            ItemType = 3;
                            FillItemList(0, ItemType);
                     

                        }

                    }
                }
                
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

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
                     DataTable dtBarCodeItemNo = ObjFunction.GetDataView("Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "'").Table;
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
                                " Where mItemMaster.IsActive='true' and mItemMaster.FkStockGroupTypeNo<>3 ";
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
                ItemList = ItemList.Replace("MItemMaster.UOMDefault,", "MItemMaster.UOML as UOMDefault ,");
                ItemList = ItemList.Replace("AND MItemMaster.UOMDefault =", "AND MItemMaster.UOML =");
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

                ItemList = ItemList.Replace("@FromDate", "" + dtpBillDate.Text.Trim() + "");
                ItemList = ItemList.Replace("@CompNo@", "MItemMaster.CompanyNo");
                if (ObjFunction.GetComboValue(cmbGodown) != 0)
                    ItemList = ItemList.Replace("@GodownNo@", "" + ObjFunction.GetComboValue(cmbGodown) + "");
                    //ItemList = ItemList.Replace("MSB.GodownNo = 2", "MSB.GodownNo = " + ObjFunction.GetComboValue(cmbGodown));

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

        private void SearchBarcode(String strBarcode, out long[] ItemNo, out long[] BarcodeNo)
        {
            //string sql = "Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";
            string sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                " WHERE ((MStockBarcode.Barcode = '" + strBarcode + "') or (MStockItems.ShortCode='" + strBarcode + "')) AND (MStockItems.IsActive = 'true') and (MStockItems.FkStockGroupTypeNo<>3) AND (MRateSetting.IsActive='true')";
            DataTable dt = ObjFunction.GetDataView(sql).Table;
            BarcodeNo = new long[dt.Rows.Count];
            ItemNo = new long[dt.Rows.Count];
            try
            {
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
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

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

                ActualStock_MoveNext();
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
                dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.ItemName].ReadOnly = true;
                dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.ActualStock].ReadOnly = false;
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { rowQtyIndex, ColIndex.ActualStock });
                dgBill.Rows.Add();
                dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].ReadOnly = false;
                dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ActualStock].ReadOnly = true;
                //UOM_Start();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

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

        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //if (Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value) > 0)
                    //{
                        long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                        int rwindex = 0;
                        if (ItemExist(i, Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value), out rwindex) == true)
                        {
                            pnlItemName.Visible = false;
                            OMMessageBox.Show("Item Already Exist.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            dgBill.CurrentCell.Value = "";
                            dgBill.CurrentCell = dgBill[dgBill.CurrentRow.Cells[ColIndex.ActualStock].ColumnIndex, rwindex];

                        }
                        else
                        {
                            dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                            dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[2].Value).ToString("0.00");//lstRate.Text;
                            dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value);//lstRate.SelectedValue;
                            dgBill.CurrentRow.Cells[ColIndex.MRP].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value).ToString("0.00");
                            //dgBill.CurrentRow.Cells[ColIndex.TempMRP].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value).ToString("0.00");
                            //dgBill.CurrentRow.Cells[ColIndex.]
                            dgBill.CurrentRow.Cells[ColIndex.StockFactor].Value = ObjQry.ReturnDouble("Select StockConversion FRom MRateSetting Where PkSrNo=" + dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value + "", CommonFunctions.ConStr).ToString();
                            dgBill.CurrentRow.Cells[ColIndex.PurRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[13].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.UOM].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[6].Value;//always lower uom 
                            dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].Value = ((dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].EditedFormattedValue.ToString().Trim());
                            dgBill.CurrentRow.Cells[ColIndex.FkStockGodownNo].Value = ((dgBill.CurrentRow.Cells[ColIndex.FkStockGodownNo].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgBill.CurrentRow.Cells[ColIndex.FkStockGodownNo].EditedFormattedValue.ToString().Trim());
                        if (DBGetVal.KachhaFirm == false)
                        {
                            dgBill.CurrentRow.Cells[ColIndex.Stock].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.StockNow].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");
                        }
                        else
                        {
                            dgBill.CurrentRow.Cells[ColIndex.Stock].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[17].Value).ToString("0.00");
                            dgBill.CurrentRow.Cells[ColIndex.StockNow].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[17].Value).ToString("0.00");

                        }
                            dgBill.CurrentRow.Cells[ColIndex.ActualStock].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.NetRate].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.DiscAmount].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.DiscRupees].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.DiscPercentage2].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.DiscAmount2].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.DiscRupees2].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.NetAmt].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.Amount].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[10].Value;
                            dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.PkBarCodeNo].Value = ObjQry.ReturnLong("select PkStockBarcodeNo From MStockBarcode Where Barcode='" + dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[10].Value + "'", CommonFunctions.ConStr);
                            dgBill.CurrentRow.Cells[ColIndex.PkVoucherNo].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                            dgBill.CurrentRow.Cells[ColIndex.PkItemTaxInfo].Value = "0";
                            //dgBill.CurrentRow.Cells[ColIndex.StockFactor].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.ActualQty].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.MKTQuantity].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.SGSTPercentage].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.SGSTAmount].Value = "0";
                            //dgBill.CurrentRow.Cells[ColIndex.TempQty].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");
                            pnlItemName.Visible = false;
                            DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo = " + Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value) + " AND IsActive='true' ").Table;
                            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
                            //Desc_MoveNext(i, 0);
                            ActualStock_MoveNext();
                        }
                        if (cmbGodown.Enabled == true && dgBill.Rows.Count > 0 && dgBill.Rows[0].Cells[ColIndex.ItemName].Value != null && dgBill.Rows[0].Cells[ColIndex.ItemName].Value.ToString() != "")
                        {
                            cmbGodown.Enabled = false;
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
                                //else
                                //    CalculateTotal();
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
               exc.ToString();
            }
        }

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

        private void pnlGroup2_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void pnlUOM_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbGodown) > 0)
                {
                    string[] ReportSession;
                    ReportSession = new string[5];

                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(dtpBillDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = ID.ToString();
                    ReportSession[3] = ObjFunction.GetComboValue(cmbGodown).ToString();
                    ReportSession[4] = cmbGodown.Text;
                    Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RptPhysicalStock(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptPhysicalStock.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                {
                    OMMessageBox.Show("Please Select Godown Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    cmbGodown.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}
