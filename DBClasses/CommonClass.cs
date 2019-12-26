using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using Yadi;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Net;
using System.Threading;
using OMControls;

namespace OM
{
    public class

        CommonFunctions : OMFunctions
    {
        private DataSet dset = new DataSet();
        public static long CompanyNo;
        private Transaction.GetDataSet objTrans = new Transaction.GetDataSet();
        private Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        private Transaction.Transactions objT = new Transaction.Transactions();

        public static string SystemName = "Yadi";//"RetailerHardware";
        public static string UserName = "", CompanyName = "", BrName = "", ServerName = "", DatabaseName = "";
        public static string ApplicationError, SessionExpired, ErrorMessge = "", strErrorMsg = "", ConStrServer, ConStr, ConStrTools, ConStrNew;
        public static long VoucherLock;
        public static string ErrorTitle = SystemName + " System";
        public static object MainPassword = "";
        // public bool KachhaFirm = false;

        private Security secure = new Security();
        public static string ReportPath, AboutInfo, SecureInfo;
        public static MDIParent1 mMain;
        DBAssemblyInfo dbAss = new DBAssemblyInfo();
        //public Control ctrlFocus = null; bool flagFocus = true;
        public static DataTable dtHelp = new DataTable();

        public static DataTable dtAppSettings;
        private static string LUserID, LPassword;
        public static string DBName = "Yadi2018";
        public static string DBFileOuputPath = "d: ";
        public static bool GItemFlag = false;
        public SqlDataReader GetData(string Sql)
        {
            SqlConnection con = new SqlConnection(ConStr);
            con.Open();
            SqlCommand cmd = new SqlCommand(Sql, con);

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public bool SetConnection(string dbName)
        {
            try
            {
                string sysDrive = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
                OMCommonClass cc = new OMCommonClass();
                //bool flag = false;
                string fname = sysDrive + "Windows\\System32\\" + dbAss.AssemblyTitle + " RegisteredSerial.dat";
                StreamReader objreader = new StreamReader(fname);

                string str = objreader.ReadLine(); SecureInfo = SecureInfo + "Mac ID:" + secure.psDecrypt(str) + "\n";
                str = objreader.ReadLine();
                MainPassword = secure.psDecrypt(str); SecureInfo = SecureInfo + "PWD:" + secure.psDecrypt(str) + "\n";
                str = objreader.ReadLine();
                ServerName = secure.psDecrypt(str); SecureInfo = SecureInfo + "Data Source Name:" + secure.psDecrypt(str);
                str = objreader.ReadLine();
                DatabaseName = secure.psDecrypt(str);
                str = objreader.ReadLine();

                objreader.Close();
                objreader = null;

                //Version Change Checking
                fname = sysDrive + "Windows\\System32\\" + dbAss.AssemblyTitle + " Registered.dat";
                objreader = new StreamReader(fname);
                str = objreader.ReadLine();
                str = objreader.ReadLine();
                str = objreader.ReadLine();
                str = objreader.ReadLine();
                if (str.IndexOf(dbAss.AssemblyVersion) == -1)
                {
                    str = objreader.ReadLine();
                    str = objreader.ReadLine();
                    str = objreader.ReadLine();
                    DateTime dt = Convert.ToDateTime(str.Substring(("Installation Date:").ToString().Length).Trim());
                    objreader.Close();
                    objreader = null;
                    OMCommonClass c1 = new OMCommonClass(true);
                    c1.setPath(dbAss.AssemblyTitle);
                    c1.WriteUpdateVersionFile(dt, dbAss.AssemblyTitle, dbAss.AssemblyProduct, dbAss.AssemblyVersion, dbAss.AssemblyCopyright, dbAss.AssemblyGUID);
                    string strMsg = "";
                    strMsg += "\n===========================================================";
                    strMsg += "\n                    System Information                     ";
                    strMsg += "\n===========================================================";
                    strMsg += "\nYadi System: " + Application.ProductVersion;
                    strMsg += "\nCompany Name:" + Application.CompanyName;

                    strMsg += "\nInstallation Date: \t\t" + dt;
                    strMsg += "\nModified Date: \t\t" + DateTime.Now;
                    strMsg += "\nAssembly Title: \t\t" + String.Format("About " + dbAss.AssemblyTitle);
                    strMsg += "\nAssembly Product: \t\t" + dbAss.AssemblyProduct;
                    strMsg += "\nAssembly Version: \t\t" + String.Format("Version {0}", dbAss.AssemblyVersion);
                    strMsg += "\nAssembly Copyright:\t" + dbAss.AssemblyCopyright;
                    strMsg += "\nAssembly GUID:\t\t" + dbAss.AssemblyGUID;
                    strMsg += "\n";
                    strMsg += "\nDeveloped By:\tUmesh Dhus(Sr. Software Developer)";
                    strMsg += "\nDeveloped By:\tShrriyans Shah(Project Manager)";
                    strMsg += "\n===========================================================";
                    strMsg += "\nThank you for using our software.";

                    OMMessageBox.Show(dbAss.AssemblyTitle + " Version is change. New Version system is more user freindly\n" + strMsg, CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                }
                else
                {
                    objreader.Close();
                    objreader = null;
                }

                //==============================

                AboutInfo = cc.GetAbountInfo(dbAss.AssemblyTitle);
                ConStr = "Data Source=" + ServerName + ";Initial Catalog=" + dbName + ";Integrated Security=true;Connection Timeout=10000;";
                return true;
            }
            catch (Exception ex)
            {
                OMMessageBox.Show("System Setting is not properly set.......", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                ErrorMessge = ex.Message;
                return false;
            }
        }

        public static string RegCompName()
        {
            DBAssemblyInfo dbAss = new DBAssemblyInfo();
            return "Powered By " + dbAss.AssemblyCompany;
        }

        public System.Data.SqlClient.SqlDataReader GetData(string Sql, string ConStr)
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConStr);
            con.Open();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(Sql, con);

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public DataView GetDataView(string sql)
        {
            System.Data.SqlClient.SqlConnection Con = new System.Data.SqlClient.SqlConnection(ConStr);
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql, Con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                Con.Close();
            }

            return ds.Tables[(0)].DefaultView;
        }

        public DataView GetDataView(string sql, string str)
        {
            System.Data.SqlClient.SqlConnection Con = new System.Data.SqlClient.SqlConnection(str);
            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sql, Con);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
            }
            catch
            {
                throw;
            }
            finally
            {
                Con.Close();
            }

            return ds.Tables[(0)].DefaultView;
        }

        public void FillCombo(ComboBox cmb)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String

            DataTable dt = new DataTable();
            dt.Columns.Add("ID"); dt.Columns.Add("Desc");
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = " ------ Select ------ ";
            dt.Rows.Add(dr);

