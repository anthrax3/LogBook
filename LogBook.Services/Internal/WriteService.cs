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
                UserName = userName
            };

            if (exception != null)
            {
            }

            _context.LogEntries.Add(logEntry);

            _context.SaveChanges();
        }
    }
}