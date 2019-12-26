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

namespace Yadi.Utilities
{
    public partial class PrintCount : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public static int NoPrintCount=1;

        public PrintCount()
        {
            InitializeComponent();
        }

        public PrintCount(int IsPrintCount)
        {
            InitializeComponent();
            NoPrintCount = IsPrintCount;
            txtPrintCount.Text = NoPrintCount.ToString();
        }


        private void PrintCount_Load(object sender, EventArgs e)
        {
            txtPrintCount.Focus();
        }

        private void txtPrintCount_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, -1, 2, OMFunctions.MaskedType.NotNegative);
        }


        private void txtPrintCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPrintCount.Text.Trim() == "")
                    NoPrintCount = 1;
                else
                    NoPrintCount = Convert.ToInt32(txtPrintCount.Text);
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (txtPrintCount.Text.Trim() == "")
                    NoPrintCount = 1;
                else
                    NoPrintCount = Convert.ToInt32(txtPrintCount.Text);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            NoPrintCount = -1;
            this.Close();
        }
    }
}
