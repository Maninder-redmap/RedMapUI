namespace Redmap.Events.DTO
{
    /// <summary>
    /// Response modle class.
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// Status code
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
    }
}
