using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISACReceive - altera email e telefone
    /// </summary>
    public class SCN4ISACReceive : BaseModelReceive
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
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// EmailAtual.
        /// </summary>
        [XmlElement("_SND-INF-EMAIL-ATUAL")]
        public string emailAtual { get; set; }

        /// <summary>
        /// DDDCelularAtual.
        /// </summary>
        [XmlElement("_SND-DDD-TEL-CEL-ATUAL")]
        public string DDDCelularAtual { get; set; }

        /// <summary>
        /// TelefoneCelularAtual.
        /// </summary>
        [XmlElement("_SND-NUM-TEL-CEL-ATUAL")]
        public string telefoneCelularAtual { get; set; }

        /// <summary>
        /// DDDResidencialAtual.
        /// </summary>
        [XmlElement("_SND-DDD-TEL-RES-ATUAL")]
        public string DDDResidencialAtual { get; set; }

        /// <summary>
        /// TelefoneResidencialAtual.
        /// </summary>
        [XmlElement("_SND-NUM-TEL-RES-ATUAL")]
        public string telefoneResidencialAtual { get; set; }

        /// <summary>
        /// DDDComercialAtual.
        /// </summary>
        [XmlElement("_SND-DDD-TEL-COM-ATUAL")]
        public string DDDComercialAtual { get; set; }

        /// <summary>
        /// TelefoneComercialAtual.
        /// </summary>
        [XmlElement("_SND-NUM-TEL-COM-ATUAL")]
        public string telefoneComercialAtual { get; set; }
    }
}
