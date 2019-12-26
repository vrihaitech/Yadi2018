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

namespace Yadi.Utilities
{
    public partial class SeasonalBarcodePrint : Form
    {
        OMCommonClass cc = new OMCommonClass();
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBTSeasonalItems dbSeasonalItems = new DBTSeasonalItems();

        DataTable dt = new DataTable();
        DataTable dtHeader = new DataTable();

        public SeasonalBarcodePrint()
        {
            InitializeComponent();
        }

        private void SeasonalBarcodePrint_Load(object sender, EventArgs e)
        {
            KeyDownFormat(this.Controls);
            txtStartNo.Text = "0";
        }

        private void rdPurchaseItemwise_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            string sql = "SELECT  PKSrNo, BillNo, ItemName, MRP, Qty,Qty AS ActQty, Barcode, IsPrint " +
                       " FROM   TSeasonalItems  WHERE   (IsPrint = 'false')";
            dt = ObjFunction.GetDataView(sql).Table;
            dgvSeasonalBarcode.DataSource = dt.DefaultView;
            if (dgvSeasonalBarcode.Rows.Count > 0)
            {
                dgvSeasonalBarcode.Focus();
                dgvSeasonalBarcode.CurrentCell = dgvSeasonalBarcode[5, 0];
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvSeasonalBarcode.Rows.Count; i++)
            {
                dgvSeasonalBarcode.Rows[i].Cells[7].Value = chkSelectAll.Checked;
            }
            CalculateQty();
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;

