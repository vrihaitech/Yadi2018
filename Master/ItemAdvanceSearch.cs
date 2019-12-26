using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;
using System.Data.SqlClient;
using OMControls;

namespace Yadi.Master
{
    public partial class ItemAdvanceSearch : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Color clrColorRow = Color.FromArgb(255, 224, 192);
        public long ItemNo = 0;
        public string BarCode = "";
        public static string serachText="";
        public  ItemAdvanceSearch()
        {
            
            InitializeComponent();
            TxtSearch.Text = serachText;
        }

        public void BindGrid()
        {
            try
            {
                dataGridView1.DataSource = null;
                //while (dataGridView1.Rows.Count > 0)
                //  dataGridView1.Rows.RemoveAt(0);

                DataView dv = new DataView();
                string str = TxtSearch.Text;
                if (str != "")
                {
                    dv = GetBySearch(0, TxtSearch.Text);
                    dataGridView1.DataSource = dv;

                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.CurrentCell = dataGridView1[1, 0];
                        dataGridView1_CurrentCellChanged(dataGridView1, new EventArgs());
                    }
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = 400;
                    dataGridView1.Columns[2].Width = 400;
                    dataGridView1.Columns[3].Width = 150;
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public DataView GetBySearch(int Column, string Value)
        {

            string sql = null;

            sql = "  SELECT mItemMaster.ItemNo,MItemGroup.ItemGroupName + '-' + mItemMaster.ItemShortName  AS 'Short Desc.' , MItemGroup.ItemGroupName + '-' + mItemMaster.ItemName  AS 'Item Desc.',mItemMaster.Barcode  FROM mItemMaster INNER JOIN MItemGroup ON mItemMaster.GroupNo = MItemGroup.ItemGroupNo  where mItemMaster.ItemNo<>1 AND (MItemGroup.ItemGroupName + '-' + mItemMaster.ItemName like '%" + Value.Trim().Replace("'", "''").Replace(" ", "%") + "' + '%' or MItemGroup.ItemGroupName + '-' + mItemMaster.ItemShortName like '%" + Value.Trim().Replace("'", "''").Replace(" ", "%") + "' + '%') AND mItemMaster.IsActive='true' order by MItemGroup.ItemGroupName + '-' + mItemMaster.ItemName";

            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException e)
            {
                CommonFunctions.ErrorMessge = e.Message;
            }
            return ds.Tables[0].DefaultView;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid();

        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            serachText = TxtSearch.Text;
            btnSearch_Click(sender, e);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Focus();
                    dataGridView1.CurrentCell = dataGridView1[1, 0];
                }
            }
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ItemNo = Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                BarCode = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString();
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Home)
            {
                TxtSearch.Focus();
            }
        }

        private void AdvancedSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell != null)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clrColorRow;
                    dataGridView1.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void ItemAdvanceSearch_Load(object sender, EventArgs e)
        {
            TxtSearch.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
            dataGridView1.RowTemplate.DefaultCellStyle.Font = null;
            dataGridView1.DefaultCellStyle.Font = ObjFunction.GetFont(FontStyle.Regular, 12);
        }
    }
}
