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
    public partial class QuotationRateChange : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBTQuotation dbTQuotation = new DBTQuotation();
        //MRateSetting mRateSettig = new MRateSetting();
        TQuotation TQuotation = new TQuotation();
        TQuotationDetails TQuotationDetails = new TQuotationDetails();
        public QuotationRateChange()
        {
            InitializeComponent();
        }

        private void QuotationRateChange_Load(object sender, EventArgs e)
        {
            try
            {
                //btnApplyChanges.Enabled = false;
                FillCmb();
                FillInDr();
                FillPerRs();
                cmbMainGroup.Focus();
                txtTotProducts.Text = "0";
                btnCancel.Visible = true;
                cmbInDeType.SelectedIndex = 0;
                cmbPerRs.SelectedIndex = 0;
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        public void FillInDr()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("ValueType");
            DataRow dr = null;

            dr = dt.NewRow();
            dr[1] = "Increase";
            dr[0] = "0";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[1] = "Decrease";
            dr[0] = "1";
            dt.Rows.Add(dr);


            cmbInDeType.DataSource = dt.DefaultView;
            cmbInDeType.DisplayMember = dt.Columns[1].ColumnName;
            cmbInDeType.ValueMember = dt.Columns[0].ColumnName;
        }

        public void FillPerRs()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("ValueType");
            DataRow dr = null;

            dr = dt.NewRow();
            dr[1] = "%";
            dr[0] = "0";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[1] = "Rs";
            dr[0] = "1";
            dt.Rows.Add(dr);


            cmbPerRs.DataSource = dt.DefaultView;
            cmbPerRs.DisplayMember = dt.Columns[1].ColumnName;
            cmbPerRs.ValueMember = dt.Columns[0].ColumnName;
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validations(0) == true)
                {
                    if (OMMessageBox.Show("Are you sure want to save this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbTQuotation = new DBTQuotation();
                        for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ChkChange].FormattedValue) == 1)
                            {
                                TQuotationDetails = new TQuotationDetails();
                                TQuotationDetails.PkSrNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value.ToString());
                                TQuotationDetails.FkQuotationNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.QuotationNo].Value.ToString());
                                TQuotationDetails.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value.ToString());
                                TQuotationDetails.LedgerNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.LedgerNo].Value.ToString());
                                TQuotationDetails.MRP = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.MRP].Value.ToString());
                                TQuotationDetails.Rate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.Rate].Value.ToString());
                                TQuotationDetails.FkRateSettingNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.FkRateSettingNo].Value.ToString());
                                TQuotationDetails.CompanyNo = DBGetVal.FirmNo;
                                TQuotationDetails.IsClose = Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsClose].Value.ToString());
                                dbTQuotation.AddTQuotationDetails1(TQuotationDetails);
                            }
                        }
                        if (dbTQuotation.ExecuteNonQueryStatements() != 0)
                        {
                            DisplayMessage("Item Changed Save Successfully...");
                            FillCmb();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillCmb()
        {
            while (gvRateSetting.Rows.Count > 0)
                gvRateSetting.Rows.RemoveAt(0);
            cmbCustomer.Enabled = true;
            cmbItemsName.Enabled = true;
            ObjFunction.FillCombo(cmbMainGroup, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 ORDER BY StockGroupName");
            ObjFunction.FillCombo(cmbItemsName, "SELECT DISTINCT mItemMaster.ItemNo, MItemGroup.StockGroupName + ' - ' + mItemMaster.ItemName AS ItemName FROM   TQuotationDetails INNER JOIN  MStockItems ON TQuotationDetails.ItemNo = mItemMaster.ItemNo INNER JOIN TQuotation ON TQuotationDetails.FkQuotationNo = TQuotation.QuotationNo INNER JOIN MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo WHERE  ('" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' BETWEEN TQuotation.FromDate AND TQuotation.ToDate) AND (TQuotationDetails.IsClose='false') ORDER BY ItemName");
            ObjFunction.FillComb(cmbItemName, "ItemNo", "ItemName");
            ObjFunction.FillCombo(cmbCustomer, "SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName FROM  MLedger INNER JOIN TQuotation ON MLedger.LedgerNo = TQuotation.LedgerNo  INNER JOIN  TQuotationDetails ON TQuotation.QuotationNo = TQuotationDetails.FkQuotationNo  WHERE  ('" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' BETWEEN TQuotation.FromDate AND TQuotation.ToDate) AND (MLedger.GroupNo = " + GroupType.SundryDebtors + ") AND (TQuotationDetails.IsClose = 'false') ORDER BY MLedger.LedgerName");
            cmbItemsName.SelectedIndex = 0;
            cmbItemsName.Focus();
            txtSaleRate.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ChkSelect.Checked = false;
                while (gvRateSetting.Rows.Count > 0)
                {
                    gvRateSetting.Rows.RemoveAt(0);
                }
                cmbCustomer.Enabled = true;
                cmbItemsName.Enabled = true;
                if (cmbItemName.SelectedIndex >= 0)
                    cmbItemName.SelectedIndex = 0;
                if (cmbMainGroup.SelectedIndex >= 0)
                    cmbMainGroup.SelectedIndex = 0;
                if (cmbItemsName.SelectedIndex >= 0)
                    cmbItemsName.SelectedIndex = 0;
                if (cmbCustomer.SelectedIndex >= 0)
                    cmbCustomer.SelectedIndex = 0;
                CalculateTotProducts();
                //cmbMainGroup.Focus();
                cmbItemsName.Focus();
                //btnApplyChanges.Enabled = false;
                txtSaleRate.Text = "";
                cmbInDeType.SelectedIndex = 0;
                cmbPerRs.SelectedIndex = 0;
                txtAmount.Text = "";

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindGrid()
        {
            try
            {
                string sql = "";
                sql = " SELECT  0 AS SrNo, mItemMaster.ItemNo, MItemGroup.StockGroupName + ' - ' + mItemMaster.ItemName AS ItemName, MLedger.LedgerNo, MLedger.LedgerName, " +
                      " TQuotation.QuotationNo, TQuotationDetails.PkSrNo, TQuotation.FromDate, TQuotation.ToDate, TQuotationDetails.MRP, TQuotationDetails.Rate, 'false' AS Chk,TQuotationDetails.FkRateSettingNo,TQuotationDetails.IsClose ," +
                      " 0 as ChkChange" +
                      " FROM    TQuotationDetails INNER JOIN " +
                      " MStockItems ON TQuotationDetails.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                      " TQuotation ON TQuotationDetails.FkQuotationNo = TQuotation.QuotationNo INNER JOIN " +
                      " MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo INNER JOIN " +
                      " MLedger ON TQuotation.LedgerNo = MLedger.LedgerNo " +
                      " WHERE ('" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' BETWEEN TQuotation.FromDate AND TQuotation.ToDate) AND TQuotationDetails.IsClose='false' ";
                string strWhere = "";
                if (cmbItemsName.Enabled == true)
                    strWhere = " AND (MStockItems.ItemNo = " + ObjFunction.GetComboValue(cmbItemsName) + ") ORDER BY ItemName";
                else
                    strWhere = " AND (MLedger.LedgerNo = " + ObjFunction.GetComboValue(cmbCustomer) + ") ORDER BY MLedger.LedgerName";

                sql += strWhere;
                DataTable dt = new DataTable();
                if (strWhere != "")
                    dt = ObjFunction.GetDataView(sql).Table;
                gvRateSetting.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvRateSetting.Rows.Add();
                        for (int j = 0; j < gvRateSetting.Columns.Count; j++)
                        {
                            //if (j == ColIndex.IsActive)
                            //    gvRateSetting.Rows[i].Cells[ColIndex.IsActive].Value = false;

                            //if (j == ColIndex.HidChk)
                            //    gvRateSetting.Rows[i].Cells[ColIndex.HidChk].Value = false;

                            gvRateSetting.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                        gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = "False";
                    }
                    if (gvRateSetting.Rows.Count > 0)
                    {
                        gvRateSetting.CurrentCell = gvRateSetting[ColIndex.Chk, 0];
                        gvRateSetting.Focus();
                    }
                }
                else
                {
                    //DisplayMessage("Records not found...!!");
                    while (gvRateSetting.Rows.Count > 0)
                    {
                        gvRateSetting.Rows.RemoveAt(0);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbMainGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbMainGroup.SelectedIndex > 0)
                {
                    BindGrid();
                    CalculateTotProducts();
                    //ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT mItemMaster.GroupNo, MItemGroup.StockGroupName FROM  MStockItems INNER JOIN  MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo WHERE (MStockItems.GroupNo IN (SELECT  GroupNo FROM MStockItems AS MStockItems  WHERE (GroupNo1 =" + ObjFunction.GetComboValue(cmbMainGroup) + "))) ORDER BY MItemGroup.StockGroupName");
                    //ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT StockGroupNo AS GroupNo, StockGroupName FROM  MStockGroup WHERE  (StockGroupNo IN (SELECT  MT.GroupNo FROM MStockItems MT WHERE (MT.GroupNo1 =" + ObjFunction.GetComboValue(cmbMainGroup) + "))) ORDER BY StockGroupName");
                    ObjFunction.FillCombo(cmbItemsName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
                }
            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int Sr = 0;
            public static int ItemNo = 1;
            public static int ItemName = 2;
            public static int LedgerNo = 3;
            public static int LedgerName = 4;
            public static int QuotationNo = 5;
            public static int PkSrNo = 6;
            public static int FromDate = 7;
            public static int ToDate = 8;
            public static int MRP = 9;
            public static int Rate = 10;
            public static int Chk = 11;
            public static int FkRateSettingNo = 12;
            public static int IsClose = 13;
            public static int ChkChange = 14;
        }
        #endregion

        private void CalculateTotProducts()
        {
            txtTotProducts.Text = Convert.ToString(gvRateSetting.Rows.Count);
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void cmbMainGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.FillCombo(cmbItemsName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
        }

        private void gvRateSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            if (e.ColumnIndex == ColIndex.FromDate || e.ColumnIndex == ColIndex.ToDate)
                e.Value = Convert.ToDateTime(e.Value).ToString(Format.DDMMMYYYY);
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void gvRateSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnApplyChanges.Focus();
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
            if (e.KeyCode == Keys.F2)
            {
                ChkSelect.Checked = !ChkSelect.Checked;
                ChkSelect_CheckedChanged(sender, e);
            }
        }
        #endregion

        private void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRateSetting.Rows.Count; i++)
            {
                gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = ChkSelect.Checked;
            }
        }

        public bool Validations(int k)
        {
            try
            {
                bool flag = false;
                for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                {
                    if (k == 0)
                    {
                        if (Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ChkChange].FormattedValue) == 1)
                        {
                            flag = true;
                            break;
                        }
                    }
                    else if (k == 1)
                    {
                        if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
               
                 if (flag == false)
                {
                    DisplayMessage("Select Atleast One Row");

                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        public bool Validations()
        {
            try
            {
                bool flag = false;
                for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                {
                    if (Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == 1)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                {
                    DisplayMessage("Select Atleast One Row");
                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(1200);
            lblMsg.Visible = false;
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void gvRateSetting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (gvRateSetting.Rows.Count > 0 && e.ColumnIndex != ColIndex.Chk)//&& e.ColumnIndex!=ColIndex.IsActive)
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].Value = true;
                        //if(e.ColumnIndex!=ColIndex.IsActive)
                        //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = true;
                        for (int i = 0; i < gvRateSetting.Columns.Count - 1; i++)
                        {
                            if (gvRateSetting.Rows[e.RowIndex].Cells[i].Value == null || gvRateSetting.Rows[e.RowIndex].Cells[i].Value.ToString().Length == 0 || gvRateSetting.Rows[e.RowIndex].Cells[i].ErrorText != "")
                            {
                                gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].Value = false;
                                //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = false;
                                break;
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



        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validations(1) == true)
                {
                    if (OMMessageBox.Show("Are you sure want to Delete this record(s)?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbTQuotation = new DBTQuotation();
                        for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                            {
                                TQuotationDetails = new TQuotationDetails();
                                TQuotationDetails.PkSrNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value.ToString());
                                dbTQuotation.DeleteTQuotationDetails1(TQuotationDetails);
                            }
                        }
                        if (dbTQuotation.ExecuteNonQueryStatements() != 0)
                        {
                            DisplayMessage("Item(s) Delete Successfully...");
                            FillCmb();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbItemsName_KeyDown(object sender, KeyEventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbItemsName) != 0 && e.KeyCode == Keys.Enter)
            {
                cmbCustomer.Enabled = false;
                cmbCustomer.SelectedValue = 0;
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void cmbItemsName_Leave(object sender, EventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbItemsName) != 0)
            {
                cmbCustomer.Enabled = false;
                cmbCustomer.SelectedValue = 0;
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbCustomer) != 0 && e.KeyCode == Keys.Enter)
            {
                cmbItemsName.Enabled = false;
                cmbItemsName.SelectedValue = 0;
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void cmbCustomer_Leave(object sender, EventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbCustomer) != 0)
            {
                cmbItemsName.Enabled = false;
                cmbItemsName.SelectedValue = 0;
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtSaleRate, 2, 7, OMFunctions.MaskedType.NotNegative);
        }

        private void txtSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtSaleRate.Text.Trim().ToString() != "")
            {
                e.SuppressKeyPress = true;
                btnApplyChanges.Focus();
            }
        }

        private void gvRateSetting_Leave(object sender, EventArgs e)
        {
            if (txtSaleRate.Text.Trim().ToString() != "")
            {
                btnApplyChanges.Focus();
            }
        }

        private void btnApplySame_Click(object sender, EventArgs e)
        {
            if (txtSaleRate.Text.Trim() != "")
            {
                if (Validations() == true)
                {
                    for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                        {
                            gvRateSetting.Rows[i].Cells[ColIndex.Rate].Value = txtSaleRate.Text.Trim();
                            gvRateSetting.Rows[i].Cells[ColIndex.ChkChange].Value = "1";
                            gvRateSetting.Rows[i].Cells[ColIndex.Rate].Style.BackColor = Color.LightSkyBlue;
                            gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = "false";
                        }
                    }
                    txtSaleRate.Text = "";
                    btnApplyChanges.Focus();
                }

            }
            else
            {
                DisplayMessage("Enter Sale Rate");
                txtAmount.Focus();
            }

        }

        private void btnApplytoInDe_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text.Trim() != "")
            {
                if (Validations() == true)
                {
                    double Amt = 0.00;
                    if (cmbPerRs.SelectedIndex == 0)
                    {
                        if (cmbInDeType.SelectedIndex == 0)
                            Amt = (Convert.ToDouble(txtAmount.Text) / 100);
                        else if (cmbInDeType.SelectedIndex == 1)
                            Amt = -(Convert.ToDouble(txtAmount.Text) / 100);
                    }
                    else if (cmbPerRs.SelectedIndex == 1)
                    {
                        if (cmbInDeType.SelectedIndex == 0)
                            Amt = Convert.ToDouble(txtAmount.Text);
                        else if (cmbInDeType.SelectedIndex == 1)
                            Amt = -Convert.ToDouble(txtAmount.Text);

                    }
                    for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                        {
                            double Totalamt = 0;
                            if (cmbPerRs.SelectedIndex == 0)
                            {
                                Totalamt = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.Rate].Value) + (Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.Rate].Value) * Amt);
                            }
                            else
                                Totalamt = (Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.Rate].Value) + Amt);

                            if (Totalamt < 0)
                                gvRateSetting.Rows[i].Cells[ColIndex.Rate].Value = 0;
                            else
                                gvRateSetting.Rows[i].Cells[ColIndex.Rate].Value = Totalamt.ToString("0.00");
                            gvRateSetting.Rows[i].Cells[ColIndex.ChkChange].Value = "1";
                            gvRateSetting.Rows[i].Cells[ColIndex.Rate].Style.BackColor = Color.LightSkyBlue;
                            gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = "false";
                        }
                    }

                    cmbInDeType.SelectedIndex = 0;
                    cmbPerRs.SelectedIndex = 0;
                    txtAmount.Text = "";
                }
            }
            else
            {
                DisplayMessage("Enter Sale Rate");
                txtAmount.Focus();
            }

        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtAmount, 2, 7, OMFunctions.MaskedType.NotNegative);
        }
    }
}
