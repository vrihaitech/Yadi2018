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
    public partial class StockGrpSummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBProgressBar PB;
        DataSet dsVd = new DataSet();
        public long CompNo, GroupNo;
        public string GroupNm;
        
        public StockGrpSummary()
        {
            InitializeComponent();
        }

        private void StockGrpSummary_Load(object sender, EventArgs e)
        {
            
            CompNo = 1;
            ObjFunction.FillCombo(cmbGroupNm, "SELECT StockGroupNo, StockGroupName FROM MStockGroup");

            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroupNm.Text != "")
                {
                    EP.SetError(cmbGroupNm, "");
                    if (cmbGroupNm.SelectedIndex != 0)
                    {
                        btnPrint.Visible = false;
                        DataGridView1.Visible = false;
                        PB = new DBProgressBar(this);
                        PB.TimerStart();
                        PB.Ctrl = DataGridView1;
                        //dsVd = ObjDset.FillDset("New", "Exec GroupWiseClosingStock " + cmbGroupNm.SelectedValue + "," + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'", CommonClass.CommonFunctions.ConStr);
                        dsVd = ObjDset.FillDset("New", "Exec GetStockAllItemGroupwise " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + cmbGroupNm.SelectedValue + "", CommonFunctions.ConStr);
                        DataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        DataTable dt = dsVd.Tables[0];
                        DataRow dr = dt.NewRow();
                        dsVd.Tables[0].Rows.Add(dr);
                        DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                        if (DataGridView1.Rows.Count > 1)
                            btnPrint.Visible = true;
                        else btnPrint.Visible = false;
                        GetCount();
                    }
                    else
                    {
                        EP.SetError(cmbGroupNm, "Please Select Stock Groups");
                        EP.SetIconAlignment(cmbGroupNm, ErrorIconAlignment.MiddleRight);
                    }
                }
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
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value = 0;

                    //DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[0].Value = 100;
                    DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[4].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[4].Value);
                }
            }

            //===========Total At footer===========

            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font = ObjFunction.GetFont();//new Font("OM-DEV-0714", 15, FontStyle.Bold);
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = "Total";
        }      

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession = new string[5];
                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = cmbGroupNm.SelectedValue.ToString();
                ReportSession[4] = cmbGroupNm.Text;
                Form NewF = null;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.ViewStockGrpSummary(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewStockGrpSummary.rpt", CommonFunctions.ReportPath), ReportSession);
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
                cmbGroupNm.Focus();
            }
        }

        private void cmbGroupNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbGroupNm, e, true);
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                BtnShow.Focus();
            }
        }

        public void GridNull()
        {
            DataGridView1.DataSource = null;
            btnPrint.Visible = false;
            EP.SetError(cmbGroupNm, "");
        }

        private void cmbGroupNm_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridNull();
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
