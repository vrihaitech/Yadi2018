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
    public partial class PackingListDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTPackingListDetails dbTPackingListDetails = new DBTPackingListDetails();
        TPackingListDetails tPackDetails = new TPackingListDetails();

        DataTable dtDelete = new DataTable();
        public long PkVoucherNo;
        public string CompName;
        public string[] ReportSession;

        public PackingListDetails()
        {
            InitializeComponent();
        }

        public PackingListDetails(long pkVoucherNo, string CompanyName)
        {
            InitializeComponent();
            PkVoucherNo = pkVoucherNo;
            CompName = CompanyName;
            btnTPrint.Visible = false;
        }

        public PackingListDetails(long pkVoucherNo, string CompanyName, string[] reportsession)
        {
            InitializeComponent();
            PkVoucherNo = pkVoucherNo;
            CompName = CompanyName;
            ReportSession = reportsession;
            btnTPrint.Visible = true;

        }
        private void PackingListDetails_Load(object sender, EventArgs e)
        {
            InitDelTable();
            BindGrid();
            dgPackDetails.Focus();

            lstItemName.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            lstItemName.RowHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            lstItemName.RowTemplate.DefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            lstItemName.RowTemplate.Height = 24;
            lstItemName.ColumnHeadersHeight = 24;
            new GridSearch(lstItemName, 0);
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == false)
            {
                plnPrinting.Visible = false;
                rbEnglish.Checked = true;
               
            }
            else
            {
                plnPrinting.Visible = true;
                if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_DefaultBillPrint)) == 1)
                    rbEnglish.Checked = true;
                else
                    rbMarathi.Checked = true;

                if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 2)
                    rbMarathi.Text = "marazI";
                else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 3)
                    rbMarathi.Text = "ihMdI";
            }
            rbEnglish.Enabled = true;
            rbMarathi.Enabled = true;
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }

        public void FillList(DataGridView lst, DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lst.Rows.Add();
                for (int j = 0; j < lst.Columns.Count; j++)
                {
                    lst.Rows[i].Cells[j].Value = dt.Rows[i][j];
                }
            }
        }

        public void BindGrid()
        {
            try
            {

                while (dgPacking.Rows.Count > 0)
                    dgPacking.Rows.RemoveAt(0);
                while (dgPackDetails.Rows.Count > 0)
                    dgPackDetails.Rows.RemoveAt(0);
                string sql = " SELECT 0 AS SrNo, MItemGroup.StockGroupName + ' ' + mItemMaster.ItemName AS ItemName, TStock.Quantity,Muom.UomName, '' AS PackedQty, '' AS BalQty,MStockItems.ItemNo ,TStock.PkStockTrnno" +
                             " FROM MStockItems INNER JOIN TStock ON mItemMaster.ItemNo = TStock.ItemNo  INNER JOIN MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo  " +
                             " Inner Join MUom on Muom.UomNo=TStock.FkUomNo " +
                             " WHERE (TStock.FkVoucherNo =" + PkVoucherNo + ") " +
                             " Order by TStock.PkStockTrnNo";
                DataTable dt = ObjFunction.GetDataView(sql).Table;

                DataTable dtItemList = new DataTable();
                dtItemList.Columns.Add();
                dtItemList.Columns.Add();
                dtItemList.Columns.Add();
                dtItemList.Columns.Add();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgPacking.Rows.Add();
                    dtItemList.Rows.Add();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        dgPacking.Rows[i].Cells[j].Value = dt.Rows[i][j];
                        dtItemList.Rows[i][1] = dt.Rows[i].ItemArray[colpIndex.Itemno].ToString();
                        dtItemList.Rows[i][0] = dt.Rows[i].ItemArray[colpIndex.Itemname].ToString();
                        dtItemList.Rows[i][2] = dt.Rows[i].ItemArray[colpIndex.Pkstocktrnno].ToString();
                        dtItemList.Rows[i][3] = dt.Rows[i].ItemArray[colpIndex.MUom].ToString();
                    }
                }

                if (dgPacking.Rows.Count > 0)
                {


                    FillList(lstItemName, dtItemList);

                    DataTable dtDetails = ObjFunction.GetDataView(" SELECT 0 AS SrNo, MItemGroup.StockGroupName + ' ' + mItemMaster.ItemName AS ItemName, MUOM.UOMName,TPackingListDetails.Quantity ,TPackingListDetails.BagNo, mItemMaster.ItemNo,TPackingListDetails.PackingListNo,IsNull(TPackingListDetails.GroupNo,0) AS GroupNo,IsNull(TPackingListDetails.FkStockTrnNo,0) as FkStockTrnNo " +
                                        " FROM MStockItems INNER JOIN MStockGroup ON mItemMaster.GroupNo = MItemGroup.StockGroupNo INNER JOIN TPackingListDetails ON mItemMaster.ItemNo = TPackingListDetails.ItemNo " +
                                        " INNER JOIN TStock ON TPackingListDetails.FkStockTrnNo = TStock.PkStockTrnNo INNER JOIN MUOM ON TStock.FkUomNo = MUOM.UOMNo " +
                                        " WHERE ( TPackingListDetails.FkVoucherNo=" + PkVoucherNo + ")  " +
                                        " Order By TPackingListDetails.PackingListNo").Table;

                    for (int i = 0; i < dtDetails.Rows.Count; i++)
                    {
                        dgPackDetails.Rows.Add();
                        for (int j = 0; j < dtDetails.Columns.Count; j++)
                        {
                            dgPackDetails.Rows[i].Cells[j].Value = dtDetails.Rows[i][j];
                        }
                    }

                    dgPackDetails.Rows.Add();
                    MovetoNext move2n = new MovetoNext(m2n);
                    BeginInvoke(move2n, new object[] { dgPackDetails.Rows.Count - 1, colpdIndex.Itemname1, dgPackDetails });
                    dgPackDetails.Focus();
                    CalculateQty();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public static class colpIndex
        {
            public static int srno = 0;
            public static int Itemname = 1;
            public static int Qty = 2;
            public static int MUom = 3;
            public static int PackedQty = 4;
            public static int BalQty = 5;
            public static int Itemno = 6;
            public static int Pkstocktrnno = 7;
        }

        public static class colpdIndex
        {
            public static int Srno1 = 0;
            public static int Itemname1 = 1;
            public static int UOM1 = 2;
            public static int Qty1 = 3;
            public static int Bagno = 4;
            public static int Itemno1 = 5;
            public static int PackingListNo = 6;
            public static int GroupNo = 7;
            public static int FkStockTrnNo = 8;
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            try
            {
                dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPacking_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #region Delete code
        private void InitDelTable()
        {
            dtDelete.Columns.Add();
            dtDelete.Columns.Add();
        }

        private void DeleteDtls(int Type, long PkSrNo)
        {
            DataRow dr = null;
            dr = dtDelete.NewRow();
            dr[0] = Type;
            dr[1] = PkSrNo;
            dtDelete.Rows.Add(dr);
        }

        private void DeleteValues()
        {
            if (dtDelete != null)
            {
                for (int i = 0; i < dtDelete.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
                    {
                        tPackDetails.PackingListNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbTPackingListDetails.DeleteTPackingListDetails(tPackDetails);
                    }
                    if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 2)
                    {
                        tPackDetails.GroupNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        tPackDetails.FkVoucherNo = PkVoucherNo;
                        dbTPackingListDetails.DeleteTPackingListDetailsGroup(tPackDetails);
                    }
                }
                dtDelete.Rows.Clear();
            }
        }


        #endregion

        public void DeleteGroup(long GroupNo)
        {
            for (int i = 0; i < dgPackDetails.Rows.Count; i++)
            {
                if (GroupNo == Convert.ToInt64((dgPackDetails[colpdIndex.GroupNo, i].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgPackDetails[colpdIndex.GroupNo, i].EditedFormattedValue.ToString()))
                {
                    dgPackDetails.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        public void FillItemList()
        {
            while (lstItemName.Rows.Count > 0)
                lstItemName.Rows.RemoveAt(0);

            DataTable dt = new DataTable();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            int Cnt = 0;
            for (int i = 0; i < dgPacking.Rows.Count; i++)
            {

                if (Convert.ToDouble(dgPacking.Rows[i].Cells[colpIndex.BalQty].Value.ToString()) != 0)
                {
                    dt.Rows.Add();
                    dt.Rows[Cnt][1] = dgPacking.Rows[i].Cells[colpIndex.Itemno].Value;
                    dt.Rows[Cnt][0] = dgPacking.Rows[i].Cells[colpIndex.Itemname].Value;
                    dt.Rows[Cnt][2] = dgPacking.Rows[i].Cells[colpIndex.Pkstocktrnno].Value;
                    dt.Rows[Cnt][3] = dgPacking.Rows[i].Cells[colpIndex.MUom].Value;
                    Cnt++;
                }
            }

            FillList(lstItemName, dt);
        }

        private void delete_row()
        {
            try
            {
                if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dgPackDetails.Rows[dgPackDetails.CurrentCell.RowIndex].Cells[colpdIndex.PackingListNo].EditedFormattedValue.ToString().Trim() != "")
                    {
                        long PackingListNo = Convert.ToInt64(dgPackDetails.Rows[dgPackDetails.CurrentCell.RowIndex].Cells[colpdIndex.PackingListNo].Value);
                        if (PackingListNo != 0)
                        {
                            long GroupNo = Convert.ToInt64((dgPackDetails[colpdIndex.GroupNo, dgPackDetails.CurrentRow.Index].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgPackDetails[colpdIndex.GroupNo, dgPackDetails.CurrentRow.Index].EditedFormattedValue.ToString());
                            if (GroupNo == 0)
                            {
                                DeleteDtls(1, PackingListNo);
                                dgPackDetails.Rows.RemoveAt(dgPackDetails.CurrentCell.RowIndex);
                            }
                            else
                            {
                                DeleteDtls(2, GroupNo);
                                DeleteGroup(GroupNo);
                            }
                            dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, dgPackDetails.Rows.Count - 1];
                        }
                    }
                    if (dgPackDetails.Rows.Count - 1 == dgPackDetails.CurrentCell.RowIndex)
                    {
                        long GroupNo = Convert.ToInt64((dgPackDetails[colpdIndex.GroupNo, dgPackDetails.CurrentRow.Index].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgPackDetails[colpdIndex.GroupNo, dgPackDetails.CurrentRow.Index].EditedFormattedValue.ToString());
                        if (GroupNo == 0)
                            dgPackDetails.Rows.RemoveAt(dgPackDetails.CurrentCell.RowIndex);
                        else DeleteGroup(GroupNo);

                        dgPackDetails.Rows.Add();
                    }
                    else
                    {
                        long GroupNo = Convert.ToInt64((dgPackDetails[colpdIndex.GroupNo, dgPackDetails.CurrentRow.Index].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgPackDetails[colpdIndex.GroupNo, dgPackDetails.CurrentRow.Index].EditedFormattedValue.ToString());
                        if (GroupNo == 0)
                            dgPackDetails.Rows.RemoveAt(dgPackDetails.CurrentCell.RowIndex);
                        else DeleteGroup(GroupNo);
                    }
                    dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, dgPackDetails.Rows.Count - 1];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPackDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                delete_row();
                CalculateQty();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int RowIndex = dgPackDetails.CurrentRow.Index;


                if (dgPackDetails.CurrentCell.ColumnIndex == colpIndex.Itemname)
                {
                    if (dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Itemname1].EditedFormattedValue.ToString() == "")
                    {
                        FillItemList();
                        for (int i = 0; i < lstItemName.Rows.Count; i++)
                        {
                            lstItemName.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 130);
                        }
                        lstItemName.Visible = true;
                        lstItemName.Focus();
                    }
                    else
                    {
                        dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Qty1, RowIndex];
                        dgPackDetails.Focus();
                    }
                }
                else if (dgPackDetails.CurrentCell.ColumnIndex == colpdIndex.Qty1)
                {

                    if (dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1].EditedFormattedValue.ToString().Trim() != "")
                    {
                        if (dgPackDetails.CurrentRow.Cells[colpdIndex.Qty1].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgPackDetails[colpdIndex.Qty1, RowIndex].ErrorText = "Enter Qty";
                            dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Qty1, RowIndex];
                            dgPackDetails.Focus();
                        }
                        else
                        {
                            double totQty = GetTotalQty(Convert.ToInt64(dgPackDetails[colpdIndex.Itemno1, RowIndex].Value), Convert.ToInt64(dgPackDetails[colpdIndex.FkStockTrnNo, RowIndex].Value));
                            double BagQty = GetItemBagQty(Convert.ToInt64(dgPackDetails[colpdIndex.Itemno1, RowIndex].Value), Convert.ToInt64(dgPackDetails[colpdIndex.FkStockTrnNo, RowIndex].Value));
                            if (totQty >= BagQty)
                            {
                                dgPackDetails[colpdIndex.Qty1, RowIndex].ErrorText = "";
                                dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Bagno, RowIndex];
                                dgPackDetails.Focus();
                                CalculateQty();
                            }
                            else
                            {
                                dgPackDetails[colpdIndex.Qty1, RowIndex].ErrorText = "Enter Valid Qty";
                                dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Qty1, RowIndex];
                                dgPackDetails.Focus();
                            }
                        }

                    }
                    else
                    {
                        dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, RowIndex];
                        dgPackDetails.Focus();
                    }
                }
                else if (dgPackDetails.CurrentCell.ColumnIndex == colpdIndex.Bagno)
                {
                    dgPackDetails[colpdIndex.Bagno, RowIndex].ErrorText = "";
                    if (dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1].EditedFormattedValue.ToString().Trim() == "" || dgPackDetails.CurrentRow.Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, RowIndex];
                        dgPackDetails.Focus();
                    }
                    else if (dgPackDetails.CurrentRow.Cells[colpdIndex.Qty1].EditedFormattedValue.ToString().Trim() != "")
                    {
                        if (dgPackDetails.CurrentRow.Cells[colpdIndex.Bagno].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgPackDetails[colpdIndex.Bagno, RowIndex].ErrorText = "Enter Bag No";
                            dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Bagno, RowIndex];
                            dgPackDetails.Focus();
                        }
                        else
                        {
                            if (dgPackDetails.CurrentCell.RowIndex < dgPackDetails.Rows.Count - 1)
                            {
                                dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, RowIndex + 1];
                                dgPackDetails.Focus();
                            }
                            else
                            {
                                if (dgPackDetails.Rows.Count == dgPackDetails.CurrentRow.Index + 1)
                                {
                                    dgPackDetails.Rows.Add();
                                    dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, RowIndex + 1];
                                    dgPackDetails.Focus();
                                }
                                else
                                {
                                    dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, RowIndex];
                                    dgPackDetails.Focus();
                                }
                            }
                        }
                    }
                    else
                    {
                        dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Qty1, RowIndex];
                        dgPackDetails.Focus();
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnSave.Focus();
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1].EditedFormattedValue.ToString() != "")
                {
                    if (ObjFunction.CheckNumeric(dgPackDetails.CurrentRow.Cells[colpdIndex.Bagno].EditedFormattedValue.ToString()))
                    {
                        SuplitFuction(dgPackDetails.CurrentRow.Index);
                    }
                    else
                    {
                        DisplayMessage("Enter Numeric Bag No");
                        dgPackDetails.CurrentCell = dgPackDetails.CurrentRow.Cells[colpdIndex.Bagno];
                        dgPackDetails.Focus();
                    }
                }
                else
                {
                    if (dgPackDetails.Rows.Count - 1 == dgPackDetails.CurrentRow.Index)
                    {
                        int RowIndex = dgPackDetails.CurrentRow.Index - 1;
                        if (RowIndex != -1)
                        {
                            if (dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString() != "")
                            {
                                if (ObjFunction.CheckNumeric(dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Bagno].EditedFormattedValue.ToString()))
                                {
                                    SuplitFuction(RowIndex);
                                }
                                else
                                {
                                    DisplayMessage("Enter Numeric Bag No");
                                    dgPackDetails.CurrentCell = dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1];
                                    dgPackDetails.Focus();
                                }
                            }

                        }
                    }
                }

            }
            else if (e.KeyCode == Keys.F4)
            {
                if (dgPackDetails.RowCount == 1)
                {
                    for (int i = 0; i < dgPacking.Rows.Count; i++)
                    {
                        int RowIndexs = dgPackDetails.RowCount - 1;
                        dgPackDetails[colpdIndex.Bagno, RowIndexs].Value = 1;
                        dgPackDetails[colpdIndex.Itemno1, RowIndexs].Value = dgPacking.Rows[i].Cells[colpIndex.Itemno].Value.ToString();
                        dgPackDetails[colpdIndex.GroupNo, RowIndexs].Value = 0;
                        dgPackDetails[colpdIndex.FkStockTrnNo, RowIndexs].Value = dgPacking.Rows[i].Cells[colpIndex.Pkstocktrnno].Value.ToString();
                        dgPackDetails[colpdIndex.Qty1, RowIndexs].Value = dgPacking.Rows[i].Cells[colpIndex.Qty].Value.ToString();
                        dgPackDetails[colpdIndex.Itemname1, RowIndexs].Value = dgPacking.Rows[i].Cells[colpIndex.Itemname].Value.ToString();
                        dgPackDetails[colpdIndex.PackingListNo, RowIndexs].Value = 0;
                        dgPackDetails.Rows.Add();
                    }
                    CalculateQty();
                }

            }
        }

        public void SuplitFuction(int RowIndex)
        {
            string GroupNo = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.GroupNo].EditedFormattedValue.ToString();
            if (GroupNo == "" || GroupNo.Trim() == "0")
            {
                double Bagno = Convert.ToDouble(dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Bagno].Value);
                double Qty = Convert.ToDouble(dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Qty1].Value);
                double BalQty = GetItemBalQty(Convert.ToInt64(dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Itemno1].Value), Convert.ToInt64(dgPackDetails.Rows[RowIndex].Cells[colpdIndex.FkStockTrnNo].Value));
                int noCount = (int)(BalQty / Qty);
                double netBalQty = BalQty - (Qty * noCount);

                if (netBalQty!=0 && netBalQty > (Qty - 2)) noCount++;

                if (noCount != 0)
                {
                    dgPackDetails.Rows[RowIndex].Cells[colpdIndex.GroupNo].Value = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Bagno].Value;
                    int i = 0;
                    while (noCount != i)
                    {
                        int RowIndexs = dgPackDetails.Rows.Count - 1;
                        dgPackDetails[colpdIndex.Bagno, RowIndexs].Value = Bagno + i + 1;
                        dgPackDetails[colpdIndex.Itemno1, RowIndexs].Value = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Itemno1].Value.ToString();
                        dgPackDetails[colpdIndex.GroupNo, RowIndexs].Value = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.GroupNo].Value;
                        dgPackDetails[colpdIndex.FkStockTrnNo, RowIndexs].Value = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.FkStockTrnNo].Value;
                        dgPackDetails[colpdIndex.UOM1, RowIndexs].Value = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.UOM1].Value;

                        if (noCount == i + 1) //last row
                        {
                            if (netBalQty <= 2)
                            {
                                dgPackDetails[colpdIndex.Qty1, RowIndexs].Value = Qty + netBalQty;
                            }
                            else if (netBalQty > (Qty - 2))
                            {
                                dgPackDetails[colpdIndex.Qty1, RowIndexs].Value = netBalQty;
                            }
                            else
                            {
                                dgPackDetails[colpdIndex.Qty1, RowIndexs].Value = Qty;
                            }
                        }
                        else
                        {
                            dgPackDetails[colpdIndex.Qty1, RowIndexs].Value = Qty;
                        }

                        dgPackDetails[colpdIndex.Itemname1, RowIndexs].Value = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.Itemname1].Value.ToString();
                        dgPackDetails[colpdIndex.PackingListNo, RowIndexs].Value = dgPackDetails.Rows[RowIndex].Cells[colpdIndex.PackingListNo].EditedFormattedValue.ToString();
                        i++;
                        dgPackDetails.Rows.Add();
                    }
                }
                CalculateQty();
                dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Itemname1, dgPackDetails.Rows.Count - 1];
                dgPackDetails.Focus();

            }
        }

        private void lstItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lstItemName.CurrentCell != null)
                {
                    e.SuppressKeyPress = true;
                    double totQty = GetItemBalQty(Convert.ToInt64(lstItemName[1, lstItemName.CurrentRow.Index].Value.ToString()), Convert.ToInt64(lstItemName[2, lstItemName.CurrentRow.Index].Value.ToString()));
                    if (totQty > 0)
                    {
                        dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1].Value = lstItemName[0, lstItemName.CurrentRow.Index].Value.ToString();
                        dgPackDetails.CurrentRow.Cells[colpdIndex.Itemno1].Value = lstItemName[1, lstItemName.CurrentRow.Index].Value.ToString();
                        dgPackDetails.CurrentRow.Cells[colpdIndex.FkStockTrnNo].Value = lstItemName[2, lstItemName.CurrentRow.Index].Value.ToString();
                        dgPackDetails.CurrentRow.Cells[colpdIndex.UOM1].Value = lstItemName[3, lstItemName.CurrentRow.Index].Value.ToString();
                        dgPackDetails.CurrentRow.Cells[colpdIndex.Qty1].Value = totQty.ToString("0.00");
                        SelectRow(Convert.ToInt64(dgPackDetails.CurrentRow.Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString()), Convert.ToInt64(lstItemName[2, lstItemName.CurrentRow.Index].Value.ToString()));
                        lstItemName.Visible = false;
                        dgPackDetails.CurrentCell = dgPackDetails[colpdIndex.Qty1, dgPackDetails.CurrentRow.Index];
                        dgPackDetails.Focus();

                    }
                    else
                    {
                        DisplayMessage("Enter Qty is equal");
                        lstItemName.Focus();
                    }
                }
            }
            if (e.KeyCode == Keys.Space)
            {
                e.SuppressKeyPress = true;
                lstItemName.Visible = false;
                dgPackDetails.Focus();
            }
        }

        private void dgPackDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1);
            }

        }

        private void lstItemName_VisibleChanged(object sender, EventArgs e)
        {
            if (lstItemName.Visible == true)
            {
                dgPackDetails.Enabled = false;
            }
            else
                dgPackDetails.Enabled = true;
        }

        private void dgPackDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txt1 = (TextBox)e.Control;
            txt1.TextChanged += new EventHandler(txtQuantity_TextChanged);
        }

        public void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgPackDetails.CurrentCell.ColumnIndex == colpdIndex.Qty1)
                {
                    ObjFunction.SetMasked((TextBox)sender, 2, 6, OMFunctions.MaskedType.NotNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgPackDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = dgPackDetails.CurrentRow.Index;
            MovetoNext move2n = new MovetoNext(m2n);
            if (dgPackDetails.CurrentCell.ColumnIndex == colpdIndex.Qty1)
            {

                if (dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1].EditedFormattedValue.ToString().Trim() == "" || dgPackDetails.CurrentRow.Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString().Trim() == "")
                {
                    BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Itemname1, dgPackDetails });
                    dgPackDetails.Focus();
                }
                else if (dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1].EditedFormattedValue.ToString().Trim() != "")
                {
                    if (dgPackDetails.CurrentRow.Cells[colpdIndex.Qty1].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgPackDetails[colpdIndex.Qty1, RowIndex].ErrorText = "Enter Qty";
                        BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Qty1, dgPackDetails });
                        dgPackDetails.Focus();
                    }
                    else
                    {
                        double totQty = GetTotalQty(Convert.ToInt64(dgPackDetails[colpdIndex.Itemno1, RowIndex].Value), Convert.ToInt64(dgPackDetails[colpdIndex.FkStockTrnNo, RowIndex].Value));
                        double BagQty = GetItemBagQty(Convert.ToInt64(dgPackDetails[colpdIndex.Itemno1, RowIndex].Value), Convert.ToInt64(dgPackDetails[colpdIndex.FkStockTrnNo, RowIndex].Value));
                        if (totQty >= BagQty)
                        {
                            dgPackDetails[colpdIndex.Qty1, RowIndex].ErrorText = "";
                            BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Bagno, dgPackDetails });
                            dgPackDetails.Focus();

                        }
                        else
                        {
                            dgPackDetails[colpdIndex.Qty1, RowIndex].ErrorText = "Enter Valid Qty";
                            BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Qty1, dgPackDetails });
                            dgPackDetails.Focus();
                        }
                    }
                    CalculateQty();
                }
                else
                {
                    BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Itemname1, dgPackDetails });
                    dgPackDetails.Focus();
                }

            }
            else if (dgPackDetails.CurrentCell.ColumnIndex == colpdIndex.Bagno)
            {
                if (dgPackDetails[colpdIndex.Qty1, RowIndex].ErrorText == "")
                {
                    dgPackDetails[colpdIndex.Bagno, RowIndex].ErrorText = "";
                    if (dgPackDetails.CurrentRow.Cells[colpdIndex.Itemname1].EditedFormattedValue.ToString().Trim() == "" || dgPackDetails.CurrentRow.Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString().Trim() == "")
                    {
                        BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Itemname1, dgPackDetails });
                        dgPackDetails.Focus();
                    }
                    else if (dgPackDetails.CurrentRow.Cells[colpdIndex.Qty1].EditedFormattedValue.ToString().Trim() != "")
                    {
                        if (dgPackDetails.CurrentRow.Cells[colpdIndex.Bagno].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgPackDetails[colpdIndex.Bagno, RowIndex].ErrorText = "Enter Bag No";
                            BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Bagno, dgPackDetails });
                            dgPackDetails.Focus();
                        }
                        else
                        {
                            //if (dgPackDetails.Rows[e.RowIndex].Cells[colpdIndex.GroupNo].EditedFormattedValue.ToString() == "")
                            //    dgPackDetails.Rows[e.RowIndex].Cells[colpdIndex.GroupNo].Value = 0;
                            if (dgPackDetails.CurrentCell.RowIndex < dgPackDetails.Rows.Count - 1)
                            {
                                BeginInvoke(move2n, new object[] { RowIndex + 1, colpdIndex.Itemname1, dgPackDetails });
                                dgPackDetails.Focus();
                            }
                            else
                            {
                                if (dgPackDetails.Rows.Count == dgPackDetails.CurrentRow.Index + 1)
                                {
                                    dgPackDetails.Rows.Add();
                                    BeginInvoke(move2n, new object[] { RowIndex + 1, colpdIndex.Itemname1, dgPackDetails });
                                    dgPackDetails.Focus();
                                }
                                else
                                {
                                    BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Itemname1, dgPackDetails });
                                    dgPackDetails.Focus();
                                }
                            }
                        }
                    }
                    else
                    {
                        BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Qty1, dgPackDetails });
                        dgPackDetails.Focus();
                    }
                }
                else
                {
                    BeginInvoke(move2n, new object[] { RowIndex, colpdIndex.Qty1, dgPackDetails });
                    dgPackDetails.Focus();
                }
            }
        }

        public double GetItemBalQty(long TItemNo, long FkStockNo)
        {
            double BalQty = 0;
            for (int i = 0; i < dgPacking.Rows.Count; i++)
            {
                if (Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Itemno].Value.ToString()) == TItemNo && Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Pkstocktrnno].Value.ToString()) == FkStockNo)
                {
                    BalQty =Math.Round(Convert.ToDouble(dgPacking.Rows[i].Cells[colpIndex.Qty].FormattedValue.ToString()) - Convert.ToDouble(((dgPacking.Rows[i].Cells[colpIndex.PackedQty].FormattedValue.ToString() == "") ? "0" : dgPacking.Rows[i].Cells[colpIndex.PackedQty].FormattedValue.ToString())),2);
                }
            }
            return BalQty;
        }

        public double GetTotalQty(long TItemNo, long FkStock)
        {
            double BalQty = 0;
            for (int i = 0; i < dgPacking.Rows.Count; i++)
            {
                if (Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Itemno].Value.ToString()) == TItemNo && Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Pkstocktrnno].Value.ToString()) == FkStock)
                {
                    BalQty = Convert.ToDouble(dgPacking.Rows[i].Cells[colpIndex.Qty].Value);
                }
            }
            return BalQty;
        }

        public double GetItemBagQty(long TItemNo, long FkStock)
        {
            double BalQty = 0;
            for (int i = 0; i < dgPackDetails.Rows.Count; i++)
            {
                if (dgPackDetails.Rows[i].Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString() != "")
                {
                    if (Convert.ToInt64(dgPackDetails.Rows[i].Cells[colpdIndex.Itemno1].Value.ToString()) == TItemNo && Convert.ToInt64(dgPackDetails.Rows[i].Cells[colpdIndex.FkStockTrnNo].Value.ToString()) == FkStock)
                    {
                        BalQty = BalQty + Convert.ToDouble(((dgPackDetails.Rows[i].Cells[colpdIndex.Qty1].FormattedValue.ToString() == "") ? "0" : dgPackDetails.Rows[i].Cells[colpdIndex.Qty1].FormattedValue.ToString()));
                    }
                }
            }
            return BalQty;
        }

        public void CalculateQty()
        {
            for (int i = 0; i < dgPacking.RowCount; i++)
            {
                dgPacking.Rows[i].Cells[colpIndex.PackedQty].Value = GetItemBagQty(Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Itemno].Value), Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Pkstocktrnno].Value)).ToString("0.00");
                dgPacking.Rows[i].Cells[colpIndex.BalQty].Value = Convert.ToDouble(Convert.ToDouble(dgPacking.Rows[i].Cells[colpIndex.Qty].Value) - Convert.ToDouble(dgPacking.Rows[i].Cells[colpIndex.PackedQty].Value)).ToString("0.00");

            }
            for (int i = 0; i < dgPackDetails.RowCount; i++)
            {
                if (dgPackDetails.Rows[i].Cells[colpdIndex.GroupNo].EditedFormattedValue.ToString() != "" && dgPackDetails.Rows[i].Cells[colpdIndex.GroupNo].EditedFormattedValue.ToString().Trim() != "0")
                {
                    dgPackDetails.Rows[i].Cells[colpdIndex.Qty1].ReadOnly = true;
                    dgPackDetails.Rows[i].Cells[colpdIndex.Bagno].ReadOnly = true;
                }
                else
                {
                    dgPackDetails.Rows[i].Cells[colpdIndex.Qty1].ReadOnly = false;
                    dgPackDetails.Rows[i].Cells[colpdIndex.Bagno].ReadOnly = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                DeleteValues();
                dbTPackingListDetails = new DBTPackingListDetails();
                int cnt = 0;
                double NoOfbags = 0;
                string strBagNos = "{@}";
                for (int i = 0; i < dgPackDetails.Rows.Count; i++)
                {
                    if (dgPackDetails.Rows[i].Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString().Trim() != "")
                    {
                        cnt = 1;
                        tPackDetails = new TPackingListDetails();
                        tPackDetails.PackingListNo = Convert.ToInt64((dgPackDetails.Rows[i].Cells[colpdIndex.PackingListNo].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgPackDetails.Rows[i].Cells[colpdIndex.PackingListNo].Value);
                        tPackDetails.BagNo = dgPackDetails[colpdIndex.Bagno, i].EditedFormattedValue.ToString();
                        tPackDetails.ItemNo = Convert.ToInt64(dgPackDetails[colpdIndex.Itemno1, i].EditedFormattedValue.ToString());
                        tPackDetails.FkVoucherNo = PkVoucherNo;
                        tPackDetails.Quantity = Convert.ToDouble(dgPackDetails[colpdIndex.Qty1, i].EditedFormattedValue.ToString());
                        tPackDetails.GroupNo = Convert.ToInt64((dgPackDetails[colpdIndex.GroupNo, i].EditedFormattedValue.ToString().Trim() == "") ? "0" : dgPackDetails[colpdIndex.GroupNo, i].EditedFormattedValue.ToString());
                        tPackDetails.FkStockTrnNo = Convert.ToInt64(dgPackDetails[colpdIndex.FkStockTrnNo, i].EditedFormattedValue.ToString());
                        tPackDetails.CompanyNo = DBGetVal.FirmNo;
                        tPackDetails.UserID = DBGetVal.UserID;
                        tPackDetails.UserDate = DBGetVal.ServerTime.Date;
                        dbTPackingListDetails.AddTPackingListDetails(tPackDetails);

                        if (strBagNos.IndexOf("{@}" + tPackDetails.BagNo + "{@}", StringComparison.CurrentCultureIgnoreCase) == -1)
                        {
                            NoOfbags++;
                            strBagNos += tPackDetails.BagNo + "{@}";
                        }
                    }
                }

                if (cnt == 1)
                {
                    if (dbTPackingListDetails.ExecuteNonQueryStatements())
                    {
                        dbTPackingListDetails.SaveTransNoOfItems(PkVoucherNo, NoOfbags);

                        OMMessageBox.Show("Packing List Details Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindGrid();
                    }
                    else
                    {
                        OMMessageBox.Show("Packing List Details  not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
        }

        public bool Validation()
        {
            bool flag = true;
            for (int i = 0; i < dgPackDetails.Rows.Count; i++)
            {
                if (dgPackDetails.Rows[i].Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString().Trim() != "")
                {
                    if (dgPackDetails.Rows[i].Cells[colpdIndex.Qty1].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgPackDetails.Rows[i].Cells[colpdIndex.Qty1].ErrorText = "Enter Qty";
                        return false;
                    }
                    else if (dgPackDetails.Rows[i].Cells[colpdIndex.Qty1].ErrorText != "")
                        return false;
                    else if (dgPackDetails.Rows[i].Cells[colpdIndex.Bagno].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgPackDetails.Rows[i].Cells[colpdIndex.Bagno].ErrorText = "Enter Bag No";
                        return false;
                    }
                    else if (dgPackDetails.Rows[i].Cells[colpdIndex.Bagno].ErrorText != "")
                        return false;
                }
            }


            if (flag == true)
            {
                bool BFlag = false;
                for (int i = 0; i < dgPacking.Rows.Count; i++)
                {
                    if (Convert.ToDouble(dgPacking.Rows[i].Cells[colpIndex.BalQty].EditedFormattedValue.ToString()) != 0)
                    {
                        BFlag = true;
                        dgPacking.Rows[i].Cells[colpIndex.BalQty].Style.BackColor = Color.LightBlue;
                    }
                    else dgPacking.Rows[i].Cells[colpIndex.BalQty].Style.BackColor = Color.White;
                }
                if (BFlag)
                {
                    if (OMMessageBox.Show("Packing list quantity remaining are you sure want to Continue ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        flag = false;
                    }
                }
            }

            return flag;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgPackDetails.Rows.Count > 0)
            {

                string[] ReportSession;
                Form NewF = null;

                ReportSession = new string[2];
                ReportSession[0] = PkVoucherNo.ToString();
                ReportSession[1] = CompName;
                if (rbEnglish.Checked == true)
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetPackingListDetails(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetPackingListDetails.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                else
                {
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.GetPackingListDetailsMar(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetPackingListDetailsMar.rpt", CommonFunctions.ReportPath), ReportSession);
                }
                ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            }
        }

        public void SelectRow(long ItemNo, long FkStock)
        {
            for (int i = 0; i < dgPacking.Rows.Count; i++)
            {
                if (ItemNo == Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Itemno].EditedFormattedValue.ToString()) && FkStock == Convert.ToInt64(dgPacking.Rows[i].Cells[colpIndex.Pkstocktrnno].EditedFormattedValue.ToString()))
                {
                    dgPacking.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    dgPacking.CurrentCell = dgPacking[colpIndex.BalQty, i];
                    dgPacking.Focus();
                }
                else
                    dgPacking.Rows[i].DefaultCellStyle.BackColor = Color.White;
            }

        }

        private void dgPackDetails_CurrentCellChanged(object sender, EventArgs e)
        {
            //dgPacking.Enabled = false;
            //int RowIndex = dgPackDetails.CurrentRow.Index;
            //if (dgPackDetails.CurrentRow.Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString() != "")
            //    SelectRow(Convert.ToInt64(dgPackDetails.CurrentRow.Cells[colpdIndex.Itemno1].EditedFormattedValue.ToString()));
            //else
            //    SelectRow(0);
            //dgPackDetails.Focus();
        }

        public void PrintBillTrans(int PrintType)
        {
            try
            {
                ReportSession[9] = (rbEnglish.Checked == true) ? "1" : "2";
                if (PrintType == 0)
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    childForm = null;
                    
                    if (rbEnglish.Checked == true)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            childForm = ObjFunction.GetReportObject("Reports.GetBillFirmPackList");
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        {
                            childForm = ObjFunction.GetReportObject("Reports.GetBillFirmPackListMar");
                        }
                    }
                    if (childForm != null)
                    {
                        DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                        objRpt.OwnPrinterName = CompanyName;
                        if (objRpt.PrintReport() == true)
                        {
                            DisplayMessage("Bill Print Successfully!!!");
                        }
                        else
                        {
                            DisplayMessage("Bill not Print !!!");
                        }
                    }
                    else
                    {
                        DisplayMessage("Bill Report not exist !!!");
                    }
                }
                else
                {
                    Form NewF = null;
                    if (rbEnglish.Checked == true)
                    {
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillFirmPackList.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    else
                    {
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("GetBillFirmPackListMar.rpt", CommonFunctions.ReportPath), ReportSession);
                    }
                    if (NewF != null)
                        ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnTPrint_Click(object sender, EventArgs e)
        {
            if (ObjQry.ReturnLong("Select Count(*) From TPackingListDetails Where FkVoucherNo=" + PkVoucherNo + " ", CommonFunctions.ConStr) != 0)
            {
                if (ObjQry.ReturnLong("Select Count(*) from TStock Where ItemNo Not In (Select ItemNo From tPackingListDetails Where FkVoucherNo=" + PkVoucherNo + ") And FkVoucherNo=" + PkVoucherNo + " ", CommonFunctions.ConStr) == 0)
                {
                    DialogResult ds = OMMessageBox.Show("Want to print this Packing List Bill?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OwnButton, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button1, "Preview");
                    if (ds == DialogResult.Yes)
                    {
                        if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsPrintCount)))
                        {
                            PrintBillTrans(0);
                        }
                        else PrintBillTrans(0);
                    }
                    else if (ds == DialogResult.Cancel)
                    {
                        PrintBillTrans(1);
                    }
                }
                else
                    DisplayMessage("Packing Report Not Print...Please Enter All Items In Packing List");
            }
            else
            {
                DisplayMessage("Please Enter Paking List Details ...");
            }
        }

        private void lstItemName_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstItemName.CurrentCell != null)
                {
                    lstItemName.BackgroundColor = Color.FromArgb(255, 192, 130);
                    for (int i = 0; i < lstItemName.Rows.Count; i++)
                    {
                        lstItemName.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 130);
                    }
                    lstItemName.Rows[lstItemName.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 192, 130);
                    lstItemName.CurrentCell.Style.SelectionBackColor = Color.LightBlue;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

    }
}
