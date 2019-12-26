using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using OM;
using OMControls;

namespace Yadi.Settings
{
    /// <summary>
    /// This Class use for PayTypeSettings
    /// </summary>
    public partial class PayTypeSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMPayTypeLedger dbPayTypeLedger = new DBMPayTypeLedger();
        MPayTypeLedger mPayTypeLedger = new MPayTypeLedger();
        DBMPayType dbPayType = new DBMPayType();
        MPayType mPayType = new MPayType();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dtPayType = new DataTable();
        DBMSettings dbMSettings = new DBMSettings();
        bool isPayTypeLedgerDataChanged = false, isPayTypeSettingChanged = false;

        /// <summary>
        /// This is Class of Constructor
        /// </summary>
        public PayTypeSetting()
        {
            InitializeComponent();
        }

        private void PayTypeSetting_Load(object sender, EventArgs e)
        {
            FillControls();
            KeyDownFormat(this.Controls);
        }

        private void FillControls()
        {
            ObjFunction.SetAppSettings();

            ObjFunction.FillCombo(cmbCompany, "Select FirmNo,FirmName from MFirm Where FirmNo=" + DBGetVal.FirmNo+ " Order by FirmName");
            cmbCompany.Enabled = false;
            cmbCompany.SelectedValue = DBGetVal.FirmNo.ToString();
            ObjFunction.FillCombo(cmbCash, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.CashInhand + " AND IsActive='true'");
            ObjFunction.FillCombo(cmbCheque, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.BankAccounts + " AND IsActive='true'");
            ObjFunction.FillCombo(cmbCreditCard, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.BankAccounts + " AND IsActive='true'");
            ObjFunction.FillCombo(cmbFoodVoucher, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.CashInhand + " AND IsActive='true'");
            ObjFunction.FillList(lstLedger, "Select LedgerNo,LedgerName from MLedger Where GroupNo in (" + GroupType.CashInhand + "," + GroupType.DirectIncome + "," + GroupType.IndirectIncome + ") AND IsActive='true'");
            ObjFunction.FillList(lstPayType, "Select PKPayTypeNo,PayTypeName from MPayType Where PKPayTypeNo in (2,4,5,6)");

            DataTable dt = new DataTable();

            dt.Columns.Add("UserNo");
            dt.Columns.Add("MethodName");
            DataRow dr = dt.NewRow();
            dr[0] = "0"; dr[1] = " ------ Select ------ "; dt.Rows.Add(dr);
            dr = dt.NewRow(); dr[0] = "1"; dr[1] = "Stricktly Use Multiple Firm Accounting Method"; dt.Rows.Add(dr);
            dr = dt.NewRow(); dr[0] = "2"; dr[1] = "Use Common / Single Firm Accounting Method"; dt.Rows.Add(dr);
            isPayTypeLedgerDataChanged = false;
            isPayTypeSettingChanged = false;


            dtPayType = new DataTable();
            dtPayType = ObjFunction.GetDataView("Select PkPayTypeLedgerNo, PayTypeNo, LedgerNo, CompanyNo, ISNULL(ChargesPerce, 0) " +
                " From MPayTypeLedger Where CompanyNo = " + ObjFunction.GetComboValue(cmbCompany).ToString()).Table;

            cmbCash.SelectedValue = "0";
            cmbCheque.SelectedValue = "0";
            cmbCreditCard.SelectedValue = "0";
            long cntPayType = -1;
            for (int i = 0; i < dtPayType.Rows.Count; i++)
            {
                cntPayType = ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where PayTypeNo=" + dtPayType.Rows[i]["PayTypeNo"].ToString() + "", CommonFunctions.ConStr);
                if (dtPayType.Rows[i]["PayTypeNo"].ToString().Equals(/*"Cash"*/"2", StringComparison.CurrentCultureIgnoreCase))
                {
                    ObjFunction.FillCombo(cmbCash, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.CashInhand + " AND IsActive='true' Or LedgerNo =" + dtPayType.Rows[i]["LedgerNo"].ToString() + "");
                    cmbCash.SelectedValue = dtPayType.Rows[i]["LedgerNo"].ToString();
                    if (cntPayType > 0) cmbCash.Enabled = false; else cmbCash.Enabled = true;
                    txtChrg1.Text = dtPayType.Rows[i][4].ToString();
                }
                else if (dtPayType.Rows[i]["PayTypeNo"].ToString().Equals(/*"Cheque"*/"4", StringComparison.CurrentCultureIgnoreCase))
                {
                    ObjFunction.FillCombo(cmbCheque, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.BankAccounts + " AND IsActive='true' Or LedgerNo =" + dtPayType.Rows[i]["LedgerNo"].ToString() + "");
                    cmbCheque.SelectedValue = dtPayType.Rows[i]["LedgerNo"].ToString();
                    //if (cntPayType > 0) cmbCheque.Enabled = false; else cmbCheque.Enabled = true;
                    txtChrg2.Text = dtPayType.Rows[i][4].ToString();
                }
                else if (dtPayType.Rows[i]["PayTypeNo"].ToString().Equals(/*"Credit Card"*/"5", StringComparison.CurrentCultureIgnoreCase))
                {
                    ObjFunction.FillCombo(cmbCreditCard, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.BankAccounts + " AND IsActive='true' Or LedgerNo =" + dtPayType.Rows[i]["LedgerNo"].ToString() + "");
                    cmbCreditCard.SelectedValue = dtPayType.Rows[i]["LedgerNo"].ToString();
                    //if (cntPayType > 0) cmbCreditCard.Enabled = false; else cmbCreditCard.Enabled = true;
                    txtChrg3.Text = dtPayType.Rows[i][4].ToString();
                }
                else if (dtPayType.Rows[i]["PayTypeNo"].ToString().Equals(/*"Food Voucher"*/"6", StringComparison.CurrentCultureIgnoreCase))
                {
                    ObjFunction.FillCombo(cmbFoodVoucher, "Select LedgerNo,LedgerName from MLedger Where GroupNo=" + GroupType.CashInhand + " AND IsActive='true' Or LedgerNo =" + dtPayType.Rows[i]["LedgerNo"].ToString() + "");
                    cmbFoodVoucher.SelectedValue = dtPayType.Rows[i]["LedgerNo"].ToString();
                    if (cntPayType > 0) cmbFoodVoucher.Enabled = false; else cmbFoodVoucher.Enabled = true;
                    txtChrg4.Text = dtPayType.Rows[i][4].ToString();
                }
            }
            DataTable dtp = new DataTable();
            GridView.Rows.Clear();
            dtp = ObjFunction.GetDataView("Select PkPayTypeLedgerNo,  MPayType.PayTypeName,(Select MP.PayTypeName From MPayType MP Where MP.PKPayTypeNo=MPayType.ControlUnder) As ControlUnderName, MLedger.LedgerName,MPayType.ShortName,MPayType.IsActive, MPayType.PKPayTypeNo, MPayTypeLedger.LedgerNo,MPayType.ControlUnder,ISNULL(MPayTypeLedger.ChargesPerce,0) " +
                                        " FROM MPayTypeLedger INNER JOIN "+
                                        " MLedger ON MPayTypeLedger.LedgerNo = MLedger.LedgerNo INNER JOIN "+
                                        " MPayType ON MPayTypeLedger.PayTypeNo = MPayType.PKPayTypeNo Where MPayTypeLedger.CompanyNo = " + ObjFunction.GetComboValue(cmbCompany).ToString() + " and MPayType.PKPayTypeNo<>1 and MPayType.PKPayTypeNo>6").Table;

            for (int i = 0; i < dtp.Rows.Count; i++)
            {
                GridView.Rows.Add();
                for (int j = 0; j < GridView.Columns.Count; j++)
                {
                    GridView.Rows[i].Cells[j].Value = dtp.Rows[i].ItemArray[j];
                }
                if (ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where PayTypeNo=" + dtp.Rows[i].ItemArray[6].ToString() + "", CommonFunctions.ConStr) > 0)
                {
                    GridView.Rows[i].ReadOnly = true;
                    GridView.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                }
                else
                {
                    GridView.Rows[i].ReadOnly = false;
                    GridView.Rows[i].DefaultCellStyle.BackColor = Color.White;
                } 
            }
                isPayTypeLedgerDataChanged = false;
            cmbCash.Focus();


        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isPayTypeLedgerDataChanged)
            {
               
                //dgSettings.DataSource = dt.DefaultView;
            }
            else
            {
                if (dtPayType.Rows.Count > 0)
                {
                    cmbCompany.SelectedValue = dtPayType.Rows[0]["CompanyNo"].ToString();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validations() == true)
            {
                if (isPayTypeLedgerDataChanged)
                {
                    DataRow[] drt = dtPayType.Select("PayTypeNo = 2");
                    dbPayTypeLedger = new DBMPayTypeLedger();
                    mPayTypeLedger = new MPayTypeLedger();
                    if (drt.Length > 0) mPayTypeLedger.PKPayTypeLedgerNo = Convert.ToInt64(drt[0]["PKPayTypeLedgerNo"].ToString());
                    else mPayTypeLedger.PKPayTypeLedgerNo = 0;

                    mPayTypeLedger.PayTypeNo = 2;
                    mPayTypeLedger.LedgerNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCash).ToString());
                    mPayTypeLedger.CompanyNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCompany).ToString());
                    mPayTypeLedger.ChargesPerce = (txtChrg1.Text == "") ? 0 : Convert.ToDouble(txtChrg1.Text);

