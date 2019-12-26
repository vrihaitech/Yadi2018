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
    public partial class SchemePromoInfo : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();

        public DataTable dtDeleteReward = new DataTable();
        
        DataGridView dgBill = null;
        public DialogResult DS = DialogResult.OK;
        double GTotal = 0;
        bool IsEnroll = false;//ChkFlag = false
        public long InsSchemeNo = 0, InsA = 0, InsN = 0, SchemeType = 0, LedgerNo = 0, MTDSchDetailsNo = 0, ID, WithoutPromo = 0;
        public double Dper = 0, DAmt = 0, schemeAmt = 0;
        DateTime BillDate;
        public Form FrmTemp;

        DataTable dtInst = new DataTable();
        DataTable dtTsku = new DataTable();
        DataTable dtTskuIw = new DataTable();
        DataTable dtTskuOther = new DataTable();
        DataTable dtTskuOtherIw = new DataTable();

        public DataTable dtTRewardDtls;

        public DataTable dtTRewardToFrom;

        public SchemePromoInfo()
        {
            InitializeComponent();
        }

        public void FillControl()
        {
            for (int i = 0; i < dtTRewardDtls.Rows.Count; i++)
            {
                for (int j = 0; j < dgInsTSKU.Rows.Count; j++)
                {
                    if (dgInsTSKU.Rows[j].Cells[ColIndexGrid.rtype].Value.ToString() != "3")
                    {
                        if ("" != dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchemeName].FormattedValue.ToString())
                        {
                            if (Convert.ToInt64(dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString()) == Convert.ToInt64(dtTRewardDtls.Rows[i].ItemArray[ColTRewardDtls.SchemeDetailsNo].ToString()))
                            {
                                //dgInsTSKU.Rows[j].Cells[ColIndexGrid.Select].Style.BackColor = ChkValue.True;
                                //dgInsTSKU.Rows[j].Cells[ColIndexGrid.Select].Value = true;
                                dgInsTSKU_CellClick(dgInsTSKU, new DataGridViewCellEventArgs(ColIndexGrid.Select, j));
                            }
                        }
                    }
                }

                //for (int j = 0; j < dgInsTSKU.Rows.Count; j++)
                //{
                //    if (dgInsTSKU.Rows[j].Cells[ColIndexGrid.rtype].Value.ToString() != "3" && Convert.ToDouble(dgInsTSKU.Rows[j].Cells[ColIndexGrid.DiscAmt].Value) == 0 &&
                //        dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchemeTypeNo].Value.ToString() == "3")
                //    {
                //        if ("" != dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchemeName].FormattedValue.ToString())
                //        {
                //            if (Convert.ToInt64(dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString()) == Convert.ToInt64(dtTRewardDtls.Rows[i].ItemArray[ColTRewardDtls.SchemeDetailsNo].ToString()))
                //            {
                //                //dgInsTSKU.Rows[j].Cells[ColIndexGrid.Select].Style.BackColor = ChkValue.True;
                //                //dgInsTSKU.Rows[j].Cells[ColIndexGrid.Select].Value = true;
                //                dgInsTSKU_CellClick(dgInsTSKU, new DataGridViewCellEventArgs(ColIndexGrid.Select, j));
                //            }
                //        }
                //    }
                //}

                //for (int j = 0; j < dgInsTSKU.Rows.Count; j++)
                //{
                //    if (dgInsTSKU.Rows[j].Cells[ColIndexGrid.rtype].Value.ToString() != "3" && Convert.ToDouble(dgInsTSKU.Rows[j].Cells[ColIndexGrid.DiscAmt].Value) > 0 &&
                //        dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchemeTypeNo].Value.ToString() == "3")
                //    {
                //        if ("" != dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchemeName].FormattedValue.ToString())
                //        {
                //            if (Convert.ToInt64(dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString()) == Convert.ToInt64(dtTRewardDtls.Rows[i].ItemArray[ColTRewardDtls.SchemeDetailsNo].ToString()))
                //            {
                //                //dgInsTSKU.Rows[j].Cells[ColIndexGrid.Select].Style.BackColor = ChkValue.True;
                //                //dgInsTSKU.Rows[j].Cells[ColIndexGrid.Select].Value = true;
                //                dgInsTSKU_CellClick(dgInsTSKU, new DataGridViewCellEventArgs(ColIndexGrid.Select, j));
                //            }
                //        }
                //    }
                //}

                //for (int j = 0; j < dgInsTSKUOther.Rows.Count; j++)
                //{
                //    if (dgInsTSKUOther.Rows[j].Cells[ColIndexGrid.rtype].Value.ToString() != "3" && Convert.ToDouble(dgInsTSKU.Rows[j].Cells[ColIndexGrid.DiscAmt].Value) == 0 &&
                //        dgInsTSKU.Rows[j].Cells[ColIndexGrid.SchemeTypeNo].Value.ToString() == "4")
                //    {
                //        if ("" != dgInsTSKUOther.Rows[j].Cells[ColIndexGrid.SchemeName].FormattedValue.ToString())
                //        {
                //            if (Convert.ToInt64(dgInsTSKUOther.Rows[j].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString()) == Convert.ToInt64(dtTRewardDtls.Rows[i].ItemArray[ColTRewardDtls.SchemeDetailsNo].ToString()))
                //            {
                //                dgInsTSKUOther_CellClick(dgInsTSKUOther, new DataGridViewCellEventArgs(ColIndexGrid.Select, j));
                //            }
                //        }
                //    }
                //}

            }
        }

        public SchemePromoInfo(double Amt, DataGridView dg, bool flag, DataTable dt, long LedgNo, DateTime BillDate, long MTDSchDetailsNo, bool IsEnroll,long ID)
        {
            InitializeComponent();
            dgBill = dg;
            GTotal = Amt;
            LedgerNo = LedgNo;
            dtTRewardDtls = dt;
            this.IsEnroll = IsEnroll;
            this.BillDate = BillDate;
            this.MTDSchDetailsNo = MTDSchDetailsNo;
            this.ID = ID;
        }

        public void Scheme_Load(object sender, EventArgs e)
        {
            try
            {
                //dgInsTSKUOther.Visible = true;
                InitTable();
                InitDelTable();
                CheckSchemeValidation();//check limited loading of data
                dgInsTSKU.Columns[ColIndexGrid.Qty].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgInsTSKU.Columns[ColIndexGrid.BillQty].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (dtTRewardDtls.Rows.Count != 0)
                    FillControl();


                txtPromoCode.Focus();
                //dgInsTSKU.Columns[ColIndexGrid.Select].Visible = ChkFlag;
                //chkSelect.Visible = ChkFlag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void CheckSchemeValidation()
        {
            try
            {
                string strItm = "";
                //DataRow[] dr = null;
                DataGridView dgTemp = dgInsTSKU;
                //for (int i = 0; i < dgBill.Rows.Count; i++)
                //{
                //    if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != "" && dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null)
                //    {
                //        if (strItm == "")
                //            strItm = dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString();
                //        else
                //            strItm = strItm + "," + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString();
                //    }
                //}
                for (int i = 0; i < dgBill.Rows.Count; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value != null)
                    {
                        if (dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() != "")
                        {
                            if (strItm == "")
                                strItm = " (ItemNo = " + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() + " AND MRP=" + ObjQry.ReturnLong("Select MRP From MRateSetting Where PkSrNo=" + dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value.ToString() + "", CommonFunctions.ConStr) + ")";
                            else
                                strItm = strItm + " OR " + " (ItemNo = " + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString() + " AND MRP=" + ObjQry.ReturnLong("Select MRP From MRateSetting Where PkSrNo=" + dgBill.Rows[i].Cells[ColIndex.PkRateSettingNo].Value.ToString() + "", CommonFunctions.ConStr) + ")";
                            //strItm = strItm + "," + dgBill.Rows[i].Cells[ColIndex.ItemNo].Value.ToString();
                        }
                    }
                }
               
                dtTsku = ObjFunction.GetDataView("Exec GetLoyaltyInstantTSKUIw_C '" + BillDate.Date + "','" + strItm + "'," + LedgerNo + ",0,'true',"+ ID +"").Table;
                string SchName = "";
                for (int i = 0; i < dtTsku.Rows.Count; i++)
                {
                    dgTemp.Rows.Add();
                    dgTemp.Rows[i].Cells[18].Value = "Show";
                    dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.Blank;
                    for (int j = 0; j < dgTemp.Columns.Count - 9; j++)
                    {
                        dgTemp.Rows[i].Cells[j].Value = dtTsku.Rows[i].ItemArray[j];

                        if (j == 3)
                        {
                            if (SchName != dtTsku.Rows[i].ItemArray[3].ToString())
                            {
                                dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.False;
                                dgTemp.Rows[i].Cells[19].Value = false;
                                //dgTemp.Rows[i].Cells[18].Value = chk;
                                SchName = dtTsku.Rows[i].ItemArray[3].ToString();

                            }
                            else
                            {
                                dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.Blank;
                                dgTemp.Rows[i].Cells[2].Value = "";
                                dgTemp.Rows[i].Cells[3].Value = "";
                            }
                        }
                    }
                    dgTemp.Rows[i].Cells[ColIndexGrid.MRP].Value = dtTsku.Rows[i].ItemArray[17];
                    if (dtTsku.Rows[i].ItemArray[0].ToString() != "3")
                    {
                        double Qty = 0, LAmt = 0; string UName = "";
                        for (int k = 0; k < dgBill.Rows.Count; k++)
                        {
                            if (dgBill.Rows[k].Cells[1].Value != null)
                            {
                                if (dgBill.Rows[k].Cells[1].Value.ToString() != "")
                                {
                                    double dMRP = 0;
                                    dMRP = ObjQry.ReturnLong("Select MRP From MRateSetting Where PkSrNo=" + dgBill.Rows[k].Cells[ColIndex.PkRateSettingNo].Value.ToString() + "", CommonFunctions.ConStr);
                                    if (dgBill.Rows[k].Cells[ColIndex.ItemNo].Value.ToString() == dtTsku.Rows[i].ItemArray[12].ToString()
                                        && dMRP == Convert.ToDouble(dtTsku.Rows[i].ItemArray[17].ToString()))
                                    {
                                        Qty = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Quantity].Value);
                                        LAmt = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Amount].Value);
                                        UName = dgBill.Rows[k].Cells[ColIndex.UOM].Value.ToString();
                                    }
                                }
                            }
                        }
                        //if (dtTsku.Rows[i].ItemArray[0].ToString() != "3")
                        //if (UName != "")
                        dgTemp.Rows[i].Cells[ColIndexGrid.BillQty].Value = Qty; //+ "-" + UName;
                        dgTemp.Rows[i].Cells[ColIndexGrid.BUom].Value = UName;
                        dgTemp.Rows[i].Cells[ColIndexGrid.LoyaltyFactor].Value = Math.Floor(Qty / Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.Qty].Value));
                        if (Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.DiscAmt].Value) == -1)
                        {
                            dgTemp.Rows[i].Cells[ColIndexGrid.DiscAmt].Value = Convert.ToDouble((LAmt * Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value)) / 100).ToString(Format.DoubleFloating);
                            dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value = "-1";
                        }
                        else dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value = "0";
                        dgTemp.Rows[i].Cells[ColIndexGrid.IsIWScheme].Value = "1";
                        dgTemp.Rows[i].Cells[ColIndexGrid.MainPromoCode].Value = dtTsku.Rows[i].ItemArray[18];
                        //else
                        //  dgTemp.Rows[i].Cells[15].Value = Qty;
                    }
                    else
                    {
                        //dgTemp.Rows[i].Cells[18].Value = txt;
                        // dgTemp.Rows[i].Cells.Add(txt);
                        dgTemp.Rows[i].Cells[19].ReadOnly = true;
                        dgTemp.Rows[i].Cells[19].Style.BackColor = Color.Gray;
                        dgTemp.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                    }
                }

                #region Loyalty TSKU Without Product
                dtTskuOther = ObjFunction.GetDataView("Exec GetLoyaltyInstantTSKUIw '" + BillDate.Date + "','" + strItm + "'," + LedgerNo + ",0,'true'," + ID + "").Table;
                SchName = "";
                int rwindex = 0;
                for (int i = 0; i < dtTskuOther.Rows.Count; i++)
                {
                    dgTemp.Rows.Add();
                    rwindex = dgTemp.Rows.Count - 1;
                    dgTemp.Rows[rwindex].Cells[18].Value = "Show";
                    dgTemp.Rows[rwindex].Cells[19].Style.BackColor = ChkValue.Blank;
                    for (int j = 0; j < dgTemp.Columns.Count - 9; j++)
                    {
                        dgTemp.Rows[rwindex].Cells[j].Value = dtTskuOther.Rows[i].ItemArray[j];

                        if (j == 3)
                        {
                            if (SchName != dtTskuOther.Rows[i].ItemArray[3].ToString())
                            {
                                dgTemp.Rows[rwindex].Cells[19].Style.BackColor = ChkValue.False;
                                dgTemp.Rows[rwindex].Cells[19].Value = false;
                                //dgTemp.Rows[i].Cells[18].Value = chk;
                                SchName = dtTskuOther.Rows[i].ItemArray[3].ToString();

                            }
                            else
                            {
                                dgTemp.Rows[rwindex].Cells[19].Style.BackColor = ChkValue.Blank;
                                dgTemp.Rows[rwindex].Cells[2].Value = "";
                                dgTemp.Rows[rwindex].Cells[3].Value = "";
                            }
                        }
                    }
                    dgTemp.Rows[rwindex].Cells[ColIndexGrid.MRP].Value = dtTskuOther.Rows[i].ItemArray[17];
                    if (dtTskuOther.Rows[i].ItemArray[0].ToString() != "3")
                    {
                        double Qty = 0, LAmt = 0; string UName = "";
                        for (int k = 0; k < dgBill.Rows.Count; k++)
                        {
                            if (dgBill.Rows[k].Cells[1].Value != null)
                            {
                                if (dgBill.Rows[k].Cells[1].Value.ToString() != "")
                                {
                                    double dMRP = 0;
                                    dMRP = ObjQry.ReturnLong("Select MRP From MRateSetting Where PkSrNo=" + dgBill.Rows[k].Cells[ColIndex.PkRateSettingNo].Value.ToString() + "", CommonFunctions.ConStr);
                                    if (dgBill.Rows[k].Cells[ColIndex.ItemNo].Value.ToString() == dtTskuOther.Rows[i].ItemArray[12].ToString()
                                        && dMRP == Convert.ToDouble(dtTskuOther.Rows[i].ItemArray[17].ToString()))
                                    {
                                        Qty = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Quantity].Value);
                                        LAmt = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Amount].Value);
                                        UName = dgBill.Rows[k].Cells[ColIndex.UOM].Value.ToString();
                                    }
                                }
                            }
                        }
                        //if (dtTskuOther.Rows[i].ItemArray[0].ToString() != "3")
                        //if (UName != "")
                        dgTemp.Rows[rwindex].Cells[ColIndexGrid.BillQty].Value = Qty; //+ "-" + UName;
                        dgTemp.Rows[rwindex].Cells[ColIndexGrid.BUom].Value = UName;
                        dgTemp.Rows[rwindex].Cells[ColIndexGrid.LoyaltyFactor].Value = Math.Floor(Qty / Convert.ToDouble(dgTemp.Rows[rwindex].Cells[ColIndexGrid.Qty].Value));
                        //New Logic of Discount structure as per new Concept
                        //if (Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.DiscAmt].Value) == -1)
                        //{
                        //    dgTemp.Rows[i].Cells[ColIndexGrid.DiscAmt].Value = Convert.ToDouble((LAmt * Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value)) / 100).ToString(Format.DoubleFloating);
                        //    dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value = "-1";
                        //}
                        //else dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value = "0";

                        //else
                        //  dgTemp.Rows[i].Cells[15].Value = Qty;
                        dgTemp.Rows[rwindex].Cells[ColIndexGrid.IsIWScheme].Value = "1";
                        dgTemp.Rows[rwindex].Cells[ColIndexGrid.MainPromoCode].Value = dtTskuOther.Rows[i].ItemArray[18];
                    }
                    else
                    {
                        //dgTemp.Rows[i].Cells[18].Value = txt;
                        // dgTemp.Rows[i].Cells.Add(txt);
                        dgTemp.Rows[rwindex].Cells[19].ReadOnly = true;
                        dgTemp.Rows[rwindex].Cells[19].Style.BackColor = Color.Gray;
                        dgTemp.Rows[rwindex].DefaultCellStyle.BackColor = Color.Gray;
                    }
                }

                //manali 

                /*
                //dtTskuIw = ObjFunction.GetDataView("Exec GetLoyaltyInstantTSKU_C '" + BillDate.Date + "','" + strItm + "'," + LedgerNo + ",0").Table;
                //SchName = "";
                //for (int i = 0; i < dtTskuIw.Rows.Count; i++)
                //{
                //    dgTemp.Rows.Add();
                //    dgTemp.Rows[i].Cells[18].Value = "Show";
                //    dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.Blank;
                //    for (int j = 0; j < dgTemp.Columns.Count - 8; j++)
                //    {
                //        dgTemp.Rows[i].Cells[j].Value = dtTskuIw.Rows[i].ItemArray[j];

                //        if (j == 3)
                //        {
                //            if (SchName != dtTskuIw.Rows[i].ItemArray[3].ToString())
                //            {
                //                dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.False;
                //                dgTemp.Rows[i].Cells[19].Value = false;
                //                //dgTemp.Rows[i].Cells[18].Value = chk;
                //                SchName = dtTskuIw.Rows[i].ItemArray[3].ToString();

                //            }
                //            else
                //            {
                //                dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.Blank;
                //                dgTemp.Rows[i].Cells[2].Value = "";
                //                dgTemp.Rows[i].Cells[3].Value = "";
                //            }
                //        }
                //    }
                //    dgTemp.Rows[i].Cells[ColIndexGrid.MRP].Value = dtTskuIw.Rows[i].ItemArray[17];
                //    if (dtTskuIw.Rows[i].ItemArray[0].ToString() != "3")
                //    {
                //        double Qty = 0, LAmt = 0; string UName = "";
                //        for (int k = 0; k < dgBill.Rows.Count; k++)
                //        {
                //            if (dgBill.Rows[k].Cells[1].Value != "" && dgBill.Rows[k].Cells[1].Value != null)
                //            {
                //                double dMRP = 0;
                //                dMRP = ObjQry.ReturnLong("Select MRP From MRateSetting Where PkSrNo=" + dgBill.Rows[k].Cells[ColIndex.PkRateSettingNo].Value.ToString() + "", CommonFunctions.ConStr);
                //                if (dgBill.Rows[k].Cells[ColIndex.ItemNo].Value.ToString() == dtTskuIw.Rows[i].ItemArray[12].ToString()
                //                    && dMRP == Convert.ToDouble(dtTskuIw.Rows[i].ItemArray[17].ToString()))
                //                {
                //                    Qty = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Quantity].Value);
                //                    LAmt = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Amount].Value);
                //                    UName = dgBill.Rows[k].Cells[ColIndex.UOM].Value.ToString();
                //                }
                //            }
                //        }
                //        //if (dtTsku.Rows[i].ItemArray[0].ToString() != "3")
                //        //if (UName != "")
                //        dgTemp.Rows[i].Cells[ColIndexGrid.BillQty].Value = Qty; //+ "-" + UName;
                //        dgTemp.Rows[i].Cells[ColIndexGrid.BUom].Value = UName;
                //        dgTemp.Rows[i].Cells[ColIndexGrid.LoyaltyFactor].Value = Math.Floor(Qty / Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.Qty].Value));
                //        if (Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.DiscAmt].Value) == -1)
                //        {
                //            dgTemp.Rows[i].Cells[ColIndexGrid.DiscAmt].Value = Convert.ToDouble((LAmt * Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value)) / 100).ToString(Format.DoubleFloating);
                //            dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value = "-1";
                //        }
                //        else dgTemp.Rows[i].Cells[ColIndexGrid.PDiscAmt].Value = "0";
                //        //else
                //        //  dgTemp.Rows[i].Cells[15].Value = Qty;
                //        dgTemp.Rows[i].Cells[ColIndexGrid.IsIWScheme].Value = "0";
                //    }
                //    else
                //    {
                //        //dgTemp.Rows[i].Cells[18].Value = txt;
                //        // dgTemp.Rows[i].Cells.Add(txt);
                //        dgTemp.Rows[i].Cells[19].ReadOnly = true;
                //        dgTemp.Rows[i].Cells[19].Style.BackColor = Color.Gray;
                //        dgTemp.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                //    }
                //}

                //manali end

                //manali 
                //dtTskuOtherIw = ObjFunction.GetDataView("Exec GetLoyaltyInstantTSKU '" + BillDate.Date + "','" + strItm + "'," + LedgerNo + ",0").Table;
                //SchName = "";
                //for (int i = 0; i < dtTskuOtherIw.Rows.Count; i++)
                //{
                //    dgTemp.Rows.Add();
                //    dgTemp.Rows[i].Cells[18].Value = "Show";
                //    dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.Blank;
                //    for (int j = 0; j < dgTemp.Columns.Count - 8; j++)
                //    {
                //        dgTemp.Rows[i].Cells[j].Value = dtTskuOtherIw.Rows[i].ItemArray[j];

                //        if (j == 3)
                //        {
                //            if (SchName != dtTskuOtherIw.Rows[i].ItemArray[3].ToString())
                //            {
                //                dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.False;
                //                dgTemp.Rows[i].Cells[19].Value = false;
                //                //dgTemp.Rows[i].Cells[18].Value = chk;
                //                SchName = dtTskuOtherIw.Rows[i].ItemArray[3].ToString();

                //            }
                //            else
                //            {
                //                dgTemp.Rows[i].Cells[19].Style.BackColor = ChkValue.Blank;
                //                dgTemp.Rows[i].Cells[2].Value = "";
                //                dgTemp.Rows[i].Cells[3].Value = "";
                //            }
                //        }
                //    }
                //    dgTemp.Rows[i].Cells[ColIndexGrid.MRP].Value = dtTskuOtherIw.Rows[i].ItemArray[17];
                //    if (dtTskuOtherIw.Rows[i].ItemArray[0].ToString() != "3")
                //    {
                //        double Qty = 0, LAmt = 0; string UName = "";
                //        for (int k = 0; k < dgBill.Rows.Count; k++)
                //        {
                //            if (dgBill.Rows[k].Cells[1].Value != "" && dgBill.Rows[k].Cells[1].Value != null)
                //            {
                //                double dMRP = 0;
                //                dMRP = ObjQry.ReturnLong("Select MRP From MRateSetting Where PkSrNo=" + dgBill.Rows[k].Cells[ColIndex.PkRateSettingNo].Value.ToString() + "", CommonFunctions.ConStr);
                //                if (dgBill.Rows[k].Cells[ColIndex.ItemNo].Value.ToString() == dtTskuOtherIw.Rows[i].ItemArray[12].ToString()
                //                    && dMRP == Convert.ToDouble(dtTskuOtherIw.Rows[i].ItemArray[17].ToString()))
                //                {
                //                    Qty = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Quantity].Value);
                //                    LAmt = Convert.ToDouble(dgBill.Rows[k].Cells[ColIndex.Amount].Value);
                //                    UName = dgBill.Rows[k].Cells[ColIndex.UOM].Value.ToString();
                //                }
                //            }
                //        }

                //        dgTemp.Rows[i].Cells[ColIndexGrid.BillQty].Value = Qty; //+ "-" + UName;
                //        dgTemp.Rows[i].Cells[ColIndexGrid.BUom].Value = UName;
                //        dgTemp.Rows[i].Cells[ColIndexGrid.LoyaltyFactor].Value = Math.Floor(Qty / Convert.ToDouble(dgTemp.Rows[i].Cells[ColIndexGrid.Qty].Value));
                //        dgTemp.Rows[i].Cells[ColIndexGrid.IsIWScheme].Value = "0";
                //    }
                //    else
                //    {
                //        dgTemp.Rows[i].Cells[19].ReadOnly = true;
                //        dgTemp.Rows[i].Cells[19].Style.BackColor = Color.Gray;
                //        dgTemp.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                //    }
                //}
                //manali end
                 */
                #endregion

            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void BtnOk_Click(object sender, EventArgs e)
        {
            int cntPromo = 0;
            for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
            {
                if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.MainPromoCode].Value != null)
                {
                    if (dgInsTSKU.Rows[i].Cells[25].Value == null || dgInsTSKU.Rows[i].Cells[25].Value.ToString() == "")
                    {
                        cntPromo++;
                    }
                }
            }
            if (cntPromo > 0) // temp
            {
                if (OMMessageBox.Show("Do you want to continue without promo code..?", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                {
                    WithoutPromo = 1;
                    DS = DialogResult.Cancel;
                    this.Close();
                }
            }
            else
            {
                //SaveData();
                FrmTemp = this;
                DS = DialogResult.OK;
                this.Close();
            }
        }

        public void SaveData()  
        {
            try
            {
                for (int i = 0; i < dgBill.Rows.Count - 1; i++)
                {
                    if (dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() != "" || dgBill.Rows[i].Cells[ColIndex.SchemeFromNo].FormattedValue.ToString() != "" || dgBill.Rows[i].Cells[ColIndex.SchemeToNo].FormattedValue.ToString() != "")
                    {
                        if (dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() != "" && dgBill.Rows[i].Cells[ColIndex.SchemeFromNo].FormattedValue.ToString() != "" && dgBill.Rows[i].Cells[ColIndex.SchemeToNo].FormattedValue.ToString() == "")
                        {
                            DeleteDtls(10, Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.RewardFromNo].FormattedValue.ToString()));// 10 For RewardFrom
                            dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].Value = "";
                            dgBill.Rows[i].Cells[ColIndex.SchemeFromNo].Value = "";
                            dgBill.Rows[i].Cells[ColIndex.RewardFromNo].Value = "";

                        }
                        else if (dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString() != "" && dgBill.Rows[i].Cells[ColIndex.SchemeFromNo].FormattedValue.ToString() == "" && dgBill.Rows[i].Cells[ColIndex.SchemeToNo].FormattedValue.ToString() != "")// && dgBill.Rows[i].Cells[ColIndex.RewardFromNo].FormattedValue.ToString() != "" && dgBill.Rows[i].Cells[ColIndex.RewardToNo].FormattedValue.ToString() != "")
                        {

                            //DataRow[] dr = dtTRewardDtls.Select("SchemeDetailsNo=" + dgBill.Rows[i].Cells[ColIndex.SchemeDetailsNo].FormattedValue.ToString());
                            //if (dr.Length != 0)
                            //{
                            //    DeleteDtls(6, Convert.ToInt64(dr[0].ItemArray[ColTRewardDtls.PkSrNo].ToString()));// 9 For RewardDetailsNo
                            //    DeleteDtls(5, Convert.ToInt64(dr[0].ItemArray[ColTRewardDtls.RewardNo].ToString()));// 11 For RewardTo
                            //    DeleteDtls(1, Convert.ToInt64(dgBill.Rows[i].Cells[ColIndex.PkStockTrnNo].FormattedValue.ToString()));// 2 For 
                            //}

                            dgBill.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                }
                int cntReward = 0;
                DataTable dtTempRwdtls = dtTRewardDtls.Clone();
                for (int i = 0; i < dtTRewardDtls.Rows.Count; i++)
                {
                    DataRow drTemp = dtTempRwdtls.NewRow();
                    for (int j = 0; j < dtTempRwdtls.Columns.Count; j++)
                        drTemp[j] = dtTRewardDtls.Rows[i].ItemArray[j].ToString();
                    dtTempRwdtls.Rows.Add(drTemp);
                }
                DataTable dtTempRwToFromdtls = dtTRewardToFrom.Clone();
                for (int i = 0; i < dtTRewardToFrom.Rows.Count; i++)
                {
                    DataRow drTemp = dtTempRwToFromdtls.NewRow();
                    for (int j = 0; j < dtTempRwToFromdtls.Columns.Count; j++)
                        drTemp[j] = dtTRewardToFrom.Rows[i].ItemArray[j].ToString();
                    dtTempRwToFromdtls.Rows.Add(drTemp);
                }

                while (dtTRewardDtls.Rows.Count > 0)
                    dtTRewardDtls.Rows.RemoveAt(0);

                while (dtTRewardToFrom.Rows.Count > 0)
                    dtTRewardToFrom.Rows.RemoveAt(0);


                for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                {
                    if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.Select].Style.BackColor == ChkValue.True)
                    {
                        DataTable DtDetails = ObjFunction.GetDataView("SELECT MSchemeDetails.DiscPercentage, MSchemeDetails.DiscAmount, MSchemeFromDetails.BillAmount FROM MSchemeDetails INNER JOIN MSchemeFromDetails ON MSchemeDetails.PkSrNo = MSchemeFromDetails.SchemeDetailsNo WHERE (MSchemeDetails.PkSrNo = " + Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].Value) + ")").Table;
                        DataRow dr = dtTRewardDtls.NewRow();
                        dr[ColTRewardDtls.PkSrNo] = (dtTempRwdtls.Rows.Count <= cntReward) ? "0" : dtTempRwdtls.Rows[cntReward].ItemArray[0].ToString();
                        dr[ColTRewardDtls.RewardNo] = (dtTempRwdtls.Rows.Count <= cntReward) ? "0" : dtTempRwdtls.Rows[cntReward].ItemArray[1].ToString();
                        dr[ColTRewardDtls.SchemeNo] = dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeNo].Value;
                        dr[ColTRewardDtls.SchemeDetailsNo] = dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].Value;
                        dr[ColTRewardDtls.SchemeType] = dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].Value;
                        dr[ColTRewardDtls.DiscPer] = Convert.ToDouble(DtDetails.Rows[0].ItemArray[0].ToString());
                        dr[ColTRewardDtls.DiscAmount] = Convert.ToDouble(DtDetails.Rows[0].ItemArray[1].ToString());
                        dr[ColTRewardDtls.SchemeAmount] = Convert.ToDouble(DtDetails.Rows[0].ItemArray[2].ToString());
                        dr[ColTRewardDtls.Status] = 0;
                        dtTRewardDtls.Rows.Add(dr);
                        cntReward++;

                        DataTable SchemeFrom = ObjFunction.GetDataView("SELECT PkSrNo,ItemNo FROM MSchemeFromDetails WHERE (SchemeDetailsNo = " + Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].Value) + ")").Table;
                        for (int f = 0; f < SchemeFrom.Rows.Count; f++)
                        {
                            DataRow drTo = dtTRewardToFrom.NewRow();
                            drTo[ColTRewardToFrom.TypeNo] = 1;
                            drTo[ColTRewardToFrom.PkSrNo] = 0;
                            drTo[ColTRewardToFrom.RewardNo] = 0;
                            drTo[ColTRewardToFrom.RewardDetailsNo] = 0;
                            drTo[ColTRewardToFrom.SchemeDetailsNo] = dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].Value;
                            drTo[ColTRewardToFrom.SchemeFromNo] = SchemeFrom.Rows[f].ItemArray[0].ToString();
                            drTo[ColTRewardToFrom.FkStockNo] = 0;
                            drTo[ColTRewardToFrom.ItemNo] = SchemeFrom.Rows[f].ItemArray[1].ToString();
                            dtTRewardToFrom.Rows.Add(drTo);
                            for (int d = 0; d < dgBill.Rows.Count - 1; d++)
                            {
                                if (Convert.ToInt64(dgBill.Rows[d].Cells[ColIndex.ItemNo].FormattedValue.ToString()) == Convert.ToInt64(SchemeFrom.Rows[f].ItemArray[1].ToString()))
                                {
                                    dgBill.Rows[d].Cells[ColIndex.SchemeDetailsNo].Value = dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].Value;
                                    dgBill.Rows[d].Cells[ColIndex.SchemeFromNo].Value = SchemeFrom.Rows[f].ItemArray[0].ToString();
                                    break;
                                }
                            }
                        }

                        DataTable SchemeTo = ObjFunction.GetDataView("SELECT PkSrNo,ItemNo FROM MSchemeToDetails WHERE (SchemeDetailsNo =  " + Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].Value) + " )").Table;
                        for (int t = 0; t < SchemeTo.Rows.Count; t++)
                        {
                            DataRow drTo = dtTRewardToFrom.NewRow();
                            drTo[ColTRewardToFrom.TypeNo] = 2;
                            drTo[ColTRewardToFrom.PkSrNo] = 0;
                            drTo[ColTRewardToFrom.RewardNo] = 0;
                            drTo[ColTRewardToFrom.RewardDetailsNo] = 0;
                            drTo[ColTRewardToFrom.SchemeDetailsNo] = dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].Value;
                            drTo[ColTRewardToFrom.SchemeFromNo] = SchemeTo.Rows[t].ItemArray[0].ToString();
                            drTo[ColTRewardToFrom.FkStockNo] = 0;
                            drTo[ColTRewardToFrom.ItemNo] = SchemeTo.Rows[t].ItemArray[1].ToString();
                            dtTRewardToFrom.Rows.Add(drTo);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dgInsTSKU.Rows.Count;i++)
            {
                dgInsTSKU.Rows[i].Cells[ColIndexGrid.PromoCode].Value = "";
            }
            DS = DialogResult.Cancel;
            this.Close();
        }

        #region Delete code
        public void InitDelTable()
        {
            dtDeleteReward.Columns.Add();
            dtDeleteReward.Columns.Add();
        }

        public void DeleteDtls(int Type, long PkNo)
        {
            DataRow dr = null;
            dr = dtDeleteReward.NewRow();
            dr[0] = Type;
            dr[1] = PkNo;
            dtDeleteReward.Rows.Add(dr);
        }

        #endregion

        #region ColumnIndex


        public static class ChkValue
        {
            public static Color Blank = Color.White;
            public static Color True = Color.Blue;
            public static Color False = Color.WhiteSmoke;

        }

        public static class ColIndex
        {
            public static int SrNo = 0;
            public static int ItemName = 1;
            public static int Quantity = 2;
            public static int UOM = 3;
            public static int Rate = 4;
            public static int NetRate = 5;
            public static int DiscPercentage = 6;
            public static int DiscAmount = 7;
            public static int DiscRupees = 8;
            public static int DiscPercentage2 = 9;
            public static int DiscAmount2 = 10;
            public static int DiscRupees2 = 11;
            public static int NetAmt = 12;
            public static int Amount = 13;
            public static int Barcode = 14;
            public static int PkStockTrnNo = 15;
            public static int PkBarCodeNo = 16;
            public static int PkVoucherNo = 17;
            public static int ItemNo = 18;
            public static int UOMNo = 19;
            public static int TaxLedgerNo = 20;
            public static int SalesLedgerNo = 21;
            public static int PkRateSettingNo = 22;
            public static int PkItemTaxInfo = 23;
            public static int StockFactor = 24;
            public static int ActualQty = 25;
            public static int MKTQuantity = 26;
            public static int SGSTPercentage = 27;
            public static int SGSTAmount = 28;
            public static int SalesVchNo = 29;
            public static int TaxVchNo = 30;
            public static int StockCompanyNo = 31;
            public static int SchemeDetailsNo = 32;
            public static int SchemeFromNo = 33;
            public static int SchemeToNo = 34;
            public static int RewardFromNo = 35;
            public static int RewardToNo = 36;
        }

        public static class ColIndexGrid
        {
            public static int rtype = 0;
            public static int SchemeNo = 1;
            public static int SchemeUserNo = 2;
            public static int SchemeName = 3;
            public static int ItemName = 4;
            public static int SchemeTypeNo = 5;
            public static int SchDate = 6;
            public static int SchePerFrom = 7;
            public static int SchPerTo = 8;
            public static int DiscAmt = 9;
            public static int PDiscAmt = 10;
            public static int SchDtlsPksrNo = 11;
            public static int ItemNo = 12;
            public static int Qty = 13;
            public static int Uom = 14;
            public static int UomNo = 15;
            public static int BillQty = 16;
            public static int BUom = 17;
            public static int BtnDetails = 18;
            public static int Select = 19;
            public static int chk = 20;
            public static int MRP = 21;
            public static int LoyaltyFactor = 22;
            public static int MainPromoCode = 23;
            public static int IsIWScheme = 24;
            public static int PromoCode = 25;

        }

        public static class ColInst
        {
            public static int rType = 0;
            public static int SchemeNo = 1;
            public static int SchemeName = 2;
            public static int BillAmount = 3;
            public static int SchemeTypeNo = 4;
            public static int SchemeUserNo = 5;
            public static int SchemeDatedatetime = 6;
            public static int SchemePeriodFromdatetime = 7;
            public static int SchemePeriodTodatetime = 8;
            public static int DiscAmount = 9;
            public static int DiscPercentage = 10;
            public static int PDiscAmount = 11;
            public static int InstantBillAmount = 12;
            public static int SchemeDetailsNo = 13;
        }

        public static class ColTRewardDtls
        {
            public static int PkSrNo = 0;
            public static int RewardNo = 1;
            public static int SchemeNo = 2;
            public static int SchemeDetailsNo = 3;
            public static int SchemeType = 4;
            public static int DiscPer = 5;
            public static int DiscAmount = 6;
            public static int SchemeAmount = 7;
            public static int Status = 8;
            public static int SchemeAchieverNo = 9;
        }

        public static class ColTRewardToFrom
        {
            public static int TypeNo = 0;
            public static int PkSrNo = 1;
            public static int RewardNo = 2;
            public static int RewardDetailsNo = 3;
            public static int SchemeDetailsNo = 4;
            public static int SchemeFromNo = 5;
            public static int FkStockNo = 6;
            public static int ItemNo = 7;
            public static int LoyaltyFactor = 8;

        }

        public static class ColMTDIndex
        {
            public static int rType = 0;
            public static int SchemeNo = 1;
            public static int SchemeTypeNo = 2;
            public static int SchemeUserNo = 3;
            public static int SchemeName = 4;
            public static int ProdDiscStatus = 5;
            public static int IsProdDisc = 6;
            public static int AmtDisc = 7;
            public static int RedemAmt = 8;
            public static int DiscBalAmt = 9;
            public static int AdjustAmt = 10;
            public static int SchemeDetailsNo = 11;
            public static int SchemeDate = 12;
            public static int SchemePeriodFromDate = 13;
            public static int SchemePeriodToDate = 14;
            public static int SchemeAchiverNo = 15;
            public static int BtnDetails = 16;
            public static int Select = 17;
            public static int chk = 18;
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
            if (e.KeyCode == Keys.F4)
            {
                btnApply_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                BtnCancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                BtnOk_Click(sender, e);
            }
        }
        #endregion

        private void btnADetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (InsA != 0)
                {
                    Form NewF = new Vouchers.SchemeDetails(InsSchemeNo, InsA);
                    ObjFunction.OpenForm(NewF);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnNDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (InsN != 0)
                {
                    Form NewF = new Vouchers.SchemeDetails(InsSchemeNo, InsN);
                    ObjFunction.OpenForm(NewF);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgInsTSKU_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgInsTSKU.CurrentRow != null)
                {
                    if (dgInsTSKU.CurrentRow.Index >= 0)
                    {
                        if (dgInsTSKU.CurrentCell.ColumnIndex == ColIndexGrid.BtnDetails)
                        {
                            if (dgInsTSKU.Rows[dgInsTSKU.CurrentRow.Index].Cells[ColIndexGrid.SchemeUserNo].Value.ToString().Trim() != "")
                            {
                                Form NewF = new Vouchers.SchemeDetails(Convert.ToInt64(dgInsTSKU.Rows[dgInsTSKU.CurrentRow.Index].Cells[ColIndexGrid.SchemeNo].Value), Convert.ToInt64(dgInsTSKU.Rows[dgInsTSKU.CurrentRow.Index].Cells[ColIndexGrid.SchDtlsPksrNo].Value));
                                ObjFunction.OpenForm(NewF);
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public void InitTable()
        {
            try
            {
                if (dtTRewardDtls == null)
                    dtTRewardDtls = ObjFunction.GetDataView("SELECT PkSrNo, RewardNo, SchemeNo, SchemeDetailsNo, SchemeType, DiscPer, DiscAmount, SchemeAmount,0 as Status,SchemeAchieverNo FROM TRewardDetails WHERE (PkSrNo = 0)").Table;
                if (dtTRewardToFrom == null)
                    dtTRewardToFrom = ObjFunction.GetDataView("SELECT 0 AS TypeNo,PkSrNo, RewardNo, RewardDetailsNo,SchemeDetailsNo, SchemeFromNo, FkStockNo,0 As 'ItemNo',0.00 AS LoyaltyFactor FROM TRewardFrom WHERE (PkSrNo = 0)").Table;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void dgInsTSKU_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == ColIndexGrid.Select)
                    {
                        if (dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Select].Style.BackColor != ChkValue.Blank)
                        {
                            if (dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.rtype].Value.ToString() != "3")
                            {
                                if (Convert.ToBoolean(dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Select].EditedFormattedValue) == true)
                                {
                                    dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Select].Value = false;
                                    dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Select].Style.BackColor = ChkValue.False;
                                }
                                else
                                {
                                    dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Select].Value = true;
                                    dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Select].Style.BackColor = ChkValue.True;
                                }
                                ChkeckValidation(e.RowIndex,e.ColumnIndex);
                            }
                        }
                        dgInsTSKU.CurrentCell = null;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
      
        public void ChkeckValidation(int RowInex,int ColumnIndex)
        {
            try
            {
                //bool TFlag = false;
                if (ColumnIndex == ColIndexGrid.Select)
                {
                    if (dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.Select].Style.BackColor == ChkValue.True)
                    {
                        if (dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.rtype].Value.ToString() != "3")
                        {
                            long schemeDetailNo = Convert.ToInt64(dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString());
                            int Index = RowInex; int Start = RowInex;
                            double minLoyaltyFlag = 0;
                            minLoyaltyFlag = Convert.ToDouble(dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.LoyaltyFactor].FormattedValue.ToString());
                            for (int i = Index; i < dgInsTSKU.Rows.Count; i++)
                            {
                                if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.rtype].Value.ToString() != "3")
                                {
                                    if (schemeDetailNo == Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString()))
                                    {
                                        if(Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.LoyaltyFactor].FormattedValue.ToString()) < minLoyaltyFlag)
                                            minLoyaltyFlag = Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.LoyaltyFactor].FormattedValue.ToString());

                                        if (Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.BillQty].Value) >= Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.Qty].Value) 
                                            && ValidRow(Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.ItemNo].Value), Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.MRP].Value)) == true)
                                        {
                                            //TFlag = true;
                                            dgInsTSKU.Rows[i].Cells[ColIndexGrid.chk].Value = true;
                                            //break;
                                        }
                                        else
                                        {
                                            //TFlag = false;
                                            dgInsTSKU.Rows[Start].Cells[ColIndexGrid.Select].Style.BackColor = ChkValue.False;
                                            dgInsTSKU.Rows[Start].Cells[ColIndexGrid.chk].Value = false;
                                            dgInsTSKU.Rows[Start].Cells[ColIndexGrid.Select].Value = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (Convert.ToBoolean(dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.chk].Value) == true)
                            {
                                dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.LoyaltyFactor].Value = minLoyaltyFlag.ToString();
                                dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.DiscAmt].Value = (Convert.ToDouble(dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.DiscAmt].Value) * minLoyaltyFlag).ToString();
                            }
                        }

                    }
                    else
                    {
                        if (dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.rtype].Value.ToString() != "3")
                        {
                            long schemeDetailNo = Convert.ToInt64(dgInsTSKU.Rows[RowInex].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString());
                            int Index = RowInex; int Start = RowInex;

                            for (int i = Index; i < dgInsTSKU.Rows.Count; i++)
                            {
                                if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.rtype].Value.ToString() != "3")
                                {
                                    if (schemeDetailNo == Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchDtlsPksrNo].FormattedValue.ToString()))
                                    {
                                        dgInsTSKU.Rows[i].Cells[ColIndexGrid.chk].Value = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        public bool ValidRow(long ItemNo,double MRP)
        {
            bool flag = true;
            try
            {
                for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dgInsTSKU.Rows[i].Cells[ColIndexGrid.chk].FormattedValue.ToString()) == true && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString()) == 0
                         && dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString() == "3"
                        && dgInsTSKU.Rows[i].Cells[ColIndexGrid.IsIWScheme].FormattedValue.ToString() == "1")
                    {
                        if (Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.ItemNo].Value.ToString()) == ItemNo && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.MRP].FormattedValue.ToString()) == MRP)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dgInsTSKU.Rows[i].Cells[ColIndexGrid.chk].FormattedValue.ToString()) == true && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString()) > 0
                             && dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString() == "3"
                            && dgInsTSKU.Rows[i].Cells[ColIndexGrid.IsIWScheme].FormattedValue.ToString() == "1")
                        {
                            if (Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.ItemNo].Value.ToString()) == ItemNo && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.MRP].FormattedValue.ToString()) == MRP)
                            {
                                flag = false;
                                break;
                            }
                        }
                    }

                    if (flag)
                    {
                        for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dgInsTSKU.Rows[i].Cells[ColIndexGrid.chk].FormattedValue.ToString()) == true && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString()) == 0
                            && dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString() == "3"
                            && dgInsTSKU.Rows[i].Cells[ColIndexGrid.IsIWScheme].FormattedValue.ToString() == "0")
                            {
                                if (Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.ItemNo].Value.ToString()) == ItemNo && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.MRP].FormattedValue.ToString()) == MRP)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }

                        if (flag)
                        {
                            for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                            {
                                if (Convert.ToBoolean(dgInsTSKU.Rows[i].Cells[ColIndexGrid.chk].FormattedValue.ToString()) == true && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString()) > 0
                                     && dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeTypeNo].FormattedValue.ToString() == "3"
                                    && dgInsTSKU.Rows[i].Cells[ColIndexGrid.IsIWScheme].FormattedValue.ToString() == "0")
                                {
                                    if (Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.ItemNo].Value.ToString()) == ItemNo && Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.MRP].FormattedValue.ToString()) == MRP)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                return flag;
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
                return false;
            }
        }

        private void dgInsTSKU_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1 )
            {
                //if (dtTsku.Rows[e.RowIndex].ItemArray[0].ToString() == "3" || dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.SchemeUserNo].Value.ToString().Trim() == "")
                //{
                //    e.PaintBackground(e.CellBounds, false);
                //    e.Handled = true;
                //}
                if (Convert.IsDBNull(dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Qty].Value) == false && dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Qty].Value != null)
                {
                    if (Convert.ToDouble(dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.Qty].Value.ToString()) > Convert.ToDouble(dgInsTSKU.Rows[e.RowIndex].Cells[ColIndexGrid.BillQty].Value.ToString()))
                    {
                        dgInsTSKU.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Yellow;
                        //e.Handled = true;
                    }
                }
            }
        }

        public void CheckAllSchemes(bool isAllowItemDisc, bool isAllowFooterDisc)
        {
            try
            {
                if (IsEnroll)
                {
                    if (isAllowItemDisc)
                    {
                        for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                        {
                            if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.Select].Style.BackColor == ChkValue.False)
                            {
                                dgInsTSKU_CellClick(dgInsTSKU, new DataGridViewCellEventArgs(ColIndexGrid.Select, i));
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                        {
                            if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.Select].Style.BackColor != ChkValue.False)
                            {
                                dgInsTSKU_CellClick(dgInsTSKU, new DataGridViewCellEventArgs(ColIndexGrid.Select, i));
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CheckAllSchemes(true, true);
        }

        private void dgPromo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dgPromo.CurrentRow.Index > -1)
                {
                    if (OMMessageBox.Show("Are you sure want to remove promo code from list.", CommonFunctions.ErrorTitle, OMMessageBoxButton.YesNo, OMMessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        long sNo = Convert.ToInt64(dgPromo.CurrentRow.Cells[2].Value);
                        for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                        {
                            if (sNo == Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeNo].Value))
                            {
                                dgInsTSKU.Rows[i].Cells[ColIndexGrid.PromoCode].Value = "";
                            }
                        }
                        dgPromo.Rows.RemoveAt(dgPromo.CurrentRow.Index);
                    }
                }
            }
        }

        private void txtPromoCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                long PrSchNo = 0;
                if (txtPromoCode.Text != "")
                {
                    bool flagExist = false;
                    PrSchNo = ObjQry.ReturnLong("Select SchemeNo From MSchemeAssign Where LedgerNo="+ LedgerNo +" AND  PromoCode='" + txtPromoCode.Text + "'", CommonFunctions.ConStr);
                    if (PrSchNo <= 0)
                    {
                        OMMessageBox.Show("Please enter valid Promo Code...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                        txtPromoCode.Focus();
                        return;
                    }
                    for (int i = 0; i < dgPromo.Rows.Count; i++)
                    {
                        if (txtPromoCode.Text == dgPromo.Rows[i].Cells[1].Value.ToString())
                        {
                            flagExist = true;
                            break;
                        }
                    }
                    if (flagExist == false)
                    {
                        dgPromo.Rows.Add();
                        dgPromo.Rows[dgPromo.Rows.Count - 1].Cells[0].Value = dgPromo.Rows.Count.ToString();
                        dgPromo.Rows[dgPromo.Rows.Count - 1].Cells[1].Value = txtPromoCode.Text.ToUpper();
                        dgPromo.Rows[dgPromo.Rows.Count - 1].Cells[2].Value = PrSchNo;
                        txtPromoCode.Text = "";
                    }
                    else
                    {
                        OMMessageBox.Show("Promo code is already exist...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    }
                    txtPromoCode.Focus();
                }
                else
                {
                    OMMessageBox.Show("Please Enter Promo Code...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                    txtPromoCode.Focus();
                }
            }
        }

        private void dgPromo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long sNo = Convert.ToInt64(dgPromo.CurrentRow.Cells[2].Value);
            for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
            {
                if (sNo == Convert.ToInt64(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeNo].Value))
                {
                    dgInsTSKU.Rows[i].Cells[ColIndexGrid.PromoCode].Value = dgPromo.CurrentRow.Cells[1].Value;
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < dgPromo.Rows.Count; k++)
            {
                for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
                {
                    if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.MainPromoCode].Value != null && Convert.IsDBNull(dgInsTSKU.Rows[i].Cells[ColIndexGrid.BillQty].Value) == false
                        && Convert.IsDBNull(dgInsTSKU.Rows[i].Cells[ColIndexGrid.BillQty].Value) == false)
                    {
                        if (Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.Qty].Value.ToString()) <= Convert.ToDouble(dgInsTSKU.Rows[i].Cells[ColIndexGrid.BillQty].Value.ToString())
                            && dgInsTSKU.Rows[i].Cells[ColIndexGrid.MainPromoCode].Value.ToString() == dgPromo.Rows[k].Cells[1].Value.ToString())
                        {
                            dgInsTSKU.Rows[i].Cells[ColIndexGrid.PromoCode].Value = dgPromo.Rows[k].Cells[1].Value;
                        }
                    }
                }
            }
        }

        public StringCollection GetInvalidScheme()
        {
            StringCollection strCollect = new StringCollection();
            for (int i = 0; i < dgInsTSKU.Rows.Count; i++)
            {
                if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.rtype].Value.ToString() == "1")
                {
                    if (dgInsTSKU.Rows[i].Cells[ColIndexGrid.PromoCode].Value == null || dgInsTSKU.Rows[i].Cells[ColIndexGrid.PromoCode].Value.ToString() == "")
                    {
                        strCollect.Add(dgInsTSKU.Rows[i].Cells[ColIndexGrid.SchemeNo].Value.ToString());
                    }
                }
            }
            return strCollect;
        }
    }
}



