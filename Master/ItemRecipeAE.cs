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
    public partial class ItemRecipeAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMRecipeMain dbMRecipeMain = new DBMRecipeMain();
        MRecipeMain RecipeMain = new MRecipeMain();
        MRecipeSub RecipeSub = new MRecipeSub();

        DataTable dtSearch = new DataTable();
        DataTable dtDelete = new DataTable();
        int cntRow;
        public long AreaNo, ID, RecipeType;
        long No = 0;
        DataTable dt = new DataTable();
        bool DGRecipeFlag = false;
        bool GridFlag = false;
        string ItemGroupNm;

        public ItemRecipeAE()
        {
            InitializeComponent();
        }

        private void RecipeAE_Load(object sender, EventArgs e)
        {
            try
            {
                GridFlag = true;
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                FormatPicture();
                FillList();
                ItemGroupNm = "";
                btnNew.Focus();
                if (ItemRecipe.RequestRecipeNo == 0)
                {
                    ID = ObjQry.ReturnLong("Select max(MRecipeID) from MRecipeMain Where RecipeType=1", CommonFunctions.ConStr);
                }
                else
                {
                    ID = ItemRecipe.RequestRecipeNo;
                }
                if (ID != 0)
                    FillControls();

                if (dgRecipe.Rows.Count > 0)
                {
                    for (int i = 0; i < dgRecipe.Rows.Count; i++)
                    {
                        dgRecipe.Rows[i].DefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Regular);
                    }
                    foreach (DataGridViewColumn col in dgRecipe.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }

                PnlD2.Visible = false;
                dgRecipe.Top = 87;
                dgRecipe.Height = 425;

                btnDelete.Enabled = false;

                SetNavigation();
                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);
                InitalTable();
                dtDelete.Rows.Clear();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillControls()
        {
            try
            {
                dgRecipe.ReadOnly = true;
                RecipeMain = dbMRecipeMain.ModifyMRecipeMainByID(ID);
                txtDocNo.Text = RecipeMain.DocNo.ToString();
                dtpDate.Text = RecipeMain.RDate.ToString();
                lstProductionGroup.SelectedValue = RecipeMain.GroupNo;
                ObjFunction.FillList(lstProductionItem, "SELECT MItemMaster.ItemNo, MItemMaster.ItemName FROM  MItemGroup INNER JOIN MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo WHERE(MItemMaster.IsActive = 'True') AND ItemGroupNo = " + RecipeMain.GroupNo + " ORDER BY MItemMaster.ItemName");
                lstProductionItem.SelectedValue = RecipeMain.FinishItemID;
                txtProductionGroup.Text = lstProductionGroup.Text;
                ItemGroupNm = txtProductionGroup.Text;
                txtProductionItems.Text = lstProductionItem.Text;
                txtRecipeQty.Text = RecipeMain.Qty.ToString();
                ObjFunction.FillList(lstUOMName, "select distinct UOMH as UOMNo,UOMName from(SELECT UOMH, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOMH = MUOM.UOMNo where ItemNo = " + RecipeMain.FinishItemID + " Union All SELECT UOML, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOML = MUOM.UOMNo where ItemNo =  " + RecipeMain.FinishItemID + ") as tbl");

                lstUOMName.SelectedValue = RecipeMain.UomNo;
                txtUomName.Text = lstUOMName.Text;
                BindGrid();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillList()
        {
            ObjFunction.FillList(lstProductionGroup, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3  and ItemGroupNo in(select groupno from MItemMaster where IsActive = 'True' ) ORDER BY ItemGroupName");
            ObjFunction.FillList(lstProductionItem, "SELECT MItemMaster.ItemNo, MItemMaster.ItemName FROM  MItemGroup INNER JOIN MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo WHERE(MItemMaster.IsActive = 'True') ORDER BY MItemMaster.ItemName");
            ObjFunction.FillList(lstUOMName, "SELECT UOMNo,UOMName from MUOM WHERE  isActive='True' ORDER BY UOMName");
            ObjFunction.FillList(lstUOM2, "SELECT UOMNo,UOMName from MUOM WHERE  isActive='True'  ORDER BY UOMName");

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void BindGrid()
        {
            string sqlQuery = "";

            sqlQuery = " SELECT  0 AS SRNO,MItemGroup.ItemGroupName,MItemMaster.ItemName as PItem, MIGrp.ItemGroupName,MRecipeSub.Qty, MUOM.UOMName, MRecipeSub.WastagePerQty," +
                       " MRecipeSub.FinalQty, MRecipeSub.RawGroupNo, MRecipeSub.RawProductID,MRecipeSub.UOMNo,MRecipeID,SRecipeID FROM MRecipeMain INNER JOIN " +
                       " MRecipeSub ON MRecipeMain.MRecipeID = MRecipeSub.FKMRecipeID INNER JOIN MUOM ON MRecipeSub.UomNo = MUOM.UOMNo INNER JOIN " +
                       " MItemMaster ON MRecipeSub.RawProductID = MItemMaster.ItemNo INNER JOIN MItemGroup ON MRecipeSub.RawGroupNo = MItemGroup.ItemGroupNo " +
                       " INNER JOIN MItemGroup as MIGrp on MRecipeMain.GroupNo = MIGrp.ItemGroupNo where MRecipeID =" + ID + " and MRecipeSub.IsActive='true' and MRecipeMain.RecipeType=1";

            dgRecipe.Rows.Clear();
            dt = ObjFunction.GetDataView(sqlQuery).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgRecipe.Rows.Add();
                for (int j = 0; j < dgRecipe.Columns.Count; j++)
                {

                    dgRecipe.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
            dgRecipe.Rows.Add();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgRecipe.Rows.Count <= 1)
                {
                    OMMessageBox.Show("Atleast one item required.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);

                    txtProductionGroup.Focus();
                    return;
                }
                if (Validations() == true)
                {
                    RecipeMain = new MRecipeMain();
                    RecipeSub = new MRecipeSub();
                    if (dtDelete.Rows != null)//for temp detete entries
                    {
                        // RecipeSub = new MRecipeSub();
                        for (int i = 0; i < dtDelete.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 0)
                            {
                                RecipeSub.SRecipeID = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                                dbMRecipeMain.UpdateMRecipeSub(RecipeSub);
                            }
                        }
                    }
                    HidePics();
                    //RecipeMain = new MRecipeMain();
                    RecipeMain.MRecipeID = ID;
                    RecipeMain.DocNo = Convert.ToInt64(txtDocNo.Text);
                    RecipeMain.ESFlag = false;
                    RecipeMain.GroupNo = Convert.ToInt64(lstProductionGroup.SelectedValue);
                    RecipeMain.FinishItemID = Convert.ToInt64(lstProductionItem.SelectedValue);
                    RecipeMain.PackingSize = 0;
                    RecipeMain.RDate = Convert.ToDateTime(dtpDate.Text);
                    RecipeMain.Qty = Convert.ToDouble(txtRecipeQty.Text);
                    RecipeMain.UomNo = Convert.ToInt64(lstUOMName.SelectedValue);
                    RecipeMain.RecipeType = 1;
                    RecipeMain.ProdQty = 0;

                    RecipeMain.FkRecipeID = 0;
                    RecipeMain.IsLock = false;

                    RecipeMain.IsActive = true;
                    RecipeMain.UserID = DBGetVal.UserID;
                    RecipeMain.UserDate = DBGetVal.ServerTime.Date;
                    dbMRecipeMain.AddMRecipeMain(RecipeMain);

                    for (int i = 0; i < dgRecipe.Rows.Count - 1; i++)
                    {
                        //  RecipeSub = new MRecipeSub();

                        if (ID == 0)
                        {
                            RecipeSub.SRecipeID = 0;
                        }
                        else
                        {
                            RecipeSub.SRecipeID = Convert.ToInt64(dgRecipe.Rows[i].Cells[ColIndex.SRecipeID].Value);
                        }
                        //RecipeSub.FKMRecipeID = 0;
                        RecipeSub.RawGroupNo = Convert.ToInt64(dgRecipe[ColIndex.RawGroupNo, i].Value);
                        RecipeSub.RawProductID = Convert.ToInt64(dgRecipe[ColIndex.RawProductID, i].Value);
                        RecipeSub.Qty = Convert.ToDecimal(dgRecipe[ColIndex.Qty, i].Value);
                        RecipeSub.UomNo = Convert.ToInt64(dgRecipe[ColIndex.UOMNo, i].Value);
                        RecipeSub.Wastageper = 0;
                        RecipeSub.WastagePerQty = Convert.ToDecimal(dgRecipe[ColIndex.WastagePerQty, i].Value);
                        RecipeSub.FinalQty = Convert.ToDecimal(dgRecipe[ColIndex.FinalQty, i].Value);
                        RecipeSub.ProductQty = 0;
                        RecipeSub.IsActive = true;
                        dbMRecipeMain.AddMRecipeSub(RecipeSub);

                    }
                    if (dbMRecipeMain.ExecuteNonQueryStatements() == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("Recipe Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            ID = ObjQry.ReturnLong("Select Max(MRecipeID) From MRecipeMain", CommonFunctions.ConStr);
                            RecipeType = ObjQry.ReturnLong("Select RecipeType From MRecipeMain where Max(MRecipeID)=" + ID, CommonFunctions.ConStr);
                            FillControls();
                            NavigationDisplay(2);
                        }
                        else
                        {
                            OMMessageBox.Show("Recipe Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls();
                        }
                        PnlD2.Visible = false;
                        dgRecipe.Top = 87;
                        dgRecipe.Height = 425;
                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("Recipe Not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=1").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["MRecipeID"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=1").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["MRecipeID"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=1 and MRecipeID >" + ID).Table;
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
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=1 and  MRecipeID <" + ID).Table;
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
                else if (type == 0)
                {
                    if (ID == 0)
                    {
                        dtSearch = ObjFunction.GetDataView("Select isnull(max(MRecipeID),0)as MRecipeID From MRecipeMain Where RecipeType=1").Table;
                        No = Convert.ToInt64(dtSearch.Rows[0]["MRecipeID"].ToString());
                        ID = No;
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
                GridFlag = true;
                dgRecipe.Enabled = true;
                btnGOk.Enabled = true;
                btnX.Enabled = true;
                txtDocNo.Enabled = false;

                PnlD2.Visible = true;
                dgRecipe.Top = 162;
                dgRecipe.Height = 352;

                dtpDate.Focus();
                txtDocNo.Text = (ObjQry.ReturnLong("SELECT isnull(max(DocNo),0) as DocNo FROM MRecipeMain Where RecipeType=1", CommonFunctions.ConStr) + 1).ToString();

                FillList();
                txtFinalQty.Enabled = false;
                dtpDate.Value = DateTime.Now.ToUniversalTime();
                txtRecipeQty.Text = "0.00";
                txtProducionQty.Text = "0.00";
                txtWasteQty.Text = "0.00";
                txtFinalQty.Text = "0.00";


                while (dgRecipe.Rows.Count > 0)
                {
                    dgRecipe.Rows.RemoveAt(0);
                }
                dgRecipe.Rows.Add();
                dtDelete.Rows.Clear();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new ItemRecipe();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
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
            NavigationDisplay(0);
            dgRecipe.Enabled = false;
            btnGOk.Enabled = false;
            btnX.Enabled = false;
            PnlD2.Visible = false;
            dgRecipe.Top = 87;
            dgRecipe.Height = 425;
            HidePics();
            btnNew.Focus();
            ObjFunction.FillList(lstUOMName, "SELECT UOMNo,UOMName from MUOM WHERE  isActive='True' ORDER BY UOMName");
            ObjFunction.FillList(lstUOM2, "SELECT UOMNo,UOMName from MUOM WHERE  isActive='True'  ORDER BY UOMName");

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            GridFlag = true;
            dgRecipe.Enabled = true;
            btnGOk.Enabled = true;
            btnX.Enabled = true;
            txtDocNo.Enabled = false;

            txtProducionQty.Text = "0.00";
            txtWasteQty.Text = "0.00";
            txtFinalQty.Text = "0.00";
            txtFinalQty.Enabled = false;
            PnlD2.Visible = true;
            dgRecipe.Top = 162;
            dgRecipe.Height = 352;
          //  dgRecipe.Rows.Add();
            dgRecipe.CurrentCell = dgRecipe[1, dgRecipe.Rows.Count - 1];
            DGRecipeFlag = false;
            if (ObjQry.ReturnInteger("select count(FinishItemID) from MRecipeMain where MRecipeID!= " + ID + " and  FinishItemID=" + lstProductionItem.SelectedValue + " and Recipetype=1 and Groupno=" + lstProductionGroup.SelectedValue + "", CommonFunctions.ConStr) > 0)
            {
            }
                txtRecipeGroup.Focus();
            dtDelete.Rows.Clear();
        }

        #region TextBox KeyDown 
        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                txtProductionGroup.Focus();

            }
        }

        private void txtItemGroup1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ObjFunction.FillList(lstProductionGroup, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3  and ItemGroupNo in(select groupno from MItemMaster where IsActive = 'True' ) ORDER BY ItemGroupName");
                if (txtProductionGroup.Text == "")
                {
                    pnlProductionGroup.Visible = true;
                    lstProductionGroup.Focus();

                }
                else if (txtProductionGroup.Text != "")
                {
                    txtProductionItems.Focus();
                }
                else
                {
                    pnlProductionGroup.Visible = false;
                    lstProductionGroup.Visible = true;
                    lstProductionGroup.Focus();
                }
            }
            else
            {
                pnlProductionGroup.Visible = true;
                lstProductionGroup.Focus();
            }
        }

        private void txtSubGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtProductionItems.Text == "")
                {
                    pnlProductionItem.Visible = true;
                    lstProductionItem.Focus();

                }
                else
                {
                    pnlProductionItem.Visible = false;
                    txtRecipeQty.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                pnlProductionItem.Visible = false;
                txtProductionItems.Focus();
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                pnlProductionItem.Visible = false;
                txtProductionItems.Focus();
            }
            else
            {
                pnlProductionItem.Visible = true;
                lstProductionItem.Focus();
            }
        }


        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtRecipeQty.Text != "0.00")
                {
                    if (lstUOMName.Items.Count == 1)
                    {

                        txtUomName.Text = lstUOMName.Text;
                        pnlRecipeGroup.Visible = true;
                        lstRecipeGroup.Focus();

                    }
                    else
                    {
                        txtUomName.Focus();
                    }
                }
                else
                {
                    txtRecipeQty.Focus();
                }
            }

        }

        private void txtUomName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUomName.Text == "")
                {
                    pnlUOMName.Visible = true;
                    lstUOMName.Focus();

                }
                else if (txtUomName.Text != "")
                {
                    txtRecipeGroup.Focus();
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
                    txtRecipeGroup.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    pnlUOMName.Visible = false;
                    txtUomName.Focus();
                }
                else if (e.KeyCode == Keys.Space)
                {
                    pnlUOMName.Visible = false;
                    txtProducionQty.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtItemGroup2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtRecipeGroup.Text == "")
                {
                    pnlRecipeGroup.Visible = true;
                    lstRecipeGroup.Focus();
                    lstRecipeGroup.SelectedIndex = 0;

                }
                else if (txtRecipeGroup.Text != "")
                {
                    txtRecipeItems.Focus();
                }
                else
                {
                    pnlRecipeGroup.Visible = false;
                    lstRecipeGroup.Visible = true;
                    lstRecipeGroup.Focus();
                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                BtnSave.Focus();
            }
        }



        private void txtProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtRecipeItems.Text == "")
                {
                    pnlRecipeItems.Visible = true;
                    lstRecipeItems.Focus();
                    lstRecipeItems.SelectedIndex = 0;
                }
                else if (txtRecipeItems.Text != "")
                {

                    txtProducionQty.Focus();
                }
                else
                {
                    pnlRecipeItems.Visible = false;
                    lstRecipeItems.Visible = true;
                    lstRecipeItems.Focus();
                }
            }
            else
            {
                pnlRecipeItems.Visible = true;
                lstRecipeItems.Focus();
            }
        }

        public void CalculateTotal()
        {

            txtFinalQty.Text = (Convert.ToDouble(txtProducionQty.Text) + Convert.ToDouble(txtWasteQty.Text)).ToString();
        }
        public void InitalTable()
        {

            dtDelete.Columns.Add();
            dtDelete.Columns.Add();
        }

        public void DeleteDtls(int Type, long PkNo)
        {
            DataRow dr = null;
            dr = dtDelete.NewRow();
            dr[0] = Type;
            dr[1] = PkNo;
            dtDelete.Rows.Add(dr);
        }
        public void delete_Row()
        {
            // DeleteDtls(0, Convert.ToInt64(dgRecipe.CurrentRow.Cells[ColIndex.SRecipeID].Value));

        }
        private void txtQty2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtProducionQty.Text != "0.00")
                {
                    CalculateTotal();
                    txtUOM2.Focus();
                }
                else
                {
                    txtProducionQty.Focus();
                }
            }
        }

        private void txtUOM2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtUOM2.Text == "")
                {
                    pnlUOM2.Visible = true;
                    lstUOM2.Focus();
                    lstUOM2.SelectedIndex = 0;
                }
                else if (txtUOM2.Text != "")
                {
                    txtWasteQty.Focus();
                }
                else
                {
                    pnlUOM2.Visible = false;
                    lstUOM2.Visible = true;
                    lstUOM2.Focus();
                }
            }
            else
            {
                pnlUOM2.Visible = true;
                lstUOM2.Focus();
            }
        }

        private void lstUOM2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtUOM2.Text = lstUOM2.Text;
                    pnlUOM2.Visible = false;
                    txtWasteQty.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (txtUOM2.Text == "")
                    {
                        pnlUOM2.Visible = true;
                        lstUOM2.Focus();
                        lstUOM2.SelectedIndex = 0;
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        pnlUOM2.Visible = false;
                        txtWasteQty.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtWasteQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtWasteQty.Text != "")
                {
                    CalculateTotal();
                    btnGOk.Focus();
                }
                else
                {
                    txtWasteQty.Focus();
                }
            }
        }

        private void txtFinalQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtFinalQty.Text != "")
                {
                    btnGOk.Focus();
                }
                else
                {
                    btnGOk.Focus();
                }
            }
        }


        private void lstUOMName_Leave(object sender, EventArgs e)
        {
            pnlUOMName.Visible = false;
        }



        private void lstProductName_Leave(object sender, EventArgs e)
        {
            pnlRecipeItems.Visible = false;
        }

        private void lstUOM2_Leave(object sender, EventArgs e)
        {
            pnlUOM2.Visible = false;
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 20, OMFunctions.MaskedType.NotNegative);
        }

        private void txtWasteQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 20, OMFunctions.MaskedType.NotNegative);
        }

        private void txtFinalQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 20, OMFunctions.MaskedType.NotNegative);
        }

        private void txtQty2_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 20, OMFunctions.MaskedType.NotNegative);
        }

        public void Clear()
        {
            txtRecipeGroup.Text = "";
            txtRecipeItems.Text = "";
            txtProducionQty.Text = "0.00";
            txtUOM2.Text = "";
            txtWasteQty.Text = "0.00";
            txtFinalQty.Text = "0.00";
            HidePics();
        }

        public bool Validations()
        {
            bool flag = false;

            if (txtProductionGroup.Text.Trim() == "")
            {
                txtProductionGroup.Focus();
            }
            else if (txtProductionItems.Text.Trim() == "")
            {
                txtProductionItems.Focus();
            }
            else if ((txtRecipeQty.Text == "0.00") && (Convert.ToDouble(txtRecipeQty.Text) != 0))
            {
                txtRecipeQty.Focus();
                OMMessageBox.Show("Please Enter Quantity");
            }
            else if (txtWasteQty.Text == "")
            {
                txtWasteQty.Focus();
            }
            else if (txtFinalQty.Text == "")
            {
                btnGOk.Focus();
            }

            else
                flag = true;
            return flag;
        }

        public bool GridValidations()
        {
            bool flag = false;

            if (txtProductionGroup.Text.Trim() == "")
            {
                txtProductionGroup.Focus();
            }
            else if (txtProductionItems.Text.Trim() == "")
            {
                txtProductionItems.Focus();
            }
            else if (txtRecipeGroup.Text.Trim() == "")
            {
                txtRecipeGroup.Focus();
            }
            else if (txtRecipeItems.Text.Trim() == "")
            {
                txtRecipeItems.Focus();
            }
            else if ((txtProducionQty.Text.Trim() == "0.00") || (Convert.ToDouble(txtProducionQty.Text) == 0) || (txtProducionQty.Text.ToString() == ""))
            {
                txtProducionQty.Focus();
                OMMessageBox.Show("Please Enter Quantity");
            }
            else if ((txtFinalQty.Text.Trim() == "0.00") || (Convert.ToDouble(txtFinalQty.Text) == 0) || (txtFinalQty.Text.ToString() == ""))
            {
                txtProducionQty.Focus();
                OMMessageBox.Show("Please Enter Quantity");
            }
            else if (txtUOM2.Text == "")
            {
                txtUOM2.Focus();
            }
            else if (txtWasteQty.Text == "")
            {
                txtWasteQty.Focus();
            }
            else if (txtFinalQty.Text == "")
            {
                btnGOk.Focus();
            }
            else
                flag = true;
            return flag;
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

        private void FormatPicture()
        {
            pnlProductionGroup.Top = txtProductionGroup.Bottom + 20;
            pnlProductionGroup.Left = txtProductionGroup.Left + 50;
            pnlProductionGroup.Width = txtProductionGroup.Width;
            pnlProductionGroup.Height = 200;
            lstProductionGroup.Top = pnlProductionGroup.Top - 50;
            lstProductionGroup.Height = pnlProductionGroup.Height - 5;

            pnlProductionItem.Left = txtProductionItems.Left + 50;
            pnlProductionItem.Top = txtProductionItems.Bottom + 20;
            pnlProductionItem.Width = txtProductionItems.Width;
            pnlProductionItem.Height = 200;
            lstProductionItem.Top = pnlProductionItem.Top - 50;
            lstProductionItem.Height = pnlProductionItem.Height - 5;

            pnlUOMName.Top = txtUomName.Bottom + 20;
            pnlUOMName.Width = txtUomName.Width;
            pnlUOMName.Height = 80;
            lstUOMName.Top = pnlUOMName.Top - 80;
            lstUOMName.Height = pnlUOMName.Height - 5;

            pnlRecipeGroup.Top = txtRecipeGroup.Bottom + 97;
            pnlRecipeGroup.Width = txtRecipeGroup.Width;
            pnlRecipeGroup.Height = 200;
            lstRecipeGroup.Top = pnlRecipeGroup.Top - 125;
            lstRecipeGroup.Height = pnlRecipeGroup.Height - 5;

            pnlRecipeItems.Top = txtRecipeItems.Bottom + 90;
            pnlRecipeItems.Left = txtRecipeItems.Left;
            pnlRecipeItems.Width = txtRecipeItems.Width;
            pnlRecipeItems.Height = 200;
            lstRecipeItems.Top = pnlRecipeItems.Top - 120;
            lstRecipeItems.Height = pnlRecipeItems.Height - 5;

            pnlUOM2.Top = txtUOM2.Bottom + 97;
            pnlUOM2.Width = txtUOM2.Width;
            pnlUOM2.Height = 80;
            lstUOM2.Top = pnlUOM2.Top - 157;
            lstUOM2.Height = pnlUOM2.Height - 5;
        }

        private void HidePics()
        {
            pnlProductionGroup.Visible = false;
            pnlProductionItem.Visible = false;
            pnlRecipeGroup.Visible = false;
            pnlRecipeItems.Visible = false;
            pnlUOMName.Visible = false;
            pnlUOM2.Visible = false;

        }

        private void PnlD1_Click(object sender, EventArgs e)
        {
            if (pnlProductionGroup.Visible == true)
            {
                pnlProductionGroup.Visible = false;
                txtProductionGroup.Focus();
            }
            else if (pnlProductionItem.Visible == true)
            {
                pnlProductionItem.Visible = false;
                txtProductionItems.Focus();
            }
            else if (pnlUOMName.Visible == true)
            {
                pnlUOMName.Visible = false;
                txtUomName.Focus();
            }
            else if (pnlRecipeGroup.Visible == true)
            {
                pnlRecipeGroup.Visible = false;
                txtRecipeGroup.Focus();
            }
            else if (pnlRecipeItems.Visible == true)
            {
                pnlRecipeItems.Visible = false;
                txtRecipeItems.Focus();
            }
            else if (pnlUOM2.Visible == true)
            {
                pnlUOM2.Visible = false;
                txtUOM2.Focus();
            }
        }

        private void PnlD2_Click(object sender, EventArgs e)
        {
            if (pnlProductionGroup.Visible == true)
            {
                pnlProductionGroup.Visible = false;
                txtProductionGroup.Focus();
            }
            else if (pnlProductionItem.Visible == true)
            {
                pnlProductionItem.Visible = false;
                txtProductionItems.Focus();
            }
            else if (pnlUOMName.Visible == true)
            {
                pnlUOMName.Visible = false;
                txtUomName.Focus();
            }
            else if (pnlRecipeGroup.Visible == true)
            {
                pnlRecipeGroup.Visible = false;
                txtRecipeGroup.Focus();
            }
            else if (pnlRecipeItems.Visible == true)
            {
                pnlRecipeItems.Visible = false;
                txtRecipeItems.Focus();
            }
            else if (pnlUOM2.Visible == true)
            {
                pnlUOM2.Visible = false;
                txtUOM2.Focus();
            }
        }

        private void pnlMain_Click(object sender, EventArgs e)
        {
            if (pnlProductionGroup.Visible == true)
            {
                pnlProductionGroup.Visible = false;
                txtProductionGroup.Focus();
            }
            else if (pnlProductionItem.Visible == true)
            {
                pnlProductionItem.Visible = false;
                txtProductionItems.Focus();
            }
            else if (pnlUOMName.Visible == true)
            {
                pnlUOMName.Visible = false;
                txtUomName.Focus();
            }
            else if (pnlRecipeGroup.Visible == true)
            {
                pnlRecipeGroup.Visible = false;
                txtRecipeGroup.Focus();
            }
            else if (pnlRecipeItems.Visible == true)
            {
                pnlRecipeItems.Visible = false;
                txtRecipeItems.Focus();
            }
            else if (pnlUOM2.Visible == true)
            {
                pnlUOM2.Visible = false;
                txtUOM2.Focus();
            }
        }
        #endregion
        public bool ItemExist(long ItNo, out int rowIndex)
        {

            rowIndex = -1;
            bool flag = false;
            try
            {
                for (int i = 0; i < dgRecipe.Rows.Count - 1; i++)
                {
                    if (ItNo == Convert.ToInt64(dgRecipe.Rows[i].Cells[ColIndex.RawProductID].Value))
                    {
                        rowIndex = i;
                        flag = true;
                        break;

                    }
                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }
        private void btnGOk_Click(object sender, EventArgs e)
        {

            if (GridValidations() == true)
            {
                if (dgRecipe.CurrentRow.Cells[ColIndex.RawProductID].Value != null || dgRecipe.CurrentRow.Cells[ColIndex.RawGroupNo].Value != null)
                {
                    //dgRecipe.CurrentCell = dgRecipe[1, dgRecipe.Rows.Count - 1];
                }

                dgRecipe.CurrentRow.Cells[ColIndex.PItem].Value = txtRecipeItems.Text;
                dgRecipe.CurrentRow.Cells[ColIndex.Group].Value = txtRecipeGroup.Text;
                dgRecipe.CurrentRow.Cells[ColIndex.Item].Value = txtProductionGroup.Text;
                dgRecipe.CurrentRow.Cells[ColIndex.Qty].Value = Convert.ToDecimal(txtProducionQty.Text);
                dgRecipe.CurrentRow.Cells[ColIndex.UOMName].Value = txtUOM2.Text;
                dgRecipe.CurrentRow.Cells[ColIndex.WastagePerQty].Value = Convert.ToDecimal(txtWasteQty.Text);
                dgRecipe.CurrentRow.Cells[ColIndex.FinalQty].Value = Convert.ToDecimal(txtFinalQty.Text);
                dgRecipe.CurrentRow.Cells[ColIndex.RawGroupNo].Value = Convert.ToInt64(lstRecipeGroup.SelectedValue);
                dgRecipe.CurrentRow.Cells[ColIndex.RawProductID].Value = Convert.ToInt64(lstRecipeItems.SelectedValue);
                dgRecipe.CurrentRow.Cells[ColIndex.UOMNo].Value = Convert.ToInt64(lstUOM2.SelectedValue);
                dgRecipe.CurrentRow.Cells[ColIndex.MRecipeID].Value = 0;
                if (ID == 0)
                {
                    dgRecipe.CurrentRow.Cells[ColIndex.SRecipeID].Value = 0;
                }
                HidePics();
                Clear();

                if (DGRecipeFlag == false)
                {
                    dgRecipe.Rows.Add();
                }
                DGRecipeFlag = false;
                txtRecipeGroup.Focus();
                dgRecipe.CurrentCell = dgRecipe[1, dgRecipe.Rows.Count - 1];
            }
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            Clear();
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
            DataGridViewCell cell = null;
            foreach (DataGridViewCell selectedCell in dgRecipe.SelectedCells)
            {
                cell = selectedCell;
                break;
            }
            if (GridFlag == true)
            {
                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;

                    long MRecipeID = Convert.ToInt64(dgRecipe.CurrentRow.Cells[ColIndex.MRecipeID].Value);
                    dgRecipe.CurrentRow.Cells[ColIndex.MRecipeID].Value = MRecipeID;

                    txtRecipeGroup.Text = dgRecipe.CurrentRow.Cells[ColIndex.Group].Value.ToString();
                    lstRecipeGroup.SelectedValue = dgRecipe.CurrentRow.Cells[ColIndex.RawGroupNo].Value;

                    ObjFunction.FillList(lstRecipeItems, "SELECT MItemMaster.ItemNo, MItemMaster.ItemName FROM  MItemGroup INNER JOIN MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo WHERE(MItemMaster.IsActive = 'True') AND ItemGroupNo = " + Convert.ToInt64(lstRecipeGroup.SelectedValue) + " and MItemMaster.ItemNo Not In(" + Convert.ToInt64(lstProductionItem.SelectedValue) + ") ORDER BY MItemMaster.ItemName");

                    lstRecipeItems.SelectedValue = dgRecipe.CurrentRow.Cells[ColIndex.RawProductID].Value;
                    txtRecipeItems.Text = dgRecipe.CurrentRow.Cells[ColIndex.PItem].Value.ToString();
                    txtProducionQty.Text = dgRecipe.CurrentRow.Cells[ColIndex.Qty].Value.ToString();

                    ObjFunction.FillList(lstUOM2, "select distinct UOMH as UOMNo,UOMName from(SELECT UOMH, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOMH = MUOM.UOMNo where ItemNo = " + Convert.ToInt64(lstRecipeItems.SelectedValue) + " Union All SELECT UOML, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOML = MUOM.UOMNo where ItemNo =  " + Convert.ToInt64(lstRecipeItems.SelectedValue) + ") as tbl");

                    lstUOM2.SelectedValue = dgRecipe.CurrentRow.Cells[ColIndex.UOMNo].Value;
                    txtUOM2.Text = dgRecipe.CurrentRow.Cells[ColIndex.UOMName].Value.ToString();
                    txtWasteQty.Text = dgRecipe.CurrentRow.Cells[ColIndex.WastagePerQty].Value.ToString();
                    txtFinalQty.Text = dgRecipe.CurrentRow.Cells[ColIndex.FinalQty].Value.ToString();

                    txtRecipeGroup.Focus();
                    DGRecipeFlag = true;

                    int Index = cell.RowIndex;
                    lblGridIndex.Text = Index.ToString();
                }
            }
        }

        private void lstRecipeGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtRecipeGroup.Text = lstRecipeGroup.Text;
                    pnlRecipeGroup.Visible = false;

                    ObjFunction.FillList(lstRecipeItems, "SELECT MItemMaster.ItemNo, MItemMaster.ItemName FROM  MItemGroup INNER JOIN MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo WHERE(MItemMaster.IsActive = 'True') AND ItemGroupNo = " + Convert.ToInt64(lstRecipeGroup.SelectedValue) + " and MItemMaster.ItemNo Not In(" + Convert.ToInt64(lstProductionItem.SelectedValue) + ") ORDER BY MItemMaster.ItemName");
                    txtRecipeItems.Text = "";
                    //pnlRecipeItems.Visible = true;
                    txtRecipeItems.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    pnlRecipeGroup.Visible = false;
                    txtRecipeGroup.Focus();

                }
                else if (e.KeyCode == Keys.Space)
                {
                    pnlRecipeGroup.Visible = false;
                    txtProducionQty.Focus();

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstRecipeGroup_Leave(object sender, EventArgs e)
        {
            pnlRecipeGroup.Visible = false;
        }

        private void lstRecipeItems_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtRecipeItems.Text = lstRecipeItems.Text;
                    pnlRecipeItems.Visible = false;
                    ObjFunction.FillList(lstUOM2, "select distinct UOMH as UOMNo,UOMName from(SELECT UOMH, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOMH = MUOM.UOMNo where ItemNo = " + Convert.ToInt64(lstRecipeItems.SelectedValue) + " Union All SELECT UOML, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOML = MUOM.UOMNo where ItemNo =  " + Convert.ToInt64(lstRecipeItems.SelectedValue) + ") as tbl");
                    txtProducionQty.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    if (txtRecipeItems.Text == "")
                    {
                        pnlRecipeItems.Visible = true;
                        lstRecipeItems.Focus();
                        lstRecipeItems.SelectedIndex = 0;
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                        pnlRecipeItems.Visible = false;
                        txtProducionQty.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstRecipeItems_Leave(object sender, EventArgs e)
        {
            pnlProductionItem.Visible = false;
            int rwindex = 0;
            if (ItemExist(Convert.ToInt64(lstRecipeItems.SelectedValue), out rwindex) == true)
            {
                OMMessageBox.Show("Item Alredy exit in Recipe.........");
                txtRecipeItems.Text = "";
                txtRecipeItems.Focus();
                return;
            }
        }

        private void lstProductionItem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtProductionItems.Text = lstProductionItem.Text;
                    pnlProductionItem.Visible = false;
                    ObjFunction.FillList(lstUOMName, "select distinct UOMH as UOMNo,UOMName from(SELECT UOMH, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOMH = MUOM.UOMNo where ItemNo = " + Convert.ToInt64(lstProductionItem.SelectedValue) + " Union All SELECT UOML, UOMName FROM MItemMaster INNER JOIN MUOM ON MItemMaster.UOML = MUOM.UOMNo where ItemNo =  " + Convert.ToInt64(lstProductionItem.SelectedValue) + ") as tbl");
                    txtRecipeQty.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {

                    pnlProductionItem.Visible = false;
                    txtProductionItems.Focus();
                }
                else if (e.KeyCode == Keys.Space)
                {

                    pnlProductionItem.Visible = false;
                    txtProductionGroup.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstProductionItem_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtProductionItems.Text.Trim() != "")
                {
                    pnlProductionItem.Visible = false;
                    if (ObjQry.ReturnInteger("select count(FinishItemID) from MRecipeMain where MRecipeID!= "+ ID + " and  FinishItemID=" + lstProductionItem.SelectedValue + " and Recipetype=1 and Groupno=" + lstProductionGroup.SelectedValue + "", CommonFunctions.ConStr) > 0)
                    {
                        OMMessageBox.Show("Duplicate Recipe Item Selected ...........");
                        txtProductionItems.Text = "";
                        txtProductionItems.Focus();
                        return;
                    }
                    ObjFunction.FillList(lstRecipeGroup, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 and ItemGroupNo in (select groupno from MItemMaster where isactive='true'  and MItemMaster.ItemNo Not In(" + Convert.ToInt64(lstProductionItem.SelectedValue) + ")) ORDER BY ItemGroupName");

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void lstProductionGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    txtProductionGroup.Text = lstProductionGroup.Text;
                    pnlProductionGroup.Visible = false;

                    ObjFunction.FillList(lstProductionItem, "SELECT MItemMaster.ItemNo, MItemMaster.ItemName FROM  MItemGroup INNER JOIN MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo WHERE(MItemMaster.IsActive = 'True') AND ItemGroupNo = " + Convert.ToInt64(lstProductionGroup.SelectedValue) + " ORDER BY MItemMaster.ItemName");
                    txtProductionItems.Text = "";
                    txtProductionItems.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    pnlProductionGroup.Visible = false;
                    txtProductionGroup.Focus();

                }
                else if (e.KeyCode == Keys.Space)
                {
                    pnlProductionGroup.Visible = false;
                    dtpDate.Focus();

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstProductionGroup_Leave(object sender, EventArgs e)
        {
            try
            {


                if (txtProductionGroup.Text.Trim() != "")
                {
                    pnlProductionGroup.Visible = false;

                    txtProductionItems.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void ItemRecipeAE_Activated(object sender, EventArgs e)
        {
            ObjFunction.FillList(lstRecipeGroup, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=3 and ItemGroupNo in (select groupno from MItemMaster where isactive='true'  and MItemMaster.ItemNo Not In(" + Convert.ToInt64(lstProductionItem.SelectedValue) + ")) ORDER BY ItemGroupName");

        }

        private void dgRecipe_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgRecipe.CurrentCell != null)
                {
                    for (int i = 0; i < dgRecipe.Rows.Count; i++)
                    {
                        dgRecipe.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    dgRecipe.Rows[dgRecipe.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 224, 192);
                    dgRecipe.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
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
                if (Convert.ToInt64(dgRecipe.CurrentRow.Cells[ColIndex.RawProductID].Value) != 0)
                {
                    //dgRecipe.CurrentCell = dgRecipe[dgRecipe.CurrentCell.ColumnIndex, dgRecipe.CurrentCell.RowIndex];
                    if (dgRecipe.Rows.Count - 1 == dgRecipe.CurrentCell.RowIndex)
                    {
                        dgRecipe.Rows.RemoveAt(dgRecipe.CurrentCell.RowIndex);
                        dgRecipe.Rows.Add();
                    }
                    else
                    {
                       
                        //  long PKStockTrnNo = Convert.ToInt64(dgRecipe.Rows[dgRecipe.CurrentCell.RowIndex].Cells[ColIndex.Item].Value);
                        DeleteDtls(0, Convert.ToInt64(dgRecipe.CurrentRow.Cells[ColIndex.SRecipeID].Value));
                        dgRecipe.Rows.RemoveAt(dgRecipe.CurrentCell.RowIndex);
                        DGRecipeFlag = false;
                    }
                }

                //  }

                dgRecipe.CurrentCell = dgRecipe[1, dgRecipe.Rows.Count - 1];
                txtRecipeGroup.Focus();
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
            public static int UOMName = 5;
            public static int WastagePerQty = 6;
            public static int FinalQty = 7;
            public static int RawGroupNo = 8;
            public static int RawProductID = 9;
            public static int UOMNo = 10;
            public static int MRecipeID = 11;
            public static int SRecipeID = 12;
        }
        #endregion
    }
}
