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
    public partial class GRNSelection : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        public DataTable dtChallanNo = new DataTable();
        public DialogResult DS = DialogResult.OK;
        long LedgerNo=0,VchNo=0;

        public GRNSelection()
        {
            InitializeComponent();
        }

        public GRNSelection(long VoucherNo,long LedgNo)
        {
            InitializeComponent();
            LedgerNo = LedgNo;
            VchNo = VoucherNo;
        }

        private void DCSelection_Load(object sender, EventArgs e)
        {
            BindGrid();
            dgSelection.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgSelection.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgSelection.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (VchNo != 0)
            {
                dgSelection.Columns[4].Visible = false;
                dtChallanNo = new DataTable();
            }
            else
            {
                dgSelection.Columns[4].Visible = true;
                btnOk.Visible = true;
                dtChallanNo = new DataTable();
            }

            dtChallanNo.Columns.Add();
        }

        public void BindGrid()
        {
            DataTable dt = new DataTable();
            if (VchNo == 0)
            {
                dt = ObjFunction.GetDataView(" SELECT GRNNo, GRNUserNo, RefNo, GRNDate " +
                            " FROM TGRN where LedgerNo=" + LedgerNo + " and GRNNo not in(Select FkGRNNo from TGRNInvoice) AND TGRN.IsCancel='false'").Table;
                
            }
            else if (VchNo != 0)
            {
                dt = ObjFunction.GetDataView(" SELECT TGRN.GRNNo, TGRN.GRNUserNo, TGRN.RefNo, TGRN.GRNDate " +
                                            " FROM TGRNInvoice INNER JOIN  TGRN ON TGRNInvoice.FkGRNNo = TGRN.GRNNo " +
                                            " where TGRN.LedgerNo=" + LedgerNo + " and  TGRNInvoice.FkVoucherNo=" + VchNo + " AND TGRN.IsCancel='false'").Table;
            }
            dgSelection.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgSelection.Rows.Add();
                for (int j = 0; j < dgSelection.Columns.Count - 1; j++)
                {
                    dgSelection.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
            }
            if (dt.Rows.Count > 0)
            {
                dgSelection.CurrentCell = dgSelection[4, 0];
                dgSelection.Focus();
            }
            else
                this.Close();
            
        }

        private void dgSelection_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null && e.Value.ToString() != "")
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DataRow dr;
            if (VchNo != 0)
            {
                DS = DialogResult.OK;
                this.Close();
            }
            else
            {
                for (int i = 0; i < dgSelection.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgSelection.Rows[i].Cells[4].FormattedValue) == true)
                    {
                        dr = dtChallanNo.NewRow();
                        dr[0] = dgSelection.Rows[i].Cells[0].Value;
                        dtChallanNo.Rows.Add(dr);
                    }
                }
                if (dtChallanNo.Rows.Count > 0)
                {
                    DS = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void dgSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                if (btnOk.Visible)
                    btnOk.Focus();
                else
                    btnCancel.Focus();
            }
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
                DataRow dr;
                if (VchNo != 0)
                {
                    if (dgSelection.Rows.Count > 1)
                    {
                        dr = dtChallanNo.NewRow();
                        dr[0] = dgSelection.Rows[dgSelection.CurrentCell.RowIndex].Cells[0].Value;
                        dtChallanNo.Rows.Add(dr);
                        dgSelection.Rows.RemoveAt(dgSelection.CurrentCell.RowIndex);
                    }
                    else
                    {
                        MessageBox.Show("You can not delete this row...", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DS = DialogResult.Cancel;
            this.Close();
        }
    }
}
