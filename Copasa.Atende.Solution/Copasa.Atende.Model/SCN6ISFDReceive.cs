using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISFDReceive Lista faturas em débito
    /// </summary>
    public class SCN6ISFDReceive : BaseModelReceive
    {
        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_MENSAGEM")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// ValorTotalDebito.
        /// </summary>
        [XmlElement("_VALOR-TOTAL-DEBITO-CLIENTE")]
        public string valorTotalDebito { get; set; }

        /// <summary>
        /// TemMaisRegistro.
        /// </summary>
        [XmlElement("_TEM-MAIS-REGISTROS")]
        public string temMaisRegistro { get; set; }

        /// <summary>
        /// FaturasSicom.
        /// </summary>
        [XmlElement("_TABELA-FATURAS")]
        [JsonIgnore]
        public SCN6ISFDReceiveFaturas[] faturasSicom { get; set; }

        /// <summary>
        /// Faturas.
        /// </summary>
        public List<SCN6ISFDReceiveFaturas> faturas { get; set; }
    }
}