                for (int i = 0; i < dgvSeasonalBarcode.Rows.Count; i++)
                {
                    dgvSeasonalBarcode.Rows[i].Cells[7].Value = chkSelectAll.Checked;
                }
                btnBarcodePrint.Focus();
                CalculateQty();
            }
            else if (e.KeyCode == Keys.F3)
            {
                rdItemwise.Checked = true;
            }
            else if (e.KeyCode == Keys.F4)
            {
                rdPurchaseItemwise.Checked = true;
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnBarcodePrint_Click(btnBarcodePrint, new EventArgs());
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnImportItems_Click(btnImportItems, new EventArgs());
            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else
                    KeyDownFormat(ctrl.Controls);
            }
        }
        #endregion

        private void btnBarcodePrint_Click(object sender, EventArgs e)
        {
            bool flag = false;

            for (int i = 0; i < dgvSeasonalBarcode.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvSeasonalBarcode.Rows[i].Cells[7].Value) == true)
                {
                    flag = true;
                }
            }
            if (flag == false)
            {
                OMMessageBox.Show("Select Atleast One Barcode To Print.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < dgvSeasonalBarcode.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvSeasonalBarcode.Rows[i].Cells[7].Value) == true)
                {
                    dbSeasonalItems.UpdateSeasonalItems(Convert.ToInt64(dgvSeasonalBarcode.Rows[i].Cells[0].Value));
                }
            }
            if (dbSeasonalItems.ExecuteNonQueryStatements() == true)
            {




                BarCodePrinting();
                //MessageBox.Show("Barcode Printed Successfully", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                BindGrid();
            }
            else
            {
                OMMessageBox.Show("Barcode Not Printed Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
        }

        private void rdItemwise_CheckedChanged(object sender, EventArgs e)
        {
            dgvSeasonalBarcode.DataSource = null;
        }

        public void BarCodePrinting()
        {

            DBTSeasonalBarCodePrint dbBarCodePrint = new DBTSeasonalBarCodePrint();
            TSeasonalBarCodePrint tBarCodePrint = new TSeasonalBarCodePrint();
            tBarCodePrint.UserID = DBGetVal.UserID;
            dbBarCodePrint.DeleteTSeasonalBarCodePrint(tBarCodePrint);

            for (int i = 0; i < dgvSeasonalBarcode.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvSeasonalBarcode.Rows[i].Cells[7].Value) == true)
                {

                    tBarCodePrint = new TSeasonalBarCodePrint();
                    tBarCodePrint.PkSrNo = 0;
                    tBarCodePrint.BillNo = Convert.ToInt64(dgvSeasonalBarcode.Rows[i].Cells[1].Value.ToString());
                    tBarCodePrint.ItemName = dgvSeasonalBarcode.Rows[i].Cells[2].Value.ToString();
                    tBarCodePrint.MRP = Convert.ToDouble(dgvSeasonalBarcode.Rows[i].Cells[3].Value.ToString());
                    tBarCodePrint.Qty = Convert.ToDouble(dgvSeasonalBarcode.Rows[i].Cells[5].Value.ToString());
                    tBarCodePrint.Barcode = dgvSeasonalBarcode.Rows[i].Cells[6].Value.ToString();
                    tBarCodePrint.UserID = DBGetVal.UserID;
                    dbBarCodePrint.AddTSeasonalBarCodePrint(tBarCodePrint);
                }
            }

            dbBarCodePrint.ExecuteNonQueryStatements();

            string[] ReportSession;
            ReportSession = new string[2];
            ReportSession[0] = txtStartNo.Text;
            ReportSession[1] = DBGetVal.UserID.ToString();

            if (OMMessageBox.Show("Do you want Preview of barcode?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form NewF;
                //if (rbBigMod.Checked == true)
                //{
                //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //        NewF = new Display.ReportViewSource(new Reports.BarCodePrintBig(), ReportSession);
                //    else
                //        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintBig.rpt", CommonFunctions.ReportPath), ReportSession);
                //    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                //}
                //else if (rbSmallMode.Checked == true)
                //{
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.SeasonalBarCodePrint(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("SeasonalBarCodePrint.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                //}
            }
            else
            {
                CrystalDecisions.CrystalReports.Engine.ReportDocument childForm = null;
                //if (rbBigMod.Checked == true)
                //{
                //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //        childForm = new Reports.BarCodePrintBig();
                //    else
                //        childForm = ObjFunction.LoadReportObject("BarCodePrintBig.rpt", CommonFunctions.ReportPath);
                //}
                //else if (rbSmallMode.Checked == true)
                //{
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    childForm = new Reports.SeasonalBarCodePrint();
                else

                    childForm = ObjFunction.LoadReportObject("SeasonalBarCodePrint.rpt", CommonFunctions.ReportPath);
                //}

                if (childForm != null)
                {
                    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    if (objRpt.PrintReport() == true)
                    {
                        OMMessageBox.Show("Printing barCode sucessfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                    else
                    {
                        OMMessageBox.Show("Barcode not Print...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                else
                {
                    OMMessageBox.Show("Barcode Print report not exist...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImportItems_Click(object sender, EventArgs e)
        {
            ImportData();
        }

        #region Import Related Methods
        public bool GetSeasonalItems(string val)
        {
            string[] str = new string[1];
            str[0] = "\n";
            string[] strLine = val.Split(str, StringSplitOptions.None);
            string[] strData;

            dtHeader = new DataTable();
            string[] strW = new string[1];
            strW[0] = ",";


            for (int i = 0; i < 5; i++)
            {
                dtHeader.Columns.Add();
            }

            for (int row = 0; row < strLine.Length; row++)
            {
                strData = strLine[row].Split(strW, StringSplitOptions.None);
                DataRow dr = dtHeader.NewRow();
                for (int i = 0; i < strData.Length; i++)
                {
                    dr[i] = strData[i].ToString();
                }
                dtHeader.Rows.Add(dr);
            }

            if (SaveSeasonalItems() == 1)
                return true;
            else
                return false;
        }

        public long SaveSeasonalItems()
        {
            DBTSeasonalItems dbSeasonalItem = new DBTSeasonalItems();
            TSeasonalItems tSeasonalItem = new TSeasonalItems();
            for (int i = 0; i < dtHeader.Rows.Count; i++)
            {
                tSeasonalItem.PKSrNo = 0;
                tSeasonalItem.BillNo = Convert.ToInt64(dtHeader.Rows[i].ItemArray[HeaderColIndex.BillNo].ToString());
                tSeasonalItem.ItemName = dtHeader.Rows[i].ItemArray[HeaderColIndex.ItemName].ToString();
                tSeasonalItem.MRP = Convert.ToDouble(dtHeader.Rows[i].ItemArray[HeaderColIndex.MRP].ToString());
                tSeasonalItem.Qty = Convert.ToDouble(dtHeader.Rows[i].ItemArray[HeaderColIndex.Qty].ToString());
                tSeasonalItem.Barcode = dtHeader.Rows[i].ItemArray[HeaderColIndex.BarCode].ToString();
                tSeasonalItem.IsPrint = false;
                tSeasonalItem.UserID = 1;
                tSeasonalItem.UserDate = DateTime.Now;
                dbSeasonalItem.AddTSeasonalItems(tSeasonalItem);
            }

            if (dbSeasonalItem.ExecuteNonQueryStatements() == true)
            {
                return 1;
            }
            else
                return 0;
        }

        private static class HeaderColIndex
        {
            public static int BillNo = 0;
            public static int ItemName = 1;
            public static int MRP = 2;
            public static int Qty = 3;
            public static int BarCode = 4;
        }

        public void ImportData()
        {
            if (OF.ShowDialog() == DialogResult.OK)
            {
                string FileName = OF.FileName;
                System.IO.StreamReader objreader = new System.IO.StreamReader(FileName);

                string strLine = objreader.ReadToEnd();
                if (GetSeasonalItems(strLine) == true)
                    OMMessageBox.Show("Data Import Successfully..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                else
                    OMMessageBox.Show("Data not Importing..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtStartNo);
        }

        public void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvSeasonalBarcode.CurrentCell.ColumnIndex == 5)
                {

                    ObjFunction.SetMasked((TextBox)sender, 2, 6, OMFunctions.MaskedType.NotNegative);
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgvSeasonalBarcode_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvSeasonalBarcode.CurrentCell.ColumnIndex == 5)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtAmount_TextChanged);
            }
        }

        public void CalculateQty()
        {
            double TotalQty = 0;
            for (int i = 0; i < dgvSeasonalBarcode.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgvSeasonalBarcode.Rows[i].Cells[7].EditedFormattedValue) == true)
                {
                    if (dgvSeasonalBarcode.Rows[i].Cells[5].EditedFormattedValue.ToString() != "")
                        TotalQty = TotalQty + Convert.ToDouble(dgvSeasonalBarcode.Rows[i].Cells[5].EditedFormattedValue);
                }
            }
            lblMsg.Text = "Printed Qty : " + TotalQty.ToString();
        }

        private void dgvSeasonalBarcode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                CalculateQty();
            }
        }

        private void dgvSeasonalBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (dgvSeasonalBarcode.CurrentCell.ColumnIndex == 7)
                {
                    CalculateQty();
                }
            }
        }

        private void dgvSeasonalBarcode_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                CalculateQty();
            }
        }

        private void dgvSeasonalBarcode_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                CalculateQty();
            }
        }

    }
}
