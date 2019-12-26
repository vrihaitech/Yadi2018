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


namespace Yadi.Master
{
    public partial class ItemLevelDiscountSearch : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMBillDiscount dbDisc = new DBMBillDiscount();
        public static long RequestDiscNo,GroupNo;

        public ItemLevelDiscountSearch()
        {
            InitializeComponent();
        }

        private void Supplier_Load(object sender, EventArgs e)
        {
            
            
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("Doc No", "DiscUserNo"));
            CmbSearch.Items.Add(new Item("Comp. Name", "MfgCompName"));
            //CmbSearch.Items.Add(new Item("Supplier. Short Code", "SupplierShortCode"));
            CmbSearch.SelectedIndex = 1;

            BindGrid();
            TxtSearch.Focus();
        }

        private void BindGrid()
        {
            try
            {
                DataView dv = new DataView();
                Item itm = (Item)CmbSearch.SelectedItem;
                dv = dbDisc.GetBySearch(itm.Value, TxtSearch.Text);


                DataGridView1.DataSource = dv;
                DataGridView1.Columns[0].Visible = false;
                //DataGridView1.Columns[1].Width = 359;
                //DataGridView1.Columns[2].Width = 200;
                //DataGridView1.Columns[3].Width = 89;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (TxtSearch.Text != "" && CmbSearch.SelectedIndex != 0)
            {
                BindGrid();
            }
            else if (CmbSearch.SelectedIndex != 0)
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
            TxtSearch.Text = "";
            BindGrid();
            if (DataGridView1.Rows.Count > 0)
            {
                DataGridView1.Focus();
                DataGridView1.CurrentCell = DataGridView1[1, 0];
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
          
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RequestDiscNo = 0;
            Form NewF = new ItemLevelDiscount();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
            {
                DataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestDiscNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new ItemLevelDiscount();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex == 3)
            //{
            //    ObjFunction.SetGridStatus(e);
            //}
            if (e.ColumnIndex == 3||e.ColumnIndex == 4||e.ColumnIndex == 5)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString(Format.DDMMMYYYY);
            }
            
        }

        private void CmbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSearch.Text = "";
            }
        }
    }
}
