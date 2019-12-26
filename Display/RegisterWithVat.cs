using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using System.Drawing.Printing;
using OMControls;

namespace Yadi.Display
{
    public partial class RegisterWithVat : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();

        public string RptTitle;
        private int VoucherType;

        public RegisterWithVat(int VoucherType)
        {
            InitializeComponent();
            this.VoucherType = VoucherType;
            if(VoucherType==VchType.Sales)
                this.Text = "Sales Entry Register";
            else if(VoucherType == VchType.Purchase)
                this.Text = "Purchase Entry Register";

        }

        private void RegisterWithVat_Load(object sender, EventArgs e)
        {
            try
            {
                if (VoucherType == VchType.Sales)
                {

                    ObjFunction.FillCombo(cmbPartyName, "SELECT MLedger.LedgerNo, MLedger.LedgerUserNo + ' - ' + MLedger.LedgerName AS LedgerName FROM MLedger INNER JOIN MGroup ON MLedger.GroupNo = MGroup.GroupNo  WHERE (MLedger.GroupNo IN (" + GroupType.CashInhand + ", " + GroupType.SundryDebtors + ")) OR (MGroup.ControlGroup IN (" + GroupType.CashInhand + ", " + GroupType.SundryDebtors + ")) order by cast(MLedger.LedgerUserNo as numeric(18,0))");

                    //lblHead.Text = "Sales Entry Register";
                }
                else if (VoucherType == VchType.Purchase)
                {
                    ObjFunction.FillCombo(cmbPartyName, "SELECT MLedger.LedgerNo, MLedger.LedgerUserNo + ' - ' + MLedger.LedgerName AS LedgerName FROM MLedger INNER JOIN MGroup ON MLedger.GroupNo = MGroup.GroupNo  WHERE (MLedger.GroupNo IN (" + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")) OR (MGroup.ControlGroup IN (" + GroupType.SundryDebtors + ", " + GroupType.SundryCreditors + ")) order by cast(MLedger.LedgerUserNo as numeric(18,0))");

                    //lblHead.Text = "Purchase Entry Register";
                }
                DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                DTToDate.MinDate = DTPFromDate.Value;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "";
                EP.SetError(cmbPartyName, "");
                ObjTrans.Execute("Drop Table RegisterVat", CommonFunctions.ConStr);
                ObjTrans.Execute("Drop Procedure AddRegisterVat", CommonFunctions.ConStr);
                if (chkPartyName.Checked == false)
                    str = "Exec GetRegisterVatDetails '" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + VoucherType + ",0";
                else
                {
                    if (cmbPartyName.SelectedIndex == 0)
                    {
                        EP.SetError(cmbPartyName, "Select Party Name");
                        EP.SetIconAlignment(cmbPartyName, ErrorIconAlignment.MiddleRight);
                    }
                    else
                        str = "Exec GetRegisterVatDetails '" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + VoucherType + "," + cmbPartyName.SelectedValue + "";
                }

                if (str != "")
                {
                    dataGridView1.DataSource = ObjFunction.GetDataView(str).Table;
                    dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dataGridView1.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

                    //if (dataGridView1.Rows.Count > 0)
                    //    btnPrint.Visible = true;
                    //else btnPrint.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkPartyName_CheckedChanged(object sender, EventArgs e)
        {
            cmbPartyName.Visible = chkPartyName.Checked;
            cmbPartyName.Focus();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.dataGridView1.Columns[e.ColumnIndex].Width = 50;
                this.dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (e.ColumnIndex == 1)
            {
                this.dataGridView1.Columns[e.ColumnIndex].HeaderText = "Date";
                this.dataGridView1.Columns[e.ColumnIndex].Width = 90;
                if (e.Value.ToString() == "1-1-1900")
                    e.Value = "";
                e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
            }
            if (e.ColumnIndex == 2)
            {
                this.dataGridView1.Columns[e.ColumnIndex].HeaderText = "Party Name";
                this.dataGridView1.Columns[e.ColumnIndex].Width = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Size.Width;
                this.dataGridView1.Columns[e.ColumnIndex].Width = 200;
            }
            if (e.ColumnIndex == 3)
            {
                this.dataGridView1.Columns[e.ColumnIndex].HeaderText = "VNo";
                this.dataGridView1.Columns[e.ColumnIndex].Width = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Size.Width;
                this.dataGridView1.Columns[e.ColumnIndex].Width = 50;
                this.dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (e.ColumnIndex == 4)
            {
                this.dataGridView1.Columns[e.ColumnIndex].HeaderText = "Total Amount";
                this.dataGridView1.Columns[e.ColumnIndex].Width = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Size.Width;
                this.dataGridView1.Columns[e.ColumnIndex].Width = 100;
                this.dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (e.ColumnIndex == 5)
            {
                this.dataGridView1.Columns[e.ColumnIndex].HeaderText = (VoucherType == VchType.Sales) ? "Sales Amount" : "Purchase Amount";
                this.dataGridView1.Columns[e.ColumnIndex].Width = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Size.Width;
                this.dataGridView1.Columns[e.ColumnIndex].Width = 70;
                this.dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (e.ColumnIndex == 6)
            {
                this.dataGridView1.Columns[e.ColumnIndex].Width = 50;
                this.dataGridView1.Columns[e.ColumnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void cmbPartyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbPartyName, e, true);
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                BtnShow.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument rpt1 = new ReportDocument();
            ReportClass rpt = new ReportClass();
            
            

        }

        private void DTPFromDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                DTToDate.Focus();
            }
        }

        private void DTToDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 13)
            {
                if (chkPartyName.Checked == true)
                    cmbPartyName.Focus();
                else
                    BtnShow.Focus();
                
            }
        }

        private void cmbPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

    }
}
