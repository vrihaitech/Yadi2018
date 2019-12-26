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
namespace Yadi.Settings.Loyalty
{
    public partial class LoyaltyPSKU : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMScheme dbMScheme = new DBMScheme();
        MScheme mScheme = new MScheme();
        MSchemeDetails mSchemeDetails = new MSchemeDetails();
        MSchemeFromDetails mSchemeFromDetails = new MSchemeFromDetails();
        MSchemeToDetails mSchemeToDetails = new MSchemeToDetails();
        MSchemeAssign mSchemeAssign = new MSchemeAssign();

        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtUOMTemp = new DataTable();
        DataTable dtVchMainDetails = new DataTable();
        DataTable dtCompRatio = new DataTable();
        DataTable dtVchPrev = new DataTable();
        DataTable dtPayLedger = new DataTable();
        DataTable dtHeader = new DataTable();
        DataTable dtSchemeFrom = new DataTable();
        DataTable dtSchemeTo = new DataTable();
        DataTable dtSchemeAssign = new DataTable();
        long TypeNo;

        Color clrColorRow = Color.FromArgb(255, 224, 192);
        long ID = 0;
        long ItemNameType = 0, RateTypeNo;
        int rowQtyIndexFrom = 0, iItemNameStartIndexFrom = 3, cntRowFrom, ItemTypeFrom = 0;
        string Param1ValueFrom = "", Param2ValueFrom = "", strUom = "";
        string[] strItemQueryFrom, strItemQuery_lastFrom;
        string SchemeName = "";
        int SchemeTypeNo = 4;
        DateTime TempdtSchFrom, TempdtSchTo;
        int rowQtyIndex = 0, iItemNameStartIndex = 3, cntRow, ItemType = 0;
        string Param1Value = "", Param2Value = "";
        string[] strItemQuery, strItemQuery_last;
        int iDiscountType = 0;

        public LoyaltyPSKU()
        {
            InitializeComponent();
        }

        public LoyaltyPSKU(int SchemeType)
        {

            SchemeTypeNo = SchemeType;
            InitializeComponent();

            if (SchemeTypeNo == 3)
                this.Text = "LoyaltyTSKU";
            else if (SchemeTypeNo == 4)
                this.Text = "LoyaltyTSKUC";
            else
                this.Text = "LoyaltyPSKU";
        }

        private void LoyaltyPSKU_Load(object sender, EventArgs e)
        {
            try
            {
                if (SchemeTypeNo == 4)
                {
                    label8.Visible = false;
                    label7.Visible = false;
                    DtpTillDateFrom.Visible = false;
                    DtpTillDateTo.Visible = false;
                    label1.Text = "Instant Item Discount - Product Combination Based";
                }
                else if (SchemeTypeNo == 3)
                {
                    label8.Visible = false;
                    label7.Visible = false;
                    DtpTillDateFrom.Visible = false;
                    DtpTillDateTo.Visible = false;
                    label1.Text = "Instant Item Discount - Qty Based";
                }
                else
                {
                    label8.Visible = true;
                    label7.Visible = true;
                    DtpTillDateFrom.Visible = true;
                    DtpTillDateTo.Visible = true;
                    label1.Text = "Quantity Purchase Over Period";
                }
                dtSchemeAssign = new DataTable();

                ObjFunction.FillDiscType(cmbDiscType);
                ObjFunction.FillCombo(cmbSponsor, "Select SponsorNo, BrandSponsorName from MBrandSponsor");

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                SetSchemeStatus(0);
                lblActivate.ForeColor = Color.Gray;
                lblClosed.ForeColor = Color.Gray;
                lblSchemeStatus.Font = new Font("Verdana", 18, FontStyle.Bold);
                lblfor.Font = new Font("Verdana", 12, FontStyle.Bold);
                lblActivate.Font = new Font("Verdana", 12, FontStyle.Bold);
                lblClosed.Font = new Font("Verdana", 12, FontStyle.Bold);
                label1.Font = new Font("Verdana", 12, FontStyle.Bold);
                lblProductmsg.Font = new Font("Verdana", 12, FontStyle.Bold);
                lblStatus.Font = new Font("Verdana", 12, FontStyle.Bold);
                RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType));
                InitDelTable();//For Delete

