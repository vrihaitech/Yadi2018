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
    public partial class SchemeActivation : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        DBMScheme dbScheme = new DBMScheme();
        public SchemeActivation()
        {
            InitializeComponent();
        }

        private void SchemeActivation_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        public void BindGrid()
        {
            DataTable dt = ObjFunction.GetDataView(" SELECT 0 as SrNo,MSchemeType.SchemeTypeName, MScheme.SchemeName, MScheme.SchemePeriodFrom, MScheme.SchemePeriodTo, MScheme.SchemeNo,'false' as IsActive " +
                                                " FROM  MScheme INNER JOIN MSchemeType ON MScheme.SchemeTypeNo = MSchemeType.SchemeTypeNo " +
                                                " Where MScheme.IsActive=0 and  MScheme.SchemeDate='" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + "' and " +
                                                "  MScheme.SchemePeriodFrom >='" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + "' and  MScheme.SchemePeriodTo<='" + DBGetVal.ServerTime.Date.ToString("dd-MMM-yyyy") + "'").Table;
            GridView.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GridView.Rows.Add();
                for (int j = 0; j < GridView.Columns.Count; j++)
                {
                    GridView.Rows[i].Cells[j].Value = dt.Rows[i].ItemArray[j];
                }
            }
            if (GridView.Rows.Count > 0)
            {
                BtnSave.Enabled = true;
                GridView.CurrentCell = GridView[6, 0];
                GridView.Focus();
            }
            else
                BtnSave.Enabled = false;
        }

        private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Value = (e.RowIndex + 1).ToString();
            }
            if (e.ColumnIndex == 3 || e.ColumnIndex==4)
            {
                if (e.Value != null)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("dd-MMM-yyyy");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            dbScheme = new DBMScheme();
            int cnt = 0;
            for (int i = 0; i < GridView.Rows.Count; i++)
            {
                cnt++;
                dbScheme.UpdateMScheme(Convert.ToInt64(GridView.Rows[i].Cells[5].Value));
            }
            if (cnt > 0)
            {
                if (dbScheme.ExecuteNonQueryStatementsScheme() ==true)
                {
                    OMMessageBox.Show("Scheme Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    BindGrid();
                }
                else
                {
                    OMMessageBox.Show("Select Atleast One Scheme To Udpate", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                }
            }
        }

        private void GridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                BtnSave.Focus();
            }
        }
    }
}
