using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OMControls;
using System.Runtime.InteropServices;
using System.Data;

namespace OM
{
    class DBBarCodePrint
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.Transactions ObjTrans = new Transaction.Transactions();
        Transaction.GetDataSet ObjDset = new Transaction.GetDataSet();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        DataTable dBarCodeSetting;
        BarCodePrintCollection barcodeprintcollection = new BarCodePrintCollection();

        public void AddBarCode(BarCodePrint barcodeprint)
        {
            barcodeprintcollection.Add(barcodeprint);
        }

        public bool BarCodePrinting(long TemplateNo)
        {
            try
            {
                long PrintType;
                dBarCodeSetting = ObjFunction.GetDataView("Select PrinterName,ScriptData,NoOfColumn From MBarcodeTemplate Where PKSRNo=" + TemplateNo + "").Table;
                string strScript = dBarCodeSetting.Rows[0].ItemArray[1].ToString();
                strScript = strScript.Replace("\r", "");
                string[] str = new string[1];
                str[0] = "\n";
               // str[1] = "\"";
                PrintType = Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_BarCodePrintType));
                string[] strLine = strScript.Split(str, StringSplitOptions.None);
                int HeaderIndex = 0;
                StringCollection strcollect = new StringCollection();
                for (int j = 0; j < strLine.Length; j++)
                {
                    if (PrintType == BarcodePrinterType.TSC)
                    {
                        if (strLine[j].IndexOf("CLS") >= 0)
                        {
                            HeaderIndex = j;
                            break;
                        }
                        strcollect.Add(strLine[j]);
                    }
                    //else if (PrintType == BarcodePrinterType.Godex)
                    //{
                    //    if (strLine[j].IndexOf("Th:m:s") >= 0)
                    //    {
                    //        HeaderIndex = j;
                    //        break;
                    //    }
                    //}
                    //else if (PrintType == BarcodePrinterType.Argox)
                    //{
                    //    if (strLine[j].IndexOf("Th:m:s") >= 0)
                    //    {
                    //        HeaderIndex = j;
                    //        break;
                    //    }
                    //}
                    
                }

