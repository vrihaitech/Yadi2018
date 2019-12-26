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

namespace Yadi.Master
{
    public partial class ItemHSNCodeDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        MItemTaxSetting mItemTaxSetting = new MItemTaxSetting();
        public DialogResult DS = DialogResult.OK;
        DBMItemMaster dbMItemMaster = new DBMItemMaster();
        MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();
        long ID = 0;
        DataTable dt = new DataTable();

        public ItemHSNCodeDetails()
        {
            InitializeComponent();
        }

        //public ItemHSNCodeDetails(long typeNo, long TaxTypeNo)
        //{
        //    InitializeComponent();
        //    GrType = typeNo;
        //    this.TaxTypeNo = TaxTypeNo;
        //}

        private void ItemHSNCodeDetails_Load(object sender, EventArgs e)
        {
            FormatPicture();
            FillList();

            foreach (DataGridViewColumn col in GvHSNCode.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            GvHSNCode.ColumnHeadersHeight = 25;
            foreach (DataGridViewColumn col in GvHSNDetails.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            GvHSNDetails.ColumnHeadersHeight = 25;
            txtHSNCode.Focus();
        }

        private void FillList()
        {
            ObjFunction.FillList(lstHSNCode, "SELECT  distinct MItemMaster.HSNCode as HSN,MItemMaster.HSNCode FROM MItemMaster Where IsActive ='True' Order By HSNCode");

            ObjFunction.FillList(lstIGSTSales, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)+ ' %') as Percentage FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                        "   MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                        " WHERE     (MLedger.GroupNo = " + GroupType.SalesAccount + ") AND (MLedger_1.GroupNo = 53) And MItemTaxSetting.IsActive='True' Order by  MItemTaxSetting.TaxSettingName ");

        }

        private void FormatPicture()
        {
            lstHSNCode.Font = ObjFunction.GetFont(FontStyle.Regular, 12);
            pnlHSNCode.Width = txtHSNCode.Width;
            pnlHSNCode.Top = txtHSNCode.Bottom;
            pnlHSNCode.Left = txtHSNCode.Left;
            pnlHSNCode.Height = 200;

            lstIGSTSales.Font = ObjFunction.GetFont(FontStyle.Regular, 12);
            pnlIGSTSales.Width = txtIGSTSales.Width;
            pnlIGSTSales.Top = txtIGSTSales.Bottom;
            pnlIGSTSales.Left = txtIGSTSales.Left;
            pnlIGSTSales.Height = 200;
        }

        private void txtHSNCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            //    txtHSNCode.Text = txtHSNCode.Text.Trim().ToUpper();
            //    if (txtHSNCode.Text.Trim() == "")
            //    {
            //        txtHSNCode.Focus();
            //    }
            //    else
            //    {
            //        if (PnlHSNCode.Visible == true)
            //        {                       
            //            BindHSNCode();
            //            GvHSNCode.Focus();
            //        }
            //    }
            //}
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtHSNCode.Text.Trim() == "")
                {
                    pnlHSNCode.Visible = true;
                    FillList();
                    lstHSNCode.Focus();
                }
                else
                {
                    pnlHSNCode.Visible = false;
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlHSNCode.Visible = true;
                lstHSNCode.Focus();
            }
        }

        private void lstHSNCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtHSNCode.Text = lstHSNCode.Text;
                    pnlHSNCode.Visible = false;
                    BindHSNCode();
                    GvHSNCode.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtHSNCode.Focus();
                    txtHSNCode.Text = lstHSNCode.Text;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtHSNCode_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (txtPercentage.Text.Trim() != "")
                //{
                //    if (ObjQry.ReturnInteger(" Select Count(*) FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                //                             " MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo WHERE     (MLedger.GroupNo = " + GrType + ") AND (MLedger_1.GroupNo =" + TaxTypeNo + ") AND MItemTaxSetting.Percentage= " + Convert.ToDouble(txtPercentage.Text.Trim()) + "", CommonFunctions.ConStr) != 0)
                //    {
                //        EP.SetError(txtPercentage, "Duplicate Percentage");
                //        EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                //        txtPercentage.Focus();
                //    }
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtIGSTSales_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtIGSTSales.Text == "")
                {
                    pnlIGSTSales.Visible = true;
                    lstIGSTSales.Focus();
                }
                else
                {
                    pnlIGSTSales.Visible = false;
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Delete) || e.KeyChar == Convert.ToChar(Keys.Back))
            {
            }
            else
            {
                pnlIGSTSales.Visible = true;
                lstIGSTSales.Focus();
            }
        }

        private void lstIGSTSales_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtIGSTSales.Text = lstIGSTSales.Text;
                    pnlIGSTSales.Visible = false;
                    string PER = txtIGSTSales.Text.Replace(" %", "");
                    btnApplyTax.Focus();

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtIGSTSales.Focus();
                    txtIGSTSales.Text = lstIGSTSales.Text;
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtHSNCode, "");
            if (txtHSNCode.Text.Trim() == "")
            {
                EP.SetError(txtHSNCode, "Enter HSNCode");
                EP.SetIconAlignment(txtHSNCode, ErrorIconAlignment.MiddleRight);
                txtHSNCode.Focus();
            }
            else
                flag = true;

            return flag;
        }

        private void btnApplyTax_Click(object sender, EventArgs e)
        {
            try
            {
                double igst = 0;
                string authors = lstIGSTSales.Text;
                string[] authorsList = authors.Split('%');
                String igs = (authorsList[0].ToString());
                igst = Convert.ToDouble(igs);
                double GSTTax = Convert.ToDouble(igst / 2);

                //double GSTTax = 0.00;
                //long Tax = Convert.ToInt64(lstIGSTSales.Text);
                //GSTTax = Convert.ToDouble(Tax) / 2;

                DataTable dtMainItemTax = ObjFunction.GetDataView("SELECT     MItemTaxSetting.PkSrNo, MItemTaxSetting.TaxSettingName, " + "MItemTaxSetting.TaxLedgerNo, MItemTaxSetting.SalesLedgerNo,MItemTaxSetting.Percentage,MLedger_1.GROUPNO as GroupNo, MLedger.GROUPNO AS TaxTypeNo, " + "MItemTaxSetting.CompanyNo, MItemTaxSetting.IsActive, MItemTaxSetting.UserID, MItemTaxSetting.UserDate FROM  MItemTaxSetting INNER JOIN " +
                  "MLedger ON MItemTaxSetting.TaxLedgerNo = MLedger.LedgerNo INNER JOIN MLedger AS MLedger_1 ON MItemTaxSetting.SalesLedgerNo=MLedger_1.LedgerNo " + "WHERE(MItemTaxSetting.Percentage IN(" + igs + ", " + GSTTax + "))").Table;


                MItemTaxInfo mItemTaxInfo = new MItemTaxInfo();

                for (int i = 0; i < GvHSNDetails.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(GvHSNDetails.Rows[i].Cells[7].Value) == true)
                    {
                        long ItemNo = Convert.ToInt64(GvHSNDetails.Rows[i].Cells[0].Value);

                        ////SGSTSales////

                        DataRow[] drMain = dtMainItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=51");

                        mItemTaxInfo.PkSrNo = 0;
                        mItemTaxInfo.ItemNo = ItemNo;
                        mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                        mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                        mItemTaxInfo.CalculationMethod = "2";
                        mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                        mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                        mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxInfo.UserID = DBGetVal.UserID;
                        mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                        mItemTaxInfo.FromDate = Convert.ToDateTime("10-Jan-2019");
                        dbMItemMaster.AddMItemTaxInfo2(mItemTaxInfo);


                        ////SGSTPurchase////

                        drMain = dtMainItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=51");

                        mItemTaxInfo.PkSrNo = 0;
                        mItemTaxInfo.ItemNo = ItemNo;
                        mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                        mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                        mItemTaxInfo.CalculationMethod = "2";
                        mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                        mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                        mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxInfo.UserID = DBGetVal.UserID;
                        mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                        mItemTaxInfo.FromDate = Convert.ToDateTime("10-Jan-2019");
                        dbMItemMaster.AddMItemTaxInfo2(mItemTaxInfo);

                        ////CGSTSales////

                        drMain = dtMainItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=52");

                        mItemTaxInfo.PkSrNo = 0;
                        mItemTaxInfo.ItemNo = ItemNo;
                        mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                        mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                        mItemTaxInfo.CalculationMethod = "2";
                        mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                        mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                        mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxInfo.UserID = DBGetVal.UserID;
                        mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                        mItemTaxInfo.FromDate = Convert.ToDateTime("10-Jan-2019");
                        dbMItemMaster.AddMItemTaxInfo2(mItemTaxInfo);

                        ////CGSTPurchase////
                        drMain = dtMainItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=52");

                        mItemTaxInfo.PkSrNo = 0;
                        mItemTaxInfo.ItemNo = ItemNo;
                        mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                        mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                        mItemTaxInfo.CalculationMethod = "2";
                        mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                        mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                        mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxInfo.UserID = DBGetVal.UserID;
                        mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                        mItemTaxInfo.FromDate = Convert.ToDateTime("10-Jan-2019");
                        dbMItemMaster.AddMItemTaxInfo2(mItemTaxInfo);

                        ////IGSTSales////

                        drMain = dtMainItemTax.Select("GroupNo=" + GroupType.SalesAccount + " AND TaxTypeNo=53");

                        mItemTaxInfo.PkSrNo = 0;
                        mItemTaxInfo.ItemNo = ItemNo;
                        mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                        mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                        mItemTaxInfo.CalculationMethod = "2";
                        mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                        mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                        mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxInfo.UserID = DBGetVal.UserID;
                        mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                        mItemTaxInfo.FromDate = Convert.ToDateTime("10-Jan-2019");
                        dbMItemMaster.AddMItemTaxInfo2(mItemTaxInfo);

                        ////IGSTPurchase////
                        drMain = dtMainItemTax.Select("GroupNo=" + GroupType.PurchaseAccount + " AND TaxTypeNo=53");

                        mItemTaxInfo.PkSrNo = 0;
                        mItemTaxInfo.ItemNo = ItemNo;
                        mItemTaxInfo.TaxLedgerNo = Convert.ToInt64(drMain[0][2].ToString());
                        mItemTaxInfo.SalesLedgerNo = Convert.ToInt64(drMain[0][3].ToString());
                        mItemTaxInfo.CalculationMethod = "2";
                        mItemTaxInfo.Percentage = Convert.ToDouble(drMain[0][4].ToString());
                        mItemTaxInfo.FKTaxSettingNo = Convert.ToInt64(drMain[0][0].ToString());
                        mItemTaxInfo.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxInfo.UserID = DBGetVal.UserID;
                        mItemTaxInfo.UserDate = DBGetVal.ServerTime.Date;
                        mItemTaxInfo.FromDate = Convert.ToDateTime("10-Jan-2019");
                        dbMItemMaster.AddMItemTaxInfo2(mItemTaxInfo);

                    }
                }

                if (dbMItemMaster.ExecuteNonQueryStatements() == true)
                {
                    if (ID == 0)
                    {
                        OMMessageBox.Show(" Tax Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                    else
                    {
                        OMMessageBox.Show(" Tax Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                    txtHSNCode.Text = "";
                    txtHSNCode.Focus();
                }
                else
                {
                    OMMessageBox.Show(" Tax Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DS = DialogResult.Cancel;
            this.Close();
        }

        private void GvHSNCode_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GvHSNCode.CurrentCell.ColumnIndex == 4)
            {
                BindHSNDetails();
            }
        }

        public void BindHSNCode()
        {
            string sqlQuery = "";
            sqlQuery = " SELECT distinct MItemTaxInfo.Percentage,HSNCode,max(convert(varchar, MItemTaxInfo.FromDate, 103)) as 'FromDate'," +
                       " 0 as ItemCount,'Details' as Details FROM MItemMaster INNER JOIN " +
                       " MItemTaxInfo ON MItemMaster.ItemNo = MItemTaxInfo.ItemNo and taxledgerno in(select ledgerno from mledger where groupno = 53) " +
                       " Where HSNCode like '" + txtHSNCode.Text + "%' Group By MItemTaxInfo.Percentage,HSNCode Order By Percentage";

            GvHSNCode.Rows.Clear();
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GvHSNCode.Rows.Add();
                for (int j = 0; j < GvHSNCode.Columns.Count; j++)
                {
                    GvHSNCode.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
        }

        public void BindHSNDetails()
        {
            double GSTPer = 0;
            string HSNCode = "";

            GSTPer = Convert.ToDouble(GvHSNCode.CurrentRow.Cells[0].Value);
            HSNCode = GvHSNCode.CurrentRow.Cells[1].Value.ToString();

            string sqlQuery = "";
            sqlQuery = " SELECT   distinct  MItemMaster.ItemNo,MItemMaster.HSNCode,MItemTaxInfo.Percentage, MItemGroup.ItemGroupName," +
                       "  MItemMaster.ItemName,convert(varchar, MItemTaxInfo.FromDate, 103) as FromDate, MItemGroup.ItemGroupNo,'False' as SelectHSN " +
                       " FROM MItemTaxInfo INNER JOIN MItemMaster ON MItemTaxInfo.ItemNo = MItemMaster.ItemNo and taxledgerno in(select ledgerno from mledger where groupno = 53) " +
                       " INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo Where HSNCode like '" + HSNCode + "%'  and Percentage = " + GSTPer + " " +
                       " order by  MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemTaxInfo.Percentage ";

            GvHSNDetails.Rows.Clear();
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GvHSNDetails.Rows.Add();
                for (int j = 0; j < GvHSNDetails.Columns.Count; j++)
                {
                    GvHSNDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
        }


    }
}
