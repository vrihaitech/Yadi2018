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
    public partial class UserWiseSalesDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();

        DBMItemMaster dbmItemMaster = new DBMItemMaster();
        MItemMaster mItemMaster = new MItemMaster();
        DBProgressBar PB;

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo, VchCode = 0;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "";
        long RateTypeNo = 0;

        public UserWiseSalesDetails()
        {
            InitializeComponent();
        }
        public UserWiseSalesDetails(long vchno)
        {
            VchCode = vchno;
            InitializeComponent();
            if (VchCode == VchType.Sales)
                this.Text = "UserWiseSalesDetails";
            else if (VchCode == VchType.Purchase)
                this.Text = "UserWisePurchaseDetails";
        }

        private void UserWiseSalesDetails_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            KeyDownFormat(this.Controls);
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            try
            {
                strItemNo = "";
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
                if (strItemNo != "")
                {
                    // GetString();
                    if (rdDetails.Checked == true || rdSummary.Checked == true)
                    {
                        string[] ReportSession;
                        ReportSession = new string[5];
                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = "true";// (IsSuperMode() == true) ? "true" : "false";
                        ReportSession[3] = strItemNo;
                        ReportSession[4] = VchCode.ToString();
                        Form NewF = null;
                        if (rdDetails.Checked == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RptUserWiseSalesReport(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptUserWiseSalesReport.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RptUserWiseSalesSumm(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptUserWiseSalesSumm.rpt", CommonFunctions.ReportPath), ReportSession);

                        }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else
                    {
                        string[] ReportSession;
                        ReportSession = new string[5];
                        ReportSession[0] = VchCode.ToString();
                        ReportSession[1] = DBGetVal.FirmNo.ToString();
                        ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[4] = strItemNo;

                        Form NewF = null;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RPTUserWiseDayWiseDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTUserWiseDayWiseDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                    OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

            this.Cursor = Cursors.Default;
        }

        public void BindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                DataTable dtItem = new DataTable();
                DataView dv = new DataView();
                DataView dvItem = new DataView();


                //string str = " SELECT DISTINCT mItemMaster.ItemNo,(Select ItemName From MStockItems_V(null,MStockItems.ItemNo,null,null,null,null,null) ) AS ItemName, 'false' as chk FROM " + //dbo.MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) as 
                //               "  MStockItems INNER JOIN " +
                //               " TStock INNER JOIN TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo ON " +
                //               " mItemMaster.ItemNo = TStock.ItemNo where TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' AND TVoucherEntry.VoucherTypeCode=" + VchCode + " ";
                string str = " SELECT DISTINCT mItemMaster.ItemNo,MStockGroup.StockGroupName  + mItemMaster.ItemName AS ItemName, 'false' as chk FROM " + //dbo.MStockItems_V(NULL,NULL,NULL,NULL,NULL,NULL,NULL) as 
                             "  MStockItems INNER JOIN " +
                             " TStock INNER JOIN TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo ON " +
                             " mItemMaster.ItemNo = TStock.ItemNo INNER JOIN MStockGroup ON mItemMaster.GroupNo = MStockGroup.StockGroupNo  where TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' AND TVoucherEntry.VoucherTypeCode in (" + VchCode + "," + ((VchCode == VchType.Sales) ? 12 : 13) + ") order by MStockGroup.StockGroupName  + mItemMaster.ItemName ";

                dt = ObjFunction.GetDataView(str).Table;
                dv = ObjFunction.GetDataView(str);

                //dtItem = (dbStockItem.GetMStockItems_V(1, 0, 0, 0, 0, 0, 0)).Table;

                //DataRelation dr = new DataRelation("",dt.Columns[0],dtItem.Columns[0] );
                gvItem.Rows.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvItem.Rows.Add();
                    for (int j = 0; j < gvItem.Columns.Count; j++)
                        gvItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                }
                gvItem.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvItem.Rows.Count > 0)
                {
                    gvItem.Focus();
                    gvItem.CurrentCell = gvItem[2, 0];
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
                ReportSession = new string[5];
                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = "true";// (IsSuperMode() == true) ? "true" : "false";
                ReportSession[4] = strItemNo;
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewStockSummaryNew(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockSummaryNew.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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

        private void BtnItmShow_Click(object sender, EventArgs e)
        {
            pnlItemDetails.Visible = false;
            PB = new DBProgressBar(this);
            PB.TimerStart();
            PB.Ctrl = pnlItemDetails;
            BindGrid();
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                BtnExport.Visible = true;
            else
                BtnExport.Visible = false;
            //pnlItemDetails.Visible = true;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvItem.Rows.Count; i++)
            {
                gvItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
            }
        }
        #region KeyDown Events
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
            //else if (e.KeyCode == Keys.O && e.Control)
            //{
            //    if (DBGetVal.IsAdmin == true)
            //    {
            //        txtRateTypePassword.Enabled = true;
            //        pnlRateType.Visible = true;
            //        txtRateTypePassword.Text = "";
            //        txtRateTypePassword.Focus();
            //    }
            //}
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else
                    KeyDownFormat(ctrl.Controls);
            }
        }
        #endregion
        private void gvItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BtnShow.Focus();
            }
        }

        private void txtRateTypePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnRateTypeOK_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pnlRateType.Visible = false;
                BtnShow.Focus();
            }
        }


        private void btnRateTypeOK_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string[,] arr = new string[5, 2];
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)) == true)
            {
                arr[0, 0] = ObjFunction.GetAppSettings(AppSettings.ARatePassword);
                arr[0, 1] = "ASaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)) == true)
            {
                arr[1, 0] = ObjFunction.GetAppSettings(AppSettings.BRatePassword);
                arr[1, 1] = "BSaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive)) == true)
            {
                arr[2, 0] = ObjFunction.GetAppSettings(AppSettings.CRatePassword);
                arr[2, 1] = "CSaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive)) == true)
            {
                arr[3, 0] = ObjFunction.GetAppSettings(AppSettings.DRatePassword);
                arr[3, 1] = "DSaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive)) == true)
            {
                arr[4, 0] = ObjFunction.GetAppSettings(AppSettings.ERatePassword);
                arr[4, 1] = "ESaleRate";
            }

            for (int i = 0; i < 5; i++)
            {
                if (arr[i, 0] != null)
                {
                    if (txtRateTypePassword.Text == arr[i, 0].ToString())
                    {

                        RateTypeNo = i + 1;

                        flag = true;
                        break;
                    }
                }
            }
            if (flag == false)
            {
                OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtRateTypePassword.Focus();
            }
            else
            {
                pnlRateType.Visible = false;
            }
        }

        private void btnRateTypeCancel_Click(object sender, EventArgs e)
        {
            txtRateTypePassword.Text = "";
            pnlRateType.Visible = false;
        }

        public string GetString(string val)
        {
            string strReturn = "";
            try
            {
                bool flag = false;
                string[] str = new string[1];
                str[0] = ",";
                string[] strLine = val.Split(str, StringSplitOptions.None);
                //string[] strLine = val.Split(",", StringSplitOptions.None);

                try
                {
                    if (strLine.Length < 2 || strLine.Length > 2)
                        flag = false;
                    else if (strLine.Length == 2)
                    {
                        string FromDate = strLine[0].ToString();
                        string ToDate = strLine[1].ToString();
                        if (ObjFunction.CheckValidDate(FromDate) == true && ObjFunction.CheckValidDate(ToDate) == true)
                        {
                            DataTable dt = new DataTable();
                            string strQuery = "";
                            string strQry = " SELECT DISTINCT mItemMaster.ItemNo FROM MStockItems INNER JOIN " +
                                            " TStock INNER JOIN TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo ON " +
                                            " mItemMaster.ItemNo = TStock.ItemNo where TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + "' and TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + "' AND TVoucherEntry.VoucherTypeCode=" + 15 + "";
                            DataTable dtable = new DataTable();
                            dtable = ObjFunction.GetDataView(strQry).Table;
                            if (dtable.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtable.Rows.Count; i++)
                                {
                                    if (strQuery == "")
                                        strQuery = dtable.Rows[i].ItemArray[0].ToString();
                                    else
                                        strQuery = strQuery + "," + dtable.Rows[i].ItemArray[0].ToString();
                                }
                                dt = ObjFunction.GetDataView(" Exec GetUserwiseSalesDetails '" + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + "', false,'" + strQuery + "'," + VchCode + "").Table;
                                if (dt.Rows.Count > 0)
                                {
                                    flag = true;
                                    for (int i = 0; i < dt.Columns.Count; i++)
                                    {
                                        if (i == 0)
                                        {
                                            strReturn = dt.Columns[i].ColumnName;
                                        }
                                        else
                                        {
                                            strReturn = strReturn + "," + dt.Columns[i].ColumnName;
                                        }
                                    }
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        strReturn = strReturn + Environment.NewLine;
                                        for (int j = 0; j < dt.Columns.Count; j++)
                                        {
                                            if (j == 0)
                                            {
                                                strReturn = strReturn + dt.Rows[i].ItemArray[j].ToString();
                                            }
                                            else
                                            {
                                                strReturn = strReturn + "," + dt.Rows[i].ItemArray[j];
                                            }
                                        }
                                    }
                                }
                                else
                                    flag = false;
                            }
                            else
                                flag = false;
                        }
                        else
                            flag = false;
                    }
                }
                catch (Exception e)
                {
                    flag = false;
                    CommonFunctions.ErrorMessge = e.Message;
                }
                if (flag == false)
                    strReturn += "Fail";
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

            return strReturn;
        }

        public bool IsSuperMode()
        {
            bool flag = false;
            long RTNo = RateTypeNo;
            if (RTNo == 1)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateSuperMode));
            }
            else if (RTNo == 2)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateSuperMode));
            }
            else if (RTNo == 3)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateSuperMode));
            }
            else if (RTNo == 4)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateSuperMode));
            }
            else if (RTNo == 5)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateSuperMode));
            }
            return flag;
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string str = "", ReportName = ""; DataTable dt = new DataTable();
            bool flag = true;// (IsSuperMode() == true) ? true : false;
            strItemNo = "";
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
            if (rdDetails.Checked == true)
            {
                if (VchCode == VchType.Sales)
                    ReportName = "UserWise Sales Details";
                else
                    ReportName = "UserWise Purchase Details";
            }
            else if (rdSummary.Checked == true)
            {
                if (VchCode == VchType.Sales)
                    ReportName = "UserWise Sales Summary";
                else
                    ReportName = "UserWise Purchse Summary";
            }
            else if (rdDayWiseSummary.Checked == true)
            {
                if (VchCode == VchType.Sales)
                    ReportName = "UserWise Daywise Sales Details";
                else
                    ReportName = "UserWise Daywise Purchse Details";
            }
            if (strItemNo != "")
            {
                if (rdDetails.Checked == true || rdSummary.Checked == true)
                {
                    try
                    {
                        str = " GetUserwiseSalesDetails '" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + flag + ",'" + strItemNo + "'," + VchCode + "";
                        int FQty = 0, TQty = 0, Rate = 0, Disc = 0, Amount = 0;
                        dt = ObjFunction.GetDataView(str).Table;

                        if (VchCode == VchType.Purchase)
                            dt.Columns.RemoveAt(1);
                        else
                            dt.Columns.RemoveAt(2);

                        dt.Columns.RemoveAt(10); //dt.Columns.RemoveAt(8);
                        //int dtCount = dt.Columns.Count;
                        int dtCount = 0;
                        if (VchCode == VchType.Purchase)
                        {
                            if (rdDetails.Checked == true)
                                dtCount = dt.Columns.Count - 2;
                            else
                                dtCount = dt.Columns.Count - 4;
                        }
                        else
                        {
                            if (rdDetails.Checked == true)
                                dtCount = dt.Columns.Count - 4;
                            else
                                dtCount = dt.Columns.Count - 6;
                        }
                        string[] strCol = new string[dtCount];
                        strCol[0] = "";
                        strCol[1] = "";
                        strCol[2] = "Date";
                        if (VchCode == VchType.Purchase)
                            strCol[3] = "Supplier";
                        else
                            strCol[3] = "Time";
                        strCol[4] = "BillNo";
                        strCol[5] = "Qty";
                        //strCol[6] = "Rate";
                        //strCol[7] = "Amount";
                        if (VchCode == VchType.Purchase)
                        {
                            strCol[6] = "FreeQty";
                            strCol[7] = "TotalQty";
                            if (rdDetails.Checked == true)
                            {
                                strCol[8] = "Rate";
                                strCol[9] = "L.Rate";
                                strCol[10] = "Amount";
                                Rate = 9; Disc = 10; Amount = 11;
                            }
                            else
                            {
                                strCol[8] = "Amount";
                                Amount = 9;
                            }
                            FQty = 7; TQty = 8;
                        }
                        else
                        {
                            if (rdDetails.Checked == true)
                            {
                                strCol[6] = "Rate";
                                strCol[7] = "Disc.";
                                strCol[8] = "Amount";
                                Rate = 7; Disc = 8; Amount = 9;
                            }
                            else
                            {
                                strCol[6] = "Amount";
                                Amount = 7;
                            }
                        }


                        string GrpName = "", ItemName = "";

                        int col = 1; int Temp = 0, ItemRowNo = 0, GrpRowNo = 0, ExtraRow = 0; double TotalQty = 0, TotalDisc = 0, TotalAmt = 0, TotQty = 0, TotDisc = 0, TotAmt = 0, GrandTotQty = 0, GrandTotalDisc = 0, GrandTotAmt = 0, TotalFreeQty = 0, TotalTotalQty = 0, TotFreeQty = 0, TotTotalQty = 0, GrandFreeQty = 0, GrandTotalQty = 0, VchTotQty = 0, VchTotAmt = 0,TotAmtGrand=0;
                        //int col = 1; int Temp = 0, ItemRowNo = 0, GrpRowNo = 0, ExtraRow = 0; double TotalQty = 0, TotalAmt = 0, TotQty = 0, TotAmt = 0, GrandTotQty = 0, GrandTotAmt = 0;
                        CreateExcel excel = new CreateExcel();
                        //Company Name Header
                        excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                        col++;
                        //Company Address Header
                        excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                        col++;
                        //Report Name And Dates
                        excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                        col++;
                        excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                        col++;
                        excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                        col++;

                        for (int i = 0; i < dtCount; i++)
                        {
                            if (rdDetails.Checked == true)
                            {
                                if (i == 0)
                                    excel.createHeaders(col, i + 1, strCol[i + 1].ToString(), excel.ColName(col, i + 1), excel.ColName(col, i + 2), 2, Color.Gainsboro, true, 5, Color.Black, 12, CreateExcel.ExAlign.Center);
                                else if (i == 2 || i == 3)
                                    excel.createHeaders(col, i + 1, strCol[i].ToString(), excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                else if (i != 1)
                                    excel.createHeaders(col, i + 1, strCol[i].ToString(), excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                            else if (rdSummary.Checked == true)
                            {
                                if (i == 0)
                                    excel.createHeaders(col, i + 1, strCol[i + 1].ToString(), excel.ColName(col, i + 1), excel.ColName(col, ColIndex.BillNo), ColIndex.BillNo, Color.Gainsboro, true, 5, Color.Black, 12, CreateExcel.ExAlign.Center);
                                else if (i != 1 && i != 2 && i != 3 && i != 4)
                                    excel.createHeaders(col, i + 1, strCol[i].ToString(), excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                        }
                        int vchName = 0;
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (vchName != Convert.ToInt16(dt.Rows[j].ItemArray[12]) && vchName != 0)
                            {
                                
                                {
                                    if (Convert.ToInt16(dt.Rows[j - 1].ItemArray[12]) == 1)
                                    {
                                        if (VchCode == VchType.Sales)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Sales", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        else if (VchCode == VchType.Purchase)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Purchase", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 1 + Temp - ExtraRow, ColIndex.Qty, Math.Abs(VchTotQty).ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Qty), ColIndex.Qty, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 1 + Temp - ExtraRow, Amount, Math.Abs(VchTotAmt).ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, Amount), excel.ColName(j + col + 1 + Temp - ExtraRow, Amount), Amount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        VchTotQty = 0; VchTotAmt = 0;
                                        Temp++;
                                    }
                                    if (Convert.ToInt16(dt.Rows[j].ItemArray[12]) == 2)
                                    {
                                        if (VchCode == VchType.Sales)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Sales Return", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        else if (VchCode == VchType.Purchase)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Purchase Return", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 1 + Temp - ExtraRow, ColIndex.Qty, "", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Qty), ColIndex.Qty, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 1 + Temp - ExtraRow, Amount, "", excel.ColName(j + col + 1 + Temp - ExtraRow, Amount), excel.ColName(j + col + 1 + Temp - ExtraRow, Amount), Amount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        GrpName = "";
                                        ItemName = "";
                                    }
                                        if (rdDetails.Checked == true)
                                        {
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, ColIndex.Disc, "", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Disc), ColIndex.Disc, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 1 + Temp - ExtraRow, Rate), excel.ColName(j + col + 1 + Temp - ExtraRow, Rate), Rate, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        }
                                    Temp++;
                                }
                                vchName = Convert.ToInt16(dt.Rows[j].ItemArray[12]);
                            }
                            if (vchName != Convert.ToInt16(dt.Rows[j].ItemArray[12]))
                            {
                                
                                {
                                    if (Convert.ToInt16(dt.Rows[j].ItemArray[12]) == 1)
                                    {
                                        if (VchCode == VchType.Sales)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Sales", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        else if (VchCode == VchType.Purchase)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Purchase", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    }
                                    else
                                    {
                                        if (VchCode == VchType.Sales)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Sales Return", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        else if (VchCode == VchType.Purchase)
                                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "Purchase Return", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    }
                                    if (rdDetails.Checked == true)
                                    {
                                        excel.createHeaders(j + col + 1 + Temp - ExtraRow, ColIndex.Disc, "", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Disc), ColIndex.Disc, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 1 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 1 + Temp - ExtraRow, Rate), excel.ColName(j + col + 1 + Temp - ExtraRow, Rate), Rate, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                    }
                                    excel.createHeaders(j + col + 1 + Temp - ExtraRow, ColIndex.Qty, "", excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.Qty), ColIndex.Qty, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(j + col + 1 + Temp - ExtraRow, Amount, "", excel.ColName(j + col + 1 + Temp - ExtraRow, Amount), excel.ColName(j + col + 1 + Temp - ExtraRow, Amount), Amount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    Temp++;
                                }
                                vchName = Convert.ToInt16(dt.Rows[j].ItemArray[12]);
                            }
                           
                            if (GrpName != dt.Rows[j].ItemArray[10].ToString())
                            {
                                //excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, "", false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                GrpName = dt.Rows[j].ItemArray[10].ToString(); GrpRowNo = j + col + 1 + Temp - ExtraRow; Temp++;
                                ItemName = "";
                            }
                            else
                            {
                                if (rdSummary.Checked == true)
                                    ExtraRow++;

                            }

                            if (ItemName != dt.Rows[j].ItemArray[2].ToString())
                            {
                                excel.createHeaders(j + col + 1 + Temp - ExtraRow, 2, "           " + dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                ItemName = dt.Rows[j].ItemArray[2].ToString(); ItemRowNo = j + col + 1 + Temp - ExtraRow;
                                if (rdSummary.Checked == true)
                                    ExtraRow++;
                                //if (rdDetails.Checked == true)
                                Temp++;
                            }



                            if (rdDetails.Checked == true)
                            {
                                excel.addData(j + col + 1 + Temp, ColIndex.Date, dt.Rows[j].ItemArray[0].ToString(), excel.ColName(j + col + 1, ColIndex.Date), excel.ColName(j + col + 1, ColIndex.Date), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                if (VchCode == VchType.Purchase)
                                    excel.addData(j + col + 1 + Temp, ColIndex.Time, dt.Rows[j].ItemArray[1].ToString(), excel.ColName(j + col + 1, ColIndex.Time), excel.ColName(j + col + 1, ColIndex.Time), Format.NoFloating, 0, CreateExcel.ExAlign.Left, false);
                                else
                                    excel.addData(j + col + 1 + Temp, ColIndex.Time, Convert.ToDateTime(dt.Rows[j].ItemArray[1]).ToShortTimeString(), excel.ColName(j + col + 1, ColIndex.Time), excel.ColName(j + col + 1, ColIndex.Time), Format.HHMMSS, 0, CreateExcel.ExAlign.Left, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.BillNo, dt.Rows[j].ItemArray[3].ToString(), excel.ColName(j + col + 1, ColIndex.BillNo), excel.ColName(j + col + 1, ColIndex.BillNo), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.Qty, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[4])).ToString(), excel.ColName(j + col + 1, ColIndex.Qty), excel.ColName(j + col + 1, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                if (VchCode == VchType.Purchase)
                                {
                                    excel.addData(j + col + 1 + Temp, FQty, dt.Rows[j].ItemArray[5].ToString(), excel.ColName(j + col + 1, FQty), excel.ColName(j + col + 1, FQty), "", 0, CreateExcel.ExAlign.Right, false);
                                    excel.addData(j + col + 1 + Temp, TQty, dt.Rows[j].ItemArray[6].ToString(), excel.ColName(j + col + 1, TQty), excel.ColName(j + col + 1, TQty), "", 0, CreateExcel.ExAlign.Right, false);
                                    excel.addData(j + col + 1 + Temp, 10, dt.Rows[j].ItemArray[8].ToString(), excel.ColName(j + col + 1, 10), excel.ColName(j + col + 1, 10), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);

                                }
                                excel.addData(j + col + 1 + Temp, Rate, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1, Rate), excel.ColName(j + col + 1, Rate), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(j + col + 1 + Temp, Disc, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[8])).ToString(), excel.ColName(j + col + 1, Disc), excel.ColName(j + col + 1, Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(j + col + 1 + Temp, Amount, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[9])).ToString(), excel.ColName(j + col + 1, Amount), excel.ColName(j + col + 1, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            }
                            TotalFreeQty = TotalFreeQty + Convert.ToDouble(dt.Rows[j].ItemArray[5]);
                            TotalTotalQty = TotalTotalQty + Convert.ToDouble(dt.Rows[j].ItemArray[6]);
                            TotalQty = TotalQty + Convert.ToDouble(dt.Rows[j].ItemArray[4]);
                            VchTotQty = VchTotQty + Convert.ToDouble(dt.Rows[j].ItemArray[4]);
                            TotalDisc = TotalDisc + Convert.ToDouble(dt.Rows[j].ItemArray[8]);
                            TotalAmt = TotalAmt + Convert.ToDouble(dt.Rows[j].ItemArray[9]);
                            VchTotAmt = VchTotAmt + Convert.ToDouble(dt.Rows[j].ItemArray[9]);
                            if (j < dt.Rows.Count - 1 || j == dt.Rows.Count - 1)
                            {
                                if (j < dt.Rows.Count - 1)
                                {
                                    if (ItemName != dt.Rows[j + 1].ItemArray[2].ToString() || GrpName != dt.Rows[j + 1].ItemArray[10].ToString())
                                    {

                                        if (rdDetails.Checked == true)
                                        {
                                            excel.createHeaders(j + col + 2 + Temp - ExtraRow, 2, "           "/* + dt.Rows[j].ItemArray[2].ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                            excel.addData(j + col + 2 + Temp - ExtraRow, Amount, Math.Abs(TotalAmt).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                            excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, Math.Abs(TotalQty).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                            excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, Math.Abs(TotalDisc).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                            if (VchCode == VchType.Purchase)
                                            {
                                                excel.addData(j + col + 2 + Temp - ExtraRow, FQty, TotalFreeQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                                excel.addData(j + col + 2 + Temp - ExtraRow, TQty, TotalTotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                                excel.addData(j + col + 2 + Temp - ExtraRow, 10, Math.Abs(TotalDisc).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, 10), excel.ColName(j + col + 2 + Temp - ExtraRow, 10), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                            }
                                            Temp++;
                                        }
                                        else
                                        {
                                            if (ExtraRow != 0)
                                                ExtraRow--;
                                        }

                                        excel.addData(ItemRowNo, ColIndex.Qty, Math.Abs(TotalQty).ToString(), excel.ColName(ItemRowNo, ColIndex.Qty), excel.ColName(ItemRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        if (rdDetails.Checked == true) excel.addData(ItemRowNo, ColIndex.Disc, Math.Abs(TotalDisc).ToString(), excel.ColName(ItemRowNo, ColIndex.Disc), excel.ColName(ItemRowNo, ColIndex.Disc), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        if (VchCode == VchType.Purchase)
                                        {
                                            excel.addData(ItemRowNo, FQty, TotalFreeQty.ToString(), excel.ColName(ItemRowNo, FQty), excel.ColName(ItemRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotFreeQty = TotFreeQty + TotalFreeQty; TotalFreeQty = 0;
                                            excel.addData(ItemRowNo, TQty, TotalTotalQty.ToString(), excel.ColName(ItemRowNo, TQty), excel.ColName(ItemRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotTotalQty = TotTotalQty + TotalTotalQty; TotalTotalQty = 0;
                                            if (rdDetails.Checked == true) excel.addData(ItemRowNo, 10, Math.Abs(TotalDisc).ToString(), excel.ColName(ItemRowNo, 10), excel.ColName(ItemRowNo, 10), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotDisc = TotDisc + TotalDisc; TotalDisc = 0;
                                        }
                                        if (rdDetails.Checked == true)
                                            excel.addData(ItemRowNo, Rate, "", excel.ColName(ItemRowNo, Rate), excel.ColName(ItemRowNo, Rate), "", 0, CreateExcel.ExAlign.Right, false); TotQty = TotQty + TotalQty; TotalQty = 0; TotDisc = TotDisc + TotalDisc; TotalDisc = 0;
                                        excel.addData(ItemRowNo, Amount, Math.Abs(TotalAmt).ToString(), excel.ColName(ItemRowNo, Amount), excel.ColName(ItemRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); TotAmtGrand = TotAmtGrand + TotalAmt; TotAmt = TotAmt + TotalAmt; TotalAmt = 0;

                                    }
                                    if (GrpName != dt.Rows[j + 1].ItemArray[10].ToString() || vchName != Convert.ToInt16(dt.Rows[j+1].ItemArray[12]))
                                    {
                                        if ((j + col + 2 + Temp - ExtraRow) - ((j + col + 2 + Temp - ExtraRow - 2)) > 1 && ExtraRow >= 0 && j != 0 && rdSummary.Checked == true)
                                            ExtraRow++;
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        if (rdDetails.Checked == true) excel.addData(j + col + 2 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), "", 0, CreateExcel.ExAlign.Right, true);

                                        excel.addData(j + col + 2 + Temp - ExtraRow, Amount, Math.Abs(TotAmt).ToString() /*TotAmt.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        //TotAmtGrand = TotAmtGrand + TotAmt;
                                        TotAmt = 0;
                                        if (rdDetails.Checked == true) excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, ""/*TotDisc.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, ""/*TotQty.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);


                                        Temp++;

                                        excel.addData(GrpRowNo, ColIndex.Qty, ""/*TotQty.ToString()*/
                                                                                                     , excel.ColName(GrpRowNo, ColIndex.Qty), excel.ColName(GrpRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotQty = GrandTotQty + TotQty;TotQty = 0;
                                        if (rdDetails.Checked == true) excel.addData(GrpRowNo, ColIndex.Disc, ""/*TotDisc.ToString()*/, excel.ColName(GrpRowNo, ColIndex.Disc), excel.ColName(GrpRowNo, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); //GrandTotDisc = GrandTotDisc + TotDisc; TotDisc = 0;
                                        //excel.addData(GrpRowNo, ColIndex.Amt, ""/*TotAmt.ToString()*/, excel.ColName(GrpRowNo, ColIndex.Amt), excel.ColName(GrpRowNo, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotAmt = GrandTotAmt + TotAmt; TotAmt = 0;

                                    }
                                    //if (GrpName != dt.Rows[j + 1].ItemArray[7].ToString())
                                    //{
                                    //    if ((j + col + 2 + Temp - ExtraRow) - ((j + col + 2 + Temp - ExtraRow - 2)) > 1 && ExtraRow >= 0 && j != 0 && rdSummary.Checked == true)
                                    //        ExtraRow++;
                                    //    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    //    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    //    if (rdDetails.Checked == true)
                                    //        excel.addData(j + col + 2 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), "", 0, CreateExcel.ExAlign.Right, true);
                                    //    excel.addData(j + col + 2 + Temp - ExtraRow, Amount, TotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    //    excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, TotQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    //    if (VchCode == VchType.Purchase)
                                    //    {
                                    //        excel.addData(j + col + 2 + Temp - ExtraRow, FQty, TotFreeQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    //        excel.addData(j + col + 2 + Temp - ExtraRow, TQty, TotTotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    //    }

                                    //    Temp++;

                                    //    excel.addData(GrpRowNo, ColIndex.Qty, TotQty.ToString(), excel.ColName(GrpRowNo, ColIndex.Qty), excel.ColName(GrpRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotQty = GrandTotQty + TotQty; TotQty = 0;
                                    //    if (VchCode == VchType.Purchase)
                                    //    {
                                    //        excel.addData(GrpRowNo, FQty, TotFreeQty.ToString(), excel.ColName(GrpRowNo, FQty), excel.ColName(GrpRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandFreeQty = GrandFreeQty + TotFreeQty; TotFreeQty = 0;
                                    //        excel.addData(GrpRowNo, TQty, TotTotalQty.ToString(), excel.ColName(GrpRowNo, TQty), excel.ColName(GrpRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotalQty = GrandTotalQty + TotTotalQty; TotTotalQty = 0;
                                    //    }
                                    //    excel.addData(GrpRowNo, Amount, TotAmt.ToString(), excel.ColName(GrpRowNo, Amount), excel.ColName(GrpRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotAmt = GrandTotAmt + TotAmt; TotAmt = 0;

                                    //}
                                }
                                else if (j == dt.Rows.Count - 1)
                                {
                                    if (rdDetails.Checked == true)
                                    {
                                        excel.createHeaders(j + col + 2 + Temp, 2, "           " /*+ dt.Rows[j].ItemArray[2].ToString()*/, excel.ColName(j + col + 2 + Temp, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.addData(j + col + 2 + Temp, ColIndex.Qty, Math.Abs(TotalQty).ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.Qty), excel.ColName(j + col + 2 + Temp, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp, ColIndex.Disc, Math.Abs(TotalDisc).ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.Disc), excel.ColName(j + col + 2 + Temp, ColIndex.Disc), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        if (VchCode == VchType.Purchase)
                                        {
                                            excel.addData(j + col + 2 + Temp, FQty, TotalFreeQty.ToString(), excel.ColName(j + col + 2 + Temp, FQty), excel.ColName(j + col + 2 + Temp, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                            excel.addData(j + col + 2 + Temp, TQty, TotalTotalQty.ToString(), excel.ColName(j + col + 2 + Temp, TQty), excel.ColName(j + col + 2 + Temp, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                            excel.addData(j + col + 2 + Temp, 10, Math.Abs(TotalDisc).ToString(), excel.ColName(j + col + 2 + Temp, 10), excel.ColName(j + col + 2 + Temp, 10), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        }
                                        excel.addData(j + col + 2 + Temp, Amount, Math.Abs(TotalAmt).ToString(), excel.ColName(j + col + 2 + Temp, Amount), excel.ColName(j + col + 2 + Temp, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        Temp++;
                                    }
                                    excel.addData(ItemRowNo, ColIndex.Qty, Math.Abs(TotalQty).ToString(), excel.ColName(ItemRowNo, ColIndex.Qty), excel.ColName(ItemRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotQty = TotQty + TotalQty; TotalQty = 0;
                                    if (rdDetails.Checked == true) excel.addData(ItemRowNo, ColIndex.Disc, Math.Abs(TotalDisc).ToString(), excel.ColName(ItemRowNo, ColIndex.Disc), excel.ColName(ItemRowNo, ColIndex.Disc), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotDisc = TotDisc + TotalDisc; TotalDisc = 0;
                                    if (VchCode == VchType.Purchase)
                                    {
                                        excel.addData(ItemRowNo, FQty, TotalFreeQty.ToString(), excel.ColName(ItemRowNo, FQty), excel.ColName(ItemRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotFreeQty = TotFreeQty + TotalFreeQty; TotalFreeQty = 0;
                                        excel.addData(ItemRowNo, TQty, Math.Abs(TotalTotalQty).ToString(), excel.ColName(ItemRowNo, TQty), excel.ColName(ItemRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotTotalQty = TotTotalQty + TotalTotalQty; TotalTotalQty = 0;
                                        if (rdDetails.Checked == true) excel.addData(ItemRowNo, 10, Math.Abs(TotalDisc).ToString(), excel.ColName(ItemRowNo, 10), excel.ColName(ItemRowNo, 10), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotDisc = TotDisc + TotalDisc; TotalDisc = 0;
                                    }
                                    if (rdDetails.Checked == true)
                                        excel.addData(ItemRowNo, Rate, "", excel.ColName(ItemRowNo, Rate), excel.ColName(ItemRowNo, Rate), "", 0, CreateExcel.ExAlign.Right, false);
                                    excel.addData(ItemRowNo, Amount, Math.Abs(TotalAmt).ToString(), excel.ColName(ItemRowNo, Amount), excel.ColName(ItemRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
              //                     if (VchCode == VchType.Purchase) { TotAmt = TotAmt + TotalAmt; TotalAmt = 0; }


                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, GrpName, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, "", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    if (rdDetails.Checked == true) excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, "", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    if (VchCode == VchType.Purchase)
                                    {
                                        excel.addData(j + col + 2 + Temp - ExtraRow, FQty, "", excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, TQty, "", excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        if (rdDetails.Checked == true) excel.addData(j + col + 2 + Temp - ExtraRow, 10, "", excel.ColName(j + col + 2 + Temp - ExtraRow, 10), excel.ColName(j + col + 2 + Temp - ExtraRow, 10), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    }
                                    if (rdDetails.Checked == true)
                                        excel.addData(j + col + 2 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), "", 0, CreateExcel.ExAlign.Right, true);
                                    if (VchCode == VchType.Purchase)
                                    {
                                        TotAmtGrand = TotAmtGrand + TotalAmt; TotAmt = TotAmt + TotalAmt; TotalAmt = 0;
                                        excel.addData(j + col + 2 + Temp - ExtraRow, Amount, Math.Abs(TotAmt).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    }
                                    else
                                    {
                                        TotAmtGrand = TotAmtGrand + TotalAmt; TotAmt = TotAmt + TotalAmt; TotalAmt = 0;
                                        excel.addData(j + col + 2 + Temp - ExtraRow, Amount, Math.Abs(TotAmt).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    }

                                    excel.addData(GrpRowNo, ColIndex.Qty, "", excel.ColName(GrpRowNo, ColIndex.Qty), excel.ColName(GrpRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotQty = GrandTotQty + TotQty;TotQty = 0;
                                    if (rdDetails.Checked == true) excel.addData(GrpRowNo, ColIndex.Disc, "", excel.ColName(GrpRowNo, ColIndex.Disc), excel.ColName(GrpRowNo, ColIndex.Disc), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotalDisc = GrandTotalDisc + TotDisc; TotDisc = 0;
                                    if (VchCode == VchType.Purchase)
                                    {
                                        excel.addData(GrpRowNo, FQty, "", excel.ColName(GrpRowNo, FQty), excel.ColName(GrpRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandFreeQty = GrandFreeQty + TotFreeQty; TotFreeQty = 0;
                                        excel.addData(GrpRowNo, TQty, "", excel.ColName(GrpRowNo, TQty), excel.ColName(GrpRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotalQty = GrandTotalQty + TotTotalQty; TotTotalQty = 0;
                                        if (rdDetails.Checked == true) excel.addData(GrpRowNo, 10, "", excel.ColName(GrpRowNo, 10), excel.ColName(GrpRowNo, 10), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotalDisc = GrandTotalDisc + TotDisc; TotDisc = 0;
                                    }
                                    excel.addData(GrpRowNo, Amount, "", excel.ColName(GrpRowNo, Amount), excel.ColName(GrpRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotAmt = GrandTotAmt + TotAmtGrand; TotAmtGrand = 0; TotAmt = 0;
                                    Temp++;
                                    
                                    {
                                        if (Convert.ToInt16(dt.Rows[j].ItemArray[12]) == 1)
                                        {
                                            if (VchCode == VchType.Sales)
                                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "Sales", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                            else if (VchCode == VchType.Purchase)
                                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "Purchase", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        }
                                        else
                                        {
                                            if (VchCode == VchType.Sales)
                                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "Sales Return", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                            else if (VchCode == VchType.Purchase)
                                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "Purchase Return", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        }
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, Math.Abs(VchTotQty).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), ColIndex.Qty, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, Amount, Math.Abs(VchTotAmt).ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Amount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        if (rdDetails.Checked == true)
                                        {
                                            excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, "", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), ColIndex.Disc, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                            excel.createHeaders(j + col + 2 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), Rate, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        }
                                        //VchTotQty = 0; VchTotAmt = 0;
                                        Temp++;
                                    }
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "GrandTotal", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, GrandTotQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    if (rdDetails.Checked == true) excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, GrandTotalDisc.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    if (rdDetails.Checked == true) excel.createHeaders(j + col + 2 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    if (VchCode == VchType.Purchase)
                                    {
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, FQty, GrandFreeQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, TQty, GrandTotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        if (rdDetails.Checked == true) excel.createHeaders(j + col + 2 + Temp - ExtraRow, 10, GrandTotalDisc.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, 10), excel.ColName(j + col + 2 + Temp - ExtraRow, 10), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    }
                                    if (rdDetails.Checked == true)
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, Amount, GrandTotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    else
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, Amount, GrandTotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    //excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Rate, GrandTotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    Temp++;
                                    
                                }
                            }
                            //col++;
                        }
                        excel.CompleteDoc("");

                    }
                    catch (Exception exc)
                    {
                        CommonFunctions.ErrorMessge = exc.Message;
                    }
                }
                else if (rdDayWiseSummary.Checked == true)
                {
                    try
                    {
                        str = "Exec GetUserwiseDaywiseSalesDetails " + VchCode + "," + DBGetVal.FirmNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + strItemNo + "'";
                        dt = ObjFunction.GetDataView(str).Table;
                        dt.Columns.RemoveAt(0);
                        dt.Columns[2].ColumnName = "Date";
                        dt.Columns[3].ColumnName = "TotalBills";
                        if (VchCode == VchType.Sales)
                        {
                            dt.Columns.RemoveAt(5); dt.Columns.RemoveAt(5);
                        }

                        int dtCount = dt.Columns.Count;

                        int col = 1; //int Temp = 0, ItemRowNo = 0, GrpRowNo = 0, ExtraRow = 0; //double TotalQty = 0, TotalAmt = 0, TotAmt = 0, GrandTotQty = 0, GrandTotAmt = 0;//, TotQty = 0
                        CreateExcel excel = new CreateExcel();
                        //Company Name Header
                        excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                        col++;
                        //Company Address Header
                        excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                        col++;
                        //Report Name And Dates
                        excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                        col++;
                        excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                        col++;
                        excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                        col++;

                        for (int i = 0; i < dtCount; i++)
                        {
                            if (i == 0 || i == 1)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else if (i == 2)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else //if (i == 3 || i == 4 || i==5)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (i == 0 || i == 1)
                                {
                                    excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, true, 25, Color.Black, 12, CreateExcel.ExAlign.Left);
                                }
                                else if (i == 2)
                                {
                                    if (Convert.ToDateTime(dt.Rows[j].ItemArray[i]).ToString("dd-MM-yyyy") == "01-01-1900")
                                        excel.createHeaders(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                }
                                else if (i == 5)
                                {
                                    if (dt.Rows[j].ItemArray[i].ToString() == "0" || dt.Rows[j].ItemArray[i].ToString() == "0.00")
                                        excel.addData(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                }
                                else
                                {
                                    if (dt.Rows[j].ItemArray[i].ToString() == "0" || dt.Rows[j].ItemArray[i].ToString() == "0.00")
                                        excel.createHeaders(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    else
                                        excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 15, Color.Black, 12, CreateExcel.ExAlign.Right);

                                }

                            }
                        }
                        excel.CompleteDoc("");
                    }
                    catch (Exception exc)
                    {
                        CommonFunctions.ErrorMessge = exc.Message;
                    }



                }
            }
            else
                OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);


        }
        public static class ColIndex
        {
            public static int Date = 3;
            public static int Time = 4;
            public static int ItemName = 2;
            public static int BillNo = 5;
            public static int Qty = 6;
            public static int Disc = 8;
            //public static int Rate = 7;
            //public static int Amt = 8;
            public static int GrpName = 1;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
