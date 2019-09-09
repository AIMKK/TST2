using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowUI
{
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
        /// <returns></returns>
        public static DataTable GetExcelData(string excelFileName, string excelHasHeader)
        {
            string excelFileNameTemp = "";
            string excelFileTempPath = "";
            string strConn = "";
            string sheetName = "Sheet1";
            OleDbConnection conn = null;
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
                //
                //如果第一行是数据而不是标题设置: "HDR=No;" 否则是Yes
                strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + excelFileNameTemp + ";Extended Properties='Excel 8.0; HDR=" + excelHasHeader + "; IMEX=1'";
                if (excelFileName.IndexOf(".xlsx") > -1)
                {
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFileNameTemp + ";Extended Properties=\"Excel 12.0;HDR=" + excelHasHeader + ";\"";
                }

                sheetName = "Sheet1";
                //
                conn = new OleDbConnection(strConn);
                conn.Open();
                DataTable sheetNames = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                if (sheetNames != null && sheetNames.Rows.Count > 0)
                {
                    sheetName = (sheetNames.Rows[0]["table_name"] ?? "").ToString().Trim();//找第一个 sheet的名字
                    OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + sheetName + "]", conn);
                    //OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + sheetName + "$]", conn);
                    odda.Fill(dtData);
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                WindowUI.NlogHelper.LogToFile(ex.ToString());
            }
            finally
            {
                if (conn != null)
                    conn.Close();
                //
                System.IO.File.Delete(excelFileNameTemp);//使用完成后删除掉
            }
            return dtData;
        }
    }
}
