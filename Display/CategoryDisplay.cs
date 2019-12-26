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
    public partial class CategoryDisplay : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public static long CompNo;
        DataTable dtGr = new DataTable();

        public CategoryDisplay()
        {
            InitializeComponent();
        }

        private void CategoryDisplay_Load(object sender, EventArgs e)
        {
            try
            {
                CompNo = 1; btnPrint.Visible = false;
                ObjFunction.FillCombo(cmbCategory, "Select StockGroupNo,StockGroupName From MStockGroup");
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
                this.Cursor = Cursors.WaitCursor;
                EP.SetError(cmbCategory, "");
                if (cmbCategory.SelectedValue.ToString() != "0")
                {
                    dtGr = new DataTable(); dtGr.Columns.Add("ImgYesNo"); dtGr.Columns.Add("StockGroupNo"); dtGr.Columns.Add("CategoryName");
                    getGroups(Convert.ToInt64(cmbCategory.SelectedValue));

                    DataGridView1.DataSource = dtGr.DefaultView;
                    DataGridView1.Columns[0].Visible = false;
                    DataGridView1.Columns[1].Visible = false;
                    DataGridView1.Columns[2].Width = DataGridView1.Width;
                    for (int i = 0; i < DataGridView1.Rows.Count; i++)
                    {
                        DataGridView1.Rows[i].Height = 100;
                    }
                    if (DataGridView1.Rows.Count > 1)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                }
                else
                {
                    EP.SetError(cmbCategory, "Please Select Category");
                    EP.SetIconAlignment(cmbCategory, ErrorIconAlignment.MiddleRight);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            this.Cursor = Cursors.Default;
        }

        public void getGroups(long GrNo)
        {
            try
            {
                DataRow dr = null;
                //if(GrNo!= Convert.ton
                dr = dtGr.NewRow();
                dr[1] = GrNo;
                dr[2] = ObjQry.ReturnString("Select StockGroupName From MStockGroup Where StockGroupNo=" + GrNo + "", CommonFunctions.ConStr);


                DataTable dtTemp = ObjFunction.GetDataView("Select StockGroupNo,StockGroupName From MStockGroup Where ControlGroup=" + GrNo + "").Table;
                if (dtTemp.Rows.Count > 0) dr[0] = "1"; else dr[0] = "0";
                dtGr.Rows.Add(dr);
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    //dr = dtGr.NewRow();
                    //dr[0] = dtTemp.Rows[i].ItemArray[0].ToString();
                    //dr[1] = dtTemp.Rows[i].ItemArray[1].ToString();
                    //dtGr.Rows.Add(dr);
                    getGroups(Convert.ToInt32(dtTemp.Rows[i].ItemArray[0].ToString()));
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.CellStyle.Font = new Font("OM-DEV-0714", 12, FontStyle.Bold);
            }
            if (e.ColumnIndex == 0)
            {
                if (dtGr.Rows[e.RowIndex].ItemArray[e.ColumnIndex].ToString() == "0")
                {
                    

                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //string[] ReportSession = new string[2];

            //ReportSession[0] = Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy");
            //ReportSession[1] = DBGetVal.FirmNo.ToString();


            //Form NewF = new Display.ReportViewSource("ViewDailyVoucherDtls", ReportSession);
            //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void cmbCategory_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObjFunction.AutoComplete(ref cmbCategory, e, true);
            if ((int)e.KeyChar == 13)
            {
                BtnShow.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        
    }
}
