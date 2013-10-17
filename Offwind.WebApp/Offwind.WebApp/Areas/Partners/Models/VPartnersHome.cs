using System.Collections.Generic;
using Offwind.WebApp.Areas.Partners.Models.Events;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Partners.Models
{
    public class VPartnersHome : VWebPage
    {
        public List<VPartner> Partners { get; set; }
        public List<Event> Meetings { get; set; }

        public VPartnersHome()
        {
            Partners = new List<VPartner>();
            Meetings = new List<Event>();
        }
    }
}