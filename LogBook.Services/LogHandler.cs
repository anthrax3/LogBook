using LogBook.Entities;
using LogBook.Entities.Entities;
using LogBook.Services.Internal;
using LogBook.Services.Models;
using System;
using System.Collections.Generic;

namespace LogBook.Services
{
    /// <summary>
    /// Use the LogHandler to Write or Read Log Entries
    /// </summary>
    public class LogHandler
    {
        #region Dependencies

        internal readonly WriteService _writeService;
        internal readonly ReadService _readService;

        public LogHandler()
        {
            var logBookEntityModel = new LogBookEntityModel();

            UpdateDatabase.Execute();

            _writeService = new WriteService(logBookEntityModel);
            _readService = new ReadService(logBookEntityModel);
        }

        #endregion Dependencies

        /// <summary>
        /// Write a basic log entry.
        /// </summary>
        /// <param name="logType">The severity of the Log Entry.</param>
        /// <param name="message">A note which will help inform a log viewer about the nature of this message.</param>
        /// <param name="userName">The name of a user which encountered an issue.</param>
        public void WriteLog(LogType logType, string message, string userName)
        {
            WriteLog(logType, string.Empty, null, message, userName);
        }

        /// <summary>
        /// Write a basic log entry including a reference to the code which raised it.
        /// </summary>
        /// <param name="logType">The severity of the Log Entry.</param>
        /// <param name="source">A reference to the source code which raised this issue.</param>
        /// <param name="message">A note which will help inform a log viewer about the nature of this message.</param>
        /// <param name="userName">The name of a user which encountered an issue.</param>
        public void WriteLog(LogType logType, string source, string message, string userName)
        {
            WriteLog(logType, source, null, message, userName);
        }

        /// <summary>
        /// Write a log entry including details about an exception that has occured.
        /// </summary>
        /// <param name="logType">The severity of the Log Entry.</param>
        /// <param name="source">A reference to the source code which raised this issue.</param>
        /// <param name="exception">The exception which contains information about what went wrong.</param>
        /// <param name="message">A note which will help inform a log viewer about the nature of this message.</param>
        /// <param name="userName">The name of a user which encountered an issue.</param>
        public void WriteLog(LogType logType, string source, Exception exception, string message, string userName)
        {
            _writeService.WriteLog(logType, source, exception, message, userName);
        }

        /// <summary>
        /// Get a list of the most recent Log Entries
        /// </summary>
        /// <param name="maximumEntryCount">How many Log Entries do you want to return?</param>
        /// <returns></returns>
        public IEnumerable<LogEntry> ReadLatestLogEntries(int maximumEntryCount = 100)
        {
            var results = _readService.GetLatestLogEntries(maximumEntryCount);

            return results;
        }

        /// <summary>
        /// Get a list of the most recent Log Entries by Log Type (For Example: Errors)
        /// </summary>
        /// <param name="logType">What type of Log Entries do you want to return?</param>
        /// <param name="maximumEntryCount">How many Log Entries do you want to return?</param>
        /// <returns></returns>
        public IEnumerable<LogEntry> ReadLatestLogEntries(LogType logType, int maximumEntryCount = 100)
        {
            var results = _readService.GetLatestLogEntries(logType, maximumEntryCount);

            return results;
        }
    }
}