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

namespace Yadi
{
    public partial class BackUp : Form
    { 
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        
        public BackUp()
        {
            InitializeComponent();
        }

        private void BackUp_Load(object sender, EventArgs e)
        {
            string ConStr = "Data Source="+ CommonFunctions.ServerName +";Integrated Security=SSPI;Initial Catalog=Master";
            //SetSamhita();
            DataSet ds = new DataSet();
            ds = ObjDset.FillDset("New", "Select  CompanyNo, CompanyName, BooksBeginFrom, BooksEndOn from Mcompany", CommonFunctions.ConStr);
            //ds = ObjDset.FillDset("New", "select name as [Company Name],db_id(name) as [No] from sysdatabases  where left(name,150)='Account18'", ConStr); 

            bindingSource1.DataSource = ds.Tables[0];
            DataGridView1.DataSource = bindingSource1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {           
            if (txtPath.Text != "")
            {
                if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "") == System.Net.Dns.GetHostName())
                {
                    System.Threading.Thread tr = new System.Threading.Thread(backup);
                    tr.Start();
                }
                else
                    OMMessageBox.Show("Doesn't allow Data backup from Client side....", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
            else
            {
                OMMessageBox.Show("Please Select Valid Path Name");
            }
        }

        public void backup()
        {          
            string str, str1;
            SqlConnection MyConn = new SqlConnection("Data Source="+ CommonFunctions.ServerName +";Initial Catalog=Master;Integrated Security=True");

            str = DBGetVal.DBName;// GetDatabaseName(CommonFunctions.ConStr);      
            string fn = this.txtPath.Text + "\\" + str + "_" + Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yy") + Convert.ToDateTime(System.DateTime.Now).ToString(" hh:mm") + ".Bak";

            str1 = "BACKUP DATABASE " + str + " TO disk = '" + fn + "'  ";
            try
            {
                SqlCommand myCommand1 = new SqlCommand(str1, MyConn);
                MyConn.Open();
                myCommand1.ExecuteNonQuery();
                OMMessageBox.Show("Back Up is successfully Completed to  " + fn);
            }
            catch (Exception ex)
            {
                OMMessageBox.Show((ex.ToString()));
                OMMessageBox.Show("Please Select Valid Path Name");
            }
            MyConn.Close();
            MyConn = null;
        }

        private void BtnBackUp_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            txtPath.Text = folderBrowserDialog1.SelectedPath;
        }        
    }
}
