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
    public partial class ItemRateChange : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        MRateSetting mRateSettig = new MRateSetting();

        DBMItemMaster dbMItemMaster = new DBMItemMaster();
        public ItemRateChange()
        {
            InitializeComponent();
        }

        private void ItemRateChange_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbMainGroup, "SELECT ItemGroupNo,ItemGroupName From MItemGroup WHERE IsActive = 'True' AND ControlGroup=2 ORDER BY ItemGroupName");
                ObjFunction.FillCombo(cmbBrandName, "SELECT DISTINCT MItemGroup.ItemGroupNo, MItemGroup.ItemGroupName  FROM   MItemGroup INNER JOIN  MItemMaster ON mItemGroup.ItemGroupNo  =  mItemMaster.GroupNo  WHERE  (MItemGroup.IsActive = 'True') AND (MItemGroup.ControlGroup = 3) ORDER BY MItemGroup.ItemGroupName");
                ObjFunction.FillComb(cmbItemName, "ItemNo", "ItemName");

                DisplayColumns();
                cmbMainGroup.Focus();
                txtTotProducts.Text = "0";
                btnCancel.Visible = true;
                KeyDownFormat(this.Controls);
                gvRateSetting.Columns[ColIndex.MRP].ReadOnly = true;
                gvRateSetting.Columns[0].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                gvRateSetting.Columns[4].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                for (int i = 6; i < 14; i++)
                {
                    gvRateSetting.Columns[i].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
                }
                gvRateSetting.Columns[14].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                gvRateSetting.Columns[19].HeaderCell.Style.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                {
                    gvRateSetting.Columns[ColIndex.LangFullDesc].Visible = true;
                    gvRateSetting.RowTemplate.DefaultCellStyle.Font = null;
                    gvRateSetting.Columns[ColIndex.LangFullDesc].DefaultCellStyle.Font = ObjFunction.GetLangFont();
                }
                else
                    gvRateSetting.Columns[ColIndex.LangFullDesc].Visible = false;

                DisplayStatus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayStatus()
        {
            try
            {
                long cntActive, cntDeActive;
                cntActive = ObjQry.ReturnLong("Select Count(*) From MStockItems Where IsActive='true' and ItemNo<>1", CommonFunctions.ConStr);
                cntDeActive = ObjQry.ReturnLong("Select Count(*) From MStockItems Where IsActive='false' and ItemNo<>1", CommonFunctions.ConStr);
                lblStatusA.Text = "Active Items : " + cntActive.ToString();
                lblStatusD.Text = "Deactive Items : " + cntDeActive.ToString();
                lblStatusA.ForeColor = Color.Blue;
                lblStatusD.ForeColor = Color.Red;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool ValidationsForDouplicateItems()
        {
            bool flag = true;
            MovetoNext move2n = new MovetoNext(m2n);
            for (int ii = 0; ii < gvRateSetting.Rows.Count; ii++)
            {
                if (gvRateSetting.Rows[ii].Cells[ColIndex.ItemName].FormattedValue.ToString().Trim() == "")
                {
                    gvRateSetting.Rows[ii].Cells[ColIndex.ItemName].ErrorText = "Enter Item Name";
                    BeginInvoke(move2n, new object[] { ii, ColIndex.ItemName, gvRateSetting });
                    return false;
                }
                else
                    gvRateSetting.Rows[ii].Cells[ColIndex.ItemName].ErrorText = "";
            }

            for (int ii = 0; ii < gvRateSetting.Rows.Count; ii++)
            {
                for (int i = ii + 1; i < gvRateSetting.Rows.Count; i++)
                {
                    if (i != ii && gvRateSetting.Rows[ii].Cells[ColIndex.ItemName].FormattedValue.ToString().Trim().ToUpper() == gvRateSetting.Rows[i].Cells[ColIndex.ItemName].FormattedValue.ToString().Trim().ToUpper())
                    {
                        gvRateSetting.Rows[ii].Cells[ColIndex.ItemName].ErrorText = "Duplicate Item Name";
                        gvRateSetting.Rows[i].Cells[ColIndex.ItemName].ErrorText = "Duplicate Item Name";
                        if (flag)
                        {
                            BeginInvoke(move2n, new object[] { ii, ColIndex.ItemName, gvRateSetting });
                            flag = false;
                        }
                    }
                }
            }


            return flag;
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validations() == true)
                {
                    dbMItemMaster = new DBMItemMaster();
                    if (rdActive.Checked == true || rdDeactive.Checked == true)
                    {
                        for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                        {
                            if (gvRateSetting.Rows[i].Cells[ColIndex.UOM].Value != null && gvRateSetting.Rows[i].Cells[ColIndex.ASaleRate].Value.ToString() != "0"
                                && gvRateSetting.Rows[i].Cells[ColIndex.BarCode].Value != null && gvRateSetting.Rows[i].Cells[ColIndex.BSaleRate].Value.ToString() != "0"
                                && gvRateSetting.Rows[i].Cells[ColIndex.CSaleRate].Value.ToString() != null
                                && gvRateSetting.Rows[i].Cells[ColIndex.DSaleRate].Value.ToString() != null && gvRateSetting.Rows[i].Cells[ColIndex.ESaleRate].Value.ToString() != null
                                && gvRateSetting.Rows[i].Cells[ColIndex.MKTQty].Value != null && gvRateSetting.Rows[i].Cells[ColIndex.MRP].Value.ToString() != "0"
                                && gvRateSetting.Rows[i].Cells[ColIndex.RateVariation].Value.ToString() != null
                                && gvRateSetting.Rows[i].Cells[ColIndex.StockConversion].Value.ToString() != "0" && gvRateSetting.Rows[i].Cells[ColIndex.Date].Value.ToString() != "0"
                                && gvRateSetting.Rows[i].Cells[ColIndex.MRP].ErrorText == "" && gvRateSetting.Rows[i].Cells[ColIndex.UOMNAme].ErrorText == ""
                                && gvRateSetting.Rows[i].Cells[ColIndex.ItemName].ErrorText == "" && gvRateSetting.Rows[i].Cells[ColIndex.LangFullDesc].ErrorText == ""
                                && (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value)) == true)
                            {

                                //Change ItemName And ItemLangFullDesc

                                if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.ItemChk].FormattedValue) == true)
                                {
                                    MItemMaster mItemMaster = new MItemMaster();
                                    mItemMaster.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);
                                    mItemMaster.ItemName = Convert.ToString(gvRateSetting.Rows[i].Cells[ColIndex.ItemName].Value);
                                    mItemMaster.LangFullDesc = Convert.ToString(gvRateSetting.Rows[i].Cells[ColIndex.LangFullDesc].Value);
                                    //  dbMItemMaster.UpdateStockItemNameAndDesc(mItemMaster);
                                }


                                if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                                {
                                    mRateSettig = new MRateSetting();
                                    if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsActive].FormattedValue) == true)
                                    {
                                        if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.HidChk].FormattedValue) == false)
                                        {
                                            long RatePk = (gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value == null) ? 0 : Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value);
                                            mRateSettig.PkSrNo = RatePk;
                                            mRateSettig.FromDate = Convert.ToDateTime(gvRateSetting.Rows[i].Cells[ColIndex.Date].Value);
                                        }
                                        else
                                        {
                                            long RatePk = (gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value == null) ? 0 : Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value);
                                            if (RatePk != 0)
                                            {
                                                //if (ObjQry.ReturnLong("SELECT PkSrNo FROM MRateSetting WHERE (PkSrNo = " + RatePk + ") AND (UOMNo =" + Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.UOM].Value) + " ) AND (MRP = " + gvRateSetting.Rows[i].Cells[ColIndex.MRP].Value + ")", CommonFunctions.ConStr) != 0)
                                                //{
                                                mRateSettig.PkSrNo = RatePk;
                                                if (ObjQry.ReturnLong("SELECT PkSrNo FROM MRateSetting WHERE (PkSrNo = " + RatePk + ")  AND (MRP = " + gvRateSetting.Rows[i].Cells[ColIndex.MRP].Value + ")", CommonFunctions.ConStr) == 0)
                                                {
                                                    MRateSetting mRateSettings = new MRateSetting();
                                                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsCloseMRPManually)) == false)
                                                    {
                                                        mRateSettings.PkSrNo = RatePk;
                                                        mRateSettings.IsActive = false;
                                                        mRateSettig.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);
                                                        //  dbMItemMaster.UpdateMRateSetting(mRateSettings);
                                                    }
                                                    mRateSettig.PkSrNo = 0;
                                                }
                                            }

                                            mRateSettig.FromDate = DBGetVal.ServerTime;


                                        }
                                        mRateSettig.FkBcdSrNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.BarCodeNo].Value);
                                        mRateSettig.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);

                                        mRateSettig.PurRate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.PurRate].Value);
                                        mRateSettig.MRP = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.MRP].Value);
                                        mRateSettig.UOMNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.UOM].Value);
                                        mRateSettig.ASaleRate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.ASaleRate].Value);
                                        mRateSettig.BSaleRate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.BSaleRate].Value);
                                        mRateSettig.CSaleRate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.CSaleRate].Value);
                                        mRateSettig.DSaleRate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.DSaleRate].Value);
                                        mRateSettig.ESaleRate = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.ESaleRate].Value);
                                        mRateSettig.StockConversion = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.StockConversion].Value);
                                        mRateSettig.PerOfRateVariation = Convert.ToDouble(gvRateSetting.Rows[i].Cells[ColIndex.RateVariation].Value);
                                        mRateSettig.MKTQty = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.MKTQty].Value);
                                        mRateSettig.IsActive = Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsActive].Value);
                                        mRateSettig.UserDate = DBGetVal.ServerTime.Date;
                                        mRateSettig.UserID = DBGetVal.UserID;
                                        mRateSettig.CompanyNo = DBGetVal.FirmNo;
                                        dbMItemMaster.AddMRateSetting1(mRateSettig);
                                    }
                                    else
                                    {
                                        mRateSettig.PkSrNo = (gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value == null) ? 0 : Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value);
                                        mRateSettig.IsActive = Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsActive].Value);
                                        mRateSettig.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);
                                        //  dbMItemMaster.UpdateMRateSetting(mRateSettig);
                                    }
                                }
                            }
                        }
                        if (dbMItemMaster.ExecuteNonQueryStatements() == true)
                        {
                            DisplayMessage("Item Save Successfully...");
                            ChkSelect.Checked = false;
                            btnCancel_Click(sender, e);
                        }
                    }
                    else if (rdItemOnOff.Checked == true)
                    {
                        if (ValidationsForDouplicateItems())
                        {
                            if (OMMessageBox.Show("Are you sure want to save this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                dbMItemMaster = new DBMItemMaster();
                                MItemMaster mItemMaster = new MItemMaster();
                                for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                                {
                                    if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.ItemChk].FormattedValue) == true)
                                    {
                                        mItemMaster = new MItemMaster();
                                        mItemMaster.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);
                                        mItemMaster.ItemName = Convert.ToString(gvRateSetting.Rows[i].Cells[ColIndex.ItemName].Value);
                                        mItemMaster.LangFullDesc = Convert.ToString(gvRateSetting.Rows[i].Cells[ColIndex.LangFullDesc].Value);
                                        // dbMItemMaster.UpdateStockItemNameAndDesc(mItemMaster);
                                    }
                                    if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                                    {
                                        mItemMaster = new MItemMaster();
                                        mItemMaster.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);
                                        mItemMaster.IsActive = Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsActive].FormattedValue);
                                        dbMItemMaster.UpdateMItemMasterIsActive(mItemMaster);
                                    }
                                }
                                if (dbMItemMaster.ExecuteNonQueryStatements() == true)
                                {
                                    DisplayMessage("Item Save Successfully...");
                                    ChkSelect.Checked = false;
                                    btnCancel_Click(sender, e);
                                }
                            }
                        }

                    }
                    else if (rdMrpchange.Checked == true)
                    {
                        for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true)
                            {
                                mRateSettig = new MRateSetting();
                                mRateSettig.PkSrNo = (gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value == null) ? 0 : Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.PkSrNo].Value);
                                mRateSettig.IsActive = Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsActive].Value);
                                mRateSettig.ItemNo = Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].Value);
                                dbMItemMaster.UpdateMRateSetting(mRateSettig);
                            }

                        }
                        if (dbMItemMaster.ExecuteNonQueryStatements() == true)
                        {
                            DisplayMessage("Item Save Successfully...");
                            ChkSelect.Checked = false;
                            btnCancel_Click(sender, e);
                        }

                    }
                    DisplayStatus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ChkSelect.Checked = false;
            while (gvRateSetting.Rows.Count > 0)
            {
                gvRateSetting.Rows.RemoveAt(0);
            }
            if (cmbItemName.SelectedIndex >= 0)
                cmbItemName.SelectedIndex = 0;
            if (cmbMainGroup.SelectedIndex >= 0)
                cmbMainGroup.SelectedIndex = 0;
            if (cmbBrandName.SelectedIndex >= 0)
                cmbBrandName.SelectedIndex = 0;
            txtBarcode.Text = "";

            lstUom.Visible = false;
            rdActive.Checked = true;
            CalculateTotProducts();
            //cmbMainGroup.Focus();
            cmbBrandName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BindGrid()
        {
            try
            {
                string strWhere = "";
                string sql = "";
                if (rdDeactive.Checked == true)
                {
                    ColumnsStatus();

                    sql = "SELECT  0 AS srNo,CASE WHEN ESFLAG='True' THEN   mItemMaster.ItemName ELSE mItemMaster.ItemName + ' *' END AS ItemName,mItemMaster.LangFullDesc, MRateSetting.FromDate, mItemMaster.Barcode, MRateSetting.MRP, " +
                        " MUOM.UOMName, MRateSetting.ASaleRate,  MRateSetting.BSaleRate, MRateSetting.CSaleRate, MRateSetting.DSaleRate, MRateSetting.ESaleRate," +
                        " MRateSetting.MKTQty, MRateSetting.PurRate,  MRateSetting.PerOfRateVariation as NoOfUnit,  MRateSetting.Stock ,MRateSetting.IsActive, MRateSetting.StockConversion," +
                        " MRateSetting.PkSrNo,  mItemMaster.ItemNo as PkStockBarcodeNo,  mItemMaster.ItemNo, 'false' AS chk, MRateSetting.IsActive AS HidChk, MUOM.UOMNo,'false' As ItemChk  " +

                         " FROM MUOM INNER JOIN " +
                         " dbo.GetItemRateAllWithStock(NULL, " + (txtBarcode.Text.Trim().Equals("") ? "NULL" : "(Select min(mItemMaster.ItemNo) FROM mItemMaster INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo Where (Barcode='" + txtBarcode.Text.Trim().Replace("'", "''") + "' or mItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "'))") + ", NULL, NULL, NULL, " + (ObjFunction.GetComboValue(cmbBrandName) == 0 ? "NULL" : ObjFunction.GetComboValue(cmbBrandName).ToString()) + ") AS MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo  " +
                         //" MStockBarcode.PkStockBarcodeNo = MRateSetting.FkBcdSrNo INNER JOIN " +
                         " INNER JOIN mItemMaster ON MRateSetting.ItemNo = mItemMaster.ItemNo " +
                         " Where mItemMaster.IsActive='False' And ";

                    gvRateSetting.Columns[ColIndex.ItemName].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.LangFullDesc].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.MRP].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.ASaleRate].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.CSaleRate].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.DSaleRate].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.BSaleRate].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.PurRate].ReadOnly = true;
                    //gvRateSetting.Columns[ColIndex.RateVariatio].Visible = true;
                    //gvRateSetting.Columns[ColIndex.RateVariation].Visible = true;

                }

                else if (rdActive.Checked == true)
                {
                    ColumnsStatus();
                    if (DBGetVal.KachhaFirm == false)
                    {
                        sql = "SELECT  0 AS srNo, CASE WHEN ESFLAG='True' THEN   mItemMaster.ItemName ELSE mItemMaster.ItemName + ' *' END AS ItemName,mItemMaster.LangFullDesc, MRateSetting.FromDate, mItemMaster.Barcode, MRateSetting.MRP, " +
                            " MUOM.UOMName, MRateSetting.ASaleRate,  MRateSetting.BSaleRate, MRateSetting.CSaleRate, MRateSetting.DSaleRate, MRateSetting.ESaleRate," +
                            " MRateSetting.MKTQty, MRateSetting.PurRate, isnull( MRateSetting.PerOfRateVariation,0) as NoOfUnit,  MRateSetting.Stock ,MRateSetting.IsActive, MRateSetting.StockConversion," +
                             " MRateSetting.PkSrNo,  mItemMaster.ItemNo as PkStockBarcodeNo,  mItemMaster.ItemNo, 'false' AS chk, MRateSetting.IsActive AS HidChk, MUOM.UOMNo,'false' As ItemChk  " +

                         " FROM MUOM INNER JOIN " +
                         " dbo.GetItemRateAllWithStock(NULL, " + (txtBarcode.Text.Trim().Equals("") ? "NULL" : "(Select min(mItemMaster.ItemNo) FROM mItemMaster INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo Where (Barcode='" + txtBarcode.Text.Trim().Replace("'", "''") + "' or mItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "'))") + ", NULL, NULL, NULL, " + (ObjFunction.GetComboValue(cmbBrandName) == 0 ? "NULL" : ObjFunction.GetComboValue(cmbBrandName).ToString()) + ") AS MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo  " +
                         //" MStockBarcode.PkStockBarcodeNo = MRateSetting.FkBcdSrNo INNER JOIN " +
                         " INNER JOIN mItemMaster ON MRateSetting.ItemNo = mItemMaster.ItemNo " +
                         " Where mItemMaster.IsActive='True' And ";

                    }
                    else
                    {
                        sql = "SELECT  0 AS srNo, CASE WHEN ESFLAG='True' THEN   mItemMaster.ItemName ELSE mItemMaster.ItemName + ' *' END AS ItemName,mItemMaster.LangFullDesc, MRateSetting.FromDate, mItemMaster.Barcode, MRateSetting.MRP, " +
                            " MUOM.UOMName, MRateSetting.ASaleRate,  MRateSetting.BSaleRate, MRateSetting.CSaleRate, MRateSetting.DSaleRate, MRateSetting.ESaleRate," +
                            " MRateSetting.MKTQty, MRateSetting.PurRate, isnull( MRateSetting.PerOfRateVariation,0) as NoOfUnit,  MRateSetting.Stock2 as Stock ,MRateSetting.IsActive, MRateSetting.StockConversion," +
                             " MRateSetting.PkSrNo,  mItemMaster.ItemNo as PkStockBarcodeNo,  mItemMaster.ItemNo, 'false' AS chk, MRateSetting.IsActive AS HidChk, MUOM.UOMNo,'false' As ItemChk  " +

                         " FROM MUOM INNER JOIN " +
                         // " dbo.GetItemRateAllWithStock(NULL, " + (txtBarcode.Text.Trim().Equals("") ? "NULL" : "(Select min(mItemMaster.ItemNo) FROM mItemMaster INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo Where (Barcode='" + txtBarcode.Text.Trim().Replace("'", "''") + "' or mItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "'))") + ", NULL, NULL, NULL, " + (ObjFunction.GetComboValue(cmbBrandName) == 0 ? "NULL" : ObjFunction.GetComboValue(cmbBrandName).ToString()) + ") AS MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo  " +
                         //" MStockBarcode.PkStockBarcodeNo = MRateSetting.FkBcdSrNo INNER JOIN " +
                         " MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo  INNER JOIN mItemMaster ON MRateSetting.ItemNo = mItemMaster.ItemNo " +
                         " Where mItemMaster.IsActive='True' And ";

                    }
                    gvRateSetting.Columns[ColIndex.ItemName].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.LangFullDesc].ReadOnly = true;
                    // gvRateSetting.Columns[ColIndex.MRP].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.ASaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.CSaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.DSaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.BSaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.PurRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.Chk].Visible = true;
                    gvRateSetting.Columns[ColIndex.MKTQty].Visible = true;
                    gvRateSetting.Columns[ColIndex.MKTQty].ReadOnly = false;
                }

                else if (rdItemOnOff.Checked == true)
                {
                    sql = " SELECT 0 AS srNo, CASE WHEN ESFLAG='True' THEN   mItemMaster.ItemName ELSE mItemMaster.ItemName + ' *' END AS ItemName, mItemMaster.LangFullDesc, 0 AS FromDate, mItemMaster.Barcode, 0 AS MRP, 0 AS UOMName, 0 AS ASaleRate,  " +
                        " 0 AS BSaleRate, 0 AS CSaleRate, 0 AS DSaleRate, 0 AS ESaleRate, 0 AS MKTQty, 0 AS PurRate, 0 AS PerOfRateVariation,0 As Stock, mItemMaster.IsActive AS IsActive, 0 AS StockConversion,  0 AS PkSrNo, 0 AS PkStockBarcodeNo, mItemMaster.ItemNo, 'False' AS chk, 0 AS HidChk, 0 AS UOMNo ,'false' As ItemChk" +
                        " FROM mItemMaster " +
                        " WHERE ";

                    gvRateSetting.Columns[ColIndex.ItemName].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.LangFullDesc].ReadOnly = false;

                }
                else if (rdMrpchange.Checked == true)
                {
                    ColumnsStatus();

                    sql = "SELECT  0 AS srNo, CASE WHEN ESFLAG='True' THEN   mItemMaster.ItemName ELSE mItemMaster.ItemName + ' *' END AS ItemName,mItemMaster.LangFullDesc, MRateSetting.FromDate, mItemMaster.Barcode, MRateSetting.MRP, " +
                " MUOM.UOMName, MRateSetting.ASaleRate,  MRateSetting.BSaleRate, MRateSetting.CSaleRate, MRateSetting.DSaleRate, MRateSetting.ESaleRate," +
                " MRateSetting.MKTQty, MRateSetting.PurRate, isnull( MRateSetting.PerOfRateVariation,0) as NoOfUnit,  MRateSetting.Stock ,MRateSetting.IsActive, MRateSetting.StockConversion," +
                " MRateSetting.PkSrNo,  mItemMaster.ItemNo as PkStockBarcodeNo,  mItemMaster.ItemNo, 'false' AS chk, MRateSetting.IsActive AS HidChk, MUOM.UOMNo,'false' As ItemChk  " +

                     " FROM MUOM INNER JOIN " +
                     " dbo.GetItemRateAllWithStock(NULL, " + (txtBarcode.Text.Trim().Equals("") ? "NULL" : "(Select min(mItemMaster.ItemNo) FROM mItemMaster INNER JOIN MRateSetting ON mItemMaster.ItemNo = MRateSetting.ItemNo Where (Barcode='" + txtBarcode.Text.Trim().Replace("'", "''") + "' or mItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "'))") + ", NULL, NULL, NULL, " + (ObjFunction.GetComboValue(cmbBrandName) == 0 ? "NULL" : ObjFunction.GetComboValue(cmbBrandName).ToString()) + ") AS MRateSetting ON MUOM.UOMNo = MRateSetting.UOMNo  " +
                     //" MStockBarcode.PkStockBarcodeNo = MRateSetting.FkBcdSrNo INNER JOIN " +
                     " INNER JOIN mItemMaster ON MRateSetting.ItemNo = mItemMaster.ItemNo " +
                     " Where mItemMaster.IsActive='True' And ";


                    gvRateSetting.Columns[ColIndex.ItemName].ReadOnly = true;
                    gvRateSetting.Columns[ColIndex.LangFullDesc].ReadOnly = true;
                    // gvRateSetting.Columns[ColIndex.MRP].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.ASaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.CSaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.DSaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.BSaleRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.PurRate].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.Chk].Visible = true;
                    gvRateSetting.Columns[ColIndex.MKTQty].Visible = true;
                    gvRateSetting.Columns[ColIndex.MKTQty].ReadOnly = false;
                    gvRateSetting.Columns[ColIndex.IsActive].ReadOnly = false;

                }

                if (ObjFunction.GetComboValue(cmbBrandName) != 0)
                {
                    strWhere += " ( mItemMaster.GroupNo = " + ObjFunction.GetComboValue(cmbBrandName) + ")";
                }

                if (txtBarcode.Text != "")
                {
                    if (ObjFunction.GetComboValue(cmbBrandName) != 0)
                        strWhere += " And (mItemMaster.Barcode ='" + txtBarcode.Text.Trim().Replace("'", "''") + "' or mItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "')";
                    else
                        strWhere += " (mItemMaster.Barcode ='" + txtBarcode.Text.Trim().Replace("'", "''") + "' or mItemMaster.ShortCode='" + txtBarcode.Text.Trim().Replace("'", "''") + "')";
                }
                sql += strWhere + " order by mItemMaster.ItemName";
                DataTable dt = new DataTable();
                if (strWhere != "")
                    dt = ObjFunction.GetDataView(sql).Table;
                gvRateSetting.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {


                        gvRateSetting.Rows.Add();
                        for (int j = 0; j < gvRateSetting.Columns.Count; j++)
                        {
                            if (j == ColIndex.IsActive)
                                gvRateSetting.Rows[i].Cells[ColIndex.IsActive].Value = false;

                            if (j == ColIndex.HidChk)
                                gvRateSetting.Rows[i].Cells[ColIndex.HidChk].Value = false;

                            gvRateSetting.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                        }
                        gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = "False";
                    }
                    if (gvRateSetting.Rows.Count > 0)
                    {
                        gvRateSetting.CurrentCell = gvRateSetting[ColIndex.ItemName, 0];
                        gvRateSetting.Focus();
                    }
                    if (rdItemOnOff.Checked == true)
                        gvRateSetting.Columns[ColIndex.IsActive].ReadOnly = false;
                    else gvRateSetting.Columns[ColIndex.IsActive].ReadOnly = true;
                }
                else
                {
                    //DisplayMessage("Records not found...!!");
                    while (gvRateSetting.Rows.Count > 0)
                    {
                        gvRateSetting.Rows.RemoveAt(0);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbMainGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cmbMainGroup.SelectedIndex > 0)
                    {
                        BindGrid();
                        CalculateTotProducts();
                        ObjFunction.FillCombo(cmbBrandName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
                    }
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
            public static int Sr = 0;
            public static int ItemName = 1;
            public static int LangFullDesc = 2;
            public static int Date = 3;
            public static int BarCode = 4;
            public static int MRP = 5;
            public static int UOMNAme = 6;
            public static int ASaleRate = 7;
            public static int BSaleRate = 8;
            public static int CSaleRate = 9;
            public static int DSaleRate = 10;
            public static int ESaleRate = 11;
            public static int MKTQty = 12;
            public static int PurRate = 13;
            public static int RateVariation = 14;
            public static int Stock = 15;
            public static int IsActive = 16;
            public static int StockConversion = 17;
            public static int PkSrNo = 18;
            public static int BarCodeNo = 19;
            public static int ItemNo = 20;
            public static int Chk = 21;
            public static int HidChk = 22;
            public static int UOM = 23;
            public static int ItemChk = 24;

        }
        #endregion

        private void cmbBrandName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (cmbBrandName.SelectedIndex >= 0)
                    {
                        BindGrid();
                        CalculateTotProducts();
                        ObjFunction.FillCombo(cmbItemName, "SELECT ItemNo,ItemName From MItemMaster WHERE GroupNo=" + ObjFunction.GetComboValue(cmbBrandName) + " ORDER BY ItemName");
                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void CalculateTotProducts()
        {
            txtTotProducts.Text = Convert.ToString(gvRateSetting.Rows.Count);
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void cmbMainGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                ObjFunction.FillCombo(cmbBrandName, "SELECT StockGroupNo,StockGroupName From MStockGroup WHERE IsActive = 'True' AND ControlGroup=3 ORDER BY StockGroupName");
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void gvRateSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            if (e.ColumnIndex == ColIndex.ASaleRate)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDouble(e.Value).ToString("0.00");
            }
            if (e.ColumnIndex == ColIndex.BSaleRate)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDouble(e.Value).ToString("0.00");
            }
            if (e.ColumnIndex == ColIndex.CSaleRate)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDouble(e.Value).ToString("0.00");
            }
            if (e.ColumnIndex == ColIndex.DSaleRate)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDouble(e.Value).ToString("0.00");
            }
            if (e.ColumnIndex == ColIndex.ESaleRate)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDouble(e.Value).ToString("0.00");
            }
            if (e.ColumnIndex == ColIndex.MRP)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDouble(e.Value).ToString("0.00");
            }
            if (e.ColumnIndex == ColIndex.PurRate)
            {
                if (e.Value != null)
                    e.Value = Convert.ToDouble(e.Value).ToString("0.00");
            }

        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
        }

        private void gvRateSetting_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ASaleRate].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ASaleRate].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.BSaleRate].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.BSaleRate].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.CSaleRate].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.CSaleRate].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.DSaleRate].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.DSaleRate].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ESaleRate].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ESaleRate].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.PurRate].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.PurRate].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.MRP].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.MRP].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.MKTQty].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.MKTQty].Value = "0";
                if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.RateVariation].Value == null) gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.RateVariation].Value = "0";
                //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.HidChk].Value = false;
                if (e.ColumnIndex != ColIndex.IsActive)
                    gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightSkyBlue;
                if (e.ColumnIndex == ColIndex.UOM)
                {
                    if (Convert.ToInt64(gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.UOM].Value) <= 0)
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.UOMNAme].ErrorText = "Select UOM";
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.UOMNAme].Style.BackColor = Color.White;
                    }
                    else
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.UOMNAme].Style.BackColor = Color.LightSkyBlue;
                }
                if (e.ColumnIndex == ColIndex.ItemName)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemName].ErrorText = "";
                    if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemName].FormattedValue.ToString().Trim() == "")
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemName].ErrorText = "Enter Item Name";
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemChk].Value = true;
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ItemName, gvRateSetting });
                    }
                    else
                    {

                        gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightSkyBlue;
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemChk].Value = true;
                        if (rdItemOnOff.Checked == true)
                        {
                            for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                            {
                                if (i != e.RowIndex && gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemName].FormattedValue.ToString().Trim().ToUpper() == gvRateSetting.Rows[i].Cells[ColIndex.ItemName].FormattedValue.ToString().Trim().ToUpper())
                                {
                                    gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemName].ErrorText = "Duplicate Item Name";
                                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ItemName, gvRateSetting });
                                    return;
                                }
                            }
                        }

                        if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.LangFullDesc].Visible == true)
                            BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.LangFullDesc, gvRateSetting });
                        else
                        {
                            if (rdItemOnOff.Checked == true)
                                BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.IsActive, gvRateSetting });
                            else
                                BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.MRP, gvRateSetting });
                        }
                    }
                }
                if (e.ColumnIndex == ColIndex.LangFullDesc)
                {
                    if (gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.LangFullDesc].FormattedValue.ToString().Trim() == "")
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.LangFullDesc].ErrorText = "Enter LangFull Desc";
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.LangFullDesc].Style.BackColor = Color.White;
                    }
                    else
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemChk].Value = true;
                        gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightSkyBlue;
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (rdItemOnOff.Checked == true)
                            BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.IsActive, gvRateSetting });
                        else
                            BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.MRP, gvRateSetting });
                    }
                }
                if (e.ColumnIndex == ColIndex.MRP)
                {
                    if (ObjFunction.CheckValidAmount(gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.MRP].Value.ToString()) == false)
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "Enter Valid MRP";
                        gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                    }
                    else
                        gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightSkyBlue;
                }
                if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MRP)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.UOMNAme, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ASaleRate)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.BSaleRate, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.BSaleRate)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    if (gvRateSetting.CurrentRow.Cells[ColIndex.CSaleRate].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.CSaleRate, gvRateSetting });
                    else if (gvRateSetting.CurrentRow.Cells[ColIndex.DSaleRate].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DSaleRate, gvRateSetting });
                    else if (gvRateSetting.CurrentRow.Cells[ColIndex.ESaleRate].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ESaleRate, gvRateSetting });
                    else if (gvRateSetting.CurrentRow.Cells[ColIndex.MKTQty].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.MKTQty, gvRateSetting });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.PurRate, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.CSaleRate)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    if (gvRateSetting.CurrentRow.Cells[ColIndex.DSaleRate].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.DSaleRate, gvRateSetting });
                    else if (gvRateSetting.CurrentRow.Cells[ColIndex.ESaleRate].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ESaleRate, gvRateSetting });
                    else if (gvRateSetting.CurrentRow.Cells[ColIndex.MKTQty].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.MKTQty, gvRateSetting });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.PurRate, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.DSaleRate)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    if (gvRateSetting.CurrentRow.Cells[ColIndex.ESaleRate].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.ESaleRate, gvRateSetting });
                    else if (gvRateSetting.CurrentRow.Cells[ColIndex.MKTQty].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.MKTQty, gvRateSetting });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.PurRate, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ESaleRate)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    if (gvRateSetting.CurrentRow.Cells[ColIndex.MKTQty].Visible == true)
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.MKTQty, gvRateSetting });
                    else
                        BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.PurRate, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MKTQty)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { e.RowIndex, ColIndex.PurRate, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.PurRate)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    if (e.RowIndex != gvRateSetting.Rows.Count - 1)
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.RateVariation)
                {
                    MovetoNext move2n = new MovetoNext(m2n);
                    if (e.RowIndex != gvRateSetting.Rows.Count - 1)
                        BeginInvoke(move2n, new object[] { e.RowIndex + 1, ColIndex.ItemName, gvRateSetting });
                }
                else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.IsActive)
                {
                    //if (Convert.ToBoolean(gvRateSetting.CurrentRow.Cells[ColIndex.IsActive].FormattedValue) == true)
                    {
                        //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].Value = true;
                        //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.HidChk].Value = true;
                        if (Convert.ToBoolean(gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].FormattedValue) == true)
                            gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightSkyBlue;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void gvRateSetting_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    btnApplyChanges.Focus();
                }
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    int x = gvRateSetting.GetCellDisplayRectangle(gvRateSetting.CurrentCell.ColumnIndex, gvRateSetting.CurrentRow.Index, false).Left + gvRateSetting.Left;
                    int y = gvRateSetting.GetCellDisplayRectangle(gvRateSetting.CurrentCell.ColumnIndex, gvRateSetting.CurrentRow.Index, false).Bottom + gvRateSetting.Top;
                    if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.UOMNAme)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.ASaleRate, gvRateSetting });
                    }
                    if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.Sr)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.ItemName, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (gvRateSetting.Rows[gvRateSetting.CurrentCell.RowIndex].Cells[ColIndex.LangFullDesc].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.LangFullDesc, gvRateSetting });
                        else
                        {
                            if (rdItemOnOff.Checked == true)
                                BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.IsActive, gvRateSetting });
                            else
                                BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.MRP, gvRateSetting });
                        }
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.LangFullDesc)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.BarCode, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.BarCode)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (rdItemOnOff.Checked == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.IsActive, gvRateSetting });
                        else
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.MRP, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MRP)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.UOMNAme, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ASaleRate)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.BSaleRate, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.BSaleRate)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (gvRateSetting.CurrentRow.Cells[ColIndex.CSaleRate].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.CSaleRate, gvRateSetting });
                        else if (gvRateSetting.CurrentRow.Cells[ColIndex.DSaleRate].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.DSaleRate, gvRateSetting });
                        else if (gvRateSetting.CurrentRow.Cells[ColIndex.ESaleRate].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.ESaleRate, gvRateSetting });
                        else if (gvRateSetting.CurrentRow.Cells[ColIndex.MKTQty].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.MKTQty, gvRateSetting });
                        else if (gvRateSetting.CurrentRow.Cells[ColIndex.PurRate].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.PurRate, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.CSaleRate)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (gvRateSetting.CurrentRow.Cells[ColIndex.DSaleRate].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.DSaleRate, gvRateSetting });
                        else if (gvRateSetting.CurrentRow.Cells[ColIndex.ESaleRate].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.ESaleRate, gvRateSetting });
                        else
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.MKTQty, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.DSaleRate)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (gvRateSetting.CurrentRow.Cells[ColIndex.ESaleRate].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.ESaleRate, gvRateSetting });
                        else
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.MKTQty, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ESaleRate)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.MKTQty, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MKTQty)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.PurRate, gvRateSetting });
                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.PurRate)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (gvRateSetting.CurrentRow.Cells[ColIndex.RateVariation].Visible == true)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex, ColIndex.RateVariation, gvRateSetting });
                        else
                            if (gvRateSetting.CurrentCell.RowIndex != gvRateSetting.Rows.Count - 1)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex + 1, ColIndex.ItemName, gvRateSetting });

                    }
                    else if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.RateVariation)
                    {
                        MovetoNext move2n = new MovetoNext(m2n);
                        if (gvRateSetting.CurrentCell.RowIndex != gvRateSetting.Rows.Count - 1)
                            BeginInvoke(move2n, new object[] { gvRateSetting.CurrentCell.RowIndex + 1, ColIndex.ItemName, gvRateSetting });
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayColumns()
        {
            try
            {
                gvRateSetting.Columns[ColIndex.ASaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive));
                gvRateSetting.Columns[ColIndex.BSaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive));
                gvRateSetting.Columns[ColIndex.CSaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive));
                gvRateSetting.Columns[ColIndex.DSaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive));
                gvRateSetting.Columns[ColIndex.ESaleRate].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive));

                gvRateSetting.Columns[ColIndex.ASaleRate].HeaderText = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
                gvRateSetting.Columns[ColIndex.BSaleRate].HeaderText = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
                gvRateSetting.Columns[ColIndex.CSaleRate].HeaderText = ObjFunction.GetAppSettings(AppSettings.CRateLabel);
                gvRateSetting.Columns[ColIndex.DSaleRate].HeaderText = ObjFunction.GetAppSettings(AppSettings.DRateLabel);
                gvRateSetting.Columns[ColIndex.ESaleRate].HeaderText = ObjFunction.GetAppSettings(AppSettings.ERateLabel);

                gvRateSetting.Columns[ColIndex.StockConversion].Visible = false;
                gvRateSetting.Columns[ColIndex.MKTQty].Visible = false;
                gvRateSetting.Columns[ColIndex.RateVariation].Visible = false;
                gvRateSetting.Columns[ColIndex.Chk].Visible = false;
                gvRateSetting.Columns[ColIndex.HidChk].Visible = false;
                gvRateSetting.Columns[ColIndex.ItemChk].Visible = false;
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
            //if (e.KeyCode == Keys.F2)
            //{
            //    ChkSelect.Checked = !ChkSelect.Checked;
            //    ChkSelect_CheckedChanged(sender, e);
            //}
            if (e.KeyCode == Keys.F5)
            {
                rdActive.Checked = true;
            }
            else if (e.KeyCode == Keys.F6)
            {
                rdDeactive.Checked = true;
            }
            else if (e.KeyCode == Keys.F7)
            {
                rdItemOnOff.Checked = true;
            }
        }
        #endregion

        private void ChkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRateSetting.Rows.Count; i++)
            {
                gvRateSetting.Rows[i].Cells[ColIndex.Chk].Value = ChkSelect.Checked;
            }
        }

        public bool Validations()
        {
            try
            {
                bool flag = false;
                bool fl = false;
                for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.Chk].FormattedValue) == true || Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.ItemChk].FormattedValue) == true)
                    {
                        fl = true;
                        break;
                    }
                }
                if (fl == false)
                {
                    //DisplayMessage("Please Select Atleast One Item");
                    DisplayMessage("Please Change Atleast One Item Details");
                }
                flag = fl;
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

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

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void gvRateSetting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (gvRateSetting.Rows.Count > 0 && e.ColumnIndex != ColIndex.Chk)//&& e.ColumnIndex!=ColIndex.IsActive)
                    {
                        if (e.ColumnIndex != ColIndex.ItemName && e.ColumnIndex != ColIndex.LangFullDesc && e.ColumnIndex != ColIndex.ItemChk)
                        {
                            gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].Value = true;
                        }
                        for (int i = 0; i < gvRateSetting.Columns.Count - 1; i++)
                        {
                            if (ColIndex.LangFullDesc != i)
                            {
                                if (gvRateSetting.Rows[e.RowIndex].Cells[i].Value == null || gvRateSetting.Rows[e.RowIndex].Cells[i].Value.ToString().Length == 0 || gvRateSetting.Rows[e.RowIndex].Cells[i].ErrorText != "")
                                {
                                    gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].Value = false;
                                    //gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstUom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                gvRateSetting.Rows[gvRateSetting.CurrentCell.RowIndex].Cells[ColIndex.UOMNAme].Value = lstUom.Text;
                gvRateSetting.Rows[gvRateSetting.CurrentCell.RowIndex].Cells[ColIndex.UOM].Value = lstUom.SelectedValue;

                lstUom.Visible = false;
                gvRateSetting.CurrentCell = gvRateSetting[ColIndex.ASaleRate, gvRateSetting.CurrentCell.RowIndex];
                gvRateSetting.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                lstUom.Visible = false;
                gvRateSetting.Focus();
            }
            else if (e.KeyCode == Keys.Space)
            {
                lstUom.Visible = false;
                gvRateSetting.Focus();
            }
        }

        private void gvRateSetting_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ASaleRate)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(ASaleRate_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.BSaleRate)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(BSaleRate_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.CSaleRate)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(CSaleRate_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.DSaleRate)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(DSaleRate_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ESaleRate)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(ESaleRate_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.PurRate)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(PurRate_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MRP)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(MRP_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MKTQty)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(MKTQty_TextChanged);
            }
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.RateVariation)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(RateVariation_TextChanged);
            }

        }

        public void ASaleRate_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ASaleRate)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void BSaleRate_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.BSaleRate)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void CSaleRate_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.CSaleRate)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void DSaleRate_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.DSaleRate)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void ESaleRate_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.ESaleRate)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void PurRate_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.PurRate)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void MRP_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MRP)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void MKTQty_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.MKTQty)
            {
                ObjFunction.SetMasked((TextBox)sender, 0, 9, OMFunctions.MaskedType.NotNegative);
            }
        }
        public void RateVariation_TextChanged(object sender, EventArgs e)
        {
            if (gvRateSetting.CurrentCell.ColumnIndex == ColIndex.RateVariation)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 9, OMFunctions.MaskedType.NotNegative);
            }
        }

        private void rdActive_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
            CalculateTotProducts();
        }

        private void rdDeactive_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
            CalculateTotProducts();
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdItemOnOff.Checked == true)
            {
                gvRateSetting.Columns[ColIndex.BarCode].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
                ColumnsStatus();
            }
            else
            {
                BindGrid();
                CalculateTotProducts();
            }
        }

        private void rdItemOnOff_CheckedChanged(object sender, EventArgs e)
        {
            gvRateSetting.Columns[ColIndex.BarCode].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
            ColumnsStatus();
        }

        public void ColumnsStatus()
        {
            try
            {
                if (rdItemOnOff.Checked == true)
                {
                    for (int i = 5; i < gvRateSetting.ColumnCount; i++)
                    {
                        if (gvRateSetting.Columns[i].Index != ColIndex.IsActive)
                            gvRateSetting.Columns[i].Visible = false;
                    }
                    DisplayColumns();
                    gvRateSetting.Columns[ColIndex.ASaleRate].Visible = false;
                    gvRateSetting.Columns[ColIndex.BSaleRate].Visible = false;
                }
                else
                {
                    for (int i = 5; i < gvRateSetting.ColumnCount; i++)
                    {
                        if (gvRateSetting.Columns[i].Index == ColIndex.StockConversion || gvRateSetting.Columns[i].Index == ColIndex.DSaleRate || gvRateSetting.Columns[i].Index == ColIndex.ESaleRate || gvRateSetting.Columns[i].Index == ColIndex.BarCodeNo || gvRateSetting.Columns[i].Index == ColIndex.PkSrNo || gvRateSetting.Columns[i].Index == ColIndex.BSaleRate || gvRateSetting.Columns[i].Index == ColIndex.ASaleRate || gvRateSetting.Columns[i].Index == ColIndex.ItemNo || gvRateSetting.Columns[i].Index == ColIndex.UOM)
                            gvRateSetting.Columns[i].Visible = false;
                        else
                            gvRateSetting.Columns[i].Visible = true;

                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                            gvRateSetting.Columns[ColIndex.LangFullDesc].Visible = true;
                        else
                            gvRateSetting.Columns[ColIndex.LangFullDesc].Visible = false;
                    }
                    DisplayColumns();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void gvRateSetting_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == ColIndex.IsActive && rdActive.Checked == true)
                {
                    if (Convert.ToBoolean(gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].FormattedValue) == true)
                    {
                        bool isValid = false;
                        long itemNo = Convert.ToInt64(gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.ItemNo].FormattedValue);
                        for (int i = 0; i < gvRateSetting.Rows.Count; i++)
                        {
                            if (i != e.RowIndex && itemNo == Convert.ToInt64(gvRateSetting.Rows[i].Cells[ColIndex.ItemNo].FormattedValue) &&
                                Convert.ToBoolean(gvRateSetting.Rows[i].Cells[ColIndex.IsActive].FormattedValue) == true)
                            {
                                isValid = true;
                                break;
                            }
                        }
                        if (isValid == false)
                        {
                            gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = true;
                        }
                        else
                        {
                            gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = false;
                            if (Convert.ToBoolean(gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].FormattedValue) == true)
                                gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.IsActive].Value = true;
                        if (Convert.ToBoolean(gvRateSetting.Rows[e.RowIndex].Cells[ColIndex.Chk].FormattedValue) == true)
                            gvRateSetting.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightSkyBlue;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void rdMrpchange_CheckedChanged(object sender, EventArgs e)
        {
            BindGrid();
            CalculateTotProducts();
        }
    }
}
