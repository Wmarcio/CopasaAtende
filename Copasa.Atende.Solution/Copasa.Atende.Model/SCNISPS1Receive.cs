using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCNISPS1Receive - Lista hidrômetros de uma matrícula
    /// </summary>
    public class SCNISPS1Receive : BaseModelReceive
    {
        /// <summary>
        /// Código de erro do Sicom
        /// </summary>
        [XmlElement("_COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// Descrição de erro do Sicom
        /// </summary>
        [XmlElement("_DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// ClasseServico
        /// </summary>
        public string classeServico { get; set; }

        /// <summary>
        /// ValorServico
        /// </summary>
        public string valorServico { get; set; }

        /// <summary>
        /// EnviaCRM
        /// </summary>
        public string enviaCRM { get; set; }

        /// <summary>
        /// Hidrometros Sicom
        /// </summary>
        [XmlElement("_TABELA-PS")]
        [JsonIgnore]
        public SCNISPS1ReceiveHidrometro[] hidrometrosSicom { get; set; }

        /// <summary>
        /// Hidrometros
        /// </summary>
        public List<SCNISPS1ReceiveHidrometro> hidrometros { get; set; }
    }
}
