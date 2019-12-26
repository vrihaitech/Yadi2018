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
    public partial class LedgerItemWiseSalesDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataTable dtParty = new DataTable();
        DBProgressBar PB;

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo, VoucherTypeNo, VoucherType, VoucherType1;
        public string ItName, RptTitle, ItNm;
        string strItemNo = "", strLedgerNo = "";

        public LedgerItemWiseSalesDetails()
        {
            InitializeComponent();
        }

        public LedgerItemWiseSalesDetails(long VoucherType1)
        {
            InitializeComponent();
            // VoucherTypeNo = VoucherType;
            VoucherType = VoucherType1;
            VoucherTypeNo = getVouchertypeno(VoucherType);
            //if (DBGetVal.KachhaFirm == false)
            //{

            //    if (VoucherType == VchType.Sales)
            //    {
            //        this.Text = "Partywise Itemwise Sales Details";
            //    }
            //    else if (VoucherType == VchType.Purchase)
            //    {
            //        this.Text = "Partywise Itemwise Purchase Details";
            //    }
            //}
            //else {
            //    if (VoucherType == VchType.DSales)
            //    {
            //        this.Text = "Partywise Itemwise Estimate Sales Details";
            //    }
            //    else if (VoucherType == VchType.DPurchase)
            //    {
            //        this.Text = "Partywise Itemwise Estimate Purchase Details";
            //    }
            //}
        }

        public long getVouchertypeno(long VoucherType)
        {
            if (DBGetVal.KachhaFirm == false)
            {
                if (VoucherType == VchType.Sales)
                {
                    VoucherTypeNo = VchType.Sales;
                    // this.Text = "Partywise Itemwise Sales Details";
                }
                else if (VoucherType == VchType.Purchase)
                {
                    VoucherTypeNo = VchType.Purchase;
                    // this.Text = "Partywise Itemwise Purchase Details";
                }
            }
            else
            {
                if (VoucherType == 15)
                {
                    VoucherTypeNo = VchType.DSales;
                    // this.Text = "Partywise Itemwise Estimate Sales Details";
                }
                else
                if (VoucherType == 9)
                {
                    VoucherTypeNo = VchType.DPurchase;
                    // this.Text = "Partywise Itemwise Estimate Purchase Details";
                }
            }
            return VoucherTypeNo;
        }

        private void LedgerItemWiseSalesDetails_Load(object sender, EventArgs e)
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
                        string[] ReportSession;

                        ReportSession = new string[5];

                        ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[2] = strItemNo;
                        ReportSession[3] = strLedgerNo;
                        ReportSession[4] = VoucherTypeNo.ToString();

                        Form NewF = null;
                        if (rdDetails.Checked == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RptLedgerItemWiseSalesDetails(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptLedgerItemWiseSalesDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RptLedgerItemWiseSalesSumm(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptLedgerItemWiseSalesSumm.rpt", CommonFunctions.ReportPath), ReportSession);
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

        public void BindGridItem()
        {
            try
            {
                string str = "";
                DataTable dt = new DataTable();
      
                str = " SELECT DISTINCT mItemMaster.ItemNo, " +
                       " (Select ItemName From MStockItems_V(NULL,mItemMaster.ItemNo,NULL,NULL,NULL,NULL,NULL)) AS ItemName, " +
                       " 'false' as chk FROM mItemMaster INNER JOIN " +
                       " TStock INNER JOIN TVoucherDetails " +
                       " INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                       " ON TStock.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo " +
                       " ON mItemMaster.ItemNo = TStock.ItemNo " +
                       " WHERE (TVoucherDetails.LedgerNo IN (" + strLedgerNo + ")) AND " +
                       " TVoucherEntry.VoucherDate >='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' AND " +
                       " TVoucherEntry.VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' AND " +
                       " (TVoucherEntry.VoucherTypeCode = " + VoucherTypeNo + ") " +
                       " Order By ItemName";

            
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
                BtnPartyShow.Focus();

            }
        }

        private void BtnPartyShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTToDate.Value < DTPFromDate.Value)
                {
                    OMMessageBox.Show("To Date cannot be less than From Date ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    DTPFromDate.Focus();
                    pnlItemDetails.Visible = false;
                    pnlPartyDetails.Visible = false;
                }
                else
                {
                    rbpartywsie.Checked = false;
                    rbpartyitem.Checked = false;
            //        PB = new DBProgressBar(this);
            //        PB.TimerStart();
            //        PB.Ctrl = pnlPartyDetails;
            //        VoucherTypeNo = getVouchertypeno(VoucherType);
            //        BindGridParty();
            //        pnlPartyDetails.Visible = true;


            //        strLedgerNo = "";
            //        strItemNo = "";
            //        chkPartySelectAll.Checked = false;
               }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridParty()
        {
            try
            {
                string str = "";
                dtParty = new DataTable();

                if (VoucherType == 15)
                {
                    str = "SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName,'false' as chk " +
                                " FROM MLedger INNER JOIN " +
                                " TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                                " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                                " WHERE (TVoucherDetails.VoucherSrno = 1) " +
                                "  AND " +
                                "  MLedger.GroupNo = " + GroupType.SundryDebtors + " AND " +
                                " (TVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "') " +
                                " AND (TVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "') " +
                                "ANd (TVoucherEntry.VoucherTypeCode =" + VoucherTypeNo + ")" +
                                " Order By LedgerName";
                }
                if (VoucherType == 9)
                {

                    str = "SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName,'false' as chk " +
                                " FROM MLedger INNER JOIN " +
                                " TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                                " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                                " WHERE (TVoucherDetails.VoucherSrno = 1) " +
                                " AND " +
                                "  MLedger.GroupNo = " + GroupType.SundryCreditors + " AND " +
                                " (TVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "') " +
                                " AND (TVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "') " +
                                "ANd (TVoucherEntry.VoucherTypeCode =" + VoucherTypeNo + ")" +
                                " Order By LedgerName";

                }
                dtParty = ObjFunction.GetDataView(str).Table;
                gvParty.Rows.Clear();
                for (int i = 0; i < dtParty.Rows.Count; i++)
                {
                    gvParty.Rows.Add();
                    for (int j = 0; j < gvParty.Columns.Count; j++)
                        gvParty.Rows[i].Cells[j].Value = dtParty.Rows[i].ItemArray[j];

                }
                gvParty.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (gvParty.Rows.Count > 0)
                {
                    gvParty.Focus();
                    gvParty.CurrentCell = gvParty[2, 0];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
                if (pnlItemDetails.Visible == true)
                {
                    chkSelectAll.Checked = !chkSelectAll.Checked;

                    for (int i = 0; i < gvItem.Rows.Count; i++)
                    {
                        gvItem.Rows[i].Cells[2].Value = chkSelectAll.Checked;
                    }
                    BtnShow.Focus();
                }
                else
                {
                    chkPartySelectAll.Checked = !chkPartySelectAll.Checked;

                    for (int i = 0; i < gvParty.Rows.Count; i++)
                    {
                        gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
                    }
                    BtnShowItem.Focus();
                }
            }
        }

        private void rbpartywsie_CheckedChanged(object sender, EventArgs e)
        {
            PB = new DBProgressBar(this);
            PB.TimerStart();
            PB.Ctrl = pnlPartyDetails;
            VoucherTypeNo = getVouchertypeno(VoucherType);
            BindGridParty();
            pnlPartyDetails.Visible = true;


            strLedgerNo = "";
            strItemNo = "";
            chkPartySelectAll.Checked = false;
        }

        private void rbpartyitem_CheckedChanged(object sender, EventArgs e)
        {
            PB = new DBProgressBar(this);
            PB.TimerStart();
            PB.Ctrl = pnlPartyDetails;
            VoucherTypeNo = getVouchertypeno(VoucherType);
            BindGridParty();
            pnlPartyDetails.Visible = true;


            strLedgerNo = "";
            strItemNo = "";
            chkPartySelectAll.Checked = false;
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

        private void BtnShowItem_Click(object sender, EventArgs e)
        {
            try
            {
                strLedgerNo = "";
                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvParty.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strLedgerNo == "")
                            strLedgerNo = gvParty.Rows[i].Cells[0].Value.ToString();
                        else
                            strLedgerNo = strLedgerNo + "," + gvParty.Rows[i].Cells[0].Value.ToString();
                    }
                }

                if (rbpartyitem.Checked == true)
                {

                    if (strLedgerNo != "")
                    {
                        pnlPartyDetails.Visible = false;
                        pnlItemDetails.Visible = false;
                        PB = new DBProgressBar(this);
                        PB.TimerStart();
                        PB.Ctrl = pnlItemDetails;
                        BindGridItem();
                        //pnlItemDetails.Visible = true;

                    }
                    else
                        OMMessageBox.Show("Select Atleast one PartyName ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                else
                {

                    string[] ReportSession;
                    ReportSession = new string[5];
                    ReportSession[0] = VoucherType.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] =  strLedgerNo.ToString();
                    Form NewF = null;
           
                    {
                        //ReportSession[4] = "1";
                        //ReportSession[5] = "0";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)

                            NewF = new Display.ReportViewSource(new Reports.RPTPartyWiseRegister(), ReportSession);
                  
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTPartyWiseRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }


                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            // this.Cursor = Cursors.Default;


        }

        private void chkPartySelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvParty.Rows.Count; i++)
            {
                gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            pnlPartyDetails.Visible = false;
            BtnPartyShow.Focus();
            chkPartySelectAll.Checked = false;
            strLedgerNo = "";
        }

        private void BtnItemCancel_Click(object sender, EventArgs e)
        {
            pnlItemDetails.Visible = false;
            pnlPartyDetails.Visible = true;
            chkPartySelectAll.Focus();
            chkSelectAll.Checked = false;
            strItemNo = "";
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "", ReportName = ""; DataTable dt = new DataTable();
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
                    if (VoucherTypeNo == VchType.Sales)
                    {
                        ReportName = "Partywise ItemWise Sales Details";
                    }
                    else if (VoucherTypeNo == VchType.Purchase)
                    {
                        ReportName = "Partywise ItemWise Purcahse Details";
                    }


                }
                else if (rdSummary.Checked == true)
                {
                    if (VoucherTypeNo == VchType.Sales)
                    {
                        ReportName = "Partywise ItemWise Sales Summary";
                    }
                    else if (VoucherTypeNo == VchType.Purchase)
                    {
                        ReportName = "Partywise ItemWise Purcahse Summary";
                    }
                }
                if (strItemNo != "")
                {
                    //str = " GetLedgerItemWiseSalesDetails '" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + flag + ",'" + strItemNo + "','" + strLedgerNo + "'";
                    str = " GetLedgerItemWiseSalesDetails '" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + strItemNo + "','" + strLedgerNo + "'," + VoucherTypeNo.ToString();

                    int FQty = 0, TQty = 0, Rate = 0, Amount = 0;
                    dt = ObjFunction.GetDataView(str).Table;
                    dt.Columns.RemoveAt(9); //dt.Columns.RemoveAt(7);
                    int dtCount = 0;
                    if (rdDetails.Checked == true)
                        dtCount = dt.Columns.Count - 1;
                    else
                        dtCount = dt.Columns.Count - 2;
                    string[] strCol = new string[dtCount];
                    strCol[0] = "PartyName";
                    strCol[1] = "ItemName";
                    strCol[2] = "Date";
                    strCol[3] = "Time";
                    strCol[4] = "BillNo";
                    strCol[5] = "Qty";
                    strCol[6] = "FreeQty";
                    strCol[7] = "TotalQty";
                    if (rdDetails.Checked == true)
                    {
                        strCol[8] = "Rate";
                        strCol[9] = "Amount";
                        Rate = 9; Amount = 10;
                    }
                    else
                    {
                        strCol[8] = "Amount";
                        Amount = 9;
                    }
                    FQty = 7; TQty = 8;



                    string GrpName = "", ItemName = "";

                    int col = 1; int Temp = 0, ItemRowNo = 0, GrpRowNo = 0, ExtraRow = 0; double TotalQty = 0, TotalAmt = 0, TotQty = 0, TotAmt = 0, GrandTotQty = 0, GrandTotAmt = 0, TotalFreeQty = 0, TotalTotalQty = 0, TotFreeQty = 0, TotTotalQty = 0, GrandFreeQty = 0, GrandTotalQty = 0;
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
                            //if (i == 0)
                            //    excel.createHeaders(col, i + 1, strCol[i + 1].ToString(), excel.ColName(col, i + 1), excel.ColName(col, i + 2), 2, Color.Gainsboro, true, 5, Color.Black, 12, CreateExcel.ExAlign.Center);
                            //else 
                            if (i == 2 || i == 3)
                                excel.createHeaders(col, i + 1, strCol[i].ToString(), excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else
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
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {

                        if (GrpName != dt.Rows[j].ItemArray[10].ToString())
                        {
                            //excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, "", false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 1 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                            GrpName = dt.Rows[j].ItemArray[10].ToString(); GrpRowNo = j + col + 1 + Temp - ExtraRow; Temp++;
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
                            excel.addData(j + col + 1 + Temp, FQty, dt.Rows[j].ItemArray[5].ToString(), excel.ColName(j + col + 1, FQty), excel.ColName(j + col + 1, FQty), "", 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(j + col + 1 + Temp, TQty, dt.Rows[j].ItemArray[6].ToString(), excel.ColName(j + col + 1, TQty), excel.ColName(j + col + 1, TQty), "", 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(j + col + 1 + Temp, Rate, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1, Rate), excel.ColName(j + col + 1, Rate), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(j + col + 1 + Temp, Amount, dt.Rows[j].ItemArray[8].ToString(), excel.ColName(j + col + 1, Amount), excel.ColName(j + col + 1, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        }
                        TotalFreeQty = TotalFreeQty + Convert.ToDouble(dt.Rows[j].ItemArray[5]);
                        TotalTotalQty = TotalTotalQty + Convert.ToDouble(dt.Rows[j].ItemArray[6]);
                        TotalQty = TotalQty + Convert.ToDouble(dt.Rows[j].ItemArray[4]);
                        TotalAmt = TotalAmt + Convert.ToDouble(dt.Rows[j].ItemArray[8]);
                        if (j < dt.Rows.Count - 1 || j == dt.Rows.Count - 1)
                        {
                            if (j < dt.Rows.Count - 1)
                            {
                                if (ItemName != dt.Rows[j + 1].ItemArray[2].ToString())
                                {

                                    if (rdDetails.Checked == true)
                                    {
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 2, "           " + dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, Amount, TotalAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, TotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, FQty, TotalFreeQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        excel.addData(j + col + 2 + Temp - ExtraRow, TQty, TotalTotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                        Temp++;
                                    }
                                    else
                                    {
                                        if (ExtraRow != 0)
                                            ExtraRow--;
                                    }

                                    excel.addData(ItemRowNo, ColIndex.Qty, TotalQty.ToString(), excel.ColName(ItemRowNo, ColIndex.Qty), excel.ColName(ItemRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(ItemRowNo, FQty, TotalFreeQty.ToString(), excel.ColName(ItemRowNo, FQty), excel.ColName(ItemRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotFreeQty = TotFreeQty + TotalFreeQty; TotalFreeQty = 0;
                                    excel.addData(ItemRowNo, TQty, TotalTotalQty.ToString(), excel.ColName(ItemRowNo, TQty), excel.ColName(ItemRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotTotalQty = TotTotalQty + TotalTotalQty; TotalTotalQty = 0;
                                    if (rdDetails.Checked == true)
                                        excel.addData(ItemRowNo, Rate, "", excel.ColName(ItemRowNo, Rate), excel.ColName(ItemRowNo, Rate), "", 0, CreateExcel.ExAlign.Right, false); TotQty = TotQty + TotalQty; TotalQty = 0;
                                    excel.addData(ItemRowNo, Amount, TotalAmt.ToString(), excel.ColName(ItemRowNo, Amount), excel.ColName(ItemRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); TotAmt = TotAmt + TotalAmt; TotalAmt = 0;

                                }

                                if (GrpName != dt.Rows[j + 1].ItemArray[10].ToString())
                                {
                                    if ((j + col + 2 + Temp - ExtraRow) - ((j + col + 2 + Temp - ExtraRow - 2)) > 1 && ExtraRow >= 0 && j != 0 && rdSummary.Checked == true)
                                        ExtraRow++;
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    if (rdDetails.Checked == true)
                                        excel.addData(j + col + 2 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), "", 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, Amount, TotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, TotQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, FQty, TotFreeQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp - ExtraRow, TQty, TotTotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);

                                    Temp++;

                                    excel.addData(GrpRowNo, ColIndex.Qty, TotQty.ToString(), excel.ColName(GrpRowNo, ColIndex.Qty), excel.ColName(GrpRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotQty = GrandTotQty + TotQty; TotQty = 0;
                                    excel.addData(GrpRowNo, FQty, TotFreeQty.ToString(), excel.ColName(GrpRowNo, FQty), excel.ColName(GrpRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandFreeQty = GrandFreeQty + TotFreeQty; TotFreeQty = 0;
                                    excel.addData(GrpRowNo, TQty, TotTotalQty.ToString(), excel.ColName(GrpRowNo, TQty), excel.ColName(GrpRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotalQty = GrandTotalQty + TotTotalQty; TotTotalQty = 0;
                                    excel.addData(GrpRowNo, Amount, TotAmt.ToString(), excel.ColName(GrpRowNo, Amount), excel.ColName(GrpRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotAmt = GrandTotAmt + TotAmt; TotAmt = 0;

                                }
                            }
                            else if (j == dt.Rows.Count - 1)
                            {
                                if (rdDetails.Checked == true)
                                {
                                    excel.createHeaders(j + col + 2 + Temp, 2, "           " + dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                    excel.addData(j + col + 2 + Temp, ColIndex.Qty, TotalQty.ToString(), excel.ColName(j + col + 2 + Temp, ColIndex.Qty), excel.ColName(j + col + 2 + Temp, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp, FQty, TotalFreeQty.ToString(), excel.ColName(j + col + 2 + Temp, FQty), excel.ColName(j + col + 2 + Temp, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp, TQty, TotalTotalQty.ToString(), excel.ColName(j + col + 2 + Temp, TQty), excel.ColName(j + col + 2 + Temp, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                    excel.addData(j + col + 2 + Temp, Amount, TotalAmt.ToString(), excel.ColName(j + col + 2 + Temp, Amount), excel.ColName(j + col + 2 + Temp, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    Temp++;
                                }
                                excel.addData(ItemRowNo, ColIndex.Qty, TotalQty.ToString(), excel.ColName(ItemRowNo, ColIndex.Qty), excel.ColName(ItemRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotQty = TotQty + TotalQty; TotalQty = 0;
                                excel.addData(ItemRowNo, FQty, TotalFreeQty.ToString(), excel.ColName(ItemRowNo, FQty), excel.ColName(ItemRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotFreeQty = TotFreeQty + TotalFreeQty; TotalFreeQty = 0;
                                excel.addData(ItemRowNo, TQty, TotalTotalQty.ToString(), excel.ColName(ItemRowNo, TQty), excel.ColName(ItemRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); TotTotalQty = TotTotalQty + TotalTotalQty; TotalTotalQty = 0;
                                if (rdDetails.Checked == true)
                                    excel.addData(ItemRowNo, Rate, "", excel.ColName(ItemRowNo, Rate), excel.ColName(ItemRowNo, Rate), "", 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(ItemRowNo, Amount, TotalAmt.ToString(), excel.ColName(ItemRowNo, Amount), excel.ColName(ItemRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); TotAmt = TotAmt + TotalAmt; TotalAmt = 0;


                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), ColIndex.BillNo, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                excel.addData(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, TotQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                excel.addData(j + col + 2 + Temp - ExtraRow, FQty, TotFreeQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                excel.addData(j + col + 2 + Temp - ExtraRow, TQty, TotTotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true);
                                if (rdDetails.Checked == true)
                                    excel.addData(j + col + 2 + Temp - ExtraRow, Rate, "", excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), "", 0, CreateExcel.ExAlign.Right, true);
                                excel.addData(j + col + 2 + Temp - ExtraRow, Amount, TotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);

                                excel.addData(GrpRowNo, ColIndex.Qty, TotQty.ToString(), excel.ColName(GrpRowNo, ColIndex.Qty), excel.ColName(GrpRowNo, ColIndex.Qty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotQty = GrandTotQty + TotQty; TotQty = 0;
                                excel.addData(GrpRowNo, FQty, TotFreeQty.ToString(), excel.ColName(GrpRowNo, FQty), excel.ColName(GrpRowNo, FQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandFreeQty = GrandFreeQty + TotFreeQty; TotFreeQty = 0;
                                excel.addData(GrpRowNo, TQty, TotTotalQty.ToString(), excel.ColName(GrpRowNo, TQty), excel.ColName(GrpRowNo, TQty), Format.NoFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotalQty = GrandTotalQty + TotTotalQty; TotTotalQty = 0;
                                excel.addData(GrpRowNo, Amount, TotAmt.ToString(), excel.ColName(GrpRowNo, Amount), excel.ColName(GrpRowNo, Amount), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true); GrandTotAmt = GrandTotAmt + TotAmt; TotAmt = 0;
                                Temp++;
                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "GrandTotal", excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.GrpName), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.BillNo), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, ColIndex.Qty, GrandTotQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), excel.ColName(j + col + 2 + Temp - ExtraRow, ColIndex.Qty), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, FQty, GrandFreeQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), excel.ColName(j + col + 2 + Temp - ExtraRow, FQty), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                excel.createHeaders(j + col + 2 + Temp - ExtraRow, TQty, GrandTotalQty.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), excel.ColName(j + col + 2 + Temp - ExtraRow, TQty), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                if (rdDetails.Checked == true)
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, Rate, GrandTotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Rate), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                else
                                    excel.createHeaders(j + col + 2 + Temp - ExtraRow, Amount, GrandTotAmt.ToString(), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), excel.ColName(j + col + 2 + Temp - ExtraRow, Amount), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                                Temp++;
                            }
                        }
                        //col++;
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

        private void rbCustomer_CheckedChanged(object sender, EventArgs e)
        {
            VoucherTypeNo = 15;
            BindGridParty();
        }

        private void rbSupplier_CheckedChanged(object sender, EventArgs e)
        {
            VoucherTypeNo = 9;
            BindGridParty();
        }



    }
}