                ItemNameType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)); //deepak
                initItemQuery();
                initItemQueryFrom();

                formatPics();
                formatPicsFrom();
                InitTable();//From DataTable

                dtSearch = ObjFunction.GetDataView("Select SchemeNo From MScheme where CompanyNo=" + DBGetVal.FirmNo + " and SchemeTypeNo=" + SchemeTypeNo + "  order by SchemeDate").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (Settings.Loyalty.Loyalty.RequestSchemeNo != 0)
                    {
                        ID = Settings.Loyalty.Loyalty.RequestSchemeNo;
                        SchemeName = "";
                    }
                    else
                    {
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString()); ;
                    }
                    FillControls();
                    SetNavigation();

                }
                else
                {
                    //btnBASave.Enabled = false;
                    btnBgSave.Enabled = false;
                    btnEdit.Enabled = false;
                    //btncancelchild.Enabled = false;
                    //btnUpdateChild.Visible = false;
                    //btnBASave.Visible = true;
                    btnBgSave.Visible = false;
                    btnEdit.Visible = true;
                    btnUpdate.Enabled = false;
                    btnSearch.Enabled = false;
                    btnDelete.Enabled = false;
                    btnNext.Enabled = false;
                    btnPrev.Enabled = false;
                    btnLast.Enabled = false;
                    btnFirst.Enabled = false;
                }


                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);

                //btnNewChild.Text = btnNewChild.Text.Replace("&", "");
                //btnBASave.Text = btnBASave.Text.Replace("&", "");
                //btncancelchild.Text = btncancelchild.Text.Replace("&", "");
                //btnUpdateChild.Text = btnUpdateChild.Text.Replace("&", "");
                btnBgSave.Text = btnBgSave.Text.Replace("&", "");
                btnEdit.Text = btnEdit.Text.Replace("&", "");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void NavigationDisplayFrom(int type)
        {
            try
            {
                long No = 0;
                if (type == 5)
                {
                    No = Convert.ToInt64(dtHeader.Rows[dtHeader.Rows.Count - 1].ItemArray[ColdtHeader.TypeNo].ToString());
                    TypeNo = No;
                }
                else if (type == 3)
                {
                    cntRowFrom = cntRowFrom + 1;
                }
                else if (type == 4)
                {
                    cntRowFrom = cntRowFrom - 1;
                }

                if (cntRowFrom < 0)
                {
                    OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    cntRowFrom = cntRowFrom + 1;
                }
                else if (cntRowFrom > dtHeader.Rows.Count - 1)
                {
                    OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    cntRowFrom = cntRowFrom - 1;
                }
                else
                {
                    if (type != 5)
                    {
                        No = Convert.ToInt64(dtHeader.Rows[cntRowFrom].ItemArray[ColdtHeader.TypeNo].ToString());
                        TypeNo = No;
                    }
                }
                FindData(TypeNo);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillControls()
        {
            try
            {
                dtSchemeAssign = new DataTable();
                EP.SetError(txtSchemeName, "");
                EP.SetError(DtpSchemeDateFrom, "");
                EP.SetError(DtpSchemeDateTo, "");
                EP.SetError(DtpTillDateFrom, "");
                EP.SetError(DtpTillDateTo, "");
                EP.SetError(txtPercentage, "");
                EP.SetError(txtRs, "");
                btnUpdate.Visible = true; btnDelete.Visible = true;
                string sql = "Exec GetAllSchemeData " + ID + "," + DBGetVal.FirmNo + "";
                DataSet ds = ObjDset.FillDset("Demo", sql, CommonFunctions.ConStr);
                DataTable dtDetails = ds.Tables[0];
                if (dtDetails.Rows.Count > 0)
                {

                    dgBill.Enabled = false;
                    DtpTillDateFrom.MinDate = Convert.ToDateTime("1-1-1900");
                    DtpTillDateTo.MinDate = Convert.ToDateTime("1-1-1900");
                    DtpSchemeDateTo.MinDate = Convert.ToDateTime("1-1-1900");
                    DtpSchemeDateFrom.MinDate = Convert.ToDateTime("1-1-1900");

                    txtSchemeName.Text = dtDetails.Rows[0].ItemArray[0].ToString();
                    SchemeName = dtDetails.Rows[0].ItemArray[0].ToString();
                    txtSchemeUserNo.Text = dtDetails.Rows[0].ItemArray[1].ToString();
                    DtpDate.Value = Convert.ToDateTime(dtDetails.Rows[0].ItemArray[2].ToString());
                    DtpSchemeDateFrom.Value = Convert.ToDateTime(dtDetails.Rows[0].ItemArray[3].ToString());
                    TempdtSchFrom = DtpSchemeDateFrom.Value;
                    DtpSchemeDateTo.Value = Convert.ToDateTime(dtDetails.Rows[0].ItemArray[4].ToString());
                    TempdtSchTo = DtpSchemeDateTo.Value;
                    if (Convert.ToDateTime(dtDetails.Rows[0].ItemArray[5].ToString()).ToString(Format.DDMMMYYYY) != "01-Jan-1900")
                        DtpTillDateFrom.Value = Convert.ToDateTime(dtDetails.Rows[0].ItemArray[5].ToString());
                    if (Convert.ToDateTime(dtDetails.Rows[0].ItemArray[6].ToString()).ToString(Format.DDMMMYYYY) != "01-Jan-1900")
                        DtpTillDateTo.Value = Convert.ToDateTime(dtDetails.Rows[0].ItemArray[6].ToString());
                    lblActivate.ForeColor = Color.Gray;
                    lblClosed.ForeColor = Color.Gray;
                    ObjFunction.FillDiscType(cmbDiscType);
                    cmbSponsor.SelectedValue = dtDetails.Rows[0].ItemArray[9].ToString();
                    txtCampaignID.Text = dtDetails.Rows[0].ItemArray[10].ToString();

                    if (dtDetails.Rows[0].ItemArray[8].ToString() != "" && dtDetails.Rows[0].ItemArray[8].ToString() != null)
                    {
                        cmbDiscType.SelectedValue = dtDetails.Rows[0].ItemArray[8].ToString();
                        iDiscountType = (int)ObjFunction.GetComboValue(cmbDiscType);
                    }

                    //if (Convert.ToBoolean(dtDetails.Rows[0].ItemArray[7].ToString()) == true)
                    //{
                    //    lblActivate.ForeColor = Color.Green;
                    //    btnUpdate.Enabled = true;
                    //}
                    //else
                    //{
                    //    lblClosed.ForeColor = Color.Red;
                    //    btnUpdate.Enabled = false;
                    //}
                    SetSchemeStatus(Convert.ToInt32(dtDetails.Rows[0].ItemArray[7].ToString()));
                    dtHeader = ds.Tables[1];
                    dtSchemeFrom = ds.Tables[2];
                    dtSchemeTo = ds.Tables[3];

                    //btnUpdateChild.Visible = true;
                    //btncancelchild.Visible = false;
                    //btnUpdateChild.Enabled = false;
                    //btnNewChild.Enabled = false;

                    //btnBASave.Visible = false;
                    btnBgSave.Visible = false;
                    btnEdit.Visible = true;
                    btnEdit.Enabled = false;
                    btncopy.Visible = true;
                    TypeNo = Convert.ToInt64(dtHeader.Rows[dtHeader.Rows.Count - 1].ItemArray[ColdtHeader.TypeNo].ToString()); ;
                    cntRowFrom = dtHeader.Rows.Count - 1;
                    FindData(TypeNo);
                    dgBill.Enabled = false;
                    dgBillFrom.Enabled = false;

                    //if (ObjQry.ReturnLong("Select Count(*) From TRewardDetails Where SchemeNo=" + ID + "", CommonFunctions.ConStr) > 0)
                    //    btnUpdate.Visible = false;
                    //else
                    //    btnUpdate.Visible = true;

                    //DataTable dt = new DataTable();
                    //string strQuery = "SELECT 0 AS SrNo, MLedger.LedgerName, MSchemeAssign.IsActive, MSchemeAssign.LedgerNo, MSchemeAssign.PkSrNo " +
                    //    " FROM MLedger INNER JOIN MSchemeAssign ON MLedger.LedgerNo = MSchemeAssign.LedgerNo " +
                    //    " WHERE     (MSchemeAssign.SchemeNo = " + ID + ")";
                    //dtSchemeAssign = ObjFunction.GetDataView(strQuery).Table;
                    if (Convert.ToInt64(dtDetails.Rows[0].ItemArray[11].ToString()) == 1)
                    {
                        btnNew.Enabled = true;
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                        btncopy.Enabled = false;
                        btnAssignCust.Enabled = false;
                        pnlActive.Enabled = false;

                    }
                    else
                    {
                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        btncopy.Enabled = true;
                        btnAssignCust.Enabled = true;
                        pnlActive.Enabled = true;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetSchemeStatus(int StatusCode)
        {
            if (StatusCode == 0)
            {
                lblSchemeStatus.Text = "Draft";
                lblSchemeStatus.ForeColor = Color.Blue;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
            }
            else if (StatusCode == 1)
            {
                lblSchemeStatus.Text = "Active";
                lblSchemeStatus.ForeColor = Color.Green;
                btnUpdate.Visible = false;
                btnDelete.Visible = true;
            }
            else if (StatusCode == 2)
            {
                lblSchemeStatus.Text = "Closed";
                lblSchemeStatus.ForeColor = Color.Red;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }

        }

        public void InitControls()
        {
            try
            {
                iDiscountType = 0;
                //  BillAmountGrid();
                DisplayList(false);
                DtpTillDateFrom.MinDate = Convert.ToDateTime("1-1-1900");
                DtpTillDateTo.MinDate = Convert.ToDateTime("1-1-1900");
                DtpSchemeDateFrom.MinDate = Convert.ToDateTime("1-1-1900");
                DtpSchemeDateTo.MinDate = Convert.ToDateTime("1-1-1900");
                TempdtSchFrom = Convert.ToDateTime("01-Jan-1900");
                TempdtSchTo = Convert.ToDateTime("01-Jan-1900");

                dgBill.CurrentCell = null;
                dgBill.Enabled = true;
                dtHeader = null;
                dtSchemeFrom = null;
                dtSchemeTo = null;
                InitTable();
                while (dgBill.Rows.Count > 0)
                    dgBill.Rows.RemoveAt(0);
                while (dgBillFrom.Rows.Count > 0)
                    dgBillFrom.Rows.RemoveAt(0);
                txtPercentage.Text = "";
                txtRs.Text = "";
                txtSchemeName.Text = "";
                txtSchemeUserNo.Text = "";
                DtpDate.Value = DBGetVal.ServerTime.Date;
                DtpSchemeDateFrom.Text = DBGetVal.ServerTime.Date.ToString();
                DtpSchemeDateTo.Text = DBGetVal.ServerTime.Date.ToString();

                //btnUpdateChild.Visible = false;
                //btnBASave.Visible = true;
                btnBgSave.Visible = false;
                btnEdit.Visible = true;
                //btnUpdateChild.Enabled = true;
                btnEdit.Enabled = true;
                btnNew.Visible = true;
                //btnBASave.Visible = false;

                chkDiscValue.Checked = false;
                chkProductDisc.Checked = false;
                DtpDate.Enabled = false;
                SchemeName = "";
                string TempDate = "1-1-1900";// ObjQry.ReturnString(" Select IsNull(SchemePeriodTo,'1-1-1900') From MScheme Where IsActive in (0,1) AND SchemeUserNo =(Select SchemeUserNo From GetMaxSchemeUserNo(" + SchemeTypeNo + ",2))", CommonFunctions.ConStr);
                DateTime dtDate = ((TempDate == "") ? Convert.ToDateTime("1-1-1900") : Convert.ToDateTime(Convert.ToDateTime(TempDate).ToString(Format.DDMMMYYYY)).AddDays(1));
                if (dtDate.ToString(Format.DDMMMYYYY) == Convert.ToDateTime("1-1-1900").ToString(Format.DDMMMYYYY))
                    dtDate = DBGetVal.ServerTime.Date;
                if (dtDate.Date < DBGetVal.ServerTime.Date)
                    dtDate = DBGetVal.ServerTime.Date;
                DtpSchemeDateFrom.Value = dtDate;//.ToString(Format.DDMMMYYYY);
                //DtpSchemeDateFrom.MinDate = dtDate;
                //DtpSchemeDateTo.MinDate = dtDate;
                DtpSchemeDateTo.Value = dtDate.AddMonths(1).AddDays(-1);// Convert.ToDateTime(dtDate.AddMonths(1).AddDays(-1)).ToString(Format.DDMMMYYYY);

                DtpTillDateFrom.Value = DtpSchemeDateTo.Value.AddDays(1); //Convert.ToDateTime(DtpSchemeDateTo.Text).AddDays(1).ToString(Format.DDMMMYYYY);
                //DtpTillDateFrom.MinDate = Convert.ToDateTime(DtpTillDateFrom.Text);

                DtpTillDateTo.Value = DtpTillDateFrom.Value.AddDays(1);// Convert.ToDateTime(DtpTillDateFrom.Text).AddDays(1).ToString(Format.DDMMMYYYY);
                //DtpTillDateTo.MinDate = Convert.ToDateTime(DtpTillDateTo.Text);
                lblProductmsg.Text = "";
                SetSchemeStatus(0);
                btnEdit.Enabled = false;
                //btncancelchild.Enabled = false;
                lblActivate.ForeColor = Color.Gray;
                lblClosed.ForeColor = Color.Gray;
                lblActivate.ForeColor = Color.Green;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayList(bool flag)
        {
            pnlItemName.Visible = flag;
            pnlGroup1.Visible = flag;
            pnlGroup2.Visible = flag;
            pnlUOM.Visible = flag;
            pnlRate.Visible = flag;

            pnlItemNameFrom.Visible = flag;
            pnlGroup1From.Visible = flag;
            pnlGroup2From.Visible = flag;
            pnlUOMFrom.Visible = flag;
            pnlRateFrom.Visible = flag;
        }

        public void DataClrscr()
        {
            txtRs.Text = "";
            txtPercentage.Text = "";

            while (dgBill.Rows.Count > 0)
                dgBill.Rows.RemoveAt(0);
        }

        #region ProductDetails

        private void btnBgSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.ItemName].ErrorText != "" || dgBill.Rows[i].Cells[ColIndex.Quantity].ErrorText != "")
                    {
                        dgBill.Focus();
                        return;
                    }
                }
                bool validFlag = false;
                EP.SetError(txtPercentage, "");
                EP.SetError(txtRs, "");
                if (chkDiscValue.Checked == false && chkProductDisc.Checked == false)
                {
                    OMMessageBox.Show("Please Select atleast one option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                if (chkDiscValue.Checked == true)
                {
                    if (Convert.ToDouble(txtPercentage.Text) == 0)
                    {
                        EP.SetError(txtPercentage, "Enter %");
                        EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                        txtPercentage.Focus();
                        return;
                    }
                    else if (Convert.ToDouble(txtRs.Text) == 0)
                    {
                        EP.SetError(txtRs, "Enter Rs.");
                        EP.SetIconAlignment(txtRs, ErrorIconAlignment.MiddleRight);
                        txtRs.Focus();
                        return;
                    }
                    else validFlag = true;
                }
                if (chkProductDisc.Checked == true)
                {
                    if (dgBill.Rows.Count <= 1)
                    {
                        OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        dgBill.Focus();
                        return;
                    }
                    else validFlag = true;
                }
                if (validFlag)
                {
                    DataRow[] drHeader = dtHeader.Select("TypeNo=" + TypeNo);
                    foreach (DataRow row in drHeader)
                    {
                        int RowNo = Convert.ToInt32(row[ColdtHeader.TypeNo].ToString());

                        DataRow[] drSchemeTo = dtSchemeTo.Select("FKTypeNo=" + RowNo);
                        foreach (DataRow drTo in drSchemeTo)
                        {
                            dtSchemeTo.Rows.Remove(drTo);
                        }
                    }

                    AddNewData();
                    //DataClrscr();
                    FindData(TypeNo);
                    btnEdit.Visible = true;
                    btnBgSave.Visible = false;
                    pnlchk.Enabled = false;
                    pnlBg.Enabled = false;
                    pnlPerc.Enabled = false;
                    BtnSave.Focus();
                    //btnNewChild.Focus();
                    DisplayList(false);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnEdit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnSave.Focus();
            }
        }



        #endregion

        #region dgBillFromGridFrom


        private void formatPicsFrom()
        {
            try
            {
                pnlItemNameFrom.Width = dgBillFrom.Width;//560;
                pnlItemNameFrom.Height = 200;
                pnlItemNameFrom.Top = pnlBgFrom.Top + 10;
                pnlItemNameFrom.Left = pnlBgFrom.Left + 10;

                pnlGroup1From.Top = pnlBgFrom.Top + 10;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    pnlGroup1From.Left = pnlBgFrom.Left + 10;
                    pnlGroup1From.Width = dgBillFrom.Width;
                    lstGroup1From.Font = ObjFunction.GetFont(FontStyle.Regular, 11);
                    lstGroup1LangFrom.Font = ObjFunction.GetLangFont();
                    dgItemListFrom.RowTemplate.DefaultCellStyle.Font = null;
                    dgItemListFrom.Columns[2].DefaultCellStyle.Font = ObjFunction.GetLangFont();
                }
                else
                {
                    pnlGroup1From.Left = pnlBgFrom.Left + 10;
                    pnlGroup1From.Width = dgBillFrom.Width;
                }
                pnlGroup1From.Height = 200;//205;// 220;

                pnlGroup2From.Top = pnlBgFrom.Top + 10;
                pnlGroup2From.Left = pnlBgFrom.Left + 10;
                pnlGroup2From.Width = dgBillFrom.Width;
                pnlGroup2From.Height = 220;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }




        #region dgBillFrom code

        private void initItemQueryFrom()
        {
            try
            {
                DataTable dtItemQueryFrom = new DataTable();
                dtItemQueryFrom = ObjFunction.GetDataView("SELECT * from MItemNameDisplayType WHERE ItemNameTypeNo = " + ItemNameType).Table;

                if (dtItemQueryFrom.Rows.Count == 1)
                {
                    int qCount = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (dtItemQueryFrom.Rows[0]["Query" + i] != null && dtItemQueryFrom.Rows[0]["Query" + i].ToString().Trim().Length > 0)
                        {
                            qCount++;
                        }
                    }

                    strItemQueryFrom = new string[qCount];
                    strItemQuery_lastFrom = new string[qCount];
                    for (int i = 0; i < strItemQuery_lastFrom.Length; i++)
                    {
                        strItemQuery_lastFrom[i] = "";
                    }
                    qCount = 0;
                    for (int i = 1; i < 4; i++)
                    {
                        if (dtItemQueryFrom.Rows[0]["Query" + i] != null && dtItemQueryFrom.Rows[0]["Query" + i].ToString().Trim().Length > 0)
                        {
                            strItemQueryFrom[qCount] = dtItemQueryFrom.Rows[0]["Query" + i].ToString().Trim();
                            qCount++;
                        }
                    }

                    iItemNameStartIndexFrom = Convert.ToInt32(dtItemQueryFrom.Rows[0]["StartIndex"].ToString());
                    Param1ValueFrom = dtItemQueryFrom.Rows[0]["Param1Value"].ToString();
                    Param2ValueFrom = dtItemQueryFrom.Rows[0]["Param2Value"].ToString();
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

        private void delete_rowFrom()
        {
            try
            {
                //bool flag;
                if (dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.SchemeFromNo].Value != null)
                {
                    if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        long SchemeFromNo = Convert.ToInt64(dgBillFrom.Rows[dgBillFrom.CurrentRow.Index].Cells[ColIndex.SchemeFromNo].Value);
                        if (SchemeFromNo != 0)
                        {
                            DeleteDtls(1, SchemeFromNo);
                            DataRow[] drTo = dtSchemeFrom.Select("FKTypeNo=" + TypeNo + " And SchemeFromNo=" + SchemeFromNo);
                            foreach (DataRow rowTo in drTo)
                            {
                                dtSchemeFrom.Rows.Remove(rowTo);
                            }
                        }
                        if (dgBillFrom.Rows.Count - 1 == dgBillFrom.CurrentCell.RowIndex)
                        {
                            dgBillFrom.Rows.RemoveAt(dgBillFrom.CurrentCell.RowIndex);
                            dgBillFrom.Rows.Add();
                        }
                        else
                            dgBillFrom.Rows.RemoveAt(dgBillFrom.CurrentCell.RowIndex);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }


        #endregion


        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int Quantity = 2;
            public static int UOM = 3;
            public static int MRP = 4;
            public static int DiscPercentage = 5;
            public static int Barcode = 6;
            public static int PkBarCodeNo = 7;
            public static int ItemNo = 8;
            public static int SchemeFromNo = 9;
            public static int UOMNo = 10;
            public static int PkRateSettingNo = 11;
            public static int SchemeDtlsNo = 12;
            public static int Amount = 13;
            public static int BillAmount = 14;
            public static int Rate = 15;
            public static int SchemeNo = 16;



        }

        //public static class ColdtHeader
        //{
        //    public static int TypeNo = 0;
        //    public static int SchemeNo = 1;
        //    public static int DiscPercentage = 2;
        //    public static int DiscAmount = 3;
        //    public static int BillAmount = 4;
        //    public static int PkSrno = 5;
        //}

        public static class ColdtHeader
        {
            public static int TypeNo = 0;
            public static int BillAmount = 1;
            public static int SchemeNo = 2;
            public static int SchemeDetailsNo = 3;
            public static int SchemeDetailsFromNo = 4;
            public static int DiscPercentage = 5;
            public static int DiscAmount = 6;
        }

        #endregion



        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void Desc_StartFrom()
        {
            try
            {
                if (dgBillFrom.CurrentCell.Value == null || Convert.ToString(dgBillFrom.CurrentCell.Value) == "")
                {
                    ItemTypeFrom = 1;
                    FillItemListFrom(0, ItemTypeFrom); //FillItemListFrom();
                }
                else
                {
                    ItemTypeFrom = 2;
                    long[] BarcodeNo = null; long[] ItemNo = null;

                    //new code
                    switch (dgBillFrom.CurrentCell.Value.ToString().Trim().ToUpper())
                    {

                        default:
                            {
                                SearchBarcodeFromFrom(dgBillFrom.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);
                                break;
                            }

                    }

                    if (ItemNo.Length == 0 || BarcodeNo.Length == 0)
                    {
                        dgBillFrom.CurrentCell.Value = null;
                        dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = "0";

                        DisplayMessageFrom("Barcode Not Found");
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBillFrom.CurrentCell.RowIndex, ColIndex.ItemName, dgBillFrom });
                    }
                    else
                    {
                        if (ItemNo.Length > 1)
                        {
                            ItemTypeFrom = 3;
                            FillItemListFrom(0, ItemTypeFrom);//FillItemListFrom();
                        }
                        else
                        {
                            int rwindex = 0;
                            if (ItemExistFrom(ItemNo[0], out rwindex) == true)
                            {
                                if (dgBillFrom.CurrentRow.Index == rwindex)
                                {
                                    dgBillFrom.CurrentRow.Cells[ColIndex.Barcode].Value = dgBillFrom.CurrentCell.Value;
                                    Desc_MoveNextFrom(ItemNo[0], BarcodeNo[0]);
                                }
                                else
                                {
                                    dgBillFrom.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                                    dgBillFrom.CurrentCell = dgBillFrom[ColIndex.Quantity, rwindex];
                                    dgBillFrom.Focus();
                                }
                            }
                            else
                            {
                                dgBillFrom.CurrentRow.Cells[ColIndex.Barcode].Value = dgBillFrom.CurrentCell.Value;
                                Desc_MoveNextFrom(ItemNo[0], BarcodeNo[0]);
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

        private void Dis_MoveNextFrom()
        {
            try
            {
                CalculatorFrom();
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { dgBillFrom.CurrentCell.RowIndex, ColIndex.DiscPercentage, dgBillFrom });
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool ItemExist(long ItNo, out int rowIndex)
        {
            rowIndex = -1;
            try
            {
                bool flag = false;
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;
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
        public bool ItemExistFrom(long ItNo, out int rowIndex)
        {

            rowIndex = -1;
            try
            {
                bool flag = false;
                for (int i = 0; i < dgBillFrom.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBillFrom.Rows[i].Cells[ColIndex.ItemNo].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;
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

        public bool ItemExist(long ItNo, long RateSettingNo, out int rowIndex)
        {
            rowIndex = -1;
            try
            {
                bool flag = false;
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) && RateSettingNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;
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

        public bool ItemExistFrom(long ItNo, long RateSettingNo, out int rowIndex)
        {
            rowIndex = -1;
            try
            {
                bool flag = false;
                for (int i = 0; i < dgBillFrom.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgBillFrom.Rows[i].Cells[ColIndex.ItemNo].Value) && RateSettingNo == Convert.ToInt64(dgBillFrom.Rows[i].Cells[ColIndex.PkRateSettingNo].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;
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


        #region Without Barcode Methods

        private void FillItemListFrom(int qNo, int iType)
        {
            try
            {
                switch (iType)
                {
                    case 1:
                        FillItemListFrom(qNo);
                        break;
                    case 2:
                        break;
                    case 3:
                        DataTable dtBarCodeItemNo = ObjFunction.GetDataView("Select ItemNo from MStockBarcode where Barcode = '" + dgBillFrom.CurrentCell.Value + "'").Table;
                        string ItemList = "";
                        for (int i = 0; i < dtBarCodeItemNo.Rows.Count; i++)
                        {
                            if (i != 0)
                            {
                                ItemList += " Union all ";
                            }

                            //ItemList += " SELECT mItemMaster.ItemNo, mItemMaster.ItemName,MStockItems.ItemNameLang, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            //    " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                            //    " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,0 AS PurRate " +
                            //    " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                            //    " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            //    " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            //    " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                            //    " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where  " +
                            //    " mItemMaster.IsActive='true'";
                            ItemList += " SELECT mItemMaster.ItemNo, mItemMaster.ItemName,MStockItems.ItemNameLang, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                                " IsNull(MSB.CurrentStock,0) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                                " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,0 AS PurRate " +
                                " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                                " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                                " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                                " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                                " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo LEFT OUTER JOIN MStockItemBalance MSB ON MSB.ItemNo = mItemMaster.ItemNo AND MSB.MRP = MRateSetting.MRP AND MSB.GodownNo = 2  Where  " +
                                " mItemMaster.IsActive='true'";
                        }
                        ItemList += " ORDER BY mItemMaster.ItemName";

                        DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                        if (dtItemList.Rows.Count > 0)
                        {
                            dgItemListFrom.DataSource = dtItemList.DefaultView;
                            pnlItemNameFrom.Visible = true;
                            dgItemListFrom.CurrentCell = dgItemListFrom[1, 0];
                            dgItemListFrom.Focus();
                        }
                        else
                        {
                            DisplayMessageFrom("Items Not Found......");
                            dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.CurrentRow.Index];
                            dgBillFrom.Focus();
                        }
                        break;
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillItemListFrom(int qNo)
        {
            try
            {
                if (qNo == 0)
                {
                    qNo = iItemNameStartIndexFrom;
                }

                string ItemList = strItemQueryFrom[qNo - 1];

                ItemList = ItemList.Replace("@cmbRateType@", "" + "ASaleRate");

                switch (qNo)
                {
                    case 1:
                        break;
                    case 2:
                        switch (strItemQueryFrom.Length)
                        {
                            case 2:
                                ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1From.SelectedValue) != 0 ? lstGroup1From.SelectedValue.ToString() : Param1ValueFrom));
                                ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1From.SelectedValue) != 0 ? lstGroup1From.SelectedValue.ToString() : "NULL"));
                                break;
                            case 3:
                                ItemList = ItemList.Replace("@Param2@", "" + (Convert.ToInt64(lstGroup2From.SelectedValue) != 0 ? lstGroup2From.SelectedValue.ToString() : Param2ValueFrom));
                                ItemList = ItemList.Replace("@Param2NULL@", "" + (Convert.ToInt64(lstGroup2From.SelectedValue) != 0 ? lstGroup2From.SelectedValue.ToString() : "NULL"));
                                break;
                        }
                        break;
                    case 3:
                        ItemList = ItemList.Replace("@Param1@", "" + (Convert.ToInt64(lstGroup1From.SelectedValue) != 0 ? lstGroup1From.SelectedValue.ToString() : Param1ValueFrom));
                        ItemList = ItemList.Replace("@Param2@", "" + (Convert.ToInt64(lstGroup2From.SelectedValue) != 0 ? lstGroup2From.SelectedValue.ToString() : Param2ValueFrom));
                        ItemList = ItemList.Replace("@Param1NULL@", "" + (Convert.ToInt64(lstGroup1From.SelectedValue) != 0 ? lstGroup1From.SelectedValue.ToString() : "NULL"));
                        ItemList = ItemList.Replace("@Param2NULL@", "" + (Convert.ToInt64(lstGroup2From.SelectedValue) != 0 ? lstGroup2From.SelectedValue.ToString() : "NULL"));
                        break;
                }
                ItemList = ItemList.Replace("@CompNo@", "MStockItems.CompanyNo");
                ItemList = ItemList.Replace("@GodownNo@", "2");

                switch (strItemQueryFrom.Length - qNo)
                {
                    case 0:
                        if (!ItemList.Equals(strItemQuery_lastFrom[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MStockItems.LangShortDesc<>'') then mItemMaster.LangShortDesc else mItemMaster.LangFullDesc end AS ItemNameLang,");
                            DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                            strItemQuery_lastFrom[qNo - 1] = ItemList;
                            if (dtItemList.Rows.Count > 0)
                            {
                                dgItemListFrom.DataSource = dtItemList.DefaultView;
                                pnlItemNameFrom.Visible = true;
                                dgItemListFrom.CurrentCell = dgItemListFrom[1, 0];
                                dgItemListFrom.Focus();
                            }
                            else
                            {
                                DisplayMessageFrom("Items Not Found......");
                                dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.CurrentRow.Index];
                                dgBillFrom.Focus();
                            }
                        }
                        else
                        {
                            pnlItemNameFrom.Visible = true;
                            dgItemListFrom.CurrentCell = dgItemListFrom[1, 0];
                            dgItemListFrom.Focus();
                        }
                        break;
                    case 1:
                        if (!ItemList.Equals(strItemQuery_lastFrom[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                                ObjFunction.FillList(lstGroup1LangFrom, ItemList.Replace("StockGroupName from", "LanguageName from"));
                            //ObjFunction.FillList(lstGroup1Lang, ItemList);

                            ObjFunction.FillList(lstGroup1From, ItemList);
                            strItemQuery_lastFrom[qNo - 1] = ItemList;
                        }
                        pnlGroup1From.Visible = true;
                        lstGroup1From.Focus();
                        break;
                    case 2:
                        if (!ItemList.Equals(strItemQuery_lastFrom[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ObjFunction.FillList(lstGroup2From, ItemList);
                            strItemQuery_lastFrom[qNo - 1] = ItemList;
                        }
                        pnlGroup2From.Visible = true;
                        lstGroup2From.Focus();
                        break;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillItemListFrom()
        {
            try
            {
                string ItemList = "";
                if (ItemTypeFrom == 3)
                {
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                        " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                        " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        "(Select ItemNo from MStockBarcode where Barcode = '" + dgBillFrom.CurrentCell.Value + "' And IsActive ='true') And mItemMaster.IsActive='true'" +
                        " ORDER BY mItemMaster.ItemName";
                    DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                    if (dtItemList.Rows.Count > 0)
                    {
                        dgItemListFrom.DataSource = dtItemList.DefaultView;
                        pnlItemNameFrom.Visible = true;
                        dgItemListFrom.CurrentCell = dgItemListFrom[1, 0];
                        dgItemListFrom.Focus();
                    }
                    else
                    {
                        DisplayMessageFrom("Items Not Found......");
                        dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.CurrentRow.Index];
                        dgBillFrom.Focus();
                    }
                }
                else if (ItemNameType == 1)
                {
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                            " FROM MStockItems INNER JOIN " +
                            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "', NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                            " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo Where mItemMaster.ItemNo <> 1 And mItemMaster.IsActive='true'" +
                            " ORDER BY mItemMaster.ItemName";
                    DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                    if (dtItemList.Rows.Count > 0)
                    {
                        dgItemListFrom.DataSource = dtItemList.DefaultView;
                        pnlItemNameFrom.Visible = true;
                        dgItemListFrom.CurrentCell = dgItemListFrom[1, 0];
                        dgItemListFrom.Focus();
                    }
                    else
                    {
                        DisplayMessageFrom("Items Not Found......");
                        dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.CurrentRow.Index];
                        dgBillFrom.Focus();
                    }

                }
                else if (ItemNameType == 2)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup1From, ItemList);
                    pnlGroup1From.Visible = true;
                    lstGroup1From.Focus();
                    //lstGroup1.Location = new Point(dgBillFrom.Left + dgBillFrom.CurrentCell.ContentBounds.Left, dgBillFrom.Top + dgBillFrom.CurrentRow.Height + dgBillFrom.CurrentCell.ContentBounds.Top + dgBillFrom.ColumnHeadersHeight);
                }
                else if (ItemNameType == 3)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo1 from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup2From, ItemList);
                    pnlGroup2From.Visible = true;
                    lstGroup2From.Focus();
                }
                else if (ItemNameType == 4)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup1From, ItemList);
                    pnlGroup1From.Visible = true;
                    lstGroup1From.Focus();
                }
                dgBillFrom.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void lstGroup1From_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //string ItemListStr = "";
                if (e.KeyChar == 13)
                {
                    pnlGroup1From.Visible = false;

                    FillItemListFrom(strItemQueryFrom.Length);
                }
                else if (e.KeyChar == ' ')
                {
                    dgBillFrom.Focus();
                    pnlGroup1From.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup2From_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //string ItemListStr = "";
                if (e.KeyChar == 13)
                {
                    pnlGroup2From.Visible = false;

                    FillItemListFrom(strItemQueryFrom.Length - 1);
                }
                else if (e.KeyChar == ' ')
                {
                    dgBillFrom.Focus();
                    pnlGroup2From.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup1From_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F9)
                {
                    pnlGroup1From.Visible = false;
                    FillItemListFrom(1);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup1From_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                lstGroup1LangFrom.SelectedIndex = lstGroup1From.SelectedIndex;

        }

        private void lstUOMFrom_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    UOM_MoveNextFrom();
                }
                else if (e.KeyChar == ' ')
                {
                    dgBillFrom.Focus();
                    pnlUOMFrom.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void UOM_MoveNextFrom()
        {
            try
            {
                int Row = dgBillFrom.CurrentCell.RowIndex;

                if (dgBillFrom.CurrentRow.Cells[ColIndex.UOMNo].Value != null &&
                    dgBillFrom.CurrentRow.Cells[ColIndex.UOMNo].Value.ToString() != lstUOMFrom.SelectedValue.ToString())
                {
                    dgBillFrom.CurrentRow.Cells[ColIndex.Rate].Value = "0.00";//lstRate.Text;
                    dgBillFrom.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0;//lstRate.SelectedValue;
                }

                dgBillFrom.CurrentRow.Cells[ColIndex.UOM].Value = lstUOMFrom.Text;
                dgBillFrom.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOMFrom.SelectedValue);
                pnlUOMFrom.Visible = false;

                Rate_StartFrom();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void FillUOMListFrom(int RowIndex)
        {
            try
            {
                ObjFunction.FillList(lstUOMFrom, "UomNo", "UomName");

                if (ItemTypeFrom == 2)
                    strUom = " Select * From GetUomList ('" + Convert.ToString(dgBillFrom.Rows[RowIndex].Cells[ColIndex.Barcode].Value) + "',0," + Convert.ToDouble(dgBillFrom.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";
                else
                    strUom = " Select * From GetUomList (''," + Convert.ToInt64(dgBillFrom.Rows[RowIndex].Cells[ColIndex.ItemNo].Value) + "," + Convert.ToDouble(dgBillFrom.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";

                ObjFunction.FillList(lstUOMFrom, strUom);

                if (lstUOMFrom.Items.Count == 1)
                {
                    lstUOMFrom.SelectedIndex = 0;
                    UOM_MoveNextFrom();
                }
                else
                {

                    pnlUOMFrom.Visible = true;
                    lstUOMFrom.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void UOM_StartFromFrom()
        {
            try
            {
                int row = 0;
                if (dgBillFrom.CurrentCell.RowIndex == 0)
                    row = dgBillFrom.CurrentCell.RowIndex;
                else
                    row = dgBillFrom.CurrentCell.RowIndex;


                FillUOMListFrom(row);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }


        private void lstRateFrom_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == 13)
            //{
            //    dgBillFrom.CurrentRow.Cells[ColIndex.MRP].Value = lstRateFrom.Text;
            //    dgBillFrom.CurrentRow.Cells[ColIndex.Rate].Value = lstRateFrom.Text;
            //    dgBillFrom.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = lstRateFrom.SelectedValue;
            //    Dis_MoveNextFrom();
            //    pnlRateFrom.Visible = false;
            //}
            //else if (e.KeyChar == ' ')
            //{
            //    dgBillFrom.Focus();
            //    pnlRateFrom.Visible = false;
            //}
        }

        private void Rate_StartFrom()
        {
            try
            {
                string str;
                int RowIndex = dgBillFrom.CurrentCell.RowIndex;
                long ItemNo = Convert.ToInt64(dgBillFrom.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);
                long BarcodeNo = Convert.ToInt64(dgBillFrom.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value);
                long UOMNo = Convert.ToInt64(dgBillFrom.Rows[RowIndex].Cells[ColIndex.UOMNo].Value);
                double Qty = Convert.ToDouble(dgBillFrom.Rows[RowIndex].Cells[ColIndex.Quantity].Value);

                if (dgBillFrom.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value == null ||
                    Convert.ToInt64(dgBillFrom.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0)
                {
                    ObjFunction.FillList(lstRateFrom, "pksrno", "MRP");
                    if (ItemTypeFrom == 2)
                    {
                        str = "select pksrno," + "MRP" +
                            " from GetItemRateAll(" + ItemNo + "," + BarcodeNo + "," + UOMNo + ",null ,'" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }
                    else
                    {
                        str = "select pksrno," + "MRP" +
                            " from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,'" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }

                    ObjFunction.FillList(lstRateFrom, str);

                    if (lstRateFrom.Items.Count == 1)
                    {
                        if (Convert.ToDouble(lstRateFrom.Text) != 0)
                        {
                            lstRateFrom.SelectedIndex = 0;
                            dgBillFrom.Rows[RowIndex].Cells[ColIndex.Rate].Value = lstRateFrom.Text;
                            dgBillFrom.Rows[RowIndex].Cells[ColIndex.MRP].Value = lstRateFrom.Text;
                            dgBillFrom.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRateFrom.SelectedValue;

                            Rate_MoveNextFrom();
                        }
                        else
                        {
                            OMMessageBox.Show("Zero MRP Item Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            long SchemeFromNo = Convert.ToInt64(dgBillFrom.Rows[dgBillFrom.CurrentRow.Index].Cells[ColIndex.SchemeFromNo].Value);
                            if (SchemeFromNo != 0)
                            {
                                DeleteDtls(1, SchemeFromNo);
                                DataRow[] drTo = dtSchemeFrom.Select("FKTypeNo=" + TypeNo + " And SchemeFromNo=" + SchemeFromNo);
                                foreach (DataRow rowTo in drTo)
                                {
                                    dtSchemeFrom.Rows.Remove(rowTo);
                                }
                            }
                            if (dgBillFrom.Rows.Count - 1 == dgBillFrom.CurrentCell.RowIndex)
                            {
                                dgBillFrom.Rows.RemoveAt(dgBillFrom.CurrentCell.RowIndex);
                                dgBillFrom.Rows.Add();
                            }
                            else
                                dgBillFrom.Rows.RemoveAt(dgBillFrom.CurrentCell.RowIndex);
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBillFrom.Rows.Count - 1, ColIndex.ItemName, dgBillFrom });
                            dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.Rows.Count - 1];
                            dgBillFrom.Focus();
                        }
                    }
                    else if (lstRateFrom.Items.Count > 1)
                    {
                        if (Convert.ToDouble(dgBillFrom.Rows[RowIndex].Cells[ColIndex.MRP].Value) != 0)
                        {
                            lstRateFrom.Text = dgBillFrom.Rows[RowIndex].Cells[ColIndex.MRP].Value.ToString();
                            dgBillFrom.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRateFrom.SelectedValue;
                            Rate_MoveNextFrom();
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.MRP, dgBillFrom });
                            dgBillFrom.CurrentCell = dgBillFrom[ColIndex.MRP, RowIndex];
                            dgBillFrom.Focus();
                            pnlRateFrom.Visible = true;
                            lstRateFrom.Focus();
                        }
                    }
                    else
                    {
                        //error invalid Qty or UOM
                    }
                }
                else
                {

                    Rate_MoveNextFrom();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SearchBarcodeFromFrom(String strBarcode, out long[] ItemNo, out long[] BarcodeNo)
        {
            string sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
              " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
              " WHERE ((MStockBarcode.Barcode = '" + strBarcode + "') or (MStockItems.ShortCode = '" + strBarcode + "')) AND (MStockItems.IsActive = 'true') AND (MRateSetting.IsActive='true')";

            //string sql = "Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";
            //string sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
            //    " WHERE (MStockBarcode.Barcode = '" + strBarcode + "') AND (MStockItems.IsActive = 'true')";

            DataTable dt = ObjFunction.GetDataView(sql).Table;
            BarcodeNo = new long[dt.Rows.Count];
            ItemNo = new long[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BarcodeNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString());
                    ItemNo[i] = Convert.ToInt64(dt.Rows[i].ItemArray[1].ToString());
                    dgBillFrom.CurrentCell.Value = dt.Rows[i].ItemArray[2].ToString();
                }
            }
            else
            {
                //ItemNo[0] = 0;
                //BarcodeNo[0] = 0;
            }
        }



        #endregion

        public void DisplayMessageFrom(string str)
        {
            try
            {
                lblMsgFrom.Visible = true;
                lblMsgFrom.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(700);
                lblMsgFrom.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #region Datagrid ItemList Methods

        private void dgItemListFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long i = Convert.ToInt64(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[0].Value);
                    int rwindex = 0;
                    if (CheckDuplicate(i.ToString()) == true)
                    {
                        if (ItemExistFrom(i, Convert.ToInt64(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[12].Value), out rwindex) == true)
                        {
                            pnlItemNameFrom.Visible = false;
                            e.SuppressKeyPress = true;
                            dgBillFrom.CurrentCell = dgBillFrom[ColIndex.Quantity, rwindex];
                            dgBillFrom.Focus();
                        }
                        else
                        {
                            if (Convert.ToDouble(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[5].Value) != 0)
                            {
                                dgBillFrom.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[13].Value);
                                dgBillFrom.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[3].Value).ToString("0.00");//lstRate.Text;
                                dgBillFrom.CurrentRow.Cells[ColIndex.MRP].Value = Convert.ToDouble(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");//lstRate.Text;
                                dgBillFrom.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[12].Value);//lstRate.SelectedValue;
                                dgBillFrom.CurrentRow.Cells[ColIndex.UOM].Value = dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[4].Value;
                                if (dgBillFrom.CurrentRow.Cells[ColIndex.SchemeFromNo].FormattedValue.ToString() == "")
                                    dgBillFrom.CurrentRow.Cells[ColIndex.SchemeFromNo].Value = 0;
                                if (dgBillFrom.CurrentRow.Cells[ColIndex.SchemeDtlsNo].FormattedValue.ToString() == "")
                                    dgBillFrom.CurrentRow.Cells[ColIndex.SchemeDtlsNo].Value = 0;
                                if (dgBillFrom.CurrentRow.Cells[ColIndex.SchemeNo].FormattedValue.ToString() == "")
                                    dgBillFrom.CurrentRow.Cells[ColIndex.SchemeNo].Value = 0;



                                pnlItemNameFrom.Visible = false;
                                Desc_MoveNextFrom(Convert.ToInt64(dgItemListFrom.Rows[dgItemListFrom.CurrentCell.RowIndex].Cells[0].Value), 0);
                            }
                            else
                            {
                                OMMessageBox.Show("Zero MRP Item Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Duplicate Item in Specified Scheme Period", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    pnlItemNameFrom.Visible = false;
                    if (strItemQueryFrom.Length > 1)
                    {
                        pnlGroup1From.Visible = true;
                        lstGroup1From.Focus();
                    }
                    else
                    {
                        dgBillFrom.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Desc_MoveNextFrom(long ItemNo, long BarcodeNo)
        {
            try
            {
                dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = ItemNo;
                //if (dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].FormattedValue.ToString() != "") dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value = 0;
                dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;

                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL," + ItemNo + ",NULL,NULL,NULL,NULL,NULL) ").Table;//where ItemNo = " + ItemNo
                dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                if (ItemTypeFrom == 2)
                    dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBillFrom.Rows[dgBillFrom.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();

                if (dgBillFrom[2, dgBillFrom.CurrentCell.RowIndex].Value == null)
                {
                    dgBillFrom.CurrentCell = dgBillFrom[2, dgBillFrom.CurrentCell.RowIndex];
                    dgBillFrom.Focus();
                }
                else
                    Qty_MoveNextFrom();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void Qty_MoveNextFrom()
        {
            try
            {
                rowQtyIndexFrom = dgBillFrom.CurrentCell.RowIndex;

                if (dgBillFrom.CurrentRow.Cells[ColIndex.SchemeFromNo].FormattedValue.ToString() == "")
                    dgBillFrom.CurrentRow.Cells[ColIndex.SchemeFromNo].Value = 0;
                if (dgBillFrom.CurrentRow.Cells[ColIndex.SchemeDtlsNo].FormattedValue.ToString() == "")
                    dgBillFrom.CurrentRow.Cells[ColIndex.SchemeDtlsNo].Value = 0;
                if (dgBillFrom.CurrentRow.Cells[ColIndex.SchemeNo].FormattedValue.ToString() == "")
                    dgBillFrom.CurrentRow.Cells[ColIndex.SchemeNo].Value = 0;

                MovetoNext move2n = new MovetoNext(m2n);

                //Rate_MoveNextFrom();
                //if (dgBillFrom.CurrentRow.Cells[ColIndex.UOM].FormattedValue == "")
                UOM_StartFromFrom();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void Rate_MoveNextFrom()
        {
            try
            {
                if (dgBillFrom.Rows.Count == dgBillFrom.CurrentRow.Index + 1 && (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.Quantity || dgBillFrom.CurrentCell.ColumnIndex == ColIndex.MRP))
                {
                    if (dgBillFrom.Rows[dgBillFrom.CurrentRow.Index].Cells[ColIndex.SchemeDtlsNo].FormattedValue.ToString() != "")
                        dgBillFrom.Rows[dgBillFrom.CurrentRow.Index].Cells[ColIndex.SchemeDtlsNo].Value = 0;

                    dgBillFrom.Rows[dgBillFrom.CurrentRow.Index].Cells[ColIndex.BillAmount].Value = txtAmount.Text;
                    if (SchemeTypeNo != 3)
                        dgBillFrom.Rows.Add();
                    else
                        //btnBASave.Focus();
                        btnBgSave.Focus();
                }
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { dgBillFrom.Rows.Count - 1, ColIndex.ItemName, dgBillFrom });
                if (SchemeTypeNo != 3) dgBillFrom.Focus();
                CalculatorFrom();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        private void dgBillFrom_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.ItemName)
                {
                    if (dgBillFrom.CurrentCell.Value != null && Convert.ToString(dgBillFrom.CurrentCell.Value) != "")
                    {
                        Desc_StartFrom();
                    }
                }
                else if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    if (dgBillFrom.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                    {
                        if (dgBillFrom.CurrentCell.Value != null && Convert.ToString(dgBillFrom.CurrentCell.Value) != "")
                        {
                            if (dgBillFrom.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                            {
                                if (dgBillFrom.CurrentRow.Cells[ColIndex.Quantity].FormattedValue.ToString() != "" && dgBillFrom.CurrentRow.Cells[ColIndex.Quantity].FormattedValue.ToString().Trim() != "0")
                                {
                                    dgBillFrom.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "";
                                    dgBillFrom.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "";
                                    Qty_MoveNextFrom();
                                    CalculatorFrom();
                                }
                                else
                                {
                                    dgBillFrom.CurrentCell.ErrorText = "Enter Valid Qty";
                                    dgBillFrom.CurrentCell = dgBillFrom.CurrentRow.Cells[ColIndex.Quantity];
                                }
                            }
                            else
                            {
                                dgBillFrom.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "Select Items";
                                dgBillFrom.CurrentCell = dgBillFrom.CurrentRow.Cells[ColIndex.ItemName];
                                dgBillFrom.Focus();
                            }
                        }
                        else
                        {
                            dgBillFrom.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "Enter Valid Qty";
                            dgBillFrom.CurrentCell = dgBillFrom.CurrentRow.Cells[ColIndex.Quantity];
                        }
                    }
                }
                else if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    if (dgBillFrom.CurrentCell.Value != null && Convert.ToString(dgBillFrom.CurrentCell.Value) != "")
                    {
                        Rate_MoveNextFrom();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBillFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    delete_rowFrom();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    dgBillFrom.Focus();
                    if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.SrNo)
                    {
                        e.SuppressKeyPress = true;
                        dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.CurrentCell.RowIndex];
                    }
                    else if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        e.SuppressKeyPress = true;
                        dgBillFrom.Focus();
                        if (dgBillFrom.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value == null)
                        {
                            dgBillFrom.CurrentCell.Value = "";
                            Desc_StartFrom();
                        }
                        else if (Convert.ToInt64(dgBillFrom.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value) == 0)
                        {
                            dgBillFrom.CurrentCell.Value = "";
                            Desc_StartFrom();
                        }
                    }
                    else if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBillFrom.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                        {
                            dgBillFrom.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "";
                            dgBillFrom.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "";
                            if (dgBillFrom.CurrentCell.Value == null) dgBillFrom.CurrentCell.Value = "1";
                            if (dgBillFrom.CurrentCell.Value.ToString().Trim() == "0") dgBillFrom.CurrentCell.Value = "1";
                            Qty_MoveNextFrom();
                            CalculatorFrom();
                        }
                        else
                        {
                            dgBillFrom.CurrentCell = dgBillFrom.CurrentRow.Cells[ColIndex.ItemName];
                            dgBillFrom.Focus();
                        }
                    }
                    else if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBillFrom.CurrentCell.Value == null) dgBillFrom.CurrentCell.Value = "0";
                        Rate_MoveNextFrom();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    //if (dgBillFrom.Rows.Count <= 1)
                    //{
                    //    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    //    return;
                    //    dgBillFrom.Focus();
                    //}
                    //else
                    //btnBASave.Focus();
                    pnlBgFrom.Enabled = false;
                    pnlBg.Enabled = false;
                    pnlchk.Enabled = false;
                    btnEdit.Enabled = true;
                    //btnPrev1.Enabled = true;
                    //btnNext1.Enabled = true;
                    btnBASave_Click(sender, new EventArgs());
                    btnEdit.Focus();
                    //btnBgSave.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBillFrom_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            if (e.ColumnIndex == ColIndex.ItemName)
            {
                if (dgBillFrom.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value != null && dgBillFrom.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "")
                {
                    if (dgBillFrom.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "0")
                    {
                        dgBillFrom.Rows[e.RowIndex].Cells[ColIndex.ItemName].ReadOnly = true;
                    }
                }
            }
        }

        private void dgBillFrom_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtQuantityFrom_TextChanged);
            }
        }


        public void CalculatorFrom()
        {
            try
            {
                double Amount = 0, Qty = 0;
                for (int i = 0; i < dgBillFrom.Rows.Count; i++)
                {
                    if (dgBillFrom.Rows[i].Cells[ColIndex.Quantity].FormattedValue.ToString() != "" || dgBillFrom.Rows[i].Cells[ColIndex.MRP].FormattedValue.ToString() != "")
                    {
                        Amount = Amount + (Convert.ToDouble(dgBillFrom.Rows[i].Cells[ColIndex.Quantity].Value) * Convert.ToDouble(dgBillFrom.Rows[i].Cells[ColIndex.MRP].Value));
                        Qty = Convert.ToDouble(dgBillFrom.Rows[i].Cells[ColIndex.Quantity].Value);
                        dgBillFrom.Rows[i].Cells[ColIndex.Amount].Value = (Convert.ToDouble(dgBillFrom.Rows[i].Cells[ColIndex.Quantity].Value) * Convert.ToDouble(dgBillFrom.Rows[i].Cells[ColIndex.MRP].Value));
                    }
                }
                txtAmount.Text = Amount.ToString("0.00");
                txtQty.Text = Qty.ToString("0.0");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void lstRateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (Convert.ToDouble(lstRateFrom.Text) != 0)
                    {
                        e.SuppressKeyPress = true;
                        dgBillFrom.CurrentRow.Cells[ColIndex.MRP].Value = lstRateFrom.Text;
                        dgBillFrom.CurrentRow.Cells[ColIndex.Rate].Value = lstRateFrom.Text;
                        //dgBillFrom.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = lstRateFrom.SelectedValue;
                        //dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.Rows.Count - 1];
                        //dgBillFrom.Focus();
                        Rate_MoveNextFrom();
                        pnlRateFrom.Visible = false;
                        CalculatorFrom();
                    }
                    else
                    {
                        OMMessageBox.Show("Zero MRP Item Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        long SchemeFromNo = Convert.ToInt64(dgBillFrom.Rows[dgBillFrom.CurrentRow.Index].Cells[ColIndex.SchemeFromNo].Value);
                        if (SchemeFromNo != 0)
                        {
                            DeleteDtls(1, SchemeFromNo);
                            DataRow[] drTo = dtSchemeFrom.Select("FKTypeNo=" + TypeNo + " And SchemeFromNo=" + SchemeFromNo);
                            foreach (DataRow rowTo in drTo)
                            {
                                dtSchemeFrom.Rows.Remove(rowTo);
                            }
                        }
                        pnlRateFrom.Visible = false;
                        if (dgBillFrom.Rows.Count - 1 == dgBillFrom.CurrentCell.RowIndex)
                        {
                            dgBillFrom.Rows.RemoveAt(dgBillFrom.CurrentCell.RowIndex);
                            dgBillFrom.Rows.Add();
                        }
                        else
                            dgBillFrom.Rows.RemoveAt(dgBillFrom.CurrentCell.RowIndex);
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBillFrom.Rows.Count - 1, ColIndex.ItemName, dgBillFrom });
                        dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, dgBillFrom.Rows.Count - 1];
                        dgBillFrom.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    e.SuppressKeyPress = true;
                    dgBillFrom.Focus();
                    pnlRateFrom.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        #region dgBillGrid

        private void formatPics()
        {
            try
            {
                pnlItemName.Width = dgBill.Width;//560;
                pnlItemName.Height = 200;
                pnlItemName.Top = pnlBg.Top + 10;
                pnlItemName.Left = pnlBg.Left + 10;

                pnlGroup1.Top = pnlBg.Top + 10;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    pnlGroup1.Left = pnlBg.Left + 10;
                    pnlGroup1.Width = dgBill.Width;
                    lstGroup1.Font = ObjFunction.GetFont(FontStyle.Regular, 11);
                    lstGroup1Lang.Font = ObjFunction.GetLangFont();
                    dgItemList.RowTemplate.DefaultCellStyle.Font = null;
                    dgItemList.Columns[2].DefaultCellStyle.Font = ObjFunction.GetLangFont();
                }
                else
                {
                    pnlGroup1.Left = pnlBg.Left + 10;
                    pnlGroup1.Width = dgBill.Width;
                }
                pnlGroup1.Height = 200;//205;// 220;

                pnlGroup2.Top = pnlBg.Top + 10;
                pnlGroup2.Left = pnlBg.Left + 10;
                pnlGroup2.Width = dgBill.Width;
                pnlGroup2.Height = 220;
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
            try
            {
                if (dtDelete != null)
                {
                    for (int i = 0; i < dtDelete.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
                        {
                            mSchemeFromDetails.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                            dbMScheme.DeleteMSchemeFromDetails(mSchemeFromDetails);
                        }
                        else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 2)
                        {
                            mSchemeToDetails.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                            dbMScheme.DeleteMSchemeToDetails(mSchemeToDetails);
                        }
                        else if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 3)
                        {
                            mSchemeDetails.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                            dbMScheme.DeleteMSchemeDetails(mSchemeDetails);
                        }
                    }
                    dtDelete.Rows.Clear();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #endregion


        #region dgBill code

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

        private void delete_row()
        {
            try
            {
                //bool flag;
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SchemeFromNo].Value != null)
                {
                    if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        long SchemeToNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SchemeFromNo].Value);
                        if (SchemeToNo != 0)
                        {
                            DeleteDtls(2, SchemeToNo);
                            DataRow[] drTo = dtSchemeTo.Select("FKTypeNo=" + TypeNo + " And SchemeFromNo=" + SchemeToNo);
                            foreach (DataRow rowTo in drTo)
                            {
                                dtSchemeTo.Rows.Remove(rowTo);
                            }
                        }
                        if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                        {
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            dgBill.Rows.Add();
                        }
                        else
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
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

        }

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            if (e.ColumnIndex == ColIndex.ItemName)
            {
                if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value != null && dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "")
                {
                    if (dgBill.Rows[e.RowIndex].Cells[ColIndex.PkBarCodeNo].Value.ToString() != "0")
                    {
                        dgBill.Rows[e.RowIndex].Cells[ColIndex.ItemName].ReadOnly = true;
                    }
                }
            }
            //dgBill.CurrentRow.Selected = true;
        }

        private void lstRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (Convert.ToDouble(lstRate.Text) != 0)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentRow.Cells[ColIndex.MRP].Value = lstRate.Text;
                        dgBill.CurrentRow.Cells[ColIndex.Rate].Value = lstRate.Text;
                        dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;
                        pnlRate.Visible = false;
                        Calculator();
                        dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, dgBill.CurrentRow.Index];
                        dgBill.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Zero MRP Item Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        long SchemeToNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SchemeFromNo].Value);
                        if (SchemeToNo != 0)
                        {
                            DeleteDtls(2, SchemeToNo);
                            DataRow[] drTo = dtSchemeTo.Select("FKTypeNo=" + TypeNo + " And SchemeFromNo=" + SchemeToNo);
                            foreach (DataRow rowTo in drTo)
                            {
                                dtSchemeTo.Rows.Remove(rowTo);
                            }
                        }
                        pnlRate.Visible = false;
                        if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                        {
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            dgBill.Rows.Add();
                        }
                        else
                            dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, ColIndex.ItemName, dgBill });
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        dgBill.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Space)
                {
                    e.SuppressKeyPress = true;
                    dgBill.Focus();
                    pnlRate.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    delete_row();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.SrNo)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentCell.RowIndex];
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.Focus();
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
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                        {
                            dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "";
                            dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "";
                            if (dgBill.CurrentCell.Value == null) dgBill.CurrentCell.Value = "1";
                            Qty_MoveNext();
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.ItemName];
                            dgBill.Focus();
                        }
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentCell.Value == null) dgBill.CurrentCell.Value = "0";
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                        {
                            if (dgBill.CurrentRow.Cells[ColIndex.Quantity].FormattedValue.ToString() != "" && dgBill.CurrentRow.Cells[ColIndex.Quantity].FormattedValue.ToString().Trim() != "0")
                            {
                                dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "";
                                dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "";
                                Rate_MoveNext();
                            }
                            else
                            {
                                dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "Enter Valid Qty";
                                dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.Quantity];
                                dgBill.Focus();
                            }
                        }
                        else
                        {
                            dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.ItemName];
                            dgBill.Focus();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    if (dgBill.Rows.Count <= 1)
                    {
                        OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        return;
                        //dgBill.Focus();
                    }
                    else
                        btnBgSave.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
            }
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
            {
                TextBox txtrate = (TextBox)e.Control;
                txtrate.TextChanged += new EventHandler(txtPercentage_TextChanged);
            }

        }

        private void Desc_Start()
        {
            try
            {
                if (dgBill.CurrentCell.Value == null || Convert.ToString(dgBill.CurrentCell.Value) == "")
                {
                    ItemType = 1;
                    FillItemList(0, ItemType); //FillItemList();
                }
                else
                {
                    ItemType = 2;
                    long[] BarcodeNo = null; long[] ItemNo = null;

                    //new code
                    switch (dgBill.CurrentCell.Value.ToString().Trim().ToUpper())
                    {
                        default:
                            {
                                SearchBarcode(dgBill.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);
                                break;
                            }

                    }

                    if (ItemNo.Length == 0 || BarcodeNo.Length == 0)
                    {
                        dgBill.CurrentCell.Value = null;
                        dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = "0";
                        DisplayMessage("Barcode Not Found");
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.ItemName, dgBill });
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
                            if (ItemExist(ItemNo[0], out rwindex) == true)
                            {
                                if (dgBill.CurrentRow.Index == rwindex)
                                {
                                    dgBill.CurrentRow.Cells[ColIndex.Barcode].Value = dgBill.CurrentCell.Value;
                                    Desc_MoveNext(ItemNo[0], BarcodeNo[0]);
                                }
                                else
                                {
                                    dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                                    dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                                    dgBill.Focus();
                                }
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

        private void Dis_MoveNext()
        {
            try
            {
                Calculator();
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { dgBill.CurrentCell.RowIndex, ColIndex.DiscPercentage, dgBill });
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region Without Barcode Methods

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

                            //ItemList += " SELECT mItemMaster.ItemNo, mItemMaster.ItemName,MStockItems.ItemNameLang, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            //    " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                            //    " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault ,0 AS PurRate " +
                            //    " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                            //    " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            //    " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            //    " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                            //    " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where  " +
                            //    " mItemMaster.IsActive='true'";
                            ItemList += " SELECT mItemMaster.ItemNo, mItemMaster.ItemName,MStockItems.ItemNameLang, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                                " IsNull(MSB.CurrentStock,0) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                                " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault,0 AS PurRate " +
                                " FROM MStockItems_V(NULL," + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ",NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                                " dbo.GetItemRateAll(" + dtBarCodeItemNo.Rows[i].ItemArray[0].ToString() + ", NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                                " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                                " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                                " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo LEFT OUTER JOIN MStockItemBalance MSB ON MSB.ItemNo = mItemMaster.ItemNo AND MSB.MRP = MRateSetting.MRP AND MSB.GodownNo = 2  Where  " +
                                " mItemMaster.IsActive='true'";
                        }
                        ItemList += " ORDER BY mItemMaster.ItemName";

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
                            dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentRow.Index];
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

                ItemList = ItemList.Replace("@cmbRateType@", "" + "ASaleRate");

                switch (qNo)
                {
                    case 1:
                        break;
                    case 2:
                        switch (strItemQuery.Length)
                        {
                            case 2:
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
                ItemList = ItemList.Replace("@CompNo@", "MStockItems.CompanyNo");
                ItemList = ItemList.Replace("@GodownNo@", "2");
                switch (strItemQuery.Length - qNo)
                {
                    case 0:
                        if (!ItemList.Equals(strItemQuery_last[qNo - 1], StringComparison.CurrentCultureIgnoreCase))
                        {
                            ItemList = ItemList.Replace("AS ItemName,", "AS ItemName,Case When(MStockItems.LangShortDesc<>'') then mItemMaster.LangShortDesc else mItemMaster.LangFullDesc end AS ItemNameLang,");
                            DataTable dtItemList = ObjFunction.GetDataView(ItemList).Table;
                            strItemQuery_last[qNo - 1] = ItemList;
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
                                dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentRow.Index];
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
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                                ObjFunction.FillList(lstGroup1Lang, ItemList.Replace("StockGroupName from", "LanguageName from"));
                            //ObjFunction.FillList(lstGroup1Lang, ItemList);

                            ObjFunction.FillList(lstGroup1, ItemList);
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
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                        " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " + //MItemTaxInfo_Sale.Percentage AS SaleTax , MItemTaxInfo_Pur.Percentage
                        " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                        " FROM MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) AS MStockItems INNER JOIN " +
                        " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                        " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                        " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                        " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo  Where mItemMaster.ItemNo in " +
                        "(Select ItemNo from MStockBarcode where Barcode = '" + dgBill.CurrentCell.Value + "' And IsActive ='true') And mItemMaster.IsActive='true'" +
                        " ORDER BY mItemMaster.ItemName";
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
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentRow.Index];
                        dgBill.Focus();
                    }
                }
                else if (ItemNameType == 1)
                {
                    ItemList = " SELECT mItemMaster.ItemNo, mItemMaster.ItemName, MRateSetting.ASaleRate AS SaleRate, MUOM.UOMName, MRateSetting.MRP, " +
                            " (SELECT  ISNULL(SUM(CASE WHEN trncode = 1 THEN quantity ELSE - quantity END), 0) +  ISNULL(SUM(CASE WHEN trncode = 1 THEN FreeQty ELSE - FreeQty END), 0)   FROM TStock  INNER JOIN TStockGodown ON TSTock.PKStockTrnNo=TStockGodown.FKStockTrnNo   WHERE  TStockGodown.GodownNo=2 AND    (FkRateSettingNo IN  (SELECT PkSrNo  FROM MRateSetting AS MRateSetting_1  WHERE (TStock.ItemNo = mItemMaster.ItemNo) AND (MRP = MRateSetting.MRP))) AND (TStock.ItemNo = mItemMaster.ItemNo) And PkStockTrnNo Not In (SELECT TStock.PkStockTrnNo FROM TVoucherEntry INNER JOIN  TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo WHERE (TVoucherEntry.IsCancel = 'True') AND (TStock.ItemNo = mItemMaster.ItemNo))) AS Stock, '' AS stkUOM, 0 AS SaleTax, 0 AS PurTax, " +//MItemTaxInfo_Sale.Percentage,MItemTaxInfo_Pur.Percentage
                            " mItemMaster.CompanyNo, MStockBarcode.Barcode, MRateSetting.PkSrNo As RateSettingNo, mItemMaster.UOMDefault " +
                            " FROM MStockItems INNER JOIN " +
                            " dbo.GetItemRateAll(NULL, NULL, NULL, NULL, '" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "', NULL) AS MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo AND  " +
                            " mItemMaster.UOMDefault = MRateSetting.UOMNo INNER JOIN " +
                            " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                            " MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo Where mItemMaster.ItemNo <> 1 And mItemMaster.IsActive='true'" +
                            " ORDER BY mItemMaster.ItemName";
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
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.CurrentRow.Index];
                        dgBill.Focus();
                    }

                }
                else if (ItemNameType == 2)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup1, ItemList);
                    pnlGroup1.Visible = true;
                    lstGroup1.Focus();
                    //lstGroup1.Location = new Point(dgBill.Left + dgBill.CurrentCell.ContentBounds.Left, dgBill.Top + dgBill.CurrentRow.Height + dgBill.CurrentCell.ContentBounds.Top + dgBill.ColumnHeadersHeight);
                }
                else if (ItemNameType == 3)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo1 from MStockItems WHERE ItemNo<>1) order by StockGroupName";
                    ObjFunction.FillList(lstGroup2, ItemList);
                    pnlGroup2.Visible = true;
                    lstGroup2.Focus();
                }
                else if (ItemNameType == 4)
                {
                    ItemList = "Select StockGroupNo,StockGroupName from MStockGroup where StockGroupNo in (select Distinct GroupNo from MStockItems WHERE ItemNo<>1) order by StockGroupName";
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
                //string ItemListStr = "";
                if (e.KeyChar == 13)
                {
                    pnlGroup1.Visible = false;

                    FillItemList(strItemQuery.Length);
                }
                else if (e.KeyChar == ' ')
                {
                    dgBill.Focus();
                    pnlGroup1.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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

        private void lstUOM_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == 13)
                {
                    UOM_MoveNext();
                }
                else if (e.KeyChar == ' ')
                {
                    dgBill.Focus();
                    pnlUOM.Visible = false;
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
                    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0;//lstRate.SelectedValue;
                }

                dgBill.CurrentRow.Cells[ColIndex.UOM].Value = lstUOM.Text;
                dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOM.SelectedValue);
                pnlUOM.Visible = false;

                Rate_Start();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
        private void FillUOMList(int RowIndex)
        {
            try
            {
                ObjFunction.FillList(lstUOM, "UomNo", "UomName");

                if (ItemType == 2)
                    strUom = " Select * From GetUomList ('" + Convert.ToString(dgBill.Rows[RowIndex].Cells[ColIndex.Barcode].Value) + "',0," + Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";
                else
                    strUom = " Select * From GetUomList (''," + Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value) + "," + Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value) + ")";

                ObjFunction.FillList(lstUOM, strUom);

                if (lstUOM.Items.Count == 1)
                {
                    lstUOM.SelectedIndex = 0;
                    UOM_MoveNext();
                }
                else
                {

                    pnlUOM.Visible = true;
                    lstUOM.Focus();
                }
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
                dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, row];

                FillUOMList(row);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }


        private void lstRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    dgBill.CurrentRow.Cells[ColIndex.Rate].Value = lstRate.Text;
            //    dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;
            //    Dis_MoveNext();
            //    pnlRate.Visible = false;
            //}
            //else if (e.KeyChar == ' ')
            //{
            //    dgBill.Focus();
            //    pnlRate.Visible = false;
            //}
        }

        private void Rate_Start()
        {
            try
            {
                string str;
                int RowIndex = dgBill.CurrentCell.RowIndex;
                long ItemNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.ItemNo].Value);
                long BarcodeNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkBarCodeNo].Value);
                long UOMNo = Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.UOMNo].Value);
                double Qty = Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.Quantity].Value);

                if (dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value == null ||
                    Convert.ToInt64(dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value) == 0)
                {
                    ObjFunction.FillList(lstRate, "pksrno", "MRP");
                    if (ItemType == 2)
                    {
                        str = "select pksrno," + "MRP" +
                            " from GetItemRateAll(" + ItemNo + "," + BarcodeNo + "," + UOMNo + ",null ,'" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }
                    else
                    {
                        str = "select pksrno," + "MRP" +
                            " from GetItemRateAll(" + ItemNo + ",null," + UOMNo + ",null ,'" + DtpDate.Value.ToString(Format.DDMMMYYYY) + " " + DBGetVal.ServerTime.ToLongTimeString() + "',null)";
                    }

                    ObjFunction.FillList(lstRate, str);

                    if (lstRate.Items.Count == 1)
                    {
                        if (Convert.ToDouble(lstRate.Text) != 0)
                        {
                            lstRate.SelectedIndex = 0;
                            dgBill.Rows[RowIndex].Cells[ColIndex.Rate].Value = lstRate.Text;
                            dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value = lstRate.Text;
                            dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;

                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.DiscPercentage, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, RowIndex];
                            dgBill.Focus();
                        }
                        else
                        {
                            OMMessageBox.Show("Zero MRP Item Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            long SchemeToNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SchemeFromNo].Value);
                            if (SchemeToNo != 0)
                            {
                                DeleteDtls(2, SchemeToNo);
                                DataRow[] drTo = dtSchemeTo.Select("FKTypeNo=" + TypeNo + " And SchemeFromNo=" + SchemeToNo);
                                foreach (DataRow rowTo in drTo)
                                {
                                    dtSchemeTo.Rows.Remove(rowTo);
                                }
                            }
                            if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                            {
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                                dgBill.Rows.Add();
                            }
                            else
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, ColIndex.ItemName, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                            dgBill.Focus();
                        }
                    }
                    else if (lstRate.Items.Count > 1)
                    {
                        if (Convert.ToDouble(dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value) != 0)
                        {
                            lstRate.Text = dgBill.Rows[RowIndex].Cells[ColIndex.MRP].Value.ToString();
                            dgBill.Rows[RowIndex].Cells[ColIndex.PkRateSettingNo].Value = lstRate.SelectedValue;
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.DiscPercentage, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, RowIndex];
                            dgBill.Focus();
                        }
                        else
                        {
                            MovetoNext move2n = new MovetoNext(m2n);
                            BeginInvoke(move2n, new object[] { RowIndex, ColIndex.MRP, dgBill });
                            dgBill.CurrentCell = dgBill[ColIndex.MRP, RowIndex];
                            dgBill.Focus();
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
                    BeginInvoke(move2n, new object[] { RowIndex, ColIndex.DiscPercentage, dgBill });
                    dgBill.CurrentCell = dgBill[ColIndex.DiscPercentage, RowIndex];
                    dgBill.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SearchBarcode(String strBarcode, out long[] ItemNo, out long[] BarcodeNo)
        {

            string sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo,MStockBarcode.Barcode FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
              " INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo " +
              " WHERE ((MStockBarcode.Barcode = '" + strBarcode + "') or (MStockItems.ShortCode = '" + strBarcode + "')) AND (MStockItems.IsActive = 'true') AND (MRateSetting.IsActive='true')";

            //string sql = "Select PkStockBarcodeNo, ItemNo from MStockBarcode where Barcode = '" + strBarcode + "' And IsActive ='true'";
            //string sql = "SELECT     MStockBarcode.PkStockBarcodeNo, MStockBarcode.ItemNo FROM MStockBarcode INNER JOIN MStockItems ON MStockBarcode.ItemNo = mItemMaster.ItemNo " +
            //    " WHERE (MStockBarcode.Barcode = '" + strBarcode + "') AND (MStockItems.IsActive = 'true')";
            DataTable dt = ObjFunction.GetDataView(sql).Table;
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
                //ItemNo[0] = 0;
                //BarcodeNo[0] = 0;
            }
        }

        #endregion

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(700);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstGroup1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F9)
                {
                    pnlGroup1.Visible = false;
                    FillItemList(1);
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
                lstGroup1Lang.SelectedIndex = lstGroup1.SelectedIndex;

        }

        #region Datagrid ItemList Methods

        private void dgItemList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long i = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value);
                    int rwindex = 0;
                    if (ItemExist(i, Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value), out rwindex) == true)
                    {
                        e.SuppressKeyPress = true;
                        pnlItemName.Visible = false;
                        if (rwindex != dgBill.CurrentRow.Index) dgBill.CurrentRow.Cells[ColIndex.ItemName].Value = "";
                        dgBill.CurrentCell = dgBill[ColIndex.Quantity, rwindex];
                        dgBill.Focus();
                    }
                    else
                    {
                        if (Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value) != 0)
                        {
                            dgBill.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[13].Value);
                            dgBill.CurrentRow.Cells[ColIndex.Rate].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[3].Value).ToString("0.00");//lstRate.Text;
                            dgBill.CurrentRow.Cells[ColIndex.MRP].Value = Convert.ToDouble(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[5].Value).ToString("0.00");//lstRate.Text;
                            dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[12].Value);//lstRate.SelectedValue;
                            dgBill.CurrentRow.Cells[ColIndex.UOM].Value = dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[4].Value;
                            if (dgBill.CurrentRow.Cells[ColIndex.SchemeFromNo].FormattedValue.ToString() == "")
                                dgBill.CurrentRow.Cells[ColIndex.SchemeFromNo].Value = 0;
                            if (dgBill.CurrentRow.Cells[ColIndex.SchemeDtlsNo].FormattedValue.ToString() == "")
                                dgBill.CurrentRow.Cells[ColIndex.SchemeDtlsNo].Value = 0;
                            if (dgBill.CurrentRow.Cells[ColIndex.SchemeNo].FormattedValue.ToString() == "")
                                dgBill.CurrentRow.Cells[ColIndex.SchemeNo].Value = 0;



                            pnlItemName.Visible = false;
                            Desc_MoveNext(Convert.ToInt64(dgItemList.Rows[dgItemList.CurrentCell.RowIndex].Cells[0].Value), 0);
                        }
                        else
                        {
                            OMMessageBox.Show("Zero MRP Item Not Allowed", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            pnlItemName.Visible = false;
                            dgBill.Focus();
                            dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                            dgBill.CurrentCell.Value = "";
                        }
                    }
                }
                else if (e.KeyCode == Keys.Space)
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
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Desc_MoveNext(long ItemNo, long BarcodeNo)
        {
            try
            {
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemNo].Value = ItemNo;
                //if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].FormattedValue.ToString() != "") dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkRateSettingNo].Value = 0;
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = BarcodeNo;

                DataTable dtItem = ObjFunction.GetDataView("Select ItemName,CompanyNo from MStockItems_V(NULL," + ItemNo + ",NULL,NULL,NULL,NULL,NULL) ").Table;//where ItemNo = " + ItemNo
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value = dtItem.Rows[0].ItemArray[0].ToString();
                if (ItemType == 2)
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.ItemName].Value += " - " + dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Barcode].Value.ToString();

                if (dgBill[2, dgBill.CurrentCell.RowIndex].Value == null)
                {
                    dgBill.CurrentCell = dgBill[2, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
                }
                else
                    Qty_MoveNext();
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

                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { rowQtyIndex, ColIndex.DiscPercentage, dgBill });

                if (dgBill.CurrentRow.Cells[ColIndex.SchemeFromNo].FormattedValue.ToString() == "")
                    dgBill.CurrentRow.Cells[ColIndex.SchemeFromNo].Value = 0;
                if (dgBill.CurrentRow.Cells[ColIndex.SchemeDtlsNo].FormattedValue.ToString() == "")
                    dgBill.CurrentRow.Cells[ColIndex.SchemeDtlsNo].Value = 0;
                if (dgBill.CurrentRow.Cells[ColIndex.SchemeNo].FormattedValue.ToString() == "")
                    dgBill.CurrentRow.Cells[ColIndex.SchemeNo].Value = 0;
                if (dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].FormattedValue.ToString() == "")
                    dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = 100;

                UOM_Start();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void Rate_MoveNext()
        {
            try
            {
                if (dgBill.Rows.Count == dgBill.CurrentRow.Index + 1 && dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    if (dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.SchemeDtlsNo].FormattedValue.ToString() != "")
                        dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.SchemeDtlsNo].Value = 0;

                    dgBill.Rows[dgBill.CurrentRow.Index].Cells[ColIndex.BillAmount].Value = txtAmount.Text;

                    dgBill.Rows.Add();
                }
                MovetoNext move2n = new MovetoNext(m2n);
                BeginInvoke(move2n, new object[] { dgBill.Rows.Count - 1, ColIndex.ItemName, dgBill });
                Calculator();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void dgItemList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgItemList.CurrentCell != null)
                {
                    for (int i = 0; i < dgItemList.Rows.Count; i++)
                    {
                        dgItemList.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    dgItemList.Rows[dgItemList.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clrColorRow;
                    dgItemList.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #endregion

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                {
                    if (dgBill.CurrentCell.Value != null && Convert.ToString(dgBill.CurrentCell.Value) != "")
                    {
                        Desc_Start();
                    }
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    if (dgBill.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                    {
                        if (dgBill.CurrentCell.Value != null && Convert.ToString(dgBill.CurrentCell.Value) != "")
                        {
                            if (dgBill.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                            {
                                if (dgBill.CurrentRow.Cells[ColIndex.Quantity].FormattedValue.ToString() != "" && dgBill.CurrentRow.Cells[ColIndex.Quantity].FormattedValue.ToString().Trim() != "0")
                                {
                                    dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "";
                                    dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "";
                                    Qty_MoveNext();
                                }
                                else
                                {
                                    dgBill.CurrentCell.ErrorText = "Enter Valid Qty";
                                    dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.Quantity];
                                }
                            }
                            else
                            {
                                dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "Select Items";
                                dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.ItemName];
                                dgBill.Focus();
                            }
                        }
                        else
                        {
                            dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "Enter Valid Qty";
                            dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.Quantity];
                        }
                    }
                    else
                    {
                        dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "Select Items";
                        dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.ItemName];
                        dgBill.Focus();
                    }

                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                {
                    if (dgBill.CurrentCell.Value != null && Convert.ToString(dgBill.CurrentCell.Value) != "")
                    {
                        if (dgBill.CurrentRow.Cells[ColIndex.ItemName].FormattedValue.ToString() != "")
                        {
                            if (dgBill.CurrentRow.Cells[ColIndex.Quantity].Value != null && Convert.ToString(dgBill.CurrentRow.Cells[ColIndex.Quantity].FormattedValue) != "" && Convert.ToString(dgBill.CurrentRow.Cells[ColIndex.Quantity].FormattedValue).Trim() != "0")
                            {
                                if (Convert.ToDouble(dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].EditedFormattedValue) > 100)
                                {
                                    OMMessageBox.Show("Please enter Valid Discount percentage .", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                    dgBill.CurrentRow.Cells[ColIndex.DiscPercentage].Value = "100";
                                    dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "";
                                    dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "";
                                    Rate_MoveNext();
                                }
                                else
                                {
                                    dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "";
                                    dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "";
                                    Rate_MoveNext();
                                }
                            }
                            else
                            {
                                dgBill.CurrentRow.Cells[ColIndex.Quantity].ErrorText = "Enter Valid Qty";
                                dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.Quantity];
                            }
                        }
                        else
                        {
                            dgBill.CurrentRow.Cells[ColIndex.ItemName].ErrorText = "Select Items";
                            dgBill.CurrentCell = dgBill.CurrentRow.Cells[ColIndex.ItemName];
                            dgBill.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void Calculator()
        {
            try
            {
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.Quantity].FormattedValue.ToString() != "" || dgBill.Rows[i].Cells[ColIndex.MRP].FormattedValue.ToString() != "")
                    {
                        dgBill.Rows[i].Cells[ColIndex.Amount].Value = (Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Quantity].Value) * Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.MRP].Value));
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        private void txtPercentage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (chkDiscValue.Checked == true)
                    {
                        if (txtPercentage.Text.Trim() == "")
                        {
                            txtPercentage.Text = "0.00";
                        }
                        if (txtPercentage.Text != "")
                        {
                            e.SuppressKeyPress = true;
                            txtRs.Text = (Convert.ToDouble(txtAmount.Text) * (Convert.ToDouble(txtPercentage.Text) / 100)).ToString("0.00");
                            txtRs.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtRs_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (txtRs.Text.Trim() == "")
                    {
                        txtRs.Text = "0.00";
                    }
                    txtPercentage.Text = (Convert.ToDouble((100 * Convert.ToDouble(txtRs.Text)) / Convert.ToDouble(txtAmount.Text))).ToString("0.00");
                    chkProductDisc.Focus();
                    chkProductDisc_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void txtRs_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtRs, "");
                if (chkDiscValue.Checked == true)
                {
                    if (txtRs.Text.Trim() == "")
                    {
                        txtRs.Text = "0.00";
                    }
                    if (Convert.ToDouble(txtRs.Text) > Convert.ToDouble(txtAmount.Text))
                    {
                        OMMessageBox.Show("Please enter value less than selected Bill Amount.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtRs.Text = "0";
                        txtPercentage.Text = (Convert.ToDouble((100 * Convert.ToDouble(txtRs.Text)) / Convert.ToDouble(txtAmount.Text))).ToString("0.00");
                    }
                    else
                        txtPercentage.Text = (Convert.ToDouble((100 * Convert.ToDouble(txtRs.Text)) / Convert.ToDouble(txtAmount.Text))).ToString("0.00");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #region dgBillAmount

        private void btnPrev1_Click(object sender, EventArgs e)
        {
            if (dtHeader.Rows.Count > 0)
                NavigationDisplayFrom(4);
        }

        private void btnNext1_Click(object sender, EventArgs e)
        {
            if (dtHeader.Rows.Count > 0)
                NavigationDisplayFrom(3);

        }

        public bool ValidationItems()
        {
            bool flag = false;

            for (int i = 0; i < dtHeader.Rows.Count; i++)
            {
                long type = Convert.ToInt64(dtHeader.Rows[i].ItemArray[ColdtHeader.TypeNo].ToString());
                DataRow[] dr = dtSchemeFrom.Select("FkTypeNo='" + type + "'");
                bool tempFlag = true;
                if (dr.Length > 0)
                {
                    for (int mainrow = 0; mainrow < ((SchemeTypeNo == 3) ? dgBillFrom.Rows.Count : dgBillFrom.Rows.Count - 1); mainrow++)
                    {
                        if (tempFlag == true)
                        {
                            for (int row = 0; row < dr.Length; row++)
                            {
                                if (dgBillFrom.Rows[mainrow].Cells[ColIndex.ItemNo].FormattedValue.ToString() != "")
                                {
                                    if (Convert.ToInt64(dgBillFrom.Rows[mainrow].Cells[ColIndex.ItemNo].FormattedValue) == Convert.ToInt64(dr[row].ItemArray[ColIndex.ItemNo].ToString()) &&
                                        Convert.ToInt64(dgBillFrom.Rows[mainrow].Cells[ColIndex.PkRateSettingNo].FormattedValue) == Convert.ToInt64(dr[row].ItemArray[ColIndex.PkRateSettingNo].ToString())
                                        //&& Convert.ToInt64(dgBillFrom.Rows[mainrow].Cells[ColIndex.Quantity].FormattedValue) == Convert.ToInt64(dr[row].ItemArray[ColIndex.Quantity].ToString())
                                        )
                                    { tempFlag = true; break; }
                                    else
                                        tempFlag = false;
                                }
                                else tempFlag = false;
                            }
                        }
                        if (tempFlag == false) break;
                    }
                    if (tempFlag == true)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
        private void btnBASave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidationItems() == true)
                {
                    OMMessageBox.Show("Duplicate combination of StockItems...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    return;
                }
                DisplayList(false);

                for (int i = 0; i < dgBillFrom.Rows.Count; i++)
                {
                    if ((dgBillFrom.Rows[i].Cells[ColIndex.ItemNo].FormattedValue.ToString() != "") &&
                        (dgBillFrom.Rows[i].Cells[ColIndex.ItemName].ErrorText != "" || dgBillFrom.Rows[i].Cells[ColIndex.Quantity].ErrorText != "" ||
                        dgBillFrom.Rows[i].Cells[ColIndex.ItemName].FormattedValue.ToString() == "" || dgBillFrom.Rows[i].Cells[ColIndex.Quantity].FormattedValue.ToString() == ""))
                    {
                        dgBillFrom.Focus();
                        return;
                    }
                }



                if (dgBillFrom.Rows.Count > 0)
                {

                    CalculatorFrom();
                    if (TypeNo == 0)
                    {
                        if (dtHeader.Rows.Count != 0)
                            TypeNo = Convert.ToInt64(dtHeader.Compute("Max(TypeNo)", string.Empty)) + 1;
                        else
                            TypeNo = 1;
                    }
                    DataRow[] drHeader = dtHeader.Select("TypeNo=" + TypeNo);
                    if (drHeader.Length != 0)
                    {
                        foreach (DataRow row in drHeader)
                        {
                            string SchemeDetailsno = row[ColdtHeader.SchemeDetailsNo].ToString();
                            string Prec = row[ColdtHeader.DiscPercentage].ToString();
                            string discamt = row[ColdtHeader.DiscAmount].ToString();

                            dtHeader.Rows.Remove(row);
                            DataRow dr = dtHeader.NewRow();
                            dr[ColdtHeader.TypeNo] = TypeNo;
                            dr[ColdtHeader.SchemeNo] = 0;
                            dr[ColdtHeader.BillAmount] = txtAmount.Text;
                            dr[ColdtHeader.DiscPercentage] = Prec;
                            dr[ColdtHeader.DiscAmount] = discamt;
                            dr[ColdtHeader.SchemeDetailsNo] = SchemeDetailsno;
                            dr[ColdtHeader.SchemeDetailsFromNo] = 0;
                            dtHeader.Rows.Add(dr);

                            DataRow[] drSchemeFrom = dtSchemeFrom.Select("FKTypeNo=" + TypeNo);
                            foreach (DataRow rowFrom in drSchemeFrom)
                            {
                                dtSchemeFrom.Rows.Remove(rowFrom);
                            }
                        }
                    }
                    else
                    {
                        DataRow dr = dtHeader.NewRow();
                        dr[ColdtHeader.TypeNo] = TypeNo;
                        dr[ColdtHeader.SchemeNo] = 0;
                        dr[ColdtHeader.BillAmount] = txtAmount.Text;
                        dr[ColdtHeader.DiscPercentage] = 0;
                        dr[ColdtHeader.DiscAmount] = 0;
                        dr[ColdtHeader.SchemeDetailsNo] = 0;
                        dr[ColdtHeader.SchemeDetailsFromNo] = 0;
                        dtHeader.Rows.Add(dr);
                    }
                    int dgFromCount = 0;
                    if (SchemeTypeNo == 3)
                        dgFromCount = dgBillFrom.Rows.Count;
                    else dgFromCount = dgBillFrom.Rows.Count - 1;


                    for (int i = 0; i < dgFromCount; i++)
                    {
                        if (dgBillFrom.Rows[i].Cells[ColIndex.SchemeDtlsNo].FormattedValue.ToString() != "")
                        {
                            DataRow drSchemeFrom = dtSchemeFrom.NewRow();
                            drSchemeFrom[ColIndex.SrNo] = TypeNo;
                            drSchemeFrom[ColIndex.SchemeDtlsNo] = dgBillFrom.Rows[i].Cells[ColIndex.SchemeDtlsNo].Value;
                            drSchemeFrom[ColIndex.ItemNo] = dgBillFrom.Rows[i].Cells[ColIndex.ItemNo].Value;
                            drSchemeFrom[ColIndex.Quantity] = dgBillFrom.Rows[i].Cells[ColIndex.Quantity].Value;
                            drSchemeFrom[ColIndex.Rate] = dgBillFrom.Rows[i].Cells[ColIndex.Rate].Value;
                            drSchemeFrom[ColIndex.UOMNo] = dgBillFrom.Rows[i].Cells[ColIndex.UOMNo].Value;
                            drSchemeFrom[ColIndex.PkRateSettingNo] = dgBillFrom.Rows[i].Cells[ColIndex.PkRateSettingNo].Value;
                            drSchemeFrom[ColIndex.SchemeFromNo] = dgBillFrom.Rows[i].Cells[ColIndex.SchemeFromNo].Value;
                            drSchemeFrom[ColIndex.Barcode] = dgBillFrom.Rows[i].Cells[ColIndex.Barcode].Value;
                            drSchemeFrom[ColIndex.PkBarCodeNo] = dgBillFrom.Rows[i].Cells[ColIndex.PkBarCodeNo].Value;
                            drSchemeFrom[ColIndex.Amount] = dgBillFrom.Rows[i].Cells[ColIndex.Amount].Value;
                            drSchemeFrom[ColIndex.MRP] = dgBillFrom.Rows[i].Cells[ColIndex.MRP].Value;
                            drSchemeFrom[ColIndex.SchemeNo] = dgBillFrom.Rows[i].Cells[ColIndex.SchemeNo].Value;
                            drSchemeFrom[ColIndex.ItemName] = dgBillFrom.Rows[i].Cells[ColIndex.ItemName].Value;
                            drSchemeFrom[ColIndex.UOM] = dgBillFrom.Rows[i].Cells[ColIndex.UOM].Value;
                            drSchemeFrom[ColIndex.DiscPercentage] = 0;
                            drSchemeFrom[ColIndex.BillAmount] = txtAmount.Text;
                            drSchemeFrom[ColIndex.SchemeDtlsNo] = dgBillFrom.Rows[i].Cells[ColIndex.SchemeDtlsNo].Value;
                            drSchemeFrom[ColIndex.SchemeFromNo] = dgBillFrom.Rows[i].Cells[ColIndex.SchemeFromNo].Value;
                            dtSchemeFrom.Rows.Add(drSchemeFrom);
                        }
                    }
                    if (dtSchemeFrom.Rows.Count <= 0)
                    {
                        OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        dgBillFrom.Focus();
                        return;
                    }
                    FindData(TypeNo);
                    //btnNewChild.Visible = true;
                    //btnUpdateChild.Visible = true;
                    //btnBASave.Visible = false;
                    //btncancelchild.Visible = false;

                    pnlBgFrom.Enabled = false;
                    pnlBg.Enabled = false;
                    pnlchk.Enabled = false;
                    btnEdit.Enabled = true;
                    //btnPrev1.Enabled = true;
                    //btnNext1.Enabled = true;
                    btnEdit.Focus();
                }
                else
                {
                    OMMessageBox.Show("Enter At least one row", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                cntRowFrom = dtHeader.Rows.Count - 1;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        public void SetEnable(bool Status)
        {
            dgBillFrom.Enabled = Status;
            btnNewChild.Enabled = Status;
            //btnUpdateChild.Enabled = Status;
        }

        #endregion

        public bool Validation()
        {

            bool flag = false;
            try
            {
                if (!validate_txtSchemeName() || !validate_DtpSchemeDateFrom() || !validate_DtpSchemeDateTo()
                    || !validate_DtpTillDateFrom() || !validate_DtpTillDateTo())
                {
                    flag = false;
                }
                else if (dtHeader.Rows.Count == 0)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    dgBillFrom.Focus();
                }
                else if (btnBASave.Visible == true)
                {
                    OMMessageBox.Show("Save The Bill Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    btnBASave.Focus();
                }
                else if (btnBgSave.Visible == true)
                {
                    OMMessageBox.Show("Please Save Data ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    btnBgSave.Focus();
                }
                else if (ObjFunction.GetComboValue(cmbDiscType) <= 0)
                {
                    OMMessageBox.Show("Select Discount Type", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    cmbDiscType.Focus();
                }
                else if (SchemeTypeNo == 3 || SchemeTypeNo == 4)
                {
                    flag = true;
                    //if (ID != 0)
                    //{
                        for (int i = 0; i < dgBillFrom.Rows.Count; i++)
                        {
                            long k = Convert.ToInt64(dgBillFrom.Rows[i].Cells[8].Value);

                            if (CheckDuplicate(k.ToString()) == false)
                            {
                                flag = false;
                                OMMessageBox.Show("Duplicate Item in Specified Scheme Period", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                break;
                            }
                        }
                    //}
                }
                else
                {
                    flag = true;
                }

                if (flag == true)
                {
                    if (ObjFunction.GetComboValue(cmbDiscType) == 1 || ObjFunction.GetComboValue(cmbDiscType) == 2)
                    {
                        if (Convert.ToDouble(dtHeader.Rows[0].ItemArray[ColdtHeader.DiscAmount].ToString()) <= 0)
                        {
                            OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            dgBillFrom.Focus();
                            flag = false;
                        }
                    }
                    else if (ObjFunction.GetComboValue(cmbDiscType) == 3)
                    {
                        if (dtSchemeTo.Rows.Count <= 0)
                        {
                            OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            dgBillFrom.Focus();
                            flag = false;
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

        #region DtHeaderMethod

        public void InitTable()
        {
            try
            {
                dtHeader = new DataTable();


                DataColumn TypeNo = new DataColumn("TypeNo");
                TypeNo.DataType = typeof(Int64);
                dtHeader.Columns.Add(TypeNo);
                dtHeader.Columns.Add("BillAmount");
                dtHeader.Columns.Add("SchemeNo");
                dtHeader.Columns.Add("SchemeDetailsNo");
                dtHeader.Columns.Add("SchemeDetailsFromNo");
                dtHeader.Columns.Add("DiscPercentage");
                dtHeader.Columns.Add("DiscAmount");


                dtSchemeTo = new DataTable();

                DataColumn FKTypeNoTO = new DataColumn("FKTypeNo");
                FKTypeNoTO.DataType = typeof(Int64);
                dtSchemeTo.Columns.Add(FKTypeNoTO);
                dtSchemeTo.Columns.Add("ItemName");
                dtSchemeTo.Columns.Add("Quantity");
                dtSchemeTo.Columns.Add("UOM");
                dtSchemeTo.Columns.Add("Rate");
                dtSchemeTo.Columns.Add("DiscPercentage");
                dtSchemeTo.Columns.Add("Barcode");
                dtSchemeTo.Columns.Add("PkBarCodeNo");
                dtSchemeTo.Columns.Add("ItemNo");
                DataColumn SchemeToNo = new DataColumn("SchemeFromNo");
                SchemeToNo.DataType = typeof(Int64);
                dtSchemeTo.Columns.Add(SchemeToNo);
                dtSchemeTo.Columns.Add("UOMNo");
                dtSchemeTo.Columns.Add("PkRateSettingNo");
                DataColumn SchemeToDtlsNo = new DataColumn("SchemeDtlsNo");
                SchemeToDtlsNo.DataType = typeof(Int64);
                dtSchemeTo.Columns.Add(SchemeToDtlsNo);
                dtSchemeTo.Columns.Add("Amount");
                dtSchemeTo.Columns.Add("BillAmount");
                dtSchemeTo.Columns.Add("MRP");
                dtSchemeTo.Columns.Add("SchemeNo");


                dtSchemeFrom = new DataTable();
                DataColumn FKTypeNoFrom = new DataColumn("FKTypeNo");
                FKTypeNoFrom.DataType = typeof(Int64);
                dtSchemeFrom.Columns.Add(FKTypeNoFrom);
                dtSchemeFrom.Columns.Add("ItemName");
                dtSchemeFrom.Columns.Add("Quantity");
                dtSchemeFrom.Columns.Add("UOM");
                dtSchemeFrom.Columns.Add("Rate");
                dtSchemeFrom.Columns.Add("DiscPercentage");
                dtSchemeFrom.Columns.Add("Barcode");
                dtSchemeFrom.Columns.Add("PkBarCodeNo");
                dtSchemeFrom.Columns.Add("ItemNo");
                DataColumn SchemeFromNo = new DataColumn("SchemeFromNo");
                SchemeFromNo.DataType = typeof(Int64);
                dtSchemeFrom.Columns.Add(SchemeFromNo);
                dtSchemeFrom.Columns.Add("UOMNo");
                dtSchemeFrom.Columns.Add("PkRateSettingNo");
                DataColumn SchemeFromDtlsNo = new DataColumn("SchemeDtlsNo");
                SchemeFromDtlsNo.DataType = typeof(Int64);
                dtSchemeFrom.Columns.Add(SchemeFromDtlsNo);
                dtSchemeFrom.Columns.Add("Amount");
                dtSchemeFrom.Columns.Add("BillAmount");
                dtSchemeFrom.Columns.Add("MRP");
                dtSchemeFrom.Columns.Add("SchemeNo");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FindData(long TypeNo)
        {
            try
            {
                if (dtHeader.Rows.Count != 0)
                {
                    DataRow[] drHeader = dtHeader.Select("TypeNo=" + TypeNo);
                    foreach (DataRow row in drHeader)
                    {
                        lblProductmsg.Text = "Redemption Details For " + row[ColdtHeader.BillAmount].ToString() + " Rs.";
                        //lblStatus.Text = (dtHeader.Rows.Count).ToString() + "/" + TypeNo.ToString();
                        txtAmount.Text = row[ColdtHeader.BillAmount].ToString();
                        txtPercentage.Text = row[ColdtHeader.DiscPercentage].ToString();
                        txtRs.Text = row[ColdtHeader.DiscAmount].ToString();
                        double Qty = 0;
                        DataRow[] drSchemeFrom = dtSchemeFrom.Select("FKTypeNo=" + row[0]);
                        while (dgBillFrom.Rows.Count > 0)
                            dgBillFrom.Rows.RemoveAt(0);
                        foreach (DataRow rowFrom in drSchemeFrom)
                        {

                            dgBillFrom.Rows.Add();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.Barcode].Value = rowFrom[ColIndex.Barcode].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.DiscPercentage].Value = rowFrom[ColIndex.DiscPercentage].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.ItemName].Value = rowFrom[ColIndex.ItemName].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.ItemNo].Value = rowFrom[ColIndex.ItemNo].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.PkBarCodeNo].Value = rowFrom[ColIndex.PkBarCodeNo].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.PkRateSettingNo].Value = rowFrom[ColIndex.PkRateSettingNo].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.Quantity].Value = rowFrom[ColIndex.Quantity].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.Rate].Value = rowFrom[ColIndex.Rate].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.SchemeDtlsNo].Value = rowFrom[ColIndex.SchemeDtlsNo].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.SchemeFromNo].Value = rowFrom[ColIndex.SchemeFromNo].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.UOM].Value = rowFrom[ColIndex.UOM].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.UOMNo].Value = rowFrom[ColIndex.UOMNo].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.Amount].Value = rowFrom[ColIndex.Amount].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.BillAmount].Value = rowFrom[ColIndex.BillAmount].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.MRP].Value = rowFrom[ColIndex.MRP].ToString();
                            dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.SchemeNo].Value = rowFrom[ColIndex.SchemeNo].ToString();

                            Qty = Qty + Convert.ToDouble(rowFrom[ColIndex.Quantity].ToString());
                        }
                        txtQty.Text = Qty.ToString("0.00");

                        if (dgBillFrom.Rows.Count > 0)
                        {
                            if (SchemeTypeNo != 3)
                                dgBillFrom.Rows.Add();
                        }

                        DataRow[] drSchemeTo = dtSchemeTo.Select("FKTypeNo=" + row[0]);
                        while (dgBill.Rows.Count > 0)
                            dgBill.Rows.RemoveAt(0);
                        foreach (DataRow rowTo in drSchemeTo)
                        {

                            dgBill.Rows.Add();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.Barcode].Value = rowTo[ColIndex.Barcode].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.DiscPercentage].Value = rowTo[ColIndex.DiscPercentage].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName].Value = rowTo[ColIndex.ItemName].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemNo].Value = rowTo[ColIndex.ItemNo].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.PkBarCodeNo].Value = rowTo[ColIndex.PkBarCodeNo].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.PkRateSettingNo].Value = rowTo[ColIndex.PkRateSettingNo].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.Quantity].Value = rowTo[ColIndex.Quantity].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.Rate].Value = rowTo[ColIndex.Rate].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.SchemeDtlsNo].Value = rowTo[ColIndex.SchemeDtlsNo].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.SchemeFromNo].Value = rowTo[ColIndex.SchemeFromNo].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.UOM].Value = rowTo[ColIndex.UOM].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.UOMNo].Value = rowTo[ColIndex.UOMNo].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.Amount].Value = rowTo[ColIndex.Amount].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.BillAmount].Value = rowTo[ColIndex.BillAmount].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.MRP].Value = rowTo[ColIndex.MRP].ToString();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.SchemeNo].Value = rowTo[ColIndex.SchemeNo].ToString();
                        }
                        if (dgBill.Rows.Count > 0)
                            dgBill.Rows.Add();

                    }

                    if (dgBill.Rows.Count == 0)
                        chkProductDisc.Checked = false;
                    else
                        chkProductDisc.Checked = true;

                    if (txtPercentage.Text.Trim() != "")
                    {
                        if (Convert.ToDouble(txtPercentage.Text.Trim()) == 0 || Convert.ToDouble(txtRs.Text.Trim()) == 0)
                            chkDiscValue.Checked = false;
                        else
                            chkDiscValue.Checked = true;
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void AddNewData()
        {
            try
            {
                if (txtRs.Text != "" || txtRs.Text != "0")
                {
                    DataRow[] drHeader = dtHeader.Select("TypeNo=" + TypeNo);
                    foreach (DataRow row in drHeader)
                    {
                        long SchemeDetailsNo = Convert.ToInt64(row[ColdtHeader.SchemeDetailsNo].ToString());
                        dtHeader.Rows.Remove(row);
                        DataRow dr = dtHeader.NewRow();
                        dr[ColdtHeader.TypeNo] = TypeNo;
                        dr[ColdtHeader.SchemeNo] = 0;
                        dr[ColdtHeader.BillAmount] = txtAmount.Text;
                        dr[ColdtHeader.DiscPercentage] = (txtPercentage.Text == "") ? "0" : txtPercentage.Text;
                        dr[ColdtHeader.DiscAmount] = (txtRs.Text == "") ? "0" : txtRs.Text;
                        dr[ColdtHeader.SchemeDetailsNo] = SchemeDetailsNo;
                        dr[ColdtHeader.SchemeDetailsFromNo] = 0;
                        dtHeader.Rows.Add(dr);
                    }
                }

                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    DataRow drSchemeTo = dtSchemeTo.NewRow();
                    drSchemeTo[ColIndex.SrNo] = TypeNo;
                    drSchemeTo[ColIndex.SchemeDtlsNo] = dgBill.Rows[i].Cells[ColIndex.SchemeDtlsNo].Value;
                    drSchemeTo[ColIndex.ItemNo] = dgBill.Rows[i].Cells[ColIndex.ItemNo].Value;
                    drSchemeTo[ColIndex.Quantity] = dgBill.Rows[i].Cells[ColIndex.Quantity].Value;
                    drSchemeTo[ColIndex.Rate] = dgBill.Rows[i].Cells[ColIndex.Rate].Value;
                    drSchemeTo[ColIndex.DiscPercentage] = dgBill.Rows[i].Cells[ColIndex.DiscPercentage].Value;
                    drSchemeTo[ColIndex.UOM] = dgBill.Rows[i].Cells[ColIndex.UOM].Value;
                    drSchemeTo[ColIndex.UOMNo] = dgBill.Rows[i].Cells[ColIndex.UOMNo].Value;
                    drSchemeTo[ColIndex.PkRateSettingNo] = dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value;
                    drSchemeTo[ColIndex.SchemeFromNo] = dgBill.Rows[i].Cells[ColIndex.SchemeFromNo].Value;
                    drSchemeTo[ColIndex.Barcode] = dgBill.Rows[i].Cells[ColIndex.Barcode].Value;
                    drSchemeTo[ColIndex.PkBarCodeNo] = dgBill.Rows[i].Cells[ColIndex.PkBarCodeNo].Value;
                    drSchemeTo[ColIndex.Amount] = dgBill.Rows[i].Cells[ColIndex.Amount].Value;
                    drSchemeTo[ColIndex.ItemName] = dgBill.Rows[i].Cells[ColIndex.ItemName].Value;
                    drSchemeTo[ColIndex.BillAmount] = dgBill.Rows[i].Cells[ColIndex.BillAmount].Value;
                    drSchemeTo[ColIndex.Rate] = dgBill.Rows[i].Cells[ColIndex.Rate].Value;
                    drSchemeTo[ColIndex.MRP] = dgBill.Rows[i].Cells[ColIndex.MRP].Value;
                    drSchemeTo[ColIndex.SchemeNo] = dgBill.Rows[i].Cells[ColIndex.SchemeNo].Value;
                    dtSchemeTo.Rows.Add(drSchemeTo);

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {
                long No = 0;
                if (type == 5)
                {
                    if (dtSearch.Rows.Count > 0)
                    {
                        No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    }
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


                FillControls();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
            try
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
                //else if (e.KeyCode == Keys.F3)
                //{
                //    if (btnNewChild.Visible) btnNewChild_Click(sender, e);
                //}
                //else if (e.KeyCode == Keys.F4)
                //{
                //    if (btnBASave.Visible) btnBASave_Click(sender, e);
                //}
                //else if (e.KeyCode == Keys.F5)
                //{
                //    if (btncancelchild.Visible) btncancelchild_Click(sender, e);
                //}
                //else if (e.KeyCode == Keys.F6)
                //{
                //    if (btnUpdateChild.Visible) btnUpdateChild_Click(sender, e);
                //}
                else if (e.KeyCode == Keys.F7)
                {
                    if (btnBgSave.Visible) btnBgSave_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (btnEdit.Visible && btnEdit.Enabled) btnEdit_Click(sender, e);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #endregion

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);

                //btncancelchild.Visible = false;
                //btnUpdateChild.Visible = true;
                //btnNewChild.Enabled = true;
                //btnUpdateChild.Enabled = true;
                btncopy.Visible = false;
                DtpDate.Enabled = false;
                dgBill.Enabled = true;
                dgBillFrom.Enabled = true;
                cmbSponsor.Enabled = false;
                txtCampaignID.Enabled = false;
                btnEdit.Enabled = true;
                txtSchemeName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                InitControls();
                DisplayList(false);
                ObjFunction.LockButtons(true, this.Controls);
                NavigationDisplay(5);
                ObjFunction.LockControls(false, this.Controls);

                //btnNewChild.Visible = true;
                //btnBASave.Visible = false;
                //btnUpdateChild.Visible = true;
                //btncancelchild.Visible = false;
                btncopy.Visible = true;
                btnNew.Focus();

                if (dtSearch.Rows.Count == 0)
                {
                    //btnBASave.Enabled = false;
                    btnBgSave.Enabled = false;
                    btnEdit.Enabled = false;
                    //btncancelchild.Enabled = false;
                    //btnUpdateChild.Visible = false;
                    //btnBASave.Visible = true;
                    btnBgSave.Visible = false;
                    btnEdit.Visible = true;
                    btnUpdate.Enabled = false;
                    btnSearch.Enabled = false;
                    btnDelete.Enabled = false;
                    btnNext.Enabled = false;
                    btnPrev.Enabled = false;
                    btnLast.Enabled = false;
                    btnFirst.Enabled = false;
                    btncopy.Visible = false;
                }
                else
                    btnUpdate.Enabled = true;
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
                if (Validation() == true)
                {
                    dbMScheme = new DBMScheme();
                    DeleteValues();
                    mScheme = new MScheme();
                    mScheme.SchemeNo = ID;
                    mScheme.SchemeName = txtSchemeName.Text.Trim();
                    mScheme.SchemeTypeNo = SchemeTypeNo;
                    mScheme.SchemeUserNo = txtSchemeUserNo.Text;
                    mScheme.SchemeDate = Convert.ToDateTime(DtpDate.Text);
                    mScheme.SchemePeriodFrom = Convert.ToDateTime(DtpSchemeDateFrom.Text);
                    mScheme.SchemePeriodTo = Convert.ToDateTime(DtpSchemeDateTo.Text);

                    mScheme.DiscType = ObjFunction.GetComboValue(cmbDiscType);

                    if (SchemeTypeNo == 4 || SchemeTypeNo == 3)
                    {
                        mScheme.SchemeRedPeriodFrom = Convert.ToDateTime("1-1-1900");
                        mScheme.SchemeRedPeriodTo = Convert.ToDateTime("1-1-1900");
                    }
                    else
                    {
                        mScheme.SchemeRedPeriodFrom = Convert.ToDateTime(DtpTillDateFrom.Text);
                        mScheme.SchemeRedPeriodTo = Convert.ToDateTime(DtpTillDateTo.Text);
                    }
                    mScheme.SchemeWorth = "0";
                    mScheme.CompanyNo = DBGetVal.FirmNo;
                    if (ID == 0) mScheme.IsActive = 0;
                    else
                    {


                        if (lblSchemeStatus.Text == "Draft") mScheme.IsActive = 0;
                        else if (lblSchemeStatus.Text == "Active") mScheme.IsActive = 1;
                        else if (lblSchemeStatus.Text == "Closed") mScheme.IsActive = 2;
                    }
                    mScheme.UserID = DBGetVal.UserID;
                    mScheme.UserDate = DBGetVal.ServerTime;
                    mScheme.SponcorNo = ObjFunction.GetComboValue(cmbSponsor);
                    mScheme.CampaignID = txtCampaignID.Text;
                    mScheme.IsIWScheme = (ID < 5001) ? 0 : 1;// (txtCampaignID.Text.Trim() == "0") ? 0 : 1;
                    mScheme.CustomerType = 3;
                    dbMScheme.AddMScheme(mScheme);

                    dtHeader.DefaultView.Sort = "TypeNo asc";
                    dtHeader = dtHeader.DefaultView.ToTable(true);

                    for (int h = 0; h < dtHeader.Rows.Count; h++)
                    {
                        mSchemeDetails = new MSchemeDetails();
                        long HCount = Convert.ToInt64(dtHeader.Rows[h].ItemArray[ColdtHeader.TypeNo].ToString());
                        mSchemeDetails.PkSrNo = Convert.ToInt64(dtHeader.Rows[h].ItemArray[ColdtHeader.SchemeDetailsNo].ToString());
                        mSchemeDetails.DiscPercentage = Convert.ToDouble(dtHeader.Rows[h].ItemArray[ColdtHeader.DiscPercentage].ToString());
                        mSchemeDetails.DiscAmount = Convert.ToDouble(dtHeader.Rows[h].ItemArray[ColdtHeader.DiscAmount].ToString());
                        mSchemeDetails.CompanyNo = DBGetVal.FirmNo;
                        mSchemeDetails.IsActive = true;
                        mSchemeDetails.UserID = DBGetVal.UserID;
                        mSchemeDetails.UserDate = DBGetVal.ServerTime;
                        dbMScheme.AddMSchemeDetails(mSchemeDetails);



                        DataRow[] drFrom = dtSchemeFrom.Select("FKTypeNo=" + HCount);
                        foreach (DataRow rowFrom in drFrom)
                        {
                            mSchemeFromDetails = new MSchemeFromDetails();
                            mSchemeFromDetails.PkSrNo = Convert.ToInt64(rowFrom[ColIndex.SchemeFromNo].ToString());
                            mSchemeFromDetails.BillAmount = Convert.ToDouble(rowFrom[ColIndex.BillAmount].ToString());
                            mSchemeFromDetails.ItemNo = Convert.ToInt64(rowFrom[ColIndex.ItemNo].ToString());
                            mSchemeFromDetails.Quantity = Convert.ToDouble(rowFrom[ColIndex.Quantity].ToString());
                            mSchemeFromDetails.Amount = Convert.ToDouble(rowFrom[ColIndex.Amount].ToString());
                            mSchemeFromDetails.DiscPercentage = Convert.ToDouble(rowFrom[ColIndex.DiscPercentage].ToString());
                            mSchemeFromDetails.UomNo = Convert.ToInt64(rowFrom[ColIndex.UOMNo].ToString());
                            mSchemeFromDetails.FkRateSettingNo = Convert.ToInt64(rowFrom[ColIndex.PkRateSettingNo].ToString());
                            mSchemeFromDetails.Rate = Convert.ToDouble(rowFrom[ColIndex.Rate].ToString());
                            mSchemeFromDetails.MRP = Convert.ToDouble(rowFrom[ColIndex.MRP].ToString());
                            mSchemeFromDetails.CompanyNo = DBGetVal.FirmNo;
                            mSchemeFromDetails.UserDate = DBGetVal.ServerTime;
                            mSchemeFromDetails.UserID = DBGetVal.UserID;
                            dbMScheme.AddMSchemeFromDetails(mSchemeFromDetails);
                        }

                        DataRow[] drTo = dtSchemeTo.Select("FKTypeNo=" + HCount);
                        foreach (DataRow rowTo in drTo)
                        {
                            mSchemeToDetails = new MSchemeToDetails();
                            mSchemeToDetails.PkSrNo = Convert.ToInt64(rowTo[ColIndex.SchemeFromNo].ToString());
                            mSchemeToDetails.BillAmount = Convert.ToDouble(rowTo[ColIndex.BillAmount].ToString());
                            mSchemeToDetails.ItemNo = Convert.ToInt64(rowTo[ColIndex.ItemNo].ToString());
                            mSchemeToDetails.Quantity = Convert.ToDouble(rowTo[ColIndex.Quantity].ToString());
                            mSchemeToDetails.Amount = Convert.ToDouble(rowTo[ColIndex.Amount].ToString());
                            mSchemeToDetails.DiscPercentage = Convert.ToDouble(rowTo[ColIndex.DiscPercentage].ToString());
                            mSchemeToDetails.UomNo = Convert.ToInt64(rowTo[ColIndex.UOMNo].ToString());
                            mSchemeToDetails.FkRateSettingNo = Convert.ToInt64(rowTo[ColIndex.PkRateSettingNo].ToString());
                            mSchemeToDetails.Rate = Convert.ToDouble(rowTo[ColIndex.Rate].ToString());
                            mSchemeToDetails.MRP = Convert.ToDouble(rowTo[ColIndex.MRP].ToString());
                            mSchemeToDetails.CompanyNo = DBGetVal.FirmNo;
                            mSchemeToDetails.UserDate = DBGetVal.ServerTime;
                            mSchemeToDetails.UserID = DBGetVal.UserID;
                            dbMScheme.AddMSchemeToDetails(mSchemeToDetails);
                        }

                    }
                    ////For Scheme Assign
                    //for (int i = 0; i < dtSchemeAssign.Rows.Count; i++)
                    //{
                    //    mSchemeAssign = new MSchemeAssign();
                    //    mSchemeAssign.PkSrNo = Convert.ToInt64(dtSchemeAssign.Rows[i].ItemArray[4].ToString());
                    //    mSchemeAssign.AssignDate = DBGetVal.ServerTime;
                    //    mSchemeAssign.LedgerNo = Convert.ToInt64(dtSchemeAssign.Rows[i].ItemArray[3].ToString());
                    //    mSchemeAssign.IsActive = Convert.ToBoolean(dtSchemeAssign.Rows[i].ItemArray[2].ToString());
                    //    mSchemeAssign.UserID = DBGetVal.UserID;
                    //    mSchemeAssign.UserDate = DBGetVal.ServerTime;
                    //    dbMScheme.AddMSchemeAssign(mSchemeAssign);
                    //}

                    long tempID = dbMScheme.ExecuteNonQueryStatements();
                    if (tempID != 0)
                    {
                        if (ObjQry.ReturnLong("Select Count(*) From MSchemeAssign Where SchemeNo=" + tempID + "", CommonFunctions.ConStr) == 0)
                        {
                            OMMessageBox.Show("Scheme Saved Successfully..." + Environment.NewLine + "Please Customer Assign to this Scheme.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                        else
                            OMMessageBox.Show("Scheme Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        if (ID == 0)
                        {
                            DataRow drSearch = dtSearch.NewRow();
                            drSearch[0] = tempID;
                            dtSearch.Rows.Add(drSearch);
                        }
                        ID = tempID;
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        FillControls();
                        DisplayList(false);
                        btnUpdate.Enabled = true;
                    }
                    else
                    {
                        OMMessageBox.Show("Scheme Not Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                cmbSponsor.SelectedValue = -1;
                cmbSponsor.Enabled = false;
                txtCampaignID.Text = "0";
                txtCampaignID.Enabled = false;
                //btnNewChild.Enabled = true;
                //btnUpdateChild.Visible = false;
                //btncancelchild.Visible = true;
                //btncancelchild.Enabled = true;
                btncopy.Visible = false;
                dgBill.Enabled = true;
                dgBillFrom.Enabled = true;
                //lblStatus.Text = "";
                ID = 0;
                InitControls();
                cmbDiscType.SelectedValue = 1;
                txtSchemeUserNo.Text = ObjQry.ReturnString("Select SchemeUserNo From GetMaxSchemeUserNo(" + SchemeTypeNo + ",1) ", CommonFunctions.ConStr);
                txtSchemeName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Settings.Loyalty.Loyalty.RequestSchemeNo = 0;
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (btnNewChild.Visible == true)
                //{
                btnBgSave.Enabled = true;
                btnBgSave.Visible = true;
                btnEdit.Visible = false;
                pnlchk.Enabled = true;
                pnlBg.Enabled = false;
                pnlPerc.Enabled = false;

                ApplyDiscTypeOnCHK();

                if (chkDiscValue.Checked == true)
                {
                    chkDiscValue_CheckedChanged(sender, new EventArgs());
                    chkDiscValue_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }
                else if (chkProductDisc.Checked == true)
                {
                    chkProductDisc_CheckedChanged(sender, new EventArgs());
                    chkProductDisc_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    if (dgBill.Rows.Count > 0)
                    {
                        dgBill.CurrentCell = dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.ItemName];
                        dgBill.Focus();
                    }
                }
                else
                {
                    chkDiscValue.Focus();
                    chkProductDisc.Focus();
                    chkDiscValue.Focus();
                }
                //}
                //else
                //{
                //    dgBillFrom.Focus();
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkDiscValue_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDiscValue.Checked == true)
                {
                    if (btnBgSave.Visible == true)
                        pnlPerc.Enabled = true;
                    chkDiscValue.Focus();
                }
                else
                {
                    if (btnBgSave.Visible == true)
                    {
                        txtPercentage.Text = "0.00";
                        txtRs.Text = "0.00";
                    }
                    pnlPerc.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkProductDisc_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkProductDisc.Checked == true)
                {
                    if (btnBgSave.Visible == true)
                        pnlBg.Enabled = true;
                    dgBill.Enabled = true;
                }
                else
                {
                    if (dgBill.Rows.Count > 0 && btnBgSave.Visible)
                    {
                        //if(OMMessageBox.Show("Are you sure clear the Scheme from Details.",CommonFunctions.ErrorTitle,OMMessageBoxIcon.Question,OMMessageBoxButton.YesNo)==DialogResult.Yes)
                        //{
                        while (dgBill.Rows.Count > 0)
                        {
                            dgBill.CurrentCell = dgBill[ColIndex.ItemName, 0];
                            long SchemeToNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.SchemeFromNo].Value);
                            if (SchemeToNo != 0)
                            {
                                DeleteDtls(2, SchemeToNo);
                                DataRow[] drTo = dtSchemeTo.Select("FKTypeNo=" + TypeNo + " And SchemeFromNo=" + SchemeToNo);
                                foreach (DataRow rowTo in drTo)
                                {
                                    dtSchemeTo.Rows.Remove(rowTo);
                                }
                            }
                            if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                            {
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                                dgBill.Rows.Add();
                            }
                            else
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        }
                        //}
                    }
                    pnlBg.Enabled = false;
                    dgBill.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkDiscValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (chkDiscValue.Checked == true)
                    {
                        txtPercentage.Focus();
                        btnEdit.Visible = false;
                        btnBgSave.Visible = true;
                    }
                    else chkProductDisc.Focus();

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void chkProductDisc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (chkProductDisc.Checked == true)
                    {
                        if (dgBill.Rows.Count <= 0)
                            dgBill.Rows.Add();
                        if (btnBgSave.Enabled == true)
                            pnlBg.Enabled = true;
                        dgBill.CurrentCell = dgBill[ColIndex.ItemName, dgBill.Rows.Count - 1];
                        dgBill.Focus();
                    }
                    else btnBgSave.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region TextChangedEvent

        private void txtPercentage_TextChanged(object sender, EventArgs e)
        {
            if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.DiscPercentage)
                ObjFunction.SetMasked((TextBox)sender, 2, 3, OMFunctions.MaskedType.NotNegative);
        }

        private void txtPercentage1_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 3, OMFunctions.MaskedType.NotNegative);
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                ObjFunction.SetMasked((TextBox)sender, 2, 7, OMFunctions.MaskedType.NotNegative);
        }
        private void txtQuantityFrom_TextChanged(object sender, EventArgs e)
        {
            if (dgBillFrom.CurrentCell.ColumnIndex == ColIndex.Quantity)
                ObjFunction.SetMasked((TextBox)sender, 2, 7, OMFunctions.MaskedType.NotNegative);
        }

        private void txtRs_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 5, OMFunctions.MaskedType.NotNegative);
        }

        #endregion

        private void txtSchemeName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (validate_txtSchemeName())
                    {
                        DtpSchemeDateFrom.Focus();
                    }
                    else
                    {
                        txtSchemeName.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtSchemeName_Leave(object sender, EventArgs e)
        {
            validate_txtSchemeName();
        }

        private void DtpSchemeDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DtpTillDateFrom.MinDate = Convert.ToDateTime("1-1-1900");
                //DtpTillDateTo.MinDate = Convert.ToDateTime("1-1-1900");
                //DtpSchemeDateTo.MinDate = Convert.ToDateTime("1-1-1900");

                //////DateTime dtDate = Convert.ToDateTime(DtpSchemeDateFrom.Text);
                //////DtpSchemeDateTo.Text = dtDate.AddMonths(1).AddDays(-1).ToString(Format.DDMMMYYYY);
                //DtpSchemeDateTo.MinDate = dtDate;

                //DtpTillDateFrom.Text = Convert.ToDateTime(DtpSchemeDateTo.Text).AddDays(1).ToString(Format.DDMMMYYYY);
                //DtpTillDateFrom.MinDate = Convert.ToDateTime(DtpTillDateFrom.Text);
                //DtpTillDateTo.Text = Convert.ToDateTime(DtpTillDateFrom.Text).AddDays(1).ToString(Format.DDMMMYYYY);
                //DtpTillDateTo.MinDate = Convert.ToDateTime(DtpTillDateFrom.Text).AddDays(1);
                e.SuppressKeyPress = true;
                DtpSchemeDateTo.Focus();
            }
        }

        private void DtpSchemeDateFrom_Leave(object sender, EventArgs e)
        {
            validate_DtpSchemeDateFrom();
        }

        private void DtpTillDateTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (validate_DtpTillDateTo())
                {
                    cmbDiscType.Focus();
                }
                e.SuppressKeyPress = true;
            }
        }

        private void DtpTillDateTo_Leave(object sender, EventArgs e)
        {
            validate_DtpTillDateTo();
        }

        private void DtpSchemeDateTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //DtpTillDateFrom.MinDate = Convert.ToDateTime("1-1-1900");
                    //DtpTillDateTo.MinDate = Convert.ToDateTime("1-1-1900");

                    if (SchemeTypeNo == 5)
                    {
                        DtpTillDateFrom.Text = Convert.ToDateTime(DtpSchemeDateTo.Text).AddDays(1).ToString(Format.DDMMMYYYY);
                        //DtpTillDateFrom.MinDate = Convert.ToDateTime(DtpTillDateFrom.Text);

                        DtpTillDateTo.Text = Convert.ToDateTime(DtpTillDateFrom.Text).AddDays(1).ToString(Format.DDMMMYYYY);
                        //DtpTillDateTo.MinDate = Convert.ToDateTime(DtpTillDateFrom.Text).AddDays(1);
                    }
                    else
                    {
                        DtpTillDateTo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpTillDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //DtpTillDateTo.MinDate = Convert.ToDateTime("1-1-1900");
                //DtpTillDateTo.Text = Convert.ToDateTime(DtpTillDateFrom.Text).AddDays(1).ToString(Format.DDMMMYYYY);
                //DtpTillDateTo.MinDate = Convert.ToDateTime(DtpTillDateFrom.Text).AddDays(1);
            }

        }

        private void btnNewChild_Click(object sender, EventArgs e)
        {
            try
            {
                TypeNo = 0;
                txtAmount.Text = "0.00";
                txtQty.Text = "0.00";
                txtPercentage.Text = "0.00";
                txtRs.Text = "0.00";
                btnBgSave.Visible = false;
                btnEdit.Visible = true;
                btnNewChild.Visible = false;
                //btnUpdateChild.Visible = false;
                //btncancelchild.Visible = true;
                //btncancelchild.Enabled = true;
                while (dgBillFrom.Rows.Count > 0)
                {
                    dgBillFrom.Rows.RemoveAt(0);
                }
                while (dgBill.Rows.Count > 0)
                {
                    dgBill.Rows.RemoveAt(0);
                }
                chkDiscValue.Checked = false;
                chkProductDisc.Checked = false;

                //btnBASave.Visible = true;
                //btnBASave.Enabled = true;
                pnlBgFrom.Enabled = true;
                //btnPrev1.Enabled = false;
                //btnNext1.Enabled = false;
                //lblStatus.Text = (dtHeader.Rows.Count).ToString() + "/" + (dtHeader.Rows.Count + 1).ToString();
                dgBillFrom.Rows.Add();
                dgBillFrom.CurrentCell = dgBillFrom[ColIndex.ItemName, 0];
                dgBillFrom.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdateChild_Click(object sender, EventArgs e)
        {
            try
            {
                //btnUpdateChild.Visible = false;
                //btnNewChild.Visible = false;
                //btnBASave.Visible = true;
                //btncancelchild.Visible = true;
                pnlBgFrom.Enabled = true;
                //btnPrev1.Enabled = false;
                //btnNext1.Enabled = false;
                if (dgBillFrom.RowCount > 0)
                {
                    dgBillFrom.CurrentCell = dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[ColIndex.ItemName];
                }
                dgBillFrom.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btncancelchild_Click(object sender, EventArgs e)
        {
            try
            {
                dgBillFrom.Rows.Add();
                while (dgBillFrom.Rows.Count > 0)
                    dgBillFrom.Rows.RemoveAt(0);
                cntRowFrom = dtHeader.Rows.Count - 1;
                NavigationDisplayFrom(5);
                //FindData(cntRowFrom);
                //btnUpdateChild.Visible = true;
                //btnNewChild.Visible = true;
                //btnBASave.Visible = false;
                //btncancelchild.Visible = false;
                //btnPrev1.Enabled = true;
                //btnNext1.Enabled = true;
                DisplayList(false);
                btnNewChild.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnNewChild_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    if (dtHeader.Rows.Count == 0)
                    {
                        OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        btnNewChild.Focus();
                    }
                    else
                    {
                        BtnSave.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpSchemeDateTo_Leave(object sender, EventArgs e)
        {
            try
            {
                validate_DtpSchemeDateTo();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpTillDateFrom_Leave(object sender, EventArgs e)
        {
            //DtpTillDateFrom_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            try
            {
                validate_DtpTillDateFrom();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbMScheme = new DBMScheme();
                mScheme = new MScheme();

                mScheme.SchemeNo = ID;
                mScheme.IsActive = 2;
                if (OMMessageBox.Show("Are you sure want to Closed scheme?" + Environment.NewLine + "ONCE A SCHEME " +
                          " IS CLOSED IT CANNOT BE RE-OPENED. TRANSACTION DONE " + Environment.NewLine +
                          " DURING THE PERIOD OF SCHEME WOULD CONTINUE TO REMAIN " + Environment.NewLine +
                          " VALID UPTO THE DATE OF THE SCHEMA WHEN THE SCHEME WAS ACTIVE.", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, 200) == DialogResult.Yes)
                //if (OMMessageBox.Show("Are you sure want to Closed this Scheme?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //if (OMMessageBox.Show("Once The Scheme Is Closed,you can not be ReActivate The Scheme ", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //if (dbMScheme.DeleteMScheme(mScheme) == true)
                    //{
                    //    OMMessageBox.Show("Scheme Closed Successfully.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //    FillControls();
                    //}
                    //else
                    //{
                    //    OMMessageBox.Show("Scheme not Closed.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    //}
                    //}
                    if (DtpSchemeDateTo.Value.Date > DBGetVal.ServerTime.Date)
                    {
                        if (DBGetVal.ServerTime.Date >= DtpSchemeDateFrom.Value.Date)
                        {
                            DtpSchemeDateTo.Value = DBGetVal.ServerTime.Date;
                            DtpTillDateFrom.MinDate = DtpSchemeDateTo.Value;
                            DtpTillDateFrom.Value = DtpSchemeDateTo.Value.Date.AddDays(1);
                            //DtpTillDateFrom.Value = DBGetVal.ServerTime.Date.AddDays(1);
                        }
                    }
                    lblSchemeStatus.Text = "Closed";
                    BtnSave_Click(sender, e);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Settings.Loyalty.Loyalty(SchemeTypeNo);
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

        }

        private bool validate_DtpSchemeDateFrom()
        {
            //try
            //{
            //    EP.SetError(DtpSchemeDateFrom, "");
            //    //DateTime dtDate = Convert.ToDateTime(DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY));
            //    //DtpSchemeDateFrom.Value = dtDate;
            //    if (TempdtSchFrom.ToString(Format.DDMMMYYYY) != DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY))
            //    {
            //        string sqlQuery = "";

            //        if (SchemeTypeNo == 5) //PSKUC
            //        {
            //            sqlQuery = "Select Count(*) from MScheme where SchemePeriodFrom <= '" + DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY) +
            //                "' And SchemePeriodTo >= '" + DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY) + "'  And SchemeTypeNo=" + SchemeTypeNo + " And IsActive in (0,1,2) And SchemeNo <> " + ID;
            //        }
            //        else if (SchemeTypeNo == 3 || SchemeTypeNo == 4)
            //        {
            //            sqlQuery = "Select Count(*) from MScheme where SchemePeriodFrom <= '" + DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY) +
            //                "' And SchemePeriodTo >= '" + DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY) + "'  And SchemeTypeNo=" + SchemeTypeNo + " And IsActive in (0,1) And SchemeNo <> " + ID;
            //        }

            //        if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
            //        {
            //            EP.SetError(DtpSchemeDateFrom, "Scheme already exist on selected date.");
            //            EP.SetIconAlignment(DtpSchemeDateFrom, ErrorIconAlignment.MiddleRight);
            //            return false;
            //        }
            //        else
            //            return true;
            //    }
            //    else
            //        return true;
            //}
            //catch (Exception exc)
            //{
            //    ObjFunction.ExceptionDisplay(exc.Message);
            //    return false;
            //}

            bool TFlag = true;
            try
            {

                EP.SetError(DtpSchemeDateFrom, "");
                if (Convert.ToDateTime(DtpSchemeDateFrom.Text) < DBGetVal.ServerTime.Date)
                {
                    EP.SetError(DtpSchemeDateFrom, "Please select date greater than equal to todays date.");
                    EP.SetIconAlignment(DtpSchemeDateFrom, ErrorIconAlignment.MiddleRight);
                    TFlag = false;
                }
                else
                    TFlag = true;


                return TFlag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool validate_DtpSchemeDateTo()
        {
            //try
            //{
            //    string sqlQuery = "";
            //    EP.SetError(DtpSchemeDateTo, "");
            //    //DateTime dtDate = Convert.ToDateTime(DtpSchemeDateTo.Value.ToString(Format.DDMMMYYYY));
            //    //DtpSchemeDateTo.Value = dtDate;

            //    if (DtpSchemeDateFrom.Value > DtpSchemeDateTo.Value)
            //    {
            //        EP.SetError(DtpSchemeDateTo, "Please select date after sceheme start date.");
            //        EP.SetIconAlignment(DtpSchemeDateTo, ErrorIconAlignment.MiddleRight);
            //        return false;
            //    }
            //    if (TempdtSchFrom.ToString(Format.DDMMMYYYY) != DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY))
            //    {
            //        if (SchemeTypeNo == 5) //PSKUC
            //        {
            //            sqlQuery = "Select Count(*) from MScheme where (SchemePeriodFrom <= '" + DtpSchemeDateTo.Value.ToString(Format.DDMMMYYYY) +
            //                "' And SchemePeriodTo >= '" + DtpSchemeDateTo.Value.ToString(Format.DDMMMYYYY) + "') And SchemeTypeNo=" +
            //                SchemeTypeNo + " And IsActive in (0,1,2) And SchemeNo <> " + ID;
            //        }
            //        else if (SchemeTypeNo == 3 || SchemeTypeNo == 4)
            //        {
            //            sqlQuery = "Select Count(*) from MScheme where (SchemePeriodFrom <= '" + DtpSchemeDateTo.Value.ToString(Format.DDMMMYYYY) +
            //                "' And SchemePeriodTo >= '" + DtpSchemeDateTo.Value.ToString(Format.DDMMMYYYY) + "') And SchemeTypeNo=" +
            //                SchemeTypeNo + " And IsActive in (0,1) And SchemeNo <> " + ID;
            //        }

            //        if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
            //        {
            //            EP.SetError(DtpSchemeDateTo, "Scheme already exist on selected date.");
            //            EP.SetIconAlignment(DtpSchemeDateTo, ErrorIconAlignment.MiddleRight);
            //            return false;
            //        }
            //    }

            //    if (TempdtSchTo.ToString(Format.DDMMMYYYY) != DtpSchemeDateTo.Value.ToString(Format.DDMMMYYYY))
            //    {
            //        sqlQuery = "Select Count(*) from MScheme where (SchemePeriodFrom >= '" + DtpSchemeDateFrom.Value.ToString(Format.DDMMMYYYY) +
            //            "' And SchemePeriodTo <= '" + DtpSchemeDateTo.Value.ToString(Format.DDMMMYYYY) + "') And SchemeTypeNo=" +
            //            SchemeTypeNo + " And IsActive in (0,1) And SchemeNo <> " + ID;
            //        if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
            //        {
            //            EP.SetError(DtpSchemeDateTo, "Scheme already exist in selected Period.");
            //            EP.SetIconAlignment(DtpSchemeDateTo, ErrorIconAlignment.MiddleRight);
            //            return false;
            //        }
            //    }

            //    return true;
            //}
            //catch (Exception exc)
            //{
            //    ObjFunction.ExceptionDisplay(exc.Message);
            //    return false;
            //}


            bool TFlag = true;
            try
            {
                //string sqlQuery = "";
                EP.SetError(DtpSchemeDateTo, "");

                if (DtpSchemeDateFrom.Value > DtpSchemeDateTo.Value)
                {
                    EP.SetError(DtpSchemeDateTo, "Please select date after scheme start date.");
                    EP.SetIconAlignment(DtpSchemeDateTo, ErrorIconAlignment.MiddleRight);
                    TFlag = false;
                }
                return TFlag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool validate_DtpTillDateFrom()
        {
            try
            {
                EP.SetError(DtpTillDateFrom, "");
                //DateTime dtDate = Convert.ToDateTime(DtpTillDateFrom.Value.ToString(Format.DDMMMYYYY));
                //DtpTillDateFrom.Value = dtDate;

                if (SchemeTypeNo == SchemeType.PSKU && DtpSchemeDateTo.Value > DtpTillDateFrom.Value)
                {
                    EP.SetError(DtpTillDateFrom, "Please select date after scheme end date.");
                    EP.SetIconAlignment(DtpTillDateFrom, ErrorIconAlignment.MiddleRight);
                    return false;
                }
                return true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool validate_DtpTillDateTo()
        {
            try
            {
                EP.SetError(DtpTillDateTo, "");
                //DateTime dtDate = Convert.ToDateTime(DtpTillDateTo.Value.ToString(Format.DDMMMYYYY));
                //DtpTillDateTo.Value = dtDate;

                if (SchemeTypeNo == SchemeType.PSKU && DtpTillDateFrom.Value > DtpTillDateTo.Value)
                {
                    EP.SetError(DtpTillDateTo, "Please select date after redemption start date.");
                    EP.SetIconAlignment(DtpTillDateTo, ErrorIconAlignment.MiddleRight);
                    return false;
                }
                return true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool validate_SchemeDetailsTo()
        {
            bool validFlag = false;
            try
            {
                EP.SetError(txtPercentage, "");
                EP.SetError(txtRs, "");
                if (chkDiscValue.Checked == true)
                {
                    if (Convert.ToDouble(txtPercentage.Text) == 0)
                    {
                        EP.SetError(txtPercentage, "Enter %");
                        EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                        txtPercentage.Focus();
                        return false;
                    }
                    else if (Convert.ToDouble(txtRs.Text) == 0)
                    {
                        EP.SetError(txtRs, "Enter Rs.");
                        EP.SetIconAlignment(txtRs, ErrorIconAlignment.MiddleRight);
                        txtRs.Focus();
                        return false;
                    }
                    else validFlag = true;
                }
                if (chkProductDisc.Checked == true)
                {
                    if (dgBill.Rows.Count <= 1)
                    {
                        OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        dgBill.Focus();
                        return false;
                    }
                    else
                    {
                        for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                        {
                            if (dgBill.Rows[i].Cells[ColIndex.ItemName].ErrorText != "" || dgBill.Rows[i].Cells[ColIndex.Quantity].ErrorText != "")
                            {
                                validFlag = false;
                                dgBill.Focus();
                                return false;
                            }
                            else validFlag = true;
                        }
                    }
                }
                return validFlag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool validate_txtSchemeName()
        {
            try
            {
                EP.SetError(txtSchemeName, "");
                if (txtSchemeName.Text.Trim() == "")
                {
                    EP.SetError(txtSchemeName, "Enter Scheme Name");
                    EP.SetIconAlignment(txtSchemeName, ErrorIconAlignment.MiddleRight);
                    return false;
                }
                if (SchemeName.ToUpper() != txtSchemeName.Text.Trim().ToUpper())
                {
                    if (ObjQry.ReturnInteger("Select Count(*) from MScheme where SchemeName = '" + txtSchemeName.Text.Trim().ToUpper() + "' And SchemeTypeNo=" + SchemeTypeNo + "  and MScheme.IsIWScheme='False'", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtSchemeName, "Duplicate Scheme Name");
                        EP.SetIconAlignment(txtSchemeName, ErrorIconAlignment.MiddleRight);
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void DtpSchemeDateFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DtpSchemeDateTo.MinDate = DtpSchemeDateFrom.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpTillDateFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DtpTillDateTo.MinDate = DtpTillDateFrom.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DtpSchemeDateFrom.MinDate = DtpDate.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpSchemeDateTo_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DtpTillDateFrom.MinDate = DtpSchemeDateTo.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btncopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (OMMessageBox.Show("Are you sure want to Copy this Scheme?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ObjFunction.LockButtons(false, this.Controls);
                    ObjFunction.LockControls(true, this.Controls);
                    DtpDate.Enabled = false;
                    //btncancelchild.Visible = false;
                    //btnUpdateChild.Visible = true;
                    //btnNewChild.Enabled = true;
                    //btnUpdateChild.Enabled = true;

                    dgBill.Enabled = true;
                    dgBillFrom.Enabled = true;

                    btnEdit.Enabled = true;

                    txtSchemeUserNo.Text = ObjQry.ReturnString("Select SchemeUserNo From GetMaxSchemeUserNo(" + SchemeTypeNo + ",1) ", CommonFunctions.ConStr);
                    ID = 0;
                    SetSchemeStatus(0);
                    SchemeName = "";
                    TempdtSchFrom = Convert.ToDateTime("01-Jan-1900");
                    TempdtSchTo = Convert.ToDateTime("01-Jan-1900");

                    for (int i = 0; i < dtHeader.Rows.Count; i++)
                    {
                        dtHeader.Rows[i][ColdtHeader.SchemeDetailsNo] = 0;
                        dtHeader.Rows[i][ColdtHeader.SchemeDetailsFromNo] = 0;
                        dtHeader.Rows[i][ColdtHeader.SchemeNo] = 0;
                        dtHeader.Rows[i][ColdtHeader.SchemeDetailsNo] = 0;
                        dtHeader.AcceptChanges();
                    }
                    for (int i = 0; i < dtSchemeFrom.Rows.Count; i++)
                    {
                        dtSchemeFrom.Rows[i][ColIndex.SchemeDtlsNo] = 0;
                        dtSchemeFrom.Rows[i][ColIndex.SchemeNo] = 0;
                        dtSchemeFrom.Rows[i][ColIndex.SchemeFromNo] = 0;
                        dtSchemeFrom.AcceptChanges();
                    }

                    for (int i = 0; i < dtSchemeTo.Rows.Count; i++)
                    {
                        dtSchemeTo.Rows[i][ColIndex.SchemeDtlsNo] = 0;
                        dtSchemeTo.Rows[i][ColIndex.SchemeNo] = 0;
                        dtSchemeTo.Rows[i][ColIndex.SchemeFromNo] = 0;
                        dtSchemeTo.AcceptChanges();
                    }

                    NavigationDisplayFrom(5);
                    txtSchemeName.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnAssignCust_Click(object sender, EventArgs e)
        {
            try
            {
                if (BtnSave.Visible == false && lblSchemeStatus.Text != "Closed")
                {
                    if (ID != 0)
                    {
                        Form frm = new LoyaltyAssign(ID, dtSchemeAssign, 1);
                        ObjFunction.OpenForm(frm);
                    }

                    // dtSchemeAssign = ((LoyaltyAssign)frm).dtSchemeAssign;
                }
                else if (BtnSave.Visible == false && lblSchemeStatus.Text == "Closed")
                {
                    if (ID != 0)
                    {
                        Form frm = new LoyaltyAssign(ID, dtSchemeAssign, 2);
                        ObjFunction.OpenForm(frm);
                    }

                    // dtSchemeAssign = ((LoyaltyAssign)frm).dtSchemeAssign;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lblSchemeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string strMessage = "";
                if (ID != 0)
                {
                    dbMScheme = new DBMScheme();
                    mScheme = new MScheme();
                    mScheme.SchemeNo = ID;
                    if (SchemeTypeNo == SchemeType.TVB) strMessage = Environment.NewLine + Environment.NewLine + "Note: Scheme is active on " + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY);
                    if (lblSchemeStatus.Text == "Draft")
                    {
                        //SchemeTypeNo == SchemeType.TVB &&
                        if (ObjFunction.GetComboValue(cmbDiscType) <= 2)
                        {
                            if (ObjQry.ReturnLong("Select Count(*) From MSchemeDetails Where SchemeNo=" + ID + " AND DiscAmount=0", CommonFunctions.ConStr) > 0)
                            {
                                OMMessageBox.Show("Please properly fill scheme details...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                return;
                            }
                        }
                        else if (ObjFunction.GetComboValue(cmbDiscType) == 3)
                        {
                            DataTable dtS = ObjFunction.GetDataView("Select PKSrNo From MSchemeDetails Where SchemeNo=" + ID + "").Table;
                            bool flag = true;
                            for (int i = 0; i < dtS.Rows.Count; i++)
                            {
                                if (ObjQry.ReturnLong("Select Count(*) From MSchemeToDetails Where SchemeNo=" + ID + " AND SchemeDetailsNo =" + dtS.Rows[i].ItemArray[0].ToString() + "", CommonFunctions.ConStr) <= 0)
                                {
                                    flag = false;
                                    OMMessageBox.Show("Please properly fill scheme details...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                    break;
                                }
                            }
                            if (flag == false) return;
                        }

                        //if (DBGetVal.ServerTime.Date >= DtpSchemeDateFrom.Value.Date && DBGetVal.ServerTime.Date <= DtpSchemeDateTo.Value.Date)
                        //{ }
                        //else
                        //{
                        //    OMMessageBox.Show("Scheme period is already expired" + Environment.NewLine + " Not Allowed scheme Active...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //    return;
                        //}

                        if (DtpSchemeDateTo.Value.Date < DBGetVal.ServerTime.Date)
                        {
                            OMMessageBox.Show("Scheme period is already expired" + Environment.NewLine + " Not Allowed scheme Active...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            return;
                        }

                        if (SchemeTypeNo == SchemeType.PSKU)
                        {
                            if (DtpSchemeDateFrom.Value.Date > DBGetVal.ServerTime.Date)
                            {
                                OMMessageBox.Show("Not allowed future scheme activate...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                return;
                            }
                        }
                        if (OMMessageBox.Show("Are you sure want to Active scheme?" + strMessage, CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DtpSchemeDateFrom.Value = DBGetVal.ServerTime.Date;
                            lblSchemeStatus.Text = "Active";
                            BtnSave_Click(sender, e);
                            //mScheme.IsActive = 1;
                            //if (dbMScheme.DeleteMScheme(mScheme) == true)
                            // FillControls();
                        }
                    }
                    else if (lblSchemeStatus.Text == "Active")
                    {
                        if (OMMessageBox.Show("Are you sure want to Closed scheme?" + Environment.NewLine + "ONCE A SCHEME " +
                          " IS CLOSED IT CANNOT BE RE-OPENED. TRANSACTION DONE " + Environment.NewLine +
                          " DURING THE PERIOD OF SCHEME WOULD CONTINUE TO REMAIN " + Environment.NewLine +
                          " VALID UPTO THE DATE OF THE SCHEMA WHEN THE SCHEME WAS ACTIVE.", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, 200) == DialogResult.Yes)
                        //if (OMMessageBox.Show("Are you sure want to Closed scheme?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            //lblSchemeStatus.Text = "Closed";
                            //BtnSave_Click(sender, e);
                            //mScheme.IsActive = 2;
                            //if (dbMScheme.DeleteMScheme(mScheme) == true)
                            //    FillControls();
                            if (DtpSchemeDateTo.Value.Date > DBGetVal.ServerTime.Date)
                            {
                                if (DBGetVal.ServerTime.Date >= DtpSchemeDateFrom.Value.Date)
                                {
                                    DtpSchemeDateTo.Value = DBGetVal.ServerTime.Date;
                                    DtpTillDateFrom.MinDate = DtpSchemeDateTo.Value;
                                    DtpTillDateFrom.Value = DtpSchemeDateTo.Value.Date.AddDays(1);
                                    //DtpTillDateFrom.Value = DBGetVal.ServerTime.Date.AddDays(1);
                                }
                            }
                            lblSchemeStatus.Text = "Closed";
                            BtnSave_Click(sender, e);
                        }
                    }
                    else if (lblSchemeStatus.Text == "Closed")
                    {
                        //         //SchemeTypeNo == SchemeType.TVB
                        //         if (DBGetVal.ServerTime.Date >= DtpSchemeDateFrom.Value.Date && DBGetVal.ServerTime.Date <= DtpSchemeDateTo.Value.Date)
                        //         { }
                        //         else
                        //         {
                        //             OMMessageBox.Show("Not Allowed scheme Active...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //             return;
                        //         }
                        //         if (OMMessageBox.Show("Are you sure want to Active scheme?" + strMessage, CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        //         {
                        //             DtpSchemeDateFrom.Value = DBGetVal.ServerTime.Date;
                        //             if (!validate_txtSchemeName() || !validate_DtpSchemeDateFrom() || !validate_DtpSchemeDateTo()
                        //|| !validate_DtpTillDateFrom() || !validate_DtpTillDateTo())
                        //             { }
                        //             else
                        //             {
                        //                 lblSchemeStatus.Text = "Active";
                        //                 BtnSave_Click(sender, e);
                        //             }
                        //             //mScheme.IsActive = 1;
                        //             //if (dbMScheme.DeleteMScheme(mScheme) == true)
                        //             //    FillControls();
                        //         }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbDiscType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbDiscType) > 0)
                {
                    if (dgBillFrom.Rows.Count < 1)
                    {
                        pnlBgFrom.Enabled = true;
                        TypeNo = 0;
                        //dgBillFrom.Enabled = true;
                        dgBillFrom.Rows.Add();
                        dgBillFrom.Focus();
                        dgBillFrom.CurrentCell = dgBillFrom.Rows[0].Cells[1];

                    }
                    else
                    {
                        pnlBgFrom.Enabled = true;
                        dgBillFrom.Focus();
                        dgBillFrom.CurrentCell = dgBillFrom.Rows[dgBillFrom.Rows.Count - 1].Cells[1];
                    }
                    //btnNewChild.Focus();
                }
            }
        }

        private void cmbDiscType_Leave(object sender, EventArgs e)
        {
            if (iDiscountType != ObjFunction.GetComboValue(cmbDiscType))
            {
                iDiscountType = (int)ObjFunction.GetComboValue(cmbDiscType);
                clearSchemeDT();
                ApplyDiscTypeOnCHK();
            }
        }

        private void clearSchemeDT()
        {
            for (int i = 0; i < dtHeader.Rows.Count; i++)
            {
                dtHeader.Rows[i][ColdtHeader.DiscAmount] = 0;
                dtHeader.Rows[i][ColdtHeader.DiscPercentage] = 0;
            }

            dtHeader.AcceptChanges();

            for (int i = 0; i < dtSchemeFrom.Rows.Count; i++)
            {
                dtSchemeFrom.Rows[i][ColIndex.DiscPercentage] = 0;
            }

            while (dtSchemeTo.Rows.Count > 0)
            {
                if (dtSchemeTo.Rows[0][ColIndex.SchemeFromNo].ToString() == "0" || dtSchemeTo.Rows[0][ColIndex.SchemeFromNo].ToString().Trim() == "")
                {
                    dtSchemeTo.Rows.RemoveAt(0);
                }
                else
                {
                    DeleteDtls(2, Convert.ToInt64(dtSchemeTo.Rows[0][ColIndex.SchemeFromNo].ToString()));
                    dtSchemeTo.Rows.RemoveAt(0);
                }
            }

            txtPercentage.Text = "0.00";
            txtRs.Text = "0.00";

            dgBill.Enabled = true;
            while (dgBill.Rows.Count > 0)
            {
                dgBill.Rows.RemoveAt(0);
            }
        }

        private void ApplyDiscTypeOnCHK()
        {
            switch (ObjFunction.GetComboValue(cmbDiscType))
            {
                case 1://Percent
                    chkDiscValue.Checked = true;
                    chkProductDisc.Checked = false;
                    break;
                case 2://discount rs
                    chkDiscValue.Checked = true;
                    chkProductDisc.Checked = false;
                    break;
                case 3://product
                    chkDiscValue.Checked = false;
                    chkProductDisc.Checked = true;
                    break;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                BtnSave.Visible = false;
                //btncancelchild.Visible = false;
                //btnUpdateChild.Visible = true;
                //btnNewChild.Enabled = true;
                //btnUpdateChild.Enabled = true;
                btncopy.Visible = false;
                DtpDate.Enabled = false;
                dgBill.Enabled = true;
                dgBillFrom.Enabled = true;

                btnEdit.Enabled = true;
                txtSchemeName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_VisibleChanged(object sender, EventArgs e)
        {
            if (btnCancel.Visible == false)
                btnView.Visible = !btnUpdate.Visible;
            else btnView.Visible = false;
        }

        private void txtPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(txtPercentage, "");
                if (chkDiscValue.Checked == true)
                {
                    if (txtPercentage.Text.Trim() == "")
                    {
                        txtPercentage.Text = "0.00";
                    }
                    if (Convert.ToDouble(txtPercentage.Text) <= 100.00)
                    {
                        if (txtPercentage.Text != "")
                        {
                            txtRs.Text = (Convert.ToDouble(txtAmount.Text) * (Convert.ToDouble(txtPercentage.Text) / 100)).ToString("0.00");
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Please Enter Value Less Than OR Equal To 100.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPercentage.Focus();
                        txtPercentage.Text = "0.00";
                        txtRs.Text = "0.00";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        //public bool CheckDuplicate()
        //{
        //    long cnt = ObjQry.ReturnLong("Select Count(*) FROM  MScheme INNER JOIN " +
        //            " MSchemeFromDetails ON MScheme.SchemeNo = MSchemeFromDetails.SchemeNo  " +
        //            " where MScheme.DiscType=" + ObjFunction.GetComboValue(cmbDiscType) + " and " +
        //            " MScheme.SchemePeriodFrom>='" + DtpSchemeDateFrom.Text + "' and " +
        //            " MScheme.SchemePeriodTo<='" + DtpSchemeDateTo.Text + "' ", CommonFunctions.ConStr);

        //    if (cnt == 0)
        //        return true;
        //    else
        //        return false;

        //}

        public bool CheckDuplicate(string strItemNo)
        {
            long cnt = 0;
            if (SchemeTypeNo == 3 || SchemeTypeNo == 4)
            {
                //cnt = ObjQry.ReturnLong("Select Count(*) FROM  MScheme INNER JOIN " +
                //         " MSchemeFromDetails ON MScheme.SchemeNo = MSchemeFromDetails.SchemeNo  " +
                //         " where ((MScheme.SchemePeriodFrom>='" + DtpSchemeDateFrom.Text + "' AND " +
                //         " MScheme.SchemePeriodTo<='" + DtpSchemeDateTo.Text + "') OR " +
                //         " (MScheme.SchemePeriodTo>='" + DtpSchemeDateFrom.Text + "' AND " +
                //         " MScheme.SchemePeriodTo<='" + DtpSchemeDateTo.Text + "') OR " +
                //         " (MScheme.SchemePeriodFrom>='" + DtpSchemeDateFrom.Text + "' AND " +
                //         " MScheme.SchemePeriodFrom<='" + DtpSchemeDateTo.Text + "')) And  MSchemeFromDetails.ItemNo in(" + strItemNo + ") and MScheme.SchemeNo!=" + ID + " and MScheme.IsIWScheme=0 AND MScheme.IsActive<>2  and MScheme.SchemeTYpeNo in (3,4)", CommonFunctions.ConStr);

                cnt = ObjQry.ReturnLong("Select Count(*) FROM  MScheme INNER JOIN " +
                         " MSchemeFromDetails ON MScheme.SchemeNo = MSchemeFromDetails.SchemeNo  " +
                         " where (((MScheme.SchemePeriodFrom >= '" + DtpSchemeDateFrom.Text + "') AND "+
                         " (MScheme.SchemePeriodTo <= '" + DtpSchemeDateTo.Text + "')) OR "+
                         " ((MScheme.SchemePeriodFrom <= '" + DtpSchemeDateFrom.Text + "') "+
                         " AND (MScheme.SchemePeriodTo >= '" + DtpSchemeDateTo.Text + "')) OR  "+
                         " (MScheme.SchemePeriodFrom <= '" + DtpSchemeDateFrom.Text + "')  "+
                         " AND ((MScheme.SchemePeriodTo <= '" + DtpSchemeDateTo.Text + "') AND  "+
                         " (MScheme.SchemePeriodTo >= '" + DtpSchemeDateFrom.Text + "') ) OR "+
                         " ((MScheme.SchemePeriodFrom >= '" + DtpSchemeDateFrom.Text + "' and MScheme.SchemePeriodFrom <= '" + DtpSchemeDateTo.Text + "') "+
                         " AND (MScheme.SchemePeriodTo >= '" + DtpSchemeDateTo.Text + "'))) And  MSchemeFromDetails.ItemNo in(" + strItemNo + ") and MScheme.SchemeNo!=" + ID + " and MScheme.IsIWScheme=0 AND MScheme.IsActive<>2  and MScheme.SchemeTYpeNo in (3,4)", CommonFunctions.ConStr);
            }
            else if (SchemeTypeNo == 5)
            {
                cnt = ObjQry.ReturnLong("Select Count(*) FROM  MScheme INNER JOIN " +
                         " MSchemeFromDetails ON MScheme.SchemeNo = MSchemeFromDetails.SchemeNo  " +
                         " where MScheme.SchemePeriodFrom>='" + DtpSchemeDateFrom.Text + "' and " +
                         " MScheme.SchemePeriodTo<='" + DtpSchemeDateTo.Text + "' and  MSchemeFromDetails.ItemNo in(" + strItemNo + ") and MScheme.SchemeNo!=" + ID + " and MScheme.IsIWScheme=0 AND MScheme.IsActive<>2 and MScheme.SchemeTYpeNo in (5)", CommonFunctions.ConStr);
            }
            if (cnt == 0)
                return true;
            else
                return false;

        }
    }
}
