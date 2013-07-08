using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class TurbineController : Controller
    {
        //
        // GET: /WindFarms/Turbine/

        public ActionResult Add()
        {
            return View();
        }

    }
}
