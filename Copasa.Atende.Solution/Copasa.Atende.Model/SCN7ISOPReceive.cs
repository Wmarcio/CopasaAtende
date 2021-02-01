using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN7ISOPReceive - Onde pagar a conta
    /// </summary>
    public class SCN7ISOPReceive : BaseModelReceive
    {
        /// <summary>
        /// CodigoLocalidade.
        /// </summary>
        [XmlElement("_MENSAGEM")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// LocaisSicom.
        /// </summary>
        [XmlElement("_TABELA")]
        [JsonIgnore]
        public SCN7ISOPReceiveLocal[] locaisSicom { get; set; }

        /// <summary>
        /// Locais.
        /// </summary>
        public List<SCN7ISOPReceiveLocal> locais { get; set; }
    }
}
