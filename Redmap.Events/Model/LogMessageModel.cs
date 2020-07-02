using NpgsqlTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Redmap.Events.Model
{
    /// <summary>
    /// Log Message Model
    /// </summary>
    [Table("logmessage")]
    public class LogMessageModel    
    {
        /// <summary>
        /// EventId
        /// </summary>
        [Key]
        public Guid EventId { get; set; }
        /// <summary>
        /// EventCategory Id
        /// </summary>
        public int EventCategoryId { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        public string Category { get; set; }   
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Summary
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// Errorcode
        /// </summary>
        public string Errorcode { get; set; }
        /// <summary>
        /// Attachment
        /// </summary>
        public string Attachment { get; set; }
        /// <summary>
        /// Source
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Server detail
        /// </summary>
        public string Serverdetail { get; set; }
        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }
        /// <summary>
        /// Tag1
        /// </summary>
        public string Tag1 { get; set; }
        /// <summary>
        /// Tag2
        /// </summary>
        public string Tag2 { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
