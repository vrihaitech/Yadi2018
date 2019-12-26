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
    public partial class PurchaseSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Security secure = new Security();

        DBMSettings dbMSettings = new DBMSettings();
        bool IsChange = false;
        string strPsw;

        public PurchaseSetting()
        {
            InitializeComponent();
        }

        private void PurchaseSettingAE_Load(object sender, EventArgs e)
        {
            tabPurchaseSetting.SelectedIndex = 0;
            ObjFunction.FillCombo(cmbParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.CashInhand + "," + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")");
            ObjFunction.FillCombo(cmbDefaultParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.CashInhand + "," + GroupType.SundryCreditors + ") AND IsActive='true'");
            //ObjFunction.FillCombo(cmbTaxType, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.DutiesAndTaxes + "");
            ObjFunction.FillCombo(cmbTaxType, "SELECT GroupNo, GroupName FROM MGroup WHERE (ControlGroup = " + GroupType.DutiesAndTaxes + " ) AND IsActive = 'True' ORDER BY GroupName");
            //ObjFunction.FillCombo(cmbTransporter, "Select TransporterNo,TransporterName from MTransporter");
            ObjFunction.FillCombo(cmbDiscount1, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbDisplayDisc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");

            ObjFunction.FillCombo(cmbATaxItemDisc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbBTaxItemDisc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbCharge1, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + ")");
            ObjFunction.FillCombo(cmbCharge2, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + ")");

            ObjFunction.FillCombo(cmbRoundOfAccount, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + "," + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");
            ObjFunction.FillCombo(cmbItemNameType, "SELECT ItemNameTypeNo,ItemNameType FROM MItemNameDisplayType ORDER BY ItemNameTypeNo");
            ObjFunction.FillComb(cmbOutwardLoc, "SELECT GodownNo,GodownName FROM MGodown where GodownNo<>1 ORDER BY GodownNo");
            ObjFunction.FillCombo(cmbPayType, "Select PkPayTypeNo,PayTypeName from MPayType Where PKPayTypeNo<>1");
            ObjFunction.FillCombo(cmbChrgTaxAcc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.VAT + ")");
            ObjFunction.FillCombo(cmbChrgAddTaxAcc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.VAT + ")");

            ObjFunction.FillCombo(cmbInterestAcc, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.InDirectExpenses + "," + GroupType.IndirectIncome + "," + GroupType.DirectExpenses + "," + GroupType.DirectIncome + ")");

            FillRateType();

            FillControls();
            FillGrid();
            dgSettings.Visible = false;
            KeyDownFormat(this.Controls);

            lblWithin.Visible = false;
            numSeconds.Visible = false;
            lblSeconds.Visible = false;
        }

        private void FillGrid()
        {
            DataTable dt = new DataTable();
            dt = ObjFunction.GetDataView("Select 0 AS SrNo,SettingKeyCode,SettingValue,PkSettingNo From MSettings Where SettingTypeNo=5").Table;
            dgSettings.DataSource = dt.DefaultView;

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

            //For Purchase Rate
            dr = dt.NewRow();
            dr[1] = "Purchase Rate";
            dr[0] = "PurRate";
            dt.Rows.Add(dr);


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
            else if (str == "PurRate") val = 6;
            return val;
        }

        public void SetRateType(long RateTypeNo)
        {
            if (RateTypeNo == 1) cmbRateType.SelectedValue = "ASaleRate";
            else if (RateTypeNo == 2) cmbRateType.SelectedValue = "BSaleRate";
            else if (RateTypeNo == 3) cmbRateType.SelectedValue = "CSaleRate";
            else if (RateTypeNo == 4) cmbRateType.SelectedValue = "DSaleRate";
            else if (RateTypeNo == 5) cmbRateType.SelectedValue = "ESaleRate";
            else if (RateTypeNo == 6) cmbRateType.SelectedValue = "PurRate";
        }
        #endregion

        private void FillControls()
        {

            ObjFunction.SetAppSettings();
            SetRateType(Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType)));
            cmbParty.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_PartyAC);
            cmbDefaultParty.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_DefaultPartyAC);
            cmbTaxType.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_TaxType);
            //cmbTransporter.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_Transporter);
            cmbOutwardLoc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_OutwardLocation);
            chkDisc1.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Discount1Display));

            chkChrg1.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Charges1Display));


            cmbCharge1.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_Charges1);
            cmbCharge2.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_Charges2);
            txtChange2.Text = ObjFunction.GetAppSettings(AppSettings.P_Charges2Display);

            cmbInterestAcc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_InterestAcc);

            cmbATaxItemDisc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_ATaxItemDisc);
            cmbBTaxItemDisc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_BTaxItemDisc);
            cmbDiscount1.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_Discount1);
            cmbDisplayDisc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_DisplayDiscount);

            cmbRoundOfAccount.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_RoundOfAcc);

            chkStopOnRate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnRate));
            chkStopOnQty.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnQty));
            chkStopOnNetAmt.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnNetAmount));
            chkStopOnRemark.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnRemark));
            chkIsBarcodeEnabled.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBarcodeEnabled));
            chkIsAllowsDuplicatesItemsInSameBill.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsAllowsDuplicatesItemsInSameBill));
            chkIsReverseRateCalc.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsReverseRateCalc));

            cmbItemNameType.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_ItemNameType);


            chkStopOnDate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnDate));
            chkStopOnParty.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnParty));
            chkStopOnHeaderDisc.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnHeaderDisc));
            chkStopOnTaxType.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnTaxType));
            chkStopOnGrid.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnGrid));
            chkSingleFirm.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsAllowSingleFirmChq));
            chkMultipleFirm.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsAllowMultipleChq));
            chkAllBarCodePrint.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_AllBarCodePrint));

            chkDisplayRateType.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsDisplayRateType));
            chkRatePassword.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_RateTypeAskPassword));
            chkBillRoundOff.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBillRoundOff));

            chkAutoMFG.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_AutoMFGMapping));

            chkNetRate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.PB_NetRate));
            chkPaymentPrint.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsPaymentPrint));
            cmbPayType.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_DefaultPayType);

            chkCreditBillUpdate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsCreditBillUpdate));
            txtCreditBillUpdatePwd.Text = (ObjFunction.GetAppSettings(AppSettings.P_CreditBillPassword).ToString()) == "" ? "" : secure.psDecrypt(ObjFunction.GetAppSettings(AppSettings.P_CreditBillPassword).ToString());
            txtCreditBillUpdatePwd.Visible = chkCreditBillUpdate.Checked;

            chkAllBillUpdate.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBillUpdate));
            txtBillUpdatePwd.Text = (ObjFunction.GetAppSettings(AppSettings.P_BillUpdatePwd).ToString()) == "" ? "" : secure.psDecrypt(ObjFunction.GetAppSettings(AppSettings.P_BillUpdatePwd).ToString());
            txtBillUpdatePwd.Visible = chkAllBillUpdate.Checked;
            chkIsBroker.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBroker));

            chkIsUseLastSaleRateEnabled.Enabled = false;
            chkIsStopOnSaleHistoryListEnabled.Enabled = false;

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_ChargesOnTax)) == true)
            {
                chkChrgTaxAcc.Checked = true;
                cmbChrgTaxAcc.Visible = true;
                cmbChrgTaxAcc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_ChargesOnTaxAcc);
                cmbChrgAddTaxAcc.Visible = true;
                cmbChrgAddTaxAcc.SelectedValue = ObjFunction.GetAppSettings(AppSettings.P_ChargesOnAddTaxAcc);
                lblAddChrgAcc.Visible = true;
            }
            else
            {
                chkChrgTaxAcc.Checked = false;
                cmbChrgTaxAcc.Visible = false;
                cmbChrgAddTaxAcc.Visible = false;
                lblAddChrgAcc.Visible = false;
            }

            FillGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validations() == true)
            {
                //panel 1
                dbMSettings.AddAppSettings(AppSettings.P_PartyAC, ObjFunction.GetComboValue(cmbParty).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_DefaultPartyAC, ObjFunction.GetComboValue(cmbDefaultParty).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_RateType, GetRateType().ToString());
                dbMSettings.AddAppSettings(AppSettings.P_ItemNameType, ObjFunction.GetComboValue(cmbItemNameType).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_TaxType, ObjFunction.GetComboValue(cmbTaxType).ToString());
                //dbMSettings.AddAppSettings(AppSettings.P_Transporter, ObjFunction.GetComboValue(cmbTransporter).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_RoundOfAcc, ObjFunction.GetComboValue(cmbRoundOfAccount).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_HideRatePopupAutomatically_Seconds, (numSeconds.Value.ToString()));
                if (ObjFunction.GetComboValue(cmbOutwardLoc) == 0)
                    dbMSettings.AddAppSettings(AppSettings.P_OutwardLocation, "2");
                else
                    dbMSettings.AddAppSettings(AppSettings.P_OutwardLocation, ObjFunction.GetComboValue(cmbOutwardLoc).ToString());

                //panel 2
                dbMSettings.AddAppSettings(AppSettings.P_Charges1, ObjFunction.GetComboValue(cmbCharge1).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_Charges2, ObjFunction.GetComboValue(cmbCharge2).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_Charges2Display, txtChange2.Text);

                dbMSettings.AddAppSettings(AppSettings.P_Charges1Display, (chkChrg1.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.P_InterestAcc, ObjFunction.GetComboValue(cmbInterestAcc).ToString());

                //panel 3
                dbMSettings.AddAppSettings(AppSettings.P_Discount1, ObjFunction.GetComboValue(cmbDiscount1).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_DisplayDiscount, ObjFunction.GetComboValue(cmbDisplayDisc).ToString());

                dbMSettings.AddAppSettings(AppSettings.P_ATaxItemDisc, ObjFunction.GetComboValue(cmbATaxItemDisc).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_BTaxItemDisc, ObjFunction.GetComboValue(cmbBTaxItemDisc).ToString());

                dbMSettings.AddAppSettings(AppSettings.P_Discount1Display, (chkDisc1.Checked ? "True" : "False"));

                //panel 4
                dbMSettings.AddAppSettings(AppSettings.P_StopOnDate, (chkStopOnDate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnHeaderDisc, (chkStopOnHeaderDisc.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnParty, (chkStopOnParty.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnTaxType, (chkStopOnTaxType.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnGrid, (chkStopOnGrid.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnQty, (chkStopOnQty.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnRate, (chkStopOnRate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnNetAmount, (chkStopOnNetAmt.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_StopOnRemark, (chkStopOnRate.Checked ? "True" : "False"));
                //panel 5
                dbMSettings.AddAppSettings(AppSettings.P_IsBarcodeEnabled, (chkIsBarcodeEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsAllowsDuplicatesItemsInSameBill, (chkIsAllowsDuplicatesItemsInSameBill.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsReverseRateCalc, (chkIsReverseRateCalc.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.P_IsShowSalesHistoryEnabled, (chkIsShowSalesHistoryEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsShowPurchaseHistoryEnabled, (chkIsShowPurchaseHistoryEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsUseLastSaleRateEnabled, (chkIsUseLastSaleRateEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsStopOnSaleHistoryListEnabled, (chkIsStopOnSaleHistoryListEnabled.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_ShowRateHistoryAutomatically, (chkShowRateHistoryAutomatically.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_HideRatePopupAutomatically, (chkHideRatePopupAutomatically.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_HideRatePopupAutomatically_Seconds, (numSeconds.Value.ToString()));
                dbMSettings.AddAppSettings(AppSettings.P_IsAllowMultipleChq, (chkMultipleFirm.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsAllowSingleFirmChq, (chkSingleFirm.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_AllBarCodePrint, (chkAllBarCodePrint.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.P_IsDisplayRateType, (chkDisplayRateType.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_RateTypeAskPassword, (chkRatePassword.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsBillRoundOff, (chkBillRoundOff.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.P_AutoMFGMapping, (chkAutoMFG.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.PB_NetRate, (chkNetRate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_IsPaymentPrint, (chkPaymentPrint.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.P_DefaultPayType, ObjFunction.GetComboValue(cmbPayType).ToString());

                dbMSettings.AddAppSettings(AppSettings.P_IsCreditBillUpdate, (chkCreditBillUpdate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_CreditBillPassword, (txtCreditBillUpdatePwd.Text == "" ? "" : secure.psEncrypt(txtCreditBillUpdatePwd.Text)));

                dbMSettings.AddAppSettings(AppSettings.P_IsBillUpdate, (chkAllBillUpdate.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_BillUpdatePwd, (txtBillUpdatePwd.Text == "" ? "" : secure.psEncrypt(txtBillUpdatePwd.Text)));

                dbMSettings.AddAppSettings(AppSettings.P_ChargesOnTax, (chkChrgTaxAcc.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.P_ChargesOnTaxAcc, ObjFunction.GetComboValue(cmbChrgTaxAcc).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_ChargesOnAddTaxAcc, ObjFunction.GetComboValue(cmbChrgAddTaxAcc).ToString());
                dbMSettings.AddAppSettings(AppSettings.P_IsBroker, (chkIsBroker.Checked ? "True" : "False"));
                
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
                    OMMessageBox.Show("Purchase Setting Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    ObjFunction.SetAppSettings();
                    FillControls();
                }
                else
                {
                    OMMessageBox.Show("Purchase Setting Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    EP.SetError(btnSave, DBMSettings.strerrormsg);
                    EP.SetIconAlignment(btnSave, ErrorIconAlignment.MiddleRight);
                }
            }
        }

        public bool Validations()
        {
            EP.SetError(cmbItemNameType, "");
            bool flag = false;
            if (ObjFunction.GetComboValue(cmbItemNameType) <= 0)
            {
                EP.SetError(cmbItemNameType, "Select Item Name Display Type");
                EP.SetIconAlignment(cmbItemNameType, ErrorIconAlignment.MiddleRight);
                cmbItemNameType.Focus();
            }
            else
                flag = true;
            return flag;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            strPsw += (char)e.KeyValue;
            if (strPsw.ToLower() == "logicall")
            {
                strPsw = "";
                tabPurchaseSetting.Visible = false;
                dgSettings.Visible = true;
                dgSettings.Size = tabPurchaseSetting.Size;
                dgSettings.Location = tabPurchaseSetting.Location;
            }
        }

        #endregion

        private void dgSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                dgSettings.Visible = false;
            }
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

        }

        private void chkIsUseLastSaleRateEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkIsBarcodeEnabled_CheckedChanged(object sender, EventArgs e)
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

        private void chkIsShowSalesHistoryEnabled_CheckedChanged_1(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                chkIsUseLastSaleRateEnabled.Enabled = true;
                chkIsStopOnSaleHistoryListEnabled.Enabled = false;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
                chkIsUseLastSaleRateEnabled.Enabled = false;
                chkIsStopOnSaleHistoryListEnabled.Enabled = false;

                chkIsUseLastSaleRateEnabled.Checked = false;
                chkIsStopOnSaleHistoryListEnabled.Checked = false;

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

        private void chkIsAllowsDuplicatesItemsInSameBill_CheckedChanged(object sender, EventArgs e)
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

        private void chkIsUseLastSaleRateEnabled_CheckedChanged_1(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Text = "Yes";
                chkIsStopOnSaleHistoryListEnabled.Enabled = true;
            }
            else
            {
                ((CheckBox)sender).Text = "No";
                chkIsStopOnSaleHistoryListEnabled.Enabled = false;
                chkIsStopOnSaleHistoryListEnabled.Checked = false;

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

        private void chkIsReverseRateCalc_CheckedChanged(object sender, EventArgs e)
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

        private void chkHideRatePopupAutomatically_CheckedChanged_1(object sender, EventArgs e)
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

        private void chkChrg_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;

            if (chk.Name == "chkChrg1")
            {
                cmbCharge1.Enabled = chk.Checked;
                cmbCharge1.SelectedIndex = 0;
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

        private void PurchaseSettingAE_Click(object sender, EventArgs e)
        {
            strPsw = "";
        }

        private void chkBillRoundOff_CheckedChanged(object sender, EventArgs e)
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

        private void chkIsSaleCollectionPrint_CheckedChanged(object sender, EventArgs e)
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

        private void chkChrgTaxAcc_CheckedChanged(object sender, EventArgs e)
        {
            cmbChrgTaxAcc.Visible = chkChrgTaxAcc.Checked;
            cmbChrgAddTaxAcc.Visible = chkChrgTaxAcc.Checked;
            lblAddChrgAcc.Visible = chkChrgTaxAcc.Checked;
        }
    }
}
