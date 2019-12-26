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
    public partial class DecimalRoundOffSettings : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();

        DBMSettings dbMSettings = new DBMSettings();
        
        string[] arraySrNo = new string[] { "Sale_Rate", "Purchase_Rate", "Sale_Subtotal", "Purchase_Subtotal", "Sale_Grandtotal", "Purchase_Grandtotal", "Sale_TaxAmount", "Purchase_TaxAmount", "Sale_TaxItemWise", "Purchase_TaxItemWise", "Sale_DiscountAmount", "Purchase_DiscountAmount", "Sale_DiscountItemWise", "Purchase_DiscountItemWise", "Sale_Qty", "Purchase_Qty" };

        public delegate void MovetoNext(int RowIndex, int ColIndex);

        public void m2n(int RowIndex, int ColIndex)
        {
            dgDecimalRoundOffSettings.CurrentCell = dgDecimalRoundOffSettings.Rows[RowIndex].Cells[ColIndex];
        }

        public DecimalRoundOffSettings()
        {
            InitializeComponent();
        }
        
        private void Decimal_RoundOff_Settings_Load(object sender, EventArgs e)
        {
           this.dgDecimalRoundOffSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));
           FillDefaultGrid();

           DataGridViewColumn column = dgDecimalRoundOffSettings.Columns[ColIndex.SrNo];
           column.Width = 200;

           MovetoNext move2n = new MovetoNext(m2n);
           BeginInvoke(move2n, new object[] { 0, ColIndex.DecimalDigits });
        }
        
        private void FillDefaultGrid()
        {
            DataTable dt = new DataTable();
            DataRow row;

             
            DataColumn colSrNo = new DataColumn("SrNo");
            DataColumn colDecimalDigits = new DataColumn("DecimalDigits");
            DataColumn colRoundOffDecimalDigits = new DataColumn("RoundOffDecimalDigits");
            DataColumn colRoundOffType = new DataColumn("RoundOffType");

            dt.Columns.Add(colSrNo);
            dt.Columns.Add(colDecimalDigits);
            dt.Columns.Add(colRoundOffDecimalDigits);
            dt.Columns.Add(colRoundOffType);

            for (int i = 0; i < 16; i++)
            {
                row = dt.NewRow();
                row[colSrNo] = arraySrNo[i];
                row[colDecimalDigits] = 0;
                row[colRoundOffDecimalDigits] = 0;
                row[colRoundOffType] = 0;
                dt.Rows.Add(row);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgDecimalRoundOffSettings.Rows.Add();
                for (int j = 0; j < dgDecimalRoundOffSettings.ColumnCount; j++)
                {
                    dgDecimalRoundOffSettings.Rows[i].Cells[0].Value = dt.Rows[i].ItemArray[0].ToString();

                }

            }
            FillComboAllMasters(1);
            FillComboAllMasters(2);
            FillComboAllMasters(3);

            SetDefaultComboValues();

        }

        private void FillComboAllMasters(int i)
        {   
                
            bool flag = false; 
            if (i == 1)
            {
                DataGridViewComboBoxColumn cmbDecimalDigits = dgDecimalRoundOffSettings.Columns[ColIndex.DecimalDigits] as DataGridViewComboBoxColumn;
                if (cmbDecimalDigits != null)
                {
                    FillCombo(cmbDecimalDigits,flag);
                }
                
                
            }
            else if (i == 2)
            {
                DataGridViewComboBoxColumn cmbRoundOffDecimalDigits = dgDecimalRoundOffSettings.Columns[ColIndex.RoundOffDecimalDigits] as DataGridViewComboBoxColumn;
                if (cmbRoundOffDecimalDigits != null)
                {
                    FillCombo(cmbRoundOffDecimalDigits,flag);
                }
            }
            else if (i == 3)
            {
                flag = true;
                DataGridViewComboBoxColumn cmbRoundOffType = dgDecimalRoundOffSettings.Columns[ColIndex.RoundOffType] as DataGridViewComboBoxColumn;
                if (cmbRoundOffType != null)
                {
                    FillCombo(cmbRoundOffType,flag);
                }
            }

            
        }

        public void FillCombo(DataGridViewComboBoxColumn cmb,bool flag)
        {
            string[] arrayRoundOffType = new string[] { "Ceil", "Floor", "Normal" };
            int[] arrayDecimal = new int[] { 0, 1, 2, 3, 4 };
        

            if (flag)
            {
                for (int i = 0; i < 3; i++)
                {
                    cmb.Items.Add(arrayRoundOffType[i].ToString());
                    cmb.ValueMember = i.ToString();

                }
                
                
            }else 
            {
                for (int i = 0; i < 5; i++)
                {
                    cmb.Items.Add(arrayDecimal[i].ToString());
                    cmb.ValueMember = i.ToString();

                }
            }
        }

        public void SetDefaultComboValues()
        {
            //for (int i = 0; i < dgDecimalRoundOffSettings.RowCount; i++)
            //{
            //    dgDecimalRoundOffSettings.Rows[i].Cells[ColIndex.DecimalDigits].Value = "4";
            //    dgDecimalRoundOffSettings.Rows[i].Cells[ColIndex.RoundOffDecimalDigits].Value = "4";
            //    dgDecimalRoundOffSettings.Rows[i].Cells[ColIndex.RoundOffType].Value = "Floor";
            //}

            string strQuery = "select SettingKeyCode , SettingValue from MSettings where PkSettingNo>99 and PkSettingNo<148 order by PkSettingNo";
            DataTable dt = new DataTable();
            dt = ObjFunction.GetDataView(strQuery).Table;

            DataRow row;
            //All rows in datatable
            for (int i = 0; i < dt.Rows.Count; i++)
            {   //filling sales rows and Decimaldigits column cells
                for (int j = 0; j < dgDecimalRoundOffSettings.RowCount; j += 2)
                {
                    row = dt.Rows[i++];
                    dgDecimalRoundOffSettings.Rows[j].Cells[ColIndex.DecimalDigits].Value = row["SettingValue"].ToString();
                }
                //filling sales rows and RoundOffDecimalDigits column cells
                for (int j = 0; j < dgDecimalRoundOffSettings.RowCount; j += 2)
                {
                    row = dt.Rows[i++];
                    dgDecimalRoundOffSettings.Rows[j].Cells[ColIndex.RoundOffDecimalDigits].Value = row["SettingValue"].ToString();
                }
                //filling sales rows and RoundOffType column cells
                for (int j = 0; j < dgDecimalRoundOffSettings.RowCount; j += 2)
                {
                    row = dt.Rows[i++];
                    dgDecimalRoundOffSettings.Rows[j].Cells[ColIndex.RoundOffType].Value = row["SettingValue"].ToString();
                }
                //filling purchase rows and DecimalDigits column cells
                for (int j = 1; j < dgDecimalRoundOffSettings.RowCount; j += 2)
                {
                    row = dt.Rows[i++];
                    dgDecimalRoundOffSettings.Rows[j].Cells[ColIndex.DecimalDigits].Value = row["SettingValue"].ToString();
                }
                //filling purchase rows and RoundOffDecimalDigits column cells
                for (int j = 1; j < dgDecimalRoundOffSettings.RowCount; j += 2)
                {
                    row = dt.Rows[i++];
                    dgDecimalRoundOffSettings.Rows[j].Cells[ColIndex.RoundOffDecimalDigits].Value = row["SettingValue"].ToString();
                }
                //filling purchase rows and RoundOffType column cells
                for (int j = 1; j < dgDecimalRoundOffSettings.RowCount; j += 2)
                {
                    row = dt.Rows[i++];
                    dgDecimalRoundOffSettings.Rows[j].Cells[ColIndex.RoundOffType].Value = row["SettingValue"].ToString();
                }

            }
        }

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int DecimalDigits = 1;
            public static int RoundOffDecimalDigits = 2;
            public static int RoundOffType = 3;
        }
        #endregion

        #region RowIndex
        public static class RowIndex
        {
            public static int S_Rate = 0;
            public static int P_Rate = 1;
            public static int S_Subtotal = 2;
            public static int P_Subtotal = 3;
            public static int S_Grandtotal = 4;
            public static int P_Grandtotal = 5;
            public static int S_TaxAmount = 6;
            public static int P_TaxAmount = 7;
            public static int S_TaxItemWise = 8;
            public static int P_TaxItemWise = 9;
            public static int S_DiscountAmount = 10;
            public static int P_DiscountAmount = 11;
            public static int S_DiscountItemWise = 12;
            public static int P_DiscountItemWise = 13;
            public static int S_Qty = 14;
            public static int P_Qty = 15;
        }
        #endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {   //Row=0
            dbMSettings.AddAppSettings(AppSettings.S_Rate_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Rate].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Rate_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Rate].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Rate_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_Rate].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=1
            dbMSettings.AddAppSettings(AppSettings.P_Rate_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Rate].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Rate_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Rate].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Rate_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_Rate].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=2
            dbMSettings.AddAppSettings(AppSettings.S_Subtotal_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Subtotal].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Subtotal_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Subtotal].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Subtotal_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_Subtotal].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=3
            dbMSettings.AddAppSettings(AppSettings.P_Subtotal_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Subtotal].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Subtotal_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Subtotal].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Subtotal_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_Subtotal].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=4
            dbMSettings.AddAppSettings(AppSettings.S_Grandtotal_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Grandtotal].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Grandtotal_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Grandtotal].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Grandtotal_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_Grandtotal].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=5
            dbMSettings.AddAppSettings(AppSettings.P_Grandtotal_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Grandtotal].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Grandtotal_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Grandtotal].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Grandtotal_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_Grandtotal].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=6
            dbMSettings.AddAppSettings(AppSettings.S_TaxAmount_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_TaxAmount].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_TaxAmount_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_TaxAmount].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_TaxAmount_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_TaxAmount].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=7
            dbMSettings.AddAppSettings(AppSettings.P_TaxAmount_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_TaxAmount].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_TaxAmount_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_TaxAmount].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_TaxAmount_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_TaxAmount].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=8
            dbMSettings.AddAppSettings(AppSettings.S_TaxItemWise_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_TaxItemWise].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_TaxItemWise_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_TaxItemWise].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_TaxItemWise_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_TaxItemWise].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=9
            dbMSettings.AddAppSettings(AppSettings.P_TaxItemWise_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_TaxItemWise].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_TaxItemWise_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_TaxItemWise].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_TaxItemWise_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_TaxItemWise].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=10
            dbMSettings.AddAppSettings(AppSettings.S_DiscountAmount_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_DiscountAmount].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_DiscountAmount_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_DiscountAmount].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_DiscountAmount_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_DiscountAmount].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=11
            dbMSettings.AddAppSettings(AppSettings.P_DiscountAmount_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_DiscountAmount].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_DiscountAmount_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_DiscountAmount].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_DiscountAmount_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_DiscountAmount].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=12
            dbMSettings.AddAppSettings(AppSettings.S_DiscountItemWise_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_DiscountItemWise].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_DiscountItemWise_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_DiscountItemWise].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_DiscountItemWise_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_DiscountItemWise].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=13
            dbMSettings.AddAppSettings(AppSettings.P_DiscountItemWise_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_DiscountItemWise].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_DiscountItemWise_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_DiscountItemWise].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_DiscountItemWise_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_DiscountItemWise].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=14
            dbMSettings.AddAppSettings(AppSettings.S_Qty_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Qty].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Qty_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.S_Qty].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.S_Qty_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.S_Qty].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
            //Row=15
            dbMSettings.AddAppSettings(AppSettings.P_Qty_DecimalDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Qty].Cells[ColIndex.DecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Qty_RoundOffDigits, dgDecimalRoundOffSettings.Rows[RowIndex.P_Qty].Cells[ColIndex.RoundOffDecimalDigits].FormattedValue.ToString());
            dbMSettings.AddAppSettings(AppSettings.P_Qty_RoundOffType, dgDecimalRoundOffSettings.Rows[RowIndex.P_Qty].Cells[ColIndex.RoundOffType].FormattedValue.ToString());
          
            if (dbMSettings.ExecuteNonQueryStatements() == true)
            {
                ObjFunction.SetAppSettings();
                OMMessageBox.Show("Sales Setting Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                ObjFunction.SetAppSettings();
                
            }
        }

    }
}
