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
    public partial class ProductionAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMRecipeMain dbMRecipeMain = new DBMRecipeMain();
        MRecipeMain RecipeMain = new MRecipeMain();
        MRecipeSub RecipeSub = new MRecipeSub();
        long vouchertypecode;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long AreaNo, ID, RecipeType;
        long No = 0;
        DataTable dt = new DataTable();

        long RecipeID = 0;

        public ProductionAE()
        {
            InitializeComponent();
        }

        private void ProductionAE_Load(object sender, EventArgs e)
        {
            try
            {
                if (DBGetVal.KachhaFirm == false)
                {

                    vouchertypecode = 0;
                }
                else
                {
                    vouchertypecode = 1;
                }
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                FormatPicture();
                FillList();

                ID = ObjQry.ReturnLong("Select max(MRecipeID) from MRecipeMain Where RecipeType=2 and MRecipeMain.ESFlag="+ vouchertypecode, CommonFunctions.ConStr);
                if (ID != 0)
                {
                    FillControls();
                }
                else {
                    btnUpdate.Visible = false;
                }
                if (dgProduction.Rows.Count > 0)
                {
                    for (int i = 0; i < dgProduction.Rows.Count; i++)
                    {
                        dgProduction.Rows[i].DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Regular);
                    }
                    foreach (DataGridViewColumn col in dgProduction.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

                btnDelete.Enabled = false;
                SetNavigation();
                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void setDisable(bool flag)
        {
            BtnSave.Visible = !flag;
            btnCancel.Visible = !flag;
            btnUpdate.Visible = flag;
            btnSearch.Visible = flag;
            btnExit.Visible = flag;
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btnDelete.Visible = flag;
        }

        private void FillControls()
        {
            try
            {
               
                RecipeMain = dbMRecipeMain.ModifyMRecipeMainByID(ID);
                txtDocNo.Text = RecipeMain.DocNo.ToString();
                dtpDate.Text = RecipeMain.RDate.ToString();
                lstItemGroup.SelectedValue = RecipeMain.GroupNo;
                lstSubGroup.SelectedValue = RecipeMain.FinishItemID;
                txtItemGroup1.Text = lstItemGroup.Text;
                txtSubGroup.Text = lstSubGroup.Text;
                txtQty.Text = RecipeMain.Qty.ToString();
                lstUOMName.SelectedValue = RecipeMain.UomNo;
                txtUomName.Text = lstUOMName.Text;
                txtProdQty.Text = RecipeMain.ProdQty.ToString();
                RecipeID = RecipeMain.FkRecipeID;
                if (DBGetVal.KachhaFirm == false)
                {
                    RecipeMain.ESFlag = false;
                }
                else
                {
                    RecipeMain.ESFlag = true;
                }

                BindGrid();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillList()
        {
            ObjFunction.FillList(lstItemGroup, "SELECT distinct MRecipeMain.GroupNo, MItemGroup.ItemGroupName FROM MItemGroup INNER JOIN MRecipeMain ON MItemGroup.ItemGroupNo = MRecipeMain.GroupNo WHERE MRecipeMain.IsActive = 'True' and RecipeType=1 ORDER BY ItemGroupName");
            ObjFunction.FillList(lstSubGroup, "SELECT MRecipeMain.FinishItemID, MItemMaster.ItemName FROM MItemMaster INNER JOIN MRecipeMain ON MItemMaster.ItemNo = MRecipeMain.FinishItemID WHERE(MRecipeMain.IsActive = 'True') and RecipeType=1  ORDER BY MItemMaster.ItemName");
            ObjFunction.FillList(lstUOMName, "SELECT MRecipeMain.UomNo, MUOM.UOMName FROM MRecipeMain INNER JOIN MUOM ON MRecipeMain.UomNo = MUOM.UOMNo ORDER BY UOMName");
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void BindGrid()
        {
            string sqlQuery = "";

            sqlQuery = " SELECT  0 AS SRNO,MItemGroup.ItemGroupName,MItemMaster.ItemName as PItem, MIGrp.ItemGroupName,MRecipeSub.Qty,ProductQty,MUOM.UOMName, MRecipeSub.WastagePerQty," +
                       " MRecipeSub.FinalQty, MRecipeSub.RawGroupNo, MRecipeSub.RawProductID,MRecipeSub.UOMNo,MRecipeMain.MRecipeID as MProductionID,SRecipeID as SProductionID FROM MRecipeMain INNER JOIN " +
                       " MRecipeSub ON MRecipeMain.MRecipeID = MRecipeSub.FKMRecipeID INNER JOIN MUOM ON MRecipeSub.UomNo = MUOM.UOMNo INNER JOIN " +
                       " MItemMaster ON MRecipeSub.RawProductID = MItemMaster.ItemNo INNER JOIN MItemGroup ON MRecipeSub.RawGroupNo = MItemGroup.ItemGroupNo " +
                       " INNER JOIN MItemGroup as MIGrp on MRecipeMain.GroupNo = MIGrp.ItemGroupNo where MRecipeID =" + ID + " and MRecipeSub.IsActive='true' and MRecipeMain.RecipeType=2";

            dgProduction.Rows.Clear();
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgProduction.Rows.Add();
                for (int j = 0; j < dgProduction.Columns.Count; j++)
                {
                    dgProduction.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
            dgProduction.Rows.Add();
        }

        public void ItemListBindGrid()
        {
            string sqlQuery = "";

            sqlQuery = " SELECT  0 AS SRNO,MItemGroup.ItemGroupName, MItemMaster.ItemName as PItem,MIGrp.ItemGroupName,MRecipeSub.Qty,ProductQty, MUOM.UOMName, MRecipeSub.WastagePerQty," +
                       " MRecipeSub.FinalQty, MRecipeSub.RawGroupNo, MRecipeSub.RawProductID,MRecipeSub.UOMNo,0 as MProductionID,0 as SProductionID  FROM MRecipeMain INNER JOIN " +
                       " MRecipeSub ON MRecipeMain.MRecipeID = MRecipeSub.FKMRecipeID INNER JOIN MUOM ON MRecipeSub.UomNo = MUOM.UOMNo INNER JOIN " +
                       " MItemMaster ON MRecipeSub.RawProductID = MItemMaster.ItemNo INNER JOIN MItemGroup ON MRecipeSub.RawGroupNo = MItemGroup.ItemGroupNo " +
                       " INNER JOIN MItemGroup as MIGrp on MRecipeMain.GroupNo = MIGrp.ItemGroupNo  " +
                       " where MRecipeMain.GroupNo = " + Convert.ToInt64(lstItemGroup.SelectedValue) + " and MRecipeMain.FinishItemID = " + Convert.ToInt64(lstSubGroup.SelectedValue) + " and MRecipeSub.IsActive='true' AND RecipeType=1";


            ID = 0;
            dgProduction.Rows.Clear();
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RecipeID = Convert.ToInt32(dt.Rows[i].ItemArray[12].ToString());
                dgProduction.Rows.Add();
                for (int j = 0; j < dgProduction.Columns.Count; j++)
                {
                    dgProduction.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
            dgProduction.Rows.Add();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtProdQty.Text) <= 0)
                {
                   
                    OMMessageBox.Show("Please enter valid quantity");
                    return;
                }
                if (Validations() == true)
                {
                    HidePics();
                    RecipeMain = new MRecipeMain();
                    RecipeMain.MRecipeID = ID;
                    RecipeMain.DocNo = Convert.ToInt64(txtDocNo.Text);

                    if (DBGetVal.KachhaFirm == false)
                    {
                        RecipeMain.ESFlag = false;
                    }
                    else
                    {
                        RecipeMain.ESFlag = true;
                    }

                    RecipeMain.GroupNo = Convert.ToInt64(lstItemGroup.SelectedValue);
                    RecipeMain.FinishItemID = Convert.ToInt64(lstSubGroup.SelectedValue);
                    RecipeMain.PackingSize = 0;
                    RecipeMain.RDate = Convert.ToDateTime(dtpDate.Text);
                    RecipeMain.Qty = Convert.ToDouble(txtQty.Text);
                    RecipeMain.ProdQty = Convert.ToDouble(txtProdQty.Text);
                    RecipeMain.UomNo = Convert.ToInt64(lstUOMName.SelectedValue);
                    RecipeMain.RecipeType = 2;
                    RecipeMain.FkRecipeID = RecipeID;
                    RecipeMain.IsLock = false;
                    RecipeMain.IsActive = true;
                    RecipeMain.UserID = DBGetVal.UserID;
                    RecipeMain.UserDate = DBGetVal.ServerTime.Date;
                    dbMRecipeMain.AddMRecipeMain(RecipeMain);

                    for (int i = 0; i < dgProduction.Rows.Count; i++)
                    {
                        if ((dgProduction.Rows[i].Cells[ColIndex.MProductionID].Value == null) || (dgProduction.Rows[i].Cells[ColIndex.Group].Value == null))
                        {
                            dgProduction.Rows.RemoveAt(i);
                        }
                    }
                    for (int i = 0; i < dgProduction.Rows.Count; i++)
                    {
                        RecipeSub = new MRecipeSub();
                        long SProductionID = Convert.ToInt64(dgProduction.Rows[i].Cells[ColIndex.SProductionID].Value);
                        RecipeSub.SRecipeID = SProductionID;

                        RecipeSub.RawGroupNo = Convert.ToInt64(dgProduction[ColIndex.RawGroupNo, i].Value);
                        RecipeSub.RawProductID = Convert.ToInt64(dgProduction[ColIndex.RawProductID, i].Value);
                        RecipeSub.Qty = Convert.ToDecimal(dgProduction[ColIndex.Qty, i].Value);
                        RecipeSub.UomNo = Convert.ToInt64(dgProduction[ColIndex.UOMNo, i].Value);
                        RecipeSub.Wastageper = 0;
                        RecipeSub.WastagePerQty = Convert.ToDecimal(dgProduction[ColIndex.WastagePerQty, i].Value);
                        RecipeSub.FinalQty = Convert.ToDecimal(dgProduction[ColIndex.FinalQty, i].Value);
                        RecipeSub.ProductQty = Convert.ToDouble(dgProduction[ColIndex.ProductQty, i].Value);
                        RecipeSub.IsActive = true;
                        dbMRecipeMain.AddMRecipeSub(RecipeSub);

                    }
                    if (dbMRecipeMain.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Production Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            ID = ObjQry.ReturnLong("Select Max(MRecipeID) From MRecipeMain", CommonFunctions.ConStr);
                            RecipeType = ObjQry.ReturnLong("Select RecipeType From MRecipeMain where Max(MRecipeID)=" + ID, CommonFunctions.ConStr);
                            FillControls();
                            NavigationDisplay(2);
                        }
                        else
                        {
                            OMMessageBox.Show("Production Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                        ObjTrans.ExecuteQuery("Exec StockUpdateAll", CommonFunctions.ConStr);
                    }
                    else
                    {
                        OMMessageBox.Show("Production Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {
                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=2 and MRecipeMain.ESFlag=" + vouchertypecode).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["MRecipeID"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=2 and MRecipeMain.ESFlag=" + vouchertypecode).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["MRecipeID"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=2 and MRecipeID >" + ID + " and MRecipeMain.ESFlag=" + vouchertypecode).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["MRecipeID"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
                else if (type == 4)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(MRecipeID),0)as MRecipeID From MRecipeMain where RecipeType=2 and MRecipeID <" + ID + " and MRecipeMain.ESFlag=" + vouchertypecode).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["MRecipeID"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            if (ID != 0)
            {
                FillControls();
            }
        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID)
                {
                    cntRow = i;
                    break;
                }
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }

        #endregion

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
            if (e.KeyCode == Keys.Left && e.Control)
            {
                if (btnPrev.Enabled) btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (btnFirst.Enabled) btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                if (btnNext.Enabled) btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (btnLast.Enabled) btnLast_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }

        }
        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {

                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                ID = 0;
                dgProduction.Enabled = true;
                txtDocNo.Text = (ObjQry.ReturnLong("SELECT isnull(max(DocNo),0) as DocNo FROM MRecipeMain Where RecipeType=2", CommonFunctions.ConStr) + 1).ToString();
                FillList();
                txtDocNo.Enabled = false;
                dtpDate.Value = DateTime.Now.ToUniversalTime();
                dtpReqDate.Value = DateTime.Now.ToUniversalTime();
                txtQty.Text = "0.00";
                txtProdQty.Text = "0.00";
                txtQty.Enabled = false;
                txtUomName.Enabled = false;
                dgProduction.Rows.Clear();
                txtItemGroup1.Focus();
                while (dgProduction.Rows.Count > 0)
                {
                    dgProduction.Rows.RemoveAt(0);
                }
                dgProduction.Rows.Add();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
         
            try
            {
                pnlSearch.Visible = true;
                pnlSearch.Enabled = true;
                rbProductionName.Enabled = true;
                rdDate.Enabled = true;
                dtpSearchDate.Visible = false;
                cmbProductionNameSearch.Enabled = true;
                cmbProductionNameSearch.Visible = true;
                btnNew.Enabled = false;
                btnUpdate.Enabled = false;
                rbProductionName.Checked = true;
       
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void BindProduction()
        {
            if (rbProductionName.Checked)
            {
                cmbProductionNameSearch.Visible = true;
                dtpSearchDate.Visible = false;
                lblLable.Text = "Production Name";
                ObjFunction.FillCombo(cmbProductionNameSearch, "SELECT distinct MRecipeMain.GroupNo,MItemGroup.ItemGroupName AS 'GroupName'FROM MRecipeMain INNER JOIN MItemGroup ON MRecipeMain.GroupNo = MItemGroup.ItemGroupNo INNER JOIN MItemMaster ON MRecipeMain.FinishItemID = MItemMaster.ItemNo WHERE(MRecipeMain.IsActive = 'true') AND (MRecipeMain.RecipeType = 2)");
                dgProductionName.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            else if (rdDate.Checked)
            {
                dtpSearchDate.Enabled = true;
                cmbProductionNameSearch.Visible = false;
                lblLable.Text = "Date :";
                dtpSearchDate.Location = new System.Drawing.Point(65, 39);
                dtpSearchDate.Visible = true;
                dtpSearchDate.Focus();
            }
            else
            {
                DisplayMessage("select Radio Button");
            }

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            NavigationDisplay(2);
            dgProduction.Enabled = false;
            HidePics();
            btnNew.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            dgProduction.Enabled = true;

            txtDocNo.Enabled = false;
            txtItemGroup1.Enabled = false;
            txtSubGroup.Enabled = false;
            txtQty.Enabled = false;
            txtUomName.Enabled = false;

           // dgProduction.Rows.Add();
            dgProduction.CurrentCell = dgProduction[1, dgProduction.Rows.Count - 1];
             txtProdQty.Focus();
        }

        #region TextBox KeyDown 

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemGroup1.Focus();
            }
        }

        private void txtItemGroup1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtItemGroup1.Text == "")
                {
                    pnlItemGroup.Visible = true;
                    lstItemGroup.Focus();
                    lstItemGroup.SelectedIndex = 0;

                }
                else
                {
                    pnlItemGroup.Visible = false;
                    lstItemGroup.Visible = true;
                    lstItemGroup.Focus();
                }
            }
            else
            {
                pnlItemGroup.Visible = true;
                lstItemGroup.Focus();
            }
        }

        private void lstItemGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtItemGroup1.Text = lstItemGroup.Text;
                    pnlItemGroup.Visible = false;

                    ObjFunction.FillList(lstSubGroup, "SELECT MItemMaster.ItemNo, MItemMaster.ItemName FROM MItemGroup INNER JOIN MRecipeMain ON MItemGroup.ItemGroupNo = MRecipeMain.GroupNo INNER JOIN MItemMaster ON MRecipeMain.FinishItemID = MItemMaster.ItemNo WHERE(MItemMaster.IsActive = 'True') AND ItemGroupNo = " + Convert.ToInt64(lstItemGroup.SelectedValue) + "  and RecipeType=1  ORDER BY MItemMaster.ItemName");
                    txtSubGroup.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (txtItemGroup1.Text == "")
                    {
                        pnlItemGroup.Visible = true;
                        lstItemGroup.Focus();
                        lstItemGroup.SelectedIndex = 0;
                    }
                    else
                    {
                        pnlItemGroup.Visible = false;
                        lstSubGroup.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtSubGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtSubGroup.Text == "")
                {
                    pnlSubGroup.Visible = true;
                    lstSubGroup.Focus();
                    lstSubGroup.SelectedIndex = 0;

                }
                else
                {
                    pnlSubGroup.Visible = false;
                    lstSubGroup.Visible = true;
                    lstSubGroup.Focus();
                }
            }
            else
            {
                pnlSubGroup.Visible = true;
                lstSubGroup.Focus();
            }
        }

        private void lstSubGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtSubGroup.Text = lstSubGroup.Text;
                    pnlSubGroup.Visible = false;
                    ItemListBindGrid();
                    double Qty = ObjQry.ReturnLong(" SELECT Qty FROM MRecipeMain where MRecipeMain.GroupNo = " + Convert.ToInt64(lstItemGroup.SelectedValue) + " and MRecipeMain.FinishItemID = " + Convert.ToInt64(lstSubGroup.SelectedValue) + " ", CommonFunctions.ConStr);
                    txtQty.Text = Qty.ToString();

                    ObjFunction.FillList(lstUOMName, "SELECT MRecipeMain.UomNo,UOMName FROM  MUOM INNER JOIN MRecipeMain ON MUOM.UOMNo = MRecipeMain.UomNo where GroupNo = " + Convert.ToInt64(lstItemGroup.SelectedValue) + "  and FinishItemID = " + Convert.ToInt64(lstSubGroup.SelectedValue) + " ");
                    txtUomName.Text = lstUOMName.Text;
                    dtpReqDate.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (txtSubGroup.Text == "")
                    {
                        pnlSubGroup.Visible = true;
                        lstSubGroup.Focus();
                        lstSubGroup.SelectedIndex = 0;
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        pnlSubGroup.Visible = false;
                        dtpReqDate.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            //    if (txtQty.Text != "0.00")
            //    {
            //        txtUomName.Focus();
            //    }
            //    else
            //    {
            //        txtQty.Focus();
            //    }
            //}

        }

        private void txtUomName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUomName.Text == "")
                {
                    pnlUOMName.Visible = true;
                    lstUOMName.Focus();
                    lstUOMName.SelectedIndex = 0;

                }
                else
                {
                    pnlUOMName.Visible = false;
                    lstUOMName.Visible = true;
                    lstUOMName.Focus();
                }
            }
            else
            {
                pnlUOMName.Visible = true;
                lstUOMName.Focus();
            }
        }

        private void lstUOMName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtUomName.Text = lstUOMName.Text;
                    pnlUOMName.Visible = false;
                    dtpReqDate.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (txtUomName.Text == "")
                    {
                        pnlUOMName.Visible = true;
                        lstUOMName.Focus();
                        lstUOMName.SelectedIndex = 0;
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        pnlUOMName.Visible = false;
                        dtpReqDate.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstItemGroup_Leave(object sender, EventArgs e)
        {
            pnlItemGroup.Visible = false;
        }

        private void lstSubGroup_Leave(object sender, EventArgs e)
        {
            pnlSubGroup.Visible = false;
        }

        private void lstUOMName_Leave(object sender, EventArgs e)
        {
            pnlUOMName.Visible = false;
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 20, OMFunctions.MaskedType.NotNegative);
        }

        private void dtpReqDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtProdQty.Focus();
            }
        }

        private void txtProdQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double FACTOR = 0.0;
                double Qty = 0.0;
                FACTOR = Convert.ToDouble(txtProdQty.Text) / (Convert.ToDouble(txtQty.Text));
                for (int i = 0; i < dgProduction.Rows.Count - 1; i++)
                {
                    Qty = 0.0;
                    // ProdQty = (Convert.ToDouble(txtQty.Text) * Convert.ToDouble(txtProdQty.Text)) / 100;
                    Qty = Convert.ToDouble(dgProduction.Rows[i].Cells[ColIndex.Qty].Value) * FACTOR;
                    dgProduction.Rows[i].Cells[ColIndex.ProductQty].Value = Qty.ToString("0.00");
                    dgProduction.Rows[i].Cells[ColIndex.FinalQty].Value =( Convert.ToDouble(dgProduction.Rows[i].Cells[ColIndex.ProductQty].Value) + Convert.ToDouble(dgProduction.Rows[i].Cells[ColIndex.WastagePerQty].Value)).ToString("0.00");

                }
                BtnSave.Focus();
            }
        }

        private void txtProdQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 20, OMFunctions.MaskedType.NotNegative);
        }

        private void txtQty2_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 20, OMFunctions.MaskedType.NotNegative);
        }

        private void PnlD1_Click(object sender, EventArgs e)
        {
            if (pnlItemGroup.Visible == true)
            {
                pnlItemGroup.Visible = false;
                txtItemGroup1.Focus();
            }
            else if (pnlSubGroup.Visible == true)
            {
                pnlSubGroup.Visible = false;
                txtSubGroup.Focus();
            }
            else if (pnlUOMName.Visible == true)
            {
                pnlUOMName.Visible = false;
                txtUomName.Focus();
            }

        }

        private void PnlD2_Click(object sender, EventArgs e)
        {
            if (pnlItemGroup.Visible == true)
            {
                pnlItemGroup.Visible = false;
                txtItemGroup1.Focus();
            }
            else if (pnlSubGroup.Visible == true)
            {
                pnlSubGroup.Visible = false;
                txtSubGroup.Focus();
            }
            else if (pnlUOMName.Visible == true)
            {
                pnlUOMName.Visible = false;
                txtUomName.Focus();
            }

        }

        private void pnlMain_Click(object sender, EventArgs e)
        {
            if (pnlItemGroup.Visible == true)
            {
                pnlItemGroup.Visible = false;
                txtItemGroup1.Focus();
            }
            else if (pnlSubGroup.Visible == true)
            {
                pnlSubGroup.Visible = false;
                txtSubGroup.Focus();
            }
            else if (pnlUOMName.Visible == true)
            {
                pnlUOMName.Visible = false;
                txtUomName.Focus();
            }

        }

        public void Clear()
        {
            txtItemGroup1.Text = "";
            txtSubGroup.Text = "";
            txtProdQty.Text = "0.00";
            txtUomName.Text = "";
            txtQty.Text = "0.00";
            HidePics();
        }

        private void HidePics()
        {
            pnlItemGroup.Visible = false;
            pnlSubGroup.Visible = false;
            pnlUOMName.Visible = false;

        }

        private void FormatPicture()
        {
            pnlItemGroup.Top = txtItemGroup1.Bottom + 20;
            pnlItemGroup.Width = txtItemGroup1.Width;
            pnlItemGroup.Height = 100;
            lstItemGroup.Top = pnlItemGroup.Top - 50;
            lstItemGroup.Height = pnlItemGroup.Height - 5;

            pnlSubGroup.Top = txtSubGroup.Bottom + 20;
            pnlSubGroup.Width = txtSubGroup.Width;
            pnlSubGroup.Height = 100;
            lstSubGroup.Top = pnlSubGroup.Top - 50;
            lstSubGroup.Height = pnlSubGroup.Height - 5;

            pnlUOMName.Top = txtUomName.Bottom + 20;
            pnlUOMName.Width = txtUomName.Width;
            pnlUOMName.Height = 80;
            lstUOMName.Top = pnlUOMName.Top - 80;
            lstUOMName.Height = pnlUOMName.Height - 5;

        }

        #endregion

        public bool Validations()
        {
            bool flag = false;

            if (txtItemGroup1.Text.Trim() == "")
            {
                txtItemGroup1.Focus();
            }
            else if (txtSubGroup.Text.Trim() == "")
            {
                txtSubGroup.Focus();
            }
            else if ((txtProdQty.Text.Trim() == "") && (Convert.ToDouble(txtProdQty.Text) != 0))
            {
                txtProdQty.Focus();
                OMMessageBox.Show("Please Enter Production Quantity");
            }
            else if (txtUomName.Text == "")
            {
                txtUomName.Focus();
                OMMessageBox.Show("Select valid uom name");
            }
            else
                flag = true;
            return flag;
        }

        private void dgRecipe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dgRecipe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancelSearch_Click(object sender, EventArgs e)
        {
            pnlSearch.Visible = false;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void cmbProductionNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string str = "";

                    str = "SELECT MRecipeMain.MRecipeID, MItemGroup.ItemGroupName AS 'GroupName', MRecipeMain.DocNo, MRecipeMain.UserDate" +
                          " FROM MRecipeMain INNER JOIN MItemGroup ON MRecipeMain.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                          " MItemMaster ON MRecipeMain.FinishItemID = MItemMaster.ItemNo " +
                          "WHERE(MRecipeMain.IsActive = 'true') AND(MRecipeMain.RecipeType = 2) and MRecipeMain.ESFlag= " + vouchertypecode +" and MRecipeMain.GroupNo=" + ObjFunction.GetComboValue(cmbProductionNameSearch) + " ";

                    dgProductionName.DataSource = ObjFunction.GetDataView(str).Table.DefaultView;
                    dgProductionName.Columns[0].Visible = false;
                    dgProductionName.Columns[1].Width = 150;
                    dgProductionName.Columns[2].Width = 80;
                    dgProductionName.Columns[3].Width = 110;

                    if (dgProductionName.RowCount > 0)
                    {
                        dgProductionName.Visible = true;

                        pnlSearch.Visible = false;

                        dgProductionName.Focus();

                    }
                    else
                    {
                        dgProductionName.Visible = false;

                        DisplayMessage("Production Name Not Found");
                        pnlSearch.Visible = true;
                        rbProductionName.Focus();

                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgProductionName_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long tempNo;
                    e.SuppressKeyPress = true;
                    tempNo = ObjQry.ReturnLong("Select MRecipeID From MRecipeMain Where MRecipeID=" + Convert.ToInt64(dgProductionName.Rows[dgProductionName.CurrentRow.Index].Cells[0].Value) + " and  RecipeType=2 ", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        FillControls();
                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnNew.Focus();
                        dgProductionName.Visible = false;

                    }
                    else
                    {
                        cmbProductionNameSearch.SelectedIndex = 0;
                        DisplayMessage("Bill Not Found");
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    pnlSearch.Visible = true;
                }

                cmbProductionNameSearch.SelectedIndex = 0;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        private void rbProductionName_CheckedChanged(object sender, EventArgs e)
        {
            BindProduction();
        }

        private void rdDate_CheckedChanged(object sender, EventArgs e)
        {
            BindProduction();
        }

        private void dtpSearchDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string str = "";

                    str = "SELECT MRecipeMain.MRecipeID, MItemGroup.ItemGroupName AS 'GroupName', MRecipeMain.DocNo, MRecipeMain.UserDate" +
                          " FROM MRecipeMain INNER JOIN MItemGroup ON MRecipeMain.GroupNo = MItemGroup.ItemGroupNo INNER JOIN " +
                          " MItemMaster ON MRecipeMain.FinishItemID = MItemMaster.ItemNo " +
                          "WHERE(MRecipeMain.IsActive = 'true') AND(MRecipeMain.RecipeType = 2) and MRecipeMain.UserDate='" + dtpSearchDate.Text + "' ";

                    dgInvSearch.DataSource = ObjFunction.GetDataView(str).Table.DefaultView;
                    dgInvSearch.Columns[0].Visible = false;
                    dgInvSearch.Columns[1].Width = 150;
                    dgInvSearch.Columns[2].Width = 80;
                    dgInvSearch.Columns[3].Width = 110;

                    if (dgInvSearch.RowCount > 0)
                    {
                        dgInvSearch.Visible = true;
                        pnlSearch.Visible = false;
                        dgInvSearch.Focus();
                    }
                    else
                    {
                        dgInvSearch.Visible = false;
                        DisplayMessage("Production Name Not Found");
                        pnlSearch.Visible = true;
                        rdDate.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgInvSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    long tempNo;
                    e.SuppressKeyPress = true;
                    tempNo = ObjQry.ReturnLong("Select MRecipeID From MRecipeMain Where MRecipeID=" + Convert.ToInt64(dgInvSearch.Rows[dgInvSearch.CurrentRow.Index].Cells[0].Value) + " and  RecipeType=2 ", CommonFunctions.ConStr);
                    if (tempNo > 0)
                    {
                        ID = tempNo;
                        FillControls();
                        btnNew.Enabled = true;
                        btnUpdate.Enabled = true;
                        btnNew.Focus();
                        dgInvSearch.Visible = false;
                    }
                    else
                    {
                        DisplayMessage("Bill Not Found");
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;

                    pnlSearch.Visible = true;


                }
                
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void dgRecipe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                long SRecipeID = Convert.ToInt64(dgProduction.CurrentRow.Cells[ColIndex.SProductionID].Value);

                if (dgProduction.Rows.Count - 1 == dgProduction.CurrentCell.RowIndex)
                {
                    dgProduction.Rows.RemoveAt(dgProduction.CurrentCell.RowIndex);
                    dgProduction.Rows.Add();
                }
                else
                    dgProduction.Rows.RemoveAt(dgProduction.CurrentCell.RowIndex);

                ObjTrans.ExecuteQuery("Update MRecipeSub set IsActive='False' where SRecipeID=" + SRecipeID + "", CommonFunctions.ConStr);
                dtpReqDate.Focus();
            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int Group = 1;
            public static int PItem = 2;
            public static int Item = 3;
            public static int Qty = 4;
            public static int ProductQty = 5;
            public static int UOMName = 6;
            public static int WastagePerQty = 7;
            public static int FinalQty = 8;
            public static int RawGroupNo = 9;
            public static int RawProductID = 10;
            public static int UOMNo = 11;
            public static int MProductionID = 12;
            public static int SProductionID = 13;
        }
        #endregion
    }
}
