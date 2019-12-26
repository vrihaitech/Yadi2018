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
    public partial class FirmSelection : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public long MfgCompNo;
        public string MfgCompName;
        public string MfgCompAdd;
        public DataTable DTMFGComp;
        public DialogResult DS = DialogResult.OK;
        public string StrQuery = "";
        public bool IsPK;   

        public FirmSelection()
        {
            InitializeComponent();
        }

        public FirmSelection(string MfgString)
        {
            InitializeComponent();
            StrQuery = MfgString;
        }


        private void Firm_Load(object sender, EventArgs e)
        {
            lstCompany.Font = new Font("Verdana", 12, FontStyle.Bold);
            if (StrQuery == "")
                ObjFunction.FillList(lstCompany, "SELECT MfgCompNo, MfgCompName FROM MManufacturerCompany Where IsActive='True'");
            else
                ObjFunction.FillList(lstCompany, StrQuery);

            lstCompany.Focus();
            lstCompany.Font = ObjFunction.GetFont(FontStyle.Regular, 14);
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DS = DialogResult.OK;
            MfgCompNo = Convert.ToInt64(lstCompany.SelectedValue);
            MfgCompName = lstCompany.Text;
            DTMFGComp = ObjFunction.GetDataView("Select MfgCompAddress,EmailID,PhoneNo,IsMfgType From MManufacturerCompany Where MfgCompNo=" + MfgCompNo + "").Table;
            MfgCompAdd = DTMFGComp.Rows[0].ItemArray[0].ToString();
            this.Close();
        }

        private void lstCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DS = DialogResult.OK;
                MfgCompNo = Convert.ToInt64(lstCompany.SelectedValue);
                MfgCompName = lstCompany.Text;

                DTMFGComp = ObjFunction.GetDataView("Select MfgCompAddress,EmailID,PhoneNo,IsMfgType From MManufacturerCompany Where MfgCompNo=" + MfgCompNo + "").Table;
                MfgCompAdd = DTMFGComp.Rows[0].ItemArray[0].ToString();
                this.Close();
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            DS = DialogResult.Cancel;
            this.Close();
        }
    }
}
