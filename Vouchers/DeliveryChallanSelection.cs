using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OMControls;
using OM;

namespace Yadi.Vouchers
{
    public partial class DeliveryChallanSelection : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public static DataTable dtSOMain = new DataTable();
        public DataTable dtSODetails = new DataTable();
        DataTable dtSO = new DataTable();
        public DialogResult DS = DialogResult.OK;
        long LedgerNo = 0;//, VchNo = 0;
        public long CountSO = 0, VchTypeCode, MfgCompNo;
        public long DCNo = 0;
        public string MainDCNos = "";

        public DeliveryChallanSelection()
        {
            InitializeComponent();
        }

        public DeliveryChallanSelection(long LedgNo, long MfgCompNo)
        {
            InitializeComponent();
            LedgerNo = LedgNo;
            this.MfgCompNo = MfgCompNo;

        }

        private void SOSelection_Load(object sender, EventArgs e)
        {
            BindGrid();
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = ObjFunction.GetFont(FontStyle.Bold, 10);
            KeyDownFormat(this.Controls);
        }

        public void BindGrid()
        {
            DataTable dt = new DataTable();

            dt = ObjFunction.GetDataView(" SELECT 0 As SrNo,TDeliveryChallan.VoucherUserNo As 'DC No', TDeliveryChallan.VoucherDate AS 'DC Date', TDeliveryChallan.BilledAmount As 'Amount', MLedger.LedgerName AS 'Party', TDeliveryChallan.PkVoucherNo " +
                " FROM TDeliveryChallan INNER JOIN MLedger ON TDeliveryChallan.LedgerNo = MLedger.LedgerNo " +
                " WHERE (TDeliveryChallan.FKVoucherNo = 0) AND (TDeliveryChallan.MfgCompNo = " + MfgCompNo + ") AND (TDeliveryChallan.LedgerNo = " + LedgerNo + ")").Table;
            // dgSO.DataSource = dt.DefaultView;
            dgSO.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgSO.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j == 0)
                        dgSO.Rows[i].Cells[j].Value = (i + 1).ToString();
                    else
                        dgSO.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
                dgSO.Rows[i].Cells[ColIndex.chk].Value = false;
            }
            if (dt.Rows.Count > 0)
            {
                dgSO.CurrentCell = dgSO[1, 0];
                dgSO.Focus();
                CountSO = dgSO.Rows.Count;
            }
            else
            {
                CountSO = 0;
                DS = DialogResult.Cancel;
                this.Close();
            }

        }
        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);
        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }
        private void dgPO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = Convert.ToString(e.RowIndex + 1);
            }
            else if (e.ColumnIndex == ColIndex.VoucherDate)
            {
                if (e.Value != null && e.Value.ToString() != "")
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }
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

        }

        #endregion



        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainDCNos = "";
            DS = DialogResult.No;
            this.Close();
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int VoucherUserNo = 1;
            public static int VoucherDate = 2;
            public static int BilledAmount = 3;
            public static int LedgerName = 4;
            public static int PkVoucherNo = 5;
            public static int chk = 6;
        }
        #endregion

        private void dgPO_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    if (OMMessageBox.Show("Are you sure want to Delivery challan generate bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        e.SuppressKeyPress = true;
            //        DCNo = Convert.ToInt64(dgSO.Rows[dgSO.CurrentCell.RowIndex].Cells[ColIndex.PkVoucherNo].Value);
            //        DS = DialogResult.Yes;
            //        this.Close();
            //    }
            //}
            if (e.KeyCode == Keys.Space)
            {
                dgSO.Rows[dgSO.CurrentCell.RowIndex].Cells[ColIndex.chk].Value = !Convert.ToBoolean(dgSO.Rows[dgSO.CurrentCell.RowIndex].Cells[ColIndex.chk].EditedFormattedValue);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnOK.Focus();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int cntRow = 0;
            MainDCNos = "";
            for (int i = 0; i < dgSO.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgSO.Rows[i].Cells[ColIndex.chk].Value) == true)
                {
                    if (cntRow == 0)
                        MainDCNos = dgSO.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString();
                    else
                        MainDCNos += "," + dgSO.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString();
                    cntRow++;
                }
            }
            if (cntRow != 0)
            {
                if (OMMessageBox.Show("Are you sure want to Delivery challan generate bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DS = DialogResult.Yes;
                    this.Close();
                }
            }
            else
            {
                OMMessageBox.Show("Please select atleast one Delivery challan", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
        }

    }
}
