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

namespace Yadi.Utilities
{
    public partial class DetailsTranspoterList : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();

        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        TVoucherEntry tvoucherentry = new TVoucherEntry();
        //TVoucherDetails tvoucherdetails = new TVoucherDetails();
        DBTVaucherEntry dbtVoucherEntry = new DBTVaucherEntry();
        DBTEWayDetails dbtEWayDetail = new DBTEWayDetails();
        TEWayDetails tewaydetails = new TEWayDetails();
        DBMFirm dbmFirm = new DBMFirm();
        DBMLedger dbmLedger = new DBMLedger();
        DBTranspotorDetail dbTranspotorDetail = new DBTranspotorDetail();
        TranspotorDetail mTranspotorDetail = new TranspotorDetail();
        DataTable dt = new DataTable();
        string strcondtion = "";
        String Expensetype="";
        public DetailsTranspoterList()
        {
            InitializeComponent();
        }

        private void DetailsExpenseList_Load(object sender, EventArgs e)
        {
            pnlDate.Visible = false;
            formatpicture();
           // rdPending.Checked = true;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void BindGrid()
        {
            try
            {
                string sqlQuery = "";
             
                sqlQuery = "SELECT   0 as Srno,TVoucherEntry.VoucherUserNo AS 'DocNo', TEWayDetails.TranspotorNm, TEWayDetails.LRNo, CONVERT(varchar, TEWayDetails.LRDate, 3) AS 'Date', TEWayDetails.LedgerName AS 'supplierNm', " +
                           " TVoucherEntry.ChallanNo AS 'Bill_No', TranspotorDetail.NoOfQty, TranspotorDetail.ReceivedQty, TranspotorDetail.BalancedQty, TVoucherEntry.ChrgesTaxPerce AS 'tax%', TVoucherEntry.BilledAmount AS 'Total_amt', TVoucherEntry.Remark "+
                           "  FROM TVoucherEntry INNER JOIN TEWayDetails ON TVoucherEntry.PkVoucherNo = TEWayDetails.FkVoucherNo INNER JOIN TranspotorDetail ON TVoucherEntry.PkVoucherNo = TranspotorDetail.FkVoucherNo "+
                              " Where " + strcondtion;
                
                //sqlQuery = "SELECT  TVoucherEntry.PkVoucherNo AS 'DocNo',TEWayDetails.TranspotorNm, TEWayDetails.LRNo ,convert(varchar, TEWayDetails.LRDate, 3) as 'Date',TEWayDetails.LedgerName AS 'supplierNm',  TVoucherEntry.ChallanNo AS 'Bill_No', TVoucherEntry.TransNoOfItems, TEWayDetails.ReceivedQty, TEWayDetails.BalancedQty, " +
                //         " TVoucherEntry.ChrgesTaxPerce AS 'tax%', TVoucherEntry.BilledAmount AS 'Total_amt', TVoucherEntry.Remark as 'Remark'  FROM TVoucherEntry INNER JOIN TEWayDetails ON TVoucherEntry.PkVoucherNo = TEWayDetails.FkVoucherNo  " +
                //" Where " + strcondtion ;

                GvTran.Rows.Clear();
                
                dt = ObjFunction.GetDataView(sqlQuery).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GvTran.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                       GvTran.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                       GvTran.Rows[i].Cells[0].Value = (i + 1).ToString();
                    }
                   // GvTran.Columns[3].DefaultCellStyle.Format = Format.DDMMMYYYY;// "dd/MM/yyyy"; 
            
                    GvTran.Rows[i].ReadOnly = true;
                }
            }
            catch (Exception e1)
            {
                ObjFunction.ExceptionDisplay(e1.Message);

            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpFromDate.Text = null;
            dtpToDate.Text = null;
            pnlDate.Visible = false;
            rdPending.Checked = false;
            rdAll.Checked = false;
            rdCleared.Checked = false;
            GvTran.Rows.Clear();
        }


        public void CheckRadio()
        {
            if (rdPending.Checked == true)
            {
                pnlDate.Visible = false;
            }
            else if (rdCleared.Checked == true || rdAll.Checked == true)
            {
                pnlDate.Visible = true;
            }
            else
            {
                pnlDate.Visible = false;
            }
        }

        private void rdPending_CheckedChanged(object sender, EventArgs e)
        {
            pnlDate.Visible = false;
            btnOk.Visible = false;
            strcondtion = " TranspotorDetail.BalancedQty in (SELECT min(BalancedQty) FROM TranspotorDetail group by FkVoucherNo) AND TranspotorDetail.BalancedQty > 0";
            //strcondtion = " TranspotorDetail.BalancedQty >0 ";
            BindGrid();
        }

