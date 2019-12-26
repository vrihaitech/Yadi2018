using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using OM;
using OMControls;

namespace Yadi
{
    public partial class frmRegistered : Form
    {
        OMCommonClass obj = new OMCommonClass(true);
        DBAssemblyInfo dbAss = new DBAssemblyInfo();
        string strPwd = "";
        public frmRegistered()
        {
            InitializeComponent();
        }

        private void btnRegistered_Click(object sender, EventArgs e)
        {
            obj.ClientName = txtServerName.Text;
            obj.setPath(dbAss.AssemblyTitle);
            int chkRes = obj.CheckRegistration(txtRegID.Text, 2, dbAss.AssemblyTitle, dbAss.AssemblyProduct, dbAss.AssemblyVersion, dbAss.AssemblyCopyright, dbAss.AssemblyGUID, CommonFunctions.DBName);
            if (chkRes == 1)
            {
                Login frm = new Login();
                frm.RegType = 1;
                frm.Show();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmRegistered_Load(object sender, EventArgs e)
        {
            txtKey.Text = obj.EncryptGenMacID();
        }

        private void txtKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            int KeyCode = (int)e.KeyChar;

            if ((KeyCode >= 65 && KeyCode <= 90) || (KeyCode >= 97 && KeyCode <= 122) || (KeyCode >= 48 && KeyCode <= 57))
            {
                strPwd = strPwd + e.KeyChar.ToString();
                if (obj.CheckKeyPassword(strPwd, 1) == true)
                {
                    obj.DisplaySerialKey(txtKey, txtRegID);
                    strPwd = "";
                }
                else if (obj.CheckKeyPassword(strPwd, 2) == true)
                {
                    txtRegID.Text = "";
                    strPwd = "";
                }
            }
        }

        private void txtKey_GotFocus(object sender, EventArgs e)
        {
            strPwd = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtServerName.Text = "";
            txtServerName.Visible = rdClient.Checked;
        }

        private void rdServer_CheckedChanged(object sender, EventArgs e)
        {
            txtServerName.Text = "";
        }

       
    }
}
