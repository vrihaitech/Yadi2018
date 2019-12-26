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
    public partial class PartywiseGRNDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataTable dtParty = new DataTable();
        DBProgressBar PB;

        public long CompNo, ItNo, MNo, Type1, No, ItNo1,BItemNo;
        public string ItName, RptTitle, ItNm;
        string  strLedgerNo="";

        public PartywiseGRNDetails()
        {
            InitializeComponent();
        }

        private void StockSummary_Load(object sender, EventArgs e)
        {           
            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            KeyDownFormat(this.Controls);
            
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                strLedgerNo = "";
                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvParty.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strLedgerNo == "")
                            strLedgerNo = gvParty.Rows[i].Cells[0].Value.ToString();
                        else
                            strLedgerNo = strLedgerNo + "," + gvParty.Rows[i].Cells[0].Value.ToString();
                    }
                }
                if (strLedgerNo != "")
                {
                    string[] ReportSession;

                    ReportSession = new string[4];
                  
                    ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[1] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                    ReportSession[2] = DBGetVal.FirmNo.ToString();
                    ReportSession[3] = strLedgerNo;
                    
                    Form NewF = null;
                    
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.GetPartywiseGRNDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetPartywiseGRNDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                   
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                    OMMessageBox.Show("Select Atleast one Item ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
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
                BtnPartyShow.Focus();
                
            }
        }

        private void BtnPartyShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTToDate.Value < DTPFromDate.Value)
                {
                    OMMessageBox.Show("To Date cannot be less than From Date ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    DTPFromDate.Focus();                   
                    pnlPartyDetails.Visible = false;
                }
                else
                {
                    pnlPartyDetails.Visible = false;
                    PB = new DBProgressBar(this);
                    PB.TimerStart();
                    PB.Ctrl = pnlPartyDetails;
                    BindGridParty();
                    //pnlPartyDetails.Visible = true;
                    strLedgerNo = "";                    
                    chkPartySelectAll.Checked = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridParty()
        {
            dtParty = new DataTable();
            
            string str=" SELECT Distinct  MLedger.LedgerNo, MLedger.LedgerName,'false' as chk " +
                        " FROM TGRN INNER JOIN MLedger ON TGRN.LedgerNo = MLedger.LedgerNo "+
                        " where ( TGRN.GRNDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "')"+
                         " AND ( TGRN.GRNDate <='" + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "') order by LedgerName";

            dtParty = ObjFunction.GetDataView(str).Table;
            gvParty.Rows.Clear();
            for (int i = 0; i < dtParty.Rows.Count; i++)
            {
                gvParty.Rows.Add();
                for (int j = 0; j < gvParty.Columns.Count; j++)
                    gvParty.Rows[i].Cells[j].Value = dtParty.Rows[i].ItemArray[j];

            }
            gvParty.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (gvParty.Rows.Count > 0)
            {
                gvParty.Focus();
                gvParty.CurrentCell = gvParty[2, 0];
            }
        }

       
        
        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {                
                    chkPartySelectAll.Checked = !chkPartySelectAll.Checked;

                    for (int i = 0; i < gvParty.Rows.Count; i++)
                    {
                        gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
                    }
                    btnShow.Focus();                
            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else
                    KeyDownFormat(ctrl.Controls);
            }
        }
        #endregion
       

       

        private void chkPartySelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvParty.Rows.Count; i++)
            {
                gvParty.Rows[i].Cells[2].Value = chkPartySelectAll.Checked;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            pnlPartyDetails.Visible = false;
            BtnPartyShow.Focus();
            chkPartySelectAll.Checked = false;
            strLedgerNo = "";
        }      

       

        public static class ColIndex
        {
            public static int Date = 3;
            public static int Time = 4;
            public static int ItemName = 2;
            public static int BillNo = 5;
            public static int Qty = 6;
            //public static int Rate = 7;
            //public static int Amt = 8;
            public static int GrpName = 1;
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
