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

namespace Yadi.Display
{
    public partial class StockSummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DBProgressBar PB;

        //bool ShowFlage = true;
        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "";
        public string strCategoryNo = "";
        public string strDepartmentNo = "";

        long MfgCompNo = 0;

        public StockSummary()
        {
            InitializeComponent();
        }

        public StockSummary(int MfgCompNo)
        {
            InitializeComponent();
            this.MfgCompNo = MfgCompNo;
        }

        private void StockSummary_Load(object sender, EventArgs e)
        {
            lblFirmName.Text = "";
            if (MfgCompNo != 0)
            {
                Form NewFrm = new Vouchers.FirmSelection();
                ObjFunction.OpenForm(NewFrm);
                MfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
            }
            CompNo = DBGetVal.FirmNo;
            label8.Text = "";
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            tabControl1.Visible = false;
            btnBarShow.Visible = false;
            KeyDownFormat(this.Controls);
            new GridSearch(gvItem, 1, 2);
            pnlDepartment.Visible = false;
            pnlItem.Visible = false;
            pnlcategory.Visible = false;

            groupBox1.Visible = false;
            chkBarcode.Visible = false;
            panel2.Visible = false;
            btnBarShow.Visible = false;
            pnlPB.Visible = false;
            label3.Visible = false;
            txtItemBarcode.Visible = false;

            rbGroupWise.Enabled = false;
            rbItemDailyDetails.Enabled = false;
            rbItemDailyDetails.Enabled = false;
            rbItemSummaryDtls.Enabled = false;
            if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)) == 2)
            {
                groupBox1.Visible = true;
                chkBarcode.Visible = true;
                panel2.Visible = true;
                btnBarShow.Visible = true;
                pnlPB.Visible = true;
                label3.Visible = true;
                txtItemBarcode.Visible = true;
                rbGroupWise.Enabled = true;
                rbItemDailyDetails.Enabled = true;
                rbItemDailyDetails.Enabled = true;
                rbItemSummaryDtls.Enabled = true;
            }
                 BtnItmShow.Visible = false;
                // SelectRedioButton();

            }

        //public void SelectRedioButton()
        //{
        //    if(rbNone.Checked == true || rbItemWise.Checked == true || rbItemSummaryDtls.Checked == true || rbGroupWise.Checked == true)
        //    {
        //        BtnItmShow.Visible = true;
        //    }

        //}


        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is CheckBox)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                strItemNo = "";
                btnPrint.Visible = false;
                //DataGridView1.DataSource = null;
                while (DataGridView1.Rows.Count > 0)
                    DataGridView1.Rows.RemoveAt(0);

                for (int i = 0; i < gvItem.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvItem.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strItemNo == "")
                            strItemNo = gvItem.Rows[i].Cells[0].Value.ToString();
                        else
                            strItemNo = strItemNo + "," + gvItem.Rows[i].Cells[0].Value.ToString();
                    }
                }

               if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)) == 2)
                {
                    if (strItemNo != "")
                    {
                        if (rbNone.Checked == true)
                        {
                            panel1.Visible = false;
                            PB = new DBProgressBar(this);
                            PB.TimerStart();
                            PB.Ctrl = tabControl1;
                            tabControl1.SelectedTab = tabPage1;
                            //ShowFlage = true;
                            if (DBGetVal.KachhaFirm == false)
                            {
                                dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + strItemNo + "',0", CommonFunctions.ConStr);

                            }
                            else
                            {
                                dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + strItemNo + "',1", CommonFunctions.ConStr);

                            }
                            DataTable dt = dsVd.Tables[0];
                            DataRow dr = dt.NewRow();
                            dsVd.Tables[0].Rows.Add(dr);
                            DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                            GetCount();
                            if (DataGridView1.Rows.Count > 1)
                                btnPrint.Visible = true;
                            else btnPrint.Visible = false;

                        }
                        else if (rbItemWise.Checked == true)
                        {
                            ReportSession = new string[5];


                            ReportSession[0] = DBGetVal.FirmNo.ToString();
                            ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                            ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                            if (chkBarcode.Checked == true)
                            {
                                ReportSession[3] = BItemNo.ToString();
                            }
                            else
                                ReportSession[3] = strItemNo;
                            if (DBGetVal.KachhaFirm == false)
                            {
                                ReportSession[4] = "0";
                            }
                            else
                            { ReportSession[4] = "1"; }
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNew.rpt", CommonFunctions.ReportPath), ReportSession);




                            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                        }
                        else if (rbItemSummaryDtls.Checked == true)
                        {
                            ReportSession = new string[9];

                            ReportSession[0] = "Stock Summary Details";
                            ReportSession[1] = DBGetVal.FirmNo.ToString();
                            ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                            ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                            ReportSession[4] = strItemNo;
                            ReportSession[5] = "0";//Type
                            ReportSession[6] = "0";//No
                            ReportSession[7] = 1.ToString();
                            if (DBGetVal.KachhaFirm == false)
                            {
                                ReportSession[8] = 0.ToString();
                            }
                            else
                            {
                                ReportSession[8] = 1.ToString();
                            }

                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewStockSummDtlsByMonthlyRType(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummDtlsByMonthlyRType.rpt", CommonFunctions.ReportPath), ReportSession);


                            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                        }
                        else if (rbItemDailyDetails.Checked == true)
                        {
                            ReportSession = new string[6];

                            ReportSession[0] = MNo.ToString();
                            ReportSession[1] = DBGetVal.FirmNo.ToString();
                            ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                            ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                            ReportSession[4] = strItemNo;


                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewGetItemClosingStockByDateRType(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGetItemClosingStockByDateRType.rpt", CommonFunctions.ReportPath), ReportSession);

                            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                        }
                        else if (rbGroupWise.Checked == true)
                        {

                            ReportSession = new string[5];

                            ReportSession[0] = DBGetVal.FirmNo.ToString();
                            ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                            ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                            if (chkBarcode.Checked == true)
                            {
                                ReportSession[3] = BItemNo.ToString();
                            }
                            else
                                ReportSession[3] = strItemNo;
                            if (DBGetVal.KachhaFirm == false)
                            {
                                ReportSession[4] = "0";
                            }
                            else
                            { ReportSession[4] = "1"; }
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryBrandWise(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryBrandWise.rpt", CommonFunctions.ReportPath), ReportSession);


                            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                        }
                    }
                    else
                        OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);



                    if (DataGridView1.Rows.Count >= 1)
                    {
                        btnPrint.Visible = true;
                        DataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else btnPrint.Visible = false;
                }
                else
                {
                    Catogery();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void Catogery()
        {
            string[] ReportSession;
            Form NewF = null;
            strCategoryNo = "";
            while (DataGridView1.Rows.Count > 0)
                DataGridView1.Rows.RemoveAt(0);

            for (int i = 0; i < gvcategory.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvcategory.Rows[i].Cells[2].FormattedValue) == true)
                {
                    if (strCategoryNo == "")
                        strCategoryNo = gvcategory.Rows[i].Cells[0].Value.ToString();
                    else
                        strCategoryNo = strCategoryNo + "," + gvcategory.Rows[i].Cells[0].Value.ToString();
                }
            }
            if (strCategoryNo != "")
            {
                if (rbNone.Checked == true)
                {
                    panel1.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = tabControl1;
                    tabControl1.SelectedTab = tabPage1;
                    //ShowFlage = true;
                    if (DBGetVal.KachhaFirm == false)
                    {
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + strItemNo + "',0", CommonFunctions.ConStr);

                    }
                    else
                    {
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + strItemNo + "',1", CommonFunctions.ConStr);

                    }
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    GetCount();
                    if (DataGridView1.Rows.Count > 1)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;

                }
                else if (rbItemWise.Checked == true)
                {
                    ReportSession = new string[5];


                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    if (chkBarcode.Checked == true)
                    {
                        ReportSession[3] = BItemNo.ToString();
                    }
                    else
                        ReportSession[3] = strCategoryNo;
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[4] = "0";
                    }
                    else
                    { ReportSession[4] = "1"; }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                       NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNewCategory(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNewCategory.rpt", CommonFunctions.ReportPath), ReportSession);

                    
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else if (rbItemSummaryDtls.Checked == true)
                {
                    ReportSession = new string[9];

                    ReportSession[0] = "Stock Summary Details";
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = strCategoryNo;
                    ReportSession[5] = "0";//Type
                    ReportSession[6] = "0";//No
                    ReportSession[7] = 1.ToString();
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[8] = 0.ToString();
                    }
                    else
                    {
                        ReportSession[8] = 1.ToString();
                    }

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummDtlsByMonthlyRType(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummDtlsByMonthlyRType.rpt", CommonFunctions.ReportPath), ReportSession);


                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rbItemDailyDetails.Checked == true)
                {
                    ReportSession = new string[6];

                    ReportSession[0] = MNo.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = strCategoryNo;


                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewGetItemClosingStockByDateRType(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGetItemClosingStockByDateRType.rpt", CommonFunctions.ReportPath), ReportSession);

                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rbGroupWise.Checked == true)
                {

                    ReportSession = new string[5];

                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    if (chkBarcode.Checked == true)
                    {
                        ReportSession[3] = BItemNo.ToString();
                    }
                    else
                        ReportSession[3] = strCategoryNo;
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[4] = "0";
                    }
                    else
                    { ReportSession[4] = "1"; }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryBrandWise(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryBrandWise.rpt", CommonFunctions.ReportPath), ReportSession);


                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
            }
            else
                OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);



            if (DataGridView1.Rows.Count >= 1)
            {
                btnPrint.Visible = true;
                DataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else btnPrint.Visible = false;
        }

        public void ItemBarcodeBindGrid()
        {

            DataTable dt = new DataTable();
            string str = " ";
            str = " SELECT ItemNo,isnull (SUM(EQTY),0) AS EQTY ,isnull (SUM(EBQTY),0) AS EBQTY,isnull (SUM(PBQTY),0) AS PBQTY,isnull (SUM(SBQTY),0) AS SBQTY FROM (SELECT     ItemNo," +
                  " CASE WHEN ITYPE = 1 AND TRNCODE = 1 THEN SUM (ISNULL(BilledQuantity, 0))  END AS EQTY," +
                  " CASE WHEN ITYPE = 1 AND TRNCODE = 2 THEN  SUM(ISNULL(BilledQuantity, 0)) END AS EBQTY," +
                  " CASE WHEN ITYPE = 0 AND TRNCODE = 1 THEN  SUM(ISNULL(BilledQuantity, 0))  END AS PBQTY," +
                  " CASE WHEN ITYPE = 0 AND TRNCODE = 2  AND FKVOUCHERNO IN(SELECT PKVOUCHERNO FROM TVOUCHERENTRY WHERE VOUCHERTYPECODE = 15)THEN  SUM(ISNULL(BilledQuantity, 0)) END AS SBQTY," +
                  " IType FROM  TStock where itemno in (select itemno from mitemmaster where barcode = '" + txtItemBarcode.Text + "') GROUP BY ITEMNO ,ITYPE,TrnCode,FKVOUCHERNO)AS B " +
                  " GROUP BY ITEMNO ,ITYPE ORDER BY ITEMNO";

            dt = ObjFunction.GetDataView(str).Table;
            double Purqty = 0.0, Saleqty = 0.0, Totalqty = 0.0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Purqty = Convert.ToDouble(dt.Rows[0].ItemArray[1].ToString()) + Convert.ToDouble(dt.Rows[0].ItemArray[3].ToString());
                Saleqty = Convert.ToDouble(dt.Rows[0].ItemArray[2].ToString()) + Convert.ToDouble(dt.Rows[0].ItemArray[4].ToString());
                Totalqty = Purqty - Saleqty;
                lblmsg.Text = Totalqty.ToString("0.00");
            }
        }

        public void BindGrid()
        {
            try
            {
                tabControl1.Visible = false;
                panel1.Visible = false;
                pnlPB.Visible = true;
                PBBar.Minimum = 1;
                PBBar.Value = 5;
                DataTable dt = new DataTable();
                string strWhere = "";
                if (rdActive.Checked == true)
                    strWhere = " Where mItemMaster.IsActive='true' ";
                else if (rdDeActive.Checked == true)
                    strWhere = " Where mItemMaster.IsActive='false' ";

                string str = "";

                if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)) == 2)
                {
                    if (DBGetVal.KachhaFirm == false)
                    {
                        str = " SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN(ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                          " WHERE(MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                          " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo " +
                            strWhere + "and mItemMaster.ESFlag='false'" +
                          " ORDER BY ItemName ";
                    }
                    else
                    {
                        str = " SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN(ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                          " WHERE(MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                          " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo " +
                          strWhere +
                          " ORDER BY ItemName ";
                    }
                }
                else
                {
                    if (DBGetVal.KachhaFirm == false)
                    { str ="SELECT DISTINCT MItemMaster.ItemNo, " + 
                            "(SELECT     MItemGroup.ItemGroupName + ' ' + CASE WHEN(ItemShortName <> '') THEN ItemShortName ELSE ItemName END  " + 
                            "  FROM          MItemMaster AS MItemMaster_1 INNER JOIN " + 
                            " MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo  WHERE(MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk" + 
                       " FROM TStock INNER JOIN " + 
                      " MItemMaster ON TStock.ItemNo = MItemMaster.ItemNo INNER JOIN " + 
                      " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo LEFT OUTER JOIN " + 
                      " MItemGroup AS MItemGroup_3 ON MItemMaster.FkCategoryNo = MItemGroup_3.ItemGroupNo LEFT OUTER JOIN " + 
                      " MItemGroup AS MItemGroup_2 ON MItemMaster.FkDepartmentNo = MItemGroup_2.ItemGroupNo " + 
                      " " + strWhere +" AND(MItemMaster.ESFlag = 'false') ORDER BY ItemName";
                        //str = " SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN(ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                        //  " WHERE(MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                        //  " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo inner join mitemgroup  on mItemMaster.fkdepartmentno =  mitemgroup.ItemGroupNo " +
                        //    strWhere + "and mItemMaster.ESFlag='false'" +
                        //  " ORDER BY ItemName ";
                    }
                    else
                    {
                        str = " SELECT DISTINCT  mItemMaster.ItemNo,   (SELECT  MItemGroup.ItemGroupName + ' ' + CASE WHEN(ItemShortName <> '') THEN ItemShortName ELSE ItemName END FROM  MItemMaster AS MItemMaster_1 INNER JOIN MItemGroup ON MItemMaster_1.GroupNo = MItemGroup.ItemGroupNo " +
                          " WHERE(MItemMaster_1.ItemNo = mItemMaster.ItemNo)) AS ItemName, 'false' AS chk " +
                          " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo  " +
                          strWhere +
                          " ORDER BY ItemName ";
                    }
                }

                dt = ObjFunction.GetDataView(str).Table;
                PBBar.Maximum = dt.Rows.Count + 5;


                gvItem.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvItem.Rows.Add();
                    Application.DoEvents();
                    PBBar.Value += 1;
                    for (int j = 0; j < gvItem.Columns.Count; j++)
                        gvItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                }
                gvItem.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvItem.Rows.Count > 0)
                {
                    gvItem.Focus();
                    gvItem.CurrentCell = gvItem[2, 0];
                }
                //PBBar.Value = 0;
                pnlPB.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }

        public void BindGridDept()
        {
            try
            {
                tabControl1.Visible = false;
                panel1.Visible = false;
                pnlPB.Visible = true;
                PBBar.Minimum = 1;
                PBBar.Value = 5;
                DataTable dt = new DataTable();
                string strWhere = "";
                if (rdActive.Checked == true)
                    strWhere = "  MItemGroup.IsActive='true' ";
                else if (rdDeActive.Checked == true)
                    strWhere = "  MItemGroup.IsActive='false' ";

                string str = "";


                if (DBGetVal.KachhaFirm == false)
                {
                    str = "select  ItemGroupNo, ItemGroupName from MItemGroup where  ItemGroupNo in (Select distinct FkDepartmentNo from mitemmaster ) and isactive = 'true'";
                   // str = "SELECT ItemGroupNo, ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup = 4 and  " + strWhere + " ORDER BY ItemGroupName";                    
                }
                else
                {
                    str = "select  ItemGroupNo, ItemGroupName from MItemGroup where  ItemGroupNo in (Select distinct FkDepartmentNo from mitemmaster ) and isactive = 'true'";
                    //str = "SELECT ItemGroupNo, ItemGroupName  From MItemGroup WHERE IsActive = 'True' AND ControlGroup = 4 and " + strWhere + " ORDER BY ItemGroupName";
                }

                dt = ObjFunction.GetDataView(str).Table;
                PBBar.Maximum = dt.Rows.Count + 5;

                gvDepartment.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvDepartment.Rows.Add();
                    Application.DoEvents();
                    PBBar.Value += 1;
                    for (int j = 0; j < gvDepartment.Columns.Count - 1; j++)
                    {
                        gvDepartment.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                }
                gvDepartment.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvDepartment.Rows.Count > 0)
                {
                    gvDepartment.Focus();
                    gvDepartment.CurrentCell = gvDepartment[2, 0];
                }
                //PBBar.Value = 0;
                pnlPB.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridcat()
        {
            try
            {
                tabControl1.Visible = false;
                panel1.Visible = false;
                //pnlPB.Visible = true;
                PBBar.Minimum = 1;
                PBBar.Value = 5;
                DataTable dt = new DataTable();
                string strWhere = "";
                if (rdActive.Checked == true)
                    strWhere = "  MItemGroup.IsActive='true' ";
                else if (rdDeActive.Checked == true)
                    strWhere = "  MItemGroup.IsActive='false' ";

                string str = "";


                if (DBGetVal.KachhaFirm == false)
                {
                    str = "Select itemgroupno, itemgroupname from mitemgroup where itemgroupno in (" +
                         "select distinct fkcategoryno  from mitemmaster  where fkdepartmentno in  (" +  (strDepartmentNo) + "))";
                    //str = "Select itemgroupno, itemgroupname from mitemgroup where itemgroupno in (" + 
                    //      "select distinct fkcategoryno  from mitemmaster  inner join " +
                    //      "fn_Split (" + (strDepartmentNo) + ") As Party_BILL on mitemmaster.fkdepartmentno= CAST(Party_BILL.value AS numeric) ) ";
                   // str = "Select ItemGroupNo,ItemGroupName from MItemGroup where ItemGroupNo in (select Distinct FkCategoryNo from mitemmaster WHERE mitemmaster.IsActive = 'true' and FkDepartmentNo = " + strDepartmentNo + " ) and IsActive = 'true' order by ItemGroupName";
                    // str = "SELECT ItemGroupNo, ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup = 4 and  " + strWhere + " ORDER BY ItemGroupName";                    
                    //" + strDepartmentNo + "
                }
                else
                {
                    str = "Select itemgroupno, itemgroupname from mitemgroup where itemgroupno in (" +
                         "select distinct fkcategoryno  from mitemmaster  where fkdepartmentno in  (" + (strDepartmentNo) + "))";
                   // str = "Select itemgroupno, itemgroupname from mitemgroup where itemgroupno in (" +
                   //       "select distinct fkcategoryno  from mitemmaster  inner join " +
                   //       "dbo.fn_Split (''' + (strDepartmentNo) + ''','' ) As Party_BILL on mitemmaster.fkdepartmentno= CAST(Party_BILL.value AS numeric)" +
                   //       "  ) ";
                   //// str = "Select ItemGroupNo,ItemGroupName from MItemGroup where ItemGroupNo in (select Distinct FkCategoryNo from mitemmaster WHERE mitemmaster.IsActive = 'true' and FkDepartmentNo = " + strDepartmentNo + " ) and IsActive = 'true' order by ItemGroupName";
                    //str = "SELECT ItemGroupNo, ItemGroupName  From MItemGroup WHERE IsActive = 'True' AND ControlGroup = 4 and " + strWhere + " ORDER BY ItemGroupName";
                }

                dt = ObjFunction.GetDataView(str).Table;
                PBBar.Maximum = dt.Rows.Count + 5;

                gvcategory.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvcategory.Rows.Add();
                    Application.DoEvents();
                    PBBar.Value += 1;
                    for (int j = 0; j < gvcategory.Columns.Count - 1; j++)
                    {
                        gvcategory.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                }
                gvcategory.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvcategory.Rows.Count > 0)
                {
                    gvcategory.Focus();
                    gvcategory.CurrentCell = gvcategory[2, 0];
                }
                //PBBar.Value = 0;
               // pnlPB.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

         public void BindGridItem()
        {
            try
            {
                tabControl1.Visible = false;
                panel1.Visible = false;
                pnlPB.Visible = true;
                PBBar.Minimum = 1;
                PBBar.Value = 5;
                DataTable dt = new DataTable();
                string strWhere = "";
                if (rdActive.Checked == true)
                    strWhere = "  MItemGroup.IsActive='true' ";
                else if (rdDeActive.Checked == true)
                    strWhere = "  MItemGroup.IsActive='false' ";

                string str = "";


                if (DBGetVal.KachhaFirm == false)
                {
                    str = "SELECT    mitemmaster.itemno, MItemGroup.ItemGroupName, MItemGroup_1.ItemGroupName AS Expr1, MItemMaster.ItemName " +
                          "FROM MItemMaster INNER JOIN " +
                          "MItemGroup AS MItemGroup_1 ON MItemMaster.GroupNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                          "MItemGroup ON MItemMaster.FkCategoryNo = MItemGroup.ItemGroupNo " +
                          "where MItemMaster.fkdepartmentno in (" +  (strDepartmentNo) + ")" ;
                    //str = "Select ItemNo, ItemName from MItemMaster where FkDepartmentNo in " +
                    //      "select distinct fkcategoryno from mitemmaster where fkdepartmentno in  (" +  (strDepartmentNo) + "))  and FkCategoryNo = 26";
                  }
                else
                {
                    str = "SELECT    mitemmaster.itemno, MItemGroup.ItemGroupName, MItemGroup_1.ItemGroupName AS Expr1, MItemMaster.ItemName " +
                          "FROM MItemMaster INNER JOIN " +
                          "MItemGroup AS MItemGroup_1 ON MItemMaster.GroupNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                          "MItemGroup ON MItemMaster.FkCategoryNo = MItemGroup.ItemGroupNo " +
                          "where MItemMaster.fkdepartmentno in (select distinct fkcategoryno from mitemmaster where fkdepartmentno in  (" + (strDepartmentNo) + "))";
                    //str = "Select ItemNo, ItemName from MItemMaster where FkDepartmentNo = 48 and FkCategoryNo = 26";
                    // str = "select  ItemGroupNo, ItemGroupName from MItemGroup where  ItemGroupNo in (Select distinct FkDepartmentNo from mitemmaster ) and isactive = 'true'";
                    //str = "SELECT ItemGroupNo, ItemGroupName  From MItemGroup WHERE IsActive = 'True' AND ControlGroup = 4 and " + strWhere + " ORDER BY ItemGroupName";
                }

                dt = ObjFunction.GetDataView(str).Table;
                PBBar.Maximum = dt.Rows.Count + 5;

                gvItems.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvItems.Rows.Add();
                    Application.DoEvents();
                    PBBar.Value += 1;
                    for (int j = 0; j < gvItems.Columns.Count - 1; j++)
                    {
                        gvItems.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                }
                gvItems.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvItems.Rows.Count > 0)
                {
                    gvItems.Focus();
                    gvItems.CurrentCell = gvItems[4, 0];
                }
                //PBBar.Value = 0;
                pnlPB.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((DataGridView1.CurrentRow.Cells[1].Value.ToString()) != "")
                {
                    tabControl1.SelectedTab = tabPage2;
                    ItNo = Convert.ToInt64(DataGridView1.CurrentRow.Cells[1].Value);
                    ItNm = Convert.ToString(DataGridView1.CurrentRow.Cells[2].Value);
                    label7.Font = ObjFunction.GetFont();
                    label7.Text = ItNm;
                    dsVd = ObjDset.FillDset("New", "Select * From GetItemClosingStockMonthly (" + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', " + ItNo + ", 0, 0 )", CommonFunctions.ConStr);

                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    for (int i = 2; i < 9; i++)
                    {
                        DataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    for (int i = 2; i < 9; i++)
                    {
                        DataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                    DataGridView2.Columns[0].Visible = false;
                    DataGridView2.Columns[4].Visible = false;
                    DataGridView2.Columns[6].Visible = false;
                    DataGridView2.Columns[8].Visible = false;
                    if (DataGridView2.Rows.Count >= 1)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                }
                else
                {
                    //tabControl1.SelectedTab = tabPage1;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }

        public void GetCount()
        {
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView1.Rows[i].Index != DataGridView1.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value = 0;


                    //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = 100;
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[6].Value);
                }
            }

            //===========Total At footer===========

            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font =ObjFunction.GetFont() ;
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = "Total";
        }

        public void GetCountVoucherDtls()
        {
            for (int i = 0; i < GridViewDaily.Rows.Count - 1; i = i + 1)
            {
                if (GridViewDaily.Rows[i].Index != GridViewDaily.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = 0;

                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = 0;

                    //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[0].Value = 100;
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value);
                }

            }

            //===========Total At footer===========

            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[3].Value = "Total";
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;

                for (int i = 0; i < gvItem.Rows.Count; i++)
                {
                    gvItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
                }
                BtnShow.Focus();
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    tabControl1.SelectedTab = tabPage3;
                    MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                    label8.Text = "(" + Convert.ToString(DataGridView2.CurrentRow.Cells[1].Value) + ")";
                    lblDatewise.Font = ObjFunction.GetFont();
                    lblDatewise.Text = ItNm;
                    dsVd = ObjDset.FillDset("New", "Exec GetItemClosingStockByDate  " + MNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', " + ItNo + "", CommonFunctions.ConStr);

                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;

                    GridViewDaily.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;


                    if (GridViewDaily.Rows.Count >= 1)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                    GetCountVoucherDtls();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                if (tabControl1.SelectedIndex == 0)
                {
                    ReportSession = new string[5];

                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    if (chkBarcode.Checked == true)
                    {
                        ReportSession[3] = BItemNo.ToString();
                    }
                    else
                        ReportSession[3] = strItemNo;
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[4] = "0";
                    }
                    else
                    { ReportSession[4] = "1"; }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNew.rpt", CommonFunctions.ReportPath), ReportSession);

                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    ReportSession = new string[9];

                    ReportSession[0] = "Stock Summary Details";
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    if (chkBarcode.Checked == true)
                    {
                        ReportSession[3] = BItemNo.ToString();
                    }
                    else
                        ReportSession[3] = strItemNo;
                    ReportSession[5] = "0";//Type
                    ReportSession[6] = "0";//No
                    ReportSession[7] = 1.ToString();
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[8] = 0.ToString();
                    }
                    else
                    {
                        ReportSession[8] = 1.ToString();
                    }

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummDtlsByMonthlyRType(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummDtlsByMonthlyRType.rpt", CommonFunctions.ReportPath), ReportSession);

                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    if (MfgCompNo == 0) ReportSession = new string[6];
                    else ReportSession = new string[7];

                    ReportSession[0] = MNo.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = ItNo.ToString();
                    ReportSession[5] = ItNm;
                    if (MfgCompNo == 0)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewGetItemClosingStockByDate(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGetItemClosingStockByDate.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else
                    {
                        ReportSession[6] = lblFirmName.Text.Replace("Firm Name :", "");
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewGetItemClosingStockByDateFirm(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewGetItemClosingStockByDateFirm.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (DataGridView1.Rows.Count > 0)
                    btnPrint.Visible = true;
                else
                    btnPrint.Visible = false;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (DataGridView2.Rows.Count > 0)
                    btnPrint.Visible = true;
                else
                    btnPrint.Visible = false;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                if (GridViewDaily.Rows.Count > 0)
                    btnPrint.Visible = true;
                else
                    btnPrint.Visible = false;
            }
        }

        private void DTPFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                DTToDate.Focus();
            }
        }

        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                BtnItmShow.Focus();
            }
        }

        public void GridNull()
        {
            //DataGridView1.DataSource = null;
            while (DataGridView1.Rows.Count > 0)
                DataGridView1.Rows.RemoveAt(0);
            //DataGridView2.DataSource = null;
            while (DataGridView2.Rows.Count > 0)
                DataGridView2.Rows.RemoveAt(0);
            //GridViewDaily.DataSource = null;
            while (GridViewDaily.Rows.Count > 0)
                GridViewDaily.Rows.RemoveAt(0);
            tabControl1.SelectedTab = tabPage1;
            btnPrint.Visible = false;
        }

        private void txtItemBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ItemBarcodeBindGrid();
            }
        }

        private void rbItemWise_CheckedChanged(object sender, EventArgs e)
        {
            BtnItmShow.Visible = true;
        }

        private void pnlMainForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                strCategoryNo = "";
                while (DataGridView1.Rows.Count > 0)
                    DataGridView1.Rows.RemoveAt(0);

                for (int i = 0; i < gvcategory.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvcategory.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strCategoryNo == "")
                            strCategoryNo = gvcategory.Rows[i].Cells[0].Value.ToString();
                        else
                            strCategoryNo = strCategoryNo + "," + gvcategory.Rows[i].Cells[0].Value.ToString();
                    }
                }

                pnlItem.Visible =true;
                pnlDepartment.Visible = false;
                pnlcategory.Visible = false;
                panel1.Visible = true;
                panel2.Visible = false;
                btnPrint.Visible = false;
                BindGridItem();                            
                tabControl1.Visible = false;
                btnBarShow.Visible = false;
                if (gvItem.Rows.Count > 0)
                {
                    gvItem.Focus();
                    gvItem.CurrentCell = gvItem[2, 0];
                }
                chkSelectAll.Checked = false;



            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void chkSelectAllCatogery_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvcategory.Rows.Count; i++)
            {
                gvcategory.Rows[i].Cells[2].Value = chkSelectAllCatogery.Checked;
            }
        }

              
        private void chkSelectAllDepartment_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvDepartment.Rows.Count; i++)
            {
                gvDepartment.Rows[i].Cells[2].Value = chkSelectAllDepartment.Checked;
            }
        }

        private void btnNextDept_Click(object sender, EventArgs e)
        {
            
            try
            {
                string[] ReportSession;
                Form NewF = null;
                strDepartmentNo = "";
                while (DataGridView1.Rows.Count > 0)
                    DataGridView1.Rows.RemoveAt(0);

                for (int i = 0; i < gvDepartment.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvDepartment.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strDepartmentNo == "")
                            strDepartmentNo = gvDepartment.Rows[i].Cells[0].Value.ToString();
                        else
                            strDepartmentNo = strDepartmentNo + "," + gvDepartment.Rows[i].Cells[0].Value.ToString();
                    }
                }


                pnlItem.Visible = true;
               chkSelectAllItem.Checked = false;
                panel1.Visible = false;
                panel2.Visible = false;
                //pnlcategory.Visible = true;

                btnPrint.Visible = false;
                pnlDepartment.Visible = false;
                //BindGridcat();
                BindGridItem();
                tabControl1.Visible = false;
               
                btnBarShow.Visible = false;
                if (gvItem.Rows.Count > 0)
                {
                    gvItem.Focus();
                    gvItem.CurrentCell = gvItem[2, 0];
                }
                chkSelectAll.Checked = false;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkSelectAllItem_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvItems.Rows.Count; i++)
            {
                gvItems.Rows[i].Cells[4].Value = chkSelectAllItem.Checked;
            }
        }

        private void btnResultShow_Click(object sender, EventArgs e)
        {

            FinalItem();
            
        }

        public void FinalItem()
        {
            string[] ReportSession;
            Form NewF = null;
            strDepartmentNo = "";
            while (DataGridView1.Rows.Count > 0)
                DataGridView1.Rows.RemoveAt(0);

            for (int i = 0; i < gvItems.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvItems.Rows[i].Cells[4].FormattedValue) == true)
                {
                    if (strDepartmentNo == "")
                        strDepartmentNo = gvItems.Rows[i].Cells[0].Value.ToString();
                    else
                        strDepartmentNo = strDepartmentNo + "," + gvItems.Rows[i].Cells[0].Value.ToString();
                }
            }
            if (strDepartmentNo != "")
            {
                if (rbNone.Checked == true)
                {
                    panel1.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = tabControl1;
                    tabControl1.SelectedTab = tabPage1;
                    //ShowFlage = true;
                    if (DBGetVal.KachhaFirm == false)
                    {
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + strItemNo + "',0", CommonFunctions.ConStr);

                    }
                    else
                    {
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + strItemNo + "',1", CommonFunctions.ConStr);

                    }
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    GetCount();
                    if (DataGridView1.Rows.Count > 1)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;

                }
                else if (rbItemWise.Checked == true)
                {
                    ReportSession = new string[5];


                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    if (chkBarcode.Checked == true)
                    {
                        ReportSession[3] = BItemNo.ToString();
                    }
                    else
                        ReportSession[3] = strDepartmentNo;
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[4] = "0";
                    }
                    else
                    { ReportSession[4] = "1"; }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNewCategory(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNewCategory.rpt", CommonFunctions.ReportPath), ReportSession);


                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else if (rbItemSummaryDtls.Checked == true)
                {
                    ReportSession = new string[9];

                    ReportSession[0] = "Stock Summary Details";
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = strDepartmentNo;
                    ReportSession[5] = "0";//Type
                    ReportSession[6] = "0";//No
                    ReportSession[7] = 1.ToString();
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[8] = 0.ToString();
                    }
                    else
                    {
                        ReportSession[8] = 1.ToString();
                    }

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNewCategory(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNewCategory.rpt", CommonFunctions.ReportPath), ReportSession);


                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rbItemDailyDetails.Checked == true)
                {
                    ReportSession = new string[6];

                    ReportSession[0] = MNo.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = strDepartmentNo;


                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNewCategory(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNewCategory.rpt", CommonFunctions.ReportPath), ReportSession);

                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rbGroupWise.Checked == true)
                {

                    ReportSession = new string[5];

                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    if (chkBarcode.Checked == true)
                    {
                        ReportSession[3] = BItemNo.ToString();
                    }
                    else
                        ReportSession[3] = strCategoryNo;
                    if (DBGetVal.KachhaFirm == false)
                    {
                        ReportSession[4] = "0";
                    }
                    else
                    { ReportSession[4] = "1"; }
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNewCategory(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNewCategory.rpt", CommonFunctions.ReportPath), ReportSession);


                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
            }
            else
                OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);



            if (DataGridView1.Rows.Count >= 1)
            {
                btnPrint.Visible = true;
                DataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else btnPrint.Visible = false;
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {

        }

       
        private void btnCancelItem_Click(object sender, EventArgs e)
        {
            pnlItem.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btnCancelDepartment_Click(object sender, EventArgs e)
        {
            pnlDepartment.Visible = false;
        }

        private void btnCancelCategory_Click(object sender, EventArgs e)
        {
            pnlcategory.Visible = false;
        }

        private void chkBarcode_CheckedChanged(object sender, EventArgs e)
        {
            GridNull();

            if (chkBarcode.Checked == true)
            {
                panel2.Visible = true;
                BtnItmShow.Visible = false;
                txtBarCode.Focus();
                panel1.Visible = false;
                tabControl1.Visible = false;
                btnBarShow.Visible = true;


            }
            else
            {
                panel2.Visible = false;
                txtBarCode.Text = "";
                BtnItmShow.Visible = true;
                btnBarShow.Visible = false;


            }
        }

        private void BtnItmShow_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.S_ItemNameType)) == 2 )
            {
                if (rbGroupWise.Checked == true)
                {
                    string str = "";
                    DataTable dt = new DataTable();
                    panel1.Visible = true;
                    panel2.Visible = false;
                    tabControl1.Visible = false;
                    btnBarShow.Visible = false;


                    str = " SELECT DISTINCT  MItemGroup.ItemGroupNo,   (SELECT  MItemGroup.ItemGroupName) AS ItemName,'false' AS chk " +
                          " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo " +
                          " inner join MItemGroup on MItemGroup.ItemGroupNo = MItemMaster.groupno " +
                          " Where mItemMaster.IsActive = 'true'  ORDER BY ItemName ";



                    gvItem.Rows.Clear();
                    dt = ObjFunction.GetDataView(str).Table;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvItem.Rows.Add();
                        Application.DoEvents();
                        // PBBar.Value += 1;
                        for (int j = 0; j < gvItem.Columns.Count; j++)
                            gvItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                    }
                    if (gvItem.Rows.Count > 0)
                    {
                        gvItem.Focus();
                        gvItem.CurrentCell = gvItem[2, 0];
                    }
                    chkSelectAll.Checked = false;
                }
                else
                {
                    btnPrint.Visible = false;
                    BindGrid();
                    //panel1.Visible = false;
                    panel1.Visible = true;
                    panel2.Visible = false;
                    tabControl1.Visible = false;
                    btnBarShow.Visible = false;
                    if (gvItem.Rows.Count > 0)
                    {
                        gvItem.Focus();
                        gvItem.CurrentCell = gvItem[2, 0];
                    }
                    chkSelectAll.Checked = false;
                }

            }  // AppSettings.S_ItemNameType
            else

            { //= new code added for 3 list boxes 

                if (rbGroupWise.Checked == true)
                {
                    string str = "";
                    DataTable dt = new DataTable();
                    panel1.Visible = true;
                    panel2.Visible = false;
                    tabControl1.Visible = false;
                    btnBarShow.Visible = false;


                    str = " SELECT DISTINCT  MItemGroup.ItemGroupNo,   (SELECT  MItemGroup.ItemGroupName) AS ItemName,'false' AS chk " +
                          " FROM TStock INNER JOIN MItemMaster ON TStock.ItemNo = mItemMaster.ItemNo " +
                          " inner join MItemGroup on MItemGroup.ItemGroupNo = MItemMaster.groupno " +
                          " Where mItemMaster.IsActive = 'true'  ORDER BY ItemName ";



                    gvItem.Rows.Clear();
                    dt = ObjFunction.GetDataView(str).Table;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        gvItem.Rows.Add();
                        Application.DoEvents();
                        // PBBar.Value += 1;
                        for (int j = 0; j < gvItem.Columns.Count; j++)
                            gvItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                    }
                    if (gvItem.Rows.Count > 0)
                    {
                        gvItem.Focus();
                        gvItem.CurrentCell = gvItem[2, 0];
                    }
                    chkSelectAll.Checked = false;
                }
                else
                {
                    panel1.Visible = false;
                    panel2.Visible = false;
                    btnPrint.Visible = false;
                   // pnlcategory.Visible = false;
                   // pnlItem.Visible = false;
                    pnlDepartment.Visible = true;
                    pnlItem.Visible = false;
                    chkSelectAllDepartment.Checked = false;
                    BindGridDept();                   
                                
                    tabControl1.Visible = false;
                    btnBarShow.Visible = false;
                    if (gvDepartment.Rows.Count > 0)
                    {
                        gvDepartment.Focus();
                        gvDepartment.CurrentCell = gvDepartment[2, 0];
                    }
                    chkSelectAll.Checked = false;
                }
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvItem.Rows.Count; i++)
            {
                gvItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
            }
        }

        private void btnBarShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBarCode.Text != "")
                {

                    btnPrint.Visible = false;
                    tabControl1.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = tabControl1;
                    tabControl1.SelectedTab = tabPage1;
                    if (DataGridView1.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;

                    txtBarCode.Text = txtBarCode.Text.Replace("\r", "").Replace("\n", "");

                    BItemNo = ObjQry.ReturnLong("SELECT ItemNo FROM MItemMaster WHERE (Barcode = '" + txtBarCode.Text + "') ", CommonFunctions.ConStr);
                    if (DBGetVal.KachhaFirm == false)
                    {
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + BItemNo.ToString() + "',0", CommonFunctions.ConStr);

                    }
                    else
                    {
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemQty " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', '" + BItemNo.ToString() + "',1", CommonFunctions.ConStr);
                    }
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                    GetCount();
                }
                else
                    OMMessageBox.Show("Enter Barcode", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                if (DataGridView1.Rows.Count >= 1)
                {
                    btnPrint.Visible = true;
                    DataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else btnPrint.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex == 1)
            //{
            //    if (e.Value == null)
            //    {
            //      e.Value= Color.PeachPuff;
            //    }
            //}
        }

        //private void rbType_Click(object sender, EventArgs e)
        //{
        //    btnPrint.Visible = false;
        //    tabControl1.Visible = false;
        //    panel1.Visible = false;
        //}

        //private void rb_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        e.SuppressKeyPress = true;
        //        BtnItmShow.Focus();
        //    }
        //}

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnBarShow.Focus();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
