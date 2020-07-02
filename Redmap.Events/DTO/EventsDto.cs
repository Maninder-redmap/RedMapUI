using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redmap.Events.DTO
{

    /// <summary>
    /// LogMessageModel
    /// </summary>
    public class EventsDto
    {
        /// <summary>
        /// Event Id
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// Category Name
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Log Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Log Summary
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// Log Errorcode
        /// </summary>
        public string Errorcode { get; set; }
        /// <summary>
        /// Log Source
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Server Detail
        /// </summary>
        public string Serverdetail { get; set; }
        /// <summary>
        /// Tag1
        /// </summary>
        public string Tag1 { get; set; }

        /// <summary>
        /// Tag2
        /// </summary>
        public string Tag2 { get; set; }

        /// <summary>
        /// Attached File
        /// </summary>
        public string Attachment { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        public string CreatedDate { get; set; }

        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Top 5 Events
        /// </summary>
        public List<EventsDto> Top5Events { get; set; }

        /// <summary>
        /// Get master categories
        /// </summary>
       // public List<MasterCategoriesDto> GetMastercategories { get; set; }
        public List<string> GetMastercategories { get; set; }

    }

}
