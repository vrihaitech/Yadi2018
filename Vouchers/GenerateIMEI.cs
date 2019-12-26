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

namespace Yadi.Vouchers
{
    public partial class GenerateIMEI : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTGenerateIMEI dbTGenerateIMEI = new DBTGenerateIMEI();
        TGenerateIMEI tGenerateIMEI = new TGenerateIMEI();
        DataTable dtSearch = new DataTable();
        long ID;
        DataTable dt = new DataTable();
        public long VoucherType;
        long Itemno, pkstockttrnno;
        string Str = "";
        int SelectedCount = 0;

        public GenerateIMEI()
        {
            InitializeComponent();
        }

        public GenerateIMEI(long ID, long VoucherType)
        {
            InitializeComponent();
            this.ID = ID;
            this.VoucherType = VoucherType;
        }

        private void GenerateIMEI_Load(object sender, EventArgs e)
        {
            try
            {
                if (VoucherType == 9)
                {
                    btnSales.Visible = false;
                }
                else
                {
                    BtnSave.Visible = false;
                    btnDelete.Visible = false;
                }
                if (ID != 0)
                {
                    FillGrid();
                }
                foreach (DataGridViewColumn col in GvItem.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                foreach (DataGridViewColumn col in dgGenerateIMEI.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                BtnSave.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillGrid()
        {
            try
            {
                string SqlQuery = "";

                SqlQuery = " SELECT  0 As SrNo,MItemGroup.ItemGroupName +' '+ MItemMaster.ItemName As ItemName,TStock.Quantity As Qty,TStock.ItemNo,TStock.PkStockTrnNo,TVoucherEntry.PkVoucherNo, " +
                           " 'False' As SelectItem FROM TVoucherEntry INNER JOIN TStock ON TVoucherEntry.PkVoucherNo = TStock.FKVoucherNo INNER JOIN " +
                           " MItemGroup INNER JOIN MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo ON TStock.ItemNo = MItemMaster.ItemNo " +
                           " Where TVoucherEntry.PkVoucherNo = " + ID + " And VoucherTypeCode = " + VoucherType + "";

                GvItem.Rows.Clear();
                dt = ObjFunction.GetDataView(SqlQuery).Table;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GvItem.Rows.Add();
                    for (int j = 0; j < GvItem.Columns.Count; j++)
                    {
                        GvItem.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (VoucherType == 9)
            {
                Form NewF = new Yadi.Vouchers.PurchaseAE(ID);
                this.Close();
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
            else if (VoucherType == 15)
            {
                Form NewF = new Yadi.Vouchers.SalesBarcodeAE(ID);
                this.Close();
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                long GenerateIMEIID = 0;
                for (int i = 0; i < dgGenerateIMEI.Rows.Count - 1; i++)
                {
                    if (dgGenerateIMEI[ColIndex1.IMEINo, i].Value.ToString() != "")
                    {
                        dbTGenerateIMEI = new DBTGenerateIMEI();
                        tGenerateIMEI = new TGenerateIMEI();
                        GenerateIMEIID = ((dgGenerateIMEI[ColIndex1.PkGenerateIMEIID, i].EditedFormattedValue.ToString() == "") ? 0 : Convert.ToInt64(dgGenerateIMEI[ColIndex1.PkGenerateIMEIID, i].EditedFormattedValue));
                        tGenerateIMEI.PkGenerateIMEIID = GenerateIMEIID;
                        tGenerateIMEI.IMEINo = (dgGenerateIMEI[ColIndex1.IMEINo, i].Value.ToString());
                        tGenerateIMEI.FkVoucherNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.FkVoucherNo, i].Value.ToString());
                        tGenerateIMEI.FkStockTrnNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.FkStockTrnNo, i].Value.ToString());
                        tGenerateIMEI.ItemNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.ItemNo, i].Value.ToString());
                        tGenerateIMEI.IsSales = Convert.ToBoolean(dgGenerateIMEI[ColIndex1.IsSales, i].Value.ToString());
                        tGenerateIMEI.SalesStockTrnNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.SalesStockTrnNo, i].Value.ToString());
                        tGenerateIMEI.SalesFkVoucherNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.SalesFkVoucherNo, i].Value.ToString());
                        tGenerateIMEI.IsActive = true;
                        tGenerateIMEI.UserID = DBGetVal.UserID;
                        tGenerateIMEI.UserDate = DBGetVal.ServerTime.Date;
                        tGenerateIMEI.CompanyNo = DBGetVal.FirmNo;

                        dbTGenerateIMEI.AddTGenerateIMEI(tGenerateIMEI);
                    }
                }

                if (GenerateIMEIID == 0)
                {
                    OMMessageBox.Show("Record Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindIMEIGrid();
                    BtnSave.Focus();
                }
                else
                {
                    OMMessageBox.Show("Record Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindIMEIGrid();
                    BtnSave.Focus();
                }

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
            if (e.KeyCode == Keys.F2)
            {
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }

        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BtnSave.Focus();
        }

        private void GvItem_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex.SrNo)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
        }

        private void dgGenerateIMEI_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == ColIndex1.SrNo)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            if (VoucherType == 15)
            {
                dgGenerateIMEI.Rows[e.RowIndex].Cells[ColIndex1.IMEINo].ReadOnly = true;
            }
        }

        #region ColumnIndex

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int Qty = 2;
            public static int ItemNo = 3;
            public static int PkStockTrnNo = 4;
            public static int PkVoucherNo = 5;
            public static int SelectItem = 6;

        }

        public static class ColIndex1
        {
            public static int SrNo = 0;
            public static int IMEINo = 1;
            public static int FkVoucherNo = 2;
            public static int ItemNo = 3;
            public static int FkStockTrnNo = 4;
            public static int IsSales = 5;
            public static int SalesStockTrnNo = 6;
            public static int SalesFkVoucherNo = 7;
            public static int PkGenerateIMEIID = 8;
            public static int SelectIMEI = 9;

        }

        #endregion

        private void BindIMEIGrid()
        {
            try
            {
                string SqlQuery = "";
                if (VoucherType == 9)
                {
                    SqlQuery = " SELECT  0 AS SrNo, TGenerateIMEI.IMEINo, TGenerateIMEI.fkVoucherNo ,TGenerateIMEI.Itemno,FkStockTrnNo, " +
                               " TGenerateIMEI.IsSales, SalesStockTrnNo, SalesFkVoucherNo,PkGenerateIMEIID,'False' As SelectIMEI FROM TGenerateIMEI INNER JOIN MItemGroup INNER JOIN " +
                               " MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo  ON TGenerateIMEI.ItemNo = MItemMaster.ItemNo " +
                               " Where TGenerateIMEI.ItemNo =" + Itemno + "  And TGenerateIMEI.fkVoucherNo =" + ID + " And TGenerateIMEI.IsActive='True'";
                }
                else
                {
                    //  long TEMPID = ObjQry.ReturnLong("SELECT TGenerateIMEI.FkVoucherNo FROM TGenerateIMEI WHERE TGenerateIMEI.SalesFkVoucherNo="+ID +" ",CommonFunctions.ConStr);
                    SqlQuery = " SELECT  0 AS SrNo, TGenerateIMEI.IMEINo, TGenerateIMEI.fkVoucherNo ,TGenerateIMEI.Itemno,FkStockTrnNo, " +
                            " TGenerateIMEI.IsSales, SalesStockTrnNo, SalesFkVoucherNo,PkGenerateIMEIID,'False' As SelectIMEI FROM TGenerateIMEI INNER JOIN MItemGroup INNER JOIN " +
                            " MItemMaster ON MItemGroup.ItemGroupNo = MItemMaster.GroupNo  ON TGenerateIMEI.ItemNo = MItemMaster.ItemNo " +
                            " Where TGenerateIMEI.ItemNo =" + Itemno + " And TGenerateIMEI.IsActive='True' ORDER by  TGenerateIMEI.IsSales ";
                }
                dgGenerateIMEI.Rows.Clear();
                dt = ObjFunction.GetDataView(SqlQuery).Table;
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgGenerateIMEI.Rows.Add();
                        for (int j = 0; j < dgGenerateIMEI.Columns.Count; j++)
                        {
                            dgGenerateIMEI.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                        }
                        if (Convert.ToBoolean(dt.Rows[i].ItemArray[5].ToString()) == true)
                        {
                            dgGenerateIMEI.Rows[i].Cells[ColIndex1.SelectIMEI].Value = true;
                        }
                    }
                    dgGenerateIMEI.Rows.Add();
                }
                else
                {
                    dgGenerateIMEI.Rows.Add();
                    dgGenerateIMEI.CurrentRow.Cells[ColIndex1.ItemNo].Value = Itemno;
                    dgGenerateIMEI.CurrentRow.Cells[ColIndex1.FkVoucherNo].Value = ID;
                    dgGenerateIMEI.CurrentRow.Cells[ColIndex1.FkStockTrnNo].Value = pkstockttrnno;
                    dgGenerateIMEI.CurrentRow.Cells[ColIndex1.IsSales].Value = false;
                    dgGenerateIMEI.CurrentRow.Cells[ColIndex1.SalesFkVoucherNo].Value = 0;
                    dgGenerateIMEI.CurrentRow.Cells[ColIndex1.SalesStockTrnNo].Value = 0;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        protected void FillBlankRowToGrid()
        {
            //if (dgGenerateIMEI.Rows.Count > 0)
            //{
            //    dgGenerateIMEI.Rows.Clear();
            //    double Quantity = Convert.ToDouble(GvItem.Rows[GvItem.CurrentRow.Index].Cells[ColIndex.Qty].Value);
            //    for (int i = 1; i < Quantity; i++)
            //    {
            //        dgGenerateIMEI.Rows.Add(Quantity);
            //    }
            //}
        }

        private void dgGenerateIMEI_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (dgGenerateIMEI.CurrentCell.ColumnIndex == ColIndex1.IMEINo)
                    {
                        if ((dgGenerateIMEI.CurrentRow.Cells[ColIndex1.IMEINo].Value != null))//&&
                            if ((dgGenerateIMEI.Rows.Count) <= Convert.ToDouble(GvItem.CurrentRow.Cells[ColIndex.Qty].Value))
                                dgGenerateIMEI.Rows.Add();
                        dgGenerateIMEI.CurrentRow.Cells[ColIndex1.ItemNo].Value = Itemno;
                        dgGenerateIMEI.CurrentRow.Cells[ColIndex1.FkVoucherNo].Value = ID;
                        dgGenerateIMEI.CurrentRow.Cells[ColIndex1.FkStockTrnNo].Value = pkstockttrnno;
                        dgGenerateIMEI.CurrentRow.Cells[ColIndex1.IsSales].Value = false;
                        dgGenerateIMEI.CurrentRow.Cells[ColIndex1.SalesFkVoucherNo].Value = 0;
                        dgGenerateIMEI.CurrentRow.Cells[ColIndex1.SalesStockTrnNo].Value = 0;
                    }
                }
                if (e.KeyCode == Keys.Delete)
                {
                    long GenerateIMEINo = Convert.ToInt64(dgGenerateIMEI.CurrentRow.Cells[ColIndex1.PkGenerateIMEIID].Value); ;
                    dbTGenerateIMEI = new DBTGenerateIMEI();
                    tGenerateIMEI = new TGenerateIMEI();
                    tGenerateIMEI.PkGenerateIMEIID = GenerateIMEINo;

                    if (OMMessageBox.Show("Are you sure you want to Delete this Entries ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        if (GenerateIMEINo == 0)
                        {
                            if (dgGenerateIMEI.Rows.Count - 1 == dgGenerateIMEI.CurrentCell.RowIndex)
                            {
                                dgGenerateIMEI.Rows.RemoveAt(dgGenerateIMEI.CurrentCell.RowIndex);
                                dgGenerateIMEI.Rows.Add();
                            }
                            else
                                dgGenerateIMEI.Rows.RemoveAt(dgGenerateIMEI.CurrentCell.RowIndex);
                        }
                        else
                        {
                            if (dbTGenerateIMEI.DeleteTGenerateIMEI(tGenerateIMEI) == true)
                            {
                                OMMessageBox.Show("Record Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                BindIMEIGrid();
                            }
                            else
                            {
                                OMMessageBox.Show("Record Not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GvItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColIndex.SelectItem)
                {

                    if (Convert.ToBoolean(GvItem.Rows[e.RowIndex].Cells[ColIndex.SelectItem].Value) == false)
                    {
                        for (int i = 0; i <= GvItem.Rows.Count - 1; i++)
                        {
                            GvItem.Rows[i].Cells[ColIndex.SelectItem].Value = false;

                        }
                        Itemno = Convert.ToInt32(GvItem.Rows[GvItem.CurrentRow.Index].Cells[ColIndex.ItemNo].Value);
                        pkstockttrnno = Convert.ToInt32(GvItem.Rows[GvItem.CurrentRow.Index].Cells[ColIndex.PkStockTrnNo].Value);
                        dgGenerateIMEI.Rows.Clear();
                        BindIMEIGrid();
                        dgGenerateIMEI.Focus();
                        dgGenerateIMEI.CurrentCell = dgGenerateIMEI[1, 0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetStrIMEIID() != "")
                {
                    string str = "";
                    str = GetStrIMEIID();

                    if (OMMessageBox.Show("Are you sure you want to Sales this Record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dgGenerateIMEI.Rows.Count - 1; i++)
                        {
                            if (Convert.ToBoolean(dgGenerateIMEI[ColIndex1.SelectIMEI, i].Value.ToString()) == true)
                            {
                                dbTGenerateIMEI = new DBTGenerateIMEI();
                                tGenerateIMEI = new TGenerateIMEI();
                                // GenerateIMEIID =
                                tGenerateIMEI.PkGenerateIMEIID = ((dgGenerateIMEI[ColIndex1.PkGenerateIMEIID, i].EditedFormattedValue.ToString() == "") ? 0 : Convert.ToInt64(dgGenerateIMEI[ColIndex1.PkGenerateIMEIID, i].EditedFormattedValue)); ;
                                tGenerateIMEI.IMEINo = (dgGenerateIMEI[ColIndex1.IMEINo, i].Value.ToString());
                                tGenerateIMEI.FkVoucherNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.FkVoucherNo, i].Value.ToString());
                                tGenerateIMEI.FkStockTrnNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.FkStockTrnNo, i].Value.ToString());
                                tGenerateIMEI.ItemNo = Convert.ToInt64(dgGenerateIMEI[ColIndex1.ItemNo, i].Value.ToString());
                                tGenerateIMEI.IsSales = true;
                                tGenerateIMEI.SalesStockTrnNo = Convert.ToInt64(GvItem[ColIndex.PkStockTrnNo, GvItem.CurrentCell.RowIndex].Value.ToString());
                                tGenerateIMEI.SalesFkVoucherNo = Convert.ToInt64(GvItem[ColIndex.PkVoucherNo, GvItem.CurrentCell.RowIndex].Value.ToString());
                                tGenerateIMEI.IsActive = true;
                                tGenerateIMEI.UserID = DBGetVal.UserID;
                                tGenerateIMEI.UserDate = DBGetVal.ServerTime.Date;
                                tGenerateIMEI.CompanyNo = DBGetVal.FirmNo;

                                dbTGenerateIMEI.AddTGenerateIMEI(tGenerateIMEI);
                            }
                        }
                        OMMessageBox.Show("Record Save Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindIMEIGrid();
                    }
                }
                else
                {
                    OMMessageBox.Show("Select Atleast One Record....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                BtnSave.Focus();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetStrIMEIID() != "")
                {
                    string str = "";
                    str = GetStrIMEIID();

                    if (OMMessageBox.Show("Are you sure you want to Release this Record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        ObjTrans.ExecuteQuery("Update TGenerateIMEI Set IsSales ='False', SalesStockTrnNo = 0,SalesFkVoucherNo = 0  Where PkGenerateIMEIID IN (" + str + ")", CommonFunctions.ConStr);
                        OMMessageBox.Show("Record Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindIMEIGrid();
                    }
                }
                else
                {
                    OMMessageBox.Show("Select Atleast One Record....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                BtnSave.Focus();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public string GetStrIMEIID()
        {
            Str = "";
            SelectedCount = 0;
            for (int i = 0; i < dgGenerateIMEI.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(dgGenerateIMEI[ColIndex1.SelectIMEI, i].Value) == true)
                {
                    SelectedCount++;

                    if (Str == "")
                        Str += dgGenerateIMEI.Rows[i].Cells[ColIndex1.PkGenerateIMEIID].Value.ToString();
                    else
                        Str += "," + dgGenerateIMEI.Rows[i].Cells[ColIndex1.PkGenerateIMEIID].Value.ToString();
                }
            }
            return Str;
        }

        private void dgGenerateIMEI_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (VoucherType == 9)
                if ((dgGenerateIMEI.Rows.Count) > Convert.ToDouble(GvItem.CurrentRow.Cells[ColIndex.Qty].Value))//&&(dgGenerateIMEI.CurrentCell.RowIndex[ColIndex1.])
                {
                    dgGenerateIMEI.CurrentRow.Cells[ColIndex1.IMEINo].Value = "";
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (GetStrIMEIID() != "")
                {
                    string str = "";
                    str = GetStrIMEIID();
                    for (int i = 0; i < dgGenerateIMEI.Rows.Count - 1; i++)
                    {
                        if (Convert.ToBoolean(dgGenerateIMEI[ColIndex1.SelectIMEI, i].Value.ToString()) == true && Convert.ToBoolean(dgGenerateIMEI[ColIndex1.IsSales, i].Value.ToString()) == false)
                        {
                            if (OMMessageBox.Show("Are you sure you want to Delete this Entries ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {

                                ObjTrans.ExecuteQuery("Update TGenerateIMEI set IsActive='False' Where PkGenerateIMEIID IN (" + str + ")", CommonFunctions.ConStr);
                                OMMessageBox.Show("Record Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                BindIMEIGrid();
                            }
                        }
                    }
                }
                else
                {
                    OMMessageBox.Show("Select Atleast One Record....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
                BtnSave.Focus();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

    }
}
