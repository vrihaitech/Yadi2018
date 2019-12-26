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
    public partial class SOSelection : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public static DataTable dtSOMain = new DataTable();
        public DataTable dtSODetails = new DataTable();
        DataTable dtSO = new DataTable();
        public DialogResult DS = DialogResult.OK;
        long LedgerNo = 0;//, VchNo = 0;
        public long CountSO = 0, VchTypeCode;

        public SOSelection()
        {
            InitializeComponent();
        }

        public SOSelection(long LedgNo, long VchTypeCode)
        {
            InitializeComponent();
            LedgerNo = LedgNo;
            this.VchTypeCode = VchTypeCode;
        }

        private void SOSelection_Load(object sender, EventArgs e)
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
                    " FROM TOtherVoucherEntry WHERE (IsComplete = 'false') AND VoucherTypeCode=" + VchTypeCode + " AND (LedgerNo = " + LedgerNo + ") AND VoucherStatus in (0,1)").Table;
            dgSO.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgSO.Rows.Add();
                for (int j = 0; j < dgSO.Columns.Count; j++)
                {
                    dgSO.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
            }
            if (dt.Rows.Count > 0)
            {
                dgSO.CurrentCell = dgSO[1, 0];
                dgSO.Focus();
                CountSO = dgSO.Rows.Count;
            }
            else
            {
                CountSO = 0;
                DS = DialogResult.Cancel;
                this.Close();
            }

        }
        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);
        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
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
                BindSales();
            }
            else if (e.KeyCode == Keys.F2)
            {
                chkSelectAllPO.Checked = !chkSelectAllPO.Checked;
                chkSelectAllPO_CheckedChanged(sender, new EventArgs());
            }
        }

        #endregion

        public void BindSales()
        {
            string strSO = ""; //int cnt = 0;
            for (int i = 0; i < dgSO.Rows.Count; i++)
            {
                //if(dgPO.Rows[i].Cells
                if (Convert.ToBoolean(dgSO.Rows[i].Cells[4].EditedFormattedValue) == true)
                {
                    if (strSO == "")
                        strSO = dgSO.Rows[i].Cells[5].Value.ToString();
                    else
                        strSO = strSO + "," + dgSO.Rows[i].Cells[5].Value.ToString();
                }
            }
            if (strSO == "")
            {
                OMMessageBox.Show("Please Select atleast one SO from List...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }

            string strQuery = "SELECT     0 AS Sr, " +
                          " (SELECT     ItemName " +
                            " FROM          dbo.MStockItems_V(NULL, TOtherStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TOtherStock.BalanceQty AS Quantity, " +
                            " MUOM.UOMName, TOtherStock.Rate, CAST(MRateSetting.MRP AS numeric(18, 2)) AS MRP, TOtherStock.NetRate, " +
                            " TOtherStock.DiscPercentage, TOtherStock.DiscAmount, TOtherStock.DiscRupees, TOtherStock.DiscPercentage2, " +
                            " TOtherStock.DiscAmount2, TOtherStock.NetAmount AS NetAmt, TOtherStock.TaxPercentage, TOtherStock.TaxAmount, TOtherStock.DiscRupees2, TOtherStock.Amount, " +
                            " MStockBarcode.Barcode, 0 As PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, 0 as PkVoucherTrnNo,MStockItems.ItemNo, MUOM.UOMNo, MItemTaxInfo.TaxLedgerNo, " +
                            " MItemTaxInfo.SalesLedgerNo, TOtherStock.FkRateSettingNo, MItemTaxInfo.PkSrNo, MRateSetting.StockConversion,  " +
                            " TOtherStock.Quantity * MRateSetting.StockConversion AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, 0 AS SalesVchNo, 0 AS TaxVchNo,  " +
                            " mItemMaster.CompanyNo,CAST(MRateSetting.MRP AS numeric(18, 2)) AS TempMRP, " +
                            " TOtherStock.LandedRate,TOtherVoucherEntry.VoucherUserNo AS SONo,TOtherStock.PkOtherStockTrnNo FROM MStockItems INNER JOIN " +
                            " TOtherStock ON mItemMaster.ItemNo = TOtherStock.ItemNo INNER JOIN " +
                            " MItemTaxInfo ON TOtherStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo INNER JOIN " +
                            " MStockBarcode ON TOtherStock.FkStockBarCodeNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                            " MUOM ON TOtherStock.FkUomNo = MUOM.UOMNo INNER JOIN " +
                            " MRateSetting ON TOtherStock.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN " +
                            " MUOM AS MUOMFree ON TOtherStock.FreeUOMNo = MUOMFree.UOMNo INNER JOIN " +
                            " TOtherVoucherEntry ON TOtherStock.FKVoucherNo = TOtherVoucherEntry.PkOtherVoucherNo " +
                            " Where TOtherVoucherEntry.VoucherTypeCode=" + VchTypeCode + " AND TOtherVoucherEntry.PkOtherVoucherNo in (" + strSO + ") AND ((TOtherStock.BalanceQty)<=TOtherStock.Quantity) " +
                            " AND ((TOtherStock.BalanceQty) > 0) " +
                            " ORDER BY TOtherStock.PkOtherStockTrnNo";
            DataTable dtSO = ObjFunction.GetDataView(strQuery).Table;
            dtSODetails = dtSO.Clone();
            if (dtSOMain.Columns.Count <= 0) dtSOMain = dtSO.Clone();
            dgSODetails.Rows.Clear();
            for (int i = 0; i < dtSO.Rows.Count; i++)
            {
                dgSODetails.Rows.Add();
                for (int j = 0; j < dtSO.Columns.Count; j++)
                {
                    dgSODetails.Rows[i].Cells[j].Value = dtSO.Rows[i].ItemArray[j];
                }
                dgSODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value = "0.00";
            }
            if (dgSODetails.Rows.Count > 0)
            {
                for (int i = 0; i < dgSODetails.Rows.Count; i++)
                {
                    for (int k = 0; k < dtSOMain.Rows.Count; k++)
                    {
                        if (dgSODetails.Rows[i].Cells[ColIndex.FKOtherStockTrnNo].Value.ToString() == dtSOMain.Rows[k].ItemArray[ColIndex.FKOtherStockTrnNo].ToString())
                            dgSODetails.Rows[i].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgSODetails.Rows[i].Cells[ColIndex.Quantity].Value) - (Convert.ToDouble(dtSOMain.Rows[k].ItemArray[ColIndex.Quantity].ToString()));
                    }
                }
                dgSODetails.CurrentCell = dgSODetails[ColIndex.ActualQtyMain, 0];
                dgSODetails.Focus();

            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            while (dtSODetails.Rows.Count > 0)
                dtSODetails.Rows.RemoveAt(0);
            long erroflag = 0;
            for (int i = 0; i < dgSODetails.Rows.Count; i++)
            {
                if (dgSODetails.Rows[i].Cells[ColIndex.ActualQtyMain].ErrorText != "")
                {
                    erroflag = 1;
                    break;
                }
            }
            if (erroflag == 1)
            {
                OMMessageBox.Show("Please enter valid quantity...", CommonFunctions.ConStr, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                btnOK.Focus();
                return;
            }
            for (int i = 0; i < dgSODetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgSODetails.Rows[i].Cells[ColIndex.SOSelection].Value) == true)
                {
                    DataRow dr = dtSODetails.NewRow();
                    DataRow dr1 = dtSOMain.NewRow();
                    for (int col = 0; col < dgSODetails.Columns.Count - 2; col++)
                    {
                        dr[col] = dgSODetails.Rows[i].Cells[col].Value;
                        dr1[col] = dgSODetails.Rows[i].Cells[col].Value;
                    }
                    dr[ColIndex.Quantity] = dgSODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dr[ColIndex.ActualQty] = dgSODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dtSODetails.Rows.Add(dr);
                    dr1[ColIndex.Quantity] = dgSODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dr1[ColIndex.ActualQty] = dgSODetails.Rows[i].Cells[ColIndex.ActualQtyMain].Value;
                    dtSOMain.Rows.Add(dr1);
                }
            }
            if (dtSODetails.Rows.Count > 0)
            {
                DS = DialogResult.OK;
                this.Close();
            }
            else
            {
                OMMessageBox.Show("Please Select atleast one SO Item ...", CommonFunctions.ConStr, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            if (e.ColumnIndex == ColIndex.Quantity)
            {
                if (Convert.ToDouble(e.Value) == 0)
                    dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].ReadOnly = true;
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
            public static int DiscPercentage = 7;
            public static int DiscAmount = 8;
            public static int DiscRupees = 9;
            public static int DiscPercentage2 = 10;
            public static int DiscAmount2 = 11;
            public static int NetAmt = 12;
            public static int SGSTPercentage = 13;
            public static int SGSTAmount = 14;
            public static int DiscRupees2 = 15;
            public static int Amount = 16;
            public static int Barcode = 17;
            public static int PkStockTrnNo = 18;
            public static int PkBarCodeNo = 19;
            public static int PkVoucherNo = 20;
            public static int ItemNo = 21;
            public static int UOMNo = 22;
            public static int TaxLedgerNo = 23;
            public static int SalesLedgerNo = 24;
            public static int PkRateSettingNo = 25;
            public static int PkItemTaxInfo = 26;
            public static int StockFactor = 27;
            public static int ActualQty = 28;
            public static int MKTQuantity = 29;
            public static int SalesVchNo = 30;
            public static int TaxVchNo = 31;
            public static int StockCompanyNo = 32;
            public static int TempMRP = 33;
            public static int LandedRate = 34;
            public static int SONo = 35;
            public static int FKOtherStockTrnNo = 36;
            public static int ActualQtyMain = 37;
            public static int SOSelection = 38;
        }
        #endregion

        private void chkSelectAllPO_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgSO.Rows.Count; i++)
            {
                dgSO.Rows[i].Cells[4].Value = chkSelectAllPO.Checked;
            }
        }

        private void btnPOSelect_Click(object sender, EventArgs e)
        {
            BindSales();
        }

        private void dgPO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                dgSO.Rows[dgSO.CurrentCell.RowIndex].Cells[4].Value = !Convert.ToBoolean(dgSO.Rows[dgSO.CurrentCell.RowIndex].Cells[4].Value);
            }
        }

        private void dgPODetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.ActualQtyMain)
            {
                //if (dgSODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || dgSODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                //    dgSODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0.00";
                if (dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].Value == null || dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].Value.ToString() == "")
                    dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].Value = "0.00";
                if (Convert.ToDouble(dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].Value.ToString()) > Convert.ToDouble(dgSODetails.Rows[e.RowIndex].Cells[ColIndex.Quantity].Value.ToString()))
                {
                    if (e.ColumnIndex == ColIndex.ActualQtyMain)
                        dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].ErrorText = "Please entert valid quantity";
                }
                else
                {
                    dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].ErrorText = "";
                }
                if (Convert.ToDouble(dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].Value.ToString()) > 0)
                    dgSODetails.Rows[e.RowIndex].Cells[ColIndex.SOSelection].Value = true;
                else
                    dgSODetails.Rows[e.RowIndex].Cells[ColIndex.SOSelection].Value = false;

                if (e.ColumnIndex == ColIndex.ActualQtyMain)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { dgSODetails.CurrentCell.RowIndex, ColIndex.ActualQtyMain, dgSODetails });
                }
            }
            else if (e.ColumnIndex == ColIndex.SOSelection)
            {
                if (Convert.ToBoolean(dgSODetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == false)
                {
                    dgSODetails.Rows[e.RowIndex].Cells[ColIndex.ActualQtyMain].Value = "0.00";
                }
            }
        }

        private void dgPODetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (dgSODetails.CurrentCell.ColumnIndex == ColIndex.ActualQtyMain)
                {
                    if (dgSODetails.Rows.Count - 1 == dgSODetails.CurrentCell.RowIndex)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgSODetails.CurrentCell.RowIndex, ColIndex.ActualQtyMain, dgSODetails });
                    }
                    else
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgSODetails.CurrentCell.RowIndex + 1, ColIndex.ActualQtyMain, dgSODetails });
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnOK.Focus();
            }
        }

        private void dgPODetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgSODetails.CurrentCell.ColumnIndex == ColIndex.ActualQtyMain)
            {
                TextBox txt = (TextBox)e.Control;
                txt.TextChanged += new EventHandler(txtQuantity_TextChanged);
            }
        }

        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (dgSODetails.CurrentCell.ColumnIndex == ColIndex.ActualQtyMain)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 6, OMFunctions.MaskedType.NotNegative);
            }
        }
    }
}
