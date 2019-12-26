using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using OM;
using OMControls;

namespace Yadi.Settings
{
    public partial class OtherSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        string strPsw;
        DBMSettings dbMSettings = new DBMSettings();

        public OtherSetting()
        {
            InitializeComponent();
        }

        private void SalesSettingAE_Load(object sender, EventArgs e)
        {
            //ObjFunction.FillLanguage(cmbLanguage, 1);
            //ObjFunction.FillLanguage(cmbDefaultBillPrint, 1);
            ObjFunction.FillComb(cmbLanguage, "Select LanguageNo,LanguageName From MLanguage Where LanguageNo!=1");
            ObjFunction.FillComb(cmbDefaultBillPrint, "Select LanguageNo,LanguageName From MLanguage");
            FillControls();

            KeyDownFormat(this.Controls);
        }

        private void FillControls()
        {

            ObjFunction.SetAppSettings();

            txtLabel1.Text = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
            txtLabel2.Text = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
            txtLabel3.Text = ObjFunction.GetAppSettings(AppSettings.CRateLabel);
            txtLabel4.Text = ObjFunction.GetAppSettings(AppSettings.DRateLabel);
            txtLabel5.Text = ObjFunction.GetAppSettings(AppSettings.ERateLabel);

            chkRate1.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)); chk_CheckedChanged(chkRate1, new EventArgs());
            chkRate2.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)); chk_CheckedChanged(chkRate2, new EventArgs());
            chkRate3.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive)); chk_CheckedChanged(chkRate3, new EventArgs());
            chkRate4.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive)); chk_CheckedChanged(chkRate4, new EventArgs());
            chkRate5.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive)); chk_CheckedChanged(chkRate5, new EventArgs());

            txtRate1Password.Text = ObjFunction.GetAppSettings(AppSettings.ARatePassword);
            txtRate2Password.Text = ObjFunction.GetAppSettings(AppSettings.BRatePassword);
            txtRate3Password.Text = ObjFunction.GetAppSettings(AppSettings.CRatePassword);
            txtRate4Password.Text = ObjFunction.GetAppSettings(AppSettings.DRatePassword);
            txtRate5Password.Text = ObjFunction.GetAppSettings(AppSettings.ERatePassword);

            chkRate1DBEffect.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateDBEffect));
            chkRate2DBEffect.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateDBEffect));
            chkRate3DBEffect.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateDBEffect));
            chkRate4DBEffect.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateDBEffect));
            chkRate5DBEffect.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateDBEffect));

            chkASupermode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateSuperMode));
            chkBSupermode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateSuperMode));
            chkCSupermode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateSuperMode));
            chkDSupermode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateSuperMode));
            chkESupermode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateSuperMode));

            ChkDepartmentDis.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_DepartmentDisplay));
            chkCategoryDis.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_CategoryDisplay));
            ChkBarCodeDis.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_BarCodeDisplay));
            ChkStockLocation.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_StockLocation));
            chkTaxTypeGrid.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.TaxTypeGridDisplay));
            ChkReportDisplay.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay));
            chkStockItemPrintBarCode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_PrintBarCode));
            chkRptExcel.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport));
            chkIsException.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsExceptionDisplay));
            chkEffectiveDate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_EffectiveDate));
            txtTopSales.Text = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue);
            chkIsBrand.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsBrandFilter));
            ChkShowLastBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_ShowLastBill));
            chkBilingual.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual));
            txtUpDownLoadPath.Text = ObjFunction.GetAppSettings(AppSettings.O_UpDownLoadPath);
            txtUpDownDays.Text = ObjFunction.GetAppSettings(AppSettings.O_UpDownTime);
            txtBackUpPath.Text = ObjFunction.GetAppSettings(AppSettings.O_BackUpPath);
            txtUpDownLink.Text = ObjFunction.GetAppSettings(AppSettings.O_UpDownLink);
            chkBillWithMRP.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_BillWithMRP));
            txtDeletePath.Text = ObjFunction.GetAppSettings(AppSettings.O_DeletePath);

            chkIngredientBarcode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsIngredientBarcode));
            chkNutritionBarcode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsNutritionBarcode));
            chkReceipeBarcode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsReceipeBarcode));

            if (chkBilingual.Checked == false)
            {
                cmbLanguage.Visible = false; lblLanguage.Visible = false;
                cmbDefaultBillPrint.Visible = false; lblDefaultBillPrint.Visible = false;
            }
            else
            {
                cmbLanguage.SelectedValue = ObjFunction.GetAppSettings(AppSettings.O_Language);
                ObjFunction.FillComb(cmbDefaultBillPrint, "Select LanguageNo,LanguageName From MLanguage Where LanguageNo=1 OR LanguageNo=" + ObjFunction.GetComboValue(cmbLanguage) + "");
                cmbDefaultBillPrint.SelectedValue = ObjFunction.GetAppSettings(AppSettings.O_DefaultBillPrint);
            }
            if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_BarCodePrintType)) == BarcodePrinterType.TSC)
                rdTSC.Checked = true;
            else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_BarCodePrintType)) == BarcodePrinterType.Godex)
                rdGodex.Checked = true;
            else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_BarCodePrintType)) == BarcodePrinterType.Argox)
                rdArgox.Checked = true;

            chkLBTApply.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_LBTSystem));
            chkCloseMRPManually.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCloseMRPManually));
            chkAutoGenBarCode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_AutogenerateBarcode));
            txtAutoGenerateLength.Text = ObjFunction.GetAppSettings(AppSettings.O_AutogenerateBarcodeLength);
            chkAutoGenBarCode_CheckedChanged(chkAutoGenBarCode, new EventArgs());

            chkWeighingBarCode.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsWeighingBarcode));
            txtWeighingBarCode.Text = ObjFunction.GetAppSettings(AppSettings.O_WeighingBarcodeChar);
            chkWeighingBarCode_CheckedChanged(chkWeighingBarCode, new EventArgs());
            chkWeighingBarcodeKGwise.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsWeighingBarcodeKGwise));

            chkPayTypeChrgCalculate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPayTypeChargesCalculation));
            chkIsPrintScript.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPrintScript));
            chkIsPartyDisplay.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea));
            chkIsCST.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsAllowCST));
            chkIsCForm.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsAllowCForm));

            chkSMSSend.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsSMSSend));
            chkEmailSend.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsEmailSend));

            chkAutoSendSMS.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAutoSMSSend));

            chkAutoSendEmail.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAutoEmailSend));

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTopSales.Text.Trim() == "") txtTopSales.Text = "5";
                if (Validations() == true)
                {
                    if (CheckValid() == true)
                    {
                        dbMSettings = new DBMSettings();
                        dbMSettings.AddAppSettings(AppSettings.ARateLabel, txtLabel1.Text);
                        dbMSettings.AddAppSettings(AppSettings.BRateLabel, txtLabel2.Text);
                        dbMSettings.AddAppSettings(AppSettings.CRateLabel, txtLabel3.Text);
                        dbMSettings.AddAppSettings(AppSettings.DRateLabel, txtLabel4.Text);
                        dbMSettings.AddAppSettings(AppSettings.ERateLabel, txtLabel5.Text);
                        dbMSettings.AddAppSettings(AppSettings.ARateIsActive, (chkRate1.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.BRateIsActive, (chkRate2.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.CRateIsActive, (chkRate3.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.DRateIsActive, (chkRate4.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.ERateIsActive, (chkRate5.Checked ? "True" : "False"));


                        dbMSettings.AddAppSettings(AppSettings.ARatePassword, txtRate1Password.Text);
                        dbMSettings.AddAppSettings(AppSettings.BRatePassword, txtRate2Password.Text);
                        dbMSettings.AddAppSettings(AppSettings.CRatePassword, txtRate3Password.Text);
                        dbMSettings.AddAppSettings(AppSettings.DRatePassword, txtRate4Password.Text);
                        dbMSettings.AddAppSettings(AppSettings.ERatePassword, txtRate5Password.Text);
                        dbMSettings.AddAppSettings(AppSettings.ARateDBEffect, (chkRate1DBEffect.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.BRateDBEffect, (chkRate2DBEffect.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.CRateDBEffect, (chkRate3DBEffect.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.DRateDBEffect, (chkRate4DBEffect.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.ERateDBEffect, (chkRate5DBEffect.Checked ? "True" : "False"));

                        dbMSettings.AddAppSettings(AppSettings.ARateSuperMode, (chkASupermode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.BRateSuperMode, (chkBSupermode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.CRateSuperMode, (chkCSupermode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.DRateSuperMode, (chkDSupermode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.ERateSuperMode, (chkESupermode.Checked ? "True" : "False"));

                        dbMSettings.AddAppSettings(AppSettings.O_DepartmentDisplay, (ChkDepartmentDis.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_CategoryDisplay, (chkCategoryDis.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_BarCodeDisplay, (ChkBarCodeDis.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_StockLocation, (ChkStockLocation.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.TaxTypeGridDisplay, (chkTaxTypeGrid.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.ReportDisplay, (ChkReportDisplay.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_PrintBarCode, (chkStockItemPrintBarCode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.IsExcelReport, (chkRptExcel.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_TopSalesValue, txtTopSales.Text.Trim());
                        dbMSettings.AddAppSettings(AppSettings.O_IsBrandFilter, (chkIsBrand.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_ShowLastBill, (ChkShowLastBill.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_Bilingual, (chkBilingual.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_IsExceptionDisplay, (chkIsException.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_Language, cmbLanguage.SelectedValue.ToString());
                        dbMSettings.AddAppSettings(AppSettings.O_DefaultBillPrint, cmbDefaultBillPrint.SelectedValue.ToString());
                        dbMSettings.AddAppSettings(AppSettings.O_UpDownLoadPath, txtUpDownLoadPath.Text.Trim());
                        dbMSettings.AddAppSettings(AppSettings.O_UpDownTime, txtUpDownDays.Text.Trim());
                        dbMSettings.AddAppSettings(AppSettings.O_BackUpPath, txtBackUpPath.Text.Trim());
                        dbMSettings.AddAppSettings(AppSettings.O_UpDownLink, txtUpDownLink.Text.Trim());

                        dbMSettings.AddAppSettings(AppSettings.O_BillWithMRP, (chkBillWithMRP.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_EffectiveDate, (chkEffectiveDate.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_LBTSystem, (chkLBTApply.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_DeletePath, txtDeletePath.Text.Trim());
                        dbMSettings.AddAppSettings(AppSettings.S_IsCloseMRPManually, (chkCloseMRPManually.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_AutogenerateBarcode, (chkAutoGenBarCode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_AutogenerateBarcodeLength, (txtAutoGenerateLength.Text.Trim() == "") ? "0" : txtAutoGenerateLength.Text.Trim());

                        dbMSettings.AddAppSettings(AppSettings.O_IsWeighingBarcode, (chkWeighingBarCode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_WeighingBarcodeChar, (txtWeighingBarCode.Text.Trim() == "") ? "" : txtWeighingBarCode.Text.Trim());
                        dbMSettings.AddAppSettings(AppSettings.O_IsWeighingBarcodeKGwise, (chkWeighingBarcodeKGwise.Checked ? "True" : "False"));

                        dbMSettings.AddAppSettings(AppSettings.O_IsPayTypeChargesCalculation, (chkPayTypeChrgCalculate.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_IsPrintScript, (chkIsPrintScript.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_IsPartyDisplayWithArea, (chkIsPartyDisplay.Checked ? "True" : "False"));

                        if (rdTSC.Checked == true)
                            dbMSettings.AddAppSettings(AppSettings.O_BarCodePrintType, BarcodePrinterType.TSC.ToString());
                        else if (rdGodex.Checked == true)
                            dbMSettings.AddAppSettings(AppSettings.O_BarCodePrintType, BarcodePrinterType.Godex.ToString());
                        else if (rdArgox.Checked == true)
                            dbMSettings.AddAppSettings(AppSettings.O_BarCodePrintType, BarcodePrinterType.Argox.ToString());
                        setBarcodeDefault();

                        dbMSettings.AddAppSettings(AppSettings.O_IsIngredientBarcode, (chkIngredientBarcode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_IsNutritionBarcode, (chkNutritionBarcode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_IsReceipeBarcode, (chkReceipeBarcode.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_IsAllowCST, (chkIsCST.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.O_IsAllowCForm, (chkIsCForm.Checked ? "True" : "False"));

                        dbMSettings.AddAppSettings(AppSettings.S_IsSMSSend, (chkSMSSend.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.S_IsEmailSend, (chkEmailSend.Checked ? "True" : "False"));

                        dbMSettings.AddAppSettings(AppSettings.S_IsAutoSMSSend, (chkAutoSendSMS.Checked ? "True" : "False"));
                        dbMSettings.AddAppSettings(AppSettings.S_IsAutoEmailSend, (chkAutoSendEmail.Checked ? "True" : "False"));

                        if (dbMSettings.ExecuteNonQueryStatements() == true)
                        {
                            ObjFunction.SetAppSettings();
                            OMMessageBox.Show("Other Settings Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }
                        else
                        {
                            OMMessageBox.Show("Other Settings Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Select Atleast one rate", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validations()
        {
            EP.SetError(txtLabel1, ""); EP.SetError(txtLabel2, "");
            EP.SetError(txtLabel3, ""); EP.SetError(txtLabel4, "");
            EP.SetError(txtLabel5, ""); EP.SetError(txtTopSales, "");
            EP.SetError(txtBackUpPath, "");
            EP.SetError(txtUpDownLink, "");
            bool flag = false;
            if (txtLabel1.Text == "")
            {
                EP.SetError(txtLabel1, "Enter First Label");
                EP.SetIconAlignment(txtLabel1, ErrorIconAlignment.MiddleRight);
                txtLabel1.Focus();
            }
            else if (txtLabel2.Text == "")
            {
                EP.SetError(txtLabel2, "Enter Second Label");
                EP.SetIconAlignment(txtLabel2, ErrorIconAlignment.MiddleRight);
                txtLabel2.Focus();
            }
            else if (txtLabel3.Text == "")
            {
                EP.SetError(txtLabel3, "Enter Third Label");
                EP.SetIconAlignment(txtLabel3, ErrorIconAlignment.MiddleRight);
                txtLabel3.Focus();
            }
            else if (txtLabel4.Text == "")
            {
                EP.SetError(txtLabel4, "Enter Fourth Label");
                EP.SetIconAlignment(txtLabel4, ErrorIconAlignment.MiddleRight);
                txtLabel4.Focus();
            }
            else if (txtLabel5.Text == "")
            {
                EP.SetError(txtLabel5, "Enter Fifth Label");
                EP.SetIconAlignment(txtLabel5, ErrorIconAlignment.MiddleRight);
                txtLabel5.Focus();
            }
            else if (ObjFunction.CheckNumeric(txtTopSales.Text.Trim()) == false)
            {

                EP.SetError(txtTopSales, "Enter Valid Value");
                EP.SetIconAlignment(txtTopSales, ErrorIconAlignment.MiddleRight);
                txtTopSales.Focus();

            }
            else if (txtBackUpPath.Text.Trim() == "")
            {
                EP.SetError(txtBackUpPath, "Enter Back Up Path");
                EP.SetIconAlignment(txtBackUpPath, ErrorIconAlignment.MiddleRight);
                txtBackUpPath.Focus();
            }
            else if (System.IO.Directory.Exists(txtBackUpPath.Text.Trim()) == false)
            {
                EP.SetError(txtBackUpPath, "Enter Correct Path");
                EP.SetIconAlignment(txtBackUpPath, ErrorIconAlignment.MiddleRight);
                txtBackUpPath.Focus();
            }
            else if (PasswordDuplicate() == false)
            {
                OMMessageBox.Show("not allowed duplicate password...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                btnSave.Focus();
            }
            else if (txtUpDownLink.Text.Trim() != "")
            {
                if (ObjFunction.CheckValidURL(txtUpDownLink.Text.Trim()) == false)
                {
                    EP.SetError(txtUpDownLink, "Please Enter valid HyperLink");
                    EP.SetIconAlignment(txtUpDownLink, ErrorIconAlignment.MiddleRight);
                    txtUpDownLink.Focus();
                }
                else
                    flag = true;
            }
            else
                flag = true;
            return flag;
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
                else if (ctrl is OMTabControl)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is OMTabPage)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            strPsw += (char)e.KeyValue;
            if (strPsw.ToLower() == "logicall")
            {
                DataTable dt = new DataTable();

                dt = ObjFunction.GetDataView("Select EmailID,EmailPass from MFirm where firmno ="+DBGetVal.FirmNo).Table;
                txtEmailId.Text = dt.Rows[0].ItemArray[0].ToString();
                txtEmailPass.Text = dt.Rows[0].ItemArray[1].ToString();
                strPsw = "";
                pnlEmailDetails.Visible = true;
            }
        }

        #endregion
        public bool PasswordDuplicate()
        {
            bool flag = true;
            string[] arr = new string[5];
            arr[0] = txtRate1Password.Text;
            arr[1] = txtRate2Password.Text;
            arr[2] = txtRate3Password.Text;
            arr[3] = txtRate4Password.Text;
            arr[4] = txtRate5Password.Text;

            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    if (arr[j] != "")
                    {
                        if (arr[i] == arr[j])
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }
            return flag;
        }

        public bool CheckValid()
        {
            bool flag = false;
            if (chkRate1.Checked == true)
            {
                flag = true;
            }
            else if (chkRate2.Checked == true)
            {
                flag = true;
            }
            else if (chkRate3.Checked == true)
            {
                flag = true;
            }
            else if (chkRate4.Checked == true)
            {
                flag = true;
            }
            else if (chkRate5.Checked == true)
            {
                flag = true;
            }
            return flag;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
            {
                ((CheckBox)sender).Text = "No";
                if (((CheckBox)sender).Name == "chkRate1")
                {
                    txtRate1Password.Text = "";
                    chkRate1DBEffect.Checked = false;
                }
                else if (((CheckBox)sender).Name == "chkRate2")
                {
                    txtRate2Password.Text = "";
                    chkRate2DBEffect.Checked = false;
                }
                else if (((CheckBox)sender).Name == "chkRate3")
                {
                    txtRate3Password.Text = "";
                    chkRate3DBEffect.Checked = false;
                }
                else if (((CheckBox)sender).Name == "chkRate4")
                {
                    txtRate4Password.Text = "";
                    chkRate4DBEffect.Checked = false;
                }
                else if (((CheckBox)sender).Name == "chkRate5")
                {
                    txtRate5Password.Text = "";
                    chkRate5DBEffect.Checked = false;
                }

            }

            if (((CheckBox)sender).Name == "chkRate1")
            {
                txtRate1Password.Visible = ((CheckBox)sender).Checked;
                chkRate1DBEffect.Visible = ((CheckBox)sender).Checked;
            }
            else if (((CheckBox)sender).Name == "chkRate2")
            {
                txtRate2Password.Visible = ((CheckBox)sender).Checked;
                chkRate2DBEffect.Visible = ((CheckBox)sender).Checked;
            }
            else if (((CheckBox)sender).Name == "chkRate3")
            {
                txtRate3Password.Visible = ((CheckBox)sender).Checked;
                chkRate3DBEffect.Visible = ((CheckBox)sender).Checked;
            }
            else if (((CheckBox)sender).Name == "chkRate4")
            {
                txtRate4Password.Visible = ((CheckBox)sender).Checked;
                chkRate4DBEffect.Visible = ((CheckBox)sender).Checked;
            }
            else if (((CheckBox)sender).Name == "chkRate5")
            {
                txtRate5Password.Visible = ((CheckBox)sender).Checked;
                chkRate5DBEffect.Visible = ((CheckBox)sender).Checked;
            }
        }

        private void chkTaxTypeGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void ChkReportDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkRptExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkIsBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void ChkShowLastBill_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkAutoDataUpload_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkBilingual_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";

            if (chkBilingual.Checked == false)
            {
                cmbLanguage.Visible = false; lblLanguage.Visible = false;
                cmbDefaultBillPrint.Visible = false; lblDefaultBillPrint.Visible = false;
            }
            else
            {
                cmbLanguage.SelectedIndex = 0;
                cmbLanguage.Visible = true; lblLanguage.Visible = true;
                cmbDefaultBillPrint.SelectedValue = "1";
                cmbDefaultBillPrint.Visible = true; lblDefaultBillPrint.Visible = true;
            }
        }

        private void cmbLanguage_Leave(object sender, EventArgs e)
        {
            ObjFunction.FillComb(cmbDefaultBillPrint, "Select LanguageNo,LanguageName From MLanguage Where LanguageNo=1 OR LanguageNo=" + ObjFunction.GetComboValue(cmbLanguage) + "");
        }

        private void cmbLanguage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbLanguage_Leave(sender, e);
                cmbDefaultBillPrint.Focus();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtUpDownLoadPath.Text = fbd.SelectedPath;
            }
        }

        private void txtUpDownDays_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtUpDownDays);
        }

        private void btnPath_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtBackUpPath.Text = fbd.SelectedPath;
            }
        }

        private void chkIsException_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void setBarcodeDefault()
        {
            int PTypeNo = 0;
            if (rdTSC.Checked == true)
                PTypeNo = 1;
            else if (rdGodex.Checked == true)
                PTypeNo = 2;
            else if (rdArgox.Checked == true)
                PTypeNo = 3;

            DataTable dtDefault = ObjFunction.GetDataView("Select BarcodeTemplateNo,DefaultScript From MBarcodeDefault Where PrinterTypeNo=" + PTypeNo + " order by BarcodeTemplateNo").Table;
            for (int i = 0; i < dtDefault.Rows.Count; i++)
            {
                long pksrNo = Convert.ToInt64(dtDefault.Rows[i].ItemArray[0].ToString());
                string script = dtDefault.Rows[i].ItemArray[1].ToString();
                dbMSettings.AddBarcodeDefault(pksrNo, script);
            }
        }

        private void chkBillWithMRP_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
            {
                if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType)) == 6)
                {
                    OMMessageBox.Show("Can't disable the option.\r\n\r\nPlease Change default Ratetype in Sales Setting.");
                    ((CheckBox)sender).Checked = true;
                    ((CheckBox)sender).Text = "Yes";
                }
                else
                {
                    ((CheckBox)sender).Text = "No";
                }
            }
        }

        private void chkLBTApply_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkCloseMRPManually_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkAutoGenBarCode_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                txtAutoGenerateLength.Visible = true;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
                txtAutoGenerateLength.Visible = false;
            }
        }

        private void txtAutoGenerateLength_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtAutoGenerateLength, 0, 2, OMFunctions.MaskedType.NotNegative);
        }

        private void chkWeighingBarCode_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                txtWeighingBarCode.Visible = true;
                chkWeighingBarcodeKGwise.Visible = true;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
                txtWeighingBarCode.Visible = false;
                chkWeighingBarcodeKGwise.Visible = false;
                chkWeighingBarcodeKGwise.Checked = false;
            }
        }

        private void chkPayTypeChrgCalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkIsPartyDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkIsCST_CheckedChanged(object sender, EventArgs e)
        {

            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
            {

                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkIsCForm_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
            {

                ((CheckBox)sender).Text = "No";
            }
        }

        private void cmbDefaultBillPrint_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSMS_EmailClose_Click(object sender, EventArgs e)
        {
            PnlSMS_EmailSetting.Visible = false;
        }

        private void chkSMSSend_Click(object sender, EventArgs e)
        {
            if (chkSMSSend.Checked == true)
            {
                PnlSMS_EmailSetting.Visible = true;
                chkAutoSendSMS.Visible = true;
               
            }
            else
            {
                if (chkEmailSend.Checked == true)
                {
                    PnlSMS_EmailSetting.Visible = true;
                    chkAutoSendSMS.Visible = false;
                    chkAutoSendEmail.Visible = true;
                }
                else
                {
                    PnlSMS_EmailSetting.Visible = false;
                    chkAutoSendEmail.Visible = false;
                }
            }
        }

        private void chkEmailSend_Click(object sender, EventArgs e)
        {
            if (chkEmailSend.Checked == true)
            {
                PnlSMS_EmailSetting.Visible = true;
                chkAutoSendEmail.Visible = true;
            }
            else
            {
                if (chkSMSSend.Checked == true)
                {
                    PnlSMS_EmailSetting.Visible = true;
                    chkAutoSendSMS.Visible = true;
                    chkAutoSendEmail.Visible = false;
                }
                else
                {
                    PnlSMS_EmailSetting.Visible = false;
                    chkAutoSendEmail.Visible = false;
                    chkAutoSendEmail.Checked = false;
                }
            }
        }

        private void chkAutoSendSMS_CheckedChanged(object sender, EventArgs e)
        {
         
        }
          
        private void chkAutoSendEmail_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void btnEmailOk_Click(object sender, EventArgs e)
        {
            ObjTrans.ExecuteQuery("Update MFirm set EmailID ='" + txtEmailId.Text.Trim() + "' , EmailPass ='" + txtEmailPass.Text.Trim() + "' where FirmNo="+DBGetVal.FirmNo , CommonFunctions.ConStr);
            strPsw = "";
            pnlEmailDetails.Visible = false;
        }

        private void btnEmailCancel_Click(object sender, EventArgs e)
        {
            strPsw = "";
            pnlEmailDetails.Visible = false;
        }
    }
}
