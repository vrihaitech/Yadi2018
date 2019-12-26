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

namespace Yadi.Master
{
    public partial class FooterDiscount : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMFooterDiscount dbMFooterDiscount = new DBMFooterDiscount();
        MFooterDiscount mFooterDiscount = new MFooterDiscount();
        MFooterDiscountDetails mFooterDiscountDetails = new MFooterDiscountDetails();
        MFooterDiscountPayTypeDetails mFooterDiscountPayTypeDetails = new MFooterDiscountPayTypeDetails();

        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        long ID = 0;
        int cntRow;//, ItemType = 0, iItemNameStartIndex = 3, rowQtyIndex = 0, 
        DateTime TempdtSchFrom, TempdtSchTo;

        public FooterDiscount()
        {
            InitializeComponent();
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void MFooterDiscount_Load(object sender, EventArgs e)
        {

            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                ObjFunction.FillCombo(cmbDiscType);
                DataTable dt = new DataTable();
                dt.Columns.Add("ID"); dt.Columns.Add("Desc");
                //DataRow dr = dt.NewRow();
                //dr[0] = "0";
                //dr[1] = " ------ Select ------ ";
                //dt.Rows.Add(dr);

                DataRow dr = dt.NewRow();
                dr[0] = "1";
                dr[1] = "Percent";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr[0] = "2";
                dr[1] = "Rupees";
                dt.Rows.Add(dr);

                cmbDiscType.DisplayMember = dt.Columns[1].ColumnName;
                cmbDiscType.ValueMember = dt.Columns[0].ColumnName;
                cmbDiscType.DataSource = dt;
                cmbDiscType.SelectedIndex = 0;
                lblSchemeStatus.Font = new Font("Verdana", 18, FontStyle.Bold);
                label1.Font = new Font("Verdana", 12, FontStyle.Bold);
                dtSearch = ObjFunction.GetDataView("Select PkSrNo From MFooterDiscount order by FooterDiscUserNo").Table;
                InitDelTable();
                if (dtSearch.Rows.Count > 0)
                {
                    if (FooterDiscoutSearch.RequestFooterDiscNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = FooterDiscoutSearch.RequestFooterDiscNo;
                    FillControls();
                    SetNavigation();
                }
                dgBillAmount.Columns[ColIndexBillAmt.BillAmount].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                dgBillAmount.Columns[ColIndexBillAmt.DiscAmount].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                dgBillAmount.Columns[ColIndexBillAmt.DiscPercentage].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                setDisplay(true);
                CheckChanged();
                cmbDiscType_Leave(sender, e);
                btnNew.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetSchemeStatus(int StatusCode)
        {
            try
            {
                if (StatusCode == 0)
                {
                    lblSchemeStatus.Text = "Draft";
                    lblSchemeStatus.ForeColor = Color.Blue;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                else if (StatusCode == 1)
                {
                    lblSchemeStatus.Text = "Active";
                    lblSchemeStatus.ForeColor = Color.Green;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = true;
                }
                else if (StatusCode == 2)
                {
                    lblSchemeStatus.Text = "Closed";
                    lblSchemeStatus.ForeColor = Color.Red;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CheckChanged()
        {
            if (chkSelectAll.Checked == true)
                chkSelectAll.Text = "DeSelecAll(F2)";
            else if (chkSelectAll.Checked == false)
                chkSelectAll.Text = "SelectAll(F2)";
        }

        public void FillControls()
        {
            try
            {
                EP.SetError(DtpDiscountDateFrom, "");
                EP.SetError(DtpDiscountDateTo, "");

                mFooterDiscount = dbMFooterDiscount.ModifyMFooterDiscountByID(ID);
                txtSchemeUserNo.Text = mFooterDiscount.FooterDiscUserNo.ToString();

                DtpDate.Enabled = false;

                DtpDiscountDateFrom.MinDate = mFooterDiscount.FooterDiscDate;
                DtpDate.Value = mFooterDiscount.FooterDiscDate;
                TempdtSchFrom = mFooterDiscount.PeriodFrom;
                TempdtSchTo = mFooterDiscount.PeriodTo;
                DtpDiscountDateFrom.Value = mFooterDiscount.PeriodFrom;
                DtpDiscountDateTo.Value = mFooterDiscount.PeriodTo;
                cmbDiscType.SelectedValue = mFooterDiscount.DiscType;
                cmbDiscType_Leave(null, null);
                SetSchemeStatus(mFooterDiscount.IsActive);
                
                BindGrid();
                dgBillAmount.Enabled = false;
                dgPayType.Enabled = false;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGrid()
        {
            try
            {
                while (dgBillAmount.Rows.Count > 0)
                    dgBillAmount.Rows.RemoveAt(0);

                string sql = " SELECT 0 AS SrNo, Cast(Amount As Numeric(18)), DiscPercentage, DiscAmount, PkSrNo, FooterDiscNo FROM MFooterDiscountDetails " +
                           " WHERE (FooterDiscNo = " + ID + ") " +
                           " ORDER BY Amount ";

                DataTable dt = ObjFunction.GetDataView(sql).Table;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgBillAmount.Rows.Add();
                    for (int j = 0; j < dgBillAmount.ColumnCount; j++)
                    {
                        dgBillAmount[j, i].Value = dt.Rows[i][j];
                    }
                }
                dgBillAmount.Rows.Add();
                FillPayType();
                dgBillAmount.CurrentCell = null;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillPayType()
        {
            while (dgPayType.Rows.Count > 0)
                dgPayType.Rows.RemoveAt(0);

            string sqlpayType = " SELECT 0 AS SrNo, MPayType.PayTypeName, ISNULL(MFooterDiscountPayTypeDetails.IsActive, 'False') AS 'Select', MPayType.PKPayTypeNo, ISNULL(MFooterDiscountPayTypeDetails.PkSrNo, 0) AS PkSrNo " +
                                 " FROM MPayType LEFT OUTER JOIN MFooterDiscountPayTypeDetails ON MPayType.PKPayTypeNo = MFooterDiscountPayTypeDetails.PayTypeNo AND  MFooterDiscountPayTypeDetails.FooterDiscNo = " + ID + " " +
                                 " WHERE (MPayType.PKPayTypeNo <> 1) AND (MPayType.IsActive = 'true') ";
            DataTable dtPayType = ObjFunction.GetDataView(sqlpayType).Table;

            for (int i = 0; i < dtPayType.Rows.Count; i++)
            {
                dgPayType.Rows.Add();
                for (int j = 0; j < dgPayType.ColumnCount; j++)
                {
                    dgPayType[j, i].Value = dtPayType.Rows[i][j];
                }
            }
        }


        public static class ColIndexBillAmt
        {
            public static int SrNo = 0;
            public static int BillAmount = 1;
            public static int DiscPercentage = 2;
            public static int DiscAmount = 3;
            public static int DtlsPkSrNo = 4;
            public static int PkSrNo = 5;
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {
                long No = 0;
                if (type == 5)
                {
                    if (dtSearch.Rows.Count > 0)
                    {
                        No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                    }
                    ID = No;
                }

                else if (type == 1)
                {
                    No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                    cntRow = 0;
                    ID = No;
                }
                else if (type == 2)
                {
                    No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    cntRow = dtSearch.Rows.Count - 1;
                    ID = No;
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
                        ID = No;
                    }

                }


                FillControls();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

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
            try
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
                else if (e.KeyCode == Keys.F3)
                {
                    if (BtnSave.Visible) BtnSave_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F2)
                {
                    if (btnNew.Visible == false)
                    {
                        chkSelectAll.Checked = !chkSelectAll.Checked;
                        CheckChanged();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
        #endregion

        #region Delete code

        public void InitDelTable()
        {
            dtDelete.Columns.Add();
            dtDelete.Columns.Add();
        }

        public void DeleteDtls(int Type, long PkNo)
        {
            DataRow dr = null;
            dr = dtDelete.NewRow();
            dr[0] = Type;
            dr[1] = PkNo;
            dtDelete.Rows.Add(dr);
        }

        public void DeleteValues()
        {
            try
            {
                if (dtDelete != null)
                {
                    for (int i = 0; i < dtDelete.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
                        {
                            mFooterDiscountDetails.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                            dbMFooterDiscount.DeleteMFooterDiscountDetails(mFooterDiscountDetails);
                        }
                    }
                    dtDelete.Rows.Clear();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #endregion

        private void DtpDiscountDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (validate_DtpDiscountDateFrom())
                    {
                        DtpDiscountDateTo.Focus();
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpDiscountDateFrom_Leave(object sender, EventArgs e)
        {
            validate_DtpDiscountDateFrom();
        }

        private void DtpDiscountDateFrom_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                DtpDiscountDateTo.MinDate = DtpDiscountDateFrom.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpDiscountDateTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    cmbDiscType.Focus();
                    //if (validate_DtpDiscountDateTo())
                    //{
                    //    e.SuppressKeyPress = true;
                    //    if (ID != 0)
                    //    {
                    //        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                    //        dgBillAmount.Focus();
                    //    }
                    //    else if (dgBillAmount.Rows.Count == 0)
                    //    {

                    //        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                    //        dgBillAmount.Focus();
                    //    }
                    //    else
                    //    {
                    //        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                    //        dgBillAmount.Focus();

                    //    }
                    //}
                    //else
                    //{
                    //    e.SuppressKeyPress = true;
                    //}
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpDiscountDateTo_Leave(object sender, EventArgs e)
        {
            if (validate_DtpDiscountDateTo())
            {
            }
        }

        private bool validate_DtpDiscountDateFrom()
        {
            EP.SetError(DtpDiscountDateFrom, "");
            try
            {
                if (TempdtSchFrom.ToString(Format.DDMMMYYYY) != DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY))
                {
                    string sqlQuery = "Select Count(*) from MFooterDiscount where PeriodFrom <= '" + DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY) +
                        "' And PeriodTo >= '" + DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY) + "'  And IsActive in (0,1) AND PkSrNo<>" + ID + "";
                    if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(DtpDiscountDateFrom, "Footer Discount already exist on selected date.");
                        EP.SetIconAlignment(DtpDiscountDateFrom, ErrorIconAlignment.MiddleRight);
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private bool validate_DtpDiscountDateTo()
        {
            string sqlQuery = "";
            EP.SetError(DtpDiscountDateTo, "");

            try
            {

                if (DtpDiscountDateFrom.Value.Date > DtpDiscountDateTo.Value.Date)
                {
                    EP.SetError(DtpDiscountDateTo, "Please select date after Footer Discount start date.");
                    EP.SetIconAlignment(DtpDiscountDateTo, ErrorIconAlignment.MiddleRight);
                    return false;
                }
                if (TempdtSchFrom.ToString(Format.DDMMMYYYY) != DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY))
                {
                    sqlQuery = "Select Count(*) from MFooterDiscount where (PeriodFrom <= '" + DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY) +
                        "' And PeriodTo >= '" + DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY) + "') And IsActive in (0,1) And PkSrNo <> " + ID;
                    if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(DtpDiscountDateTo, "Footer Discount already exist on selected date.");
                        EP.SetIconAlignment(DtpDiscountDateTo, ErrorIconAlignment.MiddleRight);
                        return false;
                    }
                }

                if (TempdtSchTo.ToString(Format.DDMMMYYYY) != DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY))
                {
                    sqlQuery = "Select Count(*) from MFooterDiscount where (PeriodFrom >= '" + DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY) +
                        "' And PeriodTo <= '" + DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY) + "') And IsActive in (0,1) And PkSrNo <> " + ID;
                    if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(DtpDiscountDateTo, "Footer Discount already exist on selected date.");
                        EP.SetIconAlignment(DtpDiscountDateTo, ErrorIconAlignment.MiddleRight);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        public void Calculator()
        {
            try
            {
                for (int i = 0; i < dgBillAmount.Rows.Count; i++)
                {
                    if (dgBillAmount.Rows[i].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() != "")
                    {
                        double BillAmt = Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.BillAmount].Value);

                        if (dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim() != "")
                        {
                            dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscAmount].Value = (BillAmt * (Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString()) / 100)).ToString("0.00");
                        }
                        else if (dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim() != "")
                        {
                            dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscPercentage].Value = (Convert.ToDouble((100 * Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscAmount].Value)) / BillAmt)).ToString("0.00");
                        }
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBillAmount_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MovetoNext move2n = new MovetoNext(m2n);
                if (ColIndexBillAmt.BillAmount == e.ColumnIndex)
                {

                    bool validFlag = true;
                    dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                    if (dgBillAmount.Rows.Count > (e.RowIndex + 1) &&
                        dgBillAmount.Rows[e.RowIndex + 1].Cells[ColIndexBillAmt.BillAmount].FormattedValue.Equals(""))
                    {
                        dgBillAmount.Rows.RemoveAt(e.RowIndex + 1);
                    }

                    if (dgBillAmount.Rows.Count > (e.RowIndex + 1))
                    {
                        dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                        if (Convert.ToDouble(dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].Value) >=
                            Convert.ToDouble(dgBillAmount.Rows[e.RowIndex + 1].Cells[ColIndexBillAmt.BillAmount].Value))
                        {
                            dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "Enter Amount greater than the previous Amount...";
                            dgBillAmount.CurrentCell = dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount];
                            dgBillAmount.Focus();
                            validFlag = false;
                            return;
                        }
                    }

                    if (e.RowIndex > 0)
                    {
                        dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                        if (Convert.ToDouble(dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].Value) <=
                            Convert.ToDouble(dgBillAmount.Rows[e.RowIndex - 1].Cells[ColIndexBillAmt.BillAmount].Value))
                        {
                            dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "Enter Amount greater than the previous Amount...";
                            dgBillAmount.CurrentCell = dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount];
                            dgBillAmount.Focus();
                            validFlag = false;
                        }
                    }

                    if (validFlag)
                    {

                        if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() == "")
                        {
                            dgBillAmount.CurrentCell = dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount];
                            dgBillAmount.Focus();
                        }
                        else if(ObjFunction.GetComboValue(cmbDiscType)==1)
                        {
                            dgBillAmount.CurrentCell = dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage];
                            dgBillAmount.Focus();
                        }
                        else if (ObjFunction.GetComboValue(cmbDiscType) == 2)
                        {
                            dgBillAmount.CurrentCell = dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount];
                            dgBillAmount.Focus();
                        }
                        //else
                        //{
                        //    dgBillAmount.CurrentCell = dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage];
                        //    dgBillAmount.Focus();
                        //}
                        if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DtlsPkSrNo].FormattedValue.ToString().Trim() == "")
                        {
                            dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DtlsPkSrNo].Value = 0;
                        }

                        if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString() != "" &&
                               dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString() != "")
                        {
                            if (dgBillAmount.Rows.Count - 1 == dgBillAmount.CurrentRow.Index)
                            {
                                dgBillAmount.Rows.Add();
                                if (ObjFunction.GetComboValue(cmbDiscType) == 1)
                                BeginInvoke(move2n, new object[] { e.RowIndex, ColIndexBillAmt.DiscPercentage, dgBillAmount });
                                else if (ObjFunction.GetComboValue(cmbDiscType) == 2)
                                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndexBillAmt.DiscAmount, dgBillAmount });
                                dgBillAmount.Focus();
                            }
                            else dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                        }
                        Calculator();
                    }

                    if (dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString() == "")
                    {
                        dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                        dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.DiscAmount].Value = ""; dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.DiscPercentage].Value = "";
                    }
                }
                else if (ColIndexBillAmt.DiscPercentage == e.ColumnIndex)
                {
                    if (dgBillAmount.Rows.Count > (e.RowIndex + 1) &&
                        dgBillAmount.Rows[e.RowIndex + 1].Cells[ColIndexBillAmt.BillAmount].FormattedValue.Equals(""))
                    {
                        dgBillAmount.Rows.RemoveAt(e.RowIndex + 1);
                    }
                    if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].ErrorText == "")
                    {
                        dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage].ErrorText = "";
                        if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() != "")
                        {

                            if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim() != "" &&
                                  Convert.ToDouble(dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim()) != 0)
                            {
                                dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].Value = (Convert.ToDouble(dgBillAmount.Rows[dgBillAmount.CurrentRow.Index].Cells[ColIndexBillAmt.BillAmount].Value) * (Convert.ToDouble(dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString()) / 100)).ToString("0.00");
                                if (dgBillAmount.Rows.Count - 1 == dgBillAmount.CurrentRow.Index)
                                {
                                    dgBillAmount.Rows.Add();
                                    //BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.DiscAmount, dgBillAmount });
                                    if (ObjFunction.GetComboValue(cmbDiscType) == 1)
                                        BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex+1, ColIndexBillAmt.BillAmount, dgBillAmount });
                                    dgBillAmount.Focus();
                                }
                                //else BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.DiscAmount, dgBillAmount });
                                else if (ObjFunction.GetComboValue(cmbDiscType) == 1)
                                {
                                    if (dgBillAmount.Rows.Count - 1 == dgBillAmount.CurrentRow.Index)
                                    {

                                        dgBillAmount.Rows.Add();
                                        dgBillAmount.Rows[dgBillAmount.CurrentCell.RowIndex + 1].Cells[ColIndexBillAmt.DtlsPkSrNo].Value = 0;
                                        BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex + 1, ColIndexBillAmt.BillAmount, dgBillAmount });
                                        dgBillAmount.Focus();
                                    }
                                    else
                                        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                                }
                                    //BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.BillAmount, dgBillAmount });
                            }
                            else
                            {
                                BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.DiscPercentage, dgBillAmount });
                                dgBillAmount.Focus();
                            }
                        }
                        else
                        {
                            BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.BillAmount, dgBillAmount });
                            dgBillAmount.Focus();
                        }
                    }
                    else
                    {
                        BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.BillAmount, dgBillAmount });
                        dgBillAmount.Focus();
                    }
                }
                else if (ColIndexBillAmt.DiscAmount == e.ColumnIndex)
                {
                    if (dgBillAmount.Rows.Count > (e.RowIndex + 1) &&
                       dgBillAmount.Rows[e.RowIndex + 1].Cells[ColIndexBillAmt.BillAmount].FormattedValue.Equals(""))
                    {
                        dgBillAmount.Rows.RemoveAt(e.RowIndex + 1);
                    }
                    if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].ErrorText == "")
                    {
                        dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].ErrorText = "";
                        if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString() != "")
                        {
                            if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim() != "" &&
                                 Convert.ToDouble(dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim()) != 0)
                            {
                                if ((Convert.ToDouble((100 * Convert.ToDouble(dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].Value)) / Convert.ToDouble(dgBillAmount.Rows[dgBillAmount.CurrentRow.Index].Cells[ColIndexBillAmt.BillAmount].Value))) > 100)
                                {
                                    dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].ErrorText = "Enter Discount Amount Less than Bill Amount";
                                    return;
                                }
                                else
                                {
                                    dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].ErrorText = "";
                                    dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage].Value = (Convert.ToDouble((100 * Convert.ToDouble(dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].Value)) / Convert.ToDouble(dgBillAmount.Rows[dgBillAmount.CurrentRow.Index].Cells[ColIndexBillAmt.BillAmount].Value))).ToString("0.00");
                                }
                            }
                            else
                            {
                                BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.DiscAmount, dgBillAmount });
                                dgBillAmount.Focus();
                            }
                            
                            if (dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString() != "" &&
                                dgBillAmount.Rows[e.RowIndex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString() != "")
                            {
                                if (dgBillAmount.Rows.Count - 1 == dgBillAmount.CurrentRow.Index)
                                {

                                    dgBillAmount.Rows.Add();
                                    dgBillAmount.Rows[dgBillAmount.CurrentCell.RowIndex + 1].Cells[ColIndexBillAmt.DtlsPkSrNo].Value = 0;
                                    BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex + 1, ColIndexBillAmt.BillAmount, dgBillAmount });
                                    dgBillAmount.Focus();
                                }
                                else dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];

                            }
                        }
                        else
                        {
                            BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.BillAmount, dgBillAmount });
                            dgBillAmount.Focus();
                        }
                    }
                    else
                    {
                        BeginInvoke(move2n, new object[] { dgBillAmount.CurrentCell.RowIndex, ColIndexBillAmt.BillAmount, dgBillAmount });
                        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, e.RowIndex];
                        dgBillAmount.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBillAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgBillAmount.Rows.Count > 0 && dgBillAmount.CurrentRow != null)
                {
                    if (e.KeyCode == Keys.Delete)
                    {
                        delete_row();
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        int rowInex = dgBillAmount.CurrentRow.Index;
                        if (ColIndexBillAmt.BillAmount == dgBillAmount.CurrentCell.ColumnIndex)
                        {
                            e.SuppressKeyPress = true;
                            dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                            bool validFlag = true;
                            if (dgBillAmount.Rows.Count > (rowInex + 1) &&
                                dgBillAmount.Rows[rowInex + 1].Cells[ColIndexBillAmt.BillAmount].FormattedValue.Equals(""))
                            {
                                dgBillAmount.Rows.RemoveAt(rowInex + 1);
                            }

                            if (dgBillAmount.Rows.Count > (rowInex + 1))
                            {
                                dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                                if (Convert.ToDouble(dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].Value) >=
                                    Convert.ToDouble(dgBillAmount.Rows[rowInex + 1].Cells[ColIndexBillAmt.BillAmount].Value))
                                {
                                    dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "Enter Amount greater than the previous Amount...";
                                    dgBillAmount.CurrentCell = dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount];
                                    dgBillAmount.Focus();
                                    validFlag = false;
                                }
                            }

                            if (rowInex > 0)
                            {
                                dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                                if (Convert.ToDouble(dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].Value) <=
                                    Convert.ToDouble(dgBillAmount.Rows[rowInex - 1].Cells[ColIndexBillAmt.BillAmount].Value))
                                {
                                    dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText = "Enter Amount greater than the previous Amount...";
                                    dgBillAmount.CurrentCell = dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount];
                                    dgBillAmount.Focus();
                                    validFlag = false;
                                }
                            }

                            if (validFlag)
                            {
                                if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() == "")
                                {
                                    dgBillAmount.CurrentCell = dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount];
                                    dgBillAmount.Focus();
                                }
                                else if (ObjFunction.GetComboValue(cmbDiscType) == 1)
                                {
                                    dgBillAmount.CurrentCell = dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage];
                                    dgBillAmount.Focus();
                                }
                                else if (ObjFunction.GetComboValue(cmbDiscType) == 2)
                                {
                                    
                                    dgBillAmount.CurrentCell = dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount];
                                    dgBillAmount.Focus();
                                
                                }

                                //else
                                //{
                                //    dgBillAmount.CurrentCell = dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage];
                                //    dgBillAmount.Focus();
                                //}
                            }
                            Calculator();
                            if (dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString() == "")
                            {
                                dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.BillAmount].ErrorText = "";
                                dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.DiscAmount].Value = "";
                                dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.DiscPercentage].ErrorText = "";
                            }
                        }
                        else if (ColIndexBillAmt.DiscPercentage == dgBillAmount.CurrentCell.ColumnIndex)
                        {
                            e.SuppressKeyPress = true;
                            if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText == "")
                            {
                                if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() != "")
                                {
                                    if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim() != "" &&
                                        Convert.ToDouble(dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim()) != 0)
                                    {
                                        dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].Value = (Convert.ToDouble(dgBillAmount.Rows[dgBillAmount.CurrentRow.Index].Cells[ColIndexBillAmt.BillAmount].Value) * (Convert.ToDouble(dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString()) / 100)).ToString("0.00");
                                       // dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.DiscAmount, rowInex];
                                        if (ObjFunction.GetComboValue(cmbDiscType) == 1)
                                        {
                                            if (dgBillAmount.Rows.Count - 1 == dgBillAmount.CurrentRow.Index)
                                            {

                                                dgBillAmount.Rows.Add();
                                                dgBillAmount.Rows[dgBillAmount.CurrentRow.Index + 1].Cells[ColIndexBillAmt.DtlsPkSrNo].Value = 0;
                                                dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.CurrentRow.Index + 1];
                                                dgBillAmount.Focus();
                                            }
                                        }
                                        else
                                        {
                                            dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].Value = (Convert.ToDouble(dgBillAmount.Rows[dgBillAmount.CurrentRow.Index].Cells[ColIndexBillAmt.BillAmount].Value) * (Convert.ToDouble(dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString()) / 100)).ToString("0.00");
                                             dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.DiscAmount, rowInex];
                                        }
                                        dgBillAmount.Focus();
                                    }
                                    else
                                    {
                                        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.DiscPercentage, rowInex];
                                        dgBillAmount.Focus();
                                    }
                                }
                                else
                                {
                                    dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, rowInex];
                                    dgBillAmount.Focus();
                                }
                            }
                            else
                            {
                                dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, rowInex];
                                dgBillAmount.Focus();
                            }
                        }
                        else if (ColIndexBillAmt.DiscAmount == dgBillAmount.CurrentCell.ColumnIndex)
                        {
                            e.SuppressKeyPress = true;
                            if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText == "" && dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].ErrorText=="")
                            {
                                if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() != "")
                                {
                                    if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim() != "" &&
                                        Convert.ToDouble(dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim()) != 0)
                                    {
                                        dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage].Value = (Convert.ToDouble((100 * Convert.ToDouble(dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].Value)) / Convert.ToDouble(dgBillAmount.Rows[dgBillAmount.CurrentRow.Index].Cells[ColIndexBillAmt.BillAmount].Value))).ToString("0.00");
                                    }
                                    else
                                    {
                                        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.DiscAmount, rowInex];
                                        dgBillAmount.Focus();
                                    }

                                    if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim() != "" &&
                                        dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim() != "")
                                    {
                                        if (dgBillAmount.Rows.Count - 1 == dgBillAmount.CurrentRow.Index)
                                        {

                                            dgBillAmount.Rows.Add();
                                            dgBillAmount.Rows[dgBillAmount.CurrentRow.Index + 1].Cells[ColIndexBillAmt.DtlsPkSrNo].Value = 0;
                                            dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.CurrentRow.Index + 1];
                                            dgBillAmount.Focus();
                                        }
                                        else dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                                    }
                                }
                                else
                                {
                                    dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, rowInex];
                                    dgBillAmount.Focus();
                                }
                            }
                            else
                            {
                                if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.BillAmount].ErrorText != "")
                                {
                                    dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, rowInex];
                                }
                                else if (dgBillAmount.Rows[rowInex].Cells[ColIndexBillAmt.DiscAmount].ErrorText != "")
                                {
                                    dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.DiscAmount, rowInex];
                                }
                                dgBillAmount.Focus();
                            }
                        }
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        e.SuppressKeyPress = true;

                        if (dgBillAmount.Rows[dgBillAmount.Rows.Count - 1].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() == "")
                        {
                            dgPayType.CurrentCell = dgPayType[2, 0];
                            dgPayType.Focus();
                        }
                        else
                        {
                            dgBillAmount.CurrentCell = dgBillAmount[dgBillAmount.CurrentCell.ColumnIndex, dgBillAmount.CurrentRow.Index];
                            dgBillAmount.Focus();
                        }

                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBillAmount_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgBillAmount.CurrentCell.ColumnIndex == ColIndexBillAmt.DiscPercentage)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtPer_TextChanged);
                }
                if (dgBillAmount.CurrentCell.ColumnIndex == ColIndexBillAmt.DiscAmount)
                {
                    TextBox txtAmt = (TextBox)e.Control;
                    txtAmt.TextChanged += new EventHandler(txtDiscAmount_TextChanged);
                }
                if (dgBillAmount.CurrentCell.ColumnIndex == ColIndexBillAmt.BillAmount)
                {
                    TextBox txtrate = (TextBox)e.Control;
                    txtrate.TextChanged += new EventHandler(txtBillAmount_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBillAmount_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColIndexBillAmt.SrNo)
                {
                    e.Value = e.RowIndex + 1;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtPer_TextChanged(object sender, EventArgs e)
        {
            if (dgBillAmount.CurrentCell.ColumnIndex == ColIndexBillAmt.DiscPercentage)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void txtDiscAmount_TextChanged(object sender, EventArgs e)
        {
            if (dgBillAmount.CurrentCell.ColumnIndex == ColIndexBillAmt.DiscAmount)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 5, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void txtBillAmount_TextChanged(object sender, EventArgs e)
        {
            if (dgBillAmount.CurrentCell.ColumnIndex == ColIndexBillAmt.BillAmount)
            {
                ObjFunction.SetMasked((TextBox)sender, -1, 8, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            FooterDiscoutSearch.RequestFooterDiscNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                dgBillAmount.Enabled = true;
                dgPayType.Enabled = true;
                ID = 0;
                InitControls();
                cmbDiscType.SelectedValue = 1;
                dgBillAmount.Rows.Add();
                dgBillAmount.CurrentCell = null;
                txtSchemeUserNo.Text = ObjQry.ReturnString("Select isNull(Max(FooterDiscUserNo),0)+1 From MFooterDiscount", CommonFunctions.ConStr);
                DtpDiscountDateFrom.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void InitControls()
        {
            try
            {
                DtpDiscountDateFrom.MinDate = Convert.ToDateTime("01-Jan-1900");
                DtpDiscountDateTo.MinDate = Convert.ToDateTime("01-Jan-1900");
                TempdtSchFrom = Convert.ToDateTime("01-Jan-1900");
                TempdtSchTo = Convert.ToDateTime("01-Jan-1900");
                DtpDate.Value = DBGetVal.ServerTime;
                DtpDiscountDateFrom.Value = DBGetVal.ServerTime;
                DtpDiscountDateTo.Value = DBGetVal.ServerTime;
                DtpDiscountDateFrom.MinDate = DtpDate.Value;
                DtpDate.Enabled = false;
                chkSelectAll.Checked = false;
                CheckChanged();
                while (dgBillAmount.Rows.Count > 0)
                    dgBillAmount.Rows.RemoveAt(0);
                FillPayType();
                SetSchemeStatus(0);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(DtpDiscountDateFrom, "");
                EP.SetError(DtpDiscountDateTo, "");
                chkSelectAll.Checked = false;
                CheckChanged();
                ObjFunction.LockButtons(true, this.Controls);
                NavigationDisplay(5);
                dgBillAmount.Enabled = false;
                ObjFunction.LockControls(false, this.Controls);
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validation()
        {
            bool flag = true;

            try
            {

                if (!validate_DtpDiscountDateFrom() || !validate_DtpDiscountDateTo())
                {
                    flag = false;
                    return false;
                }
                else
                {
                    if (dgBillAmount.Rows.Count <= 1)
                    {
                        OMMessageBox.Show("Atleast one Footer Discount required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                        dgBillAmount.Focus();
                        return false;
                    }

                    for (int i = 0; i < dgBillAmount.Rows.Count; i++)
                    {
                        if (dgBillAmount.Rows[i].Cells[ColIndexBillAmt.BillAmount].ErrorText != "" || dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscAmount].ErrorText != "" || dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscPercentage].ErrorText != "")
                        {
                            OMMessageBox.Show("Please Enter valid Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, i];
                            dgBillAmount.Focus();
                            return false;
                        }
                    }
                    for (int i = 0; i < dgBillAmount.Rows.Count; i++)
                    {
                        if (i == dgBillAmount.Rows.Count - 1)
                        {
                            if (dgBillAmount.Rows[i].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() != "")
                            {
                                OMMessageBox.Show("Please Enter Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                dgBillAmount.CurrentCell = dgBillAmount[dgBillAmount.CurrentCell.ColumnIndex, i];
                                dgBillAmount.Focus();
                                return false;
                            }
                        }
                        else if (dgBillAmount.Rows[i].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim() == "" ||
                      Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.BillAmount].FormattedValue.ToString().Trim()) == 0
                            || dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim() == "" ||
                      Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscAmount].FormattedValue.ToString().Trim()) == 0
                           || dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim() == "" ||
                   Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscPercentage].FormattedValue.ToString().Trim()) == 0)
                        {
                            OMMessageBox.Show("Please Enter Amount", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dgBillAmount.CurrentCell = dgBillAmount[dgBillAmount.CurrentCell.ColumnIndex, i];
                            dgBillAmount.Focus();
                            return false;
                        }
                    }
                    int cntrow = 0;
                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(Convert.ToBoolean(dgPayType.Rows[i].Cells[2].EditedFormattedValue)) == true)
                        {
                            cntrow = 1;
                            break;
                        }
                    }
                    if (cntrow == 0)
                    {
                        flag = false;
                        OMMessageBox.Show("Please select atleast one paytype...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        if (dgPayType.Rows.Count > 0)
                            dgPayType.CurrentCell = dgPayType[2, 0];
                        dgPayType.Focus();
                    }
                }

                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        public void SetValue()
        {
            try
            {
                if (Validation())
                {
                    

                    mFooterDiscountDetails = new MFooterDiscountDetails();
                    DeleteValues();

                    dbMFooterDiscount = new DBMFooterDiscount();
                    mFooterDiscount = new MFooterDiscount();

                    mFooterDiscount.PkSrNo = ID;
                    mFooterDiscount.FooterDiscDate = DtpDate.Value.Date;
                    mFooterDiscount.FooterDiscUserNo = Convert.ToInt64(txtSchemeUserNo.Text);
                    mFooterDiscount.PeriodFrom = DtpDiscountDateFrom.Value.Date;
                    mFooterDiscount.PeriodTo = DtpDiscountDateTo.Value.Date;
                    if (ID == 0) mFooterDiscount.IsActive = 0;
                    else
                    {
                        if (lblSchemeStatus.Text == "Draft") mFooterDiscount.IsActive = 0;
                        else if (lblSchemeStatus.Text == "Active") mFooterDiscount.IsActive = 1;
                        else if (lblSchemeStatus.Text == "Closed") mFooterDiscount.IsActive = 2;
                    }
                    
                    mFooterDiscount.DiscType = ObjFunction.GetComboValue(cmbDiscType);
                    mFooterDiscount.CompanyNo = DBGetVal.FirmNo;
                    mFooterDiscount.UserDate = DBGetVal.ServerTime;
                    mFooterDiscount.UserID = DBGetVal.FirmNo;
                    dbMFooterDiscount.AddMFooterDiscount(mFooterDiscount);

                    for (int i = 0; i < dgPayType.Rows.Count; i++)
                    {
                        mFooterDiscountPayTypeDetails = new MFooterDiscountPayTypeDetails();

                        mFooterDiscountPayTypeDetails.PkSrNo = Convert.ToInt64(dgPayType.Rows[i].Cells[4].Value);
                        mFooterDiscountPayTypeDetails.PayTypeNo = Convert.ToInt64(dgPayType.Rows[i].Cells[3].Value);
                        mFooterDiscountPayTypeDetails.IsActive = Convert.ToBoolean(dgPayType.Rows[i].Cells[2].EditedFormattedValue);
                        mFooterDiscountPayTypeDetails.CompanyNo = DBGetVal.FirmNo;
                        dbMFooterDiscount.AddMFooterDiscountPayTypeDetails(mFooterDiscountPayTypeDetails);
                    }

                    for (int i = 0; i < dgBillAmount.Rows.Count - 1; i++)
                    {
                        mFooterDiscountDetails = new MFooterDiscountDetails();
                        mFooterDiscountDetails.PkSrNo = (dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DtlsPkSrNo].FormattedValue.ToString().Trim() == "") ? 0 : Convert.ToInt64(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DtlsPkSrNo].Value);
                        mFooterDiscountDetails.Amount = Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.BillAmount].Value);
                        mFooterDiscountDetails.DiscPercentage = Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscPercentage].Value);
                        mFooterDiscountDetails.DiscAmount = Convert.ToDouble(dgBillAmount.Rows[i].Cells[ColIndexBillAmt.DiscAmount].Value);
                        mFooterDiscountDetails.UserID = DBGetVal.UserID;
                        mFooterDiscountDetails.UserDate = DBGetVal.ServerTime;
                        mFooterDiscountDetails.CompanyNo = DBGetVal.FirmNo;
                        dbMFooterDiscount.AddMFooterDiscountDetails(mFooterDiscountDetails);
                    }
                    long tempID = dbMFooterDiscount.ExecuteNonQueryStatements();
                    if (tempID != 0)
                    {
                        OMMessageBox.Show("Bill Footer Discount Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        if (ID == 0)
                        {
                            DataRow drSearch = dtSearch.NewRow();
                            drSearch[0] = tempID;
                            dtSearch.Rows.Add(drSearch);
                        }
                        ID = tempID;
                        SetNavigation();
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        FillControls();
                        btnNew.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("Bill Footer Discount Not Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void delete_row()
        {
            try
            {
                if (dgBillAmount.Rows[dgBillAmount.CurrentCell.RowIndex].Cells[ColIndexBillAmt.BillAmount].Value != null)
                {
                    if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        long DtailsPkSrNo = Convert.ToInt64(dgBillAmount.Rows[dgBillAmount.CurrentCell.RowIndex].Cells[ColIndexBillAmt.DtlsPkSrNo].FormattedValue);
                        if (DtailsPkSrNo != 0)
                        {
                            DeleteDtls(1, DtailsPkSrNo);
                        }
                        if (dgBillAmount.Rows.Count - 1 == dgBillAmount.CurrentCell.RowIndex)
                        {
                            dgBillAmount.Rows.RemoveAt(dgBillAmount.CurrentCell.RowIndex);
                            dgBillAmount.Rows.Add();
                        }
                        else
                            dgBillAmount.Rows.RemoveAt(dgBillAmount.CurrentCell.RowIndex);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void DtpDate_ValueChanged(object sender, EventArgs e)
        {
            DtpDiscountDateFrom.MinDate = DtpDate.Value;
        }

        private void lblSchemeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string strMessage = "";
                if (ID != 0)
                {
                    dbMFooterDiscount = new DBMFooterDiscount();
                    mFooterDiscount = new MFooterDiscount();
                    mFooterDiscount.PkSrNo = ID;
                    if (lblSchemeStatus.Text == "Draft")
                    {
                        if (DtpDiscountDateTo.Value.Date < DBGetVal.ServerTime.Date)
                        {
                            OMMessageBox.Show("Bill Footer Discount period is already expired" + Environment.NewLine + " Not Allowed Bill Footer Discount Active...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            return;
                        }
                        if (OMMessageBox.Show("Are you sure want to Active Bill Footer Discount?" + strMessage, CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (DBGetVal.ServerTime.Date >= DtpDiscountDateFrom.Value.Date)
                                DtpDiscountDateFrom.Value = DBGetVal.ServerTime.Date;
                            lblSchemeStatus.Text = "Active";
                            BtnSave_Click(sender, e);
                        }
                    }
                    else if (lblSchemeStatus.Text == "Active")
                    {
                        if (OMMessageBox.Show("Are you sure want to Closed Bill Footer Discount?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (DtpDiscountDateTo.Value.Date > DBGetVal.ServerTime.Date)
                            {
                                if (DBGetVal.ServerTime.Date >= DtpDiscountDateFrom.Value.Date)
                                {
                                    DtpDiscountDateTo.Value = DBGetVal.ServerTime.Date;
                                    //DtpDiscountDateFrom.Value = DBGetVal.ServerTime.Date.AddDays(1);
                                }
                            }
                            lblSchemeStatus.Text = "Closed";
                            BtnSave_Click(sender, e);
                        }
                    }
                    else if (lblSchemeStatus.Text == "Closed")
                    {
                        //if (DtpDiscountDateTo.Value.Date < DBGetVal.ServerTime.Date)
                        //{
                        //    OMMessageBox.Show("Bill Footer Discount period is already expired" + Environment.NewLine + " Not Allowed Bill Footer Active...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //    return;
                        //}
                        //if (OMMessageBox.Show("Are you sure want to Active Bill Footer Discount?" + strMessage, CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        //    DtpDiscountDateFrom.Value = DBGetVal.ServerTime.Date;
                        //    lblSchemeStatus.Text = "Active";
                        //    BtnSave_Click(sender, e);

                        //}
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                DtpDate.Enabled = false;
                dgBillAmount.Enabled = true;
                dgPayType.Enabled = true;
                DtpDiscountDateFrom.MinDate = DtpDate.Value;
                DtpDiscountDateFrom.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbMFooterDiscount = new DBMFooterDiscount();
                mFooterDiscount = new MFooterDiscount();

                mFooterDiscount.PkSrNo = ID;
                mFooterDiscount.IsActive = 2;
                if (OMMessageBox.Show("Are you sure want to Closed this Footer Discount?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //if (dbMFooterDiscount.DeleteMFooterDiscount(mFooterDiscount) == true)
                    //{
                    //    OMMessageBox.Show("Bill Footer Discount Closed Successfully.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //    FillControls();
                    //}
                    //else
                    //{
                    //    OMMessageBox.Show("Bill Footer Discount not Closed.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    //}
                    if (DtpDiscountDateTo.Value.Date > DBGetVal.ServerTime.Date)
                    {
                        if (DBGetVal.ServerTime.Date >= DtpDiscountDateFrom.Value.Date)
                        {
                            DtpDiscountDateTo.Value = DBGetVal.ServerTime.Date;
                            //DtpDiscountDateFrom.Value = DBGetVal.ServerTime.Date.AddDays(1);
                        }
                    }
                    lblSchemeStatus.Text = "Closed";
                    BtnSave_Click(sender, e);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPayType_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void dgPayType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnSave.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }

        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgPayType.Rows.Count; i++)
                {
                    dgPayType.Rows[i].Cells[2].Value = chkSelectAll.Checked;
                }
                CheckChanged();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbDiscType_Leave(object sender, EventArgs e)
        {
            if (ObjFunction.GetComboValue(cmbDiscType) == 1)
            {
                dgBillAmount.Columns[ColIndexBillAmt.DiscPercentage].ReadOnly = false;
                dgBillAmount.Columns[ColIndexBillAmt.DiscAmount].ReadOnly = true;
            }
            if (ObjFunction.GetComboValue(cmbDiscType) == 2)
            {
                dgBillAmount.Columns[ColIndexBillAmt.DiscPercentage].ReadOnly = true;
                dgBillAmount.Columns[ColIndexBillAmt.DiscAmount].ReadOnly = false;
            }
        }

        private void cmbDiscType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (ObjFunction.GetComboValue(cmbDiscType) > 0)
                {
                    if (validate_DtpDiscountDateTo())
                    {
                        e.SuppressKeyPress = true;
                        if (ID != 0)
                        {
                            dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                            dgBillAmount.Focus();
                        }
                        else if (dgBillAmount.Rows.Count == 0)
                        {

                            dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                            dgBillAmount.Focus();
                        }
                        else
                        {
                            dgBillAmount.CurrentCell = dgBillAmount[ColIndexBillAmt.BillAmount, dgBillAmount.Rows.Count - 1];
                            dgBillAmount.Focus();

                        }
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new FooterDiscoutSearch();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

    }
}
