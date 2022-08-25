using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Threading;
using System.Data;
using Indocosmo.Framework.ExceptionManagement;

namespace NutritionV1
{
    /// <summary>
    /// CLASS FOR EXPORTING DATA TO EXCEL/ FOR CREATNG EXCEL REPORTS
    ///	THIS CLASS IS INDENTED FOR EXPORTING DATA TO EXCEL WORKSHEET.THE CLASS CONTAINS METHODS FOR OPENING EXISTING WORKSHEET 
    /// SETTING THE CURRENT WORKSHEET ,PUTTING DATA ON TO A SPECIFIED CELL BY SPECIFYING THE ROW NO AND COL NO.THE CLASS ALSO 
    /// CONTAINS METHODS FOR DELETING UNWANTED ROWS.
    /// </summary>
    public class ExcelOutput :IDisposable
    {
        //Excel.Application excel = null;
        //Excel.Workbooks books = null;
        //Excel.Workbook book = null;
        //Excel.Sheets sheets = null;
        //Excel.Worksheet sheet = null;
        //Excel.Range cells = null;
        Object missing = System.Reflection.Missing.Value;
        int currentWorkSheet = 0;
        bool disposed; //Indicates if object has been disposed

        public ExcelOutput()
        {
            currentWorkSheet = 0;
            disposed = false;
        }


        /// <summary>
        /// Create Excel for Controll Budget Document
        /// </summary>
        /// <param name="reportName">Report Name</param>
        /// <param name="workPath">Path to save escel</param>
        /// <param name="workNumber">unique Worknumber</param>        
        /// <returns></returns>
        public bool CreateExcel(string reportName, string workPath, string workNumber)
        {
            bool sucess = false;
            string file, template;
            try
            {
            
                    file = workPath + "\\" + reportName + workNumber + ".xls";            
            
                    template = Convert.ToString(ConfigurationManager.AppSettings["REPORT_TEMPLATE_PATH"]) + reportName + ".xls";

                if (!File.Exists(file))
                {
                    File.Copy(template, file,true);                    
                }
                try
                {
                    OpenExcel(file, true);
                    sucess = true;
                }
                catch (IOException exc)
                {
                    sucess = false;
                    throw exc;
                }

                //book = books.get_Item(1);
                //sheets = book.Worksheets;
                SetSheet(1);

                return sucess;
            }
            catch (Exception ex)
            {
                Close();
                ExceptionManager.Publish(ex);            
                return sucess;
            }
        }

