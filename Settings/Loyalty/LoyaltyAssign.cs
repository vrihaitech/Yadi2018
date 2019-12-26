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

namespace Yadi.Settings.Loyalty
{

    public partial class LoyaltyAssign : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        Color clrColorRow = Color.FromArgb(255, 224, 192);
        long ParaSchemeNo = 0;
        string strLedgNo = "";
        public DataTable dtSchemeAssign;
        int TypeNo = 1;

        public LoyaltyAssign()
        {
            InitializeComponent();
        }

        public LoyaltyAssign(long SchemeNo, DataTable dtSchemeA, int typeno)
        {
            try
            {
                InitializeComponent();
                dtSchemeAssign = dtSchemeA;
                ParaSchemeNo = SchemeNo;
                TypeNo = typeno;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void LoyaltyMTD_Load(object sender, EventArgs e)
        {
            try
            {
                formatPics();
                //dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top)
                //            | System.Windows.Forms.AnchorStyles.Left)));
                BindGrid();
                if (TypeNo == 1)
                    SetEnable(true);
                else if (TypeNo == 2)
                    SetEnable(false);

                //btnOK.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void SetEnable(bool flag)
        {
            dgBill.Enabled = flag;
            chkDeAssign.Enabled = flag;
            btnSave.Enabled = flag;
            btnList.Enabled = flag;

        }

        private void BindGrid()
        {
            try
            {
                DataTable dt = new DataTable();
                string strQuery = "SELECT 0 AS SrNo, MLedger.LedgerName, MSchemeAssign.IsActive, MSchemeAssign.LedgerNo, MSchemeAssign.PkSrNo " +
                    " FROM MLedger INNER JOIN MSchemeAssign ON MLedger.LedgerNo = MSchemeAssign.LedgerNo " +
                    " WHERE     (MSchemeAssign.SchemeNo = " + ParaSchemeNo + ")";
                dt = ObjFunction.GetDataView(strQuery).Table;
                dt.Rows.Add(dt.NewRow());

                dtSchemeAssign = dt.Clone();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgBill.Rows.Add();
                    for (int j = 0; j < dgBill.ColumnCount; j++)
                    {
                        if (j == ColIndex.IsActive)
                        {
                            if (dt.Rows[i].ItemArray[j].ToString() != "")
                                dgBill.Rows[i].Cells[j].Value = Convert.ToBoolean(dt.Rows[i].ItemArray[j].ToString());
                            else dgBill.Rows[i].Cells[j].Value = false;
                        }
                        else dgBill.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                    }
                }

                dgBill.CurrentCell = dgBill[ColIndex.LedgerName, dgBill.Rows.Count - 1];
                dgBill.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayList(bool flag)
        {
            pnlLedger.Visible = flag;
        }

        #region dgBillGrid

        private void formatPics()
        {
            pnlLedger.Top = pnlMain.Top + 10;
            pnlLedger.Left = pnlMain.Left + 10;
            pnlLedger.Width = 300;
            pnlLedger.Height = 200;
        }

        #region dgBill code

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        public void DeleteRow()
        {
            try
            {
                if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkSrNo].Value != null)
                {
                    if (dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkSrNo].Value.ToString() == "0")
                    {
                        if (OMMessageBox.Show("Are you sure want to delete this customer name ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            if (dgBill.Rows.Count - 1 == dgBill.CurrentCell.RowIndex)
                            {
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                                dgBill.Rows.Add();
                            }
                            else
                                dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete)
                {
                    DeleteRow();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.SrNo)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell = dgBill[ColIndex.LedgerName, dgBill.CurrentCell.RowIndex];
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.LedgerName)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.Focus();
                        if (dgBill.CurrentRow.Cells[ColIndex.PkSrNo].Value.ToString() == "0" || dgBill.CurrentRow.Cells[ColIndex.PkSrNo].Value.ToString() == "")
                        {
                            if (dgBill.CurrentRow.Cells[ColIndex.LedgerName].FormattedValue.ToString() == "")
                            {
                                pnlLedger.Visible = true;
                                lstLedger.Focus();
                            }
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    btnSave.Focus();
                }
                else if (e.KeyCode == Keys.F3)
                {
                    dgBill.CurrentCell = dgBill[ColIndex.LedgerName, dgBill.Rows.Count - 1];
                    dgBill.Focus();
                }
                else if (e.KeyCode == Keys.F6)
                {
                    btnList_Click(sender, new EventArgs());
                }
                else if (e.KeyCode == Keys.F8)
                {
                    if (TypeNo == 1)
                    {
                        if (chkDeAssign.Checked == true) chkDeAssign.Checked = false; else chkDeAssign.Checked = true;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.LedgerName)
                {
                    if (dgBill.CurrentCell.Value != null && Convert.ToString(dgBill.CurrentCell.Value) != "")
                    {
                        dgBill.CurrentCell.Value = "";
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int LedgerName = 1;
            public static int IsActive = 2;
            public static int LedgerNo = 3;
            public static int PkSrNo = 4;
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        public bool PartyExist(long LedgNo, out int rowIndex)
        {
            rowIndex = -1;
            try
            {
                bool flag = false;
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (LedgNo == Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.LedgerNo].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;
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

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void lstLedger_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;

                    int rwIndex = dgBill.CurrentRow.Index, ExistRwIndex = 0;
                    if (PartyExist(Convert.ToInt64(lstLedger.SelectedValue.ToString()), out ExistRwIndex) == false)
                    {
                        if (dgBill.Rows[rwIndex].Cells[ColIndex.PkSrNo].Value.ToString() == "" || dgBill.Rows[rwIndex].Cells[ColIndex.PkSrNo].Value.ToString() == "0")
                        {
                            dgBill.Rows[rwIndex].Cells[ColIndex.LedgerName].Value = lstLedger.Text;
                            dgBill.Rows[rwIndex].Cells[ColIndex.LedgerNo].Value = lstLedger.SelectedValue;
                            dgBill.Rows[rwIndex].Cells[ColIndex.IsActive].Value = true;
                            dgBill.Rows[rwIndex].Cells[ColIndex.PkSrNo].Value = "0";
                            dgBill.Rows.Add();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.PkSrNo].Value = "0";
                            dgBill.CurrentCell = dgBill[ColIndex.LedgerName, dgBill.Rows.Count - 1];
                            dgBill.Focus();
                        }
                    }
                    else
                    {
                        DisplayMessage("Customer Name already exist...");
                        dgBill.CurrentCell = dgBill[ColIndex.LedgerName, ExistRwIndex];
                        dgBill.Focus();
                    }

                    pnlLedger.Visible = false;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    pnlLedger.Visible = false;
                    dgBill.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstLedger_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //string ItemListStr = "";
                if (e.KeyChar == 13)
                {
                    pnlLedger.Visible = false;


                }
                else if (e.KeyChar == ' ')
                {
                    dgBill.Focus();
                    pnlLedger.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #endregion

        public bool Validation()
        {
            bool flag = false;
            for (int i = 0; i < dgBill.Rows.Count - 1; i++)
            {
                if (dgBill.Rows[i].Cells[ColIndex.LedgerNo].Value.ToString() == "0")
                {
                    flag = false;
                    break;
                }
                else flag = true;
            }
            return flag;
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
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    if (TypeNo == 1)
                    {
                        if (btnSave.Visible) btnSave_Click(sender, e);
                    }
                }
                else if (e.KeyCode == Keys.F12)
                {
                    if (btnExit.Visible) btnExit_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F2)
                {
                    if (ombSearchPanel.Visible) chkSelectAll.Checked = !chkSelectAll.Checked;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation() == true)
                {
                    FillGridView();
                    SaveData();
                    this.Close();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SaveData()
        {
            try
            {
                DBMScheme dbMScheme = new DBMScheme();
                MSchemeAssign mSchemeAssign = new MSchemeAssign();

                //For Scheme Assign
                for (int i = 0; i < dtSchemeAssign.Rows.Count; i++)
                {
                    mSchemeAssign = new MSchemeAssign();
                    mSchemeAssign.PkSrNo = Convert.ToInt64(dtSchemeAssign.Rows[i].ItemArray[4].ToString());
                    mSchemeAssign.AssignDate = DBGetVal.ServerTime.Date;
                    mSchemeAssign.LedgerNo = Convert.ToInt64(dtSchemeAssign.Rows[i].ItemArray[3].ToString());
                    mSchemeAssign.IsActive = Convert.ToBoolean(dtSchemeAssign.Rows[i].ItemArray[2].ToString());
                    mSchemeAssign.UserID = DBGetVal.UserID;
                    mSchemeAssign.UserDate = DBGetVal.ServerTime;
                    mSchemeAssign.SchemeNo = ParaSchemeNo;
                    mSchemeAssign.PromoCode = "";
                    mSchemeAssign.CompanyNo = DBGetVal.FirmNo;
                    dbMScheme.AddMSchemeAssign1(mSchemeAssign);
                }
                long tempID = dbMScheme.ExecuteNonQueryStatements();
                if (tempID != 0)
                {
                    OMMessageBox.Show("Scheme Assigning Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    OMMessageBox.Show("Scheme not Assigning Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillGridView()
        {
            try
            {
                DataRow dr = null;
                dtSchemeAssign.Rows.Clear();
                foreach (DataGridViewRow row in dgBill.Rows)
                {
                    if (row.Cells[ColIndex.LedgerNo].Value != null)
                    {
                        if (row.Cells[ColIndex.LedgerNo].Value.ToString() != "0" && row.Cells[ColIndex.LedgerNo].Value.ToString() != "")
                        {
                            dr = dtSchemeAssign.NewRow();
                            dr[0] = row.Index;
                            dr[1] = row.Cells[ColIndex.LedgerName].Value;
                            dr[2] = row.Cells[ColIndex.IsActive].Value;
                            dr[3] = row.Cells[ColIndex.LedgerNo].Value;
                            dr[4] = row.Cells[ColIndex.PkSrNo].Value;
                            dtSchemeAssign.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void LoyaltyAssign_Activated(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillList(lstLedger, "Select LedgerNo,LedgerName From MLedger Where GroupNo=" + GroupType.SundryDebtors + " AND IsEnroll='true' order by LedgerName");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                strLedgNo = "";
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (i == 0)
                        strLedgNo = dgBill.Rows[i].Cells[ColIndex.LedgerNo].Value.ToString();
                    else
                        strLedgNo += "," + dgBill.Rows[i].Cells[ColIndex.LedgerNo].Value.ToString();
                }
                TxtSearch.Text = "";
                ombSearchPanel.Visible = true;
                ombSearchPanel.Dock = DockStyle.Fill;
                rbAll.Checked = true;
                rb_CheckedChanged(rbAll, e);
                TxtSearch.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BindGridSearch()
        {
            try
            {
                while (dataGridView1.Rows.Count > 0)
                    dataGridView1.Rows.RemoveAt(0);

                DataView dv = new DataView();
                string str = TxtSearch.Text;
                if (str.Trim() != "")
                {
                    str = str.Replace("+91", "");
                    if (char.IsLetter(str, 0))
                    {
                        dv = GetBySearch(0, TxtSearch.Text);
                        //dataGridView1.DataSource = dv;
                    }
                    else if (char.IsNumber(str, 0))
                    {
                        if (str[0].ToString() == "0")
                        {
                            dv = GetBySearch(1, TxtSearch.Text);
                           // dataGridView1.DataSource = dv;
                        }
                        else
                        {
                            dv = GetBySearch(2, TxtSearch.Text);
                           // dataGridView1.DataSource = dv;

                        }
                    }
                    DataTable dt = dv.Table;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i].Cells[j + 1].Value = dt.Rows[i].ItemArray[j].ToString();
                        }

                        dataGridView1.Rows[i].Cells[0].Value = true;
                        dataGridView1.Rows[i].Cells[0].ReadOnly = false;
                    }
                    
                    dataGridView1.Columns[1].Visible = false;
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                        dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    //dataGridView1.Columns[3].Visible = false;
                    chkSelectAll.Checked = true;
                }
                else
                {
                    dv = GetBySearch(3, TxtSearch.Text);
                    DataTable dt = dv.Table;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            dataGridView1.Rows[i].Cells[j + 1].Value = dt.Rows[i].ItemArray[j].ToString();
                        }

                        dataGridView1.Rows[i].Cells[0].Value = true;
                        dataGridView1.Rows[i].Cells[0].ReadOnly = false;
                    }

                    dataGridView1.Columns[1].Visible = false;
                    for (int i = 1; i < dataGridView1.Columns.Count; i++)
                        dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                    //dataGridView1.Columns[3].Visible = false;
                    chkSelectAll.Checked = true;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public DataView GetBySearch(int Column, string Value)
        {
            try
            {
                string sql = null;
                //sql = " SELECT MLedger.LedgerNo,MLedger.LedgerName +' '+ MLedger.ContactPerson + ' - ' +IsNull(MLedgerDetails.MobileNo1,' ') + ' - ' +IsNull(MLedgerDetails.MobileNo2,' ') + ' - ' +IsNull(MLedgerDetails.PhNo1,' ') + '-' +IsNull(MLedgerDetails.PhNo2,' ') +' - '+IsNull(MLedgerDetails.Address,' ') AS Search,0 As Status,MLedger.LedgerName " +
                //            " FROM MLedger  LEFT OUTER JOIN  MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo ";
                sql = " SELECT MLedger.LedgerNo,MLedger.LedgerName, Case When MLedgerDetails.MobileNo1='1111111111' Then '' Else IsNull(MLedgerDetails.MobileNo1,' ') End AS MobileNo1, IsNull(MLedgerDetails.MobileNo2,' ') AS MobileNo2, IsNull(MLedgerDetails.PhNo1,' ') AS PhNo1, IsNull(MLedgerDetails.PhNo2,' ') AS PhNo2, IsNull(MLedgerDetails.Address,' ') AS Address,MLedger.ContactPerson " +//,MLedger.LedgerName
                            " FROM MLedger  LEFT OUTER JOIN  MLedgerDetails ON MLedger.LedgerNo = MLedgerDetails.LedgerNo ";
                if (Value == "") Column = 3;
                switch (Column)
                {
                    case 0:
                        sql += " Where   (MLedger.LedgerName+' '+ MLedger.ContactPerson like '%" + Value.Trim().Replace("'", "''") + "' + '%' or  MLedgerDetails.Address like '%" + Value.Trim().Replace("'", "''") + "' + '%') And MLedger.IsActive='true' ";

                        break;

                    case 1:
                        sql += " Where    (MLedgerDetails.PhNo1 like '%" + Value.Trim().Replace("'", "''") + "' + '%' or  MLedgerDetails.PhNo2 like '%" + Value.Trim().Replace("'", "''") + "' + '%') And MLedger.IsActive='true' ";

                        break;
                    case 2:
                        sql += " Where    (MLedgerDetails.MobileNo1 like '%" + Value.Trim().Replace("'", "''") + "' + '%' or  MLedgerDetails.MobileNo2 like '%" + Value.Trim().Replace("'", "''") + "' + '%') And MLedger.IsActive='true' ";
                        break;
                    case 3:
                        sql += " Where (MLedger.LedgerName = MLedger.LedgerName) ";
                        break;
                }
                if (rbAll.Checked == false)
                    sql += ((rbMale.Checked == true) ? " And MLedgerDetails.Gender=0 " : "And MLedgerDetails.Gender=1");
                sql += " AND MLedger.IsEnroll='true' And  MLedger.GroupNo=" + GroupType.SundryDebtors;
                if (strLedgNo != "")
                    sql += " And  MLedger.LedgerNo not in (" + strLedgNo + ") ";
                sql += "Order By  MLedger.LedgerName";
                DataSet ds = new DataSet();
                try
                {
                    ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
                    
                }
                catch (Exception e)
                {
                    CommonFunctions.ErrorMessge = e.Message;
                }
                return ds.Tables[0].DefaultView;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return null;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ombSearchPanel.Visible = false;
                int rwIndex = dgBill.Rows.Count - 1;
                int ExistRwIndex = -1;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].FormattedValue) == true)
                    {
                        if (PartyExist(Convert.ToInt64(dataGridView1.Rows[i].Cells[1].Value), out ExistRwIndex) == false)
                        {
                            dgBill.Rows[rwIndex].Cells[ColIndex.LedgerName].Value = dataGridView1.Rows[i].Cells[2].Value;
                            dgBill.Rows[rwIndex].Cells[ColIndex.LedgerNo].Value = dataGridView1.Rows[i].Cells[1].Value;
                            dgBill.Rows[rwIndex].Cells[ColIndex.IsActive].Value = true;
                            dgBill.Rows[rwIndex].Cells[ColIndex.PkSrNo].Value = "0";
                            dgBill.Rows.Add();
                            dgBill.Rows[dgBill.Rows.Count - 1].Cells[ColIndex.PkSrNo].Value = "0";
                            rwIndex++;
                        }
                        else
                        {
                            if (ExistRwIndex >= 0)
                                dgBill.Rows[ExistRwIndex].Cells[ColIndex.IsActive].Value = true;
                        }
                    }
                }
                dgBill.CurrentCell = dgBill[ColIndex.LedgerName, dgBill.Rows.Count - 1];
                dgBill.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ombSearchPanel.Visible = false;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down)
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Focus();
                        dataGridView1.CurrentCell = dataGridView1[2, 0];
                    }
                }
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    dataGridView1.CurrentRow.Cells[0].Value = !Convert.ToBoolean(dataGridView1.CurrentRow.Cells[0].FormattedValue);
                    //    //if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "0")
                    //    //{
                    //    //    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightBlue;
                    //    //    dataGridView1.CurrentRow.Cells[2].Value = "1";
                    //    //}
                    //    //else
                    //    //{
                    //    //    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                    //    //    dataGridView1.CurrentRow.Cells[2].Value = "0";
                    //    //}
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    btnOK.Focus();
                }
                else if (e.KeyCode == Keys.A && e.Control)
                {
                    //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //{
                    //    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                    //    dataGridView1.Rows[i].Cells[2].Value = "1";
                    //}
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BindGridSearch();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 2)
                //{
                //    if (e.Value.ToString() == "1")
                //        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                //    else
                //        dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkDeAssign_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBill.Rows.Count > 0)
                {
                    dgBill.CurrentCell = dgBill[1, dgBill.CurrentRow.Index];
                    dgBill.Focus();
                }
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                    dgBill.Rows[i].Cells[2].Value = !chkDeAssign.Checked;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                BindGridSearch();
                TxtSearch.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (dataGridView1.CurrentRow.Cells[2].Value.ToString() == "0")
                //{
                //    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightBlue;
                //    dataGridView1.CurrentRow.Cells[2].Value = "1";
                //}
                //else
                //{
                //    dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.White;
                //    dataGridView1.CurrentRow.Cells[2].Value = "0";
                //}
                //if (e.ColumnIndex == 7)
                //{
                //    dataGridView1.CurrentRow.Cells[7].Value = !Convert.ToBoolean(dataGridView1.CurrentRow.Cells[7].FormattedValue);
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[0].Value = chkSelectAll.Checked;
                if (dataGridView1.Rows.Count > 0)
                    dataGridView1.CurrentCell = dataGridView1[2, 0];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}
