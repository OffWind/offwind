using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offwind.WebApp.Controllers
{
    public class CommonController : BaseController
    {
        public JsonResult Countries(string query)
        {
            var countries = _ctx.DCountries.Where(c=> c.Name.Contains(query)).Select(c => c.Name).ToArray();
            return Json(countries, JsonRequestBehavior.AllowGet);
        }
    }
}
