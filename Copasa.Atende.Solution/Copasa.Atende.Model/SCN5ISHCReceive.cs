using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN5ISHCReceive Histórico de consumo
    /// </summary>
    public class SCN5ISHCReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoRetornoSicom.
        /// </summary>
        [XmlElement("_NUM-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// DadosSicom.
        /// </summary>
        [XmlElement("_DADOS")]
        [JsonIgnore]
        public SCN5ISHCReceiveDados[] dadosSicom { get; set; }

        /// <summary>
        /// Ocorrencias.
        /// </summary>
        public List<SCN5ISHCReceiveDados> ocorrencias { get; set; }
    }
}
