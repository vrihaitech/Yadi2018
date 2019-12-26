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

namespace Yadi.Utilities
{
    public partial class StockEntry : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

       DBMItemMaster dbMItemMaster = new DBMItemMaster();
           MItemMaster mItemMaster = new MItemMaster();
       
        MRateSetting mRateSetting = new MRateSetting();
        DBTVaucherEntry dbTVoucherEntry = new DBTVaucherEntry();
        TVoucherEntry tVoucherEntry = new TVoucherEntry();
        TStock tStock = new TStock();
        TStockGodown tStockGodown = new TStockGodown();

        //int iItemNameStartIndex = 3, ItemType = 0;
        bool StopOnQty = true;//, StopOnRate=true;

        int rowQtyIndex;//cntRow, BillingMode,
        //long LastBillNo = 0;
        bool Spaceflag = true;//, BillSizeFlag = false;
        //long ItemNameType = 0, RateTypeNo, PrintAsk, PartyNo, PayType, ParkBillNo, OrderType = 0;/*bcdno,*/
       
        //string strUom, Param1Value = "", Param2Value = "";
        //string[] strItemQuery, strItemQuery_last;

        DataTable dtTable = new DataTable();
        DataTable dtTableTemp = new DataTable();
        List<string> M = new List<string>();
        public SqlCommand cmd;
        public SqlConnection con;
        DataSet dsDatabase;
        SqlDataAdapter da;

        string strQuery = "", strConnect = "", strDest = "", strSource = "";


        public StockEntry()
        {
            InitializeComponent();
        }

        private void StockEntry_Load(object sender, EventArgs e)
        {
            
            dgBill.Rows.Add();
            dgBill.CurrentCell = dgBill[1, 0];
            dgBill.Enabled = true;
        }

        #region dgBill Methods and Events
        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
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


