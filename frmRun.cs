using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;
using OM;
using OMControls;
using System.Globalization;

namespace Yadi
{
    partial class frmRun : Form
    {
        OMCommonClass obj = new OMCommonClass();
        DBAssemblyInfo dbAss = new DBAssemblyInfo();
        
        public frmRun()
        {
            InitializeComponent();
            


          //  this.Text = String.Format("About " + dbAss.AssemblyTitle);
            this.labelProductName.Text = CommonFunctions.SystemName + " System";// dbAss.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", dbAss.AssemblyVersion);
            this.labelCopyright.Text = dbAss.AssemblyCopyright;
            this.labelCompanyName.Text = "Powered by " + dbAss.AssemblyCompany;
            this.lblTradeMark.Text = dbAss.AssemblyTrademark;
            //OMMessageBox.SetColor(Color.Blue, Color.Red, Color.Yellow);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PB.Value += 1;
            if (PB.Value >= 100)
            {
                timer1.Enabled = false;
                this.Hide();
                this.Visible = false;
                int chkRes = obj.CheckRegistration("", 1, dbAss.AssemblyTitle, dbAss.AssemblyProduct, dbAss.AssemblyVersion, dbAss.AssemblyCopyright, dbAss.AssemblyGUID, CommonFunctions.DBName);
                if (chkRes == 1)
                {
                    Login frm = new Login();
                    frm.RegType = 0;
                    frm.Show();
                    
                    this.Hide();
                }
                else if (chkRes == -1)
                {
                    frmRegistered frm = new frmRegistered();
                    frm.ShowDialog();
                }
                
            }
        }

        private void frmRun_Load(object sender, EventArgs e)
        { 
          
            SetDateFormat();
            OMMessageBox.SetColor(Color.NavajoWhite, Color.NavajoWhite, Color.SandyBrown, Color.SandyBrown);
            IsOpenApplication();
            obj = new OMCommonClass();
            obj.setPath(dbAss.AssemblyTitle);
            ///pnlPB.Visible = true;
            PB.Minimum = 0; PB.Maximum = 100;
            PB.Value = 0; PB.Step = 1;
            PB.GlowPositionSpeed = 6;
            timer1.Enabled = true;
        }

        public bool IsProcessOpen(string name, int ID)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName == name && clsProcess.Id == ID)
                {
                    return true;
                }
            }
            return false;
        }

        public int IsProcessRun(string name, int SessionId)
        {
            int cnt = 0;
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName == name && clsProcess.SessionId == SessionId)
                {
                    cnt = cnt + 1;
                }
            }
            return cnt;
        }

        public void IsOpenApplication()
        {
            Process thisProc = Process.GetCurrentProcess();
            if (IsProcessOpen(Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id) == false)
            {
                //System.Windows.OMMessageBox.Show("Application not open!");
                //System.Windows.Application.Current.Shutdown();
            }
            else
            {
                // Check how many total processes have the same name as the current one
                if (IsProcessRun(Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().SessionId) > 3)
                {
                    // If ther is more than one, than it is already running.
                    OMMessageBox.Show("Application is already running.", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }

        }

        public void SetDateFormat()
        {
            try
            {
                Microsoft.Win32.RegistryKey rkey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                // Set value for short Date Format
                if (rkey.GetValue("sShortDate").ToString() != "M/d/yyyy")
                    rkey.SetValue("sShortDate", "M/d/yyyy");
                // Set value for Long Date Format
                if (rkey.GetValue("sLongDate").ToString() != "dddd, MMMM dd,yyyy")
                    rkey.SetValue("sLongDate", "dddd, MMMM dd,yyyy");
                if (rkey.GetValue("sShortTime").ToString() != "h:mm tt")
                    rkey.SetValue("sShortTime", "h:mm tt");
                if (rkey.GetValue("sTimeFormat").ToString() != "h:mm:ss tt")
                    rkey.SetValue("sTimeFormat", "h:mm:ss tt");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
