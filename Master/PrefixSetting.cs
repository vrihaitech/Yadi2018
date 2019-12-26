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
    public partial class PrefixSetting : Form
    {
        OMCommonClass cc = new OMCommonClass();
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMBarcodePrefix dbBarcodePrefix = new DBMBarcodePrefix();
        MBarcodePrefix mbarcodePrefix = new MBarcodePrefix();


        string BarcodeNm;//ItemNm
        DataTable dtSearch = new DataTable();
        //DataTable dt;
        int cntRow;//, nw, ColumnIndex;
        //int defaultUOMRowNo = -1, LowerUOMRowNo = -1;
        //long PreDefaultUom = 0, PreLowerUom = 0;

        long ID;

        public PrefixSetting()
        {
            InitializeComponent();
        }

        
        private void FillComboAllMasters()
        {
            try
            {
                long iselected = 0;
                iselected = ObjFunction.GetComboValue(cmbGroupNo1);
                ObjFunction.FillCombo(cmbGroupNo1, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
                cmbGroupNo1.SelectedValue = iselected;

                iselected = ObjFunction.GetComboValue(cmbCompanyName);
                //ObjFunction.FillCombo(cmbCompanyName, "SELECT CompanyNo, CompanyName FROM MCompany " +
                //    //" WHERE IsActive = 'True' " +
                //    " WHERE CompanyUserCode != 'FALSE' " +
                //    " ORDER BY CompanyName");
                ObjFunction.FillCombo(cmbCompanyName, "SELECT FirmNo, FirmName FROM MFirm ORDER BY FirmName");
                cmbCompanyName.SelectedValue = iselected;

                iselected = ObjFunction.GetComboValue(cmbGroupNo2);
                ObjFunction.FillCombo(cmbGroupNo2, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=2 ORDER BY StockGroupName");
                cmbGroupNo2.SelectedValue = iselected;

                iselected = ObjFunction.GetComboValue(cmbDepartmentName);
                ObjFunction.FillCombo(cmbDepartmentName, "SELECT DepartmentNo, DepartmentName FROM MStockDepartment WHERE IsActive = 'True' ORDER BY DepartmentName");
                cmbDepartmentName.SelectedValue = iselected;

                iselected = ObjFunction.GetComboValue(cmbCategoryName);
                ObjFunction.FillCombo(cmbCategoryName, "SELECT CategoryNo, CategoryName FROM MStockCategory WHERE IsActive = 'True' ORDER BY CategoryName");
                cmbCategoryName.SelectedValue = iselected;

                iselected = ObjFunction.GetComboValue(cmbVatSales);
                ObjFunction.FillCombo(cmbVatSales, "SELECT MItemTaxSetting.PkSrNo, MItemTaxSetting.TaxSettingName FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                    " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 32)");
                cmbVatSales.SelectedValue = iselected;

                iselected = ObjFunction.GetComboValue(cmbVatPurchase);
                ObjFunction.FillCombo(cmbVatPurchase, "SELECT MItemTaxSetting.PkSrNo, MItemTaxSetting.TaxSettingName FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                    " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = 32)");
                cmbVatPurchase.SelectedValue = iselected;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void PrefixSetting_Load(object sender, EventArgs e)
        {
            try
            {
                FillComboAllMasters();
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dtSearch = ObjFunction.GetDataView("Select PkPrefixBarcodeNo from MBarcodePrefix order by PrefixBarcode").Table;
                BarcodeNm = "";
                if (dtSearch.Rows.Count > 0)
                {
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());

                    FillControls();
                    SetNavigation();
                }
                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ID = 0;
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            BarcodeNm = "";
            txtItemName.Focus();
        }

        private void FillControls()
        {
            try
            {
                EP.SetError(txtItemName, "");

                EP.SetError(cmbGroupNo1, "");
                EP.SetError(cmbGroupNo2, "");
                EP.SetError(cmbDepartmentName, "");
                EP.SetError(cmbCategoryName, "");
                EP.SetError(cmbCompanyName, "");

                EP.SetError(cmbVatSales, "");
                EP.SetError(cmbVatPurchase, "");
                mbarcodePrefix = dbBarcodePrefix.ModifyBarcodePrefixByID(ID);
                BarcodeNm = mbarcodePrefix.PrefixBarcode.ToUpper();
                cmbCompanyName.SelectedValue = mbarcodePrefix.CompanyNo.ToString();
                txtItemName.Text = mbarcodePrefix.PrefixBarcode.ToUpper();
                cmbGroupNo1.SelectedValue = mbarcodePrefix.BrandNo.ToString();
                cmbGroupNo2.SelectedValue = mbarcodePrefix.MainGroupNo.ToString();
                cmbCategoryName.SelectedValue = mbarcodePrefix.CategoryNo.ToString();
                cmbDepartmentName.SelectedValue = mbarcodePrefix.DepartmentNo.ToString();
                cmbVatPurchase.SelectedValue = mbarcodePrefix.PurchaseTaxSettingNo.ToString();
                cmbVatSales.SelectedValue = mbarcodePrefix.SalesTaxSettingNo.ToString();
                chkActive.Checked = mbarcodePrefix.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";

                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validations()
        {
            bool flag = true;
            EP.SetError(cmbGroupNo2, "");
            EP.SetError(cmbCompanyName, "");
            EP.SetError(cmbGroupNo1, "");
            EP.SetError(txtItemName, "");
           
            EP.SetError(cmbDepartmentName, "");
            EP.SetError(cmbCategoryName, "");
           
            EP.SetError(cmbVatPurchase, "");
            EP.SetError(cmbVatSales, "");

            if (ObjFunction.GetComboValue(cmbGroupNo2) <= 0)
            {
                EP.SetError(cmbGroupNo2, "Select Main Group Name");
                EP.SetIconAlignment(cmbGroupNo2, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; cmbGroupNo2.Focus(); }
            }
            if (ObjFunction.GetComboValue(cmbVatSales) <= 0)
            {
                EP.SetError(cmbVatSales, "Select Vat Sales Name");
                EP.SetIconAlignment(cmbVatSales, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; cmbVatSales.Focus(); }
            }
            if (ObjFunction.GetComboValue(cmbVatPurchase) <= 0)
            {
                EP.SetError(cmbVatPurchase, "Select Vat Purchase Name");
                EP.SetIconAlignment(cmbVatPurchase, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; cmbVatPurchase.Focus(); }
            }
            if (ObjFunction.GetComboValue(cmbCompanyName) <= 0)
            {
                EP.SetError(cmbCompanyName, "Select Company Name");
                EP.SetIconAlignment(cmbCompanyName, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; cmbCompanyName.Focus(); }
            }
            if (ObjFunction.GetComboValue(cmbGroupNo1) <= 0)
            {
                EP.SetError(cmbGroupNo1, "Select Brand Name");
                EP.SetIconAlignment(cmbGroupNo1, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; cmbGroupNo1.Focus(); }
            }
            if (txtItemName.Text.Trim() == "")
            {
                EP.SetError(txtItemName, "Enter Item Name");
                EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; txtItemName.Focus(); }
            }
            if (BarcodeNm != txtItemName.Text)
            {
                if (ObjQry.ReturnInteger("Select Count(*) from MStockItems where ItemName = '" + txtItemName.Text.Replace("'", "''") + "' ", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtItemName, "Duplicate Barcode");
                    EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtItemName.Focus(); }
                }
            }
           
            if (ObjFunction.GetComboValue(cmbDepartmentName) <= 0)
            {
                EP.SetError(cmbDepartmentName, "Select Department Name");
                EP.SetIconAlignment(cmbDepartmentName, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; cmbDepartmentName.Focus(); }
            }
            if (ObjFunction.GetComboValue(cmbCategoryName) <= 0)
            {
                EP.SetError(cmbCategoryName, "Select Category Name");
                EP.SetIconAlignment(cmbCategoryName, ErrorIconAlignment.MiddleRight);
                if (flag) { flag = false; cmbCategoryName.Focus(); }
            }
          
           

           

            return flag;
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {
                long No = 0;
                if (type == 5)
                {
                    No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    ID = No;
                }
                else if (type == 1)
                {
                    No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                    cntRow = 0;
                    ID = No;
                }
                else if (type == 2)
                {
                    No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    cntRow = dtSearch.Rows.Count - 1;
                    ID = No;
                }
                else
                {
                    if (type == 3)
                    {
                        cntRow = cntRow + 1;
                    }
                    else if (type == 4)
                    {
                        cntRow = cntRow - 1;
                    }

                    if (cntRow < 0)
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        cntRow = cntRow + 1;
                    }
                    else if (cntRow > dtSearch.Rows.Count - 1)
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        cntRow = cntRow - 1;
                    }
                    else
                    {
                        No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                        ID = No;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            FillControls();

        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID)
                {
                    cntRow = i;
                    break;
                }
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }

        #endregion

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
            if (e.KeyCode == Keys.Left && e.Control)
            {
                if (btnPrev.Enabled) btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (btnFirst.Enabled) btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                if (btnNext.Enabled) btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (btnLast.Enabled) btnLast_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }
           
            //else if (e.KeyCode == Keys.Escape)
            //{
            //    BtnExit_Click(sender, e);
            //}
        }
        #endregion

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validations() == true)
                {
                    dbBarcodePrefix = new DBMBarcodePrefix();
                    mbarcodePrefix = new MBarcodePrefix();
                    mbarcodePrefix.PkPrefixBarcodeNo = ID;
                    mbarcodePrefix.PrefixBarcode = txtItemName.Text.ToUpper();
                    mbarcodePrefix.MainGroupNo = ObjFunction.GetComboValue(cmbGroupNo2);
                    mbarcodePrefix.BrandNo = ObjFunction.GetComboValue(cmbGroupNo1);
                    mbarcodePrefix.CategoryNo = ObjFunction.GetComboValue(cmbCategoryName);
                    mbarcodePrefix.CompanyNo = ObjFunction.GetComboValue(cmbCompanyName);
                    mbarcodePrefix.DepartmentNo = ObjFunction.GetComboValue(cmbDepartmentName);
                    mbarcodePrefix.PurchaseTaxSettingNo = ObjFunction.GetComboValue(cmbVatPurchase);
                    mbarcodePrefix.SalesTaxSettingNo = ObjFunction.GetComboValue(cmbVatSales);
                    mbarcodePrefix.IsActive = chkActive.Checked;


                    if (dbBarcodePrefix.AddMBarcodePrefix(mbarcodePrefix) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show(" Barcode Prefix Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select PkPrefixBarcodeNo from MBarcodePrefix order by PrefixBarcode").Table;
                            ID = ObjQry.ReturnLong("Select Max(PkPrefixBarcodeNo) FRom MBarcodePrefix", CommonFunctions.ConStr);
                            SetNavigation();
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show(" Barcode Prefix Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnNew.Focus();
                        //btnNew_Click(new object(), new EventArgs());
                    }
                    else
                    {
                        OMMessageBox.Show(" Barcode Prefix not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtItemName.Focus();
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = true;
            txtBrPrefix.Enabled = true;
            txtBrPrefix.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbBarcodePrefix = new DBMBarcodePrefix();
                mbarcodePrefix = new MBarcodePrefix();

                mbarcodePrefix.PkPrefixBarcodeNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbBarcodePrefix.DeleteBarcodePrefix(mbarcodePrefix) == true)
                    {
                        OMMessageBox.Show("Barcode Prefix Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //ID = ObjQry.ReturnLong("Select Max(PkPrefixBarcodeNo) FRom MBarcodePrefix", CommonFunctions.ConStr);
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Barcode Prefix not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            BarcodeNm = "";
            this.Close();
        }

        private void PrefixSetting_Activated(object sender, EventArgs e)
        {
            FillComboAllMasters();
        }

        private void txtItemName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtItemName, "");
                if (txtItemName.Text != "")
                {
                    if (BarcodeNm != txtItemName.Text)
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MBarcodePrefix where PrefixBarcode = '" + txtItemName.Text.Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtItemName, "Duplicate Barcode Prefix");
                            EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                            txtItemName.Focus();
                        }
                        else if (cc.IsVarchar(txtItemName) == false)
                        {
                            EP.SetError(txtItemName, "Enter only Characters");
                            EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                            txtItemName.Focus();
                        }
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbVatSales.Focus();
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void btnSearchOk_Click(object sender, EventArgs e)
        {
            try
            {
                ID = ObjQry.ReturnInteger("Select PkPrefixBarcodeNo from MBarcodePrefix where PrefixBarcode = '" + txtBrPrefix.Text.Trim().Replace("'", "''") + "'", CommonFunctions.ConStr);
                if (ID != 0)
                    FillControls();
                else
                    OMMessageBox.Show("Barcode Prefix Not Exist..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                pnlSearch.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearchCancel_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
        }

        private void txtBrPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearchOk.Focus();
            }
        }

       

        

    

        
    }
}
