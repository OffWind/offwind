using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Offwind.WebApp.Models.Mailing
{
    public sealed class MailingModel
    {
        public string Owner { set; get; }

        public List<string> Users { set; get; }

        [DisplayName("Select recipients")]
        public List<string> SelectedUsers { set; get; }

        [DisplayName("Subject")]
        public string Subject { set; get; }

        [DisplayName("Message text")]
        public string MsgBody { set; get; }

        public MailingModel()
        {
            Users = new List<string>();
            SelectedUsers = new List<string>();
        }
    }
}
