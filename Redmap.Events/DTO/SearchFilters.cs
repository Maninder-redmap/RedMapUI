using System;

namespace Redmap.Events.DTO
{

    /// <summary>
    /// Search Filters model
    /// </summary>
    public class SearchFilters
    {
        /// <summary>
        /// Event Id
        /// </summary>
        public Guid EventId { get; set; }
        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Category
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
        /// Attached File
        /// </summary>
        public string Attachment { get; set; }

        /// <summary>
        ///Event Time Stamp Date
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public string Startdate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public string Enddate { get; set; }

        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page Number
        /// </summary>
        public int PageNumber { get; set; }

    }

}
