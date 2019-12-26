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
    public partial class StockGroup : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMItemGroup dbMItemGroup = new DBMItemGroup();
        public static long RequestStockGroupNo;
        int GrType;

        public StockGroup()
        {
            InitializeComponent();
        }
        public StockGroup(int GroupType)
        {
            InitializeComponent();
            if (GroupType == 2)
            {
                GrType = GroupType;
                this.Text = "Category (Search)";
                this.Name = "Category (Search)";
               
            }
            else if (GroupType == 3)
            {
                GrType = GroupType;
                this.Text = "Brand Name(Search)";
                this.Name = "Brand Name(Search)";
               
            }
            else if (GroupType == 4)
            {
                GrType = GroupType;
                this.Text = "Department (Search)";
                this.Name = "Department (Search)";

            }
        }

        private void StockGroup_Load(object sender, EventArgs e)
        {
           
            
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            if (GrType == 2)
            {
                CmbSearch.Items.Add(new Item("Category Name", "StockGroupName"));
            }
            if (GrType == 3)
            {
                CmbSearch.Items.Add(new Item("Brand Name", "StockGroupName"));
            }
            if (GrType == 4)
            {
                CmbSearch.Items.Add(new Item("Department Name", "StockGroupName"));
            }
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
                dv = dbMItemGroup.GetBySearch(itm.Value, TxtSearch.Text, GrType);


                DataGridView1.DataSource = dv;
                if (GrType != 3)
                    DataGridView1.Columns[2].Visible = false;
                if (GrType == 2)
                {

                    DataGridView1.Columns[1].HeaderText = "Category Name";
                }
                else if (GrType == 3)
                {
                    DataGridView1.Columns[1].HeaderText = "Brand Name";
                }
                else if (GrType == 4)
                {
                    DataGridView1.Columns[1].HeaderText = "Department Name";
                }
                DataGridView1.Columns[0].Visible = false;
                if (GrType != 3)
                    DataGridView1.Columns[1].Width = DataGridView1.Width - 100;
                else
                    DataGridView1.Columns[1].Width = DataGridView1.Width - 200;
                DataGridView1.Columns[2].Width = 100;
                DataGridView1.Columns[3].Width = 100;
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            RequestStockGroupNo = 0;
            Form NewF = new ItemGroupAE(GrType);
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (TxtSearch.Text != "")
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
                DataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestStockGroupNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new ItemGroupAE(GrType);
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        

        private void btnCancel_Click(object sender, EventArgs e)
        {

            RequestStockGroupNo = 0;
            Form NewF = new ItemGroupAE(GrType);
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {               
               ObjFunction.SetGridStatus(e);                
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
