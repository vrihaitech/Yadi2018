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

namespace Yadi.Master
{
    public partial class UserAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMUser dbUser = new DBMUser();
        DBMUser user = new DBMUser();
        Security secure = new Security();
        MUser mUser = new MUser();
        MUserMenuMaster mUserMenu = new MUserMenuMaster();
       
        public static string UserNm;      
        bool NodeCheckFlag = true;
        string[] strNode; int cntRow = 0, cntCol = 0;

        public UserAE()
        {
            InitializeComponent();            
        }

        private void UserAE_Load(object sender, EventArgs e)
        {
            try
            {
                //TxtUserCode.Text = ObjQry.ReturnString("Select Isnull(Max(UserCode) , 0 ) + 1 from MUser", CommonFunctions.ConStr);
                //ObjFunction.FillCombo(cmbCityName, "Select CityNo,CityName From MCity Where IsActive='true'");
                //ObjFunction.FillCombo(cmbAccYear, "SELECT     AccYearNo, AccYearName FROM MAccYear");
                //ObjFunction.FillCombo(cmbCompanyName, "SELECT FirmNo, FirmName FROM MFirm");
                //ObjFunction.FillCombo(cmbLocationName, "SELECT LocationNo, LocationName FROM MLocation Where IsActive='true'");
                formatpicture();
                FillListAll();
                if (User.RequestUserNo != 0)
                {
                    UserNm = "";
                    FillControls(User.RequestUserNo);
                    if (DBGetVal.UserID != 1)
                    {
                        TVW.Enabled = false;
                        btnClear.Visible = false;
                        rdAdmin.Enabled = false; rdUser.Enabled = false; //rdAgent.Enabled = false;
                        chkIsClose.Enabled = false;
                    }
                    DataTable dtTree = ObjFunction.GetDataView("SELECT * FROM MUserMenuMaster where FKUserId =" + User.RequestUserNo + "").Table;
                    strNode = new string[dtTree.Columns.Count - 4];
                    for (int i = 0; i < dtTree.Columns.Count - 4; i++)
                    {
                        strNode[i] = secure.psDecrypt(dtTree.Rows[0].ItemArray[i + 2].ToString());
                    }
                    DisplayNodes(new TreeNode(), 0);
                }
                else
                {
                    if (DBGetVal.UserID > 1)
                    {
                        TVW.Enabled = false;
                        btnClear.Visible = false;
                        rdAdmin.Enabled = false; rdUser.Enabled = false; //rdAgent.Enabled = false;
                        chkIsClose.Enabled = false;

                        UserNm = "";
                        FillControls(DBGetVal.UserID);

                        DataTable dtTree = ObjFunction.GetDataView("SELECT * FROM MUserMenuMaster where FKUserId =" + DBGetVal.UserID + "").Table;
                        strNode = new string[dtTree.Columns.Count - 4];
                        for (int i = 0; i < dtTree.Columns.Count - 4; i++)
                        {
                            strNode[i] = secure.psDecrypt(dtTree.Rows[0].ItemArray[i + 2].ToString());
                        }
                        DisplayNodes(new TreeNode(), 0);
                    }
                    else
                        InitNodes(new TreeNode(), 0);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillListAll()
        {
            ObjFunction.FillList(lstcity, "Select CityNo,CityName From MCity where IsActive='true' order by CityName");
            ObjFunction.FillList(lstyear, "SELECT AccYearNo, AccYearName FROM MAccYear");
            ObjFunction.FillList(lstcompany, "SELECT FirmNo, FirmName FROM MFirm");
            ObjFunction.FillList(lstlocation, "SELECT LocationNo, LocationName FROM MLocation Where IsActive='true'");
           

     }
        private void formatpicture()
        {
            pnlyear.Top = txtdefaulyyear.Bottom;
            pnlyear.Width = txtdefaulyyear.Width;
            pnlyear.Height = 42;
            lstyear.Top = pnlyear.Top - 215;
            lstyear.Height = pnlyear.Height - 5;


            pnlcompany.Top = txtdefaultcompany.Bottom;
            pnlcompany.Width = txtdefaultcompany.Width;
            pnlcompany.Height = 55;
            lstcompany.Top = pnlcompany.Top - 248;
            lstcompany.Height = pnlcompany.Height - 5;

            pnllocation.Top = txtdefaultlocaton.Bottom;
            pnllocation.Width = txtdefaultlocaton.Width;
            pnllocation.Height = 70;
            lstlocation.Top = pnllocation.Top - 280;
            lstlocation.Height = pnllocation.Height - 6;

            pnlcity.Top = txtcity.Bottom;
            pnlcity.Width = txtcity.Width;
            pnlcity.Height = 98;
            lstcity.Top = pnlcity.Top - 342;
            lstcity.Height = pnlcity.Height - 5;

        }
        private void FillControls(long ID)
        {
            try
            {
                MUser MF = new MUser();
                MF = dbUser.ModifyMUserByID(ID);
                TxtUserCode.Text = MF.UsersUserCode;
                UserNm = MF.UserName;
                lstyear.SelectedValue = MF.FkAccYearNo.ToString();
                txtdefaulyyear.Text = lstyear.Text;
                lstcompany.SelectedValue = MF.FkCompanyNo.ToString();
                txtdefaultcompany.Text = lstcompany.Text;
                lstlocation.SelectedValue = MF.FkLocationNo.ToString();
                txtdefaultlocaton.Text = lstlocation.Text;
                TxtUserName.Text = MF.UserName;
                TxtPassword.Text = secure.psDecrypt(MF.Password);
                txtAdd.Text = MF.UserAddress;
                txtPhoneNo.Text = MF.PhoneNo;
                lstcity.SelectedValue = MF.CityCode.ToString();
                txtcity.Text = lstcity.Text;
                chkIsClose.Checked = (MF.IsClose == 1) ? true : false;
                if (MF.UserType == 1)
                {
                    rdAdmin.Checked = true;
                    chkIsClose.Checked = false; chkIsClose.Enabled = false;
                    //TxtUserName.ReadOnly = true;
                }
                //else if (MF.UserType == 2)
                //    rdAgent.Checked = true;
                else if (MF.UserType == 3)
                    rdUser.Checked = true;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validations() == true)
                {

                    mUser.UserCode = User.RequestUserNo;
                    mUser.UsersUserCode = TxtUserCode.Text;
                    mUser.UserName = (TxtUserName.Text.Trim());
                    mUser.Password = secure.psEncrypt(TxtPassword.Text.Trim());
                    mUser.UserAddress = txtAdd.Text;
                    mUser.PhoneNo = txtPhoneNo.Text;
                    mUser.FkAccYearNo = ObjFunction.GetListValue(lstyear);
                    mUser.FkCompanyNo = ObjFunction.GetListValue(lstcompany);
                    mUser.FkLocationNo = ObjFunction.GetListValue(lstlocation);
                    mUser.CityCode = ObjFunction.GetListValue(lstcity);
                    mUser.IsClose = (chkIsClose.Checked == true) ? 1 : 0;
                    if (rdAdmin.Checked == true) mUser.UserType = 1;
                    //else if (rdAgent.Checked == true) mUser.UserType = 2;
                    else if (rdUser.Checked == true) mUser.UserType = 3;

                    if (dbUser.AddMUser(mUser, 2) == true)
                    {
                        string[] str = SaveNodes();
                        mUserMenu.PKSrNo = 0;

                        for (int i = 0; i < str.Length; i++)
                        {
                            switch (i)
                            {
                                case 0: mUserMenu.Master = secure.psEncrypt(str[i].ToString()); break;
                                case 1: mUserMenu.Sales = secure.psEncrypt(str[i].ToString()); break;
                                case 2: mUserMenu.Purchase = secure.psEncrypt(str[i].ToString()); break;
                                case 3: mUserMenu.Accounts = secure.psEncrypt(str[i].ToString()); break;
                                case 4: mUserMenu.Reports = secure.psEncrypt(str[i].ToString()); break;
                                case 5: mUserMenu.Settings = secure.psEncrypt(str[i].ToString()); break;
                                case 6: mUserMenu.Utilities = secure.psEncrypt(str[i].ToString()); break;
                                case 7: mUserMenu.Hidden = secure.psEncrypt(str[i].ToString()); break;
                            }
                        }
                        dbUser.AddMUserMenuMaster(mUserMenu);
                        if (dbUser.ExecuteNonQueryStatements() == true)
                        {
                            OMMessageBox.Show("User Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);

                            this.Close();
                            Form childForm = new Master.User();
                            childForm.Text = "User Creation";
                            ObjFunction.OpenForm(childForm, DBGetVal.MainForm);
                        }
                        else
                        {
                            OMMessageBox.Show("User not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            UserNm = ""; User.RequestUserNo = 0;
            this.Close();
            Form NewF = new User();

            NewF.Text = "User Creation";
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
            pnlcity.Visible = false;
            pnlcompany.Visible = false;
            pnllocation.Visible = false;
            pnlyear.Visible = false;
        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(TxtUserName, ""); 
            EP.SetError(TxtPassword, "");

            if (TxtUserName.Text == "")
            {
                EP.SetError(TxtUserName, "Enter User Name");
                EP.SetIconAlignment(TxtUserName, ErrorIconAlignment.MiddleRight);
                TxtUserName.Focus();
            }
           
            else if (txtdefaulyyear.Text == "")
            {
                EP.SetError(txtdefaulyyear, "Select AccYear");
                EP.SetIconAlignment(txtdefaulyyear, ErrorIconAlignment.MiddleRight);
                txtdefaulyyear.Focus();
            }
            else if (txtdefaultcompany.Text == "")
            {
                EP.SetError(txtdefaultcompany, "Select Company");
                EP.SetIconAlignment(txtdefaultcompany, ErrorIconAlignment.MiddleRight);
                txtdefaultcompany.Focus();
            }
            else if (txtdefaultlocaton.Text == "")
            {
                EP.SetError(txtdefaultlocaton, "Select Location");
                EP.SetIconAlignment(txtdefaultlocaton, ErrorIconAlignment.MiddleRight);
                txtdefaultlocaton.Focus();
            }
            else if (TxtPassword.Text == "")
            {
                EP.SetError(TxtPassword, "Enter Password");
                EP.SetIconAlignment(TxtPassword, ErrorIconAlignment.MiddleRight);
                TxtPassword.Focus();
            }
            //else if (rdAgent.Checked == true)
            //{
            //    if (cc.IsInteger(TxtPassword) == false)
            //    {
            //        EP.SetError(TxtPassword, "Pleas Enter Password in numeric");
            //        EP.SetIconAlignment(TxtPassword, ErrorIconAlignment.MiddleRight);
            //        TxtPassword.Focus();
            //    }
            //    else
            //        flag = true;
            //}
            else
                flag = true;

            return flag;
        }

        private void UserAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserNm = ""; User.RequestUserNo = 0;
        }

        //public void getNodes(long Srno)
        //{
        //    TreeNode TD = new TreeNode(); TreeNode tdTemp;
        //    DataTable dtTree = ObjFunction.GetDataView("Select SrNo,MenuID,MenuName From MMenuMaster Where ControlMenu=" + Srno + "").Table;
        //    if (dtTree.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dtTree.Rows.Count; i++)
        //        {
        //          TD = new TreeNode(dtTree.Rows[i].ItemArray[2].ToString());
        //          TD = getNodes1(TD, Convert.ToInt64(dtTree.Rows[i].ItemArray[0].ToString()));
        //          TVW.Nodes.Add(TD);
        //        }
        //    }
        //    //return TD;
        //}

        public TreeNode InitNodes(TreeNode TD, long Srno)
        {
            try
            {
                TreeNode tdtemp;
                DataTable dtTree = dbUser.GetNodesByNodeID(Srno).Table;// ObjFunction.GetDataView("Select SrNo,MenuID,MenuName From MMenuMaster Where ControlMenu=" + Srno + "").Table;
                if (dtTree.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTree.Rows.Count; i++)
                    {
                        tdtemp = new TreeNode(dtTree.Rows[i].ItemArray[2].ToString());
                        tdtemp = InitNodes(tdtemp, Convert.ToInt64(dtTree.Rows[i].ItemArray[0].ToString()));
                        if (Srno == 0)
                            TVW.Nodes.Add(tdtemp);
                        else
                            TD.Nodes.Add(tdtemp);
                    }
                }
               
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            return TD;
        }

        public TreeNode DisplayNodes(TreeNode TD, long Srno)
        {
            try
            {
                TreeNode tdtemp;
                DataTable dtTree = dbUser.GetNodesByNodeID(Srno).Table;// ObjFunction.GetDataView("Select SrNo,MenuID,MenuName From MMenuMaster Where ControlMenu=" + Srno + "").Table;
                if (dtTree.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTree.Rows.Count; i++)
                    {
                        tdtemp = new TreeNode(dtTree.Rows[i].ItemArray[2].ToString());
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

                        tdtemp = DisplayNodes(tdtemp, Convert.ToInt64(dtTree.Rows[i].ItemArray[0].ToString()));
                        if (Srno == 0)
                        { TVW.Nodes.Add(tdtemp); cntRow++; }
                        else
                            TD.Nodes.Add(tdtemp);

                    }
                }
               
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            return TD;
        }

        public string[] SaveNodes()
        {
            string[] strMain = new string[TVW.Nodes.Count];
            try
            {               
                string str = "";
                int cntNode = 0;
                foreach (TreeNode td in TVW.Nodes)
                {
                    if (td.Checked == true) str = "1"; else str = "0";
                    str = SaveChildNodes(td, str);
                    strMain[cntNode] = str;
                    cntNode++;
                }              
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            return strMain;
        }

        public string SaveChildNodes(TreeNode TD, string str)
        {
            try
            {
                foreach (TreeNode td in TD.Nodes)
                {
                    if (td.Checked == true) str += "1"; else str += "0";
                    str = SaveChildNodes(td, str);
                }                
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            return str;
        }

        private void TVW_AfterCheck(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (NodeCheckFlag == true)
                    CheckNodes(e.Node.Nodes, e.Node.Checked);
                NodeCheckFlag = false;

                if (((TreeNode)(e.Node.Parent)) != null)
                    CheckParentNodes(((TreeNode)(e.Node.Parent)));
                if (e.Node.Checked == true)
                    e.Node.Expand();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CheckParentNodes(TreeNode td)
        {
            bool flag = false;
            foreach (TreeNode nd in td.Nodes)
            {
                if (nd.Checked == true)
                {
                    flag = true;
                    break;
                }
            }
            td.Checked = flag;
        }

        public void CheckNodes(TreeNodeCollection tdc,bool flag)
        {
            foreach (TreeNode nd in tdc)
            {
                nd.Checked = flag;
                CheckNodes(nd.Nodes, flag);
            }
        }

        private void TVW_MouseClick(object sender, MouseEventArgs e)
        {
            NodeCheckFlag = true;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CheckNodes(TVW.Nodes, false);
            NodeCheckFlag = true;
        }

        private void TxtUserName_Leave(object sender, EventArgs e)
        {
            try
            {
                EP.SetError(TxtUserName, "");
                if (TxtUserName.Text != "")
                {
                    if (UserNm.ToUpper() != TxtUserName.Text.ToUpper())
                    {
                        if (ObjQry.ReturnInteger("Select Count(*) from MUser Where UserName = '" + TxtUserName.Text.Replace("'", "''") + "'", CommonFunctions.ConStr) != 0)
                        {
                            EP.SetError(TxtUserName, "Enter User Name");
                            EP.SetIconAlignment(TxtUserName, ErrorIconAlignment.MiddleRight);
                            TxtUserName.Focus();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // txtAdd.GotFocus += delegate { txtAdd.Select(txtAdd.Text.Length, txtAdd.Text.Length); };
                txtAdd.Focus();
            }
        }

        //private void cmbCityName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (chkIsClose.Enabled == false)
        //        {
        //            TVW.Focus();
        //        }
        //        else
        //        {
        //            chkIsClose.Focus();
        //        }
        //    }
        //}

        
        private void TxtUserCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TxtUserName.Focus();
            }
        }

        private void txtAdd_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                txtPhoneNo.Focus();
            }
        }

        private void txtPhoneNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtdefaulyyear.Focus();
            }
        }

       
       
        private void txtdefaulyyear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtdefaulyyear.Text == "")
                {
                    pnlyear.Visible = true;
                    lstyear.Focus();
                    //lstlocation.SelectedIndex = 0;
                }
                else
                {
                    pnlyear.Visible = false;
                    txtdefaultcompany.Focus();
                }
            }
        }

        private void lstyear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtdefaulyyear.Text = lstyear.Text;
                    pnlyear.Visible = false;
                    txtdefaultcompany.Focus();



                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtdefaulyyear.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtdefaultcompany_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtdefaultcompany.Text == "")
                {
                    pnlcompany.Visible = true;
                    lstcompany.Focus();
                    //lstlocation.SelectedIndex = 0;
                }
                else
                {
                    pnlcompany.Visible = false;
                    txtdefaultlocaton.Focus();
                }
            }
        }

        private void lstcompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtdefaultcompany.Text = lstcompany.Text;
                    pnlcompany.Visible = false;
                    txtdefaultlocaton.Focus();



                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtdefaultcompany.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtdefaultlocaton_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtdefaultlocaton.Text == "")
                {
                    pnllocation.Visible = true;
                    lstlocation.Focus();
                    //lstlocation.SelectedIndex = 0;
                }
                else
                {
                    pnllocation.Visible = false;
                    TxtPassword.Focus();
                }
            }
        }

        private void lstlocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtdefaultlocaton.Text = lstlocation.Text;
                    pnllocation.Visible = false;
                    TxtPassword.Focus();



                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtdefaultlocaton.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtcity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtcity.Text == "")
                {
                    pnlcity.Visible = true;
                    lstcity.Focus();
                    //lstlocation.SelectedIndex = 0;
                }
                else
                {
                    pnlcity.Visible = false;
                    if (chkIsClose.Enabled == false)
                    {
                        BtnSave.Focus();
                        //TVW.Focus();
                    }
                    else
                    {
                        chkIsClose.Focus();
                       
                    }
                    
                }
            }
        }

        private void lstcity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    e.SuppressKeyPress = true;
                    txtcity.Text = lstcity.Text;
                    pnlcity.Visible = false;

                    if (chkIsClose.Enabled == false)
                    {
                        BtnSave.Focus();
                       // TVW.Focus();
                    }
                    else
                    {
                        chkIsClose.Focus();
                    }
                    
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtcity.Focus();

                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtcity.Focus();
            }
        }

        
       
    }
}
