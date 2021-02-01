using Copasa.Util.Enumerador;

namespace Copasa.Atende.Model.Core
{
    /// <summary>
    /// Objeto padrão de mensagem.
    /// </summary>
    public class BaseMessage
    {
        /// <summary>
        /// Recupera e Grava o Tipo de Mensagem.
        /// </summary>
        public TipoMensagem TipoMensagem { get; set; }

        /// <summary>
        /// Recupera e grava uma Mensagem.
        /// </summary>
        public string Message { get; set; }
    }
}
