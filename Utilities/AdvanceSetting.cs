using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using OM;
using OMControls;

namespace Yadi.Utilities
{
    public partial class AdvanceSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Security secure = new Security();
        public AdvanceSetting()
        {
            InitializeComponent();
        }

       
        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void AdvanceSetting_Click(object sender, EventArgs e)
        {
            pnlmain.Visible = true;
        }

        private void btnSstock_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                ObjTrans.ExecuteQuery("Update MRateSetting set stock=0  ", CommonFunctions.ConStr);
                OMMessageBox.Show("stock update Zero Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }

        }

        private void btnEstock_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                ObjTrans.ExecuteQuery("Update MRateSetting set stock2=0 ", CommonFunctions.ConStr);
                OMMessageBox.Show("stock update Zero Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }

        }

        private void btnLdelete_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                ObjTrans.ExecuteQuery("delete from  MLedger where groupno in(22,26) delete from MLedgerDetails where LedgerNo not in(select LedgerNo from MLedger) ", CommonFunctions.ConStr);
                OMMessageBox.Show("Ledger Delete Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }

        }

        private void btnIdelete_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                ObjTrans.ExecuteQuery("delete from  MItemMaster where itemno > 1 delete from MItemGroup delete from MRateSetting  delete from MItemTaxInfo  ", CommonFunctions.ConStr);
                OMMessageBox.Show("Itemmaster Delete Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }

        }

        private void btnAdelete_Click(object sender, EventArgs e)
        {
            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
            {
                ObjTrans.ExecuteQuery(" delete MLedger where groupno in(22,26) delete MLedgerDetails where LedgerNo not in(select LedgerNo from MLedger)  delete from  MItemMaster where itemno > 1 delete from MItemGroup delete from MRateSetting  delete from MItemTaxInfo ", CommonFunctions.ConStr);
                OMMessageBox.Show("all master Delete Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            }

        }

        private void AdvanceSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                pnlmain.Visible = true;
            }
        }

        private void btnSentrydelete_Click(object sender, EventArgs e)
        {

        }

        private void btnPentryDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
