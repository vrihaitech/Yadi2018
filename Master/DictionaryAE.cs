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
    public partial class DictionaryAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMLanguageDictionary dbLang = new DBMLanguageDictionary();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();


        public static long RequestCategoryNo;

        public DictionaryAE()
        {
            InitializeComponent();
        }

        private void DictionaryAE_Load(object sender, EventArgs e)
        {
            BindGrid();
            TxtSearch.Focus();
        }

        private void BindGrid()
        {
            DataView dv = new DataView();
            dv = dbLang.GetBySearch("Lang", TxtSearch.Text);


            DataGridView1.DataSource = dv;
            DataGridView1.Columns[0].Visible = false;

            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                DataGridView1.RowTemplate.DefaultCellStyle.Font = null;// ObjFunction.GetFont();
                DataGridView1.Columns[2].DefaultCellStyle.Font = ObjFunction.GetLangFont();
                DataGridView1.Columns[1].Width = (DataGridView1.Width / 2) - 10;
                DataGridView1.Columns[2].Width = (DataGridView1.Width / 2) - 10;
            }
            else
                DataGridView1.Columns[1].Width = DataGridView1.Width;
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                if (DataGridView1.Rows.Count > 0)
                {
                    DataGridView1.Focus();
                    DataGridView1.CurrentCell = DataGridView1[1, 0];
                }
            }
            else if ((e.KeyCode == Keys.N && e.Control) || e.KeyCode == Keys.F5)
            {
                string val = ObjFunction.ChecklLangVal(TxtSearch.Text.Trim());
                if (val == "")
                {
                    Utilities.KeyBoard frmkb = new Utilities.KeyBoard(2, TxtSearch.Text, "", "", "");
                    ObjFunction.OpenForm(frmkb);
                    if (frmkb.DS == DialogResult.OK)
                    {
                        BindGrid();
                        frmkb.Close();
                    }
                    else
                    {
                        frmkb.Close();
                    }
                }
                else
                    OMMessageBox.Show("This value is already exist...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Enter)
            {
                DataGridView1_CellContentDoubleClick(sender, new DataGridViewCellEventArgs(DataGridView1.CurrentCell.ColumnIndex, DataGridView1.CurrentCell.RowIndex));
            }
            else if (DataGridView1.CurrentCell.RowIndex >= 0 && e.KeyCode == Keys.Delete)
            {
                if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Information, OMMessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    long PkSrNo = Convert.ToInt64(DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    DBMLanguageDictionary dbLang = new DBMLanguageDictionary();
                    MLanguageDictionary mLang = new MLanguageDictionary();
                    mLang.PkSrNo = PkSrNo;
                    if (dbLang.DeleteMLanguageDictionary(mLang) == true)
                        OMMessageBox.Show("Dictionary deleted successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    else
                        OMMessageBox.Show("Dictionary not deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    BindGrid();
                    TxtSearch.Text = "";
                    TxtSearch.Focus();
                }
            }
            if (e.KeyCode == Keys.Up)
            {
                if (DataGridView1.CurrentCell.RowIndex == 0)
                    TxtSearch.Focus();
            }
        }

        private void DataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Form frmkb = new Utilities.KeyBoard(3, DataGridView1.SelectedRows[0].Cells[1].Value.ToString(), DataGridView1.SelectedRows[0].Cells[2].Value.ToString(), "", "");
            ObjFunction.OpenForm(frmkb);
            BindGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAutoCheck_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAutoCheck_Click_1(object sender, EventArgs e)
        {
            try
            {
                String str = "", strEnglish = "", strMarathi = "";
                DataTable dtEnglish;
                Utilities.KeyBoard frmkb;
                int type = 0;
                if (chkItemGroup.Checked == true)
                {
                    str = "Select ItemGroupNo,ItemGroupName from MItemGroup where ItemGroupNo>10";
                    type = 0;
                }
                else if (chkItemName.Checked == true)
                {
                    str = "Select ItemNo,ItemName from MItemMaster";
                    type = 1;
                }
                else if (chkCustomer.Checked == true)
                {
                    str = "Select LedgerNo,LedgerName from MLedger where GroupNo=26";
                    type = 2;
                }
                else if (chkSupplier.Checked == true)
                {
                    str = "Select LedgerNo,LedgerName from MLedger where GroupNo=22";
                    type = 2;
                }
                if (str != "")
                {
                    dtEnglish = ObjFunction.GetDataView(str).Table;
                    for (int i = 0; i < dtEnglish.Rows.Count; i++)
                    {
                        strEnglish = dtEnglish.Rows[i].ItemArray[1].ToString();
                        strMarathi = "";
                        if (strEnglish.Trim().Length != 0)
                        {
                            string val = ObjFunction.ChecklLangVal(strEnglish.Trim());
                            if (val == "")
                            {
                                frmkb = new Utilities.KeyBoard(4, strEnglish.Trim(), strMarathi, "", "");
                                ObjFunction.OpenForm(frmkb);
                                if (frmkb.DS == DialogResult.OK)
                                {
                                    strMarathi = frmkb.strLanguage.Trim();
                                    if (type == 0)
                                    {
                                        ObjTrans.ExecuteQuery("Update  MItemGroup set LangGroupName='" + strMarathi + "' where ItemGroupNo=" + Convert.ToInt32(dtEnglish.Rows[i].ItemArray[0].ToString()), CommonFunctions.ConStr);
                                    }
                                    else if (type == 1)
                                    {
                                        ObjTrans.ExecuteQuery("Update  MItemMaster set LangFullDesc='" + strMarathi + "' where ItemNo=" + Convert.ToInt32(dtEnglish.Rows[i].ItemArray[0].ToString()), CommonFunctions.ConStr);
                                    }
                                    else if (type == 2)
                                    {
                                        ObjTrans.ExecuteQuery("Update  MLedger set LedgerLangName='" + strMarathi + "' where LedgerNo=" + Convert.ToInt32(dtEnglish.Rows[i].ItemArray[0].ToString()), CommonFunctions.ConStr);
                                    }
                                    frmkb.Close();
                                }
                            }
                            else
                            {
                                strMarathi = val;
                                if (type == 0)
                                {
                                    ObjTrans.ExecuteQuery("Update  MItemGroup set LangGroupName='" + strMarathi + "' where ItemGroupNo=" + Convert.ToInt32(dtEnglish.Rows[i].ItemArray[0].ToString()), CommonFunctions.ConStr);
                                }
                                else if (type == 1)
                                {
                                    ObjTrans.ExecuteQuery("Update  MItemMaster set LangFullDesc='" + strMarathi + "' where ItemNo=" + Convert.ToInt32(dtEnglish.Rows[i].ItemArray[0].ToString()), CommonFunctions.ConStr);
                                }
                                else if (type == 2)
                                {
                                    ObjTrans.ExecuteQuery("Update  MLedger set LedgerLangName='" + strMarathi + "' where LedgerNo=" + Convert.ToInt32(dtEnglish.Rows[i].ItemArray[0].ToString()), CommonFunctions.ConStr);
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
    }
}
