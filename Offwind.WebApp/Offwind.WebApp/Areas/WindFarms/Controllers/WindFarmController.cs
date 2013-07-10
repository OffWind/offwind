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
        public ActionResult Details(Guid id)
        {
            var wfMapper = ObjectMapperManager.DefaultInstance.GetMapper<DWindFarm, VWindFarm>();
            var dWindFarm = _ctx.DWindFarms.First(n => n.Id == id);
            var model = wfMapper.Map(dWindFarm);
            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            var model = new VWindFarm();
            if (id == null)
            {
            }
            else
            {
                var wfMapper = ObjectMapperManager.DefaultInstance.GetMapper<DWindFarm, VWindFarm>();
                var dWindFarm = _ctx.DWindFarms.First(n => n.Id == id);
                wfMapper.Map(dWindFarm, model);
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
                return RedirectToAction("Index", "HomeWindFarms", new { area = "WindFarms" });
            }

            return View("Edit", model);
        }

        [DisplayName("Delete")]
        public ActionResult Delete(Guid id, string type)
        {
            ViewBag.ContentType = type ?? "";
            var page = _ctx.DContents.Single(p => p.Id == id);
            return View(page);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(Guid id, string type)
        {
            var page = _ctx.DContents.Single(p => p.Id == id);
            _ctx.DContents.DeleteObject(page);
            _ctx.SaveChanges();
            return RedirectToAction("Index", new { type });
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
