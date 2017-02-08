using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using hms.entappsettings.model;

namespace hms.entappsettings.webapi
{
#pragma warning disable 1591
    public static class WebApiConfig
#pragma warning restore 1591
    {
#pragma warning disable 1591
        public static void Register(HttpConfiguration config)
#pragma warning restore 1591
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //// OData API routes
            //ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<AppSettingGroup>("AppSettingGroups");
            //builder.EntitySet<AppSetting>("AppSettings");
            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }
    }
}
