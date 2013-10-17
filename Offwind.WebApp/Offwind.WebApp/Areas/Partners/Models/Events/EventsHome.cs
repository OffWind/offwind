using System.Collections.Generic;

namespace Offwind.WebApp.Areas.Partners.Models.Events
{
    public class EventsHome
    {
        public List<Event> Events { get; set; }
        public List<EventParticipant> Participants { get; set; }
    }
}