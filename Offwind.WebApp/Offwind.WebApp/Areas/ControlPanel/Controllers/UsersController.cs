using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Offwind.WebApp.Areas.ControlPanel.Models;
using Offwind.WebApp.Filters;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Account;
using WebMatrix.Data;
using WebMatrix.WebData;
using System.Collections;
using System.IO;

namespace Offwind.WebApp.Areas.ControlPanel.Controllers
{
    [Authorize]
    public class UsersController : _BaseCmController
    {
        private List<UserModel> _model; 

        public ActionResult Index()
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            _model = _ctx.DUserProfiles.Select(x => new UserModel()
                                                        {
                                                            Id = x.UserId,
                                                            Name = x.UserName,
                                                        }).ToList();
            foreach(var usr in _model)
            {
                var membershipUser = Membership.GetUser(usr.Name);
                if (membershipUser != null)
                {
                    usr.CreateDate = membershipUser.CreationDate;
                    usr.LastVisit = membershipUser.LastActivityDate;
                    usr.SelectRoles(roles.GetRolesForUser(usr.Name));
                }
            }
            ViewBag.HTitle = "Users management";
            return View(_model);
        }
        
        public ActionResult Edit(int id)
        {
            var db = _ctx.DUserProfiles;
            var usr = Enumerable.FirstOrDefault(
                db.Where(x => x.UserId == id).Select(x => new UserModel()
                                                              {
                                                                  Id = x.UserId,
                                                                  Name = x.UserName,
                                                              }));
            if (usr != null)
            {
                var roles = (SimpleRoleProvider) Roles.Provider;
                usr.SelectRoles(roles.GetRolesForUser(usr.Name));
                usr.OldPassword = Membership.GeneratePassword(6, 0);
                usr.Password = usr.ConfirmPassword = usr.OldPassword;
                var ms = Enumerable.FirstOrDefault(_ctx.webpages_Membership.Where(x => x.UserId == id));
                if (ms != null)
                {
                    usr.Email = ms.Email;
                }
            }
            return View(usr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var roles = (SimpleRoleProvider)Roles.Provider;
                roles.RemoveUsersFromRoles(new[] {model.Name}, model.SelectedRoles.Split(';'));
                model.SelectRoles();
                roles.AddUsersToRoles(new[] {model.Name}, model.SelectedRoles.Split(';'));

                var membershipUser = Membership.GetUser(model.Name);
                if (membershipUser != null)
                {
                    if (model.Password != model.OldPassword)
                    {
                        var oldPas = membershipUser.GetPassword();
                        WebSecurity.ChangePassword(model.Name, oldPas, model.Password);
                    }
                    SetEmail(model.Id, model.Email);
                }               
                return RedirectToAction("Index");    
            }
            return View(model);
        }

        public ActionResult Add()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserModel model)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(model.Name, model.Password);
                var roles = (SimpleRoleProvider)Roles.Provider;
                model.SelectRoles();
                roles.AddUsersToRoles(new[] {model.Name}, model.SelectedRoles.Split(';'));
                var uid = WebSecurity.GetUserId(model.Name);
                SetEmail(uid, model.Email);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var usr = Enumerable.FirstOrDefault(_ctx.DUserProfiles.Where(x => x.UserId == id));
            if (usr != null)
            {
                Membership.DeleteUser(usr.UserName);
            }
            return Json("OK");
        }

        private void SetEmail(int id, string value)
        {
            var ms = Enumerable.FirstOrDefault(_ctx.webpages_Membership.Where(x => x.UserId == id));
            if (ms != null)
            {
                if (ms.Email != value)
                {
                    ms.Email = value;
                    _ctx.SaveChanges();
                }
            }            
        }
    }
}
