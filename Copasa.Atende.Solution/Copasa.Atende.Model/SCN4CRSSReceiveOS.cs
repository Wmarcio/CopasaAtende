using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4CRSSReceiveOS - cria solicitação de serviço - OS
    /// </summary>
    public class SCN4CRSSReceiveOS : BaseModel
    {
        /// <summary>
        /// TipoInformacaoOS.
        /// </summary>
        [XmlElement("_SND-TIPO-INF-OS")]
        public string tipoInformacaoOS { get; set; }

        /// <summary>
        /// NumeroProtocoloOS.
        /// </summary>
        [XmlElement("_SND-NUM-PROTOCOLO-OS")]
        public string numeroProtocoloOS { get; set; }

        /// <summary>
        /// NumeroSolicitacaoServicoOS.
        /// </summary>
        [XmlElement("_SND-NUM-SS-OS")]
        public string numeroSolicitacaoServicoOS { get; set; }

        /// <summary>
        /// NumeroOrdemServico.
        /// </summary>
        [XmlElement("_SND-NUM-OS")]
        public string numeroOrdemServico { get; set; }

        /// <summary>
        /// CodigoServicoOS.
        /// </summary>
        [XmlElement("_SND-COD-SERVICO-OS")]
        public string codigoServicoOS { get; set; }

        /// <summary>
        /// DescricaoServicoOS.
        /// </summary>
        [XmlElement("_SND-DESC-SERVICO-OS")]
        public string descricaoServicoOS { get; set; }

        /// <summary>
        /// DataGeracaoOS.
        /// </summary>
        [XmlElement("_SND-DATA-GERACAO-OS")]
        public string dataGeracaoOS { get; set; }

        /// <summary>
        /// HoraGeracaoOS.
        /// </summary>
        [XmlElement("_SND-HORA-GERACAO-OS")]
        public string horaGeracaoOS { get; set; }

        /// <summary>
        /// dataPrevisaoOSDyn365.
        /// </summary>
        [Dyn365Name("copasa_dataprevisao")]
        public string dataPrevisaoOSDyn365 { get; set; }

        /// <summary>
        /// DataPrevisaoOS.
        /// </summary>
        [XmlElement("_SND-DATA-PREVISAO-OS")]
        public string dataPrevisaoOS { get; set; }

        /// <summary>
        /// HoraPrevisaoOS.
        /// </summary>
        [XmlElement("_SND-HORA-PREVISAO-OS")]
        public string horaPrevisaoOS { get; set; }

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
    }
}
