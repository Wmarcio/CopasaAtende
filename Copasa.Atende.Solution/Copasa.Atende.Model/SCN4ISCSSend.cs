using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISCSSend - busca informações de matrícula
    /// </summary>
    public class SCN4ISCSSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-COD-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-COD-SERVICO")]
        public string codigoServico { get; set; }
    }
}
