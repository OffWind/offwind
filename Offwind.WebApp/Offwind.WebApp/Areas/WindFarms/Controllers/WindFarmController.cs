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

        [DisplayName("Edit")]
        public ActionResult Edit(Guid? id)
        {
            var model = new VWindFarm();
            if (id == null)
            {
                model.H1 = "New wind farm";
            }
            else
            {
                model.H1 = "Edit wind farm";
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
            DWindFarm dWindFarm;
            if (model.Id == Guid.Empty)
            {
                dWindFarm = new DWindFarm();
                dWindFarm.Id = Guid.NewGuid();
                dWindFarm.Created = DateTime.UtcNow;
                _ctx.DWindFarms.AddObject(dWindFarm);
            }
            else
            {
                dWindFarm = _ctx.DWindFarms.First(n => n.Id == model.Id);
            }

            dWindFarm.Updated = DateTime.UtcNow;
            dWindFarm.Name = model.Name ?? "";
            dWindFarm.Country = model.Country ?? "";
            dWindFarm.Description = model.Description ?? "";
            dWindFarm.GeoLat = model.GeoLat;
            dWindFarm.GeoLng = model.GeoLng;
            dWindFarm.TotalCapacity = model.TotalCapacity;
            dWindFarm.UrlOfficial = model.UrlOfficial ?? "";
            dWindFarm.UrlPublicWiki = model.UrlPublicWiki ?? "";

            _ctx.SaveChanges();
        }
    }
}
