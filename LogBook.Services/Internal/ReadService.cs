using LogBook.Entities;
using LogBook.Entities.Entities;
using LogBook.Services.Models;
using System;
using System.Collections;
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
            var logEntries = _context.LogEntries.Where(le => le.LogType == Convert.ToInt32(logType)).OrderByDescending(le => le.LogTime).Take(maximumEntryCount);

            return logEntries;
        }
    }
}