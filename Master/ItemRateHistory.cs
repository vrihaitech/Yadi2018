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
    public partial class ItemRateHistory : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Color clrColorRow = Color.FromArgb(255, 224, 192);
        public string BarCode = "";

        public DataTable dtItemList = new DataTable();

        public static string serachText = "";
        public string strQuery = "";

        public ItemRateHistory(string itemName, long itemNo, long mfgCompNo, long ledgerNo)
        {

            InitializeComponent();
            strQuery = " SELECT  Top 10 TVoucherEntryCompany.VoucherUserNo, TVoucherEntry.VoucherDate, TStock.BilledQuantity, TStock.DiscPercentage, TStock.Rate " +
                       " FROM TVoucherEntry " +
                       " INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                       " INNER JOIN TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo " +
                       " INNER JOIN TVoucherEntryCompany ON TVoucherEntry.PkVoucherNo = TVoucherEntryCompany.FkVoucherNo " +
                       " WHERE     (TVoucherDetails.VoucherSrNo = 1) AND (TStock.ItemNo = " + itemNo + ") AND (TStock.MfgCompNo = " + mfgCompNo + ") AND (TVoucherDetails.LedgerNo = " + ledgerNo + ") " + 
                       " ORDER BY TVoucherEntry.PkVoucherNo DESC";
            dtItemList = ObjFunction.GetDataView(strQuery).Table;
            if (dtItemList.Rows.Count > 0)
            {
                dgItemList.Rows.Clear();

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
                //dgItemList.Focus();
            }
            btnOk.Focus();

        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            
            Close();
        }

        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

    }
}
