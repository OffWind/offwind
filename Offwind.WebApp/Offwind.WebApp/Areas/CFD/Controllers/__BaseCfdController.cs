using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;
using Offwind.OpenFoam.Sintef;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Account;
using log4net;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
// ReSharper disable InconsistentNaming
    [Authorize(Roles = SystemRole.RegularUser)]
    public class __BaseCfdController : Controller
    {
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected string Title;
        protected string ShortTitle;
        protected string SectionTitle;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Debug.Assert(Request.IsAuthenticated);

            var user = User.Identity.Name;
            using (var ctx = new OffwindEntities())
            {
                var dCase = ctx.DWorkCases.FirstOrDefault(c => c.Owner == user);
                if (dCase == null)
                {
                    // Init basic properties
                    dCase = new DWorkCase();
                    dCase.Id = Guid.NewGuid();
                    dCase.Name = String.Format("{0}'s case", user);
                    dCase.Owner = user;
                    dCase.Created = DateTime.UtcNow;

                    // Init model
                    var model = new SolverData();
                    var serializer = new XmlSerializer(typeof(SolverData));
                    using (var writer = new StringWriter())
                    {
                        serializer.Serialize(writer, model);
                        dCase.Model = writer.ToString();
                        writer.Close();
                    }

                    ctx.DWorkCases.AddObject(dCase);
                    ctx.SaveChanges();
                }
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.SectionTitle = SectionTitle;
            ViewBag.ShortTitle = ShortTitle ?? Title;
            ViewBag.Title = String.Format("{0} | {1} | CFD | Offwind", Title ?? ShortTitle, SectionTitle);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            _log.Error(filterContext.Exception);
        }
    }
    // ReSharper restore InconsistentNaming
}
