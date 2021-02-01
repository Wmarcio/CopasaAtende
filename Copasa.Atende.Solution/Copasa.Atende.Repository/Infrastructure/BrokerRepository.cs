using Copasa.Atende.Model.Core;
using Copasa.Atende.Model.Enumerador;
using Copasa.Atende.Util;
using Copasa.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;

namespace Copasa.Atende.Repository.Infrastructure
{
    /// <summary>
    /// Interface IBrokerRepository
    /// </summary>
    public abstract class BrokerRepository<T> : IBrokerRepository<T> where T : BaseModelRetorno
    {
        private ILog _log;
        /// <summary>
        /// Propriedade Nome do Programa.
        /// </summary>
        private string NomePrograma { get; set; }

        /// <summary>
        /// Propriedade Parâmetro.
        /// </summary>
        private string _parametro { get; set; }

        /// <summary>
        /// Host.
        /// </summary>
        private string _host;

        /// <summary>
        /// Environment.
        /// </summary>
        private string _environment;

        /// <summary>
        /// Posicao de leitura dos dados de retorno do broker.
        /// </summary>
        private int posicaoLeituraRetorno = 0;

        //private string mensagemErro = "ocorreu um erro interno";
        /// <summary>
        /// Construtor BrokerRepository
        /// </summary>
        public BrokerRepository(string nomePrograma)
        {
            NomePrograma = nomePrograma;
        }

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <returns>T</returns>
        public BaseResponse Connect()
        {
            return Connect(null);
        }

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>T</returns>
        public BaseResponse Connect(BaseModel obj)
        {
            string codigo = "";
            if (obj != null)
                _parametro = GetValuesAsString(obj);
            return Download(_parametro, codigo.Length);
        }

