using System.Web;
using System.Web.Mvc;

namespace hms.entappsettings.webapi
{
#pragma warning disable 1591
    public class FilterConfig
#pragma warning restore 1591
    {
#pragma warning disable 1591
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
#pragma warning restore 1591
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