        /// <summary>
        /// Create Excel For Work Report
        /// </summary>
        /// <param name="reportName"> report Name</param>
        /// <param name="workPath"> Path To save Report  </param>
        /// <param name="workNumber">Work Number </param>
        /// <param name="isKeyFile"> is this keyfile or not </param>
        /// <returns></returns>
        public bool CreateExcel(string reportName, string workPath, string workNumber, bool isKeyFile)
        {
            bool sucess = false;
            string file, template;
            try
            {
                if (isKeyFile)
                    file = workPath + "\\KeyFile.xls";
                else
                    file = workPath + "\\" + reportName + workNumber + ".xls";


                if (isKeyFile)
                    template = Convert.ToString(ConfigurationManager.AppSettings["REPORT_TEMPLATE_PATH"]) + "keyFile.xls";
                else
                    template = Convert.ToString(ConfigurationManager.AppSettings["REPORT_TEMPLATE_PATH"]) + reportName + ".xls";

                if (!File.Exists(file))
                {
                    File.Copy(template, file, true);
                    if (isKeyFile)
                    {
                        OpenExcel(file, false);
                        sucess = true;
                    }
                }
                try
                {
                    if (!isKeyFile)
                    {
                        OpenExcel(file, true);
                        sucess = true;
                    }
                    

                }
                catch (IOException exc)
                {
                    throw exc;
                }

                //book = books.get_Item(1);
                //sheets = book.Worksheets;
                SetSheet(1);
                return sucess;
            }
            catch (Exception ex)
            {
                Close();
                ExceptionManager.Publish(ex);            
                return sucess;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reportName"></param>
        /// <returns></returns>
        public bool CreateExcel(string reportName, string filePath)
        {
            string file, template;
            try
            {
                file = filePath; //AppDomain.CurrentDomain.BaseDirectory + @"\Reports\CC(" + DateTime.Now.ToString("dd-MM-yy hh-mm-ss tt") + ").xls";
                file = file.Replace(@"\\", @"\");
                
                template = AppDomain.CurrentDomain.BaseDirectory + @"\Templates\" + reportName + ".xls";
                template = template.Replace(@"\\", @"\");

                //template = Convert.ToString(ConfigurationManager.AppSettings["REPORT_TEMPLATE_PATH"]) + reportName + ".xls";
                //2009-0613
                //if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"\Reports\"))                
                //{
                //    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Reports\");
                //}
                //---------------


                File.Delete(file);  // used to check whether the file is open or not.if so ,generates an exception and return false.
                File.Copy(template, file, true);

                OpenExcel(file,true);

                //book = books.get_Item(1);
                //sheets = book.Worksheets;
                SetSheet(1);
                return true;
            }
            catch(Exception ex)
            {
                Close();
                ExceptionManager.Publish(ex);            
                return false;
            }
        }



        public void OpenExcel(string file,bool isVisible)
        {
            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            
            //---2008/10/15 Start            
            //ExcelSingleton.ExcelClass ex1 = ExcelSingleton.ExcelClass.Instance;
            //excel = ex1.ExcelInstance; 
            //---END 

            //excel = new Excel.Application();
            
            //books= excel.Workbooks;
            ////Excel 2000
            //books.Open(file, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing);
            ////Excel 2003
            ////books.Open(file, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);
            //excel.Visible = isVisible;
            System.Threading.Thread.CurrentThread.CurrentCulture = CurrentCI;
        }

        public void PrintExcel()
        {
            //book.PrintOut(1, 1, 1, false, missing, true, false, missing);
        }

        public void SetSheet(int sheetNumber)
        {
            try
            {
                //sheet = null;
                //sheet = (Excel.Worksheet)sheets.get_Item(sheetNumber);
                //cells = sheet.Cells;
                currentWorkSheet = sheetNumber;
            }
            catch
            {
                Close();
            }
        }

        public void CopySheet(int sheetNumber)
        {
            try
            {
                //sheet = null;
                //sheet = (Excel.Worksheet)sheets.get_Item(sheetNumber);                
                //sheet.Copy(missing, sheet);
            }
            catch
            {
                Close();
            }
        }

		/// <summary>
		/// Copy from the sheet "sheetNumber" to just before the sheet
		/// and give the given name
		/// </summary>
		/// <param name="sheetNumber"></param>
		/// <param name="name"></param>
		public void CopySheet(int sheetNumber, string name)
		{
			try
			{
				//sheet = null;
				//sheet = (Excel.Worksheet)sheets.get_Item(sheetNumber);
				//sheet.Copy(sheet, missing);
				//sheet = (Excel.Worksheet)sheets.get_Item(sheets.Count-1);
				//sheet.Name = name;
			}
			catch
			{
				Close();
			}
		}
        
        /// <summary>
        /// Copy Sheet to End of current Sheet
        /// </summary>
        public void CopySheet()
        {
            try
            {
                //sheet = null;
                //sheet = (Excel.Worksheet)sheets.get_Item(sheets.Count);                    
                //sheet.Copy(missing, sheet);
            }
            catch
            {
                Close();
            }


        }

        public void DeleteSheet(int sheetNumber)
        {
            try
            {
                //sheet = null;
                //sheet = (Excel.Worksheet)sheets.get_Item(sheetNumber);
                //sheet.Delete();
                //sheet = null;
            }
            catch
            {
                Close();
            }
        }

        public bool IsSheetExists(string sheetName)
        {
            bool isExist = false;
            //foreach (Excel.Worksheet sh in sheets)
            //{
            //    if (sh.Name == sheetName)
            //    {
            //        isExist = true;
            //        break;
            //    }
            //}
            return isExist;
        }

        public void SetCellValue(int row, int col, int value)
        {
            //cells.Cells[row, col] = value;
        }

        public void SetCellValue(int row, int col, double value)
        {
            //cells.Cells[row, col] = value;
        }

        public void SetCellValue(int row, int col, DateTime value)
        {
            //cells.Cells[row, col] = value;

        }

        public void SetCellValue(int row, int col, string value)
        {
            if (value == null)
                return;
            else if (value.Trim() == "")
                return;
            //cells.Cells[row, col] = value;
        }

        public void SetCellValue(string range, string value)
        {
            SetCellValue(range, range, value);
        }

        public void SetCellValue(string startCell, string endCell, string value)
        {
            if (value == null)
                return;
            if (value.Trim() == "")
                return;
            //sheet.get_Range(startCell, endCell).Value2 = value;
        }

        public void MergeCells(int row, int startCol, int endCol)
        {
            //Excel.Range range = sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]);
            //range.Merge(missing);
        }

        public bool InsertRows(int insertRowAt)
        {
            return InsertRows(insertRowAt, 0);
        }

        public bool InsertRows(int insertRowAt, int noOfRows)
        {
            try
            {
               // Excel 2000
                //sheet.get_Range(insertRowAt + ":" + (insertRowAt + noOfRows), missing).Insert(missing);
                //Excel 2003
                //sheet.get_Range(insertRowAt + ":" + (insertRowAt + noOfRows), missing).Insert(missing, missing);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CellClear(int row, int col)
        {
            //cells.Cells[row, col] = "";
        }

        public bool CopyCell(int fromcellrow, int fromcellcol, int tocellrow, int tocellcol)
        {
            try
            {
                //Excel.Range range = sheet.get_Range(sheet.Cells[fromcellrow, fromcellcol], sheet.Cells[fromcellrow, fromcellcol]);
                //Excel.Range range1 = sheet.get_Range(sheet.Cells[tocellrow, tocellcol], sheet.Cells[tocellrow, tocellcol]);
                //range.Copy(range1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CutCell(int fromcellrow, int fromcellcol, int tocellrow, int tocellcol)
        {
            try
            {
                //Excel.Range range = sheet.get_Range(sheet.Cells[fromcellrow, fromcellcol], sheet.Cells[fromcellrow, fromcellcol]);
                //Excel.Range range1 = sheet.get_Range(sheet.Cells[tocellrow, tocellcol], sheet.Cells[tocellrow, tocellcol]);
                //range.Cut(range1);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CopyRow(int row, int startCol, int endCol)
        {
            //Excel.Range range = sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]);
            //Excel.Range range1 = sheet.get_Range(sheet.Cells[row + 1, startCol], sheet.Cells[row + 1, endCol]);
            //range.Copy(range1);
            

        }
        public void CopyColumn(int column, int rowFrom, int rowTo, int sheetNum,int destColumn,int destRowFrom,int destRowto)
        {
            //Excel.Range range = sheet.get_Range(sheet.Cells[rowFrom, column], sheet.Cells[rowTo, column]);
            //SetSheet(sheetNum);
            //Excel.Range rangeDest = sheet.get_Range(sheet.Cells[destRowFrom, destColumn], sheet.Cells[destRowto, destColumn]);
            //range.Copy(rangeDest);
        
        }
        /*public bool IsExistSheet(string sheetName)
        {
            foreach (Excel.Worksheet sheet in sheets)
            {
                if (sheet.Name == sheetName)
                    return true;
                else
                    return false;
            }
            return false;
        }*/
        /// <summary>
        /// insert a sheet to the existing template.
        /// </summary>
        /// <param name="sheetName">Name of the sheet</param>
        /// <param name="number">where it should insert in the workbook</param>
        public void InsertSheet(string sheetName,int number)
        {

            if (IsSheetExists(sheetName) != true)
            {
                //book.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                //sheet = (Excel.Worksheet)sheets.get_Item(number);
                //sheet.Name = sheetName;

            }
        }


        public void CopyRow(int rowFrom, int rowTo, int startCol, int endCol)
        {
            //Excel.Range range = sheet.get_Range(sheet.Cells[rowFrom, startCol], sheet.Cells[rowFrom, endCol]);
            //Excel.Range range1 = sheet.get_Range(sheet.Cells[rowTo, startCol], sheet.Cells[rowTo, endCol]);
            //range.Copy(range1);
           
        }
        public void HideRows(int startRow, int endRow)
        {
            //sheet.get_Range(startRow + ":" + endRow, missing).EntireRow.Hidden = true;
        }

        public bool DeleteRows(int row)
        {
            return DeleteRows(row, row);
        }

        public bool DeleteRows(int startRow, int endRow)
        {
            try
            {
                //sheet.get_Range(startRow + ":" + endRow, missing).Delete(0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteRows(int startRow, int endRow, int sheetNo)
        {
            int previousWorkSheet = this.currentWorkSheet;
            try
            {
                this.SetSheet(sheetNo);
                this.DeleteRows(startRow, endRow);
                this.SetSheet(previousWorkSheet);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Save()
        {
            try
            {
                //book.Save();
                //book.Close(missing, missing, missing);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            finally
            {
                Close();
            }
        }

        public void Save(string savePath, string password)
        {
            try
            {
                //sheet = (Excel.Worksheet)sheets.get_Item(1);
                ////Excel 2000
                ////sheet.Protect(password, true, true, true, missing);
                //book.SaveAs(savePath, Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing);
                ////Excel 2003
                ////sheet.Protect(password, true, true, true, missing, false, false, false, false, false, false, false, false, false, false, false);
                ////book.SaveAs(savePath, Excel.XlFileFormat.xlWorkbookNormal, missing, missing, missing, missing, Excel.XlSaveAsAccessMode.xlNoChange, missing, missing, missing, missing, missing);
                ////book.Close(missing, missing, missing);
                ////Close();
            }
            catch (Exception exception)
            {
                Close();
                throw new Exception(exception.Message.ToString());
            }

        }

        private void Close()
        {
            try
            {
               // if (excel != null) //2008/10/15 To avoid Object reference error
               // {
               //     if (excel.Workbooks != null)
               //     {
               //         foreach (Excel.Workbook wb in excel.Workbooks)
               //         {
               //             foreach (Excel.Worksheet ws in wb.Worksheets)
               //             {
               //                 System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
               //             }
               //             wb.Close(false, false, missing);
               //             System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
               //         }
               //         excel.Workbooks.Close();
               //     }
               //     excel.DisplayAlerts = false;
               //     excel.Quit();
               //}
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message.ToString());
            }
            finally
            {
                //if (cells != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(cells);
                //if (sheet != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                //if (sheets != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                //if (book != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                //if (books != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(books);
                //if (excel != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
                //cells = null;
                //sheet = null; sheets = null;
                //book = null; books = null;
                //excel = null;
                System.GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        public void FillCellColor(int row, int startCol, int endCol)
        {
            //sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Interior.Color = 0x008b0000;//0x00BEBEBE; 
        }

		public void FillCellColor(int row, int startCol, int endCol,System.Drawing.Color color)
		{
			//sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Interior.Color = System.Drawing.ColorTranslator.ToOle(color);
			; //0x00BEBEBE; 
		}

		public void FillCellForeColor(int row, int startCol, int endCol)
		{
			//sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Font.Color = sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Interior.Color;
			; //0x00BEBEBE; 
		}

        public void ApplyBorderStyle(int row, int startCol, int endCol, int status)
        {
            //switch (status)
            //{
            //    case 1:
            //        sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        break;
            //    case 2:
            //        sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        break;
            //    case 3:
            //        sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        break;
            //    case 4:
            //        sheet.get_Range(sheet.Cells[row, startCol], sheet.Cells[row, endCol]).Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        break;
            //}
        }

        public void DrawLine(string from, string to, int side)
        {
            //Excel.Range rg = sheet.get_Range(from, to);
            //switch (side)
            //{
            //    //Left edge
            //    case 1:
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;
            //        break;
            //    //Right edge
            //    case 2:
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;
            //        break;
            //    //Top edge
            //    case 3:
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;
            //        break;
            //    //Bottom edge
            //    case 4:
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
            //        rg.Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;
            //        break;
            //}
        }

        ~ExcelOutput()
        {
            //Close();
            //GC.Collect();
            Dispose(false);
        }

        /// <summary>
        /// Release all object from Com Objects
        /// </summary>
        public void Release()
        {
            try
            {
                //if (excel != null) //2008/10/15 To avoid Object reference error
                //{
                //    if (excel.Workbooks != null)
                //    {
                //        foreach (Excel.Workbook wb in excel.Workbooks)
                //        {
                //            foreach (Excel.Worksheet ws in wb.Worksheets)
                //            {
                //                System.Runtime.InteropServices.Marshal.ReleaseComObject(ws);
                //            }
                //            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
                //        }
                //    }
                //    excel.DisplayAlerts = false;

                //}
            }
            catch (Exception exception)
            {
                ExceptionManager.Publish(exception);
            }
            finally
            {
                //if (cells != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(cells);
                //if (sheet != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                //if (sheets != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                //if (book != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                //if (books != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(books);
                //if (excel != null)
                //    System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            //Prevent the GC to call Finalize again, since you have already

            //cleaned up.

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //Make sure Dispose does not get called more than once,

            //by checking the disposed field

            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        //Clean up managed resources

                    }
                    //Now clean up unmanaged resources
                    Release();
                }
                disposed = true;
            }
            finally
            {
                //base.Dispose(disposing);
            }
        }

        #region Properties

        public int NoOfSheets
        {
            get
            {
                //if (sheets != null)
                //    return sheets.Count;
                //else
                    return 1;
            }
        }

        #endregion


    }
}
