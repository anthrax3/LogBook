using LogBook.Entities;
using LogBook.Entities.Entities;
using LogBook.Services.Internal;
using LogBook.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LogBook.Services.Tests
{
    [TestClass]
    public class ReadServiceTests
    {
        #region Dependencies

        private LogBookEntityModel _mockContext;

        internal WriteService _writeService;
        internal ReadService _readService;

        [TestInitialize]
        public void Initialise()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _mockContext = new LogBookEntityModel(connection);

            _mockContext.Database.CreateIfNotExists();

            _writeService = new WriteService(_mockContext);
            _readService = new ReadService(_mockContext);
        }

        #endregion Dependencies

        [TestMethod]
        public void GetLatestLogEntries_ReturnsLogEntries()
        {
            // ARRANGE
            _writeService.WriteLog(Models.LogType.Information, nameof(GetLatestLogEntries_ReturnsLogEntries), null, "Test Log Entry 1", string.Empty);
            _writeService.WriteLog(Models.LogType.Information, nameof(GetLatestLogEntries_ReturnsLogEntries), null, "Test Log Entry 2", string.Empty);

            // ACT
            var logEntries = _readService.GetLatestLogEntries(100);

            // ASSERT
            Assert.IsTrue(_mockContext.LogEntries.Count() == 2);
        }

        [TestMethod]
        public void GetLatestLogEntries_ReturnsSpecifiedNumberOfEntries()
        {
            // ARRANGE
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntries_ReturnsLogEntries), null, "Test Log Entry 1", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntries_ReturnsLogEntries), null, "Test Log Entry 2", string.Empty);

            // ACT
            var logEntries = _readService.GetLatestLogEntries(1);

            // ASSERT
            Assert.IsTrue(logEntries.Count() == 1);
            Assert.AreEqual(logEntries.First().Message, "Test Log Entry 2");
        }

        [TestMethod]
        public void GetLatestLogEntries_ReturnsSpecifiedLogTypeOnly()
        {
            // ARRANGE
            const int LOG_TYPE_ERROR = 3;

            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntries_ReturnsLogEntries), null, "Test Log Entry 1", string.Empty);
            _writeService.WriteLog(LogType.Error, nameof(GetLatestLogEntries_ReturnsLogEntries), null, "Test Log Entry 2", string.Empty);

            // ACT
            var logEntries = _readService.GetLatestLogEntries(LogType.Error, 10);

            // ASSERT
            Assert.IsTrue(logEntries.All(le => le.LogType == LOG_TYPE_ERROR));
        }

        [TestMethod]
        public void GetLatestLogEntriesPage_ReturnsCorrectFirstPage()
        {
            // ARRANGE
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectFirstPage), null, "Test Log Entry 1", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectFirstPage), null, "Test Log Entry 2", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectFirstPage), null, "Test Log Entry 3", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectFirstPage), null, "Test Log Entry 4", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectFirstPage), null, "Test Log Entry 5", string.Empty);

            // ACT
            var logEntries = _readService.GetLatestLogEntriesPage(1, 1);

            // ASSERT
            Assert.IsTrue(logEntries.First().Message == "Test Log Entry 5");
        }

        [TestMethod]
        public void GetLatestLogEntriesPage_ReturnsCorrectThirdPage()
        {
            // ARRANGE
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectThirdPage), null, "Test Log Entry 1", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectThirdPage), null, "Test Log Entry 2", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectThirdPage), null, "Test Log Entry 3", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectThirdPage), null, "Test Log Entry 4", string.Empty);
            _writeService.WriteLog(LogType.Information, nameof(GetLatestLogEntriesPage_ReturnsCorrectThirdPage), null, "Test Log Entry 5", string.Empty);

            // ACT
            var logEntries = _readService.GetLatestLogEntriesPage(1, 3);

            // ASSERT
            Assert.IsTrue(logEntries.First().Message == "Test Log Entry 3");
        }

        [TestMethod]
        public void ErrorsSinceTime_FiltersDate()
        {
            // ARRANGE
            var logEntries = new List<LogEntry>();
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 1", LogTime = new DateTime(2015, 2, 1), LogType = Convert.ToInt32(LogType.Error) });
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 2", LogTime = new DateTime(2016, 2, 1), LogType = Convert.ToInt32(LogType.Error) });
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 3", LogTime = new DateTime(2017, 2, 1), LogType = Convert.ToInt32(LogType.Error) });
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 4", LogTime = new DateTime(2017, 2, 1), LogType = Convert.ToInt32(LogType.Error) });
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 5", LogTime = new DateTime(2017, 2, 1), LogType = Convert.ToInt32(LogType.Error) });

            _mockContext.LogEntries.AddRange(logEntries);
            _mockContext.SaveChanges();

            // ACT
            var actualErrorCount = _readService.ErrorsSinceTime(new DateTime(2017, 2, 1));

            // ASSERT
            Assert.AreEqual(3, actualErrorCount);
        }

        [TestMethod]
        public void ErrorsSinceTime_FiltersLogType()
        {
            // ARRANGE
            var logEntries = new List<LogEntry>();
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 1", LogTime = new DateTime(2017, 2, 1), LogType = Convert.ToInt32(LogType.Error) });
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 2", LogTime = new DateTime(2017, 2, 1), LogType = Convert.ToInt32(LogType.Information) });
            logEntries.Add(new LogEntry { HostName = "Test", Message = "Test Log Entry 3", LogTime = new DateTime(2017, 2, 1), LogType = Convert.ToInt32(LogType.Warning) });

            _mockContext.LogEntries.AddRange(logEntries);
            _mockContext.SaveChanges();

            // ACT
            var actualErrorCount = _readService.ErrorsSinceTime(new DateTime(2017, 2, 1));

            // ASSERT
            Assert.AreEqual(1, actualErrorCount);
        }
    }
}