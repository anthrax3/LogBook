using LogBook.Entities;
using LogBook.Services.Internal;
using LogBook.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var logEntries = _readService.GetLatestLogEntries(LogType.Error, 1);

            // ASSERT
            Assert.IsTrue(logEntries.All(le => le.LogType == LOG_TYPE_ERROR));
        }
    }
}
