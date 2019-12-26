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
    public partial class AnnexureTaxDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo, LedgNo, MNo, Type1, VchCode;
        public int voucherno;

        public static string LedgName, RptTitle;
        public static int Type;
        public AnnexureTaxDetails()
        {
            InitializeComponent();
        }
        public AnnexureTaxDetails(long VchTypeCode)
        {
            InitializeComponent();
            if (VchTypeCode == VchType.Sales)
            {
                VchCode = VchType.Sales;
                this.Text = "ANNEXURE –J1";
                chkShowCust.Text = "Show Customer Name (Yes/No)";
            }
            else if (VchTypeCode == VchType.Purchase)
            {
                VchCode = VchType.Purchase;
                this.Text = "ANNEXURE –J2";
                chkShowCust.Text = "Show Supplier Name (Yes/No)";
            }


        }

        private void AnnextureTaxDetails_Load(object sender, EventArgs e)
        {
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
                chkShowCust.Focus();
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string ReportName = "",StrName="";
                string CompTinNo = ObjQry.ReturnString("Select VatNo From mCompany Where CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);

                int dtCount = 0;
                if (VchCode == VchType.Sales)
                {
                    ReportName = "CUSTOMER-WISE VAT SALES";
                    StrName = "ANNEXURE –J1";
                }
                else
                {
                    ReportName = "CUSTOMER-WISE VAT PURCHASES";
                    StrName = "ANNEXURE –J2";
                }

                DataTable dtData = new DataTable();
                if (chkShowCust.Checked == true)
                    dtData = ObjFunction.GetDataView("Exec GetAnnextureTaxDetails " + VchCode + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + GroupType.VAT + ",1").Table;
                else
                    dtData = ObjFunction.GetDataView("Exec GetAnnextureTaxDetails " + VchCode + ",'" + DTPFromDate.Text + "','" + DTToDate.Text + "'," + GroupType.VAT + ",2").Table;
                dtCount = dtData.Columns.Count;

                int col = 1;
                CreateExcel excel = new CreateExcel();
                //Company Name Header
                excel.createHeaders(col, 1, StrName, excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                excel.createHeaders(col, 1, "(Section " + ((VchCode == VchType.Sales) ? "1" : "2") + ") -Suppliment", excel.ColName(col, 1), excel.ColName(col, dtCount), dtCount, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;
                int C = 1;
                excel.createHeaders(col, C, "TIN", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;

                if (chkShowCust.Checked == false)
                {
                    excel.createHeaders(col, C, CompTinNo, excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                    excel.createHeaders(col, C, "Period", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                }
                else
                {
                    excel.createHeaders(col, C, CompTinNo, excel.ColName(col, C), excel.ColName(col, C), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++; C++;
                    excel.createHeaders(col, C, "Period", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                }
                excel.createHeaders(col, C,Convert.ToDateTime(DTPFromDate.Text).ToString("dd-MM-yy"), excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                excel.createHeaders(col, C, Convert.ToDateTime(DTToDate.Text).ToString("dd-MM-yy"), excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                col++;
                excel.createHeaders(col, 1, "Whether Form-704 Filed?", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                C = 3;
                if (chkShowCust.Checked == false)
                {
                    excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                }
                else
                {
                    excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                    excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                }
                excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 1, Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 1, Color.Gainsboro, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                col++;
                excel.createHeaders(col, 1, "If Filed Form-704,Transaction Id", excel.ColName(col, 1), excel.ColName(col, 2), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left);
                C = 3;
                if (chkShowCust.Checked == false)
                {
                    excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                }
                else
                {
                    excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 2, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                    excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                }
                excel.createHeaders(col, C, "Sheet No", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                excel.createHeaders(col, C, "", excel.ColName(col, C), excel.ColName(col, C), 1, Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Left); C++;
                col++;
                //Report Name And Dates
                excel.createHeaders(col, 1, ReportName, excel.ColName(col, 1), excel.ColName(col, dtCount), (dtCount), Color.White, true, 12, Color.Black, 12, CreateExcel.ExAlign.Center);
                col++;

                for (int i = 0; i < dtCount; i++)
                {
                    if (i == 0)
                        excel.createHeaders(col, i + 1, dtData.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 10, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else if (i == 2)
                    {
                        if (chkShowCust.Checked == true)
                            excel.createHeaders(col, i + 1, dtData.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 30, Color.Black, 12, CreateExcel.ExAlign.Left);
                        else
                            excel.createHeaders(col, i + 1, dtData.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                    }
                    else if (i == 2)
                        excel.createHeaders(col, i + 1, dtData.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 30, Color.Black, 12, CreateExcel.ExAlign.Left);
                    else
                        excel.createHeaders(col, i + 1, dtData.Columns[i].ColumnName, excel.ColName(col, i + 1), excel.ColName(col, i + 1), 1, Color.Gainsboro, true, 20, Color.Black, 12, CreateExcel.ExAlign.Left);

                    for (int j = 0; j < dtData.Rows.Count; j++)
                    {
                        if (i == 0)
                            excel.addData(j + col + 1, i + 1, (j + 1).ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Center, false);
                        else if (i == 1)
                        {
                            excel.addData(j + col + 1, i + 1, dtData.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                        }
                        else if (i == 2)
                        {
                            if (chkShowCust.Checked == true)
                                excel.addData(j + col + 1, i + 1, dtData.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Left, false);
                            else
                                excel.addData(j + col + 1, i + 1, dtData.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), "", 0, CreateExcel.ExAlign.Right, false);
                        }
                        else
                            excel.addData(j + col + 1, i + 1, dtData.Rows[j].ItemArray[i].ToString(), excel.ColName(j + col + 1, i + 1), excel.ColName(j + col + 1, i + 1), Format.DoubleFloating, 0, CreateExcel.ExAlign.Right, false);
                    }

                }
                excel.CompleteDoc("");
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
