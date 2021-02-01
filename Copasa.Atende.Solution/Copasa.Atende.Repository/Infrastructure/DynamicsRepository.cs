using Copasa.Atende.Model;
using Copasa.Atende.Model.Core;
using Copasa.Atende.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface DynamicsRepository
    /// </summary>
    public abstract class DynamicsRepository<T> : IDynamicsRepository<T> where T : BaseModel
    {
        private int TAREFA_ASYNC_ALTERAR = 0;
        //private int TAREFA_ASYNC_INCLUIR = 0;

        private int _tarefaExecucaoAsync;
        private T _baseModelAsync;
        private ILog _log = null;

        private MPToken _token = null;

        /// <summary>
        /// Construtor
        /// </summary>
        public DynamicsRepository(string nomeTabela, string host, Dyn365Bind bind)
        {
            _nomeTabela = nomeTabela;
            _bind = bind;
            _host = host;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public DynamicsRepository(string nomeTabela, string host)
        {
            _nomeTabela = nomeTabela;
            _bind = null;
            _host = host;
        }

        /// <summary>
        /// Incluir registro em tabela do Dynamics 365
        /// </summary>
        public bool Incluir(T baseModel)
        {
            return Incluir(baseModel, null);
        }

        /// <summary>
        /// Incluir registro em tabela do Dynamics 365
        /// </summary>
        public bool Incluir(T baseModel, ILog log)
        {
            try
            {
                TratarEnvio(baseModel);
                _method = Method.POST;
                _log = log;
                return sendByPost(baseModel);
            }
            catch (Exception e)
            {
                if (_log != null)
                {
                    _log.AddLog("   Erro ao incluir registro no dynamics365:" + e.GetType().ToString() + " " + e.Message, true);
                    if (e.InnerException != null)
                        _log.AddLog("   Exception dynamics365:" + e.InnerException.Message, true);
                }
                _token = GeToken();
                return false;
            }
        }

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        public bool Atualizar(T baseModel)
        {
            return Atualizar(baseModel, null);
        }

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        public bool Atualizar(T baseModel, ILog log)
        {
            try
            {
                TratarEnvio(baseModel);
                _method = Method.PATCH;
                _log = log;
                return sendByPatch(baseModel.getValor(baseModel.getIdDyn365Name()), baseModel);
            }
            catch (Exception e)
            {
                if (_log != null)
                {
                    _log.AddLog("   Erro ao incluir registro no dynamics365:" + e.GetType().ToString() + " " + e.Message, true);
                    if (e.InnerException != null)
                        _log.AddLog("   Exception dynamics365:" + e.InnerException.Message, true);
                }
                _token = GeToken();
                return false;
            }
        }

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        public bool Atualizar(T baseModel, string valorBind, ILog log)
        {
            TratarEnvio(baseModel);
            _method = Method.PATCH;
            _log = log;
            _valorBind = valorBind;
            return sendByPatch(baseModel.getValor(baseModel.getIdDyn365Name()), baseModel);
        }

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        public void AtualizarAsync(T baseModel, ILog log)
        {
            AtualizarAsync(baseModel, null, log);
        }

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        public void AtualizarAsync(T baseModel, string valorBind, ILog log)
        {
            _valorBind = valorBind;
            _tarefaExecucaoAsync = TAREFA_ASYNC_ALTERAR;
            _baseModelAsync = baseModel;
            log = new Log();
            if (log != null)
            {
                _log = log;
                _log.IsAsync();
            }
            _token = GeToken();
            ThreadStart threadStart = new ThreadStart(Run);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        /// <summary>
        /// Executa algum método em outra thread
        /// </summary>
        public void Run()
        {
            bool sucesso = true;
            if (_tarefaExecucaoAsync == TAREFA_ASYNC_ALTERAR)
            {
                TratarEnvio(_baseModelAsync);
                List<T> resultPesquisa = null;
                string idDyn365 = _baseModelAsync.getValor(_baseModelAsync.getIdDyn365Name());
                if (idDyn365 == null || "".Equals(idDyn365))
                {
                    string nomeIdCopasa = _baseModelAsync.getDyn365IdCopasaName();
                    string nomeIdCopasaDynamics = _baseModelAsync.getDyn365IdCopasaNameDynamics();

                    if (nomeIdCopasa != null && !"".Equals(nomeIdCopasa))
                    {
                        string valorIdCopasa = _baseModelAsync.getValor(nomeIdCopasa);
                        if (valorIdCopasa != null && !"".Equals(valorIdCopasa))
                        {
                            resultPesquisa = selectListValues(nomeIdCopasaDynamics, valorIdCopasa);
                        }
                    }
                    if (resultPesquisa != null && resultPesquisa.Count > 0)
                    {
                        idDyn365 = resultPesquisa[0].getValor(_baseModelAsync.getIdDyn365Name());
                    }
                }
                if (idDyn365 != null && !"".Equals(idDyn365))
                {
                    _baseModelAsync.setValue(_baseModelAsync.getIdDyn365Name(), idDyn365);
                    _method = Method.PATCH;
                    sucesso = sendByPatch(_baseModelAsync.getValor(_baseModelAsync.getIdDyn365Name()), _baseModelAsync);
                }
                else
                {
                    _method = Method.POST;
                    sucesso = sendByPost(_baseModelAsync);
                }
            }
            if (!sucesso)
            {
                _log.AddLog("AtualizarAsync Dynamics");
                _log.AddLog("    " + getEnderecoConexao());
                _log.AddLog("    " + getDadosEnvio());
                _log.AddLog("    " + getDadosRetorno());
            }
            _log.PringLog();
        }

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        public bool DAtualizar(T baseModel)
        {
            return DAtualizar(baseModel, null);
        }

        /// <summary>
        /// Atualizar registro em tabela do Dynamics 365
        /// </summary>
        public bool DAtualizar(T baseModel, string valorBind)
        {
            TratarEnvio(baseModel);
            _method = Method.PATCH;
            _valorBind = valorBind;
            return DsendByPatch(baseModel.getValor(baseModel.getIdDyn365Name()), baseModel);
        }

        /// <summary>
        /// Executa serviço de uma tabela do Dynamics 365 e retorna uma instância da classe de retorno
        /// </summary>
        public BaseModel ExecutarServico(string nomeServico, T baseModelRequest, Type tipoRetorno)
        {
            var baseModelResponse = Activator.CreateInstance(tipoRetorno);
            if (nomeServico != null)
                _nomeTabela = _nomeTabela + "/" + nomeServico;
            TratarEnvio(baseModelRequest);
            _method = Method.POST;
            if (sendByPost(baseModelRequest))
            {
                string retornoPost = "";
                using (StreamReader reader = new StreamReader(_responseStream))
                {
                    retornoPost = reader.ReadToEnd();
                }
                dynamic data = JObject.Parse(retornoPost);
                Type objtype = baseModelResponse.GetType();
                foreach (PropertyInfo p in objtype.GetProperties())
                {
                    foreach (Attribute at in p.GetCustomAttributes(false))
                    {
                        if ("Dyn365NameAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365NameAttribute dyn365NameAttribute = (Dyn365NameAttribute)at;
                            string valor = (string)data.GetValue(dyn365NameAttribute.Name);
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                        else if ("Dyn365IdAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365IdAttribute dyn365IdAttribute = (Dyn365IdAttribute)at;
                            string valor = (string)data.GetValue(dyn365IdAttribute.Name);
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                        else if ("Dyn365DisplayBindAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365DisplayBindAttribute dyn365IdAttribute = (Dyn365DisplayBindAttribute)at;
                            string valor = (string)data.GetValue(dyn365IdAttribute.IdName + "@OData.Community.Display.V1.FormattedValue");
                            if (valor != null)
                                objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                        else if ("JsonPropertyAttribute".Equals(at.GetType().Name))
                        {
                            JsonPropertyAttribute jonPropertyAttribute = (JsonPropertyAttribute)at;
                            string valor = (string)data.GetValue(jonPropertyAttribute.PropertyName);
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                    }
                }
                sucesso = true;
                return (BaseModel)baseModelResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Executa serviço de uma tabela do Dynamics 365 e retorna uma instância da classe de retorno
        /// </summary>
        public BaseModel DExecutarServico(string nomeServico, T baseModelRequest, Type tipoRetorno)
        {
            var baseModelResponse = Activator.CreateInstance(tipoRetorno);
            if (nomeServico != null)
                _nomeTabela = _nomeTabela + "/" + nomeServico;
            TratarEnvio(baseModelRequest);
            _method = Method.POST;
            if (DsendByPost(baseModelRequest))
            {
                string retornoPost = "";
                using (StreamReader reader = new StreamReader(_responseStream))
                {
                    retornoPost = reader.ReadToEnd();
                }
                dynamic data = JObject.Parse(retornoPost);
                Type objtype = baseModelResponse.GetType();
                foreach (PropertyInfo p in objtype.GetProperties())
                {
                    foreach (Attribute at in p.GetCustomAttributes(false))
                    {
                        if ("Dyn365NameAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365NameAttribute dyn365NameAttribute = (Dyn365NameAttribute)at;
                            string valor = (string)data.GetValue(dyn365NameAttribute.Name);
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                        else if ("Dyn365IdAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365IdAttribute dyn365IdAttribute = (Dyn365IdAttribute)at;
                            string valor = (string)data.GetValue(dyn365IdAttribute.Name);
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                        else if ("Dyn365DisplayBindAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365DisplayBindAttribute dyn365IdAttribute = (Dyn365DisplayBindAttribute)at;
                            string valor = (string)data.GetValue(dyn365IdAttribute.IdName + "@OData.Community.Display.V1.FormattedValue");
                            if (valor != null)
                                objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                        else if ("JsonPropertyAttribute".Equals(at.GetType().Name))
                        {
                            JsonPropertyAttribute jonPropertyAttribute = (JsonPropertyAttribute)at;
                            string valor = (string)data.GetValue(jonPropertyAttribute.PropertyName);
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                        }
                    }
                }
                sucesso = true;
                return (BaseModel)baseModelResponse;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Pesquisar registros na tabela do Dynamics 365
        /// </summary>
        public List<T> Pesquisar(List<string> listaNomeCampo, List<string> listaValor)
        {
            return selectListValues(listaNomeCampo, listaValor);
        }

        /// <summary>
        /// Pesquisar registros na tabela do Dynamics 365
        /// </summary>
        public List<T> Pesquisar(string nomeCampo, string valor)
        {
            return selectListValues(nomeCampo, valor);
        }

        /// <summary>
        /// Pesquisar registros na tabela do Dynamics 365
        /// </summary>
        public List<T> Pesquisar(BaseModel baseModel)
        {
            StringBuilder listaCondicao = new StringBuilder();
            string textoAnd = "";
            Type objtype = baseModel.GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                foreach (Attribute at in p.GetCustomAttributes(false))
                {
                    if ("Dyn365NameAttribute".Equals(at.GetType().Name)
                        || "Dyn365IdCopasaAttribute".Equals(at.GetType().Name)
                        || "Dyn365IdAttribute".Equals(at.GetType().Name))
                    {
                        dynamic dyn365Attribute = null;

                        if ("Dyn365NameAttribute".Equals(at.GetType().Name))
                            dyn365Attribute = (Dyn365NameAttribute)at;
                        if ("Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                            dyn365Attribute = (Dyn365IdCopasaAttribute)at;
                        if ("Dyn365IdAttribute".Equals(at.GetType().Name))
                            dyn365Attribute = (Dyn365IdAttribute)at;
                        var tipo = objtype.GetProperty(p.Name).PropertyType;
                        if (tipo == typeof(string) || tipo == typeof(int) || tipo == typeof(long) || tipo == typeof(Nullable<DateTime>) || tipo == typeof(DateTime))
                        {
                            dynamic valor = baseModel.getValorAsObject(p.Name);
                            if (tipo == typeof(Nullable<DateTime>) || tipo == typeof(DateTime))
                                if (valor != null && ((DateTime)valor).Year > 1)
                                {
                                    listaCondicao.Append(textoAnd);
                                    listaCondicao.Append(dyn365Attribute.Name + " eq '" + valor + "'");
                                    textoAnd = " and ";
                                }
                            if (tipo == typeof(string))
                                if (valor != null && !"".Equals(valor.Trim()) && !"0".Equals(valor.Trim()))
                                {
                                    listaCondicao.Append(textoAnd);
                                    listaCondicao.Append(dyn365Attribute.Name + " eq '" + valor + "'");
                                    textoAnd = " and ";
                                }
                            if (tipo == typeof(int))
                            {
                                if (valor != 0)
                                {
                                    listaCondicao.Append(textoAnd);
                                    listaCondicao.Append(dyn365Attribute.Name + " eq " + valor);
                                    textoAnd = " and ";
                                }
                            }
                            if (tipo == typeof(long))
                            {
                                if (valor != 0)
                                {
                                    listaCondicao.Append(textoAnd);
                                    listaCondicao.Append(dyn365Attribute.Name + " eq " + valor);
                                    textoAnd = " and ";
                                }
                            }
                        }
                    }
                }
            }
            List<T> retorno = new List<T>();
            if (listaCondicao.Length > 0)
            {
                string condicao = "$filter="+listaCondicao.ToString();
                string retornoGet = getStringByGet(condicao);
                dynamic data = JObject.Parse(retornoGet);
                dynamic values = data.value;
                retornoListSelect = new List<string>();
                retornoSelect = new StringBuilder();
                foreach (JObject value in values)
                {
                    T baseModelRetorno = Activator.CreateInstance<T>();
                    Type objtypeRetorno = baseModelRetorno.GetType();
                    baseModelRetorno = (T)getBaseModelFromJObject(objtypeRetorno, value);
                    retorno.Add(baseModelRetorno);
                }
                sucesso = true;
            }
            return retorno;
        }

        /// <summary>
        /// Pesquisa lista no ws
        /// </summary>
        public List<BaseModel> DPesquisarLista(string filtro, Type tipoRetorno)
        {
            return DPesquisarLista(filtro, tipoRetorno, false);
        }
        
        /// <summary>
        /// Pesquisa lista no ws
        /// </summary>
        public List<BaseModel> DPesquisarLista(string filtro, Type tipoRetorno, bool incluirDescricaoBind)
        {
            var baseModelResponse = Activator.CreateInstance(tipoRetorno);
            sucesso = false;

            string retornoGet = getStringByGet(filtro, incluirDescricaoBind);
            dynamic data = JObject.Parse(retornoGet);
            dynamic values = data.value;
            retornoListSelect = new List<string>();
            retornoSelect = new StringBuilder();
            List<BaseModel> retorno = new List<BaseModel>();
            foreach (JObject value in values)
            {
                retorno.Add(getBaseModelFromJObject(tipoRetorno, value));
            }
            sucesso = true;
            return retorno;
        }

        /// <summary>
        /// Pesquisa um basemodel no ws
        /// </summary>
        public BaseModel DPesquisarObjeto(string filtro, Type tipoRetorno)
        {
            return DPesquisarObjeto(filtro, tipoRetorno, false);
        }

        /// <summary>
        /// Pesquisa um basemodel no ws
        /// </summary>
        public BaseModel DPesquisarObjeto(string filtro, Type tipoRetorno, bool incluirDescricaoBind)
        {
            var baseModelResponse = Activator.CreateInstance(tipoRetorno);
            sucesso = false;
            string retornoGet = getStringByGet(filtro, incluirDescricaoBind);
            dynamic data = JObject.Parse(retornoGet);
            dynamic values = data.value;
            retornoListSelect = new List<string>();
            retornoSelect = new StringBuilder();
            List<BaseModel> retorno = new List<BaseModel>();
            if (Enumerable.Count(values) > 0)
            {
                JObject value = values[0];
                baseModelResponse = getBaseModelFromJObject(tipoRetorno, value);
            }
            else return null;
            sucesso = true;
            return (BaseModel)baseModelResponse;
        }

        /// <summary>
        /// Fornece o conteúdo do retorno da chamada do método callWebService
        /// </summary>
        public string getDadosRetorno()
        {
            if (_dadosResponse != null)
            {
                return _dadosResponse;
            }
            else
                return "";
        }

        /// <summary>
        /// Fornece o conteúdo do que é enviado na chamada do método callWebService
        /// </summary>
        public string getDadosEnvio()
        {

            if (_dadosRequest != null)
            {
                return _dadosRequest;
            }
            else
                return "";
        }

        /// <summary>
        /// Fornece o endereço de chamada da conexão com o Dynamicas 365
        /// </summary>
        public string getEnderecoConexao()
        {

            if (_strUri != null && _strUri.Length > 0)
            {
                return _strUri;
            }
            else
                return "";
        }

        /// <summary>
        /// Busca o Token de acesso no Dynamics 365
        /// </summary>
        private MPToken getTokenDynamics365()
        {
            var contentToken = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["Dyn365ClientId"].ToString()),
                        new KeyValuePair<string, string>("client_secret", ConfigurationManager.AppSettings["Dyn365ClientSecret"].ToString()),
                        new KeyValuePair<string, string>("resource", ConfigurationManager.AppSettings["Dyn365Resource"].ToString()),
                        new KeyValuePair<string, string>("username", ConfigurationManager.AppSettings["Dyn365UserName"].ToString()),
                        new KeyValuePair<string, string>("password", ConfigurationManager.AppSettings["Dyn365Password"].ToString()),
                    });
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                string host = ConfigurationManager.AppSettings["Dyn365UrlToken"].ToString();
                client.BaseAddress = new Uri(host);
                var response = client.PostAsync("Token", contentToken).Result;
                var json = response.Content.ReadAsStringAsync().Result;
                MPToken mPToken = new MPToken();
                mPToken = (MPToken)BaseModel.jsonToBaseModel(json, mPToken);
                return mPToken;
            }
        }

        /// <summary>
        /// Retorna uma lista.
        /// </summary>
        private List<T> selectListValues(string campoFiltro, string valorFiltro)
        {
            List<string> listaCampoFiltro = new List<string>();
            List<string> listaValorFiltro = new List<string>();
            listaCampoFiltro.Add(campoFiltro);
            listaValorFiltro.Add(valorFiltro);
            return selectListValues(listaCampoFiltro, listaValorFiltro);
        }

        /// <summary>
        /// Select.
        /// </summary>
        private List<T> selectListValues(List<string> listaCampoFiltro, List<string> listaValorFiltro)
        {
            sucesso = false;
            StringBuilder listaCondicao = new StringBuilder();
            listaCondicao.Append("$filter=");
            string textoAnd = "";
            for (int i = 0; i < listaCampoFiltro.ToArray().Length; i++)
            {
                listaCondicao.Append(textoAnd);
                listaCondicao.Append(listaCampoFiltro[i] + " eq '" + listaValorFiltro[i] + "'");
                textoAnd = " and ";
            }
            string condicao = listaCondicao.ToString();

            string retornoGet = getStringByGet(condicao);
            dynamic data = JObject.Parse(retornoGet);
            dynamic values = data.value;
            retornoListSelect = new List<string>();
            retornoSelect = new StringBuilder();
            List<T> retorno = new List<T>();
            foreach (JObject value in values)
            {
                T baseModel = Activator.CreateInstance<T>();
                Type objtype = baseModel.GetType();
                baseModel = getBaseModelFromJObject(objtype, data);
                /*
                foreach (PropertyInfo p in objtype.GetProperties())
                {
                    foreach (Attribute at in p.GetCustomAttributes(false))
                    {
                        if ("Dyn365DisplayBindAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365DisplayBindAttribute dyn365IdAttribute = (Dyn365DisplayBindAttribute)at;
                            string valor = (string)value.GetValue(dyn365IdAttribute.IdName + "@OData.Community.Display.V1.FormattedValue");
                            if (valor != null)
                                objtype.GetProperty(p.Name).SetValue(baseModel, valor);
                        }
                        else if ("Dyn365NameAttribute".Equals(at.GetType().Name) || "Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                        {
                            dynamic dyn365Attribute = null;
                            if ("Dyn365NameAttribute".Equals(at.GetType().Name))
                                dyn365Attribute = (Dyn365NameAttribute)at;
                            else if ("Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                                dyn365Attribute = (Dyn365IdCopasaAttribute)at;
                            var type = objtype.GetProperty(p.Name).PropertyType;
                            dynamic valor = null;
                            if (type == typeof(string))
                                valor = (string)value.GetValue(dyn365Attribute.Name);
                            if (type == typeof(Int32))
                                valor = (Int32)value.GetValue(dyn365Attribute.Name);
                            if (valor != null)
                                objtype.GetProperty(p.Name).SetValue(baseModel, valor);
                        }
                        else if ("Dyn365IdAttribute".Equals(at.GetType().Name))
                        {
                            Dyn365IdAttribute dyn365IdAttribute = (Dyn365IdAttribute)at;
                            string valor = (string)value.GetValue(dyn365IdAttribute.Name);
                            objtype.GetProperty(p.Name).SetValue(baseModel, valor);
                        }
                    }
                }
                */
                retorno.Add(baseModel);
            }
            sucesso = true;
            return retorno;
        }

        /// <summary>
        /// Trata dados do envio pra o Dynamics 365
        /// </summary>
        protected abstract void TratarEnvio(T baseModel);



        /// <summary>
        /// Conectar com o sistema Dynamics através de web service
        /// </summary>
        private bool callWebService(string servico, string dados, string metodo)
        {
            return callWebService(servico, dados, metodo, false);
        }

        /// <summary>
        /// Conectar com o sistema Dynamics através de web service
        /// </summary>
        private bool callWebService(string servico, string dados, string metodo, bool incluirDescricaoBind)
        {
            errorCode = "";
            errorMessage = "";
            if (_log != null)
            {
                _log.AddLog("   Dados enviados ao dynamics365:" + dados, true);
            }

            try
            {
                if (_token == null)
                    _token = GeToken();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                _strUri = _host + servico;
                Uri uri = new Uri(_strUri);
                WebRequest request = WebRequest.Create(uri);
                if (incluirDescricaoBind)
                    request.Headers.Add("Prefer", "odata.include-annotations=OData.Community.Display.V1.FormattedValue");
                else
                    request.Headers.Add("Prefer", "return=representation");
                request.Headers.Add("Authorization", "Bearer " + _token.access_token);
                if (dados != null && !"".Equals(dados))
                {
                    _dadosRequest = dados;
                    request.Method = metodo;
                    request.ContentType = "application/json";
                    using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                    {
                        writer.Write(dados);
                    }
                }
                HttpWebResponse response = null;
                response = (HttpWebResponse)request.GetResponse();
                HttpStatusCode statusCode = response.StatusCode;
                _responseStream = response.GetResponseStream();
                if (HttpStatusCode.Created == statusCode || HttpStatusCode.OK == statusCode || HttpStatusCode.NoContent == statusCode)
                {
                    return true;
                }
                else
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(_responseStream))
                        {
                            _dadosResponse = reader.ReadToEnd();
                            if (_log != null)
                            {
                                _log.AddLog("   Retorno dynamics365:" + _dadosResponse,true);
                            }
                            _responseStream.Position = 0;
                        }
                    }
                    catch (Exception)
                    {
                        _dadosResponse = "";
                    }
                    return false;
                }
            }
            catch (WebException e)
            {
                _token = GeToken();
                using (WebResponse responseEx = e.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)responseEx;
                    using (Stream data = responseEx.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();
                        _dadosResponse = text;
                        try
                        {
                            dynamic dataErro = JObject.Parse(_dadosResponse);
                            dynamic error = dataErro.error;
                            JValue jVcode = error.code;
                            JValue jVmessage = error.message;
                            errorCode = jVcode.Value<string>();
                            errorMessage = jVmessage.Value<string>();
                        }
                        catch (Exception) { }
                    }
                }
                if (errorCode.Equals(ERRO_CODE_READ_ONLY))
                {
                    if (_log != null)
                    {
                        _log.AddLog(" Erro:" + errorMessage);
                    }
                    return true;
                }
                else
                {
                    if (_log != null)
                    {
                        _log.AddLog(" strUri:" + _strUri);
                        _log.AddLog(" dados:" + dados);
                        _log.AddLog(" errorCode:" + errorCode);
                        _log.AddLog(" WebException dynamics365:" + e.Message, true);
                        if (e.InnerException != null)
                            _log.AddLog("   InnerException:" + e.InnerException.Message, true);
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                if (_log != null)
                {
                    _log.AddLog(" Exception dynamics365:" + e.GetType().ToString() + " " + e.Message, true);
                    if (e.InnerException != null)
                        _log.AddLog("   InnerException:" + e.InnerException.Message, true);
                }
                _token = GeToken();
                return false;
            }
        }

        private MPToken GeToken()
        {
            MPToken mPToken = null;
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Application["TokenD365"] == null || HttpContext.Current.Application["TokenD365HoraAtualizacao"] == null)
                {
                    mPToken = getTokenDynamics365();
                    if (mPToken != null)
                    {
                        HttpContext.Current.Application["TokenD365"] = mPToken;
                        HttpContext.Current.Application["TokenD365HoraAtualizacao"] = DateTime.Now;
                        if (_log != null)
                            _log.AddLog("    Carregou Token Dynamics");
                    }
                }
                else
                {
                    bool desatualizado = false;
                    if (HttpContext.Current.Application["TokenD365HoraAtualizacao"] != null)
                    {
                        DateTime ultimaAtualizacao = (DateTime)HttpContext.Current.Application["TokenD365HoraAtualizacao"];
                        ultimaAtualizacao = ultimaAtualizacao.Add(new TimeSpan(0, 15, 0));
                        if (ultimaAtualizacao < DateTime.Now)
                            desatualizado = true;
                        if (desatualizado)
                        {
                            mPToken = getTokenDynamics365();
                            HttpContext.Current.Application["TokenD365"] = mPToken;
                            HttpContext.Current.Application["TokenD365HoraAtualizacao"] = DateTime.Now;
                            if (_log != null)
                                _log.AddLog("    Atualizou Token Dynamics");
                        }
                        else if (HttpContext.Current.Application["TokenD365"] != null)
                        {
                            mPToken = (MPToken)HttpContext.Current.Application["TokenD365"];
                        }
                    }
                }
            }
            else
            {
                mPToken = getTokenDynamics365();
                if (_log != null)
                    _log.AddLog("    Busca token do Dynamics",true);
            }

            return mPToken;
        }

        /// <summary>
        /// Send by post.
        /// </summary>
        private bool sendByPost(T content)
        {
            string stringJson = "";
            if (serializerJson(content, true, out stringJson))
                return callWebService(_nomeTabela, stringJson, _method);
            else
                return false;
        }

        /// <summary>
        /// Função custom para serializar valores vazios
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool DsendByPost(T content)
        {
            return callWebService(_nomeTabela, DserializerJson(content), _method);
        }

        /// <summary>
        /// Send by get.
        /// </summary>
        private string getStringByGet()
        {
            return getStringByGet("",false);
        }

        /// <summary>
        /// Send by get.
        /// </summary>
        private string getStringByGet(string parm)
        {
            return getStringByGet(parm,false);
        }

        /// <summary>
        /// Send by get.
        /// </summary>
        private string getStringByGet(string parm, bool incluirDescricaoBind)
        {
            string servico = _nomeTabela;
            if (!"".Equals(parm))
                servico = servico + "?" + parm;
            if (callWebService(servico, "", Method.GET, incluirDescricaoBind))
            {
                using (StreamReader reader = new StreamReader(_responseStream))
                {
                    return reader.ReadToEnd();
                }
            }
            else
                return null;
        }

        /// <summary>
        /// Send by patch.
        /// </summary>
        private bool sendByPatch(string chave, T content)
        {
            string servico = _nomeTabela;
            if (!"".Equals(chave))
                servico = servico + "(" + chave + ")";
            string stringJson = "";
            if (serializerJson(content, false, out stringJson))
                return callWebService(servico, stringJson, _method);
            else
                return false;
        }

        /// <summary>
        /// Send by patch dyn365(utiliza o DserializerJson).
        /// </summary>
        private bool DsendByPatch(string chave, T content)
        {
            string servico = _nomeTabela;
            if (!"".Equals(chave))
                servico = servico + "(" + chave + ")";
            bool resultado =  callWebService(servico, DserializerJson(content), _method);
            if (resultado)
            {
                using (StreamReader reader = new StreamReader(_responseStream))
                {
                    string r =  reader.ReadToEnd();
                }
            }
            return resultado;
        }

        /// <summary>
        /// função custom para habilitar o repasse de valores vazios
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string DserializerJson(BaseModel content)
        {
            string stringJson = "";
            serializerJson(content, false,out stringJson);
            return stringJson;
        }

        private void readPropertyInfoField(PropertyInfo p, BaseModel content, bool atualizaBind, StringBuilder retorno, Type objtype)
        {
            string virgula = "";
            if (retorno.Length > 1)
                virgula = ",";
            var tipo = p.PropertyType;
            foreach (Attribute at in p.GetCustomAttributes(false))
            {
                if ("Dyn365NameAttribute".Equals(at.GetType().Name) || "Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                {
                    dynamic dyn365Attribute = null;
                    if ("Dyn365NameAttribute".Equals(at.GetType().Name))
                        dyn365Attribute = (Dyn365NameAttribute)at;
                    else if ("Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                        dyn365Attribute = (Dyn365IdCopasaAttribute)at;
                    if (dyn365Attribute != null)
                    {
                        if (tipo == typeof(string) || tipo == typeof(int) || tipo == typeof(long) || tipo == typeof(Nullable<DateTime>) || tipo == typeof(DateTime))
                        {
                            dynamic valor = content.getValorAsObject(p.Name);
                            if (tipo == typeof(Nullable<DateTime>) || tipo == typeof(DateTime))
                                if (valor != null && ((DateTime)valor).Year > 1)
                                {
                                    retorno.Append(virgula + "\"" + dyn365Attribute.Name + "\":\"" + ((DateTime)valor).ToString("MM/dd/yyyy HH:mm:ss") + "\"");
                                    virgula = ",";
                                }
                            if (tipo == typeof(string))
                                if (valor != null && !"".Equals(valor.Trim()) && !"0".Equals(valor.Trim()))
                                {
                                    retorno.Append(virgula + "\"" + dyn365Attribute.Name + "\":\"" + (string)valor + "\"");
                                    virgula = ",";
                                }
                            if (tipo == typeof(int))
                            {
                                if (valor != 0)
                                {
                                    retorno.Append(virgula + "\"" + dyn365Attribute.Name + "\":" + (int)valor);
                                    virgula = ",";
                                }
                            }
                            if (tipo == typeof(long))
                            {
                                if (valor != 0)
                                {
                                    retorno.Append(virgula + "\"" + dyn365Attribute.Name + "\":" + (long)valor);
                                    virgula = ",";
                                }
                            }

                        }
                        else
                        {
                            if (tipo.FullName.Contains("Model"))
                            {
                                string stringJsonChild = "";
                                if (serializerJson((BaseModel)content.getValorAsObject(p.Name), atualizaBind, out stringJsonChild))
                                {
                                    retorno.Append(virgula + "\"" + dyn365Attribute.Name + "\":" + stringJsonChild);
                                    virgula = ",";
                                }
                            }
                        }
                    }
                }
                else if (atualizaBind || "Dyn365BindAttribute".Equals(at.GetType().Name))
                {
                    string valor = content.getValor(p.Name);
                    if (!"".Equals(valor) && !"0".Equals(valor))
                    {
                        Dyn365BindAttribute bindAttribute = (Dyn365BindAttribute)at;
                        retorno.Append(virgula + "\"" + bindAttribute.BindName + "@odata.bind\":\"" + bindAttribute.TableName + "(" + valor + ")\"");
                        virgula = ",";
                    }
                }
                else if (atualizaBind || "Dyn365KeyBindAttribute".Equals(at.GetType().Name))
                {
                    string valor = content.getValor(p.Name);
                    if (!"".Equals(valor) && !"0".Equals(valor))
                    {
                        Dyn365KeyBindAttribute keyBindAttribute = (Dyn365KeyBindAttribute)at;
                        var fieldNamebind = objtype.GetProperty(keyBindAttribute.FieldBindName);
                        if (fieldNamebind != null)
                        {
                            string valorBind = content.getValor(fieldNamebind.Name);
                            if (valorBind == null || "".Equals(valorBind))
                            {
                                string nomeTabela = keyBindAttribute.TableName;
                                string servico = nomeTabela + "?$select=" + keyBindAttribute.ValueName + " &$filter=" + keyBindAttribute.KeyName + " eq '" + valor + "'";
                                if (callWebService(servico, "", Method.GET))
                                {
                                    using (StreamReader reader = new StreamReader(_responseStream))
                                    {
                                        string retornoGet = reader.ReadToEnd();
                                        dynamic data = JObject.Parse(retornoGet);
                                        dynamic values = data.value;
                                        foreach (JObject value in values)
                                        {
                                            valor = (string)value.GetValue(keyBindAttribute.ValueName);
                                            content.setValue(keyBindAttribute.FieldBindName, valor);
                                            readPropertyInfoField(fieldNamebind, content, atualizaBind, retorno,objtype);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool serializerJson(BaseModel content, bool atualizaBind, out string stringJson)
        {
            try
            {
                StringBuilder retorno = new StringBuilder();
                retorno.Append("{");
                Type objtype = content.GetType();
                foreach (PropertyInfo p in objtype.GetProperties())
                {
                    readPropertyInfoField(p, content, atualizaBind, retorno, objtype);
                }

                if (_bind != null && _valorBind != null)
                {
                    retorno.Append(",\"" + _bind.BindName + "@odata.bind\":\"" + _host + _bind.TableName + "(" + _valorBind + ")\"");
                }

                retorno.Append("}");
                stringJson = retorno.ToString();
                return true;
            }
            catch (Exception e)
            {
                if (_log != null)
                {
                    _log.AddLog("   Erro ao serializar model para envio ao dynamics365:" + e.GetType().ToString() + " " + e.Message, true);
                    if (e.InnerException != null)
                        _log.AddLog("   Exception dynamics365:" + e.InnerException.Message, true);
                }
                _token = GeToken();
                stringJson = "";
                return false;
            }
        }

        private BaseModel getBaseModelFromJObject(Type tipoRetorno, JObject value)
        {
            var baseModelResponse = Activator.CreateInstance(tipoRetorno);
            Type objtype = baseModelResponse.GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                foreach (Attribute at in p.GetCustomAttributes(false))
                {
                    if ("Dyn365NameAttribute".Equals(at.GetType().Name) || "Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                    {
                        dynamic dyn365Attribute = null;
                        if ("Dyn365NameAttribute".Equals(at.GetType().Name))
                            dyn365Attribute = (Dyn365NameAttribute)at;
                        else if ("Dyn365IdCopasaAttribute".Equals(at.GetType().Name))
                            dyn365Attribute = (Dyn365IdCopasaAttribute)at;
                        var type = objtype.GetProperty(p.Name).PropertyType;
                        dynamic valor = null;

                        if (type == typeof(object))
                        {
                            valor = value.GetValue(dyn365Attribute.Name);
                        }
                        else
                        {
                            var valorTemp = value.GetValue(dyn365Attribute.Name);
                            if (valorTemp != null)
                            {
                                valor = (string)valorTemp;
                                if (type == typeof(string))
                                    valor = (string)valorTemp;
                                if (type == typeof(Int32))
                                    valor = (Int32)valorTemp;
                                if (type == typeof(DateTime) || (type == typeof(Nullable<DateTime>)))
                                {
                                    valor = value.GetValue(dyn365Attribute.Name) != null ? (DateTime)value.GetValue(dyn365Attribute.Name) : value.GetValue(dyn365Attribute.Name);
                                }
                            }
                        }
                        if (valor != null)
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                    }
                    else if ("Dyn365IdAttribute".Equals(at.GetType().Name))
                    {
                        Dyn365IdAttribute dyn365IdAttribute = (Dyn365IdAttribute)at;
                        string valor = (string)value.GetValue(dyn365IdAttribute.Name);
                        objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                    }
                    else if ("Dyn365DisplayBindAttribute".Equals(at.GetType().Name))
                    {
                        Dyn365DisplayBindAttribute dyn365IdAttribute = (Dyn365DisplayBindAttribute)at;
                        string valor = (string)value.GetValue(dyn365IdAttribute.IdName + "@OData.Community.Display.V1.FormattedValue");
                        if (valor != null)
                            objtype.GetProperty(p.Name).SetValue(baseModelResponse, valor);
                    }
                }
            }
            return (BaseModel)baseModelResponse;

        }

        /// <summary>
        /// retornoListSelect
        /// </summary>
        public List<string> retornoListSelect { get; set; }

        /// <summary>
        /// retornoSelect
        /// </summary>
        public StringBuilder retornoSelect { get; set; }

        /// <summary>
        /// Sucesso
        /// </summary>
        public bool sucesso { get; set; }

        /// <summary>
        /// Nome tabela dynamics.
        /// </summary>
        private string _nomeTabela;

        /// <summary>
        /// Method de chamada ao dynamics.
        /// </summary>
        private string _method;

        /// <summary>
        /// Host de chamada ao dynamics.
        /// </summary>
        private string _host;

        /// <summary>
        /// Dados enviados ao Dynamics 365 na chamada do WS.
        /// </summary>
        private string _dadosRequest;

        /// <summary>
        /// Bind com outra tabela.
        /// </summary>
        private Dyn365Bind _bind;

        /// <summary>
        /// Valor bind.
        /// </summary>
        private string _valorBind;

        /// <summary>
        /// ResponseStream.
        /// </summary>
        private Stream _responseStream;

        /// <summary>
        /// DadosResponse.
        /// </summary>
        private string _dadosResponse;

        /// <summary>
        /// Uri da conexão.
        /// </summary>
        private string _strUri;

        /// <summary>
        /// ErrorCode
        /// </summary>
        public string errorCode { get; set; }

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string errorMessage { get; set; }

        /// <summary>
        /// ERRO_CODE_READ_ONLY
        /// </summary>
        public readonly string ERRO_CODE_READ_ONLY = "0x8007110f";
    }
}
