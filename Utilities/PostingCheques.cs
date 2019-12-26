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
    public partial class PostingCheques : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherChqCreditDetails tVchChqCreditDtls = new TVoucherChqCreditDetails();
        int rowindex; long PayType;

        public PostingCheques()
        {
            InitializeComponent();
        }

        public PostingCheques(long PayTypeCode)
        {
            PayType = PayTypeCode;            
            InitializeComponent();
            if (PayType == 1)
            {
                this.Text = "Posting Cheques";
            }
            else if (PayType == 2)
            {
                this.Text = "Posting Credits";
            }
        }

        private void PostingCheques_Load(object sender, EventArgs e)
        {
            InitGrid();
            rdPending.Checked = true;
            ObjFunction.FillList(lstBank, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.BankAccounts + "");
            ObjFunction.FillList(lstCompany, "Select CompanyNo,CompanyName from MCompany");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            btnPrint.Visible = false;
            BindGrid();
            if (gvChqDtls.Rows.Count == 0)
                OMMessageBox.Show("No Record Found", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            else
            {
                if (rdAll.Checked == true || rdPosted.Checked == true)
                {
                    if (rdPosted.Checked == true)
                        btnPrint.Visible = true;
                    btnSave.Visible = false;
                }
                else if (rdPending.Checked == true)
                    btnSave.Visible = true;
            }
        }

        private void InitGrid()
        {
           DataTable dt = new DataTable();
           string str = " SELECT 0 as SrNo, MLedger.LedgerName, case when TVoucherChqCreditDetails.ChequeNo !=0 then TVoucherChqCreditDetails.ChequeNo else TVoucherChqCreditDetails.CreditCardNo end as ChequeNo , TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.Amount, " +
                            " MBank.BankName, MBranch.BranchName,'' as PostLedger,'' as PostCompany , TVoucherChqCreditDetails.PkSrNo,0 as BankCode,0 as CompanyCode ,TVoucherDetails.LedgerNo" +
                            " FROM TVoucherEntry INNER JOIN " +
                            " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                            " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN " +
                            " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN " +
                            " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                            " MBank ON TVoucherChqCreditDetails.BankNo = MBank.BankNo " +
                            " where VoucherTypeCode=" + VchType.Sales + " and  VoucherDate>='01-01-1900' and VoucherDate<='01-01-1900' and TVoucherChqCreditDetails.IsPost='false'";
           if (PayType == 1)
           {              
               gvChqDtls.Columns[3].Visible = true;
               gvChqDtls.Columns[2].HeaderText = "ChqNo";
           }
           else if (PayType == 2)
           {              
               gvChqDtls.Columns[3].Visible = false;
               gvChqDtls.Columns[2].HeaderText = "CrCardNo";
           }
           dt = ObjFunction.GetDataView(str).Table;
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               gvChqDtls.Rows.Add();
               for (int j = 0; j < gvChqDtls.Columns.Count; j++)
               {
                   if (j == 3)
                       gvChqDtls.Rows[i].Cells[j].Value = Convert.ToDateTime(dt.Rows[i].ItemArray[j]).ToString("dd-MMM-yy");
                   else
                       gvChqDtls.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
               }
           }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool flag=true;
            for (int i = 0; i < gvChqDtls.Rows.Count; i++)
            {
                if (Convert.ToInt64(gvChqDtls.Rows[i].Cells[10].Value) != 0 && Convert.ToInt64(gvChqDtls.Rows[i].Cells[11].Value) != 0)
                {                    
                    dbTVoucherEntry = new DBTVaucherEntry();
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = 0;
                    tVoucherEntry.VoucherTypeCode = VchType.BankReceipt;
                    tVoucherEntry.VoucherUserNo = 0;
                    tVoucherEntry.VoucherDate = Convert.ToDateTime(DBGetVal.ServerTime.Date);
                    tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
                    tVoucherEntry.Narration = "";
                    tVoucherEntry.Reference = "";//(Convert.IsDBNull(txtRefNo.Text) == true) ? "" : txtRefNo.Text;
                    tVoucherEntry.ChequeNo = 0;
                    tVoucherEntry.ClearingDate = DBGetVal.ServerTime; 
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = 2*Convert.ToDouble(gvChqDtls.Rows[i].Cells[4].Value);
                   
                    tVoucherEntry.ChallanNo = "";
                    tVoucherEntry.InwardLocationCode = 0;
                    tVoucherEntry.MacNo = 0;
                    tVoucherEntry.PayTypeNo = 0;
                    tVoucherEntry.RateTypeNo = 0;
                    tVoucherEntry.TaxTypeNo = 0;
                   
                    tVoucherEntry.Remark = "";


                    tVoucherEntry.TransporterCode = 0;
                    tVoucherEntry.TransPayType = 0;
                    tVoucherEntry.LRNo = "";
                    tVoucherEntry.TransportMode = 0;
                    tVoucherEntry.TransNoOfItems = 0;

                    tVoucherEntry.UserID = DBGetVal.UserID;
                    tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                    dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

                    //for Party Account
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = 0;
                    tVoucherDetails.VoucherSrNo = 1;
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(gvChqDtls.Rows[i].Cells[12].Value);
                    tVoucherDetails.Credit = Convert.ToDouble(gvChqDtls.Rows[i].Cells[4].Value);
                    tVoucherDetails.Debit = 0;                    
                    tVoucherDetails.SrNo = Others.Party;
                    tVoucherDetails.CompanyNo = Convert.ToInt64(gvChqDtls.Rows[i].Cells[11].Value);
                    tVoucherDetails.Narration = "";
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                    //for Bank Account
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = 0;
                    tVoucherDetails.VoucherSrNo = 2;
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(gvChqDtls.Rows[i].Cells[10].Value);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.Debit = Convert.ToDouble(gvChqDtls.Rows[i].Cells[4].Value);
                    tVoucherDetails.SrNo = Others.Party;
                    tVoucherDetails.CompanyNo = Convert.ToInt64(gvChqDtls.Rows[i].Cells[11].Value);
                    tVoucherDetails.Narration = "";
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                    dbTVoucherEntry.UpdateChequeCreditDetails(Convert.ToInt64(gvChqDtls.Rows[i].Cells[9].Value));
                    if (dbTVoucherEntry.ExecuteNonQueryStatements()!= 0)
                    {
                        flag = false;
                    }
                    else
                        flag = true;
                }
            }
            if (flag == false)
            {
                OMMessageBox.Show("Cheque Posted Successfully!!", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                BindGrid();
            }
            else
            {
                OMMessageBox.Show("Cheque Not Posted !!", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
        }

        private void BindGrid()
        {
            DataTable dt = new DataTable();
            string str = "",strMain="";
            
                if (rdPending.Checked == true)
                    str = " SELECT 0 as SrNo, MLedger.LedgerName, case when TVoucherChqCreditDetails.ChequeNo !=0 then TVoucherChqCreditDetails.ChequeNo else TVoucherChqCreditDetails.CreditCardNo end as ChequeNo , TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.Amount, " +
                                 " MBank.BankName, MBranch.BranchName,'' as PostLedger,'' as PostCompany , TVoucherChqCreditDetails.PkSrNo,0 as BankCode,0 as CompanyCode ,TVoucherDetails.LedgerNo" +
                                 " FROM TVoucherEntry INNER JOIN " +
                                 " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                                 " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN " +
                                 " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN " +
                                 " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                                 " MBank ON TVoucherChqCreditDetails.BankNo = MBank.BankNo " +
                                 " where VoucherTypeCode=" + VchType.Sales + " and  VoucherDate>='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherChqCreditDetails.IsPost='false'";
                else if (rdPosted.Checked == true)
                    str = " SELECT 0 AS SrNo, MLedger.LedgerName, case when TVoucherChqCreditDetails.ChequeNo !=0 then TVoucherChqCreditDetails.ChequeNo else TVoucherChqCreditDetails.CreditCardNo end as ChequeNo, TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.Amount, " +
                          " MBank.BankName, MBranch.BranchName, PostLedg.LedgerName AS PostLedger, MCompany.CompanyName AS PostCompany, " +
                          " TVoucherChqCreditDetails.PkSrNo, 0 AS BankCode, 0 AS CompanyCode, TVoucherDetails.LedgerNo " +
                          " FROM         TVoucherEntry INNER JOIN " +
                          " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                          " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN " +
                          " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                          " TVoucherDetails AS TVS ON TVoucherChqCreditDetails.PostFkVoucherTrnNo = TVS.PkVoucherTrnNo INNER JOIN " +
                          " MCompany ON TVS.CompanyNo = MCompany.CompanyNo INNER JOIN " +
                          " MLedger AS PostLedg ON TVS.LedgerNo = PostLedg.LedgerNo LEFT OUTER JOIN " +
                          " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                          " MBank ON TVoucherChqCreditDetails.BankNo = MBank.BankNo " +
                          " where VoucherTypeCode=" + VchType.Sales + " and  VoucherDate>='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherChqCreditDetails.IsPost='true'";
                else if (rdAll.Checked == true)
                    if (PayType == 1)
                    {
                        str = " SELECT 0 as SrNo, MLedger.LedgerName, case when TVoucherChqCreditDetails.ChequeNo !=0 then TVoucherChqCreditDetails.ChequeNo else TVoucherChqCreditDetails.CreditCardNo end as ChequeNo, TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.Amount, " +
                                     " MBank.BankName, MBranch.BranchName,'' as PostLedger,'' as PostCompany , TVoucherChqCreditDetails.PkSrNo,0 as BankCode,0 as CompanyCode ,TVoucherDetails.LedgerNo" +
                                     " FROM TVoucherEntry INNER JOIN " +
                                     " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                                     " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN " +
                                     " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN " +
                                     " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                                     " MBank ON TVoucherChqCreditDetails.BankNo = MBank.BankNo " +
                                     " where VoucherTypeCode=" + VchType.Sales + " and  VoucherDate>='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherChqCreditDetails.IsPost='false' And TVoucherChqCreditDetails.ChequeNo !=0" +
                                     " UNION " +
                                     " SELECT 0 AS SrNo, MLedger.LedgerName, case when TVoucherChqCreditDetails.ChequeNo !=0 then TVoucherChqCreditDetails.ChequeNo else TVoucherChqCreditDetails.CreditCardNo end as ChequeNo, TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.Amount, " +
                                     " MBank.BankName, MBranch.BranchName, PostLedg.LedgerName AS PostLedger, MCompany.CompanyName AS PostCompany, " +
                                     " TVoucherChqCreditDetails.PkSrNo, 0 AS BankCode, 0 AS CompanyCode, TVoucherDetails.LedgerNo " +
                                     " FROM         TVoucherEntry INNER JOIN " +
                                     " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                                     " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN " +
                                     " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                                     " TVoucherDetails AS TVS ON TVoucherChqCreditDetails.PostFkVoucherTrnNo = TVS.PkVoucherTrnNo INNER JOIN " +
                                     " MCompany ON TVS.CompanyNo = MCompany.CompanyNo INNER JOIN " +
                                     " MLedger AS PostLedg ON TVS.LedgerNo = PostLedg.LedgerNo LEFT OUTER JOIN " +
                                     " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                                     " MBank ON TVoucherChqCreditDetails.BankNo = MBank.BankNo " +
                                     " where VoucherTypeCode=" + VchType.Sales + " and  VoucherDate>='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherChqCreditDetails.IsPost='true'";

                    }
                    else if (PayType == 2)
                    {
                        str = " SELECT 0 as SrNo, MLedger.LedgerName, case when TVoucherChqCreditDetails.ChequeNo !=0 then TVoucherChqCreditDetails.ChequeNo else TVoucherChqCreditDetails.CreditCardNo end as ChequeNo, TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.Amount, " +
                                     " MBank.BankName, MBranch.BranchName,'' as PostLedger,'' as PostCompany , TVoucherChqCreditDetails.PkSrNo,0 as BankCode,0 as CompanyCode ,TVoucherDetails.LedgerNo" +
                                     " FROM TVoucherEntry INNER JOIN " +
                                     " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                                     " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN " +
                                     " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo LEFT OUTER JOIN " +
                                     " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                                     " MBank ON TVoucherChqCreditDetails.BankNo = MBank.BankNo " +
                                     " where VoucherTypeCode=" + VchType.Sales + " and  VoucherDate>='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherChqCreditDetails.IsPost='false' and TVoucherChqCreditDetails.CreditCardNo !=''" +
                                     " UNION " +
                                     " SELECT 0 AS SrNo, MLedger.LedgerName, case when TVoucherChqCreditDetails.ChequeNo !=0 then TVoucherChqCreditDetails.ChequeNo else TVoucherChqCreditDetails.CreditCardNo end as ChequeNo, TVoucherChqCreditDetails.ChequeDate, TVoucherChqCreditDetails.Amount, " +
                                     " MBank.BankName, MBranch.BranchName, PostLedg.LedgerName AS PostLedger, MCompany.CompanyName AS PostCompany, " +
                                     " TVoucherChqCreditDetails.PkSrNo, 0 AS BankCode, 0 AS CompanyCode, TVoucherDetails.LedgerNo " +
                                     " FROM         TVoucherEntry INNER JOIN " +
                                     " TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                                     " TVoucherChqCreditDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherChqCreditDetails.FkVoucherTrnNo INNER JOIN " +
                                     " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                                     " TVoucherDetails AS TVS ON TVoucherChqCreditDetails.PostFkVoucherTrnNo = TVS.PkVoucherTrnNo INNER JOIN " +
                                     " MCompany ON TVS.CompanyNo = MCompany.CompanyNo INNER JOIN " +
                                     " MLedger AS PostLedg ON TVS.LedgerNo = PostLedg.LedgerNo LEFT OUTER JOIN " +
                                     " MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo LEFT OUTER JOIN " +
                                     " MBank ON TVoucherChqCreditDetails.BankNo = MBank.BankNo " +
                                     " where VoucherTypeCode=" + VchType.Sales + " and  VoucherDate>='" + Convert.ToDateTime(dtpFromDate.Value).ToString("dd-MMM-yyyy") + "' and VoucherDate<='" + Convert.ToDateTime(dtpToDate.Value).ToString("dd-MMM-yyyy") + "' and TVoucherChqCreditDetails.IsPost='true'";
                    }

                if (PayType == 1)
                {
                    strMain = str + " And TVoucherChqCreditDetails.ChequeNo !=0";
                    
                }
                else if (PayType == 2)
                {
                    strMain = str + " and TVoucherChqCreditDetails.CreditCardNo !=''";
                    
                }
                dt = ObjFunction.GetDataView(strMain).Table;
            gvChqDtls.Rows.Clear();
            gvChqDtls.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    gvChqDtls.Rows.Add();
                    for (int j = 0; j < gvChqDtls.Columns.Count; j++)
                    {
                        if (j == 3)
                            gvChqDtls.Rows[i].Cells[j].Value = Convert.ToDateTime(dt.Rows[i].ItemArray[j]).ToString("dd-MMM-yy");
                        else
                            gvChqDtls.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                }
                gvChqDtls.Focus();
                gvChqDtls.CurrentCell = gvChqDtls[7, 0];
            }
            
        }

        private void gvChqDtls_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Value = (e.RowIndex + 1).ToString();
        }

        private void gvChqDtls_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rowindex = gvChqDtls.CurrentCell.RowIndex;
                if (gvChqDtls.CurrentCell.ColumnIndex == 7)
                {
                    e.SuppressKeyPress = true;                    
                    pnlBank.Visible = true;
                    lstBank.Focus();
                }
                if (gvChqDtls.CurrentCell.ColumnIndex == 8)
                {
                    e.SuppressKeyPress = true;
                    pnlCompany.Visible = true;
                    lstCompany.Focus();
                }
            }
        }

        private void lstBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gvChqDtls.Rows[rowindex].Cells[7].Value = lstBank.Text;
                gvChqDtls.Rows[rowindex].Cells[10].Value = lstBank.SelectedValue;
                pnlBank.Visible = false;
                gvChqDtls.Focus();
                gvChqDtls.CurrentCell = gvChqDtls[8, rowindex];
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pnlBank.Visible = false;
            }
        }

        private void lstCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gvChqDtls.Rows[rowindex].Cells[8].Value = lstCompany.Text;
                gvChqDtls.Rows[rowindex].Cells[11].Value = lstCompany.SelectedValue;
                pnlCompany.Visible = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                pnlCompany.Visible = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string[] ReportSession;
            ReportSession = new string[3];
            ReportSession[0] = Convert.ToDateTime(dtpFromDate.Value).ToString("dd-MMM-yyyy");
            ReportSession[1] = Convert.ToDateTime(dtpToDate.Value).ToString("dd-MMM-yyyy");
            if (PayType == 1)           
                ReportSession[2] = "1";            
            else if (PayType == 2)
                ReportSession[2] = "2";
            Form NewF = new Display.ReportViewSource(new Reports.RPTPostingDetails(), ReportSession);
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
    }
}
