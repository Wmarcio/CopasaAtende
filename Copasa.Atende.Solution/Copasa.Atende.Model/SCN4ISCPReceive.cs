using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISC Receive
    /// </summary>
    public class SCN4ISCPReceive : BaseModelReceive
    {
        /// <summary>
        /// Código de erro do Sicom
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// Lista de Parcelamento Sicom
        /// </summary>
        [XmlElement("_SND-PARCELAMENTO")]
        [JsonIgnore]
        public SCN4ISCPReceiveParcelamento[] parcelamentosSicom { get; set; }

        /// <summary>
        /// Lista de parcelamento
        /// </summary>
        public List<SCN4ISCPReceiveParcelamento> parcelamento { get; set; }
    }
}
