using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Copasa.Atende.Repository.Repositories
{
    /// <summary>
    /// API de serviços da Azure
    /// </summary>
    public class AzureRepository : IAzureRepository
    {
        private readonly string MsgErroConexao = "ERRO ao conectar com o serviço {0}: HTTP CODE {1} - {2}";

        /// <summary>
        /// Cria um Contato Novo no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_createDyn365PortalUserSend">CreateDyn365PortalUserSend</param>
        public BaseModelAzureCopaUserReceive CreateDyn365PortalUser(Dyn365CreatePortalUserSend _createDyn365PortalUserSend)
        {
            BaseModelAzureCopaUserReceive oReturn = new BaseModelAzureCopaUserReceive();
            string host = ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString();
            string url = host + "CreateDyn365PortalUser";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_createDyn365PortalUserSend), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oReturn = JsonConvert.DeserializeObject<BaseModelAzureCopaUserReceive>(responseBody);
                }
                else
                {
                    throw new Exception(String.Format(this.MsgErroConexao, "CreateDyn365PortalUser", response.StatusCode, responseBody));
                }
            }

            return oReturn;
        }

        /// <summary>
        /// Autentica um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_authenticateDyn365UserSend">AuthenticateDyn365UserSend</param>
        public BaseModelAzureCopaUserReceive AuthenticateDyn365User(Dyn365AuthenticateUserSend _authenticateDyn365UserSend)
        {
            BaseModelAzureCopaUserReceive oReturn = new BaseModelAzureCopaUserReceive();
            string host = ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString();
            string url = host + "AuthenticateDyn365User";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_authenticateDyn365UserSend), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oReturn = JsonConvert.DeserializeObject<BaseModelAzureCopaUserReceive>(responseBody);
                }
                else
                {
                    throw new Exception(String.Format(this.MsgErroConexao, "AuthenticateDyn365User", response.StatusCode, responseBody));
                }
            }

            return oReturn;
        }

        /// <summary>
        /// Altera a Senha de um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_changeDyn365UserPasswordSend">ChangeDyn365UserPasswordSend</param>
        public BaseModelAzureCopaUserReceive ChangeDyn365UserPassword(Dyn365ChangeUserPasswordSend _changeDyn365UserPasswordSend)
        {
            BaseModelAzureCopaUserReceive oReturn = new BaseModelAzureCopaUserReceive();
            string host = ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString();
            string url = host + "ChangeDyn365UserPassword";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_changeDyn365UserPasswordSend), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oReturn = JsonConvert.DeserializeObject<BaseModelAzureCopaUserReceive>(responseBody);
                }
                else
                {
                    throw new Exception(String.Format(this.MsgErroConexao, "ChangeDyn365UserPassword", response.StatusCode, responseBody));
                }
            }

            return oReturn;
        }

        /// <summary>
        /// Gera uma Nova Senha para um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_recoveryDyn365UserPasswordSend">RecoveryDyn365UserPasswordSend</param>
        public BaseModelAzureCopaUserReceive RecoveryDyn365UserPassword(Dyn365RecoveryUserPasswordSend _recoveryDyn365UserPasswordSend)
        {
            BaseModelAzureCopaUserReceive oReturn = new BaseModelAzureCopaUserReceive();
            string host = ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString();
            string url = host + "RecoveryDyn365UserPassword";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_recoveryDyn365UserPasswordSend), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oReturn = JsonConvert.DeserializeObject<BaseModelAzureCopaUserReceive>(responseBody);
                }
                else
                {
                    throw new Exception(String.Format(this.MsgErroConexao, "RecoveryDyn365UserPassword", response.StatusCode, responseBody));
                }
            }

            return oReturn;
        }

        /// <summary>
        /// Atualiza um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_updateDyn365PortalUserSend">UpdateDyn365PortalUserSend</param>
        public BaseModelAzureCopaUserReceive UpdateDyn365PortalUser(Dyn365UpdatePortalUserSend _updateDyn365PortalUserSend)
        {
            BaseModelAzureCopaUserReceive oReturn = new BaseModelAzureCopaUserReceive();
            string host = ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString();
            string url = host + "UpdateDyn365PortalUser";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_updateDyn365PortalUserSend), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oReturn = JsonConvert.DeserializeObject<BaseModelAzureCopaUserReceive>(responseBody);
                }
                else
                {
                    throw new Exception(String.Format(this.MsgErroConexao, "UpdateDyn365PortalUser", response.StatusCode, responseBody));
                }
            }

            return oReturn;
        }

        /// <summary>
        /// Associa um Identificador com um Contato no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_associateDyn365IdentifierXUserSend">AssociateDyn365IdentifierXUserSend</param>
        public BaseModelAzureCopaUserReceive AssociateDyn365IdentifierXUser(Dyn365AssociateIdentifierXUserSend _associateDyn365IdentifierXUserSend)
        {
            BaseModelAzureCopaUserReceive oReturn = new BaseModelAzureCopaUserReceive();
            string host = ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString();
            string url = host + "AssociateDyn365IdentifierXUser";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_associateDyn365IdentifierXUserSend), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oReturn = JsonConvert.DeserializeObject<BaseModelAzureCopaUserReceive>(responseBody);
                }
                else
                {
                    throw new Exception(String.Format(this.MsgErroConexao, "AssociateDyn365IdentifierXUser", response.StatusCode, responseBody));
                }
            }

            return oReturn;
        }

        /// <summary>
        /// Altera o Status do [Identificador do Contato] no Microsoft Dynamics 365.
        /// </summary>
        /// <param name="_changeDyn365ControllerIdentifierStatusSend">ChangeDyn365ControllerIdentifierStatusSend</param>
        public BaseModelAzureCopaUserReceive ChangeDyn365ControllerIdentifierStatus(Dyn365ChangeControllerIdentifierStatusSend _changeDyn365ControllerIdentifierStatusSend)
        {
            BaseModelAzureCopaUserReceive oReturn = new BaseModelAzureCopaUserReceive();
            string host = ConfigurationManager.AppSettings["Dyn365HostAuthenticate"].ToString();
            string url = host + "ChangeDyn365ControllerIdentifierStatus";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_changeDyn365ControllerIdentifierStatusSend), Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;
                var responseBody = response.Content.ReadAsStringAsync().Result;

                if (response.IsSuccessStatusCode)
                {
                    oReturn = JsonConvert.DeserializeObject<BaseModelAzureCopaUserReceive>(responseBody);
                }
                else
                {
                    throw new Exception(String.Format(this.MsgErroConexao, "ChangeDyn365ControllerIdentifierStatus", response.StatusCode, responseBody));
                }
            }

            return oReturn;
        }
    }
}
