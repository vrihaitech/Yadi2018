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
    public partial class SalesRegister : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        
        public long CompNo, LedgNo, MNo;
        public long VchNo;
        public string RptTitle;
        private long VoucherType;
        private int voucherno;

        public SalesRegister()
        {
            InitializeComponent();
        }

        public SalesRegister(long VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
        }

        private void SalesRegister_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            btnVatReg.Visible = false;
            if (VoucherType == VchType.Contra)
            {
                VchNo = 1; lblRegister.Text = "Contra Entry Register";
                label3.Text = "Contra Voucher Entry Details By Date";
                tabPage1.Text = "Contra Register";
                tabPage2.Text = "Contra Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.Sales)
            {
                VchNo = VchType.Sales; lblRegister.Text = "Sales Entry Register";
                label3.Text = "Sales Voucher Entry Details By Date";
                tabPage1.Text = "Sales Register";
                tabPage2.Text = "Sales Register Voucher Entry Details";
                btnVatReg.Visible = true;
            }
            else if (VoucherType == VchType.Purchase)
            {
                VchNo = VchType.Purchase; lblRegister.Text = "Purchase Entry Register";
                label3.Text = "Purchase Voucher Entry Details By Date";
                tabPage1.Text = "Purchase Register";
                tabPage2.Text = "Purchase Register Voucher Entry Details";
                btnVatReg.Visible = true;
            }
            else if (VoucherType == VchType.Receipt)
            {
                VchNo = VchType.Receipt; lblRegister.Text = "Receipt Entry Register";
                label3.Text = "Receipt Voucher Entry Details By Date";
                tabPage1.Text = "Receipt Register";
                tabPage2.Text = "Receipt Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.Payment)
            {
                VchNo = VchType.Payment; lblRegister.Text = "Payment Entry  Register";
                label3.Text = "Payment Voucher Entry Details By Date";
                tabPage1.Text = "Payment Register";
                tabPage2.Text = "Payment Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.CreditNote)
            {
                VchNo = VchType.CreditNote; lblRegister.Text = "Credit Entry  Register";
                label3.Text = "Credit Voucher Entry Details By Date";
                tabPage1.Text = "Credit Register";
                tabPage2.Text = "Credit Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.DebitNote)
            {
                VchNo = VchType.DebitNote; lblRegister.Text = "Debit Entry  Register";
                label3.Text = "Debit Voucher Entry Details By Date";
                tabPage1.Text = "Debit Register";
                tabPage2.Text = "Debit Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.Journal)
            {
                VchNo = VchType.Journal; lblRegister.Text = "Journal Entry  Register";
                label3.Text = "Debit Journal Entry Details By Date";
                tabPage1.Text = "Journal Register";
                tabPage2.Text = "Journal Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.CashDepositeInBank)
            {
                VchNo = VchType.CashDepositeInBank; lblRegister.Text = "Cash Deposite Entry Register";
                label3.Text = "Cash Deposite Voucher Entry Details By Date";
                tabPage1.Text = "Cash Deposite Register";
                tabPage2.Text = "Cash Deposite Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.CashWithdrawalFromBank)
            {
                VchNo = VchType.CashWithdrawalFromBank; lblRegister.Text = "Cash Withdrawal Entry Register";
                label3.Text = "Cash Withdrawal Voucher Entry Details By Date";
                tabPage1.Text = "Cash Withdrawal Register";
                tabPage2.Text = "Cash Withdrawal Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.CashReceipt)
            {
                VchNo = VchType.CashReceipt; lblRegister.Text = "Cash Receipt Entry Register";
                label3.Text = "Cash Receipt Voucher Entry Details By Date";
                tabPage1.Text = "Cash Receipt Register";
                tabPage2.Text = "Cash Receipt Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.BankReceipt)
            {
                VchNo = VchType.BankReceipt; lblRegister.Text = "Bank Receipt Entry Register";
                label3.Text = "Bank Receipt Voucher Entry Details By Date";
                tabPage1.Text = "Bank Receipt Register";
                tabPage2.Text = "Bank Receipt Register Voucher Entry Details";
            }
            else if (VoucherType == VchType.BankPayment)
            {
                VchNo = VchType.BankReceipt; lblRegister.Text = "Bank Payment Entry Register";
                label3.Text = "Bank Payment Voucher Entry Details By Date";
                tabPage1.Text = "Bank Payment Register";
                tabPage2.Text = "Bank Payment Register Voucher Entry Details";
            }
            //lblHead.Text = lblRegister.Text;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (TabSalesRegister.SelectedTab == tabPage1)
                {
                    dsVd = ObjDset.FillDset("New", "Exec GetVoucherDetails 1," + VchNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);

                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    DataGridView2.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView2.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DataGridView2.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GetCount();
                    if (DataGridView2.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;
                }
                else
                {
                    MNo = 0; // Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                    if (VchNo == VchType.Sales)
                        dsVd = ObjDset.FillDset("New", "Exec GetSaleVouchEntryDtlsByDate " + VchNo + ", 0, " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    else if (VchNo == VchType.Purchase)
                        dsVd = ObjDset.FillDset("New", "Exec GetPurchVouchEntryDtlsByDate " + VchNo + ", 0, " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    else
                        dsVd = ObjDset.FillDset("New", "Exec GetVouchEntryDtlsByDate " + VchNo + ", 0, " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);

                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;

                    if (VchNo != VchType.Sales)
                    {
                        GetVoucherEntryCount();
                    }
                    if (GridViewDaily.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            //TabSalesRegister.SelectedTab = tabPage1;
        }

        

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[1].Selected == false)
                {
                    TabSalesRegister.SelectedTab = tabPage2;
                    MNo = Convert.ToInt64(DataGridView2.CurrentRow.Cells[0].Value);
                    //if (VchNo == VchType.Sales)
                    //    dsVd = ObjDset.FillDset("New", "Exec GetSaleVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    //else if (VchNo == VchType.Purchase)
                    //    dsVd = ObjDset.FillDset("New", "Exec GetPurchVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);
                    //else
                    dsVd = ObjDset.FillDset("New", "Exec GetVouchEntryDtlsByDate " + VchNo + "," + MNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);

                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    GridViewDaily.DataSource = dsVd.Tables[0].DefaultView;
                    GridViewDaily.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridViewDaily.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (VchNo != VchType.Sales)
                    {
                        GetVoucherEntryCount();
                    }

                    if (GridViewDaily.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else btnPrint.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TabSalesRegister_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (TabSalesRegister.SelectedTab == tabPage1)
                {
                    dsVd = ObjDset.FillDset("New", "Exec GetVoucherDetails 1," + VchNo + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonFunctions.ConStr);

                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);

                    DataGridView2.DataSource = dsVd.Tables[0].DefaultView;
                    GetCount();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GridViewDaily_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
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
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[2].Value)).ToString(Format.DoubleFloating);
                    DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value = Convert.ToDouble(Convert.ToDouble(DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[3].Value) + Convert.ToDouble(DataGridView2.Rows[i].Cells[3].Value)).ToString(Format.DoubleFloating);
                }
            }

            //===========Total At footer===========
            DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView2.Rows[DataGridView2.Rows.Count - 1].DefaultCellStyle.Font =ObjFunction.GetFont() ;
            DataGridView2.Rows[DataGridView2.Rows.Count - 1].Cells[1].Value = "Total";
        }

        public void GetVoucherEntryCount()
        {
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
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value = Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[5].Value)).ToString(Format.DoubleFloating);
                    GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value = Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[6].Value)).ToString(Format.DoubleFloating);
                    if (VchNo == VchType.Sales || VchNo == VchType.Purchase)
                    {
                        if (Convert.IsDBNull(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value) != false)
                            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value = 0;

                        GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value = Convert.ToDouble(Convert.ToDouble(GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[7].Value) + Convert.ToDouble(GridViewDaily.Rows[i].Cells[7].Value)).ToString(Format.DoubleFloating);
                    }
                }
            }

            //===========Total At footer===========
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
            GridViewDaily.Rows[GridViewDaily.Rows.Count - 1].Cells[4].Value = "Total";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                if (TabSalesRegister.SelectedIndex == 0)
                {
                    ReportSession = new string[6];
                    ReportSession[0] = "0";
                    ReportSession[1] = VchNo.ToString();
                    ReportSession[2] = DBGetVal.FirmNo.ToString();
                    ReportSession[3] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[5] = tabPage1.Text + " Details";
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByReg(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewVoucherDtlsByReg.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                }
                else
                {
                    ReportSession = new string[6];
                    ReportSession[0] = VchNo.ToString();
                    ReportSession[1] = MNo.ToString();
                    ReportSession[2] = DBGetVal.FirmNo.ToString();
                    ReportSession[3] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[4] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[5] = tabPage1.Text + " Voucher Details";

                    if (VchNo == VchType.Sales)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewSalesRegDtls(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewSalesRegDtls.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else if (VchNo == VchType.Purchase)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewPurchaseRegDtls(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewPurchaseRegDtls.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.ViewVoucherDtlsByRegDate(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewVoucherDtlsByRegDate.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnVatReg_Click(object sender, EventArgs e)
        {
            Form childForm = new Display.VatRegister(VoucherType);
            if (VoucherType == VchType.Sales)
                childForm.Text = "Sales Vat Register";
            else if (VoucherType == VchType.Purchase)
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
                BtnShow.Focus();
            }
        }

        private void GridViewDaily_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            voucherno = Convert.ToInt32(GridViewDaily.CurrentRow.Cells[3].Value);

            Form newf = new VoucherViewAE(0, voucherno);
            ObjFunction.OpenForm(newf);
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
