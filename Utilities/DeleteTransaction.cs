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
    public partial class DeleteTransaction : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dt = new DataTable();

        DBTVaucherEntry dbTVaucherEntry = new DBTVaucherEntry();

        string strPkPayTypeNo = "", strQuery = "", strPkLedgerNo = "";
        int iShowedFrom = 0;
        private long VoucherTypeCode;
        private long MainMfgCompNo = 0;

        private bool isFirmwise = false;

        public DeleteTransaction(long VchCode)
        {

            if (VchCode % 100 == 15)
            {
                this.VoucherTypeCode = 15;
            }
            else if (VchCode % 100 == 9)
            {
                this.VoucherTypeCode = 9;
            }

            VchCode = VchCode - this.VoucherTypeCode;

            if (VchCode == 1000)
            {
                isFirmwise = false;
            }
            else if (VchCode == 2000)
            {
                isFirmwise = true;
            }

            InitializeComponent();
        }

        private void DeleteTransaction_Load(object sender, EventArgs e)
        {
            if (isFirmwise)
            {
                Form NewFrm = new Vouchers.FirmSelection();
                ObjFunction.OpenForm(NewFrm);

                MainMfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;
                lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
            }
            else
            {
                lblFirmName.Visible = false;
            }

            KeyDownFormat(this.Controls);

            pnlSearch.Enabled = false;
            pnlRecords.Enabled = false;
        }

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
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAllPayType.Checked = !chkSelectAllPayType.Checked;
            }
            else if (e.KeyCode == Keys.F3)
            {
                chkSelectAllParty.Checked = !chkSelectAllParty.Checked;
            }
            else if (e.KeyCode == Keys.F4)
            {
                chkSelect.Checked = !chkSelect.Checked;
            }
        }
        #endregion

        private void BtnShowPaytype_Click(object sender, EventArgs e)
        {
            iShowedFrom = 2;
            FillPartyGridView();
            FillBillsDetails();
            dgBillDetails.Focus();
        }

        private void BtnShowParty_Click(object sender, EventArgs e)
        {
            iShowedFrom = 3;
            FillBillsDetails();
            dgBillDetails.Focus();
        }

        private void FillBillsDetails()
        {
            try
            {
                updatePayTypeFilterString();
                updatePartyFilterString();
                pnlDate.Enabled = false;
                pnlSearch.Enabled = false;
                pnlRecords.Enabled = true;

                while (dgBillDetails.Rows.Count > 0)
                    dgBillDetails.Rows.RemoveAt(0);

                if (isFirmwise)
                {
                    strQuery = " Select VoucherDate,TVoucherEntryCompany.VoucherUserNo,LedgerName,TVoucherEntryCompany.BilledAmount,PayTypeName " +
                               ", (Case When (MPayType.PKPayTypeNo=3 OR MPayType.PKPayTypeNo=1) Then (Select IsNull(Sum(Amount),0) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=TVoucherEntry.PkVoucherNo) And TypeOfRef=2 ) Else -1 End) As OutStand " +
                               ",PkVoucherNo,TVoucherDetails.LedgerNo,'false' as chk, TVoucherEntry.PayTypeNo" +
                               " From TVoucherEntry " +
                               " Inner join TVoucherDetails on TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                               " Inner join TVoucherEntryCompany on TVoucherEntryCompany.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                               " Inner join Mledger on  TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                               " Inner join MpayType on MpayType.PkpayTypeNo = TVoucherEntry.PayTypeNo " +
                               " Where TVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "'" +
                               " And TVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTPToDate.Value).ToString("dd-MMM-yyyy") + "'" +
                               " and TVoucherEntry.VoucherTypeCode=" + VoucherTypeCode + " and TVoucherDetails.VoucherSrNo=1 " +
                               " AND TVoucherEntryCompany.MfgCompNo = " + MainMfgCompNo + " ";
                }
                else
                {
                    strQuery = " Select VoucherDate,VoucherUserNo,LedgerName,BilledAmount,PayTypeName " +
                               ", (Case When (MPayType.PKPayTypeNo=3 OR MPayType.PKPayTypeNo=1) Then (Select IsNull(Sum(Amount),0) From TVoucherRefDetails Where RefNo in ( Select TR.RefNo From TVoucherRefDetails TR,TVoucherDetails TD Where TD.PKVoucherTrnNo=TR.FKVoucherTrnNo AND TD.FkVoucherNo=TVoucherEntry.PkVoucherNo) And TypeOfRef=2 ) Else -1 End) As OutStand " +
                               ",PkVoucherNo,TVoucherDetails.LedgerNo,'false' as chk, TVoucherEntry.PayTypeNo" +
                               " From TVoucherEntry " +
                               " Inner join TVoucherDetails on  TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                               " Inner join Mledger on  TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                               " Inner join MpayType on MpayType.PkpayTypeNo = TVoucherEntry.PayTypeNo " +
                               " Where VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "'" +
                               " And VoucherDate <='" + Convert.ToDateTime(DTPToDate.Value).ToString("dd-MMM-yyyy") + "'" +
                               " and VoucherTypeCode=" + VoucherTypeCode + " and TVoucherDetails.VoucherSrNo=1";
                }
                if (strPkPayTypeNo != "")
                {
                    strQuery += " and PayTypeNo in(" + strPkPayTypeNo + ")";
                }
                if (strPkLedgerNo != "")
                {
                    strQuery += " and TVoucherDetails.LedgerNo in(" + strPkLedgerNo + ")";
                }
                strQuery += " Order By VoucherDate,LedgerName,VoucherUserNo";

                dt = ObjFunction.GetDataView(strQuery).Table;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgBillDetails.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgBillDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                    }

                    double BillAmt = 0, OutStand = 0;
                    BillAmt = Convert.ToDouble(dgBillDetails[ColIndex.BilledAmount, i].Value);
                    OutStand = Convert.ToDouble(dgBillDetails[ColIndex.OutStand, i].Value);
                    if (BillAmt == OutStand) 
                    {
                        dgBillDetails[ColIndex.BilledAmount, i].Style.BackColor = Color.YellowGreen;
                        dgBillDetails[ColIndex.BilledAmount, i].ToolTipText = "Bal Amount : NILL";
                    }
                    else if (OutStand == 0)
                    {
                        dgBillDetails[ColIndex.BilledAmount, i].Style.BackColor = Color.OrangeRed;
                        dgBillDetails[ColIndex.BilledAmount, i].ToolTipText = "Bal Amount : " + BillAmt + "     Received : NILL";
                    }
                    else if (OutStand == -1)
                    {
                        dgBillDetails[ColIndex.BilledAmount, i].Style.BackColor = Color.YellowGreen;
                        dgBillDetails[ColIndex.BilledAmount, i].ToolTipText = "Bal Amount : NILL";
                    }
                    else if (OutStand != BillAmt)
                    {
                        dgBillDetails[ColIndex.BilledAmount, i].Style.BackColor = Color.Orange;
                        dgBillDetails[ColIndex.BilledAmount, i].ToolTipText = "Bal Amount :" + (BillAmt - OutStand) + "     Received : " + OutStand;
                    }
                }
            }
            catch (Exception e)
            {
                OMMessageBox.Show("Error : " + e.Message);
            }

        }

        public static class ColIndex
        {

            public static int VoucherDate = 0;
            public static int BillNo = 1;
            public static int LedgerName = 2;
            public static int BilledAmount = 3;
            public static int PayTypeName = 4;
            public static int OutStand = 5;
            public static int PkVoucherNo = 6;
            public static int PLedgerNo = 7;
            public static int ChkSelect = 8;
            public static int PayTypeNo = 9;

        }

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(1200);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillPayTypeGridView()
        {
            try
            {
                pnlSearch.Enabled = true;
                pnlRecords.Enabled = false;
                strPkPayTypeNo = "";

                if (isFirmwise)
                {
                    strQuery = "Select PkPayTypeNo,PayTypeName,'false' as Chk " +
                        " From MPayTYpe " +
                        " where PkPayTypeNo<>1 " +
                            " And PkPayTypeNo in " +
                                " (Select Distinct TvoucherEntry.PayTypeNo From TvoucherEntry INNER JOIN " +
                                " TvoucherEntryCompany ON TvoucherEntryCompany.FkVoucherNo = TvoucherEntry.PkVoucherNo " +
                                " Where TvoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' AND " +
                                " TvoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTPToDate.Value).ToString("dd-MMM-yyyy") + "' AND " +
                                " TvoucherEntry.VoucherTypeCode=" + VoucherTypeCode + " " +
                                " AND TvoucherEntryCompany.MfgCompNo = " + MainMfgCompNo + ")" +
                                "  " + 
                        " Order By PayTypeName";
                }
                else
                {
                    strQuery = "Select PkPayTypeNo,PayTypeName,'false' as Chk " +
                        " From MPayTYpe " +
                        " where PkPayTypeNo<>1 " +
                            " And PkPayTypeNo in " +
                                " (Select Distinct PayTypeNo From TvoucherEntry " +
                                " Where VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "' AND " +
                                " VoucherDate <='" + Convert.ToDateTime(DTPToDate.Value).ToString("dd-MMM-yyyy") + "' AND " +
                                " VoucherTypeCode=" + VoucherTypeCode + " )" +
                        " Order By PayTypeName";
                }
                dt = ObjFunction.GetDataView(strQuery).Table;


                while (gvPayType.Rows.Count > 0)
                    gvPayType.Rows.RemoveAt(0);
                gvPayType.DataSource = dt.DefaultView;
            }
            catch (Exception e)
            {
                OMMessageBox.Show("Error : " + e.Message);
            }
        }

        private void updatePayTypeFilterString()
        {
            strPkPayTypeNo = "";
            for (int i = 0; i < gvPayType.Rows.Count; i++)
            {
                if (Convert.ToBoolean(gvPayType.Rows[i].Cells[2].EditedFormattedValue.ToString()) == true)
                {
                    if (strPkPayTypeNo == "")
                        strPkPayTypeNo = gvPayType.Rows[i].Cells[0].Value.ToString();
                    else
                        strPkPayTypeNo = strPkPayTypeNo + "," + gvPayType.Rows[i].Cells[0].Value.ToString();
                }
            }
        }

        private void FillPartyGridView()
        {
            try
            {
                updatePayTypeFilterString();
                pnlSearch.Enabled = true;
                pnlRecords.Enabled = false;

                strPkLedgerNo = "";

                if (isFirmwise)
                {
                    strQuery = " Select Distinct TVoucherDetails.LedgerNo, LedgerName, 'false' As Chk " +
                                              " From TVoucherEntry " +
                                              " Inner join TVoucherEntryCompany on  TVoucherEntryCompany.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                                              " Inner join TVoucherDetails on  TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                                              " Inner join Mledger on  TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                                              " Where TVoucherEntry.VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "'" +
                                              " And TVoucherEntry.VoucherDate <='" + Convert.ToDateTime(DTPToDate.Value).ToString("dd-MMM-yyyy") + "'" +
                                              " and TVoucherEntry.VoucherTypeCode=" + VoucherTypeCode + " and TVoucherDetails.VoucherSrNo=1 " +
                                              " And TVoucherEntryCompany.MfgCompNo = " + MainMfgCompNo + " ";
                }
                else
                {
                    strQuery = " Select Distinct TVoucherDetails.LedgerNo,LedgerName, 'false' As Chk " +
                                              " From TVoucherEntry " +
                                              " Inner join TVoucherDetails on  TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                                              " Inner join Mledger on  TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                                              " Where VoucherDate >= '" + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "'" +
                                              " And VoucherDate <='" + Convert.ToDateTime(DTPToDate.Value).ToString("dd-MMM-yyyy") + "'" +
                                              " and VoucherTypeCode=" + VoucherTypeCode + " and TVoucherDetails.VoucherSrNo=1";
                }

                if (strPkPayTypeNo != "")
                {
                    strQuery += " and TVoucherEntry.PayTypeNo in(" + strPkPayTypeNo + ")";
                }

                strQuery += " Order by LedgerName";
                dt = ObjFunction.GetDataView(strQuery).Table;

                while (dgParty.Rows.Count > 0)
                    dgParty.Rows.RemoveAt(0);
                dgParty.DataSource = dt.DefaultView;

            }
            catch (Exception e)
            {
                OMMessageBox.Show("Error : " + e.Message);
            }
        }

        private void updatePartyFilterString()
        {
            strPkLedgerNo = "";
            for (int i = 0; i < dgParty.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgParty.Rows[i].Cells[2].EditedFormattedValue.ToString()) == true)
                {
                    if (strPkLedgerNo == "")
                        strPkLedgerNo = dgParty.Rows[i].Cells[0].Value.ToString();
                    else
                        strPkLedgerNo = strPkLedgerNo + "," + dgParty.Rows[i].Cells[0].Value.ToString();
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSelectAllPayType_CheckedChanged_1(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPayType.Rows.Count; i++)
            {
                gvPayType.Rows[i].Cells[2].Value = chkSelectAllPayType.Checked;
            }
        }

        private void chkSelectAllParty_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgParty.Rows.Count; i++)
            {
                dgParty.Rows[i].Cells[2].Value = chkSelectAllParty.Checked;
            }
        }

        private void dgBillDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.VoucherDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool flag, flg = false;
            for (int i = 0; i < dgBillDetails.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgBillDetails.Rows[i].Cells[ColIndex.ChkSelect].FormattedValue) == true)
                {
                    flg = true;
                    break;
                }
            }
            if (flg == false)
            {
                DisplayMessage("No records selected !!!");
                return;
            }

            if (OMMessageBox.Show("Are you sure want to delete the record(s)?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                flag = false;
                dbTVaucherEntry = new DBTVaucherEntry();

                for (int i = 0; i < dgBillDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgBillDetails[ColIndex.ChkSelect, i].Value) == true)
                    {
                        dbTVaucherEntry.DeleteSalesBillWithReceipt(
                            Convert.ToInt64(dgBillDetails[ColIndex.PkVoucherNo, i].Value),
                            DBGetVal.FirmNo,
                            Convert.ToDateTime(dgBillDetails[ColIndex.VoucherDate, i].Value), 
                            Convert.ToInt64(dgBillDetails[ColIndex.PLedgerNo, i].Value),
                            Convert.ToInt32(dgBillDetails[ColIndex.PayTypeNo, i].Value));
                        flag = true;
                    }
                }

                if (flag)
                {
                    ObjTrans.Execute("Exec StockUpdateAll", CommonFunctions.ConStr);
                    DisplayMessage("Delete Transaction Successfully...");
                    FillBillsDetails();
                }
            }
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgBillDetails.Rows.Count; i++)
            {
                dgBillDetails.Rows[i].Cells[ColIndex.ChkSelect].Value = chkSelect.Checked;
            }

        }

        private void btnNext1_Click(object sender, EventArgs e)
        {
            pnlDate.Enabled = false;
            FillPayTypeGridView();
            FillPartyGridView();
            gvPayType.Focus();
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            FillPartyGridView();
            dgParty.Focus();
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            pnlDate.Enabled = true;
            DTPFromDate.Focus();
            pnlSearch.Enabled = false;
            pnlRecords.Enabled = false;
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            if (iShowedFrom == 1)
            {
                pnlDate.Enabled = true;
                pnlSearch.Enabled = false;
                gvPayType.Focus();
            }
            else if (iShowedFrom == 2)
            {
                pnlDate.Enabled = false;
                pnlSearch.Enabled = true;
                gvPayType.Focus();
            }
            else if (iShowedFrom == 3)
            {
                pnlDate.Enabled = false;
                pnlSearch.Enabled = true;
                dgParty.Focus();
            }
            pnlRecords.Enabled = false;
            
        }

        private void BtnShowDate_Click(object sender, EventArgs e)
        {
            iShowedFrom = 1;
            FillBillsDetails();
            dgBillDetails.Focus();
        }
    }
}
