using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OM;

namespace Yadi
{
    partial class About : Form
    {
        string strPwd = "";
        public About()
        {   
            InitializeComponent(); 
        }

        private void About_Load(object sender, EventArgs e)
        {
            lblSystemName.Text = CommonFunctions.SystemName + " System";
            txtAboutInfo.Text = CommonFunctions.AboutInfo;
            txtAboutInfo.SelectionStart = 0;
            txtAboutInfo.SelectionLength = 0;
            txtAboutInfo.Font = new Font("Arial", 8, FontStyle.Regular);
        }

        private void txtAboutInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            int KeyCode = (int)e.KeyChar;

            if ((KeyCode >= 65 && KeyCode <= 90) || (KeyCode >= 97 && KeyCode <= 122) || (KeyCode >= 48 && KeyCode <= 57))
            {
                strPwd = strPwd + e.KeyChar.ToString();
                if (strPwd == "kamsh")
                {
                    lblMainInfo.Visible = true;
                    lblMainInfo.Text = CommonFunctions.SecureInfo;
                    strPwd = "";
                }
            }
        }

        private void txtAboutInfo_GotFocus(object sender, EventArgs e)
        {
            strPwd = "";
        }

        private void lblMainInfo_Click(object sender, EventArgs e)
        {
            lblMainInfo.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
