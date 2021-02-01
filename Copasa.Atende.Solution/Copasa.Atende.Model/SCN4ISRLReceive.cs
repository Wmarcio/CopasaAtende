using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// Busca dados religação água
    /// </summary>
    public class SCN4ISRLReceive : BaseModelReceive
    {
        /// <summary>
        /// Código de erro do Sicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// Gera solicitação de serviço.
        /// </summary>
        [XmlElement("_SND-GERA-SS")]
        public string geraSS { get; set; }

        /// <summary>
        /// Serviço.
        /// </summary>
        [XmlElement("_SND-SERVICO-SS")]
        public string servicoSS { get; set; }

        /// <summary>
        /// Valor do Serviço 1.
        /// </summary>
        [XmlElement("_SND-VALOR-SERV-1")]
        public string valorServico1 { get; set; }

        /// <summary>
        /// Valor do serviço 2.
        /// </summary>
        [XmlElement("_SND-VALOR-SERV-2")]
        public string valorServico2 { get; set; }

        /// <summary>
        /// Flag que informa se existe religação.
        /// </summary>
        [XmlElement("_SND-VALOR-RELIGACAO-N")]
        public string valorReligacao { get; set; }

        /// <summary>
        /// Informa se haverá religação.
        /// </summary>
        [XmlElement("_SND-FLAG-FINANCIA-SERVICO")]
        public string financiaServico { get; set; }

        /// <summary>
        /// ProtocoloSS.
        /// </summary>
        [XmlElement("_SND-NRO-PROTOCOLO-SS")]
        public string protocoloSS { get; set; }

        /// <summary>
        /// DataPrevisaoSS.
        /// </summary>
        [XmlElement("_SND-DT-PREVISAO-SS")]
        public string dataPrevisaoSS { get; set; }

        /// <summary>
        /// HoraPrevisaoSS.
        /// </summary>
        [XmlElement("_SND-HR-PREVISAO-SS")]
        public string horaPrevisaoSS { get; set; }
    }
}
