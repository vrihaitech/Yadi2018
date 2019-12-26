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
    public partial class Acheivers : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMScheme dbScheme = new DBMScheme();
        MSchemeAchievers mSchAch = new MSchemeAchievers();
        MSchemeAchieverDetails mSchAchDetails = new MSchemeAchieverDetails();

        public Acheivers()
        {
            InitializeComponent();
        }


        private void Acheivers_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbScheme, "Select SchemeNo,SchemeName From MScheme  where SchemeTypeNo=1 And IsActive<>0 and (IsActive in(2) or SchemePeriodTo<'" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "') Order by SchemeName");
            lblSchemeNumber.Text = "Scheme Number :";
            lblSchemePeriod.Text = "Scheme Period From :";
            lblRedemPeriod.Text = "Redemption Period From :";
            KeyDownFormat(this.Controls);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cmbScheme.SelectedIndex > 0)
            {
                Form NewF = null;
                string[] ReportSession;
                ReportSession = new string[6];
                ReportSession[0] = ObjFunction.GetComboValue(cmbScheme).ToString();
                ReportSession[1] = rdAcheivers.Checked == true ? "1" : "2";
                ReportSession[2] = lblSchemeNumber.Text.Replace("Scheme Number :", "");
                ReportSession[3] = lblSchemePeriod.Text.Replace("Scheme Period From :", "");
                ReportSession[4] = lblRedemPeriod.Text.Replace("Redemption Period From :", "");
                ReportSession[5] = cmbScheme.Text;


                //ReportSession[2] = DBGetVal.FirmNo.ToString();
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.GetAchievers(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetAchievers.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else
            {
                OMMessageBox.Show("Select Scheme Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                cmbScheme.Focus();
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            lblSchemeNumber.Text = "Scheme Number :";
            lblSchemePeriod.Text = "Scheme Period From :";
            lblRedemPeriod.Text = "Redemption Period From :";

            DataTable dtScheme = ObjFunction.GetDataView("Select SchemePeriodFrom,SchemePeriodTo,SchemeRedPeriodFrom,SchemeRedPeriodTo,schemeUserNo From MScheme Where SchemeNo=" + ObjFunction.GetComboValue(cmbScheme) + " ").Table;
            if (dtScheme.Rows.Count > 0)
            {
                lblSchemePeriod.Text = "Scheme Period From :" + Convert.ToDateTime(dtScheme.Rows[0].ItemArray[0].ToString()).ToString(Format.DDMMMYYYY);
                lblSchemePeriod.Text += " To " + Convert.ToDateTime(dtScheme.Rows[0].ItemArray[1].ToString()).ToString(Format.DDMMMYYYY);
                lblRedemPeriod.Text = "Redemption Period From :" + Convert.ToDateTime(dtScheme.Rows[0].ItemArray[2].ToString()).ToString(Format.DDMMMYYYY);
                lblRedemPeriod.Text += " To " + Convert.ToDateTime(dtScheme.Rows[0].ItemArray[3].ToString()).ToString(Format.DDMMMYYYY);
                lblSchemeNumber.Text += " " + dtScheme.Rows[0].ItemArray[4].ToString();
            }

            pnlDataInfo.Visible = false;
            dgAchiever.Rows.Clear();
            dgAchiever.Columns.Clear();
            DataTable dtAchiever = ObjFunction.GetDataView("Exec GetAcheivers " + ObjFunction.GetComboValue(cmbScheme) + "," + (rdAcheivers.Checked == true ? "1" : "2") + "").Table;
            DataTable dtMainAch = new DataTable();
            dtMainAch.Columns.Add("Amount");

            if (dtAchiever.Rows.Count > 0)
            {
                for (int i = 0; i < dtAchiever.Rows.Count; i++)
                {
                    DataRow[] dr = dtMainAch.Select("Amount=" + dtAchiever.Rows[i].ItemArray[3].ToString() + "");
                    if (dr.Length == 0)
                    {
                        DataRow dr1 = dtMainAch.NewRow();
                        dr1[0] = dtAchiever.Rows[i].ItemArray[3].ToString();
                        dtMainAch.Rows.Add(dr1);
                    }
                }

                DataTable dtAch = new DataTable();
                dtAch.Columns.Add("SrNo");
                dtAch.Columns.Add("LedgerNo");
                dtAch.Columns.Add("Customer");
                dtAch.Columns.Add("SchemeDetailsNo");
                for (int i = 0; i < dtMainAch.Rows.Count; i++)
                {
                    dtAch.Columns.Add(dtMainAch.Rows[i].ItemArray[0].ToString());
                }
                dtAch.Columns.Add("Check");
                dtAch.Columns.Add("IsChange");
                dtAch.Columns.Add("PkSrNo");

                for (int i = 0; i < dtAchiever.Rows.Count; i++)
                {
                    DataRow dr = dtAch.NewRow();
                    dr[0] = i + 1;
                    dr[1] = dtAchiever.Rows[i].ItemArray[0].ToString();
                    dr[2] = dtAchiever.Rows[i].ItemArray[1].ToString();
                    dr[3] = dtAchiever.Rows[i].ItemArray[4].ToString();
                    int col = 4;
                    for (col = 4; col < dtAch.Columns.Count - 3; col++)
                    {
                        dr[col] = 0;
                        if (dtAch.Columns[col].ColumnName == dtAchiever.Rows[i].ItemArray[3].ToString())
                            dr[col] = dtAchiever.Rows[i].ItemArray[2].ToString();
                    }

                    DataTable dt = ObjFunction.GetDataView("Select PkSrNo,IsActive From MSchemeAchievers Where LedgerNo=" + dtAchiever.Rows[i].ItemArray[0].ToString() + " AND SchemeNo=" + ObjFunction.GetComboValue(cmbScheme) + "").Table;
                    if (dt.Rows.Count > 0)
                    {
                        dr[col] = Convert.ToBoolean(dt.Rows[0].ItemArray[1]); col++;
                        dr[col] = 0; col++;
                        dr[col] = dt.Rows[0].ItemArray[0].ToString();
                    }
                    else
                    {
                        dr[col] = false; col++;
                        dr[col] = 0; col++;
                        dr[col] = 0;
                    }
                    //dr[col] = true; col++;
                    //dr[col] = ObjQry.ReturnLong("Select PkSrNo From MSchemeAchiever Where LedgerNo=" + dtAchiever.Rows[i].ItemArray[0].ToString() + " AND SchemeNo=" + ObjFunction.GetComboValue(cmbScheme) + "", CommonFunctions.ConStr);
                    dtAch.Rows.Add(dr);
                }

                for (int i = 0; i < dtAch.Columns.Count; i++)
                {
                    if (i != dtAch.Columns.Count - 3)
                    {
                        DataGridViewTextBoxColumn txt = new DataGridViewTextBoxColumn();
                        txt.Name = dtAch.Columns[i].ColumnName;
                        txt.HeaderText = dtAch.Columns[i].ColumnName;
                        txt.ReadOnly = true;
                        dgAchiever.Columns.Add(txt);
                    }
                    else
                    {
                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                        chk.Name = dtAch.Columns[i].ColumnName;
                        chk.HeaderText = dtAch.Columns[i].ColumnName;
                        dgAchiever.Columns.Add(chk);
                    }
                }

                for (int i = 0; i < dtAch.Rows.Count; i++)
                {
                    dgAchiever.Rows.Add();
                    for (int col = 0; col < dtAch.Columns.Count; col++)
                    {
                        dgAchiever.Rows[i].Cells[col].Value = dtAch.Rows[i].ItemArray[col];
                        if (col == dtAch.Columns.Count - 1)
                        {
                            bool FValue = ObjQry.ReturnBoolean("Select IsItemDiscStatus from MSchemeAchievers where PkSrNo =" + Convert.ToInt16(dtAch.Rows[i].ItemArray[dtAch.Columns.Count-1].ToString()) + "", CommonFunctions.ConStr);
                            long Cnt = ObjQry.ReturnLong("Select Count(PkSrNo) from MSchemeAchieverDetails where SchemeAchieverNo=" + Convert.ToInt16(dtAch.Rows[i].ItemArray[dtAch.Columns.Count - 1].ToString()) + "", CommonFunctions.ConStr);
                            if (FValue == true || Cnt > 1)
                            {
                                dgAchiever.Rows[i].Cells[dtAch.Columns.Count - 3].ReadOnly = true;
                                dgAchiever.Rows[i].DefaultCellStyle.BackColor = Color.SkyBlue;
                            }
                        }
                    }
                }

                dgAchiever.Columns[1].Visible = false;
                dgAchiever.Columns[0].Width = 52;
                dgAchiever.Columns[2].Width = 220;
                dgAchiever.Columns[3].Visible = false;
                for (int i = 4; i < dgAchiever.Columns.Count - 3; i++)
                {
                    dgAchiever.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgAchiever.Columns[i].Width = 75;
                }
                dgAchiever.Columns[dgAchiever.Columns.Count - 3].Width = 50;
                if (rdTAcheivers.Checked == true)
                {
                    dgAchiever.Columns[dgAchiever.Columns.Count - 3].Visible = false;
                    btnApplyAchievers.Visible = false;
                }
                else
                {
                    dgAchiever.Columns[dgAchiever.Columns.Count - 3].Visible = true;
                    btnApplyAchievers.Visible = true;
                }
                dgAchiever.Columns[dgAchiever.Columns.Count - 2].Visible = false;
                dgAchiever.Columns[dgAchiever.Columns.Count - 1].Visible = false;
                //dgAchiever.DataSource = dtAch.DefaultView;
                pnlDataInfo.Visible = true;
                if (rdTAcheivers.Checked == false)
                    dgAchiever.CurrentCell = dgAchiever[dgAchiever.Columns.Count - 3, 0];
                dgAchiever.Focus();

            }
            else
            {
                if (rdAcheivers.Checked == true)
                    OMMessageBox.Show("No Achievers For Selected Scheme", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                else
                    OMMessageBox.Show("No Tentative Achievers For Selected Scheme", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }

        }

        private void btnApplyAchievers_Click(object sender, EventArgs e)
        {
            if (Validations() == true)
            {
                dbScheme = new DBMScheme();
                mSchAch = new MSchemeAchievers();

                for (int i = 0; i < dgAchiever.Rows.Count; i++)
                {
                    if (Convert.ToInt64(dgAchiever.Rows[i].Cells[dgAchiever.Columns.Count - 2].Value) == 1)
                    {
                        mSchAch = new MSchemeAchievers();
                        mSchAch.PkSrNo = Convert.ToInt64(dgAchiever.Rows[i].Cells[dgAchiever.Columns.Count - 1].Value);
                        mSchAch.LedgerNo = Convert.ToInt64(dgAchiever.Rows[i].Cells[1].Value);
                        mSchAch.SchemeNo = ObjFunction.GetComboValue(cmbScheme);
                        mSchAch.SchemeDetailsNo = Convert.ToInt64(dgAchiever.Rows[i].Cells[3].Value);
                        mSchAch.SlabDiscAmt = ObjQry.ReturnLong("Select DiscAmount From MSchemeDetails Where PkSrNo=" + mSchAch.SchemeDetailsNo + "", CommonFunctions.ConStr);
                        for (int col = 4; col < dgAchiever.Columns.Count - 3; col++)
                        {
                            if (Convert.ToDouble(dgAchiever.Rows[i].Cells[col].Value) != 0)
                            {
                                mSchAch.SlabAmount = Convert.ToDouble(dgAchiever.Columns[col].HeaderText);
                                mSchAch.SlabBalanceAmount = Convert.ToDouble(dgAchiever.Columns[col].HeaderText);
                            }
                        }
                        mSchAch.IsActive = Convert.ToBoolean(dgAchiever.Rows[i].Cells[dgAchiever.Columns.Count - 3].Value);
                        mSchAch.CompanyNo = DBGetVal.FirmNo;
                        mSchAch.UserID = DBGetVal.UserID;
                        mSchAch.UserDate = DBGetVal.ServerTime;
                        dbScheme.AddMSchemeAchievers(mSchAch);

                        mSchAchDetails = new MSchemeAchieverDetails();
                        if (mSchAch.PkSrNo == 0)
                            mSchAchDetails.PkSrNo = 0;
                        else
                            mSchAchDetails.PkSrNo = ObjQry.ReturnLong("Select PkSrNo From MSchemeAchieverDetails Where SchemeAchieverNo=" + mSchAch.PkSrNo + "", CommonFunctions.ConStr);
                        mSchAchDetails.SchemeAchDate = DBGetVal.ServerTime.Date;
                        mSchAchDetails.SchemeAchSrNo = 1;
                        mSchAchDetails.LedgerNo = mSchAch.LedgerNo;
                        mSchAchDetails.TypeOfRef = 3;
                        mSchAchDetails.Amount = mSchAch.SlabDiscAmt;
                        mSchAchDetails.SignCode = 1;
                        mSchAchDetails.CompanyNo = DBGetVal.FirmNo;
                        mSchAchDetails.UserID = DBGetVal.UserID;
                        mSchAchDetails.UserDate = DBGetVal.ServerTime;
                        dbScheme.AddMSchemeAchieverDetails(mSchAchDetails);
                    }
                }

                if (dbScheme.ExecuteNonQueryStatements() > 0)
                {
                    OMMessageBox.Show("Achivers Details Save Successully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    dgAchiever.Rows.Clear();
                    dgAchiever.Columns.Clear();
                    pnlDataInfo.Visible = false;
                    cmbScheme.Focus();
                }
                else
                {
                    OMMessageBox.Show("Achivers Details not Saved...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                lblSchemePeriod.Text = "Scheme Period From :";
                lblRedemPeriod.Text = "Redemption Period From :";
            }
        }

        private bool Validations()
        {
            bool flag = false;

            for (int i = 0; i < dgAchiever.Rows.Count; i++)
            {
                if (Convert.ToInt32(dgAchiever.Rows[i].Cells[dgAchiever.Columns.Count - 2].Value) == 1)
                {
                    flag = true;
                    break;
                }
            }

            if (flag == false)
            {
                OMMessageBox.Show("Please change option atleast one Customer..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                dgAchiever.Focus();
            }
            return flag;
        }

        private void dgAchiever_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgAchiever.Columns.Count - 2)
            {
                if (dgAchiever.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "1")
                    dgAchiever.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.SkyBlue;
            }
        }

        private void dgAchiever_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgAchiever.Columns.Count - 3)
            {
                dgAchiever.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = 1;
            }
        }

        private void cmbScheme_Leave(object sender, EventArgs e)
        {

            lblSchemeNumber.Text = "Scheme Number :";
            lblSchemePeriod.Text = "Scheme Period From :";
            lblRedemPeriod.Text = "Redemption Period From :";
            pnlDataInfo.Visible = false;
        }

        private void cmbScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSchemeNumber.Text = "Scheme Number :";
            lblSchemePeriod.Text = "Scheme Period From :";
            lblRedemPeriod.Text = "Redemption Period From :";
            pnlDataInfo.Visible = false;
        }

        private void rd_CheckedChanged(object sender, EventArgs e)
        {
            pnlDataInfo.Visible = false;

            if (rdAcheivers.Checked == true)
            {
                ObjFunction.FillCombo(cmbScheme, "Select SchemeNo,SchemeName From MScheme  where SchemeTypeNo=1 And IsActive<>0 and (IsActive in(2) or SchemePeriodTo<'" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "') Order by SchemeName");
                lblSchemeNumber.Text = "Scheme Number :";
                lblSchemePeriod.Text = "Scheme Period From :";
                lblRedemPeriod.Text = "Redemption Period From :";
            }
            else if(rdTAcheivers.Checked == true)
            {
                ObjFunction.FillCombo(cmbScheme, "Select SchemeNo,SchemeName From MScheme  where SchemeTypeNo=1 And IsActive=1 and (SchemePeriodFrom <= '" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' and SchemePeriodTo >= '" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "') Order by SchemeName");
                lblSchemeNumber.Text = "Scheme Number :";
                lblSchemePeriod.Text = "Scheme Period From :";
                lblRedemPeriod.Text = "Redemption Period From :";
            }

            //btnShow.Focus();
            cmbScheme.Focus();
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
            if (e.KeyCode == Keys.F1)
            {
                rdAcheivers.Checked = true;
            }
            else if (e.KeyCode == Keys.F2)
            {
                rdTAcheivers.Checked = true;
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnShow_Click(sender, new EventArgs());
            }
            else if (e.KeyCode == Keys.F4)
            {
                if (btnApplyAchievers.Visible == true && pnlDataInfo.Visible == true)
                    btnApplyAchievers_Click(sender, new EventArgs());
            }
        }
        #endregion
    }
}
