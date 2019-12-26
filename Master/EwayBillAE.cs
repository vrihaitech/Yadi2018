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
    public partial class EwayBillAE : Form
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
        long Ledgerno = 0, statecode = 0, cityno = 0, BillNo = 0;
        bool enterflag = false;
        public long ID, ShortID = 0;
        string LedgerName;
        double BilledAmount = 0.0;
        DateTime BillDate;
    
        public EwayBillAE()
        {
            InitializeComponent();
        }

        public EwayBillAE(long id, int billno, DataTable dt, long ledgerno, string ledgerName, DateTime billdate, double amount)
        {
            InitializeComponent();
            ShortID = id;
            txtBillNo.Text = billno.ToString();
            dtBill = dt;
            Ledgerno = ledgerno;
            LedgerName = ledgerName;
            BillNo = billno;
            BillDate = billdate;
            BilledAmount = amount;
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
                    //OMMessageBox.Show("Invalid Pin Code ");

                    MessageBox.Show("Please Enter valid pincode...");
                    flag = false;
                    txtPinCode.Focus();
                }
                return flag;

            } // Pin Code 
            if (Convert.ToDouble(txtDistance.Text) <= 1)
            {
                flag = false;
                MessageBox.Show("Please Enter valid Distance...");
                txtDistance.Focus();
            }
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
            if ((lstMode.SelectedValue == null))
            { flag = false; }


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


        public void GenerateEWayBillold()
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
            double totalValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[0]).ToString());
            double sgstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[1]).ToString());
            double cgstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[2]).ToString());
            double igstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[3]).ToString());
            double cessValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[4]).ToString());
            string transGSTNO = "";
            if ((txtTransporterGSTNo.Text.Trim() != null) || (txtTransporterGSTNo.Text.Trim() != ""))
            {
                transGSTNO = txtTransporterGSTNo.Text.Trim();
            }

            StrBld.Append("{" + System.Environment.NewLine);
            StrBld.Append((char)(34) + "version" + (char)(34) + ":" + (char)(34) + "1.0.0618" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "billLists" + (char)(34) + ":" + "[" + System.Environment.NewLine);
            StrBld.Append("{" + System.Environment.NewLine);
            StrBld.Append((char)(34) + "userGstin" + (char)(34) + ": " + (char)(34) + FirmGST.Substring(0, 15) + (char)(34) + "," + System.Environment.NewLine);
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
            StrBld.Append((char)(34) + "fromPincode" + (char)(34) + ": " + FirmPincode.Substring(0, 6) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromStateCode" + (char)(34) + ": " + FirmState.Substring(0, 2) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "actualFromStateCode" + (char)(34) + ": " + FirmState.Substring(0, 2) + "," + System.Environment.NewLine);

            StrBld.Append((char)(34) + "toGstin" + (char)(34) + ": " + (char)(34) + LedgerGST.Substring(0, 15) + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toTrdName" + (char)(34) + ": " + (char)(34) + LedgerName + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toAddr1" + (char)(34) + ": " + (char)(34) + LedgerAdd + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toAddr2" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toPlace" + (char)(34) + ": " + (char)(34) + LedgerCity + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toPincode" + (char)(34) + ": " + txtPinCode.Text + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toStateCode" + (char)(34) + ": " + LedgerState + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "actualToStateCode" + (char)(34) + ": " + LedgerState + "," + System.Environment.NewLine);

            StrBld.Append((char)(34) + "totalValue" + (char)(34) + ": " + totalValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "cgstValue" + (char)(34) + ": " + cgstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "sgstValue" + (char)(34) + ": " + sgstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "igstValue" + (char)(34) + ": " + igstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "cessValue" + (char)(34) + ": " + cessValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "totInvValue" + (char)(34) + ": " + BilledAmount + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transMode" + (char)(34) + ": " + transmode + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDistance" + (char)(34) + ": " + txtDistance.Text + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transporterName" + (char)(34) + ": " + (char)(34) + txtTransporter.Text.ToUpper().Trim() + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transporterId" + (char)(34) + ": " + (char)(34) + transGSTNO + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDocNo" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDocDate" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "vehicleNo" + (char)(34) + ": " + (char)(34) + vehicleNo + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "vehicleType" + (char)(34) + ":" + (char)(34) + "R" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "itemList" + (char)(34) + ": " + "[" + System.Environment.NewLine);


            long itemno = 0;
            for (int i = 0; i <= dtBill.Rows.Count - 1; i++)
            {
                itemno = ObjQry.ReturnLong("select HSNCode from MItemMaster where ItemNo=" + dtBill.Rows[i].ItemArray[20].ToString(), CommonFunctions.ConStr);
                if (i >= 1)
                {
                    StrBld.Append("," + System.Environment.NewLine);
                }

                StrBld.Append("{" + System.Environment.NewLine);
                StrBld.Append((char)(34) + "itemNo" + (char)(34) + ": " + (i + 1) + "," + System.Environment.NewLine);
                StrBld.Append((char)(34) + "productName" + (char)(34) + ": " + (char)(34) + dtBill.Rows[i].ItemArray[1].ToString() + (char)(34) + "," + System.Environment.NewLine);//ItemName
                StrBld.Append((char)(34) + "productDesc" + (char)(34) + ": " + (char)(34) + dtBill.Rows[i].ItemArray[1].ToString() + (char)(34) + "," + System.Environment.NewLine);//ItemName
                StrBld.Append((char)(34) + "hsnCode" + (char)(34) + ": " + itemno + "," + System.Environment.NewLine);//
                StrBld.Append((char)(34) + "quantity" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[4] + "," + System.Environment.NewLine);//Quantity
                StrBld.Append((char)(34) + "qtyUnit" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);//UOM
                StrBld.Append((char)(34) + "taxableAmount" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[15] + "," + System.Environment.NewLine);//NetAmount
                                                                                                                                                  //StrBld.Append((char)(34) + "sgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[27] + "," + System.Environment.NewLine);//SGSTPer
                if (FirmState.Substring(0, 2) == LedgerState)
                {
                    StrBld.Append((char)(34) + "sgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[27] + "," + System.Environment.NewLine);//SGSTPer
                    StrBld.Append((char)(34) + "cgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[34] + "," + System.Environment.NewLine);//CGSTPer
                    StrBld.Append((char)(34) + "igstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[41] + "," + System.Environment.NewLine);//IGSTPer
                                                                                                                                                 //StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[70] + System.Environment.NewLine);//CessPer
                    StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": 0" + System.Environment.NewLine);//CessPer
                }
                else
                {
                    StrBld.Append((char)(34) + "sgstRate" + (char)(34) + ": 0," + System.Environment.NewLine);//SGSTPer
                    StrBld.Append((char)(34) + "cgstRate" + (char)(34) + ": 0," + System.Environment.NewLine);//CGSTPer
                    StrBld.Append((char)(34) + "igstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[27] + "," + System.Environment.NewLine);//IGSTPer
                                                                                                                                                 //StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[70] + System.Environment.NewLine);//CessPer
                    StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": 0" + System.Environment.NewLine);//CessPer }
                }
                StrBld.Append("}");


            }

            StrBld.Append(System.Environment.NewLine);
            StrBld.Append("]" + System.Environment.NewLine);
            StrBld.Append("}" + System.Environment.NewLine);
            StrBld.Append("]" + System.Environment.NewLine);
            StrBld.Append("}" + System.Environment.NewLine);


            FileName = CommonFunctions.DBFileOuputPath + "\\EWayBill_" + BillNo + ".json";
            System.IO.StreamWriter SW = new System.IO.StreamWriter(FileName, false);
            SW.WriteLine(" ");
            SW.Close();
            SW = new System.IO.StreamWriter(FileName, true);

            SW.WriteLine(StrBld);
            SW.Close();
            StrBld = new StringBuilder();

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
            double totalValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[0]).ToString());
            double sgstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[1]).ToString());
            double cgstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[2]).ToString());
            double igstValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[3]).ToString());
            double cessValue = Convert.ToDouble((dtStock.Rows[0].ItemArray[4]).ToString());
            string transGSTNO = "";
            if ((txtTransporterGSTNo.Text.Trim() != null) || (txtTransporterGSTNo.Text.Trim() != ""))
            {
                transGSTNO = txtTransporterGSTNo.Text.Trim();
            }

            StrBld.Append("{" + System.Environment.NewLine);
            //StrBld.Append((char)(34) + "version" + (char)(34) + ":" + (char)(34) + "1.0.0618" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "version" + (char)(34) + ":" + (char)(34) + "1.0.0219" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "billLists" + (char)(34) + ":" + "[" + System.Environment.NewLine);
            StrBld.Append("{" + System.Environment.NewLine);
            StrBld.Append((char)(34) + "userGstin" + (char)(34) + ": " + (char)(34) + FirmGST.Substring(0, 15) + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "supplyType" + (char)(34) + ": " + (char)(34) + "O" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "subSupplyType" + (char)(34) + ": " + "1" + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "docType" + (char)(34) + ": " + (char)(34) + "INV" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "docNo" + (char)(34) + ": " + (char)(34) + BillNo + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "docDate" + (char)(34) + ": " + (char)(34) + BillDate.ToString("dd/MM/yyyy") + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transType" + (char)(34) + ": 1," + System.Environment.NewLine);

            StrBld.Append((char)(34) + "fromGstin" + (char)(34) + ": " + (char)(34) + FirmGST.Substring(0, 15) + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromTrdName" + (char)(34) + ": " + (char)(34) + FirmName + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromAddr1" + (char)(34) + ": " + (char)(34) + FirmAdd + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromAddr2" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromPlace" + (char)(34) + ": " + (char)(34) + FirmCity + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromPincode" + (char)(34) + ": " + FirmPincode.Substring(0, 6) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "fromStateCode" + (char)(34) + ": " + FirmState.Substring(0, 2) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "actualFromStateCode" + (char)(34) + ": " + FirmState.Substring(0, 2) + "," + System.Environment.NewLine);

            StrBld.Append((char)(34) + "toGstin" + (char)(34) + ": " + (char)(34) + LedgerGST.Substring(0, 15) + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toTrdName" + (char)(34) + ": " + (char)(34) + LedgerName + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toAddr1" + (char)(34) + ": " + (char)(34) + LedgerAdd + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toAddr2" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toPlace" + (char)(34) + ": " + (char)(34) + LedgerCity + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toPincode" + (char)(34) + ": " + txtPinCode.Text + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "toStateCode" + (char)(34) + ": " + LedgerState + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "actualToStateCode" + (char)(34) + ": " + LedgerState + "," + System.Environment.NewLine);

            StrBld.Append((char)(34) + "totalValue" + (char)(34) + ": " + totalValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "cgstValue" + (char)(34) + ": " + cgstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "sgstValue" + (char)(34) + ": " + sgstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "igstValue" + (char)(34) + ": " + igstValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "cessValue" + (char)(34) + ": " + cessValue + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "TotNonAdvolVal" + (char)(34) + ": 0," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "OthValue" + (char)(34) + ": 0," + System.Environment.NewLine);


            StrBld.Append((char)(34) + "totInvValue" + (char)(34) + ": " + BilledAmount + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transMode" + (char)(34) + ": " + transmode + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDistance" + (char)(34) + ": " + txtDistance.Text + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transporterName" + (char)(34) + ": " + (char)(34) + txtTransporter.Text.ToUpper().Trim() + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transporterId" + (char)(34) + ": " + (char)(34) + transGSTNO + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDocNo" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "transDocDate" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "vehicleNo" + (char)(34) + ": " + (char)(34) + vehicleNo + (char)(34) + "," + System.Environment.NewLine);
            StrBld.Append((char)(34) + "vehicleType" + (char)(34) + ":" + (char)(34) + "R" + (char)(34) + "," + System.Environment.NewLine);



            long HSNcode = 0;
            HSNcode = ObjQry.ReturnLong("select HSNCode from MItemMaster where ItemNo=" + dtBill.Rows[0].ItemArray[20].ToString(), CommonFunctions.ConStr);
            StrBld.Append((char)(34) + "mainHsnCode" + (char)(34) + ":" + HSNcode + "," + System.Environment.NewLine);


            StrBld.Append((char)(34) + "itemList" + (char)(34) + ": " + "[" + System.Environment.NewLine);
            for (int i = 0; i <= dtBill.Rows.Count - 1; i++)
            {
                HSNcode = ObjQry.ReturnLong("select HSNCode from MItemMaster where ItemNo=" + dtBill.Rows[i].ItemArray[20].ToString(), CommonFunctions.ConStr);
                if (i >= 1)
                {
                    StrBld.Append("," + System.Environment.NewLine);
                }

                StrBld.Append("{" + System.Environment.NewLine);
                StrBld.Append((char)(34) + "itemNo" + (char)(34) + ": " + (i + 1) + "," + System.Environment.NewLine);
                StrBld.Append((char)(34) + "productName" + (char)(34) + ": " + (char)(34) + dtBill.Rows[i].ItemArray[1].ToString() + (char)(34) + "," + System.Environment.NewLine);//ItemName
                StrBld.Append((char)(34) + "productDesc" + (char)(34) + ": " + (char)(34) + dtBill.Rows[i].ItemArray[1].ToString() + (char)(34) + "," + System.Environment.NewLine);//ItemName
                StrBld.Append((char)(34) + "hsnCode" + (char)(34) + ": " + HSNcode + "," + System.Environment.NewLine);//
                StrBld.Append((char)(34) + "quantity" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[4] + "," + System.Environment.NewLine);//Quantity
                StrBld.Append((char)(34) + "qtyUnit" + (char)(34) + ": " + (char)(34) + "" + (char)(34) + "," + System.Environment.NewLine);//UOM
                StrBld.Append((char)(34) + "taxableAmount" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[15] + "," + System.Environment.NewLine);//NetAmount
                                                                                                                                                  //StrBld.Append((char)(34) + "sgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[27] + "," + System.Environment.NewLine);//SGSTPer
                if (FirmState.Substring(0, 2) == LedgerState)
                {
                    StrBld.Append((char)(34) + "sgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[27] + "," + System.Environment.NewLine);//SGSTPer
                    StrBld.Append((char)(34) + "cgstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[34] + "," + System.Environment.NewLine);//CGSTPer
                    StrBld.Append((char)(34) + "igstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[41] + "," + System.Environment.NewLine);//IGSTPer
                                                                                                                                                 //StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[70] + System.Environment.NewLine);//CessPer
                    StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": 0" + System.Environment.NewLine);//CessPer
                }
                else
                {
                    StrBld.Append((char)(34) + "sgstRate" + (char)(34) + ": 0," + System.Environment.NewLine);//SGSTPer
                    StrBld.Append((char)(34) + "cgstRate" + (char)(34) + ": 0," + System.Environment.NewLine);//CGSTPer
                    StrBld.Append((char)(34) + "igstRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[27] + "," + System.Environment.NewLine);//IGSTPer
                                                                                                                                                 //StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": " + dtBill.Rows[i].ItemArray[70] + System.Environment.NewLine);//CessPer
                    StrBld.Append((char)(34) + "cessRate" + (char)(34) + ": 0" + System.Environment.NewLine);//CessPer }
                }
                StrBld.Append("}");


            }

            StrBld.Append(System.Environment.NewLine);
            StrBld.Append("]" + System.Environment.NewLine);
            StrBld.Append("}" + System.Environment.NewLine);
            StrBld.Append("]" + System.Environment.NewLine);
            StrBld.Append("}" + System.Environment.NewLine);


            FileName = CommonFunctions.DBFileOuputPath + "\\EWayBill_" + BillNo + ".json";
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

            ID = ObjQry.ReturnLong("Select PKEWayNo from TEWayDetails Where fkvoucherno=" + ShortID + " ", CommonFunctions.ConStr);
            if (ID != 0)
            {
                grpBoxTransDetails.Enabled = false;

                grpBoxCustDetails.Enabled = false;
                FillControls(ID);

            }
            else
            {
                // grpBoxTransDetails.Enabled = true;
                txtState.Enabled = false;
                txtMode.Select();
                FillControls(ShortID);
                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }

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
                    txtEwayBill.Text = tewaydetails.EWayNo.ToString();
                    txtMode.Text = lstMode.Text;
                    if (txtEwayBill.Text == "")
                    {
                        btnUpdate.Visible = true;
                        btnSave.Visible = false;
                    }
                    else
                    {
                        txtEwayBill.Enabled = false;

                    }
                    if ((tewaydetails.Distance == 0) || (tewaydetails.Distance == 0)) tewaydetails.Distance = 1.0;
                    txtDistance.Text = tewaydetails.Distance.ToString();
                    txtAddress.Text = tewaydetails.Address;
                    lstTransporter.SelectedValue = tewaydetails.TransportNo.ToString();
                    txtTransporter.Text = lstTransporter.Text;
                    txtTransporterGSTNo.Text = ObjQry.ReturnString("select gstno from mledgerdetails where ledgerno=" + lstTransporter.SelectedValue + "", CommonFunctions.ConStr);

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

            lstTransporter.SelectedValue = ObjQry.ReturnLong("Select TransporterNo from MLedger where LedgerNo="+ Ledgerno, CommonFunctions.ConStr);
            txtTransporter.Text = lstTransporter.Text;

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
            }
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            } 

        } 





        private void txtLRNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtAddress.Enabled == true)
                {
                    txtAddress.Focus();
                }
                else { btnSave.Focus(); }
            }
        }

        private void txtLRDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtLRDate.Text.Length == 0)
                {
                    txtLRDate.CustomFormat = "dd-MMM-yy";
                    if (txtLRDate.Text.Length == 0)
                    {
                        txtVehicleNo.Focus();
                    }
                }
                else
                {
                    txtVehicleNo.Focus();
                }
           

            } // if key enter           
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                if (txtPinCode.Text.Length != 6)
                {
                    txtPinCode.Focus();
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

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtMode.Focus();
            }

        }

        private void dtpBillDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtMode.Focus();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEwayBill.Text == "")
            {
                grpBoxTransDetails.Enabled = true;
                txtMode.Enabled = true;
                txtDistance.Enabled = true;
                txtTransporter.Enabled = true;
                txtTransporterGSTNo.Enabled = true;
                txtLRDate.Enabled = true;
                txtVehicleNo.Enabled = true;
                txtLRNo.Enabled = true;
                txtEwayBill.Enabled = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
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

                if (e.KeyCode == Keys.Enter)
     
                {
                    txtTransporter.Text = lstTransporter.Text;
                    pnlTransporter.Visible = false;
                    if (txtTransporter.Text == "NA")
                    {
                        txtTransporterGSTNo.Enabled = false;
                        txtVehicleNo.Focus();
                    }
                    else { txtLRDate.Focus(); }
                    txtTransporterGSTNo.Text = ObjQry.ReturnString("select gstno from mledgerdetails where ledgerno=" + lstTransporter.SelectedValue + "", CommonFunctions.ConStr);
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

                    if (Convert.ToInt32(txtDistance.Text) <= 0)
                    {
                        txtDistance.Focus();

                    }
                    else
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
                    tewaydetails.EWayNo = txtEwayBill.Text;
                    tewaydetails.ModeNo = Convert.ToInt32(lstMode.SelectedValue);
                    tewaydetails.VoucherUserNo = Convert.ToInt32(txtBillNo.Text.Trim());
                    tewaydetails.Distance = Convert.ToDouble(txtDistance.Text.Trim());

                    tewaydetails.TransportNo = Convert.ToInt32(lstTransporter.SelectedValue);
                    tewaydetails.VehicleNo = (txtVehicleNo.Text.Trim() == null) ? "" : txtVehicleNo.Text.Trim().ToUpper();
                    tewaydetails.LRNo = (txtLRNo.Text.Trim() == null) ? "" : (txtLRNo.Text.Trim().ToUpper());
                    tewaydetails.LRDate = Convert.ToDateTime(txtLRDate.Value);
                    tewaydetails.LedgerNo = Ledgerno;
                    tewaydetails.LedgerName = txtCustomerName.Text.Trim().ToUpper();
                    tewaydetails.Address = (txtAddress.Text.Trim() == null) ? "" : txtAddress.Text.Trim().ToUpper();
                    tewaydetails.CityNo = cityno;
                    tewaydetails.CityName = txtCity.Text.Trim().ToUpper();
                    tewaydetails.PinCode = Convert.ToInt64(txtPinCode.Text.Trim());
                    tewaydetails.StateCode = statecode;
                    tewaydetails.StateName = txtState.Text.Trim().ToUpper();
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
                        else
                        {
                            OMMessageBox.Show("EWay Bill Updated Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            FillControls(ID);
                        }

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
