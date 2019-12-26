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
    public partial class ItemRateDiffDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        
        long RateTypeNo=0;

        public ItemRateDiffDetails()
        {
            InitializeComponent();
        }

        private void StockSummary_Load(object sender, EventArgs e)
        {
            RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_RateType));
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            KeyDownFormat(this.Controls);
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                btnExport.Visible = true;
            else
                btnExport.Visible = false;
        }

        public bool IsSuperMode()
        {
            bool flag = false;
            long RTNo = RateTypeNo;
            if (RTNo == 1)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateSuperMode));
            }
            else if (RTNo == 2)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateSuperMode));
            }
            else if (RTNo == 3)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateSuperMode));
            }
            else if (RTNo == 4)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateSuperMode));
            }
            else if (RTNo == 5)
            {
                flag = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateSuperMode));
            }
            return flag;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Form NewF = null;
                string[] ReportSession;

                ReportSession = new string[3];
                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = "true";//(IsSuperMode() == true) ? "true" : "false";
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.RptItemRateDiffDetails(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptItemRateDiffDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
        }
     
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.O && e.Control)
            //{
            //    if (DBGetVal.IsAdmin == true)
            //    {
            //        txtRateTypePassword.Enabled = true;
            //        pnlRateType.Visible = true;
            //        txtRateTypePassword.Text = "";
            //        txtRateTypePassword.Focus();
            //    }
            //}
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

        #region Rate Type Password related Methods

        private void txtRateTypePassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnRateTypeOK_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pnlRateType.Visible = false;
                btnShow.Focus();
            }
        }

        private void btnRateTypeOK_Click(object sender, EventArgs e)
        {
            bool flag = false;
            string[,] arr = new string[5, 2];
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive)) == true)
            {
                arr[0, 0] = ObjFunction.GetAppSettings(AppSettings.ARatePassword);
                arr[0, 1] = "ASaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive)) == true)
            {
                arr[1, 0] = ObjFunction.GetAppSettings(AppSettings.BRatePassword);
                arr[1, 1] = "BSaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive)) == true)
            {
                arr[2, 0] = ObjFunction.GetAppSettings(AppSettings.CRatePassword);
                arr[2, 1] = "CSaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive)) == true)
            {
                arr[3, 0] = ObjFunction.GetAppSettings(AppSettings.DRatePassword);
                arr[3, 1] = "DSaleRate";
            }

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive)) == true)
            {
                arr[4, 0] = ObjFunction.GetAppSettings(AppSettings.ERatePassword);
                arr[4, 1] = "ESaleRate";
            }

            for (int i = 0; i < 5; i++)
            {
                if (arr[i, 0] != null)
                {
                    if (txtRateTypePassword.Text == arr[i, 0].ToString())
                    {
                        
                        RateTypeNo = i + 1;
                        
                        flag = true;
                        break;
                    }
                }
            }
            if (flag == false)
            {
                OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtRateTypePassword.Focus();
            }
            else
            {
                pnlRateType.Visible = false;
            }
        }

        private void btnRateTypeCancel_Click(object sender, EventArgs e)
        {
            txtRateTypePassword.Text = "";
            pnlRateType.Visible = false;
        }

        #endregion

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable(); int dtCount = 0; double TotalQty = 0; double TotalDiff = 0;
                bool flag = true;// (IsSuperMode() == true) ? true : false;
                string str = "Exec RptItemRateDiffDetails '" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + flag + "";
                dt = ObjFunction.GetDataView(str).Table;
                dt.Columns.RemoveAt(0);
                DataRow dr = dt.NewRow();
                dr[0] = "";
                for (int i = 1; i < 13; i++)
                {
                    if (i != 1 && i != 5 && i != 9)
                    {
                        dr[i] = Convert.ToDouble(dt.Compute("Sum(" + dt.Columns[i].ColumnName + ")", ""));
                        if (i == 2 || i == 6 || i == 10)
                            TotalQty = TotalQty + Convert.ToDouble(dt.Compute("Sum(" + dt.Columns[i].ColumnName + ")", ""));
                        if (i == 4 || i == 8 || i == 12)
                            TotalDiff = TotalDiff + Convert.ToDouble(dt.Compute("Sum(" + dt.Columns[i].ColumnName + ")", ""));
                    }
                }

                dt.Rows.Add(dr);


                if (flag == false)
                {
                    dt.Columns.RemoveAt(9); dt.Columns.RemoveAt(9); dt.Columns.RemoveAt(9); dt.Columns.RemoveAt(9);
                    dtCount = dt.Columns.Count;
                    dt.Columns[1].ColumnName = "W/S";
                    dt.Columns[5].ColumnName = "Retail";
                }
                else
                {
                    dtCount = dt.Columns.Count;
                    dt.Columns[1].ColumnName = "W/S";
                    dt.Columns[5].ColumnName = "Retail";
                    dt.Columns[9].ColumnName = "Retails";
                }

                int col = 1; //double total = 0;
                CreateExcel excel = new CreateExcel();
                //Company Name Header
                excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                col++;
                //Company Address Header
                excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                col++;
                //Report Name And Dates
                excel.createHeaders(col, 1, "Item Rate Difference Details", excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;
                excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 10, Color.Black, 10, CreateExcel.ExAlign.Left);
                col++;

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else
                        excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Right);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (j != dt.Rows.Count - 1)
                        {
                            if (i == 0)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                        }
                        else
                            excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, true);
                    }
                }
                col = col + dt.Rows.Count - 1;
                excel.addData(col + 2, 1, "", excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 2, "", excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 3, TotalQty.ToString(), excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 4, "", excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 5, "", excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 6, "", excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 7, "", excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 8, "", excel.ColName(col + 2, 3), excel.ColName(col + 2, 3), "", 0, CreateExcel.ExAlign.Right, true);
                excel.addData(col + 2, 9, TotalDiff.ToString(), excel.ColName(col + 2, 8), excel.ColName(col + 2, 8), "", 0, CreateExcel.ExAlign.Right, true);
                excel.CompleteDoc("");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            

        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
