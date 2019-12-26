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
    public partial class CounterWiseSalesDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DBProgressBar PB;

        public long CompNo, ItNo, MNo, Type1, No, ItNo1,BItemNo,VchCode=0;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "";

        public CounterWiseSalesDetails()
        {
            InitializeComponent();
        }
        public CounterWiseSalesDetails(long vchno)
        {
            VchCode = vchno;
            InitializeComponent();
            if (VchCode == VchType.Sales)
                this.Text = "CounterWiseSalesDetails";
            else if (VchCode == VchType.Purchase)
                this.Text = "CounterWisePurchaseDetails";
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
            try
            {
                this.Cursor = Cursors.WaitCursor;

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
                    string[] ReportSession;
                    ReportSession = new string[5];
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = Convert.ToString(true);
                    ReportSession[3] = strItemNo;
                    ReportSession[4] = VchCode.ToString();
                    Form NewF = null;
                    if (rdDetails.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptCounterWiseSalesDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptCounterWiseSalesDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptCounterWiseSalesSummary(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptCounterWiseSalesSummary.rpt", CommonFunctions.ReportPath), ReportSession);

                    }
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
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
                //string str = " SELECT DISTINCT mItemMaster.ItemNo, (Select ItemName From dbo.MStockItems_V(NULL,MStockItems.ItemNo,NULL,NULL,NULL,NULL,NULL)) AS ItemName,'false' as chk FROM MStockItems INNER JOIN " +
                //               " TStock INNER JOIN TVoucherDetails INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo ON " +
                //               " mItemMaster.ItemNo = TStock.ItemNo where TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' AND TVoucherEntry.VoucherTypeCode=" + VchCode + " ";


                string str = " SELECT DISTINCT mItemMaster.ItemNo, MItemGroup.StockGroupName + mItemMaster.ItemName AS ItemName, 'false' AS chk " +
                           " FROM MStockItems INNER JOIN TStock INNER JOIN TVoucherDetails INNER JOIN " +
                           " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo ON  " +
                           " mItemMaster.ItemNo = TStock.ItemNo INNER JOIN " +
                           " MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo " +
                           "where TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' AND TVoucherEntry.VoucherTypeCode in (" + VchCode + "," + ((VchCode == VchType.Sales) ? 12 : 13) + ") order by MItemGroup.StockGroupName  + mItemMaster.ItemName ";
                dt = ObjFunction.GetDataView(str).Table;
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
                ReportSession[3] = Convert.ToString(false);
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
                                            " mItemMaster.ItemNo = TStock.ItemNo where TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy") + "' and TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy") + "' AND TVoucherEntry.VoucherTypeCode=" + 15 + " and TVoucherEntry.IsCancel='false'";
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "", ReportName = ""; DataTable dt = new DataTable();
                bool flag = false;//(IsSuperMode() == true) ? true : false;

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
                        ReportName = "CounterWise Sales Details";
                    else
                        ReportName = "CounterWise Purchase Details";
                }
                else if (rdSummary.Checked == true)
                {
                    if (VchCode == VchType.Sales)
                        ReportName = "CounterWise Sales Summary";
                    else
                        ReportName = "CounterWise Purchase Summary";
                }
                if (strItemNo != "")
                {
                    str = "Exec GetCounterwiseSalesDetails '" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + flag + ",'" + strItemNo + "'," + VchCode + "";

                    dt = ObjFunction.GetDataView(str).Table;
                    dt.Columns.RemoveAt(7); dt.Columns.RemoveAt(8);
                    int dtCount = dt.Columns.Count;
                    string[] strCol = new string[dtCount];
                    strCol[0] = "";
                    strCol[1] = "Itemname";
                    strCol[2] = "Date";
                    strCol[3] = "Time";
                    strCol[4] = "BillNo";
                    strCol[5] = "Qty";
                    strCol[6] = "Rate";
                    strCol[7] = "Disc.";
                    strCol[8] = "Amount";



                    string GrpName = "", ItemName = "";
                    try
                    {
                        int col = 1; int Temp = 0, ItemRowNo = 0, GrpRowNo = 0, ExtraRow = 0; double TotalQty = 0,TotalDisc=0, TotalAmt = 0, TotQty = 0,TotDisc=0, TotAmt = 0, GrandTotQty = 0,GrandTotDisc=0, GrandTotAmt = 0;
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
                                else if (i != 1 && i != 2 && i != 3 && i != 4 && i!=9)
                                    excel.createHeaders(col, i + 1, strCol[i].ToString(), excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                        }
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {

                            if (GrpName != dt.Rows[j].ItemArray[7].ToString())
                            {
                                //excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, "", false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                GrpName = dt.Rows[j].ItemArray[7].ToString(); GrpRowNo = j + col + 1 + Temp - ExtraRow; Temp++;
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
                                if (rdDetails.Checked == true)
                                    Temp++;
                            }



                            if (rdDetails.Checked == true)
                            {
                                excel.addData(j + col + 1 + Temp, ColIndex.Date, dt.Rows[j].ItemArray[0].ToString(), excel.ColName(j + col + 1, ColIndex.Date), excel.ColName(j + col + 1, ColIndex.Date), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.Time, Convert.ToDateTime(dt.Rows[j].ItemArray[1]).ToShortTimeString(), excel.ColName(j + col + 1, ColIndex.Time), excel.ColName(j + col + 1, ColIndex.Time), Format.HHMMSS, 0, CreateExcel.ExAlign.Left, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.BillNo, dt.Rows[j].ItemArray[3].ToString(), excel.ColName(j + col + 1, ColIndex.BillNo), excel.ColName(j + col + 1, ColIndex.BillNo), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.Qty, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1, ColIndex.Qty), excel.ColName(j + col + 1, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.Rate, dt.Rows[j].ItemArray[5].ToString(), excel.ColName(j + col + 1, ColIndex.Rate), excel.ColName(j + col + 1, ColIndex.Rate), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.Disc, dt.Rows[j].ItemArray[8].ToString(), excel.ColName(j + col + 1, ColIndex.Disc), excel.ColName(j + col + 1, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(j + col + 1 + Temp, ColIndex.Amt, dt.Rows[j].ItemArray[6].ToString(), excel.ColName(j + col + 1, ColIndex.Amt), excel.ColName(j + col + 1, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            }
                            TotalQty = TotalQty + Convert.ToDouble(dt.Rows[j].ItemArray[4]);
                            TotalDisc = TotalDisc + Convert.ToDouble(dt.Rows[j].ItemArray[8]);
                            TotalAmt = TotalAmt + Convert.ToDouble(dt.Rows[j].ItemArray[6]);
                            if (j < dt.Rows.Count - 1 || j == dt.Rows.Count - 1)
                            {
                                if (j < dt.Rows.Count - 1)
                                {
                                    if (ItemName != dt.Rows[j + 1].ItemArray[2].ToString() || GrpName != dt.Rows[j + 1].ItemArray[7].ToString())
                                    {

                                        if (rdDetails.Checked == true)
                                        {
                                            excel.createHeaders(j + col + 2 + Temp - ExtraRow, 2, "           " + dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                            excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Amt, TotalAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                            excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, TotalDisc.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                            excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, TotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                            Temp++;
                                        }
                                        else
                                        {
                                            if (ExtraRow != 0)
                                                ExtraRow--;
                                        }

                                        excel.addData(ItemRowNo, ColIndex.Qty, TotalQty.ToString(), excel.ColName(ItemRowNo, ColIndex.Qty), excel.ColName(ItemRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(ItemRowNo, ColIndex.Rate, "", excel.ColName(ItemRowNo, ColIndex.Rate), excel.ColName(ItemRowNo, ColIndex.Rate), "", 0, CreateExcel.ExAlign.Right, false); TotQty = TotQty + TotalQty; TotalQty = 0;
                                        if (rdSummary.Checked == true)
                                        {
                                            excel.addData(ItemRowNo, ColIndex.Disc, "", excel.ColName(ItemRowNo, ColIndex.Disc), excel.ColName(ItemRowNo, ColIndex.Disc), "", 0, CreateExcel.ExAlign.Right, false); TotDisc = TotDisc + TotalDisc; TotalDisc = 0;
                                        }
                                        else
                                        {
                                            excel.addData(ItemRowNo, ColIndex.Disc, TotalDisc.ToString(), excel.ColName(ItemRowNo, ColIndex.Disc), excel.ColName(ItemRowNo, ColIndex.Disc), "", 0, CreateExcel.ExAlign.Right, false); TotDisc = TotDisc + TotalDisc; TotalDisc = 0;
                                        }
                                        excel.addData(ItemRowNo, ColIndex.Amt, TotalAmt.ToString(), excel.ColName(ItemRowNo, ColIndex.Amt), excel.ColName(ItemRowNo, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); TotAmt = TotAmt + TotalAmt; TotalAmt = 0;

                                    }

                                    if (GrpName != dt.Rows[j + 1].ItemArray[7].ToString())
                                    {
                                        if ((j + col + 2 + Temp - ExtraRow) - ((j + col + 2 + Temp - ExtraRow - 2)) > 1 && ExtraRow >= 0 && j != 0 && rdSummary.Checked == true)
                                            ExtraRow++;
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Rate), "", 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Amt, ""/*TotAmt.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, ""/*TotDisc.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, ""/*TotQty.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);


                                        Temp++;

                                        excel.addData(GrpRowNo, ColIndex.Qty, ""/*TotQty.ToString()*/
                                                                                                     , excel.ColName(GrpRowNo, ColIndex.Qty), excel.ColName(GrpRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotQty = GrandTotQty + TotQty; TotQty = 0;
                                        excel.addData(GrpRowNo, ColIndex.Disc,  ""/*TotDisc.ToString()*/, excel.ColName(GrpRowNo, ColIndex.Disc), excel.ColName(GrpRowNo, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotDisc = GrandTotDisc + TotDisc; TotDisc = 0;
                                        excel.addData(GrpRowNo, ColIndex.Amt, ""/*TotAmt.ToString()*/, excel.ColName(GrpRowNo, ColIndex.Amt), excel.ColName(GrpRowNo, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotAmt = GrandTotAmt + TotAmt; TotAmt = 0;

                                    }
                                }
                                else if (j == dt.Rows.Count - 1)
                                {
                                    //if (rdDetails.Checked == true)
                                    {
                                        excel.createHeaders(j + col + 2 + Temp, 2, "           " + dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.addData(j + col + 2 + Temp, ColIndex.Qty, TotalQty.ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.Qty), excel.ColName(j + col + 2 + Temp, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp, ColIndex.Disc, TotalDisc.ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.Disc), excel.ColName(j + col + 2 + Temp, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp, ColIndex.Amt, TotalAmt.ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.Amt), excel.ColName(j + col + 2 + Temp, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        Temp++;
                                    }
                                    excel.addData(ItemRowNo, ColIndex.Qty, TotalQty.ToString(), excel.ColName(ItemRowNo, ColIndex.Qty), excel.ColName(ItemRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotQty = TotQty + TotalQty; TotalQty = 0;
                                    excel.addData(ItemRowNo, ColIndex.Rate, "", excel.ColName(ItemRowNo, ColIndex.Rate), excel.ColName(ItemRowNo, ColIndex.Rate), "", 0, CreateExcel.ExAlign.Right, false);
                                    if (rdSummary.Checked == true)
                                    {
                                        excel.addData(ItemRowNo, ColIndex.Disc,"", excel.ColName(ItemRowNo, ColIndex.Disc), excel.ColName(ItemRowNo, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); TotDisc = TotDisc + TotDisc; TotDisc = 0;
                                    }
                                    else
                                    {
                                        excel.addData(ItemRowNo, ColIndex.Disc, TotalDisc.ToString(), excel.ColName(ItemRowNo, ColIndex.Disc), excel.ColName(ItemRowNo, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); TotDisc = TotDisc + TotDisc; TotDisc = 0;
                                    }
                                    excel.addData(ItemRowNo, ColIndex.Amt, TotalAmt.ToString(), excel.ColName(ItemRowNo, ColIndex.Amt), excel.ColName(ItemRowNo, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); TotAmt = TotAmt + TotalAmt; TotalAmt = 0;
                                    if (rdSummary.Checked == true)
                                        ExtraRow++;
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, ""/*TotQty.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Rate), "", 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Disc, ""/*TotDisc.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Amt, ""/*TotAmt.ToString()*/, excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);

                                    excel.addData(GrpRowNo, ColIndex.Qty, ""/*TotQty.ToString()*/
                                                                                                 , excel.ColName(GrpRowNo, ColIndex.Qty), excel.ColName(GrpRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotQty = GrandTotQty + TotQty; TotQty = 0;
                                    excel.addData(GrpRowNo, ColIndex.Disc,""/* TotDisc.ToString()*/, excel.ColName(GrpRowNo, ColIndex.Disc), excel.ColName(GrpRowNo, ColIndex.Disc), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotDisc = GrandTotDisc + TotDisc; TotDisc = 0;
                                    excel.addData(GrpRowNo, ColIndex.Amt,""/* TotAmt.ToString()*/, excel.ColName(GrpRowNo, ColIndex.Amt), excel.ColName(GrpRowNo, ColIndex.Amt), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotAmt = GrandTotAmt + TotAmt; TotAmt = 0;

                                    Temp++;
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "GrandTotal", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, GrandTotQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Rate, GrandTotDisc.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Disc), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Amt, GrandTotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Amt), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    Temp++;
                                }
                            }
                            //col++;
                        }
                        excel.CompleteDoc("");

                    }
                    catch (Exception ex)
                    {
                        CommonFunctions.ErrorMessge = ex.Message;
                    }
                }
                else
                    OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public static class ColIndex
        {
            public static int Date = 3;
            public static int Time = 4;
            public static int ItemName = 2;
            public static int BillNo = 5;
            public static int Qty = 6;
            public static int Rate = 7;
            public static int Disc = 8;
            public static int Amt = 9;
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
