using System;
using System.Collections.Generic;

namespace Offwind.WebApp.Areas.Partners.Models.Events
{
    public class EventParticipant
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string RegisteredBy { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Organization { get; set; }
        public string Country { get; set; }
        public List<string> Comments { get; set; }
    }
}