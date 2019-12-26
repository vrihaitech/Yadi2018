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
    public partial class TopSales : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VoucherType;
        public int voucherno;
        
        public static string LedgName, RptTitle;
        public static int Type;

        public TopSales()
        {
            InitializeComponent();
        }
        
        private void TopItemSales_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            txtTopValue.Text = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue);
           
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                BtnExport.Visible = true;
            else
                BtnExport.Visible = false;
        }
       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTopValue.Text.Trim() == "") txtTopValue.Text = "0";
                if (txtTopQty.Text.Trim() == "") txtTopQty.Text = "0";
                if (txtBottomValue.Text.Trim() == "") txtBottomValue.Text = "0";
                if (txtBottomQty.Text.Trim() == "") txtBottomQty.Text = "0";

                if (ObjFunction.CheckNumeric(txtTopValue.Text.Trim()) == true)
                {
                    
                    string[] ReportSession;
                    ReportSession = new string[24];
                    ReportSession[0]= Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1]= Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2]=txtTopValue.Text;
                    ReportSession[3]=(rdItem.Checked==true)? "0" :"1";
                    ReportSession[4]=DBGetVal.FirmNo.ToString();
                    ReportSession[5] = "1";
                    ReportSession[6] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[7] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[8] = txtTopQty.Text;
                    ReportSession[9] = (rdItem.Checked == true) ? "0" : "1";
                    ReportSession[10] = DBGetVal.FirmNo.ToString();
                    ReportSession[11] = "1";
                    ReportSession[12] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[13] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[14] = txtBottomValue.Text;
                    ReportSession[15] = (rdItem.Checked == true) ? "0" : "1";
                    ReportSession[16] = DBGetVal.FirmNo.ToString();
                    ReportSession[17] = "2";
                    ReportSession[18] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[19] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[20] = txtBottomQty.Text;
                    ReportSession[21] = (rdItem.Checked == true) ? "0" : "1";
                    ReportSession[22] = DBGetVal.FirmNo.ToString();
                    ReportSession[23] = "2";

                    Form NewF = null;
                    if (rdBrand.Checked == true || rdItem.Checked == true)
                    {
                        //if (rdItem.Checked == true)
                        //{
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                                NewF = new Display.ReportViewSource(new Reports.RptTopBrandwiseItemwiseValue(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptTopBrandwiseItemwiseValue.rpt", CommonFunctions.ReportPath), ReportSession);
                        //}
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);

                }
                else
                    OMMessageBox.Show("Enter Valid Value", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
               
            } 
        }

        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                rdItem.Focus();
            } 
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string str = "", ReportName = "";//, PartyName = "";
                int dtCount = 0; string TotalSales="0";

                //if (VoucherType == VchType.Sales) 
                //else ReportName = "Purchase Register";
                if (rdItem.Checked == true)
                {
                    str = "Exec GetTopItemwiseandBrandwiseSale '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtTopValue.Text.Trim()) + ",0," + DBGetVal.FirmNo + ",1";
                    dt = ObjFunction.GetDataView(str).Table;
                    dt.Columns[0].ColumnName = "ItemName";
                    ReportName = "Top Selling Item Valuewise";
                    if(dt.Rows.Count>0) TotalSales=dt.Rows[0].ItemArray[3].ToString();

                }
                else
                {
                    str = "Exec GetTopItemwiseandBrandwiseSale '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtTopValue.Text.Trim()) + ",1," + DBGetVal.FirmNo + ",1";
                    dt = ObjFunction.GetDataView(str).Table;
                    dt.Columns[0].ColumnName = "BrandName";
                    ReportName = "Top Selling Brand Valuewise";
                    if(dt.Rows.Count>0) TotalSales=dt.Rows[0].ItemArray[3].ToString();
                }

                dtCount = dt.Columns.Count - 1;// PartyName = "Party";
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

                    excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;
                    excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTPToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;
                    col++;
                    //Report Name And Dates
                    excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    excel.createHeaders(col, 1, "Total Sales : "+TotalSales+"", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    //col++;
                    //excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    for (int i = 0; i < dtCount; i++)
                    {
                        if (i == 0)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if (i == 1)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else
                            excel.createHeaders(col, i + 1, "% " + dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 0)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        }
                    }
                    col++;
                    col = col + dt.Rows.Count+1;

                    if (rdItem.Checked == true)
                    {
                        str = "Exec GetTopItemwiseandBrandwiseSaleQty '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtTopQty.Text.Trim()) + ",0," + DBGetVal.FirmNo + ",1";
                        dt = ObjFunction.GetDataView(str).Table;
                        dt.Columns[0].ColumnName = "ItemName";
                        ReportName = "Top Selling Item Quantitywise";
                        if (dt.Rows.Count > 0) TotalSales = dt.Rows[0].ItemArray[3].ToString();
                    }
                    else
                    {
                        str = "Exec GetTopItemwiseandBrandwiseSaleQty '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtTopQty.Text.Trim()) + ",1," + DBGetVal.FirmNo + ",1";
                        dt = ObjFunction.GetDataView(str).Table;
                        dt.Columns[0].ColumnName = "BrandName";
                        ReportName = "Top Selling Brand Quantitywise";
                        if (dt.Rows.Count > 0) TotalSales = dt.Rows[0].ItemArray[3].ToString();
                    }
                    excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    excel.createHeaders(col, 1, "Total Sales : " + TotalSales + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    //col++;
                    //excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    for (int i = 0; i < dtCount; i++)
                    {
                        if (i == 0)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if(i==1)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else 
                            excel.createHeaders(col, i + 1, "% " + dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 0)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        }
                    }
                    col++;
                    col = col + dt.Rows.Count+1;

                    if (rdItem.Checked == true)
                    {
                        str = "Exec GetTopItemwiseandBrandwiseSale '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtBottomValue.Text.Trim()) + ",0," + DBGetVal.FirmNo + ",2";
                        dt = ObjFunction.GetDataView(str).Table;
                        dt.Columns[0].ColumnName = "ItemName";
                        ReportName = "Bottom Selling Item Valuewise";
                        if (dt.Rows.Count > 0) TotalSales = dt.Rows[0].ItemArray[3].ToString();
                    }
                    else
                    {
                        str = "Exec GetTopItemwiseandBrandwiseSale '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtBottomValue.Text.Trim()) + ",1," + DBGetVal.FirmNo + ",2";
                        dt = ObjFunction.GetDataView(str).Table;
                        dt.Columns[0].ColumnName = "BrandName";
                        ReportName = "Bottom Selling Brand Valuewise";
                        if (dt.Rows.Count > 0) TotalSales = dt.Rows[0].ItemArray[3].ToString();
                    }
                    excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    excel.createHeaders(col, 1, "Total Sales : " + TotalSales + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    //col++;
                    //excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    for (int i = 0; i < dtCount; i++)
                    {
                        if (i == 0)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if (i == 1)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else
                            excel.createHeaders(col, i + 1, "% " + dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 0)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        }
                    }
                    col++;
                    col = col + dt.Rows.Count+1;


                    if (rdItem.Checked == true)
                    {
                        str = "Exec GetTopItemwiseandBrandwiseSaleQty '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtBottomQty.Text.Trim()) + ",0," + DBGetVal.FirmNo + ",2";
                        dt = ObjFunction.GetDataView(str).Table;
                        dt.Columns[0].ColumnName = "ItemName";
                        ReportName = "Bottom Selling Item Quantitywise";
                        if (dt.Rows.Count > 0) TotalSales = dt.Rows[0].ItemArray[3].ToString();
                    }
                    else
                    {
                        str = "Exec GetTopItemwiseandBrandwiseSaleQty '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy") + "'," + Convert.ToInt64(txtBottomQty.Text.Trim()) + ",1," + DBGetVal.FirmNo + ",2";
                        dt = ObjFunction.GetDataView(str).Table;
                        dt.Columns[0].ColumnName = "BrandName";
                        ReportName = "Bottom Selling Brand Quantitywise";
                        if (dt.Rows.Count > 0) TotalSales = dt.Rows[0].ItemArray[3].ToString();
                    }
                    excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    excel.createHeaders(col, 1, "Total Sales : " + TotalSales + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    //col++;
                    //excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    for (int i = 0; i < dtCount; i++)
                    {
                        if (i == 0)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else if (i == 1)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else
                            excel.createHeaders(col, i + 1, "% " + dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 0)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
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

        private void rdBrand_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdItem_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtTopValue_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtTopQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtBottomValue_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtBottomQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtTopValue_Leave(object sender, EventArgs e)
        {
            if (txtTopValue.Text.Trim() == "")
                txtTopValue.Text = "0";
        }

        private void txtTopValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtTopQty.Focus();
            }
        }

        private void txtTopQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtBottomValue.Focus();
            }
        }

        private void txtTopQty_Leave(object sender, EventArgs e)
        {
            if (txtTopQty.Text.Trim() == "")
                txtTopQty.Text = "0";
        }

        private void txtBottomValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtBottomQty.Focus();
            }
        }

        private void txtBottomValue_Leave(object sender, EventArgs e)
        {
            if (txtBottomValue.Text.Trim() == "")
                txtBottomValue.Text = "0";
        }

        private void txtBottomQty_Leave(object sender, EventArgs e)
        {
            if (txtBottomQty.Text.Trim() == "")
                txtBottomQty.Text = "0";
        }

        private void txtBottomQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnPrint.Focus();
            }
        }

        
    }
}
