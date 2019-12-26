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
    public partial class QuotationDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
      
        public QuotationDetails()
        {
            InitializeComponent();
        }

        private void QuotationDetails_Load(object sender, EventArgs e)
        {
            txtFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            txtToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            txtToDate.MinDate = txtFromDate.Value;
        }

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
            if (e.KeyCode == Keys.F2)
            {

            }
        }
        #endregion

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                if (rbSummary.Checked == true)
                {

                    ReportSession = new string[4];
                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = txtFromDate.Text;
                    ReportSession[2] = txtToDate.Text;
                    ReportSession[3] = "1";


                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetQuotationSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetQuotationSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else if (rbDetails.Checked == true)
                {
                    ReportSession = new string[4];
                    ReportSession[0] = DBGetVal.FirmNo.ToString();
                    ReportSession[1] = txtFromDate.Text;
                    ReportSession[2] = txtToDate.Text;
                    ReportSession[3] = "2";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetQuatationDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetQuatationDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



        private void txtFromDate_ValueChanged(object sender, EventArgs e)
        {
            txtToDate.MinDate = txtFromDate.Value;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExecel_Click(object sender, EventArgs e)
        {
            string sql = "";
            DataTable dt = new DataTable();
            int dtCount = 0;
            if (rbSummary.Checked == true)
            {
                sql = "exec GetQuotationSummary " + DBGetVal.FirmNo + ",'" + txtFromDate.Text + "','" + txtToDate.Text + "',1";
                dt = ObjFunction.GetDataView(sql).Table;

                dt.Columns[0].ColumnName = "Quo.No";
                dt.Columns[1].ColumnName = "Date";
                dt.Columns[2].ColumnName = "Party";
                dt.Columns[3].ColumnName = "FromDate";
                dt.Columns[4].ColumnName = "ToDate";
                dt.Columns.Add();
                dt.Columns[5].ColumnName = " ";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToDateTime(dt.Rows[i].ItemArray[4].ToString()) < DBGetVal.ServerTime)
                        dt.Rows[i][5] = "*";
                    else
                        dt.Rows[i][5] = " ";
                }
                dt.AcceptChanges();
                dtCount = dt.Columns.Count;

                int col = 1;
                CreateExcel excel = new CreateExcel();
                //Company Name Header
                excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                col++;
                //Company Address Header
                excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                col++;
                //Report Name And Dates
                excel.createHeaders(col, 1, "Quotation Summary", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 5)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 2, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (i == 0 || i == 2 || i == 5)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 1 || i == 3 || i == 4)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYY, 0, CreateExcel.ExAlign.Left, false);
                        else
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                    }
                }
                excel.CompleteDoc("");
            }
            else
            {
                sql = "exec GetQuotationSummary " + DBGetVal.FirmNo + ",'" + txtFromDate.Text + "','" + txtToDate.Text + "',2";
                dt = ObjFunction.GetDataView(sql).Table;

                dt.Columns[0].ColumnName = "Quo.No";
                dt.Columns[1].ColumnName = "Date";
                dt.Columns[2].ColumnName = "Party";
                dt.Columns[3].ColumnName = "FromDate";
                dt.Columns[4].ColumnName = "ToDate";
                dt.Columns.Add();
                dt.Columns[dt.Columns.Count - 1].SetOrdinal(5);
                dt.Columns[5].ColumnName = " ";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToDateTime(dt.Rows[i].ItemArray[4].ToString()) < DBGetVal.ServerTime)
                        dt.Rows[i][5] = "*";
                    else
                        dt.Rows[i][5] = " ";
                }
                dt.AcceptChanges();
                dtCount = 6;

                int col = 1;
                CreateExcel excel = new CreateExcel();
                //Company Name Header
                excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                col++;
                //Company Address Header
                excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                col++;
                //Report Name And Dates
                excel.createHeaders(col, 1, "Quotation Details", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(txtFromDate.Text).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(txtToDate.Text).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                string strPartName = "";

                for (int i = 0; i < dtCount; i++)
                {
                    if (i == 5)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 0 || i == 1)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 2)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 30, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    col++;

                    if (strPartName != dt.Rows[i].ItemArray[2].ToString())
                    {
                        for (int j = 0; j < dtCount; j++)
                        {
                            if (j == 1 || j == 3 || j == 4)
                                excel.addData(col, j + 1, dt.Rows[i].ItemArray[j].ToString(), excel.ColName(col, j + 1), excel.ColName(col, j + 1), Format.DDMMMYY, 0, CreateExcel.ExAlign.Left, true);
                            else
                                excel.addData(col, j + 1, dt.Rows[i].ItemArray[j].ToString(), excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Left, true);
                        }

                        strPartName = dt.Rows[i].ItemArray[2].ToString();
                        col++;
                        for (int j = 0; j < dtCount; j++)
                        {
                            if (j != 0 && j != 1)
                            {
                                if (j == 2)
                                    excel.addData(col, j + 1, "Item Name", excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Left, true);
                                else if (j == 3)
                                    excel.addData(col, j + 1, "UOM", excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Left, true);
                                else if (j == 4)
                                    excel.addData(col, j + 1, "MRP", excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Right, true);
                                else if (j == 5)
                                    excel.addData(col, j + 1, "Rate", excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Right, true);
                            }
                        }
                        col++;

                    }
                    for (int j = 0; j < dtCount; j++)
                    {
                        if (j != 0 && j != 1)
                        {
                            if (j == 2)
                                excel.addData(col, j + 1, dt.Rows[i].ItemArray[6].ToString(), excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else if (j == 3)
                                excel.addData(col, j + 1, dt.Rows[i].ItemArray[7].ToString(), excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else if (j == 4)
                                excel.addData(col, j + 1, dt.Rows[i].ItemArray[9].ToString(), excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Right, false);
                            else if (j == 5)
                                excel.addData(col, j + 1, dt.Rows[i].ItemArray[8].ToString(), excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Right, false);
                            //else
                            //    excel.addData(col, j + 1, dt.Rows[i].ItemArray[j].ToString(), excel.ColName(col, j + 1), excel.ColName(col, j + 1), "", 0, CreateExcel.ExAlign.Right, false);
                        }
                    }

                }

                excel.CompleteDoc("");
            }

        }
    }
}