                    if (dbPayTypeLedger.AddMPayTypeLedger(mPayTypeLedger) == true)
                    {
                        drt = dtPayType.Select("PayTypeNo = 4");
                        dbPayTypeLedger = new DBMPayTypeLedger();
                        mPayTypeLedger = new MPayTypeLedger();
                        if (drt.Length > 0) mPayTypeLedger.PKPayTypeLedgerNo = Convert.ToInt64(drt[0]["PKPayTypeLedgerNo"].ToString());
                        else mPayTypeLedger.PKPayTypeLedgerNo = 0;

                        mPayTypeLedger.PayTypeNo = 4;
                        mPayTypeLedger.LedgerNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCheque).ToString());
                        mPayTypeLedger.CompanyNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCompany).ToString());
                        mPayTypeLedger.ChargesPerce = (txtChrg2.Text == "") ? 0 : Convert.ToDouble(txtChrg2.Text);

                        if (dbPayTypeLedger.AddMPayTypeLedger(mPayTypeLedger) == true)
                        {
                            drt = dtPayType.Select("PayTypeNo = 5");
                            dbPayTypeLedger = new DBMPayTypeLedger();
                            mPayTypeLedger = new MPayTypeLedger();
                            if (drt.Length > 0) mPayTypeLedger.PKPayTypeLedgerNo = Convert.ToInt64(drt[0]["PKPayTypeLedgerNo"].ToString());
                            else mPayTypeLedger.PKPayTypeLedgerNo = 0;

                            mPayTypeLedger.PayTypeNo = 5;
                            mPayTypeLedger.LedgerNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCreditCard).ToString());
                            mPayTypeLedger.CompanyNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCompany).ToString());
                            mPayTypeLedger.ChargesPerce = (txtChrg3.Text == "") ? 0 : Convert.ToDouble(txtChrg3.Text);

                            if (dbPayTypeLedger.AddMPayTypeLedger(mPayTypeLedger) == true)
                            {
                                drt = dtPayType.Select("PayTypeNo = 6");
                                dbPayTypeLedger = new DBMPayTypeLedger();
                                mPayTypeLedger = new MPayTypeLedger();
                                if (drt.Length > 0) mPayTypeLedger.PKPayTypeLedgerNo = Convert.ToInt64(drt[0]["PKPayTypeLedgerNo"].ToString());
                                else mPayTypeLedger.PKPayTypeLedgerNo = 0;

                                mPayTypeLedger.PayTypeNo = 6;
                                mPayTypeLedger.LedgerNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbFoodVoucher).ToString());
                                mPayTypeLedger.CompanyNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCompany).ToString());
                                mPayTypeLedger.ChargesPerce = (txtChrg4.Text == "") ? 0 : Convert.ToDouble(txtChrg4.Text);

                                if (dbPayTypeLedger.AddMPayTypeLedger(mPayTypeLedger) == true)
                                {
                                    OMMessageBox.Show("Payment Type Ledger Setting Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                                }
                                else
                                {
                                    OMMessageBox.Show("Food Voucher Settings Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                OMMessageBox.Show("Credit Card Settings Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            OMMessageBox.Show("Cheque Settings Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        OMMessageBox.Show("Cash Settings Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
                int Cnt = -1;
                dbPayType = new DBMPayType();
                for (int i = 0; i < GridView.Rows.Count; i++)
                {
                    if (GridView.Rows[i].Cells[ColIndex.PayTypeName].Value != null && GridView.Rows[i].Cells[ColIndex.PayTypeName].Value.ToString() != "")
                    {
                        Cnt++;
                        mPayType = new MPayType();
                        mPayType.PKPayTypeNo = (GridView.Rows[i].Cells[ColIndex.PayTypeNo].Value == null || GridView.Rows[i].Cells[ColIndex.PayTypeNo].Value.ToString() == "") ? 0 : Convert.ToInt64(GridView.Rows[i].Cells[ColIndex.PayTypeNo].Value);
                        mPayType.PayTypeName = GridView.Rows[i].Cells[ColIndex.PayTypeName].FormattedValue.ToString();
                        mPayType.ShortName = GridView.Rows[i].Cells[ColIndex.ShortName].FormattedValue.ToString();
                        mPayType.IsActive = Convert.ToBoolean(GridView.Rows[i].Cells[ColIndex.IsActive].FormattedValue);
                        mPayType.ControlUnder = Convert.ToInt64(GridView.Rows[i].Cells[ColIndex.ControlUnder].FormattedValue);
                        mPayType.UserID = DBGetVal.UserID;
                        mPayType.UserDate = DBGetVal.ServerTime.Date;
                        mPayType.CompanyNo = DBGetVal.FirmNo;
                        dbPayType.AddMPayType(mPayType);

                        mPayTypeLedger = new MPayTypeLedger();
                        mPayTypeLedger.PKPayTypeLedgerNo = (GridView.Rows[i].Cells[ColIndex.PkSrNo].Value == null || GridView.Rows[i].Cells[ColIndex.PkSrNo].Value.ToString() == "") ? 0 : Convert.ToInt64(GridView.Rows[i].Cells[ColIndex.PkSrNo].Value);
                        mPayTypeLedger.LedgerNo = Convert.ToInt64(GridView.Rows[i].Cells[ColIndex.LedgerNo].Value);
                        mPayTypeLedger.CompanyNo = Convert.ToInt64(ObjFunction.GetComboValue(cmbCompany).ToString());
                        mPayTypeLedger.ChargesPerce = (GridView.Rows[i].Cells[ColIndex.ChrgPerce].Value == null && GridView.Rows[i].Cells[ColIndex.ChrgPerce].Value.ToString() == "") ? 0 : Convert.ToDouble(GridView.Rows[i].Cells[ColIndex.ChrgPerce].Value);
                        dbPayType.AddMPayTypeLedger(mPayTypeLedger);
                    }

                }
                if (Cnt > -1)
                {
                    if (dbPayType.ExecuteNonQueryStatements() == true)
                    {
                        OMMessageBox.Show("Pay Type Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
                if (isPayTypeSettingChanged)
                {

                    if (dbMSettings.ExecuteNonQueryStatements() == true)
                    {
                        ObjFunction.SetAppSettings();
                        OMMessageBox.Show("Pay Type Settings Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }

                    else
                    {
                        OMMessageBox.Show("Pay Type Settings Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }

                if (isPayTypeLedgerDataChanged || isPayTypeSettingChanged)
                {
                    FillControls();
                    cmbCompany_SelectedIndexChanged(cmbCompany, null);
                }
            }
        }

        private static class ColIndex
        {
            public static int PkSrNo = 0;
            public static int PayTypeName = 1;
            public static int ControlUnderName = 2;
            public static int LedgerName = 3;
            public static int ShortName = 4;
            public static int IsActive = 5;
            public static int PayTypeNo = 6;
            public static int LedgerNo = 7;
            public static int ControlUnder = 8;
            public static int ChrgPerce = 9;
        }

        private bool Validations()
        {
            EP.SetError(cmbCompany, ""); EP.SetError(cmbCash, "");
            EP.SetError(cmbCheque, ""); EP.SetError(cmbCreditCard, "");
            EP.SetError(cmbFoodVoucher, "");
            bool flag = false;
            if (isPayTypeLedgerDataChanged)
            {
                flag = true;
                if (ObjFunction.GetComboValue(cmbCompany).ToString() == "0")
                {
                    EP.SetError(cmbCompany, "Please select proper Company / Firm.");
                    EP.SetIconAlignment(cmbCompany, ErrorIconAlignment.MiddleRight);
                    if (flag) { cmbCompany.Focus(); flag = false; }
                }
                if (ObjFunction.GetComboValue(cmbCash).ToString() == "0")
                {
                    EP.SetError(cmbCash, "Please select proper Ledger Name for Cash Payment Type.");
                    EP.SetIconAlignment(cmbCash, ErrorIconAlignment.MiddleRight);
                    if (flag) { cmbCash.Focus(); flag = false; }
                }
                if (ObjFunction.GetComboValue(cmbCheque).ToString() == "0")
                {
                    EP.SetError(cmbCheque, "Please select proper Ledger Name for Cheque Payment Type.");
                    EP.SetIconAlignment(cmbCheque, ErrorIconAlignment.MiddleRight);
                    if (flag) { cmbCheque.Focus(); flag = false; }
                }
                if (ObjFunction.GetComboValue(cmbCreditCard).ToString() == "0")
                {
                    EP.SetError(cmbCreditCard, "Please select proper Ledger Name for Credit Card Payment Type.");
                    EP.SetIconAlignment(cmbCreditCard, ErrorIconAlignment.MiddleRight);
                    if (flag) { cmbCreditCard.Focus(); flag = false; }
                }
                if (ObjFunction.GetComboValue(cmbFoodVoucher).ToString() == "0")
                {
                    EP.SetError(cmbFoodVoucher, "Please select proper Ledger Name for Food voucher Type.");
                    EP.SetIconAlignment(cmbFoodVoucher, ErrorIconAlignment.MiddleRight);
                    if (flag) { cmbFoodVoucher.Focus(); flag = false; }
                }
            }
            return flag;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPayTypeLedger_SelectedValueChanged(object sender, EventArgs e)
        {
            isPayTypeLedgerDataChanged = true;            
        }

        private void cmbChequePayTypeSettings_SelectedValueChanged(object sender, EventArgs e)
        {
            isPayTypeSettingChanged = true;
        }

        private void cmbFoodVoucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            isPayTypeLedgerDataChanged = true;
        }

        private void GridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.PayTypeName)
            {
                isPayTypeLedgerDataChanged = true;
                if (GridView.Rows[e.RowIndex].Cells[ColIndex.PayTypeName].Value == null || GridView.Rows[e.RowIndex].Cells[ColIndex.PayTypeName].Value.ToString() == "")
                {
                    GridView.Rows[e.RowIndex].Cells[ColIndex.PayTypeName].ErrorText = "Enter PayType";
                }
                else
                {
                    GridView.Rows[e.RowIndex].Cells[ColIndex.PayTypeName].ErrorText = "";
                    pnlPayType.Visible = true;
                    lstPayType.Focus();


                }
            }
            else if (e.ColumnIndex == ColIndex.ShortName)
            {
                isPayTypeLedgerDataChanged = true;
                if (GridView.Rows[e.RowIndex].Cells[ColIndex.ShortName].Value == null || GridView.Rows[e.RowIndex].Cells[ColIndex.ShortName].Value.ToString() == "")
                {
                    GridView.Rows[e.RowIndex].Cells[ColIndex.ShortName].ErrorText = "Enter ShortName";
                }
                else
                {
                    GridView.Rows[e.RowIndex].Cells[ColIndex.ShortName].ErrorText = "";
                    GridView.CurrentCell = GridView[ColIndex.ShortName, GridView.CurrentCell.RowIndex];
                    GridView.Focus();
                }
            }
            else if (e.ColumnIndex == ColIndex.IsActive)
            {
                isPayTypeLedgerDataChanged = true;
            }

        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (GridView.CurrentCell.ColumnIndex == ColIndex.PayTypeName)
                {
                    if (GridView.CurrentCell.Value == null || GridView.CurrentCell.Value.ToString() == "")
                    {
                        GridView.CurrentCell.ErrorText = "Enter PayTypeName";
                        GridView.CurrentCell = GridView[ColIndex.PayTypeName, GridView.CurrentCell.RowIndex];
                        GridView.Focus();
                    }
                    else
                    {
                        if (GridView.Rows[GridView.CurrentRow.Index].ReadOnly == false)
                        {
                            pnlPayType.Visible = true;
                            lstPayType.Focus();
                        }
                    }
                }
                else if (GridView.CurrentCell.ColumnIndex == ColIndex.LedgerName)
                {
                    GridView.CurrentCell = GridView[ColIndex.ShortName, GridView.CurrentCell.RowIndex];
                    GridView.Focus();
                }
                else if (GridView.CurrentCell.ColumnIndex == ColIndex.ShortName)
                {
                    if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ShortName].Value == null || GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ShortName].Value.ToString() == "")
                    {
                        GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ShortName].ErrorText = "Enter ShortName";
                    }
                    else
                    {
                        GridView.CurrentCell = GridView[ColIndex.ChrgPerce, GridView.CurrentCell.RowIndex];
                        GridView.Focus();
                    }
                }
                else if (GridView.CurrentCell.ColumnIndex == ColIndex.ControlUnderName)
                {
                    if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ShortName].ErrorText == "")
                    {
                        if (GridView.Rows[GridView.CurrentRow.Index].ReadOnly == false)
                        {
                            pnlPayType.Visible = true;
                            lstPayType.Focus();
                        }
                    }
                    else
                    {
                        GridView.CurrentCell = GridView[ColIndex.ShortName, GridView.CurrentCell.RowIndex];
                        GridView.Focus();
                    }
                }

            }
            else if (e.KeyCode == Keys.F4)
            {
                if (GridView.CurrentCell.ColumnIndex == ColIndex.ControlUnderName)
                {
                    if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ShortName].ErrorText == "")
                    {
                        if (GridView.Rows[GridView.CurrentRow.Index].ReadOnly == false)
                        {
                            pnlPayType.Visible = true;
                            lstPayType.Focus();
                        }
                    }
                    else
                    {
                        GridView.CurrentCell = GridView[ColIndex.ShortName, GridView.CurrentCell.RowIndex];
                        GridView.Focus();
                    }

                    //if (GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.PayTypeName].ErrorText == "")
                    //{
                    //    pnlLedger.Visible = true;
                    //    lstLedger.Focus();
                    //}
                    //else
                    //{
                    //    GridView.CurrentCell = GridView[ColIndex.PayTypeName, GridView.CurrentCell.RowIndex];
                    //    GridView.Focus();
                    //}
                }
                //pnlLedger.Visible = true;
                //lstLedger.Focus();
            }
        }

        private void lstLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerName].Value = lstLedger.Text;
                GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = lstLedger.SelectedValue;
                pnlLedger.Visible = false;
                isPayTypeLedgerDataChanged = true;
                GridView.CurrentCell = GridView[ColIndex.ShortName, GridView.CurrentCell.RowIndex];
                GridView.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
                pnlLedger.Visible = false;
                GridView.CurrentCell = GridView[ColIndex.LedgerName, GridView.CurrentCell.RowIndex];
                GridView.Focus();

            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            bool flag = true;
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                if (GridView.Rows[i].Cells[ColIndex.PayTypeName].Value == null || GridView.Rows[i].Cells[ColIndex.PayTypeName].Value.ToString() == "" 
                || GridView.Rows[i].Cells[ColIndex.ControlUnderName].Value == null || GridView.Rows[i].Cells[ColIndex.ControlUnderName].Value.ToString() == "" 
                   || GridView.Rows[i].Cells[ColIndex.LedgerName].Value == null || GridView.Rows[i].Cells[ColIndex.LedgerName].Value.ToString() == "" 
                   || GridView.Rows[i].Cells[ColIndex.ShortName].Value == null || GridView.Rows[i].Cells[ColIndex.ShortName].Value.ToString() == "")
                {
                    flag = false;
                }
            }
            if (flag == true)
            {
                GridView.Rows.Add();
                GridView.CurrentCell = GridView[ColIndex.PayTypeName, GridView.Rows.Count - 1];
                GridView.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (GridView.Rows[GridView.Rows.Count - 1].Cells[ColIndex.PkSrNo].Value == null || GridView.Rows[GridView.Rows.Count - 1].Cells[ColIndex.PkSrNo].Value.ToString() == "")
            {
                GridView.Rows.RemoveAt(GridView.Rows.Count - 1);
            }
        }

        private void lstPayType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ControlUnderName].Value = lstPayType.Text;
                GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.ControlUnder].Value = lstPayType.SelectedValue;
                DataTable dt = ObjFunction.GetDataView("SELECT MPayTypeLedger.LedgerNo, MLedger.LedgerName FROM MPayTypeLedger INNER JOIN MLedger ON MPayTypeLedger.LedgerNo = MLedger.LedgerNo " +
                    " WHERE (MPayTypeLedger.PayTypeNo = " + lstPayType.SelectedValue + ")").Table;
                if (dt.Rows.Count > 0)
                {
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerNo].Value = dt.Rows[0].ItemArray[0].ToString();
                    GridView.Rows[GridView.CurrentCell.RowIndex].Cells[ColIndex.LedgerName].Value = dt.Rows[0].ItemArray[1].ToString();
                }
                pnlPayType.Visible = false;
                isPayTypeLedgerDataChanged = true;
                GridView.CurrentCell = GridView[ColIndex.ShortName, GridView.CurrentCell.RowIndex];
                GridView.Focus();
            }
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
                pnlPayType.Visible = false;
                GridView.CurrentCell = GridView[ColIndex.ControlUnderName, GridView.CurrentCell.RowIndex];
                GridView.Focus();

            }
        }

        #region KeyDown Events
        private void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
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
                if (e.KeyCode == Keys.F2)
                {
                    if (btnAddRow.Enabled) btnAddRow_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F6)
                {
                    if (btnCancel.Enabled) btnCancel_Click(sender, e);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
        #endregion

        private void PayTypeSetting_Activated(object sender, EventArgs e)
        {
            FillControls();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            isPayTypeLedgerDataChanged = true;
            ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
        }

        private void GridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (GridView.CurrentCell.ColumnIndex == ColIndex.ChrgPerce)
                {
                    TextBox txt = (TextBox)e.Control;
                    txt.TextChanged += new EventHandler(txtChrgPerce_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void txtChrgPerce_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GridView.CurrentCell.ColumnIndex == ColIndex.ChrgPerce)
                {
                    isPayTypeLedgerDataChanged = true;
                    ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}
