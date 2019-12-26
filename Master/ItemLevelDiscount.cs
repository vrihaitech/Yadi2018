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
    public partial class ItemLevelDiscount : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions(); 
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMBillDiscount dbMBillDiscount = new DBMBillDiscount();
        MItemDiscount mItemDiscount = new MItemDiscount();
        MItemBrandDiscount mItemBrandDiscount = new MItemBrandDiscount();
        MItemDiscountDetails mItemDiscountDetails = new MItemDiscountDetails();


        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtMain = new DataTable();
        DataTable dtItem = new DataTable();
        DataTablesCollection dtBillCollect = new DataTablesCollection();


        long ID = 0;
        int cntRow;//, ItemType = 0;rowQtyIndex = 0, iItemNameStartIndex = 3, 
        long OptionType = 0;
        DateTime TempdtSchFrom, TempdtSchTo;
        //int h = 0;

        int Postion = 0;

        public ItemLevelDiscount()
        {
            InitializeComponent();
        }

        public ItemLevelDiscount(long optionType)
        {
            InitializeComponent();
            this.OptionType = optionType;
            //OptionType = 1 -> Show MfgCompnay
            //OptionType = 0 -> Hide MfgCompany
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void ItemLevelDiscount_Load(object sender, EventArgs e)
        {
            try
            {
                if (OptionType == 0)
                {
                    label2.Visible = false;
                    cmbMfgCompany.Visible = false;

                    label5.Location = label2.Location;
                    DtpDate.Location = cmbMfgCompany.Location;
                }

                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                ObjFunction.FillCombo(cmbMfgCompany, "SELECT MfgCompNo, MfgCompName FROM MManufacturerCompany WHERE (IsActive = 'true') AND (CompanyNo = " + DBGetVal.FirmNo + ") ORDER BY MfgCompName");

                lblWait.Font = new Font("Verdana", 18, FontStyle.Bold);
                lblWait.ForeColor = Color.Green;

                lblSchemeStatus.Font = new Font("Verdana", 18, FontStyle.Bold);

                label1.Font = new Font("Verdana", 12, FontStyle.Bold);
                dtSearch = ObjFunction.GetDataView("SELECT PkSrNo FROM MItemDiscount ORDER BY DiscUserNo").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (ItemLevelDiscountSearch.RequestDiscNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = ItemLevelDiscountSearch.RequestDiscNo;

                    FillControls();
                    SetNavigation();
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

        public void FillControls()
        {
            try
            {


                EP.SetError(DtpDiscountDateFrom, "");
                EP.SetError(DtpDiscountDateTo, "");
                EP.SetError(txtDiscount, "");

                mItemDiscount = dbMBillDiscount.ModifyMItemDiscountByID(ID);

                txtDiscUserNo.Text = mItemDiscount.DiscUserNo.ToString();
                DtpDate.Enabled = false;
                DtpDiscountDateFrom.MinDate = mItemDiscount.DiscDate;
                DtpDate.Value = mItemDiscount.DiscDate;
                TempdtSchFrom = mItemDiscount.PeriodFrom;
                TempdtSchTo = mItemDiscount.PeriodTo;

                ObjFunction.FillCombo(cmbMfgCompany, "SELECT MfgCompNo, MfgCompName FROM MManufacturerCompany WHERE (IsActive = 'true' or MfgCompNo=" + mItemDiscount.MfgCompanyNo + ") AND (CompanyNo = " + DBGetVal.FirmNo + ") ORDER BY MfgCompName");
                cmbMfgCompany.SelectedValue = mItemDiscount.MfgCompanyNo;

                DtpDiscountDateFrom.Value = mItemDiscount.PeriodFrom;
                DtpDiscountDateTo.Value = mItemDiscount.PeriodTo;
                SetSchemeStatus(mItemDiscount.IsActive);
                BindBrand();
                dgBrand.Enabled = false;
                //dgBrand.CurrentCell = dgBrand.CurrentCell;
                dgBrand.Focus();
                txtDiscount.Text = "";
                cmbMfgCompany.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public static class ColIndexBrand
        {
            public static int SrNo = 0;
            public static int BrandName = 1;
            public static int Disc = 2;
            public static int Select = 3;
            public static int StockGroupNo = 4;
            public static int BrandDiscNo = 5;

        }

        public static class ColIndexItem
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int MRP = 2;
            public static int Disc = 3;
            public static int Select = 4;
            public static int StockGroupNo = 5;
            public static int PkSrNo = 6;
            public static int ItemNo = 7;
            public static int FkRateSettingNo = 8;

        }

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(700);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayMessageForWait(bool flag)
        {

            try
            {
                lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                
                //this.lblWait.Enabled = true;
                //if (h == 100)
                //    h = 0;
                //else
                //    h = h + 1;

                //if (h == 1)
                //    lblWait.Text = "Processing .";
                //else if (h == 0 || h == 30 || h == 60 || h == 90)
                //    lblWait.Text = "Processing .";
                //else if (h == 10 || h == 40 || h == 70 || h == 100)
                //    lblWait.Text = "Processing ..";
                //else if (h == 20 || h == 50 || h == 80)
                //    lblWait.Text = "Processing ...";
                lblWait.Text = "Processing ...";
                Application.DoEvents();
                lblWait.Visible = flag;
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

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
                    if (validate_DtpDiscountDateTo())
                    {
                        e.SuppressKeyPress = true;
                        txtDiscount.Focus();
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
                    string sqlQuery = "Select Count(*) from MItemDiscount where PeriodFrom <= '" + DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY) +
                        "' And PeriodTo >= '" + DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY) + "' " +
                        (OptionType == 1 ? " And MfgCompanyNo=" + ObjFunction.GetComboValue(cmbMfgCompany) : " ") +
                        " And IsActive in (0,1) AND PkSrNo<>" + ID + "";
                    if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(DtpDiscountDateFrom, "Item Level Discount already exist on selected date.");
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
                    EP.SetError(DtpDiscountDateTo, "Please select date after Item Level Discount start date.");
                    EP.SetIconAlignment(DtpDiscountDateTo, ErrorIconAlignment.MiddleRight);
                    return false;
                }
                if (TempdtSchFrom.ToString(Format.DDMMMYYYY) != DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY))
                {
                    sqlQuery = "Select Count(*) from MItemDiscount where (PeriodFrom <= '" + DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY) +
                        "' And PeriodTo >= '" + DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY) + "') " + 
                        (OptionType == 1 ? " And MfgCompanyNo=" + ObjFunction.GetComboValue(cmbMfgCompany) : " ") + 
                        " And IsActive in (0,1) And PkSrNo <> " + ID;
                    if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(DtpDiscountDateTo, "Item Level Discount already exist on selected date.");
                        EP.SetIconAlignment(DtpDiscountDateTo, ErrorIconAlignment.MiddleRight);
                        return false;
                    }
                }

                if (TempdtSchTo.ToString(Format.DDMMMYYYY) != DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY))
                {
                    sqlQuery = "Select Count(*) from MItemDiscount where (PeriodFrom >= '" + DtpDiscountDateFrom.Value.ToString(Format.DDMMMYYYY) +
                        "' And PeriodTo <= '" + DtpDiscountDateTo.Value.ToString(Format.DDMMMYYYY) + "') " + 
                        (OptionType == 1 ? " And MfgCompanyNo=" + ObjFunction.GetComboValue(cmbMfgCompany) : " ") + 
                        " And IsActive in (0,1) And PkSrNo <> " + ID;
                    if (ObjQry.ReturnInteger(sqlQuery, CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(DtpDiscountDateTo, "Item Level Discount already exist on selected date.");
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

                if (ID != 0)
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
            btnPrev.Focus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
            btnNext.Focus();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
            btnLast.Focus();
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
                else if (e.KeyCode == Keys.F2)
                {
                    if (BtnSave.Visible) BtnSave_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F5)
                {
                    if (btnApply.Enabled) btnApply_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F6)
                {
                    if (btnItemCancel.Enabled) btnItemCancel_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F7)
                {
                    if (BtnSave.Visible)
                    {
                        chkBrand.Checked = !chkBrand.Checked;
                        chkBrand_CheckedChanged(sender, new EventArgs());
                    }
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (BtnSave.Visible)
                    {
                        chkItemLevel.Checked = !chkItemLevel.Checked;
                        chkItemLevel_CheckedChanged(sender, new EventArgs());
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
        #endregion

        //#region Delete code

        //public void InitDelTable()
        //{
        //    dtDelete.Columns.Add();
        //    dtDelete.Columns.Add();
        //}

        //public void DeleteDtls(int Type, long PkNo)
        //{
        //    DataRow dr = null;
        //    dr = dtDelete.NewRow();
        //    dr[0] = Type;
        //    dr[1] = PkNo;
        //    dtDelete.Rows.Add(dr);
        //}

        //public void DeleteValues()
        //{
        //    try
        //    {
        //        if (dtDelete != null)
        //        {
        //            for (int i = 0; i < dtDelete.Rows.Count; i++)
        //            {
        //                if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
        //                {
        //                    mFooterDiscountDetails.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
        //                    dbMFooterDiscount.DeleteMFooterDiscountDetails(mFooterDiscountDetails);
        //                }
        //            }
        //            dtDelete.Rows.Clear();
        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}


        //#endregion

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    EP.SetError(txtDiscount, "");
                    e.SuppressKeyPress = true;
                    if (txtDiscount.Text.Trim() != "")
                    {

                        SetDiscValue(dgBrand, Convert.ToDouble(txtDiscount.Text), ColIndexBrand.Disc);
                        AddCollectionData();
                        dgBrand.CurrentCell = dgBrand[ColIndexBrand.Select, 0];
                        dgBrand.Focus();
                    }
                    else
                    {
                        dgBrand.CurrentCell = dgBrand[ColIndexBrand.Select, 0];
                        dgBrand.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
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
                dgBrand.Enabled = true;
                dgItemMaster.Enabled = true;
                ID = 0;
                InitControls();
                txtDiscUserNo.Text = ObjQry.ReturnString("Select isNull(Max(DiscUserNo),0)+1 From MItemDiscount Where CompanyNo=" + DBGetVal.FirmNo + " ", CommonFunctions.ConStr);
                if (OptionType == 1)
                {
                    cmbMfgCompany.Focus();
                }
                else
                {
                    BindBrand();
                    DtpDiscountDateFrom.Focus();
                }
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
                EP.SetError(txtDiscount, "");

                ObjFunction.LockButtons(true, this.Controls);
                InitControls();
                NavigationDisplay(5);
                dgBrand.Enabled = false;
                dgItemMaster.Enabled = false;
                ObjFunction.LockControls(false, this.Controls);
                btnNew.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            ItemLevelDiscountSearch.RequestDiscNo = 0;
            this.Close();
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
                txtDiscount.Text = "";
                DtpDiscountDateFrom.Value = DBGetVal.ServerTime;
                DtpDiscountDateTo.Value = DBGetVal.ServerTime;
                DtpDiscountDateFrom.MinDate = DtpDate.Value;
                DtpDate.Enabled = false;
                while (dgBrand.Rows.Count > 0)
                    dgBrand.Rows.RemoveAt(0);
                while (dgItemMaster.Rows.Count > 0)
                    dgItemMaster.Rows.RemoveAt(0);
                SetSchemeStatus(0);
                cmbMfgCompany.Enabled = true;
                cmbMfgCompany.SelectedIndex = 0;
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

        private void cmbMfgCompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (ObjFunction.GetComboValue(cmbMfgCompany) != 0)
                    {
                        e.SuppressKeyPress = true;

                        if (BindBrand() == false)
                        {
                            DisplayMessage("MFG Company Brand Not Found...");
                            cmbMfgCompany.Focus();
                        }
                        else
                        {
                            dgBrand.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                            DtpDiscountDateFrom.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbMfgCompany_Leave(object sender, EventArgs e)
        {

        }

        //public bool BindBrand()
        //{
        //    try
        //    {
        //        Application.DoEvents();
        //        this.Cursor = Cursors.WaitCursor;
        //        this.Enabled = false;

        //        while (dgBrand.Rows.Count > 0)
        //            dgBrand.Rows.RemoveAt(0);
        //        while (dgItemMaster.Rows.Count > 0)
        //            dgItemMaster.Rows.RemoveAt(0);

        //        dtBillCollect = new DataTablesCollection();

        //        string sql = " SELECT 0 AS SrNo, MItemGroup.StockGroupName,IsNull(DiscPercentage,0) AS Disc, (CASE WHEN (" + ID + " = 0) THEN 'True' ELSE MItemBrandDiscount.IsActive END) AS 'Select',isNull(MItemGroup.StockGroupNo,0) As 'StockGroupNo',isNull(MItemBrandDiscount.PkSrNo,0) As 'MItemBrandDiscount' " +
        //                   " FROM MStockGroup LEFT OUTER JOIN MItemBrandDiscount ONmItemGroup.ItemGroupNo  =  MItemBrandDiscount.StockGroupNo  And (MItemBrandDiscount.ItemDiscNo = " + ID + ") " +
        //                   " Where MItemGroup.MfgCompNo=" + ObjFunction.GetComboValue(cmbMfgCompany) + " And  MItemGroup.IsActive='true' And  MItemGroup.CompanyNo=" + DBGetVal.FirmNo + "  " +
        //                   "ORDER BY MItemGroup.StockGroupName ";

        //        DataTable dt = ObjFunction.GetDataView(sql).Table;

        //        if (dt.Rows.Count == 0)
        //            return false;

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            dgBrand.Rows.Add();
        //            for (int j = 0; j < dgBrand.ColumnCount; j++)
        //            {
        //                dgBrand[j, i].Value = dt.Rows[i][j];
        //                if (ID != 0)
        //                {
        //                    if (j == ColIndexBrand.StockGroupNo)
        //                    {
        //                        string sqlItem = " SELECT  0 AS SrNo, mItemMaster.ItemName, GetItemRateAll_1.MRP, ISNULL(MItemDiscountDetails.DiscPercentage, 0) AS Disc,MItemDiscountDetails.IsActive AS 'Select',MStockItems.GroupNo, ISNULL(MItemDiscountDetails.PkSrNo, 0) AS 'PkSrNo', mItemMaster.ItemNo, GetItemRateAll_1.PkSrNo AS 'FkRateSettings' " +
        //                                         " FROM MItemDiscountDetails RIGHT OUTER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, " + dgBrand.Rows[i].Cells[j].Value + ") AS GetItemRateAll_1 INNER JOIN MStockItems ON GetItemRateAll_1.ItemNo = mItemMaster.ItemNo ON MItemDiscountDetails.ItemNo = mItemMaster.ItemNo And MItemDiscountDetails.ItemBrandDiscNo=" + Convert.ToInt64(dt.Rows[i].ItemArray[ColIndexBrand.BrandDiscNo].ToString()) + " " +
        //                                         " WHERE (MStockItems.GroupNo=" + dgBrand.Rows[i].Cells[j].Value + ") And  mItemMaster.IsActive='true' And  mItemMaster.CompanyNo=" + DBGetVal.FirmNo + "  " +
        //                                         " ORDER BY mItemMaster.ItemName ";
        //                        DataTable dtCol = ObjFunction.GetDataView(sqlItem).Table;
        //                        dtBillCollect.Add(dtCol);
        //                    }
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //        return false;
        //    }
        //    finally
        //    {
        //        this.Enabled = true;
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        public bool BindBrand()
        {
            try
            {
                Application.DoEvents();
                //pnlMain.Enabled = false;
                DisplayMessageForWait(true);

                while (dgBrand.Rows.Count > 0)
                    dgBrand.Rows.RemoveAt(0);
                while (dgItemMaster.Rows.Count > 0)
                    dgItemMaster.Rows.RemoveAt(0);
                string sql = "";
                if (ID == 0)
                {
                    sql = "SELECT Distinct 0 AS SrNo, MItemGroup.StockGroupName,0.0 AS Disc, 'true' AS 'Select', ISNULL(MItemGroup.StockGroupNo, 0) AS 'StockGroupNo', 0 AS 'MItemBrandDiscount' " +
                        " FROM MStockGroup INNER JOIN MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo " +
                        " WHERE " + 
                        (OptionType == 1 ? " mItemMaster.MfgCompNo=" + ObjFunction.GetComboValue(cmbMfgCompany) + " AND " : " ") + 
                        " (MItemGroup.IsActive = 'true') AND (MItemGroup.ControlGroup = 3) AND (MItemGroup.CompanyNo=" + DBGetVal.FirmNo + ") " +
                        " ORDER BY MItemGroup.StockGroupName";
                }
                else
                {
                    sql = " SELECT Distinct 0 AS SrNo, MItemGroup.StockGroupName,IsNull(DiscPercentage,0) AS Disc, (CASE WHEN (" + ID + " = 0) THEN 'true' ELSE isnull(MItemBrandDiscount.IsActive,'false') END) AS 'Select',isNull(MItemGroup.StockGroupNo,0) As 'StockGroupNo',isNull(MItemBrandDiscount.PkSrNo,0) As 'MItemBrandDiscount' " +
                           " FROM MStockGroup INNER JOIN MItemBrandDiscount ONmItemGroup.ItemGroupNo  =  MItemBrandDiscount.StockGroupNo INNER JOIN MStockItems ON MItemGroup.StockGroupNo=MStockItems.GroupNo   " +
                           " Where " + 
                           (OptionType == 1 ? " mItemMaster.MfgCompNo=" + ObjFunction.GetComboValue(cmbMfgCompany) + " AND " : " ") + 
                           " MItemGroup.IsActive='true' AND (MItemGroup.ControlGroup = 3) And  MItemGroup.CompanyNo=" + DBGetVal.FirmNo + "  " +
                           " AND (MItemBrandDiscount.ItemDiscNo = "+ ID +") ORDER BY MItemGroup.StockGroupName ";
                }
                //string sql = " SELECT 0 AS SrNo, MItemGroup.StockGroupName,IsNull(DiscPercentage,0) AS Disc, (CASE WHEN (" + ID + " = 0) THEN 'true' ELSE isnull(MItemBrandDiscount.IsActive,'false') END) AS 'Select',isNull(MItemGroup.StockGroupNo,0) As 'StockGroupNo',isNull(MItemBrandDiscount.PkSrNo,0) As 'MItemBrandDiscount' " +
                //           " FROM MStockGroup LEFT OUTER JOIN MItemBrandDiscount ONmItemGroup.ItemGroupNo  =  MItemBrandDiscount.StockGroupNo  And (MItemBrandDiscount.ItemDiscNo = " + ID + ") " +
                //           " Where MItemGroup.MfgCompNo=" + ObjFunction.GetComboValue(cmbMfgCompany) + " And  MItemGroup.IsActive='true' AND (MItemGroup.ControlGroup = 3) And  MItemGroup.CompanyNo=" + DBGetVal.FirmNo + "  " +
                //           "ORDER BY MItemGroup.StockGroupName ";

                DataTable dt = ObjFunction.GetDataView(sql).Table;

                if (dt.Rows.Count == 0)
                    return false;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgBrand.Rows.Add();
                    for (int j = 0; j < dgBrand.Columns.Count; j++)
                    {
                        dgBrand.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                    }
                }
               // pnlMain.Enabled = false;
                //dgBrand.Enabled = false;
                //dgBrand.Enabled = true;
                
                this.Cursor = Cursors.WaitCursor;
               // Application.DoEvents();
                string sqlItem = "";
                if (ID == 0)
                {
                    sqlItem = "SELECT 0 AS SrNo, mItemMaster.ItemName, GetItemRateAll_1.MRP, 0.0 AS Disc, 'True' AS 'Select', mItemMaster.GroupNo, 0 AS 'PkSrNo', mItemMaster.ItemNo,  " +
                        " GetItemRateAll_1.PkSrNo AS 'FkRateSettings' FROM MStockGroup INNER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS GetItemRateAll_1 INNER JOIN MStockItems ON GetItemRateAll_1.ItemNo = mItemMaster.ItemNo ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo " +
                        " WHERE (MStockItems.IsActive = 'true') AND (MStockItems.CompanyNo = " + DBGetVal.FirmNo + ") " +
                        (OptionType == 1 ? " AND (MStockItems.MfgCompNo = " + ObjFunction.GetComboValue(cmbMfgCompany) + " ) " : " ") +
                        " ORDER BY MItemGroup.StockGroupNo, mItemMaster.ItemName ";
                }
                else
                {
                    sqlItem = "SELECT     0 AS SrNo, mItemMaster.ItemName, MItemDiscountDetails.MRP, ISNULL(MItemDiscountDetails.DiscPercentage, 0) AS Disc, (ISNull(MItemDiscountDetails.IsActive, 'False')) AS 'Select', mItemMaster.GroupNo, ISNULL(MItemDiscountDetails.PkSrNo, 0) AS 'PkSrNo',  " +
                        " mItemMaster.ItemNo, GetItemRateAll_1.PkSrNo AS 'FkRateSettings' FROM MItemBrandDiscount INNER JOIN MItemDiscountDetails ON MItemBrandDiscount.PkSrNo = MItemDiscountDetails.ItemBrandDiscNo INNER JOIN " +
                        " MItemDiscount ON MItemBrandDiscount.ItemDiscNo = MItemDiscount.PkSrNo INNER JOIN MStockGroup INNER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS GetItemRateAll_1 INNER JOIN " +
                        " MStockItems ON GetItemRateAll_1.ItemNo = mItemMaster.ItemNo ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo ON MItemDiscountDetails.ItemNo = mItemMaster.ItemNo AND MItemDiscountDetails.MRP = GetItemRateAll_1.MRP " +
                        " WHERE     (MStockItems.IsActive = 'true') AND (MStockItems.CompanyNo = " + DBGetVal.FirmNo + ") " +
                        (OptionType == 1 ? " AND (MStockItems.MfgCompNo = " + ObjFunction.GetComboValue(cmbMfgCompany) + " ) " : " ") +
                        " AND (MItemDiscount.PkSrNo = " + ID + ") " +
                        " ORDER BY MItemGroup.StockGroupNo, mItemMaster.ItemName";
                }
                //string sqlItem = " SELECT 0 AS SrNo, mItemMaster.ItemName, GetItemRateAll_1.MRP, ISNULL(MItemDiscountDetails.DiscPercentage, 0) AS Disc, (CASE WHEN (" + ID + " = 0) THEN 'True' ELSE ISNull(MItemDiscountDetails.IsActive,'False') END)  AS 'Select', mItemMaster.GroupNo, ISNULL(MItemDiscountDetails.PkSrNo, 0) AS 'PkSrNo', mItemMaster.ItemNo, GetItemRateAll_1.PkSrNo AS 'FkRateSettings' " +
                //               " FROM MStockGroup INNER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, NULL) AS GetItemRateAll_1 INNER JOIN MStockItems ON GetItemRateAll_1.ItemNo = mItemMaster.ItemNo ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo LEFT OUTER JOIN MItemDiscountDetails ON mItemMaster.ItemNo = MItemDiscountDetails.ItemNo " +
                //               " WHERE (MStockItems.IsActive = 'true') AND (MStockItems.CompanyNo = " + DBGetVal.FirmNo + ") AND (MItemGroup.MfgCompNo = " + ObjFunction.GetComboValue(cmbMfgCompany) + " ) " +
                //               " ORDER BY MItemGroup.StockGroupNo, mItemMaster.ItemName ";
                dtItem = ObjFunction.GetDataView(sqlItem).Table;
                return true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
            finally
            {
                //this.Enabled = true;
                DisplayMessageForWait(false);
                pnlMain.Enabled = true;
                dgBrand.Enabled = false;
                dgBrand.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        //public void AddCollectionData()
        //{
        //    try
        //    {
        //        this.Enabled = false;
        //        this.Cursor = Cursors.WaitCursor;
        //        Application.DoEvents();

        //        dtBillCollect = new DataTablesCollection();
        //        Application.DoEvents();
        //        for (int i = 0; i < dgBrand.Rows.Count; i++)
        //        {
        //            DisplayMessageForWait(true);
        //            Application.DoEvents();
        //            string sqlItem = " SELECT  0 AS SrNo, mItemMaster.ItemName, GetItemRateAll_1.MRP,(isnull(" + Convert.ToDouble(txtDiscount.Text) + ",0)) AS Disc, (CASE WHEN (" + ID + " = 0) THEN 'True' ELSE MItemDiscountDetails.IsActive END) AS 'Select',MStockItems.GroupNo, ISNULL(MItemDiscountDetails.PkSrNo, 0) AS 'PkSrNo', mItemMaster.ItemNo, GetItemRateAll_1.PkSrNo AS 'FkRateSettings' " +
        //                                        " FROM MItemDiscountDetails RIGHT OUTER JOIN dbo.GetItemRateAll(NULL, NULL, NULL, NULL, NULL, " + dgBrand.Rows[i].Cells[ColIndexBrand.StockGroupNo].Value + ") AS GetItemRateAll_1 INNER JOIN MStockItems ON GetItemRateAll_1.ItemNo = mItemMaster.ItemNo ON MItemDiscountDetails.ItemNo = mItemMaster.ItemNo And  MItemDiscountDetails.ItemBrandDiscNo=" + Convert.ToInt64(dgBrand.Rows[i].Cells[ColIndexBrand.BrandDiscNo].Value) + "  " +
        //                                        " WHERE (MStockItems.GroupNo=" + dgBrand.Rows[i].Cells[ColIndexBrand.StockGroupNo].Value + ") And  mItemMaster.IsActive='true' And  mItemMaster.CompanyNo=" + DBGetVal.FirmNo + "   " +
        //                                        " ORDER BY mItemMaster.ItemName ";
        //            DataTable dtCol = ObjFunction.GetDataView(sqlItem).Table;
        //            dtBillCollect.Add(dtCol);
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //    finally
        //    {
        //        DisplayMessageForWait(false);
        //        this.Enabled = true;
        //        this.Cursor = Cursors.Default;
        //    }
        //}

        public void AddCollectionData()
        {
            try
            {
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    dtItem.Rows[i][ColIndexItem.Disc] = txtDiscount.Text;
                }
                dtItem.AcceptChanges();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            finally
            {
            }
        }

        public void SetDiscValue(DataGridView dg, double discValue, int ColIndex)
        {
            try
            {
                for (int i = 0; i < dg.Rows.Count; i++)
                {
                    dg.Rows[i].Cells[ColIndex].Value = discValue;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    e.SuppressKeyPress = true;
                    Postion = dgBrand.CurrentRow.Index;
                    if (BindItemMaster(Postion))
                    {
                        if (BtnSave.Visible == false) return;
                        dgBrand.Enabled = false;
                        dgItemMaster.Enabled = true;
                        btnApply.Enabled = true;
                        btnItemCancel.Enabled = true;
                        dgItemMaster.CurrentCell = dgItemMaster.Rows[0].Cells[ColIndexItem.Select];
                        dgItemMaster.Focus();
                    }
                    else
                    {
                        btnApply.Enabled = false;
                        btnItemCancel.Enabled = false;
                        DisplayMessage("Item Not Found...");
                        dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[ColIndexBrand.Select];
                        dgBrand.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    BtnSave.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        //public bool BindItemMaster(int dtPostion)
        //{
        //    try
        //    {
        //        while (dgItemMaster.Rows.Count > 0)
        //            dgItemMaster.Rows.RemoveAt(0);

        //        DataTable dt = dtBillCollect[dtPostion];
        //        if (dt.Rows.Count == 0)
        //            return false;

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            dgItemMaster.Rows.Add();
        //            for (int j = 0; j < dgItemMaster.ColumnCount; j++)
        //            {
        //                dgItemMaster[j, i].Value = dt.Rows[i][j];
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //        return false;
        //    }
        //}

        public bool BindItemMaster(int dtPostion)
        {
            try
            {
                while (dgItemMaster.Rows.Count > 0)
                    dgItemMaster.Rows.RemoveAt(0);

                dgItemMaster.Enabled = true;

                DataRow[] dtrow = dtItem.Select("GroupNo = " + dgBrand.Rows[Postion].Cells[ColIndexBrand.StockGroupNo].Value.ToString());

                if (dtrow.Length == 0)
                    return false;

                foreach (DataRow row in dtrow)
                {
                    dgItemMaster.Rows.Add();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.Disc].Value = row[ColIndexItem.Disc].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.FkRateSettingNo].Value = row[ColIndexItem.FkRateSettingNo].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.ItemName].Value = row[ColIndexItem.ItemName].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.ItemNo].Value = row[ColIndexItem.ItemNo].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.MRP].Value = row[ColIndexItem.MRP].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.PkSrNo].Value = row[ColIndexItem.PkSrNo].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.Select].Value = row[ColIndexItem.Select].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.SrNo].Value = row[ColIndexItem.SrNo].ToString();
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.StockGroupNo].Value = row[ColIndexItem.StockGroupNo].ToString();
                }


                return true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void dgBrand_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgBrand.CurrentCell.ColumnIndex == ColIndexBrand.Disc)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtPer_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtPer_TextChanged(object sender, EventArgs e)
        {
            if (dgBrand.CurrentCell.ColumnIndex == ColIndexBrand.Disc)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void dgItemMaster_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgItemMaster.CurrentCell.ColumnIndex == ColIndexItem.Disc)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(txtPer1_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtPer1_TextChanged(object sender, EventArgs e)
        {
            if (dgItemMaster.CurrentCell.ColumnIndex == ColIndexItem.Disc)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
            }
        }

        //private void dgBrand_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ColumnIndex == ColIndexBrand.Disc)
        //        {
        //            MovetoNext move2n = new MovetoNext(m2n);

        //            int pos = dgBrand.CurrentRow.Index;
        //            DataTable dt = dtBillCollect[pos];
        //            if (dt.Rows.Count == 0)
        //                return;
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                dt.Rows[i][ColIndexItem.Disc] = dgBrand.CurrentRow.Cells[ColIndexBrand.Disc].Value;
        //            }
        //            dt.AcceptChanges();
        //            dtBillCollect.RemoveAt(pos);
        //            dtBillCollect.Insert(pos, dt);
        //            BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex, ColIndexBrand.Disc, dgBrand });
        //            dgBrand.Focus();

        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        public void AddDisc(long StockGroupNo, string disc)
        {
            try
            {
                DataRow[] drRows = dtItem.Select("GroupNo = " + StockGroupNo);
                if (drRows.Length == 0)
                    return;

                foreach (DataRow row in drRows)
                {

                    DataRow dr = dtItem.NewRow();
                    dr[ColIndexItem.SrNo] = 0;
                    dr[ColIndexItem.Disc] = disc;
                    dr[ColIndexItem.FkRateSettingNo] = row[ColIndexItem.FkRateSettingNo].ToString();
                    dr[ColIndexItem.ItemName] = row[ColIndexItem.ItemName].ToString();
                    dr[ColIndexItem.ItemNo] = row[ColIndexItem.ItemNo].ToString();
                    dr[ColIndexItem.MRP] = row[ColIndexItem.MRP].ToString();
                    dr[ColIndexItem.PkSrNo] = row[ColIndexItem.PkSrNo].ToString();
                    dr[ColIndexItem.Select] = row[ColIndexItem.Select].ToString();
                    dr[ColIndexItem.StockGroupNo] = row[ColIndexItem.StockGroupNo].ToString();

                    dtItem.Rows.Remove(row);
                    dtItem.Rows.Add(dr);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBrand_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColIndexBrand.Disc)
                {
                    if (dgBrand.Rows[e.RowIndex].Cells[ColIndexBrand.Disc].EditedFormattedValue.ToString() == "")
                        dgBrand.Rows[e.RowIndex].Cells[ColIndexBrand.Disc].Value = "0";

                    MovetoNext move2n = new MovetoNext(m2n);
                    AddDisc(Convert.ToInt64(dgBrand.Rows[e.RowIndex].Cells[ColIndexBrand.StockGroupNo].Value), dgBrand.Rows[e.RowIndex].Cells[ColIndexBrand.Disc].Value.ToString());
                    BeginInvoke(move2n, new object[] { dgBrand.CurrentCell.RowIndex, ColIndexBrand.Disc, dgBrand });
                    dgBrand.Focus();

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {

                DataRow[] drRows = dtItem.Select("GroupNo = " + dgBrand.Rows[Postion].Cells[ColIndexBrand.StockGroupNo].Value.ToString());
                if (drRows.Length == 0)
                    return;

                foreach (DataRow row in drRows)
                {
                    dtItem.Rows.Remove(row);
                }

                for (int i = 0; i < dgItemMaster.Rows.Count; i++)
                {
                    DataRow dr = dtItem.NewRow();
                    dr[ColIndexItem.SrNo] = 0;
                    dr[ColIndexItem.Disc] = dgItemMaster.Rows[i].Cells[ColIndexItem.Disc].EditedFormattedValue;
                    dr[ColIndexItem.FkRateSettingNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.FkRateSettingNo].Value;
                    dr[ColIndexItem.ItemName] = dgItemMaster.Rows[i].Cells[ColIndexItem.ItemName].Value;
                    dr[ColIndexItem.ItemNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.ItemNo].Value;
                    dr[ColIndexItem.MRP] = dgItemMaster.Rows[i].Cells[ColIndexItem.MRP].Value;
                    dr[ColIndexItem.PkSrNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.PkSrNo].Value;
                    dr[ColIndexItem.Select] = dgItemMaster.Rows[i].Cells[ColIndexItem.Select].EditedFormattedValue;
                    dr[ColIndexItem.StockGroupNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.StockGroupNo].Value;
                    dtItem.Rows.Add(dr);
                }
                btnItemCancel_Click(sender, new EventArgs());

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        //private void btnApply_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = dtBillCollect[Postion];
        //        if (dt.Rows.Count == 0)
        //            return;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            dt.Rows[i][ColIndexItem.Disc] = dgItemMaster.Rows[i].Cells[ColIndexItem.Disc].EditedFormattedValue;
        //            dt.Rows[i][ColIndexItem.Select] = dgItemMaster.Rows[i].Cells[ColIndexItem.Select].EditedFormattedValue;
        //        }
        //        dt.AcceptChanges();
        //        dtBillCollect.RemoveAt(Postion);
        //        dtBillCollect.Insert(Postion, dt);
        //        btnItemCancel_Click(sender, new EventArgs());
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        private void btnItemCancel_Click(object sender, EventArgs e)
        {
            try
            {
                while (dgItemMaster.Rows.Count > 0)
                    dgItemMaster.Rows.RemoveAt(0);
                btnItemCancel.Enabled = false;
                btnApply.Enabled = false;
                dgBrand.Enabled = true;
                dgItemMaster.Enabled = false;
                dgBrand.CurrentCell = dgBrand.CurrentCell;
                dgBrand.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgItemMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    btnApply.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    dgItemMaster.CurrentCell = dgItemMaster.CurrentCell;
                    dgItemMaster.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validation()
        {
            bool flag = false;
            try
            {
                if (OptionType == 1 && ObjFunction.GetComboValue(cmbMfgCompany) == 0)
                {
                    OMMessageBox.Show("Please Select Company Name.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    cmbMfgCompany.Focus();
                    return false;
                }
                else if (!validate_DtpDiscountDateFrom() || !validate_DtpDiscountDateTo())
                {
                    flag = false;
                    return false;
                }
                else if (btnApply.Enabled == true)
                {
                    OMMessageBox.Show("Please Click Apply Changes button..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    btnApply.Focus();
                    flag = false;
                    return false;
                }
                else
                    flag = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
            return flag;
        }

        //public void SetValue()
        //{
        //    try
        //    {
        //        if (Validation())
        //        {
        //            if (dgBrand.Rows.Count <= 1)
        //            {
        //                OMMessageBox.Show("Atleast one Brand Name required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        //                cmbMfgCompany.Focus();
        //                return;
        //            }

        //            dbMBillDiscount = new DBMBillDiscount();

        //            mItemDiscount = new MItemDiscount();
        //            mItemDiscount.PkSrNo = ID;
        //            mItemDiscount.DiscUserNo = Convert.ToInt64(txtDiscUserNo.Text);
        //            mItemDiscount.DiscDate = DtpDate.Value.Date;
        //            mItemDiscount.PeriodFrom = DtpDiscountDateFrom.Value.Date;
        //            mItemDiscount.PeriodTo = DtpDiscountDateTo.Value.Date;
        //            mItemDiscount.MfgCompanyNo = ObjFunction.GetComboValue(cmbMfgCompany);
        //            if (ID == 0) mItemDiscount.IsActive = 0;
        //            else
        //            {
        //                if (lblSchemeStatus.Text == "Draft") mItemDiscount.IsActive = 0;
        //                else if (lblSchemeStatus.Text == "Active") mItemDiscount.IsActive = 1;
        //                else if (lblSchemeStatus.Text == "Closed") mItemDiscount.IsActive = 2;
        //            }
        //            mItemDiscount.CompanyNo = DBGetVal.FirmNo;
        //            mItemDiscount.UserID = DBGetVal.UserID;
        //            mItemDiscount.UserDate = DBGetVal.ServerTime;
        //            dbMBillDiscount.AddMItemDiscount(mItemDiscount);

        //            for (int i = 0; i < dgBrand.Rows.Count; i++)
        //            {
        //                mItemBrandDiscount = new MItemBrandDiscount();

        //                mItemBrandDiscount.PkSrNo = Convert.ToInt64(dgBrand.Rows[i].Cells[ColIndexBrand.BrandDiscNo].Value);
        //                mItemBrandDiscount.DiscPercentage = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndexBrand.Disc].Value);
        //                mItemBrandDiscount.StockGroupNo = Convert.ToInt64(dgBrand.Rows[i].Cells[ColIndexBrand.StockGroupNo].Value);
        //                mItemBrandDiscount.IsActive = Convert.ToBoolean(dgBrand.Rows[i].Cells[ColIndexBrand.Select].Value);
        //                mItemBrandDiscount.CompanyNo = DBGetVal.FirmNo;
        //                dbMBillDiscount.AddMItemBrandDiscount(mItemBrandDiscount);

        //                DataTable dt = dtBillCollect[i];
        //                for (int j = 0; j < dt.Rows.Count; j++)
        //                {
        //                    mItemDiscountDetails = new MItemDiscountDetails();
        //                    mItemDiscountDetails.PkSrNo = Convert.ToInt64(dt.Rows[j].ItemArray[ColIndexItem.PkSrNo].ToString());
        //                    mItemDiscountDetails.DiscPercentage = Convert.ToDouble(dt.Rows[j].ItemArray[ColIndexItem.Disc].ToString());
        //                    mItemDiscountDetails.ItemNo = Convert.ToInt64(dt.Rows[j].ItemArray[ColIndexItem.ItemNo].ToString());
        //                    mItemDiscountDetails.FkRateSettingNo = Convert.ToInt64(dt.Rows[j].ItemArray[ColIndexItem.FkRateSettingNo].ToString());
        //                    if (mItemBrandDiscount.IsActive == false)
        //                        mItemDiscountDetails.IsActive = false;
        //                    else
        //                        mItemDiscountDetails.IsActive = Convert.ToBoolean(dt.Rows[j].ItemArray[ColIndexItem.Select].ToString());
        //                    mItemDiscountDetails.CompanyNo = DBGetVal.FirmNo;
        //                    dbMBillDiscount.AddMItemDiscountDetails(mItemDiscountDetails);
        //                }
        //            }

        //            long tempID = dbMBillDiscount.ExecuteNonQueryStatements();
        //            if (tempID != 0)
        //            {
        //                OMMessageBox.Show("Item Level Discount Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
        //                if (ID == 0)
        //                {
        //                    DataRow drSearch = dtSearch.NewRow();
        //                    drSearch[0] = tempID;
        //                    dtSearch.Rows.Add(drSearch);
        //                }
        //                ID = tempID;
        //                SetNavigation();
        //                ObjFunction.LockButtons(true, this.Controls);
        //                ObjFunction.LockControls(false, this.Controls);
        //                FillControls();
        //                btnNew.Focus();
        //            }
        //            else
        //            {
        //                OMMessageBox.Show("Item Level Discount Not Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        //            }
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        public void SetValue()
        {
            try
            {
                Application.DoEvents();
                //pnlMain.Enabled = false;
                DisplayMessageForWait(true);
                //this.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                if (Validation())
                {
                    if (dgBrand.Rows.Count <= 0)
                    {
                        OMMessageBox.Show("Atleast one Brand Name required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        if (OptionType == 1)
                        {
                            cmbMfgCompany.Focus();
                        }
                        else
                        {
                            DtpDiscountDateFrom.Focus();
                        }
                        return;
                    }

                    dbMBillDiscount = new DBMBillDiscount();
                    mItemDiscount = new MItemDiscount();
                    mItemDiscount.PkSrNo = ID;
                    mItemDiscount.DiscUserNo = Convert.ToInt64(txtDiscUserNo.Text);
                    mItemDiscount.DiscDate = DtpDate.Value.Date;
                    mItemDiscount.PeriodFrom = DtpDiscountDateFrom.Value.Date;
                    mItemDiscount.PeriodTo = DtpDiscountDateTo.Value.Date;

                    if (OptionType == 0)
                        mItemDiscount.MfgCompanyNo = 0;
                    else
                        mItemDiscount.MfgCompanyNo = ObjFunction.GetComboValue(cmbMfgCompany);

                    if (ID == 0) mItemDiscount.IsActive = 0;
                    else
                    {
                        if (lblSchemeStatus.Text == "Draft") mItemDiscount.IsActive = 0;
                        else if (lblSchemeStatus.Text == "Active") mItemDiscount.IsActive = 1;
                        else if (lblSchemeStatus.Text == "Closed") mItemDiscount.IsActive = 2;
                    }
                    mItemDiscount.CompanyNo = DBGetVal.FirmNo;
                    mItemDiscount.UserID = DBGetVal.UserID;
                    mItemDiscount.UserDate = DBGetVal.ServerTime;
                    dbMBillDiscount.AddMItemDiscount(mItemDiscount);

                    for (int i = 0; i < dgBrand.Rows.Count; i++)
                    {
                        mItemBrandDiscount = new MItemBrandDiscount();
                        mItemBrandDiscount.PkSrNo = Convert.ToInt64(dgBrand.Rows[i].Cells[ColIndexBrand.BrandDiscNo].Value);
                        mItemBrandDiscount.DiscPercentage = Convert.ToDouble(dgBrand.Rows[i].Cells[ColIndexBrand.Disc].Value);
                        mItemBrandDiscount.StockGroupNo = Convert.ToInt64(dgBrand.Rows[i].Cells[ColIndexBrand.StockGroupNo].Value);
                        mItemBrandDiscount.IsActive = Convert.ToBoolean(dgBrand.Rows[i].Cells[ColIndexBrand.Select].Value);
                        mItemBrandDiscount.CompanyNo = DBGetVal.FirmNo;
                        dbMBillDiscount.AddMItemBrandDiscount(mItemBrandDiscount);

                        DataRow[] drSave = dtItem.Select("GroupNo = " + dgBrand.Rows[i].Cells[ColIndexBrand.StockGroupNo].Value.ToString());

                        foreach (DataRow row in drSave)
                        {
                            mItemDiscountDetails = new MItemDiscountDetails();
                            mItemDiscountDetails.PkSrNo = Convert.ToInt64(row[ColIndexItem.PkSrNo].ToString());
                            mItemDiscountDetails.DiscPercentage = Convert.ToDouble(row[ColIndexItem.Disc].ToString());
                            mItemDiscountDetails.MRP = Convert.ToDouble(row[ColIndexItem.MRP].ToString());
                            mItemDiscountDetails.ItemNo = Convert.ToInt64(row[ColIndexItem.ItemNo].ToString());
                            mItemDiscountDetails.FkRateSettingNo = Convert.ToInt64(row[ColIndexItem.FkRateSettingNo].ToString());
                            if (mItemBrandDiscount.IsActive == false)
                                mItemDiscountDetails.IsActive = false;
                            else
                                mItemDiscountDetails.IsActive = Convert.ToBoolean(row[ColIndexItem.Select].ToString());
                            mItemDiscountDetails.CompanyNo = DBGetVal.FirmNo;
                            dbMBillDiscount.AddMItemDiscountDetails(mItemDiscountDetails);
                        }
                    }

                    long tempID = dbMBillDiscount.ExecuteNonQueryStatements();
                    if (tempID != 0)
                    {
                        OMMessageBox.Show("Item Level Discount Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
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
                        OMMessageBox.Show("Item Level Discount Not Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            finally
            {
                pnlMain.Enabled = true;
                //this.Enabled = true;
                DisplayMessageForWait(false);
                this.Cursor = Cursors.Default;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                DtpDate.Enabled = false;
                dgBrand.Enabled = true;
                DtpDiscountDateFrom.MinDate = DtpDate.Value;
                dgBrand.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                cmbMfgCompany.Enabled = false;
                DtpDiscountDateFrom.Focus();

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBrand_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndexBrand.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void dgItemMaster_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndexItem.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbMBillDiscount = new DBMBillDiscount();
                mItemDiscount = new MItemDiscount();

                mItemDiscount.PkSrNo = ID;
                mItemDiscount.IsActive = 2;
                if (OMMessageBox.Show("Are you sure want to Closed this Item Level Discount?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //if (dbMBillDiscount.DeleteMItemDiscount(mItemDiscount) == true)
                    //{
                    //    OMMessageBox.Show("Item Level Discount Closed Successfully.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //    FillControls();
                    //}
                    //else
                    //{
                    //    OMMessageBox.Show("Item Level Discount not Closed.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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

        private void lblSchemeStatus_Click(object sender, EventArgs e)
        {
            try
            {
                string strMessage = "";
                if (ID != 0)
                {
                    dbMBillDiscount = new DBMBillDiscount();
                    mItemDiscount = new MItemDiscount();

                    mItemDiscount.PkSrNo = ID;
                    if (lblSchemeStatus.Text == "Draft")
                    {
                        if (DtpDiscountDateTo.Value.Date < DBGetVal.ServerTime.Date)
                        {
                            OMMessageBox.Show("Item Level Discount period is already expired" + Environment.NewLine + " Not Allowed Item Level Discount Active...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            return;
                        }
                        if (OMMessageBox.Show("Are you sure want to Active Item Level Discount ?" + strMessage, CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (DBGetVal.ServerTime.Date >= DtpDiscountDateFrom.Value.Date)
                                DtpDiscountDateFrom.Value = DBGetVal.ServerTime.Date;
                            lblSchemeStatus.Text = "Active";
                            BtnSave_Click(sender, e);
                        }
                    }
                    else if (lblSchemeStatus.Text == "Active")
                    {
                        if (OMMessageBox.Show("Are you sure want to Closed Item Level Discount?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
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
                        //    OMMessageBox.Show("Item Level Discount period is already expired" + Environment.NewLine + " Not Allowed Item Level Discount Active...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //    return;
                        //}
                        //if (OMMessageBox.Show("Are you sure want to Active Item Level Discount?" + strMessage, CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
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

        private void dgItemMaster_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndexItem.Disc)
            {
                if (dgItemMaster.Rows[e.RowIndex].Cells[ColIndexItem.Disc].EditedFormattedValue.ToString() == "")
                    dgItemMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "0";
            }

        }

        private void chkBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (dgBrand.Rows.Count > 0)
            {
                for (int i = 0; i < dgBrand.Rows.Count; i++)
                {
                    dgBrand.Rows[i].Cells[ColIndexBrand.Select].Value = chkBrand.Checked;
                }
                dgBrand.CurrentCell = dgBrand[ColIndexBrand.Select, 0];
                dgBrand.CurrentCell = dgBrand[ColIndexBrand.Disc, 0];
                dgBrand.Focus();
            }
        }

        private void chkItemLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (dgItemMaster.Rows.Count > 0)
            {
                for (int i = 0; i < dgItemMaster.Rows.Count; i++)
                {
                    dgItemMaster.Rows[i].Cells[ColIndexItem.Select].Value = chkItemLevel.Checked;
                }
                dgItemMaster.CurrentCell = dgItemMaster[ColIndexItem.Disc, 0];
                dgItemMaster.CurrentCell = dgItemMaster[ColIndexItem.Select, 0];
                dgItemMaster.Focus();
            }
        }

        private void dgBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                Postion = e.RowIndex;
                if (BindItemMaster(Postion))
                {
                    if (BtnSave.Visible == false) return;
                    dgBrand.Enabled = false;
                    dgItemMaster.Enabled = true;
                    btnApply.Enabled = true;
                    btnItemCancel.Enabled = true;
                    dgItemMaster.CurrentCell = dgItemMaster.Rows[0].Cells[ColIndexItem.Select];
                    dgItemMaster.Focus();
                }
                else
                {
                    btnApply.Enabled = false;
                    btnItemCancel.Enabled = false;
                    DisplayMessage("Item Not Found...");
                    dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[ColIndexBrand.Select];
                    dgBrand.Focus();
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                DtpDate.Enabled = false;
                dgBrand.Enabled = true;

                if (OptionType == 1)
                    cmbMfgCompany.Focus();
                else
                    DtpDiscountDateFrom.Focus();

                BtnSave.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnUpdate_VisibleChanged(object sender, EventArgs e)
        {
            if (btnCancel.Visible == false)
                btnView.Visible = !btnUpdate.Visible;
            else btnView.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new ItemLevelDiscountSearch();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }
    }
}
