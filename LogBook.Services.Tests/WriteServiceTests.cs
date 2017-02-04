using LogBook.Entities;
using LogBook.Services.Internal;
using NUnit.Framework;
using System;
using System.Linq;

namespace LogBook.Services.Tests
{
    [TestFixture]
    public class WriteServiceTests
    {
        #region Dependencies

        private LogBookEntityModel _mockContext;

        internal WriteService _writeService;

        [SetUp]
        public void Initialise()
        {
            var connection = Effort.DbConnectionFactory.CreateTransient();

            _mockContext = new LogBookEntityModel(connection);

            _mockContext.Database.CreateIfNotExists();

            _writeService = new WriteService(_mockContext);
        }

        #endregion Dependencies

        [Test]
        public void WriteLog_Succeeds()
        {
            _writeService.WriteLog(Models.LogType.Information, nameof(WriteLog_Succeeds), null, "WriteLog Produces a Log Entry", string.Empty);

            Assert.IsTrue(_mockContext.LogEntries.Count() == 1);
        }

        [Test]
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