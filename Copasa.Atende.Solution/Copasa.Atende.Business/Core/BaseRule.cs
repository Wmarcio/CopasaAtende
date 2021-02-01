namespace Copasa.Atende.Business.Core
{
    using Copasa.Atende.Model.Core;
    using Copasa.Util;
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// Classe BaseRule.
    /// </summary>
    public abstract class BaseRule
    {
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
        /// Gerar mensagem de erros
        /// </summary>
        /// <param name="erros">Erros.</param>
        /// <returns>Mensagem</returns>
        public string GerarMensagemErro(string erros)
        {
            var retorno = MessagesUtil.GetError(this.GetEntidadeNome(), erros);
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

        /// <summary>
        /// Grava entrada em arquivo de log
        /// </summary>
        public void gravaLog(string s)
        {
            //ILog _log = (Log)HttpContext.Current.Session["Log"];
            //_log.AddLog(s);
            //_log.PringLog();
        }

        /// <summary>
        /// Retorno o caminho até a pasta de trabalho
        /// </summary>
        protected string getPastaTrabalho()
        {
            return HttpContext.Current.Request.PhysicalApplicationPath;
        }

        /// <summary>
        /// Retorna numeros contidos em uma string
        /// </summary>
        protected string getNumeros(string valor)
        {
            try
            {
                if (valor != null)
                {
                    List<char> retorno = new List<char>();
                    char[] caracteres = valor.ToCharArray();
                    for (int i = 0; i < caracteres.Length; i++)
                    {
                        if (Char.IsDigit(caracteres[i]))
                            retorno.Add(caracteres[i]);
                    }
                    return new string(retorno.ToArray());
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
