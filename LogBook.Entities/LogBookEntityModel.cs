namespace LogBook.Entities
{
    using Entities;
    using System.Data.Common;
    using System.Data.Entity;

    public class LogBookEntityModel : DbContext
    {
        #region Dependencies

        public LogBookEntityModel(DbConnection connection) : base(connection, true)
        {
        }

        public LogBookEntityModel() : base("name=LogBookEntityModel")
        {
        }

        #endregion Dependencies

        public virtual DbSet<LogEntry> LogEntries { get; set; }

        public virtual DbSet<LogException> LogExceptions { get; set; }
    }
}