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
    public partial class DailyStockSummary : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();

        public long CompNo, ItNo, MNo, Type1, No, ItNo1, BItemNo;
        public string ItName, RptTitle, ItNm;
        public DailyStockSummary()
        {
            InitializeComponent();
        }

        private void DailyStockSummary_Load(object sender, EventArgs e)
        {
            try
            {
                rbItemWise.Checked = true;
                rbItemWise_CheckedChanged(sender, e);
                CompNo = 1;
                ObjFunction.FillCombo(cmbItemName, "SELECT ItemNo, ItemName AS ItemName FROM MStockItems_V(null,null,null,null,null,null,null) order by ItemName");
                DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
                DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                DTToDate.MinDate = DTPFromDate.Value;
                panel3.Visible = true;
                cmbItemName.Visible = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void GridNull()
        {
            dataGridView1.DataSource = null;
                       btnPrint.Visible = false;
            EP.SetError(cmbItemName, "");
        }
        private void rbItemWise_CheckedChanged(object sender, EventArgs e)
        {
             GridNull();
             
                 panel1.Visible = true;
                 panel2.Visible = true;
                 panel3.Visible = true;
                 txtBarCode.Visible = false;
                 checkBox1.Visible = true;
                 cmbItemName.Focus();
                
            
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                btnPrint.Visible = true;
                if (txtBarCode.Text != "")
                {
                    txtBarCode.Text = txtBarCode.Text.Replace("\r", "").Replace("\n", "");
                    // BItemNo = ObjQry.ReturnLong("Select MS.ItemNo from MstockItems MS,MStockItemsBarCodeDetails MB Where MS.ItemNo =MB.ItemNo AND MB.BarCode='" + txtBarCode.Text + "' ", CommonFunctions.ConStr);
                    BItemNo = ObjQry.ReturnLong("Select MS.ItemNo from MStockItems_V(null,null,null,null,null,null,null) MS,MStockBarCode MB Where MS.ItemNo =MB.ItemNo AND MB.BarCode='" + txtBarCode.Text + "' ", CommonFunctions.ConStr);
                    dsVd = ObjDset.FillDset("New", "Exec GetDailyStockSummary " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', " + BItemNo + "", CommonFunctions.ConStr);
                    DataTable dt = dsVd.Tables[0];
                    DataRow dr = dt.NewRow();
                    dsVd.Tables[0].Rows.Add(dr);
                    dataGridView1.DataSource = dsVd.Tables[0].DefaultView;

                }
                else if (rbItemWise.Checked == true || rbAllItem.Checked == true)
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (rbItemWise.Checked == true)
                    {
                        EP.SetError(cmbItemName, "");
                        if (cmbItemName.SelectedIndex != 0)
                        {
                            dsVd = ObjDset.FillDset("New", "Exec GetDailyStockSummary " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', " + cmbItemName.SelectedValue + "", CommonFunctions.ConStr);
                            DataTable dt = dsVd.Tables[0];
                            DataRow dr = dt.NewRow();
                            dsVd.Tables[0].Rows.Add(dr);
                            dataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                            // GetCount();
                        }
                        else
                        {
                            EP.SetError(cmbItemName, "Please Select Item Name");
                            EP.SetIconAlignment(cmbItemName, ErrorIconAlignment.MiddleRight);
                        }
                    }
                    else if (rbAllItem.Checked == true)
                    {
                        panel3.Visible = false;
                        panel2.Visible = false;
                        dataGridView1.Visible = true;
                        dsVd = ObjDset.FillDset("New", "Exec GetDailyStockSummary " + CompNo + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "', 0", CommonFunctions.ConStr);
                        DataTable dt = dsVd.Tables[0];
                        DataRow dr = dt.NewRow();
                        dsVd.Tables[0].Rows.Add(dr);

                        dataGridView1.DataSource = dsVd.Tables[0].DefaultView;
                        //GetCount();
                    }

                    else
                    {
                        OMMessageBox.Show("Select Type to Display Records");
                    }
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "ItemName")
            {
                e.CellStyle.Font = ObjFunction.GetFont();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ReportSession;
                Form NewF = null;
                ReportSession = new string[4];
                ReportSession[0] = Convert.ToString(DBGetVal.FirmNo);
                ReportSession[1] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[2] = Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy");
                ReportSession[3] = Convert.ToString(cmbItemName.SelectedValue);
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    NewF = new Display.ReportViewSource(new Reports.DailyStockSummary(), ReportSession);
                else
                    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("DailyStockSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

       

        private void cmbItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbItemName, e, true);
        }

      

        private void rbAllItem_CheckedChanged(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel2.Visible = false;
            checkBox1.Checked = false;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            GridNull();
            cmbItemName.SelectedIndex = 0;
            if (checkBox1.Checked == true)
            {
                txtBarCode.Visible = true;
                cmbItemName.Enabled = false;
            }
            else
            {
                txtBarCode.Visible = false;
                cmbItemName.Enabled = true;
            }
        }

        private void cmbItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridNull();
        }

        private void DTPFromDate_ValueChanged(object sender, EventArgs e)
        {
            DTToDate.MinDate = DTPFromDate.Value;
        }

       
    }
}
