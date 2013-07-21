using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmitMapper;
using Offwind.WebApp.Areas.WindFarms.Models;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class WindFarmController : _BaseController
    {
        public ActionResult List()
        {
            var m = new VWindFarmsHome();

            foreach (var db in _ctx.DWindFarms)
            {
                m.WindFarms.Add(VWindFarm.MapFromDb(db, User));
            }
            return View(m);
        }

        public ActionResult Details(Guid id)
        {
            var dWindFarm = _ctx.DWindFarms.First(n => n.Id == id);
            var model = VWindFarm.MapFromDb(dWindFarm, User);
            return View(model);
        }

        public ActionResult Edit(Guid? id, string returnTo)
        {
            var model = new VWindFarm();
            model.ReturnTo = returnTo;
            if (id == null)
            {
            }
            else
            {
                var dWindFarm = _ctx.DWindFarms.First(n => n.Id == id);
                VWindFarm.MapFromDb(model, dWindFarm, User);
            }

            return View("Edit", model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult EditSave(VWindFarm model)
        {
            if (ModelState.IsValid)
            {
                SaveDB(model);
                if (model.ReturnTo == "list") return RedirectToAction("List", "WindFarm", new { area = "WindFarms" });
                return RedirectToAction("Details", "WindFarm", new { area = "WindFarms", id = model.Id });
            }

            return View("Edit", model);
        }

        public ActionResult Delete(Guid id, string returnTo)
        {
            var dWindFarm = _ctx.DWindFarms.Single(n => n.Id == id);
            var model = VWindFarm.MapFromDb(dWindFarm, User);
            model.ReturnTo = returnTo;
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(Guid id, string returnTo)
        {
            var dWindFarm = _ctx.DWindFarms.Single(n => n.Id == id);
            _ctx.DWindFarms.DeleteObject(dWindFarm);
            _ctx.SaveChanges();
            return RedirectToAction("List", "WindFarm", new { area = "WindFarms" });
        }

        private void SaveDB(VWindFarm model)
        {
            DWindFarm db;
            if (model.Id == Guid.Empty)
            {
                db = new DWindFarm();
                db.Id = Guid.NewGuid();
                db.Created = DateTime.UtcNow;
                db.Author = HttpContext.User.Identity.Name;

                model.Id = db.Id;

                _ctx.DWindFarms.AddObject(db);
            }
            else
            {
                db = _ctx.DWindFarms.First(n => n.Id == model.Id);
            }

            db.Updated = DateTime.UtcNow;
            db.Name = model.Name ?? "";
            db.Country = model.Country ?? "";
            db.Description = model.Description ?? "";
            db.GeoLat = model.GeoLat;
            db.GeoLng = model.GeoLng;
            db.TotalCapacity = model.TotalCapacity;
            db.UrlOfficial = model.UrlOfficial ?? "";
            db.UrlPublicWiki = model.UrlPublicWiki ?? "";

            _ctx.SaveChanges();
        }
    }
}
