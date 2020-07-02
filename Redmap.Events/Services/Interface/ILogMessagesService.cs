using Redmap.Events.DTO;
using System;
using System.Collections.Generic;

namespace Redmap.Events.Services.Interface
{
    /// <summary>
    ///Event message service interface.
    /// </summary>
    public interface ILogMessagesService
    {
        /// <summary>
        /// Get log messages list with parameter
        /// </summary>
        /// <param name="logCategoryId"></param>
        /// <returns>List of type LogMessageModel</returns>
        List<EventsDto> GetLogMessages(int logCategoryId);

        /// <summary>
        /// Get events by parameters
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<EventsDto> GetLogMessages(SearchFilters model);
        
        /// <summary>
        /// Get log messages list with parameter
        /// </summary>        
        /// <returns>List of type LogMessageModel</returns>
        List<EventsDto> GetLogMessages();
        /// <summary>
        /// Export events
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        List<EventsDto> GetExportEvents(SearchFilters model);

        /// <summary>
        /// Save log message
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResponseModel SaveLogMessage(LogMessageDto model);
        /// <summary>
        /// Get top 5 events
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="ServerDetail"></param>
        /// <returns></returns>
        List<EventsDto> GetTop5Events(string CategoryId, string ServerDetail);

        /// <summary>
        /// Get Event Detail
        /// </summary>
        /// <returns></returns>
        EventsDto GetEventDetail(Guid EventId);
        /// <summary>
        /// Get master categories
        /// </summary>
        /// <returns></returns>
        List<MasterCategoriesDto> GetMasterCategories();

    }

}
