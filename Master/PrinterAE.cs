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
    public partial class PrinterAE : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBMPrinter dbPrinter = new DBMPrinter();
        MPrinter mPrinter = new MPrinter();
        string PrinterNm;
        DataTable dtSearch = new DataTable();
        int cntRow;
        public long PrinterNo,ID;
        
        string ipaddr;

        public PrinterAE()
        {
            InitializeComponent();
        }

        private void PrinterAE_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);

                System.Drawing.Printing.PrinterSettings objPrint = new System.Drawing.Printing.PrinterSettings();


                string strPrinters = null;
                foreach (string strItem in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    CmbPrinterName.Items.Add(strPrinters + strItem);
                }
                CmbPrinterName.SelectedText = objPrint.PrinterName;
                CmbPrinterName.SelectedIndex = 0;

                PrinterNm = "";
                dtSearch = ObjFunction.GetDataView("Select PrinterNo From MPrinter order by PrinterName").Table;
                if (dtSearch.Rows.Count > 0)
                {
                    if (Printer.RequestPrinterNo == 0)
                        ID = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    else
                        ID = Printer.RequestPrinterNo;
                    FillControls();
                    SetNavigation();
                }
                setDisplay(true);
                btnNew.Focus();
                KeyDownFormat(this.Controls);
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void FillControls()
        {
            try
            {
                mPrinter = dbPrinter.ModifyMPrinterByID(ID);
                PrinterNm = mPrinter.PrinterName;
                // txtPrinterName.Text = mPrinter.PrinterName;
                CmbPrinterName.Text = mPrinter.PrinterName;
                txtMachineName.Text = mPrinter.MachineName;
                ipaddr = mPrinter.MachineIP;
                chkDefault.Checked = mPrinter.IsDefault;
                chkActive.Checked = mPrinter.IsActive;
                if (chkActive.Checked == true)
                    chkActive.Text = "Yes";
                else
                    chkActive.Text = "No";
                if (chkDefault.Checked == true)
                    chkDefault.Text = "Yes";
                else
                    chkDefault.Text = "No";
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void SetValue()
        {
            try
            {
                //if (Validations() == true)
                //{
                dbPrinter = new DBMPrinter();
                mPrinter = new MPrinter();
                mPrinter.PrinterNo = ID;

                // mPrinter.PrinterName = txtPrinterName.Text.Trim();
                mPrinter.PrinterName = CmbPrinterName.Text.Trim().ToUpper();
                mPrinter.MachineName = txtMachineName.Text.Trim().ToUpper();

                mPrinter.MachineIP = ipaddr;
                mPrinter.IsDefault = chkDefault.Checked;
                mPrinter.IsActive = chkActive.Checked;
                mPrinter.UserID = DBGetVal.UserID;
                mPrinter.UserDate = DBGetVal.ServerTime.Date;

                if (dbPrinter.AddMPrinter(mPrinter) == true)
                {
                    if (ID == 0)
                    {
                        OMMessageBox.Show("Printer Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        dtSearch = ObjFunction.GetDataView("Select PrinterNo From MPrinter order by PrinterName").Table;
                        ID = ObjQry.ReturnLong("Select Max(PrinterNo) FRom MPrinter", CommonFunctions.ConStr);
                        SetNavigation();
                        FillControls();
                    }
                    else
                    {
                        OMMessageBox.Show("Printer Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                    }

                    ObjFunction.LockButtons(true, this.Controls);
                    ObjFunction.LockControls(false, this.Controls);
                }
                else
                {
                    OMMessageBox.Show("Printer not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            //}
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Printer.RequestPrinterNo = 0;
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public bool Validations()
        {
            bool flag = false;

            //if (txtPrinterName.Text.Trim() == "")
            //{

            //    EP.SetError(txtPrinterName, "Enter Printer Name");
            //    EP.SetIconAlignment(txtPrinterName, ErrorIconAlignment.MiddleRight);
            //    txtPrinterName.Focus();
            //}

            //else if (PrinterNm != txtPrinterName.Text)
            //{
            //    if (ObjQry.ReturnInteger("Select Count(*) from MPrinter where PrinterName = '" + txtPrinterName.Text + "'", CommonFunctions.ConStr) != 0)
            //    {
            //        EP.SetError(txtPrinterName, "Duplicate Printer Name");
            //        EP.SetIconAlignment(txtPrinterName, ErrorIconAlignment.MiddleRight);
            //        txtPrinterName.Focus();
            //    }
            //    else
            //        flag = true;
            //}
            //else
            //    flag = true;
            return flag;
        }

        private void PrinterAE_FormClosing(object sender, FormClosingEventArgs e)
        {
            ID = 0;
            PrinterNm = "";
        }

        //private void txtPrinterName_Leave(object sender, EventArgs e)
        //{
        //    EP.SetError(txtPrinterName, "");
        //    if (txtPrinterName.Text != "")
        //    {
        //        if (PrinterNm != txtPrinterName.Text)
        //        {
        //            if (ObjQry.ReturnInteger("Select Count(*) from MPrinter where PrinterName = '" + txtPrinterName.Text + "'", CommonFunctions.ConStr) != 0)
        //            {
        //                EP.SetError(txtPrinterName, "Duplicate Printer Name");
        //                EP.SetIconAlignment(txtPrinterName, ErrorIconAlignment.MiddleRight);
        //                txtPrinterName.Focus();
        //            }
        //        }

        //    }
        //}

        #region Navigation Methods
        private void NavigationDisplay(int type)
        {
            try
            {
                long No = 0;

                if (type == 1)
                {
                    No = Convert.ToInt64(dtSearch.Rows[0].ItemArray[0].ToString());
                    cntRow = 0;
                    ID = No;
                }
                else if (type == 2)
                {
                    No = Convert.ToInt64(dtSearch.Rows[dtSearch.Rows.Count - 1].ItemArray[0].ToString());
                    cntRow = dtSearch.Rows.Count - 1;
                    ID = No;
                }
                else
                {
                    if (type == 3)
                    {
                        cntRow = cntRow + 1;
                    }
                    else if (type == 4)
                    {
                        cntRow = cntRow - 1;
                    }

                    if (cntRow < 0)
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        cntRow = cntRow + 1;
                    }
                    else if (cntRow > dtSearch.Rows.Count - 1)
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        cntRow = cntRow - 1;
                    }
                    else
                    {
                        No = Convert.ToInt64(dtSearch.Rows[cntRow].ItemArray[0].ToString());
                        ID = No;
                    }

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

            FillControls();

        }

        private void SetNavigation()
        {
            cntRow = 0;
            for (int i = 0; i < dtSearch.Rows.Count; i++)
            {
                if (Convert.ToInt64(dtSearch.Rows[i].ItemArray[0].ToString()) == ID )
                {
                    cntRow = i;
                    break;
                }
            }
        }

        public void setDisplay(bool flag)
        {
            btnFirst.Visible = flag;
            btnPrev.Visible = flag;
            btnNext.Visible = flag;
            btnLast.Visible = flag;
            btndelete.Visible = flag;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }

        #endregion

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
            if (e.KeyCode == Keys.Left && e.Control)
            {
                if (btnPrev.Enabled) btnPrev_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Up && e.Control)
            {
                if (btnFirst.Enabled) btnFirst_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Right && e.Control)
            {
                if (btnNext.Enabled) btnNext_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Down && e.Control)
            {
                if (btnLast.Enabled) btnLast_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F2)
            {
                if (BtnSave.Visible) BtnSave_Click(sender, e);
            }
           
        }
        #endregion

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActive.Checked == true)
                chkActive.Text = "Yes";
            else
                chkActive.Text = "No";
        }

        private void chkDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefault.Checked == true)
                chkDefault.Text = "Yes";
            else
                chkDefault.Text = "No";
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkActive.Checked == false) return;
                dbPrinter = new DBMPrinter();
                mPrinter = new MPrinter();

                mPrinter.PrinterNo = ID;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbPrinter.DeleteMPrinter(mPrinter) == true)
                    {
                        OMMessageBox.Show("Printer Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillControls();
                        //Form NewF = new Printer();
                        //this.Close();
                        //ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
                    }
                    else
                    {
                        OMMessageBox.Show("Printer not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Form NewF = new Printer();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                ID = 0;
                ObjFunction.InitialiseControl(this.Controls);
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);
                CmbPrinterName.Focus();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
            //txtPrinterName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            CmbPrinterName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           NavigationDisplay(5);

            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
        }

        private void txtMachineName_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (txtMachineName.Text != "")
                //{
                //    ipaddr = System.Net.Dns.GetHostByName(txtMachineName.Text).AddressList[0].ToString();
                //    if (ipaddr == "")
                //    {
                //        OMMessageBox.Show("Please Enter valid Machine Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                //        txtMachineName.Focus();
                //    }
                //}
            }
            catch (Exception e1)
            {
                ipaddr = "";
                CommonFunctions.ErrorMessge = e1.Message;
                OMMessageBox.Show("Please Enter valid Machine Name", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                txtMachineName.Focus();
            }
        }

        
    }
}
