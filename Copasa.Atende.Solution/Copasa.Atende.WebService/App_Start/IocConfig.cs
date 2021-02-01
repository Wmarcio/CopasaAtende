using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Copasa.Atende.Business.Interfaces;
using Copasa.Atende.Business.Rules;
using Copasa.Atende.Facade.Facades;
using Copasa.Atende.Facade.Interfaces;
using Ninject;
using Ninject.Syntax;

namespace Copasa.Atende.WebService.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public class IocConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void ConfigurarDependencias()
        {
            //Cria o Container 
            IKernel kernel = new StandardKernel();

            //Instrução para mapear a interface IPessoa para a classe concreta Pessoa
            kernel.Bind<ICertidaoNegativaDebitoFacade>().To<CertidaoNegativaDebitoFacade>();
            kernel.Bind<ICertidaoNegativaDebitoRule>().To<CertidaoNegativaDebitoRule>();

            //Registra o container no ASP.NET
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kernel"></param>
        public NinjectDependencyResolver(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}