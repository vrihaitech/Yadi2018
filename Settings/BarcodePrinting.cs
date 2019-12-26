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


namespace Yadi.Settings
{
    public partial class BarcodePrinting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMBarcodeSettings dbBarcodeSetting = new DBMBarcodeSettings();
        MBarcodeSettings mBarcodeSettings = new MBarcodeSettings();
        long ID = 0, RateSettingNo = 0;
        //DateTime PkgDate;
        string MRP = "", Rate = "";//, ExpDate = "";
        string Brand = "", ShortDesc = "", Barcode = "";
        double TPurRate = 0.00;

        public BarcodePrinting()
        {
            InitializeComponent();
        }


        public BarcodePrinting(long id, long ratesettingno, string mrp, string rate, string brand, string shortDesc, string barcode, double tPurRate)
        {
            try
            {
                InitializeComponent();
                ID = id;
                RateSettingNo = ratesettingno;
                MRP = mrp;
                Rate = rate;
                Brand = brand;
                ShortDesc = shortDesc;
                Barcode = barcode;
                TPurRate = Math.Round(tPurRate, 0);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BarcodePrinting_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbBarcodeTemplate, "Select PkSrNo,TemplateName+'-'+Size as TemplateName  from MBarcodeTemplate order By PkSrNo");
                //cmbBarcodeTemplate.SelectedValue = ObjQry.ReturnLong("Select isNull(PkSrNo,0) from MBarcodeTemplate where ", CommonFunctions.ConStr).ToString();
                long TempNo = ObjQry.ReturnLong("Select ISNull(LastPrint,0) From MSetting", CommonFunctions.ConStr);
                cmbBarcodeTemplate.SelectedValue = TempNo.ToString();
                BindGrid();
                GridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbBarcodeTemplate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (ObjFunction.GetComboValue(cmbBarcodeTemplate) > 0)
                    {
                        BindGrid();
                        //if (GridView.Rows.Count > 0)
                        //{
                        //    //GridView.CurrentCell = GridView[5, 0];
                        //    //GridView.Focus();
                        //}

                        txtNoOfPrint.Focus();

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
                if (ObjFunction.GetComboValue(cmbBarcodeTemplate) > 0)
                {
                    string str = "";
                    DataTable dt = new DataTable();
                    str = " SELECT distinct 0 as SrNo , isNull(MBarcodeSettings.PkSrNo,0), ISNull(MOtherSettings.PkSrNo,0) AS SettingNo, MOtherSettings.SettingsKeyCode, MBarcodeSettings.SettingValue, MBarcodeSettings.IsActive " +
                                      " FROM  MOtherSettings LEFT OUTER JOIN MBarcodeSettings ON MOtherSettings.PkSrNo  = MBarcodeSettings.SettingNo " +
                                      " where MBarcodeSettings.BarcodeTemplateNo=" + ObjFunction.GetComboValue(cmbBarcodeTemplate) + "";

                    dt = ObjFunction.GetDataView(str, CommonFunctions.ConStr).Table;

                    GridView.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridView.Rows.Add();
                        for (int j = 0; j < GridView.Columns.Count; j++)
                        {
                            GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                        if (Convert.ToBoolean(GridView.Rows[i].Cells[5].FormattedValue) == false)
                            //GridView.Rows[i].ReadOnly = true;
                            GridView.Rows[i].Visible = false;
                    }

                    GridView.Rows[0].Cells[4].Value = DBGetVal.FirmName;
                    GridView.Rows[1].Cells[4].Value = MRP;
                    GridView.Rows[2].Cells[4].Value = Rate;
                    GridView.Rows[4].Cells[4].Value = Brand;
                    GridView.Rows[5].Cells[4].Value = ShortDesc;
                    if (GridView.Rows[6].Visible == true) GridView.Rows[6].Cells[4].Value = DBGetVal.ServerTime.Date.ToString(GridView.Rows[6].Cells[4].Value.ToString());
                    GridView.Rows[7].Cells[4].Value = "";

                    if (dt.Rows.Count > 0)
                    {
                        ApplyGridChanges();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void ApplyGridChanges()
        {
            try
            {
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    if (GridView.Rows[i].Cells[3].Value.ToString() == "Shop Name" || GridView.Rows[i].Cells[3].Value.ToString() == "Brand" || GridView.Rows[i].Cells[3].Value.ToString() == "Short Description" || GridView.Rows[i].Cells[3].Value.ToString() == "Best Before")
                    {
                        GridView.Rows[i].Cells[4].ReadOnly = true;
                        //GridView.Rows[i].Cells[4].Value = "NA";
                    }
                    if (GridView.Rows[i].Cells[3].Value.ToString() == "MRP" || GridView.Rows[i].Cells[3].Value.ToString() == "Rate")
                    {
                        GridView.Rows[i].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }


                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public static class Colindex
        {
            public static int SrNo = 0;
            public static int PkSrNo = 1;
            public static int SettingNo = 2;
            public static int Option = 3;
            public static int Input = 4;
            public static int IsActive = 5;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == Colindex.SrNo)
                {
                    e.Value = (e.RowIndex + 1).ToString();
                }
                if (GridView.Rows[e.RowIndex].Cells[3].Value.ToString() == "Packed Date" && GridView.CurrentCell.ColumnIndex == 4)
                {
                    GridView.Rows[e.RowIndex].ReadOnly = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int x = GridView.GetCellDisplayRectangle(GridView.CurrentCell.ColumnIndex, GridView.CurrentRow.Index, false).Left + GridView.Left;
                    int y = GridView.GetCellDisplayRectangle(GridView.CurrentCell.ColumnIndex, GridView.CurrentRow.Index, false).Bottom + GridView.Top;

                    if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "Packed Date" && GridView.CurrentCell.ColumnIndex == 4)
                    {
                        e.SuppressKeyPress = true;
                        dtpDate.Location = new Point(x, y);
                        dtpDate.Visible = true;
                        dtpDate.Focus();
                    }
                    if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "Best Before" && GridView.CurrentCell.ColumnIndex == 4)
                    {
                        e.SuppressKeyPress = true;
                        pnlExpDays.Visible = true;
                        txtExpDays.Text = "";
                        txtExpDays.Focus();
                    }
                    if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "Month/YR" && GridView.CurrentCell.ColumnIndex == 4)
                    {
                        e.SuppressKeyPress = true;
                        pnlDateType.Location = new Point(x, y);
                        pnlDateType.Visible = true;
                        lstDateTpe.Focus();

                    }
                    if (GridView.CurrentCell.ColumnIndex == 5)
                    {
                        e.SuppressKeyPress = true;
                        if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                            GridView.CurrentCell = GridView[5, GridView.CurrentCell.RowIndex + 1];
                        else
                            GridView.CurrentCell = GridView[5, GridView.CurrentCell.RowIndex];
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    btnOk.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    string format = ObjQry.ReturnString("Select SettingValue from MBarcodeSettings where PkSrNo =" + Convert.ToInt64(GridView.Rows[GridView.CurrentCell.RowIndex].Cells[Colindex.PkSrNo].Value) + " ", CommonFunctions.ConStr);
                    if (format == "" || format == null)
                        format = "dd-mm-yyyy";
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[4].Value = Convert.ToDateTime(dtpDate.SelectionStart).ToString(format);
                    dtpDate.Visible = false;
                    GridView.Focus();
                    if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                        GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex + 1];
                    else
                        GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex];

                    DateChange();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstDateTpe_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[4].Value = lstDateTpe.Text;
                    pnlDateType.Visible = false;
                    if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                        GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex + 1];
                    else
                        GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex];
                    GridView.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtNoOfPrint.Text = "";
            BindGrid();
            cmbBarcodeTemplate.Focus();
        }

