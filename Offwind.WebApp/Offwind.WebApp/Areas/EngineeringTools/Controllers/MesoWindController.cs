using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.Serialization;
using Offwind.WebApp.Areas.EngineeringTools.Models.MesoWind;
using Offwind.WebApp.Models;
using log4net;
using Offwind.Web.Core;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class MesoWindController : _BaseController
    {
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly DbSettings Settings = new DbSettings() { startLat = 0, showAll = ShowAll.yes, distance = 100 };

        private void ItemsCount(VMesoWind model)
        {
            if (model.UseSearchResults)
            {
                model.TotalCount = model.InterestingPoints.Count;
                model.MerraCount = model.InterestingPoints.Count(t => t.DatabaseId == (short)DbType.MERRA);
                model.FnlCount = model.InterestingPoints.Count(t => t.DatabaseId == (short)DbType.FNL);
            }
            else
            {
                model.TotalCount = _ctx.VSmallMesoscaleTabFiles.Count();
                model.FnlCount = _ctx.VSmallMesoscaleTabFiles.Count(t => t.DatabaseId == (int)DbType.FNL);
                model.MerraCount = _ctx.VSmallMesoscaleTabFiles.Count(t => t.DatabaseId == (int)DbType.MERRA);
            }
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Debug.Assert(Request.IsAuthenticated);

            var user = User.Identity.Name;
            var dCase = _ctx.DCases.FirstOrDefault(c => c.Owner == user && c.Name == StandardCases.MesoWind);
            if (dCase == null)
            {
                dCase = new DCase
                {
                    Id = Guid.NewGuid(),
                    Name = StandardCases.MesoWind,
                    Owner = user,
                    Created = DateTime.UtcNow
                };
                // Model contains points from both databases by default
                var model = new VMesoWind();
                ItemsCount(model);

                var serializer = new XmlSerializer(typeof (VMesoWind));
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, model);
                    dCase.Model = writer.ToString();
                    writer.Close();
                }
                _ctx.DCases.AddObject(dCase);
                _ctx.SaveChanges();
            }
            base.Initialize(requestContext);
        }

        private VMesoWind PopModel()
        {
            var dCase = _ctx.DCases.First(c => c.Owner == User.Identity.Name && c.Name == StandardCases.MesoWind);
            var serializer = new XmlSerializer(typeof (VMesoWind));
            using (var reader = new StringReader(dCase.Model))
            {
                return (VMesoWind) serializer.Deserialize(reader);
            }
        }

        private void PushModel(VMesoWind model)
        {
            var serializer = new XmlSerializer(typeof(VMesoWind));
            using (var writer = new StringWriter())
            {
                var dCase = _ctx.DCases.First(c => c.Owner == User.Identity.Name && c.Name == StandardCases.MesoWind);
                serializer.Serialize(writer, model);
                dCase.Model = writer.ToString();
                writer.Close();
                _ctx.SaveChanges();
            }
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Overview | Mesoscale Wind Characteristics | Offwind";
            return View(new VWebPage());
        }

        public ActionResult Database()
        {
            ViewBag.Title = "Database | Mesoscale Wind Characteristics | Offwind";
            var model = PopModel();
            ItemsCount(model);
            PushModel(model);

            Settings.DbType = model.DbType;
            ViewBag.TotalCount = model.TotalCount;
            ViewBag.FnlCount = model.FnlCount;
            ViewBag.MerraCount = model.MerraCount;

            return View(Settings);
        }

        [ActionName("Database")]
        [HttpPost]
        public ActionResult Search(DbSettings value)
        {
            var model = PopModel();
            Settings.startLat = value.startLat;
            Settings.startLng = value.startLng;
            Settings.distance = value.distance;

            model.UseSearchResults = true;
            model.InterestingPoints.AddRange(GetFiltered(Settings.startLat, Settings.startLng, Settings.distance * 1000));
            PushModel(model);
            return RedirectToAction("Database");
        }

        public JsonResult DatabaseSwitch(string id)
        {
            var model = PopModel();
            model.DbType = (DbType) Enum.Parse(typeof(DbType), id);
            PushModel(model);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reset()
        {
            var model = PopModel();
            model.UseSearchResults = false;
            model.InterestingPoints.Clear();
            //model.ImportedPoints.Clear();
            ItemsCount(model);
            PushModel(model);
            return RedirectToAction("Database");
        }

        public JsonResult SetPoint(string coord)
        {
            var model = PopModel();

            var val = coord.Split("(),".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var lat = Convert.ToDecimal(val[0]);
            var lng = Convert.ToDecimal(val[1]);

            foreach (var x in _ctx.DMesoscaleTabFiles.Where(x => (Math.Abs((double)(x.Latitude - lat)) < 1e-9) &&
                             (Math.Abs((double)(x.Longitude - lng)) < 1e-9)))
            {
                model.SelectedPoint = x;
                PushModel(model);
                break;
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public ActionResult PointPage(int id)
        {
            var m = new VPointPage();
            var tab = _ctx.DMesoscaleTabFiles.FirstOrDefault(t => t.Id == id);
            if (tab != null)
            {
                var imported = ImportFile(null, tab.Text);

                m.Lat = tab.Latitude;
                m.Lng = tab.Longitude;
                m.Db = (tab.DatabaseId == (short)DbType.FNL) ? "FNL" : "MERRA";
                
                // === //
                m.VelocityFreq = imported.VelocityFreq.Select(t => new object[] { t.Dir, t.Frequency }).ToArray();

                // === //
                m.WindRose = new IEnumerable<object[]>[2];
                var i = 0;
                m.WindRose[0] = imported.FreqByDirs.Select(t => new object[] { i++ * 360 / imported.NDirs, t });
                var j = 0;
                m.WindRose[1] = imported.MeanVelocityPerDir.Select(t => new object[] { j++ * 360 / imported.NDirs, t });
                
                // === //
                CurrentDataJson(imported, m);

                ViewBag.Title = String.Format("{0} ({1}; {2})", m.Db, tab.Latitude, tab.Longitude);
                return View(m);
            }
            return View(m);
        }

        public ActionResult Results()
        {
            return View();
        }

        public JsonResult CurrentDataJson(VDataImport imported, VPointPage vpp)
        {
            var model = PopModel();
            if (model.SelectedPoint == null)
            {
                _log.WarnFormat("CurrentFile is not set");
                var dataEmpty = new { sEcho = 0, iTotalRecords = 0, iTotalDisplayRecords = 0, aaData = new List<decimal[]>() };
                return Json(dataEmpty, JsonRequestBehavior.AllowGet);
            }

            //string DbDir = WebConfigurationManager.AppSettings["MesoWindTabDir" + Settings.DbType];
            //var imported = ImportFile(DbDir, model.SelectedPoint.Text);
            
            var final = new List<string[]>();
            var n = imported.NDirs + 1;

            var freqs = new string[n];
            freqs[0] = "Frequencies";
            for (var i = 0; i < imported.FreqByDirs.Count; i++)
            {
                freqs[i + 1] = imported.FreqByDirs[i].ToString();
            }
            final.Add(freqs);

            for (var bIndex = 0; bIndex < imported.FreqByBins.Count; bIndex++)
            {
                var bin = imported.FreqByBins[bIndex];
                var binWith13 = new string[n];
                binWith13[0] = (bIndex + 1).ToString();
                for (var i = 0; i < bin.Length; i++)
                {
                    binWith13[i + 1] = bin[i].ToString();
                }
                final.Add(binWith13);
            }

            var mean = new string[n];
            mean[0] = "Mean Vel.";
            for (var i = 0; i < imported.MeanVelocityPerDir.Count; i++)
            {
                mean[i + 1] = imported.MeanVelocityPerDir[i].ToString();
            }
            final.Add(mean);

            var data = new { sEcho = 0, iTotalRecords = imported.NBins, iTotalDisplayRecords = imported.NBins, aaData = final };
            vpp.iTotalRecords = imported.NBins;
            vpp.iTotalDisplayRecords = imported.NBins;
            vpp.Data = final;
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        private VDataImport ImportFile(string dir, string fileName)
        {
            var model = new VDataImport();
            using (var f = new StringReader(fileName))
            {
                var lineN = 0;
                while (true)
                {
                    var line = f.ReadLine();
                    if (line == null) break;
                    lineN++;
                    switch (lineN)
                    {
                        case 1:
                            break;
                        case 2:
                            var line2 = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            _log.InfoFormat("NBins parsing: [{0}]", line);
                            model.NBins = ParseInt(line2[2]);
                            _log.InfoFormat("NBins: {0}", model.NBins);
                            break;
                        case 3:
                            var line3 = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            model.NDirs = ParseInt(line3[0]);
                            _log.InfoFormat("NDirs: {0}", model.NDirs);
                            break;
                        case 4:
                            var line4 = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            foreach (var s in line4)
                            {
                                model.FreqByDirs.Add(ParseDecimal(s));
                            }
                            break;
                        default:
                            var line5N = line.Trim().Split("\t ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            Debug.Assert(line5N.Length == model.NDirs + 1); // 1st cell contains bin number
                            var tmp = new decimal[model.NDirs];
                            for (var i = 0; i < model.NDirs; i++)
                            {
                                tmp[i] = ParseDecimal(line5N[i + 1]);
                            }
                            model.FreqByBins.Add(tmp);
                            break;
                    }
                }
            }
            _log.Info("File import complete. Calculating...");

            // MeanVelocityPerDir
            model.MeanVelocityPerDir.AddRange(new decimal[model.NDirs]);
            for (var binIdx = 0; binIdx < model.NBins; binIdx++)
                for (var dirIdx = 0; dirIdx < model.NDirs; dirIdx++)
                {
                    var velocity = binIdx + 1;
                    model.MeanVelocityPerDir[dirIdx] += (decimal)(velocity * (double)model.FreqByBins[binIdx][dirIdx] / 1000);
                }

            // Velocity frequencies
            model.VelocityFreq.Clear();
            for (int binIdx = 0; binIdx < model.NBins; binIdx++)
            {
                decimal freq = 0;
                for (int dirIdx = 0; dirIdx < model.NDirs; dirIdx++)
                {
                    freq += model.FreqByBins[binIdx][dirIdx] / 1000 * model.FreqByDirs[dirIdx];
                }
                model.VelocityFreq.Add(new HPoint(binIdx, 0, freq));
            }
            _log.Info("Import&Calculations complete.");

            return model;
        }

        public JsonResult GetTableData(int sEcho, int iDisplayLength, int iDisplayStart)
        {
            var model = PopModel();
            List<VSmallMesoscaleTabFile> db = (model.UseSearchResults)
                                                   ? model.InterestingPoints
                                                   : _ctx.VSmallMesoscaleTabFiles.ToList();

            List<object[]> goodPoints =
                db.Where(t => (t.DatabaseId == (short) model.DbType) || (model.DbType == DbType.All)).Select(MapDatabaseItem).ToList();

            var filtered = goodPoints
                .Skip(iDisplayStart)
                .Take(iDisplayLength)
                .ToArray();

            var data =
                new
                {
                    sEcho,
                    iTotalRecords = goodPoints.Count,
                    iTotalDisplayRecords = goodPoints.Count,
                    aaData = filtered
                };
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetMapData()
        {
            var model = PopModel();
            object[][] filtered = null;
            List<VSmallMesoscaleTabFile> data = (model.UseSearchResults)
                                                   ? model.InterestingPoints
                                                   : _ctx.VSmallMesoscaleTabFiles.ToList();
            if (model.DbType == DbType.All)
                return Json(data.Select(MapDatabaseItem).ToArray(), JsonRequestBehavior.AllowGet);

            filtered =
                data.Where(t => (t.DatabaseId == (short) model.DbType) || (model.DbType == DbType.All))
                    .Select(MapDatabaseItem).ToArray();
            return Json(filtered, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<VSmallMesoscaleTabFile> GetFiltered(decimal lat, decimal lng, decimal allowedDistance)
        {
            foreach (var item in _ctx.VSmallMesoscaleTabFiles)
            {
                var sCoord = new GeoCoordinate((double)item.Latitude, (double)item.Longitude);
                var eCoord = new GeoCoordinate((double) lat, (double) lng);

                var distance = sCoord.GetDistanceTo(eCoord);
                if (distance > (double) allowedDistance) continue;
                yield return item;
            }
        }


        private static object[] MapDatabaseItem(VSmallMesoscaleTabFile x)
        {
            var db = x.DatabaseId == (int) DbType.FNL ? "FNL" : "MERRA";
            return new object[] {x.Id, x.Latitude, x.Longitude, db};
        }

        private int ParseInt(string input)
        {
            int ir;
            if (int.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out ir))
                return ir;
            decimal dr;
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out dr))
                return Convert.ToInt32(dr);
            _log.ErrorFormat("[ParseInt] Unable to parse '{0}'", input);
            return 0;
        }

        private decimal ParseDecimal(string input)
        {
            decimal dr;
            if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out dr))
                return dr;
            _log.ErrorFormat("[ParseDecimal] Unable to parse '{0}'", input);
            return 0;
        }
    }
}
