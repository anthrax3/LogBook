using LogBook.Services.Internal;
using LogBook.Services.Models;

namespace LogBook.Services
{
    public static class LogHandler
    {
        #region Write Methods

        public static void WriteLog(LogType logType, string message)
        {
            var writeService = new WriteService();

            writeService.WriteLog(logType, string.Empty, string.Empty, message, string.Empty);
        }

        public static void WriteLog(LogType logType, string exceptionType, string source, string message, string userName)
        {
            var writeService = new WriteService();

            writeService.WriteLog(logType, exceptionType, source, message, userName);
        }

        public static void WriteLog(LogType logType, string source, string message, string userName)
        {
            var writeService = new WriteService();

            writeService.WriteLog(logType, string.Empty, source, message, userName);
        }

        #endregion Write Methods

        #region Read Methods

        public static void ReadLogs()
        {
            var readService = new ReadService();
        }

        public static void ReadStatistics()
        {
            var readService = new ReadService();
        }

        #endregion Read Methods
    }
}