using Copasa.Atende.Model.Core;
using Copasa.Atende.Util;
using Copasa.Util;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Class ISRepository
    /// </summary>
    public abstract class ISRepository<T> : IISRepository<T> where T : BaseModelReceive
    {
        /// <summary>
        /// Class ISRepository
        /// </summary>
        private string _textoXml;

        /// <summary>
        /// Class Log
        /// </summary>
        private ILog log;

        /// <summary>
        /// Propriedade descrição do Servico.
        /// </summary>
        private string DescricaoServico { get; set; }

        /// <summary>
        /// Propriedade BaseModel enviado ao IS
        /// </summary>
        protected BaseModel baseModelSend { get; set; }

        /// <summary>
        /// Propriedade Nome do Programa.
        /// </summary>
        private string NomePrograma { get; set; }

        /// <summary>
        /// Construtor BrokerRepository
        /// </summary>
        public ISRepository(string descricaoServico, string nomePrograma)
        {
            DescricaoServico = descricaoServico;
            NomePrograma = nomePrograma;
            log = null;
        }

        /// <summary>
        /// Construtor BrokerRepository
        /// </summary>
        public ISRepository(string descricaoServico, string nomePrograma, ILog log)
        {
            DescricaoServico = descricaoServico;
            NomePrograma = nomePrograma;
            this.log = log;
        }

        /// <summary>
        /// Construtor BrokerRepository
        /// </summary>
        public ISRepository(string descricaoServico, string nomePrograma, IMensagemRepository mensagemRepository)
        {
            DescricaoServico = descricaoServico;
            NomePrograma = nomePrograma;
            log = null;
        }

        /// <summary>
        /// Construtor BrokerRepository
        /// </summary>
        public ISRepository(string descricaoServico, string nomePrograma, IMensagemRepository mensagemRepository, ILog log)
        {
            DescricaoServico = descricaoServico;
            NomePrograma = nomePrograma;
            this.log = log;
        }

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <returns>T</returns>
        public string GetEnvio()
        {
            return _textoXml;
        }

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <returns>T</returns>
        public BaseModel Connect()
        {
            return Connect(null);
        }

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>T</returns>
        public BaseModel Connect(BaseModel obj)
        {
            return conectarServidorIS(obj);
        }

        private BaseModelReceive conectarServidorIS(BaseModel objIn)
        {
            BaseModelReceive objOut = Activator.CreateInstance<T>();
            Exception erroIS = null;
            Exception exceptionXml = null;
            DateTime inicio = DateTime.Now;
            string descricaoLog = null;
            if (DescricaoServico != null)
            {
                descricaoLog = DescricaoServico.Substring(0, DescricaoServico.IndexOf('/'));
            }
            try
            {
                try
                {
                    StringBuilder parms = new StringBuilder();
                    if (objIn != null)
                    {
                        baseModelSend = objIn;
                        foreach (string parm in objIn.getValores())
                        {
                            parms.Append(parm);
                        }
                    }
                }
                catch (Exception e)
                {
                    erroIS = e;
                    throw new Exception("ErroIS Lendo parametro entrada:");
                }
                string strUri = ConfigurationManager.AppSettings.Get("HostIS") + DescricaoServico;
                string userUdIS = ConfigurationManager.AppSettings.Get("UserIdIS");
                string textoXml = "";
                try
                {
                    textoXml = System.IO.File.ReadAllText(@HttpContext.Current.Request.PhysicalApplicationPath + "\\xmlChamadaIS.xml");
                    textoXml = textoXml.Replace("NOME_PROGRAMA", NomePrograma);
                    textoXml = textoXml.Replace("IDUSUARIO", userUdIS);
                    if (objIn != null)
                    {
                        XmlSerializer xsSubmit = new XmlSerializer(objIn.GetType());
                        StringBuilder stringInRec = new StringBuilder();
                        stringInRec.Append("<inRec>");

                        using (var sww = new StringWriter())
                        {
                            using (XmlWriter xwriter = XmlWriter.Create(sww))
                            {
                                xsSubmit.Serialize(xwriter, objIn);
                                XmlDocument xmlSend = new XmlDocument();
                                xmlSend.LoadXml(sww.ToString());
                                XmlNodeList fields = xmlSend.GetElementsByTagName(objIn.GetType().Name);
                                stringInRec.Append(FiltraCaracteresEspeciais(fields.Item(0).InnerXml));
                            }
                        }
                        stringInRec.Append("</inRec>");
                        textoXml = textoXml.Replace("<inRec/>", stringInRec.ToString());
                        textoXml = textoXml.Replace("<ARRAY>", "");
                        textoXml = textoXml.Replace("</ARRAY>", "");
                        textoXml = textoXml.Replace("<ARRAY />", "");
                    }
                }
                catch (Exception e)
                {
                    erroIS = e;
                    throw new Exception("ErroIS ao montar a chamada ao IS:");
                }

                string retornoIS = "";
                _textoXml = textoXml;
                try
                {
                    Uri uri = new Uri(strUri);
                    WebRequest request = WebRequest.Create(uri);
                    //request.Timeout = 5000;
                    request.Method = "POST";
                    request.ContentType = "text/xml;charset=UTF-8";
                    request.Headers.Add("Authorization", ConfigurationManager.AppSettings.Get("AuthorizationIS"));
                    using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(textoXml);
                    }
                    WebResponse response = request.GetResponse();
                    Stream responseStream = response.GetResponseStream();
                    using (var reader = new StreamReader(responseStream))
                    {
                        retornoIS = reader.ReadToEnd().Trim();
                    }
                    response.GetResponseStream().Close();
                    response.Dispose();
                    /*
                    try
                    {
                        if (log != null)
                        {
                            TimeSpan DuracaoConexao = DateTime.Now.Subtract(inicio);
                            double duracaoSegundos = Math.Round(DuracaoConexao.TotalSeconds, 2);
                            log.AddLog(" " + descricaoLog + ": " + duracaoSegundos);
                        }
                    }
                    catch (Exception) { }
                    */
                }
                catch (Exception e)
                {
                    erroIS = e;
                    throw new Exception("ErroIS ao chamar IS:");
                }
                try
                {
                    //retornoIS = System.IO.File.ReadAllText(@"C:\temp\atualizaOS1.xml");
                    string nomeBaseModel = objOut.GetType().Name;
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(retornoIS);
                    string errorMessage = "";
                    try
                    {
                        XmlNodeList xnlerrorMessage = xml.GetElementsByTagName("errorMessage");
                        if (xnlerrorMessage.Item(0) != null)
                            errorMessage = xnlerrorMessage.Item(0).InnerXml;
                    }
                    catch (Exception) { }
                    if (!"".Equals(errorMessage))
                    {
                        objOut.descricaoErroIS = errorMessage;
                        exceptionXml = new Exception("ErrorMessage:" + objOut.descricaoErroIS);
                    }

                    var baseResponse = new BaseResponse();
                    inicializaCamposBaseModel(objOut);
                    if (objOut.descricaoErroIS == null)
                    {
                        XmlNodeList outRec = xml.GetElementsByTagName("outRec");
                        string cabecalho = System.IO.File.ReadAllText(@HttpContext.Current.Request.PhysicalApplicationPath + "\\xmlCabecalho.xml");
                        string xmlOutRec = outRec.Item(0).InnerXml;
                        string xmlRetorno = cabecalho + "<" + nomeBaseModel + ">" + xmlOutRec + "</" + nomeBaseModel + ">";
                        XmlSerializer serializer = new XmlSerializer(objOut.GetType());
                        var stream = new MemoryStream();
                        var writer = new StreamWriter(stream);
                        writer.Write(xmlRetorno);
                        writer.Flush();
                        stream.Position = 0;
                        objOut = (BaseModelReceive)serializer.Deserialize(stream);
                        objOut.IsValid = true;

                        try
                        {
                            objOut.descricaoRetorno = "";
                            int codigoRetornoSicom = 0;
                            if (!"".Equals(objOut.getValor("codigoRetornoSicom")))
                            {
                                if (int.TryParse(objOut.getValor("codigoRetornoSicom"), out codigoRetornoSicom))
                                {
                                    if (codigoRetornoSicom > 0)
                                    {
                                        objOut.codigoRetorno = codigoRetornoSicom.ToString();
                                        objOut.descricaoRetorno = codigoRetornoSicom.ToString();
                                    }
                                    else
                                    {
                                        objOut.codigoRetorno = "";
                                    }
                                }
                                else
                                {
                                    objOut.descricaoRetorno = objOut.getValor("codigoRetornoSicom");
                                }
                            }
                            if ("".Equals(objOut.descricaoRetorno) && !"".Equals(objOut.getValor("descricaoRetornoSicom")))
                                objOut.descricaoRetorno = objOut.getValor("descricaoRetornoSicom");
                        }
                        catch (Exception) { };
                        try
                        {
                            TratarRetorno((T)objOut);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        //objOut.descricaoRetorno = "Sistema comercial indisponivel";
                        objOut.descricaoRetorno = objOut.descricaoErroIS;
                        objOut.IsValid = false;
                        //_log.AddLog("    Programa: " + NomePrograma + " - Parms: " + parms.ToString());
                        //_log.AddLog("    Erro no retorno do IS: " + objOut.descricaoErroIS);
                        exceptionXml = new Exception("DescricaoErroIS:" + objOut.descricaoErroIS);
                    }
                }
                catch (Exception e)
                {
                    erroIS = e;
                    throw new Exception("ErroIS ao tratar retorno do IS:");
                }
            }
            catch (Exception e)
            {
                string textoErro = e.Message;
                if (textoErro.Length > 6 && "ErroIS".Equals(textoErro.Substring(0, 6)))
                {
                    if (erroIS != null)
                    {
                        textoErro = textoErro + erroIS.Message;
                        if (erroIS.InnerException != null && !"".Equals(erroIS.InnerException.Message))
                            textoErro = textoErro + " - " + erroIS.InnerException.Message;
                        throw new Exception(textoErro);
                    }
                    else
                        throw new Exception(textoErro);
                }
                else
                {
                    if (e.InnerException != null && !"".Equals(e.InnerException.Message))
                        textoErro = textoErro + " - " + e.InnerException.Message;
                    if (log != null)
                        log.GravaLogAdc(descricaoLog, inicio, textoErro);
                    throw new Exception("ErroIS Exception:" + textoErro);
                }
            }
            if (exceptionXml != null)
            {
                throw new Exception("ErroIS retorno no xml:" + exceptionXml.Message);
            }
            if (log != null)
                log.GravaLogAdc(descricaoLog, inicio);
            return objOut;
        }

        private void trataCamposBaseModelSend(BaseModel objOut)
        {
            Type objtype = objOut.GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                if ("String".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(objOut, "");
                }
                else if ("Int32".Equals(p.PropertyType.Name) || "Int64".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(objOut, 0);
                }
                else if ("List`1".Equals(p.PropertyType.Name))
                {
                    object objectModelo = Activator.CreateInstance(p.PropertyType);
                    objtype.GetProperty(p.Name).SetValue(objOut, objectModelo);
                }
            }
        }

        private void inicializaCamposBaseModel(BaseModel objOut)
        {
            Type objtype = objOut.GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                if ("String".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(objOut, "");
                }
                else if ("Int32".Equals(p.PropertyType.Name) || "Int64".Equals(p.PropertyType.Name))
                {
                    objtype.GetProperty(p.Name).SetValue(objOut, 0);
                }
                else if ("List`1".Equals(p.PropertyType.Name))
                {
                    object objectModelo = Activator.CreateInstance(p.PropertyType);
                    objtype.GetProperty(p.Name).SetValue(objOut, objectModelo);
                }
            }
        }

        /// <summary>
        /// Deve ser implementada de forma a retornar o nome da entdiade que sera exibido nas mensagens.
        /// </summary>
        /// <returns></returns>
        public abstract String GetEntidadeNome();

        /// <summary>
        /// Trata dados do retorno do Sicom
        /// </summary>
        protected abstract void TratarRetorno(T baseModelReceive);

        /// <summary>
        /// Gerar mensagem de sucesso
        /// </summary>
        /// <param name="delecao">Mensagem de delete</param>
        /// <returns>Mensagem</returns>
        public string GerarMensagemSucesso(bool delecao = false)
        {
            var retorno = string.Empty;

            if (delecao)
            {
                retorno = MessagesUtil.GetSuccess(GetEntidadeNome(), MessagesUtil.DELETED_SUCCESS);
            }
            else
            {
                retorno = MessagesUtil.GetSuccess(this.GetEntidadeNome());
            }

            return retorno;
        }
        /// <summary>
        /// Gera Base Response padrão de sucesso
        /// </summary>
        /// <param name="delecao">Mensagem de delete</param>
        /// <returns></returns>
        public BaseResponse GerarBaseResponseSucesso(bool delecao = false)
        {
            BaseResponse response = new BaseResponse
            {
                IsValid = true,
                Message = GerarMensagemSucesso(delecao)
            };
            return response;
        }

        /// <summary>
        /// Gera Base Response padrão de sucesso
        /// </summary>
        /// <param name="mensagem">Mensagem</param>
        /// <returns></returns>
        public BaseResponse GerarBaseResponseSucesso(string mensagem)
        {
            BaseResponse response = new BaseResponse
            {
                IsValid = true,
                Message = mensagem
            };
            return response;
        }

        /// <summary>
        /// Gera um Base Response com uma mensagem personalizada
        /// </summary>
        /// <param name="stringMsg">Mensagem que será exibida.</param>
        /// <returns></returns>
        public BaseResponse GerarBaseResponseNotValid(string stringMsg)
        {
            /*
            BaseMessage message = new BaseMessage
            {
                TipoMensagem = TipoMensagem.E,
                Message = string.Format(stringMsg),
            };
            */
            BaseResponse response = new BaseResponse
            {
                IsValid = false,
                Message = stringMsg
            };
            return response;
        }

        private string FiltraCaracteresEspeciais(string texto)
        {
            string retorno = texto.ToUpper();
            retorno = retorno.Replace("Ã", "A");
            retorno = retorno.Replace("Á", "A");
            retorno = retorno.Replace("À", "A");
            retorno = retorno.Replace("É", "E");
            retorno = retorno.Replace("Í", "I");
            retorno = retorno.Replace("Ó", "O");
            retorno = retorno.Replace("Ú", "U");
            retorno = retorno.Replace("Ç", "C");
            retorno = retorno.Replace("¨", " ");
            retorno = retorno.Replace("~", " ");
            retorno = retorno.Replace("´", " ");
            retorno = retorno.Replace("`", " ");
            retorno = retorno.Replace("'", " ");
            return retorno;
        }
    }
}
