using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.WebApp.Areas.WindFarms.Models;
using WebGrease.Css.Extensions;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class WindFarmController : _BaseController
    {
        public ActionResult List()
        {
            var m = new VWindFarmsHome();
            var sorted = _ctx.DWindFarms.Where(x => x.IsPublic || x.Author == User.Identity.Name).OrderByDescending(x => x.Rating);
            foreach (var db in sorted)
            {
                m.WindFarms.Add(VWindFarm.MapFromDb(db, User));
            }
            
            return View(m);
        }

        public ActionResult Details(Guid id, string returnTo = "")
        {
            var dWindFarm = _ctx.DWindFarms.First(n => n.Id == id);
            var model = VWindFarm.MapFromDb(dWindFarm, User);
            model.ReturnTo = returnTo;
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

        public ActionResult Copy(Guid id)
        {
            var dWindFarm = _ctx.DWindFarms.First(n => n.Id == id);
            var newId = CopyWindFarm(dWindFarm);

            return RedirectToAction("Details", "WindFarm", new { area = "WindFarms", id = newId });
        }

        public ActionResult AccessLevel(Guid id,bool isPublic)
        {
            var dWindFarm = _ctx.DWindFarms.First(n => n.Id == id);
            dWindFarm.IsPublic = isPublic;
            _ctx.SaveChanges();
            return RedirectToAction("List");
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
            RemoveAllTurbines(dWindFarm.Id);
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
            _ctx.DWindFarmTurbines.Where(x => x.WindFarmId == windFarmId).ForEach(x => _ctx.DWindFarmTurbines.DeleteObject(x));
            _ctx.SaveChanges();
            return Json("OK");
        }

        public JsonResult TurbineCoordinatesData(Guid windFarmId)
        {
            var dWindFarm = _ctx.DWindFarms.Single(wf => wf.Id == windFarmId);
            var arr = dWindFarm.DWindFarmTurbines.OrderBy(t => t.Number).Select(t => new { x=t.X,y= t.Y }).ToArray();
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TurbineCoordinatesSave(Guid windFarmId, List<decimal[]> turbines)
        {
            if (turbines == null) return Json("Bad model");

            _ctx.DWindFarmTurbines.Where(x => x.WindFarmId == windFarmId).ForEach(x => _ctx.DWindFarmTurbines.DeleteObject(x));

            for (var i = 0; i < turbines.Count; i++)
            {
                var item = new DWindFarmTurbine
                {
                    Id = Guid.NewGuid(),
                    WindFarmId = windFarmId,
                    Number = (int)turbines[i][0],
                    X = turbines[i][1],
                    Y = turbines[i][2]
                };

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
            db.IsPublic = false;
            _ctx.SaveChanges();
        }

        private Guid CopyWindFarm(DWindFarm copiedDb)
        {
            DWindFarm db;
            db = new DWindFarm();
            db.Id = Guid.NewGuid();
            db.Created = DateTime.UtcNow;
            db.Author = HttpContext.User.Identity.Name;

            _ctx.DWindFarms.AddObject(db);

            db.Updated = DateTime.UtcNow;
            var copiedDbName = copiedDb.Name;
            var copyNumber = 1;
            var dbName = string.Empty;
            while (true)
            {
                dbName = string.Format("{0} ({1})", copiedDbName, copyNumber);
                if (!_ctx.DWindFarms.Any(wf => (string.Compare(wf.Name, dbName, false) == 0)))
                {
                    break;
                }
                copyNumber++;
            }
            db.Name = dbName;
            db.Country = copiedDb.Country;
            db.Description = copiedDb.Description;
            db.GeoLat = copiedDb.GeoLat;
            db.GeoLng = copiedDb.GeoLng;
            db.TotalCapacity = copiedDb.TotalCapacity;
            db.UrlOfficial = copiedDb.UrlOfficial;
            db.UrlPublicWiki = copiedDb.UrlPublicWiki;
            db.IsPublic = copiedDb.IsPublic;
            //db.Rating
            //db.TurbineTypeId
            //_ctx.SaveChanges();

            _ctx.DWindFarmTurbines.Where(wft => wft.WindFarmId == copiedDb.Id).ForEach(
                wft =>
                {
                    _ctx.DWindFarmTurbines.AddObject(
                        new DWindFarmTurbine
                        {
                            Id = Guid.NewGuid(),
                            WindFarmId = db.Id,
                            Number = wft.Number,
                            X = wft.X,
                            Y = wft.Y,
                            Z = wft.Z
                        }
                    );
                }
            );
            _ctx.SaveChanges();

            return db.Id;
        }
    }
}
