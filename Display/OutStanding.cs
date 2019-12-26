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
    public partial class OutStanding : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        
        public long CompNo, VoucherType;

        public OutStanding()
        {
            InitializeComponent();
        }

        public OutStanding(long _VchType)
        {
            InitializeComponent();
            if (DBGetVal.KachhaFirm == false)
            {
                if (_VchType == VchType.Sales)
                {
                    this.Name = "Sales Outstanding";
                    this.Text = "Sales Outstanding";
                    VoucherType = VchType.Sales;
                }
                else if (_VchType == VchType.Purchase)
                {
                    this.Name = "Purchase OutStanding";
                    this.Text = "Purchase OutStanding";
                    VoucherType = VchType.Purchase;
                }
            }else
                {
                if (_VchType == VchType.Sales)
                {
                    this.Name = "Sales Outstanding";
                    this.Text = "Sales Outstanding";
                    VoucherType = VchType.DSales;
                }
                else if (_VchType == VchType.Purchase)
                {
                    this.Name = "Purchase OutStanding";
                    this.Text = "Purchase OutStanding";
                    VoucherType = VchType.DPurchase;
                }
            }
            
        }

        private void OutStanding_Load(object sender, EventArgs e)
        {
            rbDetails.Visible = false;
            btnShow.Visible = false;
            CompNo = DBGetVal.FirmNo;
            DTPToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            KeyDownFormat(this.Controls);
            new GridSearch(gvParty, 1, 2);
        }

        public void BindGrid()
        {
            try
            {
                gvParty.Rows.Clear();
                string str = "";
                long Vch1 = 0, Vch2 = 0;
                if (VoucherType == 15)
                {
                    Vch1 = VchType.Sales; Vch2 = VchType.OpeningBalance;
                }
                else if (VoucherType == 115)
                {
                    Vch1 = VchType.DSales; Vch2 = VchType.DOpeningBalance;
                }
                else if (VoucherType == 9)
                {
                    Vch1 = VchType.Purchase; Vch2 = VchType.OpeningBalance;
                }
                else if (VoucherType == 109)
                {
                    Vch1 = VchType.DPurchase; Vch2 = VchType.DOpeningBalance;
                }
                if ((VoucherType == VchType.Sales)|| (VoucherType == VchType.DSales))

                    str = "SELECT MLedger.LedgerNo, MLedger.LedgerName,'false' as Chk " +
                            " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                            " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN " +
                            " MLedger ON TVoucherRefDetails.LedgerNo = MLedger.LedgerNo " + 
                            " WHERE " +
                            " (TVoucherRefDetails.TypeOfRef = 3) and TVoucherEntry.vouchertypecode=" + Vch1  + " AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") " +
                            " and (TVoucherEntry.VoucherDate<='" + DTPToDate.Value.Date.ToString(Format.DDMMMYYYY) + "') and " + 
                            " TVoucherEntry.IsCancel='false' and  MLedger.GroupNo in (" + GroupType.SundryDebtors + ") And " +
                            " (MLedger.IsActive = 'True') " +

                            " Union " +

                            " SELECT  MLedger.LedgerNo, MLedger.LedgerName ,'false' AS Chk " +
                            " FROM   TVoucherEntry INNER JOIN  " +
                            " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN  " +
                            " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  " +
                            
                            " WHERE     (TVoucherEntry.VoucherTypeCode = " + Vch2 + ") AND " + 
                            " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND " +
                            " (TVoucherDetails.Debit + TVoucherDetails.Credit <> 0) AND  " +
                            " (MLedger.GroupNo = " + GroupType.SundryDebtors + ") AND (MLedger.IsActive = 'True') " +

                            " ORDER BY LedgerName ";
                else

                    str = "SELECT MLedger.LedgerNo, MLedger.LedgerName ,'false' as Chk" +
                            " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                            " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo INNER JOIN " +
                            " MLedger ON TVoucherRefDetails.LedgerNo = MLedger.LedgerNo " +
                            " WHERE (TVoucherRefDetails.TypeOfRef = 3) and TVoucherEntry.vouchertypecode=" + Vch1 + " AND (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") " +
                            " and (TVoucherEntry.VoucherDate<='" + DTPToDate.Value.Date.ToString(Format.DDMMMYYYY) + "') and " + 
                            " TVoucherEntry.IsCancel='false' and MLedger.GroupNo in (" + GroupType.SundryCreditors + ") And " +
                            " (MLedger.IsActive = 'True') " +

                            " Union " +

                            " SELECT  MLedger.LedgerNo, MLedger.LedgerName ,'false' AS Chk " +
                            " FROM   TVoucherEntry INNER JOIN  " +
                            " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN  " +
                            " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  " +
                            " WHERE     (TVoucherEntry.VoucherTypeCode = " + Vch2 + ") AND " + 
                            " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND " +
                            " (TVoucherDetails.Debit + TVoucherDetails.Credit <> 0) AND  " +
                            " (MLedger.GroupNo = " + GroupType.SundryCreditors + ") AND (MLedger.IsActive = 'True') " +
                            
                            " ORDER BY LedgerName ";

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_IsPartyDisplayWithArea)) == true)
                {
                    str = str.Replace(", MLedger.LedgerName", ", MLedger.LedgerName +'-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') AS LedgerName");
                    str = str.Replace(".LedgerNo = MLedger.LedgerNo", ".LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN MLedgerDetails ON MLedger.LedgerNo=MLedgerDetails.LedgerNo LEFT OUTER JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo LEFT OUTER JOIN MArea ON MLedgerDetails.AreaNo = MArea.AreaNo  ");
                    str = str.Replace("ORDER BY MLedger.LedgerName", "ORDER BY MLedger.LedgerName +'-' + ISNULL(MArea.AreaName, '') + '-' + ISNULL(MCity.CityName, '') ");
                }
                       //" ORDER BY MLedger.LedgerName ";
                DataTable dt = ObjFunction.GetDataView(str).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvParty.Rows.Add();
                    for (int j = 0; j < gvParty.Columns.Count; j++)
                    {
                        gvParty.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string strParty = "";
                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvParty.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strParty == "")
                            strParty = gvParty.Rows[i].Cells[0].Value.ToString();
                        else
                            strParty = strParty + "," + gvParty.Rows[i].Cells[0].Value.ToString();
                    }
                }
                if (strParty != "")
                {
                    if (rbSummary.Checked == true)
                    {
                        string[] ReportSession = new string[4];
                        ReportSession[0] = VoucherType.ToString();
                        ReportSession[1] = DBGetVal.FirmNo.ToString();
                        ReportSession[2] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = strParty;

                        Form NewF = null;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.GetOutStanding(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutStanding.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else if (rbDetails.Checked == true)
                    {
                        string[] ReportSession = new string[4];
                        ReportSession[0] = VoucherType.ToString();
                        ReportSession[1] = DBGetVal.FirmNo.ToString();
                        ReportSession[2] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = strParty;

                        Form NewF = null;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.GetOutstandingDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutstandingDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                    OMMessageBox.Show("Select Atleast one Party ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnPartyShow_Click(object sender, EventArgs e)
        {
            BindGrid();
            pnlParty.Visible = true;
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;

                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    gvParty.Rows[i].Cells[2].Value = chkSelectAll.Checked;
                }
                btnShow.Focus();
            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is CheckBox)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvParty.Rows.Count; i++)
            {
                gvParty.Rows[i].Cells[2].Value = chkSelectAll.Checked;
            }
        }

        private void DTPToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnPartyShow.Focus();
            }
        }

        private void btnBillWiseDetail_Click(object sender, EventArgs e)
        {
            try
            {
                string strParty = "";
                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvParty.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strParty == "")
                            strParty = gvParty.Rows[i].Cells[0].Value.ToString();
                        else
                            strParty = strParty + "," + gvParty.Rows[i].Cells[0].Value.ToString();
                    }
                }
                if (strParty != "")
                {                  

                        string[] ReportSession;
                        Form NewF = null;
                        string str = "";
                       
                          ReportSession = new string[2];
                         // ReportSession[0] = Convert.ToDateTime("01-Apr-1900").ToString("dd-MMM-yyyy");
                         // ReportSession[1] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                         //ReportSession[2] = VoucherType.ToString();
                         //ReportSession[3] = DBGetVal.FirmNo.ToString();
                        ReportSession[0] = strParty;
                    ReportSession[1] = VoucherType.ToString();
                    //crystalReportViewer1.SelectionFormula = "";
                 // ReportViewSource.ReportName.Select.
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptLedgerBillWiseDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                                     
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                                                
                }
                else
                    OMMessageBox.Show("Select Atleast one Party ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        
    }

        private void btnBigPrint_Click(object sender, EventArgs e)
        {
             try
            {
                string strParty = "";
                for (int i = 0; i < gvParty.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvParty.Rows[i].Cells[2].FormattedValue) == true)
                    {
                        if (strParty == "")
                            strParty = gvParty.Rows[i].Cells[0].Value.ToString();
                        else
                            strParty = strParty + "," + gvParty.Rows[i].Cells[0].Value.ToString();
                    }
                }
                if (strParty != "")
                {
                    if (rbSummary.Checked == true)
                    {
                        //                   string[] ReportSession = new string[4];
                        //                   ReportSession[0] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy"); 
                        //                   ReportSession[1] = VoucherType.ToString();
                        //                   ReportSession[2] = DBGetVal.FirmNo.ToString();
                        //                   ReportSession[3] = strParty;

                        //                   Form NewF = null;
                        //                   if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        //                       NewF = new Display.ReportViewSource(new Reports.GetOutStanding(), ReportSession);
                        //                   else
                        ////                       NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutStandingBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        //                       NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptOutstanding.rpt", CommonFunctions.ReportPath), ReportSession);

                        //                   ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                        // new code starts 

                        string[] ReportSession;
                        Form NewF = null;
                        string str = "";
                       // str = GetStrLedger();
                        ReportSession = new string[5];
                        ReportSession[0] =  Convert.ToDateTime("01-Apr-1900").ToString("dd-MMM-yyyy");
                        ReportSession[1] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                      //  if (DBGetVal.KachhaFirm == false)//--umesh 16-11-2018
                       // {
                            ReportSession[2] = VoucherType.ToString(); //(rbCustomer.Checked == true) ? VchType.Sales.ToString() : VchType.Purchase.ToString();
                       // }
                       // else
                       // {
                       //     ReportSession[2] = (rbCustomer.Checked == true) ? VchType.DSales.ToString() : VchType.DPurchase.ToString();
                       // }
                        // ReportSession[2] = (rbCustomer.Checked == true) ? VchType.Sales.ToString() : VchType.Purchase.ToString();
                        ReportSession[3] = DBGetVal.FirmNo.ToString();
                        ReportSession[4] = strParty;
                       
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptLederBookSummary.rpt", CommonFunctions.ReportPath), ReportSession);

                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    //  new code ends 

                    }
                    else if (rbDetails.Checked == true)
                    {
                        string[] ReportSession = new string[4];
                        ReportSession[0] = VoucherType.ToString();
                        ReportSession[1] = DBGetVal.FirmNo.ToString();
                        ReportSession[2] = Convert.ToDateTime(DTPToDate.Text).ToString("dd-MMM-yyyy");
                        ReportSession[3] = strParty;

                        Form NewF = null;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                            NewF = new Display.ReportViewSource(new Reports.GetOutstandingDetails(), ReportSession);
                        else
                            NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetOutstandingDetailsBig.rpt", CommonFunctions.ReportPath), ReportSession);
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                }
                else
                    OMMessageBox.Show("Select Atleast one Party ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        
        }
    }
}
