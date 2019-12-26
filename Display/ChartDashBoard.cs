using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OMControls;
using OM;
using System.Windows.Forms.DataVisualization.Charting;

namespace Yadi.Display
{
    public partial class ChartDashBoard : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public ChartDashBoard()
        {
            InitializeComponent();
        }

        public void BindSales()
        {
            string strQuery = "SELECT TOP (7) VoucherDate, SUM(BilledAmount) AS TotalAmt FROM TVoucherEntry " +
                " WHERE  (VoucherTypeCode = " + VchType.Sales + ") AND (VoucherDate >= '" + DBGetVal.ServerTime.AddDays(-7).ToString("dd-MMM-yyyy") + "') AND (VoucherDate <= '" + DBGetVal.ServerTime.ToString("dd-MMM-yyyy") + "') AND (IsCancel = 0) " +
                " GROUP BY VoucherDate ";
            DataTable dtSales = ObjFunction.GetDataView(strQuery).Table;

            chart1.Series.Clear();
            chart1.Series.Add("Sales Register");
            chart1.Titles.Add("Sales Register");
            chart1.BackGradientStyle = GradientStyle.HorizontalCenter;
            chart1.BackSecondaryColor = Color.FromArgb(0, 159, 232);
            //chart1.BackSecondaryColor = Color.White;

            ///chart1.Titles.Add("Sales Register");
            int j = 0;
            for (int i = 0; i < dtSales.Rows.Count; i++)
            {
                chart1.Series[0].Points.AddXY(j, dtSales.Rows[i].ItemArray[1].ToString());
                chart1.Series[0].Points[j].AxisLabel = Convert.ToDateTime(dtSales.Rows[i].ItemArray[0].ToString()).ToString(Format.DDMMMYYYY);
                chart1.Series[0].Points[j].Label = dtSales.Rows[i].ItemArray[1].ToString();
                chart1.Series[0].AxisLabel = "Voucher Date";
                j = j + 1;
            }
            chart1.Series[0]["DrawingStyle"] = "Cylinder";
            chart1.Series[0]["PointWidth"] = "0.5";// Show data points labels
            chart1.Series[0].IsValueShownAsLabel = true;// Set data points label style
        }
         
        private void ChartDashBoard_Load(object sender, EventArgs e)
        {
            BindSales();
            BindPurchase();
            BindTodaySales();
        }

        public void BindPurchase()
        {
            string strQuery = "SELECT TOP (7) VoucherDate, SUM(BilledAmount) AS TotalAmt FROM TVoucherEntry " +
                " WHERE  (VoucherTypeCode = " + VchType.Purchase + ") AND (VoucherDate >= '" + DBGetVal.ServerTime.AddDays(-7).ToString("dd-MMM-yyyy") + "') AND (VoucherDate <= '" + DBGetVal.ServerTime.ToString("dd-MMM-yyyy") + "') AND (IsCancel = 0) " +
                " GROUP BY VoucherDate ";
            DataTable dtPur = ObjFunction.GetDataView(strQuery).Table;

            chart2.Series.Clear();
            chart2.Series.Add("Purchase Register");
            chart2.Titles.Add("Purchase Register");
            chart2.BackGradientStyle = GradientStyle.HorizontalCenter;
            chart2.BackSecondaryColor = Color.FromArgb(0, 159, 232);
            //chart2.BackSecondaryColor = Color.White;
             
            ///chart1.Titles.Add("Sales Register");
            int j = 0;
            for (int i = 0; i < dtPur.Rows.Count; i++)
            {
                chart2.Series[0].Points.AddXY(j, dtPur.Rows[i].ItemArray[1].ToString());
                chart2.Series[0].Points[j].AxisLabel = Convert.ToDateTime(dtPur.Rows[i].ItemArray[0].ToString()).ToString(Format.DDMMMYYYY);
                chart2.Series[0].Points[j].Label = dtPur.Rows[i].ItemArray[1].ToString();
                chart2.Series[0].AxisLabel = "Voucher Date";
                j = j + 1;
            }
            chart2.Series[0]["DrawingStyle"] = "Cylinder";
            chart2.Series[0]["PointWidth"] = "0.5";// Show data points labels
            chart2.Series[0].IsValueShownAsLabel = true;// Set data points label style
        }

        public void BindTodaySales()
        {
            string strQuery = "Exec GetPayTypeDetails '" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "','" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "'," + VchType.Sales + ",0,1";
            DataTable dtTodaysSale = ObjFunction.GetDataView(strQuery).Table;

            chart3.Series.Clear();
            chart3.Series.Add("Sales");
            chart3.Titles.Add("Todays Sales");
            chart3.BackGradientStyle = GradientStyle.HorizontalCenter;
            chart3.BackSecondaryColor = Color.FromArgb(0, 159, 232);
            //chart3.BackSecondaryColor = Color.White;

            ///chart1.Titles.Add("Sales Register");
            int j = 0;
            for (int i = 0; i < dtTodaysSale.Rows.Count; i++)
            {
                chart3.Series[0].Points.AddXY(j, dtTodaysSale.Rows[i].ItemArray[4].ToString());
                chart3.Series[0].Points[j].AxisLabel = dtTodaysSale.Rows[i].ItemArray[2].ToString();
                chart3.Series[0].Points[j].Label = dtTodaysSale.Rows[i].ItemArray[4].ToString();
                chart3.Series[0].AxisLabel = "Pay Type Name";
                j = j + 1;
            }
            chart3.Series[0]["DrawingStyle"] = "Cylinder";
            chart3.Series[0]["PointWidth"] = "0.5";// Show data points labels
            chart3.Series[0].IsValueShownAsLabel = true;// Set data points label style
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
