using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApplication
{
    /// <summary>
    /// Web Api config class
    /// </summary>
    public static class WebApiConfig
    {
        #region methods
        /// <summary>
        /// method for registering config routes
        /// </summary>
        /// <param name="config">Http configuartion</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
        #endregion methods
    }
}
