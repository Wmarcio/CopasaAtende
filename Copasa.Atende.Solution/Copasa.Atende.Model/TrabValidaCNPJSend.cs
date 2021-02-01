using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabValidaCNPJSend - Verifica se identificador é uma pessoa jurídica
    /// </summary>
    public class TrabValidaCNPJSend : BaseModelSend
    {
        /// <summary>
        /// Identificador
        /// </summary>
        public string identificador { get; set; }
    }
}