            cmb.DisplayMember = dt.Columns[1].ColumnName;
            cmb.ValueMember = dt.Columns[0].ColumnName;
            cmb.DataSource = dt;
            cmb.SelectedIndex = 0;
        }

        public void FillComb(ComboBox cmb, string FColName, string SColName)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String

            DataTable dt = new DataTable();
            dt.Columns.Add(FColName); dt.Columns.Add(SColName);
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = " ------ Select ------ ";
            dt.Rows.Add(dr);

            cmb.DisplayMember = dt.Columns[1].ColumnName;
            cmb.ValueMember = dt.Columns[0].ColumnName;
            cmb.DataSource = dt;
            cmb.SelectedIndex = 0;
        }

        public void FillCombo(ComboBox cmb, string Str)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String

            dset = objTrans.FillDset("NewTable", Str, ConStr);

            DataTable dt = dset.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "0";

            dr[1] = " ------ Select ------ ";

            dset.Tables[0].Rows.InsertAt(dr, 0);
            cmb.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            cmb.ValueMember = dset.Tables[0].Columns[0].ColumnName;
            cmb.DataSource = dset.Tables[0];
            cmb.SelectedIndex = 0;
        }

        public void FillComb(ComboBox cmb, string Str)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String
            dset = objTrans.FillDset("NewTable", Str, ConStr);


            cmb.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            cmb.ValueMember = dset.Tables[0].Columns[0].ColumnName;
            cmb.DataSource = dset.Tables[0];
            cmb.SelectedIndex = 0;
        }

        public void FillCombo(ComboBox cmb, string Str, string AddStr)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String
            dset = objTrans.FillDset("NewTable", Str, ConStr);
            DataTable dt = dset.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = " ------ Select ------ ";
            dset.Tables[0].Rows.InsertAt(dr, 0);

            dr = dt.NewRow();
            dr[0] = "-1";
            dr[1] = AddStr;
            dset.Tables[0].Rows.InsertAt(dr, 1);

            cmb.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            cmb.ValueMember = dset.Tables[0].Columns[0].ColumnName;
            cmb.DataSource = dset.Tables[0];
            cmb.SelectedIndex = 0;
        }

        public void FillCombo(DataGridViewComboBoxColumn cmb, string Str)
        {
            dset = objTrans.FillDset("NewTable", Str, ConStr);

            DataTable dt = dset.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = " ------ Select ------ ";

            dset.Tables[0].Rows.InsertAt(dr, 0);
            cmb.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            cmb.ValueMember = dset.Tables[0].Columns[0].ColumnName;
            cmb.DataSource = dset.Tables[0];

        }

        public void FillComb(DataGridViewComboBoxColumn cmb, string Str)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String
            dset = objTrans.FillDset("NewTable", Str, ConStr);


            cmb.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            cmb.ValueMember = dset.Tables[0].Columns[0].ColumnName;
            cmb.DataSource = dset.Tables[0];
        }

        public void FillCombo(DataGridViewComboBoxCell cmb, string Str)
        {
            dset = objTrans.FillDset("NewTable", Str, ConStr);

            DataTable dt = dset.Tables[0];
            DataRow dr = dt.NewRow();
            dr[0] = "0";

            dr[1] = " ------ Select ------ ";
            dset.Tables[0].Rows.InsertAt(dr, 0);
            cmb.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            cmb.ValueMember = dset.Tables[0].Columns[0].ColumnName;
            cmb.DataSource = dset.Tables[0];

        }

        public void FillComb(DataGridViewComboBoxCell cmb, string Str)
        {
            dset = objTrans.FillDset("NewTable", Str, ConStr);

            cmb.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            cmb.ValueMember = dset.Tables[0].Columns[0].ColumnName;
            cmb.DataSource = dset.Tables[0];

        }

        public void FillLanguage(ComboBox cmb, int Type)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String

            DataTable dt = new DataTable();
            dt.Columns.Add("NO");
            dt.Columns.Add("Name");

            DataRow dr;
            if (Type == 1)
            {
                dr = dt.NewRow();
                dr[0] = "1";
                dr[1] = "English";
                dt.Rows.Add(dr);
            }
            dr = dt.NewRow();
            dr[0] = "2";
            dr[1] = "Marathi";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr[0] = "3";
            //dr[1] = "Hindi";
            //dt.Rows.Add(dr);

            cmb.DisplayMember = dt.Columns[1].ColumnName;
            cmb.ValueMember = dt.Columns[0].ColumnName;
            cmb.DataSource = dt;
            cmb.SelectedIndex = 0;
        }

        public void FillList(ListBox lst, string Str)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String
            dset = objTrans.FillDset("NewTable", Str, ConStr);
            lst.DataSource = dset.Tables[0];
            lst.DisplayMember = dset.Tables[0].Columns[1].ColumnName;
            lst.ValueMember = dset.Tables[0].Columns[0].ColumnName;

            //cmb.Text = "";
            //  lst.SelectedIndex = 0;
            //lst.Refresh();
            //lst.DataSource = dset.Tables[0];
        }

        public void FillList(ListBox lst, string ColName1, string ColName2)
        {
            //The Fillcombo is used for filling of dropdownlist the qurey code is filled in value propery and name in text property 
            // Dim text As String
            DataTable dt = new DataTable();
            dt.Columns.Add(ColName1);
            dt.Columns.Add(ColName2);

            lst.DisplayMember = dt.Columns[1].ColumnName;
            lst.ValueMember = dt.Columns[0].ColumnName;
            lst.DataSource = dt;
            //cmb.Text = "";
        }

        public void FillDays(ComboBox cmb)
        {
            DataTable dtDays = new DataTable();
            dtDays.Columns.Add("No", Type.GetType("System.Int32"));
            dtDays.Columns.Add("Name");

            for (int i = 1; i < 32; i++)
            {

                DataRow dr = dtDays.NewRow();
                dr[0] = i;
                dr[1] = i;
                dtDays.Rows.Add(dr);
            }
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "No";
            cmb.DataSource = dtDays;
        }

        public void FillMonth(ComboBox cmb)
        {
            DataTable dtMonth = new DataTable();
            dtMonth.Columns.Add("No", Type.GetType("System.Int32"));
            dtMonth.Columns.Add("Name");
            string strMonth = "";
            for (int i = 1; i <= 12; i++)
            {
                if (i == 1) strMonth = "Jan";
                else if (i == 2) strMonth = "Feb";
                else if (i == 3) strMonth = "Mar";
                else if (i == 4) strMonth = "Apr";
                else if (i == 5) strMonth = "May";
                else if (i == 6) strMonth = "Jun";
                else if (i == 7) strMonth = "Jul";
                else if (i == 8) strMonth = "Aug";
                else if (i == 9) strMonth = "Sep";
                else if (i == 10) strMonth = "Oct";
                else if (i == 11) strMonth = "Nov";
                else if (i == 12) strMonth = "Dec";

                DataRow dr = dtMonth.NewRow();
                dr[0] = i;
                dr[1] = strMonth;
                dtMonth.Rows.Add(dr);
            }
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "No";
            cmb.DataSource = dtMonth.DefaultView;
        }

        public void FillYear(ComboBox cmb)
        {
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("No", Type.GetType("System.Int32"));
            dtYear.Columns.Add("Name");

            for (int i = 1901; i <= DateTime.Now.Year; i++)
            {

                DataRow dr = dtYear.NewRow();
                dr[0] = i;
                dr[1] = i;
                dtYear.Rows.Add(dr);
            }
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "No";
            cmb.DataSource = dtYear;
        }

        public void FillWeek(ComboBox cmb)
        {
            DataTable dtMonth = new DataTable();
            dtMonth.Columns.Add("No", Type.GetType("System.Int32"));
            dtMonth.Columns.Add("Name");
            string strMonth = "";
            for (int i = 0; i <= 6; i++)
            {
                if (i == 0) strMonth = "Sunday";
                else if (i == 1) strMonth = "Monday";
                else if (i == 2) strMonth = "Tuesday";
                else if (i == 3) strMonth = "Wednesday";
                else if (i == 4) strMonth = "Thursday";
                else if (i == 5) strMonth = "Friday";
                else if (i == 6) strMonth = "Saturday";

                DataRow dr = dtMonth.NewRow();
                dr[0] = i;
                dr[1] = strMonth;
                dtMonth.Rows.Add(dr);
            }
            cmb.DisplayMember = "Name";
            cmb.ValueMember = "No";
            cmb.DataSource = dtMonth.DefaultView;
        }

        public void OpenForm(Form frmChild, Form frmParent)
        {
            bool flag = true;
            foreach (Form frm in Application.OpenForms)
            {
                if (frmChild.Name != "SalesRegister" && frmChild.Name != "VatRegister" && frmChild.Name != "ReportViewSource")
                {
                    if (frm.Name == frmChild.Name || frm.Name == frmChild.Name + "AE")
                    {
                        flag = false; break;
                    }
                }
            }
            if (flag == true)
            {

                // frmChild.BackColor = Color.FromArgb(230, 235, 235);
                frmChild.BackColor = Color.FromArgb(255, 255, 255);
                //frmChild.StartPosition = FormStartPosition.CenterScreen;

                FormatControl(frmChild.Controls);

                KeyPressFormat(frmChild.Controls);

                //SetHelpInfo(frmChild);
                if (frmChild.Name != "ReportViewSource")
                {
                    frmChild.MaximizeBox = false;
                    frmChild.MinimizeBox = false;
                    frmChild.MdiParent = frmParent;
                    frmChild.ControlBox = false;
                    frmChild.Activated += new EventHandler(FormActivated);
                    frmChild.Resize += new EventHandler(frmChild_Resize);
                    frmChild.Paint += new PaintEventHandler(frmChild_Paint);
                    frmChild.FormClosed += new FormClosedEventHandler(frmChild_FormClosed);

                    if (frmParent is Yadi.MDIParent1)
                    {
                        (frmParent as Yadi.MDIParent1).hidePnlMainMenu();
                    }

                    setToolBarControl(frmParent.Controls, frmChild);
                    frmChild.StartPosition = FormStartPosition.Manual;
                    frmChild.Top = 0;
                    frmChild.FormBorderStyle = FormBorderStyle.None;
                    frmChild.Text = "";
                    frmChild.KeyPreview = true;
                    DBGetVal.MainForm.lblDispTaskbar.Visible = false;
                    frmChild.Show();
                    frmChild.Dock = DockStyle.Fill;
                }
                else
                {
                    frmChild.WindowState = FormWindowState.Maximized;
                    frmChild.ShowDialog();
                }
            }
        }

        void frmChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            DBGetVal.MainForm.setToolBarControl();
            //setToolBarControl(DBGetVal.MainForm.Controls, sender as Form);
        }

        public void OpenForm(Form frmChild)
        {
            bool flag = true;
            foreach (Form frm in Application.OpenForms)
            {
                if (frmChild.Name != "SalesRegister" && frmChild.Name != "VatRegister" && frmChild.Name != "KeyBoard")
                {
                    if (frm.Name == frmChild.Name || frm.Name == frmChild.Name + "AE")
                    {
                        flag = false; break;
                    }
                }
            }
            if (flag == true)
            {
                frmChild.MinimizeBox = false;
                //  frmChild.BackColor = Color.FromArgb(213, 225, 230);
                frmChild.BackColor = Color.FromArgb(255, 255, 255);//==umesh
                frmChild.MaximizeBox = false;
                frmChild.FormBorderStyle = FormBorderStyle.None;
                frmChild.StartPosition = FormStartPosition.CenterScreen;
                frmChild.Resize += new EventHandler(frmChild_Resize);
                frmChild.Paint += new PaintEventHandler(frmChildOther_Paint);
                FormatControl(frmChild.Controls);
                frmChild.Top = 0;
                frmChild.KeyPreview = true;
                KeyPressFormat(frmChild.Controls);
                frmChild.ShowDialog();
            }
        }

        public void CloseForm(Form frmChild)
        {

            foreach (Form frm in Application.OpenForms)
            {
                if (frmChild.Name != "SalesRegister" && frmChild.Name != "VatRegister" && frmChild.Name != "KeyBoard")
                {
                    if (frm.Name == frmChild.Name || frm.Name == frmChild.Name + "AE")
                    {
                        frm.Close();
                        break;
                    }
                }
            }
        }

        private void frmChild_Paint(object sender, PaintEventArgs g)
        {
            Rectangle rect = ((Form)(sender)).ClientRectangle;
            if (rect.Height != 0 && rect.Width != 0)
            {
                //LinearGradientBrush brush = new LinearGradientBrush(rect, Color.WhiteSmoke, Color.Gainsboro, 60);
                LinearGradientBrush brush = new LinearGradientBrush(rect, Color.White, Color.White, 60);
                g.Graphics.FillRectangle(brush, rect);
            }
        }

        private void frmChildOther_Paint(object sender, PaintEventArgs g)
        {
            Rectangle rect = ((Form)(sender)).ClientRectangle;
            if (rect.Height != 0 && rect.Width != 0)
            {
                LinearGradientBrush brush = new LinearGradientBrush(rect, Color.AliceBlue, Color.SandyBrown, 60);
                g.Graphics.FillRectangle(brush, rect);


                Pen pen = new Pen(Color.SandyBrown, 2);
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Outset;
                g.Graphics.DrawRectangle(pen, rect);
            }
        }

        public void frmChild_Resize(object sender, EventArgs e)
        {
            foreach (Control ctrl in ((Form)sender).Controls)
            {
                if (ctrl is Panel)
                {
                    if (ctrl.Name == "pnlMain")
                    {
                        ctrl.Location = new Point(((Form)sender).Width / 2 - ctrl.Width / 2, (((((Form)sender).Height / 2) - (ctrl.Height / 2)) / 2) / 2);
                    }
                    else if (ctrl.Name == "pnlMainForm")
                    {
                        ctrl.Location = new Point(((Form)sender).Width / 2 - ctrl.Width / 2, 5);
                        ctrl.Height = ((Form)sender).Height - 10;
                        // ((Panel)ctrl).BorderStyle = BorderStyle.FixedSingle;
                        //((Panel)ctrl).Paint += new PaintEventHandler(panelBorder_Paint);
                        //panelBorder_Paint(new PaintEventArgs(((Panel)ctrl).CreateGraphics(), ((Panel)ctrl).ClientRectangle));
                    }
                }
            }
        }

        private void panelBorder_Paint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.CadetBlue, 1);
            e.Graphics.DrawRectangle(Pens.CadetBlue,
            e.ClipRectangle.Left,
            e.ClipRectangle.Top,
            e.ClipRectangle.Width - 1,
            e.ClipRectangle.Height - 1);
            // base.OnPaint(e);
        }

        private void GridView_Paint(object sender, PaintEventArgs e)
        {
            //base.OnPaint(e);
            if (sender is DataGridView)
            {
                //  DataGridView gv = (DataGridView)sender;
                //Pen p = new Pen(Color.CadetBlue, 1);
                //e.Graphics.DrawRectangle(Pens.SteelBlue,
                //gv.Left,
                //gv.Top,
                //gv.Width - 1,
                //gv.Height - 1);

                //ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.SteelBlue, ButtonBorderStyle.Solid);

                //e.Graphics.DrawRectangle(Pens.SteelBlue,
                //e.ClipRectangle.Left,
                //e.ClipRectangle.Top,
                //e.ClipRectangle.Width - 1,
                //e.ClipRectangle.Height - 1);
            }

        }
        public void FormActivated(object sender, EventArgs e)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == ((Form)sender).Name)
                {
                    foreach (ToolBarButton tbl in mMain.toolBar1.Buttons)
                    {
                        if (tbl.Name == frm.Name)
                            mMain.lblMainTitle.Text = tbl.Text;
                    }
                    break;
                }
            }
        }

        public void setToolBarControl(System.Windows.Forms.Control.ControlCollection ctrls, Form frmChild)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is ToolBar)
                {
                    if (((ToolBar)ctrl).Buttons.Count == 0) ((Panel)ctrls.Owner).Visible = true;
                    ToolBarButton tbl = new ToolBarButton();
                    tbl.Name = frmChild.Name;
                    tbl.Text = frmChild.Text;
                    tbl.ImageIndex = 0;
                    tbl.Style = ToolBarButtonStyle.PushButton;
                    ((ToolBar)ctrl).Buttons.Add(tbl);
                    mMain.lblMainTitle.Text = tbl.Text;
                }
                else if (ctrl is System.Windows.Forms.Panel)
                {
                    setToolBarControl(ctrl.Controls, frmChild);
                }
            }
        }

        public void ButtonMouseHover(object sender, EventArgs e)
        {
            //((Button)sender).BackgroundImage = global::Yadi.Properties.Resources.btnBGOver;
            //((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
            // ((Button)sender).ForeColor = Color.White;

            //  ((Button)sender).BackColor = Color.SandyBrown;
            ((Button)sender).FlatAppearance.BorderSize = 1;
            ((Button)sender).FlatAppearance.BorderColor = Color.Red;
            // ButtonMouseLeave(sender,new EventArgs());
        }

        public void ButtonMouseLeave(object sender, EventArgs e)
        {
            if (((Button)sender).Focused == false)
            {
                //((Button)sender).BackgroundImage = global::Yadi.Properties.Resources.btnBG;
                //((Button)sender).BackgroundImageLayout = ImageLayout.Stretch;
                //((Button)sender).ForeColor = Color.White;
                ((Button)sender).FlatAppearance.BorderSize = 1;
                ((Button)sender).FlatAppearance.BorderColor = Color.SteelBlue;

                //((Button)sender).ForeColor = Color.DarkOrange;
                //((Button)sender).BackColor = Color.SandyBrown;
            }
        }

        public void FormatControl(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            DataGridView ctrl1 = null;
            DateTimePicker ctrl2 = null;
            Button ctrl3 = null;
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is DataGridView)
                {

                    ctrl1 = new DataGridView();
                    ctrl1 = (DataGridView)ctrl;
                    ctrl1.BackgroundColor = Color.White;
                    ctrl1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(193, 211, 219);
                    ctrl1.ColumnHeadersDefaultCellStyle.Font = GetFont();
                    ctrl1.ColumnHeadersHeight = 20;
                    ctrl1.RowTemplate.Height = 20;
                    ctrl1.RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(193, 211, 219);
                    ctrl1.RowHeadersDefaultCellStyle.Font = GetFont();
                    ctrl1.RowTemplate.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 192, 130);
                    ctrl1.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.Black;
                    ctrl1.RowTemplate.DefaultCellStyle.Font = GetSimpleFont(FontStyle.Regular);
                    ctrl1.DefaultCellStyle.Font = GetSimpleFont(FontStyle.Regular);
                    ctrl1.RowTemplate.DefaultCellStyle.ForeColor = Color.Black;
                    ctrl1.RowTemplate.Resizable = DataGridViewTriState.False;
                    if (ctrl1.Rows.Count > 0) ctrl1.Rows[0].DefaultCellStyle.Font = GetFont();
                    ctrl1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    ctrl1.RowHeadersVisible = false;
                    ctrl1.ColumnAdded += new DataGridViewColumnEventHandler(DatGridView_ColumnAdded);
                    ctrl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
                    //ctrl1.BorderStyle = BorderStyle.None;
                    //ctrl1.Paint += new PaintEventHandler(GridView_Paint);
                    for (int i = 0; i < ctrl1.Columns.Count; i++)
                        ctrl1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                }
                else if (ctrl is DateTimePicker)
                {
                    ((DateTimePicker)ctrl).GotFocus += new EventHandler(ControlGotFocus);
                    ((DateTimePicker)ctrl).LostFocus += new EventHandler(ControlLostFocus);

                    ctrl2 = new DateTimePicker();
                    ctrl2 = (DateTimePicker)ctrl;
                    //ctrl2.MinDate = DBGetVal.FromDate;
                    //ctrl2.MaxDate = DBGetVal.ToDate;
                    ctrl2.Format = DateTimePickerFormat.Custom;
                    ctrl2.CustomFormat = "dd-MMM-yyyy";
                    ctrl2.Font = GetFont();
                    ctrl2.Width = 110;
                }
                //else if (ctrl is OMControls.OMButton)
                //{
                //    System.Windows.Forms.Button ctrl4 = new System.Windows.Forms.Button();
                //    ctrl4 = (System.Windows.Forms.Button)ctrl;
                //    //ctrl3.BackColor = Color.Black;
                //    ctrl4.Font = GetFont(FontStyle.Regular);
                //    ctrl4.Text = ctrl4.Text.ToUpper();

                //    if (ctrl4.Text == "SAVE") ctrl4.Text = ctrl4.Text.Replace("S", "&S");
                //    else if (ctrl4.Text == "DELETE") ctrl4.Text = ctrl4.Text.Replace("D", "&D");
                //    else if (ctrl4.Text == "PRINT") ctrl4.Text = ctrl4.Text.Replace("P", "&P");
                //    else if (ctrl4.Text == "EXIT") ctrl4.Text = ctrl4.Text.Replace("X", "&X");
                //    else if (ctrl4.Text == "SHOW") ctrl4.Text = ctrl4.Text.Replace("S", "&S");
                //    else if (ctrl4.Text == "UPDATE") ctrl4.Text = ctrl4.Text.Replace("U", "&U");
                //    else if (ctrl4.Text == "NEW") ctrl4.Text = ctrl4.Text.Replace("N", "&N");
                //    else if (ctrl4.Text == "SEARCH") ctrl4.Text = ctrl4.Text.Replace("H", "&H");
                //    else if (ctrl4.Text == "SEARCH1") ctrl4.Text = ctrl4.Text.Replace("1", "&1");
                //    else if (ctrl4.Text == "SEARCH2") ctrl4.Text = ctrl4.Text.Replace("2", "&2");
                //    else if (ctrl4.Text == "CLOSE") ctrl4.Text = ctrl4.Text.Replace("E", "&E");
                //    else if (ctrl4.Text == "CANCEL") ctrl4.Text = ctrl4.Text.Replace("C", "&C");
                //    else if (ctrl4.Text == "PRINT BARCODE") ctrl4.Text = ctrl4.Text.Replace("P", "&P");

                //    ctrl4.BackgroundImage = global::Yadi.Properties.Resources.btnBG;
                //    ctrl4.BackgroundImageLayout = ImageLayout.Stretch;
                //    ctrl4.ForeColor = Color.Black;
                //    ctrl4.FlatStyle = FlatStyle.Flat;
                //    ctrl4.FlatAppearance.BorderSize = 0;

                //    ctrl4.GradientBottom = Color.FromArgb(226, 236, 239);// System.Drawing.Color.FromArgb(63, 92, 103);
                //    ctrl4.GradientBottomOnMouse = Color.LightGoldenrodYellow;// System.Drawing.Color.FromArgb(101, 221, 237);
                //    ctrl4.GradientMiddle = Color.FromArgb(226, 236, 239);
                //    ctrl4.GradientMiddleOnMouse = Color.LightGoldenrodYellow; //System.Drawing.Color.White;
                //    ctrl4.GradientShow = true;
                //    ctrl4.GradientTop = System.Drawing.Color.FromArgb(63, 92, 103);
                //    ctrl4.GradientTopOnMouse = Color.Orange;// System.Drawing.Color.Silver;
                //    ctrl4.CornerRadius = 1;
                //    //ctrl4.MouseHover += new EventHandler(ButtonMouseHover);
                //    //ctrl4.GotFocus += new EventHandler(ButtonMouseHover);
                //    //ctrl4.MouseLeave += new EventHandler(ButtonMouseLeave);
                //    //ctrl4.LostFocus += new EventHandler(ButtonMouseLeave);
                //    if (ctrl4.Text == "<<" || ctrl4.Text == "<" || ctrl4.Text == ">" || ctrl4.Text == ">>")
                //        ctrl4.Font = GetSimpleFont(FontStyle.Bold);
                //    if (ctrl4.Name == "btnShowAll") ctrl4.Width = 102;
                //    //ctrl1.FlatAppearance.BorderColor = Color.Black;
                //}
                else if (ctrl is Button)
                {
                    ctrl3 = new Button();
                    ctrl3 = (Button)ctrl;
                    //ctrl3.BackColor = Color.Black;
                    ctrl3.Font = GetFont(FontStyle.Bold);
                    ctrl3.Text = ctrl3.Text.ToUpper();

                    if (ctrl3.Text == "SAVE") ctrl3.Text = ctrl3.Text.Replace("S", "&S");
                    else if (ctrl3.Text == "DELETE") ctrl3.Text = ctrl3.Text.Replace("D", "&D");
                    else if (ctrl3.Text == "PRINT") ctrl3.Text = ctrl3.Text.Replace("P", "&P");
                    else if (ctrl3.Text == "EXIT") ctrl3.Text = ctrl3.Text.Replace("X", "&X");
                    else if (ctrl3.Text == "SHOW") ctrl3.Text = ctrl3.Text.Replace("S", "&S");
                    else if (ctrl3.Text == "UPDATE") ctrl3.Text = ctrl3.Text.Replace("U", "&U");
                    else if (ctrl3.Text == "NEW") ctrl3.Text = ctrl3.Text.Replace("N", "&N");
                    else if (ctrl3.Text == "SEARCH") ctrl3.Text = ctrl3.Text.Replace("H", "&H");
                    else if (ctrl3.Text == "SEARCH1") ctrl3.Text = ctrl3.Text.Replace("1", "&1");
                    else if (ctrl3.Text == "SEARCH2") ctrl3.Text = ctrl3.Text.Replace("2", "&2");
                    else if (ctrl3.Text == "CLOSE") ctrl3.Text = ctrl3.Text.Replace("E", "&E");
                    else if (ctrl3.Text == "CANCEL") ctrl3.Text = ctrl3.Text.Replace("C", "&C");
                    else if (ctrl3.Text == "PRINT BARCODE") ctrl3.Text = ctrl3.Text.Replace("P", "&P");

                    //ctrl3.BackgroundImage = global::Yadi.Properties.Resources.btnBG;
                    //ctrl3.BackgroundImageLayout = ImageLayout.Stretch;
                    ctrl3.ForeColor = Color.White;
                    ctrl3.FlatStyle = FlatStyle.Flat;
                    ctrl3.FlatAppearance.BorderSize = 1;
                    ctrl3.FlatAppearance.BorderColor = Color.SteelBlue;
                    // ctrl3.BackColor = Color.SandyBrown;//==umesh
                    ctrl3.BackColor = Color.FromArgb(255, 93, 173, 226);
                    ctrl3.FlatAppearance.MouseOverBackColor = Color.NavajoWhite;// Color.SkyBlue;
                    ctrl3.FlatAppearance.MouseDownBackColor = Color.NavajoWhite;// FromArgb(255, 128, 0);
                    ctrl3.GotFocus += new EventHandler(ButtonMouseHover);
                    ctrl3.LostFocus += new EventHandler(ButtonMouseLeave);
                    //ctrl3.MouseHover += new EventHandler(ButtonMouseHover);
                    //ctrl3.MouseLeave += new EventHandler(ButtonMouseLeave);
                    //if (ctrl3.Text == "<<" || ctrl3.Text == "<" || ctrl3.Text == ">" || ctrl3.Text == ">>")
                    //  ctrl3.Font = GetSimpleFont(FontStyle.Bold);
                    if (ctrl3.Name == "btnShowAll") ctrl3.Width = 102;
                    //ctrl1.FlatAppearance.BorderColor = Color.Black;
                }
                else if (ctrl is ProgressBar)
                {
                    //((ProgressBar)ctrl).BackColor = Color.FromArgb(78, 116, 133);
                    ((ProgressBar)ctrl).BackColor = Color.FromArgb(255, 255, 255);//==umesh
                    ((ProgressBar)ctrl).ForeColor = Color.FromArgb(193, 211, 219);
                }
                else if (ctrl is TextBox)
                {
                    ((TextBox)ctrl).GotFocus += new EventHandler(ControlGotFocus);
                    ((TextBox)ctrl).LostFocus += new EventHandler(ControlLostFocus);

                    ((TextBox)ctrl).BorderStyle = BorderStyle.Fixed3D;
                    ((TextBox)ctrl).Font = GetFont();
                    if (((TextBox)ctrl).Multiline == false)
                        ((TextBox)ctrl).Height = 24;
                }
                else if (ctrl is CheckBox)
                {
                    ((CheckBox)ctrl).GotFocus += new EventHandler(ControlGotFocus);
                    ((CheckBox)ctrl).LostFocus += new EventHandler(ControlLostFocus);
                }
                else if (ctrl is RadioButton)
                {
                    ((RadioButton)ctrl).GotFocus += new EventHandler(ControlGotFocus);
                    ((RadioButton)ctrl).LostFocus += new EventHandler(ControlLostFocus);
                }
                else if (ctrl is ComboBox)
                {
                    ((ComboBox)ctrl).GotFocus += new EventHandler(ControlGotFocus);
                    ((ComboBox)ctrl).LostFocus += new EventHandler(ControlLostFocus);

                    ((ComboBox)ctrl).Font = GetFont();
                    ((ComboBox)ctrl).Height = 24;
                }
                else if (ctrl is ListBox)
                {
                    ((ListBox)ctrl).GotFocus += new EventHandler(ControlGotFocus);
                    ((ListBox)ctrl).LostFocus += new EventHandler(ControlLostFocus);

                    ((ListBox)ctrl).Font = GetFont();
                    ((ListBox)ctrl).ForeColor = Color.Maroon;

                    new ListSearch(((ListBox)ctrl));
                }
                else if (ctrl is OMControls.OMLabel)
                {
                    OMControls.OMLabel ctrl5 = new OMControls.OMLabel();
                    ctrl5 = (OMControls.OMLabel)ctrl;

                    if (ctrl.Name == "lblMsg")
                    {
                        ctrl5.GradientBottom = Color.FromArgb(255, 224, 192);
                        ctrl5.GradientTop = Color.FromArgb(255, 224, 192);
                        ctrl5.GradientMiddle = System.Drawing.Color.White;
                        ctrl5.BorderStyle = BorderStyle.FixedSingle;
                        ((OMControls.OMLabel)ctrl).Font = new Font("Verdana", 11, FontStyle.Bold);
                    }
                    else
                    {
                        ctrl5.GradientBottom = Color.FromArgb(226, 236, 239);// Color.FromArgb(213, 225, 230);
                        ctrl5.GradientTop = Color.FromArgb(63, 92, 103);// Color.FromArgb(78, 116, 133);
                        ctrl5.GradientMiddle = Color.FromArgb(226, 236, 239);// System.Drawing.Color.White;
                        ctrl5.GradientShow = true;
                        ctrl5.BorderStyle = BorderStyle.None;
                        ((OMControls.OMLabel)ctrl).Font = GetFont();
                    }
                }
                else if (ctrl is Label)
                {

                    if (ctrl.Name != "lblTitle" && ctrl.Name != "lblTotalAmt" && ctrl.Name != "lblMsg" && ctrl.Name != "lblBillItem")
                        ((Label)ctrl).Font = GetFont();

                    if (ctrl.Name == "lblHead" || ctrl.Name == "lblFooter")
                        ctrl.Height = 20;

                    if (ctrl.Name.IndexOf("Star") > 0)
                    {
                        ctrl.ForeColor = Color.Red;
                        ctrl.Font = new Font("Arial", 9, FontStyle.Regular);
                    }
                    else if (ctrl.Name.IndexOf("ChkHelp") > 0)
                    {
                        ctrl.ForeColor = Color.Gray;
                        ctrl.Font = new Font("Arial", 7, FontStyle.Regular);
                    }
                    else if (ctrl.Name.IndexOf("lblHelp") >= 0)
                    {
                        ctrl.ForeColor = Color.Maroon;
                        ctrl.Font = new Font("Arial", 9, FontStyle.Regular);
                    }
                }
                else if (ctrl is OMControls.OMPanel)
                {
                    OMControls.OMPanel ctrl5 = new OMControls.OMPanel();
                    ctrl5 = (OMControls.OMPanel)ctrl;
                    if (ctrl5.Name == "pnlStatus")
                    {
                        ctrl5.GradientBottom = Color.FromArgb(213, 225, 230);
                        ctrl5.GradientTop = Color.FromArgb(78, 116, 133);
                        ctrl5.GradientTop = Color.FromArgb(255, 255, 255);//==umesh
                        ctrl5.GradientMiddle = System.Drawing.Color.White;
                        ctrl5.GradientShow = true;
                        ctrl5.BorderStyle = BorderStyle.None;
                    }
                    else
                    {
                        ctrl5.GradientBottom = Color.FromArgb(78, 116, 133);
                        ctrl5.GradientTop = System.Drawing.Color.Bisque;
                        ctrl5.GradientMiddle = System.Drawing.Color.White;
                        ctrl5.GradientShow = true;
                        ctrl5.BorderStyle = BorderStyle.None;
                    }
                    FormatControl(ctrl.Controls);
                }
                else if (ctrl is System.Windows.Forms.Panel)
                {
                    FormatControl(ctrl.Controls);

                    if (ctrl.Name == "pnlMainForm")
                    {
                        ctrl.Location = new Point(((Form)(ctrl.Parent)).Width / 2 - ctrl.Width / 2, 5);
                        ctrl.Height = ((Form)(ctrl.Parent)).Height - 10;
                        //((Panel)ctrl).BorderStyle = BorderStyle.FixedSingle;
                    }
                    else if (ctrl.Name == "pnlMain")
                    {
                        ctrl.Location = new Point(((Form)(ctrl.Parent)).Width / 2 - ctrl.Width / 2, (((((Form)(ctrl.Parent)).Height / 2) - (ctrl.Height / 2)) / 2) / 2);
                        //((OMControls.OMGPanel)ctrl).LightingColor = Color.FromArgb(213, 225, 230);
                        //((Panel)ctrl).BorderStyle = BorderStyle.FixedSingle;
                        //((Panel)ctrl).BackColor = Color.FromArgb(185, 206, 215);
                    }

                    if (ctrl is OMControls.OMBPanel)
                    {
                        ((OMControls.OMBPanel)ctrl).BorderStyle = BorderStyle.None;
                        ((OMControls.OMBPanel)ctrl).BorderColor = Color.SteelBlue;
                        ((OMControls.OMBPanel)ctrl).BorderRadius = 2;
                    }
                }
                else if (ctrl is System.Windows.Forms.PictureBox)
                {
                    if (ctrl.Name.IndexOf("pctbCalc") >= 0)
                    {
                        ((PictureBox)ctrl).MouseHover += new EventHandler(picturebox_MouseHover);
                        ((PictureBox)ctrl).DoubleClick += new EventHandler(picturebox_DoubleClick);
                    }
                }
                else if (ctrl is System.Windows.Forms.GroupBox)
                {
                    FormatControl(ctrl.Controls);
                }
                else if (ctrl is OMControls.OMTabControl)
                {
                    ctrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
                    ((OMControls.OMTabControl)(ctrl)).ScrollButtonStyle = OMControls.OMScrollButtonStyle.Never;
                    OMControls.VsTabDrawer VSTab = new OMControls.VsTabDrawer();
                    //VSTab.GradientActiveTop = Color.FromArgb(78, 116, 133);
                    VSTab.GradientActiveTop = Color.FromArgb(255, 255, 255);//umesh
                    VSTab.GradientActiveBottom = Color.FromArgb(213, 225, 230);
                    VSTab.GradientActiveShow = true;
                    ((OMControls.OMTabControl)(ctrl)).TabDrawer = VSTab;
                    ((OMControls.OMTabControl)(ctrl)).ActiveColor = Color.FromArgb(182, 202, 211);
                    ((OMControls.OMTabControl)(ctrl)).ForeColor = Color.Black;
                    ((OMControls.OMTabControl)(ctrl)).InactiveColor = Color.FromArgb(213, 225, 230);
                    FormatControl(ctrl.Controls);
                    foreach (Control ctrltab in ((OMControls.OMTabControl)(ctrl)).Controls)
                    {
                        if (((OMControls.OMTabPage)ctrltab) is OMControls.OMTabPage)
                            ((OMControls.OMTabPage)ctrltab).BackColor = Color.FromArgb(213, 225, 230);
                    }
                }
                else if (ctrl is OMControls.OMTabPage)
                {
                    //((OMControls.OMTabPage)(ctrl)).BackgroundImage = global::Yadi.Properties.Resources.btnBG;
                    FormatControl(ctrl.Controls);
                }
                else if (ctrl is System.Windows.Forms.TabControl)
                {
                    ctrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
                    ((TabControl)(ctrl)).Appearance = TabAppearance.FlatButtons;
                    FormatControl(ctrl.Controls);
                    foreach (TabPage tbpg in ((TabControl)(ctrl)).TabPages)
                        tbpg.BackColor = Color.FromArgb(213, 225, 230);
                }
                else if (ctrl is System.Windows.Forms.TabPage)
                {
                    ((TabPage)(ctrl)).BackgroundImage = global::Yadi.Properties.Resources.btnBG;
                    ((TabPage)(ctrl)).BackgroundImageLayout = ImageLayout.Stretch;
                    FormatControl(ctrl.Controls);
                }
            }
        }

        #region Calculator Methods
        public void picturebox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();
            tip.IsBalloon = true;
            tip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            tip.ToolTipTitle = "Calculator";
            tip.SetToolTip(((PictureBox)sender), "Ctrl + Alt +C");
        }
        public void DisplayCalc()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo pr = new System.Diagnostics.ProcessStartInfo("Calc.exe");
                pr.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                System.Diagnostics.Process newProc1 = null;
                newProc1 = System.Diagnostics.Process.Start(pr);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void picturebox_DoubleClick(object sender, EventArgs e)
        {
            DisplayCalc();
        }
        #endregion

        public void DatGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public void ControlGotFocus(object sender, EventArgs e)
        {

            if (((Control)sender) is TextBox)
            {
                ((TextBox)sender).SelectionStart = 0;
                ((TextBox)sender).SelectionLength = ((TextBox)sender).Text.Length;
                //((Control)sender).BackColor = Color.FromArgb(255, 224, 192);
                ((Control)sender).BackColor = Color.FromArgb(252, 176, 101);
            }
            else if (((Control)sender) is CheckBox)
            {
                //((Control)sender).BackColor = Color.FromArgb(255, 224, 192);

                Graphics g = ((Control)((Control)sender).Parent).CreateGraphics();
                Pen penBorder = new Pen(Color.FromArgb(255, 102, 102), 2);
                Rectangle rectBorder = new Rectangle(((Control)sender).Location.X - 1, ((Control)sender).Location.Y - 1, ((Control)sender).Width + 1, ((Control)sender).Height + 1);
                g.DrawRectangle(penBorder, rectBorder);
            }
            else if (((Control)sender) is RadioButton)
            {
                ((Control)sender).BackColor = Color.FromArgb(255, 224, 192);
            }
            else if (((Control)sender) is ListBox)
            {
                ((Control)sender).BackColor = Color.FromArgb(229, 255, 204);
            }
            else
                ((Control)sender).BackColor = Color.FromArgb(255, 224, 192);
        }

        public void ControlLostFocus(object sender, EventArgs e)
        {


            if (((Control)sender) is CheckBox)
            {
                ((Control)sender).BackColor = Color.Transparent;

                Graphics g = ((Control)((Control)sender).Parent).CreateGraphics();
                Pen penBorder = new Pen(((Control)((Control)sender).Parent).BackColor, 2);
                Rectangle rectBorder = new Rectangle(((Control)sender).Location.X - 1, ((Control)sender).Location.Y - 1, ((Control)sender).Width + 1, ((Control)sender).Height + 1);
                g.DrawRectangle(penBorder, rectBorder);

            }
            else if (((Control)sender) is RadioButton)
            {
                ((Control)sender).BackColor = Color.Transparent;
            }
            else
                ((Control)sender).BackColor = Color.White;
        }

        public void GetVoucherLock()
        {
            try
            {
                string strNo = ObjQry.ReturnString("Select [Dfhr4pV0l8zivvu/b51VEg==] From [fk5PeDBBu3VfeawHXhrpjg==]", CommonFunctions.ConStr);
                if (strNo == "")
                    VoucherLock = 0;
                else
                    VoucherLock = Convert.ToInt64(secure.psDecrypt(strNo));
            }
            catch (Exception e)
            {
                VoucherLock = 0;
                CommonFunctions.ErrorMessge = e.Message;
            }
        }

        public bool AllowVoucher()
        {
            long count = ObjQry.ReturnLong("Select count(*) From TVoucherEntry", CommonFunctions.ConStr);
            if (VoucherLock <= count)
                return false;
            else
                return true;
        }

        public void setVoucherLock(string IsLock, Control.ControlCollection ctrls)
        {
            //Button ctrl1 = null;
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is Button)
                {
                    if (((Button)ctrl).Name.ToUpper() == "BTNSAVE")
                    {
                        if (secure.psDecrypt(IsLock).ToString() != "0")
                            ((Button)ctrl).Visible = false;
                    }
                    else if (((Button)ctrl).Name.ToUpper() == "BTNDELETE")
                    {
                        if (secure.psDecrypt(IsLock).ToString() != "0")
                            ((Button)ctrl).Visible = false;
                    }
                }
                else if (ctrl is System.Windows.Forms.Panel)
                {
                    setVoucherLock(IsLock, ctrl.Controls);
                }
                else if (ctrl is System.Windows.Forms.GroupBox)
                {
                    setVoucherLock(IsLock, ctrl.Controls);
                }
                else if (ctrl is System.Windows.Forms.TabControl)
                {
                    setVoucherLock(IsLock, ctrl.Controls);
                }
                else if (ctrl is System.Windows.Forms.TabPage)
                {
                    setVoucherLock(IsLock, ctrl.Controls);
                }
            }
        }

        public void ActiveNewForm()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == DBGetVal.NewCustForm.Name)
                {
                    frm.Activate();
                    break;
                }
            }
        }

        #region KeyDown Events
        public void KeyPressFormat(System.Windows.Forms.Control.ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                ctrl.KeyPress += new KeyPressEventHandler(ControlKeyPress);
                ctrl.KeyDown += new KeyEventHandler(ControlKeyDown);
                if (ctrl is Panel)
                    KeyPressFormat(ctrl.Controls);
                else if (ctrl is GroupBox)
                    KeyPressFormat(ctrl.Controls);
                else if (ctrl is OMControls.OMBPanel)
                    KeyPressFormat(ctrl.Controls);
                else if (ctrl is OMControls.OMPanel)
                    KeyPressFormat(ctrl.Controls);
                else if (ctrl is OMControls.OMGPanel)
                    KeyPressFormat(ctrl.Controls);
                else if (ctrl is OMControls.OMTabControl)
                    KeyPressFormat(ctrl.Controls);
                else if (ctrl is OMControls.OMTabPage)
                    KeyPressFormat(ctrl.Controls);
            }
        }

        public Form GetParentForm(Control ctrl)
        {
            if (ctrl.Parent is Form)
                return (Form)ctrl.Parent;
            else
                return GetParentForm(ctrl.Parent);
        }

        public Control GetFocusControl(System.Windows.Forms.Control.ControlCollection ctrls, int index)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl.TabIndex == index && ctrl.TabStop)
                {
                    /*
                    ctrlFocus = ctrl;
                    flagFocus = false;
                    break;
                    */
                    return ctrl;
                }

                if (ctrl is Panel || ctrl is GroupBox)
                {
                    Control ctrlp = GetFocusControl(ctrl.Controls, index);
                    if (ctrlp != null) return ctrlp;
                }
                /*
            else if (ctrl is GroupBox)
                GetFocusControl(ctrl.Controls, index);
            if (flagFocus == false) break;
                 * */
            }

            return null;
        }

        public Control GetFocusControlMain(System.Windows.Forms.Control.ControlCollection ctrls, int index)
        {
            Control nextCtrl = GetFocusControl(ctrls, index);
            if (nextCtrl == null)
            {
                nextCtrl = GetFocusControl(ctrls, 0);
            }

            if (nextCtrl == null)
                return null;

            for (int i = 0; i < 10; i++)
            {
                if (nextCtrl.Visible && nextCtrl.Enabled)
                    return nextCtrl;
                else
                {
                    nextCtrl = GetFocusControl(ctrls, ++index);
                    if (nextCtrl == null)
                        return null;
                }
            }
            return null;
        }

        private void ControlKeyPress(object sender, KeyPressEventArgs e)
        {
            //Form frm = null;
            if (e.Handled)
                return;

            if (sender is Control)
            {
                Control sControl = (Control)sender;
                /*
                if (sControl.Name == "txtTaxPercent") index = 7;
                else if (sControl.Name == "lstColor") index = 7;
                else if (sControl.Name == "dgPrevRate") index = 13;
                else if (sControl.Name == "lstUOM") index = 550;
                else if (sControl.Name == "lstItem") index = 550;
                flagFocus = true;
                if (((Control)sender).Parent is Form)
                {
                    GetFocusControl(((Control)sender).Parent.Controls, index);
                    frm = (Form)((Control)sender).Parent;
                }
                else if (((Control)sender).Parent.Parent is Form)
                {
                    GetFocusControl(((Control)sender).Parent.Parent.Controls, index);
                    frm = (Form)((Control)sender).Parent.Parent;
                }
                else if (((Control)sender).Parent.Parent.Parent is Form)
                {
                    GetFocusControl(((Control)sender).Parent.Parent.Parent.Controls, index);
                    frm = (Form)((Control)sender).Parent.Parent.Parent;
                }
                else if (((Control)sender).Parent.Parent.Parent.Parent is Form)
                {
                    GetFocusControl(((Control)sender).Parent.Parent.Parent.Parent.Controls, index);
                    frm = (Form)((Control)sender).Parent.Parent.Parent.Parent;
                }
                 * */

                if (sControl is ComboBox)
                {
                    ComboBox cmb = ((ComboBox)sControl);
                    if (cmb.DroppedDown == false) cmb.DroppedDown = true;
                    AutoComplete(ref cmb, e, true);
                    if ((int)e.KeyChar == 13)
                        cmb.DroppedDown = false;

                    if (cmb.DataSource != null) { if (GetComboValue(cmb) == 0) return; }
                }

                if ((int)e.KeyChar == 13)
                {
                    int index = sControl.TabIndex + 1;
                    if (sControl.Name == "txtTaxPercent") index = 7;
                    else if (sControl.Name == "lstColor") index = 7;
                    else if (sControl.Name == "dgPrevRate") index = 13;
                    //else if (sControl.Name == "lstUOM") index = 550;
                    else if (sControl.Name == "lstItem") index = 550;
                    Form frm = GetParentForm(sControl);
                    Control ctrlFocus = GetFocusControlMain(frm.Controls, index);

                    if (ctrlFocus != null)
                    {
                        //ctrlFocus.Focus();
                        //if (ctrlFocus is ComboBox)
                        //{
                        //    if (((ComboBox)ctrlFocus).SelectedIndex <= 0)
                        //        ((ComboBox)ctrlFocus).SelectedIndex = 0;
                        //    if (GetComboValue(((ComboBox)ctrlFocus)) == 0)
                        //        ((ComboBox)ctrlFocus).DroppedDown = true;
                        //}
                    }
                }
                //if ((Keys)e.KeyChar == Keys.Escape)
                //{
                //    if (frm.Name != "PurchaseAE")

                //        frm.Close();
                //}
            }
        }

        private void ControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;
            if (e.KeyValue >= 49 && e.KeyValue <= 57 && e.Control)
            {
                for (int i = 1; i <= 9; i++)
                {
                    int index = (int)Convert.ToChar(i.ToString());
                    if (e.KeyValue == index && e.Control)
                    {
                        if (mMain.toolBar1.Buttons.Count >= i)
                            mMain.toolBar1_ButtonClick(sender, new ToolBarButtonClickEventArgs(mMain.toolBar1.Buttons[i - 1]));
                    }
                }
            }
            else if (e.KeyCode == Keys.F1)
            {
                ShowHelp();
            }
            else if (e.KeyCode == Keys.C && e.Control && e.Alt)
            {
                DisplayCalc();
            }
            else if (e.KeyCode == Keys.X && e.Control)
            {
                Form frm = null;

                if (((Control)sender).Parent is Form)
                    frm = (Form)((Control)sender).Parent;
                else if (((Control)sender).Parent.Parent is Form)
                    frm = (Form)((Control)sender).Parent.Parent;
                else if (((Control)sender).Parent.Parent.Parent is Form)
                    frm = (Form)((Control)sender).Parent.Parent.Parent;
                else if (((Control)sender).Parent.Parent.Parent.Parent is Form)
                    frm = (Form)((Control)sender).Parent.Parent.Parent.Parent;
                frm.Close();
            }
            else if (e.KeyCode == Keys.Escape && sender is DataGridView)
            {
                //Control sControl = (Control)sender;
                //int index = sControl.TabIndex + 1;
                //Form frm = GetParentForm(sControl);
                //Control ctrlFocus = GetFocusControlMain(frm.Controls, index);

                //if (ctrlFocus != null)
                //{
                //    ctrlFocus.Focus();
                //    if (ctrlFocus is ComboBox)
                //    {
                //        if (((ComboBox)ctrlFocus).SelectedIndex <= 0)
                //            ((ComboBox)ctrlFocus).SelectedIndex = 0;
                //        if (GetComboValue(((ComboBox)ctrlFocus)) == 0)
                //            ((ComboBox)ctrlFocus).DroppedDown = true;
                //    }
                //}
            }
        }
        #endregion

        #region Help Related Methods
        Panel pnl = new Panel();
        Label lbl = new Label();
        public void SetHelpInfo(Form frm)
        {
            string FormName = frm.Name, strHelp = ""; //bool flag = false;
            for (int i = 0; i < dtHelp.Rows.Count; i++)
            {
                if (FormName.ToUpper() == dtHelp.Rows[i].ItemArray[0].ToString().ToUpper())
                    strHelp = dtHelp.Rows[i].ItemArray[1].ToString().ToUpper();
            }
            if (strHelp == "") strHelp = "Help Controls not available";


            pnl = new Panel();
            lbl = new Label();
            lbl.AutoSize = false;
            lbl.Text = strHelp;// "Save : F2   Exit: Esc   First:Ctrl+Right Arrow  Next:Ctrl+Left Arrow  Prev:Ctrl+Down Arrow  Last:Ctrl+Up Arrow";
            lbl.Paint += new PaintEventHandler(lbl_Paint);
            lbl.ForeColor = Color.Red; lbl.BorderStyle = BorderStyle.None;
            lbl.Font = GetFont();
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;
            pnl.Controls.Add(lbl);
            pnl.Dock = DockStyle.Bottom;
            pnl.Height = 30;
            pnl.BorderStyle = BorderStyle.None;
            pnl.Visible = false;
            pnl.BackColor = Color.FromArgb(255, 224, 192);
            frm.Controls.Add(pnl);
            pnl.BringToFront();

        }
        private void lbl_Paint(object sender, PaintEventArgs e)
        {
            Pen penBorder = new Pen(Color.MidnightBlue, 2);
            Rectangle rectBorder = new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            e.Graphics.DrawRectangle(penBorder, rectBorder);
        }
        private void ShowHelp()
        {
            if (pnl.Visible == false)
            {
                pnl.Visible = true;
            }
            else
            {
                pnl.Visible = false;
            }
        }
        #endregion

        public void SetAppSettings()
        {
            dtAppSettings = GetDataView("SELECT * FROM MSettings order by PkSettingNo").Table;
        }

        public string GetAppSettings(AppSettings app)
        {
            string expression = "PkSettingNo=" + (int)app;
            string strVal = "";
            try
            {
                if (dtAppSettings.Rows.Count > 0)
                {
                    DataRow[] result = dtAppSettings.Select(expression);
                    strVal = result[0].ItemArray[3].ToString();
                }
            }
            catch (Exception e)
            {
                strVal = "";
                CommonFunctions.ErrorMessge = e.Message;
            }
            return strVal;
        }

        public string GetAppSettings(long No)
        {
            string expression = "PkSettingNo=" + No;
            string strVal = "";
            try
            {
                if (dtAppSettings.Rows.Count > 0)
                {
                    DataRow[] result = dtAppSettings.Select(expression);
                    strVal = result[0].ItemArray[3].ToString();
                }
            }
            catch (Exception e)
            {
                strVal = "";
                CommonFunctions.ErrorMessge = e.Message;
            }
            return strVal;
        }

        public string GetAppSettingsLabel(AppSettings app)
        {
            string expression = "PkSettingNo=" + (int)app;
            string strVal = "";
            try
            {
                if (dtAppSettings.Rows.Count > 0)
                {
                    DataRow[] result = dtAppSettings.Select(expression);
                    strVal = result[0].ItemArray[1].ToString();
                }
            }
            catch (Exception e)
            {
                strVal = "";
                CommonFunctions.ErrorMessge = e.Message;
            }
            return strVal;
        }

        public void SetReportPath()
        {
            if (Application.StartupPath.IndexOf("bin") > 0)
                ReportPath = Application.StartupPath.Replace("\\bin\\Debug", "");
            else
                ReportPath = Application.StartupPath;
            ReportPath += "\\Reports\\";
        }

        public void GetTimeUserPasswords()
        {
            //bool flag = false;
            StreamReader objreader = null;
            string fname = Application.StartupPath + "\\Security.dat";
            try
            {
                if (File.Exists(fname) == true)
                {
                    objreader = new StreamReader(fname);
                    LUserID = secure.psDecrypt(objreader.ReadLine());
                    LPassword = secure.psDecrypt(objreader.ReadLine());
                    objreader.Close();
                    objreader = null;
                }
                else
                {
                    LUserID = "";
                    LPassword = "";
                }
            }
            catch
            {
                LUserID = "";
                LPassword = "";
            }
        }

        public static DateTime GetTime()
        {
            OMControls.ServerDateTime sdt = new OMControls.ServerDateTime();
            //if (LUserID != "" && LPassword != "")
            //{

            Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
            DateTime dtServer = ObjQry.ReturnDate("Select GetDate()", ConStr);
            //DateTime dtServer = sdt.GetTime(ServerName.Replace("\\SQLEXPRESS", ""), LUserID, LPassword);
            if (dtServer.ToString("dd-MMM-yyyy") == "01-Jan-1900")
                return DateTime.Now;
            else
                return dtServer;
            //}
            //else
            //    return DateTime.Now;
        }

        public Font GetLangFont()
        {
            Font FT = GetFont();
            DataTable dtLang = GetDataView("Select FontName,FontSize,FontBold From MLanguage Where LanguageNo=" + GetAppSettings(AppSettings.O_Language) + "").Table;
            if (dtLang.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtLang.Rows[0].ItemArray[2].ToString()) == true)
                    FT = new Font(dtLang.Rows[0].ItemArray[0].ToString(), Convert.ToInt64(dtLang.Rows[0].ItemArray[1].ToString()), FontStyle.Bold);
                else
                    FT = new Font(dtLang.Rows[0].ItemArray[0].ToString(), Convert.ToInt64(dtLang.Rows[0].ItemArray[1].ToString()), FontStyle.Regular);
            }
            return FT;
        }

        public string ChecklLangVal(string strVal)
        {
            string[] strSplit = { " " };
            string[] strEng = strVal.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);
            string sql = "", strReturn = "";
            int LangVal = Convert.ToInt32(GetAppSettings(AppSettings.O_Language));
            while (true)
            {
                string strPending = "";
                string strLang = "";
                for (int i = 0; i < strEng.Length; i++)
                {

                    sql = "Select PkSrNo, EnglishVal, MarathiVal, HindiVal,KarnatakaVal From MLanguageDictionary Where EnglishVal = '" + strEng[i].Replace("'", "''") + "'";
                    DataTable dt = GetDataView(sql).Table;
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][LangVal].ToString() != "")
                            strLang += " " + dt.Rows[0][LangVal].ToString();
                        else
                            strPending = strEng[i];
                    }
                    else if (strPending.Length == 0)
                    {
                        strPending = strEng[i];
                    }
                }

                if (strPending.Length > 0)
                {
                    strReturn = "";
                    break;
                }
                else
                {
                    strReturn = strLang.Trim();
                    break;
                }
            }
            return strReturn;
        }

        public void ExceptionDisplay(string strError)
        {
            try
            {
                if (GetAppSettings(AppSettings.O_IsExceptionDisplay) != "")
                {
                    if (Convert.ToBoolean(GetAppSettings(AppSettings.O_IsExceptionDisplay)) == true)
                    {
                        MessageBox.Show(strError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (GetAppSettings(AppSettings.O_IsExceptionDisplay) != "")
                    {
                        if (Convert.ToBoolean(GetAppSettings(AppSettings.O_IsExceptionDisplay)) == true)
                        {
                            MessageBox.Show(strError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ErrorMessge = e.Message;
                        }
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void SetVouchers(DataTable dtSetVouchers, long PkVoucherTrnNo)
        {
            for (int i = 0; i < dtSetVouchers.Rows.Count; i++)
            {
                if (dtSetVouchers.Rows[i].ItemArray[0].ToString() == PkVoucherTrnNo.ToString())
                {
                    dtSetVouchers.Rows[i][2] = "1";
                    dtSetVouchers.AcceptChanges();
                    return;
                }
            }
        }

        public void FillDiscType(ComboBox cmb)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", Type.GetType("System.Int32")); dt.Columns.Add("Desc");
            DataRow dr = dt.NewRow();
            //dr[0] = "0";
            //dr[1] = " ------ Select ------ ";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Percent";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Rupees";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "Product";
            dt.Rows.Add(dr);

            cmb.DisplayMember = dt.Columns[1].ColumnName;
            cmb.ValueMember = dt.Columns[0].ColumnName;
            cmb.DataSource = dt;
            cmb.SelectedIndex = 0;
        }

        public bool CheckAllowMenu(long value)
        {
            bool MenuFlag = false;
            GetToolStripItems(DBGetVal.MainForm.menuStrip.Items, value, out MenuFlag);
            if (MenuFlag == false)
                OMMessageBox.Show("This Function not allowed", ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
            return MenuFlag;
        }

        public void GetToolStripItems(ToolStripItemCollection dropDownItems, long value, out bool flag)
        {
            flag = false;
            foreach (ToolStripItem item in dropDownItems)
            {
                if (item.Name == value.ToString())
                {
                    flag = true;
                    break;
                }
                if (item is ToolStripMenuItem)
                {
                    ToolStripMenuItem it = (ToolStripMenuItem)item;
                    if (it.HasDropDownItems)
                    {
                        if (flag == true) break;
                        flag = false;
                        GetToolStripItems(it.DropDownItems, value, out flag);
                    }
                }
            }
        }

        public void CheckVersion()
        {
            try
            {
                string AppRegVer = ObjQry.ReturnString("Select AppVersion From MSetting", ConStr);
                if (secure.psDecrypt(AppRegVer) != new DBAssemblyInfo().AssemblyVersion)
                {
                    OMMessageBox.Show("Application Version is Incorrect." + Environment.NewLine + "Please contact HelpDesk.", ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            catch
            (Exception exc)
            {
                OMMessageBox.Show("Application Version is Incorrect." + Environment.NewLine + "Please contact Help Desk.", ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                CommonFunctions.ErrorMessge = exc.Message;
                Application.Exit();
            }
        }

        public void ExecuteScript(string str)
        {
            try
            {
                string strVersion = str;
                string FileScript = Application.StartupPath + "\\Yadi.txt";
                string strData = "";

                if (File.Exists(FileScript) == true)
                {
                    bool isAllOk = true;
                    System.IO.StreamReader rd = new StreamReader(FileScript);
                    strData = rd.ReadToEnd();
                    rd.Close();

                    string[] strSplitVersion = { "--<EndVersion = " + strVersion + ">" };
                    string[] strScriptsVersion = strData.Split(strSplitVersion, StringSplitOptions.None);
                    string[] strSplit = { "<BREAK>" };

                    if (strVersion != "1.0.0.0")
                    {
                        string[] strScripts = strScriptsVersion[1].Split(strSplit, StringSplitOptions.None);
                        for (int j = 0; j < strScripts.Length; j++)
                        {
                            string ErrorScript = Application.StartupPath + "\\ErrorScript.txt";
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = strScripts[j];

                            if (objT.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == false)
                            {
                                if (File.Exists(ErrorScript) == true)
                                {
                                    System.IO.StreamWriter sr = File.AppendText(ErrorScript);
                                    sr.WriteLine("Error Start =========\n " + objT.ErrorMessage + "\n=====" + cmd.CommandText + "\n Error End ========================" + " Vriha iTech" + System.DateTime.Now);
                                    sr.Close();
                                }
                                else
                                {
                                    System.IO.StreamWriter sr = File.CreateText(Application.StartupPath + "\\ErrorScript.txt");
                                    sr = File.AppendText(ErrorScript);
                                    sr.WriteLine("Error Start =========\n " + objT.ErrorMessage + "\n=====" + cmd.CommandText + "\n Error End ========================" + " Vriha iTech" + System.DateTime.Now);
                                    sr.Close();
                                }
                                isAllOk = false;
                                //  break;
                            }
                        }
                        if (isAllOk)
                        {
                            File.Delete(FileScript);
                        }
                    }
                    else
                    {
                        string[] strScripts = strData.Split(strSplit, StringSplitOptions.None);

                        for (int j = 0; j < strScripts.Length; j++)
                        {
                            string ErrorScript = Application.StartupPath + "\\ErrorScript.txt";
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = strScripts[j];

                            if (objT.ExecuteNonQuery(cmd, CommonFunctions.ConStr) == false)
                            {
                                if (File.Exists(ErrorScript) == true)
                                {
                                    System.IO.StreamWriter sr = File.AppendText(ErrorScript);
                                    sr.WriteLine("Error Start =========\n " + objT.ErrorMessage + "\n=====" + cmd.CommandText + "\n Error End ========================" + " Vriha iTech" + System.DateTime.Now + "\n");
                                    sr.Close();
                                }
                                else
                                {
                                    System.IO.StreamWriter sr = File.CreateText(Application.StartupPath + "\\ErrorScript.txt");
                                    sr = File.AppendText(ErrorScript);
                                    sr.WriteLine("Error Start =========\n " + objT.ErrorMessage + "\n=====" + cmd.CommandText + "\n Error End ========================" + " Vriha iTech" + System.DateTime.Now + "\n");
                                    sr.Close();
                                }
                                isAllOk = false;
                                //  break;
                            }
                        }
                        if (isAllOk)
                        {
                            File.Delete(FileScript);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        public string GetPrinterName(long MfgCompNo)
        {
            try
            {
                string strName = ObjQry.ReturnString("Select PrinterName From MFirm Where FirmNo=" + DBGetVal.FirmNo + "", ConStr);
                if (strName == "")
                {
                    System.Drawing.Printing.PrinterSettings objPrint = new System.Drawing.Printing.PrinterSettings();
                    strName = objPrint.PrinterName;
                }
                return strName;
            }
            catch (Exception exc)
            {
                ExceptionDisplay(exc.Message);
                return "";
            }
        }
    }
    public class DBGetFlag
    {
        private static bool mBrand;
        private static bool mCutomer;
        private static bool mSupplier;
        private static bool mCity;
        private static bool mState;
        private static bool mItemmaster = false;
        public static bool Brand
        {
            get { return mBrand; }
            set { mBrand = value; }
        }
        public static bool Itemmaster
        {
            get { return mItemmaster; }
            set { mItemmaster = value; }
        }
        public static bool Customer
        {
            get { return mCutomer; }
            set { mCutomer = value; }
        }
        public static bool Supplier
        {
            get { return mSupplier; }
            set { mSupplier = value; }
        }
        public static bool City
        {
            get { return mCity; }
            set { mCity = value; }
        }
        public static bool State
        {
            get { return mState; }
            set { mState = value; }
        }
    }
    public class DBGetVal
    {
        private static MDIParent1 mMainForm;
        private static string mFirmName;
        private static int mFirmNo;
        private static int mUID;
        private static string mUserName;
        private static DateTime mFromDate;
        private static DateTime mToDate;
        private static string mDBName;
        private static Form mNewCustForm;
        private static string mDirectoryPath;
        private static bool mIsAdmin;
        private static string mCompanyAddress;
        private static string mMacID;
        private static string mHostName;
        private static System.Net.IPAddress mMachineIPAddress;
        private static long mMacNo;
        private static long mReportDisType = 2;
        private static DateTime mServerTime;
        private static bool mKachhaFirm;
        private static string mEmail;
        private static string mEmailPass;


        public static MDIParent1 MainForm
        {
            get { return mMainForm; }
            set { mMainForm = value; }
        }
        public static string FirmName
        {
            get { return mFirmName; }
            set { mFirmName = value; }
        }
        public static int UserID
        {
            get
            {
                return mUID;
            }
            set
            {
                mUID = value;
            }
        }
        public static string UserName
        {
            get { return mUserName; }
            set { mUserName = value; }
        }
        public static int FirmNo
        {
            get { return mFirmNo; }
            set { mFirmNo = value; }
        }
        public static DateTime FromDate
        {
            get { return mFromDate; }
            set { mFromDate = value; }
        }
        public static DateTime ToDate
        {
            get { return mToDate; }
            set { mToDate = value; }
        }
        public static string DBName
        {
            get { return mDBName; }
            set { mDBName = value; }
        }
        public static string RegCompName
        {
            get
            {
                DBAssemblyInfo db = new DBAssemblyInfo();
                return "Powered By " + db.AssemblyCompany;
            }
        }

        public static Form NewCustForm
        {
            get { return mNewCustForm; }
            set { mNewCustForm = value; }
        }
        public static string DirectoryPath
        {
            get { return mDirectoryPath; }
            set { mDirectoryPath = value; }
        }
        public static bool IsAdmin
        {
            get { return mIsAdmin; }
            set { mIsAdmin = value; }
        }
        public static string CompanyAddress
        {
            get { return mCompanyAddress; }
            set { mCompanyAddress = value; }
        }
        public static string MacID
        {
            get { return mMacID; }
            set { mMacID = value; }
        }
        public static System.Net.IPAddress MachineIPAddress
        {
            get { return mMachineIPAddress; }
            set { mMachineIPAddress = value; }
        }
        public static string HostName
        {
            get { return mHostName; }
            set { mHostName = value; }
        }
        public static long MacNo
        {
            get { return mMacNo; }
            set { mMacNo = value; }
        }
        public static long ReportDisType
        {
            get { return mReportDisType; }
        }
        public static DateTime ServerTime
        {
            get { return mServerTime; }
            set { mServerTime = value; }
        }
        public static bool KachhaFirm
        {
            get { return mKachhaFirm; }
            set { mKachhaFirm = value; }
        }
        public static string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public static string EmailPass
        {
            get { return mEmailPass; }
            set { mEmailPass = value; }
        }

    }

    public static class DBCellValue
    {
        public static string mRate;
        public static string mQuanity;
        public static string mDisc;


        public static string Rate
        {
            get { return mRate; }
            set { mRate = value; }
        }
        public static string Quanity
        {
            get { return mQuanity; }
            set { mQuanity = value; }
        }
        public static string Disc
        {
            get { return mDisc; }
            set { mDisc = value; }
        }
    }

    public static class GroupType
    {
        public static long CapitalAccount
        {
            get { return 1; }
        }
        public static long LoansLiabilities
        {
            get { return 2; }
        }
        public static long CurrentLiabilities
        {
            get { return 3; }
        }
        public static long FixedAssets
        {
            get { return 4; }
        }
        public static long Investments
        {
            get { return 5; }
        }
        public static long CurrentAssets
        {
            get { return 6; }
        }
        public static long BranchDivision
        {
            get { return 7; }
        }
        public static long MiscExpensesAssets
        {
            get { return 8; }
        }
        public static long SuspenseAccount
        {
            get { return 9; }
        }
        public static long SalesAccount
        {
            get { return 10; }
        }
        public static long PurchaseAccount
        {
            get { return 11; }
        }
        public static long DirectIncome
        {
            get { return 12; }
        }
        public static long DirectExpenses
        {
            get { return 13; }
        }
        public static long IndirectIncome
        {
            get { return 14; }
        }
        public static long InDirectExpenses
        {
            get { return 15; }
        }
        public static long ReserveAndSurplus
        {
            get { return 16; }
        }
        public static long BankODAccount
        {
            get { return 17; }
        }
        public static long SecuredLoans
        {
            get { return 18; }
        }
        public static long UnSecuredLoans
        {
            get { return 19; }
        }
        public static long DutiesAndTaxes
        {
            get { return 20; }
        }
        public static long Provisions
        {
            get { return 21; }
        }
        public static long SundryCreditors
        {
            get { return 22; }
        }
        public static long StockInhand
        {
            get { return 23; }
        }
        public static long DepositAssets
        {
            get { return 24; }
        }
        public static long LoansAndAdvanceAssets
        {
            get { return 25; }
        }
        public static long SundryDebtors
        {
            get { return 26; }
        }
        public static long CashInhand
        {
            get { return 27; }
        }
        public static long BankAccounts
        {
            get { return 28; }
        }
        public static long Primary
        {
            get { return 29; }
        }
        public static long Transporter
        {
            get { return 30; }
        }
        public static long SECURED
        {
            get { return 31; }
        }
        public static long VAT
        {
            get { return 32; }
        }
        public static long GST
        {
            get { return 33; }
        }
        public static long CForm
        {
            get { return 34; }
        }
        public static long CST
        {
            get { return 35; }
        }
        public static long ServiceTax
        {
            get { return 36; }
        }
        public static long Broker
        {
            get { return 37; }
        }
        public static long AdminExpenses
        {
            get { return 43; }
        }

        public static long SGST
        {
            get { return 51; }
        }
        public static long CGST
        {
            get { return 52; }
        }
        public static long IGST
        {
            get { return 53; }
        }
        public static long Cess
        {
            get { return 54; }
        }



    }

    public static class VchType
    {
        public static long Contra
        {
            get { return 1; }
        }
        public static long CreditNote
        {
            get { return 2; }
        }
        public static long DebitNote
        {
            get { return 3; }
        }
        public static long DeliveryNote
        {
            get { return 4; }
        }
        public static long Journal
        {
            get { return 5; }
        }
        public static long Memorandum
        {
            get { return 6; }
        }
        public static long Payment
        {
            get { return 7; }
        }
        public static long PhysicalStock
        {
            get { return 8; }
        }
        public static long Purchase
        {
            get { return 9; }
        }
        public static long PurchaseOrder
        {
            get { return 10; }
        }
        public static long Receipt
        {
            get { return 11; }
        }
        public static long RejectionIn
        {
            get { return 12; }
        }
        public static long RejectionOut
        {
            get { return 13; }
        }
        public static long ReversingJournal
        {
            get { return 14; }
        }
        public static long Sales
        {
            get { return 15; }
        }
        public static long SalesOrder
        {
            get { return 16; }
        }
        public static long StockJournal
        {
            get { return 17; }
        }
        public static long GRNEntry
        {
            get { return 18; }
        }
        public static long InternalTransfer
        {
            get { return 19; }
        }
        public static long DeliveryChallan
        {
            get { return 20; }
        }
        public static long PurchaseVoucher
        {
            get { return 21; }
        }
        public static long SalesVoucher
        {
            get { return 22; }
        }
        public static long StockInward
        {
            get { return 23; }
        }
        public static long StockOutward
        {
            get { return 24; }
        }
        public static long CashDepositeInBank
        {
            get { return 25; }
        }
        public static long CashWithdrawalFromBank
        {
            get { return 26; }
        }
        public static long CashReceipt
        {
            get { return 27; }
        }
        public static long BankReceipt
        {
            get { return 28; }
        }
        public static long BankPayment
        {
            get { return 29; }
        }
        public static long SalesReceipt
        {
            get { return 30; }
        }
        public static long PurchasePayment
        {
            get { return 31; }
        }
        public static long StockTransfer
        {
            get { return 32; }
        }
        public static long ExpensesEntry
        {
            get { return 33; }
        }
        public static long CashPayment
        {
            get { return 34; }
        }
        public static long StockAssembly
        {
            get { return 35; }
        }
        public static long OpeningBalance
        {
            get { return 36; }
        }

        public static long SalesDC
        {
            get { return 37; }
        }

        public static long DContra
        {
            get { return 101; }
        }
        public static long DCreditNote
        {
            get { return 102; }
        }
        public static long DDebitNote
        {
            get { return 103; }
        }
        public static long DDeliveryNote
        {
            get { return 104; }
        }
        public static long DJournal
        {
            get { return 105; }
        }
        public static long DMemorandum
        {
            get { return 106; }
        }
        public static long DPayment
        {
            get { return 107; }
        }
        public static long DPhysicalStock
        {
            get { return 108; }
        }
        public static long DPurchase
        {
            get { return 109; }
        }
        public static long DPurchaseOrder
        {
            get { return 110; }
        }
        public static long DReceipt
        {
            get { return 111; }
        }
        public static long DRejectionIn
        {
            get { return 112; }
        }
        public static long DRejectionOut
        {
            get { return 113; }
        }
        public static long DReversingJournal
        {
            get { return 114; }
        }
        public static long DSales
        {
            get { return 115; }
        }
        public static long DSalesOrder
        {
            get { return 116; }
        }
        public static long DStockJournal
        {
            get { return 117; }
        }
        public static long DGRNEntry
        {
            get { return 118; }
        }
        public static long DInternalTransfer
        {
            get { return 119; }
        }
        public static long DDeliveryChallan
        {
            get { return 120; }
        }
        public static long DPurchaseVoucher
        {
            get { return 121; }
        }
        public static long DSalesVoucher
        {
            get { return 122; }
        }
        public static long DStockInward
        {
            get { return 123; }
        }
        public static long DStockOutward
        {
            get { return 124; }
        }
        public static long DCashDepositeInBank
        {
            get { return 125; }
        }
        public static long DCashWithdrawalFromBank
        {
            get { return 126; }
        }
        public static long DCashReceipt
        {
            get { return 127; }
        }
        public static long DBankReceipt
        {
            get { return 128; }
        }
        public static long DBankPayment
        {
            get { return 129; }
        }
        public static long DSalesReceipt
        {
            get { return 130; }
        }
        public static long DPurchasePayment
        {
            get { return 131; }
        }
        public static long DStockTransfer
        {
            get { return 132; }
        }
        public static long DExpensesEntry
        {
            get { return 133; }
        }
        public static long DCashPayment
        {
            get { return 134; }
        }
        public static long DStockAssembly
        {
            get { return 135; }
        }
        public static long DOpeningBalance
        {
            get { return 136; }
        }
        public static long Quotation
        {
            get { return 215; }
        }
    }

    public static class Others
    {
        public static long Party = 501;
        public static long Discount1 = 502;
        public static long Discount2 = 503;
        public static long Discount3 = 504;
        public static long Discount4 = 505;
        public static long Charges1 = 506;// taxable charges
        public static long Charges2 = 507;
        public static long RoundOff = 508;
        public static long ItemDisc = 509;
        public static long BTaxItemDisc = 510;
        public static long Charges3 = 511;
        public static long Charges4 = 512;
        public static long DisplayDiscount = 513;
        public static long ChargesTax = 514; // tax on charges
        public static long ChargesAddTax = 515;


        public static long SGSTLedgerno = 516;
        public static long CGSTLedgerno = 517;
        public static long IGSTLedgerno = 518;
        public static long CessLedgerno = 519;
    }

    public static class SchemeType
    {
        public static long MTD = 1;
        public static long TVB = 2;
        public static long TSKU = 3;
        public static long TSKUC = 4;
        public static long PSKU = 5;
    }

    public static class StockCountType
    {
        public static long NA = 1;
        public static long Daily = 2;
        public static long Weekly = 3;
        public static long Monthly = 4;
        public static long Yearly = 5;
    }

    public static class BarcodePrinterType
    {
        public static long TSC = 1;
        public static long Godex = 2;
        public static long Argox = 3;
    }

    public static class OtherVchType
    {
        public static long PurchaseOrder
        {
            get { return 1; }
        }
        public static long SalesOrder
        {
            get { return 2; }
        }
    }

    public enum AppSettings
    {
        S_PartyAC = 1,
        S_TaxType = 2,
        S_Transporter = 3,
        S_Discount1 = 4,
        S_Discount2 = 5,
        S_Discount3 = 6,
        S_InterestAcc = 7,
        S_Charges1 = 8,
        S_ChargeLabelName = 9,
        S_Charges2 = 10,
        S_RoundOfAcc = 12,
        P_PartyAC = 13,
        P_TaxType = 14,
        P_Transporter = 15,
        P_Discount1 = 16,
        P_InterestAcc = 17,
        P_DisplayDiscount = 19,
        P_Charges1 = 20,
        P_Charges2 = 21,
        P_Charges2Display = 22,
        P_RoundOfAcc = 24,
        S_StopOnRate = 25,
        S_StopOnQty = 26,
        S_IsBarcodeEnabled = 27,
        S_AllowsDuplicatesItems = 28,
        S_ItemNameType = 29,
        S_Rate = 30,
        S_SubAmount = 31,
        S_TaxAmount = 32,
        S_Qty = 33,
        S_GrandTotal = 34,
        P_Rate = 35,
        P_SubAmount = 36,
        P_TaxAmount = 37,
        P_Qty = 38,
        P_GrandTotal = 39,
        P_PurchaseAcc = 40,
        S_IsReverseRateCalc = 41,
        P_IsReverseRateCalc = 42,
        ARateLabel = 43,
        ARateIsActive = 44,
        BRateLabel = 45,
        BRateIsActive = 46,
        CRateLabel = 47,
        CRateIsActive = 48,
        DRateLabel = 49,
        DRateIsActive = 50,
        ERateLabel = 51,
        ERateIsActive = 52,
        S_ItemDisc = 53,
        P_ATaxItemDisc = 54,
        S_Discount1Display = 55,
        S_Charges1Display = 59,
        S_Charges2Display = 60,
        S_StopOnDate = 63,
        S_StopOnParty = 64,
        S_StopOnRateType = 65,
        S_StopOnTaxType = 66,
        S_StopOnGrid = 95,
        S_IsShowSalesHistoryEnabled = 96,
        S_IsShowPurchaseHistoryEnabled = 97,
        S_IsUseLastPartyWiseDiscEnabled = 98,
        S_IsStopOnSaleHistoryListEnabled = 99,
        S_Rate_DecimalDigits = 100,
        S_Subtotal_DecimalDigits = 101,
        S_Grandtotal_DecimalDigits = 102,
        S_TaxAmount_DecimalDigits = 103,
        S_TaxItemWise_DecimalDigits = 104,
        S_DiscountAmount_DecimalDigits = 105,
        S_DiscountItemWise_DecimalDigits = 106,
        S_Qty_DecimalDigits = 107,
        S_Rate_RoundOffDigits = 108,
        S_Subtotal_RoundOffDigits = 109,
        S_Grandtotal_RoundOffDigits = 110,
        S_TaxAmount_RoundOffDigits = 111,
        S_TaxItemWise_RoundOffDigits = 112,
        S_DiscountAmount_RoundOffDigits = 113,
        S_DiscountItemWise_RoundOffDigits = 114,
        S_Qty_RoundOffDigits = 115,
        S_Rate_RoundOffType = 116,
        S_Subtotal_RoundOffType = 117,
        S_Grandtotal_RoundOffType = 118,
        S_TaxAmount_RoundOffType = 119,
        S_TaxItemWise_RoundOffType = 120,
        S_DiscountAmount_RoundOffType = 121,
        S_DiscountItemWise_RoundOffType = 122,
        S_Qty_RoundOffType = 123,
        P_Rate_DecimalDigits = 124,
        P_Subtotal_DecimalDigits = 125,
        P_Grandtotal_DecimalDigits = 126,
        P_TaxAmount_DecimalDigits = 127,
        P_TaxItemWise_DecimalDigits = 128,
        P_DiscountAmount_DecimalDigits = 129,
        P_DiscountItemWise_DecimalDigits = 130,
        P_Qty_DecimalDigits = 131,
        P_Rate_RoundOffDigits = 132,
        P_Subtotal_RoundOffDigits = 133,
        P_Grandtotal_RoundOffDigits = 134,
        P_TaxAmount_RoundOffDigits = 135,
        P_TaxItemWise_RoundOffDigits = 136,
        P_DiscountAmount_RoundOffDigits = 137,
        P_DiscountItemWise_RoundOffDigits = 138,
        P_Qty_RoundOffDigits = 139,
        P_Rate_RoundOffType = 140,
        P_Subtotal_RoundOffType = 141,
        P_Grandtotal_RoundOffType = 142,
        P_TaxAmount_RoundOffType = 143,
        P_TaxItemWise_RoundOffType = 144,
        P_DiscountAmount_RoundOffType = 145,
        P_DiscountItemWise_RoundOffType = 146,
        P_Qty_RoundOffType = 147,
        S_RateType = 148,
        S_ShowRateHistoryAutomatically = 149,
        S_HideRatePopupAutomatically = 150,
        S_HideRatePopupAutomatically_Seconds = 151,
        S_OutwardLocation = 152,
        O_DepartmentDisplay = 153,
        O_CategoryDisplay = 154,
        O_BarCodeDisplay = 155,
        O_StockLocation = 156,

        S_IsAllowSingleFirmChq = 161,
        S_IsAllowMultipleChq = 162,
        S_IsDisplayRateType = 163,
        S_RateTypeAskPassword = 164,
        ARatePassword = 165,
        ARateDBEffect = 166,
        BRatePassword = 167,
        BRateDBEffect = 168,
        CRatePassword = 169,
        CRateDBEffect = 170,
        DRatePassword = 171,
        DRateDBEffect = 172,
        ERatePassword = 173,
        ERateDBEffect = 174,

        ARateSuperMode = 209,
        BRateSuperMode = 210,
        CRateSuperMode = 211,
        DRateSuperMode = 212,
        ERateSuperMode = 213,
        P_StopOnRate = 214,
        P_StopOnQty = 215,
        P_IsBarCodeDisplay = 216,
        P_AllowsDuplicatesItems = 217,
        P_ItemNameType = 218,
        P_Discount1Display = 219,
        P_Charges1Display = 220,
        P_StopOnDate = 221,
        P_StopOnParty = 222,
        P_StopOnHeaderDisc = 223,
        P_StopOnTaxType = 224,
        P_StopOnGrid = 225,
        P_IsShowSalesHistoryEnabled = 226,
        P_IsShowPurchaseHistoryEnabled = 227,
        P_IsUseLastSaleRateEnabled = 228,
        P_IsStopOnSaleHistoryListEnabled = 229,
        P_RateType = 230,
        P_ShowRateHistoryAutomatically = 231,
        P_HideRatePopupAutomatically = 232,
        P_HideRatePopupAutomatically_Seconds = 233,
        P_OutwardLocation = 234,
        P_IsAllowSingleFirmChq = 235,
        P_IsAllowMultipleChq = 236,
        P_IsDisplayRateType = 237,
        P_RateTypeAskPassword = 238,
        P_IsBarcodeEnabled = 239,
        P_IsAllowsDuplicatesItemsInSameBill = 240,
        TaxTypeGridDisplay = 241,
        S_IsBillPrint = 242,
        ReportDisplay = 243,

        S_AskPayableAmount = 245,
        P_BTaxItemDisc = 246,
        Multiple_Firm_Accounting_Mode = 247,
        Allow_Cheque_Pay_Type = 248,
        O_PrintBarCode = 249,
        P_AllBarCodePrint = 250,
        IsExcelReport = 251,
        S_IsManualBillNo = 252,
        S_Charge2LabelName = 253,
        S_CreditCardDigitLimit = 254,
        O_TopSalesValue = 255,
        S_SettingValue = 256,
        S_FooterValue = 257,
        S_OrderType = 258,
        O_IsBrandFilter = 259,
        O_ShowLastBill = 260,

        S_Footer2Value = 262,
        AutoUploadData = 263,
        AutoUploadHrs = 264,
        AutoUploadMins = 265,
        O_Bilingual = 266,
        O_Language = 267,
        O_DefaultBillPrint = 268,
        OS_DefaultBillPrint=555,
        O_SOD = 270,
        O_EOD = 271,
        S_ShowSavingBill = 272,
        S_ShowOutStanding = 273,
        O_UpDownLoadPath = 274,
        O_UpDownTime = 275,
        AutoUploadType = 276,
        O_BackUpPath = 277,
        ZipUploadHrs = 278,
        ZipUploadMins = 279,
        O_IsExceptionDisplay = 280,
        S_IsFooterLevelDisc = 281,
        S_IsItemLevelDisc = 282,
        S_IsBillWithMRP = 283,
        S_IsAddressInBill = 284,
        S_IsBillRoundOff = 285,
        P_IsBillRoundOff = 286,
        S_IsAddressInBillHomeDelivery = 287,
        S_IsAddressInBillCouterBill = 288,
        P_AutoMFGMapping = 289,
        O_BarCodePrintType = 290,
        O_BillWithMRP = 291,
        O_UpDownLink = 292,
        O_SODUploadBackup = 293,
        S_ShowSchemeDetails = 294,
        S_DefaultPartyAC = 295,
        P_DefaultPartyAC = 296,
        O_ToolsSetupPath = 297,
        S_ShowRemark = 298,
        S_ShowVatNo = 299,
        S_AskHomeDelv = 300,
        O_EffectiveDate = 301,
        P_Charges3 = 302,
        P_Charges4 = 303,
        O_LBTSystem = 304,
        S_StopOnDisc = 305,
        S_IsPayTypewiseBillNo = 306,
        S_IsFirmPayTypewisePrint = 307,
        O_DeletePath = 308,
        S_PostFirmwise = 309,
        S_DirectEstimateBilling = 310,
        S_IsTransportInBill = 311,
        S_IsEachTimeInitialise = 312,
        S_IsPrintCount = 313,
        S_IsMRPDisplay = 314,
        S_IsCollectionPrint = 315,
        P_IsPaymentPrint = 316,
        S_IsRateHistoryMaintain = 317,
        S_IsCloseMRPManually = 318,
        S_IsDisplayTaxType = 319,
        S_IsRateVeriation = 320,
        O_AutogenerateBarcode = 321,
        S_IsAskUserPassword = 322,
        S_AfterSaveNotDeleteItem = 323,
        O_IsWeighingBarcode = 324,
        O_WeighingBarcodeChar = 325,
        O_AutogenerateBarcodeLength = 326,
        O_IsPayTypeChargesCalculation = 327,
        O_IsPrintScript = 328,
        O_IsPartyDisplayWithArea = 329,
        S_IsCreditBillUpdate = 330,
        S_CreditBillPassword = 331,
        S_DefaultPayType = 332,
        P_DefaultPayType = 333,
        S_IsBillUpdate = 334,
        S_BillUpdatePwd = 335,
        S_IsBillDelete = 336,
        S_BillDeletePwd = 337,
        O_IsIngredientBarcode = 338,
        O_IsNutritionBarcode = 339,
        O_IsReceipeBarcode = 340,
        P_IsCreditBillUpdate = 341,
        P_CreditBillPassword = 342,
        P_IsBillUpdate = 343,
        P_BillUpdatePwd = 344,
        S_IsBillPrintAsk = 345,
        S_ChargesOnTax = 346,
        S_ChargesOnTaxAcc = 347,
        P_ChargesOnTax = 348,
        P_ChargesOnTaxAcc = 349,
        S_IsNewAskUserID = 350,
        O_IsWeighingBarcodeKGwise = 351,
        S_ShowingLandedRate = 352,
        S_IsTaxInvoiceTypeShowInBill = 353,
        S_IsTaxInvoiceTypeWiseBillNo = 354,
        S_IsTaxInvoiceTypeRequired = 355,
        P_StopOnNetAmount = 356,
        P_StopOnRemark = 357,
        P_ChargesOnAddTaxAcc = 358,
        O_IsAllowCST = 359,
        O_IsAllowCForm = 360,
        S_IsTaxTypewiseBillNo = 361, //daywise bill series
        S_IsRateChangeByUser = 362,
        S_ShowAPMCDetails = 363,
        S_ShowSalesMan = 364,
        S_IsEwayBill = 365,
        P_StopOnDiscPer = 366,
        P_StopOnDiscRs = 367,
        P_StopOnCharge = 368,
        S_IsPartyWiseDisc = 369,//from mledger fixed disc and for history disc= setting no=98
        S_IsSMSSend = 370,
        S_IsEmailSend = 371,
        S_IsAutoSMSSend = 372,
        S_IsAutoEmailSend = 373,
        S_IsMobileShop = 374,
        P_IsBroker = 375,

        SB_SrNo = 401,
        SB_ItemName = 402,
        SB_GrossWt = 403,
        SB_TareWt = 404,
        SB_Quantity = 405,
        SB_UOM = 406,
        SB_Rate = 407,
        SB_PackingCharges = 408,
        SB_NetRate = 409,
        SB_DiscPercentage = 410,
        SB_DiscAmount = 411,
        SB_DiscRupees = 412,
        SB_DiscPercentage2 = 413,
        SB_DiscAmount2 = 414,
        SB_DiscRupees2 = 415,
        SB_NetAmt = 416,
        SB_Amount = 417,
        SB_Barcode = 418,
        SB_PkStockTrnNo = 419,
        SB_PkVoucherNo = 420,
        SB_ItemNo = 421,
        SB_UOMNo = 422,
        SB_PkRateSettingNo = 423,
        SB_MRP = 424,
        SB_StockFactor = 425,
        SB_ActualQty = 426,
        SB_MKTQuantity = 427,
        SB_SGSTPercentage = 428,
        SB_SGSTAmount = 429,
        SB_TaxLedgerNo = 430,
        SB_SalesLedgerNo = 431,
        SB_PkItemTaxInfo = 432,
        SB_SalesVchNo = 433,
        SB_TaxVchNo = 434,
        SB_CGSTPercentage = 435,
        SB_CGSTAmount = 436,
        SB_TaxLedgerNo2 = 437,
        SB_SalesLedgerNo2 = 438,
        SB_PkItemTaxInfo2 = 439,
        SB_SalesVchNo2 = 440,
        SB_TaxVchNo2 = 441,
        SB_IGSTPercentage = 442,
        SB_IGSTAmount = 443,
        SB_TaxLedgerNo3 = 444,
        SB_SalesLedgerNo3 = 445,
        SB_PkItemTaxInfo3 = 446,
        SB_SalesVchNo3 = 447,
        SB_TaxVchNo3 = 448,
        SB_StockCompanyNo = 449,
        SB_SchemeDetailsNo = 450,
        SB_SchemeFromNo = 451,
        SB_SchemeToNo = 452,
        SB_RewardFromNo = 453,
        SB_RewardToNo = 454,
        SB_ItemLevelDiscNo = 455,
        SB_FKItemLevelDiscNo = 456,
        SB_DisplayName = 457,
        SB_IsRateChange = 458,
        SB_HigherVariation = 459,
        SB_LowerVariation = 460,
        SB_TempRate = 461,
        SB_SONo = 462,
        SB_PkStockDetailsNo = 463,
        SB_FkOtherStockTrnNo = 464,
        SB_ESFlag = 465,
        SB_Remarks = 466,
        SB_SalesMan = 467,
        SB_Hamali = 468,

        PB_SrNo = 501,
        PB_ItemName = 502,
        PB_Quantity = 503,
        PB_UOM = 504,
        PB_Rate = 505,
        PB_NetRate = 506,
        PB_DiscPercentage = 507,
        PB_DiscAmount = 508,
        PB_DiscRupees = 509,
        PB_DiscPercentage2 = 510,
        PB_DiscAmount2 = 511,
        PB_DiscRupees2 = 512,
        PB_NetAmt = 513,
        PB_Amount = 514,
        PB_Barcode = 515,
        PB_PkStockTrnNo = 516,
        PB_PkVoucherNo = 517,
        PB_ItemNo = 518,
        PB_UOMNo = 519,
        PB_PkRateSettingNo = 520,
        PB_MRP = 521,
        PB_StockFactor = 522,
        PB_ActualQty = 523,
        PB_MKTQuantity = 524,
        PB_TaxPercentage = 525,
        PB_TaxAmount = 526,
        PB_TaxLedgerNo = 527,
        PB_SalesLedgerNo = 528,
        PB_PkItemTaxInfo = 529,
        PB_SalesVchNo = 530,
        PB_TaxVchNo = 531,
        PB_TaxPercentage2 = 532,
        PB_TaxAmount2 = 533,
        PB_TaxLedgerNo2 = 534,
        PB_SalesLedgerNo2 = 535,
        PB_PkItemTaxInfo2 = 536,
        PB_SalesVchNo2 = 537,
        PB_TaxVchNo2 = 538,
        PB_TaxPercentage3 = 539,
        PB_TaxAmount3 = 540,
        PB_TaxLedgerNo3 = 541,
        PB_SalesLedgerNo3 = 542,
        PB_PkItemTaxInfo3 = 543,
        PB_SalesVchNo3 = 544,
        PB_TaxVchNo3 = 545,
        PB_StockCompanyNo = 546,
        PB_IsRateChange = 547,
        PB_PONo = 548,
        PB_PkStockDetailsNo = 549,
        PB_FkOtherStockTrnNo = 550,
        PB_FreeQty = 551,
        PB_FreeUOMName = 552,
        PB_BarCodePrinting = 553,
        PB_FreeUOMNo = 554,
        S_CashPosting= 556,

    }
}