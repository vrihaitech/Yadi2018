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
    public partial class MultipleBarCodePrint : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMItemMaster dbMItemMaster = new DBMItemMaster();

        public MultipleBarCodePrint()
        {
            InitializeComponent();
        }

        private void RateVariationChange_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbBrand, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
            cmbBrand.Focus();
            KeyDownFormat(this.Controls);

        }

        #region KeyDown Events
        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnPrint_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
               btnClear_Click(sender, e);
            }
        }
        #endregion

        public void BindGrid()
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);
           

            //string Sql1 = " SELECT     0 AS SrNo, MItemGroup.StockGroupName + '-' + mItemMaster.ItemName AS ItemName, 0 AS NoOfPrint,MStockItems.ItemNo,'OK' As OKBtn " +
            //            " FROM MStockGroup INNER JOIN MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo " +
            //            " Where mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbBrand) + " " +
            //            " Order By MItemGroup.StockGroupName + '-' + mItemMaster.ItemName ";
            string Sql = " SELECT 0 AS SrNo, MItemGroup.StockGroupName + '-' + mItemMaster.ItemName AS ItemName, MUOM.UOMName,MRateSetting.MRP, MRateSetting.ASaleRate,0 AS NoOfPrint,MStockItems.ItemNo, MRateSetting.PkSrNo,'OK' As OKBtn " +
                      " FROM  MUOM INNER JOIN MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo INNER JOIN "+
                      " MStockGroup INNER JOIN MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo ON MRateSetting.ItemNo = mItemMaster.ItemNo " +
                      " WHERE     (MRateSetting.IsActive = 'True')  " +
                      " And    mItemMaster.GroupNo =" + ObjFunction.GetComboValue(cmbBrand) + " And mItemMaster.IsActive='True'  " +
                      " Order By MItemGroup.StockGroupName + '-' + mItemMaster.ItemName";

            DataTable dt = ObjFunction.GetDataView(Sql).Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgDetails.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dgDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }

        }

        private void cmbBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbBrand) != 0)
                {
                    BindGrid();
                    if (dgDetails.RowCount > 0)
                    {
                        dgDetails.CurrentCell = dgDetails[ColIndex.NoOfPrint, 0];
                        dgDetails.Focus();
                    }
                }
                else
                {
                    DisplayMessage("Please Select Brand");
                    cmbBrand.Focus();
                }
            }
        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int UOM = 2;
            public static int MRP = 3;
            public static int Rate = 4;
            public static int NoOfPrint = 5;
            public static int ItemNo = 6;
            public static int PKRateSettingNo = 7;
            public static int OKButton = 8;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);
            while (dgPrint.Rows.Count > 0)
                dgPrint.Rows.RemoveAt(0);
            cmbBrand.SelectedIndex = 0;
            cmbBrand.Focus();
        }

        private void dgDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
            else if (e.ColumnIndex == ColIndex.MRP || e.ColumnIndex == ColIndex.Rate)
            {
                e.Value = Convert.ToDouble(e.Value).ToString(Format.DoubleFloating);
            }
        }

        private void dgDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnPrint.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (dgDetails.CurrentCell.ColumnIndex == ColIndex.OKButton)
                {
                    if (dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells[ColIndex.NoOfPrint].Value != null)
                    {
                        if (dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells[ColIndex.NoOfPrint].Value.ToString() != "")
                        {
                            if (Convert.ToInt64(dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells[ColIndex.NoOfPrint].Value) > 0)
                            {
                                dgPrint.Rows.Add();
                                for (int i = 0; i < dgPrint.Columns.Count; i++)
                                {
                                    dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[i].Value = dgDetails.Rows[dgDetails.CurrentCell.RowIndex].Cells[i].Value;
                                }
                                //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.SrNo].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.SrNo].Value;
                                //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.ItemName].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.ItemName].Value;
                                //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.NoOfPrint].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.NoOfPrint].Value;
                                //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.ItemNo].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value;
                                dgDetails.Rows.RemoveAt(dgDetails.CurrentCell.RowIndex);
                            }
                        }
                    }
                }
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(1200);
            lblMsg.Visible = false;
        }

        private void dgDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgDetails.CurrentCell.ColumnIndex == ColIndex.NoOfPrint)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtNoOfPrint_TextChanged);
            }
        }

        private void txtNoOfPrint_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 0, 5, OMFunctions.MaskedType.NotNegative);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgPrint.Rows.Count <= 0)
            {
                DisplayMessage("Atleast one item required.");
                cmbBrand.Focus();
                return;
            }
            DBTBarCodePrint dbBarCodePrint = new DBTBarCodePrint();
            TBarCodePrint tBarCodePrint = new TBarCodePrint();
            tBarCodePrint.MacNo = DBGetVal.MacNo;
            tBarCodePrint.UserID = DBGetVal.UserID;
            dbBarCodePrint.DeleteTBarCodePrint(tBarCodePrint);

            for (int i = 0; i < dgPrint.Rows.Count; i++)
            {
                tBarCodePrint = new TBarCodePrint();
                tBarCodePrint.PkSrNo = 0;
                tBarCodePrint.ItemNo = Convert.ToInt64(dgPrint.Rows[i].Cells[ColIndex.ItemNo].Value);
                tBarCodePrint.FKRateSettingNo = Convert.ToInt64(dgPrint.Rows[i].Cells[ColIndex.PKRateSettingNo].Value);
                tBarCodePrint.Quantity = Convert.ToInt64(dgPrint.Rows[i].Cells[ColIndex.NoOfPrint].Value);


                tBarCodePrint.MacNo = DBGetVal.MacNo;
                tBarCodePrint.UserID = DBGetVal.UserID;
                dbBarCodePrint.AddTBarCodePrint(tBarCodePrint);
            }

            if (dbBarCodePrint.ExecuteNonQueryStatements())
            {
                string[] ReportSession;
                ReportSession = new string[7];
                ReportSession[0] = "1";
                ReportSession[1] = txtStartNo.Text;
                ReportSession[2] = DBGetVal.MacNo.ToString();
                ReportSession[3] = DBGetVal.UserID.ToString();
                ReportSession[4] = "";//for wwight
                ReportSession[5] = "01-Jan-1900";//For Packing Date
                ReportSession[6] = "01-Jan-1900";//For Expiry Date

                if (OMMessageBox.Show("Do you want Preview of barcode?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form NewF;
                    if (rbBigMod.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.BarCodePrintBig(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rbSmallMode.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.BarCodePrint(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrint.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm = null;
                    if (rbBigMod.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            childForm = new Reports.BarCodePrintBig();
                        else
                            childForm = ObjFunction.LoadReportObject("BarCodePrintBig.rpt", CommonFunctions.ReportPath);
                    }
                    else if (rbSmallMode.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            childForm = new Reports.BarCodePrint();
                        else

                            childForm = ObjFunction.LoadReportObject("BarCodePrint.rpt", CommonFunctions.ReportPath);
                    }

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
                BindGrid();
                while (dgPrint.Rows.Count > 0)
                    dgPrint.Rows.RemoveAt(0);
            }
            else
                OMMessageBox.Show("Barcode not Print...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        }

        private void dgDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.OKButton)
            {
                if (e.RowIndex < 0) return;
                if (dgDetails.Rows[e.RowIndex].Cells[ColIndex.NoOfPrint].Value != null)
                {
                    if (dgDetails.Rows[e.RowIndex].Cells[ColIndex.NoOfPrint].Value.ToString() != "")
                    {
                        if (Convert.ToInt64(dgDetails.Rows[e.RowIndex].Cells[ColIndex.NoOfPrint].Value) > 0)
                        {
                            dgPrint.Rows.Add();
                            for (int i = 0; i < dgPrint.Columns.Count; i++)
                            {
                                dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[i].Value = dgDetails.Rows[e.RowIndex].Cells[i].Value;
                            }
                            //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.SrNo].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.SrNo].Value;
                            //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.ItemName].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.ItemName].Value;
                            //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.NoOfPrint].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.NoOfPrint].Value;
                            //dgPrint.Rows[dgPrint.Rows.Count - 1].Cells[ColIndex.ItemNo].Value = dgDetails.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value;
                            dgDetails.Rows.RemoveAt(dgDetails.CurrentCell.RowIndex);
                        }
                    }
                }
            }
        }

        private void dgPrint_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void dgPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (OMMessageBox.Show("Are you sure want delete this item..?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dgPrint.Rows.RemoveAt(dgPrint.CurrentCell.RowIndex);
                }
            }
        }
      
    }
}
