using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using OM;
using OMControls;

namespace Yadi.Vouchers
{
    public partial class PurchaseAPMCAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        TVoucherRefDetails tVchRefDtls = new TVoucherRefDetails();
        TVoucherPayTypeDetails tVchPayTypeDetails = new TVoucherPayTypeDetails();
        TVoucherChqCreditDetails tVchChqCredit = new TVoucherChqCreditDetails();
        TStock tStock = new TStock();
        TStockGodown tStockGodown = new TStockGodown();
        TFooterDiscountDetails tFooterDisc = new TFooterDiscountDetails();
        TItemLevelDiscountDetails tItemLevelDisc = new TItemLevelDiscountDetails();

        MLedgerRateSetting mLedgerRate = new MLedgerRateSetting();
        MRateSetting mRatesetting = new MRateSetting();

        DataTable dtTempStock = new DataTable();
        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtUOMTemp = new DataTable();
        DataTable dtVchMainDetails = new DataTable();
        DataTable dtCompRatio = new DataTable();
        DataTable dtVchPrev = new DataTable();
        DataTable dtPayLedger = new DataTable();
        DataTable dtItemLevelDisc = new DataTable();

        Color clrColorRow = Color.FromArgb(255, 224, 192);
        int l_lstGroup1_Index = 0;
        int rowQtyIndex;
        long Companycode, statecode, Ledgerno = 0, BItemNo = 0;
        double PBillTotal = 0;
        long iPayTypeControlUnder = 0;

        bool Spaceflag = true, State = true, pnlBillSize = false;
        public bool RewardDeleteFlag = false, RewardFlag = false, DiscFlag = false;
        public long PrintAsk = 0;
        long ItemNameType = 0, RateTypeNo, PartyNo, PayType, FooterDiscDtlsNo = 0;
        int iItemNameStartIndex = 3, ItemType = 0, MixModeVal = 0;
        string Param1Value = "", Param2Value = "", strSql = "";
        string[] strItemQuery, strItemQuery_last;
        bool updateFlag = false;
        bool EnrFlag = false, CancelFlag = false;
        string strFillcmbPartyName = "";
        DateTime tempDate; long tempPartyNo = 0;
        DataTable dtOrderType; DataTable dtRateSetting;

        DataTable dtPAKKABILL = new DataTable();
        DataTable dt = new DataTable();
        DataTablesCollection dtBillCollect = new DataTablesCollection();
        DateTime dtFrom, dtTo;
        DataTable dtZeroTax;
        long VoucherUserNo;

        bool StopOnQty = false, StopOnRate = false;
        public long RequestSalesNo, ID, VoucherType, No;
        bool isFromTxtBarcode = true;
        long txtPkRateSettingNo = 0;

        DataTable dtTRewardToFrom;
        DataTable dtTRewardDtls;

        PartialPayment partialPay = new PartialPayment();
        PartialPaymentAdjust partialPayAdjust = new PartialPaymentAdjust();
        long subUomno = 0;
        DataTable Subdt = new DataTable();
        double SubQty = 0, SubRate = 0, SubDiscPer = 0, SubDiscRs = 0, SubTax1 = 0, SubTax2 = 0, SubTax3 = 0, SubMktQty = 0;

        public PurchaseAPMCAE()
        {
            InitializeComponent();
        }

