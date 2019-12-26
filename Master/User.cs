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
    public partial class User : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        public static long RequestUserNo;
        //bool openflag = false;
        
        public User()
        {
            InitializeComponent();
            
        }

        private void User_Load(object sender, EventArgs e)
        {
            
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("User Name", "UserName"));
            CmbSearch.Items.Add(new Item("User Code", "UserCode"));
            
            CmbSearch.SelectedIndex = 1;

            if (DBGetVal.UserID == 1)
                BindGrid("Select UserCode, UserName,Case When(UserType=1) Then 'Admin' Else case when(UserType=2)Then 'Agent' else 'User'End End as 'User Type' from MUser Order by UserCode Desc");
            else
            {
                BindGrid("Select UserCode, UserName,Case When(UserType=1) Then 'Admin' Else case when(UserType=2)Then 'Agent' else 'User'End End as 'User Type' from MUser where UserCode=" + DBGetVal.UserID + " Order by UserCode Desc");
                BtnSearch.Visible = false; Button1.Visible = false;button3.Visible = false;
            }
        }

        private void BindGrid(string strQ)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = ObjDset.FillDset("New", strQ, CommonFunctions.ConStr);

                bindingSource1.DataSource = ds.Tables[0];
                DataGridView1.DataSource = bindingSource1;
                DataGridView1.Columns[0].Visible = true;
                DataGridView1.Columns[1].Width = 300;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private class Item
        {
            public string Name;
            public string Value;
            public Item(string name, string value)
            {
                Name = name; Value = value;
            }
            public override string ToString()
            {
                return Name;
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (TxtSearch.Text != "" && CmbSearch.SelectedIndex != 0)
            {
                Item itm = (Item)CmbSearch.SelectedItem;
                if (DBGetVal.UserID == 1)
                    BindGrid("Select UserCode, UserName,Case When(UserType=1) Then 'Admin' Else case when(UserType=2)Then 'Agent' else 'User'End End as 'User Type' from MUser where " + itm.Value + " like '" + TxtSearch.Text + "%' Order by UserCode");
                else
                    BindGrid("Select UserCode, UserName,Case When(UserType=1) Then 'Admin' Else case when(UserType=2)Then 'Agent' else 'User'End End as 'User Type' from MUser where UserCode=" + DBGetVal.UserID + " AND " + itm.Value + " like '" + TxtSearch.Text + "%' Order by UserCode Desc");
            }
           
            else
            {
                //  OMMessageBox.Show("Enter " + CmbSearch.Text + " to search");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (DBGetVal.UserID == 1)
                BindGrid("Select UserCode, UserName,Case When(UserType=1) Then 'Admin' Else case when(UserType=2)Then 'Agent' else 'User'End End as 'User Type' from MUser Order by UserCode Desc");
            else
                BindGrid("Select UserCode, UserName,Case When(UserType=1) Then 'Admin' Else case when(UserType=2)Then 'Agent' else 'User'End End as 'User Type' from MUser where UserCode=" + DBGetVal.UserID + " Order by UserCode Desc");
            CmbSearch.SelectedIndex = 0;
            TxtSearch.Text = "";
            if (DataGridView1.Rows.Count > 0)
            {
                DataGridView1.Focus();
                DataGridView1.CurrentCell = DataGridView1[1, 0];
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            RequestUserNo = 0;
            Form NewF = new UserAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void CmbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TxtSearch.Text = "";
                TxtSearch.Focus();
            }
            //ObjFunction.AutoComplete(ref CmbSearch, e, true);
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            
                BtnSearch_Click(sender, e);
            
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestUserNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new UserAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            User.RequestUserNo = 0;
            this.Close();
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
            {
                DataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
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
            //else if (e.KeyCode == Keys.Enter)
            //{
            //    DataGridView1.Focus();
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RequestUserNo = 0;
            Form NewF = new UserAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RequestUserNo = 0;
            Form NewF = new UserAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

       
    }
}
