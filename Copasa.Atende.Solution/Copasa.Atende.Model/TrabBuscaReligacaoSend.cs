using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// BuscaReligacaoSend - Religação
    /// </summary>
    public class TrabBuscaReligacaoSend : BaseModelSend
    {
        /// <summary>
        /// Número da Matricula.
        /// </summary>
        public string numeroMatricula { get; set; }

        /// <summary>
        /// Identificador.
        /// </summary>
        public string identificador { get; set; }

        /// <summary>
        /// Identificador Unidade
        /// </summary>
        public string identificadorUnidade { get; set; }
    }
}
