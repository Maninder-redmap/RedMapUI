using Redmap.Events.Common;
using Redmap.Events.DTO;
using Redmap.Events.Model;
using Redmap.Events.Repository.Interface;
using Redmap.Events.Services.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using System.Globalization;

namespace Redmap.Events.Services
{
    /// <summary>
    /// LogMessagesService
    /// </summary>
    public class LogMessagesService : ILogMessagesService
    {
        #region constructor
        private readonly ILogMessageRepository logMessagesRepository;
        [Obsolete]
        private IHostingEnvironment hostingEnvironment;
        private readonly IMapper mapper;
        private readonly string awsAccessKey;
        private readonly string awsSecreteKey;
        private readonly string bucketName;        

        /// <summary>
        /// Event message service
        /// </summary>
        /// <param name="logMessagesRepository"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        [Obsolete]
        public LogMessagesService(ILogMessageRepository logMessagesRepository, IHostingEnvironment hostingEnvironment, IMapper mapper, IConfiguration configuration)
        {
            this.logMessagesRepository = logMessagesRepository;
            this.mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
            awsAccessKey = configuration.GetValue<string>("S3ConfigInfo:AWSAccessKey");
            awsSecreteKey = configuration.GetValue<string>("S3ConfigInfo:AWSSecretKey");
            bucketName = configuration.GetValue<string>("S3ConfigInfo:BucketName");            
        }
        #endregion

        #region service functions

        /// <summary>
        /// Get log messages by category id.
        /// </summary>
        /// <param name="logCategoryId"></param>
        /// <returns> LogMessageDto List</returns>
        public List<EventsDto> GetLogMessages(int logCategoryId)
        {
            var logmessages = logMessagesRepository.GetLogMessages(logCategoryId);
            logmessages.ForEach(c => c.CreatedDate =string.IsNullOrEmpty(c.CreatedDate) ? c.CreatedDate : DateTime.Parse(CommonClass.ChangeDateFormat(c.CreatedDate)).ToLocalTime().ToString());
            return mapper.Map<List<EventsDto>>(logmessages);
        }

        /// <summary>
        /// Get events by filter parameters
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<EventsDto> GetLogMessages(SearchFilters model)
        {      
            if(model.CreatedDate!=null)
            {
                model.Startdate = model.CreatedDate.Split('~')[0].Replace('.','-');
                model.Enddate = model.CreatedDate.Split('~')[1].Replace('.', '-');
            }
            var logmessages = logMessagesRepository.GetLogMessages(model);            
            return mapper.Map<List<EventsDto>>(logmessages);
        }
        /// <summary>
        /// Export events
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<EventsDto> GetExportEvents(SearchFilters model)
        {
            if (model.CreatedDate != null)
            {
                model.Startdate = model.CreatedDate.Split('~')[0].Replace('.', '-');
                model.Enddate = model.CreatedDate.Split('~')[1].Replace('.', '-');
            }
            var logmessages = logMessagesRepository.GetExportEvents(model);            
            return mapper.Map<List<EventsDto>>(logmessages);
        }

        /// <summary>
        /// Get all event messages
        /// </summary>
        /// <returns>event List</returns>
        public List<EventsDto> GetLogMessages()
        {
            var logmessages = logMessagesRepository.GetLogMessages();           
            return mapper.Map<List<EventsDto>>(logmessages);
        }

        /// <summary>
        ///Get event detail
        /// </summary>
        /// <returns>event List</returns>
        public EventsDto GetEventDetail(Guid EventId)
        {
            var logmessage = logMessagesRepository.GetEventDetail(EventId);                      
            return mapper.Map<EventsDto>(logmessage);
        }        

        /// <summary>
        /// Save log message with attached file.
        /// </summary>
        /// <param name="model"></param>
        [Obsolete]
        public ResponseModel SaveLogMessage(LogMessageDto model)
        {
            //auto mapping
            LogMessageModel logMessage = mapper.Map<LogMessageModel>(model);
            ResponseModel response = new ResponseModel();

            // replace single quotes with double
            logMessage.Message = CommonClass.ReplaceSingleQuotes(logMessage.Message);
            logMessage.Source = CommonClass.ReplaceSingleQuotes(logMessage.Source);
            logMessage.Serverdetail = CommonClass.ReplaceSingleQuotes(logMessage.Serverdetail);
            logMessage.Summary = CommonClass.ReplaceSingleQuotes(logMessage.Summary);
            logMessage.Tag1 = CommonClass.ReplaceSingleQuotes(logMessage.Tag1);
            logMessage.Tag2 = CommonClass.ReplaceSingleQuotes(logMessage.Tag2);          

            logMessage.EventCategoryId = CommonClass.GetCategoryId(model.Category);
            
            if(logMessage.EventCategoryId==0)
            {
                response.Message = "input category not defined in system.";
                response.StatusCode = "400";
                return response;
            }

            // Getting attached file
            var attachedFile = model.AttachedFile;            

            // Saving attached file on server
            if (attachedFile !=null)
            {
                //modify file name with data                 
                var fileName = Path.GetFileNameWithoutExtension(attachedFile.FileName);
                var extension = Path.GetExtension(attachedFile.FileName);
                fileName = fileName + '_' + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + extension; 
                logMessage.Attachment = fileName;                

                //saving data into database
                 logMessagesRepository.SaveLogMessage(logMessage);
                 response.Message = "success"; response.StatusCode = "200";
                //file saving to s3 bucket.      
                CommonClass.UploadFileToS3(attachedFile, fileName,awsAccessKey,awsSecreteKey, bucketName);               
            }
            else
            {
                //saving data into database
                logMessagesRepository.SaveLogMessage(logMessage);
                response.Message = "success"; response.StatusCode = "200";
            }
            return response;
        }

        /// <summary>
        /// Get top five events
        /// </summary>
        /// <returns></returns>
        public List<EventsDto> GetTop5Events(string categoryId, string serverDetail)
        {
            var logmessages = logMessagesRepository.GetTop5Events(categoryId, serverDetail);
            return mapper.Map<List<EventsDto>>(logmessages);
        }

        /// <summary>
        /// Get master categories
        /// </summary>
        /// <returns></returns>
        public List<MasterCategoriesDto> GetMasterCategories()
        {
            var logmessages = logMessagesRepository.GetMasterCategories();
            return mapper.Map<List<MasterCategoriesDto>>(logmessages);
        }
        
        #endregion
    }
}
