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
    public partial class ServerRegistration : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Security secure = new Security();
        DBMSettings dbMSettings = new DBMSettings();

        public ServerRegistration()
        {
            InitializeComponent();
        }

        private void ServerRegistration_Load(object sender, EventArgs e)
        {
            string strConnectionString = ObjQry.ReturnString("Select DatabaseServer From MServerSettings", CommonFunctions.ConStr);
            if (strConnectionString != "")
            {
                strConnectionString = secure.psDecrypt(strConnectionString);
                txtServerName.Text = ObjFunction.GetServerName(strConnectionString);
                txtDatabase.Text = ObjFunction.GetDatabaseName(strConnectionString);
                txtUserID.Text = ObjFunction.GetServerUserID(strConnectionString);
                txtPassword.Text = ObjFunction.GetServerPassword(strConnectionString);
            }
            string StrCSVFTP = ObjQry.ReturnString("Select CSVFTPServer from MServerSettings", CommonFunctions.ConStr);
            int Cnt = 0; //string str = "";
            if (StrCSVFTP != "")
            {
                StrCSVFTP = secure.psDecrypt(StrCSVFTP);
                Cnt=StrCSVFTP.IndexOf("{");
                txtUser.Text = StrCSVFTP.Substring(0, Cnt);
                StrCSVFTP = StrCSVFTP.Remove(0, Cnt + 4);
                Cnt=StrCSVFTP.IndexOf("{");
                txtPwd.Text = StrCSVFTP.Substring(0, Cnt);
                StrCSVFTP=StrCSVFTP.Remove(0, Cnt + 4);
                Cnt = StrCSVFTP.IndexOf("{");
                txtHostName.Text = StrCSVFTP.Substring(0, Cnt);
                StrCSVFTP=StrCSVFTP.Remove(0, Cnt + 4);
                txtPortID.Text = StrCSVFTP;
            }
            txtServerName.Focus();            
            ObjFunction.SetAppSettings();
            chkAutoDataUpload.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.AutoUploadData));
            txtUploadHrs.Text = ObjFunction.GetAppSettings(AppSettings.AutoUploadHrs);
            txtUploadMins.Text = ObjFunction.GetAppSettings(AppSettings.AutoUploadMins);

            txtZipHrs.Text = ObjFunction.GetAppSettings(AppSettings.ZipUploadHrs);
            txtZipMins.Text = ObjFunction.GetAppSettings(AppSettings.ZipUploadMins);
            chkBackupUploadCsv.Checked = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_SODUploadBackup));
            if (ObjFunction.GetAppSettings(AppSettings.AutoUploadType) == "1")
                rbDBToDB.Checked = true;
            else if (ObjFunction.GetAppSettings(AppSettings.AutoUploadType) == "2")
                rbDbToCSV.Checked = true;
            else
                rbDBToDB.Checked = true;
            RbCheckChange();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtUploadHrs.Text == "") txtUploadHrs.Text = "0";
            if (txtUploadMins.Text == "") txtUploadMins.Text = "0";
            if (txtZipHrs.Text == "") txtZipHrs.Text = "0";
            if (txtZipMins.Text == "") txtZipMins.Text = "0";
            if (Convert.ToDouble(Convert.ToInt64(txtUploadHrs.Text) + "." + Convert.ToInt64(txtUploadMins.Text)) < 0.2)
            {
                OMMessageBox.Show("Ping After Hours minimum set greater than or equal to 2 mins", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }
            if (Convert.ToDouble(Convert.ToInt64(txtZipHrs.Text) + "." + Convert.ToInt64(txtZipMins.Text)) < 0.30)
            {
                OMMessageBox.Show("Backup Zip Hours minimum set greater than or equal to 30 mins", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                return;
            }

            CommonFunctions.ConStrServer = "Data Source=" + txtServerName.Text + ";Initial Catalog=" + txtDatabase.Text + ";User ID=" + txtUserID.Text + ";Password=" + txtPassword.Text + "";
            string strFtp = txtUser.Text.Trim()+"{;};"+txtPwd.Text.Trim()+"{;};"+txtHostName.Text.Trim()+"{;};"+txtPortID.Text.Trim();
            bool Result = false;
            if (rbDBToDB.Checked == true)
            {
                if (ObjQry.ReturnLong("Select Count(*) From MServerSettings", CommonFunctions.ConStr) == 0)
                    Result = ObjTrans.Execute("Insert into MServerSettings values ('" + secure.psEncrypt(CommonFunctions.ConStrServer) + "',1,'" + secure.psEncrypt(strFtp) + "'," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                else
                    Result = ObjTrans.Execute("Update MServerSettings set DatabaseServer='" + secure.psEncrypt(CommonFunctions.ConStrServer) + "' ,StatusNo=2,CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
            }
            if (rbDbToCSV.Checked == true)
            {
                if (ObjQry.ReturnLong("Select Count(*) From MServerSettings", CommonFunctions.ConStr) == 0)
                    Result = ObjTrans.Execute("Insert into MServerSettings values ('" + secure.psEncrypt(CommonFunctions.ConStrServer) + "',1,'" + secure.psEncrypt(strFtp) + "'," + DBGetVal.FirmNo + ")", CommonFunctions.ConStr);
                else
                    Result = ObjTrans.Execute("Update MServerSettings set CSVFTPServer='" + secure.psEncrypt(strFtp) + "',StatusNo=2,CompanyNo=" + DBGetVal.FirmNo + "", CommonFunctions.ConStr);
            }
            if (Result == true)
            {
                dbMSettings.AddAppSettings(AppSettings.AutoUploadData, (chkAutoDataUpload.Checked ? "True" : "False"));
                dbMSettings.AddAppSettings(AppSettings.AutoUploadHrs, txtUploadHrs.Text);
                dbMSettings.AddAppSettings(AppSettings.AutoUploadMins, txtUploadMins.Text);

                dbMSettings.AddAppSettings(AppSettings.ZipUploadHrs, txtZipHrs.Text);
                dbMSettings.AddAppSettings(AppSettings.ZipUploadMins, txtZipMins.Text);
                dbMSettings.AddAppSettings(AppSettings.O_SODUploadBackup, (chkBackupUploadCsv.Checked ? "True" : "False"));

                dbMSettings.AddAppSettings(AppSettings.AutoUploadType, (rbDBToDB.Checked ? "1" : "2"));
                if (dbMSettings.ExecuteNonQueryStatements() == true)
                {
                    MDIParent1.ScheduleTime = (Convert.ToInt64(txtUploadHrs.Text) * 360000) + (Convert.ToInt64(txtUploadMins.Text) * 60000);
                    MDIParent1.CounterExport = 1;
                    OMMessageBox.Show("Server Settings Save successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                }
                else
                {
                    OMMessageBox.Show("Server Settings not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            else
            {
                OMMessageBox.Show("Server Settings not Saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUploadHrs_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtUploadHrs);
        }

        private void txtUploadMins_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtUploadMins);
        }

        private void chkAutoDataUpload_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoDataUpload.Checked == true)
                chkAutoDataUpload.Text = "Yes";
            else
                chkAutoDataUpload.Text = "No";
        }

        private void txtUploadHrs_Leave(object sender, EventArgs e)
        {
            if (txtUploadHrs.Text.Trim() == "")
                txtUploadHrs.Text = "0";
        }

        private void txtUploadMins_Leave(object sender, EventArgs e)
        {
            if (txtUploadMins.Text.Trim() == "")
                txtUploadMins.Text = "0";
        }

        private void rbDBToDB_CheckedChanged(object sender, EventArgs e)
        {
            RbCheckChange();
        }

        private void rbDbToCSV_CheckedChanged(object sender, EventArgs e)
        {
            RbCheckChange();
        }

        public void RbCheckChange()
        {
            if (rbDbToCSV.Checked == true)
            {
                txtUser.Focus();
                pnlDBToCSV.Visible = true;
                pnlDBToDB.Visible = false;
            }
            else if (rbDBToDB.Checked == true)
            {
                txtServerName.Focus();
                pnlDBToDB.Visible = true;
                pnlDBToCSV.Visible = false;
            }
                
        }

        private void txtPortID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnSave.Focus();
            }
        }

        private void txtZipHrs_Leave(object sender, EventArgs e)
        {
            if (txtZipHrs.Text.Trim() == "")
                txtZipHrs.Text = "0";
        }

        private void txtZipMins_Leave(object sender, EventArgs e)
        {
            if (txtZipMins.Text.Trim() == "")
                txtZipMins.Text = "0";
        }

        private void txtZipHrs_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtZipHrs);
        }

        private void txtZipMins_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric(txtZipMins);
        }

        private void chkSodUploadCsv_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBackupUploadCsv.Checked == true)
                chkBackupUploadCsv.Text = "Yes";
            else
                chkBackupUploadCsv.Text = "No";
        }

      
    }
}
