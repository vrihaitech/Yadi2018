using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OMControls;

namespace OM
{
    public partial class ShowStockLocation : Form
    {
      
        public ShowStockLocation()
        {
            InitializeComponent();
        }

        static ShowStockLocation newstock;

        #region Stock Godown related Methods
        double tQty = 0;
        DataTable dt = null;
        CommonFunctions ObjFunction = new CommonFunctions();


        public  void DisplayStockGodown(DataTable dt, Double tQty)
        {


            newstock = new ShowStockLocation();
            newstock.dt = dt;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newstock.dgStockGodown.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                   newstock.dgStockGodown.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
            //dgStockGodown.DataSource = dtBillCollect[dgBill.CurrentCell.RowIndex];
            if (newstock.dgStockGodown.Rows.Count > 0)
            {
                newstock.CalculateGodownQty();
                newstock.dgStockGodown.Columns[0].Visible = false;
                newstock.dgStockGodown.Columns[1].ReadOnly = true;
                newstock.dgStockGodown.Columns[1].Width = 145;
                newstock.dgStockGodown.Columns[2].Width = 68;
                newstock.dgStockGodown.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                newstock.dgStockGodown.Columns[3].Width = 78;
                newstock.dgStockGodown.Columns[3].ReadOnly = true;
                newstock.dgStockGodown.Columns[4].Visible = false;

                newstock.dgStockGodown.CurrentCell = newstock.dgStockGodown[2, 0];
                newstock.dgStockGodown.Focus();
            }

            newstock.ShowDialog();
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
                        dgStockGodown.Rows[e.RowIndex].Cells[3].Value = Convert.ToDouble(dgStockGodown.Rows[e.RowIndex].Cells[2].Value) * tQty;
                        dgStockGodown.CurrentCell.ErrorText = "";
                    }
                    else
                    {
                        //dgStockGodown.CurrentCell.ErrorText = "Enter Valid Amount";
                        dgStockGodown.CurrentCell.Value = "0";
                        dgStockGodown.Rows[e.RowIndex].Cells[3].Value = "0";
                        dgStockGodown.CurrentCell = dgStockGodown[dgStockGodown.CurrentCell.ColumnIndex, dgStockGodown.CurrentCell.RowIndex];
                    }
                    CalculateGodownQty();
                }
                else
                {
                    dgStockGodown.CurrentCell.Value = 0;
                }
            }
        }

        private void btnStkGodownOk_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtStockGodwnQty.Text) == Convert.ToDouble(tQty))
            {
                this.Close();
            }
            else
            {
                OMMessageBox.Show("Item Bill Quantity and Stock Godown Quantity doesn't match.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                dgStockGodown.Focus();
            }
        }

        private void dgStockGodown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStkGodownOk.Focus();
            }
        }
        #endregion
    }
}