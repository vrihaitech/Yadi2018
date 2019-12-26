using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OMControls;
using OM;

namespace Yadi.Vouchers
{
    public partial class POSelection : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public static DataTable dtPOMain = new DataTable();
        public DataTable dtPODetails = new DataTable();
        DataTable dtPO = new DataTable();
        public DialogResult DS = DialogResult.OK;
        long LedgerNo = 0;//, VchNo = 0;
        public long CountPO = 0;

        public POSelection()
        {
            InitializeComponent();
        }

        public POSelection(long LedgNo)
        {
            InitializeComponent();
            LedgerNo = LedgNo;
        }

        private void POSelection_Load(object sender, EventArgs e)
        {
            BindGrid();
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = ObjFunction.GetFont(FontStyle.Bold, 10);
            KeyDownFormat(this.Controls);
        }

        public void BindGrid()
        {
            DataTable dt = new DataTable();

            dt = ObjFunction.GetDataView(" SELECT 0 AS SrNo, VoucherUserNo, VoucherDate, BilledAmount AS Amount, 'false' AS selection, PkOtherVoucherNo " +
                    " FROM TOtherVoucherEntry WHERE (IsComplete = 'false') AND (LedgerNo = " + LedgerNo + ") AND VoucherStatus in (0,1)").Table;
            dgPO.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgPO.Rows.Add();
                for (int j = 0; j < dgPO.Columns.Count; j++)
                {
                    dgPO.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
            }
            if (dt.Rows.Count > 0)
            {
                dgPO.CurrentCell = dgPO[1, 0];
                dgPO.Focus();
                CountPO = dgPO.Rows.Count;
            }
            else
            {
                CountPO = 0;
                DS = DialogResult.Cancel;
                this.Close();
            }

        }