        public PurchaseAPMCAE(long ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void PurchaseAE_Load(object sender, EventArgs e)
        {
            try
            {

                if (DBGetVal.KachhaFirm == false)
                {
                    VoucherType = VchType.Purchase;
                }
                else
                {
                    VoucherType = VchType.DPurchase;
                }
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                pnlBank.Visible = false;
                btnNew.Focus();
                FillList();
                ObjFunction.FillCombo(cmbLocation, "SELECT GodownNo, GodownName FROM MGodown WHERE (IsActive = 'true') And GodownNo<>1 ");
                FillRateType();
                dgItemList.Visible = true;
                InitDelTable();
                dtOrderType = ObjFunction.GetDataView("Select OrderTypeNo,OrderTypeName,ColorName From MOrderType").Table;
                RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType));
                ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_ItemNameType));
                StopOnQty = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnQty));
                StopOnRate = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnRate));
                initItemQuery();
                InitControls();
                if (pnlBillSize == false)
                {
                    pnlBill.Visible = false;
                    dgBill.Top = dgBill.Top - (pnlBill.Height + 2);
                    dgBill.Height = dgBill.Height + (37);
                    pnlBillSize = true;
                }
                else
                {
                    pnlBillSize = false;

                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                {
                    strFillcmbPartyName = "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and IsActive='true' order by LedgerName";

                }
                else
                {
                    strFillcmbPartyName = "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') ORDER BY LedgerName ";

                }
                //   dtSearch = ObjFunction.GetDataView("Select max(PkVoucherNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType +
                //       " AND IsVoucherLock='false' ").Table;//Order by VoucherDate, VoucherUserNo desc

                ID = ObjQry.ReturnLong("Select max(PkVoucherNo) from TVoucherEntry Where TVoucherEntry.taxtypeno=1 and  VoucherTypeCode=" + VoucherType + " AND IsVoucherLock='false'", CommonFunctions.ConStr);
                if (ID != 0)
                {
                    // ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    FillField();
                }
                else
                {
                    btnFirst.Enabled = false; btnPrev.Enabled = false; btnNext.Enabled = false; btnLast.Enabled = false;

                    btnUpdate.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSearch.Enabled = false;
                    //btnMixMode.Enabled = false;
                    btnAdvanceSearch.Visible = false;
                    btnBillCancel.Enabled = false;
                    btnPrint.Enabled = false;
                    btnCCSave.Enabled = false;
                    btnCashSave.Enabled = false; btnCreditSave.Enabled = false;
                    //   btnShowDetails.Location = new Point(btnShowDetails.Location.X, btnShowDetails.Location.Y + btnShowDetails.Height + 7);
                    dgBill.Enabled = true;

                }
                DataTable dtSettings = ObjFunction.GetDataView("Select PKSettingNo From MSettings Where SettingTypeNo=5").Table;
                for (int i = 0; i < dtSettings.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(Convert.ToInt32(dtSettings.Rows[i].ItemArray[0].ToString()))) == true)
                        dgBill.Columns[i].Visible = true;
                    else
                        dgBill.Columns[i].Visible = false;
                }
                // txtGrandTotal.Font = new Font("Verdana", 18, FontStyle.Bold);
                //  txtGrandTotal.ForeColor = Color.Black;

                lblGrandTotal.Font = new Font("Verdana", 22, FontStyle.Bold);
                lblGrandTotal.ForeColor = Color.White;
                lblitemcount.Font = new Font("Verdana", 15, FontStyle.Bold);


                label65.Font = new Font("Verdana", 15, FontStyle.Bold);
                lblCreditLimit.Font = new Font("Verdana", 9, FontStyle.Bold);
                lblOutstanding.Font = new Font("Verdana", 9, FontStyle.Bold);


                dgBill.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                dgBill.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                dgBill.RowTemplate.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                dgBill.RowTemplate.Height = 30;
                dgBill.ColumnHeadersHeight = 30;
                if (dgBill.Rows.Count > 0)
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                    {
                        dgBill.Rows[i].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                        dgBill.Rows[i].Height = 30;
                    }
                }
                dgItemList.RowTemplate.Height = 24;
                dgItemList.ColumnHeadersHeight = 24;
                dgItemList.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                dgItemList.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                dgItemList.RowTemplate.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);

                new GridSearch(dgItemList, 1, 2);
                new GridSearch(dgParkingBills, 1);
                FormatPics();
                // DisplayChargANDDisc();============umesh imp


                dtpBillDate.CustomFormat = "dd-MMM-yy"; dtpBillDate.Width = 90;

                dgBill.BackgroundColor = Color.FromArgb(242, 242, 242);// Color.FromArgb(255, 255, 210);

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == false)
                {
                    //plnPrinting.Visible = false;
                    //rbEnglish.Checked = true;
                    dgItemList.Columns[2].Visible = false;
                }
                else
                {

                    //if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_DefaultBillPrint)) == 1)
                    //    rbEnglish.Checked = true;
                    //else
                    //    rbMarathi.Checked = true;

                    //if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 2)
                    //    rbMarathi.Text = "marazI";
                    //else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 3)
                    //    rbMarathi.Text = "ihMdI";
                }

                //plnPrinting.Enabled = true;
                //rbEnglish.Enabled = true;
                //rbMarathi.Enabled = true;

                txtSchemeDisc.Enabled = false;
                txtOtherDisc.Enabled = false;
                txtChrgRupees2.Enabled = false;
                InitItemLeveDiscount();
                ////KeyDownFormat(this.Controls);
                ////  dtMfgCompany = ObjFunction.GetDataView("Select MfgCompNo,IsMfgType From MManufacturerCompany").Table;
                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                //{
                //    if (lstPartyEnglish.Items.Count > 0)
                //        lstPartyLang.SelectedIndex = lstPartyEnglish.SelectedIndex;
                //    //Ledgerno = Convert.ToInt64(lstPartyEnglish.SelectedValue);
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void InitItemLeveDiscount()
        {
            dtItemLevelDisc = ObjFunction.GetDataView("SELECT  MItemDiscount.PkSrNo AS ItemDiscNo, MItemBrandDiscount.PkSrNo AS ItemBrandDiscNo, MItemDiscountDetails.PkSrNo AS ItemDiscDetailsNo, MItemDiscountDetails.ItemNo, MItemDiscountDetails.FkRateSettingNo, MItemDiscountDetails.DiscPercentage,MItemDiscountDetails.MRP " +
                    " FROM MItemDiscountDetails INNER JOIN MItemBrandDiscount ON MItemDiscountDetails.ItemBrandDiscNo = MItemBrandDiscount.PkSrNo INNER JOIN MItemDiscount ON MItemBrandDiscount.ItemDiscNo = MItemDiscount.PkSrNo " +
                    " WHERE (MItemDiscount.PeriodFrom <= '" + dtpBillDate.Value.Date + "') AND (MItemDiscount.PeriodTo >= '" + dtpBillDate.Value.Date + "') AND (MItemDiscount.IsActive = 1) AND " +
                    " (MItemDiscountDetails.IsActive = 'true')").Table;
        }

        private void DisplayChargANDDisc()
        {
            try
            {
                lblChrgOnTax.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Charges1Display));
                // lblChrg1.Text = ObjFunction.GetAppSettings(AppSettings.P_ChargeLabelName).ToString() + ":";
                txtChrgOnTaxAmt.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Charges1Display));


                lblChrg2.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Charges2Display));
                //   lblChrg2.Text = ObjFunction.GetAppSettings(AppSettings.P_Charge2LabelName).ToString() + ":";
                txtChrgRupees2.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Charges2Display));

                lblDisc1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Discount1Display));
                txtDiscount1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Discount1Display));
                txtDiscRupees1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_Discount1Display));
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void hidePics()
        {
            pnlItemName.Visible = false;
            pnlGroup1.Visible = false;

            pnlUOM.Visible = false;
            pnlRate.Visible = false;
            pnlPartial.Visible = false;
            pnlDisplayName.Visible = false;
            pnlParking.Visible = false;
            pnlPartyName.Visible = false;
        }

        private void FormatPics()
        {
            try
            {

                pnlItemName.Width = 1000;//560;
                dgItemList.Width = 980;
                pnlItemName.Height = 350;
                dgItemList.Height = 322;
                pnlItemName.Top = 150;
                pnlItemName.Left = 50;

                pnlGroup1.Top = 150;
                pnlParty.Top = txtParty.Bottom;
                pnlParty.Width = txtParty.Width;
                pnlParty.Height = 400;
                lstPartyLang.Visible = false;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    lstPartyLang.Visible = true;
                    pnlGroup1.Left = 50;//150;
                    pnlGroup1.Width = 1000;//575;

                    lstGroup1.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
                    lstGroup1Lang.Font = ObjFunction.GetLangFont();
                    lstGroup1Lang.Visible = true;

                    dgItemList.RowTemplate.DefaultCellStyle.Font = null;
                    for (int i = 0; i < dgItemList.Columns.Count; i++)
                    {
                        dgItemList.Columns[i].DefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Regular);
                    }
                    dgItemList.Columns[2].DefaultCellStyle.Font = ObjFunction.GetLangFont();

                    lstPartyEnglish.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
                    lstPartyLang.Font = ObjFunction.GetLangFont();
                    pnlParty.Width = txtParty.Width + txtParty.Width + 20;
                    lstPartyEnglish.Top = txtParty.Top - 20;
                    lstPartyLang.Top = lstPartyEnglish.Top;
                    lstPartyLang.Height = pnlParty.Height - 5;
                    lstPartyEnglish.Height = pnlParty.Height - 5;
                    lstPartyEnglish.Width = lstPartyEnglish.Width - 5;
                    lstPartyLang.Left = 380;
                    lstPartyLang.Width = 385;

                }
                else
                {
                    pnlGroup1.Left = 50;
                    pnlGroup1.Width = 400;
                    lstPartyEnglish.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                    lstGroup1.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                    lstGroup1Lang.Visible = false;
                }
                pnlGroup1.Height = 350;// 273;//205;// 220;

                //pnlGroup2.Top = 150;
                //pnlGroup2.Left = 100;
                //pnlGroup2.Width = 300;
                //pnlGroup2.Height = 220;

                //lstGroup1.Width = 390;
                lstUOM.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                pnlUOM.Top = txtBUom.Bottom + 70;
                pnlUOM.Left = txtBUom.Left;
                pnlUOM.Width = txtBUom.Width + 50;
                pnlUOM.Height = 60;
                lstUOM.Height = 55;
                lstUOM.BackColor = Color.White;

                pnlRate.Top = 150;
                pnlRate.Left = 430;
                pnlRate.Width = 120;
                pnlRate.Height = 80;

                pnlSalePurHistory.Width = 720;
                pnlSalePurHistory.Height = 235;
                pnlSalePurHistory.Top = pnlItemName.Height + 88;
                pnlSalePurHistory.Left = pnlItemName.Left;

                // btnMixMode.Left = btnExit.Left;
                // btnMixMode.Top = btnExit.Top + btnExit.Height + 5;
                //btnScheme.Left = btnShortcut.Left;
                //btnScheme.Top = btnShortcut.Bottom + 35;
                //  btnScheme.Location = new Point(btnShowDetails.Location.X + btnShowDetails.Width + 7, btnShowDetails.Location.Y);
                //btnInsSchemeInfo.Left = btnShortcut.Left;
                // btnInsSchemeInfo.Top = btnShortcut.Bottom + 5;
                pnlTransporter.Location = new Point(31, 134);
                pnlMultiPrint.Location = new Point(168, 99);
                pnlPartial.Location = new Point(169, 123);
                pnlSearch.Location = new Point(148, 236);
                pnlPartySearch.Location = new Point(158, 94);
                pnlParking.Location = new Point(140, 174);
                pnlRateType.Location = new Point(205, 206);
                pnlMainParking.Location = new Point(205, 269);
                pnlPartyName.Location = new Point(205, 262);
                pnlStockGodown.Location = new Point(205, 183);
                pnlInvSearch.Location = new Point(711, 128);

                pnlPaymentType.Top = panel1.Top - 100;
                pnlPaymentType.Height = 100;
                pnlPaymentType.Width = 130;
                lstPaymentType.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                // pnlTotalAmt.Location = new Point(790, 238);
                lblMsg.Location = new Point(254, 217);

                //  pnlButtonInfo.Dock = DockStyle.Right;
                //txtBarCode.Font = ObjFunction.GetFont(FontStyle.Regular, 13);

                // txtMRP.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                //  txtBQuantity.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                // txtBRate.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                //  txtStock.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                //  txtBAmount.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                //txtSearchBrand.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void initItemQuery()
        {
            try
            {
                DataTable dtItemQuery = new DataTable();
                dtItemQuery = ObjFunction.GetDataView("SELECT * from MItemNameDisplayType WHERE ItemNameTypeNo = " + ItemNameType).Table;


                if (dtItemQuery.Rows.Count == 1)
                {
                    int qCount = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (dtItemQuery.Rows[0]["Query" + i] != null && dtItemQuery.Rows[0]["Query" + i].ToString().Trim().Length > 0)
                        {
                            qCount++;
                        }
                    }

                    strItemQuery = new string[qCount];
                    strItemQuery_last = new string[qCount];
                    for (int i = 0; i < strItemQuery_last.Length; i++)
                    {
                        strItemQuery_last[i] = "";
                    }
                    qCount = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (dtItemQuery.Rows[0]["Query" + i] != null && dtItemQuery.Rows[0]["Query" + i].ToString().Trim().Length > 0)
                        {
                            strItemQuery[qCount] = dtItemQuery.Rows[0]["Query" + i].ToString().Trim();
                            qCount++;
                        }
                    }

                    iItemNameStartIndex = Convert.ToInt32(dtItemQuery.Rows[0]["StartIndex"].ToString());
                    Param1Value = dtItemQuery.Rows[0]["Param1Value"].ToString();
                    Param2Value = dtItemQuery.Rows[0]["Param2Value"].ToString();
                }
                else
                {
                    OMMessageBox.Show("Please Select Valid Item Name display type in Sales Setting Form ...");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CheckSchemeEnroll()
        {
            try
            {
                EnrFlag = false;
                //EnrFlag = ObjQry.ReturnBoolean("Select IsEnroll from MLedger where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + "", CommonFunctions.ConStr);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        public void InitControls()
        {
            try
            {
                MixModeVal = 0;
                PrintAsk = 0;
                VoucherUserNo = 0;

                FillRateType();
                // SetRateType(RateTypeNo);
                lblCreditLimit.Text = "";
                dtBillCollect = new DataTablesCollection();
                lstPaymentType.SelectedIndex = 0;
                lstPaymentType.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_DefaultPayType));
                txtPaymentType.Text = lstPaymentType.Text;

                dtpBillDate.Value = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD).ToString()).Date;
                dtpBillTime.Format = DateTimePickerFormat.Time;
                lblCancelBll.Text = "";
                lblitemcount.Text = "0.00";
                dgBill.Rows.Clear();//umesh added 

                dgBill.Rows.Add();
                ObjFunction.GetFinancialYear(dtpBillDate.Value, out dtFrom, out dtTo);
                txtInvNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND VoucherDate>='" + dtFrom.Date + "' AND VoucherDate<='" + dtTo.Date + "'", CommonFunctions.ConStr) + 1).ToString();
                lstPartyEnglish.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_DefaultPartyAC));
                txtParty.Text = lstPartyEnglish.Text;
                Ledgerno = Convert.ToInt32(lstPartyEnglish.SelectedValue);

                statecode = ObjQry.ReturnLong("Select stateCode from Mledger  where Ledgerno = " + Ledgerno + " ", CommonFunctions.ConStr);
                // lblState.Text = ObjQry.ReturnString("Select Statename from mstate  where stateCode = " + statecode + " ", CommonFunctions.ConStr);
                if (statecode != 0)
                {
                    if (Companycode == statecode)
                    {
                        State = true;

                    }
                    else
                    {
                        State = false;
                    }
                }
                else
                {
                    MessageBox.Show("please update the party from customer master and save it....... ");
                }
                lblState.Text = ObjQry.ReturnString("Select Statename from mstate  where stateCode = " + statecode + " ", CommonFunctions.ConStr);



                if (Convert.ToInt32(lstPartyEnglish.SelectedValue) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC)))// && (ObjFunction.GetComboValue(cmbPaymentType) == 3))
                {

                }
                else
                    lstPaymentType.Enabled = true;

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnDate)) == true) dtpBillDate.Focus();
                else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnParty)) == true)
                {
                    txtParty.Focus();
                    // txtParty.Text = txtParty.SelectedText;
                }
                //  else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnRateType)) == true) txtRateType.Focus();
                else txtBarCode.Focus();

                dgBill.CurrentCell = dgBill[1, 0];

                txtDiscount1.Text = "0.00";
                txtDiscRupees1.Text = "0.00";
                txtChrgOnTaxAmt.Text = "0.00"; txtChrgRupees2.Text = "0.00"; //txtChrgOnTaxAmt.Text = "0.00"; txtChrgTax.Text = "0.00";
                txtSubTotal.Text = "0.00"; lblBillItem.Text = "0"; lblBilExchangeItem.Text = "0";
                // txtGrandTotal.Text = "0.00"; 
                txtTotalDisc.Text = "0.00"; txtTotalTax.Text = "0.00";
                txtSchemeDisc.Text = "0.00"; txtOtherDisc.Text = "0.00"; txtRoundOff.Text = "0.00";
                //  txtSchemeDisc.Enabled = false; txtOtherDisc.Enabled = false; txtChrgRupees2.Enabled = false;
                BindGridPayType(0);
                // BindPayChequeDetails(0);
                //BindPayCreditDetails(0);
                pnlPartial.Visible = false;
                // btnMixMode.Text = "Mix\r\nMode\r\n(F3)";
                btnBillCancel.Visible = DBGetVal.IsAdmin;
                dtTRewardDtls = ObjFunction.GetDataView("SELECT PkSrNo, RewardNo, SchemeNo, SchemeDetailsNo, SchemeType, DiscPer, DiscAmount, SchemeAmount,0 As Status,SchemeAcheiverNo FROM TRewardDetails WHERE (PkSrNo = 0)").Table;
                dtTRewardToFrom = ObjFunction.GetDataView("SELECT 0 AS TypeNo,PkSrNo, RewardNo, RewardDetailsNo,SchemeDetailsNo, SchemeFromNo, FkStockNo,0 As 'ItemNo' FROM TRewardFrom WHERE (PkSrNo = 0)").Table;
                tempDate = dtpBillDate.Value.Date;

                dtZeroTax = ObjFunction.GetDataView("Select TaxLedgerNo, SalesLedgerNo, Percentage, IsNull(pksrno,0) AS PkSrNo From MItemTaxSetting  Where SalesLedgerNo in(Select LedgerNo  From MLedger Where GroupNo=10) AND Percentage = 0").Table;

                if (Utilities.PasswordAsk.UserID == 0)
                    dgItemList.Columns[14].Visible = DBGetVal.IsAdmin;
                else
                    dgItemList.Columns[14].Visible = Utilities.PasswordAsk.IsAdmin;


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void FillList()
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
            {

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                    ObjFunction.FillList(lstPartyLang, "Select LedgerNo,LedgerLangName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and IsActive='true' order by LedgerName");
                ObjFunction.FillList(lstPartyEnglish, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and IsActive='true' order by LedgerName");

            }
            else
            {

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                    ObjFunction.FillList(lstPartyLang, "SELECT MLedger.LedgerNo, MLedger.LedgerLangName + '-' + ISNULL(MArea.AreaLangName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo  LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryCreditors + ")) ORDER BY LedgerName ");
                ObjFunction.FillList(lstPartyEnglish, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo  LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryCreditors + ")) ORDER BY LedgerName ");

            }
            Companycode = ObjQry.ReturnLong("select Statecode from Mfirm ", CommonFunctions.ConStr);

            ObjFunction.FillList(lstPaymentType, "Select PKPayTypeNo,PayTypeName From MPayType where IsActive='true' and PKPayTypeNo!=1 order by PayTypeName");
            ObjFunction.FillList(lstArea, "Select AreaNo,AreaName From Marea  Where  IsActive='true' order by AreaName");

            ObjFunction.FillList(lstBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + " And IsActive='true' order by LedgerName");
            ObjFunction.FillList(lstBranch, "Select BranchNo,BranchName From MBranch order by BranchName");

        }
        public void FillBrand()
        {
            if (DBGetVal.KachhaFirm == true)
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    ObjFunction.FillList(lstGroup1Lang, "Select ItemGroupNo,LangGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' )and esflag='true' ) and IsActive='true' order by ItemGroupName");
                }
                ObjFunction.FillList(lstGroup1, "Select ItemGroupNo,ItemGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' )and esflag='true' ) and IsActive='true' order by ItemGroupName");
            }
            else
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    ObjFunction.FillList(lstGroup1Lang, "Select ItemGroupNo,LangGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' ) ) and IsActive='true' order by ItemGroupName");
                }
                ObjFunction.FillList(lstGroup1, "Select ItemGroupNo,ItemGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' ) ) and IsActive='true' order by ItemGroupName");
            }

        }
        private void FillField()
        {
            try
            {
                hidePics();

                dtpBillDate.MinDate = Convert.ToDateTime("01-01-1900");
                tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(ID);
                VoucherUserNo = Convert.ToInt32(tVoucherEntry.VoucherUserNo);
                txtInvNo.Text = tVoucherEntry.VoucherUserNo.ToString();
                dtpBillDate.Value = tVoucherEntry.VoucherDate;
                dtpBillTime.Value = tVoucherEntry.VoucherTime;
                lstPaymentType.SelectedValue = tVoucherEntry.PayTypeNo.ToString();
                txtPaymentType.Text = lstPaymentType.Text;
                PayType = tVoucherEntry.PayTypeNo;
                tempDate = dtpBillDate.Value.Date;

                FillRateType(tVoucherEntry.RateTypeNo);
                SetRateType(tVoucherEntry.RateTypeNo);
                txtRateType.Text = lstRateType.Text;
                MixModeVal = tVoucherEntry.MixMode;
                txtSubTotal.Text = "0.00";
                txtTotalDisc.Text = "0.00";
                txtDiscount1.Text = "0.00";
                txtDiscRupees1.Text = "0.00";
                txtTotalDisc.Text = "0.00";
                txtTotalTax.Text = "0.00";
                txtChrgOnTaxAmt.Text = "0.00"; txtChrgRupees2.Text = "0.00";
                //txtGrandTotal.Text = "0.00";
                txtOtherDisc.Text = "0.00";
                txtSchemeDisc.Text = "0.00";
                txtRefNo.Text = tVoucherEntry.Reference;
                txtRemark.Text = tVoucherEntry.Remark;
                DataTable dt = ObjFunction.GetDataView("Select Case When Debit<>0 then Debit Else Credit End,LedgerNo,SrNo From TVoucherDetails Where FKVoucherNo=" + ID + " order by VoucherSrNo").Table;


                double subTot = ObjQry.ReturnDouble("Select sum(Case When(SrNo<>508 AND SrNo<>506)Then Debit else -Credit End) from TVoucherDetails  Where FKVoucherNo=" + ID + " ", CommonFunctions.ConStr);
                txtSubTotal.Text = subTot.ToString();
                long partyNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherEntry  Where pKVoucherNo=" + ID + " ", CommonFunctions.ConStr);
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                {
                    ObjFunction.FillList(lstPartyEnglish, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and (IsActive='true' or LedgerNo = " + partyNo + ") order by LedgerName");
                }
                else
                {
                    ObjFunction.FillList(lstPartyEnglish, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true' or MLedger.LedgerNo = " + partyNo + ") ORDER BY LedgerName ");
                    long areano = ObjQry.ReturnLong("select areano from marea where areano in(select areano from MLedgerDetails where ledgerno=" + partyNo + ")", CommonFunctions.ConStr);
                    lstArea.SelectedValue = areano;
                    txtArea.Text = lstArea.Text;
                }
                lstPartyEnglish.SelectedValue = partyNo;
                tempPartyNo = partyNo;
                txtParty.Text = lstPartyEnglish.Text;
                PartyNo = ObjFunction.GetListValue(lstPartyEnglish);


                statecode = ObjQry.ReturnLong("Select stateCode from Mledger  where Ledgerno = " + PartyNo + " ", CommonFunctions.ConStr);
                lblState.Text = ObjQry.ReturnString("Select Statename from mstate  where stateCode = " + statecode + " ", CommonFunctions.ConStr);
                if (statecode != 0)
                {
                    if (Companycode == statecode)
                    {
                        State = true;

                    }
                    else
                    {
                        State = false;
                    }
                }
                else
                {
                    MessageBox.Show("please update the party from customer master and save it....... ");
                }

                txtChrgOnTaxAmt.Text = "0"; txtChrgRupees2.Text = "0";
                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    if (dt.Rows[i].ItemArray[2].ToString() == Others.Discount2.ToString())
                    {
                        txtSchemeDisc.Text = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString()).ToString("0.00");
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == Others.Discount3.ToString())
                    {
                        txtOtherDisc.Text = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString()).ToString("0.00");
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == Others.Charges1.ToString())
                    {
                        txtChrgOnTaxAmt.Text = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString()).ToString("0.00");
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == Others.Charges2.ToString())
                    {
                        txtChrgRupees2.Text = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString()).ToString("0.00");
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == Others.ChargesTax.ToString())
                    {

                    }
                }

                txtDiscount1.Text = tVoucherEntry.DiscPercent.ToString(Format.DoubleFloating);
                txtDiscRupees1.Text = tVoucherEntry.DiscAmt.ToString(Format.DoubleFloating);
                MixModeVal = tVoucherEntry.MixMode;
                // txtGrandTotal.Text = ((Convert.ToDouble(txtSubTotal.Text) - Convert.ToDouble(txtTotalDisc.Text)) + Convert.ToDouble(txtTotalTax.Text)).ToString("0.00");


                dgBill.Enabled = true;
                FillGrid();
                dgBill.Enabled = false;

                DataTable dtPartial = ObjFunction.GetDataView("Select Credit From TVoucherDetails Where FKVoucherNo=" + ID + " AND VoucherSrNo in (2,3) AND LedgerNo in(1,3) ").Table;
                if (dtPartial.Rows.Count == 2)
                {

                    txtTotalAmt.Text = "0.00";
                }
                string StateName = lblState.Text = ObjQry.ReturnString("select StateName from Mstate where Statecode in (Select stateCode from Mledger  where Ledgerno = " + partyNo + ")", CommonFunctions.ConStr);
                long LastBillNo = ObjQry.ReturnLong("Select IsNull(Max(PkVoucherNo),0) From TVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND  PkVoucherNo<" + ID + "", CommonFunctions.ConStr);
                //           dt = ObjFunction.GetDataView("SELECT IsNull(SUM(TStock.Quantity),0) AS Quantity, IsNull(SUM(TStock.Amount),0) AS Amount FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                //" TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + LastBillNo + ")").Table;

                //           if (dt.Rows.Count > 0)
                //           {
                //               lblLastAmt.Text = "Amount : ";
                //               lblLastBillAmt.Text = ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where PKVoucherNo=" + LastBillNo + "", CommonFunctions.ConStr).ToString();

                //               lblLastBillQty.Text = "Qty: " + dt.Rows[0].ItemArray[0].ToString();
                //               lblLastPayment.Text = "Payment: " + ObjQry.ReturnString("SELECT MPayType.PayTypeName FROM MPayType INNER JOIN TVoucherEntry ON MPayType.PKPayTypeNo = TVoucherEntry.PayTypeNo WHERE (TVoucherEntry.PkVoucherNo = " + LastBillNo + ")", CommonFunctions.ConStr);
                //           }
                dtVchMainDetails = ObjFunction.GetDataView("Select * From TVoucherDetails Where FKVoucherNo=" + ID + "").Table;

                lblCancelBll.Text = "";
                btnUpdate.Visible = true; btnBillCancel.Visible = true; lblCancelBll.Text = "";
                if (tVoucherEntry.IsVoucherLock == true)
                {
                    btnUpdate.Visible = !tVoucherEntry.IsVoucherLock;
                    lblCancelBll.Text = "Bill Lock";
                }
                else if (tVoucherEntry.IsCancel == true)
                {
                    btnUpdate.Visible = !tVoucherEntry.IsCancel;
                    btnBillCancel.Visible = !tVoucherEntry.IsCancel;
                    lblCancelBll.Text = "Bill Cancel";
                    //BillFlag = true;
                }
                else if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    //BillFlag = false;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsCreditBillUpdate)) == true) btnUpdate.Visible = true;
                }
                else if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + " and TR.TypeOfRef in(6))", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    //BillFlag = false;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsCreditBillUpdate)) == true) btnUpdate.Visible = true;
                }
                else if (ObjQry.ReturnInteger("SELECT  COUNT(*) FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                    " WHERE     (TVoucherEntry.Reference = " + tVoucherEntry.VoucherUserNo + ") AND (TVoucherEntry.VoucherTypeCode = " + VchType.RejectionIn + ") AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ")", CommonFunctions.ConStr) > 0)
                {
                    btnBillCancel.Visible = false;
                    //BillFlag = false;
                }
                else
                {
                    btnUpdate.Visible = true;
                    btnBillCancel.Visible = true;
                    btnUpdate.Enabled = true;
                    btnBillCancel.Enabled = true;
                    //BillFlag = false;
                }

                //pnlCollectionDetails.Visible = false;
                dgCollectionDetails.DataSource = null;
                lblTotalCollection.Text = "0.00";
                lblOw.Text = "0.00";
                //btnMixMode.Text = "Mix\r\nMode\r\n(F3)";
                //dgCollectionDetails.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 210);
                //if ((ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1) && PayType == 3)
                //{
                //    btnMixMode.Visible = false;
                //}
                //else if (PayType == 3 && tVoucherEntry.IsVoucherLock == false && tVoucherEntry.IsCancel == false)
                //{
                //    btnMixMode.Visible = true;
                //    btnMixMode.Text = "Receipt\r\nMode";
                //    // DisplayCollectionDetails();
                //}
                //else
                //    btnMixMode.Visible = false;


                //Transport

                cmbTransporter.SelectedValue = (tVoucherEntry.TransporterCode == 0) ? "0" : tVoucherEntry.TransporterCode.ToString();
                cmbTransPayType.SelectedValue = (tVoucherEntry.TransPayType == 0) ? "0" : tVoucherEntry.TransPayType.ToString();
                txtLRNo.Text = tVoucherEntry.LRNo;
                cmbTransMode.SelectedValue = (tVoucherEntry.TransportMode == 0) ? "0" : tVoucherEntry.TransportMode.ToString();
                txtTransNoOfItems.Text = (tVoucherEntry.TransNoOfItems == 0) ? "0" : tVoucherEntry.TransNoOfItems.ToString();

                // FillTodaysSalesDetails();
                //End


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void FillGrid()
        {
            try
            {
                dgBill.Enabled = true;
                dgBill.Rows.Clear();
                string sqlQuery = "";
                // State = true;
                if (State == true)
                {
                    sqlQuery = "SELECT 0 AS Sr,(SELECT case when esflag='False' then ItemName else ItemName+'*' end as ItemName FROM dbo.MStockItems_V(NULL, TStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TStock.Quantity, MUOM.UOMName, " +
                            " TStock.Rate,Cast(MRateSetting.MRP as numeric(18,2)) as MRP, TStock.NetRate, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, " +
                            " TStock.NetAmount AS NetAmt, TStock.Amount,TStock.Freight as Charges, MItemMaster.Barcode, TStock.PkStockTrnNo," +/* MStockBarcode.PkStockBarcodeNo,*/ " TVoucherDetails.PkVoucherTrnNo, " +
                            " MItemMaster.ItemNo, MUOM.UOMNo,  TStock.FkRateSettingNo,MRateSetting.StockConversion, TStock.Quantity * MRateSetting.StockConversion AS ActualQty, MRateSetting.MKTQty AS MKTQuantity," +
                            /*First Tax*/
                            " tStock.SGSTPercentage,tStock.SGSTAmount,MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.PkSrNo, " +
                            "IsNULL((SELECT PkVoucherTrnNo FROM TVoucherDetails AS SV WHERE SV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.SalesLedgerNo) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS SalesVchNo, " +
                            " IsNull((SELECT PkVoucherTrnNo FROM TVoucherDetails AS TXV WHERE TXV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.TaxLedgerNo)and srno in(516,518) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS TaxVchNo," +
                            /*Second Tax*/

                            " TStock.CGSTPercentage  as TaxPercentage2 ,TStock.CGSTAmount as TaxAmount2  ,MItemTaxInfo_2.TaxLedgerNo as TaxLedgerNo2 ,MItemTaxInfo_2.SalesLedgerNo as SalesLedgerNo2 ,TStock.FkItemTaxInfo2 as FKItemTaxSettingNo2, " +
                            " IsNULL((SELECT PkVoucherTrnNo FROM TVoucherDetails AS SV WHERE SV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo_2.SalesLedgerNo) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS SalesVchNo2, " +
                            " IsNull((SELECT PkVoucherTrnNo FROM TVoucherDetails AS TXV WHERE TXV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo_2.TaxLedgerNo) and srno in(517,519) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS TaxVchNo2, " +
                            /*Third Tax*/
                            " TStock.CessPercentage  as TaxPercentage3 ,TStock.CessAmount as TaxAmount3, MItemTaxInfo_3.TaxLedgerNo as TaxLedgerNo3 ,MItemTaxInfo_3.SalesLedgerNo as SalesLedgerNo3 ,TStock.FKAddItemTaxSettingNo as FKItemTaxSettingNo3, " +
                             " IsNULL((SELECT PkVoucherTrnNo FROM TVoucherDetails AS SV WHERE SV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo_3.SalesLedgerNo) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS SalesVchNo3, " +
                            " IsNull((SELECT PkVoucherTrnNo FROM TVoucherDetails AS TXV WHERE TXV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo_3.TaxLedgerNo) and srno in(517,519) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS TaxVchNo3, " +

                            //    /*Scheme start*/
                            //  " MItemMaster.CompanyNo,  (ISNULL ((SELECT SchemeDetailsNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL)) AS 'SchemeDetailsNo', " +
                            //    " ISNULL ((SELECT SchemeFromNo FROM TRewardFrom  WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeFromNo',  ISNULL ((SELECT SchemeToNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeToNo',ISNULL ((SELECT PkSrNo FROM TRewardFrom WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardFromNo'," +
                            //    " ISNULL ((SELECT PkSrNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardToNo',IsNull((SELECT MItemDiscountDetails.PkSrNo FROM MItemDiscountDetails INNER JOIN MItemBrandDiscount ON MItemDiscountDetails.ItemBrandDiscNo = MItemBrandDiscount.PkSrNo " +
                            //    " INNER JOIN MItemDiscount ON MItemDiscount.PKSrNo = MItemBrandDiscount.ItemDiscNo WHERE (MItemDiscountDetails.ItemNo = TStock.ItemNo) AND (MItemDiscountDetails.FkRateSettingNo = TStock.FKRateSettingNo) AND MItemDiscount.IsActive=1 AND MItemDiscount.PeriodFrom >= '" + dtpBillDate.Value.ToString(Format.DDMMMYYYY) + "' AND MItemDiscount.PeriodTo <= '" + dtpBillDate.Value.ToString(Format.DDMMMYYYY) + "'),NULL) AS 'ItemLevelDiscNo', " +
                            //    " ISNULL ((SELECT PkSrNo FROM TItemLevelDiscountDetails WHERE (FKStockTrnNo  = TStock.PkStockTrnNo)), NULL) AS 'FKItemLevelDiscNo', " +
                            ///*Scheme end*/
                            // " TStock.companyno ,'' as DisplayName, 0 AS IsRateChange, " +/*ISNULL ((SELECT SchemeDetailsNo  FROM TRewardFrom WHERE (FkStockNo = TStock.PkStockTrnNo)), " + ,TStock.MfgCompNo*/
                            //  " Cast(IsNull((TStock.Rate + ((TStock.Rate*IsNull(MItemMaster.HigherVariation,0))/100)),0) as Numeric(18,2)), " +
                            //  " Cast(IsNull((TStock.Rate - ((TStock.Rate*IsNull(MItemMaster.LowerVariation,0))/100)),0) as Numeric(18,2)) , " +
                            " TStock.Remarks, TStock.companyno , TStock.Rate as TempRate,IsNull(TOtherStockDetails.FKOtherVoucherNo,0) As PONo, IsNull(TOtherStockDetails.PkSrNo,0) As PKStockOtherDetailsNo,IsNull(TOtherStockDetails.FKOtherStockTrnNo,0) AS FKOtherStockTrnNo ,0 as FreeQty ,'' as FreeUomName,0 as BarcodePrinting,0 as FreeUomNo,0 as LandedRate ,MItemMaster.ESFlag," +
                            " Cast(MRateSetting.MRP as numeric(18,2)) as TempMRP " +
                             " ,TStock.CessValue ,TStock.PackagingCharges,TStock.PackagingChargesAmt ,TStock.OtherCharges as Dekharek,TStock.NoOfBag  , MItemMaster.CessValue as CessPer ,MItemMaster.Dhekhrek as DhekhrekPer,Tstock.ContainerCharges,Tstock.ContainerChargesAmt " +

                     " FROM MItemMaster INNER JOIN TStock ON MItemMaster.ItemNo = TStock.ItemNo INNER JOIN TVoucherDetails ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo INNER JOIN " +
                            " MItemTaxInfo ON TStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo left join  MItemTaxInfo  as MItemTaxInfo_2 ON TStock.FkItemTaxInfo2 = MItemTaxInfo_2.PkSrNo " + /*INNER JOIN MStockBarcode ON TStock.FkStockBarCodeNo = MStockBarcode.PkStockBarcodeNo */
                            " INNER JOIN MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                            " LEFT OUTER JOIN TOtherStockDetails ON TStock.PkStockTrnNo = TOtherStockDetails.FKStockTrnNo " +
                            "left join    TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo LEFT OUTER JOIN  MItemTaxInfo  as MItemTaxInfo_3 ON TStock.FKAddItemTaxSettingNo = MItemTaxInfo_3.PkSrNo WHERE  TVoucherEntry.taxtypeno=1 and     (TVoucherDetails.FkVoucherNo = " + ID + ") ORDER BY TStock.PkStockTrnNo";

                }
                else
                {
                    //          sqlQuery = "SELECT 0 AS Sr,(SELECT case when esflag='False' then ItemName else ItemName+'*' end as ItemName FROM dbo.MStockItems_V(NULL, TStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TStock.Quantity, MUOM.UOMName, " +
                    //  " TStock.Rate, TStock.NetRate, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, " +
                    //  " TStock.NetAmount AS NetAmt, TStock.Amount, MStockBarcode.Barcode, TStock.PkStockTrnNo, MStockBarcode.PkStockBarcodeNo, TVoucherDetails.PkVoucherTrnNo, " +
                    //  " mItemMaster.ItemNo, MUOM.UOMNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo, TStock.FkRateSettingNo, MItemTaxInfo.PkSrNo, " +
                    //  " MRateSetting.StockConversion, TStock.Quantity * MRateSetting.StockConversion AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, TStock.IGSTPercentage, " +
                    //  " TStock.IGSTAmount, " +
                    //                         "IsNULL((SELECT PkVoucherTrnNo FROM TVoucherDetails AS SV WHERE SV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.SalesLedgerNo) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS SalesVchNo, " +
                    //  " IsNull((SELECT PkVoucherTrnNo FROM TVoucherDetails AS TXV WHERE TXV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.TaxLedgerNo)and srno in(516,518) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS TaxVchNo," +


                    //" mItemMaster.CompanyNo, ISNULL ((SELECT SchemeDetailsNo  FROM TRewardFrom WHERE (FkStockNo = TStock.PkStockTrnNo)), (ISNULL ((SELECT SchemeDetailsNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL))) AS 'SchemeDetailsNo', ISNULL " +
                    //  " ((SELECT SchemeFromNo FROM TRewardFrom  WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeFromNo',  ISNULL ((SELECT SchemeToNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeToNo',ISNULL ((SELECT PkSrNo FROM TRewardFrom WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardFromNo'," +
                    //  " ISNULL ((SELECT PkSrNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardToNo',IsNull((SELECT MItemDiscountDetails.PkSrNo FROM MItemDiscountDetails INNER JOIN MItemBrandDiscount ON MItemDiscountDetails.ItemBrandDiscNo = MItemBrandDiscount.PkSrNo " +
                    //  " INNER JOIN MItemDiscount ON MItemDiscount.PKSrNo = MItemBrandDiscount.ItemDiscNo WHERE (MItemDiscountDetails.ItemNo = TStock.ItemNo) AND (MItemDiscountDetails.FkRateSettingNo = TStock.FKRateSettingNo) AND MItemDiscount.IsActive=1 AND MItemDiscount.PeriodFrom >= '" + dtpBillDate.Value.ToString(Format.DDMMMYYYY) + "' AND MItemDiscount.PeriodTo <= '" + dtpBillDate.Value.ToString(Format.DDMMMYYYY) + "'),NULL) AS 'ItemLevelDiscNo',ISNULL ((SELECT PkSrNo FROM TItemLevelDiscountDetails WHERE (FKStockTrnNo  = TStock.PkStockTrnNo)), NULL) AS 'FKItemLevelDiscNo',TStock.MfgCompNo " +
                    //  ",Cast(MRateSetting.MRP as numeric(18,2)) as MRP,0 AS IsRateChange " +
                    //  " ,Cast(IsNull((TStock.Rate + ((TStock.Rate*IsNull(MStockItems.HigherVariation,0))/100)),0) as Numeric(18,2)),Cast(IsNull((TStock.Rate - ((TStock.Rate*IsNull(MStockItems.LowerVariation,0))/100)),0) as Numeric(18,2)) ,TStock.Rate, " +
                    //  " IsNull(TOtherStockDetails.FKOtherVoucherNo,0) As PONo, IsNull(TOtherStockDetails.PkSrNo,0) As PKStockOtherDetailsNo,IsNull(TOtherStockDetails.FKOtherStockTrnNo,0) AS FKOtherStockTrnNo " +

                    //  " ,0  as TaxPercentage2 ,0 as TaxAmount2  , 0 as SalesVchNo2,0 AS TaxVchNo,0 as TaxLedgerNo2,0 as SalesLedgerNo2 " +
                    //        ",TStock.CessPercentage  as TaxPercentage3 ,TStock.CessAmount as TaxAmount3,MItemTaxInfo_3.TaxLedgerNo as TaxLedgerNo3, TStock.FKAddItemTaxSettingNo as FKItemTaxSettingNo3,MItemTaxInfo_3.SalesLedgerNo as SalesLedgerNo3,MItemMaster.ESFlag  " +


                    //   " FROM MStockItems INNER JOIN TStock ON mItemMaster.ItemNo = TStock.ItemNo INNER JOIN TVoucherDetails ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo INNER JOIN " +
                    //                  " MItemTaxInfo ON TStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo left join  MItemTaxInfo  as MItemTaxInfo_2 ON TStock.FkItemTaxInfo2 = MItemTaxInfo_2.PkSrNo INNER JOIN MStockBarcode ON TStock.FkStockBarCodeNo = MStockBarcode.PkStockBarcodeNo INNER JOIN MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                    //                  " LEFT OUTER JOIN TOtherStockDetails ON TStock.PkStockTrnNo = TOtherStockDetails.FKStockTrnNo " +
                    //                  "left join  MItemTaxInfo  as MItemTaxInfo_3 ON TStock.FKAddItemTaxSettingNo = MItemTaxInfo_3.PkSrNo WHERE      (TVoucherDetails.FkVoucherNo = " + ID + ") ORDER BY TStock.PkStockTrnNo";

                    //     
                    sqlQuery = " SELECT 0 AS Sr,(SELECT case when esflag='False' then ItemName else ItemName+'*' end as ItemName FROM dbo.MStockItems_V(NULL, TStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, " +
                            " TStock.Quantity, MUOM.UOMName,  TStock.Rate, Cast(MRateSetting.MRP as numeric(18,2)) as MRP, TStock.NetRate,TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2,  TStock.NetAmount AS NetAmt, " +
                            " TStock.Amount,TStock.Freight as Charges, MItemMaster.Barcode, TStock.PkStockTrnNo, TVoucherDetails.PkVoucherTrnNo,  mItemMaster.ItemNo, MUOM.UOMNo, TStock.FkRateSettingNo, MRateSetting.StockConversion, " +
                            " TStock.Quantity * MRateSetting.StockConversion AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, TStock.IGSTPercentage  as SGSTPercentage ,TStock.IGSTAmount as SGSTAmount  , " +
                            " MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo, MItemTaxInfo.PkSrNo, " +
                           " IsNULL((SELECT PkVoucherTrnNo FROM TVoucherDetails AS SV WHERE SV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.SalesLedgerNo) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS SalesVchNo, " +
                                     " IsNull((SELECT PkVoucherTrnNo FROM TVoucherDetails AS TXV WHERE TXV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.TaxLedgerNo)and srno in(516,518) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS TaxVchNo, " +
                            " 0  as TaxPercentage2 ,0 as TaxAmount2 ,0 as TaxLedgerNo2,0 as SalesLedgerNo2 ,0 as FKItemTaxSettingNo2 ,0 as SalesVchNo2,0 AS TaxVchNo2 " +
                           " ,TStock.CessPercentage  as TaxPercentage3 ,TStock.CessAmount as TaxAmount3,MItemTaxInfo_3.TaxLedgerNo as TaxLedgerNo3,MItemTaxInfo_3.SalesLedgerNo as SalesLedgerNo3 , TStock.FKAddItemTaxSettingNo as FKItemTaxSettingNo3, " +
                           " 0 AS SalesVchNo3, 0 AS TaxVchNo3,TStock.Remarks, TStock.companyno , TStock.Rate as TempRate,IsNull(TOtherStockDetails.FKOtherVoucherNo,0) As PONo, IsNull(TOtherStockDetails.PkSrNo,0) As PKStockOtherDetailsNo, " +
                           " IsNull(TOtherStockDetails.FKOtherStockTrnNo,0) AS FKOtherStockTrnNo ,0 as FreeQty ,'' as FreeUomName,0 as BarcodePrinting,0 as FreeUomNo,0 as LandedRate ,MItemMaster.ESFlag, Cast(MRateSetting.MRP as numeric(18,2)) as TempMRP   " +
                           " ,TStock.CessValue , TStock.PackagingCharges,TStock.PackagingChargesAmt ,TStock.OtherCharges as Dekharek,TStock.NoOfBag  , MItemMaster.CessValue as CessPer ,MItemMaster.Dhekhrek as DhekhrekPer ,Tstock.ContainerCharges,Tstock.ContainerChargesAmt" +

                           " FROM MItemMaster INNER JOIN TStock ON MItemMaster.ItemNo = TStock.ItemNo INNER JOIN TVoucherDetails ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo " +
                           " INNER JOIN  MItemTaxInfo ON TStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo left join  MItemTaxInfo  as MItemTaxInfo_2 ON TStock.FkItemTaxInfo2 = MItemTaxInfo_2.PkSrNo " +
                            " INNER JOIN MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                            " LEFT OUTER JOIN TOtherStockDetails ON TStock.PkStockTrnNo = TOtherStockDetails.FKStockTrnNo left join    TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo LEFT OUTER JOIN MItemTaxInfo  as MItemTaxInfo_3 ON TStock.FKAddItemTaxSettingNo = MItemTaxInfo_3.PkSrNo " +
                           " WHERE  TVoucherEntry.taxtypeno=1 and     (TVoucherDetails.FkVoucherNo = " + ID + ") ORDER BY TStock.PkStockTrnNo";
                }
                if (DBGetVal.KachhaFirm == false)
                    sqlQuery = sqlQuery.Replace("SELECT 0 AS Sr,(SELECT case when esflag='False' then ItemName+'*' else ItemName end as ItemName", "SELECT 0 AS Sr,(SELECT ItemName");

                dt = ObjFunction.GetDataView(sqlQuery).Table;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBill.Rows.Add();
                    for (int i = 0; i < dgBill.Columns.Count; i++)
                    {
                        dgBill.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                    }
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_AfterSaveNotDeleteItem)) == true)
                    //    dgBill.Rows[j].ReadOnly = true;
                }
                dtBillCollect = new DataTablesCollection();
                lblBillItem.Text = "0"; lblBilExchangeItem.Text = "0";
                string strStkNo = "";
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) >= 0)
                        lblBillItem.Text = (Convert.ToDouble(lblBillItem.Text) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString();
                    else
                        lblBilExchangeItem.Text = (Convert.ToInt64(lblBilExchangeItem.Text) + Math.Abs(Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value))).ToString();
                    if (i == 0) strStkNo = dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString();
                    else strStkNo += "," + dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString();
                    // dtBillCollect.Add(ObjFunction.GetDataView("Exec GetStockGodownDetails " + dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString() + "," + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value + "").Table);
                }
                // FillGodownDetails(strStkNo);
                dgBill.Rows.Add();
                lblitemcount.Text = (dgBill.Rows.Count - 1).ToString("0.00");
                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                CalculateTotal();
                dtTempStock = dt;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            dgBill.Enabled = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            State = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            btnCancel.Enabled = true;
            btnCashSave.Visible = true;
            btnCreditSave.Visible = false;
            btnBillCancel.Enabled = false;
            txtRefNo.Text = "";
            // dtpBillDate.Focus();
            ID = 0;
            if (pnlBillSize == true)
            {
                pnlBill.Visible = true;
                dgBill.Top = dgBill.Top + (pnlBill.Height + 2);
                dgBill.Height = dgBill.Height - 78;
                pnlBillSize = false;
            }
            else
            {
                pnlBillSize = true;

            }
            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsManualBillNo)) == true)
            //{
            //    txtInvNo.Enabled = true;
            //    txtInvNo.ReadOnly = false;
            //}
            dgBill.Enabled = true;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                ObjFunction.FillList(lstPartyEnglish, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and IsActive='true' order by LedgerName");
            else
                ObjFunction.FillList(lstPartyEnglish, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryCreditors + ")) AND (MLedger.IsActive = 'true') ORDER BY LedgerName ");

            InitControls();
            FillList();
            FillBrand();
            lstPaymentType.SelectedIndex = 2;
            txtPaymentType.Text = lstPaymentType.Text;
            txtPaymentType.Enabled = false;
            cmbLocation.SelectedIndex = 0;
            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            //{
            //    if (lstPartyEnglish.Items.Count > 0)
            //        lstPartyLang.SelectedIndex = lstPartyEnglish.SelectedIndex;
            //    //Ledgerno = Convert.ToInt64(lstPartyEnglish.SelectedValue);
            //}
        }

        private void dtpBillDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtpBillTime.Focus();
            }
        }

        private void dtpBillTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtRefNo.Focus();
            }
        }

        private void txtParty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtParty.Text == "")
                {
                    pnlParty.Visible = true;
                    lstPartyEnglish.Focus();
                    lstPartyEnglish.SelectedIndex = 0;
                }
                else
                {


                    pnlParty.Visible = false;
                    ObjFunction.GetFinancialYear(dtpBillDate.Value, out dtFrom, out dtTo);
                    if (ObjQry.ReturnLong(" Select Count(*) FROM  TVoucherEntry  where TVoucherEntry.Reference ='" + txtRefNo.Text.Trim() + "' and TVoucherEntry.VoucherTypeCode=" + VoucherType + " " +
                                         " and TVoucherEntry.LedgerNo=" + Ledgerno + " " +
                                         " AND TVoucherEntry.VoucherDate>='" + dtFrom.Date + "' AND TVoucherEntry.VoucherDate<='" + dtTo.Date + "' " +
                                         " and TVoucherEntry.PkVoucherNo!=" + ID + " AND TVoucherEntry.BrokerNo=0 AND TVoucherEntry.IsCancel='false' ", CommonFunctions.ConStr) > 0)//TVoucherEntry.VoucherDate='" + Convert.ToDateTime(dtpBillDate.Text).ToString("dd-MMM-yyyy") + "' and 
                    {

                        OMMessageBox.Show("This Invoice is already exist ....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtRefNo.Focus();
                    }
                    else
                    {
                        //cmbLocation.Focus();
                        txtItemName.Focus();
                    }

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtParty.Text = "";
            }
            else
            {
                //e.KeyChar = Convert.ToChar(0);
                txtParty.Text = "";
                pnlParty.Visible = true;
                lstPartyEnglish.Focus();
            }

        }

        private void lstPartEnglish_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtParty.Text = lstPartyEnglish.Text;
                    Ledgerno = Convert.ToInt64(lstPartyEnglish.SelectedValue);
                    pnlParty.Visible = false;


                    statecode = ObjQry.ReturnLong("Select stateCode from Mledger  where Ledgerno = " + Ledgerno + " ", CommonFunctions.ConStr);
                    lblState.Text = ObjQry.ReturnString("Select Statename from mstate  where stateCode = " + statecode + " ", CommonFunctions.ConStr);
                    if (statecode != 0)
                    {
                        if (Companycode == statecode)
                        {
                            State = true;

                        }
                        else
                        {
                            State = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("please update the party from customer master and save it....... ");
                    }
                    ObjFunction.GetFinancialYear(dtpBillDate.Value, out dtFrom, out dtTo);
                    if (ObjQry.ReturnLong(" Select Count(*) FROM  TVoucherEntry  where TVoucherEntry.Reference ='" + txtRefNo.Text.Trim() + "' and TVoucherEntry.VoucherTypeCode=" + VoucherType + " " +
                                         " and TVoucherEntry.LedgerNo=" + Ledgerno + " " +
                                         " AND TVoucherEntry.VoucherDate>='" + dtFrom.Date + "' AND TVoucherEntry.VoucherDate<='" + dtTo.Date + "' " +
                                         " and TVoucherEntry.PkVoucherNo!=" + ID + " AND TVoucherEntry.BrokerNo=0 AND TVoucherEntry.IsCancel='false' ", CommonFunctions.ConStr) > 0)//TVoucherEntry.VoucherDate='" + Convert.ToDateTime(dtpBillDate.Text).ToString("dd-MMM-yyyy") + "' and 
                    {

                        OMMessageBox.Show("This Invoice is already exist ....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtParty.Focus();
                    }
                    else
                    {
                        txtItemName.Focus();
                    }

                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtParty.Focus();
                pnlParty.Visible = false;

            }
        }

        private void txtRateType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtRateType.Text == "")
                {
                    pnlRateTypeH.Visible = true;
                    lstRateType.Focus();
                    lstRateType.SelectedIndex = 0;
                }
                else
                {
                    pnlRateTypeH.Visible = false;

                    txtBarCode.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtRateType.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstRateType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtRateType.Text = lstRateType.Text;

                    txtRateType.Focus();
                    pnlRateTypeH.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtRateType.Focus();
                pnlRateTypeH.Visible = false;

            }
        }

        private void txtPaymentType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPaymentType.Text == "")
                {
                    pnlPaymentType.Visible = true;
                    lstPaymentType.Focus();
                    lstPaymentType.SelectedIndex = 0;
                }
                else
                {
                    pnlPaymentType.Visible = false;

                    txtRemark.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtPaymentType.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void dgPayChqDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgPayChqDetails.CurrentCell.ColumnIndex == 1)
                    {
                        dtpChqDate.Visible = true;
                        dtpChqDate.BringToFront();
                        dtpChqDate.Focus();
                    }
                    else if (dgPayChqDetails.CurrentCell.ColumnIndex == 2)
                    {

                        pnlBank.Visible = true;
                        pnlBank.BringToFront();
                        lstBank.Focus();
                    }
                    else if (dgPayChqDetails.CurrentCell.ColumnIndex == 3)
                    {
                        pnlBranch.Visible = true;
                        pnlBranch.BringToFront();
                        lstBranch.Focus();
                    }
                    else if (dgPayChqDetails.CurrentCell.ColumnIndex == 4)
                    {
                        double TotAmt = 0;
                        for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                        {
                            if (dgPayChqDetails.Rows[0].Cells[4].Value != null)
                            {
                                TotAmt = TotAmt + Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                            }
                        }
                        txtTotalAmt.Text = TotAmt.ToString();
                        btnOk.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    double Amt = 0;
                    if (dgPayChqDetails.Rows[0].Cells[0].EditedFormattedValue.ToString().Trim() != "")
                    {
                        if (dgPayChqDetails.Rows[0].Cells[1].FormattedValue.ToString().Trim() == "" || dgPayChqDetails.Rows[0].Cells[2].FormattedValue.ToString().Trim() == "")
                        {
                            OMMessageBox.Show("Please Fill Cheque Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            return;
                        }
                        for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                        {
                            Amt += (dgPayChqDetails.Rows[i].Cells[4].Value == null) ? 0 : Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                        }

                        if (Convert.ToDouble(dgPayType.Rows[3].Cells[2].Value) != Amt)
                        {
                            OMMessageBox.Show("Please enter Cheque amount and Cheque Details amount are not same...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            dgPayChqDetails.Focus();
                        }
                        else
                        {
                            if (ObjFunction.GetListValue(lstPaymentType) == 1)
                            {
                                pnlPartial.Size = new Size(305, 214);
                                pnlPartial.Location = new Point(200, 123);
                                dgPayType.CurrentCell = dgPayType[2, 3];
                                dgPayType.Focus();
                            }
                            else
                            {
                                btnSave.Enabled = true;
                                btnSave.Focus();
                                pnlPartial.Visible = false;
                            }
                            //btnOk.Focus();
                        }

                    }
                    else
                    {
                        if (ObjFunction.GetListValue(lstPaymentType) == 1)
                        {
                            pnlPartial.Size = new Size(305, 214);
                            pnlPartial.Location = new Point(200, 123);
                            dgPayType.CurrentCell = dgPayType[2, 3];
                            dgPayType.Focus();
                        }
                        else
                        {
                            //btnSave.Enabled = true;
                            //btnSave.Focus();
                            txtPaymentType.Focus();
                            pnlPartial.Visible = false;
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    //if (dgPayChqDetails.CurrentCell.RowIndex != dgPayChqDetails.Rows.Count - 1)
                    //{
                    if (Convert.ToInt64(dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[5].Value) != 0)
                    {
                        DeleteDtls(5, Convert.ToInt64(dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[5].Value));
                    }
                    dgPayChqDetails.Rows.RemoveAt(dgPayChqDetails.CurrentCell.RowIndex);
                    if (dgPayChqDetails.Rows.Count == 0)
                        dgPayChqDetails.Rows.Add();
                    dgPayChqDetails.CurrentCell = dgPayChqDetails[0, dgPayChqDetails.Rows.Count - 1];
                    // }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpChqDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[1].Value = dtpChqDate.SelectionStart.ToString("dd-MMM-yy");
                    dtpChqDate.Visible = false;
                    if (dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value == null)
                    {

                        pnlBank.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 150 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                        pnlBank.Visible = true;
                        pnlBank.BringToFront();
                        lstBank.Focus();
                    }
                    else
                    {
                        dgPayChqDetails.Focus();
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[2, dgPayChqDetails.CurrentCell.RowIndex];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void dgPayChqDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[1].Value == null)
                    {
                        if (e.RowIndex == 0) dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value = dgPayType.Rows[3].Cells[2].Value;
                        dtpChqDate.Location = new Point(dgPayChqDetails.CurrentCell.ContentBounds.X + 72 + dgPayChqDetails.Location.X, dgPayChqDetails.CurrentCell.ContentBounds.Y + 40);
                        dtpChqDate.Visible = true;
                        dtpChqDate.BringToFront();
                        dtpChqDate.Focus();
                    }
                    else
                    {
                        dgPayChqDetails.Focus();
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgPayChqDetails.CurrentCell.RowIndex, 1, dgPayChqDetails });
                    }
                }
                if (e.ColumnIndex == 4)
                {
                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value != null)
                    {
                        dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "";
                        if (ObjFunction.CheckValidAmount(dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value.ToString()) == false)
                        {
                            dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "Please Enter Valid Amount";
                        }
                        else
                        {
                            double TotAmt = 0;
                            dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "";
                            if (e.RowIndex == dgPayChqDetails.Rows.Count - 1 && dgPayChqDetails.Rows[e.RowIndex].Cells[1].Value != null)
                            {
                                for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                                {
                                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value != null)
                                    {
                                        TotAmt = TotAmt + Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                                    }
                                }
                                txtTotalAmt.Text = TotAmt.ToString();
                                dgPayChqDetails.Rows.Add();
                                dgPayChqDetails.Focus();
                                dgPayChqDetails.CurrentCell = dgPayChqDetails[0, dgPayChqDetails.Rows.Count - 1];
                            }
                            else
                            {
                                for (int i = 0; i < dgPayChqDetails.Rows.Count; i++)
                                {
                                    if (dgPayChqDetails.Rows[e.RowIndex].Cells[4].Value != null)
                                    {
                                        TotAmt = TotAmt + Convert.ToDouble(dgPayChqDetails.Rows[i].Cells[4].Value);
                                    }
                                }
                                txtTotalAmt.Text = TotAmt.ToString();
                                btnOk.Focus();
                            }
                        }
                    }
                    else
                    {
                        dgPayChqDetails.Rows[e.RowIndex].Cells[4].ErrorText = "Please Enter  Amount";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void lstPaymentType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                int cntCompany = 0;
                if (e.KeyCode == Keys.Enter)
                {
                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                    {
                        dgPayType.Rows[i].Cells[2].Value = "0.00";
                    }
                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + "", CommonFunctions.ConStr);
                    if (ObjFunction.GetListValue(lstPartyEnglish) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC)))
                    {
                        if (ControlUnder == 3)
                        {
                            OMMessageBox.Show("This PayType is Not Valid For Current party .. Select other PayType...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            txtPaymentType.Focus();
                            return;
                        }
                    }
                    if (ObjFunction.GetListValue(lstPaymentType) == 1 || ObjFunction.GetListValue(lstPaymentType) == 4 || ObjFunction.GetListValue(lstPaymentType) == 5)
                    {
                        lstPaymentType.TabIndex = 656;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAllowSingleFirmChq)) == true)
                        {
                            for (int i = 1; i < dgBill.Rows.Count - 1; i++)
                                if (Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dgBill.Rows[i - 1].Cells[ColIndex.StockCompanyNo].Value))
                                    cntCompany++;

                        }
                        if (cntCompany == 0 || ObjFunction.GetListValue(lstPaymentType) == 4 || ObjFunction.GetListValue(lstPaymentType) == 5)
                        {
                            //dgPayType.CurrentCell = dgPayType[2, 1];
                            pnlPartial.Visible = true;
                            if (ObjFunction.GetListValue(lstPaymentType) == 4 || ObjFunction.GetListValue(lstPaymentType) == 5)
                            {
                                if (ObjFunction.GetListValue(lstPaymentType) == 4)
                                {
                                    pnlPaymentType.Visible = false;
                                    pnlPartial.Size = new Size(475, 214);
                                    pnlPartial.Location = new Point(75, 123);
                                    dgPayChqDetails.Location = dgPayType.Location;
                                    dgPayChqDetails.Visible = true;
                                    dgPayChqDetails.BringToFront();
                                    dgPayChqDetails.Columns[3].Visible = false;
                                    dgPayChqDetails.Focus();
                                    dgPayCreditCardDetails.Visible = false;
                                    dgPayChqDetails.Enabled = true;
                                    if (dgPayChqDetails.Rows.Count == 0)
                                    {
                                        dgPayChqDetails.Rows.Add();
                                        dgPayChqDetails.CurrentCell = dgPayChqDetails[0, 0];
                                    }
                                }
                                else if (ObjFunction.GetListValue(lstPaymentType) == 5)
                                {
                                    pnlPartial.Size = new Size(475, 214);
                                    pnlPartial.Location = new Point(75, 123);
                                    dgPayCreditCardDetails.Location = dgPayType.Location;
                                    dgPayCreditCardDetails.Visible = true;
                                    ((DataGridViewTextBoxColumn)dgPayCreditCardDetails.Columns[0]).MaxInputLength = Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_CreditCardDigitLimit));
                                    dgPayCreditCardDetails.Focus();
                                    dgPayCreditCardDetails.BringToFront();
                                    dgPayChqDetails.Visible = false;
                                    dgPayCreditCardDetails.Enabled = true;
                                    dgPayCreditCardDetails.Columns[2].Visible = false;
                                    if (dgPayCreditCardDetails.Rows.Count == 0)
                                    {
                                        dgPayCreditCardDetails.Rows.Add();
                                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[0, 0];
                                    }
                                }
                                dgPayType.Rows[Convert.ToInt32(ObjFunction.GetListValue(lstPaymentType)) - 1].Cells[2].Value = lblGrandTotal.Text;
                                // dgPayType.CurrentCell = dgPayType[2, Convert.ToInt32(ObjFunction.GetComboValue(cmbPaymentType)) - 1];
                                CaluculatePayType();
                            }
                            else
                            {
                                dgPayCreditCardDetails.Visible = false;
                                dgPayChqDetails.Visible = false;
                                pnlPartial.Location = new Point(200, 123);
                                pnlPartial.Size = new Size(305, 221);
                                dgPayType.CurrentCell = dgPayType[2, 1];
                                dgPayType.Focus();
                            }
                            btnSave.Enabled = false;
                        }
                    }
                    else if (ObjFunction.GetListValue(lstPaymentType) == 3)
                    {
                        e.SuppressKeyPress = true;
                        btnSave.Enabled = true;
                        btnSave.Focus();
                    }
                    else
                    {
                        pnlPartial.Visible = false;
                        //cmbPaymentType.TabIndex = 10;
                        pnlPaymentType.Visible = false;
                        btnSave.Enabled = true;
                        txtRemark.Focus(); ;
                    }
                    e.SuppressKeyPress = true;
                    txtPaymentType.Text = lstPaymentType.Text;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void BindPayCreditDetails(long VchNo)
        {
            string strQuery = "SELECT  TVoucherChqCreditDetails.CreditCardNo, IsNull(MOtherBank.BankName,'') AS BankName, IsNull(MBranch.BranchName,'') AS BranchName,  " +
                " TVoucherChqCreditDetails.Amount, TVoucherChqCreditDetails.PkSrNo, TVoucherChqCreditDetails.BankNo, TVoucherChqCreditDetails.BranchNo FROM TVoucherChqCreditDetails LEFT OUTER JOIN " +
                " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                " MOtherBank ON TVoucherChqCreditDetails.BankNo = MOtherBank.BankNo Where TVoucherChqCreditDetails.CreditCardNo <>'' AND TVoucherChqCreditDetails.FKVoucherNo=" + VchNo + "";
            dgPayCreditCardDetails.Rows.Clear();

            DataTable dt = ObjFunction.GetDataView(strQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgPayCreditCardDetails.Rows.Add();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    dgPayCreditCardDetails.Rows[i].Cells[k].Value = dt.Rows[i].ItemArray[k];
                    //if (strBank == "0")
                    //    strBank = dt.Rows[i].ItemArray[6].ToString();
                    //else
                    //    strBank = strBank + "," + dt.Rows[i].ItemArray[6].ToString();
                    //if (strBranch == "0")
                    //    strBranch = dt.Rows[i].ItemArray[7].ToString();
                    //else
                    //    strBranch = strBranch + dt.Rows[i].ItemArray[7].ToString();
                }
            }
            // ObjFunction.FillList(lstBank, "Select BankNo,BankName From MOtherBank where IsActive='true' or BankNo in(" + strBank + ") order by BankName");
            //ObjFunction.FillList(lstBranch, "Select BranchNo,BranchName From MBranch where IsActive='true' or BranchNo in (" + strBranch + ") order by BranchName");
            //dgPayCreditCardDetails.Rows.Add();
            dgPayCreditCardDetails.Columns[0].Width = 69;
            dgPayCreditCardDetails.Columns[1].Width = 120;
            dgPayCreditCardDetails.Columns[2].Width = 114;
            dgPayCreditCardDetails.Columns[3].Width = 75;
        }

        private void dgPayCreditCardDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 1)
                {
                    pnlBank.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 72 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                    pnlBank.Visible = true;
                    pnlBank.BringToFront();
                    lstBank.Focus();
                }
                else if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 2)
                {
                    pnlBranch.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 200 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                    pnlBranch.Visible = true;
                    pnlBranch.BringToFront();
                    lstBranch.Focus();
                }
                else if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 3)
                {
                    double TotAmt = 0;
                    for (int i = 0; i < dgPayCreditCardDetails.Rows.Count; i++)
                    {
                        if (dgPayCreditCardDetails.Rows[i].Cells[3].Value != null)
                        {
                            TotAmt = TotAmt + Convert.ToDouble(dgPayCreditCardDetails.Rows[i].Cells[3].Value);
                        }
                    }
                    txtTotalAmt.Text = TotAmt.ToString();
                    btnOk.Focus();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                double Amt = 0; int cnt = 0;
                if (dgPayCreditCardDetails.Rows[0].Cells[0].Value != null)
                {
                    if (dgPayCreditCardDetails.Rows[0].Cells[0].FormattedValue.ToString() == "" || dgPayCreditCardDetails.Rows[0].Cells[1].FormattedValue.ToString() == "" || dgPayCreditCardDetails.Rows[0].Cells[2].FormattedValue.ToString() == "")
                    {
                        OMMessageBox.Show("Please Fill Credit Card Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                    }
                    for (int i = 0; i < dgPayCreditCardDetails.Rows.Count; i++)
                    {
                        Amt += (dgPayCreditCardDetails.Rows[i].Cells[3].Value == null) ? 0 : Convert.ToDouble(dgPayCreditCardDetails.Rows[i].Cells[3].Value);
                    }
                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                    {
                        if (dgPayType.Rows[i].Cells[1].Value.ToString() == ObjFunction.GetListValue(lstPaymentType).ToString())
                        {
                            cnt = i;
                            break;
                        }
                    }
                    if (Convert.ToDouble(dgPayType.Rows[cnt].Cells[2].Value) != Amt)
                    {
                        OMMessageBox.Show("Please enter CrediCard amount and CreditCard Details amount are not same...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        dgPayCreditCardDetails.Focus();
                    }
                    else
                    {
                        if (ObjFunction.GetListValue(lstPaymentType) == 1)
                        {
                            pnlPartial.Size = new Size(305, 214);
                            pnlPartial.Location = new Point(200, 123);
                            dgPayType.CurrentCell = dgPayType[2, 4];
                            dgPayType.Focus();
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnSave.Focus();
                            pnlPartial.Visible = false;
                        }
                        //btnOk.Focus();
                    }
                }
                else
                {
                    if (ObjFunction.GetListValue(lstPaymentType) == 1)
                    {
                        pnlPartial.Size = new Size(305, 214);
                        pnlPartial.Location = new Point(200, 123);
                        dgPayType.CurrentCell = dgPayType[2, 4];
                        dgPayType.Focus();
                    }
                    else
                    {
                        //btnSave.Enabled = true;
                        //btnSave.Focus();
                        txtPaymentType.Focus();
                        pnlPartial.Visible = false;
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                //if (dgPayCreditCardDetails.CurrentCell.RowIndex != dgPayCreditCardDetails.Rows.Count - 1)
                //{
                if (Convert.ToInt64(dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[4].Value) != 0)
                {
                    DeleteDtls(5, Convert.ToInt64(dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[4].Value));
                }
                dgPayCreditCardDetails.Rows.RemoveAt(dgPayCreditCardDetails.CurrentCell.RowIndex);
                if (dgPayCreditCardDetails.Rows.Count == 0)
                    dgPayCreditCardDetails.Rows.Add();
                dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[0, dgPayCreditCardDetails.Rows.Count - 1];
                //}
            }
            else if (e.KeyCode == Keys.N && e.Control)
            {
                if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 1)
                {
                    Master.OtherBankAE NewF = new Yadi.Master.OtherBankAE(-1);
                    ObjFunction.OpenForm(NewF);
                    if (NewF.ShortID < 0)
                    {
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[2, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                    else if (NewF.ShortID != 0)
                    {
                        ObjFunction.FillList(lstBank, "Select BankNo,BankName From MBank order by BankName");
                        lstBank.SelectedValue = ObjQry.ReturnLong("Select Max(BankNo) From MBank", CommonFunctions.ConStr);
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[1].Value = lstBank.Text;
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[5].Value = lstBank.SelectedValue;
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                }
                else if (dgPayCreditCardDetails.CurrentCell.ColumnIndex == 2)
                {
                    Master.BranchAE NewF = new Yadi.Master.BranchAE(-1);
                    ObjFunction.OpenForm(NewF);
                    if (NewF.ShortID < 0)
                    {
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                    else if (NewF.ShortID != 0)
                    {
                        ObjFunction.FillList(lstBranch, "Select BranchNo,BranchName From MBranch order by BranchName");
                        lstBranch.SelectedValue = ObjQry.ReturnLong("Select Max(BranchNo) From MBranch ", CommonFunctions.ConStr);
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[2].Value = lstBranch.Text;
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[6].Value = lstBranch.SelectedValue;
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                }
            }
        }

        private void dgPayCreditCardDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (dgPayCreditCardDetails.Rows[e.RowIndex].Cells[1].Value == null)
                {
                    //if (e.RowIndex == 0) dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].Value = dgPayType.Rows[4].Cells[2].Value;
                    if (e.RowIndex == 0)
                    {
                        for (int i = 0; i < dgPayType.Rows.Count; i++)
                        {
                            if (dgPayType.Rows[i].Cells[1].Value.ToString() == ObjFunction.GetListValue(lstPaymentType).ToString())
                                if (e.RowIndex == 0) dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].Value = dgPayType.Rows[i].Cells[2].Value;
                        }
                    }
                    pnlBank.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 72 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);
                    pnlBank.Visible = true;
                    pnlBank.BringToFront();
                    lblNMsg.Text = "(Ctrl+N) Add New Bank";
                    lstBank.Focus();
                }
                else
                {
                    dgPayCreditCardDetails.Focus();
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { dgPayCreditCardDetails.CurrentCell.RowIndex, 1, dgPayCreditCardDetails });
                }
            }
            if (e.ColumnIndex == 3)
            {
                if (dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].Value != null)
                {
                    dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "";
                    if (ObjFunction.CheckValidAmount(dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].Value.ToString()) == false)
                    {
                        dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "Please Enter Valid Amount";
                    }
                    else
                    {
                        double TotAmt = 0;
                        dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "";
                        if (e.RowIndex == dgPayCreditCardDetails.Rows.Count - 1 && dgPayCreditCardDetails.Rows[e.RowIndex].Cells[1].Value != null)
                        {
                            dgPayCreditCardDetails.Rows.Add();
                            dgPayCreditCardDetails.Focus();
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[0, dgPayCreditCardDetails.Rows.Count - 1];
                        }

                        for (int i = 0; i < dgPayCreditCardDetails.Rows.Count; i++)
                        {
                            if (dgPayCreditCardDetails.Rows[i].Cells[3].Value != null)
                            {
                                TotAmt = TotAmt + Convert.ToDouble(dgPayCreditCardDetails.Rows[i].Cells[3].Value);
                            }
                        }
                        txtTotalAmt.Text = TotAmt.ToString();
                    }
                }
                else
                {
                    dgPayCreditCardDetails.Rows[e.RowIndex].Cells[3].ErrorText = "Please Enter  Amount";
                }
            }
        }
        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtItemName.Focus();
            }
        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtItemName.Text == "" && lstGroup1.Items.Count > 0)
                {
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                    //  lstGroup1.SelectedIndex = 0;
                }
                else
                {
                    if (lstGroup1.Items.Count == 0)
                    {
                        DisplayMessage("Please insert the item list");
                    }
                    pnlGroup1.Visible = false;

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtItemName.Text = "";
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                txtItemName.Focus();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                dgBill.Focus();
            }
            else
            {
                // e.KeyChar = Convert.ToChar(0);
                pnlGroup1.Visible = true;
                lstGroup1.Focus();
            }
        }

        private void lstBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {

                    int GroupNo = Convert.ToInt32(lstGroup1.SelectedValue);
                    pnlItemName.Visible = true;
                    dgItemList.Focus();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }
            }

            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtItemName.Focus();

                pnlGroup1.Visible = false;

            }



        }

        private void lstItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtItemName.Text = lstGroup1.Text;

                    txtItemName.Focus();
                    pnlGroup1.Visible = false;
                    pnlItemName.Visible = false;

                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                lstGroup1.Focus();
                lstGroup1.SelectedIndex = 0;
                pnlItemName.Visible = false;

            }

        }

        private void txtBQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if ((Convert.ToDouble((txtBQuantity.Text == "") ? "0" : txtBQuantity.Text)) != 0.00)
                {
                    CalculateSubTotal();
                    if (lstUOM.Items.Count > 1)
                    {
                        //txtBUom.Focus();
                        pnlUOM.Visible = true;
                        lstUOM.Focus();
                    }
                    else { txtBRate.Focus(); }

                }



                else
                {
                    MessageBox.Show("enter Valid Quantity");

                    txtBQuantity.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                txtBQuantity.Text = txtBQuantity.SelectedText;
                txtItemName.Focus();

            }

        }

        private void txtUom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBUom.Text == "")
                {
                    pnlUOM.Visible = true;
                    lstUOM.Focus();
                    lstUOM.SelectedIndex = 0;
                }
                else
                {
                    pnlUOM.Visible = false;

                    txtBRate.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                txtBQuantity.Focus();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtBUom.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstUOMNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtBUom.Text = lstUOM.Text;
                    subUomno = Convert.ToInt32(lstUOM.SelectedValue);
                    if (State == true)
                    {
                        strSql = "Select * From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",51,NULL) As t Where r.ItemNo=t.ItemNo " +
                      " union all Select * From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",52,NULL) As t Where r.ItemNo=t.ItemNo";
                    }
                    else
                    {
                        strSql = "Select * From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",53,NULL) As t Where r.ItemNo=t.ItemNo " +
                        " union all Select * From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",54,NULL) As t Where r.ItemNo=t.ItemNo";
                    }


                    dtRateSetting = ObjFunction.GetDataView(strSql).Table;
                    if (dtRateSetting.Rows.Count > 0)
                    {
                        txtMRP.Text = dtRateSetting.Rows[0]["MRP"].ToString();
                        if (lstRateType.SelectedValue.ToString() == "ASaleRate")
                            txtBRate.Text = dtRateSetting.Rows[0]["ASALERATE"].ToString();
                        else if (lstRateType.SelectedValue.ToString() == "BSaleRate")
                            txtBRate.Text = dtRateSetting.Rows[0]["BSALERATE"].ToString();
                        else if (lstRateType.SelectedValue.ToString() == "CSaleRate")
                            txtBRate.Text = dtRateSetting.Rows[0]["CSALERATE"].ToString();
                        else if (lstRateType.SelectedValue.ToString() == "DSaleRate")
                            txtBRate.Text = dtRateSetting.Rows[0]["DSALERATE"].ToString();
                        else if (lstRateType.SelectedValue.ToString() == "ESaleRate")
                            txtBRate.Text = dtRateSetting.Rows[0]["ESALERATE"].ToString();
                        else if (lstRateType.SelectedValue.ToString() == "MRP")
                            txtBRate.Text = dtRateSetting.Rows[0]["MRP"].ToString();
                        else if (lstRateType.SelectedValue.ToString() == "PurRate")
                            txtBRate.Text = dtRateSetting.Rows[0]["PurRate"].ToString();
                        txtPkRateSettingNo = Convert.ToInt64(dtRateSetting.Rows[0]["PkSrNo"].ToString());
                        lstUOM.SelectedValue = dtRateSetting.Rows[0]["Uomno"].ToString();
                        txtBUom.Text = lstUOM.Text;
                        txtBMKTQTY.Text = dtRateSetting.Rows[0]["MKTQty"].ToString();
                        dgBill.CurrentRow.Cells[ColIndex.StockFactor].Value = txtBStockConversion.Text = dtRateSetting.Rows[0]["StockConversion"].ToString();
                        txtBDiscAmt1.Text = "0.00";
                        txtBDiscRs1.Text = "0.00";
                        txtBDiscPer1.Text = "0.00";
                        txtBAmount.Text = "";

                        CalculateSubTotal();

                    }
                    else
                    {
                        DisplayMessage("Item Tax Details Not Found.........");
                        txtBarCode.Focus();
                    }
                    txtBRate.Focus();
                    pnlUOM.Visible = false;


                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                lstUOM.Focus();
                lstUOM.SelectedIndex = 0;
                pnlUOM.Visible = false;

            }
        }

        private void txtBRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToDouble(txtBRate.Text) > 0.00)
                {
                    CalculateSubTotal();
                    txtBDiscPer1.Focus();
                }
                else { txtBRate.Focus(); }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                if (lstUOM.Items.Count == 1)
                {
                    txtBQuantity.Focus();
                }
                else
                {
                    txtBUom.Focus();
                }
            }
        }

        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtBDiscPer1.Focus();
            }
        }

        private void txtDiscPer1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToDouble(txtBDiscPer1.Text).ToString("0.00") != "0.00")
                { CalculateSubTotal(); }
                txtBDiscRs1.Focus();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                txtBRate.Focus();
            }
        }

        private void txtDiscAmt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtBDiscRs1.Focus();
            }
        }

        private void txtDiscRs1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToDouble(txtBDiscRs1.Text).ToString("0.00") != "0.00")
                { CalculateSubTotal(); }
                txtBCharges.Focus();
            }
        }

        private void txtBAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnGOk.Focus();
            }
        }

        private void btnGOk_Click(object sender, EventArgs e)
        {
            bool qty = false;
            if (dtRateSetting.Rows.Count > 0 && txtBarCode.Text != "")
            {
                int rwindex = 0;

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_AllowsDuplicatesItems)) == false && ItemExist(BItemNo, txtPkRateSettingNo, out rwindex) == true)
                {
                    pnlItemName.Visible = false;
                    //if (ItemExistScheme(BItemNo, txtPkRateSettingNo, out rwindex) == true)
                    //{
                    dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                    dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                    qty = true;
                    CalculateTotal();
                    Clear();
                    //}
                    //else
                    //{
                    //    if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                    //    OMMessageBox.Show("This Item is alreay used for Scheme...", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                    //}
                    txtBarCode.Text = "";
                    txtItemName.Focus();
                }

                if (qty == false)
                {
                    dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value = dtRateSetting.Rows[0]["Itemno"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = txtItemName.Text;
                    dgBill.CurrentRow.Cells[ColIndex.MRP].Value = txtMRP.Text;
                    dgBill.CurrentRow.Cells[ColIndex.Rate].Value = txtBRate.Text;
                    dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = txtBQuantity.Text;
                    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = txtPkRateSettingNo;
                    dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text = txtBUom.Text;
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = lstUOM.SelectedValue;
                    dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = txtBDiscPer1.Text;
                    dgBill.CurrentRow.Cells[ColIndex.DiscAmount].Value = txtBDiscAmt1.Text;
                    dgBill.CurrentRow.Cells[ColIndex.DiscRupees].Value = txtBDiscRs1.Text;
                    dgBill.CurrentRow.Cells[ColIndex.SGSTPercentage].Value = txtBTaxPer1.Text = dtRateSetting.Rows[0]["Percentage"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.CGSTPercentage].Value = txtBTaxPer2.Text = dtRateSetting.Rows[1]["Percentage"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.MKTQuantity].Value = txtBMKTQTY.Text = dtRateSetting.Rows[0]["MKTQty"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.StockFactor].Value = txtBStockConversion.Text = dtRateSetting.Rows[0]["StockConversion"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.PkItemTaxInfo].Value = dtRateSetting.Rows[0]["PkSrNo1"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.PkItemTaxInfo2].Value = dtRateSetting.Rows[1]["PkSrNo1"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.TaxLedgerNo].Value = dtRateSetting.Rows[0]["TaxLedgerNo"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.SalesLedgerNo].Value = dtRateSetting.Rows[0]["TaxLedgerNo"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.TaxLedgerNo2].Value = dtRateSetting.Rows[1]["TaxLedgerNo"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.SalesLedgerNo2].Value = dtRateSetting.Rows[1]["TaxLedgerNo"].ToString();

                    dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = txtBarCode.Text;
                    dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].Value = 0;
                    dgBill.CurrentRow.Cells[ColIndex.PkVoucherNo].Value = 0;
                    dgBill.CurrentRow.Cells[ColIndex.Charg].Value = txtBCharges.Text;
                    dgBill.CurrentRow.Cells[ColIndex.CessPer].Value = txtACessPer.Text;
                    dgBill.CurrentRow.Cells[ColIndex.DhekharekPer].Value = txtADhekhrekPer.Text;
                    dgBill.CurrentRow.Cells[ColIndex.PackagingCharges].Value = txtPackagingCharges.Text;
                    dgBill.CurrentRow.Cells[ColIndex.NoOfBag].Value = txtNoOfBag.Text == "" ? "0" : txtNoOfBag.Text;
                    dgBill.CurrentRow.Cells[ColIndex.ContainerCharges].Value = txtContainerCharges.Text;
                    // dgBill.CurrentRow.Cells[ColIndex.TRWeight].Value =0.00;
                    // dgBill.CurrentRow.Cells[ColIndex.GRWeight].Value = 0.00;
                    // dgBill.CurrentRow.Cells[ColIndex.Freight].Value = 0.00;
                    // dgBill.CurrentRow.Cells[ColIndex.OtherCharges].Value = 0.00;
                    //  dgBill.CurrentRow.Cells[ColIndex.ESFlag].Value = ObjQry.ReturnBoolean("select ESFlag from Mitemmaster where itemno =" + dtRateSetting.Rows[0]["Itemno"].ToString(), CommonFunctions.ConStr);
                    dgBill.CurrentRow.Cells[ColIndex.ESFlag].Value = ObjQry.ReturnBoolean("select ESFlag from Mitemmaster where itemno =" + dtRateSetting.Rows[0]["Itemno"].ToString(), CommonFunctions.ConStr);
                    dgBill.CurrentRow.Cells[ColIndex.StockCompanyNo].Value = DBGetVal.FirmNo;

                    CalculateTotal();
                    Clear();
                    dgBill.Rows.Add();
                    dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex + 1];
                    lblitemcount.Text = Convert.ToString((dgBill.Rows.Count - 1));
                    txtItemName.Focus();

                }
            }

        }
        public void Clear()
        {
            txtBarCode.Text = "";
            txtItemName.Text = "";
            txtBQuantity.Text = "";
            txtBUom.Text = "";
            txtBRate.Text = "";
            txtMRP.Text = "";
            txtBStock.Text = "";
            txtBStockConversion.Text = "";
            txtBTaxPer1.Text = "";
            txtBTaxPer2.Text = "";
            txtBDiscAmt1.Text = "";
            txtBDiscPer1.Text = "";
            txtBDiscRs1.Text = "";
            txtBMKTQTY.Text = "";
            txtBarCode.Focus();
            txtBAmount.Text = "";
            txtBCharges.Text = "";
            txtContainerCharges.Text = "";
            txtACessPer.Text = "";
            txtACessRs.Text = "";
            txtADhekhrekPer.Text = "";
            txtADhekhrekRs.Text = "";
            txtNoOfBag.Text = "";
            txtPackagingCharges.Text = "";
        }

        #region Rate Type Realted Methods and Functions

        public void FillRateType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RateType");
            dt.Columns.Add("RateTypeName");
            DataRow dr = null;
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

            dr = dt.NewRow();
            dr[1] = "PurRate";
            dr[0] = "PurRate";
            dt.Rows.Add(dr);

            lstRateType.DataSource = dt.DefaultView;
            lstRateType.DisplayMember = dt.Columns[1].ColumnName;
            lstRateType.ValueMember = dt.Columns[0].ColumnName;
            SetRateType(Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType).ToString()));
        }

        public void FillRateType(long RateTypeNo)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RateType");
            dt.Columns.Add("RateTypeName");
            DataRow dr = null;

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)) == true || RateTypeNo == 1)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
                dr[0] = "ASaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)) == true || RateTypeNo == 2)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
                dr[0] = "BSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive)) == true || RateTypeNo == 3)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.CRateLabel);
                dr[0] = "CSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive)) == true || RateTypeNo == 4)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.DRateLabel);
                dr[0] = "DSaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive)) == true || RateTypeNo == 5)
            {
                dr = dt.NewRow();
                dr[1] = ObjFunction.GetAppSettings(AppSettings.ERateLabel);
                dr[0] = "ESaleRate";
                dt.Rows.Add(dr);
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_BillWithMRP)) == true || RateTypeNo == 6)
            {
                dr = dt.NewRow();
                dr[1] = "MRP";
                dr[0] = "MRP";
                dt.Rows.Add(dr);
            }

            dr = dt.NewRow();
            dr[1] = "PurRate";
            dr[0] = "PurRate";
            dt.Rows.Add(dr);

            lstRateType.DataSource = dt.DefaultView;
            lstRateType.DisplayMember = dt.Columns[1].ColumnName;
            lstRateType.ValueMember = dt.Columns[0].ColumnName;
            SetRateType(Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType).ToString()));
        }

        public int GetRateType()
        {
            string str = lstRateType.SelectedValue.ToString(); ;
            int val = 0;
            if (str == "ASaleRate") val = 1;
            else if (str == "BSaleRate") val = 2;
            else if (str == "CSaleRate") val = 3;
            else if (str == "DSaleRate") val = 4;
            else if (str == "ESaleRate") val = 5;
            else if (str == "MRP") val = 6;
            else if (str == "PurRate") val = 7;
            return val;
        }

        public void SetRateType(long RateTypeNo)
        {
            if (RateTypeNo == 1) lstRateType.SelectedValue = "ASaleRate";
            else if (RateTypeNo == 2) lstRateType.SelectedValue = "BSaleRate";
            else if (RateTypeNo == 3) lstRateType.SelectedValue = "CSaleRate";
            else if (RateTypeNo == 4) lstRateType.SelectedValue = "DSaleRate";
            else if (RateTypeNo == 5) lstRateType.SelectedValue = "ESaleRate";
            //else if (RateTypeNo == 6) lstRateType.SelectedValue = "MRP";
            else if (RateTypeNo == 6) lstRateType.SelectedValue = "PurRate";
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {

            Form NewF = new MDIParent1();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            dgBill.Enabled = false;
            dgBill.Rows.Clear();
            pnlRateTypeH.Visible = false;
            pnlParty.Visible = false;
            DisplayList(false);
            NavigationDisplay(2);
            if (ID == 0)
            {
                btnUpdate.Enabled = false;
                btnBillCancel.Enabled = false;
                btnPrint.Enabled = false;
                btnSearch.Enabled = false;
            }
            viewmode();
            dtDelete.Clear();
            btnNew.Focus();
        }
        public void DisplayList(bool flag)
        {
            pnlItemName.Visible = flag;
            pnlGroup1.Visible = flag;
            pnlUOM.Visible = flag;
            pnlRate.Visible = flag;
            pnlParking.Visible = flag;
            pnlMainParking.Visible = flag;
            pnlPartial.Visible = flag;
            pnlPaymentType.Visible = flag;
            pnlBank.Visible = flag;
        }
        public void viewmode()
        {
            Clear();
            if (dtRateSetting != null)
                dtRateSetting.Clear();
            if (pnlBillSize == false)
            {
                pnlBill.Visible = false;
                dgBill.Top = dgBill.Top - (pnlBill.Height + 2);
                dgBill.Height = dgBill.Height + (pnlBill.Height);
                pnlBillSize = true;
            }
            else
            {
                pnlBillSize = false;

            }


        }
      
        private void FillItemList(int qNo, int iType)
        {
            try
            {
                switch (iType)
                {
                    case 1:
                        FillItemList(qNo);
                        break;
                    case 2:
                        break;
                    case 3:
                
                        DataTable dtBarCodeItemNo = ObjFunction.GetDataView("Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "'").Table;
                        string ItemList = "";
                        for (int i = 0; i < dtBarCodeItemNo.Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                ItemList += " Union all ";
                            }

                  
                            ItemList += " SELECT MStockItems.ItemNo, MStockItems.ItemName,MStockItems.ItemNameLang, MRateSetting." + ObjFunction.GetListValue(lstRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                                " IsNull(MSB.CurrentStock,0) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                                " MStockItems.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, MStockItems.UOMDefault,MRateSetting.PurRate " +
                                " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                                " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON MStockItems.ItemNo = MRateSetting.ItemNo AND  " +
                                " MStockItems.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                                " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                                " MUOM ON MStockItems.UOMDefault = MUOM.UOMNo LEFT OUTER JOIN MStockItemBalance MSB ON MSB.ItemNo = MStockItems.ItemNo AND MSB.MRP = MRateSetting.MRP AND MSB.GodownNo = " + ObjFunction.GetComboValue(cmbLocation) + "  Where  " +
                                " MStockItems.IsActive='true' and MStockItems.FkStockGroupTypeNo<>3 ";
                        }
                        ItemList += " ORDER BY MStockItems.ItemName";

                        DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                        if (dtItemList.Rows.Count > 0)
                        {
                            dgItemList.DataSource = dtItemList.DefaultView;
                            pnlItemName.Visible = true;
                            dgItemList.CurrentCell = dgItemList[1, 0];
                            dgItemList.Focus();
                        }
                        else
                        {
                            DisplayMessage("Items Not Found......");
                            dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                            dgBill.Focus();
                        }
                        break;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillItemList(int qNo)
        {
            try
            {
                if (qNo == 0)
                {
                    qNo = iItemNameStartIndex;
                }

                string ItemList = strItemQuery[qNo - 1];

                ItemList = ItemList.Replace("@cmbRateType@", "" + lstRateType.Text);
                ItemList = ItemList.Replace("ORDER BY ItemName", " and MStockItems.FkStockGroupTypeNo<>3 ORDER BY ItemName");
                switch (qNo)
                {
                    case 1:
                        break;
                    case 2:
                        switch (strItemQuery.Length)
                        {
                            case 2:
                                //very important for item grid bind list
                                if (DBGetVal.KachhaFirm == true)
                                {

                                    ItemList = ItemList.Replace("MItemMaster.ItemName AS ItemName", "Case When MItemMaster.esflag='false'then( MItemMaster.ItemName+ ' *') else MItemMaster.ItemName end AS ItemName");

                                    ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value));

                                }
                                else
                                {
                                    ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value) + " and MItemMaster.ESFlag='False' ");

                                }
                                ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value));
                                ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL"));
                                break;
                            case 3:
                                ItemList = ItemList.Replace("@Param2@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : Param2Value));
                                ItemList = ItemList.Replace("@Param2NULL@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : "NULL"));
                                break;
                        }
                        break;
                    case 3:
                        ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value));
                        ItemList = ItemList.Replace("@Param2@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : Param2Value));
                        ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL"));
                        ItemList = ItemList.Replace("@Param2NULL@", "" + (Convert.ToInt64(lstGroup2.SelectedValue) != 0 ? lstGroup2.SelectedValue.ToString() : "NULL"));
                        break;
                }
                ItemList = ItemList.Replace("@CompNo@", "" + DBGetVal.FirmNo);
                ItemList = ItemList.Replace("MRateSetting.PurRate AS SaleRate", "Isnull((Select Rate FRom Tstock Where PKStockTrnNo in (Select Max(PKStockTrnNo) FRom Tstock,TVoucherEntry Where TVoucherEntry.PKVoucherNo=TStock.FKVoucherNo AND TVoucherEntry.VoucherTypeCode=" + VchType.Purchase + " AND FKRateSettingNo in(MRateSetting.PkSrNo))),MRateSetting.PurRate) AS SaleRate");
                //ItemList = ItemList.Replace("TStockGodown.GodownNo=2", "TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbLocation) + "");
                ItemList = ItemList.Replace("@GodownNo@", "" + ObjFunction.GetComboValue(cmbLocation) + "");

                switch (strItemQuery.Length - qNo)
                {
                    case 0:
                        ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MItemMaster.LangShortDesc<>'') then MItemMaster.LangShortDesc else MItemMaster.LangFullDesc end AS ItemNameLang,");

                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ItemList = ItemList.Replace("ORDER BY", "And MItemMaster.itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' ) Order by ");

                            //if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                            //{
                            //    ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MStockItems.LangShortDesc<>'') then MStockItems.LangShortDesc else MStockItems.LangFullDesc end AS ItemNameLang,");


                            DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                            if (dtItemList.Rows.Count > 0)
                            {
                                dgItemList.DataSource = dtItemList.DefaultView;
                                pnlItemName.Visible = true;
                                dgItemList.CurrentCell = dgItemList[1, 0];
                                dgItemList.Focus();
                            }
                            else
                            {
                                DisplayMessage("Items Not Found......");
                                dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                                dgBill.Focus();
                            }
                        }
                        else
                        {
                            pnlItemName.Visible = true;
                            dgItemList.CurrentCell = dgItemList[1, 0];
                            dgItemList.Focus();
                        }
                        break;
                    case 1:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ObjFunction.FillList(lstGroup1, ItemList);
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                            {
                                ItemList = ItemList.Replace("ItemGroupName from", "LangGroupName from");

                                ObjFunction.FillList(lstGroup1Lang, ItemList.Replace("StockGroupName from", "LangGroupName from"));
                            }
                            //  ObjFunction.FillList(lstGroup1, ItemList);
                            strItemQuery_last[qNo - 1] = ItemList;
                        }
                        pnlGroup1.Visible = true;
                        lstGroup1.Focus();
                        break;
                    case 2:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ObjFunction.FillList(lstGroup2, ItemList);
                            strItemQuery_last[qNo - 1] = ItemList;
                        }
                        pnlGroup2.Visible = true;
                        lstGroup2.Focus();
                        break;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void FillItemList()
        {
            try
            {
                string ItemList = "";
                if (ItemType == 3)
                {
                    ItemList = " SELECT MStockItems.ItemNo, MStockItems.ItemName,MStockItems.ItemNameLang, MRateSetting." + ObjFunction.GetListValue(lstRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                        " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbLocation) + " AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = MStockItems.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = MStockItems.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = MStockItems.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        " MStockItems.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, MStockItems.UOMDefault,MRateSetting.PurRate " +
                        " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL,NULL) AS MRateSetting ON MStockItems.ItemNo = MRateSetting.ItemNo AND  " +
                        " MStockItems.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                       " MUOM ON MStockItems.UOMDefault = MUOM.UOMNo  Where MStockItems.ItemNo in " +
                        "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') AND MStockItems.CompanyNo=" + DBGetVal.FirmNo + " AND MStockItems.IsActive='true' and MStockItems.FkStockGroupTypeNo<>3" +
                        " ORDER BY MStockItems.ItemName";
                    DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                    if (dtItemList.Rows.Count > 0)
                    {
                        dgItemList.DataSource = dtItemList.DefaultView;
                        pnlItemName.Visible = true;
                        dgItemList.CurrentCell = dgItemList[1, 0];
                        dgItemList.Focus();
                    }
                    else
                    {
                        DisplayMessage("Items Not Found......");
                        dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }
                }
                else if (ItemNameType == 1)
                {
                    ItemList = " SELECT MStockItems.ItemNo, MStockItems.ItemName,MStockItems.ItemNameLang, MRateSetting." + ObjFunction.GetListValue(lstRateType) + " AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=" + ObjFunction.GetComboValue(cmbLocation) + " AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = MStockItems.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = MStockItems.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = MStockItems.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                            " MStockItems.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, MStockItems.UOMDefault,MRateSetting.PurRate " +
                            " FROM MStockItems INNER JOIN " +
                            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS MRateSetting ON MStockItems.ItemNo = MRateSetting.ItemNo AND  " +
                            " MStockItems.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        " MUOM ON MStockItems.UOMDefault = MUOM.UOMNo Where MStockItems.ItemNo <> 1 AND MStockItems.CompanyNo=" + DBGetVal.FirmNo + " and MStockItems.FkStockGroupTypeNo<>3 " +
                            " ORDER BY MStockItems.ItemName";
                    DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                    if (dtItemList.Rows.Count > 0)
                    {
                        dgItemList.DataSource = dtItemList.DefaultView;
                        pnlItemName.Visible = true;
                        dgItemList.CurrentCell = dgItemList[1, 0];
                        dgItemList.Focus();
                    }
                    else
                    {
                        DisplayMessage("Items Not Found......");
                        dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }

                }
                else if (ItemNameType == 2)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1 AND CompanyNo=" + DBGetVal.FirmNo + ") order by StockGroupName";
                    ObjFunction.FillList(lstGroup1, ItemList);
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                    //lstGroup1.Location = new Point(dgBill.Left + dgBill.CurrentCell.ContentBounds.Left, dgBill.Top + dgBill.CurrentRow.Height + dgBill.CurrentCell.ContentBounds.Top + dgBill.ColumnHeadersHeight);
                }
                else if (ItemNameType == 3)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo1 from MStockItems WHERE ItemNo<>1 AND CompanyNo=" + DBGetVal.FirmNo + ") order by StockGroupName";
                    ObjFunction.FillList(lstGroup2, ItemList);
                    pnlGroup2.Visible = true;
                    lstGroup2.Focus();
                }
                else if (ItemNameType == 4)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1 AND CompanyNo=" + DBGetVal.FirmNo + ") order by StockGroupName";
                    ObjFunction.FillList(lstGroup1, ItemList);
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                }
                dgBill.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    pnlGroup1.Visible = false;

                    l_lstGroup1_Index = lstGroup1.SelectedIndex;
                    FillItemList(strItemQuery.Length);
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Space))
                {
                    pnlGroup1.Visible = false;
                    txtItemName.Text = "";
                    txtItemName.Focus();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void lstGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                if (lstGroup1.Items.Count > 0)
                    lstGroup1Lang.SelectedIndex = lstGroup1.SelectedIndex;
            }
        }

        private void lstGroup1Lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                if (lstGroup1.Items.Count > 0)
                    lstGroup1.SelectedIndex = lstGroup1Lang.SelectedIndex;
            }
        }

        private void lstGroup2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //string ItemListStr = "";
                if (e.KeyChar == 13)
                {
                    pnlGroup2.Visible = false;

                    FillItemList(strItemQuery.Length - 1);


                }
                else if (e.KeyChar == ' ')
                {
                    dgBill.Focus();
                    pnlGroup2.Visible = false;
                }



            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lst_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (((System.Windows.Forms.Control)sender).Visible == true)
                {
                    dgBill.Enabled = false;
                    if (((System.Windows.Forms.Control)sender).Name == "lstGroup1")
                    {
                        lstGroup1.SelectedIndex = l_lstGroup1_Index;
                    }
                }
                else
                {
                    dgBill.Enabled = true;
                    dgBill.Focus();
                }
                if (((System.Windows.Forms.Control)sender).Name == "pnlItemName")
                {
                    if (((System.Windows.Forms.Control)sender).Visible == false)
                        pnlSalePurHistory.Visible = ((System.Windows.Forms.Control)sender).Visible;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstPartEnglish_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                if (lstPartyEnglish.Items.Count > 0)
                    lstPartyLang.SelectedIndex = lstPartyEnglish.SelectedIndex;
                //Ledgerno = Convert.ToInt64(lstPartyEnglish.SelectedValue);
            }
        }

        private void lstPartyLang_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtArea_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtArea.Text == "")
                {
                    pnlArea.Visible = true;
                    lstArea.Focus();
                    lstArea.SelectedIndex = 0;
                }
                else
                {
                    pnlArea.Visible = false;

                    txtParty.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtArea.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtArea.Text = lstArea.Text;
                    txtArea.Focus();
                    pnlArea.Visible = false;


                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                lstArea.Focus();
                lstArea.SelectedIndex = 0;
                pnlArea.Visible = false;

            }
        }


        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int Quantity = 2;
            public static int UOM = 3;
            public static int Rate = 4;
            public static int MRP = 5;
            public static int NetRate = 6;
            public static int DiscPercentage = 7;
            public static int DiscAmount = 8;
            public static int DiscRupees = 9;
            public static int DiscPercentage2 = 10;
            public static int DiscAmount2 = 11;
            public static int DiscRupees2 = 12;
            public static int NetAmt = 13;
            public static int Amount = 14;
            public static int Charg = 15;
            public static int Barcode = 16;
            public static int PkStockTrnNo = 17;
            public static int PkVoucherNo = 18;
            public static int ItemNo = 19;
            public static int UOMNo = 20;
            public static int PkRateSettingNo = 21;

            public static int StockFactor = 22;
            public static int ActualQty = 23;
            public static int MKTQuantity = 24;

            public static int SGSTPercentage = 25;
            public static int SGSTAmount = 26;
            public static int TaxLedgerNo = 27;
            public static int SalesLedgerNo = 28;
            public static int PkItemTaxInfo = 29;
            public static int SalesVchNo = 30;
            public static int TaxVchNo = 31;

            public static int CGSTPercentage = 32;
            public static int CGSTAmount = 33;
            public static int TaxLedgerNo2 = 34;
            public static int SalesLedgerNo2 = 35;
            public static int PkItemTaxInfo2 = 36;
            public static int SalesVchNo2 = 37;
            public static int TaxVchNo2 = 38;

            public static int IGSTPercentage = 39;
            public static int IGSTAmount = 40;
            public static int TaxLedgerNo3 = 41;
            public static int SalesLedgerNo3 = 42;
            public static int PkItemTaxInfo3 = 43;
            public static int SalesVchNo3 = 44;
            public static int TaxVchNo3 = 45;

            public static int Remarks = 46;

            public static int StockCompanyNo = 47;
            //public static int DisplayName = 46;
            //public static int IsRateChange = 47;
            public static int TempRate = 48;
            public static int PONo = 49;
            public static int PkStockDetailsNo = 50;
            public static int FkOtherStockTrnNo = 51;
            public static int FreeQty = 52;
            public static int FreeUomName = 53;
            public static int BarcodePrinting = 54;
            public static int FreeUomNo = 55;
            public static int LandedRate = 56;


            public static int ESFlag = 57;
            public static int TempMRP = 58;

            public static int CessValue = 59;
            public static int PackagingCharges = 60;
            public static int PackagingChargesAmt = 61;
            public static int OtherCharges = 62;


            public static int NoOfBag = 63;
            public static int CessPer = 64;
            public static int DhekharekPer = 65;
            public static int ContainerCharges = 66;
            public static int ContainerChargesAmt = 67;
            //public static int GRWeight = 60;
            //public static int TRWeight = 61;
            //public static int Freight = 62;
            //public static int OtherCharges = 63;

        }
        #endregion
        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Spaceflag == false) { Spaceflag = true; return; }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                {
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = 0;
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value = 0;

                    Desc_Start();
                    if (dgBill.CurrentCell.Value == null)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ItemName, dgBill });
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    Qty_MoveNext();
                }
                //else if (dgBill.CurrentCell.ColumnIndex == ColIndex.FreeQty)
                //{
                //    isDisc1PercentChanged = true;
                //    isDisc2PercentChanged = true;
                //   // FreeQty_MoveNext();
                //}
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
                {
                    UOM_Start();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                {
                    //  Rate_MoveNext();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    Disc_MoveNext();
                    if (dgBill.Columns[ColIndex.DiscAmount].Visible == true)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscAmount, dgBill });
                    }
                    else
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscPercentage2, dgBill });
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscAmount)
                {
                    CalculateTotal();
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscPercentage2, dgBill });
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscRupees)
                {
                    Disc_MoveNext();
                    if (dgBill.Columns[ColIndex.DiscPercentage2].Visible == true)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscPercentage2, dgBill });
                    }
                    else
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBill });
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage2)
                {
                    Disc_MoveNext();
                    if (dgBill.Columns[ColIndex.DiscAmount2].Visible == true)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscAmount2, dgBill });
                    }
                    else
                    {

                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBill });
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscRupees2)
                {
                    CalculateTotal();
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBill });

                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscAmount2)
                {
                    CalculateTotal();
                    MovetoNext move2n = new MovetoNext(m2n);
                    if (dgBill.Columns[ColIndex.DiscRupees2].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscRupees2, dgBill });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBill });
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.MRP)
                {
                    if (dgBill.CurrentCell.Value != null && dgBill.CurrentCell.Value.ToString() != "")
                    {
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void dgBill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row, col;
                if (dgBill.CurrentCell != null)
                { col = dgBill.CurrentCell.ColumnIndex; row = dgBill.CurrentCell.RowIndex; }
                else { col = e.ColumnIndex; row = e.RowIndex; }
                if (dgBill.Rows.Count > 0)
                    dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                if (col == ColIndex.Quantity && row >= 0)
                {
                    dgBill.CurrentCell.ErrorText = "";
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (dgBill.CurrentCell.Value.ToString() != "" && dgBill.CurrentCell.Value.ToString() != "0")
                        {
                            if (ObjFunction.CheckNumeric(dgBill.CurrentCell.Value.ToString()) == true)
                            {
                                int rowIndex = dgBill.CurrentCell.RowIndex;
                                if (dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex].Value == null || Convert.ToString(dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex].Value) == "")
                                    dgBill[ColIndex.Amount, dgBill.CurrentCell.RowIndex].Value = "0.00";
                                else
                                    CalculateTotal();
                                dgBill.Focus();

                                dgBill.CurrentCell = dgBill[2, row + 1];
                            }
                        }
                    }
                    txtBarCode.Focus();
                }
                else if (col == ColIndex.Rate && row >= 0)
                {

                    dgBill.CurrentCell.ErrorText = "";
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (dgBill.CurrentCell.Value.ToString() != "" && dgBill.CurrentCell.Value.ToString() != "0")
                        {
                            if (ObjFunction.CheckNumeric(dgBill.CurrentCell.Value.ToString()) == true)
                            {
                                dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                                dgBill.Rows[dgBill.CurrentCell.RowIndex].Selected = true;
                            }
                        }
                    }


                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                    {
                        if (pnlItemName.Visible && isFromTxtBarcode &&
                            (!(txtBarCode.Text.Equals(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value.ToString(), StringComparison.CurrentCultureIgnoreCase)) ||
                            !(txtMRP.Text.Equals(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value.ToString(), StringComparison.CurrentCultureIgnoreCase)))
                            )
                        {
                            pnlItemName.Visible = false;
                            txtBarCode.Text = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value.ToString();
                            txtBStock.Text = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[6].Value.ToString();
                            txtPkRateSettingNo = Convert.ToInt32(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value.ToString());
                            txtMRP.Text = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value.ToString();
                            FillDgBill(Convert.ToInt32(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value));
                        }
                        else
                        {
                            pnlItemName.Visible = false;
                        }
                        return;
                    }
                }
                else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Escape)
                {
                    pnlItemName.Visible = false;
                    if (strItemQuery.Length > 1)
                    {
                        pnlGroup1.Visible = true;
                        lstGroup1.Focus();
                    }
                    else
                    {
                        dgBill.Focus();
                    }
                }
                else if (e.KeyCode == Keys.F6)
                {
                    e.SuppressKeyPress = true;
                    pnlSalePurHistory.Visible = !pnlSalePurHistory.Visible;
                    pnlSalePurHistory.BringToFront();
                    if (pnlSalePurHistory.Visible == true)
                    {
                        pnlSalePurHistory.Location = new Point(42, 235 + 88);
                        pnlSalePurHistory.Size = new Size(692, 230);
                        DataSet ds = ObjDset.FillDset("New", "Exec GetLastSalePurchaseDetails " + ObjFunction.GetListValue(lstPartyEnglish) + "," + dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value.ToString() + " ", CommonFunctions.ConStr);
                        if (ds.Tables.Count > 0)
                        {
                            dgSaleHistory.DataSource = ds.Tables[0].DefaultView;
                            dgPurHistory.DataSource = ds.Tables[1].DefaultView;
                            dgSaleHistory.ReadOnly = true;
                            dgPurHistory.ReadOnly = true;
                            if (dgSaleHistory.Rows.Count > 0)
                            {
                                dgSaleHistory.Location = new Point(8, 8);
                                dgSaleHistory.Size = new Size(323, 200);
                                dgSaleHistory.Columns[0].Width = 70;
                                dgSaleHistory.Columns[1].Width = 60;
                                dgSaleHistory.Columns[2].Width = 50; dgSaleHistory.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgSaleHistory.Columns[3].Width = 50; dgSaleHistory.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgSaleHistory.Columns[4].Width = 60;
                                dgSaleHistory.Columns[5].Width = 60; dgSaleHistory.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                            if (dgPurHistory.Rows.Count > 0)
                            {
                                dgPurHistory.Location = new Point(337, 8);
                                dgPurHistory.Size = new Size(339, 200);
                                dgPurHistory.Columns[0].Width = 70;
                                dgPurHistory.Columns[1].Width = 60;
                                dgPurHistory.Columns[2].Width = 50; dgPurHistory.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgPurHistory.Columns[3].Width = 50; dgPurHistory.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgPurHistory.Columns[4].Width = 85;
                            }
                        }
                        else
                        {
                            dgSaleHistory.Rows.Clear();
                            dgPurHistory.Rows.Clear();
                        }
                    }
                    dgItemList.Focus();
                }
                else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    pnlSalePurHistory.Visible = false;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (OMMessageBox.Show("Are sure want to Deactivate the rate..?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        DBMItemMaster dbMItemMaster = new DBMItemMaster();
                        MRateSetting mRateSettings = new MRateSetting();
                        mRateSettings.PkSrNo = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);
                        mRateSettings.IsActive = false;
                        mRateSettings.ItemNo = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                        dbMItemMaster.UpdateMRateSetting(mRateSettings);
                        dbMItemMaster.ExecuteNonQueryStatements();
                        dgItemList.Rows.RemoveAt(dgItemList.CurrentCell.RowIndex);
                        dgItemList.Focus();
                    }



                }
                else if (e.KeyCode == Keys.ShiftKey)
                {

                    if (OMMessageBox.Show("Are sure want to transfer this item ....?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        DBMItemMaster dbMItemMaster = new DBMItemMaster();
                        MItemMaster mItemMaster = new MItemMaster();
                        if (Convert.ToBoolean(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[17].Value) == true)
                        {
                            mItemMaster.ESFlag = false;


                            mItemMaster.ItemNo = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                            dbMItemMaster.UpdateMItemMaster(mItemMaster.ItemNo, mItemMaster.ESFlag);
                            dbMItemMaster.ExecuteNonQueryStatements();
                            //dgItemList.Rows.RemoveAt(dgItemList.CurrentCell.RowIndex);
                            dgItemList.Focus();
                        }
                        else
                        {
                            mItemMaster.ESFlag = true;
                            mItemMaster.ItemNo = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                            dbMItemMaster.UpdateMItemMaster(mItemMaster.ItemNo, mItemMaster.ESFlag);
                            dbMItemMaster.ExecuteNonQueryStatements();
                            dgItemList.Focus();
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }


        public bool ItemExist(long ItNo, long RateSettingNo, out int rowIndex)
        {

            rowIndex = -1;
            bool flag = false;
            try
            {
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) && RateSettingNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value))
                    {
                        //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_AfterSaveNotDeleteItem)) == false)
                        //{
                        //    rowIndex = i;
                        //    flag = true;
                        //    break;
                        //}
                        //else
                        //{
                        if (Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value) <= 0)
                        {
                            rowIndex = i;
                            flag = true;
                            break;
                        }
                        //}
                    }
                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }
        private void Desc_Start()
        {
            try
            {
                if (dgBill.CurrentCell.Value == null || Convert.ToString(dgBill.CurrentCell.Value) == "")
                {
                    ItemType = 1;
                    FillItemList(0, ItemType);
                }
                else
                {
                    ItemType = 2;
                    long[] BarcodeNo = null; long[] ItemNo = null;

                    switch (dgBill.CurrentCell.Value.ToString().Trim().ToUpper())
                    {
                        case "SV":
                            {
                                if (btnSave.Visible)
                                {
                                    PrintAsk = 2;
                                    btnSave_Click(btnSave, null);
                                    return;
                                }
                                break;
                            }
                        case "SVP":
                            {
                                if (btnSave.Visible)
                                {
                                    PrintAsk = 1;
                                    btnSave_Click(btnSave, null);
                                    return;
                                }
                                break;
                            }
                        case "CRP":
                            {
                                if (btnSave.Visible)
                                {
                                    dgBill.CurrentCell.Value = "";
                                    lstPaymentType.SelectedValue = "3";
                                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                                    {
                                        dgPayType.Rows[i].Cells[2].Value = "0.00";
                                    }
                                    lstPaymentType.Focus();
                                    return;
                                }
                                break;
                            }
                        case "CHQ":
                            {
                                if (btnSave.Visible)
                                {
                                    dgBill.CurrentCell.Value = "";
                                    lstPaymentType.SelectedValue = "4";
                                    //  cmbPaymentType_KeyDown(cmbPaymentType, new KeyEventArgs(Keys.Enter));
                                    return;
                                }
                                break;
                            }
                        case "CRD":
                            {
                                if (btnSave.Visible)
                                {
                                    dgBill.CurrentCell.Value = "";
                                    lstPaymentType.SelectedValue = "5";
                                    // cmbPaymentType_KeyDown(cmbPaymentType, new KeyEventArgs(Keys.Enter));
                                    return;
                                }
                                break;
                            }
                        case "B":
                            {
                                if (btnSave.Visible)
                                {
                                    if (ObjFunction.GetListValue(lstPaymentType) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_PartyAC)))// && (ObjFunction.GetComboValue(cmbPaymentType) == 3))
                                    {
                                        if (ObjFunction.GetListValue(lstPaymentType) == 3)
                                        {
                                            DisplayMessage("B- Short cut key not working..");
                                            dgBill.CurrentCell.Value = ""; return;
                                        }
                                        lstPaymentType.SelectedValue = "2";
                                        pnlPartyName.Visible = true;
                                        if (ID == 0)
                                            txtPartyName.Text = "";
                                        txtPartyName.Focus();
                                        return;
                                    }
                                    else
                                    {
                                        DisplayMessage("B- Short cut key not working..");
                                        dgBill.CurrentCell.Value = ""; return;
                                    }
                                }
                                break;
                            }
                        case "N":
                            {
                                if (btnSave.Visible)
                                {
                                    if (ObjFunction.GetListValue(lstPaymentType) == 3)
                                    {
                                        DisplayMessage("N- Short cut key not working..");
                                        dgBill.CurrentCell.Value = ""; return;
                                    }
                                    PrintAsk = 2;
                                    lstPaymentType.SelectedValue = "2";
                                    btnSave_Click(btnSave, new EventArgs());
                                    return;
                                }
                                break;
                            }
                        default:
                            {
                                for (int i = 0; i < strItemQuery_last.Length; i++)
                                {
                                    strItemQuery_last[i] = "";
                                }
                                SearchBarcode(dgBill.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);
                                dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0;
                                break;
                            }

                    }

                    if (ItemNo.Length == 0 || BarcodeNo.Length == 0)
                    {
                        string strB = dgBill.CurrentCell.FormattedValue.ToString();
                        dgBill.CurrentCell.Value = null;
                        if (OMMessageBox.Show("Barcode Not Found.\nPRESS ESCAPE TO CONTINUE...." + Environment.NewLine + "Press Ctrl+N New Item..", "Information", OMMessageBoxButton.EscapeButton, OMMessageBoxIcon.Information, "New Item", "Ctrl+N") == DialogResult.No)
                        {

                        }

                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void SearchBarcode(String strBarcode, out long[] ItemNo, out long[] BarcodeNo)
        {

            //string sql = "Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";
            string sql = "";
            DataTable dt = new DataTable();
            sql = "SELECT    MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                " WHERE ((MStockBarcode.Barcode = '" + strBarcode + "') or (MStockItems.ShortCode = '" + strBarcode + "')) AND (MStockItems.IsActive = 'true') and( MStockBarcode.isactive='true') AND (MRateSetting.IsActive='true') " +
                " GROUP BY MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode, MRateSetting.MRP, MRateSetting.UOMNo";
            dt = ObjFunction.GetDataView(sql).Table;
            BarcodeNo = new long[dt.Rows.Count];
            ItemNo = new long[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BarcodeNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                    ItemNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[1].ToString());
                    dgBill.CurrentCell.Value = dt.Rows[i].ItemArray[2].ToString();
                }
            }
            else
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsWeighingBarcode)) == true)
                {
                    if (strBarcode.Substring(0, ObjFunction.GetAppSettings(AppSettings.O_WeighingBarcodeChar).Length) == ObjFunction.GetAppSettings(AppSettings.O_WeighingBarcodeChar))
                    {
                        string[] str = new string[1];
                        str[0] = ObjFunction.GetAppSettings(AppSettings.O_WeighingBarcodeChar).ToString();
                        string[] strLine = strBarcode.Split(str, StringSplitOptions.None);
                        strBarcode = strLine[1];

                        dt = new DataTable();
                        sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
                            " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                            " WHERE ((MStockBarcode.Barcode = '" + strBarcode + "') or (MStockItems.ShortCode = '" + strBarcode + "')) AND (MStockItems.IsActive = 'true') AND (MRateSetting.IsActive='true') " +
                            " GROUP BY MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode, MRateSetting.MRP, MRateSetting.UOMNo";
                        dt = ObjFunction.GetDataView(sql).Table;
                        BarcodeNo = new long[dt.Rows.Count];
                        ItemNo = new long[dt.Rows.Count];
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                BarcodeNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                                ItemNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[1].ToString());
                                dgBill.CurrentCell.Value = dt.Rows[i].ItemArray[2].ToString();
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsWeighingBarcodeKGwise)) == false)
                                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(strLine[2].ToString()).ToString();
                                else
                                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(Convert.ToInt64(strLine[2].ToString()) / 1000.00).ToString(Format.ThreeFloating);
                                //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(strLine[2].ToString()) / 1000;
                            }
                        }

                    }
                }

            }
        }

        private void Desc_MoveNext(long ItemNo, long BarcodeNo)
        {
            try
            {
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = ItemNo;
                // dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;

                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL," + ItemNo + ",NULL,NULL,NULL,NULL,NULL) Where IsActive='true' ").Table;//where ItemNo = " + ItemNo
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
                //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = ObjQry.ReturnString("Select ItemName from MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) where ItemNo = " + ItemNo,CommonFunctions.ConStr);
                //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IsRateChange].Value = "0";

                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();

                if (StopOnQty == true)
                {
                    if (dgBill[2, dgBill.CurrentCell.RowIndex].Value == null)
                    {
                        dgBill.CurrentCell = dgBill[2, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }
                    else
                        Qty_MoveNext();
                }
                else
                {
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value == null || dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value.ToString() == "")
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                    Qty_MoveNext();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Qty_MoveNext()
        {
            try
            {
                rowQtyIndex = dgBill.CurrentCell.RowIndex;

                UOM_Start();


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void UOM_Start()
        {
            try
            {
                int row = 0;
                if (dgBill.CurrentCell.RowIndex == 0)
                    row = dgBill.CurrentCell.RowIndex;
                else
                    row = dgBill.CurrentCell.RowIndex;
                dgBill.CurrentCell = dgBill[3, row];

                Rectangle loc = dgBill.GetCellDisplayRectangle(dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex, true);

                pnlUOM.Location = new Point(dgBill.Location.X + loc.X,
                dgBill.Location.Y + loc.Y + loc.Height);
                //dgBill.CurrentCell.ReadOnly = false;
                //FillUOMList(row);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void UOM_MoveNext()
        {
            try
            {
                int Row = dgBill.CurrentCell.RowIndex;

                if (dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value != null &&
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value.ToString() != lstUOM.SelectedValue.ToString())
                {
                    dgBill.CurrentRow.Cells[ColIndex.Rate].Value = "0.00";//lstRate.Text;
                    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0; // lstRate.SelectedValue;
                }

                dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text;
                dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOM.SelectedValue);
                pnlUOM.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BindGrid(int RowIndex)
        {
            try
            {
                long ItemNo, RateSettingNo, BarcodeNo;
                double StockConFactor;
                DataTable dtLedger = new DataTable();

                RateSettingNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value);
                ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);
                BarcodeNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);
                //if (dgBill.Rows[RowIndex].Cells[ColIndex.ItemLevelDiscNo].FormattedValue.ToString() != "" && dgBill.Rows[RowIndex].Cells[ColIndex.ItemLevelDiscNo].FormattedValue.ToString() != "0")
                //{
                //    dgBill.Rows[RowIndex].Cells[ColIndex.DiscRupees].Value = 0;
                //    dgBill.Rows[RowIndex].Cells[ColIndex.ItemLevelDiscNo].Value = "";
                //    txtOtherDisc.Text = "0.00";
                //}
                if (DiscFlag == false)
                {
                    txtOtherDisc.Text = Format.DoubleFloating;// "0.00";
                    txtSchemeDisc.Text = Format.DoubleFloating;
                }
                else DiscFlag = false;

           
                DataTable dt;
                if (State == true)
                {
                    dt = ObjFunction.GetDataView("SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                       " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",51,NULL) As t " +
                       " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo union all SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                       " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",52,NULL) As t " +
                       " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;
                }
                else
                {
                    dt = ObjFunction.GetDataView("SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                           " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",53,NULL) As t " +
                           " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo union all SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                           " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",54,NULL) As t " +
                           " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;


                }
                if (dt.Rows.Count > 0)
                {
                    dgBill.Rows[RowIndex].Cells[ColIndex.MKTQuantity].Value = Convert.ToInt64(dt.Rows[0][1].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value = Convert.ToDouble(dt.Rows[0][7].ToString());
                    StockConFactor = Convert.ToDouble(dt.Rows[0][2].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.StockFactor].Value = StockConFactor;


                    if (dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.PkStockTrnNo].Value = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkVoucherNo].Value = 0;

               
                    dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo].Value = Convert.ToInt64(dt.Rows[0][3].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo].Value = Convert.ToInt64(dt.Rows[0][4].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo].Value = Convert.ToInt64(dt.Rows[0][5].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.SGSTPercentage].Value = Convert.ToDouble(dt.Rows[0][6].ToString());

                    if (State == true)
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo2].Value = Convert.ToInt64(dt.Rows[1][3].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo2].Value = Convert.ToInt64(dt.Rows[1][4].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.SalesVchNo2].Value = Convert.ToInt64(dt.Rows[1][5].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.CGSTPercentage].Value = Convert.ToDouble(dt.Rows[1][6].ToString());

                    }
                    else
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo2].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo2].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.SalesVchNo2].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.CGSTPercentage].Value = 0;
                    }

                    DataTable dtAddtionalTax = ObjFunction.GetDataView("  SELECT r.FkBcdSrNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                   " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",54,NULL) As t " +
                   " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;
                    if (dtAddtionalTax.Rows.Count > 0)
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo3].Value = Convert.ToInt64(dtAddtionalTax.Rows[0][3].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxVchNo3].Value = Convert.ToInt64(dtAddtionalTax.Rows[0][4].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo3].Value = Convert.ToInt64(dtAddtionalTax.Rows[0][5].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.IGSTPercentage].Value = Convert.ToDouble(dtAddtionalTax.Rows[0][6].ToString());

                    }
                    else
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo3].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxVchNo3].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo3].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.IGSTPercentage].Value = 0;

                    }

                    if (ID != 0)
                    {
                        if (dgBill.Rows[RowIndex].Cells[ColIndex.SalesVchNo].Value == null)
                            dgBill.Rows[RowIndex].Cells[ColIndex.SalesVchNo].Value = ObjQry.ReturnLong("SELECT TVoucherDetails.PkVoucherTrnNo FROM MItemTaxInfo INNER JOIN TVoucherDetails ON MItemTaxInfo.SalesLedgerNo = TVoucherDetails.LedgerNo " +
                                " WHERE (MItemTaxInfo.ItemNo = " + dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value + ") AND (TVoucherDetails.FkVoucherNo = " + ID + ")", CommonFunctions.ConStr);
                        if (dgBill.Rows[RowIndex].Cells[ColIndex.TaxVchNo].Value == null)
                            dgBill.Rows[RowIndex].Cells[ColIndex.TaxVchNo].Value = ObjQry.ReturnLong("SELECT TVoucherDetails.PkVoucherTrnNo FROM MItemTaxInfo INNER JOIN TVoucherDetails ON MItemTaxInfo.TaxLedgerNo = TVoucherDetails.LedgerNo " +
                                " WHERE (MItemTaxInfo.ItemNo = " + dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value + ") AND (TVoucherDetails.FkVoucherNo = " + ID + ")", CommonFunctions.ConStr);
                    }
                    if (dgBill.Rows[RowIndex].Cells[ColIndex.PONo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.PONo].Value = "0";
   

                    if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1 && dgBill.CurrentCell.ColumnIndex == 4)
                    {
                        dgBill.Rows.Add();
                    }

                    CalculateTotal();

                    DataTable dtStk;
                    DataRow dr = null;
                    if (dtBillCollect.Count == RowIndex)
                    {
                        dtStk = ObjFunction.GetDataView("Exec GetStockGodownDetails 0,0").Table;
                        for (int row = 0; row < dtStk.Rows.Count; row++)
                        {
                            if (Convert.ToInt64(dtStk.Rows[row].ItemArray[0].ToString()) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_OutwardLocation)))
                            {
                                dr = dtStk.Rows[row];
                                dr[2] = dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value;
                                dr[3] = dgBill.Rows[RowIndex].Cells[ColIndex.ActualQty].Value;
                            }
                        }
                        dtBillCollect.Add(dtStk);
                    }
                    else
                    {
                        dtStk = dtBillCollect[RowIndex];
                        for (int row = 0; row < dtStk.Rows.Count; row++)
                        {
                            if (Convert.ToInt64(dtStk.Rows[row].ItemArray[0].ToString()) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_OutwardLocation)))
                            {
                                dr = dtStk.Rows[row];
                                dr[2] = dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value;
                                dr[3] = dgBill.Rows[RowIndex].Cells[ColIndex.ActualQty].Value;
                            }
                            else
                            {
                                dr = dtStk.Rows[row];
                                dr[2] = 0;
                                dr[3] = 0;
                            }
                        }
                        dtBillCollect.RemoveAt(RowIndex);
                        dtBillCollect.Insert(RowIndex, dtStk);
                    }
                    
                    if (EnrFlag)
                    {
                        
                    }
                    else
                    {
                        //CheckFooterDiscountDetails();
                    }
              
                }
                else
                {
                    for (int i = 1; i < dgBill.Columns.Count; i++)
                    {
                        dgBill.Rows[RowIndex].Cells[i].Value = null;
                    }
                    DisplayMessage("Items Tax Details Not Found.....");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CalculateTotal()
        {
            try
            {
                txtSubTotal.Text = "0.00";
                lblBillItem.Text = "0";
                lblBilExchangeItem.Text = "0";
                lblGrandTotal.Text = "0.00";
                txtTotalDisc.Text = "0.00";
                txtTotalTax.Text = "0.00";
                double subTotal = 0.00, totalDisc = 0.00, totalChrg = 0.00, totalTax = 0.00, TaxPerce = 0.00, TaxPerce2 = 0.00, TaxPerce3 = 0.00, TotalTaxPerce = 0.00;
                double tAmount = 0.00, TotFinal = 0.00, Amount = 0.00, Disc1 = 0.00, Disc2 = 0.00, DiscAmt = 0.00, TaxAmt = 0.00, TotalAmt = 0.00, TaxAmt2 = 0, ttRate = 0, ttax = 0, TaxAmt3 = 0;
                double MaxPer = 0, ACessPer = 0.00, ADhekhrekper = 0.00, ACessValue = 0.00, ADhekhrekValue = 0.00, APMCAMOUNT = 0, PAckingCharges = 0.00, PAckingChargesAmt = 0.00, ContainerCharges = 0.00, ContainerChargesAmt = 0.00, TotalAPMC = 0.00,TotalCess = 0.00, TotalContainerCharges = 0.00;
               
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                    {
                        if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                        {
                            if (dgBill.Rows[i].Cells[ColIndex.Quantity].Value == null) dgBill.Rows[i].Cells[ColIndex.Quantity].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value == null) dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.StockFactor].Value == null) dgBill.Rows[i].Cells[ColIndex.StockFactor].Value = 1;
                            if (dgBill.Rows[i].Cells[ColIndex.Rate].Value == null) dgBill.Rows[i].Cells[ColIndex.Rate].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value = 0;
                            if (dgBill.Rows[i].Cells[ColIndex.Charg].Value == null) dgBill.Rows[i].Cells[ColIndex.Charg].Value = 0;
                         

                            if (Convert.ToInt32(dgBill.Rows[i].Cells[ColIndex.UOMNo].Value) == 4)
                            {
                                Amount = Convert.ToDouble(((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value))) / 1000).ToString("0.0000"));
                            }
                            else
                            {
                                Amount = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value))).ToString("0.0000"));
                            }
                            Disc1 = Convert.ToDouble(((Amount * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value)) / 100).ToString("0.0000"));
                            Disc2 = Convert.ToDouble((((Amount - (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value) + Disc1)) * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.0000"));
                            DiscAmt = (Disc1 + Disc2);
                            DiscAmt += Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value);
                            SubQty = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value);
                            SubMktQty = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value);
                            tAmount = Amount - DiscAmt + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Charg].Value);


                            ACessPer = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.CessPer].Value));
                            ADhekhrekper = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.DhekharekPer].Value));
                            PAckingCharges = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.PackagingCharges].Value));
                            ContainerCharges = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.ContainerCharges].Value));

                            TaxPerce = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value);
                            TaxPerce2 = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.CGSTPercentage].Value);
                            TaxPerce3 = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.IGSTPercentage].Value);
                            TotalTaxPerce = TaxPerce + TaxPerce2 + TaxPerce3;
                            TaxAmt = 0; TaxAmt2 = 0; ttRate = 0; ttax = 0; TaxAmt3 = 0;
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsReverseRateCalc)) == true)//Reverse or forward calculation
                            {
                                ttax = Convert.ToDouble(((tAmount * TotalTaxPerce) / (100 + TotalTaxPerce)).ToString("0.00"));
                                ttRate = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) == 0) ? 0 : Math.Round(Convert.ToDouble(((tAmount - ttax) / SubQty) * SubMktQty), 2);
                                TaxAmt = Math.Round(Convert.ToDouble(((tAmount - ttax) * TaxPerce) / 100), 2);
                                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount - ttax) * TaxPerce2) / 100), 2);
                                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount - ttax) * TaxPerce3) / 100), 2);

                            }
                            else
                            {
                                ttRate = Math.Round(Convert.ToDouble((tAmount / SubQty) * SubMktQty), 2);


                                ACessValue = (ttRate * ACessPer) * SubQty / 100;
                                ADhekhrekValue = (ACessValue * ADhekhrekper) / 100;
                                PAckingChargesAmt = (ttRate * PAckingCharges) * SubQty / 100;
                                ContainerChargesAmt = ContainerCharges * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NoOfBag].Value);
                                APMCAMOUNT = PAckingChargesAmt + ACessValue + ADhekhrekValue + ContainerChargesAmt;
                                TaxAmt = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * TaxPerce) / 100), 2);
                                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * TaxPerce2) / 100), 2);
                                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * TaxPerce3) / 100), 2);

                            }

                            totalTax += TaxAmt + TaxAmt2 + TaxAmt3;

                            //dgBill.Rows[i].Cells[ColIndex.Amount].Value = tAmount.ToString("0.00");

                            dgBill.Rows[i].Cells[ColIndex.Amount].Value = (tAmount + APMCAMOUNT + TaxAmt + TaxAmt2 + TaxAmt3).ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value = Disc1.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = Disc2.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value = TaxAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.CGSTAmount].Value = TaxAmt2.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.IGSTAmount].Value = TaxAmt3.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.CessValue].Value = ACessValue.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.OtherCharges].Value = ADhekhrekValue.ToString("0.00");//dhekhrekh
                            dgBill.Rows[i].Cells[ColIndex.PackagingChargesAmt].Value = PAckingChargesAmt.ToString("0.00");//PAckingChargesAmt
                            dgBill.Rows[i].Cells[ColIndex.ContainerChargesAmt].Value = ContainerChargesAmt.ToString("0.00");//ContainerChargesAmt

                            dgBill.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = (ttRate * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.ActualQty].Value = ((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.StockFactor].Value)));
                            TotalAPMC = TotalAPMC + APMCAMOUNT;
                            subTotal = subTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetAmt].Value);
                            totalDisc = totalDisc + DiscAmt;
                            if (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) >= 0)
                                lblBillItem.Text = (Convert.ToDouble(lblBillItem.Text) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString();
                            else
                                lblBilExchangeItem.Text = (Convert.ToInt64(lblBilExchangeItem.Text) + Math.Abs(Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value))).ToString();
                            if (State == true)
                            {
                                MaxPer = Math.Max(Convert.ToDouble(((dgBill[ColIndex.SGSTPercentage, i].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgBill[ColIndex.SGSTPercentage, i].EditedFormattedValue)) * 2, MaxPer);
                            }
                            else { MaxPer = Math.Max(Convert.ToDouble(((dgBill[ColIndex.IGSTPercentage, i].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgBill[ColIndex.IGSTPercentage, i].EditedFormattedValue)), MaxPer); }

                        }

                        TotalCess = TotalCess + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.CessValue].Value);
                        TotalContainerCharges = TotalContainerCharges + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.ContainerChargesAmt].Value);
                    }
                    subTotal = Convert.ToDouble(subTotal.ToString("0.00"));
                    txtSubTotal.Text = (subTotal + totalDisc).ToString("0.00");
                    txtTotalDisc.Text = totalDisc.ToString("0.00");
                    txtTotalCessValue.Text = TotalCess.ToString("0.00");
                    txtTotalContainerCharges.Text = TotalContainerCharges.ToString("0.00");

                    txtChrgOnTaxAmt.Text = TotalAPMC.ToString("0.00");
                    totalChrg = Convert.ToDouble(txtChrgOnTaxAmt.Text) + Convert.ToDouble(txtChrgRupees2.Text);
                    TotalAmt = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) + totalTax) - Convert.ToDouble(txtTotalDisc.Text)).ToString("0.00"));

                    if (txtSchemeDisc.Text.Trim() == "") txtSchemeDisc.Text = "0.00";
                    if (txtOtherDisc.Text.Trim() == "") txtOtherDisc.Text = "0.00";
                    if (txtDiscRupees1.Text.Trim() == "") txtDiscRupees1.Text = "0.00";
                    TotalAmt -= Convert.ToDouble(txtSchemeDisc.Text) + Convert.ToDouble(txtDiscRupees1.Text) + Convert.ToDouble(txtOtherDisc.Text);


                    totalDisc = Convert.ToDouble(txtDiscRupees1.Text) + Convert.ToDouble(txtSchemeDisc.Text) + Convert.ToDouble(txtOtherDisc.Text);
                    totalChrg = Convert.ToDouble(txtChrgOnTaxAmt.Text) + Convert.ToDouble(txtChrgRupees2.Text);
                    txtTotalAnotherDisc.Text = totalDisc.ToString("0.00");
                    txtTotalChrgs.Text = totalChrg.ToString("0.00");
                    txtTotalTax.Text = totalTax.ToString("0.00");
                    if ((Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) + Convert.ToDouble(txtTotalTax.Text.Trim())) != 0)
                        txtDiscount1.Text = Convert.ToDouble((100 * Convert.ToDouble(txtDiscRupees1.Text)) / (Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) - (Convert.ToDouble(txtSchemeDisc.Text.Trim()) + Convert.ToDouble(txtOtherDisc.Text.Trim())) + Convert.ToDouble(txtTotalTax.Text.Trim()))).ToString("0.00");

                    totalTax = Convert.ToDouble(totalTax.ToString("0.00"));// Math.Round(totalTax, 00);
                    lblGrandTotal.Text = ((subTotal + totalTax + totalChrg) - totalDisc).ToString("0.00");//===u
                    TotFinal = Math.Round(Convert.ToDouble(lblGrandTotal.Text), MidpointRounding.AwayFromZero);//===u
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBillRoundOff)) == true)
                    {
                        txtRoundOff.Text = (TotFinal - Convert.ToDouble(lblGrandTotal.Text)).ToString("0.00");//===u
                        lblGrandTotal.Text = ((subTotal + totalTax + totalChrg + Convert.ToDouble(txtRoundOff.Text)) - totalDisc).ToString("0.00");//===u
                    }
                    else
                        txtRoundOff.Text = "0.00";
                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + "", CommonFunctions.ConStr);
                    if (ControlUnder == 4)
                    {
                        if (dgPayChqDetails.Rows.Count > 0)
                        {
                            dgPayChqDetails.Rows[0].Cells[4].Value = lblGrandTotal.Text;//===u
                        }
                    }
                    else if (ControlUnder == 5)
                    {
                        if (dgPayCreditCardDetails.Rows.Count > 0)
                        {
                            dgPayCreditCardDetails.Rows[0].Cells[3].Value = lblGrandTotal.Text;//===u
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void CalculateSubTotal()
        {


            SubQty = 0; SubRate = 0; SubDiscPer = 0;
            double SubDiscAmt1 = 0, SubCharge = 0.00;
            SubDiscRs = 0; SubTax1 = 0; SubTax2 = 0; SubTax3 = 0;
            double SubAmount = 0;
            double TaxAmt1 = 0, TaxAmt2 = 0, tRate = 0, ttax = 0, TaxAmt3 = 0, ACessPer = 0.00, ADhekhrekper = 0.00, ACessValue = 0.00, ADhekhrekValue = 0.00, APMCAMOUNT = 0, PAckingCharges = 0.00, PAckingChargesAmt = 0.00, ContainerCharges = 0.00, ContainerChargesAmt = 0.00, NoofBag = 0.00;

            SubQty = Convert.ToDouble((txtBQuantity.Text == "") ? "1" : txtBQuantity.Text);
            // dgBill.CurrentRow.Cells[ColIndex.Rate].Value = 
            SubRate = Convert.ToDouble((txtBRate.Text == "") ? "0" : txtBRate.Text);
            // dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = 
            SubDiscPer = Convert.ToDouble((txtBDiscPer1.Text == "") ? "0" : txtBDiscPer1.Text);
            // dgBill.CurrentRow.Cells[ColIndex.DiscRupees].Value =
            SubDiscRs = Convert.ToDouble((txtBDiscRs1.Text == "") ? "0" : txtBDiscRs1.Text);
            SubCharge = Convert.ToDouble((txtBCharges.Text == "") ? "0" : txtBCharges.Text);
            SubTax1 = Convert.ToDouble((txtBTaxPer1.Text == "") ? "0" : txtBTaxPer1.Text);//.ToDouble(dgBill.CurrentRow.Cells[ColIndex.SGSTPercentage].Value);
            SubTax2 = Convert.ToDouble((txtBTaxPer2.Text == "") ? "0" : txtBTaxPer2.Text);// Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.CGSTPercentage].Value);
            SubMktQty = Convert.ToDouble((txtBMKTQTY.Text == "") ? "1" : txtBMKTQTY.Text); //Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.MKTQuantity].Value);
            ACessPer = Convert.ToDouble((txtACessPer.Text == "") ? "0" : txtACessPer.Text);
            ADhekhrekper = Convert.ToDouble((txtADhekhrekPer.Text == "") ? "0" : txtADhekhrekPer.Text);
            PAckingCharges = Convert.ToDouble((txtPackagingCharges.Text == "") ? "0" : txtPackagingCharges.Text);
            ContainerCharges = Convert.ToDouble((txtContainerCharges.Text == "") ? "0" : txtContainerCharges.Text);
            NoofBag = Convert.ToDouble((txtNoOfBag.Text == "") ? "0" : txtNoOfBag.Text);

            if (ObjFunction.GetListValue(lstUOM) == 4)
            {
                SubAmount = Math.Round(((SubQty * (SubRate + SubCharge)) / SubMktQty) / 1000, 2);
            }
            else
            {
                SubAmount = Math.Round((SubQty * (SubRate + SubCharge)) / SubMktQty, 2);
            }

            //SubAmount = Math.Round((SubQty * SubRate) / SubMktQty, 2);
            SubDiscAmt1 = Math.Round((SubAmount * SubDiscPer) / 100, 2);
            txtBDiscAmt1.Text = SubDiscAmt1.ToString();
            SubDiscRs = Math.Round(Convert.ToDouble(txtBDiscRs1.Text), 2);
            SubAmount = Math.Round(SubAmount - SubDiscAmt1 - SubDiscRs, 2);



            double TotalTaxPerce = Convert.ToDouble(SubTax1 + SubTax2 + SubTax3);//total Tax Percentage
            double tAmount = Convert.ToDouble(SubAmount);//discount minus amount
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsReverseRateCalc)) == true)//Reverse or forward calculation
            {
                ttax = Convert.ToDouble(((tAmount * TotalTaxPerce) / (100 + TotalTaxPerce)).ToString("0.00"));
                tRate = Math.Round(Convert.ToDouble(((tAmount - ttax) / SubQty) * SubMktQty), 2);
                TaxAmt1 = Math.Round(Convert.ToDouble(((tAmount - ttax) * SubTax1) / 100), 2);
                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount - ttax) * SubTax2) / 100), 2);
                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount - ttax) * SubTax3) / 100), 2);

            }
            else
            {
                tRate = Math.Round(Convert.ToDouble((tAmount / SubQty) * SubMktQty), 2);


                ACessValue = (tRate * ACessPer) * SubQty / 100;
                ADhekhrekValue = (ACessValue * ADhekhrekper) / 100;
                PAckingChargesAmt = (tRate * PAckingCharges) * SubQty / 100;
                ContainerChargesAmt = ContainerCharges * NoofBag;
                APMCAMOUNT = PAckingChargesAmt + ACessValue + ADhekhrekValue + ContainerChargesAmt;
                txtACessRs.Text = ACessValue.ToString("0.00");
                txtADhekhrekRs.Text = ADhekhrekValue.ToString("0.00");
                txtContainerAmt.Text = ContainerChargesAmt.ToString("0.00");
                txtPackingAmt.Text = PAckingChargesAmt.ToString("0.00");
                TaxAmt1 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * SubTax1) / 100), 2);
                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * SubTax2) / 100), 2);
                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * SubTax3) / 100), 2);
                // tRate = (tAmount / SubQty);

            }
            txtBAmount.Text = (tAmount + APMCAMOUNT + TaxAmt1 + TaxAmt2 + TaxAmt3).ToString("0.00");



        }
        private void Disc_MoveNext()
        {
            CalculateTotal();
        }
        #region dgBill Methods and Events
        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }
        #endregion
        private void ChangeBillRate()
        {
            try
            {
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value != null)
                    {
                        dgBill.Rows[i].Cells[ColIndex.Rate].Value = ObjQry.ReturnDouble("Select " + lstRateType.SelectedValue.ToString() + " From MRateSetting Where PkSrNo=" + dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value + "", CommonFunctions.ConStr);
                    }
                }
                CalculateTotal();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void FillDgBill(long item)
        {

            BItemNo = item;
            if (BItemNo != 0)
            {
                pnlItemName.Visible = false;

                dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.RowCount - 1];
                strSql = "";
                ObjFunction.FillList(lstUOM, "Select UOMNo,UOMName From GetUOMList(NULL," + BItemNo + ",NULL)");

                //=========================check count more than 1  ============================================
                subUomno = ObjQry.ReturnLong("Select MItemMaster.UOMDefault FRom MItemMaster  Where MItemMaster.itemno=" + BItemNo, CommonFunctions.ConStr);
                if (State == true)
                {
                    strSql = "Select r.*,t.*,i.CessValue,i.Dhekhrek,i.ContainerCharges,i.PackagingCharges From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",51,NULL) As t ,MItemmaster as I Where r.ItemNo=t.ItemNo and r.ItemNo=i.ItemNo " +
                  " union all Select r.*,t.*,i.CessValue,i.Dhekhrek,i.ContainerCharges,i.PackagingCharges From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",52,NULL) As t ,MItemmaster as I Where r.ItemNo=t.ItemNo and r.ItemNo=i.ItemNo ";
                }
                else
                {
                    strSql = "Select r.*,t.*,i.CessValue,i.Dhekhrek,i.ContainerCharges,i.PackagingCharges From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",53,NULL) As t  ,MItemmaster as I Where r.ItemNo=t.ItemNo  and r.ItemNo=i.ItemNo " +
                    " union all Select r.*,t.*,i.CessValue,i.Dhekhrek,i.ContainerCharges,i.PackagingCharges From GetItemRateALL(" + BItemNo + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + BItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",54,NULL) As t ,MItemmaster as I  Where r.ItemNo=t.ItemNo and r.ItemNo=i.ItemNo  ";

                }
                dtRateSetting = ObjFunction.GetDataView(strSql).Table;
                if (dtRateSetting.Rows.Count > 0)
                {
                    if (DBGetVal.KachhaFirm == true)
                    {
                        txtItemName.Text = ObjQry.ReturnString("select case when esflag='False' then upper(Itemgroupname + ' ' + Itemname +'*') else  upper(Itemgroupname + ' ' + Itemname)  end as name from MItemMaster inner join MItemGroup on MItemMaster.groupno=MItemGroup.itemgroupno where itemno =" + BItemNo, CommonFunctions.ConStr);//Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value)
                    }
                    else
                    {

                        txtItemName.Text = ObjQry.ReturnString("select upper(Itemgroupname + ' ' + Itemname) as name from MItemMaster inner join MItemGroup on MItemMaster.groupno=MItemGroup.itemgroupno where itemno =" + BItemNo, CommonFunctions.ConStr);
                    }
         
                    txtBRate.Text = dtRateSetting.Rows[0]["PurRate"].ToString();
                    lstUOM.SelectedValue = dtRateSetting.Rows[0]["Uomno"].ToString();
                    txtBUom.Text = lstUOM.Text;
                    txtBTaxPer1.Text = dtRateSetting.Rows[0]["Percentage"].ToString();
                    txtBTaxPer2.Text = dtRateSetting.Rows[1]["Percentage"].ToString();
                    txtBMKTQTY.Text = dtRateSetting.Rows[0]["MKTQty"].ToString();
                    txtBStockConversion.Text = dtRateSetting.Rows[0]["StockConversion"].ToString();
                    txtMRP.Text = dtRateSetting.Rows[1]["MRP"].ToString();
                    txtACessPer.Text = dtRateSetting.Rows[1]["CessValue"].ToString();
                    txtADhekhrekPer.Text = dtRateSetting.Rows[1]["Dhekhrek"].ToString();
                    txtContainerCharges.Text = dtRateSetting.Rows[1]["ContainerCharges"].ToString();
                    txtPackagingCharges.Text = dtRateSetting.Rows[1]["PackagingCharges"].ToString();
                    txtBQuantity.Text = "1";
                    txtBDiscAmt1.Text = "0.00";
                    txtBDiscRs1.Text = "0.00";
                    txtBDiscPer1.Text = "0.00";
                    txtBCharges.Text = "0.00";
                    txtBAmount.Text = "";

                    CalculateSubTotal();
                    txtBQuantity.Focus();
                }
                else
                {
                    DisplayMessage("Item Tax Details Not Found.........");
                    txtBarCode.Focus();
                }
            }
            else
            {
                OMMessageBox.Show("Please enter valid barcode.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtBarCode.Focus();
                Clear();
            }

        }
        private void txtBarCode_KeyDown1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtBarCode.Text != "")
                {
                    BItemNo = ObjQry.ReturnLong("Select MItemMaster.ItemNo FRom MItemMaster  Where MItemMaster.BarCode='" + txtBarCode.Text + "' AND MItemMaster.IsActive = 'True'", CommonFunctions.ConStr);
                    if (BItemNo != 0)
                    {
                        FillDgBill(BItemNo);
                    }
                    else
                    {
                        OMMessageBox.Show("Please enter valid barcode.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtBarCode.Focus();
                    }
                }
                else
                {
                    txtItemName.Focus();
                }
            }
            else if (e.KeyCode == Keys.F9)
            {

            }
            else if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                dgBill.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgBill.Focus();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                if (dgBill.Rows.Count > 0)
                {
                    dgBill.CurrentCell = dgBill[ColIndex.ItemName, 0];
                    dgBill.Focus();
                }
            }


        }
        public void PakkaFirm()
        {
            PBillTotal = 0;
            dtPAKKABILL = new DataTable();
            foreach (DataGridViewColumn col in dgBill.Columns)
            {
                dtPAKKABILL.Columns.Add(col.Name);
            }
            int k = -1;
            for (int i = 0; i < dgBill.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.ESFlag].Value.ToString()) == false)
                {
                    dtPAKKABILL.Rows.Add();
                    k = k + 1;

                    for (int j = 0; j < dtPAKKABILL.Columns.Count; j++)
                    {
                        try
                        {
                            dtPAKKABILL.Rows[k][j] = dgBill.Rows[i].Cells[j].Value;
                        }
                        catch
                        {
                        }
                    }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsReverseRateCalc)) == false)
                        PBillTotal = PBillTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.CGSTAmount].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.IGSTAmount].Value);
                    else
                        PBillTotal = PBillTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].Value);
                }


            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                hidePics();

                double debit = 0;
                if (btnSave.Enabled == false)
                {
                    return;
                }
                btnSave.Enabled = true;
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsCreditBillUpdate)) == true && ObjFunction.GetListValue(lstPaymentType) == 3 && ID != 0)
                {
                    if (Utilities.PasswordAsk.CreditID == 1)
                    {
                        if (Convert.ToDouble(lblGrandTotal.Text) < ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where PkVoucherNo=" + ID + "", CommonFunctions.ConStr))
                        {
                            partialPayAdjust = new Vouchers.PartialPaymentAdjust(ID, Convert.ToDouble(lblGrandTotal.Text.Trim()), ObjFunction.GetListValue(lstPaymentType), ObjFunction.GetListValue(lstPartyEnglish));
                            ObjFunction.OpenForm(partialPayAdjust);

                            partialPayAdjust.Left = 15;
                            if (((PartialPaymentAdjust)partialPayAdjust).DS == DialogResult.Cancel)
                            {
                                return;
                            }
                        }
                    }
                }

                if (Convert.ToDouble(lblGrandTotal.Text) <= 0)
                {
                    OMMessageBox.Show("Negative or ZERO Bill amount not allowed ...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }

                CalculateTotal();
                PakkaFirm();
                dbTVoucherEntry = new DBTVaucherEntry();
                DeleteValues();
                if (ID != 0)
                {
                    dbTVoucherEntry.ReverseStock(ID);
                    dbTVoucherEntry.DeleteTReward_All(ID);
                }

                VoucherUserNo = Convert.ToInt64(txtInvNo.Text.Trim());
                int VoucherSrNo = 1;
                //Voucher Header Entry 
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;

                tVoucherEntry.VoucherTypeCode = VoucherType;
                tVoucherEntry.VoucherUserNo = VoucherUserNo;
                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                tVoucherEntry.VoucherTime = dtpBillTime.Value;
                tVoucherEntry.Narration = "Purchase Bill";
                tVoucherEntry.Reference = txtRefNo.Text;
                tVoucherEntry.ChequeNo = 0;
                tVoucherEntry.ClearingDate = dtpBillDate.Value;
                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                tVoucherEntry.BilledAmount = Convert.ToDouble(lblGrandTotal.Text);
                tVoucherEntry.ChallanNo = "";
                tVoucherEntry.Remark = txtRemark.Text.Trim();
                tVoucherEntry.MacNo = DBGetVal.MacNo;
                tVoucherEntry.PayTypeNo = ObjFunction.GetListValue(lstPaymentType);
                tVoucherEntry.RateTypeNo = GetRateType();
                tVoucherEntry.TaxTypeNo = 1;
                tVoucherEntry.OrderType = 1;
                tVoucherEntry.DiscPercent = (txtDiscount1.Text == "") ? 0 : Convert.ToDouble(txtDiscount1.Text);
                tVoucherEntry.DiscAmt = (txtDiscRupees1.Text == "") ? 0 : Convert.ToDouble(txtDiscRupees1.Text);
                tVoucherEntry.MixMode = MixModeVal;

                tVoucherEntry.UserID = DBGetVal.UserID;
                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsAskUserPassword)) == true)
                //    tVoucherEntry.UserID = Utilities.PasswordAsk.UserID == 0 ? DBGetVal.UserID : Utilities.PasswordAsk.UserID;
                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                tVoucherEntry.IsBillMulti = (dtCompRatio.Rows.Count > 1) ? 1 : 0;
                tVoucherEntry.TransporterCode = ObjFunction.GetComboValue(cmbTransporter);
                tVoucherEntry.TransPayType = ObjFunction.GetComboValue(cmbTransPayType);
                tVoucherEntry.LRNo = txtLRNo.Text;
                tVoucherEntry.TransportMode = ObjFunction.GetComboValue(cmbTransMode);
                tVoucherEntry.TransNoOfItems = (txtTransNoOfItems.Text == "") ? 0 : Convert.ToDouble(txtTransNoOfItems.Text);
                tVoucherEntry.StateCode = statecode;
                if (DBGetVal.KachhaFirm == false)
                    tVoucherEntry.TaxAmount = Convert.ToDouble(txtTotalTax.Text); ;
                tVoucherEntry.LedgerNo = Ledgerno;
                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

                DataTable dtVoucherDetails = new DataTable();

                dtVoucherDetails = new DataTable();
                dtVchPrev = new DataTable();
                if (ID != 0)
                {
                    dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                    if (PayType != 3)
                        dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                    else
                        dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,0.00 AS Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                }
                FillPayType();
                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                tVoucherDetails.SignCode = 1;
                tVoucherDetails.LedgerNo = Ledgerno;//.GetListValue(lstPartyEnglish);
                tVoucherDetails.Debit = 0;
                tVoucherDetails.Credit = Convert.ToDouble(lblGrandTotal.Text);
                tVoucherDetails.Narration = "";
                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                tVoucherDetails.SrNo = Others.Party;
                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);


                for (int row = 0; row < dgPayChqDetails.Rows.Count; row++)
                {
                    if (dgPayChqDetails.Rows[row].Cells[0].Value != null && dgPayChqDetails.Rows[row].Cells[0].Value.ToString() != "")
                    {
                        tVchChqCredit.PkSrNo = Convert.ToInt64(dgPayChqDetails.Rows[row].Cells[5].Value);
                        tVchChqCredit.ChequeNo = dgPayChqDetails.Rows[row].Cells[0].Value.ToString();
                        tVchChqCredit.ChequeDate = (dgPayChqDetails.Rows[row].Cells[1].Value == null) ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime(dgPayChqDetails.Rows[row].Cells[1].Value);
                        tVchChqCredit.BankNo = Convert.ToInt64(dgPayChqDetails.Rows[row].Cells[6].Value);
                        tVchChqCredit.BranchNo = Convert.ToInt64(dgPayChqDetails.Rows[row].Cells[7].Value);
                        tVchChqCredit.CreditCardNo = "";
                        tVchChqCredit.Amount = Convert.ToDouble(dgPayChqDetails.Rows[row].Cells[4].Value);
                        tVchChqCredit.PostFkVoucherNo = 0;
                        tVchChqCredit.PostFkVoucherTrnNo = 0;
                        tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                    }
                }

                for (int row = 0; row < dgPayCreditCardDetails.Rows.Count; row++)
                {
                    if (dgPayCreditCardDetails.Rows[row].Cells[0].Value != null && dgPayCreditCardDetails.Rows[row].Cells[0].Value.ToString() != "")
                    {
                        tVchChqCredit.PkSrNo = Convert.ToInt64(dgPayCreditCardDetails.Rows[row].Cells[4].Value);
                        tVchChqCredit.CreditCardNo = dgPayCreditCardDetails.Rows[row].Cells[0].Value.ToString();
                        tVchChqCredit.ChequeDate = Convert.ToDateTime("01-Jan-1900");
                        tVchChqCredit.BankNo = Convert.ToInt64(dgPayCreditCardDetails.Rows[row].Cells[5].Value);
                        tVchChqCredit.BranchNo = Convert.ToInt64(dgPayCreditCardDetails.Rows[row].Cells[6].Value);
                        tVchChqCredit.ChequeNo = "";
                        tVchChqCredit.Amount = Convert.ToDouble(dgPayCreditCardDetails.Rows[row].Cells[3].Value);
                        tVchChqCredit.PostFkVoucherNo = 0;
                        tVchChqCredit.PostFkVoucherTrnNo = 0;
                        tVchChqCredit.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                    }
                }

                if (Convert.ToDouble(dgPayType.Rows[2].Cells[2].Value) != 0)
                {
                    tVchRefDtls = new TVoucherRefDetails();
                    tVchRefDtls.PkRefTrnNo = ObjQry.ReturnLong("Select PKRefTrnNo From TVoucherRefDetails Where FKVoucherTrnNo=" + tVoucherDetails.PkVoucherTrnNo + " ", CommonFunctions.ConStr);
                    tVchRefDtls.FkVoucherSrNo = tVoucherDetails.VoucherSrNo;
                    tVchRefDtls.LedgerNo = tVoucherDetails.LedgerNo;
                    tVchRefDtls.TypeOfRef = 3;
                    tVchRefDtls.RefNo = 0;
                    tVchRefDtls.DueDays = 0;
                    tVchRefDtls.DueDate = DBGetVal.ServerTime;
                    tVchRefDtls.Amount = tVoucherEntry.BilledAmount;
                    tVchRefDtls.SignCode = 1;
                    tVchRefDtls.UserID = DBGetVal.UserID;
                    tVchRefDtls.UserDate = DBGetVal.ServerTime.Date;
                    tVchRefDtls.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[0].ToString());
                    dbTVoucherEntry.AddTVoucherRefDetails(tVchRefDtls);
                }
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString()) == DBGetVal.FirmNo)//Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[0].ToString())
                    {
                        tStock = new TStock();
                        if (Convert.ToInt64(dgBill[ColIndex.PkStockTrnNo, i].Value) == 0)
                        {
                            tStock.PkStockTrnNo = 0;
                        }
                        else
                        {
                            tStock.PkStockTrnNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value);
                        }

                        tStock.GroupNo = 0;
                        tStock.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                        tStock.FkVoucherSrNo = VoucherSrNo;
                        tStock.TrnCode = 1;// (Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()) < 0) ? 1 : 2;
                        tStock.Quantity = Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                        tStock.BilledQuantity = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString());
                        tStock.Rate = Convert.ToDouble(dgBill[ColIndex.Rate, i].Value.ToString());
                        tStock.Amount = Convert.ToDouble(dgBill[ColIndex.Amount, i].Value.ToString());
                        tStock.DiscPercentage = Convert.ToDouble(dgBill[ColIndex.DiscPercentage, i].Value.ToString());
                        tStock.DiscAmount = Convert.ToDouble(dgBill[ColIndex.DiscAmount, i].Value.ToString());
                        tStock.DiscRupees = Convert.ToDouble(dgBill[ColIndex.DiscRupees, i].Value.ToString());
                        tStock.DiscPercentage2 = Convert.ToDouble(dgBill[ColIndex.DiscPercentage2, i].Value.ToString());
                        tStock.DiscAmount2 = Convert.ToDouble(dgBill[ColIndex.DiscAmount2, i].Value.ToString());
                        tStock.DiscRupees2 = Convert.ToDouble(dgBill[ColIndex.DiscRupees2, i].Value.ToString());
                        tStock.NetRate = Convert.ToDouble(dgBill[ColIndex.NetRate, i].Value.ToString());
                        tStock.NetAmount = Convert.ToDouble(dgBill[ColIndex.NetAmt, i].Value.ToString());
                        tStock.FkUomNo = Convert.ToInt64(dgBill[ColIndex.UOMNo, i].Value.ToString());
                        tStock.FkRateSettingNo = Convert.ToInt64(dgBill[ColIndex.PkRateSettingNo, i].Value.ToString());
                        tStock.FkItemTaxInfo = Convert.ToInt64(dgBill[ColIndex.PkItemTaxInfo, i].Value.ToString());
                        tStock.FreeQty = 0;
                        tStock.FreeUOMNo = 1;
                        tStock.NoOfBag = Convert.ToInt16(dgBill[ColIndex.NoOfBag, i].Value.ToString());
                        tStock.PackagingCharges = Convert.ToDouble(dgBill[ColIndex.PackagingCharges, i].Value.ToString());
                        tStock.ContainerCharges = Convert.ToDouble(dgBill[ColIndex.ContainerCharges, i].Value.ToString());
                        tStock.PackagingChargesAmt = Convert.ToDouble(dgBill[ColIndex.PackagingChargesAmt, i].Value.ToString());
                        tStock.ContainerChargesAmt = Convert.ToDouble(dgBill[ColIndex.ContainerChargesAmt, i].Value.ToString());
                        tStock.UserID = DBGetVal.UserID;
                        //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsAskUserPassword)) == true)
                        //    tStock.UserID = Utilities.PasswordAsk.UserID == 0 ? DBGetVal.UserID : Utilities.PasswordAsk.UserID;
                        tStock.UserDate = DBGetVal.ServerTime.Date;
                        tStock.CompanyNo = Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString());
                        tStock.LandedRate = 0;
                        //tStock.DisplayItemName = (dgBill[ColIndex.DisplayName, i].Value.ToString()==null) ? " " : dgBill[ColIndex.DisplayName, i].Value.ToString();
                        tStock.DisplayItemName = "";// dgBill[ColIndex.DisplayName, i].EditedFormattedValue.ToString();
                        tStock.Remarks = dgBill[ColIndex.Remarks, i].EditedFormattedValue.ToString();
                        // tStock.GRWeight =( Convert.ToDouble(dgBill[ColIndex.GRWeight, i].Value.ToString()) == null) ? 0.00:Convert.ToDouble(dgBill[ColIndex.GRWeight, i].Value.ToString());
                        //tStock.TRWeight =  Convert.ToDouble(dgBill[ColIndex.TRWeight, i].Value.ToString());

                        //
                        tStock.Freight = Convert.ToDouble(dgBill[ColIndex.Charg, i].Value.ToString());
                        // 
                        tStock.OtherCharges = Convert.ToDouble(dgBill[ColIndex.OtherCharges, i].Value.ToString());
                        tStock.CessValue = Convert.ToDouble(dgBill[ColIndex.CessValue, i].Value.ToString());
                        //  tStock. = Convert.ToDouble(dgBill[ColIndex.OtherCharges, i].Value.ToString());
                        //tStock.OtherCharges = Convert.ToDouble(dgBill[ColIndex.OtherCharges, i].Value.ToString());
                        //tStock.OtherCharges = Convert.ToDouble(dgBill[ColIndex.OtherCharges, i].Value.ToString());

                        if (State == true)
                        {
                            tStock.SGSTPercentage = Convert.ToDouble(dgBill[ColIndex.SGSTPercentage, i].Value.ToString());
                            tStock.SGSTAmount = Convert.ToDouble(dgBill[ColIndex.SGSTAmount, i].Value.ToString());
                            tStock.FkItemTaxInfo = Convert.ToInt64(dgBill[ColIndex.PkItemTaxInfo, i].Value.ToString());
                            tStock.CGSTPercentage = Convert.ToDouble(dgBill[ColIndex.CGSTPercentage, i].Value.ToString());
                            tStock.CGSTAmount = Convert.ToDouble(dgBill[ColIndex.CGSTAmount, i].Value.ToString());
                            tStock.FkItemTaxInfo2 = Convert.ToInt64(dgBill[ColIndex.PkItemTaxInfo2, i].Value.ToString());
                            tStock.IGSTAmount = 0;
                            tStock.IGSTPercentage = 0;
                        }
                        else
                        {
                            tStock.SGSTPercentage = 0;
                            tStock.SGSTAmount = 0;
                            tStock.CGSTPercentage = 0;
                            tStock.CGSTAmount = 0;
                            tStock.IGSTAmount = Convert.ToDouble(dgBill[ColIndex.SGSTAmount, i].Value.ToString());
                            tStock.IGSTPercentage = Convert.ToDouble(dgBill[ColIndex.SGSTPercentage, i].Value.ToString());
                        }
                        // tStock.DisplayItemName = dgBill[ColIndex.DisplayName, i].EditedFormattedValue.ToString();

                        dbTVoucherEntry.AddTStock(tStock);

                        mRatesetting = new MRateSetting();
                        mRatesetting.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                        mRatesetting.PkSrNo = Convert.ToInt64(dgBill[ColIndex.PkRateSettingNo, i].Value.ToString());
                        mRatesetting.MRP = Convert.ToDouble(dgBill[ColIndex.MRP, i].Value.ToString());
                        mRatesetting.StockConversion = Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString());
                        mRatesetting.UOMNo = Convert.ToInt64(dgBill[ColIndex.UOMNo, i].Value.ToString());
                        mRatesetting.ASaleRate = -(Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString()) * Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()));
                        if (DBGetVal.KachhaFirm == false)
                        {
                            mRatesetting.ESFlag = false;
                        }
                        else
                        {
                            mRatesetting.ESFlag = true;
                        }

                        dbTVoucherEntry.UpdateMRateSettingStock(mRatesetting);


                    }
                }
                #region //for=========================test
                if (DBGetVal.KachhaFirm == true)
                {

                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = 25;//.GetListValue(lstPartyEnglish);//Sales Account
                    if (Convert.ToDouble(txtRoundOff.Text) >= 0)
                    {

                        tVoucherDetails.Debit = Convert.ToDouble(lblGrandTotal.Text) - Convert.ToDouble(txtRoundOff.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtRoundOff.Text)) / 10;
                    }
                    else
                    {
                        tVoucherDetails.Debit = Convert.ToDouble(lblGrandTotal.Text) + Convert.ToDouble(txtRoundOff.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Math.Abs(Convert.ToDouble(txtRoundOff.Text))) / 10;

                    }
                    tVoucherDetails.Credit = 0;
                    // tVoucherDetails.Credit = Convert.ToDouble(txtGrandTotal.Text) + Convert.ToDouble(txtRoundOff.Text);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.SrNo = Others.Party;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);


                }

                #endregion
                //}
                //====================================for pkka bill posting=============================================================
                #region //for Sales bill tax entry
                if (DBGetVal.KachhaFirm == false)
                {
                    //Item Sales Ledger Details
                    DataTable dtSaleLedger = new DataTable();
                    bool TempFlag = false;
                    dtSaleLedger.Columns.Add();
                    dtSaleLedger.Columns.Add();
                    DataRow dr = dtSaleLedger.NewRow();
                    dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.SalesLedgerNo].Value);
                    dr[1] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                    dtSaleLedger.Rows.Add(dr);
                    for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                    {
                        for (int i = 0; i < dtSaleLedger.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.SalesLedgerNo].Value) != Convert.ToInt64(dtSaleLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtSaleLedger.Rows[i].ItemArray[1].ToString()))
                            {
                                TempFlag = true;

                            }
                            else
                            {
                                TempFlag = false;
                                break;
                            }
                        }
                        if (TempFlag == true)
                        {
                            dr = dtSaleLedger.NewRow();
                            dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.SalesLedgerNo].Value);
                            dr[1] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                            dtSaleLedger.Rows.Add(dr);
                        }
                    }

                    int ledgerNo = 0; int cnt = VoucherSrNo - 1, cntLedg = -1, cntTaxLedg = -1;
                    for (int k = 0; k < dtSaleLedger.Rows.Count; k++)
                    {
                        cntLedg = -1;
                        for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.SalesLedgerNo].Value) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[1].ToString()))
                            {
                                if (cntLedg == -1) cntLedg = j;
                                debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.NetAmt].Value);
                                dgBill.Rows[j].Cells[ColIndex.SalesVchNo].Value = dgBill.Rows[cntLedg].Cells[ColIndex.SalesVchNo].Value;
                                ledgerNo = j;
                            }

                        }
                        if (debit > 0)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            if (dtVoucherDetails.Rows.Count > 0)
                            {
                                //if (Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[1].ToString()) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[0].ToString()))
                                if (Convert.ToInt64(dgBill.Rows[cntLedg].Cells[ColIndex.SalesVchNo].Value) != 0)
                                {
                                    tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[cntLedg].Cells[ColIndex.SalesVchNo].Value);// Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[0].ToString());
                                    cnt++;
                                }
                                else
                                {
                                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                }
                                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            }
                            else
                            {
                                tVoucherDetails.PkVoucherTrnNo = 0;
                            }
                            tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.LedgerNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.SalesLedgerNo].Value);
                            tVoucherDetails.Debit = debit;
                            tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                            tVoucherDetails.Credit = 0;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                            tVoucherDetails.Narration = "";
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                            debit = 0;
                        }
                    }

                    #region // FIRST TAX SGST
                    //Item Tax Details
                    DataTable dtTAxLedger = new DataTable();
                    TempFlag = false;
                    dtTAxLedger.Columns.Add();
                    dtTAxLedger.Columns.Add();
                    dr = dtTAxLedger.NewRow();
                    dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.TaxLedgerNo].Value);
                    dr[1] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                    dtTAxLedger.Rows.Add(dr);
                    for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                    {
                        TempFlag = false;
                        for (int i = 0; i < dtTAxLedger.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[1].ToString()))
                            {
                                TempFlag = true;
                            }
                            else
                            {
                                TempFlag = false;
                                break;
                            }
                        }
                        if (TempFlag == true)
                        {
                            dr = dtTAxLedger.NewRow();
                            dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo].Value);
                            dr[1] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                            dtTAxLedger.Rows.Add(dr);
                        }
                    }

                    cnt = VoucherSrNo - 1;
                    debit = 0;
                    ledgerNo = 0;
                    for (int k = 0; k < dtTAxLedger.Rows.Count; k++)
                    {
                        cntTaxLedg = -1;
                        for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.TaxLedgerNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[1].ToString()))
                            {
                                if (cntTaxLedg == -1) cntTaxLedg = j;
                                debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.SGSTAmount].Value);
                                dgBill.Rows[j].Cells[ColIndex.TaxVchNo].Value = dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo].Value;
                                ledgerNo = j;
                            }
                        }
                        if (debit != 0)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            if (dtVoucherDetails.Rows.Count > 0)
                            {
                                if (Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo].Value) != 0)
                                {
                                    tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo].Value);// Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[0].ToString());
                                    cnt++;
                                }
                                else
                                {
                                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                }
                                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            }
                            else
                            {
                                tVoucherDetails.PkVoucherTrnNo = 0;
                            }
                            tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.LedgerNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.TaxLedgerNo].Value);
                            tVoucherDetails.Debit = debit;
                            tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                            tVoucherDetails.Credit = 0;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                            tVoucherDetails.Narration = "";
                            if (State == true)
                            {
                                tVoucherDetails.SrNo = Others.SGSTLedgerno;
                            }
                            else
                            {

                                tVoucherDetails.SrNo = Others.IGSTLedgerno;
                            }
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                            debit = 0;
                        }
                    }
                    #endregion
                    #region //Item TAX CGST Second tax
                    //Item Tax Details
                    dtTAxLedger = new DataTable();
                    TempFlag = false;
                    dtTAxLedger.Columns.Add();
                    dtTAxLedger.Columns.Add();
                    dr = dtTAxLedger.NewRow();
                    dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.TaxLedgerNo2].Value);
                    dr[1] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                    dtTAxLedger.Rows.Add(dr);
                    for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                    {
                        TempFlag = false;
                        for (int i = 0; i < dtTAxLedger.Rows.Count; i++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo2].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[1].ToString()))
                            {
                                TempFlag = true;
                            }
                            else
                            {
                                TempFlag = false;
                                break;
                            }
                        }
                        if (TempFlag == true)
                        {
                            dr = dtTAxLedger.NewRow();
                            dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo2].Value);
                            dr[1] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                            dtTAxLedger.Rows.Add(dr);
                        }
                    }

                    cnt = VoucherSrNo - 1;
                    debit = 0;
                    ledgerNo = 0;
                    for (int k = 0; k < dtTAxLedger.Rows.Count; k++)
                    {
                        cntTaxLedg = -1;
                        for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.TaxLedgerNo2].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[1].ToString()))
                            {
                                if (cntTaxLedg == -1) cntTaxLedg = j;
                                debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.CGSTAmount].Value);
                                dgBill.Rows[j].Cells[ColIndex.TaxVchNo2].Value = dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo2].Value;
                                ledgerNo = j;
                            }
                        }
                        if (debit != 0)
                        {
                            tVoucherDetails = new TVoucherDetails();
                            if (dtVoucherDetails.Rows.Count > 0)
                            {
                                if (Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo2].Value) != 0)
                                {
                                    tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo2].Value);// Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[0].ToString());
                                    cnt++;
                                }
                                else
                                {
                                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                }
                                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                            }
                            else
                            {
                                tVoucherDetails.PkVoucherTrnNo = 0;
                            }
                            tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.LedgerNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.TaxLedgerNo2].Value);
                            tVoucherDetails.Debit = debit;
                            tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                            tVoucherDetails.Credit = 0;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                            tVoucherDetails.Narration = "";
                            if (State == true)
                            {
                                tVoucherDetails.SrNo = Others.CGSTLedgerno;
                            }
                            else
                            {
                                tVoucherDetails.SrNo = Others.CessLedgerno;

                            }
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                            debit = 0;
                        }
                    }

                    #endregion
                    #region //Item TAX cess third if yes only for within state  tax
                    ////Item Tax Details
                    //if (State == true)
                    //{

                    //    dtTAxLedger = new DataTable();
                    //    TempFlag = false;
                    //    dtTAxLedger.Columns.Add();
                    //    dtTAxLedger.Columns.Add();
                    //    dr = dtTAxLedger.NewRow();
                    //    dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.TaxLedgerNo3].Value);
                    //    dr[1] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                    //    dtTAxLedger.Rows.Add(dr);
                    //    for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                    //    {
                    //        TempFlag = false;
                    //        for (int i = 0; i < dtTAxLedger.Rows.Count; i++)
                    //        {
                    //            if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo3].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[1].ToString()))
                    //            {
                    //                TempFlag = true;
                    //            }
                    //            else
                    //            {
                    //                TempFlag = false;
                    //                break;
                    //            }
                    //        }
                    //        if (TempFlag == true)
                    //        {
                    //            dr = dtTAxLedger.NewRow();
                    //            dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo3].Value);
                    //            dr[1] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                    //            dtTAxLedger.Rows.Add(dr);
                    //        }
                    //    }

                    //    cnt = VoucherSrNo - 1;
                    //    debit = 0;
                    //    ledgerNo = 0;
                    //    for (int k = 0; k < dtTAxLedger.Rows.Count; k++)
                    //    {
                    //        cntTaxLedg = -1;
                    //        for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                    //        {
                    //            if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.TaxLedgerNo3].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[1].ToString()))
                    //            {
                    //                if (cntTaxLedg == -1) cntTaxLedg = j;
                    //                debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.SGSTAmount3].Value);
                    //                dgBill.Rows[j].Cells[ColIndex.TaxVchNo2].Value = dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo3].Value;
                    //                ledgerNo = j;
                    //            }
                    //        }
                    //        if (debit != 0)
                    //        {
                    //            tVoucherDetails = new TVoucherDetails();
                    //            if (dtVoucherDetails.Rows.Count > 0)
                    //            {
                    //                if (Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo3].Value) != 0)
                    //                {
                    //                    tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo3].Value);// Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[0].ToString());
                    //                    cnt++;
                    //                }
                    //                else
                    //                {
                    //                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    //                }
                    //                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    //            }
                    //            else
                    //            {
                    //                tVoucherDetails.PkVoucherTrnNo = 0;
                    //            }
                    //            tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    //            tVoucherDetails.SignCode = 2;
                    //            tVoucherDetails.LedgerNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.TaxLedgerNo3].Value);
                    //            tVoucherDetails.Debit = 0;
                    //            tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                    //            tVoucherDetails.Credit = debit;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                    //            tVoucherDetails.Narration = "";
                    //            tVoucherDetails.SrNo = Others.CessLedgerno;

                    //            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails); SetVoucherDetailsCompany(tVoucherDetails);
                    //            debit = 0;
                    //        }
                    //    }
                    //}

                    #endregion
                }
                #endregion

                //For Scheme Discount
                if (Convert.ToDouble(txtSchemeDisc.Text) != 0)
                {
                    //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                    //{
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Discount1));
                    tVoucherDetails.Debit = 0;//(Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtSchemeDisc.Text)) / 10;
                    tVoucherDetails.Credit = Convert.ToDouble(txtSchemeDisc.Text);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Discount2;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                    //}
                }

                //For Discount Ledger 1 %
                if (Convert.ToDouble(txtDiscRupees1.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Discount1));
                    tVoucherDetails.Debit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtDiscRupees1.Text)) / 10;
                    tVoucherDetails.Credit = Convert.ToDouble(txtDiscRupees1.Text);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Discount1;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                }

                //For Footer Other  disc
                if (Convert.ToDouble(txtOtherDisc.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Discount1));
                    tVoucherDetails.Debit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtOtherDisc.Text)) / 10;
                    tVoucherDetails.Credit = Convert.ToDouble(txtOtherDisc.Text);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Discount3;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                    tFooterDisc = new TFooterDiscountDetails();
                    if (ID == 0)
                    {
                        tFooterDisc.PkSrNo = 0;
                        tFooterDisc.FooterDiscNo = ObjQry.ReturnLong("Select FooterDiscNo From MFooterDiscountDetails Where PkSrNo=" + FooterDiscDtlsNo + "", CommonFunctions.ConStr);
                        tFooterDisc.FooterDiscDetailsNo = FooterDiscDtlsNo;
                    }
                    else
                    {
                        DataTable dtDisc = ObjFunction.GetDataView("Select PkSrNo,FooterDiscNo,FooterDiscDetailsNo From TFooterDiscountDetails Where FKVoucherNo=" + ID + "").Table;
                        if (dtDisc.Rows.Count > 0)
                        {
                            tFooterDisc.PkSrNo = Convert.ToInt64(dtDisc.Rows[0].ItemArray[0].ToString());
                            if (FooterDiscDtlsNo == Convert.ToInt64(dtDisc.Rows[0].ItemArray[2].ToString()))
                            {
                                tFooterDisc.FooterDiscNo = Convert.ToInt64(dtDisc.Rows[0].ItemArray[1].ToString());
                                tFooterDisc.FooterDiscDetailsNo = Convert.ToInt64(dtDisc.Rows[0].ItemArray[2].ToString());
                            }
                            else
                            {
                                tFooterDisc.FooterDiscNo = ObjQry.ReturnLong("Select FooterDiscNo From MFooterDiscountDetails Where PkSrNo=" + FooterDiscDtlsNo + "", CommonFunctions.ConStr);
                                tFooterDisc.FooterDiscDetailsNo = FooterDiscDtlsNo;
                            }
                        }
                        else
                        {
                            tFooterDisc.FooterDiscNo = ObjQry.ReturnLong("Select FooterDiscNo From MFooterDiscountDetails Where PkSrNo=" + FooterDiscDtlsNo + "", CommonFunctions.ConStr);
                            tFooterDisc.FooterDiscDetailsNo = FooterDiscDtlsNo;
                        }
                    }
                    tFooterDisc.DiscAmount = Convert.ToDouble(txtOtherDisc.Text);
                    tFooterDisc.CompanyNo = DBGetVal.FirmNo;
                    tFooterDisc.UserID = DBGetVal.UserID;
                    tFooterDisc.UserDate = DBGetVal.ServerTime;
                    dbTVoucherEntry.AddTFooterDiscountDetails(tFooterDisc);
                }

                //=========Debit Entrys=========================
                //For Charges Rupees 1
                if (Convert.ToDouble(txtChrgOnTaxAmt.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Charges1));
                    tVoucherDetails.Debit = Convert.ToDouble(txtChrgOnTaxAmt.Text);
                    tVoucherDetails.Credit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Charges1;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                }
                //For Charges Rupees 2
                if (Convert.ToDouble(txtChrgRupees2.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Charges2));
                    tVoucherDetails.Debit = Convert.ToDouble(txtChrgRupees2.Text);
                    tVoucherDetails.Credit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Charges2;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                }

                //For Round Off Acc Ledger
                if (Convert.ToDouble(txtRoundOff.Text) != 0)
                {
                    //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                    //{
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RoundOfAcc));
                    if (Convert.ToDouble(txtRoundOff.Text) >= 0)
                    {
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.Debit = Convert.ToDouble(txtRoundOff.Text);
                        tVoucherDetails.Credit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtRoundOff.Text)) / 10;
                    }
                    else
                    {
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.Debit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Math.Abs(Convert.ToDouble(txtRoundOff.Text))) / 10;
                        tVoucherDetails.Credit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));
                    }
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.RoundOff;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                    //}
                }

                if (ID != 0)
                {
                    for (int i = 0; i < dtVoucherDetails.Rows.Count; i++)
                    {
                        if (dtVoucherDetails.Rows[i].ItemArray[2].ToString() == "0")
                            DeleteDtls(2, Convert.ToInt64(dtVoucherDetails.Rows[i].ItemArray[0].ToString()));
                    }
                    // DeleteValues();===umesh
                }
                dbTVoucherEntry.EffectStock();

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsCreditBillUpdate)) == true && ObjFunction.GetListValue(lstPaymentType) == 3 && ID != 0)
                {
                    if (Utilities.PasswordAsk.CreditID == 1)
                    {
                        if (((PartialPaymentAdjust)partialPayAdjust).DS == DialogResult.OK)
                        {
                            partialPayAdjust.SaveData(dbTVoucherEntry);
                            partialPayAdjust = new PartialPaymentAdjust();
                        }
                    }
                }
                #region //Pakkbilltransfer
                if (DBGetVal.KachhaFirm == true && PBillTotal != 0)
                {

                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = ID;

                    tVoucherEntry.VoucherTypeCode = 15;
                    tVoucherEntry.VoucherUserNo = VoucherUserNo;
                    tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                    tVoucherEntry.VoucherTime = dtpBillTime.Value;
                    tVoucherEntry.Narration = "Purchase Bill";
                    tVoucherEntry.Reference = txtPartyName.Text;
                    tVoucherEntry.ChequeNo = 0;
                    tVoucherEntry.ClearingDate = dtpBillDate.Value;
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = Convert.ToDouble(PBillTotal);
                    tVoucherEntry.ChallanNo = "";
                    tVoucherEntry.Remark = txtRemark.Text.Trim();
                    tVoucherEntry.MacNo = DBGetVal.MacNo;
                    tVoucherEntry.PayTypeNo = ObjFunction.GetListValue(lstPaymentType);
                    tVoucherEntry.RateTypeNo = GetRateType();
                    tVoucherEntry.TaxTypeNo = 1;
                    tVoucherEntry.OrderType = 1;
                    tVoucherEntry.DiscPercent = (txtDiscount1.Text == "") ? 0 : Convert.ToDouble(txtDiscount1.Text);
                    tVoucherEntry.DiscAmt = (txtDiscRupees1.Text == "") ? 0 : Convert.ToDouble(txtDiscRupees1.Text);
                    tVoucherEntry.MixMode = MixModeVal;

                    tVoucherEntry.UserID = DBGetVal.UserID;
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsAskUserPassword)) == true)
                    //    tVoucherEntry.UserID = Utilities.PasswordAsk.UserID == 0 ? DBGetVal.UserID : Utilities.PasswordAsk.UserID;
                    tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                    tVoucherEntry.IsBillMulti = (dtCompRatio.Rows.Count > 1) ? 1 : 0;
                    tVoucherEntry.TransporterCode = ObjFunction.GetComboValue(cmbTransporter);
                    tVoucherEntry.TransPayType = ObjFunction.GetComboValue(cmbTransPayType);
                    tVoucherEntry.LRNo = txtLRNo.Text;
                    tVoucherEntry.TransportMode = ObjFunction.GetComboValue(cmbTransMode);
                    tVoucherEntry.TransNoOfItems = (txtTransNoOfItems.Text == "") ? 0 : Convert.ToDouble(txtTransNoOfItems.Text);
                    tVoucherEntry.StateCode = statecode;
                    if (DBGetVal.KachhaFirm == false)
                        tVoucherEntry.TaxAmount = Convert.ToDouble(txtTotalTax.Text); ;
                    tVoucherEntry.LedgerNo = Ledgerno;
                    dbTVoucherEntry.AddTVoucherEntryES(tVoucherEntry);


                    dtVoucherDetails = new DataTable();
                    dtVchPrev = new DataTable();
                    if (ID != 0)
                    {
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                        if (PayType != 3)
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                        else
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,0.00 AS Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                    }



                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Ledgerno;//.GetListValue(lstPartyEnglish);
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(PBillTotal);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.SrNo = Others.Party;
                    dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                    dtVoucherDetails = new DataTable();
                    dtVchPrev = new DataTable();
                    if (ID != 0)
                    {
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                        if (PayType != 3)
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                        else
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,0.00 AS Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                    }



                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Ledgerno;//.GetListValue(lstPartyEnglish);
                    tVoucherDetails.Debit = Convert.ToDouble(PBillTotal);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.SrNo = 502;
                    dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);


                    for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.ESFlag].Value.ToString()) == false)
                        {
                            if (Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString()) == DBGetVal.FirmNo)//Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[0].ToString())
                            {
                                tStock = new TStock();
                                if (Convert.ToInt64(dgBill[ColIndex.PkStockTrnNo, i].Value) == 0)
                                {
                                    tStock.PkStockTrnNo = 0;
                                }
                                else
                                {
                                    tStock.PkStockTrnNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value);
                                }

                                tStock.GroupNo = 0;
                                tStock.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                                tStock.FkVoucherSrNo = VoucherSrNo;
                                tStock.TrnCode = (Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString()) < 0) ? 1 : 2;
                                tStock.Quantity = Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                                tStock.BilledQuantity = Convert.ToDouble(dgBill[ColIndex.ActualQty, i].Value.ToString());
                                tStock.Rate = Convert.ToDouble(dgBill[ColIndex.Rate, i].Value.ToString());
                                tStock.Amount = Convert.ToDouble(dgBill[ColIndex.Amount, i].Value.ToString());
                                tStock.DiscPercentage = Convert.ToDouble(dgBill[ColIndex.DiscPercentage, i].Value.ToString());
                                tStock.DiscAmount = Convert.ToDouble(dgBill[ColIndex.DiscAmount, i].Value.ToString());
                                tStock.DiscRupees = Convert.ToDouble(dgBill[ColIndex.DiscRupees, i].Value.ToString());
                                tStock.DiscPercentage2 = Convert.ToDouble(dgBill[ColIndex.DiscPercentage2, i].Value.ToString());
                                tStock.DiscAmount2 = Convert.ToDouble(dgBill[ColIndex.DiscAmount2, i].Value.ToString());
                                tStock.DiscRupees2 = Convert.ToDouble(dgBill[ColIndex.DiscRupees2, i].Value.ToString());
                                tStock.NetRate = Convert.ToDouble(dgBill[ColIndex.NetRate, i].Value.ToString());
                                tStock.NetAmount = Convert.ToDouble(dgBill[ColIndex.NetAmt, i].Value.ToString());
                                tStock.FkUomNo = Convert.ToInt64(dgBill[ColIndex.UOMNo, i].Value.ToString());
                                tStock.FkRateSettingNo = Convert.ToInt64(dgBill[ColIndex.PkRateSettingNo, i].Value.ToString());
                                tStock.FkItemTaxInfo = Convert.ToInt64(dgBill[ColIndex.PkItemTaxInfo, i].Value.ToString());
                                tStock.FreeQty = 0;
                                tStock.FreeUOMNo = 1;

                                tStock.UserID = DBGetVal.UserID;
                                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsAskUserPassword)) == true)
                                //    tStock.UserID = Utilities.PasswordAsk.UserID == 0 ? DBGetVal.UserID : Utilities.PasswordAsk.UserID;
                                tStock.UserDate = DBGetVal.ServerTime.Date;
                                tStock.CompanyNo = Convert.ToInt64(dgBill[ColIndex.StockCompanyNo, i].Value.ToString());
                                tStock.LandedRate = 0;


                                if (State == true)
                                {
                                    tStock.SGSTPercentage = Convert.ToDouble(dgBill[ColIndex.SGSTPercentage, i].Value.ToString());
                                    tStock.SGSTAmount = Convert.ToDouble(dgBill[ColIndex.SGSTAmount, i].Value.ToString());
                                    tStock.FkItemTaxInfo = Convert.ToInt64(dgBill[ColIndex.PkItemTaxInfo, i].Value.ToString());
                                    tStock.CGSTPercentage = Convert.ToDouble(dgBill[ColIndex.CGSTPercentage, i].Value.ToString());
                                    tStock.CGSTAmount = Convert.ToDouble(dgBill[ColIndex.CGSTAmount, i].Value.ToString());
                                    tStock.FkItemTaxInfo2 = Convert.ToInt64(dgBill[ColIndex.PkItemTaxInfo2, i].Value.ToString());
                                    tStock.IGSTAmount = 0;
                                    tStock.IGSTPercentage = 0;
                                }
                                else
                                {
                                    tStock.SGSTPercentage = 0;
                                    tStock.SGSTAmount = 0;
                                    tStock.CGSTPercentage = 0;
                                    tStock.CGSTAmount = 0;
                                    tStock.IGSTAmount = Convert.ToDouble(dgBill[ColIndex.SGSTAmount, i].Value.ToString());
                                    tStock.IGSTPercentage = Convert.ToDouble(dgBill[ColIndex.SGSTPercentage, i].Value.ToString());
                                }
                                //   tStock.DisplayItemName = dgBill[ColIndex.DisplayName, i].EditedFormattedValue.ToString();
                                tStock.DisplayItemName = "";// dgBill[ColIndex.DisplayName, i].EditedFormattedValue.ToString();
                                tStock.Remarks = dgBill[ColIndex.Remarks, i].EditedFormattedValue.ToString();
                                tStock.GRWeight = 0.00;// (Convert.ToDouble(dgBill[ColIndex.GRWeight, i].Value.ToString()) == null) ? 0.00 : Convert.ToDouble(dgBill[ColIndex.GRWeight, i].Value.ToString());
                                tStock.TRWeight = 0.00;// Convert.ToDouble(dgBill[ColIndex.TRWeight, i].Value.ToString());

                                tStock.Freight = 0.00;// Convert.ToDouble(dgBill[ColIndex.Freight, i].Value.ToString());
                                tStock.OtherCharges = 0.00;// Convert.ToDouble(dgBill[ColIndex.OtherCharges, i].Value.ToString());

                                dbTVoucherEntry.AddTStockES(tStock);
                            }
                        }
                    }
                    #region //for Sales bill tax entry
                    if (DBGetVal.KachhaFirm == false)
                    {
                        //Item Sales Ledger Details
                        DataTable dtSaleLedger = new DataTable();
                        bool TempFlag = false;
                        dtSaleLedger.Columns.Add();
                        dtSaleLedger.Columns.Add();
                        DataRow dr = dtSaleLedger.NewRow();
                        dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.SalesLedgerNo].Value);
                        dr[1] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                        dtSaleLedger.Rows.Add(dr);
                        for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                        {
                            for (int i = 0; i < dtSaleLedger.Rows.Count; i++)
                            {
                                if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.SalesLedgerNo].Value) != Convert.ToInt64(dtSaleLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtSaleLedger.Rows[i].ItemArray[1].ToString()))
                                {
                                    TempFlag = true;

                                }
                                else
                                {
                                    TempFlag = false;
                                    break;
                                }
                            }
                            if (TempFlag == true)
                            {
                                dr = dtSaleLedger.NewRow();
                                dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.SalesLedgerNo].Value);
                                dr[1] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                                dtSaleLedger.Rows.Add(dr);
                            }
                        }

                        int ledgerNo = 0; int cnt = VoucherSrNo - 1, cntLedg = -1, cntTaxLedg = -1;
                        for (int k = 0; k < dtSaleLedger.Rows.Count; k++)
                        {
                            cntLedg = -1;
                            for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                            {
                                if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.SalesLedgerNo].Value) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[1].ToString()))
                                {
                                    if (cntLedg == -1) cntLedg = j;
                                    debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.NetAmt].Value);
                                    dgBill.Rows[j].Cells[ColIndex.SalesVchNo].Value = dgBill.Rows[cntLedg].Cells[ColIndex.SalesVchNo].Value;
                                    ledgerNo = j;
                                }

                            }
                            if (debit > 0)
                            {
                                tVoucherDetails = new TVoucherDetails();
                                if (dtVoucherDetails.Rows.Count > 0)
                                {
                                    //if (Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[1].ToString()) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[0].ToString()))
                                    if (Convert.ToInt64(dgBill.Rows[cntLedg].Cells[ColIndex.SalesVchNo].Value) != 0)
                                    {
                                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[cntLedg].Cells[ColIndex.SalesVchNo].Value);// Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[0].ToString());
                                        cnt++;
                                    }
                                    else
                                    {
                                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                    }
                                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                }
                                else
                                {
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                }
                                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                                tVoucherDetails.SignCode = 2;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.SalesLedgerNo].Value);
                                tVoucherDetails.Debit = debit;
                                tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                                tVoucherDetails.Credit = 0;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                                tVoucherDetails.Narration = "";
                                dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                                debit = 0;
                            }
                        }

                        #region // FIRST TAX SGST
                        //Item Tax Details
                        DataTable dtTAxLedger = new DataTable();
                        TempFlag = false;
                        dtTAxLedger.Columns.Add();
                        dtTAxLedger.Columns.Add();
                        dr = dtTAxLedger.NewRow();
                        dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.TaxLedgerNo].Value);
                        dr[1] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                        dtTAxLedger.Rows.Add(dr);
                        for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                        {
                            TempFlag = false;
                            for (int i = 0; i < dtTAxLedger.Rows.Count; i++)
                            {
                                if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[1].ToString()))
                                {
                                    TempFlag = true;
                                }
                                else
                                {
                                    TempFlag = false;
                                    break;
                                }
                            }
                            if (TempFlag == true)
                            {
                                dr = dtTAxLedger.NewRow();
                                dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo].Value);
                                dr[1] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                                dtTAxLedger.Rows.Add(dr);
                            }
                        }

                        cnt = VoucherSrNo - 1;
                        debit = 0;
                        ledgerNo = 0;
                        for (int k = 0; k < dtTAxLedger.Rows.Count; k++)
                        {
                            cntTaxLedg = -1;
                            for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                            {
                                if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.TaxLedgerNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[1].ToString()))
                                {
                                    if (cntTaxLedg == -1) cntTaxLedg = j;
                                    debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.SGSTAmount].Value);
                                    dgBill.Rows[j].Cells[ColIndex.TaxVchNo].Value = dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo].Value;
                                    ledgerNo = j;
                                }
                            }
                            if (debit != 0)
                            {
                                tVoucherDetails = new TVoucherDetails();
                                if (dtVoucherDetails.Rows.Count > 0)
                                {
                                    if (Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo].Value) != 0)
                                    {
                                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo].Value);// Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[0].ToString());
                                        cnt++;
                                    }
                                    else
                                    {
                                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                    }
                                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                }
                                else
                                {
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                }
                                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                                tVoucherDetails.SignCode = 2;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.TaxLedgerNo].Value);
                                tVoucherDetails.Debit = debit;
                                tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                                tVoucherDetails.Credit = 0;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                                tVoucherDetails.Narration = "";
                                if (State == true)
                                {
                                    tVoucherDetails.SrNo = Others.SGSTLedgerno;
                                }
                                else
                                {

                                    tVoucherDetails.SrNo = Others.IGSTLedgerno;
                                }
                                dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                                debit = 0;
                            }
                        }
                        #endregion
                        #region //Item TAX CGST Second tax
                        //Item Tax Details
                        dtTAxLedger = new DataTable();
                        TempFlag = false;
                        dtTAxLedger.Columns.Add();
                        dtTAxLedger.Columns.Add();
                        dr = dtTAxLedger.NewRow();
                        dr[0] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.TaxLedgerNo2].Value);
                        dr[1] = Convert.ToInt64(dgBill.Rows[0].Cells[ColIndex.StockCompanyNo].Value);
                        dtTAxLedger.Rows.Add(dr);
                        for (int k = 1; k < dgBill.Rows.Count - 1; k++)
                        {
                            TempFlag = false;
                            for (int i = 0; i < dtTAxLedger.Rows.Count; i++)
                            {
                                if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo2].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[1].ToString()))
                                {
                                    TempFlag = true;
                                }
                                else
                                {
                                    TempFlag = false;
                                    break;
                                }
                            }
                            if (TempFlag == true)
                            {
                                dr = dtTAxLedger.NewRow();
                                dr[0] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo2].Value);
                                dr[1] = Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value);
                                dtTAxLedger.Rows.Add(dr);
                            }
                        }

                        cnt = VoucherSrNo - 1;
                        debit = 0;
                        ledgerNo = 0;
                        for (int k = 0; k < dtTAxLedger.Rows.Count; k++)
                        {
                            cntTaxLedg = -1;
                            for (int j = 0; j < dgBill.Rows.Count - 1; j++)
                            {
                                if (Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.TaxLedgerNo2].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[1].ToString()))
                                {
                                    if (cntTaxLedg == -1) cntTaxLedg = j;
                                    debit = debit + Convert.ToDouble(dgBill.Rows[j].Cells[ColIndex.CGSTAmount].Value);
                                    dgBill.Rows[j].Cells[ColIndex.TaxVchNo2].Value = dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo2].Value;
                                    ledgerNo = j;
                                }
                            }
                            if (debit != 0)
                            {
                                tVoucherDetails = new TVoucherDetails();
                                if (dtVoucherDetails.Rows.Count > 0)
                                {
                                    if (Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo2].Value) != 0)
                                    {
                                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dgBill.Rows[cntTaxLedg].Cells[ColIndex.TaxVchNo2].Value);// Convert.ToInt64(dtVoucherDetails.Rows[cnt].ItemArray[0].ToString());
                                        cnt++;
                                    }
                                    else
                                    {
                                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                    }
                                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                                }
                                else
                                {
                                    tVoucherDetails.PkVoucherTrnNo = 0;
                                }
                                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                                tVoucherDetails.SignCode = 2;
                                tVoucherDetails.LedgerNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.TaxLedgerNo2].Value);
                                tVoucherDetails.Debit = debit;
                                tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                                tVoucherDetails.Credit = 0;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
                                tVoucherDetails.Narration = "";
                                if (State == true)
                                {
                                    tVoucherDetails.SrNo = Others.CGSTLedgerno;
                                }
                                else
                                {
                                    tVoucherDetails.SrNo = Others.CessLedgerno;

                                }
                                dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                                debit = 0;
                            }
                        }

                        #endregion

                    }
                    #endregion
                    #region //other entry
                    //For Scheme Discount
                    if (Convert.ToDouble(txtSchemeDisc.Text) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Discount1));
                        tVoucherDetails.Debit = 0;//(Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtSchemeDisc.Text)) / 10;
                        tVoucherDetails.Credit = Convert.ToDouble(txtSchemeDisc.Text);
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Discount2;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                        //}
                    }

                    //For Discount Ledger 1 %
                    if (Convert.ToDouble(txtDiscRupees1.Text) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Discount1));
                        tVoucherDetails.Debit = 0; // (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtDiscRupees1.Text)) / 10;
                        tVoucherDetails.Credit = Convert.ToDouble(txtDiscRupees1.Text);
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Discount1;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                        //}
                    }

                    //For Footer Other  disc
                    if (Convert.ToDouble(txtOtherDisc.Text) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Discount1));
                        tVoucherDetails.Debit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtOtherDisc.Text)) / 10;
                        tVoucherDetails.Credit = Convert.ToDouble(txtOtherDisc.Text);
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Discount3;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                        //}
                        //long FooterDiscDtlsNo = 0;
                        tFooterDisc = new TFooterDiscountDetails();
                        if (ID == 0)
                        {
                            tFooterDisc.PkSrNo = 0;
                            tFooterDisc.FooterDiscNo = ObjQry.ReturnLong("Select FooterDiscNo From MFooterDiscountDetails Where PkSrNo=" + FooterDiscDtlsNo + "", CommonFunctions.ConStr);
                            tFooterDisc.FooterDiscDetailsNo = FooterDiscDtlsNo;
                        }
                        else
                        {
                            DataTable dtDisc = ObjFunction.GetDataView("Select PkSrNo,FooterDiscNo,FooterDiscDetailsNo From TFooterDiscountDetails Where FKVoucherNo=" + ID + "").Table;
                            if (dtDisc.Rows.Count > 0)
                            {
                                tFooterDisc.PkSrNo = Convert.ToInt64(dtDisc.Rows[0].ItemArray[0].ToString());
                                if (FooterDiscDtlsNo == Convert.ToInt64(dtDisc.Rows[0].ItemArray[2].ToString()))
                                {
                                    tFooterDisc.FooterDiscNo = Convert.ToInt64(dtDisc.Rows[0].ItemArray[1].ToString());
                                    tFooterDisc.FooterDiscDetailsNo = Convert.ToInt64(dtDisc.Rows[0].ItemArray[2].ToString());
                                }
                                else
                                {
                                    tFooterDisc.FooterDiscNo = ObjQry.ReturnLong("Select FooterDiscNo From MFooterDiscountDetails Where PkSrNo=" + FooterDiscDtlsNo + "", CommonFunctions.ConStr);
                                    tFooterDisc.FooterDiscDetailsNo = FooterDiscDtlsNo;
                                }
                            }
                            else
                            {
                                tFooterDisc.FooterDiscNo = ObjQry.ReturnLong("Select FooterDiscNo From MFooterDiscountDetails Where PkSrNo=" + FooterDiscDtlsNo + "", CommonFunctions.ConStr);
                                tFooterDisc.FooterDiscDetailsNo = FooterDiscDtlsNo;
                            }
                        }
                        tFooterDisc.DiscAmount = Convert.ToDouble(txtOtherDisc.Text);
                        tFooterDisc.CompanyNo = DBGetVal.FirmNo;
                        tFooterDisc.UserID = DBGetVal.UserID;
                        tFooterDisc.UserDate = DBGetVal.ServerTime;
                        dbTVoucherEntry.AddTFooterDiscountDetails(tFooterDisc);
                    }

                    //=========Debit Entrys=========================
                    //For Charges Rupees 1
                    if (Convert.ToDouble(txtChrgOnTaxAmt.Text) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Charges1));
                        tVoucherDetails.Debit = Convert.ToDouble(txtChrgOnTaxAmt.Text);
                        tVoucherDetails.Credit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Charges1;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                        //}
                    }
                    //For Charges Rupees 2
                    if (Convert.ToDouble(txtChrgRupees2.Text) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_Charges2));
                        tVoucherDetails.Debit = Convert.ToDouble(txtChrgRupees2.Text);
                        tVoucherDetails.Credit = 0;// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Charges2;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                        //}
                    }


                    //For Round Off Acc Ledger
                    if (Convert.ToDouble(txtRoundOff.Text) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RoundOfAcc));
                        if (Convert.ToDouble(txtRoundOff.Text) >= 0)
                        {
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.Debit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));
                            tVoucherDetails.Credit = 0; // (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtRoundOff.Text)) / 10;
                        }
                        else
                        {
                            tVoucherDetails.SignCode = 1;
                            tVoucherDetails.Debit = 0; // (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Math.Abs(Convert.ToDouble(txtRoundOff.Text))) / 10;
                            tVoucherDetails.Credit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));
                        }
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.RoundOff;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                        //}
                    }
                    #endregion
                }
                #endregion

                long tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    pnlBillSize = false;
                    viewmode();

                    string strVChNo = ObjQry.ReturnLong("Select VoucherUserNo From TVoucherEntry Where PKVoucherNo=" + tempID + "", CommonFunctions.ConStr).ToString();

                    DisplayMessage("Bill No " + strVChNo + " Added Successfully");

                    ID = tempID;
                    FillField();
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    //rbEnglish.Enabled = true;
                    //rbMarathi.Enabled = true;
                    btnCashSave.Visible = false;
                    btnCreditSave.Visible = false;
                    //btnCCSave.Visible = false;
                    btnSearch.Visible = true;
                    //  btnPrint.Visible = true;
                    dgBill.Enabled = false;
                    btnNew.Focus();
                    // }

                }
                else
                {
                    OMMessageBox.Show("Bill Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {

            lblGrandTotal.Text = lblGrandTotal.Text;

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

        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where TVoucherEntry.taxtypeno=1 and  VoucherTypeCode=" + VoucherType + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["PkVoucherNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where TVoucherEntry.taxtypeno=1 and  VoucherTypeCode=" + VoucherType + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["PkVoucherNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where   TVoucherEntry.taxtypeno=1 and  PkVoucherNo >" + ID + " AND  VoucherTypeCode=" + VoucherType + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["PkVoucherNo"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
                else if (type == 4)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where TVoucherEntry.taxtypeno=1 and   PkVoucherNo <" + ID + " AND  VoucherTypeCode=" + VoucherType + " ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["PkVoucherNo"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            if (ID != 0)
            {
                FillField();
            }
        }
  
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsCreditBillUpdate)) == false)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBillUpdate)) == true)
                    {
                        Form frm = new Utilities.PasswordAsk(2);
                        ObjFunction.OpenForm(frm);
                        if (Utilities.PasswordAsk.IsAllow == 0) return;
                    }
                    if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                    {
                        btnUpdate.Visible = false;
                        btnBillCancel.Visible = false;
                        //  btnMixMode.Visible = false;
                        OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + " and TR.TypeOfRef in(6))", CommonFunctions.ConStr) > 1)
                        {
                            btnUpdate.Visible = false;
                            btnBillCancel.Visible = false;
                            //  btnMixMode.Visible = false;
                            //BillFlag = false;
                            OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            return;
                        }

                    }

                    btnCashSave.Visible = true;
                    btnCreditSave.Visible = true;
                }
                else
                {
                    int PAYTYPENO = Convert.ToInt32(lstPaymentType.SelectedValue);
                    if (PAYTYPENO == 3)
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                        {
                            Form frm = new Utilities.PasswordAsk(1);
                            ObjFunction.OpenForm(frm);
                            if (Utilities.PasswordAsk.CreditID == 0) return;


                            txtPaymentType.Enabled = false;
                            btnCreditSave.Visible = false;
                            btnCashSave.Visible = false;
                        }
                        else
                        {
                            btnCashSave.Visible = true;
                            btnCreditSave.Visible = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsBillUpdate)) == true)
                        {
                            Form frm = new Utilities.PasswordAsk(2);
                            ObjFunction.OpenForm(frm);
                            if (Utilities.PasswordAsk.IsAllow == 0) return;
                        }
                    }
                }
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtInvNo.Enabled = false;
                btnBillCancel.Enabled = false;
                if (pnlBillSize == true)
                {
                    pnlBill.Visible = true;
                    dgBill.Top = dgBill.Top + (pnlBill.Height + 2);
                    dgBill.Height = dgBill.Height - 78;
                    pnlBillSize = false;
                }
                else
                {
                    pnlBillSize = true;

                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_RateTypeAskPassword)) == true)
                    //cmbRateType.Enabled = false;
                    btnSearch.Visible = false;
                // btnPrint.Visible = false;
                dgBill.Enabled = true;
                dgBill.Focus();
                btnBillCancel.Visible = false;
                //  btnMixMode.Visible = false;
                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                txtSchemeDisc.Enabled = false;
                // txtOtherDisc.Enabled = false;
                //  txtChrgRupees2.Enabled = false;
                txtPaymentType.Enabled = false;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_IsCreditBillUpdate)) == true)
                {
                    if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                    {

                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }
        public void BindGridPayType(long ID)
        {
            try
            {
                DataTable dtPayType = new DataTable();
                dtPayLedger = ObjFunction.GetDataView("Select * From MPayTypeLedger Where PayTypeNo in(2,3,4,5)").Table;
                string sqlQuery = "";
                if (ID == 0)
                    
                    sqlQuery = "SELECT PayTypeName, PKPayTypeNo, Cast(0.00 as varchar) AS Amount,0 As LedgerNo, 0 AS PKVoucherPayTypeNo FROM MPayType where isactive='true' ORDER BY PKPayTypeNo";
                else
                    sqlQuery = "SELECT PayTypeName, PKPayTypeNo,Cast( IsNull((SELECT SUM(Amount) FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) AS varchar) AS Amount,0 As LedgerNo, 0 AS PKVoucherPayTypeNo FROM MPayType  ORDER BY PKPayTypeNo";

                dtPayType = ObjFunction.GetDataView(sqlQuery).Table;
                while (dgPayType.Columns.Count > 0)
                    dgPayType.Columns.RemoveAt(0);
                dgPayType.DataSource = dtPayType.DefaultView;
                for (int i = 0; i < dgPayType.Columns.Count; i++)
                    dgPayType.Columns[i].Visible = false;
                dgPayType.Columns[0].Visible = true;
                dgPayType.Columns[2].Visible = true;
                dgPayType.Rows[0].Visible = false;
                dgPayType.Columns[0].Width = 150;
                dgPayType.Columns[2].Width = 100;
                dgPayType.Columns[0].ReadOnly = true;
                dgPayType.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgPayType.Rows[0].Visible = false;
                if (ID != 0)
                {
                    if (ObjFunction.GetListValue(lstPaymentType) == 1 || ObjFunction.GetListValue(lstPaymentType) == 4 || ObjFunction.GetListValue(lstPaymentType) == 5)
                    {
                        dgPayType.Rows[3].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND ChequeNo <>''", CommonFunctions.ConStr).ToString("0.00");
                        dgPayType.Rows[4].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND CreditCardNo <>''", CommonFunctions.ConStr).ToString("0.00");
                    }

                    double RefAmt = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherRefDetails Where FKVucherTrnNo in (Select PKVoucherTrnNo From TVoucherDetails Where FkVoucherNo=" + ID + ")", CommonFunctions.ConStr);
                    if (RefAmt > 0)
                    {
                        dgPayType.Rows[2].Cells[2].Value = RefAmt;
                    }
                }
                CaluculatePayType();
                pnlPartial.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CaluculatePayType()
        {
            double TotAmt = 0;
            for (int i = 0; i < dgPayType.Rows.Count; i++)
            {
                if (dgPayType.Rows[i].Cells[2].Value == null) dgPayType.Rows[i].Cells[2].Value = "0";
                TotAmt += Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value);
            }
            txtTotalAmt.Text = TotAmt.ToString("0.00");
            if (lblGrandTotal.Text != "")
                lblPayTypeBal.Text = (Convert.ToDouble(lblGrandTotal.Text) - TotAmt).ToString("0.00");
            else
                lblPayTypeBal.Text = "0.00";
        }

        public void FillPayType()
        {
            try
            {
                int cntflag = 0;
                for (int i = 0; i < dgPayType.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value) > 0)
                        cntflag += 1;
                    if (cntflag > 1) break;
                }
                if (cntflag > 1) lstPaymentType.SelectedValue = "1";

                long PayType = ObjFunction.GetListValue(lstPaymentType);
                //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                //{
                //    dgPayType.Columns.Add(dtCompRatio.Rows[i].ItemArray[0].ToString(), dtCompRatio.Rows[i].ItemArray[0].ToString());
                //    dgPayType.Columns[dgPayType.Columns.Count - 1].Visible = false;
                //}

                if (PayType != 1)
                {
                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                    {
                        if (PayType == Convert.ToInt64(dgPayType.Rows[i].Cells[1].Value))
                        {
                            dgPayType.Rows[i].Cells[2].Value = lblGrandTotal.Text;

                            dgPayType.Rows[i].Cells[4].Value = Convert.ToDouble(lblGrandTotal.Text);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                    {
                        dgPayType.Rows[i].Cells[4 + (1)].Value = Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public bool SaveReceipt(long SalesID)
        {
            DataTable dtPayType = new DataTable();
            int cntPayType = 1;
            long tempid = -1, ReceiptID = 0, VoucherUserNo = 0;

            for (int row = 1; row <= dgPayType.Rows.Count - 1; row++)
            {
                if (row == 1 || row == 3 || row == 4 || row > 4)
                {
                    if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                    {
                        if (row == 1 || row == 3 || row == 4 || row > 4)
                            ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                              " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherEntry.VoucherDate ='" + dtpBillDate.Text + "') AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ") AND " +
                                " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + ") ", CommonFunctions.ConStr);
                        if (CancelFlag == true)
                        {
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + 0 + " order by VoucherSrNo").Table;
                            // setCompanyRatio();
                            FillPayType();
                        }
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                            " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
                        long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);
                        double PrevAmt = 0;
                        for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                            PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
                        PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt) + ((CancelFlag == true) ? -Convert.ToDouble(lblGrandTotal.Text) : Convert.ToDouble(lblGrandTotal.Text));
                        //int VoucherSrNo = 1;
                        dbTVoucherEntry = new DBTVaucherEntry();
                        //Voucher Header Entry
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = ReceiptID;
                        tVoucherEntry.VoucherTypeCode = VchType.Receipt;
                        tVoucherEntry.VoucherUserNo = VoucherUserNo;
                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                        tVoucherEntry.VoucherTime = dtpBillTime.Value;
                        tVoucherEntry.Narration = "Receipt Bill";
                        tVoucherEntry.Reference = "";
                        tVoucherEntry.ChequeNo = 0;
                        tVoucherEntry.ClearingDate = Convert.ToDateTime("01-Jan-1900");
                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                        tVoucherEntry.BilledAmount = (ReceiptID != 0) ? PrevAmt : Convert.ToDouble(lblGrandTotal.Text);
                        tVoucherEntry.ChallanNo = "";
                        tVoucherEntry.Remark = txtRemark.Text.Trim();
                        tVoucherEntry.MacNo = DBGetVal.MacNo;
                        tVoucherEntry.PayTypeNo = ObjFunction.GetListValue(lstPaymentType);
                        tVoucherEntry.RateTypeNo = 0;
                        tVoucherEntry.TaxTypeNo = 0;
                        tVoucherEntry.OrderType = 2;
                        tVoucherEntry.TransporterCode = 0;
                        tVoucherEntry.TransPayType = 0;
                        tVoucherEntry.LRNo = "";
                        tVoucherEntry.TransportMode = 0;
                        tVoucherEntry.TransNoOfItems = 0;

                        tVoucherEntry.UserID = DBGetVal.UserID;
                        tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

                        DataTable dtVoucherDetails = new DataTable();
                        if (ReceiptID != 0)
                        {
                            dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where FkVoucherNo=" + ReceiptID + " order by VoucherSrNo").Table;
                            dtPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo From TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=" + dgPayType.Rows[row].Cells[1].Value + "  order by PKVoucherPayTypeNo").Table;
                        }
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,TD.LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails TD,TVoucherEntry TC Where TC.PKVoucherNo=TD.FKVoucherNo AND TC.PayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + " AND TD.LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " AND  TD.FkVoucherNo=" + ReceiptID + " AND TD.CompanyNo=" + DBGetVal.FirmNo + " order by TD.VoucherSrNo").Table;

                        double totamt = 0, amt = 0;

                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                        {

                            amt = Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                            totamt = totamt + amt;
                        }
                        amt = 0; if (CancelFlag == true) totamt = -totamt;
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);

                        //For Party Ledger
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0)
                            tVoucherDetails.VoucherSrNo = VoucherSrNo;
                        else tVoucherDetails.VoucherSrNo = Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[4].ToString());
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.LedgerNo = ObjFunction.GetListValue(lstPartyEnglish);
                        tVoucherDetails.Debit = 0;

                        double Newval = 0;
                        for (int k = 0; k < dgBill.Rows.Count - 1; k++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) == DBGetVal.FirmNo)//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())
                            {
                                if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.PkStockTrnNo].Value) == 0)
                                {
                                    Newval = Newval + Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Amount].Value);
                                }
                            }
                        }
                        totamt = totamt - Newval;
                        if (ID == 0)
                            tVoucherDetails.Credit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString()) : 0) + Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                        else
                        {
                            if (dtVoucherDetails.Rows.Count > 0)
                                tVoucherDetails.Credit = Newval + ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString()) - amt) + totamt);
                            else
                            {
                                if (PayType != 3)
                                    tVoucherDetails.Credit = totamt;
                                else tVoucherDetails.Credit = totamt + Newval;
                            }
                        }
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Party;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                        totamt = 0;
                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                            totamt += Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails Where LedgerNo=" + Convert.ToInt64(dtPayLedger.Select("PayTypeNo='" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + "' AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString()) + " AND  FkVoucherNo=" + ReceiptID + " AND CompanyNo=" + DBGetVal.FirmNo + " order by VoucherSrNo").Table;

                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0)
                            tVoucherDetails.VoucherSrNo = VoucherSrNo;
                        else tVoucherDetails.VoucherSrNo = Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[4].ToString());
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(dtPayLedger.Select("PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + " AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString());// Convert.ToInt64(dgPayType.Rows[row].Cells[3].Value);//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())
                        Newval = 0;
                        for (int k = 0; k < dgBill.Rows.Count - 1; k++)
                        {
                            if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) == DBGetVal.FirmNo)//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())
                            {
                                if (Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.PkStockTrnNo].Value) == 0)
                                {
                                    Newval = Newval + Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Amount].Value);
                                }
                            }
                        }
                        totamt = totamt - Newval;
                        amt = 0; if (CancelFlag == true) totamt = -totamt;
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);

                        if (ID == 0)
                            tVoucherDetails.Debit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) : 0) + Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                        else
                        {
                            if (dtVoucherDetails.Rows.Count > 0)
                                tVoucherDetails.Debit = Newval + ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) - amt) + totamt);
                            else
                            {
                                if (PayType != 3)
                                    tVoucherDetails.Debit = totamt;
                                else tVoucherDetails.Debit = totamt + Newval;
                            }
                        }

                        tVoucherDetails.Credit = 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);




                        tVchPayTypeDetails = new TVoucherPayTypeDetails();
                        tVchPayTypeDetails.PKVoucherPayTypeNo = (dtPayType.Rows.Count > cntPayType - 1) ? Convert.ToInt64(dtPayType.Rows[cntPayType - 1].ItemArray[0].ToString()) : 0; cntPayType += 1;
                        tVchPayTypeDetails.FKSalesVoucherNo = SalesID;
                        tVchPayTypeDetails.FKPayTypeNo = Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value);
                        tVchPayTypeDetails.Amount = Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                        tVchPayTypeDetails.ChargesPerce = Convert.ToDouble(dgPayType.Rows[row].Cells[6].Value);
                        tVchPayTypeDetails.ChargesAmount = Convert.ToDouble(dgPayType.Rows[row].Cells[7].Value);
                        tVchPayTypeDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherPayTypeDetails(tVchPayTypeDetails);
                        //}

                        if ((PartyNo != ObjFunction.GetListValue(lstPartyEnglish) || PayType != ObjFunction.GetListValue(lstPaymentType)) && ID != 0)
                        {
                            DataTable dtDelPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo,TVoucherPayTypeDetails.CompanyNo,FKReceiptVoucherNo,Amount From TVoucherPayTypeDetails,TVoucherDetails Where TVoucherDetails.PkVoucherTrnNo=TVoucherPayTypeDetails.FKVoucherTrnNo AND FKSalesVoucherNo=" + SalesID + "  order by PKVoucherPayTypeNo").Table;
                            for (int k = 0; k < dtDelPayType.Rows.Count; k++)
                            {
                                if (dtPayType.Rows.Count > 0)
                                {
                                    if (dtPayType.Select("PKVoucherPayTypeNo=" + dtDelPayType.Rows[k].ItemArray[0].ToString())[0][0].ToString() != dtDelPayType.Rows[k].ItemArray[0].ToString())
                                    {
                                        tVchPayTypeDetails = new TVoucherPayTypeDetails();
                                        tVchPayTypeDetails.PKVoucherPayTypeNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[0].ToString());
                                        dbTVoucherEntry.DeleteTVoucherPayTypeDetails(tVchPayTypeDetails);
                                    }
                                }
                                else
                                {
                                    tVchPayTypeDetails = new TVoucherPayTypeDetails();
                                    tVchPayTypeDetails.PKVoucherPayTypeNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[0].ToString());
                                    dbTVoucherEntry.DeleteTVoucherPayTypeDetails(tVchPayTypeDetails);
                                }
                                DataTable dtUpdateVoucher = ObjFunction.GetDataView("Select PKVoucherTrnNo,Debit,Credit From TVoucherDetails Where FKVoucherNo=" + dtDelPayType.Rows[k].ItemArray[2].ToString() + " AND CompanyNo=" + dtDelPayType.Rows[k].ItemArray[1].ToString() + "").Table;
                                totamt = 0;
                                bool alllowdel = false;
                                for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                                {
                                    double DrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[1].ToString());
                                    double CrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[2].ToString());
                                    if (DrAmt > 0) DrAmt = DrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                                    if (CrAmt > 0) CrAmt = CrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                                    dbTVoucherEntry.UpdateVoucherDetails(DrAmt, CrAmt, Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString()));
                                    totamt = totamt + DrAmt + CrAmt;
                                    alllowdel = true;
                                }
                                if (totamt == 0 && alllowdel == true)
                                {
                                    for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                                    {
                                        tVoucherDetails = new TVoucherDetails();
                                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString());
                                        dbTVoucherEntry.DeleteTVoucherDetails(tVoucherDetails);

                                        if (m == dtUpdateVoucher.Rows.Count - 1)
                                        {
                                            if (ObjQry.ReturnLong("Select Count(*) From TVoucherDetails Where FKVoucherNo=" + Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[2].ToString()) + "", CommonFunctions.ConStr) >= dtUpdateVoucher.Rows.Count)
                                            {
                                                tVoucherEntry = new TVoucherEntry();
                                                tVoucherEntry.PkVoucherNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[2].ToString());
                                                dbTVoucherEntry.DeleteTVoucherEntry1(tVoucherEntry);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        tempid = dbTVoucherEntry.ExecuteNonQueryStatements();
                    }
                }

            }
            CancelFlag = false;

            if (tempid != 0)
                return true;
            else
                return false;
        }
        private void btnBillCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    //   btnMixMode.Visible = false;
                    //BillFlag = false;
                    OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + " and TR.TypeOfRef in(6))", CommonFunctions.ConStr) > 1)
                    {
                        btnUpdate.Visible = false;
                        btnBillCancel.Visible = false;
                        // btnMixMode.Visible = false;
                        //BillFlag = false;
                        OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                    }
                    else if (ObjQry.ReturnInteger("SELECT  COUNT(*) FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                    " WHERE     (TVoucherEntry.Reference = " + txtInvNo.Text + ") AND (TVoucherEntry.VoucherTypeCode = " + VchType.RejectionIn + ") AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ")", CommonFunctions.ConStr) > 0)
                    {
                        btnBillCancel.Visible = false;
                        //BillFlag = false;
                        OMMessageBox.Show("Already this bill is returned.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                    }
                }
              
                if (OMMessageBox.Show("Are you sure you want to cancel this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    CancelFlag = true;
                    //  SaveReceipt(ID);
                    dbTVoucherEntry = new DBTVaucherEntry();
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = ID;
                    dbTVoucherEntry.CancelTVoucherEntry(tVoucherEntry);


                    OMMessageBox.Show("Bill No. " + txtInvNo.Text + " cancel successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //SetNavigation();

                    //  setDisplay(true);
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    //   rbEnglish.Enabled = true;
                    //rbMarathi.Enabled = true;
                    dgBill.Enabled = false;

                    FillField();


                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstRateType_SelectedValueChanged(object sender, EventArgs e)
        {
            ChangeBillRate();
        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Home)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    txtBarCode.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                

                    RewardDeleteFlag = false;
                    delete_row();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null &&
                        (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value.ToString() != "0") == true)//&& Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_AfterSaveNotDeleteItem))) == true)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                        return;
                    }

                    //dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.SrNo)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentCell.RowIndex];
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value == null)
                        {
                            dgBill.CurrentCell.Value = "";
                            Desc_Start();
                        }
                        else if (Convert.ToInt64(dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value) == 0)
                        {
                            dgBill.CurrentCell.Value = "";
                            Desc_Start();
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {
                            if (dgBill.CurrentCell.Value == null) dgBill.CurrentCell.Value = "1.000";
                            CalculateTotal();
                            //else dgBill.CurrentCell.Value = Convert.ToDouble(dgBill.CurrentCell.Value.ToString()).ToString(Format.ThreeFloating);
                            if (dgBill.CurrentCell.Value != null) dgBill.CurrentCell.Value = Convert.ToDouble(dgBill.CurrentCell.Value.ToString()).ToString(Format.ThreeFloating);
                            //Qty_MoveNext();
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value != null && dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value.ToString() != "")
                        {
                            UOM_Start();

                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {
                            //  Rate_MoveNext();
                            CalculateTotal();
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {
                            dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscRupees)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {

                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        }
                    }
                }

                else if (e.KeyCode == Keys.F8)
                {
                    if (dgBill.CurrentCell.Value != null)
                        dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                    else
                    {
                        if (dgBill.CurrentCell.RowIndex == 0)
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                        else
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex - 1];
                    }
                }
                else if (e.KeyCode == Keys.F7)
                {
                    if (btnSave.Visible == true)
                    {
                        //DisplayStockGodown();
                        if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value != null)
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                        else
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex - 1];
                    }
                }
                else if (e.KeyCode == Keys.F6)
                {
                    if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "")
                    {
                        e.SuppressKeyPress = true;
                        //  txtDisplayName.Text = dgBill.CurrentRow.Cells[ColIndex.DisplayName].EditedFormattedValue.ToString().Trim();
                        pnlDisplayName.Location = new Point(100, 154);
                        pnlDisplayName.Visible = true;
                        txtDisplayName.Focus();
                    }
                }
                else if (e.KeyCode == Keys.S && e.Control)
                {
                }
                else if (e.KeyCode == Keys.Space)
                {
                    txtBarCode.Focus();
                }
                else if (e.KeyCode == Keys.R && e.Control)
                {
                }
                else if (e.KeyCode == Keys.U && e.Control)
                {
                    if (dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value != null && dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value.ToString().Trim() != "")
                    {
                        if (OMMessageBox.Show("Are you sure you want to change UOM.?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            long ItemUOMNo = Convert.ToInt64(dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value);
                            if (ItemUOMNo == 1) ItemUOMNo = 2;
                            else ItemUOMNo = 1;
                            dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = "0";
                            dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = ItemUOMNo;
                            UOM_Start();
                        }
                    }
                }
                else if (e.KeyCode == Keys.F9)
                {
                    e.SuppressKeyPress = true;
                    // cmbRateType.Focus();
                }
                else if (e.KeyCode == Keys.V && e.Control)
                {
                    e.SuppressKeyPress = true;
                    dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = Clipboard.GetText(TextDataFormat.Text);

                }
                else if (e.KeyCode == Keys.F10)
                {
                    e.SuppressKeyPress = true;
                    txtBarCode.Focus();
                }
                else if (e.KeyCode == Keys.PageUp)
                {
                    if (dgBill.Rows.Count > 0)
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, 0];
                        dgBill.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Insert)
                {
                    if (dgBill.Rows.Count > 0)
                    {
                        if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                        {
                            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value;
                            dgBill.Focus();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {
                            CalculateTotal();
                            dgBill.Focus();
                        }

                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                    {
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {

                            CalculateTotal();
                        }

                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {

                    txtSchemeDisc.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void delete_row()
        {
            try
            {
                bool flag;
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null)
                {
                    //if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() == "")
                    //{
                    if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                        if (PKStockTrnNo != 0)
                        {
                            DeleteDtls(1, PKStockTrnNo);
                            DeleteDtls(11, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value));
                            //if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() != "" && dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() != "0")
                            //{
                            //    DeleteDtls(9, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FKItemLevelDiscNo].Value));
                            //    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DiscRupees].Value = "0.00";
                            //    //ItemLevelDisc = true;
                            //}
                            flag = false;
                            for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                            {
                                if (dgBill.CurrentCell.RowIndex != i)
                                {
                                    if (Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkVoucherNo].Value) == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value))
                                    { flag = true; break; }
                                }
                            }
                            if (flag == false)
                                DeleteDtls(2, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkVoucherNo].Value));

                            //For Sales LedgerNo
                            flag = false;
                            for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                            {
                                if (dgBill.CurrentCell.RowIndex != i)
                                {
                                    if (Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SalesVchNo].Value) == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.SalesVchNo].Value))
                                    { flag = true; break; }
                                }
                            }
                            if (flag == false)
                                DeleteDtls(2, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SalesVchNo].Value));

                            //FOr TaxLedgerNo
                            flag = false;
                            for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                            {
                                if (dgBill.CurrentCell.RowIndex != i)
                                {
                                    if (Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TaxVchNo].Value) == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.TaxVchNo].Value))
                                    { flag = true; break; }
                                }
                            }
                            if (flag == false)
                                DeleteDtls(2, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TaxVchNo].Value));
                        }

                        if (SOSelection.dtSOMain != null && dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FkOtherStockTrnNo].Value != null)
                        {
                            int SOIndex = -1;
                            for (int i = 0; i < SOSelection.dtSOMain.Rows.Count; i++)
                            {
                                if (SOSelection.dtSOMain.Rows[i].ItemArray[SOSelection.ColIndex.FKOtherStockTrnNo].ToString() == dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FkOtherStockTrnNo].Value.ToString() &&
                                    SOSelection.dtSOMain.Rows[i].ItemArray[SOSelection.ColIndex.Quantity].ToString() == dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value.ToString())
                                {
                                    SOIndex = i;
                                    break;
                                }
                            }
                            if (SOIndex > -1)
                                SOSelection.dtSOMain.Rows.RemoveAt(SOIndex);
                        }

                        if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                        {
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            dgBill.Rows.Add();
                        }
                        else
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        //dtBillCollect.RemoveAt(dgBill.CurrentCell.RowIndex);//umesh new yadi//

                        txtOtherDisc.Text = Format.DoubleFloating;
                        txtSchemeDisc.Text = Format.DoubleFloating;
                        FooterDiscDtlsNo = 0;
                        CalculateTotal();
                        if (dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemNo].Value != null)
                        {
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.Rows.Count - 1];
                            dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                        }
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                    }

                    //}
                    //else
                    //    OMMessageBox.Show("This item already asssigned to scheme. Not allowed to delete this item.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                else
                {
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].EditedFormattedValue.ToString() != "")
                    {
                        if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                        {
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            dgBill.Rows.Add();
                        }
                        else
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        dgBill.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #region Delete code
        public void InitDelTable()
        {
            dtDelete.Columns.Add();
            dtDelete.Columns.Add();
        }

        public void DeleteDtls(int Type, long PkNo)
        {
            DataRow dr = null;
            dr = dtDelete.NewRow();
            dr[0] = Type;
            dr[1] = PkNo;
            dtDelete.Rows.Add(dr);
        }

        public void DeleteValues()
        {
            if (dtDelete != null)
            {
                for (int i = 0; i < dtDelete.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
                    {
                        tStock.PkStockTrnNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTStock(tStock);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 2)
                    {
                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTVoucherDetails(tVoucherDetails);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 3)
                    {
                        tVoucherDetails.CompanyNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        tVoucherDetails.FkVoucherNo = ID;
                        dbTVoucherEntry.DeleteTVoucherDetailsCompany(tVoucherDetails);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 4)
                    {
                        tStockGodown.PKStockGodownNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTStockGodown(tStockGodown);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 9)
                    {
                        tItemLevelDisc.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTItemLevelDiscountDetails(tItemLevelDisc);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 10)
                    {
                        tFooterDisc.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTFooterDiscountDetails(tFooterDisc);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 11)
                    {
                        TPackingListDetails tpld = new TPackingListDetails();
                        tpld.ItemNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        tpld.FkVoucherNo = ID;
                        dbTVoucherEntry.DeleteTParkingBillDetails(tpld);
                    }

                }
                dtDelete.Rows.Clear();
            }
        }


        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlPartySearch.Visible == false)
                {
                    pnlSearch.Visible = true;
                    pnlSearch.Enabled = true;
                    txtSearch.Text = ""; txtSearch.Enabled = true;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                        ObjFunction.FillCombo(cmbPartyNameSearch, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ")  and MLedger.Ledgerno in (select Ledgerno from TVoucherEntry where vouchertypecode=" + VoucherType + ")order by LedgerName");
                    else
                        ObjFunction.FillComb(cmbPartyNameSearch, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryCreditors + "))and MLedger.Ledgerno in (select Ledgerno from TVoucherEntry where vouchertypecode=" + VoucherType + ") ORDER BY LedgerName ");

                    txtSearch.Focus();
                    btnNew.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnBillCancel.Enabled = false;
                    rbDocNo.Checked = true;
                    rbType_CheckedChanged(rbDocNo, null);
                    dgPartySearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            rbType(true);
        }

        public void rbType(bool IsSetFocus)
        {
            try
            {
                rbDocNo.Enabled = true;
                rbPartyName.Enabled = true;
                rbDate.Enabled = true;
                dtpSearchDate.Visible = false;
                if (rbDocNo.Checked == true)
                {
                    if (IsSetFocus)
                    {
                        lblLable.Visible = true;
                        lblLable.Text = "Inv No :";
                        txtSearch.Width = 162;
                        txtSearch.Location = new System.Drawing.Point(90, 39);
                        txtSearch.Visible = true;
                        cmbPartyNameSearch.Visible = false;
                        btnPartyName.Visible = false;
                        txtSearch.Focus();
                    }
                }
                else if (rbPartyName.Checked == true)
                {
                    if (IsSetFocus)
                    {
                        btnPartyName.Enabled = true;
                        cmbPartyNameSearch.Enabled = true;
                        btnPartyName.Visible = true;
                        lblLable.Visible = true;
                        lblLable.Text = "Party Name :";
                        cmbPartyNameSearch.Width = 250;
                        cmbPartyNameSearch.Location = new System.Drawing.Point(90, 39);
                        btnPartyName.Location = new System.Drawing.Point((90 + 250 + 5), 39);
                        cmbPartyNameSearch.Visible = true;
                        txtSearch.Visible = false;
                        cmbPartyNameSearch.Focus();
                    }
                }
                else if (rbDate.Checked == true)
                {
                    if (IsSetFocus)
                    {
                        dtpSearchDate.Enabled = true;
                        cmbPartyNameSearch.Visible = false;
                        btnPartyName.Visible = false;
                        lblLable.Visible = true;
                        lblLable.Text = "Date :";
                        dtpSearchDate.Location = new System.Drawing.Point(90, 39);
                        txtSearch.Visible = false;
                        dtpSearchDate.Visible = true;
                        dtpSearchDate.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnBillCancel.Enabled = true;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                long tempNo;
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where TVoucherEntry.taxtypeno=1 and   VoucherUserNo=" + txtSearch.Text + " and VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr) > 1)
                    {
                        pnlInvSearch.Visible = true;
                        int x = dgBill.GetCellDisplayRectangle(0, 0, true).X + 200;//(Screen.PrimaryScreen.WorkingArea.Width) / 2;
                        int y = dgBill.GetCellDisplayRectangle(0, 0, true).Y + 100;
                        //pnlPartySearch.SetBounds(x, y, dgPartySearch.Width + 10, dgPartySearch.Height + 10);
                        pnlInvSearch.Location = new Point(x, y);
                        string str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #], convert(varchar(11),TVoucherEntry.VoucherDate,105) as 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                     "TVoucherEntry.PkVoucherNo FROM TVoucherEntry WHERE (TVoucherEntry.VoucherTypeCode IN (" + VchType.Sales + "," + VchType.SalesOrder + ")) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
                                     "And TVoucherEntry.VoucherUserNo=" + txtSearch.Text + " " +
                                     "Order By  TVoucherEntry.VoucherDate desc,TVoucherEntry.VoucherUserNo desc, TVoucherEntry.Reference desc";
                        dgInvSearch.DataSource = ObjFunction.GetDataView(str).Table.DefaultView;
                        dgInvSearch.Columns[0].Width = 50;
                        dgInvSearch.Columns[1].Width = 150;
                        dgInvSearch.Columns[2].Width = 80;
                        dgInvSearch.Columns[3].Width = 110;
                        dgInvSearch.Columns[3].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                        dgInvSearch.Columns[3].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                        dgInvSearch.Columns[4].Visible = false;
                        if (dgInvSearch.RowCount > 0)
                        {
                            pnlInvSearch.Focus();
                            SearchVisible(false);
                            pnlSearch.Visible = false;
                            e.SuppressKeyPress = true;
                            dgInvSearch.Focus();
                            dgInvSearch.CurrentCell = dgInvSearch[0, dgInvSearch.CurrentRow.Index];
                        }
                        txtSearch.Text = "";
                        cmbPartyNameSearch.SelectedIndex = 0;
                        return;
                    }
                    tempNo = ObjQry.ReturnLong("Select PKVoucherNo From TVoucherEntry Where TVoucherEntry.taxtypeno=1 and   VoucherUserNo=" + txtSearch.Text + " and VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        // NavigationDisplay();
                        FillField();

                        pnlSearch.Visible = false;
                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnBillCancel.Enabled = true;
                        SearchVisible(false);
                    }
                    else
                    {
                        pnlSearch.Visible = false;
                        DisplayMessage("Bill Not Found");
                        Application.DoEvents();
                        pnlSearch.Visible = true;
                        cmbPartyNameSearch.SelectedIndex = 0;
                        rbDocNo.Focus();
                        txtSearch.Focus();
                        txtSearch.Text = "";
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlSearch.Visible = false;
                    pnlPartySearch.Visible = false;
                    pnlInvSearch.Visible = false;
                    btnCancelSearch_Click(sender, new EventArgs());
                    btnNew.Focus();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    e.SuppressKeyPress = true;
                    rbPartyName.Checked = true;
                    rbType_CheckedChanged(rbPartyName, null);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void SearchVisible(bool flag)
        {
            txtSearch.Visible = flag;
            cmbPartyNameSearch.Visible = flag;
            btnPartyName.Visible = flag;
            lblLable.Visible = flag;
        }

        private void cmbPartyNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (pnlPartySearch.Visible == true)
                    {
                        pnlPartySearch.Visible = false;
                        pnlSearch.Visible = true;
                    }
                    else
                    {

                        pnlPartySearch.Visible = true;
                        int x = dgBill.GetCellDisplayRectangle(0, 0, true).X + 200;//(Screen.PrimaryScreen.WorkingArea.Width) / 2;
                        int y = dgBill.GetCellDisplayRectangle(0, 0, true).Y + 100;
                        //pnlPartySearch.SetBounds(x, y, dgPartySearch.Width + 10, dgPartySearch.Height + 10);
                        pnlPartySearch.Location = new Point(x, y);
                        string str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #],convert(varchar(11),TVoucherEntry.VoucherDate,105) as 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                     "TVoucherEntry.PkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo WHERE TVoucherEntry.taxtypeno=1 and  (TVoucherEntry.VoucherTypeCode IN (" + VchType.Purchase + ")) AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
                                     "And TVoucherDetails.LedgerNo=" + ObjFunction.GetComboValue(cmbPartyNameSearch) + " " +
                                     "Order By  TVoucherEntry.VoucherUserNo desc,TVoucherEntry.VoucherDate desc, TVoucherEntry.Reference desc";
                        dgPartySearch.DataSource = ObjFunction.GetDataView(str).Table.DefaultView;
                        dgPartySearch.Columns[0].Width = 50;
                        dgPartySearch.Columns[1].Width = 150;
                        dgPartySearch.Columns[2].Width = 80;
                        dgPartySearch.Columns[3].Width = 110;
                        dgPartySearch.Columns[3].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                        dgPartySearch.Columns[3].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                        dgPartySearch.Columns[4].Visible = false;
                        if (dgPartySearch.RowCount > 0)
                        {
                            pnlPartySearch.Focus();
                            SearchVisible(false);
                            pnlSearch.Visible = false;
                            e.SuppressKeyPress = true;
                            dgPartySearch.Focus();
                            dgPartySearch.CurrentCell = dgPartySearch[0, dgPartySearch.CurrentRow.Index];
                            lblSearchName.Text = "Party Name: " + cmbPartyNameSearch.Text;
                            lblSearchName.Font = new Font("Verdana", 11, FontStyle.Bold);
                        }
                        else
                        {
                            pnlPartySearch.Visible = false;
                            pnlSearch.Visible = false;
                            DisplayMessage("Bill Not Found");
                            pnlSearch.Visible = true;
                            rbPartyName.Focus();
                            rbType_CheckedChanged(rbPartyName, null);
                        }
                        txtSearch.Text = "";
                        cmbPartyNameSearch.SelectedIndex = 0;

                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlSearch.Visible = false;
                    pnlPartySearch.Visible = false;
                    btnCancelSearch_Click(sender, new EventArgs());
                    btnNew.Focus();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    rbDocNo.Checked = true;
                    rbType_CheckedChanged(rbDocNo, null);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPartySearch_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long tempNo;
                    e.SuppressKeyPress = true;
                    tempNo = ObjQry.ReturnLong("Select PKVoucherNo From TVoucherEntry Where TVoucherEntry.taxtypeno=1 and   PkVoucherNo=" + Convert.ToInt64(dgPartySearch.Rows[dgPartySearch.CurrentRow.Index].Cells[4].Value) + " and VoucherTypeCode=" + VoucherType + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        // SetNavigation();
                        FillField();
                        btnNew.Enabled = true;
                        btnBillCancel.Enabled = true;
                        btnUpdate.Enabled = true;
                        pnlPartySearch.Visible = false;
                        btnNew.Focus();
                        SearchVisible(false);
                    }
                    else
                    {
                        txtSearch.Text = "";
                        //cmbPartyName.SelectedIndex = 0;
                        DisplayMessage("Bill Not Found");
                        txtSearch.Focus();
                        //SearchVisible(false);
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlPartySearch.Visible = false;
                    pnlSearch.Visible = true;
                    txtSearch.Focus();
                    rbType_CheckedChanged(sender, new EventArgs());
                }
                txtSearch.Text = "";
                cmbPartyNameSearch.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnCancelSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
            //pnlSearch.Visible = false;
            //btnNew.Enabled = true;
            //btnUpdate.Enabled = true;
            //btnBillCancel.Enabled = true;
            //btnCancelSearch_Click(sender, new EventArgs());
            //btnNew.Focus();
            e.SuppressKeyPress = true;
            pnlSearch.Visible = false;
            pnlPartySearch.Visible = false;
            pnlInvSearch.Visible = false;
            btnCancelSearch_Click(sender, new EventArgs());
            btnNew.Focus();
        }

        private void PurchaseAPMCAE_Activated(object sender, EventArgs e)
        {
            updateFlag = true;
        }

        private void txtNoOfBag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNoOfBag.Text == null)
                    txtNoOfBag.Text = "0.00";
                CalculateSubTotal();
                btnGOk.Focus();
            }
        }

        private void dtpBillDate_ValueChanged(object sender, EventArgs e)
        {
            FillBrand();
        }


        private void txtBQuantity_TextChanged(object sender, EventArgs e)
        {
            if (txtBQuantity.Text != "" && txtBQuantity.Text[0].ToString() == ".")
            {
                txtBQuantity.Text = "0.";
                txtBQuantity.SelectionStart = txtBQuantity.Text.Length;
                txtBQuantity.SelectionLength = 0;
            }
            ObjFunction.SetMasked((TextBox)sender, 3, 9, OMFunctions.MaskedType.PositiveNegative);
        }

        private void txtBRate_TextChanged(object sender, EventArgs e)
        {
            if (txtBRate.Text != "" && txtBRate.Text[0].ToString() == ".")
            {
                txtBRate.Text = "0.";
                txtBRate.SelectionStart = txtBRate.Text.Length;
                txtBRate.SelectionLength = 0;
            }
            ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void txtBDiscPer1_TextChanged(object sender, EventArgs e)
        {
            if (txtBDiscPer1.Text != "" && txtBDiscPer1.Text[0].ToString() == ".")
            {
                txtBDiscPer1.Text = "0.";
                txtBDiscPer1.SelectionStart = txtBDiscPer1.Text.Length;
                txtBDiscPer1.SelectionLength = 0;
            }
            ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void txtBDiscAmt1_TextChanged(object sender, EventArgs e)
        {

            ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.PositiveNegative);
        }

        private void txtBDiscRs1_TextChanged(object sender, EventArgs e)
        {

            if (txtBDiscRs1.Text != "" && txtBDiscRs1.Text[0].ToString() == ".")
            {
                txtBDiscRs1.Text = "0.";
                txtBDiscRs1.SelectionStart = txtBDiscRs1.Text.Length;
                txtBDiscRs1.SelectionLength = 0;
            }
            ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void txtBAmount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);

        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            try
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = (e.RowIndex + 1).ToString();

                }
                if (e.ColumnIndex == ColIndex.ItemName)
                {
                    //if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "")
                    //{
                    //    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "0")
                    //        dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
                    //}

                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PONo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.PONo].Value.ToString() != "")
                        if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PONo].Value.ToString() != "0")
                            dgBill.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;

                }
                if (dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value.ToString() != "")
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value.ToString() != "0")
                    {
                        dgBill.Rows[e.RowIndex].ReadOnly = false;
                        dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName].ReadOnly = true;
                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstPaymentType_SelectedValueChanged(object sender, EventArgs e)
        {
            iPayTypeControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + "", CommonFunctions.ConStr);

        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave.Focus();
            }
        }

        private void lstArea_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    // e.SuppressKeyPress = true;
                    txtArea.Text = lstArea.Text;
                    txtArea.Focus();
                    pnlArea.Visible = false;

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtRefNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtRefNo_Leave(txtRefNo, new EventArgs());
                //  e.SuppressKeyPress = true;
            }
        }

        private void txtRefNo_Leave(object sender, EventArgs e)
        {
            if (txtRefNo.Text == "")
            {
                OMMessageBox.Show("Enter Invoice No.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtRefNo.Focus();
            }
            else
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnParty)) == true) txtParty.Focus();
                else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.P_StopOnTaxType)) == true) cmbLocation.Focus();
                else txtBarCode.Focus();
            }
        }

        private void cmbLocation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtBarCode.Focus();
            }
        }

        private void lstBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgPayChqDetails.Visible == true)
                    {
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value = lstBank.Text;
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[6].Value = lstBank.SelectedValue;
                        pnlBank.Visible = false;

                        dgPayChqDetails.Focus();
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[4, dgPayChqDetails.CurrentCell.RowIndex];
                        //}
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[1].Value = lstBank.Text;
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[5].Value = lstBank.SelectedValue;
                        pnlBank.Visible = false;

                        dgPayCreditCardDetails.Focus();
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        //}
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (dgPayChqDetails.Visible == true)
                    {
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value = "";
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[6].Value = 0;
                        pnlBank.Visible = false;
                        if (dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value == null)
                        {
                            pnlBranch.Visible = true;
                            pnlBranch.BringToFront();
                            lstBranch.Focus();
                        }
                        else
                        {
                            dgPayChqDetails.Focus();
                            dgPayChqDetails.CurrentCell = dgPayChqDetails[3, dgPayChqDetails.CurrentCell.RowIndex];
                        }
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[1].Value = "";
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[5].Value = 0;
                        pnlBank.Visible = false;
                        if (dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[2].Value == null)
                        {
                            pnlBranch.Visible = true;
                            pnlBranch.BringToFront();
                            lstBranch.Focus();
                        }
                        else
                        {
                            dgPayCreditCardDetails.Focus();
                            dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            try
            {
                bool flag = true;
                if (Convert.ToDouble(dgPayType.Rows[3].Cells[2].Value) > 0)
                {
                    if (dgPayChqDetails.Rows[0].Cells[0].Value == null || dgPayChqDetails.Rows[0].Cells[1].FormattedValue.ToString().Trim() == "" || dgPayChqDetails.Rows[0].Cells[2].FormattedValue.ToString().Trim() == "")
                    {
                        OMMessageBox.Show("Please Fill Cheque Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        flag = false;
                    }
                    else flag = true;
                }
                if (flag == true)
                {
                    if (Convert.ToDouble(dgPayType.Rows[4].Cells[2].Value) > 0)
                    {
                        if (dgPayCreditCardDetails.Rows[0].Cells[0].FormattedValue.ToString().Trim() == "" || dgPayCreditCardDetails.Rows[0].Cells[1].FormattedValue.ToString().Trim() == "")
                        {
                            OMMessageBox.Show("Please Fill Credit Card Details.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            flag = false;
                        }
                        else flag = true;
                    }
                }
                if (flag == true)
                {
                    if (Convert.ToDouble(txtTotalAmt.Text) != Convert.ToDouble(lblGrandTotal.Text))
                    {
                        OMMessageBox.Show("TOTAL AMOUNT EXCEEDS TO BILL AMOUNT.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnSave.Focus();
                        pnlPartial.Visible = false;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtSchemeDisc_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                // txtSchemeDisc.Text=(txtSchemeDisc.Text ) ==null ) ? 1 : 2;
                if (txtSchemeDisc.Text == null)
                    txtSchemeDisc.Text = "0.00";
                CalculateTotal();
                txtDiscount1.Focus();
            }
        }

        private void txtDiscount1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscount1.Text == null)
                    txtDiscount1.Text = "0.00";
                CalculateTotal();
                txtDiscRupees1.Focus();
            }
        }

        private void txtDiscRupees1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtDiscRupees1.Text == null)
                    txtDiscRupees1.Text = "0.00";
                CalculateTotal();
                txtOtherDisc.Focus();
            }
        }

        private void txtOtherDisc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtOtherDisc.Text == null)
                    txtOtherDisc.Text = "0.00";
                CalculateTotal();
                txtChrgRupees2.Focus();
            }
        }

        private void txtChrgRupees1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtChrgOnTaxAmt.Text == null)
                    txtChrgOnTaxAmt.Text = "0.00";
                CalculateTotal();
                txtChrgRupees2.Focus();
            }
        }

        private void txtChrgRupees2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (txtChrgRupees2.Text == null)
                    txtChrgRupees2.Text = "0.00";
                CalculateTotal();
                txtRemark.Focus();
            }
        }

        private void btnCancelSearch_Click_1(object sender, EventArgs e)
        {
            //e.SuppressKeyPress = true;
            pnlSearch.Visible = false;
            pnlPartySearch.Visible = false;
            pnlInvSearch.Visible = false;
            btnCancelSearch_Click(sender, new EventArgs());
            btnNew.Focus();
        }

        private void txtBCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToDouble(txtBCharges.Text).ToString("0.00") != "0.00")
                { CalculateSubTotal(); }
                btnGOk.Focus();
            }
        }

        private void txtBCharges_TextChanged(object sender, EventArgs e)
        {
            if (txtBCharges.Text != "" && txtBCharges.Text[0].ToString() == ".")
            {
                txtBCharges.Text = "0.";
                txtBCharges.SelectionStart = txtBCharges.Text.Length;
                txtBCharges.SelectionLength = 0;
            }
            ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
        }


        private void dgBill_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBox txt = (TextBox)e.Control;
                txt.KeyDown += new KeyEventHandler(txtSpace_KeyDown);
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);

                }
              
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                {
                    TextBox txtrate = (TextBox)e.Control;
                    txtrate.TextChanged += new EventHandler(txtRate_TextChanged);
                }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    TextBox txtDisc = (TextBox)e.Control;
                    txtDisc.TextChanged += new EventHandler(txtDisc_TextChanged);
                }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscRupees)
                {
                    TextBox txtDisc = (TextBox)e.Control;
                    txtDisc.TextChanged += new EventHandler(txtDisc_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtSpace_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    Spaceflag = false;
                    if (dgBill.CurrentCell.RowIndex == 0)
                    {
                        if (dgBill.CurrentCell.ColumnIndex != 0)
                            dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex - 1, dgBill.CurrentCell.RowIndex];
                    }
                    else
                    {

                        if (dgBill.CurrentCell.ColumnIndex == 1)
                            dgBill.CurrentCell = dgBill[4, dgBill.CurrentCell.RowIndex - 1];
                        else if (dgBill.CurrentCell.ColumnIndex != 0)
                            dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex - 1, dgBill.CurrentCell.RowIndex];
                    }
                }

                TextBox txt = (TextBox)sender;
                txt.KeyDown -= new KeyEventHandler(txtSpace_KeyDown);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) && Convert.ToDouble(((TextBox)sender).Text) > 999999.99)
                    {
                        OMMessageBox.Show("Please enter valid value on Rate field." + Environment.NewLine + "Press Escape to continue..", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Text = (DBCellValue.Rate == "") ? "0" : DBCellValue.Rate;

                    }
                    ObjFunction.SetMasked((TextBox)sender, 2, 7, OMFunctions.MaskedType.NotNegative);
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    ObjFunction.SetMasked((TextBox)sender, 2, 6, OMFunctions.MaskedType.PositiveNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtDisc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    ObjFunction.SetMasked((TextBox)sender, 2, 3, OMFunctions.MaskedType.NotNegative);
                }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscRupees)
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) && Convert.ToDouble(((TextBox)sender).Text) > 99999.99)
                    {
                        OMMessageBox.Show("Please enter valid value on Disc. field." + Environment.NewLine + "Press Escape to continue..", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Text = (DBCellValue.Disc == "") ? "0" : DBCellValue.Disc;

                    }
                    ObjFunction.SetMasked((TextBox)sender, 2, 5, OMFunctions.MaskedType.NotNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }






    }
}

