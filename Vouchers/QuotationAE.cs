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
using System.IO;
using Yadi.DBClasses;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Diagnostics;

namespace Yadi.Vouchers
{
    public partial class QuotationAE : Form
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
        TReward tReward = new TReward();
        TRewardDetails tRewardDetails = new TRewardDetails();
        TRewardFrom tRewardFrom = new TRewardFrom();
        TRewardTo tRewardTo = new TRewardTo();
        TDeliveryAddress tdeliveryaddress = new TDeliveryAddress();
        MSchemeAchieverDetails mSchAchDtls = new MSchemeAchieverDetails();
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
        int rowQtyIndex, UserType = 0;
        long LedgerDetailsNo = 0;
        bool IsDeliveryAddress = false;
        bool IsCancel = false;
        long Companycode, statecode, BItemNo = 0;//Ledgerno = 0,
        double  PartyDiscPerceItemLevel = 0.0;
        long iPayTypeControlUnder = 0;
        bool Spaceflag = true, BillSizeFlag = false, State = true;
        public bool RewardDeleteFlag = false, RewardFlag = false, DiscFlag = false;
        long ItemNameType = 0, RateTypeNo, PrintAsk, PartyNo, PayType, ParkBillNo, OrderType = 0, FooterDiscDtlsNo = 0;/*bcdno,*/
        int iItemNameStartIndex = 3, ItemType = 0, MixModeVal = 0;
        string Param1Value = "", Param2Value = "", strSql = "";
        string[] strItemQuery, strItemQuery_last;
        bool ShowVATNo, HistoryMaintain = false;
        public int IsPrintCount = 1;
        double subTotal = 0.00, totalDisc = 0.00, totalChrg = 0.00, totalTax = 0.00, TaxPerce2 = 0.00, TaxPerce3 = 0.00, TotalTaxPerce = 0.00;
        double tAmount = 0.00, TotFinal = 0.00, Disc1 = 0.00, Disc2 = 0.00, TaxAmt = 0.00, TotalAmt = 0.00, TaxAmt2 = 0, ttRate = 0, ttax = 0, TaxAmt3 = 0;

        bool QutFlag = false, ManualBill = false;
        string strFillcmbPartyName = "";
        DateTime tempDate; long tempPartyNo = 0, TempBillNo = 0;
        DataTable dtOrderType; DataTable dtRateSetting;

        DataTable dt = new DataTable();
        DataTablesCollection dtBillCollect = new DataTablesCollection();
        DateTime dtFrom, dtTo;

        long VoucherUserNo;
        bool flagParking = false;
        bool StopOnQty = false, StopOnRate = false;
        public long RequestSalesNo, ID, VoucherType, No, PID;
        bool isSavingTransaction = false;
        long txtPkRateSettingNo = 0;

        DataTable dtTRewardToFrom;
        DataTable dtTRewardDtls;
        PartialPayment partialPay = new PartialPayment();
        PartialPaymentAdjust partialPayAdjust = new PartialPaymentAdjust();
        long subUomno = 0;

        DataTable Subdt = new DataTable();
        double SubQty = 0, SubRate = 0, SubDiscPer = 0, SubDiscRs = 0, SubTax1 = 0, SubTax2 = 0, SubTax3 = 0, SubMktQty = 0;
        public static string EmailRptName = "";
        List<string> Path = new List<string>();
        public QuotationAE()
        {
            InitializeComponent();
        }

