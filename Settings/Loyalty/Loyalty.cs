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

namespace Yadi.Settings.Loyalty
{
    public partial class Loyalty : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMScheme dbMScheme = new DBMScheme();
        /// <summary>
        /// This variable use for Scheme No
        /// </summary>
        public static long RequestSchemeNo;
        private int SchemeTypeNo;

        /// <summary>
        /// This is Class of Constructor
        /// </summary>
        public Loyalty()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This is Class of parameterised Constructor
        /// </summary>
        public Loyalty(int SchemeType)
        {
            SchemeTypeNo = SchemeType;
            InitializeComponent();
        }
        private void Loyalty_Load(object sender, EventArgs e)
        {
            try
            {
                CmbSearch.Items.Add(new Item("-----Select-----", "0"));
                CmbSearch.Items.Add(new Item("Scheme Name", "SchemeName"));
                CmbSearch.Items.Add(new Item("Scheme Number", "SchemeUserNo"));
                CmbSearch.SelectedIndex = 1;

                BindGrid();
                TxtSearch.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BindGrid()
        {
            try
            {
                DataView dv = new DataView();
                Item itm = (Item)CmbSearch.SelectedItem;
                //dv = dbArea.GetBySearch(CmbSearch.SelectedValue.ToString(), TxtSearch.Text);

                dv = dbMScheme.GetBySearch(itm.Value, TxtSearch.Text, SchemeTypeNo);

                DataGridView1.DataSource = dv;
                DataGridView1.Columns[0].Visible = false;
                DataGridView1.Columns[1].Width = 117;
                DataGridView1.Columns[2].Width = 260;
                DataGridView1.Columns[3].Visible = false;
                DataGridView1.Columns[4].Width = 94;
                DataGridView1.Columns[5].Width = 94;
                DataGridView1.Columns[6].Width = 71;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                CmbSearch.SelectedIndex = 1;
                TxtSearch.Text = "";
                BindGrid();
                if (DataGridView1.Rows.Count > 0)
                {
                    DataGridView1.Focus();
                    DataGridView1.CurrentCell = DataGridView1[1, 0];
                }
                CmbSearch.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BtnSearch_Click(sender, e);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
                {
                    DataGridView1_CellClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestSchemeNo = 0;
            RequestSchemeNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);

            if (SchemeTypeNo == 1 || SchemeTypeNo == 2)
            {
                Form NewF = new Settings.Loyalty.LoyaltyMTD(SchemeTypeNo);
                this.Close();
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else
            {
                Form NewF = new Settings.Loyalty.LoyaltyPSKU(SchemeTypeNo);
                this.Close();
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RequestSchemeNo = 0;

            if (SchemeTypeNo == 1 || SchemeTypeNo == 2)
            {
                Form NewF = new Settings.Loyalty.LoyaltyMTD(SchemeTypeNo);
                this.Close();
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else
            {
                Form NewF = new Settings.Loyalty.LoyaltyPSKU(SchemeTypeNo);
                this.Close();
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }

        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
              //  ObjFunction.SetGridStatus(e);
                if (Convert.ToInt64(e.Value) == 0)
                {
                    e.Value = "Draft";
                    e.CellStyle.ForeColor = Color.Blue;
                }
                else if (Convert.ToInt64(e.Value) == 1)
                {
                    e.Value = "Active";
                    e.CellStyle.ForeColor = Color.Green;
                }
                else
                {
                    e.Value = "Closed";
                    e.CellStyle.ForeColor = Color.Red;
                }

            }
            if (e.ColumnIndex == 4 || e.ColumnIndex == 5)
            {
                e.Value = Convert.ToDateTime(e.Value.ToString()).ToString("dd-MMM-yyyy");
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
