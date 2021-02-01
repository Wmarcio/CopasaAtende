using Copasa.Atende.Model.Core;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// TrabValidaCNPJReceive - Verifica se identificador é uma pessoa jurídica
    /// </summary>
    public class TrabValidaCNPJReceive : BaseModelReceive
    {
        /// <summary>
        /// CNPJ
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// Confirmacao
        /// </summary>
        public string confirmacao { get; set; }
    }
}
