namespace Redmap.Events.Services.Interface
{
    /// <summary>
    ///Log Service Interface
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Information(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
    }
}
