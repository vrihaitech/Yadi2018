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
    public partial class GRNDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public GRNDetails()
        {
            InitializeComponent();
        }

        private void GRNDetails_Load(object sender, EventArgs e)
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
                        NewF = new Display.ReportViewSource(new Reports.GetGRNSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetGRNSummary.rpt", CommonFunctions.ReportPath), ReportSession);
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
                        NewF = new Display.ReportViewSource(new Reports.GetGRNDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetGRNDetails.rpt", CommonFunctions.ReportPath), ReportSession);
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

        public void ExportSummary()
        {
            DataTable dt = new DataTable();

            dt = ObjFunction.GetDataView("Exec GetGRNSummary " + DBGetVal.FirmNo + ",'" + txtFromDate.Text + "','" + txtToDate.Text + "',1").Table;
            dt.Columns[0].ColumnName = "GRNNo";
            dt.Columns[1].ColumnName = "Date";
            dt.Columns[2].ColumnName = "Ref.No";
            dt.Columns[3].ColumnName = "Party";
            dt.Columns[4].ColumnName = "Godown";
            dt.Columns[5].ColumnName = "Applicable";
            
            int dtCount = dt.Columns.Count;
            try
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
                excel.createHeaders(col, 1, "GRNSummary", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(txtFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(txtToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);


                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (i == 5)
                        {
                            if (Convert.ToInt64(dt.Rows[j].ItemArray[i].ToString()) == 0)
                            {
                                excel.createHeaders(j + col + 1, i + 1, "NO -", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            }
                            else

                                excel.createHeaders(j + col + 1, i + 1, "Yes - " + dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                        }

                        else if (i == 0)
                            excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else if (i == 1)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                        else
                            excel.createHeaders(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Left);


                    }


                }
                col++;
                excel.CompleteDoc("");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (rbSummary.Checked == true)
                ExportSummary();
            else
                ExportDetails();

        }

        public void ExportDetails()
        {
            DataTable dt = new DataTable();
            double Amt = 0,Qty=0;

            long GRNNo = -1; int ExtraRows = 0;
            dt = ObjFunction.GetDataView("Exec GetGRNSummary " + DBGetVal.FirmNo + ",'" + txtFromDate.Text + "','" + txtToDate.Text + "',2").Table;
            dt.Columns[0].ColumnName = "GRNNo";
            dt.Columns[1].ColumnName = "Date";
            dt.Columns[2].ColumnName = "Ref.No";
            dt.Columns[3].ColumnName = "Party";
            dt.Columns[4].ColumnName = "Godown";
            dt.Columns[5].ColumnName = "Applicable";

            int dtCount = dt.Columns.Count; 
            try
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
                excel.createHeaders(col, 1, "GRNSummary", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(txtFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(txtToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < 6; i++)
                {
                    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {

                    if (GRNNo != Convert.ToInt64(dt.Rows[j].ItemArray[0].ToString()))
                    {
                        if (GRNNo != -1)
                        {
                            excel.createHeaders(j + col + 1 + ExtraRows, 1, "Total", excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            excel.addData(j + col + 1 + ExtraRows, 2, "", excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), "", 0, CreateExcel.ExAlign.Left, true);
                            excel.addData(j + col + 1 + ExtraRows, 2, "", excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), "", 0, CreateExcel.ExAlign.Left, true);
                            excel.addData(j + col + 1 + ExtraRows, 3, "", excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), "", 0, CreateExcel.ExAlign.Left, true);
                            excel.addData(j + col + 1 + ExtraRows, 4, Qty.ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), "", 0, CreateExcel.ExAlign.Left, true);
                            excel.addData(j + col + 1 + ExtraRows, 5, "", excel.ColName(j + col + 1 + ExtraRows, 5), excel.ColName(j + col + 1 + ExtraRows, 5), "", 0, CreateExcel.ExAlign.Left, true);
                            excel.addData(j + col + 1 + ExtraRows, 6, Amt.ToString(), excel.ColName(j + col + 1 + ExtraRows, 6), excel.ColName(j + col + 1 + ExtraRows, 6), "", 0, CreateExcel.ExAlign.Left, true);
                            ExtraRows++;
                            Amt = 0;
                            Qty = 0;

                        }
                        //excel.createHeaders(j + col + 1 + ExtraRows, 1, dt.Rows[j].ItemArray[0].ToString(), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        excel.addData(j + col + 1 + ExtraRows, 1, dt.Rows[j].ItemArray[0].ToString(), excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), "", 0, CreateExcel.ExAlign.Right, true);
                        excel.addData(j + col + 1 + ExtraRows, 2, dt.Rows[j].ItemArray[1].ToString(), excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 3, dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 4, dt.Rows[j].ItemArray[3].ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 5, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1 + ExtraRows, 5), excel.ColName(j + col + 1 + ExtraRows, 5), "", 0, CreateExcel.ExAlign.Left, true);
                        //excel.createHeaders(j + col + 1 + ExtraRows, 2, dt.Rows[j].ItemArray[1].ToString(), excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //excel.createHeaders(j + col + 1 + ExtraRows, 3, dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //excel.createHeaders(j + col + 1 + ExtraRows, 4, dt.Rows[j].ItemArray[3].ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                        //excel.createHeaders(j + col + 1 + ExtraRows, 5, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1 + ExtraRows, 5), excel.ColName(j + col + 1 + ExtraRows, 5), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                       
                        if (Convert.ToInt64(dt.Rows[j].ItemArray[5].ToString()) == 0)
                        {
                            excel.createHeaders(j + col + 1 + ExtraRows, 6, "NO -", excel.ColName(j + col + 1 + ExtraRows, 6), excel.ColName(j + col + 1 + ExtraRows, 6), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        }
                        else
                            excel.createHeaders(j + col + 1 + ExtraRows, 6, "Yes - " + dt.Rows[j].ItemArray[5].ToString(), excel.ColName(j + col + 1 + ExtraRows, 6), excel.ColName(j + col + 1 + ExtraRows, 6), 1, Color.White, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                            
                        ExtraRows++;

                        excel.addData(j + col + 1 + ExtraRows, 1, "", excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 2, "ItemName", excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 3, "Quantity", excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 4, "UOM", excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 5, "Rate", excel.ColName(j + col + 1 + ExtraRows, 5), excel.ColName(j + col + 1 + ExtraRows, 5), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 6, "Amount", excel.ColName(j + col + 1 + ExtraRows, 6), excel.ColName(j + col + 1 + ExtraRows, 6), "", 0, CreateExcel.ExAlign.Left, true);
                        ExtraRows++;

                       
                        GRNNo = Convert.ToInt64(dt.Rows[j].ItemArray[0].ToString());
                    }

                    excel.addData(j + col + 1 + ExtraRows, 1, "", excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), "", 0, CreateExcel.ExAlign.Left, false);
                    excel.addData(j + col + 1 + ExtraRows, 2, dt.Rows[j].ItemArray[6].ToString(), excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), "", 0, CreateExcel.ExAlign.Left, false);
                    excel.addData(j + col + 1 + ExtraRows, 3, dt.Rows[j].ItemArray[7].ToString(), excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), "", 0, CreateExcel.ExAlign.Left, false);
                    excel.addData(j + col + 1 + ExtraRows, 4, dt.Rows[j].ItemArray[8].ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), "", 0, CreateExcel.ExAlign.Left, false);
                    excel.addData(j + col + 1 + ExtraRows, 5, dt.Rows[j].ItemArray[9].ToString(), excel.ColName(j + col + 1 + ExtraRows, 5), excel.ColName(j + col + 1 + ExtraRows, 5), "", 0, CreateExcel.ExAlign.Left, false);
                    excel.addData(j + col + 1 + ExtraRows, 6, dt.Rows[j].ItemArray[10].ToString(), excel.ColName(j + col + 1 + ExtraRows, 6), excel.ColName(j + col + 1 + ExtraRows, 6), "", 0, CreateExcel.ExAlign.Left, false);
                    Amt = Amt + Convert.ToDouble(dt.Rows[j].ItemArray[10].ToString());
                    Qty = Qty + Convert.ToDouble(dt.Rows[j].ItemArray[7].ToString());

                    //excel.createHeaders(j + col + 1 + ExtraRows, 2, dt.Rows[j].ItemArray[2].ToString(), excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //excel.createHeaders(j + col + 1 + ExtraRows, 3, Math.Abs(Convert.ToDouble(dt.Rows[j].ItemArray[3].ToString())).ToString(), excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), 1, Color.White, false, 20, Color.Black, 12, CreateExcel.ExAlign.Right);
                    //excel.createHeaders(j + col + 1 + ExtraRows, 4, dt.Rows[j].ItemArray[4].ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), 1, Color.White, false, 10, Color.Black, 12, CreateExcel.ExAlign.Center);




                    if (j == dt.Rows.Count - 1)
                    {
                        ExtraRows++;
                        excel.createHeaders(j + col + 1 + ExtraRows, 1, "Total", excel.ColName(j + col + 1 + ExtraRows, 1), excel.ColName(j + col + 1 + ExtraRows, 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                        excel.addData(j + col + 1 + ExtraRows, 2, "", excel.ColName(j + col + 1 + ExtraRows, 2), excel.ColName(j + col + 1 + ExtraRows, 2), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 3, "", excel.ColName(j + col + 1 + ExtraRows, 3), excel.ColName(j + col + 1 + ExtraRows, 3), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 4, Qty.ToString(), excel.ColName(j + col + 1 + ExtraRows, 4), excel.ColName(j + col + 1 + ExtraRows, 4), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 5, "", excel.ColName(j + col + 1 + ExtraRows, 5), excel.ColName(j + col + 1 + ExtraRows, 5), "", 0, CreateExcel.ExAlign.Left, true);
                        excel.addData(j + col + 1 + ExtraRows, 6, Amt.ToString(), excel.ColName(j + col + 1 + ExtraRows, 6), excel.ColName(j + col + 1 + ExtraRows, 6), "", 0, CreateExcel.ExAlign.Left, true);
                    }

                    excel.CompleteDoc("");

                }
                col++;
                excel.CompleteDoc("");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }      
    }
}
