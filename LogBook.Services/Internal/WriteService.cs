using LogBook.Services.Models;

namespace LogBook.Services.Internal
{
    internal interface IWriteService
    {
    }

    internal class WriteService : IWriteService
    {
        public void WriteLog(LogType logType, string exceptionType, string source, string message, string userName)
        {
            return;
        }
    }
}