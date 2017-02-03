namespace LogBook.Entities
{
    using Entities;
    using System.Data.Entity;

    public class LogBookEntityModel : DbContext
    {
        #region Dependencies

        public LogBookEntityModel() : base("name=LogBookEntityModel")
        {
        }

        #endregion Dependencies

        public virtual DbSet<LogEntry> LogEntries { get; set; }
    }
}