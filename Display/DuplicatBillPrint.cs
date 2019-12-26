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

namespace Yadi.Display
{
    public partial class DuplicatBillPrint : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        DataTable dtMfgs = null;
        public DuplicatBillPrint()
        {
            InitializeComponent();
        }

        private void DuplicatBillPrint_Load(object sender, EventArgs e)
        {
            DtpFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DtpToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DtpToDate.MinDate = DtpFromDate.Value;
            string sql = "SELECT MfgCompNo, MfgCompName FROM MManufacturerCompany";
            ObjFunction.FillCombo(cmbFirm, sql, "All Firm");
            ObjFunction.FillCombo(cmbPartName, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryDebtors + ") and IsActive='true' order by LedgerName");
            rbType_CheckedChanged(sender, new EventArgs());
            cmbFirm.Focus();

            KeyDownFormat(this.Controls);
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPartyWise.Checked == true)
            {
                InitControls();
                pnlDateWise.Visible = true;
                cmbPartName.Visible = true;
                label2.Visible = true;
                pnlBillNoWise.Visible = false;
                dgBill.Columns[ColIndex.LedgerName].Visible = false;
            }
            else if (rbDateWise.Checked == true)
            {
                InitControls();
                pnlDateWise.Visible = true;
                cmbPartName.Visible = false;
                label2.Visible = false;
                pnlBillNoWise.Visible = false;
                dgBill.Columns[ColIndex.LedgerName].Visible = true;
            }
            else if (rdBillNoWise.Checked == true)
            {
                InitControls();
                pnlDateWise.Visible = false;
                pnlBillNoWise.Visible = true;
                pnlBillNoWise.Location = pnlDateWise.Location;
                dgBill.Columns[ColIndex.LedgerName].Visible = true;
            }
            cmbFirm.Focus();
        }

        public void InitControls()
        {
            while (dgBill.Rows.Count > 0)
                dgBill.Rows.RemoveAt(0);

            cmbFirm.SelectedIndex = 0;
            cmbPartName.SelectedIndex = 0;
            txtFromNo.Text = "";
            txtToNo.Text = "";
            DtpFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DtpToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DtpToDate.MinDate = DtpFromDate.Value;
        }

        private void txtSetMask_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), -1, 18, OMFunctions.MaskedType.NotNegative);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool Validation()
        {
            bool flag = true;
            EP.SetError(cmbFirm, "");
            EP.SetError(cmbPartName, "");
            EP.SetError(txtFromNo, "");
            EP.SetError(txtToNo, "");

            if (rbPartyWise.Checked == true)
            {
                if (ObjFunction.GetComboValue(cmbFirm) <= 0 && ObjFunction.GetComboValue(cmbFirm) != -1)
                {
                    EP.SetError(cmbFirm, "Select Firm");
                    EP.SetIconAlignment(cmbFirm, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; cmbFirm.Focus(); }
                }
                else if (ObjFunction.GetComboValue(cmbPartName) <= 0)
                {
                    EP.SetError(cmbPartName, "Select Party");
                    EP.SetIconAlignment(cmbPartName, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; cmbPartName.Focus(); }
                }
            }
            else if (rbDateWise.Checked == true)
            {
                if (ObjFunction.GetComboValue(cmbFirm) <= 0 && ObjFunction.GetComboValue(cmbFirm) != -1)
                {
                    EP.SetError(cmbFirm, "Select Firm");
                    EP.SetIconAlignment(cmbFirm, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; cmbFirm.Focus(); }
                }
            }
            else if (rdBillNoWise.Checked == true && ObjFunction.GetComboValue(cmbFirm) != -1)
            {
                if (ObjFunction.GetComboValue(cmbFirm) <= 0)
                {
                    EP.SetError(cmbFirm, "Select Firm");
                    EP.SetIconAlignment(cmbFirm, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; cmbFirm.Focus(); }
                }
                else if (txtFromNo.Text.Trim() == "")
                {
                    EP.SetError(txtFromNo, "Enter Bill From No ");
                    EP.SetIconAlignment(txtFromNo, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtFromNo.Focus(); }
                }
                else if (txtToNo.Text.Trim() == "")
                {
                    EP.SetError(txtToNo, "Enter Bill From No ");
                    EP.SetIconAlignment(txtToNo, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtToNo.Focus(); }
                }
            }
            return flag;
        }

        #region KeyDown Events
        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                chkSelectAll.Checked = !chkSelectAll.Checked;
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    dgBill.Rows[i].Cells[ColIndex.Select].Value = chkSelectAll.Checked;
                }
                btnPrint.Focus();
            }
        }

        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else
                    KeyDownFormat(ctrl.Controls);
            }
        }
        #endregion

        private static class ColIndex
        {
            public static int SrNo = 0;
            public static int Date = 1;
            public static int BillNo = 2;
            public static int LedgerName = 3;
            public static int BillAmount = 4;
            public static int PkVoucherNo = 5;
            public static int Select = 6;
            public static int DiscAmt = 7;
            public static int OrderType = 8;
            public static int MixMode = 9;
            public static int PayTypeName = 10;
            public static int MfgCompNo = 11;
            public static int MfgCompName = 12;

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                string sql = "";
                chkSelectAll.Checked = false;
                while (dgBill.Rows.Count > 0)
                    dgBill.Rows.RemoveAt(0);

                DataTable dt = new DataTable();
                if (rbPartyWise.Checked == true)
                {
                    if (ObjFunction.GetComboValue(cmbFirm) == -1)
                    {
                        sql = " SELECT 0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo AS BillNo, '' AS LedgerName, TVoucherEntry.BilledAmount AS Amount, TVoucherEntry.PkVoucherNo,'false' As 'chkSelect' ,TVoucherEntry.DiscAmt,TVoucherEntry.OrderType,TVoucherEntry.MixMode, MPayType.PayTypeName,'' as MfgCompNo, '' as  MfgCompName" +
                              " FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo  INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo " +
                              " WHERE (TVoucherEntry.VoucherTypeCode=" + VchType.Sales + ") And (TVoucherEntry.IsCancel='false') and (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartName) + ") AND (TVoucherEntry.VoucherDate >= '" + DtpFromDate.Text + "') AND (TVoucherEntry.VoucherDate <= '" + DtpToDate.Text + "') And TVoucherEntry.IsBillMulti=1" +
                              " Order By TVoucherEntry.VoucherDate,BillNo ";
                    }
                    else
                    {
                        sql = " SELECT 0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo AS BillNo, '' AS LedgerName, TVoucherEntry.BilledAmount AS Amount, TVoucherEntry.PkVoucherNo,'false' As 'chkSelect' ,TVoucherEntry.DiscAmt,TVoucherEntry.OrderType,TVoucherEntry.MixMode, MPayType.PayTypeName,TVoucherEntryCompany.MfgCompNo, MManufacturerCompany.MfgCompName" +
                              " FROM  TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo  INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo  INNER JOIN TVoucherEntryCompany ON TVoucherEntry.PkVoucherNo  = TVoucherEntryCompany.FkVoucherNo  INNER JOIN MManufacturerCompany ON TVoucherEntryCompany.MfgCompNo = MManufacturerCompany.MfgCompNo  " +
                              " WHERE (TVoucherEntry.VoucherTypeCode=" + VchType.Sales + ") And (TVoucherEntry.IsCancel='false') and (TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartName) + ") AND PkVoucherNo in(Select TV.FKVoucherNo FRom TVoucherEntryCompany TV Where TV.MfgCompNo=" + ((ObjFunction.GetComboValue(cmbFirm) == -1) ? "TV.MfgCompNo" : ObjFunction.GetComboValue(cmbFirm).ToString()) + " AND TV.FKVoucherNo=TVoucherEntry.PKVoucherNo) AND (TVoucherEntry.VoucherDate >= '" + DtpFromDate.Text + "') AND (TVoucherEntry.VoucherDate <= '" + DtpToDate.Text + "') And TVoucherEntry.IsBillMulti=0" +
                              " Order By TVoucherEntry.VoucherDate,BillNo ";
                    }
                }
                else if (rbDateWise.Checked == true)
                {
                    if (ObjFunction.GetComboValue(cmbFirm) == -1)
                    {
                        sql = " SELECT 0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo AS BillNo, MLedger.LedgerName, TVoucherEntry.BilledAmount AS Amount, TVoucherEntry.PkVoucherNo,'false' As 'chkSelect'  ,TVoucherEntry.DiscAmt,TVoucherEntry.OrderType,TVoucherEntry.MixMode, MPayType.PayTypeName,'' as MfgCompNo, '' as MfgCompName " +
                             " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo   " +
                             " WHERE (TVoucherEntry.VoucherTypeCode=" + VchType.Sales + ") And (TVoucherEntry.IsCancel='false') and (TVoucherDetails.SrNo = 501) AND (TVoucherEntry.VoucherDate >= '" + DtpFromDate.Text + "') AND (TVoucherEntry.VoucherDate <= '" + DtpToDate.Text + "') And TVoucherEntry.IsBillMulti=1" +
                             " Order By TVoucherEntry.VoucherDate,BillNo ";
                    }
                    else
                    {
                        sql = " SELECT 0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo AS BillNo, MLedger.LedgerName, TVoucherEntry.BilledAmount AS Amount, TVoucherEntry.PkVoucherNo,'false' As 'chkSelect'  ,TVoucherEntry.DiscAmt,TVoucherEntry.OrderType,TVoucherEntry.MixMode, MPayType.PayTypeName, TVoucherEntryCompany.MfgCompNo, MManufacturerCompany.MfgCompName " +
                              " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo   INNER JOIN TVoucherEntryCompany ON TVoucherEntry.PkVoucherNo  = TVoucherEntryCompany.FkVoucherNo  INNER JOIN MManufacturerCompany ON TVoucherEntryCompany.MfgCompNo = MManufacturerCompany.MfgCompNo" +
                              " WHERE (TVoucherEntry.VoucherTypeCode=" + VchType.Sales + ") And (TVoucherEntry.IsCancel='false') and (TVoucherDetails.SrNo = 501) AND PkVoucherNo in(Select TV.FKVoucherNo FRom TVoucherEntryCompany TV Where TV.MfgCompNo=" + ((ObjFunction.GetComboValue(cmbFirm) == -1) ? "TV.MfgCompNo" : ObjFunction.GetComboValue(cmbFirm).ToString()) + " AND TV.FKVoucherNo=TVoucherEntry.PKVoucherNo) AND (TVoucherEntry.VoucherDate >= '" + DtpFromDate.Text + "') AND (TVoucherEntry.VoucherDate <= '" + DtpToDate.Text + "') And TVoucherEntry.IsBillMulti=0" +
                              " Order By TVoucherEntry.VoucherDate,BillNo ";
                    }
                }
                else if (rdBillNoWise.Checked == true)
                {
                    if (ObjFunction.GetComboValue(cmbFirm) == -1)
                    {
                        sql = " SELECT 0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo AS BillNo, MLedger.LedgerName, TVoucherEntry.BilledAmount AS Amount, TVoucherEntry.PkVoucherNo,'false' As 'chkSelect'  ,TVoucherEntry.DiscAmt,TVoucherEntry.OrderType,TVoucherEntry.MixMode, MPayType.PayTypeName,'' as MfgCompNo, '' as MfgCompName" +
                              " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo  " +
                              " WHERE  (TVoucherEntry.IsBillMulti=1) and  (TVoucherEntry.VoucherTypeCode=" + VchType.Sales + ") And (TVoucherEntry.IsCancel='false') and  (TVoucherDetails.SrNo = 501) ";
                    }
                    else
                    {
                        sql = " SELECT 0 AS SrNo, TVoucherEntry.VoucherDate, TVoucherEntry.VoucherUserNo AS BillNo, MLedger.LedgerName, TVoucherEntry.BilledAmount AS Amount, TVoucherEntry.PkVoucherNo,'false' As 'chkSelect'  ,TVoucherEntry.DiscAmt,TVoucherEntry.OrderType,TVoucherEntry.MixMode, MPayType.PayTypeName, TVoucherEntryCompany.MfgCompNo, MManufacturerCompany.MfgCompName" +
                                                     " FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo INNER JOIN MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo  INNER JOIN MPayType ON TVoucherEntry.PayTypeNo = MPayType.PKPayTypeNo  INNER JOIN TVoucherEntryCompany ON TVoucherEntry.PkVoucherNo  = TVoucherEntryCompany.FkVoucherNo  INNER JOIN MManufacturerCompany ON TVoucherEntryCompany.MfgCompNo = MManufacturerCompany.MfgCompNo" +
                                                     " WHERE  (TVoucherEntry.IsBillMulti=0) and (TVoucherEntry.VoucherTypeCode=" + VchType.Sales + ") And (TVoucherEntry.IsCancel='false') and  (TVoucherDetails.SrNo = 501) AND PkVoucherNo in(Select TV.FKVoucherNo FRom TVoucherEntryCompany TV Where TV.MfgCompNo=" + ((ObjFunction.GetComboValue(cmbFirm) == -1) ? "TV.MfgCompNo" : ObjFunction.GetComboValue(cmbFirm).ToString()) + " AND TV.FKVoucherNo=TVoucherEntry.PKVoucherNo)   ";
                    }



                    if (txtFromNo.Text.Trim() != "")
                        sql = sql + " And (TVoucherEntry.VoucherUserNo >= " + txtFromNo.Text + ") ";
                    if (txtToNo.Text.Trim() != "")
                        sql = sql + " AND (TVoucherEntry.VoucherUserNo <= " + txtToNo.Text + ")  ";
                    sql = sql + " Order By TVoucherEntry.VoucherDate,BillNo ";
                }
                else return;

                dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgBill.Rows.Add();
                    for (int j = 0; j < dgBill.ColumnCount; j++)
                    {
                        dgBill.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                    }
                }

                if (dgBill.Rows.Count > 0)
                {
                    dgBill.CurrentCell = dgBill.Rows[0].Cells[ColIndex.Select];
                    dgBill.Focus();
                }
                else
                {
                    DisplayMessage("Bill Not Found");
                    DtpFromDate.Focus();
                }
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

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
                e.Value = e.RowIndex + 1;
            else if (e.ColumnIndex == ColIndex.Date)
                e.Value = Convert.ToDateTime(e.Value).ToString(Format.DDMMMYYYY);
        }

        private void DtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            DtpToDate.MinDate = DtpFromDate.Value;
        }

        private void cmbPartName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnShow.Focus();
            }
        }

        private void cmbFirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (rbPartyWise.Checked == true || rbDateWise.Checked == true)
                    DtpFromDate.Focus();
                else
                    txtFromNo.Focus();

            }
        }

        private void txtToNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnShow.Focus();
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgBill.Rows.Count; i++)
            {
                dgBill.Rows[i].Cells[ColIndex.Select].Value = chkSelectAll.Checked;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int cntRow = 0;
            for (int i = 0; i < dgBill.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.Select].FormattedValue) == true)
                {
                    cntRow = 1;
                    break;
                }
            }
            if (cntRow == 0)
            {
                OMMessageBox.Show("Please select atleast one bill", CommonFunctions.ConStr, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                return;
            }

            if (ObjFunction.GetComboValue(cmbFirm) > 0)
            {
                PrintBillFrimWise();
            }
            else
                PrintBill1();

        }

        public void PrintBill()
        {
            string str = "";


            for (int i = 0; i < dgBill.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.Select].FormattedValue) == true)
                {
                    if (str == "")
                        str = dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString();
                    else
                        str = str + "," + dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString();
                }
            }
            if (str != "")
            {
                if (OMMessageBox.Show("Are you sure you want to Print this bill ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    string[] ReportSession;

                    ReportSession = new string[5];
                    ReportSession[0] = str;
                    ReportSession[1] = "";//ObjQry.ReturnLong("Select Max(PkVoucherNo) FRom TVoucherEntry Where VoucherTypeCode=" + ((flagPP == true) ? VchType.Sales : VoucherType) + "", CommonFunctions.ConStr).ToString();
                    ReportSession[2] = DBGetVal.CompanyAddress;
                    ReportSession[3] = DBGetVal.UserName;
                    ReportSession[4] = "1";

                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        childForm = ObjFunction.GetReportObject("Reports.GetBigBillBulk");
                    else
                        childForm = ObjFunction.LoadReportObject("GetBigBillBulk.rpt", CommonFunctions.ReportPath);

                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        if (objRpt.PrintReport() == true)
                        {
                            DisplayMessage("Bill Print Successfully!!! ");

                        }
                        else
                        {
                            DisplayMessage("Bill not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
            }
            else
                OMMessageBox.Show("Select Atleast one Bill ", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);

        }
        
        private void DtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (rbDateWise.Checked == true)
                    btnShow.Focus();
                else
                    cmbPartName.Focus();
            }

        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnPrint.Focus();
            }
        }

        public void PrintBillFrimWise()
        {
            try
            {
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.Select].FormattedValue) == true)
                    {
                        dtMfgs = ObjFunction.GetDataView("Select MfgCompAddress,EmailID,PhoneNo From MManufacturerCompany Where MfgCompNo=" + dgBill[ColIndex.MfgCompNo, i].Value + "").Table;

                        string AddressPrint = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true)
                        {
                            if (Convert.ToInt64(dgBill[ColIndex.OrderType, i].Value) == 1)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillCouterBill)) == true)
                                    AddressPrint = "0";
                                else
                                    AddressPrint = "1";
                            }
                            if (Convert.ToInt64(dgBill[ColIndex.OrderType, i].Value) == 2)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillHomeDelivery)) == true)
                                    AddressPrint = "0";
                                else
                                    AddressPrint = "1";
                            }
                        }
                        double Amt = 0;
                        Amt = ObjQry.ReturnDouble("Select sum(Case When TStock.Rate <>0 Then (((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount) Else MRateSetting.MRP*TStock.Quantity END) FROM TStock INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                              " Where TStock.FkVoucherNo=" + Convert.ToInt64(dgBill[ColIndex.PkVoucherNo, i].Value) + " AND (Case When TStock.Rate <>0 Then ((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount Else MRateSetting.MRP*TStock.Quantity END)>0 ", CommonFunctions.ConStr);
                        Amt += Convert.ToDouble(dgBill[ColIndex.DiscAmt, i].Value);
                        string[] ReportSession;

                        ReportSession = new string[28];
                        ReportSession[0] = dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString();
                        ReportSession[1] = "";
                        ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                        ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                        ReportSession[4] = dgBill.Rows[i].Cells[ColIndex.BillAmount].Value.ToString();
                        ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                        ReportSession[6] = "Type: " + ((Convert.ToInt64(dgBill[ColIndex.MixMode, i].Value) == 1) ? "Mix Mode" : dgBill[ColIndex.PayTypeName, i].Value.ToString());
                        ReportSession[7] = "0";
                        ReportSession[8] = "0";
                        ReportSession[9] = "1";
                        ReportSession[10] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSavingBill)).ToString();
                        ReportSession[11] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)).ToString();
                        ReportSession[13] = (Convert.ToInt64(dgBill[ColIndex.MixMode, i].Value) == 1) ? "1" : "0";
                        ReportSession[14] = AddressPrint;//(Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                        ReportSession[15] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff)) == true) ? "1" : "2";
                        ReportSession[16] = (ObjQry.ReturnLong("Select Count(*) FRom TReward Where FkVoucherNo=" + Convert.ToInt64(dgBill[ColIndex.PkVoucherNo, i].Value) + "", CommonFunctions.ConStr) == 0) ? "0" : "1";
                        ReportSession[17] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSchemeDetails)) == true) ? "1" : "0";
                        ReportSession[18] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? "1" : "2";
                        ReportSession[19] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? "" : "";
                        ReportSession[20] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowVatNo)) == true) ? "1" : "2";
                        ReportSession[21] = dtMfgs.Rows[0].ItemArray[0].ToString();//Manufacturing Company Address
                        ReportSession[22] = dtMfgs.Rows[0].ItemArray[1].ToString(); ;//Email ID
                        ReportSession[23] = dtMfgs.Rows[0].ItemArray[2].ToString(); ;//Phone No
                        ReportSession[24] = ObjFunction.GetComboValue(cmbFirm).ToString();
                        ReportSession[25] = NumberToWordsIndian.getWords(dgBill[ColIndex.BillAmount, i].Value.ToString());
                        ReportSession[26] = dgBill[ColIndex.MfgCompName, i].Value.ToString().Replace("Firm Name :", "");
                        ReportSession[27] = (-1 * ObjQry.ReturnDouble("Select SUM(Credit) - SUM(Debit) " +
                                        " FROM TVoucherDetails INNER JOIN " +
                                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo " +
                                        " Where TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartName) +
                                        " AND TVoucherEntry.IsCancel = 'false' And TVoucherEntry.PkVoucherNo Not In(" + 
                                        Convert.ToInt64(dgBill[ColIndex.PkVoucherNo, i].Value) + ")", CommonFunctions.ConStr)).ToString("0.00");

                        #region New Code for Outstanding
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                        {
                            double TotalDues = 0;
                            TotalDues = ObjQry.ReturnDouble("Select " +
                                        " SUM(Credit) - SUM(Debit)" +
                                        " FROM TVoucherDetails INNER JOIN " +
                                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                                        " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                                        " Where TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartName) +
                                        " AND TVoucherEntry.IsCancel = 'false'", CommonFunctions.ConStr);
                            TotalDues = -1 * TotalDues;
                            if (TotalDues > 0)
                            {
                                ReportSession[12] = "Total Dues:" + TotalDues.ToString("0.00");
                            }
                            else if (TotalDues <= 0)
                                ReportSession[12] = "Total Dues: 0";
                        }
                        else
                            ReportSession[12] = "0";

                        #endregion

                        CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                        childForm = null;

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsFirmPayTypewisePrint)) == true)
                            {
                                if (dgBill[ColIndex.PayTypeName, i].Value.ToString().ToUpper() != "CASH")
                                    childForm = ObjFunction.GetReportObject("Reports.GetBigBillFirm" + dgBill[ColIndex.MfgCompNo, i].Value);
                                else
                                    childForm = ObjFunction.GetReportObject("Reports.GetBillFirm" + dgBill[ColIndex.MfgCompNo, i].Value);
                            }
                            else
                                childForm = ObjFunction.GetReportObject("Reports.GetBillFirm" + dgBill[ColIndex.MfgCompNo, i].Value);
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsFirmPayTypewisePrint)) == true)
                            {
                                if (dgBill[ColIndex.PayTypeName, i].Value.ToString().ToUpper() != "CASH")
                                    childForm = ObjFunction.LoadReportObject("GetBigBillFirm" + dgBill[ColIndex.MfgCompNo, i].Value + ".rpt", CommonFunctions.ReportPath);
                                else
                                    childForm = ObjFunction.LoadReportObject("GetBillFirm" + dgBill[ColIndex.MfgCompNo, i].Value + ".rpt", CommonFunctions.ReportPath);
                            }
                            else
                                childForm = ObjFunction.LoadReportObject("GetBillFirm" + dgBill[ColIndex.MfgCompNo, i].Value + ".rpt", CommonFunctions.ReportPath);

                        }
                        if (childForm != null)
                        {
                            DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                            objRpt.OwnPrinterName = ObjFunction.GetPrinterName(Convert.ToInt64(dgBill[ColIndex.MfgCompNo, i].Value));
                            if (objRpt.PrintReport() == true)
                            {
                                DisplayMessage("Bill Print Successfully!!!");
                            }
                            else
                            {
                                DisplayMessage("Bill not Print !!!");
                            }
                        }
                        else
                        {
                            DisplayMessage("Bill Report not exist !!!");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void PrintBill1()
        {
            try
            {
                

                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgBill.Rows[i].Cells[ColIndex.Select].FormattedValue) == true)
                    {
                        string AddressPrint = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true)
                        {
                            if (Convert.ToInt64(dgBill[ColIndex.OrderType, i].Value) == 1)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillCouterBill)) == true)
                                    AddressPrint = "0";
                                else
                                    AddressPrint = "1";
                            }
                            if (Convert.ToInt64(dgBill[ColIndex.OrderType, i].Value) == 2)
                            {
                                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBillHomeDelivery)) == true)
                                    AddressPrint = "0";
                                else
                                    AddressPrint = "1";
                            }
                        }
                        double Amt = 0;
                        Amt = ObjQry.ReturnDouble("Select sum(Case When TStock.Rate <>0 Then (((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount) Else MRateSetting.MRP*TStock.Quantity END) FROM TStock INNER JOIN MRateSetting ON TStock.FkRateSettingNo = MRateSetting.PkSrNo " +
                             " Where TStock.FkVoucherNo=" + Convert.ToInt64(dgBill[ColIndex.PkVoucherNo, i].Value) + " AND (Case When TStock.Rate <>0 Then ((TStock.Amount+TStock.DiscRupees) * CASE WHEN (MRateSetting.MRP = 0) THEN TStock.Rate ELSE MRateSetting.MRP END/TStock.Rate)-TStock.Amount Else MRateSetting.MRP*TStock.Quantity END)>0 ", CommonFunctions.ConStr);
                        Amt += Convert.ToDouble(dgBill[ColIndex.DiscAmt, i].Value);
                        string[] ReportSession;

                        ReportSession = new string[21];
                        ReportSession[0] = dgBill.Rows[i].Cells[ColIndex.PkVoucherNo].Value.ToString();
                        ReportSession[1] = "";
                        ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.S_SettingValue).ToString();
                        ReportSession[3] = ObjFunction.GetAppSettings(AppSettings.S_FooterValue).ToString();
                        ReportSession[4] = dgBill.Rows[i].Cells[ColIndex.BillAmount].Value.ToString();
                        ReportSession[5] = ObjFunction.GetAppSettings(AppSettings.S_Footer2Value).ToString();
                        ReportSession[6] = "Type: " + ((Convert.ToInt64(dgBill[ColIndex.MixMode, i].Value) == 1) ? "Mix Mode" : dgBill[ColIndex.PayTypeName, i].Value.ToString());
                        ReportSession[7] = "0";
                        ReportSession[8] = "0";
                        ReportSession[9] = "1";
                        ReportSession[10] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSavingBill)).ToString();
                        ReportSession[11] = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)).ToString();
                        ReportSession[13] = (Convert.ToInt64(dgBill[ColIndex.MixMode, i].Value) == 1) ? "1" : "0";
                        ReportSession[14] = AddressPrint;//(Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAddressInBill)) == true) ? "0" : "1";
                        ReportSession[15] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillRoundOff)) == true) ? "1" : "2";
                        ReportSession[16] = (ObjQry.ReturnLong("Select Count(*) FRom TReward Where FkVoucherNo=" + Convert.ToInt64(dgBill[ColIndex.PkVoucherNo, i].Value) + "", CommonFunctions.ConStr) == 0) ? "0" : "1";
                        ReportSession[17] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowSchemeDetails)) == true) ? "1" : "0";
                        ReportSession[18] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? "1" : "2";
                        ReportSession[19] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowRemark)) == true) ? "" : "";
                        ReportSession[20] = (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowVatNo)) == true) ? "1" : "2";


                        #region New Code for Outstanding
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_ShowOutStanding)) == true)
                        {
                            double TotalDues = 0;
                            TotalDues = ObjQry.ReturnDouble("Select " +
                                        " SUM(Credit) - SUM(Debit) " +
                                        " FROM TVoucherDetails INNER JOIN " +
                                        " TVoucherEntry ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN " +
                                        " MLedger ON TVoucherDetails.LedgerNo = MLedger.LedgerNo " +
                                        " Where TVoucherDetails.LedgerNo = " + ObjFunction.GetComboValue(cmbPartName) +
                                        " AND TVoucherEntry.IsCancel = 'false'", CommonFunctions.ConStr);
                            TotalDues = -1 * TotalDues;
                            if (TotalDues > 0)
                            {
                                ReportSession[12] = "Total Dues:" + TotalDues.ToString("0.00");
                            }
                            else if (TotalDues <= 0)
                                ReportSession[12] = "Total Dues: 0";
                        }
                        else
                            ReportSession[12] = "0";

                        #endregion

                        CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                        childForm = null;
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                                childForm = ObjFunction.GetReportObject("Reports.GetBillMRP");
                            else
                                childForm = ObjFunction.GetReportObject("Reports.GetBill");
                        }
                        else
                        {
                            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBillWithMRP)) == true)
                                childForm = ObjFunction.LoadReportObject("GetBillMRP.rpt", CommonFunctions.ReportPath);
                            else
                                childForm = ObjFunction.LoadReportObject("GetBill.rpt", CommonFunctions.ReportPath);
                        }

                        if (childForm != null)
                        {
                            DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                            if (objRpt.PrintReport() == true)
                            {
                                DisplayMessage("Bill Print Successfully!!!");
                            }
                            else
                            {
                                DisplayMessage("Bill not Print !!!");
                            }
                        }
                        else
                        {
                            DisplayMessage("Bill Report not exist !!!");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}
