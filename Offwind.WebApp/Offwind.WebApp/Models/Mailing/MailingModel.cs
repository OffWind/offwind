using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Offwind.WebApp.Models.Mailing
{
    public sealed class MailingModel
    {
        public List<string> Users;

        [DisplayName("Select recipients")]
        public List<string> SelectedUsers { set; get; }

        [DisplayName("Message title")]
        public string Title { set; get; }

        [DisplayName("Message text")]
        public string MsgBody { set; get; }

        public MailingModel()
        {
            Users = new List<string>();
            SelectedUsers = new List<string>();
        }
    }
}
