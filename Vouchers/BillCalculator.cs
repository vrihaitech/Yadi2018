using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OM;

namespace Yadi.Vouchers
{
    public partial class BillCalculator : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        public DialogResult DS = DialogResult.OK;
        public BillCalculator()
        {
            InitializeComponent();
        }
        public BillCalculator(double Amount)
        {           
            InitializeComponent();
            txtBillAmt.Text = Amount.ToString();
           
        }

        private void BillCalculator_Load(object sender, EventArgs e)
        {
            txtRecAmt.Focus();
        }

        private void txtRecAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (txtRecAmt.Text.Trim() == "")
                {
                    txtRecAmt.Text = txtBillAmt.Text;
                }
                if (Validations() == true)
                {
                    txtBalAmt.Text = (Convert.ToDouble(txtRecAmt.Text) - Convert.ToDouble(txtBillAmt.Text)).ToString();
                    btnOk.Focus();
                }
            }
        }
        public bool Validations()
        {
            bool flag = true;
            try
            {
                lblError.Text = "";
                if (ObjFunction.CheckValidAmount(txtRecAmt.Text.Trim()) == false)
                {
                    lblError.Text = "Enter Valid Amount";
                    txtRecAmt.Focus();
                    flag = false;
                }
                else if (Convert.ToDouble(txtRecAmt.Text.Trim()) < Convert.ToDouble(txtBillAmt.Text.Trim()))
                {
                    lblError.Text = "Enter Correct Amount";
                    txtRecAmt.Focus();
                    flag = false;
                }
                else
                    flag = true;
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void Common_Click(object sender, EventArgs e)
        {
            try
            {
                double Amt = 0;
                if (txtRecAmt.Text == "")
                    Amt = 0;
                else
                    Amt = Convert.ToDouble(txtRecAmt.Text);
                txtRecAmt.Text = Convert.ToString(Convert.ToInt64(((Button)(sender)).Text) + Amt);
                txtBalAmt.Text = (Convert.ToDouble(txtRecAmt.Text) - Convert.ToDouble(txtBillAmt.Text)).ToString();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnFullAmt_Click(object sender, EventArgs e)
        {
            try
            {
                txtRecAmt.Text = txtBillAmt.Text;
                txtBalAmt.Text = "0";
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DS = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Validations() == true)
            {
                DS = DialogResult.OK;
                this.Close();
            }
        }
    }
}
