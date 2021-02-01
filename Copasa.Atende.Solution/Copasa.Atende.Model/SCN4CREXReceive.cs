using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CREXReceive - Gera OS extra
    /// </summary>
    public class SCN4CREXReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-MSG-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// OrdemServico.
        /// </summary>
        [XmlElement("_SND-NUM-OS")]
        public string ordemServico { get; set; }

        /// <summary>
        /// CodigoServico.
        /// </summary>
        [XmlElement("_SND-COD-SERV-OS")]
        public string codigoServico { get; set; }

        /// <summary>
        /// DescricaoServico.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO")]
        public string descricaoServico { get; set; }

        /// <summary>
        /// SituacaoOS.
        /// </summary>
        [XmlElement("_SND-SITUACAO-OS")]
        public string situacaoOS { get; set; }

        /// <summary>
        /// DescricaoSituacaoOS.
        /// </summary>
        [XmlElement("_SND-DESC-SITUACAO-OS")]
        public string descricaoSituacaoOS { get; set; }

        /// <summary>
        /// DataGeracaoOS.
        /// </summary>
        [XmlElement("_SND-DT-GERACAO-OS")]
        public string dataGeracaoOS { get; set; }

        /// <summary>
        /// HoraGeracaoOS.
        /// </summary>
        [XmlElement("_SND-HR-GERACAO-OS")]
        public string horaGeracaoOS { get; set; }

        /// <summary>
        /// DataPrevisaoAtendimento.
        /// </summary>
        [XmlElement("_SND-DATA-PREV-ATEND")]
        public string dataPrevisaoAtendimento { get; set; }

        /// <summary>
        /// HoraPrevisaoAtendimento.
        /// </summary>
        [XmlElement("_SND-HORA-PREV-ATEND")]
        public string horaPrevisaoAtendimento { get; set; }
    }
}
