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
    public partial class PartyWiseRateChange : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMLedger dbMLedger = new DBMLedger();
        MLedgerRateSetting mLedgerRateSetting = new MLedgerRateSetting();

        public PartyWiseRateChange()
        {
            InitializeComponent();
        }

        private void PartyWiseRateChange_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbCustomer, "SELECT MLedger.LedgerNo, MLedger.LedgerName AS LedgerName FROM MLedger WHERE (MLedger.GroupNo=" + GroupType.SundryDebtors + ") AND (MLedger.IsActive = 'true') And MLedger.IsHistoryMaintain='True' ORDER BY LedgerName ");
            ObjFunction.FillCombo(cmbBrand, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
            cmbCustomer.Focus();
            KeyDownFormat(this.Controls);
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
                btnApplyChange_Click(sender, e);
            }
           
        }
        #endregion

        private void cmbCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ObjFunction.GetComboValue(cmbCustomer) != 0)
                {
                    e.SuppressKeyPress = true;
                    while (dgDetails.Rows.Count > 0)
                        dgDetails.Rows.RemoveAt(0);
                    cmbBrand.SelectedIndex = 0;
                    cmbBrand.Focus();
                }
                else
                {
                    while (dgDetails.Rows.Count > 0)
                        dgDetails.Rows.RemoveAt(0);
                }
            }
        }

        public void BindGrid()
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);

            string Sql = " SELECT 0 AS SrNo, MItemGroup.StockGroupName + '-' + mItemMaster.ItemName AS ItemName, MUOM.UOMName,MLedgerRateSetting.MRP, MLedgerRateSetting.Rate, MLedgerRateSetting.Rate AS TempRate,MLedgerRateSetting.DiscPercentage,MLedgerRateSetting.DiscPercentage as TempDiscPer, MLedgerRateSetting.PkSrNo, mItemMaster.ItemNo ,0 As IsChange" +
                       " FROM MUOM INNER JOIN MRateSetting INNER JOIN MLedgerRateSetting ON MRateSetting.ItemNo = MLedgerRateSetting.ItemNo AND MRateSetting.MRP = MLedgerRateSetting.MRP AND  MRateSetting.UOMNo = MLedgerRateSetting.UOMNo ON MUOM.UOMNo = MLedgerRateSetting.UOMNo INNER JOIN " +
                       " MStockGroup INNER JOIN MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo ON MLedgerRateSetting.ItemNo = mItemMaster.ItemNo " +
                       " WHERE     (MRateSetting.IsActive = 'True') And MLedgerRateSetting.LedgerNo=" + ObjFunction.GetComboValue(cmbCustomer) + " " +
                       " And    MItemGroup.StockGroupNo=" + ((ObjFunction.GetComboValue(cmbBrand) == 0) ? "MItemGroup.StockGroupNo" : ObjFunction.GetComboValue(cmbBrand).ToString()) + " " +
                       " Order By MItemGroup.StockGroupName + '-' + mItemMaster.ItemName";
            DataTable dt = ObjFunction.GetDataView(Sql).Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgDetails.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dgDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }

        }

        private void cmbBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbCustomer) != 0)
                {
                    BindGrid();
                    if (dgDetails.Rows.Count <= 0)
                    {
                        DisplayMessage("Party Wise Rate Not Found");
                        cmbBrand.Focus();
                    }
                    else
                    {
                        dgDetails.CurrentCell = dgDetails[ColIndex.Rate, 0];
                        dgDetails.Focus();
                    }
                }
                else
                {
                    if (dgDetails.RowCount > 0)
                    {
                        dgDetails.CurrentCell = dgDetails[ColIndex.Rate, 0];
                        dgDetails.Focus();
                    }
                }
            }
        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int UOMName = 2;
            public static int MRP = 3;
            public static int Rate = 4;
            public static int TempRate = 5;
            public static int DiscPer = 6;
            public static int TempDiscPer = 7;
            public static int PkSrNo = 8;
            public static int ItemNo = 9;
            public static int IsChange = 10;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);
            cmbCustomer.SelectedIndex = 0;
            cmbBrand.SelectedIndex = 0;
            cmbCustomer.Focus();
        }

        private void dgDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.Rate)
            {
                if (dgDetails[ColIndex.Rate, e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    dgDetails[ColIndex.Rate, e.RowIndex].Style.BackColor = Color.White;
                    if (Convert.ToDouble(dgDetails[ColIndex.Rate, e.RowIndex].EditedFormattedValue.ToString()) != Convert.ToDouble(dgDetails[ColIndex.TempRate, e.RowIndex].EditedFormattedValue.ToString()))
                    {
                        dgDetails[ColIndex.IsChange, e.RowIndex].Value = 1;
                        dgDetails[ColIndex.Rate, e.RowIndex].Style.BackColor = Color.LightSkyBlue;
                    }
                    else
                    {
                        if (Convert.ToDouble(dgDetails[ColIndex.DiscPer, e.RowIndex].EditedFormattedValue.ToString()) == Convert.ToDouble(dgDetails[ColIndex.TempDiscPer, e.RowIndex].EditedFormattedValue.ToString()))
                        {
                            dgDetails[ColIndex.IsChange, e.RowIndex].Value = 0;
                        }
                    }
                }
            }
            if (e.ColumnIndex == ColIndex.DiscPer)
            {
                if (dgDetails[ColIndex.DiscPer, e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    dgDetails[ColIndex.Rate, e.RowIndex].Style.BackColor = Color.White;
                    if (Convert.ToDouble(dgDetails[ColIndex.DiscPer, e.RowIndex].EditedFormattedValue.ToString()) != Convert.ToDouble(dgDetails[ColIndex.TempDiscPer, e.RowIndex].EditedFormattedValue.ToString()))
                    {
                        dgDetails[ColIndex.IsChange, e.RowIndex].Value = 1;
                        dgDetails[ColIndex.DiscPer, e.RowIndex].Style.BackColor = Color.LightSkyBlue;
                    }
                    else
                    {
                        if (Convert.ToDouble(dgDetails[ColIndex.Rate, e.RowIndex].EditedFormattedValue.ToString()) == Convert.ToDouble(dgDetails[ColIndex.TempRate, e.RowIndex].EditedFormattedValue.ToString()))
                        {
                            dgDetails[ColIndex.IsChange, e.RowIndex].Value = 0;
                        }
                    }
                }
            }
        }

        private void dgDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void dgDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnApplyChange.Focus();
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

        private void dgDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgDetails.CurrentCell.ColumnIndex == ColIndex.Rate)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtRate_TextChanged);
            }
            if (dgDetails.CurrentCell.ColumnIndex == ColIndex.DiscPer)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtDiscPer_TextChanged);
            }
        }

        public void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgDetails.CurrentCell.ColumnIndex == ColIndex.Rate)
                {
                    ObjFunction.SetMasked((TextBox)sender, 2, 5, OMFunctions.MaskedType.NotNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtDiscPer_TextChanged(object sender, EventArgs e)
        {
            if (dgDetails.CurrentCell.ColumnIndex == ColIndex.DiscPer)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void btnApplyChange_Click(object sender, EventArgs e)
        {
            if (dgDetails.Rows.Count <= 0)
            {
                DisplayMessage("Atleast one item required.");
                cmbCustomer.Focus();
                return;
            }
            bool flag = false;

            dbMLedger = new DBMLedger();
            for (int i = 0; i < dgDetails.Rows.Count; i++)
            {
                if (Convert.ToInt64(dgDetails.Rows[i].Cells[ColIndex.IsChange].EditedFormattedValue.ToString()) == 1)
                {
                    mLedgerRateSetting = new MLedgerRateSetting();
                    mLedgerRateSetting.PkSrNo = Convert.ToInt64(dgDetails[ColIndex.PkSrNo, i].Value);
                    mLedgerRateSetting.Rate = Convert.ToDouble(dgDetails[ColIndex.Rate, i].Value);
                    mLedgerRateSetting.DiscPercentage = Convert.ToDouble(dgDetails[ColIndex.DiscPer, i].Value);
                    dbMLedger.UpdateLedgerRateSetting(mLedgerRateSetting);
                    flag = true;
                }
            }
            if (flag == true)
            {
                if (dbMLedger.ExecuteNonQueryStatements())
                {
                    DisplayMessage("Party Wise Rate Save Successfully!!!");
                    BindGrid();
                }
                else
                    DisplayMessage("Party Wise Rate Not Save");
            }
            else
            {
                DisplayMessage("Change Atleast one item.");
                dgDetails.CurrentCell = dgDetails[ColIndex.Rate, 0];
                dgDetails.Focus();
            }
        }


    }
}
