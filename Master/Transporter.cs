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
    public partial class Transporter : Form
    {
        
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMLedger dbLedger = new DBMLedger();
        //DBMTransporter dbmtransporter = new DBMTransporter();

        public static long RequestTransporterNo;

        #region init
        public Transporter()
        {
            InitializeComponent();
        }

        private void Transporter_Load(object sender, EventArgs e)
        {
            
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("Transporter Name", "LedgerName"));
            
            CmbSearch.SelectedIndex = 0;

            BindGrid();
            TxtSearch.Focus();
        }

        private void BindGrid()
        {
            DataView dv = new DataView();
            Item itm = (Item)CmbSearch.SelectedItem;
            dv = dbLedger.GetBySearch(itm.Value, TxtSearch.Text, GroupType.Transporter);
            
            DataGridView1.DataSource = dv;
            DataGridView1.Columns[0].Visible = false;
            DataGridView1.Columns[1].Width = 400;
           // DataGridView1.Columns[3].Width = 100;
            
        }
        #endregion

        #region Click
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

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            BtnSearch_Click(sender, e);
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestTransporterNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new TransporterAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RequestTransporterNo = 0;
            Form NewF = new TransporterAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
        #endregion

        #region KeyDown
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

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
            {
                DataGridView1_CellContentClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
        }
        #endregion

        private void CmbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtSearch.Focus();
                TxtSearch.Text = "";
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                ObjFunction.SetGridStatus(e);
            }
        }

      

    }
}
