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
    public partial class PhysicalStockRegister : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        
        public long CompNo, LedgNo, MNo;
        public int VchNo;
        public string RptTitle;

        public PhysicalStockRegister()
        {
            InitializeComponent();
        }

        private void PhysicalStockRegister_Load(object sender, EventArgs e)
        {
            CompNo = 1;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dsVd = ObjDset.FillDset("New", "Exec GetPStockVouchEntryDtlsByDate " + 8 + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                DataTable dt = dsVd.Tables[0];
                //DataRow dr = dt.NewRow();
                //dsVd.Tables[0].Rows.Add(dr);
                DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                for (int i = 0; i < 8; i++)
                {
                    if (i == 1 || i == 5 || i == 6 || i == 7)
                    {
                        DataGridView2.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                DataGridView2.Columns[0].Visible = false;
                btnPrint.Visible = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                VchNo = 8;
                string[] ReportSession = new string[5];
                ReportSession[0] = VchNo.ToString();
                ReportSession[1] = DBGetVal.FirmNo.ToString();
                ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[4] = "Physical Stock Voucher Details";
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewPhysicalStockReg(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewPhysicalStockReg.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataGridView2.Columns[e.ColumnIndex].Name == "Particulars")
            {
                e.CellStyle.Font = new Font("Verdana", 10);
            }
        }

        public void GetCount()
        {
            for (int i = 0; i < DataGridView2.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView2.Rows[i].Index != DataGridView2.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value = 0;

                    if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value) != false)
                        DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value = 0;

                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[0].Value = 100;
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[2].Value);
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[3].Value);
                }
            }

            //===========Total At footer===========
            DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
            DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[1].Value = "Total";
        }

        private void DataGridView2_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.DataGridView2.Columns[e.ColumnIndex].Name == "VoucherUserNo")
            {
                this.DataGridView2.Columns[e.ColumnIndex].Width = 50;
            }
            if (this.DataGridView2.Columns[e.ColumnIndex].Name == "VoucherDate")
            {
                this.DataGridView2.Columns[e.ColumnIndex].Width = 120;
            }
            if (this.DataGridView2.Columns[e.ColumnIndex].Name == "VoucherNo")
            {
                this.DataGridView2.Columns[e.ColumnIndex].Visible = false;
            }
            if (this.DataGridView2.Columns[e.ColumnIndex].Name == "ItemName")
            {
                this.DataGridView2.Columns[e.ColumnIndex].Width = 200;
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

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }
    }
}
