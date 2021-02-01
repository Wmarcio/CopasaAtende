using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using Swashbuckle.Examples;
using Copasa.Atende.WebService.App_Start;
using System.Net.Http;
using System;
using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace Copasa.Atende.WebService.App_Start
{
    /// <summary>
    /// Configuracao do Swagger
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Register
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "Copasa Atende Api");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.IgnoreObsoleteProperties();
                    c.OperationFilter<ExamplesOperationFilter>();
                    c.PrettyPrint();
                    c.RootUrl(req => GetRootUrl(req));
                    c.ApiKey("Token")
                     .Description("Bearer token")
                     .Name("Authorization")
                     .In("header");
                    c.DocumentFilter<AuthTokenDocumentFilter>();                    
                })
                .EnableSwaggerUi(c =>
                {
                    c.DocumentTitle("Atende Api - Documentação");
                    c.DocExpansion(DocExpansion.List);
                    c.EnableApiKeySupport("Authorization", "header");
                    c.DisableValidator();
                });
        }

        /// <summary>
        /// Monta a root url
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string GetRootUrl(HttpRequestMessage request)
        {
            var pathBase = request.GetOwinContext().Get<string>("owin.RequestPathBase");
            return new Uri(request.RequestUri, string.IsNullOrEmpty(pathBase) ? "/" : pathBase).ToString().TrimEnd('/');
        }

        /// <summary>
        /// GetXmlCommentsPath
        /// </summary>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\WebService.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AuthTokenDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Documentacao da autenticação via token
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiExplorer"></param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/token", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> { "Token" },
                    consumes = new List<string>
                    {
                        "application/x-www-form-urlencoded"
                    },
                    parameters = new List<Parameter> {
                        new Parameter
                        {
                            type = "string",
                            name = "grant_type",
                            required = true,
                            @in = "formData",
                            @default = "password"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "username",
                            required = true,
                            @in = "formData"
                        },
                        new Parameter
                        {
                            type = "string",
                            name = "password",
                            required = true,
                            @in = "formData"
                        }
                    }
                }
            });
        }
    }
}
