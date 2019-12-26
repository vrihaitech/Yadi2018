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
    public partial class DeleteQuatation : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBTQuotation dbTQuotation = new DBTQuotation();
        TQuotation tquotation = new TQuotation();
        public DeleteQuatation()
        {
            InitializeComponent();
        }
        private void DeleteQuatation_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ")  order by LedgerName");
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
        }
        public void BindData()
        {
            try
            {
                while (dgDisplay.Rows.Count > 0)
                    dgDisplay.Rows.RemoveAt(0);
                string sql = "SELECT 0 AS SrNo,(SELECT ItemName FROM dbo.MStockItems_V(NULL, mItemMaster.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, MUOM.UOMName,  TQuotationDetails.MRP, TQuotationDetails.Rate, '' AS Barcode, 0 AS BarcodeNo, mItemMaster.ItemNo, MUOM.UOMNo, TQuotationDetails.FkRateSettingNo, TQuotationDetails.PkSrNo,TQuotationDetails.IsClose " +
                                    " FROM MStockItems INNER JOIN TQuotationDetails ON mItemMaster.ItemNo = TQuotationDetails.ItemNo INNER JOIN MRateSetting ON TQuotationDetails.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN MUOM ON MRateSetting.UOMNo = MUOM.UOMNo " +
                                    " WHERE  (TQuotationDetails.FkQuotationNo =" + dgQuotation[colIndex.qno, dgQuotation.CurrentRow.Index].Value + ") ";
                DataTable dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgDisplay.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            dgDisplay.Rows[i].Cells[j].Value = dt.Rows[i][j];
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
                while (dgDisplay.Rows.Count > 0)
                    dgDisplay.Rows.RemoveAt(0);
                while (dgQuotation.Rows.Count > 0)
                    dgQuotation.Rows.RemoveAt(0);
                string sql = "select 0 AS SrNo,QuotationUserNo,QuotationDate,FromDate,ToDate,'NA'AS PurchaseNo," +
                    "'false' AS chkDelete,QuotationNo" +
                    " from TQuotation Where LedgerNo= " + ObjFunction.GetComboValue(cmbParty) +
                    " AND QuotationDate >='" + dtpFromDate.Value.Date + "' And QuotationDate <='" + dtpToDate.Value.Date + "'";
                DataTable dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgQuotation.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgQuotation.Rows[i].Cells[j].Value = dt.Rows[i][j];
                    }
                }
                if (dgQuotation.Rows.Count == 0)
                {
                    DisplayMessage("No Records Found");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void dgQuotation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            dgQuotation.Rows.Clear();
            dgDisplay.Rows.Clear();
            cmbParty.SelectedIndex = 0;
            dtpFromDate.Focus();
        }
        #region ColumnIndex
        public static class colIndex
        {
            public static int srno = 0;
            public static int quserno = 1;
            public static int date = 2;
            public static int fromdate = 3;
            public static int todate = 4;
            public static int purchaseno = 5;
            public static int delete = 6;
            public static int qno = 7;
        }
        #endregion
        private void dgQuotation_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = (e.RowIndex + 1);
                }
                if (e.ColumnIndex == colIndex.date)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                    if (Convert.ToDateTime(e.Value).Date < DateTime.Today)
                    {
                        dgQuotation.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
                if (e.ColumnIndex == colIndex.fromdate)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                }
                if (e.ColumnIndex == colIndex.todate)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void dgDisplay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
        private void btnDelete_Click(object sender, EventArgs e)
        {


            if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (OMMessageBox.Show("Are you want backup of delete Data ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbTQuotation = new DBTQuotation();
                    tquotation = new TQuotation();
                    string MainData = "";
                    for (int i = 0; i < dgQuotation.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgQuotation.Rows[i].Cells[colIndex.delete].Value) == true) 
                        if (MainData == "")
                        {
                            MainData = BackupData(Convert.ToInt64(dgQuotation.Rows[i].Cells[colIndex.qno].Value));
                        }
                        else
                        {
                            MainData += Environment.NewLine + BackupData(Convert.ToInt64(dgQuotation.Rows[i].Cells[colIndex.qno].Value));
                        }
                    }
                    string n = string.Format(@"D:\Meghana\Backup\Quotation-{0:dd-MMM-yyyy_hh-mm-ss-tt}.txt", DateTime.Now);
                    File.WriteAllText(n, MainData);
                }

                for (int i = 0; i < dgQuotation.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgQuotation.Rows[i].Cells[colIndex.delete].Value) == true)
                    {
                        tquotation.QuotationNo = Convert.ToInt64(dgQuotation.Rows[i].Cells[colIndex.qno].Value);
                        dbTQuotation.DeleteTQuotation(tquotation);
                    }
                }

                if (dbTQuotation.ExecuteNonQueryStatements1())
                {
                    OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindGrid();
                    dgQuotation.Focus();
                }
            }

            else
            {
                OMMessageBox.Show("Record not deleted..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }

        }

        private void dgQuotation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                BindData();
            }
            if (e.KeyCode == Keys.Down)
            {
                dgDisplay.Rows.Clear();
            }
            if (e.KeyCode == Keys.Up)
            {
                dgDisplay.Rows.Clear();
            }
            if (e.KeyCode == Keys.Escape)
            {
                btnDelete.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (dgQuotation.CurrentRow.Index < 0)
                    return;
                dbTQuotation = new DBTQuotation();
                tquotation = new TQuotation();

                if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {

                    if (OMMessageBox.Show("Are you want backup of delete Data ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        BackupData(Convert.ToInt64(dgQuotation.CurrentRow.Cells[colIndex.qno].Value));

                    }
                    for (int i = 0; i < dgQuotation.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgQuotation.Rows[i].Cells[colIndex.delete].Value) == true)
                            tquotation.QuotationNo = Convert.ToInt64(dgQuotation.Rows[i].Cells[colIndex.qno].Value);
                        dbTQuotation.DeleteTQuotation(tquotation);
                    }
                    if (dbTQuotation.ExecuteNonQueryStatements1())
                    {
                        OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();
                        dgQuotation.Focus();
                    }
                }
                else
                {
                    OMMessageBox.Show("Record not deleted..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
            if (e.KeyCode == Keys.F2)
            {
                chkDeleteAll.Checked = !chkDeleteAll.Checked;
                for (int i = 0; i < dgQuotation.Rows.Count; i++)
                {
                    if (dgQuotation.Rows[i].Cells[colIndex.delete].ReadOnly == false)
                        dgQuotation.Rows[i].Cells[colIndex.delete].Value = chkDeleteAll.Checked;
                }
            }
        }

        public string BackupData(long qno)
        {
            String Sql;
            Sql = "Select * from TQuotation where QuotationNo=" + qno + "";
            DataTable dt = ObjFunction.GetDataView(Sql).Table;
            String StrQuotation = GetString(dt, dt.Columns.Count, "1");
            Sql = ("Select * from TQuotationDetails where FKQuotationNo=" + qno + "");
            DataTable dtdetails = ObjFunction.GetDataView(Sql).Table;
            String StrDetails = GetString(dtdetails, dtdetails.Columns.Count, "2");
            return StrQuotation + Environment.NewLine + StrDetails;
        }

        public string GetString(DataTable dt, int ColCount, string TableNo)
        {
            string StrData = "", StrMain = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                StrData = "";
                for (int j = 0; j < ColCount; j++)
                {
                    if (StrData == "")
                        StrData = dt.Rows[i].ItemArray[j].ToString();
                    else
                        StrData += "," + dt.Rows[i].ItemArray[j].ToString();
                }
                if (StrMain == "")
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
            for (int i = 0; i < dgQuotation.Rows.Count; i++)
            {
                if (dgQuotation.Rows[i].Cells[colIndex.delete].ReadOnly == false)
                    dgQuotation.Rows[i].Cells[colIndex.delete].Value = chkDeleteAll.Checked;


            }
        }
        private void dgQuotation_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BindData();
        }
        private void dgQuotation_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgDisplay.Rows.Clear();
        }
        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpToDate.Focus();
            }
        }
        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbParty.Focus();
            }
        }
        private void cmbParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ObjFunction.GetComboValue(cmbParty) != 0)
                {
                    dgDisplay.Rows.Clear();
                    BindGrid();
                    dgQuotation.Focus();
                }
            }
        }

    }
}












