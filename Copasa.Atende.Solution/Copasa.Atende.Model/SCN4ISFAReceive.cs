using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISFAReceive Pesquisa interrupção de abastecimento
    /// </summary>
    public class SCN4ISFAReceive : BaseModelReceive
    {
        /// <summary>
        /// CodErro.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// FlTemInterrup.
        /// </summary>
        [XmlElement("_SND-FL-TEM-INTERRUP")]
        public string temInterrupcao { get; set; }

        /// <summary>
        /// DataPrevisao.
        /// </summary>
        [XmlElement("_SND-DATA-PREVISAO")]
        public string dataPrevisao { get; set; }

        /// <summary>
        /// HoraPrevisao.
        /// </summary>
        [XmlElement("_SND-HORA-PREVISAO")]
        public string horaPrevisao { get; set; }

        /// <summary>
        /// NumOsGerada.
        /// </summary>
        [XmlElement("_SND-MOTIVO")]
        public string codigoMotivo { get; set; }

        /// <summary>
        /// NumSSGerada.
        /// </summary>
        [XmlElement("_SND-DESC-MOTIVO")]
        public string descricaoMotivo { get; set; }

    }
}
