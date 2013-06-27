using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Mailing;
using WebMatrix.WebData;

namespace Offwind.WebApp.Controllers
{
    [Authorize]
    public class MailingController : BaseController
    {
        public ActionResult Mailing()
        {
            var model = new MailingModel();

            var d = _ctx.DUserProfiles
                .Where(x => x.UserName != WebSecurity.CurrentUserName)
                .Select(x => x.UserName);

            model.Users.AddRange(d);            
            return View(model);
        }

        [HttpPost]
        [ActionName("Mailing")]
        public ActionResult Send(MailingModel model)
        {
            var db = _ctx.webpages_Membership;

            var owner = Enumerable.FirstOrDefault(db.Where(x => x.UserId == WebSecurity.CurrentUserId));
            if (owner == null) return RedirectToAction("Mailing");

            using (var mail = new MailMessage())
            {
                mail.From = new MailAddress(owner.Email);
                foreach (var name in model.SelectedUsers)
                {
                    var uid = WebSecurity.GetUserId(name);
                    var ms = Enumerable.FirstOrDefault(db.Where(x => x.UserId == uid));
                    if (ms != null)
                    {
                        mail.To.Add(new MailAddress(ms.Email));
                    }
                }
                mail.Subject = model.Subject;
                mail.Body = model.MsgBody;
                mail.IsBodyHtml = true;

                var smtpClient = new SmtpClient()
                                     {
                                         Host = WebConfigurationManager.AppSettings["SmtpHost"],
                                         Port = Convert.ToInt32(WebConfigurationManager.AppSettings["SmtpHostPort"]),
                                         EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["SmtpEnableSSL"]),
                                         DeliveryMethod = SmtpDeliveryMethod.Network,
                                         UseDefaultCredentials = Convert.ToBoolean(WebConfigurationManager.AppSettings["SmtpUseDefaultCredentialas"]),
                                         Credentials = new NetworkCredential()
                                                           {
                                                               UserName = WebConfigurationManager.AppSettings["SmtpSenderMail"],
                                                               Password = WebConfigurationManager.AppSettings["SmtpSenderPswd"]
                                                           }
                                     };
                smtpClient.Send(mail);
            }
            return RedirectToAction("Mailing");
        }
    }
}
