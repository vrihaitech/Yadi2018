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
    public partial class CashDenomination : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        public delegate void MovetoNext(int RowIndex, int ColIndex);
        public void m2n(int RowIndex, int ColIndex)
        {
            dgCashDenomination.CurrentCell = dgCashDenomination.Rows[RowIndex].Cells[ColIndex];
        }

        public CashDenomination()
        {
            InitializeComponent();
        }

        private void CashDenomination_Load(object sender, EventArgs e)
        {
            try
            {
                dgCashDenomination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));

                fillGrid();
                // txtTotal.Font = new Font(txtTotal.Font, FontStyle.Bold);

                dgCashDenomination.Columns[ColIndex.SrNO].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCashDenomination.Columns[ColIndex.Deno].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCashDenomination.Columns[ColIndex.Pieces].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCashDenomination.Columns[ColIndex.AmountRs].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                lblTotal.Font = ObjFunction.GetFont(FontStyle.Bold, 14);
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { RowsIndex.Notes + 1, ColIndex.Pieces });
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void fillGrid()
        {
            try
            {
                double[] arrayDenomination = new double[] { 2000, 500, 200, 100, 50, 20, 10, 5, 2, 1, 0.5 };
                DataTable dt = new DataTable();

                //creating columns in tables
                DataColumn colSrNo = new DataColumn("SrNo");
                DataColumn colDeno = new DataColumn("DENO.");
                DataColumn colPieces = new DataColumn("PIECES");
                DataColumn colAmountRs = new DataColumn("AMOUNT");

                //creating datatype of columns
                colSrNo.DataType = System.Type.GetType("System.String");
                colDeno.DataType = System.Type.GetType("System.Double");
                colPieces.DataType = System.Type.GetType("System.Int64");
                colAmountRs.DataType = System.Type.GetType("System.Double");

                //adding columns to table
                dt.Columns.Add(colSrNo);
                dt.Columns.Add(colDeno);
                dt.Columns.Add(colPieces);
                dt.Columns.Add(colAmountRs);

                //adding rows to table
                DataRow row;
                int j = 1, k = 6;
                for (int i = 0; i < 13; i++)
                {
                    row = dt.NewRow();
                    if (i == 0)
                    {
                        row[colSrNo] = "NOTES";
                        dt.Rows.Add(row);
                    }
                    else if (i == 8)
                    {
                        row[colSrNo] = "COINS";
                        dt.Rows.Add(row);
                    }
                    else if (i > 0 && i < 8)
                    {
                        row[colSrNo] = i;
                        row[colDeno] = arrayDenomination[i - 1];
                        row[colPieces] = 0;
                        row[colAmountRs] = 0;
                        dt.Rows.Add(row);
                    }
                    else
                    {
                        row[colSrNo] = j++;
                        row[colDeno] = arrayDenomination[k++];
                        row[colPieces] = 0;
                        row[colAmountRs] = 0;
                        dt.Rows.Add(row);
                    }
                }

                //adding table to datagridview
                this.dgCashDenomination.DataSource = dt;
                this.dgCashDenomination.RowHeadersVisible = false;

                this.dgCashDenomination.Columns[ColIndex.SrNO].ReadOnly = true;
                this.dgCashDenomination.Columns[ColIndex.Deno].ReadOnly = true;
                this.dgCashDenomination.Columns[ColIndex.Pieces].ReadOnly = false;
                this.dgCashDenomination.Columns[ColIndex.AmountRs].ReadOnly = true;

                //lblTotal.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgCashDenomination_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //updating amount column after pieces column edited
                for (int i = 1; i < 8; i++)
                {
                    double tempDeno = Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.Deno].Value.ToString());
                    long tempPieces = Convert.ToInt64(dgCashDenomination.Rows[i].Cells[ColIndex.Pieces].Value.ToString());
                    dgCashDenomination.Rows[i].Cells[ColIndex.AmountRs].Value = tempDeno * Convert.ToDouble(tempPieces);
                }

                for (int i = 9; i < dgCashDenomination.RowCount; i++)
                {
                    double tempDeno = Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.Deno].Value.ToString());
                    double tempPieces = Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.Pieces].Value.ToString());
                    dgCashDenomination.Rows[i].Cells[ColIndex.AmountRs].Value = tempDeno * tempPieces;
                }

                //calculating total amount after pieces column edited
                double tempAmount = 0;
                for (int i = 1; i < 8; i++)
                {
                    tempAmount = tempAmount + Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.AmountRs].FormattedValue.ToString());
                }
                for (int i = 9; i < dgCashDenomination.RowCount; i++)
                {
                    tempAmount = tempAmount + Convert.ToDouble(dgCashDenomination.Rows[i].Cells[ColIndex.AmountRs].FormattedValue.ToString());
                }

                //putting value to total textbox
                lblTotal.Text = tempAmount.ToString();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            fillGrid();
            lblTotal.Text = "0.00";
            dgCashDenomination.CurrentCell = dgCashDenomination.Rows[RowsIndex.Notes + 1].Cells[ColIndex.Pieces];
            dgCashDenomination.Rows[RowsIndex.Notes + 1].Cells[ColIndex.Pieces].Selected = true;
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNO = 0;
            public static int Deno = 1;
            public static int Pieces = 2;
            public static int AmountRs = 3;
        }
        #endregion

        #region RowIndex
        public static class RowsIndex
        {
            public static int Notes = 0;
            public static int Coins = 8;
        }
        #endregion

        private void dgCashDenomination_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgCashDenomination.Columns[ColIndex.Pieces].Index)
                {
                    if (e.RowIndex != dgCashDenomination.Rows[RowsIndex.Notes].Index && e.RowIndex != dgCashDenomination.Rows[RowsIndex.Coins].Index)
                    {
                        // !int.TryParse(Convert.ToString(e.FormattedValue), out i))
                        if (ObjFunction.CheckNumeric(e.FormattedValue.ToString()) == false)
                        {
                            e.Cancel = true;
                            OMMessageBox.Show("Please Enter Only Numbers...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgCashDenomination_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dgCashDenomination.Rows[RowsIndex.Notes].Cells[ColIndex.Pieces].ReadOnly = true;
            this.dgCashDenomination.Rows[RowsIndex.Coins].Cells[ColIndex.Pieces].ReadOnly = true;
        }

        private void dgCashDenomination_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == 8)
            {
                e.Cancel = true;
            }
        }

        private void dgCashDenomination_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (dgCashDenomination.CurrentRow.Index == 7)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { RowsIndex.Coins + 1, ColIndex.Pieces });
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (dgCashDenomination.CurrentRow.Index == 9)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { RowsIndex.Coins - 1, ColIndex.Pieces });
                }
            }
        }
    }
}
