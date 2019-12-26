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
    public partial class StockItemIngredientDetails : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMStockItemsIngredient dbMStockItemsIngredient = new DBMStockItemsIngredient();
        MStockItemsIngredient mStockItemsIngredient = new MStockItemsIngredient();
        MStockItemsNutrition mstockitemsnutrition = new MStockItemsNutrition();
        MStockItemsReceipe mStockItemsReceipe = new MStockItemsReceipe();


        DataTable dtDelete = new DataTable();
        public long ItemNo;
        public long ID;
        public Boolean FlagBilingual;
        Color clrColorRow = Color.FromArgb(255, 224, 192);

        public StockItemIngredientDetails()
        {
            InitializeComponent();
        }

        public StockItemIngredientDetails(long ItemNo)
        {
            InitializeComponent();
            this.ItemNo = ItemNo;
        }

        private void StockItemIngredientDetails_Load(object sender, EventArgs e)
        {
           
            lblItemName.Text = ObjQry.ReturnString("Select ItemName From MStockItems_V(NULL," + ItemNo + ",NULL,NULL,NULL,NULL,NULL)", CommonFunctions.ConStr);
            ID = ObjQry.ReturnLong("Select IsNull(PkSrNo,0) From MStockItemsIngredient Where ItemNo=" + ItemNo + "", CommonFunctions.ConStr);
            dgReceipe.RowTemplate.Height = 24;
            dgReceipe.ColumnHeadersHeight = 24;
            dgReceipe.RowTemplate.DefaultCellStyle.Font = null;
            dgReceipe.Columns[ColReceipe.LangReceipeDesc].DefaultCellStyle.Font = new Font("Shivaji01", 16, FontStyle.Bold);
            dgListNutrition.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BindListNutrition();
            if (ID != 0)
            {
                FillControls();
            }
            else
            {
                dgNutrition.Rows.Add();
                dgReceipe.Rows.Add();
                dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.Rows.Count - 1];
                dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.Rows.Count - 1];
            }

            InitDelTable();
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
                FlagBilingual = true;
            txtLangHead1.Font = ObjFunction.GetLangFont();
            txtLangHead2.Font = ObjFunction.GetLangFont();
            txtLangHead3.Font = ObjFunction.GetLangFont();
            lblItemName.Font = new Font("Verdana", 11, FontStyle.Bold);
            new GridSearch(dgListNutrition, 0);
          

        }

        public void FillControls()
        {
            mStockItemsIngredient = dbMStockItemsIngredient.ModifyMStockItemsIngredientByID(ID);

            txtIngredient.Text = mStockItemsIngredient.IngredientValue;
            txtHead.Text = mStockItemsIngredient.NutritionHeadValue;
            txtHead1.Text = mStockItemsIngredient.ReceipeHead1;
            txtHead2.Text = mStockItemsIngredient.ReceipeHead2;
            txtHead3.Text = mStockItemsIngredient.ReceipeHead3;
            txtLangHead1.Text = mStockItemsIngredient.LangReceipeHead1;
            txtLangHead2.Text = mStockItemsIngredient.LangReceipeHead2;
            txtLangHead3.Text = mStockItemsIngredient.LangReceipeHead3;

            BindNutrition();
            BindReceipe();
            if (txtIngredient.Text.Trim() != "")
            {
                txtIngredient.GotFocus += delegate { txtIngredient.Select(txtIngredient.Text.Length, txtIngredient.Text.Length); }; 
            }
        }

        public void BindNutrition()
        {
            while (dgNutrition.RowCount > 0)
                dgNutrition.Rows.RemoveAt(0);

            string Str = " SELECT MStockItemsNutrition.SequenceNo, MNutrition.NutritionName, MStockItemsNutrition.NutritionValue, MStockItemsNutrition.NutritionUOM,MStockItemsNutrition.NutritionNo,IsNull(MStockItemsNutrition.PkSrNo,0) as PkSrNo,0 AS Status " +
                       " FROM MStockItemsNutrition INNER JOIN MNutrition ON MStockItemsNutrition.NutritionNo = MNutrition.NutritionNo " +
                       " WHERE (MStockItemsNutrition.IngredientNo = " + ID + ") ORDER BY MStockItemsNutrition.SequenceNo ";
            DataTable dt = ObjFunction.GetDataView(Str).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgNutrition.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dgNutrition.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }

            dgNutrition.Rows.Add();
            dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.Rows.Count - 1];

        }

        public void BindReceipe()
        {
            while (dgReceipe.RowCount > 0)
                dgReceipe.Rows.RemoveAt(0);

            string Str = " SELECT SequenceNo, ReceipeDesc, LangReceipeDesc,PkSrNo,0 AS Status " +
                         " FROM MStockItemsReceipe " +
                         " WHERE (IngredientNo = " + ID + ") ORDER BY SequenceNo ";

            DataTable dt = ObjFunction.GetDataView(Str).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgReceipe.Rows.Add();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dgReceipe.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
            dgReceipe.Rows.Add();
            dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.Rows.Count - 1];
        }

        public void BindListNutrition()
        {
            while (dgListNutrition.RowCount > 0)
                dgListNutrition.Rows.RemoveAt(0);

            string Str = "SELECT NutritionName, NutritionUOM, NutritionNo FROM MNutrition WHERE (IsAactive = 'True') Order By NutritionName";

            DataTable dt = ObjFunction.GetDataView(Str).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgListNutrition.Rows.Add();
                for (int j = 0; j < dgListNutrition.ColumnCount; j++)
                {
                    dgListNutrition.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static class ColNutrition
        {
            public static int SrNo = 0;
            public static int NutritionName = 1;
            public static int NutritionValue = 2;
            public static int NutritionUOM = 3;
            public static int NutritionNo = 4;
            public static int PkSrNo = 5;
            public static int NStatus = 6;
        }

        public static class ColReceipe
        {
            public static int SrNo = 0;
            public static int ReceipeDesc = 1;
            public static int LangReceipeDesc = 2;
            public static int PkSrNo = 3;
            public static int Status = 4;
        }

        private void dgNutrition_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ColNutrition.SrNo == e.ColumnIndex)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void dgNutrition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() != "")
                    delete_row();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.SrNo)
                {
                    if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() == "")
                        dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.CurrentRow.Index];
                    else
                        dgNutrition.CurrentCell = dgNutrition[dgNutrition.Rows.Count - 1, ColNutrition.NutritionName];
                    dgNutrition.Focus();
                }
                else if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.NutritionName)
                {
                    pnlNutrition.Location = new Point(dgNutrition.Location.X + 10, dgNutrition.Location.Y + 10);

                    pnlNutrition.Visible = true;
                    dgListNutrition.Focus();
                }
                else if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.NutritionValue)
                {
                    if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() == "")
                        dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.CurrentRow.Index];
                    else
                    {
                        if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionValue].EditedFormattedValue.ToString().Trim() == "")
                            dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionValue, dgNutrition.CurrentRow.Index];
                        else
                            dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionUOM, dgNutrition.CurrentRow.Index];
                    }
                }
                else if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.NutritionUOM)
                {
                    if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() == "")
                        dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.CurrentRow.Index];
                    else
                    {
                        if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionUOM].EditedFormattedValue.ToString().Trim() == "")
                            dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionUOM, dgNutrition.Rows.Count - 1];
                        else
                        {
                            if (dgNutrition.Rows.Count == dgNutrition.CurrentRow.Index + 1)
                            {
                                dgNutrition.Rows.Add();
                            }
                            dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.Rows.Count - 1];
                        }
                    }
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtHead1.Focus();
            }
        }

        private void dgNutrition_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MovetoNext move2n = new MovetoNext(m2n);
            if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.SrNo)
            {
                if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() == "")
                    BeginInvoke(move2n, new object[] { dgNutrition.CurrentRow.Index, ColNutrition.NutritionName, dgNutrition });
                else
                    BeginInvoke(move2n, new object[] { dgNutrition.Rows.Count - 1, ColNutrition.NutritionName, dgNutrition });
                dgNutrition.Focus();
            }
            else if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.NutritionName)
            {
                pnlNutrition.Location = new Point(dgNutrition.Location.X - 5, dgNutrition.Location.Y - 5);
                pnlNutrition.Visible = true;
                dgListNutrition.Focus();
            }
            else if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.NutritionValue)
            {
                if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() == "")
                    BeginInvoke(move2n, new object[] { dgNutrition.CurrentRow.Index, ColNutrition.NutritionName, dgNutrition });
                else
                {
                    dgNutrition.CurrentRow.Cells[ColNutrition.NutritionValue].ErrorText = "";
                    if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionValue].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgNutrition.CurrentRow.Cells[ColNutrition.NutritionValue].ErrorText = "Please Enter Vlaue";
                        BeginInvoke(move2n, new object[] { dgNutrition.CurrentRow.Index, ColNutrition.NutritionValue, dgNutrition });

                    }
                    else
                    {
                        if (dgNutrition.CurrentRow.Cells[ColNutrition.PkSrNo].EditedFormattedValue.ToString().Trim() != "" && Convert.ToInt64(dgNutrition.CurrentRow.Cells[ColNutrition.PkSrNo].EditedFormattedValue.ToString()) != 0)
                            dgNutrition.CurrentRow.Cells[ColNutrition.NStatus].Value = "1";
                        BeginInvoke(move2n, new object[] { dgNutrition.CurrentRow.Index, ColNutrition.NutritionUOM, dgNutrition });
                    }
                }
            }
            else if (dgNutrition.CurrentCell.ColumnIndex == ColNutrition.NutritionUOM)
            {
                if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() == "")
                    BeginInvoke(move2n, new object[] { dgNutrition.CurrentRow.Index, ColNutrition.NutritionName, dgNutrition });
                else
                {
                    dgNutrition.CurrentRow.Cells[ColNutrition.NutritionUOM].ErrorText = "";
                    if (dgNutrition.CurrentRow.Cells[ColNutrition.NutritionUOM].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgNutrition.CurrentRow.Cells[ColNutrition.NutritionUOM].ErrorText = "Please Enter Vlaue";
                        BeginInvoke(move2n, new object[] { dgNutrition.Rows.Count - 1, ColNutrition.NutritionUOM, dgNutrition });
                    }
                    else
                    {
                        if (dgNutrition.CurrentRow.Cells[ColNutrition.PkSrNo].EditedFormattedValue.ToString().Trim() != "" && Convert.ToInt64(dgNutrition.CurrentRow.Cells[ColNutrition.PkSrNo].EditedFormattedValue.ToString()) != 0)
                            dgNutrition.CurrentRow.Cells[ColNutrition.NStatus].Value = "1";

                        if (dgNutrition.Rows.Count == dgNutrition.CurrentRow.Index + 1)
                        {
                            dgNutrition.Rows.Add();
                        }
                        BeginInvoke(move2n, new object[] { dgNutrition.Rows.Count - 1, ColNutrition.NutritionName, dgNutrition });
                    }
                }
            }
        }

        private void delete_row()
        {
            try
            {
                if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dgNutrition.Rows[dgNutrition.CurrentCell.RowIndex].Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() != "")
                    {
                        long NurPkSrNo = Convert.ToInt64(dgNutrition.Rows[dgNutrition.CurrentCell.RowIndex].Cells[ColNutrition.PkSrNo].Value);
                        if (NurPkSrNo != 0)
                        {
                            DeleteDtls(1, NurPkSrNo);
                            dgNutrition.Rows.RemoveAt(dgNutrition.CurrentCell.RowIndex);
                            dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.Rows.Count - 1];
                        }
                    }
                    if (dgNutrition.Rows.Count - 1 == dgNutrition.CurrentCell.RowIndex)
                    {
                        dgNutrition.Rows.RemoveAt(dgNutrition.CurrentCell.RowIndex);
                        dgNutrition.Rows.Add();
                    }
                    else
                    {
                        dgNutrition.Rows.RemoveAt(dgNutrition.CurrentCell.RowIndex);
                    }
                    dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionName, dgNutrition.Rows.Count - 1];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void delete_Rrow()
        {
            try
            {
                if (OMMessageBox.Show("Are you sure want to delete this item ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (dgReceipe.Rows[dgReceipe.CurrentCell.RowIndex].Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() != "")
                    {
                        long NurPkSrNo = Convert.ToInt64(dgReceipe.Rows[dgReceipe.CurrentCell.RowIndex].Cells[ColReceipe.PkSrNo].Value);
                        if (NurPkSrNo != 0)
                        {
                            DeleteDtls(2, NurPkSrNo);
                            dgReceipe.Rows.RemoveAt(dgReceipe.CurrentCell.RowIndex);
                            dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.Rows.Count - 1];
                        }
                    }
                    if (dgReceipe.Rows.Count - 1 == dgReceipe.CurrentCell.RowIndex)
                    {
                        dgReceipe.Rows.RemoveAt(dgReceipe.CurrentCell.RowIndex);
                        dgReceipe.Rows.Add();
                    }
                    else
                    {
                        dgReceipe.Rows.RemoveAt(dgReceipe.CurrentCell.RowIndex);
                    }
                    dgReceipe.CurrentCell = dgReceipe[ColNutrition.NutritionName, dgReceipe.Rows.Count - 1];
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgListNutrition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dgNutrition.CurrentRow.Cells[0].EditedFormattedValue.ToString().Trim() != "")
                {
                    dgNutrition.CurrentRow.Cells[ColNutrition.NutritionName].Value = dgListNutrition.CurrentRow.Cells[0].EditedFormattedValue.ToString();
                    dgNutrition.CurrentRow.Cells[ColNutrition.NutritionUOM].Value = dgListNutrition.CurrentRow.Cells[1].EditedFormattedValue.ToString();
                    dgNutrition.CurrentRow.Cells[ColNutrition.NutritionNo].Value = dgListNutrition.CurrentRow.Cells[2].EditedFormattedValue.ToString();
                    dgNutrition.CurrentRow.Cells[ColNutrition.NStatus].Value = "0";

                    if (dgNutrition.CurrentRow.Cells[ColNutrition.PkSrNo].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgNutrition.CurrentRow.Cells[ColNutrition.PkSrNo].Value = 0;
                        dgNutrition.CurrentRow.Cells[ColNutrition.NStatus].Value = "1";
                    }
                    else
                    {
                        if (Convert.ToInt64(dgNutrition.CurrentRow.Cells[ColNutrition.PkSrNo].EditedFormattedValue.ToString()) != 0)
                        {
                            dgNutrition.CurrentRow.Cells[ColNutrition.NStatus].Value = "1";
                        }
                    }
                    pnlNutrition.Visible = false;
                    dgNutrition.CurrentCell = dgNutrition[ColNutrition.NutritionValue, dgNutrition.CurrentRow.Index];
                    dgNutrition.Focus();
                }
            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                pnlNutrition.Visible = false;
                dgNutrition.Focus();
            }
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
                    //1 From MStockItemsNutrition
                    if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 1)
                    {
                        mstockitemsnutrition.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbMStockItemsIngredient.DeleteMStockItemsNutrition(mstockitemsnutrition);
                    }
                    if (Convert.ToInt32(dtDelete.Rows[i].ItemArray[0]) == 2)
                    {
                        mStockItemsReceipe.PkSrNo = Convert.ToInt64(dtDelete.Rows[i].ItemArray[1]);
                        dbMStockItemsIngredient.DeleteMStockItemsReceipe(mStockItemsReceipe);
                    }
                }
                dtDelete.Rows.Clear();
            }
        }


        #endregion

        private void btnHead_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLangHead1.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtHead1.Text.Trim(), txtLangHead1.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLangHead1.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtHead1.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtHead1.Text.Trim(), txtLangHead1.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLangHead1.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLangHead1.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnHead2_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLangHead2.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtHead2.Text.Trim(), txtLangHead2.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLangHead2.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtHead2.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtHead2.Text.Trim(), txtLangHead2.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLangHead2.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLangHead2.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnHead3_Click(object sender, EventArgs e)
        {
            try
            {
                Utilities.KeyBoard frmkb;
                if (txtLangHead3.Text.Trim().Length > 0)
                {
                    frmkb = new Utilities.KeyBoard(1, txtHead3.Text.Trim(), txtLangHead3.Text, "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        txtLangHead3.Text = frmkb.strLanguage.Trim();
                        frmkb.Close();
                    }
                }
                else
                {
                    string val = ObjFunction.ChecklLangVal(txtHead3.Text.Trim());
                    if (val == "")
                    {
                        frmkb = new Utilities.KeyBoard(4, txtHead3.Text.Trim(), txtLangHead3.Text, "", "");
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            txtLangHead3.Text = frmkb.strLanguage.Trim();
                            frmkb.Close();
                        }
                    }
                    else
                        txtLangHead3.Text = val;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtHead1_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtHead1.Text.Trim() != "")
                {
                    if (FlagBilingual == true)
                    {
                        if (txtLangHead1.Text.Trim().Length == 0)
                        {
                            btnHead_Click(btnHead, null);
                        }
                    }
                    else
                        txtHead2.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtHead1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHead1_Leave(sender, new EventArgs());
                txtLangHead1.Focus();
            }
        }

        private void txtHead2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHead2_Leave(sender, new EventArgs());
                txtLangHead2.Focus();
            }
        }

        private void txtHead2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtHead2.Text.Trim() != "")
                {
                    if (FlagBilingual == true)
                    {
                        if (txtLangHead2.Text.Trim().Length == 0)
                        {
                            btnHead2_Click(btnHead2, null);
                        }
                    }
                    else
                        txtHead3.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtHead3_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtHead3.Text.Trim() != "")
                {
                    if (FlagBilingual == true)
                    {
                        if (txtLangHead3.Text.Trim().Length == 0)
                        {
                            btnHead3_Click(btnHead3, null);
                        }
                    }
                    else
                        txtHead3.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtHead3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtHead3_Leave(sender, new EventArgs());
                txtLangHead3.Focus();
            }
        }

        private void dgReceipe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgReceipe.CurrentRow.Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString().Trim() != "")
                    delete_Rrow();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dgReceipe.CurrentCell.ColumnIndex == ColNutrition.SrNo)
                {
                    if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.CurrentRow.Index];
                    else
                        dgReceipe.CurrentCell = dgReceipe[dgReceipe.Rows.Count - 1, ColReceipe.ReceipeDesc];
                    dgReceipe.Focus();
                }
                else if (dgReceipe.CurrentCell.ColumnIndex == ColReceipe.ReceipeDesc)
                {
                    if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.CurrentRow.Index];
                    else
                    {
                        dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].ErrorText = "";
                        if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].ErrorText = "Please Enter Value";
                            dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.CurrentRow.Index];
                        }
                        else
                            dgReceipe.CurrentCell = dgReceipe[ColReceipe.LangReceipeDesc, dgReceipe.CurrentRow.Index];
                    }
                }
                else if (dgReceipe.CurrentCell.ColumnIndex == ColReceipe.LangReceipeDesc)
                {
                    if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.CurrentRow.Index];
                    else
                    {
                        dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].ErrorText = "";
                        if (dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].ErrorText = "Please Enter Value";
                            dgReceipe.CurrentCell = dgReceipe[ColReceipe.LangReceipeDesc, dgReceipe.CurrentRow.Index];
                        }
                        else
                        {
                            if (dgReceipe.Rows.Count == dgReceipe.CurrentRow.Index + 1)
                            {
                                dgReceipe.Rows.Add();
                            }
                            dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.Rows.Count - 1];
                        }
                    }
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                btnSave.Focus();
            }
        }

        private void dgReceipe_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (ColReceipe.SrNo == e.ColumnIndex)
            {
                e.Value = e.RowIndex + 1;
            }
        }

        private void dgReceipe_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MovetoNext move2n = new MovetoNext(m2n);

            if (dgReceipe.CurrentCell.ColumnIndex == ColNutrition.SrNo)
            {
                if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                    dgReceipe.CurrentCell = dgReceipe[ColReceipe.ReceipeDesc, dgReceipe.CurrentRow.Index];
                else
                    dgReceipe.CurrentCell = dgReceipe[dgReceipe.Rows.Count - 1, ColReceipe.ReceipeDesc];
                dgReceipe.Focus();
            }
            else if (dgReceipe.CurrentCell.ColumnIndex == ColReceipe.ReceipeDesc)
            {

                dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].ErrorText = "";
                if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() != "" || dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].EditedFormattedValue.ToString().Trim() != "")
                {
                    if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                    {
                        dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].ErrorText = "Please Enter Value";
                        BeginInvoke(move2n, new object[] { dgReceipe.CurrentRow.Index, ColReceipe.ReceipeDesc, dgReceipe });
                    }
                    else
                        BeginInvoke(move2n, new object[] { dgReceipe.CurrentRow.Index, ColReceipe.LangReceipeDesc, dgReceipe });
                    dgReceipe.CurrentRow.Cells[ColReceipe.Status].Value = "1";
                }
            }
            else if (dgReceipe.CurrentCell.ColumnIndex == ColReceipe.LangReceipeDesc)
            {

                if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                    BeginInvoke(move2n, new object[] { dgReceipe.CurrentRow.Index, ColReceipe.ReceipeDesc, dgReceipe });
                else
                {
                    if (dgReceipe.CurrentRow.Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() != "" || dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].EditedFormattedValue.ToString().Trim() != "")
                    {
                        dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].ErrorText = "";
                        if (dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgReceipe.CurrentRow.Cells[ColReceipe.LangReceipeDesc].ErrorText = "Please Enter Value";
                            BeginInvoke(move2n, new object[] { dgReceipe.CurrentRow.Index, ColReceipe.LangReceipeDesc, dgReceipe });
                        }
                        else
                        {
                            dgReceipe.CurrentRow.Cells[ColReceipe.Status].Value = "1";

                            if (dgReceipe.Rows.Count == dgReceipe.CurrentRow.Index + 1)
                            {
                                dgReceipe.Rows.Add();
                                dgReceipe.Rows[dgReceipe.Rows.Count - 1].Cells[ColReceipe.Status].Value = "0";
                                dgReceipe.Rows[dgReceipe.Rows.Count - 1].Cells[ColReceipe.PkSrNo].Value = "0";
                            }
                            BeginInvoke(move2n, new object[] { dgReceipe.Rows.Count - 1, ColReceipe.ReceipeDesc, dgReceipe });
                        }
                    }
                }
            }
        }

        public delegate void MovetoNext(int RowIndex, int ColIndex, DataGridView dg);

        public void m2n(int RowIndex, int ColIndex, DataGridView dg)
        {
            try
            {
                if (dg.CurrentCell.Value != null)
                    dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
                else
                    dg.CurrentCell = dg.Rows[RowIndex].Cells[ColIndex];
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public Boolean Validation()
        {
            bool flag = true;

            if (dgNutrition.Rows.Count >= 1)
            {
                for (int i = 0; i < dgNutrition.Rows.Count; i++)
                {
                    if (dgNutrition.Rows[i].Cells[ColNutrition.NutritionName].EditedFormattedValue.ToString() != "")
                    {
                        dgNutrition.Rows[i].Cells[ColNutrition.NutritionValue].ErrorText = "";
                        dgNutrition.Rows[i].Cells[ColNutrition.NutritionUOM].ErrorText = "";
                        if (dgNutrition.Rows[i].Cells[ColNutrition.NutritionValue].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgNutrition.Rows[i].Cells[ColNutrition.NutritionValue].ErrorText = "Please Enter Value";
                            flag = false;
                            break;
                        }
                        if (dgNutrition.Rows[i].Cells[ColNutrition.NutritionUOM].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgNutrition.Rows[i].Cells[ColNutrition.NutritionUOM].ErrorText = "Please Enter Value";
                            flag = false;
                            break;
                        }
                    }
                }
            }

            if (flag == true && dgReceipe.Rows.Count >= 1)
            {
                if (dgReceipe.Rows.Count >= 1)
                {
                    for (int i = 0; i < dgReceipe.Rows.Count - 1; i++)
                    {
                        dgReceipe.Rows[i].Cells[ColReceipe.ReceipeDesc].ErrorText = "";
                        dgReceipe.Rows[i].Cells[ColReceipe.LangReceipeDesc].ErrorText = "";
                        if (dgReceipe.Rows[i].Cells[ColReceipe.ReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgReceipe.Rows[i].Cells[ColReceipe.ReceipeDesc].ErrorText = "Please Enter Value";
                            flag = false;
                            break;
                        }
                        if (dgReceipe.Rows[i].Cells[ColReceipe.LangReceipeDesc].EditedFormattedValue.ToString().Trim() == "")
                        {
                            dgReceipe.Rows[i].Cells[ColReceipe.LangReceipeDesc].ErrorText = "Please Enter Value";
                            flag = false;
                            break;
                        }
                    }
                }
            }

            return flag;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation())
            {

                dbMStockItemsIngredient = new DBMStockItemsIngredient();

                DeleteValues();

                mStockItemsIngredient = new MStockItemsIngredient();
                mStockItemsIngredient.PkSrNo = ID;
                mStockItemsIngredient.IngredientValue = txtIngredient.Text.Trim();
                mStockItemsIngredient.NutritionHeadValue = txtHead.Text.Trim();
                mStockItemsIngredient.ReceipeHead1 = txtHead1.Text.Trim();
                mStockItemsIngredient.ReceipeHead2 = txtHead2.Text.Trim();
                mStockItemsIngredient.ReceipeHead3 = txtHead3.Text.Trim();
                mStockItemsIngredient.LangReceipeHead1 = txtLangHead1.Text.Trim();
                mStockItemsIngredient.LangReceipeHead2 = txtLangHead2.Text.Trim();
                mStockItemsIngredient.LangReceipeHead3 = txtLangHead3.Text.Trim();
                mStockItemsIngredient.CompanyNo = DBGetVal.FirmNo;
                mStockItemsIngredient.ItemNo = ItemNo;
                mStockItemsIngredient.UserID = DBGetVal.UserID;
                mStockItemsIngredient.UserDate = DBGetVal.ServerTime;
                dbMStockItemsIngredient.AddMStockItemsIngredient(mStockItemsIngredient);

                for (int i = 0; i < dgNutrition.Rows.Count - 1; i++)
                {
                    if (dgNutrition[ColNutrition.NStatus, i].EditedFormattedValue.ToString().Trim() == "1")
                    {
                        mstockitemsnutrition = new MStockItemsNutrition();
                        mstockitemsnutrition.PkSrNo = Convert.ToInt64(dgNutrition[ColNutrition.PkSrNo, i].EditedFormattedValue.ToString());
                        mstockitemsnutrition.NutritionNo = Convert.ToInt64(dgNutrition[ColNutrition.NutritionNo, i].EditedFormattedValue.ToString());
                        mstockitemsnutrition.NutritionValue = dgNutrition[ColNutrition.NutritionValue, i].EditedFormattedValue.ToString();
                        mstockitemsnutrition.NutritionUOM = dgNutrition[ColNutrition.NutritionUOM, i].EditedFormattedValue.ToString();
                        mstockitemsnutrition.SequenceNo = Convert.ToInt64(dgNutrition[ColNutrition.SrNo, i].EditedFormattedValue.ToString());
                        mstockitemsnutrition.CompanyNo = DBGetVal.FirmNo;
                        mstockitemsnutrition.UserID = DBGetVal.UserID;
                        mstockitemsnutrition.UserDate = DBGetVal.ServerTime;
                        dbMStockItemsIngredient.AddMStockItemsNutrition(mstockitemsnutrition);
                    }
                }

                for (int i = 0; i < dgReceipe.Rows.Count - 1; i++)
                {
                    if (dgReceipe[ColReceipe.Status, i].EditedFormattedValue.ToString().Trim() == "1")
                    {
                        mStockItemsReceipe = new MStockItemsReceipe();
                        mStockItemsReceipe.PkSrNo = Convert.ToInt64((dgReceipe[ColReceipe.PkSrNo, i].EditedFormattedValue.ToString() == "") ? "0" : dgReceipe[ColReceipe.PkSrNo, i].EditedFormattedValue.ToString());
                        mStockItemsReceipe.ReceipeDesc = dgReceipe[ColReceipe.ReceipeDesc, i].EditedFormattedValue.ToString();
                        mStockItemsReceipe.LangReceipeDesc = dgReceipe[ColReceipe.LangReceipeDesc, i].EditedFormattedValue.ToString();
                        mStockItemsReceipe.SequenceNo = Convert.ToInt64(dgReceipe[ColReceipe.SrNo, i].EditedFormattedValue.ToString());
                        mStockItemsReceipe.CompanyNo = DBGetVal.FirmNo;
                        mStockItemsReceipe.UserID = DBGetVal.UserID;
                        mStockItemsReceipe.UserDate = DBGetVal.ServerTime;
                        dbMStockItemsIngredient.AddMStockItemsReceipe(mStockItemsReceipe);
                    }
                }

                long TempId = dbMStockItemsIngredient.ExecuteNonQueryStatements();
                if (TempId != 0)
                {
                    if (ID == 0)
                    {
                        OMMessageBox.Show("Stock Items Ingredient Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        ID = TempId;
                        FillControls();

                    }
                    else
                    {
                        OMMessageBox.Show("Stock Items Ingredient Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }
                }
                else
                {
                    OMMessageBox.Show("Stock Items Ingredient not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
        }

        private void txtIngredient_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dgNutrition_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgNutrition.CurrentCell != null)
                {
                    for (int i = 0; i < dgNutrition.Rows.Count; i++)
                    {
                        dgNutrition.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    dgNutrition.Rows[dgNutrition.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clrColorRow;
                    dgNutrition.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgReceipe_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgReceipe.CurrentCell != null)
                {
                    for (int i = 0; i < dgReceipe.Rows.Count; i++)
                    {
                        dgReceipe.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                    dgReceipe.Rows[dgReceipe.CurrentCell.RowIndex].DefaultCellStyle.BackColor = clrColorRow;
                    dgReceipe.CurrentCell.Style.SelectionBackColor = Color.LightCyan;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


    }
}