//public void ChkeckValidationMTD(int RowInex)
//{
//    bool TFlag = false;
//    if (dgMTD.CurrentCell.ColumnIndex == ColMTDIndex.Select)
//    {
//        if (dgMTD.Rows[RowInex].Cells[ColMTDIndex.Select].Style.BackColor == ChkValue.True)
//        {
//            long schemeDetailNo = Convert.ToInt64(dgMTD.Rows[RowInex].Cells[ColMTDIndex.SchemeDetailsNo].FormattedValue.ToString());
//            int Index = RowInex; int Start = RowInex;

//            for (int i = Index; i < dgMTD.Rows.Count; i++)
//            {
//                if (schemeDetailNo == Convert.ToInt64(dgMTD.Rows[i].Cells[ColMTDIndex.SchemeDetailsNo].FormattedValue.ToString()))
//                {
//                    if (Convert.ToDouble(dgMTD.Rows[i].Cells[ColMTDIndex.BillQty].Value) >= Convert.ToDouble(dgMTD.Rows[i].Cells[ColMTDIndex.Qty].Value))
//                    {
//                        TFlag = true;
//                        dgMTD.Rows[i].Cells[ColMTDIndex.chk].Value = true;
//                    }
//                    else
//                    {
//                        TFlag = false;
//                        dgMTD.Rows[Start].Cells[ColMTDIndex.Select].Style.BackColor = ChkValue.False;
//                        dgMTD.Rows[Start].Cells[ColMTDIndex.chk].Value = false;
//                        break;
//                    }
//                }
//            }
//        }
//        else
//        {
//            if (dgMTD.Rows[RowInex].Cells[ColMTDIndex.rtype].Value.ToString() != "3")
//            {
//                long schemeDetailNo = Convert.ToInt64(dgMTD.Rows[RowInex].Cells[ColMTDIndex.SchDtlsPksrNo].FormattedValue.ToString());
//                int Index = RowInex; int Start = RowInex;

//                for (int i = Index; i < dgMTD.Rows.Count; i++)
//                {
//                    if (dgMTD.Rows[i].Cells[ColMTDIndex.rtype].Value.ToString() != "3")
//                    {
//                        if (schemeDetailNo == Convert.ToInt64(dgMTD.Rows[i].Cells[ColMTDIndex.SchDtlsPksrNo].FormattedValue.ToString()))
//                        {
//                            dgMTD.Rows[i].Cells[ColMTDIndex.chk].Value = false;
//                        }
//                    }
//                }
//            }
//        }
//    }
//}