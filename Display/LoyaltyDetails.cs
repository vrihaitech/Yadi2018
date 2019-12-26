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
    public partial class LoyaltyDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VoucherType;
        public int voucherno;

        public static string LedgName, RptTitle;
        public static int Type;

        public LoyaltyDetails()
        {
            InitializeComponent();
        }

        private void LoyaltyDetails_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;

            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;

            //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
            //    BtnExport.Visible = true;
            //else
            //    BtnExport.Visible = false;
        }

        private void rd_CheckedChanged(object sender, EventArgs e)
        {
            if (dgInstant != null)
            {
                while (dgInstant.Rows.Count > 0)
                    dgInstant.Rows.RemoveAt(0);
            }
            if (rdInstant.Checked == true)
            {
                pnlInstant.Visible = true;
                BindData("2,3,4");
            }
            else
            {
                pnlInstant.Visible = true;
                BindData("1,5");
            }
        }

        public void BindData(string SchTypeNo)
        {
            try
            {
                string sql =  " SELECT DISTINCT  0 As srno, MSchemeType.SchemeTypeNo, "+
                              " MSchemeType.SchemeTypeName + '-' + MSchemeType.InitalChar AS SchemeType,MScheme.SchemePeriodFrom, "+
                              " MScheme.SchemePeriodTo, MScheme.SchemeDate, 'False' AS 'Select', "+
                              " MScheme.SchemeNo  FROM MScheme INNER JOIN MSchemeType ON MScheme.SchemeTypeNo = MSchemeType.SchemeTypeNo "+
                              " INNER JOIN TRewardDetails ON MScheme.SchemeNo = TRewardDetails.SchemeNo  "+
                              " WHERE (((MScheme.SchemePeriodFrom >='" + DTPFromDate.Text + "') and (MScheme.SchemePeriodFrom <= '" + DTToDate.Text + "')) or " +
                              " ((MScheme.SchemePeriodTo >='" + DTPFromDate.Text + "') and (MScheme.SchemePeriodTo <= '" + DTToDate.Text + "'))) " +
                              " AND (MSchemeType.SchemeTypeNo IN (" + SchTypeNo + ")) and MScheme.isactive in (1,2)  ORDER BY MSchemeType.SchemeTypeNo "; 
                    
                    //" SELECT DISTINCT  0 As srno, MSchemeType.SchemeTypeNo, MSchemeType.SchemeTypeName + '-' + MSchemeType.InitalChar AS SchemeType,MScheme.SchemePeriodFrom, MScheme.SchemePeriodTo, MScheme.SchemeDate, 'False' AS 'Select', MScheme.SchemeNo " +
                    //         " FROM MScheme INNER JOIN MSchemeType ON MScheme.SchemeTypeNo = MSchemeType.SchemeTypeNo INNER JOIN TRewardDetails ON MScheme.SchemeNo = TRewardDetails.SchemeNo " +
                    //         " WHERE (MScheme.SchemePeriodFrom >='" + DTPFromDate.Text + "') AND (MScheme.SchemePeriodFrom <= '" + DTToDate.Text + "') AND (MSchemeType.SchemeTypeNo IN (" + SchTypeNo + ")) ORDER BY MSchemeType.SchemeTypeNo ";
                dgInstant.DataSource = ObjFunction.GetDataView(sql).Table;
                if (dgInstant != null)
                {
                    if (dgInstant.Rows.Count != 0)
                        dgInstant.CurrentCell = dgInstant[6, 0];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DTToDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

        private void dgInstant_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;
            }
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                e.Value = Convert.ToDateTime(e.Value.ToString()).ToString("dd-MMM-yyyy");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (rdInstant.Checked == true)
            //{
            try
            {
                string str = "";
                for (int i = 0; i < dgInstant.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgInstant.Rows[i].Cells[6].FormattedValue) == true)
                    {
                        if (str == "")
                            str = dgInstant.Rows[i].Cells[7].Value.ToString();
                        else
                            str += "," + dgInstant.Rows[i].Cells[7].Value.ToString();
                    }
                }
                if (str != "")
                {
                    string[] ReportSession = new string[6];
                    Form NewF = null;
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString(Format.DDMMMYYYY);
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString(Format.DDMMMYYYY);
                    ReportSession[2] = "0";
                    ReportSession[3] = DBGetVal.FirmNo.ToString();
                    ReportSession[4] = str;
                    ReportSession[5] = (chkIsIwScheme.Checked == true) ? "1" : "0";
                    if (rbDetails.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if(chkIsIwScheme.Checked==false)
                            NewF = new Display.ReportViewSource(new Reports.GetLoyaltyDetails(), ReportSession);
                            else
                            NewF = new Display.ReportViewSource(new Reports.GetLoyaltyDetailsIw(), ReportSession);
                        }
                        else
                        {
                            if (chkIsIwScheme.Checked == false)
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltyDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltyDetailsIw.rpt", CommonFunctions.ReportPath), ReportSession);

                        }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rbSummary.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (chkIsIwScheme.Checked == false)
                                NewF = new Display.ReportViewSource(new Reports.GetLoyaltySummary(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(new Reports.GetLoyaltySummaryIw(), ReportSession);
                      
                        }
                        else
                        {
                            if(chkIsIwScheme.Checked == false)
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltySummary.rpt", CommonFunctions.ReportPath), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltySummaryIw.rpt", CommonFunctions.ReportPath), ReportSession);

                        }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdDayDetails.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (chkIsIwScheme.Checked == false)
                                NewF = new Display.ReportViewSource(new Reports.GetLoyaltyDayDetails(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(new Reports.GetLoyaltyDayDetailsIw(), ReportSession);
                        }
                        else
                        {
                            if (chkIsIwScheme.Checked == false)
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltyDayDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltyDayDetailsIw.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rdPartyDetails.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (chkIsIwScheme.Checked == false)
                            NewF = new Display.ReportViewSource(new Reports.GetLoyaltyPartyDetails(), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(new Reports.GetLoyaltyPartyDetailsIw(), ReportSession);
                        }
                        else
                        {
                            if (chkIsIwScheme.Checked == false)
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltyPartyDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                            else
                                NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetLoyaltyPartyDetailsIw.rpt", CommonFunctions.ReportPath), ReportSession);
                        }
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                    OMMessageBox.Show("Please Click One CheckBox.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            //}

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
