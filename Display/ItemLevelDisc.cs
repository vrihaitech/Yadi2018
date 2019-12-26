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
    public partial class ItemLevelDisc : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();


        public ItemLevelDisc()
        {
            InitializeComponent();
        }

        private void ItemLevelDiscount1_Load(object sender, EventArgs e)
        {
           
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
           
           
        }
       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
               
                    string[] ReportSession;
                    ReportSession = new string[4];
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = DBGetVal.FirmNo.ToString();
                    if (rdBillwise.Checked == true)
                        ReportSession[3] = "0";
                    else
                        ReportSession[3] = "1";
                    Form NewF = null;
                    if (rdBillwise.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptItemLevelDiscount(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptItemLevelDiscount.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else if (rdItemwise.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptItemwiseItemLevelDiscount(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptItemwiseItemLevelDiscount.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else if (rdBrandwise.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.RptBrandwiseItemwiseItemLevelDiscount(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptBrandwiseItemwiseItemLevelDiscount.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    
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

        private void DTPFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                DTToDate.Focus();
            }
        }

        private void DTToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnPrint.Focus();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string str = "", ReportName = "", BillNo = "", GrpName = "",ItemName = "";
                int dtCount = 0; int Temp = 0, ExtraRow = 0, ItemRowNo = 0, GrpRowNo = 0;
                double TotAmt = 0, GrTotAmt = 0, TotDiscAmt = 0, GrTotDiscAmt = 0;
               
                if (rdBillwise.Checked == true)
                {
                    str = "Exec GetItemLevelDiscount '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + DBGetVal.FirmNo + ",0";
                    dt = ObjFunction.GetDataView(str).Table;
                    dt.Columns.RemoveAt(8);
                    dtCount = dt.Columns.Count - 2;
                    ReportName = "Item Level Discount (Billwise)";
                }
                else if (rdItemwise.Checked == true)
                {
                    str = "Exec GetItemLevelDiscount '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + DBGetVal.FirmNo + ",1";
                    dt = ObjFunction.GetDataView(str).Table;
                    dt.Columns.RemoveAt(8);
                    dtCount = dt.Columns.Count - 1;
                    ReportName = "Item Level Discount (Itemwise)";
                }
                else if (rdBrandwise.Checked == true)
                {
                    str = "Exec GetItemLevelDiscount '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + DBGetVal.FirmNo + ",1";
                    dt = ObjFunction.GetDataView(str).Table;
                    dtCount = dt.Columns.Count - 2;
                    ReportName = "Item Level Discount (Brandwise)";
                }
                //PartyName = "Party";
                if (dt.Rows.Count > 0)
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
                    excel.createHeaders(col, 1, "Date :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;
                    excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    if (rdBillwise.Checked == true)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i != 1 && i != 2)
                            {                                
                                if (i == 0)
                                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);                                   
                                else
                                    excel.createHeaders(col, i + 1-2, dt.Columns[i].ColumnName, excel.ColName(col, i + 1 - 2), excel.ColName(col, i + 1 - 2), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                
                            }
                        }
                       
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (BillNo != dt.Rows[j].ItemArray[1].ToString())
                            {
                                excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "BillNo :" + dt.Rows[j].ItemArray[1].ToString() + "  Date : " + Convert.ToDateTime(dt.Rows[j].ItemArray[2]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1 + Temp - ExtraRow, 1), excel.ColName(j + col + 1 + Temp - ExtraRow, dtCount), dtCount, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                //excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, "BillNo :" + dt.Rows[j].ItemArray[1].ToString() + "  Date : " + Convert.ToDateTime(dt.Rows[j].ItemArray[2]).ToString("dd-MMM-yyyy"), excel.ColName(j + col + 1 + Temp - ExtraRow, 1), excel.ColName(j + col + 1 + Temp - ExtraRow, dtCount), dtCount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                BillNo = dt.Rows[j].ItemArray[1].ToString(); ItemRowNo = j + col + 1 + Temp - ExtraRow;

                                Temp++;
                            }

                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (i != 1 && i != 2)
                                {
                                    if (i == 0)
                                        excel.addData(j + col + 1 + Temp, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                                    else
                                        excel.addData(j + col + 1 + Temp, i + 1 - 2, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1 - 2), excel.ColName(j + col + 1, i + 1 - 2), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                    
                                }
                            }
                            TotAmt = TotAmt + Convert.ToDouble(dt.Rows[j].ItemArray[5].ToString());
                            TotDiscAmt = TotDiscAmt + Convert.ToDouble(dt.Rows[j].ItemArray[7].ToString());
                            GrTotAmt = GrTotAmt + Convert.ToDouble(dt.Rows[j].ItemArray[5].ToString());
                            GrTotDiscAmt = GrTotDiscAmt + Convert.ToDouble(dt.Rows[j].ItemArray[7].ToString());

                            if (j < dt.Rows.Count - 1 || j == dt.Rows.Count - 1)
                            {
                                if (j < dt.Rows.Count - 1)
                                {
                                    if (BillNo != dt.Rows[j + 1].ItemArray[1].ToString())
                                    {

                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "Total", excel.ColName(j + col + 2 + Temp - ExtraRow, 1), excel.ColName(j + col + 2 + Temp - ExtraRow, 3), 3, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 4, TotAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp - ExtraRow, 4), excel.ColName(j + col + 2 + Temp - ExtraRow, 4), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 5, "", excel.ColName(j + col + 2 + Temp - ExtraRow, 5), excel.ColName(j + col + 2 + Temp - ExtraRow, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 6, TotDiscAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp - ExtraRow, 6), excel.ColName(j + col + 2 + Temp - ExtraRow, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);

                                        Temp++;
                                        TotAmt = 0; TotDiscAmt = 0;

                                    }
                                }

                                else if (j == dt.Rows.Count - 1)
                                {

                                    excel.createHeaders(j + col + 2 + Temp, 1, " Total", excel.ColName(j + col + 2 + Temp, 1), excel.ColName(j + col + 2 + Temp, 3), 3, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);

                                    excel.createHeaders(j + col + 2 + Temp, 4, TotAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp, 4), excel.ColName(j + col + 2 + Temp, 4), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    excel.createHeaders(j + col + 2 + Temp, 5, "", excel.ColName(j + col + 2 + Temp, 5), excel.ColName(j + col + 2 + Temp, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(j + col + 2 + Temp, 6, TotDiscAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp, 6), excel.ColName(j + col + 2 + Temp, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    TotAmt = 0; TotDiscAmt = 0;
                                    Temp++;
                                }
                            }
                        }
                        col++;
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 1, "GrandTotal", excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 1), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 3), 3, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 4, GrTotAmt.ToString("0.00"), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 4), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 4), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow,5, "", excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 5), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 6, GrTotDiscAmt.ToString("0.00"), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 6), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                       
                        
                    }
                    if (rdItemwise.Checked == true)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i != 0)
                            {
                                if (i == 2)
                                    excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                else
                                    excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                        }
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (ItemName != dt.Rows[j].ItemArray[0].ToString())
                            {
                                excel.createHeaders(j + col + 1 + Temp - ExtraRow, 2, "           " + dt.Rows[j].ItemArray[0].ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, 1), excel.ColName(j + col + 1 + Temp - ExtraRow, dtCount), dtCount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                ItemName = dt.Rows[j].ItemArray[0].ToString(); ItemRowNo = j + col + 1 + Temp - ExtraRow;

                                Temp++;
                            }

                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (i != 0)
                                {
                                    if (i == 1)
                                        excel.addData(j + col + 1 + Temp, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                    if (i == 2)
                                        excel.addData(j + col + 1 + Temp, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                    else
                                        excel.addData(j + col + 1 + Temp, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                }
                            }
                            TotAmt = TotAmt + Convert.ToDouble(dt.Rows[j].ItemArray[5].ToString());
                            TotDiscAmt = TotDiscAmt + Convert.ToDouble(dt.Rows[j].ItemArray[7].ToString());
                            GrTotAmt = GrTotAmt + Convert.ToDouble(dt.Rows[j].ItemArray[5].ToString());
                            GrTotDiscAmt = GrTotDiscAmt + Convert.ToDouble(dt.Rows[j].ItemArray[7].ToString());

                            if (j < dt.Rows.Count - 1 || j == dt.Rows.Count - 1)
                            {
                                if (j < dt.Rows.Count - 1)
                                {
                                    if (ItemName != dt.Rows[j + 1].ItemArray[0].ToString())
                                    {

                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "Total", excel.ColName(j + col + 2 + Temp - ExtraRow, 1), excel.ColName(j + col + 2 + Temp - ExtraRow, 4), 4, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 5, TotAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp - ExtraRow, 5), excel.ColName(j + col + 2 + Temp - ExtraRow, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 6, "", excel.ColName(j + col + 2 + Temp - ExtraRow, 6), excel.ColName(j + col + 2 + Temp - ExtraRow, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 7, TotDiscAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp - ExtraRow, 7), excel.ColName(j + col + 2 + Temp - ExtraRow, 7), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);

                                        Temp++;
                                        TotAmt = 0; TotDiscAmt = 0;

                                    }
                                }

                                else if (j == dt.Rows.Count - 1)
                                {

                                    excel.createHeaders(j + col + 2 + Temp, 1, " Total" , excel.ColName(j + col + 2 + Temp, 1), excel.ColName(j + col + 2 + Temp, 4), 4, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);

                                    excel.createHeaders(j + col + 2 + Temp, 5, TotAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp, 5), excel.ColName(j + col + 2 + Temp, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    excel.createHeaders(j + col + 2 + Temp, 6, "", excel.ColName(j + col + 2 + Temp, 6), excel.ColName(j + col + 2 + Temp, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(j + col + 2 + Temp, 7, TotDiscAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp, 7), excel.ColName(j + col + 2 + Temp, 7), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    TotAmt = 0; TotDiscAmt = 0;
                                    Temp++;
                                }
                            }
                        }
                        col++;
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 1, "GrandTotal", excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 1), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 4), 4, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 5, GrTotAmt.ToString("0.00"), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 5), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 6, "", excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 6), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 7, GrTotDiscAmt.ToString("0.00"), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 7), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 7), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                       
                       
                    }
                    if (rdBrandwise.Checked == true)
                    {
                        for (int i = 0; i < dt.Columns.Count-1; i++)
                        {
                            if (i != 0)
                            {
                                if (i == 2)
                                    excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                else
                                    excel.createHeaders(col, i, dt.Columns[i].ColumnName, excel.ColName(col, i), excel.ColName(col, i), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                            }
                        }
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {

                            if (GrpName != dt.Rows[j].ItemArray[8].ToString())
                            {
                                //excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, "", false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                excel.createHeaders(j + col + 1 + Temp - ExtraRow, 1, dt.Rows[j].ItemArray[8].ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, 1), excel.ColName(j + col + 1 + Temp - ExtraRow, dtCount), dtCount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                               
                                GrpName = dt.Rows[j].ItemArray[8].ToString(); GrpRowNo = j + col + 1 + Temp - ExtraRow; Temp++;
                            }
                           
                            if (ItemName != dt.Rows[j].ItemArray[0].ToString())
                            {
                                excel.createHeaders(j + col + 1 + Temp - ExtraRow, 2, "           " + dt.Rows[j].ItemArray[0].ToString(), excel.ColName(j + col + 1 + Temp - ExtraRow, 1), excel.ColName(j + col + 1 + Temp - ExtraRow, dtCount), dtCount, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Left);
                                ItemName = dt.Rows[j].ItemArray[0].ToString(); ItemRowNo = j + col + 1 + Temp - ExtraRow;
                                
                                Temp++;
                            }



                            
                                for (int i = 0; i < dt.Columns.Count - 1; i++)
                                {
                                    if (i != 0)
                                    {
                                        if (i == 1)
                                            excel.addData(j + col + 1 + Temp, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), Format.NoFloating, 0, CreateExcel.ExAlign.Right, false);
                                        if (i == 2)
                                            excel.addData(j + col + 1 + Temp, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                                        else
                                            excel.addData(j + col + 1 + Temp, i, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i), excel.ColName(j + col + 1, i), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                                    }
                                }
                                
                            

                            TotAmt = TotAmt + Convert.ToDouble(dt.Rows[j].ItemArray[5].ToString());
                            TotDiscAmt = TotDiscAmt + Convert.ToDouble(dt.Rows[j].ItemArray[7].ToString());
                            GrTotAmt = GrTotAmt + Convert.ToDouble(dt.Rows[j].ItemArray[5].ToString());
                            GrTotDiscAmt = GrTotDiscAmt + Convert.ToDouble(dt.Rows[j].ItemArray[7].ToString());

                            if (j < dt.Rows.Count - 1 || j == dt.Rows.Count - 1)
                            {
                                if (j < dt.Rows.Count - 1)
                                {
                                    if (ItemName != dt.Rows[j + 1].ItemArray[0].ToString())
                                    {

                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 1, "Total", excel.ColName(j + col + 2 + Temp - ExtraRow, 1), excel.ColName(j + col + 2 + Temp - ExtraRow, 4), 4, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 5, TotAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp - ExtraRow, 5), excel.ColName(j + col + 2 + Temp - ExtraRow, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 6, "", excel.ColName(j + col + 2 + Temp - ExtraRow, 6), excel.ColName(j + col + 2 + Temp - ExtraRow, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                        excel.createHeaders(j + col + 2 + Temp - ExtraRow, 7, TotDiscAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp - ExtraRow, 7), excel.ColName(j + col + 2 + Temp - ExtraRow, 7), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);

                                        Temp++;
                                        TotAmt = 0; TotDiscAmt = 0;

                                    }
                                }

                                else if (j == dt.Rows.Count - 1)
                                {

                                    excel.createHeaders(j + col + 2 + Temp, 1, " Total" , excel.ColName(j + col + 2 + Temp, 1), excel.ColName(j + col + 2 + Temp, 4), 4, Color.White, true, 12, Color.Black, 10, CreateExcel.ExAlign.Right);
                                    
                                    excel.createHeaders(j + col + 2 + Temp , 5, TotAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp, 5), excel.ColName(j + col + 2 + Temp , 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    excel.createHeaders(j + col + 2 + Temp , 6, "", excel.ColName(j + col + 2 + Temp , 6), excel.ColName(j + col + 2 + Temp , 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                                    excel.createHeaders(j + col + 2 + Temp , 7, TotDiscAmt.ToString("0.00"), excel.ColName(j + col + 2 + Temp, 7), excel.ColName(j + col + 2 + Temp, 7), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                                    TotAmt = 0; TotDiscAmt = 0;
                                    Temp++;
                                }
                            }
                            
                            
                        }
                        
                        col++;
                        //tempCol = col;
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 1, "GrandTotal", excel.ColName(dt.Rows.Count + col +  Temp - ExtraRow, 1), excel.ColName(dt.Rows.Count + col  + Temp - ExtraRow, 4), 4, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 5, GrTotAmt.ToString("0.00"), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 5), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 5), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 6, "", excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 6), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 6), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.createHeaders(dt.Rows.Count + col + Temp - ExtraRow, 7, GrTotDiscAmt.ToString("0.00"), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 7), excel.ColName(dt.Rows.Count + col + Temp - ExtraRow, 7), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                       

                        col++;

                    }

                    excel.CompleteDoc("");

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

      
    }
}
