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
    public partial class TaxPercentage : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DBMLedger dbLedger = new DBMLedger();
        MLedger mLedger = new MLedger();
        MItemTaxSetting mItemTaxSetting = new MItemTaxSetting();
        public DialogResult DS = DialogResult.OK;
        long GrType = 0, TaxTypeNo;
        string Str = "", strName = "";
        string StateCode;

        public TaxPercentage()
        {
            InitializeComponent();
        }

        //public TaxPercentage(long typeNo)
        //{
        //    InitializeComponent();
        //    GrType = typeNo;
        //}

        public TaxPercentage(long typeNo, long TaxTypeNo)
        {
            InitializeComponent();
            GrType = typeNo;
            this.TaxTypeNo = TaxTypeNo;
        }

        private void txtPercentage_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtPercentage.Text.Trim() != "")
                {
                    if (ObjQry.ReturnInteger(" Select Count(*) FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                             " MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo WHERE     (MLedger.GroupNo = " + GrType + ") AND (MLedger_1.GroupNo =" + TaxTypeNo + ") AND MItemTaxSetting.Percentage= " + Convert.ToDouble(txtPercentage.Text.Trim()) + "", CommonFunctions.ConStr) != 0)
                    {
                        EP.SetError(txtPercentage, "Duplicate Percentage");
                        EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                        txtPercentage.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void TaxPercentage_Load(object sender, EventArgs e)
        {
            txtPercentage.Focus();
            if (GrType == GroupType.SalesAccount)
            {
                if (TaxTypeNo == GroupType.VAT)
                {
                    Str = " Sales";
                    strName = "VAT";
                }
                else if (TaxTypeNo == GroupType.CST)
                {
                    Str = "CST Sales";
                    strName = "CST";
                }
                else if (TaxTypeNo == GroupType.CForm)
                {
                    Str = "CForm Sales";
                    strName = "CForm";
                }
                else if (TaxTypeNo == GroupType.GST)
                {
                    Str = "GST Sales";
                    strName = "GST";
                }
                else if (TaxTypeNo == GroupType.SGST)
                {
                    Str = "SGST Sales";
                    strName = "SGST";
                }
                else if (TaxTypeNo == GroupType.CGST)
                {
                    Str = "CGST Sales";
                    strName = "CGST";
                }
                else if (TaxTypeNo == GroupType.IGST)
                {
                    Str = "IGST Sales";
                    strName = "IGST";
                }
                else if (TaxTypeNo == GroupType.Cess)
                {
                    Str = "Cess Sales";
                    strName = "Cess";
                }
            }
            else if (GrType == GroupType.PurchaseAccount)
            {
                if (TaxTypeNo == GroupType.VAT)
                {
                    Str = "Purchase";
                    strName = "VAT";
                }
                else if (TaxTypeNo == GroupType.CST)
                {
                    Str = "CST Purchase";
                    strName = "CST";
                }
                else if (TaxTypeNo == GroupType.CForm)
                {
                    Str = "CForm Purchase";
                    strName = "CForm";
                }
                else if (TaxTypeNo == GroupType.GST)
                {
                    Str = "GST Purchase";
                    strName = "GST";
                }
                else if (TaxTypeNo == GroupType.SGST)
                {
                    Str = "SGST Purchase";
                    strName = "SGST";
                }
                else if (TaxTypeNo == GroupType.CGST)
                {
                    Str = "CGST Purchase";
                    strName = "CGST";
                }
                else if (TaxTypeNo == GroupType.IGST)
                {
                    Str = "IGST Purchase";
                    strName = "IGST";
                }
                else if (TaxTypeNo == GroupType.Cess)
                {
                    Str = "Cess Purchase";
                    strName = "Cess";
                }
            }

        }

        public bool Validations()
        {
            bool flag = false;
            EP.SetError(txtPercentage, "");
            if (txtPercentage.Text.Trim() == "")
            {
                EP.SetError(txtPercentage, "Enter Percentage");
                EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                txtPercentage.Focus();
            }
            else if (ObjQry.ReturnInteger(" Select Count(*) FROM MItemTaxSetting INNER JOIN MLedger ON MItemTaxSetting.SalesLedgerNo = MLedger.LedgerNo INNER JOIN " +
                                     " MLedger AS MLedger_1 ON MItemTaxSetting.TaxLedgerNo = MLedger_1.LedgerNo WHERE     (MLedger.GroupNo = " + GrType + ") AND (MLedger_1.GroupNo = " + TaxTypeNo + ") AND  MItemTaxSetting.Percentage= " + Convert.ToDouble(txtPercentage.Text.Trim()) + "", CommonFunctions.ConStr) != 0)
            {
                EP.SetError(txtPercentage, "Duplicate Percentage");
                EP.SetIconAlignment(txtPercentage, ErrorIconAlignment.MiddleRight);
                txtPercentage.Focus();
            }
            else
                flag = true;

            return flag;
        }

        private void btnPecOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validations() == true)
                {
                    if (TaxTypeNo != 54)
                    {
                        //---GST---//
                        StateCode = ObjQry.ReturnLong("Select StateCode From MCompany", CommonFunctions.ConStr).ToString();

                        dbLedger = new DBMLedger();
                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName = strName + " @ " + txtPercentage.Text.Trim() + " % Sales";
                        mLedger.GroupNo = GroupType.IGST;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);


                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName = Str + " @ " + txtPercentage.Text.Trim() + " %";
                        mLedger.GroupNo = GrType;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);

                        mItemTaxSetting = new MItemTaxSetting();
                        mItemTaxSetting.PkSrNo = 0;
                        mItemTaxSetting.TaxSettingName = txtPercentage.Text.Trim() + " % " + Str;
                        mItemTaxSetting.Percentage = Convert.ToDouble(txtPercentage.Text.Trim());
                        mItemTaxSetting.IsActive = true;
                        mItemTaxSetting.CalculationMethod = "2";
                        mItemTaxSetting.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxSetting.UserID = DBGetVal.UserID;
                        mItemTaxSetting.UserDate = DBGetVal.ServerTime.Date;

                        dbLedger.AddMItemTaxSetting(mItemTaxSetting);
                        dbLedger.ExecuteNonQueryStatements();
                        SGST(0);//0 for sales and 1 for Purchase
                        dbLedger.ExecuteNonQueryStatements();
                        CGST(0);//0 for sales and 1 for Purchase
                        dbLedger.ExecuteNonQueryStatements();

                            //=====================Purchase tax 

                            dbLedger = new DBMLedger();
                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName = strName + " @ " + txtPercentage.Text.Trim() + " % Purchase";
                        mLedger.GroupNo = GroupType.IGST;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);


                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName = "IGST Purchase @ " + txtPercentage.Text.Trim() + " %";
                        mLedger.GroupNo = 11;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);


                        mItemTaxSetting = new MItemTaxSetting();
                        mItemTaxSetting.PkSrNo = 0;
                        mItemTaxSetting.TaxSettingName = txtPercentage.Text.Trim() + " % IGST Purchase" ;
                        mItemTaxSetting.Percentage = Convert.ToDouble(txtPercentage.Text.Trim());
                        mItemTaxSetting.IsActive = true;
                        mItemTaxSetting.CalculationMethod = "2";
                        mItemTaxSetting.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxSetting.UserID = DBGetVal.UserID;
                        mItemTaxSetting.UserDate = DBGetVal.ServerTime.Date;

                        dbLedger.AddMItemTaxSetting(mItemTaxSetting);
                        dbLedger.ExecuteNonQueryStatements();
                        SGST(1);//0 for sales and 1 for Purchase
                        dbLedger.ExecuteNonQueryStatements();
                        CGST(1);//0 for sales and 1 for Purchase
                        if (dbLedger.ExecuteNonQueryStatements() == true)
                        {
                            OMMessageBox.Show("Data Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            DS = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            OMMessageBox.Show("Data not Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        StateCode = ObjQry.ReturnLong("Select StateCode From MCompany", CommonFunctions.ConStr).ToString();

                        dbLedger = new DBMLedger();
                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName ="Cess @ " + txtPercentage.Text.Trim() + " % Sales";
                        mLedger.GroupNo = GroupType.Cess;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);


                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName = "Cess Sales @ " + txtPercentage.Text.Trim() + " %";
                        mLedger.GroupNo = 10;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);

                        mItemTaxSetting = new MItemTaxSetting();
                        mItemTaxSetting.PkSrNo = 0;
                        mItemTaxSetting.TaxSettingName = txtPercentage.Text.Trim() + " % Cess Sales ";
                        mItemTaxSetting.Percentage = Convert.ToDouble(txtPercentage.Text.Trim());
                        mItemTaxSetting.IsActive = true;
                        mItemTaxSetting.CalculationMethod = "2";
                        mItemTaxSetting.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxSetting.UserID = DBGetVal.UserID;
                        mItemTaxSetting.UserDate = DBGetVal.ServerTime.Date;

                        dbLedger.AddMItemTaxSetting(mItemTaxSetting);
                        dbLedger.ExecuteNonQueryStatements();
                        //Purchase cess
                        dbLedger = new DBMLedger();
                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName = "Cess @ " + txtPercentage.Text.Trim() + " % Purchase";
                        mLedger.GroupNo = GroupType.Cess;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);


                        mLedger = new MLedger();
                        mLedger.LedgerNo = 0;
                        mLedger.LedgerUserNo = "0";
                        mLedger.LedgerName = "Cess Purchase @ " + txtPercentage.Text.Trim() + " %";
                        mLedger.GroupNo = 11;
                        mLedger.ContactPerson = "";
                        mLedger.InvFlag = false;
                        mLedger.MaintainBillByBill = false;
                        mLedger.IsActive = true;
                        mLedger.CompanyNo = DBGetVal.FirmNo;
                        mLedger.LedgerStatus = 1;
                        mLedger.LedgerLangName = "";
                        mLedger.UserID = DBGetVal.UserID;
                        mLedger.UserDate = DBGetVal.ServerTime.Date;
                        mLedger.StateCode = Convert.ToInt32(StateCode);
                        dbLedger.AddMLedger(mLedger);

                        mItemTaxSetting = new MItemTaxSetting();
                        mItemTaxSetting.PkSrNo = 0;
                        mItemTaxSetting.TaxSettingName = txtPercentage.Text.Trim() + " % Cess Purchase ";
                        mItemTaxSetting.Percentage = Convert.ToDouble(txtPercentage.Text.Trim());
                        mItemTaxSetting.IsActive = true;
                        mItemTaxSetting.CalculationMethod = "2";
                        mItemTaxSetting.CompanyNo = DBGetVal.FirmNo;
                        mItemTaxSetting.UserID = DBGetVal.UserID;
                        mItemTaxSetting.UserDate = DBGetVal.ServerTime.Date;

                        dbLedger.AddMItemTaxSetting(mItemTaxSetting);
                        dbLedger.ExecuteNonQueryStatements();
                        if (dbLedger.ExecuteNonQueryStatements() == true)
                        {
                            OMMessageBox.Show("Data Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                            DS = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            OMMessageBox.Show("Data not Saved Successfully", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void SGST(int type)
        {
            double per = Convert.ToInt32(txtPercentage.Text.Trim());
            per = per / 2;
            dbLedger = new DBMLedger();
            mLedger = new MLedger();
            mLedger.LedgerNo = 0;
            mLedger.LedgerUserNo = "0";
            if (type == 0)
            {
                mLedger.LedgerName = "SGST @ " + per + " % Sales";
            }
            else
            {
                mLedger.LedgerName = "SGST @ " + per + " % Purchase";
            }
            mLedger.GroupNo = GroupType.SGST;
            mLedger.ContactPerson = "";
            mLedger.InvFlag = false;
            mLedger.MaintainBillByBill = false;
            mLedger.IsActive = true;
            mLedger.CompanyNo = DBGetVal.FirmNo;
            mLedger.LedgerStatus = 1;
            mLedger.LedgerLangName = "";
            mLedger.UserID = DBGetVal.UserID;
            mLedger.UserDate = DBGetVal.ServerTime.Date;
            mLedger.StateCode = Convert.ToInt32(StateCode);
            dbLedger.AddMLedger(mLedger);


            mLedger = new MLedger();
            mLedger.LedgerNo = 0;
            mLedger.LedgerUserNo = "0";
            // mLedger.LedgerName ="SGST Sales @ " + per + " %";
            // mLedger.GroupNo = GrType;
            if (type == 0)
            {
                mLedger.LedgerName = "SGST Sales @ " + per + " % Sales";
                mLedger.GroupNo = 10;
            }
            else
            {
                mLedger.LedgerName = "SGST Sales @ " + per + " % Purchase";
                mLedger.GroupNo = 11;
            }
            mLedger.ContactPerson = "";
            mLedger.InvFlag = false;
            mLedger.MaintainBillByBill = false;
            mLedger.IsActive = true;
            mLedger.CompanyNo = DBGetVal.FirmNo;
            mLedger.LedgerStatus = 1;
            mLedger.LedgerLangName = "";
            mLedger.UserID = DBGetVal.UserID;
            mLedger.UserDate = DBGetVal.ServerTime.Date;
            mLedger.StateCode = Convert.ToInt32(StateCode);
            dbLedger.AddMLedger(mLedger);

            mItemTaxSetting = new MItemTaxSetting();
            mItemTaxSetting.PkSrNo = 0;
            if (type == 0)
            {
                mItemTaxSetting.TaxSettingName = per + " % SGST Sales ";
            }
            else
            {
                mItemTaxSetting.TaxSettingName = per + " % SGST Purchase ";
            }
            mItemTaxSetting.Percentage = Convert.ToDouble(per);
            mItemTaxSetting.IsActive = true;
            mItemTaxSetting.CalculationMethod = "2";
            mItemTaxSetting.CompanyNo = DBGetVal.FirmNo;
            mItemTaxSetting.UserID = DBGetVal.UserID;
            mItemTaxSetting.UserDate = DBGetVal.ServerTime.Date;

            dbLedger.AddMItemTaxSetting(mItemTaxSetting);
        }
        private void CGST(int type)
        {
            double per = Convert.ToInt32(txtPercentage.Text.Trim());
            per = per / 2;
            dbLedger = new DBMLedger();
            mLedger = new MLedger();
            mLedger.LedgerNo = 0;
            mLedger.LedgerUserNo = "0";
            if (type == 0)
            {
                mLedger.LedgerName = "CGST @ " + per + " % Sales";
            }
            else
            {
                mLedger.LedgerName = "CGST @ " + per + " % Purchase";
            }
            mLedger.GroupNo = GroupType.CGST;
            mLedger.ContactPerson = "";
            mLedger.InvFlag = false;
            mLedger.MaintainBillByBill = false;
            mLedger.IsActive = true;
            mLedger.CompanyNo = DBGetVal.FirmNo;
            mLedger.LedgerStatus = 1;
            mLedger.LedgerLangName = "";
            mLedger.UserID = DBGetVal.UserID;
            mLedger.UserDate = DBGetVal.ServerTime.Date;
            mLedger.StateCode = Convert.ToInt32(StateCode);
            dbLedger.AddMLedger(mLedger);


            mLedger = new MLedger();
            mLedger.LedgerNo = 0;
            mLedger.LedgerUserNo = "0";
            if (type == 0)
            {
                mLedger.LedgerName = "CGST Sales @ " + per + " % Sales";
                mLedger.GroupNo = 10;
            }
            else
            {
                mLedger.LedgerName = "CGST Sales @ " + per + " % Purchase";
                mLedger.GroupNo = 11;
            }
            // mLedger.LedgerName = "CGST Sales @ " + per + " %";
            //mLedger.GroupNo = GrType;
            mLedger.ContactPerson = "";
            mLedger.InvFlag = false;
            mLedger.MaintainBillByBill = false;
            mLedger.IsActive = true;
            mLedger.CompanyNo = DBGetVal.FirmNo;
            mLedger.LedgerStatus = 1;
            mLedger.LedgerLangName = "";
            mLedger.UserID = DBGetVal.UserID;
            mLedger.UserDate = DBGetVal.ServerTime.Date;
            mLedger.StateCode = Convert.ToInt32(StateCode);
            dbLedger.AddMLedger(mLedger);

            mItemTaxSetting = new MItemTaxSetting();
            mItemTaxSetting.PkSrNo = 0; if (type == 0)
            {
                mItemTaxSetting.TaxSettingName = per + " % CGST Sales ";
            }
            else
            {
                mItemTaxSetting.TaxSettingName = per + " % CGST Purchase ";
            }
            mItemTaxSetting.Percentage = Convert.ToDouble(per);
            mItemTaxSetting.IsActive = true;
            mItemTaxSetting.CalculationMethod = "2";
            mItemTaxSetting.CompanyNo = DBGetVal.FirmNo;
            mItemTaxSetting.UserID = DBGetVal.UserID;
            mItemTaxSetting.UserDate = DBGetVal.ServerTime.Date;

            dbLedger.AddMItemTaxSetting(mItemTaxSetting);
        }
        private void BtnPerCancel_Click(object sender, EventArgs e)
        {
            DS = DialogResult.Cancel;
            this.Close();
        }

        private void txtPercentage_TextChanged(object sender, EventArgs e)
        {
            ObjFunction.SetMasked((TextBox)sender, 2, 3);
        }
    }
}
