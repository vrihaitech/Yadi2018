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
    public partial class ViewOutStanding : Form
    {
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        CommonFunctions ObjFunction = new CommonFunctions();
        public double BillAmnt, RecAmnt, NetAmnt;

        public ViewOutStanding()
        {
            InitializeComponent();
        }

        public ViewOutStanding(long LedgerNo)
        {
            InitializeComponent();
            FillGrid(LedgerNo);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VOutStandingToday_Load(object sender, EventArgs e)
        {
            dgData.Enabled = false;
        }

        private void FillGrid(long LedgerNo)
        {
            try
            {
                dgData.Rows.Clear();
                //DataRow dr;
                double total = 0;
                DataTable dt = ObjFunction.GetDataView("Exec GetCollectionDetails " + LedgerNo + "," + VchType.Sales + ",'" + DBGetVal.FirmNo + "'", CommonFunctions.ConStr).Table;

                dt.Columns.RemoveAt(1);
                for (int i = 4; i < dt.Columns.Count; i++)
                {
                    dt.Columns.RemoveAt(4);
                    i--;
                }
                //dr = dt.NewRow();
                //dr[0] = 0;
                //dr[1] = dt.Compute("Sum(Debit)", "");
                //dr[2] = dt.Compute("Sum(TotRec)", "");
                //dr[3] = dt.Compute("Sum(NetBal)", "");
                //dt.Rows.Add(dr);
                if (dt.Rows.Count > 0)
                    total = Convert.ToInt64(dt.Compute("Sum(NetBal)", ""));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgData.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                        dgData.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
                lblTot.Text = total.ToString("0.00");
                lblTot.Font = new Font("Verdana", 20, FontStyle.Bold);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}

