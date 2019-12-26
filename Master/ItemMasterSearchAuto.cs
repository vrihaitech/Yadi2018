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
using System.Data.SqlClient;

namespace Yadi.Master
{
    public partial class ItemMasterSearchAuto : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMItemMaster dbMitemMaster = new DBMItemMaster();
        public static long RequestItemNo;
        long S1 = 0, S2 = 0, S3 = 0;
        string strS1 = "", strS2 = "", strS3 = "";
        string strP = ""; string strB = ""; string strV = "";
        public ItemMasterSearchAuto()
        {
            InitializeComponent();
        }

        private void ItemMasterSearchAuto_Load(object sender, EventArgs e)
        {
            // chkBrand.Checked = true;
            KeyDownFormat(this.Controls);
            FillList();
            //BindGrid();
            txtSearch.Focus();
        }

        public void FillList()
        {
            //============Vehicle
            ObjFunction.FillList(lstVehicle, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 and  ItemGroupNo in (select GroupNo  from MItemMaster where MItemMaster.IsActive = 'True') ORDER BY ItemGroupName");
            //============Brand
            ObjFunction.FillList(lstBrand, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 and  ItemGroupNo in (select fkdepartmentno from MItemMaster where MItemMaster.IsActive = 'True') ORDER BY ItemGroupName");
            //============Product
            ObjFunction.FillList(lstProduct, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 and  ItemGroupNo in (select FkCategoryNo  from MItemMaster where MItemMaster.IsActive = 'True' ) ORDER BY ItemGroupName");

        }
        private void BindGrid()
        {
            try
            {
                DataView dv = new DataView();
                dv = GetBySearch();
                DataGridView1.DataSource = dv;
                DataGridView1.Columns[0].Visible = false;
                DataGridView1.Columns[1].Width = 200;
                DataGridView1.Columns[2].Width = 200;
                DataGridView1.Columns[3].Width = 200;
                DataGridView1.Columns[4].Width = 200;
                DataGridView1.Columns[5].Width = 200;
                DataGridView1.Columns[6].Width = 70;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public DataView GetBySearch()
        {
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string sql = null;
         
                                sql = " SELECT     MItemMaster.ItemNo, MItemGroup_1.ItemGroupName AS 'Brand', MItemGroup_2.ItemGroupName  AS 'Product', MItemGroup.ItemGroupName AS 'Vehicle',  " +
                   " MItemMaster.ItemName AS 'ItemName', MItemMaster.Barcode AS 'Barcode',  CASE WHEN (MItemMaster.IsActive = 'True') THEN 'True' ELSE 'False' END AS 'Status' " +
                   " FROM         MItemMaster INNER JOIN     MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                   " MItemGroup AS MItemGroup_1 ON MItemMaster.FkDepartmentNo = MItemGroup_1.ItemGroupNo INNER JOIN " +
                   " MItemGroup AS MItemGroup_2 ON MItemMaster.FkCategoryNo = MItemGroup_2.ItemGroupNo " +
                   " order by MItemGroup_1.ItemGroupName, MItemGroup_2.ItemGroupName,MItemGroup.ItemGroupName, MItemMaster.ItemName,MItemMaster.Barcode";

            if (txtS1.Text!="")
            {
                if ((strS1 == "Brand Name") && (S1 != 0))
                {
                    str1 = " Where  MItemGroup_1.ItemGroupNo=" + S1;
                }
                else if ((strS1 == "Product Name") && (S1 != 0))
                {
                    str1 = "  Where  MItemGroup_2.ItemGroupNo=" + S1;
                }
                else if ((strS1 == "Vehicle Name") && (S1 != 0))
                {
                    str1 = "  Where  MItemGroup.ItemGroupNo=" + S1;
                }
            }
            if (txtS2.Text != "")
            {
                if ((strS2 == "Brand Name") && (S2 != 0))
                {
                    str2 = " and  MItemGroup_1.ItemGroupNo=" + S2;
                }
                else if ((strS2 == "Product Name") && (S2 != 0))
                {
                    str2 = "  and  MItemGroup_2.ItemGroupNo=" + S2;
                }
                else if ((strS2 == "Vehicle Name") && (S2 != 0))
                {
                    str2 = "  and  MItemGroup.ItemGroupNo=" + S2;
                }
            }
            if (txtS3.Text != "")
            {
                if ((strS3 == "Brand Name") && (S3 != 0))
                {
                    str3 = " and  MItemGroup_1.ItemGroupNo=" + S3;
                }
                else if ((strS3 == "Product Name") && (S3 != 0))
                {
                    str3 = "  and  MItemGroup_2.ItemGroupNo=" + S3;
                }
                else if ((strS3 == "Vehicle Name") && (S3 != 0))
                {
                    str3 = "  and  MItemGroup.ItemGroupNo=" + S3;
                }
            }

            sql = sql.Replace("order by MItemGroup_1.ItemGroupName,",str1 + " "+ str2 + " " + str3 + " order by MItemGroup_1.ItemGroupName, ");
            DataSet ds = new DataSet();
            try
            {
                ds = ObjDset.FillDset("New", sql, CommonFunctions.ConStr);
            }
            catch (SqlException e)
            {
                CommonFunctions.ErrorMessge = e.Message;
            }
            return ds.Tables[0].DefaultView;
        }


        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (txtS1.Text != "")
            {
                BindGrid();
            }

            else
            {
                DataGridView1.DataSource = null;
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
    
            if (txtS1.Text != "")
            {
                BindGrid();
            }

            else
            {
                DataGridView1.DataSource = null;
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {

            BindGrid();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (DataGridView1.Rows.Count > 0)
                {
                    DataGridView1.Focus();
                    DataGridView1.CurrentCell = DataGridView1[1, 0];
                }
            }
            if (e.KeyCode == Keys.Enter)
            {

                if (chkBrand.Checked == true)
                {
                    pnlBrand.Visible = true;
                    lstBrand.Focus();
                }
                else if (chkProduct.Checked == true)
                {
                    pnlProduct.Visible=true;
                    lstProduct.Focus();

                }
                else if (chkVehicle.Checked == true)
                {
                    pnlVehicle.Visible = true;
                    lstVehicle.Focus();
                }
                else {
                    OMMessageBox.Show("Please select Filter Option ....");
                }
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
                {
                    DataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
                }
            }
            catch { }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RequestItemNo = Convert.ToInt32(DataGridView1.SelectedRows[0].Cells[0].Value);
            Form NewF = new ItemMasterAUTOAE();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            chkClearAll.Checked = true;
            chkClearAll_CheckedChanged(sender, e);
           // this.Close();
        }



        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                ObjFunction.SetGridStatus(e);
            }
        }

        private void lstVehicle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if ((strS1 == "" && S1 == 0)||(lblS1.Text == "Vehicle Name"))
                {
                    strS1 = "Vehicle Name";
                    lblS1.Text = "Vehicle Name";
                    txtS1.Text = lstVehicle.Text;
                    S1 = Convert.ToInt32(lstVehicle.SelectedValue);
                    txtS2.Text = "";
                    lblS2.Text = "";
                    strS2 = "";
                    S2 = 0;
                    txtS3.Text = "";
                    lblS3.Text = "";
                    strS3 = "";
                    S3 = 0;
                }
                else if ((strS2 == "" && S2 == 0)||(lblS2.Text == "Vehicle Name"))
                {
                    strS2 = "Vehicle Name";
                    lblS2.Text = "Vehicle Name";
                    txtS2.Text = lstVehicle.Text;
                    S2 = Convert.ToInt32(lstVehicle.SelectedValue);

                    txtS3.Text = "";
                    lblS3.Text = "";
                    strS3 = "";
                    S3 = 0;
                }
                else if ((strS3 == "" && S3 == 0)|| (lblS3.Text == "Vehicle Name"))
                {
                    strS3 = "Vehicle Name";
                    lblS3.Text = "Vehicle Name";
                    txtS3.Text = lstVehicle.Text;
                    S3 = Convert.ToInt32(lstVehicle.SelectedValue);
                }
                pnlVehicle.Visible = false;
            }
        }

        private void lstProduct_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==Convert.ToChar(Keys.Enter))
            {
                if ((strS1 == "" && S1 == 0)||(lblS1.Text == "Product Name"))
                {
                    strS1 = "Product Name";
                    lblS1.Text = "Product Name";
                    txtS1.Text = lstProduct.Text;
                    S1 = Convert.ToInt32(lstProduct.SelectedValue);
                    txtS2.Text = "";
                    lblS2.Text = "";
                    strS2 = "";
                    S2 = 0;
                    txtS3.Text = "";
                    lblS3.Text = "";
                    strS3 = "";
                    S3 = 0;
                }
                else if ((strS2 == "" && S2 == 0)||(lblS2.Text == "Product Name"))
                {
                    strS2 = "Product Name";
                    lblS2.Text = "Product Name";
                    txtS2.Text = lstProduct.Text;
                    S2 = Convert.ToInt32(lstProduct.SelectedValue);

                    txtS3.Text = "";
                    lblS3.Text = "";
                    strS3 = "";
                    S3 = 0;
                }
                else if ((strS3 == "" && S3 == 0)||(lblS3.Text == "Product Name"))
                {
                    strS3 = "Product Name";
                    lblS3.Text = "Product Name";
                    txtS3.Text = lstProduct.Text;
                    S3 = Convert.ToInt32(lstProduct.SelectedValue);
                }
                pnlProduct.Visible = false;
            }
        }

