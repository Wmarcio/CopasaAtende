using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Enumerador;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Infrastructure
{

    /// <summary>
    /// Classe adaptada do broker 
    /// </summary>
    public class DBrokerRepository<T> : IDBrokerRepository<T> where T : class
    {

        /// <summary>
        /// Environment.
        /// </summary>
        private string _environment, _host;

        /// <summary>
        /// Propriedade Nome do Programa.
        /// </summary>
        private string NomePrograma { get; set; }


        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="nomePrograma"></param>
        public DBrokerRepository(string nomePrograma)
        {
            NomePrograma = nomePrograma;
            _host = ConfigurationManager.AppSettings.Get("HostBrokerHomol");
        }

        /// <summary>
        /// Download do Mainframe com parâmetros.
        /// </summary>
        /// <param name="parametros">Parâmetros.</param>
        /// <param name="ambiente">Ambiente.</param>
        /// <returns></returns>
        public string Download(string parametros, EnvironmentEnum ambiente)
        {
            try
            {
                SetEnvironment(ambiente);

                var url = MontarUrl(parametros);

                String proxyUser = Base64.Base64Decode(ConfigurationUtil.GetAppSetting("proxyUser"));

                String proxyPassword = Base64.Base64Decode(ConfigurationUtil.GetAppSetting("proxyPassword"));

                WebProxy proxy = new WebProxy();

                proxy.Credentials = new NetworkCredential(proxyUser, proxyPassword);

                var client = new WebClient();

                client.Proxy = proxy;

                var response = client.DownloadString(url);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Atribuir ambiente 
        /// </summary>
        /// <param name="ambiente"></param>
        /// <returns></returns>
        private void SetEnvironment(EnvironmentEnum ambiente)
        {
            switch (ambiente)
            {
                case EnvironmentEnum.H:
                    _environment = ConfigurationManager.AppSettings.Get("EnvironmentHomologacao");
                    break;
                case EnvironmentEnum.P:
                    _environment = ConfigurationManager.AppSettings.Get("EnvironmentProducao");
                    break;
            }
        }

        /// <summary>
        /// Montar Url.
        /// </summary>
        /// <param name="parametros">Parâmetros da URL</param>        
        /// <returns></returns>
        private string MontarUrl(string parametros)
        {
            return "http://" + _host + _environment + NomePrograma + parametros;
        }
    }
}
