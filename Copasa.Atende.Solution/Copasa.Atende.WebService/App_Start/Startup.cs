using Copasa.Atende.WebService.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector.Integration.WebApi;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(Copasa.Atende.WebService.App_Start.Startup))]

namespace Copasa.Atende.WebService.App_Start
{
    /// <summary>
    /// Inicio
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuracoes: For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            // Initialise container
            var container = SimpleInjectorInitializer.GetContainer();
            // configuracao WebApi
            var config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            // configuração swagger
            SwaggerConfig.Register(config);
            // configuração web api
            WebApiConfig.Register(config);
            // ativando cors
            app.UseCors(CorsOptions.AllowAll);
            // ativando a geração do token
            AtivarGeracaoTokenAcesso(app);
            // ativando configuração WebApi
            app.UseWebApi(config);
        }

        /// <summary>
        /// Ativa a geracao de token de acesso.
        /// </summary>
        /// <param name="app"></param>
        private static void AtivarGeracaoTokenAcesso(IAppBuilder app)
        {
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1), // 1 dia
                Provider = new ProviderDeTokensDeAcesso()
            };
            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
