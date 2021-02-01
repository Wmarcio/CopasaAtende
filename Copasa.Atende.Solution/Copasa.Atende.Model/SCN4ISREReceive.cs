using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN4ISRE Receive
    /// </summary>
    public class SCN4ISREReceive : BaseModelReceive
    {
        /// <summary>
        /// Código do erro
        /// </summary>
        [XmlElement("_SND-COD-ERRO")]
        [JsonIgnore]
        public string codigoRetornoSicom { get; set; }

        /// <summary>
        /// Descricao do erro
        /// </summary>
        [XmlElement("_SND-DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }

        /// <summary>
        /// Número da solicitação de serviço
        /// </summary>
        [XmlElement("_SND-NUM-SS")]
        public string numeroSS { get; set; }

        /// <summary>
        /// Número da solicitação de serviço
        /// </summary>
        [XmlElement("_SND-NRO-PROTOCOLO-SS")]
        public string protocoloSS { get; set; }

        /// <summary>
        /// Número da solicitação de serviço
        /// </summary>
        [XmlElement("_SND-DT-PREVISAO-SS")]
        public string dataPrevisaoSS { get; set; }

        /// <summary>
        /// Número da solicitação de serviço
        /// </summary>
        [XmlElement("_SND-HR-PREVISAO-SS")]
        public string horaPrevisaoSS { get; set; }
    }
}
