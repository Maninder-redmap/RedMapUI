using System.Text.Json;

namespace Redmap.Events.Logging
{
    /// <summary>
    /// Error Detail Model
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Status Code
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// String json JsonSerializer
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
