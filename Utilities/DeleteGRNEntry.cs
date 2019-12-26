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
    public partial class DeleteGRNEntry : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBTGRN dbTGRN = new DBTGRN();
        TGRN tgrn = new TGRN();
        public DeleteGRNEntry()
        {
            InitializeComponent();
        }
        private void DeleteGRNEntry_Load(object sender, EventArgs e)
        {
            ObjFunction.FillComb(cmbParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and IsActive='true' order by LedgerName");
            dtpFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            dtpFromDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            dtpToDate.MinDate = dtpFromDate.Value;
        }
        public void BindData()
        {
            try
            {
                while (dgDisplay.Rows.Count > 0)
                    dgDisplay.Rows.RemoveAt(0);
                string sql = " SELECT   0 AS Sr,(SELECT ItemName FROM dbo.MStockItems_V(NULL, mItemMaster.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TGRNDetails.Quantity, MUOM.UOMName, TGRNDetails.Rate,TGRNDetails.Amount " +
                            " FROM  MStockItems INNER JOIN TGRNDetails ON mItemMaster.ItemNo = TGRNDetails.ItemNo INNER JOIN TGRN ON TGRNDetails.FkGRNNo = TGRN.GRNNo INNER JOIN MRateSetting ON TGRNDetails.FkRateSettingNo = MRateSetting.PkSrNo INNER JOIN " +
                            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN MUOM ON MRateSetting.UOMNo = MUOM.UOMNo WHERE  (TGRNDetails.FkGRNNo =" + dgQuotation[colIndex.grnno, dgQuotation.CurrentRow.Index].Value + ") ";
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
                string sql = "SELECT 0 AS SrNo, TGRN.GRNUserNo, TGRN.GRNDate, TGRN.RefNo, TGRNDetails.GodownNo, 'false' AS chkDelete, TGRN.GRNNo " +
                              "FROM TGRN INNER JOIN TGRNDetails ON TGRN.GRNNo = TGRNDetails.FkGRNNo " +
                              "Where LedgerNo= " + ObjFunction.GetComboValue(cmbParty) +
                              "And GRNDate >='" + dtpFromDate.Value.Date + "' And GRNDate <='" + dtpToDate.Value.Date + "'" +
                              "GROUP BY TGRN.GRNUserNo, TGRN.GRNDate, TGRN.RefNo, TGRNDetails.GodownNo, TGRN.GRNNo " +
                              "ORDER BY TGRN.GRNUserNo";
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
            cmbParty.SelectedIndex = -1;
            dtpFromDate.Focus();
        }
        #region ColumnIndex
        public static class colIndex
        {
            public static int srno = 0;
            public static int GrnUserNo = 1;
            public static int date = 2;
            public static int refNo = 3;
            public static int location = 4;
            public static int delete = 5;
            public static int grnno = 6;
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

            dbTGRN = new DBTGRN();
            tgrn = new TGRN();
            if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (OMMessageBox.Show("Are you want backup of delete Data ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {

                    BackupData(Convert.ToInt64(dgQuotation.CurrentRow.Cells[colIndex.grnno].Value));
                }

                for (int i = 0; i < dgQuotation.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgQuotation.Rows[i].Cells[colIndex.delete].Value) == true)
                        tgrn.GRNNo = Convert.ToInt64(dgQuotation.Rows[i].Cells[colIndex.grnno].Value);
                    dbTGRN.DeleteTGRN(tgrn);
                }
                if (dbTGRN.ExecuteNonQueryStatements1())
                {
                    OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindGrid();
                    dgQuotation.Focus();
                }
            }


            else
            {
                OMMessageBox.Show("Record not deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
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
                dbTGRN = new DBTGRN();
                tgrn = new TGRN();
                if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (OMMessageBox.Show("Are you want backup of delete Data ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        BackupData(Convert.ToInt64(dgQuotation.CurrentRow.Cells[colIndex.grnno].Value));
                    }

                    for (int i = 0; i < dgQuotation.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgQuotation.Rows[i].Cells[colIndex.delete].Value) == true)

                            tgrn.GRNNo = Convert.ToInt64(dgQuotation.Rows[i].Cells[colIndex.grnno].Value);
                        dbTGRN.DeleteTGRN(tgrn);
                    }
                    if (dbTGRN.ExecuteNonQueryStatements1())
                    {
                        OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();
                        dgQuotation.Focus();
                    }
                }


                else
                {
                    OMMessageBox.Show("Record not deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
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
        public void BackupData(long grnno)
        {
            String Sql, MainData = "";
            Sql = "select * from TGRN Where GRNNo=" + grnno + "";
            DataTable dt = ObjFunction.GetDataView(Sql).Table;
            string StrGRN = GetString(dt, dt.Columns.Count, "1");
            Sql = "select * from TGRNDetails Where FkGRNNo=" + grnno + "";
            DataTable dtdetails = ObjFunction.GetDataView(Sql).Table;
            string StrDetails = GetString(dtdetails, dtdetails.Columns.Count, "2");
            MainData = StrGRN + Environment.NewLine + StrDetails;
            StreamWriter sw = new StreamWriter("Write.txt");
            sw.WriteLine(MainData);
            sw.Close();
        }
        public string GetString(DataTable dt, int ColCount, string TableNo)
        {
            String StrData = "", StrMain = "";
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
                if (StrData == "")
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
                //e.SuppressKeyPress = true;
                dtpToDate.Focus();
            }
        }
        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbParty.Focus();
            }
        }
        private void cmbParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ObjFunction.GetComboValue(cmbParty) != 0)
                {
                    e.SuppressKeyPress = true;
                    dgDisplay.Rows.Clear();
                    BindGrid();
                    dgQuotation.Focus();
                }
            }
        }
    }
}







