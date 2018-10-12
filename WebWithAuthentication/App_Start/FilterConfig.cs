using System.Web;
using System.Web.Mvc;

namespace WebWithAuthentication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new RequireHttpsAttribute()); //avoid to access http no s
        }
    }
}
