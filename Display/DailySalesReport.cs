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
    public partial class DailySalesReport : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Security secure = new Security();
        DataSet dsVd = new DataSet();

        public long CompNo, LedgNo, MNo;
        public int VchNo;
        public string RptTitle;
        private int VoucherType;
       // private String serverName; 



        public DailySalesReport(int VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
        }

        private void DailySalesRegister_Load(object sender, EventArgs e)
        {


            CompNo = 1;
            
            if (VoucherType == 1)
            {
               // VchNo = 1; lblRegister.Text = "Contra Entry Register";
               // label3.Text = "Contra Voucher Entry Details By Date";
              //  tabPage1.Text = "Contra Register";
               // tabPage2.Text = "Contra Register Voucher Entry Details";
            }
            else if (VoucherType == 16)
            {
                VchNo = 16; //lblRegister.Text = "Sales Entry Report";
              //  label3.Text = "Sales Voucher Entry Details By Date";
               // tabPage1.Text = "Sales Register";
             //   tabPage2.Text = "Sales Register Voucher Entry Details";
               
            }
            else if (VoucherType == 9)
            {
                VchNo = 9;// lblRegister.Text = "Purchase Entry Register";
               // label3.Text = "Purchase Voucher Entry Details By Date";
               // tabPage1.Text = "Purchase Register";
              //  tabPage2.Text = "Purchase Register Voucher Entry Details";
               
            }
            else if (VoucherType == 11)
            {
                VchNo = 11;// lblRegister.Text = "Receipt Entry Register";
              //  label3.Text = "Receipt Voucher Entry Details By Date";
               // tabPage1.Text = "Receipt Register";
              //  tabPage2.Text = "Receipt Register Voucher Entry Details";
            }
            else if (VoucherType == 7)
            {
                VchNo = 7;// lblRegister.Text = "Payment Entry  Register";
              //  label3.Text = "Payment Voucher Entry Details By Date";
               // tabPage1.Text = "Payment Register";
              //  tabPage2.Text = "Payment Register Voucher Entry Details";
            }
            else if (VoucherType == 2)
            {
                VchNo = 2;// lblRegister.Text = "Credit Entry  Register";
              //  label3.Text = "Credit Voucher Entry Details By Date";
               // tabPage1.Text = "Credit Register";
             //   tabPage2.Text = "Credit Register Voucher Entry Details";
            }
            else if (VoucherType == 3)
            {
                VchNo = 3;// lblRegister.Text = "Debit Entry  Register";
              //  label3.Text = "Debit Voucher Entry Details By Date";
               // tabPage1.Text = "Debit Register";
             //   tabPage2.Text = "Debit Register Voucher Entry Details";
            }
         //   lblHead.Text = lblRegister.Text;
            DTPDate.Format = DateTimePickerFormat.Custom;
            DTPDate.CustomFormat = "dd-MMM-yyyy";
            DTPDate.Text = DBGetVal.FromDate.ToString("dd-MMM-yyyy");
            //DTToDate.Format = DateTimePickerFormat.Custom;
            //DTToDate.CustomFormat = "dd-MMM-yyyy";
          //  DTToDate.Text = DBGetVal.ToDate.ToString("dd-MMM-yyyy");


        }

      

        private void BtnShow_Click(object sender, EventArgs e)
        {
            
            //if (TabSalesRegister.SelectedTab == tabPage1)
            //{
            //    dsVd = ObjDset.FillDset("New", "Exec GetVoucherDetails 1," + VchNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);

            //    DataTable dt = dsVd.Tables[0];
            //    DataRow dr = dt.NewRow();
            //    dsVd.Tables[0].Rows.Add(dr);

            //    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
            //    GetCount();
            //    if (DataGridView2.Rows.Count > 0)
            //        btnPrint.Visible = true;
            //    else btnPrint.Visible = false;
            //}
            //else
            //{
            //    MNo = 0; // Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
            //    if (VchNo == 16)
            //        dsVd = ObjDset.FillDset("New", "Exec GetSaleVouchEntryDtlsByDate " + VchNo + ", 0, " + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);
            //    else if (VchNo == 9)
            //        dsVd = ObjDset.FillDset("New", "Exec GetPurchVouchEntryDtlsByDate " + VchNo + ", 0, " + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);
            //    else
            //        dsVd = ObjDset.FillDset("New", "Exec GetVouchEntryDtlsByDate " + VchNo + ", 0, " + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);

            //    DataTable dt = dsVd.Tables[0];
            //    DataRow dr = dt.NewRow();
            //    dsVd.Tables[0].Rows.Add(dr);

            //    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;

            //    if (VchNo != 16)
            //    {
            //        GetVoucherEntryCount();
            //    }
            //    if (GridViewDaily.Rows.Count > 0)
            //        btnPrint.Visible = true;
            //    else btnPrint.Visible = false;
            //}


          //  TabSalesRegister.SelectedTab = tabPage2;
           // MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);

            try
            {
                if (VchNo == 16)
                    dsVd = ObjDset.FillDset("New", "Exec GetSaleVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);
                else if (VchNo == 9)
                    dsVd = ObjDset.FillDset("New", "Exec GetPurchVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);
                else
                    dsVd = ObjDset.FillDset("New", "Exec GetVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);



                DataTable dt = dsVd.Tables[0];
                DataRow dr = dt.NewRow();
                dsVd.Tables[0].Rows.Add(dr);

                GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;
                GridViewDaily.Columns[2].Visible = false;
                if (VchNo != 16)
                {
                    GetVoucherEntryCount();
                }

                if (GridViewDaily.Rows.Count > 0)
                    btnPrint.Visible = true;
                else btnPrint.Visible = false;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GetVoucherEntryCount()
        {
            //throw new NotImplementedException();

            for (int i = 0; i < GridViewDaily.Rows.Count - 1; i = i + 1)
            {
                if (GridViewDaily.Rows[i].Index != GridViewDaily.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = 0;

                    if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) != false)
                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = 0;
                    if (Convert.IsDBNull(GridViewDaily.Rows[i].Cells[5].Value) != false)
                        GridViewDaily.Rows[i].Cells[5].Value = 0;
                    if (Convert.IsDBNull(GridViewDaily.Rows[i].Cells[6].Value) != false)
                        GridViewDaily.Rows[i].Cells[6].Value = 0;
                    //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[0].Value = 100;
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value);
                    if (VchNo == 16 || VchNo == 9)
                    {
                        if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value) != false)
                            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value = 0;

                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value = Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[7].Value);
                    }
                }
            }

            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = new Font("OM-DEV-0714", 15, FontStyle.Bold);
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[4].Value = "EHy$U";

        }

       //private void GetCount()
       // {
       //     //throw new NotImplementedException();


       //     for (int i = 0; i < DataGridView2.Rows.Count - 1; i = i + 1)
       //     {
       //         if (DataGridView2.Rows[i].Index != DataGridView2.Rows.Count - 1)
       //         {
       //             if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value) != false)
       //                 DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value = 0;

       //             if (Convert.IsDBNull(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value) != false)
       //                 DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value = 0;

       //             DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[0].Value = 100;
       //             DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[2].Value);
       //             DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[3].Value);
       //         }
       //     }

       //     //===========Total At footer===========
       //     DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
       //     DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new Font("OM-DEV-0714", 15, FontStyle.Bold);
       //     DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[1].Value = "Total";
       // }

        private void DataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {


            //if (this.DataGridView2.Columns[e.ColumnIndex].Name == "Particulars")
            //{
            //    e.CellStyle.Font = new Font("Verdana", 10);
            //}


        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {


         //   TabSalesRegister.SelectedTab = tabPage2;
           // MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
            if (VchNo == 16)
                dsVd = ObjDset.FillDset("New", "Exec GetSaleVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);
            else if (VchNo == 9)
                dsVd = ObjDset.FillDset("New", "Exec GetPurchVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);
            else
                dsVd = ObjDset.FillDset("New", "Exec GetVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);

            DataTable dt = dsVd.Tables[0];
            DataRow dr = dt.NewRow();
            dsVd.Tables[0].Rows.Add(dr);

            GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;

            if (VchNo != 16)
            {
                GetVoucherEntryCount();
            }

            if (GridViewDaily.Rows.Count > 0)
                btnPrint.Visible = true;
            else btnPrint.Visible = false;


        }

        private void TabSalesRegister_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (TabSalesRegister.SelectedTab == tabPage1)
            //{
            //    dsVd = ObjDset.FillDset("New", "Exec GetVoucherDetails 1," + VchNo + "," + CompNo + ",'" + DTPDate.Text + "','" + DTPDate.Text + "'", CommonFunctions.ConStr);

            //    DataTable dt = dsVd.Tables[0];
            //    DataRow dr = dt.NewRow();
            //    dsVd.Tables[0].Rows.Add(dr);

            //    //DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
            //    //GetCount();
            //}


        }


        private void GridViewDaily_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherTypeName")
            {
                e.CellStyle.Font = new Font("Verdana", 10);

            }
            
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherDate")
            {
                this.GridViewDaily.Columns[e.ColumnIndex].Width = 120;
                if (e.Value.ToString() == "1-1-1900")
                    e.Value = "";
              
            }
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "LedgerName")
            {
                this.GridViewDaily.Columns[e.ColumnIndex].Width = 200;
            }
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherNo")
            {
                this.GridViewDaily.Columns[e.ColumnIndex].Visible = false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                //if (TabSalesRegister.SelectedIndex == 0)
                //{
                //    ReportSession = new string[6];
                //    ReportSession[0] = "0";
                //    ReportSession[1] = VchNo.ToString();
                //    ReportSession[2] = DBGetVal.FirmNo.ToString();
                //    ReportSession[3] = Convert.ToDateTime(DTPDate.Text).ToString("dd-MMM-yyyy");
                //    ReportSession[4] = Convert.ToDateTime(DTPDate.Text).ToString("dd-MMM-yyyy");
                //   // ReportSession[5] = tabPage1.Text + " Details";

                //    Form NewF = new Display.ReportViewSource("ViewVoucherDtlsByReg", ReportSession);
                //    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                //}
                //else
                //{
                ReportSession = new string[6];
                ReportSession[0] = VchNo.ToString();
                ReportSession[1] = MNo.ToString();
                ReportSession[2] = DBGetVal.FirmNo.ToString();
                ReportSession[3] = Convert.ToDateTime(DTPDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[4] = Convert.ToDateTime(DTPDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[5] = "Daily Sales Details";// +" Voucher Details";
                Form NewF;
                if (VchNo == 16)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource("ViewSalesRegDtls", ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewSalesRegDtls.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                else if (VchNo == 9)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource("ViewPurchaseRegDtls", ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewPurchaseRegDtls.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                else
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource("ViewVoucherDtlsByRegDate", ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewVoucherDtlsByRegDate.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
           // }
        }

        private void btnVatReg_Click(object sender, EventArgs e)
        {
            Form childForm = new Display.VatRegister(VoucherType);
            if (VoucherType == 16)
                childForm.Text = "Sales Vat Register";
            else if (VoucherType == 9)
                childForm.Text = "Purchase Vat Register";
            else
                childForm.Text = "Vat Register";
            ObjFunction.OpenForm(childForm, DBGetVal.MainForm);
        }

        private void DTPDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                DTPDate.Focus();
            }
        }

        //private void GridViewDaily_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherTypeName")
        //    {
        //        e.CellStyle.Font = new Font("Verdana", 10);

        //    }

        //    if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherDate")
        //    {
        //        this.GridViewDaily.Columns[e.ColumnIndex].Width = 120;
        //        if (e.Value.ToString() == "1-1-1900")
        //            e.Value = "";

        //    }
        //    if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "LedgerName")
        //    {
        //        this.GridViewDaily.Columns[e.ColumnIndex].Width = 200;
        //    }
        //    if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherNo")
        //    {
        //        this.GridViewDaily.Columns[e.ColumnIndex].Visible = false;
        //    }
        //}

        private void GridViewDaily_CellFormatting_2(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherTypeName")
            {
                e.CellStyle.Font = new Font("Verdana", 10);

            }

            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherDate")
            {
                this.GridViewDaily.Columns[e.ColumnIndex].Width = 120;
                if (e.Value.ToString() == "1-1-1900")
                    e.Value = "";

            }
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "LedgerName")
            {
                this.GridViewDaily.Columns[e.ColumnIndex].Width = 200;
            }
            if (this.GridViewDaily.Columns[e.ColumnIndex].Name == "VoucherNo")
            {
                this.GridViewDaily.Columns[e.ColumnIndex].Visible = false;
            }
        }


      

    }
}
