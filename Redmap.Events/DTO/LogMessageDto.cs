using Microsoft.AspNetCore.Http;

namespace Redmap.Events.DTO
{
    /// <summary>
    /// LogMessageModel
    /// </summary>
    public class LogMessageDto    
    {
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
        public IFormFile AttachedFile { get; set; }
        
    }
}
