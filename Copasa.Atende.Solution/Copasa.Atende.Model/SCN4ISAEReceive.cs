using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISAEReceive - Gera alteração de economias
    /// </summary>
    public class SCN4ISAEReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// descricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// NumeroSS.
        /// </summary>
        [XmlElement("_SND-NRO-PROTOCOLO-SS")]
        public string protocoloSS { get; set; }

        /// <summary>
        /// NumeroSS.
        /// </summary>
        [XmlElement("_SND-NRO-SOLIC-SERV")]
        public string numeroSS { get; set; }

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
