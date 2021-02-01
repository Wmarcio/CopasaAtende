using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRALSend - Gera eventos prioridade
    /// </summary>
    public class SCN4CRALSend : BaseModel
    {
        /// <summary>
        /// ProtocoloSS.
        /// </summary>
        [XmlElement("_RCV-NUM-PROT-SS")]
        public string protocoloSS { get; set; }

        /// <summary>
        /// ProtocoloAtendimento.
        /// </summary>
        [XmlElement("_RCV-NUM-PROT-ATEND")]
        public string protocoloAtendimento { get; set; }

        /// <summary>
        /// Evento.
        /// </summary>
        [XmlElement("_RCV-FLAG-EVENTO")]
        public string evento { get; set; }

        /// <summary>
        /// Origem.
        /// </summary>
        [XmlElement("_RCV-FLAG-ORIGEM")]
        public string Origem { get; set; }

        /// <summary>
        /// ObservacaoEvento.
        /// </summary>
        [XmlElement("_RCV-OBS-EVENTO")]
        public string observacaoEvento { get; set; }

        /// <summary>
        /// DataPrevisao.
        /// </summary>
        public string dataPrevisao { get; set; }

        /// <summary>
        /// NumeroSS.
        /// </summary>
        public string numeroSS { get; set; }

        /// <summary>
        /// Empresa.
        /// </summary>
        public string empresa { get; set; }

        /// <summary>
        /// Protocolo.
        /// </summary>
        public string protocolo { get; set; }

    }
}
