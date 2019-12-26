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

namespace Yadi.Settings
{
    public partial class StockCountTypeAE : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMStockCountType dbMStockCountType = new DBMStockCountType();
        MStockCountType mStockCountType = new MStockCountType();

        DBMStockCountSchedule dbMStockCountSchedule = new DBMStockCountSchedule();


        //string CityNm;
        DataTable dtSearch = new DataTable();
        //int cntRow;
        public long CityNo, ID;
        //int nw;
        //bool isDoProcess = false;

        public StockCountTypeAE()
        {
            InitializeComponent();
        }

        private void StockCountTypeAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);


                dtSearch = ObjFunction.GetDataView("Select CountTypeNo From MStockcountType order by CountTypeNo").Table;

                if (dtSearch.Rows.Count > 0)
                {
                    //    if (City.RequestCityNo == 0)
                    ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    //    else
                    //        ID = City.RequestCityNo;
                    BindGrid();
                }
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
                while (dgDetails.Rows.Count > 0)
                    dgDetails.Rows.RemoveAt(0);

                pnlDefault.Visible = false;
                dgDetails.Enabled = false;

                string sql = " SELECT     0 AS SrNo, CountTypeName, '' AS Value, IsActive, CountTypeNo, DefaultValue, 0 AS Chk " +
                             " FROM MStockCountType " +
                             " ORDER BY CountTypeNo ";
                DataTable dt = ObjFunction.GetDataView(sql).Table;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgDetails.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j == 2)
                        {
                            dgDetails.Rows[i].Cells[j].Value = GetValue(Convert.ToInt64(dt.Rows[i].ItemArray[4].ToString()), Convert.ToDateTime(dt.Rows[i].ItemArray[5].ToString()));
                        }
                        else dgDetails.Rows[i].Cells[j].Value = dt.Rows[i][j];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public string GetValue(long CountTypeNo, DateTime value)
        {
            try
            {
                string str = "";
                if (CountTypeNo == StockCountType.Weekly)
                {
                    str = value.DayOfWeek.ToString().Trim();
                }
                else if (CountTypeNo == StockCountType.Monthly)
                {
                    str = value.ToString("dd");
                }
                else if (CountTypeNo == StockCountType.Yearly)
                {
                    str = value.ToString("dd-MMM");
                }

                return str;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return "";
            }
        }

        public int GetWeek(string MMM)
        {
            int Type = 0;
            switch (MMM)
            {
                case "Sunday":
                    Type = 0;
                    break;
                case "Monday":
                    Type = 1;
                    break;
                case "Tuesday":
                    Type = 2;
                    break;
                case "Wednesday":
                    Type = 3;
                    break;
                case "Thursday":
                    Type = 4;
                    break;
                case "Friday":
                    Type = 5;
                    break;
                case "Saturday":
                    Type = 6;
                    break;
            }
            return Type;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                dgDetails.Enabled = true;
                dgDetails.Focus();
                dgDetails.CurrentCell = dgDetails[2, 0];
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
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                BindGrid();
                pnlDefault.Visible = false;
                btnUpdate.Focus();
                dgDetails.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validation()
        {
            bool flag = true;

            if (pnlDefault.Visible == true)
            {
                if (cmbDefault.Visible == true)
                {
                    cmbDefault.Focus();
                    flag = false;
                }
                else if (DtpDefaultValue.Visible == true)
                {
                    DtpDefaultValue.Focus();
                    flag = false;
                }
            }
            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation() == true)
                {
                    bool flag = false;
                    for (int i = 0; i < dgDetails.Rows.Count; i++)
                    {
                        if (Convert.ToInt64(dgDetails.Rows[i].Cells[6].Value) != 0)
                        {
                            mStockCountType = new MStockCountType();
                            mStockCountType.CountTypeNo = Convert.ToInt64(dgDetails.Rows[i].Cells[4].Value);
                            mStockCountType.DefaultValue = Convert.ToDateTime(dgDetails.Rows[i].Cells[5].Value);
                            mStockCountType.IsActive = Convert.ToBoolean(dgDetails.Rows[i].Cells[3].Value);
                            mStockCountType.CompanyNo = DBGetVal.FirmNo;
                            mStockCountType.UserID = DBGetVal.UserID;
                            mStockCountType.UserDate = DBGetVal.ServerTime;
                            dbMStockCountType.AddMStockCountType(mStockCountType);
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        if (dbMStockCountType.ExecuteNonQueryStatements() == true)
                        {
                            OMMessageBox.Show("Stock Count Type Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            BindGrid();
                            ObjFunction.LockButtons(true, this.Controls);
                            ObjFunction.LockControls(false, this.Controls);
                        }
                        else
                        {
                            OMMessageBox.Show("Stock Count Type Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long CountType = Convert.ToInt64(dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[4].Value);
                    e.SuppressKeyPress = true;
                    if (CountType == StockCountType.NA || CountType == StockCountType.Daily)
                    {
                        pnlDefault.Visible = false;
                        dgDetails.CurrentCell = dgDetails[2, dgDetails.CurrentRow.Index];
                        dgDetails.Focus();
                    }
                    else if (CountType == StockCountType.Weekly)
                    {
                        pnlDefault.Visible = true;
                        cmbDefault.Visible = true;
                        DtpDefaultValue.Visible = false;
                        ObjFunction.FillWeek(cmbDefault);
                        cmbDefault.SelectedValue = GetWeek(Convert.ToDateTime(dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[5].Value).DayOfWeek.ToString().Trim());
                        cmbDefault.Focus();
                    }
                    else if (CountType == StockCountType.Monthly)
                    {
                        pnlDefault.Visible = true;
                        cmbDefault.Visible = true;
                        DtpDefaultValue.Visible = false;
                        ObjFunction.FillDays(cmbDefault);
                        cmbDefault.SelectedValue = Convert.ToInt64(Convert.ToDateTime(dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[5].Value).ToString("dd").ToUpper().Trim());
                        cmbDefault.Focus();
                    }
                    else if (CountType == StockCountType.Yearly)
                    {
                        pnlDefault.Visible = true;
                        cmbDefault.Visible = false;
                        DtpDefaultValue.Visible = true;
                        DtpDefaultValue.Location = new Point(cmbDefault.Location.X, cmbDefault.Location.Y);
                        DtpDefaultValue.CustomFormat = "dd-MMM";
                        DtpDefaultValue.Value = Convert.ToDateTime(dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[5].Value);
                        DtpDefaultValue.MinDate = Convert.ToDateTime("1-Jan-"+DBGetVal.ServerTime.Year+"");
                        DtpDefaultValue.MaxDate = Convert.ToDateTime("31-Dec-" + DBGetVal.ServerTime.Year + "");

                        DtpDefaultValue.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    dgDetails.CurrentCell = null;
                    e.SuppressKeyPress = true;
                    btnSave.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void dgDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void cmbDefault_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long CountType = Convert.ToInt64(dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[4].Value);
                    if (CountType == StockCountType.Monthly || CountType == StockCountType.Weekly)
                    {
                        e.SuppressKeyPress = true;
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[2].Value = cmbDefault.Text;
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[5].Value = dbMStockCountSchedule.SetStockCountType(CountType, ObjFunction.GetComboValue(cmbDefault).ToString());
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[6].Value = "1";
                        dgDetails.Focus();
                        pnlDefault.Visible = false;
                        dgDetails.CurrentCell = dgDetails[2, dgDetails.CurrentRow.Index];
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    pnlDefault.Visible = false;
                    e.SuppressKeyPress = true;
                    dgDetails.CurrentCell = dgDetails[2, dgDetails.CurrentRow.Index];
                    dgDetails.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpDefaultValue_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long CountType = Convert.ToInt64(dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[4].Value);
                    if (CountType == StockCountType.Yearly)
                    {
                        e.SuppressKeyPress = true;
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[2].Value = DtpDefaultValue.Value.ToString("dd-MMM");
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[5].Value = dbMStockCountSchedule.SetStockCountType(CountType, DtpDefaultValue.Value.ToString("dd-MMM"));
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[6].Value = "1";
                        dgDetails.Focus();
                        pnlDefault.Visible = false;
                        dgDetails.CurrentCell = dgDetails[2, dgDetails.CurrentRow.Index];
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    pnlDefault.Visible = false;
                    e.SuppressKeyPress = true;
                    dgDetails.CurrentCell = dgDetails[2, dgDetails.CurrentRow.Index];
                    dgDetails.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[6].Value = "1";
            }
        }

        private void cmbDefault_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgDetails.CurrentRow.Index != -1)
                {
                    long CountType = Convert.ToInt64(dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[4].Value);
                    if (CountType == StockCountType.Yearly)
                    {
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[2].Value = DtpDefaultValue.Value.ToString("dd-MMM");
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[5].Value = dbMStockCountSchedule.SetStockCountType(CountType, DtpDefaultValue.Value.ToString("dd-MMM"));
                        dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[6].Value = "1";
                        dgDetails.Focus();
                        pnlDefault.Visible = false;
                        dgDetails.CurrentCell = dgDetails[2, dgDetails.CurrentRow.Index];
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DtpDefaultValue_ValueChanged(object sender, EventArgs e)
        {

        }



    }
}
