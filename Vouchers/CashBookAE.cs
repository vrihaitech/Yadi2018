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


namespace Yadi.Vouchers
{
    public partial class CashBookAE : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dtSearch = new DataTable();
        public long ID, VoucherType; public DateTime FillDate;
        long LedgNo = 0;
        string strvouchertype = "";
        int cntRow;
        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        TVoucherChqCreditDetails tVchChqCredit = new TVoucherChqCreditDetails();
        public bool RewardDeleteFlag = false;
        DataTable dt = new DataTable();
        DataTable dtDelete = new DataTable();

        public CashBookAE()
        {
            InitializeComponent();
        }


        private void CashBookAE_Load(object sender, EventArgs e)
        {
            try
            {

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                ObjFunction.FillList(lstEntryType, "Select VoucherTypeCode,VoucherTypeName From MVoucherType Where  VoucherTypeCode in (1,2,3,5,27,28,29,33,34) ORDER BY VoucherTypeName");
                ObjFunction.FillCombo(cmbBank, "Select BankNo,BankName From MBank where IsActive='true' order by BankName");
                ObjFunction.FillCombo(cmbBranch, "Select BranchNo,BranchName From MBranch  Where isActive='true' order by BranchName");
                ObjFunction.FillCombo(cmbCompanyBank, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.BankAccounts + " And IsActive='true' order by LedgerName");
                FillList();
                FormatPics();
                chkSelectAll.Checked = true;
                chkContra.Enabled = true;
                chkCashReceipt.Enabled = true;
                chkCashPayment.Enabled = true;
                chkExpenseEntry.Enabled = true;
                chkJV.Enabled = true;
                chkCreditNote.Enabled = true;
                chkDebitNote.Enabled = true;
                chkBankReceipt.Enabled = true;
                chkBankPayment.Enabled = true;
                chkSelectAll.Enabled = true;
                dtpBillDate.Enabled = true;
                pnlVouchertypename.Enabled = true;
                // GVCashBook.Enabled = false;
                BtnOK.Enabled = false;
                BtnX.Enabled = false;
                dtDate.CustomFormat = "dd-MMM-yy"; dtDate.Width = 90;
                GVCashBook.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                GVCashBook.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                GVCashBook.RowTemplate.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
                GVCashBook.RowTemplate.Height = 30;
                GVCashBook.ColumnHeadersHeight = 30;


                label8.Location = new Point(222, 74);
                txtRemark.Location = new Point(300, 67);

                label9.Location = new Point(520, 74);
                txtReference.Location = new Point(610, 67);

                panel3.Visible = false;
                GVCashBook.Top = 9;
                GVCashBook.Height = 470;
                GVCashBook.Enabled = true;

                ID = ObjQry.ReturnLong("Select max(PkVoucherNo) from TVoucherEntry Where VoucherTypeCode in(1,5,27,33,34) ", CommonFunctions.ConStr);
                if (ID != 0)
                {
                    NavigationDisplay(2);

                }
                if (GVCashBook.Rows.Count > 0)
                {
                    for (int i = 0; i < GVCashBook.Rows.Count; i++)
                    {
                        GVCashBook.Rows[i].DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Regular);
                        GVCashBook.Rows[i].Height = 25;
                    }
                }
                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillList()
        {
            if (ObjFunction.GetListValue(lstEntryType) == 1)////Contra
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (27,28)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo   IN (27,28)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=1 ", CommonFunctions.ConStr) + 1).ToString();

            }
            else if (ObjFunction.GetListValue(lstEntryType) == 2)//// Credit Note 
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (22,26)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo  IN (27,28,13,15)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=2 ", CommonFunctions.ConStr) + 1).ToString();

            }
            else if (ObjFunction.GetListValue(lstEntryType) == 3)//// Debit Note  
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (27,28,13,15)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo  IN (22,26)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=3 ", CommonFunctions.ConStr) + 1).ToString();

            }
            else if (ObjFunction.GetListValue(lstEntryType) == 5)////JV
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (22,26)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo  IN (27,28)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=5 ", CommonFunctions.ConStr) + 1).ToString();

            }
            else if (ObjFunction.GetListValue(lstEntryType) == 27) ////cash receipt
            {
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (27)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo not IN (22,26,51, 52, 53, 54, 10, 11)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=27", CommonFunctions.ConStr) + 1).ToString();

            }

            else if (ObjFunction.GetListValue(lstEntryType) == 34)////cash Payment
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (27)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo not IN (22,26,51, 52, 53, 54, 10, 11)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo)+1 from TVoucherEntry Where VoucherTypeCode=34 ", CommonFunctions.ConStr) + 1).ToString();

            }
            else if (ObjFunction.GetListValue(lstEntryType) == 33)////Expense Entry
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (27,28)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo  IN (13,15)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo)+1 from TVoucherEntry Where VoucherTypeCode=33 ", CommonFunctions.ConStr) + 1).ToString();

            }


            else if (ObjFunction.GetListValue(lstEntryType) == 28)////Bank Receipt
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo Not IN (22,26,51, 52, 53, 54, 10, 11)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo  IN (28)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo)+1 from TVoucherEntry Where VoucherTypeCode=28 ", CommonFunctions.ConStr) + 1).ToString();

            }
            else if (ObjFunction.GetListValue(lstEntryType) == 29)////Bank Payment
            {
                ObjFunction.FillList(lstFromAccount, "SELECT  MLedger.LedgerNo, MLedger.LedgerName  FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo IN (28)) AND (MLedger.IsActive = 'true') order by MLedger.LedgerName");
                ObjFunction.FillList(lstToAccount, "SELECT  MLedger.LedgerNo,case when mledger.groupno=22 then MLedger.LedgerName  +'  (P)' else  MLedger.LedgerName end as LedgerName FROM  MGroup INNER JOIN  MLedger ON MGroup.GroupNo = MLedger.GroupNo  WHERE  (MGroup.GroupNo  Not IN (22,26,51, 52, 53, 54, 10, 11)) AND (MLedger.IsActive = 'true')  order by MLedger.LedgerName");
                txtDocNo.Text = (ObjQry.ReturnLong("Select max(VoucherUserNo)+1 from TVoucherEntry Where VoucherTypeCode=29 ", CommonFunctions.ConStr) + 1).ToString();

            }
        }

        private void FillControls()
        {

            try
            {
                bindGrid();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void bindGrid()
        {

            string sqlQuery = "";
            string sqlQuery2 = "";
            //=======For Cheque Details and credit card details RTGS details ===/// link table extra TVoucherChqCreditDetails
            sqlQuery2 = "SELECT     0 AS SRNO, TVoucherEntry.VoucherDate AS Date,TVoucherEntry.VoucherUserNo AS VoucherNo, MLedger.LedgerName AS FromAccount, MLedger_1.LedgerName AS ToAccount, TVoucherEntry.BilledAmount AS Amount, " +
                        " TVoucherEntry.Remark, TVoucherEntry.Narration, TVoucherEntry.ChequeNo, TVoucherDetails.LedgerNo AS FromLedgerNo, TVoucherEntry.VoucherTypeCode,''   AS BankName,  MOtherBank.BankName AS PartyBank, MBranch.BranchName AS PartyBranch, " +
                        " TVoucherChqCreditDetails.ChequeDate, TVoucherEntry.PkVoucherNo, MLedger_1.LedgerNo AS ToLedgerno, TVoucherChqCreditDetails.BankNo AS PBankNo, TVoucherChqCreditDetails.BranchNo AS PBranchNo, " +
                        " TVoucherChqCreditDetails.ChequeNo AS ToChequeNo, TVoucherEntry.Reference, TVoucherEntry.VoucherDate AS Date1 ,TVoucherEntry.IsCancel,'False' AS SelectPrint, 'Print' as btnPrint " +
                        " FROM         TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo AND TVoucherDetails.SrNo = 501 INNER JOIN " +
                        " TVoucherDetails AS TVoucherDetails_2 ON TVoucherEntry.PkVoucherNo = TVoucherDetails_2.FkVoucherNo AND TVoucherDetails_2.VoucherSrNo = 2 INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo INNER JOIN " +
                        " MLedger AS MLedger_1 ON TVoucherDetails_2.LedgerNo = MLedger_1.LedgerNo INNER JOIN TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo INNER JOIN " +
                        " MOtherBank ON TVoucherChqCreditDetails.BankNo = MOtherBank.BankNo INNER JOIN MBranch ON TVoucherChqCreditDetails.BranchNo = MBranch.BranchNo " +
                        " WHERE  TVoucherEntry.VoucherDate='" + dtpBillDate.Value.Date + "'  and (TVoucherEntry.VoucherTypeCode IN (1,2,3,33,34,27,28,29)) union all ";  //--ORDER BY Date1 DESC
            //=====================Only For Cash Recived and  cash Pay 
            sqlQuery = "SELECT 0 AS SRNO, TVoucherEntry.VoucherDate as Date, TVoucherEntry.VoucherUserNo AS VoucherNo, MLedger.LedgerName AS FromAccount,MLedger_1.LedgerName AS ToAccount, TVoucherEntry.BilledAmount AS Amount, " +
                       " TVoucherEntry.Remark, TVoucherEntry.Narration, TVoucherEntry.ChequeNo AS ChequeNo, TVoucherDetails.LedgerNo AS FromLedgerNo, TVoucherEntry.VoucherTypeCode,  case when ((select groupno from MLedger where ledgerno=TVoucherDetails.ledgerno ) =28 )then  MLedger.LedgerName else '' end  AS BankName,  " +
                       " '' AS PartyBank, '' AS PartyBranch, '' AS ChequeDate, TVoucherEntry.PkVoucherNo, MLedger_1.ledgerno AS ToLedgerno, 0 AS PBankNo, 0 AS PBranchNo,0 as ToChequeNo,TVoucherEntry.Reference,TVoucherEntry.VoucherDate  as Date1 ,TVoucherEntry.IsCancel,'False' AS SelectPrint,'Print' as btnPrint " +
                       " FROM   TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo AND TVoucherDetails.SrNo = 501 " +
                       " INNER JOIN  TVoucherDetails as TVoucherDetails_2  ON TVoucherEntry.PkVoucherNo = TVoucherDetails_2.FkVoucherNo AND TVoucherDetails_2.vouchersrno =2 " +
                       " INNER JOIN   MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo INNER JOIN  MLedger AS MLedger_1 ON TVoucherDetails_2.LedgerNo = MLedger_1.LedgerNo WHERE   TVoucherEntry.VoucherDate='" + dtpBillDate.Value + "'  and " +
                       " (TVoucherEntry.VoucherTypeCode in (0))  order by TVoucherEntry.VoucherDate desc ,TVoucherEntry.Narration";

            if (chkCashPayment.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (34))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = false;
                GVCashBook.Columns[ColIndex.Bank].Visible = false;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = false;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = false;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = false;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = false;
            }
            else if (chkCashReceipt.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (27))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = false;
                GVCashBook.Columns[ColIndex.Bank].Visible = false;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = false;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = false;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = false;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = false;
            }
            else if (chkContra.Checked == true)
            {
                sqlQuery2 = sqlQuery2 + sqlQuery;
                sqlQuery = sqlQuery2;
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (1))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = true;
                GVCashBook.Columns[ColIndex.Bank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = true;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = true;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = true;
            }
            else if (chkJV.Checked == true)
            {
                sqlQuery2 = sqlQuery2.Replace("IN (1,2,3,33,34,27,28,29)) union all ", "IN (5))");
                sqlQuery = sqlQuery2;
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (5))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = true;
                GVCashBook.Columns[ColIndex.Bank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = true;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = true;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = true;
            }
            else if (chkExpenseEntry.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (33))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = true;
                GVCashBook.Columns[ColIndex.Bank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = true;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = true;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = true;
            }
            else if (chkCreditNote.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (2))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = true;
                GVCashBook.Columns[ColIndex.Bank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = true;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = true;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = true;
            }
            else if (chkDebitNote.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (3))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = true;
                GVCashBook.Columns[ColIndex.Bank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = true;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = true;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = true;
            }
            else if (chkBankReceipt.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (28))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = true;
                GVCashBook.Columns[ColIndex.Bank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = true;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = true;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = true;
            }
            else if (chkBankPayment.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (29))");
                GVCashBook.Columns[ColIndex.ChequeNo].Visible = true;
                GVCashBook.Columns[ColIndex.Bank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBank].Visible = true;
                GVCashBook.Columns[ColIndex.PartyBranch].Visible = true;
                GVCashBook.Columns[ColIndex.ChequeDate].Visible = true;
                GVCashBook.Columns[ColIndex.ToChequeNo].Visible = true;
            }
            else if (chkSelectAll.Checked == true)
            {
                sqlQuery = sqlQuery.Replace(" (TVoucherEntry.VoucherTypeCode in (0))", " (TVoucherEntry.VoucherTypeCode in (1,2,3,5,27,33,34,28,29))");
            }
            GVCashBook.Rows.Clear();
            DateTime dt1;
            string date = "";
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GVCashBook.Columns[ColIndex.Date1].DefaultCellStyle.Format = "dd/MM/yyyy";
                GVCashBook.Rows.Add();
                for (int j = 0; j < GVCashBook.Columns.Count; j++)
                {
                    GVCashBook.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
                dt1 = Convert.ToDateTime(dt.Rows[i].ItemArray[1].ToString());
                date = dt1.ToString("dd/MM/yyyy");
                GVCashBook.Rows[i].Cells[1].Value = date;

                if (Convert.ToBoolean((GVCashBook.Rows[i].Cells[ColIndex.IsCancel]).Value) == true)
                {
                    GVCashBook.Rows[i].DefaultCellStyle.BackColor = Color.LightPink;

                }
                //  string date=Convert.ToDateTime(GVCashBook.Rows[i].Cells[ColIndex.Date].Value).ToString();

            }

            GVCashBook.Rows.Add();
        }

        private void hidePics()
        {
            pnlEntryType.Visible = false;
            pnlFromAccount.Visible = false;
            pnlToAccount.Visible = false;
            pnlchq.Visible = false;

        }

        public bool Validations()
        {
            bool flag = false;

            if (txtEntryType.Text.Trim() == "")
            {
                txtEntryType.Focus();
            }
            else if (txtFromAccount.Text.Trim() == "")
            {
                txtFromAccount.Focus();
            }
            else if (txtToAccount.Text.Trim() == "")
            {
                txtToAccount.Focus();
            }
            else if (txtAmount.Text == "0.00")
            {
                txtAmount.Focus();
            }
            else if (txtAmount.Text == "")
            {
                txtAmount.Focus();
            }

            else
                flag = true;
            return flag;
        }

        private void FormatPics()
        {
            pnlEntryType.Top = txtEntryType.Bottom + 10;
            pnlEntryType.Width = txtEntryType.Width;
            pnlEntryType.Height = 150;
            lstEntryType.Top = pnlEntryType.Top - 56;
            lstEntryType.Height = pnlEntryType.Height - 5;

            pnlFromAccount.Top = txtFromAccount.Bottom + 10;
            pnlFromAccount.Width = txtFromAccount.Width;
            pnlFromAccount.Height = 200;
            lstFromAccount.Top = pnlFromAccount.Top - 56;
            lstFromAccount.Height = pnlFromAccount.Height - 5;


            pnlToAccount.Top = txtToAccount.Bottom + 10;
            pnlToAccount.Width = txtToAccount.Width;
            pnlToAccount.Height = 200;
            lstToAccount.Top = pnlToAccount.Top - 56;
            lstToAccount.Height = pnlToAccount.Height - 5;

            pnlSearchEntryType.Top = txtSearchEntryType.Bottom + 100;
            pnlSearchEntryType.Width = txtSearchEntryType.Width;
            pnlSearchEntryType.Height = 200;
            lstSearchEntryType.Top = pnlSearchEntryType.Top - 152;
            lstSearchEntryType.Height = pnlSearchEntryType.Height - 5;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            lblchequeno.Visible = false;
            txtChequeNo.Visible = false;
            pnlchq.Visible = false;
            label8.Location = new Point(220, 74);
            txtRemark.Location = new Point(290, 67);

            label9.Location = new Point(470, 74);
            txtReference.Location = new Point(560, 67);

            //NavigationDisplay(2);
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            chkContra.Enabled = true;
            chkCashReceipt.Enabled = true;
            chkCashPayment.Enabled = true;
            chkExpenseEntry.Enabled = true;
            chkCreditNote.Enabled = true;
            chkDebitNote.Enabled = true;
            chkJV.Enabled = true;
            chkSelectAll.Enabled = true;
            dtpBillDate.Enabled = true;
            pnlVouchertypename.Enabled = true;
            GVCashBook.Enabled = false;
            BtnOK.Enabled = false;
            BtnX.Enabled = false;
            GVCashBook.Rows.Clear();
            panel3.Visible = false;
            GVCashBook.Top = 9;
            GVCashBook.Height = 470;
            bindGrid();
            pnlEntryType.Visible = false;
            btnNew.Focus();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                hidePics();
                long tempID = 0;
                for (int i = 0; i < GVCashBook.Rows.Count - 1; i++)
                {

                    dbTVoucherEntry = new DBTVaucherEntry();

                    int VoucherSrNo = 1;
                    //Voucher Header Entry 
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.PkVocherNo].Value));

                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 1)
                    {
                        tVoucherEntry.VoucherTypeCode = 1;
                        tVoucherEntry.Narration = "Contra Entry";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 2)
                    {
                        tVoucherEntry.VoucherTypeCode = 2;
                        tVoucherEntry.Narration = "Credit Note";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 3)
                    {
                        tVoucherEntry.VoucherTypeCode = 3;
                        tVoucherEntry.Narration = "Debit Note";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 5)
                    {
                        tVoucherEntry.VoucherTypeCode = 5;
                        tVoucherEntry.Narration = "JV Entry";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 27)
                    {
                        tVoucherEntry.VoucherTypeCode = 27;
                        tVoucherEntry.Narration = "Cash Receive";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 33)
                    {
                        tVoucherEntry.VoucherTypeCode = 33;
                        tVoucherEntry.Narration = "Expense Entry";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 34)
                    {
                        tVoucherEntry.VoucherTypeCode = 34;
                        tVoucherEntry.Narration = "Cash Payment";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 28)
                    {
                        tVoucherEntry.VoucherTypeCode = 28;
                        tVoucherEntry.Narration = "Bank Receipt";
                    }
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.VoucherType].Value)) == 29)
                    {
                        tVoucherEntry.VoucherTypeCode = 29;
                        tVoucherEntry.Narration = "Bank Payment";
                    }
                    tVoucherEntry.VoucherUserNo = Convert.ToInt64(GVCashBook[ColIndex.VoucherNo, i].Value.ToString());
                    tVoucherEntry.VoucherDate = Convert.ToDateTime(GVCashBook.Rows[i].Cells[ColIndex.Date1].Value);
                    tVoucherEntry.VoucherTime = Convert.ToDateTime(GVCashBook.Rows[i].Cells[ColIndex.Date1].Value);

                    tVoucherEntry.Reference = (GVCashBook[ColIndex.Reference, i].Value.ToString());
                    //tVoucherEntry.ChequeNo = (Convert.ToInt64(GVCashBook[ColIndex.ChequeNo, i].Value) == 0) ? 0 : Convert.ToInt64(GVCashBook[ColIndex.ChequeNo, i].Value);
                    tVoucherEntry.ClearingDate = Convert.ToDateTime(GVCashBook.Rows[i].Cells[ColIndex.Date1].Value);
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = Convert.ToDouble(GVCashBook[ColIndex.Amount, i].Value.ToString());
                    tVoucherEntry.ChallanNo = "";
                    tVoucherEntry.Remark = (GVCashBook[ColIndex.Remark, i].Value.ToString());
                    tVoucherEntry.MacNo = DBGetVal.MacNo;
                    tVoucherEntry.LedgerNo = Convert.ToInt64(GVCashBook[ColIndex.FromLedgerNo, i].Value.ToString());
                    tVoucherEntry.PayTypeNo = 0;

                    tVoucherEntry.RateTypeNo = 0;
                    tVoucherEntry.OrderType = 0;
                    tVoucherEntry.DiscPercent = 0.00;
                    tVoucherEntry.DiscAmt = 0.00;
                    tVoucherEntry.MixMode = 0;
                    tVoucherEntry.PkRefNo = 0;

                    tVoucherEntry.UserID = DBGetVal.UserID;
                    tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                    tVoucherEntry.LRNo = " ";
                    dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);


                    for (int k = 0; k < GVCashBook.Rows.Count - 1; k++)
                    {
                        if (Convert.ToBoolean((GVCashBook[ColIndex.IsCancel, i].Value)) == true)
                        {
                            tVoucherEntry = new TVoucherEntry();
                            //  tVoucherEntry.IsCancel = true;
                            tVoucherEntry.PkVoucherNo = Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.PkVocherNo].Value));
                            dbTVoucherEntry.CancelTVoucherEntrynew(tVoucherEntry);
                        }
                    }
                    //================================================from account ledger entry


                    DataTable dtVoucherDetails = new DataTable();

                    dtVoucherDetails = new DataTable();
                    if (Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.PkVocherNo].Value)) != 0)
                    {
                        dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,0 AS StatusNo From TVoucherDetails Where FkVoucherNo=" + Convert.ToInt64((GVCashBook.Rows[i].Cells[ColIndex.PkVocherNo].Value)) + " order by VoucherSrNo").Table;
                    }
                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo - 1].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = 1;
                    tVoucherDetails.SignCode = 1;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(GVCashBook[ColIndex.FromLedgerNo, i].Value);
                    tVoucherDetails.Debit = 0;
                    tVoucherDetails.Credit = Convert.ToDouble(GVCashBook[ColIndex.Amount, i].Value);
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.SrNo = Others.Party;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                    //================================================to account ledger entry



                    tVoucherDetails = new TVoucherDetails();
                    tVoucherDetails.PkVoucherTrnNo = (dtVoucherDetails.Rows.Count > 0) ? Convert.ToInt64(dtVoucherDetails.Rows[VoucherSrNo].ItemArray[0].ToString()) : 0;
                    tVoucherDetails.VoucherSrNo = 2;
                    tVoucherDetails.SignCode = 2;
                    tVoucherDetails.LedgerNo = Convert.ToInt64(GVCashBook[ColIndex.ToLedgerNo, i].Value);
                    tVoucherDetails.Debit = Convert.ToDouble(GVCashBook[ColIndex.Amount, i].Value);
                    tVoucherDetails.Credit = 0;
                    tVoucherDetails.Narration = "";
                    tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                    tVoucherDetails.SrNo = 0;
                    dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);

                    if (ObjFunction.GetListValue(lstEntryType) != 34)
                    {

                        if (ObjFunction.GetListValue(lstToAccount) == 23)
                        {
                            tVchChqCredit = new TVoucherChqCreditDetails();
                            tVchChqCredit.PkSrNo = 0;
                            tVchChqCredit.ChequeNo = Convert.ToInt64(GVCashBook[ColIndex.ToChequeNo, i].Value).ToString();
                            tVchChqCredit.ChequeDate = DBGetVal.ServerTime.Date; //(Convert.ToDateTime(GVCashBook[ColIndex.ChequeDate, i].Value) == null) ? Convert.ToDateTime("01-Jan-1900") : Convert.ToDateTime((GVCashBook[ColIndex.ChequeDate, i].Value));
                            tVchChqCredit.BankNo = Convert.ToInt64(GVCashBook[ColIndex.BankNo, i].Value);
                            tVchChqCredit.BranchNo = Convert.ToInt64(GVCashBook[ColIndex.BranchNo, i].Value);
                            tVchChqCredit.CreditCardNo = "";
                            tVchChqCredit.Amount = Convert.ToDouble(GVCashBook[ColIndex.Amount, i].Value);
                            tVchChqCredit.PostFkVoucherNo = 0;
                            tVchChqCredit.PostFkVoucherTrnNo = 0;
                            tVchChqCredit.CompanyNo = DBGetVal.FirmNo;

                            dbTVoucherEntry.AddTVoucherChqCreditDetails(tVchChqCredit);
                        }
                    }



                    tempID = dbTVoucherEntry.ExecuteNonQueryStatements();
                }
                if (tempID != 0)
                {
                    if (ID == 0)
                    {
                        OMMessageBox.Show("Cash Book Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                        FillControls();
                    }
                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                    chkContra.Enabled = true;
                    chkCashReceipt.Enabled = true;
                    chkCashPayment.Enabled = true;
                    chkExpenseEntry.Enabled = true;
                    chkJV.Enabled = true;
                    chkCreditNote.Enabled = true;
                    chkDebitNote.Enabled = true;
                    chkBankReceipt.Enabled = true;
                    chkBankPayment.Enabled = true;
                    chkSelectAll.Enabled = true;
                    dtpBillDate.Enabled = true;
                    GVCashBook.Enabled = false;
                    BtnOK.Enabled = false;
                    BtnX.Enabled = false;
                    pnlVouchertypename.Enabled = true;
                    panel3.Visible = false;
                    GVCashBook.Top = 9;
                    GVCashBook.Height = 470;
                    bindGrid();
                    btnNew.Focus();
                }
                else
                {
                    OMMessageBox.Show("Cash Book not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }


            }

            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                pnlSearch.Visible = true;
                pnlSearch.Enabled = true;
                txtSearchEntryType.Enabled = true;
                btnSearchEntryType.Enabled = true;
                pnlSearchEntryType.Enabled = true;
                lstSearchEntryType.Enabled = true;
                rbEntryType.Enabled = true;

                ObjFunction.FillList(lstSearchEntryType, "Select VoucherTypeCode,VoucherTypeName From MVoucherType Where  VoucherTypeCode in (1,5,27,33,34) ORDER BY VoucherTypeName");
                txtSearchEntryType.Focus();
                btnNew.Enabled = false;
                btnUpdate.Enabled = false;

                rbEntryType.Checked = true;

                //dgPartySearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Form NewF = new MDIParent1();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                GVCashBook.Enabled = true;
                BtnOK.Enabled = true;
                BtnX.Enabled = true;
                GVCashBook.Rows.Clear();
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                txtDocNo.Text = "";// (ObjQry.ReturnLong("Select max(VoucherUserNo) from TVoucherEntry Where VoucherTypeCode=" + VoucherType + " AND VoucherDate>='" + dtFrom.Date + "' AND VoucherDate<='" + dtTo.Date + "'", CommonFunctions.ConStr) + 1).ToString();
                FillList();
                txtAmount.Text = "0.00";
                panel3.Visible = true;
                GVCashBook.Top = 117;
                GVCashBook.Height = 363;

                dtDate.Focus();
                GVCashBook.Rows.Add();


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            txtDocNo.Enabled = false;
            GVCashBook.Enabled = true;
            chkContra.Enabled = false;
            chkCashReceipt.Enabled = false;
            chkCashPayment.Enabled = false;
            chkExpenseEntry.Enabled = false;
            chkCreditNote.Enabled = false;
            chkDebitNote.Enabled = false;
            chkJV.Enabled = false;
            chkBankReceipt.Enabled = false;
            chkBankPayment.Enabled = false;
            chkSelectAll.Enabled = false;
            dtpBillDate.Enabled = true;
            chkSelectAll.Checked = true;
            BtnOK.Enabled = true;
            BtnX.Enabled = true;
            panel3.Visible = true;
            GVCashBook.Top = 117;
            GVCashBook.Height = 363;
            GVCashBook.CurrentCell = GVCashBook[1, GVCashBook.Rows.Count - 1];
            dtDate.Focus();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (Validations() == true)
            {
                DateTime datevalue = dtDate.Value;
                String datevalue2 = datevalue.ToString("dd/MM/yyyy");
                GVCashBook.CurrentCell = GVCashBook[1, GVCashBook.Rows.Count - 1];
                GVCashBook.CurrentRow.Cells[ColIndex.Date].Value = datevalue2.ToString();
                GVCashBook.CurrentRow.Cells[ColIndex.Date1].Value = Convert.ToDateTime(dtDate.Text);
                GVCashBook.CurrentRow.Cells[ColIndex.FromAccount].Value = txtFromAccount.Text;
                GVCashBook.CurrentRow.Cells[ColIndex.ToAccount].Value = txtToAccount.Text;
                GVCashBook.CurrentRow.Cells[ColIndex.Amount].Value = txtAmount.Text;
                GVCashBook.CurrentRow.Cells[ColIndex.VoucherNo].Value = txtDocNo.Text;
                GVCashBook.CurrentRow.Cells[ColIndex.Remark].Value = txtRemark.Text;
                GVCashBook.CurrentRow.Cells[ColIndex.FromLedgerNo].Value = lstFromAccount.SelectedValue;
                GVCashBook.CurrentRow.Cells[ColIndex.ToLedgerNo].Value = lstToAccount.SelectedValue;
                GVCashBook.CurrentRow.Cells[ColIndex.Reference].Value = txtReference.Text;
                GVCashBook.CurrentRow.Cells[ColIndex.IsCancel].Value = false;
                if (ObjFunction.GetListValue(lstEntryType) == 1)////Contra
                {
                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;

                }
                else if (ObjFunction.GetListValue(lstEntryType) == 2)//// Credit Note 
                {
                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;

                }
                else if (ObjFunction.GetListValue(lstEntryType) == 3)//// Debit Note 
                {
                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;

                }
                else if (ObjFunction.GetListValue(lstEntryType) == 5)////JV
                {
                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;
                    //GVCashBook.CurrentRow.Cells[ColIndex.ChequeNo].Value = txtChqNo.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.Bank].Value = cmbBank.Text;
                    //  GVCashBook.CurrentRow.Cells[ColIndex.PartyBank].Value = cmbCompanyBank.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.PartyBranch].Value = cmbBranch.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.ChequeDate].Value = dtpChqDate.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;

                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ToChequeNo].Value = txtChqNo.Text;
                    //   GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ChequeDate].Value = dtpChqDate.Value;
                    //  GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.Bank].Value = cmbBank.Text;
                    // GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.PartyBranch].Value = cmbBranch.Text;
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromAccount].Value = lstFromAccount.Text;
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromLedgerNo].Value = ObjFunction.GetListValue(lstFromAccount);
                }
                else if (ObjFunction.GetListValue(lstEntryType) == 27) ////cash receipt
                {

                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;

                }
                else if (ObjFunction.GetListValue(lstEntryType) == 33)////Expense Entry
                {
                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ChequeNo].Value = txtChequeNo.Text;
                }
                else if (ObjFunction.GetListValue(lstEntryType) == 34)////cash Payment
                {

                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.Bank].Value = cmbBank.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.PartyBank].Value = cmbCompanyBank.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.PartyBranch].Value = cmbBranch.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.ChequeDate].Value = dtpChqDate.Text;
                    GVCashBook.CurrentRow.Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;

                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ToChequeNo].Value = txtChqNo.Text;
                    //  GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ChequeDate].Value = dtpChqDate.Value;
                    //   GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.Bank].Value = cmbBank.Text;
                    //  GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.PartyBranch].Value = cmbBranch.Text;
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromAccount].Value = lstFromAccount.Text;
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromLedgerNo].Value = ObjFunction.GetListValue(lstFromAccount);

                }

                else if (ObjFunction.GetListValue(lstEntryType) == 28)////Bank Receipt
                {
                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;
                    if ((Convert.ToInt32(lstFromAccount.SelectedValue) != 5) && (Convert.ToInt32(lstFromAccount.SelectedValue) != 1))
                    {
                        GVCashBook.CurrentRow.Cells[ColIndex.Bank].Value = cmbBank.Text;
                        //  GVCashBook.CurrentRow.Cells[ColIndex.PartyBank].Value = cmbCompanyBank.SelectedValue;
                        GVCashBook.CurrentRow.Cells[ColIndex.PartyBranch].Value = cmbBranch.Text;
                        GVCashBook.CurrentRow.Cells[ColIndex.ChequeDate].Value = dtpChqDate.Text;
                        GVCashBook.CurrentRow.Cells[ColIndex.BankNo].Value = cmbBank.SelectedValue;
                        GVCashBook.CurrentRow.Cells[ColIndex.BranchNo].Value = cmbBranch.SelectedValue;

                        GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ToChequeNo].Value = txtChqNo.Text;
                        //       GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ChequeDate].Value = dtpChqDate.Value;
                        //     GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.Bank].Value = cmbBank.Text;
                        //   GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.PartyBranch].Value = cmbBranch.Text;
                    }
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromAccount].Value = lstFromAccount.Text;
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromLedgerNo].Value = ObjFunction.GetListValue(lstFromAccount);
                }
                else if (ObjFunction.GetListValue(lstEntryType) == 29)////Bank Payment
                {
                    GVCashBook.CurrentRow.Cells[ColIndex.VoucherType].Value = lstEntryType.SelectedValue;
                    GVCashBook.CurrentRow.Cells[ColIndex.Narration].Value = txtEntryType.Text;
                    GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.ChequeNo].Value = txtChequeNo.Text;

                }
                hidePics();
                Clear();
                GVCashBook.Rows.Add();
                dtDate.Focus();
                GVCashBook.CurrentCell = GVCashBook[0, GVCashBook.Rows.Count - 1];
            }
        }

        public void Clear()
        {
            txtFromAccount.Text = "";
            txtToAccount.Text = "";
            txtAmount.Text = "0.00";
            txtRemark.Text = "";
            txtReference.Text = "";
            txtEntryType.Text = "";
            txtChequeNo.Text = "";
            hidePics();
        }

        private void BtnX_Click(object sender, EventArgs e)
        {
            Clear();
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 2)
                {
                    FillDate = ObjQry.ReturnDate(" select max (voucherdate) from TVoucherEntry WHERE     (TVoucherEntry.VoucherTypeCode IN (" + strvouchertype + "))", CommonFunctions.ConStr);
                }
                else if (type == 1)
                {
                    FillDate = ObjQry.ReturnDate("select min (voucherdate) from TVoucherEntry WHERE     (TVoucherEntry.VoucherTypeCode IN (" + strvouchertype + "))", CommonFunctions.ConStr);

                }
                else if (type == 3)
                {

                    FillDate = ObjQry.ReturnDate("select min (voucherdate) from TVoucherEntry WHERE     (TVoucherEntry.VoucherTypeCode IN (" + strvouchertype + "))and voucherdate>'" + dtpBillDate.Value + "'", CommonFunctions.ConStr);
                    if (FillDate == Convert.ToDateTime("11/11/1111 12:00:00 AM"))
                    { FillDate = ObjQry.ReturnDate(" select max (voucherdate) from TVoucherEntry WHERE     (TVoucherEntry.VoucherTypeCode IN (" + strvouchertype + "))", CommonFunctions.ConStr); }

                    if ((dtpBillDate.Value) < FillDate)
                    {
                        (dtpBillDate.Value) = FillDate;
                    }
                    else
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
                else if (type == 4)
                {
                    FillDate = ObjQry.ReturnDate("select max (voucherdate) from TVoucherEntry WHERE     (TVoucherEntry.VoucherTypeCode IN (" + strvouchertype + ")) and voucherdate<'" + dtpBillDate.Value + "'", CommonFunctions.ConStr);
                    if (FillDate == Convert.ToDateTime("11/11/1111 12:00:00 AM"))
                    { FillDate = ObjQry.ReturnDate(" select min (voucherdate) from TVoucherEntry WHERE     (TVoucherEntry.VoucherTypeCode IN (" + strvouchertype + "))", CommonFunctions.ConStr); }

                    if ((dtpBillDate.Value) > FillDate)
                    {
                        (dtpBillDate.Value) = FillDate;
                    }
                    else
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            //if (FillDate != '')
            //{
            //  
            dtpBillDate.Value = FillDate;
            FillControls();
            // }
        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID)
                {
                    cntRow = i;
                    break;
                }
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;

        }

        public void MoveNext()
        {

        }

        public void MovePrev()
        {

        }


        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }
        #endregion

        #region KeyDown Events
        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && e.Control)
            {
                if (btnPrev.Enabled) btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (btnFirst.Enabled) btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                if (btnNext.Enabled) btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (btnLast.Enabled) btnLast_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }
            //else if (e.KeyCode == Keys.Escape)
            //{
            //    BtnExit_Click(sender, e);
            //}
        }
        #endregion

        private void txtRefNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtDate.Focus();
            }
        }

        private void dtpDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtDate.Focus();
            }
        }

        private void dtDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtEntryType.Focus();
                //  bindGrid();
            }
        }

        private void txtEntryType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtEntryType.Text == "")
                {
                    pnlEntryType.Visible = true;
                    lstEntryType.Focus();
                    lstEntryType.SelectedIndex = 0;
                    txtFromAccount.Text = "";
                    txtToAccount.Text = "";
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";

                }
                else
                {
                    pnlEntryType.Visible = false;
                    txtFromAccount.Text = "";
                    txtToAccount.Text = "";
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";
                    pnlFromAccount.Visible = true;
                    lstFromAccount.Visible = true;
                    lstFromAccount.Focus();

                }
            }


            else
            {
                pnlEntryType.Visible = true;
                lstEntryType.Focus();
            }



            //else if (e.KeyChar == Convert.ToChar(Keys.Back))
            //{
            //    txtEntryType.Text = "";
            //}

            //else
            //{
            //    e.KeyChar = Convert.ToChar(0);

            //}
        }

        private void lstEntryType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtEntryType.Text = lstEntryType.Text;
                    pnlEntryType.Visible = false;
                    // txtFromAccount.Focus();
                    //pnlFromAccount.Visible = true;
                    //lstFromAccount.Visible = true;
                    //lstFromAccount.Focus();
                    txtFromAccount.Focus();
                    txtDocNo.Text = (ObjQry.ReturnLong("select Max(voucheruserno) as voucheruserno from tvoucherentry where vouchertypecode=" + lstEntryType.SelectedValue, CommonFunctions.ConStr) + 1).ToString();
                    FillList();
                    txtFromAccount.Text = "";
                    txtToAccount.Text = "";
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    // txtEntryType.Focus();
                    lstFromAccount.Focus();
                }


            }





            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtFromAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtFromAccount.Text == "")
                {
                    pnlFromAccount.Visible = true;
                    lstFromAccount.Focus();
                    lstFromAccount.SelectedIndex = 0;
                    txtToAccount.Text = "";
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";

                }
                else
                {
                    pnlFromAccount.Visible = false;
                    txtToAccount.Text = "";
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";
                    pnlToAccount.Visible = true;
                    lstToAccount.Visible = true;
                    lstToAccount.Focus();
                }
            }
            else
            {
                txtFromAccount.Text = "";
                pnlFromAccount.Visible = true;
                lstFromAccount.Focus();
            }
        }

        private void lstFromAccount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtFromAccount.Text = lstFromAccount.Text;
                    pnlFromAccount.Visible = false;


                    txtToAccount.Focus();
                    txtToAccount.Text = "";
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";

                    if ((ObjQry.ReturnLong("select groupno from Mledger where ledgerno=" + ObjFunction.GetListValue(lstFromAccount), CommonFunctions.ConStr)) == GroupType.BankAccounts)
                    {

                        lblchequeno.Visible = true;
                        txtChequeNo.Visible = true;

                        label8.Location = new Point(440, 74);
                        txtRemark.Location = new Point(510, 67);

                        label9.Location = new Point(685, 74);
                        txtReference.Location = new Point(767, 67);
                        txtToAccount.Focus();
                    }
                    else
                    {
                        txtToAccount.Focus();
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    //txtFromAccount.Focus();
                    lstToAccount.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtToAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtToAccount.Text == "")
                {
                    pnlToAccount.Visible = true;
                    lstToAccount.Focus();
                    lstToAccount.SelectedIndex = 0;
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";
                }
                else
                {
                    pnlToAccount.Visible = false;
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";
                    txtAmount.Focus();

                }
            }
            else
            {

                pnlToAccount.Visible = true;
                lstToAccount.Focus();
            }
        }

        private void lstToAccount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtToAccount.Text = lstToAccount.Text;
                    pnlToAccount.Visible = false;
                    txtAmount.Text = "0.00";
                    txtRemark.Text = "";
                    txtReference.Text = "";
                    txtAmount.Focus();

                    if ((ObjQry.ReturnLong("select groupno from Mledger where ledgerno=" + ObjFunction.GetListValue(lstFromAccount), CommonFunctions.ConStr)) == GroupType.BankAccounts)
                    {

                        //pnlchq.Visible = true;
                        // txtChqNo.Visible = true;
                        //cmbBank.Visible = true;
                        //cmbBranch.Visible = true;
                        //cmbCompanyBank.Visible = true;
                        //dtpChqDate.Visible = true;
                        //btnchqOk.Visible = true;
                        //btnchqCancel.Visible = true;
                        //pnlchq.BringToFront();
                        // txtChqNo.Focus();

                        lblchequeno.Visible = true;
                        txtChequeNo.Visible = true;
                        //label8.Location = new Point(220, 74);
                        //txtRemark.Location = new Point(290, 67);

                        //label9.Location = new Point(470, 74);
                        //txtReference.Location = new Point(560, 67);
                    }
                    else
                    {
                        txtAmount.Focus();
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtToAccount.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtChequeNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtRemark.Focus();
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                if ((txtAmount.Text != "") && (Convert.ToDouble(txtAmount.Text) > 0.0))
                {
                    if ((ObjQry.ReturnLong("select groupno from Mledger where ledgerno=" + ObjFunction.GetListValue(lstFromAccount), CommonFunctions.ConStr)) == GroupType.BankAccounts)
                    {
                        //if (txtChequeNo.Text == "")
                        //{
                        txtChequeNo.Focus();
                        // }
                    }

                    //else if ((ObjQry.ReturnLong("select groupno from Mledger where ledgerno=" + ObjFunction.GetListValue(lstToAccount), CommonFunctions.ConStr)) == GroupType.BankAccounts)
                    //{
                    //    txtChqNo.Focus();
                    //}
                    else
                    {
                        txtRemark.Focus();
                    }
                }
                else { txtAmount.Focus(); }
            }
        }

        private void txtRemark_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtReference.Focus();
            }
        }

        private void txtReference_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                BtnOK.Focus();
            }
        }

        private void txtChqNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtpChqDate.Focus();
            }
        }

        private void dtpChqDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cmbBank.Focus();
            }
        }

        private void cmbBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbBranch.Focus();

            }
        }

        private void cmbBranch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbCompanyBank.Focus();

            }
        }

        private void cmbCompanyBank_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnchqOk.Focus();
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtAmount, 2, 10, OMFunctions.MaskedType.NotNegative);
        }

        private void txtChqNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtChqNo, -1, 10);
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int Date = 1;
            public static int VoucherNo = 2;
            public static int FromAccount = 3;
            public static int ToAccount = 4;
            public static int Amount = 5;

            public static int Remark = 6;
            public static int Narration = 7;
            public static int ChequeNo = 8;
            public static int FromLedgerNo = 9;
            public static int VoucherType = 10;
            public static int Bank = 11;
            public static int PartyBank = 12;
            public static int PartyBranch = 13;
            public static int ChequeDate = 14;
            public static int PkVocherNo = 15;
            public static int ToLedgerNo = 16;
            public static int BankNo = 17;
            public static int BranchNo = 18;
            public static int ToChequeNo = 19;
            public static int Reference = 20;
            public static int Date1 = 21;
            public static int IsCancel = 22;
            public static int SelectPrint = 23;
            public static int Print = 24;

        }
        #endregion

        private void GVCashBook_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkCashReceipt.Checked = false;
            chkJV.Checked = false;
            chkExpenseEntry.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankPayment.Checked = false;
            chkBankReceipt.Checked = false;
            strvouchertype = "1,2,3,5,27,28,29,33,34";
            bindGrid();
        }

        private void GVCashBook_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                if (OMMessageBox.Show("Are you sure you want to DELETE this ENTRIES ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    long PkVocherNo = Convert.ToInt64(GVCashBook.CurrentRow.Cells[ColIndex.PkVocherNo].Value);
                    if (PkVocherNo == 0)
                    {
                        if (GVCashBook.Rows.Count - 1 == GVCashBook.CurrentCell.RowIndex)
                        {
                            GVCashBook.Rows.RemoveAt(GVCashBook.CurrentCell.RowIndex);
                            GVCashBook.Rows.Add();
                        }
                        else
                            GVCashBook.Rows.RemoveAt(GVCashBook.CurrentCell.RowIndex);
                    }
                    else
                    {

                        GVCashBook.CurrentRow.Cells[ColIndex.IsCancel].Value = true;
                        GVCashBook.CurrentRow.DefaultCellStyle.BackColor = Color.LightPink;
                    } //ObjTrans.ExecuteQuery("update TVoucherEntry set IsCancel='true' where PkVoucherNo=" + PkVocherNo + "", CommonFunctions.ConStr);
                }
            }
            if (e.KeyCode == Keys.Enter)
            {
                string StrBillNo = "";
                if (GVCashBook.CurrentCell.ColumnIndex == ColIndex.Print)
                {
                    DialogResult ds = OMMessageBox.Show("Are you sure you want to Print " + (GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.Narration].Value).ToString() + " ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                    if (ds == DialogResult.Yes)
                    {
                        PrintBill(StrBillNo, 0);
                    }
                    else if (ds == DialogResult.Cancel)
                    {
                        PrintBill(StrBillNo, 1);
                    }
                }
            }
        }

        private void btnchqCancel_Click(object sender, EventArgs e)
        {
            pnlchq.Visible = false;
            BtnSave.Enabled = true;
            txtChqNo.Text = "";
            cmbBank.SelectedIndex = 0;
            cmbBranch.SelectedIndex = 0;

            GVCashBook.Focus();
            if (GVCashBook.CurrentCell.RowIndex < GVCashBook.Rows.Count - 1)
                GVCashBook.CurrentCell = GVCashBook[ColIndex.Amount, GVCashBook.CurrentCell.RowIndex + 1];
            else
                GVCashBook.CurrentCell = GVCashBook[ColIndex.Amount, GVCashBook.Rows.Count - 1];

        }

        private bool ChqValidations()
        {
            bool tempflag = false;
            try
            {
                if (txtChqNo.Text.Trim() == "")
                {
                    txtChqNo.Focus();
                }
                else if (Convert.ToInt64(cmbBank.SelectedValue) == 0)
                {
                    cmbBank.Focus();
                }
                else if (VoucherType == VchType.Sales && Convert.ToInt64(cmbCompanyBank.SelectedValue) == 0)
                {
                    cmbCompanyBank.Focus();
                }
                else if (Convert.ToInt64(cmbBranch.SelectedValue) == 0)
                {
                    if (VoucherType == VchType.Sales)
                    {
                        cmbBranch.Focus();
                    }
                    else if (VoucherType == VchType.Purchase)
                        tempflag = true;
                }
                else
                    tempflag = true;
                return tempflag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);

                return false;
            }
        }

        private void btnchqOk_Click(object sender, EventArgs e)
        {

            try
            {
                if (ChqValidations() == true)
                {
                    BtnSave.Enabled = true;


                    if (VoucherType == VchType.Journal)
                    {
                        GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromLedgerNo].Value = ObjFunction.GetComboValue(cmbCompanyBank);//dtPayTypeLedger.Rows[1].ItemArray[1].ToString();
                        LedgNo = ObjFunction.GetComboValue(cmbCompanyBank);//Convert.ToInt64(dtPayTypeLedger.Rows[1].ItemArray[1].ToString());
                    }
                    else
                    {
                        GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromLedgerNo].Value = ObjFunction.GetComboValue(cmbBank);
                        LedgNo = ObjFunction.GetComboValue(cmbBank);

                    }


                    pnlchq.Visible = false;
                    GVCashBook.Focus();
                    if (GVCashBook.CurrentCell.RowIndex < GVCashBook.Rows.Count - 1)
                        GVCashBook.CurrentCell = GVCashBook[ColIndex.Amount, GVCashBook.CurrentCell.RowIndex + 1];
                    txtRemark.Focus();


                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CashBookAE_Click(object sender, EventArgs e)
        {
            hidePics();
        }

        private void Panel2_Click(object sender, EventArgs e)
        {
            if (pnlEntryType.Visible == true)
            {
                pnlEntryType.Visible = false;
                txtEntryType.Focus();
            }
            else if (pnlFromAccount.Visible == true)
            {
                pnlFromAccount.Visible = false;
                txtFromAccount.Focus();
            }
            else if (pnlToAccount.Visible == true)
            {
                pnlToAccount.Visible = false;
                txtToAccount.Focus();
            }
            else if (pnlSearchEntryType.Visible == true)
            {
                pnlSearchEntryType.Visible = false;
                txtSearchEntryType.Focus();
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (pnlEntryType.Visible == true)
            {
                pnlEntryType.Visible = false;
                txtEntryType.Focus();
            }
            else if (pnlFromAccount.Visible == true)
            {
                pnlFromAccount.Visible = false;
                txtFromAccount.Focus();
            }
            else if (pnlToAccount.Visible == true)
            {
                pnlToAccount.Visible = false;
                txtToAccount.Focus();
            }
            else if (pnlSearchEntryType.Visible == true)
            {
                pnlSearchEntryType.Visible = false;
                txtSearchEntryType.Focus();
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (pnlEntryType.Visible == true)
            {
                pnlEntryType.Visible = false;
                txtEntryType.Focus();
            }
            else if (pnlFromAccount.Visible == true)
            {
                pnlFromAccount.Visible = false;
                txtFromAccount.Focus();
            }
            else if (pnlToAccount.Visible == true)
            {
                pnlToAccount.Visible = false;
                txtToAccount.Focus();
            }
            else if (pnlSearchEntryType.Visible == true)
            {
                pnlSearchEntryType.Visible = false;
                txtSearchEntryType.Focus();
            }
        }

        private void pnlVouchertypename_Click(object sender, EventArgs e)
        {
            if (pnlEntryType.Visible == true)
            {
                pnlEntryType.Visible = false;
                txtEntryType.Focus();
            }
            else if (pnlFromAccount.Visible == true)
            {
                pnlFromAccount.Visible = false;
                txtFromAccount.Focus();
            }
            else if (pnlToAccount.Visible == true)
            {
                pnlToAccount.Visible = false;
                txtToAccount.Focus();
            }
            else if (pnlSearchEntryType.Visible == true)
            {
                pnlSearchEntryType.Visible = false;
                txtSearchEntryType.Focus();
            }
        }

        private void chkCashPayment_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkSelectAll.Checked = false;
            chkCashReceipt.Checked = false;
            chkJV.Checked = false;
            chkExpenseEntry.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankReceipt.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "34";
            bindGrid();
        }

        private void chkJV_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkCashReceipt.Checked = false;
            chkSelectAll.Checked = false;
            chkExpenseEntry.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankReceipt.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "5";
            bindGrid();
        }

        private void chkContra_CheckedChanged(object sender, EventArgs e)
        {
            chkJV.Checked = false;
            chkCashPayment.Checked = false;
            chkCashReceipt.Checked = false;
            chkSelectAll.Checked = false;
            chkExpenseEntry.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankReceipt.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "1";
            bindGrid();
        }

        private void chkCashReceipt_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkJV.Checked = false;
            chkSelectAll.Checked = false;
            chkExpenseEntry.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankReceipt.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "27";
            bindGrid();
        }

        private void chkExpenseEntry_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkJV.Checked = false;
            chkSelectAll.Checked = false;
            chkCashReceipt.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankReceipt.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "33";
            bindGrid();
        }

        private void chkCreditNote_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkJV.Checked = false;
            chkSelectAll.Checked = false;
            chkCashReceipt.Checked = false;
            chkDebitNote.Checked = false;
            chkBankReceipt.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "2";
            bindGrid();
        }

        private void chkDebitNote_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkJV.Checked = false;
            chkSelectAll.Checked = false;
            chkCashReceipt.Checked = false;
            chkCreditNote.Checked = false;
            chkBankReceipt.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "3";
            bindGrid();
        }

        private void chkBankReceipt_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkJV.Checked = false;
            chkSelectAll.Checked = false;
            chkCashReceipt.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankPayment.Checked = false;
            strvouchertype = "28";
            bindGrid();
        }

        private void chkBankPayment_CheckedChanged(object sender, EventArgs e)
        {
            chkContra.Checked = false;
            chkCashPayment.Checked = false;
            chkJV.Checked = false;
            chkSelectAll.Checked = false;
            chkCashReceipt.Checked = false;
            chkCreditNote.Checked = false;
            chkDebitNote.Checked = false;
            chkBankReceipt.Checked = false;
            strvouchertype = "29";
            bindGrid();
        }

        private void txtSearchEntryType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSearchEntryType.Text == "")
                {
                    pnlSearchEntryType.Visible = true;
                    lstSearchEntryType.Focus();
                    lstSearchEntryType.SelectedIndex = 0;


                }
                else
                {
                    pnlSearchEntryType.Visible = false;
                    btnSearchEntryType.Focus();

                }
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);

            }
        }

        private void lstSearchEntryType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtSearchEntryType.Text = lstSearchEntryType.Text;
                    pnlSearchEntryType.Visible = false;
                    btnSearchEntryType.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtSearchEntryType.Focus();

                }


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstEntryType_Leave(object sender, EventArgs e)
        {
            pnlEntryType.Visible = false;
            //lstFromAccount.Focus();
            txtFromAccount.Focus();
        }

        private void lstFromAccount_Leave(object sender, EventArgs e)
        {
            pnlFromAccount.Visible = false;
            txtToAccount.Focus();
        }

        private void lstToAccount_Leave(object sender, EventArgs e)
        {
            pnlToAccount.Visible = false;
            txtAmount.Focus();
        }

        private void lstSearchEntryType_Leave(object sender, EventArgs e)
        {
            pnlSearchEntryType.Visible = false;
            txtSearchEntryType.Focus();
        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;

        }

        private void txtFromAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void GVCashBook_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string StrBillNo = "";
            if (GVCashBook.CurrentCell.ColumnIndex == ColIndex.Print)
            {
                DialogResult ds = OMMessageBox.Show("Are you sure you want to Print " + (GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.Narration].Value).ToString() + " ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                if (ds == DialogResult.Yes)
                {
                    PrintBill(StrBillNo, 0);
                }
                else if (ds == DialogResult.Cancel)
                {
                    PrintBill(StrBillNo, 1);
                }
            }
        }

        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            bindGrid();
        }

        private void txtAmount_Leave_1(object sender, EventArgs e)
        {
            //if (txtAmount.Text == "0.00")
            //{
            //    txtAmount.Focus();

            //}
            //if (txtChequeNo.Text == "")
            //{
            //    txtChequeNo.Focus();
            //}
            //else
            //{
            //    txtRemark.Focus();
            //}
        }

        public void PrintBill(string PkVoucherNo, int PType)
        {
            long PkVoucherNoId = Convert.ToInt64(GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.PkVocherNo].Value);

            string[] ReportSession;
            DataTable dtPrint = ObjFunction.GetDataView("SELECT VoucherDate, BilledAmount, Remark,IsNull((Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=501 And FkVoucherNo=PkVoucherNo),0) as PartyAmount ,IsNull( (Select (Case when (Debit>0) then Debit Else Credit end) From TVoucherDetails Where SrNo=502 And FkVoucherNo=PkVoucherNo),0) as DiscAmt  FROM TVoucherEntry Where PkVoucherNo In(" + PkVoucherNoId + ") ").Table;
            for (int i = 0; i < dtPrint.Rows.Count; i++)
            {

                ReportSession = new string[11];
                ReportSession[0] = DBGetVal.FirmName;
                ReportSession[1] = (GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.Narration].Value).ToString();
                ReportSession[2] = dtPrint.Rows[i].ItemArray[0].ToString();
                ReportSession[3] = (Convert.ToDouble(dtPrint.Rows[i].ItemArray[3].ToString()) - Convert.ToDouble(dtPrint.Rows[i].ItemArray[4].ToString())).ToString();
                ReportSession[4] = NumberToWordsIndian.getWords(ReportSession[3].ToString());
                ReportSession[5] = dtPrint.Rows[i].ItemArray[2].ToString();
                ReportSession[6] = GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.FromAccount].Value.ToString();
                ReportSession[7] = GVCashBook.Rows[GVCashBook.CurrentCell.RowIndex].Cells[ColIndex.VoucherNo].Value.ToString();
                ReportSession[8] = dtPrint.Rows[i].ItemArray[4].ToString();
                if (Convert.ToInt32(cmbCompanyBank.SelectedValue) != 0)
                {
                    ReportSession[9] = cmbCompanyBank.Text.ToUpper();
                }
                else
                {
                    ReportSession[9] = "";

                }
                ReportSession[10] = txtChqNo.Text.ToUpper();
                if (PType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    childForm = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.GetCollectionPrint");
                    else
                        childForm = ObjFunction.LoadReportObject("GetCollectionPrint.rpt", CommonFunctions.ReportPath);
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            DisplayMessage("" + ((VoucherType == 15) ? "Receipt" : "Payment") + " Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("" + ((VoucherType == 9) ? "Receipt" : "Payment") + " not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
                else
                {
                    Form NewF = null;

                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetCollectionPrint(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetCollectionPrint.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
        }

        private void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            Form NewF = new Display.CashBook();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void txtChequeNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(txtChequeNo, 2, 10, OMFunctions.MaskedType.NotNegative);
        }



    }
}
