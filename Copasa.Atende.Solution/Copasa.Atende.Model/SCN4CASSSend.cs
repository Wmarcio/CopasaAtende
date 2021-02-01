using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CASS - Classe de entrada para o cancelamento de serviço
    /// </summary>
    public class SCN4CASSSend : BaseModelSend
    {
        /// <summary>
        /// Flag que indica a origem da solicitação (WEB/APP/CRM)
        /// </summary>
        [XmlElement("_RCV-FLAG-ORIGEM")]
        public string flagOrigem { get; set; }

        /// <summary>
        /// Número do protocolo da SS
        /// </summary>
        [XmlElement("_RCV-NUM-PROT-SS")]
        public string numeroProtocoloSS { get; set; }

        /// <summary>
        /// Número do Protocolo do cancelamento
        /// </summary>
        [XmlElement("_RCV-NUM-PROT-CANC")]
        public string numeroProtocoloCancelamento { get; set; }
    }
}
