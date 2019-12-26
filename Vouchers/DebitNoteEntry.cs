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
    public partial class DebitNoteEntry : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        public static long RequestDebitNo, VType;

        public DebitNoteEntry()
        {
            InitializeComponent();
            VType = VchType.DebitNote;
            this.Text = "Debit Note(Search)";
            this.Name = "Debit Note(Search)";

        }

        private void VoucherEntry_Load(object sender, EventArgs e)
        {
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("Ledger Name", "LedgerName"));
            CmbSearch.Items.Add(new Item("Date", "VoucherDate"));
            CmbSearch.SelectedIndex = 1;

            BindGrid();
            TxtSearch.Focus();
            dtpSearch.Visible = false;
            dtpSearch.Value = DateTime.Now.Date;
        }

        private void BindGrid()
        {
            DataView dv = new DataView();
            Item itm = (Item)CmbSearch.SelectedItem;
            if (TxtSearch.Visible == true)
                dv = dbTVoucherEntry.GetBySearch(itm.Value, TxtSearch.Text, VType);
            else
                dv = dbTVoucherEntry.GetBySearch(itm.Value, dtpSearch.Text, VType);


            DataGridView1.DataSource = dv;
            DataGridView1.Columns[0].Visible = false;
            DataGridView1.Columns[1].Width = 150;
            DataGridView1.Columns[2].Width = 400;
            DataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }



        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestDebitNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = null;
            NewF = new DebitNoteEntryAE();
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
            TxtSearch.Visible = true;
            dtpSearch.Visible = false;
            BindGrid();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            RequestDebitNo = 0;
            Form NewF = new DebitNoteEntryAE();
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
                DataGridView1_CellContentClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RequestDebitNo = 0;
            Form NewF = new DebitNoteEntryAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void dtpSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BindGrid();
                if (DataGridView1.Rows.Count > 0)
                {
                    DataGridView1.Focus();
                    DataGridView1.CurrentCell = DataGridView1[1, 0];
                }
            }
        }

        private void CmbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSearch.SelectedIndex == 2)
            {
                TxtSearch.Visible = false;
                dtpSearch.Visible = true;
            }
            else
            {
                TxtSearch.Visible = true;
                dtpSearch.Visible = false;
            }
        }

        private void CmbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (CmbSearch.SelectedIndex == 2)
                    dtpSearch.Focus();
                else
                    TxtSearch.Focus();
            }
        }
    }
}
