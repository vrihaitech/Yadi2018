using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Excel = Microsoft.Office.Interop.Excel;
using System.ComponentModel;
using OMControls;

namespace OM
{
    class DBExcel 
    {
        public DataSet ReadExcelFile(string FileName)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=Excel 8.0");
            
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                DataTable dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt.Rows.Count > 0)
                {
                    String[] excelSheets = new String[dt.Rows.Count];
                    int i = 0;

                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[i] = row["TABLE_NAME"].ToString();
                        i++;
                    }
                    OleDbDataAdapter da;

                    for (int j = 0; j < excelSheets.Length; j++)
                    {
                        da = new OleDbDataAdapter(" SELECT * FROM [" + excelSheets[j] + "]", con);
                        da.Fill(ds, excelSheets[j]);
                    }
                }

                return ds;
            }
            catch (Exception e1)
            {
                CommonFunctions.ErrorMessge = e1.Message;
                throw;
            }
            finally
            {
                con.Close();
            }
        }


    }

    class CreateExcel
    {
        private Excel.Application app = null;
        private Excel.Workbook workbook = null;
        private Excel.Worksheet worksheet = null;
        private Excel.Range workSheet_range = null;
        public CreateExcel()
        {
            createDoc();
        }
        public void createDoc()
        {
            try
            {


                app = new Excel.Application();
                app.Visible = true;
                workbook = app.Workbooks.Add(1);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];
                

            }
            catch (Exception e)
            {
                Console.Write("Error");
                CommonFunctions.ErrorMessge = e.Message;
            }
            finally
            {

            }
        }

        public string GetData(int Srow, int Scol, int Erow, int Ecol)
        {
            Excel.Range rng = worksheet.get_Range(worksheet.Cells[Srow, Scol], worksheet.Cells[Erow, Ecol]);
            return (rng.Value2 == null) ? "" : rng.Value2.ToString();
        }

        public void CompleteDoc(string FileName)
        {
            app.Visible = true;
            
            //workbook.SaveCopyAs("d:\\" + FileName + ".xlsx");
            //workbook.SaveAs("d:\\" + FileName + ".xlsx",
            //Excel.XlFileFormat.xlWorkbookNormal, System.Reflection.Missing.Value, System.Reflection.Missing.Value,
            //false, false, Excel.XlSaveAsAccessMode.xlNoChange,
            //System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
        }

        public void createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, System.Drawing.Color backcolor, bool fontbold, int size, System.Drawing.Color fcolor, int fontsize, ExAlign HAlignment)
        {
            workSheet_range = worksheet.get_Range(cell1, cell2);// (Excel.Range)worksheet.Cells[row, col]; // worksheet.get_Range(cell1, cell2);
            workSheet_range.NumberFormat = "@";
            worksheet.Cells[row, col] = htext;
            workSheet_range.Merge(mergeColumns);  
           
            workSheet_range.Interior.Color = backcolor.ToArgb();
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            
            workSheet_range.Font.Bold = fontbold;
            workSheet_range.Font.Size = fontsize;
            workSheet_range.ColumnWidth = size;
            workSheet_range.HorizontalAlignment = HorizontalAlignment(HAlignment);
            //workSheet_range.Columns.AutoFit();
            workSheet_range.Font.Color = fcolor.ToArgb();
        }

        public void createHeaders(string data, int Srow, int Scol, int Erow, int Ecol, System.Drawing.Color backcolor, bool fontbold, int size, System.Drawing.Color fcolor, int fontsize, ExAlign HAlignment)
        {
            worksheet.Cells[Srow, Scol] = data;

            workSheet_range = worksheet.get_Range(worksheet.Cells[Srow, Scol], worksheet.Cells[Erow, Ecol]);// (Excel.Range)worksheet.Cells[row, col]; // worksheet.get_Range(cell1, cell2);
            workSheet_range.MergeCells = true;
            workSheet_range.Merge(false);
            workSheet_range.HorizontalAlignment = HorizontalAlignment(HAlignment);
            workSheet_range.Interior.Color = backcolor.ToArgb();
            workSheet_range.Font.Bold = fontbold;
            workSheet_range.Font.Size = fontsize;
            workSheet_range.ColumnWidth = size;
            workSheet_range.Font.Color = fcolor.ToArgb();
            //workSheet_range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexNone, 0);
            //workSheet_range.WrapText = false;
        }

        public void createSubHeaders(string data, int Srow, int Scol, int Erow, int Ecol, System.Drawing.Color backcolor, bool fontbold, int size, System.Drawing.Color fcolor, int fontsize, ExAlign HAlignment)
        {
            worksheet.Cells[Srow, Scol] = data;

            workSheet_range = worksheet.get_Range(worksheet.Cells[Srow, Scol], worksheet.Cells[Erow, Ecol]);// (Excel.Range)worksheet.Cells[row, col]; // worksheet.get_Range(cell1, cell2);
            workSheet_range.MergeCells = true;
            workSheet_range.Merge(false);
            workSheet_range.HorizontalAlignment = HorizontalAlignment(HAlignment);
            workSheet_range.Interior.Color = backcolor.ToArgb();
            workSheet_range.Font.Bold = fontbold;
            workSheet_range.Font.Size = fontsize;
            if (size != 0) workSheet_range.ColumnWidth = size;
            workSheet_range.Font.Color = fcolor.ToArgb();
            workSheet_range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexNone, 1);
            //workSheet_range.WrapText = false;
        }

        public void createSubHeadersWB(string data, int Srow, int Scol, int Erow, int Ecol, System.Drawing.Color backcolor, bool fontbold, int size, System.Drawing.Color fcolor, int fontsize, ExAlign HAlignment)
        {
            worksheet.Cells[Srow, Scol] = data;

            workSheet_range = worksheet.get_Range(worksheet.Cells[Srow, Scol], worksheet.Cells[Erow, Ecol]);// (Excel.Range)worksheet.Cells[row, col]; // worksheet.get_Range(cell1, cell2);
            workSheet_range.MergeCells = true;
            workSheet_range.Merge(false);
            workSheet_range.HorizontalAlignment = HorizontalAlignment(HAlignment);
            workSheet_range.Interior.Color = backcolor.ToArgb();
            workSheet_range.Font.Bold = fontbold;
            workSheet_range.Font.Size = fontsize;
            if (size != 0) workSheet_range.ColumnWidth = size;
            workSheet_range.Font.Color = fcolor.ToArgb();
            //workSheet_range.WrapText = false;
        }

        public void addData(int row, int col, string data, string cell1, string cell2, string format, int ctype, ExAlign HAlignment, bool font)
        {
            worksheet.Cells[row, col] = data;
            workSheet_range = (Excel.Range)worksheet.Cells[row, col];// worksheet.get_Range(cell1, cell2);
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.NumberFormat = format;
            if (format == Format.DDMMMYYYY) workSheet_range.Columns.AutoFit();
            workSheet_range.HorizontalAlignment = HorizontalAlignment(HAlignment);
            if (ctype == 1) workSheet_range.Interior.Color = System.Drawing.Color.PeachPuff.ToArgb();
            workSheet_range.Font.Bold = font;
            //workSheet_range.Orientation = 90;    
        }

        public void addData(int row, int col, string data, string cell1, string cell2, string format, int ctype, ExAlign HAlignment, bool font,string FontName,int FontSize)
        {
            worksheet.Cells[row, col] = data;
            workSheet_range = (Excel.Range)worksheet.Cells[row, col];// worksheet.get_Range(cell1, cell2);
            workSheet_range.Borders.Color = System.Drawing.Color.Black.ToArgb();
            workSheet_range.NumberFormat = format;
            if (format == Format.DDMMMYYYY) workSheet_range.Columns.AutoFit();
            workSheet_range.HorizontalAlignment = HorizontalAlignment(HAlignment);
            if (ctype == 1) workSheet_range.Interior.Color = System.Drawing.Color.PeachPuff.ToArgb();
            workSheet_range.Font.Bold = font;
            workSheet_range.Font.Name = FontName;
            workSheet_range.Font.Size = FontSize;
        }

        public Excel.XlHAlign HorizontalAlignment(ExAlign exalign)
        {
            Excel.XlHAlign EX = new Microsoft.Office.Interop.Excel.XlHAlign();
            switch (exalign)
            {
                case ExAlign.Left: EX = Excel.XlHAlign.xlHAlignLeft;
                    break;
                case ExAlign.Right: EX = Excel.XlHAlign.xlHAlignRight;
                    break;
                case ExAlign.Justify: EX = Excel.XlHAlign.xlHAlignJustify;
                    break;
                case ExAlign.Center: EX = Excel.XlHAlign.xlHAlignCenter;
                    break;
                case ExAlign.Distributed: EX = Excel.XlHAlign.xlHAlignDistributed;
                    break;
                case ExAlign.General: EX = Excel.XlHAlign.xlHAlignGeneral;
                    break;
                case ExAlign.Fill: EX = Excel.XlHAlign.xlHAlignFill;
                    break;
                case ExAlign.CenterAcrossSelection: EX = Excel.XlHAlign.xlHAlignCenterAcrossSelection;
                    break;
            }
            return EX;
        }

        public enum ExAlign
        {
            Right = 1,
            Left = 2,
            Justify = 3,
            Distributed = 4,
            Center = 5,
            General = 6,
            Fill = 7,
            CenterAcrossSelection = 8,
        }

        public string ColName(int row, int col)
        {
            //col = col + 64;
            //char ch = (char)col;
            //return ch.ToString() + row;
            col = col + 64;

            if (col > 90)
            {
                int A = col - 66;
                string result = string.Empty;
                while (--A >= 0)
                {
                    result = (char)('A' + A % 26) + result;
                    A /= 26;
                }
                return result + row;
            }
            else
            {
                char ch = (char)col;
                return ch.ToString() + row;
            }
        }
        
    }


}
