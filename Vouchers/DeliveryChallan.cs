using OM;
using OMControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Yadi.DBClasses;

namespace Yadi.Vouchers
{
    public partial class DeliveryChallan : Form
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
        double PBillTotal = 0, PartyDiscPerceItemLevel = 0.0;
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

        bool EnrFlag = false, QutFlag = false, CancelFlag = false, ManualBill = false;
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
        List<string> File = new List<string>();
        DataTable Subdt = new DataTable();
        DataTable dtReportName = new DataTable();
        double SubQty = 0, SubRate = 0, SubDiscPer = 0, SubDiscRs = 0, SubTax1 = 0, SubTax2 = 0, SubTax3 = 0, SubMktQty = 0;
        public static string EmailRptName = "";
        public string PrintEngRpt = "";
        public string PrintMarrpt = "";

        public string RptPrint = "", RptPrintMar = "", RptPrinter = "", RptPrinterMar = "";
        public DeliveryChallan()
        {
            InitializeComponent();
        }
        static DeliveryChallan()
        {
            // InitializeComponent();
        }
        public DeliveryChallan(long ID)
        {
            InitializeComponent();
            this.ID = ID;
        }

        private void DeliveryChallan_Load(object sender, EventArgs e)
        {
            try
            {
                //addmodeDC();

                if (DBGetVal.KachhaFirm == false)
                {
                    VoucherType = VchType.DeliveryChallan;
                }
                else
                {
                    VoucherType = VchType.DSales;
 
                }
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                pnlBank.Visible = false;
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

                btnBigPrint.Visible = false;
                btnShortcut.Visible = false;

                //btnShortcut.Visible = true;
                //btnBigPrint.Visible = true;
                KeyDownFormat(this.Controls);
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsEwayBill)) == true)
                {
                    // btnEwayBill.Visible = false ;
                    btnEwayBill.Visible = true;
                }
                else
                {
                    btnEwayBill.Visible = false;
                }
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
                //   dtSearch = ObjFunction.GetDataView("Select max(PkVoucherNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType +
                //       " AND IsVoucherLock='false' ").Table;//Order by VoucherDate, VoucherUserNo desc
                btnDeliveryAddress.Visible = false;
                ID = ObjQry.ReturnLong("Select max(PkVoucherNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType + " ", CommonFunctions.ConStr);
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
                    btnMixMode.Enabled = false;
                    btnAdvanceSearch.Visible = false;
                    btnBillCancel.Enabled = false;
                    btnPrint.Enabled = false;
                    btnCCSave.Enabled = false;
                    btnCashSave.Enabled = false; btnCreditSave.Enabled = false;
                    // btnShowDetails.Location = new Point(btnShowDetails.Location.X, btnShowDetails.Location.Y + btnShowDetails.Height + 7);
                    dgBill.Enabled = true;

                }
                DataTable dtSettings = ObjFunction.GetDataView("Select PKSettingNo From MSettings Where SettingTypeNo=4").Table;
                for (int i = 0; i < dtSettings.Rows.Count; i++)
                {
                    //try
                    //{
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(Convert.ToInt32(dtSettings.Rows[i].ItemArray[0].ToString()))) == true)
                        dgBill.Columns[i].Visible = true;
                    else
                        dgBill.Columns[i].Visible = false;
                    //}
                    //catch (Exception exec)
                    //{
                    //    ObjFunction.ExceptionDisplay(exec.Message);

                    //}
                }
                txtGrandTotal.Font = new Font("Verdana", 18, FontStyle.Bold);
                txtGrandTotal.ForeColor = Color.Black;

                lblGrandTotal.Font = new Font("Verdana", 22, FontStyle.Bold);
                lblGrandTotal.ForeColor = Color.White;
                lblitemcount.Font = new Font("Verdana", 15, FontStyle.Bold);

                label65.Font = new Font("Verdana", 15, FontStyle.Bold);
                lblCreditLimit.Font = new Font("Arial", 9, FontStyle.Bold);
                lblOutstanding.Font = new Font("Arial", 12, FontStyle.Bold);
                lblLastBillAmt.Font = new Font("Arial", 9, FontStyle.Bold);

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

                dtpBillDate.CustomFormat = "dd-MMM-yyyy"; dtpBillDate.Width = 110;
                dgBill.BackgroundColor = Color.FromArgb(242, 242, 242);// Color.FromArgb(255, 255, 210);
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == false)
                {
                    pnlPrinting.Visible = false;
                    rbEnglish.Checked = true;
                    dgItemList.Columns[2].Visible = false;
                }
                else
                {
                    if (DBGetVal.KachhaFirm == false)

                    {
                        if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.OS_DefaultBillPrint)) == 1)
                            rbEnglish.Checked = true;
                        else
                            rbMarathi.Checked = true;
                    }

                    else
                    {
                        if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_DefaultBillPrint)) == 1)
                            rbEnglish.Checked = true;
                        else
                            rbMarathi.Checked = true;
                    }

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
                PrintAsk = Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsBillPrintAsk));
                // btnTemp.Visible = true;
                //dtPAKKABILL = new DataTable();
                //foreach (DataGridViewColumn col in dgBill.Columns)
                //{
                //    dtPAKKABILL.Columns.Add(col.Name);
                //}
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsMobileShop)) == true)
                {
                    btnGenerateIMEI.Visible = true;
                    btnRackWisePrint.Visible = false;
                    btnDcPrint.Visible = false;
                    btnKgPrint.Visible = false;
                    btnBox.Visible = false;
                    btnEwayBill.Visible = false;
                }
                else
                {
                    btnGenerateIMEI.Visible = false;
                    btnRackWisePrint.Visible = true;
                    btnDcPrint.Visible = true;
                    btnKgPrint.Visible = true;
                    btnBox.Visible = true;
                    btnEwayBill.Visible = true;
                }

                btnRackWisePrint.Visible = false;
                btnDcPrint.Visible = false;
                btnKgPrint.Visible = false;
                btnBox.Visible = false;
                btnSendSms.Visible = false;
                btnEmail.Visible = false;
                
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
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display)) == false)
                {
                    lblDisc1.Visible = false;// Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display));
                    txtDiscount1Per.Visible = false;// Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display));
                    txtDiscRupees1.Visible = false;// Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Discount1Display));
                    label74.Visible = false;
                }
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

        public void FilldgDeliveryAddress()
        {
            try
            {
                string sql = "";
                sql = "SELECT     MLedgerDetails.LedgerDetailsNo, MLedgerDetails.LedgerNo, MLedgerDetails.Address, MArea.AreaNo, MArea.AreaName, MCity.CityNo, MCity.CityName,MLedgerDetails.PinCode, MLedgerDetails.PhNo1, MLedgerDetails.MobileNo1  , '' as selected               " +
                 " FROM MLedgerDetails left JOIN    MArea ON MLedgerDetails.AreaNo = MArea.AreaNo left JOIN " +
                 " MCity ON MLedgerDetails.CityNo = MCity.CityNo WHERE(LedgerNo = " + PartyNo + ") Union all " +
                   "SELECT     MLedgerDetails.LedgerDetailsNo, MLedgerDetails.LedgerNo, MLedgerDetails.Address, MArea.AreaNo, MArea.AreaName, MCity.CityNo, MCity.CityName,MLedgerDetails.PinCode, MLedgerDetails.PhNo1, MLedgerDetails.MobileNo1  , '' as selected               " +
                   " FROM MLedgerDetails left JOIN    MArea ON MLedgerDetails.AreaNo = MArea.AreaNo left JOIN " +
                   " MCity ON MLedgerDetails.CityNo = MCity.CityNo WHERE(DeliveryLedgerNo = " + PartyNo + ")";
                DataView dv = new DataView();
                // Item itm = (Item)CmbSearch.SelectedItem;
                DataSet ds = new DataSet();

                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
                // CommonFunctions.ErrorMessge = e.Message;
                /// return ds.Tables[0].DefaultView;
                dv = ds.Tables[0].DefaultView;
                // dv = dbLedger.GetBySearch(itm.Value, TxtSearch.Text);            
                dgDeliveryAddress.DataSource = dv;
                dgDeliveryAddress.Columns[0].Visible = false;
                dgDeliveryAddress.Columns[1].Visible = false;
                dgDeliveryAddress.Columns[3].Visible = false;
                dgDeliveryAddress.Columns[5].Visible = false;
                // DataGridView1.Columns[1].Width = 550;
                // DataGridView1.Columns[2].Width = 100;

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
                    pnlParty.Width = txtParty.Width + txtParty.Width;
                    //     lstPartyEnglish.Top = pnlParty.Top;
                    // lstPartyLang.Top = lstPartyEnglish.Top;
                    // lstPartyLang.Height = pnlParty.Height - 10;
                    //lstPartyEnglish.Height = pnlParty.Height - 10;
                    lstPartyEnglish.Width = pnlParty.Width - 10;
                    lstPartyLang.Left = lstPartyEnglish.Right - txtParty.Width;// 380;
                    lstPartyLang.Width = txtParty.Width;// 385;

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

                pnlRate.Top = 150;
                pnlRate.Left = 430;
                pnlRate.Width = 120;
                pnlRate.Height = 80;

                pnlSalePurHistory.Width = 720;
                pnlSalePurHistory.Height = 235;
                pnlSalePurHistory.Top = pnlItemName.Height + 88;
                pnlSalePurHistory.Left = pnlItemName.Left;

                btnMixMode.Left = btnExit.Left;
                btnMixMode.Top = btnExit.Top + btnExit.Height + 5;
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
                pnlRateTypePassword.Location = new Point(205, 206);
                pnlMainParking.Location = new Point(205, 269);
                pnlPartyName.Location = new Point(205, 262);
                pnlStockGodown.Location = new Point(205, 183);
                pnlInvSearch.Location = new Point(711, 128);
                pnlTodaysSales.Location = new Point(790, 51);
                pnlTotalAmt.Location = new Point(790, 238);
                lblMsg.Location = new Point(254, 217);

                pnlPaymentType.Top = panel1.Top - 100;
                pnlPaymentType.Height = 100;
                pnlPaymentType.Width = 130;
                lstPaymentType.Font = ObjFunction.GetFont(FontStyle.Regular, 13);
                lblState.Font = ObjFunction.GetFont(FontStyle.Bold, 16);
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
                //dtItemQuery = ObjFunction.GetDataView("SELECT * from MItemNameDisplayType WHERE ItemNameTypeNo = " + ItemNameType).Table;
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
                lstPaymentType.SelectedIndex = 0;
                lstPaymentType.SelectedValue = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_DefaultPayType));
                txtPaymentType.Text = lstPaymentType.Text;

                dtpBillDate.Value = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD).ToString()).Date;
                dtpBillTime.Value = System.DateTime.Now;
                dtpBillTime.Format = DateTimePickerFormat.Time;
                lblCancelBll.Text = "";
                lblitemcount.Text = "0.00";
                // dgBill.Rows.Clear();//umesh added 

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
                if (Convert.ToInt32(lstPartyEnglish.SelectedValue) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))// && (ObjFunction.GetComboValue(cmbPaymentType) == 3))
                {

                }
                else
                    lstPaymentType.Enabled = true;

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDate)) == true) dtpBillDate.Focus();
                else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true)
                {
                    //  txtParty.Focus();
                    // txtParty.Text = txtParty.SelectedText;
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
                txtChargePer.Text = "0.00";
                txtSchemeDisc.Enabled = false; txtOtherDisc.Enabled = false; txtChrgRupees2.Enabled = false;
                BindGridPayType(0);
                // BindPayChequeDetails(0);
                //BindPayCreditDetails(0);
                pnlPartial.Visible = false;
                btnMixMode.Text = "Mix\r\nMode\r\n(F3)";
                btnBillCancel.Visible = DBGetVal.IsAdmin;
                ParkBillNo = 0;
                dtTRewardDtls = ObjFunction.GetDataView("SELECT PkSrNo, RewardNo, SchemeNo, SchemeDetailsNo, SchemeType, DiscPer, DiscAmount, SchemeAmount,0 As Status,SchemeAcheiverNo FROM TRewardDetails WHERE (PkSrNo = 0)").Table;
                dtTRewardToFrom = ObjFunction.GetDataView("SELECT 0 AS TypeNo,PkSrNo, RewardNo, RewardDetailsNo,SchemeDetailsNo, SchemeFromNo, FkStockNo,0 As 'ItemNo' FROM TRewardFrom WHERE (PkSrNo = 0)").Table;
                tempDate = dtpBillDate.Value.Date;

                //dtZeroTax = ObjFunction.GetDataView("Select TaxLedgerNo, SalesLedgerNo, Percentage, IsNull(pksrno,0) AS PkSrNo From MItemTaxSetting  Where SalesLedgerNo in(Select LedgerNo  From MLedger Where GroupNo=10) AND Percentage = 0").Table;

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
                    // lstPartyLang.SelectedValue = lstPartyEnglish.SelectedValue;
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
                    //lstPartyLang.SelectedValue = lstPartyEnglish.SelectedValue;
                }
                ObjFunction.FillList(lstPartyEnglish, "SELECT MLedger.LedgerNo, MLedger.LedgerName + ' ' + ISNULL(Mcity.Cityname, '') + '-' + ISNULL(MArea.AreaName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo  left outer join mcity on mcity.cityno=mledgerdetails.cityno  LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (26) and MLedger.ledgerno not in (24)) and MLedger.IsActive='true' ORDER BY LedgerName ");

            }
            Companycode = ObjQry.ReturnLong("select Statecode from Mfirm ", CommonFunctions.ConStr);

            ObjFunction.FillList(lstPaymentType, "Select PKPayTypeNo,PayTypeName From MPayType where IsActive='true' and PKPayTypeNo!=1 order by PayTypeName");
            ObjFunction.FillList(lstArea, "Select AreaNo,AreaName From Marea  Where  IsActive='true' order by AreaName");
            ObjFunction.FillList(lstBank, "Select BankNo,BankName From MBank where IsActive='true' order by BankName");
            ObjFunction.FillList(lstBranch, "Select BranchNo,BranchName From MBranch where IsActive='true' order by BranchName");
            // if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSalesMan)) == true)
            { ObjFunction.FillList(lstSalesMan, "Select SalesmanNo,SalesmanName From MSalesman  Where  IsActive='true' order by SalesmanName"); }


        }

        public void FillBrand()
        {
            //if (DBGetVal.KachhaFirm == false)
            //{
            //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            //    {
            //        ObjFunction.FillList(lstGroup1Lang, "Select ItemGroupNo,LangGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' )and esflag='false' ) and IsActive='true' order by ItemGroupName");
            //    }
            //    ObjFunction.FillList(lstGroup1, "Select ItemGroupNo,ItemGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' )and esflag='false' ) and IsActive='true' order by ItemGroupName");
            //}
            //else
            //{
            //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            //    {
            //        ObjFunction.FillList(lstGroup1Lang, "Select ItemGroupNo,LangGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' ) ) and IsActive='true' order by ItemGroupName");
            //    }
            //    ObjFunction.FillList(lstGroup1, "Select ItemGroupNo,ItemGroupName From MItemGroup Where  ItemGroupNo in (select  groupno from mitemmaster where itemno in (select itemno from MItemTaxInfo where FromDate<='" + dtpBillDate.Text.Trim() + "' ) ) and IsActive='true' order by ItemGroupName");
            //}

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
                tVoucherEntry.PkRefNo = tVoucherEntry.PkRefNo;
                txtPaymentType.Text = lstPaymentType.Text;
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
                txtChargePer.Text = "0.00";
                txtGrandTotal.Text = "0.00";
                txtOtherDisc.Text = "0.00";
                txtSchemeDisc.Text = "0.00";
                OrderType = tVoucherEntry.OrderType;
                txtRemark.Text = tVoucherEntry.Remark;
                DataTable dt = ObjFunction.GetDataView("Select Case When Debit<>0 then Debit Else Credit End,LedgerNo,SrNo From TVoucherDetails Where FKVoucherNo=" + ID + " order by VoucherSrNo").Table;

                double subTot = ObjQry.ReturnDouble("Select sum(Case When(SrNo<>508 AND SrNo<>506)Then Debit else -Credit End) from TVoucherDetails  Where FKVoucherNo=" + ID + " ", CommonFunctions.ConStr);
                txtSubTotal.Text = subTot.ToString();
                //long partyNo = ObjQry.ReturnLong("Select LedgerNo from TVoucherEntry  Where pKVoucherNo=" + ID + " ", CommonFunctions.ConStr);
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
                {
                    //  ObjFunction.FillList(lstPartyEnglish, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") and (IsActive='true' or LedgerNo = " + partyNo + ") order by LedgerName");
                }
                else
                {
                    //  ObjFunction.FillList(lstPartyEnglish, "SELECT MLedger.LedgerNo, MLedger.LedgerName + '-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerNam FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (" + GroupType.SundryDebtors + ")) AND (MLedger.IsActive = 'true' or MLedger.LedgerNo = " + partyNo + ") ORDER BY LedgerName ");
                    long areano = ObjQry.ReturnLong("select areano from marea where areano in(select areano from MLedgerDetails where ledgerno=" + PartyNo + ")", CommonFunctions.ConStr);
                    lstArea.SelectedValue = areano;
                    txtArea.Text = lstArea.Text;
                }
                lstPartyEnglish.SelectedValue = PartyNo;
                tempPartyNo = PartyNo;
                txtParty.Text = lstPartyEnglish.Text;
                //   PartyNo = ObjFunction.GetListValue(lstPartyEnglish);
                statecode = Convert.ToInt32(tVoucherEntry.StateCode);
                PartyNo = Convert.ToInt32(lstPartyEnglish.SelectedValue);

                //statecode = ObjQry.ReturnLong("Select stateCode from Mledger  where Ledgerno = " + PartyNo + " ", CommonFunctions.ConStr);
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
                // string StateName = lblState.Text = ObjQry.ReturnString("select StateName from Mstate where Statecode in (Select stateCode from Mledger  where Ledgerno = " + partyNo + ")", CommonFunctions.ConStr);
                if (PartyNo != 21)
                {
                    double amt = 0.00;
                    //amt = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno=" + PartyNo + " group by ledgerno", CommonFunctions.ConStr);
                    amt = ObjQry.ReturnDouble("exec GetOutstandingForBills '01-Apr-2017','" + DBGetVal.ServerTime + "'," + VoucherType + " ,1,'" + PartyNo + "' ", CommonFunctions.ConStr);
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

                        // TaxAmt = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce) / 100), 2);
                        //  txtChrgRupees2.Text = (((subTotal + totalTax + totalChrg - totalDisc) * Convert.ToDouble(txtChargePer.Text)) / 100).ToString();

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
                //   txtChargePer.Text = ((Convert.ToDouble(txtChrgRupees1.Text) * 100) / (Convert.ToDouble(txtGrandTotal.Text))).ToString();

                dgBill.Enabled = true;
                FillGrid();
                dgBill.Enabled = false;
                txtChargePer.Text = ((Convert.ToDouble(txtChrgRupees1.Text) * 100) / (Convert.ToDouble(txtGrandTotal.Text) - Convert.ToDouble(txtChrgRupees2.Text) - Convert.ToDouble(txtChrgRupees1.Text) - Convert.ToDouble(txtRoundOff.Text))).ToString("0.0");

                DataTable dtPartial = ObjFunction.GetDataView("Select Credit From TVoucherDetails Where FKVoucherNo=" + ID + " AND VoucherSrNo in (2,3) AND LedgerNo in(1,3) ").Table;
                if (dtPartial.Rows.Count == 2)
                {
                    txtTotalAmt.Text = "0.00";
                }
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
                    lblCancelBll.Font = new Font("Arial", 30, FontStyle.Bold);
                    lblCancelBll.Text = "Bill Cancel";
                    //BillFlag = true;
                }
                else if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    //BillFlag = false;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true) btnUpdate.Visible = true;
                }
                else if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + " and TR.TypeOfRef in(6))", CommonFunctions.ConStr) > 1)
                {
                    btnUpdate.Visible = false;
                    btnBillCancel.Visible = false;
                    //BillFlag = false;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true) btnUpdate.Visible = true;
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
                btnMixMode.Text = "Mix\r\nMode\r\n(F3)";
                dgCollectionDetails.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 210);
                if ((ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1) && PayType == 3)
                {
                    btnMixMode.Visible = false;
                }
                else if (PayType == 3 && tVoucherEntry.IsVoucherLock == false && tVoucherEntry.IsCancel == false)
                {
                    btnMixMode.Visible = true;
                    btnMixMode.Text = "Receipt\r\nMode";
                    // DisplayCollectionDetails();
                }
                else
                    btnMixMode.Visible = false;

                //Transport
                cmbTransporter.SelectedValue = (tVoucherEntry.TransporterCode == 0) ? "0" : tVoucherEntry.TransporterCode.ToString();
                cmbTransPayType.SelectedValue = (tVoucherEntry.TransPayType == 0) ? "0" : tVoucherEntry.TransPayType.ToString();
                txtLRNo.Text = tVoucherEntry.LRNo;
                cmbTransMode.SelectedValue = (tVoucherEntry.TransportMode == 0) ? "0" : tVoucherEntry.TransportMode.ToString();
                txtTransNoOfItems.Text = (tVoucherEntry.TransNoOfItems == 0) ? "0" : tVoucherEntry.TransNoOfItems.ToString();

                FillTodaysSalesDetails();
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
                            " IsNull(TOtherStockDetails.FKOtherVoucherNo,0) As PONo, IsNull(TOtherStockDetails.PkSrNo,0) As PKStockOtherDetailsNo,IsNull(TOtherStockDetails.FKOtherStockTrnNo,0) AS FKOtherStockTrnNo ,TStock.IType as ESFlag,TStock.Remarks as Remarks,TStock.SalesMan,TStock.Hamali" +

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
                   " IsNull(TOtherStockDetails.FKOtherStockTrnNo,0) AS FKOtherStockTrnNo ,TStock.IType as ESFlag,TStock.Remarks as Remarks ,TStock.SalesMan,TStock.Hamali" + // TStock.CessValue ,TStock.PackagingCharges ,TStock.OtherCharges as Dekharek,TStock.Freight ,TStock.NoOfBag  , MItemMaster.CessValue,MItemMaster.Dhekhrek  " +


                                " FROM MItemMaster INNER JOIN TStock ON MItemMaster.ItemNo = TStock.ItemNo INNER JOIN TVoucherDetails ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo " +
                                            " INNER JOIN  MItemTaxInfo ON TStock.FkItemTaxInfo = MItemTaxInfo.PkSrNo left join  MItemTaxInfo  as MItemTaxInfo_2 ON TStock.FkItemTaxInfo2 = MItemTaxInfo_2.PkSrNo " +
                                             " INNER JOIN MUOM ON TStock.FkUomNo = MUOM.UOMNo INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                                             " LEFT OUTER JOIN TOtherStockDetails ON TStock.PkStockTrnNo = TOtherStockDetails.FKStockTrnNo left join  MItemTaxInfo  as MItemTaxInfo_3 ON TStock.FKAddItemTaxSettingNo = MItemTaxInfo_3.PkSrNo " +
                                            " WHERE      (TVoucherDetails.FkVoucherNo = " + ID + ") ORDER BY TStock.PkStockTrnNo";
                }
                if (DBGetVal.KachhaFirm == false)
                    sqlQuery = sqlQuery.Replace("SELECT 0 AS Sr,(SELECT case when esflag='False' then ItemName+'*' else ItemName end as ItemName", "SELECT 0 AS Sr,(SELECT ItemName");
                //dbTVoucherEntry.FillGridSales(1, 1,out sqlQuery);
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
                    // dtBillCollect.Add(ObjFunction.GetDataView("Exec GetStockGodownDetails " + dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value.ToString() + "," + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value + "").Table);
                }
                // FillGodownDetails(strStkNo);
                dgBill.Rows.Add();
                lblitemcount.Text = (dgBill.Rows.Count - 1).ToString("0.00");
                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                CalculateTotal();
                BindGridPayType(ID);
                ///  BindPayChequeDetails(ID);
                BindPayCreditDetails(ID);
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
            //  State = true;
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            pnlPrinting.Enabled = true;
            dgBill.Rows.Clear();

            //if (lstGroup1.Items.Count != 0)
            //    lstGroup1.SelectedIndex = 1;

            ID = 0;
            PID = 0;
            partialPay = new PartialPayment();
            partialPayAdjust = new PartialPaymentAdjust();
            ManualBill = false;
            IsDeliveryAddress = false;
            TempBillNo = 0;
            //long cnt=  ObjQry.ReturnLong("select count(voucheruserno ) as cnt from tvoucherentry where vouchertypecode=15 and iscancel='false' ", CommonFunctions.ConStr);
            //  if(cnt>=100){
            //      OMMessageBox.Show("Demo Version Expired..."+"\n"+"Please Contact On Mob.No.9225522282");
            //      btnExit_Click(sender, e);
            //      return;

            //  }
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsManualBillNo)) == true)
            {
                txtInvNo.Enabled = true;
                txtInvNo.ReadOnly = false;
                TempBillNo = Convert.ToInt32(txtInvNo.Text);
                ManualBill = true;
            }
            //   dgBill.Visible = true;
            dgBill.Enabled = true;
            txtCustCode.Text = "";
            FillList();
            FillBrand();
            InitControls();
            if (ManualBill == true)
            {
                txtInvNo.Focus();
            }
            txtRemark.Text = "";
            btnCancel.Enabled = true;
            btnCashSave.Visible = true;
            btnCreditSave.Visible = true;
            btnBillCancel.Enabled = false;
            btnCashSave.Enabled = true; btnCreditSave.Enabled = true;
            btnPrint.Visible = false;
            btnSearch.Visible = false;
            btnDeliveryAddress.Visible = true;
            if (Convert.ToInt32(lstPartyEnglish.SelectedValue) != Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_DefaultPartyAC)))
            { btnCreditSave.Visible = true; }
            else
            { btnCreditSave.Visible = false; }
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSalesMan)) == true)
            {
                lstSalesMan.SelectedIndex = 0;
                txtSalesMan.Text = lstSalesMan.Text;
            }
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
                //  dtpBillTime.Focus();

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnParty)) == true)
                {
                    // pnlParty.Visible = true;
                    //lstPartyEnglish.Focus();
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
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                        amt = ObjQry.ReturnDouble("exec GetOutstandingForBills '01-Apr-2019','" + DBGetVal.ServerTime + "'," + VoucherType  + ",1,'" + PartyNo + "' ", CommonFunctions.ConStr);
//                    amt = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno=" + PartyNo + " group by ledgerno", CommonFunctions.ConStr);
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
                    // long itemno = dgBill.Rows[i].Cells[ColIndex.ItemNo].Value;
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
                //e.KeyChar = Convert.ToChar(0);
                txtParty.Text = "";
                pnlParty.Visible = true;
                lstPartyEnglish.Focus();
            }
        }
        public void FillCreditLimit()
        {
            try
            {
                lblCreditLimit.Text = " Cr Limit : 0\r\n Cr Bal    : 0";
                //  btnOutstanding.Visible = true;
                if (ObjFunction.GetListValue(lstPartyEnglish) != Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))// && (ObjFunction.GetComboValue(cmbPaymentType) == 3))
                {
                    double CreditLimit = ObjQry.ReturnDouble("Select isNull((CreditLimit),0) from MLedgerDetails where LedgerNo=" + PartyNo + "", CommonFunctions.ConStr);
                    lblCreditLimit.Text = "Cr Limit : " + CreditLimit.ToString();
                    double AvlBal = ObjQry.ReturnDouble("Select OpAmt from " +
                        " GetCurrentLedgerBalance(" + PartyNo + ", " +
                        " " + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                    AvlBal = CreditLimit - AvlBal;
                    lblCreditLimit.Text += "\r\nCr Bal : " + AvlBal.ToString();
                    if (AvlBal >= 0)
                    {
                        lblCreditLimit.ForeColor = Color.Maroon;
                    }
                    else if (AvlBal < 0)
                    {
                        lblCreditLimit.ForeColor = Color.Red;
                    }
                    btnCreditSave.Enabled = true;
                    if (ID == 0) btnMixMode.Visible = true;
                }
                else
                {
                    btnCreditSave.Enabled = false;
                    if (btnSave.Visible == true)
                    {
                        if (ID == 0)
                            btnMixMode.Visible = true;//btnMixMode.Visible = false;
                    }
                }
                if (ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where (LedgerNo=" + PartyNo + ") And  (VoucherDate <= '" + dtpBillDate.Text + "') AND (EffectiveDate >= '" + dtpBillDate.Text + "') ", CommonFunctions.ConStr) == 0)
                    QutFlag = false;
                else
                    QutFlag = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                    else if (txtSalesMan.Visible == true)
                    {
                        txtSalesMan.Focus();
                    }
                    else {
                        dgBill.Focus();
                    }

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
                    if ((Convert.ToInt32(lstPartyEnglish.SelectedValue) != Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_DefaultPartyAC))) && (PartyNo != 21))
                    { btnCreditSave.Visible = true; }
                    else
                    { btnCreditSave.Visible = false; }
                    //lblOutstanding.Text = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno="+ PartyNo + " group by ledgerno",CommonFunctions.ConStr).ToString();
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                    {
                        if (PartyNo != 21)
                        {
                            double amt = 0.00;
                            
                            amt = ObjQry.ReturnDouble("exec GetOutstandingForBills '01-Apr-2019','" + DBGetVal.ServerTime + "'," + VoucherType +" ,1,'" + PartyNo + "' ", CommonFunctions.ConStr);
//                            amt = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno=" + PartyNo + " group by ledgerno", CommonFunctions.ConStr);
                            if (amt > 0)
                            {
                                lblOutstanding.Text = "Out Recive(RS) :" + amt;
                            }
                            else
                                lblOutstanding.Text = "Out Pay(RS) :" + amt;
                        }
                        else
                            lblOutstanding.Text = "Out Recive(RS) :0.00";
                    }
                    HistoryMaintain = ObjQry.ReturnBoolean("Select IsPartyWiseRate From MLedger Where LedgerNo=" + PartyNo + "", CommonFunctions.ConStr);
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPartyWiseDisc)) == true)
                    {
                        PartyDiscPerceItemLevel = ObjQry.ReturnDouble("Select DiscPer From MLedgerDetails Where LedgerNo=" + PartyNo + "", CommonFunctions.ConStr);
                    }
                    FillCreditLimit();
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
                    // e.KeyChar = Convert.ToChar(0);
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
                    pnlRateTypeH.Visible = false;
                    dgBill.Focus();

                    dgBill_KeyDown(sender, new KeyEventArgs(Keys.Enter));

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

        private void txtPaymentType_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_Charges1Display)) == true)
                        {
                            txtChargePer.Focus();
                            //txtChrgRupees1.Focus();
                        }
                        else
                        {
                            txtRemark.Focus();
                        }
                    }
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    txtPaymentType.Text = "";
                }
                else
                {
                    //e.KeyChar = Convert.ToChar(0);
                    pnlPaymentType.Visible = true;
                    lstPaymentType.Focus();
                    lstPaymentType.SelectedIndex = 0;
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
                    if (ObjFunction.GetListValue(lstPartyEnglish) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))
                    {
                        if (ControlUnder == 3 || ControlUnder == 4)
                        {
                            OMMessageBox.Show("This PayType is Not Valid For Current party .. Select other PayType...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            lstPaymentType.Focus();
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
                                    //dgPayChqDetails.Columns[3].Visible = false;
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
                                    pnlPaymentType.Visible = false;
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
                        pnlPaymentType.Visible = false;
                        e.SuppressKeyPress = true;
                        btnSave.Enabled = true;
                        btnSave.Focus();
                        if (txtChrgRupees1.Enabled == true)
                        { txtChrgRupees1.Focus(); }
                        else
                        {
                            txtRemark.Focus();
                        }
                    }
                    else
                    {
                        pnlPartial.Visible = false;
                        //cmbPaymentType.TabIndex = 10;
                        pnlPaymentType.Visible = false;
                        btnSave.Enabled = true;
                        if (txtChrgRupees1.Enabled == true)
                        { txtChrgRupees1.Focus(); }
                        else
                        {
                            txtRemark.Focus();
                        }
                    }
                    e.SuppressKeyPress = true;
                    txtPaymentType.Text = lstPaymentType.Text;
                }
                if (e.KeyCode == Keys.F3)
                {
                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                    {
                        dgPayType.Rows[i].Cells[2].Value = "0.00";
                    }
                    pnlPartial.Visible = false;
                    //cmbPaymentType.TabIndex = 10;
                    pnlPaymentType.Visible = false;
                    btnSave.Enabled = true;
                    if (txtChrgRupees1.Enabled == true)
                    { txtChrgRupees1.Focus(); }
                    else
                    {
                        txtRemark.Focus();
                    }
                }
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
                    //e.KeyChar = Convert.ToChar(0);
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
                // CalculateGridValues();
                int RowIndex = dgBill.CurrentCell.RowIndex;
                long ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);

                long subUomno = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.UOMNo].Value);
                double Qty = Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value);
                if (dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value == null || Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0)
                {
                    // ObjFunction.FillList(lstRate, "pksrno", ObjFunction.GetListValue(lstRateType));
                    if (ItemType == 2)
                    {
                        //long a = Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain));
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
                            //    else
                            //        str = "select pksrno," + ObjFunction.GetComboValueString(cmbRateType) +
                        }
                        else
                            str = "select pksrno," + lstRateType.SelectedValue +
                        //           " from GetItemRateAll(" + ItemNo + "," + UOMNo + ",null ,'" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
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
                            // For MRP Double Rate Show, //" + dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value + "
                        }
                    }
                    //string Columnname = "";
                    //    if (RateTypeNo == 1) Columnname = "ASaleRate";
                    //    else if (RateTypeNo == 2) Columnname = "BSaleRate";
                    //    else if (RateTypeNo == 3) Columnname = "CSaleRate";
                    //    else if (RateTypeNo == 4) Columnname = "DSaleRate";
                    //    else if (RateTypeNo == 5) Columnname = "ESaleRate";
                    //    else if (RateTypeNo == 6) Columnname = "MRP";
                    //    else if (RateTypeNo == 7) Columnname = "PurRate";
                    //  str = "select pksrno," + lstRateType.SelectedValue + " From GetItemRateALL(" + ItemNo + "," + subUomno + ",NULL,NULL,NULL) ";
                    // str = "select pksrno," + Columnname + " From GetItemRateALL(" + ItemNo + "," + subUomno + ",NULL,NULL,NULL) ";
                    ObjFunction.FillList(lstRate, str);

                    //if (State == true)
                    //{
                    //    strSql = "Select * From GetItemRateALL(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",51,NULL) As t Where r.ItemNo=t.ItemNo " +
                    //  " union all Select * From GetItemRateALL(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",52,NULL) As t Where r.ItemNo=t.ItemNo";
                    //}
                    //else
                    //{
                    //    strSql = "Select * From GetItemRateALL(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",53,NULL) As t Where r.ItemNo=t.ItemNo " +
                    //    " union all Select * From GetItemRateALL(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + "," + subUomno + ",NULL,NULL,NULL) AS r,GetItemTaxAll(" + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",54,NULL) As t Where r.ItemNo=t.ItemNo";
                    //}
                    //dtRateSetting = ObjFunction.GetDataView(strSql).Table;
                    //if (dtRateSetting.Rows.Count > 0)
                    //{
                    if (lstRate.Items.Count == 1)
                    {
                        lstRate.SelectedIndex = 0;
                        dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = dgBill.Rows[RowIndex].Cells[ColIndex.TempRate].Value = lstRate.Text;
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                        dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                        dgBill.Focus();
                        //BindGrid(dgBill.CurrentRow.Index);

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
                    else
                    {
                        //error invalid Qty or UOM
                    }
                }
                else
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { RowIndex, ColIndex.Rate, dgBill });
                    dgBill.CurrentCell = dgBill[ColIndex.Rate, RowIndex];
                    dgBill.Focus();
                    //BindGrid(dgBill.CurrentRow.Index);

                    if (StopOnRate == false)
                    {
                        BindGrid(RowIndex);

                        //Rate_MoveNext();
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
                    // txtBUom.Text = lstUOM.Text;
                    int RowIndex = dgBill.CurrentCell.RowIndex;
                    if (dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value != null &&
                   dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value.ToString() != lstUOM.SelectedValue.ToString())
                    {
                        dgBill.CurrentRow.Cells[ColIndex.Rate].Value = "0.00";//lstRate.Text;
                        dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0; // lstRate.SelectedValue;
                    }
                    //long a=Convert.ToInt32(dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value);
                    dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text;
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOM.SelectedValue);
                    pnlUOM.Visible = false;
                    Rate_Start();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    e.SuppressKeyPress = true;
                    //lstUOM.Focus();
                    //lstUOM.SelectedIndex = 0;
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
                        //  dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                        qty = true;
                        CalculateTotal();
                        //  Clear();
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

                pnlSalePurHistory.Visible = false;

                DisplayList(false);
                //NavigationDisplay(2);
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
                btnCreditSave.Visible = false;
                btnCashSave.Visible = false;
                btnPrint.Visible = true;
                btnSearch.Visible = true;
                dtDelete.Clear();
                btnNew.Focus();
                pnlPrinting.Visible = true;
                rbEnglish.Enabled = true;
                rbEnglish.Enabled = true;
               //pnlPrinting. = true;
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
            pnlPartial.Visible = flag;
            pnlPaymentType.Visible = flag;
        }

        public void viewmode()
        {
            //Clear();
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
                        //DataTable dtBarCodeItemNo = ObjFunction.GetDataView("Select ItemNo from MItemMaster where Barcode = '" + dgBill.CurrentCell.Value + "'").Table;
                        long TempItemno = ObjQry.ReturnLong("Select ItemNo from MItemMaster where Barcode = '" + dgBill.CurrentCell.Value + "'", CommonFunctions.ConStr);
                        string ItemList = "SELECT MItemMaster.ItemNo,MItemMaster.ItemName AS ItemName, MRateSetting.@cmbRateType@ AS SaleRate, MUOM.UOMName, MRateSetting.MRP,case when MItemMaster.esflag='false' then MRateSetting.stock else MRateSetting.stock2 end AS Stock, (select uomname from mitemmaster inner join muom on MItemMaster.uoml=muom.uomno where MItemMaster.itemno=MRateSetting.itemno ) AS stkUOM, 0 AS SaleTax, 0 AS PurTax,  MItemMaster.CompanyNo, MItemMaster.Barcode,  MRateSetting.PkSrNo As RateSettingNo, MItemMaster.UOMDefault,MRateSetting.PurRate,0 As DiscPerce,MItemMaster.ShortCode ,MItemMaster.esflag  FROM  MItemMaster INNER JOIN MUOM ON MItemMaster.UOMDefault = MUOM.UOMNo  LEFT OUTER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL,null) AS MRateSetting  ON  MItemMaster.ItemNo = MRateSetting.ItemNo AND MItemMaster.UOMDefault = MRateSetting.UOMNo WHERE     (MItemMaster.ItemNo <> 1) AND (MItemMaster.IsActive = 'true') AND (MItemMaster.GroupNo = @Param1@)  ORDER BY MItemMaster.ItemName";

                        // if (qNo == 2) //for itemlist or item name
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
                            //if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                            //{
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
                            //}
                            //else
                            //{
                            //    pnlItemName.Visible = true;
                            //    dgItemList.CurrentCell = dgItemList[1, 0];
                            //    dgItemList.Focus();
                            //}
                        }
                        //DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                        //if (dtItemList.Rows.Count > 0)
                        //{
                        //    dgItemList.DataSource = dtItemList.DefaultView;
                        //    pnlItemName.Visible = true;
                        //    dgItemList.CurrentCell = dgItemList[1, 0];
                        //    dgItemList.Focus();
                        //}
                        //else
                        //{
                        //    //DisplayMessage("Items Not Found......");
                        //    OMMessageBox.Show("SKU Not Found.\nPRESS ESCAPE TO CONTINUE....", "Information", OMMessageBoxButton.Escape, OMMessageBoxIcon.Information);
                        //}
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
                    ItemList = ItemList.Replace("AND MItemMaster.CompanyNo<>2", "   AND MItemMaster.CompanyNo <> 2 and ItemType not in(2) ");

                    if (DBGetVal.KachhaFirm == true)
                    {

                        ItemList = ItemList.Replace("and esflag='false'", "  ");
                    }
                    if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))//===//umesh
                    {

                        //ItemList = ItemList.Replace("AND MItemMaster.CompanyNo<>2", "   AND MItemMaster.CompanyNo <> 2 and ItemType not in(2) ");

                        ObjFunction.FillList(lstGroup1, ItemList);
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                        {
                            ItemList = ItemList.Replace("ItemGroupName from", "LangGroupName from");

                            ObjFunction.FillList(lstGroup1Lang, ItemList.Replace("ItemGroupName From", "LangGroupName from"));
                        }
                        //  ObjFunction.FillList(lstGroup1, ItemList);
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
                            ItemList = ItemList.Replace("MRateSetting.@cmbRateType@", " (CASE WHEN (IsNull((SELECT TOP (1) IsNull(tStock.Rate, 0) FROM TStock INNER JOIN TVoucherEntry ON TStock.FkVoucherNo = TVoucherEntry.PkVoucherNo WHERE (TVoucherEntry.LedgerNo = " + PartyNo + ") AND (TVoucherEntry.VoucherDate <= '" + dtpBillDate.Text + "') AND (TVoucherEntry.EffectiveDate >= '" + dtpBillDate.Text + "') " +
                                " AND  (tstock.FkRateSettingNo = MRateSetting.PkSrNo)),0) = 0 ) THEN MRateSetting." + lstRateType.SelectedValue + " ELSE (SELECT TOP (1) IsNull(TStock.Rate, 0)  FROM  TStock " +
                                " INNER JOIN  TVoucherEntry ON TStock.FkVoucherNo = TVoucherEntry.pkVoucherNo  WHERE (TVoucherEntry.LedgerNo =" + lstPartyEnglish.SelectedValue + ") AND (TVoucherEntry.VoucherDate <= '" + dtpBillDate.Text + "') AND (TVoucherEntry.EffectiveDate >= '" + dtpBillDate.Text + "') AND (TSTOCK.FkRateSettingNo = MRateSetting.PkSrNo))End) ");
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
                    // ItemList = ItemList.Replace("dbo.GetItemRateAll(NULL, NULL, NULL, NULL ,", "dbo.GetItemRateAll(" + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : Param1Value) + ",");
                    ItemList = ItemList.Replace(" NULL,null) AS", " NULL," + (Convert.ToInt64(lstGroup1.SelectedValue) != 0 ? lstGroup1.SelectedValue.ToString() : "NULL") + " ) AS ");
                    ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MItemMaster.LangShortDesc<>'') then MItemMaster.LangShortDesc else MItemMaster.LangFullDesc end AS ItemNameLang,");
                    ItemList = ItemList.Replace("ORDER BY", "And MItemMaster.ItemType <>2  Order by ");
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

                    //else
                    //{
                    //    pnlItemName.Visible = true;
                    //    dgItemList.CurrentCell = dgItemList[1, 0];
                    //    dgItemList.Focus();
                    //}

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
                    //txtBarCode.Text = "";
                    //txtBarCode.Focus();
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
                {
                    // lstPartyLang.SelectedValue = lstPartyEnglish.SelectedValue;
                    lstPartyLang.SelectedIndex = lstPartyEnglish.SelectedIndex;
                    //lstPartyLang.SelectedValue = lstPartyEnglish.SelectedValue;
                }//Ledgerno = Convert.ToInt64(lstPartyEnglish.SelectedValue);
            }
        }

        private void lstPartyLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            //{
            //    if (lstPartyEnglish.Items.Count > 0)
            //        lstPartyEnglish.SelectedIndex = lstPartyLang.SelectedIndex;
            //}
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
            public static int Hamali = 67;
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
                                //else
                                //    CalculateTotal();
                                dgBill.Focus();

                                // 
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
                        // if (ItemExistScheme(i, Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value), out rwindex) == true)
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
                        //  dgBill.Rows.Add();
                        // dgBill.Focus();
                    }
                    pnlSalePurHistory.Visible = false;
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
                else if (Convert.ToInt32(dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value) == 0)
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
                                    lstPaymentType_KeyDown(lstPaymentType, new KeyEventArgs(Keys.Enter));
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
                                    lstPaymentType_KeyDown(lstPaymentType, new KeyEventArgs(Keys.Enter));
                                    return;
                                }
                                break;
                            }
                        case "B":
                            {
                                if (btnSave.Visible)
                                {
                                    if (ObjFunction.GetListValue(lstPaymentType) == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))// && (ObjFunction.GetComboValue(cmbPaymentType) == 3))
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
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.ItemName, dgBill });
                        }
                        else
                            NewItemAdd(strB);
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
                                    //  dgBill_CellValueChanged(dgBill, new DataGridViewCellEventArgs(ColIndex.Quantity, rwindex));
                                    // qty = true;
                                    CalculateTotal();
                                    //  Clear();
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
                                //   UOM_Start();
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

        public void NewItemAdd(string BarCode)
        {
            if (ObjFunction.CheckAllowMenu(10) == false) return;
            Form NewF = new Master.ItemMasterAE(-1, BarCode);
            ObjFunction.OpenForm(NewF);

            // if (((Master.ItemMasterAE)NewF).ShortID != 0)
            {
                //System.Threading.Thread.Sleep(2000);
                // string barcode = ObjQry.ReturnString("Select BarCode From MItemMaster where ItemNo=" + ((Master.ItemMasterAE)NewF).ShortID + "", CommonFunctions.ConStr);
                int rwindex = dgBill.CurrentCell.RowIndex;
                dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = BarCode;
                dgBill_CellEndEdit(dgBill, new DataGridViewCellEventArgs(ColIndex.ItemName, rwindex));
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
                // dgBill.CurrentRow.Cells[ColIndex.ESFlag].Value = ObjQry.ReturnBoolean("select ESFlag from Mitemmaster where itemno =" + dt.Rows[0]["Itemno"].ToString(), CommonFunctions.ConStr);
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
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.IsRateChange].Value = "0";

                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();

                if (StopOnQty == true)
                {
                    if (dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex].Value == null)
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
                        //        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = 1;
                        dgBill.Focus();
                    }
                    else
                        Qty_MoveNext();
                }
                else if (StopOnRate == true)
                {
                    UOM_Start();

                    //if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value == null || dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value.ToString() == "")
                    //    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                    //dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex];
                    //Rate_MoveNext();
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
                //int a = Convert.ToInt32(dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value);

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
                //  int a = Convert.ToInt32(dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value);
                pnlUOM.Location = new Point(dgBill.Location.X + loc.X,
                dgBill.Location.Y + loc.Y + loc.Height);

                ObjFunction.FillList(lstUOM, "Select UOMNo,UOMName From GetUOMList(NULL," + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value + ",NULL)");
                if (lstUOM.Items.Count > 1)
                {
                    //txtBUom.Focus();
                    pnlUOM.Visible = true;
                    lstUOM.Focus();
                }
                else
                {
                    dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text;
                    dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = lstUOM.SelectedValue;
                    dgBill.CurrentCell = dgBill[ColIndex.Rate, row];//stop on rate focusdgBill[4, row];
                    //if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value==null)
                    //   // if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].EditedFormattedValue.ToString().Trim() != ""
                    //{
                    //    dgBill.Rows.Add();
                    //}
                    Rate_Start();
                    // BindGrid(row);
                    dgBill.Focus();

                }
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
                       " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP,r.Hamali FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",51,NULL) As t " +
                       " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo union all SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                       " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP,r.Hamali FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",52,NULL) As t " +
                       " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;
                }
                else
                {
                    dt = ObjFunction.GetDataView("SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                           " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP ,r.Hamali FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",53,NULL) As t " +
                           " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo union all SELECT r.ItemNo, r.MKTQty, r.StockConversion, t.TaxLedgerNo, t.SalesLedgerNo, " +
                           " t.PkSrNo,t.Percentage,cast(r.MRP as numeric(18,2)) as MRP ,r.Hamali FROM MRateSetting As r,GetItemTaxAll(" + ItemNo + ", '" + dtpBillDate.Value.ToString("dd-MMM-yyyy") + " " + DBGetVal.ServerTime.ToLongTimeString() + "', " + GroupType.SalesAccount + ",54,NULL) As t " +
                           " WHERE r.PkSrNo = " + RateSettingNo + " AND r.ItemNo = t.ItemNo").Table;
                }
                if (dt.Rows.Count > 0)
                {


                    dgBill.Rows[RowIndex].Cells[ColIndex.MKTQuantity].Value = Convert.ToInt64(dt.Rows[0][1].ToString());
                    dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value = Convert.ToDouble(dt.Rows[0][7].ToString());
                    StockConFactor = Convert.ToDouble(dt.Rows[0][2].ToString());

                    dgBill.Rows[RowIndex].Cells[ColIndex.StockFactor].Value = StockConFactor;
                    if (VoucherType == 115)
                    {
                        dgBill.Rows[RowIndex].Cells[ColIndex.Hamali].Value = Convert.ToDouble(dt.Rows[0][8].ToString());
                    }
                    else { dgBill.Rows[RowIndex].Cells[ColIndex.Hamali].Value = 0; }
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
                    //  Boolean slab = ObjQry.ReturnBoolean("Select GSTSlab from Mitemmaster where ItemNo=" + ItemNo, CommonFunctions.ConStr);
                    Boolean slab = false;
                    if (slab == true)
                    {
                        DataTable SecondDt;
                        double rate = Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Rate].Value);// +Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TaxAmount].Value) + Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TaxAmount2].Value);
                        if (rate >= 1000.00)
                        {
                            if (State == true)
                            {
                                SecondDt = ObjFunction.GetDataView("Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod" +
                                                  " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                  " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=6.00 and MLEDGERT.GroupNo=" + GroupType.SGST + " and MLEDGER.groupno=10  " +
                                                  " union all Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod " +
                                                  " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                  " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=6.00 and MLEDGERT.GroupNo=" + GroupType.CGST + "and MLEDGER.groupno=10  ").Table;

                            }
                            else
                            {
                                SecondDt = ObjFunction.GetDataView("Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod" +
                                                 " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                 " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=12.00 and MLEDGERT.GroupNo=" + GroupType.IGST + " and MLEDGER.groupno=10  " +
                                                 " union all Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod " +
                                                 " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                 " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=0.00 and MLEDGERT.GroupNo=" + GroupType.Cess + "and MLEDGER.groupno=10  ").Table;

                            }
                        }
                        else
                        {

                            if (State == true)
                            {
                                SecondDt = ObjFunction.GetDataView("Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod" +
                                                  " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                  " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=2.50 and MLEDGERT.GroupNo=" + GroupType.SGST + " and MLEDGER.groupno=10  " +
                                                  " union all Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod " +
                                                  " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                  " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=2.50 and MLEDGERT.GroupNo=" + GroupType.CGST + "and MLEDGER.groupno=10 ").Table;

                            }
                            else
                            {
                                SecondDt = ObjFunction.GetDataView("Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod" +
                                                 " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                 " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=5.00 and MLEDGERT.GroupNo=" + GroupType.IGST + " and MLEDGER.groupno=10  " +
                                                 " union all Select MItemTaxInfo.PkSrNo, MItemTaxInfo.ItemNo, MItemTaxInfo.TaxLedgerNo, MItemTaxInfo.SalesLedgerNo,MItemTaxInfo.Percentage, MItemTaxInfo.FromDate, MItemTaxInfo.CalculationMethod " +
                                                 " From Mitemmaster INNER JOIN MItemTaxInfo ON Mitemmaster.ItemNo = MItemTaxInfo.ItemNo INNER JOIN MLEDGER ON MItemTaxInfo.SalesLedgerNo = MLEDGER.LedgerNo INNER JOIN MLEDGER AS MLEDGERT ON MItemTaxInfo.TaxLedgerNo = MLEDGERT.LedgerNo" +
                                                 " where MItemTaxInfo.itemno=" + ItemNo + " and percentage=0.00 and MLEDGERT.GroupNo=" + GroupType.Cess + "and MLEDGER.groupno=10  ").Table;

                            }
                        }
                        dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo].Value = Convert.ToInt64(SecondDt.Rows[0][2].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo].Value = Convert.ToInt64(SecondDt.Rows[0][3].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo].Value = Convert.ToInt64(SecondDt.Rows[0][0].ToString());
                        dgBill.Rows[RowIndex].Cells[ColIndex.SGSTPercentage].Value = Convert.ToDouble(SecondDt.Rows[0][4].ToString());

                        if (State == true)
                        {
                            dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo2].Value = Convert.ToInt64(SecondDt.Rows[1][2].ToString());
                            dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo2].Value = Convert.ToInt64(SecondDt.Rows[1][3].ToString());
                            dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo2].Value = Convert.ToInt64(SecondDt.Rows[1][0].ToString());
                            dgBill.Rows[RowIndex].Cells[ColIndex.CGSTPercentage].Value = Convert.ToDouble(SecondDt.Rows[1][4].ToString());

                        }
                        else
                        {
                            dgBill.Rows[RowIndex].Cells[ColIndex.TaxLedgerNo2].Value = 0;
                            dgBill.Rows[RowIndex].Cells[ColIndex.SalesLedgerNo2].Value = 0;
                            dgBill.Rows[RowIndex].Cells[ColIndex.PkItemTaxInfo2].Value = 0;
                            dgBill.Rows[RowIndex].Cells[ColIndex.CGSTPercentage].Value = 0;
                        }


                    }
                    else
                    {
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
                txtChrgRupees2.Text = "0.00";
                double TotHamali = 0;
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
                            if (dgBill.Rows[i].Cells[ColIndex.Hamali].Value == null) dgBill.Rows[i].Cells[ColIndex.Hamali].Value = 0.00;

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

                            TotHamali += (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) == 0) ? 0 : Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Hamali].Value);
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == true)//Reverse or forward calculation
                            {
                                ttax = Convert.ToDouble(((tAmount * TotalTaxPerce) / (100 + TotalTaxPerce)).ToString("0.00"));
                                ttRate = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) == 0) ? 0 : Math.Round(Convert.ToDouble(((tAmount - ttax) / SubQty) * SubMktQty), 2);
                                APMCAMOUNT = PAckingCharges;

                                TaxAmt = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce) / 100), 2);
                                TaxAmt2 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce2) / 100), 2);
                                TaxAmt3 = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce3) / 100), 2);

                                //dgBill.Rows[i].Cells[ColIndex.Amount].Value = (tAmount+APMCAMOUNT).ToString("0.00");
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

                            //dgBill.Rows[i].Cells[ColIndex.Amount].Value = tAmount.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiscAmount].Value = Disc1.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.DiscAmount2].Value = Disc2.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value = TaxAmt.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.CGSTAmount].Value = TaxAmt2.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.IGSTAmount].Value = TaxAmt3.ToString("0.00");

                            dgBill.Rows[i].Cells[ColIndex.NetRate].Value = ttRate.ToString("0.00");
                            dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = ((tAmount - ttax)).ToString("0.00");
                            //  dgBill.Rows[i].Cells[ColIndex.NetAmt].Value = (ttRate * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value)).ToString("0.00");
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
                    txtChrgRupees2.Text = TotHamali.ToString();
                    TotalAmt = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text) + totalTax) - Convert.ToDouble(txtTotalDisc.Text)).ToString("0.00"));

                    if (txtSchemeDisc.Text.Trim() == "") txtSchemeDisc.Text = "0.00";
                    if (txtOtherDisc.Text.Trim() == "") txtOtherDisc.Text = "0.00";
                    if (txtDiscRupees1.Text.Trim() == "") txtDiscRupees1.Text = "0.00";
                    //umesh--21-10-2018
                    //    TotalAmt -= Convert.ToDouble(txtSchemeDisc.Text) + Convert.ToDouble(txtDiscRupees1.Text) + Convert.ToDouble(txtOtherDisc.Text);

                    if ((Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) + Convert.ToDouble(txtTotalTax.Text.Trim())) != 0)
                        //umesh--21-10-2018
                        //  txtDiscRupees1.Text = Convert.ToDouble((100 * Convert.ToDouble(txtDiscount1Per.Text)) / (Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) - (Convert.ToDouble(txtSchemeDisc.Text.Trim()) + Convert.ToDouble(txtOtherDisc.Text.Trim())) + Convert.ToDouble(txtTotalTax.Text.Trim()))).ToString("0.00");
                        txtDiscRupees1.Text = Math.Round(Convert.ToDouble((Convert.ToDouble(txtDiscount1Per.Text) * (Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) + Convert.ToDouble(txtTotalTax.Text.Trim()))) / 100), 2).ToString();


                    totalDisc = Convert.ToDouble(txtDiscRupees1.Text) + Convert.ToDouble(txtSchemeDisc.Text) + Convert.ToDouble(txtOtherDisc.Text);

                    if (Convert.ToDouble(txtChargePer.Text) > 0.0)
                    {
                        // TaxAmt = Math.Round(Convert.ToDouble(((tAmount + APMCAMOUNT - ttax) * TaxPerce) / 100), 2);
                        txtChrgRupees1.Text = (((subTotal + totalTax + totalChrg - totalDisc) * Convert.ToDouble(txtChargePer.Text)) / 100).ToString();
                    }
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
                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + "", CommonFunctions.ConStr);
                    if (ControlUnder == 4)
                    {
                        if (dgPayChqDetails.Rows.Count > 0)
                        {
                            dgPayChqDetails.Rows[0].Cells[4].Value = txtGrandTotal.Text;
                        }
                    }
                    else if (ControlUnder == 5)
                    {
                        if (dgPayCreditCardDetails.Rows.Count > 0)
                        {
                            dgPayCreditCardDetails.Rows[0].Cells[3].Value = txtGrandTotal.Text;
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
            double SubDiscAmt1 = 0;
            SubDiscRs = 0; SubTax1 = 0; SubTax2 = 0; SubTax3 = 0;
            double SubAmount = 0;
            double TaxAmt1 = 0, TaxAmt2 = 0, tRate = 0, ttax = 0, TaxAmt3 = 0;

            SubQty = Convert.ToDouble((txtBQuantity.Text == "") ? "1" : txtBQuantity.Text);
            // dgBill.CurrentRow.Cells[ColIndex.Rate].Value = 
            SubRate = Convert.ToDouble((txtBRate.Text == "") ? "0" : txtBRate.Text);
            // dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = 
            SubDiscPer = Convert.ToDouble((txtBDiscPer1.Text == "") ? "0" : txtBDiscPer1.Text);
            // dgBill.CurrentRow.Cells[ColIndex.DiscRupees].Value =
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
                // tRate = (tAmount / SubQty);
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
                // ACessPer = (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.CessPer].Value));
                // ADhekhrekper = (Convert.ToDouble(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.DhekharekPer].Value));
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
            // dtPAKKABILL.Clear();
            //foreach (DataGridViewColumn col in dgBill.Columns)
            //{
            //    dtPAKKABILL.Columns.Add(col.Name);
            //}
            // int k = -1;
            for (int i = 0; i < dgBill.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.ESFlag].Value.ToString()) == false)
                {
                    //  dtPAKKABILL.Rows.Add();
                    // k = k + 1;

                    //for (int j = 0; j < dtPAKKABILL.Columns.Count; j++)
                    //{
                    //    try
                    //    {
                    //        dtPAKKABILL.Rows[k][j] = dgBill.Rows[i].Cells[j].Value;
                    //    }
                    //    catch
                    //    {
                    //    }
                    //}
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == false)
                        PBillTotal = PBillTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.SGSTAmount].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.CGSTAmount].Value) + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.IGSTAmount].Value);
                    else
                        PBillTotal = PBillTotal + Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Amount].Value);
                }


            }
            PBillTotal = Convert.ToInt32(PBillTotal);

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
                            // dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
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

        public void ReverseStock()
        {
            try
            {
                for (int i = 0; i < dtTempStock.Rows.Count; i++)
                {
                    if (Convert.ToInt64(dtTempStock.Rows[i].ItemArray[48].ToString()) == DBGetVal.FirmNo)//Convert.ToInt64(dtCompRatio.Rows[j].ItemArray[0].ToString())
                    {
                        #region for Item Product stock Plus Minus
                        mRatesetting = new MRateSetting();
                        mRatesetting.ItemNo = Convert.ToInt64(dtTempStock.Rows[i].ItemArray[20].ToString());
                        mRatesetting.PkSrNo = Convert.ToInt64(dtTempStock.Rows[i].ItemArray[22].ToString());
                        mRatesetting.MRP = Convert.ToDouble(dtTempStock.Rows[i].ItemArray[23].ToString());
                        mRatesetting.StockConversion = Convert.ToDouble(dtTempStock.Rows[i].ItemArray[24].ToString());
                        mRatesetting.UOMNo = Convert.ToInt64(dtTempStock.Rows[i].ItemArray[21].ToString());
                        mRatesetting.ASaleRate = -(Convert.ToDouble(dtTempStock.Rows[i].ItemArray[24].ToString()) * Convert.ToDouble(dtTempStock.Rows[i].ItemArray[4].ToString()));
                        dbTVoucherEntry.UpdateMRateSettingStock(mRatesetting);

                        #endregion

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
                if (PartyNo == 0)
                {
                    OMMessageBox.Show("Please Select Valid Party Name ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    txtParty.Focus();
                    return;
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true && ObjFunction.GetListValue(lstPaymentType) == 3 && ID != 0)
                {
                    if (Utilities.PasswordAsk.CreditID == 1)
                    {
                        if (Convert.ToDouble(txtGrandTotal.Text) < ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where PkVoucherNo=" + ID + "", CommonFunctions.ConStr))
                        {
                            partialPayAdjust = new Vouchers.PartialPaymentAdjust(ID, Convert.ToDouble(txtGrandTotal.Text.Trim()), ObjFunction.GetListValue(lstPaymentType), ObjFunction.GetListValue(lstPartyEnglish));
                            ObjFunction.OpenForm(partialPayAdjust);

                            partialPayAdjust.Left = 15;
                            if (((PartialPaymentAdjust)partialPayAdjust).DS == DialogResult.Cancel)
                            {
                                isSavingTransaction = false;
                                return;
                            }
                        }
                    }
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
                        //NewF.ShowDialog();
                    }
                }
                // DeleteValues();//Delete Old Values
                if ((DBGetVal.KachhaFirm == true) && (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)) == true))
                    PakkaFirm();
                dbTVoucherEntry = new DBTVaucherEntry();
                //
                DeleteRefDetails();
                DeleteValues();

                if (ID != 0)
                {
                    ReverseStock();
                    //dbTVoucherEntry.ReverseStock(ID);
                    /// dbTVoucherEntry.DeleteTReward_All(ID);
                }

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
                tVoucherEntry.Narration = "Sales Bill";
                tVoucherEntry.Reference = txtPartyName.Text;
                tVoucherEntry.ChequeNo = 0;
                tVoucherEntry.ClearingDate = dtpBillDate.Value;
                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                tVoucherEntry.BilledAmount = Convert.ToDouble(txtGrandTotal.Text);
                tVoucherEntry.ChallanNo = "";
                tVoucherEntry.Remark = txtRemark.Text.Trim();
                tVoucherEntry.MacNo = DBGetVal.MacNo;
                tVoucherEntry.PayTypeNo = ObjFunction.GetListValue(lstPaymentType);
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
                FillPayType();
                tVoucherDetails = new TVoucherDetails();
                tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                tVoucherDetails.SignCode = 1;
                tVoucherDetails.LedgerNo = PartyNo;//.GetListValue(lstPartyEnglish);
                tVoucherDetails.Debit = Convert.ToDouble(txtGrandTotal.Text);
                tVoucherDetails.Credit = 0;
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
                    //int a = DBGetVal.FirmNo;
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
                        tStock.Hamali = ((dgBill[ColIndex.Hamali, i].Value) == null) ? 0.00 : Convert.ToDouble(dgBill[ColIndex.Hamali, i].Value.ToString());//Priyanka

                        dbTVoucherEntry.AddTStock(tStock);

                        #region for Item Product stock Plus Minus

                        mRatesetting = new MRateSetting();
                        mRatesetting.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                        mRatesetting.PkSrNo = Convert.ToInt64(dgBill[ColIndex.PkRateSettingNo, i].Value.ToString());
                        mRatesetting.MRP = Convert.ToDouble(dgBill[ColIndex.MRP, i].Value.ToString());
                        mRatesetting.StockConversion = Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString());
                        mRatesetting.UOMNo = Convert.ToInt64(dgBill[ColIndex.UOMNo, i].Value.ToString());
                        mRatesetting.ASaleRate = Convert.ToDouble(dgBill[ColIndex.StockFactor, i].Value.ToString()) * Convert.ToDouble(dgBill[ColIndex.Quantity, i].Value.ToString());
                        mRatesetting.ESFlag = Convert.ToBoolean(dgBill[ColIndex.ESFlag, i].Value.ToString());


                        dbTVoucherEntry.UpdateMRateSettingStock(mRatesetting);
                        #endregion


                        if ((dgBill[ColIndex.IsRateChange, i].Value != null && dgBill[ColIndex.IsRateChange, i].Value.ToString() == "1") || (Convert.ToDouble(dgBill[ColIndex.DiscPercentage, i].Value.ToString()) > 0.00))
                        {
                            if (HistoryMaintain == true && Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_IsRateHistoryMaintain)) == 1)
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
                #region //for=========================KachhaFirm sales Ledger
                if (DBGetVal.KachhaFirm == true)
                {

                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = 24;//.GetListValue(lstPartyEnglish);//Sales Account
                    if (Convert.ToDouble(txtRoundOff.Text) >= 0)
                    {

                        tVoucherDetails.Credit = Convert.ToDouble(txtGrandTotal.Text) - Convert.ToDouble(txtChrgRupees1.Text) - Convert.ToDouble(txtChrgRupees2.Text) - Convert.ToDouble(txtRoundOff.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtRoundOff.Text)) / 10;
                    }
                    else
                    {
                        tVoucherDetails.Credit = Convert.ToDouble(txtGrandTotal.Text) - Convert.ToDouble(txtChrgRupees1.Text) - Convert.ToDouble(txtChrgRupees2.Text) + Convert.ToDouble(txtRoundOff.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Math.Abs(Convert.ToDouble(txtRoundOff.Text))) / 10;

                    }
                    tVoucherDetails.Debit = 0;
                    // tVoucherDetails.Credit = Convert.ToDouble(txtGrandTotal.Text) + Convert.ToDouble(txtRoundOff.Text);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.SrNo = 0;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);


                }

                #endregion
                //}

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
                    //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                    //{
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount2));
                    tVoucherDetails.Debit = Convert.ToDouble(txtSchemeDisc.Text);//(Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtSchemeDisc.Text)) / 10;
                    tVoucherDetails.Credit = 0;
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
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                    tVoucherDetails.Debit = Convert.ToDouble(txtDiscRupees1.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtDiscRupees1.Text)) / 10;
                    tVoucherDetails.Credit = 0;
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
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount3));
                    tVoucherDetails.Debit = Convert.ToDouble(txtOtherDisc.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtOtherDisc.Text)) / 10;
                    tVoucherDetails.Credit = 0;
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
                if (Convert.ToDouble(txtChrgRupees1.Text) != 0)
                {
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1));
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(txtChrgRupees1.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
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
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges2));
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(txtChrgRupees2.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
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
                    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc));
                    if (Convert.ToDouble(txtRoundOff.Text) >= 0)
                    {
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.Debit = 0;
                        tVoucherDetails.Credit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtRoundOff.Text)) / 10;
                    }
                    else
                    {
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.Debit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Math.Abs(Convert.ToDouble(txtRoundOff.Text))) / 10;
                        tVoucherDetails.Credit = 0;
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
                    //
                    DeleteValues();//===umesh transfer
                }
                //   dbTVoucherEntry.EffectStock();

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true && ObjFunction.GetListValue(lstPaymentType) == 3 && ID != 0)
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
                if ((DBGetVal.KachhaFirm == true && PBillTotal != 0) && (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)) == true))
                {
                    double Troundoff = 0, TRsalesamt = 0, Rsalesamt = 0, Rtaxamt = 0, Rtaxamt2 = 0;
                    // PID = ObjQry.ReturnLong("SELECT PKREFNO FROM TVoucherEntry WHERE PKVOUCHERNO="+ ID +" AND VOUCHERTYPECODE=115",CommonFunctions.ConStr);
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = PID;

                    tVoucherEntry.VoucherTypeCode = 20;
                    tVoucherEntry.VoucherUserNo = VoucherUserNo;
                    tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                    tVoucherEntry.VoucherTime = dtpBillTime.Value;
                    tVoucherEntry.Narration = "Sales Bill";
                    tVoucherEntry.Reference = txtPartyName.Text;
                    tVoucherEntry.ChequeNo = 0;
                    tVoucherEntry.ClearingDate = dtpBillDate.Value;
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = Convert.ToDouble(PBillTotal);
                    tVoucherEntry.ChallanNo = "";
                    tVoucherEntry.Remark = txtRemark.Text.Trim();
                    tVoucherEntry.MacNo = DBGetVal.MacNo;
                    tVoucherEntry.PayTypeNo = 2;// ObjFunction.GetListValue(lstPaymentType);
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
                    dbTVoucherEntry.AddTVoucherEntryES(tVoucherEntry);

                    VoucherSrNo = 1;
                    dtVoucherDetails = new DataTable();

                    if (PID != 0)
                    {
                        dtVchPrev = new DataTable();
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + PID + " order by VoucherSrNo").Table;
                        if (PayType != 3)
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + PID + " and srno!=508  order by VoucherSrNo").Table;
                        else
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,0.00 AS Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + PID + " and srno!=508  order by VoucherSrNo").Table;
                    }

                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = PartyNo;//.GetListValue(lstPartyEnglish);
                    tVoucherDetails.Debit = Convert.ToDouble(PBillTotal);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.SrNo = Others.Party;
                    dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                    dtVoucherDetails = new DataTable();
                    //dtVchPrev = new DataTable();
                    //if (PID != 0)
                    //{
                    //    dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + PID + " order by VoucherSrNo").Table;
                    //    if (PayType != 3)
                    //        dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + PID + " order by VoucherSrNo").Table;
                    //    else
                    //        dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,0.00 AS Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + PID + " order by VoucherSrNo").Table;
                    //}


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
                                    //  tStock.PkStockTrnNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].Value);
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
                                //#region For LBT
                                //tStock.LBTPerce = 0;
                                //tStock.LBTApplicableAmount = 0;
                                //tStock.LBTAmount = 0;
                                //#endregion


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
                                //tStock.DisplayItemName = dgBill[ColIndex.DisplayName, i].EditedFormattedValue.ToString();
                                tStock.TRWeight = 0;
                                tStock.GRWeight = 0;
                                tStock.Remarks = "";
                                tStock.Freight = 0;
                                tStock.OtherCharges = 0;

                                tStock.SalesMan = ((dgBill[ColIndex.SalesMan, i].Value) == null) ? 0 : Convert.ToInt32(dgBill[ColIndex.SalesMan, i].Value.ToString());
                                tStock.IType = ((dgBill[ColIndex.ESFlag, i].Value) == null) ? true : Convert.ToBoolean(dgBill[ColIndex.ESFlag, i].Value.ToString());
                                dbTVoucherEntry.AddTStockES(tStock);
                            }
                        }
                    }
                    #region //for Sales bill tax entry
                    //if (DBGetVal.KachhaFirm == false)
                    //{
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
                            if (Convert.ToBoolean(dgBill.Rows[k].Cells[ColIndex.ESFlag].Value) == false && Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.SalesLedgerNo].Value) != Convert.ToInt64(dtSaleLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtSaleLedger.Rows[i].ItemArray[1].ToString()))
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
                            if (Convert.ToBoolean(dgBill.Rows[j].Cells[ColIndex.ESFlag].Value) == false && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.SalesLedgerNo].Value) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtSaleLedger.Rows[k].ItemArray[1].ToString()))
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
                            dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                            Rsalesamt = debit;
                            debit = 0;

                        }
                        TRsalesamt += Rsalesamt;
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
                            if (Convert.ToBoolean(dgBill.Rows[k].Cells[ColIndex.ESFlag].Value) == false && Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[1].ToString()))
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
                            if (Convert.ToBoolean(dgBill.Rows[j].Cells[ColIndex.ESFlag].Value) == false && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.TaxLedgerNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[1].ToString()))
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
                            dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                            Rtaxamt += debit;
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
                            if (Convert.ToBoolean(dgBill.Rows[k].Cells[ColIndex.ESFlag].Value) == false && Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.TaxLedgerNo2].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[0].ToString()) || Convert.ToInt64(dgBill.Rows[k].Cells[ColIndex.StockCompanyNo].Value) != Convert.ToInt64(dtTAxLedger.Rows[i].ItemArray[1].ToString()))
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
                            if (Convert.ToBoolean(dgBill.Rows[j].Cells[ColIndex.ESFlag].Value) == false && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.TaxLedgerNo2].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[0].ToString()) && Convert.ToInt64(dgBill.Rows[j].Cells[ColIndex.StockCompanyNo].Value) == Convert.ToInt64(dtTAxLedger.Rows[k].ItemArray[1].ToString()))
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
                            dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                            Rtaxamt2 += debit;
                            debit = 0;
                        }
                    }

                    #endregion

                    //}
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
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount2));
                        tVoucherDetails.Debit = Convert.ToDouble(txtSchemeDisc.Text);//(Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtSchemeDisc.Text)) / 10;
                        tVoucherDetails.Credit = 0;
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
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount1));
                        tVoucherDetails.Debit = Convert.ToDouble(txtDiscRupees1.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtDiscRupees1.Text)) / 10;
                        tVoucherDetails.Credit = 0;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Discount1;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                        dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                        //}
                    }
                    Troundoff = PBillTotal - (TRsalesamt + Rtaxamt2 + Rtaxamt);
                    //For Footer Other  disc
                    if (Convert.ToDouble(txtOtherDisc.Text) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Discount3));
                        tVoucherDetails.Debit = Convert.ToDouble(txtOtherDisc.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtOtherDisc.Text)) / 10;
                        tVoucherDetails.Credit = 0;
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
                    ////For Charges Rupees 1
                    //if (Convert.ToDouble(txtChrgRupees1.Text) != 0)
                    //{
                    //    //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                    //    //{
                    //    tVoucherDetails = new TVoucherDetails();
                    //    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    //    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    //    tVoucherDetails.SignCode = 2;
                    //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges1));
                    //    tVoucherDetails.Debit = 0;
                    //    tVoucherDetails.Credit = Convert.ToDouble(txtChrgRupees1.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
                    //    tVoucherDetails.Narration = "";
                    //    tVoucherDetails.SrNo = Others.Charges1;
                    //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    //    dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                    //    //}
                    //}
                    ////For Charges Rupees 2
                    //if (Convert.ToDouble(txtChrgRupees2.Text) != 0)
                    //{
                    //    //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                    //    //{
                    //    tVoucherDetails = new TVoucherDetails();
                    //    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    //    tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    //    tVoucherDetails.SignCode = 2;
                    //    tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_Charges2));
                    //    tVoucherDetails.Debit = 0;
                    //    tVoucherDetails.Credit = Convert.ToDouble(txtChrgRupees2.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtChrgRupees1.Text)) / 10;
                    //    tVoucherDetails.Narration = "";
                    //    tVoucherDetails.SrNo = Others.Charges2;
                    //    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;// Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString());
                    //    dbTVoucherEntry.AddTVoucherDetailsES(tVoucherDetails);
                    //    //}
                    //}


                    //For Round Off Acc Ledger
                    if (Math.Round((Troundoff), 2) != 0)
                    {
                        //for (int i = 0; i < dtCompRatio.Rows.Count; i++)
                        //{
                        tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.VoucherSrNo = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                        tVoucherDetails.LedgerNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc));
                        if (Convert.ToDouble(txtRoundOff.Text) >= 0)
                        {
                            tVoucherDetails.SignCode = 2;
                            tVoucherDetails.Debit = 0;
                            tVoucherDetails.Credit = Convert.ToDouble(txtRoundOff.Text);// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Convert.ToDouble(txtRoundOff.Text)) / 10;
                        }
                        else
                        {
                            tVoucherDetails.SignCode = 1;
                            tVoucherDetails.Debit = Math.Abs(Convert.ToDouble(txtRoundOff.Text));// (Convert.ToDouble(dtCompRatio.Rows[i].ItemArray[1].ToString()) * Math.Abs(Convert.ToDouble(txtRoundOff.Text))) / 10;
                            tVoucherDetails.Credit = 0;
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

                if (IsDeliveryAddress == true)
                {
                    tdeliveryaddress = new TDeliveryAddress();
                    tdeliveryaddress.PkSrNo = 0;
                    //     tdeliveryaddress.FkVoucherno = VoucherSrNo; VoucherSrNo += 1; ObjFunction.SetVouchers(dtVoucherDetails, tVoucherDetails.PkVoucherTrnNo);
                    tdeliveryaddress.Ledgerno = PartyNo;
                    tdeliveryaddress.LedgerDetailsNo = LedgerDetailsNo;
                    tdeliveryaddress.UserId = DBGetVal.UserID;
                    tdeliveryaddress.UserDate = DBGetVal.ServerTime.Date;
                    //tdeliveryaddress.Narration = "";
                    // tdeliveryaddress.SrNo = Others.Charges2;
                    tdeliveryaddress.CompanyNo = DBGetVal.FirmNo;
                    dbTVoucherEntry.AddTDeliveryAddress(tdeliveryaddress);

                }
                long tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                if (tempID != 0)
                {

                    FooterDiscDtlsNo = 0;

                    //S_CashPosting
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_CashPosting)) == true)
                        
                    //{
                    //    if (Convert.ToDouble(dgPayType.Rows[1].Cells[2].Value) > 0 || Convert.ToDouble(dgPayType.Rows[3].Cells[2].Value) > 0 || Convert.ToDouble(dgPayType.Rows[4].Cells[2].Value) > 0)
                    //        if (DBGetVal.KachhaFirm == false)
                    //        {
                    //            if (Convert.ToDouble(dgPayType.Rows[2].Cells[2].Value) == 0)
                    //            //if(tVoucherEntry.PayTypeNo==2)
                    //            {
                    //                if (tempDate.Date == dtpBillDate.Value.Date && tempPartyNo == ObjFunction.GetListValue(lstPartyEnglish))
                    //                    SaveReceipt(tempID);
                    //                else if (tempDate.Date != dtpBillDate.Value.Date || tempPartyNo != ObjFunction.GetListValue(lstPartyEnglish))
                    //                {
                    //                    SaveReceiptNew(tempID);
                    //                    //  SaveReceiptOLD(tempDate, tempPartyNo);
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            if (Convert.ToDouble(dgPayType.Rows[2].Cells[2].Value) == 0)
                    //            {
                    //                if (tempDate.Date == dtpBillDate.Value.Date && tempPartyNo == ObjFunction.GetListValue(lstPartyEnglish))
                    //                    SaveReceiptES(tempID);
                    //                else if (tempDate.Date != dtpBillDate.Value.Date || tempPartyNo != ObjFunction.GetListValue(lstPartyEnglish))
                    //                {
                    //                    SaveReceiptESNew(tempID);
                    //                }
                    //            }

                        //  }
                        //pnlBillSize = false;
                    //}
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
                            //if (MessageBox.Show("Want to print this bill?", CommonFunctions.ErrorTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
                        else PrintBill(0);
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

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAutoEmailSend)) == true)
                    {
                        string ledgeremail = ObjQry.ReturnString("Select EmailID From MLedgerDetails Where LedgerNo=" + lstPartyEnglish.SelectedValue + "", CommonFunctions.ConStr).ToString();
                        if (ledgeremail == "")
                        {
                            txtToEmailid.Enabled = true;
                            txtToEmailid.Focus();
                            pnlEmail.Visible = true;

                        }
                        else
                        {
                            txtToEmailid.Text = ledgeremail;
                            StartWorker();
                        }

                    }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAutoSMSSend)) == true)
                    {

                        string MobileNo = ObjQry.ReturnString("Select MobileNo1 From MLedgerDetails Where LedgerNo=" + lstPartyEnglish.SelectedValue + "", CommonFunctions.ConStr).ToString();
                        if (MobileNo == "")
                        {
                            txtSendNumber.Enabled = true;
                            pnlPhone.Visible = true;
                        }
                        else
                        {
                            txtSendNumber.Text = MobileNo;
                            StartWorkerSMS();
                        }

                    }
                    FillField();
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    rbEnglish.Enabled = true;
                    rbMarathi.Enabled = true;
                    btnCashSave.Visible = false;
                    btnCreditSave.Visible = false;
                    //btnCCSave.Visible = false;
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
        public void StartWorkerSMS()
        {
            Thread worker = new Thread(SendSMS);
            worker.IsBackground = true;
            worker.SetApartmentState(System.Threading.ApartmentState.STA);
            worker.Start();
        }

        public void StartWorker()
        {
            Thread worker = new Thread(PrintBillEmail);
            worker.IsBackground = true;
            worker.SetApartmentState(System.Threading.ApartmentState.STA);
            worker.Start();
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
            else { pnlSalesMan.Visible = true;
                lstSalesMan.Focus();
            }
            // if(Keys.Enter==)
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

            }else if(e.KeyCode==Keys.Escape)
            {
                pnlSalesMan.Visible = false;
                txtSalesMan.Focus();
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

        private void btnEwayBill_Click(object sender, EventArgs e)
        {
            Form frmChild = new Master.EwayBillAE(ID, Convert.ToInt32(txtInvNo.Text), dtTempStock, PartyNo, txtParty.Text, dtpBillDate.Value, Convert.ToDouble(lblGrandTotal.Text));
            ObjFunction.OpenForm(frmChild);
        }

        private void btnEwayBill_Click_1(object sender, EventArgs e)
        {
            Form frmChild = new Master.EwayBillAE(ID, Convert.ToInt32(txtInvNo.Text), dtTempStock, PartyNo, txtParty.Text, dtpBillDate.Value, Convert.ToDouble(lblGrandTotal.Text));
            ObjFunction.OpenForm(frmChild);

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

        //private void btnDcPrint_Click(object sender, EventArgs e)
        //{
        //    if (ID != 0)
        //    {
        //        DialogResult ds = OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
        //        if (ds == DialogResult.Yes)
        //            DCPrintBill(0);
        //        else if (ds == DialogResult.Cancel)
        //            DCPrintBill(1);
        //    }
        //}

        private void btnDcPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (PrintAsk == 0)
                {
                    //DialogResult ds = OMMessageBox.Show("Are you sure to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                    //if (ds == DialogResult.Yes)
                    //{
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
                    //}
                    //else if (ds == DialogResult.Cancel)
                    //    DCPrintBill(1);
                }
                else
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
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType..DeliveryChallan : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : lstPaymentType.Text);
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

        private void btnAutoPost_Click(object sender, EventArgs e)
        {
            // if estimate bill transfer to sales bill
            //DataTable dt = ObjFunction.GetDataView("select Pkvoucherno from tvoucherentry where pkvoucherno in (select fkvoucherno from tstock where itype=0) and vouchertypecode=115 order by pkvoucherno asc ,voucherdate ", CommonFunctions.ConStr).Table;
            //for(int i=0;i<=dt.Rows.Count-1;i++)
            //{
            //    //break;
            //    ID = Convert.ToInt32(dt.Rows[i][0].ToString());
            //    FillField();
            //    btnUpdate_Click(sender, e);
            //    btnSave_Click(sender, e);

            //}
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
                        // SetNavigation();
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
                        //cmbPartyName.SelectedIndex = 0;
                        DisplayMessage("Bill Not Found");
                        txtSearch.Focus();
                        //SearchVisible(false);
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
                //cmbPartyNameSearch.SelectedIndex = 0;
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
                        //pnlPartySearch.SetBounds(x, y, dgPartySearch.Width + 10, dgPartySearch.Height + 10);
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

        private void btnKgPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (PrintAsk == 0)
                {
                   // DialogResult ds = OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                  // if (ds == DialogResult.Yes)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                        {
                            Form NewFF = new Utilities.PrintCount(IsPrintCount);
                            ObjFunction.OpenForm(NewFF);
                            if (Utilities.PrintCount.NoPrintCount != -1)
                            {
                                IsPrintCount = Utilities.PrintCount.NoPrintCount;
                                KgPrint(0);
                            }
                        }
                        else KgPrint(0);
                    }
                   // else if (ds == DialogResult.Cancel)
                  //    KgPrint(1);
                }
                else
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                    {
                        Form NewFF = new Utilities.PrintCount(IsPrintCount);
                        ObjFunction.OpenForm(NewFF);
                        if (Utilities.PrintCount.NoPrintCount != -1)
                        {
                            IsPrintCount = Utilities.PrintCount.NoPrintCount;
                            KgPrint(0);
                        }
                    }
                    else KgPrint(0);

                }
            }
        }

        public void KgPrint(int PrintType)
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
                ReportSession[0] = ID.ToString();
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType..DeliveryChallan : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : lstPaymentType.Text);
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
                                    childForm = ObjFunction.LoadReportObject("DCGetBill-A4.rpt", CommonFunctions.ReportPath);
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
                                    childForm = ObjFunction.LoadReportObject("GetDCKgPrintMar.rpt", CommonFunctions.ReportPath);
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
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetDCKgPrintMar.rpt", CommonFunctions.ReportPath), ReportSession);
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

        private void btnBox_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                if (PrintAsk == 0)
                {
                    //DialogResult ds = OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                    //if (ds == DialogResult.Yes)
                    //{
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                        {
                            Form NewFF = new Utilities.PrintCount(IsPrintCount);
                            ObjFunction.OpenForm(NewFF);
                            if (Utilities.PrintCount.NoPrintCount != -1)
                            {
                                IsPrintCount = Utilities.PrintCount.NoPrintCount;
                                BoxPrint(0);
                            }
                        }
                        else BoxPrint(0);
                    //}
                    //else if (ds == DialogResult.Cancel)
                    //    BoxPrint(1);
                }
                else
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                    {
                        Form NewFF = new Utilities.PrintCount(IsPrintCount);
                        ObjFunction.OpenForm(NewFF);
                        if (Utilities.PrintCount.NoPrintCount != -1)
                        {
                            IsPrintCount = Utilities.PrintCount.NoPrintCount;
                            BoxPrint(0);
                        }
                    }
                    else BoxPrint(0);
                }
            }
        }

        public void BoxPrint(int PrintType)
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
                ReportSession[0] = ID.ToString();
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : lstPaymentType.Text);
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
                                    childForm = ObjFunction.LoadReportObject("DCGetBill-A4.rpt", CommonFunctions.ReportPath);
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
                                    childForm = ObjFunction.LoadReportObject("GetDCBoxPrintMar.rpt", CommonFunctions.ReportPath);
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
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetDCBoxPrintMar.rpt", CommonFunctions.ReportPath), ReportSession);
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

        private void btnBigPrint_Click(object sender, EventArgs e)
        {
            if (PrintAsk == 0)
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
            else
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
                if (DBGetVal.KachhaFirm == true)//for option billing big print 
                {
                    ReportSession[0] = PID.ToString();
                }
                else
                {
                    ReportSession[0] = ID.ToString();
                }
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : lstPaymentType.Text);
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
                                //GetBillMarPakka-A5.rpt
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBill-a4.rpt", CommonFunctions.ReportPath), ReportSession);


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

                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillMarPakka-A5.rpt", CommonFunctions.ReportPath), ReportSession);


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
                    lstPaymentType.SelectedValue = "2";
                    lstPaymentType_KeyDown(Keys.Enter, e);
                    btnSave_Click(sender, e);
                }
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (btnCreditSave.Visible == true && btnCreditSave.Enabled == true)
                {
                    btnCreditSave_Click(sender, new EventArgs());
                }
                else
                {
                    OMMessageBox.Show("Please Select Valid Customer..");
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
                //MessageBox.Show("Are you want to cancel this item....",);
                if ((btnSave.Visible == true) && (dgItemList.Visible == false))
                {
                    //if (OMMessageBox.Show("Are you sure you want to cancel this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    //{
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
            //else if (e.KeyCode == Keys.C && e.Alt)
            //{
            //    if (OMMessageBox.Show("Are you sure you want to cancel this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
            //    {
            //        btnCancel_Click(sender, new EventArgs());
            //    }
            //}
            else if (e.KeyCode == Keys.T && e.Control)
            {
                if (ID != 0)
                {
                    ShowVATNo = true;
                    PrintBill(0);
                    ShowVATNo = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowVatNo));
                }
            }
            else if ((e.KeyCode == Keys.F5) && (pnlSalePurHistory.Visible == false))
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
            //else if (e.KeyCode == Keys.F4)
            //    ValidationsMain();
            else if (e.KeyCode == Keys.O && e.Control)
            {
                if (DBGetVal.IsAdmin == true)
                {
                    if (btnNew.Visible == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_RateTypeAskPassword)) == true)
                        {
                            txtRateTypePassword.Enabled = true;
                            pnlRateTypePassword.Visible = true;
                            txtRateTypePassword.Text = "";
                            txtRateTypePassword.Focus();
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.D && e.Control)
            {
                if (ID != 0 && btnSave.Visible)
                {
                    //long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPaymentType) + "", CommonFunctions.ConStr);
                    //if (ObjFunction.GetComboValue(cmbPaymentType) == 4)
                    if (iPayTypeControlUnder == 4)
                    {
                        pnlPartial.Visible = !pnlPartial.Visible;
                        pnlPartial.Size = new Size(475, 214);
                        pnlPartial.Location = new Point(75, 123);
                        dgPayChqDetails.Location = dgPayType.Location;
                        dgPayChqDetails.Visible = true;
                        dgPayChqDetails.BringToFront();
                        dgPayChqDetails.Focus();
                        //dgPayChqDetails.Enabled = false;
                        dgPayCreditCardDetails.Visible = false;
                    }
                    //else if (ObjFunction.GetComboValue(cmbPaymentType) == 5)
                    else if (iPayTypeControlUnder == 5)
                    {
                        pnlPartial.Visible = !pnlPartial.Visible;
                        pnlPartial.Size = new Size(475, 214);
                        pnlPartial.Location = new Point(75, 123);
                        dgPayCreditCardDetails.Location = dgPayType.Location;
                        dgPayCreditCardDetails.Visible = true;
                        ((DataGridViewTextBoxColumn)dgPayCreditCardDetails.Columns[0]).MaxInputLength = Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_CreditCardDigitLimit));
                        dgPayCreditCardDetails.Focus();
                        dgPayCreditCardDetails.BringToFront();
                        //dgPayCreditCardDetails.Enabled = false;
                        dgPayChqDetails.Visible = false;

                    }
                }

            }
            else if (e.KeyCode == Keys.M && e.Control)
            {
                //if (btnInsScheme.Enabled == true && btnInsScheme.Visible == true)
                //{
                //    btnInsScheme_Click(new object(), new EventArgs());
                //}
            }
            else if (e.Alt && e.KeyCode == Keys.F2)
            {
                if (btnNew.Visible == false)
                {
                    //  if (btnAdvanceSearch.Enabled) btnAdvanceSearch_Click(sender, e);
                }
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                if (btnNew.Visible == false)
                {
                    // ChangeRateRealtime();
                }
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                if (btnNew.Visible == false)
                {
                    // ChangeRateRealtime();
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

        public void FillTodaysSalesDetails()
        {
            try
            {

                DataTable dtPayType = ObjFunction.GetDataView("Exec GetPayTypeDetails '" + DBGetVal.ServerTime.ToString("dd-MMM-yyyy") + "','" + DBGetVal.ServerTime.ToString("dd-MMM-yyyy") + "'," + VchType.DeliveryChallan + ",0," + DBGetVal.FirmNo + "").Table;
                dgSalesDetails.DataSource = dtPayType.DefaultView;
                dgSalesDetails.ColumnHeadersVisible = false;
                dgSalesDetails.Columns[0].Visible = false;
                dgSalesDetails.Columns[1].Visible = false;
                dgSalesDetails.Columns[2].Width = 100;
                dgSalesDetails.Columns[3].Visible = false;
                dgSalesDetails.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgSalesDetails.Columns[4].Width = 112;
                dgSalesDetails.Columns[5].Visible = false;
                dgSalesDetails.RowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 210);
                dgSalesDetails.BackgroundColor = Color.FromArgb(255, 255, 210);
                dgSalesDetails.Height = 107;
                lblTotAmt.Text = dtPayType.Compute("Sum(Amount)", "").ToString();
                lblBillCnt.Text = ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VoucherDAte='" + DBGetVal.ServerTime.ToString("dd-MMM-yyyy") + "' AND VoucherTypeCode=" + VchType.DeliveryChallan + " AND IsCancel='false'", CommonFunctions.ConStr).ToString();// dtPayType.Compute("Sum(NoOfBills)", "").ToString();

                lblTodaysSales.Font = new Font("Verdana", 10, FontStyle.Bold);
                lblTotAmt.Font = new Font("Verdana", 8, FontStyle.Bold);
                lblBillCnt.Font = new Font("Verdana", 8, FontStyle.Bold);
                if (lblCash.Text == "") lblCash.Text = "0.00";
                if (lblCheque.Text == "") lblCheque.Text = "0.00";
                if (lblCredit.Text == "") lblCredit.Text = "0.00";
                if (lblCc.Text == "") lblCc.Text = "0.00";
                if (lblFV.Text == "") lblFV.Text = "0.00";
                if (lblTotAmt.Text == "") lblTotAmt.Text = "0.00";
                if (lblBillCnt.Text == "") lblBillCnt.Text = "0.00";
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
                pnlTodaysSales.Location = new Point(dgBill.Height + 10, pnlTodaysSales.Location.Y);
                pnlTodaysSales.Location = new Point(dgBill.Location.X, dgBill.Location.Y + dgBill.Height + 10);//pnlTodaysSales.Location.Y+dgBill.Height + 10
                pnlTodaysSales.Visible = true;

                pnlCollectionDetails.Location = new Point(dgBill.Location.X + pnlTodaysSales.Width + 5, dgBill.Location.Y + dgBill.Height + 10);//pnlTodaysSales.Location.Y+dgBill.Height + 10
                pnlCollectionDetails.Visible = true;
                BillSizeFlag = true;

            }
            else
            {
                BillSizeFlag = false;
                pnlTotalAmt.Visible = false;
                pnlTodaysSales.Visible = false;
                pnlCollectionDetails.Visible = false;
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
                            btnSearch.Visible = false;
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillUpdate)) == false)
                        {
                            Form frm = new Utilities.PasswordAsk(2);
                            ObjFunction.OpenForm(frm);
                            if (Utilities.PasswordAsk.IsAllow == 0) return;
                        }
                        else
                        {
                            btnCashSave.Visible = true;
                            if ((Convert.ToInt32(lstPartyEnglish.SelectedValue) != Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_DefaultPartyAC))) && (PartyNo != 21))
                            { btnCreditSave.Visible = true; }
                            else
                            { btnCreditSave.Visible = false; }
                        }
                    }
                }
                txtInvNo.Enabled = false;
                txtSchemeDisc.Enabled = false;
                txtOtherDisc.Enabled = false;
                txtChrgRupees2.Enabled = false;

                btnBillCancel.Enabled = false;
                btnBillCancel.Visible = false;
                btnMixMode.Visible = false;
                btnSearch.Visible = false;
                btnPrint.Visible = false;
                btnDeliveryAddress.Visible = true;

                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                // if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_RateTypeAskPassword)) == true)

                dgBill.Enabled = true;
                dgBill.Focus();
                dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCreditBillUpdate)) == true)
                {
                    if (ObjQry.ReturnInteger("Select Count(*) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=" + ID + ")", CommonFunctions.ConStr) > 1)
                    {
                        // if (ObjFunction.GetComboValue(cmbPaymentType) == 3) cmbPaymentType.Enabled = false;
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
                dtPayLedger = ObjFunction.GetDataView("Select * From MPayTypeLedger").Table;
                string sqlQuery = "";
                if (ID == 0)
                    sqlQuery = "SELECT PayTypeName, PKPayTypeNo, Cast(0.00 as varchar) AS Amount, 0 AS LedgerNo, 0 AS PKVoucherPayTypeNo, ControlUnder,IsNull((Select ChargesPerce FRom MPayTypeLedger Where PayTypeNo=PKPayTypeNo),0) As ChrgPerce,0.00 As ChrgAmt FROM MPayType ORDER BY PKPayTypeNo";
                else
                    //sqlQuery = "SELECT PayTypeName, PKPayTypeNo,Cast( IsNull((SELECT SUM(Amount) FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) AS varchar) AS Amount, 0 AS LedgerNo, 0 AS PKVoucherPayTypeNo,ControlUnder,IsNull((SELECT ChargesPerce FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) As ChrgPerce,IsNull((SELECT ChargesAmount FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) As ChrgAmt FROM MPayType ORDER BY PKPayTypeNo";

                    sqlQuery = "Select MPayType.PayTypeName, MPayType.PKPayTypeNo,TTaBle.Amount,0 AS LedgerNo, 0 AS PKVoucherPayTypeNo,ControlUnder,Case When(TTaBle.ChrgPerce<>0)Then TTaBle.ChrgPerce Else IsNull((Select ChargesPerce From MPayTypeLedger Where PayTypeNo=MPayType.PKPayTypeNo),0) End AS ChrgPerce,TTaBle.ChrgAmt From( " +
                        " SELECT PKPayTypeNo,Cast(IsNull((SELECT SUM(Amount) FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) AS varchar) AS Amount, IsNull((SELECT ChargesPerce FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) As ChrgPerce,IsNull((SELECT ChargesAmount FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ") AND (FKPayTypeNo = PKPayTypeNo)),0) As ChrgAmt FROM MPayType " +
                   " ) TTable INNER JOIN MPayType ON TTaBle.PkPayTypeNo=MPayType.PkPayTypeNo ";
                //sqlQuery = "SELECT PT.PayTypeName, PT.PKPayTypeNo, ISNULL(Sum(TD.Amount), 0) AS Amount, PT.LedgerNo, " +
                //    " 0 AS PKVoucherPayTypeNo FROM MPayType AS PT LEFT OUTER JOIN (SELECT PKVoucherPayTypeNo, FKSalesVoucherNo, FKReceiptVoucherNo, FKVoucherTrnNo, FKPayTypeNo, Amount " +
                //    " FROM TVoucherPayTypeDetails WHERE (FKSalesVoucherNo = " + ID + ")) AS TD ON PT.PKPayTypeNo = TD.FKPayTypeNo " +
                //    " GROUP BY PT.PayTypeName, PT.PKPayTypeNo, ISNULL(TD.Amount, 0), PT.LedgerNo ORDER BY PT.PKPayTypeNo";


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
                    //long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + ObjFunction.GetComboValue(cmbPaymentType) + "", CommonFunctions.ConStr);
                    //if (ObjFunction.GetComboValue(cmbPaymentType) == 1 || ObjFunction.GetComboValue(cmbPaymentType) == 4 || ObjFunction.GetComboValue(cmbPaymentType) == 5)
                    if (iPayTypeControlUnder == 1 || iPayTypeControlUnder == 4 || iPayTypeControlUnder == 5)
                    {
                        for (int i = 0; i < dgPayType.Rows.Count; i++)
                        {
                            if (dgPayType.Rows[i].Cells[1].Value.ToString() == ObjFunction.GetListValue(lstPaymentType).ToString())
                            {
                                if (iPayTypeControlUnder == 4)
                                    dgPayType.Rows[i].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND ChequeNo <>''", CommonFunctions.ConStr).ToString("0.00");
                                else if (iPayTypeControlUnder == 5)
                                    dgPayType.Rows[i].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND CreditCardNo <>''", CommonFunctions.ConStr).ToString("0.00");
                            }
                        }
                        //dgPayType.Rows[3].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND ChequeNo <>''", CommonFunctions.ConStr).ToString("0.00");
                        //dgPayType.Rows[4].Cells[2].Value = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherChqCreditDetails Where FKVoucherNo=" + ID + "  AND CreditCardNo <>''", CommonFunctions.ConStr).ToString("0.00");
                    }

                    double RefAmt = ObjQry.ReturnDouble("Select Sum(Amount) From TVoucherRefDetails Where FKVucherTrnNo in (Select PKVoucherTrnNo From TVoucherDetails Where FkVoucherNo=" + ID + ")", CommonFunctions.ConStr);
                    if (RefAmt > 0)
                    {
                        dgPayType.Rows[2].Cells[2].Value = RefAmt;
                    }
                }
                //if (ObjFunction.GetComboValue(cmbPaymentType) == 3)
                //{
                //    //dgPayType.Rows[2].Cells[2].Value = txtGrandTotal.Text;
                //}
                //CaluculatePayType();
                pnlPartial.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
                if (cntflag > 1 && ObjFunction.GetListValue(lstPaymentType) != 3) lstPaymentType.SelectedValue = "1";

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
                            dgPayType.Rows[i].Cells[2].Value = txtGrandTotal.Text;

                            dgPayType.Rows[i].Cells[4 + (1)].Value = Convert.ToDouble(txtGrandTotal.Text);
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
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + 0 + " and srno!=508  order by VoucherSrNo").Table;
                            // setCompanyRatio();
                            FillPayType();
                        }
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                            " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
                        long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);
                        double PrevAmt = 0;
                        for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                            //{if(i==0)
                            //    PrevAmt += Convert.ToDouble(dtVchPrev.Rows[0].ItemArray[1].ToString());
                            //}
                            //double a = PrevAmt;
                            PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
                        PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));

                        // PrevAmt =( (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr)) + a);// +((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));

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
                        tVoucherEntry.BilledAmount = (ReceiptID != 0) ? PrevAmt : Convert.ToDouble(txtGrandTotal.Text);
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
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//error umesh

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

        public bool SaveReceiptNew(long SalesID)
        {
            DataTable dtPayType = new DataTable();
            int cntPayType = 1;

            long tempid = -1, ReceiptID = 0, VoucherUserNo = 0;

            for (int row = 1; row <= dgPayType.Rows.Count - 1; row++) //for (int row = 1; row <= 4; row++)//for (int j = 0; j < dgPayType.Rows.Count; j++)
            {
                if (row == 1 || row == 3 || row == 4 || row > 4)
                {
                    if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                    {
                        if (row == 1 || row == 3 || row == 4 || row > 4)
                            ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                              " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherEntry.VoucherDate ='" + dtpBillDate.Text + "') AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ") AND " +
                                " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + ") ", CommonFunctions.ConStr);
                        //else if (row == 3 || row == 4)
                        //  ReceiptID = ObjQry.ReturnLong("SELECT FKReceiptVoucherNo FROM TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=" + dgPayType.Rows[row].Cells[1].Value + " ", CommonFunctions.ConStr);
                        if (CancelFlag == true)
                        {
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + 0 + " and srno!=508  order by VoucherSrNo").Table;

                            FillPayType();
                        }
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                            " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
                        long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);
                        double PrevAmt = 0;
                        for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                            PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
                        if (ReceiptID != 0)
                        {
                            if (tempDate.Date == dtpBillDate.Value.Date)
                                PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
                            else
                                PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr)) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
                        }
                        else
                            PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr)) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
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
                        tVoucherEntry.BilledAmount = PrevAmt;// Convert.ToDouble(txtGrandTotal.Text);
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
                        tVoucherEntry.LedgerNo = ObjFunction.GetListValue(lstPartyEnglish);
                        tVoucherEntry.UserID = DBGetVal.UserID;
                        tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);// SetVoucherCompany(tVoucherEntry);

                        DataTable dtVoucherDetails = new DataTable();
                        if (ReceiptID != 0)
                        {
                            dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where FkVoucherNo=" + ReceiptID + " order by VoucherSrNo").Table;
                            dtPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo From TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=" + dgPayType.Rows[row].Cells[1].Value + "  order by PKVoucherPayTypeNo").Table;
                        }
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,TD.LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails TD,TVoucherEntry TC Where TC.PKVoucherNo=TD.FKVoucherNo AND TC.PayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + " AND TD.LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " AND  TD.FkVoucherNo=" + ReceiptID + " AND TD.CompanyNo=" + DBGetVal.FirmNo + " order by TD.VoucherSrNo").Table;//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        double totamt = 0, amt = 0;

                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                        {

                            amt = Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                            totamt = totamt + amt;
                        }
                        amt = 0; if (CancelFlag == true) totamt = -totamt;
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        //For Party Ledger
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
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
                                tVoucherDetails.Credit = Newval + ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString())) + totamt);
                            else
                                tVoucherDetails.Credit = totamt;
                        }
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Party;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                        totamt = 0;
                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                            totamt += Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails Where LedgerNo=" + Convert.ToInt64(dtPayLedger.Select("PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + " AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString()) + " AND  FkVoucherNo=" + ReceiptID + " AND CompanyNo=" + DBGetVal.FirmNo + " order by VoucherSrNo").Table;//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        //For PayType Details
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
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
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        if (ID == 0)
                            tVoucherDetails.Debit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) : 0) + Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                        else
                        {
                            if (dtVoucherDetails.Rows.Count > 0)
                                tVoucherDetails.Debit = Newval + ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString())) + totamt);
                            else
                                tVoucherDetails.Debit = totamt;
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

        public bool SaveReceiptOLD(DateTime dt, long PartID)
        {

            long tempid = -1, ReceiptID = 0, VoucherUserNo = 0;

            for (int row = 1; row <= dgPayType.Rows.Count - 1; row++) //for (int row = 1; row <= 4; row++)//for (int j = 0; j < dgPayType.Rows.Count; j++)
            {
                if (row == 1 || row == 3 || row == 4 || row > 4)
                {
                    if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                    {
                        if (row == 1 || row == 3 || row == 4 || row > 4)
                            ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                              " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherEntry.VoucherDate ='" + dt.ToString(Format.DDMMMYYYY) + "') AND (TVoucherDetails.LedgerNo = " + PartID + ") AND " +
                                " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + ") ", CommonFunctions.ConStr);
                        if (CancelFlag == true)
                        {
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + 0 + " and srno!=508 order by VoucherSrNo").Table;

                            FillPayType();
                        }
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                            " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
                        long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);
                        double PrevAmt = 0;
                        for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                            PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
                        PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt);// +((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
                        if (PrevAmt < 0.0) break;

                        dbTVoucherEntry = new DBTVaucherEntry();
                        //Voucher Header Entry
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = ReceiptID;
                        tVoucherEntry.VoucherTypeCode = VchType.Receipt;
                        tVoucherEntry.VoucherUserNo = VoucherUserNo;
                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dt.ToString("dd-MMM-yyyy"));
                        tVoucherEntry.VoucherTime = dtpBillTime.Value;
                        tVoucherEntry.Narration = "Receipt Bill";
                        tVoucherEntry.Reference = "";
                        tVoucherEntry.ChequeNo = 0;
                        tVoucherEntry.ClearingDate = Convert.ToDateTime("01-Jan-1900");
                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                        tVoucherEntry.BilledAmount = PrevAmt;// Convert.ToDouble(txtGrandTotal.Text);
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
                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);

                        DataTable dtVoucherDetails = new DataTable();
                        if (ReceiptID != 0)
                        {
                            dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where FkVoucherNo=" + ReceiptID + " order by VoucherSrNo").Table;
                            //dtPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo From TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=" + dgPayType.Rows[row].Cells[1].Value + "  order by PKVoucherPayTypeNo").Table;
                        }
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,TD.LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails TD,TVoucherEntry TC Where TC.PKVoucherNo=TD.FKVoucherNo AND TC.PayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + " AND TD.LedgerNo=" + PartID + " AND  TD.FkVoucherNo=" + ReceiptID + " AND TD.CompanyNo=" + DBGetVal.FirmNo + " order by TD.VoucherSrNo").Table;//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        double totamt = 0, amt = 0;

                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                        {

                            amt = Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                            totamt = totamt + amt;
                        }
                        amt = 0; if (CancelFlag == true) totamt = -totamt;
                        if (ID != 0 && PartyNo == PartID && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())
                        //}

                        //For Party Ledger
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0)
                            tVoucherDetails.VoucherSrNo = VoucherSrNo;
                        else tVoucherDetails.VoucherSrNo = Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[4].ToString());
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.LedgerNo = PartID;
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
                            {
                                if (PartID != ObjFunction.GetListValue(lstPartyEnglish))
                                    tVoucherDetails.Credit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString())));
                                else
                                    tVoucherDetails.Credit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString()) - amt));
                            }
                            else
                                tVoucherDetails.Credit = totamt;
                        }
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Party;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                        totamt = 0;
                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                            totamt += Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails Where LedgerNo=" + Convert.ToInt64(dtPayLedger.Select("PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + " AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString()) + " AND  FkVoucherNo=" + ReceiptID + " AND CompanyNo=" + DBGetVal.FirmNo + " order by VoucherSrNo").Table;

                        //For PayType Details
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0)
                            tVoucherDetails.VoucherSrNo = VoucherSrNo;
                        else tVoucherDetails.VoucherSrNo = Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[4].ToString());
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(dtPayLedger.Select("PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + " AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString());// Convert.ToInt64(dgPayType.Rows[row].Cells[3].Value);
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
                        if (ID != 0 && PartyNo == PartID && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);

                        if (ID == 0)
                            tVoucherDetails.Debit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) : 0) + Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                        else
                        {
                            if (dtVoucherDetails.Rows.Count > 0)
                            {
                                if (PartID != ObjFunction.GetListValue(lstPartyEnglish))
                                    tVoucherDetails.Debit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString())));
                                else
                                    tVoucherDetails.Debit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) - amt));
                            }
                            else
                                tVoucherDetails.Debit = totamt;
                        }

                        tVoucherDetails.Credit = 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);


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

        public bool SaveReceiptES(long SalesID)
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
                              " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.DReceipt + ") AND (TVoucherEntry.VoucherDate ='" + dtpBillDate.Text + "') AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ") AND " +
                                " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + ") ", CommonFunctions.ConStr);
                        if (CancelFlag == true)
                        {
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + 0 + " and srno!=508  order by VoucherSrNo").Table;
                            // setCompanyRatio();
                            FillPayType();
                        }
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                            " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
                        long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);
                        double PrevAmt = 0;
                        for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                            //{if(i==0)
                            //    PrevAmt += Convert.ToDouble(dtVchPrev.Rows[0].ItemArray[1].ToString());
                            //}
                            //double a = PrevAmt;
                            PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
                        PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));

                        // PrevAmt =( (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr)) + a);// +((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));

                        //int VoucherSrNo = 1;
                        dbTVoucherEntry = new DBTVaucherEntry();
                        //Voucher Header Entry
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = ReceiptID;
                        tVoucherEntry.VoucherTypeCode = VchType.DReceipt;
                        tVoucherEntry.VoucherUserNo = VoucherUserNo;
                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                        tVoucherEntry.VoucherTime = dtpBillTime.Value;
                        tVoucherEntry.Narration = "Receipt Bill";
                        tVoucherEntry.Reference = "";
                        tVoucherEntry.ChequeNo = 0;
                        tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date; //; ; Convert.ToDateTime("01-Jan-1900");
                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                        tVoucherEntry.BilledAmount = (ReceiptID != 0) ? PrevAmt : Convert.ToDouble(txtGrandTotal.Text);
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
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//error umesh

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

        public bool SaveReceiptESNew(long SalesID)
        {
            DataTable dtPayType = new DataTable();
            int cntPayType = 1;

            long tempid = -1, ReceiptID = 0, VoucherUserNo = 0;

            for (int row = 1; row <= dgPayType.Rows.Count - 1; row++) //for (int row = 1; row <= 4; row++)//for (int j = 0; j < dgPayType.Rows.Count; j++)
            {
                if (row == 1 || row == 3 || row == 4 || row > 4)
                {
                    if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                    {
                        if (row == 1 || row == 3 || row == 4 || row > 4)
                            ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                              " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.DReceipt + ") AND (TVoucherEntry.VoucherDate ='" + dtpBillDate.Text + "') AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetListValue(lstPartyEnglish) + ") AND " +
                                " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + ") ", CommonFunctions.ConStr);
                        //else if (row == 3 || row == 4)
                        //  ReceiptID = ObjQry.ReturnLong("SELECT FKReceiptVoucherNo FROM TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=" + dgPayType.Rows[row].Cells[1].Value + " ", CommonFunctions.ConStr);
                        if (CancelFlag == true)
                        {
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + 0 + " and srno!=508  order by VoucherSrNo").Table;

                            FillPayType();
                        }
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                            " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
                        long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);
                        double PrevAmt = 0;
                        for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                            PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
                        if (ReceiptID != 0)
                        {
                            if (tempDate.Date == dtpBillDate.Value.Date)
                                PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
                            else
                                PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr)) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
                        }
                        else
                            PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr)) + ((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
                        //int VoucherSrNo = 1;
                        dbTVoucherEntry = new DBTVaucherEntry();
                        //Voucher Header Entry
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = ReceiptID;
                        tVoucherEntry.VoucherTypeCode = VchType.DReceipt;
                        tVoucherEntry.VoucherUserNo = VoucherUserNo;
                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                        tVoucherEntry.VoucherTime = dtpBillTime.Value;
                        tVoucherEntry.Narration = "Receipt Bill";
                        tVoucherEntry.Reference = "";
                        tVoucherEntry.ChequeNo = 0;
                        tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;
                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                        tVoucherEntry.BilledAmount = PrevAmt;// Convert.ToDouble(txtGrandTotal.Text);
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
                        tVoucherEntry.LedgerNo = ObjFunction.GetListValue(lstPartyEnglish);
                        tVoucherEntry.UserID = DBGetVal.UserID;
                        tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);// SetVoucherCompany(tVoucherEntry);

                        DataTable dtVoucherDetails = new DataTable();
                        if (ReceiptID != 0)
                        {
                            dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where FkVoucherNo=" + ReceiptID + " order by VoucherSrNo").Table;
                            dtPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo From TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=" + dgPayType.Rows[row].Cells[1].Value + "  order by PKVoucherPayTypeNo").Table;
                        }
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,TD.LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails TD,TVoucherEntry TC Where TC.PKVoucherNo=TD.FKVoucherNo AND TC.PayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + " AND TD.LedgerNo=" + ObjFunction.GetListValue(lstPartyEnglish) + " AND  TD.FkVoucherNo=" + ReceiptID + " AND TD.CompanyNo=" + DBGetVal.FirmNo + " order by TD.VoucherSrNo").Table;//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        double totamt = 0, amt = 0;

                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                        {

                            amt = Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                            totamt = totamt + amt;
                        }
                        amt = 0; if (CancelFlag == true) totamt = -totamt;
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        //For Party Ledger
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
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
                                tVoucherDetails.Credit = Newval + ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString())) + totamt);
                            else
                                tVoucherDetails.Credit = totamt;
                        }
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Party;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                        totamt = 0;
                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                            totamt += Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails Where LedgerNo=" + Convert.ToInt64(dtPayLedger.Select("PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + " AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString()) + " AND  FkVoucherNo=" + ReceiptID + " AND CompanyNo=" + DBGetVal.FirmNo + " order by VoucherSrNo").Table;//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        //For PayType Details
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
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
                        if (ID != 0 && PartyNo == ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        if (ID == 0)
                            tVoucherDetails.Debit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) : 0) + Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                        else
                        {
                            if (dtVoucherDetails.Rows.Count > 0)
                                tVoucherDetails.Debit = Newval + ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString())) + totamt);
                            else
                                tVoucherDetails.Debit = totamt;
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

        public bool SaveReceiptESOLD(DateTime dt, long PartID)
        {
            long tempid = -1, ReceiptID = 0, VoucherUserNo = 0;

            for (int row = 1; row <= dgPayType.Rows.Count - 1; row++) //for (int row = 1; row <= 4; row++)//for (int j = 0; j < dgPayType.Rows.Count; j++)
            {
                if (row == 1 || row == 3 || row == 4 || row > 4)
                {
                    if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                    {
                        if (row == 1 || row == 3 || row == 4 || row > 4)
                            ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                              " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherEntry.VoucherDate ='" + dt.ToString(Format.DDMMMYYYY) + "') AND (TVoucherDetails.LedgerNo = " + PartID + ") AND " +
                                " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + ") ", CommonFunctions.ConStr);
                        if (CancelFlag == true)
                        {
                            dtVchPrev = ObjFunction.GetDataView("Select LedgerNo,Debit,CompanyNo From TVoucherDetails Where FkVoucherNo=" + 0 + " and srno!=508  order by VoucherSrNo").Table;

                            FillPayType();
                        }
                        DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                            " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
                        VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
                        long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);
                        double PrevAmt = 0;
                        for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                            PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
                        PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt);// +((CancelFlag == true) ? -Convert.ToDouble(txtGrandTotal.Text) : Convert.ToDouble(txtGrandTotal.Text));
                        if (PrevAmt < 0.0) break;

                        dbTVoucherEntry = new DBTVaucherEntry();
                        //Voucher Header Entry
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = ReceiptID;
                        tVoucherEntry.VoucherTypeCode = VchType.Receipt;
                        tVoucherEntry.VoucherUserNo = VoucherUserNo;
                        tVoucherEntry.VoucherDate = Convert.ToDateTime(dt.ToString("dd-MMM-yyyy"));
                        tVoucherEntry.VoucherTime = dtpBillTime.Value;
                        tVoucherEntry.Narration = "Receipt Bill";
                        tVoucherEntry.Reference = "";
                        tVoucherEntry.ChequeNo = 0;
                        tVoucherEntry.ClearingDate = Convert.ToDateTime("01-Jan-1900");
                        tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                        tVoucherEntry.BilledAmount = PrevAmt;// Convert.ToDouble(txtGrandTotal.Text);
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
                        dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry); //SetVoucherCompany(tVoucherEntry);

                        DataTable dtVoucherDetails = new DataTable();
                        if (ReceiptID != 0)
                        {
                            dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where FkVoucherNo=" + ReceiptID + " order by VoucherSrNo").Table;
                            //dtPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo From TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo=" + dgPayType.Rows[row].Cells[1].Value + "  order by PKVoucherPayTypeNo").Table;
                        }
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,TD.LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails TD,TVoucherEntry TC Where TC.PKVoucherNo=TD.FKVoucherNo AND TC.PayTypeNo=" + ObjFunction.GetListValue(lstPaymentType) + " AND TD.LedgerNo=" + PartID + " AND  TD.FkVoucherNo=" + ReceiptID + " AND TD.CompanyNo=" + DBGetVal.FirmNo + " order by TD.VoucherSrNo").Table;//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())

                        double totamt = 0, amt = 0;

                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                        {

                            amt = Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                            totamt = totamt + amt;
                        }
                        amt = 0; if (CancelFlag == true) totamt = -totamt;
                        if (ID != 0 && PartyNo == PartID && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);//Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString())
                        //}

                        //For Party Ledger
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0)
                            tVoucherDetails.VoucherSrNo = VoucherSrNo;
                        else tVoucherDetails.VoucherSrNo = Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[4].ToString());
                        tVoucherDetails.SignCode = 2;
                        tVoucherDetails.LedgerNo = PartID;
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
                            {
                                if (PartID != ObjFunction.GetListValue(lstPartyEnglish))
                                    tVoucherDetails.Credit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString())));
                                else
                                    tVoucherDetails.Credit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[3].ToString()) - amt));
                            }
                            else
                                tVoucherDetails.Credit = totamt;
                        }
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.SrNo = Others.Party;
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                        totamt = 0;
                        if (Convert.ToDouble(dgPayType.Rows[row].Cells[2].Value) != 0)
                            totamt += Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);

                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails Where LedgerNo=" + Convert.ToInt64(dtPayLedger.Select("PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + " AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString()) + " AND  FkVoucherNo=" + ReceiptID + " AND CompanyNo=" + DBGetVal.FirmNo + " order by VoucherSrNo").Table;

                        //For PayType Details
                        tVoucherDetails = new TVoucherDetails();
                        //tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > VoucherSrNo - 1) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                        tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[0].ToString()) : 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0)
                            tVoucherDetails.VoucherSrNo = VoucherSrNo;
                        else tVoucherDetails.VoucherSrNo = Convert.ToInt64(dtVoucherDetails.Rows[0].ItemArray[4].ToString());
                        tVoucherDetails.SignCode = 1;
                        tVoucherDetails.LedgerNo = Convert.ToInt64(dtPayLedger.Select("PayTypeNo=" + Convert.ToInt64(dgPayType.Rows[row].Cells[1].Value) + " AND CompanyNo=" + DBGetVal.FirmNo)[0][2].ToString());// Convert.ToInt64(dgPayType.Rows[row].Cells[3].Value);
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
                        if (ID != 0 && PartyNo == PartID && PayType == ObjFunction.GetListValue(lstPaymentType) && CancelFlag == false) amt = Convert.ToDouble(dtVchPrev.Select("CompanyNo=" + DBGetVal.FirmNo + "")[0].ItemArray[1].ToString());//ObjQry.ReturnDouble("Select Sum(Debit) From TVoucherDetails Where LedgerNo=" + ObjFunction.GetComboValue(cmbPartyName) + " AND  CompanyNo=" + Convert.ToInt64(dtCompRatio.Rows[i].ItemArray[0].ToString()) + " ", CommonFunctions.ConStr);

                        if (ID == 0)
                            tVoucherDetails.Debit = ((dtVoucherDetails.Rows.Count > 0) ? Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) : 0) + Convert.ToDouble(dgPayType.Rows[row].Cells[4 + (1)].Value);
                        else
                        {
                            if (dtVoucherDetails.Rows.Count > 0)
                            {
                                if (PartID != ObjFunction.GetListValue(lstPartyEnglish))
                                    tVoucherDetails.Debit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString())));
                                else
                                    tVoucherDetails.Debit = ((Convert.ToDouble(dtVoucherDetails.Rows[0].ItemArray[2].ToString()) - amt));
                            }
                            else
                                tVoucherDetails.Debit = totamt;
                        }

                        tVoucherDetails.Credit = 0;
                        if (tVoucherDetails.PkVoucherTrnNo == 0) VoucherSrNo += 1;
                        tVoucherDetails.Narration = "";
                        tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);


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
                    btnMixMode.Visible = false;
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
                        btnMixMode.Visible = false;
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
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillDelete)) == true)
                {
                    Form frm = new Utilities.PasswordAsk(3);
                    ObjFunction.OpenForm(frm);
                    if (Utilities.PasswordAsk.IsDeleteAllow == 0) return;
                }

                if (OMMessageBox.Show("Are you sure you want to cancel this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    CancelFlag = true;
//                    if ((DBGetVal.KachhaFirm == false))
 //                   {
  //                      SaveReceipt(ID);
   //                 }
    //                else
     //               {
      //                  SaveReceiptES(ID);
       //             }
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
                    //SetNavigation();

                    //  setDisplay(true);
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
                            //dgBill.CurrentCell.Value = "";
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
                            //else dgBill.CurrentCell.Value = Convert.ToDouble(dgBill.CurrentCell.Value.ToString()).ToString(Format.ThreeFloating);
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
                    //else if (dgBill.CurrentCell.ColumnIndex == ColIndex.PackagingCharges)
                    //{
                    //    e.SuppressKeyPress = true;
                    //    dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];

                    //}
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
                    //  dgGrossWt.Rows.Add();
                    //dgGrossWt.CurrentCell.Value = 0;
                    txtTotalGrossWt.Text = "0.00";
                    txtGrossWt.Text = "0.00";
                    txtTariffWt.Text = "0.00";
                    //if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value != null)
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Remarks].Value != null)
                    {

                        string str = null;
                        string[] strArr = null;
                        str = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Remarks].Value.ToString();
                        char[] splitchar = { ',' };
                        strArr = str.Split(splitchar);
                        //string str = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Remarks].Value.ToString();
                        {
                            //  string a = str.Split(); 
                            int a = strArr.Count();
                            for (int i = 0; i < a; i++)
                            {
                                dgGrossWt.Rows.Add();
                                dgGrossWt.Rows[i].Cells[1].Value = strArr[i];
                                dgGrossWt.Rows[i].Cells[0].Value = i + 1;
                                // dgGrossWt.CurrentCell = dgGrossWt.Rows[i+1].Cells[1];
                            }
                        }
                        txtTotalGrossWt.Text = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value.ToString();
                        txtGrossWt.Text = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.GrossWt].Value.ToString();
                        txtTariffWt.Text = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TariffWt].Value.ToString();
                        // dgGrossWt.Rows.Add();
                        // dgGrossWt.CurrentCell = dgGrossWt.Rows[dgGrossWt.Rows.Count + 1].Cells[1];
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
                        txtDisplayName.Text = dgBill.CurrentRow.Cells[ColIndex.DisplayName].EditedFormattedValue.ToString().Trim();
                        pnlDisplayName.Location = new Point(100, 154);
                        pnlDisplayName.Visible = true;
                        txtDisplayName.Focus();
                    }
                }
                else if (e.KeyCode == Keys.S && e.Control)
                {

                    Master.ItemAdvanceSearch itemSearch = new Master.ItemAdvanceSearch();
                    ObjFunction.OpenForm(itemSearch);
                    if (itemSearch.ItemNo != 0)
                    {
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value = itemSearch.BarCode;
                        dgBill_CellEndEdit(dgBill, new DataGridViewCellEventArgs(ColIndex.ItemName, dgBill.Rows.Count - 1));
                        //dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                        itemSearch.Close();
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value == null)
                    {
                        if (txtDiscount1Per.Visible == true)
                        {
                            txtDiscount1Per.Focus();
                        }
                        else
                        {
                            txtPaymentType.Focus();
                        }
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
                    //Master.ItemAdvanceSearch itemSearch = new Master.ItemAdvanceSearch();
                    //ObjFunction.OpenForm(itemSearch);
                    //if (itemSearch.ItemNo != 0)
                    //{
                    //    dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                    //    dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value = itemSearch.BarCode;
                    //    dgBill_CellEndEdit(dgBill, new DataGridViewCellEventArgs(ColIndex.ItemName, dgBill.Rows.Count - 1));
                    //    //dgBill_KeyDown(dgBill, new KeyEventArgs(Keys.Enter));
                    //    itemSearch.Close();
                    //}
                    //txtRateType.Focus();
                    pnlRateTypeH.Visible = true;
                    lstRateType.Focus();
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
                                    //ItemLevelDisc = true;
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

        private void DeleteRefDetails()
        {
            try
            {
                if (PayType == 3)
                {
                    if (PayType != ObjFunction.GetListValue(lstPaymentType) && ID != 0)
                    {
                        DataTable dtRef = ObjFunction.GetDataView("Select PKRefTrnNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FKVoucherNo=" + ID + " ").Table;
                        for (int i = 0; i < dtRef.Rows.Count; i++)
                        {
                            tVchRefDtls = new TVoucherRefDetails();
                            tVchRefDtls.PkRefTrnNo = Convert.ToInt64(dtRef.Rows[i].ItemArray[0].ToString());
                            dbTVoucherEntry.DeleteTVoucherRefDetails(tVchRefDtls);
                        }
                    }
                }
                else if (PayType != 3)// old paytype not credit
                {
                    if (ObjFunction.GetListValue(lstPaymentType) == 3) //current paytype
                    {
                        if ((tempPartyNo != ObjFunction.GetListValue(lstPartyEnglish) || PayType != ObjFunction.GetListValue(lstPaymentType)) && ID != 0)
                        {
                            DataTable dtDelPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo,TVoucherPayTypeDetails.CompanyNo,FKReceiptVoucherNo,Amount From TVoucherPayTypeDetails,TVoucherDetails Where TVoucherDetails.PkVoucherTrnNo=TVoucherPayTypeDetails.FKVoucherTrnNo AND FKSalesVoucherNo=" + ID + "  order by PKVoucherPayTypeNo").Table;
                            for (int k = 0; k < dtDelPayType.Rows.Count; k++)
                            {
                                tVchPayTypeDetails = new TVoucherPayTypeDetails();
                                tVchPayTypeDetails.PKVoucherPayTypeNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[0].ToString());
                                dbTVoucherEntry.DeleteTVoucherPayTypeDetails(tVchPayTypeDetails);

                                DataTable dtUpdateVoucher = ObjFunction.GetDataView("Select PKVoucherTrnNo,Debit,Credit From TVoucherDetails Where FKVoucherNo=" + dtDelPayType.Rows[k].ItemArray[2].ToString() + " AND CompanyNo=" + dtDelPayType.Rows[k].ItemArray[1].ToString() + "").Table;
                                double totamt = 0;
                                double billedamt = 0;
                                bool alllowdel = false;
                                for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                                {
                                    double DrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[1].ToString());
                                    double CrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[2].ToString());
                                    if (DrAmt > 0) DrAmt = DrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                                    if (CrAmt > 0) CrAmt = CrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                                    dbTVoucherEntry.UpdateVoucherDetails(DrAmt, CrAmt, Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString()));
                                    billedamt = billedamt + DrAmt;
                                    totamt = totamt + DrAmt + CrAmt;
                                    alllowdel = true;
                                }
                                dbTVoucherEntry.UpdateVoucherEntry(billedamt, Convert.ToInt32(dtDelPayType.Rows[k].ItemArray[2].ToString()));
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
                    }
                    else if ((ObjFunction.GetListValue(lstPaymentType) == 2) && (tempDate.Date == dtpBillDate.Value.Date))
                    {
                        if ((tempPartyNo != ObjFunction.GetListValue(lstPartyEnglish) && PayType == ObjFunction.GetListValue(lstPaymentType)) && ID != 0)
                        {
                            DataTable dtDelPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo,TVoucherPayTypeDetails.CompanyNo,FKReceiptVoucherNo,Amount From TVoucherPayTypeDetails,TVoucherDetails Where TVoucherDetails.PkVoucherTrnNo=TVoucherPayTypeDetails.FKVoucherTrnNo AND FKSalesVoucherNo=" + ID + "  order by PKVoucherPayTypeNo").Table;
                            for (int k = 0; k < dtDelPayType.Rows.Count; k++)
                            {
                                tVchPayTypeDetails = new TVoucherPayTypeDetails();
                                tVchPayTypeDetails.PKVoucherPayTypeNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[0].ToString());
                                dbTVoucherEntry.DeleteTVoucherPayTypeDetails(tVchPayTypeDetails);

                                DataTable dtUpdateVoucher = ObjFunction.GetDataView("Select PKVoucherTrnNo,Debit,Credit From TVoucherDetails Where FKVoucherNo=" + dtDelPayType.Rows[k].ItemArray[2].ToString() + " AND CompanyNo=" + dtDelPayType.Rows[k].ItemArray[1].ToString() + "").Table;
                                double totamt = 0;
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
                    }
                    else if ((tempDate.Date != dtpBillDate.Value.Date))
                    {

                        DataTable dtDelPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo,TVoucherPayTypeDetails.CompanyNo,FKReceiptVoucherNo,Amount From TVoucherPayTypeDetails,TVoucherDetails Where TVoucherDetails.PkVoucherTrnNo=TVoucherPayTypeDetails.FKVoucherTrnNo AND FKSalesVoucherNo=" + ID + "  order by PKVoucherPayTypeNo").Table;
                        for (int k = 0; k < dtDelPayType.Rows.Count; k++)
                        {
                            tVchPayTypeDetails = new TVoucherPayTypeDetails();
                            tVchPayTypeDetails.PKVoucherPayTypeNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[0].ToString());
                            dbTVoucherEntry.DeleteTVoucherPayTypeDetails(tVchPayTypeDetails);

                            DataTable dtUpdateVoucher = ObjFunction.GetDataView("Select PKVoucherTrnNo,Debit,Credit From TVoucherDetails Where FKVoucherNo=" + dtDelPayType.Rows[k].ItemArray[2].ToString() + " AND CompanyNo=" + dtDelPayType.Rows[k].ItemArray[1].ToString() + "").Table;
                            //   double totamt = 0;
                            double billedamt = 0;
                            // bool alllowdel = false;
                            for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                            {
                                double DrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[1].ToString());
                                double CrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[2].ToString());
                                if (DrAmt > 0) DrAmt = DrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                                if (CrAmt > 0) CrAmt = CrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                                //  dbTVoucherEntry.UpdateVoucherDetails(DrAmt, CrAmt, Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString()));
                                billedamt = billedamt + DrAmt;
                                // totamt = totamt + DrAmt + CrAmt;
                                //alllowdel = true;
                            }
                            dbTVoucherEntry.UpdateVoucherEntry(billedamt, Convert.ToInt32(dtDelPayType.Rows[k].ItemArray[2].ToString()));

                        }
                    }
                    long ControlUnder = ObjQry.ReturnLong("Select ControlUnder From MPayType Where PKPayTypeNo=" + PayType + "", CommonFunctions.ConStr);
                    //if (PayType == 5 || PayType == 4)
                    if (ControlUnder == 5 || ControlUnder == 4)
                    {
                        if (PayType != ObjFunction.GetListValue(lstPaymentType) && ID != 0)
                        {
                            DataTable dtCredit = ObjFunction.GetDataView("Select PKSrNo From TVoucherChqCreditDetails  Where FKVoucherNo=" + ID + " ").Table;
                            for (int i = 0; i < dtCredit.Rows.Count; i++)
                            {
                                tVchChqCredit = new TVoucherChqCreditDetails();
                                tVchChqCredit.PkSrNo = Convert.ToInt64(dtCredit.Rows[i].ItemArray[0].ToString());
                                dbTVoucherEntry.DeleteTVoucherChqCreditDetails(tVchChqCredit);
                            }
                            //if (PayType == 4)
                            if (ControlUnder == 4)
                                while (dgPayChqDetails.Rows.Count > 0)
                                    dgPayChqDetails.Rows.RemoveAt(0);
                            //if (PayType == 5)
                            if (ControlUnder == 5)
                                while (dgPayCreditCardDetails.Rows.Count > 0)
                                    dgPayCreditCardDetails.Rows.RemoveAt(0);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                //  Amt = ObjQry.ReturnDouble("Select sum(Case When TStock.Rate <>0 Then (((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount) Else MRateSetting.MRP*TStock.Quantity END) FROM TStock INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                //                     " Where TStock.FkVoucherNo=" + ID + " AND (Case When TStock.Rate <>0 Then ((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount Else MRateSetting.MRP*TStock.Quantity END)>0 ", CommonFunctions.ConStr);
                Amt += Convert.ToDouble(txtTotalAnotherDisc.Text);// +Convert.ToDouble(txtTotalDisc.Text);
                string[] ReportSession;

                ReportSession = new string[22];
                ReportSession[0] = ID.ToString();
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType..DeliveryChallan : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : lstPaymentType.Text);
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
                                    childForm = ObjFunction.LoadReportObject("DCGetBill-A4.rpt", CommonFunctions.ReportPath); // for print 
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
                        childForm = ObjFunction.LoadReportObject("GetBillMar.rpt", CommonFunctions.ReportPath);
                        if (childForm != null)
                        {
                            DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession, true);
                            if (objRpt.PrintReport() == true)
                            {
                                File.Add(objRpt.newFullPath);
                                SendMail.SendEmail(Convert.ToString(txtToEmailid.Text), null, GetEmailSubAndBody().EmailSubject, GetEmailSubAndBody().EmailBody, File);
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
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBillMar.rpt", CommonFunctions.ReportPath), ReportSession);
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

        public void PrintBillEmail()
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
                ReportSession[0] = ID.ToString();
                ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType..DeliveryChallan : VoucherType) + "", CommonFunctions.ConStr).ToString();
                ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                ReportSession[4] = Amt.ToString("0.00");
                ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : lstPaymentType.Text);
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


                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                {

                    if ((PartyNo != 21) && (Convert.ToInt32(lstPaymentType.SelectedValue) != 2))
                    {
                        double amt = 0.00;


                        //

                        
                        amt = ObjQry.ReturnDouble("exec GetOutstandingForBills '01-Apr-2019','"+ DBGetVal.ServerTime + "'," + VoucherType + " ,1,'" + PartyNo + "' " , CommonFunctions.ConStr);

                        //
                      //amt = ObjQry.ReturnDouble("select sum(debit-credit) as amt from tvoucherdetails where ledgerno=" + PartyNo + " group by ledgerno", CommonFunctions.ConStr);
                        if (amt > 0)
                        {
                            lblOutstanding.Text = "Outstanding To Recive :" + amt;
                        }
                        else
                            lblOutstanding.Text = "Outstanding To Pay :" + amt;
                    }
                    //else
                    //    lblOutstanding.Text = "Outstanding To Recive :0.00";

                    ReportSession[21] = lblOutstanding.Text.ToString();
                }
                else
                    ReportSession[21] = "0";

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
                            File.Add(objRpt.newFullPath);
                            SendMail.SendEmail(Convert.ToString(txtToEmailid.Text), null, GetEmailSubAndBody().EmailSubject, GetEmailSubAndBody().EmailBody, File);
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
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                //DialogResult ds = OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                //if (ds == DialogResult.Yes)
                //{
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
                //}
                //else if (ds == DialogResult.Cancel)
                //    PrintBill(1);

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
                        //pnlPartySearch.SetBounds(x, y, dgPartySearch.Width + 10, dgPartySearch.Height + 10);
                        pnlInvSearch.Location = new Point(x, y);
                        string str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #],convert(varchar(11),TVoucherEntry.VoucherDate,105) as 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                     "TVoucherEntry.PkVoucherNo FROM TVoucherEntry WHERE (TVoucherEntry.VoucherTypeCode IN (" + VoucherType + ")) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
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
                        //pnlPartySearch.SetBounds(x, y, dgPartySearch.Width + 10, dgPartySearch.Height + 10);
                        pnlPartySearch.Location = new Point(x, y);
                        string str = "";
                        if (DBGetVal.KachhaFirm == false)
                        {
                            str = "SELECT    0 as [#], TVoucherEntry.VoucherUserNo AS [Doc #], convert(varchar(11),TVoucherEntry.VoucherDate,105) as 'Date', TVoucherEntry.BilledAmount AS 'Amount'," +
                                        "TVoucherEntry.PkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo WHERE (TVoucherEntry.VoucherTypeCode IN (" + VchType.DeliveryChallan + "," + VchType.SalesOrder + ")) AND (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")" +
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
            pnlSearch.Visible = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btnBillCancel.Enabled = true;
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

        private void pnlPartial_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerateIMEI_Click(object sender, EventArgs e)
        {
            Form NewF = new GenerateIMEI(ID, VoucherType);
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnTemp_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAdvanceSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == false)
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    lstPartyEnglish.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
                    lstPartyLang.Font = ObjFunction.GetLangFont();
                    ObjFunction.FillList(lstPartyLang, "Select LedgerNo,LedgerLangName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") and MLedger.IsActive='true' and ledgerno not in (24) order by LedgerName");
                    // lstPartyLang.SelectedValue = lstPartyEnglish.SelectedValue;
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
                    //lstPartyLang.SelectedValue = lstPartyEnglish.SelectedValue;
                }
                ObjFunction.FillList(lstPartyEnglish, "SELECT MLedger.LedgerNo, MLedger.LedgerName + ' ' + ISNULL(Mcity.Cityname, '') + '-' + ISNULL(MArea.AreaName, '') AS LedgerName FROM MLedger LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo  left outer join mcity on mcity.cityno=mledgerdetails.cityno  LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo WHERE (MLedger.GroupNo IN (26) and MLedger.ledgerno not in (24)) and MLedger.IsActive='true' ORDER BY LedgerName ");

            }
        }

        private void btnTransOK_Click(object sender, EventArgs e)
        {

        }

        private void btnTCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnTransporterDetails_Click(object sender, EventArgs e)
        {
            pnlTransporter.Visible = true;
        }

        private void dgDeliveryAddress_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rbMarathi_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void txtChargePer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CalculateTotal();
                txtChrgRupees1.Focus();

                // txtRemark.Focus();


            }

        }

        private void txtChargePer_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pnlDeliveryAddress.Visible = false;
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
                    //e.SuppressKeyPress = true;
                    dgBill.CurrentRow.Cells[ColIndex.Quantity].Value = Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.GrossWt].Value) - Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.TariffWt].Value);


                    MovetoNext move2n = new MovetoNext(m2n1);
                    BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.TariffWt, dgBill });
                    dgBill.CurrentCell = dgBill[ColIndex.TariffWt, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.TariffWt)
                {
                    //  e.SuppressKeyPress = true;
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
                    // int a =Convert.ToInt32( dgBill.CurrentRow.Cells[ColIndex.ItemNo].Value);// dgBill.CurrentRow.Cells[dgBill.CurrentCell, ColIndex.ItemNo].value;
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
                    // BeginInvoke(move2n, new object[] { e.RowIndex, colIndex, dgBill });
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
                else
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                    dgBill.CurrentCell = dgBill[1, dgBill.Rows.Count - 1];
                    dgBill.Focus();
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
                        //if ((Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkItemTaxInfo].Value) == 0) || (Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0))
                        //   if ((Convert.ToInt32(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0))
                        BindGrid(dgBill.CurrentCell.RowIndex);
                        //else
                        //{
                        //    CalculateTotal();
                        //}
                        if (dgBill.Columns[ColIndex.PackagingCharges].Visible == true)
                        {

                            MovetoNext move2n = new MovetoNext(m2n1);
                            BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.PackagingCharges, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.PackagingCharges, dgBill.CurrentCell.RowIndex];
                            dgBill.Focus();

                        }
                        //  else if (dgBill.Columns[ColIndex.DiscPercentage].Visible == true)
                        // {
                        else if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_StopOnDisc)) == true)
                        {
                            MovetoNext move2n = new MovetoNext(m2n1);
                            BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.DiscPercentage, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, dgBill.CurrentCell.RowIndex];
                            dgBill.Focus();
                            //}
                            //else
                            //{
                            //    MovetoNext move2n = new MovetoNext(m2n);
                            //    BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                            //    dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.Rows.Count - 1];
                            //    dgBill.Focus();
                            //}
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
                            if (StopOnRate == true)
                            {
                                // MovetoNext move2n = new MovetoNext(m2n);
                                // BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, 1, dgBill });
                                dgBill.CurrentCell = dgBill[ColIndex.Rate, dgBill.Rows.Count - 1];
                                dgBill.Focus();
                                //    return;
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
                                Desc_Start();
                            }
                        }
                        CalculateTotal();
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
                        pnlBranch.Location = new Point(dgPayCreditCardDetails.CurrentCell.ContentBounds.X + 200 + dgPayCreditCardDetails.Location.X, dgPayCreditCardDetails.CurrentCell.ContentBounds.Y + 50);

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

        private void lstBranch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgPayChqDetails.Visible == true)
                    {
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value = lstBranch.Text;
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[7].Value = lstBranch.SelectedValue;
                        pnlBranch.Visible = false;
                        dgPayChqDetails.Focus();
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[4, dgPayChqDetails.CurrentCell.RowIndex];
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[2].Value = lstBranch.Text;
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[6].Value = lstBranch.SelectedValue;
                        pnlBranch.Visible = false;
                        dgPayCreditCardDetails.Focus();
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    if (dgPayChqDetails.Visible == true)
                    {
                        if (dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value == null)
                            dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[3].Value = "";
                        dgPayChqDetails.Rows[dgPayChqDetails.CurrentCell.RowIndex].Cells[7].Value = 0;
                        pnlBranch.Visible = false;
                        dgPayChqDetails.Focus();
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[4, dgPayChqDetails.CurrentCell.RowIndex];
                    }
                    else if (dgPayCreditCardDetails.Visible == true)
                    {
                        if (dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[3].Value == null)
                            dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[3].Value = "";
                        dgPayCreditCardDetails.Rows[dgPayCreditCardDetails.CurrentCell.RowIndex].Cells[6].Value = 0;
                        pnlBranch.Visible = false;
                        dgPayCreditCardDetails.Focus();
                        dgPayCreditCardDetails.CurrentCell = dgPayCreditCardDetails[3, dgPayCreditCardDetails.CurrentCell.RowIndex];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
                        dgPayChqDetails.CurrentCell = dgPayChqDetails[3, dgPayChqDetails.CurrentCell.RowIndex];
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

        private void txtSchemeDisc_TextChanged(object sender, EventArgs e)
        {
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            try
            {
                double TotalAmt = 0;
                TotalAmt = ((Convert.ToDouble(txtSubTotal.Text) + Convert.ToDouble(txtTotalTax.Text)) - Convert.ToDouble(txtTotalDisc.Text));
                if (((TextBox)sender).Name == "txtDiscount1Per")
                {
                    //if (((TextBox)sender).Text == "") ((TextBox)sender).Text = "0.00";
                    txtOtherDisc.Text = Format.DoubleFloating;
                    //  txtDiscount1Per.Text = txtDiscount1Per.Text.ToString();
                    // Format.DoubleFloating;
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        //txtDiscRupees1.Focus();//txtChrgRupees1.Focus();//txtDiscRupees1.Text = Convert.ToDouble((TotalAmt * Convert.ToDouble(txtDiscount1.Text)) / 100).ToString("0.00");
                        CalculateTotal();
                    }
                }
                else if (((TextBox)sender).Name == "txtDiscRupees1")
                {
                    txtOtherDisc.Text = Format.DoubleFloating;
                    txtSchemeDisc.Text = Format.DoubleFloating;
                    // if (((TextBox)sender).Text == "") ((TextBox)sender).Text = "0.00";
                    if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
                    {
                        OMMessageBox.Show("Enter Discount Value.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        ((TextBox)sender).Focus();
                    }
                    else
                    {
                        CalculateTotal();
                        // txtDiscount1.Text = Convert.ToDouble((100 * Convert.ToDouble(txtDiscRupees1.Text)) / TotalAmt).ToString("0.00");
                        // txtDiscount1.Text = "0.00";
                        // txtChrgRupees1.Focus();
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
                        if (lstPaymentType.Enabled == true)
                            lstPaymentType.Focus();
                        else
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
                    else if (ObjQry.ReturnLong("Select VoucherUserNo from TVoucherEntry where VoucherTypeCode=" + VchType.DeliveryChallan + " and VoucherUserNo=" + Convert.ToInt64(((TextBox)sender).Text) + " AND VoucherDate>='" + dtFrom.Date + "'  And ( PkVoucherNo <> " + ID + " ) AND VoucherDate<='" + dtTo.Date + "' ", CommonFunctions.ConStr) != 0)
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
                //txtDiscRupees1.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) - Convert.ToDouble(txtSchemeDisc.Text.Trim()) + Convert.ToDouble(txtTotalTax.Text.Trim())) * Convert.ToDouble(txtDiscount1.Text)) / 100).ToString("0.00");
                Control_Leave((object)txtDiscount1Per, new EventArgs());
                //txtDiscRupees1.Focus();
                txtSchemeDisc.Enabled = true;
                txtSchemeDisc.Focus();
            }
        }

        private void txtSchemeDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSchemeDisc.Text == "") txtSchemeDisc.Text = "0.00";
                //txtDiscRupees1.Text = Convert.ToDouble(((Convert.ToDouble(txtSubTotal.Text.Trim()) - Convert.ToDouble(txtTotalDisc.Text.Trim()) - Convert.ToDouble(txtSchemeDisc.Text.Trim()) + Convert.ToDouble(txtTotalTax.Text.Trim())) * Convert.ToDouble(txtDiscount1.Text)) / 100).ToString("0.00");
                Control_Leave((object)txtSchemeDisc, new EventArgs());
                txtPaymentType.Focus();
                //CalculateTotal();
            }
        }

        private void txtOtherDisc_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void txtChrgRupees2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtChrgRupees2.Text == "")
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

        private void btnShortcut_Click(object sender, EventArgs e)
        {
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
                    // if (Convert.ToInt32(dgGrossWt.Rows.Count) != dgGrossWt[dgGrossWt.CurrentCell.ColumnIndex, dgGrossWt.CurrentCell.RowIndex + 1](dgGrossWt[dgGrossWt.CurrentCell.ColumnIndex, dgGrossWt.CurrentCell.RowIndex]))
                    if (Convert.ToDouble(dgGrossWt.Rows[count - 1].Cells[1].Value) != 0)
                        dgGrossWt.Rows.Add();
                    dgGrossWt.CurrentCell = dgGrossWt[dgGrossWt.CurrentCell.ColumnIndex, dgGrossWt.CurrentCell.RowIndex + 1];
                    dgGrossWt.CurrentCell.Value = 0;
                }
                else if ((dgGrossWt.CurrentCell.Value == null))
                {
                    dgGrossWt.CurrentCell = dgGrossWt.CurrentCell;//dgGrossWt[dgGrossWt.CurrentCell.ColumnIndex, dgGrossWt.CurrentCell.RowIndex + 1];
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
                //btnGrossWt.Focus();
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
            // dgBill[dgBill.CurrentCell.ColumnIndex, dgBill.CurrentCell.RowIndex].Value = txtTotalGrossWt.Text;
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
            //dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.TariffWt].Value =
            pnlGrossWt.Visible = false;
            CalculateTotal();
            dgBill.CurrentCell = dgBill[ColIndex.Quantity, dgBill.CurrentCell.RowIndex];
            dgBill.Focus();
        }

        private void pnlGrossWt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            pnlGrossWt.Visible = false;
        }

        private void label81_Click(object sender, EventArgs e)
        {
        }

        private void dgBill_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value == null) dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = 1;
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
                        if (((ObjQry.ReturnLong("select count(*) from tvoucherentry where vouchertypecode=20 and voucheruserno=" + txtInvNo.Text, CommonFunctions.ConStr)) == 0)) // && (TempBillNo >Convert.ToInt32(txtInvNo.Text)))
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

        private void txtInvNo_TextChanged(object sender, EventArgs e)
        {
            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsManualBillNo)) == true)
            //{
            //    TempBillNo = Convert.ToInt32(txtInvNo.Text);
            //}
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
                        txtChrgRupees2.Enabled = true;

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
            //DataTable tempDt = new DataTable();
            //tempDt = ObjFunction.GetDataView("Select Pkvoucherno from Tvoucherentry where vouchertypecode=15 ").Table;
            //for (int i = 0; i <= tempDt.Rows.Count - 1; i++)
            //{
            //    ID = Convert.ToInt32(tempDt.Rows[i].ItemArray[0]);
            //    FillField();
            //    btnUpdate_Click(sender, e);

            //}
        }

        private void btnCashSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Visible)
            {
                PrintAsk = 0;
                lstPaymentType.SelectedValue = "2";
                btnSave_Click(sender, e);
            }
        }

        private void btnCreditSave_Click(object sender, EventArgs e)
        {
            if ((btnSave.Visible) && (btnCreditSave.Visible))
            {
                PrintAsk = 0;
                lstPaymentType.SelectedValue = "3";

                btnSave_Click(sender, e);
            }
        }

        private void btnMixMode_Click(object sender, EventArgs e)
        {

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
            SendSMS();
        }

        public void SendSMS()
        {
            string MobileNo = txtSendNumber.Text;
            string message = ObjQry.ReturnString("select ScriptData from MSmsTemplate", CommonFunctions.ConStr);

            String BillNo = Convert.ToString(ID);
            String billdate = Convert.ToString(dtpBillDate.Value);
            message = message.Replace("\r\n", "");
            message = message.Replace("<VarCustomerName>", txtParty.Text);
            message = message.Replace("<VarBillNo>", BillNo);
            message = message.Replace("<VarBillAmount>", txtGrandTotal.Text);
            message = message.Replace("<VarBillDate>", billdate);

            //string message = txtsms.Text;
            // use the API URL here  
            string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=sulbhagolekar@gmail.com:estofa@123&senderID=TEST SMS&receipientno=" + MobileNo + "&dcs=0&msgtxt=" + message + "&state=4";
            // Create a request object  
            WebRequest request = HttpWebRequest.Create(strUrl);
            // Get the response back  
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            System.IO.Stream s = (System.IO.Stream)response.GetResponseStream();
            System.IO.StreamReader readStream = new System.IO.StreamReader(s);
            string dataString = readStream.ReadToEnd();
            response.Close();
            s.Close();
            readStream.Close();

            OMMessageBox.Show("SMS Send Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            pnlPhone.Visible = false;
        }

        private void btnSendsmsclose_Click(object sender, EventArgs e)
        {
            pnlPhone.Visible = false;
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            // string Emailid = ObjQry.ReturnString("SELECT MLedger.LedgerNo, MLedgerDetails.EmailID FROM MLedger INNER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo INNER JOIN TVoucherEntry ON MLedger.LedgerNo = TVoucherEntry.LedgerNo Where PkVoucherNo = " + ID + "", CommonFunctions.ConStr);

            string Emailid = ObjQry.ReturnString("SELECT  MLedgerDetails.EmailID FROM  MLedgerDetails  INNER JOIN TVoucherEntry ON MLedgerDetails.LedgerNo = TVoucherEntry.LedgerNo Where PkVoucherNo = " + ID + "", CommonFunctions.ConStr);
            if (Emailid != "")
            {
                txtToEmailid.Text = Emailid;
            }
            else
            {
                txtToEmailid.Text = "";
            }
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

                    "<span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12;\">Dear Sir/Madam, <br/><br/>I have send Sales Invoice  <br/>"

                    + "<br/><b> Thanks & Regards,</b><br/>" + DBGetVal.FirmName + "<br/>" + DBGetVal.CompanyAddress + "<br/> </span>";
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

        private void btnRackWisePrint_Click(object sender, EventArgs e)
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
                            RackWisePrint(0);
                        }
                    }
                    else RackWisePrint(0);
                }
                else if (ds == DialogResult.Cancel)
                    RackWisePrint(1);
            }
        }

        public void RackWisePrint(int PrintType)
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
                ReportSession[6] = "Type: " + ((MixModeVal == 1) ? "Mix Mode" : lstPaymentType.Text);
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
                                    childForm = ObjFunction.LoadReportObject("DCGetBill-A4.rpt", CommonFunctions.ReportPath);
                                }
                                else
                                {
                                    childForm = ObjFunction.LoadReportObject("GetRackWiseBill-a4.rpt", CommonFunctions.ReportPath);
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
                                    childForm = ObjFunction.LoadReportObject("GetDCBoxPrintMar.rpt", CommonFunctions.ReportPath);
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
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DCGetBill-a4.rpt", CommonFunctions.ReportPath), ReportSession);
                                }
                                else
                                {
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetRackWiseBill-a4.rpt", CommonFunctions.ReportPath), ReportSession);
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
                                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetDCBoxPrintMar.rpt", CommonFunctions.ReportPath), ReportSession);
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



    }
}