        private void rdCleared_CheckedChanged(object sender, EventArgs e)
        {
             pnlDate.Visible = true;
            btnOk.Visible = true;
        }
        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            pnlDate.Visible = true;
            btnOk.Visible = true;
        }
        public void Checkfun1()
        {
            tvoucherentry = new TVoucherEntry();
            tewaydetails = new TEWayDetails();
            mTranspotorDetail = new TranspotorDetail();
            try
            {

                if ((dtpFromDate.Text == tewaydetails.LRDate.ToString()) && (dtpToDate.Text == tewaydetails.LRDate.ToString()))
                {
                    BindGrid();
                }
                else if ((dtpFromDate.Text == tewaydetails.LRDate.ToString()) && (dtpToDate.Text == tewaydetails.LRDate.ToString()))
                {
                    BindGrid();
                }
                else if (mTranspotorDetail.BalancedQty == 0)
                {
                    BindGrid();
                }
                else
                {
                    OMMessageBox.Show("No Radio button selected", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                }
            }
            catch(Exception exech)
            {
                ObjFunction.ExceptionDisplay(exech.Message);
            }

        }
      

        private void dtpFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (dtpFromDate.Text == "")
                    {
                        dtpFromDate.Focus();
                    }
                    else
                    {
                        dtpToDate.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (dtpToDate.Text == "")
                    {
                        dtpToDate.Focus();
                    }
                    else
                    {
                        btnOk.Focus();
                       // Checkfun1();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void formatpicture()
        {
            GvTran.Top = 80;
            GvTran.Width = 1235;
            GvTran.Height = 400;
        }
        string s1 = "";
        string s2 = "";

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rdCleared.Checked== true)
            {
                strcondtion = " TranspotorDetail.BalancedQty=0  and TVoucherEntry.VoucherDate >= '" + dtpFromDate.Value + "' and TVoucherEntry.voucherdate <= '" + dtpToDate.Value + "'"; 
                BindGrid();
            }
            else if (rdAll.Checked == true)
            {
                strcondtion = " TVoucherEntry.VoucherDate >= '" + dtpFromDate.Value + "' and TVoucherEntry.voucherdate <= '" + dtpToDate.Value + "' order by TVoucherEntry.VoucherUserNo ";
                BindGrid();

                //string s= " TVoucherEntry.VoucherDate >= '" + dtpFromDate.Value + "'";
                //string s1 = " TVoucherEntry.voucherdate <= '" + dtpToDate.Value + "'";
                //if (s == s1)
                //{
                //    strcondtion = " TVoucherEntry.VoucherDate >= '" + dtpFromDate.Value + "' and TVoucherEntry.voucherdate <= '" + dtpToDate.Value + "' order by TVoucherEntry.VoucherUserNo ";
                //    BindGrid();
                //} 
                //else
                //{

                //}
            }
            else
            {
                //order by TVoucherEntry.VoucherUserNo
                strcondtion = " TVoucherEntry.VoucherDate >= '" + dtpFromDate.Value + "' and TVoucherEntry.voucherdate <= '" + dtpToDate.Value + "' order by TVoucherEntry.VoucherUserNo ";
                BindGrid();

            }
        }
   
        //private void btnReport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string[] ReportSession;
        //        Form NewF = null;
        //        ReportSession = new string[3];
        //        ReportSession[0] = Convert.ToDateTime(dtpFromDate.Text).ToString("dd-MMM-yyyy");
        //        ReportSession[1] = Convert.ToDateTime(dtpToDate.Text).ToString("dd-MMM-yyyy");

        //        if (rdPending.Checked == true)
        //        {
        //            Expensetype = "1";
        //            ReportSession[2] = Expensetype;
        //            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
        //                NewF = new Display.ReportViewSource(new Reports.DetailExpenseList(), ReportSession);
        //            else
        //                try
        //                {
        //                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DetailExpenseList.rpt", CommonFunctions.ReportPath), ReportSession);
        //                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        //                }
        //                catch (Exception exc)
        //                {
        //                    ObjFunction.ExceptionDisplay(exc.Message);
        //                }
        //        }
        //        else if (rdCleared.Checked == true)
        //        {
        //            Expensetype = "2";
        //            ReportSession[2] = Expensetype;
        //            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
        //                NewF = new Display.ReportViewSource(new Reports.DetailExpenseList(), ReportSession);
        //            else
        //                try
        //                {
        //                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DetailExpenseList.rpt", CommonFunctions.ReportPath), ReportSession);
        //                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        //                }
        //                catch (Exception exc)
        //                {
        //                    ObjFunction.ExceptionDisplay(exc.Message);
        //                }

        //        }
        //        else if (rdAll.Checked == true)
        //        {
        //            Expensetype = "3";
        //            ReportSession[2] = Expensetype;
        //            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
        //                NewF = new Display.ReportViewSource(new Reports.DetailExpenseList(), ReportSession);
        //            else
        //                try
        //                {
        //                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DetailExpenseList.rpt", CommonFunctions.ReportPath), ReportSession);
        //                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        //                }
        //                catch (Exception exc)
        //                {
        //                    ObjFunction.ExceptionDisplay(exc.Message);
        //                }
        //        }
        //        else
        //        {
        //            OMMessageBox.Show("Select One Option", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        private void GvTran_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

    }
}