        private void cmbBarcodeTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ObjFunction.GetComboValue(cmbBarcodeTemplate) > 0)
            //{
            //    BindGrid();
            //    //if(GridView.Rows.Count>0)
            //    txtNoOfPrint.Focus();
            //}
        }

        private void txtNoOfPrint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true;
                int rowNo = RowNo();
                GridView.CurrentCell = GridView[5, rowNo];
                GridView.Focus();
            }
        }

        public int RowNo()
        {
            int rowno = 0;
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                if (GridView.Rows[i].Visible == true)
                {
                    rowno = i;
                    break;
                }
            }
            return rowno;
        }

        private void txtStartNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (GridView.Rows.Count > 0)
                    {
                        GridView.CurrentCell = GridView[5, 0];
                        GridView.Focus();
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtNoOfPrint_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtNoOfPrint);
        }

        private void txtStartNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtStartNo);
        }

        private void cmbBarcodeTemplate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbBarcodeTemplate) > 0)
                {
                    BindGrid();
                    txtNoOfPrint.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "Rate" || GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "MRP")
                {
                    TextBox txt = (TextBox)e.Control;
                    txt.TextChanged += new EventHandler(txt_TextChanged);
                }
                if (GridView.CurrentCell.RowIndex == 8 || GridView.CurrentCell.RowIndex == 9 || GridView.CurrentCell.RowIndex == 10 || GridView.CurrentCell.RowIndex == 11)
                {
                    TextBox txtV = (TextBox)e.Control;
                    txtV.TextChanged += new EventHandler(txtV_TextChanged);
                }
                if (GridView.CurrentCell.RowIndex == 3)
                {
                    TextBox txtWeight = (TextBox)e.Control;
                    txtWeight.TextChanged += new EventHandler(txtWeight_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (GridView.CurrentCell.RowIndex == 3)
            {
                if (((TextBox)sender).Text.ToString().Length < 40)
                {
                    ((TextBox)sender).Text = ((TextBox)sender).Text;
                }
                else
                {
                    ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.ToString().Length - 1);
                    ((TextBox)sender).Select(((TextBox)sender).Text.ToString().Length, 0);
                }

            }

        }

        public void txt_TextChanged(object sender, EventArgs e)
        {
            if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "Rate" || GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "MRP")
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 5, OMFunctions.MaskedType.NotNegative);
            }
        }

        public void txtV_TextChanged(object sender, EventArgs e)
        {
            if (GridView.CurrentCell.RowIndex == 8 || GridView.CurrentCell.RowIndex == 9 || GridView.CurrentCell.RowIndex == 10 || GridView.CurrentCell.RowIndex == 11)
            {
                if (((TextBox)sender).Text.ToString().Length < 31)
                {
                    ((TextBox)sender).Text = ((TextBox)sender).Text;
                }
                else
                {
                    ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.ToString().Length - 1);
                    ((TextBox)sender).Select(((TextBox)sender).Text.ToString().Length, 0);
                }

            }

        }

        private void dtpDate_Leave(object sender, EventArgs e)
        {
            try
            {
                string format = ObjQry.ReturnString("Select SettingValue from MBarcodeSettings where PkSrNo =" + Convert.ToInt64(GridView.Rows[GridView.CurrentCell.RowIndex].Cells[Colindex.PkSrNo].Value) + " ", CommonFunctions.ConStr);
                if (format == "" || format == null)
                    format = "dd-mm-yyyy";
                GridView.Rows[GridView.CurrentCell.RowIndex].Cells[4].Value = Convert.ToDateTime(dtpDate.SelectionStart).ToString(format);
                dtpDate.Visible = false;
                GridView.Focus();
                GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private bool Validation()
        {
            bool flag = true;
            if (txtNoOfPrint.Text.Trim() == "")
            {
                flag = false;
                OMMessageBox.Show("Please Enter No. of Sticker ...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtNoOfPrint.Focus();
                return flag;
            }
            if ((Convert.ToBoolean(GridView.Rows[7].Cells[5].FormattedValue)))
            {

                if (GridView.Rows[7].Cells[4].Value.ToString() == "")
                {
                    flag = false;
                    OMMessageBox.Show("Please Enter Best Before value...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    GridView.Focus();
                    return flag;
                }

            }
            return flag;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    ObjTrans.Execute("update MSetting set LastPrint=" + ObjFunction.GetComboValue(cmbBarcodeTemplate) + "", CommonFunctions.ConStr);
                    // string Barcode="", ItemName = "",CompanyName="";
                    DataTable dtCount = ObjFunction.GetDataView("Select * from MOtherSettings order by PkSrNo", CommonFunctions.ConStr).Table;

                    if (GridView.Rows[1].Cells[4].Value == null || GridView.Rows[1].Cells[4].Value == null)
                        GridView.Rows[1].Cells[4].Value = -1;

                    if (GridView.Rows[2].Cells[4].Value == null || GridView.Rows[2].Cells[4].Value == null)
                        GridView.Rows[2].Cells[4].Value = -1;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPrintScript)) == false)
                    {
                        #region Crystal Report Printing

                        string[] ReportSession;
                        ReportSession = new string[17];
                        ReportSession[0] = txtStartNo.Text;
                        ReportSession[1] = txtNoOfPrint.Text;
                        ReportSession[2] = Barcode;
                        ReportSession[3] = (Convert.ToBoolean(GridView.Rows[0].Cells[5].FormattedValue) == true) ? GridView.Rows[0].Cells[4].Value.ToString() : "";
                        ReportSession[4] = (Convert.ToBoolean(GridView.Rows[1].Cells[5].FormattedValue) == true) ? GridView.Rows[1].Cells[4].Value.ToString() : "-1";
                        ReportSession[5] = (Convert.ToBoolean(GridView.Rows[2].Cells[5].FormattedValue) == true) ? GridView.Rows[2].Cells[4].Value.ToString() : "-1";
                        ReportSession[6] = (Convert.ToBoolean(GridView.Rows[3].Cells[5].FormattedValue) == true) ? GridView.Rows[3].Cells[4].Value.ToString() : "";
                        ReportSession[7] = (Convert.ToBoolean(GridView.Rows[4].Cells[5].FormattedValue) == true) ? GridView.Rows[4].Cells[4].Value.ToString() : "";
                        ReportSession[8] = (Convert.ToBoolean(GridView.Rows[5].Cells[5].FormattedValue) == true) ? GridView.Rows[5].Cells[4].Value.ToString() : "";
                        ReportSession[9] = (Convert.ToBoolean(GridView.Rows[6].Cells[5].FormattedValue) == true) ? GridView.Rows[6].Cells[4].Value.ToString() : "";
                        ReportSession[10] = (Convert.ToBoolean(GridView.Rows[7].Cells[5].FormattedValue) == true) ? GridView.Rows[7].Cells[4].Value.ToString() : "";
                        ReportSession[11] = (Convert.ToBoolean(GridView.Rows[8].Cells[5].FormattedValue) == true) ? GridView.Rows[8].Cells[4].Value.ToString() : "";
                        ReportSession[12] = (Convert.ToBoolean(GridView.Rows[9].Cells[5].FormattedValue) == true) ? GridView.Rows[9].Cells[4].Value.ToString() : "";
                        ReportSession[13] = (Convert.ToBoolean(GridView.Rows[10].Cells[5].FormattedValue) == true) ? GridView.Rows[10].Cells[4].Value.ToString() : "";
                        ReportSession[14] = (Convert.ToBoolean(GridView.Rows[11].Cells[5].FormattedValue) == true) ? GridView.Rows[11].Cells[4].Value.ToString() : "";
                        // ReportSession[15] = (Convert.ToBoolean(GridView.Rows[12].Cells[5].FormattedValue) == true) ? GridView.Rows[12].Cells[4].Value.ToString() : "";
                        //ReportSession[12] = "";//(Convert.ToBoolean(GridView.Rows[9].Cells[5].FormattedValue)==true)?GridView.Rows[9].Cells[4].Value.ToString():"";

                        //for (int i = 0; i < dtCount.Rows.Count; i++)
                        //{
                        //    if (Convert.ToInt64(GridView.Rows[i].Cells[2].Value) == Convert.ToInt64(dtCount.Rows[i].ItemArray[0].ToString()))
                        //        ReportSession[3 + i] = GridView.Rows[i].Cells[4].Value.ToString();
                        //    else
                        //        ReportSession[3 + i] = "";
                        //    //ReportSession[5] = Convert.ToDateTime(dtpPackingDate.Value).ToString();
                        //    //ReportSession[6] = lblExpDate.Text.Trim();
                        //}
                        //ReportSession[3 + dtCount.Rows.Count] = CompanyName;
                        ReportSession[15] = ID.ToString();

                        ReportSession[16] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsIngredientBarcode)) == true) ? ObjQry.ReturnString("Select IngredientValue From MStockItemsIngredient Where ItemNo=" + ID + "", CommonFunctions.ConStr) : "";

                        if (OMMessageBox.Show("Do you want Preview of barcode?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Form NewF;
                            if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 3)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    NewF = new Display.ReportViewSource(new Reports.BarCodePrintBig50x50(), ReportSession);
                                else
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintBig50x50.rpt", CommonFunctions.ReportPath), ReportSession);
                                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 1)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    NewF = new Display.ReportViewSource(new Reports.BarCodePrintSmall34x22(), ReportSession);
                                else
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintSmall34x22.rpt", CommonFunctions.ReportPath), ReportSession);
                                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 2)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    NewF = new Display.ReportViewSource(new Reports.BarCodePrintMedium50x25(), ReportSession);
                                else
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintMedium50x25.rpt", CommonFunctions.ReportPath), ReportSession);

                                //NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintMedium50x25OnMart.rpt", CommonFunctions.ReportPath), ReportSession);
                                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 4)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    NewF = new Display.ReportViewSource(new Reports.BarCodePrintMedium50x25(), ReportSession);
                                else
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintLarge40x35.rpt", CommonFunctions.ReportPath), ReportSession);
                                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 5)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    NewF = new Display.ReportViewSource(new Reports.BarCodePrintMedium50x25(), ReportSession);
                                else
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintExtraLarge24x35.rpt", CommonFunctions.ReportPath), ReportSession);

                                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                            }

                        }
                        else
                        {
                            CrystalDecisions.CrystalReports.Engine.ReportDocument childForm = null;
                            if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 3)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    childForm = new Reports.BarCodePrintBig50x50();
                                else
                                    childForm = ObjFunction.LoadReportObject("BarCodePrintBig50x50.rpt", CommonFunctions.ReportPath);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 1)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    childForm = new Reports.BarCodePrintSmall34x22();
                                else

                                    childForm = ObjFunction.LoadReportObject("BarCodePrintSmall34x22.rpt", CommonFunctions.ReportPath);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 2)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    childForm = new Reports.BarCodePrintMedium50x25();
                                else

                                    childForm = ObjFunction.LoadReportObject("BarCodePrintMedium50x25.rpt", CommonFunctions.ReportPath);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 4)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    childForm = new Reports.BarCodePrintBig50x50();
                                else
                                    childForm = ObjFunction.LoadReportObject("BarCodePrintLarge40x35.rpt", CommonFunctions.ReportPath);
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 5)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                    childForm = new Reports.BarCodePrintExtraLarge24x35();
                                else
                                    childForm = ObjFunction.LoadReportObject("BarCodePrintExtraLarge24x35.rpt", CommonFunctions.ReportPath);
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
                        #endregion
                    }
                    else
                    {
                        #region BarCode Printing With USB DLL
                        string str = "";
                        string strcode = "";
                        string strvalue = "";

                        str = "UNIVERSALO";
                        DBBarCodePrint dbBarcodePrint = new DBBarCodePrint();
                        BarCodePrint barcodeprint = new BarCodePrint();

                        if (str != "" && TPurRate != 0)
                        {
                            for (int i = 0; i <= TPurRate.ToString().Length - 1; i++)
                            {
                                strvalue = TPurRate.ToString().Substring(i, 1);

                                switch (strvalue)
                                {
                                    case "1":
                                        strcode = strcode + "U";
                                        break;
                                    case "2":
                                        strcode = strcode + "N";
                                        break;
                                    case "3":
                                        strcode = strcode + "I";
                                        break;
                                    case "4":
                                        strcode = strcode + "V";
                                        break;
                                    case "5":
                                        strcode = strcode + "E";
                                        break;
                                    case "6":
                                        strcode = strcode + "R";
                                        break;
                                    case "7":
                                        strcode = strcode + "S";
                                        break;
                                    case "8":
                                        strcode = strcode + "A";
                                        break;
                                    case "9":
                                        strcode = strcode + "L";
                                        break;
                                    case "0":
                                        strcode = strcode + "O";
                                        break;
                                }
                            }
                        }
                        barcodeprint = new BarCodePrint();
                        barcodeprint.VarBarCode = Barcode;
                        barcodeprint.VarFirmName = (Convert.ToBoolean(GridView.Rows[0].Cells[5].FormattedValue) == true) ? GridView.Rows[0].Cells[4].Value.ToString() : "";
                        barcodeprint.VarMRP = (Convert.ToBoolean(GridView.Rows[1].Cells[5].FormattedValue) == true) ? GridView.Rows[1].Cells[4].Value.ToString() : "";
                        barcodeprint.VarRate = (Convert.ToBoolean(GridView.Rows[2].Cells[5].FormattedValue) == true) ? GridView.Rows[2].Cells[4].Value.ToString() : "";
                        barcodeprint.VarWeight = (Convert.ToBoolean(GridView.Rows[3].Cells[5].FormattedValue) == true) ? GridView.Rows[3].Cells[4].Value.ToString() : "";
                        barcodeprint.VarBrand = (Convert.ToBoolean(GridView.Rows[4].Cells[5].FormattedValue) == true) ? GridView.Rows[4].Cells[4].Value.ToString() : "";
                        barcodeprint.VarShortDesc = (Convert.ToBoolean(GridView.Rows[5].Cells[5].FormattedValue) == true) ? GridView.Rows[5].Cells[4].Value.ToString() : "";
                        barcodeprint.VarPackedDate = (Convert.ToBoolean(GridView.Rows[6].Cells[5].FormattedValue) == true) ? GridView.Rows[6].Cells[4].Value.ToString() : "";
                        barcodeprint.VarBestBefore = (Convert.ToBoolean(GridView.Rows[7].Cells[5].FormattedValue) == true) ? GridView.Rows[7].Cells[4].Value.ToString() : "";
                        barcodeprint.VarFreeText1 = (Convert.ToBoolean(GridView.Rows[8].Cells[5].FormattedValue) == true) ? GridView.Rows[8].Cells[4].Value.ToString() : "";
                        barcodeprint.VarFreeText2 = (Convert.ToBoolean(GridView.Rows[9].Cells[5].FormattedValue) == true) ? GridView.Rows[9].Cells[4].Value.ToString() : "";
                        barcodeprint.VarFreeText3 = (Convert.ToBoolean(GridView.Rows[10].Cells[5].FormattedValue) == true) ? GridView.Rows[10].Cells[4].Value.ToString() : "";
                        barcodeprint.VarFreeText4 = (Convert.ToBoolean(GridView.Rows[11].Cells[5].FormattedValue) == true) ? GridView.Rows[11].Cells[4].Value.ToString() : "";
                        barcodeprint.VarQuantity = Convert.ToInt64(txtNoOfPrint.Text);
                        if (strcode != "")
                        {
                            barcodeprint.VarCode = strcode;
                        }
                        dbBarcodePrint.AddBarCode(barcodeprint);

                        if (dbBarcodePrint.BarCodePrinting(ObjFunction.GetComboValue(cmbBarcodeTemplate)) == true)
                            OMMessageBox.Show("Printing Barcode sucessfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        else
                            OMMessageBox.Show("Barcode not Print...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        #endregion
                    }



                    //dbBarCodePrint.DeleteTBarCodePrint(tBarCodePrint);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbBarcodeTemplate_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void txtExpDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtExpDays.Text != "")
                {
                    btnOkExpDays.Focus();
                }
                else
                {
                    txtExpDays.Focus();
                }
            }
        }

        private void btnCancelExpDays_Click(object sender, EventArgs e)
        {
            pnlExpDays.Visible = false;
            btnOk.Focus();
        }

        private void dtpDate_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                string format = ObjQry.ReturnString("Select SettingValue from MBarcodeSettings where PkSrNo =" + Convert.ToInt64(GridView.Rows[GridView.CurrentCell.RowIndex].Cells[Colindex.PkSrNo].Value) + " ", CommonFunctions.ConStr);
                if (format == "" || format == null)
                    format = "dd-mm-yyyy";
                GridView.Rows[GridView.CurrentCell.RowIndex].Cells[4].Value = Convert.ToDateTime(dtpDate.SelectionStart).ToString(format);
                dtpDate.Visible = false;
                GridView.Focus();
                if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                    GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex + 1];
                else
                    GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnOkExpDays_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtExpDays.Text.Equals(""))
                {
                    txtExpDays.Text = "0";
                }
                pnlExpDays.Visible = false;
                DateChange();
                GridView.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtExpDays_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtExpDays, -1, 7, OMFunctions.MaskedType.NotNegative);
        }

        public void DateChange()
        {
            try
            {
                DateTime dt;
                string format = ObjQry.ReturnString("Select SettingValue from MBarcodeSettings where PkSrNo =" + Convert.ToInt64(GridView.Rows[GridView.CurrentCell.RowIndex].Cells[Colindex.PkSrNo].Value) + " ", CommonFunctions.ConStr);
                if (format == "" || format == null)
                    format = "dd-MM-yyyy";
                if (txtExpDays.Text.Trim() != "")
                {
                    dt = Convert.ToDateTime(GridView.Rows[6].Cells[4].Value).AddDays(Convert.ToInt64(txtExpDays.Text.Trim()));
                    GridView.Rows[7].Cells[4].Value = Convert.ToDateTime(dt).ToString(format);
                }
                else
                {
                    txtExpDays.Text = "0";
                    GridView.Rows[7].Cells[4].Value = Convert.ToDateTime(GridView.Rows[6].Cells[4].Value).ToString(format);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

    }
}
