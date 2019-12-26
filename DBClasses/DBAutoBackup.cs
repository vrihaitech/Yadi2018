using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Management;
using OMControls;

namespace OM
{
    class DBAutoBackup
    {
        CommonFunctions ObjFunction = new CommonFunctions();

        public void backup()
        {
            string str, str1;
            SqlConnection MyConn = new SqlConnection(CommonFunctions.ConStr);
            DBAssemblyInfo dbAss = new DBAssemblyInfo();
            str = CommonFunctions.DatabaseName;// GetDatabaseName(CommonFunctions.ConStr);
            //string strDir = "C:\\" + dbAss.AssemblyTitle;
            string strDir = ObjFunction.GetAppSettings(AppSettings.O_BackUpPath);//+ dbAss.AssemblyTitle;
            if (Directory.Exists(strDir) == false)
            {
                Directory.CreateDirectory(strDir);
            }
            DateTime dtNow = System.DateTime.Now;
            string fn = strDir + "\\" + str + "_" + dtNow.ToString("dd-MMM-yy") + "_" + dtNow.Hour + "." + dtNow.Minute + ".Bak";

            str1 = "BACKUP DATABASE " + str + " TO disk = '" + fn + "'";
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

        public void backupOne()
        {
            string str, str1;
            SqlConnection MyConn = new SqlConnection(CommonFunctions.ConStr);
            DBAssemblyInfo dbAss = new DBAssemblyInfo();
            str = CommonFunctions.DatabaseName;// GetDatabaseName(CommonFunctions.ConStr);
           // string strDir = "C:\\" + dbAss.AssemblyTitle;
            string strDir = ObjFunction.GetAppSettings(AppSettings.O_BackUpPath); //+ dbAss.AssemblyTitle;
            if (Directory.Exists(strDir) == false)
            {
                try
                {
                    Directory.CreateDirectory(strDir);
                }
                catch (Exception ex)
                {
                    CommonFunctions.ErrorMessge = ex.Message;
                    OMMessageBox.Show("Invalid Path Specified for Backup in setting.\n\n\nPlease update valid backup path in setting.");
                    return;
                }
            }

            DateTime dtNow = System.DateTime.Now;
            //string fn = strDir + "\\" + str + "_" + dtNow.ToString("dd-MMM-yy") + "_" + dtNow.Hour + "." + dtNow.Minute + ".Bak";
            string fn = strDir + "\\" + str + "_" + dtNow.ToString("dd-MMM-yy") + ".Bak";
            //string fn = strDir + "\\" + str + "_" + Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yy") + ".Bak";

            if (File.Exists(fn) == false)
            {
                str1 = "BACKUP DATABASE " + str + " TO disk = '" + fn + "'";
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

        }

        public void ExportBackUp()
        {
            string MainPath = ObjFunction.GetAppSettings(AppSettings.O_UpDownLoadPath) + "\\RetailerFile";
            string str, str1;
            SqlConnection MyConn = new SqlConnection(CommonFunctions.ConStr);
            str = ObjFunction.GetDatabaseName(CommonFunctions.ConStr);
            string strDir = ObjFunction.GetAppSettings(AppSettings.O_BackUpPath); //+ dbAss.AssemblyTitle;
            if (Directory.Exists(strDir) == false)
            {
                Directory.CreateDirectory(strDir);
            }

            if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "").ToUpper() != System.Net.Dns.GetHostName().ToUpper())
            {
                return;
            }
            DateTime dtNow = System.DateTime.Now;
            string fn = MainPath + "\\NewCSV\\" + str + "_" + dtNow.ToString("dd-MMM-yy") + "_" + dtNow.Hour + "_" + dtNow.Minute + ".Bak";

            if (File.Exists(fn) == false)
            {
                str1 = "BACKUP DATABASE " + str + " TO disk = '" + fn + "'";
                try
                {
                    SqlCommand myCommand1 = new SqlCommand(str1, MyConn);
                    MyConn.Open();
                    myCommand1.ExecuteNonQuery();

                    OMControls.OMWinZip objZip = new OMWinZip();
                    string FileName = MainPath + "\\NewCSV\\" + DBGetVal.FirmNo + "_" +
                                DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("HHmmss") + ".zip";
                    objZip.ConvertFileToZip(FileName, fn);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(FileName, true);
                    sw.WriteLine("");
                    sw.Close();
                }
                catch (Exception ex)
                {
                    CommonFunctions.ErrorMessge = ex.Message;
                }
                MyConn.Close();
                MyConn = null;
            }

        }
    }
}
