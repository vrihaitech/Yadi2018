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

namespace Yadi.Master
{
    public partial class BarcodeTemplateAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMBarcodeTemplate dbMBarcodeTemplate = new DBMBarcodeTemplate();
        MBarcodeTemplate mBarcodeTemplate = new MBarcodeTemplate();

        public static string DefaultScript = "";

        public BarcodeTemplateAE()
        {
            InitializeComponent();
        }

        private void BarcodeTemplateAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbBarcodeTemplate, "Select PkSrNo,TemplateName+'-'+Size as TemplateName from MBarcodeTemplate order By PkSrNo");

                BtnSave.Enabled = false;
                BtnDefault.Enabled = false;
                cmbBarcodeTemplate.Focus();
                DataTable dtHelp = new DataTable();
                dtHelp.Columns.Add("Name");
                dtHelp.Columns.Add("Value");

                AddData(dtHelp, "Barcode", "VarBarcode");
                AddData(dtHelp, "Shop Name", "VarFirmName");
                AddData(dtHelp, "MRP", "VarMRP");
                AddData(dtHelp, "RATE", "VarRate");
                AddData(dtHelp, "Weight", "VarWeight");
                AddData(dtHelp, "Brand", "VarBrand");
                AddData(dtHelp, "Short Description", "VarShortDesc");
                AddData(dtHelp, "Packed Date", "VarPackedDate");
                AddData(dtHelp, "Best Before", "VarBestBefore");
                AddData(dtHelp, "Free Text 1", "VarFreeText1");
                AddData(dtHelp, "Free Text 2", "VarFreeText2");
                AddData(dtHelp, "Free Text 3", "VarFreeText3");
                AddData(dtHelp, "Free Text 4", "VarFreeText4");
                AddData(dtHelp, "No Of Print", "VarPrint");
                dgHelp.DataSource = dtHelp.DefaultView;
                this.dgHelp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void AddData(DataTable dt, string value, string DispName)
        {
            DataRow dr = dt.NewRow();
            dr[0] = value;
            dr[1] = DispName;
            dt.Rows.Add(dr);
        }

        public void FillControl()
        {
            try
            {
                mBarcodeTemplate = dbMBarcodeTemplate.ModifyMBarcodeTemplateByID(ObjFunction.GetComboValue(cmbBarcodeTemplate));

                txtSize.Text = mBarcodeTemplate.Size;
                txtScript.Text = mBarcodeTemplate.ScriptData;
                DefaultScript = mBarcodeTemplate.DefaultScript;
                FillPrinter(mBarcodeTemplate.PrinterName);
                BtnSave.Enabled = true;
                BtnDefault.Enabled = true;
                cmbPrinter.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillPrinter(string PrinterName)
        {
            try
            {
                bool flag = false;
                string strPrinters = null;
                int i = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID"); dt.Columns.Add("Desc");

                foreach (string strItem in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    DataRow dr = dt.NewRow();
                    dr[1] = (strPrinters + strItem);
                    dr[0] = i.ToString(); i++;
                    dt.Rows.Add(dr);
                    if (PrinterName != null)
                    {
                        if (PrinterName.ToString().ToUpper().Trim() == (strPrinters + strItem).ToString().ToUpper().Trim())
                            flag = true;
                    }
                }
                cmbPrinter.DisplayMember = dt.Columns[1].ColumnName;
                cmbPrinter.ValueMember = dt.Columns[0].ColumnName;
                cmbPrinter.DataSource = dt;

                if (flag == true)
                {
                    cmbPrinter.Text = PrinterName;
                }
                else
                {
                    System.Drawing.Printing.PrinterSettings objPrint = new System.Drawing.Printing.PrinterSettings();
                    cmbPrinter.Text = objPrint.PrinterName;
                }
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
                if (ObjFunction.GetComboValue(cmbBarcodeTemplate) != 0)
                {
                    FillControl();
                }
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
                    cmbBarcodeTemplate_Leave(sender, new EventArgs());
                    cmbPrinter.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void Clrscr()
        {
            try
            {
                cmbBarcodeTemplate.SelectedIndex = 0;
                txtSize.Text = "";
                txtScript.Text = "";
                cmbPrinter.DataSource = null;
                DefaultScript = "";
                BtnSave.Enabled = false;
                BtnDefault.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clrscr();
                cmbBarcodeTemplate.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbBarcodeTemplate) != 0)
                {
                    if (OMMessageBox.Show("Are you sure want to set Default Values ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        txtScript.Text = DefaultScript;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(cmbBarcodeTemplate) != 0)
                {
                    dbMBarcodeTemplate = new DBMBarcodeTemplate();
                    mBarcodeTemplate = new MBarcodeTemplate();

                    mBarcodeTemplate.PkSrNo = ObjFunction.GetComboValue(cmbBarcodeTemplate);
                    mBarcodeTemplate.ScriptData = txtScript.Text;
                    mBarcodeTemplate.PrinterName = cmbPrinter.Text;
                    mBarcodeTemplate.UserID = DBGetVal.FirmNo;
                    mBarcodeTemplate.UserDate = DBGetVal.ServerTime;
                    mBarcodeTemplate.CompanyNo = DBGetVal.FirmNo;

                    if (dbMBarcodeTemplate.AddMBarcodeTemplate(mBarcodeTemplate) == true)
                    {
                        OMMessageBox.Show("Barcode Template Updated Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        Clrscr();
                        cmbBarcodeTemplate.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Barcode Template Updated not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbPrinter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    txtScript.GotFocus += delegate { txtScript.Select(txtScript.Text.Length, txtScript.Text.Length); };
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkReadOnly_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkReadOnly.Checked == true)
                {
                    chkReadOnly.Text = "Edit On";
                    txtScript.ReadOnly = false;
                }
                else
                {
                    chkReadOnly.Text = "Read Only";
                    txtScript.ReadOnly = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

    }
}
