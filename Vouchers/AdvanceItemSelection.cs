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
    public partial class AdvanceItemSelection : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dt = new DataTable();
        bool isDisc1PercentChanged = false, isDisc2PercentChanged = false;

        public AdvanceItemSelection()
        {
            InitializeComponent();
        }



        private void AdvanceItemSelection_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbBrand, "Select StockGroupNo,StockGroupName from MStockGroup where ControlGroup = 3");
                rbAll.Checked = true;
                cmbBrand.Focus();
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
                dgBrand.Rows.Clear();
                if (rbAll.Checked == true)
                {



                    //string sqlQuery = " SELECT     0 AS Sr,  (SELECT  MItemGroup.StockGroupName + ' ' + CASE WHEN (ItemShortCode <> '') THEN ItemShortCode ELSE ItemName END) As ItemName, " +
                    //  " 0 As Quantity,  MUOM.UOMName, MRateSetting.PurRate, CAST(MRateSetting.MRP AS numeric(18, 2)) AS MRP, " +
                    //  " MRateSetting.PurRate As NetRate, 0 As FreeQty,  MUOM.UOMName AS FreeUOM, 0 As DiscPercentage, " +
                    //  " 0 As DiscAmount, 0 As DiscRupees, 0 As DiscPercentage2,  0 As DiscAmount2, " +
                    //  " 0 AS NetAmt, MItemTaxInfo.Percentage as TaxPercentage, 0 As TaxAmount, 0 As DiscRupees2, " +
                    //  " 0 As Amount,  MStockBarcode.Barcode, 0 As PkOtherStockTrnNo, MStockBarcode.PkStockBarcodeNo, " +
                    //  " 0 as PkVoucherTrnNo,MStockItems.ItemNo, MUOM.UOMNo, MItemTaxInfo.TaxLedgerNo,  MItemTaxInfo.SalesLedgerNo, " +
                    //  " MRateSetting.PkSrNo as FkRateSettingNo, MItemTaxInfo.PkSrNo, MRateSetting.StockConversion, " +
                    //  " 0 As ActualQty, MRateSetting.MKTQty AS MKTQuantity, " +
                    //  " 0 AS SalesVchNo, 0 AS TaxVchNo,   mItemMaster.CompanyNo, 'Print' AS BarcodePrinting, MUOM.UOMNo AS FreeUOMNo, " +
                    //  " CAST(MRateSetting.MRP AS numeric(18, 2)) AS TempMRP,  0 As LandedRate " +
                    //  " FROM MStockItems " +
                    //  " INNER JOIN    GetItemTaxAll(NULL, NULL, 11, 32,NULL) As MItemTaxInfo ON mItemMaster.ItemNo = MItemTaxInfo.ItemNo " +
                    //  " INNER JOIN    GetItemRateAll(NULL,NULL,NULL,NULL ,NULL,11)As MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                    //  " INNER JOIN   MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo " +
                    //  " INNER JOIN   MUOM ON MRateSetting.UomNo = MUOM.UOMNo " +
                    //  " INNER JOIN  MStockGroup ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo " +
                    //  " wheremItemGroup.ItemGroupNo  =  '" + ObjFunction.GetComboValue(cmbBrand) + "' " +
                    //  " ORDER BY ItemName ";


                    string sqlQuery = " SELECT     0 AS Sr,  (SELECT  MItemGroup.StockGroupName + ' ' + CASE WHEN (ItemShortCode <> '') THEN ItemShortCode ELSE ItemName END) As ItemName, " +
                                       " isnull(MStockItems.MinLevel,0) as MinLevel, " +
                                       " isnull(MStockItems.MaxLevel,0) as MaxLevel,isnull(Sum(OpQty),0) as currentQty, isnull((MStockItems.MinLevel-Sum(OpQty)),0) as OrdQty,isnull((MStockItems.MaxLevel-Sum(OpQty)),0) as MaxQty, " +
                                       " 0 As Quantity,  MUOM.UOMName, MRateSetting.PurRate, CAST(MRateSetting.MRP AS numeric(18, 2)) AS MRP, " +
                                       " MRateSetting.PurRate As NetRate, 0 As FreeQty,  MUOM.UOMName AS FreeUOM, 0 As DiscPercentage, " +
                                       " 0 As DiscAmount, 0 As DiscRupees, 0 As DiscPercentage2,  0 As DiscAmount2, " +
                                       " 0 AS NetAmt, MItemTaxInfo.Percentage as TaxPercentage, 0 As TaxAmount, 0 As DiscRupees2, " +
                                       " 0 As Amount,  MStockBarcode.Barcode, 0 As PkOtherStockTrnNo, MStockBarcode.PkStockBarcodeNo, " +
                                       " 0 as PkVoucherTrnNo,MStockItems.ItemNo, MUOM.UOMNo, MItemTaxInfo.TaxLedgerNo,  MItemTaxInfo.SalesLedgerNo, " +
                                       " MRateSetting.PkSrNo as FkRateSettingNo, MItemTaxInfo.PkSrNo, MRateSetting.StockConversion, " +
                                       " 0 As ActualQty, MRateSetting.MKTQty AS MKTQuantity, " +
                                       " 0 AS SalesVchNo, 0 AS TaxVchNo,   mItemMaster.CompanyNo, 'Print' AS BarcodePrinting, MUOM.UOMNo AS FreeUOMNo, " +
                                       " CAST(MRateSetting.MRP AS numeric(18, 2)) AS TempMRP,  0 As LandedRate  " +
                                       " FROM " +
                                       "( " +
                                       "    SELECT  TStock.ItemNo, " +
                                       "   sum(case when (TStock.TrnCode = 1) then isnull(TStock.BilledQuantity,0) " +
                                       "    else isnull(TStock.BilledQuantity,0)*-1 end) as OpQty " +
                                       "    FROM    TVoucherEntry INNER JOIN " +
                                       "    TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo " +
                                       "    WHERE   TVoucherEntry.IsCancel='false' " +
                                       "    Group by TStock.itemno " +

                                       "      ) As Tbl1  " +
                                       " RIGHT OUTER JOIN  MStockItems ON mItemMaster.ItemNo = Tbl1.ItemNo " +
                                       " INNER JOIN    GetItemTaxAll(NULL, NULL, 11, 32,NULL) As MItemTaxInfo ON mItemMaster.ItemNo = MItemTaxInfo.ItemNo " +
                                       " INNER JOIN    GetItemRateAll(NULL,NULL,NULL,NULL ,NULL,11)As MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                                       " INNER JOIN   MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo " +
                                       " INNER JOIN   MUOM ON MRateSetting.UomNo = MUOM.UOMNo " +
                                       " INNER JOIN  MStockGroup ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo " +
                                       " wheremItemGroup.ItemGroupNo  =  '" + ObjFunction.GetComboValue(cmbBrand) + "'  " +
                                       " group by MItemGroup.StockGroupName, mItemMaster.ItemShortCode, mItemMaster.ItemShortCode, mItemMaster.ItemName, mItemMaster.MinLevel, mItemMaster.MaxLevel, MUOM.UOMName,MRateSetting.PurRate, MRateSetting.MRP,MRateSetting.PkSrNo,MRateSetting.StockConversion, MRateSetting.MKTQty, " +
                                       " MItemTaxInfo.Percentage,MStockBarcode.Barcode,MStockBarcode.PkStockBarcodeNo,MStockItems.ItemNo,MUOM.UOMNo,MItemTaxInfo.TaxLedgerNo,  MItemTaxInfo.SalesLedgerNo, MItemTaxInfo.PkSrNo,MStockItems.CompanyNo " +
                                       " ORDER BY ItemName ";

                    dt = ObjFunction.GetDataView(sqlQuery).Table;
                }
                else
                {
                    string sqlQuery = " SELECT     0 AS Sr,  (SELECT  MItemGroup.StockGroupName + ' ' + CASE WHEN (ItemShortCode <> '') THEN ItemShortCode ELSE ItemName END) As ItemName, " +
                                     " isnull(MStockItems.MinLevel,0) as MinLevel, " +
                                     " isnull(MStockItems.MaxLevel,0) as MaxLevel,isnull(Sum(OpQty),0) as currentQty, isnull((MStockItems.MinLevel-Sum(OpQty)),0) as OrdQty,isnull((MStockItems.MaxLevel-Sum(OpQty)),0) as MaxQty, " +
                                     " 0 As Quantity,  MUOM.UOMName, MRateSetting.PurRate, CAST(MRateSetting.MRP AS numeric(18, 2)) AS MRP, " +
                                     " MRateSetting.PurRate As NetRate, 0 As FreeQty,  MUOM.UOMName AS FreeUOM, 0 As DiscPercentage, " +
                                     " 0 As DiscAmount, 0 As DiscRupees, 0 As DiscPercentage2,  0 As DiscAmount2, " +
                                     " 0 AS NetAmt, MItemTaxInfo.Percentage as TaxPercentage, 0 As TaxAmount, 0 As DiscRupees2, " +
                                     " 0 As Amount,  MStockBarcode.Barcode, 0 As PkOtherStockTrnNo, MStockBarcode.PkStockBarcodeNo, " +
                                     " 0 as PkVoucherTrnNo,MStockItems.ItemNo, MUOM.UOMNo, MItemTaxInfo.TaxLedgerNo,  MItemTaxInfo.SalesLedgerNo, " +
                                     " MRateSetting.PkSrNo as FkRateSettingNo, MItemTaxInfo.PkSrNo, MRateSetting.StockConversion, " +
                                     " 0 As ActualQty, MRateSetting.MKTQty AS MKTQuantity, " +
                                     " 0 AS SalesVchNo, 0 AS TaxVchNo,   mItemMaster.CompanyNo, 'Print' AS BarcodePrinting, MUOM.UOMNo AS FreeUOMNo, " +
                                     " CAST(MRateSetting.MRP AS numeric(18, 2)) AS TempMRP,  0 As LandedRate  " +
                                     " FROM " +
                                     "( " +
                                     "    SELECT  TStock.ItemNo, " +
                                     "   sum(case when (TStock.TrnCode = 1) then isnull(TStock.BilledQuantity,0) " +
                                     "    else isnull(TStock.BilledQuantity,0)*-1 end) as OpQty " +
                                     "    FROM    TVoucherEntry INNER JOIN " +
                                     "    TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo " +
                                     "    WHERE   TVoucherEntry.IsCancel='false' " +
                                     "    Group by TStock.itemno " +

                                     "      ) As Tbl1  " +
                                     " RIGHT OUTER JOIN  MStockItems ON mItemMaster.ItemNo = Tbl1.ItemNo " +
                                     " INNER JOIN    GetItemTaxAll(NULL, NULL, 11, 32,NULL) As MItemTaxInfo ON mItemMaster.ItemNo = MItemTaxInfo.ItemNo " +
                                     " INNER JOIN    GetItemRateAll(NULL,NULL,NULL,NULL ,NULL,11)As MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                                     " INNER JOIN   MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo " +
                                     " INNER JOIN   MUOM ON MRateSetting.UomNo = MUOM.UOMNo " +
                                     " INNER JOIN  MStockGroup ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo " +
                                     " wheremItemGroup.ItemGroupNo  =  '" + ObjFunction.GetComboValue(cmbBrand) + "' AND OpQty<MinLevel " +
                                     " group by MItemGroup.StockGroupName, mItemMaster.ItemShortCode, mItemMaster.ItemShortCode, mItemMaster.ItemName, mItemMaster.MinLevel, mItemMaster.MaxLevel, MUOM.UOMName,MRateSetting.PurRate, MRateSetting.MRP,MRateSetting.PkSrNo,MRateSetting.StockConversion, MRateSetting.MKTQty, " +
                                     " MItemTaxInfo.Percentage,MStockBarcode.Barcode,MStockBarcode.PkStockBarcodeNo,MStockItems.ItemNo,MUOM.UOMNo,MItemTaxInfo.TaxLedgerNo,  MItemTaxInfo.SalesLedgerNo, MItemTaxInfo.PkSrNo,MStockItems.CompanyNo " +
                                     " ORDER BY ItemName ";

                    dt = ObjFunction.GetDataView(sqlQuery).Table;

                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBrand.Rows.Add();
                    dgBrand.Rows[j].Cells[0].Value = j + 1;
                    for (int i = 1; i < dt.Columns.Count; i++)//LandedRate
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dgBrand.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                            dgBrand.Rows[j].Cells[0].ReadOnly = true;
                            dgBrand.Rows[j].Cells[1].ReadOnly = true;
                        }
                    }

                    //if (Convert.ToDouble(dgBrand.Rows[j].Cells[ColIndex.Quantity].Value) >= 0)
                    //    lblBillItem.Text = (Convert.ToDouble(lblBillItem.Text) + Convert.ToDouble(dgBrand.Rows[j].Cells[ColIndex.Quantity].Value)).ToString();
                    //else
                    //    lblBilExchangeItem.Text = (Convert.ToInt64(lblBilExchangeItem.Text) + Math.Abs(Convert.ToDouble(dgBrand.Rows[j].Cells[ColIndex.Quantity].Value))).ToString();
                    //DEEPAK
                    //strStkNo = dgBill.Rows[j].Cells[ColIndex.PkStockTrnNo].Value.ToString();
                    //FillGodownDetails(strStkNo);
                    //if (j == 0)
                    //{
                    //    long GdNo = ObjQry.ReturnLong("Select GodownNo From TStockGodown WHERE (FKStockTrnNo in (" + strStkNo + "))", CommonFunctions.ConStr);
                    //    ObjFunction.FillCombo(cmbLocation, "SELECT GodownNo, GodownName FROM MGodown WHERE ((IsActive = 'true') Or (GodownNo=" + GdNo + ")) And GodownNo<>1 ");
                    //    cmbLocation.SelectedValue = GdNo.ToString();
                    //}
                    ////DEEPAK
                }
                //dgBrand.Rows.Add();
                //dgBrand.CurrentCell = dgBrand[1, dgBrand.Rows.Count - 1];
                CalculateTotal();


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
            public static int MinStocklevel = 2;
            public static int CurrentStock = 3;
            public static int MaxStocklevel = 4;
            public static int SuggestedMinQty = 5;
            public static int SuggestedMaxQty = 6;
            public static int Quantity = 7;
            public static int UOM = 8;
            public static int Rate = 9;
            public static int MRP = 10;
            public static int NetRate = 11;
            public static int FreeQty = 12;
            public static int FreeUOM = 13;
            public static int DiscPercentage = 14;
            public static int DiscAmount = 15;
            public static int DiscRupees = 16;
            public static int DiscPercentage2 = 17;
            public static int DiscAmount2 = 18;
            public static int NetAmt = 19;
            public static int SGSTPercentage = 20;
            public static int SGSTAmount = 21;
            public static int DiscRupees2 = 22;
            public static int Amount = 23;
            public static int Barcode = 24;
            public static int PkStockTrnNo = 25;
            public static int PkBarCodeNo = 26;
            public static int PkVoucherNo = 27;
            public static int ItemNo = 28;
            public static int UOMNo = 29;
            public static int TaxLedgerNo = 30;
            public static int SalesLedgerNo = 31;
            public static int PkRateSettingNo = 32;
            public static int PkItemTaxInfo = 33;
            public static int StockFactor = 34;
            public static int ActualQty = 35;
            public static int MKTQuantity = 36;
            public static int SalesVchNo = 37;
            public static int TaxVchNo = 38;
            public static int StockCompanyNo = 39;
            public static int BarcodePrint = 40;
            public static int FreeUomNo = 41;
            public static int TempMRP = 42;
            public static int LandedRate = 43;
        }
        #endregion

        private void cmbBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    FillGrid();

                    if (dgBrand.Rows.Count > 0)
                    {
                        dgBrand.Focus();

                        dgBrand.CurrentCell = dgBrand.Rows[0].Cells[7];
                    }
                }

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
                int rowQtyIndex = dgBrand.CurrentCell.RowIndex;

                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { rowQtyIndex, 9, dgBrand });

                //CalculateTotal();//temp

                // dgBrand.Rows[rowQtyIndex].Cells[2].ReadOnly = true;

                //found in the dgBrand_keydown
                //if (dgBrand.CurrentCell.ColumnIndex == 2)
                //{
                //    if (dgBrand.Rows.Count > 1)
                //    {
                //        row = (dgBrand.CurrentCell.RowIndex == 0) ? 0 : dgBrand.CurrentCell.RowIndex;
                //        if (Convert.ToString(dgBrand.Rows[row].Cells[2].Value) != "")
                //        {
                //            dgBrand.CurrentCell = dgBrand[3, row];
                //            dgBrand.CurrentCell.ReadOnly = false;
                //        }
                //    }
                //}
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
                if (dgBrand.CurrentCell.Value != null)
                {
                    if (ObjFunction.CheckValidAmount(dgBrand.CurrentCell.Value.ToString()) == true)
                    {
                        //dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[ColIndex.Amount].Value = (Convert.ToDouble(dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value) * Convert.ToDouble(dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[2].Value)) / Convert.ToDouble(dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[ColIndex.MKTQuantity].Value);
                        //dgBrand.CurrentCell.ReadOnly = true;
                        //BindGrid(dgBrand.CurrentCell.RowIndex);

                        if (dgBrand.Columns[ColIndex.FreeQty].Visible == true)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex, ColIndex.FreeQty, dgBrand });
                            dgBrand.CurrentCell = dgBrand[ColIndex.FreeQty, dgBrand.CurrentCell.RowIndex];
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBrand.Rows.Count - 1, 1, dgBrand });
                            dgBrand.CurrentCell = dgBrand[1, dgBrand.Rows.Count - 1];
                        }
                        dgBrand.Focus();

                        //CalculateTotal();
                    }
                    else
                    {
                        dgBrand.CurrentCell.ErrorText = "Please Enter valid rate...";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void FreeQty_MoveNext()
        {
            try
            {
                if (dgBrand.CurrentCell.Value != null && dgBrand.CurrentCell.Value.ToString() != "")
                {
                    if (ObjFunction.CheckValidAmount(dgBrand.CurrentCell.Value.ToString()) == true)
                    {

                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex, ColIndex.DiscPercentage, dgBrand });
                        //dgBrand.CurrentCell = dgBrand[1, dgBrand.Rows.Count - 1];
                        dgBrand.Focus();

                        CalculateTotal();
                    }
                    else
                    {
                        dgBrand.CurrentCell.ErrorText = "Please Enter Valid Free Quantity...";
                    }
                }
                else
                {
                    dgBrand.CurrentCell.Value = "0";
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


        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        public void CalculateTotal()
        {
            try
            {
                //txtSubTotal.Text = "0.00";
                //lblBillItem.Text = "0";
                //lblBilExchangeItem.Text = "0";
                //txtGrandTotal.Text = "0.00";
                //if (txtTotalDisc.Text == null || txtTotalDisc.Text == "")
                //    txtTotalDisc.Text = "0.00";
                //if (txtDistDisc.Text == null || txtDistDisc.Text == "")
                //    txtDistDisc.Text = "0.00";
                //txtTotalItemDisc.Text = "0.00";
                //txtTotalTax.Text = "0.00";
                //txtVisibility.Text = "0.00";
                //txtReturnAmt.Text = "0.00";
                double subTotal = 0, totalTax = 0, TotSchemeDisc = 0, TotDistDisc = 0;//TotalDiscBeforeTax = 0,TotalDiscAfterTax = 0
                //if (Validations() == true)
                //{
                for (int i = 0; i < dgBrand.Rows.Count; i++)
                {
                    if (dgBrand.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBrand.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                    {

                        #region check & init Default values
                        if (dgBrand.Rows[i].Cells[ColIndex.Quantity].Value == null) dgBrand.Rows[i].Cells[ColIndex.Quantity].Value = Convert.ToDouble(1).ToString("0.00");

                        if (dgBrand.Rows[i].Cells[ColIndex.MKTQuantity].Value == null) dgBrand.Rows[i].Cells[ColIndex.MKTQuantity].Value = 1;
                        if (dgBrand.Rows[i].Cells[ColIndex.StockFactor].Value == null) dgBrand.Rows[i].Cells[ColIndex.StockFactor].Value = 1;
                        if (dgBrand.Rows[i].Cells[ColIndex.Rate].Value == null) dgBrand.Rows[i].Cells[ColIndex.Rate].Value = 0;
                        if (dgBrand.Rows[i].Cells[ColIndex.DiscPercentage].Value == null) dgBrand.Rows[i].Cells[ColIndex.DiscPercentage].Value = 0;
                        if (dgBrand.Rows[i].Cells[ColIndex.DiscRupees].Value == null) dgBrand.Rows[i].Cells[ColIndex.DiscRupees].Value = 0;
                        if (dgBrand.Rows[i].Cells[ColIndex.DiscPercentage2].Value == null) dgBrand.Rows[i].Cells[ColIndex.DiscPercentage2].Value = 0;
                        if (dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value == null) dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value = 0;
                        if (dgBrand.Rows[i].Cells[ColIndex.DiscRupees2].Value == null) dgBrand.Rows[i].Cells[ColIndex.DiscRupees2].Value = 0;
                        if (dgBrand.Rows[i].Cells[ColIndex.FreeQty].Value == null) dgBrand.Rows[i].Cells[ColIndex.FreeQty].Value = 0;
                        if (dgBrand.Rows[i].Cells[ColIndex.LandedRate].Value == null) dgBrand.Rows[i].Cells[ColIndex.LandedRate].Value = 0;
                        //if (dgBrand.Rows[i].Cells[ColIndex.FreeQty].Value == "") dgBrand.Rows[i].Cells[ColIndex.FreeQty].Value = 0;
                        #endregion

                        #region fetch basic values
                        double Qty = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.Quantity].Value);
                        double Rate = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.Rate].Value);
                        double TaxPerce = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.SGSTPercentage].Value);
                        double MktQty = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.MKTQuantity].Value);

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsReverseRateCalc)) == true)
                        {
                            Rate = Convert.ToDouble(((Rate * 100) / (100 + TaxPerce)).ToString("0.00")); //reverse rate
                        }

                        double Amount = Convert.ToDouble((((Qty) * (Rate)) / (MktQty)).ToString("0.0000"));
                        #endregion

                        #region Before tax discount calculation
                        //disc1 %
                        double Disc1 = 0;
                        if (isDisc1PercentChanged == true && i == dgBrand.CurrentRow.Index)
                        {
                            dgBrand.Rows[i].Cells[ColIndex.DiscPercentage].Value = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscPercentage].Value).ToString(Format.DoubleFloating);
                            dgBrand.Rows[i].Cells[ColIndex.DiscAmount].Value = Convert.ToDouble(((Amount * Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscPercentage].Value)) / 100).ToString("0.00"));
                            isDisc1PercentChanged = false;
                            //isDisc2PercentChanged = true;
                            Disc1 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount].Value);
                        }
                        else if (i == dgBrand.CurrentRow.Index)
                        {
                            //Disc1 = Convert.ToDouble(((Amount * Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscPercentage].Value)) / 100).ToString("0.0000"));
                            Disc1 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount].Value);
                            dgBrand.Rows[i].Cells[ColIndex.DiscPercentage].Value = (Amount == 0) ? "0" : ((Disc1 * 100) / Amount).ToString("0.00");
                        }
                        Disc1 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount].Value);
                        //disc 1 rs
                        double DiscAmt1 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscRupees].Value);
                        double DiscBeforeTax = Disc1 + DiscAmt1;// before disc 2%
                        double tAmount = Amount - DiscBeforeTax; // before disc 2%
                        //disc 2 %
                        double Disc2 = 0;
                        if (isDisc2PercentChanged == true && i == dgBrand.CurrentRow.Index)
                        {
                            dgBrand.Rows[i].Cells[ColIndex.DiscPercentage2].Value = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscPercentage2].Value).ToString(Format.DoubleFloating);
                            dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value = Convert.ToDouble(((tAmount * Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.00"));
                            isDisc2PercentChanged = false;
                            Disc2 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value);
                        }
                        else if (i == dgBrand.CurrentRow.Index)
                        {
                            //Disc2 = Convert.ToDouble(((tAmount * Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.0000"));
                            Disc2 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value);
                            dgBrand.Rows[i].Cells[ColIndex.DiscPercentage2].Value = (tAmount == 0) ? "0" : ((Disc2 * 100) / tAmount).ToString("0.00");
                        }
                        Disc2 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value);
                        // Total disc before tax
                        DiscBeforeTax = 0;
                        DiscBeforeTax += Disc2;
                        //Net Amt Before Tax - for sub total
                        tAmount -= Disc2;
                        //Net Rate (after 1st disc %, 1st Rs, 2nd % OR Before Tax)
                        double ttRate = 0, LandedRate = 0;
                        #region Tax Calculation
                        double TaxAmt = Convert.ToDouble(((tAmount * TaxPerce) / (100)).ToString("0.00"));
                        #endregion

                        if (tAmount != 0 || Qty != 0)
                        {
                            ttRate = (tAmount) / Qty;
                            LandedRate = (tAmount + TaxAmt) / (Qty + Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.FreeQty].Value));
                        }
                        #endregion



                        //#region After tax discount calculation
                        //double DiscAmt2 = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscRupees2].Value);
                        //double DiscAfterTax = DiscAmt2;
                        //#endregion
                        double DiscAfterTax = 0;

                        #region Put values in Grid
                        dgBrand.Rows[i].Cells[ColIndex.Amount].Value = (tAmount + TaxAmt - DiscAfterTax).ToString("0.00");
                        dgBrand.Rows[i].Cells[ColIndex.DiscAmount].Value = Disc1.ToString("0.00");
                        dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value = Disc2.ToString("0.00");
                        dgBrand.Rows[i].Cells[ColIndex.SGSTAmount].Value = TaxAmt.ToString("0.00");
                        dgBrand.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                        dgBrand.Rows[i].Cells[ColIndex.NetAmt].Value = tAmount.ToString("0.00");
                        dgBrand.Rows[i].Cells[ColIndex.ActualQty].Value = (((Qty) + Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.FreeQty].Value)) * (Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.StockFactor].Value)));
                        dgBrand.Rows[i].Cells[ColIndex.LandedRate].Value = LandedRate.ToString("0.00");
                        #endregion

                        //#region Cumulative sum for footer calc usage
                        //TotalDiscBeforeTax += DiscBeforeTax;
                        //TotalDiscAfterTax += DiscAfterTax;
                        //subTotal += tAmount;
                        //totalTax += TaxAmt;
                        //#endregion

                        //#region Calculate total Sale & Exchange Qty
                        //if (Qty >= 0)
                        //    lblBillItem.Text = (Convert.ToDouble(lblBillItem.Text) + Qty).ToString();
                        //else
                        //    lblBilExchangeItem.Text = (Convert.ToDouble(lblBilExchangeItem.Text) + Math.Abs(Qty)).ToString();
                        //#endregion

                        TotSchemeDisc = TotSchemeDisc + Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount].Value);
                        TotDistDisc = TotDistDisc + Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndex.DiscAmount2].Value);
                        //}
                    }



                    double TotalAmt = 0.0;
                    if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_BTaxItemDisc)) != 0)
                    {
                        //subTotal = Convert.ToDouble((subTotal + TotalDiscBeforeTax).ToString("0.00"));// Math.Round(subTotal, 00);
                        subTotal = Convert.ToDouble((subTotal).ToString("0.00"));// Math.Round(subTotal, 00);
                        //TotalAmt = Convert.ToDouble((subTotal - TotalDiscBeforeTax + totalTax - TotalDiscAfterTax).ToString("0.00"));
                        TotalAmt = Convert.ToDouble((subTotal + totalTax).ToString("0.00"));//- TotalDiscAfterTax
                    }
                    else
                    {
                        subTotal = Convert.ToDouble(subTotal.ToString("0.00"));// Math.Round(subTotal, 00);
                        TotalAmt = Convert.ToDouble((subTotal + totalTax).ToString("0.00"));//- TotalDiscAfterTax
                    }


                    //#region footer discount & Charges calculation
                    ////txtDiscRupees1.Text = Convert.ToDouble((TotalAmt * Convert.ToDouble(txtDiscount1.Text)) / 100).ToString("0.00");
                    //TotalAmt -= Convert.ToDouble(txtDiscRupees1.Text);



                    //double TotalAnotherDisc = Convert.ToDouble(txtDiscRupees1.Text);
                    //totalChrg = Convert.ToDouble(txtChrgRupees1.Text) + Convert.ToDouble(txtOtherTax.Text);

                    //#endregion

                    //#region Put Values in Footer TextFields
                    //txtSubTotal.Text = subTotal.ToString("0.00");

                    //if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_BTaxItemDisc)) != 0)
                    //{
                    //    txtTotalItemDisc.Text = TotalDiscBeforeTax.ToString("0.00");
                    //}
                    //else
                    //{
                    //    txtTotalItemDisc.Text = "0.00";
                    //    TotalDiscBeforeTax = 0;
                    //}
                    ///// txtTotalDisc.Text = TotalDiscAfterTax.ToString("0.00");
                    //txtTotalDisc.Text = TotSchemeDisc.ToString("0.00");
                    //txtDistDisc.Text = TotDistDisc.ToString("0.00");
                    //txtTotalTax.Text = totalTax.ToString("0.00");

                    //txtTotalAnotherDisc.Text = TotalAnotherDisc.ToString("0.00");
                    //txtTotalChrgs.Text = totalChrg.ToString("0.00");

                    //totalTax = Convert.ToDouble(totalTax.ToString("0.00"));
                    //txtGrandTotal.Text = ((subTotal + totalTax + totalChrg) - (TotalDiscAfterTax + TotalAnotherDisc)).ToString("0.00");
                    ////txtGrandTotal.Text = ((subTotal + totalTax + totalChrg) - (TotalDiscAfterTax + TotalDiscBeforeTax + TotalAnotherDisc)).ToString("0.00");
                    //TotFinal = Math.Round(Convert.ToDouble(txtGrandTotal.Text), MidpointRounding.AwayFromZero);
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBillRoundOff)) == true)
                    //{
                    //    txtRoundOff.Text = (TotFinal - Convert.ToDouble(txtGrandTotal.Text)).ToString("0.00");
                    //    // txtGrandTotal.Text = ((subTotal + totalTax + totalChrg + Convert.ToDouble(txtRoundOff.Text)) - (TotalDiscAfterTax + TotalDiscBeforeTax + TotalAnotherDisc)).ToString("0.00");
                    //    txtGrandTotal.Text = ((subTotal + totalTax + totalChrg + Convert.ToDouble(txtRoundOff.Text)) - (TotalDiscAfterTax + TotalAnotherDisc)).ToString("0.00");
                    //}
                    //else
                    //    txtRoundOff.Text = "0.00";
                    //#endregion

                    //long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPaymentType) + "", CommonFunctions.ConStr);
                    //if (ControlUnder == 4)
                    //{
                    //    if (dgPayChqDetails.Rows.Count > 0)
                    //    {
                    //        dgPayChqDetails.Rows[0].Cells[4].Value = txtGrandTotal.Text;
                    //    }
                    //}
                    //else if (ControlUnder == 5)
                    //{
                    //    if (dgPayCreditCardDetails.Rows.Count > 0)
                    //    {
                    //        dgPayCreditCardDetails.Rows[0].Cells[3].Value = txtGrandTotal.Text;
                    //    }
                    //}
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgBrand_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    if (dgBrand.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        if (dgBrand.CurrentCell.Value == null) dgBrand.CurrentCell.Value = "0";
                        Qty_MoveNext();
                        //dgBrand.CurrentCell = dgBrand[ColIndex.Rate, dgBrand.CurrentCell.RowIndex];
                        //  dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[9];
                    }
                    else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.Rate)
                    {
                        Rate_MoveNext();
                       // dgBrand.CurrentCell = dgBrand[ColIndex.MRP, dgBrand.CurrentCell.RowIndex];
                        // dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[10];
                    }
                    else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.MRP)
                    {
                        dgBrand.CurrentCell = dgBrand[ColIndex.FreeQty, dgBrand.CurrentCell.RowIndex];
                        //dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[12];
                    }
                    else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.FreeQty)
                    {
                        if (dgBrand.CurrentCell.Value == null && dgBrand.CurrentCell.Value.ToString() == "") dgBrand.CurrentCell.Value = "0";
                        FreeQty_MoveNext();
                        //dgBrand.CurrentCell = dgBrand[ColIndex.DiscPercentage, dgBrand.CurrentCell.RowIndex];
                        // dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[14];
                    }
                    //else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.FreeUOM)
                    //{
                    //    dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[14];
                    //}
                    else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                    {
                        if (dgBrand.CurrentCell.Value == null) dgBrand.CurrentCell.Value = "0";
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex, ColIndex.DiscAmount, dgBrand });

                      //  dgBrand.CurrentCell = dgBrand[ColIndex.DiscAmount, dgBrand.CurrentCell.RowIndex];
                        // dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[15];
                    }
                    else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscAmount)
                    {
                        if (dgBrand.CurrentCell.Value == null) dgBrand.CurrentCell.Value = "0";
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (dgBrand.Rows.Count - 1 != dgBrand.CurrentCell.RowIndex)
                            BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex, ColIndex.DiscAmount2, dgBrand });
                        
                        //dgBrand.CurrentCell = dgBrand[ColIndex.DiscAmount2, dgBrand.CurrentCell.RowIndex];
                        // dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[18];
                    }
                    else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscAmount2)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (dgBrand.Rows.Count - 1 != dgBrand.CurrentCell.RowIndex)
                            BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex, ColIndex.Amount, dgBrand });
                       // dgBrand.CurrentCell = dgBrand[ColIndex.Amount, dgBrand.CurrentCell.RowIndex];
                        //dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[23];
                    }
                    //else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.SGSTPercentage)
                    //{
                    //    dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[21];
                    //}
                    //else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.SGSTAmount)
                    //{
                    //    dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[23];
                    //}
                    //else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.Amount)
                    //{
                    //    dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[7];
                    //}
                    else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.Amount)
                    {

                        if (dgBrand.CurrentCell.RowIndex < dgBrand.Rows.Count - 1)
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                           
                                BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex + 1, ColIndex.Quantity, dgBrand });
                           // dgBrand.CurrentCell = dgBrand[ColIndex.Quantity, dgBrand.CurrentCell.RowIndex + 1];
                            // dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex + 1].Cells[7];
                        }
                    }
                    else
                        btnExit.Focus();
                    CalculateTotal();
                }

                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    btnExit.Focus();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rbAll_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void dgBrand_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBrand.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {
                isDisc1PercentChanged = true;
                isDisc2PercentChanged = true;
                Qty_MoveNext();
            }
            else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.FreeQty)
            {
                isDisc1PercentChanged = true;
                isDisc2PercentChanged = true;
                FreeQty_MoveNext();
            }
          
            else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.Rate)
            {
                isDisc1PercentChanged = true;
                isDisc2PercentChanged = true;
                Rate_MoveNext();
            }
            else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
            {
                isDisc1PercentChanged = true;
                isDisc2PercentChanged = true;
                Disc_MoveNext();
                if (dgBrand.Columns[ColIndex.DiscAmount].Visible == true)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscAmount, dgBrand });
                }
                //else
                //{
                //    MovetoNext move2n = new MovetoNext(m2n);
                //    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscAmount, dgBrand });
                //}
            }
            else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscAmount)
            {
                isDisc2PercentChanged = true;
               
                //double amt = Convert.ToDouble((Convert.ToDouble(dgBrand.CurrentCell.Value) * 100) / (Convert.ToDouble(dgBrand.Rows[e.RowIndex].Cells[ColIndex.Quantity].Value) * Convert.ToDouble(dgBrand.Rows[e.RowIndex].Cells[ColIndex.Rate].Value)));
                //dgBrand.Rows[e.RowIndex].Cells[ColIndex.DiscPercentage].Value = amt;
                ////dgBrand.Rows[e.RowIndex].Cells[ColIndex.Amount].Value = Convert.ToDouble(dgBrand.Rows[e.RowIndex].Cells[ColIndex.Amount].Value) - Convert.ToDouble(dgBrand.Rows[e.RowIndex].Cells[ColIndex.DiscAmount].Value);
                CalculateTotal();
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscAmount2, dgBrand });
            }
            //else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscRupees)
            //{
            //    Disc_MoveNext();
            //    if (dgBrand.Columns[ColIndex.DiscPercentage2].Visible == true)
            //    {
            //        MovetoNext move2n = new MovetoNext(m2n);
            //        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscPercentage2, dgBrand });
            //    }
            //    else
            //    {
            //        MovetoNext move2n = new MovetoNext(m2n);
            //        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBrand });
            //    }
            //}
            //else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscPercentage2)
            //{
            //    isDisc1PercentChanged = true;
            //    isDisc2PercentChanged = true;
            //    Disc_MoveNext();
            //    if (dgBrand.Columns[ColIndex.DiscAmount2].Visible == true)
            //    {
            //        MovetoNext move2n = new MovetoNext(m2n);
            //        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscAmount2, dgBrand });
            //    }
            //    //else
            //    //{

            //    //    MovetoNext move2n = new MovetoNext(m2n);
            //    //    BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBrand });
            //    //}
            //}
            //else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscRupees2)
            //{
            //    CalculateTotal();
            //    MovetoNext move2n = new MovetoNext(m2n);
            //    BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBrand });
            //    //Disc_MoveNext();
            //    //MovetoNext move2n = new MovetoNext(m2n);
            //    //BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.Amount, dgBrand });
            //}
            else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.DiscAmount2)
            {
                //isDisc1PercentChanged = true;
                //double amt = Convert.ToDouble((Convert.ToDouble(dgBrand.CurrentCell.Value) * 100) / Convert.ToDouble(dgBrand.Rows[e.RowIndex].Cells[ColIndex.Amount].Value));
                //dgBrand.Rows[e.RowIndex].Cells[ColIndex.DiscPercentage2].Value = amt;
                //dgBrand.Rows[e.RowIndex].Cells[ColIndex.Amount].Value = Convert.ToDouble(dgBrand.Rows[e.RowIndex].Cells[ColIndex.Amount].Value) - Convert.ToDouble(dgBrand.Rows[e.RowIndex].Cells[ColIndex.DiscAmount2].Value);
                CalculateTotal();
                MovetoNext move2n = new MovetoNext(m2n);
                //if (dgBrand.Columns[ColIndex.DiscRupees2].Visible == true)
                //    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscRupees2, dgBrand });
                //else
                    BeginInvoke(move2n, new object[] { e.RowIndex , ColIndex.Amount, dgBrand });
            }
            else if (dgBrand.CurrentCell.ColumnIndex == ColIndex.MRP)
            {
                if (dgBrand.CurrentCell.Value != null && dgBrand.CurrentCell.Value.ToString() != "")
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.FreeQty, dgBrand });

                    if (ObjFunction.CheckValidAmount(dgBrand.CurrentCell.Value.ToString()) == true)
                    {

                    }
                }
            }
        }

        

    }
}
