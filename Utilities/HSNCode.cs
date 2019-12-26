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
    public partial class HSNCode : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMItemMaster dbMItemMaster = new DBMItemMaster();
        MItemMaster mItemMaster = new MItemMaster();

        DBMItemGroup dbmItemGroup = new DBMItemGroup();
        MItemGroup mItemGroup = new MItemGroup();
        DataTable dt = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtGV = new DataTable();
        long ID;
        bool flag;
        string strcondtion;
        private void HSNCode_Load(object sender, EventArgs e)
        {
            try
            {
              
                txtHSN_Code.Text = "";
                txtpkItemNo.Text = "0";
                txtpkItemNo.Enabled = false;
                txtItemName.Enabled = false;
                txtHSN_Code.Enabled = false;
                //FillList();
                panelShow.Visible = true;
                panelBtn.Visible = true;
                btnCancel.Enabled = false;
                btnOK.Enabled = false;
                BtnOkGetdata.Enabled = false;
                btnUpdate.Focus();
            }
            catch (Exception exe)
            {
                ObjFunction.ExceptionDisplay(exe.Message);
            }

        }

        public HSNCode()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BindGrid()
        {
            try
            {
                
                string SqlQuery2 = "";
                //   SqlQuery2 = "SELECT 0 as 'SrNo', MItemMaster.ItemNo,MItemMaster.ItemName, MItemMaster.GroupNo, MItemGroup.ItemGroupName, MItemMaster.HSNCode FROM  MItemMaster INNER JOIN  MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo WHERE(MItemMaster.IsActive = 'true')ORDER BY ItemGroupName";
                //    " WHERE(MItemMaster.IsActive = 'true') and GroupNo="+txtpkItemNo.Text+" ORDER BY ItemGroupName";
                SqlQuery2 = "SELECT 0 as 'SrNo',MItemGroup.ItemGroupName, MItemMaster.ItemName, MItemMaster.HSNCode, MItemMaster.GroupNo, MItemMaster.ItemNo FROM  MItemMaster INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo " +
                " Where " + strcondtion;
            
                GvInfo.Rows.Clear();
                dt = ObjFunction.GetDataView(SqlQuery2).Table;
                for (int i = 0; i < dt.Rows.Count; i++) 
                {
                    GvInfo.Rows.Add();
                    for (int j = 0; j < GvInfo.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            GvInfo.Rows[i].Cells[j].Value = i + 1;
                        }
                        else
                        {
                            GvInfo.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                        }
                    }
                }

                
                GvInfo.Columns[0].ReadOnly = true;
                GvInfo.Columns[1].ReadOnly = true;
                GvInfo.Columns[2].ReadOnly = true;
                GvInfo.Columns[4].ReadOnly = true;
                GvInfo.Columns[5].ReadOnly = true;
                //GvInfo.Columns[5].ReadOnly = true;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        //private void BindGridUpdate()
        //{
        //    try
        //    {
        //        string SqlQuery = "";
        //        SqlQuery = "SELECT 0 as 'SrNo', MItemGroup.ItemGroupName, MItemMaster.ItemName, MItemMaster.HSNCode , MItemMaster.GroupNo,  MItemMaster.ItemNo FROM  MItemMaster INNER JOIN  MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo " + //WHERE(MItemMaster.IsActive = 'true')ORDER BY ItemGroupName" +
        //        " WHERE(MItemMaster.IsActive = 'true') and GroupNo=" + txtpkItemNo.Text + " and " + strcondtion + " ORDER BY ItemGroupName";

        //        GvInfo.Rows.Clear();
        //        dt = ObjFunction.GetDataView(SqlQuery).Table;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            GvInfo.Rows.Add();
        //            for (int j = 0; j < GvInfo.Columns.Count; j++)
        //            {
        //                if (j == 0)
        //                {
        //                    GvInfo.Rows[i].Cells[j].Value = i + 1;
        //                }
        //                else
        //                {
        //                    GvInfo.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
        //                }
        //            }
        //        }


        //        GvInfo.Columns[0].ReadOnly = true;
        //        GvInfo.Columns[1].ReadOnly = true;
        //        GvInfo.Columns[2].ReadOnly = true;
        //        GvInfo.Columns[4].ReadOnly = true;
        //        GvInfo.Columns[5].ReadOnly = true;
        //        //GvInfo.Columns[5].ReadOnly = true;

        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}
        private void BindGridUpdate()
        {
            try
            {

                string SqlQuery = "";
                SqlQuery = "SELECT 0 as 'SrNo', MItemGroup.ItemGroupName, MItemMaster.ItemName, MItemMaster.HSNCode , MItemMaster.GroupNo,  MItemMaster.ItemNo FROM  MItemMaster INNER JOIN  MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo " + //WHERE(MItemMaster.IsActive = 'true')ORDER BY ItemGroupName" +
                " WHERE(MItemMaster.IsActive = 'true') and GroupNo=" + txtpkItemNo.Text + " ORDER BY ItemGroupName";
                //SqlQuery2 = "SELECT 0 as 'SrNo', MItemMaster.ItemNo,MItemGroup.ItemGroupName,MItemMaster.ItemName, MItemMaster.GroupNo,  MItemMaster.HSNCode FROM  MItemMaster INNER JOIN MItemGroup ON MItemMaster.GroupNo = MItemGroup.ItemGroupNo " +
                //" Where " + strcondtion;

                GvInfo.Rows.Clear();
                dt = ObjFunction.GetDataView(SqlQuery).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GvInfo.Rows.Add();
                    for (int j = 0; j < GvInfo.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            GvInfo.Rows[i].Cells[j].Value = i + 1;
                        }
                        else
                        {
                            GvInfo.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                        }
                    }
                }


                GvInfo.Columns[0].ReadOnly = true;
                GvInfo.Columns[1].ReadOnly = true;
                GvInfo.Columns[2].ReadOnly = true;
                GvInfo.Columns[4].ReadOnly = true;
                GvInfo.Columns[5].ReadOnly = true;
                //GvInfo.Columns[5].ReadOnly = true;

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillList()

        {
//            strcondtion = "Select disitnct groupno from itemmaster where ";
            ObjFunction.FillList(lstItemName, "SELECT   MItemGroup.ItemGroupNo, MItemGroup.ItemGroupName FROM    MItemGroup where  " + strcondtion + " and MItemGroup.IsActive='true' ORDER BY ItemGroupName");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            btnCancel.Enabled = false;
            btnOK.Enabled = false;
            txtpkItemNo.Text = "0";
            txtHSN_Code.Text = "";
            txtItemName.Text = "";
            GvInfo.Rows.Clear();
            btnUpdate.Focus();
        }

        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
             
                btnUpdate.Enabled = false;
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtItemName.Text == "")
                    {
                        FillList();

                        pnlItemName.Visible = true;
                        lstItemName.Focus();
                        BtnOkGetdata.Enabled = true;
                    }
                    else
                    {
                        pnlItemName.Visible = false;
                        txtHSN_Code.Enabled = true;
                       // strcondtion = " ";
                        BindGridUpdate();
                        txtHSN_Code.Focus();
                    }
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    txtItemName.Text = "";
                }
                else
                {
                    e.KeyChar = Convert.ToChar(0);
                }
            }
            catch (Exception exe1)
            {
                ObjFunction.ExceptionDisplay(exe1.Message);
            }
        }


        private void lstItemName_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
               // txtItemName.Text = lstItemName.Text;
                pnlItemName.Visible = false;
                BtnOkGetdata.Enabled = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }



       

        private void txtHSN_Code_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtHSN_Code.Text == "")
                {
                    txtHSN_Code.Focus();
                }
                else
                {
                    if (txtHSN_Code.Text.Length == 2 || txtHSN_Code.Text.Length == 4 || txtHSN_Code.Text.Length == 6 || txtHSN_Code.Text.Length == 8 || txtHSN_Code.Text.Length == 10)
                    {
                        
                        BtnOkGetdata.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show("HSN Code lenght should be 2,4,6,8 or 10....pls enter correct HSN code.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtHSN_Code.Text = "";
                        txtHSN_Code.Focus();
                    }
                }
            }

        }
      
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtHSN_Code.Text != " ")
                {
                    rdNoHSNCode.Enabled = false;
                    rdAll.Enabled = false;
                    rdHSNCode.Enabled = false;
                    btnUpdate.Enabled = true;
                    GvInfo.Rows.Clear();
                    dbMItemMaster = new DBMItemMaster();
                    mItemMaster = new MItemMaster();

                    mItemGroup.ItemGroupNo = Convert.ToInt32(txtpkItemNo.Text);
                    mItemMaster.HSNCode = Convert.ToString(txtHSN_Code.Text);
                    mItemMaster.ItemName = Convert.ToString(txtItemName.Text);

                    // string str = "";

                    ObjTrans.ExecuteQuery("Update  MItemMaster set HSNCode='" + txtHSN_Code.Text.Trim() + "' where GroupNo='" + txtpkItemNo.Text.Trim() + "' ", CommonFunctions.ConStr);
                    OMMessageBox.Show("HSN Code Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    //strcondtion = " ";
                    BindGridUpdate();
                    btnOK.Enabled = false;
                    btnCancel.Enabled = false;

                    txtpkItemNo.Text = "0";
                    txtHSN_Code.Text = "";
                    txtItemName.Text = "";
                    GvInfo.Rows.Clear();

                    btnUpdate.Enabled = true;
                    btnUpdate.Focus();
                }
                else
                {
                    OMMessageBox.Show("HSN Code Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    btnOK.Enabled = false;
                    btnCancel.Enabled = false;
                    btnUpdate.Enabled = true;

                    txtpkItemNo.Text = "0";
                    txtHSN_Code.Text = "";
                    txtItemName.Text = "";
                    GvInfo.Rows.Clear();
                    btnUpdate.Enabled = true;
                    btnUpdate.Focus();
                }
            }
            catch (Exception exe)
            {
                ObjFunction.ExceptionDisplay(exe.Message);
            }
        }


        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemGroupName = 1;
            public static int ItemName = 2;
            public static int HSNCode = 3;
            public static int GroupNo = 4;
            public static int ItemNo = 5;
        }

        #endregion
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            rdNoHSNCode.Enabled = true;
            rdAll.Enabled = true;
            rdHSNCode.Enabled = true;

            btnUpdate.Enabled = false;
            btnCancel.Enabled = true;
            btnOK.Enabled = true;
            txtItemName.Enabled = true;
            txtItemName.Focus();
            strcondtion = " ";
            rdNoHSNCode.Focus();

            //BindGridUpdate();
        }

        private void lstItemName_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtItemName.Text = lstItemName.Text;
                    txtHSN_Code.Enabled = true;
                    txtHSN_Code.Focus();
                    
                   // pnlItemName.Visible = false;
                    //txtItemName.Text = lstItemName.Text;
                    pnlItemName.Visible = false;
                    var a = lstItemName.SelectedValue.ToString();
                    txtpkItemNo.Text = a;
                    //                    strcondtion = " ";
                    btnOK.Enabled = true;
                    btnCancel.Enabled = true;

                    BindGridUpdate();
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GvInfo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dbMItemMaster = new DBMItemMaster();
                mItemMaster = new MItemMaster();
                txtHSN_Code.Text = " "; 
                int i = GvInfo.CurrentRow.Index;
                string j = Convert.ToString(GvInfo.CurrentRow.Cells[ColIndex.HSNCode].Value);//.ToString();
                int k = Convert.ToInt32(GvInfo.CurrentRow.Cells[ColIndex.ItemNo].Value);

                mItemGroup.ItemGroupNo = Convert.ToInt32(txtpkItemNo.Text);
                string val = Convert.ToString(GvInfo.CurrentRow.Cells[ColIndex.HSNCode].Value);
                if (val !="")
                {

                    mItemGroup.ItemGroupNo = Convert.ToInt32(txtpkItemNo.Text);
                    ObjTrans.ExecuteQuery("Update  MItemMaster set HSNCode = '" + val +  "' where itemNo = " + Convert.ToInt32(GvInfo.CurrentRow.Cells[ColIndex.ItemNo].Value) + " ", CommonFunctions.ConStr);
                    btnOK.Enabled = true;
                    btnOK.Focus();
                }
            }
            catch (Exception e1)
            {
                ObjFunction.ExceptionDisplay(e1.Message);
            }
        }

        private void BtnOkGetdata_Click(object sender, EventArgs e)
        {
            if (txtHSN_Code.Text != " ")
            {
                for (int i = 0; i < GvInfo.Rows.Count; i++)
                {
                    for (int j = 0; j < GvInfo.Columns.Count; j++)
                    {
                        if (j == 3)
                        {
                            GvInfo.Rows[i].Cells[3].Value = txtHSN_Code.Text;
                        }
                    }
                }           
            }
            btnOK.Focus();
        }

        private void rdHSNCode_CheckedChanged(object sender, EventArgs e)
        {
            strcondtion = "(MItemMaster.IsActive = 'true' and MItemMaster.HSNCode not like '0%' and  GroupNo=" + txtpkItemNo.Text + ") ORDER BY ItemGroupName";

            //            BindGrid();
        }

        private void rdNoHSNCode_CheckedChanged(object sender, EventArgs e)
        {
//            strcondtion = "   MItemMaster.HSNCode = ' ' or MItemMaster.HSNCode = '0' or MItemMaster.HSNCode = null) and GroupNo=" + txtpkItemNo.Text + " ORDER BY ItemGroupName";

            strcondtion = "  itemgroupno in ( select groupno from mitemmaster where  MItemMaster.HSNCode = ' ' or MItemMaster.HSNCode = '0' or MItemMaster.HSNCode = null ) ";
            //          BindGrid();
//            FillList();
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            strcondtion = "(MItemMaster.IsActive = 'true' and  GroupNo=" + txtpkItemNo.Text + ")  ORDER BY ItemGroupName ";
    //        BindGrid();

        }

        private void rdNoHSNCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtItemName.Focus();
            }

        }

        private void rdHSNCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtItemName.Focus();
            }

        }

        private void rdAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtItemName.Focus();
            }

        }
    }
    

}
