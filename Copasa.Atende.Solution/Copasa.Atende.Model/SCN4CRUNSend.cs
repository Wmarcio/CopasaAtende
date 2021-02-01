using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRUNSend - Unidade de Destino
    /// </summary>
    public class SCN4CRUNSend : BaseModelSend
    {
        /// <summary>
        /// Matricula.
        /// </summary>
        [XmlElement("_RCV-MATRICULA")]
        public string matricula { get; set; }

        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_RCV-COD-LOCALIDADE")]
        public string codigoLocalidade { get; set; }

        /// <summary>
        /// CodigoBairro.
        /// </summary>
        [XmlElement("_RCV-COD-BAIRRO")]
        public string codigoBairro { get; set; }
    }
}