        /// <summary>
        /// Conectar com o Sicom via Broker
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="log"></param>
        /// <returns>T</returns>
        public BaseResponse Connect(BaseModel obj,ILog log)
        {
            _log = log;
            return Connect(obj);
        }
        /// <summary>
        /// Download do Mainframe com parâmetros.
        /// </summary>
        /// <param name="parametro">Código.</param>
        /// <param name="tamanho">Tamanho.</param>
        /// <returns></returns>
        private BaseResponse Download(string parametro, int tamanho)
        {
            //ILog _log = null;
            var baseResponse = new BaseResponse();
            try
            {
                _environment = ConfigurationManager.AppSettings.Get("Environment");
                baseResponse.Model = Activator.CreateInstance<T>();
                for (int z = 0; z < 3; z++)
                {
                    if (retornoBroker == null || "".Equals(retornoBroker))
                        retornoBroker = conectarServidorviaViaPost(parametro);
                    //_log.GravaLog("retornobroker:" + retornoBroker);
                    if (retornoBroker.Contains("INEXISTENTE RETORNA AO SERVIDOR"))
                    {
                        //_log.GravaLog("Sub programa " + NomePrograma + " inexistente na biblioteca broker");
                        //((BaseModelRetorno)baseResponse.Model).descricaoRetorno = mensagemErro;
                        //return baseResponse;
                        throw new Exception(retornoBroker);
                    }
                    else if (retornoBroker.Contains("API: Invalid SEND-LENGTH"))
                    {
                        //_log.AddLog("Erro na chamada ao broker: " + NomePrograma + " - Erro: " + retornoBroker);
                        //_log.PringLog();
                        //((BaseModelRetorno)baseResponse.Model).descricaoRetorno = mensagemErro;
                        //return baseResponse;
                        throw new Exception(retornoBroker);
                    }
                    else if (!retornoBroker.Contains("ErroBroker") && !"ERRO".Equals(retornoBroker.Substring(0, 4).ToUpper()))
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(500);                       
                    }
                }
                if (!"".Equals(retornoBroker))
                {
                    if (retornoBroker.Contains("ErroBroker") || "ERRO".Equals(retornoBroker.Substring(0, 4).ToUpper()))
                    {
                        throw new Exception(retornoBroker);
                    }
                    else
                    {
                        posicaoLeituraRetorno = 0;
                        T objOut = Activator.CreateInstance<T>();
                        objOut.retornoBroker = retornoBroker.Trim();
                        baseResponse.Model = objOut;
                        SetValueInBaseModel(retornoBroker, baseResponse.Model);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("ErroBroker:" + e.Message);

            }
            retornoBroker = "";
            return baseResponse;
        }

        /// <summary>
        /// Atribuir ambiente 
        /// </summary>
        /// <param name="ambiente"></param>
        /// <returns></returns>
        private void SetEnvironment(EnvironmentEnum ambiente)
        {
            /*
            switch (ambiente)
            {
                case EnvironmentEnum.H:
                    _environment = ConfigurationManager.AppSettings.Get("EnvironmentHomologacao");
                    break;
                case EnvironmentEnum.D:
                    _environment = ConfigurationManager.AppSettings.Get("EnvironmentDesenvolvimento");
                    break;
                case EnvironmentEnum.P:
                    _environment = ConfigurationManager.AppSettings.Get("EnvironmentProducao");
                    break;
            }
            */
        }

        private bool SetValueInBaseModel(string codigo, BaseModel objOut)
        {
            bool temValor = false;
            Type objtype = objOut.GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                foreach (Attribute at in p.GetCustomAttributes(false))
                {
                    if (!"BrokerAttribute".Equals(at.GetType().Name))
                        continue;
                    BrokerAttribute ba = (BrokerAttribute)at;
                    var prop = objOut.GetType();
                    if (ba.Ocorrencia == 1)
                    {
                        char tipo = ba.Tipo[0];
                        int tamanho = int.Parse(ba.Tipo.Substring(1));
                        if (tipo == 'N')
                        {
                            string valor = codigo.Substring(posicaoLeituraRetorno, tamanho);
                            if ("".Equals(valor.Trim()))
                            {
                                valor = "0";
                            }
                            if (tamanho > 9)
                            {
                                Int64 valInt = Int64.Parse(valor);
                                if (valInt > 0)
                                    temValor = true;
                                p.SetValue(objOut, valInt);
                                //prop.GetProperty(ba.Nome).SetValue(objOut, valInt);
                            }
                            else
                            {
                                int valInt = int.Parse(valor);
                                if (valInt > 0)
                                    temValor = true;
                                p.SetValue(objOut, valInt);
                                //prop.GetProperty(ba.Nome).SetValue(objOut, valInt);
                            }
                        }
                        else
                        {
                            string valor = codigo.Substring(posicaoLeituraRetorno, tamanho).Trim();
                            if (!"".Equals(valor.Trim()))
                                temValor = true;
                            p.SetValue(objOut, valor);
                            //prop.GetProperty(ba.Nome).SetValue(objOut, valor);
                        }
                        posicaoLeituraRetorno = posicaoLeituraRetorno + tamanho;
                    }
                    else
                    {
                        dynamic[] arrayValores = null;
                        dynamic valor;
                        object objectModelo;
                        Type type = Type.GetType("Copasa.Atende.Model." + ba.Tipo + ",Copasa.Atende.Model");
                        Type typoArray;
                        if (type == null)
                        {
                            typoArray = type;
                            char tipo = ba.Tipo[0];
                            int tamanho = int.Parse(ba.Tipo.Substring(1));
                            if (tipo == 'N')
                            {
                                if (tamanho > 9)
                                {
                                    objectModelo = Activator.CreateInstance(Type.GetType("System.Int64"));
                                }
                                else
                                {
                                    objectModelo = Activator.CreateInstance(Type.GetType("System.Int32"));
                                }
                            }
                            else
                            {
                                objectModelo = "";
                            }
                        }
                        else
                        {
                            objectModelo = Activator.CreateInstance(type);
                        }
                        var lista = getList(objectModelo);
                        for (int i = 0; i < ba.Ocorrencia; i++)
                        {
                            if (type == null)
                            {
                                char tipo = ba.Tipo[0];
                                int tamanho = int.Parse(ba.Tipo.Substring(1));
                                if (tipo == 'N')
                                {
                                    string valorAlfa = codigo.Substring(posicaoLeituraRetorno, tamanho);
                                    if ("".Equals(valorAlfa.Trim()))
                                    {
                                        valorAlfa = "0";
                                    }
                                    if (tamanho > 9)
                                    {
                                        valor = Int64.Parse(valorAlfa);
                                    }
                                    else
                                    {
                                        valor = int.Parse(valorAlfa);
                                    }
                                }
                                else
                                {
                                    valor = codigo.Substring(posicaoLeituraRetorno, tamanho).Trim();
                                }
                                posicaoLeituraRetorno = posicaoLeituraRetorno + tamanho;
                                lista.Add(valor);
                            }
                            else
                            {
                                valor = Activator.CreateInstance(type);
                                if (SetValueInBaseModel(codigo, valor))
                                    lista.Add(valor);

                            }
                            if (arrayValores == null)
                            {
                                arrayValores = Array.CreateInstance(valor.GetType(), ba.Ocorrencia);
                            }
                            arrayValores[i] = valor;
                        }
                        //prop.GetProperty(ba.Nome).SetValue(objOut, toArray(lista, type));
                        //prop.GetProperty(ba.Nome).SetValue(objOut, toArray(lista, objectModelo.GetType()));
                        p.SetValue(objOut, toArray(lista, objectModelo.GetType()));
                    }
                }
            }
            return temValor;
        }

