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
    public partial class TransporterList : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        TVoucherEntry tvoucherentry = new TVoucherEntry();
        DBTVaucherEntry dbtVoucherEntry = new DBTVaucherEntry();

        DBTEWayDetails dbtEWayDetail = new DBTEWayDetails();
        TEWayDetails tewaydetails = new TEWayDetails();

        DBTranspotorDetail dbTranspotorDetail = new DBTranspotorDetail();
        TranspotorDetail mTranspotorDetail = new TranspotorDetail();

        DBMFirm dbmFirm = new DBMFirm();
        DBMLedger dbmLedger = new DBMLedger();
        
        DataTable dtTras = new DataTable();
        DataTable dtSearch = new DataTable();
        DataTable dt = new DataTable();

        public static long RqstTransNO;
        public long ID, No=0, OldID = 0;
        public bool flag = false;
        public bool flag1 = false;
        public bool flag2 = false;
        long DocNo = 0, MaxId = 0, TempVal = 0;
        public int rowcnt = 0;
        public bool flagRec;
        public bool flagChk;


        public TransporterList()
        {
            InitializeComponent();
        }       
                
        private void ExpenseList_Load(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(true, this.Controls);
                ObjFunction.LockControls(false, this.Controls);
                isAdmin();
                btnNew.Focus();
                viewmode();
                FillCmb();
                FillList();
                ID = ObjQry.ReturnLong("SELECT max(PkVoucherNo) FROM TVoucherEntry Where VoucherTypeCode = 37", CommonFunctions.ConStr);
                
                if (ID != 0)
                {
                    MaxId = ID; 
                    pnlRecQty.Visible = false;
                    lblBill.Visible = false;
                    grpTax.Enabled = false;
                    FillFields();                    
                }
               else
                {
                    ID = 0;                    
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void isAdmin()
        {
            if (DBGetVal.IsAdmin == true)
            {
                btnUpdate.Enabled = true;
                btndelete.Enabled = true;
            }
            else
            {
                btnUpdate.Enabled = true;
                btndelete.Enabled = true;
            }
        }

        public void FillList()
        {

            ObjFunction.FillList(lstSupplierNm, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and MLedger.IsActive='true' order by LedgerName");
            ObjFunction.FillList(lstTaxPer, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)) as Percentage " +
                "FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                "MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo " +
                 "WHERE(MLedger_1.GroupNo = 53) And MItemTaxSetting.IsActive = 'True' " +
                "and(MLedger.GroupNo = 11) Order by  MItemTaxSetting.Percentage");
            // ObjFunction.FillList(lstTranspotorNm, "select transporterno, transportername from dbo.MTransporter order by transportername");
            ObjFunction.FillList(lstTranspotorNm, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.Transporter + ") and MLedger.IsActive='true' order by LedgerName");
        }

        private void FillFields()
        {
            try
            {
                flagRec = false;
                tvoucherentry = new TVoucherEntry();
                tvoucherentry = dbtVoucherEntry.ModifyTVoucherEntryByID(ID);
                txtDocNo.Text = tvoucherentry.VoucherUserNo.ToString();
                txtBillNo.Text =tvoucherentry.ChallanNo.ToString();
                txtNoOfQty.Text = "0.00";// Convert.ToDouble(tvoucherentry.TransNoOfItems).ToString();
                txtBasicAmt.Text = Convert.ToDouble(tvoucherentry.ReturnAmount).ToString();
                lstTaxPer.SelectedValue = tvoucherentry.TaxTypeNo;
                txtTaxPer.Text = lstTaxPer.Text;
                txtTaxAmt.Text = Convert.ToDouble(tvoucherentry.TaxAmount).ToString();
                txtTotalAmt.Text = Convert.ToDouble(tvoucherentry.BilledAmount).ToString();
                txtRemarks.Text = tvoucherentry.Remark.ToString();

                tewaydetails = new TEWayDetails();
                tewaydetails = dbtEWayDetail.ModifyTEWayDetailsByID(ID);

                txtLRNo.Text = tewaydetails.LRNo.ToString();
                dtpLRDate.Text =tewaydetails.LRDate.ToString();
                txtReceivedQty.Text = "0.00";//Convert.ToDouble(tewaydetails.ReceivedQty).ToString();
                txtBalancedQty.Text = "0.00";// Convert.ToDouble(tewaydetails.BalancedQty).ToString();
                lstSupplierNm.SelectedValue = tewaydetails.LedgerNo;
                txtSupplierNm.Text = lstSupplierNm.Text;
                lstTranspotorNm.SelectedValue = tewaydetails.TransportNo;
                txtTranspotorNm.Text = lstTranspotorNm.Text;
                lblTotalAmt.Text =Convert.ToString(tvoucherentry.BilledAmount);
                double totalAmt = ObjQry.ReturnDouble("select billedamount from dbo.TVoucherEntry where pkvoucherno = " + txtDocNo.Text + "", CommonFunctions.ConStr);
                lblTotalAmt.Text = totalAmt.ToString();

                mTranspotorDetail = new TranspotorDetail();
                mTranspotorDetail = dbTranspotorDetail.ModifyTranspotorDetailByID(ID);
                    txtNoOfQty.Text = Convert.ToDouble(mTranspotorDetail.NoOfQty).ToString();
                txtBalancedQty.Text =  Convert.ToDouble(mTranspotorDetail.BalancedQty).ToString();
                txtReceivedQty.Text =  Convert.ToDouble(mTranspotorDetail.ReceivedQty).ToString();

                
                pnlRecQty.Visible = false;
                btnUpdate.Enabled = true;
                BtnDetail.Enabled = true;
                btnReceivedQty.Enabled = true;

                //deleteInfo
                if (tvoucherentry.IsVoucherLock == true)
                {
                    lblCancelBll.Visible = true;
                    btnUpdate.Enabled = false;
                    btndelete.Enabled = false;
                    btnNew.Enabled = true;
                    btnReceivedQty.Enabled = false;
                    lblCancelBll.Font = new Font("Arial", 25, FontStyle.Bold);
                    lblCancelBll.Text = "Bill Cancel";
                    lblCancelBll.Visible = true;

                }
                else
                {
                    btnUpdate.Visible = true;
                    btndelete.Enabled = true;
                    btndelete.Visible = true;
                    lblCancelBll.Visible = false;
                }

                //QTY--------------------------------------
                if (Convert.ToDouble(txtBalancedQty.Text) == 0)
                {
                    btnReceivedQty.Enabled = false;
                    btnUpdate.Enabled = false;
                    BtnSave.Enabled = false;
                    btnSearch.Enabled = false;
                    btnNew.Enabled = true;
                    lblBill.Visible = true;
                    lblBill.Font = new Font("Arial", 25, FontStyle.Bold);
                    lblBill.Text = "All Item received";
                }
                else
                {
                    lblBill.Visible = false;
                }
                Tax_Detail(); 
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void FillCmb()
        {
          
            ObjFunction.FillCombo(cmbTaxPer, "SELECT MItemTaxSetting.PkSrNo, (cast(MItemTaxSetting.Percentage as varchar)) as Percentage "+
                "FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN "+
                "MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo "+
                 "WHERE(MLedger_1.GroupNo = 53) And MItemTaxSetting.IsActive = 'True' "+
                "and(MLedger.GroupNo = 11) Order by  MItemTaxSetting.Percentage");
            cmbTaxPer.SelectedValue = 1;
            ObjFunction.FillCombo(cmbTypeOfTransport, "select transModeNo, TransModeName from dbo.MTransporterMode ORDER BY TransModeName");
            cmbTypeOfTransport.SelectedValue = 1;
            ObjFunction.FillCombo(cmbSupplierNm, "Select LedgerNo,LedgerName From MLedger Where GroupNo in (" + GroupType.SundryCreditors + ") and MLedger.IsActive='true' order by LedgerName");
            cmbSupplierNm.SelectedValue = 1;
            ObjFunction.FillCombo(cmbTranspotorNm, "select transporterno, transportername from dbo.MTransporter order by transportername");
            cmbTranspotorNm.SelectedValue = 1;
        }
        public void viewmode()
        {
           
        }

        public void addmode()
        {
            
        }
        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
         
            txtDocNo.Text = "";
            txtBillNo.Text = "";
            txtTotalAmt.Text = "";
            txtTaxAmt.Text = "";
            chkTranName.Checked = false;
            txtCGSTPer.Text = "";
            txtIGSTPer.Text = "";
            txtSGSTPer.Text = "";
            txtCGSTAmt.Text = "";
            txtIGSTAmt.Text = "";
            txtSGSTAmt.Text = "";
            txtTotalTaxAmt.Text = "";
            txtTotalTaxPer.Text = "";
            txtNoOfQty.Text = "";
            txtReceivedQty.Text = "";
            txtBalancedQty.Text = "";
            txtRemarks.Text = "";
            txtLRNo.Text = "";
            txtBasicAmt.Text = "";
            chkTranName.Text = "";
            dtpLRDate.Text = "";
            cmbSupplierNm.Text = "";
            cmbTaxPer.Text = "";
            cmbTranspotorNm.Text = "";
            cmbTypeOfTransport.Text = "";
            dtpLRDate.Text = "";
            lblTotalAmt.Text = "";
            FillFields();
            ObjFunction.LockButtons(true, this.Controls);
            ObjFunction.LockControls(false, this.Controls);
            btnNew.Enabled = true;
            btnReceivedQty.Enabled = false;
            btnNew.Focus();
            pnlSupplierNm.Visible = false;
            pnlTaxPer.Visible = false;
            pnlTranspotorNm.Visible = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //flagRec = false;
            SaveFields();

        }
        public bool Validation()
        {
            bool flag = true;
            try
            {
                EP.SetError(txtNoOfQty, "");
                EP.SetError(txtBasicAmt, "");
                EP.SetError(txtReceivedQty, "");
                if (txtNoOfQty.Text.Trim() == "")
                {
                    if (txtNoOfQty.Text.Trim() == "0.00")
                    {
                        txtReceivedQty.Focus();
                    }
                    else
                    {
                        EP.SetError(txtNoOfQty, "Enter Qty");
                        EP.SetIconAlignment(txtNoOfQty, ErrorIconAlignment.MiddleRight);
                        if (flag) { flag = false; txtNoOfQty.Focus(); }
                    }
                }
                else if (txtReceivedQty.Text.Trim() == "")
                {
                   // double val = 0.00;
                    txtReceivedQty.Text ="0.00";
                }

                else if (txtBasicAmt.Text.Trim() == "")
                {
                    EP.SetError(txtBasicAmt, "Enter Amount");
                    EP.SetIconAlignment(txtBasicAmt, ErrorIconAlignment.MiddleRight);
                    if (flag) { flag = false; txtBasicAmt.Focus(); }
                }
                
                if (flag == true)
                {
                    if (ObjFunction.CheckNumeric(txtNoOfQty.Text.Trim()) == false)
                    {
                        EP.SetError(txtNoOfQty, "Enter Valid qty");
                        EP.SetIconAlignment(txtNoOfQty, ErrorIconAlignment.MiddleRight);
                        txtNoOfQty.Focus(); flag = false;
                    }
                    else if (ObjFunction.CheckNumeric(txtReceivedQty.Text.Trim()) == false)
                    {
                        EP.SetError(txtReceivedQty, "Enter Valid qty");
                        EP.SetIconAlignment(txtReceivedQty, ErrorIconAlignment.MiddleRight);
                        txtReceivedQty.Focus(); flag = false;
                    }
                    else if (ObjFunction.CheckValidAmount(txtBasicAmt.Text.Trim()) == false)
                    {
                        EP.SetError(txtBasicAmt, "Enter Valid Amount");
                        EP.SetIconAlignment(txtBasicAmt, ErrorIconAlignment.MiddleRight);
                        txtBasicAmt.Focus(); flag = false;
                    }
                    else
                        flag = true;
                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }
       
        public void SaveFields()
        {
            try
            {
                //int FkVoucherNo = 
                if (Validation() == true)
                {
                
                        dbtVoucherEntry = new DBTVaucherEntry();
                        tvoucherentry = new TVoucherEntry();
                    //tvoucherentry.PkVoucherNo = Convert.ToInt32(txtDocNo.Text);
                    if(ID != 0)
                    {
                        tvoucherentry.PkVoucherNo = ID;
                    }
                        tvoucherentry.VoucherUserNo = Convert.ToInt32(txtDocNo.Text);
                        tvoucherentry.TransNoOfItems = Convert.ToDouble(txtNoOfQty.Text);
                        tvoucherentry.ReturnAmount = Convert.ToDouble(txtBasicAmt.Text);
                        tvoucherentry.ChallanNo = txtBillNo.Text.Trim();
                        tvoucherentry.ChrgesTaxPerce = Convert.ToDouble(lstTaxPer.SelectedValue);
                        tvoucherentry.TaxAmount = Convert.ToDouble(txtTaxAmt.Text);
                        tvoucherentry.BilledAmount = Convert.ToDouble(txtTotalAmt.Text);
                        tvoucherentry.Remark = txtRemarks.Text.Trim();
                        tvoucherentry.CompanyNo = DBGetVal.FirmNo;
                        tvoucherentry.UserID = DBGetVal.UserID;
                        tvoucherentry.TransporterCode = 0;
                        tvoucherentry.UserDate = DBGetVal.ServerTime.Date;
                        tvoucherentry.BrokerNo = 0;
                        tvoucherentry.ChequeNo = 0;
                        tvoucherentry.ClearingDate = Convert.ToDateTime(dtpLRDate.Value);
                        tvoucherentry.DiscAmt = 0;
                        tvoucherentry.DiscPercent = 0;
                        tvoucherentry.EffectiveDate = Convert.ToDateTime(dtpLRDate.Value);
                        tvoucherentry.InwardLocationCode = 0;
                        tvoucherentry.IsBillMulti = 0;
                        tvoucherentry.IsCancel = false;
                        tvoucherentry.IsFooterLevelDisc = false;
                        tvoucherentry.IsItemLevelDisc = false;
                        tvoucherentry.IsTaxFree = false;
                        tvoucherentry.IsVoucherLock = false;
                        tvoucherentry.LedgerNo = 0;
                        tvoucherentry.LRNo = "";
                        tvoucherentry.MacNo = 0;
                        tvoucherentry.MixMode = 0;
                        tvoucherentry.ModifiedBy = "";
                        tvoucherentry.msg = "";
                        tvoucherentry.Narration = "";
                        tvoucherentry.OrderType = 0;
                        tvoucherentry.PayTypeNo = 0;
                        tvoucherentry.PkRefNo = 0;
                        tvoucherentry.PrintCount = 0;
                        tvoucherentry.RateTypeNo = 0;
                        tvoucherentry.Reference = "";
                        tvoucherentry.StateCode = 0;
                        tvoucherentry.StatusNo = 0;
                        tvoucherentry.SuppCategory = 0;
                        tvoucherentry.TaxInvoiceTypeNo = 0;
                        tvoucherentry.TaxTypeNo = Convert.ToInt32(lstTaxPer.SelectedValue);
                        tvoucherentry.TransPayType = 0;
                        tvoucherentry.TransportMode = 0;
                        tvoucherentry.VoucherDate = Convert.ToDateTime(dtpLRDate.Value);
                        tvoucherentry.VoucherTime = Convert.ToDateTime(dtpLRDate.Value);
                        tvoucherentry.VoucherTypeCode = 37;
                        flag = dbtVoucherEntry.AddTVoucherEntry(tvoucherentry);
                        if (flag == true)
                        {
                            tewaydetails = new TEWayDetails();
                        // tewaydetails.PKEWayNo = 0;
                        if (ID != 0)
                        {
                            tewaydetails.PKEWayNo = ObjQry.ReturnLong("SELECT PKEWayNo FROM TEwaydetails Where FkVoucherNo = " + ID + "", CommonFunctions.ConStr);
                        }
                            tewaydetails.EWayNo = "";
                            tewaydetails.VoucherUserNo = 0;
                            tewaydetails.EWayDate = Convert.ToDateTime(dtpLRDate.Value);
                            tewaydetails.ModeNo = 0;
                            tewaydetails.Distance = 0;
                            tewaydetails.TransportNo = Convert.ToInt32(lstTranspotorNm.SelectedValue);
                            tewaydetails.VehicleNo = "";
                            tewaydetails.LRNo = txtLRNo.Text.Trim();
                            tewaydetails.LRDate = Convert.ToDateTime(dtpLRDate.Value);
                            tewaydetails.LedgerNo = Convert.ToInt32(lstSupplierNm.SelectedValue);
                            tewaydetails.LedgerName = Convert.ToString(txtSupplierNm.Text);
                            tewaydetails.Address = "";
                            tewaydetails.CityNo = 0;
                            tewaydetails.CityName = "";
                            tewaydetails.PinCode = 0;
                            tewaydetails.StateCode = 0;
                            tewaydetails.StateName = "";
                            tewaydetails.UserID = DBGetVal.UserID;
                            tewaydetails.UserDate = DBGetVal.ServerTime.Date;
                            tewaydetails.StatusNo = 0;
                            tewaydetails.IsActive = false;
                            tewaydetails.msg = "";
                            tewaydetails.TranspotorNm = Convert.ToString(txtTranspotorNm.Text);
                        
                        flag1 = dbtVoucherEntry.AddTEWayDetails1(tewaydetails);
                        }
                        if (flag1 == true)
                        {
                            mTranspotorDetail = new TranspotorDetail();
                            if (ID != 0)
                            {
                             mTranspotorDetail.PkTranspotorDetail = ObjQry.ReturnLong("SELECT PkTranspotorDetail FROM mTranspotorDetail Where FkVoucherNo = " + ID + "", CommonFunctions.ConStr);
                            }                            
                            mTranspotorDetail.NoOfQty = Convert.ToDouble(txtNoOfQty.Text);
                            mTranspotorDetail.BalancedQty = Convert.ToDouble(txtBalancedQty.Text);
                            mTranspotorDetail.ReceivedQty = Convert.ToDouble(txtReceivedQty.Text);
                            mTranspotorDetail.msg = "";
                            mTranspotorDetail.UpatedDate = Convert.ToDateTime(dtpLRDate.Value);
                            mTranspotorDetail.RemarkQty = Convert.ToString(txtQtyRemark.Text);
                            flag2 = dbtVoucherEntry.AddTranspotorDetail(mTranspotorDetail);
                        }
                        
                    long TempID = 0;
                    TempID = dbtVoucherEntry.ExecuteNonQueryStatements();
                       if (TempID != 0)
                        {
                               if (ID == 0)
                               {
                                       OMMessageBox.Show("Record Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                       ID = ObjQry.ReturnLong("SELECT max(PkVoucherNo) FROM TVoucherEntry Where VoucherTypeCode = 37", CommonFunctions.ConStr);
                                }
                                else
                                {
                                      OMMessageBox.Show("Record Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                                }
                            FillFields();

                            ObjFunction.LockButtons(true, this.Controls);
                            ObjFunction.LockControls(false, this.Controls);
                            btnNew.Enabled = true;
                            btnNew.Focus();
                        }
                      else
                      {
                          OMMessageBox.Show("Record not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                      }
                }
       
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            
            ObjFunction.InitialiseControl(this.Controls);
            ObjFunction.LockButtons(false, this.Controls);
            ObjFunction.LockControls(true, this.Controls);
            lblCancelBll.Visible = false;
            panel2.Enabled = true;
            panel3.Enabled = true;
            panel4.Enabled = true;
           // txtDocNo.Text = (ObjQry.ReturnLong("select IsNull(Max(VoucherUserNo),0) as 'DocNo' from TVoucherEntry  ", CommonFunctions.ConStr)+1).ToString();
            txtDocNo.Text = (ObjQry.ReturnLong("select IsNull(Max(VoucherUserNo),0) as 'DocNo' from TVoucherEntry where VoucherTypeCode = 37", CommonFunctions.ConStr) + 1).ToString();
            lblTotalAmt.Text = "";
            dtpLRDate.Text = "";
            txtLRNo.Text = "";
            cmbSupplierNm.Text = "";
            cmbTranspotorNm.Text = "";
            txtBalancedQty.Enabled = false;
            txtReceivedQty.Enabled = false;
            txtTaxAmt.Enabled = false;
            txtTotalAmt.Enabled = false;
            btnReceivedQty.Enabled = false;
            pnlRecQty.Visible = false;
            BtnDetail.Enabled = false;
            BtnSave.Enabled = true;
            txtTranspotorNm.Focus();
            ID = 0;
            flagChk = true;
            lblBill.Visible = false;
            lblCancelBll.Visible = false;
        }
        private void formatpicture()
        {
            pnlTranspotorNm.Top = 101;
            pnlTranspotorNm.Width = 1260;
            pnlTranspotorNm.Height = 500;
            pnlTranspotorNm.Left = 367;
            
            pnlSupplierNm.Top = 168;
            pnlSupplierNm.Width = 1260;
            pnlSupplierNm.Height = 400;
            pnlSupplierNm.Left = 144;

            pnlTaxPer.Top = 208;
            pnlTaxPer.Width = 1235;
            pnlTaxPer.Height = 500;
            pnlTranspotorNm.Left = 348;

            pnlRecQty.Top = 253;
            pnlRecQty.Width = 300;
            pnlRecQty.Height = 130;
            pnlRecQty.Left = 714;
        }
        public void CalculateQty(double ReceivedQty)
        {
            try
            {
                double Qty = 0.00;
                double BalQty = 0.00;
                double GQty = 0.00;
                double RecQty;
                BalQty = Convert.ToDouble(txtBalancedQty.Text = string.IsNullOrEmpty(txtBalancedQty.Text) ? "0" : txtBalancedQty.Text);
                Qty = Convert.ToDouble(txtNoOfQty.Text = string.IsNullOrEmpty(txtNoOfQty.Text) ? "0" : txtNoOfQty.Text);
                ReceivedQty = Convert.ToDouble(txtQtyRecQty.Text = string.IsNullOrEmpty(txtQtyRecQty.Text) ? "0" : txtQtyRecQty.Text) + Convert.ToDouble(txtReceivedQty.Text = string.IsNullOrEmpty(txtReceivedQty.Text) ? "0" : txtReceivedQty.Text);

                BalQty = Qty - ReceivedQty;
                txtBalancedQty.Text = BalQty.ToString("0.00");
                GQty = Math.Round(BalQty, 2);
            }
            catch(Exception exe)
            {
                ObjFunction.ExceptionDisplay(exe.Message);
            }
        }
        //public void CalculateQty2()
        //{
        //    double Qty = 0;
        //    double BalQty = 0;
        //    double RecQty = 0;
        //    double GQty = 0;
        //    BalQty = Convert.ToDouble(txtBalancedQty.Text = string.IsNullOrEmpty(txtBalancedQty.Text) ? "0" : txtBalancedQty.Text);
        //    Qty = BalQty;//Convert.ToDouble(txtNoOfQty.Text = string.IsNullOrEmpty(txtNoOfQty.Text) ? "0" : txtNoOfQty.Text);
        //    RecQty = Convert.ToDouble(txtQtyRecQty.Text = string.IsNullOrEmpty(txtQtyRecQty.Text) ? "0" : txtQtyRecQty.Text);
        //    BalQty = Qty - RecQty;
        //    txtBalancedQty.Text = BalQty.ToString("0.00");
        //    GQty = Math.Round(BalQty, 2);

        //}
        public void CalculateToatAmt()
        {
            double cal = 0;
            double taxAmt = 0;
            double basicamt = 0;
            basicamt = Convert.ToDouble(txtBasicAmt.Text = string.IsNullOrEmpty(txtBasicAmt.Text) ? "0" : txtBasicAmt.Text);
            taxAmt = Convert.ToDouble(txtTaxAmt.Text = string.IsNullOrEmpty(txtTaxAmt.Text) ? "0" : txtTaxAmt.Text);
            cal = basicamt + taxAmt;
            txtTotalAmt.Text = Convert.ToDouble(Math.Round(cal)).ToString();
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigationDisplay(1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigationDisplay(2);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigationDisplay(3);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            NavigationDisplay(4);
        }

        //private void NavigationDisplay(int type)
        //{
        //    try
        //    {

        //        if (type == 1)
        //        {
        //            dtSearch = ObjFunction.GetDataView("Select isnull(min(VoucherUserNo),0)as Docno From TVoucherEntry where VoucherTypeCode = 37").Table;
        //            No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
        //            ID = No;
        //        }
        //        else if (type == 2)
        //        {
        //            dtSearch = ObjFunction.GetDataView("Select isnull(max(VoucherUserNo),0)as Docno From TVoucherEntry where VoucherTypeCode = 37").Table;
        //            No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
        //            ID = No;
        //        }
        //        else if (type == 3)
        //        {
        //            //  dtSearch = ObjFunction.GetDataView("Select isnull(min(VoucherUserNo),0)as Docno From TVoucherEntry where pkvoucherno > "+ ID).Table;
        //            dtSearch = ObjFunction.GetDataView("Select isnull(min(VoucherUserNo),0)as Docno From TVoucherEntry where  VoucherTypeCode = 37 and pkvoucherno > (SELECT pkvoucherno FROM TVoucherEntry Where VoucherTypeCode = 37 and voucheruserno = " + ID + ")").Table;

        //            // (SELECT pkvoucherno FROM TVoucherEntry Where VoucherTypeCode = 37 and voucheruserno = "+ ID +")
        //            No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
        //            if (No > 0)
        //            {
        //                ID = No;
        //            }
        //            else
        //            {
        //                OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
        //            }
        //        }
        //        else if (type == 4)
        //        {
        //            // dtSearch = ObjFunction.GetDataView("Select isnull(max(VoucherUserNo),0)as Docno From TVoucherEntry where pkvoucherno <" + ID).Table;
        //            dtSearch = ObjFunction.GetDataView("Select isnull(max(VoucherUserNo),0)as Docno From TVoucherEntry where  VoucherTypeCode = 37 and  pkvoucherno < (SELECT pkvoucherno FROM TVoucherEntry Where VoucherTypeCode = 37 and voucheruserno = " + ID + ")").Table;

        //            No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
        //            if (No > 0)
        //            {
        //                ID = No;
        //            }
        //            else
        //            {
        //                OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
        //            }
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //    FillFields();
        //}
        private void NavigationDisplay(int type)
        {
            try
            {

                if (type == 1)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(pkvoucherno),0)as Docno From TVoucherEntry where VoucherTypeCode = 37").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
                    ID = No;
                }
                else if (type == 2)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(pkvoucherno),0)as Docno From TVoucherEntry where VoucherTypeCode = 37").Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
                    ID = No;
                }
                else if (type == 3)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(min(pkvoucherno),0)as Docno From TVoucherEntry where  VoucherTypeCode = 37 and pkvoucherno >" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is Last Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
                else if (type == 4)
                {
                    dtSearch = ObjFunction.GetDataView("Select isnull(max(pkvoucherno),0)as Docno From TVoucherEntry where  VoucherTypeCode = 37 and pkvoucherno <" + ID).Table;
                    No = Convert.ToInt64(dtSearch.Rows[0]["Docno"].ToString());
                    if (No > 0)
                    {
                        ID = No;
                    }
                    else
                    {
                        OMMessageBox.Show("This is First Record", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                    }
                }
                FillFields();
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void cmbTypeOfTransport_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (cmbTypeOfTransport.Text == "")
                    {
                        cmbTypeOfTransport.Focus();
                    }
                    else
                    {
                       chkTranName.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbLedgerNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (cmbSupplierNm.Text == "")
                    {
                    }
                    else
                    {
                        txtBillNo.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtLRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtLRNo.Text == "")
                    {
                        txtLRNo.Focus();
                    }
                    else
                    {
                        txtSupplierNm.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dtpLRDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (dtpLRDate.Text == "")
                    {
                        dtpLRDate.Focus();
                    }
                    else
                    {
                        txtLRNo.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void chkTranName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (chkTranName.Checked == true)
                    {
                        txtNoOfQty.Visible = true;
                        txtBalancedQty.Visible = true;
                        txtReceivedQty.Visible = true;
                        lblBalQty.Visible = true;
                        lblNoOfQty.Visible = true;
                        lblReceQty.Visible = true;
                        cmbTranspotorNm.Focus();
                    }
                    else
                    {
                        txtNoOfQty.Visible = false;
                        txtBalancedQty.Visible = false;
                        txtReceivedQty.Visible = false;

                        lblBalQty.Visible = false;
                        lblNoOfQty.Visible = false;
                        lblReceQty.Visible = false;
                        cmbTranspotorNm.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try

            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtBillNo.Text == "")
                    {
                        txtBillNo.Focus();
                    }
                    else
                    {
                        txtNoOfQty.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbTranspotorNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (cmbTranspotorNm.Text == "")
                    {
                        cmbTranspotorNm.Focus();
                    }
                    else
                    {
                        dtpLRDate.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtNoOfQty_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtNoOfQty.Text == "")
                    {
                        txtNoOfQty.Focus();
                    }
                    else
                    {
                        double ReceivedQty = 0.00;
                        CalculateQty(ReceivedQty);
                        txtBasicAmt.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtReceivedQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtReceivedQty.Text == "")
                    {
                        txtReceivedQty.Focus();
                    }
                    else
                    {
                        if(Convert.ToDouble(txtNoOfQty.Text) == Convert.ToDouble(txtReceivedQty.Text) || Convert.ToDouble(txtNoOfQty.Text) > Convert.ToDouble(txtReceivedQty.Text))
                        {
                            double ReceivedQty = Convert.ToDouble(txtQtyRecQty.Text);
                            CalculateQty(ReceivedQty);
                            txtBasicAmt.Focus();

                        }
                        else
                        {
                            OMMessageBox.Show("pls enter valid Qty", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            txtReceivedQty.Text = "";
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtBalancedQty_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtBalancedQty.Text == "")
                    {
                       // CalculateQty();
                        txtBalancedQty.Focus();
                    }
                    else
                    {
                        //CalculateQty();
                        txtBasicAmt.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        
      
        private void txtRemark_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtRemarks.Text == "")
                    {
                        txtRemarks.Focus();
                    }
                    else
                    {
                        BtnSave.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtIGST_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtIGSTPer.Text == "")
                    {
                        txtIGSTPer.Focus();
                    }
                    else
                    {
                        txtIGSTAmt.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        
        private void txtTotalAmt_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtTotalAmt.Text == "")
                    {
                        txtTotalAmt.Focus();
                    }
                    else
                    {
                        txtRemarks.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtBasicAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtBasicAmt.Text == "")
                    {
                        txtBasicAmt.Focus();
                    }
                    else
                    {
                        txtTaxPer.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        //private void cmbTaxPer_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyChar == Convert.ToChar(Keys.Enter))
        //        {
        //            if (cmbTaxPer.Text == "")
        //            {
        //                cmbTaxPer.Focus();
        //            }
        //            else
        //            {
        //                txtTaxAmt.Focus();
        //                MFirm mfirm = new MFirm();
        //                MLedger mledger = new MLedger ();
        //                if (mfirm.StateNo == mledger.StateCode)
        //                {
        //                    double taxPer = Convert.ToDouble(cmbTaxPer.Text = string.IsNullOrEmpty(cmbTaxPer.Text) ? "0.00" : cmbTaxPer.Text);
        //                    double amt = Convert.ToDouble(txtBasicAmt.Text = string.IsNullOrEmpty(txtBasicAmt.Text) ? "0.00" : txtBasicAmt.Text);
        //                    double cal = (taxPer / 100) * (amt);
        //                    txtTaxAmt.Text = Convert.ToDouble(cal).ToString();


        //                    txtIGSTPer.Text = Convert.ToDouble(cmbTaxPer.Text).ToString();
        //                    txtCGSTPer.Text = "0";
        //                    txtSGSTPer.Text = "0";
        //                    txtTotalTaxPer.Text = Convert.ToDouble(cmbTaxPer.Text).ToString();

        //                    txtIGSTAmt.Text = Convert.ToDouble(cal).ToString();
        //                    txtCGSTAmt.Text = "0.00";
        //                    txtSGSTAmt.Text = "0.00";
        //                    txtTotalTaxAmt.Text = Convert.ToDouble(cal).ToString();
        //                }
        //                else
        //                {
        //                    txtIGSTPer.Text = "0";
        //                    double Other_GSTvalue1 = Convert.ToDouble(cmbTaxPer.Text) / 2;
        //                    txtCGSTPer.Text = Convert.ToDouble(Other_GSTvalue1).ToString();
        //                    txtSGSTPer.Text = Convert.ToDouble(Other_GSTvalue1).ToString();
        //                    txtTotalTaxPer.Text = Convert.ToDouble(Other_GSTvalue1).ToString();

        //                    txtIGSTAmt.Text = "0.00";
        //                    double taxPer = Convert.ToDouble(txtCGSTPer.Text = string.IsNullOrEmpty(cmbTaxPer.Text) ? "0.00" : cmbTaxPer.Text);
        //                    double amt = Convert.ToDouble(txtBasicAmt.Text = string.IsNullOrEmpty(txtBasicAmt.Text) ? "0.00" : txtBasicAmt.Text);
        //                    double cal = (taxPer / 100) * (amt);
        //                    txtTaxAmt.Text = Convert.ToDouble(cal).ToString();

        //                    double Other_GSTvalue= Convert.ToDouble(cmbTaxPer.Text) / 2;
        //                    txtCGSTAmt.Text = Convert.ToDouble(cal).ToString();
        //                    txtSGSTAmt.Text = Convert.ToDouble(cal).ToString();
        //                    txtTotalTaxAmt.Text= Convert.ToDouble(cal).ToString();
        //                }
        //                CalculateToatAmt();
        //                txtRemarks.Focus();
        //            }
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        private void txtTaxAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtTaxAmt.Text == "")
                    {
                        txtTaxAmt.Focus();
                    }
                    else
                    {
                        Tax_Detail();
                        txtTotalAmt.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtTotalAmt_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtTotalAmt.Text == "")
                    {
                        txtTotalAmt.Focus();
                    }
                    else
                    {
                        txtRemarks.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtRemarks.Text == "")
                    {
                        BtnSave.Focus();
                    }
                    else
                    {
                        BtnSave.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtLRNo_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtNoOfQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtReceivedQty_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtBasicAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtIGSTPer_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMaskedNumeric((TextBox)sender);
            ObjFunction.SetMasked(((TextBox)sender), 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void txtTaxAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtSGSTPer_TextChanged(object sender, EventArgs e)
        {
            //ObjFunction.SetMaskedNumeric((TextBox)sender);
            ObjFunction.SetMasked(((TextBox)sender), 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void txtCGSTPer_TextChanged(object sender, EventArgs e)
        {
            //  ObjFunction.SetMaskedNumeric((TextBox)sender);
            ObjFunction.SetMasked(((TextBox)sender), 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void txtTotalTaxPer_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtIGSTAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtSGSTAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtCGSTAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void txtTotalTaxAmt_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ObjFunction.LockButtons(false, this.Controls);
                ObjFunction.LockControls(true, this.Controls);

                btnNew.Focus();
                btnNew.Enabled = false;
                BtnDetail.Enabled = false;
                BtnSave.Enabled = true;
                btnReceivedQty.Enabled = true;
                txtBalancedQty.Enabled = false;
                txtReceivedQty.Enabled = false;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void cmbTranspotorNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (ObjFunction.GetComboValue(cmbTranspotorNm) <= 0)
                {
                    cmbTranspotorNm.Focus();
                    e.SuppressKeyPress = true;
                }
                else
                {
                    dtpLRDate.Focus();
                }
            }
        }

        private void lstTranspotorNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    txtTranspotorNm.Text = lstTranspotorNm.Text;
                    pnlTranspotorNm.Visible = false;
                    dtpLRDate.Focus();
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                {
                    txtTranspotorNm.Focus();
                    pnlTranspotorNm.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstSupplierNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    txtSupplierNm.Text = lstSupplierNm.Text;
                    pnlSupplierNm.Visible = false;
                    txtBillNo.Focus();
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                {
                    txtSupplierNm.Focus();
                    pnlSupplierNm.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lstTaxPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    txtTaxPer.Text = lstTaxPer.Text;
                    pnlTaxPer.Visible = false;
                    txtTaxAmt.Focus();
                    Tax_Detail();
                    txtRemarks.Focus();
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Escape))
                {
                    txtTaxPer.Focus();
                    pnlTaxPer.Visible = false;
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        public void Tax_Detail()
        {
            MFirm mfirm = new MFirm();
            MLedger mledger = new MLedger();

            DBMFirm dbmFirm = new DBMFirm();
            DBMLedger dbmLedger = new DBMLedger();
            
            long stateNo, transStateCode;
            transStateCode = ObjQry.ReturnLong("Select statecode From MLedger Where GroupNo in (" + GroupType.Transporter+ ") and MLedger.IsActive='true' and ledgerName ='" + txtTranspotorNm.Text +"' order by LedgerName ", CommonFunctions.ConStr);
            stateNo = ObjQry.ReturnLong(" select stateno from dbo.MFirm", CommonFunctions.ConStr);
           
            
            if (transStateCode == stateNo)
            {
                double taxPer = Convert.ToDouble(txtTaxPer.Text = string.IsNullOrEmpty(txtTaxPer.Text) ? "0.00" : txtTaxPer.Text);
                double amt = Convert.ToDouble(txtBasicAmt.Text = string.IsNullOrEmpty(txtBasicAmt.Text) ? "0.00" : txtBasicAmt.Text);
                double cal = (taxPer / 100) * (amt);
                txtTaxAmt.Text = Convert.ToDouble(Math.Round(cal)).ToString();
                
                txtIGSTPer.Text = Convert.ToDouble(txtTaxPer.Text).ToString();
                txtCGSTPer.Text = "0";
                txtSGSTPer.Text = "0";
                txtTotalTaxPer.Text = Convert.ToDouble(txtTaxPer.Text).ToString();

                txtIGSTAmt.Text = Convert.ToDouble(Math.Round(cal)).ToString();
                txtCGSTAmt.Text = "0.00";
                txtSGSTAmt.Text = "0.00";
                txtTotalTaxAmt.Text = Convert.ToDouble(Math.Round(cal)).ToString();
            }
            else
            {
                txtIGSTPer.Text = "0";
                double Other_GSTvalue1 = Convert.ToDouble(txtTaxPer.Text) / 2;
                //double GSTPer = Convert.ToDouble(txtTaxPer.Text);
                //double SGST = GSTPer / 2;
                //double CGST = GSTPer / 2;
                //double CGSTvalue1 = Convert.ToDouble(txtTaxPer.Text) / 2;
                //txtSGSTPer.Text = Convert.ToString(SGST);      // Convert.ToString( Convert.ToDouble(txtTaxPer.Text) / 2.00);
                //txtCGSTPer.Text = Convert.ToString(CGST);      //Convert.ToString( Convert.ToDouble(txtTaxPer.Text) / 2.00);
                txtSGSTPer.Text = Convert.ToDouble(Other_GSTvalue1).ToString();
                txtCGSTPer.Text = Convert.ToDouble(Other_GSTvalue1).ToString();
                txtTotalTaxPer.Text = Convert.ToDouble(txtTaxPer.Text).ToString();

                txtIGSTAmt.Text = "0.00";
                double taxPer = Convert.ToDouble(txtTaxPer.Text = string.IsNullOrEmpty(txtTaxPer.Text) ? "0.00" : txtTaxPer.Text);
                double amt = Convert.ToDouble(txtBasicAmt.Text = string.IsNullOrEmpty(txtBasicAmt.Text) ? "0.00" : txtBasicAmt.Text);
                double cal = (taxPer / 100) * (amt);
                txtTaxAmt.Text = Convert.ToDouble(cal).ToString();
                double TaxAmtDivide = (cal / 2);
                double Other_GSTvalue = Convert.ToDouble(txtTaxPer.Text) / 2;

                txtCGSTAmt.Text = Convert.ToDouble(Math.Round(TaxAmtDivide)).ToString();
                txtSGSTAmt.Text = Convert.ToDouble(Math.Round(TaxAmtDivide)).ToString();
                txtTotalTaxAmt.Text = Convert.ToDouble(Math.Round(cal)).ToString();
            }
            CalculateToatAmt();

        }
        private void txtTranspotorNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {

                    if (txtTranspotorNm.Text == "")
                    {
                        pnlTranspotorNm.Visible = true;
                        lstTranspotorNm.Focus();
                    }
                    else
                    {
                        pnlTranspotorNm.Visible = false;
                        dtpLRDate.Focus();
                    }

                }
                else if (e.KeyChar == Convert.ToChar(Keys.Delete))
                {
                    txtTranspotorNm.Text = "";
                }
                else
                {
                    TempVal = Convert.ToInt32(lstTranspotorNm.SelectedValue);
                    pnlTranspotorNm.Visible = true;
                    lstTranspotorNm.Focus();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtSupplierNm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {

                    if (txtSupplierNm.Text == "")
                    {
                        pnlSupplierNm.Visible = true;
                        lstSupplierNm.Focus();
                    }
                    else
                    {
                        pnlSupplierNm.Visible = false;
                        txtBillNo.Focus();
                    }

                }
                else if (e.KeyChar == Convert.ToChar(Keys.Delete))
                {
                    txtSupplierNm.Text = "";
                }
                else
                {

                    TempVal = Convert.ToInt32(lstSupplierNm.SelectedValue);
                    pnlSupplierNm.Visible = true;
                    lstSupplierNm.Focus();
                }                       
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtTaxPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtTaxPer.Text == "")
                    {
                        pnlTaxPer.Visible = true;
                        lstTaxPer.Focus();
                    }
                    else
                    {
                        pnlTaxPer.Visible = false;
                        txtTaxAmt.Focus();
                        Tax_Detail();
                        txtRemarks.Focus();

                    }

                }
                else if (e.KeyChar == Convert.ToChar(Keys.Delete))
                {
                    txtTaxPer.Text = "";
                }
                else
                {
                    TempVal = Convert.ToInt32(lstTaxPer.SelectedValue);
                    pnlTaxPer.Visible = true;
                    lstTaxPer.Focus();

                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnReceivedQty_Click(object sender, EventArgs e)
        {
            pnlRecQty.Visible = true;
            pnlRecQty.Enabled = true;
            dtpQtyDate.Enabled = true;
            btnUpdate.Enabled = false;
            btndelete.Enabled = false;
            btnNew.Enabled = false;
            BtnDetail.Enabled = false;
            txtQtyRecQty.Enabled=true;
            txtQtyRecQty.Text = "";
            txtQtyRemark.Text = "";
            txtQtyRecQty.Focus();
   
        }


        private void BtnQtyExit_Click(object sender, EventArgs e)
        {
            pnlRecQty.Visible = false;
            panel2.Enabled = true;
            panel3.Enabled = true;
            panel6.Enabled = true;
            btnNew.Enabled = true;
            btnUpdate.Enabled = true;
            btndelete.Enabled = true;
            btnNew.Focus();

        }

        private void btnQtySave_Click(object sender, EventArgs e)
        {
            try
            {
                Double BalQty;
                dbtVoucherEntry = new DBTVaucherEntry();
                mTranspotorDetail = new TranspotorDetail();
                mTranspotorDetail.PkTranspotorDetail = 0;
                mTranspotorDetail.FKEWayNo = 0;
                mTranspotorDetail.FkVoucherNo = ID; 
             
                mTranspotorDetail.NoOfQty = Convert.ToDouble(txtNoOfQty.Text);
                mTranspotorDetail.ReceivedQty = Convert.ToDouble(txtQtyRecQty.Text)+ Convert.ToDouble(txtReceivedQty.Text);
            
                if (mTranspotorDetail.NoOfQty == mTranspotorDetail.ReceivedQty)
                {
                    double recQty = Convert.ToDouble(txtReceivedQty.Text) + Convert.ToDouble(txtQtyRecQty.Text);
                    BalQty = Convert.ToDouble(txtNoOfQty.Text) - recQty;
                }
                else
                {
                    double recQty = Convert.ToDouble(txtReceivedQty.Text) + Convert.ToDouble(txtQtyRecQty.Text);
                     BalQty= Convert.ToDouble(txtNoOfQty.Text) - recQty;

                }
                mTranspotorDetail.BalancedQty = Convert.ToDouble(BalQty);
                mTranspotorDetail.UpatedDate = Convert.ToDateTime(dtpQtyDate.Text);
                mTranspotorDetail.msg = "";
                mTranspotorDetail.RemarkQty = txtQtyRemark.Text.Trim();
                dbtVoucherEntry.AddTranspotorDetailNew(mTranspotorDetail);
                pnlRecQty.Visible = false;
                FillFields();
                btnNew.Focus();
                panel2.Enabled = true;
                panel3.Enabled = true;
                panel6.Enabled = true;
            }
            catch (Exception exe)
            {
                ObjFunction.ExceptionDisplay(exe.Message);
            }
        }

        private void dtpQtyDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (dtpQtyDate.Text == "")
                    {
                        dtpQtyDate.Focus();
                    }
                    else
                    {
                        txtQtyRecQty.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        //private void txtQtyRecQty_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    try
        //    {
        //        if (e.KeyChar == Convert.ToChar(Keys.Enter))
        //        {
        //            if (txtQtyRecQty.Text == "")
        //            {
        //                txtQtyRecQty.Focus();
        //            }
        //            else
        //            {
        //                if (Convert.ToDouble(txtQtyRecQty.Text) == Convert.ToDouble(txtBalancedQty.Text) || Convert.ToDouble(txtQtyRecQty.Text) < Convert.ToDouble(txtBalancedQty.Text))
        //                {
        //                    double ReceivedQty = Convert.ToDouble(txtQtyRecQty.Text);
        //                    CalculateQty(ReceivedQty);
        //                    //CalculateQty2();
        //                    txtQtyRemark.Enabled = true;
        //                    txtQtyRemark.Focus();
        //                }
        //                else
        //                {
        //                    OMMessageBox.Show("pls enter valid Qty", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        //                    txtQtyRecQty.Text = "";
        //                }

        //                // CalculateQty();

        //            }
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        private void txtQtyRecQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtQtyRecQty.Text == "")
                    {
                        txtQtyRecQty.Focus();
                    }
                    else
                    {
                        if (Convert.ToDouble(txtQtyRecQty.Text) <= Convert.ToDouble(txtNoOfQty.Text) )//|| Convert.ToDouble(txtQtyRecQty.Text) < Convert.ToDouble(txtBalancedQty.Text))
                        {
                            double ReceivedQty = Convert.ToDouble(txtQtyRecQty.Text) + Convert.ToDouble(txtReceivedQty.Text);
                            CalculateQty(ReceivedQty);
                            txtQtyRemark.Enabled = true;
                            txtQtyRemark.Focus();
                        }
                        else
                        {
                            OMMessageBox.Show("pls enter valid Qty", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                            txtQtyRecQty.Text = "";
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        private void txtQtyRecQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQtyRemark_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtQtyRemark.Text == "")
                    {
                        txtQtyRemark.Focus();
                    }
                    else
                    {
                        btnQtySave.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtRemarks_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtRemarks.Text == "")
                    {
                        BtnSave.Focus();
                    }
                    else
                    {
                        BtnSave.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtQtyRemark_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtQtyRemark.Text == "")
                    {
                        btnQtySave.Focus();
                    }
                    else
                    {
                        btnQtySave.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                dbtVoucherEntry = new DBTVaucherEntry();
                tvoucherentry = new TVoucherEntry();
                tvoucherentry.PkVoucherNo = ID;
                btnReceivedQty.Enabled = false;
                if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (dbtVoucherEntry.DeleteTVoucherEntry(tvoucherentry) == true)
                    {
                        OMMessageBox.Show("Item Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        FillFields();
                    }
                    else
                    {
                        OMMessageBox.Show("Item not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtRemarks_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtRemarks.Text == "")
                    {
                        txtRemarks.Focus();
                    }
                    else
                    {
                        BtnSave.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            Form NewF = new DetailsTranspoterList();
            this.Close();
            ObjFunction.OpenForm(NewF, DBGetVal.MainForm);
        }

      
    }
}
