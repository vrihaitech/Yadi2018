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
using System.Data.SqlClient;

namespace Yadi.Master
{
    public partial class ItemMasterSearchAutoOld : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
               Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMItemMaster dbMitemMaster = new DBMItemMaster();
        public static long RequestItemNo;

        public ItemMasterSearchAutoOld()
        {
            InitializeComponent();
        }

        private void StockItem_Load(object sender, EventArgs e)
        {
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("Brand", "Brand"));
            CmbSearch.Items.Add(new Item("Product", "Product"));
            CmbSearch.Items.Add(new Item("Vehicle", "Vehicle"));
            CmbSearch.Items.Add(new Item("ItemName", "ItemName"));
            CmbSearch.Items.Add(new Item("ItemShortName", "ItemShortName"));
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

                dv = GetBySearch(itm.Value, TxtSearch.Text);
    

                DataGridView1.DataSource = dv;
                DataGridView1.Columns[0].Visible = false;
                DataGridView1.Columns[1].Width = 200;
                DataGridView1.Columns[2].Width = 200;
                DataGridView1.Columns[3].Width = 200;
                DataGridView1.Columns[4].Width = 200;
                DataGridView1.Columns[5].Width = 200;
                DataGridView1.Columns[6].Width = 70;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public DataView GetBySearch(string Column, string Value)
        {


            string sql = null;
            switch (Column)
            {
                case "0":

                    sql = " SELECT     MItemMaster.ItemNo, MItemGroup_1.ItemGroupName AS 'Brand', MItemGroup_2.ItemGroupName  AS 'Product', MItemGroup.ItemGroupName AS 'Veicle',  " +
                   " MItemMaster.ItemName AS 'ItemName', MItemMaster.Barcode AS 'Barcode',  CASE WHEN (MItemMaster.IsActive = 'True') THEN 'True' ELSE 'False' END AS 'Status' " +
                   " FROM         MItemMaster INNER JOIN     MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                   " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                   " MItemGroup AS MItemGroup_2 ON MItemMaster.FkCategoryNo = MItemGroup_2.ItemGroupNo " +
                   " order by MItemGroup_1.ItemGroupName, MItemGroup_2.ItemGroupName,MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemMaster.Barcode";
                    break;
                case "ItemName":
                    sql = " SELECT     MItemMaster.ItemNo, MItemGroup_1.ItemGroupName AS 'Brand', MItemGroup_2.ItemGroupName  AS 'Product', MItemGroup.ItemGroupName AS 'Veicle',  " +
                    " MItemMaster.ItemName AS 'ItemName', MItemMaster.Barcode AS 'Barcode',  CASE WHEN (MItemMaster.IsActive = 'True') THEN 'True' ELSE 'False' END AS 'Status' " +
                    " FROM         MItemMaster INNER JOIN     MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                    " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                    " MItemGroup AS MItemGroup_2 ON MItemMaster.FkCategoryNo = MItemGroup_2.ItemGroupNo " +
                    " Where ItemNo<>1 AND MItemGroup.ItemGroupName + '-' + MItemMaster.ItemName like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup_1.ItemGroupName, MItemGroup_2.ItemGroupName,MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemMaster.Barcode";
                    break;
                case "ItemShortName":
                    sql = " SELECT     MItemMaster.ItemNo, MItemGroup_1.ItemGroupName AS 'Brand', MItemGroup_2.ItemGroupName  AS 'Product', MItemGroup.ItemGroupName AS 'Veicle',  " +
                 " MItemMaster.ItemName AS 'ItemName', MItemMaster.Barcode AS 'Barcode',  CASE WHEN (MItemMaster.IsActive = 'True') THEN 'True' ELSE 'False' END AS 'Status' " +
                 " FROM         MItemMaster INNER JOIN     MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                 " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                 " MItemGroup AS MItemGroup_2 ON MItemMaster.FkCategoryNo = MItemGroup_2.ItemGroupNo " +
                 " Where ItemNo<>1 AND  MItemMaster.Barcode  like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup_1.ItemGroupName, MItemGroup_2.ItemGroupName,MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemMaster.Barcode";

                    break;
                case "Brand":
                    sql = " SELECT     MItemMaster.ItemNo, MItemGroup_1.ItemGroupName AS 'Brand', MItemGroup_2.ItemGroupName  AS 'Product', MItemGroup.ItemGroupName AS 'Veicle',  " +
                      " MItemMaster.ItemName AS 'ItemName', MItemMaster.Barcode AS 'Barcode',  CASE WHEN (MItemMaster.IsActive = 'True') THEN 'True' ELSE 'False' END AS 'Status' " +
                      " FROM         MItemMaster INNER JOIN     MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                      " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                      " MItemGroup AS MItemGroup_2 ON MItemMaster.FkCategoryNo = MItemGroup_2.ItemGroupNo " +
                      " Where ItemNo<>1 AND MItemGroup_1.ItemGroupName like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup_1.ItemGroupName, MItemGroup_2.ItemGroupName,MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemMaster.Barcode";

                    break;
                case "Product":
                    sql = " SELECT     MItemMaster.ItemNo, MItemGroup_1.ItemGroupName AS 'Brand', MItemGroup_2.ItemGroupName  AS 'Product', MItemGroup.ItemGroupName AS 'Veicle',  " +
                  " MItemMaster.ItemName AS 'ItemName', MItemMaster.Barcode AS 'Barcode',  CASE WHEN (MItemMaster.IsActive = 'True') THEN 'True' ELSE 'False' END AS 'Status' " +
                  " FROM         MItemMaster INNER JOIN     MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                  " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                  " MItemGroup AS MItemGroup_2 ON MItemMaster.FkCategoryNo = MItemGroup_2.ItemGroupNo " +
                  " Where ItemNo<>1 AND MItemGroup_2.ItemGroupName like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup_1.ItemGroupName, MItemGroup_2.ItemGroupName,MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemMaster.Barcode";

                    break;
                case "Vehicle":
                    sql = " SELECT     MItemMaster.ItemNo, MItemGroup_1.ItemGroupName AS 'Brand', MItemGroup_2.ItemGroupName  AS 'Product', MItemGroup.ItemGroupName AS 'Veicle',  " +
              " MItemMaster.ItemName AS 'ItemName', MItemMaster.Barcode AS 'Barcode',  CASE WHEN (MItemMaster.IsActive = 'True') THEN 'True' ELSE 'False' END AS 'Status' " +
              " FROM         MItemMaster INNER JOIN     MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
              " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
              " MItemGroup AS MItemGroup_2 ON MItemMaster.FkCategoryNo = MItemGroup_2.ItemGroupNo " +
              " Where ItemNo<>1 AND MItemGroup.ItemGroupName like '%" + Value.Trim().Replace("'", "''") + "' + '%' order by MItemGroup_1.ItemGroupName, MItemGroup_2.ItemGroupName,MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemMaster.Barcode";

                    break;
            }
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
            if (e.KeyCode == Keys.Enter)
            {
                BindGrid();
                DataGridView1.Focus();
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
            RequestItemNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new ItemMasterAUTOAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //===umesh test msg
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
            if (e.ColumnIndex == 6)
            {
                ObjFunction.SetGridStatus(e);
            }
        }
    }
}

