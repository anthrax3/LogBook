namespace LogBook.Entities
{
    using Entities;
    using System.Data.Entity;

    public class LogBookEntityModel : DbContext
    {
        #region Dependencies

        public LogBookEntityModel(string connectionString = "name=LogBookEntityModel") : base(connectionString)
        {
        }

        #endregion Dependencies

        public virtual DbSet<LogEntry> LogEntries { get; set; }
    }
}