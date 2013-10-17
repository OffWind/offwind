using System;
using System.Collections.Generic;
using System.Text;

namespace Offwind.WebApp.Areas.Partners.Models.Events
{
    public class Event
    {
        public DateTime DateTime { get; set; }
        public EventType Type { get; set; }
        public List<EventParticipant> Participants { get; set; }

        public Event()
        {
            Participants = new List<EventParticipant>();
        }

        public string FormattedParticipants()
        {
            var txt = new StringBuilder();
            foreach(var p in Participants)
            {
                if (txt.Length > 0) txt.Append("; ");
                //txt.Append(p.UserName);
            }
            return txt.ToString();
        }
    }
}