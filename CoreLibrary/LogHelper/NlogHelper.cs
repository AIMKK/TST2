using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLibrary
{
    /// <summary>
    /// NlogHelper
    /// </summary>
    public class NlogHelper
    {
        /// <summary>
        /// 记录日志的内容
        /// </summary>
        public class NLogMessage
        {
            /// <summary>
            /// LogPurpose
            /// </summary>
            public enum Purpose
            {
                None = 0,
                SaveLocaldata = 1,
                SolveProblem = 2
            }
            /// <summary>
            /// Message
            /// </summary>
            public string Message { get; set; }

            /////// <summary>
            /////// Loggerlevel
            /////// </summary>
            ////public LogLevel Loggerlevel { get; set; }

            /// <summary>
            /// CodeLoction
            /// </summary>
            public string CodeLoction { get; set; }

            /// <summary>
            /// savelocaldata,solveProblem
            /// </summary>
            public Purpose LogPurpose { get; set; }

            /// <summary>
            /// Other01
            /// </summary>
            public string Other01 { get; set; }

        }
        /// <summary>
        /// StartupPath
        /// </summary>
        static string startupPath = System.AppDomain.CurrentDomain.BaseDirectory;// System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);//System.AppDomain.CurrentDomain.BaseDirectory

        /// <summary>
        /// 
        /// </summary>
        static string logDBPath = string.Format("{0}\\AppLog\\AppDBLog", startupPath);

        /// <summary>
        /// 
        /// </summary>
        static Logger sqlitedbLog = LogManager.GetLogger("sqlitedblog");

        /// <summary>
        /// 
        /// </summary>
        static Logger fileLog = LogManager.GetLogger("filelog");
        /// <summary>
        /// WriteSqliteDB
        /// 每个月创建一个表，最多保存前三个月的历史数据
        /// </summary>
        private static void LogToSqliteDB(NLogMessage evc)
        {
            if (evc == null)
                return;

            if (sqlitedbLog != null)//里面进行细致分类，
            {//如果数据库文件很大了，就清空文件，如果表的文件很大了，就清空记录，清空一个月以前的记录，
                if (!Directory.Exists(logDBPath))//文件夹不存在，创建
                    Directory.CreateDirectory(logDBPath);
                string fileFullName = logDBPath + "\\AppSqlitLog.db";
                if (!File.Exists(fileFullName))//如果数据库不存在，创建数据库，并且创建初始表
                {
                    SQLiteConnectionStringBuilder cnnSB = new SQLiteConnectionStringBuilder();
                    cnnSB.DataSource = fileFullName;
                    using (SQLiteConnection connection = new SQLiteConnection(cnnSB.ToString()))
                    {
                        using (SQLiteCommand command = new SQLiteCommand(
                            "CREATE TABLE Log (LogYM VARCHAR(10),Date VARCHAR(50) , Level VARCHAR(50) NOT NULL,LoggerIP VARCHAR(50) NOT NULL, CodeLoction VARCHAR(255) NOT NULL,LogPurpose VARCHAR(8) NOT NULL, Message TEXT DEFAULT NULL)",
                            connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                else//文件存在，检查表是否存在，如果，日志文件特别大，自动分表的功能，
                {
                    #region 不用
                    //SELECT COUNT(*)  as CNT FROM sqlite_master where type='table' and name='DBInfo' //其中DBInfo为需要判断的表名。注意大小写敏感！

                    //var config = new LoggingConfiguration();
                    //var fileTarget = new FileTarget()
                    //{
                    //    Layout = "${longdate} ${uppercase:[${level}]} - ${message} ${exception:format=tostring}",
                    //    FileName = string.Format("${basedir}/logs/{0}/{1}月/${shortdate}.log", DateTime.Now.Year, DateTime.Now.Month),
                    //    Encoding = Encoding.UTF8
                    //};
                    //config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
                    //LogManager.Configuration = config;                    
                    #endregion

                    SQLiteConnectionStringBuilder cnnSB = new SQLiteConnectionStringBuilder();
                    cnnSB.DataSource = fileFullName;
                    using (SQLiteConnection connection = new SQLiteConnection(cnnSB.ToString()))
                    {
                        using (SQLiteCommand command = new SQLiteCommand(
                            string.Format("delete from Log where logYM <='{0}'", DateTime.Now.AddMonths(-3).ToString("yyyyMM"))//删除三个月以前的数据
                            ,
                            connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }

                //
                LogEventInfo logEvnt = new LogEventInfo();
                logEvnt.Properties["LogPurpose"] = evc.LogPurpose;
                logEvnt.Properties["LoggerIP"] = "";
                logEvnt.Properties["CodeLoction"] = evc.CodeLoction;
                logEvnt.Properties["Other01"] = evc.Other01;
                logEvnt.Level = LogLevel.Info;//默认info
                logEvnt.Message = evc.Message;

                //
                sqlitedbLog.Log(logEvnt);

            }
        }


        /// <summary>
        /// LogToFile
        /// </summary>
        public static void LogToFile(string mess)
        {
            if (fileLog != null)//里面进行细致分类，
            {
                try
                {
                    fileLog.Info(mess);
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
            }
        }
    }

}
