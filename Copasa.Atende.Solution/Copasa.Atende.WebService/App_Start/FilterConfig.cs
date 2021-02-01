using System.Web;
using System.Web.Mvc;

namespace Copasa.Atende.WebService
{
    /// <summary>
    /// Classe FilterConfig.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// RegisterGlobalFilters
        /// </summary>
        /// <param name="filters">GlobalFilterCollection</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
