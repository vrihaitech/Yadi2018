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
    public partial class PasswordAsk : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Security secure = new Security();

        public static long UserID = 0;
        public static long CreditID = 0;
        public static bool IsAdmin = false;
        public static long IsAllow = 0;
        public static long IsDeleteAllow = 0;
        int PType = 0;

        public PasswordAsk()
        {
            PType = 0;
            IsAllow = 0;
            IsDeleteAllow = 0;
            CreditID = 0;
            InitializeComponent();
        }
        public PasswordAsk(int pwdtype)
        {
            PType = pwdtype;
            IsAllow = 0;
            CreditID = 0;
            IsDeleteAllow = 0;
            InitializeComponent();
        }

        private void PasswordAsk_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
            txtPassword.Font = ObjFunction.GetFont(FontStyle.Regular, 24);
        }


        private void txtPrintCount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (PType == 0)
                {
                    UserID = ObjQry.ReturnLong("Select UserCode From MUser Where Password='" + secure.psEncrypt(txtPassword.Text) + "'", CommonFunctions.ConStr);
                    if (UserID == 0)
                    {
                        OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                        IsAdmin = false;
                    }
                    else
                    {
                        IsAdmin = ObjQry.ReturnInteger("Select UserType from Muser where UserCode=" + UserID + " ", CommonFunctions.ConStr) == 1 ? true : false;
                        this.Close();
                    }
                }
                else if (PType == 1)
                {
                    if (secure.psEncrypt(txtPassword.Text) != ObjFunction.GetAppSettings(AppSettings.S_CreditBillPassword))
                    {
                        OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                    else
                    {
                        CreditID = 1;
                        this.Close();
                    }
                }
                else if (PType == 2)
                {
                    if (secure.psEncrypt(txtPassword.Text) != ObjFunction.GetAppSettings(AppSettings.S_BillUpdatePwd))
                    {
                        OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                    else
                    {
                        IsAllow = 1;
                        this.Close();
                    }
                }
                else if (PType == 3)
                {
                    if (secure.psEncrypt(txtPassword.Text) != ObjFunction.GetAppSettings(AppSettings.S_BillDeletePwd))
                    {
                        OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                    else
                    {
                        IsDeleteAllow = 1;
                        this.Close();
                    }
                }
                else if (PType == 4)
                {
                    if (secure.psEncrypt(txtPassword.Text) != ObjFunction.GetAppSettings(AppSettings.P_CreditBillPassword))
                    {
                        OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                    else
                    {
                        CreditID = 1;
                        this.Close();
                    }
                }
                else if (PType == 5)
                {
                    if (secure.psEncrypt(txtPassword.Text) != ObjFunction.GetAppSettings(AppSettings.P_BillUpdatePwd))
                    {
                        OMMessageBox.Show("Please enter valid password", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPassword.Text = "";
                        txtPassword.Focus();
                    }
                    else
                    {
                        IsAllow = 1;
                        this.Close();
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UserID = 0;
            CreditID = 0;
            this.Close();
        }
    }
}
