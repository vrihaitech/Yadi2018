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
    public partial class HomeDeliverSalesRegister : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();




        public HomeDeliverSalesRegister()
        {
            InitializeComponent();
        }

        private void HomeDeliverSalesRegister_Load(object sender, EventArgs e)
        {
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            btnShow.Focus();
            KeyDownFormat(this.Controls);
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;

                for (int i = 0; i < dgDetails.Rows.Count; i++)
                {
                    dgDetails.Rows[i].Cells[9].Value = chkSelectAll.Checked;
                }
                BtnPrint.Focus();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            string strItemNo = "";
            for (int i = 0; i < dgDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgDetails.Rows[i].Cells[9].EditedFormattedValue) == true)
                {
                    if (strItemNo == "")
                        strItemNo = dgDetails.Rows[i].Cells[3].Value.ToString();
                    else
                        strItemNo = strItemNo + "," + dgDetails.Rows[i].Cells[3].Value.ToString();
                }
            }
            if (strItemNo != "")
            {
                string[] ReportSession;
                ReportSession = new string[3];
                ReportSession[0] = DTPFromDate.Text;
                ReportSession[1] = DTToDate.Text;
                ReportSession[2] = strItemNo;
                Form NewF = null;

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.RPTHomeDeliverSalesRegister(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTHomeDeliverSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else
                OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string strItemNo = "";
            for (int i = 0; i < dgDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgDetails.Rows[i].Cells[9].EditedFormattedValue) == true)
                {
                    if (strItemNo == "")
                        strItemNo = dgDetails.Rows[i].Cells[3].Value.ToString();
                    else
                        strItemNo = strItemNo + "," + dgDetails.Rows[i].Cells[3].Value.ToString();
                }
            }

            string str = "", ReportName = "Sales Register (Home Delivery)";

            int dtCount = 0;

            if (strItemNo != "")
            {

                str = "EXEC GetHomeDeliverSalesRegister '" + DTPFromDate.Text + "','" + DTToDate.Text + "','" + strItemNo + "'";

                DataTable dt = ObjFunction.GetDataView(str).Table;
                dt.Columns[1].ColumnName = "Bill Date";
                dt.Columns[2].ColumnName = "Bill Time";
                dt.Columns[3].ColumnName = "Bill No";
                dt.Columns[4].ColumnName = "Customer Name";
                dt.Columns[6].ColumnName = "Amount";

                if (dt.Rows.Count > 0)
                {

                    double TotalAmt, totalDisc;
                    TotalAmt = Convert.ToDouble(dt.Compute("Sum(Amount)", ""));
                    totalDisc = Convert.ToDouble(dt.Compute("Sum(DiscAmt)", ""));
                    DataRow dr = dt.NewRow();
                    dr[4] = "Total";
                    dr[6] = TotalAmt;
                    dr[7] = totalDisc;
                    dt.Rows.Add(dr);
                }
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
                excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 8, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else if (i == 1 || i == 2)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    else if (i == 4 || i == 5 || i == 8)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 35, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 6 || i == 7)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                    else
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (i == 0)
                        {
                            if (dt.Rows[j].ItemArray[0].ToString().Trim() != "")
                            excel.addData(j + col + 1, i + 1, (j + 1).ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else
                            excel.addData(j + col + 1, i + 1, "", excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                        }
                        else if (i == 1)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 2)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.HHMM, 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 3 || i == 8 || i == 5)
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                        else if (i == 4)
                        {
                            if (dt.Rows[j].ItemArray[0].ToString().Trim() == "")
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, true);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                        }
                        else
                        {
                            if (dt.Rows[j].ItemArray[0].ToString().Trim() == "")
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, true);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                        }
                    }
                }
                col++;
                excel.CompleteDoc("");
            }
            else
                OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);

            string str = "EXEC GetHomeDeliverSalesRegister '" + DTPFromDate.Text + "','" + DTToDate.Text + "',''";
            DataTable dt = ObjFunction.GetDataView(str).Table;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgDetails.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dgDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                    if (j == 0)
                        dgDetails.Rows[i].Cells[j].Value = i + 1;
                }
            }
            if (dt.Rows.Count > 0)
            {
                BtnPrint.Enabled = true;
                btnExport.Enabled = true;
            }
            else
            {
                BtnPrint.Enabled = false;
                btnExport.Enabled = false;
            }
        }

        private void dgDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
                e.Value = Convert.ToDateTime(e.Value).ToString(Format.DDMMMYYYY);
            else if (e.ColumnIndex == 2)
                e.Value = Convert.ToDateTime(e.Value).ToShortTimeString();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgDetails.Rows.Count; i++)
            {
                dgDetails.Rows[i].Cells[9].Value = chkSelectAll.Checked;
            }
        }

    }
}