        private void dgPO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = Convert.ToString(e.RowIndex + 1);
            }
            else if (e.ColumnIndex == 2)
            {
                if (e.Value != null && e.Value.ToString() != "")
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }
        }

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
            if (e.KeyCode == Keys.F3)
            {
                BindPurchase();
            }
            else if (e.KeyCode == Keys.F2)
            {
                chkSelectAllPO.Checked = !chkSelectAllPO.Checked;
                chkSelectAllPO_CheckedChanged(sender, new EventArgs());
            }
        }

        #endregion

        public void BindPurchase()
        {
            string strPO = ""; //int cnt = 0;
            for (int i = 0; i < dgPO.Rows.Count; i++)
            {
                //if(dgPO.Rows[i].Cells
                if (Convert.ToBoolean(dgPO.Rows[i].Cells[4].EditedFormattedValue) == true)
                {
                    if (strPO == "")
                        strPO = dgPO.Rows[i].Cells[5].Value.ToString(); 
                    else
                        strPO = strPO + "," + dgPO.Rows[i].Cells[5].Value.ToString();
                }
            }
            if (strPO == "")
            {
                OMMessageBox.Show("Please Select atleast one PO from List...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }

            string strQuery = "SELECT     0 AS Sr, " +
                          " (SELECT     ItemName " +
                            " FROM          dbo.MStockItems_V(NULL, TOtherStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TOtherStock.BalanceQty AS Quantity, " +
                            " MUOM.UOMName, TOtherStock.Rate, CAST(MRateSetting.MRP AS numeric(18, 2)) AS MRP, TOtherStock.NetRate, 0 AS FreeQty,  " +
                            " MUOMFree.UOMName AS FreeUOM, TOtherStock.DiscPercentage, TOtherStock.DiscAmount, TOtherStock.DiscRupees, TOtherStock.DiscPercentage2, " +
                            " TOtherStock.DiscAmount2, TOtherStock.NetAmount AS NetAmt, TOtherStock.TaxPercentage, TOtherStock.TaxAmount, TOtherStock.DiscRupees2, TOtherStock.Amount, " +
                            " MStockBarcode.Barcode, 0 As PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, 0 as PkVoucherTrnNo,MStockItems.ItemNo, MUOM.UOMNo, MItemTaxInfo.TaxLedgerNo, " +
                            " MItemTaxInfo.SalesLedgerNo, TOtherStock.FkRateSettingNo, MItemTaxInfo.PkSrNo, MRateSetting.StockConversion,  " +
                            " TOtherStock.Quantity * MRateSetting.StockConversion AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, 0 AS SalesVchNo, 0 AS TaxVchNo,  " +
                            " mItemMaster.CompanyNo, 'Print' AS BarcodePrinting, TOtherStock.FreeUOMNo, CAST(MRateSetting.MRP AS numeric(18, 2)) AS TempMRP, " +
                            " TOtherStock.LandedRate,TOtherVoucherEntry.VoucherUserNo AS PONo,TOtherStock.PkOtherStockTrnNo FROM MStockItems INNER JOIN " +
                            " TOtherStock ON mItemMaster.ItemNo = TOtherStock.ItemNo INNER JOIN " +
                            " MItemTaxInfo ON TOtherStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo INNER JOIN " +
                            " MStockBarcode ON TOtherStock.FkStockBarCodeNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                            " MUOM ON TOtherStock.FkUomNo = MUOM.UOMNo INNER JOIN " +
                            " MRateSetting ON TOtherStock.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN " +
                            " MUOM AS MUOMFree ON TOtherStock.FreeUOMNo = MUOMFree.UOMNo INNER JOIN " +
                            " TOtherVoucherEntry ON TOtherStock.FKVoucherNo = TOtherVoucherEntry.PkOtherVoucherNo " +
                            " Where TOtherVoucherEntry.PkOtherVoucherNo in (" + strPO + ") AND ((TOtherStock.BalanceQty)<=TOtherStock.Quantity) " +
                            " AND ((TOtherStock.BalanceQty) > 0) " +
                            " ORDER BY TOtherStock.PkOtherStockTrnNo";
            DataTable dtPO = ObjFunction.GetDataView(strQuery).Table;
            dtPODetails = dtPO.Clone();
            if (dtPOMain.Columns.Count <=  0) dtPOMain = dtPO.Clone();
            dgPODetails.Rows.Clear();
            for (int i = 0; i < dtPO.Rows.Count; i++)
            {
                dgPODetails.Rows.Add();
                for (int j = 0; j < dtPO.Columns.Count; j++)
                {
                    dgPODetails.Rows[i].Cells[j].Value = dtPO.Rows[i].ItemArray[j];
                }
                dgPODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value = "0.00";
            }
            if (dgPODetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgPODetails.Rows.Count; i++)
                {
                    for (int k = 0; k < dtPOMain.Rows.Count; k++)
                    {
                        if (dgPODetails.Rows[i].Cells[ColIndex.FKOtherStockTrnNo].Value.ToString() == dtPOMain.Rows[k].ItemArray[ColIndex.FKOtherStockTrnNo].ToString())
                            dgPODetails.Rows[i].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgPODetails.Rows[i].Cells[ColIndex.Quantity].Value) - Convert.ToDouble(dtPOMain.Rows[k].ItemArray[ColIndex.Quantity].ToString());
                    }
                }
                dgPODetails.CurrentCell = dgPODetails[ColIndex.ActualQtyMain, 0];
                dgPODetails.Focus();

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            while (dtPODetails.Rows.Count > 0)
                dtPODetails.Rows.RemoveAt(0);

            for (int i = 0; i < dgPODetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgPODetails.Rows[i].Cells[ColIndex.POSelection].Value) == true)
                {
                    DataRow dr = dtPODetails.NewRow();
                    DataRow dr1 = dtPOMain.NewRow();
                    for (int col = 0; col < dgPODetails.Columns.Count - 2; col++)
                    {
                        dr[col] = dgPODetails.Rows[i].Cells[col].Value;
                        dr1[col] = dgPODetails.Rows[i].Cells[col].Value;
                    }
                    dr[ColIndex.Quantity] = dgPODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dr[ColIndex.ActualQty] = dgPODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dtPODetails.Rows.Add(dr);
                    dr1[ColIndex.Quantity] = dgPODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dr1[ColIndex.ActualQty] = dgPODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dtPOMain.Rows.Add(dr1);
                }
            }
            if (dtPODetails.Rows.Count > 0)
            {
                DS = DialogResult.OK;
                this.Close();
            }
            else
            {
                OMMessageBox.Show("Please Select atleast one PO Item ...", CommonFunctions.ConStr, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                btnOK.Focus();
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DS = DialogResult.Cancel;
            this.Close();
        }

        private void dgPODetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = Convert.ToString(e.RowIndex + 1);
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
            public static int MRP = 5;
            public static int NetRate = 6;
            public static int FreeQty = 7;
            public static int FreeUOM = 8;
            public static int DiscPercentage = 9;
            public static int DiscAmount = 10;
            public static int DiscRupees = 11;
            public static int DiscPercentage2 = 12;
            public static int DiscAmount2 = 13;
            public static int NetAmt = 14;
            public static int SGSTPercentage = 15;
            public static int SGSTAmount = 16;
            public static int DiscRupees2 = 17;
            public static int Amount = 18;
            public static int Barcode = 19;
            public static int PkStockTrnNo = 20;
            public static int PkBarCodeNo = 21;
            public static int PkVoucherNo = 22;
            public static int ItemNo = 23;
            public static int UOMNo = 24;
            public static int TaxLedgerNo = 25;
            public static int SalesLedgerNo = 26;
            public static int PkRateSettingNo = 27;
            public static int PkItemTaxInfo = 28;
            public static int StockFactor = 29;
            public static int ActualQty = 30;
            public static int MKTQuantity = 31;
            public static int SalesVchNo = 32;
            public static int TaxVchNo = 33;
            public static int StockCompanyNo = 34;
            public static int BarcodePrint = 35;
            public static int FreeUomNo = 36;
            public static int TempMRP = 37;
            public static int LandedRate = 38;
            public static int PONo = 39;
            public static int FKOtherStockTrnNo = 40;
            public static int ActualQtyMain = 41;
            public static int POSelection = 42;
        }
        #endregion

        private void chkSelectAllPO_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgPO.Rows.Count; i++)
            {
                dgPO.Rows[i].Cells[4].Value = chkSelectAllPO.Checked;
            }
        }

        private void btnPOSelect_Click(object sender, EventArgs e)
        {
            BindPurchase();
        }

        private void dgPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                dgPO.Rows[dgPO.CurrentCell.RowIndex].Cells[4].Value = !Convert.ToBoolean(dgPO.Rows[dgPO.CurrentCell.RowIndex].Cells[4].Value);
            }
        }

        private void dgPODetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.ActualQtyMain)
            {
                if (dgPODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dgPODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                    dgPODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                if (Convert.ToDouble(dgPODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) > 0)
                    dgPODetails.Rows[e.RowIndex].Cells[ColIndex.POSelection].Value = true;
                else
                    dgPODetails.Rows[e.RowIndex].Cells[ColIndex.POSelection].Value = false;
            }
            else if (e.ColumnIndex == ColIndex.POSelection)
            {
                if (Convert.ToBoolean(dgPODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == false)
                    dgPODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].Value = "0.00";
            }
        }

        private void dgPODetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnOK.Focus();
            }
        }

        private void dgPODetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgPODetails.CurrentCell.ColumnIndex == ColIndex.ActualQtyMain)
            {
                TextBox txt = (TextBox)e.Control;
                txt.TextChanged += new EventHandler(txtQuantity_TextChanged);
            }
        }

        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (dgPODetails.CurrentCell.ColumnIndex == ColIndex.ActualQtyMain)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 6, OMFunctions.MaskedType.NotNegative);
            }
        }
    }
}
