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
    public partial class StockOrderItemsAE : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMItemMaster dbMItemMaster = new DBMItemMaster();
        DBMStockOrderItems dbMStockOrderItems = new DBMStockOrderItems();
        MStockOrderItems mStockOrderItems = new MStockOrderItems();

        public StockOrderItemsAE()
        {
            InitializeComponent();
        }
        
        private void StockOrderItemsAE_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT MItemGroup.StockGroupNo, MItemGroup.StockGroupName  FROM   MStockGroup INNER JOIN  MStockItems ONmItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.StockGroupName");
            cmbBrandName.Focus();
            KeyDownFormat(this.Controls);
            dgItems.RowTemplate.DefaultCellStyle.Font = null;
            dgItems.Columns[ColIndex.LangFullDesc].DefaultCellStyle.Font = ObjFunction.GetLangFont();
            dgItems.Columns[ColIndex.LanguageName].DefaultCellStyle.Font = ObjFunction.GetLangFont();
        }

        private void cmbBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BindGrid();
            }
        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemShortCode = 1;
            public static int ActualStockGroupName = 2;
            public static int ItemName = 3;
            public static int LanguageName = 4;
            public static int LangFullDesc = 5;
            public static int Upload = 6;
            public static int General = 7;
            public static int IsUpload = 8;
            public static int IsGeneral = 9;
            public static int Itemno = 10;
            public static int StockGroupNo = 11;
            public static int StockOrderItemNo = 12;
            public static int TempActualStockName = 13;
            public static int TempItemShortCode = 14;
            public static int TempItemName = 15;
            public static int UOM = 16;
            public static int MRP = 17;
            public static int Stock = 18;
            public static int FKRateSettingNo = 19;
            public static int SRate = 20;
        }

        public void BindGrid()
        {

            while (dgItems.Rows.Count > 0)
                dgItems.Rows.RemoveAt(0);

            //string Sql = " SELECT 0 AS SrNo, mItemMaster.ItemShortCode, MItemGroup.ActualStockGroupName, mItemMaster.ItemName, MItemGroup.LanguageName, mItemMaster.LangFullDesc, ISNULL(MStockOrderItems.IsUpload, 'False') AS Upload, ISNULL(MStockOrderItems.IsGeneral, 'False') AS General, ISNULL(MStockOrderItems.IsUpload, 0) AS IsUpload, ISNULL(MStockOrderItems.IsGeneral, 0) AS IsGeneral, mItemMaster.ItemNo, MItemGroup.StockGroupNo, ISNULL(MStockOrderItems.StockOrderItemNo,0) AS StockOrderItemNo, " +
            //             " MItemGroup.ActualStockGroupName as TempActualStockGroupName, mItemMaster.ItemShortCode as TempItemShortCode,MStockItems.ItemName  as TempItemName " +
            //             " FROM MStockItems INNER JOIN MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo LEFT OUTER JOIN MStockOrderItems ON mItemMaster.ItemNo = MStockOrderItems.ItemNo " +
            //             " Where mItemMaster.GroupNo=" + ObjFunction.GetComboValue(cmbBrandName) + " " +
            //             " Order By mItemMaster.ItemName ";

            string Sql = " SELECT 0 AS SrNo, mItemMaster.ItemShortCode, MItemGroup.ActualStockGroupName, mItemMaster.ItemName, MItemGroup.LanguageName, mItemMaster.LangFullDesc, ISNULL(MStockOrderItems.IsUpload, 'False') AS Upload, ISNULL(MStockOrderItems.IsGeneral, 'False') AS General, ISNULL(MStockOrderItems.IsUpload, 0) AS IsUpload, ISNULL(MStockOrderItems.IsGeneral, 0) AS IsGeneral, mItemMaster.ItemNo, MItemGroup.StockGroupNo, ISNULL(MStockOrderItems.StockOrderItemNo,0) AS StockOrderItemNo, " +
                         " MItemGroup.ActualStockGroupName as TempActualStockGroupName, mItemMaster.ItemShortCode as TempItemShortCode,MStockItems.ItemName  as TempItemName,MUOM.UOMName AS UOM,MRateSetting.MRP, " +
                         " CASE WHEN mItemMaster.FKStockGroupTypeNo <>3 THEN ISNULL((MSB.CurrentStock) + (SELECT  ISNULL(SUM(MSB.CurrentStock * Case When MST.Factorval= 0 Then 1 Else MST.Factorval End), 0) " +
                         " FROM  MStockItemBalance MSB INNER JOIN MStockItems MST ON MST.ItemNo=MSB.ItemNo AND MSB.GodownNo=2 AND MST.ControlUnder=MStockItems.ItemNo),0) ELSE 0 END AS Stock,MRateSetting.PKSrNo,MRateSetting.ASaleRate aS SRate " +
                         " FROM MStockItems INNER JOIN MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo " +
                         " INNER JOIN GetItemRateAll(NULL,NULL,NULL,NULL,NULL," + ObjFunction.GetComboValue(cmbBrandName) + ") AS MRateSetting ON mItemMaster.ItemNo=MRateSetting.ItemNo LEFT OUTER JOIN MStockItemBalance MSB ON MSB.ItemNo = mItemMaster.ItemNo AND MSB.MRP = MRateSetting.MRP AND MSB.GodownNo=2 " +
                         " INNER JOIN  MUOM ON mItemMaster.UOMDefault = MUOM.UOMNo " +
                         " LEFT OUTER JOIN MStockOrderItems ON mItemMaster.ItemNo = MStockOrderItems.ItemNo AND MStockOrderItems.MRP=MRateSetting.MRP  " +
                         " Where mItemMaster.GroupNo=" + ObjFunction.GetComboValue(cmbBrandName) + " " +
                         " AND mItemMaster.IsActive='true' AND mItemMaster.UOMDefault = MRateSetting.UOMNo " +
                         " Order By mItemMaster.ItemName ";

            DataTable dtDetails = ObjFunction.GetDataView(Sql).Table;

            for (int i = 0; i < dtDetails.Rows.Count; i++)
            {
                dgItems.Rows.Add();
                for (int j = 0; j < dgItems.Columns.Count; j++)
                {
                    dgItems.Rows[i].Cells[j].Value = dtDetails.Rows[i].ItemArray[j].ToString();

                    if (j == ColIndex.Upload)
                    {
                        if (Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.Upload].EditedFormattedValue.ToString()) == true)
                            dgItems.Rows[i].Cells[ColIndex.Upload].Style.BackColor = Color.Pink;
                        else
                            dgItems.Rows[i].Cells[ColIndex.Upload].Style.BackColor = Color.White;
                    }
                    else if (j == ColIndex.General)
                    {
                        if (Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.General].EditedFormattedValue.ToString()) == true)
                            dgItems.Rows[i].Cells[ColIndex.General].Style.BackColor = Color.Pink;
                        else
                            dgItems.Rows[i].Cells[ColIndex.General].Style.BackColor = Color.White;
                    }
                    else if (j == ColIndex.MRP || j == ColIndex.Stock || j == ColIndex.SRate)
                    {
                        dgItems.Rows[i].Cells[j].Value = Convert.ToDouble(dgItems.Rows[i].Cells[j].Value).ToString("0.00");
                    }
                }
            }
            dgItems.Columns[ColIndex.UOM].DisplayIndex = ColIndex.LangFullDesc + 1;
            dgItems.Columns[ColIndex.MRP].DisplayIndex = ColIndex.LangFullDesc + 2;
            dgItems.Columns[ColIndex.Stock].DisplayIndex = ColIndex.LangFullDesc + 3;
            dgItems.Columns[ColIndex.SRate].DisplayIndex = ColIndex.LangFullDesc + 4;
            dgItems.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            while (dgItems.Rows.Count > 0)
                dgItems.Rows.RemoveAt(0);

            chkGeneralSelect.Checked = false;
            chkUploadSelect.Checked = false;
            cmbBrandName.SelectedIndex = 0;
            cmbBrandName.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = e.RowIndex + 1;
            }
            //else if (e.RowIndex > 0 && e.ColumnIndex == ColIndex.Upload)
            //{
            //    if (Convert.ToBoolean(dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].EditedFormattedValue.ToString()) == true)
            //        dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].Style.BackColor = Color.Pink;
            //    else
            //        dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].Style.BackColor = Color.White;
            //}
            //else if (e.ColumnIndex == ColIndex.General)
            //{
            //    if (Convert.ToBoolean(dgItems.Rows[e.RowIndex].Cells[ColIndex.General].EditedFormattedValue.ToString()) == true)
            //        dgItems.Rows[e.RowIndex].Cells[ColIndex.General].Style.BackColor = Color.Pink;
            //    else
            //        dgItems.Rows[e.RowIndex].Cells[ColIndex.General].Style.BackColor = Color.White;
            //}
        }

        private void chkUploadSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgItems.Rows.Count; i++)
            {
                dgItems.Rows[i].Cells[ColIndex.Upload].Value = chkUploadSelect.Checked;
                if (chkUploadSelect.Checked == true)
                    dgItems.Rows[i].Cells[ColIndex.Upload].Style.BackColor = Color.Pink;
                else
                    dgItems.Rows[i].Cells[ColIndex.Upload].Style.BackColor = Color.White;
            }
        }

        private void chkGeneralSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dgItems.Rows.Count; i++)
            {
                dgItems.Rows[i].Cells[ColIndex.General].Value = chkGeneralSelect.Checked;
                if (chkGeneralSelect.Checked == true)
                    dgItems.Rows[i].Cells[ColIndex.General].Style.BackColor = Color.Pink;
                else
                    dgItems.Rows[i].Cells[ColIndex.General].Style.BackColor = Color.White;
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
            if (e.KeyCode == Keys.F2)
            {
                chkUploadSelect.Checked = !chkUploadSelect.Checked;
            }
            else if (e.KeyCode == Keys.F3)
            {
                chkGeneralSelect.Checked = !chkGeneralSelect.Checked;

            }
            else if (e.KeyCode == Keys.F5)//Next
            {
                int bcnt = cmbBrandName.SelectedIndex;
                int itemCnt = cmbBrandName.Items.Count-1;
                int SetCnt = 0;
                if (bcnt < itemCnt)
                {
                    SetCnt = bcnt + 1;
                }
                if (SetCnt != 0)
                {
                    cmbBrandName.SelectedIndex = SetCnt;
                    BindGrid();
                }

            }
            else if (e.KeyCode == Keys.F6)//Previous
            {
                int bcnt = cmbBrandName.SelectedIndex;
                int SetCnt = 0;
                if (bcnt>0)
                {
                    SetCnt = bcnt - 1;
                }
                if (SetCnt != 0)
                {
                    cmbBrandName.SelectedIndex = SetCnt;
                    BindGrid();
                }

            }
        }
        #endregion

        public void DisplayMessage(string str)
        {
            try
            {
                lblMsg.Visible = true;
                lblMsg.Text = str;
                Application.DoEvents();
                System.Threading.Thread.Sleep(1200);
                lblMsg.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dbMStockOrderItems = new DBMStockOrderItems();
            bool flag = false;
            for (int i = 0; i < dgItems.Rows.Count; i++)
            {

                if (Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.Upload].EditedFormattedValue.ToString()) == false && Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.General].EditedFormattedValue.ToString()) == false)
                {
                    if (Convert.ToInt64(dgItems.Rows[i].Cells[ColIndex.StockOrderItemNo].EditedFormattedValue.ToString()) != 0)
                    {
                        mStockOrderItems = new MStockOrderItems();
                        mStockOrderItems.StockOrderItemNo = Convert.ToInt64(dgItems.Rows[i].Cells[ColIndex.StockOrderItemNo].EditedFormattedValue.ToString());
                        dbMStockOrderItems.DeleteMStockOrderItems(mStockOrderItems);
                        flag = true;
                    }
                }
                else
                {

                    if (Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.Upload].EditedFormattedValue.ToString()) != Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.IsUpload].EditedFormattedValue.ToString())
                        || Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.General].EditedFormattedValue.ToString()) != Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.IsGeneral].EditedFormattedValue.ToString()))
                    {
                        mStockOrderItems = new MStockOrderItems();
                        mStockOrderItems.StockOrderItemNo = Convert.ToInt64(dgItems.Rows[i].Cells[ColIndex.StockOrderItemNo].EditedFormattedValue.ToString());
                        mStockOrderItems.IsUpload = Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.Upload].EditedFormattedValue.ToString());
                        mStockOrderItems.IsGeneral = Convert.ToBoolean(dgItems.Rows[i].Cells[ColIndex.General].EditedFormattedValue.ToString());
                        mStockOrderItems.MRP = Convert.ToDouble(dgItems.Rows[i].Cells[ColIndex.MRP].EditedFormattedValue.ToString());
                        mStockOrderItems.FKRateSettingNo = Convert.ToInt64(dgItems.Rows[i].Cells[ColIndex.FKRateSettingNo].EditedFormattedValue.ToString());
                        mStockOrderItems.ItemNo = Convert.ToInt64(dgItems.Rows[i].Cells[ColIndex.Itemno].EditedFormattedValue.ToString());
                        mStockOrderItems.CompanyNo = DBGetVal.FirmNo;
                        mStockOrderItems.UserDate = DBGetVal.ServerTime.Date;
                        mStockOrderItems.UserID = DBGetVal.UserID;
                        dbMStockOrderItems.AddMStockOrderItems(mStockOrderItems);
                        flag = true;
                    }
                }

            }

            if (flag == true)
            {
                if (dbMStockOrderItems.ExecuteNonQueryStatements1())
                {
                    DisplayMessage("Item Save Successfully..");
                    BindGrid();
                }
                else
                    DisplayMessage("Item Not Save");

            }
        }

        private void dgItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.ActualStockGroupName)
            {
                string ActualName = dgItems.Rows[e.RowIndex].Cells[ColIndex.ActualStockGroupName].EditedFormattedValue.ToString().Trim().ToUpper();
                if (ActualName != dgItems.Rows[e.RowIndex].Cells[ColIndex.TempActualStockName].EditedFormattedValue.ToString().Trim().ToUpper())
                {

                    if (ObjTrans.Execute("Update MStockGroup Set ActualStockGroupName='" + ActualName + "',StatusNo=2 Where  StockGroupNo=" + dgItems.Rows[e.RowIndex].Cells[ColIndex.StockGroupNo].EditedFormattedValue.ToString() + "", CommonFunctions.ConStr))
                    {
                        for (int i = 0; i < dgItems.Rows.Count; i++)
                        {
                            dgItems.Rows[i].Cells[ColIndex.ActualStockGroupName].Value = ActualName;
                            dgItems.Rows[i].Cells[ColIndex.TempActualStockName].Value = ActualName;
                        }
                    }
                }
                dgItems.Focus();
            }
            else if (e.ColumnIndex == ColIndex.ItemShortCode)
            {

                string ItemCode = dgItems.Rows[e.RowIndex].Cells[ColIndex.ItemShortCode].EditedFormattedValue.ToString().Trim().ToUpper();
                if (ItemCode != "")
                {
                    if (ItemCode != dgItems.Rows[e.RowIndex].Cells[ColIndex.TempItemShortCode].EditedFormattedValue.ToString().Trim().ToUpper())
                    {
                        if (ObjTrans.Execute("Update MStockItems Set ItemShortCode='" + ItemCode + "',StatusNo=2 Where  ItemNo=" + dgItems.Rows[e.RowIndex].Cells[ColIndex.Itemno].EditedFormattedValue.ToString() + "", CommonFunctions.ConStr))
                        {
                            dgItems.Rows[e.RowIndex].Cells[ColIndex.ItemShortCode].Value = ItemCode;
                            dgItems.Rows[e.RowIndex].Cells[ColIndex.TempItemShortCode].Value = ItemCode;
                        }
                    }
                }
                dgItems.Focus();
            }
            else if (e.ColumnIndex == ColIndex.ItemName)
            {
                string ItemNames = dgItems.Rows[e.RowIndex].Cells[ColIndex.ItemName].EditedFormattedValue.ToString().Trim().ToUpper();
                if (ItemNames != "")
                {
                    if (ItemNames != dgItems.Rows[e.RowIndex].Cells[ColIndex.TempItemName].EditedFormattedValue.ToString().Trim().ToUpper())
                    {
                        if (ObjTrans.Execute("Update MStockItems Set ItemName='" + ItemNames + "',StatusNo=2 Where  ItemNo=" + dgItems.Rows[e.RowIndex].Cells[ColIndex.Itemno].EditedFormattedValue.ToString() + "", CommonFunctions.ConStr))
                        {
                            dgItems.Rows[e.RowIndex].Cells[ColIndex.ItemName].Value = ItemNames;
                            dgItems.Rows[e.RowIndex].Cells[ColIndex.TempItemName].Value = ItemNames;
                        }

                    }
                }
                dgItems.Focus();
            }
        }

        private void dgItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.Upload)
            {
                if (Convert.ToBoolean(dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].EditedFormattedValue.ToString()) == true)
                    dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].Style.BackColor = Color.Pink;
                else
                    dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].Style.BackColor = Color.White;
            }
            else if (e.ColumnIndex == ColIndex.General)
            {
                if (Convert.ToBoolean(dgItems.Rows[e.RowIndex].Cells[ColIndex.General].EditedFormattedValue.ToString()) == true)
                {
                    dgItems.Rows[e.RowIndex].Cells[ColIndex.General].Style.BackColor = Color.Pink;
                    dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].Value = "true";
                    dgItems.Rows[e.RowIndex].Cells[ColIndex.Upload].Style.BackColor = Color.Pink;
                }
                else
                {
                    dgItems.Rows[e.RowIndex].Cells[ColIndex.General].Style.BackColor = Color.White;
                }
                
            }
        }
    }
}
