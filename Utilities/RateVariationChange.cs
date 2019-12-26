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
    public partial class RateVariationChange : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMItemMaster dbMItemMaster = new DBMItemMaster();

        public RateVariationChange()
        {
            InitializeComponent();
        }

        private void RateVariationChange_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbBrand, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
            cmbBrand.Focus();
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
            else if (e.KeyCode == Keys.F3)
            {
                txtLower.Focus();
            }
            else if (e.KeyCode == Keys.F4)
            {
                txtHigher.Focus();
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnApply_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
               btnClear_Click(sender, e);
            }
        }
        #endregion

        public void BindGrid()
        {
            txtHigher.Text = "";
            txtLower.Text = "";
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);

            string Sql = " SELECT     0 AS SrNo, MItemGroup.StockGroupName + '-' + mItemMaster.ItemName AS ItemName, ISNULL(MStockItems.LowerVariation, 0) AS LowerVariation,  ISNULL(MStockItems.HigherVariation, 0) AS HigherVariation, ISNULL(MStockItems.LowerVariation, 0) AS LV, ISNULL(MStockItems.HigherVariation, 0) AS HV,  mItemMaster.ItemNo, 0 AS IsChange " +
                        " FROM MStockGroup INNER JOIN MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo " +
                        " Where mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbBrand) + " " +
                        " Order By MItemGroup.StockGroupName + '-' + mItemMaster.ItemName ";
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
                if (ObjFunction.GetComboValue(cmbBrand) != 0)
                {
                    BindGrid();
                    if (dgDetails.RowCount > 0)
                    {
                        dgDetails.CurrentCell = dgDetails[ColIndex.LowerVariation, 0];
                        dgDetails.Focus();
                    }
                }
                else
                {
                    DisplayMessage("Please Select Brand");
                    cmbBrand.Focus();
                }
            }
        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int LowerVariation = 2;
            public static int HigherVariation = 3;
            public static int LV = 4;
            public static int HV = 5;
            public static int ItemNo = 6;
            public static int IsChange = 7;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);
            txtHigher.Text = "";
            txtLower.Text = "";
            cmbBrand.SelectedIndex = 0;
            cmbBrand.Focus();
        }

        private void dgDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.LowerVariation)
            {
                if (dgDetails[ColIndex.LowerVariation, e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    dgDetails[ColIndex.LowerVariation, e.RowIndex].Style.BackColor = Color.White;
                    if (Convert.ToDouble(dgDetails[ColIndex.LowerVariation, e.RowIndex].EditedFormattedValue.ToString()) != Convert.ToDouble(dgDetails[ColIndex.LV, e.RowIndex].EditedFormattedValue.ToString()))
                    {
                        dgDetails[ColIndex.IsChange, e.RowIndex].Value = 1;
                        dgDetails[ColIndex.LowerVariation, e.RowIndex].Style.BackColor = Color.LightSkyBlue;
                    }
                    else
                    {
                        if (dgDetails[ColIndex.HigherVariation, e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                        {
                            if (Convert.ToDouble(dgDetails[ColIndex.HigherVariation, e.RowIndex].EditedFormattedValue.ToString()) != Convert.ToDouble(dgDetails[ColIndex.HV, e.RowIndex].EditedFormattedValue.ToString()))
                            {
                                dgDetails[ColIndex.IsChange, e.RowIndex].Value = 0;
                            }
                        }
                    }
                }
            }
            if (e.ColumnIndex == ColIndex.HigherVariation)
            {
                if (dgDetails[ColIndex.HigherVariation, e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                {
                    dgDetails[ColIndex.HigherVariation, e.RowIndex].Style.BackColor = Color.White;
                    if (Convert.ToDouble(dgDetails[ColIndex.HigherVariation, e.RowIndex].EditedFormattedValue.ToString()) != Convert.ToDouble(dgDetails[ColIndex.HV, e.RowIndex].EditedFormattedValue.ToString()))
                    {
                        dgDetails[ColIndex.IsChange, e.RowIndex].Value = 1;
                        dgDetails[ColIndex.HigherVariation, e.RowIndex].Style.BackColor = Color.LightSkyBlue;
                    }
                    else
                    {
                        if (dgDetails[ColIndex.LowerVariation, e.RowIndex].EditedFormattedValue.ToString().Trim() != "")
                        {
                            if (Convert.ToDouble(dgDetails[ColIndex.LowerVariation, e.RowIndex].EditedFormattedValue.ToString()) != Convert.ToDouble(dgDetails[ColIndex.LV, e.RowIndex].EditedFormattedValue.ToString()))
                            {
                                dgDetails[ColIndex.IsChange, e.RowIndex].Value = 0;
                            }
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
            if (dgDetails.CurrentCell.ColumnIndex == ColIndex.LowerVariation)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtPer_TextChanged);
            }
            if (dgDetails.CurrentCell.ColumnIndex == ColIndex.HigherVariation)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtPer_TextChanged);
            }
        }

        //private void btnApplyChange_Click(object sender, EventArgs e)
        //{
        //    if (dgDetails.Rows.Count <= 0)
        //    {
        //        DisplayMessage("Atleast one item required.");
        //        cmbBrand.Focus();
        //        return;
        //    }
        //    bool flag = false;

        //    dbMLedger = new DBMLedger();
        //    for (int i = 0; i < dgDetails.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt64(dgDetails.Rows[i].Cells[ColIndex.IsChange].EditedFormattedValue.ToString()) == 1)
        //        {
        //            mLedgerRateSetting = new MLedgerRateSetting();
        //            mLedgerRateSetting.PkSrNo = Convert.ToInt64(dgDetails[ColIndex.PkSrNo, i].Value);
        //            mLedgerRateSetting.Rate = Convert.ToDouble(dgDetails[ColIndex.Rate, i].Value);
        //            dbMLedger.UpdateLedgerRateSetting(mLedgerRateSetting);
        //            flag = true;
        //        }
        //    }
        //    if (flag == true)
        //    {
        //        if (dbMLedger.ExecuteNonQueryStatements())
        //        {
        //            DisplayMessage("Party Wise Rate Save Successfully!!!");
        //            BindGrid();
        //        }
        //        else
        //            DisplayMessage("Party Wise Rate Not Save");
        //    }
        //    else
        //    {
        //        DisplayMessage("Change Atleast one item.");
        //        dgDetails.CurrentCell = dgDetails[ColIndex.Rate, 0];
        //        dgDetails.Focus();
        //    }
        //}

        private void txtPer_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 2, OMFunctions.MaskedType.NotNegative);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (dgDetails.Rows.Count <= 0)
            {
                DisplayMessage("Atleast one item required.");
                cmbBrand.Focus();
                return;
            }
            if (txtLower.Text.Trim() != "" || txtHigher.Text.Trim() != "")
            {
                for (int i = 0; i < dgDetails.Rows.Count; i++)
                {
                    dgDetails[ColIndex.IsChange, i].Value = 1;
                    if (txtLower.Text.Trim() != "")
                    {
                        dgDetails.Rows[i].Cells[ColIndex.LowerVariation].Value = txtLower.Text;
                        dgDetails[ColIndex.LowerVariation, i].Style.BackColor = Color.LightSkyBlue;
                    }
                    if (txtHigher.Text.Trim() != "")
                    {
                        dgDetails.Rows[i].Cells[ColIndex.HigherVariation].Value = txtHigher.Text; dgDetails[ColIndex.HigherVariation, i].Style.BackColor = Color.LightSkyBlue;
                    }
                }
            }
            else
            {
                DisplayMessage("Please Enter Lower Or Higher Value");
            }
        }

        private void btnApplyChange_Click(object sender, EventArgs e)
        {
            if (dgDetails.Rows.Count <= 0)
            {
                DisplayMessage("Atleast one item required.");
                cmbBrand.Focus();
                return;
            }
            bool flag = false;
             dbMItemMaster = new DBMItemMaster();
            for (int i = 0; i < dgDetails.Rows.Count; i++)
            {
                if (Convert.ToInt64(dgDetails.Rows[i].Cells[ColIndex.IsChange].Value.ToString()) != 0)
                {
                    flag = true;

                    //=============umesh
                   // dbMItemMaster.UpdateStockItemsVariationChange(Convert.ToInt64(dgDetails.Rows[i].Cells[ColIndex.ItemNo].Value),
                   ///     Convert.ToDouble(dgDetails.Rows[i].Cells[ColIndex.LowerVariation].Value),
                      //  Convert.ToDouble(dgDetails.Rows[i].Cells[ColIndex.HigherVariation].Value));
                }
            }
            if (flag == true)
            {
                if (dbMItemMaster.ExecuteNonQueryStatements())
                {
                    DisplayMessage("Rate Variation Change Save Successfully!!!");
                    BindGrid();
                }
                else
                    DisplayMessage("Rate Variation Change Not Save");
            }
            else
            {
                DisplayMessage("Change Atleast one item.");
                dgDetails.CurrentCell = dgDetails[ColIndex.HigherVariation, 0];
                dgDetails.Focus();
            }
        }
    }
}
