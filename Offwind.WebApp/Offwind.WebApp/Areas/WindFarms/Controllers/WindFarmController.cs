using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class WindFarmController : _BaseController
    {
        //
        // GET: /WindFarms/WindFarm/

        public ActionResult Add()
        {
            return View();
        }

    }
}
