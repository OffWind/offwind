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

namespace Offwind.WebApp.Areas.ControlPanel.Controllers
{
    [Authorize]
    public class UsersController : _BaseCmController
    {
        private List<UserModel> _model; 

        public ActionResult Index()
        {
            _model = _ctx.DUserProfiles.Select(x => new UserModel()
                                                        {
                                                            Id = x.UserId,
                                                            Name = x.UserName,
                                                            Role = x.webpages_UsersInRoles.webpages_Roles.RoleName,
                                                            CreateDate = (DateTime) x.webpages_Membership.CreateDate
                                                        }).ToList();
            ViewBag.HTitle = "Users management";
            return View(_model);
        }
        
        public ActionResult Edit(int id)
        {
            var db = _ctx.DUserProfiles;
            UserModel u = Enumerable.FirstOrDefault(
                db.Where(x => x.UserId == id).Select(x => new UserModel()
                                                              {
                                                                  Id = x.UserId,
                                                                  Name = x.UserName,
                                                                  Role = x.webpages_UsersInRoles.webpages_Roles.RoleName,
                                                              }));
            if (u != null)
            {                
                u.OldPassword = Membership.GeneratePassword(6, 0);
                u.Password = u.ConfirmPassword = u.OldPassword;
            }
            return View(u);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var db = _ctx.DUserProfiles;
                var profile = Enumerable.FirstOrDefault(db.Where(x => x.UserId == model.Id));
                if (profile != null)
                {
                    profile.webpages_Membership.PasswordChangedDate = DateTime.Now;
                    _ctx.SaveChanges();

                    var roles = (SimpleRoleProvider)Roles.Provider;
                    var userRole = model.RoleT.ToString();
                    if (!roles.IsUserInRole(model.Name, userRole))
                    {
                        roles.RemoveUsersFromRoles(new[] {model.Name}, new[] {model.Role});
                        model.Role = userRole;
                        roles.AddUsersToRoles(new[] {model.Name}, new[] {model.Role});
                    }
                    if (model.Password != model.OldPassword)
                    {
                        var oldPas = Convert.FromBase64String(profile.webpages_Membership.Password).ToString();
                        WebSecurity.ChangePassword(profile.UserName, oldPas, model.Password);
                    }
                }
                return RedirectToAction("Index");    
            }
            return View(model);
        }

        public ActionResult Add()
        {
            var model = new UserModel();
            model.RoleT = SystemRoleType.RegularUser;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(UserModel model)
        {
            if (ModelState.IsValid)
            {
                WebSecurity.CreateUserAndAccount(model.NewName, model.Password);
                var roles = (SimpleRoleProvider)Roles.Provider;
                roles.AddUsersToRoles(new[] {model.NewName}, new[] {model.RoleT.ToString()});

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var db = _ctx.DUserProfiles;
            var entity = Enumerable.FirstOrDefault(db.Where(x => x.UserId == id));
            db.DeleteObject(entity);
            return RedirectToAction("Index");
        }
    }
}
