using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISGPReceive - Gera parcelamento de débito
    /// </summary>
    public class SCN6ISGPReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// NumeroFaturaAvulsaGerada
        /// </summary>
        [XmlElement("_SND-NUM-FATURA-AVULSA-GER")]
        public string numeroFaturaAvulsaGerada { get; set; }
    }
}
