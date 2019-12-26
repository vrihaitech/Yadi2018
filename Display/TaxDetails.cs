
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
    public partial class TaxDetails : Form
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
        public TaxDetails()
        {
            InitializeComponent();
        }
        public TaxDetails(long VchTypeCode)
        {
            InitializeComponent();
            if (VchTypeCode == VchType.Sales)
            {
                VchCode = VchType.Sales;
                this.Text = "Sales Tax Details";
            }
            else if (VchTypeCode == VchType.RejectionIn)
            {
                VchCode = VchType.RejectionIn;
                this.Text = "Sales Return Tax Details";
            }
            else if (VchTypeCode == VchType.Purchase)
            {
                VchCode = VchType.Purchase;
                this.Text = "Purchase Tax Details";
            }
            else if (VchTypeCode == VchType.RejectionOut)
            {
                VchCode = VchType.RejectionOut;
                this.Text = "Purchase Return Tax Details";
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

            btnNewExcel.Visible = false;
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
                rdBillSummary.Focus();
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
                long RndOff = 0;
                int dtCount = 0;
                if ((rdBillSummary.Checked == true || rdBillWiseDetails.Checked == true) && VchCode == 15)
                {
                    strDisc += ObjFunction.GetAppSettings(AppSettings.S_Discount1);
                    strChrg += ObjFunction.GetAppSettings(AppSettings.S_Charges1);
                    RndOff = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RoundOfAcc));
                }
                else if (rdBillSummary.Checked == true || rdBillWiseDetails.Checked == true && VchCode == 9)
                {
                    strDisc += ObjFunction.GetAppSettings(AppSettings.P_Discount1);
                    strChrg += ObjFunction.GetAppSettings(AppSettings.P_Charges1);
                    RndOff = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RoundOfAcc));
                }

                if (rdBillSummary.Checked == true)
                {
                    if (VchCode == VchType.Sales) ReportName = "Sale Tax Details Billwise Summary  Percentwise";
                    else if (VchCode == VchType.RejectionIn) ReportName = "Sales Return Tax Details Billwise Percentwise";
                    else if (VchCode == VchType.Purchase) ReportName = "Purchase Tax Details Billwise Percentwise";
                    else if (VchCode == VchType.RejectionOut) ReportName = "Purchase Return Tax Details Billwise Percentwise";

                    str = "Exec GetGSTTaxDetailsCess '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',1," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + ", 0";

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
                else if (rdBillWiseDetails.Checked == true)
                {
                    //if (VchCode == VchType.Sales) ReportName = "Sales  GST Tax Details BillWise Summary";
                    //else if (VchCode == VchType.RejectionIn) ReportName = "Sales Return GST Tax Details Datewise Summary";
                    //else if (VchCode == VchType.Purchase) ReportName = "Purchase GST Tax Details Datewise Summary";
                    //else if (VchCode == VchType.RejectionOut) ReportName = "Purchase Return GST Tax Details Datewise Summary";

                    //str = "Exec GetGSTTaxDetailsCess '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "',2," + VchCode + ",'" + strDisc + "','" + strChrg + "'," + RndOff + ", 32";

                    //ds = ObjDset.FillDset("New", str, CommonFunctions.ConStr);
                    //if (ds.Tables.Count <= 1)
                    //{
                    //    MessageBox.Show("No Data Found", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    //else
                    //{
                    //    dtHeader = ds.Tables[0];
                    //    dt = ds.Tables[1];
                    //    //dt.Columns[1].ColumnName = "TotalBills";
                    //    dtCount = dt.Columns.Count;
                    //}

                    btnExportNew_Click(sender, e);

                }

                else if (rdHSNCodeSummary.Checked == true)
                {
                    if (VchCode == VchType.Sales) ReportName = "Sales Monthly HSNCode Wise GST Tax Details";
                    else if (VchCode == VchType.RejectionIn) ReportName = "Sales Return Monthly GST Tax Details";
                    else if (VchCode == VchType.Purchase) ReportName = "Purchase Monthly HSNCode Wise GST Tax Details";
                    else if (VchCode == VchType.RejectionOut) ReportName = "Purchase Return Monthly GST Tax Details";

                    str = "exec GetGSTTaxDetailsHSNCodeWise '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VchCode + " ";
                    ds = ObjDset.FillDset("New", str, CommonFunctions.ConStr);
                    if (ds.Tables.Count <= 1)
                    {

                        dtHeader = ds.Tables[0];
                        dt = ds.Tables[0];
                        //  dt.Columns[1].ColumnName = "TotalBills";
                        dtCount = dt.Columns.Count;   //- 3; PartyName = "Month";
                    }
                }

                else
                {
                    if( (rdForm3B.Checked == true)&& (rdFormB2BPayType.Checked == true))
                    {
                          OMMessageBox.Show("Only Select Print Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                    else
                    {
                        OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                    return;
                }
                if ((str != "" && flag == true) && (rdBillWiseDetails.Checked != true))
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
                    if (rdBillSummary.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 8), 8, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (rdBillWiseDetails.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 9), 9, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    if (rdBillSummary.Checked == true || rdBillWiseDetails.Checked == true)
                    {
                        if (dtHeader.Rows.Count > 0)
                        {
                            if (rdBillSummary.Checked == true || rdBillWiseDetails.Checked == true)
                                excel.createHeaders(col, 9, "Goods and Services Tax ", excel.ColName(col, 9), excel.ColName(col, dtCount - 2), dtCount - 5 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                            else
                                excel.createHeaders(col, 7, "Goods and Services Tax ", excel.ColName(col, 7), excel.ColName(col, dtCount - 2), dtCount - 5 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                        }
                        excel.createHeaders(col, dtCount - 2 + 1, "", excel.ColName(col, dtCount - 2 + 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    }
                    col++;
                    if (rdBillSummary.Checked == true || rdBillWiseDetails.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 8), 8, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);

                    else if (rdBillSummary.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 6), 6, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    if (rdBillSummary.Checked == true || rdBillWiseDetails.Checked == true)
                    {
                        for (int i = 0, j = 0; i < dtHeader.Rows.Count; i++, j = j + 5)
                        {
                            if (rdBillSummary.Checked == true || rdBillWiseDetails.Checked == true)
                                excel.createHeaders(col, j + 9, dtHeader.Rows[i].ItemArray[0].ToString() + "%", excel.ColName(col, j + 9), excel.ColName(col, j + 9 + 4), 5, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                        }


                        excel.createHeaders(col, dtCount - 1, "Total", excel.ColName(col, dtCount - 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                        col++;
                    }
                    //=======fill left to right colunm 
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (rdBillSummary.Checked == true)
                        {
                            if (i == 0 || i == 1)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                            else if (i > 6 && i < dtCount)
                            {
                                //if ((i % 2) == 1)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                            else if (i == 2)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 35, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        }
                        else if (rdHSNCodeSummary.Checked == true)
                        {

                            excel.createHeaders(col, i + 1, dtHeader.Columns[i].ColumnName.ToString() + " ", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 25, Color.Black, 12, CreateExcel.ExAlign.Center);

                        }

                        else if (rdBillWiseDetails.Checked == true)
                        {
                            if (i == 0 || i == 1)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                            else if (i > 6 && i < dtCount)
                            {
                                //if ((i % 2) == 1)
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
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
                            else if (i > 8 && i < dtCount)
                            {
                                if ((i % 2) == 0)
                                    excel.createHeaders(col, i + 1, "Amt", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                                else
                                    excel.createHeaders(col, i + 1, "Tax", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                            else
                            {
                                if (i == 2 || i == 3 || i == 8)
                                {
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 25, Color.Black, 12, CreateExcel.ExAlign.Right);
                                }
                                else
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                        }
                        //-==========for exec data fill one single column //=========MAin Fill)
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (rdBillSummary.Checked == true)
                            {
                                if (i == 1)
                                {
                                    if (VchCode == 9)
                                    {
                                        string billno = ObjQry.ReturnString("select reference from tvoucherentry where vouchertypecode=9 and voucheruserno=" + dt.Rows[j].ItemArray[1].ToString() + "  and TVoucherEntry.VoucherDate='" + dt.Rows[j].ItemArray[0].ToString() + "'", CommonFunctions.ConStr);
                                        excel.addData(j + col + 1, i + 1, billno, excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);

                                    }
                                    else
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);

                                }
                                else if (i == 0)
                                {
                                    if (rdBillWiseDetails.Checked == true || rdBillSummary.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                }
                                else if (i == 2)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Left, false);
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            }
                            else if (rdHSNCodeSummary.Checked == true)
                            {
                                if (i == 1)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                else if (i == 0)
                                {
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                                }
                                else if (i == 2)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Left, false);
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            }

                            else if (rdBillWiseDetails.Checked == true)
                            {
                                //  if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsTaxTypewiseBillNo)) == false)
                                // {
                                if (i == 1)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                else if (i == 0)
                                {
                                    if (rdBillWiseDetails.Checked == true || rdBillSummary.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                }
                                else if (i == 2)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Left, false);
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                                //}
                                //else
                                //{
                                //if (i == 1)
                                //{
                                //    string str1 = Convert.ToDateTime(dt.Rows[j].ItemArray[i - 1].ToString()).Day.ToString();
                                //    str1 = str1 +""+ Convert.ToDateTime(dt.Rows[j].ItemArray[i - 1].ToString()).Month.ToString();
                                //    str1 = str1 + "" + Convert.ToDateTime(dt.Rows[j].ItemArray[i - 1].ToString()).Year.ToString();
                                //    str1 = Convert.ToDateTime(dt.Rows[j].ItemArray[i - 1].ToStringddMMyyyy");


                                //}
                                //        string str1 =Convert.ToDateTime(dt.Rows[j].ItemArray[i-1].ToString()).ToString("ddMMyyyy") + "-" + dt.Rows[j].ItemArray[i].ToString();
                                //        excel.addData(j + col + 1, i + 1, str1, excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                //    }
                                //    else if (i == 0)
                                //    {
                                //        if (rdBillWiseDetails.Checked == true || rdBillSummary.Checked == true)
                                //            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                //    }
                                //    else if (i == 2)
                                //    {

                                //        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Left, false);

                                //    }
                                //    else
                                //        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                //}
                            }

                            else
                            {
                                if (i == 1)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                else if (i == 8)
                                {
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                                }
                                else if (i == 0)
                                {
                                    if (rdBillWiseDetails.Checked == true || rdBillSummary.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                    if (rdHSNCodeSummary.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                }
                                else
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                //===gstNo 

                            }

                        }
                    }
                    col++;
                    excel.CompleteDoc("");

                }
                else if ((rdBillWiseDetails.Checked != true))
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
                    if (rdBillSummary.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 8), 8, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    if (dtHeader.Rows.Count > 0)
                    {
                        if (rdBillSummary.Checked == true)
                            excel.createHeaders(col, 9, "Value Added Tax", excel.ColName(col, 9), excel.ColName(col, dtCount - 2), dtCount - 3 - 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    }
                    excel.createHeaders(col, dtCount - 2 + 1, "", excel.ColName(col, dtCount - 2 + 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    col++;
                    if (rdBillSummary.Checked == true)
                        excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 8), 8, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    for (int i = 0, j = 0; i < dtHeader.Rows.Count; i++, j = j + 2)
                    {
                        if (rdBillSummary.Checked == true)
                            excel.createHeaders(col, j + 9, dtHeader.Rows[i].ItemArray[0].ToString() + "%", excel.ColName(col, j + 9), excel.ColName(col, j + 9 + 1), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    }

                    excel.createHeaders(col, dtCount - 1, "Total", excel.ColName(col, dtCount - 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (rdBillSummary.Checked == true)
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
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            else
                                excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        }
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (rdBillSummary.Checked == true)
                            {
                                if (i == 1)
                                    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                                else if (i == 0)
                                {
                                    if (rdBillWiseDetails.Checked == true || rdBillSummary.Checked == true)
                                        excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
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
        }

        private void btnExportNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdBillWiseDetails.Checked == true)
                {
                    DataTable dt = new DataTable();
                    string str;
                    str =  "  SELECT     TVoucherEntry.PkVoucherNo, TVoucherEntry.StateCode AS Code, MState.StateName AS State, MLedgerDetails.GSTNO, TVoucherEntry.VoucherUserNo AS BillNo, " +
                           " TVoucherEntry.VoucherDate AS Date, MLedger.LedgerName, TVoucherEntry.BilledAmount AS BillAmount,  SUM(TStock.DiscAmount + TStock.DiscRupees) AS Discount," +
                           "  0 AS Charge, 0 AS Roundoff, SUM(TStock.Quantity)AS Quantity, (SELECT     CASE WHEN tStock.SGSTPercentage = 2.5 THEN 'Tax Rate 2.5%' WHEN tStock.SGSTPercentage = 6 THEN 'Tax Rate 6%' WHEN tStock.SGSTPercentage =" +
                           " 9 THEN 'Tax Rate 9%' WHEN tStock.SGSTPercentage = 14 THEN 'Tax Rate 14%' WHEN tStock.SGSTPercentage = 1.5 THEN 'Tax Rate 3%'  ELSE 'Tax Rate 0%' END AS Expr1) AS TaxPercentage, " +
                           "                   SUM(TStock.NetAmount)AS Amount, SUM(TStock.SGSTAmount) AS SGST, SUM(TStock.CGSTAmount) AS CGST, 0 AS IGST,MPayType.PayTypeName " +
                           " FROM         TVoucherEntry INNER JOIN                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                           "                    MState ON MLedger.StateCode = MState.StateCode INNER JOIN                      MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo" +
                          " INNER JOIN                      TStock ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                          "                     MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE     (TVoucherEntry.VoucherTypeCode = " + VchCode + ") and  VoucherDate>='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' " +
                        " and TVoucherEntry.isCancel='false' and MLedger.StateCode  in 	  (select Statecode from mfirm) group by PkVoucherNo,tStock.SGSTPercentage, TStock.CGSTPercentage,VoucherUserNo,VoucherDate,TVoucherEntry.StateCode,StateName,GSTNO,LedgerName,BilledAmount,MPayType.PayTypeName " +
                        " Union all  SELECT     TVoucherEntry.PkVoucherNo, TVoucherEntry.StateCode AS Code, MState.StateName AS State, MLedgerDetails.GSTNO, TVoucherEntry.VoucherUserNo AS BillNo, " +
                           " TVoucherEntry.VoucherDate AS Date, MLedger.LedgerName, TVoucherEntry.BilledAmount AS BillAmount,  SUM(TStock.DiscAmount + TStock.DiscRupees) AS Discount," +
                           "  0 AS Charge, 0 AS Roundoff, SUM(TStock.Quantity)AS Quantity, (SELECT     CASE WHEN tStock.IGSTPercentage = 5 THEN 'Tax Rate 5%' WHEN tStock.IGSTPercentage = 12 THEN 'Tax Rate 12%' WHEN tStock.IGSTPercentage =" +
                           " 18 THEN 'Tax Rate 18%' WHEN tStock.IGSTPercentage = 28 THEN 'Tax Rate 28%' WHEN tStock.IGSTPercentage = 3 THEN 'Tax Rate 3%'  ELSE 'Tax Rate 0%' END AS Expr1) AS TaxPercentage, " +
                           "                   SUM(TStock.NetAmount)AS Amount, 0 AS SGST, 0  AS CGST, SUM(TStock.IGSTAmount) AS IGST, MPayType.PayTypeName " +
                           " FROM         TVoucherEntry INNER JOIN                      MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                           "                    MState ON MLedger.StateCode = MState.StateCode INNER JOIN                      MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo" +
                          " INNER JOIN                      TStock ON TStock.FKVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                          "                     MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE     (TVoucherEntry.VoucherTypeCode = " + VchCode + ") and  VoucherDate>='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "' " +
                        " and TVoucherEntry.isCancel='false' and MLedger.StateCode not in 	  (select Statecode from mfirm) group by PkVoucherNo,tStock.IGSTPercentage,VoucherUserNo,VoucherDate,TVoucherEntry.StateCode,StateName,GSTNO,LedgerName,BilledAmount,MPayType.PayTypeName " +

                        " order by VoucherDate,VoucherUserNo";
                    dt = ObjFunction.GetDataView(str).Table;
                    // int bb= dt.Columns.Count;
                    int dtCount = dt.Columns.Count;
                    int col = 1;
                    string ReportName = "";

                    if (VchCode == VchType.Sales) ReportName = "Sales Tax GST Details Billwise Percentwise";
                    else if (VchCode == VchType.RejectionIn) ReportName = "Sales Return Tax Details Billwise Percentwise";
                    else if (VchCode == VchType.Purchase) ReportName = "Purchase Tax Details Billwise Percentwise";
                    else if (VchCode == VchType.RejectionOut) ReportName = "Purchase Return Tax Details Billwise Percentwise";


                    CreateExcel excel = new CreateExcel();
                    //Company Name Header
                    //excel.createDoc();
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
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        if (i == 3 || i == 6)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
                        else
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);

                    }
                    int pknew, pkold, browno = 7, cnt = 0; double roff = 0.00, disc = 0.00;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i > 0)
                        {
                            pkold = Convert.ToInt32(dt.Rows[i - 1].ItemArray[0].ToString());
                            pknew = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());
                            if (pknew != pkold)
                            {
                                roff = 0.00;
                                roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[16].ToString())) - roff;
                                roff = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[7].ToString()) - roff, 2);
                                disc = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[8].ToString()), 2);
                                // excel.addData(col + 1, 1, dt.Rows[i].ItemArray[0].ToString(), excel.ColName(col + 2, i + 1), excel.ColName(col + 2, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 2, dt.Rows[i].ItemArray[1].ToString(), excel.ColName(col + 3, i + 1), excel.ColName(col + 3, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 3, dt.Rows[i].ItemArray[2].ToString(), excel.ColName(col + 4, i + 1), excel.ColName(col + 4, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 4, dt.Rows[i].ItemArray[3].ToString(), excel.ColName(col + 5, i + 1), excel.ColName(col + 5, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                //  excel.addData(col + 1, 5, dt.Rows[i].ItemArray[4].ToString(), excel.ColName(col + 6, i + 1), excel.ColName(col + 6, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);

                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsTaxTypewiseBillNo)) == false)
                                {
                                    excel.addData(col + 1, 5, dt.Rows[i].ItemArray[4].ToString(), excel.ColName(col + 6, i + 1), excel.ColName(col + 6, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Left, false);
                                }

                                else
                                {
                                    // string billno = Convert.ToString(dt.Rows[i].ItemArray[5].ToString()) + " " + Convert.ToString(dt.Rows[i].ItemArray[4].ToString());
                                    string billno = Convert.ToDateTime(dt.Rows[i].ItemArray[5].ToString()).ToString("yyyy-MMM-dd");
                                    billno = billno + "- " + Convert.ToString(dt.Rows[i].ItemArray[4].ToString());
                                    excel.addData(col + 1, 5, billno, excel.ColName(col + 6, i + 1), excel.ColName(col + 6, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Left, false);

                                }
                                excel.addData(col + 1, 6, dt.Rows[i].ItemArray[5].ToString(), excel.ColName(col + 7, i + 1), excel.ColName(col + 7, i + 1), Format.DDMMYYYY, 0, CreateExcel.ExAlign.Right, false);

                                excel.addData(col + 1, 7, dt.Rows[i].ItemArray[6].ToString(), excel.ColName(col + 8, i + 1), excel.ColName(col + 8, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 8, dt.Rows[i].ItemArray[7].ToString(), excel.ColName(col + 9, i + 1), excel.ColName(col + 9, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 9, dt.Rows[i].ItemArray[8].ToString(), excel.ColName(col + 10, i + 1), excel.ColName(col + 10, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                                excel.addData(col + 1, 10, dt.Rows[i].ItemArray[10].ToString(), excel.ColName(col + 11, i + 1), excel.ColName(col + 11, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 11, roff.ToString(), excel.ColName(col + 12, i + 1), excel.ColName(col + 12, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 12, dt.Rows[i].ItemArray[11].ToString(), excel.ColName(col + 13, i + 1), excel.ColName(col + 13, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 13, dt.Rows[i].ItemArray[12].ToString(), excel.ColName(col + 14, i + 1), excel.ColName(col + 14, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 14, dt.Rows[i].ItemArray[13].ToString(), excel.ColName(col + 15, i + 1), excel.ColName(col + 15, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 15, dt.Rows[i].ItemArray[14].ToString(), excel.ColName(col + 16, i + 1), excel.ColName(col + 16, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 16, dt.Rows[i].ItemArray[15].ToString(), excel.ColName(col + 17, i + 1), excel.ColName(col + 17, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 17, dt.Rows[i].ItemArray[16].ToString(), excel.ColName(col + 18, i + 1), excel.ColName(col + 18, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 18, dt.Rows[i].ItemArray[17].ToString(), excel.ColName(col + 19, i + 1), excel.ColName(col + 19, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                                browno = col + 1;
                                col++;
                                cnt = 1;

                            }
                            else
                            {

                                if (cnt == 2 || cnt == 4 || cnt == 6)
                                {
                                    roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[16].ToString())) - (-roff);
                                    roff = Math.Round((roff) * -1, 2);
                                }
                                else if (cnt == 1 || cnt == 3 || cnt == 5)
                                {
                                    roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[16].ToString())) - (roff);
                                    roff = Math.Round(roff, 2);
                                }
                                disc = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[8].ToString()) + disc, 2);
                                excel.addData(browno, 9, disc.ToString(), excel.ColName(col + 10, i + 1), excel.ColName(col + 10, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(browno, 11, (roff * -1).ToString(), excel.ColName(col + 12, i + 1), excel.ColName(col + 12, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 12, dt.Rows[i].ItemArray[11].ToString(), excel.ColName(col + 13, i + 1), excel.ColName(col + 13, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 13, dt.Rows[i].ItemArray[12].ToString(), excel.ColName(col + 14, i + 1), excel.ColName(col + 14, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 14, dt.Rows[i].ItemArray[13].ToString(), excel.ColName(col + 15, i + 1), excel.ColName(col + 15, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 15, dt.Rows[i].ItemArray[14].ToString(), excel.ColName(col + 16, i + 1), excel.ColName(col + 16, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 16, dt.Rows[i].ItemArray[15].ToString(), excel.ColName(col + 17, i + 1), excel.ColName(col + 17, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                col++;
                                cnt = cnt + 1;

                            }
                        }
                        else
                        {
                            roff = 0.00;
                            roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[16].ToString())) - roff;
                            roff = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[7].ToString()) - roff, 2);
                            disc = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[8].ToString()), 2);
                            // excel.addData(col + 1, 1, dt.Rows[i].ItemArray[0].ToString(), excel.ColName(col + 2, i + 1), excel.ColName(col + 2, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 2, dt.Rows[i].ItemArray[1].ToString(), excel.ColName(col + 3, i + 1), excel.ColName(col + 3, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 3, dt.Rows[i].ItemArray[2].ToString(), excel.ColName(col + 4, i + 1), excel.ColName(col + 4, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 4, dt.Rows[i].ItemArray[3].ToString(), excel.ColName(col + 5, i + 1), excel.ColName(col + 5, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsTaxTypewiseBillNo)) == false)
                            {
                                excel.addData(col + 1, 5, dt.Rows[i].ItemArray[4].ToString(), excel.ColName(col + 6, i + 1), excel.ColName(col + 6, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Left, false);
                            }

                            else {
                                string billno = Convert.ToDateTime(dt.Rows[i].ItemArray[5].ToString()).ToString("yyyy-MMM-dd");
                                billno = billno + "- " + Convert.ToString(dt.Rows[i].ItemArray[4].ToString());
                                excel.addData(col + 1, 5, billno, excel.ColName(col + 6, i + 1), excel.ColName(col + 6, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Left, false);

                            }
                            excel.addData(col + 1, 6, dt.Rows[i].ItemArray[5].ToString(), excel.ColName(col + 7, i + 1), excel.ColName(col + 7, i + 1), Format.DDMMYYYY, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 7, dt.Rows[i].ItemArray[6].ToString(), excel.ColName(col + 8, i + 1), excel.ColName(col + 8, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 8, dt.Rows[i].ItemArray[7].ToString(), excel.ColName(col + 9, i + 1), excel.ColName(col + 9, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 9, dt.Rows[i].ItemArray[8].ToString(), excel.ColName(col + 10, i + 1), excel.ColName(col + 10, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                            excel.addData(col + 1, 10, dt.Rows[i].ItemArray[9].ToString(), excel.ColName(col + 11, i + 1), excel.ColName(col + 11, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 11, roff.ToString(), excel.ColName(col + 12, i + 1), excel.ColName(col + 12, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 12, dt.Rows[i].ItemArray[11].ToString(), excel.ColName(col + 13, i + 1), excel.ColName(col + 13, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 13, dt.Rows[i].ItemArray[12].ToString(), excel.ColName(col + 14, i + 1), excel.ColName(col + 14, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 14, dt.Rows[i].ItemArray[13].ToString(), excel.ColName(col + 15, i + 1), excel.ColName(col + 15, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 15, dt.Rows[i].ItemArray[14].ToString(), excel.ColName(col + 16, i + 1), excel.ColName(col + 16, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 16, dt.Rows[i].ItemArray[15].ToString(), excel.ColName(col + 17, i + 1), excel.ColName(col + 17, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 17, dt.Rows[i].ItemArray[16].ToString(), excel.ColName(col + 18, i + 1), excel.ColName(col + 18, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 18, dt.Rows[i].ItemArray[17].ToString(), excel.ColName(col + 19, i + 1), excel.ColName(col + 19, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                            col++;
                            cnt = 1;                        }
                    }

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNewExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdBillWiseDetails.Checked == true)
                {
                    DataTable dt = new DataTable();
                    string str;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsReverseRateCalc)) == false)//Reverse or forward calculation
                    {
                        str = " SELECT     TVoucherEntry.PkVoucherNo, TVoucherEntry.StateCode as Code , MState.StateName as State, MLedgerDetails.GSTNO As GSTNO, TVoucherEntry.VoucherUserNo as BillNo, TVoucherEntry.VoucherDate as Date,  MLedger.LedgerName,  TVoucherEntry.BilledAmount  as BillAmount " +
                        " ,sum(DiscAmount+DiscRupees) as Discount,0 as Charge ,0 as Roundoff,sum(TStock.Quantity) as Quantity,  " +
                        " (select case when  tStock.SGSTPercentage=2.5 then  'Tax Rate 2.5%' when  tStock.SGSTPercentage=6 then  'Tax Rate 6%' when  tStock.SGSTPercentage=9 then  'Tax Rate 9%' when  tStock.SGSTPercentage=14 then  'Tax Rate 14%' when  tStock.SGSTPercentage=1.5 then  'Tax Rate 3%' else 'Tax Rate 0%' end) as TaxPercentage   , sum(TStock.Amount) as Amount,sum( tStock.SGSTAmount) as SGST ,sum(TStock.CGSTAmount) as CGST " +
                        " FROM       TVoucherEntry INNER JOIN MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN MState ON MLedger.StateCode = MState.StateCode INNER JOIN  MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo inner join TStock on TStock.fkvoucherno=TVoucherEntry.pkvoucherno " +
                        " WHERE     (TVoucherEntry.VoucherTypeCode = " + VchCode + ") and  VoucherDate>='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "'  group by PkVoucherNo,tStock.SGSTPercentage, TStock.CGSTPercentage,VoucherUserNo,VoucherDate,TVoucherEntry.StateCode,StateName,GSTNO,LedgerName,BilledAmount " +
                        " order by VoucherDate,VoucherUserNo";
                    }
                    else
                    {
                        str = " SELECT     TVoucherEntry.PkVoucherNo, TVoucherEntry.StateCode as Code , MState.StateName as State, MLedgerDetails.GSTNO As GSTNO, TVoucherEntry.VoucherUserNo as BillNo, TVoucherEntry.VoucherDate as Date,  MLedger.LedgerName,  TVoucherEntry.BilledAmount  as BillAmount " +
                 " ,sum(DiscAmount+DiscRupees) as Discount,0 as Charge ,0 as Roundoff,sum(TStock.Quantity) as Quantity,  " +
                 " (select case when  tStock.SGSTPercentage=2.5 then  'Tax Rate 2.5%' when  tStock.SGSTPercentage=6 then  'Tax Rate 6%' when  tStock.SGSTPercentage=9 then  'Tax Rate 9%' when  tStock.SGSTPercentage=14 then  'Tax Rate 14%' when  tStock.SGSTPercentage=1.5 then  'Tax Rate 3%' else 'Tax Rate 0%' end) as TaxPercentage   , sum(TStock.NetAmount) as Amount,sum( tStock.SGSTAmount) as SGST ,sum(TStock.CGSTAmount) as CGST " +
                 " FROM       TVoucherEntry INNER JOIN MLedger ON TVoucherEntry.LedgerNo = MLedger.LedgerNo INNER JOIN MState ON MLedger.StateCode = MState.StateCode INNER JOIN  MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo inner join TStock on TStock.fkvoucherno=TVoucherEntry.pkvoucherno " +
                 " WHERE     (TVoucherEntry.VoucherTypeCode = " + VchCode + ") and  VoucherDate>='" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "'  group by PkVoucherNo,tStock.SGSTPercentage, TStock.CGSTPercentage,VoucherUserNo,VoucherDate,TVoucherEntry.StateCode,StateName,GSTNO,LedgerName,BilledAmount " +
                 " order by VoucherDate,VoucherUserNo";
                    }
                    dt = ObjFunction.GetDataView(str).Table;
                    // int bb= dt.Columns.Count;
                    int dtCount = dt.Columns.Count;
                    int col = 1;
                    string ReportName = "";

                    if (VchCode == VchType.Sales) ReportName = "Sales Tax GST Details Billwise Percentwise";
                    else if (VchCode == VchType.RejectionIn) ReportName = "Sales Return Tax Details Billwise Percentwise";
                    else if (VchCode == VchType.Purchase) ReportName = "Purchase Tax Details Billwise Percentwise";
                    else if (VchCode == VchType.RejectionOut) ReportName = "Purchase Return Tax Details Billwise Percentwise";

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
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        if (i == 3 || i == 6)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Center);
                        else
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);

                    }
                    int pknew, pkold, browno = 7, cnt = 0; double roff = 0.00, disc = 0.00;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (i > 0)
                        {
                            pkold = Convert.ToInt32(dt.Rows[i - 1].ItemArray[0].ToString());
                            pknew = Convert.ToInt32(dt.Rows[i].ItemArray[0].ToString());
                            if (pknew != pkold)
                            {
                                roff = 0.00;
                                roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString())) - roff;
                                roff = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[7].ToString()) - roff, 2);
                                disc = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[8].ToString()), 2);
                                // excel.addData(col + 1, 1, dt.Rows[i].ItemArray[0].ToString(), excel.ColName(col + 2, i + 1), excel.ColName(col + 2, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 2, dt.Rows[i].ItemArray[1].ToString(), excel.ColName(col + 3, i + 1), excel.ColName(col + 3, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 3, dt.Rows[i].ItemArray[2].ToString(), excel.ColName(col + 4, i + 1), excel.ColName(col + 4, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 4, dt.Rows[i].ItemArray[3].ToString(), excel.ColName(col + 5, i + 1), excel.ColName(col + 5, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 5, dt.Rows[i].ItemArray[4].ToString(), excel.ColName(col + 6, i + 1), excel.ColName(col + 6, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 6, dt.Rows[i].ItemArray[5].ToString(), excel.ColName(col + 7, i + 1), excel.ColName(col + 7, i + 1), Format.DDMMYYYY, 0, CreateExcel.ExAlign.Right, false);

                                excel.addData(col + 1, 7, dt.Rows[i].ItemArray[6].ToString(), excel.ColName(col + 8, i + 1), excel.ColName(col + 8, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 8, dt.Rows[i].ItemArray[7].ToString(), excel.ColName(col + 9, i + 1), excel.ColName(col + 9, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 9, dt.Rows[i].ItemArray[8].ToString(), excel.ColName(col + 10, i + 1), excel.ColName(col + 10, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                                excel.addData(col + 1, 10, dt.Rows[i].ItemArray[10].ToString(), excel.ColName(col + 11, i + 1), excel.ColName(col + 11, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 11, roff.ToString(), excel.ColName(col + 12, i + 1), excel.ColName(col + 12, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 12, dt.Rows[i].ItemArray[11].ToString(), excel.ColName(col + 13, i + 1), excel.ColName(col + 13, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 13, dt.Rows[i].ItemArray[12].ToString(), excel.ColName(col + 14, i + 1), excel.ColName(col + 14, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 14, dt.Rows[i].ItemArray[13].ToString(), excel.ColName(col + 15, i + 1), excel.ColName(col + 15, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 15, dt.Rows[i].ItemArray[14].ToString(), excel.ColName(col + 16, i + 1), excel.ColName(col + 16, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 16, dt.Rows[i].ItemArray[15].ToString(), excel.ColName(col + 17, i + 1), excel.ColName(col + 17, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                                browno = col + 1;
                                col++;
                                cnt = 1;

                            }
                            else
                            {

                                if (cnt == 2 || cnt == 4 || cnt == 6)
                                {
                                    roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString())) - (-roff);
                                    roff = Math.Round((roff) * -1, 2);
                                }
                                else if (cnt == 1 || cnt == 3 || cnt == 5)
                                {
                                    roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString())) - (roff);
                                    roff = Math.Round(roff, 2);
                                }
                                disc = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[8].ToString()) + disc, 2);
                                excel.addData(browno, 9, disc.ToString(), excel.ColName(col + 10, i + 1), excel.ColName(col + 10, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(browno, 11, (roff * -1).ToString(), excel.ColName(col + 12, i + 1), excel.ColName(col + 12, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 12, dt.Rows[i].ItemArray[11].ToString(), excel.ColName(col + 13, i + 1), excel.ColName(col + 13, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 13, dt.Rows[i].ItemArray[12].ToString(), excel.ColName(col + 14, i + 1), excel.ColName(col + 14, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 14, dt.Rows[i].ItemArray[13].ToString(), excel.ColName(col + 15, i + 1), excel.ColName(col + 15, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 15, dt.Rows[i].ItemArray[14].ToString(), excel.ColName(col + 16, i + 1), excel.ColName(col + 16, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                excel.addData(col + 1, 16, dt.Rows[i].ItemArray[15].ToString(), excel.ColName(col + 17, i + 1), excel.ColName(col + 17, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                col++;
                                cnt = cnt + 1;

                            }
                        }
                        else
                        {
                            roff = 0.00;
                            roff = (Convert.ToDouble(dt.Rows[i].ItemArray[13].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[14].ToString()) + Convert.ToDouble(dt.Rows[i].ItemArray[15].ToString())) - roff;
                            roff = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[7].ToString()) - roff, 2);
                            disc = Math.Round(Convert.ToDouble(dt.Rows[i].ItemArray[8].ToString()), 2);
                            // excel.addData(col + 1, 1, dt.Rows[i].ItemArray[0].ToString(), excel.ColName(col + 2, i + 1), excel.ColName(col + 2, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 2, dt.Rows[i].ItemArray[1].ToString(), excel.ColName(col + 3, i + 1), excel.ColName(col + 3, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 3, dt.Rows[i].ItemArray[2].ToString(), excel.ColName(col + 4, i + 1), excel.ColName(col + 4, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 4, dt.Rows[i].ItemArray[3].ToString(), excel.ColName(col + 5, i + 1), excel.ColName(col + 5, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 5, dt.Rows[i].ItemArray[4].ToString(), excel.ColName(col + 6, i + 1), excel.ColName(col + 6, i + 1), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 6, dt.Rows[i].ItemArray[5].ToString(), excel.ColName(col + 7, i + 1), excel.ColName(col + 7, i + 1), Format.DDMMYYYY, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 7, dt.Rows[i].ItemArray[6].ToString(), excel.ColName(col + 8, i + 1), excel.ColName(col + 8, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 8, dt.Rows[i].ItemArray[7].ToString(), excel.ColName(col + 9, i + 1), excel.ColName(col + 9, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 9, dt.Rows[i].ItemArray[8].ToString(), excel.ColName(col + 10, i + 1), excel.ColName(col + 10, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                            excel.addData(col + 1, 10, dt.Rows[i].ItemArray[9].ToString(), excel.ColName(col + 11, i + 1), excel.ColName(col + 11, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 11, roff.ToString(), excel.ColName(col + 12, i + 1), excel.ColName(col + 12, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 12, dt.Rows[i].ItemArray[11].ToString(), excel.ColName(col + 13, i + 1), excel.ColName(col + 13, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 13, dt.Rows[i].ItemArray[12].ToString(), excel.ColName(col + 14, i + 1), excel.ColName(col + 14, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 14, dt.Rows[i].ItemArray[13].ToString(), excel.ColName(col + 15, i + 1), excel.ColName(col + 15, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 15, dt.Rows[i].ItemArray[14].ToString(), excel.ColName(col + 16, i + 1), excel.ColName(col + 16, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            excel.addData(col + 1, 16, dt.Rows[i].ItemArray[15].ToString(), excel.ColName(col + 17, i + 1), excel.ColName(col + 17, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);

                            col++;
                            cnt = 1;
                        }
                    }

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string[] ReportSession;
            Form NewF = null;
            if (rdForm3B.Checked == true)
            {
                ReportSession = new string[3];
                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                if (VchCode == 15)
                {
                    ReportSession[2] = "15";
                }
                else { ReportSession[2] = "9"; }
                //ReportSession[5] = tabPage1.Text + " Details";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTFORM3B.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else if (rdBillWiseDetails.Checked == true)
            {
                ReportSession = new string[3];
                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                if (VchCode == 15)
                {
                    ReportSession[2] = "15";
                }
                else { ReportSession[2] = "9"; }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTFORMPartyWise.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else if ( rdBillSummary.Checked == true)
            {
                ReportSession = new string[3];
                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                if (VchCode == 15)
                {
                    ReportSession[2] = "15";
                }
                else { ReportSession[2] = "9"; }
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTFORMDetail.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else if ( rdFormB2BPayType.Checked == true)
                {
                //    ReportSession = new string[2];
                //    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                //    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession = new string[3];
            ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
            ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");

            if (VchCode == 15)
            {
                ReportSession[2] = "15";
            }
            else { ReportSession[2] = "9"; }
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptGSTB2BPayTypewise.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

            }

        }
    }
}
