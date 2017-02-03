using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogBook.Entities.Entities
{
    public class LogException
    {
        [Key]
        public int LogExceptionId { get; set; }

        [ForeignKey(nameof(LogEntry))]
        public int LogEntryId { get; set; }

        public string ExceptionDetail { get; set; }

        #region Virtual Properties

        public virtual LogEntry LogEntry { get; set; }

        #endregion Virtual Properties
    }
}