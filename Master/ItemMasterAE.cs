using OM;
using OMControls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Yadi.Master
{
    public partial class ItemMasterAE : Form
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

        string ItemNm, ItemShort, BarcodeNm, ShortCodeNm, strParaBarCode = "", Sstockcon = "";
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long ShortID = 0;
        long ID, PkSrNo, GID = 0, SPkSrNo;long OldID = 0;
        bool FlagChange, FlagBilingual, SFlagChange, esflag1;
        double PurRate = 0, MRP = 0, ASaleRate = 0, BSaleRate = 0;
        double MRPC = 0;
        DateTime FromDate, SFromDate;
        long isSGSTSales = 0, isSGSTPur = 0, isCGSTSales = 0, isCGSTPur = 0, isIGSTSales = 0, isIGSTPur = 0, isCessSales = 0, isCessPur = 0;
        double SPurRate = 0, SMRP = 0, SASaleRate = 0, SBSaleRate = 0;
        long tepmGroupno = 0;
        DBMRackDetails dbMRackDetails = new DBMRackDetails();
        MRackDetails mRackDetails = new MRackDetails();
        DataTable dt = new DataTable();

        public ItemMasterAE()
        {
            InitializeComponent();
        }

        public ItemMasterAE(long ShortID)
        {
            InitializeComponent();
            this.ShortID = ShortID;
        }

        public ItemMasterAE(long ShortID, string strParaBarCode)
        {
            InitializeComponent();
            this.ShortID = ShortID;
            this.strParaBarCode = strParaBarCode;
        }

        private void ViewMode(bool flag)
        {
            pnlGroup1.Visible = flag;
            pnlCategory.Visible = flag;
            pnlDepartment.Visible = flag;
            pnlUOML.Visible = flag;
            pnlUOMH.Visible = flag;
            pnlUOMD.Visible = flag;
            pnlSGSTSales.Visible = flag;
            pnlSGSTPur.Visible = flag;
            pnlCGSTSales.Visible = flag;
            pnlCGSTPur.Visible = flag;
            pnlIGSTSales.Visible = flag;
            pnlIGSTPur.Visible = flag;
            pnlCessSales.Visible = flag;
            pnlCessPur.Visible = flag;
            pnlItemType.Visible = flag;
            txtSearchBarCode.Enabled = flag;
            cmbMfgComp.Enabled = flag;
            btnSalesSGST.Enabled = flag;
            btnSaleCGST.Enabled = flag;
            btnSalesIGST.Enabled = flag;
            btnSalesCess.Enabled = flag;
            btnPurSGST.Enabled = flag;
            btnPurCGST.Enabled = flag;
            btnPurIGST.Enabled = flag;
            btnPurCess.Enabled = flag;
        }

        private void FormatPicture()    // code by swati --- to create a listview run time
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                lstGroup1.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
                lstGroup1Lang.Font = ObjFunction.GetLangFont();
                pnlGroup1.Top = txtBrandName.Bottom;
                pnlGroup1.Left = txtBrandName.Left;
                pnlGroup1.Width = txtBrandName.Width + txtBrandName.Width;
                pnlGroup1.Height = 227;

            }
            else
            {
                lstGroup1.Font = ObjFunction.GetFont(FontStyle.Regular, 12);
                pnlGroup1.Left = txtBrandName.Left;
                pnlGroup1.Top = txtBrandName.Bottom;
                pnlGroup1.Width = txtBrandName.Width;
                pnlGroup1.Height = 160;

            }

            if ((ObjQry.ReturnBoolean("select IsType from MFirm ", CommonFunctions.ConStr)) == true)
            {
                groupBox3.Visible = true;
            }
            else
            {
                groupBox3.Visible = false;
                txtAPMCCessPer.Text = "0.00";
                txtAPMCAddCessPer.Text = "0.00";
                txtAPMCDhekhrekPer.Text = "0.00";
                txtAPMCPackingPer.Text = "0.00";
                txtContainerCharges.Text = "0.00";
            }

            lstDepartment.Font = ObjFunction.GetFont(FontStyle.Regular, 12);
            pnlDepartment.Width = txtDepartment.Width;

            pnlDepartment.Top = txtDepartment.Bottom;
            pnlDepartment.Left = txtDepartment.Left;
            pnlDepartment.Height = 200;

            //Cateogory/Product
            lstCategory.Font = ObjFunction.GetFont(FontStyle.Regular, 12);
            pnlCategory.Left = txtCategory.Left;
            pnlCategory.Top = txtCategory.Bottom;
            pnlCategory.Width = txtCategory.Width;
            pnlCategory.Height = 200;

            pnlUOMH.Top = pnlUOML.Top = txtMRP.Top;
            pnlUOMH.Left = pnlUOML.Left = txtMRP.Left;
            pnlUOMH.Width = pnlUOML.Width = txtUOML.Width;
            pnlUOMH.Height = pnlUOML.Height = 80;

            pnlUOMD.Top = txtUOMD.Top;
            pnlUOMD.Left = txtUOMD.Left + 50;
            pnlUOMD.Width = txtUOMD.Width;
            pnlUOMD.Height = 43;

            pnlItemType.Top = txtItemType.Bottom + 10;
            pnlItemType.Left = txtItemType.Left + 5;
            pnlItemType.Width = txtItemType.Width;
            pnlItemType.Height = 70;

            pnlIGSTSales.Top = pnlSGSTSales.Top = pnlCGSTSales.Top = pnlCessSales.Top = txtIGSTPur.Top;
            pnlIGSTSales.Width = pnlSGSTSales.Width = pnlCGSTSales.Width = pnlCessSales.Width = txtIGSTPur.Width;
            pnlIGSTSales.Height = pnlSGSTSales.Height = pnlCGSTSales.Height = pnlCessSales.Height = 100;
            pnlIGSTSales.Left = pnlSGSTSales.Left = pnlCGSTSales.Left = pnlCessSales.Left = txtIGSTPur.Left;

            pnlIGSTPur.Top = pnlSGSTPur.Top = pnlCGSTPur.Top = pnlCessPur.Top = txtIGSTSales.Top;
            pnlIGSTPur.Width = pnlSGSTPur.Width = pnlCGSTPur.Width = pnlCessPur.Width = txtIGSTSales.Width;
            pnlIGSTPur.Height = pnlSGSTPur.Height = pnlCGSTPur.Height = pnlCessPur.Height = 100;
            pnlIGSTPur.Left = pnlSGSTPur.Left = pnlCGSTPur.Left = pnlCessPur.Left = txtIGSTSales.Left;

        }

        private void ItemMasterAE_Load(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    FlagBilingual = true;
                    SetLangFlag(true);
                }
                else
                {
                    FlagBilingual = false;
                    SetLangFlag(false);
                }
                FormatPicture();
                FillListAllMasters();
                KeyDownFormat(this.Controls);
                ItemNm = "";
                ItemShort = "";
                BarcodeNm = "";
                ShortCodeNm = "";
                PkSrNo = 0;
                SPkSrNo = 0;
                lblManufacturerCompanyName.Text = "";
                lblUnderGroupUomName.Text = "";
                PurRate = 0; MRP = 0; ASaleRate = 0; BSaleRate = 0;
                FlagChange = false;
                SFlagChange = false;
                lbluom.Visible = false;
                btnNew.Focus();
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                ViewMode(false);
                dtSearch = ObjFunction.GetDataView("Select  MItemMaster.Itemno From MItemMaster  inner join MItemGroup on MItemGroup.itemgroupno=MItemMaster.groupno  order by itemgroupname ,itemname ").Table;
                if (ShortID == 0)
                {
                    if (dtSearch.Rows.Count > 0)
                    {
                        //================umesh ==07-02-2018
                        if (StockItem.RequestItemNo == 0)
                        {
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                            // ID = Convert.ToInt64(dtSearch.Rows[0]["Itemno"].ToString());
                        }
                        else
                        {
                            ID = StockItem.RequestItemNo;
                        }
                        FillField();
                        SetNavigation();
                    }
                    lblASaleRate.Text = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
                    lblBSaleRate.Text = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
                    btnNew.Focus();
                }
                else
                {
                    lblASaleRate.Text = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
                    lblBSaleRate.Text = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
                    btnNew_Click(sender, e);
                    if (strParaBarCode != "")
                    {
                        txtBarcode.Text = strParaBarCode;

                        //txtBarcode_KeyPress(txtBarcode, new KeyPressEventArgs((char)Keys.Enter));
                    }
                }

                btnPrintBarCode.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_PrintBarCode));
                btnPrintBarCodeAdvance.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_PrintBarCode));

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_PrintBarCode)) == true)
                {
                    btnPrintBarCode.Visible = (ID == 0) ? false : true;
                    btnPrintBarCodeAdvance.Visible = (ID == 0) ? false : true;
                    if (ID != 0)
                    {
                        if (chkActive.Checked == true)
                        {
                            btnPrintBarCode.Visible = true;
                            btnPrintBarCodeAdvance.Visible = true;
                        }
                        else
                        {
                            btnPrintBarCode.Visible = false;
                            btnPrintBarCodeAdvance.Visible = false;
                        }
                    }
                }
              
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void ItemMasterAE_Activated(object sender, EventArgs e)
        {
            try
            {
                txtBrandLang.Font = ObjFunction.GetLangFont();
                txtItemLang.Font = ObjFunction.GetLangFont();
                txtShortLang.Font = ObjFunction.GetLangFont();

                if (StockItem.RequestItemNo != 0)
                {
                    ID = StockItem.RequestItemNo;
                    FillField();
                    StockItem.RequestItemNo = 0;
                }
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void ItemMasterAE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                txtSearchBarCode.Focus();
            }

        }

        private void ItemMasterAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            MRPC = 0;
            ItemNm = "";
            ItemShort = "";
            BarcodeNm = "";
            ShortCodeNm = "";
            PkSrNo = 0;
            SPkSrNo = 0;

        }

        private void ItemMasterAE_Deactivate(object sender, EventArgs e)
        {
            // isDoProcess = true;

        }

        public void SetLangFlag(bool flag)
        {
            txtBrandLang.Visible = flag;
            txtItemLang.Visible = flag;
            txtShortLang.Visible = flag;
            lblStar15.Visible = flag;
            lblStar16.Visible = flag;
            btnItemLang.Visible = flag;
            btnShortLang.Visible = flag;
            lstGroup1Lang.Visible = flag;
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
        }

        private void FillListAllMasters()
        {
            try
            {
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsBrandFilter)) == true)
                {
                    ObjFunction.FillList(lstDepartment, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 ORDER BY ItemGroupName");
                    txtDepartment.Text = lstDepartment.Text.ToString();

                    ObjFunction.FillList(lstCategory, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 ORDER BY ItemGroupName");
                    txtCategory.Text = lstCategory.Text.ToString();
                }
                else
                {
                    lblDepartment.Visible = false;
                    txtDepartment.Visible = false;
                    lstDepartment.Visible = false;
                    lblCategory.Visible = false;
                    txtCategory.Visible = false;
                    lstCategory.Visible = false;

                    lblBarcode.Location = new Point(17, 64);
                    txtBarcode.Location = new Point(152, 60);
                    lblStar1.Location = new Point(457, 60);

                    lblShortCode.Location = new Point(17, 90);
                    txtShortCode.Location = new Point(152, 88);
                    lblStar17.Location = new Point(457, 88);

                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    ObjFunction.FillList(lstGroup1Lang, "SELECT ItemGroupNo,LangGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY ItemGroupName");
                    txtBrandLang.Text = lstGroup1Lang.Text.ToString();
                }
                ObjFunction.FillList(lstGroup1, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY ItemGroupName");
                txtBrandName.Text = lstGroup1.Text.ToString();

                DataTable dt1 = new DataTable();
                dt1.Columns.Add("UomType");
                dt1.Columns.Add("valueid");
                DataRow dr = null;
                dr = dt1.NewRow();
                dr[0] = "Both";
                dr[1] = "3";

                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr[0] = "Purchase";
                dr[1] = "2";

                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr[0] = "Sales";
                dr[1] = "1";

                dt1.Rows.Add(dr);
                lstItemType.DataSource = dt1.DefaultView;
                lstItemType.DisplayMember = dt1.Columns[0].ColumnName;
                lstItemType.ValueMember = dt1.Columns[1].ColumnName;

                ObjFunction.FillList(lstUOML, "SELECT UOMNo,UOMName from MUOM WHERE  isActive='True' and uomno not in(1) ORDER BY UOMName");
                ObjFunction.FillList(lstUOMH, "SELECT UOMNo,UOMName from MUOM WHERE  UOMtype=1 and isActive='True'  ORDER BY UOMName");
                ObjFunction.FillList(lstUOMD, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' or UOMNo in(" + lstUOML.SelectedValue + "," + lstUOMH.SelectedValue + " ) ORDER BY UOMName");
                txtUOMD.Text = lstUOMH.Text.ToString();
                ObjFunction.FillCombo(cmbMfgComp, "SELECT FirmNo, FirmName FROM MFirm  ORDER BY FirmName");
                cmbMfgComp.SelectedValue = 1;

                //if (ID != 0)
                {
                    ObjFunction.FillList(lstSGSTSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 51) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isSGSTSales + " Order by  MItemTaxSetting.Percentage ");
                    lstSGSTSales.SelectedIndex = 0;
                    txtSGSTSales.Text = lstSGSTSales.Text.ToString();


                    ObjFunction.FillList(lstSGSTPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = 51) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isSGSTPur + " Order by  MItemTaxSetting.Percentage ");
                    lstSGSTPur.SelectedIndex = 0;
                    txtSGSTPur.Text = lstSGSTPur.Text.ToString();

                    ObjFunction.FillList(lstCGSTSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 52) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCGSTSales + " Order by  MItemTaxSetting.TaxSettingName ");
                    lstCGSTSales.SelectedIndex = 0;
                    txtCGSTSales.Text = lstCGSTSales.Text.ToString();

                    ObjFunction.FillList(lstCGSTPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = 52) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCGSTPur + " Order by  MItemTaxSetting.TaxSettingName ");
                    lstCGSTPur.SelectedIndex = 0;
                    txtCGSTPur.Text = lstCGSTPur.Text.ToString();

                    ObjFunction.FillList(lstIGSTSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 53) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isIGSTSales + " Order by  MItemTaxSetting.TaxSettingName ");
                    lstIGSTSales.SelectedIndex = 0;
                    txtIGSTSales.Text = lstIGSTSales.Text.ToString();


                    ObjFunction.FillList(lstIGSTPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = 53) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isIGSTPur + " Order by  MItemTaxSetting.TaxSettingName ");
                    lstIGSTPur.SelectedIndex = 0;
                    txtIGSTPur.Text = lstIGSTPur.Text.ToString();

                    ObjFunction.FillList(lstCessSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = " + GroupType.Cess + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCessSales + " Order by  MItemTaxSetting.TaxSettingName ");
                    lstCessSales.SelectedIndex = 0;
                    txtCessSales.Text = lstCessSales.Text.ToString();

                    ObjFunction.FillList(lstCessPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = " + GroupType.Cess + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + isCessPur + " Order by  MItemTaxSetting.TaxSettingName ");
                    lstCessPur.SelectedIndex = 0;
                    txtCessPur.Text = lstCessPur.Text.ToString();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillField()
        {
            try
            {
                if (ID != 0)
                {
                    lbluom.Visible = false;
                    mItemMaster = dbMItemMaster.ModifyMItemMasterByID(ID);
                    txtBarcode.Text = mItemMaster.Barcode;
                    txtShortCode.Text = mItemMaster.ShortCode;
                    txtItemName.Text = mItemMaster.ItemName;
                    txtShortName.Text = mItemMaster.ItemShortName;
                    txtItemLang.Text = mItemMaster.LangFullDesc;
                    txtShortLang.Text = mItemMaster.LangShortDesc;
                    txtHSNCode.Text = mItemMaster.HSNCode;

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsBrandFilter)) == true)
                    {
                        //============Department
                        ObjFunction.FillList(lstDepartment, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4  and ItemGroupNo =" + mItemMaster.FkDepartmentNo + " ORDER BY ItemGroupName");
                        lstDepartment.SelectedValue = mItemMaster.FkDepartmentNo.ToString();
                        lstCategory.SelectedValue = mItemMaster.FkCategoryNo.ToString();

                        txtDepartment.Text = lstDepartment.Text.ToString();
                        //============Category
                        ObjFunction.FillList(lstCategory, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 and ItemGroupNo =" + mItemMaster.FkCategoryNo + " ORDER BY ItemGroupName");
                        txtCategory.Text = lstCategory.Text.ToString();
                    }

                    lstGroup1Lang.SelectedValue = lstGroup1.SelectedValue = mItemMaster.GroupNo.ToString();
                    txtBrandName.Text = lstGroup1.Text;
                    txtBrandLang.Text = lstGroup1Lang.Text;

                    lstUOML.SelectedValue = mItemMaster.UOML.ToString();
                    txtUOML.Text = lstUOML.Text;

                    if (txtUOML.Text == "GRAM")
                    {
                        lbluom.Visible = true;
                    }

                    lstUOMH.SelectedValue = mItemMaster.UOMH.ToString();
                    if (mItemMaster.UOMH == mItemMaster.UOML)
                    {
                        lstUOMH.SelectedValue = 1;
                    }
                    txtUOMH.Text = lstUOMH.Text;

                    lstUOMD.SelectedValue = mItemMaster.UOMDefault.ToString();
                    txtUOMD.Text = lstUOMD.Text;

                    lstItemType.SelectedValue = mItemMaster.ItemType.ToString();
                    txtItemType.Text = lstItemType.Text;

                    chkActive.Checked = mItemMaster.IsActive;
                    chkSlab.Checked = mItemMaster.GSTSlab;
                    esflag1 = mItemMaster.ESFlag;
                    txtContainerCharges.Text = mItemMaster.ContainerCharges.ToString();

                    if (chkActive.Checked == true)
                    {
                        chkActive.Text = "Yes";
                    }
                    else
                    {
                        chkActive.Text = "No";
                    }
                    if (chkSlab.Checked == true)
                    {
                        chkSlab.Text = "Yes";
                    }
                    else
                    {
                        chkSlab.Text = "No";
                    }
                    DataTable dtItemTax = ObjFunction.GetDataView("Select * From dbo.GetItemTaxAll(" + ID + ", NULL, NULL,NULL,NULL)  order by fromdate desc , pksrno desc").Table;
                    if (dtItemTax.Rows.Count != 0)
                    {
                        DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=51");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstSGSTSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = " + GroupType.SGST + " ) And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.Percentage ");
                            isSGSTSales = Convert.ToInt64(dr[0][10].ToString());
                            //   isVATSales = Convert.ToInt64(dr[0][10].ToString());
                            lstSGSTSales.SelectedValue = dr[0][10].ToString();
                            txtSGSTSales.Text = Convert.ToString(lstSGSTSales.Text);
                            //dtpedate.Value = Convert.ToDateTime(dr[0][5].ToString());
                            dtpedate.Value = Convert.ToDateTime(dtItemTax.Rows[0]["FromDate"]);
                        }
                        dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=51");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstSGSTPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = " + GroupType.SGST + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.Percentage ");
                            isSGSTPur = Convert.ToInt64(dr[0][10].ToString());
                            lstSGSTPur.SelectedValue = dr[0][10].ToString();
                            txtSGSTPur.Text = lstSGSTPur.Text;
                        }
                        //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsAllowCST)) == true)
                        // {
                        dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=52");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstCGSTSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = " + GroupType.CGST + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.TaxSettingName ");
                            isCGSTSales = Convert.ToInt64(dr[0][10].ToString());
                            lstCGSTSales.SelectedValue = dr[0][10].ToString();
                            txtCGSTSales.Text = lstCGSTSales.Text;
                        }
                        dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=52");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstCGSTPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = " + GroupType.CGST + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.TaxSettingName ");
                            isCGSTPur = Convert.ToInt64(dr[0][10].ToString());
                            lstCGSTPur.SelectedValue = dr[0][10].ToString();
                            txtCGSTPur.Text = lstCGSTPur.Text;
                        }
                        // }
                        //  if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsAllowCForm)) == true)
                        //{
                        dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=53");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstIGSTSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = " + GroupType.IGST + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.TaxSettingName ");
                            isIGSTSales = Convert.ToInt64(dr[0][10].ToString());
                            lstIGSTSales.SelectedValue = dr[0][10].ToString();
                            txtIGSTSales.Text = lstIGSTSales.Text;
                        }
                        dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=53");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstIGSTPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = " + GroupType.IGST + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.TaxSettingName ");
                            isIGSTPur = Convert.ToInt64(dr[0][10].ToString());
                            lstIGSTPur.SelectedValue = dr[0][10].ToString();
                            txtIGSTPur.Text = lstIGSTPur.Text;
                        }
                        //}
                        dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=" + GroupType.Cess + "");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstCessSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = " + GroupType.Cess + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.TaxSettingName ");
                            isCessSales = Convert.ToInt64(dr[0][10].ToString());
                            lstCessSales.SelectedValue = dr[0][10].ToString();
                            txtCessSales.Text = lstCessSales.Text;
                        }
                        dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=" + GroupType.Cess + "");
                        if (dr.Length > 0)
                        {
                            //ObjFunction.FillList(lstCessPur, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                            //    "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                            //    " WHERE     (MLedger.GroupNo = " + GroupType.PurchaseAccount + ") AND (MLedger_1.GroupNo = " + GroupType.Cess + ") And MItemTaxSetting.IsActive='True' or MItemTaxSetting.PkSrNo=" + dr[0][10].ToString() + " Order by  MItemTaxSetting.TaxSettingName ");
                            isCessPur = Convert.ToInt64(dr[0][10].ToString());
                            lstCessPur.SelectedValue = dr[0][10].ToString();
                            txtCessPur.Text = lstCessPur.Text;
                        }
                    }
                    else
                    {
                        txtIGSTSales.Text = ""; txtSGSTSales.Text = ""; txtCGSTSales.Text = ""; txtCessSales.Text = "";
                        txtIGSTPur.Text = ""; txtSGSTPur.Text = ""; txtCGSTPur.Text = ""; txtCessPur.Text = "";
                    }

                    DataTable dtRtSettingNos = ObjFunction.GetDataView("Select PkSrNo,PurRate,MRP,ASaleRate,BSaleRate,FromDate,StockConversion,MKTQty,Weight1,Weight2,Hamali from dbo.GetItemRateAll(" + ID + "," + (lstUOML.SelectedValue) + ",NULL,NULL,NUll) Order BY FromDate Desc").Table;
                    if (dtRtSettingNos.Rows.Count > 0)
                    {
                        txtPurRate.Text = dtRtSettingNos.Rows[0].ItemArray[1].ToString();
                        txtSMRP.Text = txtMRP.Text = dtRtSettingNos.Rows[0].ItemArray[2].ToString();
                        MRPC = Convert.ToDouble(dtRtSettingNos.Rows[0].ItemArray[2].ToString());
                        txtASaleRate.Text = dtRtSettingNos.Rows[0].ItemArray[3].ToString();
                        txtBSaleRate.Text = dtRtSettingNos.Rows[0].ItemArray[4].ToString();
                        PkSrNo = Convert.ToInt64(dtRtSettingNos.Rows[0].ItemArray[0].ToString());
                        FromDate = Convert.ToDateTime(dtRtSettingNos.Rows[0].ItemArray[5].ToString());

                        MRP = Convert.ToDouble(dtRtSettingNos.Rows[0].ItemArray[2].ToString());
                        PurRate = Convert.ToDouble(dtRtSettingNos.Rows[0].ItemArray[1].ToString());
                        ASaleRate = Convert.ToDouble(dtRtSettingNos.Rows[0].ItemArray[3].ToString());
                        BSaleRate = Convert.ToDouble(dtRtSettingNos.Rows[0].ItemArray[4].ToString());

                        txtStockConv.Text = Convert.ToString(dtRtSettingNos.Rows[0].ItemArray[6].ToString());
                        txtMKTQty.Text = Convert.ToString(dtRtSettingNos.Rows[0].ItemArray[7].ToString());
                        txtWeight1.Text = Convert.ToString(dtRtSettingNos.Rows[0].ItemArray[8].ToString());
                        txtHamali1.Text= Convert.ToString(dtRtSettingNos.Rows[0].ItemArray[10].ToString());
                    }
                    else
                    {
                        txtPurRate.Text = "0.00";
                        txtMRP.Text = "0.00";
                        txtASaleRate.Text = "0.00";
                        txtBSaleRate.Text = "0.00";
                        PkSrNo = 0;
                        MRPC = 0;
                        FromDate = Convert.ToDateTime("01-01-1900");
                        MRP = 0;
                        PurRate = 0;
                        ASaleRate = 0;
                        BSaleRate = 0;
                        txtStockConv.Text = "1";
                        txtMKTQty.Text = "1";
                        txtWeight1.Text = "0.00";
                        txtHamali1.Text = "0.00";
                    }
                    if ((lstUOMH.SelectedValue != null) && (txtUOMH.Text != txtUOML.Text) && (txtUOMH.Text != "  NA"))
                    {
                        DataTable dtSRtSettingNos = ObjFunction.GetDataView("Select PkSrNo,PurRate,MRP,ASaleRate,BSaleRate,FromDate,StockConversion,MKTQty,Weight1,Weight2,Hamali from dbo.GetItemRateAll(" + ID + "," + (lstUOMH.SelectedValue) + ",NULL,NULL,NUll) Order BY FromDate Desc").Table;
                        if (dtSRtSettingNos.Rows.Count > 0)
                        {
                            pnlSUOM.Visible = true;
                            txtSPurRate.Text = dtSRtSettingNos.Rows[0].ItemArray[1].ToString();
                            txtSMRP.Text = dtSRtSettingNos.Rows[0].ItemArray[2].ToString();
                            MRPC = Convert.ToDouble(dtSRtSettingNos.Rows[0].ItemArray[2].ToString());
                            txtSASaleRate.Text = dtSRtSettingNos.Rows[0].ItemArray[3].ToString();
                            txtSBSaleRate.Text = dtSRtSettingNos.Rows[0].ItemArray[4].ToString();

                            SPkSrNo = Convert.ToInt64(dtSRtSettingNos.Rows[0].ItemArray[0].ToString());
                            SFromDate = Convert.ToDateTime(dtSRtSettingNos.Rows[0].ItemArray[5].ToString());

                            SMRP = Convert.ToDouble(dtSRtSettingNos.Rows[0].ItemArray[2].ToString());
                            SPurRate = Convert.ToDouble(dtSRtSettingNos.Rows[0].ItemArray[1].ToString());
                            SASaleRate = Convert.ToDouble(dtSRtSettingNos.Rows[0].ItemArray[3].ToString());
                            SBSaleRate = Convert.ToDouble(dtSRtSettingNos.Rows[0].ItemArray[4].ToString());
                            txtSStockConv.Text = Convert.ToString(dtSRtSettingNos.Rows[0].ItemArray[6].ToString());
                            Sstockcon = txtSStockConv.Text;
                            txtSMKTQty.Text = Convert.ToString(dtSRtSettingNos.Rows[0].ItemArray[7].ToString());
                            txtUOMD.Text = ObjQry.ReturnString("Select Uomname from muom where uomno in (select UOMDefault  from MItemMaster where itemno =" + ID + ") ", CommonFunctions.ConStr);
                            lblKg.Text = "1" + " " + txtUOMH.Text + " " + "=";
                            label17.Text = txtUOML.Text;
                            txtWeight2.Text = Convert.ToString(dtSRtSettingNos.Rows[0].ItemArray[9].ToString());
                            txtHamali2.Text = Convert.ToString(dtSRtSettingNos.Rows[0].ItemArray[10].ToString());
                        }
                        else
                        {
                            pnlSUOM.Visible = false;
                            txtSPurRate.Text = "0.00";
                            txtSMRP.Text = "0.00";
                            txtSASaleRate.Text = "0.00";
                            txtSBSaleRate.Text = "0.00";
                            SPkSrNo = 0;
                            SFromDate = Convert.ToDateTime("01-01-1900");
                            SMRP = 0;
                            SPurRate = 0;
                            SASaleRate = 0;
                            SBSaleRate = 0;
                            txtSStockConv.Text = "1";
                            txtSMKTQty.Text = "1";
                            txtWeight2.Text = "0.00";
                            txtHamali2.Text = "0.00";
                        }
                    }
                    else
                    {
                        pnlSUOM.Visible = false;
                        txtSPurRate.Text = "0.00";
                        txtSMRP.Text = "0.00";
                        txtSASaleRate.Text = "0.00";
                        txtSBSaleRate.Text = "0.00";
                        SPkSrNo = 0;
                        SFromDate = Convert.ToDateTime("01-01-1900");
                        SMRP = 0;
                        SPurRate = 0;
                        SASaleRate = 0;
                        SBSaleRate = 0;
                        txtSStockConv.Text = "1";
                        txtSMKTQty.Text = "1";
                        txtWeight2.Text = "0.00";
                        txtHamali2.Text = "0.00";
                    }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_PrintBarCode)) == true)
                    {
                        btnPrintBarCode.Visible = (ID == 0) ? false : true;
                        btnPrintBarCodeAdvance.Visible = (ID == 0) ? false : true;
                        if (ID != 0)
                        {
                            if (chkActive.Checked == true)
                            {
                                btnPrintBarCode.Visible = true;
                                btnPrintBarCodeAdvance.Visible = true;
                            }
                            else
                            {
                                btnPrintBarCode.Visible = false;
                                btnPrintBarCodeAdvance.Visible = false;
                            }
                        }
                    }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowAPMCDetails)) == true)
                    {
                        groupBox3.Visible = true;
                    }
                    else { groupBox3.Visible = false; }
                    // if (groupBox3.Visible == true)
                    {
                        txtAPMCCessPer.Text = Convert.ToString(mItemMaster.CessValue);
                        txtAPMCAddCessPer.Text = Convert.ToString(mItemMaster.OtherCharges);
                        txtAPMCDhekhrekPer.Text = Convert.ToString(mItemMaster.Dhekhrek);
                        txtAPMCPackingPer.Text = Convert.ToString(mItemMaster.PackagingCharges);
                        txtContainerCharges.Text = Convert.ToString(mItemMaster.ContainerCharges);
                    }
                    DisplayCount();
                }
                else
                {
                    txtIGSTSales.Text = ""; txtSGSTSales.Text = ""; txtCGSTSales.Text = ""; txtCessSales.Text = "";
                    txtIGSTPur.Text = ""; txtSGSTPur.Text = ""; txtCGSTPur.Text = ""; txtCessPur.Text = "";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool ValidationRate()
        {
            bool flag = true;
            try
            {
                EP.SetError(txtPurRate, "");
                EP.SetError(txtASaleRate, "");
                EP.SetError(txtBSaleRate, "");
                EP.SetError(txtMRP, "");

                if (txtMRP.Text.Trim() == "")
                {
                    if (txtMRP.Text.Trim() == "0.00")
                    {
                        txtASaleRate.Focus();
                    }
                    else
                    {
                        EP.SetError(txtMRP, "Enter MRP");
                        EP.SetIconAlignment(txtMRP, ErrorIconAlignment.MiddleRight);
                        if (flag) { flag = false; txtMRP.Focus(); }
                    }
                }
                else if (txtASaleRate.Text.Trim() == "")
                {
                    EP.SetError(txtASaleRate, "Enter " + ObjFunction.GetAppSettings(AppSettings.ARateLabel) + " Rate");
                    EP.SetIconAlignment(txtASaleRate, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtASaleRate.Focus(); }
                }
                else if (txtBSaleRate.Text.Trim() == "")
                {
                    EP.SetError(txtBSaleRate, "Enter " + ObjFunction.GetAppSettings(AppSettings.BRateLabel) + " Rate");
                    EP.SetIconAlignment(txtBSaleRate, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtBSaleRate.Focus(); }
                }
                else if (txtPurRate.Text.Trim() == "")
                {
                    EP.SetError(txtPurRate, "Enter PurRate");
                    EP.SetIconAlignment(txtPurRate, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtPurRate.Focus(); }
                }
                if (flag == true)
                {
                    if (ObjFunction.CheckValidAmount(txtASaleRate.Text.Trim()) == false)
                    {
                        EP.SetError(txtASaleRate, "Enter Valid " + ObjFunction.GetAppSettings(AppSettings.ARateLabel) + " Rate");
                        EP.SetIconAlignment(txtASaleRate, ErrorIconAlignment.MiddleRight);
                        txtASaleRate.Focus(); flag = false;
                    }
                    else if (ObjFunction.CheckValidAmount(txtBSaleRate.Text.Trim()) == false)
                    {
                        EP.SetError(txtBSaleRate, "Enter Valid " + ObjFunction.GetAppSettings(AppSettings.BRateLabel) + " Rate");
                        EP.SetIconAlignment(txtBSaleRate, ErrorIconAlignment.MiddleRight);
                        txtBSaleRate.Focus(); flag = false;
                    }
                    else if (ObjFunction.CheckValidAmount(txtMRP.Text.Trim()) == false)
                    {
                        EP.SetError(txtMRP, "Enter Valid MRP");
                        EP.SetIconAlignment(txtMRP, ErrorIconAlignment.MiddleRight);
                        txtMRP.Focus(); flag = false;
                    }
                    else if (ObjFunction.CheckValidAmount(txtPurRate.Text.Trim()) == false)
                    {
                        EP.SetError(txtPurRate, "Enter Valid PurRate");
                        EP.SetIconAlignment(txtPurRate, ErrorIconAlignment.MiddleRight);
                        txtPurRate.Focus(); flag = false;
                    }
                    else
                        flag = true;
                }
                if (flag)
                    flag = ValidationSRate();

                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        public bool ValidationSRate()
        {
            bool flag = true;
            try
            {
                EP.SetError(txtSPurRate, "");
                EP.SetError(txtSASaleRate, "");
                EP.SetError(txtSBSaleRate, "");
                EP.SetError(txtSMRP, "");
                EP.SetError(txtSMKTQty, "");

                if (txtSMRP.Text.Trim() != "" && Convert.ToDouble(txtSMRP.Text.Trim()) != 0)
                {
                    if (txtSASaleRate.Text.Trim() == "")
                    {
                        EP.SetError(txtSASaleRate, "Enter " + ObjFunction.GetAppSettings(AppSettings.ARateLabel) + " Rate");
                        EP.SetIconAlignment(txtSASaleRate, ErrorIconAlignment.MiddleRight);
                        if (flag) { flag = false; txtSASaleRate.Focus(); }
                    }
                    else if (txtSBSaleRate.Text.Trim() == "")
                    {
                        EP.SetError(txtSBSaleRate, "Enter " + ObjFunction.GetAppSettings(AppSettings.BRateLabel) + " Rate");
                        EP.SetIconAlignment(txtSBSaleRate, ErrorIconAlignment.MiddleRight);
                        if (flag) { flag = false; txtSBSaleRate.Focus(); }
                    }
                    else if (txtSPurRate.Text.Trim() == "")
                    {
                        EP.SetError(txtSPurRate, "Enter PurRate");
                        EP.SetIconAlignment(txtSPurRate, ErrorIconAlignment.MiddleRight);
                        if (flag) { flag = false; txtSPurRate.Focus(); }
                    }
                    if (flag == true)
                    {
                        if (ObjFunction.CheckValidAmount(txtSASaleRate.Text.Trim()) == false)
                        {
                            EP.SetError(txtSASaleRate, "Enter Valid " + ObjFunction.GetAppSettings(AppSettings.ARateLabel) + " Rate");
                            EP.SetIconAlignment(txtSASaleRate, ErrorIconAlignment.MiddleRight);
                            txtSASaleRate.Focus(); flag = false;
                        }
                        else if (ObjFunction.CheckValidAmount(txtSBSaleRate.Text.Trim()) == false)
                        {
                            EP.SetError(txtSBSaleRate, "Enter Valid " + ObjFunction.GetAppSettings(AppSettings.BRateLabel) + " Rate");
                            EP.SetIconAlignment(txtSBSaleRate, ErrorIconAlignment.MiddleRight);
                            txtSBSaleRate.Focus(); flag = false;
                        }
                        else if (ObjFunction.CheckValidAmount(txtSMRP.Text.Trim()) == false)
                        {
                            EP.SetError(txtSMRP, "Enter Valid MRP");
                            EP.SetIconAlignment(txtSMRP, ErrorIconAlignment.MiddleRight);
                            txtSMRP.Focus(); flag = false;
                        }
                        else if (ObjFunction.CheckValidAmount(txtSPurRate.Text.Trim()) == false)
                        {
                            EP.SetError(txtSPurRate, "Enter Valid PurRate");
                            EP.SetIconAlignment(txtSPurRate, ErrorIconAlignment.MiddleRight);
                            txtSPurRate.Focus(); flag = false;
                        }
                        else if (txtSMKTQty.Text.Trim() == "")
                        {
                            EP.SetError(txtSMKTQty, "Enter Mkt Qty");
                            EP.SetIconAlignment(txtSMKTQty, ErrorIconAlignment.MiddleRight);
                            txtSMKTQty.Focus(); flag = false;
                        }
                        else if (Convert.ToDouble(txtMKTQty.Text) == 0)
                        {
                            EP.SetError(txtSMKTQty, "Enter Valid Mkt Qty");
                            EP.SetIconAlignment(txtSMKTQty, ErrorIconAlignment.MiddleRight);
                            txtSMKTQty.Focus(); flag = false;
                        }
                        else
                            flag = true;
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                //nw = 0;
                //  ShortID = ID;
                OldID = ID;
                ID = 0;
                MRPC = 0;
                mItemMaster = new MItemMaster();
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                isSGSTSales = 0;
                isSGSTPur = 0;
                isCGSTSales = 0;
                isCGSTPur = 0;
                isIGSTSales = 0;
                isIGSTPur = 0;
                isCessSales = 0;
                isCessPur = 0;
                BarcodeNm = "";
                ItemNm = "";
                ShortCodeNm = "";
                PkSrNo = 0;
                txtPurRate.Text = "0.00";
                txtASaleRate.Text = "0.00";
                txtBSaleRate.Text = "0.00";
                txtMRP.Text = "0.00";

                txtSASaleRate.Text = "0.00";
                txtSBSaleRate.Text = "0.00";
                txtSPurRate.Text = "0.00";
                txtSMRP.Text = "0.00";

                txtWeight1.Text = "0.00";
                txtWeight2.Text = "0.00";
                txtContainerCharges.Text = "";

                SPkSrNo = 0;
                PurRate = 0; MRP = 0; ASaleRate = 0; BSaleRate = 0;
                chkActive.Checked = true;
                txtSearchBarCode.Enabled = false;
                txtBarcode.Focus();

                FlagChange = false;
                SFlagChange = false;
                FromDate = Convert.ToDateTime("01-01-1900");
                SFromDate = Convert.ToDateTime("01-01-1900");
                FromDate = Convert.ToDateTime(dtpedate.Value);
                SFromDate = Convert.ToDateTime(dtpedate.Value);
                btnNewBrand.Visible = true;
                btnSalesIGST.Enabled = true;
                btnPurIGST.Enabled = true;
                btnPurCess.Enabled = true;
                btnSalesCess.Enabled = true;
                chkSlab.Checked = false;
                txtHamali1.Text = "0.0";
                txtHamali2.Text = "0.0";
                // if (groupBox3.Visible == true)
                {
                    txtAPMCCessPer.Text = "0";
                    txtAPMCAddCessPer.Text = "0";
                    txtAPMCDhekhrekPer.Text = "0";
                    txtAPMCPackingPer.Text = "0";
                    txtContainerCharges.Text = "0";
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    ObjFunction.FillList(lstGroup1Lang, "SELECT ItemGroupNo,LangGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY ItemGroupName");

                }
                ObjFunction.FillList(lstGroup1, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY ItemGroupName");

                long tempUom = ObjQry.ReturnLong("Select UomNo from MUom where UomName='Nos'", CommonFunctions.ConStr);


                lblManufacturerCompanyName.Visible = false;

                GID = 0;
                txtStockConv.Text = "1";
                txtMKTQty.Text = "1";
                txtSStockConv.Text = "1";
                txtSMKTQty.Text = "1";
                pnlSUOM.Visible = false;
                txtHSNCode.Text = "0";
                lbluom.Visible = false;
                if (cmbMfgComp.Items.Count >= 2)
                    cmbMfgComp.SelectedIndex = 1;

                // dgSelectComp.Enabled = true;

                lstItemType.SelectedValue = 3;
                txtItemType.Text = lstItemType.Text;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            StockItem.RequestItemNo = 0;
            this.Close();
        }

        public void SaveFields()
        {
            try
            {
                if (Validations() == true)
                {
                    dbMItemMaster = new DBMItemMaster();
                    mItemMaster = new MItemMaster();
                    mItemMaster.ItemNo = ID;
                    mItemMaster.ItemName = txtItemName.Text.Trim().ToUpper();
                    mItemMaster.ItemShortName = txtShortName.Text.Trim().ToUpper();

                    mItemMaster.GroupNo = ObjFunction.GetListValue(lstGroup1);//Brand Name
                                                                              // mItemMaster.FkDepartmentNo = 1;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsBrandFilter)) == true)
                    {
                        mItemMaster.FkDepartmentNo = ObjFunction.GetListValue(lstDepartment);
                        mItemMaster.FkCategoryNo = ObjFunction.GetListValue(lstCategory);
                    }
                    else
                    {
                        mItemMaster.FkDepartmentNo = 1;
                    }
                    mItemMaster.UOML = Convert.ToInt32(lstUOML.SelectedValue);
                    if (lstUOMH.Text == "  NA")
                    {
                        mItemMaster.UOMH = Convert.ToInt32(lstUOML.SelectedValue);
                        mItemMaster.UOMDefault = ObjFunction.GetListValue(lstUOML);
                    }
                    else
                    {
                        mItemMaster.UOMH = Convert.ToInt32(lstUOMH.SelectedValue);
                        mItemMaster.UOMDefault = ObjFunction.GetListValue(lstUOMD);
                    }
                    mItemMaster.CompanyNo = DBGetVal.FirmNo;

                    mItemMaster.LangFullDesc = txtItemLang.Text.Trim();
                    mItemMaster.LangShortDesc = txtShortLang.Text.Trim();

                    mItemMaster.IsActive = chkActive.Checked;

                    mItemMaster.UserId = DBGetVal.UserID;
                    mItemMaster.UserDate = DBGetVal.ServerTime.Date;
                    mItemMaster.ShortCode = txtShortCode.Text.Trim();
                    mItemMaster.Barcode = txtBarcode.Text.Trim();

                    mItemMaster.HSNCode = txtHSNCode.Text;
                    mItemMaster.CessValue = Convert.ToDouble(txtAPMCCessPer.Text);
                    mItemMaster.OtherCharges = Convert.ToDouble(txtAPMCAddCessPer.Text);
                    mItemMaster.Dhekhrek = Convert.ToDouble(txtAPMCDhekhrekPer.Text);
                    mItemMaster.PackagingCharges = Convert.ToDouble(txtAPMCPackingPer.Text);
                    mItemMaster.ItemType = Convert.ToInt32(lstItemType.SelectedValue);
                    if (ID == 0)
                    {
                        if (DBGetVal.KachhaFirm == true)
                        {
                            mItemMaster.ESFlag = true;
                        }
                        else
                        {
                            mItemMaster.ESFlag = false;
                        }
                    }
                    else
                    {
                        mItemMaster.ESFlag = esflag1;//mItemMaster.ESFlag;
                    }
                    mItemMaster.FKStockGroupTypeNo = 1;
                    mItemMaster.ContainerCharges = Convert.ToDouble(txtContainerCharges.Text);
                    mItemMaster.GSTSlab = chkSlab.Checked;
                    dbMItemMaster.AddMItemMaster(mItemMaster);

                    mRateSetting = new MRateSetting();
                    if (FlagChange == true && ID == 0)
                    {
                        mRateSetting.PkSrNo = 0;
                        mRateSetting.FromDate = dtpedate.Value;// Convert.ToDateTime("01-01-1900");UMESH==14-11-18
                    }
                    else if (FlagChange == true && ID != 0)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCloseMRPManually)) == true)
                        {
                            mRateSetting.PkSrNo = PkSrNo;
                            mRateSetting.FromDate = dtpedate.Value;// FromDate;
                            //mRateSetting.IsActive = false;
                            //dbMItemMaster.UpdateMRateSetting(mRateSetting);
                        }
                        else
                        {
                            // mRateSetting = new MRateSetting();
                            mRateSetting.PkSrNo = 0;
                            mRateSetting.FromDate = dtpedate.Value;// Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD).ToString()).Date;
                        }
                    }
                    else if (FlagChange == false)
                    {
                        mRateSetting.PkSrNo = PkSrNo;
                        mRateSetting.FromDate = dtpedate.Value;
                    }
                    mRateSetting.PurRate = Convert.ToDouble(txtPurRate.Text.Trim());
                    mRateSetting.UOMNo = ObjFunction.GetListValue(lstUOML);
                    mRateSetting.ASaleRate = Convert.ToDouble(txtASaleRate.Text.Trim());
                    mRateSetting.BSaleRate = Convert.ToDouble(txtBSaleRate.Text.Trim());
                    mRateSetting.CSaleRate = 0; // Convert.ToDouble(txtNosMRP.Text.Trim());
                    mRateSetting.DSaleRate = 0; // Convert.ToDouble(txtNosMRP.Text.Trim());
                    mRateSetting.ESaleRate = 0; // Convert.ToDouble(txtNosMRP.Text.Trim());
                    mRateSetting.StockConversion = 1;
                    mRateSetting.PerOfRateVariation = 1;
                    mRateSetting.MKTQty = ((txtMKTQty.Text.Trim() == "") ? 1 : Convert.ToInt64(txtMKTQty.Text));
                    mRateSetting.MRP = Convert.ToDouble(txtMRP.Text.Trim());
                    mRateSetting.IsActive = true;
                    mRateSetting.UserID = DBGetVal.UserID;
                    mRateSetting.UserDate = DBGetVal.ServerTime.Date;
                    mRateSetting.CompanyNo = DBGetVal.FirmNo;
                    mRateSetting.Weight1 = Convert.ToDouble(txtWeight1.Text);
                    mRateSetting.Weight2 = Convert.ToDouble(txtWeight2.Text);
                    mRateSetting.LPPerc = 0; //Jugal
                    mRateSetting.SPPerc = 0; //Jugal  
                    mRateSetting.Hamali = Convert.ToDouble(((txtHamali1.Text == "") ? "0" : txtHamali1.Text));//priyanka

                    dbMItemMaster.AddMRateSetting2(mRateSetting);

                    if ((txtUOMH.Text != "") && (txtUOMH.Text != "  NA") && (txtUOMH.Text != txtUOML.Text) && (lstUOMH.Text != "  NA"))
                    {
                        mRateSetting = new MRateSetting();
                        bool flag = false;
                        if (SFlagChange == true && ID == 0)
                        {
                            if (ObjFunction.GetListValue(lstUOMH) != 0 && txtSMRP.Text.Trim() != "")
                            {
                                mRateSetting.PkSrNo = 0;
                                mRateSetting.FromDate = dtpedate.Value;// Convert.ToDateTime("01-01-1900");UMESH--14-11-18
                                flag = true;
                            }
                        }
                        else if (SFlagChange == true && ID != 0)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCloseMRPManually)) == false)
                            {
                                mRateSetting.PkSrNo = SPkSrNo;
                                mRateSetting.FromDate = dtpedate.Value;// FromDate;
                                //mRateSetting.IsActive = false;
                                //dbMItemMaster.UpdateMRateSetting(mRateSetting);
                            }
                            else
                            {

                                // mRateSetting = new MRateSetting();
                                mRateSetting.PkSrNo = 0;
                                mRateSetting.FromDate = dtpedate.Value; //Convert.ToDateTime("01-01-1900");
                            }
                            if (ObjFunction.GetListValue(lstUOMH) != 0 && txtSMRP.Text.Trim() != "")
                                flag = true;
                        }
                        else if (SFlagChange == false)
                        {
                            if (txtUOMH.Text != "  NA" && txtSMRP.Text.Trim() != "")
                            {
                                mRateSetting.PkSrNo = SPkSrNo;
                                mRateSetting.FromDate = dtpedate.Value;
                                flag = true;
                            }
                        }
                        else if (txtUOMH.Text == "")
                        {
                            flag = false;
                        }
                        if (flag)
                        {
                            mRateSetting.PurRate = Convert.ToDouble(txtSPurRate.Text.Trim());
                            mRateSetting.UOMNo = ObjFunction.GetListValue(lstUOMH);
                            mRateSetting.ASaleRate = Convert.ToDouble(txtSASaleRate.Text.Trim());
                            mRateSetting.BSaleRate = Convert.ToDouble(txtSBSaleRate.Text.Trim());
                            mRateSetting.CSaleRate = 0; // Convert.ToDouble(txtNosMRP.Text.Trim());
                            mRateSetting.DSaleRate = 0; // Convert.ToDouble(txtNosMRP.Text.Trim());
                            mRateSetting.ESaleRate = 0; // Convert.ToDouble(txtNosMRP.Text.Trim());
                            mRateSetting.StockConversion = Convert.ToDouble(((txtSStockConv.Text == "") ? "1" : txtSStockConv.Text));
                            mRateSetting.PerOfRateVariation = 1;
                            mRateSetting.MKTQty = Convert.ToInt64(((txtSMKTQty.Text == "") ? "1" : txtSMKTQty.Text));
                            mRateSetting.MRP = Convert.ToDouble(txtSMRP.Text.Trim());
                            mRateSetting.IsActive = true;
                            mRateSetting.UserID = DBGetVal.UserID;
                            mRateSetting.UserDate = DBGetVal.ServerTime.Date;
                            mRateSetting.CompanyNo = DBGetVal.FirmNo;
                            mRateSetting.Weight1 = Convert.ToDouble(((txtWeight1.Text == "") ? "0" : txtWeight1.Text)); //Convert.ToDouble(txtWeight1.Text);
                            mRateSetting.Weight2 = Convert.ToDouble(((txtWeight2.Text == "") ? "0" : txtWeight2.Text));// Convert.ToDouble(txtWeight2.Text);
                            mRateSetting.LPPerc = 0; //Jugal
                            mRateSetting.SPPerc = 0; //Jugal  
                            mRateSetting.Hamali = Convert.ToDouble(((txtHamali2.Text == "") ? "0" : txtHamali2.Text));// Convert.ToDouble(txtHamali2.Text);   //Priyanka  
                            dbMItemMaster.AddMRateSetting2(mRateSetting);
                        }
                    }
                    DataTable dtItemTax = ObjFunction.GetDataView("Select * From dbo.GetItemTaxAll(" + ID + ", NULL, NULL,NULL,NULL)").Table;
                    if ((dtItemTax.Rows.Count != 0) || (chkSlab.Checked == false))
                    {
                        DataTable dtMainItemTax = ObjFunction.GetDataView("Select * From MItemTaxSetting Where   PkSrNo in (" + Convert.ToInt32(lstIGSTSales.SelectedValue) + "," + Convert.ToInt32(lstIGSTPur.SelectedValue) + "," + Convert.ToInt32(lstSGSTSales.SelectedValue) + "," + Convert.ToInt32(lstSGSTPur.SelectedValue) + "," + Convert.ToInt32(lstCGSTSales.SelectedValue) + "," + Convert.ToInt32(lstCGSTPur.SelectedValue) + "," + Convert.ToInt32(lstCessSales.SelectedValue) + "," + Convert.ToInt32(lstCessPur.SelectedValue) + ")").Table;
                        //SGSTSales
                        if (Convert.ToInt32(lstSGSTSales.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstSGSTSales.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=51");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstSGSTSales.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            //mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }

                        //SGSTPurchase
                        if (Convert.ToInt32(lstSGSTPur.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstSGSTPur.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=51");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstSGSTPur.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            // mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }

                        //CGSTSales
                        if (Convert.ToInt32(lstCGSTSales.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstCGSTSales.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=52");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstCGSTSales.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            //  mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                        //CGSTPurchase
                        if (Convert.ToInt32(lstCGSTPur.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstCGSTPur.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=52");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstCGSTPur.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            //  mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                        //CessSales
                        if (Convert.ToInt32(lstCessSales.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstCessSales.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=54");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstCessSales.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            //  mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                        //CessPurchase
                        if (Convert.ToInt32(lstCessPur.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstCessPur.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=54");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstCessPur.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            // mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                        //IGSTSales
                        if (Convert.ToInt32(lstIGSTSales.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstIGSTSales.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=53");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstIGSTSales.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            //mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }

                        //IGSTPurchase
                        if (Convert.ToInt32(lstIGSTPur.SelectedValue) != 0)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            DataRow[] drMain = dtMainItemTax.Select("PkSrNo=" + Convert.ToInt32(lstIGSTPur.SelectedValue) + "");
                            DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=53");

                            if (dr.Length > 0)
                            {
                                if (DBGetVal.ServerTime.Date != Convert.ToDateTime(dr[0][4].ToString()).Date)
                                {
                                    if (Convert.ToInt64(dr[0][10].ToString()) == Convert.ToInt32(lstIGSTPur.SelectedValue))
                                        mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                                    else
                                        mItemTaxInfo.PkSrNo = 0;
                                }
                                else
                                    mItemTaxInfo.PkSrNo = Convert.ToInt64(dr[0][0].ToString());
                            }
                            else
                            {
                                mItemTaxInfo.PkSrNo = 0;
                            }
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                            // mItemTaxInfo.FromDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                    }
                    else
                    {
                        FillTaxDetails(Convert.ToDouble(5));
                        DataTable dtMainItemTax = ObjFunction.GetDataView("Select * From MItemTaxSetting Where   PkSrNo in (" + Convert.ToInt32(lstIGSTSales.SelectedValue) + "," + Convert.ToInt32(lstIGSTPur.SelectedValue) + "," + Convert.ToInt32(lstSGSTSales.SelectedValue) + "," + Convert.ToInt32(lstSGSTPur.SelectedValue) + "," + Convert.ToInt32(lstCGSTSales.SelectedValue) + "," + Convert.ToInt32(lstCGSTPur.SelectedValue) + "," + Convert.ToInt32(lstCessSales.SelectedValue) + "," + Convert.ToInt32(lstCessPur.SelectedValue) + ")").Table;
                        for (int i = 0; i < dtMainItemTax.Rows.Count; i++)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            mItemTaxInfo.PkSrNo = 0;
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(dtMainItemTax.Rows[i][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(dtMainItemTax.Rows[i][3].ToString());

                        mItemTaxInfo.CalculationMethod = "2";
                        mItemTaxInfo.Percentage = Convert.ToDouble(dtMainItemTax.Rows[i][4].ToString());
                        mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(dtMainItemTax.Rows[i][0].ToString());
                        mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxInfo.UserID = DBGetVal.UserID;
                        mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                        mItemTaxInfo.FromDate = dtpedate.Value;
                        dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                        FillTaxDetails(Convert.ToDouble(12));
                        dtMainItemTax = null;
                         dtMainItemTax = ObjFunction.GetDataView("Select * From MItemTaxSetting Where   PkSrNo in (" + Convert.ToInt32(lstIGSTSales.SelectedValue) + "," + Convert.ToInt32(lstIGSTPur.SelectedValue) + "," + Convert.ToInt32(lstSGSTSales.SelectedValue) + "," + Convert.ToInt32(lstSGSTPur.SelectedValue) + "," + Convert.ToInt32(lstCGSTSales.SelectedValue) + "," + Convert.ToInt32(lstCGSTPur.SelectedValue) + "," + Convert.ToInt32(lstCessSales.SelectedValue) + "," + Convert.ToInt32(lstCessPur.SelectedValue) + ")").Table;
                        for (int i = 0; i < dtMainItemTax.Rows.Count; i++)
                        {
                            MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                            mItemTaxInfo.PkSrNo = 0;
                            mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(dtMainItemTax.Rows[i][2].ToString());
                            mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(dtMainItemTax.Rows[i][3].ToString());

                            mItemTaxInfo.CalculationMethod = "2";
                            mItemTaxInfo.Percentage = Convert.ToDouble(dtMainItemTax.Rows[i][4].ToString());
                            mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(dtMainItemTax.Rows[i][0].ToString());
                            mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                            mItemTaxInfo.UserID = DBGetVal.UserID;
                            mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                            mItemTaxInfo.FromDate = dtpedate.Value;
                            dbMItemMaster.AddMItemTaxInfo1(mItemTaxInfo);
                        }
                    }

                    // if (dbMItemMaster.ExecuteNonQueryStatements() == true)===umesh
                    long tempid = (dbMItemMaster.ExecuteNonQueryStatements1());
                    if (tempid != 0)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show(" Item Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select  MItemMaster.Itemno From MItemMaster  inner join MItemGroup on MItemGroup.itemgroupno=MItemMaster.groupno  order by itemgroupname ,itemname ").Table;
                            if (ShortID == 0)
                            {
                                ID = tempid;
                                FillField();
                            }
                            else
                            {
                                ShortID = ID;
                                for (int i = 0; i < 3; i++)
                                {
                                    System.Threading.Thread.Sleep(1000);
                                    Application.DoEvents();
                                }
                                this.Close();
                            }
                        }
                        else
                        {
                            OMMessageBox.Show(" Item Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                            //  NavigationDisplay(2);
                        }

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        btnItemLang.Enabled = false;
                        btnShortLang.Enabled = false;
                        btnSalesSGST.Enabled = false;
                        btnPurSGST.Enabled = false;
                        btnSaleCGST.Enabled = false;
                        btnPurCGST.Enabled = false;
                        btnSalesIGST.Enabled = false;
                        btnPurIGST.Enabled = false;
                        btnPurCess.Enabled = false;
                        btnSalesCess.Enabled = false;
                        pnlDepartment.Visible = false;
                        pnlCategory.Visible = false;
                        setError();
                        txtSearchBarCode.Enabled = true;
                        //btnNew_Click(new object(), new EventArgs());
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show(" Item not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
            bool flag = true;
            try
            {


                if (txtShortCode.Text.Trim() == "")
                {
                    EP.SetError(txtShortCode, "Enter Short Code");
                    EP.SetIconAlignment(txtShortCode, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtShortCode.Focus(); }
                }
                if (txtShortName.Text.Trim() == "")
                {
                    EP.SetError(txtShortName, "Enter ShortName Code");
                    EP.SetIconAlignment(txtShortName, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtShortName.Focus(); }
                }
                if (txtBrandName.Text.Trim() == "")
                {
                    EP.SetError(txtBrandName, "Select Brand Name");
                    EP.SetIconAlignment(txtBrandName, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtBrandName.Focus(); }
                }
                if (txtUOML.Text.Trim() == "")
                {
                    EP.SetError(txtUOML, "Select UOM Name");
                    EP.SetIconAlignment(txtUOML, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtUOML.Focus(); }
                }
                if (txtSGSTSales.Text.Trim() == "")
                {
                    EP.SetError(txtSGSTSales, "Select SGST");
                    EP.SetIconAlignment(txtSGSTSales, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtSGSTSales.Focus(); }
                }
                if (txtSGSTPur.Text.Trim() == "")
                {
                    EP.SetError(txtSGSTPur, "Select SGST%");
                    EP.SetIconAlignment(txtSGSTPur, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtSGSTPur.Focus(); }
                }
                if (txtCGSTSales.Text.Trim() == "")
                {
                    EP.SetError(txtCGSTSales, "Select CGST%");
                    EP.SetIconAlignment(txtCGSTSales, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtCGSTSales.Focus(); }
                }
                if (txtCGSTPur.Text.Trim() == "")
                {
                    EP.SetError(txtCGSTPur, "Select CGST%");
                    EP.SetIconAlignment(txtCGSTPur, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtCGSTPur.Focus(); }
                }
                if (txtItemName.Text.Trim() == "")
                {
                    EP.SetError(txtItemName, "Enter Item Name");
                    EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtItemName.Focus(); }
                }
                if (txtASaleRate.Text.Trim() == "")
                {
                    EP.SetError(txtASaleRate, "Enter Value");
                    EP.SetIconAlignment(txtASaleRate, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtASaleRate.Focus(); }
                }
                if (txtBSaleRate.Text.Trim() == "")
                {
                    EP.SetError(txtBSaleRate, "Enter Value");
                    EP.SetIconAlignment(txtBSaleRate, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtBSaleRate.Focus(); }
                }
                if (txtMRP.Text.Trim() == "")
                {
                    if (txtMRP.Text.Trim() == "0")
                    {
                        txtASaleRate.Focus();
                    }
                    else
                    {
                        EP.SetError(txtMRP, "Enter Value");
                        EP.SetIconAlignment(txtMRP, ErrorIconAlignment.MiddleRight);
                        if (flag) { flag = false; txtMRP.Focus(); }
                    }
                }
                if (ObjQry.ReturnInteger("Select Count(*) from MItemMaster where ItemName = '" + txtItemName.Text.Trim() + "' " + " AND GroupNo = (" + lstGroup1.SelectedValue + ") AND ItemNo not in (" + ID + ") ", CommonFunctions.ConStr) != 0)
                {
                    EP.SetError(txtItemName, "Duplicate Item Name");
                    EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                    txtItemName.Focus();
                }
                //}
                if (BarcodeNm != txtBarcode.Text)
                {
                    //f (ObjQry.ReturnInteger("Select Count(*) from MStockBarcode where Barcode = '" + txtBarcobe.Text + "'", CommonFunctions.ConStr) != 0)
                    if (ObjQry.ReturnInteger("Select Count(*) FROM MItemMaster where (Barcode = '" + txtBarcode.Text.Trim().Replace("'", "''") + "' or MItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "') AND MItemMaster.ItemNo<>" + ID + " and MItemMaster.ItemNo not in (" + ID + ") ", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtBarcode, "Duplicate Barcode");
                        EP.SetIconAlignment(txtBarcode, ErrorIconAlignment.MiddleRight);
                        //txtBarcode.Focus();

                        if (flag) { flag = false; txtBarcode.Focus(); }
                    }
                }
                if (ShortCodeNm != txtShortCode.Text)//&& txtShortCode.Text.Trim() != "0"
                {
                    if (ObjQry.ReturnInteger("Select Count(*) FROM MItemMaster  where (MItemMaster.ShortCode = '" + txtShortCode.Text.Trim() + "'  or MItemMaster.Barcode='" + txtShortCode.Text.Trim() + "') AND MItemMaster.ItemNo<>(" + ID + ")", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtShortCode, "Duplicate Short Code");
                        EP.SetIconAlignment(txtShortCode, ErrorIconAlignment.MiddleRight);
                        //txtShortCode.Focus();
                        if (flag) { flag = false; txtShortCode.Focus(); }
                    }
                }
                if (ItemShort != txtShortName.Text.Trim())
                {
                    if (ObjQry.ReturnInteger("Select Count(*) from MItemMaster where ShortCode = '" + txtShortName.Text.Trim() + "' AND ItemNo not in (" + ID + ") " +
                    " AND GroupNo = " + lstGroup1.SelectedValue +
                    " ", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtShortName, "Duplicate Item Short Desc");
                        EP.SetIconAlignment(txtShortName, ErrorIconAlignment.MiddleRight);
                        txtShortName.Focus();
                    }
                }
                if (txtUOMH.Text != "  NA")
                {
                    if (Convert.ToDouble(txtSStockConv.Text) <= 1.00)
                    {
                        EP.SetError(txtSStockConv, "Please Enter Value Greater than 1");
                        EP.SetIconAlignment(txtSStockConv, ErrorIconAlignment.MiddleRight);
                        txtSStockConv.Focus();

                        flag = false;
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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFields();
            btnNewBrand.Visible = false;
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbMItemMaster = new DBMItemMaster();
                mItemMaster = new MItemMaster();

                mItemMaster.ItemNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbMItemMaster.DeleteMItemMaster(mItemMaster) == true)
                    {
                        OMMessageBox.Show("Item Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillField();
                    }
                    else
                    {
                        OMMessageBox.Show("Item not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dtSearch = ObjFunction.GetDataView("Select  MItemMaster.Itemno From MItemMaster  inner join MItemGroup on MItemGroup.itemgroupno=MItemMaster.groupno  order by itemgroupname ,itemname ").Table;
                Form NewF = new StockItem();
                //  this.Close();
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           
            if (ID != 0)
            {

                FillField();
            }
            else if (ID == 0)
            {
                ID = OldID;
                FillField();
            }

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            ViewMode(false);
            txtSearchBarCode.Enabled = true;
            btnNewBrand.Visible = false;
            setError();
            pnlCessPur.Visible = false;
            pnlCessSales.Visible = false;
            pnlCGSTPur.Visible = false;
            pnlCGSTSales.Visible = false;
            pnlIGSTPur.Visible = false;
            pnlIGSTSales.Visible = false;
            pnlSGSTPur.Visible = false;
            pnlSGSTSales.Visible = false;
            pnlUOMD.Visible = false;
            pnlUOMH.Visible = false;
            pnlUOML.Visible = false;
            pnlGroup1.Visible = false;
            pnlBarCodePrint.Visible = false;
            pnlDepartment.Visible = false;
            pnlCategory.Visible = false;
            //PnlHSNCode.Visible = false;
            //pnlHSNDetails.Visible = false;
            btnNew.Focus();
            GID = 0;
        }

        public void setError()
        {
            EP.SetError(lstGroup1, "");
            EP.SetError(txtItemName, "");
            EP.SetError(txtShortName, "");
            EP.SetError(txtBarcode, "");
            EP.SetError(txtMRP, "");
            EP.SetError(lstUOML, "");
            EP.SetError(lstSGSTPur, "");
            EP.SetError(lstSGSTSales, "");
            EP.SetError(lstCGSTPur, "");
            EP.SetError(lstCGSTSales, "");
            EP.SetError(lstIGSTSales, "");
            EP.SetError(lstIGSTPur, "");
            EP.SetError(lstCessPur, "");
            EP.SetError(lstCessSales, "");
            EP.SetError(txtItemLang, "");
            EP.SetError(txtShortLang, "");
            EP.SetError(txtShortCode, "");
            EP.SetError(txtBSaleRate, "");
            EP.SetError(txtASaleRate, "");
            EP.SetError(txtShortCode, "");
            EP.SetError(txtMKTQty, "");
            EP.SetError(txtBrandName, "");
            EP.SetError(txtUOML, "");
            EP.SetError(txtSGSTSales, "");
            EP.SetError(txtSGSTPur, "");
            EP.SetError(txtCGSTSales, "");
            EP.SetError(txtCGSTPur, "");
            EP.SetError(txtUOMH, "");
            EP.SetError(txtUOML, "");
            EP.SetError(txtSMRP, "");
            EP.SetError(txtSStockConv, "");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ShortID = ID;
                int count = 0;
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                //chkActive.Checked = true;
                txtBarcode.Focus();
                btnNewBrand.Visible = true;
                GID = 0;
                BarcodeNm = txtBarcode.Text;
                if ((ObjQry.ReturnLong("select count(itemno) from Tstock where itemno=" + ID, CommonFunctions.ConStr)) > count)
                {
                    txtUOML.Enabled = false;
                    txtUOMD.Enabled = false;
                }
                //PkSrNo = ObjQry.ReturnLong("select Max(pksrno) from MRateSetting  where itemno=" + ID, CommonFunctions.ConStr);
                pnlBarCodePrint.Visible = false;
                FlagChange = false;
                SFlagChange = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool IsVarchar(string str)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum = false;
            for (int i = 0; i < str.Length; i++)
            {
                if ((Convert.ToChar(str[i]) >= 65 && Convert.ToChar(str[i]) <= 90) || (Convert.ToChar(str[i]) >= 97 && Convert.ToChar(str[i]) <= 122) || Convert.ToChar(str[i]) == 32)
                    isNum = true;
                else
                {
                    isNum = false;
                    break;
                }
            }
            return isNum;
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        public delegate void MovetoNext(int type);

        public void SearchBarcode()
        {
            try
            {
                string str = ""; long INo = 0;
                str = "SELECT ISNULL(MItemMaster.ItemNo,0) FROM MItemMaster  WHERE  (MItemMaster.Barcode = '" + txtSearchBarCode.Text.Trim().Replace("'", "''") + "') ";
                INo = ObjQry.ReturnLong(str, CommonFunctions.ConStr);
                if (INo != 0)
                {
                    ItemNm = "";
                    ItemShort = "";
                    BarcodeNm = "";
                    ShortCodeNm = "";
                    PkSrNo = 0;
                    SPkSrNo = 0;
                    dtSearch = ObjFunction.GetDataView("Select ItemNo From MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) WHERE ItemNo<>1  ORDER BY ItemName").Table;
                    StockItem.RequestItemNo = INo;
                    if (dtSearch.Rows.Count > 0)
                    {
                        if (StockItem.RequestItemNo == 0)
                            ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                        else
                            ID = StockItem.RequestItemNo;
                        FillField();
                        // SetNavigation();
                    }

                    txtSearchBarCode.Text = "";
                    txtSearchBarCode.Enabled = true;
                }
                else
                {
                    str = "SELECT ItemNo FROM MItemMaster WHERE (ItemNo <> 1) AND (ShortCode LIKE '" + txtSearchBarCode.Text.Trim().Replace("'", "''") + "')";
                    INo = ObjQry.ReturnLong(str, CommonFunctions.ConStr);

                    if (INo != 0)
                    {
                        ItemNm = "";
                        ItemShort = "";
                        BarcodeNm = "";
                        ShortCodeNm = "";
                        PkSrNo = 0;
                        SPkSrNo = 0;
                        dtSearch = ObjFunction.GetDataView("Select ItemNo From MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) WHERE ItemNo<>1 ORDER BY ItemName").Table;
                        StockItem.RequestItemNo = INo;
                        if (dtSearch.Rows.Count > 0)
                        {
                            if (StockItem.RequestItemNo == 0)
                                ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                            else
                                ID = StockItem.RequestItemNo;
                            FillField();
                        }
                        txtSearchBarCode.Text = "";
                        txtSearchBarCode.Enabled = true;
                    }
                    else
                    {
                        OMMessageBox.Show("BarCode does'nt exist", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtSearchBarCode.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNew_VisibleChanged(object sender, EventArgs e)
        {
            txtSearchBarCode.Enabled = btnNew.Visible;
        }

        private void txtMasked_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void btnLangLongDesc_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtItemLang.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtItemName.Text.Trim(), txtItemLang.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtItemLang.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtItemName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtItemName.Text.Trim(), txtItemLang.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtItemLang.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtItemLang.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnPrintBarCode_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                pnlBarCodePrint.Location = new Point(120, 50);
                pnlBarCodePrint.Height = 400;
                pnlBarCodePrint.Width = 300;
                pnlBarCodePrint.Visible = true;
                txtNoOfPrint.Text = "";
                txtStartNo.Text = "0";
                txtwt.Text = "";
                txtExpDays.Text = "";
                txtNoOfPrint.Enabled = true;
                txtStartNo.Enabled = true;
                rbBigMod.Enabled = true;
                rbSmallMode.Enabled = true;
                txtwt.Enabled = true;
                txtExpDays.Enabled = true;
                dtpPackingDate.Enabled = true;
                dtpPackingDate.Value = DBGetVal.ServerTime.Date;
                DateChange();
                txtNoOfPrint.Focus();
            }
        }

        private void btnOKPrintBarCode_Click(object sender, EventArgs e)
        {
            try
            {
                DBTBarCodePrint dbBarCodePrint = new DBTBarCodePrint();
                TBarCodePrint tBarCodePrint = new TBarCodePrint();
                tBarCodePrint.MacNo = DBGetVal.MacNo;
                tBarCodePrint.UserID = DBGetVal.UserID;
                dbBarCodePrint.DeleteTBarCodePrint(tBarCodePrint);

                tBarCodePrint = new TBarCodePrint();
                tBarCodePrint.PkSrNo = 0;
                tBarCodePrint.ItemNo = ID;
                tBarCodePrint.FKRateSettingNo = PkSrNo;
                tBarCodePrint.Quantity = Convert.ToInt64(txtNoOfPrint.Text);


                tBarCodePrint.MacNo = DBGetVal.MacNo;
                tBarCodePrint.UserID = DBGetVal.UserID;
                dbBarCodePrint.AddTBarCodePrint(tBarCodePrint);
                dbBarCodePrint.ExecuteNonQueryStatements();

                string[] ReportSession;
                ReportSession = new string[7];
                ReportSession[0] = "1";
                ReportSession[1] = txtStartNo.Text;
                ReportSession[2] = DBGetVal.MacNo.ToString();
                ReportSession[3] = DBGetVal.UserID.ToString();
                ReportSession[4] = txtwt.Text.Trim();
                ReportSession[5] = Convert.ToDateTime(dtpPackingDate.Value).ToString();
                ReportSession[6] = lblExpDate.Text.Trim();

                if (OMMessageBox.Show("Do you want Preview of barcode?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Form NewF;
                    if (rbBigMod.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.BarCodePrintBig(), ReportSession);
                        else
                            // NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintBig.rpt", CommonFunctions.ReportPath), ReportSession);
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rbSmallMode.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.BarCodePrint(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("BarCodePrintOnMart.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm = null;
                    if (rbBigMod.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            childForm = new Reports.BarCodePrintBig();
                        else
                            childForm = ObjFunction.LoadReportObject("BarCodePrintBig.rpt", CommonFunctions.ReportPath);
                    }
                    else if (rbSmallMode.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            childForm = new Reports.BarCodePrint();
                        else

                            childForm = ObjFunction.LoadReportObject("BarCodePrint.rpt", CommonFunctions.ReportPath);
                    }

                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            OMMessageBox.Show("Printing barCode sucessfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                        else
                        {
                            OMMessageBox.Show("Barcode not Print...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Barcode Print report not exist...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                //dbBarCodePrint.DeleteTBarCodePrint(tBarCodePrint);
                pnlBarCodePrint.Visible = false;
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancelPrintBarcode_Click(object sender, EventArgs e)
        {
            pnlBarCodePrint.Visible = false;
            btnNew.Focus();
        }

        private void txtExpDays_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtExpDays, -1, 10, OMFunctions.MaskedType.NotNegative);
        }

        private void dtpPackingDate_ValueChanged(object sender, EventArgs e)
        {
            DateChange();
        }

        private void txtExpDays_Leave(object sender, EventArgs e)
        {
            DateChange();
        }

        public void DateChange()
        {
            DateTime dt;
            if (txtExpDays.Text.Trim() != "")
            {
                dt = Convert.ToDateTime(dtpPackingDate.Value).AddDays(Convert.ToInt64(txtExpDays.Text.Trim()));
                lblExpDate.Text = Convert.ToDateTime(dt).ToString("dd-MMM-yyyy");
            }
            else
            {
                txtExpDays.Text = "0";
                lblExpDate.Text = Convert.ToDateTime(dtpPackingDate.Value).ToString("dd-MMM-yyyy");
            }
        }

        private void txtNoOfPrint_Leave(object sender, EventArgs e)
        {
            if (txtNoOfPrint.Text.Trim() == "")
                txtNoOfPrint.Text = "1";
        }

        private void txtStartNo_Leave(object sender, EventArgs e)
        {
            if (txtStartNo.Text.Trim() == "")
                txtStartNo.Text = "0";
        }

        private void txtNoOfPrint_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtStartNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        public bool CheckGolableMaster()
        {
            bool flag = false;
            GID = ObjQry.ReturnInteger("SELECT ISNULL(MGlobalStockItems.ItemNo, 0) AS ItemNo FROM MGlobalStockBarcode INNER JOIN MGlobalStockItems ON MGlobalStockBarcode.ItemNo = MGlobalStockItems.ItemNo WHERE (MGlobalStockItems.IsActive = 'false') AND (MGlobalStockBarcode.Barcode = '" + txtBarcode.Text.Trim().Replace("'", "''") + "') ", CommonFunctions.ConStr);
            if (GID != 0)
            {
                //FillControlsGlobal();
                ID = 0;
                //  lblmsg.Visible = true;
                flag = true;
            }
            else
            {
                GID = 0;
                //lblmsg.Visible = false;
            }
            return flag;
        }

        private void txtLangFullDesc_Leave(object sender, EventArgs e)
        {
            if (txtItemLang.Text.Trim() == "")
                txtItemLang.Text = ObjFunction.ChecklLangVal(txtItemName.Text);
            else
                txtShortName.Focus();
        }

        private void txtFactor_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 3, 11, OMFunctions.MaskedType.NotNegative);
        }

        private void btnPrintBarCodeAdvance_Click(object sender, EventArgs e)
        {
            pnlBarCodePrint.Visible = false;
            if (ID != 0)
            {
                Form frmChild = new Settings.BarcodePrinting(ID, PkSrNo, txtMRP.Text, txtBSaleRate.Text, lstGroup1.Text, txtShortName.Text, txtBarcode.Text,Convert.ToDouble(txtPurRate.Text));
                ObjFunction.OpenForm(frmChild);
            }
        }

        private void txtMargin2_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 4, OMFunctions.MaskedType.NotNegative);
        }

        private void txtMargin2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

            }
        }

        private void txtCessValues_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
        }

        private void txtPkgChrg_Leave(object sender, EventArgs e)
        {

        }

        private void txtSMktQty_Leave(object sender, EventArgs e)
        {
            if (txtSStockConv.Text.Trim() == "")
                txtSStockConv.Text = "1";
        }

        private void dgSelectComp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                DataTable dtComp = new DataTable();
                dtComp.Columns.Add("MfgCompNo");
                dtComp.Columns.Add("MfgCompName");

                FillCombo(cmbMfgComp, dtComp);
            }
        }

        public void FillCombo(ComboBox cmb, DataTable dtComp)
        {
            DataTable dt = dtComp;
            DataRow dr = dt.NewRow();
            dr[0] = "0";

            dr[1] = " ------ Select ------ ";

            dtComp.Rows.InsertAt(dr, 0);
            cmb.DisplayMember = dtComp.Columns[1].ColumnName;
            cmb.ValueMember = dtComp.Columns[0].ColumnName;
            cmb.DataSource = dtComp;
            cmb.SelectedIndex = 0;
        }

        private void chkCompType_CheckedChanged(object sender, EventArgs e)
        {
            DataTable dtComp = new DataTable();
            dtComp.Columns.Add("MfgCompNo");
            dtComp.Columns.Add("MfgCompName");

            string prevValue = "";
            if (cmbMfgComp.SelectedValue != null)
                prevValue = cmbMfgComp.SelectedValue.ToString();

            FillCombo(cmbMfgComp, dtComp);

            if (!prevValue.Equals("")) cmbMfgComp.SelectedValue = prevValue;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            dgSelectComp_CellContentClick(sender, new DataGridViewCellEventArgs(0, 0));
            cmbMfgComp.Focus();
        }

        private void dgSelectComp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                cmbMfgComp.Focus();
            }
        }

        private void chkType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbMfgComp.Focus();
            }
        }

        private void txtHigherVariation_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 2, OMFunctions.MaskedType.NotNegative);
        }

        private void txtLowerVariation_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 2, OMFunctions.MaskedType.NotNegative);
        }

        private void btnIngredientDetails_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                Form NewF = new StockItemIngredientDetails(ID);
                ObjFunction.OpenForm(NewF);
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////

        private void txtSearchBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (btnNew.Visible)
                    SearchBarcode();
            }
            if (e.KeyChar == Convert.ToChar(Keys.LButton))
            {
                btnNew.Focus();
            }
            if (e.KeyChar == Convert.ToChar(Keys.RButton))
            {
                btnLast.Focus();
            }
        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtBarcode, "");
                // dgSelectComp.Enabled = true;
                if (txtBarcode.Text.Trim() != "")
                {
                    if (BarcodeNm != txtBarcode.Text.Trim())
                    {
                        ItemNm = ""; ItemShort = ""; ShortCodeNm = ""; PkSrNo = 0;
                        FromDate = Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD)).Date;
                        ID = ObjQry.ReturnInteger("SELECT ISNULL(MItemMaster.ItemNo,0) FROM MItemMaster  WHERE  (MItemMaster.Barcode = '" + txtBarcode.Text.Trim().Replace("'", "''") + "') ", CommonFunctions.ConStr);
                        if (ID != 0)
                        {
                            FillField();
                            txtShortCode.Focus();

                        }
                        else if (ObjQry.ReturnInteger("Select Count(*) FROM MStockBarcode INNER JOIN MItemMaster ON MStockBarcode.ItemNo = MItemMaster.ItemNo where (Barcode = '" + txtBarcode.Text.Trim().Replace("'", "''") + "' or MItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "') AND MItemMaster.ItemNo<>" + ID + " and MItemMaster.ItemNo not in (" + ID + ") ", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtBarcode, "Duplicate Barcode");
                            EP.SetIconAlignment(txtBarcode, ErrorIconAlignment.MiddleRight);
                            txtBarcode.Focus();
                        }
                        //else if (CheckGolableMaster())
                        // {
                        //   txtASaleRate.Focus();
                        // }
                        else if (txtBarcode.Text.Trim().Length > 8)
                        {
                            long tempUom = ObjQry.ReturnLong("Select UomNo from MUom where UomName='Nos'", CommonFunctions.ConStr);
                            // lstUom.SelectedValue = tempUom.ToString();
                            txtShortCode.Text = txtBarcode.Text.Trim();
                        }
                        else
                        {
                            FlagChange = true;
                            SFlagChange = true;
                            txtShortCode.Text = txtBarcode.Text.Trim();
                        }
                    }
                }
                else
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_AutogenerateBarcode)) == true)
                    {
                        txtBarcode.Text = (ObjQry.ReturnLong("Select Max(Cast(BarCode As numeric)) From MItemMaster Where Len(BarCode)<=" + ObjFunction.GetAppSettings(AppSettings.O_AutogenerateBarcodeLength) + " AND IsNumeric(BarCode)=1", CommonFunctions.ConStr) + 1).ToString();
                        txtShortCode.Text = txtBarcode.Text.Trim();
                    }
                    else
                    {
                        EP.SetError(txtBarcode, "Enter barcode.");
                        EP.SetIconAlignment(txtBarcode, ErrorIconAlignment.MiddleRight);
                        txtBarcode.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtShortCode_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtShortCode, "");
                if (txtShortCode.Text != "")
                {
                    if (ShortCodeNm != txtShortCode.Text)//&& txtShortCode.Text.Trim() != "0"
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) FROM MItemMaster  where (MItemMaster.ShortCode = '" + txtShortCode.Text.Trim() + "'  or MItemMaster.Barcode='" + txtShortCode.Text.Trim() + "') AND MItemMaster.ItemNo<>(" + ID + ")", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtShortCode, "Duplicate Short Code");
                            EP.SetIconAlignment(txtShortCode, ErrorIconAlignment.MiddleRight);
                            txtShortCode.Focus();
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsBrandFilter)) == true)
                            {
                                txtDepartment.Focus();
                            }
                            else
                            {
                                txtBrandName.Focus();
                            }
                        }
                    }
                }
                else
                {
                    if (txtBarcode.Text != "")
                    {
                        txtShortCode.Text = txtBarcode.Text;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsBrandFilter)) == true)
                        {
                            txtDepartment.Focus();
                        }
                        else
                        {
                            txtBrandName.Focus();
                        }
                    }
                    else
                    {
                        txtBarcode.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtItemLang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtItemLang.Text != "")
                {
                    txtShortName.Focus();
                }
                else
                    txtItemLang.Focus();
            }
        }

        private void txtItemName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtItemName, "");
                txtItemName.Text = txtItemName.Text.Trim().ToUpper();
                if (txtItemName.Text.Trim() != "")
                {
                    //txtLangFullDesc.Text = "";
                    if (ItemNm != txtItemName.Text.Trim())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MItemMaster where ItemName = '" + txtItemName.Text.Trim() + "' " + " AND GroupNo = (" + lstGroup1.SelectedValue + ") AND ItemNo not in (" + ID + ") ", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(txtItemName, "Duplicate Item Name");
                            EP.SetIconAlignment(txtItemName, ErrorIconAlignment.MiddleRight);
                            txtItemName.Focus();
                        }
                        else if (FlagBilingual == true)
                        {
                            txtShortName.Text = txtItemName.Text;
                            txtItemLang.Text = "";
                            //  if (txtItemLang.Text.Trim().Length == 0)
                            {
                                btnLangLongDesc_Click(btnItemLang, null);
                            }
                        }
                        else
                        {
                            txtShortName.Text = txtItemName.Text;
                            txtShortName.Focus();
                        }
                    }
                    else
                    {
                        if (FlagBilingual == true)
                        {
                            //txtLangFullDesc.Focus();
                            if (txtItemName.Text.Trim().Length == 0)
                            {
                                btnLangLongDesc_Click(btnItemLang, null);
                            }
                        }
                        else
                            txtShortName.Focus();
                    }
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

        private void txtShortName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtShortName, "");
                txtShortName.Text = txtShortName.Text.Trim().ToUpper();
                if (txtShortName.Text.Trim() != "")
                {

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                    {
                        txtShortLang.Text = txtItemLang.Text;
                        txtShortLang.Focus();
                    }
                    else
                    {
                        txtUOML.Focus();
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnShortLang_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtShortLang.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtShortName.Text, txtShortLang.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtShortLang.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }

                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtShortName.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtShortName.Text, txtShortLang.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtShortLang.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtShortLang.Text = val;

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtShortLang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtUOML.Enabled == true)
                {
                    txtUOML.Focus();
                }
                else { txtMRP.Focus(); }
            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBarcode.Text == "")
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_AutogenerateBarcode)) == true)
                    {
                        txtBarcode.Text = (ObjQry.ReturnLong("Select Max(Cast(BarCode As numeric)) From MitemMaster Where Len(BarCode)<=" + ObjFunction.GetAppSettings(AppSettings.O_AutogenerateBarcodeLength) + " AND IsNumeric(BarCode)=1", CommonFunctions.ConStr) + 1).ToString();
                        txtShortCode.Text = txtBarcode.Text.Trim();
                        txtShortCode.Focus();
                    }
                    else
                    {
                        EP.SetError(txtBarcode, "Enter barcode.");
                        EP.SetIconAlignment(txtBarcode, ErrorIconAlignment.MiddleRight);
                        txtBarcode.Focus();
                    }
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

                if (txtBarcode.Text != "")
                {
                    txtShortCode.Text = txtBarcode.Text;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsBrandFilter)) == true)
                    {
                        txtDepartment.Focus();
                    }
                    else
                    {
                        txtBrandName.Focus();
                    }
                }
                else
                {
                    txtBarcode.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);

            }
        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtItemName_Leave(sender, new EventArgs());
                txtItemLang.Focus();
            }

        }

        private void txtShortName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                // e.SuppressKeyPress = true;
                if (txtShortName.Text == "")
                {
                    txtShortName.Text = txtItemName.Text.Trim();
                }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    txtShortLang.Focus();
                }
                else
                {
                    if (txtUOML.Enabled == true)
                    {
                        txtUOML.Focus();
                    }
                    else
                    {
                        txtMRP.Focus();

                    }
                }
            }

        }

        private void txtBrandName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBrandName.Text == "")
                {
                    if (DBGetFlag.Brand == true)
                    {

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                        {
                            ObjFunction.FillList(lstGroup1Lang, "SELECT ItemGroupNo,LangGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY ItemGroupName");

                        }
                        ObjFunction.FillList(lstGroup1, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY ItemGroupName");

                    }
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                    lstGroup1.SelectedIndex = 0;
                }
                else
                {
                    pnlGroup1.Visible = false;
                    txtItemName.Focus();
                }

            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                //e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstGroup1.SelectedValue);
                pnlGroup1.Visible = true;
                lstGroup1.Focus();

            }
        }

        private void lstGroup1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtBrandName.Text = lstGroup1.Text;
                    txtBrandLang.Text = ObjQry.ReturnString(" select LangGroupName from MItemGroup where ItemGroupNo=" + lstGroup1.SelectedValue + "", CommonFunctions.ConStr);
                    txtItemName.Focus();
                    pnlGroup1.Visible = false;
                    FillTaxDetails();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtBrandName.Focus();
                    lstGroup1.SelectedValue = tepmGroupno;
                    txtBrandName.Text = lstGroup1.Text;
                    tepmGroupno = 0;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    pnlGroup1.Visible = false;
                    txtBrandName.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillTaxDetails(double PER)
        {
            try
            {
                DataTable dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=" + PER / 2 + " AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=10) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=51) ").Table;
                // DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=51");
                if (dtItemTax.Rows.Count > 0)
                {
                    lstSGSTSales.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtSGSTSales.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }
                //dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=51");
                dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=" + PER / 2 + " AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=11) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=51) ").Table;
                if (dtItemTax.Rows.Count > 0)
                {
                    lstSGSTPur.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtSGSTPur.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }

                // dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=52");
                dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=" + PER / 2 + " AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=10) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=52) ").Table;

                if (dtItemTax.Rows.Count > 0)
                {
                    lstCGSTSales.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtCGSTSales.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }

                //   dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=52");
                dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=" + PER / 2 + " AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=11) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=52) ").Table;

                if (dtItemTax.Rows.Count > 0)
                {
                    lstCGSTPur.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtCGSTPur.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }

                // dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=" + GroupType.IGST + "");
                dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=" + PER + " AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=10) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=53) ").Table;

                if (dtItemTax.Rows.Count > 0)
                {
                    lstIGSTSales.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtIGSTSales.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }

                //    dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=" + GroupType.IGST + "");
                dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=" + PER + " AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=11) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=53) ").Table;

                if (dtItemTax.Rows.Count > 0)
                {
                    lstIGSTPur.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtIGSTPur.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }
                //  dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=" + GroupType.Cess + "");
                dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=0 AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=10) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=54) ").Table;

                if (dtItemTax.Rows.Count > 0)
                {
                    lstCessSales.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtCessSales.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }

                // dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=" + GroupType.Cess + "");
                dtItemTax = ObjFunction.GetDataView("SELECT * FROM MItemTaxSetting WHERE PERCENTAGE=0 AND SALESLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=11) AND TAXLEDGERNO IN (SELECT LEDGERNO FROM MLEDGER WHERE GROUPNO=54) ").Table;

                if (dtItemTax.Rows.Count > 0)
                {
                    lstCessPur.SelectedValue = dtItemTax.Rows[0][0].ToString();
                    txtCessPur.Text = dtItemTax.Rows[0][4].ToString() + " %";
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillTaxDetails()
        {
            long ItemId = ObjQry.ReturnLong("SELECT TOP (1) ItemNo FROM MItemMaster WHERE (GroupNo = " + (lstGroup1.SelectedValue) + " ) and itemno in (select ItemNo FROM MItemTaxInfo) order by itemno desc ", CommonFunctions.ConStr);
            if (ItemId != 0)
            {
                DataTable dtItemTax = ObjFunction.GetDataView("Select * From dbo.GetItemTaxAll(" + ItemId + ", NULL, NULL,NULL,NULL) order by Percentage  ").Table;
                DataRow[] dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=51");
                if (dr.Length > 0)
                {
                    lstSGSTSales.SelectedValue = dr[0][10].ToString();
                    txtSGSTSales.Text = lstSGSTSales.Text;
                }
                dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=51");
                if (dr.Length > 0)
                {
                    lstSGSTPur.SelectedValue = dr[0][10].ToString();
                    txtSGSTPur.Text = lstSGSTPur.Text;
                }
                dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=52");
                if (dr.Length > 0)
                {
                    lstCGSTSales.SelectedValue = dr[0][10].ToString();
                    txtCGSTSales.Text = lstCGSTSales.Text;
                }
                dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=52");
                if (dr.Length > 0)
                {
                    lstCGSTPur.SelectedValue = dr[0][10].ToString();
                    txtCGSTPur.Text = lstCGSTPur.Text;
                }
                dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=" + GroupType.IGST + "");
                if (dr.Length > 0)
                {
                    lstIGSTSales.SelectedValue = dr[0][10].ToString();
                    txtIGSTSales.Text = lstIGSTSales.Text;
                }
                dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=" + GroupType.IGST + "");
                if (dr.Length > 0)
                {
                    lstIGSTPur.SelectedValue = dr[0][10].ToString();
                    txtIGSTPur.Text = lstIGSTPur.Text;
                }
                dr = dtItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=" + GroupType.Cess + "");
                if (dr.Length > 0)
                {
                    lstCessSales.SelectedValue = dr[0][10].ToString();
                    txtCessSales.Text = lstCessSales.Text;
                }
                dr = dtItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=" + GroupType.Cess + "");
                if (dr.Length > 0)
                {
                    lstCessPur.SelectedValue = dr[0][10].ToString();
                    txtCessPur.Text = lstCessPur.Text;
                }
                //Wrong not decide logic
                string sqlQuery = "";
                sqlQuery = "SELECT MItemGroup.ItemGroupNo, MItemGroup.ItemGroupName, MItemMaster.UOMH, MItemMaster.UOML, MItemMaster.UOMDefault " +
                 " FROM  MItemGroup INNER JOIN MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo " +
                 " WHERE(MItemGroup.IsActive = 'True') AND(MItemGroup.ControlGroup = 2) AND ItemNo = " + Convert.ToInt64(lstCategory.SelectedValue) + "  ORDER BY MItemGroup.ItemGroupName";

                DataTable dtUOM = ObjFunction.GetDataView(sqlQuery).Table;
                for (int i = 0; i < dtUOM.Rows.Count; i++)
                {
                    lstUOMH.SelectedValue = dtUOM.Rows[i].ItemArray[2].ToString();
                    txtUOMH.Text = lstUOMH.Text;

                    lstUOML.SelectedValue = dtUOM.Rows[i].ItemArray[3].ToString();
                    txtUOML.Text = lstUOML.Text;

                    lstUOMD.SelectedValue = dtUOM.Rows[i].ItemArray[4].ToString();
                    txtUOMD.Text = lstUOMD.Text;
                }
            }
            else
            {
                txtSGSTSales.Text = "";
                txtSGSTPur.Text = "";
                txtCGSTSales.Text = "";
                txtCGSTPur.Text = "";
                txtIGSTSales.Text = "";
                txtIGSTPur.Text = "";
                txtCessSales.Text = "";
                txtCessPur.Text = "";
            }
        }

        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtDepartment.Text == "")
                {
                    pnlDepartment.Visible = true;
                    lstDepartment.Focus();
                    FillListAllMasters();
                    //lstDepartment.SelectedIndex = 0;
                }
                else
                {
                    pnlDepartment.Visible = false;
                    txtCategory.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlDepartment.Visible = true;
                lstDepartment.Focus();
            }
        }

        private void lstDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtDepartment.Text = lstDepartment.Text;
                    pnlDepartment.Visible = false;
                    txtCategory.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtDepartment.Focus();
                    txtDepartment.Text = lstDepartment.Text;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCategory.Text == "")
                {
                    pnlCategory.Visible = true;
                    lstCategory.Focus();
                    FillListAllMasters();
                    // lstCategory.SelectedIndex = 0;
                }
                else
                {
                    pnlCategory.Visible = false;
                    txtBrandName.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlCategory.Visible = true;
                lstCategory.Focus();
            }
        }

        private void lstCategory_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtCategory.Text = lstCategory.Text;
                    pnlCategory.Visible = false;
                    txtBrandName.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtCategory.Focus();
                    txtCategory.Text = lstCategory.Text;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtUOML_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUOML.Text == "")
                {
                    pnlUOML.Visible = true;
                    lstUOML.Focus();
                    lstUOML.SelectedIndex = 0;
                }
                else
                {
                    pnlUOML.Visible = false;
                    txtMRP.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                tepmGroupno = Convert.ToInt32(lstUOML.SelectedValue);
                pnlUOML.Visible = true;
                lstUOML.Focus();
            }
        }

        private void lstUOML_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtUOML.Text = lstUOML.Text;
                    pnlUOML.Visible = false;
                    if (txtUOML.Text == "GRAM")
                    {
                        lbluom.Visible = true;
                    }
                    txtMRP.Focus();
                    ObjFunction.FillList(lstUOMH, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMtype=1 and UOMNo  NOT IN(" + lstUOML.SelectedValue + " ) ORDER BY UOMName");
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtUOML.Focus();
                    lstUOML.SelectedValue = tepmGroupno;
                    txtUOML.Text = lstUOML.Text;
                    tepmGroupno = 0;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtUOMH_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUOMH.Text == "")
                {
                    pnlUOMH.Visible = true;
                    lstUOMH.Focus();
                    lstUOMH.SelectedIndex = 0;
                }
                else if (txtUOMH.Text == "  NA")
                {
                    ObjFunction.FillList(lstUOMD, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMNo in(" + lstUOML.SelectedValue + " ) ORDER BY UOMName");
                    txtUOMD.Text = lstUOMD.Text;

                    chkActive.Focus();
                }
                else
                {
                    pnlUOMH.Visible = false;
                    txtSMRP.Focus();
                    ObjFunction.FillList(lstUOMD, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMNo in(" + lstUOML.SelectedValue + "," + lstUOMH.SelectedValue + " ) ORDER BY UOMName");
                    txtUOMD.Text = lstUOMD.Text;
                }
            }
            else
            {
                tepmGroupno = Convert.ToInt32(lstUOMH.SelectedValue);
                pnlUOMH.Visible = true;
                lstUOMH.Focus();
            }


        }

        private void lstUOMH_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtUOMH.Text = lstUOMH.Text;
                    pnlUOMH.Visible = false;
                    if (Convert.ToInt32(lstUOMH.SelectedValue) != 1)
                    {
                        txtSMRP.Focus();
                        lblKg.Text = "1" + " " + txtUOMH.Text + " " + "=";
                        label17.Text = txtUOML.Text;
                        if (txtUOMH.Text == "KG" && txtUOML.Text == "GRAM")
                        {
                            txtSStockConv.Text = "1000";
                            txtSStockConv.Enabled = false;

                        }
                        else
                        {
                            txtSStockConv.Enabled = true;
                            txtSStockConv.Text = "1";
                        }

                        ObjFunction.FillList(lstUOMD, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMNo in(" + lstUOML.SelectedValue + "," + lstUOMH.SelectedValue + " ) and UOMNo!=1 ORDER BY UOMName");
                        txtUOMD.Text = lstUOMD.Text ;
                    }
                    else
                    {

                        ObjFunction.FillList(lstUOMD, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMNo in(" + lstUOML.SelectedValue + " ) ORDER BY UOMName");
                        txtUOMD.Text = lstUOMD.Text;

                        chkActive.Focus();
                        // txtUOMD.Focus();

                    }



                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtUOMH.Focus();
                    pnlUOMH.Visible = false;
                    lstUOMH.SelectedValue = tepmGroupno;
                    txtUOMH.Text = lstUOMH.Text;
                    tepmGroupno = 0;

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtUOMD_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUOMD.Text == "")
                {
                    // ObjFunction.FillList(lstUOMD, "SELECT UOMNo,UOMName from MUOM WHERE IsActive = 'True' and UOMNo in(" + lstUOML.SelectedValue + "," + lstUOMH.SelectedValue + " ) ORDER BY UOMName");

                    pnlUOMD.Visible = true;
                    lstUOMD.Focus();
                }
                else
                {
                    pnlUOMD.Visible = false;
                    chkActive.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                tepmGroupno = Convert.ToInt32(lstUOMD.SelectedValue);
                pnlUOMD.Visible = true;
                lstUOMD.Focus();

            }

        }

        private void lstUOMD_KeyDown_1(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtUOMD.Text = lstUOMD.Text;

                    chkActive.Focus();
                    pnlUOMD.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtUOMD.Focus();
                pnlUOMD.Visible = false;
                lstUOMD.SelectedValue = tepmGroupno;
                txtUOMD.Text = lstUOMD.Text;
                tepmGroupno = 0;

            }
        }

        private void txtIGSTSales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtIGSTSales.Text == "")
                {
                    pnlIGSTSales.Visible = true;
                    lstIGSTSales.Focus();
                }
                else
                {
                    pnlIGSTSales.Visible = false;
                    txtIGSTPur.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                //e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstIGSTSales.SelectedValue);
                pnlIGSTSales.Visible = true;
                lstIGSTSales.Focus();
            }
        }

        private void lstIGSTSales_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtIGSTSales.Text = lstIGSTSales.Text;
                    pnlIGSTSales.Visible = false;
                    string PER = txtIGSTSales.Text.Replace(" %", "");
                    FillTaxDetails(Convert.ToDouble(PER));
                    txtIGSTPur.Focus();

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtIGSTSales.Focus();
                    lstIGSTSales.SelectedValue = tepmGroupno;
                    txtIGSTSales.Text = lstIGSTSales.Text;
                    tepmGroupno = 0;
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtIGSTPur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtIGSTPur.Text == "")
                {
                    pnlIGSTPur.Visible = true;
                    lstIGSTPur.Focus();
                }
                else
                {
                    pnlIGSTPur.Visible = false;
                    txtSGSTSales.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                //e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstIGSTPur.SelectedValue);
                pnlIGSTPur.Visible = true;
                lstIGSTPur.Focus();
            }
        }

        private void lstIGSTPur_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtIGSTPur.Text = lstIGSTPur.Text;
                    string PER = txtIGSTPur.Text.Replace(" %", "");
                    FillTaxDetails(Convert.ToDouble(PER));
                    txtSGSTSales.Focus();
                    pnlIGSTPur.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtIGSTPur.Focus();
                pnlIGSTPur.Visible = false;
                lstIGSTPur.SelectedValue = tepmGroupno;
                txtIGSTPur.Text = lstIGSTPur.Text;
                tepmGroupno = 0;


            }
        }

        private void txtSGSTSales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSGSTSales.Text == "")
                {
                    pnlSGSTSales.Visible = true;
                    lstSGSTSales.Focus();
                }
                else
                {
                    pnlSGSTSales.Visible = false;
                    txtSGSTPur.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                //  e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstSGSTSales.SelectedValue);
                pnlSGSTSales.Visible = true;
                lstSGSTSales.Focus();
            }
        }

        private void lstSGSTSales_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtSGSTSales.Text = lstSGSTSales.Text;
                    string PER = txtSGSTSales.Text.Replace(" %", "");
                    FillTaxDetails(Convert.ToDouble(PER) * 2);
                    pnlSGSTSales.Visible = false;
                    txtSGSTPur.Focus();

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtSGSTSales.Focus();
                    lstSGSTSales.SelectedValue = tepmGroupno;
                    txtSGSTSales.Text = lstSGSTSales.Text;
                    tepmGroupno = 0;

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtSGSTPur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSGSTPur.Text == "")
                {
                    pnlSGSTPur.Visible = true;
                    lstSGSTPur.Focus();
                }
                else
                {
                    pnlSGSTPur.Visible = false;
                    txtCGSTSales.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                tepmGroupno = Convert.ToInt32(lstSGSTPur.SelectedValue);
                pnlSGSTPur.Visible = true;
                lstSGSTPur.Focus();
            }

        }

        private void lstSGSTPur_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtSGSTPur.Text = lstSGSTPur.Text;
                    string PER = txtSGSTPur.Text.Replace(" %", "");
                    FillTaxDetails(Convert.ToDouble(PER) * 2);
                    txtCGSTSales.Focus();
                    pnlSGSTPur.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtSGSTPur.Focus();
                pnlSGSTPur.Visible = false;
                lstSGSTPur.SelectedValue = tepmGroupno;
                txtSGSTPur.Text = lstSGSTPur.Text;
                tepmGroupno = 0;


            }
        }

        private void txtCGSTSales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCGSTSales.Text == "")
                {
                    pnlCGSTSales.Visible = true;
                    lstCGSTSales.Focus();
                }
                else
                {
                    pnlCGSTSales.Visible = false;
                    txtCGSTPur.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                // e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstCGSTSales.SelectedValue);
                pnlCGSTSales.Visible = true;
                lstCGSTSales.Focus();
            }
        }

        private void lstCGSTSales_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtCGSTSales.Text = lstCGSTSales.Text;
                    string PER = txtCGSTSales.Text.Replace(" %", "");
                    FillTaxDetails(Convert.ToDouble(PER) * 2);

                    pnlCGSTSales.Visible = false;
                    txtCGSTSales.Focus();

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtCGSTPur.Focus();
                    lstCGSTSales.SelectedValue = tepmGroupno;
                    txtCGSTSales.Text = lstCGSTSales.Text;
                    tepmGroupno = 0;

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtCGSTPur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCGSTPur.Text == "")
                {
                    pnlCGSTPur.Visible = true;
                    lstCGSTPur.Focus();
                }
                else
                {
                    pnlCGSTPur.Visible = false;
                    txtCessSales.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                //    e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstCGSTPur.SelectedValue);
                pnlCGSTPur.Visible = true;
                lstCGSTPur.Focus();
            }
        }

        private void lstCGSTPur_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtCGSTPur.Text = lstCGSTPur.Text;
                    string PER = txtCGSTPur.Text.Replace(" %", "");
                    FillTaxDetails(Convert.ToDouble(PER) * 2);

                    txtCessSales.Focus();
                    pnlCGSTPur.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtCGSTPur.Focus();
                pnlCGSTPur.Visible = false;
                lstCGSTPur.SelectedValue = tepmGroupno;
                txtCGSTPur.Text = lstCGSTPur.Text;
                tepmGroupno = 0;


            }
        }

        private void txtCessSales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCessSales.Text == "")
                {
                    pnlCessSales.Visible = true;
                    lstCessSales.Focus();
                }
                else
                {
                    pnlCessSales.Visible = false;
                    txtCessPur.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                //e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstCessSales.SelectedValue);
                pnlCessSales.Visible = true;
                lstCessSales.Focus();
            }
        }

        private void lstCessSales_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtCessSales.Text = lstCessSales.Text;
                    pnlCessSales.Visible = false;
                    txtCessPur.Focus();

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtIGSTPur.Focus();
                    lstCessSales.SelectedValue = tepmGroupno;
                    txtCessSales.Text = lstCessSales.Text;
                    tepmGroupno = 0;

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtCessPur_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtCessPur.Text == "")
                {
                    pnlCessPur.Visible = true;
                    lstCessPur.Focus();
                }
                else
                {
                    pnlCessPur.Visible = false;
                    if (groupBox3.Visible == true)
                    {
                        dtpedate.Focus();
                    }
                    else
                    {
                        cmbMfgComp.Focus();
                        //dtpedate.Focus();
                    }
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                // e.KeyChar = Convert.ToChar(0);
                tepmGroupno = Convert.ToInt32(lstCessPur.SelectedValue);
                pnlCessPur.Visible = true;
                lstCessPur.Focus();
            }
        }

        private void lstCessPur_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtCessPur.Text = lstCessPur.Text;

                    dtpedate.Focus();
                    pnlCessPur.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtCessPur.Focus();
                pnlCessPur.Visible = false;
                lstCessPur.SelectedValue = tepmGroupno;
                txtCessPur.Text = lstCessPur.Text;
                tepmGroupno = 0;

            }
        }

        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtMRP.Text.Trim() != "")
                {

                    if (txtMRP.Text.Trim() == "0.00")
                    {
                        txtASaleRate.Focus();
                    }
                    if (MRPC != Convert.ToDouble(txtMRP.Text))
                    {
                        txtASaleRate.Text = txtMRP.Text;
                        txtBSaleRate.Text = txtMRP.Text;
                        txtSMRP.Text = txtMRP.Text;
                        FlagChange = true;
                    }
                    if ((ObjFunction.GetListValue(lstUOML) != 0))
                        pnlSUOM.Visible = true;

                    txtASaleRate.Focus();

                }
                else
                {

                    pnlSUOM.Visible = false;
                }

            }
        }

        private void txtASaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtASaleRate.Text == "")
                { txtASaleRate.Focus(); }
                else txtBSaleRate.Focus();

            }
        }

        private void txtBSaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtBSaleRate.Text == "")
                { txtBSaleRate.Focus(); }
                else txtMKTQty.Focus();

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
                else txtPurRate.Focus();

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
                else //if ((ObjFunction.GetListValue(lstUOML) != 0))
                {
                    //pnlSUOM.Visible = true;
                    // txtUOMH.Focus();
                    txtHamali1.Focus();
                }
            }
        }

        private void txtWeight1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtWeight1.Text == "")
                {
                    txtWeight1.Focus();
                }
                else if ((ObjFunction.GetListValue(lstUOML) != 0))
                {
                    pnlSUOM.Visible = true;
                    txtUOMH.Focus();
                }
            }
        }

        private void txtSMRP_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSMRP.Text.Trim() != "")
                {
                    if (MRPC != Convert.ToDouble(txtSMRP.Text))
                    {
                        txtSASaleRate.Text = txtSMRP.Text;
                        txtSBSaleRate.Text = txtSMRP.Text;
                        txtSASaleRate.Focus();
                        FlagChange = true;
                    }
                    txtSASaleRate.Focus();
                }
                //else
                //    pnlSUOM.Visible = false;

            }

        }

        private void txtSASaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSASaleRate.Text == "")
                { txtSASaleRate.Focus(); }
                else txtSBSaleRate.Focus();

            }
        }

        private void txtSBSaleRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSBSaleRate.Text == "")
                { txtSBSaleRate.Focus(); }
                else txtSPurRate.Focus();

            }
        }

        private void txtSPurRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                EP.SetError(txtSPurRate, "");
                //if (txtSPurRate.Text.Trim() == "")
                //    txtSPurRate.Text = "0.00";
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtSPurRate.Text.Trim() == "")
                        txtSPurRate.Text = "0.00";

                    if (txtSStockConv.Enabled == false)
                    { txtUOMD.Focus(); }
                    else
                    {
                        txtSStockConv.Focus();

                    }
                }
                else
                    txtSPurRate.Focus();
            }

        }

        private void chkActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //e.SuppressKeyPress = true;
                txtHSNCode.Focus();
            }

        }

        private void txtHSNCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //e.SuppressKeyPress = true;
                txtHSNCode.Text = txtHSNCode.Text.Trim().ToUpper();
                if (txtHSNCode.Text.Trim() == "")
                {
                    txtHSNCode.Focus();
                }
                else
                {
                    txtItemType.Focus();
                }
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
            if (e.KeyCode == Keys.Left && e.Control)
            {
                if (btnPrev.Enabled) btnPrev_Click_1(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (btnFirst.Enabled) btnFirst_Click_1(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                if (btnNext.Enabled) btnNext_Click_1(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (btnLast.Enabled) btnLast_Click_1(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (btnNewBrand.Visible) btnNewBrand_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                //  BtnExit_Click(sender, e);
            }


        }
        #endregion

        #region //Navigation all method

        private void NavigationDisplay(int type)
        {
            try
            {
                long No = 0;
                if (dtSearch.Rows.Count > 0)
                {
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
                    FillField();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SetNavigation()
        {
            cntRow = 0;
            DataRow[] dr = dtSearch.Select("ItemNo=" + ID);
            if (dr.Length > 0)
            {
                cntRow = dtSearch.Rows.IndexOf(dr[0]);
            }
            else
            {
                cntRow = dtSearch.Rows.Count - 1;
            }
        }

        private void btnFirst_Click_1(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnPrev_Click_1(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnLast_Click_1(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }

        #endregion

        private void btnNewBrand_Click(object sender, EventArgs e)
        {
            if (ObjFunction.CheckAllowMenu(14) == false) return;
            Form NewF = new Master.ItemGroupAE(-1);
            ObjFunction.OpenForm(NewF);

            if (((Master.ItemGroupAE)NewF).ShortID != 0)
            {
                ObjFunction.FillList(lstGroup1, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3  ORDER BY ItemGroupName");
                if (((Master.ItemGroupAE)NewF).ShortID > 0)
                    lstGroup1.SelectedValue = ((Master.ItemGroupAE)NewF).ShortID;
                else
                    lstGroup1.SelectedValue = 0;
                txtBrandName.Focus();
            }
        }

        private void rbBigMod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                if (rbBigMod.Checked == false)
                    rbSmallMode.Focus();
                else
                    btnOKPrintBarCode.Focus();
            }
        }

        private void rbSmallMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                if (rbSmallMode.Checked == false)
                    rbBigMod.Focus();
                else
                    btnOKPrintBarCode.Focus();
            }
        }

        private void cmbMfgComp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                BtnSave.Focus();
            }
        }

        private void btnSalesIGST_Click(object sender, EventArgs e)
        {
            Form NewF = new Master.TaxPercentage(GroupType.SalesAccount, GroupType.IGST);
            ObjFunction.OpenForm(NewF);
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void lstItemType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtItemType.Text = lstItemType.Text;
                    txtIGSTSales.Focus();
                    pnlItemType.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtItemType.Focus();
                pnlItemType.Visible = false;
                lstItemType.SelectedValue = tepmGroupno;
                txtItemType.Text = lstItemType.Text;
                tepmGroupno = 0;

            }
        }

        private void txtItemType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtItemType.Text == "")
                {
                    pnlItemType.Visible = true;
                    lstItemType.Focus();
                }
                else
                {
                    pnlItemType.Visible = false;
                    txtIGSTSales.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlItemType.Visible = true;
                lstItemType.Focus();
            }
        }

        private void lstItemType_Leave(object sender, EventArgs e)
        {
            pnlItemType.Visible = false;
        }

        private void btnSalesCess_Click(object sender, EventArgs e)
        {
            Form NewF = new Master.TaxPercentage(GroupType.SalesAccount, GroupType.Cess);
            ObjFunction.OpenForm(NewF);
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

        private void DisplayCount()
        {
            lblTotalCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  Mitemmaster ", CommonFunctions.ConStr).ToString();
            lblActiveCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  Mitemmaster WHERE   (IsActive = 'true')", CommonFunctions.ConStr).ToString();
            lblDeActiveCount.Text = ObjQry.ReturnLong("SELECT  COUNT(*) FROM  Mitemmaster WHERE   (IsActive = 'false')", CommonFunctions.ConStr).ToString();
            lblCategory.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblTotalCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            label10.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblActiveCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblBarcode.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
            lblDeActiveCount.Font = ObjFunction.GetFont(FontStyle.Regular, 7);
        }

        private void txtSStockConv_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtSStockConv.Text != "")
                    {
                        txtHamali2.Focus();
                    }
                    else
                    {
                        txtSStockConv.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtWeight2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtWeight2.Text != "")
                    {
                        if (txtUOMD.Enabled == true)
                        {
                            txtUOMD.Focus();
                        }
                        else
                        { txtHSNCode.Focus(); }
                    }
                    else
                        txtWeight2.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstUOML_Leave(object sender, EventArgs e)
        {
            pnlUOML.Visible = false;
        }

        private void lstUOMH_Leave(object sender, EventArgs e)
        {
            pnlUOMH.Visible = false;
        }

        private void lstUOMD_Leave(object sender, EventArgs e)
        {
            pnlUOMD.Visible = false;
        }

        private void lstIGSTSales_Leave(object sender, EventArgs e)
        {
            pnlIGSTSales.Visible = false;
        }

        private void lstSGSTSales_Leave(object sender, EventArgs e)
        {
            pnlSGSTSales.Visible = false;
        }

        private void lstCGSTSales_Leave(object sender, EventArgs e)
        {
            pnlCGSTSales.Visible = false;
        }

        private void lstCessSales_Leave(object sender, EventArgs e)
        {
            pnlCessSales.Visible = false;
        }

        private void lstIGSTPur_Leave(object sender, EventArgs e)
        {
            pnlIGSTPur.Visible = false;
        }

        private void lstSGSTPur_Leave(object sender, EventArgs e)
        {
            pnlSGSTPur.Visible = false;
        }

        private void lstCGSTPur_Leave(object sender, EventArgs e)
        {
            pnlCGSTPur.Visible = false;
        }

        private void lstCessPur_Leave(object sender, EventArgs e)
        {
            pnlCessPur.Visible = false;
        }

        private void lstGroup1_Leave(object sender, EventArgs e)
        {
            pnlGroup1.Visible = false;
            //txtBrandName.Text = brandname;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (pnlGroup1.Visible == true)
            {
                pnlGroup1.Visible = false;
                txtBrandName.Focus();
            }
        }

        private void txtAPMCCessPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAPMCCessPer.Text == "")
                {
                    txtAPMCCessPer.Focus();
                }
                else
                {
                    txtAPMCAddCessPer.Focus();
                }
            }

        }

        private void txtAPMCAddCessPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAPMCAddCessPer.Text == "")
                {
                    txtAPMCAddCessPer.Focus();
                }
                else
                {
                    txtAPMCDhekhrekPer.Focus();
                }
            }
        }

        private void btnHSNDetails_Click(object sender, EventArgs e)
        {
            Form NewF = new Master.ItemHSNCodeDetails();
            ObjFunction.OpenForm(NewF);
        }

        private void txtHamali1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtHamali1.Text == "")
                {
                    txtHamali1.Focus();
                }
                else 
                {                    
                    txtWeight1.Focus();
                }
            }
        }

        private void txtHamali2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtHamali2.Text == "")
                {
                    txtHamali2.Focus();
                }
                else
                {
                    txtWeight2.Focus();
                }
            }
        }

        private void btnPurIGST_Click(object sender, EventArgs e)
        {

        }

        private void txtAPMCDhekhrekPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAPMCDhekhrekPer.Text == "")
                {
                    txtAPMCDhekhrekPer.Focus();
                }
                else
                {
                    txtAPMCPackingPer.Focus();
                }
            }
        }

        private void GvHSNCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FillHSNCodeTaxDetails();
            }
        }

        private void txtAPMCPackingPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAPMCPackingPer.Text == "")
                {
                    txtAPMCPackingPer.Focus();
                }
                else
                {
                    txtContainerCharges.Focus();
                }
            }
        }

        private void txtContainerCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtContainerCharges.Text == "")
                {
                    txtContainerCharges.Focus();
                }
                else
                {
                    cmbMfgComp.Focus();
                }
            }
        }

        private void dtpedate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                FromDate = Convert.ToDateTime(dtpedate.Value);
                SFromDate = Convert.ToDateTime(dtpedate.Value);
                txtAPMCCessPer.Focus();
            }
        }

        #region RackWise Details

        private void btnRackWise_Click(object sender, EventArgs e)
        {
            pnlRackDetails.Location = new Point(500, 285);
            pnlRackDetails.Height = 320;
            pnlRackDetails.Width = 515;

            pnlRackDetails.Visible = true;
            foreach (DataGridViewColumn col in GvRack.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            GvRack.ColumnHeadersHeight = 30;
            txtRackName.Enabled = true;
            txtRackUOML.Enabled = true;
            lstRackName.Enabled = true;
            lstRackUOML.Enabled = true;
            txtToQty.Enabled = true;
            chkRackIsActive.Enabled = true;
            chkRackIsActive.Checked = true;
            lblUOMLText.Text = "";
            Clear();
            RackFillList();
            BindRackGrid();
            txtRackUOML.Focus();
        }

        private void btnRackExit_Click(object sender, EventArgs e)
        {
            pnlRackDetails.Visible = false;
        }

        private void txtRackUOML_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtRackUOML.Text == "")
                {
                    pnlRackUOML.Visible = true;
                    lstRackUOML.Focus();
                    // lstCategory.SelectedIndex = 0;
                }
                else
                {
                    pnlRackUOML.Visible = false;
                    txtToQty.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlRackUOML.Visible = true;
                lstRackUOML.Focus();
            }

        }

        private void lstRackUOML_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtRackUOML.Text = lstRackUOML.Text;
                    pnlRackUOML.Visible = false;
                    lblUOMLText.Text = lstRackUOML.Text;
                    txtToQty.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtRackUOML.Focus();
                    txtRackUOML.Text = lstRackUOML.Text;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtToQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtToQty.Text == "")
                {
                    txtToQty.Focus();
                }
                else
                {
                    //for (int i = 0; i < GvRack.Rows.Count - 1; i++)
                    //{
                    //    if (Convert.ToDouble(txtToQty.Text) > Convert.ToDouble(GvRack.CurrentRow.Cells[ColIndex.Qty].Value))
                    //    {
                    //        txtToQty.Focus();
                    //    }
                    //    else
                    //    {
                    txtRackName.Focus();
                    //    }
                    //}
                }
            }
        }

        private void txtRackName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtRackName.Text == "")
                {
                    pnlRackName.Visible = true;
                    lstRackName.Focus();
                    // lstCategory.SelectedIndex = 0;
                }
                else
                {
                    pnlRackName.Visible = false;
                    chkRackIsActive.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlRackName.Visible = true;
                lstRackName.Focus();
            }
        }

        private void lstRackName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtRackName.Text = lstRackName.Text;
                    pnlRackName.Visible = false;
                    chkRackIsActive.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtRackName.Focus();
                    txtRackName.Text = lstRackName.Text;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkRackIsActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnRackOk.Focus();
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 20, 20, OMFunctions.MaskedType.NotNegative);
        }

        private void RackFillList()
        {
            ObjFunction.FillList(lstRackName, "SELECT RackNo,RackName From MRack WHERE IsActive = 'True' ORDER BY RackName");
            ObjFunction.FillList(lstRackUOML, "SELECT UOMNo,UOMName from MUOM WHERE  isActive='True' and uomno not in(1) and uomno in (select distinct uomno from MRateSetting  where itemno=" + ID +") ORDER BY UOMName");
        }

        private void btnRackOk_Click(object sender, EventArgs e)
        {
            double LastQty = 0;
            if (RackValidation() == true)
            {
                if (GvRack.Rows.Count > 1)
                {
                    LastQty = Convert.ToDouble(GvRack.Rows[GvRack.Rows.Count - 2].Cells[ColIndex.Qty].Value);
                }
                if (Convert.ToDouble(txtToQty.Text) > LastQty)
                {
                    GvRack.CurrentCell = GvRack[1, GvRack.Rows.Count - 1];
                    GvRack.CurrentRow.Cells[ColIndex.UOM].Value = txtRackUOML.Text;
                    GvRack.CurrentRow.Cells[ColIndex.Qty].Value = txtToQty.Text;
                    GvRack.CurrentRow.Cells[ColIndex.RackName].Value = txtRackName.Text;
                    GvRack.CurrentRow.Cells[ColIndex.UOMNo].Value = lstRackUOML.SelectedValue;
                    GvRack.CurrentRow.Cells[ColIndex.RackNo].Value = lstRackName.SelectedValue;
                    hidePics();
                    Clear();
                    GvRack.Rows.Add();
                    txtToQty.Focus();
                    GvRack.CurrentCell = GvRack[0, GvRack.Rows.Count - 1];
                }
                else
                {
                    OMMessageBox.Show("Enter Max Quantity....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    txtToQty.Focus();
                }
            }
        }

        private void btnRackSave_Click(object sender, EventArgs e)
        {
            try
            {
                hidePics();
                long RackDetailsNo = 0;
                for (int i = 0; i < GvRack.Rows.Count - 1; i++)
                {
                    dbMRackDetails = new DBMRackDetails();
                    mRackDetails = new MRackDetails();

                    mRackDetails.RackDetailsNo = Convert.ToInt64((GvRack.Rows[i].Cells[ColIndex.RackDetailsNo].Value));
                    mRackDetails.UOMLNo = Convert.ToInt64(GvRack[ColIndex.UOMNo, i].Value.ToString());
                    mRackDetails.FkRackNo = Convert.ToInt64(GvRack.Rows[i].Cells[ColIndex.RackNo].Value);
                    mRackDetails.ItemNo = Convert.ToInt64(lstGroup1.SelectedValue);
                    mRackDetails.ToQty = Convert.ToDouble(GvRack[ColIndex.Qty, i].Value.ToString());
                    mRackDetails.IsActive = chkRackIsActive.Checked;
                    mRackDetails.UserID = DBGetVal.UserID;
                    mRackDetails.UserDate = DBGetVal.ServerTime.Date;

                    dbMRackDetails.AddMRackDetails(mRackDetails);
                }
                if (RackDetailsNo == 0)
                {
                    OMMessageBox.Show("Rack Details Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindRackGrid();
                    pnlRackDetails.Visible = false;
                    txtRackUOML.Focus();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public bool RackValidation()
        {
            bool flag = false;

            if (txtRackUOML.Text.Trim() == "")
            {
                txtRackUOML.Focus();
            }
            else if (txtToQty.Text.Trim() == "0.00")
            {
                txtToQty.Focus();
            }
            else if (txtRackName.Text.Trim() == "")
            {
                txtRackName.Focus();
            }
            else
                flag = true;
            return flag;
        }

        private void hidePics()
        {
            pnlRackUOML.Visible = false;
            pnlRackName.Visible = false;
        }

        public void Clear()
        {
            txtToQty.Text = "0.00";
            txtRackName.Text = "";
        }

        public void BindRackGrid()
        {
            string sqlQuery = "";
            sqlQuery = " SELECT   0 As SrNo,MUOM.UOMName,MRackDetails.ToQty,MRack.RackName,MRackDetails.UOMLNo,MRack.RackNo,RackDetailsNo " +
                       " FROM MRack INNER JOIN MRackDetails ON MRack.RackNo = MRackDetails.FkRackNo INNER JOIN " +
                       " MUOM ON MRackDetails.UOMLNo = MUOM.UOMNo Where ItemNo = " + Convert.ToInt64(lstGroup1.SelectedValue) + " And MRackDetails.IsActive='True'";
            GvRack.Rows.Clear();
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GvRack.Rows.Add();
                for (int j = 0; j < GvRack.Columns.Count; j++)
                {
                    GvRack.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
            GvRack.Rows.Add();
        }

        private void GvRack_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void GvRack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                dbMRackDetails = new DBMRackDetails();
                mRackDetails = new MRackDetails();

                if (OMMessageBox.Show("Are You Sure Want to Delete this Record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    long RackDetailsNo = Convert.ToInt64(GvRack.CurrentRow.Cells[ColIndex.RackDetailsNo].Value);
                    mRackDetails.RackDetailsNo = RackDetailsNo;
                    if (RackDetailsNo == 0)
                    {
                        if (GvRack.Rows.Count - 1 == GvRack.CurrentCell.RowIndex)
                        {
                            GvRack.Rows.RemoveAt(GvRack.CurrentCell.RowIndex);
                            GvRack.Rows.Add();
                        }
                    }
                    else
                    {
                        if (dbMRackDetails.DeleteMRackDetails(mRackDetails) == true)
                        {
                            OMMessageBox.Show("Record Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            BindRackGrid();
                        }
                    }
                }
            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int UOM = 1;
            public static int Qty = 2;
            public static int RackName = 3;
            public static int UOMNo = 4;
            public static int RackNo = 5;
            public static int RackDetailsNo = 6;
        }
        #endregion

        #endregion

        public void FillHSNCodeTaxDetails()
        {
            try
            {
                //double GSTPer = 0.00;
                //txtIGSTPur.Text = (GvHSNCode.CurrentRow.Cells[0].Value).ToString() + " %";
                //lstIGSTPur.Text = (GvHSNCode.CurrentRow.Cells[0].Value).ToString() + " %";
                //lstIGSTPur.SelectedItem = lstIGSTPur.Items.Equals(lstIGSTPur.Text);
                //long Pur = Convert.ToInt32(lstIGSTPur.SelectedValue);
                //GSTPer = Convert.ToDouble(GvHSNCode.CurrentRow.Cells[0].Value) / 2;

                //lstIGSTSales.Text = (GvHSNCode.CurrentRow.Cells[0].Value).ToString() + " %";
                //lstIGSTSales.SelectedItem = lstIGSTSales.Items.Equals(lstIGSTSales.Text);
                //txtIGSTSales.Text = lstIGSTSales.Text;

                //lstSGSTPur.Text = GSTPer.ToString("0.00") + " %";
                //lstSGSTPur.SelectedItem = lstSGSTPur.Items.Equals(lstSGSTPur.Text);
                //txtSGSTPur.Text = lstSGSTPur.Text;

                //lstSGSTSales.Text = GSTPer.ToString("0.00") + " %";
                //lstSGSTSales.SelectedItem = lstSGSTSales.Items.Equals(lstSGSTSales.Text);
                //txtSGSTSales.Text = lstSGSTSales.Text;

                //lstCGSTSales.Text = GSTPer.ToString("0.00") + " %";
                //lstCGSTSales.SelectedItem = lstCGSTSales.Items.Equals(lstCGSTSales.Text);
                //txtCGSTSales.Text = lstCGSTSales.Text;

                //lstCGSTPur.Text = GSTPer.ToString("0.00") + " %";
                //lstCGSTPur.SelectedItem = lstCGSTPur.Items.Equals(lstCGSTPur.Text);
                //txtCGSTPur.Text = lstCGSTPur.Text;
                ////lstCessPur.Text = GSTPer.ToString("0.00") + " %";
                ////lstCessPur.SelectedItem = lstCessPur.Items.Equals(lstCessPur.Text);

                ////lstCessSales.Text = "0.00" + " %";
                ////lstCessSales.SelectedItem = lstCessSales.Items.Equals(lstCessSales.Text);

                //PnlHSNCode.Visible = false;

                //// lstCessSales.Text = (GvHSNCode.CurrentRow.Cells[0].Value).ToString() + " %";
                ////txtCessSales.Text = dtItemTax.Rows[0][4].ToString() + " %";

                //// lstCessPur.Text = (GvHSNCode.CurrentRow.Cells[0].Value).ToString() + " %";
                ////txtCessPur.Text = dtItemTax.Rows[0][4].ToString() + " %";
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

    }
}
