using LogBook.Entities;
using LogBook.Entities.Entities;
using LogBook.Services.Models;
using System;

namespace LogBook.Services.Internal
{
    internal class WriteService
    {
        internal readonly LogBookEntityModel _context;

        internal WriteService(LogBookEntityModel context)
        {
            _context = context;
        }

        internal void WriteLog(LogType logType, string source, Exception exception, string message, string userName)
        {
            var logEntry = new LogEntry
            {
                LogType = Convert.ToInt32(logType),
                HostName = Environment.MachineName,
                Source = source,
                Message = message,
                UserName = userName,
                LogTime = DateTime.Now
            };

            if (exception != null)
            {
                var logException = new LogException
                {
                    LogEntry = logEntry,
                    ExceptionDetail = exception.ToString()
                };

                _context.LogExceptions.Add(logException);
            }

            _context.LogEntries.Add(logEntry);

            _context.SaveChanges();
        }
    }
}