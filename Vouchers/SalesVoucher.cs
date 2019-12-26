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
    public partial class SalesVoucher : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        public static long RequestSVNo,GroupNo;

        public SalesVoucher()
        {
            InitializeComponent();
        }

        private void SalesVoucher_Load(object sender, EventArgs e)
        {
           // GroupNo = 15;
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("Ledger Name", "LedgerName"));
            CmbSearch.SelectedIndex = 0;

            BindGrid();
            TxtSearch.Focus();
        }

        private void BindGrid()
        {
            DataView dv = new DataView();
            Item itm = (Item)CmbSearch.SelectedItem;
            dv = dbTVoucherEntry.GetBySearch(itm.Value, TxtSearch.Text,VchType.Sales);

            
            DataGridView1.DataSource = dv;
            DataGridView1.Columns[0].Visible = false;
            DataGridView1.Columns[1].Width = 50;
            DataGridView1.Columns[2].Width = 448;
            DataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestSVNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new SalesVoucherAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (TxtSearch.Text != "" && CmbSearch.SelectedIndex != 0)
            {
                BindGrid();
            }
            else
            {
                DataGridView1.DataSource = null;
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            CmbSearch.SelectedIndex = 0;
            BindGrid();  
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            RequestSVNo = 0;
            Form NewF = new SalesVoucherAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (TxtSearch.Text != "*")
                BtnSearch_Click(sender, e);
            else
                BindGrid();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (DataGridView1.Rows.Count > 0)
                {
                    DataGridView1.Focus();
                    DataGridView1.CurrentCell = DataGridView1[1, 0];
                }
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
            {
                DataGridView1_CellClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
        }
    }
}