        public QuotationAE(long ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void QuotationAE_Load(object sender, EventArgs e)
        {
            try
            {
                VoucherType = VchType.Quotation;
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                btnNew.Focus();
                FillList();
                FillRateType();
                dgItemList.Visible = true;
                InitDelTable();

                dtOrderType = ObjFunction.GetDataView("Select OrderTypeNo,OrderTypeName,ColorName From MOrderType").Table;
                RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType));
                ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType));
                StopOnQty = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnQty));
                StopOnRate = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnRate));
                initItemQuery();
                InitControls();
                btnShortcut.Visible = true;
                btnBigPrint.Visible = true;
                KeyDownFormat(this.Controls);
               
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSalesMan)) == true)
                {
                    lstSalesMan.SelectedValue = 1;
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                {
                    strFillcmbPartyName = "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") and IsActive='true' order by LedgerName";
                    Area.Visible = false;
                    txtArea.Visible = false;
                }
                else
                {
                    strFillcmbPartyName = "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryDebtors + ")) AND (MLedger.IsActive = 'true') ORDER BY LedgerName ";
                }
                btnDeliveryAddress.Visible = false;
                ID = ObjQry.ReturnLong("Select max(PkVoucherNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType + " ", CommonFunctions.ConStr);
                if (ID != 0)
                {
                    FillField();
                }
                else
                {
                    btnFirst.Enabled = false; btnPrev.Enabled = false; btnNext.Enabled = false; btnLast.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnCancel.Enabled = false;
                    btnSearch.Enabled = false;
                    btnMixMode.Enabled = false;
                    btnAdvanceSearch.Visible = false;
                    btnBillCancel.Enabled = false;
                    btnPrint.Enabled = false;
                    btnCCSave.Enabled = false;
                    btnCashSave.Enabled = false;
                    dgBill.Enabled = true;
                }
                DataTable dtSettings = ObjFunction.GetDataView("Select PKSettingNo From MSettings Where SettingTypeNo=4").Table;
                for (int i = 0; i < dtSettings.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(Convert.ToInt32(dtSettings.Rows[i].ItemArray[0].ToString()))) == true)
                        dgBill.Columns[i].Visible = true;
                    else
                        dgBill.Columns[i].Visible = false;
                }
                txtGrandTotal.Font = new Font("Verdana", 18, FontStyle.Bold);
                txtGrandTotal.ForeColor = Color.Black;

                lblGrandTotal.Font = new Font("Verdana", 22, FontStyle.Bold);
                lblGrandTotal.ForeColor = Color.White;
                lblitemcount.Font = new Font("Verdana", 15, FontStyle.Bold);

                label65.Font = new Font("Verdana", 15, FontStyle.Bold);
                lblCreditLimit.Font = new Font("Verdana", 9, FontStyle.Bold);
                lblOutstanding.Font = new Font("Verdana", 13, FontStyle.Bold);
                lblLastBillAmt.Font = new Font("Verdana", 9, FontStyle.Bold);

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
                DisplayChargANDDisc();

                dtpBillDate.CustomFormat = "dd-MMM-yy"; dtpBillDate.Width = 90;
                dtpDueDate.CustomFormat = "dd-MMM-yy"; dtpDueDate.Width = 90;
                dgBill.BackgroundColor = Color.FromArgb(242, 242, 242);
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == false)
                {
                    pnlPrinting.Visible = false;
                    rbEnglish.Checked = true;
                    dgItemList.Columns[2].Visible = false;
                }
                else
                {
                    if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_DefaultBillPrint)) == 1)
                        rbEnglish.Checked = true;
                    else
                        rbMarathi.Checked = true;

                    if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 2)
                        rbMarathi.Text = "marazI";
                    else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 3)
                        rbMarathi.Text = "ihMdI";
                }
                pnlPrinting.Enabled = true;
                rbEnglish.Enabled = true;
                rbMarathi.Enabled = true;

                txtSchemeDisc.Enabled = false;
                txtOtherDisc.Enabled = false;
                txtChrgRupees2.Enabled = false;
                InitItemLeveDiscount();

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
                lblChrg1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges1Display));
                lblChrg1.Text = ObjFunction.GetAppSettings(AppSettings.S_ChargeLabelName).ToString() + ":";
                txtChrgRupees1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges1Display));

                lblChrg2.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges2Display));
                lblChrg2.Text = ObjFunction.GetAppSettings(AppSettings.S_Charge2LabelName).ToString() + ":";
                txtChrgRupees2.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges2Display));

                lblDisc1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display));
                txtDiscount1Per.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display));
                txtDiscRupees1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display));
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
            pnlDisplayName.Visible = false;
            pnlParking.Visible = false;
            pnlPartyName.Visible = false;
        }

        public void FilldgDeliveryAddress()
        {
            try
            {
                string sql = "";
                sql = "SELECT     MLedgerDetails.LedgerDetailsNo, MLedgerDetails.LedgerNo, MLedgerDetails.Address, MArea.AreaNo, MArea.AreaName, MCity.CityNo, MCity.CityName,MLedgerDetails.PinCode, MLedgerDetails.PhNo1, MLedgerDetails.MobileNo1  , '' as selected               " +
                      " MCity ON MLedgerDetails.CityNo = MCity.CityNo WHERE(LedgerNo = " + PartyNo + ") Union all " +
                      "SELECT     MLedgerDetails.LedgerDetailsNo, MLedgerDetails.LedgerNo, MLedgerDetails.Address, MArea.AreaNo, MArea.AreaName, MCity.CityNo, MCity.CityName,MLedgerDetails.PinCode, MLedgerDetails.PhNo1, MLedgerDetails.MobileNo1  , '' as selected               " +
                      " FROM MLedgerDetails left JOIN    MArea ON MLedgerDetails.AreaNo = MArea.AreaNo left JOIN " +
                      " MCity ON MLedgerDetails.CityNo = MCity.CityNo WHERE(DeliveryLedgerNo = " + PartyNo + ")";
                DataView dv = new DataView();
                DataSet ds = new DataSet();

                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);

                dv = ds.Tables[0].DefaultView;
                dgDeliveryAddress.DataSource = dv;
                dgDeliveryAddress.Columns[0].Visible = false;
                dgDeliveryAddress.Columns[1].Visible = false;
                dgDeliveryAddress.Columns[3].Visible = false;
                dgDeliveryAddress.Columns[5].Visible = false;

                if (ID != 0)
                {
                    long deliveryno = ObjQry.ReturnLong("select LedgerDetailsNo from TDeliveryAddress where FkVoucherno =" + ID, CommonFunctions.ConStr);
                    for (int i = 0; i <= dgDeliveryAddress.Rows.Count - 1; i++)
                    {
                        if ((deliveryno == Convert.ToInt32(dgDeliveryAddress.Rows[i].Cells[0].Value)) && (deliveryno != 0))
                        {
                            dgDeliveryAddress.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FormatPics()
        {
            try
            {
                pnlItemName.Width = 1000;
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
                    pnlGroup1.Left = 50;
                    pnlGroup1.Width = 1000;

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
                    lstPartyLang.Height = pnlParty.Height - 10;
                    lstPartyEnglish.Height = pnlParty.Height - 10;
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
                pnlGroup1.Height = 350;

                lstUOM.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                pnlUOM.Top = txtBUom.Bottom + 70;
                pnlUOM.Left = txtBUom.Left;
                pnlUOM.Width = txtBUom.Width + 50;
                pnlUOM.Height = 60;

                pnlRate.Top = 150;
                pnlRate.Left = 430;
                pnlRate.Width = 120;
                pnlRate.Height = 80;

                btnMixMode.Left = btnExit.Left;
                btnMixMode.Top = btnExit.Top + btnExit.Height + 5;

                pnlTransporter.Location = new Point(31, 134);
                pnlMultiPrint.Location = new Point(168, 99);
                pnlSearch.Location = new Point(148, 236);
                pnlPartySearch.Location = new Point(158, 94);
                pnlParking.Location = new Point(140, 174);
                pnlRateType.Location = new Point(205, 206);
                pnlMainParking.Location = new Point(205, 269);
                pnlPartyName.Location = new Point(205, 262);
                pnlInvSearch.Location = new Point(711, 128);
                pnlTotalAmt.Location = new Point(790, 238);
                lblMsg.Location = new Point(254, 217);          
                lblState.Font = ObjFunction.GetFont(FontStyle.Bold, 16);

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
                dtItemQuery = ObjFunction.GetDataView("SELECT * from MItemNameDisplayType WHERE ItemNameTypeNo = 2").Table;


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
                //EnrFlag = false;
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
                flagParking = false;
                MixModeVal = 0;
                PrintAsk = 0;
                VoucherUserNo = 0;
                OrderType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_OrderType));
                ShowVATNo = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowVatNo));
                FillRateType();
                SetRateType(RateTypeNo);

                txtRateType.Text = lstRateType.Text;
                lblCreditLimit.Text = "";
                dtBillCollect = new DataTablesCollection();           

                dtpBillDate.Value = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD).ToString()).Date;
                dtpDueDate.Value= System.DateTime.Now;
                dtpBillTime.Value = System.DateTime.Now;
                dtpBillTime.Format = DateTimePickerFormat.Time;
                lblCancelBll.Text = "";
                lblitemcount.Text = "0.00";

                dgBill.Rows.Add();
                ObjFunction.GetFinancialYear(dtpBillDate.Value, out dtFrom, out dtTo);
                txtInvNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND VoucherDate>='" + dtFrom.Date + "' AND VoucherDate<='" + dtTo.Date + "'", CommonFunctions.ConStr) + 1).ToString();

                lstPartyEnglish.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_DefaultPartyAC));
                txtParty.Text = lstPartyEnglish.Text;
                PartyNo = Convert.ToInt32(lstPartyEnglish.SelectedValue);

                statecode = ObjQry.ReturnLong("Select stateCode from Mledger  where Ledgerno = " + PartyNo + " ", CommonFunctions.ConStr);
                string StateName = lblState.Text = ObjQry.ReturnString("Select Statename from mstate  where stateCode = " + statecode + " ", CommonFunctions.ConStr);
                lblState.Text = lblState.Text.ToString().ToUpper();

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSalesMan)) == true)
                {
                    lblSalesMan.Visible = true;
                    txtSalesMan.Visible = true;
                }
                else
                {
                    lblSalesMan.Visible = false;
                    txtSalesMan.Visible = false;
                }   

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDate)) == true) dtpBillDate.Focus();
                else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true)
                {
                    if (btnNew.Visible == false)
                    {
                        pnlParty.Visible = true;
                        lstPartyEnglish.Focus();
                    }
                }
                else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnRateType)) == true) txtRateType.Focus();
                else dgBill.Focus();

                dgBill.CurrentCell = dgBill[1, 0];

                txtDiscount1Per.Text = "0.00";
                txtDiscRupees1.Text = "0.00";
                txtChrgRupees1.Text = "0.00"; txtChrgRupees2.Text = "0.00"; //txtChrgOnTaxAmt.Text = "0.00"; txtChrgTax.Text = "0.00";
                txtSubTotal.Text = "0.00"; lblBillItem.Text = "0"; lblBilExchangeItem.Text = "0";
                txtGrandTotal.Text = "0.00"; txtTotalDisc.Text = "0.00"; txtTotalTax.Text = "0.00";
                txtSchemeDisc.Text = "0.00"; txtOtherDisc.Text = "0.00"; txtRoundOff.Text = "0.00";
                txtSchemeDisc.Enabled = false; txtOtherDisc.Enabled = false; txtChrgRupees2.Enabled = false;
                btnBillCancel.Visible = DBGetVal.IsAdmin;
                ParkBillNo = 0;
                dtTRewardDtls = ObjFunction.GetDataView("SELECT PkSrNo, RewardNo, SchemeNo, SchemeDetailsNo, SchemeType, DiscPer, DiscAmount, SchemeAmount,0 As Status,SchemeAcheiverNo FROM TRewardDetails WHERE (PkSrNo = 0)").Table;
                dtTRewardToFrom = ObjFunction.GetDataView("SELECT 0 AS TypeNo,PkSrNo, RewardNo, RewardDetailsNo,SchemeDetailsNo, SchemeFromNo, FkStockNo,0 As 'ItemNo' FROM TRewardFrom WHERE (PkSrNo = 0)").Table;
                tempDate = dtpBillDate.Value.Date;

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
                {
                    lstPartyEnglish.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
                    lstPartyLang.Font = ObjFunction.GetLangFont();
                    ObjFunction.FillList(lstPartyLang, "Select LedgerNo,LedgerLangName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") and MLedger.IsActive='true' and ledgerno not in (24) order by LedgerName");
                }
                ObjFunction.FillList(lstPartyEnglish, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") and MLedger.IsActive='true' and ledgerno not in (24) order by LedgerName");
            }
            else
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    lstPartyEnglish.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
                    lstPartyLang.Font = ObjFunction.GetLangFont();
                    ObjFunction.FillList(lstPartyLang, "SELECT MLedger.LedgerNo, MLedger.LedgerLangName  + ' ' + ISNULL(Mcity.CityLangname, '') + ' ' + ISNULL(MArea.AreaLangName, '') AS LedgerNam,MLedger.LedgerName + ' ' + ISNULL(Mcity.CityName, '')  + '-' + ISNULL(MArea.AreaName, '') AS LedgerName1   FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo  left outer join mcity on mcity.cityno=mledgerdetails.cityno  LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo   WHERE (MLedger.GroupNo IN (26) and MLedger.ledgerno not in (24)) and MLedger.IsActive='true' ORDER BY LedgerName1  ");
                }
                ObjFunction.FillList(lstPartyEnglish, "SELECT MLedger.LedgerNo, MLedger.LedgerName + ' ' + ISNULL(Mcity.Cityname, '') + '-' + ISNULL(MArea.AreaName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo  left outer join mcity on mcity.cityno=mledgerdetails.cityno  LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (26) and MLedger.ledgerno not in (24)) and MLedger.IsActive='true' ORDER BY LedgerName ");
            }
            Companycode = ObjQry.ReturnLong("select Statecode from Mfirm ", CommonFunctions.ConStr);

            ObjFunction.FillList(lstArea, "Select AreaNo,AreaName From Marea  Where  IsActive='true' order by AreaName");
             { ObjFunction.FillList(lstSalesMan, "Select SalesmanNo,SalesmanName From MSalesman  Where  IsActive='true' order by SalesmanName"); }

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
                dtpDueDate.Value = tVoucherEntry.EffectiveDate;
                dtpBillTime.Value = tVoucherEntry.VoucherTime;
                tVoucherEntry.PkRefNo = tVoucherEntry.PkRefNo;
                PayType = tVoucherEntry.PayTypeNo;
                PartyNo = Convert.ToInt32(tVoucherEntry.LedgerNo);
                txtCustCode.Text = PartyNo.ToString();
                tempDate = dtpBillDate.Value.Date;
                PID = tVoucherEntry.PkRefNo;
                FillRateType(tVoucherEntry.RateTypeNo);
                SetRateType(tVoucherEntry.RateTypeNo);
                txtRateType.Text = lstRateType.Text;
                MixModeVal = tVoucherEntry.MixMode;
                txtSubTotal.Text = "0.00";
                txtTotalDisc.Text = "0.00";
                txtDiscount1Per.Text = "0.00";
                txtDiscRupees1.Text = "0.00";
                txtTotalDisc.Text = "0.00";
                txtTotalTax.Text = "0.00";
                txtChrgRupees1.Text = "0.00"; txtChrgRupees2.Text = "0.00";
                txtGrandTotal.Text = "0.00";
                txtOtherDisc.Text = "0.00";
                txtSchemeDisc.Text = "0.00";
                OrderType = tVoucherEntry.OrderType;
                txtRemark.Text = tVoucherEntry.Remark;
                DataTable dt = ObjFunction.GetDataView("Select Case When Debit<>0 then Debit Else Credit End,LedgerNo,SrNo From TVoucherDetails Where FKVoucherNo=" + ID + " order by VoucherSrNo").Table;

                double subTot = ObjQry.ReturnDouble("Select sum(Case When(SrNo<>508 AND SrNo<>506)Then Debit else -Credit End) from TVoucherDetails  Where FKVoucherNo=" + ID + " ", CommonFunctions.ConStr);
                txtSubTotal.Text = subTot.ToString();
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                {
                    //  ObjFunction.FillList(lstPartyEnglish, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") and (IsActive='true' or LedgerNo = " + partyNo + ") order by LedgerName");
                }
                else
                {
                    long areano = ObjQry.ReturnLong("select areano from marea where areano in(select areano from MLedgerDetails where ledgerno=" + PartyNo + ")", CommonFunctions.ConStr);
                    lstArea.SelectedValue = areano;
                    txtArea.Text = lstArea.Text;
                }
                lstPartyEnglish.SelectedValue = PartyNo;
                tempPartyNo = PartyNo;
                txtParty.Text = lstPartyEnglish.Text;
                statecode = Convert.ToInt32(tVoucherEntry.StateCode);
                PartyNo = Convert.ToInt32(lstPartyEnglish.SelectedValue);

                string StateName = lblState.Text = ObjQry.ReturnString("Select Statename from mstate  where stateCode = " + statecode + " ", CommonFunctions.ConStr);
                lblState.Text = lblState.Text.ToString().ToUpper();
                if ((statecode != 0) || (Companycode != 0))
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
                    OMMessageBox.Show("please update the party from customer master and save it....... ");
                }
                if (PartyNo != 21)
                {
                    double amt = 0.00;
                    amt = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno=" + PartyNo + " group by ledgerno", CommonFunctions.ConStr);
                    if (amt > 0)
                    {
                        lblOutstanding.Text = "Outstanding To Recive :" + amt;
                    }
                    else
                        lblOutstanding.Text = "Outstanding To Pay :" + amt;
                }
                else
                    lblOutstanding.Text = "Outstanding To Recive :0.00";

                txtChrgRupees1.Text = "0"; txtChrgRupees2.Text = "0";
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
                        txtChrgRupees1.Text = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString()).ToString("0.00");
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == Others.Charges2.ToString())
                    {
                        txtChrgRupees2.Text = Convert.ToDouble(dt.Rows[i].ItemArray[0].ToString()).ToString("0.00");
                    }
                    else if (dt.Rows[i].ItemArray[2].ToString() == Others.ChargesTax.ToString())
                    {

                    }
                }

                txtDiscount1Per.Text = tVoucherEntry.DiscPercent.ToString(Format.DoubleFloating);
                txtDiscRupees1.Text = tVoucherEntry.DiscAmt.ToString(Format.DoubleFloating);
                MixModeVal = tVoucherEntry.MixMode;
                txtGrandTotal.Text = ((Convert.ToDouble(txtSubTotal.Text) - Convert.ToDouble(txtTotalDisc.Text)) + Convert.ToDouble(txtTotalTax.Text)).ToString("0.00");


                dgBill.Enabled = true;
                FillGrid();
                dgBill.Enabled = false;

             
                long LastBillNo = ObjQry.ReturnLong("Select IsNull(Max(PkVoucherNo),0) From TVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND  PkVoucherNo<" + ID + "", CommonFunctions.ConStr);
                dt = ObjFunction.GetDataView("SELECT IsNull(SUM(TStock.Quantity),0) AS Quantity, IsNull(SUM(TStock.Amount),0) AS Amount FROM TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                " TStock ON TVoucherDetails.PkVoucherTrnNo = TStock.FkVoucherTrnNo WHERE (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherEntry.PkVoucherNo = " + LastBillNo + ")").Table;

                if (dt.Rows.Count > 0)
                {
                    lblLastAmt.Text = "Amount : ";
                    lblLastBillAmt.Text = ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where PKVoucherNo=" + LastBillNo + "", CommonFunctions.ConStr).ToString();

                    lblLastBillQty.Text = "Qty: " + dt.Rows[0].ItemArray[0].ToString();
                    lblLastPayment.Text = "Payment: " + ObjQry.ReturnString("SELECT MPayType.PayTypeName FROM MPayType INNER JOIN TVoucherEntry ON MPayType.PKPayTypeNo = TVoucherEntry.PayTypeNo WHERE (TVoucherEntry.PkVoucherNo = " + LastBillNo + ")", CommonFunctions.ConStr);
                }
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
                }
                else if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true) btnUpdate.Visible = true;
                }
                else if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + " and TR.TypeOfRef in(6))", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true) btnUpdate.Visible = true;
                }
                else if (ObjQry.ReturnInteger("SELECT  COUNT(*) FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                    " WHERE     (TVoucherEntry.Reference = " + tVoucherEntry.VoucherUserNo + ") AND (TVoucherEntry.VoucherTypeCode = " + VchType.RejectionIn + ") AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ")", CommonFunctions.ConStr) > 0)
                {
                    btnBillCancel.Visible = false;
                }
                else
                {
                    btnUpdate.Visible = true;
                    btnBillCancel.Visible = true;
                    btnUpdate.Enabled = true;
                    btnBillCancel.Enabled = true;
                }
                //Transport
                cmbTransporter.SelectedValue = (tVoucherEntry.TransporterCode == 0) ? "0" : tVoucherEntry.TransporterCode.ToString();
                cmbTransPayType.SelectedValue = (tVoucherEntry.TransPayType == 0) ? "0" : tVoucherEntry.TransPayType.ToString();
                txtLRNo.Text = tVoucherEntry.LRNo;
                cmbTransMode.SelectedValue = (tVoucherEntry.TransportMode == 0) ? "0" : tVoucherEntry.TransportMode.ToString();
                txtTransNoOfItems.Text = (tVoucherEntry.TransNoOfItems == 0) ? "0" : tVoucherEntry.TransNoOfItems.ToString();
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
                    sqlQuery = "SELECT 0 AS Sr,(SELECT case when esflag='False' then ItemName+'*' else ItemName end as ItemName FROM dbo.MStockItems_V(NULL, TStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, TStock.GRWeight as GrossWt ,TStock.TRWeight as TariffWt , TStock.Quantity,MUOM.UOMName, " +
                            " TStock.Rate,TStock.PackagingCharges , TStock.NetRate, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2, " +
                            " TStock.NetAmount AS NetAmt, TStock.Amount, MItemMaster.Barcode, TStock.PkStockTrnNo," +/* MStockBarcode.PkStockBarcodeNo,*/ " TVoucherDetails.PkVoucherTrnNo, " +
                            " MItemMaster.ItemNo, MUOM.UOMNo,  TStock.FkRateSettingNo,Cast(MRateSetting.MRP as numeric(18,2)) as MRP,MRateSetting.StockConversion, TStock.Quantity * MRateSetting.StockConversion AS ActualQty, MRateSetting.MKTQty AS MKTQuantity," +
                            /*First Tax*/
                            " TStock.SGSTPercentage,TStock.SGSTAmount,MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.PkSrNo, " +
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

                          /*Scheme start*/
                          " MItemMaster.CompanyNo,  (ISNULL ((SELECT SchemeDetailsNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL)) AS 'SchemeDetailsNo', " +
                            " ISNULL ((SELECT SchemeFromNo FROM TRewardFrom  WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeFromNo',  ISNULL ((SELECT SchemeToNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeToNo',ISNULL ((SELECT PkSrNo FROM TRewardFrom WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardFromNo'," +
                            " ISNULL ((SELECT PkSrNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardToNo',IsNull((SELECT MItemDiscountDetails.PkSrNo FROM MItemDiscountDetails INNER JOIN MItemBrandDiscount ON MItemDiscountDetails.ItemBrandDiscNo = MItemBrandDiscount.PkSrNo " +
                            " INNER JOIN MItemDiscount ON MItemDiscount.PKSrNo = MItemBrandDiscount.ItemDiscNo WHERE (MItemDiscountDetails.ItemNo = TStock.ItemNo) AND (MItemDiscountDetails.FkRateSettingNo = TStock.FKRateSettingNo) AND MItemDiscount.IsActive=1 AND MItemDiscount.PeriodFrom >= '" + dtpBillDate.Value.ToString(Format.DDMMMYYYY) + "' AND MItemDiscount.PeriodTo <= '" + dtpBillDate.Value.ToString(Format.DDMMMYYYY) + "'),NULL) AS 'ItemLevelDiscNo', " +
                            " ISNULL ((SELECT PkSrNo FROM TItemLevelDiscountDetails WHERE (FKStockTrnNo  = TStock.PkStockTrnNo)), NULL) AS 'FKItemLevelDiscNo', " +
                            /*Scheme end*/
                            " IsNull(TStock.DisplayItemName,'') AS DisplayItemName,0 AS IsRateChange, " +/*ISNULL ((SELECT SchemeDetailsNo  FROM TRewardFrom WHERE (FkStockNo = TStock.PkStockTrnNo)), " + ,TStock.MfgCompNo*/
                            " Cast(IsNull((TStock.Rate + ((TStock.Rate*IsNull(MItemMaster.HigherVariation,0))/100)),0) as Numeric(18,2)), " +
                            " Cast(IsNull((TStock.Rate - ((TStock.Rate*IsNull(MItemMaster.LowerVariation,0))/100)),0) as Numeric(18,2)) ,TStock.Rate, " +
                            " IsNull(TOtherStockDetails.FKOtherVoucherNo,0) As PONo, IsNull(TOtherStockDetails.PkSrNo,0) As PKStockOtherDetailsNo,IsNull(TOtherStockDetails.FKOtherStockTrnNo,0) AS FKOtherStockTrnNo ,TStock.IType as ESFlag,TStock.Remarks as Remarks,TStock.SalesMan" +

                     " FROM MItemMaster INNER JOIN TStock ON MItemMaster.ItemNo = TStock.ItemNo INNER JOIN TVoucherDetails ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo INNER JOIN " +
                            " MItemTaxInfo ON TStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo left join  MItemTaxInfo  as MItemTaxInfo_2 ON TStock.FkItemTaxInfo2 = MItemTaxInfo_2.PkSrNo " + /*INNER JOIN MStockBarcode ON TStock.FkStockBarCodeNo = MStockBarcode.PkStockBarcodeNo */
                            " INNER JOIN MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                            " LEFT OUTER JOIN TOtherStockDetails ON TStock.PkStockTrnNo = TOtherStockDetails.FKStockTrnNo " +
                            "left join  MItemTaxInfo  as MItemTaxInfo_3 ON TStock.FKAddItemTaxSettingNo = MItemTaxInfo_3.PkSrNo WHERE      (TVoucherDetails.FkVoucherNo = " + ID + ") ORDER BY TStock.PkStockTrnNo";

                }
                else
                {
                    sqlQuery = "SELECT 0 AS Sr,(SELECT ItemName FROM dbo.MStockItems_V(NULL, TStock.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS ItemName, " +
                   " TStock.GRWeight as GrossWt ,TStock.TRWeight as TariffWt , TStock.Quantity,MUOM.UOMName,  TStock.Rate,TStock.PackagingCharges , TStock.NetRate, TStock.DiscPercentage, TStock.DiscAmount, TStock.DiscRupees, " +
                    " TStock.DiscPercentage2, TStock.DiscAmount2, TStock.DiscRupees2,  TStock.NetAmount AS NetAmt, TStock.Amount, MItemMaster.Barcode, TStock.PkStockTrnNo, TVoucherDetails.PkVoucherTrnNo,  MItemMaster.ItemNo, MUOM.UOMNo,  " +
                    " TStock.FkRateSettingNo,Cast(MRateSetting.MRP as numeric(18,2)) as MRP,MRateSetting.StockConversion, TStock.Quantity * MRateSetting.StockConversion AS ActualQty, MRateSetting.MKTQty AS MKTQuantity, " +
                    " TStock.iGSTPercentage as SGSTPercentage,TStock.iGSTAmount as SGSTAmount,MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.PkSrNo," +
                    " IsNULL((SELECT PkVoucherTrnNo FROM TVoucherDetails AS SV WHERE SV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.SalesLedgerNo) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS SalesVchNo, " +
                    " IsNull((SELECT PkVoucherTrnNo FROM TVoucherDetails AS TXV WHERE TXV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo.TaxLedgerNo) and srno in(516,518) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS TaxVchNo, " +
                   " 0  as TaxPercentage2 ,0 as TaxAmount2 ,0 as TaxLedgerNo2,0 as SalesLedgerNo2 ,0 as FKItemTaxSettingNo2 ,0 as SalesVchNo2,0 AS TaxVchNo2," +
                     " TStock.CessPercentage  as TaxPercentage3 ,TStock.CessAmount as TaxAmount3, MItemTaxInfo_3.TaxLedgerNo as TaxLedgerNo3 ,MItemTaxInfo_3.SalesLedgerNo as SalesLedgerNo3 ,TStock.FKAddItemTaxSettingNo as FKItemTaxSettingNo3,  " +
                   " IsNULL((SELECT PkVoucherTrnNo FROM TVoucherDetails AS SV WHERE SV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo_3.SalesLedgerNo) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS SalesVchNo3, " +
                    " IsNull((SELECT PkVoucherTrnNo FROM TVoucherDetails AS TXV WHERE TXV.CompanyNo=TVoucherDetails.CompanyNo AND (LedgerNo = MItemTaxInfo_3.TaxLedgerNo) and srno in(516,518) AND (FkVoucherNo = TVoucherDetails.FkVoucherNo)),0) AS TaxVchNo3, " +
                   "  MItemMaster.CompanyNo,  (ISNULL ((SELECT SchemeDetailsNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL)) AS 'SchemeDetailsNo', " +
                    " ISNULL ((SELECT SchemeFromNo FROM TRewardFrom  WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeFromNo',  ISNULL ((SELECT SchemeToNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'SchemeToNo', " +
                   " ISNULL ((SELECT PkSrNo FROM TRewardFrom WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardFromNo', ISNULL ((SELECT PkSrNo FROM TRewardTo WHERE (FkStockNo = TStock.PkStockTrnNo)), NULL) AS 'RewardToNo', " +
                   " IsNull((SELECT MItemDiscountDetails.PkSrNo FROM MItemDiscountDetails INNER JOIN MItemBrandDiscount ON MItemDiscountDetails.ItemBrandDiscNo = MItemBrandDiscount.PkSrNo  " +
                    " INNER JOIN MItemDiscount ON MItemDiscount.PKSrNo = MItemBrandDiscount.ItemDiscNo WHERE (MItemDiscountDetails.ItemNo = TStock.ItemNo) AND (MItemDiscountDetails.FkRateSettingNo = TStock.FKRateSettingNo) " +
                    " AND MItemDiscount.IsActive=1 AND MItemDiscount.PeriodFrom >= '03-Apr-2018' AND MItemDiscount.PeriodTo <= '03-Apr-2018'),NULL) AS 'ItemLevelDiscNo', " +
                    " ISNULL ((SELECT PkSrNo FROM TItemLevelDiscountDetails WHERE (FKStockTrnNo  = TStock.PkStockTrnNo)), NULL) AS 'FKItemLevelDiscNo', " +
                    " IsNull(TStock.DisplayItemName,'') AS DisplayItemName,0 AS IsRateChange,  Cast(IsNull((TStock.Rate + ((TStock.Rate*IsNull(MItemMaster.HigherVariation,0))/100)),0) as Numeric(18,2))," +
                     " Cast(IsNull((TStock.Rate - ((TStock.Rate*IsNull(MItemMaster.LowerVariation,0))/100)),0) as Numeric(18,2)) ,TStock.Rate,  IsNull(TOtherStockDetails.FKOtherVoucherNo,0) As PONo, IsNull(TOtherStockDetails.PkSrNo,0) As PKStockOtherDetailsNo, " +
                   " IsNull(TOtherStockDetails.FKOtherStockTrnNo,0) AS FKOtherStockTrnNo ,TStock.IType as ESFlag,TStock.Remarks as Remarks ,TStock.SalesMan " + // TStock.CessValue ,TStock.PackagingCharges ,TStock.OtherCharges as Dekharek,TStock.Freight ,TStock.NoOfBag  , MItemMaster.CessValue,MItemMaster.Dhekhrek  " +


                                " FROM MItemMaster INNER JOIN TStock ON MItemMaster.ItemNo = TStock.ItemNo INNER JOIN TVoucherDetails ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo " +
                                            " INNER JOIN  MItemTaxInfo ON TStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo left join  MItemTaxInfo  as MItemTaxInfo_2 ON TStock.FkItemTaxInfo2 = MItemTaxInfo_2.PkSrNo " +
                                             " INNER JOIN MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                                             " LEFT OUTER JOIN TOtherStockDetails ON TStock.PkStockTrnNo = TOtherStockDetails.FKStockTrnNo left join  MItemTaxInfo  as MItemTaxInfo_3 ON TStock.FKAddItemTaxSettingNo = MItemTaxInfo_3.PkSrNo " +
                                            " WHERE      (TVoucherDetails.FkVoucherNo = " + ID + ") ORDER BY TStock.PkStockTrnNo";
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
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AfterSaveNotDeleteItem)) == true)
                        dgBill.Rows[j].ReadOnly = true;
                }
                dtBillCollect = new DataTablesCollection();
                lblBillItem.Text = "0"; lblBilExchangeItem.Text = "0";
                string strStkNo = "";
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (txtSalesMan.Visible == true)
                    {
                        lstSalesMan.SelectedValue = Convert.ToInt32(dgBill.Rows[0].Cells[ColIndex.SalesMan].Value);
                        txtSalesMan.Text = lstSalesMan.Text;
                    }
                    if (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) >= 0)
                        lblBillItem.Text = (Convert.ToDouble(lblBillItem.Text) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString();
                    else
                        lblBilExchangeItem.Text = (Convert.ToInt64(lblBilExchangeItem.Text) + Math.Abs(Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value))).ToString();
                    if (i == 0) strStkNo = dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString();
                    else strStkNo += "," + dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString();
                }
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
            pnlPrinting.Enabled = true;
            dgBill.Rows.Clear();

            ID = 0;
            PID = 0;
            partialPay = new PartialPayment();
            partialPayAdjust = new PartialPaymentAdjust();
            ManualBill = false;
            IsDeliveryAddress = false;
            TempBillNo = 0;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsManualBillNo)) == true)
            {
                txtInvNo.Enabled = true;
                txtInvNo.ReadOnly = false;
                TempBillNo = Convert.ToInt32(txtInvNo.Text);
                ManualBill = true;
            }
            dgBill.Enabled = true;
            txtCustCode.Text = "";
            FillList();
            InitControls();
            if (ManualBill == true)
            {
                txtInvNo.Focus();
            }
            txtRemark.Text = "";
            btnCancel.Enabled = true;
            btnCashSave.Visible = true;
            btnBillCancel.Enabled = false;
            btnCashSave.Enabled = true; 
            btnPrint.Visible = false;
            btnSearch.Visible = false;
            btnDeliveryAddress.Visible = true;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSalesMan)) == true)
            {
                lstSalesMan.SelectedIndex = 0;
                txtSalesMan.Text = lstSalesMan.Text;
            }
        }

        private void dtpBillDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true)
                {
                    txtCustCode.Focus();
                }
                else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnRateType)) == true)
                {
                    pnlRateTypeH.Visible = true;
                    lstRateType.Focus();
                }
                else dgBill.Focus();
            }
        }

        private void dtpBillTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtArea.Visible == true)
                {
                    pnlArea.Visible = true;
                    lstArea.Focus();
                }
                else
                {
                    pnlParty.Visible = true;
                    lstPartyEnglish.Focus();
                }
            }
        }

        private void GSTSET(int RowIndex)
        {
            DataTable dt;
            long ItemNo, RateSettingNo;

            RateSettingNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value);
            ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);

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
                DataTable dtAddtionalTax = ObjFunction.GetDataView("  SELECT r.itemno, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
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
                    txtRateType.Focus();
                }
                if (PartyNo != 21)
                {
                    double amt = 0.00;
                    amt = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno=" + PartyNo + " group by ledgerno", CommonFunctions.ConStr);
                    if (amt > 0)
                    {
                        lblOutstanding.Text = "Outstanding To Recive :" + amt;
                    }
                    else
                        lblOutstanding.Text = "Outstanding To Pay :" + amt;
                }
                else
                    lblOutstanding.Text = "Outstanding To Recive :0.00";
                for (int i = 0; i <= dgBill.Rows.Count - 1; i++)
                {
                    GSTSET(i);
                }
                CalculateTotal();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtParty.Text = "";
            }
            else
            {
                txtParty.Text = "";
                pnlParty.Visible = true;
                lstPartyEnglish.Focus();
            }
        }

        private void lstPartEnglish_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtParty.Text = lstPartyEnglish.Text;
                    PartyNo = Convert.ToInt64(lstPartyEnglish.SelectedValue);
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnRateType)) == true)
                    {
                        pnlRateTypeH.Visible = true;
                        lstRateType.Focus();
                    }
                    else { dgBill.Focus(); }

                    pnlParty.Visible = false;

                    statecode = ObjQry.ReturnLong("Select stateCode from Mledger  where Ledgerno = " + PartyNo + " ", CommonFunctions.ConStr);
                    lblState.Text = ObjQry.ReturnString("Select Statename from mstate  where stateCode = " + statecode + " ", CommonFunctions.ConStr);
                    lblState.Text = lblState.Text.ToString().ToUpper();
                    if ((statecode != 0) || (Companycode != 0))
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
                        OMMessageBox.Show("please update the party from customer master and save it....... ");
                    }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                    {
                        if (PartyNo != 21)
                        {
                            double amt = 0.00;
                            amt = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno=" + PartyNo + " group by ledgerno", CommonFunctions.ConStr);
                            if (amt > 0)
                            {
                                lblOutstanding.Text = "Outstanding To Recive :" + amt;
                            }
                            else
                                lblOutstanding.Text = "Outstanding To Pay :" + amt;
                        }
                        else
                            lblOutstanding.Text = "Outstanding To Recive :0.00";
                    }
                    HistoryMaintain = ObjQry.ReturnBoolean("Select IsPartyWiseRate From MLedger Where LedgerNo=" + PartyNo + "", CommonFunctions.ConStr);
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPartyWiseDisc)) == true)
                    {
                        PartyDiscPerceItemLevel = ObjQry.ReturnDouble("Select DiscPer From MLedgerDetails Where LedgerNo=" + PartyNo + "", CommonFunctions.ConStr);
                    }
                }
                else if ((e.KeyCode == Keys.Escape) || (e.KeyCode == Keys.Space))
                {
                    e.SuppressKeyPress = true;
                    pnlParty.Visible = false;
                    lstPartyEnglish.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_DefaultPartyAC));
                    txtParty.Text = lstPartyEnglish.Text;
                    txtParty.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtRateType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
                        if (txtSalesMan.Visible == true)
                        { txtSalesMan.Focus(); }
                        else
                        {
                            dgBill.Focus();
                        }

                    }
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    txtRateType.Text = "";
                }
                else
                {
                    pnlRateTypeH.Visible = true;
                    lstRateType.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstRateType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtRateType.Text = lstRateType.Text;
                    dgBill.Focus();
                    pnlRateTypeH.Visible = false;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtRateType.Focus();
                    pnlRateTypeH.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtItemName.Text == "" && lstGroup1.Items.Count > 0)
                    {
                        pnlGroup1.Visible = true;
                        lstGroup1.Focus();
                        lstGroup1.SelectedIndex = 0;
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
                    txtBarCode.Focus();
                }
                else
                {
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int GroupNo = Convert.ToInt32(lstGroup1.SelectedValue);
                    pnlItemName.Visible = true;
                    dgItemList.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtItemName.Focus();
                    pnlGroup1.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstItemName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtItemName.Text = lstGroup1.Text;
                    txtItemName.Focus();
                    pnlGroup1.Visible = false;
                    pnlItemName.Visible = false;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    lstGroup1.Focus();
                    lstGroup1.SelectedIndex = 0;
                    pnlItemName.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtBQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if ((Convert.ToDouble((txtBQuantity.Text == "") ? "0" : txtBQuantity.Text)) != 0.00)
                    {
                        CalculateSubTotal();
                        if (lstUOM.Items.Count > 1)
                        {
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtUom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Rate_Start()
        {
            try
            {
                string str = "";
                int RowIndex = dgBill.CurrentCell.RowIndex;
                long ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);

                long subUomno = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.UOMNo].Value);
                double Qty = Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value);
                if (dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value == null || Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0)
                {
                    if (ItemType == 2)
                    {
                        if (HistoryMaintain == true && Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 1)
                        {
                            str = "select pksrno,Case When (IsNull((Select MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " And MLedgerRateSetting.ItemNo=RR.ItemNo And MLedgerRateSetting.UomNo=RR.UOMNo AND MLedgerRateSetting.MRP=RR.MRP),0)=0) Then RR." + lstRateType.SelectedValue + " Else IsNull((Select MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " And MLedgerRateSetting.ItemNo=RR.ItemNo And MLedgerRateSetting.UomNo=RR.UOMNo AND MLedgerRateSetting.MRP=RR.MRP),0)  END AS " + lstRateType.SelectedValue + "" +
                                   " from GetItemRateAll(" + ItemNo + "," + subUomno + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null) AS RR";
                        }
                        else if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 2)
                        {
                            if (QutFlag == true)
                                str = "select pksrno,(CASE WHEN (IsNull((SELECT TOP (1) IsNull(Tqd.Rate, 0) FROM TQuotationDetails AS Tqd  INNER JOIN TQuotation as Tq ON Tqd.FkQuotationNo = Tq.QuotationNo  " +
                                    " WHERE (Tqd.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ") AND (Tq.FromDate <= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND (Tq.ToDate >= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND  (Tqd.FkRateSettingNo = GetItemRateAll.PkSrNo)),0) = 0 ) Then " + lstRateType.SelectedValue + " Else (SELECT TOP (1) IsNull(Tqd.Rate, 0) FROM  TQuotationDetails AS Tqd  INNER JOIN  TQuotation as Tq ON Tqd.FkQuotationNo = Tq.QuotationNo   " +
                                    " WHERE (Tqd.LedgerNo =" + ObjFunction.GetListValue(lstPartyEnglish) + ") AND (Tq.FromDate <= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND (Tq.ToDate >= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND (Tqd.FkRateSettingNo = GetItemRateAll.PkSrNo))End)  as " + lstRateType.SelectedValue + "" +
                                    " from GetItemRateAll(" + ItemNo + "," + ItemNo + "," + subUomno + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null) ";
                        }
                        else
                            str = "select pksrno," + lstRateType.SelectedValue +
                        " from GetItemRateAll(" + ItemNo + "," + lstUOM.SelectedValue + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }
                    else
                    {
                        if (HistoryMaintain == true && Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 1)
                        {
                            str = "select pksrno,Case When (IsNull((Select MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " And MLedgerRateSetting.ItemNo=RR.ItemNo And MLedgerRateSetting.UomNo=RR.UOMNo AND MLedgerRateSetting.MRP=RR.MRP),0)=0) Then RR." + lstRateType.SelectedValue + " Else IsNull((Select MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " And MLedgerRateSetting.ItemNo=RR.ItemNo And MLedgerRateSetting.UomNo=RR.UOMNo AND MLedgerRateSetting.MRP=RR.MRP),0)  END AS " + lstRateType.SelectedValue + "" +
                                   " from GetItemRateAll(" + ItemNo + "," + lstUOM.SelectedValue + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null) AS RR";
                        }
                        else if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 2)
                        {
                            if (QutFlag == true)
                                str = "select pksrno,(CASE WHEN (IsNull((SELECT TOP (1) IsNull(Tqd.Rate, 0) FROM TQuotationDetails AS Tqd  INNER JOIN TQuotation as Tq ON Tqd.FkQuotationNo = Tq.QuotationNo  " +
                                    " WHERE (Tqd.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ") AND (Tq.FromDate <= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND (Tq.ToDate >= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND  (Tqd.FkRateSettingNo = GetItemRateAll.PkSrNo)),0) = 0 ) Then " + lstRateType.SelectedValue + " Else (SELECT TOP (1) IsNull(Tqd.Rate, 0) FROM  TQuotationDetails AS Tqd  INNER JOIN  TQuotation as Tq ON Tqd.FkQuotationNo = Tq.QuotationNo   " +
                                    " WHERE (Tqd.LedgerNo =" + ObjFunction.GetListValue(lstPartyEnglish) + ") AND (Tq.FromDate <= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND (Tq.ToDate >= '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + "') AND (Tqd.FkRateSettingNo = GetItemRateAll.PkSrNo))End) as " + lstRateType.SelectedValue + " " +
                                    " from GetItemRateAll(" + ItemNo + "," + lstUOM.SelectedValue + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                            else
                                str = "select pksrno," + lstRateType.SelectedValue +
                                " from GetItemRateAll(" + ItemNo + "," + lstUOM.SelectedValue + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                        }
                        else
                        {
                            str = "select pksrno," + lstRateType.SelectedValue +
                                " from GetItemRateAll(" + ItemNo + "," + lstUOM.SelectedValue + ",NULL ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                        }
                    }
                    ObjFunction.FillList(lstRate, str);
                    if (lstRate.Items.Count == 1)
                    {
                        lstRate.SelectedIndex = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = dgBill.Rows[RowIndex].Cells[ColIndex.TempRate].Value = lstRate.Text;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                        dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                        dgBill.Focus();

                        if (StopOnRate == false)
                        {
                            Rate_MoveNext();
                        }
                        else
                        {
                            BindGrid(dgBill.CurrentRow.Index);
                        }
                    }
                    else if (lstRate.Items.Count > 1)
                    {
                        if (flagParking == true)
                        {
                            lstRate.Text = dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value.ToString();
                            dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                            dgBill.Focus();
                            if (StopOnRate == false)
                                Rate_MoveNext();
                            else
                                BindGrid(dgBill.CurrentRow.Index);
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                            dgBill.Focus();
                            CalculateTotal();
                            pnlRate.Visible = true;
                            lstRate.Focus();
                        }
                    }

                }
                else
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                    dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                    dgBill.Focus();
                    if (StopOnRate == false)
                    {
                        BindGrid(RowIndex);
                    }
                    else
                    {
                        BindGrid(dgBill.CurrentRow.Index);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstUOMNew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    int RowIndex = dgBill.CurrentCell.RowIndex;
                    if (dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value != null &&
                   dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value.ToString() != lstUOM.SelectedValue.ToString())
                    {
                        dgBill.CurrentRow.Cells[ColIndex.Rate].Value = "0.00";//lstRate.Text;
                        dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0; // lstRate.SelectedValue;
                    }
                    dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text;
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOM.SelectedValue);
                    pnlUOM.Visible = false;
                    Rate_Start();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    e.SuppressKeyPress = true;
                    pnlUOM.Visible = false;
                    dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                if ((Convert.ToDouble((txtBDiscRs1.Text == "") ? "0" : txtBDiscRs1.Text)) != 0.00)
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
                if ((Convert.ToDouble((txtBDiscRs1.Text == "") ? "0" : txtBDiscRs1.Text)) != 0.00)
                { CalculateSubTotal(); }
                btnGOk.Focus();
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

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AllowsDuplicatesItems)) == false && ItemExist(BItemNo, txtPkRateSettingNo, out rwindex) == true)
                {
                    pnlItemName.Visible = false;
                    if (ItemExistScheme(BItemNo, txtPkRateSettingNo, out rwindex) == true)
                    {
                        dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                        qty = true;
                        CalculateTotal();
                    }
                    else
                    {
                        if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                        OMMessageBox.Show("This Item is alreay used for Scheme...", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                    }
                    txtBarCode.Text = "";
                    txtBarCode.Focus();
                }
                if (qty == false)
                {
                    dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value = dtRateSetting.Rows[0]["Itemno"].ToString();
                    dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = txtItemName.Text;
                    dgBill.CurrentRow.Cells[ColIndex.MRP].Value = txtMRP.Text;
                    dgBill.CurrentRow.Cells[ColIndex.Rate].Value = txtBRate.Text;
                    dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = txtBQuantity.Text.ToString();
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
                    dgBill.CurrentRow.Cells[ColIndex.ESFlag].Value = ObjQry.ReturnBoolean("select ESFlag from Mitemmaster where itemno =" + dtRateSetting.Rows[0]["Itemno"].ToString(), CommonFunctions.ConStr);
                    dgBill.CurrentRow.Cells[ColIndex.StockCompanyNo].Value = DBGetVal.FirmNo;

                    CalculateTotal();
                    Clear();
                    dgBill.Rows.Add();
                    lblitemcount.Text = Convert.ToString((dgBill.Rows.Count - 1));

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
            SetRateType(Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType).ToString()));
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
            SetRateType(Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType).ToString()));
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
            else if (RateTypeNo == 6) lstRateType.SelectedValue = "MRP";
            else if (RateTypeNo == 7) lstRateType.SelectedValue = "PurRate";
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
            if (OMMessageBox.Show("Are you sure you want to cancel this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                dgBill.Enabled = false;
                dgBill.Rows.Clear();
                pnlPrinting.Enabled = true;
                pnlRateTypeH.Visible = false;
                pnlParty.Visible = false;
                pnlSalesMan.Visible = false;
                pnlDeliveryAddress.Visible = false;

                DisplayList(false);
                NavigationDisplay(2);
                if (ID == 0)
                {
                    btnUpdate.Enabled = false;
                    btnBillCancel.Enabled = false;
                    btnPrint.Enabled = false;
                    btnSearch.Enabled = false;
                    NavigationDisplay(2);
                }
                else { FillField(); }
                viewmode();
                pnlGrossWt.Visible = false;
                btnCashSave.Visible = false;
                btnPrint.Visible = true;
                btnSearch.Visible = true;
                dtDelete.Clear();
                btnNew.Focus();
                pnlPrinting.Visible = true;
            }
        }

        public void DisplayList(bool flag)
        {
            pnlItemName.Visible = flag;
            pnlGroup1.Visible = flag;
            pnlUOM.Visible = flag;
            pnlRate.Visible = flag;
            pnlParking.Visible = flag;
            pnlMainParking.Visible = flag;
        }

        public void viewmode()
        {
            if (dtRateSetting != null)
                dtRateSetting.Clear();
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
                        long TempItemno = ObjQry.ReturnLong("Select ItemNo from MItemMaster where Barcode = '" + dgBill.CurrentCell.Value + "'", CommonFunctions.ConStr);
                        string ItemList = "SELECT MItemMaster.ItemNo,MItemMaster.ItemName AS ItemName, MRateSetting.@cmbRateType@ AS SaleRate, MUOM.UOMName, MRateSetting.MRP,case when MItemMaster.esflag='false' then MRateSetting.stock else MRateSetting.stock2 end AS Stock, (select uomname from mitemmaster inner join muom on MItemMaster.uoml=muom.uomno where MItemMaster.itemno=MRateSetting.itemno ) AS stkUOM, 0 AS SaleTax, 0 AS PurTax,  MItemMaster.CompanyNo, MItemMaster.Barcode,  MRateSetting.PkSrNo As RateSettingNo, MItemMaster.UOMDefault,MRateSetting.PurRate,0 As DiscPerce,MItemMaster.ShortCode ,MItemMaster.esflag  FROM  MItemMaster INNER JOIN MUOM ON MItemMaster.UOMDefault = MUOM.UOMNo  LEFT OUTER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL,null) AS MRateSetting  ON  MItemMaster.ItemNo = MRateSetting.ItemNo AND MItemMaster.UOMDefault = MRateSetting.UOMNo WHERE     (MItemMaster.ItemNo <> 1) AND (MItemMaster.IsActive = 'true') AND (MItemMaster.GroupNo = @Param1@)  ORDER BY MItemMaster.ItemName";
                        {
                            if (HistoryMaintain == true && Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 1)
                            {
                                ItemList = ItemList.Replace("MRateSetting.@cmbRateType@", " Case When (IsNull((Select Top(1) MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + lstPartyEnglish.SelectedValue + " And MLedgerRateSetting.ItemNo=MItemMaster.ItemNo And MLedgerRateSetting.UomNo=MRateSetting.UOMNo AND MLedgerRateSetting.MRP=MRateSetting.MRP Order By PkSrNo desc),0)=0) Then MRateSetting." + lstRateType.SelectedValue + " Else IsNull((Select Top(1) MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + lstPartyEnglish.SelectedValue + " And MLedgerRateSetting.ItemNo=MItemMaster.ItemNo And MLedgerRateSetting.UomNo=MRateSetting.UOMNo AND MLedgerRateSetting.MRP=MRateSetting.MRP Order By PkSrNo Desc),0) End");
                                ItemList = ItemList.Replace("0 As DiscPerce", " IsNull((Select Top(1) MLedgerRateSetting.DiscPercentage From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + lstPartyEnglish.SelectedValue + " And MLedgerRateSetting.ItemNo=MItemMaster.ItemNo And MLedgerRateSetting.UomNo=MRateSetting.UOMNo AND MLedgerRateSetting.MRP=MRateSetting.MRP Order By PkSrNo desc),0) As DiscPerce ");
                            }
                            else if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 2)
                            {
                                if (QutFlag == true)
                                    ItemList = ItemList.Replace("MRateSetting.@cmbRateType@", " (CASE WHEN (IsNull((SELECT TOP (1) IsNull(TQuotationDetails.Rate, 0) FROM TQuotationDetails INNER JOIN TQuotation ON TQuotationDetails.FkQuotationNo = TQuotation.QuotationNo WHERE (TQuotationDetails.LedgerNo = " + lstPartyEnglish.SelectedValue + ") AND (TQuotation.FromDate <= '" + dtpBillDate.Text + "') AND (TQuotation.ToDate >= '" + dtpBillDate.Text + "') AND  (TQuotationDetails.FkRateSettingNo = MRateSetting.PkSrNo)),0) = 0 ) THEN MRateSetting." + lstRateType.SelectedValue + " ELSE (SELECT TOP (1) IsNull(TQuotationDetails.Rate, 0)  FROM  TQuotationDetails INNER JOIN  TQuotation ON TQuotationDetails.FkQuotationNo = TQuotation.QuotationNo  WHERE (TQuotationDetails.LedgerNo =" + lstPartyEnglish.SelectedValue + ") AND (TQuotation.FromDate <= '" + dtpBillDate.Text + "') AND (TQuotation.ToDate >= '" + dtpBillDate.Text + "') AND (TQuotationDetails.FkRateSettingNo = MRateSetting.PkSrNo))End) ");
                                else
                                    ItemList = ItemList.Replace("@cmbRateType@", "" + lstRateType.SelectedValue);
                            }
                            else
                                ItemList = ItemList.Replace("@cmbRateType@", "" + lstRateType.SelectedValue);

                            ItemList = ItemList.Replace("@CompNo@", "MItemMaster.CompanyNo");

                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowingLandedRate)) == true)
                                ItemList = ItemList.Replace("MRateSetting.PurRate,", "IsNull((Select Top(1) LandedRate From TStock where ItemNo =MItemMaster.ItemNo and LandedRate>0 Order by PkStockTrnNo desc),0) as PurRate,");
                            ItemList = ItemList.Replace("@GodownNo@", "" + ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation) + "");

                            if (DBGetVal.KachhaFirm == true)
                            {
                                ItemList = ItemList.Replace("MItemMaster.ItemName AS ItemName", "Case When MItemMaster.esflag='false'then( MItemMaster.ItemName+ ' *') else MItemMaster.ItemName end AS ItemName");
                                ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value));
                            }
                            else
                            {
                                ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value) + " and MItemMaster.ESFlag='False' ");

                            }
                            ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL"));
                            ItemList = ItemList.Replace("dbo.GetItemRateAll(NULL, NULL, NULL, NULL ,", "dbo.GetItemRateAll(" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value) + ",");

                            ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MItemMaster.LangShortDesc<>'') then MItemMaster.LangShortDesc else MItemMaster.LangFullDesc end AS ItemNameLang,");

                            ItemList = ItemList.Replace("ORDER BY", "And MItemMaster.itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' ) Order by ");

                            DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                            strItemQuery_last[qNo - 1] = ItemList;
                            if (dtItemList.Rows.Count > 0)
                            {
                                dgItemList.DataSource = dtItemList.DefaultView;
                                dgItemList.CurrentCell = dgItemList[1, 0];
                                pnlItemName.Visible = true;
                                dgItemList.Focus();
                            }
                            else
                            {
                                OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                            }
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
                if (qNo == 1)// for group list
                {
                    ItemList = ItemList.Replace("@FromDate", "" + dtpBillDate.Text.Trim() + "");
                    ItemList = ItemList.Replace("=@CompNo@", "<>2");
                    if (DBGetVal.KachhaFirm == true)
                    {

                        ItemList = ItemList.Replace("and esflag='false'", "  ");
                    }
                    if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))//===//umesh
                    {
                        ObjFunction.FillList(lstGroup1, ItemList);
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                        {
                            ItemList = ItemList.Replace("ItemGroupName from", "LangGroupName from");

                            ObjFunction.FillList(lstGroup1Lang, ItemList.Replace("ItemGroupName From", "LangGroupName from"));
                        }
                        strItemQuery_last[qNo - 1] = ItemList;
                    }
                    if (lstGroup1.Items.Count != 0)
                    {
                        pnlGroup1.Visible = true;
                        lstGroup1.Focus();
                    }
                    else
                    {

                        OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                        dgBill.Focus();
                    }
                }
                else if (qNo == 2) //for itemlist or item name
                {
                    if (HistoryMaintain == true && Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 1)
                    {
                        ItemList = ItemList.Replace("MRateSetting.@cmbRateType@", " Case When (IsNull((Select Top(1) MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + lstPartyEnglish.SelectedValue + " And MLedgerRateSetting.ItemNo=MItemMaster.ItemNo And MLedgerRateSetting.UomNo=MRateSetting.UOMNo AND MLedgerRateSetting.MRP=MRateSetting.MRP Order By PkSrNo desc),0)=0) Then MRateSetting." + lstRateType.SelectedValue + " Else IsNull((Select Top(1) MLedgerRateSetting.Rate From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + lstPartyEnglish.SelectedValue + " And MLedgerRateSetting.ItemNo=MItemMaster.ItemNo And MLedgerRateSetting.UomNo=MRateSetting.UOMNo AND MLedgerRateSetting.MRP=MRateSetting.MRP Order By PkSrNo Desc),0) End");
                        ItemList = ItemList.Replace("0 As DiscPerce", " IsNull((Select Top(1) MLedgerRateSetting.DiscPercentage From MLedgerRateSetting Where MLedgerRateSetting.LedgerNo=" + lstPartyEnglish.SelectedValue + " And MLedgerRateSetting.ItemNo=MItemMaster.ItemNo And MLedgerRateSetting.UomNo=MRateSetting.UOMNo AND MLedgerRateSetting.MRP=MRateSetting.MRP Order By PkSrNo desc),0) As DiscPerce ");
                    }
                    else if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 2)
                    {
                        if (QutFlag == true)
                            ItemList = ItemList.Replace("MRateSetting.@cmbRateType@", " (CASE WHEN (IsNull((SELECT TOP (1) IsNull(TQuotationDetails.Rate, 0) FROM TQuotationDetails INNER JOIN TQuotation ON TQuotationDetails.FkQuotationNo = TQuotation.QuotationNo WHERE (TQuotationDetails.LedgerNo = " + lstPartyEnglish.SelectedValue + ") AND (TQuotation.FromDate <= '" + dtpBillDate.Text + "') AND (TQuotation.ToDate >= '" + dtpBillDate.Text + "') AND  (TQuotationDetails.FkRateSettingNo = MRateSetting.PkSrNo)),0) = 0 ) THEN MRateSetting." + lstRateType.SelectedValue + " ELSE (SELECT TOP (1) IsNull(TQuotationDetails.Rate, 0)  FROM  TQuotationDetails INNER JOIN  TQuotation ON TQuotationDetails.FkQuotationNo = TQuotation.QuotationNo  WHERE (TQuotationDetails.LedgerNo =" + lstPartyEnglish.SelectedValue + ") AND (TQuotation.FromDate <= '" + dtpBillDate.Text + "') AND (TQuotation.ToDate >= '" + dtpBillDate.Text + "') AND (TQuotationDetails.FkRateSettingNo = MRateSetting.PkSrNo))End) ");
                        else
                            ItemList = ItemList.Replace("@cmbRateType@", "" + lstRateType.SelectedValue);
                    }
                    else
                        ItemList = ItemList.Replace("@cmbRateType@", "" + lstRateType.SelectedValue);

                    ItemList = ItemList.Replace("@CompNo@", "MItemMaster.CompanyNo");

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowingLandedRate)) == true)
                        ItemList = ItemList.Replace("MRateSetting.PurRate,", "IsNull((Select Top(1) LandedRate From TStock where ItemNo =MItemMaster.ItemNo and LandedRate>0 Order by PkStockTrnNo desc),0) as PurRate,");
                    ItemList = ItemList.Replace("@GodownNo@", "" + ObjFunction.GetAppSettings(AppSettings.S_OutwardLocation) + "");

                    if (DBGetVal.KachhaFirm == true)
                    {
                        ItemList = ItemList.Replace("MItemMaster.ItemName AS ItemName", "Case When MItemMaster.esflag='false'then( MItemMaster.ItemName+ ' *') else MItemMaster.ItemName end AS ItemName");
                        ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value) + " ");
                        ItemList = ItemList.Replace("'false'='false'", "'True'='false'");
                    }
                    else
                    {
                        ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value) + " and MItemMaster.ESFlag='False' ");

                    }
                    ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL"));
                    ItemList = ItemList.Replace(" NULL,null) AS", " NULL," + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL") + " ) AS ");
                    ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MItemMaster.LangShortDesc<>'') then MItemMaster.LangShortDesc else MItemMaster.LangFullDesc end AS ItemNameLang,");
                    ItemList = ItemList.Replace("ORDER BY", "And MItemMaster.ItemType <>2  Order by ");
                    {
                        ItemList = ItemList.Replace("ORDER BY", "And MItemMaster.itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' ) Order by ");

                        DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                        strItemQuery_last[qNo - 1] = ItemList;
                        if (dtItemList.Rows.Count > 0)
                        {
                            dgItemList.DataSource = dtItemList.DefaultView;

                            pnlItemName.Visible = true;
                            dgItemList.CurrentCell = dgItemList[1, 0];
                            if (DBGetVal.KachhaFirm == false)
                                dgItemList.Columns[18].Visible = false;

                            dgItemList.Focus();
                        }
                        else
                        {
                            OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                            dgBill.Focus();
                        }
                    }
                }
                switch (qNo)
                {
                    case 1:
                        break;
                    case 2:
                        switch (strItemQuery.Length)
                        {
                            case 2://very important for item grid bind list

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
                switch (strItemQuery.Length - qNo)
                {
                    case 0:

                        break;
                    case 1:

                        break;
                    case 2:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ObjFunction.FillList(lstGroup1, ItemList);
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
                    dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
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
                if ((lstGroup1.Items.Count > 0) && (lstGroup1Lang.Items.Count > 0))
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
                   // if (((System.Windows.Forms.Control)sender).Visible == false)
                       // pnlSalePurHistory.Visible = ((System.Windows.Forms.Control)sender).Visible;
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
                {
                    lstPartyLang.SelectedIndex = lstPartyEnglish.SelectedIndex;
                }
            }
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
            public static int GrossWt = 2;
            public static int TariffWt = 3;
            public static int Quantity = 4;

            public static int UOM = 5;
            public static int Rate = 6;
            public static int PackagingCharges = 7;
            public static int NetRate = 8;
            public static int DiscPercentage = 9;
            public static int DiscAmount = 10;
            public static int DiscRupees = 11;
            public static int DiscPercentage2 = 12;
            public static int DiscAmount2 = 13;
            public static int DiscRupees2 = 14;
            public static int NetAmt = 15;
            public static int Amount = 16;
            public static int Barcode = 17;
            public static int PkStockTrnNo = 18;
            public static int PkVoucherNo = 19;
            public static int ItemNo = 20;
            public static int UOMNo = 21;
            public static int PkRateSettingNo = 22;
            public static int MRP = 23;
            public static int StockFactor = 24;
            public static int ActualQty = 25;
            public static int MKTQuantity = 26;

            public static int SGSTPercentage = 27;
            public static int SGSTAmount = 28;
            public static int TaxLedgerNo = 29;
            public static int SalesLedgerNo = 30;
            public static int PkItemTaxInfo = 31;
            public static int SalesVchNo = 32;
            public static int TaxVchNo = 33;

            public static int CGSTPercentage = 34;
            public static int CGSTAmount = 35;
            public static int TaxLedgerNo2 = 36;
            public static int SalesLedgerNo2 = 37;
            public static int PkItemTaxInfo2 = 38;
            public static int SalesVchNo2 = 39;
            public static int TaxVchNo2 = 40;

            public static int IGSTPercentage = 41;
            public static int IGSTAmount = 42;
            public static int TaxLedgerNo3 = 43;
            public static int SalesLedgerNo3 = 44;
            public static int PkItemTaxInfo3 = 45;
            public static int SalesVchNo3 = 46;
            public static int TaxVchNo3 = 47;

            public static int StockCompanyNo = 48;
            public static int SchemeDetailsNo = 49;
            public static int SchemeFromNo = 50;
            public static int SchemeToNo = 51;
            public static int RewardFromNo = 52;
            public static int RewardToNo = 53;
            public static int ItemLevelDiscNo = 54;
            public static int FKItemLevelDiscNo = 55;
            public static int DisplayName = 56;

            public static int IsRateChange = 57;
            public static int HigherVariation = 58;
            public static int LowerVariation = 59;
            public static int TempRate = 60;
            public static int SONo = 61;
            public static int PkStockDetailsNo = 62;
            public static int FkOtherStockTrnNo = 63;
            public static int ESFlag = 64;
            public static int Remarks = 65;
            public static int SalesMan = 66;

        }
        #endregion

        public bool ItemExistScheme(long ItNo, long RateSettingNo, out int rowIndex)
        {
            rowIndex = -1;
            bool flag = false;
            try
            {
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) && RateSettingNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value))
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AfterSaveNotDeleteItem)) == false)
                        {
                            if ((dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() == "" || dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() == "0") &&
                                (dgBill.Rows[i].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() == "" || dgBill.Rows[i].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() == "0"))
                            {
                                rowIndex = i;
                                flag = true;
                                break;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value) <= 0)
                            {
                                if ((dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() == "" || dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() == "0") &&
                                (dgBill.Rows[i].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() == "" || dgBill.Rows[i].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() == "0"))
                                {
                                    rowIndex = i;
                                    flag = true;
                                    break;
                                }
                            }
                        }
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
                                dgBill.Focus();
                                dgBill.CurrentCell = dgBill[4, row];
                            }
                        }
                    }
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
                else if (col == ColIndex.DiscPercentage && row >= 0)
                {
                    dgBill.CurrentCell.ErrorText = "";
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (dgBill.CurrentCell.Value.ToString() != "" && dgBill.CurrentCell.Value.ToString() != "0")
                        {
                            if (ObjFunction.CheckNumeric(dgBill.CurrentCell.Value.ToString()) == true)
                            {
                                dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, dgBill.CurrentCell.RowIndex];
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
                    if (dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value == null) dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value = 0;
                    long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                    bool a = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AllowsDuplicatesItems));
                    int rwindex = 0;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AllowsDuplicatesItems)) == true && ItemExist(i, Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value), out rwindex) == true)
                    {
                        pnlItemName.Visible = false;
                        if (rwindex != -1) //-1 is return value if not present
                        {
                            dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                            if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                            dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                            dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                            dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                        }
                        else
                        {
                            if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                            OMMessageBox.Show("This Item is alreay used for Scheme...", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[13].Value);
                        dgBill.CurrentRow.Cells[ColIndex.Rate].Value = dgBill.CurrentRow.Cells[ColIndex.TempRate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[3].Value).ToString("0.00");//lstRate.Text;
                        dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);//lstRate.SelectedValue;
                        dgBill.CurrentRow.Cells[ColIndex.UOM].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value;
                        dgBill.CurrentRow.Cells[ColIndex.MRP].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPartyWiseDisc)) == true)//===from mledger fixed discount
                        {
                            dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = PartyDiscPerceItemLevel;
                        }
                        else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsUseLastPartyWiseDiscEnabled)) == true)//===from rate history
                        {
                            dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[15].Value;
                        }
                        else
                        {
                            dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = 0.00;

                        }
                        dgBill.CurrentRow.Cells[ColIndex.IsRateChange].Value = "0";
                        dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[11].Value;
                        dgBill.CurrentRow.Cells[ColIndex.ESFlag].Value = Convert.ToBoolean(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[17].Value);
                        dgBill.CurrentRow.Cells[ColIndex.GrossWt].Value = "0.00";
                        dgBill.CurrentRow.Cells[ColIndex.TariffWt].Value = "0.00";
                        dgBill.CurrentRow.Cells[ColIndex.PackagingCharges].Value = "0.00";
                        if (IsCancel == false)
                            dgBill.CurrentRow.Cells[ColIndex.PkStockTrnNo].Value = 0;

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSalesMan)) == true)
                        {
                            dgBill.CurrentRow.Cells[ColIndex.SalesMan].Value = lstSalesMan.SelectedValue;
                        }
                        else { dgBill.CurrentRow.Cells[ColIndex.SalesMan].Value = 1; }
                        pnlItemName.Visible = false;
                        Desc_MoveNext(Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value), 0);
                        IsCancel = false;
                        dgBill.Focus();
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
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AfterSaveNotDeleteItem)) == false)
                        {
                            rowIndex = i;
                            flag = true;
                            break;
                        }
                        else
                        {
                            if (Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value) <= 0)
                            {
                                rowIndex = i;
                                flag = true;
                                break;
                            }
                        }
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
                    // 
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
                    else
                    {
                        if (ItemNo.Length > 1)
                        {
                            ItemType = 3;
                            FillItemList(0, ItemType);//FillItemList();
                        }
                        else
                        {
                            int rwindex = 0;

                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AllowsDuplicatesItems)) == true && ItemExist(ItemNo[0], txtPkRateSettingNo, out rwindex) == true)
                            {
                                pnlItemName.Visible = false;
                                if (ItemExistScheme(BItemNo, txtPkRateSettingNo, out rwindex) == true)
                                {
                                    dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.Rows[rwindex].Cells[ColIndex.Quantity].Value) + 1;
                                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                                    CalculateTotal();
                                }
                                else
                                {
                                    if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                                    OMMessageBox.Show("This Item is alreay used for Scheme...", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                                }
                                txtBarCode.Text = "";
                                txtBarCode.Focus();
                            }
                            else
                            {
                                dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgBill.CurrentCell.Value;
                                Desc_MoveNext(ItemNo[0], BarcodeNo[0]);
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
            string sql = "";
            DataTable dt = new DataTable();
            if (DBGetVal.KachhaFirm == false)
            {
                sql = "SELECT   distinct MItemMaster.ItemNo,MItemMaster.Barcode,MItemMaster.esflag FROM  MItemMaster  INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                 " WHERE ((MItemMaster.Barcode = '" + strBarcode + "') or (MItemMaster.ShortCode = '" + strBarcode + "')) " +
                 " AND (MItemMaster.IsActive = 'true')  AND (MRateSetting.IsActive='true')   AND (MItemMaster.esflag = 'false')" +
                 " GROUP BY  MItemMaster.ItemNo,MItemMaster.Barcode, MRateSetting.MRP, MRateSetting.UOMNo,MItemMaster.esflag";
            }
            else
            {
                sql = "SELECT   distinct MItemMaster.ItemNo,MItemMaster.Barcode,MItemMaster.esflag FROM  MItemMaster  INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
                " WHERE ((MItemMaster.Barcode = '" + strBarcode + "') or (MItemMaster.ShortCode = '" + strBarcode + "')) " +
                " AND (MItemMaster.IsActive = 'true')  AND (MRateSetting.IsActive='true')  " +
                " GROUP BY  MItemMaster.ItemNo,MItemMaster.Barcode, MRateSetting.MRP, MRateSetting.UOMNo,MItemMaster.esflag";
            }
            dt = ObjFunction.GetDataView(sql).Table;
            BarcodeNo = new long[dt.Rows.Count];
            ItemNo = new long[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {    //===umesh==15-11-18
                dgBill.CurrentRow.Cells[ColIndex.ESFlag].Value = Convert.ToString(dt.Rows[0].ItemArray[2].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BarcodeNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                    ItemNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                    dgBill.CurrentCell.Value = dt.Rows[i].ItemArray[1].ToString();
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
                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL," + ItemNo + ",NULL,NULL,NULL,NULL,NULL) Where IsActive='true' ").Table;//where ItemNo = " + ItemNo
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.StockCompanyNo].Value = dtItem.Rows[0].ItemArray[1].ToString();
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IsRateChange].Value = "0";

                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();

                if (StopOnQty == true)
                {
                    if (dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex].Value == null)
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }
                    else
                        Qty_MoveNext();
                }
                else if (StopOnRate == true)
                {
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value == null || dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value.ToString() == "")
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                    Rate_MoveNext();
                }
                else
                {
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                    UOM_Start();
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
                rowQtyIndex = dgBill.CurrentCell.RowIndex;
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { rowQtyIndex, 5, dgBill });
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
                dgBill.CurrentCell = dgBill[ColIndex.UOM, row];// stop on uom name focus//dgBill[3, row];

                Rectangle loc = dgBill.GetCellDisplayRectangle(dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex, true);
                pnlUOM.Location = new Point(dgBill.Location.X + loc.X,
                dgBill.Location.Y + loc.Y + loc.Height);

                ObjFunction.FillList(lstUOM, "Select UOMNo,UOMName From GetUOMList(NULL," + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + ",NULL)");
                if (lstUOM.Items.Count > 1)
                {
                    pnlUOM.Visible = true;
                    lstUOM.Focus();
                }
                else
                {
                    dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text;
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = lstUOM.SelectedValue;
                    dgBill.CurrentCell = dgBill[ColIndex.Rate, row];//stop on rate focusdgBill[4, row];
                    Rate_Start();
                    BindGrid(row);
                    dgBill.Focus();
                }
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
                if (dgBill.Rows[RowIndex].Cells[ColIndex.ItemLevelDiscNo].FormattedValue.ToString() != "" && dgBill.Rows[RowIndex].Cells[ColIndex.ItemLevelDiscNo].FormattedValue.ToString() != "0")
                {
                    dgBill.Rows[RowIndex].Cells[ColIndex.DiscRupees].Value = 0;
                    dgBill.Rows[RowIndex].Cells[ColIndex.ItemLevelDiscNo].Value = "";
                    txtOtherDisc.Text = "0.00";
                }
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

                    string Str = "Select CompanyNo, ";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsRateVeriation)) && dgBill.Rows[RowIndex].Cells[ColIndex.TempRate].EditedFormattedValue.ToString().Trim() != "")
                        Str = Str + " Cast(IsNull((" + dgBill.Rows[RowIndex].Cells[ColIndex.TempRate].Value + " + ((" + dgBill.Rows[RowIndex].Cells[ColIndex.TempRate].Value + "*HigherVariation)/100)),0) as Numeric(18,2)) , " +
                        " Cast(IsNull((" + dgBill.Rows[RowIndex].Cells[ColIndex.TempRate].Value + " - ((" + dgBill.Rows[RowIndex].Cells[ColIndex.TempRate].Value + "*LowerVariation)/100)),0) as Numeric(18,2))  ";
                    else
                        Str = Str + "0 as HigherVariation,0 as LowerVariation";
                    Str = Str + " From MItemMaster Where ItemNo=" + dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value + "";

                    DataTable dtItemsDetails = ObjFunction.GetDataView(Str).Table;
                    if (dtItemsDetails.Rows.Count > 0)
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.HigherVariation].Value = dtItemsDetails.Rows[0].ItemArray[1].ToString();
                        dgBill.Rows[RowIndex].Cells[ColIndex.LowerVariation].Value = dtItemsDetails.Rows[0].ItemArray[2].ToString();
                    }

                    dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo].Value = Convert.ToInt64(dt.Rows[0][3].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo].Value = Convert.ToInt64(dt.Rows[0][4].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo].Value = Convert.ToInt64(dt.Rows[0][5].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.SGSTPercentage].Value = Convert.ToDouble(dt.Rows[0][6].ToString());

                    if (State == true)
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo2].Value = Convert.ToInt64(dt.Rows[1][3].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo2].Value = Convert.ToInt64(dt.Rows[1][4].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo2].Value = Convert.ToInt64(dt.Rows[1][5].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.CGSTPercentage].Value = Convert.ToDouble(dt.Rows[1][6].ToString());

                    }
                    else
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo2].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo2].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo2].Value = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.CGSTPercentage].Value = 0;
                    }

                    DataTable dtAddtionalTax = ObjFunction.GetDataView("  SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
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
                        dgBill.Rows[RowIndex].Cells[ColIndex.CGSTPercentage].Value = 0;

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
                    if (dgBill.Rows[RowIndex].Cells[ColIndex.SONo].Value == null) dgBill.Rows[RowIndex].Cells[ColIndex.SONo].Value = "0";

                    //=================umesh
                    if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1 && dgBill.CurrentCell.ColumnIndex == 6)
                    {
                        dgBill.Rows.Add();
                    }
                    CalculateTotal();

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnRate)) == true)
                    {
                        MovetoNext move2n = new MovetoNext(m2n1);
                        BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.Rate, dgBill });
                        dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }
                    else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDisc)) == true)
                    {
                        MovetoNext move2n = new MovetoNext(m2n1);
                        BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.DiscPercentage, dgBill });
                        dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                    }
                    else
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
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
                txtGrandTotal.Text = "0.00";
                txtTotalDisc.Text = "0.00";
                txtTotalTax.Text = "0.00";
                subTotal = 0.00; totalDisc = 0.00; totalChrg = 0.00; totalTax = 0.00; TaxPerce2 = 0.00; TaxPerce3 = 0.00; TotalTaxPerce = 0.00;
                tAmount = 0.00; TotFinal = 0.00; Disc1 = 0.00; Disc2 = 0.00; TaxAmt = 0.00; TotalAmt = 0.00; TaxAmt2 = 0; ttRate = 0; ttax = 0; TaxAmt3 = 0;
                double TaxPerce = 0.00, Amount = 0.00, DiscAmt = 0.00, PAckingCharges = 0.00, APMCAMOUNT = 0, TotalAPMC = 0.00;
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                    {
                        if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                        {
                            if (dgBill.Rows[i].Cells[ColIndex.Quantity].Value == null) dgBill.Rows[i].Cells[ColIndex.Quantity].Value = 1.00;
                            if (dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value == null) dgBill.Rows[i].Cells[ColIndex.MKTQuantity].Value = 1.00;
                            if (dgBill.Rows[i].Cells[ColIndex.StockFactor].Value == null) dgBill.Rows[i].Cells[ColIndex.StockFactor].Value = 1.00;
                            if (dgBill.Rows[i].Cells[ColIndex.Rate].Value == null) dgBill.Rows[i].Cells[ColIndex.Rate].Value = 0.00;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value = 0.00;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees].Value = 0.00;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscPercentage2].Value = 0.00;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = 0.00;
                            if (dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value == null) dgBill.Rows[i].Cells[ColIndex.DiscRupees2].Value = 0.00;
                            if (dgBill.Rows[i].Cells[ColIndex.PackagingCharges].Value == null) dgBill.Rows[i].Cells[ColIndex.PackagingCharges].Value = 0.00;
                            APMCAMOUNT = 0;
                            PAckingCharges = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.PackagingCharges].Value));
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
                            tAmount = Amount - DiscAmt;
                            TaxPerce = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.SGSTPercentage].Value);

                            TaxPerce2 = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.CGSTPercentage].Value);
                            TaxPerce3 = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.IGSTPercentage].Value);
                            TotalTaxPerce = TaxPerce + TaxPerce2 + TaxPerce3;
                            TaxAmt = 0; TaxAmt2 = 0; ttRate = 0; ttax = 0; TaxAmt3 = 0;
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == true)//Reverse or forward calculation
                            {
                                ttax = Convert.ToDouble(((tAmount * TotalTaxPerce) / (100 + TotalTaxPerce)).ToString("0.00"));
                                ttRate = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) == 0) ? 0 : Math.Round(Convert.ToDouble(((tAmount - ttax) / SubQty) * SubMktQty), 2);
                                APMCAMOUNT = PAckingCharges;

                                TaxAmt = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce) / 100), 2);
                                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce2) / 100), 2);
                                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce3) / 100), 2);

                                dgBill.Rows[i].Cells[ColIndex.Amount].Value = (tAmount + APMCAMOUNT + (APMCAMOUNT * TotalTaxPerce / 100)).ToString("0.00");
                            }
                            else
                            {

                                ttRate = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) / SubQty) * SubMktQty), 2);
                                TaxAmt = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * TaxPerce) / 100), 2);
                                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * TaxPerce2) / 100), 2);
                                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT) * TaxPerce3) / 100), 2);
                                dgBill.Rows[i].Cells[ColIndex.Amount].Value = (tAmount + TaxAmt + TaxAmt2 + TaxAmt3).ToString("0.00");
                            }

                            totalTax += TaxAmt + TaxAmt2 + TaxAmt3;

                            dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value = Disc1.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = Disc2.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value = TaxAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.CGSTAmount].Value = TaxAmt2.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.IGSTAmount].Value = TaxAmt3.ToString("0.00");

                            dgBill.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = ((tAmount - ttax)).ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.ActualQty].Value = ((Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.StockFactor].Value)));

                            subTotal = subTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.NetAmt].Value);
                            TotalAPMC = TotalAPMC + APMCAMOUNT;
                            totalDisc = totalDisc + DiscAmt;
                            if (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) >= 0)
                                lblBillItem.Text = (Convert.ToDouble(lblBillItem.Text) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString();
                            else
                                lblBilExchangeItem.Text = (Convert.ToInt64(lblBilExchangeItem.Text) + Math.Abs(Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value))).ToString();

                        }
                    }
                    subTotal = Convert.ToDouble(subTotal.ToString("0.00"));
                    txtSubTotal.Text = (subTotal + totalDisc).ToString("0.00");
                    txtTotalDisc.Text = totalDisc.ToString("0.00");

                    TotalAmt = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) + totalTax) - Convert.ToDouble(txtTotalDisc.Text)).ToString("0.00"));

                    if (txtSchemeDisc.Text.Trim() == "") txtSchemeDisc.Text = "0.00";
                    if (txtOtherDisc.Text.Trim() == "") txtOtherDisc.Text = "0.00";
                    if (txtDiscRupees1.Text.Trim() == "") txtDiscRupees1.Text = "0.00";
                    //umesh--21-10-2018
                    if ((Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) + Convert.ToDouble(txtTotalTax.Text.Trim())) != 0)
                        //umesh--21-10-2018
                        txtDiscRupees1.Text = Math.Round(Convert.ToDouble((Convert.ToDouble(txtDiscount1Per.Text) * (Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) + Convert.ToDouble(txtTotalTax.Text.Trim()))) / 100), 2).ToString();
                    totalDisc = Convert.ToDouble(txtDiscRupees1.Text) + Convert.ToDouble(txtSchemeDisc.Text) + Convert.ToDouble(txtOtherDisc.Text);
                    totalChrg = Convert.ToDouble(txtChrgRupees1.Text) + Convert.ToDouble(txtChrgRupees2.Text) + TotalAPMC;
                    txtDiscount1Per.Text = Math.Round(Convert.ToDouble(txtDiscount1Per.Text), 2).ToString("0.00");
                    txtTotalAnotherDisc.Text = Math.Round(totalDisc, 2).ToString("0.00");
                    txtTotalChrgs.Text = Math.Round(totalChrg, 2).ToString("0.00");
                    txtTotalTax.Text = Math.Round(totalTax, 2).ToString("0.00");
                    totalTax = Convert.ToDouble(totalTax.ToString("0.00"));
                    // Math.Round(totalTax, 00);
                    txtGrandTotal.Text = (((subTotal + totalTax + totalChrg) - totalDisc)).ToString();
                    TotFinal = Math.Round(Convert.ToDouble(txtGrandTotal.Text), MidpointRounding.AwayFromZero);
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff)) == true)
                    {
                        txtRoundOff.Text = (TotFinal - Convert.ToDouble(txtGrandTotal.Text)).ToString("0.00");
                        txtGrandTotal.Text = ((subTotal + totalTax + totalChrg + Convert.ToDouble(txtRoundOff.Text)) - totalDisc).ToString("0.00");
                    }
                    else
                        txtRoundOff.Text = "0.00";

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
            double SubDiscAmt1 = 0;
            SubDiscRs = 0; SubTax1 = 0; SubTax2 = 0; SubTax3 = 0;
            double SubAmount = 0;
            double TaxAmt1 = 0, TaxAmt2 = 0, tRate = 0, ttax = 0, TaxAmt3 = 0;

            SubQty = Convert.ToDouble((txtBQuantity.Text == "") ? "1" : txtBQuantity.Text);
            SubRate = Convert.ToDouble((txtBRate.Text == "") ? "0" : txtBRate.Text);
            SubDiscPer = Convert.ToDouble((txtBDiscPer1.Text == "") ? "0" : txtBDiscPer1.Text);
            SubDiscRs = Convert.ToDouble((txtBDiscRs1.Text == "") ? "0" : txtBDiscRs1.Text);

            SubTax1 = Convert.ToDouble((txtBTaxPer1.Text == "") ? "0" : txtBTaxPer1.Text);//.ToDouble(dgBill.CurrentRow.Cells[ColIndex.SGSTPercentage].Value);
            SubTax2 = Convert.ToDouble((txtBTaxPer2.Text == "") ? "0" : txtBTaxPer2.Text);// Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.CGSTPercentage].Value);
            SubMktQty = Convert.ToDouble((txtBMKTQTY.Text == "") ? "1" : txtBMKTQTY.Text); //Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.MKTQuantity].Value);

            if (ObjFunction.GetListValue(lstUOM) == 4)
            {
                SubAmount = Math.Round(((SubQty * SubRate) / SubMktQty) / 1000, 2);
            }
            else
            {
                SubAmount = Math.Round((SubQty * SubRate) / SubMktQty, 2);
            }
            SubDiscAmt1 = Math.Round((SubAmount * SubDiscPer) / 100, 2);
            txtBDiscAmt1.Text = SubDiscAmt1.ToString();
            SubDiscRs = Math.Round(Convert.ToDouble(txtBDiscRs1.Text), 2);
            SubAmount = Math.Round(SubAmount - SubDiscAmt1 - SubDiscRs, 2);

            double TotalTaxPerce = Convert.ToDouble(SubTax1 + SubTax2 + SubTax3);//total Tax Percentage
            double tAmount = Convert.ToDouble(SubAmount);//discount minus amount
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == true)//Reverse or forward calculation
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
                TaxAmt1 = Math.Round(Convert.ToDouble((tAmount * SubTax1) / 100), 2);
                TaxAmt2 = Math.Round(Convert.ToDouble((tAmount * SubTax2) / 100), 2);
                TaxAmt3 = Math.Round(Convert.ToDouble((tAmount * SubTax3) / 100), 2);
            }
            txtBAmount.Text = tAmount.ToString("0.00");
        }

        public void CalculateRate()
        {
            tAmount = 0.00; TotFinal = 0.00; Disc1 = 0.00; Disc2 = 0.00; TaxAmt = 0.00; TotalAmt = 0.00; TaxAmt2 = 0; ttRate = 0; ttax = 0; TaxAmt3 = 0;
            double TaxPerce = 0.00, Amount = 0.00, DiscAmt = 0.00, ACessPer = 0.00, ADhekhrekper = 0.00, ACessValue = 0.00, ADhekhrekValue = 0.00, APMCAMOUNT = 0, PAckingCharges = 0.00;

            if ((dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value != null) && (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value != null))
            {
                ADhekhrekper = 0;
                ACessPer = 0;
                PAckingCharges = (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PackagingCharges].Value));

                if (Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.UOMNo].Value) == 4)
                {
                    Amount = Convert.ToDouble(((((Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MKTQuantity].Value))) / 1000).ToString("0.0000"));
                }
                else
                {
                    Amount = Convert.ToDouble((((Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value)) * (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value))) / (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MKTQuantity].Value))).ToString("0.0000"));
                }

                Disc1 = Convert.ToDouble(((Amount * Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DiscPercentage].Value)) / 100).ToString("0.0000"));
                Disc2 = Convert.ToDouble((((Amount - (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DiscRupees].Value) + Disc1)) * Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DiscPercentage2].Value)) / 100).ToString("0.0000"));
                DiscAmt = (Disc1 + Disc2);
                DiscAmt += Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DiscRupees].Value) + Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DiscRupees2].Value);
                SubQty = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value);
                SubMktQty = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.MKTQuantity].Value);
                tAmount = (Amount - DiscAmt) / SubQty;
                //apmc calculation=====================
                ACessValue = (tAmount * ACessPer) / 100;
                ADhekhrekValue = (ACessValue * ADhekhrekper) / 100;

                APMCAMOUNT = PAckingCharges + ACessValue + ADhekhrekValue;

                TaxPerce = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SGSTPercentage].Value);

                TaxPerce2 = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.CGSTPercentage].Value);
                TaxPerce3 = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IGSTPercentage].Value);
                TotalTaxPerce = TaxPerce + TaxPerce2 + TaxPerce3;
                TaxAmt = 0; TaxAmt2 = 0; ttRate = 0; ttax = 0; TaxAmt3 = 0;
                ttRate = Math.Round(Convert.ToDouble(((tAmount))), 2);
                TaxAmt = Math.Round(Convert.ToDouble(((tAmount) * TaxPerce) / 100), 2);
                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount) * TaxPerce2) / 100), 2);
                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount) * TaxPerce3) / 100), 2);
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value = (ttRate + TaxAmt + TaxAmt2 + TaxAmt3).ToString("0.00");
            }

        }

        private void Disc_MoveNext()
        {
            CalculateTotal();
        }

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
                    if (DBGetVal.KachhaFirm == true)
                    {
                        txtItemName.Text = ObjQry.ReturnString("select case when esflag='False' then upper(Itemgroupname + ' ' + Itemname +'*') else  upper(Itemgroupname + ' ' + Itemname)  end as name from MItemMaster inner join MItemGroup on MItemMaster.groupno=MItemGroup.itemgroupno where itemno =" + BItemNo, CommonFunctions.ConStr);//Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value)
                    }
                    else
                    {

                        txtItemName.Text = ObjQry.ReturnString("select upper(Itemgroupname + ' ' + Itemname) as name from MItemMaster inner join MItemGroup on MItemMaster.groupno=MItemGroup.itemgroupno where itemno =" + BItemNo, CommonFunctions.ConStr);
                    }
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
                    lstUOM.SelectedValue = dtRateSetting.Rows[0]["Uomno"].ToString();
                    txtBUom.Text = lstUOM.Text;
                    txtBTaxPer1.Text = dtRateSetting.Rows[0]["Percentage"].ToString();
                    txtBTaxPer2.Text = dtRateSetting.Rows[1]["Percentage"].ToString();
                    txtBMKTQTY.Text = dtRateSetting.Rows[0]["MKTQty"].ToString();
                    txtBStockConversion.Text = dtRateSetting.Rows[0]["StockConversion"].ToString();
                    txtMRP.Text = dtRateSetting.Rows[1]["MRP"].ToString();
                    txtBQuantity.Text = "1";
                    txtBDiscAmt1.Text = "0.00";
                    txtBDiscRs1.Text = "0.00";
                    txtBDiscPer1.Text = "0.00";
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
                //Clear();
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

        public void Validations()
        {

            try
            {
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if ((Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value) == 0) || (Convert.ToDouble(dgBill[ColIndex.Rate, i].Value) == 0))
                    {
                        if (dgBill.Rows.Count == 1)
                        {
                            dgBill.Rows.RemoveAt(i);
                            dgBill.Rows.Add();
                        }
                        else
                        {
                            dgBill.Rows.RemoveAt(i);
                            i = i - 1;
                        }
                    }
                }
                dgBill.Rows.Add();
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
                if (isSavingTransaction)
                    return;
                isSavingTransaction = true;
                hidePics();
                pnlDeliveryAddress.Visible = false;
                double debit = 0;
                if (btnSave.Enabled == false)
                {
                    isSavingTransaction = false;
                    return;
                }
                btnSave.Enabled = true;
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    isSavingTransaction = false;
                    txtBarCode.Focus();
                    return;
                }

                if (ID == 0)
                    Validations();
                CalculateTotal();
                if (Convert.ToDouble(txtGrandTotal.Text) <= 0)
                {
                    OMMessageBox.Show("Negative or ZERO Bill amount not allowed ...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    isSavingTransaction = false;
                    dgBill.Rows.Clear();
                    dgBill.Rows.Add();

                    dgBill.Focus();
                    return;
                }
                if (iPayTypeControlUnder == 2)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AskPayableAmount)) == true)
                    {
                        Form NewF = new Vouchers.BillCalculator(Convert.ToDouble(txtGrandTotal.Text));
                        ObjFunction.OpenForm(NewF);
                        NewF.Left = 15;
                        if (((BillCalculator)NewF).DS == DialogResult.Cancel)
                        {
                            isSavingTransaction = false;
                            return;
                        }
                    }
                }

                dbTVoucherEntry = new DBTVaucherEntry();
     
                DeleteValues();

                VoucherUserNo = Convert.ToInt64(txtInvNo.Text.Trim());
                int VoucherSrNo = 1;
                //Voucher Header Entry 
                tVoucherEntry = new TVoucherEntry();
                tVoucherEntry.PkVoucherNo = ID;

                tVoucherEntry.VoucherTypeCode = VoucherType;
                if ((ManualBill == true) && (TempBillNo != Convert.ToInt32(txtInvNo.Text)))
                {
                    tVoucherEntry.VoucherUserNo = Convert.ToInt32(txtInvNo.Text);
                    tVoucherEntry.SuppCategory = 1;
                }
                else
                {
                    tVoucherEntry.VoucherUserNo = VoucherUserNo;
                }
                tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                tVoucherEntry.VoucherTime = dtpBillTime.Value;
                tVoucherEntry.Narration = "Quotation";
                tVoucherEntry.Reference = txtPartyName.Text;
                tVoucherEntry.ChequeNo = 0;
                tVoucherEntry.ClearingDate = dtpBillDate.Value;
                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                tVoucherEntry.BilledAmount = Convert.ToDouble(txtGrandTotal.Text);
                tVoucherEntry.ChallanNo = "";
                tVoucherEntry.Remark = txtRemark.Text.Trim();
                tVoucherEntry.MacNo = DBGetVal.MacNo;
                tVoucherEntry.PayTypeNo = 2;
                tVoucherEntry.RateTypeNo = GetRateType();
                tVoucherEntry.OrderType = OrderType;
                tVoucherEntry.DiscPercent = (txtDiscount1Per.Text == "") ? 0 : Convert.ToDouble(txtDiscount1Per.Text);
                tVoucherEntry.DiscAmt = (txtDiscRupees1.Text == "") ? 0 : Convert.ToDouble(txtDiscRupees1.Text);
                tVoucherEntry.MixMode = MixModeVal;

                tVoucherEntry.UserID = DBGetVal.UserID;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAskUserPassword)) == true)
                    tVoucherEntry.UserID = Utilities.PasswordAsk.UserID == 0 ? DBGetVal.UserID : Utilities.PasswordAsk.UserID;
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
                tVoucherEntry.LedgerNo = PartyNo;
                tVoucherEntry.PkRefNo = PID;//for refrence dc and bill 
                tVoucherEntry.EffectiveDate = dtpDueDate.Value;
                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);
                //--umesh-22-10-18
                if (DBGetVal.KachhaFirm == true) //for Reference entry sales bill deleted when estimate bill is updated   
                    dbTVoucherEntry.DeleteAllVoucherEntryNew(PID);

                DataTable dtVoucherDetails = new DataTable();
                dtVoucherDetails = new DataTable();
                dtVchPrev = new DataTable();
                if (ID != 0)
                {
                    dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + ID + " order by VoucherSrNo").Table;
                    if (PayType != 3)
                        dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " and srno!=508  order by VoucherSrNo").Table;
                    else
                        dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,0.00 AS Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + ID + " and srno!=508  order by VoucherSrNo").Table;
                }
                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                tVoucherDetails.SignCode = 1;
                tVoucherDetails.LedgerNo = PartyNo;
                tVoucherDetails.Debit = Convert.ToDouble(txtGrandTotal.Text);
                tVoucherDetails.Credit = 0;
                tVoucherDetails.Narration = "";
                tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                tVoucherDetails.SrNo = Others.Party;
                dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

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
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAskUserPassword)) == true)
                            tStock.UserID = Utilities.PasswordAsk.UserID == 0 ? DBGetVal.UserID : Utilities.PasswordAsk.UserID;
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
                        tStock.DisplayItemName = dgBill[ColIndex.DisplayName, i].EditedFormattedValue.ToString();
                        tStock.TRWeight = ((dgBill[ColIndex.TariffWt, i].Value) == null) ? 0.00 : Convert.ToDouble(dgBill[ColIndex.TariffWt, i].Value.ToString());
                        tStock.GRWeight = ((dgBill[ColIndex.GrossWt, i].Value) == null) ? 0.00 : Convert.ToDouble(dgBill[ColIndex.GrossWt, i].Value.ToString());
                        tStock.Remarks = ((dgBill[ColIndex.Remarks, i].Value) == null) ? "" : (dgBill[ColIndex.Remarks, i].Value.ToString());
                        tStock.Freight = 0;
                        tStock.OtherCharges = 0;
                        tStock.SalesMan = ((dgBill[ColIndex.SalesMan, i].Value) == null) ? 0 : Convert.ToInt32(dgBill[ColIndex.SalesMan, i].Value.ToString());
                        tStock.PackagingCharges = ((dgBill[ColIndex.PackagingCharges, i].Value) == null) ? 0.00 : Convert.ToDouble(dgBill[ColIndex.PackagingCharges, i].Value.ToString());
                        tStock.IType = ((dgBill[ColIndex.ESFlag, i].Value) == null) ? true : Convert.ToBoolean(dgBill[ColIndex.ESFlag, i].Value.ToString());
                        dbTVoucherEntry.AddTStock(tStock);
                      
                        if ((dgBill[ColIndex.IsRateChange, i].Value != null && dgBill[ColIndex.IsRateChange, i].Value.ToString() == "1") || (Convert.ToDouble(dgBill[ColIndex.DiscPercentage, i].Value.ToString()) > 0.00))
                        {
                            if (HistoryMaintain == true && Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 2)
                            {
                                mLedgerRate = new MLedgerRateSetting();
                                mLedgerRate.PkSrNo = ObjQry.ReturnLong("Select PkSrNo From MLedgerRateSetting Where LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " AND ItemNo=" + dgBill[ColIndex.ItemNo, i].Value + " AND UOMNo=" + dgBill[ColIndex.UOMNo, i].Value.ToString() + " AND MRP=" + dgBill[ColIndex.MRP, i].Value.ToString() + "", CommonFunctions.ConStr);
                                mLedgerRate.LedgerNo = ObjFunction.GetListValue(lstPartyEnglish);
                                mLedgerRate.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                                mLedgerRate.UOMNo = Convert.ToInt64(dgBill[ColIndex.UOMNo, i].Value.ToString());
                                mLedgerRate.Rate = Convert.ToDouble(dgBill[ColIndex.Rate, i].Value.ToString());
                                mLedgerRate.MRP = Convert.ToDouble(dgBill[ColIndex.MRP, i].Value.ToString());
                                mLedgerRate.DiscPercentage = Convert.ToDouble(dgBill[ColIndex.DiscPercentage, i].Value.ToString());
                                mLedgerRate.DiscAmount = 0;
                                mLedgerRate.UserID = DBGetVal.UserID;
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAskUserPassword)) == true)
                                    mLedgerRate.UserID = Utilities.PasswordAsk.UserID == 0 ? DBGetVal.UserID : Utilities.PasswordAsk.UserID;
                                mLedgerRate.UserDate = DateTime.Now.Date;
                                mLedgerRate.CompNo = DBGetVal.FirmNo;
                                dbTVoucherEntry.AddMLedgerRateSetting(mLedgerRate);
                            }
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Please check item company no...");
                    }
                }

                #region //for Sales bill tax entry for regular direct main bill 
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
                            tVoucherDetails.Debit = 0;
                            tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                            tVoucherDetails.Credit = debit;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
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
                            tVoucherDetails.Debit = 0;
                            tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                            tVoucherDetails.Credit = debit;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
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
                            tVoucherDetails.Debit = 0;
                            tVoucherDetails.CompanyNo = Convert.ToInt64(dgBill.Rows[ledgerNo].Cells[ColIndex.StockCompanyNo].Value);
                            tVoucherDetails.Credit = debit;// (GetRatio(tVoucherDetails.CompanyNo) * debit) / 10;
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
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount2));
                    tVoucherDetails.Debit = Convert.ToDouble(txtSchemeDisc.Text);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Discount2;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                }
                //For Discount Ledger 1 %
                if (Convert.ToDouble(txtDiscRupees1.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                    tVoucherDetails.Debit = Convert.ToDouble(txtDiscRupees1.Text);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Discount1;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                }
                //For Footer Other  disc
                if (Convert.ToDouble(txtOtherDisc.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount3));
                    tVoucherDetails.Debit = Convert.ToDouble(txtOtherDisc.Text);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Discount3;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
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
                if (Convert.ToDouble(txtChrgRupees1.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1));
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(txtChrgRupees1.Text);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Charges1;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                }
                //For Charges Rupees 2
                if (Convert.ToDouble(txtChrgRupees2.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges2));
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(txtChrgRupees2.Text);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.Charges2;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                }
                //For Round Off Acc Ledger
                if (Convert.ToDouble(txtRoundOff.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc));
                    if (Convert.ToDouble(txtRoundOff.Text) >= 0)
                    {
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.Debit = 0;
                        tVoucherDetails.Credit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));
                    }
                    else
                    {
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.Debit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));
                        tVoucherDetails.Credit = 0;
                    }
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.SrNo = Others.RoundOff;
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                }
                if (ID != 0)
                {
                    for (int i = 0; i < dtVoucherDetails.Rows.Count; i++)
                    {
                        if (dtVoucherDetails.Rows[i].ItemArray[2].ToString() == "0")
                            DeleteDtls(2, Convert.ToInt64(dtVoucherDetails.Rows[i].ItemArray[0].ToString()));
                    }
                    DeleteValues();//===umesh transfer
                }
 
                if (IsDeliveryAddress == true)
                {
                    tdeliveryaddress = new TDeliveryAddress();
                    tdeliveryaddress.PkSrNo = 0;
                    tdeliveryaddress.Ledgerno = PartyNo;
                    tdeliveryaddress.LedgerDetailsNo = LedgerDetailsNo;
                    tdeliveryaddress.UserId = DBGetVal.UserID;
                    tdeliveryaddress.UserDate = DBGetVal.ServerTime.Date;
                    tdeliveryaddress.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTDeliveryAddress(tdeliveryaddress);

                }
                long tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    FooterDiscDtlsNo = 0;                
                    viewmode();
                    IsDeliveryAddress = false;
                    btnDeliveryAddress.Visible = false;
                    string strVChNo = ObjQry.ReturnLong("Select VoucherUserNo From TVoucherEntry Where PKVoucherNo=" + tempID + "", CommonFunctions.ConStr).ToString();
                    DisplayMessage("Bill No " + strVChNo + " Added Successfully");
                    ID = tempID;

                    #region //print
                    if (PrintAsk == 0)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrint)) == true && (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsEachTimeInitialise)) == true))
                        {
                            DialogResult ds = OMMessageBox.Show("Want to print this bill?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, GetBillPrintAsk(), "Preview");
                            if (ds == DialogResult.Yes)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                                {
                                    Form NewFF = new Utilities.PrintCount(IsPrintCount);
                                    ObjFunction.OpenForm(NewFF);
                                    if (Utilities.PrintCount.NoPrintCount != -1)
                                    {
                                        IsPrintCount = Utilities.PrintCount.NoPrintCount;
                                        PrintBill(0);
                                    }
                                }
                                else PrintBill(0);
                            }
                            else if (ds == DialogResult.Cancel)
                                PrintBill(1);

                        }
                        else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsEachTimeInitialise)) == true && (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrint)) == false))
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                            {
                                Form NewFF = new Utilities.PrintCount(IsPrintCount);
                                ObjFunction.OpenForm(NewFF);
                                if (Utilities.PrintCount.NoPrintCount != -1)
                                {
                                    IsPrintCount = Utilities.PrintCount.NoPrintCount;
                                    PrintBill(0);
                                }
                            }
                            else PrintBill(0);
                        }
                        else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsEachTimeInitialise)) == false && (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrint)) == true))
                        {
                            DialogResult ds = OMMessageBox.Show("Want to print this bill?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, GetBillPrintAsk(), "Preview");
                            if (ds == DialogResult.Yes)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                                {
                                    Form NewFF = new Utilities.PrintCount(IsPrintCount);
                                    ObjFunction.OpenForm(NewFF);
                                    if (Utilities.PrintCount.NoPrintCount != -1)
                                    {
                                        IsPrintCount = Utilities.PrintCount.NoPrintCount;
                                        PrintBill(0);
                                    }
                                }
                                else PrintBill(0);
                            }
                            else if (ds == DialogResult.Cancel)
                                PrintBill(1);
                        }
                    }
                    else if (PrintAsk == 1)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrint)) == true)
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                            {
                                Form NewFF = new Utilities.PrintCount(IsPrintCount);
                                ObjFunction.OpenForm(NewFF);
                                if (Utilities.PrintCount.NoPrintCount != -1)
                                {
                                    IsPrintCount = Utilities.PrintCount.NoPrintCount;
                                    PrintBill(0);
                                }
                            }
                            else PrintBill(0);
                    }
                    #endregion
                    FillField();
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    rbEnglish.Enabled = true;
                    rbMarathi.Enabled = true;
                    btnCashSave.Visible = false;
                    btnSearch.Visible = true;
                    btnPrint.Visible = true;
                    dgBill.Enabled = false;
                    btnNew.Focus();
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

            isSavingTransaction = false;
        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            lblGrandTotal.Text = txtGrandTotal.Text;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void txtSalesMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSalesMan.Text == "")
                {
                    pnlSalesMan.Visible = true;
                    lstSalesMan.Focus();
                }
                else { dgBill.Focus(); }
            }
        }

        private void lstSalesMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSalesMan.Text = lstSalesMan.Text;
                dgBill.Focus();
                pnlSalesMan.Visible = false;

                for (int i = 0; i <= dgBill.Rows.Count - 1; i++)
                {
                    dgBill.Rows[i].Cells[ColIndex.SalesMan].Value = lstSalesMan.SelectedValue;
                }

            }

        }

        private void dgDeliveryAddress_KeyDown(object sender, KeyEventArgs e)
        {
            pnlDeliveryAddress.Visible = false;
            IsDeliveryAddress = true;
            LedgerDetailsNo = (dgDeliveryAddress.CurrentRow.Cells[0].Value == null) ? 0 : Convert.ToInt32(dgDeliveryAddress.CurrentRow.Cells[0].Value);

        }

        private void btnDiliveryAddress_Click(object sender, EventArgs e)
        {
            pnlDeliveryAddress.Visible = false;
        }

        private void btnDeliveryAddress_Click(object sender, EventArgs e)
        {

            btnDiliveryAddress.Visible = true;
            FilldgDeliveryAddress();
            pnlDeliveryAddress.Visible = true;
        }
 
        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnPartyCancel_Click(object sender, EventArgs e)
        {
            pnlPartyName.Visible = false;
        }

        private void btnParkingCancel_Click(object sender, EventArgs e)
        {
            pnlMainParking.Visible = false;
        }

        private void btnDcPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                DialogResult ds = OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                if (ds == DialogResult.Yes)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                    {
                        Form NewFF = new Utilities.PrintCount(IsPrintCount);
                        ObjFunction.OpenForm(NewFF);
                        if (Utilities.PrintCount.NoPrintCount != -1)
                        {
                            IsPrintCount = Utilities.PrintCount.NoPrintCount;
                            DCPrintBill(0);
                        }
                    }
                    else DCPrintBill(0);
                }
                else if (ds == DialogResult.Cancel)
                    DCPrintBill(1);

            }
        }

        public void DCPrintBill(int PrintType)
        {
            try
            {
                string AddressPrint = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true)
                {
                    if (OrderType == 1)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillCouterBill)) == true)
                            AddressPrint = "0";
                        else
                            AddressPrint = "1";
                    }
                    if (OrderType == 2)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillHomeDelivery)) == true)
                            AddressPrint = "0";
                        else
                            AddressPrint = "1";
                    }
                }
                double Amt = 0;
                Amt = ObjQry.ReturnDouble("Select sum(Case When TStock.Rate <>0 Then (((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount) Else MRateSetting.MRP*TStock.Quantity END) FROM TStock INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                                                  " Where TStock.FkVoucherNo=" + ID + " AND (Case When TStock.Rate <>0 Then ((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount Else MRateSetting.MRP*TStock.Quantity END)>0 ", CommonFunctions.ConStr);
                Amt += Convert.ToDouble(txtTotalAnotherDisc.Text);// +Convert.ToDouble(txtTotalDisc.Text);
                string[] ReportSession;

                ReportSession = new string[22];
                ReportSession[0] = ID.ToString();
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "No Payment";
                ReportSession[7] = (txtTotalAnotherDisc.Text == "") ? "0" : txtTotalAnotherDisc.Text;
                ReportSession[8] = (txtTotalChrgs.Text == "") ? "0" : txtTotalChrgs.Text;
                ReportSession[9] = (rbEnglish.Checked == true) ? "1" : "2";
                ReportSession[10] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSavingBill)).ToString();
                ReportSession[11] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)).ToString();

                ReportSession[12] = (MixModeVal == 1) ? "1" : "0";
                ReportSession[13] = AddressPrint;//(Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                ReportSession[14] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff)) == true) ? "1" : "2";
                ReportSession[15] = (ObjQry.ReturnLong("Select Count(*) FRom TReward Where FkVoucherNo=" + ID + "", CommonFunctions.ConStr) == 0) ? "0" : "1";
                ReportSession[16] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSchemeDetails)) == true) ? "1" : "0";
                ReportSession[17] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? "1" : "2";
                ReportSession[18] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? txtRemark.Text : "";
                ReportSession[19] = (ShowVATNo == true) ? "1" : "2";

                string AmountIn = "";
                string str = Convert.ToString(NumberToWordsIndian.getWords(txtGrandTotal.Text));
                AmountIn = str.Substring(0, str.Length - 14);
                // ReportSession[20] = NumberToWordsIndian.getWords(txtGrandTotal.Text);
                ReportSession[20] = AmountIn;

                #region New Code for Outstanding
                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                //{
                //    //double TotalDues = 0;

                //    //TotalDues = ObjQry.ReturnDouble("Select OpAmt from " +
                //    //                        " GetCurrentLedgerBalance(" + ObjFunction.GetListValue(lstPartyEnglish) + "," + DBGetVal.FirmNo + ")"
                //    //                        , CommonFunctions.ConStr);
                //    //if (TotalDues > 0)
                //    //{
                //    //    ReportSession[21] = "Total Dues:" + TotalDues.ToString("0.00");
                //    //}
                //    //else if (TotalDues <= 0)
                //    //    ReportSession[21] = "Total Dues: 0";
                //}
                //else
                //    ReportSession[21] = "0";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                {

                    ReportSession[21] = lblOutstanding.Text.ToString();
                }
                else
                    ReportSession[21] = "0";
                #endregion

                if (PrintType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    childForm = null;

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    {
                        childForm = ObjFunction.GetReportObject("Reports.GetDCPrint");
                    }
                    else
                    {
                        childForm = ObjFunction.LoadReportObject("GetDCPrint.rpt", CommonFunctions.ReportPath);
                    }
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            DisplayMessage("Bill Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("Bill not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
                else
                {
                    Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    {

                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetDCPrint.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else
                    {
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetDCPrint.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    if (NewF != null)
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void dgInvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long tempNo;
                    e.SuppressKeyPress = true;
                    tempNo = ObjQry.ReturnLong("Select PKVoucherNo From TVoucherEntry Where PkVoucherNo=" + Convert.ToInt64(dgInvSearch.Rows[dgInvSearch.CurrentRow.Index].Cells[4].Value) + " and VoucherTypeCode=" + VoucherType + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        FillField();
                        btnNew.Enabled = true;
                        btnBillCancel.Enabled = true;
                        btnUpdate.Enabled = true;
                        pnlInvSearch.Visible = false;
                        btnNew.Focus();
                        SearchVisible(false);
                    }
                    else
                    {
                        txtSearch.Text = "";
                        DisplayMessage("Bill Not Found");
                        txtSearch.Focus();
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlInvSearch.Visible = false;
                    pnlSearch.Visible = true;
                    txtSearch.Focus();
                    rbType_CheckedChanged(sender, new EventArgs());
                }
                txtSearch.Text = "";
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpSearchDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                long tempNo;
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    long cnt = ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VoucherDate='" + dtpSearchDate.Text + "' and VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                    if (cnt > 1)
                    {
                        pnlInvSearch.Visible = true;
                        int x = dgBill.GetCellDisplayRectangle(0, 0, true).X + 200;//(Screen.PrimaryScreen.WorkingArea.Width) / 2;
                        int y = dgBill.GetCellDisplayRectangle(0, 0, true).Y + 100;
                        pnlInvSearch.Location = new Point(x, y);
                        string str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #], TVoucherEntry.VoucherDate AS 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                     "TVoucherEntry.PkVoucherNo FROM TVoucherEntry WHERE (TVoucherEntry.VoucherTypeCode IN (" + VoucherType + ")) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
                                     "And TVoucherEntry.VoucherDate='" + dtpSearchDate.Text + "' " +
                                     "Order By  TVoucherEntry.VoucherUserNo desc,TVoucherEntry.VoucherDate desc, TVoucherEntry.Reference desc";
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
                    tempNo = ObjQry.ReturnLong("Select PKVoucherNo From TVoucherEntry Where VoucherDate='" + dtpSearchDate.Text + "' and VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
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
                        rbDate.Focus();
                        dtpSearchDate.Focus();
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

        private void btnCancelSearch_Click_1(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnBillCancel.Enabled = true;
        }

        private void btnRefreshList_Click(object sender, EventArgs e)
        {
            string ItemList = strItemQuery[0];
            ItemList = ItemList.Replace("@FromDate", "" + dtpBillDate.Text.Trim() + "");
            ItemList = ItemList.Replace("=@CompNo@", "=" + DBGetVal.FirmNo);
            ObjFunction.FillList(lstGroup1, ItemList);
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                ItemList = ItemList.Replace("ItemGroupName from", "LangGroupName from");

                ObjFunction.FillList(lstGroup1Lang, ItemList.Replace("ItemGroupName From", "LangGroupName from"));
            }
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where VoucherTypeCode=" + VoucherType + "  ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["PkVoucherNo"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(max(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where VoucherTypeCode=" + VoucherType + "  ").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["PkVoucherNo"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {

                    dtSearch = ObjFunction.GetDataView("Select isnull(min(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where  PkVoucherNo >" + ID + " AND  VoucherTypeCode=" + VoucherType + "  ").Table;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(PkVoucherNo),0)as PkVoucherNo From TVoucherEntry Where   PkVoucherNo <" + ID + " AND  VoucherTypeCode=" + VoucherType + "   ").Table;
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
                if (ID != 0)
                {
                    FillField();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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

        private void btnBigPrint_Click(object sender, EventArgs e)
        {
            if (PID != 0)
            {
                DialogResult ds = OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                if (ds == DialogResult.Yes)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                    {
                        Form NewFF = new Utilities.PrintCount(IsPrintCount);
                        ObjFunction.OpenForm(NewFF);
                        if (Utilities.PrintCount.NoPrintCount != -1)
                        {
                            IsPrintCount = Utilities.PrintCount.NoPrintCount;
                            BigPrint(0);
                        }
                    }
                    else BigPrint(0);
                }
                else if (ds == DialogResult.Cancel)
                    BigPrint(1);

            }
        }

        public void BigPrint(int PrintType)
        {
            try
            {
                string AddressPrint = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true)
                {
                    if (OrderType == 1)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillCouterBill)) == true)
                            AddressPrint = "0";
                        else
                            AddressPrint = "1";
                    }
                    if (OrderType == 2)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillHomeDelivery)) == true)
                            AddressPrint = "0";
                        else
                            AddressPrint = "1";
                    }
                }
                double Amt = 0;
                //  Amt = ObjQry.ReturnDouble("Select sum(Case When TStock.Rate <>0 Then (((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount) Else MRateSetting.MRP*TStock.Quantity END) FROM TStock INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                //                     " Where TStock.FkVoucherNo=" + ID + " AND (Case When TStock.Rate <>0 Then ((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount Else MRateSetting.MRP*TStock.Quantity END)>0 ", CommonFunctions.ConStr);
                Amt += Convert.ToDouble(txtTotalAnotherDisc.Text);// +Convert.ToDouble(txtTotalDisc.Text);
                string[] ReportSession;

                ReportSession = new string[22];
                ReportSession[0] = PID.ToString();
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "No Payment";
                ReportSession[7] = (txtTotalAnotherDisc.Text == "") ? "0" : txtTotalAnotherDisc.Text;
                ReportSession[8] = (txtTotalChrgs.Text == "") ? "0" : txtTotalChrgs.Text;
                ReportSession[9] = (rbEnglish.Checked == true) ? "1" : "2";
                ReportSession[10] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSavingBill)).ToString();
                ReportSession[11] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)).ToString();

                ReportSession[12] = (MixModeVal == 1) ? "1" : "0";

                ReportSession[13] = AddressPrint;//(Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                ReportSession[14] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff)) == true) ? "1" : "2";
                //ReportSession[13] = (btnMixMode.Visible == true && MixModeVal == 1) ? "0" : "1";
                ReportSession[15] = (ObjQry.ReturnLong("Select Count(*) FRom TReward Where FkVoucherNo=" + ID + "", CommonFunctions.ConStr) == 0) ? "0" : "1";
                ReportSession[16] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSchemeDetails)) == true) ? "1" : "0";
                ReportSession[17] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? "1" : "2";
                ReportSession[18] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? txtRemark.Text : "";
                ReportSession[19] = (ShowVATNo == true) ? "1" : "2";
                //string[] str = new string[] { "and zero paise" };
                string AmountIn = "";
                string str = Convert.ToString(NumberToWordsIndian.getWords(txtGrandTotal.Text));
                AmountIn = str.Substring(0, str.Length - 14);
                // ReportSession[20] = NumberToWordsIndian.getWords(txtGrandTotal.Text);
                ReportSession[20] = AmountIn;

                #region New Code for Outstanding
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                {

                    ReportSession[21] = lblOutstanding.Text.ToString();
                }
                else
                    ReportSession[21] = "0";

                #endregion

                if (PrintType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    childForm = null;
                    if (rbEnglish.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBillMRP");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBillMRP");
                                }

                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBill");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBill");
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("DCGetBillMRP.rpt", CommonFunctions.ReportPath);
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath);
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBill-a4.rpt", CommonFunctions.ReportPath);
                                    //childForm = ObjFunction.LoadReportObject("GetBillAPMC.rpt", CommonFunctions.ReportPath);
                                }

                            }
                        }
                    }
                    else if (rbMarathi.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBillMarMRP");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBillMarMRP");
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBillMar");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBillMar");
                                }
                            }

                        }
                        else
                        {


                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("DCGetBillMarMRP.rpt", CommonFunctions.ReportPath);
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMarPakka-A5.rpt", CommonFunctions.ReportPath);// print
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath);
                                }

                            }
                        }
                    }
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        objRpt.PrintCount = IsPrintCount;
                        if (objRpt.PrintReport() == true)
                        {
                            IsPrintCount = 1;
                            DisplayMessage("Bill Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("Bill not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
                else
                {
                    Form NewF = null;


                    if (rbEnglish.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)

                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBill.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBill.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }

                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }


                        }
                    }
                    else if (rbMarathi.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)

                            {
                                if (DBGetVal.KachhaFirm == true)
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                                else
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }

                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)

                            {
                                if (DBGetVal.KachhaFirm == true)
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                                else
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarPakka-A5.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }

                        }
                    }
                    if (NewF != null)
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtCustCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtCustCode.Text != "")
                {
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true)
                    //{
                    lstPartyEnglish.SelectedValue = txtCustCode.Text;
                    txtParty.Text = lstPartyEnglish.Text;
                    lstPartEnglish_KeyDown(sender, e);
                    txtParty.Focus();
                    //}
                }
                else
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true)
                    {
                        pnlParty.Visible = true;
                        lstPartyEnglish.Focus();
                    }
                    else
                    {
                        dgBill.Focus();
                    }
                }
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
            else if (e.KeyCode == Keys.F3)
            {
                if (btnSave.Visible)
                {
                    PrintAsk = 0;
                    btnSave_Click(sender, e);
                }
            }           
            else if (e.KeyCode == Keys.F1)
            {
                e.SuppressKeyPress = true;
                if (pnlFooterInfo.Visible == false)
                {
                    pnlFooterInfo.Dock = DockStyle.Bottom;
                    //pnlFooterInfo.Height = 30;
                    pnlFooterInfo.BorderStyle = BorderStyle.None;
                    pnlFooterInfo.BringToFront();
                    pnlFooterInfo.Visible = true;
                }
                else
                {
                    pnlFooterInfo.Visible = false;
                }
            }
            else if (e.KeyCode == Keys.F7)
            {
                if (dgBill.Focused == true)
                {
                    if (dgBill.Rows.Count > 0)
                    {
                        if (dgBill.CurrentCell.ColumnIndex == 2)
                        {
                            dgBill.CurrentCell.ReadOnly = false;

                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                //btnExit_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                //if (txtDiscount1.Enabled) txtDiscount1.Focus();
            }

            if (e.KeyCode == Keys.P && e.Control)
            {
                if (ID != 0)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                    {
                        Form NewFF = new Utilities.PrintCount(IsPrintCount);
                        ObjFunction.OpenForm(NewFF);
                        if (Utilities.PrintCount.NoPrintCount != -1)
                        {
                            IsPrintCount = Utilities.PrintCount.NoPrintCount;
                            PrintBill(0);
                        }
                    }
                    else PrintBill(0);
                }
            }
            else if (e.KeyCode == Keys.C && e.Control)
            {
                if ((btnSave.Visible == true) && (dgItemList.Visible == false))
                {

                    if (OMMessageBox.Show("Are you sure want to cancel this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                        IsCancel = true;

                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentCell.RowIndex];
                        dgBill.CurrentCell.Value = "";
                        Desc_Start();
                    }
                }
            }

            else if (e.KeyCode == Keys.T && e.Control)
            {
                if (ID != 0)
                {
                    ShowVATNo = true;
                    PrintBill(0);
                    ShowVATNo = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowVatNo));
                }
            }
            else if ((e.KeyCode == Keys.F5))
            {
                if (ParkBillNo != 0)
                {
                    if (ObjTrans.Execute("Update TParkingBill set IsCancel='true' where ParkingBillNo=" + ParkBillNo + "", CommonFunctions.ConStr) == true)
                        ParkBillNo = 0;
                }
                if (dgBill.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                else if (ID == 0 && ParkBillNo == 0)
                {
                    pnlMainParking.Visible = true;
                    if (ObjFunction.GetListValue(lstPartyEnglish) != Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))
                        txtPersonName.Text = lstPartyEnglish.Text;
                    else
                        txtPersonName.Text = "";
                    txtPersonName.Focus();
                }
            }
            else if (e.KeyCode == Keys.F6)
            {
                if (ID == 0)
                {
                    if (dgBill.Rows.Count == 1)
                    {
                        if (pnlItemName.Visible == false)
                        {
                            dgParkingBills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            ShowParkingBill();
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.O && e.Control)
            {
                if (DBGetVal.IsAdmin == true)
                {
                    if (btnNew.Visible == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_RateTypeAskPassword)) == true)
                        {
                            txtRateTypePassword.Enabled = true;
                            pnlRateType.Visible = true;
                            txtRateTypePassword.Text = "";
                            txtRateTypePassword.Focus();
                        }
                    }
                }
            }
        
        }
        #endregion

        public void ShowParkingBill()
        {
            try
            {
                new GridSearch(dgParkingBills, 1);
                DataTable dtParking = ObjFunction.GetDataView("SELECT BillNo, PersonName AS Name " +
                    ",(SELECT SUM(Qty)FROM TParkingBillDetails WHERE (ParkingBillNo = TParkingBill.ParkingBillNo)) AS ItemQty,BillDate, BillTime,ParkingBillNo " +
                                                              "FROM TParkingBill Where IsBill='false' and IsCancel='false' Order by BillNo desc").Table;
                if (dtParking.Rows.Count > 0)
                {
                    pnlParking.Visible = true;
                    dgParkingBills.DataSource = dtParking.DefaultView;
                    dgParkingBills.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgParkingBills.Focus();
                    dgParkingBills.CurrentCell = dgParkingBills[0, dgParkingBills.CurrentRow.Index];

                }
                else
                    DisplayMessage("Parking Bills not Available...");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
   
        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            if (DBGetVal.IsAdmin == false)
            {
                OMMessageBox.Show("This facility not allowed to user.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                return;
            }
            if (BillSizeFlag == false)
            {
                dgBill.Height = dgBill.Height - (pnlTotalAmt.Height + 10);
                pnlTotalAmt.Location = new Point(dgBill.Height + 10, pnlTotalAmt.Location.Y);
                pnlTotalAmt.Location = new Point(dgBill.Width - pnlTotalAmt.Width + 10, dgBill.Location.Y + dgBill.Height + 10);
                pnlTotalAmt.Visible = true;
                BillSizeFlag = true;

            }
            else
            {
                BillSizeFlag = false;
                pnlTotalAmt.Visible = false;
                dgBill.Height = dgBill.Height + (pnlTotalAmt.Height + 10);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == false)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillUpdate)) == true)
                    {
                        Form frm = new Utilities.PasswordAsk(2);
                        ObjFunction.OpenForm(frm);
                        if (Utilities.PasswordAsk.IsAllow == 0) return;
                    }
                    if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                    {
                        btnUpdate.Visible = false;
                        btnBillCancel.Visible = false;
                        btnMixMode.Visible = false;
                        OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + " and TR.TypeOfRef in(6))", CommonFunctions.ConStr) > 1)
                        {
                            btnUpdate.Visible = false;
                            btnBillCancel.Visible = false;
                            btnMixMode.Visible = false;
                            OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            return;
                        }
                    }
                    btnCashSave.Visible = true;
                }    
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtInvNo.Enabled = false;
                btnBillCancel.Enabled = false;

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_RateTypeAskPassword)) == true)
                    btnSearch.Visible = false;
                btnPrint.Visible = false;
                dgBill.Enabled = true;
                dgBill.Focus();
                btnBillCancel.Visible = false;
                btnMixMode.Visible = false;
                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                if (ID != 0)
                {

                }
                txtSchemeDisc.Enabled = false;
                txtOtherDisc.Enabled = false;
                txtChrgRupees2.Enabled = false;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true)
                {
                    if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                    {

                    }
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsRateChangeByUser)) == true)
                {
                    if (UserType != 1)
                    {
                        dgBill.Columns[ColIndex.Rate].ReadOnly = true;
                        dgBill.Columns[ColIndex.DiscPercentage].ReadOnly = true;
                        dgBill.Columns[ColIndex.DiscAmount].ReadOnly = true;
                        dgBill.Columns[ColIndex.DiscRupees].ReadOnly = true;
                    }
                }
                btnDeliveryAddress.Visible = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnBillCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    btnMixMode.Visible = false;
                    OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + " and TR.TypeOfRef in(6))", CommonFunctions.ConStr) > 1)
                    {
                        btnUpdate.Visible = false;
                        btnBillCancel.Visible = false;
                        btnMixMode.Visible = false;
                        OMMessageBox.Show("Already this bill is amount collected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                    }
                    else if (ObjQry.ReturnInteger("SELECT  COUNT(*) FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                    " WHERE     (TVoucherEntry.Reference = " + txtInvNo.Text + ") AND (TVoucherEntry.VoucherTypeCode = " + VchType.RejectionIn + ") AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ")", CommonFunctions.ConStr) > 0)
                    {
                        btnBillCancel.Visible = false;
                        OMMessageBox.Show("Already this bill is returned.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                    }
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillDelete)) == true)
                {
                    Form frm = new Utilities.PasswordAsk(3);
                    ObjFunction.OpenForm(frm);
                    if (Utilities.PasswordAsk.IsDeleteAllow == 0) return;
                }

                if (OMMessageBox.Show("Are you sure you want to cancel this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    dbTVoucherEntry = new DBTVaucherEntry();
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = ID;
                    dbTVoucherEntry.CancelTVoucherEntry(tVoucherEntry);
                    if ((PID != 0) && ((DBGetVal.KachhaFirm == true)))//==========umesh=14-11-18- sir say auto posting entry  also delete
                    {
                        dbTVoucherEntry.CancelPostTVoucherEntry(PID);
                    }
                    ObjTrans.ExecuteQuery("Exec StockUpdateAll", CommonFunctions.ConStr);
                    OMMessageBox.Show("Bill No. " + txtInvNo.Text + " cancel successfully.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    rbEnglish.Enabled = true;
                    rbMarathi.Enabled = true;
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
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value.ToString() != "0" && Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AfterSaveNotDeleteItem)) == true)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                        return;
                    }

                    RewardDeleteFlag = false;
                    delete_row();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null &&
                        (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value.ToString() != "0" && Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AfterSaveNotDeleteItem))) == true)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                        dgBill.Focus();
                        return;
                    }

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
                            Desc_Start();
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.GrossWt)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.GrossWt].Value) - Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.TariffWt].Value);

                        dgBill.CurrentCell = dgBill[ColIndex.TariffWt, dgBill.CurrentCell.RowIndex];
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.TariffWt)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.GrossWt].Value) - Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.TariffWt].Value);
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        e.SuppressKeyPress = true;

                        if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "" && dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "0")
                        {
                            if (dgBill.CurrentCell.Value == null) dgBill.CurrentCell.Value = "1.000";
                            CalculateTotal();
                            if (dgBill.CurrentCell.Value != null) dgBill.CurrentCell.Value = Convert.ToDouble(dgBill.CurrentCell.Value.ToString()).ToString(Format.ThreeFloating);
                            Qty_MoveNext();
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
                            // 
                            Rate_MoveNext();
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

                    else
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];

                    }
                }
                else if (e.KeyCode == Keys.G && e.Control)
                {
                    pnlGrossWt.Visible = true;
                    dgGrossWt.Focus();
                    dgGrossWt.Rows.Clear();
                    txtTotalGrossWt.Text = "0.00";
                    txtGrossWt.Text = "0.00";
                    txtTariffWt.Text = "0.00";
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Remarks].Value != null)
                    {

                        string str = null;
                        string[] strArr = null;

                        str = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Remarks].Value.ToString();
                        char[] splitchar = { ',' };
                        strArr = str.Split(splitchar);
                        {
                            int a = strArr.Count();
                            for (int i = 0; i < a; i++)
                            {
                                dgGrossWt.Rows.Add();
                                dgGrossWt.Rows[i].Cells[1].Value = strArr[i];
                                dgGrossWt.Rows[i].Cells[0].Value = i + 1;
                            }
                        }
                        txtTotalGrossWt.Text = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value.ToString();
                        txtGrossWt.Text = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.GrossWt].Value.ToString();
                        txtTariffWt.Text = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TariffWt].Value.ToString();
                    }
                    else
                    {
                        dgGrossWt.Rows.Add();
                        if (dgGrossWt.CurrentCell.Value == null)
                        {
                            dgGrossWt.CurrentCell.Value = "1";
                            dgGrossWt.CurrentCell = dgGrossWt.Rows[0].Cells[1];
                        }
                    }
                }
                else if (e.KeyCode == Keys.F && e.Control)
                {
                    if ((dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value != null) && (Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IsRateChange].Value) != 2))
                    {
                        CalculateRate();
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IsRateChange].Value = 2;
                    }
                    else
                    {
                        dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 2].Cells[ColIndex.ItemName];
                        if (Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IsRateChange].Value) != 2)
                        {
                            CalculateRate();
                            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IsRateChange].Value = 2;
                        }
                    }
                    dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
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
                        txtDisplayName.Text = dgBill.CurrentRow.Cells[ColIndex.DisplayName].EditedFormattedValue.ToString().Trim();
                        pnlDisplayName.Location = new Point(100, 154);
                        pnlDisplayName.Visible = true;
                        txtDisplayName.Focus();
                    }
                }
                else if (e.KeyCode == Keys.S && e.Control)
                {
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value == null)
                    {
                        txtDiscount1Per.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != "")
                    {
                    }
                    txtBarCode.Focus();
                }
                else if (e.KeyCode == Keys.R && e.Control)
                {
                    if (dgBill.CurrentCell.Value != null)
                        dgBill.CurrentCell = dgBill[ColIndex.Remarks, dgBill.CurrentCell.RowIndex];
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
                    Master.ItemAdvanceSearch itemSearch = new Master.ItemAdvanceSearch();
                    ObjFunction.OpenForm(itemSearch);
                    if (itemSearch.ItemNo != 0)
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value = itemSearch.BarCode;
                        dgBill_CellEndEdit(dgBill, new DataGridViewCellEventArgs(ColIndex.ItemName, dgBill.Rows.Count - 1));
                        itemSearch.Close();
                    }
                    txtRateType.Focus();
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
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() == "")
                    {
                        if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            long PKStockTrnNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkStockTrnNo].Value);
                            if (PKStockTrnNo != 0)
                            {
                                DeleteDtls(1, PKStockTrnNo);
                                DeleteDtls(11, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value));
                                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() != "" && dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FKItemLevelDiscNo].FormattedValue.ToString() != "0")
                                {
                                    DeleteDtls(9, Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.FKItemLevelDiscNo].Value));
                                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DiscRupees].Value = "0.00";
                                }
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

                    }
                    else
                        OMMessageBox.Show("This item already asssigned to scheme. Not allowed to delete this item.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
                        //tVoucherDetails.CompanyNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        //tVoucherDetails.FkVoucherNo = ID;
                        //dbTVoucherEntry.DeleteTVoucherDetailsCompany(tVoucherDetails);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 4)
                    {
                        tStockGodown.PKStockGodownNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTStockGodown(tStockGodown);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 5)
                    {
                        tReward.RewardNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTReward(tReward);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 6)
                    {
                        tRewardDetails.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTRewardDetails(tRewardDetails);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 7)
                    {
                        tRewardFrom.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTRewardFrom(tRewardFrom);
                    }
                    else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 8)
                    {
                        tRewardTo.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTVoucherEntry.DeleteTRewardTo(tRewardTo);
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

        public OMMessageBoxDefaultButton GetBillPrintAsk()
        {
            if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrintAsk)) == 0)
                return OMMessageBoxDefaultButton.Button1;
            else if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrintAsk)) == 1)
                return OMMessageBoxDefaultButton.Button2;
            else
                return OMMessageBoxDefaultButton.Button3;
        }

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
                        ObjFunction.FillCombo(cmbPartyNameSearch, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ")  and MLedger.Ledgerno in (select Ledgerno from TVoucherEntry where vouchertypecode=" + VoucherType + ")order by LedgerName");
                    else
                        ObjFunction.FillComb(cmbPartyNameSearch, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryDebtors + "))and MLedger.Ledgerno in (select Ledgerno from TVoucherEntry where vouchertypecode=" + VoucherType + ") ORDER BY LedgerName ");

                    txtSearch.Focus();
                    btnNew.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnBillCancel.Enabled = false;
                    rbInvNo.Checked = true;
                    rbType_CheckedChanged(rbInvNo, null);
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
                rbInvNo.Enabled = true;
                rbPartyName.Enabled = true;
                rbDate.Enabled = true;
                dtpSearchDate.Visible = false;
                if (rbInvNo.Checked == true)
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

        public void PrintBill(int PrintType)
        {
            try
            {
                string AddressPrint = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true)
                {
                    if (OrderType == 1)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillCouterBill)) == true)
                            AddressPrint = "0";
                        else
                            AddressPrint = "1";
                    }
                    if (OrderType == 2)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillHomeDelivery)) == true)
                            AddressPrint = "0";
                        else
                            AddressPrint = "1";
                    }
                }
                double Amt = 0;
                Amt += Convert.ToDouble(txtTotalAnotherDisc.Text);
                string[] ReportSession;

                ReportSession = new string[22];
                ReportSession[0] = ID.ToString();
                ReportSession[1] = "";
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "No Payment";
                ReportSession[7] = (txtTotalAnotherDisc.Text == "") ? "0" : txtTotalAnotherDisc.Text;
                ReportSession[8] = (txtTotalChrgs.Text == "") ? "0" : txtTotalChrgs.Text;
                ReportSession[9] = (rbEnglish.Checked == true) ? "1" : "2";
                ReportSession[10] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSavingBill)).ToString();
                ReportSession[11] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)).ToString();

                ReportSession[12] = (MixModeVal == 1) ? "1" : "0";

                ReportSession[13] = AddressPrint;
                ReportSession[14] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff)) == true) ? "1" : "2";
                ReportSession[15] = (ObjQry.ReturnLong("Select Count(*) FRom TReward Where FkVoucherNo=" + ID + "", CommonFunctions.ConStr) == 0) ? "0" : "1";
                ReportSession[16] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSchemeDetails)) == true) ? "1" : "0";
                ReportSession[17] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? "1" : "2";
                ReportSession[18] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? txtRemark.Text : "";
                ReportSession[19] = (ShowVATNo == true) ? "1" : "2";
                string AmountIn = "";
                string str = Convert.ToString(NumberToWordsIndian.getWords(txtGrandTotal.Text));
                AmountIn = str.Substring(0, str.Length - 14);
                ReportSession[20] = AmountIn;

                #region New Code for Outstanding
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                {

                    ReportSession[21] = lblOutstanding.Text.ToString();
                }
                else
                    ReportSession[21] = "0";

                #endregion

                if (PrintType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    childForm = null;
                    if (rbEnglish.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBillMRP");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBillMRP");
                                }

                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBill");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBill");
                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("DCGetBillMRP.rpt", CommonFunctions.ReportPath);
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("DCGetBill-A4.rpt", CommonFunctions.ReportPath); // for print 
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBill-a4.rpt", CommonFunctions.ReportPath);
                                }

                            }
                        }
                    }
                    else if (rbMarathi.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBillMarMRP");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBillMarMRP");
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.GetBillMar");
                                }
                                else
                                {
                                    childForm = ObjFunction.GetReportObject("Reports.DCGetBillMar");
                                }
                            }

                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("DCGetBillMarMRP.rpt", CommonFunctions.ReportPath);
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath);
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath);
                                }
                            }
                        }
                    }
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        objRpt.PrintCount = IsPrintCount;
                        if (objRpt.PrintReport() == true)
                        {
                            IsPrintCount = 1;
                            DisplayMessage("Bill Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("Bill not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
                else if (PrintType == 2)
                {
                    try
                    {
                        EmailRptName = txtInvNo.Text.ToString();
                        CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                        childForm = null;
                        childForm = ObjFunction.LoadReportObject("GetBill-a4.rpt", CommonFunctions.ReportPath);
                        if (childForm != null)
                        {
                            DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession, true);
                            if (objRpt.PrintReport() == true)
                            {
                                Path.Add(objRpt.newFullPath);
                                SendMail.SendEmail(Convert.ToString(txtToEmailid.Text), null, GetEmailSubAndBody().EmailSubject, GetEmailSubAndBody().EmailBody, Path);
                                DisplayMessage("Email Send Successfully.....!!!");
                            }
                            else
                            {
                                DisplayMessage("Email Not Send....... !!!");
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    Form NewF = null;
                    if (rbEnglish.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)

                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBill.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBill.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }

                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBill-a4.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBill-a4.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }


                        }
                    }
                    else if (rbMarathi.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)

                            {
                                if (DBGetVal.KachhaFirm == true)
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                                else
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }

                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)

                            {
                                if (DBGetVal.KachhaFirm == true)
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                                else
                                { NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarMRP.rpt", CommonFunctions.ReportPath), ReportSession); }
                            }
                            else
                            {
                                if (DBGetVal.KachhaFirm == true)
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
                                }

                            }

                        }
                    }
                    if (NewF != null)
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                DialogResult ds = OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                if (ds == DialogResult.Yes)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                    {
                        Form NewFF = new Utilities.PrintCount(IsPrintCount);
                        ObjFunction.OpenForm(NewFF);
                        if (Utilities.PrintCount.NoPrintCount != -1)
                        {
                            IsPrintCount = Utilities.PrintCount.NoPrintCount;
                            PrintBill(0);
                        }
                    }
                    else PrintBill(0);
                }
                else if (ds == DialogResult.Cancel)
                    PrintBill(1);

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
                    if (ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VoucherUserNo=" + txtSearch.Text + " and VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr) > 1)
                    {
                        pnlInvSearch.Visible = true;
                        // dgInvSearch.Visible = true;
                        int x = dgBill.GetCellDisplayRectangle(0, 0, true).X + 200;//(Screen.PrimaryScreen.WorkingArea.Width) / 2;
                        int y = dgBill.GetCellDisplayRectangle(0, 0, true).Y + 100;
                        pnlInvSearch.Location = new Point(x, y);
                        string str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #],convert(varchar(11),TVoucherEntry.VoucherDate,105) as 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                     "TVoucherEntry.PkVoucherNo FROM TVoucherEntry WHERE (TVoucherEntry.VoucherTypeCode IN (" + VchType.Sales + "," + VchType.SalesOrder + ")) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
                                     "And TVoucherEntry.VoucherUserNo=" + txtSearch.Text + " " +
                                     "Order By  TVoucherEntry.VoucherUserNo desc,TVoucherEntry.VoucherDate desc, TVoucherEntry.Reference desc";
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
                    tempNo = ObjQry.ReturnLong("Select PKVoucherNo From TVoucherEntry Where VoucherUserNo=" + txtSearch.Text + " and VoucherTypeCode=" + VoucherType + "", CommonFunctions.ConStr);
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
                        rbInvNo.Focus();
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
                        pnlPartySearch.Location = new Point(x, y);
                        string str = "";
                        if (DBGetVal.KachhaFirm == false)
                        {
                            str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #], convert(varchar(11),TVoucherEntry.VoucherDate,105) as 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                        "TVoucherEntry.PkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo WHERE (TVoucherEntry.VoucherTypeCode IN (" + VchType.Sales + "," + VchType.SalesOrder + ")) AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
                                        "And TVoucherDetails.LedgerNo=" + ObjFunction.GetComboValue(cmbPartyNameSearch) + " " +
                                        "Order By  TVoucherEntry.VoucherUserNo desc,TVoucherEntry.VoucherDate desc, TVoucherEntry.Reference desc";
                        }
                        else
                        {
                            str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #], convert(varchar(11),TVoucherEntry.VoucherDate,105) as 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                 "TVoucherEntry.PkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo WHERE (TVoucherEntry.VoucherTypeCode IN (" + VchType.DSales + "," + VchType.DSalesOrder + ")) AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
                                 "And TVoucherDetails.LedgerNo=" + ObjFunction.GetComboValue(cmbPartyNameSearch) + " " +
                                 "Order By  TVoucherEntry.VoucherUserNo desc,TVoucherEntry.VoucherDate desc, TVoucherEntry.Reference desc";
                        }
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
                    rbInvNo.Checked = true;
                    rbType_CheckedChanged(rbInvNo, null);
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
                    tempNo = ObjQry.ReturnLong("Select PKVoucherNo From TVoucherEntry Where PkVoucherNo=" + Convert.ToInt64(dgPartySearch.Rows[dgPartySearch.CurrentRow.Index].Cells[4].Value) + " and VoucherTypeCode=" + VoucherType + " AND CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
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
                        DisplayMessage("Bill Not Found");
                        txtSearch.Focus();
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
            pnlSearch.Visible = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnBillCancel.Enabled = true;
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

                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.IsRateChange].Value != null)
                    {
                        if (dgBill.Rows[e.RowIndex].Cells[ColIndex.IsRateChange].Value.ToString() == "1")
                        {
                            e.Value = e.Value.ToString() + " *";
                        }
                    }
                }
                if (e.ColumnIndex == ColIndex.ItemName)
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value.ToString() != "")
                    {
                        if (dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemNo].Value.ToString() != "0")
                        {
                            dgBill.Rows[e.RowIndex].ReadOnly = false;
                            dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName].ReadOnly = true;
                        }
                    }
                }
                if (e.ColumnIndex == ColIndex.ItemName)
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.SchemeDetailsNo].Value != null)
                    {
                        if (dgBill.Rows[e.RowIndex].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() != "")
                        {
                            dgBill.Rows[e.RowIndex].ReadOnly = true;
                        }
                    }
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.SONo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.SONo].Value.ToString() != "")
                    {
                        if (dgBill.Rows[e.RowIndex].Cells[ColIndex.SONo].Value.ToString() != "0")
                        {
                            dgBill.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AliceBlue;
                            dgBill.Rows[e.RowIndex].Cells[ColIndex.Quantity].ReadOnly = true;
                        }
                    }
                }
                if (e.ColumnIndex == ColIndex.DiscRupees)
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.Amount].Value == null)
                        dgBill.Rows[e.RowIndex].Cells[ColIndex.DiscRupees].ReadOnly = true;
                    else
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsRateChangeByUser)) == true)
                        {
                            if (UserType != 1)
                            {
                                dgBill.Rows[e.RowIndex].Cells[ColIndex.DiscRupees].ReadOnly = true;
                            }
                        }
                        else
                        {
                            dgBill.Rows[e.RowIndex].Cells[ColIndex.DiscRupees].ReadOnly = false;
                        }
                    }
                }
                if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkStockTrnNo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.PkStockTrnNo].Value.ToString() != "0")
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_AfterSaveNotDeleteItem)) == true)
                        dgBill.Rows[e.RowIndex].ReadOnly = true;
                }
                int a = Convert.ToInt32(dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
                    txtArea.Text = lstArea.Text;
                    pnlParty.Visible = true;
                    lstPartyEnglish.Focus();
                    pnlArea.Visible = false;

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            try
            {
                if (dg.CurrentCell.Value != null)
                    dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
                else
                    dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void m2n1(int RowIndex, int ColIndex, DataGridView dg)
        {
            try
            {
                dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.Quantity)
                DBCellValue.Quanity = dgBill.Rows[e.RowIndex].Cells[ColIndex.Quantity].FormattedValue.ToString();
            if (e.ColumnIndex == ColIndex.Rate)
                DBCellValue.Rate = dgBill.Rows[e.RowIndex].Cells[ColIndex.Rate].FormattedValue.ToString();
            if (e.ColumnIndex == ColIndex.DiscRupees)
                DBCellValue.Disc = dgBill.Rows[e.RowIndex].Cells[ColIndex.DiscRupees].FormattedValue.ToString();
        }

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (Spaceflag == false) { Spaceflag = true; return; }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                {
                    if (dgBill.CurrentCell.Value != null && Convert.ToString(dgBill.CurrentCell.Value) != "")
                    {
                        Desc_Start();
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.GrossWt)
                {
                    dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.GrossWt].Value) - Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.TariffWt].Value);
                    MovetoNext move2n = new MovetoNext(m2n1);
                    BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.TariffWt, dgBill });
                    dgBill.CurrentCell = dgBill[ColIndex.TariffWt, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.TariffWt)
                {
                    dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.GrossWt].Value) - Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.TariffWt].Value);
                    MovetoNext move2n = new MovetoNext(m2n1);
                    BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.Quantity, dgBill });
                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == true)
                        {
                            if (Convert.ToDouble(dgBill.CurrentCell.Value.ToString()) == 0) dgBill.CurrentCell.Value = "1";
                        }
                        else
                            dgBill.CurrentCell.Value = "1";
                    }
                    Qty_MoveNext();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.UOM)
                {
                    UOM_Start();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                {

                    if (dgBill.CurrentRow.Cells[ColIndex.Rate].Value == null)
                    { dgBill.CurrentRow.Cells[ColIndex.Rate].Value = 0.00; }
                    dgBill.CurrentRow.Cells[ColIndex.IsRateChange].Value = "1";
                    //}
                    Rate_MoveNext();

                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.PackagingCharges)
                {
                    if (dgBill.CurrentRow.Cells[ColIndex.PackagingCharges].Value == null)
                    { dgBill.CurrentRow.Cells[ColIndex.PackagingCharges].Value = 0.00; }
                    CalculateTotal();
                    if (dgBill.Columns[ColIndex.DiscPercentage].Visible == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDisc)) == true)
                        {
                            MovetoNext move2n = new MovetoNext(m2n1);
                            BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.DiscPercentage, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, dgBill.CurrentCell.RowIndex];
                            dgBill.Focus();
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                            dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                            dgBill.Focus();
                        }
                    }
                    else
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                        dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                        dgBill.Focus();
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    MovetoNext move2n = new MovetoNext(m2n1);
                    if (Convert.ToDouble(dgBill.CurrentCell.FormattedValue) > 100)
                    {
                        OMMessageBox.Show("Not allowed Greater than 100%.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscPercentage, dgBill });
                        dgBill.CurrentCell.Value = "100";
                    }

                    Disc_MoveNext();

                    int colIndex = -1;
                    for (int i = e.ColumnIndex + 1; i < dgBill.Columns.Count; i++)
                    {

                        if (dgBill.Columns[i].Visible == true)
                        {
                            if (ColIndex.Amount != i && ColIndex.SGSTAmount != i && ColIndex.SGSTPercentage != i && ColIndex.MRP != i)
                            {
                                colIndex = i;
                                break;
                            }
                        }

                    }
                    if (colIndex != -1)
                        BeginInvoke(move2n, new object[] { e.RowIndex, colIndex, dgBill });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBill });

                    if (dgBill.Columns[ColIndex.DiscRupees].Visible == true)
                    {
                        move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DiscRupees, dgBill });
                    }
                    else
                    {
                        move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBill });
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscRupees)
                {
                    Disc_MoveNext();

                    MovetoNext move2n = new MovetoNext(m2n);
                    int colIndex = -1;
                    for (int i = e.ColumnIndex + 1; i < dgBill.Columns.Count; i++)
                    {
                        if (dgBill.Columns[i].Visible == true)
                        {
                            colIndex = i;
                            break;
                        }
                    }
                    if (colIndex != -1)
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, dgBill });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, colIndex, dgBill });

                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage2)
                {
                    Disc_MoveNext();
                    MovetoNext move2n = new MovetoNext(m2n);
                    int colIndex = -1;
                    for (int i = e.ColumnIndex + 1; i < dgBill.Columns.Count; i++)
                    {
                        if (dgBill.Columns[i].Visible == true)
                        {
                            colIndex = i;
                            break;
                        }
                    }
                    if (colIndex != -1)
                        BeginInvoke(move2n, new object[] { e.RowIndex, colIndex, dgBill });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, colIndex, dgBill });

                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscRupees2)
                {
                    Disc_MoveNext();
                    MovetoNext move2n = new MovetoNext(m2n);
                    int colIndex = -1;
                    for (int i = e.ColumnIndex + 1; i < dgBill.Columns.Count; i++)
                    {
                        if (dgBill.Columns[i].Visible == true)
                        {
                            colIndex = i;
                            break;
                        }
                    }
                    if (colIndex != -1)
                        BeginInvoke(move2n, new object[] { e.RowIndex, colIndex, dgBill });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, colIndex, dgBill });
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void Rate_MoveNext()
        {
            try
            {
                if (dgBill.CurrentCell.Value != null)
                {
                    if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsRateVeriation)))
                        {
                            double HValue = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.HigherVariation].Value);
                            double LValue = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.LowerVariation].Value);
                            if (HValue > 0 && HValue < Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value))
                            {
                                OMMessageBox.Show("Rate is too high, please check ...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            }
                            else if (LValue > 0 && LValue > Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value))
                            {
                                OMMessageBox.Show("Rate is too low, please check ...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            }

                        }

                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value).ToString("0.00");
                        if ((Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkItemTaxInfo].Value) == 0) || (Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0))
                            BindGrid(dgBill.CurrentCell.RowIndex);
                        else
                        {
                            CalculateTotal();
                        }
                        if (dgBill.Columns[ColIndex.PackagingCharges].Visible == true)
                        {

                            MovetoNext move2n = new MovetoNext(m2n1);
                            BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.PackagingCharges, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.PackagingCharges, dgBill.CurrentCell.RowIndex];
                            dgBill.Focus();

                        }
                        else if (dgBill.Columns[ColIndex.DiscPercentage].Visible == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDisc)) == true)
                            {
                                MovetoNext move2n = new MovetoNext(m2n1);
                                BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.DiscPercentage, dgBill });
                                dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, dgBill.CurrentCell.RowIndex];
                                dgBill.Focus();
                            }
                            else
                            {
                                MovetoNext move2n = new MovetoNext(m2n);
                                BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                                dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.Rows.Count - 1];
                                dgBill.Focus();
                            }
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                            dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                            dgBill.Focus();
                        }

                        if (ItemType == 1 && Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnGrid)) == false)
                        {
                            Desc_Start();
                        }
                    }
                    else
                    {
                        dgBill.CurrentCell.ErrorText = "Please Enter valid rate...";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell != null)
                {
                    for (int i = 0; i < dgBill.Rows.Count; i++)
                    {
                        dgBill.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clrColorRow;
                    dgBill.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            try
            {
                double TotalAmt = 0;
                TotalAmt = ((Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtTotalTax.Text)) - Convert.ToDouble(txtTotalDisc.Text));
                if (((TextBox)sender).Name == "txtDiscount1Per")
                {
                    txtOtherDisc.Text = Format.DoubleFloating;
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscRupees1")
                {
                    txtOtherDisc.Text = Format.DoubleFloating;
                    txtSchemeDisc.Text = Format.DoubleFloating;
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtSchemeDisc")
                {
                    CalculateTotal();
                }
                else if (((TextBox)sender).Name == "txtChrgRupees1")
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Charges Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                    
                            txtRemark.Focus();
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtInvNo")
                {
                    ObjFunction.GetFinancialYear(dtpBillDate.Value, out dtFrom, out dtTo);
                    if (ObjFunction.CheckNumeric(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Valid No.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else if (ObjQry.ReturnLong("Select VoucherUserNo from TVoucherEntry where VoucherTypeCode=" + VchType.Sales + " and VoucherUserNo=" + Convert.ToInt64(((TextBox)sender).Text) + " AND VoucherDate>='" + dtFrom.Date + "'  And ( PkVoucherNo <> " + ID + " ) AND VoucherDate<='" + dtTo.Date + "' ", CommonFunctions.ConStr) != 0)
                    {
                        OMMessageBox.Show("No Already Exist.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        if (Convert.ToBoolean(AppSettings.S_IsManualBillNo) == true)
                            VoucherUserNo = Convert.ToInt64(txtInvNo.Text.Trim());
                        dtpBillDate.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtDiscount1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtDiscount1Per.Text == "") txtDiscount1Per.Text = "0.00";
                Control_Leave((object)txtDiscount1Per, new EventArgs());
                txtSchemeDisc.Enabled = true;
                txtSchemeDisc.Focus();
            }
        }

        private void txtSchemeDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSchemeDisc.Text == "") txtSchemeDisc.Text = "0.00";
                Control_Leave((object)txtSchemeDisc, new EventArgs());
            }
        }

        private void txtOtherDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtChrgRupees2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void btnShortcut_Click(object sender, EventArgs e)
        {
            if (pnlFooterInfo.Visible == false)
            {
                pnlFooterInfo.Dock = DockStyle.Bottom;
                pnlFooterInfo.BorderStyle = BorderStyle.None;
                pnlFooterInfo.BringToFront();
                pnlFooterInfo.Visible = true;
            }
            else
            {
                pnlFooterInfo.Visible = false;
            }
        }

        private void txtBigPrint_Click(object sender, EventArgs e)
        {
        }

        private void dgGrossWt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((dgGrossWt.CurrentCell.Value != null) && (Convert.ToDouble(dgGrossWt.CurrentCell.Value) != 0))
                {
                    int count = dgGrossWt.Rows.Count;
                    if (Convert.ToDouble(dgGrossWt.Rows[count - 1].Cells[1].Value) != 0)
                        dgGrossWt.Rows.Add();
                    dgGrossWt.CurrentCell = dgGrossWt[dgGrossWt.CurrentCell.ColumnIndex, dgGrossWt.CurrentCell.RowIndex + 1];
                    dgGrossWt.CurrentCell.Value = 0;
                }
                else if ((dgGrossWt.CurrentCell.Value == null))
                {
                    dgGrossWt.CurrentCell = dgGrossWt.CurrentCell;
                    dgGrossWt.Focus();
                }
                else
                {
                    dgGrossWt.CurrentCell = dgGrossWt[dgGrossWt.CurrentCell.ColumnIndex, dgGrossWt.CurrentCell.RowIndex];
                    dgGrossWt.Focus();
                }
                double qty = 0.00;
                if (dgGrossWt.CurrentCell.Value != null)
                    for (int i = 0; i < dgGrossWt.Rows.Count; i++)
                    {
                        qty += Convert.ToDouble(dgGrossWt.Rows[i].Cells[1].Value);
                    }
                txtGrossWt.Text = Convert.ToDouble(qty).ToString("0.00");
                qty = Convert.ToDouble(txtGrossWt.Text) - Convert.ToDouble(txtTariffWt.Text);
                txtTotalGrossWt.Text = qty.ToString();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pnlGrossWt.Visible = false;

                dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                dgBill.Focus();
            }
            else if (e.KeyCode == Keys.T && e.Control)
            {
                txtTariffWt.Focus();
            }
        }

        private void txtTariffWt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double qty = Convert.ToDouble(txtGrossWt.Text) - Convert.ToDouble(txtTariffWt.Text);
                txtTotalGrossWt.Text = qty.ToString();
                btnGrossWt.Focus();
            }
        }

        private void btnGrossWt_Click(object sender, EventArgs e)
        {
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = txtTotalGrossWt.Text;
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.GrossWt].Value = txtGrossWt.Text;
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TariffWt].Value = txtTariffWt.Text;
            string str = "";
            for (int i = 0; i < dgGrossWt.Rows.Count; i++)
            {
                if ((i == 0) && (dgGrossWt.Rows[i].Cells[1].Value != null))
                {
                    str = dgGrossWt.Rows[i].Cells[1].Value.ToString();
                }
                else if (dgGrossWt.Rows[i].Cells[1].Value.ToString() != "")
                {
                    str = str + "," + dgGrossWt.Rows[i].Cells[1].Value;
                }
            }
            dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Remarks].Value = str;
            pnlGrossWt.Visible = false;
            CalculateTotal();
            dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
            dgBill.Focus();
        }

        private void pnlGrossWt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            pnlGrossWt.Visible = false;
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
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.GrossWt)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);

                }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.TariffWt)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);

                }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.Rate)
                {
                    TextBox txtrate = (TextBox)e.Control;
                    txtrate.TextChanged += new EventHandler(txtRate_TextChanged);
                }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.PackagingCharges)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(PackagingCharges_TextChanged);

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

        public void PackagingCharges_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.PackagingCharges)
                {
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) && Convert.ToDouble(((TextBox)sender).Text) > 999999.99)
                    {
                        OMMessageBox.Show("Please enter valid value on PackagingCharges field." + Environment.NewLine + "Press Escape to continue..", CommonFunctions.ErrorTitle, OMMessageBoxButton.Escape, OMMessageBoxIcon.Error);
                        //((TextBox)sender).Text ="" ? "0" : DBCellValue.Rate;
                    }
                    ObjFunction.SetMasked((TextBox)sender, 2, 7, OMFunctions.MaskedType.NotNegative);
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

        private void txtDisplayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DisplayName].Value = txtDisplayName.Text;

                    pnlDisplayName.Visible = false;
                    txtDisplayName.Text = "";
                    dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtInvNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsManualBillNo)) == true)
                {
                    if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "") == System.Net.Dns.GetHostName())
                    {

                        if (((ObjQry.ReturnLong("select count(*) from tvoucherentry where vouchertypecode=15 and voucheruserno=" + txtInvNo.Text, CommonFunctions.ConStr)) == 0)) // && (TempBillNo >Convert.ToInt32(txtInvNo.Text)))
                        {
                            dtpBillDate.Focus();
                            ManualBill = true;
                        }
                        else
                        {
                            txtInvNo.Focus();
                            MessageBox.Show("Bill Already Exist");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Client Pc Not Allowed Manual Bill ");
                    }
                }
            }
        }

        private void txtChrgRupees1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtChrgRupees1.Text == "")
                    {
                        txtChrgRupees1.Focus();

                    }
                    else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges2Display)) == true)
                    {
                        txtChrgRupees2.Focus();
                    }

                    else
                    {
                        txtRemark.Focus();
                    }
                    CalculateTotal();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnTemp_Click(object sender, EventArgs e)
        {
            DataTable tempDt = new DataTable();
            tempDt = ObjFunction.GetDataView("Select Pkvoucherno from Tvoucherentry where vouchertypecode=15 ").Table;
            for (int i = 0; i <= tempDt.Rows.Count - 1; i++)
            {
                ID = Convert.ToInt32(tempDt.Rows[i].ItemArray[0]);
                FillField();
                btnUpdate_Click(sender, e);

            }
        }

        private void btnCashSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Visible)
            {
                PrintAsk = 0;
                btnSave_Click(sender, e);
            }
        }

        private void btnSendSms_Click(object sender, EventArgs e)
        {
            txtSendNumber.Text = "";
            txtSendNumber.Enabled = true;
            pnlPhone.Visible = true;
            pnlEmail.Visible = false;
            txtSendNumber.Focus();
        }

        private void btnSendSmslink_Click(object sender, EventArgs e)
        {
            DBClasses.SendSmsLink objSmsCode = new DBClasses.SendSmsLink("AF8Y1", txtPartyName.Text, txtSendNumber.Text, txtGrandTotal.Text, "123");
            PaymentGatway GatwayType = new PaymentGatway();
            GatwayType.Sendsms("https://mobilepayments.benow.in/payments/paymentadapter/portablePaymentRequest", objSmsCode);
            OMMessageBox.Show("SMS Send Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            pnlPhone.Visible = false;
        }

        private void btnSendsmsclose_Click(object sender, EventArgs e)
        {
            pnlPhone.Visible = false;
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            txtToEmailid.Text = "";
            txtToEmailid.Enabled = true;
            pnlEmail.Visible = true;
            pnlPhone.Visible = false;
            txtToEmailid.Focus();
        }

        private EmailTemplateBO GetEmailSubAndBody()
        {
            EmailTemplateBO oEmailTemplateBO;
            try
            {
                oEmailTemplateBO = new EmailTemplateBO();
                oEmailTemplateBO.EmailSubject = "Sales Bill";

                oEmailTemplateBO.EmailBody =

                  "  <div style=\"width:100%;border:solid 1px;border-color:lightgray\"></div><br/>" +

                    "<span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12;\">Dear Sir/Madam, <br/><br/>I have send demo email. <br/>"

                    + "<br/><b> Thanks & Regards,</b><br/>Shrriyans Shah<br/>VIRHA ITech,NanaPeth<br/>Pune</span>";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oEmailTemplateBO;
        }

        private void btnEmailSend_Click(object sender, EventArgs e)
        {
            PrintBill(2);
            pnlEmail.Visible = false;
        }

        private void btnEmailCancel_Click(object sender, EventArgs e)
        {
            pnlEmail.Visible = false;
        }

        private void txtToEmailid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnEmailSend.Focus();
            }
        }

        private void txtSendNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSendSmslink.Focus();
            }
        }
    }
}

