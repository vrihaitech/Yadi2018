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
    public partial class ViewMinMaxLevel : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DBProgressBar PB;
        int valActDeAct = 0;
        long MfgCompNo = 0;

        public ViewMinMaxLevel()
        {
            InitializeComponent();
        }

        public ViewMinMaxLevel(long MfgCompNo)
        {
            InitializeComponent();
            this.MfgCompNo = MfgCompNo;
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
                //dsVd = ObjDset.FillDset("New", "Exec ViewMaxMinLevel 1,'" + s.Text + "','" + DTToDate.Text + "' ", CommonFunctions.ConStr);
                
                if (rdActive.Checked == true) valActDeAct = 0;
                else if (rdDeActive.Checked == true) valActDeAct = 1;
                else if (rdActiveDeActive.Checked == true) valActDeAct = -1;
                if (MfgCompNo == 0)
                    dsVd = ObjDset.FillDset("New", "Exec RptMaxMinLevel 1,'" + s.Text + "','" + DTToDate.Text + "'," + valActDeAct + " ", CommonFunctions.ConStr);
                else
                    dsVd = ObjDset.FillDset("New", "Exec RptMaxMinLevelFirm 1,'" + s.Text + "','" + DTToDate.Text + "'," + valActDeAct + "," + MfgCompNo + " ", CommonFunctions.ConStr);
                for (int i = 2; i < 7; i++)
                    DataGridView1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DataTable dt = dsVd.Tables[0];
                DataRow dr = dt.NewRow();
                dsVd.Tables[0].Rows.Add(dr);

                DataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                //DataGridView1.Columns["OrdQty"].Visible = false;
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
                   
                    if (Convert.IsDBNull(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) != false)
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = 0;
                        DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value = Convert.ToDouble(DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[2].Value) + Convert.ToDouble(DataGridView1.Rows[i].Cells[2].Value);
                    }
            }

            //===========Total At footer===========
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = System.Drawing.Color.SkyBlue;
            //DataGridView1.Rows[DataGridView1.Rows.Count - 1].DefaultCellStyle.Font =ObjFunction.GetFont() ;
            DataGridView1.Rows[DataGridView1.Rows.Count - 1].Cells[1].Value = "Total";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                if (MfgCompNo == 0) ReportSession = new string[4];
                else ReportSession = new string[6];

                ReportSession[0] = DBGetVal.FirmNo.ToString();
                ReportSession[1] = Convert.ToDateTime(s.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = valActDeAct.ToString();
                Form NewF = null;
                if (MfgCompNo == 0)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewMinMaxLevel(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewMinMaxLevel.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                else
                {
                    ReportSession[4] = MfgCompNo.ToString();
                    ReportSession[5] = lblFirmName.Text.Replace("Firm Name :", "");
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.ViewMinMaxLevelFirm(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("ViewMinMaxLevelFirm.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void ViewMinMaxLevel_Load(object sender, EventArgs e)
        {
            lblFirmName.Text = "";
            if (MfgCompNo != 0)
            {
                Form NewFrm = new Vouchers.FirmSelection();
                ObjFunction.OpenForm(NewFrm);
                MfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
            }

            btnPrint.Visible = false;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.RowIndex < DataGridView1.Rows.Count-1)
                {
                    if (Convert.IsDBNull(DataGridView1.Rows[e.RowIndex].Cells[3].Value) == false)
                    {
                        if (Convert.ToDouble(DataGridView1.Rows[e.RowIndex].Cells[3].Value) > Convert.ToDouble(DataGridView1.Rows[e.RowIndex].Cells[2].Value))
                            e.CellStyle.BackColor = Color.Yellow;
                    }
                    if(Convert.IsDBNull(DataGridView1.Rows[e.RowIndex].Cells[3].Value)==false)
                    {
                    if (Convert.ToDouble(DataGridView1.Rows[e.RowIndex].Cells[4].Value) < Convert.ToDouble(DataGridView1.Rows[e.RowIndex].Cells[2].Value))
                        e.CellStyle.BackColor = Color.Red;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void s_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = s.Value;
        }

        private void rdActive_CheckedChanged(object sender, EventArgs e)
        {
            while (DataGridView1.Rows.Count > 0)
                DataGridView1.Rows.RemoveAt(0);
        }

       
       
        }
      

    }




