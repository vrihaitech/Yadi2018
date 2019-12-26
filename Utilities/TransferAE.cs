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
    public partial class TransferAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTablesCollection dtColletion = new DataTablesCollection();
        DataTablesCollection dtColletionRef = new DataTablesCollection();
        DataTablesCollection dtCollectionLedg = new DataTablesCollection();
        CommandCollection commandcollection = new CommandCollection();
        DataTable dtVoucher = new DataTable();

        DataTable dtTable = new DataTable();
        DataTable dtTableTemp = new DataTable();
        List<string> M = new List<string>();
        public SqlCommand cmd;
        public SqlConnection con;
        DataSet dsDatabase;
        SqlDataAdapter da;
        bool flag = false;
        
        string strQuery = "", strConnect = "", strDest = "", strSource = "";
        //string[] TableName = { "MPayType","MPayTypeDetails","MStockItems","MRateSetting","MStockBarcode","MItemTaxInfo","MItemTaxSetting",
        //                       "MTransporterMode","MTransporterPayType",  
        //                       "MOtherBank","MGroup","MStockGroup","MStockCategory","MStockDepartment","MMailSettings","MLedger"};

        string[] TableName = { "MStockItems", "MRateSetting", "MStockBarcode", "MItemTaxInfo" };
        string[] TableNameTemp = { "MStockItemsTemp", "MRateSettingTemp", "MStockBarcodeTemp", "MItemTaxInfoTemp" };
        public TransferAE()
        {
            InitializeComponent();
            dtTable.Columns.Add("SrNo");
            dtTable.Columns.Add("TableName");
            dtTableTemp.Columns.Add("SrNo");
            dtTableTemp.Columns.Add("TableName");
            AddTableInDatatable(dtTable, TableName);
            AddTableInDatatable(dtTableTemp, TableNameTemp);
        }

        private void TransferAE_Load(object sender, EventArgs e)
        {
            rbType_CheckedChanged(null, null);
        }

        #region Export Related Methods

        public void Export()
        {
           
            DataTable dtTable = new DataTable();
            DataTable dtDetails = ObjFunction.GetDataView("Select * from TVoucherDetails").Table;
            DataTable dtStock = ObjFunction.GetDataView("Select * from TStock").Table;
            DataTable dtStockGodown = ObjFunction.GetDataView("Select * from TStockGodown").Table;
            DataTable dtRefDetails = ObjFunction.GetDataView("Select * from TVoucherRefDetails").Table;
            DataTable dtPayType = ObjFunction.GetDataView("Select * from TVoucherPayTypeDetails").Table;
            DataTable dtChqCr = ObjFunction.GetDataView("Select * from TVoucherChqCreditDetails").Table;
            DataRow[] DtlsRows;
            DataRow[] StockRows;
            //DataRow[] StockGodowns;
            DataRow[] DetailsRefRows;
            DataRow[] PayTypeRows;
            DataRow[] ChqCrRows;

            if (dtVoucher.Rows.Count > 0)
            {
                PB.Minimum = 1; PB.Maximum = dtVoucher.Rows.Count + 1;
                PB.Step = 1;
                if (PB.InvokeRequired)
                {
                    PB.Invoke(new MethodInvoker(delegate { PB.Visible = true; }));
                }
                else
                PB.Visible = true;

                for (int i = 0; i < dtVoucher.Rows.Count; i++)
                {
                    long PKVoucherNo = 0;
                    commandcollection = new CommandCollection();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AddTVoucherEntry";
                    for (int col = 0; col < dtVoucher.Columns.Count; col++)
                    {
                        if (dtVoucher.Columns[col].ColumnName == "PkVoucherNo")
                        {
                            PKVoucherNo = Convert.ToInt64(dtVoucher.Rows[i].ItemArray[col].ToString());
                            //    cmd.Parameters.AddWithValue("@" + dtVoucher.Columns[col].ColumnName + "", 0);
                        }
                        //else 
                        // if (dtVoucher.Columns[col].ColumnName != "ModifiedBy" && dtVoucher.Columns[col].ColumnName != "IsVoucherLock" && dtVoucher.Columns[col].ColumnName != "IsCancel" && dtVoucher.Columns[col].ColumnName != "VoucherStatus")
                        if (dtVoucher.Columns[col].ColumnName != "ModifiedBy" && dtVoucher.Columns[col].ColumnName != "IsVoucherLock" && dtVoucher.Columns[col].ColumnName != "IsCancel" && dtVoucher.Columns[col].ColumnName != "VoucherStatus")
                            cmd.Parameters.AddWithValue("@" + dtVoucher.Columns[col].ColumnName + "", dtVoucher.Rows[i].ItemArray[col].ToString());

                    }
                    SqlParameter outParameter = new SqlParameter();
                    outParameter.ParameterName = "@ReturnID";
                    outParameter.Direction = ParameterDirection.Output;
                    outParameter.DbType = DbType.Int32;
                    cmd.Parameters.Add(outParameter);
                    commandcollection.Add(cmd);
                    DtlsRows = dtDetails.Select("FkVoucherNo=" + Convert.ToInt64(dtVoucher.Rows[i].ItemArray[0].ToString()) + "");
                    foreach (DataRow row in DtlsRows)
                    {
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "AddTVoucherDetails";
                        for (int col = 0; col < dtDetails.Columns.Count; col++)
                        {
                            //if (dtDetails.Columns[col].ColumnName == "PkVoucherTrnNo")
                            //    cmd.Parameters.AddWithValue("@" + dtDetails.Columns[col].ColumnName + "", 0);
                            //else 
                            // if (dtDetails.Columns[col].ColumnName != "ModifiedBy" && dtDetails.Columns[col].ColumnName != "FkVoucherNo")
                            if (dtDetails.Columns[col].ColumnName != "ModifiedBy")
                                cmd.Parameters.AddWithValue("@" + dtDetails.Columns[col].ColumnName + "", row[col]);

                        }
                        SqlParameter outParameter1 = new SqlParameter();
                        outParameter1.ParameterName = "@ReturnID";
                        outParameter1.Direction = ParameterDirection.Output;
                        outParameter1.DbType = DbType.Int32;
                        cmd.Parameters.Add(outParameter1);
                        commandcollection.Add(cmd);

                        //Voucher PayType Details
                        PayTypeRows = dtPayType.Select("FKSalesVoucherNo=" + Convert.ToInt64(row[0]) + "");
                        foreach (DataRow rowPayType in PayTypeRows)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "AddTVoucherPayTypeDetails";
                            for (int col = 0; col < dtPayType.Columns.Count; col++)
                            {
                                //if (dtPayType.Columns[col].ColumnName == "PKVoucherPayTypeNo")
                                //    cmd.Parameters.AddWithValue("@" + dtPayType.Columns[col].ColumnName + "", 0);
                                //else 
                                //if (dtPayType.Columns[col].ColumnName != "ModifiedBy" && dtPayType.Columns[col].ColumnName != "FKReceiptVoucherNo" && dtPayType.Columns[col].ColumnName != "FKVoucherTrnNo")
                                if (dtPayType.Columns[col].ColumnName != "ModifiedBy")
                                    cmd.Parameters.AddWithValue("@" + dtPayType.Columns[col].ColumnName + "", rowPayType[col]);
                            }
                            commandcollection.Add(cmd);
                        }

                        //Voucher Cheque/Credit Details
                        ChqCrRows = dtChqCr.Select("FkVoucherNo=" + Convert.ToInt64(row[0]) + "");
                        foreach (DataRow rowChqCr in ChqCrRows)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "AddTVoucherChqCreditDetails";
                            for (int col = 0; col < dtChqCr.Columns.Count; col++)
                            {
                                //if (dtRefDetails.Columns[col].ColumnName == "PkSrNo")
                                //    cmd.Parameters.AddWithValue("@" + dtChqCr.Columns[col].ColumnName + "", 0);
                                //else
                                //if (dtChqCr.Columns[col].ColumnName != "ModifiedBy" && dtChqCr.Columns[col].ColumnName != "FKVoucherNo" && dtChqCr.Columns[col].ColumnName != "FkVoucherTrnNo")
                                if (dtChqCr.Columns[col].ColumnName != "ModifiedBy")
                                    cmd.Parameters.AddWithValue("@" + dtChqCr.Columns[col].ColumnName + "", rowChqCr[col]);
                            }
                            commandcollection.Add(cmd);
                        }


                        //VoucherRefDetails Add
                        DetailsRefRows = dtRefDetails.Select("FkVoucherTrnNo=" + Convert.ToInt64(row[0]) + "");
                        foreach (DataRow rowRef in DetailsRefRows)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "AddTVoucherRefDetails";
                            for (int col = 0; col < dtRefDetails.Columns.Count; col++)
                            {
                                //if (dtRefDetails.Columns[col].ColumnName == "PkRefTrnNo")
                                //    cmd.Parameters.AddWithValue("@" + dtRefDetails.Columns[col].ColumnName + "", 0);
                                //else
                                //if (dtRefDetails.Columns[col].ColumnName != "Modifiedby" && dtRefDetails.Columns[col].ColumnName != "FkVoucherTrnNo")
                                if (dtRefDetails.Columns[col].ColumnName != "Modifiedby")
                                    cmd.Parameters.AddWithValue("@" + dtRefDetails.Columns[col].ColumnName + "", rowRef[col]);
                            }
                            commandcollection.Add(cmd);
                        }

                        //Stock Details Add
                        StockRows = dtStock.Select("FkVoucherTrnNo=" + Convert.ToInt64(row[0]) + "");
                        foreach (DataRow rowStock in StockRows)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "AddTStock";
                            for (int col = 0; col < dtStock.Columns.Count; col++)
                            {
                                //if (dtStock.Columns[col].ColumnName == "PkStockTrnNo")
                                //    cmd.Parameters.AddWithValue("@" + dtStock.Columns[col].ColumnName + "", 0);
                                //else 
                                // if (dtStock.Columns[col].ColumnName != "ModifiedBy" && dtStock.Columns[col].ColumnName != "FKVoucherNo" && dtStock.Columns[col].ColumnName != "FkVoucherTrnNo" && dtStock.Columns[col].ColumnName != "IsVoucherLock")
                                if (dtStock.Columns[col].ColumnName != "ModifiedBy" && dtStock.Columns[col].ColumnName != "IsVoucherLock")
                                    cmd.Parameters.AddWithValue("@" + dtStock.Columns[col].ColumnName + "", rowStock[col]);


                            }
                            SqlParameter outParameter2 = new SqlParameter();
                            outParameter2.ParameterName = "@ReturnID";
                            outParameter2.Direction = ParameterDirection.Output;
                            outParameter2.DbType = DbType.Int32;
                            cmd.Parameters.Add(outParameter2);
                            commandcollection.Add(cmd);

                            ////Stock Godown Add
                            //StockGodowns = dtStockGodown.Select("FKStockTrnNo=" + Convert.ToInt64(rowStock[0]) + "");
                            //foreach (DataRow rowStockGodown in StockGodowns)
                            //{
                            //    cmd = new SqlCommand();
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    cmd.CommandText = "AddTStockGodown";
                            //    for (int col = 0; col < dtStockGodown.Columns.Count; col++)
                            //    {
                            //        if (dtStockGodown.Columns[col].ColumnName == "PKStockGodownNo")
                            //            cmd.Parameters.AddWithValue("@" + dtStockGodown.Columns[col].ColumnName + "", 0);
                            //        else if (dtStockGodown.Columns[col].ColumnName != "ModifiedBy" && dtStockGodown.Columns[col].ColumnName != "FKStockTrnNo")
                            //            cmd.Parameters.AddWithValue("@" + dtStockGodown.Columns[col].ColumnName + "", rowStockGodown[col]);
                            //    }
                            //    commandcollection.Add(cmd);
                            //}
                        }
                    }

                    if (DtlsRows.Length == 0)
                    {
                        StockRows = dtStock.Select("FKVoucherNo=" + Convert.ToInt64(dtVoucher.Rows[i].ItemArray[0].ToString()) + "");
                        foreach (DataRow rowStock in StockRows)
                        {
                            cmd = new SqlCommand();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "AddTStock";
                            for (int col = 0; col < dtStock.Columns.Count; col++)
                            {
                                //if (dtStock.Columns[col].ColumnName == "PkStockTrnNo")
                                //    cmd.Parameters.AddWithValue("@" + dtStock.Columns[col].ColumnName + "", 0);
                                //else 
                                //if (dtStock.Columns[col].ColumnName != "ModifiedBy" && dtStock.Columns[col].ColumnName != "FKVoucherNo" && dtStock.Columns[col].ColumnName != "FkVoucherTrnNo" && dtStock.Columns[col].ColumnName != "IsVoucherLock")
                                if (dtStock.Columns[col].ColumnName != "ModifiedBy")
                                    cmd.Parameters.AddWithValue("@" + dtStock.Columns[col].ColumnName + "", rowStock[col]);


                            }
                            SqlParameter outParameter2 = new SqlParameter();
                            outParameter2.ParameterName = "@ReturnID";
                            outParameter2.Direction = ParameterDirection.Output;
                            outParameter2.DbType = DbType.Int32;
                            cmd.Parameters.Add(outParameter2);
                            commandcollection.Add(cmd);

                            ////Stock Godown Add
                            //StockGodowns = dtStockGodown.Select("FKStockTrnNo=" + Convert.ToInt64(rowStock[0]) + "");
                            //foreach (DataRow rowStockGodown in StockGodowns)
                            //{
                            //    cmd = new SqlCommand();
                            //    cmd.CommandType = CommandType.StoredProcedure;
                            //    cmd.CommandText = "AddTStockGodown";
                            //    for (int col = 0; col < dtStockGodown.Columns.Count; col++)
                            //    {
                            //        if (dtStockGodown.Columns[col].ColumnName == "PKStockGodownNo")
                            //            cmd.Parameters.AddWithValue("@" + dtStockGodown.Columns[col].ColumnName + "", 0);
                            //        else if (dtStockGodown.Columns[col].ColumnName != "ModifiedBy" && dtStockGodown.Columns[col].ColumnName != "FKStockTrnNo")
                            //            cmd.Parameters.AddWithValue("@" + dtStockGodown.Columns[col].ColumnName + "", rowStockGodown[col]);
                            //    }
                            //    commandcollection.Add(cmd);
                            //}
                        }
                    }

                    if (ExecuteNonQueryStatementExport(commandcollection) == true)
                    {
                        ObjTrans.Execute("Update TVoucherEntry set VoucherStatus=3 Where PKVoucherNo=" + PKVoucherNo + "", CommonFunctions.ConStr);
                    }
                    dgDetails.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.PeachPuff;
                    PB.Value++;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                    if (flag == false) break;
                }
            }
            else
            {
                OMMessageBox.Show("Record not available..", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }

            if (PB.InvokeRequired)
            {
                PB.Invoke(new MethodInvoker(delegate {
                    lblText.Visible = false;
                    PB.Visible = false;
                    PB.Minimum = 1;
                    PB.Value = 1;
                    Application.DoEvents();
                    btnTransfer.Visible = true;
                    pnlImportExport.Visible = false;
                    BtnExport.Visible = false;
                    plnExport.Visible = false;
                }));
            }
            else
            {
                lblText.Visible = false;
                PB.Visible = false;
                PB.Minimum = 1;
                PB.Value = 1;
                Application.DoEvents();
                btnTransfer.Visible = true;
                pnlImportExport.Visible = false;
                BtnExport.Visible = false;
                plnExport.Visible = false;
            }
        }

        #endregion

        public bool ExecuteNonQueryStatement(CommandCollection commandcollection)
        {
            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStrServer);
            cn.Open();
            SqlTransaction myTrans;
            int cntVchNo = -1, cntRef = 0, cntStock = 0;//, cntStockColor = 0, cntStockBarCode = 0;
            myTrans = cn.BeginTransaction();
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;
                        if (commandcollection[i].CommandText == "AddTVoucherEntry")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTVoucherDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            cntRef = i;
                        }

                        if (commandcollection[i].CommandText == "AddTVoucherRefDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTStock")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            if (cntRef != 0)
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            else
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                            cntStock = i;
                        }
                        if (commandcollection[i].CommandText == "AddTStockGodown")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FKStockTrnNo", commandcollection[cntStock].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i].CommandText == "AddTVoucherPayTypeDetails")
                        {
                            if (cntVchNo != -1)
                            {
                                commandcollection[i].Parameters.AddWithValue("@FKReceiptVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                                if (cntRef != 0)
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            }
                            else
                            {
                                commandcollection[i].Parameters.AddWithValue("@FKReceiptVoucherNo", 0);
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                            }
                        }
                        if (commandcollection[i].CommandText.IndexOf("Update") >= 0)
                        {
                            if (cntRef != 0)
                                if (commandcollection[i].CommandText.IndexOf("@pkSrNo") >= 0)
                                {
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                                }
                                else
                                    commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);

                        }
                        if (commandcollection[i].CommandText == "AddTVoucherChqCreditDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@FkVoucherNo", commandcollection[cntVchNo].Parameters["@ReturnID"].Value);
                            if (cntRef != 0)
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", commandcollection[cntRef].Parameters["@ReturnID"].Value);
                            else
                                commandcollection[i].Parameters.AddWithValue("@FkVoucherTrnNo", 0);
                        }
                        if (commandcollection[i].CommandText == "AddTParkingBill")
                        {
                            cntVchNo = i;
                        }
                        if (commandcollection[i].CommandText == "AddTParkingBillDetails")
                        {
                            commandcollection[i].Parameters.AddWithValue("@ParkingBillNo", commandcollection[0].Parameters["@ReturnID"].Value);
                        }
                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                CommonFunctions.ErrorMessge = e.Message;
                return false;
            }
            finally
            {
                cn.Close();
            }
        }

        public bool ExecuteNonQueryStatementExport(CommandCollection commandcollection)
        {
            SqlConnection cn = null;
            cn = new SqlConnection(CommonFunctions.ConStrServer);
            cn.Open();
            SqlTransaction myTrans;
            //int cntVchNo = -1, cntRef = 0, cntStock = 0, cntStockColor = 0, cntStockBarCode = 0;
            myTrans = cn.BeginTransaction();
            try
            {
                for (int i = 0; (i < this.commandcollection.Count); i = (i + 1))
                {
                    if ((this.commandcollection[i] != null))
                    {
                        commandcollection[i].Connection = cn;
                        commandcollection[i].Transaction = myTrans;

                        if (commandcollection[i] != null)
                            commandcollection[i].ExecuteNonQuery();
                    }
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception e)
            {
                myTrans.Rollback();
                CommonFunctions.ErrorMessge = e.Message;
                return false;
            }
            finally
            {
                cn.Close();
            }
        }


        public void AddTableInDatatable(DataTable dt, string[] str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = str[i].ToString();
                dt.Rows.Add(dr);
            }
        }

        #region Import Related Methods

        public void Import()
        {
            PB.Visible = true;
            PB.Minimum = 1; PB.Maximum = dtTable.Rows.Count + 1;
            PB.Step = 1;
            PB.Visible = true;

            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                lblText.Visible = true;
                lblText.Text = "Table Is Importing....";
                strQuery = "";
                strDest = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo." + dtTableTemp.Rows[i].ItemArray[1].ToString();
                //strSource = ObjFunction.GetDatabaseName(CommonFunctions.ConStrServer) + ".dbo." + dtTable.Rows[i].ItemArray[1].ToString();
                strSource = ObjFunction.GetDatabaseName(CommonFunctions.ConStrServer) + ".dbo." + dtTable.Rows[i].ItemArray[1].ToString();
                strQuery = "Delete From " + strDest;
                strQuery = strQuery + " set Identity_Insert " + strDest + " ON  ";

                strQuery = strQuery + "insert " + strDest + " (" + GetColumns(dtTable.Rows[i].ItemArray[1].ToString()) + " )";

                strQuery = strQuery + " Select * from " + strSource;

                strQuery = strQuery + " set Identity_Insert " + strDest + " OFF ";
                if (ObjTrans.Execute(strQuery, CommonFunctions.ConStrServer))
                {

                    PB.Value++;
                    dgDetails.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.PeachPuff;
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(1000);
                }
            }
            lblText.Visible = false;
            PB.Visible = false;
            PB.Minimum = 1;
            PB.Value = 1;
            PB.Visible = false;
            System.Threading.Thread.Sleep(1000);
            pnlImportExport.Visible = false;

            Application.DoEvents();
        }

        public string GetColumns(string tableName)
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

        #endregion

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
           
            if (rbImport.Checked == true)
            {
                btnTransfer.Visible = true;
                plnExport.Visible = false;
                pnlImportExport.Visible = true;
                DeleteData(dgDetails);
                dgDetails.DataSource = dtTable.DefaultView;
                dgDetails.Columns[0].Width = 60;
                dgDetails.Columns[1].Width = 388;
                Import();
                BtnExport.Visible = false;
            }
            else if (rbExport.Checked == true)
            {
                plnExport.Visible = true;
                dtpFromDate.Focus();
                btnTransfer.Visible = false;
            }
        }

        public void DeleteData(DataGridView dg)
        {
            while (dg.Rows.Count > 0)
            {
                dg.Rows.RemoveAt(0);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SrNo");
            dt.Columns.Add("Date");
            dt.Columns.Add("Voucher No");
            dt.Columns.Add("Amount");

            DataRow dr;
            dtVoucher = ObjFunction.GetDataView("Select * from TVoucherEntry  Where VoucherDate>='" + dtpFromDate.Text + "' AND TVoucherEntry.VoucherDate<='" + dtpToDate.Text + "' AND VoucherStatus in (1,2) AND VoucherTypeCode in (0," + VchType.Sales + "," + VchType.Purchase + "," + VchType.SalesReceipt + "," + VchType.PurchasePayment + "," + VchType.RejectionIn + "," + VchType.RejectionOut + ") order by PKVoucherNo").Table;

            for (int i = 0; i < dtVoucher.Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr[0] = i + 1;
                dr[1] = Convert.ToDateTime(dtVoucher.Rows[i].ItemArray[3].ToString()).ToString("dd-MMM-yyyy");
                dr[2] = dtVoucher.Rows[i].ItemArray[1].ToString();
                dr[3] = dtVoucher.Rows[i].ItemArray[10].ToString();
                dt.Rows.Add(dr);
            }
            dgDetails.DataSource = dt.DefaultView;
            dgDetails.Columns[0].Width = 68;
            dgDetails.Columns[1].Width = 129;
            dgDetails.Columns[2].Width = 108;
            dgDetails.Columns[3].Width = 159;
            pnlImportExport.Visible = true;
            BtnExport.Visible = true;

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            flag = true;
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Export));
            th.Start();
            //Export();
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {

            if (rbExport.Checked == true)
            {
                plnExport.Visible = true;
                DateTime dtVch = ObjQry.ReturnDate("Select Min(VoucherDate) From TVoucherEntry Where VoucherStatus in (1,2)", CommonFunctions.ConStr);
                if (dtVch.ToString("dd-MMM-yyyy") != "11-Nov-1111")
                    dtpFromDate.Value = ObjQry.ReturnDate("Select Min(VoucherDate) From TVoucherEntry Where VoucherStatus in (1,2)", CommonFunctions.ConStr);
                else
                    dtpFromDate.Value = DateTime.Now;
                dtpFromDate.Focus();
                btnTransfer.Visible = false;
            }
            else
            {
                btnTransfer.Visible = true;
                pnlImportExport.Visible = false;
                plnExport.Visible = false;
                BtnExport.Visible = false;
            }
        }

        private void btnStopTransfer_Click(object sender, EventArgs e)
        {
            flag = false;
        }

        public void TransAll()
        {
            string strDest = "";
            string[] TableName = {  "MArea", "MBank", "MBranch", "MCity", "MCompany", "MCountry", "MDutiesTaxesInfo", "MGodown",
                                     "MItemTaxInfo", "MItemTaxSetting",
                                     "MLanguage", "MLanguageDictionary", "MLedger", "MLedgerDetails", "MLedgerDistDetails", "MLocation", 
                                     "MOccupation", "MOtherBank", "MPayType", "MPayTypeDetails",
                                     "MPayTypeLedger", "MQualification", "MRateSetting", "MRegion", "MRegistration", 
                                     "MRegistrationDetails", "MServerSettings", "MSettings", "MSettingsType",  "MState", "MStockBarcode", 
                                     "MStockCategory", "MStockDepartment", "MStockGroup", "MStockItems", "MStockLocation", 
                                     "MTransporter", "MUOM", "MUser",
                                     "MUserMenuMaster", "TParkingBill", "TParkingBillDetails"};

            string str = "Data Source=Server\\SQLEXPRESS;Initial Catalog=RetailerServer0001;User ID=Logicall;Password=Logicall";
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add();
            for(int i=0;i<TableName.Length;i++)
            {
                dr=dt.NewRow();
                dr[0]=TableName[i];
                dt.Rows.Add(dr);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
                strSource = ObjFunction.GetDatabaseName(CommonFunctions.ConStr) + ".dbo." + dt.Rows[i].ItemArray[0].ToString();
                strDest = "RetailerServer0001.dbo." + dt.Rows[i].ItemArray[0].ToString();


                strQuery = strQuery + "insert " + strDest + " (" + GetColumns(dt.Rows[i].ItemArray[0].ToString()) + " )";

                strQuery = strQuery + " Select * from " + strSource + " Where StatusNo in(1,2) ";

                strQuery = strQuery + " update  " + strSource + " set statusno=3";

                if (ObjTrans.Execute(strQuery, str))
                {
                    strQuery = "";
                }
                else
                {
                    MessageBox.Show("Error");
                }
               
            }
        }

    }
}
