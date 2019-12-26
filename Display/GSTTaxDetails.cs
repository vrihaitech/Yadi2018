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
    public partial class GSTTaxDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VchCode;
        public int voucherno;

        public static string LedgName, RptTitle;
        public static int Type;
        public GSTTaxDetails()
        {
            InitializeComponent();
        }
        public GSTTaxDetails(long VchTypeCode)
        {
            InitializeComponent();
            if (VchTypeCode == VchType.Sales)
            {
                VchCode = VchType.Sales;
                this.Text = "Sales Tax Details";
            }
            else if (VchTypeCode == VchType.Purchase)
            {
                VchCode = VchType.Purchase;
                this.Text = "Purchase Tax Details";
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

            rdType_CheckedChanged(sender, new EventArgs());
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
                rdSales.Focus();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = true;
                DataTable dt = new DataTable();
                DataTable dtHeader = new DataTable();
                DataSet ds;
                string str = "", ReportName = "", strDisc = "", strChrg = "";
                long RndOff;
                int dtCount = 0;
                if (rdSales.Checked == true)
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
                if (rdSales.Checked == true)
                {
                    if (VchCode == VchType.Sales) ReportName = "Sale Tax Details Billwise Percentwise";
                    else ReportName = "Purchase Tax Details Billwise Percentwise";

                    if (chkShowVatNo.Checked == false)
                    {
                        str = "Exec GetTaxDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + "," + GroupType.SGST + "";
                    }
                    else
                    {
                        flag = false;
                        str = "Exec GetTaxDetailsVatNoWise '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + "," + GroupType.VAT + "";
                    }
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
                        dtCount = dt.Columns.Count; //- 2; PartyName = "Party";
                    }
                }
                else if (rdPurchase.Checked == true)
                {
                    if (VchCode == VchType.Sales) ReportName = "Sales Tax Details Datewise Summary";
                    else ReportName = "Purchase Tax Details Datewise Summary";
                    str = "Exec GetTaxDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + "," + GroupType.VAT + "";
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
                        dt.Columns[1].ColumnName = "TotalBills";
                        dtCount = dt.Columns.Count;
                    }

                }
                //else if (rdMonthSummary.Checked == true)
                //{
                //    if (VchCode == VchType.Sales) ReportName = "Sales Monthly Tax Details";
                //    else ReportName = "Purchase Monthly Tax Details";
                //    str = "Exec GetTaxDetailsMonthy '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + "," + GroupType.VAT + "";
                //    ds = ObjDset.FillDset("New", str, CommonFunctions.ConStr);
                //    if (ds.Tables.Count <= 1)
                //    {
                //        MessageBox.Show("No Data Found", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //    else
                //    {
                //        dtHeader = ds.Tables[0];
                //        dt = ds.Tables[1];
                //        dt.Columns[1].ColumnName = "TotalBills";
                //        dtCount = dt.Columns.Count;   //- 3; PartyName = "Month";
                //    }
                //}
                //else if (rdQuarterSummary.Checked == true)
                //{
                //    if (VchCode == VchType.Sales) ReportName = "Sales Quarterly Tax Details";
                //    else ReportName = "Purchase Quarterly Tax Details";
                //    str = "Exec GetTaxDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',3," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + "," + GroupType.VAT + "";
                //    ds = ObjDset.FillDset("New", str, CommonFunctions.ConStr);
                //    if (ds.Tables.Count <= 1)
                //    {
                //        MessageBox.Show("No Data Found", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        return;
                //    }
                //    else
                //    {
                //        dtHeader = ds.Tables[0];
                //        dt = ds.Tables[1];
                //        dt.Columns[1].ColumnName = "TotalBills";
                //        dtCount = dt.Columns.Count;
                //    }
                //}
                else
                {
                    OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                if (str != "" && flag == true)
                {

                    int col = 1;
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
                    if (rdSales.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 7), 7, Color.AliceBlue , true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                     
                    else
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 6), 6, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    if (dtHeader.Rows.Count > 0)
                    {
                        if (rdSales.Checked == true)
                            excel.createHeaders(col, 8, "Value Added Tax", excel.ColName(col, 8), excel.ColName(col, dtCount - 2), dtCount - 3 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                           //  excel.cre

                        else
                            excel.createHeaders(col, 7, "Value Added Tax", excel.ColName(col, 7), excel.ColName(col, dtCount - 2), dtCount - 3 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    }
                    excel.createHeaders(col, dtCount - 2 + 1, "", excel.ColName(col, dtCount - 2 + 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    col++;
                    if (rdSales.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 7), 7, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 6),6, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    for (int i = 0, j = 0; i < dtHeader.Rows.Count; i++, j = j + 2)
                    {
                        if (rdSales.Checked == true)
                            excel.createHeaders(col, j +8, dtHeader.Rows[i].ItemArray[0].ToString() + "%", excel.ColName(col, j + 8), excel.ColName(col, j + 9 + 1), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                        else
                            excel.createHeaders(col, j + 7, dtHeader.Rows[i].ItemArray[0].ToString() + "%", excel.ColName(col, j + 7), excel.ColName(col, j + 8 + 1), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    }

                    excel.createHeaders(col, dtCount - 1, "Total", excel.ColName(col, dtCount - 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (rdSales.Checked == true)
                        {
                            if (i == 0 || i == 1)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                            else if (i > 6 && i < dtCount)
                            {
                                if ((i % 2) == 1)
                                    excel.createHeaders(col, i + 1, "Amt", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                else
                                    excel.createHeaders(col, i + 1, "Tax", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                            else if (i == 2)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 35, Color.Black, 12, CreateExcel.ExAlign.Left);
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
                            if (rdSales.Checked == true)
                            {
                                if (i == 1)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                else if (i == 0)
                                {
                                    if (rdPurchase.Checked == true || rdSales.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                    //if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                    //    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                }
                                else if (i == 2)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Left, false);
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            }
                            else
                            {
                                if (i == 1)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                else if (i == 0)
                                {
                                    if (rdPurchase.Checked == true || rdSales.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                    //if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                    //    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                }
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            }

                        }
                    }
                    col++;
                    excel.CompleteDoc("");

                }
                else
                {

                    int col = 1;
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
                    if (rdSales.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 8), 8, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    if (dtHeader.Rows.Count > 0)
                    {
                        if (rdSales.Checked == true)
                            excel.createHeaders(col, 9, "Value Added Tax", excel.ColName(col, 9), excel.ColName(col, dtCount - 2), dtCount - 3 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    }
                    excel.createHeaders(col, dtCount - 2 + 1, "", excel.ColName(col, dtCount - 2 + 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    col++;
                    if (rdSales.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 8), 8, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    for (int i = 0, j = 0; i < dtHeader.Rows.Count; i++, j = j + 2)
                    {
                        if (rdSales.Checked == true)
                            excel.createHeaders(col, j + 9, dtHeader.Rows[i].ItemArray[0].ToString() + "%", excel.ColName(col, j + 9), excel.ColName(col, j + 9 + 1), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    }

                    excel.createHeaders(col, dtCount - 1, "Total", excel.ColName(col, dtCount - 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (rdSales.Checked == true)
                        {
                            if (i == 0 || i == 1)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                            else if (i > 7 && i < dtCount)
                            {
                                if ((i % 2) == 0)
                                    excel.createHeaders(col, i + 1, "Amt", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                else
                                    excel.createHeaders(col, i + 1, "Tax", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                            else if (i == 2)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 35, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else if (i == 3)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true,20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        }
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (rdSales.Checked == true)
                            {
                                if (i == 1)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                else if (i == 0)
                                {
                                    if (rdPurchase.Checked == true || rdSales.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                    //if (rdMonthSummary.Checked == true || rdQuarterSummary.Checked == true)
                                    //    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                }
                                else if (i == 2 || i == 3)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Left, false);
                                else
                                {
                                    if (dt.Rows[j].ItemArray[0].ToString().Trim() == "")
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                }
                            }
                        }
                    }
                    col++;
                    excel.CompleteDoc("");
                }
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

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void rdType_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSales.Checked == true)
                pnlVatNo.Enabled = true;
            else
                pnlVatNo.Enabled = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                string[] ReportSession;
                Form NewF = null;
                if (rdSales.Checked == true && rdForm3B.Checked == true)
                {
                    ReportSession = new string[3];
                    // ReportSession[0] = "0";

                    //ReportSession[2] = DBGetVal.FirmNo.ToString();
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = "15";
                    //ReportSession[5] = tabPage1.Text + " Details";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTFORM3B.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else if (rdPurchase.Checked == true && rdForm3B.Checked == true)
                {
                    ReportSession = new string[3];
                    // ReportSession[0] = "0";

                    //ReportSession[2] = DBGetVal.FirmNo.ToString();
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = "9";
                    //ReportSession[5] = tabPage1.Text + " Details";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTFORM3B.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rdSales.Checked == true && rdGSTR1.Checked == true)
                {
                    ReportSession = new string[3];
                    // ReportSession[0] = "0";

                    //ReportSession[2] = DBGetVal.FirmNo.ToString();
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = "15";
                    //ReportSession[5] = tabPage1.Text + " Details";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTR1.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rdPurchase.Checked == true && rdGSTR1.Checked == true)
                {
                    ReportSession = new string[3];
                    // ReportSession[0] = "0";

                    //ReportSession[2] = DBGetVal.FirmNo.ToString();
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = "9";
                    //ReportSession[5] = tabPage1.Text + " Details";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTR1.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                
                //string[] ReportSession;
                //Form NewF = null;
                //if (rdSummary.Checked == true)
                //{
                //    ReportSession = new string[3];
                //    // ReportSession[0] = "0";

                //    //ReportSession[2] = DBGetVal.FirmNo.ToString();
                //    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                //    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                //    ReportSession[2] = "15";
                //    //ReportSession[5] = tabPage1.Text + " Details";
                //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //        NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                //    else
                //        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTFORM3B.rpt", CommonFunctions.ReportPath), ReportSession);
                //    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                //}
                //if (rdDaySummary.Checked == true)
                //{
                //    ReportSession = new string[3];
                //    // ReportSession[0] = "0";

                //    //ReportSession[2] = DBGetVal.FirmNo.ToString();
                //    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                //    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                //    ReportSession[2] = "9";
                //    //ReportSession[5] = tabPage1.Text + " Details";
                //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                //        NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                //    else
                //        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTFORM3B.rpt", CommonFunctions.ReportPath), ReportSession);
                //    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                //}
                
              
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
