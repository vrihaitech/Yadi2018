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

namespace Yadi.Settings
{
    public partial class GodownSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions(); 
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMGodownSetting dbGodownSetting = new DBMGodownSetting();
        MGodownSetting mGodownSetting = new MGodownSetting();
        DataTable dtDelete = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dtMain = new DataTable();
        DataTable dtItem = new DataTable();
        DataTablesCollection dtBillCollect = new DataTablesCollection();     
        int Postion = 0;

        public GodownSetting()
        {
            InitializeComponent();
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void GodownSetting_Load(object sender, EventArgs e)
        {
            try
            {

                //ObjFunction.LockButtons(true, this.Controls);
                //ObjFunction.LockControls(false, this.Controls);
                ObjFunction.FillCombo(CmbBrand, "Select StockGroupNo,StockGroupName from MStockGroup where ControlGroup=3 and ISActive='true' ORDER BY StockGroupName");
                ObjFunction.FillList(lstLocation, "Select GodownNo,GodownNAme from MGodown where IsActive='true' and GodownNo<>1 order by GodownNAme");

                lblWait.Font = new Font("Verdana", 18, FontStyle.Bold);
                lblWait.ForeColor = Color.Green;

                

                label1.Font = new Font("Verdana", 12, FontStyle.Bold);
                
                InitItemTable();
                InitDelTable();
                CmbBrand.Enabled = true;
                CmbBrand.Focus();
                //btnNew.Focus();
                KeyDownFormat(this.Controls);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void InitItemTable()
        {
            try
            {

                dtItem = ObjFunction.GetDataView(" SELECT  0 AS SrNo,MGodownSetting.QuantitySlabFrom, MGodownSetting.QuantitySlabTo, MGodown.GodownName, MGodownSetting.GodownNo, " +
                                                 " MGodownSetting.PkGodownSettingNo, MGodownSetting.ItemNo, MGodownSetting.FkBcdSrNo, MGodownSetting.UOMNo FROM  MGodownSetting INNER JOIN " +
                                                 " MGodown ON MGodownSetting.GodownNo = MGodown.GodownNo where PkGodownSettingNo=0").Table;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillControls()
        {
            try
            {
                BindGrid();
               
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        public static class ColIndexBrand
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int Barcode = 2;
            public static int Uom = 3;
            public static int ItemNo = 4;
            public static int BarcodeNo = 5;
            public static int UomNo = 6;

        }

        public static class ColIndexItem
        {
            public static int SrNo = 0;
            public static int FromSlab = 1;
            public static int ToSlab = 2;
            public static int Location = 3;
            public static int LocationNo = 4;
            public static int SettingNo = 5;
            public static int ItemNo = 6;
            public static int FkBcdNo = 7;
            public static int UomNo = 8;

        }

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(700);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayMessageForWait(bool flag)
        {

            try
            {
                lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                
                //this.lblWait.Enabled = true;
                //if (h == 100)
                //    h = 0;
                //else
                //    h = h + 1;

                //if (h == 1)
                //    lblWait.Text = "Processing .";
                //else if (h == 0 || h == 30 || h == 60 || h == 90)
                //    lblWait.Text = "Processing .";
                //else if (h == 10 || h == 40 || h == 70 || h == 100)
                //    lblWait.Text = "Processing ..";
                //else if (h == 20 || h == 50 || h == 80)
                //    lblWait.Text = "Processing ...";
                lblWait.Text = "Processing ...";
                Application.DoEvents();
                lblWait.Visible = flag;
                Application.DoEvents();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
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
            try
            {
               if (e.KeyCode == Keys.F2)
                {
                    if (BtnSave.Visible) BtnSave_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F5)
                {
                    if (btnApply.Enabled) btnApply_Click(sender, e);
                }
                else if (e.KeyCode == Keys.F6)
                {
                    if (btnItemCancel.Enabled) btnItemCancel_Click(sender, e);
                }                
                
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
        #endregion

        #region Delete code

        public void InitDelTable()
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

        public void DeleteValues()
        {
            try
            {
                if (dtDelete != null)
                {
                    
                    for (int i = 0; i < dtDelete.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
                        {
                           
                            mGodownSetting = new MGodownSetting();
                            mGodownSetting.PkGodownSettingNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                            dbGodownSetting.DeleteMGodownSetting(mGodownSetting);
                        }
                    }
                    dtDelete.Rows.Clear();
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        #endregion
     
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ObjFunction.GetComboValue(CmbBrand) == 0)
                {
                    OMMessageBox.Show("Select Brand", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    CmbBrand.Focus();
                    return;
                }
                dbGodownSetting = new DBMGodownSetting();
                DeleteValues();
                for (int i = 0; i < dtItem.Rows.Count; i++)
                {
                    mGodownSetting = new MGodownSetting();
                    mGodownSetting.PkGodownSettingNo = Convert.ToInt64(dtItem.Rows[i].ItemArray[ColIndexItem.SettingNo]);
                    mGodownSetting.QuantitySlabFrom = Convert.ToDouble(dtItem.Rows[i].ItemArray[ColIndexItem.FromSlab]);
                    mGodownSetting.QuantitySlabTo = Convert.ToDouble(dtItem.Rows[i].ItemArray[ColIndexItem.ToSlab]);
                    mGodownSetting.FkBcdSrNo = Convert.ToInt64(dtItem.Rows[i].ItemArray[ColIndexItem.FkBcdNo]);
                    mGodownSetting.ItemNo = Convert.ToInt64(dtItem.Rows[i].ItemArray[ColIndexItem.ItemNo]);
                    mGodownSetting.UOMNo = Convert.ToInt64(dtItem.Rows[i].ItemArray[ColIndexItem.UomNo]);
                    mGodownSetting.GodownNo = Convert.ToInt64(dtItem.Rows[i].ItemArray[ColIndexItem.LocationNo]);
                    mGodownSetting.UserID = DBGetVal.UserID;
                    mGodownSetting.UserDate = DBGetVal.ServerTime.Date;
                    dbGodownSetting.AddMGodownSetting(mGodownSetting);
                }
                if (dtItem.Rows.Count > 0)
                {
                    if (dbGodownSetting.ExecuteNonQueryStatements() == true)
                    {
                        OMMessageBox.Show("Godown Setting Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        InitItemTable();
                        btnNew_Click(sender, e);
                    }
                    else
                        OMMessageBox.Show("Godown Setting not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
                else
                {
                    OMMessageBox.Show("No Data to save", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                dgBrand.Enabled = true;
                dgItemMaster.Enabled = true;
                InitControls();
                InitItemTable();
                CmbBrand.Focus();
               
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                InitControls();                
                dgBrand.Enabled = false;
                dgItemMaster.Enabled = false;
                ObjFunction.LockControls(false, this.Controls);
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                dgBrand.Enabled = true;
                dgItemMaster.Enabled = true;
                InitControls();
                InitItemTable();
                CmbBrand.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            //ItemLevelDiscountSearch.RequestDiscNo = 0;
            this.Close();
        }

        public void InitControls()
        {
            try
            {

                CmbBrand.Enabled = true;
                CmbBrand.SelectedValue = 0;
                
                while (dgBrand.Rows.Count > 0)
                    dgBrand.Rows.RemoveAt(0);
                while (dgItemMaster.Rows.Count > 0)
                    dgItemMaster.Rows.RemoveAt(0);
              
               
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBrand_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    e.SuppressKeyPress = true;
                    Postion = dgBrand.CurrentRow.Index;
                    if (BindItemMaster(Postion))
                    {
                        if (BtnSave.Visible == false) return;
                        dgBrand.Enabled = false;
                        dgItemMaster.Enabled = true;
                        btnApply.Enabled = true;
                        btnItemCancel.Enabled = true;
                        dgItemMaster.CurrentCell = dgItemMaster.Rows[0].Cells[ColIndexItem.ToSlab];
                        dgItemMaster.Focus();
                    }
                    else
                    {
                        btnApply.Enabled = false;
                        btnItemCancel.Enabled = false;
                        DisplayMessage("Item Not Found...");
                        dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[ColIndexBrand.ItemName];
                        dgBrand.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    BtnSave.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool BindItemMaster(int dtPostion)
        {
            try
            {
                dgItemMaster.Rows.Clear();
                DataRow[] drItem = dtItem.Select("ItemNo=" + dgBrand.Rows[dtPostion].Cells[ColIndexBrand.ItemNo].Value.ToString() + "");

                if (drItem.Length > 0)
                {
                    foreach (DataRow row in drItem)
                    {
                        dgItemMaster.Rows.Add();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.SrNo].Value = row[ColIndexItem.SrNo].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.FromSlab].Value = row[ColIndexItem.FromSlab].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.ToSlab].Value = row[ColIndexItem.ToSlab].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.Location].Value = row[ColIndexItem.Location].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.LocationNo].Value = row[ColIndexItem.LocationNo].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.FkBcdNo].Value = row[ColIndexItem.FkBcdNo].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.ItemNo].Value = row[ColIndexItem.ItemNo].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.SettingNo].Value = row[ColIndexItem.SettingNo].ToString();
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.UomNo].Value = row[ColIndexItem.UomNo].ToString();                        
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].ReadOnly = true;

                    }
                    dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].ReadOnly = false;
                }
                else
                {
                    DataTable dt = ObjFunction.GetDataView(" SELECT 0 AS SrNo, MGodownSetting.QuantitySlabFrom, MGodownSetting.QuantitySlabTo, MGodown.GodownName, MGodownSetting.GodownNo, " +
                                                                 " MGodownSetting.PkGodownSettingNo, MGodownSetting.ItemNo, MGodownSetting.FkBcdSrNo, MGodownSetting.UOMNo FROM MGodownSetting INNER JOIN " +
                                                                 " MGodown ON MGodownSetting.GodownNo = MGodown.GodownNo " +
                                                                 " where ItemNo=" + Convert.ToInt64(dgBrand.Rows[Postion].Cells[ColIndexBrand.ItemNo].Value) + "").Table;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dgItemMaster.Rows.Add();
                            for (int j = 0; j < dgItemMaster.Columns.Count; j++)
                            {
                                dgItemMaster.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                            }
                            
                            dgItemMaster.Rows[i].ReadOnly = true;
                        }
                        dgItemMaster.Rows[dt.Rows.Count-1].ReadOnly = false;
                    }
                }

                if (dgItemMaster.Rows.Count == 0)
                {
                    dgItemMaster.Rows.Add();
                    dgItemMaster.Rows[0].Cells[1].Value = "0";
                    dgItemMaster.Rows[0].Cells[ColIndexItem.ItemNo].Value = dgBrand.Rows[Postion].Cells[ColIndexBrand.ItemNo].Value;
                    dgItemMaster.Rows[0].Cells[ColIndexItem.FkBcdNo].Value = dgBrand.Rows[Postion].Cells[ColIndexBrand.BarcodeNo].Value;
                    dgItemMaster.Rows[0].Cells[ColIndexItem.UomNo].Value = dgBrand.Rows[Postion].Cells[ColIndexBrand.UomNo].Value;
                   
                   

                }
                //else
                //{
                //    btnApply.Enabled = true;
                //    btnItemCancel.Enabled = true;
                //    dgItemMaster.Enabled = true;
                //    dgItemMaster.Rows[dt.Rows.Count].Cells[1].Value = (Convert.ToInt64(dgItemMaster.Rows[dt.Rows.Count - 1].Cells[2].Value) + 1).ToString();
                //    dgItemMaster.Rows[dt.Rows.Count].Cells[ColIndexItem.ItemNo].Value = (Convert.ToInt64(dgItemMaster.Rows[dt.Rows.Count - 1].Cells[ColIndexItem.ItemNo].Value)).ToString();
                //    dgItemMaster.Rows[dt.Rows.Count].Cells[ColIndexItem.FkBcdNo].Value = (Convert.ToInt64(dgItemMaster.Rows[dt.Rows.Count - 1].Cells[ColIndexItem.FkBcdNo].Value)).ToString();
                //    dgItemMaster.Rows[dt.Rows.Count].Cells[ColIndexItem.UomNo].Value = (Convert.ToInt64(dgItemMaster.Rows[dt.Rows.Count - 1].Cells[ColIndexItem.UomNo].Value)).ToString();
                //}
                return true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

       
      
        private void dgItemMaster_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dgItemMaster.CurrentCell.ColumnIndex == ColIndexItem.ToSlab)
                {
                    TextBox txt1 = (TextBox)e.Control;
                    txt1.TextChanged += new EventHandler(ToSlab_TextChanged);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void ToSlab_TextChanged(object sender, EventArgs e)
        {

            if (dgItemMaster.CurrentCell.ColumnIndex == ColIndexItem.ToSlab)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9);
            }
        }     

       

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidData() == false) return;
                DataRow[] drItem = dtItem.Select("ItemNo=" + dgBrand.Rows[Postion].Cells[ColIndexBrand.ItemNo].Value.ToString() + "");

               
                foreach (DataRow row in drItem)
                {
                    dtItem.Rows.Remove(row);
                }

                for (int i = 0; i < dgItemMaster.Rows.Count; i++)
                {
                    CmbBrand.Enabled = false;
                    DataRow dr = dtItem.NewRow();
                    dr[ColIndexItem.SrNo] = 0;
                    dr[ColIndexItem.FromSlab] = dgItemMaster.Rows[i].Cells[ColIndexItem.FromSlab].Value;
                    dr[ColIndexItem.ToSlab] = dgItemMaster.Rows[i].Cells[ColIndexItem.ToSlab].Value;
                    dr[ColIndexItem.Location] = dgItemMaster.Rows[i].Cells[ColIndexItem.Location].Value;
                    dr[ColIndexItem.LocationNo]=dgItemMaster.Rows[i].Cells[ColIndexItem.LocationNo].Value;
                    if (dgItemMaster.Rows[i].Cells[ColIndexItem.SettingNo].EditedFormattedValue.ToString() == "")
                        dgItemMaster.Rows[i].Cells[ColIndexItem.SettingNo].Value = 0;
                    dr[ColIndexItem.SettingNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.SettingNo].Value;
                    dr[ColIndexItem.ItemNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.ItemNo].Value;
                    dr[ColIndexItem.FkBcdNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.FkBcdNo].Value;
                    dr[ColIndexItem.UomNo] = dgItemMaster.Rows[i].Cells[ColIndexItem.UomNo].Value;
                    dtItem.Rows.Add(dr);
                }

                btnItemCancel_Click(sender, new EventArgs());

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnItemCancel_Click(object sender, EventArgs e)
        {
            try
            {
                while (dgItemMaster.Rows.Count > 0)
                    dgItemMaster.Rows.RemoveAt(0);
                btnItemCancel.Enabled = false;
                btnApply.Enabled = false;
                dgBrand.Enabled = true;
                dgItemMaster.Enabled = false;
                dgBrand.CurrentCell = dgBrand.CurrentCell;
                dgBrand.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgItemMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    btnApply.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (dgItemMaster.CurrentCell.ColumnIndex == ColIndexItem.Location)
                    {
                        if (dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ToSlab].ReadOnly == false)
                        {
                            pnlLocation.Visible = true;
                            lstLocation.Focus();
                        }
                    }
                    //dgItemMaster.CurrentCell = dgItemMaster.CurrentCell;
                    //dgItemMaster.Focus();
                }
                if (e.KeyCode == Keys.F3)
                {
                    if (dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ToSlab].EditedFormattedValue.ToString() == ""  || dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ToSlab].ErrorText!="")
                    {
                        if (dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ToSlab].EditedFormattedValue.ToString() == "")
                        {
                            dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ToSlab].ErrorText = "Enter ToSlab Value";
                        }
                        else
                            dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ToSlab].ErrorText = "";
                    }
                    else if (dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.Location].Value.ToString() == "" || dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.Location].Value == null)
                    {
                        dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.Location].ErrorText = "Select Location";
                    }
                    else
                    {
                        dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.Location].ErrorText = "";
                        dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].ReadOnly = true;
                        dgItemMaster.Rows.Add();
                        dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex + 1].Cells[ColIndexItem.ItemNo].Value = (Convert.ToInt64(dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ItemNo].Value)).ToString();
                        dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex + 1].Cells[ColIndexItem.FkBcdNo].Value = (Convert.ToInt64(dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.FkBcdNo].Value)).ToString();
                        dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex + 1].Cells[ColIndexItem.UomNo].Value = (Convert.ToInt64(dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.UomNo].Value)).ToString();
                        dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex + 1].Cells[ColIndexItem.FromSlab].Value = (Convert.ToDouble(dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.ToSlab].Value)).ToString();
                        dgItemMaster.CurrentCell = dgItemMaster[ColIndexItem.ToSlab, dgItemMaster.CurrentCell.RowIndex + 1];
                        dgItemMaster.Focus();
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {
                    if (OMMessageBox.Show("Are you sure want to delete this row ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        DeleteDtls(1, Convert.ToInt16(dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].Cells[ColIndexItem.SettingNo].Value));
                        dgItemMaster.Rows.RemoveAt(dgItemMaster.Rows.Count - 1);
                        dgItemMaster.Rows[dgItemMaster.Rows.Count - 1].ReadOnly = false;
                    }
                    
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        

        public bool CheckValidData()
        {
            try
            {
                int cnt = 0;
                for (int i = 0; i < dgItemMaster.Rows.Count; i++)
                {
                    dgItemMaster.Rows[i].Cells[ColIndexItem.ToSlab].ErrorText = "";
                    dgItemMaster.Rows[i].Cells[ColIndexItem.Location].ErrorText = "";
                    if (dgItemMaster.Rows[i].Cells[ColIndexItem.ToSlab].EditedFormattedValue.ToString() == "")
                    {
                        dgItemMaster.Rows[i].Cells[ColIndexItem.ToSlab].ErrorText = "Enter ToSlab";
                        cnt++;
                    }
                    else if (Convert.ToDouble(dgItemMaster.Rows[i].Cells[ColIndexItem.ToSlab].Value) < Convert.ToDouble(dgItemMaster.Rows[i].Cells[ColIndexItem.FromSlab].Value))
                    {
                        dgItemMaster.Rows[i].Cells[ColIndexItem.ToSlab].ErrorText = "Enter ToSlab greater than from slab";
                        cnt++;

                    }

                    if (dgItemMaster.Rows[i].Cells[ColIndexItem.Location].EditedFormattedValue.ToString() == "")
                    {
                        dgItemMaster.Rows[i].Cells[ColIndexItem.Location].ErrorText = "Select Location";
                        cnt++;
                    }

                }
                if (cnt == 0)
                    return true;
                else
                    return false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }      

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                
                dgBrand.Enabled = true;
               
                dgBrand.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
               
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBrand_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndexBrand.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
            
        }

        private void dgItemMaster_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColIndexItem.SrNo)
                {
                    e.Value = e.RowIndex + 1;
                }
                if (e.ColumnIndex == ColIndexItem.FromSlab || e.ColumnIndex == ColIndexItem.ToSlab)
                {
                    if (e.Value != null && e.Value.ToString() != "")
                        e.Value = Convert.ToDouble(e.Value).ToString("");
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        
        private void dgItemMaster_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dgItemMaster.CurrentCell.ErrorText = "";
                if (e.ColumnIndex == ColIndexItem.ToSlab)
                {
                    if (dgItemMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "" && dgItemMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (Convert.ToDouble(dgItemMaster.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) < Convert.ToDouble(dgItemMaster.Rows[e.RowIndex].Cells[ColIndexItem.FromSlab].Value))
                        {
                            dgItemMaster.CurrentCell.ErrorText = "Enter ToSlab Greater Than FromSlab ";
                            dgItemMaster.CurrentCell = dgItemMaster[e.ColumnIndex, e.RowIndex];
                        }
                        else
                        {
                            dgItemMaster.CurrentCell = dgItemMaster[ColIndexItem.Location, e.RowIndex];
                            dgItemMaster.Focus();
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBrand_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {
                    Postion = e.RowIndex;
                    dgItemMaster.Rows.Clear();
                    if (BindItemMaster(Postion))
                    {
                        if (BtnSave.Visible == false) return;
                        dgBrand.Enabled = false;
                        dgItemMaster.Enabled = true;
                        btnApply.Enabled = true;
                        btnItemCancel.Enabled = true;
                        dgItemMaster.CurrentCell = dgItemMaster.Rows[0].Cells[ColIndexItem.ToSlab];
                        dgItemMaster.Focus();
                    }
                    else
                    {
                        btnApply.Enabled = false;
                        btnItemCancel.Enabled = false;
                        DisplayMessage("Item Not Found...");
                        dgBrand.CurrentCell = dgBrand.Rows[dgBrand.CurrentCell.RowIndex].Cells[ColIndexBrand.ItemName];
                        dgBrand.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CmbBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    dgItemMaster.Rows.Clear();
                    InitItemTable();
                    BindGrid();
                    if (dgBrand.Rows.Count > 0)
                    {
                        dgBrand.Enabled = true;
                        dgBrand.CurrentCell = dgBrand[1, 0];
                        dgBrand.Focus();
                    }
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }
            }
        }

        public void BindGrid()
        {
            try
            {
                dgBrand.Rows.Clear();
                if (ObjFunction.GetComboValue(CmbBrand) > 0)
                {
                    string str = "";

                    str = " SELECT  0 AS SrNo, MStockItems_V_1.ItemName, MStockBarcode.Barcode, MUOM.UOMName, MStockItems_V_1.ItemNo, " +
                              " MStockBarcode.PkStockBarcodeNo ,MUOM.UOMNo FROM  dbo.MStockItems_V(NULL, NULL, NULL, NULL, NULL, NULL, NULL) AS MStockItems_V_1 INNER JOIN " +
                              " MUOM ON MStockItems_V_1.UOMDefault = MUOM.UOMNo INNER JOIN MStockBarcode ON MStockItems_V_1.ItemNo = MStockBarcode.ItemNo " +
                              " where MStockItems_V_1.GroupNo=" + ObjFunction.GetComboValue(CmbBrand) + " order By MStockItems_V_1.ItemName";


                    DataTable dt = ObjFunction.GetDataView(str).Table;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgBrand.Rows.Add();
                        for (int j = 0; j < dgBrand.Columns.Count; j++)
                        {
                            dgBrand.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void lstLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.Location].ErrorText = "";
                    dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.Location].Value = lstLocation.Text;
                    dgItemMaster.Rows[dgItemMaster.CurrentCell.RowIndex].Cells[ColIndexItem.LocationNo].Value = lstLocation.SelectedValue;
                    pnlLocation.Visible = false;
                    dgItemMaster.CurrentCell = dgItemMaster[ColIndexItem.Location, dgItemMaster.CurrentCell.RowIndex];
                    dgItemMaster.Focus();
                }
                if (e.KeyCode == Keys.Space)
                {
                    pnlLocation.Visible = false;
                    dgItemMaster.CurrentCell = dgItemMaster[ColIndexItem.Location, dgItemMaster.CurrentCell.RowIndex];
                    dgItemMaster.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
           
        }

        private void CmbBrand_Leave(object sender, EventArgs e)
        {
            try
            {
                CmbBrand_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
    }
}