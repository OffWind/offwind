using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Security;
using System.Text;
using System.Transactions;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Offwind.Web.Core;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Account;
using WebMatrix.WebData;

namespace Offwind.WebApp.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginModel());
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            model.RememberMe = true;

            //var d = WebSecurity.GeneratePasswordResetToken("victor");
            //WebSecurity.ResetPassword("ZRKyXNpvG7G6Ftx87jnh3g2", "1234567");

            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            var m = new RegisterModel();
            return View(m);
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Attempt to register the user
            var roles = (SimpleRoleProvider)Roles.Provider;
            if (!roles.RoleExists(SystemRole.Admin))
            {
                roles.CreateRole(SystemRole.Admin);
            }
            if (!roles.RoleExists(SystemRole.Partner))
            {
                roles.CreateRole(SystemRole.Partner);
            }
            if (!roles.RoleExists(SystemRole.User))
            {
                roles.CreateRole(SystemRole.User);
            }

            model.UserName = model.UserName.Trim();
            model.Password = model.Password.Trim();
            model.ConfirmPassword = model.ConfirmPassword.Trim();
            model.CompanyName = model.CompanyName == null ? "" : model.CompanyName.Trim();

            // Register user
            try
            {
                WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                WebSecurity.Login(model.UserName, model.Password);
                roles.AddUsersToRoles(new[] {model.UserName}, new[] {SystemRole.User});
            }
            catch (MembershipCreateUserException e)
            {
                ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                return View(model);
            }

            // Generate verification code 
            var verificationCode = Guid.NewGuid();
            var profile = _ctx.DUserProfiles.FirstOrDefault(p => p.UserName == model.UserName);
            profile.VerificationCode = verificationCode;
            profile.CompanyName = model.CompanyName;
            profile.FirstName = model.FirstName;
            profile.MiddleName = model.MiddleName;
            profile.LastName = model.LastName;
            profile.WorkEmail = model.WorkEmail;
            profile.WorkPhone = model.WorkPhone;
            profile.CellPhone = model.CellPhone;
            profile.AcademicDegree = model.AcademicDegree;
            profile.Position = model.Position;
            profile.Country = model.Country;
            profile.City = model.City;
            profile.Info = model.Info;
            _ctx.SaveChanges();

            // Send verification email to user
            try
            {
                var url = String.Format("{0}/account/verify/{1}",
                    WebConfigurationManager.AppSettings["AppHost"],
                    verificationCode);
                var anchor = String.Format("<a href=\"{0}\" target=\"_blank\">{0}</a>", url);
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress("admin@offwind.eu", "Offwind Administrator");
                    mail.To.Add(new MailAddress(model.UserName));
                    mail.Bcc.Add(new MailAddress("vlad.ogay@nrg-soft.com"));
                    //mail.Bcc.Add(new MailAddress("jafar.mahmoudi@iris.no"));
                    //mail.Bcc.Add(new MailAddress("jafar.mahmoudi@gmail.com"));
                    mail.Subject = "Offwind registration: verify your account";

                    var text = new StringBuilder();
                    text.AppendFormat("Welcome to Offwind!<br /><br />");
                    text.AppendFormat("You've registered a new account and verification is required in order to finish the process.<br /><br />");
                    text.AppendFormat("Your account: {0}<br />", model.UserName);
                    text.AppendFormat("Verification code: {0}<br />", verificationCode);
                    text.AppendFormat("You can simply follow the link: {0}<br /><br />", anchor);
                    text.AppendFormat("Best regards,<br />");
                    text.AppendFormat("Offwind group");
                    mail.Body = text.ToString();
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

            }
            catch (Exception)
            {
                // TODO: Add logging
                //throw;
            }
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("RegisterComplete", "Account");
        }

        [AllowAnonymous]
        public ActionResult RegisterComplete()
        {
            var m = new RegisterModel();
            m.UserName = User.Identity.Name;
            return View(m);
        }

        private bool UserWithCodeExists(string userName, Guid code)
        {
            var profile = _ctx.DUserProfiles.FirstOrDefault(p => p.UserName != userName && p.VerificationCode == code);
            return profile != null;
        }

        public ActionResult ChangePassword(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            //ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("ChangePassword");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(LocalPasswordModel model)
        {
            //bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            //ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("ChangePassword");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                return View(model);
            }

            // ChangePassword will throw an exception rather than return false in certain failure scenarios.
            bool changePasswordSucceeded;
            try
            {
                changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword,
                                                                     model.NewPassword);
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }

            if (!changePasswordSucceeded)
            {
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                return View(model);
            }

            return RedirectToAction("ChangePassword", new { Message = ManageMessageId.ChangePasswordSuccess });
        }

        public new ActionResult Profile(string userName)
        {
            var model = new VUserProfile();
            if (userName == null || userName.Trim().Length == 0)
            {
                userName = User.Identity.Name;
            }
            var profile = _ctx.DVUserProfiles.FirstOrDefault(p => p.UserName == userName);
            if (profile == null)
            {
                // This is unlikely to happen
                return View(model);
            }
            VUserProfile.MapFromDb(model, profile);
            //var roles = _ctx.VUserRoles.Where(r => r.UserId == mbr.UserId).Select(r => r.RoleName);
            //m.Roles.AddRange(roles);

            foreach (var dCase in _ctx.DCases.Where(c => c.Owner == User.Identity.Name))
            {
                model.Cases.Add(new VProfileCase {Created = dCase.Created, Name = dCase.Name, Id = dCase.Id});
            }

            ViewBag.IsOwner = User.Identity.Name == userName;

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Verify(Guid id)
        {
            var m = new VerifyModel();
            m.VerificationCode = id;
            var profile = _ctx.DUserProfiles.FirstOrDefault(p => p.VerificationCode == id);
            if (profile == null)
            {
                return View("VerifyNotFound", m);
            }

            m.UserName = profile.UserName;

            profile.IsVerified = true;
            _ctx.SaveChanges();
            return View(m);
        }

        public ActionResult EditProfile()
        {
            var model = new VUserProfile();

            var profile = _ctx.DVUserProfiles.FirstOrDefault(p => p.UserName == User.Identity.Name);
            if (profile == null)
            {
                // This is unlikely to happen
                return View(model);
            }

            VUserProfile.MapFromDb(model, profile);

            return View(model);
        }

        [HttpPost]
        [ActionName("EditProfile")]
        [ValidateInput(false)]
        public ActionResult EditProfileSave(VUserProfile model)
        {
            if (ModelState.IsValid)
            {
                var profile = _ctx.DUserProfiles.First(p => p.UserName == User.Identity.Name);
                profile.FirstName = model.FirstName ?? "";
                profile.MiddleName = model.MiddleName ?? "";
                profile.LastName = model.LastName ?? "";
                profile.CompanyName = model.CompanyName ?? "";
                profile.AcademicDegree = model.AcademicDegree ?? "";
                profile.Position = model.Position ?? "";
                profile.City = model.City ?? "";
                profile.Country = model.Country ?? "";
                profile.WorkEmail = model.WorkEmail ?? "";
                profile.CellPhone = model.CellPhone ?? "";
                profile.WorkPhone = model.WorkPhone ?? "";
                profile.Info = model.Info ?? "";
                _ctx.SaveChanges();

                return RedirectToAction("Profile", new { userName = User.Identity.Name });
            }

            return View("EditProfile", model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