        private void lstBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                if((strS1=="" && S1==0)||(lblS1.Text == "Brand Name"))
                {
                    strS1= "Brand Name";
                    lblS1.Text = "Brand Name";
                    txtS1.Text = lstBrand.Text;
                    S1 =Convert.ToInt32(lstBrand.SelectedValue);
                    txtS2.Text = "";
                    lblS2.Text = "";
                    strS2 = "";
                    S2 = 0;

                    txtS3.Text = "";
                    lblS3.Text = "";
                    strS3 = "";
                    S3 = 0;
                }
                else if ((strS2 == "" && S2 == 0)|| (lblS2.Text == "Brand Name"))
                {
                    strS2 = "Brand Name";
                    lblS2.Text = "Brand Name";
                    txtS2.Text = lstBrand.Text;
                    S2 = Convert.ToInt32(lstBrand.SelectedValue);

                    txtS3.Text = "";
                    lblS3.Text = "";
                    strS3 = "";
                    S3 = 0;
                }
                else if ((strS3 == "" && S3 == 0)|| (lblS3.Text == "Brand Name"))
                {
                    strS3= "Brand Name";
                    lblS3.Text = "Brand Name";
                    txtS3.Text = lstBrand.Text;
                    S3 = Convert.ToInt32(lstBrand.SelectedValue);
                }
                pnlBrand.Visible = false; 
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkBrand_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBrand.Checked == true)
            {
                if ((S1 != 0) && (strS1 != "Brand Name"))
                {
                    if (strS1 == "Product Name" && strS2 == "")
                    {
                      strB="SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 and  ItemGroupNo in (select fkdepartmentno from MItemMaster where MItemMaster.IsActive = 'True' and FkCategoryNo =" + S1 +") ORDER BY ItemGroupName";

                    }
                    else if (strS1 == "Vehicle Name" && strS2 == "")
                    {
                       strB= "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 and  ItemGroupNo in (select fkdepartmentno from MItemMaster where MItemMaster.IsActive = 'True' and GroupNo =" + S1 + ") ORDER BY ItemGroupName";

                    }
              
                    if (strS2 == "Product Name")
                    {
                    strB="SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 and  ItemGroupNo in (select fkdepartmentno from MItemMaster where MItemMaster.IsActive = 'True' and FkCategoryNo =" + S2 + " and GroupNo =" + S1 + ") ORDER BY ItemGroupName";

                    }
                    else if (strS2 == "Vehicle Name")
                    {
                       strB="SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 and  ItemGroupNo in (select fkdepartmentno from MItemMaster where MItemMaster.IsActive = 'True' and GroupNo =" + S2 + " and FkCategoryNo =" + S1 + ") ORDER BY ItemGroupName";


                    }
                }
                else
                {
                   strB= "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=4 and  ItemGroupNo in (select fkdepartmentno from MItemMaster where MItemMaster.IsActive = 'True') ORDER BY ItemGroupName";

                }
                ObjFunction.FillList(lstBrand, strB);
                pnlBrand.Visible = true;
                lstBrand.Focus();
                chkClearAll.Checked = false;
                chkProduct.Checked = false;
                chkVehicle.Checked = false;
                pnlVehicle.Visible = false;
                pnlProduct.Visible = false;
            }

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
                chkBrand.Checked = true;
                chkBrand_CheckedChanged(sender, e);
                lstBrand.Focus();
                
            }
            else if (e.KeyCode == Keys.F2)
            {
                chkProduct.Checked = true;
                chkProduct_CheckedChanged(sender, e);
                lstProduct.Focus();
            }
            else if (e.KeyCode == Keys.F3)
            {
                chkVehicle.Checked = true;
                chkVehicle_CheckedChanged(sender, e);
                lstVehicle.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if(DataGridView1.Rows.Count>0)
                { DataGridView1.Focus(); }
            }


        }
        #endregion
        private void chkProduct_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProduct.Checked == true)
            {


                if ((S1 != 0) && (strS1 != "Product Name"))
                {
                    if (strS1 == "Brand Name" && strS2 == "")
                    {
                    strP="SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 and  ItemGroupNo in (select FkCategoryNo  from MItemMaster where MItemMaster.IsActive = 'True' and fkdepartmentno =" + S1 + ") ORDER BY ItemGroupName";
            
                    }
                    else if (strS1 == "Vehicle Name" && strS2 == "")
                    {
                       strP= "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 and  ItemGroupNo in (select FkCategoryNo  from MItemMaster where MItemMaster.IsActive = 'True' and groupno =" + S1 + ") ORDER BY ItemGroupName";

                    }
              
                    if (strS2 == "Brand Name")
                    {
                     strP= "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 and  ItemGroupNo in (select FkCategoryNo from MItemMaster where MItemMaster.IsActive = 'True' and fkdepartmentno =" + S2 + " and GroupNo =" + S1 + ") ORDER BY ItemGroupName";

                    }
                    else if (strS2 == "Vehicle Name")
                    {
                       strP= "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 and  ItemGroupNo in (select FkCategoryNo from MItemMaster where MItemMaster.IsActive = 'True' and GroupNo =" + S2 + " and fkdepartmentno =" + S1 + ") ORDER BY ItemGroupName";


                    }
                }
                else
                {
                    strP= "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 and  ItemGroupNo in (select FkCategoryNo  from MItemMaster where MItemMaster.IsActive = 'True' ) ORDER BY ItemGroupName";

                }
                ObjFunction.FillList(lstProduct, strP);
                pnlProduct.Visible = true;
                lstProduct.Focus();
                chkClearAll.Checked = false;
                chkBrand.Checked = false;
                chkVehicle.Checked = false;
                pnlBrand.Visible = false;
                pnlVehicle.Visible = false;
            }
        }

        private void chkVehicle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkVehicle.Checked == true)
            {
                if((S1 != 0)&&(strS1 != "Vehicle Name"))
                {
                    if (strS1 == "Brand Name" && strS2 == "")
                    {
                        strV = "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 and  ItemGroupNo in (select GroupNo  from MItemMaster where MItemMaster.IsActive = 'True' and fkdepartmentno =" + S1 + ") ORDER BY ItemGroupName";
                                               
                    }
                    else if (strS1 == "Product Name" && strS2 == "" )
                    {
                        strV = "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND     ControlGroup=3 and  ItemGroupNo in (select GroupNo  from MItemMaster where MItemMaster.IsActive = 'True' and FkCategoryNo =" + S1 + ") ORDER BY ItemGroupName";

                    }

                    if (strS2 == "Brand Name")
                    {
                        strV = "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 and  ItemGroupNo in (select GroupNo  from MItemMaster where MItemMaster.IsActive = 'True' and fkdepartmentno =" + S2 + " and FkCategoryNo =" + S1 + ") ORDER BY ItemGroupName";
                    }
                    else if (strS2 == "Product Name")
                    {
                        strV = "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 and  ItemGroupNo in (select GroupNo  from MItemMaster where MItemMaster.IsActive = 'True' and fkdepartmentno =" + S1 + " and FkCategoryNo =" + S2 + ") ORDER BY ItemGroupName";

                    }

                }
                else
                {
                    strV= "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 and  ItemGroupNo in (select GroupNo  from MItemMaster where MItemMaster.IsActive = 'True') ORDER BY ItemGroupName";

                }
                ObjFunction.FillList(lstVehicle, strV);
                pnlVehicle.Visible = true;
                lstVehicle.Focus();
                chkClearAll.Checked = false;
                chkProduct.Checked = false;
                chkBrand.Checked = false;
                pnlBrand.Visible = false;
                pnlProduct.Visible = false;
            }
        }

        private void chkClearAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClearAll.Checked == true)
            {
                pnlBrand.Visible = false;
                pnlProduct.Visible = false;
                pnlVehicle.Visible = false;

                chkVehicle.Checked = false;
                chkProduct.Checked = false;
                chkBrand.Checked = false;
                S1 = 0; S2 = 0; S3 = 0;
                strS1 = ""; strS2 = ""; strS3 = "";
                txtS1.Text = "";txtS2.Text = "";
                txtS3.Text = "";
            }

        }
    }
}

