namespace Copasa.Atende.WebService.Controllers
{
    using Copasa.Atende.Model;
    using Copasa.Atende.Model.Core;
    using Copasa.Atende.Util;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using System.Web.Script.Serialization;


    /// <summary>
    /// Classe base para todos os ApiControllers.
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Instancia da classe Log.
        /// </summary>
        protected ILog _log;

        /// <summary>
        /// Criar Objeto HttpResponseMessage a partir de um BaseResponse Inválido.
        /// </summary>
        /// <param name="baseResponse">Objeto BaseResponse Inválido.</param>
        /// <returns>Objeto HttpResponseMessage.</returns>
        protected HttpResponseMessage CreateResponseInvalid(BaseResponse baseResponse)
        {
            if (baseResponse.Messages.Any())
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Messages);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Message);
            }
        }

        /// <summary>
        /// conectarServidorviaViaPost.
        /// </summary>
        protected WebResponse conectarServidorviaViaPost(string servico, BaseModel objeto)
        {
            string host = ConfigurationManager.AppSettings["HostLocal"].ToString();
            int tempoTimeout = 20 * 60 * 1000;

            string strUri = host + servico;
            Uri uri = new Uri(strUri);
            WebRequest request = WebRequest.Create(uri);
            request.Timeout = tempoTimeout;
            //request.Method = "GET";
            
            JavaScriptSerializer jsonSerializer;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            jsonSerializer = new JavaScriptSerializer();
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(jsonSerializer.Serialize(objeto));
            }

            //WebResponse response = request.GetResponse();
            //return response.GetResponseStream();
            return request.GetResponse();
        }

        /// <summary>
        /// conectarServidorviaViaPost.
        /// </summary>
        protected Stream conectarServidorviaViaPost(string servico, string texto)
        {
            string host = ConfigurationManager.AppSettings["HostLocal"].ToString();
            int tempoTimeout = 20 * 60 * 1000;

            string strUri = host + servico;
            Uri uri = new Uri(strUri);
            WebRequest request = WebRequest.Create(uri);
            request.Timeout = tempoTimeout;
            //request.Method = "GET";

            JavaScriptSerializer jsonSerializer;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            jsonSerializer = new JavaScriptSerializer();
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(texto);
            }

            WebResponse response = request.GetResponse();
            return response.GetResponseStream();
        }

        /// <summary>
        /// Converte quebra de linha \n para formato html <br/>
        /// </summary>
        protected BaseModel converteQuebraPaginaHtml(BaseModel objeto)
        {
            Type _type = objeto.GetType();
            IEnumerable<string> propertiesNames = _type.GetProperties().Select(p => p.Name);
            foreach (PropertyInfo p in _type.GetProperties())
            {
                if ("System.String".Equals(p.PropertyType.ToString()))
                    {
                    string valor = (string)_type.GetProperty(p.Name).GetValue(objeto);
                    objeto.GetType().GetProperty(p.Name).SetValue(objeto, valor.Replace("\n", "<br/>"));                    
                }
            }
            return objeto;
        }

        /// <summary>
        /// Consiste parâmetro de entrada <br/>
        /// </summary>
        protected bool consisteEntrada (BaseModel entrada)
        {
            if (entrada == null)
            {
                throw new Exception("  Dados recebidos estão vazios ou em formato irregular");
                //return "Dados recebidos estão vazios ou em formato irrerular";
            }
            //entrada.validar();
            //entrada.retiraTexto("string");
            return true;
        }

        /// <summary>
        /// Trata o baseResponse retornado da camada facade <br/>
        /// </summary>
        protected Task<HttpResponseMessage> TrataRetornoFacade(Object facade, string method, BaseModel entrada)
        {
            return TrataRetornoFacade(facade, method, entrada, false);
        }
        /// <summary>
        /// Trata o baseResponse retornado da camada facade <br/>
        /// </summary>
        protected Task<HttpResponseMessage> TrataRetornoFacade(Object facade, string method,BaseModel entrada,bool printRetorno)
        {
            //StringBuilder entradas = new StringBuilder();
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                _log.SetEntrada();
                _log.SetNomeServico(GetNameCallMethod());
                _log.AddLog(GetClassNameCallMethod() + " " + GetNameCallMethod() + "-" + method + ": " + entrada?.ToString());
                try
                {
                    string origem = ((BaseModelSend)entrada).Origem;
                    if ("RAJADA".Equals(origem))
                        {
                        _log.IsRajada();
                    }
                }
                catch (Exception) { }
                consisteEntrada(entrada);
                /*
                try
                {
                    foreach (string valor in entrada.getValores())
                    {
                        //entradas.Append(valor);
                    }
                }
                catch (Exception e)
                {
                    _log.AddLog(" erro :" + e.Message);
                }
                */
                Type thisType = null;
                BaseResponse baseResponse = null;
                try
                {
                    thisType = facade.GetType();
                    MethodInfo theMethod = thisType.GetMethod(method);
                    baseResponse = (BaseResponse)theMethod.Invoke(facade, new Object[1] { entrada });
                }
                catch (TargetInvocationException ex)
                {
                    Exception exceptionMethodInvoke = ex.InnerException; // ex now stores the original exception
                    throw exceptionMethodInvoke;
                }
                catch (NullReferenceException)
                {
                    if (thisType != null)
                        throw new Exception("Método " + method + " inexistente na classe " + thisType.ToString());
                    else
                        throw new Exception("Método " + method + " inexistente");
                }
                if (baseResponse.IsValid)
                {
                    if (printRetorno)
                        _log.AddLog("    Retorno : " + baseResponse.Model.ToString());
                    response = Request.CreateResponse(HttpStatusCode.OK, baseResponse.Model);
                }
                else
                {
                    if (baseResponse.Messages.Any())
                    {
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Messages);
                    }
                    else
                    {
                        response = Request.CreateResponse(HttpStatusCode.InternalServerError, baseResponse.Message);
                    }
                }
                try
                {
                    string descricaoRetorno = ((BaseModelReceive)baseResponse.Model).descricaoRetorno;
                    if (!"".Equals(descricaoRetorno))
                    _log.AddLog(" Retorno ao Dynamics: " + descricaoRetorno);
                }
                catch (Exception) { }               

                _log.PringLog();
            }
            catch (Exception e)
            {
                var pegouMensagem = false;

                string mensagem = "Desculpe! Houve uma falha no processamento solicitado";
                if (e.Message.Length > 5 && "errois".Equals(e.Message.Substring(0,6).ToLower()))
                {
                    _log.setErroIS();
                    pegouMensagem = true;
                    mensagem = "Desculpe! O sistema comercial está indisponivel no momento";
                }
                else if (e.Message.Length > 10 && "errobroker".Equals(e.Message.Substring(0, 10).ToLower()))
                {
                    _log.setErroBroker();
                    pegouMensagem = true;
                    mensagem = "Desculpe! O sistema comercial está indisponivel no momento";
                }
                else if (e.Message.Length > 10 && "errooracle".Equals(e.Message.Substring(0, 10).ToLower()))
                {
                    _log.setErroOracle();
                    pegouMensagem = true;
                    mensagem = "Desculpe! Houve uma falha na comunicação com o portal";
                }
                else if (e.Message.Length > 6 && "errodyn".Equals(e.Message.Substring(0, 7).ToLower()))
                {
                    pegouMensagem = true;
                    mensagem = "Desculpe! Houve uma falha na comunicação com o portal";
                }
                else if (e.Message.Contains("Dados recebidos estão vazios ou em formato irregular"))
                {
                    pegouMensagem = true;
                    mensagem = "Dados recebidos estão vazios ou em formato irregular";
                }

                _log.AddLog("    Erro "+ GetNameCallMethod(),true);
                string textoErro = e.Message;

                // é possível através de debug, verificar na linha acima o erro original 
                // retornado do SICOM 
                // abaixo uma edição da mensagem a tornando mais amigável ao usuário TASK 834 
                if (!pegouMensagem)
                {
                   textoErro = "Houve um erro inesperado. Tente novamente mais tarde ou entre em contato com o Fale conosco no 115";
                }                  
                else
                {
                    textoErro = mensagem;
                }

                if ("System.Exception".Equals(e.GetType().ToString()))
                {
                    _log.AddLog("    " + e.Message);
                }
                else
                {
                    if (e.InnerException != null && !"".Equals(e.InnerException.Message))
                        textoErro = textoErro + " - " + e.InnerException.Message;
                    _log.AddLog("    " + e.GetType().ToString() + ": " + textoErro);
                }
                string userIdIS = ConfigurationManager.AppSettings["UserIdIS"].ToString();
                if (!"BRKRPCPR".Equals(userIdIS))
                {
                    mensagem = "ERRO COPASA: " + textoErro;
                }
                string idLog = _log.PringLog();
                if (!"".Equals(idLog))
                    mensagem = mensagem + " (" + idLog + ")";
                Dyn365SomenteMensagem baseModel = new Dyn365SomenteMensagem();
                baseModel.descricaoRetorno = mensagem;
                response = Request.CreateResponse(HttpStatusCode.OK, baseModel);
            }
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }

        /// <summary>
        /// Retorna nome do método anterior <br/>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetNameCallMethod()
        {
            string nome = "";
            try
            {
                var st = new StackTrace();
                for (int i = 0; i < 5; i++)
                {
                    var sf = st.GetFrame(i);
                    nome = sf.GetMethod().Name;
                    if (!"TrataRetornoFacade".Equals(nome) && !"GetNameCallMethod".Equals(nome))
                        break;
                }
                return nome;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Retorna nome da classe do método anterior <br/>
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetClassNameCallMethod()
        {
            string nome = "";
            try
            {                
                var st = new StackTrace();
                for (int i = 0; i < 5; i++)
                {
                    var sf = st.GetFrame(i);
                    nome = sf.GetMethod().ReflectedType.Name;
                    if (!"BaseApiController".Equals(nome))
                        break;
                }
                return nome;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Retorna instancia da classe Log<br/>
        /// </summary>
        public ILog getLog()
        {
            return _log;
        }
    }
}
