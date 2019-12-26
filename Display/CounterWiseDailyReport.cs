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
    public partial class CounterWiseDailyReport : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataSet dsVd1 = new DataSet();
        DataSet dsVd2 = new DataSet();
        public long CompNo, LedgNo, MNo;
        public int VchNo;
        public string RptTitle;
        private int VoucherType;

        public CounterWiseDailyReport(int VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
        }

        public CounterWiseDailyReport()
        {
            InitializeComponent();
        }

        private void CounterWiseDailyReport_Load(object sender, EventArgs e)
        {
            CompNo = 1;
            btnVatReg.Visible = false;
            btnPrint.Visible = false;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;

        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // tabSumm.SelectedTab = tabPage1;
                dsVd = ObjDset.FillDset("New", "Exec ViewCounterwiseReport '" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                DataTable dt = dsVd.Tables[0];
                DataRow dr = dt.NewRow();
                dsVd.Tables[0].Rows.Add(dr);

                GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;
                //if (dsVd.Tables[0].Rows.Count > 0 && tabSumm.SelectedTab == tabPage1)
                //{
                //    GetCountVch();
                //}

                if (GridViewDaily.Rows.Count > 0)
                {
                    GetCountVch();
                    btnPrint.Visible = true;
                }
                else btnPrint.Visible = false;

                dsVd1 = ObjDset.FillDset("New", "Exec ViewCounterwiseReport '" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                DataTable dt1 = dsVd1.Tables[0];
                DataRow dr1 = dt1.NewRow();
                dsVd1.Tables[0].Rows.Add(dr1);

                dataGridView1.DataSource = dsVd1.Tables[0].DefaultView;

                //Nitesh
                //dsVd2 = ObjDset.FillDset("New", "Exec GetDailyItemSummary '" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                //DataTable dt2 = dsVd2.Tables[0];
                //for (int i = 0; i < 2; i++)
                //{
                //    DataRow dr2 = dt2.NewRow();
                //    dsVd2.Tables[0].Rows.Add(dr2);
                //}

                //Nitesh
                //dataGridView2.DataSource = dsVd2.Tables[0].DefaultView;
                //if (dsVd2.Tables[0].Rows.Count > 0 && tabSumm.SelectedTab == tabPage3)
                //{
                //    GetCountVchDtl();
                //}
                ////GetCountVch();
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
                string[] ReportSession;
                Form NewF = null;
                //if (tabSumm.SelectedIndex == 0)
                //{
                ReportSession = new string[2];
                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.CounterWiseReports(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("CounterWiseReports.rpt", CommonFunctions.ReportPath), ReportSession);

                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnVatReg_Click(object sender, EventArgs e)
        {
            Form childForm = new Display.VatRegister(VoucherType);
            if (VoucherType == 15)
                childForm.Text = "Sales Vat Register";
            else if (VoucherType == 9)
                childForm.Text = "Purchase Vat Register";
            else
                childForm.Text = "Vat Register";
            ObjFunction.OpenForm(childForm, DBGetVal.MainForm);
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
                DTPFromDate.Focus();
            }
        }

        private void GridViewDaily_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "CounterName")
            //{
            //    e.CellStyle.Font = new Font("Verdana", 10);
            //}
        }

        private void tabSumm_SelectedIndexChanged(object sender, EventArgs e) 
        {
            try
            {
                if (tabSumm.SelectedIndex == 0)
                {
                    //DTPFromDate.Left = 90;
                    //label1.Text = "From Date:";
                    DTToDate.Visible = true;
                    DTPFromDate.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;

                    dsVd = ObjDset.FillDset("New", "Exec ViewCounterwiseReport '" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;
                    if (dsVd.Tables[0].Rows.Count > 0 && tabSumm.SelectedTab == tabPage1)
                    {
                        GetCountVch();
                    }

                    if (GridViewDaily.Rows.Count > 0)
                    {

                        btnPrint.Visible = true;
                    }
                    else btnPrint.Visible = false;

                }
                else if (tabSumm.SelectedIndex == 1)
                {
                    //DTPFromDate.Left = 90;
                    //label1.Text = "From Date:";
                    DTToDate.Visible = true;
                    DTPFromDate.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;
                    dsVd = ObjDset.FillDset("New", "Exec GetItemDaily '" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    DataTable dt = dsVd.Tables[0];
                    dataGridView1.DataSource = dsVd.Tables[0].DefaultView;

                    if (dataGridView1.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;
                }
                else if (tabSumm.SelectedIndex == 2)
                {
                    //label1.Text = "Date:";
                    //DTPFromDate.Left = 50;
                    //DTToDate.Visible = false;
                    //label2.Visible = false;
                    //DTPFromDate.Value =Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

                    //Nitesh
                    //dsVd = ObjDset.FillDset("New", "Exec GetDailyItemSummary '" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    //DataTable dt = dsVd.Tables[0];
                    //for (int i = 0; i < 2; i++)
                    //{
                    //    DataRow dr = dt.NewRow();
                    //    dsVd.Tables[0].Rows.Add(dr);
                    //}

                    //dataGridView2.DataSource = dsVd.Tables[0].DefaultView;

                    //GetCountVchDtl();
                    //if (dataGridView2.Rows.Count > 0)
                    //    btnPrint.Visible = true;
                    //else btnPrint.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (this.dataGridView2.Columns[e.ColumnIndex].Name == "Group Name")
            //{
            //    e.CellStyle.Font = ObjFunction.GetFont();
            //}
        }

        public void GetCountVch()
        {
            for (int i = 0; i < GridViewDaily.Rows.Count - 1; i++)
            {
                if (GridViewDaily.Rows[i].Index != GridViewDaily.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value = 0;
                    else
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[2].Value);

                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[3].Value) == true)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[3].Value = 0;


                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[4].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[4].Value = 0;


                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value = (Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[2].Value)).ToString("0.00");
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[3].Value = (Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[3].Value)).ToString("0.00");
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[4].Value = (Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[4].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[4].Value)).ToString("0.00");

                }

                GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                // GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[1].Value = "EHy$U";
            }
        }

        public void GetCountVchDtl()
        {
            for (int i = 0; i < dataGridView2.Rows.Count - 2; i++)
            {
                if (dataGridView2.Rows[i].Index != dataGridView2.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value) != false)
                        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value = 0;
                    //else
                    //    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value = Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value) + Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value);

                    if (Convert.IsDBNull(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value) == true)
                        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = 0;


                    if (Convert.IsDBNull(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[4].Value) != false)
                        dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[4].Value = 0;


                    if (Convert.IsDBNull(dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value) != false)
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = 0;
                    if (Convert.IsDBNull(dataGridView2.Rows[i].Cells[3].Value) != false)
                        dataGridView2.Rows[i].Cells[3].Value = 0;

                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value = (Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value) + Convert.ToDouble(dataGridView2.Rows[i].Cells[2].Value)).ToString("0.00");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value = (Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value) + Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value)).ToString("0.00");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[4].Value = (Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[4].Value) + Convert.ToDouble(dataGridView2.Rows[i].Cells[4].Value)).ToString("0.00");

                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[2].Value = (Math.Round(Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[2].Value))).ToString("0.00");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[3].Value = (Math.Round(Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[3].Value))).ToString("0.00");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[4].Value = (Math.Round(Convert.ToDouble(dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[4].Value))).ToString("0.00");


                }

                dataGridView2.Rows[dataGridView2.Rows.Count - 2].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                //dataGridView2.Rows[dataGridView2.Rows.Count - 2].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                //dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                dataGridView2.Rows[dataGridView2.Rows.Count - 2].Cells[1].Value = "Total";
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = "amD$§S> Am°µµ\\$";
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "Branch Name")
            {
                e.CellStyle.Font = ObjFunction.GetFont();

            }
        }

        private void GridViewDaily_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {

            //if (GridViewDaily.Rows.Count > 0)
            //{
            //    string s = GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[1].Value.ToString();
            //    for (int i = 0; i < GridViewDaily.Rows.Count - 1; i++)
            //    {
            //        if (GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[1].Value.ToString() == "EHy$U")
            //        {                       
            //                e.CellStyle.Font = new Font("OM-DEV-0714E", 14);

            //        }
            //    }
            //}
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
