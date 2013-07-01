using System;
using System.Collections.Generic;
using System.Text;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Partners.Models
{
    public class VMeeting
    {
        public DateTime DateTime { get; set; }
        public string Type { get; set; }
        public List<VFile> Files { get; set; }
        public List<VPartner> Participants { get; set; }

        public VMeeting()
        {
            Files = new List<VFile>();
            Participants = new List<VPartner>();
        }

        public string FormattedParticipants()
        {
            var txt = new StringBuilder();
            foreach(var p in Participants)
            {
                if (txt.Length > 0) txt.Append("; ");
                txt.Append(p.UserName);
            }
            return txt.ToString();
        }
    }
}