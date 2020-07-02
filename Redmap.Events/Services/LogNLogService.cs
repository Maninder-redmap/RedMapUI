using Redmap.Events.Services.Interface;
using NLog;

namespace Redmap.Events.Services
{
    /// <summary>
    /// LogNLogService class
    /// </summary>
    public class LogNLogService : ILogService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// LogNLogService Constructor
        /// </summary>
        public LogNLogService()
        {
        }

        /// <summary>
        /// Get Log Information
        /// </summary>
        /// <param name="message"></param>
        public void Information(string message)
        {
            logger.Info(message);
        }
        /// <summary>
        /// Get Log Warning
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            logger.Warn(message);
        }
        /// <summary>
        /// Get Log Debug
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            logger.Debug(message);
        }
        /// <summary>
        /// Get Log Error
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            logger.Error(message);
        }
    }
}
