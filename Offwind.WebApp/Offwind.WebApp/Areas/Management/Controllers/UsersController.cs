using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Offwind.WebApp.Areas.Management.Models;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class UsersController : _BaseController
    {
        private List<VUserProfile> _model;

        public ActionResult Index()
        {
            var model = new VUsersHome();
            model.Users = _ctx.DVUserProfiles.OrderBy(x => x.FirstName).Select(VUserProfile.MapFromDb).ToList();
            return View(model);
        }

        public ActionResult Details(string userName)
        {
            var model = _ctx.DVUserProfiles.FirstOrDefault(x => x.UserName == userName);
            return View(model);
        }

        public FileResult DownloadExcel()
        {
            return File(new byte[0], "");
        }
        //public ActionResult Edit(int id)
        //{
        //    var db = _ctx.DUserProfiles;
        //    var usr = Enumerable.FirstOrDefault(
        //        db.Where(x => x.UserId == id).Select(x => new VUserProfile()
        //                                                      {
        //                                                          Id = x.UserId,
        //                                                          UserName = x.UserName,
        //                                                      }));
        //    if (usr != null)
        //    {
        //        var roles = (SimpleRoleProvider) Roles.Provider;
        //        usr.SelectRoles(roles.GetRolesForUser(usr.UserName));
        //        usr.OldPassword = Membership.GeneratePassword(6, 0);
        //        usr.Password = usr.ConfirmPassword = usr.OldPassword;
        //    }
        //    return View(usr);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(VUserProfile model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var roles = (SimpleRoleProvider) Roles.Provider;
        //        roles.RemoveUsersFromRoles(new[] {model.UserName}, model.SelectedRoles.Split(';'));
        //        model.SelectRoles();
        //        roles.AddUsersToRoles(new[] {model.UserName}, model.SelectedRoles.Split(';'));

        //        var membershipUser = Enumerable.FirstOrDefault(_ctx.webpages_Membership.Where(x => x.UserId == model.Id));
        //        if (membershipUser != null)
        //        {
        //            if (model.Password != model.OldPassword)
        //            {
        //                var oldPas = membershipUser.Password;
        //                WebSecurity.ChangePassword(model.UserName, oldPas, model.Password);
        //            }
        //        }               
        //        return RedirectToAction("Index");    
        //    }
        //    return View(model);
        //}

        //public ActionResult Add()
        //{
        //    var model = new VUserProfile();
        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Add(VUserProfile model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
        //        var roles = (SimpleRoleProvider)Roles.Provider;
        //        model.SelectRoles();
        //        roles.AddUsersToRoles(new[] {model.UserName}, model.SelectedRoles.Split(';'));
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public JsonResult Delete(int id)
        //{
        //    var usr = Enumerable.FirstOrDefault(_ctx.DUserProfiles.Where(x => x.UserId == id));
        //    if (usr != null)
        //    {
        //        Membership.DeleteUser(usr.UserName);
        //    }
        //    return Json("OK");
        //}        

        public ActionResult Delete(int id)
        {
            var usr = Enumerable.FirstOrDefault(_ctx.DVUserProfiles.Where(x => x.UserId == id));
            if (usr == null)
            {
                throw new System.ArgumentException();
            }
            var model = VUserProfile.MapFromDb(usr);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(int id)
        {
            var usr = Enumerable.FirstOrDefault(_ctx.DUserProfiles.Where(x => x.UserId == id));
            if (usr == null || usr.UserName == User.Identity.Name)
            {
                throw new System.ArgumentException();
            }
            RemoveUser(usr);
            return RedirectToAction("Index", "Users", new { area = "Management" });
        }

        private void RemoveUser(Web.Core.DUserProfile usr)
        {
            var cases = _ctx.DCases.Where(c => c.Owner == usr.UserName).ToList();
            foreach (var case_ in cases)
            {
                _ctx.DCases.DeleteObject(case_);
            }

            var comments = _ctx.DComments.Where(c => c.Author == usr.UserName).ToList();
            foreach (var comment in comments)
            {
                _ctx.DComments.DeleteObject(comment);
            }

            var eventParticipants = _ctx.DEventParticipants.Where(ep => ep.UserId == usr.UserId).ToList();
            foreach (var eventParticipant in eventParticipants)
            {
                var eventParticipantComments = _ctx.DEventParticipantComments.Where(epc => epc.ParticipantId == eventParticipant.Id).ToList();
                foreach (var eventParticipantComment in eventParticipantComments)
                {
                    _ctx.DEventParticipantComments.DeleteObject(eventParticipantComment);
                }
                _ctx.DEventParticipants.DeleteObject(eventParticipant);
            }

            var jobs = _ctx.DJobs.Where(j => j.Owner == usr.UserName).ToList();
            foreach (var job in jobs)
            {
                _ctx.DJobs.DeleteObject(job);
            }

            var turbines = _ctx.DTurbines.Where(t => t.Author == usr.UserName).ToList();
            foreach (var turbine in turbines)
            {
                var turbineParameters = _ctx.DTurbineParameters.Where(tp => tp.TurbineId == turbine.Id).ToList();
                foreach (var turbineParameter in turbineParameters)
                {
                    _ctx.DTurbineParameters.DeleteObject(turbineParameter);
                }
                _ctx.DTurbines.DeleteObject(turbine);
            }

            var windFarms = _ctx.DWindFarms.Where(wf => wf.Author == usr.UserName).ToList();
            foreach (var windFarm in windFarms)
            {
                var windFarmTurbines = _ctx.DWindFarmTurbines.Where(wft => wft.WindFarmId == windFarm.Id).ToList();
                foreach (var windFarmTurbine in windFarmTurbines)
                {
                    _ctx.DWindFarmTurbines.DeleteObject(windFarmTurbine);
                }
                _ctx.DWindFarms.DeleteObject(windFarm);
            }

            var rolesProvider = (WebMatrix.WebData.SimpleRoleProvider)System.Web.Security.Roles.Provider;
            var userRoles = usr.webpages_Roles.Select(wp_r => wp_r.RoleName);
            rolesProvider.RemoveUsersFromRoles(new[] { usr.UserName }, userRoles.ToArray());

            System.Web.Security.Membership.DeleteUser(usr.UserName);

            _ctx.SaveChanges();

            //WebMatrix.WebData.WebSecurity.DeleteUserAndAccount(usr.UserName);
        }
    }
}
