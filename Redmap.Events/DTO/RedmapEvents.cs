using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Redmap.Events.DTO
{
    /// <summary>
    /// Redmap events and top 5 events 
    /// </summary>
    public class RedmapEvents
    {
        /// <summary>
        /// Events
        /// </summary>
        public List<EventsDto> Events { get; set; }
        /// <summary>
        /// Top 5 events
        /// </summary>
        public List<EventsDto> Top5Events { get; set; }
    }
}
