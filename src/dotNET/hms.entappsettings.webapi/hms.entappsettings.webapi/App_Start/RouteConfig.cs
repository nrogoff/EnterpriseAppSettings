using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace hms.entappsettings.webapi
{
#pragma warning disable 1591
    public class RouteConfig
#pragma warning restore 1591
    {
#pragma warning disable 1591
        public static void RegisterRoutes(RouteCollection routes)
#pragma warning restore 1591
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
