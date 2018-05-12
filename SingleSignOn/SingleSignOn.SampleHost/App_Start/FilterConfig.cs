using System.Web;
using System.Web.Mvc;

namespace SingleSignOn.SampleHost
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
