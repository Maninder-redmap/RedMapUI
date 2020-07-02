using Microsoft.Extensions.Configuration;
using Redmap.Events.DTO;
using Redmap.Events.Model;
using Redmap.Events.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Redmap.Events.Repository
{
    /// <summary>
    /// Event Message Repository
    /// </summary>
    public class LogMessageRepository : BaseRepository, ILogMessageRepository
    {
        #region database connection
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public LogMessageRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion


        #region repository functions


        /// <summary>
        /// For fetch event messages data with parameter using postgreSQL function.
        /// </summary>
        /// <param name="logCategoryId"></param>
        /// <returns>List of type LogMessageModel</returns>
        public List<LogMessageModel> GetLogMessages(int logCategoryId)
        {
            var query = "select * from public.func_geteventmessagesbycategory(" + logCategoryId + ") ";
            var logMessages = Get<LogMessageModel>(query);
            return logMessages.ToList();
        }

        /// <summary>
        /// Get filter events
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<LogMessageModel> GetLogMessages(SearchFilters model)
        {
            
            var query = "";
            if (string.IsNullOrEmpty(model.CreatedDate))
            {
                query = "select * from public.func_geteventmessages(" + model.CategoryId + ",'" + model.Message + "','" + model.Summary + "','" + model.Source + "','" + model.Serverdetail + "','" + model.Errorcode + "','" + model.Attachment + "',"+"null"+ "," + "null" + "," + model.PageSize + "," + model.PageNumber + ") ";
            }
            else
            {
                query = "select * from public.func_geteventmessages(" + model.CategoryId + ",'" + model.Message + "','" + model.Summary + "','" + model.Source + "','" + model.Serverdetail + "','" + model.Errorcode + "','" + model.Attachment + "','" + model.Startdate.ToString() + "','" + model.Enddate.ToString() + "'," + model.PageSize + "," + model.PageNumber + ") ";
            }
             
            var logMessages = Get<LogMessageModel>(query);
            return logMessages.ToList();
        }
        /// <summary>
        /// Export events
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<LogMessageModel> GetExportEvents(SearchFilters model)
        {

            var query = "";
            if (string.IsNullOrEmpty(model.CreatedDate))
            {
                query = "select * from public.func_exportevents(" + model.CategoryId + ",'" + model.Message + "','" + model.Summary + "','" + model.Source + "','" + model.Serverdetail + "','" + model.Errorcode + "','" + model.Attachment + "'," + "null" + "," + "null" + ") ";
            }
            else
            {
                query = "select * from public.func_exportevents(" + model.CategoryId + ",'" + model.Message + "','" + model.Summary + "','" + model.Source + "','" + model.Serverdetail + "','" + model.Errorcode + "','" + model.Attachment + "','" + model.Startdate.ToString() + "','" + model.Enddate.ToString() + "') ";
            }

            var logMessages = Get<LogMessageModel>(query);
            return logMessages.ToList();
        }


        /// <summary>
        /// For fetch event messages data using postgreSQL function.
        /// </summary>        
        /// <returns>List of type LogMessageModel </returns>
        public List<LogMessageModel> GetLogMessages()
        {
            var query = "select * from public.func_geteventmessages()";
            var logMessages = Get<LogMessageModel>(query);
            return logMessages.ToList();
        }

        /// <summary>
        /// Get event detail.
        /// </summary>        
        /// <returns>single event </returns>
        public LogMessageModel GetEventDetail(Guid EventId)
        {
            var query = "select * from public.func_geteventdetail('" + EventId + "')";
            var logMessages = Get<LogMessageModel>(query).FirstOrDefault();
            return logMessages;
        }
       
        /// <summary>
        /// Save event message data using postgreSQL function.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>eventid</returns>
        public Guid SaveLogMessage(LogMessageModel model)
        {

            var query = "select * from public.func_saveevents(" + model.EventCategoryId + ",'" + model.Message + "','" + model.Summary + "','" + model.Errorcode + "','" + model.Attachment + "','" + model.Serverdetail + "','" + model.Source + "','" + model.Tag1 + "','" + model.Tag2 + "'," + "null" + ") ";

            var logMessage = Get<LogMessageModel>(query).FirstOrDefault();
            return logMessage.EventId;
        }

        /// <summary>
        /// Get top 5 events.
        /// </summary>
        /// <returns></returns>
        public List<LogMessageModel> GetTop5Events(string CategoryId, string ServerDetail)
        {
            var query = "select * from public.func_gettop5events('" + CategoryId + "','" + ServerDetail + "')";
            var logMessages = Get<LogMessageModel>(query);
            return logMessages.ToList();
        }

        /// <summary>
        /// Get master categories.
        /// </summary>
        /// <returns></returns>
        public List<MasterCategoriesModel> GetMasterCategories()
        {
            var query = "select * from public.func_getmastercategories()";
            var logMessages = Get<MasterCategoriesModel>(query);
            return logMessages.ToList();
        }
    }

        #endregion

    }



