using Lab2._2Mark.Filters;
using System.Web;
using System.Web.Mvc;

namespace Lab2._2Mark
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogAttribute());
        }
    }
}
