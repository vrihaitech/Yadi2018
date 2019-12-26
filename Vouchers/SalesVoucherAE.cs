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
    public partial class SalesVoucherAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        DataTable dtSearch = new DataTable();
        int cntRow, VoucherUserNo, VoucherNo,rowCount;
        double drTotal, crTotal;
        long _VchType = 0;
        long _PKVoucherNo = 0;
        public SalesVoucherAE()
        {
            InitializeComponent();
            this._VchType = OM.VchType.Sales;
        }

        public SalesVoucherAE(long _VchType, long _PKVoucherNo)
        {
            this._VchType = _VchType;
            this._PKVoucherNo = _PKVoucherNo;
            InitializeComponent();
        }

        private void SalesVoucherAE_Load(object sender, EventArgs e)
        {
            //txtNo.Visible = false;
            //txtRefNo.Visible = false;
            DataGridViewComboBoxColumn cbItemName = GridView.Columns[1] as DataGridViewComboBoxColumn;
            if (cbItemName != null)
            {
                ObjFunction.FillCombo(cbItemName, "Select SignCode,ShortName From MSign");
            }

            DataGridViewComboBoxColumn cbItemName1 = GridView.Columns[2] as DataGridViewComboBoxColumn;
            if (cbItemName1 != null)
            {
                ObjFunction.FillCombo(cbItemName1, "Select LedgerNo, LedgerName AS LedgerName From MLedger order by LedgerName");
            }

            DataGridViewComboBoxColumn cbItemName2 = GridView.Columns[6] as DataGridViewComboBoxColumn;
            if (cbItemName2 != null)
            {
                ObjFunction.FillCombo(cbItemName2, "Select CompanyNo, CompanyName  From MCompany order by CompanyName");
            }

            if (SalesVoucher.RequestSVNo != 0)
            {
                FillControls();
                String sql = "";

                if (_PKVoucherNo != 0)
                {
                    sql = "Select PkVoucherNo from TVoucherEntry Where PkVoucherNo=" + this._PKVoucherNo + "";
                }
                else if(_VchType != 0)
                {
                    sql = "Select PkVoucherNo from TVoucherEntry Where VoucherTypeCode=" + this._VchType + "";
                }

                dtSearch = ObjFunction.GetDataView(sql).Table;
                SetNavigation();
                setDisplay(true);
            }
            else
            {

                setDisplay(false);
            }
            KeyDownFormat(this.Controls);
            dtpVoucherDate.Focus();

        }

        private void FillControls()
        {
            tVoucherEntry = dbTVoucherEntry.ModifyTVoucherEntryByID(SalesVoucher.RequestSVNo);
            VoucherUserNo = Convert.ToInt32(tVoucherEntry.VoucherUserNo);
            VoucherNo = Convert.ToInt32(tVoucherEntry.PkVoucherNo);
            txtNo.Text = tVoucherEntry.PkVoucherNo.ToString();
            BindGrid();
        }

        private void BindGrid()
        {
            DataView dv = new DataView();

            dv = dbTVoucherEntry.GetTVoucherDetailsByID(SalesVoucher.RequestSVNo);
            GridView.DataSource = dv;

            GridView.Columns[5].Visible = false;
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtNarration, "");

            if (txtNarration.Text.Trim() == "")
            {
                EP.SetError(txtNarration, "Enter Narration");
                EP.SetIconAlignment(txtNarration, ErrorIconAlignment.MiddleRight);
                txtNarration.Focus();
            }

            else
                flag = true;
            return flag;

        }

        private void setValues()
        {
            if (Validations() == true)
            {
                TVoucherEntry tVoucherEntry = new TVoucherEntry();

                tVoucherEntry.PkVoucherNo = SalesVoucher.RequestSVNo;
                tVoucherEntry.VoucherTypeCode = Convert.ToInt64(this._VchType);
                tVoucherEntry.VoucherUserNo = VoucherUserNo;
                tVoucherEntry.VoucherDate = dtpVoucherDate.Value;
                tVoucherEntry.VoucherTime = Convert.ToDateTime("01-Jan-1900");
                tVoucherEntry.Narration = txtNarration.Text.Trim();
                tVoucherEntry.Reference = "";
                tVoucherEntry.ChequeNo = 0;
                tVoucherEntry.ClearingDate = dtpVoucherDate.Value;
                tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                tVoucherEntry.BilledAmount = 0;
                tVoucherEntry.ChallanNo = "";
                tVoucherEntry.OrderType = 1;
                tVoucherEntry.Remark = "";
                tVoucherEntry.TransporterCode = 0;
                tVoucherEntry.TransPayType = 0;
                tVoucherEntry.LRNo = "";
                tVoucherEntry.TransportMode = 0;
                tVoucherEntry.TransNoOfItems = 0;
                tVoucherEntry.UserID = DBGetVal.UserID;
                tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;

                dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);

                for (int j = 0; j < GridView.RowCount; j++)
                {
                    if (GridView.Rows[j].Cells[1].Value != null && GridView.Rows[j].Cells[2].Value != null)
                    {
                        TVoucherDetails tVoucherDetails = new TVoucherDetails();

                        tVoucherDetails.VoucherSrNo = j + 1;
                        tVoucherDetails.SignCode = Convert.ToInt32(GridView.Rows[j].Cells[1].Value);
                        tVoucherDetails.LedgerNo = Convert.ToInt64(GridView.Rows[j].Cells[2].Value);
                        if (Convert.IsDBNull(GridView.Rows[j].Cells[4].Value) == false)
                            tVoucherDetails.Credit = (GridView.Rows[j].Cells[4].Value == null) ? 0 : Convert.ToDouble(GridView.Rows[j].Cells[4].Value);
                        else
                            tVoucherDetails.Credit = 0;
                        if (Convert.IsDBNull(GridView.Rows[j].Cells[3].Value) == false)
                            tVoucherDetails.Debit = (GridView.Rows[j].Cells[3].Value == null) ? 0 : Convert.ToDouble(GridView.Rows[j].Cells[3].Value);
                        else
                            tVoucherDetails.Debit = 0;
                        if (Convert.IsDBNull(GridView.Rows[j].Cells[5].Value) == false)
                            tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(GridView.Rows[j].Cells[5].Value);
                        else
                            tVoucherDetails.PkVoucherTrnNo = 0;
                        tVoucherDetails.CompanyNo = Convert.ToInt64(GridView.Rows[j].Cells[6].Value);
                        dbTVoucherEntry.AddTVoucherDetails(tVoucherDetails);
                    }

                }

                if (dbTVoucherEntry.ExecuteNonQueryStatements() != 0)
                {
                    OMMessageBox.Show("Voucher Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    Form NewF = new SalesVoucherAE();
                    this.Close();
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
                else
                    OMMessageBox.Show("Voucher Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            setValues();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Form Newf = new SalesVoucher();
            this.Close();
            ObjFunction.OpenForm(Newf, DBGetVal.MainForm);
        }

        private void SalesVoucherAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            SalesVoucher.RequestSVNo = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //dbTVoucherEntry = new DBTVaucherEntry();
            //tVoucherEntry = new TVoucherEntry();

            //tVoucherEntry.PkVoucherNo = SalesVoucher.RequestSVNo;
            //if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    if (dbTVoucherEntry.DeleteTVoucherEntry(tVoucherEntry) == true)
            //    {
            //        OMMessageBox.Show("VoucherEntry Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            //        Form NewF = new SalesVoucher();
            //        this.Close();
            //        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            //    }
            //    else
            //    {
            //        OMMessageBox.Show("VoucherEntry not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            //    }

            //}
        }

        private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            long No = 0;

            if (type == 1)
            {
                No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                cntRow = 0;
                SalesVoucher.RequestSVNo = No;
            }
            else if (type == 2)
            {
                No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                cntRow = dtSearch.Rows.Count - 1;
                SalesVoucher.RequestSVNo = No;
            }
            else
            {
                if (type == 3)
                {
                    cntRow = cntRow + 1;
                }
                else if (type == 4)
                {
                    cntRow = cntRow - 1;
                }

                if (cntRow < 0)
                {
                    OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    cntRow = cntRow + 1;
                }
                else if (cntRow > dtSearch.Rows.Count - 1)
                {
                    OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    cntRow = cntRow - 1;
                }
                else
                {
                    No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    SalesVoucher.RequestSVNo = No;
                }

            }


            FillControls();

        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == SalesVoucher.RequestSVNo)
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
            btnDelete.Visible = flag;
            //GridRange.Height = 25;
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
                btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                btnLast_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                BtnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                BtnExit_Click(sender, e);
            }
        }
        #endregion


        private void dtpVoucherDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == 13)
            {
                GridView.Focus();
                GridView.Rows.Add();
                GridView.CurrentCell = GridView[1, 0];
                

            }
        }

        private void GridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            rowCount = GridView.CurrentCell.RowIndex;
            if (GridView.CurrentCell.ColumnIndex == 1)
            {
                ComboBox cmb = (ComboBox)e.Control;

                cmb.GotFocus += new EventHandler(cmbCrDR_GotFocus);
                cmb.KeyDown += new KeyEventHandler(cmbCrDR_KeyDown);
            }
            else if (GridView.CurrentCell.ColumnIndex == 2)
            {
                ComboBox cmb = (ComboBox)e.Control;

                cmb.GotFocus += new EventHandler(cmbParticulars_GotFocus);
                cmb.KeyDown +=new KeyEventHandler(cmbParticulars_KeyDown);
            }
            else if (GridView.CurrentCell.ColumnIndex == 3)
            {
                
                TextBox txt = (TextBox)e.Control;
                txt.KeyDown +=new KeyEventHandler(txtCrAmt_KeyDown);
            }
            GridView.CurrentCell.ErrorText = "";
        }

        public void cmbCrDR_GotFocus(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        public void cmbCrDR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                GridView.CurrentCell = GridView[2, GridView.CurrentCell.RowIndex];
            }
        }

        public void cmbParticulars_GotFocus(object sender, EventArgs e)
        {
            ((ComboBox)sender).DroppedDown = true;
        }

        public void cmbParticulars_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if(Convert.ToInt64(GridView.Rows[rowCount].Cells[1].Value) == 1)
                    GridView.CurrentCell = GridView[3, GridView.CurrentCell.RowIndex];
                else if (Convert.ToInt64(GridView.Rows[rowCount].Cells[1].Value) == 2)
                    GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex];
            }
        }

        public void txtCrAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                GridView.CurrentCell= GridView[1,GridView.NewRowIndex];
            }
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X && e.Control)
            {
                crTotal = 0; drTotal = 0;
                for (int i = 0; i < GridView.Rows.Count - 1; i++)
                {
                    if (Convert.IsDBNull(GridView.Rows[i].Cells[4].Value) == true)
                    {
                        GridView.Rows[i].Cells[4].Value = 0;
                    }
                    if (Convert.IsDBNull(GridView.Rows[i].Cells[3].Value) == true)
                    {
                        GridView.Rows[i].Cells[3].Value = 0;
                    }
                    crTotal = crTotal + Convert.ToInt64(GridView.Rows[i].Cells[3].Value);
                    drTotal = drTotal + Convert.ToInt64(GridView.Rows[i].Cells[4].Value);

                }
                if (crTotal != drTotal)
                {
                    OMMessageBox.Show("Debit Amount and Crdit Amount Are not Same", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    GridView.Focus();
                }
                else
                {
                    txtNarration.Focus();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (GridView.CurrentCell.ColumnIndex == 2)
                {
                    if (Convert.ToInt64(GridView.Rows[GridView.CurrentCell.RowIndex].Cells[1].Value) == 1)
                        GridView.CurrentCell = GridView[3, GridView.CurrentCell.RowIndex];
                    else if (Convert.ToInt64(GridView.Rows[GridView.CurrentCell.RowIndex].Cells[1].Value) == 2)
                        GridView.CurrentCell = GridView[4, GridView.CurrentCell.RowIndex];

                }
                else if (GridView.CurrentCell.ColumnIndex == 3 || GridView.CurrentCell.ColumnIndex == 4)
                {
                    if (GridView.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(GridView.CurrentCell.Value.ToString()) == false)
                            GridView.CurrentCell.ErrorText = "Please Enter Valid Amount";
                        else
                        {
                            GridView.Rows.Add();
                            GridView.CurrentCell = GridView[1, GridView.Rows.Count - 1];
                        }
                    }
                }

            }
        }
    }
}

