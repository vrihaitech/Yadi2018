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
    public partial class KeyBoard : Form
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        Transaction.QueryOutPut ObjQry = new Transaction.QueryOutPut();
        public DialogResult DS = DialogResult.OK;
        public string strLanguage = "", strLanguageFull = "";
        string sql = "";
        string strEnglish = "", strEnglishFull = "";
        int iActionType = 0;
        long mLanguageDictionary_PkSrNo = 0;
        public const int changeText = 1, AddToDictionary = 2, UpdateDictionary = 3, autoCheckText = 4;
        public int LangVal = 0;
        bool flagMain = true;
        public KeyBoard()
        {
            InitializeComponent();
        }

        public KeyBoard(int iActionType, string strEnglish, string strLanguage, string strEnglishFull, string strLanguageFull)
        {
            InitializeComponent();
            try
            {
                this.iActionType = iActionType;
                this.strEnglish = strEnglish;
                this.strLanguage = strLanguage;
                this.strEnglishFull = strEnglishFull;
                this.strLanguageFull = strLanguageFull;

                lblEnglishText.Text = strEnglish;
                txtAppend.Text = strLanguage;
                LangVal = Convert.ToInt32(ObjFunction.GetAppSettings(AppSettings.O_Language));
                if (iActionType == UpdateDictionary)
                {
                    sql = "Select PkSrNo, EnglishVal, MarathiVal, HindiVal,KarnatakaVal From MLanguageDictionary Where EnglishVal = '" + strEnglish.Replace("'", "''") + "'";
                    DataTable dt = ObjFunction.GetDataView(sql).Table;
                    mLanguageDictionary_PkSrNo = Convert.ToInt64(dt.Rows[0][0].ToString());
                    if (strLanguage.Trim().Length == 0)
                    {
                        txtAppend.Focus();
                        this.strLanguage = dt.Rows[0][LangVal].ToString();
                        txtAppend.Text = strLanguage;
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            flagMain = false;
            DS = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAppend.Text.Trim() != "")
                {
                    DBMLanguageDictionary dbmld = new DBMLanguageDictionary();
                    MLanguageDictionary mld = new MLanguageDictionary();

                    switch (iActionType)
                    {
                        case changeText:
                        case -1:
                            strLanguage = txtAppend.Text;
                            break;
                        case AddToDictionary:
                            mld.PkSrNo = ObjQry.ReturnLong("Select PKSrNo FRom MLanguageDictionary Where EnglishVal='" + strEnglish + "'", CommonFunctions.ConStr);
                            mld.EnglishVal = strEnglish;
                            mld.CompanyNo = DBGetVal.FirmNo;
                            if (LangVal == 2)
                            {
                                mld.MarathiVal = txtAppend.Text.Trim();
                                mld.HindiVal = "";
                                mld.KarnatakaVal = "";
                            }
                            else if (LangVal == 3)
                            {
                                mld.MarathiVal = "";
                                mld.KarnatakaVal = "";
                                mld.HindiVal = txtAppend.Text.Trim();
                            }
                            else if (LangVal == 4)
                            {
                                mld.MarathiVal = "";
                                mld.KarnatakaVal = txtAppend.Text.Trim();
                                mld.HindiVal = "";
                            }
                            if (dbmld.AddMLanguageDictionary(mld) == true)
                            {
                                //saved successfully
                            }
                            else
                            {
                                //error while saving
                            }
                            break;
                        case UpdateDictionary:
                            mld.PkSrNo = ObjQry.ReturnLong("Select PKSrNo FRom MLanguageDictionary Where EnglishVal='" + strEnglish + "'", CommonFunctions.ConStr);
                            mld.EnglishVal = strEnglish;
                            mld.CompanyNo = DBGetVal.FirmNo;
                            if (LangVal == 2)
                            {
                                mld.MarathiVal = txtAppend.Text.Trim();
                                mld.HindiVal = "";
                            }
                            else if (LangVal == 3)
                            {
                                mld.MarathiVal = "";
                                mld.HindiVal = txtAppend.Text.Trim();
                            }
                            if (dbmld.AddMLanguageDictionary(mld) == true)
                            {
                                //saved successfully
                            }
                            else
                            {
                                //error while saving
                            }
                            break;
                    }
                    DS = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //OMMessageBox.Show("Please enter value...", CommonFunctions.ErrorTitle, OMMessageBoxButton.OK, OMMessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void lButton_Click(object sender, EventArgs e)
        {
            string str = ((Button)sender).Text.Trim();
            int i = txtAppend.SelectionStart;
            txtAppend.Text = txtAppend.Text.Substring(0, i) + str + (txtAppend.Text.Length == i ? "" : txtAppend.Text.Substring(i, txtAppend.Text.Length - i));
            txtAppend.Focus();
            txtAppend.SelectionStart = i + 1;
            txtAppend.SelectionLength = 0;

        }

        private void button106_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAppend.Text.Trim().Length > 0)
                {
                    int i = txtAppend.SelectionStart;
                    if (i != 0)
                    {
                        if (txtAppend.SelectedText.Length == 0)
                        {
                            txtAppend.Text = txtAppend.Text.Substring(0, i - 1) + (txtAppend.Text.Length == i ? "" : txtAppend.Text.Substring(i, txtAppend.Text.Length - i));
                            txtAppend.Focus();
                            txtAppend.SelectionStart = i - 1;
                            txtAppend.SelectionLength = 0;
                        }
                        else
                        {
                            txtAppend.Text = txtAppend.Text.Substring(0, i) + (txtAppend.Text.Length == i ? "" : txtAppend.Text.Substring(i + txtAppend.SelectedText.Length, txtAppend.Text.Length - i - txtAppend.SelectedText.Length));
                            txtAppend.Focus();
                            txtAppend.SelectionStart = i;
                            txtAppend.SelectionLength = 0;
                        }
                    }
                    else
                    {
                        if (txtAppend.SelectedText.Length != 0)
                            txtAppend.Text = "";
                        txtAppend.Focus();
                    }
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void button107_Click(object sender, EventArgs e)
        {
            int i = txtAppend.SelectionStart;
            txtAppend.Text = txtAppend.Text.Substring(0, i) + " " + (txtAppend.Text.Length == i ? "" : txtAppend.Text.Substring(i, txtAppend.Text.Length - i));
            txtAppend.Focus();
            txtAppend.SelectionStart = i + 1;
            txtAppend.SelectionLength = 0;
        }

        private void KeyBoard_Load(object sender, EventArgs e)
        {

        }

        private void applyButtonFont()
        {
            try
            {
                this.txtAppend.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.button6.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button6.Text = "Q";

                this.button13.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button13.Text = "W";

                this.button14.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button14.Text = "E";

                this.button15.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button15.Text = " R";

                this.button16.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button16.Text = "T";

                this.button17.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button17.Text = "Y";

                this.button18.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button18.Text = " U";

                this.button19.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button19.Text = "I";

                this.button20.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button20.Text = " O";

                this.button21.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button21.Text = "P";

                this.button22.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button22.Text = "{";

                this.button23.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button23.Text = "}";

                this.button24.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button24.Text = "|";

                this.button25.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button25.Text = " \\";

                this.button26.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button26.Text = "]";

                this.button27.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button27.Text = "[";

                this.button28.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button28.Text = "p";
                this.button29.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button29.Text = " o";
                this.button30.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button30.Text = "i";
                this.button31.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button31.Text = " u";
                this.button32.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button32.Text = "y";
                this.button33.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button33.Text = "t";
                this.button34.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button34.Text = "r";
                this.button35.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button35.Text = "e";
                this.button36.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button36.Text = "w";
                this.button37.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button37.Text = "q";
                this.button40.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button40.Text = "\'";
                this.button41.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button41.Text = ";";
                this.button42.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button42.Text = "l";
                this.button43.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button43.Text = "k";
                this.button44.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button44.Text = "j";
                this.button45.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button45.Text = "h";
                this.button46.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button46.Text = "g";
                this.button47.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button47.Text = "f";
                this.button48.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button48.Text = "d";
                this.button49.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button49.Text = "s";
                this.button50.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button50.Text = "a";
                this.button53.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button53.Text = "\"";
                this.button54.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button54.Text = ":";
                this.button55.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button55.Text = "L";
                this.button56.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button56.Text = "K";
                this.button57.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button57.Text = "J";
                this.button58.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button58.Text = "H";
                this.button59.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button59.Text = "G";
                this.button60.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button60.Text = "F";
                this.button61.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button61.Text = "D";
                this.button62.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button62.Text = "S";
                this.button63.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button63.Text = "A";
                this.button39.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button39.Text = " /";
                this.button51.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button51.Text = ".";
                this.button52.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button52.Text = " ,";
                this.button64.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button64.Text = "m";
                this.button65.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button65.Text = "n";
                this.button66.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button66.Text = "b";
                this.button67.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button67.Text = "v";
                this.button68.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button68.Text = "c";
                this.button69.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button69.Text = "x";
                this.button70.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button70.Text = "z";
                this.button72.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button72.Text = "?";
                this.button73.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button73.Text = ">";
                this.button74.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button74.Text = "<";
                this.button75.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button75.Text = " M";
                this.button76.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button76.Text = "N";
                this.button77.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button77.Text = "B";
                this.button78.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button78.Text = "V";
                this.button79.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button79.Text = "C";
                this.button80.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button80.Text = "X";
                this.button81.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button81.Text = "Z";
                this.button38.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button38.Text = "=";
                this.button71.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button71.Text = " -";
                this.button82.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button82.Text = "0";
                this.button83.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button83.Text = "9";
                this.button84.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button84.Text = "8";
                this.button85.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button85.Text = "7";
                this.button86.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button86.Text = "6";
                this.button87.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button87.Text = "5";
                this.button88.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button88.Text = "4";
                this.button89.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button89.Text = "3";
                this.button90.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button90.Text = "2";
                this.button91.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button91.Text = "1";
                this.button92.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button92.Text = " `";
                this.button93.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button93.Text = "+";
                this.button94.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button94.Text = "_";
                this.button95.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button95.Text = ")";
                this.button96.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button96.Text = "(";
                this.button97.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button97.Text = "*";
                this.button98.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button98.Text = "&";
                this.button99.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button99.Text = " ^";
                this.button100.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button100.Text = "%";
                this.button101.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button101.Text = "$";
                this.button102.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button102.Text = "#";
                this.button103.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button103.Text = "@";
                this.button104.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button104.Text = "!";
                this.button105.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button105.Text = "~";
                this.button106.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button106.Text = "Backspace";
                this.button107.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button107.Text = " ";
                this.button1.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button1.Text = "À";
                this.button2.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button2.Text = "º";
                this.button3.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button3.Text = "µ";
                this.button4.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button4.Text = "´";
                this.button5.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button5.Text = "³";
                this.button7.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button7.Text = "²";
                this.button8.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button8.Text = "¥";
                this.button9.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button9.Text = "¤";
                this.button10.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button10.Text = "£";
                this.button11.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button11.Text = "¡";
                this.button12.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button12.Text = "Ë";
                this.button108.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button108.Text = "É";
                this.button109.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button109.Text = "È";
                this.button110.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button110.Text = "Ç";
                this.button111.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button111.Text = "Æ";
                this.button112.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button112.Text = "Å";
                this.button113.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button113.Text = "Ä";
                this.button114.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button114.Text = "Ã";
                this.button115.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button115.Text = "Â";
                this.button116.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button116.Text = "Á";
                this.button117.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button117.Text = "Ö";
                this.button118.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button118.Text = "Õ";
                this.button119.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button119.Text = "Ô";
                this.button120.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button120.Text = "Ó";
                this.button121.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button121.Text = "Ò";
                this.button122.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button122.Text = " Ð";
                this.button123.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button123.Text = "Ï";
                this.button124.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button124.Text = "Î";
                this.button125.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button125.Text = "Í";
                this.button126.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button126.Text = "Ì";
                this.button127.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button127.Text = "„";
                this.button128.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button128.Text = "”";
                this.button129.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button129.Text = "“";
                this.button130.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button130.Text = "‚";
                this.button131.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button131.Text = "’";
                this.button132.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button132.Text = "‘";
                this.button133.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button133.Text = "–";
                this.button134.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button134.Text = "š";
                this.button135.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button135.Text = "œ";
                this.button136.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button136.Text = "÷";
                this.button137.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button137.Text = "á";
                this.button138.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button138.Text = "Ý";
                this.button139.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button139.Text = "Ü";
                this.button140.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button140.Text = "Ù";
                this.button141.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button141.Text = "Ø";
                this.button142.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button142.Text = "…";
                this.button143.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button143.Text = "‰";
                this.label3.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //this.label3.Text = "marazI";
                if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 2)
                {

                    this.label3.Text = "marazI";
                }
                else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 3)
                {
                    this.label3.Text = "ihMdI";

                }
                this.lblLangFull.Font = new System.Drawing.Font("Shivaji01", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //this.lblLangFull.Text = "marazI";
                if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 2)
                {
                    this.lblLangFull.Text = "marazI";

                }
                else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 3)
                {

                    this.lblLangFull.Text = "ihMdI";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }
        private void applyButtonFontNudi()
        {
            try
            {
                this.txtAppend.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                this.button6.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button6.Text = "Q";

                this.button13.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button13.Text = "W";

                this.button14.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button14.Text = "E";

                this.button15.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button15.Text = " R";

                this.button16.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button16.Text = "T";

                this.button17.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button17.Text = "Y";

                this.button18.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button18.Text = " U";

                this.button19.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button19.Text = "I";

                this.button20.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button20.Text = " O";

                this.button21.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button21.Text = "P";

                this.button22.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button22.Text = "{";

                this.button23.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button23.Text = "}";

                this.button24.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button24.Text = "|";

                this.button25.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button25.Text = " \\";

                this.button26.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button26.Text = "]";

                this.button27.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button27.Text = "[";

                this.button28.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button28.Text = "p";
                this.button29.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button29.Text = " o";
                this.button30.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button30.Text = "i";
                this.button31.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button31.Text = " u";
                this.button32.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button32.Text = "y";
                this.button33.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button33.Text = "t";
                this.button34.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button34.Text = "r";
                this.button35.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button35.Text = "e";
                this.button36.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button36.Text = "w";
                this.button37.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button37.Text = "q";
                this.button40.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button40.Text = "\'";
                this.button41.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button41.Text = ";";
                this.button42.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button42.Text = "l";
                this.button43.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button43.Text = "k";
                this.button44.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button44.Text = "j";
                this.button45.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button45.Text = "h";
                this.button46.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button46.Text = "g";
                this.button47.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button47.Text = "f";
                this.button48.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button48.Text = "d";
                this.button49.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button49.Text = "s";
                this.button50.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button50.Text = "a";
                this.button53.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button53.Text = "\"";
                this.button54.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button54.Text = ":";
                this.button55.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button55.Text = "L";
                this.button56.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button56.Text = "K";
                this.button57.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button57.Text = "J";
                this.button58.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button58.Text = "H";
                this.button59.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button59.Text = "G";
                this.button60.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button60.Text = "F";
                this.button61.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button61.Text = "D";
                this.button62.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button62.Text = "S";
                this.button63.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button63.Text = "A";
                this.button39.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button39.Text = " /";
                this.button51.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button51.Text = ".";
                this.button52.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button52.Text = " ,";
                this.button64.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button64.Text = "m";
                this.button65.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button65.Text = "n";
                this.button66.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button66.Text = "b";
                this.button67.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button67.Text = "v";
                this.button68.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button68.Text = "c";
                this.button69.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button69.Text = "x";
                this.button70.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button70.Text = "z";
                this.button72.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button72.Text = "?";
                this.button73.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button73.Text = ">";
                this.button74.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button74.Text = "<";
                this.button75.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button75.Text = " M";
                this.button76.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button76.Text = "N";
                this.button77.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button77.Text = "B";
                this.button78.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button78.Text = "V";
                this.button79.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button79.Text = "C";
                this.button80.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button80.Text = "X";
                this.button81.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button81.Text = "Z";
                this.button38.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button38.Text = "=";
                this.button71.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button71.Text = " -";
                this.button82.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button82.Text = "0";
                this.button83.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button83.Text = "9";
                this.button84.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button84.Text = "8";
                this.button85.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button85.Text = "7";
                this.button86.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button86.Text = "6";
                this.button87.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button87.Text = "5";
                this.button88.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button88.Text = "4";
                this.button89.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button89.Text = "3";
                this.button90.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button90.Text = "2";
                this.button91.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button91.Text = "1";
                this.button92.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button92.Text = " `";
                this.button93.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button93.Text = "+";
                this.button94.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button94.Text = "_";
                this.button95.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button95.Text = ")";
                this.button96.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button96.Text = "(";
                this.button97.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button97.Text = "*";
                this.button98.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button98.Text = "&";
                this.button99.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button99.Text = " ^";
                this.button100.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button100.Text = "%";
                this.button101.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button101.Text = "$";
                this.button102.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button102.Text = "#";
                this.button103.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button103.Text = "@";
                this.button104.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button104.Text = "!";
                this.button105.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button105.Text = "~";
                this.button106.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button106.Text = "Backspace";
                this.button107.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button107.Text = " ";
                this.button1.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button1.Text = "À";
                this.button2.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button2.Text = "º";
                this.button3.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button3.Text = "µ";
                this.button4.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button4.Text = "´";
                this.button5.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button5.Text = "³";
                this.button7.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button7.Text = "²";
                this.button8.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button8.Text = "¥";
                this.button9.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button9.Text = "¤";
                this.button10.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button10.Text = "£";
                this.button11.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button11.Text = "¡";
                this.button12.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button12.Text = "Ë";
                this.button108.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button108.Text = "É";
                this.button109.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button109.Text = "È";
                this.button110.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button110.Text = "Ç";
                this.button111.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button111.Text = "Æ";
                this.button112.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button112.Text = "Å";
                this.button113.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button113.Text = "Ä";
                this.button114.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button114.Text = "Ã";
                this.button115.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button115.Text = "Â";
                this.button116.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button116.Text = "Á";
                this.button117.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button117.Text = "Ö";
                this.button118.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button118.Text = "Õ";
                this.button119.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button119.Text = "Ô";
                this.button120.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button120.Text = "Ó";
                this.button121.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button121.Text = "Ò";
                this.button122.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button122.Text = " Ð";
                this.button123.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button123.Text = "Ï";
                this.button124.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button124.Text = "Î";
                this.button125.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button125.Text = "Í";
                this.button126.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button126.Text = "Ì";
                this.button127.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button127.Text = "„";
                this.button128.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button128.Text = "”";
                this.button129.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button129.Text = "“";
                this.button130.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button130.Text = "‚";
                this.button131.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button131.Text = "’";
                this.button132.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button132.Text = "‘";
                this.button133.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button133.Text = "–";
                this.button134.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button134.Text = "š";
                this.button135.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button135.Text = "œ";
                this.button136.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button136.Text = "÷";
                this.button137.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button137.Text = "á";
                this.button138.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button138.Text = "Ý";
                this.button139.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button139.Text = "Ü";
                this.button140.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button140.Text = "Ù";
                this.button141.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button141.Text = "Ø";
                this.button142.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button142.Text = "…";
                this.button143.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.button143.Text = "‰";
                this.label3.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //this.label3.Text = "marazI";
                if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 2)
                {

                    this.label3.Text = "marazI";
                }
                else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 3)
                {
                    this.label3.Text = "ihMdI";

                }
                else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 4)
                {
                    this.label3.Text = "Nudi 01 e";

                }
                this.lblLangFull.Font = new System.Drawing.Font("Nudi 01 e", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //this.lblLangFull.Text = "marazI";
                if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 2)
                {
                    this.lblLangFull.Text = "marazI";

                }
                else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 3)
                {

                    this.lblLangFull.Text = "ihMdI";
                }
                else if (Convert.ToInt64(ObjFunction.GetAppSettings(AppSettings.O_Language)) == 4)
                {

                    this.lblLangFull.Text = "Nudi 01 e";
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void KeyBoard_Activated(object sender, EventArgs e)
        {
            try
            {
                if (flagMain == false) return;
                if (LangVal != 4)
                {
                    applyButtonFont();
                }
                else { applyButtonFontNudi(); }
                if (iActionType == AddToDictionary)
                {
                    if (strEnglishFull != null && strEnglishFull.Length > 0)
                    {
                        lblEngFull.Text = "English - " + strEnglishFull;
                        lblEngFull.Visible = true;
                    }
                    if (strLanguageFull != null && strLanguageFull.Length > 0)
                    {
                        if (LangVal == 2)
                            lblLangFull.Text = "marazI – " + strLanguageFull;
                        else if (LangVal == 3)
                            lblLangFull.Text = "ihMdI – " + strLanguageFull;
                        lblLangFull.Visible = true;
                    }
                }
                if (iActionType == autoCheckText)
                {
                    iActionType = -1;
                    btnAutoCheck_Click(btnAutoCheck, null);
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void btnAutoCheck_Click(object sender, EventArgs e)
        {
            try
            {
                string[] strSplit = { " ", "\r", "\n" };
                string[] strEng = strEnglish.Split(strSplit, StringSplitOptions.RemoveEmptyEntries);

                while (true)
                {
                    string strPending = "";
                    string strLang = "";
                    for (int i = 0; i < strEng.Length; i++)
                    {

                        sql = "Select PkSrNo, EnglishVal, MarathiVal, HindiVal,KarnatakaVal From MLanguageDictionary Where EnglishVal = '" + strEng[i].Replace("'", "''") + "'";
                        DataTable dt = ObjFunction.GetDataView(sql).Table;
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
                        KeyBoard frmkb = new KeyBoard(2, strPending, "", strEnglish, strLang);
                        ObjFunction.OpenForm(frmkb);
                        if (frmkb.DS == DialogResult.OK)
                        {
                            frmkb.Close();
                        }
                        else if (frmkb.DS == DialogResult.Cancel)
                        {
                            break;
                        }
                    }
                    else
                    {
                        txtAppend.Text = strLang.Trim();
                        break;
                    }
                }
                if (iActionType == -1)
                {
                    btnOk_Click(btnOk, null);
                }
                else
                {
                    txtAppend.Focus();
                    txtAppend.SelectAll();
                }
            }
            catch (Exception exc)
            {
                ObjFunction.ExceptionDisplay(exc.Message);
            }
        }

        private void txtAppend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnOk.Focus();
            }
        }

        private void KeyBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnAutoCheck_Click(sender, new EventArgs());
            }
        }
    }
}
