using System.Web.Mvc;
using WebPortalMVCClient.Filters;

namespace WebPortalMVCClient
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new PortalExceptionFilter());
        }
    }
}