                for (int i = 0; i < barcodeprintcollection.Count; i++)
                {
                    for (int j = HeaderIndex; j < strLine.Length; j++)
                    {
                        string rdline = strLine[j];
                        int pos = rdline.IndexOf("Var");
                        if (pos != -1)
                        {
                            string varName= "";
                            if (PrintType == BarcodePrinterType.TSC)
                                varName = rdline.Substring(pos + 3, (rdline.Length - 1) - (pos + 3)).Replace("\\", "");
                            else if (PrintType == BarcodePrinterType.Godex)
                                varName = rdline.Substring(pos + 3, (rdline.Length - 1) - (pos + 2)).Replace("\\", "");
                            else if (PrintType == BarcodePrinterType.Argox)
                                varName = rdline.Substring(pos + 3, (rdline.Length - 1) - (pos + 3)).Replace("\\", "");

                            if (varName.ToLower().Equals("barcode"))
                            {
                                rdline = rdline.Replace("VarBarcode", barcodeprintcollection[i].VarBarCode);
                            }
                            else if (varName.ToLower().Equals("barcodeno"))
                            {
                                rdline = rdline.Replace("VarBarcodeno", barcodeprintcollection[i].VarBarCode);
                            }
                            else if (varName.ToLower().Equals("firmname"))
                            {
                                rdline = rdline.Replace("VarFirmName", barcodeprintcollection[i].VarFirmName);
                            }
                            else if (varName.ToLower().Equals("mrp"))
                            {
                                rdline = rdline.Replace("VarMRP", barcodeprintcollection[i].VarMRP);
                            }
                            else if (varName.ToLower().Equals("rate"))
                            {
                                rdline = rdline.Replace("VarRate", barcodeprintcollection[i].VarRate);
                            }
                            else if (varName.ToLower().Equals("weight"))
                            {
                                rdline = rdline.Replace("VarWeight", barcodeprintcollection[i].VarWeight);
                            }
                            else if (varName.ToLower().Equals("brand"))
                            {
                                rdline = rdline.Replace("VarBrand", barcodeprintcollection[i].VarBrand);
                            }
                            else if (varName.ToLower().Equals("shortdesc"))
                            {
                                rdline = rdline.Replace("VarShortDesc", barcodeprintcollection[i].VarBrand + " " + barcodeprintcollection[i].VarShortDesc);
                            }
                            else if (varName.ToLower().Equals("packeddate"))
                            {
                                rdline = rdline.Replace("VarPackedDate", barcodeprintcollection[i].VarPackedDate);
                            }
                            else if (varName.ToLower().Equals("bestbefore"))
                            {
                                rdline = rdline.Replace("VarBestBefore", barcodeprintcollection[i].VarBestBefore);
                            }
                            else if (varName.ToLower().Equals("freetext1"))
                            {
                                rdline = rdline.Replace("VarFreeText1", barcodeprintcollection[i].VarFreeText1);
                            }
                            else if (varName.ToLower().Equals("freetext2"))
                            {
                                rdline = rdline.Replace("VarFreeText2", barcodeprintcollection[i].VarFreeText2);
                            }
                            else if (varName.ToLower().Equals("freetext3"))
                            {
                                rdline = rdline.Replace("VarFreeText3", barcodeprintcollection[i].VarFreeText3);
                            }
                            else if (varName.ToLower().Equals("freetext4"))
                            {
                                rdline = rdline.Replace("VarFreeText4", barcodeprintcollection[i].VarFreeText4);
                            }
                            else if (varName.ToLower().Equals("code"))
                            {
                                rdline = rdline.Replace("VarCode", barcodeprintcollection[i].VarCode);
                            }
                            else if (rdline.ToLower().IndexOf("varprint") > -1)
                            {
                                double tmp = Math.Ceiling(Convert.ToDouble(Convert.ToDouble(barcodeprintcollection[i].VarQuantity) / Convert.ToInt64(dBarCodeSetting.Rows[0].ItemArray[2].ToString())));
                                //tmp = Math.Floor(tmp);
                                //rdline = rdline.Replace("varPrint", barcodeprintcollection[i].VarQuantity.ToString());
                                rdline = rdline.Replace("VarPrint", tmp.ToString());
                            }
                            
                        }
                        strcollect.Add(rdline);
                    }
                }
                if (PrintType == BarcodePrinterType.TSC)
                    strcollect.Add(strLine[strLine.Length - 1]);
                string strTemp = "";
                if (PrintType == BarcodePrinterType.TSC)
                {
                    TSCLIB_DLL.openport(dBarCodeSetting.Rows[0].ItemArray[0].ToString());
                    for (int i = 0; i < strcollect.Count; i++)
                    {
                        strTemp += strcollect[i].ToString() + Environment.NewLine;
                        TSCLIB_DLL.sendcommand(strcollect[i].ToString());
                    }
                    TSCLIB_DLL.closeport();
                }
                else if (PrintType == BarcodePrinterType.Godex)
                {
                    Godex_DLL.openport(dBarCodeSetting.Rows[0].ItemArray[0].ToString());
                    Godex_DLL.beginjob(100, 12, 4, 0, 3, 0);
                    for (int i = 0; i < strcollect.Count; i++)
                    {
                        strTemp += strcollect[i].ToString() + Environment.NewLine;
                        Godex_DLL.sendcommand(strcollect[i].ToString());
                    }
                    Godex_DLL.endjob();
                    Godex_DLL.closeport();
                }
                else if (PrintType == BarcodePrinterType.Argox)
                {
                    int nLen, ret = 0, sw;
                    byte[] pbuf = new byte[128];
                    IntPtr ver;
                    System.Text.Encoding encAscII = System.Text.Encoding.ASCII;
                    System.Text.Encoding encUnicode = System.Text.Encoding.Unicode;
                    // dll version.
                    ver = ArgoxDLL.A_Get_DLL_Version(0);
                    // search port.
                    nLen = ArgoxDLL.A_GetUSBBufferLen() + 1;

                    if (nLen > 1)
                    {
                        byte[] buf1, buf2;
                        int len1 = 128, len2 = 128;
                        buf1 = new byte[len1];
                        buf2 = new byte[len2];
                        ArgoxDLL.A_EnumUSB(pbuf);
                        ArgoxDLL.A_GetUSBDeviceInfo(1, buf1, out len1, buf2, out len2);
                        sw = 1;
                        if (1 == sw)
                            ret = ArgoxDLL.A_CreatePrn(12, encAscII.GetString(buf2, 0, len2));// open usb.
                        else
                            ret = ArgoxDLL.A_CreateUSBPort(1);// must call A_GetUSBBufferLen() function fisrt.
                        if (0 != ret)
                        {
                        }
                        else
                        {
                            if (2 == sw)
                            {
                                //get printer status.
                                pbuf[0] = 0x01;
                                pbuf[1] = 0x46;
                                pbuf[2] = 0x0D;
                                pbuf[3] = 0x0A;
                                ArgoxDLL.A_WriteData(1, pbuf, 4);
                                ret = ArgoxDLL.A_ReadData(pbuf, 2, 1000);
                            }
                        }
                    }

                    if (0 != ret)
                        return false;


                    // sample setting.
                    ArgoxDLL.A_Set_DebugDialog(1);
                    ArgoxDLL.A_Set_Unit('n');
                    ArgoxDLL.A_Set_Syssetting(1, 0, 0, 0, 0);
                    ArgoxDLL.A_Set_Darkness(8);
                    ArgoxDLL.A_Del_Graphic(1, "*");// delete all picture.
                    ArgoxDLL.A_Clear_Memory();// clear memory.
                    strTemp = "";
                    for (int i = 0; i < strcollect.Count; i++)
                    {
                        strTemp += strcollect[i].ToString() +Environment.NewLine;
                        
                    }
                    //ArgoxDLL.A_WriteData(0, encAscII.GetBytes(strcollect[i].ToString()), strcollect[i].ToString().Length);
                    ArgoxDLL.A_WriteData(0, encAscII.GetBytes(strTemp), strTemp.Length);
                    // output.
                    ArgoxDLL.A_Print_Out(1, 1, 1, 1);// copy 2.

                    // close port.
                    ArgoxDLL.A_ClosePrn();
                }
                return true;
            }
            catch (Exception e)
            {
                OMMessageBox.Show(e.Message, CommonFunctions.ErrorTitle);
                return false;
            }
        }
    }

    /// <summary>
    /// This Class use for BarCodePrint
    /// </summary>
    public class BarCodePrint
    {
        private string mVarBarCode;
        private string mVarFirmName;
        private string mVarMRP;
        private string mVarRate;
        private string mVarWeight;
        private string mVarBrand;
        private string mVarShortDesc;
        private string mVarPackedDate;
        private string mVarBestBefore;
        private string mVarFreeText1;
        private string mVarFreeText2;
        private string mVarFreeText3;
        private string mVarFreeText4;
        private long mVarQuantity;
        private string mVarCode;
        /// <summary>
        /// This Properties use for VarBarCode
        /// </summary>
        /// 
        public string VarCode
        {
            get { return mVarCode; }
            set { mVarCode = value; }
        }

        public string VarBarCode
        {
            get { return mVarBarCode; }
            set { mVarBarCode = value; }
        }
        /// <summary>
        /// This Properties use for VarFirmName
        /// </summary>
        public string VarFirmName
        {
            get { return mVarFirmName; }
            set { mVarFirmName = value; }
        }
        /// <summary>
        /// This Properties use for VarMRP
        /// </summary>
        public string VarMRP
        {
            get { return mVarMRP; }
            set { mVarMRP = value; }
        }
        /// <summary>
        /// This Properties use for VarRate
        /// </summary>
        public string VarRate
        {
            get { return mVarRate; }
            set { mVarRate = value; }
        }
        /// <summary>
        /// This Properties use for VarWeight
        /// </summary>
        public string VarWeight
        {
            get { return mVarWeight; }
            set { mVarWeight = value; }
        }
        /// <summary>
        /// This Properties use for VarBrand
        /// </summary>
        public string VarBrand
        {
            get { return mVarBrand; }
            set { mVarBrand = value; }
        }
        /// <summary>
        /// This Properties use for VarShortDesc
        /// </summary>
        public string VarShortDesc
        {
            get { return mVarShortDesc; }
            set { mVarShortDesc = value; }
        }
        /// <summary>
        /// This Properties use for VarPackedDate
        /// </summary>
        public string VarPackedDate
        {
            get { return mVarPackedDate; }
            set { mVarPackedDate = value; }
        }
        /// <summary>
        /// This Properties use for VarBestBefore
        /// </summary>
        public string VarBestBefore
        {
            get { return mVarBestBefore; }
            set { mVarBestBefore = value; }
        }
        /// <summary>
        /// This Properties use for VarFreeText1
        /// </summary>
        public string VarFreeText1
        {
            get { return mVarFreeText1; }
            set { mVarFreeText1 = value; }
        }
        /// <summary>
        /// This Properties use for VarFreeText2
        /// </summary>
        public string VarFreeText2
        {
            get { return mVarFreeText2; }
            set { mVarFreeText2 = value; }
        }
        /// <summary>
        /// This Properties use for VarFreeText3
        /// </summary>
        public string VarFreeText3
        {
            get { return mVarFreeText3; }
            set { mVarFreeText3 = value; }
        }
        /// <summary>
        /// This Properties use for VarFreeText4
        /// </summary>
        public string VarFreeText4
        {
            get { return mVarFreeText4; }
            set { mVarFreeText4 = value; }
        }
        /// <summary>
        /// This Properties use for VarQuantity
        /// </summary>
        public long VarQuantity
        {
            get { return mVarQuantity; }
            set { mVarQuantity = value; }
        }
    }

    public class BarCodePrintCollection : System.Collections.CollectionBase
    {

        public BarCodePrint this[int index]
        {
            get
            {
                return ((BarCodePrint)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }

        public int Add(BarCodePrint value)
        {
            return (List.Add(value));
        }

        public int IndexOf(BarCodePrint value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, BarCodePrint value)
        {
            List.Insert(index, value);
        }

        public void Remove(BarCodePrint value)
        {
            List.Remove(value);
        }

        public bool Contains(BarCodePrint value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }

        protected override void OnValidate(Object value)
        {
            if (value.GetType() != typeof(BarCodePrint))
                throw new ArgumentException("value must be of type PrintBarCode.", "value");
        }

    }

}
