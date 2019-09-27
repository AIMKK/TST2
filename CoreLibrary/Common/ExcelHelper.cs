using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
    //.net core下对于Excel的一些操作及使用
    //EPPlus与NPOI的选择
    public class ExcelHelper
    {
        /// <summary>
        /// StartupPath
        /// </summary>
        static string startupPath = System.AppDomain.CurrentDomain.BaseDirectory; //System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);//System.AppDomain.CurrentDomain.BaseDirectory

        /// <summary>
        /// GetExcelData
        /// </summary>
        /// <param name="excelFileName">excelFileName</param>
        /// <param name="excelHasHeader">第一行是数据而不是标题设置: "HDR=No;" 否则是Yes</param>
        /// <param name="sheetName">sheetName</param>
        /// <returns></returns>
        public static DataTable GetExcelData(string excelFileName, bool excelHasHeader, string sheetName = null)
        {
            string excelFileNameTemp = "";
            string excelFileTempPath = "";           
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int dataRowIndex = 0;
             
            DataTable dtData = new DataTable();
            //
            if ((excelFileName ?? "").Trim().Length == 0)
            {
                return null;
            }
            try
            { //
                excelFileTempPath = string.Format("{0}\\tempImportData", startupPath);//构建临时文件路径               
                excelFileNameTemp = string.Format("{0}\\{1}.xlstemp", excelFileTempPath, System.Guid.NewGuid().ToString("N"));//构建临时文件路径               
                //
                if (System.IO.File.Exists(excelFileTempPath))
                    System.IO.File.Delete(excelFileTempPath);
                //
                if (!System.IO.Directory.Exists(excelFileTempPath))
                    System.IO.Directory.CreateDirectory(excelFileTempPath);
                //
                System.IO.FileStream fstream = System.IO.File.Create(excelFileNameTemp);
                if (fstream != null)
                    fstream.Close();
                //
                System.IO.File.Copy(excelFileName, excelFileNameTemp, true);//避免原来文件被打开，导致没有办法读取数据，先拷贝一份临时的 
                
                //根据指定路径读取文件
                FileStream fs = new FileStream(excelFileNameTemp, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fs);
                //IWorkbook workbook = new HSSFWorkbook(fs);
                //如果有指定工作表名称
                if ((sheetName??"").Trim().Length>0)
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                else//如果没有指定的sheetName，取第一个sheet
                {                    
                    sheet = workbook.GetSheetAt(0);
                }
                //
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (excelHasHeader)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    dtData.Columns.Add(column);
                                }
                            }
                        }
                        dataRowIndex = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        dataRowIndex = sheet.FirstRowNum;
                    }                   
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = dataRowIndex; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = dtData.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        dtData.Rows.Add(dataRow);
                    }
                }                
            }
            catch (Exception ex)
            {
                CoreLibrary.NlogHelper.LogToFile(ex.ToString());
            }
            finally
            {               
                System.IO.File.Delete(excelFileNameTemp);//使用完成后删除掉
            }
            return dtData;
        }
    }
}
