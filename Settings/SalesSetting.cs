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
    public partial class SalesSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Security secure = new Security();

        DBMSettings dbMSettings = new DBMSettings();
        bool IsChange = false;
        string strPsw;

        public SalesSetting()
        {
            InitializeComponent();
        }

        private void SalesSettingAE_Load(object sender, EventArgs e)
        {

            //cmbItemNameType.Enabled = false;
            ObjFunction.FillCombo(cmbParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ")");
            ObjFunction.FillCombo(cmbDefaultParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") AND IsActive='true'");
            //ObjFunction.FillCombo(cmbTaxType, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.DutiesAndTaxes + "");
            ObjFunction.FillCombo(cmbTaxType, "SELECT GroupNo, GroupName FROM MGroup WHERE (ControlGroup = " + GroupType.DutiesAndTaxes + " ) AND IsActive = 'True' ORDER BY GroupName");
            ObjFunction.FillCombo(cmbDiscount1, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbSchemeDisc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbOtherDisc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");

            ObjFunction.FillCombo(cmbItemDisc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbCharge1, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + ")");
            ObjFunction.FillCombo(cmbCharge2, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + ")");

            ObjFunction.FillCombo(cmbRoundOfAccount, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + "," + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbItemNameType, "SELECT ItemNameTypeNo,ItemNameType FROM MItemNameDisplayType ORDER BY ItemNameTypeNo");
            ObjFunction.FillComb(cmbOutwardLoc, "SELECT GodownNo,GodownName FROM MGodown where GodownNo<>1 ORDER BY GodownNo ");
            ObjFunction.FillCombo(cmbOrderType, "Select OrderTypeNo,OrderTypeName from MOrderType");
            ObjFunction.FillCombo(cmbPayType, "Select PkPayTypeNo,PayTypeName from MPayType Where PKPayTypeNo<>1");
            ObjFunction.FillCombo(cmbChrgTaxAcc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + ")");

            ObjFunction.FillCombo(cmbInterestAcc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + "," + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");

            FillRateType();

            FillControls();
            FillGrid();
            tabSalesSetting.SelectedIndex = 0;
            dgSettings.Visible = false;
            KeyDownFormat(this.Controls);

            lblWithin.Visible = false;
            numSeconds.Visible = false;
            lblSeconds.Visible = false;
        }

        private void FillGrid()
        {
            DataTable dt = new DataTable();
            dt = ObjFunction.GetDataView("Select 0 AS SrNo,SettingKeyCode,SettingValue,PkSettingNo From MSettings Where SettingTypeNo=4").Table;
            dgSettings.DataSource = dt.DefaultView;

        }

        private void FillControls()
        {

            ObjFunction.SetAppSettings();
            SetRateType(Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType)));
            cmbParty.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_PartyAC);
            cmbDefaultParty.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_DefaultPartyAC);
            cmbTaxType.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_TaxType);
            //cmbTransporter.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_Transporter);
            cmbOutwardLoc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation);
            chkDisc1.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display));

            chkChrg1.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges1Display));
            chkChrg2.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges2Display));

            cmbCharge1.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_Charges1);
            cmbCharge2.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_Charges2);

            cmbInterestAcc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_InterestAcc);

            cmbItemDisc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_ItemDisc);
            cmbDiscount1.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_Discount1);
            cmbSchemeDisc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_Discount2);
            cmbOtherDisc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_Discount3);

            cmbRoundOfAccount.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc);

            chkStopOnRate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnRate));
            chkStopOnQty.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnQty));
            chkIsBarcodeEnabled.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBarcodeEnabled));
            chkIsAllowsDuplicatesItemsInSameBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AllowsDuplicatesItems));

            chkIsReverseRateCalc.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc));

            if (ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VouchertypeCode in (" + VchType.Sales +
                "," + VchType.RejectionIn + ")", CommonFunctions.ConStr) == 0)
            {
                chkIsReverseRateCalc.Enabled = true;
            }

            cmbItemNameType.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_ItemNameType);


            chkStopOnDate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDate));
            chkStopOnParty.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty));
            chkStopOnRateType.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnRateType));
            chkStopOnTaxType.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnTaxType));
            chkStopOnGrid.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnGrid));
            chkSingleFirm.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAllowSingleFirmChq));
            chkMultipleFirm.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAllowMultipleChq));
            chkStopOnDisc.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDisc));

            chkDisplayRateType.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsDisplayRateType));
            chkRatePassword.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_RateTypeAskPassword));
            chkBillPrint.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrint));
            ChkPayableAmt.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AskPayableAmount));
            ChkManualBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsManualBillNo));
            txtChargeLabel.Text = ObjFunction.GetAppSettings(AppSettings.S_ChargeLabelName).ToString();
            txtChargeLabel2.Text = ObjFunction.GetAppSettings(AppSettings.S_Charge2LabelName).ToString();
            txtCreditCardDigits.Text = ObjFunction.GetAppSettings(AppSettings.S_CreditCardDigitLimit).ToString();
            // chkIsUseLastPartyWiseDisc.Enabled = false;
            chkIsStopOnSaleHistoryListEnabled.Enabled = false;
            txtSavingValue.Text = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
            txtFooterValue.Text = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
            txtFooter2Value.Text = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
            cmbOrderType.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_OrderType);
            chkShowSavingBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSavingBill));
            chkShowOutStanding.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding));
            chckShowSchemeDetails.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSchemeDetails));
            chkBillRoundOff.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff));
            chkBillWithMRP.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP));
            chkAddressInBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill));
            chkHomeDelivery.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillHomeDelivery));
            chkCounterBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillCouterBill));
            chkShowRemark.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark));
            chkShowVATNo.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowVatNo));
            chkAskHomeDelv.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AskHomeDelv));
            chkPayTypewiseBillNo.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPayTypewiseBillNo));
            chkFirmPayTypewisePrint.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsFirmPayTypewisePrint));
            chkShowRateHistoryAutomatically.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRateHistoryAutomatically));
            chkPostFirmwise.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise));
            chkDirectEstimateBilling.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_DirectEstimateBilling));
            chkTransportInBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsTransportInBill));
            chkPrintingCnt.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsEachTimeInitialise));
            chkIsPrintCount.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount));
            chkIsMRPDisplay.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsMRPDisplay));
            chkIsSaleCollectionPrint.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCollectionPrint));

            chkDisplayTaxType.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsDisplayTaxType));
            chkIsRateVeriation.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsRateVeriation));

            chkAskUserPassword.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAskUserPassword));
            chkAfterSaveNotDelItem.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AfterSaveNotDeleteItem));
            chkRateChangeByUser.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsRateChangeByUser));
            chkCreditBillUpdate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate));
            txtCreditBillUpdatePwd.Text = (ObjFunction.GetAppSettings(AppSettings.S_CreditBillPassword).ToString()) == "" ? "" : secure.psDecrypt(ObjFunction.GetAppSettings(AppSettings.S_CreditBillPassword).ToString());
            txtCreditBillUpdatePwd.Visible = chkCreditBillUpdate.Checked;

            cmbPayType.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_DefaultPayType);

            chkAllBillUpdate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillUpdate));
            txtBillUpdatePwd.Text = (ObjFunction.GetAppSettings(AppSettings.S_BillUpdatePwd).ToString()) == "" ? "" : secure.psDecrypt(ObjFunction.GetAppSettings(AppSettings.S_BillUpdatePwd).ToString());
            txtBillUpdatePwd.Visible = chkAllBillUpdate.Checked;

            chkBillDelete.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillDelete));
            txtBillDeletePwd.Text = (ObjFunction.GetAppSettings(AppSettings.S_BillDeletePwd).ToString()) == "" ? "" : secure.psDecrypt(ObjFunction.GetAppSettings(AppSettings.S_BillDeletePwd).ToString());

            txtBillDeletePwd.Visible = chkBillDelete.Checked;
            cmbBillPrintAsk.SelectedIndex = Convert.ToInt16(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrintAsk));
            cmbBillPrintAsk.Visible = chkBillPrint.Checked;

            //chkIsNewAskUserID.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsNewAskUserID));

            chkShowingLandedRate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowingLandedRate));

            chkIsTaxTypewiseBillNo.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsTaxTypewiseBillNo));
            chkShowAPMCDeatails.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowAPMCDetails));
            chkShowSalesMan.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSalesMan));
            chkIsEwayBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsEwayBill));
            if (chkDisplayTaxType.Checked == true)
            {
                chkStopOnTaxType.Enabled = false;
                chkStopOnTaxType.Checked = false;
            }
            else
            {
                chkStopOnTaxType.Enabled = true;
                chkStopOnTaxType.Checked = false;
            }

            chkPartywise.Checked = false;
            chkQuotationwise.Checked = false;
            if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 1)
            {
                chkPartywise.Checked = true;
            }
            else if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 2)
            {
                chkQuotationwise.Checked = true;
            }

            if (chkAddressInBill.Checked == true)
            {

                pnlAddressInBill.Visible = true;
            }
            else
            {
                pnlAddressInBill.Visible = false;
                chkHomeDelivery.Checked = false;
                chkCounterBill.Checked = false;

            }
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ChargesOnTax)) == true)
            {
                chkChrgTaxAcc.Checked = true;
                cmbChrgTaxAcc.Visible = true;
                cmbChrgTaxAcc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.S_ChargesOnTaxAcc);
            }
            else
            {
                chkChrgTaxAcc.Checked = false;
                cmbChrgTaxAcc.Visible = false;
            }

            chkUsePartyWiseFixedDisc.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPartyWiseDisc));
            chkMobileShop.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsMobileShop));
            FillGrid();

        }

        public void FillDay()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Day");
            dt.Columns.Add("DayNo");
            DataRow dr = null;
            dr = dt.NewRow();
            dr[0] = "Sunday";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "Sunday";
            dt.Rows.Add(dr);
            // lstRateType.DataSource = dt.DefaultView;
            //lstRateType.DisplayMember = dt.Columns[1].ColumnName;
            //lstRateType.ValueMember = dt.Columns[0].ColumnName;

        }
        #region Rate Type Realted Methods and Functions
        public void FillRateType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RateType");
            dt.Columns.Add("RateTypeName");
            DataRow dr = null;

            //dr = dt.NewRow();
            //dr[0] = "----Select----";
            //dr[1] = "0";
            //dt.Rows.Add(dr);

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
                dr[0] = "ASaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
                dr[0] = "BSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.CRateLabel);
                dr[0] = "CSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.DRateLabel);
                dr[0] = "DSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive)) == true)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.ERateLabel);
                dr[0] = "ESaleRate";
                dt.Rows.Add(dr);
            }
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_BillWithMRP)) == true)
            {
                dr = dt.NewRow();
                dr[1] = "MRP";
                dr[0] = "MRP";
                dt.Rows.Add(dr);
            }

            cmbRateType.DataSource = dt.DefaultView;
            cmbRateType.DisplayMember = dt.Columns[1].ColumnName;
            cmbRateType.ValueMember = dt.Columns[0].ColumnName;
        }

        public int GetRateType()
        {
            string str = ObjFunction.GetComboValueString(cmbRateType);
            int val = 0;
            if (str == "ASaleRate") val = 1;
            else if (str == "BSaleRate") val = 2;
            else if (str == "CSaleRate") val = 3;
            else if (str == "DSaleRate") val = 4;
            else if (str == "ESaleRate") val = 5;
            else if (str == "MRP") val = 6;
            return val;
        }

        public void SetRateType(long RateTypeNo)
        {
            if (RateTypeNo == 1) cmbRateType.SelectedValue = "ASaleRate";
            else if (RateTypeNo == 2) cmbRateType.SelectedValue = "BSaleRate";
            else if (RateTypeNo == 3) cmbRateType.SelectedValue = "CSaleRate";
            else if (RateTypeNo == 4) cmbRateType.SelectedValue = "DSaleRate";
            else if (RateTypeNo == 5) cmbRateType.SelectedValue = "ESaleRate";
            else if (RateTypeNo == 6) cmbRateType.SelectedValue = "MRP";
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validations() == true)
            {
                //panel 1
                dbMSettings.AddAppSettings(AppSettings.S_PartyAC, ObjFunction.GetComboValue(cmbParty).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_DefaultPartyAC, ObjFunction.GetComboValue(cmbDefaultParty).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_RateType, GetRateType().ToString());
                dbMSettings.AddAppSettings(AppSettings.S_ItemNameType, ObjFunction.GetComboValue(cmbItemNameType).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_TaxType, ObjFunction.GetComboValue(cmbTaxType).ToString());
                //dbMSettings.AddAppSettings(AppSettings.S_Transporter, ObjFunction.GetComboValue(cmbTransporter).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_RoundOfAcc, ObjFunction.GetComboValue(cmbRoundOfAccount).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_HideRatePopupAutomatically_Seconds, (numSeconds.Value.ToString()));
                if (ObjFunction.GetComboValue(cmbOutwardLoc) == 0)
                    dbMSettings.AddAppSettings(AppSettings.S_OutwardLocation, "2");
                else
                    dbMSettings.AddAppSettings(AppSettings.S_OutwardLocation, ObjFunction.GetComboValue(cmbOutwardLoc).ToString());

                //panel 2
                dbMSettings.AddAppSettings(AppSettings.S_Charges1, ObjFunction.GetComboValue(cmbCharge1).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_Charges2, ObjFunction.GetComboValue(cmbCharge2).ToString());


                dbMSettings.AddAppSettings(AppSettings.S_Charges1Display, (chkChrg1.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_Charges2Display, (chkChrg2.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_InterestAcc, ObjFunction.GetComboValue(cmbInterestAcc).ToString());

                //panel 3
                dbMSettings.AddAppSettings(AppSettings.S_Discount1, ObjFunction.GetComboValue(cmbDiscount1).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_Discount2, ObjFunction.GetComboValue(cmbSchemeDisc).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_Discount3, ObjFunction.GetComboValue(cmbOtherDisc).ToString());

                dbMSettings.AddAppSettings(AppSettings.S_ItemDisc, ObjFunction.GetComboValue(cmbItemDisc).ToString());

                dbMSettings.AddAppSettings(AppSettings.S_Discount1Display, (chkDisc1.Checked ? "True" : "False"));

                //panel 4
                dbMSettings.AddAppSettings(AppSettings.S_StopOnDate, (chkStopOnDate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_StopOnRateType, (chkStopOnRateType.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_StopOnParty, (chkStopOnParty.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_StopOnTaxType, (chkStopOnTaxType.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_StopOnGrid, (chkStopOnGrid.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_StopOnQty, (chkStopOnQty.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_StopOnRate, (chkStopOnRate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_StopOnDisc, (chkStopOnDisc.Checked ? "True" : "False"));

                //panel 5
                dbMSettings.AddAppSettings(AppSettings.S_IsBarcodeEnabled, (chkIsBarcodeEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_AllowsDuplicatesItems, (chkIsAllowsDuplicatesItemsInSameBill.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsReverseRateCalc, (chkIsReverseRateCalc.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_IsShowSalesHistoryEnabled, (chkIsShowSalesHistoryEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsShowPurchaseHistoryEnabled, (chkIsShowPurchaseHistoryEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsUseLastPartyWiseDiscEnabled, (chkIsUseLastPartyWiseDisc.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsStopOnSaleHistoryListEnabled, (chkIsStopOnSaleHistoryListEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ShowRateHistoryAutomatically, (chkShowRateHistoryAutomatically.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_HideRatePopupAutomatically, (chkHideRatePopupAutomatically.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_HideRatePopupAutomatically_Seconds, (numSeconds.Value.ToString()));
                dbMSettings.AddAppSettings(AppSettings.S_IsAllowMultipleChq, (chkMultipleFirm.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsAllowSingleFirmChq, (chkSingleFirm.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_IsDisplayRateType, (chkDisplayRateType.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_RateTypeAskPassword, (chkRatePassword.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsBillPrint, (chkBillPrint.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_AskPayableAmount, (ChkPayableAmt.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsManualBillNo, (ChkManualBill.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ChargeLabelName, ((txtChargeLabel.Text.Trim() == "") ? "Charges" : txtChargeLabel.Text.Trim()));
                dbMSettings.AddAppSettings(AppSettings.S_Charge2LabelName, ((txtChargeLabel2.Text.Trim() == "") ? "Charges2" : txtChargeLabel2.Text.Trim()));
                dbMSettings.AddAppSettings(AppSettings.S_CreditCardDigitLimit, txtCreditCardDigits.Text.Trim());
                dbMSettings.AddAppSettings(AppSettings.S_SettingValue, txtSavingValue.Text.Trim());
                dbMSettings.AddAppSettings(AppSettings.S_FooterValue, txtFooterValue.Text.Trim());
                dbMSettings.AddAppSettings(AppSettings.S_Footer2Value, txtFooter2Value.Text.Trim());
                dbMSettings.AddAppSettings(AppSettings.S_OrderType, ObjFunction.GetComboValue(cmbOrderType).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_ShowSavingBill, (chkShowSavingBill.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ShowOutStanding, (chkShowOutStanding.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsBillRoundOff, (chkBillRoundOff.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsBillWithMRP, (chkBillWithMRP.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsAddressInBill, (chkAddressInBill.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsAddressInBillHomeDelivery, (chkHomeDelivery.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsAddressInBillCouterBill, (chkCounterBill.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ShowSchemeDetails, (chckShowSchemeDetails.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ShowRemark, (chkShowRemark.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ShowVatNo, (chkShowVATNo.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_AskHomeDelv, (chkAskHomeDelv.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsPayTypewiseBillNo, (chkPayTypewiseBillNo.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsFirmPayTypewisePrint, (chkFirmPayTypewisePrint.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_PostFirmwise, (chkPostFirmwise.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_DirectEstimateBilling, (chkDirectEstimateBilling.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_IsTransportInBill, (chkTransportInBill.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsEachTimeInitialise, (chkPrintingCnt.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsPrintCount, (chkIsPrintCount.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsMRPDisplay, (chkIsMRPDisplay.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsCollectionPrint, (chkIsSaleCollectionPrint.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_IsDisplayTaxType, (chkDisplayTaxType.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_IsRateVeriation, (chkIsRateVeriation.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_IsAskUserPassword, (chkAskUserPassword.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_AfterSaveNotDeleteItem, (chkAfterSaveNotDelItem.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.S_IsCreditBillUpdate, (chkCreditBillUpdate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_CreditBillPassword, (txtCreditBillUpdatePwd.Text == "" ? "" : secure.psEncrypt(txtCreditBillUpdatePwd.Text)));

                dbMSettings.AddAppSettings(AppSettings.S_DefaultPayType, ObjFunction.GetComboValue(cmbPayType).ToString());

                dbMSettings.AddAppSettings(AppSettings.S_IsBillUpdate, (chkAllBillUpdate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_BillUpdatePwd, (txtBillUpdatePwd.Text == "" ? "" : secure.psEncrypt(txtBillUpdatePwd.Text)));

                dbMSettings.AddAppSettings(AppSettings.S_IsBillDelete, (chkBillDelete.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsRateChangeByUser, (chkRateChangeByUser.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_BillDeletePwd, (txtBillDeletePwd.Text == "" ? "" : secure.psEncrypt(txtBillDeletePwd.Text)));

                dbMSettings.AddAppSettings(AppSettings.S_ShowingLandedRate, (chkShowingLandedRate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsTaxTypewiseBillNo, (chkIsTaxTypewiseBillNo.Checked ? "True" : "False"));

                if (chkPartywise.Checked == false && chkQuotationwise.Checked == false)
                    dbMSettings.AddAppSettings(AppSettings.S_IsRateHistoryMaintain, "0");
                else if (chkPartywise.Checked == true)
                    dbMSettings.AddAppSettings(AppSettings.S_IsRateHistoryMaintain, "1");
                else if (chkQuotationwise.Checked == true)
                    dbMSettings.AddAppSettings(AppSettings.S_IsRateHistoryMaintain, "2");

                dbMSettings.AddAppSettings(AppSettings.S_IsBillPrintAsk, cmbBillPrintAsk.SelectedIndex.ToString());

                dbMSettings.AddAppSettings(AppSettings.S_ChargesOnTax, (chkChrgTaxAcc.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ChargesOnTaxAcc, ObjFunction.GetComboValue(cmbChrgTaxAcc).ToString());
                dbMSettings.AddAppSettings(AppSettings.S_ShowAPMCDetails, (chkShowAPMCDeatails.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_ShowSalesMan, (chkShowSalesMan.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsEwayBill, (chkIsEwayBill.Checked ? "True" : "False"));
                //dbMSettings.AddAppSettings(AppSettings.S_IsNewAskUserID, (chkIsNewAskUserID.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsPartyWiseDisc, (chkUsePartyWiseFixedDisc.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.S_IsMobileShop, (chkMobileShop.Checked ? "True" : "False"));

                if (IsChange == true)
                {
                    for (int i = 0; i < dgSettings.Rows.Count; i++)
                    {
                        dbMSettings.AddAppSettings(Convert.ToInt32(dgSettings.Rows[i].Cells[3].Value), (Convert.ToBoolean(dgSettings.Rows[i].Cells[2].FormattedValue) ? "True" : "False"));
                    }
                }

                if (dbMSettings.ExecuteNonQueryStatements() == true)
                {
                    ObjFunction.SetAppSettings();
                    OMMessageBox.Show("Sales Setting Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    ObjFunction.SetAppSettings();
                    FillControls();
                }
                else
                {
                    OMMessageBox.Show("Sales Setting Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    EP.SetError(btnSave, DBMSettings.strerrormsg);
                    EP.SetIconAlignment(btnSave, ErrorIconAlignment.MiddleRight);
                }
            }
        }

        public bool Validations()
        {
            EP.SetError(cmbItemNameType, "");
            EP.SetError(txtCreditCardDigits, "");
            bool flag = false;
            if (ObjFunction.GetComboValue(cmbItemNameType) <= 0)
            {
                EP.SetError(cmbItemNameType, "Select Item Name Display Type");
                EP.SetIconAlignment(cmbItemNameType, ErrorIconAlignment.MiddleRight);
                cmbItemNameType.Focus();
                tabSalesSetting.SelectedTab = tabPage1;
                flag = false;
            }
            else if (txtCreditCardDigits.Text.Trim() == "")
            {
                EP.SetError(txtCreditCardDigits, "Enter CreditCard Digits");
                EP.SetIconAlignment(txtCreditCardDigits, ErrorIconAlignment.MiddleRight);
                txtCreditCardDigits.Focus();
                tabSalesSetting.SelectedTab = tabPage4;
                flag = false;
            }
            else if (ObjFunction.CheckNumeric(txtCreditCardDigits.Text.Trim()) == false)
            {
                EP.SetError(txtCreditCardDigits, "Enter Valid Digits");
                EP.SetIconAlignment(txtCreditCardDigits, ErrorIconAlignment.MiddleRight);
                txtCreditCardDigits.Focus();
                tabSalesSetting.SelectedTab = tabPage4;
                flag = false;
            }
            else if (ObjFunction.GetComboValue(cmbOrderType) <= 0)
            {
                EP.SetError(cmbOrderType, "Select Order Type");
                EP.SetIconAlignment(cmbOrderType, ErrorIconAlignment.MiddleRight);
                cmbOrderType.Focus();
                tabSalesSetting.SelectedTab = tabPage5;
                flag = false;
            }
            else
                flag = true;
            return flag;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkChrg_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Name == "chkChrg1")
            {
                cmbCharge1.Enabled = chk.Checked;
                cmbCharge1.SelectedIndex = 0;
            }


        }

        private void chkChrg2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Name == "chkChrg2")
            {
                cmbCharge2.Enabled = chk.Checked;
                cmbCharge2.SelectedIndex = 0;
            }
        }

        private void chkDisc_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Name == "chkDisc1")
            {
                cmbDiscount1.Enabled = chk.Checked;
                cmbDiscount1.SelectedIndex = 0;
            }

        }

        private void dgSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                IsChange = true;
            }
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
                strPsw = "";
                tabSalesSetting.Visible = false;
                dgSettings.Visible = true;
                dgSettings.Size = tabSalesSetting.Size;
                dgSettings.Location = tabSalesSetting.Location;
                chkPostFirmwise.Visible = true;
                chkDirectEstimateBilling.Visible = true;
                chkIsEwayBill.Visible = true;
                chkMobileShop.Visible = true;
                if (chkPostFirmwise.Checked == true)
                {
                    chkDirectEstimateBilling.Visible = true;
                }

            }
            else if (strPsw.ToLower() == "123")
            {
                strPsw = "";
                // tabSalesSetting.Visible = false;
                Utilities.AdvanceSetting NewF = new Utilities.AdvanceSetting();
                ObjFunction.OpenForm(NewF);
            }

            else if (e.KeyCode == Keys.F1)
                tabSalesSetting.SelectedIndex = 0;
            else if (e.KeyCode == Keys.F2)
                tabSalesSetting.SelectedIndex = 1;
            else if (e.KeyCode == Keys.F3)
                tabSalesSetting.SelectedIndex = 2;
            else if (e.KeyCode == Keys.F4)
                tabSalesSetting.SelectedIndex = 3;
            else if (e.KeyCode == Keys.F5)
                tabSalesSetting.SelectedIndex = 4;
            else if (e.KeyCode == Keys.F6)
                tabSalesSetting.SelectedIndex = 5;
        }

        #endregion

        private void dgSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dgSettings.Visible = false;
                tabSalesSetting.Visible = true;
            }
        }

        private void SalesSettingAE_Activated(object sender, EventArgs e)
        {

        }

        private void SalesSettingAE_Click(object sender, EventArgs e)
        {
            strPsw = "";
        }

        private void dgSettings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = Convert.ToString(e.RowIndex + 1);
            }
        }

        private void chkIsShowSalesHistoryEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                chkIsUseLastPartyWiseDisc.Enabled = true;
                chkIsStopOnSaleHistoryListEnabled.Enabled = false;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
                chkIsUseLastPartyWiseDisc.Enabled = false;
                chkIsStopOnSaleHistoryListEnabled.Enabled = false;

                chkIsUseLastPartyWiseDisc.Checked = false;
                chkIsStopOnSaleHistoryListEnabled.Checked = false;

            }
        }

        private void chkIsUseLastSaleRateEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                //((CheckBox)sender).Text = "Yes";
                chkIsStopOnSaleHistoryListEnabled.Enabled = true;
            }
            else
            {
                //((CheckBox)sender).Text = "No";
                chkIsStopOnSaleHistoryListEnabled.Enabled = false;
                chkIsStopOnSaleHistoryListEnabled.Checked = false;

            }
        }

        private void chkIsStopOnSaleHistoryListEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkIsShowPurchaseHistoryEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkShowRateHistoryAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkHideRatePopupAutomatically_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                lblWithin.Visible = true;
                numSeconds.Visible = true;
                lblSeconds.Visible = true;
            }
            else
            {
                lblWithin.Visible = false;
                numSeconds.Visible = false;
                lblSeconds.Visible = false;
            }
        }

        private void chkSingleFirm_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                chkMultipleFirm.Checked = false;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkMultipleFirm_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                chkSingleFirm.Checked = false;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkBillPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                cmbBillPrintAsk.Visible = true;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
                cmbBillPrintAsk.Visible = false;
            }
        }

        private void ChkPayableAmt_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void ChkManualBill_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkFooterLevelDisc_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkItemLevelDisc_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkBillWithMRP_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkAddressInBill_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                pnlAddressInBill.Visible = true;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
                pnlAddressInBill.Visible = false;
                chkHomeDelivery.Checked = false;
                chkCounterBill.Checked = false;

            }
        }

        private void chkHomeDelivery_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkCounterBill_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkShowRemark_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkShowVATNo_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkAskHomeDelv_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkPayTypewiseBillNo_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkFirmPayTypewisePrint_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkTransportInBill_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkPrintingCnt_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkIsPrintCount_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkIsMRPDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";

        }

        private void chkIsSaleCooectionPrint_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";

        }

        private void chkPartywise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPartywise.Checked == true)
                chkQuotationwise.Checked = false;
        }

        private void chkQuotationwise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuotationwise.Checked == true)
                chkPartywise.Checked = false;
        }

        private void chkDisplayTaxType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDisplayTaxType.Checked == true)
            {
                chkStopOnTaxType.Enabled = false;
                chkStopOnTaxType.Checked = false;
            }
            else
            {
                chkStopOnTaxType.Enabled = true;
                chkStopOnTaxType.Checked = false;
            }
        }

        private void chkIsRateVeriation_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";

        }

        private void chkAskUserPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkAfterSaveNotDelItem_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkCreditBillUpdate_CheckedChanged(object sender, EventArgs e)
        {
            txtCreditBillUpdatePwd.Visible = ((CheckBox)sender).Checked;
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkAllBillUpdate_CheckedChanged(object sender, EventArgs e)
        {
            txtBillUpdatePwd.Visible = ((CheckBox)sender).Checked;
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkBillDelete_CheckedChanged(object sender, EventArgs e)
        {
            txtBillDeletePwd.Visible = ((CheckBox)sender).Checked;
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkChrgTaxAcc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChrgTaxAcc.Checked == true)
                cmbChrgTaxAcc.Visible = true;
            else cmbChrgTaxAcc.Visible = false;
        }

        private void chkPostFirmwise_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkPostFirmwise.Checked == true && chkPostFirmwise.Visible == true)
            //{
            //    chkDirectEstimateBilling.Visible = true;
            //}
            //else
            //{
            //    chkDirectEstimateBilling.Visible = false;
            //}
        }

        private void chkIsTaxTypewiseBillNo_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }
        }

        private void chkShowAPMCDeatails_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
                ((CheckBox)sender).Text = "Yes";
            else
                ((CheckBox)sender).Text = "No";
        }

        private void chkUsePartyWiseFixedDisc_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
            }
            else
            {
                ((CheckBox)sender).Text = "No";
            }

        }
    }
}
