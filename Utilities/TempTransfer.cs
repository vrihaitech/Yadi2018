using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using OM;
using OMControls;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
namespace Yadi.Utilities
{
    public partial class TempTransfer : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataSet dsVd = new DataSet();
        public long CompNo;
        public string LedgName, RptTitle;
        DataTable dt = new DataTable();

        public TempTransfer()
        {
            InitializeComponent();
        }

        private void TempTransfer_Load(object sender, EventArgs e)
        {
            CompNo = DBGetVal.FirmNo;
            FillList();
            txtGroupName.Focus();
            chkSelectAll.Checked = false;
            this.Cursor = Cursors.WaitCursor;
            plnLedger.Visible = true;
            this.Cursor = Cursors.Default;
            KeyDownFormat(this.Controls);
            dgTransfer.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            dgTransfer.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
            dgTransfer.RowTemplate.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Regular);
            dgTransfer.RowTemplate.Height = 30;
            dgTransfer.ColumnHeadersHeight = 30;
            dgTransfer.Columns[ColIndex.WSaleRate].DefaultCellStyle.Format = Format.DoubleFloating;

        }

        public void FillList()
        {
            ObjFunction.FillList(lstGroupName, "select distinct spno , name ,Marname  from Itemmast where itemcode > 0 and ItemonFlag='true' order by Name");
        }

        public void BindGrid()
        {
            try
            {
                string sql = "";
                sql = " SELECT  0 as SrNo ,NAME as GroupName, ITEMDESC, Mrp, UOM as UOMH, DefUom as UOML, DefUom, wsrate, ComPri, " +
                      " MktUnit, NoOfUnit,  purrate,ItemCode, SPNO, HSNCode,EstimateSales,'false' as CheckSelect,bno FROM    Itemmast where  SPNO =" + lstGroupName.SelectedValue + " and ItemonFlag='true' and bno not in(1,5)" +
                "  union all SELECT  0 as SrNo ,NAME as GroupName, ITEMDESC, Mrp, UOM as UOMH, DefUom as UOML, DefUom, wsrate, ComPri, " +
                      " MktUnit, NoOfUnit,  purrate,ItemCode, SPNO, HSNCode,EstimateSales,'false' as CheckSelect,bno FROM    Itemmast where  SPNO =" + lstGroupName.SelectedValue + " and ItemonFlag='true' and bno in (1,5) order by ITEMDESC";

                dgTransfer.Rows.Clear();
                dt = ObjFunction.GetDataView(sql).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgTransfer.Rows.Add();
                    for (int j = 0; j < dgTransfer.Columns.Count; j++)
                    {
                        dgTransfer.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();

                    }//  dgTransfer.Rows[i].Cells[ColIndex.RetailerRate].Value =Format.DoubleFloating;
                    // if (((dgTransfer.Rows[i].Cells[ColIndex.UOMH].Value != dgTransfer.Rows[i].Cells[ColIndex.UOML].Value) && (Convert.ToInt64(dgTransfer.Rows[i].Cells[ColIndex.NoOfUnit].Value) == 1)) || ((dgTransfer.Rows[i].Cells[ColIndex.UOMH].Value == dgTransfer.Rows[i].Cells[ColIndex.UOML].Value) && (Convert.ToInt64(dgTransfer.Rows[i].Cells[ColIndex.NoOfUnit].Value) > 1)))
                    if (dgTransfer.Rows[i].Cells[ColIndex.UOMH].Value.ToString() != dgTransfer.Rows[i].Cells[ColIndex.UOML].Value.ToString())
                    {

                        if (Convert.ToInt64(dgTransfer.Rows[i].Cells[ColIndex.NoOfUnit].Value) == 1)
                        {
                            dgTransfer.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                            dgTransfer.Rows[i].Cells[ColIndex.Select].ReadOnly = true;
                        }
                        if ((Convert.ToInt32(dgTransfer.Rows[i].Cells[ColIndex.bno].Value) == 1) || (Convert.ToInt32(dgTransfer.Rows[i].Cells[ColIndex.bno].Value) == 5))
                        {
                            dgTransfer.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                            dgTransfer.Rows[i].Cells[ColIndex.Select].ReadOnly = true;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt64(dgTransfer.Rows[i].Cells[ColIndex.NoOfUnit].Value) != 1)
                        {
                            dgTransfer.Rows[i].DefaultCellStyle.BackColor = Color.Pink;
                            dgTransfer.Rows[i].Cells[ColIndex.Select].ReadOnly = true;
                        }
                        if ((Convert.ToInt32(dgTransfer.Rows[i].Cells[ColIndex.bno].Value) == 1) || (Convert.ToInt32(dgTransfer.Rows[i].Cells[ColIndex.bno].Value) == 5))
                        {
                            dgTransfer.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                            dgTransfer.Rows[i].Cells[ColIndex.Select].ReadOnly = true;
                        }
                    }

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgTransfer.Rows.Count; i++)
            {
                if ((dgTransfer.Rows[i].DefaultCellStyle.BackColor != Color.Pink) && (dgTransfer.Rows[i].DefaultCellStyle.BackColor != Color.LightGreen))
                {
                    dgTransfer.Rows[i].Cells[ColIndex.Select].Value = chkSelectAll.Checked;
                }
            }
        }

        private void dgLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //btnSLedger.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                if (chkSelectAll.Checked == true)
                {
                    chkSelectAll.Checked = false;
                    chkSelectAll_CheckedChanged(sender, (EventArgs)e);
                }
                else if (chkSelectAll.Checked == false)
                {
                    chkSelectAll.Checked = true;
                    chkSelectAll_CheckedChanged(sender, (EventArgs)e);
                }
            }
            else if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Enter))
            {
                if (txtGroupName.Text != "")
                    if (Convert.ToInt32(dgTransfer.CurrentRow.Cells[ColIndex.NoOfUnit].Value) > 1)
                    {
                        ObjTrans.ExecuteQuery("Update Itemmast set NoOfUnit=" + (dgTransfer.CurrentRow.Cells[ColIndex.NoOfUnit].Value) + " where ItemCode=" + (dgTransfer.CurrentRow.Cells[ColIndex.ItemNo].Value) + " and SPNO=" + (dgTransfer.CurrentRow.Cells[ColIndex.GroupNo].Value), CommonFunctions.ConStr);

                    }
            }
            else if (e.KeyCode == (Keys.F5))
            {
                if (txtGroupName.Text != "")
                {
                    BindGrid();
                }
            }
        }

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
            if (e.KeyCode == Keys.F2)
            {

            }
            else if (e.KeyCode == Keys.F4)
            {

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Form NewF = new MDIParent1();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void dgTransfer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgTransfer.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dgTransfer.Rows[i].Cells[ColIndex.Select].Value) == true) && ((dgTransfer.Rows[i].DefaultCellStyle.BackColor != Color.Pink)) && ((dgTransfer.Rows[i].DefaultCellStyle.BackColor != Color.LightGreen)))
                {
                    ObjTrans.ExecuteQuery(" update itemmast set bno=1 where spno=" + dgTransfer.Rows[i].Cells[ColIndex.GroupNo].Value + " and itemcode= " + dgTransfer.Rows[i].Cells[ColIndex.ItemNo].Value + "", CommonFunctions.ConStr);
                }
                else if ((Convert.ToBoolean(dgTransfer.Rows[i].Cells[ColIndex.Select].Value) == false) && ((dgTransfer.Rows[i].DefaultCellStyle.BackColor != Color.Pink)) && ((dgTransfer.Rows[i].DefaultCellStyle.BackColor != Color.LightGreen)))
                {
                    ObjTrans.ExecuteQuery(" update itemmast set bno=2 where spno=" + dgTransfer.Rows[i].Cells[ColIndex.GroupNo].Value + " and itemcode= " + dgTransfer.Rows[i].Cells[ColIndex.ItemNo].Value + "", CommonFunctions.ConStr);
                }
            }
            button1.Focus();
        }

        private void txtGroupName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtGroupName.Text == "")
                {
                    pnlGroupName.Visible = true;
                    lstGroupName.Focus();
                    lstGroupName.SelectedIndex = 0;
                }
                else
                {
                    pnlGroupName.Visible = false;
                    //txtstate.Focus();
                    BindGrid();
                }
            }
            else
            {
                // e.KeyChar = Convert.ToChar(0);

                pnlGroupName.Visible = true;
                lstGroupName.Focus();

            }
        }

        private void lstGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtGroupName.Text = lstGroupName.Text;
                    pnlGroupName.Visible = false;
                    //txtstate.Focus();
                    BindGrid();

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtGroupName.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int GroupName = 1;
            public static int ItemName = 2;
            public static int MRP = 3;
            public static int UOMH = 4;
            public static int UOML = 5;
            public static int UOMD = 6;
            public static int WSaleRate = 7;
            public static int RetailerRate = 8;
            public static int MktQty = 9;
            public static int NoOfUnit = 10;
            public static int PurRate = 11;
            public static int ItemNo = 12;
            public static int GroupNo = 13;

            public static int HSNCode = 14;
            public static int EstimateSales = 15;
            public static int Select = 16;
            public static int bno = 17;


        }
        #endregion

        private void TempTransfer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.F5))
            {
                if (txtGroupName.Text != "")
                {
                    BindGrid();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.N))
            {
                long ITEMCOUNT = lstGroupName.Items.Count;
                if ((ITEMCOUNT - 1) > lstGroupName.SelectedIndex)
                {
                    lstGroupName.SelectedIndex = (lstGroupName.SelectedIndex + 1);
                    txtGroupName.Text = lstGroupName.Text;
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.P))
            {
                long ITEMCOUNT2 = lstGroupName.Items.Count;
                if (((ITEMCOUNT2 - 1) >= lstGroupName.SelectedIndex) && lstGroupName.SelectedIndex != -1)
                {
                    lstGroupName.SelectedIndex = (lstGroupName.SelectedIndex - 1);
                    txtGroupName.Text = lstGroupName.Text;
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.A))
                {
                    if (chkSelectAll.Checked == true)
                    { chkSelectAll.Checked = false;}
                    else { chkSelectAll.Checked = true; }
                }
        }
        public long GetUom(String UomName)
        {
            string uomname;
            uomname = UomName;
            long Uomno = 0;

            if (uomname.Equals("NOS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("NUMBER", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("NUMBERS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 2;

            }
            else if (uomname.Equals("Kg", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("Kgs", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 3;

            }
            else if (uomname.Equals("GRAM", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("GRAMS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("GM", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("GMS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 4;

            }
            else if (uomname.Equals("UNITS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("UNIT", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 5;

            }
            else if (uomname.Equals("CM.", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("CM", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("C.M.", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 6;

            }
            else if (uomname.Equals("LTR", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("LITER", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 7;

            }
            else if (uomname.Equals("PCS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("PC", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 8;

            }
            else if (uomname.Equals("BAGS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BAG", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 9;

            }
            else if (uomname.Equals("BUNDLE", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BUNDLES", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BDL", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BUNDAL", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 10;

            }
            else if (uomname.Equals("PKT", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("PACKET", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 11;

            }
            else if (uomname.Equals("DOZEN", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("DZ", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("DZ.", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 12;

            }
            else if (uomname.Equals("FOOT", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("FT", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 13;

            }
            else if (uomname.Equals("MTR", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 14;

            }
            else if (uomname.Equals("PAIR", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("PAIRS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 15;

            }
            else if (uomname.Equals("GATTU", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("GATTUS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 16;

            }
            else if (uomname.Equals("BOTTLE", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BT", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BOTTLES", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 17;

            }
            else if (uomname.Equals("BOX", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BOXS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 18;

            }
            else if (uomname.Equals("ROLL", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("ROLLS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("RL", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 19;

            }
            else if (uomname.Equals("SQMTR", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("SQMTRS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("SQ.MTR", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 20;

            }
            else if (uomname.Equals("YARD", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("YARDS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 21;

            }
            else if (uomname.Equals("SQFT", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("SQ.FT", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 22;

            }
            else if (uomname.Equals("CASE", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("CASES", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 23;

            }
            else if (uomname.Equals("BARNI", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("BARNIS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 24;

            }
            else if (uomname.Equals("DAG", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("DAGS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("Daga", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 25;

            }
            else if (uomname.Equals("HANGER", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("HANGERS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 26;

            }
            else if (uomname.Equals("OUTER", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("OUTERS", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("Otr", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 27;

            }

            else if (uomname.Equals("PATTI", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 28;

            }
            else if (uomname.Equals("Pote", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 29;

            }
            else if (uomname.Equals("STRIP", StringComparison.InvariantCultureIgnoreCase) || uomname.Equals("STRIPS", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 30;

            }
            else if (uomname.Equals("TIN", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 31;

            }
            else if (uomname.Equals("SET", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 32;

            }
            else if (uomname.Equals("UOM", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 33;

            }
            else if (uomname.Equals("TERUNDA", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 34;

            }
            else if (uomname.Equals("LITRE", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 35;

            }
            else if (uomname.Equals("ML", StringComparison.InvariantCultureIgnoreCase))
            {
                Uomno = 36;

            }
            else
            {
                Uomno = ObjQry.ReturnLong("select Uomno from MUOM where UomName ='" + uomname + "' ", CommonFunctions.ConStr);
            }

            return Uomno;
        }
        private int ExecuteScript(string strScript, string Constr)
        {
            if (String.IsNullOrEmpty(strScript))
            {
                return 0;
            }

            using (SqlConnection sqlConnection = new SqlConnection(Constr))
            {
                ServerConnection svrConnection = new ServerConnection(sqlConnection);
                Server server = new Server(svrConnection);
                return server.ConnectionContext.ExecuteNonQuery(strScript);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string str1 = "";
            string excludeStr = "";
            excludeStr = "select name from sys.tables where name in ('MItemGroup','MItemMaster','MItemTaxInfo','MLedger')";

            DataTable dttable = ObjFunction.GetDataView(excludeStr).Table;

            for (int i = 0; i < dttable.Rows.Count; i++)
            {
                DataTable dtColumnTargate = ObjFunction.GetDataView("select COLUMN_NAME,DATA_TYPE,COLUMN_DEFAULT from INFORMATION_SCHEMA.COLUMNS where Table_Name='" + dttable.Rows[i].ItemArray[0] + "'", CommonFunctions.ConStr).Table;
                string targatecolumnName = "";
                for (int j = 0; j < dtColumnTargate.Rows.Count; j++)
                {
                    targatecolumnName += dtColumnTargate.Rows[j].ItemArray[0].ToString();
                    targatecolumnName += ",";
                }


                if (targatecolumnName.Length > 0)

                    targatecolumnName = targatecolumnName.Remove(targatecolumnName.Length - 1);


                if (targatecolumnName.Length > 0)
                {
                    //checkUom();

                    if (dttable.Rows[i].ItemArray[0].ToString().Equals("MArea"))      //code for transfer Area table
                    {

                        DataTable MArea = ObjFunction.GetDataView("select * from Area where areacode > 0 and AreaName NOT IN('.','-') ", CommonFunctions.ConStr).Table;
                        for (int k = 0; k < MArea.Rows.Count; k++)
                        {
                            if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                            {

                                str1 = " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                 " Insert Into " + dttable.Rows[i].ItemArray[0] + " (" + targatecolumnName + ") values(" + MArea.Rows[k]["AreaCode"] + ",'" + MArea.Rows[k]["AreaName"] + "','" + MArea.Rows[k]["ShortCode"] + "'," +
                                 " '','" + MArea.Rows[k]["onoffflag"] + "',1,'06/01/2017 12:00:00 AM'," +
                                 " '',1," + MArea.Rows[k]["StatusCode"] + ")" +
                                 " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                            }
                            ExecuteScript(str1, CommonFunctions.ConStr);
                        }
                    }

                    //if (dttable.Rows[i].ItemArray[0].ToString().Equals("MCity"))    //code for transfer City table
                    //{

                    //    DataTable MCity = ObjFunction.GetDataView("select * from City where citycode > 0 and CityName NOT IN('.','-')", CommonFunctions.ConTargateStr).Table;
                    //    for (int k = 0; k < MCity.Rows.Count; k++)
                    //    {
                    //        if (objqry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConTargateStr) == 1)
                    //        {

                    //            str1 = " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                    //                   " Insert Into " + dttable.Rows[i].ItemArray[0] + " (" + targatecolumnName + ") values(" + MCity.Rows[k]["CityCode"] + ",'" + MCity.Rows[k]["CityName"] + "','" + MCity.Rows[k]["ShortCode"] + "'," +
                    //                   " '',27,'" + MCity.Rows[k]["onoffflag"] + "',1," +
                    //                   " '06/01/2017 12:00:00 AM','',1,1)" +
                    //                   " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                    //        }
                    //        ExecuteScript(str1, CommonFunctions.ConTargateStr);
                    //    }
                    //}


                    if (dttable.Rows[i].ItemArray[0].ToString().Equals("MFirm")) //code for transfer firm table
                    {
                        //String Address = "";
                        String Statecode = "";
                        //String Stateno = "";
                        DataTable MCompany = ObjFunction.GetDataView("select * from Firm ", CommonFunctions.ConStr).Table;
                        for (int k = 0; k < MCompany.Rows.Count; k++)
                        {

                            //Address = MCompany.Rows[k]["AddressCode"].ToString();
                            //Address = Address.Replace("'", "''");
                            Statecode = MCompany.Rows[k]["StateCode"].ToString();
                            //DataTable Mstate = objfun.GetDataView("select StateNo from MState Where StateCode ='" + Statecode + "' ", CommonFunctions.ConSourceStr).Table;
                            //Stateno = Mstate.Rows[k]["StateNo"].ToString();
                            if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                            {

                                str1 = " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                       " Insert Into " + dttable.Rows[i].ItemArray[0] + " (" + targatecolumnName + ") values(" + MCompany.Rows[k]["Firmcode"] + ",'.','" + MCompany.Rows[k]["FirmName"] + "'," +
                                       " '.','" + MCompany.Rows[k]["Address"] + "',0,(case When '" + Statecode + " ' IS NULL Then 0 else '" + Statecode + " ' end)," +
                                       " 27,'" + MCompany.Rows[k]["PinCode"] + "','" + MCompany.Rows[k]["OffPh-1"] + "','" + MCompany.Rows[k]["OffPh-2"] + "'," +
                                       " '" + MCompany.Rows[k]["Mobile"] + "','','" + MCompany.Rows[k]["E-mail"] + "','" + MCompany.Rows[k]["CSTNo"] + "'," +
                                       " '06/01/2017 12:00:00 AM','.','06/01/2017 12:00:00 AM','.','.','.','.','.','" + MCompany.Rows[k]["onoffflag"] + "'," +
                                       " 0,'','','',''," +
                                       "  0,'',''," +
                                       "  '','',0,1," +
                                       "  1,1,'06/01/2017 12:00:00 AM','.')" +
                                       " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                            }
                            ExecuteScript(str1, CommonFunctions.ConStr);
                        }
                    }

                    if (dttable.Rows[i].ItemArray[0].ToString().Equals("MLedger")) // code for transfer MLedger,MLedgerDetails table
                    {
                        string targatecolumnName1 = "";
                        string custname = "";
                        string maraddress = "";
                        string MarName = "";

                        targatecolumnName = targatecolumnName.Replace("LedgerNo,", string.Empty);
                        string ledgerno = "";
                        DataTable MCustomer = ObjFunction.GetDataView("select * from customer where custcode > 0 and CustomerName NOT IN('CASH SALE','.',' CASH SALE') and custtype=0 order by customername", CommonFunctions.ConStr).Table;
                        for (int k = 0; k < MCustomer.Rows.Count; k++)
                        {
                            custname = MCustomer.Rows[k]["CustomerName"].ToString();
                            custname = custname.Replace("'", "''");
                            MarName = MCustomer.Rows[k]["marname"].ToString();
                            MarName = MarName.Replace("'", "''");
                            if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                            {


                                str1 =
                                       " Insert Into " + dttable.Rows[i].ItemArray[0] + " (" + targatecolumnName + ") values(" + MCustomer.Rows[k]["CustCode"] + ",'" + custname.ToUpper() + "'," +
                                       "'" + custname.ToUpper() + "','" + MarName + "',26," + MCustomer.Rows[k]["Openingbal"] + "," +
                                       " 0 ,27,'','" + MCustomer.Rows[k]["OnOffFlag"] + "'," +
                                       " 1,0,1 ,'06/01/2017 12:00:00 AM'," +
                                       " '',1,'',''," +
                                       " '" + MCustomer.Rows[k]["MarName"] + "',0,0,0)";

                            }
                            ExecuteScript(str1, CommonFunctions.ConStr);

                            // if (dttable.Rows[i].ItemArray[0].ToString().Equals("MLedgerDetails"))
                            // {
                            targatecolumnName1 = "LedgerNo,Address,StateNo,CityNo,PinCode,PhNo1,PhNo2,MobileNo1,MobileNo2,EmailID,DOB,QualificationNo,OccupationNo,CustomerType,CreditDays,CreditLimit,PANNo,VATNo,CSTNo,UserID,UserDate,ModifiedBy,StatusNo,CompanyNo,FSSAIDate,GSTNO,AdharCardNo,GSTDate,FSSAI,AreaNo,CustTaxTypeNo,AccountNo,AddressLang,BillPrintType,RateTypeNo,DiscRs,DiscPer,AnyotherNo1,AnyotherNo2";
                            DataTable Mledger = ObjFunction.GetDataView("select LedgerNo from MLedger where LedgerName ='" + MCustomer.Rows[k]["CustomerName"].ToString() + "' and GroupNo = 26 ", CommonFunctions.ConStr).Table;
                            ledgerno = Mledger.Rows[0]["LedgerNo"].ToString();
                            maraddress = MCustomer.Rows[k]["MarAddress"].ToString();
                            maraddress = maraddress.Replace("'", "''");
                            if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                            {

                                str1 =// " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                       " Insert Into MLedgerDetails (" + targatecolumnName1 + ") values('" + ledgerno + "','" + MCustomer.Rows[k]["Address"] + "'," +
                                       " 27,0,'" + MCustomer.Rows[k]["PinCode"] + "','" + MCustomer.Rows[k]["OffPh-1"] + "','" + MCustomer.Rows[k]["OffPh-2"] + "'," +
                                       " '" + MCustomer.Rows[k]["Mobile"] + "','','" + MCustomer.Rows[k]["email"] + "','',0," +
                                       " 0 ,0,0,0," +
                                       " '','','',1,'06/01/2017 12:00:00 AM'," +
                                       " '',1,1 ,'06/01/2017 12:00:00 AM'," +
                                       " '" + MCustomer.Rows[k]["fax"] + "','','06/01/2017 12:00:00 AM',''," + MCustomer.Rows[k]["AreaCode"] + "," +
                                       "  1,'','" + maraddress.Trim() + "',0," +
                                       " 0,0,0,'','')";
                                // " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                            }
                            ExecuteScript(str1, CommonFunctions.ConStr);
                            // }
                            ObjTrans.ExecuteQuery("update customer set custtype=1 where  custcode= " + MCustomer.Rows[k]["custcode"].ToString() + "", CommonFunctions.ConStr);
                        }

                        //===================================================================end of customer
                        DataTable MCompany = ObjFunction.GetDataView("select * from Company  where CompCode >0 and CompanyName NOT IN('CASH','CASH PURCHASE','.','-') and custtype=0 order by companyname", CommonFunctions.ConStr).Table;
                        for (int k = 0; k < MCompany.Rows.Count; k++)
                        {
                            string discper = "";
                            string compname = "";
                            string stateno = "";
                            compname = MCompany.Rows[k]["CompanyName"].ToString();
                            compname = compname.Replace("'", "''");
                            stateno = MCompany.Rows[k]["StateNo"].ToString();
                            if (string.IsNullOrEmpty(stateno) == true)
                            {
                                stateno = "27";
                            }

                            if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                            {
                                discper = MCompany.Rows[k]["DiscPer"].ToString();
                                if (string.IsNullOrEmpty(discper))
                                {
                                    discper = "0";
                                }
                                else
                                {
                                    discper = MCompany.Rows[k]["DiscPer"].ToString();
                                }


                                str1 =
                                       " Insert Into " + dttable.Rows[i].ItemArray[0] + " (" + targatecolumnName + ") values(" + MCompany.Rows[k]["CompCode"] + ",'" + compname.ToUpper() + "'," +
                                       " '" + compname.ToUpper() + "','',22," + MCompany.Rows[k]["OpBal"] + "," +
                                       " 0 ,'" + stateno + "','','" + MCompany.Rows[k]["OnOffFlag"] + "'," +
                                       " 1,0,1 ,'06/01/2017 12:00:00 AM'," +
                                       " '',1,'',''," +
                                       " '',0,0,0)";

                            }
                            ExecuteScript(str1, CommonFunctions.ConStr);

                            // String targatecolumnName1 = "LedgerNo,Address,StateNo,CityNo,PinCode,PhNo1,PhNo2,MobileNo1,MobileNo2,EmailID,DOB,QualificationNo,OccupationNo,CustomerType,CreditDays,CreditLimit,PANNo,VATNo,CSTNo,UserID,UserDate,ModifiedBy,StatusNo,CompanyNo,FSSAIDate,GSTNO,AdharCardNo,GSTDate,FSSAI,AreaNo,CustTaxTypeNo,AccountNo,AddressLang,BillPrintType,RateTypeNo,DiscRs,DiscPer,AnyotherNo1,AnyotherNo2";
                            DataTable Mledger1 = ObjFunction.GetDataView("select LedgerNo from MLedger where LedgerName ='" + MCompany.Rows[k]["CompanyName"].ToString() + "' and GroupNo = 22 ", CommonFunctions.ConStr).Table;
                            ledgerno = Mledger1.Rows[0]["LedgerNo"].ToString();
                            if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                            {

                                str1 =// " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                       " Insert Into MLedgerDetails (" + targatecolumnName1 + ") values('" + ledgerno + "','" + MCompany.Rows[k]["Address"] + "'," +
                                       " " + MCompany.Rows[k]["StateNo"] + ",0,'" + MCompany.Rows[k]["PinCode"] + "','" + MCompany.Rows[k]["OffPh1"] + "','" + MCompany.Rows[k]["OffPh2"] + "'," +
                                       " '" + MCompany.Rows[k]["Mobile"] + "','','" + MCompany.Rows[k]["EMail"] + "','',0," +
                                       " 0 ,0,0,0," +
                                       " '','','" + MCompany.Rows[k]["Cstno"] + "',1,'06/01/2017 12:00:00 AM'," +
                                       " '',1,1 ,'06/01/2017 12:00:00 AM'," +
                                       " '" + MCompany.Rows[k]["BstNo"] + "','','06/01/2017 12:00:00 AM',''," + MCompany.Rows[k]["AreaCode"] + "," +
                                       "  1,'','',0," +
                                       " 0,0,'" + discper + "','','')";

                                // " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                            }
                            ExecuteScript(str1, CommonFunctions.ConStr);

                            ObjTrans.ExecuteQuery("update Company set custtype=1 where  CompCode= " + MCompany.Rows[k]["CompCode"].ToString() + "", CommonFunctions.ConStr);
                        }
                    }




                    //====================================================================================================
                    //===================================MItemGroup=====================
                    if (dttable.Rows[i].ItemArray[0].ToString().Equals("MItemGroup"))
                    {
                        string itemname = "";
                        string langname = "";
                        string groupname = "";
                        string groupno = "";
                        long uomprimary = 0;
                        long loweruom = 0;
                        string langitem = "";
                        string itemno = "";
                        targatecolumnName = targatecolumnName.Replace("ItemGroupNo,", string.Empty);
                        string unitname = "";
                        string defunitname = "";
                        string noofunit = "";
                        DataTable MStockGr = ObjFunction.GetDataView("select distinct spno , name ,Marname  from Itemmast where itemcode > 0 and name='" + txtGroupName.Text.ToString().Trim() + "'  order by Name ", CommonFunctions.ConStr).Table;
                        //   DataTable MStockGr = objfun.GetDataViewAccess("select distinct spno , name ,Marname,ItemonFlag,margin  from Itemmast where  spno in (select spno from itemmast where itemcode > 0) order by spno ", CommonFunctions.ConSourceStr).Table;
                        for (int m = 0; m < MStockGr.Rows.Count; m++)
                        {

                            groupname = MStockGr.Rows[m]["Name"].ToString();
                            groupname = groupname.Replace("'", "''");
                            langname = MStockGr.Rows[m]["MarName"].ToString();
                            langname = langname.Replace("'", "''");

                            if (ObjQry.ReturnInteger("Select Count (ItemGroupName) from MItemGroup where MItemGroup.ItemGroupName='" + groupname + "'", CommonFunctions.ConStr) == 0)
                            {
                                if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                                {

                                    str1 = //" SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                           " Insert Into " + dttable.Rows[i].ItemArray[0] + " (" + targatecolumnName + ") values('" + groupname.ToUpper() + "'," +
                                           " '" + langname + "',3,8,'1'," +
                                           " 1,'06/01/2017 12:00:00 AM','',1 ,1,0)";
                                    // " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                                }
                                ExecuteScript(str1, CommonFunctions.ConStr);
                            }
                            String ItemMastercolumnName = "ItemName,ItemShortName,Barcode,ShortCode,GroupNo,UOMH,UOML,UOMDefault,FkDepartmentNo,FkCategoryNo,MinLevel,MaxLevel,ReOrderLevelQty,LangFullDesc,LangShortDesc,CompanyNo,IsActive,UserId,UserDate,ModifiedBy,StatusNo,ControlUnder,FactorVal,Margin,CessValue,PackagingCharges,Dhekhrek,OtherCharges,HigherVariation,LowerVariation,HSNCode,FKStockGroupTypeNo,ESFlag";
                            DataTable itemgroup = ObjFunction.GetDataView("select ItemGroupNo from MItemGroup where ItemGroupName ='" + MStockGr.Rows[m]["Name"].ToString() + "'", CommonFunctions.ConStr).Table;
                            groupno = itemgroup.Rows[0]["ItemGroupNo"].ToString();

                            DataTable MStockGroup = ObjFunction.GetDataView("select * from Itemmast where itemcode > 0 and  spno=" + MStockGr.Rows[m]["SPNO"] + " and itemonflag<>0 and bno=1 order by spno ", CommonFunctions.ConStr).Table;
                            //  DataTable MStockGroup = objfun.GetDataViewAccess("select * from Itemmast where itemcode > 0 and ItemonFlag = 0 ", CommonFunctions.ConSourceStr).Table;

                            for (int k = 0; k < MStockGroup.Rows.Count; k++)
                            {

                                unitname = MStockGroup.Rows[k]["uom"].ToString();
                                uomprimary = GetUom(unitname);

                                defunitname = MStockGroup.Rows[k]["defUom"].ToString();
                                loweruom = GetUom(defunitname);

                                itemname = MStockGroup.Rows[k]["ITEMDESC"].ToString();
                                itemname = itemname.Replace("'", "''");
                                langitem = MStockGroup.Rows[k]["MarItemDesc"].ToString();
                                langitem = langitem.Replace("'", "''");
                                if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                                {
                                    str1 = //" SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                          " Insert Into MItemMaster (" + ItemMastercolumnName + ") values ('" + itemname + " ','" + itemname.ToUpper() + "','" + MStockGroup.Rows[k]["ItembarCOde"] + "','" + MStockGroup.Rows[k]["ItembarCOde"] + "'," +
                                       " '" + groupno + "'," + uomprimary + "," + loweruom + "," + loweruom + ",1,1,0,0, " +
                                        "0,'" + langitem + "','" + langitem + "'," +
                                       " 1 ,'" + MStockGroup.Rows[k]["ItemonFlag"] + "',1,'06/01/2017 12:00:00 AM'," +
                                       " '',1,0,0," +
                                         " " + MStockGroup.Rows[k]["margin"] + ",0,0,0,0,0," +
                                      " 0,'" + MStockGroup.Rows[k]["HSNCode"] + "',1,'" + MStockGroup.Rows[k]["EstimateSales"] + "')";
                                    //" SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";

                                }
                                ExecuteScript(str1, CommonFunctions.ConStr);

                                String ratesettingcolumnName = "ItemNo,FromDate,PurRate,MRP,UOMNo,ASaleRate,BSaleRate,CSaleRate,DSaleRate,ESaleRate,StockConversion,MKTQty,IsActive,UserID,UserDate,ModifiedBy,CompanyNo,StatusNo,Stock2,Stock,PerOfRateVariation";

                                DataTable mitemno = ObjFunction.GetDataView("select max(itemno) as itemno from MItemMaster where ItemName='" + itemname + "' AND GROUPNO=" + groupno + "", CommonFunctions.ConStr).Table;
                                itemno = mitemno.Rows[0]["itemno"].ToString();
                                if (uomprimary != loweruom)
                                {

                                    if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                                    {

                                        str1 = //" SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                               " Insert Into MRateSetting (" + ratesettingcolumnName + ") values('" + itemno + "','06/01/2017 12:00:00 AM'," +
                                               " " + MStockGroup.Rows[k]["PurRate"] + "," + MStockGroup.Rows[k]["Mrp"] + "," + loweruom + "," + MStockGroup.Rows[k]["MktRate"] + "," + MStockGroup.Rows[k]["wsrate"] + "," +
                                               " " + MStockGroup.Rows[k]["ComPri"] + ",0,0,1," +
                                               " " + MStockGroup.Rows[k]["MktUnit"] + ",'1',1 ,'06/01/2017 12:00:00 AM',''," +
                                               " 1,1,0,0,0)";
                                        // " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                                    }
                                    ExecuteScript(str1, CommonFunctions.ConStr);

                                    if ((defunitname.Equals("GRAM", StringComparison.InvariantCultureIgnoreCase)) && (unitname.Equals("KG", StringComparison.InvariantCultureIgnoreCase)))
                                    {
                                        noofunit = "1000";
                                    }
                                    else
                                    {
                                        noofunit = MStockGroup.Rows[k]["NoOfUnit"].ToString();
                                    }
                                    if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                                    {

                                        str1 = //" SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                               " Insert Into MRateSetting (" + ratesettingcolumnName + ") values('" + itemno + "','06/01/2017 12:00:00 AM'," +
                                               " " + MStockGroup.Rows[k]["PurRate"] + "," + MStockGroup.Rows[k]["Mrp"] + "," + uomprimary + "," + MStockGroup.Rows[k]["CaseRate"] + "," + MStockGroup.Rows[k]["wsrate"] + "," +
                                               " " + MStockGroup.Rows[k]["ComPri"] + ",0,0,'" + noofunit + "'," +
                                               " " + MStockGroup.Rows[k]["MktUnit"] + ",'1',1 ,'06/01/2017 12:00:00 AM',''," +
                                               " 1,1,0,0,0)";
                                        // " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                                    }
                                    ExecuteScript(str1, CommonFunctions.ConStr);
                                }

                                else
                                {
                                    if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                                    {

                                        str1 = //" SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                               " Insert Into MRateSetting (" + ratesettingcolumnName + ") values('" + itemno + "','06/01/2017 12:00:00 AM'," +
                                               " " + MStockGroup.Rows[k]["PurRate"] + "," + MStockGroup.Rows[k]["Mrp"] + "," + loweruom + "," + MStockGroup.Rows[k]["MktRate"] + "," + MStockGroup.Rows[k]["wsrate"] + "," +
                                               " " + MStockGroup.Rows[k]["ComPri"] + ",0,0,1," +
                                               " " + MStockGroup.Rows[k]["MktUnit"] + ",'1',1 ,'06/01/2017 12:00:00 AM',''," +
                                               " 1,1,0,0,0)";
                                        // " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                                    }
                                    ExecuteScript(str1, CommonFunctions.ConStr);

                                }

                                String taxinfocolumnName = "FKTaxSettingNo,ItemNo,TaxLedgerNo,SalesLedgerNo,FromDate,Percentage,CompanyNo,UserID,UserDate,ModifiedBy,StatusNo,CalculationMethod";

                                DataTable Mtaxinfo = ObjFunction.GetDataView("select * from  Mitemtaxsetting where percentage =" + MStockGroup.Rows[k]["SalesVat"] + " or percentage=" + MStockGroup.Rows[k]["SalesVat"] + "*2 or taxsettingname='0 % Cess Sales ' or taxsettingname='0 % Cess Purchase'  ", CommonFunctions.ConStr).Table;
                                //  DataTable MStockGroup = objfun.GetDataViewAccess("select * from Itemmast where itemcode > 0 and ItemonFlag = 0 ", CommonFunctions.ConSourceStr).Table;

                                for (int j = 0; j < Mtaxinfo.Rows.Count; j++)
                                {
                                    if (ObjQry.ReturnInteger("SELECT OBJECTPROPERTY(OBJECT_ID('" + dttable.Rows[i].ItemArray[0] + "'), 'TableHasIdentity') AS TableHasIdentity", CommonFunctions.ConStr) == 1)
                                    {


                                        str1 = //" SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  ON ; " +
                                                " Insert Into MItemTaxInfo (" + taxinfocolumnName + ") values(" + Mtaxinfo.Rows[j]["PkSrNo"] + ",'" + itemno + "'," +
                                               " " + Mtaxinfo.Rows[j]["TaxLedgerNo"] + "," + Mtaxinfo.Rows[j]["SalesLedgerNo"] + ",'1/1/2018 12:46:37 PM'," + Mtaxinfo.Rows[j]["Percentage"] + "," +
                                               " 1 ,1,'06/01/2017 12:00:00 AM'," +
                                                " '',1,2)";
                                        // " SET IDENTITY_INSERT  " + dttable.Rows[i].ItemArray[0] + "  OFF ;";
                                    }
                                    ExecuteScript(str1, CommonFunctions.ConStr);
                                }

                                ObjTrans.ExecuteQuery(" update itemmast set bno=5 where  itemcode= " + MStockGroup.Rows[k]["itemcode"].ToString() + "", CommonFunctions.ConStr);
                            }
                        }

                    }
                    Application.DoEvents();

                }
            }

            MessageBox.Show("Master table transfer successfully ...");
            txtGroupName.Focus();
        }

        private void lstGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstGroupName.SelectedIndex >= 1)
                BindGrid();
        }


    }
}
