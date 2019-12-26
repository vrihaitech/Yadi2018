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
    public partial class TaxCategoryCFormDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VchCode, MainMfgCompNo;
        public int voucherno;

        public static string LedgName, RptTitle, MainMfgCompName;
        public static int Type;
        public TaxCategoryCFormDetails()
        {
            InitializeComponent();
        }
        public TaxCategoryCFormDetails(long VchTypeCode)
        {
            InitializeComponent();
            if (VchTypeCode == VchType.Sales)
            {
                VchCode = VchType.Sales;
                this.Text = "Sales Category CST Details";
            }
            else if (VchTypeCode == VchType.Purchase)
            {
                VchCode = VchType.Purchase;
                this.Text = "Purchase Category CST Details";
            }


        }

        private void TaxDetails_Load(object sender, EventArgs e)
        {
            Form NewFrm = new Vouchers.FirmSelection();
            ObjFunction.OpenForm(NewFrm);
            MainMfgCompNo = ((Vouchers.FirmSelection)NewFrm).MfgCompNo;

            lblFirmName.Text = "Firm Name :" + ((Vouchers.FirmSelection)NewFrm).MfgCompName;
            lblFirmName.Font = new Font("Verdana", 9, FontStyle.Bold);
            MainMfgCompName = ((Vouchers.FirmSelection)NewFrm).MfgCompName;

            CompNo = DBGetVal.FirmNo;
            DTPFromDate.Text = "01-" + DBGetVal.ServerTime.ToString("MMM-yyyy");
            DTToDate.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
            DTToDate.MinDate = DTPFromDate.Value;
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.IsExcelReport)) == true)
                BtnExport.Visible = true;
            else
                BtnExport.Visible = false;
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
                rdSummary.Focus();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
               // DataTable dtHeader = new DataTable();
                DataSet ds;
                string str = "", ReportName = "";
                int dtCount = 0;

                if (VchCode == VchType.Sales) ReportName = "CST Report";
                else ReportName = "CST Report";

                str = "Exec GetCategoryTaxDetails '" + Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MMM-yyyy") + "','" + Convert.ToDateTime(DTToDate.Text).ToString("dd-MMM-yyyy") + "'," + VchCode + "," + GroupType.CForm + ","+MainMfgCompNo;
                ds = ObjDset.FillDset("New", str, CommonFunctions.ConStr);
                if (ds.Tables.Count <= 0)
                {
                    MessageBox.Show("No Data Found", CommonFunctions.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    dt = ds.Tables[0];
                  
                    dtCount = dt.Columns.Count; //- 2; PartyName = "Party";
                }


                if (str != "")
                {

                    int col = 1;
                    CreateExcel excel = new CreateExcel();
                    //Company Name Header
                    excel.createHeaders(col, 1, DBGetVal.FirmName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, true, 20, Color.Black, 20, CreateExcel.ExAlign.Center);
                    col++;
                    //Company Address Header
                    excel.createHeaders(col, 1, DBGetVal.CompanyAddress, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.PeachPuff, false, 10, Color.Black, 10, CreateExcel.ExAlign.Center);
                    col++;
                    //Report Name And Dates
                    excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                    col++;
                    excel.createHeaders(col, 1, "FromDate :  " + Convert.ToDateTime(DTPFromDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 15, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;
                    excel.createHeaders(col, 1, "ToDate :  " + Convert.ToDateTime(DTToDate.Value).ToString("dd-MMM-yyyy") + "", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 15, Color.Black, 10, CreateExcel.ExAlign.Left);
                    col++;

                    //excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 7), 7, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    
                   // excel.createHeaders(col, dtCount - 2 + 1, "", excel.ColName(col, dtCount - 2 + 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    //col++;

                    //excel.createHeaders(col, 1, "", excel.ColName(col, 1), excel.ColName(col, 7), 7, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    
                    //excel.createHeaders(col, dtCount - 1, "Total", excel.ColName(col, dtCount - 1), excel.ColName(col, dtCount), 2, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Center);
                    //col++;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {

                        if (i == 0)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15, Color.Black, 12, CreateExcel.ExAlign.Right);
                        else if(i == 2)
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 25, Color.Black, 12, CreateExcel.ExAlign.Center);
                        else
                            excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 15 , Color.Black, 12, CreateExcel.ExAlign.Right);

                        ////else if (i > 6 && i < dtCount)
                        ////{
                        ////    if ((i % 2) == 1)
                        ////        excel.createHeaders(col, i + 1, "Amt", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        ////    else
                        ////        excel.createHeaders(col, i + 1, "Tax", excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Right);
                        ////}
                        //else if (i == 2)
                        //    excel.createHeaders(col, i + 1, dt.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 35, Color.Black, 12, CreateExcel.ExAlign.Left);
                       

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {

                            //if (i == 1)
                            //    excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                            //else 
                            if (i == 0)
                            {
                                
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DDMMMYYYY, 0, CreateExcel.ExAlign.Right, false);
                            }

                            else if (i > 4 && i<9)
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                            else
                                excel.addData(j + col + 1, i + 1, dt.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "",0, CreateExcel.ExAlign.Right, false);


                        }
                    }
                    col++;
                    excel.CompleteDoc("");

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }


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
