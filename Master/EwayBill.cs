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
    public partial class EwayBill : Form
    {

        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        Yadi.Vouchers.SalesAPMC bill = new Yadi.Vouchers.SalesAPMC();
        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        MLedgerDetails mLedgerDetails = new MLedgerDetails();

        DBTEWayDetails dbtewaydetails = new DBTEWayDetails();
        TEWayDetails tewaydetails = new TEWayDetails();

        DataTable dtSearch = new DataTable();
        DataTable dtBill = new DataTable();

        long StateCode, CompanyCode, tempPartyNo = 0, Ledgerno = 0, statecode = 0, cityno = 0, BillNo = 0;
        bool State = true, FlagChange, ManualBill = false, enterflag = false, foundflag = false;
        public long ID, ShortID = 0;
        string LedgerName;
        DateTime BillDate;

        double distance = 0;

        public EwayBill()
        {
            InitializeComponent();
        }

        public EwayBill(long id, int billno, DataTable dt, long ledgerno, string ledgerName, DateTime billdate)
        {
            InitializeComponent();
            ShortID = id;
            txtBillNo.Text = billno.ToString();
            dtBill = dt;
            Ledgerno = ledgerno;
            LedgerName = ledgerName;
            BillNo = billno;
            BillDate = billdate;
        }

        public bool Validations()
        {
            bool flag = false;
            if (txtPinCode.Text != "")
            {
                if (txtPinCode.Text.Length == 6 || txtPinCode.Text.Length == 8)
                {
                    flag = true;
                }
                else
                {
                    OMMessageBox.Show("Invalid Pin Code ");
                    flag = false;
                    txtPinCode.Focus();
                }
                //return flag;

            } // Pin Code 

            if (txtGSTNo.Text == "")
            {

                if (txtGSTNo.Text.Length == 15)
                {
                    flag = true;
                }
                else
                {
                    MessageBox.Show("Customer's Gst No Not Valid");
                    flag = false;
                    txtGSTNo.Focus();
                }
                //return flag;

            } // GstNo



            if (txtVehicleNo.Text.Length > 2)
            {
                //                if (txtVehicleNo.Text.Length > 2)
                //              {
                flag = true;
            }

            else
            {

                flag = false;
            }


            if (flag == false)
            {
                if (txtTransporterGSTNo.Text.Length != 15 || txtLRDate.Text.Length == 0)
                {
                    OMMessageBox.Show("Transporter's GST No  or LrDate Not Proper ...");
                    flag = false;
                }

                else
                {
                    flag = true;
                }

            }
            return flag;
        }


        public void GenerateEWayBill()
        {
            string FileName;

            DataTable dtFirm = new DataTable();
            DataTable dtLedger = new DataTable();
            DataTable dtLedgerName = new DataTable();
            DataTable dtTEWay = new DataTable();
            DataTable dtStock = new DataTable();
            DataTable dtVoucher = new DataTable();

            StringBuilder StrBld = new StringBuilder();

            dtFirm = ObjFunction.GetDataView("select * from MFirm where firmno=" + DBGetVal.FirmNo, CommonFunctions.ConStr).Table;
            string FirmCity = ObjQry.ReturnString("select cityname from Mcity where cityno =" + Convert.ToInt32(dtFirm.Rows[0].ItemArray[5]), CommonFunctions.ConStr);
            string FirmState = (dtFirm.Rows[0].ItemArray[6]).ToString();//ObjQry.ReturnString("select StateName from MState where StateNo =" + Convert.ToInt32(dtFirm.Rows[0].ItemArray[6]), CommonFunctions.ConStr);
            string FirmPincode = (dtFirm.Rows[0].ItemArray[8]).ToString();
            string FirmAdd = (dtFirm.Rows[0].ItemArray[4]).ToString();
            string FirmGST = (dtFirm.Rows[0].ItemArray[14]).ToString();
            string FirmName = (dtFirm.Rows[0].ItemArray[2]).ToString();

           // Ledgerno = ObjQry.ReturnLong("Select Ledgerno from TVoucherEntry Where pkvoucherno=" + ShortID + " ", CommonFunctions.ConStr);
            dtLedger = ObjFunction.GetDataView("Select * from MLedgerDetails where Ledgerno=" + Ledgerno + " ", CommonFunctions.ConStr).Table;
            string LedgerGST = (dtLedger.Rows[0].ItemArray[26]).ToString();
            string LedgerAdd = (dtLedger.Rows[0].ItemArray[2]).ToString();
            string LedgerPincode = (dtLedger.Rows[0].ItemArray[5]).ToString();
            string LedgerState = (dtLedger.Rows[0].ItemArray[3]).ToString();//ObjQry.ReturnString("select StateName from MState where StateNo =" + Convert.ToInt32(dtLedger.Rows[0].ItemArray[3]), CommonFunctions.ConStr);
            string LedgerCity = ObjQry.ReturnString("select cityname from Mcity where cityno =" + Convert.ToInt32(dtLedger.Rows[0].ItemArray[4]), CommonFunctions.ConStr);

          //  dtLedgerName = ObjFunction.GetDataView("Select * from MLedger where Ledgerno=" + Ledgerno + " ", CommonFunctions.ConStr).Table;
         //   string LedgerName = (dtLedgerName.Rows[0].ItemArray[2]).ToString();

            dtTEWay = ObjFunction.GetDataView("Select * from TEWayDetails where FkVoucherNo=" + ShortID + " ", CommonFunctions.ConStr).Table;
            string transmode = (dtTEWay.Rows[0].ItemArray[5]).ToString();
            double transDistance = Convert.ToDouble(dtTEWay.Rows[0].ItemArray[6]);
            string vehicleNo = (dtTEWay.Rows[0].ItemArray[8]).ToString();
            DateTime transDocDate = Convert.ToDateTime(dtTEWay.Rows[0].ItemArray[4]);
          //  long transDocNo = (dtTEWay.Rows[0].ItemArray[1]).ToString();


            
            dtStock = ObjFunction.GetDataView("SELECT   sum( NetAmount+PackagingCharges) as NetAmount ,sum( SGSTAmount) as SGSTAmount, sum(CGSTAmount) as CGSTAmount,sum (IGSTAmount) as iGSTAmount,sum (CessAmount) as CessAmount FROM TStock where fkvoucherno=" + ShortID + " ", CommonFunctions.ConStr).Table;
            double  totalValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[0]).ToString());
            double sgstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[1]).ToString());
            double cgstValue =Convert.ToDouble( (dtStock.Rows[0].ItemArray[2]).ToString());
            double igstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[3]).ToString());
            double cessValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[4]).ToString());
         

            //dtVoucher = ObjFunction.GetDataView("Select * from TVoucherEntry where pkvoucherno=" + ShortID + " ", CommonFunctions.ConStr).Table;
            //string docDate = (dtVoucher.Rows[0].ItemArray[3]).ToString();
            //string docNo = (dtVoucher.Rows[0].ItemArray[2]).ToString();






            StrBld.Append("{" + System.Environment.NewLine);
            StrBld.Append((char)(34) + "version" + (char)(34) + ": " + (char)(34) + "1.0.0123" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "billLists" + (char)(34) + ": " + "[" + System.Environment.NewLine);
            StrBld.Append("{" + System.Environment.NewLine);
            StrBld.Append((char)(34) + "userGstin" + (char)(34) + ": " + (char)(34) + FirmGST.Substring(0,15) + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "supplyType" + (char)(34) + ": " + (char)(34) + "O" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "subSupplyType" + (char)(34) + ": " + "1" + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "docType" + (char)(34) + ": " + (char)(34) + "INV" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "docNo" + (char)(34) + ": " + (char)(34) + BillNo + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "docDate" + (char)(34) + ": " + (char)(34) + BillDate.ToString("dd/MM/yyyy") + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromGstin" + (char)(34) + ": " + (char)(34) + FirmGST.Substring(0, 15) + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromTrdName" + (char)(34) + ": " + (char)(34) + FirmName + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromAddr1" + (char)(34) + ": " + (char)(34) + FirmAdd + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromAddr2" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromPlace" + (char)(34) + ": " + (char)(34) + FirmCity + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromPincode" + (char)(34) + ": " + FirmPincode.Substring(0,6) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromStateCode" + (char)(34) + ": " + FirmState.Substring(0,2) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toGstin" + (char)(34) + ": " + (char)(34) + LedgerGST.Substring(0, 15) + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toTrdName" + (char)(34) + ": " + (char)(34) + LedgerName + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toAddr1" + (char)(34) + ": " + (char)(34) + LedgerAdd + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toAddr2" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toPlace" + (char)(34) + ": " + (char)(34) + LedgerCity + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toPincode" + (char)(34) + ": " + LedgerPincode.Substring(0, 6) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toStateCode" + (char)(34) + ": " + LedgerState.Substring(0, 2) + "," + System.Environment.NewLine);


            StrBld.Append((char)(34) + "totalValue" + (char)(34) + ": " + totalValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "cgstValue" + (char)(34) + ": " + cgstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "sgstValue" + (char)(34) + ": " + sgstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "igstValue" + (char)(34) + ": " + igstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "cessValue" + (char)(34) + ": " + cessValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transMode" + (char)(34) + ": " + transmode + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDistance" + (char)(34) + ": " + transDistance + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transporterName" + (char)(34) + ": " + (char)(34) + " " +(char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transporterId" + (char)(34) + ": " + (char)(34) + " " + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDocNo" + (char)(34) + ": " + (char)(34) + BillNo + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDocDate" + (char)(34) + ": " + (char)(34) + transDocDate.ToString("dd/MM/yyyy") + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "vehicleNo" + (char)(34) + ": " + (char)(34) + vehicleNo + (char)(34) + "," + System.Environment.NewLine);

            StrBld.Append((char)(34) + "itemList" + (char)(34) + ": " + "[" + System.Environment.NewLine);


            long itemno = 0;
            for (int i = 0; i <= dtBill.Rows.Count - 1; i++)
            {
                itemno = ObjQry.ReturnLong("select HSNCode from MItemMaster where ItemNo=" + dtBill.Rows[i].ItemArray[20].ToString(), CommonFunctions.ConStr);


                StrBld.Append("{" + System.Environment.NewLine);
                StrBld.Append((char)(34) + "itemNo" + (char)(34) + ": " + (i + 1) + "," + System.Environment.NewLine);
                StrBld.Append((char)(34) + "productName" + (char)(34) + ": " + (char)(34) + dtBill.Rows[i].ItemArray[1].ToString() + (char)(34) + "," + System.Environment.NewLine);//ItemName
                StrBld.Append((char)(34) + "productDesc" + (char)(34) + ": " + (char)(34) + dtBill.Rows[i].ItemArray[1].ToString() + (char)(34) + "," + System.Environment.NewLine);//ItemName
                StrBld.Append((char)(34) + "hsnCode" + (char)(34) + ": " + itemno + "," + System.Environment.NewLine);//
                StrBld.Append((char)(34) + "quantity" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[4] + "," + System.Environment.NewLine);//Quantity
                StrBld.Append((char)(34) + "qtyUnit" + (char)(34) + ": " + (char)(34) + dtBill.Rows[i].ItemArray[5] + (char)(34) + "," + System.Environment.NewLine);//UOM
                StrBld.Append((char)(34) + "taxableAmount" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[15] + "," + System.Environment.NewLine);//NetAmount
                StrBld.Append((char)(34) + "sgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[27] + "," + System.Environment.NewLine);//SGSTPer
                StrBld.Append((char)(34) + "cgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[34] + "," + System.Environment.NewLine);//CGSTPer
                StrBld.Append((char)(34) + "igstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[41] + "," + System.Environment.NewLine);//IGSTPer
                StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[70] + System.Environment.NewLine);//CessPer
                StrBld.Append("}" + "," + System.Environment.NewLine);
                StrBld.Append(System.Environment.NewLine);

            }



            StrBld.Append("]" + System.Environment.NewLine);
            StrBld.Append("}" + System.Environment.NewLine);
            StrBld.Append("]" + System.Environment.NewLine);
            StrBld.Append("}" + System.Environment.NewLine);


            FileName = CommonFunctions.DBFileOuputPath + "\\EWayBill.xml";
            System.IO.StreamWriter SW = new System.IO.StreamWriter(FileName, false);
            SW.WriteLine(" ");
            SW.Close();
            SW = new System.IO.StreamWriter(FileName, true);
            
            SW.WriteLine(StrBld);
            SW.Close();
            StrBld = new StringBuilder();
           

        }



        private void EwayBill_Load(object sender, EventArgs e)
        {

            //txtEwayBill.Enabled = false;
            dtpBillDate.Enabled = false;
            FillList();
            // txtLRDate.CustomFormat = " ";

            ID = ObjQry.ReturnLong("Select PKEWayNo from TEWayDetails Where fkvoucherno=" + ShortID + " ", CommonFunctions.ConStr);
            if (ID != 0)
            {
                grpBoxTransDetails.Enabled = false;
                grpBoxCustDetails.Enabled = false;
                FillControls(ID);

            }
            else
            {
                grpBoxTransDetails.Enabled = true;
                txtState.Enabled = false;
                txtMode.Select();
                FillControls(ShortID);

            }


            //if (lstTransporter.SelectedItems.Count <= 0)
            //{
            //    MessageBox.Show("Please Insert Transporter details ........");

            //}






        }


        public void FillBrand()
        {

        }

        private void FillControls(long id)
        {
            try
            {

                if (id == ID)
                {
                    tewaydetails = dbtewaydetails.ModifyTEWayDetailsByID(ID);
                    lstMode.SelectedValue = tewaydetails.ModeNo.ToString();
                    txtMode.Text = lstMode.Text;
                    txtDistance.Text = tewaydetails.Distance.ToString();
                    txtAddress.Text = tewaydetails.Address;
                    txtTransporter.Text = tewaydetails.TransportNo.ToString();
                    txtPinCode.Text = tewaydetails.PinCode.ToString();
                    txtLRDate.Value = tewaydetails.EWayDate;
                    txtVehicleNo.Text = tewaydetails.VehicleNo;
                    txtLRNo.Text = tewaydetails.LRNo.ToString();
                    txtCustomerName.Text = tewaydetails.LedgerName;
                    txtState.Text = tewaydetails.StateName;
                    txtCity.Text = tewaydetails.CityName;
                    Ledgerno = ObjQry.ReturnLong("Select Ledgerno from TVoucherEntry Where pkvoucherno=" + ShortID + " ", CommonFunctions.ConStr);
                    // mLedger = dbLedger.ModifyMLedgerByID(Ledgerno);
                    mLedgerDetails = dbLedger.ModifyMLedgerDetailsByID(Ledgerno);
                    txtGSTNo.Text = mLedgerDetails.GSTNo;


                }
                else
                {
                    Ledgerno = ObjQry.ReturnLong("Select Ledgerno from TVoucherEntry Where pkvoucherno=" + ShortID + " ", CommonFunctions.ConStr);
                    mLedger = dbLedger.ModifyMLedgerByID(Ledgerno);
                    mLedgerDetails = dbLedger.ModifyMLedgerDetailsByID(Ledgerno);
                    txtDistance.Text = mLedgerDetails.Distance.ToString();
                    txtCustomerName.Text = mLedger.LedgerName;

                    statecode = mLedger.StateCode;
                    string statename = ObjQry.ReturnString("Select statename from MState Where statecode=" + statecode + " ", CommonFunctions.ConStr);
                    txtState.Text = statename;

                    cityno = mLedgerDetails.CityNo;
                    string cityname = ObjQry.ReturnString("Select cityname from Mcity Where cityno=" + cityno + " ", CommonFunctions.ConStr);
                    txtCity.Text = cityname;

                    txtGSTNo.Text = mLedgerDetails.GSTNo;
                    txtPinCode.Text = mLedgerDetails.PinCode;
                    txtAddress.Text = mLedgerDetails.Address;
                }

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }


        public void FillList()
        {
            ObjFunction.FillList(lstMode, "Select TransModeNo,TransModeName From MTransporterMode order by TransModeName");
            ObjFunction.FillList(lstTransporter, "Select LedgerNo,LedgerName From MLedger WHERE GroupNo = " + GroupType.Transporter + " ");

        }

        private void lstMode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtMode.Text = lstMode.Text;

                    txtDistance.Focus();
                    pnlMode.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtMode.Focus();
                pnlMode.Visible = false;

            }
        }

        private void txtMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                if (txtMode.Text == "")
                {

                    if (lstMode.Items.Count > 0)
                    {
                        pnlMode.Visible = true;
                        lstMode.Focus();
                    }
                    else
                    {
                        pnlMode.Visible = false;
                        txtMode.Focus();
                    }

                }
                else
                {
                    pnlMode.Visible = false;
                    txtDistance.Focus();

                }
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtMode.Text = "";
            }
            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void lstPartyEnglish_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ObjFunction.GetAppSettings(AppSettings.O_Bilingual)) == true)
            {
                //  if (lstPartyEnglish.Items.Count > 0)
                //lstPartyLang.SelectedIndex = lstPartyEnglish.SelectedIndex;

                //  Ledgerno = Convert.ToInt64(lstPartyEnglish.SelectedValue);

                //  if (ID == 0) tempPartyNo = Convert.ToInt64(lstPartyEnglish.SelectedValue);
            }
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{
            //    if (txtCustomerName.Text == "")
            //    {
            //        pnlParty.Visible = true;
            //        lstPartyEnglish.Focus();
            //    }
            //    else
            //    {
            //        pnlParty.Visible = false;
            //        txtBillNo.Focus();

            //    }
            //}
            //else if (e.KeyChar == Convert.ToChar(Keys.Back))
            //{
            //    txtCustomerName.Text = "";
            //}
            //else
            //{
            //    txtCustomerName.Text = "";
            //    pnlParty.Visible = true;
            //    lstPartyEnglish.Focus();
            //}

            if (txtEwayBill.Text == null)
            {
                txtEwayBill.Text = ".";
            }
            else if (txtCustomerName.Text == null)
            {
                txtCustomerName.Text = ".";
            }
            else
            {
                txtCustomerName.Focus();
            }
        }

        private void lstPartyEnglish_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    //txtCustomerName.Text = lstPartyEnglish.Text;
                    Ledgerno = Convert.ToInt32(lstPartyEnglish.SelectedValue);
                    mLedger = dbLedger.ModifyMLedgerByID(Ledgerno);
                    mLedgerDetails = dbLedger.ModifyMLedgerDetailsByID(Ledgerno);
                    txtCustomerName.Text = mLedger.LedgerName;
                    txtAddress.Text = mLedgerDetails.Address;
                    txtPinCode.Text = mLedgerDetails.PinCode;
                    txtPinCode.Enabled = false;
                    txtGSTNo.Text = mLedgerDetails.GSTNo;
                    txtGSTNo.Enabled = false;
                    lstCityName.SelectedValue = mLedgerDetails.CityNo.ToString();
                    txtCity.Text = lstCityName.Text;
                    txtCity.Enabled = false;
                    //lstStateName.SelectedValue = mLedgerDetails.StateNo.ToString();
                    //txtState.Text = lstStateName.Text;
                    txtState.Enabled = false;

                    // BtnSave.Focus();
                    pnlParty.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtCustomerName.Focus();
                pnlParty.Visible = false;

            }



        }

        private void BtnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }



        private void txtTransporter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {


                if (txtTransporter.Text == "")
                {
                    if (lstTransporter.Items.Count > 0)
                    {
                        pnlTransporter.Visible = true;
                        lstTransporter.Focus();
                    }
                    else
                    {
                        pnlTransporter.Visible = false;
                        txtTransporter.Focus();
                    }
                }

                else
                {

                    pnlTransporter.Visible = false;

                    txtTransporterGSTNo.Focus();
                }
            }



            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {
                txtTransporter.Text = "";
            }

            else
            {
                e.KeyChar = Convert.ToChar(0);
            }
        }

        private void txtTransporterGSTNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtTransporterGSTNo.Text.Length == 0 || txtTransporterGSTNo.Text.Length == 15)
                {

                    txtLRDate.Focus();
                }
                else
                {
                    MessageBox.Show("Gst no should be 15 digit or blank ....");
                    txtTransporterGSTNo.Focus();
                }
            } // enter key 
        }
        private void txtVehicleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //    txtLRNo.Focus();
                //}

                if (txtVehicleNo.Text == null)
                {
                    txtVehicleNo.Text = ".";
                }
                else
                {
                    txtLRNo.Focus();

                }
                //                else
                //                {
                //                    int j, i;

                //                    bool chkveh = true;
                //                    for (i = 1; i <= txtVehicleNo.Text.Length; i++)
                //                    {
                ////                        j = Convert.ToInt32(Asc(txtVehicleNo.Text.Substring(i, 1)));
                //                        //switch(j)





                //                        j = 6; // txtVehicleNo.Text.Substring(i, 1);



                //                        if (j >= 97 && j <= 122)
                //                        {
                //                            break;
                //                        }

                //                        else if (j >= 65 && j <= 90)
                //                        {
                //                            break;
                //                        }


                //                        else if (j >= 48 && j <= 57)
                //                        {
                //                        }

                //                        else
                //                        {
                //                            j = i;
                //                            chkveh = false;

                //                        }
                //                    } // for 


                //                    if (chkveh == false)
                //                    {
                //                        OMMessageBox.Show((txtVehicleNo.Text.ToString().Substring(i, 1)) + " " + "not allowed");
                //                        txtVehicleNo.Focus();
                //                    }
                //                    else
                //                    {
                //                        txtLRNo.Focus();
                //                    }

                // } // esle 
            } // enter key 

        } // function 





        private void txtLRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtAddress.Focus();
            }

            //if (txtLRNo.Text == null) 
            //{
            //    txtLRNo.Text = ".";
            //    txtCustomerName.Focus();
            //}
        }

        private void txtLRDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                string i, j;
                if (txtLRDate.Text.Length == 0)
                {
                    txtLRDate.CustomFormat = "dd-MMM-yy";
                    if (txtLRDate.Text.Length == 0)
                    {
                        // txtVehicleNo.Text = "";
                        txtVehicleNo.Focus();
                    }
                }
                else
                {
                    txtVehicleNo.Focus();
                }
                //else if (txtLRDate.Value.ToString() == null)
                //{
                //    i = txtLRDate.Text.ToString().Substring(7, 4) + " " + txtLRDate.Text.ToString().Substring(4, 2) + " " + txtLRDate.Text.ToString().Substring(1, 2);
                //    j = txtLRDate.Text.ToString().Substring(7, 4) + " " + txtLRDate.Text.ToString().Substring(4, 2) + " " + txtLRDate.Text.ToString().Substring(1, 2);

                //    if (i.Length >= j.Length)
                //    {
                //        txtVehicleNo.Text = "";
                //        txtVehicleNo.Focus();
                //    }

                //    else
                //    {
                //        OMMessageBox.Show(" Invalid Date ....");
                //        txtLRDate.Focus();
                //    }
                //}

                //else
                //{
                //    OMMessageBox.Show(" Invalid Date ....");
                //    txtLRDate.Focus();
                //}

                //}

            } // if key enter           
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {


            //if (txtEwayBill.Text == null)
            //{
            //    txtEwayBill.Text = ".";
            //}
            //else if (txtAddress.Text == null)
            //{
            //    txtAddress.Text = ".";
            //}
            //else
            //{
            //    txtAddress.Focus();
            //}

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (enterflag == false)
                {
                    enterflag = true;
                }
                else
                {
                    txtCity.Focus();
                    enterflag = false;
                }
            }
            else
            {
                enterflag = false;
            }







        }

        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

                //    if (txtCity.Text == "")
                //    {

                //        if (lstCityName.Items.Count > 0)
                //        {
                //            pnlCityName.Visible = true;
                //            lstCityName.Focus();
                //        }
                //        else
                //        {
                //            pnlCityName.Visible = false;
                //            txtCity.Focus();
                //        }

                //    }
                //    else
                //    {
                //        pnlCityName.Visible = false;
                //        txtPinCode.Focus();

                //    }
                //}
                //else if (e.KeyChar == Convert.ToChar(Keys.Back))
                //{
                //    txtCity.Text = "";
                //}
                //else
                //{
                //    e.KeyChar = Convert.ToChar(0);
                //}

                if (txtEwayBill.Text == null)
                {
                    txtEwayBill.Text = ".";
                }
                else if (txtCity.Text == null)
                {
                    txtCity.Text = ".";
                }
                else
                {
                    txtPinCode.Focus();
                }
            }
        }

        private void txtPinCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtPinCode.Text == null)
                {
                    txtPinCode.Text = ".";
                }

                else if (txtGSTNo.Enabled == true)
                {
                    txtGSTNo.Focus();
                }
                else
                {
                    txtGSTNo.Select();
                }
            }
        }

        private void txtState_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (e.KeyChar == Convert.ToChar(Keys.Enter))
            //{

            //    if (txtState.Text == "")
            //    {

            //        if (lstStateName.Items.Count > 0)
            //        {
            //            pnlStateName.Visible = true;
            //            lstStateName.Focus();
            //        }
            //        else
            //        {
            //            pnlStateName.Visible = false;
            //            txtState.Focus();
            //        }

            //    }
            //    else
            //    {
            //        pnlStateName.Visible = false;
            //        txtGSTNo.Focus();

            //    }
            //}
            //else if (e.KeyChar == Convert.ToChar(Keys.Back))
            //{
            //    txtState.Text = "";
            //}
            //else
            //{
            //    e.KeyChar = Convert.ToChar(0);
            //}

            if (txtEwayBill.Text == null)
            {
                txtEwayBill.Text = ".";
            }
            else if (txtGSTNo.Text == null)
            {
                txtGSTNo.Text = ".";
                txtGSTNo.Focus();
            }


        }

        private void txtGSTNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnSave.Focus();
            }
        }

        private void txtEwayBill_KeyPress(object sender, KeyPressEventArgs e)
        {

            //if (txtEwayBill.Text.Trim().Length > 0)
            //{
            //    if (txtDistance.Text == null)
            //    {
            //        txtDistance.Text = "1";
            //        txtDistance.Focus();
            //    }
            //    else
            //    {
            //        lstMode.Visible = true;
            //        lstMode.SelectedIndex = 0;
            //        lstMode.Focus();
            //    }

            //}
        }

        private void dtpBillDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtMode.Focus();
            }
        }

        private void txtDistance_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 9, OMFunctions.MaskedType.NotNegative);
            //ObjFunction.SetMaskedNumeric((TextBox)sender);
        }

        private void dtpBillDate_ValueChanged(object sender, EventArgs e)
        {
            FillBrand();
        }

        private void lstTransporter_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyCode == Keys.Enter)
                //{


                //lstTransporter.Items.Add(new Item("NA", "0"));
                //lstTransporter.Items.Add(new Item(".", "1"));


                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        e.SuppressKeyPress = true;
                        txtTransporter.Text = lstTransporter.Text;

                        txtLRDate.Focus();
                        pnlTransporter.Visible = false;
                        if (txtTransporter.Text == "NA")
                        {
                            txtTransporterGSTNo.Enabled = false;
                            txtVehicleNo.Focus();
                        }
                        else if (txtTransporter.Text == ".")
                        {
                            pnlTransporter.Visible = false;
                            txtTransporter.Focus();
                        }
                    }
                    catch (Exception exc)
                    {
                        ObjFunction.ExceptionDisplay(exc.Message);
                    }

                }
                else if (e.KeyCode == Keys.Escape)
                {
                    e.SuppressKeyPress = true;
                    txtTransporter.Focus();
                    pnlTransporter.Visible = false;

                }
                //}
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }

        }

        private void txtDistance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                //txtDistance_Leave(sender, new EventArgs());


                if (txtDistance.Text != null)
                {

                    if (txtTransporter.Text.Length == 0)
                    {
                        txtTransporter.Text = ".";

                    }
                    txtTransporter.Focus();
                }
                else
                {
                    OMMessageBox.Show(" Please enter distance in KMS ... ");
                    txtDistance.Text = "1";

                    txtDistance.Focus();
                }
            }

        }

        //private void txtDistance_Leave(object sender, EventArgs e)
        //{
        ////    if (txtDistance.Text.Trim() == "")
        ////    {
        ////        txtDistance.Focus();

        ////    }
        ////    else
        ////    {

        ////        if (txtTransporter.Text == "")
        ////        {
        ////            if (lstTransporter.SelectedItems.Count <= 0)
        ////            {
        ////                MessageBox.Show("Please Insert Transporter details ........");

        ////            }
        ////            else
        ////            {

        ////                pnlTransporter.Visible = true;
        ////                lstTransporter.Focus();
        ////            }
        ////        }


        ////    }
        ////    else
        ////        { txtTransporter.Focus(); }
        //}

        private void lstCityName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    e.SuppressKeyPress = true;
                    txtCity.Text = lstCityName.Text;

                    txtPinCode.Focus();
                    pnlCityName.Visible = false;
                }
                catch (Exception exc)
                {
                    ObjFunction.ExceptionDisplay(exc.Message);
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                txtCity.Focus();
                pnlCityName.Visible = false;

            }
        }

        //private void lstStateName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        try
        //        {
        //            e.SuppressKeyPress = true;
        //            //txtState.Text = lstStateName.Text;

        //            txtGSTNo.Focus();
        //            //pnlStateName.Visible = false;
        //        }
        //        catch (Exception exc)
        //        {
        //            ObjFunction.ExceptionDisplay(exc.Message);
        //        }

        //    }
        //    else if (e.KeyCode == Keys.Escape)
        //    {
        //        e.SuppressKeyPress = true;
        //        txtGSTNo.Focus();
        //        //                pnlStateName.Visible = false;

        //    }
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        dbLedger = new DBMLedger();
        //        mLedger = new MLedger();
        //        mLedger.LedgerNo = ID;

        //        if (ObjQry.ReturnLong("Select Count(*) from TVoucherEntry Inner Join TvoucherDetails on TVoucherEntry.PkVoucherNo=TVoucherDetails.FkVoucherNo where TVoucherEntry.VoucherDate>='" + DBGetVal.FromDate + "' and TVoucherEntry.VoucherDate<='" + DBGetVal.ToDate + "' and TVoucherDetails.LedgerNo=" + ID + "", CommonFunctions.ConStr) > 0)
        //        {
        //            OMMessageBox.Show("Sorry You can not delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
        //        }
        //        else
        //        {

        //            if (OMMessageBox.Show("Are you sure want to delete this record?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
        //            {
        //                if (dbLedger.DeleteMLedger(mLedger) == true)
        //                {
        //                    OMMessageBox.Show("EWay Bill Deleted Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
        //                }
        //                else
        //                {
        //                    OMMessageBox.Show("EWay Bill not Deleted", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception exc)
        //    {
        //        ObjFunction.ExceptionDisplay(exc.Message);
        //    }
        //}

        private void txtBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtAddress.Focus();
            }
        }

        private void txtBillNo_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked(((TextBox)sender), 2, 9, OMFunctions.MaskedType.NotNegative);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SetValue();
        }

        public void SetValue()
        {
            try
            {
                if (Validations() == true)
                {
                    dbtewaydetails = new DBTEWayDetails();
                    tewaydetails = new TEWayDetails();
                    tewaydetails.PKEWayNo = ID;

                    tewaydetails.EWayDate = Convert.ToDateTime(dtpBillDate.Value);
                    tewaydetails.ModeNo = Convert.ToInt32(lstMode.SelectedValue);
                    tewaydetails.VoucherUserNo = Convert.ToInt32(txtBillNo.Text.Trim());
                    tewaydetails.Distance = Convert.ToDouble(txtDistance.Text.Trim());
                    //tewaydetails.TransportNo = Convert.ToInt64(txtTransporter.Text.Trim());
                    tewaydetails.VehicleNo = txtVehicleNo.Text.Trim();
                    tewaydetails.LRNo = Convert.ToInt32(txtLRNo.Text.Trim());
                    tewaydetails.LRDate = Convert.ToDateTime(txtLRDate.Value);
                    tewaydetails.LedgerNo = Ledgerno;
                    tewaydetails.LedgerName = txtCustomerName.Text.Trim();
                    tewaydetails.Address = txtAddress.Text.Trim();
                    tewaydetails.CityNo = cityno;
                    tewaydetails.CityName = txtCity.Text.Trim();
                    tewaydetails.PinCode = Convert.ToInt64(txtPinCode.Text.Trim());
                    tewaydetails.StateCode = statecode;
                    tewaydetails.StateName = txtState.Text.Trim();
                    tewaydetails.UserID = DBGetVal.UserID;
                    tewaydetails.FkVoucherNo = ShortID;
                    tewaydetails.UserDate = DBGetVal.ServerTime.Date;
                    //mLedgerDetails.GSTNo = Convert.ToString(txtGSTNo.Text);
                    tewaydetails.IsActive = true;


                    if (dbtewaydetails.AddTEWayDetails(tewaydetails) == true)
                    {
                        if (ID == 0)
                        {
                            OMMessageBox.Show("EWay Bill Added Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            dtSearch = ObjFunction.GetDataView("Select PKEWayNo From TEWayDetails").Table;
                            ID = ObjQry.ReturnLong("Select Max(PKEWayNo) From TEWayDetails", CommonFunctions.ConStr);
                            FillControls(ID);
                        }
                        //else
                        //{
                        //    OMMessageBox.Show("EWay Bill Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        //    //FillControls();
                        //}

                        ObjFunction.LockButtons(true, this.Controls);
                        ObjFunction.LockControls(false, this.Controls);
                    }
                    else
                    {
                        OMMessageBox.Show("EWay Bill not saved", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnGenerateFile_KeyPress(object sender, KeyPressEventArgs e)
        {
            GenerateEWayBill();
        }

        private void BtnGenerateFile_Click(object sender, EventArgs e)
        {
            GenerateEWayBill();
        }







    }
}
