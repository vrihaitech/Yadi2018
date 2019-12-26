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
    public partial class BarcodeSettings : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMBarcodeSettings dbBarcodeSetting = new DBMBarcodeSettings();
        MBarcodeSettings mBarcodeSettings = new MBarcodeSettings();

        public BarcodeSettings()
        {
            InitializeComponent();
        }

        private void BarcodeSettings_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbBarcodeTemplate, "Select PkSrNo,TemplateName+'-'+Size as TemplateName from MBarcodeTemplate order By PkSrNo");
                cmbBarcodeTemplate.SelectedValue = ObjQry.ReturnLong("Select distinct isNull(BarcodeTemplateNo,0) from MBarcodeSettings", CommonFunctions.ConStr).ToString();
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
                        if (GridView.Rows.Count > 0)
                        {
                            GridView.CurrentCell = GridView[5, 0];
                            GridView.Focus();
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dbBarcodeSetting = new DBMBarcodeSettings();
                mBarcodeSettings = new MBarcodeSettings();

                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    mBarcodeSettings = new MBarcodeSettings();
                    mBarcodeSettings.PkSrNo = (GridView.Rows[i].Cells[Colindex.PkSrNo].Value == null) ? 0 : Convert.ToInt64(GridView.Rows[i].Cells[Colindex.PkSrNo].Value);
                    mBarcodeSettings.SettingNo = Convert.ToInt64(GridView.Rows[i].Cells[Colindex.SettingNo].Value);
                    mBarcodeSettings.BarcodeTemplateNo = ObjFunction.GetComboValue(cmbBarcodeTemplate);
                    mBarcodeSettings.SettingValue = (GridView.Rows[i].Cells[Colindex.Input].Value == null) ? "" : GridView.Rows[i].Cells[Colindex.Input].Value.ToString();
                    mBarcodeSettings.IsActive = Convert.ToBoolean(GridView.Rows[i].Cells[Colindex.IsActive].FormattedValue);
                    mBarcodeSettings.CompanyNo = DBGetVal.FirmNo;
                    dbBarcodeSetting.AddMBarcodeSettings(mBarcodeSettings);
                }
                if (dbBarcodeSetting.ExecuteNonQueryStatements() == true)
                {
                    OMMessageBox.Show("Barcode Settings Added Successfuly", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindGrid();
                    cmbBarcodeTemplate.Focus();
                }
                else
                {
                    OMMessageBox.Show("Barcode Settings Not Added Successfuly", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
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
                    if (dt.Rows.Count == 0)
                    {
                        str = " SELECT distinct 0 as SrNo , 0 as PkSrNo, ISNull(MOtherSettings.PkSrNo,0) AS SettingNo, MOtherSettings.SettingsKeyCode, MBarcodeSettings.SettingValue, 'false' as IsActive " +
                                      " FROM  MOtherSettings LEFT OUTER JOIN MBarcodeSettings ON MOtherSettings.PkSrNo  = MBarcodeSettings.SettingNo ";
                        dt = ObjFunction.GetDataView(str, CommonFunctions.ConStr).Table;

                    }
                    GridView.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridView.Rows.Add();
                        for (int j = 0; j < GridView.Columns.Count; j++)
                        {
                            long ii = Convert.ToInt64(GridView.Rows[i].Cells[1].Value);
                            if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 1)
                            {
                                if (ii == 4 || ii == 7 || ii == 8 || ii == 9 || ii == 10 || ii == 31 || ii == 34)
                                    GridView.Rows[i].ReadOnly = true;
                                else
                                    GridView.Rows[i].ReadOnly = false;
                            }
                            else if (ObjFunction.GetComboValue(cmbBarcodeTemplate) == 2)
                            {
                                if (ii == 19 || ii == 20 || ii == 32 || ii == 35)
                                    GridView.Rows[i].ReadOnly = true;
                                else
                                    GridView.Rows[i].ReadOnly = false;
                            }

                            GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                    }
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
                    //if (GridView.Rows[i].Cells[3].Value.ToString() == "MRP" || GridView.Rows[i].Cells[3].Value.ToString() == "Rate" || GridView.Rows[i].Cells[3].Value.ToString() == "Brand" || GridView.Rows[i].Cells[3].Value.ToString() == "Short Description")
                    //{
                    //    GridView.Rows[i].Cells[4].ReadOnly = true;
                    //    GridView.Rows[i].Cells[4].Value = "NA";
                    //}
                    if (GridView.Rows[i].Cells[3].Value.ToString() == "Shop Name" || GridView.Rows[i].Cells[3].Value.ToString() == "Short Description" || GridView.Rows[i].Cells[3].Value.ToString() == "Brand")
                    {
                        GridView.Rows[i].Cells[4].ReadOnly = true;
                        GridView.Rows[i].Cells[4].Value = "NA";
                    }
                    if (GridView.Rows[i].Cells[3].Value.ToString() == "Best Before")
                    {
                        GridView.Rows[i].Cells[4].ReadOnly = true;
                    }
                    if (GridView.Rows[i].Cells[3].Value.ToString() == "MRP" || GridView.Rows[i].Cells[3].Value.ToString() == "Rate")
                    {
                        GridView.Rows[i].Cells[4].ReadOnly = true;
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
            if (e.ColumnIndex == Colindex.SrNo)
            {
                e.Value = (e.RowIndex + 1).ToString();
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
                        pnlDateType.Location = new Point(x, y);
                        pnlDateType.Visible = true;
                        lstDateTpe.Focus();
                        //e.SuppressKeyPress = true;
                        //dtpDate.Location = new Point(x, y);
                        //dtpDate.Visible = true;
                        //dtpDate.Focus();
                    }
                    //if (GridView.CurrentCell.RowIndex == 6 && GridView.CurrentCell.ColumnIndex == 4)
                    //{
                    //    e.SuppressKeyPress = true;
                    //    pnlDateType.Location = new Point(x, y);                   
                    //    pnlDateType.Visible = true;
                    //    lstDateTpe.Focus();

                    //}
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    btnSave.Focus();
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

                    GridView.Rows[5].Cells[4].Value = dtpDate.Text;
                    dtpDate.Visible = false;
                    GridView.Focus();
                    GridView.CurrentCell = GridView[4, 6];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void lstDateTpe_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[4].Value = lstDateTpe.Text;
                    GridView.Rows[GridView.CurrentCell.RowIndex + 1].Cells[4].Value = lstDateTpe.Text;
                    pnlDateType.Visible = false;
                    if (GridView.CurrentCell.RowIndex < GridView.Rows.Count - 1)
                        GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex + 1];
                    else
                        GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex];

                    GridView.Focus();
                    //e.SuppressKeyPress = true;
                    //GridView.Rows[6].Cells[4].Value = lstDateTpe.Text;
                    //pnlDateType.Visible = false;
                    //GridView.CurrentCell = GridView[4, 7];
                    //GridView.Focus();
                }
                if (e.KeyCode == Keys.Space)
                {
                    e.SuppressKeyPress = true;
                    pnlDateType.Visible = false;
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
            BindGrid();
            cmbBarcodeTemplate.Focus();
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
            try
            {
                if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "Rate" || GridView.Rows[GridView.CurrentCell.RowIndex].Cells[3].Value.ToString() == "MRP")
                {
                    ObjFunction.SetMasked((TextBox)sender, 2, 5, OMFunctions.MaskedType.NotNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtV_TextChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void dtpDate_Leave(object sender, EventArgs e)
        {
            try
            {
                GridView.Rows[5].Cells[4].Value = dtpDate.Text;
                dtpDate.Visible = false;
                GridView.Focus();
                GridView.CurrentCell = GridView[4, 6];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbBarcodeTemplate_Leave(object sender, EventArgs e)
        {
            try
            {

                if (ObjFunction.GetComboValue(cmbBarcodeTemplate) > 0)
                {
                    BindGrid();
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
    }
}
