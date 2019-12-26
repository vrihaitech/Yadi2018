using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using OM;
using OMControls;
using System.Threading;
using Yadi.DBClasses;
using System.IO;

namespace Yadi
{
    public partial class MDIParent1 : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        int procID = 0;
        public static string DailyItm;
        public bool Logout = false;
        public static long AutoTimer, CounterTimer, CounterDate, CounterExport, CounterZip;
        string[] strNode; int cntRow = 0, cntCol = 0;
        Security secure = new Security();
        DBMUser dbUser = new DBMUser();
        DataTable dtMenu = new DataTable();
        DBReportStatus dbReportStatus = new DBReportStatus();
        ReportStatus ReportStatus = new ReportStatus();
        public static long ScheduleTime, ScheduleZipTime;
        public static bool CloseFlag = false, isSalesOrderAvailable = false;
        public delegate void Vch(); bool SpaceFlag = false; string strPsw;
        Color clrNotifyBlinking = Color.Red;
        public long CompNo, LedgNo, MNo;
        public static string EmailRptName = "";
        List<string> Path = new List<string>();
        bool EmailFlag = false;
        public MDIParent1()
        {
            InitializeComponent();
            AutoTimer = 360;
            CounterTimer = 0;
            CounterDate = 0;
            CounterExport = 0;
        }

