using Redmap.Events.DTO;
using Redmap.Events.Model;
using System;
using System.Collections.Generic;

namespace Redmap.Events.Repository.Interface
{
    /// <summary>
    /// Event message repository interface
    /// </summary>
    public interface ILogMessageRepository
    {
        /// <summary>
        /// For fetch event messages data with parameter.
        /// </summary>
        /// <param name="logCategoryId"></param>
        /// <returns>List of events</returns>
        List<LogMessageModel> GetLogMessages(int logCategoryId);

        /// <summary>
        /// Get events data with filters
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<LogMessageModel> GetLogMessages(SearchFilters model);

        /// <summary>
        /// For fetch event messages data.
        /// </summary>        
        /// <returns>List of events</returns>
        List<LogMessageModel> GetLogMessages();

        /// <summary>
        /// Get event detail
        /// </summary>        
        /// <returns>Event</returns>
        LogMessageModel GetEventDetail(Guid EventId);
        

        /// <summary>
        /// Save event message data .
        /// </summary>
        /// <param name="model"></param>
        /// <returns>logid</returns>
        Guid SaveLogMessage(LogMessageModel model);

        /// <summary>
        /// Get top five events
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="ServerDetail"></param>
        /// <returns></returns>
        List<LogMessageModel> GetTop5Events(string CategoryId, string ServerDetail);

        /// <summary>
        /// Export events
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<LogMessageModel> GetExportEvents(SearchFilters model);

        /// <summary>
        /// Get master categories
        /// </summary>
        /// <returns></returns>
        List<MasterCategoriesModel> GetMasterCategories();
    }
}
