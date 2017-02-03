using LogBook.Entities;
using LogBook.Services.Internal;
using LogBook.Services.Models;
using System;

namespace LogBook.Services
{
    public class LogHandler
    {
        #region Dependencies

        internal readonly WriteService _writeService;

        public LogHandler(string connectionString)
        {
            var logBookEntityModel = new LogBookEntityModel(connectionString);

            _writeService = new WriteService(logBookEntityModel);
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
    }
}