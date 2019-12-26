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

namespace Yadi.Vouchers
{
    public partial class SchemeDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public long DSchemeTypeNo, DSchemeNo, DSchemeDetailsNo;
        public long SchAchieverNo = 0;

        public SchemeDetails()
        {
            InitializeComponent();
        }

        public SchemeDetails(long SchNo, long SchemeDetailsNo)
        {
            InitializeComponent();
            DSchemeNo = SchNo;
            DSchemeDetailsNo = SchemeDetailsNo;
           
        }

        public SchemeDetails(long SchAchieverNo)
        {
            InitializeComponent();
            this.SchAchieverNo = SchAchieverNo;
        }

        private void SchemeDetails_Load(object sender, EventArgs e)
        {
            try
            {
                if (SchAchieverNo == 0)
                {
                    lblMsg.Font = new Font("Verdana", 12, FontStyle.Bold);
                    pnlPerc.Enabled = false;
                    dgBill.Enabled = false;
                    DataClrscr();
                    BindGrid();
                    dgSchemeCollect.Visible = false;
                }
                else
                {
                    label10.Visible = false;
                    label11.Visible = false;
                    txtPercentage.Visible = false;
                    txtRs.Visible = false;
                    dgBill.Visible = false;

                    DataTable dtSchAch = ObjFunction.GetDataView("Select SchemeAchSrNo As SrNo,SchemeAchDate As 'Date',Amount FROM MSchemeAchieverDetails Where SchemeAchieverNo=" + SchAchieverNo + "").Table;
                    if (dtSchAch.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtSchAch.Rows.Count; i++)
                        {
                            dgSchemeCollect.Rows.Add();
                            for (int col = 0; col < dtSchAch.Rows.Count; col++)
                            {
                                if (col != 1)
                                    dgSchemeCollect.Rows[i].Cells[col].Value = dtSchAch.Rows[i].ItemArray[col].ToString();
                                else
                                    dgSchemeCollect.Rows[i].Cells[col].Value = Convert.ToDateTime(dtSchAch.Rows[i].ItemArray[col].ToString()).ToString(Format.DDMMMYYYY);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void DataClrscr()
        {
            txtPercentage.Text = "0.0";
            lblMsg.Text = "";
            txtRs.Text = "0.00";
            while (dgBill.Rows.Count > 0)
                dgBill.Rows.RemoveAt(0);
        }

        public void BindGrid()
        {
            try
            {
                lblMsg.Text = ObjQry.ReturnString("Select SchemeName+'-[ '+SchemeUserNo+' ]' From mScheme Where SchemeNo=" + DSchemeNo + "", CommonFunctions.ConStr).ToUpper();
                string sqlDetails = "Select DiscPercentage,DiscAmount From MSchemeDetails Where PkSrNo=" + DSchemeDetailsNo + " And SchemeNo=" + DSchemeNo + "";
                DataTable dtDetails = ObjFunction.GetDataView(sqlDetails).Table;
                for (int i = 0; i < dtDetails.Rows.Count; i++)
                {
                    txtPercentage.Text = dtDetails.Rows[i].ItemArray[0].ToString();
                    txtRs.Text = dtDetails.Rows[i].ItemArray[1].ToString();
                }

                string sqlData = " SELECT 0 AS SrNo,(SELECT TOP (1) ItemName FROM  dbo.MStockItems_V(NULL, MSchemeToDetails.ItemNo, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1) AS 'ItemName', MSchemeToDetails.Quantity, MUOM.UOMName, MSchemeToDetails.Rate, MSchemeToDetails.DiscPercentage " +
                                " FROM MSchemeToDetails INNER JOIN MUOM ON MSchemeToDetails.UomNo = MUOM.UOMNo " +
                                " WHERE (MSchemeToDetails.SchemeDetailsNo = " + DSchemeDetailsNo + ") ";
                DataTable dtData = ObjFunction.GetDataView(sqlData).Table;
                for (int i = 0; i < dtData.Rows.Count; i++)
                {
                    dgBill.Rows.Add();
                    for (int j = 0; j < dgBill.ColumnCount; j++)
                    {
                        dgBill.Rows[i].Cells[j].Value = dtData.Rows[i].ItemArray[j].ToString();
                    }
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

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        
    }
}
