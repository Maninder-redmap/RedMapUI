using Redmap.Events.Common;
using Redmap.Events.DTO;
using Redmap.Events.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Redmap.Events.Controllers
{
    /// <summary>
    /// Log Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]    
    public class LogController : ControllerBase
    {

        private readonly ILogMessagesService logMessagesService;
        /// <summary>
        /// Log Controller Constructer
        /// </summary>
        /// <param name="logMessagesService"></param>
        public LogController(ILogMessagesService logMessagesService)
        {
            this.logMessagesService = logMessagesService;            
        }

        /// <summary>
        /// For fetch log messages data with parameter.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>LogMessageModel</returns>
        [HttpGet("{categoryName}")]
        public ActionResult<List<EventsDto>> GetLogMessage(string categoryName)
        {
            int LogCategoryId = CommonClass.GetCategoryId(categoryName);
            List<EventsDto> logMessages = logMessagesService.GetLogMessages(LogCategoryId).ToList();
            return logMessages;
        }

        /// <summary>
        /// For fetch log messages data.
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<EventsDto>> GetLogMessage()
        {                       
            return logMessagesService.GetLogMessages().ToList();
        }

        /// <summary>
        /// Save log message data with attached file.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>logid</returns>
        [HttpPost]
        [Produces("application/json")]
       // [RequestSizeLimit(500000000)]
        public ActionResult AttachedFileWithData([FromForm] LogMessageDto model)
        {
           var response= logMessagesService.SaveLogMessage(model);
            return Ok(new { status = response.StatusCode, message = response.Message });
        }            

    }
}