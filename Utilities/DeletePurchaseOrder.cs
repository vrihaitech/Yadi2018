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
using System.IO;

namespace Yadi.Utilities
{
    public partial class DeletePurchaseOrder : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTOtherVoucherEntry dbtOtherVoucherEntry = new DBTOtherVoucherEntry();
        TOtherVoucherEntry tOtherVoucherEntry = new TOtherVoucherEntry();

        public DeletePurchaseOrder()
        {
            InitializeComponent();
        }

        private void DeletePurchaseOrder_Load(object sender, EventArgs e)
        {

            ObjFunction.FillCombo(cmbParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ")  order by LedgerName");
            dtpFromDate.Text  = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            dtpFromDate.Text  = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            dtpToDate.MinDate = dtpFromDate.Value;
            dtpFromDate.Focus();
        }

        public void BindData()
        {
            try
            {
                while (dgdisplay.Rows.Count > 0)
                    dgdisplay.Rows.RemoveAt(0);

                string sql = " SELECT 0 AS SrNo, (SELECT ItemName  FROM dbo.MStockItems_V(NULL, TOtherStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, MUOM.UOMName,TOtherStock.Quantity, MRateSetting.MRP, TOtherStock.Rate " +
                             " FROM TOtherStock INNER JOIN  MUOM ON TOtherStock.FkUomNo = MUOM.UOMNo INNER JOIN MRateSetting ON TOtherStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                             " WHERE     (TOtherStock.FKVoucherNo = " + dgOrder[colIndex.PkOtherVoucherNo, dgOrder.CurrentRow.Index].Value + ")";

                DataTable dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgdisplay.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dgdisplay.Rows[i].Cells[j].Value = dt.Rows[i][j];
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGrid()
        {
            try
            {
                while (dgdisplay.Rows.Count > 0)
                    dgdisplay.Rows.RemoveAt(0);
                while (dgOrder.Rows.Count > 0)
                    dgOrder.Rows.RemoveAt(0);
                string sql = " SELECT 0 AS SrNo, VoucherUserNo, VoucherDate, Reference,BilledAmount, 'false' AS chkDelete,PkOtherVoucherNo " +
                             " FROM TOtherVoucherEntry " +
                            " WHERE  LedgerNo= " + ObjFunction.GetComboValue(cmbParty) + " AND VoucherDate >='" + dtpFromDate.Text + "' And VoucherDate <='" + dtpToDate.Text + "'";

                DataTable dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgOrder.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgOrder.Rows[i].Cells[j].Value = dt.Rows[i][j];
                    }
                }
                if (dt.Rows.Count == 0)
                    DisplayMessage("No Records Found");
                else
                {
                    dgOrder.CurrentCell = dgOrder[colIndex.chkDelete, 0];
                    dgOrder.Focus();
                }


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            cmbParty.SelectedIndex = 0;
            while (dgOrder.RowCount > 0)
                dgOrder.Rows.RemoveAt(0);
            while (dgdisplay.RowCount > 0)
                dgdisplay.Rows.RemoveAt(0);
            dtpFromDate.Focus();
        }

        private void cmbParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                {
                    if (ObjFunction.GetComboValue(cmbParty) != 0)
                    {
                        //e.SuppressKeyPress = true;
                        dgdisplay.Rows.Clear();
                        BindGrid();

                    }

                }

        }
           
        

        #region ColumnIndex
        public static class colIndex
        {
            public static int srno = 0;
            public static int VoucherUserNo = 1;
            public static int VoucherDate = 2;
            public static int Reference = 3;
            public static int BilledAmount = 4;
            public static int chkDelete = 5;
            public static int PkOtherVoucherNo = 6;
        }
        #endregion

