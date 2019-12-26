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
    public partial class AdvancedSearch : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        public long LedgerNo = 0;
        private long GroupNo = 0;
        public AdvancedSearch()
        {
            InitializeComponent();
        }

        public AdvancedSearch(long MGroupNo)
        {
            InitializeComponent();
            GroupNo = MGroupNo;
        }

        private void AdvancedSearch_Load(object sender, EventArgs e)
        {

        }

        public void BindGrid()
        {
            while (dataGridView1.Rows.Count > 0)
                dataGridView1.Rows.RemoveAt(0);

            DataView dv = new DataView();
            string str = TxtSearch.Text;
            if (str.Trim() != "")
            {
                str = str.Replace("+91", "");
                if (char.IsLetter(str, 0))
                {
                    dv = GetBySearch(0, TxtSearch.Text);
                    dataGridView1.DataSource = dv;
                }
                else if (char.IsNumber(str, 0))
                {
                    if (str[0].ToString() == "0")
                    {
                        dv = GetBySearch(1, TxtSearch.Text);
                        dataGridView1.DataSource = dv;
                    }
                    else
                    {
                        dv = GetBySearch(2, TxtSearch.Text);
                        dataGridView1.DataSource = dv;

                    }
                }


            }
        }

        public DataView GetBySearch(int Column, string Value)
        {

            string sql = null;
            //sql = " SELECT MLedger.LedgerNo,MLedger.LedgerName +' '+ MLedger.ContactPerson + ' - ' +IsNull(MLedgerDetails.MobileNo1,' ') + ' - ' +IsNull(MLedgerDetails.MobileNo2,' ') + ' - ' +IsNull(MLedgerDetails.PhNo1,' ') + '-' +IsNull(MLedgerDetails.PhNo2,' ') +' - '+IsNull(MLedgerDetails.Address,' ') AS Search " +
              //          " FROM MLedger  LEFT OUTER JOIN  MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo ";
            sql = " SELECT MLedger.LedgerNo,MLedger.LedgerName , MLedger.ContactPerson ,IsNull(MLedgerDetails.MobileNo1,' ') As MobNo1,IsNull(MLedgerDetails.MobileNo2,' ') AS MobNo2 ,IsNull(MLedgerDetails.PhNo1,' ') As PhNo1 ,IsNull(MLedgerDetails.PhNo2,' ') As PhNo2,IsNull(MLedgerDetails.Address,' ')  As Address" +
                        " FROM MLedger  LEFT OUTER JOIN  MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo ";
            switch (Column)
            {
                case 0:
                    sql += " Where   (MLedger.LedgerName+' '+ MLedger.ContactPerson like '%" + Value.Trim().Replace("'","''") + "' + '%' or  MLedgerDetails.Address like '%" + Value.Trim().Replace("'","''") + "' + '%') And MLedger.IsActive='true' ";

                    break;

                case 1:
                    sql += " Where    (MLedgerDetails.PhNo1 like '%" + Value.Trim().Replace("'","''") + "' + '%' or  MLedgerDetails.PhNo2 like '%" + Value.Trim().Replace("'","''") + "' + '%') And MLedger.IsActive='true' ";

                    break;
                case 2:
                    sql += " Where    (MLedgerDetails.MobileNo1 like '%" + Value.Trim().Replace("'","''") + "' + '%' or  MLedgerDetails.MobileNo2 like '%" + Value.Trim().Replace("'","''") + "' + '%') And MLedger.IsActive='true' ";
                    break;
            }
            if (GroupNo != 0)
                sql += "And  MLedger.GroupNo=" + GroupNo;
            sql += "Order By  MLedger.LedgerName";
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
            dataGridView1.Columns[1].Width = 220;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[5].Width = 80;
            dataGridView1.Columns[6].Width = 80;
            dataGridView1.Columns[7].Width = 300;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
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
                LedgerNo = Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                this.Close();
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void AdvancedSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
