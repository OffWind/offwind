using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Offwind.WebApp.Models.Mailing
{
    public sealed class MailingModel
    {
        public string Owner { set; get; }

        public List<string> Users { set; get; }

        [Display(Name = "Select recipients")]
        public List<string> SelectedUsers { set; get; }

        [Display(Name = "Subject")]
        public string Subject { set; get; }

        [Display(Name = "Message text")]
        public string MsgBody { set; get; }

        public MailingModel()
        {
            Users = new List<string>();
            SelectedUsers = new List<string>();
        }
    }
}
