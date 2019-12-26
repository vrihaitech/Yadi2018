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

namespace Yadi.Vouchers
{
    public partial class OrderTypeSelection : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public long OrderTypeNo;
        DataTable dtOrderType;

        public OrderTypeSelection()
        {
            InitializeComponent();
        }

        public OrderTypeSelection(long OrderTypeNo,DataTable dtOrderType)
        {
            InitializeComponent();
            this.OrderTypeNo = OrderTypeNo;
            this.dtOrderType = dtOrderType;
        }

        private void POSelection_Load(object sender, EventArgs e)
        {
            btnOrderFirst.Text = dtOrderType.Select("OrderTypeNo='1'")[0].ItemArray[1].ToString() + "(F1)";
            btnOrderFirst.BackColor = Color.FromName(dtOrderType.Select("OrderTypeNo=1")[0].ItemArray[2].ToString());
            btnOrderSecond.Text = dtOrderType.Select("OrderTypeNo=2")[0].ItemArray[1].ToString() + "(F2)";
            btnOrderSecond.BackColor = Color.FromName(dtOrderType.Select("OrderTypeNo=2")[0].ItemArray[2].ToString());
            KeyDownFormat(this.Controls);
        }

        #region KeyDown Events
        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyDownFormat(ctrl.Controls);
            }
        }

        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnOrderFirst_Click(sender, new EventArgs());
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnOrderSecond_Click(sender, new EventArgs());
            }
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        

        private void btnOrderFirst_Click(object sender, EventArgs e)
        {
            OrderTypeNo = 1;
            this.Close();
        }

        private void btnOrderSecond_Click(object sender, EventArgs e)
        {
            OrderTypeNo = 2;
            this.Close();
        }
    }
}
