using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmitMapper;
using Offwind.WebApp.Areas.WindFarms.Models;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class TurbineController : _BaseController
    {
        public ActionResult List()
        {
            var m = new VWindFarmsHome();
            foreach (var db in _ctx.DTurbines)
            {
                m.Turbines.Add(VTurbine.MapFromDb(db, User));
            }
            return View(m);
        }

        public ActionResult Details(Guid id)
        {
            var db = _ctx.DTurbines.First(n => n.Id == id);
            var model = VTurbine.MapFromDb(db, User);
            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            var model = new VTurbine();
            if (id == null)
            {
            }
            else
            {
                var db = _ctx.DTurbines.First(n => n.Id == id);
                model = VTurbine.MapFromDb(db, User);
            }

            return View("Edit", model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult EditSave(VTurbine model)
        {
            if (ModelState.IsValid)
            {
                SaveDB(model);
                if (model.ReturnTo == "list") return RedirectToAction("List", "Turbine", new { area = "WindFarms" });
                return RedirectToAction("Details", "Turbine", new { area = "WindFarms", id = model.Id });
            }

            return View("Edit", model);
        }

        private void SaveDB(VTurbine model)
        {
            DTurbine db;
            var now = DateTime.UtcNow;
            if (model.Id == Guid.Empty)
            {
                db = new DTurbine();
                db.Id = Guid.NewGuid();
                db.Created = now;
                db.Author = HttpContext.User.Identity.Name;

                model.Id = db.Id;

                _ctx.DTurbines.AddObject(db);
            }
            else
            {
                db = _ctx.DTurbines.First(n => n.Id == model.Id);
            }

            db.Updated = now;
            db.Name = model.Name ?? "";
            db.Description = model.Description ?? "";
            db.Manufacturer = model.Manufacturer ?? "";
            db.RatedPower = model.RatedPower;
            db.RotorDiameter = model.RotorDiameter;
            db.RotorOrientation = model.RotorOrientation ?? "";
            db.RotorConfiguration = model.RotorConfiguration ?? "";
            db.Control = model.Control ?? "";
            db.HubHeight = model.HubHeight;
            db.HubDiameter = model.HubDiameter;
            db.WindSpeedCutIn = model.WindSpeedCutIn;
            db.WindSpeedRated = model.WindSpeedRated;
            db.WindSpeedCutOut = model.WindSpeedCutOut;
            db.RotorSpeedCutIn = model.RotorSpeedCutIn;
            db.RotorSpeedRated = model.RotorSpeedRated;
            db.TipSpeedRated = model.TipSpeedRated;
            db.RotorMass = model.RotorMass;
            db.NacelleMass = model.NacelleMass;
            db.TowerMass = model.TowerMass;

            _ctx.SaveChanges();
        }
    }
}