        private void Desc_Start()
        {
            try
            {
                bool flagBr = true;
                long[] BarcodeNo = null; long[] ItemNo = null;
                string StrBarcode = dgBill.CurrentCell.Value.ToString();

                //dgBill.CurrentRow.Cells[ColIndex.PkRateSettingNo].Value = 0;

                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.Barcode].Value != null)
                    {
                        if (dgBill.Rows[i].Cells[ColIndex.Barcode].Value.ToString() == StrBarcode)
                        {
                            flagBr = false;
                            break;
                        }
                        else
                            flagBr = true;
                    }

                }
                long BrNo = ObjQry.ReturnLong("Select PkStockBarcodeNo from MStockBarcode where Barcode='" + StrBarcode + "'", CommonFunctions.ConStr);
                if (flagBr == true && BrNo == 0)
                {
                    SearchBarcode(dgBill.CurrentCell.Value.ToString().Trim(), out ItemNo, out BarcodeNo);
                }
                if (flagBr == false)
                {
                    dgBill.CurrentCell.Value = null;
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = "0";
                    DisplayMessage("Barcode already Exist");
                }
                else if (BrNo != 0)
                {
                    dgBill.CurrentCell.Value = null;
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = "0";
                    DisplayMessage(" Barcode already Exist");
                }
                else if (ItemNo.Length == 0 || BarcodeNo.Length == 0)
                {
                    dgBill.CurrentCell.Value = null;
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.PkBarCodeNo].Value = "0";
                    DisplayMessage("Barcode Not Found");
                }
                else
                {
                    //int rwindex = 0;
                    Desc_MoveNext(ItemNo[0], BarcodeNo[0]);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void SearchBarcode(String strBarcode, out long[] ItemNo, out long[] BarcodeNo)
        {
           
            //string sql = " SELECT   0 as SrNo,  MStockItemsTemp.ItemName, 1 AS Quantity, MRateSettingTemp.ASaleRate, MStockBarcodeTemp.Barcode,  " +
            //          " MStockBarcodeTemp.PkStockBarcodeNo, MStockItemsTemp.ItemNo ,MRateSettingTemp.PkSrNo AS RateSettingNo , MUOM.UOMNo" +
            //          " FROM MStockBarcodeTemp INNER JOIN " +
            //          " MStockItemsTemp INNER JOIN MUOM INNER JOIN MRateSettingTemp ON MUOM.UOMNo = MRateSettingTemp.UOMNo ON MStockItemsTemp.ItemNo = MRateSettingTemp.ItemNo ON " +
            //          " MStockBarcodeTemp.PkStockBarcodeNo = MRateSettingTemp.FkBcdSrNo where Barcode = '" + strBarcode + "' And MRateSettingTemp.IsActive ='true'";
            string sql = " SELECT   0 as SrNo,  MItemGroup.StockGroupName + ' ' + MStockGroup_1.StockGroupName + ' ' + MStockItemsTemp.ItemName AS ItemName, 1 AS Quantity, MRateSettingTemp.ASaleRate, MStockBarcodeTemp.Barcode,  " +
                      " MStockBarcodeTemp.PkStockBarcodeNo, MStockItemsTemp.ItemNo ,MRateSettingTemp.PkSrNo AS RateSettingNo , MUOM.UOMNo" +
                      " FROM  MStockGroup INNER JOIN MStockBarcodeTemp INNER JOIN MStockItemsTemp INNER JOIN MUOM INNER JOIN "+
                      " MRateSettingTemp ON MUOM.UOMNo = MRateSettingTemp.UOMNo ON MStockItemsTemp.ItemNo = MRateSettingTemp.ItemNo ON "+
                      " MStockBarcodeTemp.PkStockBarcodeNo = MRateSettingTemp.FkBcdSrNo ONmItemGroup.ItemGroupNo  =  MStockItemsTemp.GroupNo INNER JOIN "+
                      " MStockGroup AS MStockGroup_1 ON MStockItemsTemp.GroupNo1 = MStockGroup_1.StockGroupNo where Barcode = '" + strBarcode + "' And MRateSettingTemp.IsActive ='true'";
            DataTable dt = ObjFunction.GetDataView(sql).Table;
            BarcodeNo = new long[dt.Rows.Count];
            ItemNo = new long[dt.Rows.Count];
             if (dt.Rows.Count > 0)
            {
            for(int i=1;i<dt.Columns.Count;i++)
                dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[i].Value=dt.Rows[0].ItemArray[i].ToString();
           
            ItemNo[0] = Convert.ToInt64(dt.Rows[0].ItemArray[ColIndex.ItemNo].ToString());
            BarcodeNo[0] = Convert.ToInt64(dt.Rows[0].ItemArray[ColIndex.PkBarCodeNo].ToString());
                
            }
            else
            {
                //ItemNo[0] = 0;
                //BarcodeNo[0] = 0;
            }
        }

        private void Desc_MoveNext(long ItemNo, long BarcodeNo)
        {
            try
            {
                if (StopOnQty == true)
                {
                    //if (dgBill[2, dgBill.CurrentCell.RowIndex].Value == null || dgBill[2,dgBill.CurrentCell.RowIndex].Value=="0")
                    //{
                    dgBill.CurrentCell = dgBill[2, dgBill.CurrentCell.RowIndex];
                    dgBill.Focus();
                    //}
                    //else
                    //    Qty_MoveNext();
                }
                else
                {
                    dgBill.Rows[dgBill.CurrentCell.RowIndex].Cells[ColIndex.Quantity].Value = "1";
                    Qty_MoveNext();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void Qty_MoveNext()
        {
            try
            {
                rowQtyIndex = dgBill.CurrentCell.RowIndex;
                if (dgBill.Rows[rowQtyIndex].Cells[ColIndex.ItemName].Value != null && dgBill.Rows[rowQtyIndex].Cells[ColIndex.ItemName].Value.ToString() != "")
                {
                    dgBill.Rows.Add();

                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { rowQtyIndex + 1, ColIndex.ItemName, dgBill });
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }
    
        private void dgBill_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Spaceflag == false) { Spaceflag = true; return; }
                if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                {
                    Desc_Start();
                }
                else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                {
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (ObjFunction.CheckValidAmount(dgBill.CurrentCell.Value.ToString()) == true)
                        {
                            if (Convert.ToDouble(dgBill.CurrentCell.Value.ToString()) == 0) dgBill.CurrentCell.Value = "1";
                        }
                        else
                            dgBill.CurrentCell.Value = "1";
                    }
                    Qty_MoveNext();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    dgBill.Focus();
                    if (dgBill.CurrentCell.ColumnIndex == ColIndex.ItemName)
                    {
                        e.SuppressKeyPress = true;
                        dgBill.CurrentCell.Value = "";
                        Desc_Start();
                    }
                    else if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
                    {
                        e.SuppressKeyPress = true;
                        if (dgBill.CurrentCell.Value == null) dgBill.CurrentCell.Value = "1";
                        Qty_MoveNext();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    BtnSave.Focus();
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    dgBill.Rows.RemoveAt(dgBill.CurrentCell.RowIndex);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgBill_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row, col;
                if (dgBill.CurrentCell != null)
                { col = dgBill.CurrentCell.ColumnIndex; row = dgBill.CurrentCell.RowIndex; }
                else { col = e.ColumnIndex; row = e.RowIndex; }
                if (dgBill.Rows.Count > 0)
                    dgBill.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "";
                if (col == ColIndex.Quantity && row >= 0)
                {
                    dgBill.CurrentCell.ErrorText = "";
                    if (dgBill.CurrentCell.Value != null)
                    {
                        if (dgBill.CurrentCell.Value.ToString() != "" && dgBill.CurrentCell.Value.ToString() != "0")
                        {
                            if (ObjFunction.CheckNumeric(dgBill.CurrentCell.Value.ToString()) == true)
                            {
                                int rowIndex = dgBill.CurrentCell.RowIndex;
                                if (dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex].Value == null || Convert.ToString(dgBill[ColIndex.Rate, dgBill.CurrentCell.RowIndex].Value) == "")


                                    dgBill.Focus();

                                dgBill.CurrentCell = dgBill[2, row];
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

        private void dgBill_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt = (TextBox)e.Control;
            txt.KeyDown += new KeyEventHandler(txtSpace_KeyDown);
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
                //txt1.TextChanged -= new EventHandler(txtQuantity_TextChanged);
            }
            
        }

        public void txtSpace_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    Spaceflag = false;
                    if (dgBill.CurrentCell.RowIndex == 0)
                    {
                        if (dgBill.CurrentCell.ColumnIndex != 0)
                            dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex - 1, dgBill.CurrentCell.RowIndex];
                    }
                    else
                    {

                        if (dgBill.CurrentCell.ColumnIndex == 1)
                            dgBill.CurrentCell = dgBill[4, dgBill.CurrentCell.RowIndex - 1];
                        else if (dgBill.CurrentCell.ColumnIndex != 0)
                            dgBill.CurrentCell = dgBill[dgBill.CurrentCell.ColumnIndex - 1, dgBill.CurrentCell.RowIndex];
                    }
                }
                TextBox txt = (TextBox)sender;
                txt.KeyDown -= new KeyEventHandler(txtSpace_KeyDown);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

       
        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMasked((TextBox)sender, 2);
            if (dgBill.CurrentCell.ColumnIndex == ColIndex.Quantity)
            {
                ObjFunction.SetMasked((TextBox)sender, 2, 4, OMFunctions.MaskedType.NotNegative);
            }
        }

      

        #endregion

        #region ColumnIndex
        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int Quantity = 2;
            
            public static int Rate = 3;
            public static int Barcode = 4;
            public static int PkBarCodeNo = 5;
            public static int ItemNo = 6;
                
            public static int PkRateSettingNo = 7;
            public static int UomNo = 8;
            
          
        }
        #endregion

        private void dgBill_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
                e.Value = (e.RowIndex + 1).ToString();
        }

        public string GetColumns(string tableName)
        {
            try
            {
                //         strConnect = "Server=.\\SQLEXPRESS;Initial Catalog=Retailer0002;Integrated Security=SSPI;Pooling=False";
                strConnect = CommonFunctions.ConStr;

                con = new SqlConnection(strConnect);
                con.Open();

                cmd = new SqlCommand();
                cmd.CommandText = "sp_Columns " + tableName;
                cmd.CommandType = CommandType.Text;

                da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.SelectCommand.Connection = con;

                dsDatabase = new DataSet();
                da.Fill(dsDatabase, "Columns");
                string str = "";
                for (int i = 0; i < dsDatabase.Tables["Columns"].Rows.Count; i++)
                {
                    if (i == 0)
                        str = Convert.ToString(dsDatabase.Tables[0].Rows[i].ItemArray[3]);
                    else
                        str = str + "," + Convert.ToString(dsDatabase.Tables[0].Rows[i].ItemArray[3]);


                }
                return str;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return "";
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = false; int cnt = -1; int VoucherSrNo = 1;
                dbTVoucherEntry = new DBTVaucherEntry();
                if (dgBill.Rows.Count <= 0)
                {
                    OMMessageBox.Show("No Data To Save", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    return;
                }
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.ItemName].Value != null && dgBill.Rows[i].Cells[ColIndex.ItemName].Value.ToString() != "")
                    {
                        strQuery = "";
                        strSource = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MStockItemsTemp";
                        strDest = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MStockItems";
                        strQuery = "delete from " + strDest + " where ItemNo=" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) + " set Identity_Insert " + strDest + " ON  ";

                        strQuery = strQuery + "insert " + strDest + " (" + GetColumns("MStockItems") + " )";

                        strQuery = strQuery + " Select * from " + strSource + " where ItemNo=" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) + "";

                        strQuery = strQuery + " set Identity_Insert " + strDest + " OFF ";
                        if (ObjTrans.Execute(strQuery, CommonFunctions.ConStrServer))
                        {
                            strQuery = "";

                            strSource = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MStockBarcodeTemp";
                            strDest = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MStockBarcode";
                            strQuery = "delete from " + strDest + " where PkStockBarcodeNo=" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkBarCodeNo].Value) + " set Identity_Insert " + strDest + " ON  ";

                            strQuery = strQuery + "insert " + strDest + " (" + GetColumns("MStockBarcode") + " )";

                            strQuery = strQuery + " Select * from " + strSource + " where PkStockBarcodeNo=" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkBarCodeNo].Value) + "";

                            strQuery = strQuery + " set Identity_Insert " + strDest + " OFF ";
                            if (ObjTrans.Execute(strQuery, CommonFunctions.ConStrServer))
                            {
                                strQuery = "";
                                strSource = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MRateSettingTemp";
                                strDest = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MRateSetting";
                                strQuery = "delete from " + strDest + " where PkSrNo=" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value) + " set Identity_Insert " + strDest + " ON  ";

                                strQuery = strQuery + "insert " + strDest + " (" + GetColumns("MRateSetting") + " )";

                                strQuery = strQuery + " Select * from " + strSource + " where PkSrNo=" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value) + "";

                                strQuery = strQuery + " set Identity_Insert " + strDest + " OFF ";
                                if (ObjTrans.Execute(strQuery, CommonFunctions.ConStrServer))
                                {
                                    DataTable dttax = ObjFunction.GetDataView("Select PkSrNo from MItemTaxInfoTemp where ItemNo=" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) + " order by PkSrNo").Table;
                                    for (int j = 0; j < dttax.Rows.Count; j++)
                                    {
                                        strQuery = "";
                                        strSource = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MItemTaxInfoTemp";
                                        strDest = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo.MItemTaxInfo";
                                        strQuery = "delete from " + strDest + " where PkSrNo =" + Convert.ToInt64(dttax.Rows[j].ItemArray[0].ToString()) + " set Identity_Insert " + strDest + " ON  ";

                                        strQuery = strQuery + "insert " + strDest + " (" + GetColumns("MItemTaxInfo") + " )";

                                        strQuery = strQuery + " Select * from " + strSource + " where PkSrNo=" + Convert.ToInt64(dttax.Rows[j].ItemArray[0].ToString()) + "";

                                        strQuery = strQuery + " set Identity_Insert " + strDest + " OFF ";
                                        if (ObjTrans.Execute(strQuery, CommonFunctions.ConStrServer))
                                            flag = true;
                                        else
                                            flag = false;
                                    }
                                    if (flag == true)
                                    {
                                        DataTable dt = ObjFunction.GetDataView("SELECT  t.PkSrNo,t.Percentage FROM MRateSetting As r,GetItemTaxAll(" + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.ItemNo].Value) + ", NULL, " + GroupType.SalesAccount + ",null,NULL) As t " +
                                                   " WHERE r.PkSrNo = " + Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value) + " AND r.ItemNo = t.ItemNo").Table;
                                        if (cnt == -1)
                                        {
                                            long Id = ObjQry.ReturnLong("Select isNull(max(PkVoucherNo),0) from TVoucherEntry where VoucherTypeCode=0", CommonFunctions.ConStr);

                                            tVoucherEntry = new TVoucherEntry();
                                            tVoucherEntry.PkVoucherNo = Id;
                                            tVoucherEntry.VoucherTypeCode = 0;
                                            tVoucherEntry.VoucherUserNo = 0;
                                            tVoucherEntry.VoucherDate = Convert.ToDateTime("1-1-1900");
                                            tVoucherEntry.VoucherTime = DBGetVal.ServerTime;
                                            tVoucherEntry.Narration = "Sales Bill";
                                            tVoucherEntry.Reference = "";
                                            tVoucherEntry.ChequeNo = 0;
                                            tVoucherEntry.ClearingDate = DBGetVal.ServerTime.Date;
                                            tVoucherEntry.CompanyNo = DBGetVal.FirmNo;
                                            tVoucherEntry.BilledAmount = 0;
                                            tVoucherEntry.ChallanNo = "";
                                            tVoucherEntry.Remark = "";
                                            tVoucherEntry.MacNo = DBGetVal.MacNo;
                                            tVoucherEntry.PayTypeNo = 0;
                                            tVoucherEntry.RateTypeNo = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.P_RateType));
                                            tVoucherEntry.TaxTypeNo = 0;

                                            tVoucherEntry.TransporterCode = 0;
                                            tVoucherEntry.TransPayType = 0;
                                            tVoucherEntry.LRNo = "";
                                            tVoucherEntry.TransportMode = 0;
                                            tVoucherEntry.TransNoOfItems = 0;

                                            tVoucherEntry.UserID = DBGetVal.UserID;
                                            tVoucherEntry.UserDate = DBGetVal.ServerTime.Date;
                                            dbTVoucherEntry.AddTVoucherEntry(tVoucherEntry);
                                        }
                                        if (dt.Rows.Count > 0)
                                        {
                                            tStock = new TStock();
                                            tStock.PkStockTrnNo = 0;
                                            tStock.GroupNo = GroupType.CapitalAccount;
                                            tStock.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                                            tStock.FkVoucherSrNo = VoucherSrNo;
                                            tStock.TrnCode = 1;
                                            tStock.Quantity = Convert.ToInt64(dgBill[ColIndex.Quantity, i].Value.ToString());
                                            tStock.BilledQuantity = Convert.ToInt64(dgBill[ColIndex.Quantity, i].Value.ToString());
                                            tStock.Rate = Convert.ToDouble(dgBill.Rows[i].Cells[ColIndex.Rate].Value);
                                            tStock.Amount = tStock.Rate * tStock.Quantity;
                                            tStock.SGSTPercentage = Convert.ToDouble(dt.Rows[0].ItemArray[1].ToString());
                                            tStock.SGSTAmount = Convert.ToDouble(((tStock.Amount * tStock.SGSTPercentage) / (100 + tStock.SGSTPercentage)).ToString("0.00"));
                                            tStock.DiscPercentage = 0;
                                            tStock.DiscAmount = 0;
                                            tStock.DiscRupees = 0;
                                            tStock.DiscPercentage2 = 0;
                                            tStock.DiscAmount2 = 0;
                                            tStock.DiscRupees2 = 0;
                                            tStock.NetRate = tStock.Rate - tStock.SGSTAmount;
                                            tStock.NetAmount = tStock.Quantity * tStock.NetRate;
                                            tStock.FkStockBarCodeNo = Convert.ToInt64(dgBill[ColIndex.PkBarCodeNo, i].Value.ToString());
                                            tStock.FkUomNo = Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.UomNo].Value);
                                            tStock.FkRateSettingNo = Convert.ToInt64(dgBill[ColIndex.PkRateSettingNo, i].Value.ToString());
                                            tStock.FkItemTaxInfo = Convert.ToInt64(dt.Rows[0].ItemArray[0].ToString());
                                            tStock.UserID = DBGetVal.UserID;
                                            tStock.UserDate = DBGetVal.ServerTime.Date;
                                            tStock.CompanyNo = DBGetVal.FirmNo;
                                            tStock.LandedRate = 0;
                                            //tStock.DisplayItemName = "";
                                            dbTVoucherEntry.AddTStock(tStock);

                                            tStockGodown = new TStockGodown();
                                            tStockGodown.PKStockGodownNo = 0;
                                            tStockGodown.ItemNo = Convert.ToInt64(dgBill[ColIndex.ItemNo, i].Value.ToString());
                                            tStockGodown.GodownNo = 2;
                                            tStockGodown.Qty = Convert.ToInt64(dgBill[ColIndex.Quantity, i].Value.ToString());
                                            tStockGodown.ActualQty = Convert.ToInt64(dgBill[ColIndex.Quantity, i].Value.ToString());
                                            tStockGodown.UserID = DBGetVal.UserID;
                                            tStockGodown.UserDate = DBGetVal.ServerTime.Date;
                                            tStockGodown.CompanyNo = DBGetVal.FirmNo;
                                            dbTVoucherEntry.AddTStockGodown(tStockGodown);
                                            cnt++; VoucherSrNo++;
                                        }

                                    }

                                }
                            }
                        }
                    }

                }
                long tempId = dbTVoucherEntry.ExecuteNonQueryStatements();
                if (tempId != 0)
                {
                    OMMessageBox.Show("Items Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    dgBill.Rows.Clear();
                    dgBill.Rows.Add();
                    dgBill.CurrentCell = dgBill[1, 0];
                    dgBill.Enabled = true;
                }
                else
                    OMMessageBox.Show("Items Not Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dgBill.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
