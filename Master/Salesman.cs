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
    public partial class Salesman : Form
    {
        OMCommonClass cc = new OMCommonClass();
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMSalesman dbSalesman = new DBMSalesman();
        public static long RequestSalesmanNo;

        public Salesman()
        {
            InitializeComponent();
        }

        private void Salesman_Load(object sender, EventArgs e)
        {
            
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("Name", "SalesmanName"));
            CmbSearch.Items.Add(new Item("Salesman. Code", "SalesmanUserNo"));
            CmbSearch.SelectedIndex = 1;

            BindGrid();
            TxtSearch.Focus();
        }

        private void BindGrid()
        {
            DataView dv = new DataView();
            Item itm = (Item)CmbSearch.SelectedItem;
            dv = dbSalesman.GetBySearch(itm.Value, TxtSearch.Text);

            
            DataGridView1.DataSource = dv;
            DataGridView1.Columns[0].Visible = false;
            DataGridView1.Columns[2].Width = DataGridView1.Width - (DataGridView1.Columns[1].Width + 5);
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
            //RequestSalesmanNo = 0;
            //Form NewF = new SalesmanAE();
            //this.Close();
            //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
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

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
            {
                DataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestSalesmanNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new SalesmanAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RequestSalesmanNo = 0;
            Form NewF = new SalesmanAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
    }
}
