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

        [HttpPost]
        public JsonResult AddTurbines(Guid windFarmId, int n)
        {
            if (n <= 0) return Json("Invalid argument N");
            var dWindFarm = _ctx.DWindFarms.Single(wf => wf.Id == windFarmId);
            var maxN = dWindFarm.DWindFarmTurbines.Select(wft => wft.Number).Concat(new[] {0}).Max();
            maxN++;
            for (int i = 0; i < n; i++)
            {
                _ctx.DWindFarmTurbines.AddObject(new DWindFarmTurbine { Id = Guid.NewGuid(), WindFarmId = windFarmId, Number = maxN++ });
            }
            _ctx.SaveChanges();
            return Json("OK");
        }

        [HttpPost]
        public JsonResult RemoveAllTurbines(Guid windFarmId)
        {
            _ctx.WindFarm_DeleteTurbines(windFarmId);
            return Json("OK");
        }

        public JsonResult TurbineCoordinatesData(Guid windFarmId)
        {
            var dWindFarm = _ctx.DWindFarms.Single(wf => wf.Id == windFarmId);
            var arr = dWindFarm.DWindFarmTurbines.OrderBy(t => t.Number).Select(t => new[] { t.X, t.Y }).ToArray();
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TurbineCoordinatesSave(Guid windFarmId, List<decimal[]> turbines)
        {
            if (turbines == null) return Json("Bad model");

            _ctx.WindFarm_DeleteTurbines(windFarmId);
            _ctx.SaveChanges();

            for (int i = 0; i < turbines.Count - 1; i++)
            {
                var item = new DWindFarmTurbine { Id = Guid.NewGuid(), WindFarmId = windFarmId };

                item.Number = i + 1;
                item.X = turbines[i][0];
                item.Y = turbines[i][1];
                _ctx.DWindFarmTurbines.AddObject(item);
            }
            _ctx.SaveChanges();
            return Json("OK");
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
