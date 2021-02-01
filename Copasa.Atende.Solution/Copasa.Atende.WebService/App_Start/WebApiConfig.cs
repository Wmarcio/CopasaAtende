using Newtonsoft.Json;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace Copasa.Atende.WebService
{
    /// <summary>
    ///  Web Api Config
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Método Register.
        /// </summary>
        /// <param name="config">HttpConfiguration</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;

            // Web API routes
            config.MapHttpAttributeRoutes();
            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */

            config.Routes.MapHttpRoute(
                name: "Swagger UI",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler(req => RootUrlResolver(req), "swagger/ui/index"));
            //SwaggerDocsConfig.DefaultRootUrlResolver
        }

        /// <summary>
        /// Resolve a root url
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string RootUrlResolver(HttpRequestMessage request)
        {
            var pathBase = request.GetOwinContext().Get<string>("owin.RequestPathBase");
            return new Uri(request.RequestUri, pathBase).ToString().TrimEnd('/');
        }
    }
}
