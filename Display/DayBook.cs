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
    public partial class DayBook : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public static long CompNo;
        int RType = 0;
        long MfgCompNo = 0;
        string MfgCompName = "";
        //int voucherno;

        public DayBook()
        {
            MfgCompNo = 0;
            InitializeComponent();
        }
        public DayBook(int type)
        {
            RType = type;
            InitializeComponent();
        }
               
        private void DayBook_Load(object sender, EventArgs e)
        {
            lblFirmName.Text = "";
            if (RType != 0)
            {
                Form NewFrm = new Vouchers.FirmSelection();
                ObjFunction.OpenForm(NewFrm);
                MfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                MfgCompName = ((Vouchers.FirmSelection)NewFrm).MfgCompName;
                lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
            }
            CompNo = DBGetVal.FirmNo; btnPrint.Visible = false;
            DTPFromDate.Text = DBGetVal.ServerTime.ToString(Format.DDMMMYYYY);
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dsVd = ObjDset.FillDset("New", "exec GetVoucherDetailsByDays '" + DTPFromDate.Text + "'," + CompNo + "," + MfgCompNo + "", CommonFunctions.ConStr);
                DataGridView1.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataTable dt = dsVd.Tables[0];
                DataRow dr = dt.NewRow();
                dsVd.Tables[0].Rows.Add(dr);
                DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                if (DataGridView1.Rows.Count > 1)
                    btnPrint.Visible = true;
                else
                    btnPrint.Visible = false;

                GetCount();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
            //DataGridView1.Columns[4].Visible = false;

        }

        public void GetCount()
        {
            for (int i = 0; i < DataGridView1.Rows.Count - 1; i = i + 1)
            {
                if (DataGridView1.Rows[i].Index != DataGridView1.Rows.Count - 1)
                {
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value = 0;
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value = 0;
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value =Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[6].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[6].Value)).ToString(Format.DoubleFloating);
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value =Convert.ToDouble(Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[5].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[5].Value)).ToString(Format.DoubleFloating);
                }
            }

            //===========Total At footer===========
            if (DataGridView1.Rows.Count > 1)
            {
                DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
                //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();
                DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = "Total";
            }
        }

       

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession = new string[4];
                Form NewF = null;
                ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[1] = DBGetVal.FirmNo.ToString();
                ReportSession[2] = MfgCompNo.ToString();
                ReportSession[3] = MfgCompName.ToString();
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewDailyVoucherDtls(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewDailyVoucherDtls.rpt", CommonFunctions.ReportPath), ReportSession);
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
                BtnShow.Focus();
            } 
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //voucherno = Convert.ToInt32(DataGridView1.CurrentRow.Cells[4].Value);

            //Form newf = new VoucherViewAE(0, voucherno);
            //ObjFunction.OpenForm(newf);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
