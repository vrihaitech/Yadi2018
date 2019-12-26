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
    public partial class UOM : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMUOM dbUOM = new DBMUOM();
        public static long RequestUOMNo;

        public UOM()
        {
            InitializeComponent();
        }

        private void UOM_Load(object sender, EventArgs e)
        {
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("UOM Name", "UOMName"));
            CmbSearch.Items.Add(new Item("UOM. Short Code", "UOMShortCode"));
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
                dv = dbUOM.GetBySearch(itm.Value, TxtSearch.Text);
                DataGridView1.DataSource = dv;
                DataGridView1.Columns[0].Visible = false;
                DataGridView1.Columns[2].Width = 522;
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
            RequestUOMNo = 0;
            Form NewF = new UomAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            BtnSearch_Click(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RequestUOMNo = 0;
            Form NewF = new UomAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

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
            else if (e.KeyCode == Keys.Enter)
            {
                DataGridView1.Focus();
            }
        }

        public void SetStatus(DataGridViewCellFormattingEventArgs ex,Color Active,Color DeActive)
        {
            if (ex.Value.ToString()=="1")
            {
                ex.Value = "Active";
                ex.CellStyle.ForeColor = Active;
            }
            else if (ex.Value.ToString()=="0")
            {
                ex.Value = "DeActive";
                ex.CellStyle.ForeColor = DeActive;
            }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestUOMNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new UomAE();
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

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView1.RowCount >= 1)
            {
                if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
                {
                    DataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
                }
            }
        }

        private void CmbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSearch.Focus();
                TxtSearch.Text = "";
            }
        }
    }
}
