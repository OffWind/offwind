using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Offwind.WebApp.Areas.ControlPanel.Models;
using WebMatrix.WebData;

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
            }
            return View(usr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var roles = (SimpleRoleProvider) Roles.Provider;
                roles.RemoveUsersFromRoles(new[] {model.Name}, model.SelectedRoles.Split(';'));
                model.SelectRoles();
                roles.AddUsersToRoles(new[] {model.Name}, model.SelectedRoles.Split(';'));

                var membershipUser = Enumerable.FirstOrDefault(_ctx.webpages_Membership.Where(x => x.UserId == model.Id));
                if (membershipUser != null)
                {
                    if (model.Password != model.OldPassword)
                    {
                        var oldPas = membershipUser.Password;
                        WebSecurity.ChangePassword(model.Name, oldPas, model.Password);
                    }
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
    }
}
