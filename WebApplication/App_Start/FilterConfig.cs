using System.Web;
using System.Web.Mvc;

namespace WebApplication
{
    /// <summary>
    /// Filter config class
    /// </summary>
    public class FilterConfig
    {
        #region methods
        /// <summary>
        /// method for registering global filters
        /// </summary>
        /// <param name="filters">filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            if(!HttpContext.Current.IsDebuggingEnabled)
            {
                filters.Add(new RequireHttpsAttribute());
            }
            filters.Add(new HandleErrorAttribute());
        }
        #endregion methods
    }
}
