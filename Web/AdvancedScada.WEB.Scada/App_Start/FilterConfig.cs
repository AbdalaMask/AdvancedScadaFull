using System.Web;
using System.Web.Mvc;

namespace AdvancedScada.WEB.Scada
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