        private void dgquotation_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == 0)
                {
                    e.Value = (e.RowIndex + 1);
                }
                if (e.ColumnIndex == colIndex.chkDelete)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                }
                if (e.ColumnIndex == colIndex.VoucherDate)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void dgdisplay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = (e.RowIndex + 1);
                }
            }


            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }

        private void btndelete_Click(object sender, EventArgs e)
        {
                            dbtOtherVoucherEntry = new DBTOtherVoucherEntry();
                            tOtherVoucherEntry = new TOtherVoucherEntry();
                if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (OMMessageBox.Show("Are you sure want Backup of record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                      
                        BackupData(Convert.ToInt64(dgOrder.CurrentRow.Cells[colIndex.PkOtherVoucherNo].Value));
                    }
                    for (int i = 0; i < dgOrder.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgOrder.Rows[i].Cells[colIndex.chkDelete].EditedFormattedValue))
                        {
                           
                            tOtherVoucherEntry.PkOtherVoucherNo = Convert.ToInt64(dgOrder.Rows[i].Cells[colIndex.PkOtherVoucherNo].Value);
                            dbtOtherVoucherEntry.DeleteAllTOtherVoucherEntryByCollection(tOtherVoucherEntry);
                            if (dbtOtherVoucherEntry.ExecuteNonQueryStatements1())
                            {
                                OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                BindGrid();
                                dgOrder.Focus();
                            }
                        }
                    }
                }

                else
                {
                    OMMessageBox.Show("Record not deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }

            }
        

        private void dgquotation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BindData();
                e.SuppressKeyPress = true;
                dgOrder.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                dgdisplay.Rows.Clear();

            }
            if (e.KeyCode == Keys.Up)
            {
                dgdisplay.Rows.Clear();
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnDelete.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (dgOrder.CurrentRow.Index < 0)
                    return;
                  dbtOtherVoucherEntry = new DBTOtherVoucherEntry();
                  tOtherVoucherEntry = new TOtherVoucherEntry();
                if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (OMMessageBox.Show("Are you sure want Backup of record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        BackupData(Convert.ToInt64(dgOrder.CurrentRow.Cells[colIndex.PkOtherVoucherNo].Value));
                    }
                    for (int i = 0; i < dgOrder.Rows.Count;i++ )
                    if(Convert.ToBoolean(dgOrder.Rows[i].Cells[colIndex.PkOtherVoucherNo].Value) == true)
                        tOtherVoucherEntry.PkOtherVoucherNo = Convert.ToInt64(dgOrder.CurrentRow.Cells[colIndex.PkOtherVoucherNo].Value);
                        dbtOtherVoucherEntry.DeleteAllTOtherVoucherEntryByCollection(tOtherVoucherEntry);
                    if (dbtOtherVoucherEntry.ExecuteNonQueryStatements1())
                    {
                        OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();
                        e.SuppressKeyPress = true;
                        dgOrder.Focus();
                    }
                }
                else
                {
                    OMMessageBox.Show("Record not deleted ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
            if (e.KeyCode == Keys.F2)
            {
                chkDeleteAll.Checked = !chkDeleteAll.Checked;
                for (int i = 0; i < dgOrder.Rows.Count; i++)
                 {
                if (dgOrder.Rows[i].Cells[colIndex.chkDelete].ReadOnly == false)
                    dgOrder.Rows[i].Cells[colIndex.chkDelete].Value = chkDeleteAll.Checked;
                 }
            }
        }
        public void BackupData(long PkOtherVoucherNo)
        {
            String Sql, MainData="";
            Sql = "select * from TOtherVoucherEntry where PkOtherVoucherNo= "+ PkOtherVoucherNo+"";
            DataTable dt = ObjFunction.GetDataView(Sql).Table;
            string StrVoucher = GetString(dt, dt.Columns.Count, "1");
            Sql = "select * from TOtherStock where FkVoucherNo=" + PkOtherVoucherNo + "";
            DataTable dtdetails = ObjFunction.GetDataView(Sql).Table;
            string StrDetails = GetString(dtdetails, dtdetails.Columns.Count, "2");
            MainData = StrVoucher + Environment.NewLine + StrDetails;
            StreamWriter sw= new StreamWriter("Write.txt");
            sw.WriteLine(MainData);
            sw.Close();
        }
        public String GetString(DataTable dt,int ColCount,string TableNo)
        {
            String StrData = "", StrMain = "";
            for(int i=0 ; i <dt.Rows.Count; i++)
            {
                StrData="";
                for (int j = 0; j < ColCount; j++)
                {
                    if (StrData == "")
                        StrData = dt.Rows[i].ItemArray[j].ToString();
                    else
                        StrData += "," + dt.Rows[i].ItemArray[j].ToString();
                }
                if(StrData == "")
                    StrMain = TableNo + "," + StrData;
                    else
                    StrMain += Environment.NewLine + TableNo + "," + StrData;
                }
            return StrMain;
        }

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(1200);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkDeleteAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgOrder.Rows.Count; i++)
            {
                dgOrder.Rows[i].Cells[colIndex.chkDelete].Value = chkDeleteAll.Checked;
            }
        }

        private void dgquotation_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BindData();
        }

        private void dgquotation_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgdisplay.Rows.Clear();
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //e.SuppressKeyPress = true;
                dtpToDate.Focus();
            }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //e.SuppressKeyPress = true;
                cmbParty.Focus();
            }
        }

    }
}







