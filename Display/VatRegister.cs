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
    public partial class VatRegister : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataSet dsVd1 = new DataSet();
        DataSet dsVd2 = new DataSet();
        DataSet dsVd3 = new DataSet();
        DataSet dsVd4 = new DataSet();
        DataSet dsVd5 = new DataSet();
        public static int StepNo;
        public static int MonthlyType;

        public static int VoucherNo, SelectedLedger, SelectedItem, ItNo, SubType, Gno;
        public static string ItName;
        public static string GrpName;
        private long VoucherType;

        public VatRegister()
        {
            InitializeComponent();
        }

        public VatRegister(long VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
        }

        private void VatRegister_Load(object sender, EventArgs e)
        {
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            BtnShow.Focus();
            btnPrint.Visible = false;

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                //dsVd = ObjDset.FillDset("New", "exec GetVatWiseSaleDtls " + VType + ",1,'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                dsVd = ObjDset.FillDset("New", "exec VatTaxDetails " + VoucherType + "," + DBGetVal.FirmNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                DataTable dt = dsVd.Tables[0];
                DataRow dr = dt.NewRow();
                dsVd.Tables[0].Rows.Add(dr);

                DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                DataGridView1.Visible = true;
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
                        if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value) != false)
                            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = 0;

                        if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) != false)
                            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = 0;
                        //if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value) != false)
                        //    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value = 0;

                        if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[1].Value) != false)
                        { DataGridView1.Rows[i].Cells[1].Value = 0; }
                        if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[2].Value) != false)
                        { DataGridView1.Rows[i].Cells[2].Value = 0; }
                        //if (Convert.IsDBNull(DataGridView1.Rows[i].Cells[7].Value) != false)
                        //{ DataGridView1.Rows[i].Cells[7].Value = 0; }
                        //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[0].Value = 100;
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[1].Value);
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value);
                        //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[7].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[7].Value);
                    }
                }
                DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();//new Font("OM-DEV-0714", 15, FontStyle.Bold);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
           // DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = "Total";
            //DataGridView1.Columns[3].Visible = false;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //VoucherNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[3].Value);
            //dsVd1 = ObjDset.FillDset("New", "exec GetItemWiseTaxDetails " + VoucherNo + "", CommonFunctions.ConStr);
            ////DataTable dt = dsVd1.Tables[0];
            ////DataRow dr = dt.NewRow();
            ////dsVd1.Tables[0].Rows.Add(dr);
            //bindingSource2.DataSource = dsVd1.Tables[0];
            //dataGridView2.DataSource = bindingSource2;
            //dataGridView2.Visible = true;
            //DataGridView1.Visible = false;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (this.DataGridView1.Columns[e.ColumnIndex].Name == "VoucherTypeName")
            //{
            //    e.CellStyle.Font = new Font("Verdana", 10);
            //}
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
                if (VoucherType == VchType.Sales)
                    ReportSession[4] = "Sales Vat Calculation Register";
                else if (VoucherType == VchType.Purchase)
                    ReportSession[4] = "Purchase Vat Calculation Register";
                else
                    ReportSession[4] = "Vat Calcualtion Register";

                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource("VatCalculationRegister", ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("VatCalculationRegister.rpt", CommonFunctions.ReportPath), ReportSession);
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
