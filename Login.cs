using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using OM;
using OMControls;


namespace Yadi
{
    public partial class Login : Form
    {
        OMCommonClass cc = new OMCommonClass();
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Security secure = new Security();
        public int RegType;
    
        public Login()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ObjFunction.FormatControl(this.Controls);
            ObjFunction.KeyPressFormat(this.Controls);
            txtUserName.Focus();
            if (ObjFunction.SetConnection("Master") == false)
                Application.Exit();
            else
            {
                try
                {
                    //CommonFunctions.ServerName = "192.168.1.12\\SQLEXPRESS";
                    // CommonFunctions.ServerName = "2159-PC\\SQLEXPRESS";
                    // CommonFunctions.ServerName = "COMP18-PC\\SQLEXPRESS";
                    //  CommonFunctions.ServerName = "COMP1-PC\\SQLEXPRESS";
                    //CommonFunctions.ServerName = "SAMSANGSIR-PC\\SQLEXPRESS";
                    // CommonFunctions.DatabaseName = "karlon";
                    //  CommonFunctions.DatabaseName = "JantaFromOffice";
                    //    CommonFunctions.DatabaseName = "Yadi2018Kurlon";
                    //CommonFunctions.DatabaseName = "Yadi2018SOHAM";
                    //   CommonFunctions.DatabaseName = "Blank Database";
                    //  CommonFunctions.DatabaseName = "Yadi2018Jeetendra";
                    //CommonFunctions.DatabaseName = "Yadi2018Sonifootwear";
                    // CommonFunctions.DatabaseName = "YadiPlusShivshankar";
                    // CommonFunctions.DatabaseName = "YadiPlus2018Jitendra";
                    //CommonFunctions.DatabaseName = "Yadi2018Vaibhav";
                    // CommonFunctions.DatabaseName = "Yadi2018";
                    //CommonFunctions.DatabaseName = "Yadi2108Bhatiya";
                    CommonFunctions.DatabaseName = "YadiPlusJanata";
                    //CommonFunctions.DatabaseName = "MarkYadi2018";
                    // CommonFunctions.DatabaseName = "Yadi2018Parag";
                    //CommonFunctions.DatabaseName = "YadiKomalBhai";
                    //CommonFunctions.DatabaseName = "mark";
                    // CommonFunctions.DatabaseName = "NewYadiTest";

                    //CommonFunctions.ConStr = "Data Source=sharedmssql3.znetindia.net,1234;Initial Catalog=Yadi2018Apte;User ID=Trend2;Password=estofa@123";
                    CommonFunctions.ConStr = "Data Source=" + CommonFunctions.ServerName + ";Initial Catalog=" + CommonFunctions.DatabaseName + ";User ID=Logicall;Password=Logicall";
                    // DBGetVal.DBName = ObjFunction.GetDatabaseName(CommonFunctions.ConStr);
                   // CommonFunctions.ConStr = "Data Source=" + CommonFunctions.ServerName + ";Initial Catalog=" + CommonFunctions.DatabaseName + ";User ID=rahpri2830;Password=rahpri2830";

                    CommonFunctions.ConStrTools = @"Data Source=.\SQLEXPRESS;Initial Catalog=RetailerTools0001;Integrated security=true;";
                    //CBUser.DataSource = null;
                    DBGetVal.MacID = cc.GetMacName();
                    DBGetVal.MachineIPAddress = Dns.GetHostByName(Dns.GetHostName().ToString()).AddressList[0];
                    DBGetVal.HostName = Dns.GetHostName();
                    if (RegType == 1) InsertRegistration();
                    DBGetVal.MacNo = ObjQry.ReturnLong("Select RegNo From MRegistration Where MacID='" + secure.psEncrypt(DBGetVal.MacID) + "'", CommonFunctions.ConStr);

                    if (DBGetVal.MacNo == 0)
                    {
                        InsertRegistration();
                        DBGetVal.MacNo = ObjQry.ReturnLong("Select RegNo From MRegistration Where MacID='" + secure.psEncrypt(DBGetVal.MacID) + "'", CommonFunctions.ConStr);
                    }
                    ObjFunction.SetAppSettings();

                    ObjFunction.SetReportPath();
                    ObjFunction.GetTimeUserPasswords();
                    string strServer = ObjQry.ReturnString("Select DatabaseServer From MServerSettings", CommonFunctions.ConStr);
                    CommonFunctions.ConStrServer = (strServer != "") ? secure.psDecrypt(strServer) : "";

                    cc.WriteSecurityFile(DBGetVal.ServerTime, new DBAssemblyInfo().AssemblyTitle, OMCommonClass.InstallationType.None, 0, 0, 0, false);
                    if (cc.ReadSecurity(DateTime.Now, new DBAssemblyInfo().AssemblyTitle) == false)
                    {
                        cc.WriteLockFile(new DBAssemblyInfo().AssemblyTitle);
                        OMMessageBox.Show("Software is expired. Please Contact to System Administrator.", CommonFunctions.ErrorTitle);
                        Application.Exit();
                    }

                    string AppRegVer = ObjQry.ReturnString("Select AppVersion From MSetting", CommonFunctions.ConStr);
                    if (secure.psDecrypt(AppRegVer) != new DBAssemblyInfo().AssemblyVersion)
                    {
                        ObjFunction.ExecuteScript(secure.psDecrypt(AppRegVer));
                    }
                    ObjFunction.CheckVersion();
 
                    DataTable dtFirm = new DataTable();
                    dtFirm = ObjFunction.GetDataView("Select FirmNo,FirmName,EmailID,EmailPass,BooksBeginFrom ,BooksEndOn,Address From MFirm").Table;
                    DBGetVal.FirmNo = Convert.ToInt32(dtFirm.Rows[0].ItemArray[0].ToString());
                    CommonFunctions.CompanyName = dtFirm.Rows[0].ItemArray[1].ToString();
                    DBGetVal.Email = dtFirm.Rows[0].ItemArray[2].ToString();
                    DBGetVal.EmailPass = dtFirm.Rows[0].ItemArray[3].ToString();
                    DBGetVal.CompanyAddress = dtFirm.Rows[0].ItemArray[6].ToString();
                    //CommonFunctions.CompanyName = ObjQry.ReturnString("Select FirmName From MFirm", CommonFunctions.ConStr);
                    if (CommonFunctions.CompanyName == "")
                    {
                        Application.Exit();
                    }
                    btnLogin.Focus();
                    //txtPass.Focus();
                    cnxtMenu.Items.Add(CommonFunctions.ConStr);
                    DBGetVal.ServerTime = CommonFunctions.GetTime();
                    DBGetVal.KachhaFirm = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_DirectEstimateBilling));
                    // DBGetVal.KachhaFirm = Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.S_PostFirmwise));
                    //txtUserName.Text = "admin";
                    //txtPass.Text = "1";
                }
                catch (Exception e1)
                {
                    OMMessageBox.Show(e1.Message);
                    Application.Exit();
                }
            }
        }

        public bool Valid()
        {
            bool flag = false;
            DBAssemblyInfo dbAss = new DBAssemblyInfo();
            if (txtUserName.Text == "")
            {
                OMMessageBox.Show("Please Enter User Name.", dbAss.AssemblyTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtUserName.Focus();
            }
            else if (txtPass.Text == "")
            {
                OMMessageBox.Show("Please Enter Password.", dbAss.AssemblyTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtPass.Focus();
            }
            else flag = true;


            return flag;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Valid() == true)
                {
                    DBGetVal.UserID = ObjQry.ReturnInteger("Select UserCode From Muser Where UsersUserCode='" + txtUserName.Text + "' AND IsClose=0", CommonFunctions.ConStr);
                    if (DBGetVal.UserID > 0)
                    {
                        DBGetVal.FirmNo = ObjQry.ReturnInteger("Select FirmNo From MFirm Order by FirmNo desc", CommonFunctions.ConStr);
                        if (CommonFunctions.MainPassword.ToString() == txtPass.Text)
                        {
                            DBGetVal.FirmName = CommonFunctions.CompanyName;

                            MDIParent1 NewF = new MDIParent1();
                            DBGetVal.MainForm = NewF;
                            DBGetVal.UserName = txtUserName.Text.ToUpper();
                            DBGetVal.IsAdmin = ObjQry.ReturnInteger("Select UserType from Muser where UserCode=" + DBGetVal.UserID + " ", CommonFunctions.ConStr) == 1 ? true : false;
                            NewF.TSUserName.Text = "User Name: " + DBGetVal.UserName;
                            NewF.Show();
                            txtUserName.Text = "";
                            txtPass.Text = "";
                            this.Hide();

                        }
                        else
                        {
                            string pass = ObjQry.ReturnString("Select Password from Muser where UserCode=" + DBGetVal.UserID + " ", CommonFunctions.ConStr);

                            if (txtPass.Text == secure.psDecrypt(pass))
                            {
                                DBGetVal.FirmName = CommonFunctions.CompanyName;

                                DBGetVal.IsAdmin = ObjQry.ReturnInteger("Select UserType from Muser where UserCode=" + DBGetVal.UserID + " ", CommonFunctions.ConStr) == 1 ? true : false;
                                DBGetVal.UserName = txtUserName.Text.ToUpper();
                                MDIParent1 NewF = new MDIParent1();
                                DBGetVal.MainForm = NewF;
                                NewF.TSUserName.Text = "User Name: " + DBGetVal.UserName;
                                NewF.Show();
                                txtUserName.Text = "";
                                txtPass.Text = "";
                                this.Hide();
                            }
                            else
                            {
                                OMMessageBox.Show("Password Incorrect", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                                txtPass.Focus();
                            }
                        }
                    }
                    else
                    {
                        OMMessageBox.Show("User Name is Incorrect", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtUserName.Focus();
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
            this.Close();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Login_Activated(object sender, EventArgs e)
        {
            txtUserName.Focus();
            //txtUserName.Text = "Admin";
            //txtPass.Text = "36";
        }

        private void Login_Shown(object sender, EventArgs e)
        {
            //txtUserName.Text = "Admin";
            //    txtPass.Text = "36";
            txtUserName.Focus();
        }

        public void InsertRegistration()
        {
            DBMRegistration dbReg = new DBMRegistration();
            MRegistration mReg = new MRegistration();
            MRegistrationDetails mRegDtls = new MRegistrationDetails();

            mReg.RegNo = ObjQry.ReturnLong("Select RegNo From MRegistration Where MacID='" + secure.psEncrypt(DBGetVal.MacID) + "'", CommonFunctions.ConStr);
            mReg.MacID = DBGetVal.MacID;
            mReg.MachineIP = DBGetVal.MachineIPAddress.ToString();
            mReg.HostName = DBGetVal.HostName;
            mReg.IsActive = true;
            mReg.IsManual = false;
            mReg.CompanyNo = DBGetVal.FirmNo;
            dbReg.AddMRegistration(mReg);

            mRegDtls.RegDtlsNo = 0;
            mRegDtls.RegDate = Convert.ToDateTime(DBGetVal.ServerTime.ToString("dd-MMM-yyyy"));
            mRegDtls.RegTime = DBGetVal.ServerTime;
            mRegDtls.CompanyNo = DBGetVal.FirmNo;
            dbReg.AddMRegistrationDetails(mRegDtls);

            dbReg.ExecuteNonQueryStatements();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPass.Focus();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.Focus();
                btnLogin_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void Login_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;

            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.WhiteSmoke, Color.Gainsboro, 75);

            e.Graphics.FillRectangle(brush, rect);

            Pen pen = new Pen(Color.Snow, 2);
            pen.Alignment = PenAlignment.Inset;
            e.Graphics.DrawRectangle(pen, rect);
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
