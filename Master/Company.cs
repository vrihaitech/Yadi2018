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
    public partial class Company : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        public static long RequestCompNo;
        
        public Company()
        {
            InitializeComponent();
        }

        private void Company_Load(object sender, EventArgs e)
        {
            
            CmbSearch.Items.Add(new Item("-----Select-----", "0"));
            CmbSearch.Items.Add(new Item("Company Name", "CompanyName"));
            //CmbSearch.Items.Add(new Item("Company No", "CompanyNo"));
            CmbSearch.SelectedIndex = 1;

            BindGrid("Select CompanyNo, CompanyName from MCompany Order by CompanyNo Desc");  
        }

        private void BindGrid(string strQ)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = ObjDset.FillDset("New", strQ, CommonFunctions.ConStr);

                bindingSource1.DataSource = ds.Tables[0];
                DataGridView1.DataSource = bindingSource1;
                DataGridView1.Columns[0].Visible = false;
                DataGridView1.Columns[1].Width = DataGridView1.Width;
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
                BindGrid("Select  CompanyNo, CompanyName from MCompany where " + itm.Value + " like '" + TxtSearch.Text + "%' Order by CompanyNo");
            }
            else
            {
                OMMessageBox.Show("Enter " + CmbSearch.Text + " to search");
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            BindGrid("Select CompanyNo, CompanyName from MCompany Order by CompanyNo Desc"); 
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestCompNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new CompanyAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);  
        }
    }
}
