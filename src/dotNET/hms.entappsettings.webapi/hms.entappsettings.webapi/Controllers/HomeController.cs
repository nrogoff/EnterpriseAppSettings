using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace hms.entappsettings.webapi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var thisAssembly = typeof(HomeController).Assembly;
            var version = thisAssembly.GetName().Version;
            ViewBag.Version = string.Format("Version: {0} - Environment: {1}", version.ToString(),
                WebConfigurationManager.AppSettings["Environment"]);

            return View();
        }
    }
}
