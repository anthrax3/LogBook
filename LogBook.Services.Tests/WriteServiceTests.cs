using LogBook.Entities;
using LogBook.Services.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LogBook.Services.Tests
{
    [TestClass]
    public class WriteServiceTests
    {
        #region Dependencies

        private LogBookEntityModel _mockContext;

        internal WriteService _writeService;

        [TestInitialize]
        public void Initialise()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _mockContext = new LogBookEntityModel(connection);

            _mockContext.Database.CreateIfNotExists();

            _writeService = new WriteService(_mockContext);
        }

        #endregion Dependencies

        [TestMethod]
        public void WriteLog_Succeeds()
        {
            _writeService.WriteLog(Models.LogType.Information, nameof(WriteLog_Succeeds), null, "WriteLog Produces a Log Entry", string.Empty);

            Assert.IsTrue(_mockContext.LogEntries.Count() == 1);
        }

        [TestMethod]
        public void WriteLog_ExceptionLogged()
        {
            try
            {
                throw new Exception("This is a test Exception");
            }
            catch (Exception ex)
            {
                _writeService.WriteLog(Models.LogType.Information, nameof(WriteLog_ExceptionLogged), ex, "WriteLog Produces an Exception Log Entry", string.Empty);
            }

            Assert.IsTrue(_mockContext.LogEntries.Count() == 1);
            Assert.IsTrue(_mockContext.LogExceptions.Count() == 1);
        }
    }
}