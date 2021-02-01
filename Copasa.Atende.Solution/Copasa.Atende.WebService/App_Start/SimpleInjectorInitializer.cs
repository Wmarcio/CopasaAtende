/* Utilizando a linha abaixo, quando a aplicação for inicializada o IIS irá executar o método Initialize da classe, assim o Simple Injector será inicalizado corretamente.
    Para utilizá-la foi necessário instalar o WebActivatorEx */
//[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Copasa.Atende.WebService.App_Start.SimpleInjectorInitializer), "Initialize")]
namespace Copasa.Atende.WebService.App_Start
{
    using Bootstrapper;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using SimpleInjector.Lifestyles;
    using System.Reflection;
    using System.Web.Http;


    /// <summary>
    /// 
    /// </summary>
    public static class SimpleInjectorInitializer
    {
        /// <summary>
        /// 
        /// </summary>
        public static Container GetContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();
            return container;
            //GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            SimpleInjectorContainer.Register(container);
        }
    }
}