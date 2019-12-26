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
    public partial class LedgerOpeningBalance : Form
    {
        //DBMLedger dbmledger = new DBMLedger();
        CommonFunctions ObjFunction = new CommonFunctions();
        //MLedger mLedger = new MLedger();

        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TVoucherDetails tVoucherDetails = new TVoucherDetails();
        TVoucherEntryCompany tVoucherEntryComp = new TVoucherEntryCompany();
        TVoucherDetailsCompany tVoucherDetailsComp = new TVoucherDetailsCompany();
        long vouchertypecode = 0;
        int GroupTypeCode;
        long MainPkVoucherNo = 0;
        long MainPKVoucherCompanyNo = 0;
        string RVchType ="0";
        #region Initialize And Load Related Methods

        public LedgerOpeningBalance()
        {
            InitializeComponent();
        }

        public LedgerOpeningBalance(int type)
        {
            InitializeComponent();
            GroupTypeCode = type;
            if (DBGetVal.KachhaFirm == false)
            {
                vouchertypecode = VchType.OpeningBalance;
                RVchType = "30,31";
            }
            else {
                vouchertypecode = VchType.DOpeningBalance;
                RVchType = "130,131";
            }
                if (GroupTypeCode == 1)
            {
                this.Text = "Customer Opening Balance ";
                rdAllLedger.Text = "All Customer";
                rdLedgerwise.Text = "Customer Wise";
                TotalLedger.Text = "Total Customer :";
                lblChkHelp.Text = "To Pay enter data in format -200\r\nTo Receive enter data in format  200\r\n\r\n";
                lblNoteOpeningBal.Text = " = Collection Done Against Opening balance.";
            }
            else if (GroupTypeCode == 2)
            {

                this.Text = "Supplier Opening Balance ";
                rdAllLedger.Text = "All Supplier";
                rdLedgerwise.Text = "Supplier Wise";
                TotalLedger.Text = "Total Supplier :";
                lblChkHelp.Text = "To Pay enter data in format 200\r\nTo Receive enter data in format  -200\r\n\r\n";
                lblNoteOpeningBal.Text = " = Payment Done Against Opening balance.";
            }
            else if (GroupTypeCode == 3)
            {

                this.Text = "Bank Opening Balance ";
                rdAllLedger.Text = "All Bank";
                rdLedgerwise.Text = "Bank Wise";
                TotalLedger.Text = "Total Bank :";
                lblChkHelp.Text = "To Debit enter data in format 200\r\nTo Credit enter data in format  -200\r\n\r\n";
                lblNoteOpeningBal.Text = " = Adjustment Done Against Opening balance.";
            }
            else if (GroupTypeCode == 4)
            {

                this.Text = "Tax Opening Balance ";
                rdAllLedger.Text = "All Tax";
                rdLedgerwise.Text = "Tax Wise";
                TotalLedger.Text = "Total Tax :";
                lblChkHelp.Text = "To Debit enter data in format 200\r\nTo Credit enter data in format  -200\r\n\r\n";
                lblNoteOpeningBal.Text = " = Adjustment Done Against Opening balance.";
            }
            else if (GroupTypeCode == 5)
            {

                this.Text = "Other Opening Balance ";
                rdAllLedger.Text = "All Other";
                rdLedgerwise.Text = "Other Wise";
                TotalLedger.Text = "Total Other :";
                lblChkHelp.Text = "To Debit enter data in format 200\r\nTo Credit enter data in format  -200\r\n\r\n";
                lblNoteOpeningBal.Text = " = Adjustment Done Against Opening balance.";
            }
        }

        private void setVoucherNos()
        {
            string strQuery = "Select TVoucherEntry.PkVoucherNo , 0 as PKVoucherCompanyNo " +
                        " FROM  TVoucherEntry  WHERE " +
                        " TVoucherEntry.VoucherTypeCode = " + vouchertypecode;
            DataTable dt = ObjFunction.GetDataView(strQuery).Table;
            if (dt.Rows.Count >0)
            {
                MainPkVoucherNo = Convert.ToInt64(dt.Rows[0][0].ToString());
                if (dt.Rows[0][1] != null && dt.Rows[0][1].ToString().Length > 0)
                {
                    MainPKVoucherCompanyNo = Convert.ToInt64(dt.Rows[0][1].ToString());
                }
                    //dtpBillDate.Value = Convert.ToDateTime(dt.Rows[0][2].ToString());
            }
        }

        private void LedgerOpeningBalance_Load(object sender, EventArgs e)
        {
            try
            {
                //Form NewFrm = new Vouchers.FirmSelection();
                //ObjFunction.OpenForm(NewFrm);

                //MainMfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                //lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;

                setVoucherNos();

                DataGridViewComboBoxColumn cmbSign = GridLedger.Columns[4] as DataGridViewComboBoxColumn;
                ObjFunction.FillComb(cmbSign, "SELECT SignCode, SignName FROM MSign");
                txtTotal.Text = "0";

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        #region Radio Button Related Methods

        private void rdAllLedger_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxSearch.Visible = false;
            TotalLedger.Visible = true;
            txtTotal.Visible = true;
            BindGrid();
        }

        private void rdLedgerwise_CheckedChanged(object sender, EventArgs e)
        {
            txtBoxSearch.Text = "";
            txtBoxSearch.Visible = true;
            txtBoxSearch.Focus();
            txtTotal.Text = "0";
            btnApplyChange.Enabled = false;
            GridLedger.Rows.Clear();
        }

        #endregion

        #region Grid Related Methods

        public void BindGrid()
        {
            try
            {
                
                string strQuery="";
                strQuery = "SELECT 0 as SrNo, MLedger.LedgerName  + '-' + isnull(MCity.CityName,'No City Found') as LedgerName, MGroup.GroupName, " +
                            " ISNULL(TVoucherDetails.Debit + TVoucherDetails.Credit,0) As OpeningBalance, " +
                            " ISNULL(TVoucherDetails.SignCode,1) As SignCode, " +
                            " MLedger.LedgerNo, 'false' as chk, " +
                            " ISNULL(TVoucherDetails.Debit + TVoucherDetails.Credit,0) As OpBal, " +
                            " ISNULL(TVoucherDetails.PkVoucherTrnNo,0) As PkVoucherTrnNo, " +
                            " ISNULL(TVoucherDetails.FkVoucherNo,0) As FkVoucherNo, " +
//   " (Case When((Select count(*) from TVoucherRefDetails where LedgerNo=MLedger.LedgerNo and TypeOfRef=5)=0) " +
                 "( Case When((Select count(*) from TVoucherRefDetails where LedgerNo = MLedger.LedgerNo and TypeOfRef = 5 and fkvouchertrnno in (select pkvouchertrnno from tvoucherdetails where fkvoucherno in " +
                 " (select pkvoucherno from tvoucherentry where vouchertypecode in (" + RVchType + "))) )= 0 ) " +
                 " Then 'false' else 'true' End) As chkOp  FROM   MGroup INNER JOIN " +
                " MLedger ON MGroup.GroupNo = MLedger.GroupNo LEFT OUTER JOIN " +
                            //" MSign ON MLedger.SignCode = MSign.SignCode LEFT OUTER JOIN " +
                 " TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo " +
                                    " AND TVoucherDetails.FkVoucherNo = " + MainPkVoucherNo + " " +
                                   " INNER JOIN MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo " +
" left outer JOIN MCity ON MLedgerDetails.CityNo = MCity.CityNo " +
                " LEFT OUTER JOIN  MSign ON TVoucherDetails.SignCode = MSign.SignCode ";

                if (GroupTypeCode == 1)
                    strQuery += " WHERE  MLedger.GroupNo IN (" + GroupType.SundryDebtors + ")";
                else if (GroupTypeCode == 2)
                    strQuery += " WHERE MLedger.GroupNo IN (" + GroupType.SundryCreditors + ")";
                else if (GroupTypeCode == 3)
                    strQuery += " WHERE MLedger.GroupNo IN (" + GroupType.BankAccounts + "," + GroupType.CashInhand + ")";
                else if (GroupTypeCode == 4)
                    strQuery += " WHERE MLedger.GroupNo IN (" + GroupType.DutiesAndTaxes + ") OR MLedger.GroupNo IN (select GroupNo From MGroup Where ControlGroup in(" + GroupType.DutiesAndTaxes + "))";
                else if (GroupTypeCode == 5)
                    strQuery += " WHERE MLedger.GroupNo NOT IN (" + GroupType.SundryDebtors + "," + GroupType.SundryCreditors + "," + GroupType.BankAccounts + "," + GroupType.DutiesAndTaxes + "," + GroupType.CashInhand + ") AND MLedger.GroupNo Not IN (select GroupNo From MGroup Where ControlGroup in(" + GroupType.DutiesAndTaxes + "))";

                if (rdAllLedger.Checked == false)
                    strQuery += " AND (MLedger.LedgerName LIKE '" + txtBoxSearch.Text.Trim() + "%') ";

                //if (MainPkVoucherNo != 0 && MainMfgCompNo != 0)
                //    strQuery += " AND TVoucherEntry.PkVoucherNo = " + MainPkVoucherNo + " AND " +
                //                " TVoucherEntryCompany.MfgCompNo = 1 AND TVoucherDetailsCompany.MfgCompNo = 1";

                //strQuery += " AND TVoucherDetails.PkVoucherTrnNo IS NOT NULL ";

                strQuery += " ORDER BY MLedger.LedgerName ";

                DataTable dt = ObjFunction.GetDataView(strQuery).Table;
                txtTotal.Text = dt.Rows.Count.ToString();
                GridLedger.Rows.Clear();
                GridLedger.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                GridLedger.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GridLedger.Rows.Add();
                    for (int j = 0; j < GridLedger.Columns.Count; j++)
                    {
                        GridLedger.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];

                        if (j == 3 || j == 7)
                        {
                            if (GroupTypeCode == 1 || GroupTypeCode == 2)
                                GridLedger.Rows[i].Cells[j].ReadOnly = Convert.ToBoolean(dt.Rows[i].ItemArray[10].ToString());

                            if (Convert.ToInt32(dt.Rows[i].ItemArray[4]) == 1)
                            {
                                if (GroupTypeCode == 1)
                                    GridLedger.Rows[i].Cells[j].Value = Math.Abs(Convert.ToInt32(dt.Rows[i].ItemArray[j]));
                                else if (GroupTypeCode == 2)
                                    GridLedger.Rows[i].Cells[j].Value = Convert.ToInt32(dt.Rows[i].ItemArray[j]) * -1;
                            }
                            else if (Convert.ToInt32(dt.Rows[i].ItemArray[4]) == 2)
                            {
                                if (GroupTypeCode == 1)
                                    GridLedger.Rows[i].Cells[j].Value = Convert.ToInt32(dt.Rows[i].ItemArray[j]) * -1;
                                else if (GroupTypeCode == 2)
                                    GridLedger.Rows[i].Cells[j].Value = Math.Abs(Convert.ToInt32(dt.Rows[i].ItemArray[j]));
                            }
                        }

                    }
                }
                if (dt.Rows.Count <= 0)
                {
                    btnApplyChange.Enabled = false;
                    GridLedger.Visible = true;
                }
                else
                {
                    if (GroupTypeCode == 1 || GroupTypeCode == 2)
                    {
                        GridLedger.Columns[2].Visible = false;
                        GridLedger.Columns[4].Visible = false;
                    }


                    btnApplyChange.Enabled = true;
                    GridLedger.Visible = true;
                    TotalLedger.Visible = true;
                    txtTotal.Visible = true;
                    if (rdAllLedger.Checked == true)
                    {
                        GridLedger.Focus();
                        GridLedger.CurrentCell = GridLedger[3, 0];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GridLedger_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool flag = true;
                for (int i = 0; i < GridLedger.Rows.Count; i++)
                {
                    if (e.ColumnIndex == 3)
                    {
                        if (GridLedger.Rows[i].Cells[3].Value != null)
                        {
                            GridLedger.Rows[i].Cells[3].ErrorText = "";
                            if (ObjFunction.CheckValidAmount(GridLedger.Rows[i].Cells[3].Value.ToString()) == false)
                            {
                                GridLedger.Rows[i].Cells[3].ErrorText = "Please Enter Valid Amount";
                                flag = false;
                                break;
                            }
                            else
                            {
                                if (Convert.ToDouble(GridLedger.Rows[i].Cells[3].Value.ToString()) != Convert.ToDouble(GridLedger.Rows[i].Cells[7].Value.ToString()))
                                    GridLedger.Rows[i].Cells[6].Value = true;
                                else
                                    GridLedger.Rows[i].Cells[6].Value = false;
                                flag = true;
                            }
                        }
                        else
                        {
                            GridLedger.Rows[i].Cells[3].ErrorText = "Please Enter  Amount";
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag == true)
                    btnApplyChange.Enabled = true;
                else
                    btnApplyChange.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GridLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnApplyChange.Focus();
            }
        }

        private void GridLedger_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;
            }
            if (e.ColumnIndex == 0)
            {
                if (GroupTypeCode == 1 || GroupTypeCode == 2)
                {
                    if (GridLedger.Rows[e.RowIndex].Cells[3].ReadOnly == true)
                        GridLedger.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.BlanchedAlmond;
                    else
                        GridLedger.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                }
            }

        }

        #endregion

        #region Button And Text Box Related Methods

        private void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void txtBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (GridLedger.Rows.Count > 0)
                {
                    GridLedger.Focus();
                    GridLedger.CurrentCell = GridLedger[3, 0];
                }
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(1200);
            lblMsg.Visible = false;
        }

        private void btnApplyChange_Click(object sender, EventArgs e)
        {
            try
            {
                int cnt = 0;
                if (OMMessageBox.Show("Are you sure want to change Ledger opening balances?",
                    CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {

                    dbTVoucherEntry = new DBTVaucherEntry();

                    //TVoucherEntry
                    #region TVoucherEntry
                    tVoucherEntry = new TVoucherEntry();
                    tVoucherEntry.PkVoucherNo = MainPkVoucherNo;
                    tVoucherEntry.VoucherTypeCode = vouchertypecode;
                    tVoucherEntry.VoucherUserNo = 1;
                    //if dtpBillDate.Text.Cast
                     tVoucherEntry.VoucherDate = Convert.ToDateTime("1-Apr-2018");
                 //   tVoucherEntry.VoucherDate = Convert.ToDateTime(dtpBillDate.Text);
                    tVoucherEntry.VoucherTime = Convert.ToDateTime(dtpBillDate.Text);
                    tVoucherEntry.Narration = "Opening Balance";
                    tVoucherEntry.Reference = "Opening Balance";
                    tVoucherEntry.ChequeNo = 0;
                    tVoucherEntry.ClearingDate = Convert.ToDateTime(dtpBillDate.Text);
                    tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                    tVoucherEntry.BilledAmount = 0;
                    tVoucherEntry.ChallanNo = "Opening Balance";
                    tVoucherEntry.Remark = "Opening Balance";
                    tVoucherEntry.MacNo = DBGetVal.MacNo;
                    tVoucherEntry.PayTypeNo = 0;
                    tVoucherEntry.RateTypeNo = 0;
                    tVoucherEntry.TaxTypeNo = 0;
                    tVoucherEntry.OrderType = 0;
                    tVoucherEntry.DiscPercent = 0;
                    tVoucherEntry.DiscAmt = 0;
                    tVoucherEntry.MixMode = 0;
                    tVoucherEntry.TransporterCode = 0;
                    tVoucherEntry.TransPayType = 0;
                    tVoucherEntry.LRNo = "";
                    tVoucherEntry.TransportMode = 0;
                    tVoucherEntry.TransNoOfItems = 0;
                    tVoucherEntry.IsItemLevelDisc = false;
                    tVoucherEntry.IsFooterLevelDisc = false;
                    tVoucherEntry.UserID = DBGetVal.UserID;
                    tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                    tVoucherEntry.IsBillMulti = 0;
                    tVoucherEntry.ChrgesTaxPerce = 0;
                    dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);
                    #endregion

                    //TVoucherEntryCompany
                    #region TVoucherEntryCompany
                    //tVoucherEntryComp = new TVoucherEntryCompany();
                    //tVoucherEntryComp.PKVoucherCompanyNo = MainPKVoucherCompanyNo;
                    //tVoucherEntryComp.VoucherTypeCode = tVoucherEntry.VoucherTypeCode;
                    //tVoucherEntryComp.VoucherUserNo = 1;// Convert.ToInt64(txtInvCompNo.Text);
                    //tVoucherEntryComp.BilledAmount = 0;
                    //tVoucherEntryComp.CompanyNo = tVoucherEntry.CompanyNo;
                    //tVoucherEntryComp.MfgCompNo = MainMfgCompNo;
                    //tVoucherEntryComp.PayTypeNo = tVoucherEntry.PayTypeNo;
                    //tVoucherEntryComp.UserID = tVoucherEntry.UserID;
                    //tVoucherEntryComp.UserDate = tVoucherEntry.UserDate;
                    //dbTVoucherEntry.AddTVoucherEntryCompany(tVoucherEntryComp);
                    #endregion

                    for (int i = 0; i < GridLedger.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(GridLedger.Rows[i].Cells[6].FormattedValue) == true)
                        {
                            cnt = +1;
                            //mLedger = new MLedger();

                            //TVoucherDetails
                            #region TVoucherDetails
                            tVoucherDetails = new TVoucherDetails();
                            tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(GridLedger.Rows[i].Cells[8].Value);
                            tVoucherDetails.VoucherSrNo = 0;
                            if (Convert.ToDouble(GridLedger.Rows[i].Cells[3].Value) > 0)
                                tVoucherDetails.SignCode = GroupTypeCode == 2 ? 2 : 1;
                            else
                                tVoucherDetails.SignCode = GroupTypeCode == 2 ? 1 : 2;
                            tVoucherDetails.LedgerNo = Convert.ToInt64(GridLedger.Rows[i].Cells[5].Value);

                            if (tVoucherDetails.SignCode == 1)
                            {
                                tVoucherDetails.Debit = Math.Abs(Convert.ToDouble(GridLedger.Rows[i].Cells[3].Value));
                                tVoucherDetails.Credit = 0;
                            }
                            else
                            {
                                tVoucherDetails.Debit = 0;
                                tVoucherDetails.Credit = Math.Abs(Convert.ToDouble(GridLedger.Rows[i].Cells[3].Value));
                            }
                            tVoucherDetails.Narration = "";
                            tVoucherDetails.CompanyNo = DBGetVal.FirmNo;
                            tVoucherDetails.SrNo = 0;// Others.Party;
                            dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                            #endregion

                            ////TVoucherDetailsCompany
                            //#region TVoucherDetailsCompany
                            //tVoucherDetailsComp = new TVoucherDetailsCompany();
                            //tVoucherDetailsComp.PkVoucherCompTrnNo = Convert.ToInt64(GridLedger.Rows[i].Cells[9].Value);
                            //tVoucherDetailsComp.VoucherSrNo = tVoucherDetails.VoucherSrNo;
                            //tVoucherDetailsComp.SignCode = tVoucherDetails.SignCode;
                            //tVoucherDetailsComp.LedgerNo = tVoucherDetails.LedgerNo;
                            //tVoucherDetailsComp.Debit = tVoucherDetails.Debit;
                            //tVoucherDetailsComp.Credit = tVoucherDetails.Credit;
                            //tVoucherDetailsComp.SrNo = tVoucherDetails.SrNo;
                            //tVoucherDetailsComp.CompanyNo = tVoucherDetails.CompanyNo;
                            //tVoucherDetailsComp.Narration = tVoucherDetails.Narration;
                            //tVoucherDetailsComp.MfgCompNo = MainMfgCompNo;
                            //dbTVoucherEntry.AddTVoucherDetailsCompany(tVoucherDetailsComp);
                            //#endregion
                        }
                    }
                    if (cnt != 0)
                    {
                        //dbTVoucherEntry.UpdateQuery("Update    TVoucherDetails SET   TVoucherDetails.Debit = totDebit, " +
                        //            " TVoucherDetails.Credit = totCredit " +
                        //            " FROM         TVoucherDetails INNER JOIN " +
                        //            " (SELECT TVoucherDetailsCompany.FKVoucherTrnNo, SUM(TVoucherDetailsCompany.Debit) as totDebit, " +
                        //            " SUM(TVoucherDetailsCompany.Credit) as totCredit " +
                        //            "	FROM TVoucherDetailsCompany INNER JOIN TVoucherDetails " +
                        //            " ON TVoucherDetails.PkVoucherTrnNo = TVoucherDetailsCompany.FKVoucherTrnNo " +
                        //            " WHERE (TVoucherDetails.FkVoucherNo = @FkVoucherNo) " +
                        //            " GROUP BY TVoucherDetailsCompany.FKVoucherTrnNo " +
                        //            " ) As Tmp ON TVoucherDetails.PkVoucherTrnNo = Tmp.FKVoucherTrnNo ");

                        //if (dbmledger.ExecuteNonQueryStatements() == true)
                        if (dbTVoucherEntry.ExecuteNonQueryStatements() > 0)
                        {

                            DisplayMessage("Opening Balance Saved Successfully ...");

                            // setVoucherNos();

                            BindGrid();
                            txtBoxSearch.Text = "";
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("Please select atleast one ledger...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();
                        if (rdLedgerwise.Checked == true)
                            txtBoxSearch.Focus();
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

        #endregion

        private void GridLedger_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (GridLedger.CurrentCell.ColumnIndex == 3)
            {
                TextBox txtOP = (TextBox)e.Control;
                txtOP.TextChanged += new EventHandler(txtOP_TextChanged);
            }
        }

        public void txtOP_TextChanged(object sender, EventArgs e)
        {
            if (GridLedger.CurrentCell.ColumnIndex == 3)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.PositiveNegative);
            }
        }
    }
}
