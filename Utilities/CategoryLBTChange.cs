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
    public partial class CategoryLBTChange : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public CategoryLBTChange()
        {
            InitializeComponent();
        }

        private void CategoryLBTChange_Load(object sender, EventArgs e)
        {

            BindData();
        }


        private void BindData()
        {
            txtSearch.Text = "";
            btnSave.Enabled = false;
            while (dgDetails.Rows.Count > 0)
                dgDetails.Rows.RemoveAt(0);

            string sql = " SELECT   0 as SrNo,StockGroupName,LBTVal,StockGroupNo,0 AS ChkChange,IsLBTApply" +
                        " FROM  MStockGroup " +
                        " WHERE (ControlGroup = 2) AND (IsActive = 'True') " +
                        " Order By StockGroupName";

            DataTable dt = ObjFunction.GetDataView(sql).Table;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgDetails.Rows.Add();
                for (int j = 0; j < dgDetails.ColumnCount; j++)
                {
                    dgDetails.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j].ToString();
                    if (ColIndex.IsLBTApply == j)
                    {
                        if (Convert.ToBoolean(dgDetails.Rows[i].Cells[ColIndex.IsLBTApply].Value) == false)
                            dgDetails.Rows[i].Cells[ColIndex.Value].ReadOnly = true;
                    }
                }
            }
            if (dgDetails.Rows.Count > 0)
            {
                dgDetails.CurrentCell = dgDetails[ColIndex.Value, 0];
                dgDetails.Focus();
            }
        }


        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int CategeryName = 1;
            public static int Value = 2;
            public static int GroupNo1 = 3;
            public static int chkChange = 4;
            public static int IsLBTApply = 5;
        }

        private void dgDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (ColIndex.Value == e.ColumnIndex)
            {
                dgDetails.Rows[e.RowIndex].Cells[ColIndex.Value].ErrorText = "";
                if (dgDetails.Rows[e.RowIndex].Cells[ColIndex.Value].EditedFormattedValue.ToString().Trim() == "")
                {
                    dgDetails.Rows[e.RowIndex].Cells[ColIndex.Value].ErrorText = "Enter LBT Value(%)";
                    //dgDetails.CurrentCell = dgDetails.Rows[e.RowIndex].Cells[ColIndex.Value];
                    //dgDetails.Focus();
                }
                else
                {
                    btnSave.Enabled = true;
                    dgDetails.Rows[e.RowIndex].Cells[ColIndex.chkChange].Value = "1";
                    dgDetails.Rows[e.RowIndex].Cells[ColIndex.Value].Style.BackColor = Color.LightBlue;
                    //dgDetails.CurrentCell = dgDetails[ColIndex.Value, (dgDetails.Rows.Count - 1 == dgDetails.CurrentRow.Index ? dgDetails.CurrentRow.Index : dgDetails.CurrentRow.Index + 1)];
                    //dgDetails.Focus();
                }
            }
        }

        private void dgDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgDetails.CurrentCell.ColumnIndex == ColIndex.Value)
            {
                TextBox txt1 = (TextBox)e.Control;
                txt1.TextChanged += new EventHandler(txtValue_TextChanged);
            }
        }

        public void txtValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgDetails.CurrentCell.ColumnIndex == ColIndex.Value)
                {
                    ObjFunction.SetMasked((TextBox)sender, 2, 2, OMFunctions.MaskedType.NotNegative);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgDetails_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (ColIndex.Value == dgDetails.CurrentCell.ColumnIndex)
                    {
                        e.SuppressKeyPress = true;
                        if (dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[ColIndex.Value].ErrorText == "")
                        {
                            dgDetails.CurrentCell = dgDetails.Rows[((dgDetails.Rows.Count - 1 == dgDetails.CurrentRow.Index) ? dgDetails.CurrentRow.Index : dgDetails.CurrentRow.Index + 1)].Cells[ColIndex.Value];
                            dgDetails.Focus();
                        }
                        else
                        {
                            dgDetails.CurrentCell = dgDetails.Rows[dgDetails.CurrentRow.Index].Cells[ColIndex.Value];
                            dgDetails.Focus();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    btnSave.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public Boolean Validation()
        {
            Boolean flag = true;

            for (int i = 0; i < dgDetails.Rows.Count; i++)
            {
                if (dgDetails.Rows[i].Cells[ColIndex.Value].ErrorText != "")
                {
                    flag = false;
                    dgDetails.CurrentCell = dgDetails.Rows[i].Cells[ColIndex.Value];
                    dgDetails.Focus();
                    break;
                }
            }
            if (flag == true)
            {
                for (int i = 0; i < dgDetails.Rows.Count; i++)
                {
                    if (dgDetails.Rows[i].Cells[ColIndex.chkChange].FormattedValue.ToString() == "1")
                    {
                        flag = true;
                        break;
                    }
                    else flag = false;
                }
                if (flag == false)
                    OMMessageBox.Show("Change At least once LBT Vlaue", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

            }
            return flag;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            bool flag = false;
            try
            {
                if (Validation())
                {
                    for (int i = 0; i < dgDetails.Rows.Count; i++)
                    {
                        if (dgDetails.Rows[i].Cells[ColIndex.chkChange].FormattedValue.ToString() == "1")
                        {

                            ObjTrans.Execute("Update MStockGroup Set LBTVal=" + dgDetails.Rows[i].Cells[ColIndex.Value].Value.ToString() + " Where StockGroupNo=" + dgDetails.Rows[i].Cells[ColIndex.GroupNo1].Value.ToString() + "", CommonFunctions.ConStr);

                            double LBTVG = Convert.ToDouble(dgDetails.Rows[i].Cells[ColIndex.Value].FormattedValue.ToString());

                            string sql = "Update TStock Set LBTPerce=" + LBTVG + ",TStock.LBTAmount=(((LBTApplicableAmount * " + LBTVG + ")/100)) " +
                                       " Where ItemNo In(Select ItemNo From MStockItems Where GroupNo1=" + dgDetails.Rows[i].Cells[ColIndex.GroupNo1].FormattedValue.ToString() + ") ";
                            ObjTrans.Execute(sql, CommonFunctions.ConStr);

                            flag = true;
                        }
                    }
                    if (flag == true)
                    {
                        OMMessageBox.Show("Category LBT Change Successfully.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        BindData();
                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchGridValue(dgDetails, txtSearch.Text.Replace("'", "''"));
        }

        public void SearchGridValue(DataGridView dg, string strSearch)
        {
            int i = 0, cnt = 0;
            try
            {
                if (strSearch != "")
                {
                    for (i = 0; i < dg.Rows.Count; i++)
                    {
                        dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                        cnt = 0;
                        for (int j = 0; j < strSearch.Trim().ToUpper().Length; j++)
                        {
                            if (dg.Rows[i].Cells[ColIndex.CategeryName].Value != null)
                            {
                                if (strSearch.Trim().ToUpper()[j].ToString() == dg.Rows[i].Cells[ColIndex.CategeryName].Value.ToString().Trim().ToUpper()[j].ToString())
                                    cnt++;
                                else break;
                            }
                        }
                        if (cnt == strSearch.Trim().ToUpper().Length)
                        {
                            dg.CurrentCell = dg.Rows[i].Cells[ColIndex.Value];
                            dg.Rows[i].DefaultCellStyle.BackColor = CommonFunctions.RowColor;
                        }
                        cnt = 0;
                    }
                }
                else
                {
                    for (i = 0; i < dg.Rows.Count; i++)
                    {
                        dg.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                e.SuppressKeyPress = true;
                dgDetails.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
