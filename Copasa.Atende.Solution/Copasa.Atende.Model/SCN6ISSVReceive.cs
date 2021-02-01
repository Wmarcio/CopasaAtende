using Copasa.Atende.Model.Core;
using System.Xml.Serialization;

namespace Copasa.Atende.Model
{
    /// <summary>
    /// SCN6ISSVReceive - Segunda via fatura
    /// </summary>
    public class SCN6ISSVReceive : BaseModelReceive
    {
        /// <summary>
        /// Dados.
        /// </summary>
        [XmlElement("_DADOS-SAIDA-SEGUNDA-VIA")]
        public string[] dados { get; set; }
    }
}
