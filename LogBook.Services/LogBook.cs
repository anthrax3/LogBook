using LogBook.Services.Internal;
using LogBook.Services.Models;

namespace LogBook.Interact
{
    public static class LogBook
    {
        public static void WriteLog(LogType logType)
        {
            var writeService = new WriteService();
        }

        public static void ReadLogs()
        {
            var readService = new ReadService();
        }

        public static void ReadStatistics()
        {
            var readService = new ReadService();
        }
    }
}