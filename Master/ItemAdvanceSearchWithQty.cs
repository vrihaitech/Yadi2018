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
    public partial class ItemAdvanceSearchWithQty : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Color clrColorRow = Color.FromArgb(255, 224, 192);
        public string BarCode = "";

        public DataTable dtItemList = new DataTable();

        public static string serachText = "";

        public ItemAdvanceSearchWithQty(string qry)
        {

            InitializeComponent();
            dtItemList = ObjFunction.GetDataView(qry).Table;
            if (dtItemList.Rows.Count > 0)
            {
                dgItemList.Rows.Clear();
                dtItemList.Columns.Add("Qty");
                dgItemList.Columns[17].DisplayIndex = 2;
                for (int i = 0; i < dtItemList.Rows.Count; i++)
                {
                    dgItemList.Rows.Add();
                    for (int j = 0; j < dtItemList.Columns.Count; j++)
                    {
                        dgItemList.Rows[i].Cells[j].Value = dtItemList.Rows[i].ItemArray[j].ToString();
                    }
                }


                // dgItemList.DataSource = dtItemList.DefaultView;

                //dgItemList.CurrentCell = dgItemList[1, 0];
                dgItemList.Focus();
            }

        }
        private void ItemAdvanceSearchWithQty_Load(object sender, EventArgs e)
        {
            new GridSearch(dgItemList, 1, 2);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            dtItemList.Rows.Clear();
            DataRow dr = null;
            for (int i = 0; i < dgItemList.Rows.Count; i++)
            {
                dr = dtItemList.NewRow();
                for (int j = 0; j < dgItemList.ColumnCount; j++)
                {
                    dr[j] = dgItemList.Rows[i].Cells[j].Value;
                }
                dtItemList.Rows.Add(dr);
            }
            //dg = dgItemList;
            Close();
        }

    }
}