        private List<Q> getList<Q>(Q obj)
        {
            return new List<Q>();
        }

        private Q[] toArray<Q>(List<Q> list, Type tipo)
        {
            Q[] toR = (Q[])Array.CreateInstance(tipo, list.Count);
            int i = 0;
            foreach (Q obj in list)
            {
                toR[i] = obj;
                i++;
            }
            return toR;
        }

        private string GetValuesAsString(BaseModel model)
        {
            StringBuilder retorno = new StringBuilder();
            Type objtype = model.GetType();
            foreach (PropertyInfo p in objtype.GetProperties())
            {
                foreach (Attribute baTemp in p.GetCustomAttributes(false))
                {
                    try
                    {
                        BrokerAttribute ba = (BrokerAttribute)baTemp;
                        if (ba.Ocorrencia == 1)
                        {
                            char tipo = ba.Tipo[0];
                            int tamanho = int.Parse(ba.Tipo.Substring(1));
                            var prop = model.GetType();
                            var value = p.GetGetMethod().Invoke(model, new object[] { });
                            //var value = prop.GetProperty(ba.Nome).GetGetMethod().Invoke(model, new object[] { });
                            string valorFormatado;
                            if (tipo == 'N')
                            {
                                if (value == null)
                                {
                                    value = 0;
                                }
                                if (tamanho < value.ToString().Length)
                                    valorFormatado = value.ToString().Substring(0, tamanho);
                                else if (tamanho > value.ToString().Length)
                                    valorFormatado = new string('0', tamanho - value.ToString().Length) + value.ToString();
                                else
                                    valorFormatado = value.ToString();
                            }
                            else
                            {
                                if (value == null)
                                {
                                    value = " ";
                                }
                                if (tamanho < value.ToString().Length)
                                    valorFormatado = value.ToString().Substring(0, tamanho);
                                else if (tamanho > value.ToString().Length)
                                    valorFormatado = value.ToString() + new string(' ', tamanho - value.ToString().Length);
                                else
                                    valorFormatado = value.ToString();
                            }
                            retorno.Append(valorFormatado);
                        }
                        else
                        {
                            var prop = model.GetType();
                            //BaseModel[] arrayModels = (BaseModel[])prop.GetProperty(ba.Nome).GetGetMethod().Invoke(model, new object[] { });
                            BaseModel[] arrayModels = (BaseModel[])p.GetGetMethod().Invoke(model, new object[] { });
                            for (int i = 0; i < ba.Ocorrencia; i++)
                            {
                                if (arrayModels == null || arrayModels.Length <= i || arrayModels[i] == null)
                                {
                                    var type = Type.GetType("Copasa.Atende.Model." + ba.Tipo + ",Copasa.Atende.Model");
                                    BaseModel modelChild = (BaseModel)Activator.CreateInstance(type);
                                    retorno.Append(GetValuesAsString(modelChild));
                                }
                                else
                                {
                                    retorno.Append(GetValuesAsString(arrayModels[i]));
                                }
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            return retorno.ToString();
        }

        /// <summary>
        /// Deve ser implementada de forma a retornar o nome da entdiade que sera exibido nas mensagens.
        /// </summary>
        /// <returns></returns>
        public abstract String GetEntidadeNome();

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

        private string conectarServidorviaViaPost(string parametro)
        {
            _host = ConfigurationManager.AppSettings.Get("HostBroker");
            string strUri = "http://" + _host + _environment;
            parametro = NomePrograma + parametro;
            try
            {
                _log.AddLog("Chamada ao Broker:" + parametro);
            }
            catch (Exception) { }
            ASCIIEncoding encoding = new ASCIIEncoding();
            string postData = "parametro=" + HttpUtility.UrlEncode(FiltraCaracteresEspeciais(parametro));
            byte[] data = encoding.GetBytes(postData);

            Uri uri = new Uri(strUri);
            WebRequest request = WebRequest.Create(uri);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            request.ContentLength = data.Length;
            Stream newStream = request.GetRequestStream();

            newStream.Write(data, 0, data.Length);
            newStream.Close();

            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            using (var reader = new StreamReader(responseStream))
            {
                var retorno = reader.ReadToEnd();
                return retorno;
            }
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

        /// <summary>
        /// RetornoBroker.
        /// </summary>
        public string retornoBroker { get; set; }

    }
}
