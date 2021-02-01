using Copasa.Atende.Model.Core;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISCEReceive Atualiza status para envio conta por email
    /// </summary>
    public class SCN6ISCEReceive : BaseModelReceive
    {
        /// <summary>
        /// DescricaoRetornoSicom.
        /// </summary>
        [XmlElement("_DESC-ERRO")]
        [JsonIgnore]
        public string descricaoRetornoSicom { get; set; }
    }
}
