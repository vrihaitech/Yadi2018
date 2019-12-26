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
    public partial class PartyWiseTaxDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VchCode;
        public int voucherno;

        DataTable dtParty;
        public static string LedgName, RptTitle, strLedgerNo;
        public static int Type;
        public PartyWiseTaxDetails()
        {
            InitializeComponent();
        }
        public PartyWiseTaxDetails(long VchTypeCode)
        {
            InitializeComponent();
            if (VchTypeCode == VchType.Sales)
            {
                VchCode = VchType.Sales;
                this.Text = "Party Wise Sales Tax Details";
            }
            else if (VchTypeCode == VchType.Purchase)
            {
                VchCode = VchType.Purchase;
                this.Text = "Party Wise Purchase Tax Details";
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

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkPartySelectAll.Checked = !chkPartySelectAll.Checked;

                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
                }
                if (chkPartySelectAll.Checked) BtnExport.Focus();
            }
            if (e.KeyCode == Keys.F3)
            {
                BtnPartyShow_Click(sender, new EventArgs());
            }
            if (e.KeyCode == Keys.F4)
            {
                rdSummary.Checked = true;
            }
            if (e.KeyCode == Keys.F5)
            {
                rdDaySummary.Checked = true;
            }
            if (e.KeyCode == Keys.F6)
            {
                if (BtnExport.Visible == true) BtnExport_Click(sender, new EventArgs());
         
            }
        }

        private void TaxDetails_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                BtnExport.Visible = true;
            else
                BtnExport.Visible = false;
            KeyDownFormat(this.Controls);
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
                rdSummary.Focus();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            CreateExcel excel;
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
                if (strLedgerNo != "")
                {
                    DataTable dt = new DataTable();
                    DataTable dtHeader = new DataTable();
                    DataSet ds;
                    string str = "", ReportName = "", strDisc = "", strChrg = "";
                    long RndOff;
                    int dtCount = 0;
                    if (rdSummary.Checked == true)
                    {
                        strDisc += ObjFunction.GetAppSettings(AppSettings.S_Discount1);
                        strChrg += ObjFunction.GetAppSettings(AppSettings.S_Charges1);
                        RndOff = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc));
                    }
                    else
                    {
                        strDisc += ObjFunction.GetAppSettings(AppSettings.P_Discount1);
                        strChrg += ObjFunction.GetAppSettings(AppSettings.P_Charges1);
                        RndOff = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RoundOfAcc));
                    }
                    if (rdSummary.Checked == true)
                    {
                        if (VchCode == VchType.Sales) ReportName = "Sale Tax Details Billwise Percentwise";
                        else ReportName = "Purchase Tax Details Billwise Percentwise";

                        str = "Exec GetLedgerWiseTaxDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + ",'" + strLedgerNo + "'";
                        ds = ObjDset.FillDset("New", str, CommonFunctions.ConStr);
                        if (ds.Tables.Count <= 1)
                        {
                            MessageBox.Show("No Data Found", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            dtHeader = ds.Tables[0];
                            dt = ds.Tables[1];
                            dt.Columns.RemoveAt(0);

                            //- 2; PartyName = "Party";

                            string strLedgerName = "";

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i].ItemArray[2].ToString().Trim() != "Total")
                                {
                                    if (strLedgerName == dt.Rows[i].ItemArray[2].ToString().Trim())
                                    {

                                    }
                                    else
                                    {
                                        if (dt.Rows.Count - 1 != i)
                                        {
                                            DataRow dr1 = dt.NewRow();
                                            dr1[1] = "-1";
                                            dr1[0] = dt.Rows[i].ItemArray[2].ToString();
                                            dr1[2] = dt.Rows[i].ItemArray[2].ToString();
                                            dt.Rows.InsertAt(dr1, i);
                                        }

                                        if (i != 0)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dr[2] = "Total ";
                                            dr[1] = "Total ";
                                            for (int c = 3; c < dt.Columns.Count; c++)
                                            {
                                                dr[c] = dt.Compute("Sum(" + dt.Columns[c].ColumnName + ")", "" + dt.Columns[2].ColumnName + " = '" + strLedgerName + "' ");
                                            }
                                            dt.Rows.InsertAt(dr, i);
                                            strLedgerName = dt.Rows[i + 1].ItemArray[2].ToString().Trim();
                                        }
                                        else strLedgerName = dt.Rows[i].ItemArray[2].ToString().Trim();

                                    }
                                }
                                else strLedgerName = dt.Rows[i].ItemArray[2].ToString().Trim();
                            }
                        }
                        dt.Columns.RemoveAt(2);
                        dtCount = dt.Columns.Count;
                    }
                    else if (rdDaySummary.Checked == true)
                    {
                        if (VchCode == VchType.Sales) ReportName = "Sales Tax Details Datewise Summary";
                        else ReportName = "Purchase Tax Details Datewise Summary";
                        str = "Exec GetLedgerWiseTaxDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + ",'" + strLedgerNo + "'";
                        ds = ObjDset.FillDset("New", str, CommonFunctions.ConStr);
                        if (ds.Tables.Count <= 1)
                        {
                            MessageBox.Show("No Data Found", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            dtHeader = ds.Tables[0];
                            dt = ds.Tables[1];
                            dt.Columns[2].ColumnName = "TotalBills";
                            dt.Columns[1].ColumnName = "Date";


                            long TempLedgerNo = 0;

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i].ItemArray[3].ToString().Trim() != "Total")
                                {
                                    if (TempLedgerNo == Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString().Trim()))
                                    {

                                    }
                                    else
                                    {
                                        if (dt.Rows.Count - 1 != i)
                                        {
                                            DataRow dr1 = dt.NewRow();
                                            dr1[2] = "-1";
                                            dr1[0] = dt.Rows[i].ItemArray[0].ToString();
                                            dr1[1] = dt.Rows[i].ItemArray[3].ToString();
                                            dr1[3] = dt.Rows[i].ItemArray[3].ToString();
                                            dt.Rows.InsertAt(dr1, i);
                                        }

                                        if (i != 0)
                                        {
                                            DataRow dr = dt.NewRow();
                                            dr[2] = "Total ";
                                            dr[3] = "Total ";
                                            for (int c = 4; c < dt.Columns.Count; c++)
                                            {
                                                dr[c] = dt.Compute("Sum(" + dt.Columns[c].ColumnName + ")", "" + dt.Columns[0].ColumnName + " = '" + TempLedgerNo + "' ");
                                            }
                                            dt.Rows.InsertAt(dr, i);
                                            TempLedgerNo = Convert.ToInt64(dt.Rows[i + 1].ItemArray[0].ToString().Trim());
                                        }
                                        else TempLedgerNo = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString().Trim());

                                    }
                                }
                                else TempLedgerNo = Convert.ToInt64(dt.Rows[i].ItemArray[0].ToString().Trim());
                            }
                            dt.Columns.RemoveAt(0);
                            dt.Columns.RemoveAt(2);

                            dtCount = dt.Columns.Count;
                        }

                    }
                    else
                    {
                        OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                    if (str != "")
                    {

                        int col = 1;
                        excel = new CreateExcel();
                        //Company Name Header
                        excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                        col++;
                        //Company Address Header
                        excel.createHeaders(col, 1, DBGetVal.CompanyAddress.Replace("\r\n",""), excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                        col++;
                        //Report Name And Dates
                        excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                        col++;
                        excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                        col++;
                        excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                        col++;
                        if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                            excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 6), 6, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else
                            excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 6), 6, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        if (dtHeader.Rows.Count > 0)
                        {
                            if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                                excel.createHeaders(col, 7, "Value Added Tax", excel.ColName(col, 7), excel.ColName(col, dtCount - 2), dtCount - 3 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                            else
                                excel.createHeaders(col, 7, "Value Added Tax", excel.ColName(col, 7), excel.ColName(col, dtCount - 2), dtCount - 3 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                        }
                        excel.createHeaders(col, dtCount - 2 + 1, "", excel.ColName(col, dtCount - 2 + 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        col++;
                        if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                            excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 6), 6, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else
                            excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 6), 6, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                        for (int i = 0, j = 0; i < dtHeader.Rows.Count; i++, j = j + 2)
                        {
                            if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                                excel.createHeaders(col, j + 7, dtHeader.Rows[i].ItemArray[0].ToString() + "%", excel.ColName(col, j + 7), excel.ColName(col, j + 7 + 1), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                            else
                                excel.createHeaders(col, j + 7, dtHeader.Rows[i].ItemArray[0].ToString() + "%", excel.ColName(col, j + 7), excel.ColName(col, j + 7 + 1), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                        }

                        excel.createHeaders(col, dtCount - 1, "Total", excel.ColName(col, dtCount - 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                        col++;
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {

                            if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                            {
                                if (i == 0 || i == 1)
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                                else if (i > 5 && i < dtCount)
                                {
                                    if ((i % 2) == 0)
                                        excel.createHeaders(col, i + 1, "Amt", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    else
                                        excel.createHeaders(col, i + 1, "Tax", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                }
                                else
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                            else
                            {
                                if (i == 0 || i == 1)
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                                else if (i > 5 && i < dtCount)
                                {
                                    if ((i % 2) == 0)
                                        excel.createHeaders(col, i + 1, "Amt", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    else
                                        excel.createHeaders(col, i + 1, "Tax", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                }
                                else
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);

                            }
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {

                                if (rdSummary.Checked == true || rdDaySummary.Checked == true)
                                {

                                    if (i == 0)
                                    {
                                        if (dt.Rows[j].ItemArray[1].ToString().Trim() == "-1")
                                        {
                                            excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[0].ToString(), excel.ColName(j + col + 1, 1), excel.ColName(j + col + 1, dtCount), dtCount, Color.White, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                                        }
                                        else
                                        {
                                            if (dt.Rows[j].ItemArray[0].ToString().Trim() != "") excel.addData(j + col + 1, i + 1, Convert.ToDateTime(dt.Rows[j].ItemArray[i].ToString()).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                            else excel.addData(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Left, false);
                                        }
                                    }
                                    else
                                        if (i == 1)
                                        {
                                            if (dt.Rows[j].ItemArray[1].ToString().Trim() != "-1")
                                            {
                                                if (dt.Rows[j].ItemArray[1].ToString().Trim() != "Total" && dt.Rows[j].ItemArray[1].ToString().Trim() != "Grand Total")
                                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                                                else
                                                    excel.addData(j + col + 1, i + 1, "     " + dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, true);
                                            }
                                        }
                                        else if (i == 2)
                                        {
                                            if (dt.Rows[j].ItemArray[1].ToString().Trim() == "Grand Total" || dt.Rows[j].ItemArray[1].ToString().Trim() == "Total")
                                            {
                                                if (dt.Rows[j].ItemArray[1].ToString().Trim() != "-1")
                                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                            }
                                            else
                                            {
                                                if (dt.Rows[j].ItemArray[1].ToString().Trim() != "-1")
                                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                            }
                                        }
                                        else
                                        {
                                            if (dt.Rows[j].ItemArray[1].ToString().Trim() == "Grand Total" || dt.Rows[j].ItemArray[1].ToString().Trim() == "Total")
                                            {
                                                if (dt.Rows[j].ItemArray[1].ToString().Trim() != "-1")
                                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                            }
                                            else
                                            {
                                                if (dt.Rows[j].ItemArray[1].ToString().Trim() != "-1")
                                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                            }
                                        }
                                }
                                else
                                {
                                    if (i == 1)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                    else if (i == 0)
                                    {
                                        if (rdDaySummary.Checked == true || rdSummary.Checked == true)
                                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                    }
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                }

                            }
                        }
                        col++;
                        excel.CompleteDoc("");
                    }
                }
                else
                    OMMessageBox.Show("Select Atleast one Party.....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            finally
            {

            }


        }



        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void BtnPartyShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTToDate.Value < DTPFromDate.Value)
                {
                    OMMessageBox.Show("To Date cannot be less than From Date ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    DTPFromDate.Focus();
                    BtnExport.Visible = false;
                }
                else
                {
                    BindGridParty();
                    strLedgerNo = "";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridParty()
        {
            dtParty = new DataTable();
            string str = "SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName,'false' as chk " +
                         " FROM MLedger INNER JOIN " +
                         " TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN " +
                         " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                         " WHERE (TVoucherDetails.VoucherSrno = 1) AND TVoucherEntry.VoucherTypeCode = " + VchCode + " AND" +
                         "(TVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "')" +
                         " AND (TVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "')";

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

        private void chkPartySelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvParty.Rows.Count; i++)
            {
                gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
            }
        }
    }
}
