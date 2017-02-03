using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogBook.Entities.Entities
{
    public class LogEntry
    {
        [Key]
        public int LogEntryId { get; set; }

        [Required]
        public int LogType { get; set; }

        [Required]
        public string HostName { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string UserName { get; set; }

        public DateTime LogTime { get; set; }

        #region Virtual Properties

        public virtual ICollection<LogException> LogExceptions { get; set; }

        #endregion Virtual Properties
    }
}