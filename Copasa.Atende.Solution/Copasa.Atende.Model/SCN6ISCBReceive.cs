using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCBReceive - Código de barras de fatura
    /// </summary>
    public class SCN6ISCBReceive : BaseModelReceive
    {
        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_NUMCODB")]
        public string codigoBarras { get; set; }

        /// <summary>
        /// NumeroFatura.
        /// </summary>
        [XmlElement("_CODBARSD")]
        public string codigoBarrasFormatado { get; set; }
    }
}
