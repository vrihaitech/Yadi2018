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
    public partial class PartialPurchasePaymentAdjust : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public DialogResult DS = DialogResult.Cancel;
        DataTable dtDelete = new DataTable();
        long ID = 0, PayType = 1, PartyNo = 0;
        DataTable dtPayLedger = new DataTable();
        double GrandTotal = 0, TotalCollectAmt = 0;

        public DataTable dtCompRatio = new DataTable();
        public long MfgCompNo = 0;

        public PartialPurchasePaymentAdjust()
        {
            InitializeComponent();
        }

        public PartialPurchasePaymentAdjust(long id, double Amt, long paytype, long partyno)
        {
            InitializeComponent();
            ID = id;
            GrandTotal = Amt;
            PayType = paytype;
            PartyNo = partyno;
        }

        private void PartialPayment_Load(object sender, EventArgs e)
        {
            try
            {
                BindGridPayType(ID);
                lblBillAmt.Text = GrandTotal.ToString(Format.DoubleFloating);
                lblAdjustAmt.Text = "0.00";
                TotalCollectAmt = 0;
                for (int i = 0; i < dgPayType.Rows.Count; i++)
                {
                    TotalCollectAmt += Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value.ToString());
                }
                txtTotalAmt.Text = TotalCollectAmt.ToString(Format.DoubleFloating);
                lblCollectionAmt.Text = TotalCollectAmt.ToString(Format.DoubleFloating);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridPayType(long ID)
        {
            try
            {
                DataTable dtPayType = new DataTable();
                long refNo = ObjQry.ReturnLong("SELECT TVoucherRefDetails.RefNo FROM TVoucherRefDetails INNER JOIN TVoucherDetails ON TVoucherRefDetails.FkVoucherTrnNo = TVoucherDetails.PkVoucherTrnNo WHERE (TVoucherRefDetails.TypeOfRef = 3) AND (TVoucherDetails.FkVoucherNo = " + ID + ")", CommonFunctions.ConStr);
                string strQuery = "SELECT 0 As SrNo,TVoucherEntry.VoucherDate, TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt As DiscAmt,  Case When(TVoucherEntry.VoucherTypeCode<>" + VchType.RejectionIn + ")Then MPayType.PayTypeName Else 'Against Sales Return' End AS PayTypeName , TVoucherChqCreditDetails.ChequeNo, TVoucherChqCreditDetails.ChequeDate, " +
                              " TVoucherChqCreditDetails.CreditCardNo,TVoucherRefDetails.PkRefTrnNo,'false' As chk,TVoucherEntry.PKVoucherNo " +
                              " FROM         TVoucherEntry INNER JOIN " +
                              " TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                              " TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo LEFT OUTER JOIN " +
                              " TVoucherChqCreditDetails ON TVoucherEntry.PkVoucherNo = TVoucherChqCreditDetails.FKVoucherNo INNER JOIN " +
                              " MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                        " WHERE (TVoucherDetails.LedgerNo = " + PartyNo + ") AND  (TVoucherEntry.VoucherTypeCode in( " + VchType.PurchasePayment + "," + VchType.RejectionOut + "))" +
                        "AND (TVoucherRefDetails.RefNo = " + refNo + ") AND " +
                        "(TVoucherEntry.CompanyNo = " + DBGetVal.FirmNo + ")  " +//ORDER BY TVoucherEntry.VoucherDate
                        " UNION " +
                        " SELECT 0 As SrNo,TVoucherRefDetails.UserDate, TVoucherRefDetails.Amount,TVoucherRefDetails.DiscAmt,  'Against Op Balance' , '', '',  '',0,'false' As chk,0 AS PKVoucherNo " +
                        " FROM TVoucherRefDetails   WHERE (TVoucherRefDetails.LedgerNo = " + PartyNo + ") AND TVoucherRefDetails.TypeOfRef=5 AND (TVoucherRefDetails.RefNo = " + refNo + ") " +
                        " AND (TVoucherRefDetails.CompanyNo = " + DBGetVal.FirmNo + ")  ORDER BY TVoucherRefDetails.PkRefTrnNo";

                dtPayType = ObjFunction.GetDataView(strQuery).Table;


                //while (dgPayType.Columns.Count > 0)
                //    dgPayType.Columns.RemoveAt(0);
                dgPayType.DataSource = dtPayType.DefaultView;
                for (int i = 0; i < dgPayType.Rows.Count; i++)
                {
                    dgPayType.Rows[i].Cells[9].Value = false;
                }
                pnlPartial.Visible = true;
                if (dgPayType.Rows.Count > 0)
                {
                    dgPayType.CurrentCell = dgPayType[2, 0];
                    dgPayType.Focus();
                }
                else
                {
                    DS = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool SaveData(DBTVaucherEntry dbTVoucherEntry)
        {
            for (int i = 0; i < dgPayType.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgPayType.Rows[i].Cells[9].Value) == true)
                {
                    //long RefNo = ObjQry.ReturnLong("SELECT isnull(MAX(RefNo),0) + 1 FROM TVoucherRefDetails", CommonFunctions.ConStr);
                    //dbTVoucherEntry.UpdateVoucherRefDetails(Convert.ToInt64(dgPayType.Rows[i].Cells[7].Value), RefNo);
                    TVoucherRefDetails tVchRefDetails = new TVoucherRefDetails();
                    tVchRefDetails.PkRefTrnNo = Convert.ToInt64(dgPayType.Rows[i].Cells[8].Value);
                    dbTVoucherEntry.DeleteTVoucherRefDetails(tVchRefDetails);
                    dbTVoucherEntry.UpdateVoucherRefDetails(Convert.ToInt64(dgPayType.Rows[i].Cells[10].Value), Convert.ToDouble(dgPayType.Rows[i].Cells[3].Value), 1);
                    //string strQuery = "Select FKVoucherNo From TVoucherDetails Where PKVoucherTrnNo in (Select FKVoucherTrnNo From TVoucherRefDetails Where PkRefTrnNo=" + ID + ")";
                    //dbTVoucherEntry.DeleteVoucherPayTypeDetails(ObjQry.ReturnLong(strQuery, CommonFunctions.ConStr));
                }
            }
            return true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;

                if (Convert.ToDouble(lblCollectionAmt.Text) < Convert.ToDouble(lblBillAmt.Text))
                {
                    flag = true;
                }
                else if ((Convert.ToDouble(lblCollectionAmt.Text) - Convert.ToDouble(lblAdjustAmt.Text)) > Convert.ToDouble(lblBillAmt.Text))
                {
                    OMMessageBox.Show("Adjust Amount should be less than bill amount.. ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    flag = false;
                }
                else
                    flag = true;

                if (flag == true)
                {
                    DS = DialogResult.OK;
                    this.Close();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (Convert.ToDouble(txtTotalAmt.Text) == 0)
            //{
            DS = DialogResult.Cancel;
            //if (PartyNo == Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.S_PartyAC)))
            //{
            //    OMMessageBox.Show("Bill Amount does not match...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            //    return;
            //}
            this.Close();
            //}
            //else
            //{
            //    OMMessageBox.Show("Amount Should Be Zero", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            //    return;
            //}
        }

        private void dgPayType_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    btnOk.Enabled = true;
                    if (dgPayType.CurrentCell.Value == null) dgPayType.CurrentCell.Value = "0";
                    if (ObjFunction.CheckValidAmount(dgPayType.CurrentCell.Value.ToString()) == false)
                    {
                        dgPayType.CurrentCell.Value = "0.00";
                        OMMessageBox.Show("Please Enter valid amount..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { dgPayType.CurrentCell.RowIndex, 2, dgPayType });
                    }
                    else
                    {
                        dgPayType.CurrentCell.ErrorText = "";
                        dgPayType.CurrentCell.Value = Convert.ToDouble(dgPayType.CurrentCell.Value).ToString("0.00");
                        dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[8].Value = Convert.ToDouble((Convert.ToDouble(dgPayType.CurrentCell.Value.ToString()) * Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[7].Value)) / 100).ToString("0.00");


                        if (dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[6].Value.ToString() == "4")//dgPayType.CurrentCell.RowIndex == 3
                        {
                            if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) > 0)
                                btnOk.Enabled = false;
                            else
                                btnOk.Enabled = true;


                        }
                        if (dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[6].Value.ToString() == "5")
                        {
                            if (Convert.ToDouble(dgPayType.Rows[dgPayType.CurrentCell.RowIndex].Cells[2].Value) > 0)
                                btnOk.Enabled = false;
                            else
                                btnOk.Enabled = true;


                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayType_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgPayType.CurrentCell.ColumnIndex == 2)
            {
                TextBox txt = (TextBox)e.Control;
            }
        }

        private void dgPayType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Escape)
                //{
                //    btnOk_Click(sender, new EventArgs());
                //}

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void dgPayType_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Value = (e.RowIndex + 1).ToString();
            if (e.ColumnIndex == 1 || e.ColumnIndex == 6)
            {
                if (Convert.IsDBNull(e.Value) == false)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                    if (e.Value.ToString() == "01-Jan-1900")
                        e.Value = "";
                }
            }
        }

        private void dgPayType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 8 )
            //{
            //    CalculateTotal();
            //}
        }

        public void CalculateTotal()
        {
            double TotAdjustAmt = 0;

            for (int i = 0; i < dgPayType.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgPayType.Rows[i].Cells[9].EditedFormattedValue.ToString()) == true)
                    TotAdjustAmt += Convert.ToDouble(dgPayType.Rows[i].Cells[2].Value.ToString());
            }

            lblAdjustAmt.Text = TotAdjustAmt.ToString(Format.DoubleFloating);
        }

        private void dgPayType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9)
            {
                CalculateTotal();
            }
        }

    }
}
