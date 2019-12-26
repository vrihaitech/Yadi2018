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
    public partial class RateSetting : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();


        MRateSetting mRateSetting = new MRateSetting();
        DBMItemMaster dbMItemMaster = new DBMItemMaster();
             
        DataTable dt;
        bool Cancel = false;       
        string sql;
        long pk = 0;      

        public RateSetting()
        {
            InitializeComponent();
        }

        private void RateSetting_Load(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbGroupName, "SELECT StockGroupNo, StockGroupName FROM MStockGroup ORDER BY StockGroupName");
            ObjFunction.FillCombo(CmbUOMName, "Select UOMNo,UOMName from MUOM ORDER BY UOMName");
            cmbGroupName.Focus();
            Cancel = false;
            //gvRateSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //            | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));

            setValidation((Control)txtPurRate);
            setValidation((Control)txtStockCon);
            setValidation((Control)txtPer);
            setValidation((Control)txtASaleRate);
            setValidation((Control)txtBSaleRate);
            setValidation((Control)txtCSaleRate);
            setValidation((Control)txtDSaleRate);
            setValidation((Control)txtESaleRate);

            setRateControls();
        }

        public void setRateControls()
        {
            lblRate1.Text = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
            lblRate2.Text = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
            lblRate3.Text = ObjFunction.GetAppSettings(AppSettings.CRateLabel);
            lblRate4.Text = ObjFunction.GetAppSettings(AppSettings.DRateLabel);
            lblRate5.Text = ObjFunction.GetAppSettings(AppSettings.ERateLabel);

            txtASaleRate.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive));
            txtBSaleRate.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive));
            txtCSaleRate.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive));
            txtDSaleRate.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive));
            txtESaleRate.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive));

            lblRate1.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive));
            lblRate2.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive));
            lblRate3.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive));
            lblRate4.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive));
            lblRate5.Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive));

            gvRateSetting.Columns[4].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ARateIsActive));
            gvRateSetting.Columns[5].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.BRateIsActive));
            gvRateSetting.Columns[6].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.CRateIsActive));
            gvRateSetting.Columns[7].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.DRateIsActive));
            gvRateSetting.Columns[8].Visible = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ERateIsActive));

            gvRateSetting.Columns[4].HeaderText = ObjFunction.GetAppSettings(AppSettings.ARateLabel);
            gvRateSetting.Columns[5].HeaderText = ObjFunction.GetAppSettings(AppSettings.BRateLabel);
            gvRateSetting.Columns[6].HeaderText = ObjFunction.GetAppSettings(AppSettings.CRateLabel);
            gvRateSetting.Columns[7].HeaderText = ObjFunction.GetAppSettings(AppSettings.DRateLabel);
            gvRateSetting.Columns[8].HeaderText = ObjFunction.GetAppSettings(AppSettings.ERateLabel);
        }

        public void setValidation(Control ctrl)
        {
            ctrl.KeyDown += new KeyEventHandler(Control_KeyDown);
            ctrl.Leave += new EventHandler(Control_Leave);
        }

        private void Control_Leave(object sender, EventArgs e)
        {
            
            if (((TextBox)sender).Text == "")
            {
                OMMessageBox.Show("Enter atleast 1 character.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);

                if (((TextBox)sender).Name == "txtPatternNo") ((TextBox)sender).Text = ".";
                ((TextBox)sender).Focus();
            }
            else if (ObjFunction.CheckValidAmount(((TextBox)sender).Text) == false)
            {
                OMMessageBox.Show("Enter Valid Amount.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                ((TextBox)sender).Focus();
            }
            else
            {
                EP.SetError(((TextBox)sender), "");
            }

        }               

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                Control_Leave(sender, new EventArgs());
        }

        private void BindGrid()
        {
            while (gvRateSetting.Rows.Count > 0)
            {
                gvRateSetting.Rows.RemoveAt(0);
            }

            if (ObjQry.ReturnLong("Select count(*) from MRateSetting Where ItemNo=" + ObjFunction.GetComboValue(cmbItemName) + "", CommonFunctions.ConStr) != 0)
            {
                if (chkRateBy.Checked == false)
                {
                    //sql = "Select 0 AS SrNo,MRateSetting.FromDate, MStockBarcode.Barcode, MRateSetting.PurRate, MRateSetting.SaleRate, MUOM.UOMName, " +
                    //      " MRateSetting.MKTQty, MRateSetting.QuantitySlabFrom, MRateSetting.QuantitySlabTo, MGodown.GodownName, MRateSetting.StockConversion, " +
                    //      " MRateSetting.PerOfRateVariation, MRateSetting.PkSrNo, MStockBarcode.PkStockBarcodeNo, MRateSetting.UOMNo " +
                    //      " from MRateSetting INNER JOIN " +
                    //      " MStockItems_V(NULL,NULL) as MStockItems ON MRateSetting.ItemNo = mItemMaster.ItemNo INNER JOIN " +
                    //      " MUOM ON MRateSetting.UOMNo = MUOM.UOMNo INNER JOIN " +
                    //      " MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo INNER JOIN " +
                    //      " MGodown ON MRateSetting.GodownNo = MGodown.GodownNo" +
                    //      " WHERE (MStockItems.ItemNo =" + ObjFunction.GetComboValue(cmbItemName) + ")ORDER BY MRateSetting.FromDate DESC ";


                    sql = " SELECT 0 AS SrNo, T.FromDate, MStockBarcode.Barcode, T.PurRate, T.ASaleRate,T.BSaleRate,T.CSaleRate,T.DSaleRate,T.ESaleRate, MUOM.UOMName, T.MKTQty, T.StockConversion, T.PerOfRateVariation, T.PkSrNo, T.FkBcdSrNo,T.UOMNO, CASE WHEN T1.PkSrNo IS NULL  " +
                        " THEN 'false' ELSE 'true' END AS IsActive ,T.IsActive As 'Open',T.MRP " +
                        " FROM         MRateSetting AS T INNER JOIN MUOM ON T.UOMNo = MUOM.UOMNo INNER JOIN " +
                        " MStockBarcode ON T.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo " +
                        "  LEFT OUTER JOIN  dbo.GetItemRateAll(" + ObjFunction.GetComboValue(cmbItemName) + ", NULL, NULL, NULL, NULL, NULL) AS T1 ON T.PkSrNo = T1.PkSrNo " +
                        " WHERE (T.ItemNo =" + ObjFunction.GetComboValue(cmbItemName) + ") ORDER BY T.FromDate DESC, T.ItemNo DESC ";
                }
                else if (chkRateBy.Checked == true)
                {
                    sql = " SELECT 0 AS SrNo, T.FromDate, MStockBarcode.Barcode, T.PurRate, T.ASaleRate,T.BSaleRate,T.CSaleRate,T.DSaleRate,T.ESaleRate, MUOM.UOMName, T.MKTQty,  T.StockConversion, T.PerOfRateVariation, T.PkSrNo, T.FkBcdSrNo,T.UOMNO, CASE WHEN T1.PkSrNo IS NULL  " +
                                           " THEN 'false' ELSE 'true' END AS IsActive,T.IsActive As 'Open',T.MRP " +
                                           " FROM         MRateSetting AS T INNER JOIN MUOM ON T.UOMNo = MUOM.UOMNo INNER JOIN " +
                                           " MStockBarcode ON T.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo " +
                                           " INNER JOIN  dbo.GetItemRateAll(" + ObjFunction.GetComboValue(cmbItemName) + ", NULL, NULL, NULL, NULL, NULL) AS T1 ON T.PkSrNo = T1.PkSrNo " +
                                           " WHERE (T.ItemNo =" + ObjFunction.GetComboValue(cmbItemName) + ") ORDER BY T.FromDate DESC, T.ItemNo DESC ";

                }
            }

            dt = ObjFunction.GetDataView(sql).Table;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                gvRateSetting.Rows.Add();
                for (int i = 0; i < gvRateSetting.Columns.Count; i++)
                {
                    gvRateSetting.Rows[j].Cells[i].Value = dt.Rows[j].ItemArray[i].ToString();
                    gvRateSetting.Rows[j].Cells[0].Value = j + 1;

                }
            }
        }

        private void chkSave_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSave.Checked == true)
            {
                chkSave.Text = "Master";
                BtnSave.Enabled = true;
            }
            else
            {
                chkSave.Text = "";
                BtnSave.Enabled = true;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           
                SetValueMStockBarCodeAndMRateSetting();
                txtBarCode.Focus();
            
        }

        public void SetValueMRateSetting()
        {
            if (chkSave.Visible == false)
            {
                if (txtBarCode.Enabled == false)
                {
                    txtBarCode.Enabled = true;

                }
                mRateSetting.PkSrNo = pk;
                mRateSetting.ItemNo = ObjFunction.GetComboValue(cmbItemName);
                mRateSetting.FromDate = dtpFromDate.Value;
                mRateSetting.UOMNo = ObjFunction.GetComboValue(CmbUOMName);
                mRateSetting.StockConversion = Convert.ToDouble(txtStockCon.Text.Trim());
                mRateSetting.PerOfRateVariation = Convert.ToDouble(txtPer.Text.Trim());
                mRateSetting.MKTQty = Convert.ToInt64(txtMKTQty.Text.Trim());
                mRateSetting.MRP = Convert.ToDouble(txtMRP.Text);
                mRateSetting.IsActive = true;
                mRateSetting.UserID = DBGetVal.UserID;
                mRateSetting.UserDate = DBGetVal.ServerTime.Date;
                mRateSetting.CompanyNo = DBGetVal.FirmNo;

                if (ObjQry.ReturnInteger("SELECT PkStockBarcodeNo FROM MStockBarcode WHERE (Barcode = '" + txtBarCode.Text + "') AND   (MStockBarcode.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + " )", CommonFunctions.ConStr) != 0)
                {
                    if (ObjQry.ReturnInteger("SELECT MStockBarcode.PkStockBarcodeNo FROM MStockBarcode INNER JOIN MRateSetting ON MStockBarcode.PkStockBarcodeNo = MRateSetting.FkBcdSrNo WHERE (Barcode = '" + txtBarCode.Text + "') AND   (MStockBarcode.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + " )", CommonFunctions.ConStr) != 0)
                    {
                        mRateSetting.FkBcdSrNo = ObjQry.ReturnInteger("SELECT   MRateSetting.FkBcdSrNo FROM MRateSetting INNER JOIN MStockBarcode ON MRateSetting.FkBcdSrNo = MStockBarcode.PkStockBarcodeNo WHERE     (MStockBarcode.Barcode = '" + txtBarCode.Text + "')", CommonFunctions.ConStr);
                    }
                    else
                    {
                        string sql1 = "SELECT  PkStockBarcodeNo FROM MStockBarcode WHERE (MStockBarcode.Barcode = '" + txtBarCode.Text + "') ";
                        mRateSetting.FkBcdSrNo = Convert.ToInt32(ObjQry.ReturnInteger(sql1, CommonFunctions.ConStr));
                    }
                }
                mRateSetting.PurRate = Convert.ToDouble(txtPurRate.Text);
                mRateSetting.ASaleRate = (txtASaleRate.Text == "") ? 0 : Convert.ToDouble(txtASaleRate.Text);
                mRateSetting.BSaleRate = (txtBSaleRate.Text == "") ? 0 : Convert.ToDouble(txtBSaleRate.Text);
                mRateSetting.CSaleRate = (txtCSaleRate.Text == "") ? 0 : Convert.ToDouble(txtCSaleRate.Text);
                mRateSetting.DSaleRate = (txtDSaleRate.Text == "") ? 0 : Convert.ToDouble(txtDSaleRate.Text);
                mRateSetting.ESaleRate = (txtESaleRate.Text == "") ? 0 : Convert.ToDouble(txtESaleRate.Text);
            }
            if (dbMItemMaster.AddMRateSetting1(mRateSetting) == true)
            {
                OMMessageBox.Show("Item Rate Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                BindGrid();
                pk = 0;
                BtnSave.Enabled = false;
                txtBarCode.Focus();
                Clrscr();
                btnCancel.Visible = true;
            }
            else
            {
                OMMessageBox.Show(" Item Rate not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
        }

        public void Clrscr()
        {
            txtBarCode.Text = "";
            txtPurRate.Text = "";
            txtASaleRate.Text = "";
            txtBSaleRate.Text = "";
            txtCSaleRate.Text = "";
            txtDSaleRate.Text = "";
            txtESaleRate.Text = "";
            CmbUOMName.SelectedIndex = 0;
            txtPer.Text = "";
            txtMRP.Text = "";
            txtStockCon.Text = "";
            txtStockCon.Text = "";
            txtMKTQty.Text = "";
            lblPrimary.Text = "";
            lblPurRate.Text = "";
            lblPrimary.Text = "";
            lblVariation.Text = "";
            lblSaleRate.Text = "";
        }

        public void SetValueMStockBarCodeAndMRateSetting()
        {
            if (Validations() == true)
            {
                if (chkSave.Visible == true && chkSave.Checked == true)
                {
                     dbMItemMaster = new DBMItemMaster();
                    mRateSetting = new MRateSetting();
                    //mStockBarcode = new MStockBarcode();
                    //mStockBarcode.ItemNo = ObjFunction.GetComboValue(cmbItemName);
                    //mStockBarcode.Barcode = txtBarCode.Text;
                    //mStockBarcode.IsActive = true;
                    //mStockBarcode.UserID = DBGetVal.UserID;
                    //mStockBarcode.UserDate = DBGetVal.ServerTime.Date;
                    //dbmStockItems.AddMStockBarcode1(mStockBarcode);
                    mRateSetting.ItemNo = ObjFunction.GetComboValue(cmbItemName);
                    mRateSetting.FromDate = dtpFromDate.Value;
                    mRateSetting.UOMNo = ObjFunction.GetComboValue(CmbUOMName);
                    mRateSetting.StockConversion = Convert.ToDouble(txtStockCon.Text.Trim());
                    mRateSetting.PerOfRateVariation = Convert.ToDouble(txtPer.Text.Trim());
                    mRateSetting.MKTQty = Convert.ToInt64(txtMKTQty.Text.Trim());
                    mRateSetting.MRP = Convert.ToDouble(txtMRP.Text);
                    mRateSetting.IsActive = true;
                    mRateSetting.UserID = DBGetVal.UserID;
                    mRateSetting.UserDate = DBGetVal.ServerTime.Date;
                    mRateSetting.PurRate = Convert.ToDouble(txtPurRate.Text);
                    mRateSetting.ASaleRate = Convert.ToDouble(txtASaleRate.Text);
                    mRateSetting.BSaleRate = Convert.ToDouble(txtBSaleRate.Text);
                    mRateSetting.CSaleRate = (txtCSaleRate.Text == "") ? 0 : Convert.ToDouble(txtCSaleRate.Text);
                    mRateSetting.DSaleRate = (txtDSaleRate.Text=="") ? 0:Convert.ToDouble(txtDSaleRate.Text);
                    mRateSetting.ESaleRate =(txtESaleRate.Text=="") ? 0: Convert.ToDouble(txtESaleRate.Text);
                    mRateSetting.CompanyNo = DBGetVal.FirmNo;
                    dbMItemMaster.AddMRateSetting2(mRateSetting);
                    if (dbMItemMaster.ExecuteNonQueryStatements() == true)
                    {
                        OMMessageBox.Show(" Item Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();
                        if (chkSave.Checked == true)
                        {
                            chkSave.Checked = false;
                            chkSave.Visible = false;
                        }
                        else
                        {
                            chkSave.Checked = true;
                            chkSave.Visible = true;

                        }
                        BtnSave.Enabled = false;
                        Clrscr();
                        gvRateSetting.Focus();
                    }
                    else
                    {
                        OMMessageBox.Show(" Item not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }

                }
                else
                {
                    SetValueMRateSetting();
                }
            }
        }

        public bool Validations()
        {
            bool flag = false;
            if (ObjFunction.GetComboValue(cmbItemName) <= 1)
            {
                EP.SetError(cmbItemName, "Select Item Name ");
                EP.SetIconAlignment(cmbItemName, ErrorIconAlignment.MiddleRight);
                cmbItemName.Focus();
            }
            else if (txtPurRate.Text.Trim() == "")
            {
                EP.SetError(txtPurRate, "Enter The PurRate");
                EP.SetIconAlignment(txtPurRate, ErrorIconAlignment.MiddleRight);
                txtPurRate.Focus();
            }
            else if (txtASaleRate.Text.Trim() == "")
            {
                EP.SetError(txtASaleRate, "Enter The A SaleRate");
                EP.SetIconAlignment(txtASaleRate, ErrorIconAlignment.MiddleRight);
                txtASaleRate.Focus();
            }
            else if (txtPer.Text.Trim() == "")
            {
                EP.SetError(txtPer, "Enter The % of Variation ");
                EP.SetIconAlignment(txtPer, ErrorIconAlignment.MiddleRight);
                txtPer.Focus();

            }
            else if (txtMRP.Text.Trim() == "")
            {
                EP.SetError(txtMRP, "Enter The MRP ");
                EP.SetIconAlignment(txtMRP, ErrorIconAlignment.MiddleRight);
                txtMRP.Focus();

            }
            else if (txtStockCon.Text.Trim() == "")
            {
                EP.SetError(txtStockCon, "Enter Stock Conversion");
                EP.SetIconAlignment(txtStockCon, ErrorIconAlignment.MiddleRight);
                txtStockCon.Focus();
            }
            else
            {
                EP.Clear();
                flag = true;
            }
            return flag;
        }            

        public void BindData()
        {
            while (gvRateSetting.Rows.Count > 0)
            {
                gvRateSetting.Rows.RemoveAt(0);
            }
            gvRateSetting.Rows.Add();
            for (int i = 0; i < gvRateSetting.Rows.Count - 1; i++)
            {
                gvRateSetting.Rows[i].Cells[0].Value = i + 1;
                gvRateSetting.Rows[i].Cells[1].Value = dtpFromDate.Value; 
                gvRateSetting.Rows[i].Cells[2].Value = txtBarCode.Text;
                gvRateSetting.Rows[i].Cells[3].Value = CmbUOMName.Text;
                gvRateSetting.Rows[i].Cells[4].Value = txtPurRate.Text;
                gvRateSetting.Rows[i].Cells[5].Value = 0;
                gvRateSetting.Rows[i].Cells[6].Value = txtASaleRate.Text;
                gvRateSetting.Rows[i].Cells[7].Value = txtBSaleRate.Text;
                gvRateSetting.Rows[i].Cells[8].Value = txtCSaleRate.Text;
                gvRateSetting.Rows[i].Cells[9].Value = txtDSaleRate.Text;
                gvRateSetting.Rows[i].Cells[10].Value = txtESaleRate.Text;
                gvRateSetting.Rows[i].Cells[11].Value = txtMKTQty.Text.Trim();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel = true;
            txtBarCode.Text = "..";
            EP.SetError(txtBarCode, "");
            EP.SetError(txtPurRate, "");
            EP.SetError(txtASaleRate, "");
            EP.SetError(txtBSaleRate, "");
            EP.SetError(txtCSaleRate, "");
            EP.SetError(txtDSaleRate, "");
            EP.SetError(txtESaleRate, "");
            EP.SetError(txtPer, "");
            EP.SetError(txtMRP, "");
            EP.SetError(txtStockCon, "");
            txtBarCode.Enabled = false;
            chkSave.Visible = false;
            BtnSave.Enabled = false;
            while (gvRateSetting.Rows.Count > 0)
            {
                gvRateSetting.Rows.RemoveAt(0);
            }
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            lblVariation.Text = "";
            lblUom.Text = "Default";
            lblPurRate.Text = "";
            lblSaleRate.Text = "";
            lblPrimary.Text = "";
            txtMKTQty.Text = "1";
            EP.SetError(txtBarCode, "");
            cmbGroupName.Focus();
        }              

        private void gvRateSetting_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 14)
            {
                txtBarCode.Focus();
            }
        }

        private void gvRateSetting_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = Convert.ToDateTime(e.Value).ToString("MMM-dd");
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvRateSetting_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //string sql = "SELECT  MAX(distinct MRateSetting.FromDate) FROM MStockBarcode INNER JOIN MRateSetting ON MStockBarcode.PkStockBarcodeNo = MRateSetting.FkBcdSrNo WHERE     (MStockBarcode.Barcode = '" + gvRateSetting.Rows[e.RowIndex].Cells[2].Value.ToString() + "') ";
            //if (Convert.ToDateTime(ObjQry.ReturnDate(sql, CommonFunctions.ConStr)) == Convert.ToDateTime(gvRateSetting.Rows[e.RowIndex].Cells[1].Value))
            //{
                CmbUOMName.Focus();

               // txtBarCode.Enabled =true ;
                pk = 0;
                if (chkSave.Visible == true)
                {
                    chkSave.Visible = false;
                    chkSave.Enabled = false;
                }
                BtnSave.Enabled = true;
                dtpFromDate.Value = Convert.ToDateTime(gvRateSetting.Rows[e.RowIndex].Cells[1].Value.ToString());
                txtBarCode.Text = gvRateSetting.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtBarCode.Enabled = false;
                txtPurRate.Text = gvRateSetting.Rows[e.RowIndex].Cells[3].Value.ToString();
                
                CmbUOMName.SelectedValue = Convert.ToInt64(gvRateSetting.Rows[e.RowIndex].Cells[15].Value.ToString());
                txtASaleRate.Text = gvRateSetting.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtBSaleRate.Text = gvRateSetting.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtCSaleRate.Text = gvRateSetting.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtDSaleRate.Text = gvRateSetting.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtESaleRate.Text = gvRateSetting.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtMKTQty.Text = gvRateSetting.Rows[e.RowIndex].Cells[10].Value.ToString();
                txtStockCon.Text = gvRateSetting.Rows[e.RowIndex].Cells[11].Value.ToString();
                txtPer.Text = gvRateSetting.Rows[e.RowIndex].Cells[12].Value.ToString();
                txtMRP.Text = gvRateSetting.Rows[e.RowIndex].Cells[18].Value.ToString();
                pk = Convert.ToInt32(gvRateSetting.Rows[e.RowIndex].Cells[13].Value);
               
                lblPrimary.Text = "1 " + CmbUOMName.Text + " = ";
                alignFields();
                lblPurRate.Text = "/ " + txtMKTQty.Text +" "+ CmbUOMName.Text;
                lblSaleRate.Text = "/ " + txtMKTQty.Text +" "+ CmbUOMName.Text;

                //double Min, Max;
                //Min = Convert.ToDouble(txtSaleRate.Text) - (Convert.ToDouble(txtSaleRate.Text) * (Convert.ToDouble(txtPer.Text) / 100));
                //Max = Convert.ToDouble(txtSaleRate.Text) + (Convert.ToDouble(txtSaleRate.Text) * (Convert.ToDouble(txtPer.Text) / 100));
                //lblVariation.Text = "Allowed between Min :" + Min.ToString() + " Max :" + Max.ToString();
              
                txtPurRate.Focus();
           // }
            //else
           // {
             //   OMMessageBox.Show("This Row Can Not Be Updated", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

           // }
        }

        private void alignFields()
        {
            txtStockCon.Left = lblPrimary.Left + lblPrimary.Width + 10;
            lblUom.Left = txtStockCon.Left + txtStockCon.Width + 10;
        }

        private void cmbdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPurRate.Focus();
            }
        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SearchBarCode();
            }
        }
        
        public void SearchBarCode()
        {
            EP.SetError(txtBarCode, "");
            chkSave.Visible = false;
            chkSave.Enabled = false;
            
            //if (txtBarCode.Text.Trim() == "" && Convert.ToBoolean(ObjQry.ReturnString("SELECT IsBarCodeDisplay FROM  TSalesSetting", CommonFunctions.ConStr)) != false)
            if (txtBarCode.Text.Trim() == "" && Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsBarcodeEnabled)) != false)
            {
                EP.SetError(txtBarCode, "Enter The BarCode");
                EP.SetIconAlignment(txtBarCode, ErrorIconAlignment.MiddleRight);
                txtBarCode.Focus();
            }
            else if (txtBarCode.Text.Trim() == "")
            {
                string Barcode = ObjQry.ReturnString(" Select Barcode from MStockBarcode where PkStockBarcodeNo=(select max(PkStockBarcodeNo) from MStockBarcode where ItemNo=" + ObjFunction.GetComboValue(cmbItemName) + " and IsActive='true')", CommonFunctions.ConStr);
                if (Barcode == "")
                {
                    string str = ObjQry.ReturnString(" SELECT MUOM.UOMName " +
                    " FROM MUOM INNER JOIN " +
                    " MStockItems_V(1," + ObjFunction.GetComboValue(cmbItemName) + ",NULL,NULL,NULL,NULL,NULL) as MStockItems ON MUOM.UOMNo = mItemMaster.UOMPrimary " +
                    " WHERE (MStockItems.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + " ) AND (MStockItems.IsActive='true')", CommonFunctions.ConStr);
                    lblUom.Text = str;   
                    chkSave.Visible = true;
                    chkSave.Enabled = true;
                    chkSave.Checked = true;
                }
                else
                {
                    txtBarCode.Text = Barcode.ToString();
                    string str = ObjQry.ReturnString(" SELECT     MUOM.UOMName " +
                                                " FROM   MUOM INNER JOIN " +
                                                " MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON MUOM.UOMNo = mItemMaster.UOMPrimary INNER JOIN " +
                                                " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo WHERE (Barcode = '" + txtBarCode.Text + "') AND   (MStockBarcode.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + " ) AND (MStockItems.IsActive='true')", CommonFunctions.ConStr);
                    lblUom.Text = str;
                    chkSave.Visible = false;
                }
               
                            

                //fetch last barcode 

                
            }
            else if (ObjQry.ReturnInteger("SELECT PkStockBarcodeNo FROM MStockBarcode WHERE (Barcode = '" + txtBarCode.Text + "') AND   (MStockBarcode.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + " )", CommonFunctions.ConStr) != 0)
            {
                string str = ObjQry.ReturnString(" SELECT     MUOM.UOMName " +
                                                " FROM   MUOM INNER JOIN " +
                                                " MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON MUOM.UOMNo = mItemMaster.UOMPrimary INNER JOIN " +
                                                " MStockBarcode ON mItemMaster.ItemNo = MStockBarcode.ItemNo WHERE (Barcode = '" + txtBarCode.Text + "') AND   (MStockBarcode.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + " ) AND (MStockItems.IsActive='true')", CommonFunctions.ConStr);
                lblUom.Text = str;
                chkSave.Visible = false;
            }
            else if (ObjQry.ReturnInteger("SELECT PkStockBarcodeNo FROM MStockBarcode WHERE (Barcode = '" + txtBarCode.Text + "')", CommonFunctions.ConStr) != 0)
            {
                EP.SetError(txtBarCode, "");
                EP.SetError(txtBarCode, "This BarCode Assing to Another Items");
                EP.SetIconAlignment(txtBarCode, ErrorIconAlignment.MiddleRight);
                txtBarCode.Focus();
            }
            else
            {
                string str = ObjQry.ReturnString(" SELECT MUOM.UOMName " +
                                                       " FROM MUOM INNER JOIN " +
                                                       " MStockItems_V(1," + ObjFunction.GetComboValue(cmbItemName) + ",NULL,NULL,NULL,NULL,NULL) as MStockItems ON MUOM.UOMNo = mItemMaster.UOMPrimary " +
                                                       " WHERE (MStockItems.ItemNo = " + ObjFunction.GetComboValue(cmbItemName) + " ) AND (MStockItems.IsActive='true')", CommonFunctions.ConStr);
                lblUom.Text = str;
                CmbUOMName.Focus();
                chkSave.Visible = true;
                chkSave.Enabled = true;
                chkSave.Checked = true;

            }

        }

        private void cmbGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObjFunction.FillCombo(cmbItemName, "SELECT MStockItems_V.ItemNo, MStockItems_V.ItemName FROM MStockGroup INNER JOIN MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems_V ONmItemGroup.ItemGroupNo  =  MStockItems_V.GroupNo " +
                                               " WHERE(MItemGroup.StockGroupNo = " + ObjFunction.GetComboValue(cmbGroupName) + ")AND (MStockItems_V.ItemNo <> 1) AND (MStockItems_V.IsActive='true')");
            EP.Clear();
            chkSave.Visible = false;

        }

        private void CmbUOMName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EP.SetError(CmbUOMName, "");
                if (ObjFunction.GetComboValue(CmbUOMName) <= 0)
                {
                    EP.SetError(CmbUOMName, "Select Item Name ");
                    EP.SetIconAlignment(CmbUOMName, ErrorIconAlignment.MiddleRight);
                    CmbUOMName.Focus();
                }
                else
                {
                    lblPrimary.Text = "1 " + CmbUOMName.Text + " = ";
                    alignFields();
                    lblPurRate.Text = "/ " + txtMKTQty.Text +" "+ CmbUOMName.Text;
                    lblSaleRate.Text = "/ " + txtMKTQty.Text +" "+ CmbUOMName.Text;
                }
            }
        }

        private void cmbItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                EP.SetError(cmbItemName, "");
                if (ObjFunction.GetComboValue(cmbItemName) <= 0)
                {
                    EP.SetError(cmbItemName, "Select Item Name ");
                    EP.SetIconAlignment(cmbItemName, ErrorIconAlignment.MiddleRight);
                    cmbItemName.Focus();
                    
                }
                else
                {
                    Cancel = false;
                    CmbUOMName.Text = ObjQry.ReturnString("SELECT MUOM.UOMName FROM MUOM INNER JOIN MStockItems_V(1,NULL,NULL,NULL,NULL,NULL,NULL) as MStockItems ON MUOM.UOMNo = mItemMaster.UOMDefault  Where mItemMaster.ItemName='" + cmbItemName.Text + "' AND (MStockItems.IsActive='true')", CommonFunctions.ConStr);
                    lblUom.Text = CmbUOMName.Text;
                    lblPrimary.Text = "1 " + CmbUOMName.Text + " = ";
                    alignFields();
                    if (ObjQry.ReturnInteger("Select ItemNo from MRateSetting Where ItemNo=" + ObjFunction.GetComboValue(cmbItemName) + "", CommonFunctions.ConStr) == 0)
                    {
                        txtBarCode.Focus();
                    }
                    else
                    {
                        BindGrid();
                        cmbGroupName.Enabled = false;
                        cmbItemName.Enabled = false;
                    }
                    txtBarCode.Focus();
                }
            }
        }                            

        private void txtPer_Leave(object sender, EventArgs e)
        {
            TxtPerValidation();
        }
        public bool TxtValidation(TextBox T)
        {
            bool flage=false;
            EP.SetError(T, "");
            if (Cancel == false)
            {
                if (T.Text.Trim() == "")
                {
                    EP.SetError(T, "Enter MRP");
                    EP.SetIconAlignment(T, ErrorIconAlignment.MiddleRight);
                     T.Focus();
                }
                else if (ObjFunction.CheckValidAmount(T.Text.Trim()) == false)
                {
                    EP.SetError(T, "Enter Valid MRP");
                    EP.SetIconAlignment(T, ErrorIconAlignment.MiddleRight);
                    T.Focus();
                }
                else
                {
                    flage = true;
                    
                }
            }
            return flage;
        }

        public void TxtPerValidation()
        {
            EP.SetError(txtPer, "");
            if (Cancel == false)
            {
                if (txtPer.Text.Trim() == "")
                {
                    EP.SetError(txtPer, "Enter % of Variation");
                    EP.SetIconAlignment(txtPer, ErrorIconAlignment.MiddleRight);
                    txtPer.Focus();
                }
                else if (ObjFunction.CheckValidAmount(txtPer.Text.Trim()) == false)
                {
                    EP.SetError(txtPer, "Enter Valid % of Variation");
                    EP.SetIconAlignment(txtPer, ErrorIconAlignment.MiddleRight);
                    txtPer.Focus();
                }
                else
                {
                    BtnSave.Enabled = true;
                    //double Min, Max;
                    //Min = Convert.ToDouble(txtSaleRate.Text) - (Convert.ToDouble(txtSaleRate.Text) * (Convert.ToDouble(txtPer.Text) / 100));
                    //Max = Convert.ToDouble(txtSaleRate.Text) + (Convert.ToDouble(txtSaleRate.Text) * (Convert.ToDouble(txtPer.Text) / 100));
                    //lblVariation.Text = "Allowed between Min :" + Min.ToString() + " Max :" + Max.ToString();
                    chkSave.Focus();

                }
            }
        }

        private void txtPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                TxtPerValidation();
                if(chkSave.Visible==false)
                 BtnSave.Focus();
            }
        }

        private void chkRateBy_CheckedChanged(object sender, EventArgs e)
        {
            EP.SetError(cmbItemName, "");
            if (ObjFunction.GetComboValue(cmbItemName) <= 0)
            {
                EP.SetError(cmbItemName, "Select Item Name");
                EP.SetIconAlignment(cmbItemName, ErrorIconAlignment.MiddleRight);
                cmbItemName.Focus();
            }
            else
            {
                if (chkRateBy.Checked == true)
                {
                    chkRateBy.Text = "Yes";
                    BindGrid();
                    txtBarCode.Focus();
                }
                else if (chkRateBy.Checked == false)
                {
                    chkRateBy.Text = "No";
                    BindGrid();
                    txtBarCode.Focus();
                }
            }
        }

        private void txtMKTQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                lblPurRate.Text = "/ " + txtMKTQty.Text + " " + CmbUOMName.Text;
                lblSaleRate.Text = "/ " + txtMKTQty.Text + " " + CmbUOMName.Text;
            }
        }

        private void txtMRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (TxtValidation(txtMRP) == true)
                {
                    txtASaleRate.Focus();
                }
            }
        }

        private void txtMRP_Leave(object sender, EventArgs e)
        {
            if (TxtValidation(txtMRP) == true)
            {
                txtASaleRate.Focus();
            }
        }

        private void gvRateSetting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.D)
            {
                DataGridView gv = (DataGridView)sender;
                if (gv.CurrentCell.ColumnIndex == 4 && gv.Columns[4].Visible == true)
                {
                    if(gv.Columns[5].Visible == true)
                       gv.CurrentRow.Cells[5].Value=gv.CurrentCell.Value.ToString();
                    if (gv.Columns[6].Visible == true)
                        gv.CurrentRow.Cells[6].Value = gv.CurrentCell.Value.ToString();
                    if (gv.Columns[7].Visible == true)
                        gv.CurrentRow.Cells[7].Value = gv.CurrentCell.Value.ToString();
                    if (gv.Columns[8].Visible == true)
                        gv.CurrentRow.Cells[8].Value = gv.CurrentCell.Value.ToString();
                }
                else if (gv.CurrentCell.ColumnIndex == 5 && gv.Columns[5].Visible == true)
                {
                    if (gv.Columns[6].Visible == true)
                        gv.CurrentRow.Cells[6].Value = gv.CurrentCell.Value.ToString();
                    if (gv.Columns[7].Visible == true)
                        gv.CurrentRow.Cells[7].Value = gv.CurrentCell.Value.ToString();
                    if (gv.Columns[8].Visible == true)
                        gv.CurrentRow.Cells[8].Value = gv.CurrentCell.Value.ToString();
                }
                else if (gv.CurrentCell.ColumnIndex == 6 && gv.Columns[6].Visible == true)
                {
                    if (gv.Columns[7].Visible == true)
                        gv.CurrentRow.Cells[7].Value = gv.CurrentCell.Value.ToString();
                    if (gv.Columns[8].Visible == true)
                        gv.CurrentRow.Cells[8].Value = gv.CurrentCell.Value.ToString();
                }
                else if (gv.CurrentCell.ColumnIndex == 7 && gv.Columns[7].Visible == true)
                {
                    if (gv.Columns[8].Visible == true)
                        gv.CurrentRow.Cells[8].Value = gv.CurrentCell.Value.ToString();
                }
                
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnSave.Focus();
            }
        }
    }
}
