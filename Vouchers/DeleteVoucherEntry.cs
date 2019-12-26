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

namespace Yadi.Vouchers
{
    public partial class DeleteVoucherEntry : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        DBTVaucherEntry dbTVoucherEntry;
        
        public DeleteVoucherEntry()
        {
            InitializeComponent();
        }

        public void DeleteCashVouchers()
        {
            dbTVoucherEntry = new DBTVaucherEntry();

            DataTable dtVouchers = ObjFunction.GetDataView("Select PKVoucherNo From TVoucherEntry").Table;// Where VoucherTypeCode=" + VchType.Sales + " AND  PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=2)", CommonFunctions.ConStr).Table;
            DataTable dtKacchaCompany = ObjFunction.GetDataView("Select MfgCompNo From MManufacturerCompany Where IsMfgType='false'").Table;
            string strKacchaCompNo = "";
            for (int i = 0; i < dtKacchaCompany.Rows.Count; i++)
            {
                if (i == 0) strKacchaCompNo = dtKacchaCompany.Rows[i].ItemArray[0].ToString();
                else strKacchaCompNo += "," + dtKacchaCompany.Rows[i].ItemArray[0].ToString();
            }


            for (int i = 0; i < dtVouchers.Rows.Count; i++)
            {
                long VchCompNo = ObjQry.ReturnLong("Select PkVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + "", CommonFunctions.ConStr);
                long TotalKaccha = ObjQry.ReturnLong("Select Count(*) From TVoucherEntryCompany Where MfgCompNo in(" + strKacchaCompNo + ") AND FKVoucherNo=" + Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()) + "", CommonFunctions.ConStr);
                DataTable dtVchDetailsComp = ObjFunction.GetDataView("Select FKVoucherTrnNo,LedgerNo,Sum(Debit),Sum(Credit) FRom TVoucherDetailsCompany " +
                    " Where FKVoucherNo in (Select PkVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + ") AND MfgCompNo not in(" + strKacchaCompNo + ")" +
                    " Group by FKVoucherTrnNo,LedgerNo ").Table;
                DataTable dtVchDetailsMain = ObjFunction.GetDataView("Select PKVoucherTrnNo FRom TVoucherDetails Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + " ").Table;
                if (dtVchDetailsComp.Rows.Count > 0 && TotalKaccha > 0)
                {
                    double Amt = 0;
                    for (int j = 0; j < dtVchDetailsComp.Rows.Count; j++)
                    {
                        dbTVoucherEntry.UpdateQuery("Update TVoucherDetails set Debit=" + dtVchDetailsComp.Rows[j].ItemArray[2].ToString() + ", " +
                            " Credit=" + dtVchDetailsComp.Rows[j].ItemArray[3].ToString() + " Where PKVoucherTrnNo=" + dtVchDetailsComp.Rows[j].ItemArray[0].ToString() + "");

                        Amt += Convert.ToDouble(dtVchDetailsComp.Rows[j].ItemArray[2].ToString());

                        dbTVoucherEntry.UpdateQuery("Delete From TVoucherDetailsCompany Where FKVoucherTrnNo=" + dtVchDetailsComp.Rows[j].ItemArray[0].ToString() + " ANd MfgCompNo in ("+ strKacchaCompNo +")");
                    }
                    //dtVchDetailsComp = ObjFunction.GetDataView("Select FKVoucherTrnNo FRom TVoucherDetailsCompany " +
                    //    " Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + " AND MfgCompNo in(" + strKacchaCompNo + ")").Table;

                    for (int j = 0; j < dtVchDetailsMain.Rows.Count; j++)
                    {
                        bool delFlag=false;
                        for (int col = 0; col < dtVchDetailsComp.Rows.Count; col++)
                        {
                            if (dtVchDetailsMain.Rows[j].ItemArray[0].ToString() == dtVchDetailsComp.Rows[col].ItemArray[0].ToString())
                            {
                                delFlag = true;
                                break;
                            }
                        }
                        if (delFlag == false)
                        {
                            TVoucherDetails tvchD = new TVoucherDetails();
                            tvchD.PkVoucherTrnNo = Convert.ToInt64(dtVchDetailsMain.Rows[j].ItemArray[0].ToString());
                            dbTVoucherEntry.DeleteTVoucherDetails(tvchD);
                        }
                    }
                    
                    dbTVoucherEntry.UpdateQuery("Delete From TStockGodown Where FKStockTrnNo in (Select PkStockTrnNo From TStock Where FKVoucherNo =" + dtVouchers.Rows[i].ItemArray[0].ToString() +
                        "ANd TStock.ItemNo in (Select mItemMaster.ItemNo From MStockItems Where mItemMaster.ItemNo=TStock.ItemNo AND mItemMaster.MfgCompNo in(" + strKacchaCompNo + ")) )");
                    dbTVoucherEntry.UpdateQuery("Delete From TStock Where FKVoucherNo =" + dtVouchers.Rows[i].ItemArray[0].ToString() + " AND TStock.ItemNo in (Select mItemMaster.ItemNo From MStockItems Where mItemMaster.ItemNo=TStock.ItemNo AND mItemMaster.MfgCompNo in(" + strKacchaCompNo + "))");
                    if (Amt == 0)
                        dbTVoucherEntry.DeleteAllVoucherEntry(Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
                    else
                        dbTVoucherEntry.UpdateVoucherEntry(Amt, Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
                }
                else if (TotalKaccha > 0)
                    dbTVoucherEntry.DeleteAllVoucherEntry(Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
            }
            long tempid = 0;
            if (dbTVoucherEntry.commandcollection.Count > 0)
                tempid = dbTVoucherEntry.ExecuteNonQueryStatements();
            //ObjTrans.Execute("Exec SetMisMatchBills " + VchType.Sales + "," + VchType.Receipt, CommonFunctions.ConStr);
        }

        public void DeleteCreditVouchers()
        {
            dbTVoucherEntry = new DBTVaucherEntry();

            string Sql = " Select PkVoucherNo From (SELECT TVoucherEntry.PkVoucherNo,TVoucherRefDetails.RefNo as RfNo, TVoucherRefDetails.Amount FROM  TVoucherEntry INNER JOIN " +
                       " TVoucherDetails ON TVoucherDetails.FkVoucherNo = TVoucherEntry.PkVoucherNo INNER JOIN TVoucherRefDetails ON TVoucherDetails.PkVoucherTrnNo = TVoucherRefDetails.FkVoucherTrnNo  " +
                       " WHERE   (TVoucherEntry.VoucherTypeCode = " + VchType.Sales + ") And (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherRefDetails.TypeOfRef = 3) AND  (TVoucherEntry.CompanyNo = 1) and (TVoucherEntry.IsCancel='false')) as tbl1  " +
                       " Where   Amount=((Select IsNull(sum(TVoucherRefDetails.Amount),0) from TVoucherRefDetails where RefNo=RfNo and TypeOfRef in (2,5))) ";


            DataTable dtVouchers = ObjFunction.GetDataView(Sql).Table;// Where VoucherTypeCode=" + VchType.Sales + " AND  PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=2)", CommonFunctions.ConStr).Table;
            DataTable dtKacchaCompany = ObjFunction.GetDataView("Select MfgCompNo From MManufacturerCompany Where IsMfgType='false'").Table;
            string strKacchaCompNo = "";
            for (int i = 0; i < dtKacchaCompany.Rows.Count; i++)
            {
                if (i == 0) strKacchaCompNo = dtKacchaCompany.Rows[i].ItemArray[0].ToString();
                else strKacchaCompNo += "," + dtKacchaCompany.Rows[i].ItemArray[0].ToString();
            }


            for (int i = 0; i < dtVouchers.Rows.Count; i++)
            {
                long VchCompNo = ObjQry.ReturnLong("Select PkVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + "", CommonFunctions.ConStr);
                long TotalKaccha = ObjQry.ReturnLong("Select Count(*) From TVoucherEntryCompany Where MfgCompNo in(" + strKacchaCompNo + ") AND FKVoucherNo=" + Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()) + "", CommonFunctions.ConStr);
                DataTable dtVchDetailsComp = ObjFunction.GetDataView("Select FKVoucherTrnNo,LedgerNo,Sum(Debit),Sum(Credit) FRom TVoucherDetailsCompany " +
                    " Where FKVoucherNo in (Select PkVoucherCompanyNo From TVoucherEntryCompany Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + ") AND MfgCompNo not in(" + strKacchaCompNo + ")" +
                    " Group by FKVoucherTrnNo,LedgerNo ").Table;
                DataTable dtVchDetailsMain = ObjFunction.GetDataView("Select PKVoucherTrnNo FRom TVoucherDetails Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + " ").Table;
                if (dtVchDetailsComp.Rows.Count > 0 && TotalKaccha > 0)
                {
                    double Amt = 0;
                    for (int j = 0; j < dtVchDetailsComp.Rows.Count; j++)
                    {
                        dbTVoucherEntry.UpdateQuery("Update TVoucherDetails set Debit=" + dtVchDetailsComp.Rows[j].ItemArray[2].ToString() + ", " +
                            " Credit=" + dtVchDetailsComp.Rows[j].ItemArray[3].ToString() + " Where PKVoucherTrnNo=" + dtVchDetailsComp.Rows[j].ItemArray[0].ToString() + "");

                        Amt += Convert.ToDouble(dtVchDetailsComp.Rows[j].ItemArray[2].ToString());

                        dbTVoucherEntry.UpdateQuery("Delete From TVoucherDetailsCompany Where FKVoucherTrnNo=" + dtVchDetailsComp.Rows[j].ItemArray[0].ToString() + " ANd MfgCompNo in (" + strKacchaCompNo + ")");
                    }
                    //dtVchDetailsComp = ObjFunction.GetDataView("Select FKVoucherTrnNo FRom TVoucherDetailsCompany " +
                    //    " Where FKVoucherNo=" + dtVouchers.Rows[i].ItemArray[0].ToString() + " AND MfgCompNo in(" + strKacchaCompNo + ")").Table;

                    for (int j = 0; j < dtVchDetailsMain.Rows.Count; j++)
                    {
                        bool delFlag = false;
                        for (int col = 0; col < dtVchDetailsComp.Rows.Count; col++)
                        {
                            if (dtVchDetailsMain.Rows[j].ItemArray[0].ToString() == dtVchDetailsComp.Rows[col].ItemArray[0].ToString())
                            {
                                delFlag = true;
                                break;
                            }
                        }
                        if (delFlag == false)
                        {
                            TVoucherDetails tvchD = new TVoucherDetails();
                            tvchD.PkVoucherTrnNo = Convert.ToInt64(dtVchDetailsMain.Rows[j].ItemArray[0].ToString());
                            dbTVoucherEntry.DeleteTVoucherDetails(tvchD);
                        }
                    }

                    dbTVoucherEntry.UpdateQuery("Delete From TStockGodown Where FKStockTrnNo in (Select PkStockTrnNo From TStock Where FKVoucherNo =" + dtVouchers.Rows[i].ItemArray[0].ToString() +
                        "ANd TStock.ItemNo in (Select mItemMaster.ItemNo From MStockItems Where mItemMaster.ItemNo=TStock.ItemNo AND mItemMaster.MfgCompNo in(" + strKacchaCompNo + ")) )");
                    dbTVoucherEntry.UpdateQuery("Delete From TStock Where FKVoucherNo =" + dtVouchers.Rows[i].ItemArray[0].ToString() + " AND TStock.ItemNo in (Select mItemMaster.ItemNo From MStockItems Where mItemMaster.ItemNo=TStock.ItemNo AND mItemMaster.MfgCompNo in(" + strKacchaCompNo + "))");
                    if (Amt == 0)
                        dbTVoucherEntry.DeleteAllVoucherEntry(Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
                    else
                        dbTVoucherEntry.UpdateVoucherEntry(Amt, Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
                }
                else if (TotalKaccha > 0)
                    dbTVoucherEntry.DeleteAllVoucherEntry(Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
            }
            long tempid = 0;
            if (dbTVoucherEntry.commandcollection.Count > 0)
                tempid = dbTVoucherEntry.ExecuteNonQueryStatements();
            //ObjTrans.Execute("Exec SetMisMatchBills " + VchType.Sales + "," + VchType.Receipt, CommonFunctions.ConStr);
        }

        public void DeleteAllCashBill()
        {
            dbTVoucherEntry = new DBTVaucherEntry();
            DataTable dtVouchers = ObjFunction.GetDataView("Select PKVoucherNo From TVoucherEntry Where PayTypeNo=2").Table;
            for (int i = 0; i < dtVouchers.Rows.Count; i++)
            {
                dbTVoucherEntry.DeleteAllVoucherEntry(Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
            }
            dbTVoucherEntry.ExecuteNonQueryStatements();
        }

        public void DeleteAllCreditBill()
        {
            dbTVoucherEntry = new DBTVaucherEntry();
            DataTable dtVouchers = ObjFunction.GetDataView("Select PKVoucherNo From TVoucherEntry Where PayTypeNo=2").Table;
            for (int i = 0; i < dtVouchers.Rows.Count; i++)
            {
                dbTVoucherEntry.DeleteAllVoucherEntry(Convert.ToInt64(dtVouchers.Rows[i].ItemArray[0].ToString()));
            }
            dbTVoucherEntry.ExecuteNonQueryStatements();
        }

        public bool DeleteReceipt(long SalesID, TVoucherEntry tVch)
        {
            DataTable dtVchPrev = new DataTable();
            DataTable dtPayType = new DataTable();
            double PrevAmt = 0;
            long tempid = -1, ReceiptID = 0, VoucherUserNo = 0, LedgerNo;
            bool CancelFlag = true;
            

            LedgerNo = ObjQry.ReturnLong("Select LedgerNo From TVoucherDetails Where FKVoucherNo=" + SalesID + " AND VoucherSrNo=1 AND SrNo=" + Others.Party + "", CommonFunctions.ConStr);


            ReceiptID = ObjQry.ReturnLong("SELECT TVoucherDetails.FkVoucherNo FROM TVoucherEntry INNER JOIN TVoucherDetails ON TVoucherEntry.PkVoucherNo = TVoucherDetails.FkVoucherNo " +
                  " WHERE (TVoucherEntry.VoucherTypeCode = " + VchType.Receipt + ") AND (TVoucherEntry.VoucherDate ='" + tVch.VoucherDate.Date + "') AND (TVoucherDetails.LedgerNo = " + LedgerNo + ") AND " +
                    " (TVoucherDetails.VoucherSrNo = 1) AND (TVoucherEntry.PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=2)) ", CommonFunctions.ConStr);


            DataTable dtReceipt = ObjFunction.GetDataView("SELECT PkVoucherTrnNo,LedgerNo,Debit,Credit FROM TVoucherDetails " +
                " WHERE (FkVoucherNo = " + ReceiptID + ") order by VoucherSrNo ").Table;
            VoucherUserNo = ObjQry.ReturnLong("Select IsNull((VoucherUserNo),0) From TVoucherEntry Where PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr);
            long VoucherSrNo = ObjQry.ReturnLong("select IsNull(max(VoucherSrNo),0)+1 from TVoucherDetails where (FkVoucherNo = " + ReceiptID + ")", CommonFunctions.ConStr);

            for (int i = 0; i < dtVchPrev.Rows.Count; i++)
                PrevAmt += Convert.ToDouble(dtVchPrev.Rows[i].ItemArray[1].ToString());
            PrevAmt = (ObjQry.ReturnDouble("Select BilledAmount From TVoucherEntry Where  PkVoucherNo=" + ReceiptID + "", CommonFunctions.ConStr) - PrevAmt) + ((CancelFlag == true) ? -tVch.BilledAmount : tVch.BilledAmount);

            dbTVoucherEntry = new DBTVaucherEntry();

            DataTable dtVoucherDetails = new DataTable();
            if (ReceiptID != 0)
            {
                dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit From TVoucherDetails Where FkVoucherNo=" + ReceiptID + " order by VoucherSrNo").Table;
                dtPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo From TVoucherPayTypeDetails Where FKSalesVoucherNo=" + SalesID + " AND FKPayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=2)  order by PKVoucherPayTypeNo").Table;
            }

            dtVoucherDetails = ObjFunction.GetDataView("Select PkVoucherTrnNo,LedgerNo,Debit,Credit,VoucherSrNo From TVoucherDetails TD,TVoucherEntry TC Where TC.PKVoucherNo=TD.FKVoucherNo AND TC.PayTypeNo in (Select PKPayTypeNo From MPayType Where ControlUnder=2) AND TD.LedgerNo=" + LedgerNo + " AND  TD.FkVoucherNo=" + ReceiptID + " AND TD.CompanyNo=" + DBGetVal.FirmNo + " order by TD.VoucherSrNo").Table;

            double totamt = 0;
            DataTable dtDelPayType = ObjFunction.GetDataView("Select PKVoucherPayTypeNo,TVoucherPayTypeDetails.CompanyNo,FKReceiptVoucherNo,Amount From TVoucherPayTypeDetails,TVoucherDetails Where TVoucherDetails.PkVoucherTrnNo=TVoucherPayTypeDetails.FKVoucherTrnNo AND FKSalesVoucherNo=" + SalesID + "  order by PKVoucherPayTypeNo").Table;
            for (int k = 0; k < dtDelPayType.Rows.Count; k++)                                                                                                                                                                                                                                                                                                                                     
            {
                DataTable dtUpdateVoucher = ObjFunction.GetDataView("Select PKVoucherTrnNo,Debit,Credit From TVoucherDetails Where FKVoucherNo=" + dtDelPayType.Rows[k].ItemArray[2].ToString() + " AND CompanyNo=" + dtDelPayType.Rows[k].ItemArray[1].ToString() + "").Table;
                totamt = 0;
                bool alllowdel = false;
                
                bool allowVoucher = false;
                for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                {
                    double DrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[1].ToString());
                    double CrAmt = Convert.ToDouble(dtUpdateVoucher.Rows[m].ItemArray[2].ToString());
                    if (DrAmt > 0) DrAmt = DrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                    if (CrAmt > 0) CrAmt = CrAmt - Convert.ToDouble(dtDelPayType.Rows[k].ItemArray[3].ToString());
                    if (allowVoucher == false)
                    {
                        dbTVoucherEntry.UpdateVoucherEntry((DrAmt > CrAmt) ? DrAmt : CrAmt, ReceiptID);
                        allowVoucher = true;
                    }
                    dbTVoucherEntry.UpdateVoucherDetails(DrAmt, CrAmt, Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString()));
                    totamt = totamt + DrAmt + CrAmt;
                    alllowdel = true;
                }
                if (totamt == 0 && alllowdel == true)
                {
                    for (int m = 0; m < dtUpdateVoucher.Rows.Count; m++)
                    {
                        TVoucherDetails tVoucherDetails = new TVoucherDetails();
                        tVoucherDetails.PkVoucherTrnNo = Convert.ToInt64(dtUpdateVoucher.Rows[m].ItemArray[0].ToString());
                        dbTVoucherEntry.DeleteTVoucherDetails(tVoucherDetails);

                        if (m == dtUpdateVoucher.Rows.Count - 1)
                        {
                            if (ObjQry.ReturnLong("Select Count(*) From TVoucherDetails Where FKVoucherNo=" + Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[2].ToString()) + "", CommonFunctions.ConStr) >= dtUpdateVoucher.Rows.Count)
                            {
                                TVoucherEntry tVoucherEntry = new TVoucherEntry();
                                tVoucherEntry.PkVoucherNo = Convert.ToInt64(dtDelPayType.Rows[k].ItemArray[2].ToString());
                                dbTVoucherEntry.DeleteTVoucherEntry1(tVoucherEntry);
                            }
                        }
                    }
                }
            }



            tempid = dbTVoucherEntry.ExecuteNonQueryStatements();

            CancelFlag = false;

            if (tempid != 0)
                return true;
            else
                return false;
        }

        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtKey.Text = "";
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (txtKey.Text.Trim() != "")
                {
                    if (txtKey.Text.Trim().ToUpper() == "C12")
                    {
                        if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DeleteCashVouchers();
                            this.Close();
                        }
                    }
                    else if (txtKey.Text.Trim().ToUpper() == "C123")
                    {
                        if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DeleteAllCashBill();
                            this.Close();
                        }
                    }
                    else if (txtKey.Text.Trim().ToUpper() == "CR12")
                    {
                        if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DeleteCreditVouchers();
                            this.Close();
                        }
                    }
                    else if (txtKey.Text.Trim().ToUpper() == "CR123")
                    {
                        if (OMMessageBox.Show("Are you sure want to delete this record ?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            DeleteAllCreditBill();
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
 
                    }

                }
            }
        }
    }
}
