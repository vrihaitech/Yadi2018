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
    public partial class BillSelection : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dt = new DataTable();
        public DialogResult DS = DialogResult.OK;
        public long PKVoucherNo = 0;
        public string StrVoucherName = "", VoucherUserNo = "";

        long LedgerNo;

        public BillSelection()
        {
            InitializeComponent();
        }

        public BillSelection(long LedgerNo)
        {
            this.LedgerNo = LedgerNo;
            InitializeComponent();
        }

        private void BillSelection_Load(object sender, EventArgs e)
        {
            try
            {

                PKVoucherNo = 0;
                ObjFunction.FillCombo(cmbFirmName, "Select * from MManufacturerCompany where isactive='true' ");
                ObjFunction.FillCombo(cmbParty, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.SundryDebtors + "order by LedgerName");
                cmbFirmName.Focus();
                //if (LedgerNo > 0)
                //{
                //    cmbParty.SelectedValue = LedgerNo.ToString();
                //    //cmbParty.Enabled = false;
                //    FillGrid();
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillGrid()
        {
            try
            {
                dgBill.Rows.Clear();
                long VchCode = 0;
                if (rbSalesBillMaterial.Checked == true)
                {
               
                    VchCode = VchType.Sales;
                    StrVoucherName = "Sales Bill ";
                }
               // string  MainMfgCompNo='1','2','3','4'.ToString() ;
                //DataTable dt = ObjFunction.GetDataView("Exec GetCollectionDetails " + ObjFunction.GetComboValue(cmbParty) + "," + VchCode + "," + DBGetVal.FirmNo + "").Table;

                //string sqlQuery = "SELECT 0 As SrNo,TVoucherEntry.PKVoucherNo,TVoucherEntry.VoucherUserNo As BillNo, TVoucherEntry.VoucherDate AS Date, TVoucherEntry.BilledAmount As 'Billed Amount', MLedger.LedgerName As Party" +
                //       " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN " +
                //       " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                //       " WHERE (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.VoucherTypeCode = " + VchCode + ") AND TVoucherEntry.PayTypeNo=3 AND " +
                //       " (TVoucherEntry.IsCancel = 'false') AND MLedger.LedgerNo=" + ObjFunction.GetComboValue(cmbParty) + " order by VoucherDate desc ";
                string sqlQuery = " SELECT 0 As SrNo,TVoucherEntry.PKVoucherNo,TVoucherEntryCompany.VoucherUserNo As BillNo, TVoucherEntry.VoucherDate AS Date," +
                                   " TVoucherEntry.BilledAmount As 'Billed Amount', MLedger.LedgerName As Party " +
                                   " FROM TVoucherEntry INNER JOIN TVoucherEntryCompany on TVoucherEntryCompany.FkVoucherNo=TVoucherEntry.PkVoucherNo " +
                                   " inner join TVoucherDetailsCompany ON TVoucherEntryCompany.PKVoucherCompanyNo = TVoucherDetailsCompany.FkVoucherNo " +
                                    " INNER JOIN  MLedger ON TVoucherDetailsCompany.LedgerNo = MLedger.LedgerNo  WHERE (TVoucherDetailsCompany.VoucherSrNo = 1) " +
                                    " AND (TVoucherEntry.VoucherTypeCode = " + VchCode + ") AND TVoucherEntry.PayTypeNo=3 AND  (TVoucherEntry.IsCancel = 'false') and TVoucherEntryCompany.mfgcompno=" + ObjFunction.GetComboValue(cmbFirmName) + " " +
                                    " AND MLedger.LedgerNo=" + ObjFunction.GetComboValue(cmbParty) + " order by VoucherDate desc ";


                dt = ObjFunction.GetDataView(sqlQuery).Table;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    dgBill.Rows.Add();
                    dgBill.Rows[j].Cells[0].Value = j + 1;
                    for (int i = 1; i < dt.Columns.Count; i++)//LandedRate
                    {
                        if (i != 3)
                            dgBill.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                        else
                            dgBill.Rows[j].Cells[i].Value = Convert.ToDateTime(dt.Rows[j].ItemArray[i].ToString()).ToString(Format.DDMMMYYYY);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbParty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    FillGrid();

                    if (dgBill.Rows.Count > 0)
                    {
                        dgBill.Focus();
                        dgBill.CurrentCell = dgBill.Rows[0].Cells[2];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DS = DialogResult.Cancel;
            this.Close();
        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dgBill.CurrentCell.RowIndex != -1)
                {
                    if (OMMessageBox.Show("Are your sure want this bill against to generate Credit Not entry", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PKVoucherNo = Convert.ToInt64(dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[1].Value);
                        VoucherUserNo = dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[2].Value.ToString();
                        DS = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void cmbFirmName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (LedgerNo > 0)
                {
                    cmbParty.SelectedValue = LedgerNo.ToString();
                    //cmbParty.Enabled = false;
                    FillGrid();
                }
            }
        }

  
    }
}
