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
    public partial class ItemWiseVatDtls : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBProgressBar PB;
        DataSet dsVd = new DataSet();
        DataSet dsVd1 = new DataSet();
        DataSet dsVd2 = new DataSet();
        DataSet dsVd3 = new DataSet();
        DataSet dsVd4 = new DataSet();
        DataSet dsVd5 = new DataSet();
        public static int StepNo;
        public static int MonthlyType;
        public static DateTime FromDate, ToDate;
        public static int VoucherNo, SelectedLedger, SelectedItem, ItNo, SubType, Gno;
        public static string ItName;
        public static string GrpName;
        private int VoucherType;

        public ItemWiseVatDtls(int VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
        }

        private void ItemWiseVatDtls_Load(object sender, EventArgs e)
        {
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            BtnShow.Focus();
            btnPrint.Visible = false;
            dataGridView2.Visible = false;
            
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
                dsVd = ObjDset.FillDset("New", "exec GetSaleVouchVatEntryDtls " + VoucherType + ",1,'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                DataTable dt = dsVd.Tables[0];
                DataRow dr = dt.NewRow();
                dsVd.Tables[0].Rows.Add(dr);

                DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                //DataGridView1.Visible = true;
                dataGridView2.Visible = false;
                if (DataGridView1.Rows.Count > 0)
                {
                    DataGridView1.Columns[1].Width = 250;
                    DataGridView1.Columns[2].Width = 150;
                    btnPrint.Visible = true;
                }
                for (int i = 0; i < DataGridView1.Rows.Count - 1; i = i + 1)
                {
                    if (DataGridView1.Rows[i].Index != DataGridView1.Rows.Count - 1)
                    {
                        if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) != false)
                            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value = 0;

                        if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value) != false)
                            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value = 0;
                        if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value) != false)
                            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value = 0;

                        if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[5].Value) != false)
                        { DataGridView1.Rows[i].Cells[5].Value = 0; }
                        if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[6].Value) != false)
                        { DataGridView1.Rows[i].Cells[6].Value = 0; }
                        if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[7].Value) != false)
                        { DataGridView1.Rows[i].Cells[7].Value = 0; }
                        //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[0].Value = 100;
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[5].Value);
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[6].Value);
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[7].Value);
                    }
                }
                DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new Font("OM-DEV-0714", 15, FontStyle.Bold);
                DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = "EHw$U";
                DataGridView1.Columns[3].Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                VoucherNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[3].Value);
                dsVd1 = ObjDset.FillDset("New", "exec GetItemWiseTaxDetails " + VoucherNo + "", CommonFunctions.ConStr);
                //DataTable dt = dsVd1.Tables[0];
                //DataRow dr = dt.NewRow();
                //dsVd1.Tables[0].Rows.Add(dr);

                dataGridView2.DataSource = dsVd1.Tables[0].DefaultView;
                dataGridView2.Visible = true;
                //DataGridView1.Visible = false;
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i = i + 1)
                {
                    if (dataGridView2.Rows[i].Index != dataGridView2.Rows.Count - 1)
                    {
                        if (Convert.IsDBNull(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value) != false)
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = 0;

                        if (Convert.IsDBNull(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value) != false)
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value = 0;

                        if (Convert.IsDBNull(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[6].Value) != false)
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[6].Value = 0;

                        if (Convert.IsDBNull(dataGridView2.Rows[i].Cells[3].Value) != false)
                        { dataGridView2.Rows[i].Cells[3].Value = 0; }
                        if (Convert.IsDBNull(dataGridView2.Rows[i].Cells[5].Value) != false)
                        { dataGridView2.Rows[i].Cells[5].Value = 0; }
                        if (Convert.IsDBNull(dataGridView2.Rows[i].Cells[6].Value) != false)
                        { dataGridView2.Rows[i].Cells[6].Value = 0; }
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value);
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(dataGridView2.Rows[i].Cells[5].Value);
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(dataGridView2.Rows[i].Cells[6].Value);
                    }
                }
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new Font("OM-DEV-0714", 15, FontStyle.Bold);
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = "EHw$U";

                if (DataGridView1.Rows.Count > 1)
                    btnPrint.Visible = true;
                else btnPrint.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
               
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.DataGridView1.Columns[e.ColumnIndex].Width = 130;
            }
            if (this.DataGridView1.Columns[e.ColumnIndex].Name == "Voucher Type")
            {
                e.CellStyle.Font = new Font("Verdana", 10);
            }
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession = new string[5];
                ReportSession[0] = VoucherType.ToString();
                ReportSession[1] = DBGetVal.FirmNo.ToString();
                ReportSession[2] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                if (VoucherType == 16)
                    ReportSession[4] = "Sales Vat Calculation Details Report";
                else if (VoucherType == 9)
                    ReportSession[4] = "Purchase Vat Calculation Details Report";
                else
                    ReportSession[4] = "Vat Calcualtion Details Report";
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource("ItemWiseVatRegister", ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ItemWiseVatRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
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
