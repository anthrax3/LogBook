using LogBook.Entities;
using LogBook.Entities.Entities;
using LogBook.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogBook.Services.Internal
{
    internal class ReadService
    {
        #region Dependencies

        internal readonly LogBookEntityModel _context;

        internal ReadService(LogBookEntityModel context)
        {
            _context = context;
        }

        #endregion Dependencies

        internal IEnumerable<LogEntry> GetLatestLogEntries(int maximumEntryCount = 100)
        {
            var logEntries = _context.LogEntries.OrderByDescending(le => le.LogTime).Take(maximumEntryCount);

            return logEntries;
        }

        internal IEnumerable<LogEntry> GetLatestLogEntries(LogType logType, int maximumEntryCount = 100)
        {
            var logTypeId = Convert.ToInt32(logType);

            var logEntries = _context.LogEntries.Where(le => le.LogType == logTypeId).OrderByDescending(le => le.LogTime).Take(maximumEntryCount);

            return logEntries;
        }

        internal IEnumerable<LogEntry> GetLatestLogEntriesPage(int pageSize = 100, int pageNumber = 1)
        {
            var logEntries = _context.LogEntries.OrderByDescending(le => le.LogTime).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return logEntries;
        }

        internal int ErrorsSinceTime(DateTime startTime)
        {
            var errorLogTypeId = Convert.ToInt32(LogType.Error);

            var errorCount = _context.LogEntries.Count(x =>
                x.LogType == errorLogTypeId &&
                x.LogTime.Year >= startTime.Year &&
                x.LogTime.Month >= startTime.Month &&
                x.LogTime.Day >= startTime.Day &&
                x.LogTime.Hour >= startTime.Hour &&
                x.LogTime.Minute >= startTime.Minute);

            return errorCount;
        }
    }
}