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
    public partial class NewItemMasterAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMItemMaster dbMItemMaster = new DBMItemMaster();
        MItemMaster mItemMaster = new MItemMaster();

        MRateSetting mRateSetting = new MRateSetting();
        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        DataTable dtSearch = new DataTable();
        public long ShortID = 0;
        long isSGSTSales = 0, isSGSTPur = 0, isCGSTSales = 0, isCGSTPur = 0, isIGSTSales = 0, isIGSTPur = 0, isCessSales = 0, isCessPur = 0;

        public NewItemMasterAE()
        {
            InitializeComponent();
        }

        private void NewItemMasterAE_Load(object sender, EventArgs e)
        {
            try
            {

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                chkActive.Checked = true;
                btnNew.Focus();
                btnLangLongDesc.Enabled = false;
                btnLangItemShortDisc.Enabled = false;

                ObjFunction.FillList(lstBrandName, "Select ItemGroupNo,ItemGroupName From MItemGroup");
                ObjFunction.FillList(lstUOM1, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMType='1' ORDER BY UOMName");
                ObjFunction.FillList(lstUOM2, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMType='0' ORDER BY UOMName");
                ObjFunction.FillList(lstSSGST, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                                "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                                                " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 51) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isSGSTSales + " Order by  MItemTaxSetting.Percentage ");
                ObjFunction.FillList(lstPSGST, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                                  "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                                                  " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = 51) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isSGSTPur + " Order by  MItemTaxSetting.Percentage ");
                ObjFunction.FillList(lstSCGST, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                                   "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                                                   " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 52) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCGSTSales + " Order by  MItemTaxSetting.TaxSettingName ");
                ObjFunction.FillList(lstPCGST, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                                   "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                                                   " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = 52) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCGSTPur + " Order by  MItemTaxSetting.TaxSettingName ");
                ObjFunction.FillList(lstSIGST, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                                     "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                                                     " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 53) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isIGSTSales + " Order by  MItemTaxSetting.TaxSettingName ");

                ObjFunction.FillList(lstPIGST, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                              "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                                              " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = 53) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isIGSTPur + " Order by  MItemTaxSetting.TaxSettingName ");
                ObjFunction.FillList(lstSCess, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                           "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                                           " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = " + GroupType.Cess + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCessSales + " Order by  MItemTaxSetting.TaxSettingName ");
                ObjFunction.FillList(lstPCess, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = " + GroupType.Cess + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCessPur + " Order by  MItemTaxSetting.TaxSettingName ");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillFields()
        {
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {

                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                chkActive.Checked = true;
                txtBarcobe.Focus();
                btnLangLongDesc.Enabled = true;
                btnLangItemShortDisc.Enabled = true;
                txtStockConv.Text = "1";
                txtMKTQty.Text = "1";
                txtPurRate.Text = "0";
                txtSMKTQty.Text = "1";
                txtSStockConv.Text = "1";

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Focus();
        }

        private void NewItemMasterAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            //ID = 0;
            //PkSrNo = 0;
            //SPkSrNo = 0;
        }

        private void txtBarcobe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBarcobe.Text.Trim() == "")
                {

                    OMMessageBox.Show("Enter Barcode Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    EP.SetIconAlignment(txtBarcobe, ErrorIconAlignment.MiddleRight);
                    txtBarcobe.Focus();
                }
                else
                {
                    txtShortCode.Focus();
                }
            }
        }

        private void txtShortCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtShortCode.Text.Trim() == "")
                {

                    OMMessageBox.Show("Enter Shortcode Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    EP.SetIconAlignment(txtShortCode, ErrorIconAlignment.MiddleRight);
                    txtShortCode.Focus();
                }
                else
                {
                    txtBrandName.Focus();
                }

            }
        }

        private void txtBrandName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBrandName.Text == "")
                {
                    pnlBrandName.Visible = true;
                    lstBrandName.Focus();
                }
                else
                {
                    pnlBrandName.Visible = false;

                    txtItemName.Focus();

                }
            }
        }

        private void lstBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtBrandName.Text = lstBrandName.Text;

                    txtBrandName.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtBrandName.Focus();

            }
        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtItemName.Text.Trim() == "")
                {

                    OMMessageBox.Show("Enter Item Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                    txtItemName.Focus();
                }
                else
                {
                    txtLangFullDesc.Focus();
                }

            }
        }

        private void txtItemShortDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtItemShortDisc.Text.Trim() == "")
                {

                    txtItemShortDisc.Focus();
                }
                else
                {
                    txtLangItemShortDisc.Focus();
                }

            }
        }

        private void txtLangFullDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtLangFullDesc.Text.Trim() == "")
                {

                    btnLangLongDesc.Focus();
                }
                else
                {
                    txtItemShortDisc.Focus();
                }

            }
        }

        private void txtLangItemShortDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtLangItemShortDisc.Text.Trim() == "")
                {

                    btnLangItemShortDisc.Focus();
                }
                else
                {
                    txtUOM1.Focus();
                }

            }
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLangFullDesc.Text.Trim().Length > 0)
                {
                    //string val = ObjFunction.ChecklLangVal(txtItemName.Text.Trim());
                    //if (val == "")
                    //{
                    frmkb = new Utilities.KeyBoard(1, txtItemName.Text.Trim(), txtLangFullDesc.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLangFullDesc.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();

                    }
                    //}
                    //else
                    //    txtLangFullDesc.Text = val;
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtItemName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtItemName.Text.Trim(), txtLangFullDesc.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLangFullDesc.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();

                        }
                    }
                    else
                        txtLangFullDesc.Text = val;
                    txtLangFullDesc.Focus();

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnLangItemShortDisc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLangItemShortDisc.Text.Trim().Length > 0)
                {
                    //string val = ObjFunction.ChecklLangVal(txtItemName.Text.Trim());
                    //if (val == "")
                    //{
                    frmkb = new Utilities.KeyBoard(1, txtItemShortDisc.Text.Trim(), txtLangItemShortDisc.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLangItemShortDisc.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                    //}
                    //else
                    //    txtLangFullDesc.Text = val;
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtItemShortDisc.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtItemShortDisc.Text.Trim(), txtLangItemShortDisc.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLangItemShortDisc.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLangItemShortDisc.Text = val;
                    txtLangItemShortDisc.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtUOM1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUOM1.Text == "")
                {
                    pnlUOM1.Visible = true;
                    lstUOM1.Focus();
                }
                else
                {
                    pnlUOM1.Visible = false;

                    txtMRP.Focus();

                }
            }
        }

        private void lstUOM1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtUOM1.Text = lstUOM1.Text;

                    txtUOM1.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtUOM1.Focus();

            }
        }

        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtMRP.Text == "")
                {

                    txtMRP.Focus();
                }
                else
                {
                    txtASaleRate.Text = txtMRP.Text;
                    txtBSaleRate.Text = txtMRP.Text;
                    pnlSUOM.Visible = true;
                    txtASaleRate.Focus();

                }
            }
        }

        private void txtASaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtASaleRate.Text == "")
                {

                    txtASaleRate.Focus();
                }
                else
                {

                    txtBSaleRate.Focus();

                }
            }
        }

        private void txtBSaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBSaleRate.Text == "")
                {

                    txtBSaleRate.Focus();
                }
                else
                {

                    txtStockConv.Focus();

                }
            }
        }

        private void txtStockConv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtStockConv.Text == "")
                {

                    txtStockConv.Focus();
                }
                else
                {

                    txtMKTQty.Focus();

                }
            }
        }
        private void txtMKTQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtMKTQty.Text == "")
                {

                    txtMKTQty.Focus();
                }
                else
                {

                    txtPurRate.Focus();

                }
            }
        }

        private void txtPurRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPurRate.Text == "")
                {

                    txtPurRate.Focus();
                }
                else
                {

                    txtUOM2.Focus();

                }
            }
        }

        private void txtUOM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUOM2.Text == "")
                {
                    pnlUOM2.Visible = true;
                    lstUOM2.Focus();
                }
                else
                {
                    pnlUOM2.Visible = false;

                    txtSMRP.Focus();

                }
            }
        }

        private void lstUOM2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtUOM2.Text = lstUOM2.Text;

                    txtUOM2.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtUOM2.Focus();

            }
        }

        private void txtSMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSMRP.Text == "")
                {

                    txtSMRP.Focus();
                }
                else
                {
                    txtSASaleRate.Focus();

                }
            }
        }

        private void txtSASaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSASaleRate.Text == "")
                {

                    txtSASaleRate.Focus();
                }
                else
                {
                    txtSBSaleRate.Focus();

                }
            }
        }

        private void txtSBSaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSBSaleRate.Text == "")
                {

                    txtSBSaleRate.Focus();
                }
                else
                {
                    txtSStockConv.Focus();

                }
            }
        }

        private void txtSStockConv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSStockConv.Text == "")
                {

                    txtSStockConv.Focus();
                }
                else
                {
                    txtSMKTQty.Focus();

                }
            }
        }

        private void txtSMKTQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSMKTQty.Text == "")
                {

                    txtSMKTQty.Focus();
                }
                else
                {
                    txtSPurRate.Focus();

                }
            }
        }

        private void txtSPurRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSPurRate.Text == "")
                {

                    txtSPurRate.Text = "0.00";
                }
                else
                {
                    chkActive.Focus();

                }
            }
        }

        private void chkActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (chkActive.Checked == true)
                {

                    txtHSNCode.Focus();
                }
                else
                {


                }
            }
        }

        private void txtDUOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtDUOM.Text == "")
                {
                    pnlDefaultUOM.Visible = true;
                    lstUOM2.Focus();
                }
                else
                {
                    pnlDefaultUOM.Visible = false;

                    txtHSNCode.Focus();

                }
            }
        }

        private void lstDUOM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtDUOM.Text = lstDUOM.Text;

                    txtDUOM.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtDUOM.Focus();

            }
        }

        private void txtHSNCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtHSNCode.Text == "")
                {

                    txtHSNCode.Focus();
                }
                else
                {
                    txtSSGST.Focus();

                }
            }
        }

        private void txtSSGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSSGST.Text == "")
                {
                    pnlSSGST.Visible = true;
                    lstSSGST.Focus();
                }
                else
                {
                    pnlSSGST.Visible = false;
                    txtPSGST.Focus();

                }
            }
        }

        private void txtPSGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPSGST.Text == "")
                {
                    pnlPSGST.Visible = true;
                    lstPSGST.Focus();
                }
                else
                {
                    pnlPSGST.Visible = false;
                    txtSCGST.Focus();

                }
            }
        }

        private void txtSCGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSCGST.Text == "")
                {
                    pnlSCGST.Visible = true;
                    lstSCGST.Focus();
                }
                else
                {
                    pnlSCGST.Visible = false;
                    txtPCGST.Focus();

                }
            }
        }

        private void txtPCGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPCGST.Text == "")
                {
                    pnlPCGST.Visible = true;
                    lstPCGST.Focus();
                }
                else
                {
                    pnlPCGST.Visible = false;
                    txtSIGST.Focus();

                }
            }
        }

        private void txtSIGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSIGST.Text == "")
                {
                    pnlSIGST.Visible = true;
                    lstSIGST.Focus();
                }
                else
                {
                    pnlSIGST.Visible = false;
                    txtPIGST.Focus();

                }
            }
        }

        private void txtPIGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPIGST.Text == "")
                {
                    pnlPIGST.Visible = true;
                    lstPIGST.Focus();
                }
                else
                {
                    pnlPIGST.Visible = false;
                    txtSCess.Focus();

                }
            }
        }

        private void txtSCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSCess.Text == "")
                {
                    pnlSCess.Visible = true;
                    lstSCess.Focus();
                }
                else
                {
                    pnlSCess.Visible = false;
                    txtPCess.Focus();

                }
            }
        }

        private void txtPCess_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPCess.Text == "")
                {
                    pnlPCess.Visible = true;
                    lstPCess.Focus();
                }
                else
                {
                    pnlPCess.Visible = false;
                    BtnSave.Focus();

                }
            }
        }

        private void lstSSGST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtSSGST.Text = lstSSGST.Text;

                    txtSSGST.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtSSGST.Focus();

            }
        }

        private void lstPSGST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtPSGST.Text = lstPSGST.Text;

                    txtPSGST.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtPSGST.Focus();

            }
        }

        private void lstSCGST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtSCGST.Text = lstSCGST.Text;

                    txtSCGST.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtSCGST.Focus();

            }
        }

        private void lstPCGST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtPCGST.Text = lstPCGST.Text;

                    txtPCGST.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtPCGST.Focus();

            }
        }

        private void lstSIGST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtSIGST.Text = lstSSGST.Text;

                    txtSIGST.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtSIGST.Focus();

            }
        }

        private void lstPIGST_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtPIGST.Text = lstPIGST.Text;

                    txtPIGST.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtPIGST.Focus();

            }
        }

        private void lstSCess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtSCess.Text = lstSCess.Text;

                    txtSCess.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtSCess.Focus();

            }
        }

        private void lstPCess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtPCess.Text = lstPCess.Text;

                    txtPCess.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtPCess.Focus();

            }
        }












    }
}