        private void RefreshMainForm()
        {
            //toolBar1.Buttons.Clear();
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            pnlMainMenu.Visible = true;
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = CommonFunctions.SystemName + " System";
                dtMenu = ObjFunction.GetDataView("Select * from MMenuMaster order by SrNo").Table;
                CommonFunctions.dtHelp = ObjFunction.GetDataView("SELECT  MHelpFormDtls.FormName, MHelp.HelpInfo FROM MHelpFormDtls INNER JOIN MHelp ON MHelpFormDtls.FKHelpInfo = MHelp.PkSrNo").Table;
                ObjFunction.FormatControl(this.Controls);
                ObjFunction.KeyPressFormat(this.Controls);

                ObjFunction.GetVoucherLock();
                lblCompName.Text = CommonFunctions.CompanyName;
                CommonFunctions.mMain = this;
                //btnBilling.Enabled = false; btnItemChangeRate.Enabled = false; btnSalesReturn.Enabled = false;
                //btnPurchase.Enabled = false; btnPurchaseReturn.Enabled = false; btnEOD.Enabled = false;
                SetMenus();

                // DisableMainControl();
                toolBar1.Buttons.Clear();
                toolBar1.ButtonSize = new System.Drawing.Size(24, 24);
                pnlActivate.Visible = false; lblMainTitle.Text = "";
                string imagePath = ObjQry.ReturnString("Select LogoPath From Mcompany", CommonFunctions.ConStr);
                if (System.IO.File.Exists(imagePath) == true)
                    this.BackgroundImage = System.Drawing.Image.FromFile(imagePath);
                TSUserName.Text = "User Name:" + DBGetVal.UserName;
                TSAppVersion.Text = "Version: " + new DBAssemblyInfo().AssemblyVersion;
                if (CommonFunctions.ServerName.ToUpper().Replace("\\SQLEXPRESS", "") == System.Net.Dns.GetHostName().ToUpper())
                    TSCompName.Text = "" + System.Net.Dns.GetHostName().ToUpper() + " (Server)";
                else
                    TSCompName.Text = "" + System.Net.Dns.GetHostName().ToUpper() + " (Client)";

                //btnMinimize.Top = 0;
                KeyDownFormat(this.Controls);
                btnHome.BackColor = Color.Gainsboro;
                lblDispTaskbar.Location = new Point(this.Width - lblDispTaskbar.Width - 10, this.Height - lblDispTaskbar.Height - 10);

                btnMinimize.BringToFront();
                btnMinimize.Visible = true;
                TSCmpName.Text = "Powered by " + new DBAssemblyInfo().AssemblyCompany;
                ScheduleTime = (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.AutoUploadHrs)) * 360) + (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.AutoUploadMins)) * 60000);
                ScheduleZipTime = (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.ZipUploadHrs)) * 360) + (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.ZipUploadMins)) * 60000);
                if (Convert.ToDateTime(ObjFunction.GetAppSettings(AppSettings.O_SOD)) < DBGetVal.ServerTime.Date)
                {
                    Form NewF = new Master.StartDay();
                    ObjFunction.OpenForm(NewF);
                }

                //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)) == false)
                //{
                //    btnBilling.Text = btnBilling.Text.Replace("Billing Firm Wise".ToUpper(), "Sales Bill".ToUpper());
                //}
                //  SetStockGroup();
                btnHome.Focus();
                CloseFlag = false;
                //AlertNotification();
                if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "") == System.Net.Dns.GetHostName())
                {
                    ObjFunction.DeleteFiles(ObjFunction.GetAppSettings(AppSettings.O_BackUpPath), 7, "bak");
                }


                if (DBGetVal.KachhaFirm == true)
                {
                    //strPsw = "";
                    // DBGetVal.KachhaFirm = true;
                    lblCompName.BackColor = Color.Magenta;

                }
                else if (DBGetVal.KachhaFirm == false)
                {
                    //strPsw = "";
                    //DBGetVal.KachhaFirm = false;
                    lblCompName.BackColor = Color.FromArgb(255, 93, 173, 226);//Equals={Color [A=255, R=93, G=173, B=226]}
                }

                if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_IsAutoEmailSend)) == true)
                {
                    long FrequencyNo = ObjQry.ReturnLong("Select FrequencyNo From Frequency Where IsMail='true'", CommonFunctions.ConStr);

                    // DataTable dtReportType = ObjFunction.GetDataView("Select ReportName From ReportType where IsMail='true'").Table;


                    if (FrequencyNo == 1)
                    {
                        string From = ObjQry.ReturnDate("Select isnull(max(LastSentDateTime),0) From ReportStatus where  FrequencyNo=1", CommonFunctions.ConStr).ToString("MM/dd/yyyy");
                        string Today = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");



                        if (From.ToString() == "01/01/1900")
                        {
                            From = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy");
                        }

                        DateTime start = Convert.ToDateTime(From);
                        DateTime end = Convert.ToDateTime(Today);
                        TimeSpan difference = end - start;
                        int Diff = difference.Days;
                        for (int i = 0; i <= Diff; i++)
                        {
                            From = Convert.ToDateTime(From).AddDays(i).ToString();
                            Print(From, Today, "15");
                            Print(From, Today, "9");
                            EmailFlag = true;
                        }

                        if (EmailFlag == true)
                        {
                            EmailSend(DBGetVal.Email, FrequencyNo, 1);
                            EmailFlag = false;
                        }
                        else
                        {

                        }

                    }
                    else if (FrequencyNo == 2)
                    {

                    }
                    else if (FrequencyNo == 3)
                    {

                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        public void Print(string FromDate, string ToDate, string VchType)
        {
            string[] ReportSession;
            ReportSession = new string[6];
            ReportSession[0] = VchType;
            ReportSession[1] = DBGetVal.FirmNo.ToString();
            ReportSession[2] = Convert.ToDateTime(FromDate).ToString("dd-MMM-yyyy");
            ReportSession[3] = Convert.ToDateTime(ToDate).ToString("dd-MMM-yyyy");

            ReportSession[4] = "2";
            ReportSession[5] = "0";
            try
            {
                EmailRptName = "DailySalesDetails";
                CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                childForm = null;
                childForm = ObjFunction.LoadReportObject("RPTSalesRegisterDateWiseSummary.rpt", CommonFunctions.ReportPath);

                if (childForm != null)
                {
                    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession, true, EmailRptName, true);

                    if (objRpt.PrintReport() == true)
                    {
                        Path.Add(objRpt.newFullPath);
                    }
                }
            }
            catch
            {

            }
        }

        void EmailSend(string EmailId, long FrequencyNo, long ReportType)
        {
            try
            {
                SendMail.SendEmail(EmailId, null, GetEmailSubAndBody().EmailSubject, GetEmailSubAndBody().EmailBody, Path);
                dbReportStatus = new DBReportStatus();
                ReportStatus = new ReportStatus();

                ReportStatus.FrequencyNo = FrequencyNo;
                ReportStatus.ReportType = ReportType;
                ReportStatus.LastSentDateTime = DateTime.Now;

                if (dbReportStatus.AddReportStatus(ReportStatus) == true)
                {
                    DisplayMessage("Email Send Successfully.....!!!");
                }
                else
                {
                    DisplayMessage("Email Not Send....... !!!");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void DisplayMessage(string str)
        {
            lblMsg.Visible = true;
            lblMsg.Text = str;
            Application.DoEvents();
            System.Threading.Thread.Sleep(700);
            lblMsg.Visible = false;
        }
        private EmailTemplateBO GetEmailSubAndBody()
        {
            EmailTemplateBO oEmailTemplateBO;
            try
            {
                oEmailTemplateBO = new EmailTemplateBO();
                oEmailTemplateBO.EmailSubject = "Sales Bill";

                oEmailTemplateBO.EmailBody =

                  "  <div style=\"width:100%;border:solid 1px;border-color:lightgray\"></div><br/>" +

                    "<span style=\"font-family: Arial, Helvetica, sans-serif; font-size: 12;\">Dear Sir/Madam, <br/><br/>I have send demo email. <br/>"

                    + "<br/><b> Thanks & Regards,</b><br/>Shrriyans Shah<br/>VIRHA ITech,NanaPeth<br/>Pune</span>";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oEmailTemplateBO;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            CounterDate++;
            //TSSDateTime.Text = DateTime.Now.ToString("dd-MMM-yyyy") + "  " + DateTime.Now.ToLongTimeString();
            if (CounterDate == 1 || CounterDate >= 600) //10 Mins
            {
                CounterDate = 1;
                DBGetVal.ServerTime = CommonFunctions.GetTime();
            }
            else
            {
                DBGetVal.ServerTime = DBGetVal.ServerTime.AddSeconds(1);
            }

            TSSDateTime.Text = DBGetVal.ServerTime.ToString("dd-MMM-yyyy") + "  " + DBGetVal.ServerTime.ToShortTimeString();

            if (CounterTimer == 0)
            {
                if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "").ToUpper() == System.Net.Dns.GetHostName().ToUpper())
                {
                    DBAutoBackup dbAuto = new DBAutoBackup();
                    System.Threading.Thread tr = new System.Threading.Thread(dbAuto.backupOne);
                    tr.Start();
                }
            }
            if (CounterTimer == AutoTimer)
            {
                CounterTimer = 0;
                if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "").ToUpper() == System.Net.Dns.GetHostName().ToUpper())
                {
                    DBAutoBackup dbAuto = new DBAutoBackup();
                    System.Threading.Thread tr = new System.Threading.Thread(dbAuto.backup);
                    tr.Start();
                }
            }
            CounterTimer++;
            if (CounterTimer == 360) CounterTimer = 0;
            if (CounterExport == 0) TransferData(0);
            if (ScheduleTime > 0) { if (CounterExport >= (ScheduleTime / 1000)) { CounterExport = 0; TransferData(0); } } else CounterExport = 1;
            CounterExport++;
            if (CounterZip == 0) TransferData(1);
            if (ScheduleZipTime > 0) { if (CounterZip >= (ScheduleZipTime / 1000)) { CounterZip = 0; TransferData(1); } } else CounterZip = 1;
            CounterZip++;
            //setToolBarControl();
            //CheckNotify();
        }

        private void MDIParent1_Resize(object sender, EventArgs e)
        {
            try
            {
                int WD = this.Width;
                TSUserName.Width = Convert.ToInt32(WD * 0.60);
                pnlSubMenu.Location = new Point(pnlMainMenu.Width / 2 - pnlSubMenu.Width / 2, pnlMainMenu.Height / 2 - pnlSubMenu.Height / 2);
                lblDispTaskbar.Location = new Point(this.Width - lblDispTaskbar.Width - 10, this.Height - lblDispTaskbar.Height - 10);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e1)
        {
            DBAssemblyInfo dbAss = new DBAssemblyInfo();
            if (CloseFlag == false)
            {
                CloseFlag = true;
                if (OMMessageBox.Show("Do You Want To Quit ? ", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e1.Cancel = true;
                    CloseFlag = false;
                }
            }
            else if (CloseFlag == true && Logout == true)
            {
                Logout = false;
                if (OMMessageBox.Show("Do You Want To LogOut ? ", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.OpenForms["Login"].Show();
                }
                else
                {
                    e1.Cancel = true;
                    CloseFlag = false;
                }
            }
        }

        public ToolStripMenuItem DisplayMenu(ToolStripMenuItem TD, long Srno)
        {
            ToolStripMenuItem tdtemp;
            DataTable dtTree = dbUser.GetNodesByNodeID(Srno).Table;// ObjFunction.GetDataView("Select SrNo,MenuID,MenuName From MMenuMaster Where ControlMenu=" + Srno + "").Table;
            if (dtTree.Rows.Count > 0)
            {

                for (int i = 0; i < dtTree.Rows.Count; i++)
                {
                    if (dtTree.Rows[i].ItemArray[0].ToString() != "8")
                    {
                        tdtemp = new ToolStripMenuItem(dtTree.Rows[i].ItemArray[2].ToString());
                        tdtemp.Click += new EventHandler(ClickMenus);
                        tdtemp.Name = dtTree.Rows[i].ItemArray[0].ToString();
                        if (Srno == 0)
                        {
                            cntCol = 0;
                            if (cntCol < strNode[cntRow].Length)
                                if (strNode[cntRow][cntCol].ToString() == "1") tdtemp.Checked = true;
                            cntCol++;
                        }
                        else
                        {
                            if (cntCol < strNode[cntRow].Length)
                                if (strNode[cntRow][cntCol].ToString() == "1") tdtemp.Checked = true;
                            cntCol++;
                        }

                        tdtemp = DisplayMenu(tdtemp, Convert.ToInt64(dtTree.Rows[i].ItemArray[0].ToString()));
                        if (Convert.ToInt64(dtTree.Rows[i].ItemArray[0].ToString()) == 14)
                            setShortKey(tdtemp, Keys.S);
                        else if (Convert.ToInt64(dtTree.Rows[i].ItemArray[0].ToString()) == 15)
                            setShortKey(tdtemp, Keys.P);
                        if (dtTree.Rows[i].ItemArray[3].ToString() != "") setShortKey(tdtemp, dtTree.Rows[i].ItemArray[3].ToString());
                        if (Srno == 0)
                        {
                            if (tdtemp.Checked == true)
                            {
                                tdtemp.Checked = false;
                                if (tdtemp.Name == "1")
                                {
                                    //ToolStripSeparator ts = new ToolStripSeparator();
                                    //tdtemp.DropDownItems.Add(ts);
                                    //ToolStripMenuItem t = new ToolStripMenuItem("Refresh"); t.Name = "Refresh";
                                    //t.Click += new EventHandler(ClickMenus);
                                    //tdtemp.DropDownItems.Add(t);
                                    //t = new ToolStripMenuItem("Logout"); t.Name = "Logout";
                                    //t.Click += new EventHandler(ClickMenus);
                                    //tdtemp.DropDownItems.Add(t);
                                    //t = new ToolStripMenuItem("Exit"); t.Name = "Exit";
                                    //t.Click += new EventHandler(ClickMenus);
                                    //tdtemp.DropDownItems.Add(t);
                                }
                                tdtemp.ForeColor = Color.Black;
                                tdtemp.MouseHover += new EventHandler(tdtemp_MouseHover);
                                tdtemp.MouseLeave += new EventHandler(tdtemp_MouseLeave);
                                // DisableMainControl(Convert.ToInt64(tdtemp.Name));
                                menuStrip.Items.Add(tdtemp);
                            }
                            cntRow++;
                        }
                        else
                        {
                            if (tdtemp.Checked == true)
                            {
                                //DisableMainControl(Convert.ToInt64(tdtemp.Name));
                                tdtemp.Checked = false;
                                TD.DropDownItems.Add(tdtemp);
                            }
                        }
                    }
                }

            }
            return TD;
        }

        private void tdtemp_MouseLeave(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).ForeColor = Color.Black;
            ((ToolStripMenuItem)sender).BackColor = Color.Transparent;
        }

        private void tdtemp_MouseHover(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).ForeColor = Color.Black;
            ((ToolStripMenuItem)sender).BackColor = Color.Yellow;
        }
        //private void tdtemp1_MouseLeave(object sender, EventArgs e)
        //{
        //    ((ToolStripMenuItem)sender).ForeColor = Color.Black;
        //    ((ToolStripMenuItem)sender).BackColor = Color.Transparent;
        //}

        //private void tdtemp1_MouseHover(object sender, EventArgs e)
        //{
        //    ((ToolStripMenuItem)sender).ForeColor = Color.Black;
        //    ((ToolStripMenuItem)sender).BackColor = Color.Yellow;
        //}

        private void tdtemp1_Click(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {

                if (DBGetVal.KachhaFirm == false)
                {
                    DBGetVal.KachhaFirm = true;
                    lblCompName.BackColor = Color.Magenta;

                }
                else if (DBGetVal.KachhaFirm == true)
                {

                    DBGetVal.KachhaFirm = false;
                    lblCompName.BackColor = Color.FromArgb(255, 93, 173, 226);//Equals={Color [A=255, R=93, G=173, B=226]}
                }
                foreach (Form childForm in MdiChildren)
                {
                    childForm.Close();
                }
                pnlMainMenu.Visible = true;
            }
        }

        public void SetMenus()
        {
            try
            {
                DataTable dtTree = ObjFunction.GetDataView("SELECT * FROM MUserMenuMaster where FKUserId =" + DBGetVal.UserID + "").Table;
                strNode = new string[dtTree.Columns.Count - 4];
                for (int i = 0; i < dtTree.Columns.Count - 4; i++)
                {
                    strNode[i] = secure.psDecrypt(dtTree.Rows[0].ItemArray[i + 2].ToString());
                }
                menuStrip.Items.Clear();
                cntRow = 0; cntCol = 0;
                DisplayMenu(new ToolStripMenuItem(), 0);
                DisplayMainMenu();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisplayMainMenu()
        {
            ToolStripMenuItem tdtemp = new ToolStripMenuItem("Help");
            tdtemp.Click += new EventHandler(ClickMenus);
            tdtemp.Name = "Help";
            tdtemp.Checked = false;

            ToolStripMenuItem t = new ToolStripMenuItem("Refresh"); t.Name = "Refresh";
            t.Click += new EventHandler(ClickMenus);
            tdtemp.DropDownItems.Add(t);

            ToolStripSeparator ts = new ToolStripSeparator();
            tdtemp.DropDownItems.Add(ts);

            t = new ToolStripMenuItem("Logout"); t.Name = "Logout";
            t.Click += new EventHandler(ClickMenus);
            tdtemp.DropDownItems.Add(t);

            ts = new ToolStripSeparator();
            tdtemp.DropDownItems.Add(ts);

            t = new ToolStripMenuItem("Backup"); t.Name = "Backup";
            t.Click += new EventHandler(ClickMenus);
            tdtemp.DropDownItems.Add(t);

            t = new ToolStripMenuItem("Exit"); t.Name = "Exit";
            t.Click += new EventHandler(ClickMenus);
            tdtemp.DropDownItems.Add(t);

            tdtemp.ForeColor = Color.Black;
            tdtemp.MouseHover += new EventHandler(tdtemp_MouseHover);
            tdtemp.MouseLeave += new EventHandler(tdtemp_MouseLeave);
            menuStrip.Items.Add(tdtemp);

            ToolStripMenuItem tdtemp1 = new ToolStripMenuItem("   ");
            tdtemp1.Click += new EventHandler(ClickMenus);
            tdtemp1.Name = "   ";
            tdtemp1.Checked = false;
            tdtemp1.ForeColor = Color.Black;
            //   tdtemp1.MouseHover += new EventHandler(tdtemp1_MouseHover);
            // tdtemp1.MouseLeave += new EventHandler(tdtemp1_MouseLeave);
            tdtemp1.MouseDown += new MouseEventHandler(tdtemp1_Click);
            menuStrip.Items.Add(tdtemp1);
        }

        public void setShortKey(ToolStripMenuItem TD, Keys keys)
        {
            TD.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | keys)));
        }

        public void setShortKey(ToolStripMenuItem TD, string strShort)
        {
            TD.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | (Keys)((int)strShort[0]))));
        }

        private void ClickMenus(object sender, EventArgs e)
        {
            try
            {
                Form childForm = new Form();
                ToolStripMenuItem TSM = (ToolStripMenuItem)sender;
                if (ObjFunction.CheckNumeric(TSM.Name) == true)
                {
                    DataRow[] dr = dtMenu.Select("SrNo=" + TSM.Name + "");
                    if (dr.Length > 0)
                    {
                        childForm = ObjFunction.GetFormObject(dr[0][4].ToString(), dr);
                        if (childForm != null)
                        {
                            //pnlMainMenu.Visible = false;
                            ObjFunction.OpenForm(childForm, this);
                        }
                    }
                }
                else
                {
                    switch (TSM.Name)
                    {
                        case "Exit":
                            this.Close();
                            break;
                        case "Backup":
                            if (CommonFunctions.ServerName.Replace("\\SQLEXPRESS", "").ToUpper() == System.Net.Dns.GetHostName().ToUpper())
                            {
                                DBAutoBackup dbAuto = new DBAutoBackup();
                                System.Threading.Thread tr = new System.Threading.Thread(dbAuto.backup);
                                tr.Start();
                            }
                            else OMMessageBox.Show("Backup allowed only server package", CommonFunctions.ConStr, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            break;
                        case "Refresh":
                            RefreshMainForm();
                            break;
                        case "Logout":
                            CloseFlag = true; Logout = true;
                            this.Close();
                            break;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void RunHandHeld()
        {
            try
            {
                System.Diagnostics.Process newProc1 = null;
                newProc1 = System.Diagnostics.Process.Start(DBGetVal.DirectoryPath + "PrjPDAComm");
                procID = newProc1.Id;
                newProc1.WaitForExit();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TeamViwer()
        {
            try
            {
                System.Diagnostics.Process newProc1 = null;
                newProc1 = System.Diagnostics.Process.Start(DBGetVal.DirectoryPath + "TeamViewerQS_en");
                procID = newProc1.Id;
                newProc1.WaitForExit();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void hidePnlMainMenu()
        {
            pnlMainMenu.Visible = false;
        }

        public void setToolBarControl()
        {
            //toolBar1.Buttons.Clear();
            int cnt = 0;
            timer2.Enabled = false;
            int row = 0, MaxWidth = 0;
            bool[] arrFlag = new bool[toolBar1.Buttons.Count];

            foreach (ToolBarButton tbl in toolBar1.Buttons)
            {
                cnt = 0; if (tbl.Rectangle.Width > MaxWidth) MaxWidth = tbl.Rectangle.Width;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == tbl.Name)
                    {
                        cnt = 1;
                        break;
                    }
                }
                if (cnt == 0)
                    arrFlag[row] = true;
                else
                    arrFlag[row] = false;

                row++;
            }

            for (int i = 0; i < arrFlag.Length; i++)
            {
                if (arrFlag[i] == true)
                {
                    if (toolBar1.Buttons.Count > i)
                    {
                        toolBar1.Buttons.RemoveAt(i);
                        if (toolBar1.Buttons.Count == 0)
                        {
                            CloseFlag = false;
                            lblDispTaskbar.Visible = true;
                        }
                        else
                            lblDispTaskbar.Visible = false;
                    }
                }
            }
            if (toolBar1.Buttons.Count == 0) { pnlActivate.Visible = false; lblMainTitle.Text = ""; pnlMainMenu.Visible = true; }
        }

        public void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            try
            {
                if (pnlMainMenu.Visible == true) pnlMainMenu.Visible = false;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == e.Button.Name)
                    {
                        frm.Focus();
                        if (frm.WindowState == FormWindowState.Minimized)
                            frm.WindowState = FormWindowState.Maximized;
                        lblMainTitle.Text = e.Button.Text;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetHelpInfo()
        {
            string str = "Close Form: Esc.";
            str += "\nMenu Open: Ctrl + A";
            lblInfo.Text = str;
        }

        #region KeyDown Events
        public void KeyDownFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyDown += new KeyEventHandler(CotrolKeyDown);
                if (ctrl is Panel)
                    KeyDownFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyDownFormat(ctrl.Controls);
            }
        }


        private void CotrolKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.H && e.Alt && e.Control)
            {
                pnlInfo.Visible = true;
                pnlInfo.Dock = DockStyle.Fill;
                SetHelpInfo();
            }
            else if (e.KeyCode == Keys.X && e.Alt && e.Control)
            {
                pnlInfo.Visible = false;
            }
            else if (e.Alt && e.Control)
            {
                if (DBGetVal.UserID == 1)
                {
                    if (menuStrip.Items.Count == 7)
                        SetHiddenMenus();
                    else
                        menuStrip.Items.RemoveAt(menuStrip.Items.Count - 1);
                }
            }
            else if (e.KeyCode == Keys.H && e.Control)
            {
                About frm = new About();
                frm.ShowDialog();
            }
            else if (e.KeyCode == Keys.D && e.Control)
            {
                Vouchers.DeleteVoucherEntry frm = new Vouchers.DeleteVoucherEntry();
                ObjFunction.OpenForm(frm);
            }
            else if (e.KeyCode == Keys.M && e.Control)
            {
                menuStrip.Visible = !menuStrip.Visible;
            }
            else if (e.KeyCode == Keys.F1)
                Button_Click(this.btnBilling, new EventArgs());
            else if (e.KeyCode == Keys.F2)
                Button_Click(this.btnItemChangeRate, new EventArgs());
            else if (e.KeyCode == Keys.F3)
                Button_Click(this.btnSalesReturn, new EventArgs());
            else if (e.KeyCode == Keys.F4)
                Button_Click(this.btnPurchase, new EventArgs());
            else if (e.KeyCode == Keys.F5)
                Button_Click(this.btnPurchaseReturn, new EventArgs());
            else if (e.KeyCode == Keys.F6)
                Button_Click(this.btnSaleSummary, new EventArgs());
            else if (e.KeyCode == Keys.F7)
                Button_Click(this.btnWeeklySales, new EventArgs());
            else if (e.KeyCode == Keys.F8)
                Button_Click(this.btnSalesRegister, new EventArgs());
            else if (e.KeyCode == Keys.F9)
                Button_Click(this.btnTopItemSales, new EventArgs());
            else if (e.KeyCode == Keys.F10)
                Button_Click(this.btnTopBrandSales, new EventArgs());
            else if (e.KeyCode == Keys.F11)
                btnExit_Click(sender, new EventArgs());

            else if (e.KeyCode == Keys.F12 && e.Control)
                Button_Click(this.btnChartDashboard, new EventArgs());
            else if (e.KeyCode == Keys.F12)
                Button_Click(this.btnEOD, new EventArgs());
            else if (e.KeyCode == Keys.Space && e.Control)
            {
                if (SpaceFlag == false)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.Location = new Point(0, 0);
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                    SpaceFlag = true;
                    lblDispTaskbar.Text = "Hide Taskbar (Ctrl + Space)";
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                    SpaceFlag = false;
                    lblDispTaskbar.Text = "Show Taskbar (Ctrl + Space)";
                }
            }
            strPsw += (char)e.KeyValue;
            if (strPsw.ToLower() == "logicall")
            {
                if (DBGetVal.KachhaFirm == false)
                {
                    strPsw = "";
                    DBGetVal.KachhaFirm = true;
                    lblCompName.BackColor = Color.Magenta;

                }
                else if (DBGetVal.KachhaFirm == true)
                {
                    strPsw = "";
                    DBGetVal.KachhaFirm = false;
                    lblCompName.BackColor = Color.FromArgb(255, 93, 173, 226);//Equals={Color [A=255, R=93, G=173, B=226]}
                }
                strPsw = "";
            }
        }

        #endregion

        private void timer2_Tick(object sender, EventArgs e)
        {
            setToolBarControl();
        }

        #region HiddenMenu
        public void SetHiddenMenus()
        {
            DataTable dtTree = ObjFunction.GetDataView("SELECT Hidden FROM MUserMenuMaster where FKUserId =" + DBGetVal.UserID + "").Table;
            strNode = new string[1];
            for (int i = 0; i < 1; i++)
            {
                strNode[i] = secure.psDecrypt(dtTree.Rows[0].ItemArray[i].ToString());
            }
            cntRow = 0; cntCol = 0;

            DisplayHiddenMenu(new ToolStripMenuItem(), 0);
        }

        public ToolStripMenuItem DisplayHiddenMenu(ToolStripMenuItem TD, long Srno)
        {
            ToolStripMenuItem tdtemp;
            DataTable dtTree = dbUser.GetNodesByNodeID(Srno).Table;// ObjFunction.GetDataView("Select SrNo,MenuID,MenuName From MMenuMaster Where ControlMenu=" + Srno + "").Table;
            if (dtTree.Rows.Count > 0)
            {

                for (int i = 0; i < dtTree.Rows.Count; i++)
                {
                    if (dtTree.Rows[i].ItemArray[0].ToString() == "8")
                    {
                        tdtemp = new ToolStripMenuItem(dtTree.Rows[i].ItemArray[2].ToString());
                        tdtemp.Click += new EventHandler(ClickMenus);
                        tdtemp.Name = dtTree.Rows[i].ItemArray[0].ToString();
                        if (Srno == 0)
                        {
                            cntCol = 0;
                            if (cntCol < strNode[cntRow].Length)
                                if (strNode[cntRow][cntCol].ToString() == "1") tdtemp.Checked = true;
                            cntCol++;
                        }
                        else
                        {
                            if (cntCol < strNode[cntRow].Length)
                                if (strNode[cntRow][cntCol].ToString() == "1") tdtemp.Checked = true;
                            cntCol++;
                        }

                        tdtemp = DisplayHiddenMenu(tdtemp, Convert.ToInt64(dtTree.Rows[i].ItemArray[0].ToString()));

                        if (dtTree.Rows[i].ItemArray[3].ToString() != "") setShortKey(tdtemp, dtTree.Rows[i].ItemArray[3].ToString());

                        if (Srno == 0)
                        {
                            if (tdtemp.Checked == true)
                            {
                                tdtemp.Checked = false;
                                tdtemp.ForeColor = Color.White;
                                tdtemp.MouseHover += new EventHandler(tdtemp_MouseHover);
                                tdtemp.MouseLeave += new EventHandler(tdtemp_MouseLeave);
                                menuStrip.Items.Add(tdtemp);
                            }
                            cntRow++;
                        }
                        else
                        {
                            if (tdtemp.Checked == true)
                            {
                                tdtemp.Checked = false;
                                TD.DropDownItems.Add(tdtemp);
                            }
                        }
                    }
                }

            }
            return TD;
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //if (this.WindowState != FormWindowState.Minimized && this.WindowState != FormWindowState.Maximized)
            //{
            Rectangle rect = ((Panel)sender).ClientRectangle;
            if (rect.Height != 0 && rect.Width != 0)
            {
                LinearGradientBrush brush = new LinearGradientBrush(rect, Color.WhiteSmoke, Color.Gainsboro, 75);
                e.Graphics.FillRectangle(brush, rect);
            }
            // }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                if (btn.Enabled == false) return;
                pnlMainMenu.Visible = false;
                if (btn.Name == "btnBilling")
                {
                    //  if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise)) == false)
                    {
                        bool MenuFlag = false;
                        ObjFunction.GetToolStripItems(DBGetVal.MainForm.menuStrip.Items, 33, out MenuFlag);
                        if (MenuFlag == true)
                        {
                            //if (ObjFunction.CheckAllowMenu(33) == true)
                            //{
                            Form frmChild = new Vouchers.SalesBarcodeAE();
                            ObjFunction.OpenForm(frmChild, this);
                        }
                        else if (ObjFunction.CheckAllowMenu(172) == true)
                        {
                            Form frmChild = new Vouchers.SalesAutoAE();
                            ObjFunction.OpenForm(frmChild, this);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (btn.Name == "btnItemChangeRate")
                {
                    if (ObjFunction.CheckAllowMenu(38) == false) return;
                    Form frmChild = new Utilities.ItemRateChange();
                    ObjFunction.OpenForm(frmChild, this);
                }
                else if (btn.Name == "btnSalesReturn")
                {
                    if (ObjFunction.CheckAllowMenu(69) == false) return;
                    Form frmChild = new Vouchers.SalesReturnAE();
                    ObjFunction.OpenForm(frmChild, this);
                    //OMMessageBox.Show("!!!!!  Work Processing  !!!!!", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information); 
                }
                else if (btn.Name == "btnPurchase")
                {
                    if (ObjFunction.CheckAllowMenu(34) == false) return;
                    Form frmChild = new Vouchers.PurchaseAE();
                    ObjFunction.OpenForm(frmChild, this);
                }
                else if (btn.Name == "btnPurchaseReturn")
                {
                    if (ObjFunction.CheckAllowMenu(35) == false) return;
                    Form frmChild = new Vouchers.PurchaseReturnAE();
                    ObjFunction.OpenForm(frmChild, this);
                }
                else if (btn.Name == "btnTopItemSales")
                {
                    if (ObjFunction.CheckAllowMenu(144) == false) return;
                    //    string[] ReportSession;
                    //    ReportSession = new string[4];
                    //    ReportSession[0] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    //    ReportSession[1] = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    //    ReportSession[2] = "0";
                    //    ReportSession[3] = DBGetVal.FirmNo.ToString();


                    string[] ReportSession;
                    ReportSession = new string[24];
                    ReportSession[0] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[1] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[3] = "0";
                    ReportSession[4] = DBGetVal.FirmNo.ToString();
                    ReportSession[5] = "1";
                    ReportSession[6] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[7] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[8] = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[9] = "0";
                    ReportSession[10] = DBGetVal.FirmNo.ToString();
                    ReportSession[11] = "1";
                    ReportSession[12] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[13] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[14] = "0";//ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[15] = "0";
                    ReportSession[16] = DBGetVal.FirmNo.ToString();
                    ReportSession[17] = "2";
                    ReportSession[18] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[19] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[20] = "0";//ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[21] = "0";
                    ReportSession[22] = DBGetVal.FirmNo.ToString();
                    ReportSession[23] = "2";



                    //if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RptTopBrandwiseItemwiseValue(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptTopBrandwiseItemwiseValue.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    //}
                    //else
                    //{
                    //    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //        childForm = ObjFunction.GetReportObject("Reports.RptTopSales");
                    //    else
                    //        childForm = ObjFunction.LoadReportObject("RptTopSales.rpt");

                    //    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    //    objRpt.PrintReport();
                    //}
                }
                else if (btn.Name == "btnTopBrandSales")
                {
                    if (ObjFunction.CheckAllowMenu(144) == false) return;
                    string[] ReportSession;
                    ReportSession = new string[24];
                    ReportSession[0] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[1] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[2] = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[3] = "1";
                    ReportSession[4] = DBGetVal.FirmNo.ToString();
                    ReportSession[5] = "1";
                    ReportSession[6] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[7] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[8] = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[9] = "1";
                    ReportSession[10] = DBGetVal.FirmNo.ToString();
                    ReportSession[11] = "1";
                    ReportSession[12] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[13] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[14] = "0";//ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[15] = "1";
                    ReportSession[16] = DBGetVal.FirmNo.ToString();
                    ReportSession[17] = "2";
                    ReportSession[18] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[19] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[20] = "0";//ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    ReportSession[21] = "1";
                    ReportSession[22] = DBGetVal.FirmNo.ToString();
                    ReportSession[23] = "2";



                    //if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    Form NewF = null;
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RptTopBrandwiseItemwiseValue(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptTopBrandwiseItemwiseValue.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);

                    //string[] ReportSession;
                    //ReportSession = new string[4];
                    //ReportSession[0] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    //ReportSession[1] = ObjFunction.GetAppSettings(AppSettings.O_TopSalesValue).ToString();
                    //ReportSession[2] = "1";
                    //ReportSession[3] = DBGetVal.FirmNo.ToString();

                    ////if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    ////{

                    //Form NewF = null;
                    //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //    NewF = new Display.ReportViewSource(new Reports.RptTopSales(), ReportSession);
                    //else
                    //    NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RptTopSales.rpt", CommonFunctions.ReportPath), ReportSession);
                    //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    ////}
                    ////else
                    ////{
                    ////    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    ////    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    ////        childForm = ObjFunction.GetReportObject("Reports.RptTopSales");
                    ////    else
                    ////        childForm = ObjFunction.LoadReportObject("RptTopSales.rpt");

                    ////    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    ////    objRpt.PrintReport();
                    ////}
                }
                else if (btn.Name == "btnSalesRegister")
                {
                    if (ObjFunction.CheckAllowMenu(69) == false) return;
                    string[] ReportSession;
                    Form NewF = null;

                    ReportSession = new string[6];
                    ReportSession[0] = VchType.Sales.ToString();
                    ReportSession[1] = DBGetVal.FirmNo.ToString();
                    ReportSession[2] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[3] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[4] = "1";
                    ReportSession[5] = "0";

                    //if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RPTSalesRegister(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesRegister.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    //}
                    //else
                    //{
                    //    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //        childForm = ObjFunction.GetReportObject("Reports.RPTSalesRegister");
                    //    else
                    //        childForm = ObjFunction.LoadReportObject("RPTSalesRegister.rpt");

                    //    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    //    objRpt.PrintReport();
                    //}
                }
                else if (btn.Name == "btnSaleSummary")
                {
                    if (ObjFunction.CheckAllowMenu(69) == false) return;
                    string[] ReportSession;
                    Form NewF = null;

                    ReportSession = new string[7];
                    ReportSession[0] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[1] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[2] = VchType.Sales.ToString();
                    ReportSession[3] = "0";
                    ReportSession[4] = DBGetVal.FirmNo.ToString();
                    ReportSession[5] = ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VoucherTypeCode=" + VchType.Sales + " AND VoucherDate='" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' ANd IsCancel='false'", CommonFunctions.ConStr).ToString();
                    ReportSession[6] = ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VoucherTypeCode=" + VchType.RejectionIn + " AND VoucherDate='" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' ANd IsCancel='false'", CommonFunctions.ConStr).ToString();
                    //if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RPTSalesSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    //}
                    //else
                    //{
                    //    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //        childForm = ObjFunction.GetReportObject("Reports.RPTSalesSummary");
                    //    else
                    //        childForm = ObjFunction.LoadReportObject("RPTSalesSummary.rpt");

                    //    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    //    objRpt.PrintReport();
                    //}
                }
                else if (btn.Name == "btnWeeklySales")
                {
                    if (ObjFunction.CheckAllowMenu(69) == false) return;
                    string[] ReportSession;
                    Form NewF = null;

                    ReportSession = new string[7];
                    ReportSession[0] = ObjFunction.GetStartWeekDays(DBGetVal.ServerTime).ToString("dd-MMM-yyyy");
                    ReportSession[1] = DBGetVal.ServerTime.ToString("dd-MMM-yyyy");
                    ReportSession[2] = VchType.Sales.ToString();
                    ReportSession[3] = "0";
                    ReportSession[4] = DBGetVal.FirmNo.ToString();
                    ReportSession[5] = ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VoucherTypeCode=" + VchType.Sales + " AND VoucherDate>='" + ObjFunction.GetStartWeekDays(DBGetVal.ServerTime).ToString("dd-MMM-yyyy") + "' AND VoucherDate<='" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' ANd IsCancel='false'", CommonFunctions.ConStr).ToString();
                    ReportSession[6] = ObjQry.ReturnLong("Select Count(*) From TVoucherEntry Where VoucherTypeCode=" + VchType.RejectionIn + " AND VoucherDate>='" + ObjFunction.GetStartWeekDays(DBGetVal.ServerTime).ToString("dd-MMM-yyyy") + "' AND VoucherDate<='" + DBGetVal.ServerTime.ToString(Format.DDMMMYYYY) + "' ANd IsCancel='false'", CommonFunctions.ConStr).ToString();
                    //if (OMMessageBox.Show("Do you want to Preview the report", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                        NewF = new Display.ReportViewSource(new Reports.RPTSalesSummary(), ReportSession);
                    else
                        NewF = new Display.ReportViewSource(ObjFunction.LoadReportObject("RPTSalesSummary.rpt", CommonFunctions.ReportPath), ReportSession);
                    ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    //}
                    //else
                    //{
                    //    CrystalDecisions.CrystalReports.Engine.ReportDocument childForm;
                    //    if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.ReportDisplay)) == true)
                    //        childForm = ObjFunction.GetReportObject("Reports.RPTSalesSummary");
                    //    else
                    //        childForm = ObjFunction.LoadReportObject("RPTSalesSummary.rpt");

                    //    DBReportGenerate objRpt = new DBReportGenerate(childForm, ReportSession);
                    //    objRpt.PrintReport();
                    //}
                }
                else if (btn.Name == "btnEOD")
                {
                    if (ObjFunction.CheckAllowMenu(157) == false) return;
                    Form frmChild = new Master.EndDay();
                    ObjFunction.OpenForm(frmChild, this);
                }
                else if (btn.Name == "btnChartDashboard")
                {
                    if (ObjFunction.CheckAllowMenu(365) == false) return;
                    Form frmChild = new Display.ChartDashBoard();
                    //Form frmChild = new Vouchers.DeleteVoucherEntry();
                    ObjFunction.OpenForm(frmChild, this);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            DBAssemblyInfo dbAss = new DBAssemblyInfo();
            if (CloseFlag == false)
            {
                CloseFlag = true;
                if (OMMessageBox.Show("Do You Want To Quit ? ", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //DBAutoBackup dbAuto = new DBAutoBackup();
                    //dbAuto.ExportBackUp();
                    Application.Exit();
                }
                else
                {

                    CloseFlag = false;
                }
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.WindowState = FormWindowState.Minimized;
            }
            pnlMainMenu.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form NewForm = new Utilities.StockUpdateUtilities();
            ObjFunction.OpenForm(NewForm, this);
        }

        #region Upload Data
        private void TransferData(int ZipType)
        {
            timer1.Enabled = false;//Deepak
            try
            {//============umesh
             //bool flag = true;
             //if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.AutoUploadData)) == true)
             //{
             //    if (System.IO.File.Exists(Application.StartupPath + "\\RetailerAuto.exe") == true)
             //    {
             //        foreach (System.Diagnostics.Process clsProcess in System.Diagnostics.Process.GetProcesses())
             //        {
             //            if (clsProcess.ProcessName.ToString() == "RetailerAuto")
             //            {
             //                flag = false;
             //            }
             //        }
             //        if (flag == true)
             //        {
             //            try
             //            {
             //                System.Diagnostics.ProcessStartInfo pr = new System.Diagnostics.ProcessStartInfo(Application.StartupPath + "\\RetailerAuto.exe", CommonFunctions.ConStr.Replace(" ", "") + " " + CommonFunctions.ConStrServer.Replace(" ", "") + " " + ZipType);
             //                pr.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
             //                System.Diagnostics.Process newProc1 = null;
             //                newProc1 = System.Diagnostics.Process.Start(pr);
             //            }
             //            catch (Exception e)
             //            {
             //                System.IO.StreamWriter sw = new System.IO.StreamWriter(Application.StartupPath + "\\Log.txt", true);
             //                sw.WriteLine(e.Message);
             //                sw.Close();
             //            }
             //        }
             //    }
             //}
            }
            catch (Exception e)
            {
                CommonFunctions.ErrorMessge = e.Message;
            }
            timer1.Enabled = true;//Deepak
                                  //Thread thread = new Thread(new ThreadStart(run));
                                  //thread.IsBackground = true;
                                  //thread.Start();
        }

        #endregion

        public void SetStockGroup()
        {
            try
            {
                if (ObjQry.ReturnInteger("Select ControlGroup FRom MStockGroup Where StockGroupNo=8", CommonFunctions.ConStr) == 1)
                {
                    ObjTrans.Execute("Update MStockGroup set ControlGroup=2 , ControlSubGroup=10 Where StockGroupNo=8 ", CommonFunctions.ConStr);
                    ObjTrans.Execute("Update MStockGroup set ControlGroup=3 Where StockGroupNo=9", CommonFunctions.ConStr);
                    ObjTrans.Execute("Update MStockGroup set ControlGroup=4 Where StockGroupNo=10", CommonFunctions.ConStr);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void DisableMainControl(long MenuNo)
        {
            if (MenuNo == 41)
                btnBilling.Enabled = true;
            else if (MenuNo == 115)
                btnItemChangeRate.Enabled = true;
            else if (MenuNo == 151)
                btnSalesReturn.Enabled = true;
            else if (MenuNo == 3)
                btnPurchase.Enabled = true;
            else if (MenuNo == 44)
                btnPurchaseReturn.Enabled = true;
            else if (MenuNo == 157)
                btnEOD.Enabled = true;
        }

        public void chekbtn()
        {
            btnAlert1.Text = "";
            btnAlert2.Text = "";
            btnAlert3.Text = "";
            btnAlert4.Text = "";
            btnAlert5.Text = "";
            btnAlert6.Text = "";
            btnAlert7.Text = "";
            btnAlert1.Visible = false;
            btnAlert2.Visible = false;
            btnAlert3.Visible = false;
            btnAlert4.Visible = false;
            btnAlert5.Visible = false;
            btnAlert6.Visible = false;
            btnAlert7.Visible = false;

        }

        public void AlertNotification()
        {
            try
            {
                chekbtn();
                DataTable dtAlert = ObjFunction.GetDataView("Select * from MAlertNotification where ViewMsg<>'.'or ViewMsg is Null order by PkSrNo", CommonFunctions.ConStr).Table;
                if (dtAlert.Rows.Count > 0)
                {
                    pnlAlert.Visible = true;
                }
                int j = 1;
                for (int i = 0; i < dtAlert.Rows.Count; i++)
                {
                    if (i > 7)
                        break;
                    switch (j)
                    {
                        case 1:

                            btnAlert1.BackColor = Color.FromName(dtAlert.Rows[i].ItemArray[3].ToString());
                            btnAlert1.Text = dtAlert.Rows[i].ItemArray[2].ToString();
                            btnAlert1.Visible = true;
                            j++;
                            break;
                        case 2:
                            btnAlert2.BackColor = Color.FromName(dtAlert.Rows[i].ItemArray[3].ToString());
                            btnAlert2.Text = dtAlert.Rows[i].ItemArray[2].ToString();
                            btnAlert2.Visible = true;
                            j++;
                            break;
                        case 3:
                            btnAlert3.BackColor = Color.FromName(dtAlert.Rows[i].ItemArray[3].ToString());
                            btnAlert3.Text = dtAlert.Rows[i].ItemArray[2].ToString();
                            btnAlert3.Visible = true;
                            j++;
                            break;
                        case 4:
                            btnAlert4.BackColor = Color.FromName(dtAlert.Rows[i].ItemArray[3].ToString());
                            btnAlert4.Text = dtAlert.Rows[i].ItemArray[2].ToString();
                            btnAlert4.Visible = true;
                            j++;
                            break;
                        case 5:
                            btnAlert5.BackColor = Color.FromName(dtAlert.Rows[i].ItemArray[3].ToString());
                            btnAlert5.Text = dtAlert.Rows[i].ItemArray[2].ToString();
                            btnAlert5.Visible = true;
                            j++;
                            break;
                        case 6:
                            btnAlert6.BackColor = Color.FromName(dtAlert.Rows[i].ItemArray[3].ToString());
                            btnAlert6.Text = dtAlert.Rows[i].ItemArray[2].ToString();
                            btnAlert6.Visible = true;
                            j++;
                            break;
                        case 7:
                            btnAlert7.BackColor = Color.FromName(dtAlert.Rows[i].ItemArray[3].ToString());
                            btnAlert7.Text = dtAlert.Rows[i].ItemArray[2].ToString();
                            btnAlert7.Visible = true;
                            j++;
                            break;
                    }
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void btnRetailerTools_Click(object sender, EventArgs e)
        {
            try
            {
                string SQLPath = Application.StartupPath + "\\Tools\\RetailerTools.exe";
                System.Diagnostics.ProcessStartInfo pr = new System.Diagnostics.ProcessStartInfo(SQLPath);
                pr.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                pr.UseShellExecute = true;
                pr.Verb = "runas";
                pr.Arguments = "/user:Administrator cmd /K " + SQLPath;
                System.Diagnostics.Process newProc1 = null;
                newProc1 = System.Diagnostics.Process.Start(pr);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay("RetailerHardware Tools system doesn't exist..." + Environment.NewLine + exc.Message);
            }
        }

        #region Notify Message Related Methods
        public void CheckNotify()
        {
            try
            {
                if (ObjQry.ReturnLong("Select Count(*) From MNotification Where NotifyStatus=1", CommonFunctions.ConStr) > 0)
                    btnNotify.Visible = true;
                else
                {
                    btnNotify.Visible = false;
                    pnlNotify.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNotify_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnlNotify.Visible == false)
                {
                    BindNotifyGrid();
                    Application.DoEvents();
                    pnlNotify.BackColor = lblInfo.BackColor;
                    pnlNotify.Location = new Point(lblCompName.Width - pnlNotify.Width, lblCompName.Location.Y + lblCompName.Height);
                    OMNativeMethods.AnimateWindow(pnlNotify.Handle, 500, OMNativeMethods.AW_ACTIVATE | OMNativeMethods.AW_HOR_NEGATIVE | OMNativeMethods.AW_SLIDE);
                    pnlNotify.Visible = true;
                }
                else
                    pnlNotify.Visible = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void BindNotifyGrid()
        {
            dgNotify.Rows.Clear();
            string strQuery = "Select NotifyMessage,'OK' AS Status,NotifyNo,NotifyType,NotifyFileName From MNotification Where NotifyStatus=1";
            DataTable dtNotify = ObjFunction.GetDataView(strQuery).Table;
            for (int i = 0; i < dtNotify.Rows.Count; i++)
            {
                dgNotify.Rows.Add();
                for (int col = 0; col < dtNotify.Columns.Count; col++)
                {
                    dgNotify.Rows[i].Cells[col].Value = dtNotify.Rows[i].ItemArray[col].ToString();
                }
            }
            if (dgNotify.Rows.Count > 0)
            {
                dgNotify.CurrentCell = dgNotify[0, 0];
            }
        }

        private void dgNotify_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    DBMNotification dbNotify = new DBMNotification();
                    MNotification mNotify = new MNotification();
                    if (dgNotify.Rows[e.RowIndex].Cells[3].Value.ToString() != "1")
                    {
                        mNotify.NotifyNo = Convert.ToInt64(dgNotify.Rows[e.RowIndex].Cells[2].Value);
                        mNotify.NotifyStatus = 2;
                        dbNotify.UpdateNotification(mNotify);
                        dbNotify.ExecuteNonQueryStatements();
                    }
                    else if (dgNotify.Rows[e.RowIndex].Cells[3].Value.ToString() == "1")
                    {
                        if (AttachDB() == true)
                        {
                            SqlConnection.ClearAllPools();
                            if (ObjQry.ReturnLong("Select Count(*) From MLedger Where IMStatus=0", CommonFunctions.ConStrTools) == 0)
                            {
                                mNotify.NotifyNo = Convert.ToInt64(dgNotify.Rows[e.RowIndex].Cells[2].Value);
                                mNotify.NotifyStatus = 2;
                                dbNotify.UpdateNotification(mNotify);
                                dbNotify.ExecuteNonQueryStatements();
                            }
                            OpenToolsEXE();
                        }
                        else
                        {
                            OMMessageBox.Show("RetailerHardware Tools system DB doesn't exist...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }

                    try
                    {
                        if (dgNotify.Rows[e.RowIndex].Cells[3].Value.ToString() == "3" || dgNotify.Rows[e.RowIndex].Cells[3].Value.ToString() == "4")
                        {
                            string strDownloaded = ObjFunction.GetAppSettings(AppSettings.O_UpDownLoadPath) + "\\RetailerFile\\Downloaded";
                            string strViewRead = ObjFunction.GetAppSettings(AppSettings.O_UpDownLoadPath) + "\\RetailerFile\\ViewRead";
                            string strFlName = dgNotify.Rows[e.RowIndex].Cells[4].Value.ToString();
                            System.IO.File.Copy(strDownloaded + "\\" + strFlName, strViewRead + "\\" + strFlName);
                            System.IO.File.Delete(strDownloaded + "\\" + strFlName);
                            System.Diagnostics.Process.Start(strViewRead + "\\" + strFlName);
                        }
                    }
                    catch { }
                    BindNotifyGrid();
                    if (dgNotify.Rows.Count == 0)
                    {
                        btnNotify.Visible = false;
                        pnlNotify.Visible = false;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        #endregion

        public void OpenToolsEXE()
        {
            try
            {
                string SQLPath = Application.StartupPath + "\\Tools\\RetailerTools.exe";
                System.Diagnostics.ProcessStartInfo pr = new System.Diagnostics.ProcessStartInfo(SQLPath);
                pr.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                pr.UseShellExecute = true;
                pr.Verb = "runas";
                pr.Arguments = "/user:Administrator cmd /K " + SQLPath;
                System.Diagnostics.Process newProc1 = null;
                newProc1 = System.Diagnostics.Process.Start(pr);
            }
            catch (Exception exc)
            {
                CommonFunctions.ErrorMessge = exc.Message;
                OMMessageBox.Show("RetailerHardware Tools system doesn't exist...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
            }
        }

        public bool AttachDB()
        {
            string strDatabase = "RetailerTools0001";
            string DBFilePath = System.Windows.Forms.Application.StartupPath + "\\Tools";
            string DBFileName = System.Windows.Forms.Application.StartupPath + "\\Tools\\" + strDatabase + ".mdf";

            bool flag = false;
            SqlConnection con = new SqlConnection();
            try
            {
                con.ConnectionString = "Server=.\\SqlEXPRESS;Initial Catalog=master;Integrated security=true;";
                con.Open();
                if (ObjQry.ReturnLong("select count(*) from sys.databases where name = '" + strDatabase + "'", con.ConnectionString) == 0)
                {
                    if (System.IO.File.Exists(DBFileName) == false) return false;
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = @"sp_attach_db '" + strDatabase + "','" + DBFilePath + @"\" + strDatabase + ".mdf','" + DBFilePath + @"\" + strDatabase + "_log.ldf'";
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                }
                //ConStrLocal = "Data Source=.\\SqlEXPRESS;Initial Catalog=" + strDatabase + ";Integrated security=true;";
                flag = true;
            }
            catch (Exception e1)
            {
                flag = false;
                CommonFunctions.ErrorMessge = e1.Message;
            }
            finally
            {
                con.Close();
            }
            return flag;
        }

        private void TSPendingOrders_Click(object sender, EventArgs e)
        {
            if (ObjFunction.CheckAllowMenu(378) == false) return;
            pnlMainMenu.Visible = false;
            Form frmChild = new Vouchers.SalesReturnAE();
            ObjFunction.OpenForm(frmChild, this);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Interval = 60;
            isSalesOrderAvailable = false;
            TSPendingOrders.Visible = false;
            timer4.Enabled = false;
            //timer3.Interval = 60000*5;//5 min 60000*5=========umesh
            //int i = ObjQry.ReturnInteger("Select Count(PkOtherVoucherNo) FROM TOtherVoucherEntry Where VoucherStatus in (9,10)", CommonFunctions.ConStr);//
            //if (i > 0)
            //{
            //    TSPendingOrders.Text = "Total " + i + " order(s) pending";
            //    TSPendingOrders.Visible = true;
            //    isSalesOrderAvailable = true;
            //    timer4.Enabled = true;
            //}
            //else
            //{
            //isSalesOrderAvailable = false;
            //TSPendingOrders.Visible = false;
            //timer4.Enabled = false;
            //}
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (isSalesOrderAvailable)
            {
                if (clrNotifyBlinking == Color.Red)
                {
                    clrNotifyBlinking = Color.Blue;
                }
                else
                {
                    clrNotifyBlinking = Color.Red;
                }
            }

            if (isSalesOrderAvailable)
            {
                TSPendingOrders.ForeColor = clrNotifyBlinking;
            }


        }

        private void MDIParent1_Click(object sender, EventArgs e)
        {
            strPsw = "";
        }

        private void pnlMainMenu_Click(object sender, EventArgs e)
        {
            strPsw = "";
        }

    }
}
