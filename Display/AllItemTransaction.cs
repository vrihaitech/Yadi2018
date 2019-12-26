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
    public partial class AllItemTransaction : Form
    { 
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DBProgressBar PB;
        public static long CompNo, ItNo, MNo, Type1, No, ItNo1;
        public static DateTime FromDate, ToDate;
        public static string ItName, RptTitle, ItNm;

        public AllItemTransaction()
        {
            InitializeComponent();
        }

        private void AllItemTransaction_Load(object sender, EventArgs e)
        {
            
            CompNo = 1;           
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");           
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            btnPrint.Visible = false;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                btnPrint.Visible = false;
                DataGridView1.Visible = false;
                PB = new DBProgressBar(this);
                PB.TimerStart();
                PB.Ctrl = DataGridView1;
                dsVd = ObjDset.FillDset("New", "Select * From GetClosingStockItemWise (" + CompNo + ", '" + DTPFromDate.Text + "','" + DTToDate.Text + "', 0, 0)", CommonFunctions.ConStr);
                for (int i = 0; i < 10; i++)
                {
                    if (i != 1)
                        DataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                DataTable dt = dsVd.Tables[0];
                DataRow dr = dt.NewRow();
                dsVd.Tables[0].Rows.Add(dr);

                DataGridView1.DataSource = dsVd.Tables[0].DefaultView;

                if (DataGridView1.Rows.Count >= 1)
                    btnPrint.Visible = true;
                else btnPrint.Visible = false;

                GetCount();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void GetCount()
        {
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView1.Rows[i].Index != DataGridView1.Rows.Count - 1)
                {                    
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value = 0;
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value = 0;
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value = 0;
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[9].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[9].Value = 0;
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[3].Value);
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[5].Value);
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[7].Value);
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[9].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[9].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[9].Value);
                }
            }

            //===========Total At footer===========
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = "Total";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession = new string[6];
                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = "0";//Type
                ReportSession[4] = "0";//No
                ReportSession[5] = "All Item Transaction Details";
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewAllItemTransaction(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewAllItemTransaction.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "ItemID")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Visible = false;
            }
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "ItemName")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 200;
            }
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "OpAmt")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 100;
            }
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "InwAmt")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 100;
            }
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "OutwAmt")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 100;
            }
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "ClosingAmt")
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 100;
            }
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
                BtnShow.Focus();
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
    }
}

