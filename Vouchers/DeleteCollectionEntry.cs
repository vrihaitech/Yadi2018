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
    public partial class DeleteCollectionEntry : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();

        long VoucherType = 0, grpNo;
        bool isBinding_CollectionEntry_Grid = false;
        long SalesPkVoucherNo = 0, CollectionPkVoucherNo = 0;
        int iRowNo_Sales_Bill = 0, iRowNo_Collection = 0;

        public DeleteCollectionEntry()
        {
            InitializeComponent();
        }

        public DeleteCollectionEntry(long VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;





            if (VoucherType == VchType.Sales)
            {
                if (DBGetVal.KachhaFirm == false)
                {
                    this.VoucherType = VchType.Sales;
                }
                else
                {
                    this.VoucherType = VchType.DSales;
                }
                this.Text = "Delete Sales Collection Entry";
                grpNo = GroupType.SundryDebtors;
            }
            else if (VoucherType == VchType.Purchase)
            {
                if (DBGetVal.KachhaFirm == false)
                {
                    this.VoucherType = VchType.Purchase;
                   
                }
                else
                {
                    this.VoucherType = VchType.DPurchase;
                }
                this.Text = "Delete Purchase Payment Entry";
                grpNo = GroupType.SundryCreditors;
            }
        }

        private void DeleteCollectionEntry_Load(object sender, EventArgs e)
        {
 //           string sql = " SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName " +
   //                       " FROM MLedger INNER JOIN TVoucherDetails ON MLedger.LedgerNo = TVoucherDetails.LedgerNo INNER JOIN TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
     //                     " WHERE (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") And MLedger.GroupNo =" + grpNo + " order by MLedger.LedgerName ";
              string sql = " SELECT DISTINCT MLedger.LedgerNo, MLedger.LedgerName " +
                          " FROM MLedger  WHERE  MLedger.GroupNo =" + grpNo + " order by MLedger.LedgerName ";

            ObjFunction.FillCombo(cmbParty, sql);
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            dtpToDate.MinDate = dtpFromDate.Value;
            InitControls();
            KeyDownFormat(this.Controls);
            rb_CheckedChanged(sender, new EventArgs());
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                rbDeleteCollection.Checked = true;
                rb_CheckedChanged(rbDeleteCollection, new EventArgs());
            }
            else if (e.KeyCode == Keys.F10)
            {
                rbBillwise.Checked = true;
                rb_CheckedChanged(rbBillwise, new EventArgs());
            }
            else if (e.KeyCode == Keys.F11)
            {
                rbCollectionwise.Checked = true;
                rb_CheckedChanged(rbCollectionwise, new EventArgs());
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (rbBillwise.Checked == true)
                {
                    chkSelectAll.Checked = !chkSelectAll.Checked;
                    for (int i = 0; i < dgDeleteCollection.Rows.Count; i++)
                    {
                        dgDeleteCollection.Rows[i].Cells[DeleteCollectionColIndex.chk].Value = chkSelectAll.Checked;
                    }
                    BtnDelete.Focus();
                }
                else if (pnlCollection.Visible == true)
                {
                    checkBox1.Checked = !checkBox1.Checked;
                    for (int i = 0; i < dgCollwiseBill.Rows.Count; i++)
                    {
                        dgCollwiseBill.Rows[i].Cells[CollBillIndex.Chk].Value = checkBox1.Checked;
                    }
                }
                else if (pnlDeleteCollection.Visible == true)
                {
                    checkBox3.Checked = !checkBox3.Checked;
                    for (int i = 0; i < dgDeleteCollectionDtls.Rows.Count; i++)
                    {
                        dgDeleteCollectionDtls.Rows[i].Cells[CollBillIndex.Chk].Value = checkBox3.Checked;
                    }
                }
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

        #region Main Methods
        public void InitControls()
        {
            pnlBillWise.Location = new Point(13, 121);
            pnlCollection.Location = new Point(13, 121);
            pnlDeleteCollection.Location = new Point(13, 121);
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            dtpToDate.MinDate = dtpFromDate.Value;
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            //  BindRecords();
        }

        private void dtpFromDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                dtpToDate.Focus();
            }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                cmbParty.Focus();
            }
        }

        private void cmbParty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (GridView.Rows.Count > 0)
                    GridView.Rows.Clear();
                while (dgDataRecord.Rows.Count > 0)
                    dgDataRecord.Rows.RemoveAt(0);
                btnShow.Focus();
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now.Date;
            dtpToDate.Value = DateTime.Now.Date;
            cmbParty.SelectedValue = 0;
            rbOutStanding.Checked = true;

            while (GridView.Rows.Count > 0)
                GridView.Rows.RemoveAt(0);
            dtpFromDate.Focus();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (rbBillwise.Checked == true)
                BindGrid();
            else if (rbCollectionwise.Checked == true)
                BindRecordsCollectionwise();
            else if (rbDeleteCollection.Checked == true)
                BindRecordsDeleteCollectionwise();
        }
        #endregion

        #region Billwise Collection
        private void BindGrid()
        {
            try
            {
                if (cmbParty.SelectedIndex != 0)
                {
                    GridView.Rows.Clear();

                    string sql = " Select BillNo,VoucherDate,LedgerName,BilledAmount ,((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) as TotRec,((Amount-((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) )) as NetBal,LedgerNo,RfNo,'false' As Chk, PkVoucherNo " +
                               " From (SELECT TVoucherRefDetails.RefNo as RfNo, TVoucherEntry.VoucherDate,(Select L.LedgerName From MLedger L Where L.LedgerNo=TVoucherDetails.LedgerNo)As LedgerName, case when " + VoucherType + "=9 then TVoucherEntry.Reference else cast(TVoucherEntry.VoucherUserNo as varchar) end as BillNo, TVoucherEntry.BilledAmount ,TVoucherRefDetails.Amount, TVoucherDetails.LedgerNo, TVoucherEntry.PkVoucherNo FROM  TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo  " +
                               " WHERE   (TVoucherEntry.VoucherTypeCode = " + VoucherType + ") AND (TVoucherRefDetails.TypeOfRef = 3) AND  (TVoucherEntry.CompanyNo =" + DBGetVal.FirmNo + ") and (TVoucherEntry.IsCancel='false') And	(TVoucherEntry.VoucherDate >= '" + dtpFromDate.Text + "') And (TVoucherEntry.VoucherDate <= '" + dtpToDate.Text + "')     ) as tbl1  " +
                               " Where    (LedgerNo = Case When (" + ObjFunction.GetComboValue(cmbParty) + "<>-1) Then " + ObjFunction.GetComboValue(cmbParty) + " Else LedgerNo End) " +
                               " And ((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5)))<>0 ";

                    if (rbOutStanding.Checked == true)
                        sql = sql + " and  Amount>((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) ";
                    else if (rbwithoutOustSanding.Checked == true)
                        sql = sql + " and  Amount=((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) ";
                    sql = sql + " Order By VoucherDate,BillNo ";

                    DataTable dt = ObjFunction.GetDataView(sql).Table;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        GridView.Rows.Add();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                            if (Convert.ToDouble(GridView.Rows[i].Cells[ColIndex.TotRec].Value) == 0)
                                GridView.Rows[i].Cells[ColIndex.Chk].ReadOnly = true;
                            else
                                GridView.Rows[i].Cells[ColIndex.Chk].ReadOnly = false;
                        }
                    }
                    if (ObjFunction.GetComboValue(cmbParty) != -1)
                        GridView.Columns[ColIndex.LedgerName].Visible = false;
                    else
                        GridView.Columns[ColIndex.LedgerName].Visible = true;

                    GridView.Columns[ColIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    GridView.Columns[ColIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (GridView.Rows.Count > 0)
                    {
                        GridView.CurrentCell = GridView[ColIndex.BillNo, 0];
                        GridView.Focus();
                    }
                    else
                        btnShow.Focus();
                }
                else
                    GridView.Rows.Clear();

                while (dgDataRecord.Rows.Count > 0)
                    dgDataRecord.Rows.RemoveAt(0);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                e.SuppressKeyPress = true;
                BindRecords();
                chkSelectAll.Checked = false;
                dgDataRecord.Focus();
            }
        }

        private void BindRecords()
        {
            try
            {
                while (dgDataRecord.Rows.Count > 0)
                    dgDataRecord.Rows.RemoveAt(0);
                //pnlBillWise.Visible = false;
                DataTable dt = new DataTable();
                if (GridView.Rows.Count > 0)
                {
                    if (VoucherType == VchType.Sales)
                    {
                        dt = ObjFunction.GetDataView("SELECT TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt,  Case When(TVoucherEntry.VoucherTypeCode<>" + VchType.RejectionIn + ")Then MPayType.PayTypeName Else 'Against Sales Return' End AS PayTypeName , TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate, " +
                              " TVoucherChqCreditDetails.CreditCardNo,TVoucherEntry.PKVoucherNo,TVoucherRefDetails.PkRefTrnNo,TVoucherChqCreditDetails.PkSrNo,'false' as  Chk " +
                              " FROM         TVoucherEntry INNER JOIN " +
                              " TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                              " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo LEFT OUTER JOIN " +
                              " TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo INNER JOIN " +
                              " MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherDetails.LedgerNo = " + GridView.CurrentRow.Cells[ColIndex.LedgerNo].Value + ") AND (TVoucherEntry.VoucherTypeCode in( " + VchType.SalesReceipt + "," + VchType.RejectionIn + "))" +
                        "AND (TVoucherRefDetails.RefNo = " + GridView.CurrentRow.Cells[ColIndex.RefNo].Value + ") AND " +
                        "(TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")  " +//ORDER BY TVoucherEntry.VoucherDate
                        " UNION " +
                        " SELECT 0 AS VoucherUserNo,TVoucherRefDetails.UserDate, TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt,  'Against Op Balance' , '', '',  '',0,0,0,'false' as  Chk " +
                        " FROM TVoucherRefDetails   WHERE (TVoucherRefDetails.LedgerNo = " + GridView.CurrentRow.Cells[ColIndex.LedgerNo].Value + ") AND TVoucherRefDetails.TypeOfRef=5 AND (TVoucherRefDetails.RefNo = " + GridView.CurrentRow.Cells[ColIndex.RefNo].Value + ") " +
                        " AND (TVoucherRefDetails.CompanyNo = " + DBGetVal.FirmNo + ")  ORDER BY TVoucherRefDetails.PkRefTrnNo").Table;
                    }
                    else if (VoucherType == VchType.Purchase)
                    {
                        dt = ObjFunction.GetDataView("SELECT TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt,  Case When(TVoucherEntry.VoucherTypeCode<>" + VchType.RejectionOut + ")Then MPayType.PayTypeName Else 'Against Purchase Return' End AS PayTypeName , TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate, " +
                              " TVoucherChqCreditDetails.CreditCardNo,TVoucherEntry.PKVoucherNo,TVoucherRefDetails.PkRefTrnNo,TVoucherChqCreditDetails.PkSrNo,'false' as  Chk " +
                              " FROM         TVoucherEntry INNER JOIN " +
                              " TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                              " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo LEFT OUTER JOIN " +
                              " TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo INNER JOIN " +
                              " MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherDetails.LedgerNo = " + GridView.CurrentRow.Cells[ColIndex.LedgerNo].Value + ") AND (TVoucherEntry.VoucherTypeCode in (" + VchType.PurchasePayment + "," + VchType.RejectionOut + "))" +
                        "AND (TVoucherRefDetails.RefNo = " + GridView.CurrentRow.Cells[ColIndex.RefNo].Value + ") AND " +
                        "(TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") " +//ORDER BY TVoucherEntry.VoucherDate
                        " UNION " +
                        " SELECT 0 AS VoucherUserNo,TVoucherRefDetails.UserDate, TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt,  'Against Op Balance' , '', '',  '',0,0,0,'false' as  Chk " +
                        " FROM TVoucherRefDetails   WHERE (TVoucherRefDetails.LedgerNo = " + GridView.CurrentRow.Cells[ColIndex.LedgerNo].Value + ") AND TVoucherRefDetails.TypeOfRef=5 AND (TVoucherRefDetails.RefNo = " + GridView.CurrentRow.Cells[ColIndex.RefNo].Value + ") " +
                        " AND (TVoucherRefDetails.CompanyNo = " + DBGetVal.FirmNo + ")  ORDER BY TVoucherRefDetails.PkRefTrnNo").Table;
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    if (VoucherType == VchType.Sales)
                        DisplayMessage("No Collection Entry Found");
                    else
                        DisplayMessage("No Payments Entry Found");
                }
                else
                {
                    SalesPkVoucherNo = Convert.ToInt64(GridView.CurrentRow.Cells[ColIndex.BillPkVoucherNo].Value.ToString());
                    iRowNo_Sales_Bill = GridView.CurrentCell.RowIndex;
                    pnlBillWise.Visible = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgDataRecord.Rows.Add();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dgDataRecord.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                    }

                    // pnlBillWise.Location = new System.Drawing.Point(50, 110);
                    dgDataRecord.Columns[BillwiseColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDataRecord.Columns[BillwiseColIndex.DiscAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgDataRecord.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataRecord_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == BillwiseColIndex.CollDate || e.ColumnIndex == BillwiseColIndex.ChequeDate)
            {
                if (Convert.IsDBNull(e.Value) == false)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
                    if (e.Value.ToString() == "01-Jan-19")
                        e.Value = "";
                }
            }
        }

        private void DataRecord_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    if (pnlBillWise.Visible == true)
                    {
                        pnlBillWise.Visible = false;
                        GridView.CurrentCell = GridView[ColIndex.Chk, GridView.CurrentRow.Index];
                        GridView.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    if (dgDataRecord.CurrentRow.Index < 0)
                        return;
                    if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dbTVoucherEntry = new DBTVaucherEntry();
                        tVoucherEntry = new TVoucherEntry();
                        tVoucherEntry.PkVoucherNo = Convert.ToInt64(dgDataRecord.CurrentRow.Cells[BillwiseColIndex.PKVoucherNo].Value);
                        dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);
                        OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                        if (pnlBillWise.Visible == true)
                        {
                            pnlBillWise.Visible = false;
                            GridView.CurrentCell = GridView[ColIndex.Chk, GridView.CurrentRow.Index];
                            GridView.Focus();
                        }
                    }
                }



            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            Boolean flag = false;

            for (int i = 0; i < dgDataRecord.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgDataRecord.Rows[i].Cells[BillwiseColIndex.Chk].EditedFormattedValue))
                {
                    flag = true; break;
                }
            }
            if (flag)
            {
                if (OMMessageBox.Show("Are you sure want to delete the Adjustment record ?" + "\r\n\r\n\r\n" +
                    "Note : Collection entry will now treated as advance."
                    , CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbTVoucherEntry = new DBTVaucherEntry();
                    for (int i = 0; i < dgDataRecord.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgDataRecord.Rows[i].Cells[BillwiseColIndex.Chk].EditedFormattedValue))
                        {
                            #region Check for the JV of Interest exist. If found take action on data
                            DataTable dtJV = ObjFunction.GetDataView("Select FkJournalVoucherNo, PkSrNo, JVAmount From TVoucherJournalEntry " +
                                " Where FKReceiptVoucherNo = " + Convert.ToInt64(dgDataRecord.Rows[i].Cells[BillwiseColIndex.PKVoucherNo].Value) + " AND " +
                                " FKVoucherNo = " + SalesPkVoucherNo).Table;

                            for (int j = 0; j < dtJV.Rows.Count; j++)
                            {
                                DialogResult dr = OMMessageBox.Show("Interst Entry of Rs. : " + dtJV.Rows[j][2].ToString() + " found." + "\r\n\r\n\r\n" +
                                    "Do you want to delete Interest entry ?", "Interest Entry found ..."
                                    , OMMessageBoxButton.YesNo
                                    , OMMessageBoxIcon.Question
                                    , OMMessageBoxDefaultButton.Button2);

                                if (dr == DialogResult.Yes)
                                {
                                    #region Delete JV's Agst Ref in Collection And JV's Voucher then reduce Collection amount by JV's Amount

                                    string strQuery = "SELECT TVRD_C.PkRefTrnNo " +
                                            " FROM         TVoucherDetails AS TVD_B INNER JOIN " +
                                            " TVoucherRefDetails AS TVRD_B ON TVD_B.PkVoucherTrnNo = TVRD_B.FkVoucherTrnNo INNER JOIN " +
                                            " TVoucherRefDetails AS TVRD_C ON TVRD_B.RefNo = TVRD_C.RefNo AND TVRD_B.PkRefTrnNo <> TVRD_C.PkRefTrnNo " +
                                            " WHERE     (TVD_B.FkVoucherNo in (" + dtJV.Rows[j][0].ToString() + ")) ";

                                    TVoucherRefDetails tvref = new TVoucherRefDetails();
                                    tvref.PkRefTrnNo = ObjQry.ReturnLong(strQuery, CommonFunctions.ConStr);
                                    dbTVoucherEntry.DeleteTVoucherRefDetails(tvref);

                                    tVoucherEntry = new TVoucherEntry();
                                    tVoucherEntry.PkVoucherNo = Convert.ToInt64(dtJV.Rows[j][0].ToString());
                                    dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);

                                    TVoucherJournalEntry tvje = new TVoucherJournalEntry();
                                    tvje.PKSrNo = Convert.ToInt64(dtJV.Rows[j][1].ToString());
                                    dbTVoucherEntry.DeleteTVoucherJournalEntry(tvje);

                                    if (VoucherType == VchType.Sales)
                                        dbTVoucherEntry.UpdateVoucherRefDetails_Interest(Convert.ToInt64(dgDataRecord.Rows[i].Cells[BillwiseColIndex.PKVoucherNo].Value), Convert.ToDouble(dtJV.Rows[j][2].ToString()), 2);
                                    else
                                        dbTVoucherEntry.UpdateVoucherRefDetails_Interest(Convert.ToInt64(dgDataRecord.Rows[i].Cells[BillwiseColIndex.PKVoucherNo].Value), Convert.ToDouble(dtJV.Rows[j][2].ToString()), 1);

                                    #endregion
                                }
                            }
                            #endregion

                            TVoucherRefDetails tVchRefDetails = new TVoucherRefDetails();
                            tVchRefDetails.PkRefTrnNo = Convert.ToInt64(dgDataRecord.Rows[i].Cells[BillwiseColIndex.PKRefTrnNo].Value);
                            dbTVoucherEntry.DeleteTVoucherRefDetails(tVchRefDetails);

                            if (Convert.ToDouble(dgDataRecord.Rows[i].Cells[BillwiseColIndex.DiscAmt].Value) > 0)
                            {
                                if (VoucherType == VchType.Sales)
                                    dbTVoucherEntry.UpdateVoucherRefDetails(Convert.ToInt64(dgDataRecord.Rows[i].Cells[BillwiseColIndex.PKVoucherNo].Value), Convert.ToDouble(dgDataRecord.Rows[i].Cells[BillwiseColIndex.DiscAmt].Value), 2);
                                else
                                    dbTVoucherEntry.UpdateVoucherRefDetails(Convert.ToInt64(dgDataRecord.Rows[i].Cells[BillwiseColIndex.PKVoucherNo].Value), Convert.ToDouble(dgDataRecord.Rows[i].Cells[BillwiseColIndex.DiscAmt].Value), 1);
                            }
                        }
                    }
                    dbTVoucherEntry.ExecuteNonQueryStatements();
                    //pnlBillWise.Visible = false;
                    OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindGrid();
                    //GridView.CurrentCell = GridView[ColIndex.Chk, GridView.CurrentRow.Index];
                    // GridView.Focus();
                }
            }
            else
            {
                DisplayMessage("Select Atleast one Bill");
                dgDataRecord.Focus();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            if (pnlCollection.Visible == true)
            {
                for (int i = 0; i < dgCollwiseBill.Rows.Count; i++)
                {
                    dgCollwiseBill.Rows[i].Cells[CollBillIndex.Chk].Value = ((CheckBox)sender).Checked;
                }
            }
            else if (pnlDeleteCollection.Visible == true)
            {
                for (int i = 0; i < dgDeleteCollection.Rows.Count; i++)
                {
                    dgDeleteCollection.Rows[i].Cells[CollBillIndex.Chk].Value = ((CheckBox)sender).Checked;
                }
            }
            else if (pnlBillWise.Visible == true)
            {
                for (int i = 0; i < dgDataRecord.Rows.Count; i++)
                {
                    dgDataRecord.Rows[i].Cells[BillwiseColIndex.Chk].Value = ((CheckBox)sender).Checked;
                }
            }

        }

        private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.BillDate)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");

            }
        }

        public static class BillwiseColIndex
        {
            public static int VoucherUserNo = 0;
            public static int CollDate = 1;
            public static int Amount = 2;
            public static int DiscAmt = 3;
            public static int PayTypeName = 4;
            public static int ChequeNo = 5;
            public static int ChequeDate = 6;
            public static int CreditCardNo = 7;
            public static int PKVoucherNo = 8;
            public static int PKRefTrnNo = 9;
            public static int PkSrNo = 10;
            public static int Chk = 11;
        }
        #endregion

        #region Collectionwise
        public static class CollectionwiseColIndex
        {
            public static int VoucherUserNo = 0;
            public static int VchDate = 1;
            public static int PayType = 2;
            public static int Amount = 3;
            public static int RecAmt = 4;
            public static int NetBal = 5;
            public static int PKVoucherTrnNo = 6;
            public static int VoucherSrNo = 7;
            public static int PkVoucherNoCollection = 8;
        }

        private void BindRecordsCollectionwise()
        {
            try
            {
                while (dgCollwiseCollection.Rows.Count > 0)
                    dgCollwiseCollection.Rows.RemoveAt(0);
                DataTable dt = new DataTable();

                if (VoucherType == VchType.Sales)
                {
                    dt = ObjFunction.GetDataView("Select VoucherUserNo,VoucherDate,PayTypeName,AdvAmount,TotBalAmt AS RecAmt,(AdvAmount-TotBalAmt)As NetBal,PKVoucherTrnNo,VoucherSrNo, PkVoucherNo " +
                        " From(SELECT     TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, MPayType.PayTypeName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount  , TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo," +
                        " (SELECT IsNull(SUM(Amount),0), TVoucherEntry.PkVoucherNo FROM  TVoucherRefDetails WHERE(FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)) AS TotBalAmt FROM TVoucherDetails INNER JOIN " +
                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.SalesReceipt + ") AND (TVoucherDetails.SrNo = 501) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") AND " +
                        " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND (TVoucherEntry.IsCancel = 'false') AND TVoucherEntry.VoucherDate>='" + dtpFromDate.Text + "' AND TVoucherEntry.VoucherDate<='" + dtpToDate.Text + "'" +
                        " GROUP BY TVoucherEntry.VoucherUserNo,TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MPayType.PayTypeName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo " +
                        ")TempTable").Table;

                }
                else if (VoucherType == VchType.Purchase)
                {
                    dt = ObjFunction.GetDataView("Select VoucherUserNo,VoucherDate,PayTypeName,AdvAmount,TotBalAmt AS RecAmt,(AdvAmount-TotBalAmt)As NetBal,PKVoucherTrnNo,VoucherSrNo " +
                        " From(SELECT     TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, MPayType.PayTypeName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount  , TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo," +
                        " (SELECT IsNull(SUM(Amount),0) FROM  TVoucherRefDetails WHERE(FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)) AS TotBalAmt FROM TVoucherDetails INNER JOIN " +
                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.PurchasePayment + ") AND (TVoucherDetails.SrNo = 501) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") AND " +
                        " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND (TVoucherEntry.IsCancel = 'false') AND TVoucherEntry.VoucherDate>='" + dtpFromDate.Text + "' AND TVoucherEntry.VoucherDate<='" + dtpToDate.Text + "'" +
                        " GROUP BY TVoucherEntry.VoucherUserNo,TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MPayType.PayTypeName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo " +
                        ")TempTable").Table;
                }

                if (dt.Rows.Count == 0)
                {
                    if (VoucherType == VchType.Sales)
                        DisplayMessage("No Collection Entry Found");
                    else
                        DisplayMessage("No Payments Entry Found");
                }
                else
                {

                    pnlCollection.Visible = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgCollwiseCollection.Rows.Add();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dgCollwiseCollection.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                    }
                    if (dgCollwiseCollection.Rows.Count > 0)
                    {
                        dgCollwiseCollection.CurrentCell = dgCollwiseCollection[CollectionwiseColIndex.VoucherUserNo, 0];
                        dgCollwiseCollection.Focus();
                    }
                    // pnlBillWise.Location = new System.Drawing.Point(50, 110);
                    dgCollwiseCollection.Columns[CollectionwiseColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgCollwiseCollection.Columns[CollectionwiseColIndex.RecAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgCollwiseCollection.Columns[CollectionwiseColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgDataRecord.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    while (dgCollwiseBill.Rows.Count > 0)
                        dgCollwiseBill.Rows.RemoveAt(0);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgCollwiseCollection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == CollectionwiseColIndex.VchDate)
            {
                if (Convert.IsDBNull(e.Value) == false)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
                    if (e.Value.ToString() == "01-Jan-19")
                        e.Value = "";
                }
            }
        }

        private void dgCollwiseCollection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                e.SuppressKeyPress = true;
                BindGridCollectionwise();
                dgCollwiseBill.Focus();
            }
        }

        private void BindGridCollectionwise()
        {
            try
            {

                dgCollwiseBill.Rows.Clear();
                string sql = "Select BillNo,VoucherDate,LedgerName,BilledAmount ,((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) as TotRec,DiscAmt,((Amount-((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) )) as NetBal,LedgerNo,RfNo,'false' As Chk ,PKRefTrnNo,PKVoucherNo " +
                    " From (SELECT TVoucherRefDetails.RefNo as RfNo, TVoucherEntry.VoucherDate,(Select L.LedgerName From MLedger L Where L.LedgerNo=TVoucherDetails.LedgerNo)As LedgerName, case when " + VchType.Sales + "=9 then TVoucherEntry.Reference else cast(TVoucherEntry.VoucherUserNo as varchar) end as BillNo, TVoucherEntry.BilledAmount ,TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt, TVoucherDetails.LedgerNo,TVoucherRefDetails_1.PKRefTrnNo, TVoucherEntry.PKVoucherNo  " +
                    " FROM  TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo  " +
                    " INNER JOIN TVoucherRefDetails AS TVoucherRefDetails_1 ON TVoucherRefDetails.RefNo=TVoucherRefDetails_1.RefNo WHERE   (TVoucherEntry.VoucherTypeCode = " + VchType.Sales + ") AND (TVoucherRefDetails.TypeOfRef = 3) AND  (TVoucherEntry.CompanyNo =" + DBGetVal.FirmNo + ") and (TVoucherEntry.IsCancel='false')  " +
                    " AND TVoucherRefDetails_1.TypeOfRef=2 AND TVoucherRefDetails_1.FKVoucherTrnNo=" + dgCollwiseCollection.CurrentRow.Cells[CollectionwiseColIndex.PKVoucherTrnNo].Value + "   ) as tbl1  Where    (LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") ";

                DataTable dt = ObjFunction.GetDataView(sql).Table;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgCollwiseBill.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgCollwiseBill.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                }


                dgCollwiseBill.Columns[CollBillIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCollwiseBill.Columns[CollBillIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCollwiseBill.Columns[CollBillIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgCollwiseBill.Columns[CollBillIndex.DiscAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (dgCollwiseBill.Rows.Count > 0)
                {
                    CollectionPkVoucherNo = Convert.ToInt64(dgCollwiseCollection.CurrentRow.Cells[CollectionwiseColIndex.PkVoucherNoCollection].Value);
                    iRowNo_Collection = dgCollwiseCollection.CurrentCell.RowIndex;
                    dgCollwiseBill.CurrentCell = dgCollwiseBill[CollBillIndex.Chk, 0];
                    dgCollwiseBill.Focus();
                }
                else
                    btnShow.Focus();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private static class CollBillIndex
        {
            public static int BillNo = 0;
            public static int BillDate = 1;
            public static int PayTypeName = 2;
            public static int BillAmt = 3;
            public static int TotRec = 4;
            public static int DiscAmt = 5;
            public static int NetBal = 6;
            public static int PKVoucherTrnNo = 7;
            public static int VoucherSrNo = 8;
            public static int Chk = 9;
            public static int PkRefTrnNo = 10;
            public static int BillPkVoucherNo = 11;
        }

        private void dgCollwiseBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == CollBillIndex.BillDate)
            {
                if (Convert.IsDBNull(e.Value) == false)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
                    if (e.Value.ToString() == "01-Jan-19")
                        e.Value = "";
                }
            }
        }

        private void btnDelCollwise_Click(object sender, EventArgs e)
        {
            Boolean flag = false;

            for (int i = 0; i < dgCollwiseBill.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgCollwiseBill.Rows[i].Cells[CollBillIndex.Chk].EditedFormattedValue))
                {
                    flag = true; break;
                }
            }
            if (flag)
            {
                if (OMMessageBox.Show("Are you sure want to delete the Adjustment record ?" + "\r\n\r\n\r\n" +
                    "Note : Collection entry will now treated as advance."
                    , CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbTVoucherEntry = new DBTVaucherEntry();
                    for (int i = 0; i < dgCollwiseBill.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgCollwiseBill.Rows[i].Cells[CollBillIndex.Chk].EditedFormattedValue))
                        {
                            #region Check for the JV of Interest exist. If found take action on data
                            DataTable dtJV = ObjFunction.GetDataView("Select FkJournalVoucherNo, PkSrNo, JVAmount From TVoucherJournalEntry " +
                                " Where FKReceiptVoucherNo = " + CollectionPkVoucherNo + " AND " +
                                " FKVoucherNo = " + dgCollwiseBill.Rows[i].Cells[CollBillIndex.BillPkVoucherNo].EditedFormattedValue.ToString()).Table;

                            for (int j = 0; j < dtJV.Rows.Count; j++)
                            {
                                DialogResult dr = OMMessageBox.Show("Interst Entry of Rs. : " + dtJV.Rows[j][2].ToString() + " found." + "\r\n\r\n\r\n" +
                                    "Do you want to delete Interest entry ?", "Interest Entry found ..."
                                    , OMMessageBoxButton.YesNo
                                    , OMMessageBoxIcon.Question
                                    , OMMessageBoxDefaultButton.Button2);

                                if (dr == DialogResult.Yes)
                                {
                                    #region Delete JV's Agst Ref in Collection And JV's Voucher then reduce Collection amount by JV's Amount

                                    string strQuery = "SELECT TVRD_C.PkRefTrnNo " +
                                            " FROM         TVoucherDetails AS TVD_B INNER JOIN " +
                                            " TVoucherRefDetails AS TVRD_B ON TVD_B.PkVoucherTrnNo = TVRD_B.FkVoucherTrnNo INNER JOIN " +
                                            " TVoucherRefDetails AS TVRD_C ON TVRD_B.RefNo = TVRD_C.RefNo AND TVRD_B.PkRefTrnNo <> TVRD_C.PkRefTrnNo " +
                                            " WHERE     (TVD_B.FkVoucherNo in (" + dtJV.Rows[j][0].ToString() + ")) ";

                                    TVoucherRefDetails tvref = new TVoucherRefDetails();
                                    tvref.PkRefTrnNo = ObjQry.ReturnLong(strQuery, CommonFunctions.ConStr);
                                    dbTVoucherEntry.DeleteTVoucherRefDetails(tvref);

                                    tVoucherEntry = new TVoucherEntry();
                                    tVoucherEntry.PkVoucherNo = Convert.ToInt64(dtJV.Rows[j][0].ToString());
                                    dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);

                                    TVoucherJournalEntry tvje = new TVoucherJournalEntry();
                                    tvje.PKSrNo = Convert.ToInt64(dtJV.Rows[j][1].ToString());
                                    dbTVoucherEntry.DeleteTVoucherJournalEntry(tvje);

                                    if (VoucherType == VchType.Sales)
                                        dbTVoucherEntry.UpdateVoucherRefDetails_Interest(CollectionPkVoucherNo, Convert.ToDouble(dtJV.Rows[j][2].ToString()), 2);
                                    else
                                        dbTVoucherEntry.UpdateVoucherRefDetails_Interest(CollectionPkVoucherNo, Convert.ToDouble(dtJV.Rows[j][2].ToString()), 1);

                                    #endregion
                                }
                            }
                            #endregion

                            TVoucherRefDetails tVchRefDetails = new TVoucherRefDetails();
                            tVchRefDetails.PkRefTrnNo = Convert.ToInt64(dgCollwiseBill.Rows[i].Cells[CollBillIndex.PkRefTrnNo].Value);
                            dbTVoucherEntry.DeleteTVoucherRefDetails(tVchRefDetails);

                            if (Convert.ToInt64(dgCollwiseBill.Rows[i].Cells[CollBillIndex.DiscAmt].Value) > 0)
                            {
                                if (VoucherType == VchType.Sales)
                                    dbTVoucherEntry.UpdateVoucherRefDetails(CollectionPkVoucherNo, Convert.ToInt64(dgCollwiseBill.Rows[i].Cells[CollBillIndex.DiscAmt].Value), 2);
                                else
                                    dbTVoucherEntry.UpdateVoucherRefDetails(CollectionPkVoucherNo, Convert.ToInt64(dgCollwiseBill.Rows[i].Cells[CollBillIndex.DiscAmt].Value), 1);
                            }
                        }
                    }
                    dbTVoucherEntry.ExecuteNonQueryStatements();
                    //pnlBillWise.Visible = false;
                    OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindGridCollectionwise();
                }
            }
            else
            {
                DisplayMessage("Select Atleast one Bill");
                dgDataRecord.Focus();
            }
        }

        #endregion

        private static class ColIndex
        {
            public static int BillNo = 0;
            public static int BillDate = 1;
            public static int LedgerName = 2;
            public static int BillAmt = 3;
            public static int TotRec = 4;
            public static int NetBal = 5;
            public static int LedgerNo = 6;
            public static int RefNo = 7;
            public static int Chk = 8;
            public static int BillPkVoucherNo = 9;
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            while (GridView.Rows.Count > 0)
                GridView.Rows.RemoveAt(0);
            btnShow.Focus();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //Boolean flag = false;

            //for (int i = 0; i < GridView.Rows.Count; i++)
            //{
            //    if (Convert.ToBoolean(GridView.Rows[i].Cells[ColIndex.Chk].EditedFormattedValue))
            //    {
            //        flag = true; break;
            //    }
            //}
            //if (flag)
            //{
            //    if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            //    {

            //        for (int i = 0; i < GridView.Rows.Count; i++)
            //        {
            //            if (Convert.ToBoolean(GridView.Rows[i].Cells[ColIndex.Chk].EditedFormattedValue))
            //            {

            //                string strData = "";

            //                if (VoucherType == VchType.Sales)
            //                {
            //                    strData = " SELECT TVoucherEntry.PkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
            //                              " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo " +
            //                              " WHERE (TVoucherDetails.LedgerNo = " + GridView.Rows[i].Cells[ColIndex.LedgerNo].Value + ") AND (TVoucherEntry.VoucherTypeCode in( " + VchType.SalesReceipt + "," + VchType.RejectionIn + "))" +
            //                              " AND (TVoucherRefDetails.RefNo = " + GridView.Rows[i].Cells[ColIndex.RefNo].Value + ") AND " +
            //                              " (TVoucherEntry.CompanyNo = " + DBGetVal.CompanyNo + ")  ";
            //                }
            //                else if (VoucherType == VchType.Purchase)
            //                {
            //                    strData = " SELECT TVoucherEntry.PkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
            //                              " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo " +
            //                              " WHERE (TVoucherDetails.LedgerNo = " + GridView.CurrentRow.Cells[ColIndex.LedgerNo].Value + ") AND (TVoucherEntry.VoucherTypeCode in (" + VchType.PurchasePayment + "," + VchType.RejectionOut + "))" +
            //                              " AND (TVoucherRefDetails.RefNo = " + GridView.CurrentRow.Cells[ColIndex.RefNo].Value + ") AND " +
            //                              " (TVoucherEntry.CompanyNo = " + DBGetVal.CompanyNo + ") ";

            //                }

            //                DataTable dt = ObjFunction.GetDataView(strData).Table;
            //                for (int j = 0; j < dt.Rows.Count; j++)
            //                {
            //                    dbTVoucherEntry = new DBTVaucherEntry();
            //                    tVoucherEntry = new TVoucherEntry();
            //                    tVoucherEntry.PkVoucherNo = Convert.ToInt64(dt.Rows[j].ItemArray[0].ToString());
            //                    dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);
            //                }
            //            }
            //        }
            //        OMMessageBox.Show("Record deleted successfully...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            //        GridView.Focus();
            //        BindGrid();
            //    }
            //}
            //else
            //{
            //    DisplayMessage("Select Atleast one Bill");
            //    GridView.Focus();
            //}
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            btnwdCancel_Click(sender, new EventArgs());
            btnDCCancel_Click(sender, new EventArgs());
            btnpnCancel_Click(sender, new EventArgs());
            if (rbBillwise.Checked == true)
            {
                pnlBillWise.Visible = true;
                pnlCollection.Visible = false;
                pnlDeleteCollection.Visible = false;
                dtpFromDate.Focus();
            }
            else if (rbCollectionwise.Checked == true)
            {
                pnlBillWise.Visible = false;
                pnlCollection.Visible = true;
                pnlDeleteCollection.Visible = false;
                dtpFromDate.Focus();
            }
            else if (rbDeleteCollection.Checked == true)
            {
                pnlBillWise.Visible = false;
                pnlCollection.Visible = false;
                pnlDeleteCollection.Visible = true;
                dtpFromDate.Focus();
            }
        }

        private void btnpnCancel_Click(object sender, EventArgs e)
        {
            while (dgCollwiseCollection.Rows.Count > 0)
                dgCollwiseCollection.Rows.RemoveAt(0);
            while (dgCollwiseBill.Rows.Count > 0)
                dgCollwiseBill.Rows.RemoveAt(0);
            cmbParty.SelectedIndex = 0;
            dtpFromDate.Focus();
        }

        private void btnwdCancel_Click(object sender, EventArgs e)
        {
            while (GridView.Rows.Count > 0)
                GridView.Rows.RemoveAt(0);
            while (dgDataRecord.Rows.Count > 0)
                dgDataRecord.Rows.RemoveAt(0);
            cmbParty.SelectedIndex = 0;
            dtpFromDate.Focus();
        }


        #region Delete Collection

        public static class DeleteCollectionColIndex
        {
            public static int VoucherUserNo = 0;
            public static int VchDate = 1;
            public static int PayType = 2;
            public static int Amount = 3;
            public static int RecAmt = 4;
            public static int NetBal = 5;
            public static int PKVoucherTrnNo = 6;
            public static int VoucherSrNo = 7;
            public static int PkVoucherNo = 8;
            public static int chk = 9;
        }

        private void BindRecordsDeleteCollectionwise()
        {
            try
            {
                isBinding_CollectionEntry_Grid = true;
                while (dgDeleteCollection.Rows.Count > 0)
                    dgDeleteCollection.Rows.RemoveAt(0);

                while (dgDeleteCollectionDtls.Rows.Count > 0)
                    dgDeleteCollectionDtls.Rows.RemoveAt(0);

                DataTable dt = new DataTable();

                if (VoucherType == VchType.Sales)
                {
                    dt = ObjFunction.GetDataView("Select VoucherUserNo,VoucherDate,PayTypeName,AdvAmount,TotBalAmt AS RecAmt,(AdvAmount-TotBalAmt)As NetBal,PKVoucherTrnNo,VoucherSrNo,PkVoucherNo,'false' as chk " +
                        " From (SELECT     TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, MPayType.PayTypeName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount  , TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo," +
                        " (SELECT IsNull(SUM(Amount),0) FROM  TVoucherRefDetails WHERE(FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)) AS TotBalAmt,TVoucherEntry.PkVoucherNo FROM TVoucherDetails INNER JOIN " +
                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.SalesReceipt + ") AND (TVoucherDetails.SrNo = 501) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") AND " +
                        " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND (TVoucherEntry.IsCancel = 'false') AND TVoucherEntry.VoucherDate>='" + dtpFromDate.Text + "' AND TVoucherEntry.VoucherDate<='" + dtpToDate.Text + "'" +
                        " GROUP BY TVoucherEntry.VoucherUserNo,TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MPayType.PayTypeName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo ,TVoucherEntry.PkVoucherNo" +
                        ")TempTable").Table;

                }
                else if (VoucherType == VchType.Purchase)
                {
                    dt = ObjFunction.GetDataView("Select VoucherUserNo,VoucherDate,PayTypeName,AdvAmount,TotBalAmt AS RecAmt,(AdvAmount-TotBalAmt)As NetBal,PKVoucherTrnNo,VoucherSrNo,PkVoucherNo " +
                        " From(SELECT     TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, MPayType.PayTypeName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount  , TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo," +
                        " (SELECT IsNull(SUM(Amount),0) FROM  TVoucherRefDetails WHERE(FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)) AS TotBalAmt,TVoucherEntry.PkVoucherNo FROM TVoucherDetails INNER JOIN " +
                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.PurchasePayment + ") AND (TVoucherDetails.SrNo = 501) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") AND " +
                        " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND (TVoucherEntry.IsCancel = 'false') AND TVoucherEntry.VoucherDate>='" + dtpFromDate.Text + "' AND TVoucherEntry.VoucherDate<='" + dtpToDate.Text + "'" +
                        " GROUP BY TVoucherEntry.VoucherUserNo,TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MPayType.PayTypeName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo,TVoucherEntry.PkVoucherNo " +
                        ")TempTable").Table;
                }
                else if (VoucherType == VchType.DSales)
                {
                    dt = ObjFunction.GetDataView("Select VoucherUserNo,VoucherDate,PayTypeName,AdvAmount,TotBalAmt AS RecAmt,(AdvAmount-TotBalAmt)As NetBal,PKVoucherTrnNo,VoucherSrNo,PkVoucherNo,'false' as chk " +
                      " From (SELECT     TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, MPayType.PayTypeName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount  , TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo," +
                      " (SELECT IsNull(SUM(Amount),0) FROM  TVoucherRefDetails WHERE(FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)) AS TotBalAmt,TVoucherEntry.PkVoucherNo FROM TVoucherDetails INNER JOIN " +
                      " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                      " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.DSalesReceipt + ") AND (TVoucherDetails.SrNo = 501) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") AND " +
                      " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND (TVoucherEntry.IsCancel = 'false') AND TVoucherEntry.VoucherDate>='" + dtpFromDate.Text + "' AND TVoucherEntry.VoucherDate<='" + dtpToDate.Text + "'" +
                      " GROUP BY TVoucherEntry.VoucherUserNo,TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MPayType.PayTypeName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo ,TVoucherEntry.PkVoucherNo" +
                      ")TempTable").Table;
                }
                else if (VoucherType == VchType.DPurchase)
                {
                    dt = ObjFunction.GetDataView("Select VoucherUserNo,VoucherDate,PayTypeName,AdvAmount,TotBalAmt AS RecAmt,(AdvAmount-TotBalAmt)As NetBal,PKVoucherTrnNo,VoucherSrNo,PkVoucherNo " +
                        " From(SELECT     TVoucherEntry.VoucherUserNo,TVoucherEntry.VoucherDate, MPayType.PayTypeName, SUM(TVoucherDetails.Credit + TVoucherDetails.Debit) AS AdvAmount  , TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo," +
                        " (SELECT IsNull(SUM(Amount),0) FROM  TVoucherRefDetails WHERE(FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo)) AS TotBalAmt,TVoucherEntry.PkVoucherNo FROM TVoucherDetails INNER JOIN " +
                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.DPurchasePayment + ") AND (TVoucherDetails.SrNo = 501) AND (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") AND " +
                        " (TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ") AND (TVoucherEntry.IsCancel = 'false') AND TVoucherEntry.VoucherDate>='" + dtpFromDate.Text + "' AND TVoucherEntry.VoucherDate<='" + dtpToDate.Text + "'" +
                        " GROUP BY TVoucherEntry.VoucherUserNo,TVoucherEntry.PkVoucherNo, TVoucherEntry.VoucherDate, MPayType.PayTypeName, TVoucherDetails.PkVoucherTrnNo, TVoucherDetails.VoucherSrNo,TVoucherEntry.PkVoucherNo " +
                        ")TempTable").Table;
                }
                if (dt.Rows.Count == 0)
                {
                    if (VoucherType == VchType.Sales)
                        DisplayMessage("No Collection Entry Found");
                    else
                        DisplayMessage("No Payments Entry Found");
                }
                else
                {

                    pnlDeleteCollection.Visible = true;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgDeleteCollection.Rows.Add();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dgDeleteCollection.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                    }
                    if (dgDeleteCollection.Rows.Count > 0)
                    {
                        isBinding_CollectionEntry_Grid = false;
                        BindDeleteCollectionDtls(0);
                        dgDeleteCollection.CurrentCell = dgDeleteCollection[DeleteCollectionColIndex.VoucherUserNo, 0];
                        dgDeleteCollection.Focus();
                    }
                    // pnlBillWise.Location = new System.Drawing.Point(50, 110);
                    dgDeleteCollection.Columns[DeleteCollectionColIndex.Amount].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDeleteCollection.Columns[DeleteCollectionColIndex.RecAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDeleteCollection.Columns[DeleteCollectionColIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //dgDataRecord.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                }


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            isBinding_CollectionEntry_Grid = false;
        }

        private void dgDeleteCollection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == DeleteCollectionColIndex.VchDate)
            {
                if (Convert.IsDBNull(e.Value) == false)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
                    if (e.Value.ToString() == "01-Jan-19")
                        e.Value = "";
                }
            }
        }

        private void dgDeleteCollection_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BindDeleteCollectionDtls(int newRowIndex)
        {
            try
            {
                if (isBinding_CollectionEntry_Grid) return;

                dgDeleteCollectionDtls.Rows.Clear();

                if (newRowIndex != -1 && dgDeleteCollection.Rows[newRowIndex].Cells[DeleteCollectionColIndex.PKVoucherTrnNo].EditedFormattedValue.ToString() != "")
                {
                    //string sql = "Select BillNo,VoucherDate,LedgerName,BilledAmount ,((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) as TotRec,DiscAmt,((Amount-((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) )) as NetBal,LedgerNo,RfNo,'false' As Chk ,PKRefTrnNo " +
                    //    " From (SELECT TVoucherRefDetails.RefNo as RfNo, TVoucherEntry.VoucherDate,(Select L.LedgerName From MLedger L Where L.LedgerNo=TVoucherDetails.LedgerNo)As LedgerName, case when " + VchType.Sales + "=9 then TVoucherEntry.Reference else cast(TVoucherEntry.VoucherUserNo as varchar) end as BillNo, TVoucherEntry.BilledAmount ,TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt, TVoucherDetails.LedgerNo,TVoucherRefDetails_1.PKRefTrnNo  " +
                    //    " FROM  TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo  " +
                    //    " INNER JOIN TVoucherRefDetails AS TVoucherRefDetails_1 ON TVoucherRefDetails.RefNo=TVoucherRefDetails_1.RefNo WHERE   (TVoucherEntry.VoucherTypeCode = " + VchType.Sales + ") AND (TVoucherRefDetails.TypeOfRef = 3) AND  (TVoucherEntry.CompanyNo =" + DBGetVal.FirmNo + ") and (TVoucherEntry.IsCancel='false')  " +
                    //    " AND TVoucherRefDetails_1.TypeOfRef in (2) AND TVoucherRefDetails_1.FKVoucherTrnNo=" + dgDeleteCollection.Rows[newRowIndex].Cells[DeleteCollectionColIndex.PKVoucherTrnNo].Value + "   ) as tbl1  Where    (LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") ";
                    string sql = "";

                    if (VoucherType == VchType.Purchase || VoucherType == VchType.DPurchase)
                    {
                        sql = "Select BillNo,VoucherDate,LedgerName,BilledAmount ,((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) as TotRec,DiscAmt,((Amount-((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) )) as NetBal,LedgerNo,RfNo,'false' As Chk ,PKRefTrnNo " +
                          " From (SELECT TVoucherRefDetails.RefNo as RfNo, TVoucherEntry.VoucherDate,(Select L.LedgerName From MLedger L Where L.LedgerNo=TVoucherDetails.LedgerNo)As LedgerName, case when " + VchType.Sales + "=9 then TVoucherEntry.Reference else cast(TVoucherEntry.VoucherUserNo as varchar) end as BillNo, TVoucherEntry.BilledAmount ,TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt, TVoucherDetails.LedgerNo,TVoucherRefDetails_1.PKRefTrnNo  " +
                          " FROM  TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo  " +
                          " INNER JOIN TVoucherRefDetails AS TVoucherRefDetails_1 ON TVoucherRefDetails.RefNo=TVoucherRefDetails_1.RefNo WHERE   (TVoucherEntry.VoucherTypeCode in (31)) AND  (TVoucherEntry.CompanyNo =" + DBGetVal.FirmNo + ") and (TVoucherEntry.IsCancel='false')  " +
                          " AND TVoucherRefDetails_1.TypeOfRef in (2,5) AND TVoucherRefDetails_1.FKVoucherTrnNo=" + dgDeleteCollection.Rows[newRowIndex].Cells[DeleteCollectionColIndex.PKVoucherTrnNo].Value + "   ) as tbl1  Where    (LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") ";
                    }
                    else if (VoucherType == VchType.Sales || VoucherType == VchType.DSales)
                    {
                        sql = "Select BillNo,VoucherDate,LedgerName,BilledAmount ,((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) as TotRec,DiscAmt,((Amount-((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) )) as NetBal,LedgerNo,RfNo,'false' As Chk ,PKRefTrnNo " +
                           " From (SELECT TVoucherRefDetails.RefNo as RfNo, TVoucherEntry.VoucherDate,(Select L.LedgerName From MLedger L Where L.LedgerNo=TVoucherDetails.LedgerNo)As LedgerName, case when " + VchType.Sales + "=9 then TVoucherEntry.Reference else cast(TVoucherEntry.VoucherUserNo as varchar) end as BillNo, TVoucherEntry.BilledAmount ,TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt, TVoucherDetails.LedgerNo,TVoucherRefDetails_1.PKRefTrnNo  " +
                           " FROM  TVoucherEntry INNER JOIN  TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo  " +
                           " INNER JOIN TVoucherRefDetails AS TVoucherRefDetails_1 ON TVoucherRefDetails.RefNo=TVoucherRefDetails_1.RefNo WHERE   (TVoucherEntry.VoucherTypeCode in (30)) AND  (TVoucherEntry.CompanyNo =" + DBGetVal.FirmNo + ") and (TVoucherEntry.IsCancel='false')  " +
                           " AND TVoucherRefDetails_1.TypeOfRef in (2,5) AND TVoucherRefDetails_1.FKVoucherTrnNo=" + dgDeleteCollection.Rows[newRowIndex].Cells[DeleteCollectionColIndex.PKVoucherTrnNo].Value + "   ) as tbl1  Where    (LedgerNo = " + ObjFunction.GetComboValue(cmbParty) + ") ";
                    }
                    DataTable dt = ObjFunction.GetDataView(sql).Table;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgDeleteCollectionDtls.Rows.Add();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dgDeleteCollectionDtls.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                    }


                    dgDeleteCollectionDtls.Columns[CollBillIndex.BillAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDeleteCollectionDtls.Columns[CollBillIndex.NetBal].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDeleteCollectionDtls.Columns[CollBillIndex.TotRec].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgDeleteCollectionDtls.Columns[CollBillIndex.DiscAmt].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (dgDeleteCollectionDtls.Rows.Count > 0)
                    {
                        dgDeleteCollectionDtls.CurrentCell = dgDeleteCollectionDtls[0, 0];
                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgDeleteCollectionDtls_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == CollBillIndex.BillDate)
            {
                if (Convert.IsDBNull(e.Value) == false)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yy");
                    if (e.Value.ToString() == "01-Jan-19")
                        e.Value = "";
                }
            }

        }

       

        private void btnDCCancel_Click(object sender, EventArgs e)
        {
            while (dgDeleteCollection.Rows.Count > 0)
                dgDeleteCollection.Rows.RemoveAt(0);
            while (dgDeleteCollectionDtls.Rows.Count > 0)
                dgDeleteCollectionDtls.Rows.RemoveAt(0);
            cmbParty.SelectedIndex = 0;
            dtpFromDate.Focus();

        }

        private void btnDeleteCollection_Click(object sender, EventArgs e)
        {
            Boolean flag = false;

            for (int i = 0; i < dgDeleteCollection.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgDeleteCollection.Rows[i].Cells[DeleteCollectionColIndex.chk].EditedFormattedValue))
                {
                    flag = true; break;
                }
            }
            if (flag)
            {
                if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = 0; i < dgDeleteCollection.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgDeleteCollection.Rows[i].Cells[DeleteCollectionColIndex.chk].EditedFormattedValue.ToString()))
                        {

                            DataTable dtJV = ObjFunction.GetDataView("Select FkJournalVoucherNo From TVoucherJournalEntry " +
                                " Where FKReceiptVoucherNo = " + Convert.ToInt64(dgDeleteCollection.Rows[i].Cells[DeleteCollectionColIndex.PkVoucherNo].Value)).Table;

                            for (int j = 0; j < dtJV.Rows.Count; j++)
                            {
                                dbTVoucherEntry = new DBTVaucherEntry();
                                tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = Convert.ToInt64(dtJV.Rows[j][0].ToString());
                                dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);
                            }

                            dbTVoucherEntry = new DBTVaucherEntry();
                            tVoucherEntry = new TVoucherEntry();
                            tVoucherEntry.PkVoucherNo = Convert.ToInt64(dgDeleteCollection.Rows[i].Cells[DeleteCollectionColIndex.PkVoucherNo].Value);
                            dbTVoucherEntry.DeleteAllVoucherEntry(tVoucherEntry);
                        }
                    }
                    checkBox3.Checked = false;
                    BindRecordsDeleteCollectionwise();
                }
            }
            else
            {
                DisplayMessage("Select Atleast one Bill");
                dgDeleteCollection.Focus();
            }
        }

        #endregion

        private void dgDeleteCollection_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            BindDeleteCollectionDtls(e.RowIndex);
            //dgDeleteCollectionDtls.Focus();
        }





    }
}
