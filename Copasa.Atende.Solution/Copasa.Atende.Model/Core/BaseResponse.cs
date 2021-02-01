using Copasa.Atende.DTO.Core;
using System;
using System.Collections.Generic;

namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Objeto padrão de resposta das requisições
    /// </summary>
    [Serializable()]
    public class BaseResponse
    {
        /// <summary>
        /// Objeto de código.
        /// </summary>
        private string _codigo;

        /// <summary>
        /// Objeto de título.
        /// </summary>
        private string _titulo;

        //public void InfoLog(string message)
        //{
        //    log.Info(message);
        //    //log.Debug(message);
        //}

        /// <summary>
        /// Instância objeto BaseResponse vazio e válido.
        /// </summary>
        public BaseResponse()
        {
            IsValid = true;
            Model = new BaseModel();
            Collection = new List<BaseModel>();
            Messages = new List<BaseMessage>();
            //Message = new BaseMessage();
        }

        /// <summary>
        /// Instância objeto BaseResponse vazio e válido.
        /// </summary>
        public BaseResponse(bool isValid, bool isDTO)
        {
            IsValid = true;
            ModelDTO = new BaseDTO();
            CollectionDTO = new List<BaseDTO>();
            Messages = new List<BaseMessage>();
            //Message = new BaseMessage();
        }

        /// <summary>
        /// Instância objeto BaseResponse vazio com o status informado
        /// </summary>
        /// <param name="isValid">BaseResponse válido ou não.</param>
        public BaseResponse(bool isValid)
        {
            IsValid = isValid;
            Model = new BaseModel();
            Collection = new List<BaseModel>();
            Messages = new List<BaseMessage>();
            //Message = new BaseMessage();
        }

        /// <summary>
        /// Instância objeto BaseResponse a partir de um Exception.
        /// </summary>
        /// <param name="ex">Objeto Exception.</param>
        public BaseResponse(Exception ex)
        {
            IsValid = false;
            //Message = new BaseMessage() { Message = ex.Message, TipoMensagem = Util.Enumerador.TipoMensagem.E };
            Message = ex.Message;
            Erro = ex;
            Messages = new List<BaseMessage>();
        }

        /// <summary>
        /// Instância objeto BaseResponse a partir da mensagem.
        /// </summary>
        /// <param name="message">Mensagem de retorno.</param>
        /// <param name="isValid">BaseResponse válido ou não. Default: inválido.</param>
        public BaseResponse(string message, bool isValid = false)
        {
            IsValid = isValid;
            //Message = new BaseMessage() { Message = message };
            Messages = new List<BaseMessage>();
        }

        /// <summary>
        /// Indica se o objeto possui um estado válido.
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Recupera o total de registros da Collection.
        /// </summary>
        public int CollectionCount
        {
            get
            {
                if (this.Collection != null)
                {
                    return this.Collection.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Recupera e grava o total de registros.
        /// </summary>
        public Int64? Total { get; set; }

        /// <summary>
        /// Objeto BaseModel.
        /// </summary>
        public BaseModel Model { get; set; }

        /// <summary>
        /// Objeto BaseDTO.
        /// </summary>
        public BaseDTO ModelDTO { get; set; }

        /// <summary>
        /// Recupera e grava uma coleção de BaseModel.
        /// </summary>
        public List<BaseModel> Collection { get; set; }

        /// <summary>
        /// Recupera e grava uma coleção de BaseModel DTO.
        /// </summary>
        public List<BaseDTO> CollectionDTO { get; set; }

        /// <summary>
        /// Recupera e grava um tipo genérico de resposta do tipo dicionário
        /// </summary>
        public Dictionary<object, object> DictionaryValues { get; set; }

        /// <summary>
        /// Recupera e grava um tipo genérico de resposta do tipo Array
        /// </summary>
        public object[] ArrayValues { get; set; }

        /// <summary>
        /// Recupera e grava um tipo genérico de resposta do tipo String
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// Recupera e grava um código de resposta.
        /// </summary>
        public string Codigo
        {
            get
            {
                if (!string.IsNullOrEmpty(_codigo))
                {
                    this._codigo = this._codigo.Replace("'", string.Empty).Replace("\"", string.Empty);
                    this._codigo = this._codigo.Replace(Environment.NewLine, string.Empty).Replace("\n", " ");
                }
                return _codigo;
            }
            set
            {
                _codigo = value;
            }
        }

        /// <summary>
        /// Recupera e grava um título de resposta.
        /// </summary>
        public string Titulo
        {
            get
            {
                if (!string.IsNullOrEmpty(_titulo))
                {
                    this._titulo = this._titulo.Replace("'", string.Empty).Replace("\"", string.Empty);
                    this._titulo = this._titulo.Replace(Environment.NewLine, string.Empty).Replace("\n", " ");
                }
                return _titulo;
            }
            set
            {
                _titulo = value;
            }
        }

        /// <summary>
        /// Recupera e grava um objeto de resposta.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Recupera e grava uma lista de respostas.
        /// </summary>
        public IList<BaseMessage> Messages { get; set; }

        /// <summary>
        /// Sobrescre o método para exibir informações sobre o objeto.
        /// </summary>
        /// <returns>Objeto string.</returns>
        public override string ToString()
        {
            string r = this.IsValid ? "Valido" : "Invalido";
            if (this.IsValid)
            {
                var total = this.CollectionCount > 0 ? this.CollectionCount : this.Model != null ? 1 : 0;
                r += " - " + total.ToString() + " registro(s) encontrado(s)";
                if (this.CollectionCount == 1 && this.Model != null)
                    r += ": " + this.Model.ToString();
            }
            else
            {
                r += " - " + this.Message;
            }

            return r;
        }

        /// <summary>
        /// Adiciona um erro
        /// </summary>
        public Exception Erro { get; set; }

        /// <summary>
        /// Verifica de algum erro foi setado na propriedade Erro
        /// </summary>
        public bool HasError { get { return this.Erro != null; } }
    }
}
